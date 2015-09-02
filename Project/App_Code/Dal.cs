using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;


/// <summary>
/// Summary description for Dal
/// </summary>
static public class Dal
{
    const string dbName = "DB.accdb";
    public static string GetConnectionString()
    {
        return "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + HttpContext.Current.Server.MapPath("~/App_Data/") + dbName+"; Persist Security Info=True";
    }
    //מחרוזת SQL
    //מספר שורות ששונו
    static public int ExecuteNonQuery(string sql)
    {
        int numOfUpdateRows;
        OleDbConnection con = null;
        OleDbCommand cmd = null;  
        try
        {
            con = new OleDbConnection(GetConnectionString());
            cmd = new OleDbCommand(sql, con);
            con.Open();
            numOfUpdateRows = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            numOfUpdateRows=0;
        }
        finally
        {
            con.Close();            
        }
        return numOfUpdateRows;
    }
    // משפט SQL
    //מוחזר העצם עליו בוצעה הפעולה

    static public object ExecuteScalar(string sql)
    {
        OleDbConnection con = null;
        OleDbCommand cmd = null;
        object val = null;
        try
        {
            con = new OleDbConnection(GetConnectionString());
            cmd = new OleDbCommand(sql, con);
            con.Open();
            val = cmd.ExecuteScalar();
        }
        catch (Exception e)
        {
            val = null;
        }
        finally
        {
            con.Close();
        }
        return val;

    }
    //  // משפט SQL
    // הפעולה מחזירה אוסף טבלאות בהתאם למשפט SQL

    static public DataSet GetDataSet(string sql)
    {
        OleDbConnection con = null;
        DataSet ds = null;
        try
        {
            con = new OleDbConnection(GetConnectionString());
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception e)
        {
            ds = null;
        }
        return ds;
    }
}