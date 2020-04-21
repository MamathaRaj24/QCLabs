<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Agri_Admin_DashBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Admin/AgriMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DashBoard</title>
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
                        <div class="widget-header coler">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                Dash Board
                            </h3>
                        </div>
                        <div class="widget-content">
                            <div class=" form-group">
                                <div class="col-lg-2">
                                    Samples Registered<br />
                                    <img src="../../Assets/img/samreg.png" />
                                    <asp:Label ID="lblsamplesReg" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    Accepted<br />
                                    <img src="../../Assets/img/accepted.png" />
                                    <asp:Label ID="lblAccepted" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    Rejected<br />
                                    <img src="../../Assets/img/rejected.png" />
                                    <asp:Label ID="lblRejected" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    SamplesTested<br />
                                    <img src="../../Assets/img/SamplesTested.png" />
                                    <asp:Label ID="lblSamTested" runat="server"></asp:Label>
                                </div>
                                <label class="col-lg-2">
                                    As Per Norms<br />
                                    <img src="../../Assets/img/Norms.png" />
                                    <asp:Label ID="lblnoc" runat="server"></asp:Label>
                                </label>
                                <div class="col-lg-2">
                                    Not As Per Norms<br />
                                    <img src="../../Assets/img/NotNorms.png" />
                                    <asp:Label ID="lblnonc" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%-- <div class="span3">
                                <div class="control-group">
                                    <label class="control-label" for="ddlmemo">
                                        Samples Registered
                                    </label>
                                    <div class="controls">
                                        <img src="../../Asets/images/samregter.png" />
                                        <asp:Label ID="lblsamplesReg" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span3">
                                <div class="control-group">
                                    <label class="control-label" for="ddlsample">
                                        SamplesTested</label>
                                    <div class="controls">
                                        <img src="../../Asets/images/SamplesTested.png" />
                                        <asp:Label ID="lblSamTested" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span3">
                                <div class="control-group">
                                    <label class="control-label" for="ddlsample">
                                        As Per Norms</label>
                                    <div class="controls">
                                        <img src="../../Asets/images/Norms.png" />
                                        <asp:Label ID="lblnoc" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span3">
                                <div class="control-group">
                                    <label class="control-label" for="ddlsample">
                                        Not As Per Norms</label>
                                    <div class="controls">
                                        <img src="../../Asets/images/NotNorms.png" />
                                        <asp:Label ID="lblnonc" runat="server"></asp:Label>
                                    </div>
                                </div>--%>
                        </div>
                        <div class="page-header">
                            Sample Status
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVStaus" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" OnRowCommand="GVStaus_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="StatusId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusId" runat="server" Text='<%# Bind("status_id") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("status") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No.of Applications">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNop" runat="server" Text='<%# Bind("NOAP") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" CommandName="View" formnovalidate="formnovalidate"
                                                Text="VIEW" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Panel ID="SampleData" runat="server">
                        <div class="span12">
                            <div class="control-group">
                                <div class="controls">
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVSamples" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" OnRowCommand="GVSamples_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("RegID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Category Name" DataField="category_name" />
                                    <asp:BoundField HeaderText="Sample Name" DataField="SampleName" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnView" runat="server" CommandName="View" formnovalidate="formnovalidate"
                                                Text="VIEW" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
                <asp:Panel ID="Div1" runat="server">
                    <div class="span12">
                        <div class="control-group">
                            <div class="controls">
                                <asp:Label ID="lblSampleStatus" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="GVViewSmpl" runat="server" CssClass="table table-striped table-bordered"
                            GridLines="None" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Status" DataField="status" />
                                <asp:BoundField HeaderText="Status Update On" DataField="StatusUpdateon" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <Footer:footer ID="footer" runat="server" />
    </form>
</body>
</html>
