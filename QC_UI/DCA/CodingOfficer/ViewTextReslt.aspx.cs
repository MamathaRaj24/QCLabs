using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class CodingOfficer_ViewTextReslt : System.Web.UI.Page
{

    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objBE = new Registration_BE();
    Validate objValidate = new Validate();
    DataTable dt;
    IFormatProvider provider = new System.Globalization.CultureInfo("EN-IN", true);
    string con, user, state, Department, dicode;
    SampleSqlInjectionScreeningModule obj = new SampleSqlInjectionScreeningModule();

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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "3")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            dicode = Session["ssoCode"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString() + " >> " + Session["ssoName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

                    BindSample();
                    Div.Visible = false;
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
    public void BindSample()
    {
        try
        {
            objBE.Action = "SMPL";
            dt = Objrdl.PrintForm13(objBE, con);

            cf.BindDropDownLists(ddlSampleID, dt, "", "RegID", "0");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    protected void ddlSampleID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Div.Visible = true;
            dt = new DataTable();
            objBE.SampleID = ddlSampleID.SelectedValue;
            objBE.Action = "FORM";
            dt = Objrdl.PrintForm13(objBE, con);

            if (dt.Rows.Count > 0)
            {
                Div.Visible = true;
                lblInspectName.Text = dt.Rows[0]["Name"].ToString();
                lblWhomReceived.Text = dt.Rows[0]["ZoneName"].ToString();
                lblManuName.Text = dt.Rows[0]["ManfucaturerName"].ToString();
                lblMemoId.Text = dt.Rows[0]["Memo_ID"].ToString();
                lblSampleName.Text = dt.Rows[0]["SampleName"].ToString();
                lblReceiptDate.Text = dt.Rows[0]["ReceiptDt"].ToString();
                lblBatchNo.Text = dt.Rows[0]["BatchNo"].ToString();
                lblManuDate.Text = dt.Rows[0]["ManufacturingDate"].ToString();
                lblExpireDate.Text = dt.Rows[0]["ExpiryDate"].ToString();
                lblmemoDate.Text = dt.Rows[0]["Memo_Dt"].ToString();
                lblManuName.Text = dt.Rows[0]["ManfucaturerName"].ToString();
                lblAddress.Text = dt.Rows[0]["Address"].ToString() + ',' + dt.Rows[0]["State"].ToString();
                lblManuLicense.Text = dt.Rows[0]["ManufacutrerLicence"].ToString();
                lblDescription.Text = dt.Rows[0]["Description"].ToString();
                lblRemarks.Text = dt.Rows[0]["JARemarks"].ToString();
                lblComposition.Text = dt.Rows[0]["Composition"].ToString();
                lblGenericName.Text = dt.Rows[0]["GenericName"].ToString();
                lblTradeName.Text = dt.Rows[0]["TradeName"].ToString();
                lblStatus.Text = dt.Rows[0]["status"].ToString();
                GvView.DataSource = dt; 
                GvView.DataBind();
            }
            else
                cf.ShowAlertMessage("No Data Found");
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
            //cookie_value = System.Web.HttpContext.Current.Request.Cookies["ASPFIXATION2"].Value;
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