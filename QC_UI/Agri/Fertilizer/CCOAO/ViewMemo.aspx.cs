using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using System.Data;
using QC_BE;
public partial class Agri_Fertilizer_CCOAO_ViewMemo : System.Web.UI.Page
{
    Masters ObjMDL = new Masters();
    Master_BE objMBE = new Master_BE();
    AgriBE objBE = new AgriBE();
    AgriDL ObjDL = new AgriDL();
    CommonFuncs ObjCommon = new CommonFuncs();
    DataTable dt;
    string con, user, dept, cate;
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

        if (Session["UsrName"] != null && Session["RoleID"].ToString().Trim() == "14")
        {
            user = Session["CCOAO"].ToString();
            con = Session["ConnKey"].ToString();
            dept = Session["Department"].ToString();
            cate = Session["Category"].ToString();
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
                    ObjCommon.ShowAlertMessage(ex.ToString());
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
            dt = new DataTable();
            objBE.Action = "VIEWMEMO";
            objBE.UserId = user;
            objBE.dept = dept;
            objBE.SampleCategory = cate;
            dt = ObjDL.CourierIUDR(objBE, con);
            if (dt.Rows.Count > 0)
            {
                GVAck.DataSource = dt;
                GVAck.DataBind();
            }
            else
            {
                GVAck.DataSource = null;
                GVAck.DataBind();
                lbltext.Visible = true;

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }

    }


    protected void GVAck_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {

            if (e.CommandName == "M")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                objBE.MemoId = ((Label)(gvrow.FindControl("lblMemoId"))).Text;
                RadioButtonList rdblist = (RadioButtonList)gvrow.FindControl("Rdbaceptrjctlist");
                DropDownList ddlrejected = ((DropDownList)(gvrow.FindControl("ddlrjctreson")));

                if (ValidateGrid(rdblist, ddlrejected))
                {
                    if (rdblist.SelectedValue == "A")
                    {
                        objBE.status = rdblist.SelectedValue;
                        objBE.Ref = null;
                    }
                    if (rdblist.SelectedValue == "R")
                    {

                        objBE.status = rdblist.SelectedValue;
                        objBE.Ref = ddlrejected.SelectedValue;
                    }
                    dt = new DataTable();
                    objBE.Action = "ACTN_MEMO";
                    dt = ObjDL.GenerateSticker_AGRI(objBE, con);
                    if (dt.Rows.Count > 0)
                    {
                        ObjCommon.ShowAlertMessage("Failed Data");
                    }
                    else
                    {
                        ObjCommon.ShowAlertMessage("Saved Data");
                    }
                    BindGrid();
                }
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendExcepToDB(ex, Session["UsrName"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            ObjCommon.ShowAlertMessage(ex.ToString());
        }
    }
    protected void Rdbaceptrjctlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList Dropdown = sender as RadioButtonList;
        GridViewRow gRow = (GridViewRow)Dropdown.NamingContainer;
        RadioButtonList rblaceptrjct = ((RadioButtonList)(gRow.FindControl("Rdbaceptrjctlist")));
        DropDownList ddlrejected = ((DropDownList)(gRow.FindControl("ddlrjctreson")));
        Button buttongrid = ((Button)(gRow.FindControl("btnAck")));
        GVAck.TemplateControl.Visible = true;
        if (rblaceptrjct.SelectedValue == "R")
        {
            ddlrejected.Enabled = true;
            objMBE.Dept = dept;
            objMBE.Action = "R";
            dt = ObjMDL.RejectedReasons(objMBE, con);
            ObjCommon.BindDropDownLists(ddlrejected, dt, "Reject_Reason", "ID", "Select");
            buttongrid.Visible = true;
            buttongrid.Text = "Rejected";
            buttongrid.BackColor = System.Drawing.Color.Red;

        }
        else
        {
            buttongrid.Visible = true;
            buttongrid.Text = "Aproove";
            buttongrid.BackColor = System.Drawing.Color.Green;
            ddlrejected.Enabled = false;
        }
    }
    protected bool ValidateGrid(RadioButtonList rdblist, DropDownList ddlreject)
    {
        if (rdblist.SelectedIndex == -1)
        {
            ObjCommon.ShowAlertMessage("Select Accept/Reject");
            rdblist.Focus();
            return false;
        }
        else if (rdblist.SelectedValue == "R")
        {
            if (ddlreject.SelectedIndex == 0)
            {
                ObjCommon.ShowAlertMessage("Select Rejected Reasons");
                ddlreject.Focus();
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