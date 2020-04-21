<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Logout</title>
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link href="../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        history.pushState(null, null, 'Logout.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Logout.aspx');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <div class="row">
                    <div class="span12">
                        <div class="span2">
                            <img src="../Assets/img/tg.png" alt="StateLogo" height="100px" width="100px" /></div>
                        <div class="span7">
                            <div class="span12">
                                <h3 class="brand">
                                    QUALITY CONTROL LABS</h3>
                            </div>
                            <div class="span12 ">
                                <h3 class="brand">
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
    <div class="container-fluid">
    </div>
    <br />
    <br />
    <br />
    <div class="container" style="text-align: center">
        <div class="col-md-12" align="center">
            <h3 class="form-signin-heading">
                LogOut</h3>
            <div class="login">
                <h1>
                    &nbsp;
                </h1>
                <div class="col-md-12 h6">
                    Successfully Logout.. Click Here to <a href="Login.aspx" class="h4">Login</a>
                </div>
            </div>
        </div>
    </div>
    <div class="clearfix">
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="row">
    </div>
    <br />
    <br />
    <br />
    <br />
    <div class="footer">
        <div class="footer-inner">
            <div class="container">
                <div class="row">
                    <div class="span12">
                        <div class="span6 left">
                        Site Designed,Developed And Hosted by National Informatics Centre (TS).<img src="../Assets/img/nic-logo.png"
                            alt="Nic Logo" />
                    </div>
                    <div class="span5 right">
                        Content Maintained and Updated by Drug Control Administration
                        <img src="../Assets/img/dca.png" />
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
