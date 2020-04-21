<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabReport.aspx.cs" Inherits="DCA_CodingOfficer_LabReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/CodingOfficer/COMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coding Officer--Lab Report</title>
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
    <script type="text/javascript" src="../../Assets/js/MonthPicker.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript">

        history.pushState(null, null, 'LabReport.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'LabReport.aspx');
        });


        $(function () {
            $('input.monthpicker').monthpicker({ changeYear: true, minDate: "-5 Y", maxDate: "0 M" });
        });
        $(function () {
            $('input.monthpicker1').monthpicker({ changeYear: true, minDate: "-0 Y", maxDate: "+6 Y" });

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
    <form id="form1" runat="server">
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
                                Lab Report</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Select Lab
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlunit" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Sample Category
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlsamplecategory" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:UpdatePanel ID="uppanel" runat="server">
                                        <ContentTemplate>
                                            <label class="col-lg-2">
                                                Select Month
                                            </label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtdate" runat="server" required AutoComplete="off" AutoPostBack="true"
                                                    CssClass="form-control monthpicker" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                                <asp:Label ID="lblmonth" Visible="false" runat="server"></asp:Label>
                                                <asp:Label ID="lblyear" Visible="false" runat="server"></asp:Label>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group text-center offset2">
                                    <asp:Button ID="Btnget" Text="Get Data" runat="server" CssClass="btn btn-primary "
                                        OnClick="Btnget_Click" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div id="DivViewData" runat="server">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    Lab Report</h3>
                            </div>
                            <asp:Panel ID="panelreport" runat="server" ScrollBars="Both">
                                <div id="Div1" class="row form-group" runat="server">
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 table-responsive">
                                            <rsweb:ReportViewer ID="Rptform13analyst" runat="server" Width="1000%" Height="20px"
                                                align="center" SizeToReportContent="true" OnLoad="Rptform13analyst_Load">
                                            </rsweb:ReportViewer>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hf" runat="server" />
            <Footer:footer ID="footer" runat="server"></Footer:footer>
        </div>
    </div>
    </form>
</body>
</html>
