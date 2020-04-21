<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTestResult.aspx.cs" Inherits="DCA_Analyst_ViewTestResult" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Analyst/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Analyst - View Test Results</title>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,600italic,400,600"
        rel="stylesheet" type="text/css" />
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
    </script>
    <style type="text/css">
        .Text-Bold
        {
            font-weight: bold;
            color: #3a87ad;
        }
    </style>
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
                                            <asp:Button ID="BtnView" runat="server" CommandName="ViewResult" formnovalidate Text="ViewResult" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="DivViewData" runat="server">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    View Test Results</h3>
                                <asp:Label ID="lblsamplid" runat="server" CssClass="alert alert-info Text-Bold"></asp:Label>
                            </div>
                            <div class="widget-content">
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2 ">
                                        Laboratory
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbllab" runat="server" CssClass="Text-Bold"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2 ">
                                        Tested By
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbljaName" runat="server" CssClass="Text-Bold"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Name of the drug
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbldrugnm" runat="server" CssClass="Text-Bold"></asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Manufacturing Date
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblmfgdt" runat="server" CssClass="Text-Bold"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Expiry Date
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblexpdt" runat="server" CssClass="Text-Bold"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Quantity Received
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblqty" runat="server" CssClass="Text-Bold"></asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Test Started on
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="txtStartDt" runat="server" CssClass="Text-Bold" />
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Test Completed on
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="txtEndDt" runat="server" CssClass="Text-Bold" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Composition
                                    </label>
                                    <div class="col-md-8 col-lg-8">
                                        <asp:Label ID="lblComposition" runat="server" CssClass="Text-Bold"></asp:Label>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Description
                                    </label>
                                    <div class="col-md-2 col-lg-6">
                                        <asp:Label ID="txtDesc" runat="server" CssClass="Text-Bold" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        Junior Analyst Remarks
                                    </label>
                                    <div class="col-md-2 col-lg-6">
                                        <asp:Label ID="txtremarks" runat="server" CssClass="Text-Bold" />
                                    </div>
                                </div>
                                <asp:GridView ID="GvResult" runat="server" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField HeaderText="Test Parameter" DataField="Parameter" />
                                        <asp:BoundField HeaderText="Test Name (In Case of Others)" DataField="TestName" />
                                        <asp:BoundField HeaderText="Calculation" DataField="Calculation" />
                                        <asp:BoundField HeaderText="Test Protocol" DataField="ProtocolName" />
                                        <asp:BoundField HeaderText="Found" DataField="found" />
                                        <asp:BoundField HeaderText="Claim" DataField="claim" />
                                        <asp:BoundField HeaderText="Limit" DataField="limit" />
                                        
                                        <asp:TemplateField HeaderText="Unit Officer Remarks" Visible="false">
                                        <ItemTemplate>
                                        <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"></asp:TextBox>
                                      
                                         <asp:Label ID="lbltestids" runat="server" Visible="false"  Text='<%# Eval("TestID") %>' ></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="col-md-12 table-responsive" id="divs1" runat="server" visible="false">
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
                                                                        <asp:Label ID="txtS1calculation1" CssClass="form-control" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1F1" CssClass="form-control" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1Cl1" CssClass="form-control" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1P1" CssClass="form-control" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        2
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtS1calculation2" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1F2" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1Cl2" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1P2" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        3
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtS1calculation3" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1F3" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1Cl3" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1P3" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        4
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtS1calculation4" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1F4" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1Cl4" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1P4" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        5
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtS1calculation5" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1F5" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1Cl5" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1P5" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        6
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txtS1calculation6" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1F6" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1Cl6" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="txts1P6" runat="server" CssClass="form-control" />
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
                                                            <asp:Label ID="txts1Remrks" runat="server" CssClass="form-control" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-12 table-responsive" id="divs2" runat="server" visible="false">
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
                                                            <asp:Label ID="txts2calculation1" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2F1" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2Cl1" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2P1" CssClass="form-control" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            2
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2calculation2" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2F2" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2Cl2" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2P2" CssClass="form-control" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            3
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2calculation3" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2F3" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2Cl3" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2P3" CssClass="form-control" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            4
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2calculation4" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2F4" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2Cl4" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2P4" CssClass="form-control" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            5
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2calculation5" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2F5" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2Cl5" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2P5" CssClass="form-control" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            6
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2calculation6" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2F6" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2Cl6" CssClass="form-control" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts2P6" CssClass="form-control" runat="server" />
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
                                                <asp:Label ID="txts2remarks" CssClass="form-control" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-12 table-responsive" id="divs3" visible="false" runat="server">
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
                                                        <td>
                                                            1
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation1" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F1" CssClass="form-control" runat="server"> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl1" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P1" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            2
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation2" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F2" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl2" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P2" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            3
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation3" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F3" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl3" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P3" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            4
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation4" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F4" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl4" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P4" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            5
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation5" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F5" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl5" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P5" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            6
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation6" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F6" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl6" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P6" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            7
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation7" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F7" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl7" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P7" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            8
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation8" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F8" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl8" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P8" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            9
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation9" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F9" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl9" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P9" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            10
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation10" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F10" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl10" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P10" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            11
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation11" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F11" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl11" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P11" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            12
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3calculation12" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3F12" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3Cl12" CssClass="form-control" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txts3P12" CssClass="form-control" runat="server"></asp:Label>
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
                                                <asp:Label ID="txts3remarks" CssClass="form-control" runat="server"></asp:Label>
                                            </td>
                                    </table>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hf" runat="server" />
            <Footer:footer ID="footer" runat="server"></Footer:footer>
        </div>
    </div>
    </form>
</body>
</html>
