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

public partial class DCA_CodingOfficer_SearchSamples_CO : System.Web.UI.Page
{
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objR = new Registration_BE();
    DataTable dt;
    string con, user, state, Department, Analystcode, ssoCode;

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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "3")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            ssoCode = Session["ssoCode"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            lblUser.Text = Session["Role"].ToString() + " >> " + Session["ssoName"].ToString();

            if (!IsPostBack)
            {
                random();
                try
                {

                    span1.Visible = false;
                    Span2.Visible = false;
                    span3.Visible = false;
                    DivViewData.Visible = false;
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
    //BindZone
    protected void BindZone()
    {
        objR.Action = "DI";
        objR.dept = Department;
        dt = Objrdl.SampleRegister(objR, con);
        cf.BindDropDownLists(ddlDiName, dt, "NAME", "Zone_Cd", "0");
    }
    protected void BindMemos()
    {
        dt = new DataTable();
        objR.Action = "CMEMO";
        // objR.dept = Department;
        dt = Objrdl.SampleRegister(objR, con);
        cf.BindDropDownLists(ddlmemo, dt, "Memo_ID", "Memo_ID", "Select");
    }
    protected void BindBatch()
    {

        objR.Action = "CBATCH";
        dt = Objrdl.SampleRegister(objR, con);
        cf.BindDropDownLists(ddlbatch, dt, "BatchNo", "BatchNo", "Select");
    }
    protected void GetDetails()
    {
        check();
        try
        {
            dt = new DataTable();
            objR.Action = "COGRID";
            dt = Objrdl.searchsample(objR, con);
            if (dt.Rows.Count > 0)
            {
                pnlgird.Visible = true;
                GvCourier.DataSource = dt;
                GvCourier.DataBind();
            }
            else
            {

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
        objR.AnalystId = ssoCode;
        if (rdbMrgSta.SelectedValue == "1")
        {
            span1.Visible = true;
            BindZone();
            Span2.Visible = false;
            span3.Visible = false;

        }

        if (rdbMrgSta.SelectedValue == "2")
        {
            Span2.Visible = true;
            BindBatch();
            span3.Visible = false;
            span1.Visible = false;
        }

        if (rdbMrgSta.SelectedValue == "3")
        {
            span3.Visible = true;
            BindMemos();
            span1.Visible = false;
            Span2.Visible = false;
        }

        else
        {

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
                string memoid = ((Label)gvrow.FindControl("lblmemoid")).Text.ToUpper();
                lblsamplid.Text = ((Label)gvrow.FindControl("lblSampleId")).Text.ToUpper();
                objR.MemoId = memoid;
                objR.Action = "SERCHLABRDLC";
                dt = Objrdl.PrintForm1718(objR, con);
                if (dt.Rows.Count > 0)
                {
                    Div1.Visible = true;
                    Rptform13analyst.LocalReport.DataSources.Clear();
                    Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    Rptform13analyst.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/CoLabrpt.rdlc");
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
        GetDetails();
    }


    protected void ddlDiName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objR.AnalystId = ddlDiName.SelectedValue.ToString();
        GetDetails();

    }
    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objR.SampleName = ddlbatch.SelectedValue.ToString();
        GetDetails();
    }
    protected void ddlmemo_SelectedIndexChanged(object sender, EventArgs e)
    {
        objR.AckId = ddlmemo.SelectedValue.ToString();
        GetDetails();
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
