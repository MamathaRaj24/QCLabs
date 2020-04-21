using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;


public partial class Agri_Fertilizer_CCOAO_CourierEntry : System.Web.UI.Page
{

    AgriBE objBE = new AgriBE();
    AgriDL objdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, DI_AO_code, dept;


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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "14")
        {
            state = Session["StateCode"].ToString();
            user = Session["CCOAO"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
         
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year; 
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["UsrName"].ToString();

            if (!IsPostBack)
            {
                random();
                BindGrid();
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
            objBE.Action = "CO"; 
            objBE.dept = dept;
            dt = objdl.GenerateSticker_AGRI(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvCourier.DataSource = dt;
                GvCourier.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                GvCourier.DataSource = null;
                GvCourier.DataBind();
                cf.ShowAlertMessage("No Data Found");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            DataTable ddt = new DataTable();
            ddt.Columns.Add("Memo_ID", typeof(string));
            ddt.Columns.Add("CourierName", typeof(string));
            ddt.Columns.Add("DispatchDate", typeof(DateTime));
            ddt.Columns.Add("POD_No", typeof(string));
            int i = 0;
            foreach (GridViewRow gr in GvCourier.Rows)
            {
                CheckBox CHK = ((CheckBox)gr.FindControl("chkSelct"));
                if (CHK.Checked == true)
                {
                    if (((TextBox)gr.FindControl("txtCourierName")).Text != "" && ((TextBox)gr.FindControl("txtDisDate")).Text != "" && ((TextBox)gr.FindControl("txtPodNo")).Text != "")
                    {
                        ddt.Rows.Add();
                        ddt.Rows[i]["Memo_ID"] = ((Label)gr.FindControl("lblMemoId")).Text; 
                        ddt.Rows[i]["CourierName"] = ((TextBox)gr.FindControl("txtCourierName")).Text;
                        ddt.Rows[i]["DispatchDate"] = cf.Texttodateconverter(((TextBox)gr.FindControl("txtDisDate")).Text);
                        ddt.Rows[i]["POD_No"] = ((TextBox)gr.FindControl("txtPodNo")).Text;
                        i++;
                    }
                    objBE.MemoId = ((Label)gr.FindControl("lblMemoId")).Text;
                    objBE.SampleID = ((Label)gr.FindControl("lblsampleid")).Text; 
                }
                else
                {
                    cf.ShowAlertMessage("Courier Details Should not be Empty");
                }
               
            }
            DataTable TVP = new DataTable(); 
            objBE.dept = dept;
            objBE.UserId = user;
            objBE.Action = "U";
            objBE.TVP = ddt;
            dt = objdl.GenerateSticker_AGRI(objBE, con);
            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage("Insert Fail");
            else
                cf.ShowAlertMessage("Courier Entry Saved Successfully");
            BindGrid();
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