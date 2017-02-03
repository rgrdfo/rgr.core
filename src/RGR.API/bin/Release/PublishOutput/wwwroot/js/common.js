$(function(){
	$('.filtr-result a').click(function() {
		$(this).toggleClass('active');
	});
	$('.chb').click(function() {
		$(this).toggleClass('active');
	});
	$('.header .menu-button').click(function() {
		$('.top-menu').removeClass('hidden');
	});
	$('.index-page-slider').slick({
		autoplay: true,
		dots: true
	});
});