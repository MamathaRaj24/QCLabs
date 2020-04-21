using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;


public partial class DCA_Admin_Specifications : System.Web.UI.Page
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
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "0" || Session["RoleID"].ToString() == "7"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();


            if (!IsPostBack)
            {
                random();
                try
                {
                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindCategory();
                    // CreateRow();
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
        ObjCommon.BindDropDownLists(ddlSamCat, dt, "category_name", "category_id", "Select");
    }

    protected void ddlSamCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSamCat.SelectedIndex != 0)
        {
             CreateRow();
            BindGrid();
        }
    }

    protected void CreateRow()
    {
        DataTable dtparam = new DataTable();
        dtparam.Columns.Add("Parameter", typeof(string));
        dtparam.Rows.Add();
        GVTestParam.DataSource = dtparam;
        GVTestParam.DataBind();
        BtnSave.Visible = true;
    }
    protected void GVTestParam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Remove")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("Parameter", typeof(string));
                int j = 0;
                foreach (GridViewRow gr in GVTestParam.Rows)
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["Parameter"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    j++;
                }
                dtparam.Rows.RemoveAt(gvrow.RowIndex);
                if (dtparam.Rows.Count == 0)
                {
                    dtparam.Rows.Add();
                }
                GVTestParam.DataSource = dtparam;
                GVTestParam.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }

    protected void imgBtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("Parameter", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in GVTestParam.Rows)
            {
                string param = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                if (param != "") 
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["Parameter"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    j++;
                }
            }
            dtparam.Rows.Add();
            GVTestParam.DataSource = dtparam;
            GVTestParam.DataBind();
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
        try
        {
            dt = new DataTable();
            objBE.CatId = ddlSamCat.SelectedValue;
            objBE.Action = "R";
            dt = ObjDL.TestParameterIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvTest.DataSource = dt;
                GvTest.DataBind();
                GvTest.Visible = true;
            }
            else
            {
                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("Parameter", typeof(string));
                dtparam.Rows.Add();
                GVTestParam.DataSource = dtparam;
                GVTestParam.DataBind();
                GvTest.Visible = false;

            }
            BtnSave.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("param", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in GVTestParam.Rows)
            {
                string param = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                if (param != "")// && stv != "")
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["param"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    j++;
                }
            }
            dt = new DataTable();
            objBE.CatId = ddlSamCat.SelectedValue;
            objBE.User = Session["UsrName"].ToString();
            objBE.Action = "I";
            objBE.TVP = dtparam;
            dt = ObjDL.TestParameterIUDR(objBE, con);
            if (dt.Rows.Count > 0)
                ObjCommon.ShowAlertMessage("Data Not Saved");
            else
            {
                ObjCommon.ShowAlertMessage("Data Saved");
                CreateRow();
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
   
    protected void GvTest_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        GvTest.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void GvTest_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label ParaId = (Label)GvTest.Rows[e.RowIndex].FindControl("lblparamid");
        objBE.ParamID = ParaId.Text;
        objBE.Action = "D";
        DataTable dt = new DataTable();
        dt = ObjDL.TestParameterIUDR(objBE, con);
        if (dt.Rows.Count > 0)
        {
            ObjCommon.ShowAlertMessage("Data Not Deleted");
        }
        else
        {
            GvTest.DataSource = dt;
            GvTest.DataBind();
            BindGrid();
        }
    }
    protected void GvTest_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label ParaId = (Label)GvTest.Rows[e.RowIndex].FindControl("lblparamids");
        TextBox paraName = (TextBox)GvTest.Rows[e.RowIndex].FindControl("txtParam");
        objBE.ParamID = ParaId.Text;
        objBE.ParamName = paraName.Text;
        objBE.Action = "U";
        DataTable dt = new DataTable();
        dt = ObjDL.TestParameterIUDR(objBE, con);
        if (dt.Rows.Count > 0)
        {
            ObjCommon.ShowAlertMessage("Data Not Updated");
                

        }
        else
        {
            GvTest.DataSource = dt;
            GvTest.DataBind();
            GvTest.EditIndex = -1;
            BindGrid();

        }
    }
    protected void GvTest_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GvTest.EditIndex = -1;
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