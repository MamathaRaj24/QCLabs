using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DEO_EnterSampleDetails : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Master_BE mbe = new Master_BE();
    Masters objm = new Masters();
    Registration_DL objRDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, deocode, dept;

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
        
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "2")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            deocode = Session["DeoCode"].ToString();

                       
            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString() + "-" + Session["DeoName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindMemos();
                    BindGrid();
                    BindCategory();
                    BindSampleSent();
                    BindPriority();
                    //BindSampeltype();
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
        objBE.UserId = deocode;
        objBE.Action = "MEMO";
        dt = objRDL.SampleRegister(objBE, con);
        cf.BindDropDownLists(ddlmemo, dt, "Memo_ID", "Memo_ID", "Select");
    }

    protected void BindCategory()
    {
        mbe.Id = dept;
        mbe.Action = "CAT";
        dt = objm.Getdetails(mbe, con);
        cf.BindDropDownLists(ddlcatgry, dt, "category_name", "category_id", "0");
    }

    protected void BindPriority()
    {
        mbe.Action = "PRI";
        dt = objm.Getdetails(mbe, con);
        cf.BindDropDownLists(ddlpriority, dt, "priority", "priority_id", "0");
    }

    protected void BindSampleSent()
    {
        objBE.Action = "DI";
        objBE.dept = dept;
        dt = objRDL.SampleRegister(objBE, con);
        cf.BindDropDownLists(ddlDiName, dt, "NAME", "User_Code", "0");
    }
    public void BindSample()
    {
        dt = new DataTable();
        objBE.MemoId = ddlmemo.SelectedValue;
        objBE.dept = dept;
        objBE.Action = "SMPL";
        dt = objRDL.SampleRegister(objBE, con);
        cf.BindDropDownLists(ddlsample, dt, "RegID", "RegID", "Select");
        // cf.BindDropDownLists(ddlsample, dt, "Sample_RegID", "RegID", "Select");
    }
    public void BindGrid()
    {
        try
        {
            objBE.Action = "R";
            objBE.MemoId = "";
            objBE.SampleID = "";
            dt = objRDL.SampleRegister(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvSamples.DataSource = dt;
                GvSamples.DataBind();
            }
            else
            {
                GvSamples.DataSource = null;
                GvSamples.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }
    public bool Validate()
    {
        if (ddlmemo.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Memo ID");
            ddlmemo.Focus();
            return false;
        }
        if (ddlsample.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Memo ID");
            ddlsample.Focus();
            return false;
        }
        if (txtRecvdDate.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Received Date");
            txtRecvdDate.Focus();
            return false;
        }
        if (ddlDiName.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Sample Name");
            ddlDiName.Focus();
            return false;
        }
        if (ddlcatgry.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Category");
            ddlcatgry.Focus();
            return false;
        }
        if (ddlusage.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Usage");
            ddlusage.Focus();
            return false;
        }
        if (ddlpriority.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Priority");
            ddlpriority.Focus();
            return false;
        }
        if (txtgnrname.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Generic Name");
            txtgnrname.Focus();
            return false;
        }
        if (txtqty.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Quantity");
            txtqty.Focus();
            return false;
        }
        //if (txtManu.Text == "")
        //{
        //    cf.ShowAlertMessage("Please Enter Manufacture Name");
        //    txtManu.Focus();
        //    return false;
        //}
        if (txtdtmn.Text == "")
        {
            cf.ShowAlertMessage(" Enter Manufacture Date");
            txtdtmn.Focus();
            return false;
        }
        if (txtCompostion.Text == "")
        {
            cf.ShowAlertMessage(" Enter Composition");
            txtCompostion.Focus();
            return false;
        }
        if (txtdtexpry.Text == "")
        {
            cf.ShowAlertMessage(" Enter Expire Date");
            txtdtexpry.Focus();
            return false;
        }
        return true;
    }

    public void Clear()
    {
        ddlmemo.ClearSelection();
        ddlsample.ClearSelection();
        ddlDiName.SelectedIndex = 0;
        ddlcatgry.SelectedIndex = 0;
        txtRecvdDate.Text = "";
        ddlusage.SelectedIndex = 0;
        ddlpriority.SelectedIndex = 0;
        txtTradeName.Text = "";
        txtremarks.Text = "";
        txtgnrname.Text = "";
        txtqty.Text = "";
        //txtBatchno.Text = "";
        txtManu.Text = "";
        txtLicenseNo.Text = "";
        txtMrktBy.Text = "";
        txtdtmn.Text = "";
        txtCompostion.Text = "";
        txtdtexpry.Text = "";
        ddlusage.ClearSelection();

    }
    protected void ddlmemo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSample();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    protected void btnAck_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (Validate())
            {
                dt = new DataTable();
                objBE.MemoId = ddlmemo.SelectedValue;
                objBE.SampleID = ddlsample.SelectedValue.ToString();
                objBE.dept = dept;
                objBE.ReceiptDate = cf.Texttodateconverter(txtRecvdDate.Text);
                objBE.DI_AO_Code = ddlDiName.SelectedValue;
                objBE.SampleCategory = ddlcatgry.SelectedValue;
                objBE.Usage = ddlusage.SelectedItem.Text;
                objBE.Priority = ddlpriority.SelectedValue;
                objBE.PriorityRemarks= txtremarks.Text;
               // objBE.SampleType = ddlthrctgry.Text;
                objBE.DrugName = txtTradeName.Text.Trim();
                objBE.GenericName = txtgnrname.Text;
                objBE.SampleQty = txtqty.Text;
                //objBE.BatchNo = txtBatchno.Text.Trim();
                objBE.ManfucaturerName = txtManu.Text.Trim();
                objBE.LicenceNo = txtLicenseNo.Text.Trim();
                objBE.DealerName = txtMrktBy.Text.Trim();
                objBE.ManufacturingDate = DateTime.Parse(txtdtmn.Text).ToString("yyyy-MM");
                objBE.Composition = txtCompostion.Text.Trim();
                objBE.ExpiryDate = DateTime.Parse(txtdtexpry.Text).ToString("yyyy-MM");
                objBE.UserId = Session["DeoCode"].ToString();
                objBE.Action = "I";
                dt = objRDL.SampleRegister(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("Saved");
                    Clear();
                }
                BindGrid();
                BindMemos();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

   

    protected void GvSamples_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
           
            lblmemoid.Text = ((Label)(gvrow.FindControl("lblMemoId"))).Text;
            lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleId"))).Text;
          
            txtRecvdDate.Text = ((Label)(gvrow.FindControl("lblReceiveDate"))).Text;
            BindSampleSent();
            ddlDiName.SelectedValue = ((Label)(gvrow.FindControl("lblSentBy"))).Text;
            lbldiname.Text = ((Label)(gvrow.FindControl("lbldruguserid"))).Text;
            BindCategory();
            ddlcatgry.SelectedValue = ((Label)(gvrow.FindControl("lblCategoryId"))).Text;
            ddlusage.SelectedItem.Text = ((Label)(gvrow.FindControl("lblusage"))).Text;
            BindPriority();
            ddlpriority.SelectedValue = ((Label)(gvrow.FindControl("lblPriority"))).Text;
            txtremarks.Text = ((Label)(gvrow.FindControl("lblRemarks"))).Text;
            txtTradeName.Text = ((Label)(gvrow.FindControl("lblDrugName"))).Text;
            txtgnrname.Text = ((Label)(gvrow.FindControl("lblGeneric"))).Text;
            txtqty.Text = ((Label)(gvrow.FindControl("lblQuantity"))).Text;
           // txtBatchno.Text = ((Label)(gvrow.FindControl("lblBatchNo"))).Text;
            txtManu.Text = ((Label)(gvrow.FindControl("lblManuBy"))).Text;
            txtLicenseNo.Text = ((Label)(gvrow.FindControl("lblMfgLicenceNo"))).Text;
            txtMrktBy.Text = ((Label)(gvrow.FindControl("lblMarketBy"))).Text;
            txtdtmn.Text = ((Label)(gvrow.FindControl("lblManuDate"))).Text;
            txtdtexpry.Text = ((Label)(gvrow.FindControl("lblExpireDate"))).Text;
            txtCompostion.Text = ((Label)(gvrow.FindControl("lblComposition"))).Text;
           
            lblmemoid.Text = ((Label)(gvrow.FindControl("lblMemoId"))).Text.Trim();
            lblmemoid.Visible = true;
            ddlmemo.Visible = false;

            lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleId"))).Text.Trim();
            lblsampleid.Visible = true;
            ddlsample.Visible = false;

            btnAck.Visible = false;
            btnUpdate.Visible = true;

        }
        if (e.CommandName == "dlt")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            lblmemoid.Text = ((Label)(gvrow.FindControl("lblMemoId"))).Text;
            lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleId"))).Text;
            objBE.MemoId = lblmemoid.Text;
            objBE.SampleID = lblsampleid.Text;
            objBE.Action = "D";
            dt = objRDL.SampleRegister(objBE, con);
            if (dt.Rows.Count > 0)
            {
                BindGrid();
                cf.ShowAlertMessage(dt.Rows[0][0].ToString());
            }
            else
            {
                BindGrid();
                cf.ShowAlertMessage("Saved");
                Clear();
            }

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (ValidateUp())
            {

                dt = new DataTable();
                objBE.MemoId = lblmemoid.Text;
                objBE.SampleID = lblsampleid.Text;
                objBE.dept = dept;
                objBE.ReceiptDate = cf.Texttodateconverter(txtRecvdDate.Text);
                objBE.DI_AO_Code = lbldiname.Text;
                objBE.SampleCategory = ddlcatgry.SelectedValue;
                objBE.Usage = ddlusage.SelectedItem.Text;
                objBE.Priority = ddlpriority.SelectedValue;
                objBE.PriorityRemarks = txtremarks.Text;
               // objBE.SampleType = ddlthrctgry.Text;
               // objBE.DrugName = ddldrug.SelectedValue;
                objBE.GenericName = txtgnrname.Text;
                objBE.SampleQty = txtqty.Text;
                //objBE.BatchNo = txtBatchno.Text.Trim();
                objBE.ManfucaturerName = txtManu.Text.Trim();
                objBE.LicenceNo = txtLicenseNo.Text.Trim();
                objBE.DealerName = txtMrktBy.Text.Trim();
                objBE.ManufacturingDate = DateTime.Parse(txtdtmn.Text).ToString("yyyy-MM");
                objBE.Composition = txtCompostion.Text.Trim();
                objBE.ExpiryDate = DateTime.Parse(txtdtexpry.Text).ToString("yyyy-MM");
                objBE.UserId = Session["DeoCode"].ToString();
                objBE.Action = "U";
                dt = objRDL.SampleRegister(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    BindGrid();
                    cf.ShowAlertMessage("Update Failed");
                    Clear();
                }
                else
                {
                    BindGrid();
                    cf.ShowAlertMessage("Sample Updated Succussfully");
                   
                    Clear();
                }
                btnAck.Visible = true;
                btnUpdate.Visible = false;
                ddlmemo.Enabled = true;
                ddlsample.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }
    public bool ValidateUp()
    {
        //if (ddlmemo.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Please Select Memo ID");
        //    ddlmemo.Focus();
        //    return false;
        //}
        //if (ddlsample.SelectedIndex == 0)
        //{
        //    cf.ShowAlertMessage("Please Select Memo ID");
        //    ddlsample.Focus();
        //    return false;
        //}
        if (txtRecvdDate.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Received Date");
            txtRecvdDate.Focus();
            return false;
        }
        if (ddlDiName.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Sample Name");
            ddlDiName.Focus();
            return false;
        }
        if (ddlcatgry.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Category");
            ddlcatgry.Focus();
            return false;
        }
        if (ddlusage.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Usage");
            ddlusage.Focus();
            return false;
        }
        if (ddlpriority.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Priority");
            ddlpriority.Focus();
            return false;
        }
        if (ddlpriority.SelectedValue == "High")
        {
            if (txtremarks.Text == "")
            {
                cf.ShowAlertMessage("Please Enter Remarks");
                txtremarks.Focus();
                return false;
            }
        }
       
        if (txtgnrname.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Generic Name");
            txtgnrname.Focus();
            return false;
        }
        if (txtqty.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Quantity");
            txtqty.Focus();
            return false;
        }
        //if (txtBatchno.Text == "")
        //{
        //    cf.ShowAlertMessage("Please Enter Batch No");
        //    txtBatchno.Focus();
        //    return false;
        //}
        if (txtManu.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Manufacture Name");
            txtManu.Focus();
            return false;
        }
        if (txtLicenseNo.Text == "")
        {
            cf.ShowAlertMessage("Please Enter License No");
            txtLicenseNo.Focus();
            return false;
        }
        if (txtMrktBy.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Marketed Name");
            txtManu.Focus();
            return false;
        }
        //if (txtdtmn.Text == "")
        //{
        //    cf.ShowAlertMessage(" Enter Manufacture Date");
        //    txtdtmn.Focus();
        //    return false;
        //}
        if (txtCompostion.Text == "")
        {
            cf.ShowAlertMessage("Enter Composition");
            txtCompostion.Focus();
            return false;
        }
        //if (txtdtexpry.Text == "")
        //{
        //    cf.ShowAlertMessage(" Enter Expire Date");
        //    txtdtexpry.Focus();
        //    return false;
        //}
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