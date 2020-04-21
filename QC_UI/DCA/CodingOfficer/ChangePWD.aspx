<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePWD.aspx.cs" Inherits="ChangePWD" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/CodingOfficer/COMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QCLabs- CHANGE PASSWORD</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../Assets/js/bootstrap.js" type="text/javascript"></script>
    <script src="../../Assets/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../Assets/js/shaa256.js" type="text/javascript"></script>
    <style type="text/css">
        #message
        {
            display: none;
            background: #f1f1f1;
            color: #000;
            position: relative;
            padding: 20px;
        }
        
        #message p
        {
            padding: 2px 20px;
            font-size: 12px;
        }
        
        #check
        {
            display: none;
            background: #f1f1f1;
            color: #000;
            position: relative;
            padding: 20px;
        }
        
        #check p
        {
            padding: 2px 20px;
            font-size: 12px;
        }
        
        /* Add a green text color and a checkmark when the requirements are right */
        .valid
        {
            color: green;
        }
        
        .valid:before
        {
            position: relative;
            left: -35px;
            content: "✔";
        }
        
        /* Add a red text color and an "x" when the requirements are wrong */
        .invalid
        {
            color: red;
        }
        
        .invalid:before
        {
            position: relative;
            left: -35px;
            content: "✖";
        }
    </style>
    <script type="text/javascript">
        function validateReg() {
            var newpwd = document.getElementById('txtNpwd').value;
            var cpwd = document.getElementById('txtCpwd').value;
            if (newpwd == cpwd) {
                if (newpwd != '') {
                    var oldpwd = document.getElementById('txtOpwd').value;
                    document.getElementById('txtOldPwdHash').value = '';
                    document.getElementById('txtNewPwdHash').value = '';
                    var keyGenrate = '<%= ViewState["KeyGenerator"]%>';
                    var myval1 = sha256(oldpwd);
                    var myval = sha256(keyGenrate);
                    var myval2 = sha256(newpwd);
                    document.getElementById('txtOpwd').value = '**********';
                    document.getElementById('txtNpwd').value = '**********';
                    document.getElementById('txtCpwd').value = '**********';
                    document.getElementById('txtOldPwdHash').value = sha256(myval1 + myval);
                    document.getElementById('txtNewPwdHash').value = myval2;
                }
            } else {
                alert("New Password and Confirm Password should be same");
                return;
            }
        }       
    </script>
    <link href="../../Assets/css/pages/signin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <Header:header ID="header" runat="server" />
    <Menu:menu ID="menu" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="main">
        <div class="main-inner">
            <div class="container">
                <div class="row">
                    <div class="widget-header" style="margin-top: -30px;">
                        <i class="icon-pushpin"></i>
                        <h3>
                            Logged In As :
                            <asp:Label ID="lblUser" runat="server"></asp:Label>
                        </h3>
                        <h3 style="float: right">
                            Date:<asp:Label ID="lblDate" runat="server"></asp:Label>
                        </h3>
                    </div>
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <img src="../../Assets/img/processing.gif" alt="loading...." />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="widget ">
                        <div class="widget-header">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                CHANGE PASSWORD</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span6">
                                <div class="control-group">
                                    <label class="control-label">
                                        Old Password:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtOpwd" runat="server" CssClass="form-control" TextMode="Password"
                                            required></asp:TextBox>
                                        <asp:HiddenField ID="txtOldPwdHash" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="control-group">
                                    <label class="control-label">
                                        New Password:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtNpwd" runat="server" CssClass="form-control" TextMode="Password"
                                            required></asp:TextBox>
                                        <asp:HiddenField ID="txtNewPwdHash" runat="server" />
                                        <input type="checkbox" onclick="myFunction()" value="Show New Password" />
                                        Show New Password
                                        <asp:HiddenField ID="hf" runat="server" />
                                    </div>
                                </div>
                                <br />
                                <div class="control-group">
                                    <label class="control-label">
                                        Confirm Password:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtCpwd" runat="server" CssClass="form-control" TextMode="Password"
                                            required></asp:TextBox>
                                        <input type="checkbox" onclick="myFunction1()" />
                                        Show Confirm Password
                                    </div>
                                </div>
                                <div class="control-group">
                                    <span class="login-checkbox">
                                        <asp:Image ID="Image2" runat="server" />
                                    </span>
                                    <br />
                                    <asp:ImageButton ID="btnImgRefresh" Height="40" Width="40" runat="server" ImageUrl="~/Assets/img/RecaptchaLogo.png"
                                        ToolTip="Refresh Captcha Text" OnClick="btnImgRefresh_Click" formnovalidate /></div>
                                <div class="controls">
                                    <asp:TextBox ID="captch" runat="server" CssClass="form-control" placeholder="Enter Captcha"></asp:TextBox>
                                </div>
                            </div>
                            <div class="span5">
                                <div id="message">
                                    Password must contain the following:
                                    <p id="letter" class="invalid">
                                        A <b>lowercase</b> letter</p>
                                    <p id="capital" class="invalid">
                                        A <b>capital (uppercase)</b> letter</p>
                                    <p id="number" class="invalid">
                                        A <b>number</b></p>
                                    <p id="length" class="invalid">
                                        Minimum <b>8 characters</b></p>
                                    <p id="spl" class="invalid">
                                        A <b>Special character</b></p>
                                </div>
                                <div id="check">
                                    <p id="P1" class="invalid">
                                        New Password and Confirm Password Must be <b>Same</b></p>
                                </div>
                            </div>
                        </div>
                        <div class="login-actions" align="left">
                            <asp:Button ID="Button1" runat="server" Text="CHANGE PASSWORD" class=" btn btn-primary"
                                OnClientClick="return validateReg();" Font-Bold="True" OnClick="Button1_Click">
                            </asp:Button>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <Footer:footer ID="footer" runat="server"></Footer:footer>
    <script type="text/javascript">
        var myInput = document.getElementById("txtNpwd");
        var letter = document.getElementById("letter");
        var capital = document.getElementById("capital");
        var number = document.getElementById("number");
        var length = document.getElementById("length");
        var spl = document.getElementById("spl");

        var cpwd = document.getElementById("txtCpwd");

        cpwd.onfocus = function () {
            document.getElementById("check").style.display = "block";
        }
        cpwd.onblur = function () {
            document.getElementById("check").style.display = "none";
        }
        cpwd.onkeyup = function () {
            if (myInput.value == cpwd.value) {
                P1.classList.remove("invalid");
                P1.classList.add("valid");
            } else {
                P1.classList.remove("valid");
                P1.classList.add("invalid");
            }
        }

        // When the user clicks on the password field, show the message box
        myInput.onfocus = function () {
            document.getElementById("message").style.display = "block";
        }

        // When the user clicks outside of the password field, hide the message box
        myInput.onblur = function () {
            document.getElementById("message").style.display = "none";
        }

        // When the user starts to type something inside the password field
        myInput.onkeyup = function () {
            // Validate lowercase letters
            var lowerCaseLetters = /[a-z]/g;
            if (myInput.value.match(lowerCaseLetters)) {
                letter.classList.remove("invalid");
                letter.classList.add("valid");
            } else {
                letter.classList.remove("valid");
                letter.classList.add("invalid");
            }

            // Validate capital letters
            var upperCaseLetters = /[A-Z]/g;
            if (myInput.value.match(upperCaseLetters)) {
                capital.classList.remove("invalid");
                capital.classList.add("valid");
            } else {
                capital.classList.remove("valid");
                capital.classList.add("invalid");
            }

            // Validate numbers
            var numbers = /[0-9]/g;
            if (myInput.value.match(numbers)) {
                number.classList.remove("invalid");
                number.classList.add("valid");
            } else {
                number.classList.remove("valid");
                number.classList.add("invalid");
            }

            // Validate length
            if (myInput.value.length >= 8) {
                length.classList.remove("invalid");
                length.classList.add("valid");
            } else {
                length.classList.remove("valid");
                length.classList.add("invalid");
            }

            //valid special characters
            var splchars = /[@ # $ % & *]/g;
            if (myInput.value.match(splchars)) {
                spl.classList.remove("invalid");
                spl.classList.add("valid");
            } else {
                spl.classList.remove("valid");
                spl.classList.add("invalid");
            }
        }

        function myFunction() {
            var x = document.getElementById("txtNpwd");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }

        function myFunction1() {
            var x = document.getElementById("txtCpwd");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>
    </form>
</body>
</html>
