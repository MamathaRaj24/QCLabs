<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTextReslt.aspx.cs" Inherits="CodingOfficer_ViewTextReslt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/CodingOfficer/COMenu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title> Coding Officer--Print Form 13</title>
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
        history.pushState(null, null, 'ViewTextReslt.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'ViewTextReslt.aspx');
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
</head>
<body>
     <form id="form1" runat="server" class="form-horizontal">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <header:header id="header" runat="server" />
    <menu:menu id="menu" runat="server" />
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
                         View Test Result </h3>
                        </div>
                        <div class="widget-content">
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlmemo">
                                         Sample Id </label>
                                        <div class="controls">
                                    <asp:DropDownList ID="ddlSampleID" runat="server" AutoPostBack="true" 
                                                CssClass="form-control"                                    >
                                </asp:DropDownList>
                                                                   </div>
                                </div>
                            </div>
                            <asp:Panel ID="Div" runat="server">
                           
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlsample">
                                    Name of the Inspector</label>
                                    <div class="controls">
                                         <asp:Label ID="lblInspectName" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                         
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtRecvdDate">
                                            Whom the Sample Received</label>
                                    <div class="controls">
                                  <asp:Label ID="lblWhomReceived" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div> 
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlDiName">
                                       Sample Name</label>
                                    <div class="controls">
                                       <asp:Label ID="lblSampleName" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlcatgry">
                                 Memo Id</label>
                                    <div class="controls" style="font:in";>
                                         <asp:Label ID="lblMemoId" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlusage">
                                      Memo Date</label>
                                    <div class="controls">
                                        
                                    <asp:Label ID="lblmemoDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                              <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlusage">
                                     Batch Number</label>
                                    <div class="controls">
                                        
                                   <asp:Label ID="lblBatchNo" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                              <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlusage">
                                   Manufacturer Name</label>
                                    <div class="controls">
                                        
                                     <asp:Label ID="lblManuName" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                              <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlusage">
                                   Manufacturing Date</label>
                                    <div class="controls">
                                        
                                     <asp:Label ID="lblManuDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlpriority">
                             Expire Date
                                    </label>
                                    <div class="controls">
                                     
                                   
                                    <asp:Label ID="lblExpireDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtgrname">
                                     Date of Receipt</label>
                                    <div class="controls">
                                  
                                    <asp:Label ID="lblReceiptDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtgrname">
                                     Manufacutrer Licence</label>
                                    <div class="controls">
                                         
                                    <asp:Label ID="lblManuLicense" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddldrug">
                                      Address</label>
                                    <div class="controls">
                                       <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtgrname">
                                  Composition</label>
                                    <div class="controls">
                                          <asp:Label ID="lblComposition" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtqty">
                                    Generic Name</label>
                                    <div class="controls">
                                     <asp:Label ID="lblGenericName" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtBatchno">
                                            Trade Name</label>
                                    <div class="controls">
                                      
                                    <asp:Label ID="lblTradeName" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtManu">
                                       JA Remarks</label>
                                    <div class="controls">
                                     <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                             <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtManu">
                                       Description</label>
                                    <div class="controls">
                                      <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="txtLicenseNo">
                                     Status</label>
                                    <div class="controls">
                                       
                                   
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                           
                           
                            <div class="span12">
                           <div class="widget-content">
                            <asp:GridView ID="GvView" runat="server" CellPadding="4" GridLines="None" CssClass="table table-striped table-bordered"
                                AutoGenerateColumns="False">
                                <Columns>
                                            <asp:TemplateField HeaderText="Sl.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TestDone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatusId" Visible="false" runat="server" Text='<%# Bind("TestDone") %>' />
                                                    <asp:Label ID="lblParameter" runat="server" Text='<%# Bind("Parameter") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Found">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFound" runat="server" Text='<%# Bind("Found") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Claim">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNop" runat="server" Text='<%# Bind("claim") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Limit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("limit") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                            </asp:GridView>
                        </div>
                            </div>
                              </asp:Panel>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
   
    <asp:HiddenField ID="hf" runat="server" />
    <footer:footer id="footer" runat="server"></footer:footer>
    </form>
</body>
</html>
