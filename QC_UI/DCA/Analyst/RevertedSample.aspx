<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RevertedSample.aspx.cs" Inherits="DCA_Analyst_RevertedSample" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Analyst/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Junior Analyst-Reverted Samples </title>
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
                                Reverted Unit Officer</h3>
                        </div>
                        <div class="widget-content">
                         <div class="span4"  runat="server"  >
                                <div class="control-group">
                                    <label class="control-label" for="ddlAnalyst">
                                        Select Sample</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlsample" CssClass="form-control" required runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlCategory">
                                        Remarks</label>
                                    <div class="controls">
                                      <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                         
                            <div class="clearfix">
                            </div>
                            <div class="row form-group">
                                <div class="col-md-6 text-center offset2">
                                    <asp:Button ID="btnreverted" CssClass="btn btn-primary" CausesValidation="false"
                                        runat="server" Text="Reverted to UnitOfficer" 
                                        onclick="btnreverted_Click" />
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <Footer:footer ID="footer" runat="server" />
    </form>
</body>
</html>
