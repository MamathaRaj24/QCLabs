﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class AO_Pesticides_Reg : System.Web.UI.Page
{

    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriDL Regobjdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objR = new AgriBE();
    Validate objValidate = new Validate();
    DataTable dt;
    string con, user, state, Department, Aocode;
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

        
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "8" || Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

            Aocode = Session["AO_Code"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DistName"].ToString() + " -  " + Session["MandName"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                try
                {
                    random();
                    
                    BindClass();
                    BindState();
                    BindSampeltype();

                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    cf.ShowAlertMessage(ex.ToString());
                }
            }
        }
    }
    protected void BindClass()
    {
        objBE.Action = "CLS";
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlClass, dt, "class_name", "class_id", "0");
    }
    protected void BindState()
    {
        objBE.Action = "S";
        dt = ObjDL.GetLocations(objBE, con);

        cf.BindDropDownLists(ddlstate, dt, "State", "StateCode", "0");
    }
    protected void BindDistrict()
    {
        objBE.Action = "D";
        objBE.statecode = state;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddldist, dt, "DistName", "DistCode", "0");
    }
    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMandal();
    }
    protected void BindMandal()
    {
        objBE.Action = "M";
        objBE.DistCode = ddldist.SelectedValue;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddlmandal, dt, "MandName", "MandCode", "0");
    }
    protected void BindSampeltype()
    {
        objBE.Action = "R";
        objBE.Dept = Department;
        objBE.CatId = Session["SampleCategory"].ToString();
        dt = ObjDL.SampleTypeIUDR(objBE, con);
        cf.BindDropDownLists(ddlsampleType, dt, "SampleTypeName", "SampleTypeCode", "0");
    }
    protected void BindSampel()
    {
        objBE.Action = "R";
        objBE.Dept = Department;
        objBE.SampleTypeId = ddlsampleType.SelectedValue;
        dt = ObjDL.SampleIUDR(objBE, con);
        cf.BindDropDownLists(ddlSample, dt, "SampleName", "SampleCode", "0");
    }
    protected void ddlsampleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSampel();
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDistrict();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            check();
            if (Validate_Save())
            {
                objR.DI_AO_Code = Aocode;
                objR.SampleClass = ddlClass.SelectedValue;
                objR.CodeSticker = txtcdastkr.Text;
                objR.Firm_Name = txtfirnm.Text.Trim();
                objR.LicenceNo = txtlcnno.Text.Trim();

                objR.ContactPerson = txtnmdlr.Text.Trim();
                if (txtvlddt.Text != "")
                    objR.Validity = cf.Texttodateconverter(txtvlddt.Text.Trim());
                objR.state = state;
                objR.District = ddldist.SelectedValue;
                objR.Mandal = ddlmandal.SelectedValue;
                objR.HouseNo = txthsno.Text.Trim();
                objR.Locality = txtlocality.Text.Trim();
                objR.SmplCollectingDt = cf.Texttodateconverter(txtsampldate.Text.Trim());
                objR.SampleType = ddlsampleType.SelectedValue;
                objR.SampleID = ddlSample.SelectedValue;
                objR.Gaurentees_AI = txtgrntdAI.Text.Trim();
                objR.TradeName = txtTradeNm.Text.Trim();
                objR.SampleQty = txtsdnlsis.Text.Trim();
                objR.BatchNo = txtbatchno.Text.Trim();
                objR.StockPosition = txtstockpostion.Text.Trim();
                objR.ManufacturingDate = cf.Texttodateconverter(txtManufDt.Text.Trim());
                objR.ExpiryDate = cf.Texttodateconverter(txtdtexpry.Text.Trim());
                objR.InvoiceNo = txtinvoiceNo.Text.Trim();
                objR.InvoiceDate = cf.Texttodateconverter(txtinvdate.Text.Trim());
                objR.StkRcvdDate = cf.Texttodateconverter(txtdtrcp.Text.Trim());
                objR.StkRcvdFrom = txtStkRcvdFrm.Text.Trim();

                objR.ManfucaturerName = txtmnname.Text.Trim();
                objR.Address = txtmnaddress.Text.Trim();
                objR.ISImark = RdbIsimark.SelectedValue;
                objR.MarketerName = txtmrketername.Text.Trim();
                objR.punchnama = Rdbpunchanama.SelectedValue;
                objR.login_state = state;
                objR.dept = Department;
                objR.user = user;
                objR.SampleCategory = Session["SampleCategory"].ToString();
                objR.Action = "I";

                dt = Regobjdl.PesticideRegistration(objR, con);

                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage("Save failed, please try again");
                    //  Clear();
                }
                else
                {
                    cf.ShowAlertMessage("Your data has been successfully saved");
                    //Clear();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected bool Validate_Save()
    {
        if (ddlClass.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Sample Class");
            ddlClass.Focus();
            return false;
        }
        if (txtcdastkr.Text == "")
        {
            cf.ShowAlertMessage("Enter Name of the Firm");
            txtcdastkr.Focus();
            return false;
        }
        if (txtcdastkr.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtcdastkr.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
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
        if (txtnmdlr.Text == "")
        {
            cf.ShowAlertMessage("Enter Name of the Dealer");
            txtnmdlr.Focus();
            return false;
        }
        if (txtnmdlr.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtnmdlr.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtlcnno.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Licence No");
            txtlcnno.Focus();
            return false;
        }
        if (txtlcnno.Text != "")
        {
            bool val;

            val = obj.CheckInput_new(txtlcnno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        //if (txtvlddt.Text.Trim() != "")
        //{

        //    bool val;
        //    val = obj.CheckInput_new(txtvlddt.Text);
        //    if (val == true)
        //        Response.Redirect("~/Error.aspx");

        //    if (!objValidate.IsDate(txtvlddt.Text.Trim()))
        //    {
        //        cf.ShowAlertMessage("Enter Valid  Date");
        //        txtvlddt.Focus();
        //        return false;
        //    }
        //}

        //if (ddlstate.SelectedValue == "0")
        //{
        //    cf.ShowAlertMessage("Select State");
        //    ddlstate.Focus();
        //    return false;
        //}


        if (ddldist.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select District");
            ddldist.Focus();
            return false;
        }
        //if (ddldvsn.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Select Division");
        //    ddldvsn.Focus();
        //    return false;
        //}
        //if (ddlmandal.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Select Mandal");
        //    ddlmandal.Focus();
        //    return false;
        //}
        //if (txtvlddt.Text.Trim() != "")
        //{

        //    bool val;
        //    val =obj.CheckInput_new(txtvlddt.Text);
        //    if (val == true)
        //        Response.Redirect("~/Error.aspx");

        //    if (!objValidate.IsDate(txtvlddt.Text.Trim()))
        //    {
        //        cf.ShowAlertMessage("Enter Valid Date");
        //        txtvlddt.Focus();
        //        return false;
        //    }
        //}
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
        //if (ddldosage.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Select Dosage");
        //    ddldosage.Focus();
        //    return false;
        //}
        if (ddlsampleType.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Therapeutic Category");
            ddlsampleType.Focus();
            return false;
        }
        if (ddlSample.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Name of Sample/Drug");
            ddlSample.Focus();
            return false;
        }
        if (txtgrntdAI.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Generic Name");
            txtgrntdAI.Focus();
            return false;
        }

        if (txtgrntdAI.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtgrntdAI.Text);
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
        if (txtsdnlsis.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Size of Sample drawn for analysis");
            txtTradeNm.Focus();
            return false;
        }

        if (txtsdnlsis.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtsdnlsis.Text);
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
        if (txtstockpostion.Text == "")
        {
            cf.ShowAlertMessage("Please EnterStock Postion");
            txtstockpostion.Focus();
            return false;
        }

        if (txtstockpostion.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtstockpostion.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtManufDt.Text.Trim() != "")
        {

            bool val;
            val = obj.CheckInput_new(txtManufDt.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");

            if (!objValidate.IsDate(txtManufDt.Text.Trim()))
            {
                cf.ShowAlertMessage("Enter Date of Manufacturing");
                txtManufDt.Focus();
                return false;
            }
        }
        if (txtdtexpry.Text.Trim() != "")
        {

            bool val;
            val = obj.CheckInput_new(txtdtexpry.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");

            if (!objValidate.IsDate(txtManufDt.Text.Trim()))
            {
                cf.ShowAlertMessage("Enter Date of Expiry");
                txtdtexpry.Focus();
                return false;
            }
        }
        if (txtdtrcp.Text.Trim() != "")
        {

            bool val;
            val = obj.CheckInput_new(txtdtrcp.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");

            if (!objValidate.IsDate(txtdtrcp.Text.Trim()))
            {
                cf.ShowAlertMessage("Enter Date of Receipt:");
                txtdtrcp.Focus();
                return false;
            }
        }
        if (txtinvoiceNo.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Invoice No");
            txtinvoiceNo.Focus();
            return false;
        }

        if (txtinvoiceNo.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtinvoiceNo.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtinvdate.Text.Trim() != "")
        {

            bool val;
            val = obj.CheckInput_new(txtinvdate.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");

            if (!objValidate.IsDate(txtinvdate.Text.Trim()))
            {
                cf.ShowAlertMessage("Select Invoice Date");
                txtinvdate.Focus();
                return false;
            }
        }
        if (txtdtrcp.Text.Trim() != "")
        {

            bool val;
            val = obj.CheckInput_new(txtdtrcp.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");

            if (!objValidate.IsDate(txtdtrcp.Text.Trim()))
            {
                cf.ShowAlertMessage("Select Invoice Date");
                txtdtrcp.Focus();
                return false;
            }
        }
        if (txtStkRcvdFrm.Text == "")
        {
            cf.ShowAlertMessage("Please Stock Received from whom");
            txtStkRcvdFrm.Focus();
            return false;
        }

        if (txtStkRcvdFrm.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtStkRcvdFrm.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }



        if (txtmnname.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Name of Manufacturer / Importer");
            txtmnname.Focus();
            return false;
        }
        if (txtmnname.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtmnname.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtmnaddress.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtmnaddress.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtmnaddress.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Address ");
            txtmnaddress.Focus();
            return false;
        }
        //if (txtisi.Text != "")
        //{
        //    bool val;
        //    val = obj.CheckInput_new(txtisi.Text);
        //    if (val == true)
        //        Response.Redirect("~/Error.aspx");
        //}
        //if (txtisi.Text == "")
        //{
        //    cf.ShowAlertMessage("Please Enter ISI Mark position(if any) ");
        //    txtisi.Focus();
        //    return false;
        //}
        if (RdbIsimark.SelectedIndex == -1)
        {
            cf.ShowAlertMessage("Please Select ISI Mark");
            RdbIsimark.Focus();
            return false;
        }
        if (txtmrketername.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtmrketername.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtmrketername.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Name of the Marketer ");
            txtmrketername.Focus();
            return false;
        }

        if (Rdbpunchanama.SelectedIndex == -1)
        {
            cf.ShowAlertMessage("Please Select whether Panchanama Conducted ");
            Rdbpunchanama.Focus();
            return false;
        }

        return true;
    }
    protected void Clear()
    {
        txtbatchno.Text = "";
        txtcdastkr.Text = "";
        txtdtexpry.Text = "";
        txtdtrcp.Text = "";
        txtfirnm.Text = "";
        txtgrntdAI.Text = "";
        txthsno.Text = "";
        txtinvdate.Text = "";
        txtinvoiceNo.Text = "";

        txtlcnno.Text = "";
        txtlocality.Text = "";
        txtManufDt.Text = "";
        txtmnaddress.Text = "";
        txtmnname.Text = "";
        txtmrketername.Text = "";
        txtnmdlr.Text = "";
        txtsampldate.Text = "";
        txtsdnlsis.Text = "";
        txtStkRcvdFrm.Text = "";
        txtstockpostion.Text = "";
        txtTradeNm.Text = "";
        txtvlddt.Text = "";
        Rdbpunchanama.ClearSelection();
        RdbIsimark.ClearSelection();
        ddlClass.ClearSelection();
        ddldist.ClearSelection();
        ddlSample.ClearSelection();
        ddlsampleType.ClearSelection();
        ddlstate.ClearSelection();

        //ddldvsn.ClearSelection();
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