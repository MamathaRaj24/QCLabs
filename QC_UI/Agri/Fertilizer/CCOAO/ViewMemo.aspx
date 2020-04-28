<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMemo.aspx.cs" Inherits="Agri_Fertilizer_CCOAO_ViewMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Fertilizer/CCOAO/Menu_CCOAO.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Fertilizer/CCOAO/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Fertilizer/CCOAO/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Memo</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../../../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../../Assets/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../../Assets/js/bootstrap.js" type="text/javascript"></script>
    <script src="../../../Assets/js/base.js" type="text/javascript"></script>
    <script src="../../../Assets/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../../Assets/js/jquery-ui.js" type="text/javascript"></script>
    <link href="../../../Assets/css/Jquery_UI.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        history.pushState(null, null, 'ViewMemo.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'ViewMemo.aspx');
        });
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
                                Acknowledge Memo Received</h3>
                        </div>
                        <div class="widget-content span12">
                            <asp:GridView ID="GVAck" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" OnRowCommand="GVAck_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Memo Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemoId" Text='<%# Eval("Memo_ID") %>' runat="server"> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accept/Reject">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="Rdbaceptrjctlist" RepeatDirection="Horizontal" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="Rdbaceptrjctlist_SelectedIndexChanged">
                                                <asp:ListItem Value="A">Accept</asp:ListItem>
                                                <asp:ListItem Value="R">Reject</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rejected Reason">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlrjctreson" CssClass=" form-control" Enabled="false" RepeatDirection="Horizontal"
                                                runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnAck" CssClass="btn btn-primary" Visible="false" CommandName="M" CausesValidation="false"
                                                runat="server" Text="Save" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lbltext" runat="server" Visible="false">No Details are available For Acknowledgement</asp:Label>
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
