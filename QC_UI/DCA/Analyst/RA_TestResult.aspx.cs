using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using QC_DL;
using System.Data;
using QC_BE;
using System.Text;
using System.Collections.Specialized;
public partial class Analyst_RA_TestResult : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    Masters ObjDAL = new Masters();
    Master_BE Obj = new Master_BE();
    DataTable dt;
    SampleSqlInjectionScreeningModule obj= new SampleSqlInjectionScreeningModule();

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
        BtnS2.Enabled = true;
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
                pnltestresult.Visible = false;
            txtsample.Focus();
        }
        else
            Response.Redirect("~/Error.aspx");
    }

    protected void txtsample_TextChanged(object sender, EventArgs e)
    {
        try
        {
            objBE.SampleID = txtsample.Text.Trim();
            if (Session["RoleID"].ToString() == "5")
                objBE.AnalystId = Analystcode;
            if (Session["RoleID"].ToString() == "6")
                objBE.AnalystId = Uocode;

            objBE.Action = "RTVIEW";
            dt = new DataTable();
            dt = ObjDL.JAAction(objBE, con);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "1")
                    cf.ShowAlertMessage("Not Yet Acknowledged");
                else
                {
                    txtsample.Enabled = false;
                    lbllab.Text = Session["Labname"].ToString();
                    lbldrugnm.Text = dt.Rows[0]["TradeName"].ToString();
                    lblcategory.Text = dt.Rows[0]["Category_Name"].ToString();
                    lblmfgdt.Text = dt.Rows[0]["Mdt"].ToString();
                    lblexpdt.Text = dt.Rows[0]["Edt"].ToString();
                    lblqty.Text = dt.Rows[0]["SampleQty"].ToString();
                    lblComposition.Text = dt.Rows[0]["Composition"].ToString();
                    txtStartDt.Text = dt.Rows[0]["Test_start_dt"].ToString();
                    txtEndDt.Text = dt.Rows[0]["Test_End_dt"].ToString();
                    txtDesc.Text = dt.Rows[0]["Description"].ToString();
                    txtremarks.Text = dt.Rows[0]["JARemarks"].ToString();
                    txtgvtanalistremarks.Text = dt.Rows[0]["UORemarks"].ToString();

                    txtS1calculation1.Text = dt.Rows[0]["S1Cal1"].ToString();
                    txts1F1.Text = dt.Rows[0]["S1found1"].ToString();
                    txts1Cl1.Text = dt.Rows[0]["S1claim1"].ToString();
                    txts1P1.Text = dt.Rows[0]["S1Per1"].ToString();
                    txtS1calculation2.Text = dt.Rows[0]["S1Cal2"].ToString();
                    txts1F2.Text = dt.Rows[0]["S1found2"].ToString();
                    txts1Cl2.Text = dt.Rows[0]["S1claim2"].ToString();
                    txts1P2.Text = dt.Rows[0]["S1Per2"].ToString();
                    txtS1calculation3.Text = dt.Rows[0]["S1Cal3"].ToString();
                    txts1F3.Text = dt.Rows[0]["S1found3"].ToString();
                    txts1Cl3.Text = dt.Rows[0]["S1claim3"].ToString();
                    txts1P3.Text = dt.Rows[0]["S1Per3"].ToString();
                    txtS1calculation4.Text = dt.Rows[0]["S1Cal4"].ToString();
                    txts1F4.Text = dt.Rows[0]["S1found4"].ToString();
                    txts1Cl4.Text = dt.Rows[0]["S1claim4"].ToString();
                    txts1P4.Text = dt.Rows[0]["S1Per4"].ToString();
                    txtS1calculation5.Text = dt.Rows[0]["S1Cal5"].ToString();
                    txts1F5.Text = dt.Rows[0]["S1found5"].ToString();
                    txts1Cl5.Text = dt.Rows[0]["S1claim5"].ToString();
                    txts1P5.Text = dt.Rows[0]["S1Per5"].ToString();
                    txtS1calculation6.Text = dt.Rows[0]["S1Cal6"].ToString();
                    txts1F6.Text = dt.Rows[0]["S1found6"].ToString();
                    txts1Cl6.Text = dt.Rows[0]["S1claim6"].ToString();
                    txts1P6.Text = dt.Rows[0]["S1Per6"].ToString();
                   txts1Remrks.Text=dt.Rows[0]["S1Remrks"].ToString(); 

                    txts2calculation1.Text = dt.Rows[0]["S2Cal1"].ToString();
                    txts2F1.Text = dt.Rows[0]["S2found1"].ToString();
                    txts2Cl1.Text = dt.Rows[0]["S2claim1"].ToString();
                    txts2P1.Text = dt.Rows[0]["S2Per1"].ToString();
                    txts2calculation2.Text = dt.Rows[0]["S2Cal2"].ToString();
                    txts2F2.Text = dt.Rows[0]["S2found2"].ToString();
                    txts2Cl2.Text = dt.Rows[0]["S2claim2"].ToString();
                    txts2P2.Text = dt.Rows[0]["S2Per2"].ToString();
                    txts2calculation3.Text = dt.Rows[0]["S2Cal3"].ToString();
                    txts2F3.Text = dt.Rows[0]["S2found3"].ToString();
                    txts2Cl3.Text = dt.Rows[0]["S2claim3"].ToString();
                    txts2P3.Text = dt.Rows[0]["S2Per3"].ToString();
                    txts2calculation4.Text = dt.Rows[0]["S2Cal4"].ToString();
                    txts2F4.Text = dt.Rows[0]["S2found4"].ToString();
                    txts2Cl4.Text = dt.Rows[0]["S2claim4"].ToString();
                    txts2P4.Text = dt.Rows[0]["S2Per4"].ToString();
                    txts2calculation5.Text = dt.Rows[0]["S2Cal5"].ToString();
                    txts2F5.Text = dt.Rows[0]["S2found5"].ToString();
                    txts2Cl5.Text = dt.Rows[0]["S2claim5"].ToString();
                    txts2P5.Text = dt.Rows[0]["S2Per5"].ToString();
                    txts2calculation6.Text = dt.Rows[0]["S2Cal6"].ToString();
                    txts2F6.Text = dt.Rows[0]["S2found6"].ToString();
                    txts2Cl6.Text = dt.Rows[0]["S2claim6"].ToString();
                    txts2P6.Text = dt.Rows[0]["S2Per6"].ToString();
                    txts2remarks.Text = dt.Rows[0]["S2Remrks"].ToString();

                    txts3calculation1.Text = dt.Rows[0]["S3Cal1"].ToString();
                    txts3F1.Text = dt.Rows[0]["S3found1"].ToString();
                    txts3Cl1.Text = dt.Rows[0]["S3claim1"].ToString();
                    txts3P1.Text = dt.Rows[0]["S3Per1"].ToString();
                    txts3calculation2.Text = dt.Rows[0]["S3Cal2"].ToString();
                    txts3F2.Text = dt.Rows[0]["S3found2"].ToString();
                    txts3Cl2.Text = dt.Rows[0]["S3claim2"].ToString();
                    txts3P2.Text = dt.Rows[0]["S3Per2"].ToString();
                    txts3calculation3.Text = dt.Rows[0]["S3Cal3"].ToString();
                    txts3F3.Text = dt.Rows[0]["S3found3"].ToString();
                    txts3Cl3.Text = dt.Rows[0]["S3claim3"].ToString();
                    txts3P3.Text = dt.Rows[0]["S3Per3"].ToString();
                    txts3calculation4.Text = dt.Rows[0]["S3Cal4"].ToString();
                    txts3F4.Text = dt.Rows[0]["S3found4"].ToString();
                    txts3Cl4.Text = dt.Rows[0]["S3claim4"].ToString();
                    txts3P4.Text = dt.Rows[0]["S3Per4"].ToString();
                    txts3calculation5.Text = dt.Rows[0]["S3Cal5"].ToString();
                    txts3F5.Text = dt.Rows[0]["S3found5"].ToString();
                    txts3Cl5.Text = dt.Rows[0]["S3claim5"].ToString();
                    txts3P5.Text = dt.Rows[0]["S3Per5"].ToString();
                    txts3calculation6.Text = dt.Rows[0]["S3Cal6"].ToString();
                    txts3F6.Text = dt.Rows[0]["S3found6"].ToString();
                    txts3Cl6.Text = dt.Rows[0]["S3claim6"].ToString();
                    txts3P6.Text = dt.Rows[0]["S3Per6"].ToString();
                    txts3calculation7.Text = dt.Rows[0]["S3Cal7"].ToString();
                    txts3F7.Text = dt.Rows[0]["S3found7"].ToString();
                    txts3Cl7.Text = dt.Rows[0]["S3claim7"].ToString();
                    txts3P7.Text = dt.Rows[0]["S3Per7"].ToString();
                    txts3calculation8.Text = dt.Rows[0]["S3Cal8"].ToString();
                    txts3F8.Text = dt.Rows[0]["S3found8"].ToString();
                    txts3Cl8.Text = dt.Rows[0]["S3claim8"].ToString();
                    txts3P8.Text = dt.Rows[0]["S3Per8"].ToString();
                    txts3calculation9.Text = dt.Rows[0]["S3Cal9"].ToString();
                    txts3F9.Text = dt.Rows[0]["S3found9"].ToString();
                    txts3Cl9.Text = dt.Rows[0]["S3claim9"].ToString();
                    txts3P9.Text = dt.Rows[0]["S3Per9"].ToString();
                    txts3calculation10.Text = dt.Rows[0]["S3Cal10"].ToString();
                    txts3F10.Text = dt.Rows[0]["S3found10"].ToString();
                    txts3Cl10.Text = dt.Rows[0]["S3claim10"].ToString();
                    txts3P10.Text = dt.Rows[0]["S3Per10"].ToString();
                    txts3calculation11.Text = dt.Rows[0]["S3Cal11"].ToString();
                    txts3F11.Text = dt.Rows[0]["S3found11"].ToString();
                    txts3Cl11.Text = dt.Rows[0]["S3claim11"].ToString();
                    txts3P11.Text = dt.Rows[0]["S3Per11"].ToString();
                    txts3calculation12.Text = dt.Rows[0]["S3Cal12"].ToString();
                    txts3F12.Text = dt.Rows[0]["S3found12"].ToString();
                    txts3Cl12.Text = dt.Rows[0]["S3claim12"].ToString();
                    txts3P12.Text = dt.Rows[0]["S3Per12"].ToString();
                    txts3remarks.Text = dt.Rows[0]["S3Remrks"].ToString();

                    txtA1calculation1.Text = dt.Rows[0]["A1Cal1"].ToString();
                    txtA1F1.Text = dt.Rows[0]["A1found1"].ToString();
                    txtA1Cl1.Text = dt.Rows[0]["A1claim1"].ToString();
                    txtA1P1.Text = dt.Rows[0]["A1Per1"].ToString();
                    txtA1calculation2.Text = dt.Rows[0]["A1Cal2"].ToString();
                    txtA1F2.Text = dt.Rows[0]["A1found2"].ToString();
                    txtA1Cl2.Text = dt.Rows[0]["A1claim2"].ToString();
                    txtA1P2.Text = dt.Rows[0]["A1Per2"].ToString();
                    txtA1calculation3.Text = dt.Rows[0]["A1Cal3"].ToString();
                    txtA1F3.Text = dt.Rows[0]["A1found3"].ToString();
                    txtA1Cl3.Text = dt.Rows[0]["A1claim3"].ToString();
                    txtA1P3.Text = dt.Rows[0]["A1Per3"].ToString();
                    txtA1calculation4.Text = dt.Rows[0]["A1Cal4"].ToString();
                    txtA1F4.Text = dt.Rows[0]["A1found4"].ToString();
                    txtA1Cl4.Text = dt.Rows[0]["A1claim4"].ToString();
                    txtA1P4.Text = dt.Rows[0]["A1Per4"].ToString();
                    txtA1calculation5.Text = dt.Rows[0]["A1Cal5"].ToString();
                    txtA1F5.Text = dt.Rows[0]["A1found5"].ToString();
                    txtA1Cl5.Text = dt.Rows[0]["A1claim5"].ToString();
                    txtA1P5.Text = dt.Rows[0]["A1Per5"].ToString();
                    txtA1calculation6.Text = dt.Rows[0]["A1Cal6"].ToString();
                    txtA1F6.Text = dt.Rows[0]["A1found6"].ToString();
                    txtA1Cl6.Text = dt.Rows[0]["A1claim6"].ToString();
                    txtA1P6.Text = dt.Rows[0]["A1Per6"].ToString();
                    txtA1Remrks.Text = dt.Rows[0]["A1Remrks"].ToString();

                    txtA2calculation1.Text = dt.Rows[0]["A2Cal1"].ToString();
                    txtA2F1.Text = dt.Rows[0]["A2found1"].ToString();
                    txtA2Cl1.Text = dt.Rows[0]["A2claim1"].ToString();
                    txtA2P1.Text = dt.Rows[0]["A2Per1"].ToString();
                    txtA2calculation2.Text = dt.Rows[0]["A2Cal2"].ToString();
                    txtA2F2.Text = dt.Rows[0]["A2found2"].ToString();
                    txtA2Cl2.Text = dt.Rows[0]["A2claim2"].ToString();
                    txtA2P2.Text = dt.Rows[0]["A2Per2"].ToString();
                    txtA2calculation3.Text = dt.Rows[0]["A2Cal3"].ToString();
                    txtA2F3.Text = dt.Rows[0]["A2found3"].ToString();
                    txtA2Cl3.Text = dt.Rows[0]["A2claim3"].ToString();
                    txtA2P3.Text = dt.Rows[0]["A2Per3"].ToString();
                    txtA2calculation4.Text = dt.Rows[0]["A2Cal4"].ToString();
                    txtA2F4.Text = dt.Rows[0]["A2found4"].ToString();
                    txtA2Cl4.Text = dt.Rows[0]["A2claim4"].ToString();
                    txtA2P4.Text = dt.Rows[0]["A2Per4"].ToString();
                    txtA2calculation5.Text = dt.Rows[0]["A2Cal5"].ToString();
                    txtA2F5.Text = dt.Rows[0]["A2found5"].ToString();
                    txtA2Cl5.Text = dt.Rows[0]["A2claim5"].ToString();
                    txtA2P5.Text = dt.Rows[0]["A2Per5"].ToString();
                    txtA2calculation6.Text = dt.Rows[0]["A2Cal6"].ToString();
                    txtA2F6.Text = dt.Rows[0]["A2found6"].ToString();
                    txtA2Cl6.Text = dt.Rows[0]["A2claim6"].ToString();
                    txtA2P6.Text = dt.Rows[0]["A2Per6"].ToString();
                    txtA2remarks.Text = dt.Rows[0]["A2Remrks"].ToString();

                    txtA3calculation1.Text = dt.Rows[0]["A3Cal1"].ToString();
                    txtA3F1.Text = dt.Rows[0]["A3found1"].ToString();
                    txtA3Cl1.Text = dt.Rows[0]["A3claim1"].ToString();
                    txtA3P1.Text = dt.Rows[0]["A3Per1"].ToString();
                    txtA3calculation2.Text = dt.Rows[0]["A3Cal2"].ToString();
                    txtA3F2.Text = dt.Rows[0]["A3found2"].ToString();
                    txtA3Cl2.Text = dt.Rows[0]["A3claim2"].ToString();
                    txtA3P2.Text = dt.Rows[0]["A3Per2"].ToString();
                    txtA3calculation3.Text = dt.Rows[0]["A3Cal3"].ToString();
                    txtA3F3.Text = dt.Rows[0]["A3found3"].ToString();
                    txtA3Cl3.Text = dt.Rows[0]["A3claim3"].ToString();
                    txtA3P3.Text = dt.Rows[0]["A3Per3"].ToString();
                    txtA3calculation4.Text = dt.Rows[0]["A3Cal4"].ToString();
                    txtA3F4.Text = dt.Rows[0]["A3found4"].ToString();
                    txtA3Cl4.Text = dt.Rows[0]["A3claim4"].ToString();
                    txtA3P4.Text = dt.Rows[0]["A3Per4"].ToString();
                    txtA3calculation5.Text = dt.Rows[0]["A3Cal5"].ToString();
                    txtA3F5.Text = dt.Rows[0]["A3found5"].ToString();
                    txtA3Cl5.Text = dt.Rows[0]["A3claim5"].ToString();
                    txtA3P5.Text = dt.Rows[0]["A3Per5"].ToString();
                    txtA3calculation6.Text = dt.Rows[0]["A3Cal6"].ToString();
                    txtA3F6.Text = dt.Rows[0]["A3found6"].ToString();
                    txtA3Cl6.Text = dt.Rows[0]["A3claim6"].ToString();
                    txtA3P6.Text = dt.Rows[0]["A3Per6"].ToString();
                    txtA3calculation7.Text = dt.Rows[0]["A3Cal7"].ToString();
                    txtA3F7.Text = dt.Rows[0]["A3found7"].ToString();
                    txtA3Cl7.Text = dt.Rows[0]["A3claim7"].ToString();
                    txtA3P7.Text = dt.Rows[0]["A3Per7"].ToString();
                    txtA3calculation8.Text = dt.Rows[0]["A3Cal8"].ToString();
                    txtA3F8.Text = dt.Rows[0]["A3found8"].ToString();
                    txtA3Cl8.Text = dt.Rows[0]["A3claim8"].ToString();
                    txtA3P8.Text = dt.Rows[0]["A3Per8"].ToString();
                    txtA3calculation9.Text = dt.Rows[0]["A3Cal9"].ToString();
                    txtA3F9.Text = dt.Rows[0]["A3found9"].ToString();
                    txtA3Cl9.Text = dt.Rows[0]["A3claim9"].ToString();
                    txtA3P9.Text = dt.Rows[0]["A3Per9"].ToString();
                    txtA3calculation10.Text = dt.Rows[0]["A3Cal10"].ToString();
                    txtA3F10.Text = dt.Rows[0]["A3found10"].ToString();
                    txtA3Cl10.Text = dt.Rows[0]["A3claim10"].ToString();
                    txtA3P10.Text = dt.Rows[0]["A3Per10"].ToString();
                    txtA3calculation11.Text = dt.Rows[0]["A3Cal11"].ToString();
                    txtA3F11.Text = dt.Rows[0]["A3found11"].ToString();
                    txtA3Cl11.Text = dt.Rows[0]["A3claim11"].ToString();
                    txtA3P11.Text = dt.Rows[0]["A3Per11"].ToString();
                    txtA3calculation12.Text = dt.Rows[0]["A3Cal12"].ToString();
                    txtA3F12.Text = dt.Rows[0]["A3found12"].ToString();
                    txtA3Cl12.Text = dt.Rows[0]["A3claim12"].ToString();
                    txtA3P12.Text = dt.Rows[0]["A3Per12"].ToString();
                    txtA3remarks.Text = dt.Rows[0]["A3Remrks"].ToString();


                    txtB1calculation1.Text = dt.Rows[0]["B1Cal1"].ToString();
                    txtB1F1.Text = dt.Rows[0]["B1found1"].ToString();
                    txtB1Cl1.Text = dt.Rows[0]["B1claim1"].ToString();
                    txtB1P1.Text = dt.Rows[0]["B1Per1"].ToString();
                    txtB1calculation2.Text = dt.Rows[0]["B1Cal2"].ToString();
                    txtB1F2.Text = dt.Rows[0]["B1found2"].ToString();
                    txtB1Cl2.Text = dt.Rows[0]["B1claim2"].ToString();
                    txtB1P2.Text = dt.Rows[0]["B1Per2"].ToString();
                    txtB1calculation3.Text = dt.Rows[0]["B1Cal3"].ToString();
                    txtB1F3.Text = dt.Rows[0]["B1found3"].ToString();
                    txtB1Cl3.Text = dt.Rows[0]["B1claim3"].ToString();
                    txtB1P3.Text = dt.Rows[0]["B1Per3"].ToString();
                    txtB1calculation4.Text = dt.Rows[0]["B1Cal4"].ToString();
                    txtB1F4.Text = dt.Rows[0]["B1found4"].ToString();
                    txtB1Cl4.Text = dt.Rows[0]["B1claim4"].ToString();
                    txtB1P4.Text = dt.Rows[0]["B1Per4"].ToString();
                    txtB1calculation5.Text = dt.Rows[0]["B1Cal5"].ToString();
                    txtB1F5.Text = dt.Rows[0]["B1found5"].ToString();
                    txtB1Cl5.Text = dt.Rows[0]["B1claim5"].ToString();
                    txtB1P5.Text = dt.Rows[0]["B1Per5"].ToString();
                    txtB1calculation6.Text = dt.Rows[0]["B1Cal6"].ToString();
                    txtB1F6.Text = dt.Rows[0]["B1found6"].ToString();
                    txtB1Cl6.Text = dt.Rows[0]["B1claim6"].ToString();
                    txtB1P6.Text = dt.Rows[0]["B1Per6"].ToString();
                    txtB1Remrks.Text = dt.Rows[0]["B1Remrks"].ToString();

                    txtB2calculation1.Text = dt.Rows[0]["B2Cal1"].ToString();
                    txtB2F1.Text = dt.Rows[0]["B2found1"].ToString();
                    txtB2Cl1.Text = dt.Rows[0]["B2claim1"].ToString();
                    txtB2P1.Text = dt.Rows[0]["B2Per1"].ToString();
                    txtB2calculation2.Text = dt.Rows[0]["B2Cal2"].ToString();
                    txtB2F2.Text = dt.Rows[0]["B2found2"].ToString();
                    txtB2Cl2.Text = dt.Rows[0]["B2claim2"].ToString();
                    txtB2P2.Text = dt.Rows[0]["B2Per2"].ToString();
                    txtB2calculation3.Text = dt.Rows[0]["B2Cal3"].ToString();
                    txtB2F3.Text = dt.Rows[0]["B2found3"].ToString();
                    txtB2Cl3.Text = dt.Rows[0]["B2claim3"].ToString();
                    txtB2P3.Text = dt.Rows[0]["B2Per3"].ToString();
                    txtB2calculation4.Text = dt.Rows[0]["B2Cal4"].ToString();
                    txtB2F4.Text = dt.Rows[0]["B2found4"].ToString();
                    txtB2Cl4.Text = dt.Rows[0]["B2claim4"].ToString();
                    txtB2P4.Text = dt.Rows[0]["B2Per4"].ToString();
                    txtB2calculation5.Text = dt.Rows[0]["B2Cal5"].ToString();
                    txtB2F5.Text = dt.Rows[0]["B2found5"].ToString();
                    txtB2Cl5.Text = dt.Rows[0]["B2claim5"].ToString();
                    txtB2P5.Text = dt.Rows[0]["B2Per5"].ToString();
                    txtB2calculation6.Text = dt.Rows[0]["B2Cal6"].ToString();
                    txtB2F6.Text = dt.Rows[0]["B2found6"].ToString();
                    txtB2Cl6.Text = dt.Rows[0]["B2claim6"].ToString();
                    txtB2P6.Text = dt.Rows[0]["B2Per6"].ToString();
                    txtB2remarks.Text = dt.Rows[0]["B2Remrks"].ToString();

                    txtB3calculation1.Text = dt.Rows[0]["B3Cal1"].ToString();
                    txtB3F1.Text = dt.Rows[0]["B3found1"].ToString();
                    txtB3Cl1.Text = dt.Rows[0]["B3claim1"].ToString();
                    txtB3P1.Text = dt.Rows[0]["B3Per1"].ToString();
                    txtB3calculation2.Text = dt.Rows[0]["B3Cal2"].ToString();
                    txtB3F2.Text = dt.Rows[0]["B3found2"].ToString();
                    txtB3Cl2.Text = dt.Rows[0]["B3claim2"].ToString();
                    txtB3P2.Text = dt.Rows[0]["B3Per2"].ToString();
                    txtB3calculation3.Text = dt.Rows[0]["B3Cal3"].ToString();
                    txtB3F3.Text = dt.Rows[0]["B3found3"].ToString();
                    txtB3Cl3.Text = dt.Rows[0]["B3claim3"].ToString();
                    txtB3P3.Text = dt.Rows[0]["B3Per3"].ToString();
                    txtB3calculation4.Text = dt.Rows[0]["B3Cal4"].ToString();
                    txtB3F4.Text = dt.Rows[0]["B3found4"].ToString();
                    txtB3Cl4.Text = dt.Rows[0]["B3claim4"].ToString();
                    txtB3P4.Text = dt.Rows[0]["B3Per4"].ToString();
                    txtB3calculation5.Text = dt.Rows[0]["B3Cal5"].ToString();
                    txtB3F5.Text = dt.Rows[0]["B3found5"].ToString();
                    txtB3Cl5.Text = dt.Rows[0]["B3claim5"].ToString();
                    txtB3P5.Text = dt.Rows[0]["B3Per5"].ToString();
                    txtB3calculation6.Text = dt.Rows[0]["B3Cal6"].ToString();
                    txtB3F6.Text = dt.Rows[0]["B3found6"].ToString();
                    txtB3Cl6.Text = dt.Rows[0]["B3claim6"].ToString();
                    txtB3P6.Text = dt.Rows[0]["B3Per6"].ToString();
                    txtB3calculation7.Text = dt.Rows[0]["B3Cal7"].ToString();
                    txtB3F7.Text = dt.Rows[0]["B3found7"].ToString();
                    txtB3Cl7.Text = dt.Rows[0]["B3claim7"].ToString();
                    txtB3P7.Text = dt.Rows[0]["B3Per7"].ToString();
                    txtB3calculation8.Text = dt.Rows[0]["B3Cal8"].ToString();
                    txtB3F8.Text = dt.Rows[0]["B3found8"].ToString();
                    txtB3Cl8.Text = dt.Rows[0]["B3claim8"].ToString();
                    txtB3P8.Text = dt.Rows[0]["B3Per8"].ToString();
                    txtB3calculation9.Text = dt.Rows[0]["B3Cal9"].ToString();
                    txtB3F9.Text = dt.Rows[0]["B3found9"].ToString();
                    txtB3Cl9.Text = dt.Rows[0]["B3claim9"].ToString();
                    txtB3P9.Text = dt.Rows[0]["B3Per9"].ToString();
                    txtB3calculation10.Text = dt.Rows[0]["B3Cal10"].ToString();
                    txtB3F10.Text = dt.Rows[0]["B3found10"].ToString();
                    txtB3Cl10.Text = dt.Rows[0]["B3claim10"].ToString();
                    txtB3P10.Text = dt.Rows[0]["B3Per10"].ToString();
                    txtB3calculation11.Text = dt.Rows[0]["B3Cal11"].ToString();
                    txtB3F11.Text = dt.Rows[0]["B3found11"].ToString();
                    txtB3Cl11.Text = dt.Rows[0]["B3claim11"].ToString();
                    txtB3P11.Text = dt.Rows[0]["B3Per11"].ToString();
                    txtB3calculation12.Text = dt.Rows[0]["B3Cal12"].ToString();
                    txtB3F12.Text = dt.Rows[0]["B3found12"].ToString();
                    txtB3Cl12.Text = dt.Rows[0]["B3claim12"].ToString();
                    txtB3P12.Text = dt.Rows[0]["B3Per12"].ToString();
                    txtB3remarks.Text = dt.Rows[0]["B3Remrks"].ToString();

                    GVTestResult.DataSource = dt;
                    GVTestResult.DataBind();

                    trtab.Attributes.Add("class", "active");
                    pnltestresult.Visible = true;
                    editprofile1.Visible = true;
                }
            }
            else
            {
                cf.ShowAlertMessage("Test Results Already Entered for This Sample OR not Alloted to You");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            //Response.Redirect(ex.Message.ToString());
        }
    }
    protected void BindGrid()
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
        GVTestResult.DataSource = dtparam;
        GVTestResult.DataBind();
    }
    protected void GVTestResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label testParam = (e.Row.FindControl("lblTestParameter") as Label);
            DropDownList ddlTestParameter = (DropDownList)e.Row.FindControl("ddlTestParameter");
            DataTable dt1 = new DataTable();
            objBE.Action = "PARAM";
            objBE.SampleID = txtsample.Text.Trim();

            if (Session["RoleID"].ToString() == "5")
                objBE.AnalystId = Analystcode;
            if (Session["RoleID"].ToString() == "6")
                objBE.AnalystId = Uocode;

            dt1 = ObjDL.bulkdata(objBE, con);
            cf.BindDropDownLists(ddlTestParameter, dt1, "Parameter", "Parameter_Id", "Select Parameter");
            ddlTestParameter.SelectedValue = testParam.Text;

            Label testtestprotocal = (e.Row.FindControl("lbltestpr") as Label);
            DropDownList ddlTestProtocal = (DropDownList)e.Row.FindControl("ddlTestProtocol");
            dt1 = new DataTable();
            Obj.Action = "PROTOCOL";
            dt1 = ObjDAL.Getdetails(Obj, con);
            cf.BindDropDownLists(ddlTestProtocal, dt1, "ProtocolName", "Protocolid", "Select");
            ddlTestProtocal.SelectedValue = testtestprotocal.Text;
        }
    }
    protected void GVTestResult_RowCommand(object sender, GridViewCommandEventArgs e)
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
                foreach (GridViewRow gr in GVTestResult.Rows)
                {

                    DropDownList Paramid = (DropDownList)gr.FindControl("ddlTestParameter");
                    TextBox testanalysttfor = (TextBox)gr.FindControl("txttestfor");
                    TextBox testName = (TextBox)gr.FindControl("txtTestDone");
                    TextBox calculation = (TextBox)gr.FindControl("txtcalculation");

                    DropDownList TestProtocol = (DropDownList)gr.FindControl("ddlTestProtocol");
                    TextBox found = (TextBox)gr.FindControl("txtValueFound");
                    TextBox claim = (TextBox)gr.FindControl("txtValueClaim");
                    TextBox limit = (TextBox)gr.FindControl("txtlimit");

                    if (ValidateGrid(Paramid, testanalysttfor, testName, calculation, TestProtocol, found, claim, limit))
                    {
                        dtparam.Rows.Add();
                        dtparam.Rows[j]["TestParam"] = ((DropDownList)gr.FindControl("ddlTestParameter")).SelectedValue.Trim();
                        dtparam.Rows[j]["Testfor"] = ((TextBox)gr.FindControl("txttestfor")).Text.Trim();
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
                GVTestResult.DataSource = dtparam;
                GVTestResult.DataBind();
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
                foreach (GridViewRow gr in GVTestResult.Rows)
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["TestParam"] = ((DropDownList)gr.FindControl("ddlTestParameter")).SelectedValue.Trim();
                    dtparam.Rows[j]["Testfor"] = ((TextBox)gr.FindControl("txttestfor")).Text.Trim();
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
                GVTestResult.DataSource = dtparam;
                GVTestResult.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected bool ValidateGrid(DropDownList ddlParam, TextBox Testfor, TextBox testnm, TextBox cal, DropDownList ddlpr, TextBox fnd, TextBox clm, TextBox lmt)
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
            val =CheckInput_new(cal.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        //    else if (ddlpr.SelectedIndex == 0)
        //    {
        //        cf.ShowAlertMessage("Select Test Protocol");
        //        ddlpr.Focus();
        //        return false;
        //    }
        //    else if (fnd.Text == "")
        //    {
        //        cf.ShowAlertMessage("Enter Found Values");
        //        fnd.Focus();
        //        return false;
        //    }
        //    else if (clm.Text == "")
        //    {
        //        cf.ShowAlertMessage("Enter Claim Values");
        //        clm.Focus();
        //        return false;
        //    }
        //    else if (lmt.Text == "")
        //    {
        //        cf.ShowAlertMessage("Enter Limit Values");
        //        lmt.Focus();
        //        return false;
        //    }
        //}
        return true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (validate())
            {
                objBE.Action = "TR";
                objBE.SampleID = txtsample.Text.Trim();
                if (Session["RoleID"].ToString() == "5")
                    objBE.AnalystId = Analystcode;
                if (Session["RoleID"].ToString() == "6")
                    objBE.AnalystId = Uocode;

                objBE.user = Session["RoleID"].ToString();
                objBE.startdt = cf.Texttodateconverter(txtStartDt.Text);
                objBE.enddt = cf.Texttodateconverter(txtEndDt.Text);
                objBE.Remarks = txtremarks.Text.Trim();
                objBE.description = txtDesc.Text.Trim();
                objBE.TVP = null;

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
                foreach (GridViewRow gr in GVTestResult.Rows)
                {
                    DropDownList Paramid = (DropDownList)gr.FindControl("ddlTestParameter");
                    TextBox testanalysttfor = (TextBox)gr.FindControl("txttestfor");
                    TextBox testName = (TextBox)gr.FindControl("txtTestDone");

                    TextBox calculation = (TextBox)gr.FindControl("txtcalculation");
                    DropDownList TestProtocol = (DropDownList)gr.FindControl("ddlTestProtocol");
                    TextBox found = (TextBox)gr.FindControl("txtValueFound");
                    TextBox claim = (TextBox)gr.FindControl("txtValueClaim");
                    TextBox limit = (TextBox)gr.FindControl("txtlimit");

                    if (ValidateGrid(Paramid, testanalysttfor, testName, calculation, TestProtocol, found, claim, limit))
                    {
                        dtparam.Rows.Add();
                        dtparam.Rows[i]["TestParam"] = ((DropDownList)gr.FindControl("ddlTestParameter")).SelectedValue.Trim();
                        dtparam.Rows[i]["Testfor"] = ((TextBox)gr.FindControl("txttestfor")).Text.Trim();
                        dtparam.Rows[i]["TestProtocol"] = ((DropDownList)gr.FindControl("ddlTestProtocol")).SelectedValue.Trim();
                        dtparam.Rows[i]["Calculation"] = ((TextBox)gr.FindControl("txtcalculation")).Text.Trim();
                        dtparam.Rows[i]["TestName"] = ((TextBox)gr.FindControl("txtTestDone")).Text.Trim();
                        if (((TextBox)gr.FindControl("txtValueFound")).Text.Trim() == "")
                            dtparam.Rows[i]["found"] = "0";
                        else
                            dtparam.Rows[i]["found"] = ((TextBox)gr.FindControl("txtValueFound")).Text.Trim();
                        if (((TextBox)gr.FindControl("txtValueClaim")).Text.Trim() == "")
                            dtparam.Rows[i]["claim"] = "0";
                        else
                            dtparam.Rows[i]["claim"] = ((TextBox)gr.FindControl("txtValueClaim")).Text.Trim();
                        if (((TextBox)gr.FindControl("txtlimit")).Text.Trim() == "")
                            dtparam.Rows[i]["limit"] = "0";
                        else
                            dtparam.Rows[i]["limit"] = ((TextBox)gr.FindControl("txtlimit")).Text.Trim();
                    }
                    i++;
                }
                objBE.AckTVP = dtparam;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("Test results saved");
                    clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BtnS1_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (S1validate())
            {
                objBE.Action = "DS";
                objBE.flag = "S1I";
                objBE.SampleID = txtsample.Text.Trim();
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

                objBE.Ds1TVP = dtparam;
                objBE.Remarks = txts1Remrks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("S1 Dissolution results saved ");
                    S1Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
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
                objBE.flag = "S2I";
                objBE.SampleID = txtsample.Text.Trim();
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

                objBE.Ds2TVP = dtparam;
                objBE.Remarks = txts2remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("S2 Dissolution results saved ");
                    S2Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BtnS3_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (S3validate())
            {
                objBE.Action = "DS";
                objBE.flag = "S3I";
                objBE.SampleID = txtsample.Text.Trim();
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

                objBE.Ds3TVP = dtparam;
                objBE.Remarks = txts3remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("S3 Dissolution results saved");
                    S3Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
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

    protected void clear()
    {
        lblComposition.Text = "";
        lbldrugnm.Text = "";
        lblmfgdt.Text = "";
        lblqty.Text = "";
        lblexpdt.Text = "";
        lbllab.Text = "";
        txtStartDt.Text = "";
        txtEndDt.Text = "";
        txtremarks.Text = "";
        GVTestResult.DataSource = null;
        GVTestResult.DataBind();
        BtnTR.Visible = false;
    }
    protected void S1Clear()
    {

        txtS1calculation1.Text = "";
        txts1F1.Text = "";
        txts1Cl1.Text = "";
        txts1P1.Text = "";
        txtS1calculation2.Text = "";
        txts1F2.Text = "";
        txts1Cl2.Text = "";
        txts1P2.Text = "";
        txtS1calculation3.Text = "";
        txts1F3.Text = "";
        txts1Cl3.Text = "";
        txts1P3.Text = "";
        txtS1calculation4.Text = "";
        txts1F4.Text = "";
        txts1Cl4.Text = "";
        txts1P4.Text = "";
        txtS1calculation5.Text = "";
        txts1F5.Text = "";
        txts1Cl5.Text = "";
        txts1P5.Text = "";
        txtS1calculation6.Text = "";
        txts1F6.Text = "";
        txts1Cl6.Text = "";
        txts1P6.Text = "";
        txts1Remrks.Text = "";
    }
    protected void S2Clear()
    {
        //   txtsample.Text = "";
        txts2calculation1.Text = "";
        txts2F1.Text = "";
        txts2Cl1.Text = "";
        txts2P1.Text = "";
        txts2calculation2.Text = "";
        txts2F2.Text = "";
        txts2Cl2.Text = "";
        txts2P2.Text = "";
        txts2calculation3.Text = "";
        txts2F3.Text = "";
        txts2Cl3.Text = "";
        txts2P3.Text = "";
        txts2calculation4.Text = "";
        txts2F4.Text = "";
        txts2Cl4.Text = "";
        txts2P4.Text = "";
        txts2calculation5.Text = "";
        txts2F5.Text = "";
        txts2Cl5.Text = "";
        txts2P5.Text = "";
        txts2calculation6.Text = "";
        txts2F6.Text = "";
        txts2Cl6.Text = "";
        txts2P6.Text = "";
        txts2remarks.Text = "";


    }
    protected void S3Clear()
    {

        txts3calculation1.Text = "";
        txts3F1.Text = "";
        txts3Cl1.Text = "";
        txts3P1.Text = "";
        txts3calculation2.Text = "";
        txts2F2.Text = "";
        txts3Cl2.Text = "";
        txts3P2.Text = "";
        txts3calculation3.Text = "";
        txts3F3.Text = "";
        txts3Cl3.Text = "";
        txts3P3.Text = "";
        txts3calculation4.Text = "";
        txts3F4.Text = "";
        txts3Cl4.Text = "";
        txts3P4.Text = "";
        txts3calculation5.Text = "";
        txts3F5.Text = "";
        txts3Cl5.Text = "";
        txts3P5.Text = "";
        txts3calculation6.Text = "";
        txts3F6.Text = "";
        txts3Cl6.Text = "";
        txts3P6.Text = "";
        txts3calculation7.Text = "";
        txts3F7.Text = "";
        txts3Cl7.Text = ""; ;
        txts3P7.Text = "";
        txts3calculation8.Text = "";
        txts3F8.Text = "";
        txts3Cl8.Text = "";
        txts3P8.Text = "";
        txts3calculation9.Text = "";
        txts3F9.Text = "";
        txts3Cl9.Text = "";
        txts3P9.Text = "";
        txts3calculation10.Text = "";
        txts3F10.Text = "";
        txts3Cl10.Text = "";
        txts3P10.Text = "";
        txts3calculation11.Text = "";
        txts3F11.Text = "";
        txts3Cl11.Text = "";
        txts3P11.Text = "";
        txts3calculation12.Text = "";
        txts3F12.Text = "";
        txts3Cl12.Text = "";
        txts3P12.Text = "";
        txts3remarks.Text = "";


    }
    protected void A1Clear()
    {

        txtA1calculation1.Text = "";
        txtA1F1.Text = "";
        txtA1Cl1.Text = "";
        txtA1P1.Text = "";
        txtA1calculation2.Text = "";
        txtA1F2.Text = "";
        txtA1Cl2.Text = "";
        txtA1P2.Text = "";
        txtA1calculation3.Text = "";
        txtA1F3.Text = "";
        txtA1Cl3.Text = "";
        txtA1P3.Text = "";
        txtA1calculation4.Text = "";
        txtA1F4.Text = "";
        txtA1Cl4.Text = "";
        txtA1P4.Text = "";
        txtA1calculation5.Text = "";
        txtA1F5.Text = "";
        txtA1Cl5.Text = "";
        txtA1P5.Text = "";
        txtA1calculation6.Text = "";
        txtA1F6.Text = "";
        txtA1Cl6.Text = "";
        txtA1P6.Text = "";
        txtA1Remrks.Text = "";
    }
    protected void A2Clear()
    {
        //   txtsample.Text = "";
        txtA2calculation1.Text = "";
        txtA2F1.Text = "";
        txtA2Cl1.Text = "";
        txtA2P1.Text = "";
        txtA2calculation2.Text = "";
        txtA2F2.Text = "";
        txtA2Cl2.Text = "";
        txtA2P2.Text = "";
        txtA2calculation3.Text = "";
        txtA2F3.Text = "";
        txtA2Cl3.Text = "";
        txtA2P3.Text = "";
        txtA2calculation4.Text = "";
        txtA2F4.Text = "";
        txtA2Cl4.Text = "";
        txtA2P4.Text = "";
        txtA2calculation5.Text = "";
        txtA2F5.Text = "";
        txtA2Cl5.Text = "";
        txtA2P5.Text = "";
        txtA2calculation6.Text = "";
        txtA2F6.Text = "";
        txtA2Cl6.Text = "";
        txtA2P6.Text = "";
        txtA2remarks.Text = "";


    }
    protected void A3Clear()
    {

        txtA3calculation1.Text = "";
        txtA3F1.Text = "";
        txtA3Cl1.Text = "";
        txtA3P1.Text = "";
        txtA3calculation2.Text = "";
        txtA2F2.Text = "";
        txtA3Cl2.Text = "";
        txtA3P2.Text = "";
        txtA3calculation3.Text = "";
        txtA3F3.Text = "";
        txtA3Cl3.Text = "";
        txtA3P3.Text = "";
        txtA3calculation4.Text = "";
        txtA3F4.Text = "";
        txtA3Cl4.Text = "";
        txtA3P4.Text = "";
        txtA3calculation5.Text = "";
        txtA3F5.Text = "";
        txtA3Cl5.Text = "";
        txtA3P5.Text = "";
        txtA3calculation6.Text = "";
        txtA3F6.Text = "";
        txtA3Cl6.Text = "";
        txtA3P6.Text = "";
        txtA3calculation7.Text = "";
        txtA3F7.Text = "";
        txtA3Cl7.Text = ""; ;
        txtA3P7.Text = "";
        txtA3calculation8.Text = "";
        txtA3F8.Text = "";
        txtA3Cl8.Text = "";
        txtA3P8.Text = "";
        txtA3calculation9.Text = "";
        txtA3F9.Text = "";
        txtA3Cl9.Text = "";
        txtA3P9.Text = "";
        txtA3calculation10.Text = "";
        txtA3F10.Text = "";
        txtA3Cl10.Text = "";
        txtA3P10.Text = "";
        txtA3calculation11.Text = "";
        txtA3F11.Text = "";
        txtA3Cl11.Text = "";
        txtA3P11.Text = "";
        txtA3calculation12.Text = "";
        txtA3F12.Text = "";
        txtA3Cl12.Text = "";
        txtA3P12.Text = "";
        txtA3remarks.Text = "";


    }
    protected void B1Clear()
    {

        txtB1calculation1.Text = "";
        txtB1F1.Text = "";
        txtB1Cl1.Text = "";
        txtB1P1.Text = "";
        txtB1calculation2.Text = "";
        txtB1F2.Text = "";
        txtB1Cl2.Text = "";
        txtB1P2.Text = "";
        txtB1calculation3.Text = "";
        txtB1F3.Text = "";
        txtB1Cl3.Text = "";
        txtB1P3.Text = "";
        txtB1calculation4.Text = "";
        txtB1F4.Text = "";
        txtB1Cl4.Text = "";
        txtB1P4.Text = "";
        txtB1calculation5.Text = "";
        txtB1F5.Text = "";
        txtB1Cl5.Text = "";
        txtB1P5.Text = "";
        txtB1calculation6.Text = "";
        txtB1F6.Text = "";
        txtB1Cl6.Text = "";
        txtB1P6.Text = "";
        txtB1Remrks.Text = "";
    }
    protected void B2Clear()
    {
        //   txtsample.Text = "";
        txtB2calculation1.Text = "";
        txtB2F1.Text = "";
        txtB2Cl1.Text = "";
        txtB2P1.Text = "";
        txtB2calculation2.Text = "";
        txtB2F2.Text = "";
        txtB2Cl2.Text = "";
        txtB2P2.Text = "";
        txtB2calculation3.Text = "";
        txtB2F3.Text = "";
        txtB2Cl3.Text = "";
        txtB2P3.Text = "";
        txtB2calculation4.Text = "";
        txtB2F4.Text = "";
        txtB2Cl4.Text = "";
        txtB2P4.Text = "";
        txtB2calculation5.Text = "";
        txtB2F5.Text = "";
        txtB2Cl5.Text = "";
        txtB2P5.Text = "";
        txtB2calculation6.Text = "";
        txtB2F6.Text = "";
        txtB2Cl6.Text = "";
        txtB2P6.Text = "";
        txtB2remarks.Text = "";


    }
    protected void B3Clear()
    {

        txtB3calculation1.Text = "";
        txtB3F1.Text = "";
        txtB3Cl1.Text = "";
        txtB3P1.Text = "";
        txtB3calculation2.Text = "";
        txtB2F2.Text = "";
        txtB3Cl2.Text = "";
        txtB3P2.Text = "";
        txtB3calculation3.Text = "";
        txtB3F3.Text = "";
        txtB3Cl3.Text = "";
        txtB3P3.Text = "";
        txtB3calculation4.Text = "";
        txtB3F4.Text = "";
        txtB3Cl4.Text = "";
        txtB3P4.Text = "";
        txtB3calculation5.Text = "";
        txtB3F5.Text = "";
        txtB3Cl5.Text = "";
        txtB3P5.Text = "";
        txtB3calculation6.Text = "";
        txtB3F6.Text = "";
        txtB3Cl6.Text = "";
        txtB3P6.Text = "";
        txtB3calculation7.Text = "";
        txtB3F7.Text = "";
        txtB3Cl7.Text = ""; ;
        txtB3P7.Text = "";
        txtB3calculation8.Text = "";
        txtB3F8.Text = "";
        txtB3Cl8.Text = "";
        txtB3P8.Text = "";
        txtB3calculation9.Text = "";
        txtB3F9.Text = "";
        txtB3Cl9.Text = "";
        txtB3P9.Text = "";
        txtB3calculation10.Text = "";
        txtB3F10.Text = "";
        txtB3Cl10.Text = "";
        txtB3P10.Text = "";
        txtB3calculation11.Text = "";
        txtB3F11.Text = "";
        txtB3Cl11.Text = "";
        txtB3P11.Text = "";
        txtB3calculation12.Text = "";
        txtB3F12.Text = "";
        txtB3Cl12.Text = "";
        txtB3P12.Text = "";
        txtB3remarks.Text = "";


    }

    protected void BtnA1_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (A1validate())
            {
                objBE.Action = "DS";
                objBE.flag = "A1I";
                objBE.SampleID = txtsample.Text.Trim();
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
                dtparam.Rows[0]["percentage_12"] = null;


                objBE.Ds1TVP = dtparam;
                objBE.Remarks = txtA1Remrks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("A1 Dissolution results saved");
                    A1Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
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
        check();
        try
        {
            if (A2validate())
            {
                objBE.Action = "DS";
                objBE.flag = "A2I";
                objBE.SampleID = txtsample.Text.Trim();
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


                objBE.Ds2TVP = dtparam;
                objBE.Remarks = txtA2remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("A2 Dissolution results saved ");
                    A2Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BtnA3_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (A3validate())
            {
                objBE.Action = "DS";
                objBE.flag = "A3I";
                objBE.SampleID = txtsample.Text.Trim();
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
                objBE.Remarks = txtA3remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("A3 Dissolution results saved ");
                    A3Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
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
                objBE.Action = "DS";
                objBE.flag = "B1I";
                objBE.SampleID = txtsample.Text.Trim();
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


                objBE.Ds1TVP = dtparam;
                objBE.Remarks = txtB1Remrks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("B1 Dissolution results saved ");
                    B1Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
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
                objBE.Action = "DS";
                objBE.flag = "B2I";
                objBE.SampleID = txtsample.Text.Trim();
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

                objBE.Ds2TVP = dtparam;
                objBE.Remarks = txtB2remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("B2 Dissolution results saved ");
                    B2Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
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
            {
                objBE.Action = "DS";
                objBE.flag = "B3I";
                objBE.SampleID = txtsample.Text.Trim();
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

                objBE.Ds3TVP = dtparam;
                objBE.Remarks = txtB3remarks.Text;
                objBE.ip = HttpContext.Current.Request.UserHostAddress;
                dt = ObjDL.bulkdata(objBE, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                else
                {
                    cf.ShowAlertMessage("B3 Dissolution results saved ");
                    S3Clear();
                    txtsample.Enabled = false;
                    pnltestresult.Visible = false;
                }
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