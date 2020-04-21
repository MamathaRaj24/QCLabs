using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspector_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        String activepage = Request.RawUrl;

        if (activepage.Contains("DashBoard.aspx"))
            db.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ViewSamples.aspx"))
            viewLi.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("GenerateStickers.aspx"))
            code.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Qrcodeprint.aspx"))
            code.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("AllotToLab.aspx"))
            allot.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ViewTextReslt.aspx"))
            test.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintForm13.aspx"))
            test.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintForm34.aspx"))
            test.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RAPrintForm13.aspx"))
            test.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RAPrintForm34.aspx"))
            test.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("LabReport.aspx"))
            test.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("SearchSamples_CO.aspx"))
            test.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ChangePWD.aspx"))
            liC.Attributes.Add("class", "dropdown active");
    }
}