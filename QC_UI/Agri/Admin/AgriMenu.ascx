<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AgriMenu.ascx.cs" Inherits="AgriAdmin_AgriMenu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="db" runat="server"><a href="DashBoard.aspx"><i class="icon-dashboard"></i><span>
                    Dashboard</span> </a></li>
                <li id="liagri" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-group"></i><span>User Management</span> <b class="caret">
                    </b></a>
                    <ul class="dropdown-menu">
                        <li><a href="Designation.aspx">Deignations</a></li>
                        <li><a href="Employee_Mst.aspx">Employee</a></li>
                        <li><a href="AddUser.aspx">Add User </a></li>
                        <li><a href="UserList.aspx">User List</a></li>
                    </ul>
                </li>
                <li id="Smpl" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-leaf"></i><span>Fertilizer Masters</span> <b
                        class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <%-- <li><a href="SampleCategory.aspx">Sample Category</a></li>--%>
                        <li><a href="SampleType.aspx">Sample Types</a></li>
                        <li><a href="AgriSample.aspx">Samples</a></li>
                        <li><a href="Specifications.aspx">Specifications</a></li>               
                        <li><a href="#">Rejection Reasons</a></li>
                    </ul>
                </li>
                  <%--<li id="Li4" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-table "></i><span>Seed Masters</span> <b
                        class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="SampleType.aspx">Sample Types</a></li>
                        <li><a href="AgriSample.aspx">Samples</a></li>
                        <li><a href="Specifications.aspx">Specifications</a></li>
                        <li><a href="#">Rejection Reasons</a></li>
                    </ul>
                </li>--%>
                <li id="TargetLi" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-table "></i><span>Target Allotment</span> <b
                        class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="TargetAllotment.aspx">Add</a></li>
                        <li><a href="Edit_TargetAllotment.aspx">Edit/Delete</a></li>
                        <li><a href="Freeze_TargetAllotment.aspx">Freeze</a></li>
                      
                    </ul>
                </li>
              
               <li id="labli" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-beaker "></i><span>Lab Masters</span> <b
                        class="caret"></b></a>
                    <ul class="dropdown-menu">
                        <li><a href="AddLabs.aspx">Add Labs</a></li>
                        <li><a href="EquipmentMaster.aspx">Equipment</a></li>  
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
