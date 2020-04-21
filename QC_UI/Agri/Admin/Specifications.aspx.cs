using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;


public partial class Admin_Specifications : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs ObjCommon = new CommonFuncs();
    Validate objValidate = new Validate();
    DataTable dt;
    string con, user, state, Department;

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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "7")
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
                    BindSampleType();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    ObjCommon.ShowAlertMessage(ex.ToString());
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
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindSampleType()
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
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
    protected void ddlSamtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objBE.Action = "R";
            objBE.Dept = Session["Department"].ToString();
            objBE.SampleTypeId = ddlSamtype.SelectedValue;
            dt = ObjDL.SampleIUDR(objBE, con);
            ObjCommon.BindDropDownLists(ddlsample, dt, "SampleName", "SampleCode", "0");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
    protected void ddlsample_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsample.SelectedIndex != 0)
        {
            CreateRow();
            //BindGrid();
            BindGrid();
        }
    }
    
    protected void BindGrid()
    {
        objBE.SampleID = ddlsample.SelectedValue;
        objBE.Action = "R";
        dt = ObjDL.Specifications_IUDR(objBE, con);
        if (dt.Rows.Count > 0)
        {
            GvSpec.DataSource = dt;
            GvSpec.DataBind();
        }
        else
        {
            GvSpec.DataSource = null;
            GvSpec.DataBind();
            ObjCommon.ShowAlertMessage("No Data");
        }
    }
    protected void CreateRow()
    {
        DataTable dtparam = new DataTable();
        dtparam.Columns.Add("param", typeof(string));
        dtparam.Columns.Add("stv", typeof(string));
        dtparam.Rows.Add();
        GVSpecifications.DataSource = dtparam;
        GVSpecifications.DataBind();
    }
    protected void GVSpecifications_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Remove")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("param", typeof(string));
                dtparam.Columns.Add("stv", typeof(string));
                int j = 0;
                foreach (GridViewRow gr in GVSpecifications.Rows)
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["param"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    dtparam.Rows[j]["stv"] = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                    j++;
                }
                dtparam.Rows.RemoveAt(gvrow.RowIndex);
                if (dtparam.Rows.Count == 0)
                {
                    dtparam.Rows.Add();
                }
                GVSpecifications.DataSource = dtparam;
                GVSpecifications.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());

        }
    }
    protected void imgBtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("param", typeof(string));
            dtparam.Columns.Add("stv", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in GVSpecifications.Rows)
            {
                string param = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                string stv = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                if (param != "" && stv != "")
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["param"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    dtparam.Rows[j]["stv"] = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                    j++;
                }
            }
            dtparam.Rows.Add();
            GVSpecifications.DataSource = dtparam;
            GVSpecifications.DataBind();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("param", typeof(string));
            dtparam.Columns.Add("stv", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in GVSpecifications.Rows)
            {
                string param = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                string stv = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                if (param != "" )
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["param"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    dtparam.Rows[j]["stv"] = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                    j++;
                }
            }
            dt = new DataTable();
            objBE.SampleID = ddlsample.SelectedValue;
            objBE.User = Session["UsrName"].ToString();
            objBE.Action = "I";
            objBE.TVP = dtparam;
            dt = ObjDL.Specifications_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
                ObjCommon.ShowAlertMessage("Data Not Saved");
            else
                ObjCommon.ShowAlertMessage("Data Saved");
            BindGrid();
            ddlSamtype.ClearSelection();
            ddlsample.ClearSelection();
            ddlCategory.ClearSelection();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
   
    protected void GvSpec_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GvSpec.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void GvSpec_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = GvSpec.Rows[e.RowIndex];
            int sampleid = Convert.ToInt32(GvSpec.DataKeys[e.RowIndex].Values[0]);
            int Parameterid = Convert.ToInt32(GvSpec.DataKeys[e.RowIndex].Values[1]);
            string paramname = (row.FindControl("txtParam") as TextBox).Text;
            string stdvalue = (row.FindControl("txtStdVal") as TextBox).Text;
            objBE.ParamName = paramname.ToString();
            objBE.StdValue = stdvalue.ToString(); ;
            objBE.ParamID = Parameterid.ToString();
            objBE.SampleID = sampleid.ToString();
            objBE.Action = "U";
            dt = ObjDL.Specifications_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                ObjCommon.ShowAlertMessage("Update Succsessfully");
                GvSpec.EditIndex = -1;
            }
            else
                ObjCommon.ShowAlertMessage("Update Failed");
            BindGrid();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
    protected void GvSpec_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow row = GvSpec.Rows[e.RowIndex];
            int sampleid = Convert.ToInt32(GvSpec.DataKeys[e.RowIndex].Values[0]);
            int Parameterid = Convert.ToInt32(GvSpec.DataKeys[e.RowIndex].Values[1]);
            objBE.ParamID = Parameterid.ToString();
            objBE.SampleID = sampleid.ToString();
            objBE.Action = "D";
            dt = ObjDL.Specifications_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
                ObjCommon.ShowAlertMessage("Delete Failed ");
            else
                ObjCommon.ShowAlertMessage("Delete Succsessfully");
            BindGrid();
            GvSpec.EditIndex = -1;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
    protected void GvSpec_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GvSpec.EditIndex = -1;
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