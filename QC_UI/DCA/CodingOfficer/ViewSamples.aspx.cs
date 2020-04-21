using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class CodingOfficer_ViewSamples : System.Web.UI.Page
{
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
    Registration_DL Objrdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objBE = new Registration_BE();
    DataTable dt;
    string con, user, state, Department, ssoCode;

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
            ssoCode = Session["ssoCode"].ToString();
                   
            
            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString() + " >> " + Session["ssoName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

                    BtnReject.Visible = false;
                    BtnAccept.Visible = false;
                    //regreasonid.Visible = false;
                    BindMemos();
                    ViewDiv.Visible = false;
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
    protected void BindMemos()
    {
        dt = new DataTable();
        objBE.Action = "VMEMO";
        objBE.dept = Department;
        dt = Objrdl.SampleRegister(objBE, con);
        cf.BindDropDownLists(ddlmemo, dt, "Memo_ID", "Memo_ID", "Select");
    }
    protected void ddlmemo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dt = new DataTable();
            objBE.MemoId = ddlmemo.SelectedValue;
            objBE.Action = "VSMPL";
            objBE.dept = Department;
            dt = Objrdl.SampleRegister(objBE, con);
            cf.BindDropDownLists(ddlsample, dt, "SampleID", "SampleID", "Select");
            ViewDiv.Visible = false;
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void ddlsample_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
            ViewDiv.Visible = true;
            DataTable dtt = new DataTable();
            objBE.Action = "V";
            objBE.MemoId = ddlmemo.SelectedValue;
            objBE.SRegid = ddlsample.SelectedValue;
            dtt = Objrdl.SampleRegistrationDI(objBE, con);

            if (dtt.Rows.Count > 0)
            {

                lblcategory.Text = dtt.Rows[0]["category_name"].ToString();
                lblusage.Text = dtt.Rows[0]["Usage"].ToString();
                lblPayment.Text = dtt.Rows[0]["PaymentType"].ToString();
                lblPriority.Text = dtt.Rows[0]["Priority"].ToString();

                lblremarks.Text = dtt.Rows[0]["Remarks"].ToString();
                lblfirmtype.Text = dtt.Rows[0]["TypeName"].ToString();
                lblfirmNm.Text = dtt.Rows[0]["Firm_Name"].ToString();
                lblcontactNm.Text = dtt.Rows[0]["ContactPerson"].ToString();
                lblLicenseNo.Text = dtt.Rows[0]["LicenceNo"].ToString();
                if (dtt.Rows[0]["Validity"].ToString() != "")
                    lblvalidity.Text = dtt.Rows[0]["Validity"].ToString();
                lblstate.Text = dtt.Rows[0]["State"].ToString().Trim();
                lbldist.Text = dtt.Rows[0]["District"].ToString().Trim();
                lblMand.Text = dtt.Rows[0]["Mandal"].ToString();
                lblhno.Text = dtt.Rows[0]["HouseNo"].ToString().Trim();
                lblloality.Text = dtt.Rows[0]["Locality"].ToString();

                lblcategory.Text = dtt.Rows[0]["Category_Name"].ToString();
                lblsampledt.Text = dtt.Rows[0]["SampleDate"].ToString();
                lbldrugnm.Text = dtt.Rows[0]["TradeName"].ToString();
                lblgenericnm.Text = dtt.Rows[0]["GenericName"].ToString();
                lblqty.Text = dtt.Rows[0]["SampleQty"].ToString();
                lblbatchno.Text = dtt.Rows[0]["BatchNo"].ToString();
               
                lblmanudt.Text = dtt.Rows[0]["ManufacturingDate"].ToString();
                lblexpdt.Text = dtt.Rows[0]["ExpiryDate"].ToString();
                lblstkrcvd.Text = dtt.Rows[0]["StkRcvdDate"].ToString();
                lblcomposition.Text = dtt.Rows[0]["Composition"].ToString().Trim();
                //lblStkRcvdFrom.Text = dtt.Rows[0]["StockReceivedFrom"  lblInvoice.Text = dtt.Rows[0]["InvoiceNo"].ToString();
               
                lblmanuNm.Text = dtt.Rows[0]["ManfucaturerName"].ToString();
                lblManuLicence.Text = dtt.Rows[0]["ManufacutrerLicence"].ToString();
                lbladdress.Text = dtt.Rows[0]["ManufAddress"].ToString();

                lblmanufactureState.Text = dtt.Rows[0]["ManufacturerState"].ToString();

            }
            DataTable ds;
            objBE.MemoId = ddlmemo.SelectedValue;
            objBE.SampleID = ddlsample.SelectedValue;
            objBE.Action = "R";
            ds = Objrdl.SampleRegister(objBE, con);

            if (ds.Rows.Count > 0)
            {
                lblDrugName.Text = ds.Rows[0]["NameOfDrug"].ToString();
                lblqtyRcvd.Text = ds.Rows[0]["Quantity"].ToString();
                lblUsages.Text = ds.Rows[0]["Usage"].ToString();
                lblBtchNo.Text = ds.Rows[0]["BatchNo"].ToString();
                lblManufName.Text = ds.Rows[0]["ManufacturedBy"].ToString();
                lblManuFDt.Text = Convert.ToDateTime(ds.Rows[0]["ManufDate"]).ToString("MM-yyyy");
                lblExpiryDt.Text = Convert.ToDateTime(ds.Rows[0]["ExpiryDt"]).ToString("MM-yyyy");
                lblGenricNm.Text = ds.Rows[0]["GenericNm"].ToString();
                lblLicenseNos.Text = ds.Rows[0]["MfgLicenceNo"].ToString();
                lblCptn.Text = ds.Rows[0]["Composition"].ToString();
                lblRcvdDt.Text = Convert.ToDateTime(ds.Rows[0]["RcvdDt"]).ToString("dd-MM-yyyy");
                lblNAME.Text = ds.Rows[0]["NAME"].ToString();
                lblCategorys.Text = ds.Rows[0]["category_name"].ToString();
                lblPrioritys.Text = ds.Rows[0]["Priority"].ToString();
                lblRemarkes.Text = ds.Rows[0]["PriorityRemarks"].ToString();
                lblMarketedBy.Text = ds.Rows[0]["MarketedBy"].ToString();
            }
            BtnReject.Visible = true;
            BtnAccept.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BtnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            objBE.MemoId = ddlmemo.SelectedValue;
            objBE.SampleID = ddlsample.SelectedValue;
            objBE.UserId = Session["ssoCode"].ToString();
            objBE.COAction = "ACCEPT";
            objBE.Action = "ACTION";
            objBE.status = "6";
            dt = Objrdl.SampleRegister(objBE, con);

            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Update Fail");
            }
            else
            {
                cf.ShowAlertMessage("Sample Accepted Successfully");
                ddlsample.SelectedIndex = 0;
                BindMemos();
                ViewDiv.Visible = false;
                ddlmemo.Focus();
            }
            BtnReject.Visible = false;
            BtnAccept.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BtnReject_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtReasons.Text == "")
            {
                cf.ShowAlertMessage("Please Enter Reject Reasons");
            }
            else
            {
                objBE.MemoId = ddlmemo.SelectedValue;
                objBE.SampleID = ddlsample.SelectedValue;
                objBE.UserId = Session["ssoCode"].ToString();
                objBE.Remarks = txtReasons.Text;
                objBE.COAction = "REJECT";
                objBE.Action = "ACTION";
                objBE.status = "7";
                dt = Objrdl.SampleRegister(objBE, con);

                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage("Update Fail");
                }
                else
                {
                    cf.ShowAlertMessage("Sample Rejected.");
                    ViewDiv.Visible = false;
                    ddlsample.SelectedIndex = 0;
                    ddlmemo.ClearSelection();
                    ddlsample.ClearSelection();
                    BindMemos();
                    BtnReject.Visible = false;
                }
                BtnReject.Visible = false;
                BtnAccept.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
   
    //protected void Rdbaccept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Rdbaccept.SelectedValue == "1")
    //    {
    //        BtnAccept.Visible = true;
    //        BtnReject.Visible = false;
    //        regreasonid.Visible = false;
    //    }
    //    if (Rdbaccept.SelectedValue == "0")
    //    {
    //        BtnAccept.Visible = false;
    //        BtnReject.Visible = true;
    //        regreasonid.Visible = true;
    //    }
    //}
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