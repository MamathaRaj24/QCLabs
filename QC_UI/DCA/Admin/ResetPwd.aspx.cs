using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using QC_BE;
using QC_DL;

public partial class DCA_Admin_ResetPwd : System.Web.UI.Page
{
    Master_BE MstObj = new Master_BE();
    Masters MstDL = new Masters();
    Login_DL mstlogin = new Login_DL();
    CommonFuncs ObjCommon = new CommonFuncs();
    DataTable dt;
    string con, user, state, Department;

    protected void Page_Load(object sender, EventArgs e)
    {
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
                Response.Redirect("../Error.aspx");
            }
        }
        PrevBrowCache.enforceNoCache();
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "0"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();


            if (!IsPostBack)
            {
                random();
                try
                {

                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindRoles();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    ObjCommon.ShowAlertMessage(ex.ToString());
                }
            }
        }
    }
    protected void BindRoles()
    {
        MstObj.Action = "ROLE";
        MstObj.Id = Department;

        dt = MstDL.Getdetails(MstObj, con);
        ObjCommon.BindDropDownLists(ddlRole, dt, "RoleName", "Role_Id", "Select");
    }
    protected void Binduser()
    {
        try
        {
            MstObj.Action = "UL";
            MstObj.Id = Department;
            MstObj.Role = ddlRole.SelectedValue;
            dt = MstDL.UserRegistration_IUDR(MstObj, con);
            ObjCommon.BindDropDownLists(ddluser, dt, "UserName", "Sno", "Select");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
    
    protected void Save_Click(object sender, EventArgs e)
    {
        check();
        try
        {          
            dt = mstlogin.ResetPWD(ddluser.SelectedItem.Text, HttpContext.Current.Request.UserHostAddress, user, con);
            if (dt.Rows.Count > 0)
            {
                ddlRole.ClearSelection();
                ddluser.ClearSelection();
                ObjCommon.ShowAlertMessage("Password Reset Successfully");
            }
            else
            {
                ObjCommon.ShowAlertMessage("Password Reset Failed, Pls try again!");

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
        
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        Binduser();
    }
    public void random()
    {
        try
        {
            string strString = "abcdefghijklmnpqrstuvwxyzABCDQEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string num = "";
            Random rm = new Random();
            for (int i = 0; i < 16; i++)
            {
                int randomcharindex = rm.Next(0, strString.Length);
                char randomchar = strString[randomcharindex];
                num += Convert.ToString(randomchar);
            }
            hf.Value = num;
            Session["ASPFIXATION2"] = num;
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx");
        }
    }
    public void check()
    {
        try
        {
            string cookie_value = null;
            string session_value = null;

            cookie_value = hf.Value;
            session_value = System.Web.HttpContext.Current.Session["ASPFIXATION2"].ToString();
            if (cookie_value != session_value)
            {
                System.Web.HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.Redirect("~/Error.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx");
        }
    }
}