<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintMemoLetter.aspx.cs" Inherits="AO_PrintMemoLetter" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/AO/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AO-Print Memo Letter</title>
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
    <script type="text/javascript">
        history.pushState(null, null, 'PrintMemoLetter.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'PrintMemoLetter.aspx');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <Header:header ID="header" runat="server" />
    <Menu:menu ID="menu" runat="server" />
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
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
                                Print Form J</h3>
                        </div>
                        <div class="widget-content">
                            <div class="span5">
                                <div class="control-group">
                                    <label class="radio-inline" for="username">
                                        Sample Category</label>
                                    <div class="controls example">
                                        <asp:RadioButtonList class="radio inline" ID="RblCategory" runat="server" AutoPostBack="True"
                                            RepeatDirection="Vertical" OnSelectedIndexChanged="RblCategory_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="Gvprintformj" runat="server" CssClass="table table-striped table-bordered"
                                GridLines="None" AutoGenerateColumns="False" OnRowCommand="Gvprintformj_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sample Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampId" Text='<%# Eval("Memo_ID") %>' runat="server"> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Date Of Sampling">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSampDate" runat="server" Text='<%# Bind("SampleDate") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Category_Name" HeaderText="Sample Category" />
                                    <asp:BoundField DataField="SampleTypeName" HeaderText="Sample Type" />
                                    <asp:BoundField DataField="SampleName" HeaderText="Sample" />
                                    <asp:BoundField DataField="class_name" HeaderText="Sample Class" />
                                    <asp:TemplateField HeaderText="Memo Letter">
                                        <ItemTemplate>
                                            <asp:Button ID="btnprintj" runat="server" CommandName="Print-j" formnovalidate Text="Print-Memo_Letter" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="pnlprint" Visible="false" runat="server">
                              
                                    <h1 class="alert alert-info">
                                        <asp:Label ID="lblSampleId" runat="server"> </asp:Label>
                                    </h1>
                               
                                <div class="span12">
                                    <rsweb:ReportViewer ID="Rpt_PrintFormJ" Width="100%" Height="20px" align="center"
                                        runat="server" SizeToReportContent="true">
                                    </rsweb:ReportViewer>
                                </div>
                            </asp:Panel>
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
