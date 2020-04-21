using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Inspector_Samplebyhand : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL objdl = new Registration_DL();
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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "1")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            DI_AO_code = Session["DI_AO_Code"].ToString();

            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DIZoneNm"].ToString() + " -  " + Session["DIName"].ToString();

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
            objBE.Action = "C";
            objBE.DI_AO_Code = DI_AO_code;
            dt = objdl.SampleRegistrationDI(objBE, con);

            if (dt.Rows.Count > 0)
            {
                Gvcouriers.DataSource = dt;
                Gvcouriers.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                Gvcouriers.DataSource = null;
                Gvcouriers.DataBind();
                cf.ShowAlertMessage("No Data Found");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            lblError.Text = ex.Message.ToString();
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
            int i = 0; int n = 0;
            foreach (GridViewRow gr in Gvcouriers.Rows)
            {               
                CheckBox CHK = ((CheckBox)gr.FindControl("chkSelct"));
                if (CHK.Checked == true)
                {
                    n++;
                    if (((((TextBox)gr.FindControl("txtDisDate")).Text != "")))
                    {
                        ddt.Rows.Add();
                        ddt.Rows[i]["SampleCat"] = null;
                        ddt.Rows[i]["Memo_ID"] = ((Label)gr.FindControl("lblMemoId")).Text;
                        ddt.Rows[i]["CourierName"] = "Send by hand";
                        ddt.Rows[i]["DispatchDate"] = cf.Texttodateconverter(((TextBox)gr.FindControl("txtDisDate")).Text);
                        ddt.Rows[i]["POD_No"] = null;
                        i++;
                    }
                }
            }
            if (n == 0)
                cf.ShowAlertMessage("Please check at lease one row to submit");
            else
            {
                DataTable TVP = new DataTable();
                objBE.state = state;
                objBE.dept = dept;
                objBE.UserId = DI_AO_code;
                objBE.Action = "DII";
                objBE.TVP = ddt;
                dt = objdl.CourierIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Insert Fail");
                else
                    cf.ShowAlertMessage("Sample Send By Hand Saved Successfully");
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