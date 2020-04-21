using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Configuration;
using QC_DL;
using QC_BE;

public partial class ChangePWD : System.Web.UI.Page
{

    Master_BE objBE = new Master_BE();
    Login_DL objLogin = new Login_DL();
    CommonFuncs objCommon = new CommonFuncs();
    DataTable dt;
    string ConnKey,user;
    //string ConnKey = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        user = Session["UsrName"].ToString();
        ConnKey = Session["ConnKey"].ToString();
        if (!IsPostBack)
        {
            random();
            ViewState["KeyGenerator"] = Guid.NewGuid().ToString("N").Substring(0, 16);
            getCaptchaImage();
        }
    }
    public void getCaptchaImage()
    {      
        Captcha ci = new Captcha();
        string text = Captcha.generateRandomCode();
        ViewState["captchtext"] = text;
        Image2.ImageUrl = "~/Assets/cpImg/cpImage.aspx?randomNo=" + text;
    }
    protected void btnImgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        captch.Text = "";
        getCaptchaImage();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (PageValidate())
        {
            objBE.Action = "R"; 
            objBE.User = user;
            DataTable dtLogin = new DataTable();
            dtLogin = objLogin.getLoginDetails(objBE, ConnKey);
            
            if (dtLogin.Rows.Count > 0)
            {
                string password = dtLogin.Rows[0]["Password"].ToString();
                string myval = ShaEncrypt(ViewState["KeyGenerator"].ToString());
                string value = ShaEncrypt(password.ToLower() + myval.ToLower());
                if (password.ToLower() != txtNewPwdHash.Value)
                {
                    if (txtOldPwdHash.Value == value.ToLower())
                    {
                        dt = objLogin.UpdatePWD(user, txtNewPwdHash.Value.ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), ConnKey);
                        if (dt.Rows.Count > 0)
                        {
                            objCommon.ShowAlertMessage("Password successfully changed");
                            Response.Redirect("login.aspx");
                        }
                        else
                        {
                            txtOldPwdHash.Value = "";
                            txtNewPwdHash.Value = "";
                            objCommon.ShowAlertMessage("Invalid Old Password");
                        }
                    }
                    else
                    {
                        txtOldPwdHash.Value = "";
                        txtNewPwdHash.Value = "";
                        objCommon.ShowAlertMessage("Invalid Old Password");
                    }

                }
                else
                {
                    objCommon.ShowAlertMessage("New Password should not be same as old password");
                }
            }
            else
            {
                objCommon.ShowAlertMessage("New Password should not be same as old password");
            }
        }
    }
    protected bool PageValidate()
    {
        if (txtOpwd.Text == "")
        {
            objCommon.ShowAlertMessage("Enter Old Password");
            txtOpwd.Focus();
            return false;
        }
        if (txtNpwd.Text == "")
        {
            objCommon.ShowAlertMessage("Enter New Password");
            txtOpwd.Focus();
            return false;
        }
        if (txtCpwd.Text == "")
        {
            objCommon.ShowAlertMessage("Enter Confirm Password");
            txtCpwd.Focus();
            return false;
        }
        return true;

    }
    public string ShaEncrypt(string Ptext)
    {
        string hash = "";
        System.Security.Cryptography.SHA256CryptoServiceProvider m1 = new System.Security.Cryptography.SHA256CryptoServiceProvider();
        byte[] s1 = System.Text.Encoding.ASCII.GetBytes(Ptext);
        s1 = m1.ComputeHash(s1);
        foreach (byte bt in s1)
        {
            hash = hash + bt.ToString("x2");
        }
        return hash;
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

            Response.Cookies.Add(new HttpCookie("ASPFIXATION2", num));
            hf.Value = num;
            Session["ASPFIXATION2"] = num;
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx", false);
        }
    }
    public void check()
    {
        try
        {
            string cookie_value = null;
            string session_value = null;
            //cookie_value = System.Web.HttpContext.Current.Request.Cookies["ASPFIXATION2"].Value;
            cookie_value = hf.Value;
            session_value = System.Web.HttpContext.Current.Session["ASPFIXATION2"].ToString();
            if (cookie_value != session_value)
            {
                System.Web.HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.Redirect("~/Error.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx");
        }
    }
    
}