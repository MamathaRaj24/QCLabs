﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeedRegistrationAO.aspx.cs"
    Inherits="Agri_AO_SeedRegistrationAO" %>

<%@ Register TagPrefix="Menu" TagName="menu" Src="~/Agri/AO/Menu.ascx" %>
<%@ Register TagPrefix="Header" TagName="header" Src="~/Agri/Header.ascx" %>
<%@ Register TagPrefix="Footer" TagName="footer" Src="~/Agri/Footer.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AO-Seed Registration</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link href="../../Assets/css/font.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../../Assets/css/style.css" rel="stylesheet" type="text/css" />
    <script src="../../Assets/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Assets/js/bootstrap.js"></script>
    <script type="text/javascript" src="../../Assets/js/base.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../Assets/js/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Assets/css/Jquery_UI.css" />
    <script type="text/javascript">
        window.history.forward(1);
        function noBack() {
            window.history.forward();
        }
        function Confirm(link) {
            if (confirm("Are you sure to delete the row?")) {
                return true;
            }
            else
                return false;

        }  
    </script>
    <script type="text/javascript">
        history.pushState(null, null, 'SeedRegistrationAO.aspx');
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null, 'SeedRegistrationAO.aspx');
        });
    </script>
    <script type="text/javascript">


        $(document).ready(function () {
            $('#txtdateexpiry').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-0:+10"
            });
        });
        $(document).ready(function () {
            $('#txtdateofcollection').datepicker({
                maxDate: new Date(),
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "-80:+10"
            });

        });


    </script>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <Header:header ID="header" runat="server" />
    <Menu:menu ID="menu" runat="server" />
    <div class="main">
        <div class="main-inner">
            <div class="container" style="margin-top: -30px;">
                <div class="widget-header">
                    <i class="icon-pushpin"></i>
                    <h3>
                        Logged In As :
                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                    </h3>
                    <h3 style="float: right">
                        Date:<asp:Label ID="lblDate" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="row">
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <img src="../../Assets/img/processing.gif" alt="loading...." />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget ">
                        <div class="widget-header coler">
                            <i class="icon-reorder "></i>
                            <h3 align="center">
                                Seed Registration</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Sample Class <span style="color: Red">*</span>
                                    </label>
                                    <label class="col-lg-2">
                                        <asp:DropDownList ID="ddlClass" CssClass="form-control" AutoPostBack="true" runat="server"
                                            required OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label class="col-lg-2">
                                        Code No. of the Sample
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtcodesample" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server" required></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server"
                                            BehaviorID="txtcodesample_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtcodesample" ValidChars=" /.,()-'">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-lg-2">
                                        Date of collection: <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtdateofcollection" CssClass="form-control" AutoComplete="off"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        District Name <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddldist" CssClass="form-control" AutoPostBack="true" runat="server"
                                            required OnSelectedIndexChanged="ddldist_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Mandal Name <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlmandal" CssClass="form-control" AutoPostBack="true" runat="server"
                                            required OnSelectedIndexChanged="ddlmandal_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Village Name <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlvlg" CssClass="form-control" runat="server" required>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <asp:Panel ID="pnlvigilence" runat="server" Visible="false">
                            <div class="widget-header coler">
                                <i class="icon-reorder "></i>
                                <h3 align="center">
                                    Vigilance Sample Submission Check List:</h3>
                                <p class="text-danger">
                                    &nbsp;&nbsp;&nbsp;&nbsp; In case of submition of a Vigilance Sample following is the
                                    check list
                                </p>
                                <ol style="color: Red">
                                <li>Panchanama</li>
                                <li>FIR Copy</li>
                                <li>Letter from the SHO/CI of Police Department
                                    with complete postel address and mobile number and mail id of the concerned SHO/CI </li>
                               </ol>
                               
                                <p class="text-danger">
                                    &nbsp;&nbsp;&nbsp;&nbsp; The Scanned copies of documents 1,2 & 3 should be uploaded
                                    in portel,hovever the original documents should be submitted in person along with intact sample immediately</p>
                            </div>
                            <div class="widget-content">
                                <fieldset>
                                    <div class=" form-group">
                                        <label class="col-lg-2">
                                            Upload Panchanama Copy <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-lg-2">
                                            <asp:FileUpload ID="Filepanchanama" runat="server" />
                                        </div>
                                        <label class="col-lg-2">
                                            Upload FIR Copy <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-lg-2">
                                            <asp:FileUpload ID="FileFricopy" runat="server" />
                                        </div>
                                        <label class="col-lg-2">
                                            Upload letter from SHO/CI of Police Department Copy <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-lg-2">
                                            <asp:FileUpload ID="FileSHOCI" runat="server" />
                                        </div>
                                    </div>
                                    <div class=" form-group">
                                        <label class="col-lg-2">
                                            Name of SHO/CI<span style="color: Red">*</span>
                                        </label>
                                        <div class="col-lg-2">
                                            <asp:TextBox ID="txtshorci" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                BehaviorID="txtshorci_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                TargetControlID="txtshorci" ValidChars="/,-,">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                        <label class="col-lg-2">
                                            Postel Address <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-lg-2">
                                            <asp:TextBox ID="txtposteladdress" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtposteladdressFilteredTextBoxExtender6"
                                                runat="server" BehaviorID="txtposteladdress_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                TargetControlID="txtposteladdress" ValidChars="/,-,">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                        <label class="col-lg-2">
                                            Mobile Number <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-lg-2">
                                            <asp:TextBox ID="txtmobileno" CssClass="form-control" MaxLength="10" AutoComplete="off"
                                                runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtmobilenoFilteredTextBoxExtender6" runat="server"
                                                BehaviorID="txtmobileno_FilteredTextBoxExtender" FilterType="Numbers" TargetControlID="txtmobileno">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class=" form-group">
                                        <label class="col-lg-2">
                                            Email id <span style="color: Red">*</span>
                                        </label>
                                        <div class="col-lg-2">
                                            <asp:TextBox ID="txtemail" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="txtemailFilteredTextBoxExtender2" runat="server"
                                                BehaviorID="txtemail_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                                TargetControlID="txtemail" ValidChars="/,-,@.">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </asp:Panel>
                        <div class="widget-header coler">
                            <i class="icon-reorder "></i>
                            <h3 align="center">
                                Nature of Article submitted for analysis/test:</h3>
                        </div>
                        <div class="widget-content">
                            <fieldset>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Kind and Variety of seed <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlkindVrtyofseed" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="HB">Hybrid</asp:ListItem>
                                            <asp:ListItem Value="Vr">Variety</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Crop Name <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlcrop" CssClass="form-control" AutoPostBack="true" runat="server"
                                            required OnSelectedIndexChanged="ddlcrop_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Crop Variety <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlcropvariety" CssClass="form-control" AutoPostBack="true"
                                            runat="server" required>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Lot No <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtlotno" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtlotnoFilteredTextBoxExtender1" runat="server"
                                            BehaviorID="txtlotno_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtlotno" ValidChars="/,-,">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-lg-2">
                                        Quantity of seed in the lot Quntity <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtquantitylot" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtquantitylotFilteredTextBoxExtender1"
                                            runat="server" BehaviorID="txtquantitylot_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtquantitylot">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                    <label class="col-lg-2">
                                        Quantity of sample submit( in grms) <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtqntysamplesubmit" CssClass="form-control" AutoComplete="off"
                                            runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtqntysamplesubmitFilteredTextBoxExtender2"
                                            runat="server" BehaviorID="txtqntysamplesubmit_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtqntysamplesubmit">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Orgin or Class of seed <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:DropDownList ID="ddlorginclass" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="FS">FS</asp:ListItem>
                                            <asp:ListItem Value="CS">CS</asp:ListItem>
                                            <asp:ListItem Value="TL">TL</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-lg-2">
                                        Date of Expiry <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtdateexpiry" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <label class="col-lg-2">
                                        Remarks <span style="color: Red">*</span>
                                    </label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtremarks" CssClass="form-control" AutoComplete="off" MaxLength="50"
                                            runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="txtremarksFilteredTextBoxExtender2" runat="server"
                                            BehaviorID="txtremarks_FilteredTextBoxExtender" FilterType="UppercaseLetters, LowercaseLetters,Numbers,custom"
                                            TargetControlID="txtremarks">
                                        </ajaxToolkit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class=" form-group">
                                    <label class="col-lg-2">
                                        Test required(if any)
                                    </label>
                                    <div class="col-lg-4">
                                        <asp:CheckBoxList ID="chktestparametrs" runat="server" RepeatDirection="Vertical">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="col-lg-4">
                                        <p class="text-danger">
                                            For Bt.Analysis Separate Sample of 25gr has to be submitted.</p>
                                        <p class="text-danger">
                                            For Maistore analysis a separate sample of 100gr in maisture proof bags container
                                            has be submitted
                                        </p>
                                    </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btnadd" formnovalidate="formnovalidate" Text="Save" class="btn btn-primary "
                            CausesValidation="false" runat="server" OnClick="btnadd_Click" />
                        <asp:Label ID="lblgrcode" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hf" runat="server" />
    <Footer:footer ID="footer" runat="server"></Footer:footer>
    </form>
</body>
</html>
