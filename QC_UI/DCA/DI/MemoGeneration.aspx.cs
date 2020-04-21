using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;
using Microsoft.Reporting.WebForms;

public partial class DI_MemoGeneration : System.Web.UI.Page
{

    Registration_BE objBE = new Registration_BE();
    Registration_DL objdi = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, DI_AO_code, dept;


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
                Response.Redirect("~/Error.aspx");
        }
        PrevBrowCache.enforceNoCache();
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "1" || Session["RoleID"].ToString() == "8" || Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10"))
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
                try
                {
                    btnSubmit.Visible = false;
                    random();
                    BindSampleId();
                    pnltestresult.Visible = false;
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

    protected void BindSampleId()
    {
        dt = new DataTable();
        objBE.DI_AO_Code = DI_AO_code;
        objBE.Action = "F";
        dt = objdi.SampleRegistrationDI(objBE, con);
        cf.BindDropDownLists(ddlSamId, dt, "RegID", "RegID", "Select Sample Id");
    }
    protected void ddlSamId_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewData();
    }
    protected void ViewData()
    {
        check();
        try
        {
            objBE.DI_AO_Code = DI_AO_code;
            objBE.SRegid = ddlSamId.SelectedValue;
            objBE.Action = "F";
            dt = objdi.SampleRegistrationDI(objBE, con);
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
              
                lblPaymentType.Text = dt.Rows[0]["PaymentType"].ToString();
                //lblCatName.Text = dt.Rows[0]["Category_Name"].ToString();
                //lblSamDate.Text = dt.Rows[0]["SampleDate"].ToString();
                lblSamColDate.Text = dt.Rows[0]["SampleCollectingDate"].ToString();
                lblGenName.Text = dt.Rows[0]["GenericName"].ToString();
                lblFirmName.Text = dt.Rows[0]["Firm_Name"].ToString();
                lblfirmtype.Text = dt.Rows[0]["TypeName"].ToString();
                lblSamcls.Text = dt.Rows[0]["class_name"].ToString();
                lblFirmName.Text = dt.Rows[0]["Firm_Name"].ToString();
                lblSamcls.Text = dt.Rows[0]["class_name"].ToString();
                lblUsage.Text = dt.Rows[0]["Usage"].ToString();
                lblPriority.Text = dt.Rows[0]["Priority"].ToString();
                lblRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                lblLicNo.Text = dt.Rows[0]["LicenceNo"].ToString();
                lblValidity.Text = dt.Rows[0]["Validity"].ToString();
                lblContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
                lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
                lblQuantity.Text = dt.Rows[0]["QtyPicked"].ToString();
                lblState.Text = dt.Rows[0]["State"].ToString();
                lblDistrict.Text = dt.Rows[0]["DistName"].ToString();
                lblMandal.Text = dt.Rows[0]["MandName"].ToString();
                lblHouse.Text = dt.Rows[0]["HouseNo"].ToString();
                lblLocality.Text = dt.Rows[0]["Locality"].ToString();
                lblSamCategory.Text = dt.Rows[0]["Category_Name"].ToString();
                lblTrade.Text = dt.Rows[0]["TradeName"].ToString();
                lblBatchNo.Text = dt.Rows[0]["BatchNo"].ToString();
                lblManuDate.Text = dt.Rows[0]["ManufacturingDate"].ToString();//
                lblExpDt.Text = dt.Rows[0]["ExpiryDate"].ToString();
                lblManSt.Text = dt.Rows[0]["ManufacturerState"].ToString();
                lblmanuAddress.Text = dt.Rows[0]["ManufAddress"].ToString();
                lblManName.Text = dt.Rows[0]["ManfucaturerName"].ToString();
                lblManuLicense.Text = dt.Rows[0]["ManufacutrerLicence"].ToString();
                lblMarketerAddress.Text = dt.Rows[0]["MarketerAddress"].ToString();
                lblMarketerName.Text = dt.Rows[0]["MarketerName"].ToString();
                lblMarketerState.Text = dt.Rows[0]["MarketerState"].ToString();
                lblstkRecDt.Text = dt.Rows[0]["StkRcvdDate"].ToString();
                lblCom.Text = dt.Rows[0]["Composition"].ToString();
                lblSamQty.Text = dt.Rows[0]["SampleQty"].ToString();             
                pnltestresult.Visible = true;
            }
            else
            {
                cf.ShowAlertMessage("Data Not Present");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());            
        }
    }
   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            objBE.state = state;
            objBE.dept = dept;
            objBE.SRegid = ddlSamId.SelectedValue;
            objBE.Action = "MEMO";
            objBE.DI_AO_Code = DI_AO_code;
            dt = objdi.SampleRegistrationDI(objBE, con);
            if (dt.Rows.Count > 0)
            {
                ddlSamId.Enabled = false;
                cf.ShowAlertMessage("Memo Id Generated Successfully");
                Session["Memoid"] = dt.Rows[0][0].ToString();
                objBE.MemoId = Session["Memoid"].ToString();
                objBE.Action = "PRINT";
                dt = objdi.SampleRegistrationDI(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    pnltestresult.Visible = false;
                    Rpt_PrintMemo.LocalReport.DataSources.Clear();
                    Rpt_PrintMemo.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                    Rpt_PrintMemo.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/RdlcReports/Rpt_MemoGeneration.rdlc");
                    Rpt_PrintMemo.LocalReport.Refresh();
                    Rpt_PrintMemo.Visible = true;
                }
                else
                {
                    cf.ShowAlertMessage("No Data Found");
                }
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
