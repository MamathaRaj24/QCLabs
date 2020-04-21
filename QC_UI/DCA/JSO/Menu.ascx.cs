using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DCA_JSO_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String activepage = Request.RawUrl;

        if (activepage.Contains("ChangePWD.aspx"))
            liC.Attributes.Add("class", "dropdown active");
    }

}