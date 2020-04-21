using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using QC_DL;
using System.Data;
using QC_BE;
public partial class DCA_UnitOfficer_PrintForm34 : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, Uocode;
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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "4")
        {
            con = Session["ConnKey"].ToString();

            Uocode = Session["UnitOfficerCode"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["UnitOfficerName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                random();
                try
                {
                    BindGrid();
                    DivViewData.Visible = false;
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
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BindGrid()
    {
        objBE.Action = "FREEZEUOCT";
        objBE.UOId = Uocode;
        objBE.flag = "1";
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            GvPrintForm.DataSource = dt;
            GvPrintForm.DataBind();
        }
        else
        {
            GvPrintForm.DataSource = null;
            GvPrintForm.DataBind();
            cf.ShowAlertMessage("No data");
        }

    }
    protected void GvPrintForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Form34")
            {
                DataTable dtt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objBE.SampleID = ((Label)gvrow.FindControl("lblsampleid")).Text.ToUpper();
                lblsamplid.Text = ((Label)gvrow.FindControl("lblsampleid")).Text;
                objBE.flag = "1";
                //objBE.Action = "RDLC-JA";
                objBE.Action = "form13_UO";
                dt = ObjDL.JAActionEdit(objBE, con);
                objBE.Action = "fmtest_UO";
                dtt = ObjDL.JAActionEdit(objBE, con);

                if (((Label)gvrow.FindControl("lbltestresult")).Text == "YES4" && ((Label)gvrow.FindControl("lblstatus")).Text == "STANDARD")
                {
                    if (dt.Rows.Count > 0)
                    {
                        Rpt_PrintForm13.LocalReport.DataSources.Clear();

                        Rpt_PrintForm13.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        Rpt_PrintForm13.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dtt));
                        Rpt_PrintForm13.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_Form34UO_s.rdlc");
                        Rpt_PrintForm13.LocalReport.Refresh();
                        DivViewData.Visible = true;
                    }
                    else
                    {
                        DivViewData.Visible = false;
                        cf.ShowAlertMessage("No Data Found");
                    }
                }
                if (((Label)gvrow.FindControl("lbltestresult")).Text == "YES4" && ((Label)gvrow.FindControl("lblstatus")).Text == "NOT OF STANDARD")
                {
                    if (dt.Rows.Count > 0)
                    {
                        Rpt_PrintForm13.LocalReport.DataSources.Clear();

                        Rpt_PrintForm13.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtt));
                        Rpt_PrintForm13.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
                        Rpt_PrintForm13.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_Form34UO_ns.rdlc");
                        Rpt_PrintForm13.LocalReport.Refresh();
                        DivViewData.Visible = true;
                    }
                    else
                    {
                        DivViewData.Visible = false;
                        cf.ShowAlertMessage("No Data Found");
                    }
                }


            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void GvPrintForm_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvPrintForm.PageIndex = e.NewPageIndex;
        BindGrid();
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

    protected void Rpt_PrintForm13_Load(object sender, EventArgs e)
    {

        string exportOption = "Excel";
        string exportOption1 = "Word";
        RenderingExtension extension = Rpt_PrintForm13.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        RenderingExtension extensions = Rpt_PrintForm13.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));
        if (extension != null)
        {
            System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            fieldInfo.SetValue(extension, false);
        }
        if (extensions != null)
        {
            System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            fieldInfo.SetValue(extensions, false);
        }
    }

}