using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using QC_BE;
using QC_DL;

public partial class DCA_Admin_AddUser : System.Web.UI.Page
{

    Master_BE MstObj = new Master_BE();
    Masters MstDL = new Masters();
    CommonFuncs ObjCommon = new CommonFuncs();
    DataTable dt;
    string con, user, state, Department;
    SampleSqlInjectionScreeningModule objsi = new SampleSqlInjectionScreeningModule();


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
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "0"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

           
            if (!IsPostBack)
            {
                random();
                try
                {
                    BindRoles();
                    BindDistrict();
                    BindEmployee();
                    BindDesignation();
                    lblUser.Text = Session["Role"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    divZone.Visible = false;
                    divlab.Visible = false;
                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    Response.Redirect("~/Error.aspx");
                }
            }
        }
    }
    protected void BindRoles()
    {
        MstObj.Action = "ROLE";
        MstObj.Id = Department;
        dt = MstDL.Getdetails(MstObj, con);
        ObjCommon.BindDropDownLists(ddlRole, dt, "RoleName", "Role_Id", "Select");
    }
    protected void BindDesignation()
    {
        MstObj.Action = "R";
        MstObj.Dept = Department;
        dt = MstDL.DesignationIUDR(MstObj, con);
        ObjCommon.BindDropDownLists(ddlDesig, dt, "Designation", "DesignationID", "Select");
    }
    protected void BindEmployee()
    {
        MstObj.Action = "R";
        MstObj.Dept = Department;
        dt = MstDL.EmployeeIUDR(MstObj, con);
        ObjCommon.BindDropDownLists(ddlEmployee, dt, "EmployeeName", "EmpID", "Select");
    }
    protected void BindZOne()
    {
        MstObj.Action = "R";
        MstObj.statecode = state;
        MstObj.Dept = Department;
        dt = MstDL.Zone_IUDR(MstObj, con);
        ObjCommon.BindDropDownLists(ddlzone, dt, "ZoneName", "ZoneId", "Select");
    }
    protected void BindLab()
    {
        MstObj.statecode = state;
        MstObj.Dept = Department;
        MstObj.CatId = null;
        MstObj.Action = "R";
        dt = MstDL.Lab_IUDR(MstObj, con);
        ObjCommon.BindDropDownLists(ddllab, dt, "LabName", "LabCode", "Select Mandal");
    }
    protected void BindDistrict()
    {
        MstObj.statecode = state;
        MstObj.Action = "D";
        dt = MstDL.GetLocations(MstObj, con);
        ObjCommon.BindDropDownLists(ddldist, dt, "DistName", "DistCode", "Select District");
    }
    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMandals();
    }

    protected void BindMandals()
    {
        MstObj.DistCode = ddldist.SelectedValue;
        MstObj.Action = "M";
        dt = MstDL.GetLocations(MstObj, con);
        ObjCommon.BindDropDownLists(ddlmand, dt, "MandName", "MandCode", "Select Mandal");
    }
    protected void txtUser_TextChanged(object sender, EventArgs e)
    {
        check();
        try
        {
            dt = new DataTable();
            MstObj.UserName = txtUser.Text.Trim();
            MstObj.Action = "C";

            dt = MstDL.UserRegistration_IUDR(MstObj, con);
            if (dt.Rows.Count > 0)
            {
                ObjCommon.ShowAlertMessage("User Name already taken");
                txtUser.Text = "";
            }
            else
                getCaptchaImage();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            lblError.Text = ex.Message.ToString();                
        }
    }
    public void getCaptchaImage()
    {
        Captcha ci = new Captcha();
        string text = Captcha.generateRandomCode();
        ViewState["captchtext"] = text;
        Image1.ImageUrl = "../../Assets/cpImg/cpImage.aspx?randomNo=" + text;
    }
    protected void btnImgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        captcha.Text = "";
        getCaptchaImage();
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            if (validate())
            {
                if (CheckCaptcha())
                {
                    MstObj.statecode = state;
                    MstObj.Dept = Department;
                    MstObj.Role = ddlRole.SelectedValue;

                    if (ddlRole.SelectedValue == "1")
                        MstObj.ZoneCode = ddlzone.SelectedValue;
                    if (ddlRole.SelectedValue == "4" || ddlRole.SelectedValue == "5")
                        MstObj.labid = ddllab.SelectedValue;

                    MstObj.DesignationID = ddlDesig.SelectedValue;
                    MstObj.EmpId = ddlEmployee.SelectedValue;
                    MstObj.Name = ddlEmployee.SelectedItem.Text;
                    MstObj.DistCode = ddldist.SelectedValue;
                    MstObj.MandCode = ddlmand.SelectedValue;
                    MstObj.Address = txtaddress.Text.Trim();
                    MstObj.UserName = txtUser.Text.Trim();
                    MstObj.UserID = user;
                    MstObj.Action = "I";
                    dt = MstDL.UserRegistration_IUDR(MstObj, con);
                    if (dt.Rows.Count > 0)
                        ObjCommon.ShowAlertMessage(dt.Rows[0][0].ToString());
                    else
                        ObjCommon.ShowAlertMessage("User Saved Successfully");
                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UserId"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            lblError.Text = ex.Message.ToString();  
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedValue == "1")
        {
            BindZOne();
            divZone.Visible = true;
            divlab.Visible = false;
            ddlDesig.SelectedValue ="3";
        }
        if (ddlRole.SelectedValue == "2" || ddlRole.SelectedValue == "3")
        {
            divZone.Visible = false;
            divlab.Visible = false;
        }
        if (ddlRole.SelectedValue == "4" || ddlRole.SelectedValue == "5")
        {
            divZone.Visible = false;
            divlab.Visible = true;
            BindLab();
        }

    }
    protected bool CheckCaptcha()
    {
        if (captcha.Text == ViewState["captchtext"].ToString())
        {
            return true;
        }
        else
        {
            lblmsg.Text = "image code is not valid.";
            captcha.Text = "";
            captcha.Focus();
            return false;
        }
        //return true;
    }
    protected void clear()
    {
        ddlRole.ClearSelection();
        ddlDesig.ClearSelection();
        ddlEmployee.ClearSelection();
        ddllab.ClearSelection();
        ddlzone.ClearSelection();
        ddldist.ClearSelection();
        ddlmand.ClearSelection();
        txtaddress.Text = "";
        txtUser.Text = "";
        captcha.Text = "";
    }
    public bool validate()
    {
        if (ddlRole.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage(" Select Role");
            ddlRole.Focus();
            return false;
        }
        if (ddlRole.SelectedValue == "1" && ddlzone.SelectedValue=="0")
        {
            ObjCommon.ShowAlertMessage(" Select DI Zone");
            ddlzone.Focus();
            return false;
        }
        if (ddlRole.SelectedValue == "3" || ddlRole.SelectedValue == "4")
        {
            if (ddllab.SelectedIndex == 0)
            {
                ObjCommon.ShowAlertMessage("Select Lab");
                ddllab.Focus();
                return false;
            }
        }            
        if (ddlDesig.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage(" Select Designation");
            ddlDesig.Focus();
            return false;
        }
        if (ddlEmployee.SelectedIndex == 0)
        {

            ObjCommon.ShowAlertMessage(" Select Employee");
            ddlEmployee.Focus();
            return false;
        }
        if (ddldist.SelectedIndex == 0)
        {
            ObjCommon.ShowAlertMessage("Please Select District");
            ddldist.Focus();
            return false;
        }
        //if (ddlmand.SelectedIndex == 0)
        //{
        //    ObjCommon.ShowAlertMessage("Please Select Mandal");
        //    ddlmand.Focus();
        //    return false;
        //}
        if (txtaddress.Text == "")
        {
            ObjCommon.ShowAlertMessage("Please Enter Address");
            txtaddress.Focus();
            return false;
        }
        if (txtaddress.Text != "")
        {
            bool val;
            val = objsi.CheckInput_new(txtaddress.Text);
            if (val == true)
            {
                Response.Redirect("~/Error.aspx");
                return false;
            }
        }
        if (txtUser.Text == "")
        {
            ObjCommon.ShowAlertMessage("Please Enter Username");
            txtUser.Focus();
            return false;
        }
        if (txtUser.Text != "")
        {
            bool val;
            val = objsi.CheckInput_new(txtUser.Text);
            if (val == true)
            {
                Response.Redirect("~/Error.aspx");
                return false;
            }
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