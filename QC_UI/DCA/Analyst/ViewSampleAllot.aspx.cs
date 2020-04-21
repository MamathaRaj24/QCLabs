using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;

public partial class DCA_Analyst_ViewSampleAllot : System.Web.UI.Page
{
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objR = new Registration_BE();
    DataTable dt;
    string con, user, state, Department, Analystcode, Uocode;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.ServerVariables["HTTP_REFERER"] == null) || (Request.ServerVariables["HTTP_REFERER"] == ""))
            Response.Redirect("~/Error.aspx");
        else
        {
            string http_ref = Request.ServerVariables["HTTP_REFERER"].Trim();
            string http_hos = Request.ServerVariables["HTTP_HOST"].Trim();
            int len = http_hos.Length;
            if (http_ref.IndexOf(http_hos, 0) < 0)
                Response.Redirect("../Error.aspx");
        }
        PrevBrowCache.enforceNoCache();
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "5" || Session["RoleID"].ToString().Trim() == "6")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (Session["RoleID"].ToString().Trim() == "5")
            {
                lblUser.Text = Session["Role"].ToString() + " -  " + Session["AnalystName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
                Analystcode = Session["AnalystCode"].ToString();
            }
            if (Session["RoleID"].ToString().Trim() == "6")
            {
                lblUser.Text = Session["Role"].ToString() + " -  " + Session["JsoName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
                Uocode = Session["Jsocode"].ToString();
            }
            if (!IsPostBack)
            {
                random();
                span4.Visible = false;
            }
        }
        else
            Response.Redirect("../Error.aspx");
    }
    protected void GetDetails()
    {
        check();
        try
        {
            dt = new DataTable();
            if (Session["RoleID"].ToString() == "5")
            {
                objR.AnalystId = Analystcode;
            }
            if (Session["RoleID"].ToString() == "6")
            {
                objR.AnalystId = Uocode;
            }
            if (rdbMrgSta.SelectedValue == "1")
            {
                objR.Action = "Analyst";
                objR.COAction = "TOP10";
            }
            else
            {
                objR.Action = "Analyst";
                objR.COAction = "BETW";
            }
            
            dt = Objrdl.AnalystDashBoard(objR, con);
            if (dt.Rows.Count > 0)
            {
                //btnFreeze.Visible = true;
                GvCourier.DataSource = dt;
                GvCourier.DataBind();
            }
            else
            {
               // btnFreeze.Visible = false;
                GvCourier.DataSource = null;
                GvCourier.DataBind();
                cf.ShowAlertMessage("Data is Not Found");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.Message.ToString());
        }
    }
  
    protected void rdbMrgSta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMrgSta.SelectedValue == "2")
        {
            span4.Visible = true;
           GvCourier.DataSource = null;
           GvCourier.DataBind();
        }
        else
        {
            span4.Visible = false;
            GetDetails();
        }

    }
    protected void BtnTR_Click(object sender, EventArgs e)
    {
        objR.Entry_Dt = cf.Texttodateconverter(txtfromdt.Text.Trim());
        objR.ExpiryDate =cf.Texttodateconverter(txtToDt.Text.Trim());      

         GetDetails();
        //txtfromdt.Text = "";
        //txtToDt.Text = "";
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
            Session["hf"] = num;
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
            cookie_value = Session["hf"].ToString();
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