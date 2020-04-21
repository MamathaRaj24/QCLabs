<%@ Control Language="C#" AutoEventWireup="true" CodeFile="COMenu.ascx.cs" Inherits="Inspector_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="DashBoard.aspx"><i class="icon-dashboard"></i><span>Dashboard</span> </a></li>
                <li id="viewLi" runat="server"><a href="ViewSamples.aspx"><i class="icon-paste"></i><span>View Samples</span> </a></li>

                  <li id="code" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-qrcode"></i><span>Coding</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                    
                        <li><a href="GenerateStickers.aspx">Generate Code</a></li>
                         <li><a href="Qrcodeprint.aspx">Print QrCode</a></li>
                          <li><a href="Rt_Qrcodeprint.aspx">Reallotted-Print QrCode</a></li>
                    </ul>
                </li>
                               
                <li id="allot" runat="server"><a href="AllotToLab.aspx"><i class="icon-external-link"></i><span>Allot to Lab</span> </a></li>
                <li id="test" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class=" icon-beaker"></i><span>Reports</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <%--<li><a href="ViewTextReslt.aspx">View</a></li>--%>
                        <li><a href="PrintForm13.aspx">Form 13</a></li>
                         <li><a href="PrintForm34.aspx">Form 34</a></li>
                          <li><a href="RAPrintForm13.aspx">Reallotted Form 13</a></li>
                         <li><a href="RAPrintForm34.aspx"> Reallotted Form 34</a></li>
                         <li><a href="LabReport.aspx"> Lab Report</a></li>
                          <li><a href="SearchSamples_CO.aspx">Search Sample</a></li>
                    </ul>
                </li>
               
             


                <li id="usr" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>User Managment</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="#.aspx">Profile</a></li>
                         <li  id="liC" runat="server" class="dropdown"><a href="ChangePWD.aspx">Change Password</a></li>
                        <li><a href="../Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
