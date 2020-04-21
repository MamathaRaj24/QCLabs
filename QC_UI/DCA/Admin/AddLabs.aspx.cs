using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DCA_Admin_AddLabs : System.Web.UI.Page
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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "0")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

            
        }
        if (!IsPostBack)
        {
            try
            {
                random();
                lblUser.Text = Session["Role"].ToString();
                lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                BindGrid();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                Response.Redirect("../Error.aspx");
            }
        }
    }
    protected void BindGrid()
    {
        try
        {
            objBE.Action = "R";
            objBE.statecode = state;
            objBE.Dept = Department;
            dt = ObjDL.Lab_IUDR(objBE, con);
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
            Response.Redirect("../Error.aspx");
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
                if (txtLabName.Text != "")
                {
                    objBE.labName = txtLabName.Text;
                }
                objBE.UserID = user;
                objBE.Action = "I";
                dt = ObjDL.Lab_IUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    ObjCommon.ShowAlertMessage(dt.Rows[0][0].ToString());
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Saved Successfully");
                }
                BindGrid();
                txtLabName.Text = "";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("../Error.aspx");
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
                objBE.labid = lblLabCode.Text;
                objBE.labName = txtLabName.Text;
                objBE.Action = "U";
                dt = ObjDL.Lab_IUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    txtLabName.Text = "";
                    ObjCommon.ShowAlertMessage("Not Updated");
                }
                else
                {
                    ObjCommon.ShowAlertMessage("Lab Details Updated Successfully");
                    btnUpdate.Visible = false;
                    BtnSave.Visible = true;
                    BindGrid();
                    txtLabName.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("../Error.aspx");
        }
    }

    protected void GvLabs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblLabCode.Text = ((Label)(gvrow.FindControl("lblLabCode"))).Text;
                txtLabName.Text = ((Label)(gvrow.FindControl("lblLabName"))).Text;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;

                BindGrid();
            }
            if (e.CommandName == "dlt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objBE.statecode = state;
                objBE.Dept = Department;
                objBE.labid = ((Label)(gvrow.FindControl("lblLabCode"))).Text;
                objBE.Action = "D";
                dt = ObjDL.Lab_IUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    txtLabName.Text = "";
                }
                else
                {
                    txtLabName.Text = "";
                    ObjCommon.ShowAlertMessage("Deleted Successfully");
                }
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("../Error.aspx");
        }
    }
    public bool validate()
    {
        if (txtLabName.Text == "")
        {
            ObjCommon.ShowAlertMessage("Please Enter LabName");
            txtLabName.Focus();
            return false;

        }
        if (txtLabName.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtLabName.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
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