<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Specifications.aspx.cs" Inherits="Admin_Specifications" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Admin/AgriMenu.ascx"%>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agri_Admin-Sample Specifications</title>
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
        function Confirm(link) {
            if (confirm("Are you sure to delete the selected Specifications?")) {
                return true;
            }
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'Specifications.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Specifications.aspx.');
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
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <Header:header ID="header" runat="server" />
    <Menu:menu ID="menu" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                                SAMPLE SPECIFICATIONS</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Sample Category</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="ddlSamtype">
                                        Sample Type Name</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlSamtype" CssClass="form-control" runat="server" required
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSamtype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="txtSampName">
                                        Sample Name</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlsample" CssClass="form-control" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlsample_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="txtSampName">
                                        Physical Condition</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlPhyCond" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="PhyCond_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Powder"> Powder</asp:ListItem>
                                            <asp:ListItem Value="Liquid">Liquid</asp:ListItem>
                                            <asp:ListItem Value="Granuals">Granuals</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                        <asp:GridView ID="GVSpecifications" runat="server" CssClass="table table-striped table-bordered"
                            GridLines="None" AutoGenerateColumns="False" ShowFooter="true" 
                            OnRowCommand="GVSpecifications_RowCommand" 
                            >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgBtnRemove" runat="server" Width="30" CommandName="Remove"
                                            CommandArgument="" ImageUrl="~/Assets/img/minus.png" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="imgBtnAdd" runat="server" Width="30" OnClientClick="return Validate(this)"
                                            ImageUrl="~/Assets/img/plus.png" OnClick="imgBtnAdd_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parameter">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtParam" CssClass="form-control" MaxLength="250" runat="server" Text='<%# Eval("param") %>'></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtParam_FilteredTextBoxExtender" runat="server"
                                            BehaviorID="txtParam_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtParam" ValidChars=" .,()-'%">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Standard Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtStdVal" CssClass="form-control" MaxLength="80" runat="server"
                                            Text='<%# Eval("stv") %>'></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="StvExtender" FilterType="Numbers,Custom,UppercaseLetters, LowercaseLetters"
                                            runat="server" TargetControlID="txtStdVal" ValidChars=".-+ " />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="form-actions">
                            <asp:Button ID="BtnSave" class="btn btn-primary" formnovalidate="formnovalidate"
                                runat="server" Text="SAVE" OnClick="BtnSave_Click" />
                        </div>
                        <asp:GridView ID="GvSpec" runat="server" CssClass="table table-striped table-bordered"
                            GridLines="None" AutoGenerateColumns="False"  AutoGenerateEditButton="true" AutoGenerateDeleteButton="true"
                            onrowcancelingedit="GvSpec_RowCancelingEdit" onrowdeleting="GvSpec_RowDeleting" DataKeyNames="Checkid,Parameter_ID" 
                            onrowediting="GvSpec_RowEditing" onrowupdating="GvSpec_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parameter">
                                    <ItemTemplate>
                                        <asp:Label ID="lblparamid" runat="server" Visible="false" Text='<%# Eval("Parameter_ID") %>'></asp:Label>
                                        <asp:Label ID="lblParam" runat="server" Text='<%# Eval("Parameter") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtParam" CssClass="form-control" runat="server" Text='<%# Eval("Parameter") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Standard Value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStdVal" runat="server" Text='<%# Eval("StandardValue") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStdVal" CssClass="form-control" MaxLength="35" runat="server"
                                            Text='<%# Eval("StandardValue") %>'></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="StvExtender" FilterType="Numbers,Custom"
                                            runat="server" TargetControlID="txtStdVal" ValidChars="." />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
                        
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
