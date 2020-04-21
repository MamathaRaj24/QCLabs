using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;
using Microsoft.Reporting.WebForms;

public partial class Analyst_PrintForm13 : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs ObjCommon = new CommonFuncs();
    Validate objValidate = new Validate();
    DataTable dt;
    
    string con, user, state, zone;

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    if ((Request.ServerVariables["HTTP_REFERER"] == null) || (Request.ServerVariables["HTTP_REFERER"] == ""))
        //    {
        //        Response.Redirect("~/Error.aspx");
        //    }
        //    else
        //    {
        //        string http_ref = Request.ServerVariables["HTTP_REFERER"].Trim();
        //        string http_hos = Request.ServerVariables["HTTP_HOST"].Trim();
        //        int len = http_hos.Length;
        //        if (http_ref.IndexOf(http_hos, 0) < 0)
        //        {
        //            Response.Redirect("~/Error.aspx");
        //        }
        //    }
        //    PrevBrowCache.enforceNoCache();

        //    if (Session["UsrName"] == null && Session["RoleID"].ToString() != "1")
        //    {
        //        Response.Redirect("~/Error.aspx");
        //    }
        //    else
        //    {
        //        con = Session["ConnKey"].ToString();
        //        lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        //        lblUser.Text = Session["Role"].ToString() + " -  " + Session["AnalystName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
        //    }

        //    if (!IsPostBack)
        //    {
        //        lblUser.Text = user;
        //        lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        //    }

        //}
        //catch (Exception ex)
        //{
        //    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
        //    Response.Redirect("~/Error.aspx");
        //}
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            dt = new DataTable();
            objBE.SampleID = txtSampleId.Text;
            dt = ObjDL.PrintForm13(objBE, con);
            if (dt.Rows.Count > 0)
            {
                Rpt_PrintForm13.LocalReport.DataSources.Clear();
                Rpt_PrintForm13.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                Rpt_PrintForm13.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_Form13.rdlc");
                Rpt_PrintForm13.LocalReport.Refresh();
                PrintAnalyst.Visible = true;
            }
            else
            {
                ObjCommon.ShowAlertMessage("No Data Found");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
}