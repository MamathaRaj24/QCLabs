using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
              
        String activepage = Request.RawUrl;

        if (activepage.Contains("DashBoard.aspx"))
            db.Attributes.Add("class", "active");

        if (activepage.Contains("Designation.aspx"))
            liDca.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Zone_Mst.aspx"))
            liDca.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Employee_Mst.aspx"))
            liDca.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("SampleCategory.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("SampleType.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Sample.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("AddLabs.aspx"))
            lab.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Specifications.aspx"))
            Smpl.Attributes.Add("class", "dropdown active");


        if (activepage.Contains("AddUser.aspx"))
            usr.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("EditUser.aspx"))
            usr.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("Userlist.aspx"))
            usr.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ResetPwd.aspx"))
            usr.Attributes.Add("class", "dropdown active");
    }
}