using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DCA_UnitOfficer_Dashboard : System.Web.UI.Page
{

    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objR = new Registration_BE();
    DataTable dt;
    string con, user, state, Department, uocode, labcode;


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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "4")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            uocode = Session["UnitOfficerCode"].ToString();
            labcode = Session["Labcode"].ToString();
            if (!IsPostBack)
            {
                random();
                try
                {

                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["UnitOfficerName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    GetDetails();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    cf.ShowAlertMessage(ex.ToString());
                }
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
            objR.AnalystId = labcode;
            objR.Action = "UO";
            objR.COAction = "RSCVD";
            dt = Objrdl.AnalystDashBoard(objR, con);
            lblsamplesReg.Text = dt.Rows[0][0].ToString();


            objR.COAction = "ANLYST";
            dt = Objrdl.AnalystDashBoard(objR, con);
            lblAccepted.Text = dt.Rows[0][0].ToString();


            objR.COAction = "TEST";
            objR.AnalystId = labcode;
            dt = Objrdl.AnalystDashBoard(objR, con); ;
            lblRejected.Text = dt.Rows[0][0].ToString();

          
            objR.COAction = "S";
            objR.AnalystId = labcode;
            dt = Objrdl.AnalystDashBoard(objR, con);
            lblSamTested.Text = dt.Rows[0][0].ToString();

            objR.COAction = "NS";
            objR.AnalystId = labcode;
            dt = Objrdl.AnalystDashBoard(objR, con);
            lblnotstanderd.Text = dt.Rows[0][0].ToString();

           
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.Message.ToString());
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