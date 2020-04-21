using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DI_Freeze_DI : System.Web.UI.Page
{
    Registration_BE objR = new Registration_BE();
    Registration_DL objdi = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, DI_AO_Code, dept;


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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "1")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            DI_AO_Code = Session["DI_AO_Code"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["DIZoneNm"].ToString() + " -  " + Session["DIName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindGrid();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    Response.Redirect("~/Error.aspx");
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
        objR.Action = "R";
        objR.DI_AO_Code = DI_AO_Code;
        dt = objdi.SampleRegistrationDI(objR, con);
        if (dt.Rows.Count > 0)
        {
            Gvfreeze.DataSource = dt;
            Gvfreeze.DataBind();
        }
    }
    protected void btnfreeze_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtfreeze = new DataTable();
            dtfreeze.Columns.Add("sampleID", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in Gvfreeze.Rows)
            {
                if (((CheckBox)gr.FindControl("chkSelct")).Checked == true)
                {
                    dtfreeze.Rows.Add();
                    dtfreeze.Rows[j]["sampleID"] = ((Label)gr.FindControl("lblsmid")).Text;
                    j++;
                }
            }
            if (j == 0)
                cf.ShowAlertMessage("Select atleast one row to freeze");
            else
            {
                objR.TVP = dtfreeze;
                objR.Action = "DI";
                dt = objdi.FreezeSamples(objR, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage("Not freezed, try again");
                }
                else
                {
                    cf.ShowAlertMessage("Sample Registered Successfully");
                    Gvfreeze.DataSource = null;
                    Gvfreeze.DataBind();
                    BindGrid();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("../Error.aspx");
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