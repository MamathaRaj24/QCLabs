using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Configuration;
using System.Text;
using QC_DL;

public partial class Logout : System.Web.UI.Page
{
    string ConnKey;

    protected void Page_Load(object sender, EventArgs e)
    {
        PrevBrowCache.enforceNoCache();
        if ((Request.ServerVariables["HTTP_REFERER"] == null) || (Request.ServerVariables["HTTP_REFERER"] == ""))
        {
            Response.Redirect("~/Error.aspx");
        }
        else
        {
            string http_ref = Request.ServerVariables["HTTP_REFERER"].Trim();
            string http_hos = Request.ServerVariables["HTTP_HOST"].Trim();
            int len = http_hos.Length;
            if (http_ref.IndexOf(http_hos, 0) < 0)
            {
                Response.Redirect("~/Error.aspx", false);
            }
        }
        try
        {
            ConnKey = Session["ConnKey"].ToString();
            Login_DL objLogin = new Login_DL();
            if (Session["UsrName"] != null)
            {
                try
                {
                    objLogin.updateUserLoginStatus(Convert.ToInt32(Session["LoginSno"].ToString()), "Logout Success", DateTime.Now, ConnKey);

                    try
                    {
                        HttpCookie aCookie;
                        string cookieName;
                        int limit = Request.Cookies.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            cookieName = Request.Cookies[i].Name;
                            aCookie = new HttpCookie(cookieName);
                            aCookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(aCookie);
                        }
                    }
                    catch { }

                    Session.Abandon();
                    Session.Clear();
                    Session.RemoveAll();

                    DeleteCookie.DelCookie();
                    if (Request.Cookies["ASP.NET_SessionId"] != null)
                    {
                        Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                    }

                    if (Request.Cookies["AuthToken"] != null)
                    {
                         Response.Cookies["AuthToken"].Value = string.Empty;
                        Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                    }

                }
                catch (Exception ex)
                {
                    //ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    Response.Redirect("~/Error.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            //ExceptionLogging.SendExcepToDB(ex, "0", Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx", false);
        }
    }
}