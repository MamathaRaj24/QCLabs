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

        if (activepage.Contains("Dashboard.aspx"))
            db.Attributes.Add("class", "active");

        if (activepage.Contains("Sample_Registration_DI.aspx"))
            Smpl_DCA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("EditSample_DI.aspx"))
            Smpl_DCA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Freeze_DI.aspx"))
            Smpl_DCA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("MemoGeneration.aspx"))
            liMemo.Attributes.Add("class", "active");

        if (activepage.Contains("CourierEntry.aspx"))
            Li1.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Samplebyhand.aspx"))
            Li1.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintMemoLetter.aspx"))
            Print_DCA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintForm1718.aspx"))
            Print_DCA.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintForm13.aspx"))
            Li3.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RAPrintForm13.aspx"))
            Li3.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("PrintForm34.aspx"))
            Li2.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("RAPrintForm34.aspx"))
            Li2.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ChangePWD.aspx"))
            liC.Attributes.Add("class", "dropdown active");

    }
}