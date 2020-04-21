using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class AO_Ferti_Reg : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriDL Regobjdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objR = new AgriBE();
    DataTable dt;
    string con, user, state, Department, Aocode;
    SampleSqlInjectionScreeningModule obj = new SampleSqlInjectionScreeningModule();


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
                Response.Redirect("../Error.aspx");
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
            if (!IsPostBack)
            {
                try
                {
                    random();
                   
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindClass();
                    BindState();
                    BindSampeltype();
                    BindPhysicalCndtn();
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


    protected void BindClass()
    {
        objBE.Action = "CLS";
        objBE.Id = Department;
        objBE.CatId = Session["SampleCategory"].ToString();
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlClass, dt, "class_name", "class_id", "0");
    }
    protected void BindPhysicalCndtn()
    {
        objBE.Action = "PHY";
        objBE.Id = Department;
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlPhysicalCon, dt, "Physical_ConditionName", "Physical_Cid", "");
    }
    protected void BindState()
    {
        objBE.Action = "S";
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddlmnstate, dt, "State", "StateCode", "0");
        cf.BindDropDownLists(ddlstate, dt, "State", "StateCode", "0");
        ddlstate.SelectedValue = state;
        //ddlstate.Enabled = false;
        BindDistrict();
    }
    protected void BindDistrict()
    {
        objBE.Action = "D";
        objBE.statecode = ddlstate.SelectedValue;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddldist, dt, "DistName", "DistCode", "0");
        ddldist.SelectedValue = Session["DistCode"].ToString();
        //ddldist.Enabled = false;
    }
    protected void BindSampeltype()
    {
        objBE.Action = "R";
        objBE.Dept = Department;
        objBE.CatId = Session["SampleCategory"].ToString();
        dt = ObjDL.SampleTypeIUDR(objBE, con);
        cf.BindDropDownLists(ddlsmltype, dt, "SampleTypeName", "SampleTypeCode", "0");
    }
    protected void BindSampel()
    {
        objBE.Action = "R";
        objBE.Dept = Department;
        objBE.SampleTypeId = ddlsmltype.SelectedValue;
        dt = ObjDL.SampleIUDR(objBE, con);
        cf.BindDropDownLists(ddlsample, dt, "SampleName", "SampleCode", "0");
    }
    //protected void BindSpecificationGrid()
    //{
    //    dt = new DataTable();
    //    objBE.SampleID = ddlsample.SelectedValue;
    //    objBE.Action = "R";
    //    dt = ObjDL.Specifications_IUDR(objBE, con);
    //    GvSpec.DataSource = dt;
    //    GvSpec.DataBind();
    //}
    protected void ddlsmltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSampel();
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDistrict();
    }
    //protected void ddlsample_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindSpecificationGrid();
    //}
    protected void btnadd_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (Validatesave())
            {
                objR.DI_AO_Code = Aocode;
                objR.SampleClass = ddlClass.SelectedValue;
                objR.CodeSticker = txtcodesample.Text.Trim();
                objR.Firm_Name = txtfirnmdlr.Text.Trim();
                objR.ownername = txtnmowner.Text.Trim();
                objR.LicenceNo = txtdllcno.Text.Trim();
                objR.Validity = cf.Texttodateconverter(txtvldty.Text.Trim());
                objR.BatchNo = txtbthchno.Text.Trim();
                objR.ManufacturingDate = cf.Texttodateconverter(txtdtmn.Text.Trim());
                objR.StockPosition = txtstockl.Text.Trim();

                objR.StkRcvdDate = cf.Texttodateconverter(txtdatemipagency.Text.Trim());
                objR.state = ddlstate.SelectedValue;
                objR.District = ddldist.SelectedValue;
                objR.HouseNo = txthsno.Text.Trim();
                objR.Locality = txtlcstr.Text.Trim();
                objR.SmplCollectingDt = cf.Texttodateconverter(txtdtsam.Text.Trim());
                objR.SampleType = ddlsmltype.SelectedValue;
                objR.SampleID = ddlsample.SelectedValue;
                objR.ManfucaturerName = txtmnname.Text.Trim();
                objR.Address = txtaddrs.Text.Trim();
                objR.SmplDrawnBag = RblSmpleDrawnFrom.SelectedValue;
                objR.punchnama = Rblphnma.SelectedValue;
                objR.login_state = state;
                objR.dept = Department;
                objR.user = user;
                objR.PhycicalCondition = ddlPhysicalCon.SelectedValue;
                objR.ManufacturerState = ddlmnstate.SelectedValue;

                // DataTable dtspecfic = new DataTable();
                // dtspecfic.Columns.Add("Parameter_ID", typeof(Int64));
                //// dtspecfic.Columns.Add("Checkid", typeof(Int64));
                // int j = 0;
                // foreach (GridViewRow gr in GvSpec.Rows)
                // {
                //     if (((CheckBox)gr.FindControl("chkSelct")).Checked == true)
                //     {
                //         dtspecfic.Rows.Add();
                //         dtspecfic.Rows[j]["Parameter_ID"] = Convert.ToDecimal(((Label)gr.FindControl("lblSpecCode")).Text);
                //         //dtspecfic.Rows[j]["Checkid"] = "1";
                //         j++;
                //     }
                // }
                // if (j == 0)
                //     cf.ShowAlertMessage("Select atleast one row");
                // else
                // {
                //     objR.TVP = dtspecfic;
                objR.SampleCategory = Session["SampleCategory"].ToString();

                objR.Action = "I";
                dt = Regobjdl.FertlizerRegistration(objR, con);
                
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Fertilizer Registered Successfully With Sample ID: " + dt.Rows[0][0].ToString());
                else
                    cf.ShowAlertMessage("Save failed, please try again");
                Clear();
                //    GvSpec.Visible = false;

                //}
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("../Error.aspx");
        }
    }

    protected void Clear()
    {
        txtaddrs.Text = "";
        txtbthchno.Text = "";
        txtcodesample.Text = "";
        txtdllcno.Text = "";
        txtdatemipagency.Text = "";
        txtdtmn.Text = "";
        txtdtsam.Text = "";
        txtfirnmdlr.Text = "";
        txthsno.Text = "";
        txtlcstr.Text = "";
        txtmnname.Text = "";
        txtnmowner.Text = "";
        txtstockl.Text = "";
        txtvldty.Text = "";
        txtvldty.Text = "";
        ddlClass.ClearSelection();
        ddldist.ClearSelection();
        ddlsample.ClearSelection();
        ddlsmltype.ClearSelection();
        ddlstate.ClearSelection();
        Rblphnma.ClearSelection();
        RblSmpleDrawnFrom.ClearSelection();
        ddlPhysicalCon.ClearSelection();
        ddlmnstate.ClearSelection();


    }
    public bool Validatesave()
    {
        if (ddlClass.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Sample Class");
            ddlClass.Focus();
            return false;
        }
        if (txtcodesample.Text == "")
        {
            cf.ShowAlertMessage("Enter Code No. of Sample");
            txtcodesample.Focus();
            return false;
        }
        if (txtcodesample.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtcodesample.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtfirnmdlr.Text == "")
        {
            cf.ShowAlertMessage("Enter Name of the Firm/Dealer");
            txtfirnmdlr.Focus();
            return false;
        }
        if (txtfirnmdlr.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtcodesample.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtnmowner.Text == "")
        {
            cf.ShowAlertMessage("Enter Name of the Owner");
            txtnmowner.Focus();
            return false;
        }
        if (txtnmowner.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtnmowner.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtdllcno.Text == "")
        {
            cf.ShowAlertMessage("Enter Licence No");
            txtdllcno.Focus();
            return false;
        }
        if (txtdllcno.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtdllcno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        //if (txtvldty.Text.Trim() != "")
        //{

        //    bool val;
        //    val = obj.CheckInput_new(txtvldty.Text);
        //    if (val == true)
        //        Response.Redirect("~/Error.aspx");

        //    if (!objValidate.IsDate(txtvldty.Text.Trim()))
        //    {
        //        cf.ShowAlertMessage("Enter Valid  Date");
        //        txtvldty.Focus();
        //        return false;
        //    }
        //}
        if (txtbthchno.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Batch No");
            txtbthchno.Focus();
            return false;
        }

        if (txtbthchno.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtbthchno.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtdtmn.Text.Trim() != "")
        {

            bool val;
            val = obj.CheckInput_new(txtdtmn.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtstockl.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Stock position of the Lot");
            txtstockl.Focus();
            return false;
        }

        if (txtstockl.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtstockl.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtdatemipagency.Text == "")
        {
            cf.ShowAlertMessage("Date of Receipt of the stock by the dealer/manufacture/importer/pool handling agency");
            txtdatemipagency.Focus();
            return false;
        }

        if (txtdatemipagency.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtdatemipagency.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (ddlstate.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select State Name");
            ddlstate.Focus();
            return false;
        }
        if (ddldist.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select District Name");
            ddldist.Focus();
            return false;
        }
        if (txthsno.Text == "")
        {
            cf.ShowAlertMessage("Please Enter House No. Flat No");
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
        if (txtlcstr.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Locality/Street Name");
            txtlcstr.Focus();
            return false;
        }

        if (txtlcstr.Text != "")
        {


            bool val;
            val = obj.CheckInput_new(txtlcstr.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        //if (txtdtsam.Text.Trim() != "")
        //{

        //    bool val;
        //    val = obj.CheckInput_new(txtdtsam.Text);
        //    if (val == true)
        //        Response.Redirect("~/Error.aspx");

        //    if (!objValidate.IsDate(txtdtsam.Text.Trim()))
        //    {
        //        cf.ShowAlertMessage("Enter Valid  Date");
        //        txtdtsam.Focus();
        //        return false;
        //    }
        //}
        if (ddlsmltype.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Sample type");
            ddlsmltype.Focus();
            return false;
        }
        if (ddlsample.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Sample Grade");
            ddlsample.Focus();
            return false;
        }
        if (ddlPhysicalCon.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Physical Condition");
            ddlPhysicalCon.Focus();
            return false;
        }
        if (txtmnname.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Locality/Street Name");
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
        if (txtaddrs.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Manufacturer Address");
            txtaddrs.Focus();
            return false;
        }

        if (txtaddrs.Text != "")
        {


            bool val;
            val = obj.CheckInput_new(txtaddrs.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (RblSmpleDrawnFrom.SelectedIndex == -1)
        {
            cf.ShowAlertMessage("Select from Which Bag Samples is drwan ");
            RblSmpleDrawnFrom.Focus();
            return false;
        }
        if (Rblphnma.SelectedIndex == -1)
        {
            cf.ShowAlertMessage("Select whether Panchanama Conducted");
            ddlsample.Focus();
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