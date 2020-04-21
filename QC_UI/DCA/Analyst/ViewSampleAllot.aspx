<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSampleAllot.aspx.cs"
    Inherits="DCA_Analyst_ViewSampleAllot" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Analyst/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Junior Analyst-Reverted Samples </title>
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
        function Confirm(link) {
            if (confirm("Are you sure to delete the row?")) {
                return true;
            }
            else
                return false;
        }

        $(document).ready(function () {
            $('#txtfromdt').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });

        $(document).ready(function () {
            $('#txtToDt').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
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
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
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
                                View Sample Allotted</h3>
                        </div>
                        <div class="widget-content">
                            <div class="row">
                                <div class="col-3">
                                    <div id="radiobuttons" class="span4" runat="server">
                                        <div class="control-group">
                                            <div class="row">
                                                <div id="Div2" runat="server" class="col-12">
                                                    <span>
                                                        <asp:RadioButtonList CssClass="rbListWrap" ID="rdbMrgSta" runat="server" AutoPostBack="true"
                                                            RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbMrgSta_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Latest 10</asp:ListItem>
                                                            <asp:ListItem Value="2">By Date</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div runat="server" id="span4" class="">
                                        <div class="row">
                                            <div class="col col-3">
                                                <h4>
                                                    <label class=" form-control-label">
                                                        From Date
                                                    </label>
                                                </h4>
                                            </div>
                                            <div class=" col-3">
                                                <asp:TextBox ID="txtfromdt" CssClass="form-control" AutoComplete="off" runat="server"
                                                    placeholder="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                            <div class="col col-3">
                                                <h4>
                                                    <label class=" form-control-label">
                                                        To Date
                                                    </label>
                                                </h4>
                                                <asp:HiddenField ID="hf" runat="server" />
                                            </div>
                                            <div class=" col-3">
                                                <asp:TextBox ID="txtToDt" CssClass="form-control" AutoComplete="off" runat="server"
                                                    placeholder="dd/mm/yyyy"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-12 text-center">
                                                <asp:Button ID="BtnTR" CssClass="btn btn-primary" runat="server" ValidationGroup="tr"
                                                    Text="Get Data" OnClick="BtnTR_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-3">
                                </div>
                            </div>
                            <br />
                            <asp:GridView ID="GvCourier" runat="server" CssClass="table table-striped table-bordered"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("SampleID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allotted_On">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("Analyst_Allotted_On") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ack">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("Analyst_Ack") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="clearfix">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <Footer:footer ID="footer" runat="server" />
    </form>
</body>
</html>
