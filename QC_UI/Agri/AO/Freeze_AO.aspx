﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Freeze_AO.aspx.cs" Inherits="AO_FreezeFertilizer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/AO/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AO-Fertilizer Registration</title>
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
    <script type="text/javascript" src="../../Assets/js/MonthPicker.js"></script>
    <script type="text/javascript">
        function SelectAllCheckboxes1(chk) {
            $('#<%=Gvfreeze.ClientID%>').find("input:checkbox").each(function () {
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
        history.pushState(null, null, 'Freeze_AO.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Freeze_AO.aspx');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                        <div class="widget-header">
                            <i class="icon-calendar-empty"></i>
                            <h3 align="center">
                                FREEZE SAMPLES</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span5">
                                <div class="control-group">
                                    <label class="radio-inline" for="username">
                                        Sample Category</label>
                                    <div class="controls example">
                                        <asp:RadioButtonList class="radio inline" ID="RblCategory" runat="server" AutoPostBack="True"
                                            RepeatDirection="Vertical" OnSelectedIndexChanged="RblCategory_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Panel ID="pnlfreeze" runat="server">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-tasks"></i>
                                <h3 align="center">
                                    Freeze
                                    <asp:Label ID="lblfreezename" runat="server"></asp:Label></h3>
                            </div>
                            <div class="widget-content">
                                <asp:GridView ID="Gvfreeze" runat="server" CssClass="table table-striped table-bordered"
                                    GridLines="None" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SlNo">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sample ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsmid" Text='<%# Bind("RegID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="SampleDate" HeaderText="Date of Sampling" />--%>
                                        <asp:BoundField DataField="StkRcvdDate" HeaderText="Date of Sampling" />
                                        <asp:BoundField DataField="Category_Name" HeaderText="Sample Category" />
                                        <asp:BoundField DataField="SampleTypeName" HeaderText="Sample Type" />
                                        <asp:BoundField DataField="SampleName" HeaderText="Sample" />
                                        <asp:BoundField DataField="ManfucaturerName" HeaderText="Manufactured By" />
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <input id="chkAll" class="cbAll" onclick="javascript:SelectAllCheckboxes1(this);"
                                                    runat="server" type="checkbox" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelct" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="span6 text-center">
                                <asp:Button ID="btnfreeze" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                                    Text="Freeze" OnClick="btnfreeze_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hf" runat="server" />
    <Footer:footer ID="footer" runat="server"></Footer:footer>
    </form>
</body>
</html>
