<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="UnitOfficer_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="Li1" runat="server"><a href="Dashboard.aspx"><i class="icon-dashboard"></i><span>
                    DashBoard</span> </a></li>
                <li id="Ack_Unit" runat="server"><a href="Ack.aspx"><i class=" icon-check"></i><span>
                    Acknowledgement</span> </a></li>
                <li id="Analyst_Unit" runat="server"><a href="AllotToAnalyst.aspx"><i class="icon-share">
                </i><span>Allot to Analyst</span> </a></li>

               
                 
                <%--  <li id="Li2" runat="server"><a href="RevertedSample_UO.aspx"><i class="icon-share">
                </i><span>Revert Allot to Analyst</span> </a></li>--%>
                <%--<li id="Li2" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="TestResult.aspx">Enter Test Result</a></li>
                        <li><a href="EditTestResult.aspx">Edit/ Freeze Result</a></li>
                        <li><a href="FreezeTestResult.aspx">Freeze & Submit Result </a></li>
                    </ul>
                </li>--%>
                <li id="ResultLi" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>View Test Result</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="ViewTestResult.aspx">Save and Submit Test Results</a></li>
                        <li><a href="Printview_UO.aspx">Print Preview </a></li>
                        <li><a href="Signature_UO.aspx">Freeze Test Results</a></li>
                      
                        <li><a href="SignPrintForm13.aspx">Print Form-13 </a></li>
                        <li><a href="PrintForm34.aspx">Print Form-34 </a></li>
                       
                    </ul>
                </li>
                 <li id="Reallot" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Reallot View Test Result</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">  
                        <li><a href="RA_ViewTestResult.aspx">Realloted Save and Submit Test Results</a></li>
                        <li><a href="RA_Printview_UO.aspx">Realloted Print Preview </a></li>
                        <li><a href="RA_FreezeTestresult_UO.aspx">Reallotd Freeze Test Results</a></li>  
                        <li><a href="RA_PrintForm13.aspx">Realloted Print Form-13 </a></li>
                        <li><a href="RAPrintForm34.aspx">Realloted Print Form-34 </a></li>
                    </ul>
                </li>
                <li id="Li2" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Search</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="SearchSamples.aspx">Search Samples</a></li>
                        <li><a href="RA_SearchSamples.aspx">Realloted Searh Samples </a></li> 
                       
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
