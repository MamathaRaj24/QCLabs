<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu_CCOAO.ascx.cs" Inherits="Agri_CodingCenter_Menu_AO" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="#"><i class="icon-dashboard"></i><span>Dashboard</span>
                </a></li>
                <li id="liAck" runat="server"><a href="Ack.aspx"><i class="icon-check"></i><span>Acknowledgment
                </span></a></li>
                <li id="LiView" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>Accept Reject</span> <b
                        class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="ViewSample.aspx">View Sample</a></li>
                        <li><a href="ViewMemo.aspx">View Memo</a></li>
                    </ul>
                </li>
                <%--  <li id="li3" runat="server"><a href="ViewSample.aspx"><i></i><span>View Sample</span></a></li>--%>
                <li id="li2" runat="server"><a href="InformationSheet.aspx"><i></i><span>Information
                    Sheet </span></a></li>
                <li id="li1" runat="server"><a href="CourierEntry.aspx"><i></i><span>Courier Entry </span>
                </a></li>
                <li id="usr" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>User Managment</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="#">Profile</a></li>
                        <li><a href="#">Change Password</a></li>
                        <li><a href="../../Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
