using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QC_BE;
using QC_DL;

public partial class AO_MemoGeneration : System.Web.UI.Page
{
    Master_BE objmBE = new Master_BE();
    Masters ObjDL = new Masters();
    AgriBE objBE = new AgriBE();
    AgriDL objdi = new AgriDL();
    CommonFuncs cf = new CommonFuncs();
    DataTable dt;
    string con, user, state, AO_code, dept;


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
                Response.Redirect("~/Error.aspx");
        }
        PrevBrowCache.enforceNoCache();
        if (Session["UserId"] != null && (Session["RoleID"].ToString() == "8" || Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10"))
        {
            state = Session["StateCode"].ToString();
            user = Session["UserId"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            AO_code = Session["AO_Code"].ToString();
            lblDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            
            lblUser.Text = Session["Role"].ToString() + " -  " + Session["DistName"].ToString() + " -  " + Session["MandName"].ToString();

            if (!IsPostBack)
            {
                try
                {
                    random();

                    BindCategory();
                    btnmemo.Visible = false;
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
        objmBE.Id = dept;
        objmBE.Action = "CAT";
        dt = ObjDL.Getdetails(objmBE, con);
        cf.BindRadioLists(RblCategory, dt, "category_name", "category_id", "0");
    }
    protected void RblCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void BindGrid()
    {
        check();
        try
        {
            objBE.UserId = AO_code;
            objBE.SampleCategory = RblCategory.SelectedValue;
            objBE.dept = dept;
            objBE.Action = "R";
            dt = objdi.MemoID(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GVMemo.DataSource = dt;
                GVMemo.DataBind();
                lblmemoid.Text = RblCategory.SelectedItem.Text;
                btnmemo.Visible = true;
            }
            else
            {
                GVMemo.DataSource = null;
                GVMemo.DataBind(); 
                btnmemo.Visible = false;
                lblmemoid.Visible = false;
                cf.ShowAlertMessage("No Records Available To Generate Memo");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            cf.ShowAlertMessage(ex.ToString());
        }
    } 

    protected void btnmemo_Click(object sender, EventArgs e)
    {
        check();
        try
        {
            DataTable ddt = new DataTable();
            ddt.Columns.Add("RegID", typeof(string));

            int i = 0;
            foreach (GridViewRow gr in GVMemo.Rows)
            {
                CheckBox CHK = ((CheckBox)gr.FindControl("chkSelct"));
                if (CHK.Checked == true)
                {
                    ddt.Rows.Add();
                    ddt.Rows[i]["RegID"] = ((Label)gr.FindControl("lblSampId")).Text;
                    i++;
                }
            }
            objBE.TVP = ddt;
            objBE.state = state;
            objBE.dept = dept;
            objBE.UserId = AO_code;
            objBE.Action = "MID";
            dt = objdi.MemoID(objBE, con);

            if (dt.Rows.Count > 0)
            {
                cf.ShowAlertMessage("Memo Id Generated Successfully");
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
