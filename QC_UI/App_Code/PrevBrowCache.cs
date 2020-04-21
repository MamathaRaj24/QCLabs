using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for PrevBrowCache
/// </summary>
public class PrevBrowCache
{
    public static void enforceNoCache()
    {
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(60));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(true);
    }
    public static void enforceNoCacheClient(Page webPageInstance)
    {
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
        HttpContext.Current.Response.CacheControl = "no-cache";
        HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        HttpContext.Current.Response.Expires = -1;
        //webPageInstance.ClientScript.RegisterClientScriptBlock(this.GetType(), "signout", "DisableHistory()", true);
    }
    
}