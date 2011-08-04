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

            $.getJSON("/Handlers/MyPicturesHandler.ashx",
                { s: showId, u: userId },

                function(data) {
                    $('#gallery').galleria({
                        data_source: data.records,
						transition: 'fade',
						maxScaleRatio: 1,
                        width: 600,
                        height: 600,
						thumbnails: "numbers"
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
                        height: 600
                    });

                });
        }
		
//MyPosters.aspx
//Calls a handler to get images back for galleria
function callMyPostersHandler(showId, userId) {

            $.getJSON("/Handlers/MyPostersHandler.ashx",
                { s: showId, u: userId },

                function(data) {
                    $('#gallery').galleria({
                        data_source: data.records,
						transition: 'fade',
						maxScaleRatio: 1,
                        width: 600,
                        height: 600
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
                        height: 600
                    });

                });
        }