<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewSample.aspx.cs" Inherits="Agri_Fertilizer_CCOAO_ViewSample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/Fertilizer/CCOAO/Menu_CCOAO.ascx" %> 
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Fertilizer/CCOAO/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Fertilizer/CCOAO/Footer.ascx"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CCOAO-ViewSample</title>
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
    <script type="text/javascript" src="../../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
       
    <script type="text/javascript">

        history.pushState(null, null, 'ViewSample.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'ViewSample.aspx');
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#rdbaccept input').click(function () {
                var value = $("#rdbaccept input:radio:checked").val();
                if (value == "A") {
                  
                    $("#ddlrejctreson").hide();
                    $("#btnSave").show(); 
                    $('#lblresons').hide();
                    $('#BtnReject').hide();
                }
                if (value == "R") { 
                    $("#ddlrejctreson").show();
                    $("#lblresons").show(); 
                    $("#BtnReject").show();
                    $("#btnSave").hide(); 
                    $("#lblresons").text('Rejected Resons');

                }

            }); 

        });
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').hide();
            $('#BtnReject').hide();
            $('#ddlrejctreson').hide();
            $('#lblresons').hide();
        });
    </script>
    <script language="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=ddlrejctreson.ClientID%>").value == "") {
                alert("Please Select Rejected Resons");
                document.getElementById("<%=ddlrejctreson.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
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
                                View Sample</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="gvviewsample" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvviewsample_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsampleid" runat="server" Text='<%# Bind("RegID") %>'> 
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SampleType Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsampletype" runat="server" Text='<%# Bind("SampleTypeName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsamplename" Text='<%# Bind("SampleName") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Sampling">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmandate" Text='<%# Bind("SampleCollectingDate") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkview" runat="server" Text="VIEW" CommandName="VIEW"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnltestresult" runat="server" Visible="false">
                    <div class="row">
                        <div class="widget ">
                            <div class="widget-header">
                                <i class="icon-user"></i>
                                <h3 align="center">
                                    View Sample</h3>
                                <div class="alert alert-info">
                                    <asp:Label ID="lblsampleid" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="widget-content">
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Sample Class:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblSamcls" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Code No. of Sample:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblcodenoofsample" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Memo ID:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblmemoid" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="widget-header coler">
                                    <i class="icon-tasks"></i>
                                    <h3 align="center">
                                        Dealer-Distributor Details</h3>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Licence No:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbllcnsno" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Name of the Firm:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblnameofthfirm" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Name of the Owner:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblnameoftheowner" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Validity :</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblvalidity" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Batch No:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblbatchno" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Stock position of the Lot:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblstckpositionofthelot" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">State Name:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblstatename" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">District Name :</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbldistname" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">House No\Flat No:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblhsflatno" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Locality/Street Name:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbllocality" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Date of Receipt of the stock by the dealer/manufacture/importer/pool
                                            handling agency:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lbldatercpt" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="widget-header coler">
                                    <i class="icon-tasks"></i>
                                    <h3 align="center">
                                        Sample Marking Details</h3>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Sample Collecting Date:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblSamColDate" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Sample Type:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblsampletype" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Sample Grade:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblsamplegradename" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Physical Condition :</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblphysicalcondition" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="widget-header coler">
                                    <i class="icon-tasks"></i>
                                    <h3 align="center">
                                        Manufacturer Details</h3>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Date of Manufacturing:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblmanfdate" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">Manufacture Name:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblManName" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small">MManufacturer State:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblmanustate" runat="server"></asp:Label>
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
                                        <b style="font-size: small">Whether Samples drwan from:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:Label ID="lblsampledrwanfrom" runat="server"></asp:Label>
                                    </div>
                                    <label class="col-md-2 col-lg-2 col-lg-2 ">
                                        <b style="font-size: small">Whether Panchanama Conducted:</b>
                                    </label>
                                    <div class="col-md-2 col-lg-2 col-lg-2">
                                        <asp:Label ID="lblpanchanama" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <asp:ScriptManager ID="scriptmanager1" runat="server">
                                </asp:ScriptManager>
                                <div class="form-group">
                                   <%-- <label class="col-md-2 col-lg-2">
                                        <b style="font-size: small"></b>
                                        Sample is:
                                    </label>--%>
                                    <div class="col-md-2 col-lg-2">
                                        <asp:RadioButtonList ID="rdbaccept" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="A">Accept</asp:ListItem>
                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <label class="col-md-2 col-lg-2 col-lg-2 ">
                                        <asp:Label ID="lblresons" runat="server"></asp:Label>
                                    </label>
                                    <div class="col-md-2 col-lg-2 col-lg-2">
                                        <asp:DropDownList ID="ddlrejctreson" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="span6 offset5">
                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" Text="Save"
                                        CausesValidation="false" formnovalidate="formnovalidate" runat="server" OnClick="btnSave_Click" />

                                        <asp:Button ID="BtnReject" CssClass="btn btn-danger" Text="Reject" OnClientClick=" return validate()"
                                        CausesValidation="false" formnovalidate="formnovalidate" runat="server" 
                                        onclick="BtnReject_Click"/>
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
