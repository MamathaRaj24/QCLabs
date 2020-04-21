<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>QC LABS | DRUG CONTROL ADMINISTRATION</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />    
    <link href="../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/css/style.css" rel="stylesheet" type="text/css" />

    <link rel="shortcut icon" type="image/png" href="../../Asets/images/favicon.png" />
    <script src="../Assets/js/signin.js" type="text/javascript"></script>
    <script src="../Assets/js/jquery.min.js" type="text/javascript"></script>
    <script src="../Assets/js/shaa256.js" type="text/javascript"></script>
    <script type="text/javascript">
        function validateReg() {
            var pwd = document.getElementById('txtPwd').value;
            if (pwd != "") {
                var keyGenrate = '<%=ViewState["KeyGenerator"]%>';
                var myval = sha256(keyGenrate);
                document.getElementById('txtPwdHash').value = '';
                var myval1 = sha256(document.getElementById('txtPwd').value.toString());
                document.getElementById('txtPwd').value = '******';
                document.getElementById('txtPwdHash').value = sha256(myval1 + myval);
            }
        } 
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'Login.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Login.aspx');
        });
    </script>
    <link href="../Assets/css/pages/signin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main_header">
        <div class="header">
            <div class="container-fluid" style="background-color: #1b72c8; color: White">
                <div class="logo_contact_info_part">
                    <div class="col-md-1 text-left">
                        <div class="logo">
                            <img src="../Assets/img/tg.png" alt="logo" style="height: 100px; width: 100px;" />
                        </div>
                    </div>
                    <div class="col-md-8  text-center">
                        <div class="cont_info" style="margin-top: -65px;">
                            <h1>
                                QUALITY CONTROL LABS</h1>
                            <br />
                            <h3>
                                Drug Control Administration, Government of Telangana</h3>
                        </div>
                    </div>
                    <div class="col-md-1 text-right">
                        <div class="">
                            <div class="book_now">
                                <img src="../Assets/img/qclogo.png" alt="logo" style="height: 110px; width: 110px;
                                    margin-top: -114px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="account-container">
        <div class="content clearfix">
            <h1>
                Department Login</h1>
            <div class="login-fields">
                <p>
                    Login with your credentials</p>
                <div class="field">
                    <label for="username">
                        Username</label>
                    <asp:TextBox ID="txtUname" runat="server" class="login username-field form-control"
                        name="username" placeholder="Username" required autofocus></asp:TextBox>
                </div>
                <div class="field">
                    <label for="password">
                        Password:</label>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" class="login password-field form-control"
                        name="password" placeholder="Password" required></asp:TextBox>
                </div>
                <div class="field">
                    <strong>
                        <asp:Label ID="lblmsg" CssClass="alert-danger" runat="server"></asp:Label></strong>
                    <asp:HiddenField ID="txtPwdHash" runat="server" />
                    <asp:HiddenField ID="hf" runat="server" />
                </div>
                <div class="login-actions">
                    <span class="login-checkbox">
                        <asp:Image ID="Image2" runat="server" />
                    </span>
                    <br />
                    <asp:ImageButton ID="btnImgRefresh" Height="40" Width="40" runat="server" ImageUrl="~/Assets/img/RecaptchaLogo.png"
                        ToolTip="Refresh Captcha Text" OnClick="btnImgRefresh_Click" formnovalidate /></div>
                <div class="field">
                    <asp:TextBox ID="captch" runat="server" class="captcha-field form-control" placeholder="Enter Captcha"></asp:TextBox>
                </div>
            </div>
            <div class="login-actions">
                <asp:Button ID="btnLogin" Width="100%" runat="server" Text="SIGN IN" class="button btn btn-primary btn-large"
                    OnClientClick="return validateReg();" Font-Bold="True" Font-Size="Large" OnClick="btnLogin_Click">
                </asp:Button>
            </div>
            <div class="clearfix">
            </div>
            <br />
            <div class="alert alert-info" style="text-align: center">
                <h3>
                    <a href="Default.aspx"><span><i class=" icon-home ">Back to Home</i></span></a></h3>
            </div>
        </div>
    </div>
    <div class="footer">
        <div class="footer-inner" style="background-color: #808080">
            <div class="container">
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
    </form>
</body>
</html>
