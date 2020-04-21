<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateStickers.aspx.cs"
    Inherits="CodingOfficer_GenerateStickers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/CodingOfficer/COMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Coding Officer-GenerateStickers</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../Assets/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Assets/js/bootstrap.js"></script>
    <script type="text/javascript" src="../../Assets/js/base.js"></script>
    <script type="text/javascript">
        history.pushState(null, null, 'GenerateStickers.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'GenerateStickers.aspx');
        });
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <Header:header ID="header" runat="server" />
    <Menu:menu ID="menu" runat="server" />
    <div class="main">
        <div class="main-inner">
            <div class="container" style="margin-top: -30px;">
                <div class="widget-header">
                    <i class="icon-pushpin"></i>
                    <h3>
                        Logged In As :
                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                    </h3>
                    <h3 style="float: right">
                        Date:<asp:Label ID="lblDate" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="row">
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <img src="../Assets/img/processing.gif" alt="loading...." />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="widget ">
                        <div class="widget-header coler">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                GENERATE STICKER
                            </h3>
                        </div>
                        <div class="widget-content">
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlsample">
                                        Select Sample Id
                                    </label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlsample" CssClass="form-control" required runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlsample">
                                    </label>
                                    <div class="controls">
                                        <asp:Button ID="btngeneratecode" runat="server" Text="GenarateCode" CssClass="btn btn-primary"
                                            OnClick="btngeneratecode_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="span12">
                               
                                <div class="span3">
                                    <div class="span4">
                                        <asp:ImageButton ID="btnImgprint" runat="server" Width="20%" formnovalidate Visible="false" ImageUrl="~/Assets/img/print-button.png"
                                            OnClick="btnImgprint_Click1" />
                                    </div>
                                </div>
                                <div class="span12">
                                    <rsweb:ReportViewer ID="Rpt_Sticker" Width="100%" Height="20px" align="center" runat="server"
                                        SizeToReportContent="true" onload="Rpt_Sticker_Load">
                                    </rsweb:ReportViewer>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hf" runat="server" />
    <Footer:footer ID="footer" runat="server"></Footer:footer>
    </form>
</body>
</html>
