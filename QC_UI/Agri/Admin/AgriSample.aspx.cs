using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;
public partial class AgriAdmin_AgriSample : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs ObjCommon = new CommonFuncs();
    Validate objValidate = new Validate();
    DataTable dt;
    string con, user, state, Department;
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
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "7"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindCategory();
                    BindGrid();
                    bindSampleTypes();
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
    protected void BindCategory()
    {
        dt = new DataTable();
        objBE.Dept = Session["Department"].ToString();
        objBE.Action = "R";
        dt = ObjDL.CategoryIUDR(objBE, con);
        ObjCommon.BindDropDownLists(ddlCategory, dt, "category_name", "category_id", "Select");
        ddlCategory.SelectedValue = "30";
        ddlCategory.Enabled = false;
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSampleTypes();
    }
    protected void bindSampleTypes()
    {
        try
        {
            dt = new DataTable();
            objBE.Dept = Session["Department"].ToString();
            objBE.CatId = ddlCategory.SelectedValue;
            objBE.SampleTypeId = null;
            objBE.Action = "R";
            dt = ObjDL.SampleTypeIUDR(objBE, con);
            ObjCommon.BindDropDownLists(ddlSamtype, dt, "SampleTypeName", "SampleTypeCode", "0");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BindGrid()
    {
        check();
        objBE.Action = "RAGRI";
        objBE.Dept = Session["Department"].ToString();
        objBE.SampleTypeId = ddlSamtype.SelectedValue;
        dt = ObjDL.SampleIUDR(objBE, con);
        if (dt.Rows.Count > 0)
        {
            GVSamples.DataSource = dt;
            GVSamples.DataBind();
        }
        else
        {
            GVSamples.DataSource = null;
            GVSamples.DataBind();
        }
    }

    protected void ddlSamtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (validate())
            {
                dt = new DataTable();
                objBE.Action = "I";
                objBE.SampleName = txtSampName.Text;
                objBE.SampleTypeId = ddlSamtype.SelectedValue;
                objBE.User = user;
                dt = ObjDL.SampleIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {

                    ObjCommon.ShowAlertMessage(dt.Rows[0][0].ToString());
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Sample Saved");
                }

                txtSampName.Text = "";
                ddlSamtype.ClearSelection();
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            dt = new DataTable();
            objBE.Action = "U";
            objBE.SampleID = Session["SampleCode"].ToString();
            objBE.SampleName = txtSampName.Text;
            objBE.SampleTypeId = ddlSamtype.SelectedValue;
            dt = ObjDL.SampleIUDR(objBE, con);
            if (dt.Rows.Count > 0)
                ObjCommon.ShowAlertMessage("Sample Not Updated.");
            else
            {
                ObjCommon.ShowAlertMessage("Sample Updated Successfully");
                btnUpdate.Visible = false;
                BtnSave.Visible = true;
            }
            BindGrid();
            txtSampName.Text = "";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void GVSamples_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        check();
        if (e.CommandName == "edt")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            ddlCategory.SelectedValue = ((Label)(gvrow.FindControl("lblCategoryId"))).Text;
            ddlSamtype.SelectedValue = ((Label)(gvrow.FindControl("lblSampTypeCode"))).Text;
            ddlCategory.Enabled = false;
            bindSampleTypes();
            Session["SampleCode"] = ((Label)(gvrow.FindControl("lblCode"))).Text;
            txtSampName.Text = ((Label)(gvrow.FindControl("lblName"))).Text;
            btnUpdate.Visible = true;
            ddlSamtype.Enabled = false;
            BtnSave.Visible = false;
            BindGrid();
        }
        if (e.CommandName == "dlt")
        {
            GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            objBE.SampleID = ((Label)(gvrow.FindControl("lblCode"))).Text;
            objBE.Action = "D";
            dt = ObjDL.SampleIUDR(objBE, con);
            if (dt.Rows.Count > 0)

            ObjCommon.ShowAlertMessage("Sample Deleted Successfully");
            btnUpdate.Visible = false;
            BtnSave.Visible = true;

            txtSampName.Text = "";
            ddlSamtype.ClearSelection();
            BindGrid();
        }
    }
    public bool validate()
    {

        if (ddlSamtype.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage("Please Select SampleType");
            ddlSamtype.Focus();
            return false;
        }
        if (txtSampName.Text == "")
        {
            ObjCommon.ShowAlertMessage("Please Enter SampleName");
            txtSampName.Focus();
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
            //cookie_value = System.Web.HttpContext.Current.Request.Cookies["ASPFIXATION2"].Value;
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
