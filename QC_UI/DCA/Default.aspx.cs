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

public partial class DCA_Default : System.Web.UI.Page
{
    Master_BE objbe = new Master_BE();
    Reports rprt = new Reports();
    DataTable dt;
    string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCounts();
        }
    }
    protected void GetCounts()
    {

        dt = new DataTable();
        objbe.Action = "REG";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblRegCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "ACC";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblAcc.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "REJ";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblRej.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "UNIT";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblUnitCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "LAB";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblLabCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "TEST";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblTestCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "YES";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblYesCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "NO";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblNoCnt.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "CAT";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lblsamCat.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "PROTO";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lbltestproto.Text = dt.Rows[0][0].ToString();

        dt = new DataTable();
        objbe.Action = "PARAM";
        objbe.Dept = "1";
        dt = rprt.HomePageCnts(objbe, con);
        if (dt.Rows.Count > 0)
            lbltestPara.Text = dt.Rows[0][0].ToString();
    }
}