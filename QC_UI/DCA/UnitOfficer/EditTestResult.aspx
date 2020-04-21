<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTestResult.aspx.cs" Inherits="DCA_Analyst_ExEdittestresult" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/UnitOfficer/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unit Officer- Test Result</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600"
        rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../Assets/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Assets/js/bootstrap.js"></script>
    <script type="text/javascript" src="../../Assets/js/base.js"></script>
    <link href="../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript" src="../../Assets/js/base.js"></script>
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

        $(document).ready(function () {
            $('#txtStartDt').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });

        $(document).ready(function () {
            $('#txtEndDt').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
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
                        <div class="widget-header">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                Edit Test Results</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVTestResult" runat="server" CssClass="table table-striped table-bordered"
                                AutoGenerateColumns="False" OnRowCommand="GVTestResult_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSampleid" Text='<%# Eval("Sample_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Category" DataField="Category_Name" />
                                    <asp:BoundField HeaderText="Trade Name" DataField="TradeName" />
                                    <asp:BoundField HeaderText="S1 Result Entered" DataField="S1TestDone" />
                                    <asp:BoundField HeaderText="S2 Result Entered" DataField="S2TestDone" />
                                    <asp:BoundField HeaderText="S3 Result Entered" DataField="S3TestDone" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnTRremarksDesc" runat="server" CommandName="editDR" formnovalidate
                                                Text="Edit Description&Remarks" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="BtnTREdit" runat="server" CommandName="editTR" formnovalidate Text="Test Result" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btns1Edit" runat="server" CommandName="editS1" formnovalidate Text="S1 Result" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btns2Edit" runat="server" CommandName="editS2" formnovalidate Text="S2 Result" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Btns3Edit" runat="server" CommandName="editS3" formnovalidate Text=" S3 Result" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="Edidescrmrks" runat="server" visible="false">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    Edit Description & Remarks Test Results
                                    <asp:Label ID="lblsampleid" runat="server" CssClass="alert alert-info"></asp:Label>
                                </h3>
                            </div>
                            <div class="widget-content">
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2 col-lg-2 ">
                                        Laboratory
                                    </label>
                                    <div class="col-md-2 col-lg-2 col-lg-2">
                                        <asp:Label ID="lbllab" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Name of the drug
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbldrugnm" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Manufacturing Date
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblmfgdt" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        Expiry Date
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblexpdt" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Quantity Received
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblqty" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Composition
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblComposition" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-md-2 col-lg-2">
                                        Test Started on
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:TextBox ID="txtStartDt" CssClass="form-control" required runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Test Completed on
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:TextBox ID="txtEndDt" CssClass="form-control" required runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-md-2 col-lg-2">
                                        Description
                                    </label>
                                    <div class="col-md-2 col-lg-4">
                                        <asp:TextBox ID="txtDesc" TextMode="MultiLine" Height="80px" CssClass="form-control"
                                            required runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Junior Analyst Remarks
                                    </label>
                                    <div class="col-md-2 col-lg-4">
                                        <asp:TextBox ID="txtremarks" TextMode="MultiLine" Height="80px" CssClass="form-control"
                                            required runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 text-center">
                                        <asp:Button ID="BtnTR" CssClass="btn btn-primary" runat="server" ValidationGroup="tr"
                                            Text="Update Test Result" OnClick="BtnTR_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="EditTr" runat="server">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    Edit Test Results</h3>
                                <asp:Label ID="lbltestsamplid" runat="server" CssClass="alert alert-info"></asp:Label>
                            </div>
                            <div class="widget-content">
                                <div id="edttrgrid" runat="server" visible="false">
                                    <div class="col-md-12 table-responsive">
                                        <asp:GridView ID="Gvedittest" runat="server" CssClass="table table-striped table-bordered table-hover"
                                            AutoGenerateColumns="False" ShowFooter="true" AutoGenerateEditButton="true" DataKeyNames="TestID"
                                            OnRowCancelingEdit="Gvedittest_RowCancelingEdit" OnRowDataBound="Gvedittest_RowDataBound"
                                            OnRowEditing="Gvedittest_RowEditing" OnRowUpdating="Gvedittest_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Test Parameter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledtTestParameter" Text='<%# Eval("Parameter") %>' runat="server"> </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbltestparam" Visible="false" Text='<%# Eval("TestParam") %>' runat="server"> </asp:Label>
                                                        <asp:DropDownList ID="ddledtTestParameter" CssClass="form-control" Width="65%" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test For">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledtTestfor" Text='<%# Eval("Testfor") %>' runat="server"> </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtedttestfor" CssClass="form-control" Text='<%# Eval("Testfor") %>'
                                                            runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Name (In Case of Others)" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledtTestDone" Text='<%# Eval("TestName") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtedttestname" CssClass="form-control" Text='<%# Eval("TestName") %>'
                                                            runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Calculation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledtcalculation" Text='<%# Eval("Calculation") %>' TextMode="MultiLine"
                                                            Width="300" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtedtcalculation" CssClass="form-control" Text='<%# Eval("Calculation") %>'
                                                            runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Protocol">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledttestpr" Text='<%# Eval("ProtocolName") %>' runat="server"> </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbltestprotocal" Visible="false" Text='<%# Eval("TestProtocol") %>'
                                                            runat="server"> </asp:Label>
                                                        <asp:DropDownList ID="ddledtTestProtocols" CssClass="form-control" Width="65%" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Found">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledtValueFound" Text='<%# Eval("found") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtedtvaluefound" MaxLength="20" CssClass="form-control" Text='<%# Eval("found") %>'
                                                            runat="server"></asp:TextBox>
                                                        <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtValueFoundExtender" runat="server" FilterType="Numbers,Custom"
                                                            TargetControlID="txtedtvaluefound" ValidChars="." />--%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Claim ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledtValueClaim" Text='<%# Eval("claim") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtedtValueClaim" CssClass="form-control" MaxLength="20" Text='<%# Eval("claim") %>' runat="server"></asp:TextBox>
                                                        <%--<ajaxToolkit:FilteredTextBoxExtender ID="txtedtValueClaimExtender" runat="server"
                                                            FilterType="Numbers,Custom" TargetControlID="txtedtValueClaim" ValidChars="." />--%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Limit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbledtlimit" Text='<%# Eval("limit") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtedtlimit" MaxLength="20" CssClass="form-control" Text='<%# Eval("limit") %>'
                                                            runat="server"></asp:TextBox>
                                                        <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtedtlimitExtender" runat="server" FilterType="Numbers,Custom"
                                                            TargetControlID="txtedtlimit" ValidChars=".-" />--%>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="addmore" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-12 table-responsive">
                                            <asp:GridView ID="GridTr" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AutoGenerateColumns="False" ShowFooter="true" OnRowCommand="GridTr_RowCommand"
                                                OnRowDataBound="GridTr_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <FooterTemplate>
                                                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Assets/img/plus.png" formnovalidate
                                                                CommandName="AddRows" />
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDeletes" runat="server" formnovalidate ImageUrl="~/Assets/img/minus.png"
                                                                CommandName="Remove" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Test Parameter">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlTestParameter" Width="65%" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblTestParameter" Visible="false" Text='<%# Eval("TestParam") %>'
                                                                runat="server"> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Test For">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTestfor" CssClass="form-control" Text='<%# Eval("Testfor") %>'
                                                                runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Test Name (In Case of Others)" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTestDone" CssClass="form-control" Text='<%# Eval("TestName") %>'
                                                                runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Calculation">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtcalculation" CssClass="form-control" Text='<%# Eval("Calculation") %>'
                                                                TextMode="MultiLine" Width="300" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Test Protocol">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlTestProtocol" Width="65%" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lbltestpr" Visible="false" Text='<%# Eval("TestProtocol") %>' runat="server"> </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Found ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValueFound" MaxLength="20" CssClass="form-control" Text='<%# Eval("found") %>'
                                                                runat="server"></asp:TextBox>
                                                            <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtValueFoundExtender" runat="server" FilterType="Numbers,Custom"
                                                                TargetControlID="txtValueFound" ValidChars="." />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Claim">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtValueClaim" MaxLength="20" CssClass="form-control" Text='<%# Eval("claim") %>'
                                                                runat="server"></asp:TextBox>
                                                            <%--  <ajaxToolkit:FilteredTextBoxExtender ID="txtClaimExtender" runat="server" FilterType="Numbers,Custom"
                                                                TargetControlID="txtValueClaim" ValidChars="." />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Limit">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtlimit" MaxLength="20" CssClass="form-control" Text='<%# Eval("limit") %>'
                                                                runat="server"></asp:TextBox>
                                                            <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtlimitExtender" runat="server" FilterType="Numbers,Custom"
                                                                TargetControlID="txtlimit" ValidChars="-." />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-6 text-center offset5">
                                            <asp:Button ID="Btntrtvp" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="Btntrtvp_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <div class="col-md-6 text-center pull-left">
                                        <asp:Button ID="Btnadd" CssClass="btn btn-primary" runat="server" ValidationGroup="tr"
                                            Visible="false" Text="Addmore" OnClick="Btnadd_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="EditS1" runat="server" visible="false">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    Edit S1 Dissolution Test Result
                                    <asp:Label ID="lbls1smpid" runat="server" CssClass="alert alert-info"></asp:Label>
                                </h3>
                            </div>
                            <div class="widget-content">
                                <div class="col-md-12 table-responsive">
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="font-size: large; font-weight: 700;" width="55">
                                                            S1
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        Calculation
                                                                    </td>
                                                                    <td>
                                                                        Found
                                                                    </td>
                                                                    <td>
                                                                        Claim
                                                                    </td>
                                                                    <td>
                                                                        Percentage
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        1
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtS1calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                            MaxLength="5" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        2
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtS1calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        3
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtS1calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        4
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtS1calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        5
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtS1calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        6
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtS1calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txts1P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: large; font-weight: 700;" width="125">
                                                            S1 Remarks
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts1Remrks" CssClass="form-control" TextMode="MultiLine" MaxLength="250"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="BtnS1" CssClass="btn btn-primary" runat="server" CausesValidation="false"
                                            formnovalidate Text="Update S1 Dissolution Test Result" OnClick="BtnS1_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="EditS2" runat="server" visible="false">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    Edit S2 Dissolution Test Result
                                    <asp:Label ID="lbls2smpid" runat="server" CssClass="alert alert-info"></asp:Label>
                                </h3>
                            </div>
                            <div class="widget-content">
                                <div class="col-md-12 table-responsive">
                                    <table>
                                        <tr>
                                            <td style="font-size: large; font-weight: 700;" width="55">
                                                S2
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            Calculation
                                                        </td>
                                                        <td>
                                                            Found
                                                        </td>
                                                        <td>
                                                            Claim
                                                        </td>
                                                        <td>
                                                            Percentage
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            1
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            2
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            3
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            4
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            5
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            6
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts2P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: large; font-weight: 700;" width="125">
                                                S2 Remarks
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txts2remarks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="BtnS2" CssClass="btn btn-primary" runat="server" CausesValidation="false"
                                            formnovalidate Text="Update S2 Dissolution Test Result" OnClick="BtnS2_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="EditS3" runat="server" visible="false">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    Edit S3 Dissolution Test Result
                                    <asp:Label ID="lbls3smpid" runat="server" CssClass="alert alert-info"></asp:Label>
                                </h3>
                            </div>
                            <div class="widget-content">
                                <div class="col-md-12 table-responsive">
                                    <table>
                                        <tr>
                                            <td style="font-size: large; font-weight: 700;" width="55">
                                                S3
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            Calculation
                                                        </td>
                                                        <td>
                                                            Found
                                                        </td>
                                                        <td>
                                                            Claim
                                                        </td>
                                                        <td>
                                                            Percentage
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            1
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            2
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            3
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            4
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            5
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            6
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            7
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation7" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            8
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation8" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            9
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation9" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            10
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation10" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            11
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation11" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                            12
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3calculation12" CssClass="form-control" TextMode="MultiLine"
                                                                runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3F12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3Cl12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txts3P12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="font-size: large; font-weight: 700;" width="125">
                                                S3 Remarks
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txts3remarks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                    </table>
                                </div>
                                <div class="row form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="BtnS3" CssClass="btn btn-primary" ValidationGroup="s3" runat="server"
                                            formnovalidate Text="Save S3 Dissolution Test Result" OnClick="BtnS3_Click" />
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
