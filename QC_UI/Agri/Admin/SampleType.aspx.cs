using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Admin_SampleType : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs ObjCommon = new CommonFuncs();
    DataTable dt;
    string con, user, state;
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
                    BindGrid();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    lblmsg.Text = ex.Message.ToString();
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
        BindGrid();
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (validate())
            {

                dt = new DataTable();
                objBE.Dept = Session["Department"].ToString();
                objBE.CatId = ddlCategory.SelectedValue;
                objBE.SampleTypeName = txttype.Text.Trim();
                objBE.Action = "I";
                dt = ObjDL.SampleTypeIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage(dt.Rows[0][0].ToString());
                    txttype.Text = "";

                    BindGrid();
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Sampele Type Saved");
                    txttype.Text = "";
                    BindGrid();
                }
            }
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            lblmsg.Text = ex.Message.ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (validate())
            {
                dt = new DataTable();
                objBE.Dept = Session["Department"].ToString();
                objBE.SampleTypeId = Session["SampleCode"].ToString();
                // objBE.CatId = ddlCategory.SelectedValue;
                objBE.SampleTypeName = txttype.Text.Trim();
                objBE.Action = "U";
                dt = ObjDL.SampleTypeIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage("Sample Updated Failed ");
                    BindGrid();
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Sample Updated Successfully");
                    btnUpdate.Visible = false;
                    BtnSave.Visible = true;
                    BindGrid();
                    txttype.Text = "";
                    ddlCategory.Enabled = true;

                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void BindGrid()
    {
        check();
        dt = new DataTable();
        objBE.Dept = Session["Department"].ToString();
        objBE.CatId = ddlCategory.SelectedValue;
        objBE.SampleTypeId = null;
        objBE.Action = "R";
        dt = ObjDL.SampleTypeIUDR(objBE, con);
        if (dt.Rows.Count > 0)
        {
            GVType.DataSource = dt;
            GVType.DataBind();
        }
        else
        {
            GVType.DataSource = null;
            GVType.DataBind();
        }
    }
    protected void GVType_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        check();
        try
        {
            if (e.CommandName == "edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                txttype.Text = ((Label)(gvrow.FindControl("lblName"))).Text;
                BindCategory();
                ddlCategory.SelectedValue = ((Label)(gvrow.FindControl("lblcatid"))).Text;
                Session["SampleCode"] = ((Label)(gvrow.FindControl("lblSampTypeCode"))).Text;
                ddlCategory.Enabled = false;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                BindGrid();

            }
            if (e.CommandName == "dlt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objBE.Dept = Session["Department"].ToString();
                objBE.SampleTypeId = ((Label)(gvrow.FindControl("lblSampTypeCode"))).Text;
                objBE.Action = "D";

                dt = ObjDL.SampleTypeIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage("Sampele Type Failed,Pls Try Again");
                    txttype.Text = "";

                    BindGrid();
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Your data has been Delete successfully");
                    txttype.Text = "";
                    btnUpdate.Visible =false;
                    //ddlCategory.ClearSelection();
                    BindGrid();
                }

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            lblmsg.Text = ex.Message.ToString();
        }
    }
    public bool validate()
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage("Please Select Category");
            ddlCategory.Focus();
            return false;
        }

        if (txttype.Text == "")
        {
            ObjCommon.ShowAlertMessage("Please Enter SampleType");
            txttype.Focus();
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