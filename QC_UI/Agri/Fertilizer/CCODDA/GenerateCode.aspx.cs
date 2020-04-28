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

public partial class Agri_Fertilizer_CCODDA_GenerateCode : System.Web.UI.Page
{
     
    AgriDL Objrdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objBE = new AgriBE();
    DataTable dt;
    string con, user, state, Department, dicode,Category; 

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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "15")
        {
            state = Session["StateCode"].ToString();
            user = Session["CCDDA_Code"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
         
            Category = Session["Category"].ToString();

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
        objBE.SampleCategory = Category;
        objBE.Action = "MEMO";
        dt = Objrdl.GenerateSticker_AGRI(objBE, con);
        cf.BindDropDownLists(ddlmemo, dt, "Memo_ID", "Memo_ID", "Select");
    }
    public string GetImage(object img)
    {
        if (img != DBNull.Value)
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }
        else
        {
            return "";
        }
    }
    protected void BindmemoSamples()
    {
        dt = new DataTable();
        objBE.dept = Department;
        objBE.SampleCategory = Category;
        objBE.MemoId = ddlmemo.SelectedValue;
        objBE.Action = "MEMO_SAMPLE";
        dt = Objrdl.GenerateSticker_AGRI(objBE, con);
        Session["dtsamples"] = dt; 
    }
    protected void btngeneratecode_Click(object sender, EventArgs e)
    {
        BindmemoSamples();
        GenerateCode();
    }
    protected void GenerateCode()
    {
        try
        {
            DataTable dtt = new DataTable();
            DataTable dt1 = (DataTable)Session["dtsamples"];
            dtt.Columns.Add("SampleID", typeof(string));
            dtt.Columns.Add("QRCode", typeof(byte[]));
            int i = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                dtt.Rows.Add();
                dtt.Rows[i]["SampleID"] =dt1.Rows[i]["SampleID"].ToString();
                dtt.Rows[i]["QRCode"] = GenerateQRCode(dt1.Rows[i]["SampleID"].ToString());
                i++;
            }
         
            objBE.UserId = user;
            objBE.Action = "STICKERAG";
            objBE.TVpstiker = dtt;
           

           dt = Objrdl.GenerateSticker_AGRI(objBE, con);

            if (dt.Rows.Count > 0)
            {
                Gvqr.DataSource = dt;
                Gvqr.DataBind();
                ddlmemo.Enabled = false; 
            }
           // BindmemoSamples();
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