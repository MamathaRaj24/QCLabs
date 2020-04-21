<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ack.aspx.cs" Inherits="Agri_Fertilizer_CCOAO_Ack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Fertilizer/CCOAO/Menu_CCOAO.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Fertilizer/CCOAO/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Fertilizer/CCOAO/Footer.ascx"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acknowledgement</title>
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
        function SelectAllCheckboxes(chk) {
            $('#<%=GVAck.ClientID%>').find("input:checkbox").each(function () {
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

        history.pushState(null, null, 'Ack.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Ack.aspx');
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.RDate').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-0:+10",
                changedate:false, 
            });
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
                                    <img src="~/Assets/img/processing.gif" alt="loading...." />
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
                                Acknowledge Samples Received</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVAck" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False">
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
                                    <asp:TemplateField HeaderText="Reciept Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRecieptDt" CssClass="RDate form-control" AutoComplete="Off" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Check All">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelct" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-12 text-center">
                                <asp:Label ID="lbltext" runat="server" Visible="false">No Details are available For Acknowledgement</asp:Label>
                                <asp:Button ID="btnAck" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                                    Text="Generate Acknowledgement" OnClick="btnAck_Click1" />
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
