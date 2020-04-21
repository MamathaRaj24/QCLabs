<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_TargetAllotment.aspx.cs"
    Inherits="Agri_Admin_Edit_TargetAllotment" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Admin/AgriMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Target Allotmnet</title>
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
            if (confirm("Are you sure to delete the selected Target Allotment?")) {
                return true;
            }
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'Edit_TargetAllotment.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Edit_TargetAllotment.aspx');
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
                                Target Allotment-Edit/Delete</h3>
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
                                        <asp:DropDownList ID="ddlCategory" CssClass="form-control " Height="35px" 
                                            runat="server" onselectedindexchanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                           
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span5" runat="server" id="distdiv">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        District</label>
                                    <div class="controls">
                                        <asp:Label ID="lblDist" runat="server"  CssClass="form-control "></asp:Label>
                                         <asp:Label ID="lblDistCode" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span5" runat="server" id="alloteddiv">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Alloted Qty (in Qtls)</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtquantity" runat="server" MaxLength="7" CssClass="form-control " onblur="if(this.value==''){ this.value='0';}"
                                            onfocus="if(this.value=='0'){ this.value=''; }"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtAlloted_extender" FilterType="Numbers,Custom"
                                            ValidChars="." runat="server" TargetControlID="txtquantity">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                        <asp:Label ID="lblaid" runat="server" Visible="false"></asp:Label><asp:HiddenField
                                            runat="server" ID="HiddenField1" />
                                    </div>
                                </div>
                            </div>
                            <div class="span12 offset5">
                                <div class="control-group">
                                    <%-- <asp:Button ID="BtnSave" class="btn btn-primary" formnovalidate="formnovalidate"
                                        runat="server" Text="SAVE" OnClick="BtnSave_Click" OnClientClick=" return Hidebutton();" />--%>
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                                        Text="UPDATE" Visible="false" OnClick="btnUpdate_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-content" runat="server" id="gridid">
                            <asp:GridView ID="GVDistAllot" CssClass="table table-striped table-bordered" runat="server"
                                AutoGenerateColumns="false" ShowFooter="true" OnRowCommand="GVDistAllot_RowCommand">
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
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                           
                                                <asp:ImageButton ID="btnEdit" runat="server" formnovalidate="formnovalidate" ImageUrl="../../Assets/img/edit.png"
                                                   width="30" CommandName="Edt"></asp:ImageButton>
                                           
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../../Assets/img/delete1.png"
                                                   width="30" CommandName="Dlt" OnClientClick="return Confirm(this)"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="span12 offset5">
                                <div class="control-group">
                                    <asp:Button ID="btnGetTotal" runat="server" Text="Get Total" OnClick="btnGetTotal_Click">
                                    </asp:Button>
                                    <asp:Label runat="server" ID="lbltotquantityAlloted" Text="Total Qty Allotted:" ></asp:Label>
                                 
                                    <asp:Label runat="server" ID="lbltotal"></asp:Label>
                                </div>
                            </div>
                            <div class="span12 offset5">
                                <div class="control-group">
                                    <%-- <asp:Button ID="BtnSave" class="btn btn-primary" formnovalidate="formnovalidate"
                                        runat="server" Text="SAVE" OnClientClick=" return Hidebutton();" 
                                        onclick="BtnSave_Click" />
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                                        Text="UPDATE" Visible="false" OnClick="btnUpdate_Click" />--%>
                                </div>
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
