using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class AO_EditFertilizer : System.Web.UI.Page
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
                  //  lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;                    
                    BindGrid();
                    pnleditfertilizer.Visible = false;
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                   Response.Redirect("~/Error.aspx");
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
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlClass, dt, "class_name", "class_id", "0");
    }
    protected void BindGrid()
    {
        objR.Action = "R";
        objR.DI_AO_Code = Aocode;
        objR.dept = Department;
        objR.SampleCategory = Session["SampleCategory"].ToString();
        dt = Regobjdl.FertlizerRegistration(objR, con);     
        if (dt.Rows.Count > 0)
        {
            Gvfreeze.DataSource = dt;
            Gvfreeze.DataBind();
        }
    }
    protected void BindState()
    {
        objBE.Action = "S";
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddlstate, dt, "State", "StateCode", "0");
        cf.BindDropDownLists(ddlmnstate, dt, "State", "StateCode", "0");
        ddlstate.SelectedValue = state;
        ddlstate.Enabled = false;
    }
    protected void BindDistrict()
    {
        objBE.Action = "D";
        objBE.statecode = state;
        dt = ObjDL.GetLocations(objBE, con);
        cf.BindDropDownLists(ddldist, dt, "DistName", "DistCode", "0");
        ddldist.SelectedValue=Session["DistCode"].ToString();
        ddldist.Enabled=false;
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
        cf.BindDropDownLists(ddlsample, dt, "SampleName", "SampleCode", "0");
    }
    protected void BindPhysicalCndtn()
    {
        objBE.Action = "PHY";
        objBE.Id = Department;
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindDropDownLists(ddlPhysicalCon, dt, "Physical_ConditionName", "Physical_Cid", "");
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
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDistrict();
    }
    protected void ddlsampleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSampel();
    }
    //protected void ddlsample_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindSpecificationGrid();
    //}
    protected void Gvfreeze_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;      
            pnleditfertilizer.Visible = true;
            lblFertilizerno.Visible = true;
            lblFertilizerno.Text = ((Label)(gvrow.FindControl("lblsmid"))).Text;
            BindData();
        }
        if (e.CommandName == "DLT")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            lblsmpleid.Text = ((Label)(gvrow.FindControl("lblsmid"))).Text;
            objR.SRegid = lblsmpleid.Text;
            objR.Action = "D";
            dt = Regobjdl.SampleRegistration(objR, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Delete Failed");
            }
            else
            {
                cf.ShowAlertMessage("Delete Succusfully");
                BindGrid();
                pnleditfertilizer.Visible = false;
            }
        }
    }
    //protected void BindChkdSpecs()
    //{
    //    dt = new DataTable();
    //    objR.Action = "SPEC";
    //    objR.SRegid = lblsmpleid.Text.Trim();
    //    dt = Regobjdl.FertlizerRegistration(objR, con);
    //    GvSpec.DataSource = dt;
    //    GvSpec.DataBind();
    //}
    protected void BindData()
    {
        try
        {
            objR.Action = "R";
            objR.DI_AO_Code = Aocode;
            objR.SRegid = lblFertilizerno.Text;
            objR.dept = Department;
            objR.SampleCategory = Session["SampleCategory"].ToString();
            DataTable smplData = new DataTable();
            smplData = Regobjdl.FertlizerRegistration(objR, con);
            if (smplData.Rows.Count > 0)
            {
                BindClass();
                ddlClass.SelectedValue = smplData.Rows[0]["Sample_Class"].ToString();
                txtcodesample.Text = smplData.Rows[0]["Code_Sticker_No"].ToString();
                txtfirnmdlr.Text = smplData.Rows[0]["Firm_Name"].ToString();
                txtnmowner.Text = smplData.Rows[0]["FOwner_Name"].ToString();
                txtdllcno.Text = smplData.Rows[0]["LicenceNo"].ToString();
                txtvldty.Text = smplData.Rows[0]["Validity"].ToString();
                txtbthchno.Text = smplData.Rows[0]["BatchNo"].ToString();
                txtdtmn.Text = smplData.Rows[0]["ManufacturingDate"].ToString();
                txtstockl.Text = smplData.Rows[0]["StockPosition"].ToString();
                BindState();
                ddlstate.SelectedValue = smplData.Rows[0]["State"].ToString();
                BindDistrict();
                ddldist.SelectedValue = smplData.Rows[0]["District"].ToString();
                txthsno.Text = smplData.Rows[0]["HouseNo"].ToString();
                txtlcstr.Text = smplData.Rows[0]["Locality"].ToString();
                txtdmipagency.Text = smplData.Rows[0]["StkRcvdDate"].ToString();
                txtdtsam.Text = smplData.Rows[0]["SampleCollectingDate"].ToString();
                BindSampeltype();
                ddlsampleType.SelectedValue = smplData.Rows[0]["SampleType_ID"].ToString();
                BindSampel();
                ddlsample.SelectedValue = smplData.Rows[0]["Sample_ID"].ToString();

                //BindChkdSpecs();
                BindPhysicalCndtn();
                ddlPhysicalCon.SelectedValue = smplData.Rows[0]["PhysicalCondition"].ToString();
                BindState();
                ddlmnstate.SelectedValue = smplData.Rows[0]["ManufacturerState"].ToString();
                txtaddrs.Text = smplData.Rows[0]["Address"].ToString();
                txtmnname.Text = smplData.Rows[0]["ManfucaturerName"].ToString();

                if (smplData.Rows[0]["Drawn_Bag"].ToString() == "OB")
                    RblSmpleDrawnFrom.SelectedValue = "OB";
                else
                    RblSmpleDrawnFrom.SelectedValue = "SB";
                
                if (smplData.Rows[0]["punchnama"].ToString() == "1")
                    Rblphnma.SelectedValue = "1";
                else
                    Rblphnma.SelectedValue = "0";
            }
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            //Response.Redirect("~/Error.aspx");
        }

    }

    protected void GvSpec_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((Label)e.Row.FindControl("lblspecid")).Text != "")
                ((CheckBox)e.Row.FindControl("chkSelct")).Checked = true;
        }
    }


    protected void btnupdate_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (Validatesave())
            {
                objR.SampleClass = ddlClass.SelectedValue;
                objR.CodeSticker = txtcodesample.Text;
                objR.Firm_Name = txtfirnmdlr.Text;
                objR.ContactPerson = txtnmowner.Text;
                objR.LicenceNo = txtdllcno.Text;
                objR.Validity = cf.Texttodateconverter(txtvldty.Text);
                objR.BatchNo = txtbthchno.Text;
                objR.ManufacturingDate = cf.Texttodateconverter(txtdtmn.Text);
                objR.StockPosition = txtstockl.Text;

                objR.state = ddlstate.SelectedValue;

                objR.District = ddldist.SelectedValue;
                objR.HouseNo = txthsno.Text;
                objR.Locality = txtlcstr.Text;
                objR.StkRcvdDate = cf.Texttodateconverter(txtdmipagency.Text);
                objR.SmplCollectingDt = cf.Texttodateconverter(txtdtsam.Text);

                objR.SampleType = ddlsampleType.SelectedValue;

                objR.SampleID = ddlsample.SelectedValue;
                objR.Address = txtaddrs.Text;

                objR.ManfucaturerName = txtmnname.Text;
                objR.SmplDrawnBag = RblSmpleDrawnFrom.SelectedValue;
                objR.punchnama = Rblphnma.SelectedValue;
                objR.PhycicalCondition = ddlPhysicalCon.SelectedValue;
                objR.dept = Department;
                objR.Action = "U";
                objR.SRegid =lblFertilizerno.Text.Trim();
                objR.ManufacturerState = ddlmnstate.SelectedValue;
               
                //DataTable dtspecfic = new DataTable();
                //dtspecfic.Columns.Add("Parameter_ID", typeof(Int64));

                //int j = 0;
                //foreach (GridViewRow gr in GvSpec.Rows)
                //{
                //    if (((CheckBox)gr.FindControl("chkSelct")).Checked == true)
                //    {
                //        dtspecfic.Rows.Add();
                //        dtspecfic.Rows[j]["Parameter_ID"] = Convert.ToDecimal(((Label)gr.FindControl("lblSpecCode")).Text);
                //        j++;
                //    }
                //}
                //if (j == 0)
                //    cf.ShowAlertMessage("Select atleast one row ");
                //else
                //{
                //    objR.TVP = dtspecfic;
                    objR.SampleCategory = Session["SampleCategory"].ToString();
                    dt = Regobjdl.FertlizerRegistration(objR, con);

                    if (dt.Rows.Count > 0)
                        cf.ShowAlertMessage("Update failed, please try again");
                    else
                        cf.ShowAlertMessage("Your data has been successfully Updated");
                    Clear();
                    pnleditfertilizer.Visible = false;
                    BindGrid();
               // }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
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

            if (!objValidate.IsDate(txtdtmn.Text.Trim()))
            {
                cf.ShowAlertMessage("Enter Date of Manufacturing");
                txtdtmn.Focus();
                return false;
            }
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
        if (txtdmipagency.Text == "")
        {
            cf.ShowAlertMessage("Date of Receipt of the stock by the dealer/manufacture/importer/pool handling agency");
            txtdmipagency.Focus();
            return false;
        }

        if (txtdmipagency.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtdmipagency.Text);
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
        if (ddlsampleType.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Sample type");
            ddlsampleType.Focus();
            return false;
        }
        if (ddlsample.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Sample Name");
            ddlsample.Focus();
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
        if (ddlmnstate.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Manufacturer State");
            ddlmnstate.Focus();
            return false;
        }
        return true;
    }
    protected void Clear()
    {
        txtaddrs.Text = "";
        txtbthchno.Text = "";
        txtcodesample.Text = "";
        txtdllcno.Text = "";
        txtdmipagency.Text = "";
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
        ddlsampleType.ClearSelection();
        ddlstate.ClearSelection();
        Rblphnma.ClearSelection();
        RblSmpleDrawnFrom.ClearSelection();


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