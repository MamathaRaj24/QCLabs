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

public partial class DCA_Analyst_SearchSamples : System.Web.UI.Page
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
                try
                {
                    span4.Visible = false;
                    span5.Visible = false;
                    span6.Visible = false;
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
            Response.Redirect("../Error.aspx");
    }
    protected void BindCategory()
    {
       
        objR.Action = "C";
        objR.SampleID = null;

        dt = Objrdl.searchsample(objR, con);
        cf.BindDropDownLists(ddlCategory, dt, "Category_Name", "Category_ID", "Select");
    }
    protected void BindSample()
    {
        
        objR.Action = "S";
        dt = Objrdl.searchsample(objR, con);
        cf.BindDropDownLists(ddlSampId, dt, "Sample_ID", "Sample_ID", "Select");
    }
    protected void BindDrug()
    {

        objR.Action = "T";
        // objR.SampleID = null;

        dt = Objrdl.searchsample(objR, con);
        cf.BindDropDownLists(ddlDrug, dt, "TradeName", "TradeName", "Select");
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
           // objR.AckId = ddlSampId.SelectedValue.ToString();

            objR.Action = "GRID";
            dt = Objrdl.searchsample(objR, con);
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
            span4.Visible = true;
            BindSample();
        }
        else
        {
            span4.Visible = false;
            GvCourier.DataSource = null;
            GvCourier.DataBind();
        }
        if (rdbMrgSta.SelectedValue == "2")
        {
            span5.Visible = true;
            BindCategory();
        }
        else
        {
            span5.Visible = false;
            GvCourier.DataSource = null;
            GvCourier.DataBind();
        }
        if (rdbMrgSta.SelectedValue == "3")
        {
            span6.Visible = true;
            BindDrug();
        }
        else
        {
            span6.Visible = false;
            GvCourier.DataSource = null;
            GvCourier.DataBind();
        }
        DivViewData.Visible = false;
    }
    protected void GvCourier_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ViewResult")
        {
            try
            {
                DataTable dtt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsamplid.Text = ((Label)gvrow.FindControl("lblSampleId")).Text.ToUpper();
                if (Session["RoleID"].ToString() == "5")
                {
                    objR.AnalystId = Analystcode;
                }
                if (Session["RoleID"].ToString() == "6")
                {
                    objR.AnalystId = Uocode;
                }
                objR.AckId = lblsamplid.Text;
                objR.Action = "RDLC-JAFORM";
                dt = Objrdl.searchsample(objR, con);
                objR.Action = "RDLC-JATEST";
                dtt = Objrdl.searchsample(objR, con);
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
    protected void GvCourier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvCourier.PageIndex = e.NewPageIndex;
        // GetDetails();
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
    protected void BtnTR_Click1(object sender, EventArgs e)
    {
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
            objR.AckId = ddlSampId.SelectedValue.ToString();

            GetDetails();
            DivViewData.Visible = false;
        }
        else
        {
            span4.Visible = false;
           
        }
        if (rdbMrgSta.SelectedValue == "2")
        {
            objR.Category = ddlCategory.SelectedValue.ToString();
            GetDetails();
            DivViewData.Visible = false;
           
        }
        else
        {
            span5.Visible = false;
        }
        if (rdbMrgSta.SelectedValue == "3")
        {
            objR.SampleName = ddlDrug.SelectedValue.ToString();
            GetDetails();
            DivViewData.Visible = false;
           
        }
        else
        {
            span6.Visible = false;
        }
 
    }
    protected void ddlSampId_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            objR.AckId = ddlSampId.SelectedValue.ToString();

            GetDetails();
            DivViewData.Visible = false;
        }
        else
        {
            span4.Visible = false;

        }
        DivViewData.Visible = false;
 
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMrgSta.SelectedValue == "2")
        {
            objR.Category = ddlCategory.SelectedValue.ToString();
            GetDetails();
            DivViewData.Visible = false;

        }
        else
        {
            span5.Visible = false;
        }
        DivViewData.Visible = false;
    }
    protected void ddlDrug_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMrgSta.SelectedValue == "3")
        {
            objR.SampleName = ddlDrug.SelectedValue.ToString();
            GetDetails();
            DivViewData.Visible = false;

        }
        else
        {
            span6.Visible = false;
        }
        DivViewData.Visible = false;
    }
}