using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;
using Microsoft.Reporting.WebForms;

public partial class AO_PrintCourier : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriDL Objrdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objR = new AgriBE();
    DataTable dt;
    string con, user, state, Department, Aocode;


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

        if (Session["UsrName"] == null && Session["RoleID"].ToString() != "1")
        {
            Response.Redirect("../Error.aspx");
        }

        else
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            Aocode = Session["AO_Code"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DistName"].ToString() + " -  " + Session["MandName"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        }
        if (!IsPostBack)
        {
            try
            {
                random();
                 
                BindGrid();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                cf.ShowAlertMessage(ex.ToString());
            }
        }

    }
    protected void BindGrid()
    {
        try
        {
            objR.Action = "R2";
            objR.UserId = Aocode;
            dt = Objrdl.MemoID(objR, con);
            GvCourier.DataSource = dt;
            GvCourier.DataBind();
            if (dt.Rows.Count > 0)
            {
                GvCourier.DataSource = dt;
                GvCourier.DataBind();
            }
            else
            {
                GvCourier.DataSource = null;
                GvCourier.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }

    }
    protected void GvCourier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            objR.UserId = Session["AO_Code"].ToString();
            objR.Action = "R";
            dt = Objrdl.CourierIUDR(objR, con);
            if (dt.Rows.Count > 0)
            {

                Rpt_PrintCourier.LocalReport.DataSources.Clear();
                Rpt_PrintCourier.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                Rpt_PrintCourier.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_PrintCourier.rdlc");
                Rpt_PrintCourier.LocalReport.Refresh();
                Rpt_PrintCourier.Visible = true;

            }
            else
            {
                cf.ShowAlertMessage("No Report Data Found");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
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