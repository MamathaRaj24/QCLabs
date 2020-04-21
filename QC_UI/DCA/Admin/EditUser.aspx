<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditUser.aspx.cs" Inherits="DI_Admin_EditUser" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Admin/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin-UpdateUsers</title>
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
             if (confirm("Are you sure to delete the selected User?")) {
                 return true;
             }
             else
                 return false;
         }
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'EditUser.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'EditUser.aspx');
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
                        <div class="widget-header">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                User List</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVUsers" runat="server" CssClass="table table-bordered table-hover"
                                GridLines="None" AutoGenerateColumns="False" 
                                onrowcommand="GVUsers_RowCommand" >
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>' />
                                            <asp:Label ID="lblid" Text='<%# Eval("id") %>' Visible="false" runat="server"> </asp:Label>
                                            <asp:Label ID="lblUserCode" Text='<%# Eval("User_Code") %>' Visible="false" runat="server"> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role">
                                        <ItemTemplate>
                                            <asp:Label ID="lblroleid" Visible="false" runat="server" Text='<%# Bind("Role") %>' />
                                            <asp:Label ID="lblRole" runat="server" Text='<%# Bind("RoleName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstate" Visible="false" runat="server" Text='<%# Bind("Statecode") %>' />
                                            <asp:Label ID="lblstatenm" runat="server" Text='<%# Bind("StateNm") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Zone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblzoneid" Visible="false" runat="server" Text='<%# Bind("Zone_Cd") %>' />
                                            <asp:Label ID="lblZone" runat="server" Text='<%# Bind("ZoneName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lab Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllabid" Visible="false" runat="server" Text='<%# Bind("Lab_ID") %>' />
                                            <asp:Label ID="lbllab" runat="server" Text='<%# Bind("LabName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesid" Visible="false" runat="server" Text='<%# Bind("Designid") %>' />
                                            <asp:Label ID="lbldesgname" runat="server" Text='<%# Bind("Designation") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emp Name" Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblempcode" Visible="false" runat="server" Text='<%# Bind("EmpCode") %>' />
                                            <asp:Label ID="lblempname" runat="server" Text='<%# Bind("EmployeeName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="District Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldistcode" Visible="false" runat="server" Text='<%# Bind("Dist_Cd") %>' />
                                            <asp:Label ID="lbldistname" runat="server" Text='<%# Bind("DistName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Mandal Name" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmandcode" Visible="false" runat="server" Text='<%# Bind("Mandal_Cd") %>' />
                                            <asp:Label ID="lblmandanme" runat="server" Text='<%# Bind("MandName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Address" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbladdress"  runat="server" Text='<%# Bind("Address") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UserName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" CssClass="text-primary" runat="server" Text='<%# Bind("UserName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnedit" runat="server" formnovalidate CommandName="edt" Text="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDel" runat="server" formnovalidate CommandName="dlt" OnClientClick="return Confirm(this)"
                                                Text="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:Panel ID="pnluser" runat="server" Visible="false">
                         <div class="widget-header">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                ADD USERS</h3>
                        </div>
                        <div class="widget-content" id="upuserid" runat="server" >
                            <div class="span6">
                                <div class="control-group">
                                    <label class="control-label">
                                        Select User Type (In Application)</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlRole" CssClass="form-control" runat="server" 
                                            AutoPostBack="true" onselectedindexchanged="ddlRole_SelectedIndexChanged"
                                           >
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
                            <div class="span5" id="divZone" runat="server">
                                <div class="control-group">
                                    <label class="control-label">
                                        Select Zone
                                    </label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlzone" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span5" id="divlab" runat="server">
                                <div class="control-group">
                                    <label class="control-label">
                                        Selected Lab
                                        <%-- <asp:Label ID="Label1" Text="Select Zone" runat="server"></asp:Label>--%>
                                    </label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddllab" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="widget ">
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
                                            <asp:DropDownList ID="ddldist" CssClass="form-control" required runat="server" 
                                                AutoPostBack="True" onselectedindexchanged="ddldist_SelectedIndexChanged"
                                               >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span5">
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
                                            <asp:TextBox ID="txtaddress" TextMode="MultiLine" Autocomplete="Off" required CssClass="form-control"
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
                                                    <asp:TextBox ID="txtUser" Autocomplete="Off" CssClass="form-control" MaxLength="50" required runat="server" AutoPostBack="true"
                                                        ></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="txtUser_FilteredTextBoxExtender1" runat="server"
                                                        BehaviorID="txtUser_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                        TargetControlID="txtUser">
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
                                                        ToolTip="Refresh Captcha Text"   formnovalidate 
                                                        onclick="btnImgRefresh_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span6">
                                            <div class="control-group">
                                                <label class="control-label">
                                                    Enter Captcha Text</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="captcha" Autocomplete="Off" CssClass="form-control" runat="server" required class="captcha-field"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <div class="span5">
                            <div class="controls">
                                <asp:Button ID="BtnUpdate" class="btn btn-primary" formnovalidate="formnovalidate"
                                    runat="server" Text="Update" onclick="BtnUpdate_Click"/>
                                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                        </asp:Panel>
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
