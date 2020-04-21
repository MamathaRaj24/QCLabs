<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeoMenu.ascx.cs" Inherits="Inspector_Menu" %>
<div class="subnavbar">
    <div class="subnavbar-inner">
        <div class="container">
            <ul class="mainnav">
                <li id="ack" runat="server"><a href="CourierAck.aspx"><i class=" icon-share"></i><span>Courier Acknowledgement</span> </a>
                </li>
                <li id="smpl" runat="server"><a href="EnterSampleDetails.aspx"><i class="icon-edit"></i><span>Enter Sample Details</span> </a>
                </li>
                
                  <li id="usr" runat="server" class="dropdown"><a href="javascript:;" class="dropdown-toggle"
                    data-toggle="dropdown"><i class="icon-user-md "></i><span>User Managment</span>
                    <b class="caret"></b></a>
                    <ul class="dropdown-menu">
                        
                         <li  id="liC" runat="server" class="dropdown"><a href="ChangePWD.aspx"> <i class=" icon-share"></i><span>Change Password</span></a></li>
                        <li><a href="../Logout.aspx"><i class=" icon-signout "></i><span>Logout</span> </a>
                        </li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
</div>
