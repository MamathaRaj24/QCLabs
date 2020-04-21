<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>QC | LABS</title>
    <link rel="stylesheet" href="Asets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="Asets/css/theme_default.css" />
    <link rel="stylesheet" href="Asets/css/owl.theme.css" />
    <link rel="stylesheet" href="Asets/css/owl.carousel.css" />
    <link rel="stylesheet" href="Asets/css/settings.css" media="screen" />
    <link rel="stylesheet" href="Asets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Asets/css/easy-responsive-tabs.css" />
    <link rel="stylesheet" href="Asets/css/style.css" />
    <link rel="stylesheet" href="Asets/css/responsive.css" />
    <link rel="shortcut icon" type="image/png" href="Asets/images/favicon.png" />
    <script src="Asets/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //-- Plugin implementation
        (function ($) {
            $.fn.countTo = function (options) {
                return this.each(function () {
                    //-- Arrange
                    var FRAME_RATE = 60; // Predefine default frame rate to be 60fps
                    var $el = $(this);
                    var countFrom = parseInt($el.attr('data-count-from'));
                    var countTo = parseInt($el.attr('data-count-to'));
                    var countSpeed = $el.attr('data-count-speed'); // Number increment per second

                    //-- Action
                    var rafId;
                    var increment;
                    var currentCount = countFrom;
                    var countAction = function () {              // Self looping local function via requestAnimationFrame
                        if (currentCount < countTo) {              // Perform number incremeant
                            $el.text(Math.floor(currentCount));     // Update HTML display
                            increment = countSpeed / FRAME_RATE;    // Calculate increment step
                            currentCount += increment;              // Increment counter
                            rafId = requestAnimationFrame(countAction);
                        } else {                                  // Terminate animation once it reaches the target count number
                            $el.text(countTo);                      // Set to the final value before everything stops
                            //cancelAnimationFrame(rafId);
                        }
                    };
                    rafId = requestAnimationFrame(countAction); // Initiates the looping function
                });
            };
        } (jQuery));

        //-- Executing
        $('.number-counter').countTo();
    </script>
    <script type="text/javascript">
        function SetTarget() {
            alert("hi");
            document.forms[0].target = "_blank";
        }
    </script>
