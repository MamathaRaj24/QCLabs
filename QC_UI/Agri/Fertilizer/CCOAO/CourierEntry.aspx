﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourierEntry.aspx.cs" Inherits="Agri_Fertilizer_CCOAO_CourierEntry" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Fertilizer/CCOAO/Menu_CCOAO.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Fertilizer/CCOAO/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Fertilizer/CCOAO/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fertilizer-Postal Entry</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../../../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../../Assets/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../Assets/js/bootstrap.js"></script>
    <script type="text/javascript" src="../../../Assets/js/base.js"></script>
    <script type="text/javascript" src="../../../Assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        function SelectAllCheckboxes(chk) {
            $('#<%=GvCourier.ClientID%>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }
        $(".cbChild").change(function () {
            var all = $('.cbChild');
            if (all.length === all.find(':checked').length) {
                $(".cbAll").attr("checked", true);
            } else {
                $(".cbAll").attr("checked", false);
            }
        });       
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'CourierEntry.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'CourierEntry.aspx');
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.date').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-90:+0"
            });
        });
    </script>
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
                                    <img src="../../../Assets/img/processing.gif" alt="loading...." />
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
                                Sample Sent through Post</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GvCourier" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Memo Id">
                                        <ItemTemplate>
                                          
                                            <asp:Label ID="lblMemoId" runat="server" Text='<%# Bind("Memo_ID") %>'>
                                            </asp:Label>
                                             
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No of Sample">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnofsample" runat="server" Text='<%# Bind("NOS") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lab Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllabname" runat="server" Text='<%# Bind("LabName") %>'>
                                            </asp:Label>
                                             <asp:Label ID="lbllaballoted" Visible="false" runat="server" Text='<%# Bind("Lab_Alloted") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Courier/Post Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCourierName" AutoComplete="Off" CssClass="form-control" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dispatch Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDisDate" AutoComplete="Off" CssClass="date form-control" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="POD/Reference No">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPodNo" runat="server" AutoComplete="Off" CssClass="form-control"
                                                MaxLength="10"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Check All">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" class="cbAll" onclick="javascript:SelectAllCheckboxes(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelct" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="span6 offset3 text-center">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" CausesValidation="false" formnovalidate="formnovalidate"
                                    runat="server" Text="Save" OnClick="btnSave_Click" />
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
