$(function() {
	"use strict";
	var _w, _h, _r, _t = 0, _hh = 77, _ha, _l;
	var _o = ("IntersectionObserver" in window);

	// Page Calculations
	function OnResize(){
		_w = $(window).width();
		_h = $(window).height();

		if($('.mob-icon').is(':visible')) _r = true;
		else _r = false;

		_hh = $("header").height();

        $('.block.type-2 .col-md-6.col-md-push-6').height($('.block.type-2 .col-md-4.col-md-pull-6').height());
        $('.page-404 .table-view').css({'height':_h});
	}
	OnResize();

	$(document).ready(function() {

		// Fix non observer model items
		if ( !_o )
		{
			$('.lazy').each(function() {
				$(this).css('background-image', 'url(' + $(this).attr("data-src") + ')');
			});
		}

		// Turn on tips
		tippy('.tip', {
			delay: 100,
			arrow: true,
			inertia: true,
			arrowType: 'sharp',
			size: 'large',
			duration: 500,
			animation: 'shift-away',
			interactive: true,
			theme: 'dark'			
		});
	});

	$(window).on('load', function(){

		_ha = (location.href.split("#")[1] || "").toLowerCase();
		if(_ha.length && $('.scroll-to-block[data-id="'+_ha+'"]').length) {
			if (_r) 
			{
				window.scroll({
					top: ($('.scroll-to-block[data-id="'+_ha+'"]').offset().top - _hh),
					behavior: "instant"
				});
			}
			else
			{
				window.scroll({
					top: ($('.scroll-to-block[data-id="'+_ha+'"]').offset().top - _hh + $('.scroll-to-block[data-id="'+_ha+'"]').data("scroll-offset")),
					behavior: "instant"
				});
			}

			// Check for auto-tab selection	
			var shouldTab = $('#tab-' + _ha + ".tabs-switch");;
			if (shouldTab.length > 0)
			{
				// Hanlde Tab
				if($(shouldTab).hasClass('active') || _t) return false;
				_t = 1;
				$(shouldTab).parent().find('.active').removeClass('active');
				$(shouldTab).addClass('active');

				// Handle Tab Content
				var tabContent = $('#tab-' + _ha + '-tab');
				var tabsContainer = $(shouldTab).closest('.tabs-switch-container');
				tabsContainer.find('.tabs-entry:visible').fadeOut('fast', function(){
					tabContent.fadeIn('fast', function(){_t = 0;});
				});
			}

			// Auto Filter
			var shouldFilter = $('#filter-' + _ha);
			if (shouldFilter.length > 0)
			{
				if($(shouldFilter).hasClass('active')) return false;
				$(shouldFilter).parent().find('.active').removeClass('active');
				$(shouldFilter).addClass('active');

				filterBy("filter-" + _ha);
			}
		}
		
		$('body').addClass('loaded');
		_l = true;

		// Update Scroll Values
		scrollCall();
	});

	$(window).resize(function(){
		OnResize();
	});
	window.addEventListener("orientationchange", function() {
		OnResize();
	}, false);


	/*==============================*/
	/* 06 - function on page scroll */
	/*==============================*/
	var _buffer = null;
	$(window).scroll(function(){
        scrollCall();
	});


	// Evaluate for button highlighting
	function scrollCall(){
		if (!_l) return;

		var winScroll = $(window).scrollTop();
		
		if(!_r && !$('header').hasClass('default-act')) 
		{
			// Min scroll to switch over
			if($(window).scrollTop()>=25)
			{
				$('header').addClass('act');
			} 
			else 
			{
				$('header').removeClass('act');
			}
		}

 		if($('.scroll-to-block').length){
         	$('.scroll-to-block').each(function( index, element ) {
				if($(element).offset().top<=(winScroll+_hh) && ($(element).offset().top+$(element).height()) > (winScroll+_hh) )
				{
					$('.scroll-to-link.act').removeClass('act');
					$('.scroll-to-link[data-id="'+$(element).attr('data-id')+'"]').addClass('act');

					if(_ha!='#'+$(element).attr('data-id'))
					{ 
						_ha = '#'+$(element).attr('data-id');
						window.location.hash = _ha;
					}
					return false;
				}
			});
		}

	}

	//scrolling to some block
	$('.scroll-to-link').on('click', function(){
		// Remove base URL
		var urlSplit = ($(this).attr('href').split("#") || "");
		if (urlSplit.length && $('.scroll-to-block[data-id="'+urlSplit[1]+'"]').length)
		{
			
			if (_r)
			{
				$('body, html').animate({'scrollTop':$('.scroll-to-block[data-id="'+urlSplit[1]+'"]').offset().top - _hh }, 500);
			}
			else
			{
				$('body, html').animate({'scrollTop':$('.scroll-to-block[data-id="'+urlSplit[1]+'"]').offset().top - _hh + $('.scroll-to-block[data-id="'+urlSplit[1]+'"]').data("scroll-offset")}, 500);
			}
		}
		$('header').removeClass('act-mob');
		$('.mob-icon').removeClass('act');
		return false;
	});

	$('.responsive-filtration-toggle a').on('click', function() {
		
		if($(this).hasClass('active')) return false;
		$(this).parent().find('.active').removeClass('active');
		$(this).addClass('active');

		_ha = $(this).data('filter').replace("filter-", "");
		window.location.hash = _ha;

		filterBy($(this).data('filter'));
	});

	function filterBy(filter)
	{
		if ( filter == "*" ) {
			$('.sorting-container > div').each(function() { $(this).show(); });
		} else {
			$('.sorting-container > div').each(function() {
				if($(this).hasClass(filter))
				{
					$(this).show();
				}
				else
				{
					$(this).hide()
				}
			});
		}
	}

    
	/*==============================*/
	/* 08 - buttons, clicks, hovers */
	/*==============================*/

	//menu click in responsive
    $('.mob-icon').on('click', function(){
        if($(this).hasClass('act')){
            $('.mob-icon').removeClass('act');
            $('header').removeClass('act-mob');
        }else{
            $('.mob-icon').addClass('act');
            $('header').addClass('act-mob');
        }   
    });
    
    var obj;
	$('.play, .open-video-popup').on('click', function(){
        obj = $(this);
        $('.video-popup').addClass('act-act'); 
        $('.video-popup').addClass('act'); 
        setTimeout(function(){
            $('.video-popup iframe').attr('src',obj.attr('data-src'));
            setTimeout(function(){
                $('.video-popup iframe').addClass('act'); 
                $('.video-popup a').addClass('act');    
            },350);    
        },710);
        return false;
    });
    
    $('.video-popup a').on('click', function(){
        $('.video-popup iframe').removeClass('act'); 
        $('.video-popup a').removeClass('act');
        setTimeout(function(){
            $('.video-popup iframe').attr('src','');
            $('.video-popup').removeClass('act'); 
            setTimeout(function(){
                $('.video-popup').removeClass('act-act');    
            },500);
        },500);
        return false;
    });

	//tabs
	$('.tabs-switch').on('click', function(){
		if($(this).hasClass('active') || _t) return false;
		_t = 1;
		$(this).parent().find('.active').removeClass('active');
		$(this).addClass('active');
		var id = String($(this).attr('id')).replace("tab-","");
		
		if(_ha!='#'+$(this).attr('id'))
		{ 
			_ha = id;
			window.location.hash = _ha;
		}
		
		var tabIndex = $(this).parent().find('.tabs-switch').index(this);
		var tabsContainer = $(this).closest('.tabs-switch-container');
		tabsContainer.find('.tabs-entry:visible').fadeOut('fast', function(){
			tabsContainer.find('.tabs-entry').eq(tabIndex).fadeIn('fast', function(){_t = 0;});
		});
	});

	$('.categories-wrapper .entry.toggle').on('click', function(){
		$(this).toggleClass('active');
		$(this).next('.sub-wrapper').slideToggle('fast');
	});

	//open fullscreen preview popup
	$('.open-fullscreen').on('click', function(){
		$('.screen-preview-popup').addClass('active').find('.overflow img').attr('src', $(this).data('fullscreen'));
		$('body, html').toggleClass('overflow-hidden');
	});

	$('.screen-preview-popup .close-popup').on('click', function(){
		$('.screen-preview-popup').removeClass('active');	
		$('body, html').toggleClass('overflow-hidden');	
	});

	//checkbox
	$('.checkbox-entry.checkbox label').on('click', function(){
		$(this).parent().toggleClass('active');
		$(this).parent().find('input').click();
	});

	$('.checkbox-entry.radio label').on('click', function(){
		$(this).parent().find('input').click();
		if(!$(this).parent().hasClass('active')){
			var nameVar = $(this).parent().find('input').attr('name');
			$('.checkbox-entry.radio input[name="'+nameVar+'"]').parent().removeClass('active');
			$(this).parent().addClass('active');
		}
	});

	//responsive drop-down in gallery
	$('.responsive-filtration-title').on('click', function(){
		$(this).closest('.sorting-menu').toggleClass('active');
	});

	$('#site-search').on("submit", function(){
		if ( $('#site-search input#q').val().length > 0 ) {
			window.location = "https://www.google.ca/search?q=site:galileo.education+" + $('#site-search input#q').val();
		}
		return false;
	});

	$('#mailing-list').on("submit", function(){
		if ( $('#mailing-list input#q').val().length > 0 ) {
			window.location = "http://dotbunny.us7.list-manage.com/subscribe?u=862e4c930cb0704921c2b54e3&id=124cd90a99&EMAIL=" + $('#mailing-list input#q').val();
		}
		return false;
	});

	$(document).on('focus', '.error-class', function(){
		$(this).removeClass('error-class');
	});

	$('.form-popup-close-layer').on('click', function(){
		clearTimeout(formPopupTimeout);
		$('.form-popup').fadeOut(300);
	});

	$('.mouse-icon').on('click', function(){
		$('body, html').animate({'scroll-top':_h});
	});

	// Scroll to top button
	$('.back-to-top').on('click', function(){
		$('body, html').animate({'scroll-top':0});
	});

	$('.form-submit').on('click', function(){
		$(this).addClass("disabled");
		if ($(this).data('id').length > 0)
		{
			$('#' + $(this).data('id')).submit();
		}
		$(this).data('id', '');
	});

	// Lost Key
	$('#key-recovery').on("submit", function(){
		$.post( "/api/find-key.php", $( "#key-recovery" ).serialize() ).done(function( data ) {
			$('#key-recovery-response').html( data );
		}).fail(function() {
			$('#key-recovery-response').html( "Unavailable At This Time" );
		});;
		return false;
	});
});