</head>
<body id="container-fluid">
    <form id="form1" runat="server">
    <div class="main_header">
        <div class="header_top_area">
            <div class="container">
                <div class="header_top">
                    <ul>
                        <input type="button" class="increase" value=" A+ ">
                        <input type="button" class="decrease" value=" A- " />
                        <input type="button" class="resetMe" value="" a="">
                        <button onclick="myFunction2()">
                            <font color="#fff">T</font></button>
                        <button onclick="myFunction1()">
                            <font color="#cccccc">T</font></button>
                        <button onclick="myFunction3()">
                            <font color="#4ee2b9">T</font></button>
                    </ul>
                </div>
            </div>
        </div>
        <div class="header">
            <div class="container-fluid">
                <div class="logo_contact_info_part">
                    <div class="col-md-2 text-left">
                        <div class="logo">
                            <a href="#">
                                <img src="Asets/images/tg.png" alt="logo" style="height: 100px; width: 100px; margin-top: -2px;" /></a>
                        </div>
                    </div>
                    <div class="col-md-8  text-center">
                        <div class="cont_info">
                            <h1>
                                QUALITY CONTROL LABS</h1>
                            <h3>
                                Government of Telangana</h3>
                        </div>
                    </div>
                    <div class="col-md-2 text-right">
                        <div class="">
                            <div class="book_now">
                                <a href="#">
                                    <img src="Asets/images/qclogo.png" alt="logo" style="height: 100px; width: 100px;
                                        margin-top: 13px; margin-right: -120px;" /></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="nav_search_area hidden-xs">
            <div class="container-fluid">
                <div class="menu_area">
                    <div class="col-md-9">
                        <div class="nav_part">
                            <nav class="navbar navbar-default">
									<div class="container-fluid">
										<div class="row">
										<div class="main_menu">
										<div class="navbar-header">
										  <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
											<span class="sr-only">Toggle navigation</span>
											<span class="icon-bar"></span>
											<span class="icon-bar"></span>
											<span class="icon-bar"></span>
										  </button>	
										</div>
										<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
										  <ul class="nav navbar-nav">
											<li class="active"><a href="Home.aspx">Home</a></li>
											
										  </ul>
										</div>
										</div>
										</div>
									</div>
								</nav>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="search">
                            <div class="form-group navbar-form navbar-left">
                                Select Department
                                <asp:DropDownList ID="ddldept" CssClass="form-control" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="slider_area">
        <div class="tp-banner">
            <ul>
                <li data-transition="random" data-slotamount="7" data-masterspeed="1500">
                    <img src="Asets/images/slide_1.png" alt="slidebg1" data-bgfit="cover" data-bgposition="center top"
                        data-bgrepeat="no-repeat">
                    <div class="tp-caption lightgrey_divider skewfromrightshort fadeout" data-x="-90"
                        data-y="280" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="font-family: roboto; font-size: 16px; color: #fff; background: #3f4b5d;
                        text-transform: Lowercase; display: inline-block; min-height: 27px; line-height: 27px;
                        width: auto; padding: 0 12px;">
                        Government of Telangana
                    </div>
                    <div class="tp-caption large_black sfr start" data-x="-90" data-y="295" data-speed="500"
                        data-start="1200" data-easing="Power4.easeOut" style="background: transparent;">
                        <img src="Asets/images/slider_caption_dantal.png" alt="">
                    </div>
                    <div class="tp-caption lightgrey_divider skewfromleftshort fadeout" data-x="-90"
                        data-y="430" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="background: transparent; font-size: 16px; font-family: roboto; font-weight: 300;
                        color: #2a2e35; line-height: 25px; width: 295px; word-wrap: break-word; display: inline-block;">
                        Quality Control Laboratories<br />
                        An online approach to track the samples sent for quality testing
                        <br>
                    </div>
                </li>
                <li data-transition="random" data-slotamount="7" data-masterspeed="1500">
                    <img src="Asets/images/slide4.png" alt="slidebg1" data-bgfit="cover" data-bgposition="center top"
                        data-bgrepeat="no-repeat">
                    <div class="tp-caption lightgrey_divider skewfromrightshort fadeout" data-x="-90"
                        data-y="280" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="font-family: roboto; font-size: 16px; color: #fff; background: #3f4b5d;
                        text-transform: Lowercase; display: inline-block; min-height: 27px; line-height: 27px;
                        width: auto; padding: 0 12px;">
                        Government of Telangana
                    </div>
                    <div class="tp-caption large_black sfr start" data-x="-90" data-y="295" data-speed="500"
                        data-start="1200" data-easing="Power4.easeOut" style="background: transparent;">
                        <img src="Asets/images/slider_caption_dantal.png" alt="">
                    </div>
                    <div class="tp-caption lightgrey_divider skewfromleftshort fadeout" data-x="-90"
                        data-y="430" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="background: transparent; font-size: 16px; font-family: roboto; font-weight: 300;
                        color: #2a2e35; line-height: 25px; width: 295px; word-wrap: break-word; display: inline-block;">
                        Quality Control Laboratories<br />
                        An online approach to track the samples sent for quality testing
                    </div>
                </li>
                <li data-transition="random" data-slotamount="7" data-masterspeed="1000">
                    <img src="Asets/images/slide-4.png" alt="darkblurbg" data-bgfit="cover" data-bgposition="center top"
                        data-bgrepeat="no-repeat">
                    <div class="tp-caption lightgrey_divider skewfromrightshort fadeout" data-x="-90"
                        data-y="280" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="font-family: roboto; font-size: 16px; color: #fff; background: #3f4b5d;
                        text-transform: uppercase; display: inline-block; min-height: 27px; line-height: 27px;
                        width: auto; padding: 0 12px;">
                        Government of Telangana
                    </div>
                    <div class="tp-caption large_black sfr start" data-x="-90" data-y="295" data-speed="500"
                        data-start="1200" data-easing="Power4.easeOut" style="background: transparent;">
                        <img src="Asets/images/slider_caption_dantal.png" alt="">
                    </div>
                    <div class="tp-caption lightgrey_divider skewfromleftshort fadeout" data-x="-90"
                        data-y="430" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="background: transparent; font-size: 16px; font-family: roboto; font-weight: 300;
                        color: #2a2e35; line-height: 25px; width: 295px; word-wrap: break-word; display: inline-block;">
                        Quality Control Laboratories<br />
                        An online approach to track the samples sent for quality testing
                    </div>
                </li>
                <li data-transition="random" data-slotamount="7" data-masterspeed="1000">
                    <img src="Asets/images/slide-5.jpeg" alt="darkblurbg" data-bgfit="cover" data-bgposition="center top"
                        data-bgrepeat="no-repeat">
                    <div class="tp-caption lightgrey_divider skewfromrightshort fadeout" data-x="-90"
                        data-y="280" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="font-family: roboto; font-size: 16px; color: #fff; background: #3f4b5d;
                        text-transform: uppercase; display: inline-block; min-height: 27px; line-height: 27px;
                        width: auto; padding: 0 12px;">
                        Government of Telangana
                    </div>
                    <div class="tp-caption large_black sfr start" data-x="-90" data-y="295" data-speed="500"
                        data-start="1200" data-easing="Power4.easeOut" style="background: transparent;">
                        <img src="Asets/images/slider_caption_dantal.png" alt="">
                    </div>
                    <div class="tp-caption lightgrey_divider skewfromleftshort fadeout" data-x="-90"
                        data-y="430" data-speed="500" data-start="1200" data-easing="Power4.easeOut"
                        style="background: transparent; font-size: 16px; font-family: roboto; font-weight: 300;
                        color: #2a2e35; line-height: 25px; width: 295px; word-wrap: break-word; display: inline-block;">
                        Quality Control Laboratories<br />
                        An online approach to track the samples sent for quality testing
                    </div>
                </li>
            </ul>
        </div>
    </div>
      <section class="welcome_area">
    <div class="container">
        <div class="welcome">
            <div class="col-md-5 col-sm-5">
                <div class="doctor_img">
                    <img src="Asets/images/doctors.png" alt="Doctors" />
                </div>
            </div>
            <div class="col-md-7 col-sm-7">
                <div class="doctor_text">
                    <h1>
                        Quality
                    </h1>
                    <h2>
                        Control <strong>Laboratories</strong></h2>
                    <p align="justify">
                        It is an initiation by Government of Telanagana. Quality control involves testing
                        of samples and determining if they are as per the government standards or not. This
                        purpose is automized for Accuracy and better reports.
                    </p>
                </div>
            </div>
        </div>
    </div>
    </section>
    <div class="our_doctors_area">
        <div class="container-fluid">
            <div class="our_doc_list">
                <div class="our_doc">
                     <div class="serv_text_part">
                    <h2>
                        Live Dashboard</h2>
                    <p>
                        <img src="Asets/images/detail_heartl.png" alt="Services" />
                    </p>
                </div>
                    <div class="counter_part" style="width: 100%;">
                        <div class="container-fluid">
                            <div class="hospital_info">
                                <div class="col-md-6">
                                    <div class="col-md-3">
                                        <div class="hospital_room">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/sample1.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblRegCnt" runat="server"></asp:Label>
                                                    </h2>
                                                </span>
                                                <h3>
                                                    Samples registered</h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 ">
                                        <div class="hospital_room">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/Accept.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblAcc" runat="server"></asp:Label>
                                                    </h2>
                                                </span>
                                                <h3>
                                                    Accepted</h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="hospital_room">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/Reject.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblRej" runat="server"></asp:Label>
                                                    </h2>
                                                </span>
                                                <h3>
                                                    Rejected</h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="doc_award">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/unitAlloc.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblUnitCnt" runat="server"></asp:Label></h2>
                                                </span>
                                                <h3>
                                                    Unit Allotted</h3>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-3">
                                        <div class="doc_award">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/test.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblLabCnt" runat="server"></asp:Label></h2>
                                                </span>
                                                <h3>
                                                    Lab Allotted</h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 ">
                                        <div class="doc_award">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/doctor6.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblTestCnt" runat="server"></asp:Label></h2>
                                                </span>
                                                <h3>
                                                    Samples Tested</h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="doc_award">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/asperNorms.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblYesCnt" runat="server"></asp:Label></h2>
                                                </span>
                                                <h3>
                                                    As per the Norms
                                                </h3>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="doc_award">
                                            <div class="hospital_room_img">
                                                <img src="Asets/images/notasper.png" />
                                            </div>
                                            <div class="htext">
                                                <span class="number-counter" data-count-from="0" data-count-to="100" data-count-speed="30">
                                                    <h2 class="count">
                                                        <asp:Label ID="lblNoCnt" runat="server"></asp:Label></h2>
                                                </span>
                                                <h3>
                                                    Not As Per the Norms</h3>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer_bottom">
        <div class="container-fluid">
            <div class="footer_copyright">
                <div class="col-md-6 col-sm-6">
                    <div class="copy_text">
                        <p style="color: Silver">
                            Site Designed,Developed And Hosted by National Informatics Centre (TS), hyderabad.</p>
                    </div>
                </div>
                <div class="col-md-6 col-sm-6">
                    <div class="copy_text">
                        <p style="color: Silver">
                            Content Maintained and Updated by Concerned departments , Government Of Telangana.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="Asets/js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="Asets/js/count.js" type="text/javascript"></script>
    <script src="Asets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="Asets/js/jquery.slicknav.min.js" type="text/javascript"></script>
    <script src="Asets/js/jquery.themepunch.tools.min.js" type="text/javascript"></script>
    <script src="Asets/js/jquery.themepunch.revolution.min.js" type="text/javascript"></script>
    <script src="Asets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="Asets/js/owl.carousel.min.js" type="text/javascript"></script>
    <script src="Asets/js/jquery.mixitup.js" type="text/javascript"></script>
    <script src="Asets/js/jquery.prettyPhoto.js" type="text/javascript"></script>
    <script src="Asets/js/smk-accordion.js" type="text/javascript"></script>
    <script src="Asets/js/easyResponsiveTabs.js" type="text/javascript"></script>
    <script src="Asets/js/main.js" type="text/javascript"></script>
    <script src="Asets/js/gigw.js" type="text/javascript"></script>
    </form>
</body>
</html>
