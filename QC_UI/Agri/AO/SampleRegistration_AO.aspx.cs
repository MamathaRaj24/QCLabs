using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class AO_SampleRegistration_AO : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state,dist,mand, Department,Aocode;


    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.ServerVariables["HTTP_REFERER"] == null) || (Request.ServerVariables["HTTP_REFERER"] == ""))
            Response.Redirect("~/Error.aspx");
        else
        {
            string http_ref = Request.ServerVariables["HTTP_REFERER"].Trim();
            string http_hos = Request.ServerVariables["HTTP_HOST"].Trim();
            int len = http_hos.Length;
            if (http_ref.IndexOf(http_hos, 0) < 0)
               Response.Redirect("../Error.aspx");
        }
        PrevBrowCache.enforceNoCache();
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "8" || Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

            Aocode = Session["AO_Code"].ToString();
            lblUser.Text = Session["Role"].ToString() + "  " + Session["DistName"].ToString() + "  " + Session["MandName"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                try
                {
                    random();
                   
                    BindCategory();

                }
                catch (Exception ex)
                {
                    ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                    cf.ShowAlertMessage(ex.ToString());
                }
            }
        }
        else
            Response.Redirect("../Error.aspx");        
    }

    protected void BindCategory()
    {
        check();
        objBE.Id = Department;
        objBE.Action = "CAT";
        dt = ObjDL.Getdetails(objBE, con);       
        cf.BindRadioLists(RblCategory, dt, "category_name", "category_id", "0");
    }

    protected void RblCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SampleCategory"] = RblCategory.SelectedValue;
        if (RblCategory.SelectedItem.Text == "Fertilizers")
            Response.Redirect("~/Agri/AO/Ferti_Reg.aspx", false);

        if (RblCategory.SelectedItem.Text == "Pesticides")
            Response.Redirect("~/Agri/AO/Pesticides_Reg.aspx", false);
        if (RblCategory.SelectedItem.Text == "Seeds")
            Response.Redirect("~/Agri/AO/SeedRegistrationAO.aspx", false);
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