using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;


public partial class Agri_Admin_Freeze_TargetAllotment : System.Web.UI.Page
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
           
            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    //BindGrid();
                    BindCategory();
                    ObjCommon.BindFinancialYears(ddlYear);
                    gridid.Visible = false;
                    FrrezedGridDiv.Visible = false;
                   
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
        dt = new DataTable();
        objBE.Id = Session["Department"].ToString();
        objBE.Action = "CAT";
        dt = ObjDL.Getdetails(objBE, con);
        ObjCommon.BindDropDownLists(ddlCategory, dt, "category_name", "category_id", "Select");
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
            objBE = new Master_BE();
            objBE.ActionBy = "ADMIN";
            objBE.Action = "R";
            objBE.Year = ddlYear.SelectedValue.Trim();
            objBE.CatId = ddlCategory.SelectedValue.Trim();
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
            //ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            //Response.Redirect("~/Error.aspx");
        }
    }
    protected void FreezedDataGrid()
    {
        dt = new DataTable();
        try
        {
            objBE = new Master_BE();
            objBE.ActionBy = "ADMIN";
            objBE.Action = "F";
            objBE.Year = ddlYear.SelectedValue.Trim();
            objBE.CatId = ddlCategory.SelectedValue.Trim();
            dt = ObjDL.TargetAllotment_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GVFreezed.DataSource = dt;
                GVFreezed.DataBind();
                FrrezedGridDiv.Visible = true;
                //btnGetTotal.Visible = true;
            }
            else
            {
                GVFreezed.DataSource = null;
                GVFreezed.DataBind();
            }
        }
        catch (Exception ex)
        {
            //ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            //Response.Redirect("~/Error.aspx");
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void FreezeAllotments(object sender, EventArgs e)
    {
        try
        {
            DataTable dtfreeze = new DataTable();
            dtfreeze.Columns.Add("allot_id", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in GVDistAllot.Rows)
            {
                if (((CheckBox)gr.FindControl("chkSelct")).Checked == true)
                {
                    dtfreeze.Rows.Add();
                    dtfreeze.Rows[j]["allot_id"] = ((Label)gr.FindControl("lblAllotedId")).Text;
                    j++;
                }
            }
            if (j == 0)
                ObjCommon.ShowAlertMessage("Select atleast one row to freeze");
            else
            {

                objBE.TVP = dtfreeze;
                objBE.Action = "ADMIN";
                dt = ObjDL.FreezeTargetDetails(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage("Not freezed, try again");
                }
                else
                {
                    ObjCommon.ShowAlertMessage("data freezed");
                    GVDistAllot.DataSource = null;
                    GVDistAllot.DataBind();
                    BindGrid();
                    FreezedDataGrid();
                }
            }
        }
        catch (Exception ex)
        {
           // ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
          // Response.Redirect("../Error.aspx");

        }
    }
}