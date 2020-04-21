<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Userlist.aspx.cs" Inherits="DCA_Admin_Userlist" %>


<%@ Register TagPrefix="Menu" TagName="menu" Src="~/DCA/Admin/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/DCA/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/DCA/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Admin-UserList</title>
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
          history.pushState(null, null, 'Userlist.aspx');
          window.addEventListener('popstate', function (event) {
              history.pushState(null, null, 'Userlist.aspx');
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

    <form id="form1" runat="server">
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
                                User List</h3>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="GVUsers" runat="server" CssClass="table table-bordered table-hover"
                                GridLines="None" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>' />
                                            <asp:Label ID="lblid" Text='<%# Eval("id") %>' Visible="false" runat="server"> </asp:Label>
                                            <asp:Label ID="lblUserCode" Text='<%# Eval("User_Code") %>' Visible="false" runat="server"> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role">
                                        <ItemTemplate>
                                            <asp:Label ID="lblroleid" Visible="false" runat="server" Text='<%# Bind("Role") %>' />
                                            <asp:Label ID="lblRole" runat="server" Text='<%# Bind("RoleName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstate" Visible="false" runat="server" Text='<%# Bind("Statecode") %>' />
                                            <asp:Label ID="lblstatenm" runat="server" Text='<%# Bind("StateNm") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lbldistcode" Visible="false" runat="server" Text='<%# Bind("Dist_Cd") %>' />
                                            <asp:Label ID="lbldistname" runat="server" Text='<%# Bind("DistName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mandal">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmandcode" Visible="false" runat="server" Text='<%# Bind("Mandal_Cd") %>' />
                                            <asp:Label ID="lblmandNm" runat="server" Text='<%# Bind("MandName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Zone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblzoneid" Visible="false" runat="server" Text='<%# Bind("Zone_Cd") %>' />
                                            <asp:Label ID="lblZone" runat="server" Text='<%# Bind("ZoneName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lab Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllabid" Visible="false" runat="server" Text='<%# Bind("Lab_ID") %>' />
                                            <asp:Label ID="lbllab" runat="server" Text='<%# Bind("LabName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UserName">
                                        <ItemTemplate>
                                        <asp:Label ID="lblUserName" CssClass="text-primary"  runat="server" Text='<%# Bind("UserName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                  
                                </Columns>
                            </asp:GridView>
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
