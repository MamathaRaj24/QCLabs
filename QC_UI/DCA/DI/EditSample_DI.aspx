<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditSample_DI.aspx.cs" Inherits="DI_EditSample_DI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/DI/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inspector-Edit Sample Registration</title>
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
    <script type="text/javascript" src="../../Assets/js/MonthPicker.js"></script>
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
    <script type="text/javascript">
        history.pushState(null, null, 'EditSample_DI.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'EditSample_DI.aspx');
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $('input.monthpicker').monthpicker({ changeYear: true, minDate: "-5 Y", maxDate: "0 M" });
        });
        $(function () {
            $('input.monthpicker1').monthpicker({ changeYear: true, minDate: "-0 Y", maxDate: "+6 Y" });

        });
        $(document).ready(function () {
            $('#txtvlddt').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-0:+10"
            });
        });
        $(document).ready(function () {
            $('#txtinvdate').datepicker({
                maxDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-1:+10"
            });
        });
        $(document).ready(function () {
            $('#txtsampldate').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-1:+0"
            });
        });
        $(document).ready(function () {
            $('#txtStkRcvdDt').datepicker({
                maxDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-1:+10"
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
                        <div class="widget-header">
                            <i class="icon-tasks"></i>
                            <h3 align="center">
                                Update Sample</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="Gvfreeze" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" OnRowCommand="Gvfreeze_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="SlNo">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsmid" Text='<%# Bind("RegID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SampleDate" HeaderText="Sample Collecting Date" />
                                    <asp:BoundField DataField="Category_Name" HeaderText="Sample Category" />
                                    <asp:BoundField DataField="GenericName" HeaderText="Generic Name" />
                                    <asp:BoundField DataField="TradeName" HeaderText="Trade Name" />
                                    <asp:BoundField DataField="ManfucaturerName" HeaderText="Manufactured By" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnedit" runat="server" CommandName="edt" formnovalidate Text="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnDel" runat="server" CommandName="DLT" formnovalidate OnClientClick="return Confirm(this)"
                                                Text="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlsr" runat="server" Visible="false">
                    <div class="row">
                        <div class="widget ">
                            <div class="widget-header coler">
                                <i class="icon-tasks"></i>
                                <h3 align="center">
                                    Update Sample Registration</h3>
                                <h1 class="alert alert-info">
                                    <asp:Label ID="lblSampleno" Visible="false" runat="server"></asp:Label>
                                </h1>
                            </div>
                            <div class="widget-content">
                                <fieldset>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label" for="ddlClass">
                                                Sample Class<span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlClass" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label" for="ddlSamtype">
                                                Usage<span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlusage" CssClass="form-control" runat="server" required>
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="Human">Human</asp:ListItem>
                                                    <asp:ListItem Value="Veterinary">Veterinary</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label" for="ddlpmttype">
                                                Payment Type<span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlpmttype" CssClass="form-control" runat="server" required>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Priority<span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlpriority" CssClass="form-control" runat="server" required>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Reason & Remarks for Top Priority</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtremarks" CssClass="form-control" runat="server"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtremarks_FilteredTextBoxExtender" runat="server"
                                                    BehaviorID="txtremarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom"
                                                    TargetControlID="txtremarks" ValidChars=" .,()-">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="widget-header coler">
                                <i class="icon-tasks"></i>
                                <h3 align="center">
                                    Dealer-Distributor Details</h3>
                            </div>
                            <div class="widget-content">
                                <fieldset>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Type of Firm<span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlFirmType" CssClass="form-control" runat="server" required>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Name of the Firm <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtfirnm" CssClass="form-control" runat="server" required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtfirnmFilteredTextBoxExtender1" runat="server"
                                                    BehaviorID="txtfirnm_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters"
                                                    TargetControlID="txtfirnm">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Licence No</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtlcnno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Validity</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtvlddt" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Name of the Contact Person</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtContactPerson" CssClass="form-control" runat="server"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtContactPersonFilteredTextBoxExtender1"
                                                    runat="server" BehaviorID="txtContactPerson_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom "
                                                    TargetControlID="txtContactPerson" ValidChars=".">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Mobile No <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtmbno" MaxLength="10" CssClass="form-control" runat="server" required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtmbnoFilteredTextBoxExtender1" runat="server"
                                                    BehaviorID="txtmbno_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtmbno">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Sample Quantity Picked for Analysis <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtqtyPickd" CssClass="form-control" runat="server" required></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                State Name <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlstate" AutoPostBack="true" runat="server" CssClass="form-control"
                                                    required OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                District Name <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddldist" CssClass="form-control" AutoPostBack="true" runat="server"
                                                    required OnSelectedIndexChanged="ddldist_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Mandal <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlmandl" CssClass="form-control" runat="server" required>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                House No <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txthsno" CssClass="form-control" runat="server" MaxLength="15" required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txthsnoFilteredTextBoxExtender1" runat="server"
                                                    BehaviorID="txthsno_FilteredTextBoxExtender" FilterType="UppercaseLetters,Numbers,custom"
                                                    TargetControlID="txthsno" ValidChars=".,-\/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Locality/Road Name <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtlocality" TextMode="MultiLine" CssClass="form-control" runat="server"
                                                    required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtlocalityFilteredTextBoxExtender1" runat="server"
                                                    BehaviorID="txtlocality_FilteredTextBoxExtender" FilterType="Numbers,custom,UppercaseLetters, LowercaseLetters"
                                                    TargetControlID="txtlocality" ValidChars=".,-">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="widget-header coler">
                                <i class="icon-tasks"></i>
                                <h3 align="center">
                                    Sample Marking Details</h3>
                            </div>
                            <div class="widget-content">
                                <fieldset>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Sample Collecting Date <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtsampldate" CssClass="form-control" required AutoComplete="off"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Dosage Form <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" required>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Generic Name <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtGenericNm" CssClass="form-control" MaxLength="50" runat="server"
                                                    required></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Trade Name <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtTradeNm" MaxLength="50" CssClass="form-control" runat="server"
                                                    required></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Batch No <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtbatchno" MaxLength="50" CssClass="form-control" runat="server"
                                                    required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtbatchno_FilteredTextBoxExtender1" runat="server"
                                                    BehaviorID="txtbatchno_FilteredTextBoxExtender" FilterType="Numbers,custom,UppercaseLetters"
                                                    TargetControlID="txtbatchno" ValidChars=".,-/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Date of Manufacturing <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtManufDt" AutoComplete="off" CssClass="form-control monthpicker"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Date of Expiry <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtdtexpry" AutoComplete="off" CssClass="form-control monthpicker1"
                                                    AutoPostBack="true" runat="server" required OnTextChanged="txtdtexpry_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Date of Stock Recived <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtStkRcvdDt" CssClass="form-control" AutoComplete="off" runat="server"
                                                    AutoPostBack="true" required></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Sample Qty sent for analysis <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtqty" CssClass="form-control" MaxLength="10" runat="server" required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    BehaviorID="txtbatchno_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtbatchno">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span8">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Composition <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtCompostion" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                    required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtCompostion_FilteredTextBoxExtender2"
                                                    runat="server" BehaviorID="txtCompostion_FilteredTextBoxExtender" FilterType="Numbers,custom,UppercaseLetters,LowercaseLetters"
                                                    TargetControlID="txtCompostion" ValidChars=".,-/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="widget-header coler">
                                <i class="icon-tasks"></i>
                                <h3 align="center">
                                    Manufacturer/ Marketer Details</h3>
                            </div>
                            <div class="widget-content">
                                <fieldset>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Manufacturer State <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlmnstate" CssClass="form-control" runat="server" required>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Name of Manufacturer <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtmnfname" CssClass="form-control" runat="server" required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtmnfname_FilteredTextBoxExtender2" runat="server"
                                                    BehaviorID="txtmnfname_FilteredTextBoxExtender" FilterType="custom,UppercaseLetters,LowercaseLetters"
                                                    TargetControlID="txtmnfname" ValidChars=".,-/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Manufacturer License No <span class="mandatory">*</span></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtmnflcnno" MaxLength="30" CssClass="form-control" runat="server"
                                                    required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtmnflcnno_FilteredTextBoxExtender2" runat="server"
                                                    BehaviorID="txtmnflcnno_FilteredTextBoxExtender" FilterType="custom,UppercaseLetters,Numbers"
                                                    TargetControlID="txtmnflcnno" ValidChars=".,-/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Address <span class="mandatory">*</span>
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtmnaddress" MaxLength="80" CssClass="form-control" runat="server"
                                                    required></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtmnaddress_FilteredTextBoxExtender2" runat="server"
                                                    BehaviorID="txtmnaddress_FilteredTextBoxExtender" FilterType="custom,UppercaseLetters,Numbers,LowercaseLetters"
                                                    TargetControlID="txtmnaddress" ValidChars=".,-/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Marketer Name
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtMarketerName" CssClass="form-control" runat="server"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtMarketerName_FilteredTextBoxExtender2"
                                                    runat="server" BehaviorID="txtMarketerName_FilteredTextBoxExtender" FilterType="custom,UppercaseLetters,Numbers,LowercaseLetters"
                                                    TargetControlID="txtMarketerName" ValidChars=".,-/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span4">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Marketer state
                                            </label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlMarkerState" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span8">
                                        <div class="control-group">
                                            <label class="control-label">
                                                Marketer Address
                                            </label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtMarkerAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtMarkerAddress_FilteredTextBoxExtender2"
                                                    runat="server" BehaviorID="txtMarkerAddress_FilteredTextBoxExtender" FilterType="custom,UppercaseLetters,Numbers,LowercaseLetters"
                                                    TargetControlID="txtMarkerAddress" ValidChars=".,-/">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnadd" Text="Update Sample" Width="30%" runat="server" class="btn btn-primary "
                                    OnClick="btnadd_Click"></asp:Button>
                                <asp:Label ID="lblsmpleid" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hf" runat="server" />
    <Footer:footer ID="footer" runat="server"></Footer:footer>
    </form>
</body>
</html>
