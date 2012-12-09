// This file requires jquery be loaded first!

function SetupColorSwatch() {

	// Fix for IE 7's z-index bug
	/*$(function () {
		var zIndexNumber = 10000;
		$('span.color_swatch').each(function () {
			$(this).css('zIndex', zIndexNumber);
			zIndexNumber -= 1;
		});
	});*/

	$(".color_swatch").each(function () {
		//$(".swatch_hover", this).css('backgroundColor', $(this).css('backgroundColor'));
	}).mouseenter(function () {
		$(".swatch_hover", this).css('display', 'block');
	}).mouseleave(function () {
		$(".swatch_hover", this).css('display', 'none');
	});
};

onload = function () {
	if (document.getElementsByClassName == undefined) {
		document.getElementsByClassName = function (className) {
			var hasClassName = new RegExp("(?:^|\\s)" + className + "(?:$|\\s)");
			var allElements = document.getElementsByTagName("*");
			var results = [];

			var element;
			for (var i = 0; (element = allElements[i]) != null; i++) {
				var elementClass = element.className;
				if (elementClass && elementClass.indexOf(className) != -1 && hasClassName.test(elementClass))
					results.push(element);
			}

			return results;
		}
	}
}

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
//This function will call on the End of Asynchoronus call

function EndRequestHandler() {
	SetupColorSwatch();
}

