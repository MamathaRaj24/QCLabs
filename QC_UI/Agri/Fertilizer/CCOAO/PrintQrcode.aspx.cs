using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;
using System.Web.Security;
using KeepAutomation.Barcode;
using KeepAutomation.Barcode.Bean;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Net.Mime;
using System.IO;
public partial class Agri_Fertilizer_CCOAO_PrintQrcode : System.Web.UI.Page
{
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
    AgriDL Objrdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objBE = new AgriBE();
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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "14")
        {
            state = Session["StateCode"].ToString();
            user = Session["CCOAO"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            


            if (!IsPostBack)
            {
                random();
                try
                {
                  
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["UsrName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindSamples();
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
    protected void BindSamples()
    {
        dt = new DataTable();
        objBE.dept = Department;
        objBE.UserId = user;
        objBE.Action = "SMPL";
        dt = Objrdl.GenerateSticker_AGRI(objBE, con);
        cf.BindDropDownLists(ddlsample, dt, "SampleID", "SampleID", "Select");
    }
    protected void btngeneratecode_Click(object sender, EventArgs e)
    {
        try
        {
            objBE.SampleID = ddlsample.SelectedValue;
            objBE.Action = "RPSTICKER"; 
            dt = Objrdl.GenerateSticker_AGRI(objBE, con);

            if (dt.Rows.Count > 0)
            {
                Session["Sampleid"] = ddlsample.SelectedValue; 
                btnImgprint.Visible = true;
                Rpt_Sticker.LocalReport.DataSources.Clear();
                Rpt_Sticker.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                Rpt_Sticker.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
                Rpt_Sticker.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Stiker_agri.rdlc");
                Rpt_Sticker.LocalReport.Refresh(); 
                Rpt_Sticker.ShowPrintButton = true;
            }
            //BindSamples();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }
   
    protected void btnImgprint_Click(object sender, ImageClickEventArgs e)
    {
        Session["ReportName"] = "GenarateStikker";
        Session["Sampleid"] = Session["Sampleid"].ToString();
        Session["con"] = con;
        string url = "Printform.aspx";
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.open('");
        sb.Append(url);
        sb.Append("','_blank');");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),
                     "script", sb.ToString());
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
            //cookie_value = System.Web.HttpContext.Current.Request.Cookies["ASPFIXATION2"].Value;
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