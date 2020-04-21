using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DCA_DI_Dashboard : System.Web.UI.Page
{
    
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objR = new Registration_BE();
    DataTable dt;
    string con, user, state, Department, dicode;


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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "1")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            dicode = Session["DI_AO_Code"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["DIZoneNm"].ToString() + " -  " + Session["DIName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    SampleData.Visible = false;
                    Div1.Visible = false;
                    GetDetails();
                    BindGrid();
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
    protected void GetDetails()
    {
        try
        {
            dt = new DataTable();
            objR.DI_AO_Code = dicode;
            objR.dept = Department;
            objR.Action = "STATUS";
            dt = Objrdl.DashBoard(objR, con);
            lblsamplesReg.Text = dt.Rows[0][0].ToString();

            objR.status = "5";
            objR.Action = "STATUS";
            dt = Objrdl.DashBoard(objR, con);
            lblAccepted.Text = dt.Rows[0][0].ToString();
           
            lblRejected.Text = (Convert.ToInt16(lblsamplesReg.Text.Trim()) - Convert.ToInt16(lblsamplesReg.Text.Trim())).ToString();

            objR.status = "11";
            objR.Action = "STATUS";
            dt = Objrdl.DashBoard(objR, con);
            lblSamTested.Text = dt.Rows[0][0].ToString();

            objR.status = "12";
            objR.Action = "STATUS";
            dt = Objrdl.DashBoard(objR, con);
            lblnoc.Text = dt.Rows[0][0].ToString();

            objR.status = "13";
            objR.Action = "STATUS";
            dt = Objrdl.DashBoard(objR, con);
            lblnonc.Text = dt.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BindGrid()
    {
        try
        {
            Registration_BE objR = new Registration_BE();
            objR.DI_AO_Code = dicode;
            objR.dept = Department;
            dt = new DataTable();
            objR.Action = "DISTATUS";
            dt = Objrdl.DashBoard(objR, con);
            GVStaus.DataSource = dt;
            GVStaus.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void GVStaus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                Registration_BE objR = new Registration_BE();
                dt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objR.DI_AO_Code = dicode;
                objR.dept = Department;
                objR.status = ((Label)(gvrow.FindControl("lblStatusId"))).Text;
                string Status = ((Label)(gvrow.FindControl("lblStatusName"))).Text;
                objR.Action = "SMPL";
                dt = Objrdl.DashBoard(objR, con);
                if (dt.Rows.Count > 0)
                {
                    GVSamples.DataSource = dt;
                    GVSamples.DataBind();
                    SampleData.Visible = true;
                    lblStatus.Text = Status;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void GVSamples_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                dt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objR.SampleID = ((Label)(gvrow.FindControl("lblSampleId"))).Text;
                string SampleStatus = objR.SampleID;
                objR.dept = Department;
                objR.Action = "VIEW";
                dt = Objrdl.DashBoard(objR, con);
                if (dt.Rows.Count > 0)
                {
                    GVViewSmpl.DataSource = dt;
                    GVViewSmpl.DataBind();
                    Div1.Visible = true;
                    lblSampleStatus.Text = SampleStatus;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
}