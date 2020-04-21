<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee_Mst.aspx.cs" Inherits="Admin_Employee_Mst" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Admin/AgriMenu.ascx"%>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin-Employee</title>
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
            if (confirm("Are you sure to delete the selected Employee?")) {
                return true;
            }
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'Employee_Mst.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Employee_Mst.aspx');
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
                                Employee MASTER</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Employee Code</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtempcode" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtempcode_FilteredTextBoxExtender" runat="server"
                                            BehaviorID="txtempcode_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers, custom"
                                            TargetControlID="txtempcode" ValidChars=" .,()-">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Employee Name</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtempname" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtempname_FilteredTextBoxExtender" runat="server"
                                            BehaviorID="txtempname_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers, custom"
                                            TargetControlID="txtempname" ValidChars=" .,()-">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Employee Mobile</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtempmobile" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtempmobile_FilteredTextBoxExtender" runat="server"
                                            BehaviorID="txtempmobile_FilteredTextBoxExtender" FilterType="Numbers"
                                            TargetControlID="txtempmobile">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                             
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Employee Email</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtempemail" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtempemail"
                                            ValidationGroup="g1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ForeColor="Red" ErrorMessage="Give valid Email Id" Font-Size="Smaller"></asp:RegularExpressionValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtempemail_FilteredTextBoxExtender" runat="server"
                                            BehaviorID="txtempemail_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers, custom"
                                            TargetControlID="txtempemail" ValidChars=" .,()-@">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Active</label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="Rdbactive" CssClass="radio-inline radio"  RepeatDirection="Vertical"  runat="server">
                                        <asp:ListItem Value="0" Selected>True</asp:ListItem>
                                          <asp:ListItem Value="1">False</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="span6">
                                <div class="controls">
                                    <asp:Button ID="BtnSave" class="btn btn-primary" 
                                        formnovalidate="formnovalidate" ValidationGroup="g1"
                                        runat="server" Text="SAVE" onclick="BtnSave_Click"  />
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                                        Text="UPDATE"  Visible="false" onclick="btnUpdate_Click" />
                                         <asp:Label ID="lblempid" Visible="false" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="Gvemp" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" 
                                onrowcommand="Gvemp_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblempcode" runat="server"  Text='<%# Bind("EmployeeCode") %>'></asp:Label>
                                           
                                            <asp:Label ID="lblempid" runat="server" Visible="false" Text='<%# Bind("EmpID") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                         <asp:Label ID="lblempname" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                           
                                        
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobilenumber" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                                           
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            
                                            <asp:Label ID="lblemail" runat="server" Text='<%# Bind("email") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:Label ID="lblactive" runat="server" Text='<%# Bind("A") %>'></asp:Label>
                                             
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnedit" runat="server" CommandName="edt" Text="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDel" runat="server" CommandName="dlt" OnClientClick="return Confirm(this)"
                                                Text="Delete" />
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
