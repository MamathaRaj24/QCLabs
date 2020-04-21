using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mime;
using System.IO;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Agri_Fertilizer_CCOAO_ViewSample : System.Web.UI.Page
{
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
   // AgriDL objRegdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
   // AgriBE objRegbe = new AgriBE();
    AgriBE objRegbe = new AgriBE();
    AgriDL objRegdl = new AgriDL();
    DataTable dt;
    string con, user, state, Department, dicode;

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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "14")
        {
            state = Session["StateCode"].ToString();
            user = Session["CCOAO"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();


            if (!IsPostBack)
            {
                random();
                try
                {
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["UsrName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindGrid();
                    Bindreject();

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
    protected void BindGrid()
    {
        objRegbe.Action = "VIEW";
        objRegbe.dept = Department;
        dt = objRegdl.CourierIUDR(objRegbe, con);
        if (dt.Rows.Count > 0)
        {
            gvviewsample.DataSource = dt;
            gvviewsample.DataBind();
        }
        else
        {
            gvviewsample.DataSource = null;
            gvviewsample.DataBind();
            cf.ShowAlertMessage("No Data");
        }

    }
    protected void Bindreject()
    {
        dt = new DataTable();
        objMBE.Dept = Department;
        objMBE.Action = "R";
        dt = ObjDL.RejectedReasons(objMBE, con);
        cf.BindDropDownLists(ddlrejctreson, dt, "Reject_Reason", "ID", "Select");
    }
    protected void gvviewsample_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        check();
        if (e.CommandName == "VIEW")
        {
            try
            {
                DataTable dt = new DataTable();
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objRegbe.MemoId = ((Label)gvrow.FindControl("lblsampleid")).Text;

                objRegbe.Action = "VIEW1";
                lblsampleid.Text = objRegbe.MemoId;
                dt = objRegdl.CourierIUDR(objRegbe, con);
                if (dt.Rows.Count > 0)
                {

                    pnltestresult.Visible = true;
                    lblSamcls.Text = dt.Rows[0]["class_name"].ToString();
                    lblcodenoofsample.Text = dt.Rows[0]["Code_Sticker_No"].ToString();
                    lbllcnsno.Text = dt.Rows[0]["LicenceNo"].ToString();
                    lblnameofthfirm.Text = dt.Rows[0]["Firm_Name"].ToString();
                    lblnameoftheowner.Text = dt.Rows[0]["FOwner_Name"].ToString();
                    lblvalidity.Text = dt.Rows[0]["Validity"].ToString();
                    lblbatchno.Text = dt.Rows[0]["BatchNo"].ToString();
                    lblstckpositionofthelot.Text = dt.Rows[0]["StockPosition"].ToString();
                    lblstatename.Text = dt.Rows[0]["State"].ToString();
                    lbldistname.Text = dt.Rows[0]["DistName"].ToString();
                    lblhsflatno.Text = dt.Rows[0]["HouseNo"].ToString();
                    lbllocality.Text = dt.Rows[0]["Locality"].ToString();
                    lbldatercpt.Text = dt.Rows[0]["StkRcvdDate"].ToString();
                    lblSamColDate.Text = dt.Rows[0]["SampleCollectingDate"].ToString();
                    lblsampletype.Text = dt.Rows[0]["SampleTypeName"].ToString();
                    lblsamplegradename.Text = dt.Rows[0]["SampleName"].ToString();
                    lblphysicalcondition.Text = dt.Rows[0]["Physical_ConditionName"].ToString();
                    lblmanfdate.Text = dt.Rows[0]["ManufacturingDate"].ToString();
                    lblManName.Text = dt.Rows[0]["ManfucaturerName"].ToString();
                    lblmanustate.Text = dt.Rows[0]["ManufacturerState"].ToString();
                    lblmanuAddress.Text = dt.Rows[0]["Address"].ToString();
                    lblsampledrwanfrom.Text = dt.Rows[0]["Drawn_Bag"].ToString();
                    lblpanchanama.Text = dt.Rows[0]["punchnama"].ToString();
                    lblmemoid.Text = dt.Rows[0]["Memo_ID"].ToString();
                    rdbaccept.ClearSelection();
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                cf.ShowAlertMessage(ex.ToString());
            }
        }
    }


    //protected void rdbaccept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rdbaccept.SelectedValue == "A")
    //    {

    //        btnSave.Visible = true;

    //        lblresons.Visible = false;
    //        ddlrejctreson.Visible = false;
    //        btnSave.Text = "Save";
    //    }
    //    if (rdbaccept.SelectedValue == "R")
    //    {
    //        lblresons.Visible = true;
    //        ddlrejctreson.Visible = true;
    //        Bindreject();

    //        btnSave.Visible = true;
    //        ddlrejctreson.Focus();
    //        btnSave.Text = "Rejected";
    //    }

    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        //if (rdbaccept.SelectedValue == "R")
        //{
        //    if (ddlrejctreson.SelectedIndex ==0)
        //    {
        //        cf.ShowAlertMessage("Pls select Rejected Reasons");
        //        ddlrejctreson.Focus();
        //        return;
        //    }
        //    objRegbe.Ref = ddlrejctreson.SelectedValue;
        //    objRegbe.status = rdbaccept.SelectedValue;
        //    objRegbe.Action = "Rjct";
        //}
        if (rdbaccept.SelectedValue == "A")
        {
            objRegbe.status = rdbaccept.SelectedValue;
            objRegbe.Action = "ACCEPT";
        }
        objRegbe.SampleID = lblsampleid.Text;
        objRegbe.MemoId = lblmemoid.Text;
         dt = objRegdl.GenerateSticker_AGRI(objRegbe, con);
        if (dt.Rows.Count > 0)
        {
            cf.ShowAlertMessage(dt.Rows[0][0].ToString());
        }
        else
        {
            cf.ShowAlertMessage("Already exists");
        }
        pnltestresult.Visible = false;
        BindGrid();


    }

    protected void BtnReject_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        if (rdbaccept.SelectedValue == "R")
        {
            if (ddlrejctreson.SelectedIndex == 0)
            {
                cf.ShowAlertMessage("Pls select Rejected Reasons");
                ddlrejctreson.Focus();
                return;
            }
            objRegbe.Ref = ddlrejctreson.SelectedValue;
            objRegbe.status = rdbaccept.SelectedValue;
            objRegbe.Action = "Rjct";
        }
        objRegbe.SampleID = lblsampleid.Text;
        objRegbe.MemoId = lblmemoid.Text;
        dt = objRegdl.GenerateSticker_AGRI(objRegbe, con);
        if (dt.Rows.Count > 0)
        {
            cf.ShowAlertMessage(dt.Rows[0][0].ToString());
        }
        else
        {
            cf.ShowAlertMessage("Already exists");
        }

        pnltestresult.Visible = false;
        BindGrid();

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