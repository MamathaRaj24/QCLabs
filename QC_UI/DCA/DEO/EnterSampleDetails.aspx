<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnterSampleDetails.aspx.cs"
    Inherits="DEO_EnterSampleDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/DEO/DeoMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DEO-Sample Registration</title>
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
        $(document).ready(function () {
            $('#txtRecvdDate').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });


        $(function () {
            $('input.monthpicker').monthpicker({ changeYear: true, minDate: "-5 Y", maxDate: "0 M" });
        });
        $(function () {
            $('input.monthpicker1').monthpicker({ changeYear: true, minDate: "-0 Y", maxDate: "+6 Y" });
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
                                Enter Received Sample Details</h3>
                        </div>
                        <div class="widget-content">
                            <div class=" form-group">
                                <label class="col-lg-2">
                                    Select Memo ID <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlmemo" CssClass="form-control" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlmemo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                       <asp:Label ID="lblmemoid" runat="server" Visible="false"></asp:Label>
                                </div>
                                <label class="col-lg-2">
                                    Select Sample Id <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlsample" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblsampleid" runat="server" Visible="false"></asp:Label>
                                </div>
                                <label class="col-lg-2">
                                    Sample Received On <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtRecvdDate" AutoComplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" form-group">
                                <label class="col-lg-2">
                                    Sample Sent By <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlDiName" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-lg-2">
                                    Sample Category <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlcatgry" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-lg-2">
                                    Usage <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlusage" CssClass="form-control" runat="server" required>
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Human">Human</asp:ListItem>
                                        <asp:ListItem Value="Veterinary">Veterinary</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class=" form-group">
                                <label class="col-lg-2">
                                    Priority <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:DropDownList ID="ddlpriority" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-lg-2">
                                    Reason & Remarks for Top Priority
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtremarks" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-lg-2">
                                    Trade Name <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtTradeName" CssClass="form-control" required runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" form-group">
                                <label class="col-lg-2">
                                    Generic Name<span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtgnrname" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-lg-2">
                                    Quantity Received<span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtqty" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-lg-2">
                                    Manufacturer Name
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtManu" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" form-group">
                                <label class="col-lg-2">
                                    LicenseNo
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtLicenseNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-lg-2">
                                    Marketed By
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtMrktBy" MaxLength="12" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="col-lg-2">
                                    Date of Manufacturing<span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtdtmn" CssClass="form-control monthpicker" AutoComplete="off"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" form-group">
                                <label class="col-lg-2">
                                    Date of Expiry<span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtdtexpry" CssClass="form-control monthpicker1" AutoComplete="off"
                                        runat="server"></asp:TextBox>
                                </div>
                                <label class="col-lg-2">
                                    Compostion <span style="color: Red">*</span>
                                </label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtCompostion" TextMode="MultiLine" CssClass="form-control" Height="80px"
                                        runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group right">
                                 
                                    
                                    <asp:Label ID="lbldiname" runat="server" Visible="false"></asp:Label>
                                    <asp:Button ID="btnAck" CssClass="btn btn-primary" CausesValidation="false" runat="server"
                                        formnovalidate="formnovalidate" Text="Save Details" OnClick="btnAck_Click" />
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" CausesValidation="false" formnovalidate="formnovalidate"
                                        runat="server" Text="Update Details" Visible="false" OnClick="btnUpdate_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget-content">
                        <asp:GridView ID="GvSamples" runat="server" CellPadding="4" GridLines="None" CssClass="table table-striped table-bordered"
                            AutoGenerateColumns="False" OnRowCommand="GvSamples_RowCommand1">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Memo Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMemoId" runat="server" Text='<%# Bind("Memo_ID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleId" runat="server" Text='<%# Bind("SampleID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sample Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSampleCat" runat="server" Text='<%# Bind("Category_Name") %>'>
                                            <asp:Label ID="lblcatid" runat="server" Visible="false" Text='<%# Bind("Category") %>'>
                                            </asp:Label>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name Of Drug">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrugName" runat="server" Text='<%# Bind("NameOfDrug") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReceiveDate" runat="server" Visible="false" Text='<%# Bind("RcvdDt") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lbldrugusername" runat="server" Visible="false" Text='<%# Bind("Name") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lbldruguserid" runat="server" Visible="false" Text='<%# Bind("AddedBy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Batch No" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("BatchNo") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryId" runat="server" Visible="false" Text='<%# Bind("Category") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("Category_Name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("Priority") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lblusage" runat="server" Text='<%# Bind("Usage") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Generic Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGeneric" runat="server" Text='<%# Bind("GenericNm") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="Manufacture Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManuDate" runat="server" Text='<%# Bind("ManufDate") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expiry Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpireDate" runat="server" Text='<%# Bind("ExpiryDt") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mfg LicenceNo" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMfgLicenceNo" runat="server" Text='<%# Bind("MfgLicenceNo") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manufactured By" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManuBy" runat="server" Text='<%# Bind("ManufacturedBy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marketed By" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarketBy" runat="server" Text='<%# Bind("MarketedBy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Composition" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComposition" runat="server" Text='<%# Bind("Composition") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sent By">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSentBy" runat="server" Visible="false" Text='<%# Bind("SentBy") %>'>
                                        </asp:Label>
                                        <asp:Label ID="lblSentname" runat="server" Text='<%# Bind("NAME") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("PriorityRemarks") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnedit" runat="server" formnovalidate CommandName="edt" Text="Edit" />
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
    <asp:HiddenField ID="hf" runat="server" />
    <Footer:footer ID="footer" runat="server"></Footer:footer>
    </form>
</body>
</html>
