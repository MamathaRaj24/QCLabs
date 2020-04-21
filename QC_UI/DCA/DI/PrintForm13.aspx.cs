using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DI_PrintForm13 : System.Web.UI.Page
{


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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "1")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            dicode = Session["DI_AO_Code"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["DIZoneNm"].ToString() + " -  " + Session["DIName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
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
            Response.Redirect("../Error.aspx");
        }
    }

    protected void BindGrid()
    {
        objBE.Action = "Diview";
        objBE.AnalystId = dicode;
        dt = Objrdl.JAActionEdit(objBE, con);
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

    protected void BindReport()
    {
        try
        {
            objBE.Action = "RDLC";
            objBE.SampleID = lblsamplid.Text.Trim();
            DataTable dtt = new DataTable();
            dtt = Objrdl.JAActionEdit(objBE, con); 
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect(ex.Message.ToString());
        }
    }

    protected void GvPrintForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewResult")
        {
            try
            {
                DataTable dtt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsamplid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                objBE.SampleID = lblsamplid.Text;
                objBE.flag = "1";
                objBE.Action = "form13_UO";
                dt = Objrdl.JAActionEdit(objBE, con);
                objBE.Action = "fmtest_UO";
                dtt = Objrdl.JAActionEdit(objBE, con);
                if (((Label)gvrow.FindControl("lbltestresult")).Text == "YES" && ((Label)gvrow.FindControl("lblstatus")).Text == "STANDARD")
                {
                    if (dt.Rows.Count > 0)
                    {
                        Rptform13analyst.LocalReport.DataSources.Clear(); 
                        Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dtt));
                        Rptform13analyst.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_Form13UO_S.rdlc");
                        Rptform13analyst.LocalReport.Refresh();
                        DivViewData.Visible = true;
                    }
                    else
                    {
                        DivViewData.Visible = false;
                        cf.ShowAlertMessage("No Data Found");
                    }
                }
                if (((Label)gvrow.FindControl("lbltestresult")).Text == "YES" && ((Label)gvrow.FindControl("lblstatus")).Text == "NOT OF STANDARD")
                {
                    if (dt.Rows.Count > 0)
                    {
                        Rptform13analyst.LocalReport.DataSources.Clear(); 
                        Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                        Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dtt));
                        Rptform13analyst.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_Form13UO_ns.rdlc");
                        Rptform13analyst.LocalReport.Refresh();
                        DivViewData.Visible = true;
                    }
                    else
                    {
                        DivViewData.Visible = false;
                        cf.ShowAlertMessage("No Data Found");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                cf.ShowAlertMessage(ex.ToString());
            }
        }
    }
    protected void Rptform13analyst_Load(object sender, EventArgs e)
    {
        string exportOption = "Excel";
        string exportOption1 = "Word";
        RenderingExtension extension = Rptform13analyst.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        RenderingExtension extensions = Rptform13analyst.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));
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