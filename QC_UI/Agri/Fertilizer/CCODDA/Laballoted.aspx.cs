using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class Agri_Fertilizer_CCODDA_Laballoted : System.Web.UI.Page
{
    Masters ObjDL = new Masters();
    Master_BE objMBE = new Master_BE();
    AgriDL Objrdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objBE = new AgriBE();
    DataTable dt;
    string con, user, state, Department, dicode; 
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

        if (Session["UsrName"] != null && Session["RoleID"].ToString() == "15")
        {
            state = Session["StateCode"].ToString();
            user = Session["CCDDA_Code"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();
            //  dicode = Session["ssoCode"].ToString();


            if (!IsPostBack)
            {
                random();
                try
                {

                    lblUser.Text = Session["Role"].ToString() + " -  " + Session["UsrName"].ToString();
                    lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    BindSamples();
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
            Response.Redirect("../Error.aspx");
        }

    }
    protected void BindSamples()
    {
        dt = new DataTable();
      //  objBE.dept = Department;
        //objBE.UserId = user;  
        objBE.Action = "ASMPL";
        dt = Objrdl.GenerateSticker_AGRI(objBE, con);
        if (dt.Rows.Count > 0)
        {
            cf.BindDropDownLists(ddlsample, dt, "SampleID", "SampleType_ID", "Select");
          //  lblsampletypeid.Text = dt.Rows[0]["SAMPLETYPE"].ToString();
        }
        
    } 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objBE.dept = Department;
        objBE.UserId = Session["Category"].ToString();
        objBE.SampleID = ddlsample.SelectedItem.Text;
        objBE.SampleType = ddlsample.SelectedValue;
        objBE.status = "0";
        objBE.Action = "ALLOT";
        dt = Objrdl.GenerateSticker_AGRI(objBE, con);
        if (dt.Rows.Count > 0)
        {
            
            cf.ShowAlertMessage("Sample Alloted is : "  + dt.Rows[0]["LabName"].ToString());
        }
        else
        {

            cf.ShowAlertMessage("Lab alloted failed");
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