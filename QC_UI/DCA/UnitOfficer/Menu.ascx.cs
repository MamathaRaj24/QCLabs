using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UnitOfficer_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String activepage = Request.RawUrl;

        if (activepage.Contains("Ack.aspx"))
            Ack_Unit.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("AllotToAnalyst.aspx"))
            Analyst_Unit.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ViewTestResult.aspx"))
            ResultLi.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintForm13.aspx"))
            ResultLi.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Printview_UO.aspx"))
            ResultLi.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("FreezeTestresult_UO.aspx"))
            ResultLi.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Dashboard.aspx"))
            Li1.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RA_ViewTestResult.aspx"))
            Reallot.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RA_Printview_UO.aspx"))
            Reallot.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RA_FreezeTestresult_UO.aspx"))
            Reallot.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RA_PrintForm13.aspx"))
            ResultLi.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintForm34.aspx"))
            ResultLi.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ChangePWD.aspx"))
            liC.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("SearchSamples.aspx"))
            Li2.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RA_SearchSamples.aspx"))
            Li2.Attributes.Add("class", "dropdown active");
    }
}