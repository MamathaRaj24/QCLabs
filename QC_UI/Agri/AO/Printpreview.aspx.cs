using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Agri_AO_Printpreview : System.Web.UI.Page
{
    CommonFuncs cf = new CommonFuncs();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["ReportName"].ToString() == "P_FileContent")
                {
                    byte[] bytes;
                    bytes = (byte[])Session["Filepanchanama"];
                    Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + Session["pfilename"].ToString());
                    Response.BinaryWrite(bytes);
                }
                if (Session["ReportName"].ToString() == "FIR_FileContent")
                {
                    byte[] bytes;
                    bytes = (byte[])Session["FIRFileContent"];
                    Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + Session["firfilename"].ToString());
                    Response.BinaryWrite(bytes);
                }
                if (Session["ReportName"].ToString() == "Ltr_Content")
                {
                    byte[] bytes;
                    bytes = (byte[])Session["LtrContent"];
                    Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + Session["ltrfilename"].ToString());
                    Response.BinaryWrite(bytes);
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