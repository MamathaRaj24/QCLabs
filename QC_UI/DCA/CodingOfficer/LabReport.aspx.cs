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

public partial class DCA_CodingOfficer_LabReport : System.Web.UI.Page
{
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "3")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            dicode = Session["ssoCode"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString() + " >> " + Session["ssoName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindLabs();
                    Bindsamplecategory();
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
    protected void BindLabs()
    {
        objMBE.Action = "R";
        objMBE.statecode = Session["StateCode"].ToString();
        objMBE.Dept = Department;
        dt = ObjDL.Lab_IUDR(objMBE, con);
        if (dt.Rows.Count > 0)
            cf.BindDropDownLists(ddlunit, dt, "LabName", "LabCode", "Select");
        else
            cf.ShowAlertMessage("No Labs Added");
    }
    protected void Bindsamplecategory()
    {
        check();
        dt = new DataTable();
        objMBE.Dept = Session["Department"].ToString();
        objMBE.Action = "R";
        dt = ObjDL.CategoryIUDR(objMBE, con);
        if (dt.Rows.Count > 0)
            cf.BindDropDownLists(ddlsamplecategory, dt, "category_name", "category_id", "Select");
    }
    protected void txtdate_TextChanged(object sender, EventArgs e)
    {
        string date = txtdate.Text;
        string month, year;
        month = date.Substring(0, 2);
        if (month.Substring(0, 1) == "0")
            lblmonth.Text = month.Substring(1, 1);
        else
            lblmonth.Text = month;
        year = date.Substring(3, 4);       
        lblyear.Text = year;
    }

    protected void Btnget_Click(object sender, EventArgs e)
    {
        if (Validate_Save())
        {
            try
            {
                DataTable dtt = new DataTable();
                if (ddlunit.SelectedIndex == 0)
                    objBE.labID = null;
                else
                    objBE.labID = ddlunit.SelectedValue;
                if(ddlsamplecategory.SelectedIndex==0)
                    objBE.SampleCategory = null;
                else
                    objBE.SampleCategory = ddlsamplecategory.SelectedValue;
                objBE.ManufacturingDate = lblmonth.Text;
                objBE.ExpiryDate = lblyear.Text;
                objBE.Action = "LABRDLC";
                dt = Objrdl.PrintForm1718(objBE, con);

                if (dt.Rows.Count > 0)
                {
                    Rptform13analyst.LocalReport.DataSources.Clear();
                    Rptform13analyst.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    Rptform13analyst.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/CoLabrpt.rdlc");
                    Rptform13analyst.LocalReport.Refresh();
                    DivViewData.Visible = true;
                }
                else
                {
                    DivViewData.Visible = false;
                    cf.ShowAlertMessage("No Data Found");
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
    protected bool Validate_Save()
    {
        //if (ddlunit.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Select Lab");
        //    ddlunit.Focus();
        //    return false;
        //}
        //if (ddlsamplecategory.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Select Sample Category");
        //    ddlsamplecategory.Focus();
        //    return false;
        //}

        if (txtdate.Text == "")
        {
            cf.ShowAlertMessage("Select Month");
            txtdate.Focus();
            return false;
        }
        return true;
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