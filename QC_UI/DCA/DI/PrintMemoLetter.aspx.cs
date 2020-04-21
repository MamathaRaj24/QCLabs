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

public partial class Inspector_PrintMemoLetter : System.Web.UI.Page
{
   
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objR = new Registration_BE();
    DataTable dt;
    string con, user, state, dept, dicode;

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
                Response.Redirect("~/Error.aspx");
            }
        }
        PrevBrowCache.enforceNoCache();
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "1")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            dicode = Session["DI_AO_Code"].ToString();

            if (!IsPostBack)
            {
                random();
                lblUser.Text = Session["Role"].ToString() + " -  " + Session["DIZoneNm"].ToString() + " -  " + Session["DIName"].ToString();
                lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("~/Error.aspx");
        }
    }
    public void BindGrid()
    {
        try
        {
            objR.Action = "PM";
            objR.DI_AO_Code = dicode;
            dt = Objrdl.SampleRegistrationDI(objR, con);
            if (dt.Rows.Count > 0)
            {
                GvPrintForm.DataSource = dt;
                GvPrintForm.DataBind();
            }
            else
            {
                GvPrintForm.DataSource = null;
                GvPrintForm.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }

    }
    protected void GvPrintForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Form17")
        {
            try
            {
                DataTable dt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objR.MemoId = ((Label)gvrow.FindControl("lblRegId")).Text;

                objR.Action = "PRINT";
                dt = Objrdl.SampleRegistrationDI(objR, con);
                if (dt.Rows.Count > 0)
                {
                    Rpt_PrintForm17.LocalReport.DataSources.Clear();
                    Rpt_PrintForm17.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    Rpt_PrintForm17.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_MemoGeneration.rdlc");
                    Rpt_PrintForm17.LocalReport.Refresh();
              
                }
                else
                {
                    cf.ShowAlertMessage("No Data Found");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                Response.Redirect("~/Error.aspx");
            }
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