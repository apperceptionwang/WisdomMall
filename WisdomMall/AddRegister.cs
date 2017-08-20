using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ISO15693;

namespace WisdomMall
{
    public partial class AddRegister : Form
    {
        Reader reader = new Reader();
        static bool a = true;
        public AddRegister()
        {
            InitializeComponent();
        }

        #region 打开串口
        private void btn_OpenSerialPort_Click(object sender, EventArgs e)
        {
            if(btn_OpenSerialPort.Text=="打开串口")
            {
                if (cmb_PortID.Text != "")
                {
                    try
                    {
                        byte result = reader.OpenSerialPort(cmb_PortID.Text);
                        if (result == 0x00)
                        {
                            MessageBox.Show("串口成功打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btn_OpenSerialPort.Text = "关闭串口";
                            btn_ReadCardID.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("串口打开失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    

                }
                else
                {
                    MessageBox.Show("请选择串口号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //判断串口是否打开
                if (reader.IsOpen)
                {
                    //关闭串口方法
                    Byte value = reader.CloseSerialPort();
                    if (value == 0x00)
                    {
                        MessageBox.Show("串口关闭成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btn_OpenSerialPort.Text = "打开串口";
                        btn_ReadCardID.Enabled = false;
                        btnGoodsLogin.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("串口关闭失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("错误：串口已经处于关闭状态！"));
                }
            }
        }
        #endregion

        #region 读取卡号
        public bool ReadCardID()
        {
            try
            {
                int tagCount = 0;
                string[] tagNumber = null;
                byte result = reader.Inventory(ModulateMethod.FSK, InventoryModel.Multiple, ref tagCount, ref tagNumber);
                //判断是否读取到卡号
                if (result == 0x00)
                {
                    lbl_label.Text = tagNumber[0];
                    txt_CardID.Text = tagNumber[0];
                    a = false;
                    return true;
                }
                else
                {
                    //判断是否有对话框弹出
                    a = false;
                    MessageBox.Show("寻卡失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion


        #region 加载信息
        private void AddRegister_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“sM_DBDataSet.Goods”中。您可以根据需要移动或删除它。
            this.goodsTableAdapter.Fill(this.sM_DBDataSet.Goods);
            // TODO: 这行代码将数据加载到表“sM_DBDataSet.Model”中。您可以根据需要移动或删除它。
            this.modelTableAdapter.Fill(this.sM_DBDataSet.Model);
            string[] ports = SerialPort.GetPortNames();
            //把串口添加到comboBox控件中
            for (int i = 0; i < ports.Length; i++)
            {
                cmb_PortID.Items.Add(ports[i]);
            }
            cmb_PortID.Text = "COM102";
            byte result = reader.OpenSerialPort(cmb_PortID.Text);
            if (ReadCardID())
            {
                lbl_label.Visible = true;
            }
            btn_ReadCardID.Enabled = false;
            btnGoodsLogin.Enabled = false;
        }
        #endregion

        private void btn_ReadCardID_Click(object sender, EventArgs e)
        {      
            if(ReadCardID())
            {
                btnGoodsLogin.Enabled = true;
            }
            
        }

        #region 确认
        private void btn_OK_Click(object sender, EventArgs e)
        {
            string sql = "insert into Goods (ID,ModelID,Color,Size) values ('{0}',(select ID from Model where Model.Name='{1}'),'{2}',{3}) ";
            sql = string.Format(sql, lbl_label.Text, cmb_Type.Text, cmb_color.Text, cmb_size.Text);
            if(lbl_label.Text=="")
            {
                MessageBox.Show(String.Format("标签号为空!"));
                return;
            }
            int n = SqlDbHelper.ExecuteInsertSql(sql);
            if (n > 0)
            {
                MessageBox.Show(String.Format("插入成功!"));
            }
            else
            {
                MessageBox.Show(String.Format("插入失败!"));
            }

        }
        #endregion

        #region 取消
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
