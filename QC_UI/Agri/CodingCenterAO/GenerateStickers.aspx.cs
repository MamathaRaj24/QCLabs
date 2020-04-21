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


public partial class Agri_CodingCenter_GenerateStickers : System.Web.UI.Page
{
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE  objBE = new Registration_BE ();
    DataTable dt;
    string con, user, state, Department, code;
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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "11")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            code = Session["CodingCenter_Code"].ToString() ;


            if (!IsPostBack)
            {
                random();
                try
                {
                    lblUser.Text = Session["Role"].ToString() + " >> " + Session["ssoName"].ToString();
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
        objBE.Action = "SMPL";
        dt = Objrdl.GenerateSticker(objBE, con);
        cf.BindDropDownLists(ddlsample, dt, "SampleID", "SampleID", "Select");
    }
    protected void BtnGen_Click(object sender, EventArgs e)
    {

        try
        {
            dt = new DataTable();
            objBE.dept = Department;
            objBE.Action = "SMPL";
            dt = Objrdl.GenerateSticker(objBE, con);
            cf.BindDropDownLists(ddlsample, dt, "SampleID", "SampleID", "Select");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
        GenerateCode();
    }
    protected void GenerateCode()
    {
        try
        {
            dt = new DataTable();
            objBE.SampleID = ddlsample.SelectedValue;
            objBE.Action = "STICKER";
            objBE.qrcode = GenerateQRCode(ddlsample.SelectedValue);
            dt = Objrdl.GenerateSticker(objBE, con);

            if (dt.Rows.Count > 0)
            {
                Session["Sampleid"] = ddlsample.SelectedValue;            
                btnImgprint.Visible = true;
                Rpt_Sticker.LocalReport.DataSources.Clear();
                Rpt_Sticker.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                Rpt_Sticker.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Sticker.rdlc");
                Rpt_Sticker.LocalReport.Refresh();
                Rpt_Sticker.ShowPrintButton = true;
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected Byte[] GenerateQRCode(string sampleid)
    {
        BarCode qrcode = new BarCode();
        qrcode.Symbology = KeepAutomation.Barcode.Symbology.QRCode;
        qrcode.QRCodeDataMode = QRCodeDataMode.Auto;
        qrcode.CodeToEncode = sampleid;
        qrcode.BarcodeUnit = BarcodeUnit.Pixel;
        qrcode.DPI = 72;
        qrcode.X = 3;
        qrcode.Y = 3;
        qrcode.LeftMargin = 12;
        qrcode.RightMargin = 12;
        qrcode.TopMargin = 12;
        qrcode.BottomMargin = 12;
        qrcode.Orientation = KeepAutomation.Barcode.Orientation.Degree0;
        qrcode.QRCodeVersion = QRCodeVersion.V5;
        qrcode.QRCodeECL = QRCodeECL.H;
        qrcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
        Byte[] bytes = qrcode.generateBarcodeToByteArray();
        return bytes;
    }


    protected void btnImgprint_Click1(object sender, ImageClickEventArgs e)
    {
        Session["ReportName"] = "GenarateStikker";
        Session["Sampleid"] = ddlsample.SelectedValue;
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
    protected void btngeneratecode_Click(object sender, EventArgs e)
    {
        GenerateCode();
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