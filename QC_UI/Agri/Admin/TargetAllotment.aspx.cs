using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Agri_Admin_TargetAllotment : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs ObjCommon = new CommonFuncs();
    DataTable dt;
    string con, user, state, Department, username;
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
            username = Session["UsrName"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    //BindGrid();
                    BindCategory();
                    lbltotal.Visible = false;
                    lbltotquantityAlloted.Visible = false;
                    gridid.Visible = false;
                    ObjCommon.BindFinancialYears(ddlYear);
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    Response.Redirect("../Error.aspx");
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
        dt = new DataTable();
        objBE.Id = Session["Department"].ToString();
        objBE.Action = "CAT";
        dt = ObjDL.Getdetails(objBE, con);
        ObjCommon.BindDropDownLists(ddlCategory, dt, "category_name", "category_id", "Select");
    }   
   
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (validate())
            {
                dt = new DataTable();
                objBE.Year = ddlYear.SelectedValue.Trim();
                objBE.CatId = ddlCategory.SelectedValue.Trim();
                objBE.ActionBy = "ADMIN";
                objBE.Action = "DIST";
                dt = ObjDL.TargetAllotment_IUDR(objBE, con);
                GVDistAllot.DataSource = dt;
                GVDistAllot.DataBind();
                btnGetTotal.Visible = true;
                lbltotal.Visible = true;
                BtnSave.Visible = true;
                gridid.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
  
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            decimal tot = 0;
            foreach (GridViewRow gvr in GVDistAllot.Rows)
            {
                decimal q = Convert.ToDecimal(((TextBox)gvr.FindControl("txtqty")).Text.Trim());
                tot += q;
            }
            lbltotal.Text = tot.ToString();
            btnGetTotal.Visible = false;
            BtnSave.Visible = true;
            lbltotal.Visible = true;
            lbltotquantityAlloted.Visible = true;
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
            if (validate())
            {
                DataTable dtAllot = new DataTable();
                dtAllot.Columns.Add("district", typeof(string));
                dtAllot.Columns.Add("allotedQty", typeof(string));
                int j = 0;
                foreach (GridViewRow gr in GVDistAllot.Rows)
                {
                    if (((TextBox)gr.FindControl("txtqty")).Text != "0")
                    {
                        bool val;

                        val = obj.CheckInput_new(((TextBox)gr.FindControl("txtqty")).Text);
                        if (val == true)
                            Response.Redirect("~/Error.aspx");
                        else
                        {
                            dtAllot.Rows.Add();
                            dtAllot.Rows[j]["district"] = ((Label)gr.FindControl("lbldistcode")).Text;
                            dtAllot.Rows[j]["allotedQty"] = ((TextBox)gr.FindControl("txtqty")).Text;
                            j++;
                        }
                    }
                }
                dt = new DataTable();
                objBE.TVP= dtAllot;
                objBE.Year = ddlYear.SelectedValue.Trim();
                objBE.CatId = ddlCategory.SelectedValue.Trim();
                objBE.ActionBy = "ADMIN";
                objBE.User = user;
                objBE.Action = "I";
                dt = ObjDL.TargetAllotment_IUDR(objBE,con);
               
                if (dt.Rows.Count > 0)
                    ObjCommon.ShowAlertMessage("Not Saved , Try Again");
                else
                    ObjCommon.ShowAlertMessage(" Saved. Click on Update Delete to make any change");
                BtnSave.Visible = false;
                btnGetTotal.Visible = false;
            }
        }
        catch (Exception ex)
        {
           ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
           Response.Redirect("~/Error.aspx");
        }
    }

    public bool validate()
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage("Please Select Year");
            ddlYear.Focus();
            return false;
        }
        if (ddlCategory.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage("Please Select Category");
            ddlCategory.Focus();
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
}