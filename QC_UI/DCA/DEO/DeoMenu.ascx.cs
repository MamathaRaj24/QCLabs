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

        //if (Session["Department"].ToString() == "1")
        //{
        //    Print_Agri.Visible = false;
        //    Smpl_Agri.Visible = false;

        //    Print_DCA.Visible = true;
        //    Smpl_DCA.Visible = true;
        //}

        //if (Session["Department"].ToString() == "2")
        //{
        //    Print_DCA.Visible = false;
        //    Smpl_DCA.Visible = false;

        //    Print_Agri.Visible = true;
        //    Smpl_Agri.Visible = true;
        //}

        String activepage = Request.RawUrl;



        if (activepage.Contains("CourierAck.aspx"))
            ack.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("EnterSampleDetails.aspx"))
            smpl.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ChangePWD.aspx"))
            smpl.Attributes.Add("class", "dropdown active");

        if (activepage.Contains("ChangePWD.aspx"))
            liC.Attributes.Add("class", "dropdown active");
    }
}