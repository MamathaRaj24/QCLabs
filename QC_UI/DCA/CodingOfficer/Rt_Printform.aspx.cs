using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_BE;
using QC_DL;
using System.Data;
using System.Web.Security;
using Microsoft.Reporting.WebForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Net.Mime;
using System.Text;
using System.IO;

public partial class CodingOfficer_Rt_Printform : System.Web.UI.Page
{

    private int m_currentPageIndex;
    private IList<Stream> m_streams;
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objBE = new Registration_BE();
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
                Response.Redirect("~/Error.aspx");
            }
        }
        /*KILL COOKIE & clear Caching*/
        PrevBrowCache.enforceNoCache();
        if (!IsPostBack)
        {
            try
            {
                /*CALL REPORT FUNCTION*/
                PrintAllReports(Session["ReportName"].ToString());
            }
            catch (Exception ex)
            {
                // ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            }
        }
    }
    private void PrintAllReports(string ReportName)
    {
        try
        {
            LocalReport Report = new LocalReport();
            Report.DataSources.Clear();
            if (ReportName == "GenarateStikker")
            {

                dt = new DataTable();
                objBE.SampleID = Session["sampleid"].ToString();

                objBE.Action = "RTSTICKER";
               
                // objBE.qrcode =byte.Parse( Session["qrcode"].ToString());
                dt = Objrdl.GenerateSticker(objBE, Session["con"].ToString());
                Report.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/StikerDI.rdlc");
                Report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                Report.Refresh();

            }

            Export(Report);
            Print(ReportName);
        }
        catch (Exception ex)
        {
            //ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
        }
    }
    private void Print(string ReportName)
    {
        //const string printerName = "Microsoft Office Document Image Writer";

        if (m_streams == null || m_streams.Count == 0)
            return;


        Stream[] s = m_streams.ToArray();
        byte[] b = ReadFully(s[0]);

        Response.Clear();

        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "inline; filename=" + ReportName + ".pdf");
        Response.ContentType = "application/pdf";
        Response.Buffer = true;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite(b);
        Response.Flush();
        //Response.End();

    }
    public static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }
    private void Export(LocalReport report)
    {
        string deviceInfo =
          @"<DeviceInfo>
                <OutputFormat>PDF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
        Warning[] warnings;

        m_streams = new List<Stream>();
        report.Render("PDF", deviceInfo, CreateStream,
             out warnings);

        foreach (Stream stream in m_streams)
        {
            stream.Position = 0;

        }

    }
    private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
    {
        MemoryStream stream = new MemoryStream();
        m_streams.Add(stream);
        return stream;
    }
    public void Dispose()
    {
        if (m_streams != null)
        {
            foreach (Stream stream in m_streams)
                stream.Close();
            m_streams = null;
        }
    }
}