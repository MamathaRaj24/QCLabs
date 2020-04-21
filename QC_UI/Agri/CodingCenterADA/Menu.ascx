<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Agri_CodingCenterADA_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="#"><i class="icon-dashboard"></i><span>Dashboard</span> </a></li>
                <li id="view" runat="server"><a href="ViewSample.aspx"><i class="icon-screenshot "></i><span>View Samples</span> </a></li>
              
               <li id="liCdGen" runat="server"><a href="GenerateCode.aspx"><i class="icon-qrcode"></i><span>Code Generation </span></a></li>
             
               <li id="usr" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>User Managment</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="#">Profile</a></li>
                        <li><a href="#">Change Password</a></li>
                        <li><a href="../Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
