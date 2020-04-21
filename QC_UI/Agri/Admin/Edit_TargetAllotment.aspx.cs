using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Agri_Admin_Edit_TargetAllotment : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs ObjCommon = new CommonFuncs();
    DataTable dt;
    string con, user, state, Department, username;

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
            //Label lbldept = (Label)header.FindControl("lbldept");
            //Label lblfooter = (Label)footer.FindControl("lbldept");
            //Image img = (Image)footer.FindControl("Image1");

            //lbldept.Text = "Department of Agriculture ,";
            //lblfooter.Text = "Department of Agriculture.";
            //img.ImageUrl = "~/Assets/img/doa.png";


            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    //BindGrid();
                    BindCategory();
                    distdiv.Visible = false;
                    alloteddiv.Visible = false;
                    lbltotal.Visible = false;
                    lbltotquantityAlloted.Visible = false;
                    gridid.Visible = false;
                    lblDistCode.Visible = false;
                    ObjCommon.BindFinancialYears(ddlYear);
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
    protected void BindGrid()
    {
        dt = new DataTable();
        try
        {
            objBE.ActionBy = "ADMIN";
            objBE.Action = "R";
            objBE.Year = ddlYear.SelectedValue.Trim();
            objBE.CatId = ddlCategory.SelectedValue.Trim();
            objBE.DistCode = lblDistCode.Text.Trim();
            dt = ObjDL.TargetAllotment_IUDR(objBE, con);              
            if (dt.Rows.Count > 0)
            {
                GVDistAllot.DataSource = dt;
                GVDistAllot.DataBind();
                gridid.Visible = true;
                //btnGetTotal.Visible = true;
            }
            else
            {
                 GVDistAllot.DataSource = null;
                 GVDistAllot.DataBind();
            }    
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    
    protected void GVDistAllot_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                ddlYear.SelectedValue = ((Label)gvrow.FindControl("lblYear")).Text;
                ddlCategory.SelectedValue = ((Label)gvrow.FindControl("lblCatcode")).Text;
                lblDist.Text = ((Label)gvrow.FindControl("lbldist")).Text;
                ddlYear.Enabled = false;
                ddlCategory.Enabled = false;             
                txtquantity.Text = ((Label)gvrow.FindControl("lblqty")).Text;
                lblaid.Text = ((Label)gvrow.FindControl("lblAllotedId")).Text;
                lblDistCode.Text = ((Label)gvrow.FindControl("lbldistcode")).Text;
                distdiv.Visible = true;
                alloteddiv.Visible = true;
                btnUpdate.Visible = true;
                lblDist.Enabled = false;
                btnGetTotal.Visible = false;
                lbltotquantityAlloted.Visible = false;
                lbltotal.Visible = false;
            }
            if (e.CommandName == "Dlt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblaid.Text = ((Label)gvrow.FindControl("lblAllotedId")).Text;
                dt = new DataTable();
                objBE.ActionBy = "ADMIN";
                objBE.Action = "D";
                objBE.AllotId = lblaid.Text;
                dt = ObjDL.TargetAllotment_IUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage("Failed to Delete");
                    BindGrid();
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Deleted. Click on Freeze to Freeze Allotments");
                    BindGrid();
                    btnGetTotal.Visible = false;
                    lbltotquantityAlloted.Visible = false;
                    lbltotal.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
          //  ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            //Response.Redirect("~/Error.aspx");
        }
    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        decimal tot = 0;
        foreach (GridViewRow gvr in GVDistAllot.Rows)
        {
            decimal q = Convert.ToDecimal(((Label)gvr.FindControl("lblqty")).Text.Trim());
            tot += q;
        }
        lbltotal.Text = tot.ToString();
        lbltotal.Visible = true;
        lbltotquantityAlloted.Visible = true;
        gridid.Visible = true;
    }
    protected void BindCategory()
    {
        dt = new DataTable();
        objBE.Id = Session["Department"].ToString();
        objBE.Action = "CAT";
        dt = ObjDL.Getdetails(objBE, con);
        ObjCommon.BindDropDownLists(ddlCategory, dt, "category_name", "category_id", "Select");
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        check();
       
            try
            {
                dt = new DataTable();
                 objBE.Year = ddlYear.SelectedValue.Trim();
                objBE.CatId = ddlCategory.SelectedValue.Trim();
                objBE.DistCode = lblDistCode.Text.Trim();
                objBE.Target=txtquantity.Text;
                objBE.AllotId = lblaid.Text;
                objBE.ActionBy ="ADMIN" ;
                objBE.User = user;
                objBE.Action = "U";
               dt = ObjDL.TargetAllotment_IUDR(objBE,con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage("Failed to Update");
                    BindGrid();
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Updated. Click on Freeze to Freeze Allotments");
                    BindGrid();
                    
                }
                btnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                Response.Redirect("~/Error.aspx");
            }
        
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        BindGrid();
    }
   
}