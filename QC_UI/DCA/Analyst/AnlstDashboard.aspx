<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnlstDashboard.aspx.cs" Inherits="DCA_Analyst_AnlstDashboard" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Analyst/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script type="text/javascript" src="../../Assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript" src="../../Assets/js/MonthPicker.js"></script>
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
                        <div class="widget-header coler">
                            <i class="icon-dashboard"></i>
                            <h3 align="center">
                                Dash Board
                            </h3>
                        </div>
                        <div class="widget-content">
                            <div class=" form-group">
                                <div class="col-lg-3">
                                    Samples Alloted<br />
                                    <img src="../../Assets/img/samreg.png" />
                                    <b>
                                        <asp:Label ID="lblsamplesReg" runat="server"></asp:Label></b>
                                </div>
                                <div class="col-lg-3">
                                    Sample Tested<br />
                                    <img src="../../Assets/img/accepted.png" />
                                    <b>
                                        <asp:Label ID="lblAccepted" runat="server"></asp:Label></b>
                                </div>
                                <%-- <div class="col-lg-2">
                                    As per the standered<br />
                                    <img src="../../Asets/images/rejected.png" />
                                    <b>
                                        <asp:Label ID="lblRejected" runat="server"></asp:Label></b>
                                </div>
                                <div class="col-lg-2">
                                    Not as per the standered<br />
                                    <img src="../../Asets/images/SamplesTested.png" />
                                    <b>
                                        <asp:Label ID="lblSamTested" runat="server"></asp:Label></b>
                                </div>--%>
                                <label class="col-lg-3">
                                    Re-alloted<br />
                                    <img src="../../Assets/img/Norms.png" />
                                    <asp:Label ID="lblnoc" runat="server"></asp:Label>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
