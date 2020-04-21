<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Freeze_TargetAllotment.aspx.cs"
    Inherits="Agri_Admin_Freeze_TargetAllotment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Admin/AgriMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Freeze Target Allotmnet</title>
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
    <script type="text/javascript">
        history.pushState(null, null, 'Freeze_TargetAllotment.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Freeze_TargetAllotment.aspx');
        });
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        function SelectAllCheckboxes1(chk) {

            $('#<%=GVDistAllot.ClientID%>').find("input:checkbox").each(function () {
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
    <script type="text/javascript" language="javascript">
        function Hidebutton() {
            if (confirm("Are you sure to Freeze ?, Once freezed data con not be updated.")) {
                return true;
            }
            else
                return false;
            $('#<%=btnFreeze.ClientID %>').hide();
        }
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
                                Target Allotment-Freeze</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Year</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlYear" CssClass="form-control " Height="35px" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Category</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlCategory" CssClass="form-control " Height="35px" runat="server"
                                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="widget-content" runat="server" id="gridid">
                            <asp:GridView ID="GVDistAllot" CssClass="table table-striped table-bordered" runat="server"
                                AutoGenerateColumns="false" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SlNo">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYear" Text='<%# Eval("Year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCatcode" Text='<%# Eval("SampleCategory") %>' Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lblCat" Text='<%# Eval("Category_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbldistcode" Text='<%# Eval("District") %>' Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbldist" Text='<%# Eval("DistName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alloted Quantity(in Qtls)">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAllotedId" Text='<%# Eval("Allot_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Target")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                            <div class="span12 offset5">
                                <div class="control-group">
                                    <asp:Button ID="btnFreeze" runat="server" class="btn btn-primary" Text="Freeze" OnClick="FreezeAllotments"
                                        OnClientClick="return Hidebutton();" />
                                    <asp:HiddenField runat="server" ID="HiddenField1" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-content" runat="server" id="FrrezedGridDiv">
                            <asp:GridView ID="GVFreezed" CssClass="table table-striped table-bordered" runat="server"
                                AutoGenerateColumns="false" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SlNo">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Year">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblYear" Text='<%# Eval("Year") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCatcode" Text='<%# Eval("SampleCategory") %>' Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lblCat" Text='<%# Eval("Category_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbldistcode" Text='<%# Eval("District") %>' Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lbldist" Text='<%# Eval("DistName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Alloted Quantity(in Qtls)">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAllotedId" Text='<%# Eval("Allot_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Target")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
