using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;

public partial class Analyst_Ack : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con;

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

        if (Session["UsrName"] != null && Session["RoleID"].ToString().Trim() == "5" || Session["RoleID"].ToString().Trim() == "6")
        {

            con = Session["ConnKey"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;


            if (Session["RoleID"].ToString().Trim() == "5")
            {
                lblUser.Text = Session["Role"].ToString() + " -  " + Session["AnalystName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            }
            if (Session["RoleID"].ToString().Trim() == "6")
            {
                lblUser.Text = Session["Role"].ToString() + " -  " + Session["JsoName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            }
            //state = Session["StateCode"].ToString();
            // user = Session["UserId"].ToString();

            // Department = Session["Department"].ToString();
            // dicode = Session["ssoCode"].ToString();


            if (!IsPostBack)
            {
                try
                {
                    random();
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
        {
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BindGrid()
    {
        try
        {
            dt = new DataTable();
            objBE.Action = "ACKSMPL";

            if (Session["RoleID"].ToString() == "5")
            {

                objBE.AnalystId = Session["AnalystCode"].ToString();
            }
            if (Session["RoleID"].ToString() == "6")
            {
                objBE.AnalystId = Session["Jsocode"].ToString(); 
            }

            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvCourier.DataSource = dt;
                GvCourier.DataBind();
            }
            else
            {
                GvCourier.DataSource = null;
                GvCourier.DataBind();
                lbltext.Visible = true;
                btnAck.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkselect = sender as CheckBox;

            if (chkselect.Checked == true)
            {
                foreach (GridViewRow gr in GvCourier.Rows)
                {
                    ((CheckBox)gr.FindControl("chkSelct")).Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow gr in GvCourier.Rows)
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
    protected void btnAck_Click(object sender, EventArgs e)
    {

        try
        {
            DataTable ddt = new DataTable();
            ddt.Columns.Add("Sample_ID", typeof(string));
            ddt.Columns.Add("ReceiptDt", typeof(string));

            int i = 0;
            foreach (GridViewRow gr in GvCourier.Rows)
            {
                if (((TextBox)gr.FindControl("txtRecieptDt")).Text != "")
                {
                    CheckBox CHK = ((CheckBox)gr.FindControl("chkSelct"));
                    if (CHK.Checked == true)
                    {
                        ddt.Rows.Add();
                        ddt.Rows[i]["Sample_ID"] = ((Label)gr.FindControl("lblSampleId")).Text.Trim();
                        ddt.Rows[i]["ReceiptDt"] = cf.Texttodateconverter(((TextBox)gr.FindControl("txtRecieptDt")).Text.Trim());
                        i++;
                    }
                }
                else
                {
                    cf.ShowAlertMessage("Receipt Date Should not be Empty");
                }
            }
            objBE.TVP = ddt;
            
            objBE.Action = "ACK";
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                // cf.ShowAlertMessage("Selected Samples Acknowledged");
                BindGrid();
            }
            else
            {
                cf.ShowAlertMessage("Selected Samples Acknowledged");
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
