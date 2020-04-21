<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Inspector_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="Dashboard.aspx"><i class="icon-dashboard"></i><span>Dashboard</span> </a></li>

                <li id="Smpl_DCA" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-table"></i><span>Sample Registration</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="Sample_Registration_DI.aspx">Enter Sample Details</a></li>
                        <li><a href="EditSample_DI.aspx">Update Details</a></li>
                        <li><a href="Freeze_DI.aspx">Freeze</a></li>
                    </ul>
                </li>
                <li id="liMemo" runat="server"><a href="MemoGeneration.aspx"><i class="icon-list-alt"> </i><span>Memo Generation </span></a></li>


                <li id="Li1" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class=" icon-truck"></i><span>Sample Sent Details</span> <b
                        class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="Samplebyhand.aspx">Sample By Hand</a></li>
                        <li><a href="CourierEntry.aspx">Postal Entry</a></li>
                    </ul>
                </li>

                <li id="Print_DCA" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class=" icon-print"></i><span>Print</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="PrintMemoLetter.aspx">Memo Letter</a></li>
                        <li><a href="PrintForm1718.aspx">Form 18</a></li>
                    </ul>
                </li>
                 <li id="Li3" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <%--<li><a href="TestResult_Anlst.aspx">View Result</a></li>--%>
                        <li><a href="PrintForm13.aspx">Form 13</a></li>
                        <li><a href="PrintForm34.aspx">Form 34</a></li>
                    </ul>
                </li>
                 <li id="Li2" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>Reallot Test Result</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <%--<li><a href="TestResult_Anlst.aspx">View Result</a></li>--%>
                        <li><a href="RAPrintForm13.aspx">Form 13</a></li>
                         <li><a href="RAPrintForm34.aspx">Form 34</a></li>
                    </ul>
                </li>
                
                <li id="usr" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker"></i><span>User Managment</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        
                        <li  id="liC" runat="server" class="dropdown"><a href="ChangePWD.aspx">Change Password</a></li>
                        <li><a href="../Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
