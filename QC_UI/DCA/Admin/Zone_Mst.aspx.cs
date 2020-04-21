using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class DCA_Admin_Zone_Mst : System.Web.UI.Page
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
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "0" || Session["RoleID"].ToString() == "7"))
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
                    txtzone.Focus();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    Response.Redirect(ex.ToString());
                }
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
            dt = ObjDL.ZoneIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvZone.DataSource = dt;
                GvZone.DataBind();
            }
            else
            {
                GvZone.DataSource = null;
                GvZone.DataBind();
                cf.ShowAlertMessage("No Data");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect(ex.ToString());
        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        try
        {
            check();
            if (validate())
            {

                objBE.Dept = Department;
                if (txtzone.Text != "")
                {
                    objBE.ZoneName = txtzone.Text;
                }
                objBE.statecode = state;
                objBE.UserID = user;
                objBE.Action = "I";
                dt = ObjDL.ZoneIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                }
                else
                {
                    cf.ShowAlertMessage("Saved Successfully");
                }
                BindGrid();
                txtzone.Text = "";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

        }
    }
    protected void GvZone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblzone.Text = ((Label)(gvrow.FindControl("lblLabCode"))).Text;
                txtzone.Text = ((Label)(gvrow.FindControl("lblLabName"))).Text;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;

                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

        }

        try
        {
            if (e.CommandName == "dlt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objBE.statecode = state;
                objBE.Dept = Department;
                objBE.ZoneCode= ((Label)(gvrow.FindControl("lblLabCode"))).Text;
                objBE.Action = "D";
                dt = ObjDL.ZoneIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    txtzone.Text = "";
                }
                else
                {
                    txtzone.Text = "";
                    cf.ShowAlertMessage("Deleted Successfully");
                }
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

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
                objBE.ZoneCode= lblzone.Text;
                objBE.ZoneName = txtzone.Text;
                objBE.Action = "U";
                dt = ObjDL.ZoneIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    txtzone.Text = "";
                    cf.ShowAlertMessage("Not Updated");
                    BindGrid();
                }
                else
                {
                    cf.ShowAlertMessage("Zone Details Updated Successfully");
                    btnUpdate.Visible = false;
                    BtnSave.Visible = true;
                    BindGrid();
                    txtzone.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

        }
    }
    public bool validate()
    {
        if (txtzone.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Zone");
            txtzone.Focus();
            return false;

        }
        if (txtzone.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtzone.Text);
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