using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;
public partial class AO_FreezeFertilizer : System.Web.UI.Page
{
    Master_BE objBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriDL Regobjdl = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    AgriBE objR = new AgriBE();
    DataTable dt;
    string con, user, state, Department, Aocode;

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
        if (Session["RoleID"].ToString() == "8" || Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10")
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            Department = Session["Department"].ToString();

            Aocode = Session["AO_Code"].ToString();
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DistName"].ToString() + " -  " + Session["MandName"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            if (!IsPostBack)
            {
                try
                {
                    random();
                    //lblUser.Text = Session["Role"].ToString();
                   
                    BindCategory();
                    pnlfreeze.Visible = false;
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

    protected void BindGrid()
    {

        objR.Action = "R";
        objR.DI_AO_Code = Aocode;
        objR.dept = Department;
        objR.SampleCategory = RblCategory.SelectedValue;

        dt = Regobjdl.FertlizerRegistration(objR, con);

        if (dt.Rows.Count > 0)
        {
            Gvfreeze.DataSource = dt;
            Gvfreeze.DataBind();
            lblfreezename.Text = RblCategory.SelectedItem.Text;
        }
        else
        {
            Gvfreeze.DataSource = null;
            Gvfreeze.DataBind();
            cf.ShowAlertMessage("No Data");
            pnlfreeze.Visible = false;
            btnfreeze.Visible = false;
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
        pnlfreeze.Visible = true;
        BindGrid();
    }
    protected void btnfreeze_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtfreeze = new DataTable();
            dtfreeze.Columns.Add("sampleID", typeof(string));
            int j = 0;
            foreach (GridViewRow gr in Gvfreeze.Rows)
            {
                if (((CheckBox)gr.FindControl("chkSelct")).Checked == true)
                {
                    dtfreeze.Rows.Add();
                    dtfreeze.Rows[j]["sampleID"] = ((Label)gr.FindControl("lblsmid")).Text;
                    j++;
                }
            }
            if (j == 0)
                cf.ShowAlertMessage("Select atleast one row to freeze");
            else
            {
                objR.Action = "AO";
                objR.TVP = dtfreeze;
                dt = Regobjdl.FreezeSamples(objR, con);
                if (dt.Rows.Count > 0)
                    cf.ShowAlertMessage("Not freezed, try again");
                else
                {
                    cf.ShowAlertMessage(" data freezed");
                    Gvfreeze.DataSource = null;
                    Gvfreeze.DataBind();
                    
                }
                pnlfreeze.Visible = false;
                RblCategory.ClearSelection();
                BindGrid();
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