using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using QC_DL;
using QC_BE;

public partial class Home : System.Web.UI.Page
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
            GetCounts();
        }
    }
    protected void BindDept()
    {
        dt = new DataTable();
        objbe.Action = "DEPT";
        dt = objm.Getdetails(objbe,con);
        cf.BindDropDownLists(ddldept, dt, "Dept_Des", "Dept_Code", "Select Department");
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldept.SelectedItem.Text.Trim() == "Drug Control Administration")
        {
            Response.Write("<script type='text/javascript'>");
            Response.Write("window.open('DCA/Default.aspx','_blank');");
            Response.Write("</script>");
        }
        if (ddldept.SelectedItem.Text.Trim() == "Department Of Agriculture")
        {
            Response.Write("<script type='text/javascript'>");
            Response.Write("window.open('Agri/Default.aspx','_blank');");
            Response.Write("</script>");
        }
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
}