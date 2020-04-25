using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;


public partial class Agri_Fertilizer_CCOAO_Ack : System.Web.UI.Page
{
    AgriBE objBE = new AgriBE();
    AgriDL ObjDL = new AgriDL();
    CommonFuncs ObjCommon = new CommonFuncs();
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
                    BindGrid();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    ObjCommon.ShowAlertMessage(ex.ToString());
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
            objBE.Action = "AO";
            objBE.UserId = user;
            objBE.dept = dept;
            objBE.SampleCategory = cate;
            dt = ObjDL.CourierIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GVAck.DataSource = dt;
                GVAck.DataBind();
            }
            else
            {
                GVAck.DataSource = null;
                GVAck.DataBind();
                lbltext.Visible = true;
                btnAck.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }

    }

    protected void btnAck_Click1(object sender, EventArgs e)
    {
        check();
        try
        {
            DataTable ddt = new DataTable();
            ddt.Columns.Add("Memo_ID", typeof(string));
            ddt.Columns.Add("ReceiptDt", typeof(string));

            int i = 0;
            foreach (GridViewRow gr in GVAck.Rows)
            {
                if (((TextBox)gr.FindControl("txtRecieptDt")).Text != "")
                {
                    CheckBox CHK = ((CheckBox)gr.FindControl("chkSelct"));
                    if (CHK.Checked == true)
                    {
                        ddt.Rows.Add();
                        ddt.Rows[i]["Memo_ID"] = ((Label)gr.FindControl("lblMemoId")).Text.Trim();
                        ddt.Rows[i]["ReceiptDt"] = ObjCommon.Texttodateconverter(((TextBox)gr.FindControl("txtRecieptDt")).Text.Trim());
                        i++;
                    }
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Receipt Date Should not be Empty");
                }
            }
         
            objBE.AckTVP = ddt; 
            objBE.dept = dept;
            objBE.UserId =user;
            objBE.Action = "U";
            dt = ObjDL.CourierIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
               //  ObjCommon.ShowAlertMessage("Selected Samples Acknowledged");
                 ObjCommon.ShowAlertMessage("Selected MemoId's Acknowledged");
                BindGrid();
            }
            else
            {
                ObjCommon.ShowAlertMessage("Selected MemoId's Acknowledged");
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
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