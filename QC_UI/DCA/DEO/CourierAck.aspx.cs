using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DEO_CourierAck : System.Web.UI.Page
{

    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objBE = new Registration_BE();
    DataTable dt;
    string con, user, state, Department, deocode;

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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "2")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            deocode = Session["DeoCode"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString()+"-"+ Session["DeoName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
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
            objBE.Action = "S";
            objBE.dept = Department;
            dt = Objrdl.CourierIUDR(objBE, con);

            if (dt.Rows.Count > 0)
            {
                GVCourierAck.DataSource = dt;
                GVCourierAck.DataBind();
            }
            else
            {
                GVCourierAck.DataSource = null;
                GVCourierAck.DataBind();
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

    protected void btnAck_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            DataTable ddt = new DataTable();
            ddt.Columns.Add("Memo_ID", typeof(string));
            ddt.Columns.Add("ReceiptDt", typeof(string));

            int i = 0;
            foreach (GridViewRow gr in GVCourierAck.Rows)
            {
                if (((TextBox)gr.FindControl("txtRecieptDt")).Text != "")
                {
                    CheckBox CHK = ((CheckBox)gr.FindControl("chkSelct"));
                    if (CHK.Checked == true)
                    {
                        ddt.Rows.Add();
                        ddt.Rows[i]["Memo_ID"] = ((Label)gr.FindControl("lblMemoId")).Text;
                        ddt.Rows[i]["ReceiptDt"] = cf.Texttodateconverter(((TextBox)gr.FindControl("txtRecieptDt")).Text.Trim());
                        i++;
                    }
                }
                else
                {
                    cf.ShowAlertMessage("Receipt Date Should not be Empty");
                }
            }
            objBE.AckTVP = ddt;
            objBE.state = Session["StateCode"].ToString();
            objBE.dept = Session["Department"].ToString();
            objBE.UserId = deocode;
            objBE.Action = "U";
            dt = Objrdl.CourierIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Sample Details Acknowledged Successfully");
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