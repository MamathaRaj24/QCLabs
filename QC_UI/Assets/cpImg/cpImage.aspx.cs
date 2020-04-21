using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;

public partial class cpImg_cpImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["randomNo"] != null)
        {
            string text = Request.QueryString["randomNo"].ToString();
            int captchaWidth = 200;
            int captchaHeight = 40;
            Captcha.caseSensitive = false;
            Captcha ci = new Captcha();
            ci.Captchaimage(captchaWidth, captchaHeight, text);
            Response.Clear();
            Response.ContentType = "image/png";
            ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg);
            ci.Dispose();
        }
        else
        {
            Response.Redirect("~/Error.aspx");
        }

    }
}