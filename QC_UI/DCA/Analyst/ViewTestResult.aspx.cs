using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;

public partial class DCA_Analyst_ViewTestResult : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "5")
        {
            con = Session["ConnKey"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["AnalystName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                random();
                BindGrid();
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
            objBE.Action = "ANVIEW";
            objBE.AnalystId = Session["AnalystCode"].ToString();
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
    protected void GVTestResult_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewResult")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            lblsamplid.Text = ((Label)(gvrow.FindControl("lblSampleid"))).Text;
            ViewData();
            if (gvrow.Cells[4].Text.ToString() == "YES")
                ViewS1();
            else
                divs1.Visible = false; ;

            if (gvrow.Cells[5].Text.ToString() == "YES")
                ViewS2();
            else
                divs2.Visible = false;

            if (gvrow.Cells[6].Text.ToString() == "YES")
                ViewS3();
            else
                divs3.Visible = false;
        }
    }

    protected void ViewData()
    {
        objBE.Action = "TR";
        objBE.SampleID = lblsamplid.Text.Trim();
        DataTable dtt = new DataTable();
        dtt = ObjDL.JAActionEdit(objBE, con);
        if (dtt.Rows.Count > 0)
        {
            DivViewData.Visible = true;
            lbllab.Text = dtt.Rows[0]["LabName"].ToString();
            lbljaName.Text = dtt.Rows[0]["JAName"].ToString();
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
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
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
    protected void ViewS2()
    {
        divs2.Visible = true;
        objBE.Action = "RS2";
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {

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

    protected void ViewS3()
    {
        divs3.Visible = true;
        objBE.SampleID = lblsamplid.Text.Trim();
        objBE.Action = "S3R";
        dt = new DataTable();
        dt = ObjDL.JAActionEdit(objBE, con);
        if (dt.Rows.Count > 0)
        {
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

    //protected void BtnSave_Click(object sender, EventArgs e)
    //{
    //    dt = new DataTable();
    //    objBE.UOStatus = rblstatus.SelectedValue;
    //    objBE.UOId = Session["UnitOfficerCode"].ToString();
    //    objBE.UORemarks = txtremarks.Text.Trim();
    //    objBE.Action = "UO";
    //    objBE.SampleID = lblsamplid.Text;

    //    DataTable dtparam = new DataTable();
    //    dtparam.Columns.Add("TestID", typeof(string));
    //    dtparam.Columns.Add("testRemarks", typeof(string));
       

    //    int i = 0;
    //    foreach (GridViewRow gr in GvResult.Rows)
    //    {
    //        TextBox testremarks = (TextBox)gr.FindControl("txtremarks");

    //        if (ValidateGrid(testremarks))
    //        {
    //            dtparam.Rows.Add();
    //            dtparam.Rows[i]["TestID"] = ((Label)gr.FindControl("lbltestids")).Text.Trim();
    //            dtparam.Rows[i]["testRemarks"] = ((TextBox)gr.FindControl("txtremarks")).Text.Trim();
    //        }
    //        i++;
    //    }
    //    objBE.UOTVP = dtparam;
    //    objBE.TVP = null;

    //    dt = ObjDL.JAActionEdit(objBE, con);
    //    if (dt.Rows.Count > 0)
    //        cf.ShowAlertMessage("not Saved , try again");
    //    else
    //        cf.ShowAlertMessage("Saved");
    //    rblstatus.SelectedIndex = 0;
    //    txtremarks.Text = "";
    //    BindGrid();
    //    DivViewData.Visible = false;
    //}
    protected bool ValidateGrid(TextBox rmrks)
    {

        if (rmrks.Text == "")
        {
            cf.ShowAlertMessage("Enter Remarks");
            rmrks.Focus();
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