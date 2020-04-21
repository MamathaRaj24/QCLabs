using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class AO_CourierEntry : System.Web.UI.Page
{

    AgriBE objBE = new AgriBE();
    AgriDL objdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, AO_code, dept;


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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "8")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            AO_code = Session["AO_Code"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year; 
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DistName"].ToString() + " -  " + Session["MandName"].ToString();


            if (!IsPostBack)
            {
                random();
                try
                {
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
            objBE.Action = "MR";
            objBE.UserId = AO_code;
            objBE.dept = dept;
            objBE.TVP = null;
            dt = objdl.MemoID(objBE, con);

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
                btnSave.Visible = false;
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
            ddt.Columns.Add("SampleCat", typeof(string));
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
                        ddt.Rows[i]["SampleCat"] = ((Label)gr.FindControl("lblcategory")).Text;
                        ddt.Rows[i]["Memo_ID"] = ((Label)gr.FindControl("lblMemoId")).Text;
                        ddt.Rows[i]["CourierName"] = ((TextBox)gr.FindControl("txtCourierName")).Text;
                        ddt.Rows[i]["DispatchDate"] = cf.Texttodateconverter(((TextBox)gr.FindControl("txtDisDate")).Text);
                        ddt.Rows[i]["POD_No"] = ((TextBox)gr.FindControl("txtPodNo")).Text;
                        i++;
                    }
                }
                else
                {
                    cf.ShowAlertMessage("Courier Details Should not be Empty");
                }
            }
            DataTable TVP = new DataTable();
            objBE.state = state;
            objBE.dept = dept;
            objBE.UserId = AO_code;
            objBE.Action = "I";
            objBE.TVP = ddt;
            objBE.SampleCategory = "";
            dt = objdl.CourierIUDR(objBE, con);
            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage("Insert Fail");
            else
                cf.ShowAlertMessage("Courier Entry Saved Successfully");
            BindGrid();
            btnSave.Visible = false;
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
}