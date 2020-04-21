<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="DCA_JSO_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="Li1" runat="server"><a href=""><i class="icon-dashboard"></i><span>DashBoard</span>  </a></li>
                <li id="Ack_Unit" runat="server"><a href="Ack.aspx"><i class=" icon-check"></i><span> Acknowledgement</span> </a></li>
                <li id="Analyst_Unit" runat="server"><a href="AllotToAnalyst.aspx"><i class="icon-share"> </i><span>Allot to Analyst</span> </a></li>

                  <li id="Li2" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="TestResult.aspx">Enter Test Result</a></li>
                        <li><a href="EditTestResult.aspx">Edit/ Freeze Result</a></li>
                        <li><a href="FreezeTestResult.aspx">Freeze & Submit Result </a></li>
                    </ul>
                </li>

                 <li id="ResultLi" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="ViewTestResult.aspx">View Test Result</a></li>
                        <li><a href="PrintForm13.aspx">Print Form-13 </a></li>
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
