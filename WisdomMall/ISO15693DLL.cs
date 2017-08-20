using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Collections;

namespace ISO15693
{
    /// <summary>
    /// 王子玉 20140820 创建（在泰格瑞德的方法上加注释；加上了错误码解释）
    /// </summary>
    public class Reader
    {
        #region 变量

        public SerialPort serialport;
        private int autoRcvdTagCount = 0;
        private string[] autoRcvdTagNum = new string[0];
        private bool isAutoRcv = false;
        private bool isRegistedEvent = false;
        private byte CurrNum = 0;

        /// <summary>
        /// 成功
        /// </summary>
        private byte AllDone = 0x00;

        /// <summary>
        /// CRC校验错误
        /// </summary>
        private byte CRCERR = 0x05;

        /// <summary>
        /// 数据长度错误
        /// </summary>
        private byte DataLengthERR = 0x06;

        /// <summary>
        /// 自动读取事件错误
        /// </summary>
        private byte EventErr = 0x09;

        /// <summary>
        /// 数据格式错误
        /// </summary>
        private byte FrameFormatErr = 0x04;

        /// <summary>
        /// 获取多卡失败
        /// </summary>
        private byte GetAllTagsNumErr = 0x21;

        /// <summary>
        /// 获取多块安全状态失败
        /// </summary>
        private byte GetMultiBlockSecErr = 0x1F;

        /// <summary>
        /// 获取读写器参数失败
        /// </summary>
        private byte GetParametersErr = 0x28;

        /// <summary>
        /// 获取标签系统信息失败
        /// </summary>
        private byte GetSystemInfoErr = 0x1E;

        /// <summary>
        /// 设置读写器的I-Code参数失败
        /// </summary>
        private byte ICodeConfigErr = 0x24;

        /// <summary>
        /// 寻卡失败
        /// </summary>
        private byte InventoryErr = 0x11;

        /// <summary>
        /// 锁定AFI失败
        /// </summary>
        private byte LockAFIErr = 0x16;

        /// <summary>
        /// 锁定数据存储格式标识失败
        /// </summary>
        private byte LockDSFIDErr = 0x1D;

        /// <summary>
        /// 锁定单块失败
        /// </summary>
        private byte LockSingleBlockErr = 0x19;

        /// <summary>
        /// 设置读写器Multiplex参数失败
        /// </summary>
        private byte MultiplexConfigErr = 0x26;

        /// <summary>
        /// 参数错误
        /// </summary>
        private byte ParameterErr = 0x08;

        /// <summary>
        /// 接收超时
        /// </summary>
        private byte RcvTimeOut = 0x03;

        /// <summary>
        /// 设置读写器工作参数失败
        /// </summary>
        private byte ReaderConfigErr = 0x22;

        /// <summary>
        /// 读取多块失败
        /// </summary>
        private byte ReadMultiBlockErr = 0x1A;

        /// <summary>
        /// 读取单块失败
        /// </summary>
        private byte ReadSingleBlockErr = 0x17;

        /// <summary>
        /// 重置标签失败
        /// </summary>
        private byte ResetToReadyErr = 0x14;

        /// <summary>
        /// 选中标签错误
        /// </summary>
        private byte SelectErr = 0x13;

        /// <summary>
        /// 发送超时
        /// </summary>
        private byte SendTimeOut = 0x02;

        /// <summary>
        /// 串口错误
        /// </summary>
        private byte SerialPortErr = 0x01;

        /// <summary>
        /// 设置天线失败
        /// </summary>
        private byte SetAntennaErr = 0x29;

        /// <summary>
        /// 静默设置失败
        /// </summary>
        private byte StayQuietErr = 0x12;

        /// <summary>
        /// 切换天线失败
        /// </summary>
        private byte SwitchChannelErr = 0x27;

        /// <summary>
        /// 设置读写器的Tag-It参数失败
        /// </summary>
        private byte TagItConfigErr = 0x25;

        /// <summary>
        /// 设置读写器定时工作参数失败
        /// </summary>
        private byte TimingConfigErr = 0x23;

        /// <summary>
        /// 写AFI失败
        /// </summary>
        private byte WriteAFIErr = 0x15;

        /// <summary>
        /// 写入数据存储格式标识失败
        /// </summary>
        private byte WriteDSFIDErr = 0x1C;

        /// <summary>
        /// 写入多块失败
        /// </summary>
        private byte WriteMultiBlockErr = 0x1B;

        /// <summary>
        /// 写入单块失败
        /// </summary>
        private byte WriteSingleBlockErr = 0x18;

        #endregion


        #region 属性

        public int AutoRcvdTagCount
        {
            get
            {
                return this.autoRcvdTagCount;
            }
        }

        public string[] AutoRcvdTagNum
        {
            get
            {
                return this.autoRcvdTagNum;
            }
        }

        public bool IsAutoRcv
        {
            get
            {
                return this.isAutoRcv;
            }
        }

        public bool IsOpen
        {
            get
            {
                try
                {
                    return this.serialport.IsOpen;
                }
                catch
                {
                    return false;
                }
            }
        }

        public string PortName
        {
            get
            {
                try
                {
                    return this.serialport.PortName;
                }
                catch
                {
                    return "";
                }
            }
        }

        #endregion


        #region 事件&委托

        public delegate void DataReceived();
        public event DataReceived OnDataArrive;


        public delegate void DelegateReadLine(string text);

        /// <summary>
        /// 串口读取事件
        /// </summary>
        public event DelegateReadLine EventReadLine;

        public delegate void DelegateWriteLine(string text);

