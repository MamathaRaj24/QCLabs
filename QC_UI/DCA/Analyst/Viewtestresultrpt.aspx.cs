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

public partial class DCA_Analyst_Viewtestresultrpt : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, Analystcode, Uocode;

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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "5"  ||  Session["RoleID"].ToString() == "6")
         {
            con = Session["ConnKey"].ToString();
            //lblUser.Text = Session["Role"].ToString() + " -  " + Session["AnalystName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
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
                BindGrid();
                DivViewData.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BindGrid()
    {
        try
        {
            dt = new DataTable();
            objBE.Action = "ANVIEW";
            if (Session["RoleID"].ToString() == "5")
            {
                objBE.AnalystId = Analystcode;
            }
            if (Session["RoleID"].ToString() == "6")
            {
                objBE.AnalystId = Uocode;
            } 
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GVTestResult.DataSource = dt;
                GVTestResult.DataBind();
            }
            else
            {
                GVTestResult.DataSource = null;
                GVTestResult.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void GVTestResult_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewResult")
        {
            try
            {
                DataTable dtt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsamplid.Text = ((Label)gvrow.FindControl("lblsampleid")).Text.ToUpper();
                objBE.SampleID = lblsamplid.Text;
                objBE.Action = "RDLC-JAFORM";
                dt = ObjDL.JAActionEdit(objBE, con);
                objBE.Action = "RDLC-JATEST";
                dtt = ObjDL.JAActionEdit(objBE, con);
                //if (((Label)gvrow.FindControl("lbltestresult")).Text == "YES4")
                //{
                    if (dt.Rows.Count > 0)
                    {
                        Rptform13analyst.LocalReport.DataSources.Clear();
                        Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtt));
                        Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
                        Rptform13analyst.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_TestAnalyst.rdlc");
                       // Rptform13analyst.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_Form13UO.rdlc");
                        Rptform13analyst.LocalReport.Refresh();
                        DivViewData.Visible = true;
                    }
                    else
                    {
                        cf.ShowAlertMessage("No Data Found");
                        DivViewData.Visible = false;
                    }
               

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                cf.ShowAlertMessage(ex.ToString());
            }
        }

    }
    protected void GVTestResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVTestResult.PageIndex = e.NewPageIndex;
        BindGrid();
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