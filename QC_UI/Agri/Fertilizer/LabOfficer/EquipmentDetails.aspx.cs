using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;


public partial class Agri_Fertilizer_LabOfficer_EquipmentDetails : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriBE objagribe = new AgriBE();
    AgriDL objagridl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, dept, Category;

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
        if (Session["UserId"] != null && Session["RoleID"].ToString() == "16")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            Category = Session["Category"].ToString();

            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

            lblUser.Text = Session["Role"].ToString() + " -  " + Session["UsrName"].ToString();

            if (!IsPostBack)
            {
                random();
                BindSampleType();
            }
        }
        else
        {
            Response.Redirect("~/Error.aspx");
        }
    }

    public bool validate()
    {
        if (ddlSampType.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Please Select Sample Type");
            ddlSampType.Focus();
            return false;
        }
        return true;
    }
    protected void BindGrid()
    {
        try
        {
            objBE.statecode = Session["StateCode"].ToString();
            objBE.Dept = Session["Department"].ToString();
            objBE.CatId = Session["Category"].ToString();
            objBE.Action = "R";
            objBE.SampleTypeId = ddlSampType.SelectedValue;

            dt = ObjDL.Equipment_IUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GvEquipment.DataSource = dt;
                GvEquipment.DataBind();
                divequipment.Visible = true;
            }
            else
            {
                GvEquipment.DataSource = null;
                GvEquipment.DataBind();
                cf.ShowAlertMessage("No Data");
                divequipment.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    protected void BindSampleType()
    {
        dt = new DataTable();
        objBE.Dept = Session["Department"].ToString();
        objBE.CatId = Session["Category"].ToString();
        objBE.Action = "RE";
        dt = ObjDL.SampleTypeIUDR(objBE, con);
        cf.BindDropDownLists(ddlSampType, dt, "SampleTypeName", "SampleTypeCode", "Select");
    }
    protected void ddlSampType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objagribe.SampleType = ddlSampType.SelectedValue;
            objagribe.user = user;
            objagribe.dept = dept;
            objagribe.labID = Session["Labcode"].ToString();
            DataTable dtequipment = new DataTable();
            dtequipment.Columns.Add("Equipment_Code", typeof(string));
            dtequipment.Columns.Add("Condition", typeof(string));
            dtequipment.Columns.Add("Availability", typeof(string));
            dtequipment.Columns.Add("WorkingCondition", typeof(string));
            int i = 0;
            foreach (GridViewRow gr in GvEquipment.Rows)
            {

                TextBox condition = (TextBox)gr.FindControl("txtCondition");
                RadioButtonList rblavailable = (RadioButtonList)gr.FindControl("rblisAvailable");
                RadioButtonList rblworkcondition = (RadioButtonList)gr.FindControl("rblWorkCondition");

                // if (ValidateGrid(condition, rblavailable, rblworkcondition))
                // {
                dtequipment.Rows.Add();
                dtequipment.Rows[i]["Equipment_Code"] = ((Label)gr.FindControl("lblEquipCode")).Text.Trim();
                dtequipment.Rows[i]["Condition"] = ((TextBox)gr.FindControl("txtCondition")).Text.Trim();
                dtequipment.Rows[i]["Availability"] = ((RadioButtonList)gr.FindControl("rblisAvailable")).SelectedValue.Trim();
                dtequipment.Rows[i]["WorkingCondition"] = ((RadioButtonList)gr.FindControl("rblWorkCondition")).SelectedValue.Trim();
                // }
                i++;
            }

            //}
            objagribe.TVP = dtequipment;
            objagribe.ip = HttpContext.Current.Request.UserHostAddress;
            objagribe.type = rdbldontallot.SelectedValue;
            objagribe.Category = Session["Category"].ToString();
            objagribe.Action = "I";
            dt = objagridl.Equipemntdetails(objagribe, con);
            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage(dt.Rows[0][0].ToString());
            else
            {
                cf.ShowAlertMessage("Equipment Details saved");
                BindSampleType();
                divequipment.Visible = false;
            }
            ddlSampType.ClearSelection();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
    public bool ValidateGrid(TextBox txtcondition, RadioButtonList rblavlblity, RadioButtonList rblworkcondition)
    {

        if (txtcondition.Text == "")
        {
            cf.ShowAlertMessage("Enter Condition");
            txtcondition.Focus();
            return false;
            divequipment.Visible = true;

        }
        if (rblavlblity.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select Availability");
            rblavlblity.Focus();
            return false;
        }
        if (rblworkcondition.SelectedIndex == 0)
        {
            cf.ShowAlertMessage("Select work condtion");
            rblavlblity.Focus();
            return false;
        }
        return false;
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
}