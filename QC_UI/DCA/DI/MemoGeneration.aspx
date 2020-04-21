<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemoGeneration.aspx.cs" Inherits="DI_MemoGeneration" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/DI/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin-Memo Generation</title>
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
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <Header:header ID="header" runat="server" />
    <Menu:menu ID="menu" runat="server" />
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
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
                                Memo Generation</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class="form-group">
                                    <label class="col-lg-4">
                                        Select Sample Id
                                    </label>
                                    <div class="col-lg-4">
                                        <asp:DropDownList ID="ddlSamId" CssClass="form-control" runat="server" required OnSelectedIndexChanged="ddlSamId_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <asp:Panel ID="pnltestresult" runat="server" Visible="false">
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Sample Class</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblSamcls" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Usage</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblUsage" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Payment Type</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Priority</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblPriority" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Remarks:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="widget-header coler">
                                        <i class="icon-tasks"></i>
                                        <h3 align="center">
                                            Dealer-Distributor Details</h3>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Type Of Firm:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblfirmtype" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Name of the Firm:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblFirmName" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">License Number:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblLicNo" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Validity:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblValidity" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Contact Person:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Mobile No:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Quantity Picked:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">State:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblState" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">District:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblDistrict" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Mandal:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblMandal" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">House No:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblHouse" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Locality:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblLocality" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="widget-header coler">
                                        <i class="icon-tasks"></i>
                                        <h3 align="center">
                                            Sample Marking Details</h3>
                                    </div>
                                    <div class="form-group">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Sample Collecting Date</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblSamColDate" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Dosage Form:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblSamCategory" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Generic Name</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblGenName" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Trade Name:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblTrade" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Batch No:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblBatchNo" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Manufacture Date:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblManuDate" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Expire Date:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblExpDt" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Stock Recover Date:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblstkRecDt" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Sample Quantity:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblSamQty" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Composition:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-6">
                                            <asp:Label ID="lblCom" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="widget-header coler">
                                        <i class="icon-tasks"></i>
                                        <h3 align="center">
                                            Manufacturer/ Marketer Details</h3>
                                    </div>
                                    <div class="form-group">
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Manufacture State:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblManSt" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Manufacture Name:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblManName" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Manufacture License:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblManuLicense" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Manufacture Address:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblmanuAddress" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Marketer State:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblMarketerState" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 col-lg-2 col-lg-2 ">
                                            <b style="font-size: small">Marketer Name:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2 col-lg-2">
                                            <asp:Label ID="lblMarketerName" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 col-lg-2">
                                            <b style="font-size: small">Marketer Address:</b>
                                        </label>
                                        <div class="col-md-2 col-lg-2">
                                            <asp:Label ID="lblMarketerAddress" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </fieldset>
                            <div class="row form-group">
                                <div class="row form-group">
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" CausesValidation="false" runat="server"
                                            Text="Generate Memo" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="row form-group">
                                    <div class="col-md-12 text-center">
                                        <rsweb:ReportViewer ID="Rpt_PrintMemo" Width="100%" Height="20px" Visible="false"
                                            align="center" runat="server" SizeToReportContent="true">
                                        </rsweb:ReportViewer>
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
