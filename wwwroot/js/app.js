$(function () {
	"use strict";
	/* perfect scrol bar */
	// new PerfectScrollbar("body");
	// search bar
	



	/* Back To Top */
    $(function () {
		$(window).on("scroll", function () {
			if ($(this).scrollTop() > 300) {
				$('.back-to-top').fadeIn();
			} else {
				$('.back-to-top').fadeOut();
			}
		});
		$('.back-to-top').on("click", function () {
			$("html, body").animate({
				scrollTop: 0
			}, 600);
			return false;
        });


	});

		
});