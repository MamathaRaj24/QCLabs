using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DeleteCookie
/// </summary>
public class DeleteCookie
{
	public DeleteCookie()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void DelCookie()
    {
        /*KILL COOKIE -- CHANGING EXPIRY DATE*/
        if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
        {
            HttpCookie myCookie = new HttpCookie("ASP.NET_SessionId");
            myCookie.Expires = DateTime.Now.AddDays(-10);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

    }
}