<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu_CCODDA.ascx.cs" Inherits="Agri_Fertilizer_CCODDA_Menu_CCODDA" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="#"><i class="icon-dashboard"></i><span>Dashboard</span>
                </a></li>
                <li id="Li5" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>Qr Code</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li id="li6" runat="server"><a href="GenerateCode.aspx"><i></i><span>Genarate Qr Code
                        </span></a></li>
                    </ul>
                </li>
                <li id="Li1" runat="server"><a href="Laballoted.aspx"><i class="icon-dashboard"></i>
                    <span>Allot Lab</span> </a></li>
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
