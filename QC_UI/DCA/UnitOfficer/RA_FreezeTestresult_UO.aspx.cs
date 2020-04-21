using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using QC_DL;
using System.Data;
using QC_BE;
public partial class DCA_UnitOfficer_RA_FreezeTestresult_UO : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, Uocode;

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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "4")
        {
            con = Session["ConnKey"].ToString();

            Uocode = Session["UnitOfficerCode"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["UnitOfficerName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                random();
                try
                {
                    BindGrid();
                   // DivViewData.Visible = false;
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
    protected void BindGrid()
    {
        objBE.Action = "RAUOVIEMPRINT";
        objBE.UOId = Uocode;
        objBE.flag = "0";
        // objBE.status = "RT";
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            GVTestResult.DataSource = dt;
            GVTestResult.DataBind();
        }
        else
        {
            GVTestResult.DataSource = null;
            GVTestResult.DataBind();
            cf.ShowAlertMessage("No data");
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkselect = sender as CheckBox;

            if (chkselect.Checked == true)
            {
                foreach (GridViewRow gr in GVTestResult.Rows)
                {
                    ((CheckBox)gr.FindControl("chkSelct")).Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow gr in GVTestResult.Rows)
                {
                    ((CheckBox)gr.FindControl("chkSelct")).Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void btnFreeze_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable ddt = new DataTable();
            ddt.Columns.Add("Sample_ID", typeof(string));
          //  ddt.Columns.Add("ReceiptDt", typeof(string));
            int i = 0;
            foreach (GridViewRow gr in GVTestResult.Rows)
            {
                CheckBox CHK = ((CheckBox)gr.FindControl("chkSelct"));
                if (CHK.Checked == true)
                {
                    ddt.Rows.Add();
                    ddt.Rows[i]["Sample_ID"] = ((Label)gr.FindControl("lblSampleid")).Text.Trim();
                    //ddt.Rows[i]["ReceiptDt"] = null;
                    i++;
                }
            }
            if (i == 0)
                cf.ShowAlertMessage("Select atleast one row to freeze");
            else
            {
                objBE.tvpfreeze_UO = ddt;
                objBE.flag = "1";
                objBE.Action = "FREEZE_UO";
                dt = ObjDL.JAActionEdit(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Not Submitted , Try again");
                else
                    cf.ShowAlertMessage("Selected Samples Submitted");
                BindGrid();
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
            //cookie_value = System.Web.HttpContext.Current.Request.Cookies["ASPFIXATION2"].Value;
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