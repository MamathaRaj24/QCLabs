<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPesticides.aspx.cs" Inherits="AO_EditPesticides" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/AO/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx"%>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>AO-Edit Pesticides Registration</title>
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
    <script type="text/javascript">
        history.pushState(null, null, 'EditPesticides.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'EditPesticides.aspx');
        });
    </script>
    <script type="text/javascript">


        $(document).ready(function () {
            $('#txtvlddt').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });
        $(document).ready(function () {
            $('#txtManufDt').datepicker({
                maxDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });
        $(document).ready(function () {
            $('#txtdtexpry').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });
        $(document).ready(function () {
            $('#txtinvdate').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+0"
            });
        });
        $(document).ready(function () {
            $('#txtsampldate').datepicker({
                maxDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });
        });
        $(document).ready(function () {
            $('#txtdtrcp').datepicker({
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
                                    <img src="../Assets/img/processing.gif" alt="loading...." />
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
                                DELETE / UPDATE PESTICIDE SAMPLE</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="Gvfreeze" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" 
                                onrowcommand="Gvfreeze_RowCommand" >
                               <Columns>
                                    <asp:TemplateField HeaderText="SlNo">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsampleregid" Visible="false" Text='<%# Bind("Sample_RegID") %>'
                                                runat="server"></asp:Label>
                                            <asp:Label ID="lblsmid" Text='<%# Bind("RegID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SampleCollectingDate" HeaderText="Date of Sampling" />
                                    <asp:BoundField DataField="ManfucaturerName" HeaderText="Manufactured By" />
                                    <asp:TemplateField HeaderText="Sample Category">
                                        <ItemTemplate>
                                         <asp:Label ID="lblsampleclass" Visible="false" Text='<%# Bind("Sample_Class") %>' runat="server"></asp:Label>
                                          <asp:Label ID="lblpaymenttype" Visible="false" Text='<%# Bind("PaymentType") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblcategoryid" Visible="false" Text='<%# Bind("category_id") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblcategoryname" Text='<%# Bind("Category_Name") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsampletypecode" Visible="false" Text='<%# Bind("SampleType_ID") %>'
                                                runat="server"></asp:Label>
                                            <asp:Label ID="lblsampletypename" Text='<%# Bind("SampleTypeName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsamplecode" Visible="false" Text='<%# Bind("Sample_ID") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblsamplename" Text='<%# Bind("SampleName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="State" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstacodecode" Visible="false" Text='<%# Bind("StateCode") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lbldistcode" Visible="false" Text='<%# Bind("DistCode") %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblmandcode" Visible="false" Text='<%# Bind("MandCode") %>' runat="server"></asp:Label>
                                             <asp:Label ID="lblclassid" Visible="false" Text='<%# Bind("class_id") %>' runat="server"></asp:Label>
                                              <asp:Label ID="lblpaymentid" Visible="false" Text='<%# Bind("PaymentType_ID") %>' runat="server"></asp:Label>
                                               <asp:Label ID="lbltypeid" Visible="false" Text='<%# Bind("Type_ID") %>' runat="server"></asp:Label>
                                               <%-- <asp:Label ID="Label4" Visible="false" Text='<%# Bind("MandCode") %>' runat="server"></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
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
                <asp:Panel ID="panelpesticide" runat="server">
               
                <div class="row">
                    <div class="widget ">
                        <div class="widget-header coler">
                            <i class="icon-reorder"></i>
                            <h3 align="center">
                               Edit Pesticide Registration</h3>
                                <h1 class="alert alert-info">
                                    <asp:Label ID="lblPesticedid" Visible="false" runat="server"></asp:Label>
                                </h1>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                            <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtcdsmpl">
                                            Sample Class
                                        </label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlClass" CssClass="form-control" runat="server" required>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtcdsmpl">
                                            C & DA Sticker No
                                        </label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtcdastkr" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="widget-header coler">
                            <i class="icon-reorder"></i>
                            <h3 align="center">
                                Dealer/Distributor/Manufacturing Unit Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtfirnm">
                                            Name of the Firm
                                        </label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtfirnm" CssClass="form-control" runat="server" required></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtnmowner">
                                            Name of the Dealer</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtnmdlr" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtlcnno">
                                            Licence No</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtlcnno" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtvlddt">
                                            Validity</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtvlddt" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtbthchno">
                                            State Name</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlstate" CssClass="form-control" AutoPostBack="true" runat="server"
                                                required OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="ddldist">
                                            District Name</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddldist" CssClass="form-control" AutoPostBack="true" runat="server"
                                                required>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="ddldist">
                                            Division
                                        </label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddldvsn" CssClass="form-control" AutoPostBack="true" runat="server"
                                                required>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="ddldist">
                                            Mandal
                                        </label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlmandal" CssClass="form-control" AutoPostBack="true" runat="server"
                                                required>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txthsno">
                                            House No</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txthsno" CssClass="form-control" runat="server" required></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtlcstr">
                                            Locality/Road Name</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtlocality" CssClass="form-control" runat="server" required></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <%--<div class="widget-header coler">
                            <i class="icon-user"></i>
                            <h3 align="center">
                                Sample/Marking Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtdtmn">
                                            Date of Manufacture</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtdtmn" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtstockl">
                                            Stock position of the Lot</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtstockl" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtdmipagency">
                                            Date of Receipt of the stock by the dealer/manufacture/importer/pool handling agency</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtdmipagency" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="ddlstate">
                                            State Name</label>
                                        <div class="controls">
                                          
                                        </div>
                                    </div>
                                </div>
                               
                            </fieldset>
                        </div>--%>
                        <div class="widget-header coler">
                            <i class="icon-reorder"></i>
                            <h3 align="center">
                                Sample/Marking Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtdtsam">
                                            Date of Sampling
                                        </label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtsampldate" CssClass="form-control" runat="server" required></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="ddlsampleType">
                                            Pesticide Type</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlsampleType" runat="server" CssClass="form-control" 
                                                AutoPostBack="true" onselectedindexchanged="ddlsampleType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="ddlSample">
                                            Name of Pesticide</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlSample" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtgrntdAI">
                                            Gaurenteed % of A.I</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtgrntdAI" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtTradeNm">
                                            Trade Name</label>
                                        <div class="controls">
                                          
                                             <asp:TextBox ID="txtTradeNm" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtsdnlsis">
                                            Size of Sample drawn for analysis</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtsdnlsis" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="ddlunits">
                                            Units</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlunits" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtbatchno">
                                            Batch No</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtbatchno" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtstockpostion">
                                            Stock Position</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtstockpostion" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtManufDt">
                                            Date of Manufacturing</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtManufDt" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtdtexpry">
                                            Date of Expiry</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtdtexpry" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtinvoiceNo">
                                            Invoice No</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtinvoiceNo" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtinvdate">
                                            Invoice Date</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtinvdate" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtdtrcp">
                                            Date of Receipt</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtdtrcp" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtStkRcvdFrm">
                                            Stock Received from whom</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtStkRcvdFrm" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="widget-header coler">
                            <i class="icon-reorder"></i>
                            <h3 align="center">
                                Manufacturer Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtmnname">
                                            Name of the Manufacturer</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtmnname" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtaddrs">
                                            Address</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtmnaddress" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtisi">
                                            ISI Mark position(if any)</label>
                                        <div class="controls">
                                        <asp:RadioButtonList ID="RdbIsimark" runat="server" CssClass="radio-inline" RepeatDirection="Vertical">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="txtmrketername">
                                            Name of the Marketer</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtmrketername" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="widget-header coler">
                            <i class="icon-reorder"></i>
                            <h3 align="center">
                                Panchanama Report/Details</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class="span4">
                                    <div class="control-group">
                                        <label class="control-label" for="Rblphnma">
                                            whether Panchanama Conducted</label>
                                        <div class="controls">
                                            <asp:RadioButtonList ID="Rdbpunchanama" runat="server" RepeatDirection="Vertical">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                         <div class="form-actions">
                            <div class="control-group">
                                <asp:Button ID="btnUpdate" formnovalidate="formnovalidate" 
                                    Text="Update Pesticides" CausesValidation="false" CssClass="btn btn-primary"
                                    runat="server" onclick="btnUpdate_Click"/>
                                <asp:Label ID="lblsmpleid" runat="server" Visible="false"></asp:Label>
                            </div>
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
