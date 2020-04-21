<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleCategory.aspx.cs" Inherits="Admin_SampleCategory" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Admin/AgriMenu.ascx"%>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin-Sample Category</title>
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
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        function Confirm(link) {
            if (confirm("Are you sure to delete the Sample Category?")) {
                return true;
            }
            else
                return false;

        }  
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'SampleCategory.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'SampleCategory.aspx');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <Header:header ID="header" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                                SAMPLE CATEGORY MASTER</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span6">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Sample Category Name</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txttype" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txttype_FilteredTextBoxExtender" runat="server"
                                            BehaviorID="txt_Product_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers, custom"
                                            TargetControlID="txttype" ValidChars=" .,()-">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="span5">
                                <div class="controls">
                                    <asp:Button ID="BtnSave" class="btn btn-primary" formnovalidate="formnovalidate"
                                        runat="server" Text="SAVE" OnClick="Save_Click" />
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                                        Text="UPDATE" OnClick="btnUpdate_Click" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVType" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" OnRowCommand="GVType_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Type Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampTypeCode" runat="server" Text='<%# Bind("category_id") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Type Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("category_name") %>'>
                                            </asp:Label>
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
