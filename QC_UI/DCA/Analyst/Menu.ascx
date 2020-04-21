<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Analyst_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">

                <li id="db" runat="server"><a href="AnlstDashboard.aspx"><i class="icon-dashboard"></i><span>Dashboard</span> </a></li>

                   <%-- <li id="Li2" runat="server"><a href="RevertedSample.aspx"><i class="icon-table"></i><span>Reverted Sample</span> </a></li>

                <li id="Ack_Unit" runat="server"><a href="Ack.aspx"><i class="icon-table"></i><span>Acknowledgement</span> </a></li>--%>


                 <li id="Ack_Unit" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-table"></i><span>Ack / Revert</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="Ack.aspx">Acknowledge</a></li>
                        <li><a href="RevertedSample.aspx">Revert</a></li> 
                    </ul>
                </li>


                <li id="Analyst_Unit" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="TstRslt.aspx">Enter Test Result</a></li>
                        <li><a href="EditTestResult.aspx">Edit Result</a></li> 
                        <li><a href="FreezeTestResult.aspx">Freeze & Submit Result </a></li> 
                    </ul>
                </li>
                <li id="RA" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Reallotted Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="RA_TestResult.aspx">Enter Test Result</a></li> 
                         <li><a href="RAEditTR.aspx">Re-Alloted Edit Result</a></li> 
                        <li><a href="RAFreezeTR.aspx">Re-Alloted Freeze & Submit Result </a></li>
                    </ul>
                </li>

                 <li id="Li3" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Reports</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="Viewtestresultrpt.aspx">View Test Result</a></li>
                         <li><a href="RAViewtestresultrpt.aspx">Re-Allot View Test Result</a></li>
                         <li><a href="ViewSampleAllot.aspx">view sample alloted</a></li>
                         <li><a href="SearchSamples.aspx">Search Samples</a></li>
                          <li><a href="RA_SearchSamples.aspx">Re Alloted Search Samples</a></li>
                    </ul>
                </li>
          <%--    <li id="vtr" runat="server"><a href="Viewtestresultrpt.aspx"><i class="icon-table"></i><span>View Test Result</span> </a></li>--%>

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
