using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Agri_SeedAdmin_CropVarietyMaster : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs cf = new CommonFuncs();
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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "14")
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
                    BindCrop();
                    ddlcrop.Focus();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    cf.ShowAlertMessage(ex.ToString());
                }
            }
        }
        else
        {
            Response.Redirect("../Error.aspx");
        }
    }
    protected void BindCrop()
    {
        dt = new DataTable();
        objBE.Dept = Session["Department"].ToString();
        objBE.Action = "R";
        dt = ObjDL.CropIUDR(objBE, con);
        cf.BindDropDownLists(ddlcrop, dt, "CropName", "CropCode", "Select");
    }
    protected void BindGrid()
    {
        try
        {
            objBE.Action = "R";

            objBE.Dept = Department;
            dt = ObjDL.CropVarietyIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvCropVariety.DataSource = dt;
                GvCropVariety.DataBind();
            }
            else
            {
                GvCropVariety.DataSource = null;
                GvCropVariety.DataBind();
                cf.ShowAlertMessage("No Data");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (validate())
            {

                dt = new DataTable();
                objBE.Dept = Session["Department"].ToString();
                objBE.Cropid = ddlcrop.SelectedValue;
                objBE.CropVrName = txtcropvariety.Text.Trim();
                objBE.UserID = user;
                objBE.Action = "I";
                dt = ObjDL.CropVarietyIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                    txtcropvariety.Text = "";

                    BindGrid();
                }
                else
                {
                    cf.ShowAlertMessage("CropVariety Saved");
                    txtcropvariety.Text = "";
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
                objBE.CropVrcode = Session["Cropvcode"].ToString();
                objBE.Cropid = ddlcrop.SelectedValue;
                objBE.CropVrName = txtcropvariety.Text.Trim();
                objBE.Action = "U";
                dt = ObjDL.CropVarietyIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage("CropVariety Updated Failed ");
                    BindGrid();
                }
                else
                {
                    cf.ShowAlertMessage("CropVariety Updated Successfully");
                    btnUpdate.Visible = false;
                    BtnSave.Visible = true;
                    BindGrid();
                    txtcropvariety.Text = "";
                    ddlcrop.Enabled = true;
                    ddlcrop.ClearSelection();

                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            lblmsg.Text = ex.Message.ToString();
        }
    }
    protected void GvCropVariety_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        check();
        try
        {
            if (e.CommandName == "edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                txtcropvariety.Text = ((Label)(gvrow.FindControl("lblcrvname"))).Text;

                BindCrop();
                ddlcrop.SelectedValue = ((Label)(gvrow.FindControl("lblcropcode"))).Text;
                Session["Cropvcode"] = ((Label)(gvrow.FindControl("lblcrvcode"))).Text;
                ddlcrop.Enabled = false;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                BindGrid();

            }
            if (e.CommandName == "dlt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                objBE.CropVrcode = ((Label)(gvrow.FindControl("lblcrvcode"))).Text;
                objBE.Action = "D";

                dt = ObjDL.SampleTypeIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage("CropVariety Failed,Pls Try Again");
                    txtcropvariety.Text = "";

                    BindGrid();
                }
                else
                {
                    cf.ShowAlertMessage("Your data has been Delete successfully");
                    txtcropvariety.Text = "";
                    btnUpdate.Visible = false;
                    //ddlcrop.ClearSelection();
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
        if (ddlcrop.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Crop");
            ddlcrop.Focus();
            return false;
        }

        if (txtcropvariety.Text == "")
        {
            cf.ShowAlertMessage("Please Enter CropVariety");
            txtcropvariety.Focus();
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