using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;


public partial class AO_EditSample_AO : System.Web.UI.Page
{

    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, dist, mand, Department, Aocode;

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
        if (Session["RoleID"].ToString() == "8" || Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

            Aocode = Session["AO_Code"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DistName"].ToString() + " -  " + Session["MandName"].ToString();
        }
        else
        {
            Response.Redirect("../Error.aspx");
        }
        if (!IsPostBack)
        {
            try
            {               
                
                lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                BindCategory();

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                cf.ShowAlertMessage(ex.ToString());
            }
        }

    }
    protected void BindCategory()
    {
        objBE.Id = Department;
        objBE.Action = "CAT";
        dt = ObjDL.Getdetails(objBE, con);
        cf.BindRadioLists(RblCategory, dt, "category_name", "category_id", "0");
    }

    protected void RblCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SampleCategory"] = RblCategory.SelectedValue;
        if (RblCategory.SelectedItem.Text == "Fertilizers")
        {
            Response.Redirect("~/Agri/AO/EditFertilizer.aspx");
        }

        if (RblCategory.SelectedItem.Text == "Pesticides")
        {
            Response.Redirect("EditPesticides.aspx", false);
        }
    }
}