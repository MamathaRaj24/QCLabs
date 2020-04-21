<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Printview_UO.aspx.cs" Inherits="DCA_UnitOfficer_Printview_UO" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/UnitOfficer/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unit Officer - View Test Results</title>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600"
        rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="../../Assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
    </script>
    <style type="text/css">
        .Text-Bold
        {
            font-weight: bold;
            color: #3a87ad;
        }
    </style>
    <script type="text/javascript">
        history.pushState(null, null, 'Printview_UO.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Printview_UO.aspx');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <Header:header ID="header" runat="server" />
    <Menu:menu ID="menu" runat="server" />
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
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
                    <div class="widget">
                        <div class="widget-header">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                Print Form-13</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GvPrintForm" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="GvPrintForm_PageIndexChanging" OnRowCommand="GvPrintForm_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsampleid" runat="server" Text='<%# Bind("Sample_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcategoryname" runat="server" Text='<%# Bind("Category_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trade Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltrade" runat="server" Text='<%# Bind("TradeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TestCompleted Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltestcmpldate" runat="server" Text='<%# Bind("TestCompledOn") %>'></asp:Label>
                                            <asp:Label ID="lblstatus" runat="server" Visible="false" Text='<%# Bind("TotCompl") %>'></asp:Label>
                                            <asp:Label ID="lbls1testdone" runat="server" Visible="false" Text='<%# Bind("S1TestDone") %>'></asp:Label>
                                            <asp:Label ID="lbls2testdone" runat="server" Visible="false" Text='<%# Bind("S2TestDone") %>'></asp:Label>
                                            <asp:Label ID="lbls3testdone" runat="server" Visible="false" Text='<%# Bind("S3TestDone") %>'></asp:Label>
                                            <asp:Label ID="lbltestresult" runat="server" Visible="false" Text='<%# Bind("Testresult") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnPRint13" runat="server" CommandName="Form13" Text="Print Form "></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="DivViewData" runat="server">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    View Test Results</h3>
                                <asp:Label ID="lblsamplid" runat="server" CssClass="alert alert-info Text-Bold"></asp:Label>
                            </div>
                            <div id="Div1" class="row form-group" runat="server">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 table-responsive">
                                        <rsweb:ReportViewer ID="Rpt_PrintForm13" Width="100%" Height="20px" align="center"
                                            runat="server" SizeToReportContent="true" OnLoad="Rpt_PrintForm13_Load">
                                        </rsweb:ReportViewer>
                                    </div>
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
