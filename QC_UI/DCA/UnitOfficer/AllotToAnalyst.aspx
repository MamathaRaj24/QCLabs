<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllotToAnalyst.aspx.cs" Inherits="UnitOfficer_AllotToAnalyst" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/UnitOfficer/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unit Officer- Sample Allot to Analyst</title>
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
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'AllotToAnalyst.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'AllotToAnalyst.aspx');
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.txtDisDate').datepicker({
                dateFormat: 'dd-mm-yy',
                maxDate: new Date(),
                changeMonth: true,
                changeYear: true,
                yearRange: "-90:+0"
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
                                    <img src="../Assets/img/processing.gif" alt="loading...." />
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
                                Allott Sample to a Analyst</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="ddlCategory">
                                        Select Sample Id</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlsample" CssClass="form-control" required runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <div class="control-group">
                                    <label class="control-label" for="username">
                                        Alloted Analyst</label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="Rdbidlist" CssClass=" radio radio-inline" AutoPostBack="true"
                                            RepeatDirection="Vertical" runat="server" OnSelectedIndexChanged="Rdbidlist_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Analyst</asp:ListItem>
                                            <asp:ListItem Value="1">JSO</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="span4" id="divid" runat="server" visible="false">
                                <div class="control-group">
                                    <label class="control-label" for="ddlAnalyst">
                                        Select Analyst</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlAnalyst" CssClass="form-control" required runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="span4" id="divjso" runat="server" visible="false">
                                <div class="control-group">
                                    <label class="control-label" for="ddljso">
                                        Select JSO</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddljso" CssClass="form-control" required runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="clearfix">
                            </div>
                            <div class="row form-group">
                                <div class="col-md-6 text-center offset2">
                                    <asp:Button ID="btnAllot" CssClass="btn btn-primary" Visible="false" CausesValidation="false"
                                        runat="server" Text="Allot To Analyst" OnClick="btnAllot_Click" />
                                </div>
                                <div class="col-md-6 text-center">
                                    <asp:Button ID="Btnjso" CssClass="btn btn-primary" Visible="false" CausesValidation="false"
                                        runat="server" Text="Allot To JSO" OnClick="Btnjso_Click" />
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
