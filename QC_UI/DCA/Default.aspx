<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DCA_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>QC | LABS</title>
    <link href="../Assets_Home/css/fonts.css" rel="stylesheet" type="text/css" />
    <link href="../Assets_Home/fonts/icomoon/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Assets_Home/css/bootstrap.min.css" />
    <link href="../Assets_Home/css/jquery.fancybox.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets_Home/css/owl.theme.default.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets_Home/fonts/flaticon/font/_flaticon.css" rel="stylesheet" type="text/css" />
    <link href="../Assets_Home/css/owl.carousel.min.css" rel="stylesheet" type="text/css" />
    <link href="../Assets_Home/css/aos.css" rel="stylesheet" type="text/css" />
    <link href="../Assets_Home/fonts/flaticon/font/flaticon.css" rel="stylesheet" type="text/css" />
    <link href="../Assets_Home/css/selection.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Rubik:300,400,700|Oswald:400,700"
        rel="stylesheet">
    <%--<link rel="stylesheet" href="Assets_Home/css/font-awesome.min.css" />
    
    <link rel="stylesheet" href="Assets_Home/css/theme_default.css" />
    <link rel="stylesheet" href="Assets_Home/css/owl.theme.css" />
   
    
    <link rel="stylesheet" href="Assets_Home/css/settings.css" media="screen" />
    
    <link rel="stylesheet" href="Assets_Home/css/easy-responsive-tabs.css" />--%>
    <link rel="stylesheet" href="../Assets_Home/css/style.css" />
    <%-- <link rel="stylesheet" href="Assets_Home/css/responsive.css" />--%>
    <link rel="shortcut icon" type="image/png" href="../Assets/img/favicon.png" />
   <style>
        /*@media only screen and (max-width: 800px) {
  #headerimg{
  width:330px;
  }
}*//* Smartphones (portrait and landscape) ----------- */@media only screen and (min-device-width : 320px) and (max-device-width : 480px)
        {
            #headerimg
            {
                width: 330px;
            }
        }
        /* Smartphones (landscape) ----------- */@media only screen and (min-width : 321px)
        {
            #headerimg
            {
                width: 300px;
            }
        }
        /* Smartphones (portrait) ----------- */@media only screen and (max-width : 320px)
        {
            #headerimg
            {
                width: 150px;
            }
        }
        /* iPads (portrait and landscape) ----------- */@media only screen and (min-device-width : 768px) and (max-device-width : 1024px)
        {
            #headerimg
            {
                width: 330px;
            }
        }
        /* iPads (landscape) ----------- */@media only screen and (min-device-width : 768px) and (max-device-width : 1024px) and (orientation : landscape)
        {
            #headerimg
            {
                width: 650px;
            }
        }
        /* iPads (portrait) ----------- */@media only screen and (min-device-width : 768px) and (max-device-width : 1024px) and (orientation : portrait)
        {
            #headerimg
            {
                width: 650px;
            }
        }
        /* Desktops and laptops ----------- */@media only screen and (min-width : 1224px)
        {
            #headerimg
            {
                width: 750px;
            }
        }
        /* Large screens ----------- */@media only screen and (min-width : 1824px)
        {
            #headerimg
            {
                width: 630px;
            }
        }
        /* iPhone 5 (portrait &amp; landscape)----------- */@media only screen and (min-device-width : 320px) and (max-device-width : 568px)
        {
            #headerimg
            {
                width: 450px;
            }
        }
        /* iPhone 5 (landscape)----------- */@media only screen and (min-device-width : 320px) and (max-device-width : 568px) and (orientation : landscape)
        {
            #headerimg
            {
                width: 330px;
            }
        }
        /* iPhone 5 (portrait)----------- */@media only screen and (min-device-width : 320px) and (max-device-width : 568px) and (orientation : portrait)
        {
            #headerimg
            {
                width: 330px;
            }
        }
    </style>
    <script src="../Assets/js/jquery.min.js" type="text/javascript"></script>
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
<body data-spy="scroll" data-target=".site-navbar-target" data-offset="300">
    <form id="form1" runat="server">
    <div id="overlayer">
    </div>
    <div class="loader">
        <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
    <div class="site-wrap" id="home-section">
        <div class="site-mobile-menu site-navbar-target">
            <div class="site-mobile-menu-header">
                <div class="site-mobile-menu-close mt-3">
                    <span class="icon-close2 js-menu-toggle"></span>
                </div>
            </div>
            <div class="site-mobile-menu-body">
            </div>
        </div>
        <header class="site-navbar js-sticky-header site-navbar-target" role="banner">
    <div class="container">
        <div class="row align-items-center position-relative">
          
            
            <div class="site-logo">
              <a href="#" class="navbar-brand" ><img  id="headerimg" src="../Assets_Home/images/drugs_header.png" alt="QCLabs, Drug Control Administrtion" title="Urbanic Template" /></a>
            </div>
            
            <div class="col-12">
              <nav class="site-navigation text-right ml-auto " role="navigation">

                <ul class="site-menu main-menu js-clone-nav ml-auto d-none d-lg-block">
                <li><a href="../Default.aspx" class="nav-link">Home</a></li>
                <li><a href="#services-section" class="nav-link">Dash Board</a></li>
              

              <%--  <li class="has-children">
                  <a href="#about-section" class="nav-link">About Us</a>
                  <ul class="dropdown arrow-top">
                    <li><a href="#team-section" class="nav-link">Team</a></li>
                    <li><a href="#pricing-section" class="nav-link">Pricing</a></li>
                    <li><a href="#faq-section" class="nav-link">FAQ</a></li>
                    <li class="has-children">
                      <a href="#">More Links</a>
                      <ul class="dropdown">
                        <li><a href="#">Menu One</a></li>
                        <li><a href="#">Menu Two</a></li>
                        <li><a href="#">Menu Three</a></li>
                      </ul>
                    </li>
                  </ul>
                </li>--%>

                <li><a href="Login.aspx" class="nav-link">Login</a></li>
                
                <li><a href="#testimonials-section" class="nav-link">Contact</a></li>
               <%-- <li><a href="#blog-section" class="nav-link">Blog</a></li>
                <li><a href="#contact-section" class="nav-link">Contact</a></li>--%>
              </ul>
              </nav>
          
            </div>

          <div class="toggle-button d-inline-block d-lg-none"><a href="#" class="site-menu-toggle py-5 js-menu-toggle text-black"><span class="icon-menu h3"></span></a></div>

        </div>
      </div>
      </header>
        <div class="row">
            <div class="col-md-8">
                <div class="owl-carousel slide-one-item">
                    <div class="site-section-cover overlay img-bg-section" style="background-image: url('../Assets/img/dcaslider/dca1.PNG');">
                        <div class="container">
                            <div class="row align-items-center justify-content-center text-center">
                                <div class="col-md-12 col-lg-7">
                                    <h1 data-aos="fade-up" data-aos-delay="">
                                        Quality Control Labs</h1>
                                    <h5 style="color: white">
                                        Drugs Control Administration</h5>
                                    <p class="mb-5" data-aos="fade-up" data-aos-delay="100">
                                        An initiation by Government of Telanagana.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="site-section-cover overlay img-bg-section" style="background-image: url('../Assets/img/dcaslider/dca2.png');">
                        <div class="container">
                            <div class="row align-items-center justify-content-center text-center">
                                <div class="col-md-12 col-lg-8">
                                    <h1 data-aos="fade-up" data-aos-delay="">
                                        Quality Control Labs</h1>
                                    <h5 style="color: white">
                                        Drugs Control Administration</h5>
                                    <p class="mb-5" data-aos="fade-up" data-aos-delay="100">
                                        An initiation by Government of Telanagana.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="site-section-cover overlay img-bg-section" style="background-image: url('../Assets/img/dcaslider/dca3.png');">
                        <div class="container">
                            <div class="row align-items-center justify-content-center text-center">
                                <div class="col-md-12 col-lg-8">
                                    <h1 data-aos="fade-up" data-aos-delay="">
                                        Quality Control Labs</h1>
                                    <h5 style="color: white">
                                        Drugs Control Administration</h5>
                                    <p class="mb-5" data-aos="fade-up" data-aos-delay="100">
                                        An initiation by Government of Telanagana.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="site-section-cover overlay img-bg-section" style="background-image: url('../Assets/img/dcaslider/slide4.png');">
                        <div class="container">
                            <div class="row align-items-center justify-content-center text-center">
                                <div class="col-md-12 col-lg-8">
                                    <h1 data-aos="fade-up" data-aos-delay="">
                                        Quality Control Labs</h1>
                                    <h5 style="color: white">
                                        Drugs Control Administration</h5>
                                    <p class="mb-5" data-aos="fade-up" data-aos-delay="100">
                                        An initiation by Government of Telanagana.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4" style="margin-top: 5px;">
                <div class="site-section bg-light">
                    <div class="col-12 text-center" data-aos="fade">
                        <h2 class="section-title text-primary">
                            <u>LIVE DASHBOARD</u></h2>
                    </div>
                    <br>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets_Home/images/sample1.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblRegCnt" runat="server"></asp:Label>
                                    </h2>
                                    </span>
                                    <h3 class="mb-3">
                                        SAMPLES REGISTER</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="100">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets/img/accepted.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblAcc" runat="server"></asp:Label>
                                    </h2>
                                    </span>
                                    <h3 class="mb-3">
                                        ACCEPTED</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets/img/rejected.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblRej" runat="server"></asp:Label>
                                    </h2>
                                    </span>
                                    <h3 class="mb-3">
                                        REJECTED</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets_Home/images/unitAlloc.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblUnitCnt" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        UNIT ALLOTTED</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="100">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets_Home/images/test.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblLabCnt" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        LAB ALLOTTED</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets_Home/images/doctor6.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblTestCnt" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        SAMPLES TESTED</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets_Home/images/Accept.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblYesCnt" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        AS PER THE NORMS</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets_Home/images/Reject.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblNoCnt" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        NOT AS PER THE NORMS</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                    <br />
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets/img/SamplesTested.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lblsamCat" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        Sample Categories</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets/img/testproto.png" style="height: 70px; width: 70px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lbltestproto" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        Testing Protocols</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets/img/testingprotocol.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lbltestPara" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        Testing Paramerters</h3>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-3 mb-3" data-aos="fade-up" data-aos-delay="200">
                                <div class="block__35630">
                                    <div class="icon mb-3">
                                        <img src="../Assets/img/disolutionTests.png" style="height: 68px; width: 68px;" />
                                    </div>
                                    <h2 class="count">
                                        <asp:Label ID="lbldisolutnTest" Text="72" runat="server"></asp:Label></h2>
                                    </span>
                                    <h3 class="mb-3">
                                        Dissolution Tests</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="site-section">
        <div class="block__73694 mb-2" id="services-section">
            <div class="container">
                <div class="row d-flex no-gutters align-items-stretch">
                    <div class="col-lg-8 ml-auto p-lg-5 mt-4 mt-lg-0">
                        <h2 class="section-title text-primary">
                            <strong>DRUG CONTROL ADMINISTRTION</strong></h2>
                        <p>
                            The Drugs Control Administration was extended to Telangana region after the formation
                            of Andhra Pradesh State in the year 1956 when four Drugs Inspectors were appointed
                            and implementation of Drugs & Cosmetics Act was extended to this area. Subsequently,
                            Drugs Control Administration was also entrusted with the enforcement of Prevention
                            of Food Adulteration Act and the Head of the Department was designated as Drugs
                            Controller and Food Health Authority. The organizational set up of various functionaries
                            in Drugs and Control Administration are broadly classified into two wings. 1. Enforcement
                            Wing 2. Laboratory Wing.</p>
                    </div>
                    <div class="col-12 col-lg-4 block__73422" style="background-image: url('../Assets/img/doctors.png');">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer class="">
      <div class="container-fluid" style="background: linear-gradient(to right, #e6910d 0%, #ffffff 51%, #7da791 100%);">
       
        <div class="text-center">
          <div class="col-md-12">
            <div class="border-top pt-4">
            <p class="copyright"><small>
            <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
            <font color="#000">Site Designed,Developed And Hosted by National Informatics Centre (TS), hyderabad. Content Maintained and Updated by Concerned departments , Government Of Telangana.</font></a>
            <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
            </small></p>
            </div>
          </div>
          
        </div>
      </div>
    </footer>
    </div>
    <script src="../Assets_Home/js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/popper.min.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/owl.carousel.min.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/jquery.sticky.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/jquery.animateNumber.min.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/jquery.fancybox.min.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/aos.js" type="text/javascript"></script>
    <script src="../Assets_Home/js/main.js" type="text/javascript"></script>
    </form>
</body>
</html>
