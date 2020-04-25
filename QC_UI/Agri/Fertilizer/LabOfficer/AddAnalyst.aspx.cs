using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;
public partial class Agri_Fertilizer_LabOfficer_AddAnalyst : System.Web.UI.Page
{
    AgriBE objBE = new AgriBE();
    AgriDL ObjDL = new AgriDL();
    Masters OBJMSTDL = new Masters();
    Master_BE OBJMSTBE = new Master_BE();
    CommonFuncs cf = new CommonFuncs();
    SampleSqlInjectionScreeningModule obj = new SampleSqlInjectionScreeningModule();
    DataTable dt;
    string con, user, dept, cate, state, Category;
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
                Response.Redirect("~/Error.aspx");
            }
        }
        PrevBrowCache.enforceNoCache();

        if (Session["UsrName"] != null && Session["RoleID"].ToString().Trim() == "16")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            Category = Session["Category"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["UsrName"].ToString();
                    BindGrid();
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    cf.ShowAlertMessage(ex.ToString());
                }
            }
        }
        else
        {
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void BindGrid()
    {
        try
        {
            
            OBJMSTBE.Dept = dept;
            OBJMSTBE.labid = Session["Labcode"].ToString();
            OBJMSTBE.Action = "R2";
            dt = OBJMSTDL.EmployeeIUDR_Agri(OBJMSTBE, con);
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
            Session["hf"] = num;
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
            cookie_value = Session["hf"].ToString();
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
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate())
            {
                if (CheckCaptcha())
                {
                    OBJMSTBE.Empcode = txtempcode.Text.Trim();
                    OBJMSTBE.EmpName = txtempname.Text.Trim();
                    OBJMSTBE.Mobile = txtempmobile.Text.Trim();
                    OBJMSTBE.Email = txtempemail.Text.Trim();
                    int ActiveSt = Convert.ToInt16(Rdbactive.SelectedValue);
                    OBJMSTBE.Active = ActiveSt.ToString();
                    OBJMSTBE.Dept = Session["Department"].ToString();
                    OBJMSTBE.statecode = Session["StateCode"].ToString();
                    OBJMSTBE.Role = "17";
                    OBJMSTBE.labid = Session["Labcode"].ToString();
                    OBJMSTBE.DistCode = Session["DistCode"].ToString();
                    OBJMSTBE.MandCode = Session["Mandcode"].ToString();
                    OBJMSTBE.UserName = txtusername.Text;
                    OBJMSTBE.UserID = user;
                    OBJMSTBE.Action = "IU";
                    dt = OBJMSTDL.EmployeeIUDR_Agri(OBJMSTBE, con);
                    if (dt.Rows.Count > 0)
                    {
                        cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        cf.ShowAlertMessage("Analyst User Created");
                    }
                    BindGrid();
                    Clear();
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
    protected void txtusername_TextChanged(object sender, EventArgs e)
    {
        check();
        try
        {
            dt = new DataTable();
            OBJMSTBE.UserName = txtusername.Text.Trim();
            OBJMSTBE.Action = "C";

            dt = OBJMSTDL.UserRegistration_IUDR(OBJMSTBE, con);
            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("User Name already taken");
                txtusername.Text = "";
                txtusername.Focus();
                BtnSave.Visible = false;
            }
            else
            {
                getCaptchaImage();
                BtnSave.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    public void getCaptchaImage()
    {
        Captcha ci = new Captcha();
        string text = Captcha.generateRandomCode();
        ViewState["captchtext"] = text;
        Image1.ImageUrl = "~/Assets/cpImg/cpImage.aspx?randomNo=" + text;
    }
    protected void btnImgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        captch.Text = "";
        getCaptchaImage();
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
                txtusername.Text = ((Label)(gvrow.FindControl("lblusername"))).Text;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                txtempcode.Enabled = false;
                txtusername.Enabled = false;
                lblmsg.Visible = false;


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

                OBJMSTBE.EmpId = ((Label)(gvrow.FindControl("lblempid"))).Text;
                OBJMSTBE.Action = "D";
                dt =OBJMSTDL.EmployeeIUDR_Agri(OBJMSTBE, con);
                if (dt.Rows.Count > 0)
                {

                }
                else
                {

                    cf.ShowAlertMessage("Deleted Successfully");
                }
                BindGrid();
               // Clear();
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
    protected void Clear()
    {
        txtempcode.Text = "";
        txtempemail.Text = "";
        txtempmobile.Text = "";
        txtempname.Text = "";
        txtusername.Text = "";
        lblmsg.Text = "";
        captch.Text = "";
        Rdbactive.ClearSelection();
        BtnSave.Visible = false;
    }
    protected bool CheckCaptcha()
    {
        if (captch.Text == "")
        {
            lblmsg.Text = "Enter Captcha Text";
            lblmsg.Visible = true;
            return false;
        }
        else if (captch.Text != ViewState["captchtext"].ToString())
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Enter Captcha Text as shown in the image";
            captch.Text = "";
            return false;
        }
        else if (captch.Text == ViewState["captchtext"].ToString())
            return true;
        else
        {
            lblmsg.Text = "image code is not valid.";
            captch.Text = "";
            return false;
        }
        return true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (validate())
            {
                if (CheckCaptcha())
                {
                    
                    OBJMSTBE.EmpName = txtempname.Text.Trim();
                    OBJMSTBE.Mobile = txtempmobile.Text.Trim();
                    OBJMSTBE.Email = txtempemail.Text.Trim();
                    int ActiveSt = Convert.ToInt16(Rdbactive.SelectedValue);
                    OBJMSTBE.Active = ActiveSt.ToString(); 
                    OBJMSTBE.UserID = user;
                    OBJMSTBE.Action = "U";
                    OBJMSTBE.EmpId = lblempid.Text;
                    dt = OBJMSTDL.EmployeeIUDR_Agri(OBJMSTBE, con);
                    if (dt.Rows.Count > 0)
                    {
                        cf.ShowAlertMessage(dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        cf.ShowAlertMessage("Analyst Details updated");
                    }
                    BindGrid();
                    Clear();
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
}