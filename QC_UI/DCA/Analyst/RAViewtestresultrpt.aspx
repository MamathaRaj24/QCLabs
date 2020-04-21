<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RAViewtestresultrpt.aspx.cs"
    Inherits="DCA_Analyst_RAViewtestresultrpt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Analyst/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Analyst - View Test Results</title>
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
                                View Test Results</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVTestResult" runat="server" CssClass="table table-striped table-bordered"
                                AutoGenerateColumns="False" OnRowCommand="GVTestResult_RowCommand" AllowPaging="true"
                                PageSize="10" OnPageIndexChanging="GVTestResult_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSampleid" Text='<%# Eval("Sample_ID") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbls1testdone" Visible="false" Text='<%# Eval("S1TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbls2testdone" Visible="false" Text='<%# Eval("S2TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbls3testdone" Visible="false" Text='<%# Eval("S3TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbltestresult" Visible="false" Text='<%# Eval("Testresult") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbla1testdone" Visible="false" Text='<%# Eval("A1TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbla2testdone" Visible="false" Text='<%# Eval("A2TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lbla3testdone" Visible="false" Text='<%# Eval("A3TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblb1testdone" Visible="false" Text='<%# Eval("B1TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblb2testdone" Visible="false" Text='<%# Eval("B2TestDone") %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblb3testdone" Visible="false" Text='<%# Eval("B3TestDone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Category" DataField="Category_Name" />
                                    <asp:BoundField HeaderText="Trade Name" DataField="TradeName" />
                                    <asp:BoundField HeaderText="Submited On" DataField="SubmittedOn" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" CommandName="ViewResult" formnovalidate Text="ViewResult" />
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
                            <div class="row form-group" runat="server">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 table-responsive">
                                        <rsweb:ReportViewer ID="Rptform13analyst" runat="server" Width="1000%" Height="20px"
                                            align="center" SizeToReportContent="true" OnLoad="Rptform13analyst_Load">
                                        </rsweb:ReportViewer>
                                    </div>
                                </div>
                            </div>
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
