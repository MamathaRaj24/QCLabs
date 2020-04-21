<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Logout</title>
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link href="../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
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
                     <div class="logo_contact_info_part">
                     <div class="row form-group">
                    <div class="col-md-2 text-left">
                    <div class="logo">
                         <img src="  ../Assets/img/tg.png"  alt="logo" style="height: 100px; width: 100px;" />
                    </div> 
                </div>
                <div class="col-md-8  text-center">
                    <div class="cont_info" style="margin-top: -65px;">
                        <h1>
                            QUALITY CONTROL LABS</h1>
                        <h3>
                           Department Of Agriculture, Government of Telangana</h3>
                    </div>
                </div>
                <div class="col-md-2 text-right">
                    <div class="">
                        <div class="book_now">
                           <img src="  ../Assets/img/qclogo.png"   alt="logo" style="height: 110px; width: 110px;   
                                margin-top: -100px;" />
                                  
                        </div>
                    </div>
                </div>
                    </div>
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
                        <div class="span6">
                            Site Designed,Developed And Hosted by National Informatics Centre (TS).
                        </div>
                        <div class="span5 right">
                            Content Maintained and Updated by Government of Telangana
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
