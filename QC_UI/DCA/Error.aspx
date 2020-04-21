<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="DCA_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/font-awesome.css" rel="stylesheet" />
    <link href="../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <div class="row">
                    <div class="span12">
                        <div class="span2">
                            <img src="../Assets/img/tg.png" alt="StateLogo" /></div>
                        <div class="span7">
                            <div class="span12">
                                <h3 class="brand">
                                    QUALITY CONTROL LABS</h3>
                            </div>
                            <div class="span12 ">
                                <h3 class="caption">
                                    Government Of Telangana</h3>
                            </div>
                        </div>
                        <div class="span2">
                            <img src="../Assets/img/qc.PNG" alt="Logo" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="span12">
                <div class="error-container">
                    <h1>
                        404</h1>
                    <h2>
                        Internal Error Occured.</h2>
                    <div class="error-details">
                        Sorry, an error has occured! Why not try going back to the <a href="Login.aspx">home
                            page</a> or perhaps try following!
                    </div>
                    <div class="error-actions">
                        <a href="Login.aspx" class="btn btn-large btn-primary"><i class="icon-chevron-left">
                        </i>&nbsp; Back to Login Page </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