        /// <summary>
        /// 串口写入事件
        /// </summary>
        public event DelegateWriteLine EventWriteLine;

        #endregion


        #region 私有方法



        private string ByteArrayToString(byte[] array, int StartPos, int Length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = StartPos; i < (StartPos + Length); i++)
            {
                builder = builder.Append(array[i].ToString("X2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 计算CRC
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="offset"></param>
        /// <param name="datalength"></param>
        private void CalculateCRC(ref byte[] frame, int offset, int datalength)
        {
            ushort num = 0xffff;
            ushort num2 = 0x8408;
            //ushort num2 = 0x1408;
            ushort num3 = num;
            for (ushort i = 0; i < datalength; i = (ushort)(i + 1))
            {
                num3 = (ushort)(num3 ^ ((ushort)(frame[offset + i] & 0xff)));
                for (byte j = 0; j < 8; j = (byte)(j + 1))
                {
                    if ((num3 & 1) > 0)
                    {
                        num3 = (ushort)((num3 >> 1) ^ num2);
                    }
                    else
                    {
                        num3 = (ushort)(num3 >> 1);
                    }
                }
            }
            frame[offset + datalength] = (byte)(num3 % 0x100);
            frame[(offset + datalength) + 1] = (byte)(num3 / 0x100);
        }

        /// <summary>
        /// 校验CRC
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private bool CheckCRC(byte[] frame)
        {
            int length = frame.Length;
            byte num3 = frame[length - 1];
            byte num2 = frame[length - 2];
            this.CalculateCRC(ref frame, 0, length - 2);
            return ((num3 == frame[length - 1]) && (num2 == frame[length - 2]));
        }

        private void DataHandle(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.isAutoRcv)
            {
                int num5;
                ArrayList list = new ArrayList();
                byte returnFrameNum = 0;
                byte stateCode = 0;
                int dataLength = 0;
                byte[] frameData = new byte[1];
                while (this.serialport.BytesToRead > 0)
                {
                    if (((this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData) != this.AllDone) || (stateCode != 0)) || ((dataLength == 0) || (frameData[0] != 0)))
                    {
                        break;
                    }
                    try
                    {
                        this.autoRcvdTagCount = dataLength / 10;
                        num5 = 0;
                        while (num5 < this.autoRcvdTagCount)
                        {
                            string str = "";
                            for (int i = 0; i < 8; i++)
                            {
                                str = str + frameData[(((num5 + 1) * 10) - i) - 1].ToString("X2");
                            }
                            if (list.IndexOf(str) < 0)
                            {
                                list.Add(str);
                            }
                            num5++;
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
                this.autoRcvdTagCount = list.Count;
                this.autoRcvdTagNum = new string[this.autoRcvdTagCount];
                for (num5 = 0; num5 < this.autoRcvdTagCount; num5++)
                {
                    this.autoRcvdTagNum[num5] = (string)list[num5];
                }
                this.OnDataArrive();
            }
        }

        private bool ExecuteSpecialCommand(byte[] data, out string msg)
        {
            msg = "";
            if (!this.IsOpen)
            {
                msg = "命令执行失败！原因是尚未打开串口或建立与服务器的TCP连接。";
                return false;
            }
            try
            {
                this.serialport.Write(data, 0, data.Length);
                Thread.Sleep(20);
                this.serialport.DiscardInBuffer();
                msg = "命令执行完毕！";
                return true;
            }
            catch (Exception exception)
            {
                msg = string.Format("命令执行失败！失败原因为：\r\n{0}", exception.Message);
                return false;
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="BaudRate"></param>
        /// <param name="DataBits"></param>
        /// <param name="StopBits"></param>
        /// <param name="Parity"></param>
        /// <returns></returns>
        private byte mOpenSerialPort(string portName, int BaudRate, int DataBits, StopBits StopBits, Parity Parity)
        {
            try
            {
                this.serialport = new SerialPort(portName);
                this.serialport.BaudRate = BaudRate;
                this.serialport.DataBits = DataBits;
                this.serialport.StopBits = StopBits;
                this.serialport.Parity = Parity;
                this.serialport.NewLine = "\r\n";
                this.serialport.ReadTimeout = 0xFE0;
                this.serialport.Open();
                if (this.serialport.IsOpen)
                {
                    return this.AllDone;
                }
                return this.SerialPortErr;
            }
            catch
            {
                return this.SerialPortErr;
            }
        }

        /// <summary>
        /// 串口从读写器读取数据
        /// </summary>
        /// <param name="ReturnFrameNum"></param>
        /// <param name="StateCode"></param>
        /// <param name="DataLength"></param>
        /// <param name="FrameData"></param>
        /// <returns></returns>
        private byte RcvAFrame(ref byte ReturnFrameNum, ref byte StateCode, ref int DataLength, ref byte[] FrameData)
        {
            string str;
            int num;
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            try
            {
                str = this.serialport.ReadLine();
                if (this.EventReadLine != null)
                {
                    this.EventReadLine(str);
                }
            }
            catch
            {
                return this.RcvTimeOut;
            }
            if (str.IndexOf(":") != 0)
            {
                return this.FrameFormatErr;
            }
            str = str.Remove(0, 1);
            byte[] frame = new byte[str.Length / 2];
            for (num = 0; num < frame.Length; num++)
            {
                frame[num] = Convert.ToByte(str.Substring(num * 2, 2), 0x10);
            }
            if (this.CheckCRC(frame))
            {
                ReturnFrameNum = frame[0];
                if (ReturnFrameNum < (this.CurrNum - 1))
                {
                    return this.RcvAFrame(ref ReturnFrameNum, ref StateCode, ref DataLength, ref FrameData);
                }
                StateCode = frame[1];
                DataLength = (frame[3] * 0x100) + frame[2];
                if (DataLength != (frame.Length - 6))
                {
                    return this.DataLengthERR;
                }
                if (DataLength > 0)
                {
                    FrameData = new byte[DataLength];
                    for (num = 0; num < DataLength; num++)
                    {
                        FrameData[num] = frame[4 + num];
                    }
                }
                return this.AllDone;
            }
            return this.CRCERR;
        }

        /// <summary>
        /// 串口向读写器发送命令
        /// </summary>
        /// <param name="Command">命令</param>
        /// <param name="data">命令体</param>
        /// <returns>0x00成功；0x01串口错误；0x02发送超时</returns>
        private byte SendAFrame(byte Command, byte[] data)
        {
            int num;
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte[] frame = new byte[data.Length + 6];
            frame[0] = this.CurrNum;
            frame[1] = Command;
            frame[2] = (byte)((data.Length * 2) % 0x100);
            frame[3] = (byte)((data.Length * 2) / 0x100);
            for (num = 0; num < data.Length; num++)
            {
                frame[4 + num] = (byte)(frame[4 + num] + data[num]);
            }
            this.CalculateCRC(ref frame, 0, data.Length + 4);
            string text = ":";
            for (num = 0; num < frame.Length; num++)
            {
                text = text + string.Format("{0:X2}", frame[num]);
            }
            this.CurrNum = (byte)(this.CurrNum + 1);
            try
            {
                this.serialport.DiscardInBuffer();
                this.serialport.WriteLine(text);
                if (this.EventWriteLine != null)
                {
                    this.EventWriteLine(text);
                }
                return this.AllDone;
            }
            catch
            {
                return this.SendTimeOut;
            }
        }

        #endregion


        #region 公有方法

        /// <summary>
        /// 字符串转字节数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public byte[] StringToByteArray(string str)
        {
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(str.Substring(i * 2, 2), 0x10);
            }
            return buffer;
        }

        /// <summary>
        /// 字节数组转字符串
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public string ByteArrayToString(byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in array)
            {
                builder = builder.Append(num.ToString("X2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 用于开始自动接收
        /// </summary>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte BeginAutoReceive()
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            if (!this.isRegistedEvent)
            {
                this.serialport.DataReceived += new SerialDataReceivedEventHandler(this.DataHandle);
                this.isRegistedEvent = true;
            }
            try
            {
                this.serialport.DiscardInBuffer();
                this.isAutoRcv = true;
                return this.AllDone;
            }
            catch
            {
                return this.EventErr;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte CloseSerialPort()
        {
            try
            {
                this.serialport.Close();
                if (!this.serialport.IsOpen)
                {
                    this.serialport = null;
                    return this.AllDone;
                }
                return this.SerialPortErr;
            }
            catch
            {
                return this.SerialPortErr;
            }
        }

        /// <summary>
        /// 用于开启和关闭读写器的板载蜂鸣器
        /// </summary>
        /// <param name="flag">标志，布尔型，为True时表示开启蜂鸣器，为False表示关闭蜂鸣器</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte EnableBuzzer(bool flag)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            lock (this.serialport)
            {
                byte num = flag ? ((byte)1) : ((byte)0);
                byte num2 = this.SendAFrame(240, new byte[] { num });
                if (num2 != this.AllDone)
                {
                    return num2;
                }
                Thread.Sleep(50);
                return this.AllDone;
            }
        }

        /// <summary>
        /// 用于结束自动接收
        /// </summary>
        /// <returns></returns>
        public byte EndAutoReceive()
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            this.isAutoRcv = false;
            return this.AllDone;
        }

        public bool EnterTransparentMode(ushort Target, out string msg)
        {
            int num;
            msg = "";
            byte[] bytes = Encoding.ASCII.GetBytes(Target.ToString());
            byte[] data = new byte[4 + bytes.Length];
            for (num = 0; num < data.Length; num++)
            {
                data[num] = 0x2d;
            }
            for (num = 0; num < bytes.Length; num++)
            {
                data[3 + num] = bytes[num];
            }
            return this.ExecuteSpecialCommand(data, out msg);
        }

        /// <summary>
        /// 用于获取读写器天线场区内卡片的数量和所有卡片的卡号
        /// </summary>
        /// <param name="TagCount">卡片数量，字节类型，引用参数，用于存放读取到的卡片数量</param>
        /// <param name="TagNumbers">字符串数组类型，引用参数，用于存放读取到的所有卡片的卡号</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte GetAllTagsNum(out int TagCount, out string[] TagNumbers)
        {
            TagCount = 0;
            TagNumbers = new string[0];
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte num = this.SendAFrame(0xf1, new byte[] { 2 });
            if (num != this.AllDone)
            {
                return num;
            }
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.GetAllTagsNumErr;
            }
            if (dataLength == 0)
            {
                return this.GetAllTagsNumErr;
            }
            if (frameData[0] != 0)
            {
                return this.GetAllTagsNumErr;
            }
            try
            {
                TagCount = dataLength / 10;
                List<string> list2 = new List<string>();
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < TagCount; i++)
                {
                    builder = new StringBuilder();
                    for (byte j = 0; j < 8; j = (byte)(j + 1))
                    {
                        builder.Append(string.Format("{0:X2}", frameData[(((i + 1) * 10) - j) - 1]));
                    }
                    string item = builder.ToString();
                    if (list2.IndexOf(item) < 0)
                    {
                        list2.Add(item);
                    }
                }
                TagNumbers = list2.ToArray();
                TagCount = TagNumbers.Length;
                return this.AllDone;
            }
            catch (Exception)
            {
                return this.GetAllTagsNumErr;
            }
        }

        /// <summary>
        /// 获取卡片的多个数据块的安全状态信息
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要读取卡片安全状态信息的卡片</param>
        /// <param name="BlockAddrss">字节类型，用于指定需要获取安全状态信息的多个数据块中的首个数据块地址</param>
        /// <param name="BlockCount">字节类型，用于指定需要获取安全状态信息的数据块数量</param>
        /// <param name="SecStatus">字节数组型，引用类型，用于存放卡片相应数据块的安全状态值，正常情况下，命令执行完后，该数组的长度应该等于参数三中指定的数据块数量，因为是一个数据块，一个直接的安全状态信息</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte GetMultiBlockSec(string TagNum, byte BlockAddrss, byte BlockCount, ref byte[] SecStatus)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 15;
            byte[] data = new byte[11];
            data[0] = 0x23;
            data[1] = BlockAddrss;
            data[2] = (BlockCount > 0) ? ((byte)(BlockCount - 1)) : BlockCount;
            TagNum = TagNum.Trim();
            try
            {
                for (byte j = 0; j < 8; j = (byte)(j + 1))
                {
                    data[3 + j] = Convert.ToByte(TagNum.Substring((7 - j) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.GetMultiBlockSecErr;
            }
            if (dataLength != (BlockCount + 1))
            {
                return this.GetMultiBlockSecErr;
            }
            if (frameData[0] != 0)
            {
                return this.GetMultiBlockSecErr;
            }
            SecStatus = new byte[BlockCount];
            for (int i = 0; i < SecStatus.Length; i++)
            {
                SecStatus[i] = frameData[1 + i];
            }
            return this.AllDone;
        }

        /// <summary>
        /// 获取读写器当前的参数配置
        /// </summary>
        /// <param name="Parameters">用于输出读写器的参数配置。长度为52个字节</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte GetParameters(out byte[] Parameters)
        {
            Parameters = new byte[0];
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte[] data = new byte[0];
            byte num = this.SendAFrame(0x9b, data);
            if (num != this.AllDone)
            {
                return num;
            }
            Thread.Sleep(100);
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.GetParametersErr;
            }
            if (dataLength == 0)
            {
                return this.GetParametersErr;
            }
            if (frameData[0] != 0)
            {
                return this.GetParametersErr;
            }
            Parameters = new byte[frameData.Length - 1];
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = frameData[1 + i];
            }
            return this.AllDone;
        }

        /// <summary>
        /// 获取标签系统信息
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要读取卡片信息的卡片</param>
        /// <param name="InfoFlag">信息标志，字节型，引用类型，用于存储卡片的信息标志，有关信息标志位的具体含义，请参考其它相关的技术文档和标准规范</param>
        /// <param name="DSFID">字节型，引用类型，用于存储卡片的DSFID值</param>
        /// <param name="AFI">字节型，引用类型，用于存储卡片的AFI值</param>
        /// <param name="VICCMemorySize">无符号短整型，引用类型，用于存储卡片的VICCMemorySize的值</param>
        /// <param name="ICReference">字节型，引用类型，用于存储卡片的ICReference值</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte GetSystemInfo(string TagNum, ref byte InfoFlag, ref byte DSFID, ref byte AFI, ref ushort VICCMemorySize, ref byte ICReference)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 14;
            byte[] data = new byte[9];
            data[0] = 0x23;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[1 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.GetSystemInfoErr;
            }
            if (dataLength != 15)
            {
                return this.GetSystemInfoErr;
            }
            if (frameData[0] != 0)
            {
                return this.GetSystemInfoErr;
            }
            InfoFlag = frameData[1];
            DSFID = frameData[10];
            AFI = frameData[11];
            VICCMemorySize = (ushort)(frameData[12] + (frameData[13] * 0x100));
            ICReference = frameData[14];
            return this.AllDone;
        }

        /// <summary>
        /// 用于设置读写器的I-Code参数
        /// </summary>
        /// <param name="ConfigData"></param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte ICodeConfig(byte[] ConfigData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte num = this.SendAFrame(0x87, ConfigData);
            if (num != this.AllDone)
            {
                return num;
            }
            Thread.Sleep(100);
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.ICodeConfigErr;
            }
            if (dataLength == 0)
            {
                return this.ICodeConfigErr;
            }
            if (frameData[0] != 0)
            {
                return this.ICodeConfigErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 寻卡，获取场区内卡片的卡号
        /// </summary>
        /// <param name="mm">ASK或FSK</param>
        /// <param name="im">单卡或多卡</param>
        /// <param name="TagCount">卡片数量</param>
        /// <param name="TagNumber">卡号</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte Inventory(ModulateMethod mm, InventoryModel im, ref int TagCount, ref string[] TagNumber)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 1;
            byte[] data = new byte[] { (byte)(((byte)mm) + ((byte)im)) };
            byte num2 = this.SendAFrame(command, data);
            if (num2 != this.AllDone)
            {
                return num2;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num2 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num2 != this.AllDone)
            {
                return num2;
            }
            if (stateCode != 0)
            {
                return this.InventoryErr;
            }
            if (dataLength == 0)
            {
                return this.InventoryErr;
            }
            if (frameData[0] != 0)
            {
                return this.InventoryErr;
            }
            try
            {
                TagCount = dataLength / 10;
                TagNumber = new string[TagCount];
                for (int i = 0; i < TagCount; i++)
                {
                    TagNumber[i] = "";
                    for (int j = 0; j < 8; j++)
                    {
                        string[] strArray;
                        IntPtr ptr;
                        (strArray = TagNumber)[(int)(ptr = (IntPtr)i)] = strArray[(int)ptr] + frameData[(((i + 1) * 10) - j) - 1].ToString("X2");
                    }
                }
                return this.AllDone;
            }
            catch
            {
                return this.InventoryErr;
            }
        }

        /// <summary>
        /// 锁定AFI，用以锁定卡片的AFI值，注意，AFI值的锁定是不可逆的，一旦被锁定，将不能再修改和解锁，因此要慎用
        /// </summary>
        /// <param name="TagNum">TagNum，卡号，字符串类型，用于指定需要进行AFI锁定的卡片</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte LockAFI(string TagNum)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 6;
            byte[] data = new byte[9];
            data[0] = 0x23;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[1 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.LockAFIErr;
            }
            if (dataLength != 1)
            {
                return this.LockAFIErr;
            }
            if (frameData[0] != 0x23)
            {
                return this.LockAFIErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 锁定DSFID，用以锁定卡片的DSFID值，注意，DSFID值的锁定是不可逆的，一旦被锁定，将不能再修改和解锁，因此要慎用
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要进行DSFID锁定的卡片</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte LockDSFID(string TagNum)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 13;
            byte[] data = new byte[9];
            data[0] = 0x23;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[1 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.LockAFIErr;
            }
            if (dataLength != 1)
            {
                return this.LockAFIErr;
            }
            if (frameData[0] != 0x41)
            {
                return this.LockAFIErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 锁定单个数据块的数据，注意，这样的锁定时不可逆的，因此要慎用
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要进行数据写入的卡片</param>
        /// <param name="BlockAddress">字节类型，用于指定需要锁定的数据块地址</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte LockSingleBlock(string TagNum, byte BlockAddress)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 9;
            byte[] data = new byte[10];
            data[0] = 0x23;
            data[1] = BlockAddress;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[2 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.LockSingleBlockErr;
            }
            if (dataLength != 1)
            {
                return this.LockSingleBlockErr;
            }
            if (frameData[0] != 0x41)
            {
                return this.LockSingleBlockErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 用于设置读写器的Multiplex参数
        /// </summary>
        /// <param name="ConfigData"></param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte MultiplexConfig(byte[] ConfigData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte num = this.SendAFrame(0x41, ConfigData);
            if (num != this.AllDone)
            {
                return num;
            }
            Thread.Sleep(100);
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.MultiplexConfigErr;
            }
            if (dataLength == 0)
            {
                return this.MultiplexConfigErr;
            }
            if (frameData[0] != 0)
            {
                return this.MultiplexConfigErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName">COM</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte OpenSerialPort(string portName)
        {
            return this.mOpenSerialPort(portName, 0x1c200, 8, StopBits.One, Parity.None);
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName">COM</param>
        /// <param name="BaudRate">波特率：默认115200</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte OpenSerialPort(string portName, int BaudRate)
        {
            return this.mOpenSerialPort(portName, BaudRate, 8, StopBits.One, Parity.None);
        }

        public bool QuitTransparentMode(out string msg)
        {
            msg = "";
            byte[] data = new byte[] { 0x3d, 0x3d, 0x3d };
            return this.ExecuteSpecialCommand(data, out msg);
        }

        /// <summary>
        /// 用于设置读写器的工作参数
        /// </summary>
        /// <param name="ConfigData"></param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte ReaderConfig(byte[] ConfigData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte num = this.SendAFrame(0x9a, ConfigData);
            if (num != this.AllDone)
            {
                return num;
            }
            Thread.Sleep(100);
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.ReaderConfigErr;
            }
            if (dataLength == 0)
            {
                return this.ReaderConfigErr;
            }
            if (frameData[0] != 0)
            {
                return this.ReaderConfigErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 读取多个数据块的数据
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要读取的卡片</param>
        /// <param name="bl">枚举类型，用于指定卡片的数据块长度</param>
        /// <param name="BlockAddrss">字节类型，用于指定需要读取的多个数据块中的首个数据块地址</param>
        /// <param name="BlockCount">字节类型，用于指定需要读取的数据块数量</param>
        /// <param name="BlockData">字节数组类型，引用参数，用于存放读取出来的块数据，正常情况下，函数执行后该字节数组的长度应该等于参数2指定的数据块长度与参数4指定的数据块数量的积</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte ReadMultiBlock(string TagNum, BlockLength bl, byte BlockAddrss, byte BlockCount, ref byte[] BlockData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 10;
            byte[] data = new byte[12];
            data[0] = (byte)bl;
            data[1] = 0x23;
            data[2] = BlockAddrss;
            data[3] = (BlockCount > 0) ? ((byte)(BlockCount - 1)) : BlockCount;
            TagNum = TagNum.Trim();
            try
            {
                for (byte j = 0; j < 8; j = (byte)(j + 1))
                {
                    data[4 + j] = Convert.ToByte(TagNum.Substring((7 - j) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.ReadMultiBlockErr;
            }
            if (dataLength != ((((byte)bl) * BlockCount) + 1))
            {
                return this.ReadMultiBlockErr;
            }
            if (frameData[0] != 0)
            {
                return this.ReadMultiBlockErr;
            }
            BlockData = new byte[((byte)bl) * BlockCount];
            for (int i = 0; i < BlockData.Length; i++)
            {
                BlockData[i] = frameData[1 + i];
            }
            return this.AllDone;
        }

        /// <summary>
        /// 读取单个数据块的数据
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要进行数据读取的卡片</param>
        /// <param name="bl">枚举类型，用于指定卡片的数据块长度</param>
        /// <param name="BlockAddrss">用于指定需要进行读取的数据块地址</param>
        /// <param name="BlockData">字节数组类型，用于存放读取到的块数据</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte ReadSingleBlock(string TagNum, BlockLength bl, byte BlockAddrss, ref byte[] BlockData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 7;
            byte[] data = new byte[11];
            data[0] = (byte)bl;
            data[1] = 0x23;
            data[2] = BlockAddrss;
            TagNum = TagNum.Trim();
            try
            {
                for (byte j = 0; j < 8; j = (byte)(j + 1))
                {
                    data[3 + j] = Convert.ToByte(TagNum.Substring((7 - j) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.ReadSingleBlockErr;
            }
            if (dataLength != (((byte)bl) + 1))
            {
                return this.ReadSingleBlockErr;
            }
            if (frameData[0] != 0)
            {
                return this.ReadSingleBlockErr;
            }
            BlockData = new byte[(byte)bl];
            for (int i = 0; i < BlockData.Length; i++)
            {
                BlockData[i] = frameData[1 + i];
            }
            return this.AllDone;
        }

        /// <summary>
        /// 重置卡片，两个重载，第一个适用于重置所有卡片，第二个适用于重置特定的卡片。重置模式ResetMode中共包含四种重置模式，RstAllQuiet为重置场区内所有静默的卡片，RstAllSelected为重置场区内所有被选中的卡片，RstSpecificQuiet为重置场区内某个特定（指定卡号）的静默的卡片，RstSpecificSelected为重置场区内某个特定的被选中的卡片。因此重载一用于前两种重置模式，重载二用于后两种重置模式
        /// </summary>
        /// <param name="rm">ResetMode枚举类型，用于选择重置模式</param>
        /// <returns></returns>
        public byte ResetToReady(ResetMode rm)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 4;
            byte[] data = new byte[] { (byte)rm };
            if (data[0] > 0x13)
            {
                return this.ParameterErr;
            }
            byte num2 = this.SendAFrame(command, data);
            if (num2 != this.AllDone)
            {
                return num2;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num2 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num2 != this.AllDone)
            {
                return num2;
            }
            if (stateCode != 0)
            {
                return this.ResetToReadyErr;
            }
            if (dataLength != 1)
            {
                return this.ResetToReadyErr;
            }
            if (frameData[0] != 0)
            {
                return this.ResetToReadyErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 重置卡片，两个重载，第一个适用于重置所有卡片，第二个适用于重置特定的卡片。重置模式ResetMode中共包含四种重置模式，RstAllQuiet为重置场区内所有静默的卡片，RstAllSelected为重置场区内所有被选中的卡片，RstSpecificQuiet为重置场区内某个特定（指定卡号）的静默的卡片，RstSpecificSelected为重置场区内某个特定的被选中的卡片。因此重载一用于前两种重置模式，重载二用于后两种重置模式
        /// </summary>
        /// <param name="rm">ResetMode枚举类型，用于选择重置模式</param>
        /// <param name="TagNum">字符串类型，只有在后面两种重置模式中使用，通过该参数设定需要进行重置的卡片</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte ResetToReady(ResetMode rm, string TagNum)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 4;
            byte[] data = new byte[9];
            data[0] = (byte)rm;
            if (data[0] < 0x23)
            {
                return this.ResetToReady(rm);
            }
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[1 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.ResetToReadyErr;
            }
            if (dataLength != 1)
            {
                return this.ResetToReadyErr;
            }
            if (frameData[0] != 0)
            {
                return this.ResetToReadyErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 选择指定的卡片，让卡片进入选中状态；该命令可以让卡片从静默状态中恢复
        /// </summary>
        /// <param name="TagNum">TagNum，卡号，字符串类型，用于指定需要选择的卡片</param>
        /// <returns>>成功返回0x00，失败返回其它</returns>
        public byte Select(string TagNum)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 3;
            byte[] data = new byte[9];
            data[0] = 0x23;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[1 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.SelectErr;
            }
            if (dataLength != 1)
            {
                return this.SelectErr;
            }
            if (frameData[0] != 0)
            {
                return this.SelectErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 设置与读写器连接的天线
        /// </summary>
        /// <param name="AntannaIndex">Byte型参数AntannaIndex为天线序号(有效值为x01~0x28,即1~40)</param>
        /// <returns>Byte类型，0x00:设置成功;  0x01:设置失败，原因是串口尚未打开;  0x08:设置失败，原因是天线序号不在有效范围内;  0x29:设置失败，失败原因是串口发送或接收超时</returns>
        public byte SetAntanna(byte AntannaIndex)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            if ((AntannaIndex < 1) || (AntannaIndex > 40))
            {
                return this.ParameterErr;
            }
            byte command = 0xf4;
            byte[] data = new byte[] { AntannaIndex };
            byte num2 = this.SendAFrame(command, data);
            if (num2 != this.AllDone)
            {
                return num2;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num2 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num2 != this.AllDone)
            {
                return num2;
            }
            if (stateCode != 0)
            {
                return this.SetAntennaErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 静默，让指定的卡片进入静默状态，卡片进入静默状态时，卡片不再响应Inventory命令
        /// </summary>
        /// <param name="TagNum">TagNum，卡号，字符串类型，用于指定需要静默的卡片</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        private byte StayQuiet(string TagNum)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 2;
            byte[] data = new byte[9];
            data[0] = 0x23;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[1 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.StayQuietErr;
            }
            if (dataLength != 1)
            {
                return this.StayQuietErr;
            }
            if (frameData[0] != 0)
            {
                return this.StayQuietErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 用于切换与读写器连接的天线
        /// </summary>
        /// <param name="Channel">字节型，表示天线的编号</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte SwitchChannel(byte Channel)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte[] buffer3 = new byte[3];
            buffer3[0] = Channel;
            byte[] data = buffer3;
            byte num = this.SendAFrame(0x40, data);
            if (num != this.AllDone)
            {
                return num;
            }
            Thread.Sleep(100);
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.SwitchChannelErr;
            }
            if (dataLength == 0)
            {
                return this.SwitchChannelErr;
            }
            if (frameData[0] != 0)
            {
                return this.SwitchChannelErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 用于设置读写器的Tag-It参数
        /// </summary>
        /// <param name="ConfigData"></param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte TagItConfig(byte[] ConfigData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte num = this.SendAFrame(0x77, ConfigData);
            if (num != this.AllDone)
            {
                return num;
            }
            Thread.Sleep(100);
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.TagItConfigErr;
            }
            if (dataLength == 0)
            {
                return this.TagItConfigErr;
            }
            if (frameData[0] != 0)
            {
                return this.TagItConfigErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 用于设置读写器定时工作参数
        /// </summary>
        /// <param name="ConfigData"></param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte TimingConfig(byte[] ConfigData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte num = this.SendAFrame(0x42, ConfigData);
            if (num != this.AllDone)
            {
                return num;
            }
            Thread.Sleep(100);
            ArrayList list = new ArrayList();
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num != this.AllDone)
            {
                return num;
            }
            if (stateCode != 0)
            {
                return this.TimingConfigErr;
            }
            if (dataLength == 0)
            {
                return this.TimingConfigErr;
            }
            if (frameData[0] != 0)
            {
                return this.TimingConfigErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 重写卡片的AFI值
        /// </summary>
        /// <param name="TagNum">TagNum，卡号，字符串类型，用于指定需要进行AFI值重写的卡片</param>
        /// <param name="AFI">AFI，字节类型，用于指定需要写入到卡片的AFI值</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte WriteAFI(string TagNum, byte AFI)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 5;
            byte[] data = new byte[10];
            data[0] = 0x23;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[1 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            data[9] = AFI;
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.WriteAFIErr;
            }
            if (dataLength != 1)
            {
                return this.WriteAFIErr;
            }
            if (frameData[0] != 0x41)
            {
                return this.WriteAFIErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 重写卡片的DSFID值
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要进行DSFID值重写的卡片</param>
        /// <param name="DSFID">字节类型，用于指定需要写入到卡片的DSFID值</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte WriteDSFID(string TagNum, byte DSFID)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 12;
            byte[] data = new byte[10];
            data[0] = 0x23;
            data[1] = DSFID;
            TagNum = TagNum.Trim();
            try
            {
                for (byte i = 0; i < 8; i = (byte)(i + 1))
                {
                    data[2 + i] = Convert.ToByte(TagNum.Substring((7 - i) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            num3 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num3 != this.AllDone)
            {
                return num3;
            }
            if (stateCode != 0)
            {
                return this.WriteDSFIDErr;
            }
            if (dataLength != 1)
            {
                return this.WriteDSFIDErr;
            }
            if (frameData[0] != 0x41)
            {
                return this.WriteDSFIDErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 写入多个数据块的数据
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要进行数据写入的卡片</param>
        /// <param name="bl">枚举类型，用于指定卡片的数据块长度</param>
        /// <param name="BlockAddrss">字节类型，用于指定需要写入的多个数据块中的首个数据块地址</param>
        /// <param name="BlockCount">字节类型，用于指定需要写入的数据块数量</param>
        /// <param name="BlockData">字节数组类型，用于存放需要写入的块数据，该字节数组的长度应该等于参数2指定的数据块长度与参数4指定的数据块数量的积，其中存放需要写入的数据</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte WriteMultiBlock(string TagNum, BlockLength bl, byte BlockAddrss, byte BlockCount, byte[] BlockData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 11;
            int dataLength = ((byte)bl) * BlockCount;
            byte[] data = new byte[12 + dataLength];
            data[0] = (byte)bl;
            data[1] = 0x23;
            data[2] = BlockAddrss;
            data[3] = (BlockCount > 0) ? ((byte)(BlockCount - 1)) : BlockCount;
            TagNum = TagNum.Trim();
            try
            {
                byte num3;
                for (num3 = 0; num3 < dataLength; num3 = (byte)(num3 + 1))
                {
                    data[4 + num3] = BlockData[num3];
                }
                for (num3 = 0; num3 < 8; num3 = (byte)(num3 + 1))
                {
                    data[(4 + dataLength) + num3] = Convert.ToByte(TagNum.Substring((7 - num3) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num4 = this.SendAFrame(command, data);
            if (num4 != this.AllDone)
            {
                return num4;
            }
            byte returnFrameNum = 0;
            byte stateCode = 0;
            dataLength = 0;
            byte[] frameData = new byte[1];
            num4 = this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData);
            if (num4 != this.AllDone)
            {
                return num4;
            }
            if (stateCode != 0)
            {
                return this.WriteMultiBlockErr;
            }
            if (dataLength != 1)
            {
                return this.WriteMultiBlockErr;
            }
            if (frameData[0] != 0)
            {
                return this.WriteMultiBlockErr;
            }
            return this.AllDone;
        }

        /// <summary>
        /// 写入单个数据块的数据
        /// </summary>
        /// <param name="TagNum">卡号，字符串类型，用于指定需要进行数据写入的卡片</param>
        /// <param name="bl">枚举类型，用于指定卡片的数据块长度</param>
        /// <param name="BlockAddrss">字节类型，用于指定需要进行写入的数据块地址</param>
        /// <param name="BlockData">字节数组类型，用于存放需要写入的块数据，注意，该字节数组的长度必须大于等于参数2中的规定块长度的值</param>
        /// <returns>成功返回0x00，失败返回其它</returns>
        public byte WriteSingleBlock(string TagNum, BlockLength bl, byte BlockAddrss, byte[] BlockData)
        {
            if (!this.IsOpen)
            {
                return this.SerialPortErr;
            }
            byte command = 8;
            byte[] data = new byte[11 + ((byte)bl)];
            data[0] = (byte)bl;
            data[1] = 0x23;
            data[2] = BlockAddrss;
            TagNum = TagNum.Trim();
            try
            {
                byte num2;
                for (num2 = 0; num2 < ((byte)bl); num2 = (byte)(num2 + 1))
                {
                    data[3 + num2] = BlockData[num2];
                }
                for (num2 = 0; num2 < 8; num2 = (byte)(num2 + 1))
                {
                    data[(3 + ((byte)bl)) + num2] = Convert.ToByte(TagNum.Substring((7 - num2) * 2, 2), 0x10);
                }
            }
            catch
            {
                return this.ParameterErr;
            }
            byte num3 = this.SendAFrame(command, data);
            if (num3 != this.AllDone)
            {
                return num3;
            }

            byte returnFrameNum = 0;
            byte stateCode = 0;
            int dataLength = 0;
            byte[] frameData = new byte[1];
            if ((this.RcvAFrame(ref returnFrameNum, ref stateCode, ref dataLength, ref frameData) == this.AllDone) && (frameData[0] == 0))
            {
                return this.AllDone;
            }

            byte[] blockData = new byte[0];
            if ((this.ReadSingleBlock(TagNum, bl, BlockAddrss, ref blockData) == 0) && this.ByteArrayToString(BlockData).Equals(this.ByteArrayToString(blockData)))
            {
                return this.AllDone;
            }

            return this.WriteSingleBlockErr;
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <returns>错误信息</returns>
        public string GetMessageError(byte errorCode)
        {
            string result = "无此错误信息";

            switch (errorCode)
            {
                case 0x05:
                    result = "CRC校验错误";
                    break;
                case 0x06:
                    result = "数据长度错误";
                    break;
                case 0x09:
                    result = "自动读取事件错误";
                    break;
                case 0x04:
                    result = "数据格式错误";
                    break;
                case 0x21:
                    result = "获取多卡失败";
                    break;
                case 0x1F:
                    result = "获取多块安全状态失败";
                    break;
                case 0x28:
                    result = "获取读写器参数失败";
                    break;
                case 0x1E:
                    result = "获取卡片系统信息失败";
                    break;
                case 0x24:
                    result = "设置读写器的I-Code参数失败";
                    break;
                case 0x11:
                    result = "寻卡失败";
                    break;
                case 0x16:
                    result = "锁定AFI失败";
                    break;
                case 0x1D:
                    result = "锁定数据存储格式标识失败";
                    break;
                case 0x19:
                    result = "锁定单块失败";
                    break;
                case 0x26:
                    result = "设置读写器Multiplex参数失败";
                    break;
                case 0x08:
                    result = "参数错误";
                    break;
                case 0x03:
                    result = "接收超时";
                    break;
                case 0x22:
                    result = "设置读写器工作参数失败";
                    break;
                case 0x1A:
                    result = "读取多块失败";
                    break;
                case 0x17:
                    result = "读取单块失败";
                    break;
                case 0x14:
                    result = "重置卡片失败";
                    break;
                case 0x13:
                    result = "选中标签错误";
                    break;
                case 0x02:
                    result = "发送超时";
                    break;
                case 0x01:
                    result = "串口错误";
                    break;
                case 0x29:
                    result = "设置天线失败";
                    break;
                case 0x12:
                    result = "静默设置失败";
                    break;
                case 0x27:
                    result = "切换天线失败";
                    break;
                case 0x25:
                    result = "设置读写器的Tag-It参数失败";
                    break;
                case 0x23:
                    result = "设置读写器定时工作参数失败";
                    break;
                case 0x15:
                    result = "写AFI失败";
                    break;
                case 0x1C:
                    result = "写入数据存储格式标识失败";
                    break;
                case 0x1B:
                    result = "写入多块失败";
                    break;
                case 0x18:
                    result = "写入单块失败";
                    break;
            }

            return result;
        }

        #endregion
    }

    /// <summary>
    /// 调制方式
    /// </summary>
    public enum ModulateMethod
    {
        ASK = 0x06,
        FSK = 0x07
    }

    /// <summary>
    /// 寻卡方式
    /// </summary>
    public enum InventoryModel
    {
        Single = 0x20,
        Multiple = 0x00
    }

    /// <summary>
    /// 重置模式
    /// </summary>
    public enum ResetMode
    {
        RstAllQuiet = 0x03,
        RstAllSelected = 0x13,
        RstSpecificQuiet = 0x23,
        RstSpecificSelected = 0x33
    }

    /// <summary>
    /// 块长度
    /// </summary>
    public enum BlockLength
    {
        ShortBlock4Byte = 0x04,
        LongBlock8Byte = 0x08
    }


}
