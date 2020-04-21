<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ferti_Reg.aspx.cs" Inherits="AO_Ferti_Reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/AO/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AO-Fertilizer Registration</title>
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
    <script type="text/javascript" src="../../Assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        function Confirm(link) {
            if (confirm("Are you sure to delete the row?")) {
                return true;
            }
            else
                return false;

        }  
    </script>
   <%-- <script type="text/javascript">
        function SelectAllCheckboxes1(chk) {
            $('#<%=GvSpec.ClientID%>').find("input:checkbox").each(function () {
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
    </script>--%>
    <script type="text/javascript">
        history.pushState(null, null, 'Ferti_Reg.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'Ferti_Reg.aspx');
        });
    </script>
    <script type="text/javascript">


        $(document).ready(function () {
            $('#txtvldty').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-0:+10"
            });
        });
        $(document).ready(function () {
            $('#txtdtmn').datepicker({
                maxDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });
        $(document).ready(function () {
            $('#txtdatemipagency').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+0"
            });
        });
        $(document).ready(function () {
            $('#txtdtsam').datepicker({
                maxDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
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
                                    <img src="../../Assets/img/processing.gif" alt="loading...." />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="widget ">
                        <div class="widget-header coler">
                            <i class="icon-reorder "></i>
                            <h3 align="center">
                                Fertilizer Registration</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Sample Class: <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlClass" CssClass="form-control" runat="server" required>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Code No. of Sample <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtcodesample" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server" required></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtcodesample_FilteredTextBoxExtender" runat="server"
                                            BehaviorID="txtcodesample_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtcodesample" ValidChars=" /.,()-'">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="widget-header coler">
                            <i class="icon-reorder "></i>
                            <h3 align="center">
                                Dealer Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Licence No <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtdllcno" CssClass="form-control" required AutoComplete="off" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtdllcnoFilteredTextBoxExtender1" runat="server"
                                            BehaviorID="txtdllcno_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtdllcno" ValidChars="/,-,">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-lg-2">
                                        Name of the Firm: <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtfirnmdlr" CssClass="form-control" MaxLength="50" AutoComplete="off"
                                            runat="server" required></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtfirnmdlrFilteredTextBoxExtender1" runat="server"
                                            BehaviorID="txtfirnmdlr_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtfirnmdlr">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-lg-2">
                                        Name of the Owner <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtnmowner" CssClass="form-control" AutoComplete="off" required
                                            runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtnmownerFilteredTextBoxExtender1" runat="server"
                                            BehaviorID="txtnmowner_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtnmowner">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Validity <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtvldty" CssClass="form-control" required AutoComplete="off" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-lg-2">
                                        Batch No <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtbthchno" CssClass="form-control" required AutoComplete="off"
                                            MaxLength="50" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtbthchnoFilteredTextBoxExtender1" runat="server"
                                            BehaviorID="txtbthchno_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtbthchno">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-lg-2">
                                        Stock position of the Lot <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtstockl" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            required runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtstocklFilteredTextBoxExtender2" runat="server"
                                            BehaviorID="txtstockl_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtstockl">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        State Name <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlstate" CssClass="form-control" AutoPostBack="true" runat="server"
                                            required OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        District Name <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddldist" CssClass="form-control" AutoPostBack="true" runat="server"
                                            required>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        House No\Flat No <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txthsno" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server" required></asp:TextBox>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Locality/Street Name <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtlcstr" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server" required></asp:TextBox>
                                    </div>
                                    <label class="col-lg-6">
                                        Date of Receipt of the stock by the dealer/manufacture/importer/pool handling agency
                                        <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtdatemipagency" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="widget-header coler">
                            <i class="icon-reorder "></i>
                            <h3 align="center">
                                Sample/Marking Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-1">
                                        Date of Sampling <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtdtsam" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server" required></asp:TextBox>
                                    </div>
                                    <label class="col-lg-1">
                                        Sample Type <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlsmltype" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlsmltype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-1">
                                        Sample Grade <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlsample" runat="server" CssClass="form-control" >
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-1">
                                        Physical Condition <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlPhysicalCon" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- <div class="widget-content">
                                    <asp:GridView ID="GvSpec" runat="server" CssClass="table table-striped table-bordered"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SlNo">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpecCode" Visible="false" Text='<%# Bind("Parameter_ID") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblSpec" Text='<%# Bind("Parameter") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Standard Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstdVal" Text='<%# Bind("StandardValue") %>' runat="server"></asp:Label>
                                                    <%--<asp:Label ID="lblcheckid" Text='<%# Bind("Checkid") %>' runat="server"></asp:Label>--%>
                                <%--</ItemTemplate>
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
                                </div>--%>
                            </fieldset>
                        </div>
                        <div class="widget-header coler">
                            <i class="icon-reorder "></i>
                            <h3 align="center">
                                Manufacturer Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Date of Manufacturing<span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtdtmn" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-lg-2">
                                        Name of the Manufacturer<span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtmnname" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtmnnameFilteredTextBoxExtender6" runat="server"
                                            BehaviorID="txtmnname_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtmnname">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-lg-2">
                                        Manufacturer State<span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlmnstate" CssClass="form-control" runat="server" required>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Address<span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtaddrs" TextMode="MultiLine" CssClass="form-control" AutoComplete="off"
                                            MaxLength="150" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-lg-2" for="Rblsmdrf">
                                        whether Samples drwan from
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:RadioButtonList ID="RblSmpleDrawnFrom" runat="server" CssClass=" radio inline"
                                            RepeatDirection="Vertical">
                                            <asp:ListItem Value="OB">Open Bag</asp:ListItem>
                                            <asp:ListItem Value="SB">Stitched Bag</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <label class="col-lg-2">
                                        whether Panchanama Conducted<span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:RadioButtonList ID="Rblphnma" runat="server" CssClass=" radio inline" RepeatDirection="Vertical">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnadd" formnovalidate="formnovalidate" Text="Submit Fertilizer Sample" class="btn btn-primary "
                                CausesValidation="false" runat="server" OnClick="btnadd_Click" />
                            <asp:Label ID="lblgrcode" runat="server" Visible="false"></asp:Label>
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
