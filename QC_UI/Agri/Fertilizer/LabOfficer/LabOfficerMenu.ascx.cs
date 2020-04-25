using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Agri_Fertilizer_LabOfficer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String activepage = Request.RawUrl;

        if (activepage.Contains("Dashboard.aspx"))
            db.Attributes.Add("class", "active");

        if (activepage.Contains("EquipmentDetails.aspx"))
            Li1.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Ack.aspx"))
            Li2.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("AddAnalyst.aspx"))
            Li3.Attributes.Add("class", "dropdown active"); 
    }
}