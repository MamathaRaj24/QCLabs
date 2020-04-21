using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QC_DL;
using QC_BE;
using System.Data;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    Masters objm = new Masters();
    Master_BE objbe = new Master_BE();
    CommonFuncs cf = new CommonFuncs();
    Reports rprt = new Reports();
    DataTable dt;
    string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDept();
            //GetCounts();
        }
    }
    protected void BindDept()
    {
        dt = new DataTable();
        objbe.Action = "DEPT";
        dt = objm.Getdetails(objbe, con);
        BindDropDownLists(ddldept, dt, "Dept_Des", "Dept_Code", "Select Department");
    }
    public void BindDropDownLists(DropDownList ddl, DataTable ddt, string textfield, string valuefield, string strDefaultValue)
    {


        // if (ds.Tables.Count > 0)
        //{
        // if (ds.Tables[0].Rows.Count > 0)
        // {
        ddl.Items.Clear();
        ddl.DataSource = ddt;
        ddl.DataTextField = textfield;
        ddl.DataValueField = valuefield;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select Department", ""));
        ////ddl.SelectedIndex = 0;
        // }
    }
    protected void GetCounts()
    {
        dt = new DataTable();
        objbe.Action = "REG";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblRegCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "ACC";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblAcc.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "REJ";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblRej.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "UNIT";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblUnitCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "TEST";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblTestCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "YES";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblYesCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "NO";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblNoCnt.Text = dt.Rows[0][0].ToString();
    }
   
 
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldept.SelectedItem.Text.Trim() == "Drug Control Administration")
        {
            Response.Redirect("DCA/Default.aspx");
            //Response.Write("<script type='text/javascript'>");
            //Response.Write("window.open('DCA/Default.aspx','_blank');");
            //Response.Write("</script>");
        }
        if (ddldept.SelectedItem.Text.Trim() == "Department Of Agriculture")
        {
            Response.Redirect("Agri/Default.aspx");
            //Response.Write("<script type='text/javascript'>");
            // Response.Write("</script>");
            //Response.Write("window.open('Agri/Default.aspx','_blank');");
        }
    }
}