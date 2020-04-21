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


public partial class Agri_Login : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Login_DL objLogin = new Login_DL();
    CommonFuncs objCommon = new CommonFuncs();
    DataTable dt;
    string ConnKey = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUname.Attributes.Add("autocomplete", "off");
        txtPwd.Attributes.Add("autocomplete", "off");
        if (!IsPostBack)
        {
            random();
            ViewState["KeyGenerator"] = Guid.NewGuid().ToString("N").Substring(0, 16);
            txtUname.Focus();
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
    protected bool CheckCaptcha()
    {
        //if (captch.Text == "")
        //{
        //    lblmsg.Text = "Enter Captcha Text";
        //    return false;
        //}
        //else if (captch.Text != ViewState["captchtext"].ToString())
        //{
        //    lblmsg.Text = "Enter Captcha Text as shown in the image";
        //    captch.Text = "";
        //    return false;
        //}
        //else if (captch.Text == ViewState["captchtext"].ToString())
        //    return true;
        //else
        //{
        //    lblmsg.Text = "image code is not valid.";
        //    captch.Text = "";
        //    return false;
        //}
        return true;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (CheckCaptcha())
            {
                objBE.Action = "R";
                objBE.User = txtUname.Text;
                DataTable dtLogin = objLogin.getLoginDetails(objBE, ConnKey);
                if (dtLogin.Rows.Count > 0)
                {
                    string password = dtLogin.Rows[0]["Password"].ToString();
                    string StateCode = dtLogin.Rows[0]["StateCode"].ToString();
                    string Statename = dtLogin.Rows[0]["State"].ToString();
                    string DistCode = dtLogin.Rows[0]["DistCode"].ToString();
                    string MandCode = dtLogin.Rows[0]["MandCode"].ToString();
                    string Department = dtLogin.Rows[0]["Department"].ToString();
                    string Category = dtLogin.Rows[0]["Category"].ToString();
                    string district = dtLogin.Rows[0]["DistName"].ToString();
                    string mandal = dtLogin.Rows[0]["MandName"].ToString();
                    string roleNm = dtLogin.Rows[0]["RoleName"].ToString();
                    string role = dtLogin.Rows[0]["role"].ToString();
                    string Code = dtLogin.Rows[0]["code"].ToString();
                    string Name = dtLogin.Rows[0]["Name"].ToString();
                    string ZoneName = dtLogin.Rows[0]["ZoneName"].ToString();
                    string ZoneCode = dtLogin.Rows[0]["ZoneCode"].ToString();

                    string LabName = dtLogin.Rows[0]["LabName"].ToString();
                    string LabCode = dtLogin.Rows[0]["Lab_Code"].ToString();
                    Session["UserId"] = dtLogin.Rows[0]["Sno"].ToString();

                    string myval = ShaEncrypt(ViewState["KeyGenerator"].ToString());
                    string value = ShaEncrypt(password.ToLower() + myval.ToLower());

                    if (txtPwdHash.Value == value.ToLower())
                    {
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;
                        Response.ClearContent();
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                        Session["ConnKey"] = ConnKey;
                        Session["Role"] = roleNm;
                        Session["RoleID"] = role;
                        Session["UsrName"] = txtUname.Text;
                        Session["StateCode"] = StateCode;
                        Session["Statename"] = Statename;
                        Session["Department"] = Department;
                        captch.Text = "";

                        Session["LoginSno"] = objLogin.insertUserLoginStatus(txtUname.Text, DateTime.Now, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login Successful", ConnKey);

                        //if (password.ToUpper() == "6B4C8CBCB6B66F050C12D6A0203C58A8BC6D36E5A8C28B74111681F7AECE378A")
                        //{
                        //    Session["Role"] = roleNm;
                        //    Session["UsrName"] = txtUname.Text;
                        //    Session["StateCode"] = StateCode;
                        //    Session["distCode"] = DistCode;
                        //    Session["mandcode"] = MandCode;
                        //    Session["district"] = district;
                        //    Session["mandal"] = mandal;
                        //    Response.Redirect("ChangePWD.aspx", false);
                        //}

                        //else 
                        if (dtLogin.Rows[0]["Role"].ToString() == "7")
                        {
                            Response.Redirect("~/Agri/Admin/DashBoard.aspx", false);
                        }
                        else if (dtLogin.Rows[0]["Role"].ToString() == "8")
                        {
                            Session["DistCode"] = DistCode;
                            Session["DistName"] = district;
                            Session["Mandcode"] = MandCode;
                            Session["MandName"] = mandal;
                            Session["AOName"] = Name;
                            Session["AO_Code"] = Code;
                            Response.Redirect("~/Agri/AO/SampleRegistration_AO.aspx", false);
                        }
                        else if (dtLogin.Rows[0]["Role"].ToString() == "11")
                        {
                            Session["CodingCenter_Code"] = Code;
                            Response.Redirect("~/Agri/CodingCenter/GenerateStickers.aspx", false);
                        }



                        else if (dtLogin.Rows[0]["Role"].ToString() == "14")
                        {
                            Session["CCOAO"] = Code;
                            Session["Department"] = Department;
                            Session["Category"] = Category;
                            Response.Redirect("~/Agri/Fertilizer/CCOAO/Ack.aspx", false);
                        }
                        else if (dtLogin.Rows[0]["Role"].ToString() == "15")
                        {
                            Session["CCDDA_Code"] = Code;
                            Session["Department"] = Department;
                            Session["Category"] = Category;
                            Response.Redirect("~/Agri/Fertilizer/CCODDA/GenerateCode.aspx", false);
                        }

                        else if (dtLogin.Rows[0]["Role"].ToString() == "16")
                        {
                            Session["Department"] = Department;
                            Session["Category"] = Category;
                            Session["Labcode"]=LabCode;
                            Session["COLabcode"] = Code;
                            Response.Redirect("~/Agri/Fertilizer/LabOfficer/EquipmentDetails.aspx", false);
                        }
                        else
                        {
                            ViewState["KeyGenerator"] = Guid.NewGuid().ToString("N").Substring(0, 16);
                            objCommon.ShowAlertMessage("Invalid Username & Password");
                        }
                    }
                    else
                    {
                        captch.Text = "";
                        ViewState["KeyGenerator"] = Guid.NewGuid().ToString("N").Substring(0, 16);
                        objCommon.ShowAlertMessage("Please Enter Valid user name");
                    }
                }
            }
            else
            {
                captch.Text = "";
                ViewState["KeyGenerator"] = Guid.NewGuid().ToString("N").Substring(0, 16);
                lblmsg.Text = "The characters you entered didn't match.Please try again";
            }

        }

        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }

    protected void btnImgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        captch.Text = "";
        getCaptchaImage();
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