﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Agri_CodingCenter_Menu_AO : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String activepage = Request.RawUrl;

        if (activepage.Contains("DashBoard.aspx"))
            db.Attributes.Add("class", "active");

        if (activepage.Contains("Ack.aspx"))
            liAck.Attributes.Add("class", "active");
    }
}