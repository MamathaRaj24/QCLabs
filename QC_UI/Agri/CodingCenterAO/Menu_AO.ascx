﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu_AO.ascx.cs" Inherits="Agri_CodingCenter_Menu_AO" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="#"><i class="icon-dashboard"></i><span>Dashboard</span>
                </a></li>
                <li id="liAck" runat="server"><a href="Ack.aspx"><i class="icon-check"></i><span>
                    Acknowledgment </span></a></li>
              <li id="li1" runat="server"><a href="#"><i class="icon-check"></i><span>
                    Courier Entry </span></a></li>
                
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