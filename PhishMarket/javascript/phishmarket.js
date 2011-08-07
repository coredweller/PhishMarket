
//This extends jquery to help get the GET variables for use in javascript/jquery
$.extend({
  getUrlVars: function(){
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for(var i = 0; i < hashes.length; i++)
    {
      hash = hashes[i].split('=');
      vars.push(hash[0]);
      vars[hash[0]] = hash[1];
    }
    return vars;
  },
  getUrlVar: function(name){
    return $.getUrlVars()[name];
  }
});

		
//MyPictures.aspx
//Calls a handler to get images back for galleria
function callMyPictureHandler(showId, userId) {
            callHandler("/Handlers/MyPicturesHandler.ashx", showId, userId);
        }
		
//MyPosters.aspx
//Calls a handler to get images back for galleria
function callMyPostersHandler(showId, userId) {
			callHandler("/Handlers/MyPostersHandler.ashx", showId, userId);
        }
		
function callHandler(handler, showId, userId) {

			$.getJSON(handler,
                { s: showId, u: userId },

                function(data) {
                    $('#gallery').galleria({
                        data_source: data.records,
						transition: 'fade',
						maxScaleRatio: 1,
                        width: 600,
                        height: 675,
						thumbnails: "numbers",
                    });

                });

		}
		
//ShowReviewws.aspx
//Calls a handler to get images back for galleria
function callShowReviewsHandler(showId, showDate) {

            $.getJSON("/Handlers/ShowReviewsHandler.ashx",
                { s: showId, d: showDate },

                function(data) {
                    $('#gallery').galleria({
                        data_source: data.records,
						transition: 'fade',
						maxScaleRatio: 1,
                        width: 600,
                        height: 675,
						thumbnails: "numbers",
                    });

                });
        }
		
//ViewProfile.aspx
//Calls a handler to get images back for galleria
function callViewProfileHandler(userId) {

            $.getJSON("/Handlers/ViewProfileHandler.ashx",
                { u: userId },

                function(data) {
                    $('#gallery').galleria({
                        data_source: data.records,
						transition: 'fade',
						maxScaleRatio: 1,
                        width: 600,
                        height: 675,
						thumbnails: "numbers",
                    });

                });
        }