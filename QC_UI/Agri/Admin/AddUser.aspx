<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="Admin_AddUser" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Admin/AgriMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agri_Admin-Add Users</title>
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
        history.pushState(null, null, 'AddUser.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'AddUser.aspx');
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
    <script type="text/javascript" language="javascript">
        function Hidebutton() {
            $('#<%=BtnSave.ClientID %>').hide();
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
                                ADD USERS</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span6">
                                <div class="control-group">
                                    <label class="control-label">
                                        Select User Type (In Application)</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlRole" CssClass="form-control" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label">
                                        Select Designation</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlDesig" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span6">
                                <div class="control-group">
                                    <label class="control-label">
                                        Select Employee</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlEmployee" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div id="divlab" runat="server">
                                    <div class="control-group">
                                        <label class="control-label">
                                            Selected Lab
                                        </label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddllab" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="widget">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    Office Address</h3>
                            </div>
                            <div class="widget-content">
                                <div class="span6">
                                    <div class="control-group">
                                        <label class="control-label">
                                            District</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddldist" CssClass="form-control" required runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddldist_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span5">
                                    <div id="divdivision" runat="server">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Division</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddldivision" CssClass="form-control" required runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divmand" runat="server">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Mandal</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlmand" CssClass="form-control" required runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="span6">
                                    <div class="control-group">
                                        <label class="control-label">
                                            Address</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtaddress" TextMode="MultiLine" required CssClass="form-control"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtaddress_FilteredTextBoxExtender" runat="server"
                                                BehaviorID="txtaddress_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                TargetControlID="txtaddress">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="widget ">
                                <div class="widget-header">
                                    <i class="icon-user"></i>
                                    <h3 align="center">
                                        Login Details</h3>
                                </div>
                                <div class="widget-content">
                                    <fieldset>
                                        <div class="span6">
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Login User Name</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtUser" CssClass="form-control" MaxLength="50" required runat="server"
                                                        AutoPostBack="true" OnTextChanged="txtUser_TextChanged"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtUser_FilteredTextBoxExtender1" runat="server"
                                                        BehaviorID="txtUser_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                        TargetControlID="txtUser" ValidChars='-_'>
                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span5">
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Captcha</label>
                                                <div class="controls">
                                                    <asp:Image ID="Image1" runat="server" />
                                                    <asp:ImageButton ID="btnImgRefresh" runat="server" ImageUrl="~/Assets/img/RecaptchaLogo.png"
                                                        ToolTip="Refresh Captcha Text" OnClick="btnImgRefresh_Click" formnovalidate />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span6">
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Enter Captcha Text</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="captch" CssClass="form-control" runat="server" required class="captcha-field"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hfId" runat="server" />
                                        </div>
                                    </fieldset>
                                    <div class="span5">
                                        <div class="controls">
                                            <asp:Button ID="BtnSave" class="btn btn-primary" formnovalidate="formnovalidate"
                                                runat="server" Text="SAVE" OnClick="Save_Click" />
                                            <asp:Button ID="BtnUpdate" class="btn btn-primary" Visible="false" formnovalidate="formnovalidate"
                                                runat="server" Text="Update" OnClick="BtnUpdate_Click" />
                                            <asp:Label ID="lblmsg" runat="server" style="color:Red; font-weight:bold"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="widget-content">
                                <asp:GridView ID="GvUser" runat="server" CssClass="table table-striped table-bordered"
                                    GridLines="None" AutoGenerateColumns="False" OnRowCommand="GvUser_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblroleid" runat="server" Visible="false" Text='<%# Bind("Role") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Bind("id") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("RoleName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesigCode" runat="server" Visible="false" Text='<%# Bind("Designid") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblDesigName" runat="server" Text='<%# Bind("Designation") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpCode" runat="server" Visible="false" Text='<%# Bind("EmpCode") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("Name") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDistCode" runat="server" Visible="false" Text='<%# Bind("Dist_Cd") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblDistName" runat="server" Text='<%# Bind("DistName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lab">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLabCode" runat="server" Visible="false" Text='<%# Bind("Lab_ID") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblLabName" runat="server" Text='<%# Bind("LabName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Division">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivCode" runat="server" Visible="false" Text='<%# Bind("Zone_Cd") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblDivName" runat="server" Text='<%# Bind("ZoneName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mandal">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMandCode" runat="server" Visible="false" Text='<%# Bind("Mandal_Cd") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblMandName" runat="server" Text='<%# Bind("MandName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdd" runat="server" Text='<%# Bind("Address") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UserID to Login">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserCode" runat="server" Visible="false" Text='<%# Bind("User_Code") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblUserName" runat="server" style="color:Maroon; font-weight:bold" Text='<%# Bind("UserName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnedit" runat="server" formnovalidate CommandName="edt" Text="Edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDel" runat="server" CommandName="dlt" OnClientClick="return Confirm(this)"
                                                Text="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
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
