using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;


public partial class Agri_Admin_EquipmentMaster : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs ObjCommon = new CommonFuncs();
    DataTable dt;
    string con, user, state, Department;
    SampleSqlInjectionScreeningModule obj = new SampleSqlInjectionScreeningModule();
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
        if (Session["UserId"] != null || Session["RoleID"].ToString() == "7")
        {

            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindGrid();
                    BindCategory();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    ObjCommon.ShowAlertMessage(ex.ToString());
                }
            }
        }
        else
        {
            Response.Redirect("../Error.aspx");
        }
    }
    protected void BindCategory()
    {
        objBE.Id = Department;
        objBE.Action = "CAT";
        dt = ObjDL.Getdetails(objBE, con);
        ObjCommon.BindDropDownLists(ddlSampleCat, dt, "category_name", "category_id", "0");
    }
    protected void BindSampleType()
    {
        dt = new DataTable();
        objBE.Dept = Session["Department"].ToString();
        objBE.CatId = ddlSampleCat.SelectedValue;
        objBE.Action = "R";
        dt = ObjDL.SampleTypeIUDR(objBE, con);
        ObjCommon.BindDropDownLists(ddlSampType, dt, "SampleTypeName", "SampleTypeCode", "Select");
    }
    protected void BindGrid()
    {
        try
        {
            objBE.Action = "R";
            objBE.statecode = state;
            objBE.Dept = Department;
            dt = ObjDL.Equipment_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvLabs.DataSource = dt;
                GvLabs.DataBind();
            }
            else
            {
                GvLabs.DataSource = null;
                GvLabs.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());

        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            check();
            if (validate())
            {
                objBE.statecode = state;
                objBE.Dept = Department;
                objBE.EquipName = txtEquipName.Text;
                objBE.SampleTypeId = ddlSampType.Text;
                objBE.CatId = ddlSampleCat.SelectedValue;
                objBE.User = user;
                objBE.Action = "I";
                dt = ObjDL.Equipment_IUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage(dt.Rows[0][0].ToString());
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Saved Successfully");
                }
                BindGrid();
                txtEquipName.Text = "";
               
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            check();
            if (validate())
            {
                objBE.statecode = state;
                objBE.Dept = Department;
                objBE.EquipCode = lblEquipCode.Text;
                objBE.EquipName = txtEquipName.Text;
                objBE.SampleTypeId = ddlSampType.Text;
                objBE.CatId = ddlSampleCat.SelectedValue;
                objBE.Action = "U";
                dt = ObjDL.Equipment_IUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    txtEquipName.Text = "";                  
                    ObjCommon.ShowAlertMessage("Not Updated");
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Equipment Details Updated Successfully");
                    btnUpdate.Visible = false;
                    BtnSave.Visible = true;
                    BindGrid();
                    txtEquipName.Text = "";
     
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());

        }
    }

    protected void GvLabs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblEquipCode.Text = ((Label)(gvrow.FindControl("lblEquipCode"))).Text;
                txtEquipName.Text = ((Label)(gvrow.FindControl("lblEquipName"))).Text;
                BindCategory();
                ddlSampleCat.SelectedValue = ((Label)(gvrow.FindControl("lblCatCode"))).Text;
                BindSampleType();
                ddlSampType.SelectedValue = ((Label)(gvrow.FindControl("lblSampleTypeCode"))).Text;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());

        }

        try
        {
            if (e.CommandName == "dlt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objBE.statecode = state;
                objBE.Dept = Department;
                objBE.EquipCode = ((Label)(gvrow.FindControl("lblEquipCode"))).Text;
                objBE.Action = "D";
                dt = ObjDL.Equipment_IUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    txtEquipName.Text = "";
                 
                }
                else
                {
                    txtEquipName.Text = "";
                    ObjCommon.ShowAlertMessage("Deleted Successfully");
                }
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());

        }
    }
    public bool validate()
    {
        if (txtEquipName.Text == "")
        {
            ObjCommon.ShowAlertMessage("Please Enter Equipment Name");
            txtEquipName.Focus();
            return false;

        }
        if (ddlSampleCat.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage("Please Select Category");
            ddlSampleCat.Focus();
            return false;
        }
        if (ddlSampType.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage("Please Select Sample Type");
            ddlSampType.Focus();
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
    protected void ddlSampleCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSampleType();
        objBE.CatId = ddlSampleCat.SelectedValue;
        BindGrid();
    }
    protected void ddlSampType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objBE.SampleTypeId = ddlSampType.SelectedValue;
        objBE.CatId = ddlSampleCat.SelectedValue;
        BindGrid();
    }
}