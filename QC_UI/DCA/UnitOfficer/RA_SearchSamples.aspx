<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RA_SearchSamples.aspx.cs" Inherits="DCA_UnitOfficer_RA_SearchSamples" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/UnitOfficer/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx"%>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Unit Officer-Search Samples </title>
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
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        
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
                               Re-alloted Search Samples</h3>
                        </div>
                        <div class="widget-content">
                            <div class="row">
                                <div class="col-10">
                                    <div id="radiobuttons" class="span12" runat="server">
                                        <div class="control-group">
                                            <div class="row">
                                                <div id="Div2" runat="server" class="col-12 text-right">
                                                    <span>
                                                        <asp:RadioButtonList CssClass="rbListWrap" ID="rdbMrgSta" runat="server" AutoPostBack="true"
                                                            RepeatDirection="Horizontal" 
                                                        onselectedindexchanged="rdbMrgSta_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">By Sample ID</asp:ListItem>
                                                            <asp:ListItem Value="2">By Category</asp:ListItem>
                                                            <asp:ListItem Value="3">Name Of the Drug</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </br> </br> </br>
                                <div class="col-12">
                                    <div runat="server" id="span4" class="">
                                        <div class="row">
                                            <div class="col col-4 text-right">
                                                <h4>
                                                    <label class=" form-control-label">
                                                        Search Sample ID
                                                    </label>
                                                </h4>
                                            </div>
                                            <div class=" col-4 text-left">
                                                <asp:DropDownList ID="ddlSampId" CssClass="form-control" runat="server" 
                                                    AutoPostBack="true" 
                                                    onselectedindexchanged="ddlSampId_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="span5" class="">
                                        <div class="row">
                                            <div class="col col-3 text-right">
                                                <h4>
                                                    <label class=" form-control-label">
                                                        Search Category
                                                    </label>
                                            </div>
                                            <div class=" col-3">
                                                <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" 
                                                    AutoPostBack="true" 
                                                    onselectedindexchanged="ddlCategory_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="span6" class="">
                                        <div class="row">
                                            <div class="col col-3 text-right">
                                                <h4>
                                                    <label class=" form-control-label">
                                                        Search Drug
                                                    </label>
                                            </div>
                                            <div class=" col-3">
                                                <asp:DropDownList ID="ddlDrug" CssClass="form-control" runat="server" 
                                                    AutoPostBack="true" onselectedindexchanged="ddlDrug_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-12 text-center">
                                            <asp:Button ID="BtnTR" CssClass="btn btn-primary" runat="server" ValidationGroup="tr"  Visible="false"
                                                Text="Get Data" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="widget-content">
                            <%-- <div class="widget-content">--%>
                            <asp:GridView ID="GvCourier" runat="server" CssClass="table table-striped table-bordered"
                                  AutoGenerateColumns="false" AllowPaging="true" 
                                onrowcommand="GvCourier_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("Sample_ID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("Category_Name") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryID" runat="server" Text='<%# Bind("Category_ID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trade Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTradename" runat="server" Text='<%# Bind("TradeName") %>'>
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
                                    <div id="Div1" class="row form-group" runat="server">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 table-responsive">
                                                <rsweb:ReportViewer ID="Rptform13analyst" runat="server" Width="1000%" Height="20px"
                                                    align="center" SizeToReportContent="true" onload="Rptform13analyst_Load" >
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
        </div>
        <</div>
        <Footer:footer ID="footer" runat="server" />
    </form>
</body>
</html>
