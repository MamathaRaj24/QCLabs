<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSamples.aspx.cs" Inherits="CodingOfficer_ViewSamples" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/CodingOfficer/COMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coding Officer-View Samples</title>
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
        history.pushState(null, null, 'ViewSamples.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'ViewSamples.aspx');
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
            $('#<%=BtnAccept.ClientID %>').hide();
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
                                    <img src="../Assets/img/processing.gif" alt="loading...." />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="widget ">
                        <div class="widget-header coler">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                View Sample Received & DI Entered Details
                            </h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Select Memo ID
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlmemo" CssClass="form-control" required runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlmemo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Select Sample Id
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlsample" CssClass="form-control" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlsample_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                            <asp:Panel ID="ViewDiv" runat="server">
                                <div class="span6">
                                    <div class="page-header bar">
                                        <h4 class=" text-primary">
                                            Details As DI Entered
                                        </h4>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtRecvdDate">
                                                Sample Category</label>
                                            <div class="controls">
                                                <asp:Label ID="lblcategory" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="ddlDiName">
                                                Usage</label>
                                            <div class="controls">
                                                <asp:Label ID="lblusage" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="ddlcatgry">
                                                Payment Type</label>
                                            <div class="controls">
                                                <asp:Label ID="lblPayment" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="ddlusage">
                                                Priority</label>
                                            <div class="controls">
                                                <asp:Label ID="lblPriority" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="ddlpriority">
                                                Reason for Top Priority
                                            </label>
                                            <div class="controls">
                                                <asp:Label ID="lblremarks" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="page-header bar">
                                        <h4 class=" text-primary">
                                            Sample Marking Details
                                        </h4>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtgrname">
                                                Sample Collecting Date</label>
                                            <div class="controls">
                                                <asp:Label ID="lblsampledt" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="ddldrug">
                                                Sample Type</label>
                                            <div class="controls">
                                                <asp:Label ID="lbltherptic" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtgrname">
                                                Name of Sample/Drug</label>
                                            <div class="controls">
                                                <asp:Label ID="lbldrugnm" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtLicenseNo">
                                                Batch No</label>
                                            <div class="controls">
                                                <asp:Label ID="lblbatchno" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtqty">
                                                Generic Name</label>
                                            <div class="controls">
                                                <asp:Label ID="lblgenericnm" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                   
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtManu">
                                                Composition</label>
                                            <div class="controls">
                                                <asp:Label ID="lblcomposition" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                  
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtMrktBy">
                                                Date of Manufacturing</label>
                                            <div class="controls">
                                                <asp:Label ID="lblmanudt" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtdtexpry">
                                                Date of Expiry</label>
                                            <div class="controls">
                                                <asp:Label ID="lblexpdt" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Date of Stock Recived</label>
                                            <div class="controls">
                                                <asp:Label ID="lblstkrcvd" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Sample Qty sent for analysis</label>
                                            <div class="controls">
                                                <asp:Label ID="lblqty" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="page-header bar">
                                        <h4 class=" text-primary">
                                            Manufacturer/ Importer Details
                                        </h4>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Manufacturer State</label>
                                            <div class="controls">
                                                <asp:Label ID="lblmanufactureState" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Name of Manufacturer / Importer</label>
                                            <div class="controls">
                                                <asp:Label ID="lblmanuNm" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Manufacturer / Importer License No</label>
                                            <div class="controls">
                                                <asp:Label ID="lblManuLicence" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Address</label>
                                            <div class="controls">
                                                <asp:Label ID="lbladdress" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="page-header bar">
                                        <h4 class=" text-primary">
                                            Dealer-Distributor Details</h4>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Type of Firm</label>
                                            <div class="controls">
                                                <asp:Label ID="lblfirmtype" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Name of the Firm</label>
                                            <div class="controls">
                                                <asp:Label ID="lblfirmNm" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Licence No</label>
                                            <div class="controls">
                                                <asp:Label ID="lblLicenseNo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Validity</label>
                                            <div class="controls">
                                                <asp:Label ID="lblvalidity" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Type of Firm</label>
                                            <div class="controls">
                                                <asp:Label ID="lblcontactNm" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                   
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Invoice No</label>
                                            <div class="controls">
                                                <asp:Label ID="lblInvoice" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                   
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                State Name</label>
                                            <div class="controls">
                                                <asp:Label ID="lblstate" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                District Name</label>
                                            <div class="controls">
                                                <asp:Label ID="lbldist" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Mandal</label>
                                            <div class="controls">
                                                <asp:Label ID="lblMand" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                House No</label>
                                            <div class="controls">
                                                <asp:Label ID="lblhno" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Locality/Road Name</label>
                                            <div class="controls">
                                                <asp:Label ID="lblloality" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Mobile / Phone No</label>
                                            <div class="controls">
                                                <asp:Label ID="lblmobile" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                E-Mail ID</label>
                                            <div class="controls">
                                                <asp:Label ID="lblmail" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="span5">
                                    <div class="page-header bar">
                                        <h4 class=" text-primary">
                                            Details As DEO Entered</h4>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Sample Category</label>
                                            <div class="controls">
                                                <asp:Label ID="lblCategorys" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Usage</label>
                                            <div class="controls">
                                                <asp:Label ID="lblUsages" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Sent By</label>
                                            <div class="controls">
                                                <asp:Label ID="lblNAME" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Priority</label>
                                            <div class="controls">
                                                <asp:Label ID="lblPrioritys" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Reason for Priority</label>
                                            <div class="controls">
                                                <asp:Label ID="lblpriorityRsn" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="page-header ">
                                        <h4 class=" text-primary">
                                            Sample Marking Details</h4>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Sample Received Date</label>
                                            <div class="controls">
                                                <asp:Label ID="lblRcvdDt" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Therapeutic Category</label>
                                            <div class="controls">
                                                <asp:Label ID="lblSampleType" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Sample Name</label>
                                            <div class="controls">
                                                <asp:Label ID="lblDrugName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Quantity Receivede</label>
                                            <div class="controls">
                                                <asp:Label ID="lblqtyRcvd" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Batch Number</label>
                                            <div class="controls">
                                                <asp:Label ID="lblBtchNo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Compostion</label>
                                            <div class="controls">
                                                <asp:Label ID="lblCptn" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Generic Name</label>
                                            <div class="controls">
                                                <asp:Label ID="lblGenricNm" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Compostion</label>
                                            <div class="controls">
                                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Manufacturer Name</label>
                                            <div class="controls">
                                                <asp:Label ID="lblManufName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                License No</label>
                                            <div class="controls">
                                                <asp:Label ID="lblLicenseNos" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Date of Manufacturing</label>
                                            <div class="controls">
                                                <asp:Label ID="lblManuFDt" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Date of Expiry</label>
                                            <div class="controls">
                                                <asp:Label ID="lblExpiryDt" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Marketed By</label>
                                            <div class="controls">
                                                <asp:Label ID="lblMarketedBy" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="span6">
                                        <div class="control-group">
                                            <label class="control-label" for="txtCompostion">
                                                Remarks</label>
                                            <div class="controls">
                                                <asp:Label ID="lblRemarkes" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="span6">
                                    <div class="control-group">
                                        <label class="control-label" for="txtCompostion">
                                            Accept</label>
                                        <div class="controls">
                                            <asp:RadioButtonList ID="Rdbaccept" runat="server" CssClass="radio-inline" AutoPostBack="true"
                                                OnSelectedIndexChanged="Rdbaccept_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="span6" id="regreasonid" runat="server">
                                    <div class="control-group">
                                        <label class="control-label" for="txtCompostion">
                                            Rejecting Reason</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtReasons" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="span6 text-center offset2">
                            <div class="control-group right">
                                <asp:Button ID="BtnAccept" CssClass="btn btn-primary" Visible="false" CausesValidation="false"
                                    runat="server" formnovalidate="formnovalidate" Text="ACCEPT" OnClick="BtnAccept_Click" />
                                <asp:Button ID="BtnReject" CssClass="btn btn-danger" Visible="false" CausesValidation="false"
                                    runat="server" Text="REJECT" OnClick="BtnReject_Click" />
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
