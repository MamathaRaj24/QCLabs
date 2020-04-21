using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AO_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String activepage = Request.RawUrl;

        if (activepage.Contains("DashBoard.aspx"))
            db.Attributes.Add("class", "active");

        if (activepage.Contains("SampleRegistration_AO.aspx"))
            Smpl_Agri.Attributes.Add("class", "dropdown active");


        if (activepage.Contains("Ferti_Reg.aspx"))
            Smpl_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("EditFertilizer.aspx"))
            Smpl_Agri.Attributes.Add("class", "dropdown active");


        if (activepage.Contains("SeedRegistrationAO.aspx"))
            Smpl_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("EditFertilizer.aspx"))
            Smpl_Agri.Attributes.Add("class", "dropdown active");


        if (activepage.Contains("EditSample_AO.aspx"))
            Smpl_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Freeze_AO.aspx"))
            Smpl_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("MemoGeneration.aspx"))
            liMemo.Attributes.Add("class", "active");

        if (activepage.Contains("CourierEntry.aspx"))
            li1.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintCourier.aspx"))
            Print_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintMemoLetter.aspx"))
            Print_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("PrintformJ.aspx"))
            Print_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("FormP.aspx"))
            Print_Agri.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("FormK.aspx"))
            Print_Agri.Attributes.Add("class", "dropdown active");

       
    }
}