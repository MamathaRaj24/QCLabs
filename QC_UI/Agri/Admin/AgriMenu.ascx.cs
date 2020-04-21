using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AgriAdmin_AgriMenu : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        String activepage = Request.RawUrl;
        //usermanagment
        if (activepage.Contains("DashBoard.aspx"))
            db.Attributes.Add("class", "active");

        //User
        if (activepage.Contains("Designation.aspx"))
            liagri.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("Employee_Mst.aspx"))
            liagri.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("AddUser.aspx"))
            liagri.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("UserList.aspx"))
            liagri.Attributes.Add("class", "dropdown active");

        //Fertlizer masters
        if (activepage.Contains("SampleCategory.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("SampleType.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("Sample.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");



        //Target
        if (activepage.Contains("TargetAllotment.aspx"))
            TargetLi.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("Edit_TargetAllotment.aspx"))
            TargetLi.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("Freeze_TargetAllotment.aspx"))
            TargetLi.Attributes.Add("class", "dropdown active");


        //lab master
        if (activepage.Contains("AddLabs.aspx"))
            labli.Attributes.Add("class", "dropdown active");
        if (activepage.Contains("EquipmentMaster.aspx"))
            labli.Attributes.Add("class", "dropdown active");




        if (activepage.Contains("../ChangePWD.aspx"))
            Li2.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Specifications.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");

        
    }
    
}