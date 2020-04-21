using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Agri_SeedAdmin_TestParameters : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs cf = new CommonFuncs();
    Validate objValidate = new Validate();
    DataTable dt;
    string con, user, state, Department;
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
                    BindTest();
                    
                    ddltest.Focus();
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
    protected void BindTest()
    {
        try
        {
            objBE.Action = "R";
            //objBE.Testcode = ddltest.SelectedValue;
            objBE.Dept = Department;
            dt = ObjDL.TestIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.BindDropDownLists(ddltest, dt, "TestName", "Testid", "Select");
               
            }
           
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }

    }
    protected void CreateRow()
    {
        DataTable dtparam = new DataTable();
        dtparam.Columns.Add("param", typeof(string));
        dtparam.Columns.Add("stv", typeof(string));
        dtparam.Rows.Add();
        GVTestparameters.DataSource = dtparam;
        GVTestparameters.DataBind();
    }
    protected void BindGrid()
    {
        DataTable dt = new DataTable();
        objBE.Testcode = ddltest.SelectedValue;
        objBE.Dept = Department;
        objBE.Action = "R";
        dt=ObjDL.AgriTestParameters_IUDR(objBE,con);
        if (dt.Rows.Count > 0)
        {
            Gvtestprm.DataSource = dt;
            Gvtestprm.DataBind();
        }
        else
        {
            Gvtestprm.DataSource = null;
            Gvtestprm.DataBind();
            cf.ShowAlertMessage("No Data");
        }
    }
    protected void GVTestparameters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Remove")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("param", typeof(string));
                dtparam.Columns.Add("stv", typeof(string));
                int j = 0;
                foreach (GridViewRow gr in GVTestparameters.Rows)
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["param"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    dtparam.Rows[j]["stv"] = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                    j++;
                }
                dtparam.Rows.RemoveAt(gvrow.RowIndex);
                if (dtparam.Rows.Count == 0)
                {
                    dtparam.Rows.Add();
                }
                GVTestparameters.DataSource = dtparam;
                GVTestparameters.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

        }
    }
    protected void imgBtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("param", typeof(string));
            dtparam.Columns.Add("stv", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in GVTestparameters.Rows)
            {
                string param = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                string stv = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                if (param != "" && stv != "")
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["param"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    dtparam.Rows[j]["stv"] = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                    j++;
                }
            }
            dtparam.Rows.Add();
            GVTestparameters.DataSource = dtparam;
            GVTestparameters.DataBind();
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
            DataTable dtparam = new DataTable();
            dtparam.Columns.Add("param", typeof(string));
            dtparam.Columns.Add("stv", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in GVTestparameters.Rows)
            {
                string param = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                string stv = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                if (param != "" && stv != "")
                {
                    dtparam.Rows.Add();
                    dtparam.Rows[j]["param"] = ((TextBox)gr.FindControl("txtParam")).Text.Trim();
                    dtparam.Rows[j]["stv"] = ((TextBox)gr.FindControl("txtStdVal")).Text.Trim();
                    j++;
                }
            }
            dt = new DataTable();
            objBE.Testcode = ddltest.SelectedValue;
            objBE.User = user;
            objBE.Action = "I";
            objBE.TVP = dtparam;
            objBE.Dept = Department;
            dt = ObjDL.AgriTestParameters_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage("Data Not Saved");
            else
                cf.ShowAlertMessage("Data Saved");
            BindTest();
           // GVTestparameters.Visible = false;
            ddltest.ClearSelection();
          
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
  
    protected void ddltest_SelectedIndexChanged(object sender, EventArgs e)
    {
        CreateRow();
        BindGrid();
    }
    protected void Gvtestprm_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gvtestprm.EditIndex = e.NewEditIndex;
        
        BindGrid();
    }
    protected void Gvtestprm_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = Gvtestprm.Rows[e.RowIndex];
            int paramid = Convert.ToInt32(Gvtestprm.DataKeys[e.RowIndex].Values[0]);
            int testid = Convert.ToInt32(Gvtestprm.DataKeys[e.RowIndex].Values[1]);
            string paramname = (row.FindControl("txtParam") as TextBox).Text;
            string stdvalue = (row.FindControl("txtStdVal") as TextBox).Text;
          

            objBE.ParamName = paramname.ToString();
            objBE.StdValue = stdvalue.ToString();;
            objBE.Testcode = testid.ToString();
            objBE.ParamID = paramid.ToString();
            objBE.Action = "U";
            dt = ObjDL.AgriTestParameters_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Update Failed");
                BindGrid();
            }
            else
            {
                cf.ShowAlertMessage("Update Succsessfully");
                Gvtestprm.EditIndex = -1;
                BindGrid();
            }
           
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

        }
    }
    protected void Gvtestprm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Gvtestprm.EditIndex = -1;
        BindGrid();
    }
    protected void Gvtestprm_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            // int userid = Convert.ToInt32(Gvtestprm.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = Gvtestprm.Rows[e.RowIndex];
            int paramid = Convert.ToInt32(Gvtestprm.DataKeys[e.RowIndex].Values[0]);
            int testid = Convert.ToInt32(Gvtestprm.DataKeys[e.RowIndex].Values[1]);

            objBE.Testcode = testid.ToString();
            objBE.ParamID = paramid.ToString();
            objBE.Action = "D";
            dt = ObjDL.AgriTestParameters_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Delete Failed");
                BindGrid();
            }
            else
            {
                cf.ShowAlertMessage("Delete Succsessfully");
                BindGrid();

            }
            Gvtestprm.EditIndex = -1;
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());

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