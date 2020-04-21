<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SeedAdminMenu.ascx.cs"
    Inherits="Agri_SeedAdmin_SeedAdminMenu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="DashBoard.aspx"><i class="icon-dashboard"></i><span>
                    Dashboard</span> </a></li>
                <li id="Smpl" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-table "></i><span>Seed Masters</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="CropMaster.aspx">Crop Master</a></li>
                        <li><a href="CropVarietyMaster.aspx">CropVariety </a></li>
                        <li><a href="TestMaster.aspx">Test Master</a></li>
                        <li><a href="TestParameters.aspx">Specifications</a></li>
                        <li><a href="#">Rejection Reasons</a></li>
                    </ul>
                </li>
                <li id="Li2" runat="server"><a href="../ChangePWD.aspx"><i class="icon-edit"></i><span>
                    Change Password</span></a></li>
                <li><a href="Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                </li>
            </ul>
        </div>
    </div>
</div>
