using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;

public partial class UnitOfficer_AllotToAnalyst : System.Web.UI.Page
{
    Registration_BE objBE = new Registration_BE();
    Registration_DL ObjDL = new Registration_DL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con;

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
        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "4")
        {
            con = Session["ConnKey"].ToString();
            
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["UnitOfficerName"].ToString() + ",   Lab Allotted :" + Session["Labname"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                random();
                try
                {                  
                    BindSamples();
                    //BindAnalyst();
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


    protected void BindSamples()
    {
        dt = new DataTable();
        objBE.Action = "SMPL";
        objBE.labID = Session["Labcode"].ToString();
        dt = ObjDL.UOAction(objBE, con);
        if (dt.Rows.Count > 0)

            cf.BindDropDownLists(ddlsample, dt, "SampleID", "SampleID", "Select");
    }
    protected void BindAnalyst()
    {
        dt = new DataTable();
        objBE.Action = "ANALYST";
        objBE.labID = Session["Labcode"].ToString();
        dt = ObjDL.UOAction(objBE, con);
        if (dt.Rows.Count > 0)
            cf.BindDropDownLists(ddlAnalyst, dt, "Name", "User_Code", "Select");
        else
            cf.ShowAlertMessage("No Analysts Added");
    }

    //protected void BindJSO()
    //{
    //    dt = new DataTable();
    //    objBE.Action = "JSO";
    //    objBE.labID = Session["Labcode"].ToString();
    //    dt = ObjDL.UOAction(objBE, con);
    //    if (dt.Rows.Count > 0)
    //        cf.BindDropDownLists(ddljso, dt, "Name", "User_Code", "Select");
    //    else
    //        cf.ShowAlertMessage("No JSO Added");
    //}
    protected void btnAllot_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            dt = new DataTable();
            objBE.SampleID = ddlsample.SelectedValue;
   
            objBE.AnalystId = ddlAnalyst.SelectedValue;
            objBE.Action = "UNIT";
            dt = ObjDL.UOAction(objBE, con);
            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage(dt.Rows[0][0].ToString());
            else
                cf.ShowAlertMessage(ddlsample.SelectedValue + " Allotted  to Analyst  " + ddlAnalyst.SelectedItem.Text);
            BindSamples(); 
            ddlAnalyst.ClearSelection();
            btnAllot.Visible = false;
            divid.Visible = false;
           // btnAllot.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    }
   
    protected void Rdbidlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rdbidlist.SelectedValue == "0")
        {
            divid.Visible = true;
            //divjso.Visible = false;
            Btnjso.Visible = false;
            BindAnalyst();
            btnAllot.Visible = true;
        }
        else
        {
            divid.Visible = false;
            //divjso.Visible = true;
            Btnjso.Visible = true;
            btnAllot.Visible = false;
            //BindJSO();
        }
    }
    protected void Btnjso_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            dt = new DataTable();
            objBE.SampleID = ddlsample.SelectedValue;
            objBE.AnalystId= Session["UnitOfficerCode"].ToString();
            objBE.Action = "UNIT";
            dt = ObjDL.UOAction(objBE, con);
            if (dt.Rows.Count > 0)
                cf.ShowAlertMessage(dt.Rows[0][0].ToString());
            else
                cf.ShowAlertMessage(ddlsample.SelectedValue + " Allotted  to JSo  " + Session["UnitOfficerName"].ToString());
            BindSamples();
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
   
}

