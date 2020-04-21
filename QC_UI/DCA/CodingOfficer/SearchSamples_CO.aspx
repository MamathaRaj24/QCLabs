<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchSamples_CO.aspx.cs"
    Inherits="DCA_CodingOfficer_SearchSamples_CO" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/CodingOfficer/COMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coding Officer-Search Samples </title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600"
        rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../Assets/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Assets/js/bootstrap.js"></script>
    <script type="text/javascript" src="../../Assets/js/base.js"></script>
    <link href="../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript" src="../../Assets/js/base.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/dataTables.jqueryui.css" />
    <script type="text/javascript" src="../../Assets/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/footable.min.css" />
    <script type="text/javascript" src="../../Assets/js/footable.min.js"></script>
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        function Confirm(link) {
            if (confirm("Are you sure to delete the row?")) {
                return true;
            }
            else
                return false;
        }

        $(document).ready(function () {
            $('#GvCourier').dataTable({
                "bLengthChange": true,
                "paging": true,
                "sPaginationType": "full_numbers",
                "aLengthMenu": [[25, 50, 75, -1], [25, 50, 75, "All"]],                   //For Different Paging  Style
                // "scrollY": 400,                                     // For Scrolling
                "jQueryUI": true                                      //Enabling JQuery UI(User InterFace)
            });

        });
    </script>
    <style type="text/css">
        /*.rbListWrap {
            width: 500px;
         }*/
        
        .rbListWrap tr td
        {
            height: 20px;
            vertical-align: middle;
            padding: 5px;
            width: 33%;
        }
        
        .rbListWrap input
        {
            float: left;
        }
        
        .rbListWrap label
        {
            position: relative;
            padding-left: 20px;
        }
    </style>
    <style type="text/css">
        .ui-widget-header
        {
            border: 1px solid #4aa078;
            background: #4aa078 url(images/ui-bg_gloss-wave_35_f6a828_500x100.png) 50% 50% repeat-x;
            color: #ffffff;
            font-weight: bold;
        }
        .ui-state-highlight, .ui-widget-content .ui-state-highlight, .ui-widget-header .ui-state-highlight
        {
            border: 1px solid #4aa078;
            background: #4aa078 url("images/ui-bg_highlight-soft_75_ffe45c_1x100.png") 50% top repeat-x;
            color: #363636;
        }
        .ui-state-hover, .ui-widget-content .ui-state-hover, .ui-widget-header .ui-state-hover, .ui-state-focus, .ui-widget-content .ui-state-focus, .ui-widget-header .ui-state-focus
        {
            border: 1px solid #4aa078;
            background: #4aa078 url("images/ui-bg_glass_100_fdf5ce_1x400.png") 50% 50% repeat-x;
            font-weight: bold;
            color: black;
        }
    </style>
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
                                Search Samples</h3>
                        </div>
                        <div class="widget-content">
                            <div class="row">
                                <div class="col-10">
                                    <div id="radiobuttons" class="span12" runat="server">
                                        <div class="control-group">
                                            <div class="row">
                                                <div id="Div2" runat="server" class="col-4">
                                                    <span>
                                                        <asp:RadioButtonList CssClass="rbListWrap" ID="rdbMrgSta" runat="server" AutoPostBack="true"
                                                            RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbMrgSta_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">By DI</asp:ListItem>
                                                            <asp:ListItem Value="2">By Batch</asp:ListItem>
                                                            <asp:ListItem Value="3">By Memo</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </br> </br> </br>
                                <div class="col-12">
                                    <div runat="server" id="span1" class="">
                                        <div class="row">
                                            <div class="col col-3">
                                                <h4>
                                                    <label class="form-control-label text-right">
                                                        Search DI
                                                    </label>
                                            </div>
                                            <div class=" col-3">
                                                <asp:DropDownList ID="ddlDiName" CssClass="form-control" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlDiName_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="Span2" class="">
                                        <div class="row">
                                            <div class="col col-3">
                                                <h4>
                                                    <label class=" form-control-label text-right">
                                                        Search Batch
                                                    </label>
                                            </div>
                                            <div class=" col-3">
                                                <asp:DropDownList ID="ddlbatch" CssClass="form-control" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlbatch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="span3" class="">
                                        <div class="row">
                                            <div class="col col-3">
                                                <h4>
                                                    <label class=" form-control-label text-right">
                                                        Search Memo
                                                    </label>
                                            </div>
                                            <div class=" col-3">
                                                <asp:DropDownList ID="ddlmemo" CssClass="form-control" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlmemo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlgird" runat="server" ScrollBars="Both" Visible="false">
                            <div class="widget-content">
                                <%-- <div class="widget-content">--%>
                                <asp:GridView ID="GvCourier" runat="server" CssClass="table table-striped table-bordered"
                                    OnRowCommand="GvCourier_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                                    OnPageIndexChanging="GvCourier_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Zone Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblzonename" runat="server" Text='<%# Bind("ZoneName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sample Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("Sample_ID") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Memo Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmemoid" runat="server" Text='<%# Bind("Memo_ID") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("Category_Name") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trade Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTradename" runat="server" Text='<%# Bind("TradeName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Generic Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgnrcname" runat="server" Text='<%# Bind("GenericName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Batch Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbatchno" runat="server" Text='<%# Bind("BatchNo") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lab Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllabname" runat="server" Text='<%# Bind("LabName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Analyst Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblanalystname" runat="server" Text='<%# Bind("Name") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Test start Dt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTest_startDt" runat="server" Text='<%# Bind("Test_start_Dt") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Test End Dt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTest_endDt" runat="server" Text='<%# Bind("Test_End_Dt") %>'>
                                     
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="BtnView" runat="server" CommandName="ViewResult" Text="ViewResult" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="clearfix">
                                </div>
                                <div id="DivViewData" runat="server">
                                    <div class="widget ">
                                        <div class="widget-header">
                                            <i class="icon-user"></i>
                                            <h3 align="center">
                                                View Test Results</h3>
                                            <asp:Label ID="lblsamplid" runat="server" CssClass="alert alert-info Text-Bold"></asp:Label>
                                        </div>
                                        <div id="Div1" class="row form-group" runat="server" visible="false">
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
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <Footer:footer ID="footer" runat="server" />
    </form>
</body>
</html>
