<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Admin_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">

               <%-- <li><a href="DashBoard.aspx"><i class="icon-dashboard"></i><span>Dashboard</span> </a></li>--%>
              

                <li id="db" runat="server"><a href="DashBoard.aspx"><i class="icon-dashboard"></i><span>Dashboard</span> </a></li>

                <li id="liDca" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md"></i><span>Employee</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="Designation.aspx">Deignations</a></li>
                        <li><a href="Zone_Mst.aspx">Zones</a></li>
                        <li><a href="Employee_Mst.aspx">Employee</a></li>
                    </ul>
                </li>
                <li id="Smpl" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-table "></i><span>Sample Masters</span> <b
                        class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="SampleCategory.aspx">Sample Category</a></li>
                        <li><a href="SampleType.aspx">Sample Types</a></li>
                        <li><a href="Sample.aspx">Samples</a></li>
                        <li><a href="Specifications.aspx">Specifications</a></li>
                        <%--<li><a href="#">Rejection Reasons</a></li>--%>
                    </ul>
                </li>
                <li id="lab" runat="server"><a href="AddLabs.aspx"><i class="icon-beaker"></i><span>
                    Add Labs </span></a></li>
                <li id="usr" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-group"></i><span>User Managment</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="AddUser.aspx">Add User</a></li>
                        <li><a href="EditUser.aspx">Edit User</a></li>
                        <li><a href="Userlist.aspx">User List</a></li>
                        <li><a href="ResetPwd.aspx">Reset Password</a></li>
                    </ul>
                </li>
                <li><a href="../Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                </li>
            </ul>
        </div>
    </div>
</div>
