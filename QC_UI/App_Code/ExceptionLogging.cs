using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using context = System.Web.HttpContext;
using System.Data;   
/// <summary>
/// Summary description for LogException
/// </summary>
public class ExceptionLogging
{
    private static String exepurl;
    static SqlConnection con;
    private static void connection()
    {
        string ConnKey = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        con = new SqlConnection(ConnKey);
        con.Open();
    }
    public static void SendExcepToDB(Exception exdb,string UserName , string IpAddress)
    {
        /*STORE USERNAME , IP ADDRESS*/
        connection();
        exepurl = context.Current.Request.Url.ToString();
        SqlCommand com = new SqlCommand("ExceptionLoggingToDataBase", con);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
        com.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
        com.Parameters.AddWithValue("@ExceptionURL", exepurl);
        com.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());
        com.Parameters.AddWithValue("@UserName", UserName);
        com.Parameters.AddWithValue("@IPAddress", IpAddress);
        com.ExecuteNonQuery();



    }   
}