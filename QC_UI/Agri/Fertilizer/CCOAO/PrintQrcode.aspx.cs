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
    Masters ObjMDL = new Masters();
    Master_BE objMBE = new Master_BE();
    AgriBE objBE = new AgriBE();
    AgriDL ObjDL = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, dept, cate;

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

        if (Session["UsrName"] != null && Session["RoleID"].ToString().Trim() == "14")
        {
            user = Session["CCOAO"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            cate = Session["Category"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["UsrName"].ToString();
                    BindMemo();
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
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BindMemo()
    {
        dt = new DataTable();
        objBE.dept = dept;
        objBE.SampleCategory = cate;
        objBE.UserId = user;
        objBE.Action = "MEMO_QRCODE";
        dt = ObjDL.GenerateSticker_AGRI(objBE, con);
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
    protected void BindGrid()
    {
        try
        {
            dt = new DataTable();
            objBE.Action = "MEMO_QRCODEGV";
            objBE.dept = dept;
            objBE.SampleCategory = cate;
            objBE.MemoId = ddlmemo.SelectedValue;
            dt = ObjDL.GenerateSticker_AGRI(objBE, con);
            if (dt.Rows.Count > 0)
            {
                Gvqrcode.DataSource = dt;
                Gvqrcode.DataBind();
            }
            else
            {
                Gvqrcode.DataSource = null;
                Gvqrcode.DataBind();
                lbltext.Visible = true;

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
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
    protected void btnget_Click(object sender, EventArgs e)
    {
        BindGrid();

    }
    protected void Gvqrcode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "INFR")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblsampleid"))).Text;
                objBE.SampleID = lblsampleid.Text;
                objBE.Action = "RPSTICKER";
                dt = ObjDL.GenerateSticker_AGRI(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    Rpt_Sticker.LocalReport.DataSources.Clear();
                    Rpt_Sticker.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    Rpt_Sticker.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
                    Rpt_Sticker.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Stiker_agri.rdlc");
                    Rpt_Sticker.LocalReport.Refresh();
                    //sticker.Visible = true;
                    Rpt_Sticker.ShowPrintButton = true;
                }
            }
            if (e.CommandName == "QRCODE")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblsampleid"))).Text;
                objBE.SampleID = lblsampleid.Text;
                objBE.Action = "RPSTICKER";
                dt = ObjDL.GenerateSticker_AGRI(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    Rpt_Sticker.LocalReport.DataSources.Clear();
                    Rpt_Sticker.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    Rpt_Sticker.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
                    Rpt_Sticker.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Qrcode_agri.rdlc");
                    Rpt_Sticker.LocalReport.Refresh();
                    //sticker.Visible = true;
                    Rpt_Sticker.ShowPrintButton = true;
                }
            }


        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void Rpt_Sticker_Load(object sender, EventArgs e)
    {
        string exportOption = "Excel";
        string exportOption1 = "Word";
        RenderingExtension extension = Rpt_Sticker.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        RenderingExtension extensions = Rpt_Sticker.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));
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
}