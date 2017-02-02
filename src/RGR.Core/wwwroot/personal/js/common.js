$(function(){
	$('.filtr-result a').click(function() {
		$(this).toggleClass('active');
	});
	$('.chb').click(function() {
		if ($(this).hasClass('active')) {
			$(this).removeClass('active');
			$('.catalog-header.main').removeClass('hidden');
			$('.catalog-header.edit').addClass('hidden');
		}
		else {
			$(this).addClass('active');
			$('.catalog-header.main').addClass('hidden');
			$('.catalog-header.edit').removeClass('hidden');
		}
	});
	$('.header .menu-button').click(function() {
		$('.top-menu').removeClass('hidden');
	});
	$('.index-page-slider').slick({
		autoplay: true,
		dots: true
	});
	$('ul.object-type li a').click(function() {
		$(this).parent().parent().find('a').removeClass('active');
		$(this).addClass('active');
		var href = $(this).attr('href');
		if ($(this).parent().parent().hasClass('first-object-type')) {
			$('ul.second.object-type').removeClass('active');
		}
		if ($(this).hasClass('secondlink')) {
			var secondhref = $(this).attr('data-link');
			$('ul.'+secondhref).addClass('active');
		}
		$('.catalog-list, .infobox').removeClass('active')
		$('.catalog-list'+href+', .infobox'+href).addClass('active');
		return false;
	});
});