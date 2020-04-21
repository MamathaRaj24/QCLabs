using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;

public partial class DCA_Analyst_RAEditTestResult : System.Web.UI.Page
{
    Registration_BE  objBE = new Registration_BE ();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Masters ObjDAL = new Masters();
    Master_BE Obj = new Master_BE();
    DataTable dt;
    SampleSqlInjectionScreeningModule obj = new SampleSqlInjectionScreeningModule();
    string con, Analystcode, Uocode;

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
        if (Session["UsrName"] != null && Session["RoleID"].ToString().Trim() == "5" || Session["RoleID"].ToString().Trim() == "6")
        {
            con = Session["ConnKey"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

            if (Session["RoleID"].ToString().Trim() == "5")
            {
                lblUser.Text = Session["Role"].ToString() + " -  " + Session["AnalystName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
                Analystcode = Session["AnalystCode"].ToString();
            }
            if (Session["RoleID"].ToString().Trim() == "6")
            {
                lblUser.Text = Session["Role"].ToString() + " -  " + Session["JsoName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
                Uocode = Session["Jsocode"].ToString();
            }

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
            objBE.flag = null;
            objBE.Action = "RAEDIT";
            if (Session["RoleID"].ToString() == "5")
                objBE.AnalystId = Session["AnalystCode"].ToString();
            if (Session["RoleID"].ToString() == "6")
                objBE.AnalystId = Uocode;
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
            objBE.Action = "RATR";
            objBE.SampleID = lblsampleid.Text.Trim();

            if (Session["RoleID"].ToString() == "5")
                objBE.AnalystId = Session["AnalystCode"].ToString();
            if (Session["RoleID"].ToString() == "6")
                objBE.AnalystId = Uocode;
            objBE.user = Session["RoleID"].ToString();
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
            objBE.TVP = null; objBE.AckTVP = null;
            objBE.SampleID = lblsampleid.Text.Trim();
            if (Session["RoleID"].ToString() == "5")
                objBE.AnalystId = Session["AnalystCode"].ToString();
           
            if (Session["RoleID"].ToString() == "6")
                objBE.AnalystId = Uocode;
          
            lbllab.Text = Session["Labname"].ToString();
            objBE.Action = "RATR";
            objBE.user = Session["RoleID"].ToString();
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
            objBE.Action = "DS";
            objBE.flag = "S1R";
            dt = new DataTable();
            
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditS1.Visible = true;
                txtS1calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txts1F1.Text = dt.Rows[0]["found_1"].ToString();
                txts1Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txts1P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txtS1calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txts1F2.Text = dt.Rows[0]["found_2"].ToString();
                txts1Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txts1P2.Text = dt.Rows[0]["per_2"].ToString();
                txtS1calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txts1F3.Text = dt.Rows[0]["found_3"].ToString();
                txts1Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txts1P3.Text = dt.Rows[0]["per_3"].ToString();
                txtS1calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txts1F4.Text = dt.Rows[0]["found_4"].ToString();
                txts1Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txts1P4.Text = dt.Rows[0]["per_4"].ToString();
                txtS1calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txts1F5.Text = dt.Rows[0]["found_5"].ToString();
                txts1Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txts1P5.Text = dt.Rows[0]["per_5"].ToString();
                txtS1calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txts1F6.Text = dt.Rows[0]["found_6"].ToString();
                txts1Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txts1P6.Text = dt.Rows[0]["per_6"].ToString();
                txts1Remrks.Text = dt.Rows[0]["Remarks"].ToString();
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
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "S2R";
            dt = new DataTable();
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //s2
                EditS2.Visible = true;
                txts2calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txts2F1.Text = dt.Rows[0]["found_1"].ToString();
                txts2Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txts2P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txts2calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txts2F2.Text = dt.Rows[0]["found_2"].ToString();
                txts2Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txts2P2.Text = dt.Rows[0]["per_2"].ToString();
                txts2calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txts2F3.Text = dt.Rows[0]["found_3"].ToString();
                txts2Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txts2P3.Text = dt.Rows[0]["per_3"].ToString();
                txts2calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txts2F4.Text = dt.Rows[0]["found_4"].ToString();
                txts2Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txts2P4.Text = dt.Rows[0]["per_4"].ToString();
                txts2calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txts2F5.Text = dt.Rows[0]["found_5"].ToString();
                txts2Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txts2P5.Text = dt.Rows[0]["per_5"].ToString();
                txts2calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txts2F6.Text = dt.Rows[0]["found_6"].ToString();
                txts2Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txts2P6.Text = dt.Rows[0]["per_6"].ToString();
                txts2remarks.Text = dt.Rows[0]["Remarks"].ToString();
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

            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "S3R";
            dt = new DataTable();
            //dt = ObjDL.JAActionEdit(objBE, con);
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S3
                EditS3.Visible = true;
                txts3calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txts3F1.Text = dt.Rows[0]["found_1"].ToString();
                txts3Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txts3P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txts3calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txts3F2.Text = dt.Rows[0]["found_2"].ToString();
                txts3Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txts3P2.Text = dt.Rows[0]["per_2"].ToString();
                txts3calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txts3F3.Text = dt.Rows[0]["found_3"].ToString();
                txts3Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txts3P3.Text = dt.Rows[0]["per_3"].ToString();
                txts3calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txts3F4.Text = dt.Rows[0]["found_4"].ToString();
                txts3Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txts3P4.Text = dt.Rows[0]["per_4"].ToString();
                txts3calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txts3F5.Text = dt.Rows[0]["found_5"].ToString();
                txts3Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txts3P5.Text = dt.Rows[0]["per_5"].ToString();

                txts3calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txts3F6.Text = dt.Rows[0]["found_6"].ToString();
                txts3Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txts3P6.Text = dt.Rows[0]["per_6"].ToString();

                txts3calculation7.Text = dt.Rows[0]["cal_7"].ToString();
                txts3F7.Text = dt.Rows[0]["found_7"].ToString();
                txts3Cl7.Text = dt.Rows[0]["claim_7"].ToString();
                txts3P7.Text = dt.Rows[0]["per_7"].ToString();

                txts3calculation8.Text = dt.Rows[0]["cal_8"].ToString();
                txts3F8.Text = dt.Rows[0]["found_8"].ToString();
                txts3Cl8.Text = dt.Rows[0]["claim_8"].ToString();
                txts3P8.Text = dt.Rows[0]["per_8"].ToString();

                txts3calculation9.Text = dt.Rows[0]["cal_9"].ToString();
                txts3F9.Text = dt.Rows[0]["found_9"].ToString();
                txts3Cl9.Text = dt.Rows[0]["claim_9"].ToString();
                txts3P9.Text = dt.Rows[0]["per_9"].ToString();

                txts3calculation10.Text = dt.Rows[0]["cal_10"].ToString();
                txts3F10.Text = dt.Rows[0]["found_10"].ToString();
                txts3Cl10.Text = dt.Rows[0]["claim_10"].ToString();
                txts3P10.Text = dt.Rows[0]["per_10"].ToString();

                txts3calculation11.Text = dt.Rows[0]["cal_11"].ToString();
                txts3F11.Text = dt.Rows[0]["found_11"].ToString();
                txts3Cl11.Text = dt.Rows[0]["claim_11"].ToString();
                txts3P11.Text = dt.Rows[0]["per_11"].ToString();

                txts3calculation12.Text = dt.Rows[0]["cal_12"].ToString();
                txts3F12.Text = dt.Rows[0]["found_12"].ToString();
                txts3Cl12.Text = dt.Rows[0]["claim_12"].ToString();
                txts3P12.Text = dt.Rows[0]["per_12"].ToString();
                txts3remarks.Text = dt.Rows[0]["Remarks"].ToString();



            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }

    protected void BindA1()
    {
        try
        {
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "A1R";
            dt = new DataTable();
            //dt = ObjDL.JAActionEdit(objBE, con);
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditA1.Visible = true;
                txtA1calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txtA1F1.Text = dt.Rows[0]["found_1"].ToString();
                txtA1Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txtA1P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txtA1calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txtA1F2.Text = dt.Rows[0]["found_2"].ToString();
                txtA1Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txtA1P2.Text = dt.Rows[0]["per_2"].ToString();
                txtA1calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txtA1F3.Text = dt.Rows[0]["found_3"].ToString();
                txtA1Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txtA1P3.Text = dt.Rows[0]["per_3"].ToString();
                txtA1calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txtA1F4.Text = dt.Rows[0]["found_4"].ToString();
                txtA1Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txtA1P4.Text = dt.Rows[0]["per_4"].ToString();
                txtA1calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txtA1F5.Text = dt.Rows[0]["found_5"].ToString();
                txtA1Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txtA1P5.Text = dt.Rows[0]["per_5"].ToString();
                txtA1calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txtA1F6.Text = dt.Rows[0]["found_6"].ToString();
                txtA1Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txtA1P6.Text = dt.Rows[0]["per_6"].ToString();
                txtA1Remrks.Text = dt.Rows[0]["Remarks"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindA2()
    {
        try
        {
            dt = new DataTable();
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "A2R";
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditA2.Visible = true;
                txtA2calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txtA2F1.Text = dt.Rows[0]["found_1"].ToString();
                txtA2Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txtA2P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txtA2calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txtA2F2.Text = dt.Rows[0]["found_2"].ToString();
                txtA2Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txtA2P2.Text = dt.Rows[0]["per_2"].ToString();
                txtA2calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txtA2F3.Text = dt.Rows[0]["found_3"].ToString();
                txtA2Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txtA2P3.Text = dt.Rows[0]["per_3"].ToString();
                txtA2calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txtA2F4.Text = dt.Rows[0]["found_4"].ToString();
                txtA2Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txtA2P4.Text = dt.Rows[0]["per_4"].ToString();
                txtA2calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txtA2F5.Text = dt.Rows[0]["found_5"].ToString();
                txtA2Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txtA2P5.Text = dt.Rows[0]["per_5"].ToString();
                txtA2calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txtA2F6.Text = dt.Rows[0]["found_6"].ToString();
                txtA2Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txtA2P6.Text = dt.Rows[0]["per_6"].ToString();
                txtA2remarks.Text = dt.Rows[0]["Remarks"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindA3()
    {
        try
        {
            dt = new DataTable();
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "A3R";
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditA3.Visible = true;
                txtA3calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txtA3F1.Text = dt.Rows[0]["found_1"].ToString();
                txtA3Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txtA3P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txtA3calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txtA3F2.Text = dt.Rows[0]["found_2"].ToString();
                txtA3Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txtA3P2.Text = dt.Rows[0]["per_2"].ToString();
                txtA3calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txtA3F3.Text = dt.Rows[0]["found_3"].ToString();
                txtA3Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txtA3P3.Text = dt.Rows[0]["per_3"].ToString();
                txtA3calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txtA3F4.Text = dt.Rows[0]["found_4"].ToString();
                txtA3Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txtA3P4.Text = dt.Rows[0]["per_4"].ToString();
                txtA3calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txtA3F5.Text = dt.Rows[0]["found_5"].ToString();
                txtA3Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txtA3P5.Text = dt.Rows[0]["per_5"].ToString();
                txtA3calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txtA3F6.Text = dt.Rows[0]["found_6"].ToString();
                txtA3Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txtA3P6.Text = dt.Rows[0]["per_6"].ToString();

                txtA3calculation7.Text = dt.Rows[0]["cal_7"].ToString();
                txtA3F7.Text = dt.Rows[0]["found_7"].ToString();
                txtA3Cl7.Text = dt.Rows[0]["claim_7"].ToString();
                txtA3P7.Text = dt.Rows[0]["per_7"].ToString();
                txtA3calculation8.Text = dt.Rows[0]["cal_8"].ToString();
                txtA3F8.Text = dt.Rows[0]["found_8"].ToString();
                txtA3Cl8.Text = dt.Rows[0]["claim_8"].ToString();
                txtA3P8.Text = dt.Rows[0]["per_8"].ToString();
                txtA3calculation9.Text = dt.Rows[0]["cal_9"].ToString();
                txtA3F9.Text = dt.Rows[0]["found_9"].ToString();
                txtA3Cl9.Text = dt.Rows[0]["claim_9"].ToString();
                txtA3P9.Text = dt.Rows[0]["per_9"].ToString();
                txtA3calculation10.Text = dt.Rows[0]["cal_10"].ToString();
                txtA3F10.Text = dt.Rows[0]["found_10"].ToString();
                txtA3Cl10.Text = dt.Rows[0]["claim_10"].ToString();
                txtA3P10.Text = dt.Rows[0]["per_10"].ToString();
                txtA3calculation11.Text = dt.Rows[0]["cal_11"].ToString();
                txtA3F11.Text = dt.Rows[0]["found_11"].ToString();
                txtA3Cl11.Text = dt.Rows[0]["claim_11"].ToString();
                txtA3P11.Text = dt.Rows[0]["per_11"].ToString();
                txtA3calculation12.Text = dt.Rows[0]["cal_12"].ToString();
                txtA3F12.Text = dt.Rows[0]["found_12"].ToString();
                txtA3Cl12.Text = dt.Rows[0]["claim_12"].ToString();
                txtA3P12.Text = dt.Rows[0]["per_12"].ToString();

                txtA3remarks.Text = dt.Rows[0]["Remarks"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindB1()
    {
        try
        {
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "B1R";
            dt = new DataTable();
            //dt = ObjDL.JAActionEdit(objBE, con);
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditB1.Visible = true;
                txtB1calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txtB1F1.Text = dt.Rows[0]["found_1"].ToString();
                txtB1Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txtB1P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txtB1calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txtB1F2.Text = dt.Rows[0]["found_2"].ToString();
                txtB1Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txtB1P2.Text = dt.Rows[0]["per_2"].ToString();
                txtB1calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txtB1F3.Text = dt.Rows[0]["found_3"].ToString();
                txtB1Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txtB1P3.Text = dt.Rows[0]["per_3"].ToString();
                txtB1calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txtB1F4.Text = dt.Rows[0]["found_4"].ToString();
                txtB1Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txtB1P4.Text = dt.Rows[0]["per_4"].ToString();
                txtB1calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txtB1F5.Text = dt.Rows[0]["found_5"].ToString();
                txtB1Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txtB1P5.Text = dt.Rows[0]["per_5"].ToString();
                txtB1calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txtB1F6.Text = dt.Rows[0]["found_6"].ToString();
                txtB1Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txtB1P6.Text = dt.Rows[0]["per_6"].ToString();
                txtB1Remrks.Text = dt.Rows[0]["Remarks"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindB2()
    {
        try
        {
            dt = new DataTable();
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "B2R";
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditB2.Visible = true;
                txtB2calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txtB2F1.Text = dt.Rows[0]["found_1"].ToString();
                txtB2Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txtB2P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txtB2calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txtB2F2.Text = dt.Rows[0]["found_2"].ToString();
                txtB2Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txtB2P2.Text = dt.Rows[0]["per_2"].ToString();
                txtB2calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txtB2F3.Text = dt.Rows[0]["found_3"].ToString();
                txtB2Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txtB2P3.Text = dt.Rows[0]["per_3"].ToString();
                txtB2calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txtB2F4.Text = dt.Rows[0]["found_4"].ToString();
                txtB2Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txtB2P4.Text = dt.Rows[0]["per_4"].ToString();
                txtB2calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txtB2F5.Text = dt.Rows[0]["found_5"].ToString();
                txtB2Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txtB2P5.Text = dt.Rows[0]["per_5"].ToString();
                txtB2calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txtB2F6.Text = dt.Rows[0]["found_6"].ToString();
                txtB2Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txtB2P6.Text = dt.Rows[0]["per_6"].ToString();
                txtB2remarks.Text = dt.Rows[0]["Remarks"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindB3()
    {
        try
        {
            dt = new DataTable();
            objBE.SampleID = lblsampleid.Text.Trim();
            objBE.Action = "DS";
            objBE.flag = "B3R";
            dt = ObjDL.bulkdata(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //S1
                EditB3.Visible = true;
                txtB3calculation1.Text = dt.Rows[0]["cal_1"].ToString();
                txtB3F1.Text = dt.Rows[0]["found_1"].ToString();
                txtB3Cl1.Text = dt.Rows[0]["claim_1"].ToString();
                txtB3P1.Text = dt.Rows[0]["percentage_1"].ToString();
                txtB3calculation2.Text = dt.Rows[0]["cal_2"].ToString();
                txtB3F2.Text = dt.Rows[0]["found_2"].ToString();
                txtB3Cl2.Text = dt.Rows[0]["claim_2"].ToString();
                txtB3P2.Text = dt.Rows[0]["per_2"].ToString();
                txtB3calculation3.Text = dt.Rows[0]["cal_3"].ToString();
                txtB3F3.Text = dt.Rows[0]["found_3"].ToString();
                txtB3Cl3.Text = dt.Rows[0]["claim_3"].ToString();
                txtB3P3.Text = dt.Rows[0]["per_3"].ToString();
                txtB3calculation4.Text = dt.Rows[0]["cal_4"].ToString();
                txtB3F4.Text = dt.Rows[0]["found_4"].ToString();
                txtB3Cl4.Text = dt.Rows[0]["claim_4"].ToString();
                txtB3P4.Text = dt.Rows[0]["per_4"].ToString();
                txtB3calculation5.Text = dt.Rows[0]["cal_5"].ToString();
                txtB3F5.Text = dt.Rows[0]["found_5"].ToString();
                txtB3Cl5.Text = dt.Rows[0]["claim_5"].ToString();
                txtB3P5.Text = dt.Rows[0]["per_5"].ToString();
                txtB3calculation6.Text = dt.Rows[0]["cal_6"].ToString();
                txtB3F6.Text = dt.Rows[0]["found_6"].ToString();
                txtB3Cl6.Text = dt.Rows[0]["claim_6"].ToString();
                txtB3P6.Text = dt.Rows[0]["per_6"].ToString();
                txtB3calculation7.Text = dt.Rows[0]["cal_7"].ToString();
                txtB3F7.Text = dt.Rows[0]["found_7"].ToString();
                txtB3Cl7.Text = dt.Rows[0]["claim_7"].ToString();
                txtB3P7.Text = dt.Rows[0]["per_7"].ToString();
                txtB3calculation8.Text = dt.Rows[0]["cal_8"].ToString();
                txtB3F8.Text = dt.Rows[0]["found_8"].ToString();
                txtB3Cl8.Text = dt.Rows[0]["claim_8"].ToString();
                txtB3P8.Text = dt.Rows[0]["per_8"].ToString();
                txtB3calculation9.Text = dt.Rows[0]["cal_9"].ToString();
                txtB3F9.Text = dt.Rows[0]["found_9"].ToString();
                txtB3Cl9.Text = dt.Rows[0]["claim_9"].ToString();
                txtB3P9.Text = dt.Rows[0]["per_9"].ToString();
                txtB3calculation10.Text = dt.Rows[0]["cal_10"].ToString();
                txtB3F10.Text = dt.Rows[0]["found_10"].ToString();
                txtB3Cl10.Text = dt.Rows[0]["claim_10"].ToString();
                txtB3P10.Text = dt.Rows[0]["per_10"].ToString();
                txtB3calculation11.Text = dt.Rows[0]["cal_11"].ToString();
                txtB3F11.Text = dt.Rows[0]["found_11"].ToString();
                txtB3Cl11.Text = dt.Rows[0]["claim_11"].ToString();
                txtB3P11.Text = dt.Rows[0]["per_11"].ToString();
                txtB3calculation12.Text = dt.Rows[0]["cal_12"].ToString();
                txtB3F12.Text = dt.Rows[0]["found_12"].ToString();
                txtB3Cl12.Text = dt.Rows[0]["claim_12"].ToString();
                txtB3P12.Text = dt.Rows[0]["per_12"].ToString();

                txtB3remarks.Text = dt.Rows[0]["Remarks"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindBufferstage()
    {
        try
        {

            objBE.SampleID = lblbssampleid.Text.Trim();
            objBE.Action = "RBS";
            dt = new DataTable();
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                //BS
                EdtBufferstage.Visible = true;
                txtbscal1.Text = dt.Rows[0]["bs1cal"].ToString();
                txtbsf1.Text = dt.Rows[0]["bs1f"].ToString();
                txtbscl1.Text = dt.Rows[0]["bs1c"].ToString();
                txtbsp1.Text = dt.Rows[0]["bs1p"].ToString();
                txtbscal2.Text = dt.Rows[0]["bs2cal"].ToString();
                txtbsf2.Text = dt.Rows[0]["bs2f"].ToString();
                txtbscl2.Text = dt.Rows[0]["bs2c"].ToString();
                txtbsp2.Text = dt.Rows[0]["bs2p"].ToString();
                txtbscal3.Text = dt.Rows[0]["bs3cal"].ToString();
                txtbsf3.Text = dt.Rows[0]["bs3f"].ToString();
                txtbscl3.Text = dt.Rows[0]["bs3c"].ToString();
                txtbsp3.Text = dt.Rows[0]["bs3p"].ToString();
                txtbscal4.Text = dt.Rows[0]["bs4cal"].ToString();
                txtbsf4.Text = dt.Rows[0]["bs4f"].ToString();
                txtbscl4.Text = dt.Rows[0]["bs4c"].ToString();
                txtbsp4.Text = dt.Rows[0]["bs4p"].ToString();
                txtbscal5.Text = dt.Rows[0]["bs5cal"].ToString();
                txtbsf5.Text = dt.Rows[0]["bs5f"].ToString();
                txtbscl5.Text = dt.Rows[0]["bs5c"].ToString();
                txtbsp5.Text = dt.Rows[0]["bs5p"].ToString();
                txtbscal6.Text = dt.Rows[0]["bs6cal"].ToString();
                txtbsf6.Text = dt.Rows[0]["bs6f"].ToString();
                txtbscl6.Text = dt.Rows[0]["bs6c"].ToString();
                txtbsp6.Text = dt.Rows[0]["bs6p"].ToString();


                txtbsremarks.Text = dt.Rows[0]["bsremarks"].ToString();



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
                addmore.Visible = false;
                EdtBufferstage.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;
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
                Btnadd.Visible = true;
                addmore.Visible = false;
                EdtBufferstage.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;
                BindTrGrid();
            }
            if (e.CommandName == "editS1")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbls1smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;
                EditS1.Visible = true;

                Edidescrmrks.Visible = false;
                addmore.Visible = false;
                EdtBufferstage.Visible = false;
                EditS1.Visible = true;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;
                BindS1();
            }
            if (e.CommandName == "editS2")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbls2smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;


                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;
                EdtBufferstage.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = true;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;
                BindS2();
            }
            if (e.CommandName == "editS3")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbls3smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;

                EditS3.Visible = true;

                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;
                EdtBufferstage.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;

                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;
                BindS3();
            }
            if (e.CommandName == "editA1")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbla1smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;


                EditA1.Visible = true;
                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;
                EdtBufferstage.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;

                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;

                BindA1();
            }
            if (e.CommandName == "editA2")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lblA2smpid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditA2.Visible = true;

                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;

                EditA3.Visible = false;
                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;

                BindA2();
            }
            if (e.CommandName == "editA3")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lbla3sampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditA3.Visible = true;

                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;

                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;

                BindA3();
            }
            if (e.CommandName == "editB1")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lblb1sampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;

                EditB1.Visible = true;
                EditTr.Visible = false;
                Edidescrmrks.Visible = false;
                addmore.Visible = false;
                EdtBufferstage.Visible = false;
                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = false;

                BindB1();
            }
            if (e.CommandName == "editB2")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lblb2sampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;

                Edidescrmrks.Visible = false;
                addmore.Visible = false;

                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = true;

                BindB2();
            }
            if (e.CommandName == "editB3")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lblb3sampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;

                Edidescrmrks.Visible = false;
                addmore.Visible = false;

                EditS1.Visible = false;
                EditS2.Visible = false;
                EditS3.Visible = false;
                EditA1.Visible = false;
                EditA2.Visible = false;
                EditA3.Visible = false;
                EditB1.Visible = false;
                EditB2.Visible = false;
                EditB3.Visible = true;

                BindB3();
            }

            if (e.CommandName == "BS")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblsampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                lblbssampleid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
                EditTr.Visible = false;

                Edidescrmrks.Visible = false;
                EditS2.Visible = false;

                EditS1.Visible = false;
                addmore.Visible = false;
                EditS3.Visible = false;
                EdtBufferstage.Visible = true;
                BindBufferstage();
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
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
                if (Session["RoleID"].ToString() == "5")
                {

                    objBE.AnalystId = Session["AnalystCode"].ToString();
                }
                if (Session["RoleID"].ToString() == "6")
                {

                    objBE.AnalystId = Uocode;
                }
                //  objBE.AnalystId = Session["AnalystCode"].ToString();
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
    protected void Gvedittest_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

        Gvedittest.EditIndex = -1;
        BindTrGrid();

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
                objBE.Action = "PARAM";
                objBE.SampleID = lblsampleid.Text.Trim();
                if (Session["RoleID"].ToString() == "5")
                {

                    objBE.AnalystId = Session["AnalystCode"].ToString();
                }
                if (Session["RoleID"].ToString() == "6")
                {

                    objBE.AnalystId = Uocode;
                }
                //  objBE.AnalystId = Session["AnalystCode"].ToString();
                dt = ObjDL.bulkdata(objBE, con);
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
            //Response.Redirect("~/Error.aspx");
        }
    }
    protected void GridTr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label testParam = (e.Row.FindControl("lblTestParameter") as Label);
            DropDownList ddlTestParameter = (DropDownList)e.Row.FindControl("ddlTestParameter");
            dt = new DataTable();
            objBE.Action = "PARAM";
            objBE.SampleID = lblsampleid.Text;

            if (Session["RoleID"].ToString() == "5")
                objBE.AnalystId = Session["AnalystCode"].ToString();
            if (Session["RoleID"].ToString() == "6")
                objBE.AnalystId = Uocode;

            dt = ObjDL.bulkdata(objBE, con);
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
                cf.ShowAlertMessage(dt.Rows[0][0].ToString());
            else
                cf.ShowAlertMessage("Test results saved");
                //clear();
                // pnltestresult.Visible = true;
           
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
    protected void Btnadd_Click(object sender, EventArgs e)
    {
        addmore.Visible = true;
        EditTr.Visible = true;

        Btnadd.Visible = false;
        edttrgrid.Visible = false;
    }
    protected void BtnS1_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (S1validate())
            {
                objBE.Action = "DS";
                objBE.flag = "S1U";
                objBE.SampleID = lblsampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));
                dtparam.Columns.Add("percentage_1", typeof(string));
                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));
                dtparam.Columns.Add("percentage_2", typeof(string));
                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));
                dtparam.Columns.Add("percentage_3", typeof(string));
                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));
                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));
                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));
                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));
                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));
                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));
                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));
                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));
                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));
                dtparam.Columns.Add("percentage_12", typeof(string));

                dtparam.Rows.Add();

                dtparam.Rows[0]["type1"] = "1";
                dtparam.Rows[0]["cal_1"] = txtS1calculation1.Text.Trim();
                dtparam.Rows[0]["found_1"] = txts1F1.Text.Trim();
                dtparam.Rows[0]["claim_1"] = txts1Cl1.Text.Trim();
                dtparam.Rows[0]["percentage_1"] = txts1P1.Text.Trim();
                dtparam.Rows[0]["type2"] = "2";
                dtparam.Rows[0]["cal_2"] = txtS1calculation2.Text.Trim();
                dtparam.Rows[0]["found_2"] = txts1F2.Text.Trim();
                dtparam.Rows[0]["claim_2"] = txts1Cl2.Text.Trim();
                dtparam.Rows[0]["percentage_2"] = txts1P2.Text.Trim();

                dtparam.Rows[0]["type3"] = "3";
                dtparam.Rows[0]["cal_3"] = txtS1calculation3.Text.Trim();
                dtparam.Rows[0]["found_3"] = txts1F3.Text.Trim();
                dtparam.Rows[0]["claim_3"] = txts1Cl3.Text.Trim();
                dtparam.Rows[0]["percentage_3"] = txts1P3.Text.Trim();

                dtparam.Rows[0]["type4"] = "4";
                dtparam.Rows[0]["cal_4"] = txtS1calculation4.Text.Trim();
                dtparam.Rows[0]["found_4"] = txts1F4.Text.Trim();
                dtparam.Rows[0]["claim_4"] = txts1Cl4.Text.Trim();

                dtparam.Rows[0]["percentage_4"] = txts1P4.Text.Trim();
                dtparam.Rows[0]["type5"] = "5";
                dtparam.Rows[0]["cal_5"] = txtS1calculation5.Text.Trim();
                dtparam.Rows[0]["found_5"] = txts1F5.Text.Trim();
                dtparam.Rows[0]["claim_5"] = txts1Cl5.Text.Trim();

                dtparam.Rows[0]["percentage_5"] = txts1P5.Text.Trim();
                dtparam.Rows[0]["type6"] = "6";
                dtparam.Rows[0]["cal_6"] = txtS1calculation6.Text.Trim();
                dtparam.Rows[0]["found_6"] = txts1F6.Text.Trim();
                dtparam.Rows[0]["claim_6"] = txts1Cl6.Text.Trim();

                dtparam.Rows[0]["percentage_6"] = txts1P6.Text.Trim();

                dtparam.Rows[0]["type7"] = null;
                dtparam.Rows[0]["cal_7"] = null;
                dtparam.Rows[0]["found_7"] = null;
                dtparam.Rows[0]["claim_7"] = null;

                dtparam.Rows[0]["percentage_7"] = null;
                dtparam.Rows[0]["type8"] = null;
                dtparam.Rows[0]["cal_8"] = null;
                dtparam.Rows[0]["found_8"] = null;
                dtparam.Rows[0]["claim_8"] = null;

                dtparam.Rows[0]["percentage_8"] = null;

                dtparam.Rows[0]["type9"] = null;
                dtparam.Rows[0]["cal_9"] = null;
                dtparam.Rows[0]["found_9"] = null;
                dtparam.Rows[0]["claim_9"] = null;

                dtparam.Rows[0]["percentage_9"] = null;

                dtparam.Rows[0]["type10"] = null;
                dtparam.Rows[0]["cal_10"] = null;
                dtparam.Rows[0]["found_10"] = null;
                dtparam.Rows[0]["claim_10"] = null;

                dtparam.Rows[0]["percentage_10"] = null;
                dtparam.Rows[0]["type10"] = null;
                dtparam.Rows[0]["cal_11"] = null;
                dtparam.Rows[0]["found_11"] = null;
                dtparam.Rows[0]["claim_11"] = null;

                dtparam.Rows[0]["percentage_11"] = null;
                dtparam.Rows[0]["type12"] = null;
                dtparam.Rows[0]["cal_12"] = null;
                dtparam.Rows[0]["found_12"] = null;
                dtparam.Rows[0]["claim_12"] = null;

                dtparam.Rows[0]["percentage_12"] = null;


                // }

                objBE.Ds1TVP = dtparam;
                objBE.Remarks = txts1Remrks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("S1 Dissolution results updated ");
                    //   clear();
                    BindGrid();
                    EditS1.Visible = false;


                }
            }


            //     }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect(ex.Message.ToString());
        }





    }
    protected void BtnS2_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (S2validate())
            {
                objBE.Action = "DS";
                objBE.flag = "S2U";
                objBE.SampleID = lblsampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;


                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));

                dtparam.Columns.Add("percentage_1", typeof(string));

                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));

                dtparam.Columns.Add("percentage_2", typeof(string));

                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));

                dtparam.Columns.Add("percentage_3", typeof(string));

                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));

                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));

                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));

                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));

                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));

                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));

                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));

                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));

                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));

                dtparam.Columns.Add("percentage_12", typeof(string));
                // for (int i = 0; i <= s1table.Rows.Count; i++)
                int i = 0;
                // for (int i = 0; i <= 0; i++)
                //{

                dtparam.Rows.Add();

                dtparam.Rows[i]["type1"] = "1";
                dtparam.Rows[i]["cal_1"] = txts2calculation1.Text.Trim();
                dtparam.Rows[i]["found_1"] = txts2F1.Text.Trim();
                dtparam.Rows[i]["claim_1"] = txts2Cl1.Text.Trim();

                dtparam.Rows[i]["percentage_1"] = txts2P1.Text.Trim();
                dtparam.Rows[i]["type2"] = "2";
                dtparam.Rows[i]["cal_2"] = txts2calculation2.Text.Trim();
                dtparam.Rows[i]["found_2"] = txts2F2.Text.Trim();
                dtparam.Rows[i]["claim_2"] = txts2Cl2.Text.Trim();

                dtparam.Rows[i]["percentage_2"] = txts2P2.Text.Trim();

                dtparam.Rows[i]["type3"] = "3";
                dtparam.Rows[i]["cal_3"] = txts2calculation3.Text.Trim();
                dtparam.Rows[i]["found_3"] = txts2F3.Text.Trim();
                dtparam.Rows[i]["claim_3"] = txts2Cl3.Text.Trim();

                dtparam.Rows[i]["percentage_3"] = txts2P3.Text.Trim();
                dtparam.Rows[i]["type4"] = "4";
                dtparam.Rows[i]["cal_4"] = txts2calculation4.Text.Trim();
                dtparam.Rows[i]["found_4"] = txts2F4.Text.Trim();
                dtparam.Rows[i]["claim_4"] = txts2Cl4.Text.Trim();

                dtparam.Rows[i]["percentage_4"] = txts2P4.Text.Trim();
                dtparam.Rows[i]["type5"] = "5";
                dtparam.Rows[i]["cal_5"] = txts2calculation5.Text.Trim();
                dtparam.Rows[i]["found_5"] = txts2F5.Text.Trim();
                dtparam.Rows[i]["claim_5"] = txts2Cl5.Text.Trim();

                dtparam.Rows[i]["percentage_5"] = txts2P5.Text.Trim();
                dtparam.Rows[i]["type6"] = "6";
                dtparam.Rows[i]["cal_6"] = txts2calculation6.Text.Trim();
                dtparam.Rows[i]["found_6"] = txts2F6.Text.Trim();
                dtparam.Rows[i]["claim_6"] = txts2Cl6.Text.Trim();

                dtparam.Rows[i]["percentage_6"] = txts2P6.Text.Trim();

                dtparam.Rows[i]["type7"] = null;
                dtparam.Rows[i]["cal_7"] = null;
                dtparam.Rows[i]["found_7"] = null;
                dtparam.Rows[i]["claim_7"] = null;

                dtparam.Rows[i]["percentage_7"] = null;
                dtparam.Rows[i]["type8"] = null;
                dtparam.Rows[i]["cal_8"] = null;
                dtparam.Rows[i]["found_8"] = null;
                dtparam.Rows[i]["claim_8"] = null;

                dtparam.Rows[i]["percentage_8"] = null;

                dtparam.Rows[i]["type9"] = null;
                dtparam.Rows[i]["cal_9"] = null;
                dtparam.Rows[i]["found_9"] = null;
                dtparam.Rows[i]["claim_9"] = null;

                dtparam.Rows[i]["percentage_9"] = null;

                dtparam.Rows[i]["type10"] = null;
                dtparam.Rows[i]["cal_10"] = null;
                dtparam.Rows[i]["found_10"] = null;
                dtparam.Rows[i]["claim_10"] = null;

                dtparam.Rows[i]["percentage_10"] = null;
                dtparam.Rows[i]["type10"] = null;
                dtparam.Rows[i]["cal_11"] = null;
                dtparam.Rows[i]["found_11"] = null;
                dtparam.Rows[i]["claim_11"] = null;

                dtparam.Rows[i]["percentage_11"] = null;
                dtparam.Rows[i]["type12"] = null;
                dtparam.Rows[i]["cal_12"] = null;
                dtparam.Rows[i]["found_12"] = null;
                dtparam.Rows[i]["claim_12"] = null;

                dtparam.Rows[i]["percentage_12"] = null;
                //}

                objBE.Ds2TVP = dtparam;
                objBE.Remarks = txts2remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("S2 Dissolution results Updated ");

                }
                EditS2.Visible = false;
                BindGrid();

            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }



    protected void BtnS3_Click(object sender, EventArgs e)
    {
        check();
        //S3
        try
        {
            if (S3validate())
            {

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));

                dtparam.Columns.Add("percentage_1", typeof(string));

                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));

                dtparam.Columns.Add("percentage_2", typeof(string));

                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));

                dtparam.Columns.Add("percentage_3", typeof(string));

                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));

                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));

                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));

                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));

                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));

                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));

                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));

                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));

                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));

                dtparam.Columns.Add("percentage_12", typeof(string));
                // for (int i = 0; i <= s1table.Rows.Count; i++)
                int i = 0;
                // for (int i = 0; i <= 0; i++)
                //{

                dtparam.Rows.Add();

                dtparam.Rows[i]["type1"] = "1";
                dtparam.Rows[i]["cal_1"] = txts3calculation1.Text.Trim();
                dtparam.Rows[i]["found_1"] = txts3F1.Text.Trim();
                dtparam.Rows[i]["claim_1"] = txts3Cl1.Text.Trim();

                dtparam.Rows[i]["percentage_1"] = txts3P1.Text.Trim();
                dtparam.Rows[i]["type2"] = "2";
                dtparam.Rows[i]["cal_2"] = txts3calculation2.Text.Trim();
                dtparam.Rows[i]["found_2"] = txts3F2.Text.Trim();
                dtparam.Rows[i]["claim_2"] = txts3Cl2.Text.Trim();

                dtparam.Rows[i]["percentage_2"] = txts3P2.Text.Trim();

                dtparam.Rows[i]["type3"] = "3";
                dtparam.Rows[i]["cal_3"] = txts3calculation3.Text.Trim();
                dtparam.Rows[i]["found_3"] = txts3F3.Text.Trim();
                dtparam.Rows[i]["claim_3"] = txts3Cl3.Text.Trim();

                dtparam.Rows[i]["percentage_3"] = txts3P3.Text.Trim();
                dtparam.Rows[i]["type4"] = "4";
                dtparam.Rows[i]["cal_4"] = txts3calculation4.Text.Trim();
                dtparam.Rows[i]["found_4"] = txts3F4.Text.Trim();
                dtparam.Rows[i]["claim_4"] = txts3Cl4.Text.Trim();

                dtparam.Rows[i]["percentage_4"] = txts3P4.Text.Trim();
                dtparam.Rows[i]["type5"] = "5";
                dtparam.Rows[i]["cal_5"] = txts3calculation5.Text.Trim();
                dtparam.Rows[i]["found_5"] = txts3F5.Text.Trim();
                dtparam.Rows[i]["claim_5"] = txts3Cl5.Text.Trim();

                dtparam.Rows[i]["percentage_5"] = txts3P5.Text.Trim();
                dtparam.Rows[i]["type6"] = "6";
                dtparam.Rows[i]["cal_6"] = txts3calculation6.Text.Trim();
                dtparam.Rows[i]["found_6"] = txts3F6.Text.Trim();
                dtparam.Rows[i]["claim_6"] = txts3Cl6.Text.Trim();

                dtparam.Rows[i]["percentage_6"] = txts3P6.Text.Trim();

                dtparam.Rows[i]["type7"] = "7";
                dtparam.Rows[i]["cal_7"] = txts3calculation7.Text.Trim();
                dtparam.Rows[i]["found_7"] = txts3F7.Text.Trim();
                dtparam.Rows[i]["claim_7"] = txts3Cl7.Text.Trim();

                dtparam.Rows[i]["percentage_7"] = txts3P7.Text.Trim();

                dtparam.Rows[i]["type8"] = "8";
                dtparam.Rows[i]["cal_8"] = txts3calculation8.Text.Trim();
                dtparam.Rows[i]["found_8"] = txts3F8.Text.Trim();
                dtparam.Rows[i]["claim_8"] = txts3Cl8.Text.Trim();

                dtparam.Rows[i]["percentage_8"] = txts3P8.Text.Trim();

                dtparam.Rows[i]["type9"] = "9";
                dtparam.Rows[i]["cal_9"] = txts3calculation9.Text.Trim();
                dtparam.Rows[i]["found_9"] = txts3F9.Text.Trim();
                dtparam.Rows[i]["claim_9"] = txts3Cl9.Text.Trim();

                dtparam.Rows[i]["percentage_9"] = txts3P9.Text.Trim();
                dtparam.Rows[i]["type10"] = "10";

                dtparam.Rows[i]["cal_10"] = txts3calculation10.Text.Trim();
                dtparam.Rows[i]["found_10"] = txts3F10.Text.Trim();
                dtparam.Rows[i]["claim_10"] = txts3Cl10.Text.Trim();

                dtparam.Rows[i]["percentage_10"] = txts3P10.Text.Trim();

                dtparam.Rows[i]["type11"] = "11";
                dtparam.Rows[i]["cal_11"] = txts3calculation11.Text.Trim();
                dtparam.Rows[i]["found_11"] = txts3F11.Text.Trim();
                dtparam.Rows[i]["claim_11"] = txts3Cl11.Text.Trim();

                dtparam.Rows[i]["percentage_11"] = txts3P11.Text.Trim();

                dtparam.Rows[i]["type12"] = "12";
                dtparam.Rows[i]["cal_12"] = txts3calculation12.Text.Trim();
                dtparam.Rows[i]["found_12"] = txts3F12.Text.Trim();
                dtparam.Rows[i]["claim_12"] = txts3Cl12.Text.Trim();

                dtparam.Rows[i]["percentage_12"] = txts3P12.Text.Trim();
                //}
                objBE.Action = "DS";
                objBE.flag = "S3U";
                objBE.SampleID = lblsampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;

                objBE.Ds3TVP = dtparam;
                objBE.Remarks = txts3remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("S3 Dissolution results Updated ");

                }
                EditS3.Visible = false;
                BindGrid();

            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect(ex.Message.ToString());
        }





    }
    protected void btnbuffer_Click(object sender, EventArgs e)
    {

        check();
        try
        {
            //if (S2validate())
            //{
            //S2
            objBE.SampleID = lblbssampleid.Text.Trim();
            objBE.bsCalculation1 = txtbscal1.Text.Trim();
            objBE.bsFound1 = txtbsf1.Text.Trim();
            objBE.bsClaim1 = txtbscl1.Text.Trim();
            objBE.bsPercentage1 = txtbsp1.Text.Trim();
            objBE.bsCalculation2 = txtbscal2.Text.Trim();
            objBE.bsFound2 = txtbsf2.Text.Trim();
            objBE.bsClaim2 = txtbscl2.Text.Trim();
            objBE.bsPercentage2 = txtbsp2.Text.Trim();
            objBE.bsCalculation3 = txtbscal3.Text.Trim();
            objBE.bsFound3 = txtbsf3.Text.Trim();
            objBE.bsClaim3 = txtbscl3.Text.Trim();
            objBE.bsPercentage3 = txtbsp3.Text.Trim();
            objBE.bsCalculation4 = txtbscal4.Text.Trim();
            objBE.bsFound4 = txtbsf4.Text.Trim();
            objBE.bsClaim4 = txtbscl4.Text.Trim();
            objBE.bsPercentage4 = txtbsp4.Text.Trim();
            objBE.bsCalculation5 = txtbscal5.Text.Trim();
            objBE.bsFound5 = txtbsf5.Text.Trim();
            objBE.bsClaim5 = txtbscl5.Text.Trim();
            objBE.bsPercentage5 = txtbsp5.Text.Trim();
            objBE.bsCalculation6 = txtbscal6.Text.Trim();
            objBE.bsFound6 = txtbsf6.Text.Trim();
            objBE.bsClaim6 = txtbscl6.Text.Trim();
            objBE.bsPercentage6 = txtbsp6.Text.Trim();

            objBE.bsRemarks = txtbsremarks.Text.Trim();
            objBE.Action = "BSU";
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Unable to save test result");
            }
            else
            {
                cf.ShowAlertMessage(" Buffer Stage Test results saved");
                //clear();
                EdtBufferstage.Visible = false;
            }
            // }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
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
        
            //else if (ddlpr.SelectedIndex == 0)
            //{
            //    cf.ShowAlertMessage("Select Test Protocol");
            //    ddlpr.Focus();
            //    return false;
            //}
            //else if (fnd.Text == "")
            //{
            //    cf.ShowAlertMessage("Enter Found Values");
            //    fnd.Focus();
            //    return false;
            //}
            //else if (clm.Text == "")
            //{
            //    cf.ShowAlertMessage("Enter Claim Values");
            //    clm.Focus();
            //    return false;
            //}
            //else if (lmt.Text == "")
            //{
            //    cf.ShowAlertMessage("Enter Limit Values");
            //    lmt.Focus();
            //    return false;
            //}

        }
        return true;
    }
    protected void BtnA1_Click(object sender, EventArgs e)
    {
        try
        {
            if (A1validate())
            {

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));
                dtparam.Columns.Add("percentage_1", typeof(string));
                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));
                dtparam.Columns.Add("percentage_2", typeof(string));
                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));
                dtparam.Columns.Add("percentage_3", typeof(string));
                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));
                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));
                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));
                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));
                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));
                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));
                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));
                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));
                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));
                dtparam.Columns.Add("percentage_12", typeof(string));
                // for (int i = 0; i <= s1table.Rows.Count; i++)

                //for (int i = 0; i <= 0; i++)
                //{

                dtparam.Rows.Add();

                dtparam.Rows[0]["type1"] = "1";
                dtparam.Rows[0]["cal_1"] = txtA1calculation1.Text.Trim();
                dtparam.Rows[0]["found_1"] = txtA1F1.Text.Trim();
                dtparam.Rows[0]["claim_1"] = txtA1Cl1.Text.Trim();

                dtparam.Rows[0]["percentage_1"] = txtA1P1.Text.Trim();
                dtparam.Rows[0]["type2"] = "2";
                dtparam.Rows[0]["cal_2"] = txtA1calculation2.Text.Trim();
                dtparam.Rows[0]["found_2"] = txtA1F2.Text.Trim();
                dtparam.Rows[0]["claim_2"] = txtA1Cl2.Text.Trim();
                dtparam.Rows[0]["percentage_2"] = txtA1P2.Text.Trim();

                dtparam.Rows[0]["type3"] = "3";
                dtparam.Rows[0]["cal_3"] = txtA1calculation3.Text.Trim();
                dtparam.Rows[0]["found_3"] = txtA1F3.Text.Trim();
                dtparam.Rows[0]["claim_3"] = txtA1Cl3.Text.Trim();

                dtparam.Rows[0]["percentage_3"] = txtA1P3.Text.Trim();

                dtparam.Rows[0]["type4"] = "4";
                dtparam.Rows[0]["cal_4"] = txtA1calculation4.Text.Trim(); ;
                dtparam.Rows[0]["found_4"] = txtA1F4.Text.Trim();
                dtparam.Rows[0]["claim_4"] = txtA1Cl4.Text.Trim();
                dtparam.Rows[0]["percentage_4"] = txtA1P4.Text.Trim();

                dtparam.Rows[0]["type5"] = "5";
                dtparam.Rows[0]["cal_5"] = txtA1calculation5.Text.Trim();
                dtparam.Rows[0]["found_5"] = txtA1F5.Text.Trim();
                dtparam.Rows[0]["claim_5"] = txtA1Cl5.Text.Trim();
                dtparam.Rows[0]["percentage_5"] = txtA1P5.Text.Trim();

                dtparam.Rows[0]["type6"] = "6";
                dtparam.Rows[0]["cal_6"] = txtA1calculation6.Text.Trim();
                dtparam.Rows[0]["found_6"] = txtA1F6.Text.Trim();
                dtparam.Rows[0]["claim_6"] = txtA1Cl6.Text.Trim();
                dtparam.Rows[0]["percentage_6"] = txtA1P6.Text.Trim();

                dtparam.Rows[0]["type7"] = null;
                dtparam.Rows[0]["cal_7"] = null;
                dtparam.Rows[0]["found_7"] = null;
                dtparam.Rows[0]["claim_7"] = null;
                dtparam.Rows[0]["percentage_7"] = null;
                dtparam.Rows[0]["type8"] = null;
                dtparam.Rows[0]["cal_8"] = null;
                dtparam.Rows[0]["found_8"] = null;
                dtparam.Rows[0]["claim_8"] = null;
                dtparam.Rows[0]["percentage_8"] = null;

                dtparam.Rows[0]["type9"] = null;
                dtparam.Rows[0]["cal_9"] = null;
                dtparam.Rows[0]["found_9"] = null;
                dtparam.Rows[0]["claim_9"] = null;
                dtparam.Rows[0]["percentage_9"] = null;

                dtparam.Rows[0]["type10"] = null;
                dtparam.Rows[0]["cal_10"] = null;
                dtparam.Rows[0]["found_10"] = null;
                dtparam.Rows[0]["claim_10"] = null;
                dtparam.Rows[0]["percentage_10"] = null;
                dtparam.Rows[0]["type10"] = null;
                dtparam.Rows[0]["cal_11"] = null;
                dtparam.Rows[0]["found_11"] = null;
                dtparam.Rows[0]["claim_11"] = null;
                dtparam.Rows[0]["percentage_11"] = null;
                dtparam.Rows[0]["type12"] = null;
                dtparam.Rows[0]["cal_12"] = null;
                dtparam.Rows[0]["found_12"] = null;
                dtparam.Rows[0]["claim_12"] = null;
                dtparam.Rows[0]["percentage_12"] = null;                // }


                // }

                objBE.Ds1TVP = dtparam;
                objBE.Remarks = txtA1Remrks.Text;
                objBE.Action = "DS";
                objBE.flag = "A1U";
                objBE.SampleID = lbla1smpid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("A1 Dissolution results Updated");
                    //   clear();

                }
                EditA1.Visible = false;
                // BindGrid();


            }

        }


        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }
    protected void BtnA2_Click(object sender, EventArgs e)
    {
        try
        {
            if (A2validate())
            {
                objBE.Action = "DS";
                objBE.flag = "A2U";
                objBE.SampleID = lblsampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));
                dtparam.Columns.Add("percentage_1", typeof(string));
                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));
                dtparam.Columns.Add("percentage_2", typeof(string));
                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));
                dtparam.Columns.Add("percentage_3", typeof(string));
                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));
                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));
                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));
                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));
                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));
                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));
                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));
                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));
                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));
                dtparam.Columns.Add("percentage_12", typeof(string));
                // for (int i = 0; i <= s1table.Rows.Count; i++)
                int i = 0;
                // for (int i = 0; i <= 0; i++)
                //{

                dtparam.Rows.Add();

                dtparam.Rows[i]["type1"] = "1";
                dtparam.Rows[i]["cal_1"] = txtA2calculation1.Text.Trim();
                dtparam.Rows[i]["found_1"] = txtA2F1.Text.Trim();
                dtparam.Rows[i]["claim_1"] = txtA2Cl1.Text.Trim();
                dtparam.Rows[i]["percentage_1"] = txtA2P1.Text.Trim();
                dtparam.Rows[i]["type2"] = "2";
                dtparam.Rows[i]["cal_2"] = txtA2calculation2.Text.Trim();
                dtparam.Rows[i]["found_2"] = txtA2F2.Text.Trim();
                dtparam.Rows[i]["claim_2"] = txtA2Cl2.Text.Trim();
                dtparam.Rows[i]["percentage_2"] = txtA2P2.Text.Trim();

                dtparam.Rows[i]["type3"] = "3";
                dtparam.Rows[i]["cal_3"] = txtA2calculation3.Text.Trim();
                dtparam.Rows[i]["found_3"] = txtA2F3.Text.Trim();
                dtparam.Rows[i]["claim_3"] = txtA2Cl3.Text.Trim();
                dtparam.Rows[i]["percentage_3"] = txtA2P3.Text.Trim();
                dtparam.Rows[i]["type4"] = "4";
                dtparam.Rows[i]["cal_4"] = txtA2calculation4.Text.Trim();
                dtparam.Rows[i]["found_4"] = txtA2F4.Text.Trim();
                dtparam.Rows[i]["claim_4"] = txtA2Cl4.Text.Trim();
                dtparam.Rows[i]["percentage_4"] = txtA2P4.Text.Trim();
                dtparam.Rows[i]["type5"] = "5";
                dtparam.Rows[i]["cal_5"] = txtA2calculation5.Text.Trim();
                dtparam.Rows[i]["found_5"] = txtA2F5.Text.Trim();
                dtparam.Rows[i]["claim_5"] = txtA2Cl5.Text.Trim();
                dtparam.Rows[i]["percentage_5"] = txtA2P5.Text.Trim();
                dtparam.Rows[i]["type6"] = "6";
                dtparam.Rows[i]["cal_6"] = txtA2calculation6.Text.Trim();
                dtparam.Rows[i]["found_6"] = txtA2F6.Text.Trim();
                dtparam.Rows[i]["claim_6"] = txtA2Cl6.Text.Trim();
                dtparam.Rows[i]["percentage_6"] = txtA2P6.Text.Trim();

                dtparam.Rows[i]["type7"] = null;
                dtparam.Rows[i]["cal_7"] = null;
                dtparam.Rows[i]["found_7"] = null;
                dtparam.Rows[i]["claim_7"] = null;
                dtparam.Rows[i]["percentage_7"] = null;
                dtparam.Rows[i]["type8"] = null;
                dtparam.Rows[i]["cal_8"] = null;
                dtparam.Rows[i]["found_8"] = null;
                dtparam.Rows[i]["claim_8"] = null;
                dtparam.Rows[i]["percentage_8"] = null;

                dtparam.Rows[i]["type9"] = null;
                dtparam.Rows[i]["cal_9"] = null;
                dtparam.Rows[i]["found_9"] = null;
                dtparam.Rows[i]["claim_9"] = null;
                dtparam.Rows[i]["percentage_9"] = null;

                dtparam.Rows[i]["type10"] = null;
                dtparam.Rows[i]["cal_10"] = null;
                dtparam.Rows[i]["found_10"] = null;
                dtparam.Rows[i]["claim_10"] = null;
                dtparam.Rows[i]["percentage_10"] = null;
                dtparam.Rows[i]["type10"] = null;
                dtparam.Rows[i]["cal_11"] = null;
                dtparam.Rows[i]["found_11"] = null;
                dtparam.Rows[i]["claim_11"] = null;
                dtparam.Rows[i]["percentage_11"] = null;
                dtparam.Rows[i]["type12"] = null;
                dtparam.Rows[i]["cal_12"] = null;
                dtparam.Rows[i]["found_12"] = null;
                dtparam.Rows[i]["claim_12"] = null;
                dtparam.Rows[i]["percentage_12"] = null;

                //}

                objBE.Ds2TVP = dtparam;
                objBE.Remarks = txtA2remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("A2 Dissolution results Updated ");

                }
                EditA2.Visible = false;
                // BindGrid();

            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect(ex.Message.ToString());
        }

    }
    protected void BtnA3_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (A3validate())
            {


                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));
                dtparam.Columns.Add("percentage_1", typeof(string));
                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));
                dtparam.Columns.Add("percentage_2", typeof(string));
                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));
                dtparam.Columns.Add("percentage_3", typeof(string));
                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));
                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));
                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));
                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));
                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));
                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));
                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));
                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));
                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));
                dtparam.Columns.Add("percentage_12", typeof(string));

                int i = 0;
                dtparam.Rows.Add();

                dtparam.Rows[i]["type1"] = "1";
                dtparam.Rows[i]["cal_1"] = txtA3calculation1.Text.Trim();
                dtparam.Rows[i]["found_1"] = txtA3F1.Text.Trim();
                dtparam.Rows[i]["claim_1"] = txtA3Cl1.Text.Trim();
                dtparam.Rows[i]["percentage_1"] = txtA3P1.Text.Trim();
                dtparam.Rows[i]["type2"] = "2";
                dtparam.Rows[i]["cal_2"] = txtA3calculation2.Text.Trim();
                dtparam.Rows[i]["found_2"] = txtA3F2.Text.Trim();
                dtparam.Rows[i]["claim_2"] = txtA3Cl2.Text.Trim();
                dtparam.Rows[i]["percentage_2"] = txtA3P2.Text.Trim();

                dtparam.Rows[i]["type3"] = "3";
                dtparam.Rows[i]["cal_3"] = txtA3calculation3.Text.Trim();
                dtparam.Rows[i]["found_3"] = txtA3F3.Text.Trim();
                dtparam.Rows[i]["claim_3"] = txtA3Cl3.Text.Trim();
                dtparam.Rows[i]["percentage_3"] = txtA3P3.Text.Trim();
                dtparam.Rows[i]["type4"] = "4";
                dtparam.Rows[i]["cal_4"] = txtA3calculation4.Text.Trim();
                dtparam.Rows[i]["found_4"] = txtA3F4.Text.Trim();
                dtparam.Rows[i]["claim_4"] = txtA3Cl4.Text.Trim();
                dtparam.Rows[i]["percentage_4"] = txtA3P4.Text.Trim();
                dtparam.Rows[i]["type5"] = "5";
                dtparam.Rows[i]["cal_5"] = txtA3calculation5.Text.Trim();
                dtparam.Rows[i]["found_5"] = txtA3F5.Text.Trim();
                dtparam.Rows[i]["claim_5"] = txtA3Cl5.Text.Trim();
                dtparam.Rows[i]["percentage_5"] = txtA3P5.Text.Trim();
                dtparam.Rows[i]["type6"] = "6";
                dtparam.Rows[i]["cal_6"] = txtA3calculation6.Text.Trim();
                dtparam.Rows[i]["found_6"] = txtA3F6.Text.Trim();
                dtparam.Rows[i]["claim_6"] = txtA3Cl6.Text.Trim();
                dtparam.Rows[i]["percentage_6"] = txtA3P6.Text.Trim();

                dtparam.Rows[i]["type7"] = "7";
                dtparam.Rows[i]["cal_7"] = txtA3calculation7.Text.Trim();
                dtparam.Rows[i]["found_7"] = txtA3F7.Text.Trim();
                dtparam.Rows[i]["claim_7"] = txtA3Cl7.Text.Trim();
                dtparam.Rows[i]["percentage_7"] = txtA3P7.Text.Trim();

                dtparam.Rows[i]["type8"] = "8";
                dtparam.Rows[i]["cal_8"] = txtA3calculation8.Text.Trim();
                dtparam.Rows[i]["found_8"] = txtA3F8.Text.Trim();
                dtparam.Rows[i]["claim_8"] = txtA3Cl8.Text.Trim();
                dtparam.Rows[i]["percentage_8"] = txtA3P8.Text.Trim();

                dtparam.Rows[i]["type9"] = "9";
                dtparam.Rows[i]["cal_9"] = txtA3calculation9.Text.Trim();
                dtparam.Rows[i]["found_9"] = txtA3F9.Text.Trim();
                dtparam.Rows[i]["claim_9"] = txtA3Cl9.Text.Trim();
                dtparam.Rows[i]["percentage_9"] = txtA3P9.Text.Trim();
                dtparam.Rows[i]["type10"] = "10";

                dtparam.Rows[i]["cal_10"] = txtA3calculation10.Text.Trim();
                dtparam.Rows[i]["found_10"] = txtA3F10.Text.Trim();
                dtparam.Rows[i]["claim_10"] = txtA3Cl10.Text.Trim();
                dtparam.Rows[i]["percentage_10"] = txtA3P10.Text.Trim();

                dtparam.Rows[i]["type11"] = "11";
                dtparam.Rows[i]["cal_11"] = txtA3calculation11.Text.Trim();
                dtparam.Rows[i]["found_11"] = txtA3F11.Text.Trim();
                dtparam.Rows[i]["claim_11"] = txtA3Cl11.Text.Trim();
                dtparam.Rows[i]["percentage_11"] = txtA3P11.Text.Trim();

                dtparam.Rows[i]["type12"] = "12";
                dtparam.Rows[i]["cal_12"] = txtA3calculation12.Text.Trim();
                dtparam.Rows[i]["found_12"] = txtA3F12.Text.Trim();
                dtparam.Rows[i]["claim_12"] = txtA3Cl12.Text.Trim();
                dtparam.Rows[i]["percentage_12"] = txtA3P12.Text.Trim();

                objBE.Ds3TVP = dtparam;
                objBE.Action = "DS";
                objBE.flag = "A3U";
                objBE.SampleID = lbla3sampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;
                objBE.Remarks = txtA3remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("A3 Dissolution results Updated ");
                    //   clear();

                }
                EditA3.Visible = false;
                BindGrid();


            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void Btnb1_Click(object sender, EventArgs e)
    {
        check();

        try
        {
            if (B1validate())
            {


                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));
                dtparam.Columns.Add("percentage_1", typeof(string));
                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));
                dtparam.Columns.Add("percentage_2", typeof(string));
                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));
                dtparam.Columns.Add("percentage_3", typeof(string));
                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));
                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));
                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));
                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));
                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));
                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));
                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));
                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));
                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));
                dtparam.Columns.Add("percentage_12", typeof(string));

                // for (int i = 0; i <= s1table.Rows.Count; i++)

                //for (int i = 0; i <= 0; i++)
                //{

                dtparam.Rows.Add();

                dtparam.Rows[0]["type1"] = "1";
                dtparam.Rows[0]["cal_1"] = txtB1calculation1.Text.Trim();
                dtparam.Rows[0]["found_1"] = txtB1F1.Text.Trim();
                dtparam.Rows[0]["claim_1"] = txtB1Cl1.Text.Trim();
                dtparam.Rows[0]["percentage_1"] = txtB1P1.Text.Trim();
                dtparam.Rows[0]["type2"] = "2";
                dtparam.Rows[0]["cal_2"] = txtB1calculation2.Text.Trim();
                dtparam.Rows[0]["found_2"] = txtB1F2.Text.Trim();
                dtparam.Rows[0]["claim_2"] = txtB1Cl2.Text.Trim();
                dtparam.Rows[0]["percentage_2"] = txtB1P2.Text.Trim();

                dtparam.Rows[0]["type3"] = "3";
                dtparam.Rows[0]["cal_3"] = txtB1calculation3.Text.Trim();
                dtparam.Rows[0]["found_3"] = txtB1F3.Text.Trim();
                dtparam.Rows[0]["claim_3"] = txtB1Cl3.Text.Trim();
                dtparam.Rows[0]["percentage_3"] = txtB1P3.Text.Trim();

                dtparam.Rows[0]["type4"] = "4";
                dtparam.Rows[0]["cal_4"] = txtB1calculation4.Text.Trim();
                dtparam.Rows[0]["found_4"] = txtB1F4.Text.Trim();
                dtparam.Rows[0]["claim_4"] = txtB1Cl4.Text.Trim();
                dtparam.Rows[0]["percentage_4"] = txtB1P4.Text.Trim();

                dtparam.Rows[0]["type5"] = "5";
                dtparam.Rows[0]["cal_5"] = txtB1calculation5.Text.Trim();
                dtparam.Rows[0]["found_5"] = txtB1F5.Text.Trim();
                dtparam.Rows[0]["claim_5"] = txtB1Cl5.Text.Trim();
                dtparam.Rows[0]["percentage_5"] = txtB1P5.Text.Trim();

                dtparam.Rows[0]["type6"] = "6";
                dtparam.Rows[0]["cal_6"] = txtB1calculation6.Text.Trim();
                dtparam.Rows[0]["found_6"] = txtB1F6.Text.Trim();
                dtparam.Rows[0]["claim_6"] = txtB1Cl6.Text.Trim();
                dtparam.Rows[0]["percentage_6"] = txtB1P6.Text.Trim();

                dtparam.Rows[0]["type7"] = null;
                dtparam.Rows[0]["cal_7"] = null;
                dtparam.Rows[0]["found_7"] = null;
                dtparam.Rows[0]["claim_7"] = null;
                dtparam.Rows[0]["percentage_7"] = null;
                dtparam.Rows[0]["type8"] = null;
                dtparam.Rows[0]["cal_8"] = null;
                dtparam.Rows[0]["found_8"] = null;
                dtparam.Rows[0]["claim_8"] = null;
                dtparam.Rows[0]["percentage_8"] = null;

                dtparam.Rows[0]["type9"] = null;
                dtparam.Rows[0]["cal_9"] = null;
                dtparam.Rows[0]["found_9"] = null;
                dtparam.Rows[0]["claim_9"] = null;
                dtparam.Rows[0]["percentage_9"] = null;

                dtparam.Rows[0]["type10"] = null;
                dtparam.Rows[0]["cal_10"] = null;
                dtparam.Rows[0]["found_10"] = null;
                dtparam.Rows[0]["claim_10"] = null;
                dtparam.Rows[0]["percentage_10"] = null;
                dtparam.Rows[0]["type10"] = null;
                dtparam.Rows[0]["cal_11"] = null;
                dtparam.Rows[0]["found_11"] = null;
                dtparam.Rows[0]["claim_11"] = null;
                dtparam.Rows[0]["percentage_11"] = null;
                dtparam.Rows[0]["type12"] = null;
                dtparam.Rows[0]["cal_12"] = null;
                dtparam.Rows[0]["found_12"] = null;
                dtparam.Rows[0]["claim_12"] = null;
                dtparam.Rows[0]["percentage_12"] = null;

                // }
                objBE.Action = "DS";
                objBE.flag = "B1U";
                objBE.SampleID = lblb1sampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;
                objBE.Ds1TVP = dtparam;
                objBE.Remarks = txtB1Remrks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("B1 Dissolution results Updated ");
                    //   clear();

                }
                EditB1.Visible = false;
                BindGrid();


            }

        }


        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }



    }
    protected void BtnB2_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (B2validate())
            {

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));
                dtparam.Columns.Add("percentage_1", typeof(string));
                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));
                dtparam.Columns.Add("percentage_2", typeof(string));
                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));
                dtparam.Columns.Add("percentage_3", typeof(string));
                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));
                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));
                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));
                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));
                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));
                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));
                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));
                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));
                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));
                dtparam.Columns.Add("percentage_12", typeof(string));
                // for (int i = 0; i <= s1table.Rows.Count; i++)
                int i = 0;
                // for (int i = 0; i <= 0; i++)
                //{

                dtparam.Rows.Add();

                dtparam.Rows[i]["type1"] = "1";
                dtparam.Rows[i]["cal_1"] = txtB2calculation1.Text.Trim();
                dtparam.Rows[i]["found_1"] = txtB2F1.Text.Trim();
                dtparam.Rows[i]["claim_1"] = txtB2Cl1.Text.Trim();
                dtparam.Rows[i]["percentage_1"] = txtB2P1.Text.Trim();
                dtparam.Rows[i]["type2"] = "2";
                dtparam.Rows[i]["cal_2"] = txtB2calculation2.Text.Trim();
                dtparam.Rows[i]["found_2"] = txtB2F2.Text.Trim();
                dtparam.Rows[i]["claim_2"] = txtB2Cl2.Text.Trim();
                dtparam.Rows[i]["percentage_2"] = txtB2P2.Text.Trim();

                dtparam.Rows[i]["type3"] = "3";
                dtparam.Rows[i]["cal_3"] = txtB2calculation3.Text.Trim();
                dtparam.Rows[i]["found_3"] = txtB2F3.Text.Trim();
                dtparam.Rows[i]["claim_3"] = txtB2Cl3.Text.Trim();
                dtparam.Rows[i]["percentage_3"] = txtB2P3.Text.Trim();
                dtparam.Rows[i]["type4"] = "4";
                dtparam.Rows[i]["cal_4"] = txtB2calculation4.Text.Trim();
                dtparam.Rows[i]["found_4"] = txtB2F4.Text.Trim();
                dtparam.Rows[i]["claim_4"] = txtB2Cl4.Text.Trim();
                dtparam.Rows[i]["percentage_4"] = txtB2P4.Text.Trim();
                dtparam.Rows[i]["type5"] = "5";
                dtparam.Rows[i]["cal_5"] = txtB2calculation5.Text.Trim();
                dtparam.Rows[i]["found_5"] = txtB2F5.Text.Trim();
                dtparam.Rows[i]["claim_5"] = txtB2Cl5.Text.Trim();
                dtparam.Rows[i]["percentage_5"] = txtB2P5.Text.Trim();
                dtparam.Rows[i]["type6"] = "6";
                dtparam.Rows[i]["cal_6"] = txtB2calculation6.Text.Trim();
                dtparam.Rows[i]["found_6"] = txtB2F6.Text.Trim();
                dtparam.Rows[i]["claim_6"] = txtB2Cl6.Text.Trim();
                dtparam.Rows[i]["percentage_6"] = txtB2P6.Text.Trim();

                dtparam.Rows[i]["type7"] = null;
                dtparam.Rows[i]["cal_7"] = null;
                dtparam.Rows[i]["found_7"] = null;
                dtparam.Rows[i]["claim_7"] = null;
                dtparam.Rows[i]["percentage_7"] = null;
                dtparam.Rows[i]["type8"] = null;
                dtparam.Rows[i]["cal_8"] = null;
                dtparam.Rows[i]["found_8"] = null;
                dtparam.Rows[i]["claim_8"] = null;
                dtparam.Rows[i]["percentage_8"] = null;

                dtparam.Rows[i]["type9"] = null;
                dtparam.Rows[i]["cal_9"] = null;
                dtparam.Rows[i]["found_9"] = null;
                dtparam.Rows[i]["claim_9"] = null;
                dtparam.Rows[i]["percentage_9"] = null;

                dtparam.Rows[i]["type10"] = null;
                dtparam.Rows[i]["cal_10"] = null;
                dtparam.Rows[i]["found_10"] = null;
                dtparam.Rows[i]["claim_10"] = null;
                dtparam.Rows[i]["percentage_10"] = null;
                dtparam.Rows[i]["type10"] = null;
                dtparam.Rows[i]["cal_11"] = null;
                dtparam.Rows[i]["found_11"] = null;
                dtparam.Rows[i]["claim_11"] = null;
                dtparam.Rows[i]["percentage_11"] = null;
                dtparam.Rows[i]["type12"] = null;
                dtparam.Rows[i]["cal_12"] = null;
                dtparam.Rows[i]["found_12"] = null;
                dtparam.Rows[i]["claim_12"] = null;
                dtparam.Rows[i]["percentage_12"] = null;

                //}
                objBE.Action = "DS";
                objBE.flag = "B2U";
                objBE.SampleID = lblb2sampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;

                objBE.Ds2TVP = dtparam;
                objBE.Remarks = txtB2remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("B2 Dissolution results saved ");
                    //   clear();

                }
                EditB2.Visible = false;
                BindGrid();

            }

        }


        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }



    }
    protected void BtnB3_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (B3validate())
            // if (validate())
            {

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("type1", typeof(string));
                dtparam.Columns.Add("cal_1", typeof(string));
                dtparam.Columns.Add("found_1", typeof(string));
                dtparam.Columns.Add("claim_1", typeof(string));
                dtparam.Columns.Add("percentage_1", typeof(string));
                dtparam.Columns.Add("type2", typeof(string));
                dtparam.Columns.Add("cal_2", typeof(string));
                dtparam.Columns.Add("found_2", typeof(string));
                dtparam.Columns.Add("claim_2", typeof(string));
                dtparam.Columns.Add("percentage_2", typeof(string));
                dtparam.Columns.Add("type3", typeof(string));
                dtparam.Columns.Add("cal_3", typeof(string));
                dtparam.Columns.Add("found_3", typeof(string));
                dtparam.Columns.Add("claim_3", typeof(string));
                dtparam.Columns.Add("percentage_3", typeof(string));
                dtparam.Columns.Add("type4", typeof(string));
                dtparam.Columns.Add("cal_4", typeof(string));
                dtparam.Columns.Add("found_4", typeof(string));
                dtparam.Columns.Add("claim_4", typeof(string));
                dtparam.Columns.Add("percentage_4", typeof(string));
                dtparam.Columns.Add("type5", typeof(string));
                dtparam.Columns.Add("cal_5", typeof(string));
                dtparam.Columns.Add("found_5", typeof(string));
                dtparam.Columns.Add("claim_5", typeof(string));
                dtparam.Columns.Add("percentage_5", typeof(string));
                dtparam.Columns.Add("type6", typeof(string));
                dtparam.Columns.Add("cal_6", typeof(string));
                dtparam.Columns.Add("found_6", typeof(string));
                dtparam.Columns.Add("claim_6", typeof(string));
                dtparam.Columns.Add("percentage_6", typeof(string));
                dtparam.Columns.Add("type7", typeof(string));
                dtparam.Columns.Add("cal_7", typeof(string));
                dtparam.Columns.Add("found_7", typeof(string));
                dtparam.Columns.Add("claim_7", typeof(string));
                dtparam.Columns.Add("percentage_7", typeof(string));
                dtparam.Columns.Add("type8", typeof(string));
                dtparam.Columns.Add("cal_8", typeof(string));
                dtparam.Columns.Add("found_8", typeof(string));
                dtparam.Columns.Add("claim_8", typeof(string));
                dtparam.Columns.Add("percentage_8", typeof(string));
                dtparam.Columns.Add("type9", typeof(string));
                dtparam.Columns.Add("cal_9", typeof(string));
                dtparam.Columns.Add("found_9", typeof(string));
                dtparam.Columns.Add("claim_9", typeof(string));
                dtparam.Columns.Add("percentage_9", typeof(string));
                dtparam.Columns.Add("type10", typeof(string));
                dtparam.Columns.Add("cal_10", typeof(string));
                dtparam.Columns.Add("found_10", typeof(string));
                dtparam.Columns.Add("claim_10", typeof(string));
                dtparam.Columns.Add("percentage_10", typeof(string));
                dtparam.Columns.Add("type11", typeof(string));
                dtparam.Columns.Add("cal_11", typeof(string));
                dtparam.Columns.Add("found_11", typeof(string));
                dtparam.Columns.Add("claim_11", typeof(string));
                dtparam.Columns.Add("percentage_11", typeof(string));
                dtparam.Columns.Add("type12", typeof(string));
                dtparam.Columns.Add("cal_12", typeof(string));
                dtparam.Columns.Add("found_12", typeof(string));
                dtparam.Columns.Add("claim_12", typeof(string));
                dtparam.Columns.Add("percentage_12", typeof(string));
                // for (int i = 0; i <= s1table.Rows.Count; i++)
                int i = 0;
                // for (int i = 0; i <= 0; i++)
                //{

                dtparam.Rows.Add();

                dtparam.Rows[i]["type1"] = "1";
                dtparam.Rows[i]["cal_1"] = txtB3calculation1.Text.Trim();
                dtparam.Rows[i]["found_1"] = txtB3F1.Text.Trim();
                dtparam.Rows[i]["claim_1"] = txtB3Cl1.Text.Trim();
                dtparam.Rows[i]["percentage_1"] = txtB3P1.Text.Trim();
                dtparam.Rows[i]["type2"] = "2";
                dtparam.Rows[i]["cal_2"] = txtB3calculation2.Text.Trim();
                dtparam.Rows[i]["found_2"] = txtB3F2.Text.Trim();
                dtparam.Rows[i]["claim_2"] = txtB3Cl2.Text.Trim();
                dtparam.Rows[i]["percentage_2"] = txtB3P2.Text.Trim();

                dtparam.Rows[i]["type3"] = "3";
                dtparam.Rows[i]["cal_3"] = txtB3calculation3.Text.Trim();
                dtparam.Rows[i]["found_3"] = txtB3F3.Text.Trim();
                dtparam.Rows[i]["claim_3"] = txtB3Cl3.Text.Trim();
                dtparam.Rows[i]["percentage_3"] = txtB3P3.Text.Trim();
                dtparam.Rows[i]["type4"] = "4";
                dtparam.Rows[i]["cal_4"] = txtB3calculation4.Text.Trim();
                dtparam.Rows[i]["found_4"] = txtB3F4.Text.Trim();
                dtparam.Rows[i]["claim_4"] = txtB3Cl4.Text.Trim();
                dtparam.Rows[i]["percentage_4"] = txtB3P4.Text.Trim();
                dtparam.Rows[i]["type5"] = "5";
                dtparam.Rows[i]["cal_5"] = txtB3calculation5.Text.Trim();
                dtparam.Rows[i]["found_5"] = txtB3F5.Text.Trim();
                dtparam.Rows[i]["claim_5"] = txtB3Cl5.Text.Trim();
                dtparam.Rows[i]["percentage_5"] = txtB3P5.Text.Trim();
                dtparam.Rows[i]["type6"] = "6";
                dtparam.Rows[i]["cal_6"] = txtB3calculation6.Text.Trim();
                dtparam.Rows[i]["found_6"] = txtB3F6.Text.Trim();
                dtparam.Rows[i]["claim_6"] = txtB3Cl6.Text.Trim();
                dtparam.Rows[i]["percentage_6"] = txtB3P6.Text.Trim();

                dtparam.Rows[i]["type7"] = "7";
                dtparam.Rows[i]["cal_7"] = txtB3calculation7.Text.Trim();
                dtparam.Rows[i]["found_7"] = txtB3F7.Text.Trim();
                dtparam.Rows[i]["claim_7"] = txtB3Cl7.Text.Trim();
                dtparam.Rows[i]["percentage_7"] = txtB3P7.Text.Trim();

                dtparam.Rows[i]["type8"] = "8";
                dtparam.Rows[i]["cal_8"] = txtB3calculation8.Text.Trim();
                dtparam.Rows[i]["found_8"] = txtB3F8.Text.Trim();
                dtparam.Rows[i]["claim_8"] = txtB3Cl8.Text.Trim();
                dtparam.Rows[i]["percentage_8"] = txtB3P8.Text.Trim();

                dtparam.Rows[i]["type9"] = "9";
                dtparam.Rows[i]["cal_9"] = txtB3calculation9.Text.Trim();
                dtparam.Rows[i]["found_9"] = txtB3F9.Text.Trim();
                dtparam.Rows[i]["claim_9"] = txtB3Cl9.Text.Trim();
                dtparam.Rows[i]["percentage_9"] = txtB3P9.Text.Trim();
                dtparam.Rows[i]["type10"] = "10";

                dtparam.Rows[i]["cal_10"] = txtB3calculation10.Text.Trim();
                dtparam.Rows[i]["found_10"] = txtB3F10.Text.Trim();
                dtparam.Rows[i]["claim_10"] = txtB3Cl10.Text.Trim();
                dtparam.Rows[i]["percentage_10"] = txtB3P10.Text.Trim();

                dtparam.Rows[i]["type11"] = "11";
                dtparam.Rows[i]["cal_11"] = txtB3calculation11.Text.Trim();
                dtparam.Rows[i]["found_11"] = txtB3F11.Text.Trim();
                dtparam.Rows[i]["claim_11"] = txtB3Cl11.Text.Trim();
                dtparam.Rows[i]["percentage_11"] = txtB3P11.Text.Trim();

                dtparam.Rows[i]["type12"] = "12";
                dtparam.Rows[i]["cal_12"] = txtB3calculation12.Text.Trim();
                dtparam.Rows[i]["found_12"] = txtB3F12.Text.Trim();
                dtparam.Rows[i]["claim_12"] = txtB3Cl12.Text.Trim();
                dtparam.Rows[i]["percentage_12"] = txtB3P12.Text.Trim();

                objBE.Action = "DS";
                objBE.flag = "B3U";
                objBE.SampleID = lblb3sampleid.Text.Trim();
                objBE.AckTVP = null;
                objBE.TVP = null;

                objBE.Ds3TVP = dtparam;
                objBE.Remarks = txtB3remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("B3 Dissolution results Updated ");
                    //   clear();

                }
                EditB3.Visible = false;
                BindGrid();

            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }


    }

    public bool S1validate()
    {

        if (txtS1calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtS1calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtS1calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtS1calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtS1calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtS1calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtS1calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtS1calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtS1calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtS1calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtS1calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtS1calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts1P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txts1Remrks.Text != "")
        {
            bool val;
            val = CheckInput_new(txts1Remrks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool S2validate()
    {

        if (txts2calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts2P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txts2remarks.Text != "")
        {
            bool val;
            val = CheckInput_new(txts2remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool S3validate()
    {

        if (txts3calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txts3Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txts3F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }



        if (txts3calculation7.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F7.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl7.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P7.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation8.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F8.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl8.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P8.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation9.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F9.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl9.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P9.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation10.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F10.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl10.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P10.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txts3calculation11.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F11.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl11.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P11.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3calculation12.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3calculation12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3F12.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3F12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3Cl12.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3Cl12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3P12.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3P12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txts3remarks.Text != "")
        {
            bool val;
            val = CheckInput_new(txts3remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        return true;
    }
    public bool A1validate()
    {

        if (txtA1calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA1P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtA1Remrks.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA1Remrks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool A2validate()
    {

        if (txtA2calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA2P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtA2remarks.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA2remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool A3validate()
    {

        if (txtA3calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtA3Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtA3F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtA3calculation7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3calculation11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3F11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3Cl11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3P11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtA3calculation12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3calculation12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtA3F12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3F12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtA3Cl12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3Cl12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtA3P12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3P12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtA3remarks.Text != "")
        {
            bool val;
            val = CheckInput_new(txtA3remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        return true;
    }
    public bool B1validate()
    {

        if (txtB1calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB1P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtB1Remrks.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB1Remrks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool B2validate()
    {

        if (txtB2calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtB2remarks.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        return true;
    }
    public bool B3validate()
    {

        if (txtB3calculation1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P1.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P1.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB2F2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB2F2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P2.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P2.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtB3Cl3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P3.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P3.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtB3F4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P4.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P4.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P5.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P5.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P6.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P6.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtB3calculation7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P7.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P7.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P8.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P8.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P9.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P9.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P10.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P10.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3calculation11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3F11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3Cl11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3P11.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P11.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }


        if (txtB3calculation12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3calculation12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtB3F12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3F12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtB3Cl12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3Cl12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtB3P12.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3P12.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }

        if (txtB3remarks.Text != "")
        {
            bool val;
            val = CheckInput_new(txtB3remarks.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
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
    public bool CheckInput_new(string parameter)
    {
        bool FoundSusp = false; //
        string[] blackList = { "--", ";--", ";", "/*", "*/", "@@","<",">","%","?","uff1c","uff1e","onerror" ,"img" ,
                                 "nchar", "varchar", "nvarchar", "alter", "begin", "cast", "create",
                                 "cursor", "declare", "d elete", "drop", "exec", "execute", "fetch",
                                 "insert", "kill", "open", "select", "sys", "sysobjects", "syscolumns", "truncate",
                                 "update", "<script", "<script>", ".js",".exe", ".sql","xp_","DBCC","prompt","src"};
        for (int i = 0; i <= blackList.Length - 1; i++)
        {
            if (parameter.IndexOf(blackList[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                FoundSusp = true;
                break;
            }
        }
        return FoundSusp;
    }
}