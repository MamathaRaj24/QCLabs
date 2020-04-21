using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Admin_Employee_Mst : System.Web.UI.Page
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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "7")
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
                    txtempcode.Focus();
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
    protected void BindGrid()
    {
        try
        {
            objBE.Action = "R";
            objBE.Dept = Department;
            dt = ObjDL.EmployeeIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                Gvemp.DataSource = dt;
                Gvemp.DataBind();
            }
            else
            {
                Gvemp.DataSource = null;
                Gvemp.DataBind();
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
        try
        {
            check();
            if (validate())
            {

                objBE.Dept = Department;
                if (txtempcode.Text != "")
                {
                    objBE.Empcode = txtempcode.Text.Trim();
                }
                if (txtempname.Text != "")
                {
                    objBE.EmpName = txtempname.Text.Trim();
                }
                if (txtempmobile.Text != "")
                {
                    objBE.Mobile = txtempmobile.Text.Trim();
                }
                if (txtempemail.Text != "")
                {
                    objBE.Email = txtempemail.Text.Trim();
                }
                int ActiveSt = Convert.ToInt16(Rdbactive.SelectedValue);
                objBE.Active = ActiveSt.ToString();
                objBE.UserID = user;
                objBE.Action = "I";
                dt = ObjDL.EmployeeIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {
                    cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                }
                else
                {
                    cf.ShowAlertMessage("Saved Successfully");
                }
                BindGrid();
                Clear();
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

                //objBE.Dept = Department;
                //if (txtempcode.Text != "")
                //{
                //    objBE.Empcode = txtempcode.Text.Trim();
                //}
                if (txtempname.Text != "")
                {
                    objBE.EmpName = txtempname.Text.Trim();
                }
                if (txtempmobile.Text != "")
                {
                    objBE.Mobile = txtempmobile.Text.Trim();
                }
                if (txtempemail.Text != "")
                {
                    objBE.Email = txtempemail.Text.Trim();
                }
                int ActiveSt = Convert.ToInt16(Rdbactive.SelectedValue);
                objBE.Active = ActiveSt.ToString();
                // objBE.UserID = user;
                objBE.Action = "U";
                objBE.EmpId = lblempid.Text.Trim();
                dt = ObjDL.EmployeeIUDR(objBE, con);

                if (dt.Rows.Count > 0)
                {

                    cf.ShowAlertMessage("Not Updated");
                }
                else
                {
                    cf.ShowAlertMessage("Designation Details Updated Successfully");
                    btnUpdate.Visible = false;
                    BtnSave.Visible = true;
                    BindGrid();

                }
                Clear();
                txtempcode.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

        }
    }
    protected void Gvemp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edt")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                lblempid.Text = ((Label)(gvrow.FindControl("lblempid"))).Text;
                txtempname.Text = ((Label)(gvrow.FindControl("lblempname"))).Text;
                txtempmobile.Text = ((Label)(gvrow.FindControl("lblmobilenumber"))).Text;
                txtempemail.Text = ((Label)(gvrow.FindControl("lblemail"))).Text;
                txtempcode.Text = ((Label)(gvrow.FindControl("lblempcode"))).Text;
                if (((Label)(gvrow.FindControl("lblactive"))).Text == "TRUE")
                {
                    Rdbactive.SelectedValue = "0";
                }
                else
                {
                    Rdbactive.SelectedValue = "1";
                }
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                txtempcode.Enabled = false;

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

                objBE.EmpId = ((Label)(gvrow.FindControl("lblempid"))).Text;
                objBE.Action = "D";
                dt = ObjDL.EmployeeIUDR(objBE, con);
                if (dt.Rows.Count > 0)
                {

                }
                else
                {

                    cf.ShowAlertMessage("Deleted Successfully");
                }
                BindGrid();
                Clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

        }
    }
    protected void Clear()
    {
        txtempcode.Text = "";
        txtempemail.Text = "";
        txtempmobile.Text = "";
        txtempname.Text = "";
        Rdbactive.ClearSelection();
    }
    public bool validate()
    {
        if (txtempcode.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Empcode");
            txtempcode.Focus();
            return false;

        }
        if (txtempcode.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtempcode.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtempname.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Empname");
            txtempname.Focus();
            return false;

        }
        if (txtempname.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtempname.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtempmobile.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Mobile");
            txtempmobile.Focus();
            return false;

        }
        if (txtempmobile.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtempmobile.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (txtempemail.Text == "")
        {
            cf.ShowAlertMessage("Please Enter Email");
            txtempemail.Focus();
            return false;

        }
        if (txtempemail.Text != "")
        {
            bool val;
            val = obj.CheckInput_new(txtempemail.Text);
            if (val == true)
                Response.Redirect("~/Error.aspx");
        }
        if (Rdbactive.SelectedIndex == -1)
        {
            cf.ShowAlertMessage("Please Select Active");
            Rdbactive.Focus();
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