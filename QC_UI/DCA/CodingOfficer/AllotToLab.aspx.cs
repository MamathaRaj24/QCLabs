using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class CodingOfficer_AllotToLab : System.Web.UI.Page
{
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objBE = new Registration_BE();
    Validate objValidate = new Validate();
    DataTable dt;

    string con, user, state, Department, dicode;

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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "3")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            dicode = Session["ssoCode"].ToString();
                      
            if (!IsPostBack)
            {
                random();
                try
                {
                    lblUser.Text = Session["Role"].ToString() + " >> " + Session["ssoName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindSamples();
                    BindLabs();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    cf.ShowAlertMessage(ex.ToString());
                }
            }
        }
        else
        {
            Response.Redirect("../Error.aspx");
        }
    }
    protected void BindSamples()
    {
        dt = new DataTable();
        objBE.Action = "ASMPL";
        objBE.dept = Department;
        dt = Objrdl.GenerateSticker(objBE, con);
        cf.BindDropDownLists(ddlsample, dt, "SampleID", "SampleID", "Select");
    }
    protected void BindLabs()
    {
        objMBE.Action = "R";
        objMBE.statecode = Session["StateCode"].ToString();
        objMBE.Dept = Session["Department"].ToString();
        dt = ObjDL.Lab_IUDR(objMBE, con);
        if (dt.Rows.Count > 0)
            cf.BindDropDownLists(ddlLabs, dt, "LabName", "LabCode", "Select");
        else
            cf.ShowAlertMessage("No Labs Added");
    }
    protected void btnAllot_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            dt = new DataTable();
            objBE.SampleID = ddlsample.SelectedValue;
            objBE.labID = ddlLabs.SelectedValue;
            objBE.Action = "ALLOT";
            dt = Objrdl.GenerateSticker(objBE, con);

            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage(dt.Rows[0][0].ToString());
            else
            {
                cf.ShowAlertMessage(ddlsample.SelectedItem.Text + " is allotted to Lab  " + ddlLabs.SelectedItem.Text);
                BindSamples();
                ddlLabs.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
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
            Response.Redirect("~/Error.aspx");
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
                HttpContext.Current.Response.Redirect("~/Error.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx");
        }
    }
}