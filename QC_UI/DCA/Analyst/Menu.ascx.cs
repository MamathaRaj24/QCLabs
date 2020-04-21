using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Analyst_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String activepage = Request.RawUrl;

        if (activepage.Contains("Dashboard.aspx"))
            db.Attributes.Add("class", "active");

        if (activepage.Contains("Ack.aspx"))
            Ack_Unit.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RevertedSample.aspx"))
            Ack_Unit.Attributes.Add("class", "dropdown active");


        if (activepage.Contains("TstRslt.aspx".Trim()))
            Analyst_Unit.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("EditTestResult.aspx"))
            Analyst_Unit.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("FreezeTestResult.aspx"))
            Analyst_Unit.Attributes.Add("class", "dropdown active");



        if (activepage.Contains("RA_TestResult.aspx".Trim()))
            RA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RAEditTR.aspx"))
            RA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RAFreezeTR.aspx"))
            RA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Viewtestresultrpt.aspx"))
            Li3.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ViewSampleAllot.aspx"))
            Li3.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("SearchSamples.aspx"))
            Li3.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RAViewtestresultrpt.aspx"))
            Li3.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ChangePWD.aspx"))
            liC.Attributes.Add("class", "dropdown active");

    }
}