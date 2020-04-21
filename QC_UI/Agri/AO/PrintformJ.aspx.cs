using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;
using Microsoft.Reporting.WebForms;

public partial class AO_PrintformJ : System.Web.UI.Page
{
    Master_BE objmBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriDL Objrdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objR = new AgriBE();
    DataTable dt;
    string con, user, state, Department, Aocode;


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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "8")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            Aocode = Session["AO_Code"].ToString();

            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DistName"].ToString() + " -  " + Session["MandName"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                try
                {
                    random();
                   
                    BindCategory();

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
    protected void BindGrid()
    {
        check();
        try
        {
            objR.UserId = Aocode;
            objR.SampleCategory = RblCategory.SelectedValue;
            objR.dept = Department;
            objR.Action = "R3";
            dt = Objrdl.MemoID(objR, con);
            if (dt.Rows.Count > 0)
            {
                Gvprintformj.DataSource = dt;
                Gvprintformj.DataBind();
            }
            else
            {
                Gvprintformj.DataSource = null;
                Gvprintformj.DataBind();
                cf.ShowAlertMessage("No Records ");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void Printformj()
    {
        try
        {

            dt = new DataTable();
            objR.Action = "K";
            objR.SRegid = lblSampleId.Text;
            dt = Objrdl.PrintForm1718(objR, con);
            if (dt.Rows.Count > 0)
            {
                Rpt_PrintFormJ.LocalReport.DataSources.Clear();
                Rpt_PrintFormJ.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                Rpt_PrintFormJ.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/FormJ.rdlc");
                Rpt_PrintFormJ.LocalReport.Refresh();
                pnlprint.Visible = true;

            }
            else
            {
                cf.ShowAlertMessage("No Data Found");
                pnlprint.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }


    protected void BindCategory()
    {
        objmBE.Id = Department;
        objmBE.Action = "CAT";
        dt = ObjDL.Getdetails(objmBE, con);
        cf.BindRadioLists(RblCategory, dt, "category_name", "category_id", "0");
    }
    protected void RblCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        pnlprint.Visible = false;
    }


    protected void Gvprintformj_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Print-j")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblSampleId.Text = ((Label)(gvrow.FindControl("lblSampId"))).Text;
                lblSampleId.Visible = true;
                Printformj();
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