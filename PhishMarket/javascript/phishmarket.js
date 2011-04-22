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

            $.getJSON("/../../Handlers/MyPicturesHandler.ashx",
                { s: showId, u: userId },

                function(data) {
                    //alert(data.records[0]['image']);
                    $('#gallery').galleria({
                        data_source: data.records,
						transition: 'fade',
                        width: 600,
                        height: 600
                    });

                });
        }