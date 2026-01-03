(function ($) {
    "use strict";

    /*=============================================
    =    		 Preloader			      =
=============================================*/
    function preloader() {
        $("#preloader").delay(0).fadeOut();
    }

    $(window).on("load", function () {
        // preloader();
        wowAnimation();
        aosAnimation();
        tg_title_animation();
        accordion();
        mobileHeaderActive();
    });

    /*===========================================
    =    		Mobile Menu			      =
=============================================*/
    //SubMenu Dropdown Toggle
    if ($(".tgmenu__wrap li.menu-item-has-children ul").length) {
        $(".tgmenu__wrap .navigation li.menu-item-has-children").append('<div class="dropdown-btn"><span class="plus-line"></span></div>');
    }

    //Mobile Nav Hide Show
    if ($(".tgmobile__menu").length) {
        var mobileMenuContent = $(".tgmenu__wrap .tgmenu__main-menu").html();
        $(".tgmobile__menu .tgmobile__menu-box .tgmobile__menu-outer").append(mobileMenuContent);

        //Dropdown Button
        $(".tgmobile__menu li.menu-item-has-children .dropdown-btn").on("click", function () {
            $(this).toggleClass("open");
            $(this).prev("ul").slideToggle(300);
        });
        //Menu Toggle Btn
        $(".mobile-nav-toggler").on("click", function () {
            $("body").addClass("mobile-menu-visible");
        });

        //Menu Toggle Btn
        $(".tgmobile__menu-backdrop, .tgmobile__menu .close-btn").on("click", function () {
            $("body").removeClass("mobile-menu-visible");
        });
    }

    /*=============================================
    =           Data Background             =
=============================================*/
    $("[data-background]").each(function () {
        $(this).css("background-image", "url(" + $(this).attr("data-background") + ")");
    });

    /*===========================================
    =     Menu sticky & Scroll to top      =
=============================================*/
    $(window).on("scroll", function () {
        var scroll = $(window).scrollTop();
        if (scroll < 245) {
            $("#sticky-header").removeClass("sticky-menu");
            $(".scroll-to-target").removeClass("open");
            $("#header-fixed-height").removeClass("active-height");
        } else {
            $("#sticky-header").addClass("sticky-menu");
            $(".scroll-to-target").addClass("open");
            $("#header-fixed-height").addClass("active-height");
        }
    });

    /*=============================================
    =    		 Scroll Up  	         =
=============================================*/
    if ($(".scroll-to-target").length) {
        $(".scroll-to-target").on("click", function () {
            var target = $(this).attr("data-target");
            // animate
            $("html, body").animate(
                {
                    scrollTop: $(target).offset().top,
                },
                1000
            );
        });
    }

    /*=============================================
    =            Header Search            =
=============================================*/
    $(".search-open-btn").on("click", function () {
        $(".search__popup").addClass("search-opened");
        $(".search-popup-overlay").addClass("search-popup-overlay-open");
    });
    $(".search-close-btn").on("click", function () {
        $(".search__popup").removeClass("search-opened");
        $(".search-popup-overlay").removeClass("search-popup-overlay-open");
    });

    /*=============================================
=     Offcanvas Menu      =
=============================================*/
    $(".menu-tigger").on("click", function () {
        $(".offCanvas__info, .offCanvas__overly").addClass("active");
        return false;
    });
    $(".menu-close, .offCanvas__overly").on("click", function () {
        $(".offCanvas__info, .offCanvas__overly").removeClass("active");
    });

    /*=============================================
    =          brand active              =
=============================================*/
    var swiper2 = new Swiper(".slider__active", {
        spaceBetween: 0,
        effect: "fade",
        loop: true,
        autoplay: {
            delay: 6000,
        },
    });

    var swiper3 = new Swiper(".slider_baner__active", {
        spaceBetween: 0,
        // effect: "fade",
        loop: false,
        autoplay: {
            delay: 6000,
        },
        // Navigation arrows
        navigation: {
            nextEl: ".button-swiper-next",
            prevEl: ".button-swiper-prev",
        },
        pagination: {
            el: ".swiper-pagination-testimonials",
            clickable: true,
        },
    });

    var swiper4 = new Swiper(".slider_partners__active", {
        spaceBetween: 0,
        slidesPerView: "auto",

        // effect: "fade",
        loop: true,
        autoplay: {
            delay: 6000,
        },
        // Navigation arrows
        navigation: {
            nextEl: ".button-swiper-next",
            prevEl: ".button-swiper-prev",
        },
    });

    /*=============================================
    =          brand active              =
=============================================*/
    var slider = new Swiper(".brand-active", {
        slidesPerView: 1,
        spaceBetween: 30,
        loop: true,
        breakpoints: {
            1200: {
                slidesPerView: 6,
            },
            992: {
                slidesPerView: 5,
            },
            768: {
                slidesPerView: 4,
            },
            576: {
                slidesPerView: 3,
            },
            0: {
                slidesPerView: 2,
            },
        },
    });

    /*=============================================
    =          testimonials active              =
=============================================*/
    var slider = new Swiper(".testiminials-active", {
        slidesPerView: 3,
        spaceBetween: 24,
        loop: true,
        breakpoints: {
            1200: {
                slidesPerView: 3,
            },
            992: {
                slidesPerView: 3,
            },
            768: {
                slidesPerView: 2,
            },
            576: {
                slidesPerView: 1,
            },
            0: {
                slidesPerView: 1,
            },
        },
    });

    var slider_test = new Swiper(".testiminials-active-2", {
        slidesPerView: 2,
        spaceBetween: 24,
        loop: true,
        navigation: {
            nextEl: ".button-swiper-testimonial-next",
            prevEl: ".button-swiper-testimonial-prev",
        },
        breakpoints: {
            1200: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 2,
            },
            768: {
                slidesPerView: 1,
            },
            576: {
                slidesPerView: 1,
            },
            0: {
                slidesPerView: 1,
            },
        },
    });

    /*=============================================
    =          project active              =
=============================================*/
    var swiper2 = new Swiper(".project-active", {
        spaceBetween: 0,
        loop: true,
        autoplay: {
            delay: 6000,
        },
        thumbs: {
            swiper: swiper,
        },
        // Navigation arrows
        navigation: {
            nextEl: ".project-button-next",
            prevEl: ".project-button-prev",
        },
    });

    /*=============================================
    =          brand active              =
=============================================*/
    var slider = new Swiper(".project-active-two", {
        slidesPerView: 1,
        spaceBetween: 5,
        loop: true,
        breakpoints: {
            1200: {
                slidesPerView: 4,
            },
            992: {
                slidesPerView: 3,
            },
            768: {
                slidesPerView: 2,
            },
            576: {
                slidesPerView: 2,
            },
            0: {
                slidesPerView: 1,
            },
        },
    });

    /*=============================================
    =          testimonial active              =
=============================================*/
    var swiper = new Swiper(".testimonial-nav", {
        spaceBetween: 0,
        slidesPerView: 4,
    });
    var swiper2 = new Swiper(".testimonial-active", {
        spaceBetween: 0,
        loop: true,
        autoplay: {
            delay: 6000,
        },
        thumbs: {
            swiper: swiper,
        },
        // And if we need scrollbar
        scrollbar: {
            el: ".swiper-scrollbar",
            draggable: !0,
        },
    });

    /*=============================================
    =          testimonial active two             =
=============================================*/
    var swiper3 = new Swiper(".testimonial-active-two", {
        spaceBetween: 0,
        loop: true,
        slidesPerView: 1,
        spaceBetween: 0,
        autoplay: {
            delay: 6000,
        },
        // Navigation arrows
        navigation: {
            nextEl: ".testimonial-button-next",
            prevEl: ".testimonial-button-prev",
        },
    });

    /*=============================================
    =          testimonial active              =
=============================================*/
    var swiper = new Swiper(".testimonial__nav-three", {
        spaceBetween: 0,
        slidesPerView: 4,
    });
    var swiper2 = new Swiper(".testimonial-active-three", {
        spaceBetween: 0,
        loop: true,
        autoplay: {
            delay: 6000,
        },
        thumbs: {
            swiper: swiper,
        },
        // Navigation arrows
        navigation: {
            nextEl: ".testimonial-two-button-next",
            prevEl: ".testimonial-two-button-prev",
        },
    });

    /*=============================================
    =        Team Social Active 	       =
=============================================*/
    $(".social-toggle-icon").on("click", function () {
        $(this).parent().find("ul").slideToggle(400);
        $(this).find("i").toggleClass("fa-times");
        return false;
    });

    /*=============================================
    =    		pricing Active  	       =
=============================================*/
    $(".pricing__tab-switcher, .pricing__tab-btn").on("click", function () {
        $(".pricing__tab-switcher, .pricing__tab-btn").toggleClass("active"), $(".pricing__tab").toggleClass("seleceted"), $(".pricing__price").toggleClass("change-subs-duration");
    });

    /*=============================================
    =    		Odometer Active  	       =
=============================================*/
    $(".odometer").appear(function (e) {
        var odo = $(".odometer");
        odo.each(function () {
            var countNumber = $(this).attr("data-count");
            $(this).html(countNumber);
        });
    });

    /*=============================================
    =    		Magnific Popup		      =
=============================================*/
    $(".popup-image").magnificPopup({
        type: "image",
        gallery: {
            enabled: true,
        },
    });

    /* magnificPopup video view */
    $(".popup-video").magnificPopup({
        type: "iframe",
    });

    /*=============================================
    =    		 Wow Active  	         =
=============================================*/
    function wowAnimation() {
        var wow = new WOW({
            boxClass: "wow",
            animateClass: "animated",
            offset: 0,
            mobile: false,
            live: true,
        });
        wow.init();
    }

    /*=============================================
    =           Aos Active       =
=============================================*/
    function aosAnimation() {
        AOS.init({
            duration: 1000,
            mirror: true,
            once: true,
            disable: "mobile",
        });
    }

    $(".view-password").on("click", function () {
        var _parent = $(this).parent("div");
        var _input = _parent.find("input");
        if (_input.attr("type") == "password") {
            _input.attr("type", "text");
        } else {
            _input.attr("type", "password");
        }
    });

    $(window)
        .resize(function () {
            var _container = $("main .container");
            var _window_w = $(window).width();
            var _container_w = _container.width();
            var _space = (_window_w - _container_w) / 2 - 15;
            var _form_quote = $(".slider__area-home8 .box-form-quote");
            _form_quote.css("right", "" + _space + "px");
        })
        .resize();

    function accordion() {
        window.setTimeout(function () {
            $(".accordion").css("opacity", "1");
        }, 2000);
        $(".accordion-item").addClass("default");
        $(".accordion-item").on("click", function () {
            var e = $(".accordion > .accordion-item");
            if (e.hasClass("expand")) {
                e.removeClass("expand");
                $(this).addClass("expand");
            } else {
                $(this).addClass("expand");
            }
        });
    }
    function mobileHeaderActive() {
        var navbarTrigger = $(".burger-icon"),
            navCanvas = $(".burger-icon-2"),
            closeCanvas = $(".close-canvas"),
            endTrigger = $(".mobile-menu-close"),
            container = $(".mobile-header-active"),
            containerCanvas = $(".sidebar-canvas-wrapper"),
            wrapper4 = $("body");
        wrapper4.prepend('<div class="body-overlay-1"></div>');
        navbarTrigger.on("click", function (e) {
            navbarTrigger.toggleClass("burger-close");
            e.preventDefault();
            container.toggleClass("sidebar-visible");
            wrapper4.toggleClass("mobile-menu-active");
        });

        endTrigger.on("click", function () {
            container.removeClass("sidebar-visible");
            wrapper4.removeClass("mobile-menu-active");
        });

        var $offCanvasNav = $(".mobile-menu"),
            $offCanvasNavSubMenu = $offCanvasNav.find(".sub-menu");
        /*Add Toggle Button With Off Canvas Sub Menu*/
        $offCanvasNavSubMenu.parent().prepend('<span class="menu-expand"><i class="arrow-small-down"></i></span>');
        /*Close Off Canvas Sub Menu*/
        $offCanvasNavSubMenu.slideUp();
        /*Category Sub Menu Toggle*/
        $offCanvasNav.on("click", "li a, li .menu-expand", function (e) {
            var $this = $(this);
            if (
                $this
                    .parent()
                    .attr("class")
                    .match(/\b(menu-item-has-children|has-children|has-sub-menu)\b/) &&
                ($this.attr("href") === "#" || $this.hasClass("menu-expand"))
            ) {
                e.preventDefault();
                if ($this.siblings("ul:visible").length) {
                    $this.parent("li").removeClass("active");
                    $this.siblings("ul").slideUp();
                } else {
                    $this.parent("li").addClass("active");
                    $this.closest("li").siblings("li").removeClass("active").find("li").removeClass("active");
                    $this.closest("li").siblings("li").find("ul:visible").slideUp();
                    $this.siblings("ul").slideDown();
                }
            }
        });
    }

    $('#frmQuickUrl').on('submit', function (e) {
        e.preventDefault();
        var longUrl = $('#longUrl').val();
        var $submitBtn = $(this).find('button[type="submit"]');
        // Add spinner if not present
        if ($submitBtn.find('.spinner-border').length === 0) {
            $submitBtn.prepend('<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true" style="display:none"></span>');
        }
        var $spinner = $submitBtn.find('.spinner-border');
        // Disable button and show spinner
        $submitBtn.prop('disabled', true);
        $spinner.show();

        // Check if longUrl is null, empty, or whitespace
        if (!longUrl || $.trim(longUrl) === "") {
            Swal.fire({
                icon: 'warning',
                title: 'Missing URL',
                text: 'Please enter a valid long URL.',
                position: 'top-end',
                showConfirmButton: false,
                timer: 2000,
                toast: true
            });
            $submitBtn.prop('disabled', false);
            $spinner.hide();
            return;
        }

        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/Home/GenerateShortUrl',
            type: 'POST',
            data: { originalUrl: longUrl },
            headers: { 'RequestVerificationToken': token },
            success: function (response) {
                if (response.responseType === "Success") {
                    var html = '<input type="text" id="shortUrlDisplay" value="' + response.jsonContent + '" class="form-control" readonly style="width:auto;display:inline-block;">'
                        + '<button id="copyShortUrlBtn" class="btn btn-secondary" type="button">Copy</button>'
                        + '<div><img src="' + response.qrCodeImage + '" alt="QR Code" style="max-width:60px;" /></div>';
                    $('#frmQuickUrl').html(html);
                    $('#spanIntro').text("Right click on the QR image to save.")
                    Swal.fire({
                        icon: 'success',
                        title: 'Short URL Created',
                        text: response.responseMessage,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 5000,
                        toast: true
                    });
                } else {
                    // Show error message from response
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: response.responseMessage,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 5000,
                        toast: true
                    });
                    $submitBtn.prop('disabled', false);
                    $spinner.hide();
                }
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Unable to generate short URL. Please try again.',
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 5000,
                    toast: true
                });
                $submitBtn.prop('disabled', false);
                $spinner.hide();
            }
        });
    });

    // Delegate event for dynamically created button
    $(document).on('click', '#copyShortUrlBtn', function () {
        var shortUrl = $('#shortUrlDisplay').val();
        navigator.clipboard.writeText(shortUrl).then(function () {
            Swal.fire({
                icon: 'success',
                title: 'Copied!',
                text: 'Short URL copied to clipboard!',
                position: 'top-end',
                showConfirmButton: false,
                timer: 5000,
                toast: true
            });
        });
    });

})(jQuery);
