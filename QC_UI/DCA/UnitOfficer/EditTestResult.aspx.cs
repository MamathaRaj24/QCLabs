using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;

public partial class DCA_Analyst_ExEdittestresult : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Masters ObjDAL = new Masters();
    Master_BE Obj = new Master_BE();
    DataTable dt;
    SampleSqlInjectionScreeningModule obj = new SampleSqlInjectionScreeningModule();
    string con;

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
        if (Session["UsrName"] != null && Session["RoleID"].ToString().Trim() == "4")
        {
            con = Session["ConnKey"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["UnitOfficerName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            random();
            if (!IsPostBack)
            {
                BindGrid();
                EditTr.Visible = false;
            }
        }
        else
            Response.Redirect("~/Error.aspx");
    }
    protected void BindGrid()
    {
        try
        {
            dt = new DataTable();
            objBE.Action = "EDIT";
            objBE.AnalystId = Session["UnitOfficerCode"].ToString();
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GVTestResult.DataSource = dt;
                GVTestResult.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void Binddescremarks()
    {
        try
        {
            lbllab.Text = Session["Labname"].ToString();
            objBE.Action = "TR";
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.AnalystId = Session["UnitOfficerCode"].ToString();
            DataTable dtt = new DataTable();
            dtt = ObjDL.JAActionEdit(objBE, con);
            if (dtt.Rows.Count > 0)
            {
                lbldrugnm.Text = dtt.Rows[0]["TradeName"].ToString();
                lblmfgdt.Text = dtt.Rows[0]["ManufacturingDate"].ToString();
                lblexpdt.Text = dtt.Rows[0]["ExpiryDate"].ToString();
                lblqty.Text = dtt.Rows[0]["SampleQty"].ToString();
                lblComposition.Text = dtt.Rows[0]["Composition"].ToString();
                txtStartDt.Text = dtt.Rows[0]["Test_start_Dt"].ToString();
                txtEndDt.Text = dtt.Rows[0]["Test_End_Dt"].ToString();
                txtDesc.Text = dtt.Rows[0]["Description"].ToString();
                txtremarks.Text = dtt.Rows[0]["JARemarks"].ToString();
                Edidescrmrks.Visible = true;
                addmore.Visible = false;
                EditTr.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                Btnadd.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindTrGrid()
    {
        try
        {
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.AnalystId = Session["UnitOfficerCode"].ToString();
            lbllab.Text = Session["Labname"].ToString();
            objBE.Action = "TR";
            DataTable dtt = new DataTable();
            dtt = ObjDL.JAActionEdit(objBE, con);
            if (dtt.Rows.Count > 0)
            {               
                BindGrids();
                Gvedittest.DataSource = dtt;
                Gvedittest.DataBind();

                addmore.Visible = false;
                EditTr.Visible = true;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                Btnadd.Visible = true;
                edttrgrid.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindGrids()
    {
        try
        {
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("TestParam", typeof(string));
            dtparam.Columns.Add("Testfor", typeof(string));
            dtparam.Columns.Add("TestName", typeof(string));
            dtparam.Columns.Add("Calculation", typeof(string));
            dtparam.Columns.Add("TestProtocol", typeof(string));
            dtparam.Columns.Add("found", typeof(string));
            dtparam.Columns.Add("claim", typeof(string));
            dtparam.Columns.Add("limit", typeof(string));

            dtparam.Rows.Add();
            Session["dttestresult"] = dtparam;
            GridTr.DataSource = dtparam;
            GridTr.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindTest()
    {
        try
        {
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("TestParam", typeof(string));
            dtparam.Columns.Add("Testfor", typeof(string));
            dtparam.Columns.Add("TestName", typeof(string));
            dtparam.Columns.Add("Calculation", typeof(string));
            dtparam.Columns.Add("TestProtocol", typeof(string));
            dtparam.Columns.Add("found", typeof(string));
            dtparam.Columns.Add("claim", typeof(string));
            dtparam.Columns.Add("limit", typeof(string));

            dtparam.Rows.Add();
            Session["dttestresult"] = dtparam;
            GridTr.DataSource = dtparam;
            GridTr.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    protected void BindS1()
    {
        try
        {
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "RS1";
            dt = new DataTable();
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditS1.Visible = true;
                txtS1calculation1.Text = dt.Rows[0]["s11cal"].ToString();
                txts1F1.Text = dt.Rows[0]["s11f"].ToString();
                txts1Cl1.Text = dt.Rows[0]["s11c"].ToString();
                txts1P1.Text = dt.Rows[0]["s11p"].ToString();
                txtS1calculation2.Text = dt.Rows[0]["s12cal"].ToString();
                txts1F2.Text = dt.Rows[0]["s12f"].ToString();
                txts1Cl2.Text = dt.Rows[0]["s12c"].ToString();
                txts1P2.Text = dt.Rows[0]["s12p"].ToString();
                txtS1calculation3.Text = dt.Rows[0]["s13cal"].ToString();
                txts1F3.Text = dt.Rows[0]["s13f"].ToString();
                txts1Cl3.Text = dt.Rows[0]["s13c"].ToString();
                txts1P3.Text = dt.Rows[0]["s13p"].ToString();
                txtS1calculation4.Text = dt.Rows[0]["s14cal"].ToString();
                txts1F4.Text = dt.Rows[0]["s14f"].ToString();
                txts1Cl4.Text = dt.Rows[0]["s14c"].ToString();
                txts1P4.Text = dt.Rows[0]["s14p"].ToString();
                txtS1calculation5.Text = dt.Rows[0]["s15cal"].ToString();
                txts1F5.Text = dt.Rows[0]["s15f"].ToString();
                txts1Cl5.Text = dt.Rows[0]["s15c"].ToString();
                txts1P5.Text = dt.Rows[0]["s15p"].ToString();
                txtS1calculation6.Text = dt.Rows[0]["s16cal"].ToString();
                txts1F6.Text = dt.Rows[0]["s16f"].ToString();
                txts1Cl6.Text = dt.Rows[0]["s16c"].ToString();
                txts1P6.Text = dt.Rows[0]["s16p"].ToString();
                txts1Remrks.Text = dt.Rows[0]["s1remarks"].ToString();  
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindS2()
    {
        try
        {
            objBE.SampleID = lbls2smpid.Text.Trim();
            objBE.Action = "RS2";
            dt = new DataTable();
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //s2
                EditS2.Visible = true;
                txts2calculation1.Text = dt.Rows[0]["s21cal"].ToString();
                txts2F1.Text = dt.Rows[0]["s21f"].ToString();
                txts2Cl1.Text = dt.Rows[0]["s21c"].ToString();
                txts2P1.Text = dt.Rows[0]["s21p"].ToString();
                txts2calculation2.Text = dt.Rows[0]["s22cal"].ToString();
                txts2F2.Text = dt.Rows[0]["s22f"].ToString();
                txts2Cl2.Text = dt.Rows[0]["s22c"].ToString();
                txts2P2.Text = dt.Rows[0]["s22p"].ToString();
                txts2calculation3.Text = dt.Rows[0]["s23cal"].ToString();
                txts2F3.Text = dt.Rows[0]["s23f"].ToString();
                txts2Cl3.Text = dt.Rows[0]["s23c"].ToString();
                txts2P3.Text = dt.Rows[0]["s23p"].ToString();
                txts2calculation4.Text = dt.Rows[0]["s24cal"].ToString();
                txts2F4.Text = dt.Rows[0]["s24f"].ToString();
                txts2Cl4.Text = dt.Rows[0]["s24c"].ToString();
                txts2P4.Text = dt.Rows[0]["s24p"].ToString();
                txts2calculation5.Text = dt.Rows[0]["s25cal"].ToString();
                txts2F5.Text = dt.Rows[0]["s25f"].ToString();
                txts2Cl5.Text = dt.Rows[0]["s25c"].ToString();
                txts2P5.Text = dt.Rows[0]["s25p"].ToString();
                txts2calculation6.Text = dt.Rows[0]["s26cal"].ToString();
                txts2F6.Text = dt.Rows[0]["s26f"].ToString();
                txts2Cl6.Text = dt.Rows[0]["s26c"].ToString();
                txts2P6.Text = dt.Rows[0]["s26p"].ToString();
                txts2remarks.Text = dt.Rows[0]["s2Remarks"].ToString();         
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindS3()
    {
        try
        {

            objBE.SampleID = lbls3smpid.Text.Trim();
            objBE.Action = "S3R";
            dt = new DataTable();
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S3
                EditS3.Visible = true;
                txts3calculation1.Text = dt.Rows[0]["s31cal"].ToString();
                txts3F1.Text = dt.Rows[0]["s31f"].ToString();
                txts3Cl1.Text = dt.Rows[0]["s31c"].ToString();
                txts3P1.Text = dt.Rows[0]["s31p"].ToString();
                txts3calculation2.Text = dt.Rows[0]["s32cal"].ToString();
                txts3F2.Text = dt.Rows[0]["s32f"].ToString();
                txts3Cl2.Text = dt.Rows[0]["s32c"].ToString();
                txts3P2.Text = dt.Rows[0]["s32p"].ToString();
                txts3calculation3.Text = dt.Rows[0]["s33cal"].ToString();
                txts3F3.Text = dt.Rows[0]["s33f"].ToString();
                txts3Cl3.Text = dt.Rows[0]["s33c"].ToString();
                txts3P3.Text = dt.Rows[0]["s33p"].ToString();
                txts3calculation4.Text = dt.Rows[0]["s34cal"].ToString();
                txts3F4.Text = dt.Rows[0]["s34f"].ToString();
                txts3Cl4.Text = dt.Rows[0]["s34c"].ToString();
                txts3P4.Text = dt.Rows[0]["s34p"].ToString();
                txts3calculation5.Text = dt.Rows[0]["s35cal"].ToString();
                txts3F5.Text = dt.Rows[0]["s35f"].ToString();
                txts3Cl5.Text = dt.Rows[0]["s35c"].ToString();
                txts3P5.Text = dt.Rows[0]["s35p"].ToString();
                txts3calculation6.Text = dt.Rows[0]["s36cal"].ToString();
                txts3F6.Text = dt.Rows[0]["s36f"].ToString();
                txts3Cl6.Text = dt.Rows[0]["s36c"].ToString();
                txts3P6.Text = dt.Rows[0]["s36p"].ToString();

                txts3calculation7.Text = dt.Rows[0]["s37cal"].ToString();
                txts3F7.Text = dt.Rows[0]["s37f"].ToString();
                txts3Cl7.Text = dt.Rows[0]["s37c"].ToString();
                txts3P7.Text = dt.Rows[0]["s37p"].ToString();
                txts3calculation8.Text = dt.Rows[0]["s38cal"].ToString();
                txts3F8.Text = dt.Rows[0]["s38f"].ToString();
                txts3Cl8.Text = dt.Rows[0]["s38c"].ToString();
                txts3P8.Text = dt.Rows[0]["s38p"].ToString();
                txts3calculation9.Text = dt.Rows[0]["s39cal"].ToString();
                txts3F9.Text = dt.Rows[0]["s39f"].ToString();
                txts3Cl9.Text = dt.Rows[0]["s39c"].ToString();
                txts3P9.Text = dt.Rows[0]["s39p"].ToString();
                txts3calculation10.Text = dt.Rows[0]["s310cal"].ToString();
                txts3F10.Text = dt.Rows[0]["s310f"].ToString();
                txts3Cl10.Text = dt.Rows[0]["s310c"].ToString();
                txts3P10.Text = dt.Rows[0]["s310p"].ToString();
                txts3calculation11.Text = dt.Rows[0]["s311cal"].ToString();
                txts3F11.Text = dt.Rows[0]["s311f"].ToString();
                txts3Cl11.Text = dt.Rows[0]["s311c"].ToString();
                txts3P11.Text = dt.Rows[0]["s311p"].ToString();
                txts3calculation12.Text = dt.Rows[0]["s312cal"].ToString();
                txts3F12.Text = dt.Rows[0]["s312f"].ToString();
                txts3Cl12.Text = dt.Rows[0]["s312c"].ToString();
                txts3P12.Text = dt.Rows[0]["s312p"].ToString();
                txts3remarks.Text = dt.Rows[0]["s3remarks"].ToString();



            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    public bool validate()
    {
        if (txtStartDt.Text == "")
        {
            cf.ShowAlertMessage("Please Select Date");
            txtStartDt.Focus();
            return false;
        }
        if (txtEndDt.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtStartDt.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtEndDt.Text == "")
        {
            cf.ShowAlertMessage("Please Select Date");
            txtEndDt.Focus();
            return false;

        }
        if (txtEndDt.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtEndDt.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtDesc.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Desciprtion");
            txtDesc.Focus();
            return false;

        }
        if (txtDesc.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtDesc.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtremarks.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Email");
            txtremarks.Focus();
            return false;

        }
        if (txtremarks.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtremarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        return true;
    }
    public bool S1validate()
    {
        if (txtS1calculation1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Calculation 1");
            txtS1calculation1.Focus();
            return false;
        }
        if (txtS1calculation1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtS1calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1F1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Found 1");
            txts1F1.Focus();
            return false;

        }
        if (txts1F1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1Cl1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Claim 1");
            txts1Cl1.Focus();
            return false;

        }
        if (txts1Cl1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1P1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Percentage 1");
            txts1P1.Focus();
            return false;

        }
        if (txts1P1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtS1calculation2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Calculation 2");
            txtS1calculation2.Focus();
            return false;
        }
        if (txtS1calculation2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtS1calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1F2.Text == "")
        {

            cf.ShowAlertMessage("Please Enter S1Found 2");
            return false;

        }
        if (txts1F2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1Cl2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Claim 2");
            txts1Cl2.Focus();
            return false;

        }
        if (txts1Cl2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1P2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Percentage 2");
            txts1P2.Focus();
            return false;

        }
        if (txts1P2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtS1calculation3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Calculation 3");
            txtS1calculation3.Focus();
            return false;
        }
        if (txtS1calculation3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtS1calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1F3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Found 3");
            txts1F3.Focus();
            return false;

        }
        if (txts1F3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1Cl3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Claim 3");
            txts1Cl3.Focus();
            return false;

        }
        if (txts1Cl3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1P3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Percentage 3");
            txts1P3.Focus();
            return false;

        }
        if (txts1P3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtS1calculation4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Calculation 4");
            txtS1calculation4.Focus();
            return false;
        }
        if (txtS1calculation4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtS1calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1F4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Found 4");
            txts1F4.Focus();
            return false;

        }
        if (txts1F4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1Cl4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Claim 4");
            txts1Cl4.Focus();
            return false;

        }
        if (txts1Cl4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1P4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Percentage 4");
            txts1P4.Focus();
            return false;

        }
        if (txts1P4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtS1calculation5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Calculation 5");
            txtS1calculation5.Focus();
            return false;
        }
        if (txtS1calculation5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtS1calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1F5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Found 5");
            txts1F5.Focus();
            return false;

        }
        if (txts1F5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1Cl5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Claim 5");
            txts1Cl5.Focus();
            return false;

        }
        if (txts1Cl5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1P5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Percentage 5");
            txts1P5.Focus();
            return false;

        }
        if (txts1P5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtS1calculation6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Calculation 6");
            txtS1calculation6.Focus();
            return false;
        }
        if (txtS1calculation6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtS1calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1F6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Found 6");
            txts1F6.Focus();
            return false;

        }
        if (txts1F6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1Cl6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Claim 6");
            txts1Cl6.Focus();
            return false;

        }
        if (txts1Cl6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts1P6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Percentage 6");
            txts1P6.Focus();
            return false;

        }
        if (txts1P6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1Remrks.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Remarks");
            txts1Remrks.Focus();
            return false;

        }
        if (txts1Remrks.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts1Remrks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool S2validate()
    {
        if (txts2calculation1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Calculation 1");
            txts2calculation1.Focus();
            return false;
        }
        if (txts2calculation1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2F1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Found 1");
            txts2F1.Focus();
            return false;

        }
        if (txts2F1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2Cl1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Claim 1");
            txts2Cl1.Focus();
            return false;

        }
        if (txts2Cl1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2P1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Percentage 1");
            txts2P1.Focus();
            return false;

        }
        if (txts2P1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2calculation2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Calculation 2");
            txts2calculation2.Focus();
            return false;
        }
        if (txts2calculation2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2F2.Text == "")
        {

            cf.ShowAlertMessage("Please Enter S2Found 2");
            txts2F2.Focus();
            return false;

        }
        if (txts2F2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2Cl2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Claim 2");
            txts2Cl2.Focus();
            return false;

        }
        if (txts2Cl2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2P2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Percentage 2");
            txts2P2.Focus();
            return false;

        }
        if (txts2P2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2calculation3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Calculation 3");
            txts2calculation3.Focus();
            return false;
        }
        if (txts2calculation3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2F3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S1Found 3");
            txts2F3.Focus();
            return false;

        }
        if (txts2F3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2Cl3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Claim 3");
            txts2Cl3.Focus();
            return false;

        }
        if (txts2Cl3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2P3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Percentage 3");
            txts2P3.Focus();
            return false;

        }
        if (txts2P3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2calculation4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Calculation 4");
            txts2calculation4.Focus();
            return false;
        }
        if (txts2calculation4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2F4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Found 4");
            txts2F4.Focus();
            return false;

        }
        if (txts2F4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2Cl4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Claim 4");
            txts2Cl4.Focus();
            return false;

        }
        if (txts2Cl4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2Cl4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Percentage 4");
            txts2Cl4.Focus();
            return false;

        }
        if (txts2P4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2P4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Calculation 5");
            txts2P4.Focus();
            return false;
        }
        if (txts2calculation5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2F5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Found 5");
            txts2F5.Focus();
            return false;

        }
        if (txts2F5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2Cl5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Claim 5");
            txts2Cl5.Focus();
            return false;

        }
        if (txts2Cl5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2P5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Percentage 5");
            txts2P5.Focus();
            return false;

        }
        if (txts2P5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2calculation6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Calculation 6");
            txts2calculation6.Focus();
            return false;
        }
        if (txts2calculation6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2F6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Found 6");
            txts2F6.Focus();
            return false;

        }
        if (txts2F6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2Cl6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Claim 6");
            txts2Cl6.Focus();
            return false;

        }
        if (txts2Cl6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts2P6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S2Percentage 6");
            txts2P6.Focus();
            return false;

        }
        if (txts2P6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2remarks.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Remarks");
            txts2remarks.Focus();
            return false;

        }
        if (txts2remarks.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts2remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool S3validate()
    {
        if (txts3calculation1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 1");
            txts3calculation1.Focus();
            return false;
        }
        if (txts3calculation1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 1");
            txts3F1.Focus();
            return false;

        }
        if (txts3F1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 1");
            txts3Cl1.Focus();
            return false;

        }
        if (txts3Cl1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P1.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 1");
            txts3P1.Focus();
            return false;

        }
        if (txts3P1.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 2");
            txts3calculation2.Focus();
            return false;
        }
        if (txts3calculation2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F2.Text == "")
        {

            cf.ShowAlertMessage("Please Enter S3Found 2");
            txts3F2.Focus();
            return false;

        }
        if (txts3F2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 2");
            txts3Cl2.Focus();
            return false;

        }
        if (txts3Cl2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P2.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 2");
            txts3P2.Focus();
            return false;

        }
        if (txts3P2.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 3");
            txts3calculation3.Focus();
            return false;
        }
        if (txts3calculation3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 3");
            txts3F3.Focus();
            return false;

        }
        if (txts3F3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 3");
            txts3Cl3.Focus();
            return false;

        }
        if (txts3Cl3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P3.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 3");
            txts3P3.Focus();
            return false;

        }
        if (txts3P3.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 4");
            txts3calculation4.Focus();
            return false;
        }
        if (txts3calculation4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 4");
            txts3F4.Focus();
            return false;

        }
        if (txts3F4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 4");
            txts3Cl4.Focus();
            return false;

        }
        if (txts3Cl4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P4.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 4");
            txts3P4.Focus();
            return false;

        }
        if (txts3P4.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 5");
            txts3calculation5.Focus();
            return false;
        }
        if (txts3calculation5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 5");
            txts3F5.Focus();
            return false;

        }
        if (txts3F5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 5");
            txts3Cl5.Focus();
            return false;

        }
        if (txts3Cl5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P5.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 5");
            txts3P5.Focus();
            return false;

        }
        if (txts3P5.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 6");
            txts3calculation6.Focus();
            return false;
        }
        if (txts3calculation6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 6");
            txts3F6.Focus();
            return false;

        }
        if (txts3F6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 6");
            txts3Cl6.Focus();
            return false;

        }
        if (txts3Cl6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P6.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 6");
            txts3P6.Focus();
            return false;

        }
        if (txts3P6.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3remarks.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Remarks");
            txts3remarks.Focus();
            return false;

        }
        if (txts3remarks.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        //
        if (txts3calculation7.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 7");
            txts3calculation7.Focus();
            return false;
        }
        if (txts3calculation7.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F7.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 7");
            txts3F7.Focus();
            return false;

        }
        if (txts3F7.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl7.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 7");
            txts3Cl7.Focus();
            return false;

        }
        if (txts3Cl7.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P7.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 7");
            txts3P7.Focus();
            return false;

        }
        if (txts3P7.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation8.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 8");
            txts3calculation8.Focus();
            return false;
        }
        if (txts3calculation8.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F8.Text == "")
        {

            cf.ShowAlertMessage("Please Enter S3Found 8");
            txts3F8.Focus();
            return false;

        }
        if (txts3F8.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl8.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 8");
            txts3Cl8.Focus();
            return false;

        }
        if (txts3Cl8.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P8.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 8");
            txts3P8.Focus();
            return false;

        }
        if (txts3P8.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation9.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 9");
            txts3calculation9.Focus();
            return false;
        }
        if (txts3calculation9.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F9.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 9");
            txts3F9.Focus();
            return false;

        }
        if (txts3F9.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl9.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 9");
            txts3Cl9.Focus();
            return false;

        }
        if (txts3Cl9.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P10.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage10");
            txts3P10.Focus();
            return false;

        }
        if (txts3P10.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation11.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 11");
            txts3calculation11.Focus();
            return false;
        }
        if (txts3calculation11.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F11.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 11");
            txts3F11.Focus();
            return false;

        }
        if (txts3F11.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl11.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 11");
            txts3Cl11.Focus();
            return false;

        }
        if (txts3Cl11.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P11.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 11");
            txts3P11.Focus();
            return false;

        }
        if (txts3P11.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation12.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Calculation 12");
            txts3calculation12.Focus();
            return false;
        }
        if (txts3calculation12.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3calculation12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3F12.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Found 12");
            txts3F12.Focus();
            return false;

        }
        if (txts3F12.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3F12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3Cl12.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Claim 12");
            txts3Cl12.Focus();
            return false;

        }
        if (txts3Cl12.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3Cl12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3P12.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3Percentage 12");
            txts3P12.Focus();
            return false;

        }
        if (txts3P12.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3P12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3remarks.Text == "")
        {
            cf.ShowAlertMessage("Please Enter S3 Remarks");
            txts3remarks.Focus();
            return false;

        }
        if (txts3remarks.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txts3remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        return true;
    }
    protected void GVTestResult_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "editDR")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                //EditTr.Visible = true;
                Edidescrmrks.Visible = true;
                EditTr.Visible = false;
                EditS1.Visible = false;
                addmore.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                // BindTrGrid();
                Binddescremarks();
            }
            if (e.CommandName == "editTR")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbltestsamplid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = true;
                Edidescrmrks.Visible = false;
                EditS1.Visible = false;
                addmore.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                Btnadd.Visible = true;
                BindTrGrid();
            }
            if (e.CommandName == "editS1")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbls1smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;
                EditS1.Visible = true;
                addmore.Visible = false;
                Edidescrmrks.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                BindS1();
            }
            if (e.CommandName == "editS2")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbls2smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;

                Edidescrmrks.Visible = false;
                EditS2.Visible = true;

                EditS1.Visible = false;
                addmore.Visible = false;

                EditS3.Visible = false;
                BindS2();
            }
            if (e.CommandName == "editS3")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbls3smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;

                Edidescrmrks.Visible = false;
                EditS2.Visible = false;

                EditS1.Visible = false;
                addmore.Visible = false;
                EditS3.Visible = true;
                BindS3();
            }
           
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void GridTr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AddRows")
            {
                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("TestParam", typeof(string));
                dtparam.Columns.Add("Testfor", typeof(string));
                dtparam.Columns.Add("TestName", typeof(string));
                dtparam.Columns.Add("Calculation", typeof(string));
                dtparam.Columns.Add("TestProtocol", typeof(string));
                dtparam.Columns.Add("found", typeof(string));
                dtparam.Columns.Add("claim", typeof(string));
                dtparam.Columns.Add("limit", typeof(string));

                int j = 0;
                foreach (GridViewRow gr in GridTr.Rows)
                {

                    DropDownList Paramid = (DropDownList)gr.FindControl("ddlTestParameter");
                    TextBox testfor = (TextBox)gr.FindControl("txtTestfor");
                    TextBox testName = (TextBox)gr.FindControl("txtTestDone");
                    TextBox calculation = (TextBox)gr.FindControl("txtcalculation");
                    DropDownList TestProtocol = (DropDownList)gr.FindControl("ddlTestProtocol");
                    TextBox found = (TextBox)gr.FindControl("txtValueFound");
                    TextBox claim = (TextBox)gr.FindControl("txtValueClaim");
                    TextBox limit = (TextBox)gr.FindControl("txtlimit");

                    if (ValidateGrid(Paramid, testName, calculation, TestProtocol, found, claim, limit))
                    {
                        dtparam.Rows.Add();
                        dtparam.Rows[j]["TestParam"] = ((DropDownList)gr.FindControl("ddlTestParameter")).SelectedValue.Trim();
                        dtparam.Rows[j]["Testfor"] = ((TextBox)gr.FindControl("txtTestfor")).Text.Trim();
                        dtparam.Rows[j]["TestName"] = ((TextBox)gr.FindControl("txtTestDone")).Text.Trim();
                        dtparam.Rows[j]["Calculation"] = ((TextBox)gr.FindControl("txtcalculation")).Text.Trim();
                        dtparam.Rows[j]["TestProtocol"] = ((DropDownList)gr.FindControl("ddlTestProtocol")).SelectedValue.Trim();
                        dtparam.Rows[j]["found"] = ((TextBox)gr.FindControl("txtValueFound")).Text.Trim();
                        dtparam.Rows[j]["claim"] = ((TextBox)gr.FindControl("txtValueClaim")).Text.Trim();
                        dtparam.Rows[j]["limit"] = ((TextBox)gr.FindControl("txtlimit")).Text.Trim();

                        j++;
                    }
                }
                dtparam.Rows.Add();
                Session["dttestresult"] = dtparam;
                GridTr.DataSource = dtparam;
                GridTr.DataBind();
            }

            if (e.CommandName == "Remove")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("TestParam", typeof(string));
                dtparam.Columns.Add("Testfor", typeof(string));
                dtparam.Columns.Add("TestName", typeof(string));
                dtparam.Columns.Add("Calculation", typeof(string));
                dtparam.Columns.Add("TestProtocol", typeof(string));
                dtparam.Columns.Add("found", typeof(string));
                dtparam.Columns.Add("claim", typeof(string));
                dtparam.Columns.Add("limit", typeof(string));

                int j = 0;
                foreach (GridViewRow gr in GridTr.Rows)
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["TestParam"] = ((DropDownList)gr.FindControl("ddlTestParameter")).SelectedValue.Trim();
                    dtparam.Rows[j]["Testfor"] = ((TextBox)gr.FindControl("txtTestfor")).Text.Trim();
                    dtparam.Rows[j]["TestName"] = ((TextBox)gr.FindControl("txtTestDone")).Text.Trim();
                    dtparam.Rows[j]["Calculation"] = ((TextBox)gr.FindControl("txtcalculation")).Text.Trim();
                    dtparam.Rows[j]["TestProtocol"] = ((DropDownList)gr.FindControl("ddlTestProtocol")).SelectedValue.Trim();
                    dtparam.Rows[j]["found"] = ((TextBox)gr.FindControl("txtValueFound")).Text.Trim();
                    dtparam.Rows[j]["claim"] = ((TextBox)gr.FindControl("txtValueClaim")).Text.Trim();
                    dtparam.Rows[j]["limit"] = ((TextBox)gr.FindControl("txtlimit")).Text.Trim();

                    j++;
                }
                dtparam.Rows.RemoveAt(gvrow.RowIndex);
                if (dtparam.Rows.Count == 0)
                    dtparam.Rows.Add();
                GridTr.DataSource = dtparam;
                GridTr.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void GridTr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label testParam = (e.Row.FindControl("lblTestParameter") as Label);
            DropDownList ddlTestParameter = (DropDownList)e.Row.FindControl("ddlTestParameter");
            dt = new DataTable();
            objBE.Action = "PARAM_UO";
            objBE.SampleID = lblsampleid.Text;
            objBE.labID = Session["Labcode"].ToString();
           // objBE.AnalystId = Session["UnitOfficerCode"].ToString();
            dt = ObjDL.JAActionEdit(objBE, con);
            cf.BindDropDownLists(ddlTestParameter, dt, "Parameter", "Parameter_Id", "Select Parameter");
            ddlTestParameter.SelectedValue = testParam.Text;


            Label testtestprotocal = (e.Row.FindControl("lbltestpr") as Label);
            DropDownList ddlTestProtocal = (DropDownList)e.Row.FindControl("ddlTestProtocol");
            dt = new DataTable();
            Obj.Action = "PROTOCOL";
            dt = ObjDAL.Getdetails(Obj, con);
            cf.BindDropDownLists(ddlTestProtocal, dt, "ProtocolName", "Protocolid", "Select");
            ddlTestProtocal.SelectedValue = testtestprotocal.Text;
        }
    }

    protected bool ValidateGrid(DropDownList ddlParam, TextBox testnm, TextBox cal, DropDownList ddlpr, TextBox fnd, TextBox clm, TextBox lmt)
    {
        if (ddlParam.SelectedIndex == 0)
        {

            cf.ShowAlertMessage("Select Test Parameter");
            ddlParam.Focus();
            return false;
        }
        else if (ddlParam.SelectedItem.Text == "Other tests" || ddlParam.SelectedItem.Text == "other tests")
        {
            if (testnm.Text == "")
            {
                cf.ShowAlertMessage("Enter Test Name");
                testnm.Focus();
                return false;
            }
        }
        else if (cal.Text == "")
        {
            cf.ShowAlertMessage("Enter Calculation");
            cal.Focus();
            return false;
        }
        else if (cal.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(cal.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");

            else if (ddlpr.SelectedIndex == 0)
            {
                cf.ShowAlertMessage("Select Test Protocol");
                ddlpr.Focus();
                return false;
            }
            else if (fnd.Text == "")
            {
                cf.ShowAlertMessage("Enter Found Values");
                fnd.Focus();
                return false;
            }
            else if (clm.Text == "")
            {
                cf.ShowAlertMessage("Enter Claim Values");
                clm.Focus();
                return false;
            }
            else if (lmt.Text == "")
            {
                cf.ShowAlertMessage("Enter Limit Values");
                lmt.Focus();
                return false;
            }

        }
        return true;
    }
    protected void BtnTR_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate())
            {
                objBE.startdt = cf.Texttodateconverter(txtStartDt.Text);
                objBE.enddt = cf.Texttodateconverter(txtEndDt.Text);
                objBE.Remarks = txtremarks.Text.Trim();
                objBE.description = txtDesc.Text.Trim();
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                objBE.SampleID = lblsampleid.Text;
                objBE.AnalystId = Session["UnitOfficerCode"].ToString();
                objBE.Action = "TRU";
                dt = ObjDL.JAActionEdit(objBE, con);

                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage("Update Failed");
                    BindGrid();
                }
                else
                {
                    cf.ShowAlertMessage("Update Succsessfully");
                    Edidescrmrks.Visible = false;
                    edttrgrid.Visible = false;
                    BindGrid();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");

        }
    }
    protected void BtnS1_Click(object sender, EventArgs e)
    {
        try
        {
            //if (S1validate())
            //{
                objBE.SampleID = lbls1smpid.Text.Trim();
                objBE.S1Calculation1 = txtS1calculation1.Text.Trim();
                objBE.S1Found1 = txts1F1.Text.Trim();
                objBE.S1Claim1 = txts1Cl1.Text.Trim();
                objBE.S1Percentage1 = txts1P1.Text.Trim();
                objBE.S1Calculation2 = txtS1calculation2.Text.Trim();
                objBE.S1Found2 = txts1F2.Text.Trim();
                objBE.S1Claim2 = txts1Cl2.Text.Trim();
                objBE.S1Percentage2 = txts1P2.Text.Trim();
                objBE.S1Calculation3 = txtS1calculation3.Text.Trim();
                objBE.S1Found3 = txts1F3.Text.Trim();
                objBE.S1Claim3 = txts1Cl3.Text.Trim();
                objBE.S1Percentage3 = txts1P3.Text.Trim();
                objBE.S1Calculation4 = txtS1calculation4.Text.Trim();
                objBE.S1Found4 = txts1F4.Text.Trim();
                objBE.S1Claim4 = txts1Cl4.Text.Trim();
                objBE.S1Percentage4 = txts1P4.Text.Trim();
                objBE.S1Calculation5 = txtS1calculation5.Text.Trim();
                objBE.S1Found5 = txts1F5.Text.Trim();
                objBE.S1Claim5 = txts1Cl5.Text.Trim();
                objBE.S1Percentage5 = txts1P5.Text.Trim();
                objBE.S1Calculation6 = txtS1calculation6.Text.Trim();
                objBE.S1Found6 = txts1F6.Text.Trim();
                objBE.S1Claim6 = txts1Cl6.Text.Trim();
                objBE.S1Percentage6 = txts1P6.Text.Trim();
                objBE.S1Remarks = txts1Remrks.Text.Trim();
                objBE.Action = "S1U";
                dt = ObjDL.JAActionEdit(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Unable to Update test result");
                else
                    cf.ShowAlertMessage("Dissolution S1 Test results Updated");
                BindGrid();
                EditS1.Visible = false;

       //     }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }

    }
    protected void BtnS2_Click(object sender, EventArgs e)
    {
        try
        {
            //if (S2validate())
            //{
                //S2
                objBE.SampleID = lbls2smpid.Text.Trim();
                objBE.S2Calculation1 = txts2calculation1.Text.Trim();
                objBE.S2Found1 = txts2F1.Text.Trim();
                objBE.S2Claim1 = txts2Cl1.Text.Trim();
                objBE.S2Percentage1 = txts2P1.Text.Trim();
                objBE.S2Calculation2 = txts2calculation2.Text.Trim();
                objBE.S2Found2 = txts2F2.Text.Trim();
                objBE.S2Claim2 = txts2Cl2.Text.Trim();
                objBE.S2Percentage2 = txts2P2.Text.Trim();
                objBE.S2Calculation3 = txts2calculation3.Text.Trim();
                objBE.S2Found3 = txts2F3.Text.Trim();
                objBE.S2Claim3 = txts2Cl3.Text.Trim();
                objBE.S2Percentage3 = txts2P3.Text.Trim();
                objBE.S2Calculation4 = txts2calculation4.Text.Trim();
                objBE.S2Found4 = txts2F4.Text.Trim();
                objBE.S2Claim4 = txts2Cl4.Text.Trim();
                objBE.S2Percentage4 = txts2P4.Text.Trim();
                objBE.S2Calculation5 = txts2calculation5.Text.Trim();
                objBE.S2Found5 = txts2F5.Text.Trim();
                objBE.S2Claim5 = txts2Cl5.Text.Trim();
                objBE.S2Percentage5 = txts2P5.Text.Trim();
                objBE.S2Calculation6 = txts2calculation6.Text.Trim();
                objBE.S2Found6 = txts2F6.Text.Trim();
                objBE.S2Claim6 = txts2Cl6.Text.Trim();
                objBE.S2Percentage6 = txts2P6.Text.Trim();

                objBE.S2Remarks = txts2remarks.Text.Trim();
                objBE.Action = "S2U";
                dt = ObjDL.JAActionEdit(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Unable to save test result");
                else
                    cf.ShowAlertMessage(" Dissolution S2 Test results Updated");
                EditS2.Visible = false;
                BindGrid();
           // }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BtnS3_Click(object sender, EventArgs e)
    {
        //S3
        try
        {
            //if (S3validate())
            //{

                objBE.SampleID = lbls3smpid.Text.Trim();
                objBE.S3Calculation1 = txts3calculation1.Text.Trim();
                objBE.S3Found1 = txts3F1.Text.Trim();
                objBE.S3Claim1 = txts3Cl1.Text.Trim();
                objBE.S3Percentage1 = txts3P1.Text.Trim();
                objBE.S3Calculation2 = txts3calculation2.Text.Trim();
                objBE.S3Found2 = txts3F2.Text.Trim();
                objBE.S3Claim2 = txts3Cl2.Text.Trim();
                objBE.S3Percentage2 = txts3P2.Text.Trim();
                objBE.S3Calculation3 = txts3calculation3.Text.Trim();
                objBE.S3Found3 = txts3F3.Text.Trim();
                objBE.S3Claim3 = txts3Cl3.Text.Trim();
                objBE.S3Percentage3 = txts3P3.Text.Trim();
                objBE.S3Calculation4 = txts3calculation4.Text.Trim();
                objBE.S3Found4 = txts3F4.Text.Trim();
                objBE.S3Claim4 = txts3Cl4.Text.Trim();
                objBE.S3Percentage4 = txts3P4.Text.Trim();
                objBE.S3Calculation5 = txts3calculation5.Text.Trim();
                objBE.S3Found5 = txts3F5.Text.Trim();
                objBE.S3Claim5 = txts3Cl5.Text.Trim();
                objBE.S3Percentage5 = txts3P5.Text.Trim();
                objBE.S3Calculation6 = txts3calculation6.Text.Trim();
                objBE.S3Found6 = txts3F6.Text.Trim();
                objBE.S3Claim6 = txts3Cl6.Text.Trim();
                objBE.S3Percentage6 = txts3P6.Text.Trim();
                objBE.S3Calculation7 = txts3calculation7.Text.Trim();
                objBE.S3Found7 = txts3F7.Text.Trim();
                objBE.S3Claim7 = txts3Cl7.Text.Trim();
                objBE.S3Percentage7 = txts3P7.Text.Trim();
                objBE.S3Calculation8 = txts3calculation8.Text.Trim();
                objBE.S3Found8 = txts3F8.Text.Trim();
                objBE.S3Claim8 = txts3Cl8.Text.Trim();
                objBE.S3Percentage8 = txts3P8.Text.Trim();
                objBE.S3Calculation9 = txts3calculation9.Text.Trim();
                objBE.S3Found9 = txts3F9.Text.Trim();
                objBE.S3Claim9 = txts3Cl9.Text.Trim();
                objBE.S3Percentage9 = txts3P9.Text.Trim();
                objBE.S3Calculation10 = txts3calculation10.Text.Trim();
                objBE.S3Found10 = txts3F10.Text.Trim();
                objBE.S3Claim10 = txts3Cl10.Text.Trim();
                objBE.S3Percentage10 = txts3P10.Text.Trim();
                objBE.S3Calculation11 = txts3calculation11.Text.Trim();
                objBE.S3Found11 = txts3F11.Text.Trim();
                objBE.S3Claim11 = txts3Cl11.Text.Trim();
                objBE.S3Percentage11 = txts3P11.Text.Trim();
                objBE.S3Calculation12 = txts3calculation12.Text.Trim();
                objBE.S3Found12 = txts3F12.Text.Trim();
                objBE.S3Claim12 = txts3Cl12.Text.Trim();
                objBE.S3Percentage12 = txts3P12.Text.Trim();
                objBE.S3Remarks = txts3remarks.Text.Trim();

                objBE.Action = "S3U";
                dt = ObjDL.JAActionEdit(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Unable to Update test result");
                else
                    cf.ShowAlertMessage("Dissolution S3 Test results Updated");
                EditS3.Visible = false;
                BindGrid();
           // }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }

    }
   
    protected void Gvedittest_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = Gvedittest.Rows[e.RowIndex];
            int testid = Convert.ToInt32(Gvedittest.DataKeys[e.RowIndex].Values[0]);

            string testparamname = (row.FindControl("ddledtTestParameter") as DropDownList).SelectedValue;
            string testfor = (row.FindControl("txtedttestfor") as TextBox).Text;
            string testname = (row.FindControl("txtedttestname") as TextBox).Text;
            string testcalculation = (row.FindControl("txtedtcalculation") as TextBox).Text;

            string testprotocal = (row.FindControl("ddledtTestProtocols") as DropDownList).SelectedValue;
            string testvaluefound = (row.FindControl("txtedtvaluefound") as TextBox).Text;
            string testvalueclaim = (row.FindControl("txtedtValueClaim") as TextBox).Text;
            string testlimit = (row.FindControl("txtedtlimit") as TextBox).Text;
            objBE.testparameter = testparamname.ToString();
            objBE.testforanalyst = testfor.ToString();
            objBE.testname = testname.ToString();
            objBE.testcalculation = testcalculation.ToString();
            objBE.testprotocal = testprotocal.ToString();

            objBE.testfound = testvaluefound.ToString();
            objBE.testclaim = testvalueclaim.ToString();
            objBE.testlimit = testlimit.ToString();
            objBE.Testid = testid.ToString();
            objBE.Action = "AU";
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Update Failed");
                BindGrid();
            }
            else
            {
                cf.ShowAlertMessage("Update Succsessfully");
                Gvedittest.EditIndex = -1;
                BindTrGrid();
            }

        }

        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");

        }
    }
    protected void Gvedittest_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Gvedittest.EditIndex = e.NewEditIndex;
        BindTrGrid();
    }
    protected void Gvedittest_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gvedittest.EditIndex = -1;
        BindTrGrid();
    }
    protected void Gvedittest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //    Label testParamss = (e.Row.FindControl("lbledtTestParameter") as Label);
            //    DropDownList ddlTestParameter = (DropDownList)e.Row.FindControl("ddlTestParameters");
            //    dt = new DataTable();
            //    objBE.Action = "PARAM";
            //    objBE.SampleID = lblsampleid.Text.Trim();
            //    objBE.AnalystId = Session["AnalystCode"].ToString();
            //    dt = ObjDL.JAAction(objBE, con);
            //    cf.BindDropDownLists(ddlTestParameter, dt, "Parameter", "Parameter_Id", "Select Parameter");
            //    ddlTestParameter.SelectedValue = testParamss.Text;


            //    Label testtestprotocalss = (e.Row.FindControl("lbledttestpr") as Label);
            //    DropDownList ddlTestProtocalss = (DropDownList)e.Row.FindControl("ddledtTestProtocol");
            //    dt = new DataTable();
            //    Obj.Action = "PROTOCOL";
            //    dt = ObjDAL.Getdetails(Obj, con);
            //    cf.BindDropDownLists(ddlTestProtocalss, dt, "ProtocolName", "Protocolid", "Select");
            //    ddlTestProtocalss.SelectedValue = testtestprotocalss.Text;
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {

                Label testParam = (e.Row.FindControl("lbltestparam") as Label);
                DropDownList ddlTestParameters = (DropDownList)e.Row.FindControl("ddledtTestParameter");
                dt = new DataTable();
                objBE.Action = "PARAM_UO";
                objBE.SampleID = lblsampleid.Text;
                objBE.labID = Session["Labcode"].ToString();

                dt = ObjDL.JAActionEdit(objBE, con);
                cf.BindDropDownLists(ddlTestParameters, dt, "Parameter", "Parameter_Id", "Select Parameter");
                ddlTestParameters.SelectedValue = testParam.Text;

                //TextBox testnames = (e.Row.FindControl("lbledtTestDone") as TextBox);
                //Label lbltestdones = (e.Row.FindControl("lblTestDone") as Label);
                //testnames.Text = lbltestdones.Text;


                Label testtestprotocals = (e.Row.FindControl("lbltestprotocal") as Label);
                DropDownList ddlTestProtocals = (DropDownList)e.Row.FindControl("ddledtTestProtocols");
                dt = new DataTable();
                Obj.Action = "PROTOCOL";
                dt = ObjDAL.Getdetails(Obj, con);
                cf.BindDropDownLists(ddlTestProtocals, dt, "ProtocolName", "Protocolid", "Select");
                ddlTestProtocals.SelectedValue = testtestprotocals.Text;

            }
        }
    }
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        addmore.Visible = true;
        EditTr.Visible = true;

        Btnadd.Visible = false;
        edttrgrid.Visible = false;

    }

    protected void Btntrtvp_Click(object sender, EventArgs e)
    {
        check();
        try
        {
           // if (validate())
           // {
                objBE.Action = "ADD";
                objBE.SampleID = lblsampleid.Text.Trim();
               // objBE.AnalystId = Session["AnalystCode"].ToString();
                objBE.AckTVP = null;

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("TestParam", typeof(string));
                dtparam.Columns.Add("Testfor", typeof(string));
                dtparam.Columns.Add("TestProtocol", typeof(string));
                dtparam.Columns.Add("Calculation", typeof(string));
                dtparam.Columns.Add("TestName", typeof(string));
                dtparam.Columns.Add("found", typeof(string));
                dtparam.Columns.Add("claim", typeof(string));
                dtparam.Columns.Add("limit", typeof(string));


                int i = 0;
                foreach (GridViewRow gr in GridTr.Rows)
                {
                    DropDownList Paramid = (DropDownList)gr.FindControl("ddlTestParameter");
                    TextBox testfor = (TextBox)gr.FindControl("txtTestfor");
                    TextBox testName = (TextBox)gr.FindControl("txtTestDone");
                    TextBox calculation = (TextBox)gr.FindControl("txtcalculation");
                    DropDownList TestProtocol = (DropDownList)gr.FindControl("ddlTestProtocol");
                    TextBox found = (TextBox)gr.FindControl("txtValueFound");
                    TextBox claim = (TextBox)gr.FindControl("txtValueClaim");
                    TextBox limit = (TextBox)gr.FindControl("txtlimit");

                    if (ValidateGrid(Paramid, testName, calculation, TestProtocol, found, claim, limit))
                    {
                        dtparam.Rows.Add();
                        dtparam.Rows[i]["TestParam"] = ((DropDownList)gr.FindControl("ddlTestParameter")).SelectedValue.Trim();
                        dtparam.Rows[i]["testfor"] = ((TextBox)gr.FindControl("txtTestfor")).Text.Trim();
                        dtparam.Rows[i]["TestProtocol"] = ((DropDownList)gr.FindControl("ddlTestProtocol")).SelectedValue.Trim();
                        dtparam.Rows[i]["Calculation"] = ((TextBox)gr.FindControl("txtcalculation")).Text.Trim();
                        dtparam.Rows[i]["TestName"] = ((TextBox)gr.FindControl("txtTestDone")).Text.Trim();
                        dtparam.Rows[i]["found"] = ((TextBox)gr.FindControl("txtValueFound")).Text.Trim();
                        dtparam.Rows[i]["claim"] = ((TextBox)gr.FindControl("txtValueClaim")).Text.Trim();
                        dtparam.Rows[i]["limit"] = ((TextBox)gr.FindControl("txtlimit")).Text.Trim();

                    }
                    i++;
                }

                objBE.TVP = dtparam;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.JAActionEdit(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                }
                else
                {
                    cf.ShowAlertMessage("Test results saved");
                    //clear();
                    // pnltestresult.Visible = true;
                }
                BindTrGrid();
                GridTr.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
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
            //cookie_value = System.Web.HttpContext.Current.Request.Cookies["ASPFIXATION2"].Value;
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