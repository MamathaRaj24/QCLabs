<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentDetails.aspx.cs"
    Inherits="Agri_Fertilizer_LabOfficer_EquipmentDetails" %>

<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Fertilizer/LabOfficer/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Fertilizer/LabOfficer/Footer.ascx" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Fertilizer/LabOfficer/LabOfficerMenu.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Lab Officer-Equipment Details</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../../../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../../Assets/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../Assets/js/bootstrap.js"></script>
    <script type="text/javascript" src="../../../Assets/js/base.js"></script>
    <script type="text/javascript" src="../../../Assets/js/jquery.min.js"></script> 
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'EquipmentDetails.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'EquipmentDetails.aspx');
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
                                Update Equipment Details</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span5">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Sample Type</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlSampType" CssClass="form-control " Height="35px" AutoPostBack="true"
                                            runat="server" OnSelectedIndexChanged="ddlSampType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divequipment" visible="false" runat="server">
                            <div class="widget-content">
                                <asp:GridView ID="GvEquipment" runat="server" CssClass="table table-striped table-bordered"
                                    GridLines="None" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Equipment Details">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEquipCode" runat="server" Visible="false" Text='<%# Bind("EquipCode") %>'>
                                                </asp:Label>
                                                <asp:Label ID="lblEqiupmentDetails" runat="server" Text='<%# Bind("EquipName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Condition">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCondition" AutoComplete="Off" CssClass="form-control" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Availability">
                                            <ItemTemplate>
                                                <asp:Label ID="lblavail" runat="server"></asp:Label>
                                                <asp:RadioButtonList ID="rblisAvailable" align="center" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Working Condition">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWorkCon" runat="server"></asp:Label>
                                                <asp:RadioButtonList ID="rblWorkCondition" align="center" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="span5">
                                    <div class="control-group">
                                        <label class="control-label" for="username">
                                           Allot Sample</label>
                                        <div class="controls">
                                            <asp:RadioButtonList ID="rdbldontallot" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span5">
                                    <div class="control-group">
                                        <div class="controls">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
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
