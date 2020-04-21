using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DI_EditSample_DI : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    Registration_DL Regobjdl = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Registration_BE objR = new Registration_BE();
    Validate objValidate = new Validate();
    DataTable dt;
    string con, user, state, Department, dicode, DI_AO_Code;
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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "1")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            DI_AO_Code = Session["DI_AO_Code"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["DIZoneNm"].ToString() + " -  " + Session["DIName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindGrid();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    Response.Redirect("../Error.aspx"); 
                }
            }
        }
        else
            Response.Redirect("../Error.aspx");
    }
    protected void BindGrid()
    {
        objR.Action = "R";
        objR.DI_AO_Code = DI_AO_Code;
       
        dt = Regobjdl.SampleRegistrationDI(objR, con);
        if (dt.Rows.Count > 0)
        {
            Gvfreeze.DataSource = dt;
            Gvfreeze.DataBind();
        }
    }
    protected void BindClass()
    {
        objBE.Action = "CLS";
        objBE.Id = Department;
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlClass, dt, "class_name", "class_id", "0");
    }
    protected void BindPriority()
    {
        objBE.Action = "PRI";
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlpriority, dt, "priority", "priority_id", "0");
    }
    protected void BindCategory()
    {
        objBE.Action = "CAT";
        objBE.Id = Department;
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlCategory, dt, "category_name", "category_id", "0");
    }
    protected void BindPaymentType()
    {
        objBE.Action = "PAY";
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlpmttype, dt, "PaymentType", "PaymentType_ID", "0");
    }
    protected void BindFirmTypes()
    {
        objBE.Action = "TYPE";
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlFirmType, dt, "TypeName", "Type_ID", "0");
    }
    protected void BindState()
    {
        objBE.Action = "S";
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddlstate, dt, "State", "StateCode", "0");
        cf.BindDropDownLists(ddlmnstate, dt, "State", "StateCode", "0");
        cf.BindDropDownLists(ddlMarkerState, dt, "State", "StateCode", "0");
    }
    protected void BindDistrict()
    {
        objBE.Action = "D";
        objBE.statecode = ddlstate.SelectedValue; ;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddldist, dt, "DistName", "DistCode", "0");
        ddlmandl.Items.Clear();
    }
    protected void BindMandal()
    {
        objBE.Action = "M";
        objBE.DistCode = ddldist.SelectedValue;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddlmandl, dt, "MandName", "MandCode", "0");
    }   
   
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDistrict();
    }
    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMandal();
    }
    
    protected void txtdtexpry_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime ManfDate = Convert.ToDateTime(txtManufDt.Text);
            DateTime ExpireDate = Convert.ToDateTime(txtdtexpry.Text);

            string mfmonth = ManfDate.Month.ToString();

            if (mfmonth.Length == 1)
                mfmonth = "0" + mfmonth;

            Int64 mandt = Convert.ToInt64(ManfDate.Year.ToString() + mfmonth);

            string expmonth = ExpireDate.Month.ToString();
            if (expmonth.Length == 1)
                expmonth = "0" + expmonth;

            Int64 expdt = Convert.ToInt64(ManfDate.Year.ToString() + expmonth);

            if (expdt < mandt)
            {
                cf.ShowAlertMessage("Date of Expiry  Can not be less than Manufacturing Date");
                txtdtexpry.Text = "";
                txtdtexpry.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
   
    protected void Gvfreeze_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsmpleid.Text = ((Label)(gvrow.FindControl("lblsmid"))).Text;
                lblSampleno.Visible = true;
                lblSampleno.Text = lblsmpleid.Text;              
                pnlsr.Visible = true;
                BindData();
            }
            if (e.CommandName == "DLT")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsmpleid.Text = ((Label)(gvrow.FindControl("lblsampleregid"))).Text;
                objR.SRegid = lblsmpleid.Text;
                objR.Action = "D";
                dt = Regobjdl.SampleRegistration(objR, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Delete Failed");
                else
                    cf.ShowAlertMessage("Delete Succusfully");
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }
    protected void BindData()
    {
        try
        {
            DataTable dtt = new DataTable();
            objR.Action = "R";
            objR.DI_AO_Code = DI_AO_Code;
            objR.SRegid = lblsmpleid.Text;
            
            dtt = Regobjdl.SampleRegistrationDI(objR, con);
            if (dtt.Rows.Count > 0)
            {
                BindClass();
                ddlClass.SelectedValue = dtt.Rows[0]["Sample_Class"].ToString().Trim();              

                ddlusage.SelectedValue = dtt.Rows[0]["Usage"].ToString();
                BindPaymentType();
                ddlpmttype.SelectedValue = dtt.Rows[0]["PymnttypId"].ToString();
                BindPriority();
                ddlpriority.SelectedValue = dtt.Rows[0]["Priority"].ToString();
                if (dtt.Rows[0]["Remarks"].ToString() != "")
                {
                    txtremarks.Text = dtt.Rows[0]["Remarks"].ToString();
                    txtremarks.Enabled = true;
                }
                BindFirmTypes();
                ddlFirmType.SelectedValue = dtt.Rows[0]["Firm_Type"].ToString();
                txtfirnm.Text = dtt.Rows[0]["Firm_Name"].ToString();
                txtlcnno.Text = dtt.Rows[0]["LicenceNo"].ToString();
                txtvlddt.Text = dtt.Rows[0]["Validity"].ToString();
                txtContactPerson.Text = dtt.Rows[0]["ContactPerson"].ToString();
             
                BindState();
                ddlstate.SelectedValue = dtt.Rows[0]["State"].ToString();

                ddldist.SelectedValue = dtt.Rows[0]["District"].ToString();
                BindDistrict();

                ddlmandl.SelectedValue = dtt.Rows[0]["Mandal"].ToString();
                BindMandal();
                txthsno.Text = dtt.Rows[0]["HouseNo"].ToString();
                txtlocality.Text = dtt.Rows[0]["Locality"].ToString();
                txtmbno.Text = dtt.Rows[0]["Mobile"].ToString();
               
                txtsampldate.Text = dtt.Rows[0]["SampleCollectingDate"].ToString();
                BindCategory();
                ddlCategory.SelectedValue = dtt.Rows[0]["SampleCategory"].ToString();

                txtGenericNm.Text = dtt.Rows[0]["GenericName"].ToString();
                txtTradeNm.Text = dtt.Rows[0]["TradeName"].ToString();
                txtbatchno.Text = dtt.Rows[0]["BatchNo"].ToString();
                txtCompostion.Text = dtt.Rows[0]["Composition"].ToString();
                txtManufDt.Text = dtt.Rows[0]["ManufacturingDate"].ToString();
                txtdtexpry.Text = dtt.Rows[0]["ExpiryDate"].ToString();
                txtStkRcvdDt.Text = dtt.Rows[0]["StkRcvdDate"].ToString();
                txtqty.Text = dtt.Rows[0]["SampleQty"].ToString();
                ddlmnstate.SelectedValue = dtt.Rows[0]["ManufacturerState"].ToString();
                txtmnfname.Text = dtt.Rows[0]["ManfucaturerName"].ToString();
                txtmnflcnno.Text = dtt.Rows[0]["ManufacutrerLicence"].ToString();
                txtmnaddress.Text = dtt.Rows[0]["ManufAddress"].ToString();
                txtMarketerName.Text = dtt.Rows[0]["MarketerName"].ToString();
                txtMarkerAddress.Text = dtt.Rows[0]["MarketerAddress"].ToString();
                if(dtt.Rows[0]["MarketerState"].ToString()!="0")
                    ddlMarkerState.SelectedValue = dtt.Rows[0]["MarketerState"].ToString();
                txtqtyPickd.Text = dtt.Rows[0]["QtyPicked"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (Validate_Save())
            {
                objR.DI_AO_Code = dicode;
                objR.SampleClass = ddlClass.SelectedValue;
                objR.Usage = ddlusage.SelectedValue;
                objR.Priority = ddlpriority.SelectedValue;
                objR.Paymenttype = ddlpmttype.SelectedValue;
                objR.PriorityRemarks = txtremarks.Text.Trim();
                objR.FirmType = ddlFirmType.SelectedValue;
                objR.Firm_Name = txtfirnm.Text.Trim();
                objR.LicenceNo = txtlcnno.Text.Trim();
                if (txtvlddt.Text != "")
                    objR.Validity = cf.Texttodateconverter(txtvlddt.Text.Trim());
                objR.ContactPerson = txtContactPerson.Text.Trim();
                objR.Mobile = txtmbno.Text.Trim();
                objR.qtyPicked = txtqtyPickd.Text.Trim();
                objR.state = state;
                objR.District = ddldist.SelectedValue;
                objR.Mandal = ddlmandl.SelectedValue;
                objR.HouseNo = txthsno.Text.Trim();
                objR.Locality = txtlocality.Text.Trim();
                objR.SampleCategory = ddlCategory.SelectedValue;
                objR.SmplCollectingDt = cf.Texttodateconverter(txtsampldate.Text.Trim());
                objR.GenericName = txtGenericNm.Text.Trim();
                objR.TradeName = txtTradeNm.Text.Trim();
                objR.BatchNo = txtbatchno.Text.Trim();
                objR.ManufacturingDate = DateTime.Parse(txtManufDt.Text).ToString("yyyy-MM");
                objR.ExpiryDate = DateTime.Parse(txtdtexpry.Text).ToString("yyyy-MM");
                objR.StkRcvdDate = cf.Texttodateconverter(txtStkRcvdDt.Text.Trim());
                objR.SampleQty = txtqty.Text.Trim();
                objR.Composition = txtCompostion.Text.Trim();
                objR.ManufacturerState = ddlmnstate.SelectedValue;

                objR.ManfucaturerName = txtmnfname.Text.Trim();
                objR.ManufacutrerLicence = txtmnflcnno.Text.Trim();
                objR.ManufAddress = txtmnaddress.Text.Trim();

                objR.MarketerState = ddlMarkerState.SelectedValue;
                objR.MarketerName = txtMarketerName.Text;
                objR.MarketerAddress = txtMarkerAddress.Text.Trim();
                objR.login_state = state;
                objR.dept = Department;
                objR.user = user;
                objR.Action = "U";
                objR.SRegid = lblSampleno.Text.Trim();
                dt = Regobjdl.SampleRegistrationDI(objR, con);
            }
            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage("Update failed, please try again");
            else
                cf.ShowAlertMessage("Your data has been successfully Updated");
            Clear();
            pnlsr.Visible = false;
            BindGrid();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    protected void Clear()
    {
        ddlClass.ClearSelection();
        ddlCategory.SelectedIndex = 0;
        ddlusage.SelectedIndex = 0;
        ddlpmttype.SelectedIndex = 0;
        ddlFirmType.SelectedIndex = 0;
        txtremarks.Text = "";
        txtfirnm.Text = "";
        txtContactPerson.Text = ""; ;
        txtlcnno.Text = "";
        txtvlddt.Text = "";
        // txtStkPstn.Text = "";
        //  ddlstate.SelectedIndex = 0;
        ddldist.SelectedIndex = 0;
        //ddldvsn.SelectedIndex = 0;
        ddlmandl.SelectedIndex = 0;
        txthsno.Text = "";
        txtlocality.Text = "";
        //txtadhar.Text = "";
        txtmbno.Text = "";
        //txtemail.Text = "";
        txtsampldate.Text = "";

        //ddlsampleType.SelectedIndex = 0;
        //ddlSample.SelectedIndex = 0;
        txtGenericNm.Text = "";
        txtTradeNm.Text = "";
        txtqty.Text = "";
        txtbatchno.Text = "";
        txtManufDt.Text = "";
        txtdtexpry.Text = "";
        txtStkRcvdDt.Text = "";
        //txtStkRcvdFrm.Text = "";
        txtContactPerson.Text = "";
        //txtfaiPrice.Text = "";
        //txtinvoiceNo.Text = "";
        txtCompostion.Text = "";
        txtmnflcnno.Text = "";
        //txtinvdate.Text = "";
        txtmnfname.Text = "";
        txtlcnno.Text = "";
        txtmnaddress.Text = "";
        ddlmnstate.SelectedIndex = 0;

        //Rdbpunchanama.SelectedIndex = 0;

    }
    protected bool Validate_Save()
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Sample Category");
            ddlCategory.Focus();
            return false;
        }
        if (ddlusage.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Usage");
            ddlusage.Focus();
            return false;
        }
        if (ddlpmttype.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Payment Type");
            ddlpmttype.Focus();
            return false;
        }
        if (ddlFirmType.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Firm Type");
            ddlFirmType.Focus();
            return false;
        }
        if (txtfirnm.Text == "")
        {
            cf.ShowAlertMessage("Enter Name of the Firm");
            txtfirnm.Focus();
            return false;
        }
        if (txtfirnm.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtfirnm.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtContactPerson.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtContactPerson.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtlcnno.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtlcnno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }        
        if (ddldist.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select District");
            ddldist.Focus();
            return false;
        }
        if (ddlmandl.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Mandal");
            ddlmandl.Focus();
            return false;
        }
        if (txthsno.Text == "")
        {
            cf.ShowAlertMessage("Please Enter House No");
            txthsno.Focus();
            return false;
        }

        if (txthsno.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txthsno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtlocality.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Locality/Road Name");
            txtlocality.Focus();
            return false;
        }

        if (txtlocality.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtlocality.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtsampldate.Text.Trim() != "")
        {

            bool val;
            val = obj.CheckInput_new(txtsampldate.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");

            if (!objValidate.IsDate(txtsampldate.Text.Trim()))
            {
                cf.ShowAlertMessage("Enter Sample Date");
                txtsampldate.Focus();
                return false;
            }
        }


        if (txtGenericNm.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Generic Name");
            txtGenericNm.Focus();
            return false;
        }

        if (txtGenericNm.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtGenericNm.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtTradeNm.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Trade Name");
            txtTradeNm.Focus();
            return false;
        }

        if (txtTradeNm.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtTradeNm.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtqty.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Sample Qty. sent for analysis");
            txtqty.Focus();
            return false;
        }

        if (txtqty.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtqty.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtbatchno.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Batch No");
            txtbatchno.Focus();
            return false;
        }

        if (txtbatchno.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtbatchno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        
        if (txtmnfname.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Name of Manufacturer / Importer");
            txtmnfname.Focus();
            return false;
        }
        if (txtmnfname.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtmnfname.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtmnflcnno.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtmnflcnno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtmnflcnno.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Manufacturer / Importer License No ");
            txtmnflcnno.Focus();
            return false;
        }
        if (txtmnaddress.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtmnflcnno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtmnaddress.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Address ");
            txtmnflcnno.Focus();
            return false;
        }
        if (ddlmnstate.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Manufacturer State ");
            ddlmnstate.Focus();
            return false;
        }

        return true;
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