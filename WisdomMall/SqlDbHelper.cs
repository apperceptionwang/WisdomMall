// File:    SqlDbHelper.cs
// Author:  audrey
// Created: 2009年6月15日 10:30:03
// Purpose: Definition of Class SqlDbHelper

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

public class SqlDbHelper
{
    //private static string CONNECTION_STRING = "server=william\\sqlexpress;database=musicbar;uid=sa;pwd=william.shan";
    //Data Source=.\SQLEXPRESS;AttachDbFilename=D:\MusicWebSite\ch6\App_Data\MusicBar.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True

    //private static string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|MusicBar.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
    //private static string CONNECTION_STRING =
    //    System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();(有效)
    private static string CONNECTION_STRING = "server = .;database = SM_DB;uid = sa;pwd = 123";


    /// <summary>
    /// 执行Select语句，
    /// </summary>
    /// <param name="strSql"></param>
    /// <returns></returns>
    public static DataSet ExecuteSelectSql(string strSql)
    {
        SqlConnection conn = new SqlConnection(CONNECTION_STRING);
        SqlDataAdapter da = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception("执行SQL出现错误：\r\n" + strSql + "\r\n" + ex.ToString());
        }
        finally
        {
            conn.Close();
        }


        //throw new NotImplementedException();
    }
    /// <summary>
    /// 执行UPDATE语句
    /// </summary>
    /// <param name="strSql"></param>
    /// <returns></returns>
    public static int ExecuteUpdateSql(string strSql)
    {

        SqlConnection conn = new SqlConnection(CONNECTION_STRING);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandType = CommandType.Text;
        comm.CommandText = strSql;
        int ret = -1;
        try
        {
            conn.Open();
            ret = comm.ExecuteNonQuery();

            return ret;
        }
        catch (Exception ex)
        {
            throw new Exception("执行SQL出现错误：\r\n" + strSql + "\r\n" + ex.ToString());
        }
        finally
        {
            conn.Close();
        }

        //throw new NotImplementedException();
    }
    /// <summary>
    /// 执行Delete语句
    /// </summary>
    /// <param name="strSql"></param>
    /// <returns></returns>
    public static int ExecuteDelSql(string strSql)
    {
        SqlConnection conn = new SqlConnection(CONNECTION_STRING);
        SqlCommand comm = new SqlCommand(strSql, conn);
        int ret = -1;
        try
        {
            conn.Open();
            ret = comm.ExecuteNonQuery();
            return ret;
        }
        catch (Exception ex)
        {
            throw new Exception("执行SQL出现错误：\r\n" + strSql + "\r\n" + ex.ToString());
        }
        finally
        {
            conn.Close();
        }

        //throw new NotImplementedException();
    }
    /// <summary>
    /// 执行Insert语句
    /// </summary>
    /// <param name="strSql"></param>
    /// <returns></returns>
    public static int ExecuteInsertSql(string strSql)
    {
        SqlConnection conn = new SqlConnection(CONNECTION_STRING);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandType = CommandType.Text;
        comm.CommandText = strSql;
        int ret = -1;
        try
        {
            conn.Open();
            ret = comm.ExecuteNonQuery();

            return ret;
        }
        catch (Exception ex)
        {
            throw new Exception("执行SQL出现错误：\r\n" + strSql + "\r\n" + ex.ToString());
        }
        finally
        {
            conn.Close();
        }


        //throw new NotImplementedException();
    }

    public static Object ExecuteScalar(string strSql)
    {
        SqlConnection conn = new SqlConnection(CONNECTION_STRING);
        SqlCommand comm = new SqlCommand();
        comm.Connection = conn;
        comm.CommandType = CommandType.Text;
        comm.CommandText = strSql;

        try
        {
            conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.Read())
            {
                return dr[0];
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("执行SQL出现错误：\r\n" + strSql + "\r\n" + ex.ToString());
        }
        finally
        {
            conn.Close();
        }


        //throw new NotImplementedException();
    }

    public SqlConnection GetConnection()
    {
        SqlConnection conn = new SqlConnection(CONNECTION_STRING);
        return conn;
        //throw new NotImplementedException();
    }



}