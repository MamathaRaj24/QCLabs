<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RA_TestResult.aspx.cs" Inherits="Analyst_RA_TestResult" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Analyst/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Junior Analyst- Realloted Test Result</title>
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
                                Realloted Enter Test Results</h3>
                        </div>
                        <div class="row" id="divAddDis" runat="server" visible="false">
                        </div>
                        <div class="widget-content">
                            <div class="tabbable">
                                <ul class="nav nav-tabs">
                                    <li id="trtab" runat="server" class="active"><a href="#TestResult" data-toggle="tab">
                                        Test Result</a></li>
                                    <li id="s1tab" runat="server"><a href="#S1controls" data-toggle="tab">S1 </a></li>
                                    <li id="s2tab" runat="server"><a href="#S2controls" data-toggle="tab">S2 </a></li>
                                    <li id="S3tab" runat="server"><a href="#S3controls " data-toggle="tab">S3</a></li>
                                    <li id="A1tab" runat="server"><a href="#A1controls" data-toggle="tab">A1 </a></li>
                                    <li id="A2ab" runat="server"><a href="#A2controls" data-toggle="tab">A2 </a></li>
                                    <li id="A3ab" runat="server"><a href="#A3controls" data-toggle="tab">A3 </a></li>
                                    <li id="B1tab" runat="server"><a href="#B1controls" data-toggle="tab">B1 </a></li>
                                    <li id="B2tab" runat="server"><a href="#B2controls" data-toggle="tab">B2 </a></li>
                                    <li id="B3tab" runat="server"><a href="#B3controls" data-toggle="tab">B3 </a></li>
                                </ul>
                                <br />
                                <div class="tab-content">
                                    <div class="tab-pane active" id="TestResult">
                                        <div id="editprofile1" runat="server" class="form-horizontal">
                                            <fieldset>
                                                <div class="form-group">
                                                    <label class="col-lg-2">
                                                        Sample Id
                                                    </label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtsample" CssClass="form-control" required runat="server" AutoPostBack="true"
                                                            OnTextChanged="txtsample_TextChanged"></asp:TextBox>
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB3remarksFilteredTextBoxExtender8" runat="server"
                                                            BehaviorID="txtsample_FilteredTextBoxExtender" FilterType="UppercaseLetters, custom,Numbers"
                                                            TargetControlID="txtsample" ValidChars="-/">
                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="pnltestresult" runat="server" DefaultButton="BtnTR" ValidationGroup="tr">
                                                    <div class="row">
                                                        <div class="widget-content ">
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
                                                                    Sample Category
                                                                </label>
                                                                <div class="col-md-2 col-lg-2">
                                                                    <asp:Label ID="lblcategory" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 col-lg-2">
                                                                    Manufacturing Date
                                                                </label>
                                                                <div class="col-md-2 col-lg-2">
                                                                    <asp:Label ID="lblmfgdt" runat="server"></asp:Label>
                                                                </div>
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
                                                            </div>
                                                            <div class=" form-group">
                                                                <label class="col-md-2 col-lg-2">
                                                                    Composition
                                                                </label>
                                                                <div class="col-md-2 col-lg-2">
                                                                    <asp:Label ID="lblComposition" runat="server"></asp:Label>
                                                                </div>
                                                                <label class="col-md-2 col-lg-2">
                                                                    Test Started on
                                                                </label>
                                                                <div class="col-md-2 col-lg-2">
                                                                    <asp:TextBox ID="txtStartDt" Autocomplete="Off" CssClass="form-control" required
                                                                        runat="server"></asp:TextBox>
                                                                </div>
                                                                <label class="col-md-2 col-lg-2">
                                                                    Test Completed on
                                                                </label>
                                                                <div class="col-md-2 col-lg-2">
                                                                    <asp:TextBox ID="txtEndDt" CssClass="form-control" Autocomplete="Off" required runat="server"></asp:TextBox>
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
                                                                <label class="col-md-2 col-lg-2">
                                                                    Govt Analyst Remarks
                                                                </label>
                                                                <div class="col-md-2 col-lg-4">
                                                                    <asp:TextBox ID="txtgvtanalistremarks" Enabled="false" TextMode="MultiLine" Height="80px"
                                                                        CssClass="form-control" required runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12 table-responsive">
                                                            <asp:GridView ID="GVTestResult" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                                AutoGenerateColumns="False" ShowFooter="true" OnRowCommand="GVTestResult_RowCommand"
                                                                OnRowDataBound="GVTestResult_RowDataBound">
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
                                                                    <asp:TemplateField HeaderText="TestFor">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txttestfor" MaxLength="50" CssClass="form-control" Text='<%# Eval("Testfor") %>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="txttestforExtender" runat="server" FilterType="Numbers,Custom"
                                                                                TargetControlID="txttestfor"/>--%>
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
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtcalculationFilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtcalculation_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtcalculation" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Test Protocol">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlTestProtocol" Width="65%" runat="server">
                                                                            </asp:DropDownList>
                                                                            <asp:Label ID="lbltestpr" Visible="false" Text='<%# Eval("TestProtocol") %>' runat="server"> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Found">
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
                                                                            <%-- <ajaxToolkit:FilteredTextBoxExtender ID="txtClaimExtender" runat="server" FilterType="Numbers,Custom"
                                                                                TargetControlID="txtValueClaim" ValidChars="." />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Limit">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtlimit" MaxLength="20" CssClass="form-control" Text='<%# Eval("limit") %>'
                                                                                runat="server"></asp:TextBox>
                                                                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="txtlimitExtender" runat="server" FilterType="Numbers,Custom"
                                                                                TargetControlID="txtlimit" ValidChars="-." />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtremarks" CssClass="form-control" Text='<%# Eval("TestRemarks_UO") %>'
                                                                                runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                        <div class="col-md-12 text-center">
                                                            <asp:Button ID="BtnTR" CssClass="btn btn-primary" runat="server" ValidationGroup="tr"
                                                                Text="Save Test Result" OnClick="btnSubmit_Click" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="S1controls" runat="server">
                                        <asp:Label ID="lbls1" runat="server" Text="S1"></asp:Label>
                                        <asp:Panel ID="s1Panel" runat="server" DefaultButton="BtnS1" ValidationGroup="s1">
                                            <fieldset>
                                                <div class="col-md-12 table-responsive">
                                                    <table id="s1table" runat="server">
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
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtS1calculation1FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtS1calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtS1calculation1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1F1FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1F1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1Cl1FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1Cl1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1P1FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1P1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        2
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtS1calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtS1calculation2FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtS1calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtS1calculation2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1F2FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1F2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1Cl2FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1Cl2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1P2FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1P2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        3
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtS1calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtS1calculation3FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtS1calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtS1calculation3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1F3FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1F3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1Cl3FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1Cl3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1P3FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1P3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        4
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtS1calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtS1calculation4FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtS1calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtS1calculation4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1F4FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1F4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1Cl4FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1Cl4FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1Cl4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1P4FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1P4FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1P4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        5
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtS1calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtS1calculation5FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtS1calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtS1calculation5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1F5FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1F5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1Cl5FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1Cl5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1P5FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1P5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        6
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtS1calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtS1calculation6FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtS1calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtS1calculation6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1F6FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1F6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1Cl6FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1Cl6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txts1P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txts1P6FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txts1P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txts1P6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
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
                                                                            <asp:TextBox ID="txts1Remrks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                                                Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts1RemrksFilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts1Remrks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts1Remrks" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
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
                                                            formnovalidate Text="Save S1 Dissolution Test Result" OnClick="BtnS1_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane" id="S2controls" runat="server">
                                        <asp:Panel ID="s2Panel" runat="server" DefaultButton="BtnS2" ValidationGroup="s2">
                                            <fieldset>
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
                                                                            Found (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Claim (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Percentage (In Mg)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2calculation1FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts2calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2calculation1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2F1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2F1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2Cl1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2Cl1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2P1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2P1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2calculation2FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts2calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2calculation2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2F2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2F2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2Cl2FilteredTextBoxExtender2" runat="server"
                                                                                BehaviorID="txts2Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2Cl2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2P2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2P2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2calculation3FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts2calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2calculation3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2F3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2F3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2Cl3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2Cl3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2P3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2P3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2calculation4FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts2calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2calculation4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2F4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2F4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2Cl4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2Cl4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2P4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2P4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            5
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2calculation5FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts2calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2calculation5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2F5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2F5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2Cl5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2Cl5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2P5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2P5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            6
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2calculation6FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts2calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2calculation6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2F6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2F6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2Cl6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2Cl6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts2P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts2P6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts2P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts2P6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
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
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txts2remarksFilteredTextBoxExtender1" runat="server"
                                                                    BehaviorID="txts2remarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                    TargetControlID="txts2remarks" ValidChars=" =.,()/-+*">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="BtnS2" CssClass="btn btn-primary" ValidationGroup="s2" runat="server"
                                                            formnovalidate Text="Save S2 Dissolution Test Result" OnClick="BtnS2_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane" id="S3controls" runat="server">
                                        <asp:Panel ID="s3Panel" runat="server" DefaultButton="BtnS3" ValidationGroup="s3">
                                            <fieldset>
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
                                                                            Found (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Claim (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Percentage (In Mg)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation1FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation2FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation3FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation4FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            5
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation5FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            6
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation6FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            7
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation7" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation7FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F7FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl7FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P7FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            8
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation8" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation8FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F8FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl8FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P8FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            9
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation9" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation9FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F9FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl9FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P9FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            10
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation10" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation10FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F10FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl10FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P10FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            11
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation11" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation11FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F11FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl11FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P11FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            12
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3calculation12" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3calculation12FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txts3calculation12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3calculation12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3F12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3F12FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3F12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3F12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3Cl12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3Cl12FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3Cl12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3Cl12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txts3P12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txts3P12FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txts3P12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txts3P12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
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
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txts3remarksFilteredTextBoxExtender1" runat="server"
                                                                    BehaviorID="txts3remarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                    TargetControlID="txts3remarks" ValidChars=" =.,()/-+*">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="BtnS3" CssClass="btn btn-primary" ValidationGroup="s3" runat="server"
                                                            formnovalidate Text="Save S3 Dissolution Test Result" OnClick="BtnS3_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane" id="A1controls" runat="server">
                                        <asp:Panel ID="PanelA1" runat="server" DefaultButton="BtnA1" ValidationGroup="A1">
                                            <fieldset>
                                                <div class="col-md-12 table-responsive">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="font-size: large; font-weight: 700;" width="55">
                                                                            A1
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
                                                                                        <asp:TextBox ID="txtA1calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1calculation1FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtA1calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1calculation1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1F1FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1F1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1Cl1FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1Cl1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1P1FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1P1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        2
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1calculation2FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtA1calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1calculation2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1F2FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1F2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1Cl2FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1Cl2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1P2FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1P2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        3
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1calculation3FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtA1calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1calculation3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1F3FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1F3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1Cl3FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1Cl3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1P3FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1P3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        4
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1calculation4FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtA1calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1calculation4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1F4FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1F4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1Cl4FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1Cl4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1P4FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1P4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        5
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1calculation5FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtA1calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1calculation5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1F5FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1F5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1Cl5FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1Cl5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1P5FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1P5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        6
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1calculation6FilteredTextBoxExtender1"
                                                                                            runat="server" BehaviorID="txtA1calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1calculation6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1F6FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1F6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1Cl6FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1Cl6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtA1P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtA1P6FilteredTextBoxExtender1" runat="server"
                                                                                            BehaviorID="txtA1P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtA1P6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-size: large; font-weight: 700;" width="125">
                                                                            A1 Remarks
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA1Remrks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                                                Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA1RemrksFilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA1Remrks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA1Remrks" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="BtnA1" CssClass="btn btn-primary" runat="server" CausesValidation="false"
                                                            formnovalidate Text="Save A1 Dissolution Test Result" OnClick="BtnA1_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane" id="A2controls" runat="server">
                                        <asp:Panel ID="A2panel" runat="server" DefaultButton="BtnA2" ValidationGroup="A2">
                                            <fieldset>
                                                <div class="col-md-12 table-responsive">
                                                    <table>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="55">
                                                                A2
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
                                                                            Found (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Claim (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Percentage (In Mg)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2calculation1FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA2calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2calculation1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2F1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2F1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2Cl1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2Cl1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2P1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2P1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2calculation2FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA2calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2calculation2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2F2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2F2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2Cl2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2Cl2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2P2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2P2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2calculation3FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA2calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2calculation3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2F3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2F3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2Cl3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2Cl3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2P3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2P3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2calculation4FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA2calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2calculation4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2F4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2F4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2Cl4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2Cl4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2P4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2P4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            5
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2calculation5FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA2calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2calculation5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2F5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2F5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2Cl5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2Cl5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2P5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2P5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            6
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2calculation6FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA2calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2calculation6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2F6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2F6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2Cl6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2Cl6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA2P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA2P6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA2P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA2P6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="125">
                                                                A2 Remarks
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtA2remarks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                                    Width="100%"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtA2remarksFilteredTextBoxExtender1" runat="server"
                                                                    BehaviorID="txtA2remarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                    TargetControlID="txtA2remarks" ValidChars=" =.,()/-+*">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="BtnA2" CssClass="btn btn-primary" ValidationGroup="s2" runat="server"
                                                            formnovalidate Text="Save A2 Dissolution Test Result" OnClick="BtnA2_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane " id="A3controls" runat="server">
                                        <asp:Panel ID="A3Panel" runat="server" DefaultButton="BtnA3" ValidationGroup="A3">
                                            <fieldset>
                                                <div class="col-md-12 table-responsive">
                                                    <table>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="55">
                                                                A3
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
                                                                            Found (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Claim (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Percentage (In Mg)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation1FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA3calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P1FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation2FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA3calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P2FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation3FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA3calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3Cl3FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P3FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation4FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA3calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P4FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            5
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation5FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA3calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P5FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            6
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation6FilteredTextBoxExtender1"
                                                                                runat="server" BehaviorID="txtA3calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P6FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            7
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation7" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                                BehaviorID="txtA3calculation7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                                BehaviorID="txtA3F7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                                BehaviorID="txtA3Cl7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation8FilteredTextBoxExtender4"
                                                                                runat="server" BehaviorID="txtA3P7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            8
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation8" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation8FilteredTextBoxExtender5"
                                                                                runat="server" BehaviorID="txtA3calculation8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F8FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3F8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl8FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3Cl8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P8FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3P8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            9
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation9" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation9FilteredTextBoxExtender4"
                                                                                runat="server" BehaviorID="txtA3calculation9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F9FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3F9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl9FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3Cl9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P9FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3P9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            10
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation10" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation10FilteredTextBoxExtender4"
                                                                                runat="server" BehaviorID="txtA3calculation10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F10FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3F10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl10FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3Cl10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P10FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3P10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            11
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation11" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation11FilteredTextBoxExtender4"
                                                                                runat="server" BehaviorID="txtA3calculation11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F11FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3F11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl11FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3Cl11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P11FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3P11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            12
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3calculation12" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3calculation12FilteredTextBoxExtender4"
                                                                                runat="server" BehaviorID="txtA3calculation12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3calculation12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3F12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3F12FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3F12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3F12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3Cl12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3Cl12FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3Cl12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3Cl12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtA3P12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtA3P12FilteredTextBoxExtender4" runat="server"
                                                                                BehaviorID="txtA3P12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtA3P12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="125">
                                                                A3 Remarks
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtA3remarks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                                    Width="100%"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtA3remarksFilteredTextBoxExtender4" runat="server"
                                                                    BehaviorID="txtA3remarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                    TargetControlID="txtA3remarks" ValidChars=" =.,()/-+*">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="BtnA3" CssClass="btn btn-primary" ValidationGroup="A3" runat="server"
                                                            formnovalidate Text="Save A3 Dissolution Test Result" OnClick="BtnA3_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane" id="B1controls" runat="server">
                                        <asp:Panel ID="pnlb1" runat="server" DefaultButton="BtnS1" ValidationGroup="s1">
                                            <fieldset>
                                                <div class="col-md-12 table-responsive">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td style="font-size: large; font-weight: 700;" width="55">
                                                                            B1
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
                                                                                        <asp:TextBox ID="txtB1calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1calculation1FilteredTextBoxExtender4"
                                                                                            runat="server" BehaviorID="txtB1calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1calculation1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1F1FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1F1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1Cl1FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1Cl1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1P1FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1P1" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        2
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1calculation2FilteredTextBoxExtender4"
                                                                                            runat="server" BehaviorID="txtB1calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1calculation2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1F2FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1F2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1Cl2FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1Cl2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1P2FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1P2" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        3
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1calculation3FilteredTextBoxExtender4"
                                                                                            runat="server" BehaviorID="txtB1calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1calculation3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1F3FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1F3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1Cl3FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1Cl3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1P3FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1P3" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        4
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1calculation4FilteredTextBoxExtender4"
                                                                                            runat="server" BehaviorID="txtB1calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1calculation4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1F4FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1F4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1Cl4FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1Cl4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1P4FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1P4" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        5
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1calculation5FilteredTextBoxExtender4"
                                                                                            runat="server" BehaviorID="txtB1calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1calculation5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                                            BehaviorID="txtB1F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1F5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1Cl5FilteredTextBoxExtender5" runat="server"
                                                                                            BehaviorID="txtB1Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1Cl5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1P5FilteredTextBoxExtender5" runat="server"
                                                                                            BehaviorID="txtB1P55_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1P5" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        6
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                            runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1calculation6FilteredTextBoxExtender5"
                                                                                            runat="server" BehaviorID="txtB1calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1calculation6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1F6FilteredTextBoxExtender5" runat="server"
                                                                                            BehaviorID="txtB1F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1F6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1Cl6FilteredTextBoxExtender5" runat="server"
                                                                                            BehaviorID="txtB1Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1Cl6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtB1P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtB1P6FilteredTextBoxExtender5" runat="server"
                                                                                            BehaviorID="txtB1P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                            TargetControlID="txtB1P6" ValidChars=" =.,()/-+*">
                                                                                        </ajaxToolkit:FilteredTextBoxExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-size: large; font-weight: 700;" width="125">
                                                                            B1 Remarks
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB1Remrks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                                                Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                                BehaviorID="txtB1Remrks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB1Remrks" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="Btnb1" CssClass="btn btn-primary" runat="server" CausesValidation="false"
                                                            formnovalidate Text="Save B1 Dissolution Test Result" OnClick="Btnb1_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane" id="B2controls" runat="server">
                                        <asp:Panel ID="Panel2" runat="server" DefaultButton="BtnS2" ValidationGroup="A2">
                                            <fieldset>
                                                <div class="col-md-12 table-responsive">
                                                    <table>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="55">
                                                                B2
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
                                                                            Found (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Claim (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Percentage (In Mg)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2calculation1FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB2calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2calculation1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2F1FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2F1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2Cl1FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2Cl1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2P1FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2P1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2calculation2FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB2calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2calculation2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2F2FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2F2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2Cl2FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2Cl2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2P2FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2P2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2calculation3FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB2calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2calculation3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2F3FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2F3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2Cl3FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2Cl3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2P3FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2P3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2calculation4FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB2calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2calculation4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2F4FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2F4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2Cl4FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2Cl4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2P4FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2P4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            5
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2calculation5FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB2calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2calculation5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2F5FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2F5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2Cl5FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2Cl5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2P5FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2P5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            6
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2calculation6FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB2calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2calculation6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2F6FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2F6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2Cl6FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2Cl6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB2P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB2P6FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB2P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB2P6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="125">
                                                                B2 Remarks
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtB2remarks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                                    Width="100%"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtB2remarksFilteredTextBoxExtender6" runat="server"
                                                                    BehaviorID="txtB2remarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                    TargetControlID="txtB2remarks" ValidChars=" =.,()/-+*">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="BtnB2" CssClass="btn btn-primary" ValidationGroup="b2" runat="server"
                                                            formnovalidate Text="Save B2 Dissolution Test Result" OnClick="BtnB2_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                    <div class="tab-pane" id="B3controls" runat="server">
                                        <asp:Panel ID="b3Panel" runat="server" DefaultButton="BtnS3" ValidationGroup="s3">
                                            <fieldset>
                                                <div class="col-md-12 table-responsive">
                                                    <table>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="55">
                                                                B3
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
                                                                            Found (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Claim (In Mg)
                                                                        </td>
                                                                        <td>
                                                                            Percentage (In Mg)
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation1" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation1FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB3calculation1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F1FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3F1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl1FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3Cl1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P1" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P1FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3P1_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P1" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation2" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation2FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB3calculation2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F2FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3F2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl2FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3Cl2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P2" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P2FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3P2_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P2" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation3" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation3FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB3calculation3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F3FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3F3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl3FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3Cl3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P3" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                                BehaviorID="txtB3P3_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P3" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation4" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation4FilteredTextBoxExtender6"
                                                                                runat="server" BehaviorID="txtB3calculation4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F4FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3F4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl4FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3Cl4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P4" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P4FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3P4_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P4" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            5
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation5" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation5FilteredTextBoxExtender7"
                                                                                runat="server" BehaviorID="txtB3calculation5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F5FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3F5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl5FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3Cl5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P5" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P5FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3P5_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P5" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            6
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation6" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation6FilteredTextBoxExtender7"
                                                                                runat="server" BehaviorID="txtB3calculation6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F6FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3F6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl6FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3Cl6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P6" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P6FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3P6_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P6" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            7
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation7" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation7FilteredTextBoxExtender7"
                                                                                runat="server" BehaviorID="txtB3calculation7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F7FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3F7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                                BehaviorID="txtB3Cl7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P7" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P7FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3P7_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P7" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            8
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation8" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation8FilteredTextBoxExtender8"
                                                                                runat="server" BehaviorID="txtB3calculation8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F8FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3F8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl8FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3Cl8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P8" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P8FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3P8_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P8" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            9
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation9" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation9FilteredTextBoxExtender8"
                                                                                runat="server" BehaviorID="txtB3calculation9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F9FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3F9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl9FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3Cl9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P9" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P9FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3P9_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P9" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            10
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation10" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation10FilteredTextBoxExtender8"
                                                                                runat="server" BehaviorID="txtB3calculation10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F10FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3F10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl10FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3Cl10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P10" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P10FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3P10_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P10" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            11
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation11" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation11FilteredTextBoxExtender8"
                                                                                runat="server" BehaviorID="txtB3calculation11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F11FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3F11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl11FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3Cl11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P11" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P11FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3P11_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P11" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="style2">
                                                                            12
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3calculation12" CssClass="form-control" TextMode="MultiLine"
                                                                                runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3calculation12FilteredTextBoxExtender8"
                                                                                runat="server" BehaviorID="txtB3calculation12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3calculation12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3F12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3F12FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3F12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3F12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3Cl12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3Cl12FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3Cl12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3Cl12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtB3P12" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtB3P12FilteredTextBoxExtender8" runat="server"
                                                                                BehaviorID="txtB3P12_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                                TargetControlID="txtB3P12" ValidChars=" =.,()/-+*">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-size: large; font-weight: 700;" width="125">
                                                                B3 Remarks
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtB3remarks" CssClass="form-control" TextMode="MultiLine" runat="server"
                                                                    Width="100%"></asp:TextBox>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="txtB3remarksFilteredTextBoxExtender" runat="server"
                                                                    BehaviorID="txtB3remarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,custom,Numbers"
                                                                    TargetControlID="txtB3remarks" ValidChars=" =.,()/-+*">
                                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="row form-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="BtnB3" CssClass="btn btn-primary" ValidationGroup="b3" runat="server"
                                                            formnovalidate Text="Save B3 Dissolution Test Result" OnClick="BtnB3_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
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
