using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;

public partial class DCA_UnitOfficer_RA_ViewTestResult : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    Masters ObjDAL = new Masters();
    Master_BE Obj = new Master_BE();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "4")
        {
            con = Session["ConnKey"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["UnitOfficerName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                random();
                BindGrid();
                BindProtocol();
                DivViewData.Visible = false;
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
            dt = new DataTable();
            objBE.Action = "RAUOVIEM";  
            objBE.labID = Session["Labcode"].ToString();
            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GVTestResult.DataSource = dt;
                GVTestResult.DataBind();
            }
            else
            {
                GVTestResult.DataSource = null;
                GVTestResult.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindProtocol()
    {
        dt = new DataTable();
        Obj.Action = "PROTOCOL";
        dt = ObjDAL.Getdetails(Obj, con);
        cf.BindDropDownLists(ddlprotocol, dt, "ProtocolName", "Protocolid", "Select");
    }
    protected void GVTestResult_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewResult")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            lblsamplid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
            lbltestresult.Text = ((Label)(gvrow.FindControl("lbltestresult"))).Text;
            lbls1testresult.Text = ((Label)(gvrow.FindControl("lbls1testdone"))).Text;
            lbls2testresult.Text = ((Label)(gvrow.FindControl("lbls2testdone"))).Text;
            lbls3testresult.Text = ((Label)(gvrow.FindControl("lbls3testdone"))).Text;
            lblA1testresult.Text = ((Label)(gvrow.FindControl("lbA1testdone"))).Text;
            lblA2testresult.Text = ((Label)(gvrow.FindControl("lblA2testdone"))).Text;
            lblA3testresult.Text = ((Label)(gvrow.FindControl("lblA3testdone"))).Text;
            lblB1testresult.Text = ((Label)(gvrow.FindControl("lblB1testdone"))).Text;
            lblB2testresult.Text = ((Label)(gvrow.FindControl("lblB2testdone"))).Text;
            lblB3testresult.Text = ((Label)(gvrow.FindControl("lblB3testdone"))).Text;
            lblRole.Text = ((Label)(gvrow.FindControl("lblRole"))).Text;
            //lblbstestresult.Text = ((Label)(gvrow.FindControl("lblbs"))).Text;
            // lblstatus.Text = ((Label)(gvrow.FindControl("lblstatus"))).Text;
            ViewData();
            if (lbls1testresult.Text == "S1")
            {
                ViewS1();
            }
            else
                divs1.Visible = false;

            if (lbls2testresult.Text == "S2")
                ViewS2();
            else
                divs2.Visible = false;

            if (lbls3testresult.Text == "S3")
                ViewS3();
            else
                divs3.Visible = false;

            if (lblA1testresult.Text == "A1")
                ViewA1();
            else
                diva1.Visible = false;

            if (lblA2testresult.Text == "A2")
                ViewA2();
            else
                divA2.Visible = false;
            if (lblA3testresult.Text == "A3")
                ViewA3();
            else
                divA3.Visible = false;

            if (lblB1testresult.Text == "B1")
                ViewB1();
            else
                divB1.Visible = false;
            if (lblB2testresult.Text == "B2")
                ViewB2();
            else
                divB2.Visible = false;
            if (lblB3testresult.Text == "B3")
                ViewB3();
            else
                divB3.Visible = false;
        }
    }

    protected void ViewData()
    {
        objBE.Action = "RATR";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.user = lblRole.Text;
        DataTable dtt = new DataTable();
        dtt = ObjDL.JAActionEdit(objBE, con);
        if (dtt.Rows.Count > 0)
        {
            DivViewData.Visible = true;
            lbllab.Text = dtt.Rows[0]["LabName"].ToString();
            lbljaName.Text = dtt.Rows[0]["JAName"].ToString();
            lblCat.Text = dtt.Rows[0]["Category_Name"].ToString();
            lbldrugnm.Text = dtt.Rows[0]["TradeName"].ToString();
            lblmfgdt.Text = dtt.Rows[0]["ManufacturingDate"].ToString();
            lblexpdt.Text = dtt.Rows[0]["ExpiryDate"].ToString();
            lblqty.Text = dtt.Rows[0]["SampleQty"].ToString();
            lblComposition.Text = dtt.Rows[0]["Composition"].ToString();
            txtStartDt.Text = dtt.Rows[0]["Test_start_Dt"].ToString();
            txtEndDt.Text = dtt.Rows[0]["Test_End_Dt"].ToString();
            txtDesc.Text = dtt.Rows[0]["Description"].ToString();
            txtremarks.Text = dtt.Rows[0]["JARemarks"].ToString();
            GvResult.DataSource = dtt;
            GvResult.DataBind();
        }
    }
    protected void ViewS1()
    {
        divs1.Visible = true;
        objBE.Action = "RS1";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lbls1testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
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
    protected void ViewS2()
    {
        divs2.Visible = true;
        objBE.Action = "RS1";
        objBE.description = lbls2testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {

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
    protected void ViewS3()
    {
        divs3.Visible = true;
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lbls3testresult.Text;
        objBE.Action = "RS1";
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
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
    protected void ViewA1()
    {
        diva1.Visible = true;
        objBE.Action = "RS1";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lblA1testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            lblA1calculation1.Text = dt.Rows[0]["cal_1"].ToString();
            lblA1F1.Text = dt.Rows[0]["found_1"].ToString();
            lblA1Cl1.Text = dt.Rows[0]["claim_1"].ToString();
            lblA1P1.Text = dt.Rows[0]["percentage_1"].ToString();
            lblA1calculation2.Text = dt.Rows[0]["cal_2"].ToString();
            lblA1F2.Text = dt.Rows[0]["found_2"].ToString();
            lblA1Cl2.Text = dt.Rows[0]["claim_2"].ToString();
            lblA1P2.Text = dt.Rows[0]["per_2"].ToString();
            lblA1calculation3.Text = dt.Rows[0]["cal_3"].ToString();
            lblA1F3.Text = dt.Rows[0]["found_3"].ToString();
            lblA1Cl3.Text = dt.Rows[0]["claim_3"].ToString();
            lblA1P3.Text = dt.Rows[0]["per_3"].ToString();
            lblA1calculation4.Text = dt.Rows[0]["cal_4"].ToString();
            lblA1F4.Text = dt.Rows[0]["found_4"].ToString();
            lblA1Cl4.Text = dt.Rows[0]["claim_4"].ToString();
            lblA1P4.Text = dt.Rows[0]["per_4"].ToString();
            lblA1calculation5.Text = dt.Rows[0]["cal_5"].ToString();
            lblA1F5.Text = dt.Rows[0]["found_5"].ToString();
            lblA1Cl5.Text = dt.Rows[0]["claim_5"].ToString();
            lblA1P5.Text = dt.Rows[0]["per_5"].ToString();
            lblA1calculation6.Text = dt.Rows[0]["cal_6"].ToString();
            lblA1F6.Text = dt.Rows[0]["found_6"].ToString();
            lblA1Cl6.Text = dt.Rows[0]["claim_6"].ToString();
            lblA1P6.Text = dt.Rows[0]["per_6"].ToString();
            lblA1Remarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }
    protected void ViewA2()
    {
        divA2.Visible = true;
        objBE.Action = "RS1";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lblA2testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            lblA2calculation1.Text = dt.Rows[0]["cal_1"].ToString();
            lblA2F1.Text = dt.Rows[0]["found_1"].ToString();
            lblA2Cl1.Text = dt.Rows[0]["claim_1"].ToString();
            lblA2P1.Text = dt.Rows[0]["percentage_1"].ToString();
            lblA2calculation2.Text = dt.Rows[0]["cal_2"].ToString();
            lblA2F2.Text = dt.Rows[0]["found_2"].ToString();
            lblA2Cl2.Text = dt.Rows[0]["claim_2"].ToString();
            lblA2P2.Text = dt.Rows[0]["per_2"].ToString();
            lblA2calculation3.Text = dt.Rows[0]["cal_3"].ToString();
            lblA2F3.Text = dt.Rows[0]["found_3"].ToString();
            lblA2Cl3.Text = dt.Rows[0]["claim_3"].ToString();
            lblA2P3.Text = dt.Rows[0]["per_3"].ToString();
            lblA2calculation4.Text = dt.Rows[0]["cal_4"].ToString();
            lblA2F4.Text = dt.Rows[0]["found_4"].ToString();
            lblA2Cl4.Text = dt.Rows[0]["claim_4"].ToString();
            lblA2P4.Text = dt.Rows[0]["per_4"].ToString();
            lblA2calculation5.Text = dt.Rows[0]["cal_5"].ToString();
            lblA2F5.Text = dt.Rows[0]["found_5"].ToString();
            lblA2Cl5.Text = dt.Rows[0]["claim_5"].ToString();
            lblA2P5.Text = dt.Rows[0]["per_5"].ToString();
            lblA2calculation6.Text = dt.Rows[0]["cal_6"].ToString();
            lblA2F6.Text = dt.Rows[0]["found_6"].ToString();
            lblA2Cl6.Text = dt.Rows[0]["claim_6"].ToString();
            lblA2P6.Text = dt.Rows[0]["per_6"].ToString();
            lblA2Remarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }
    protected void ViewA3()
    {
        divA3.Visible = true;
        objBE.Action = "RS1";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lblA3testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            lblA3calculation1.Text = dt.Rows[0]["cal_1"].ToString();
            lblA3F1.Text = dt.Rows[0]["found_1"].ToString();
            lblA3Cl1.Text = dt.Rows[0]["claim_1"].ToString();
            lblA3P1.Text = dt.Rows[0]["percentage_1"].ToString();
            lblA3calculation2.Text = dt.Rows[0]["cal_2"].ToString();
            lblA3F2.Text = dt.Rows[0]["found_2"].ToString();
            lblA3Cl2.Text = dt.Rows[0]["claim_2"].ToString();
            lblA3P2.Text = dt.Rows[0]["per_2"].ToString();
            lblA3calculation3.Text = dt.Rows[0]["cal_3"].ToString();
            lblA3F3.Text = dt.Rows[0]["found_3"].ToString();
            lblA3Cl3.Text = dt.Rows[0]["claim_3"].ToString();
            lblA3P3.Text = dt.Rows[0]["per_3"].ToString();
            lblA3calculation4.Text = dt.Rows[0]["cal_4"].ToString();
            lblA3F4.Text = dt.Rows[0]["found_4"].ToString();
            lblA3Cl4.Text = dt.Rows[0]["claim_4"].ToString();
            lblA3P4.Text = dt.Rows[0]["per_4"].ToString();
            lblA3calculation5.Text = dt.Rows[0]["cal_5"].ToString();
            lblA3F5.Text = dt.Rows[0]["found_5"].ToString();
            lblA3Cl5.Text = dt.Rows[0]["claim_5"].ToString();
            lblA3P5.Text = dt.Rows[0]["per_5"].ToString();
            lblA3calculation6.Text = dt.Rows[0]["cal_6"].ToString();
            lblA3F6.Text = dt.Rows[0]["found_6"].ToString();
            lblA3Cl6.Text = dt.Rows[0]["claim_6"].ToString();
            lblA3P6.Text = dt.Rows[0]["per_6"].ToString();

            lblA3calculation7.Text = dt.Rows[0]["cal_7"].ToString();
            lblA3F7.Text = dt.Rows[0]["found_7"].ToString();
            lblA3Cl7.Text = dt.Rows[0]["claim_7"].ToString();
            lblA3P7.Text = dt.Rows[0]["per_7"].ToString();
            lblA3calculation8.Text = dt.Rows[0]["cal_8"].ToString();
            lblA3F8.Text = dt.Rows[0]["found_8"].ToString();
            lblA3Cl8.Text = dt.Rows[0]["claim_8"].ToString();
            lblA3P8.Text = dt.Rows[0]["per_8"].ToString();
            lblA3calculation9.Text = dt.Rows[0]["cal_9"].ToString();
            lblA3F9.Text = dt.Rows[0]["found_9"].ToString();
            lblA3Cl9.Text = dt.Rows[0]["claim_9"].ToString();
            lblA3P9.Text = dt.Rows[0]["per_9"].ToString();
            lblA3calculation10.Text = dt.Rows[0]["cal_10"].ToString();
            lblA3F10.Text = dt.Rows[0]["found_10"].ToString();
            lblA3Cl10.Text = dt.Rows[0]["claim_10"].ToString();
            lblA3P10.Text = dt.Rows[0]["per_10"].ToString();
            lblA3calculation11.Text = dt.Rows[0]["cal_11"].ToString();
            lblA3F11.Text = dt.Rows[0]["found_11"].ToString();
            lblA3Cl11.Text = dt.Rows[0]["claim_11"].ToString();
            lblA3P11.Text = dt.Rows[0]["per_11"].ToString();
            lblA3calculation12.Text = dt.Rows[0]["cal_12"].ToString();
            lblA3F12.Text = dt.Rows[0]["found_12"].ToString();
            lblA3Cl12.Text = dt.Rows[0]["claim_12"].ToString();
            lblA3P12.Text = dt.Rows[0]["per_12"].ToString();
            lblA3Remarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }
    protected void ViewB1()
    {
        divB1.Visible = true;
        objBE.Action = "RS1";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lblB1testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            lblB1calculation1.Text = dt.Rows[0]["cal_1"].ToString();
            lblB1F1.Text = dt.Rows[0]["found_1"].ToString();
            lblB1Cl1.Text = dt.Rows[0]["claim_1"].ToString();
            lblB1P1.Text = dt.Rows[0]["percentage_1"].ToString();
            lblB1calculation2.Text = dt.Rows[0]["cal_2"].ToString();
            lblB1F2.Text = dt.Rows[0]["found_2"].ToString();
            lblB1Cl2.Text = dt.Rows[0]["claim_2"].ToString();
            lblB1P2.Text = dt.Rows[0]["per_2"].ToString();
            lblB1calculation3.Text = dt.Rows[0]["cal_3"].ToString();
            lblB1F3.Text = dt.Rows[0]["found_3"].ToString();
            lblB1Cl3.Text = dt.Rows[0]["claim_3"].ToString();
            lblB1P3.Text = dt.Rows[0]["per_3"].ToString();
            lblB1calculation4.Text = dt.Rows[0]["cal_4"].ToString();
            lblB1F4.Text = dt.Rows[0]["found_4"].ToString();
            lblB1Cl4.Text = dt.Rows[0]["claim_4"].ToString();
            lblB1P4.Text = dt.Rows[0]["per_4"].ToString();
            lblB1calculation5.Text = dt.Rows[0]["cal_5"].ToString();
            lblB1F5.Text = dt.Rows[0]["found_5"].ToString();
            lblB1Cl5.Text = dt.Rows[0]["claim_5"].ToString();
            lblB1P5.Text = dt.Rows[0]["per_5"].ToString();
            lblB1calculation6.Text = dt.Rows[0]["cal_6"].ToString();
            lblB1F6.Text = dt.Rows[0]["found_6"].ToString();
            lblB1Cl6.Text = dt.Rows[0]["claim_6"].ToString();
            lblB1P6.Text = dt.Rows[0]["per_6"].ToString();
            lblB1remarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }
    protected void ViewB2()
    {
        divB2.Visible = true;
        objBE.Action = "RS1";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lblB2testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            lblB2calculation1.Text = dt.Rows[0]["cal_1"].ToString();
            lblB2F1.Text = dt.Rows[0]["found_1"].ToString();
            lblB2Cl1.Text = dt.Rows[0]["claim_1"].ToString();
            lblB2P1.Text = dt.Rows[0]["percentage_1"].ToString();
            lblB2calculation2.Text = dt.Rows[0]["cal_2"].ToString();
            lblB2F2.Text = dt.Rows[0]["found_2"].ToString();
            lblB2Cl2.Text = dt.Rows[0]["claim_2"].ToString();
            lblB2P2.Text = dt.Rows[0]["per_2"].ToString();
            lblB2calculation3.Text = dt.Rows[0]["cal_3"].ToString();
            lblB2F3.Text = dt.Rows[0]["found_3"].ToString();
            lblB2Cl3.Text = dt.Rows[0]["claim_3"].ToString();
            lblB2P3.Text = dt.Rows[0]["per_3"].ToString();
            lblB2calculation4.Text = dt.Rows[0]["cal_4"].ToString();
            lblB2F4.Text = dt.Rows[0]["found_4"].ToString();
            lblB2Cl4.Text = dt.Rows[0]["claim_4"].ToString();
            lblB2P4.Text = dt.Rows[0]["per_4"].ToString();
            lblB2calculation5.Text = dt.Rows[0]["cal_5"].ToString();
            lblB2F5.Text = dt.Rows[0]["found_5"].ToString();
            lblB2Cl5.Text = dt.Rows[0]["claim_5"].ToString();
            lblB2P5.Text = dt.Rows[0]["per_5"].ToString();
            lblB2calculation6.Text = dt.Rows[0]["cal_6"].ToString();
            lblB2F6.Text = dt.Rows[0]["found_6"].ToString();
            lblB2Cl6.Text = dt.Rows[0]["claim_6"].ToString();
            lblB2P6.Text = dt.Rows[0]["per_6"].ToString();
            lblB2Remarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }
    protected void ViewB3()
    {
        divB3.Visible = true;
        objBE.Action = "RS1";
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.description = lblB3testresult.Text;
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
            lblB3calculation1.Text = dt.Rows[0]["cal_1"].ToString();
            lblB3F1.Text = dt.Rows[0]["found_1"].ToString();
            lblB3Cl1.Text = dt.Rows[0]["claim_1"].ToString();
            lblB3P1.Text = dt.Rows[0]["percentage_1"].ToString();
            lblB3calculation2.Text = dt.Rows[0]["cal_2"].ToString();
            lblB3F2.Text = dt.Rows[0]["found_2"].ToString();
            lblB3Cl2.Text = dt.Rows[0]["claim_2"].ToString();
            lblB3P2.Text = dt.Rows[0]["per_2"].ToString();
            lblB3calculation3.Text = dt.Rows[0]["cal_3"].ToString();
            lblB3F3.Text = dt.Rows[0]["found_3"].ToString();
            lblB3Cl3.Text = dt.Rows[0]["claim_3"].ToString();
            lblB3P3.Text = dt.Rows[0]["per_3"].ToString();
            lblB3calculation4.Text = dt.Rows[0]["cal_4"].ToString();
            lblB3F4.Text = dt.Rows[0]["found_4"].ToString();
            lblB3Cl4.Text = dt.Rows[0]["claim_4"].ToString();
            lblB3P4.Text = dt.Rows[0]["per_4"].ToString();
            lblB3calculation5.Text = dt.Rows[0]["cal_5"].ToString();
            lblB3F5.Text = dt.Rows[0]["found_5"].ToString();
            lblB3Cl5.Text = dt.Rows[0]["claim_5"].ToString();
            lblB3P5.Text = dt.Rows[0]["per_5"].ToString();
            lblB3calculation6.Text = dt.Rows[0]["cal_6"].ToString();
            lblB3F6.Text = dt.Rows[0]["found_6"].ToString();
            lblB3Cl6.Text = dt.Rows[0]["claim_6"].ToString();
            lblB3P6.Text = dt.Rows[0]["per_6"].ToString();

            lblB3calculation7.Text = dt.Rows[0]["cal_7"].ToString();
            lblB3F7.Text = dt.Rows[0]["found_7"].ToString();
            lblB3Cl7.Text = dt.Rows[0]["claim_7"].ToString();
            lblB3P7.Text = dt.Rows[0]["per_7"].ToString();
            lblB3calculation8.Text = dt.Rows[0]["cal_8"].ToString();
            lblB3F8.Text = dt.Rows[0]["found_8"].ToString();
            lblB3Cl8.Text = dt.Rows[0]["claim_8"].ToString();
            lblB3P8.Text = dt.Rows[0]["per_8"].ToString();
            lblB3calculation9.Text = dt.Rows[0]["cal_9"].ToString();
            lblB3F9.Text = dt.Rows[0]["found_9"].ToString();
            lblB3Cl9.Text = dt.Rows[0]["claim_9"].ToString();
            lblB3P9.Text = dt.Rows[0]["per_9"].ToString();
            lblB3calculation10.Text = dt.Rows[0]["cal_10"].ToString();
            lblB3F10.Text = dt.Rows[0]["found_10"].ToString();
            lblB3Cl10.Text = dt.Rows[0]["claim_10"].ToString();
            lblB3P10.Text = dt.Rows[0]["per_10"].ToString();
            lblB3calculation11.Text = dt.Rows[0]["cal_11"].ToString();
            lblB3F11.Text = dt.Rows[0]["found_11"].ToString();
            lblB3Cl11.Text = dt.Rows[0]["claim_11"].ToString();
            lblB3P11.Text = dt.Rows[0]["per_11"].ToString();
            lblB3calculation12.Text = dt.Rows[0]["cal_12"].ToString();
            lblB3F12.Text = dt.Rows[0]["found_12"].ToString();
            lblB3Cl12.Text = dt.Rows[0]["claim_12"].ToString();
            lblB3P12.Text = dt.Rows[0]["per_12"].ToString();
            lblB3Remarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (validate())
        {
            dt = new DataTable();
            objBE.UOStatus = rblstatus.SelectedValue;
            objBE.UOId = Session["UnitOfficerCode"].ToString();
            objBE.UORemarks = txtremarks.Text.Trim();
            objBE.Ref = ddlprotocol.SelectedValue;
            objBE.Action = "UO";
            objBE.flag = "0";
            objBE.SampleID = lblsamplid.Text;

            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("TestID", typeof(string));
            dtparam.Columns.Add("testRemarks", typeof(string));

            int i = 0;
            foreach (GridViewRow gr in GvResult.Rows)
            {
                TextBox testremarks = (TextBox)gr.FindControl("txtremarks");
                if (ValidateGrid(testremarks))
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[i]["TestID"] = ((Label)gr.FindControl("lbltestids")).Text.Trim();
                    dtparam.Rows[i]["testRemarks"] = ((TextBox)gr.FindControl("txtremarks")).Text.Trim();
                }
                else
                {
                    return;
                }
                i++;
            }
            objBE.UOTVP = dtparam;

            if (rblstatus.SelectedValue == "RA")
            {
                objBE.AnalystId = ddlAnalyst.SelectedValue;
            }
            objBE.TVP = null;

            dt = ObjDL.JAActionEdit(objBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("not Saved , try again");
            }
            else
            {
                cf.ShowAlertMessage("Saved");

            }
            rblstatus.SelectedIndex = 0;
            txtremarks.Text = "";
            BindGrid();
            DivViewData.Visible = false;
        }

    }
    protected bool ValidateGrid(TextBox rmrks)
    {

        if (rmrks.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Unit Officer Remarks");
            rmrks.Focus();
            return false;
        }

        return true;
    }


    protected void BindAnalyst()
    {
        dt = new DataTable();
        objBE.Action = "ANALYST";
        objBE.labID = Session["Labcode"].ToString();
        dt = ObjDL.UOAction(objBE, con);
        if (dt.Rows.Count > 0)
            cf.BindDropDownLists(ddlAnalyst, dt, "Name", "User_Code", "Select");
        else
            cf.ShowAlertMessage("No Analysts Added");
    }
    protected bool validate()
    {
        if (rblstatus.SelectedIndex == -1)
        {
            cf.ShowAlertMessage("Please select Sample is of");
            rblstatus.Focus();
            return false;
        }
        if (rblstatus.SelectedValue == "S" || rblstatus.SelectedValue == "NS")
        {
            if (ddlprotocol.SelectedIndex == 0)
            {
                cf.ShowAlertMessage("Please select Ref As Per");
                ddlprotocol.Focus();
                return false;
            }
            if (txtcoremarks.Text == "")
            {
                cf.ShowAlertMessage("Please Enter Govt Analyst Remarks");
                txtcoremarks.Focus();
                return false;
            }
        }
        if (rblstatus.SelectedValue == "RA")
        {
            if (txtcoremarks.Text == "")
            {
                cf.ShowAlertMessage("Please Enter Govt Analyst Remarks");
                txtcoremarks.Focus();
                return false;
            }
            if (ddlAnalyst.SelectedIndex == 0)
            {
                cf.ShowAlertMessage("Please Select Allot to Analyst");
                ddlAnalyst.Focus();
                return false;
            }
        }
        return true;
    }
    protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblstatus.SelectedValue == "RA")
        {
            lblgvanalist.Visible = true;
            ddlAnalyst.Visible = true;
            ddlprotocol.Enabled = false;
            ddlprotocol.ClearSelection();
            BindAnalyst();
        }
        else
        {
            lblgvanalist.Visible = false;
            ddlAnalyst.Visible = false;
            ddlprotocol.Enabled = true;


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