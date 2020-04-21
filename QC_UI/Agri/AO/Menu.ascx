<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="AO_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="#"><i class="icon-dashboard"></i><span>Dashboard</span> </a></li>
              
                <li id="Smpl_Agri" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-table"></i><span>Sample Registration</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="SampleRegistration_AO.aspx">Enter Sample Details</a></li>
                        <li><a href="EditSample_AO.aspx">Update Details</a></li>
                        <li><a href="Freeze_AO.aspx">Freeze</a></li>
                    </ul>
                </li>
                <li id="liMemo" runat="server"><a href="MemoGeneration.aspx"><i class="icon-list-alt">
                </i><span>Memo Generation </span></a></li>
                <li id="li1" runat="server"><a href="CourierEntry.aspx"><i class="icon-truck">
                </i><span>Courier Entry </span></a></li>
               
                <%-- <li id="liCor" runat="server"><a href="CourierEntry.aspx"><i class="icon-truck"></i><span>Courier Entry </span></a></li>--%>
                <li id="Print_Agri" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-print"></i><span>Print</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="PrintCourier.aspx">Courier </a></li>
                        <li><a href="PrintMemoLetter.aspx">Memo Letter</a></li>
                        <li><a href="PrintformJ.aspx">Form J </a></li>
                        <li><a href="FormP.aspx">Form P </a></li>
                        <li><a href="FormK.aspx">Form K</a></li>
                    </ul>
                </li>
               
            <%--    <li id="Li2" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="TestResult_Anlst.aspx">Test Result</a></li>
                        <li><a href="#.aspx">Form13</a></li>
                        
                    </ul>
                </li>--%>
                <li id="usr" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>User Managment</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="#.aspx">Profile</a></li>
                        <li><a href="../ChangePWD.aspx">Change Password</a></li>
                        <li><a href="../Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
