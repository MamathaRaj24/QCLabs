<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LabOfficerMenu.ascx.cs" Inherits="Agri_Fertilizer_LabOfficer" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
               
             <li id="db" runat="server"><a href="#"><i class="icon-dashboard"></i><span>
                    Dashboard</span> </a></li>
             <li id="Li1" runat="server"><a href="EquipmentDetails.aspx"><i class="icon-dashboard"></i><span>
                 Equipment Details</span> </a></li>            
             <li id="Li2" runat="server"><a href="Ack.aspx"><i class="icon-dashboard"></i><span>
                 Ack</span> </a></li>
                  <li id="Li3" runat="server"><a href="AddAnalyst.aspx"><i class="icon-dashboard"></i><span>
                 Add Analyst</span> </a></li>
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