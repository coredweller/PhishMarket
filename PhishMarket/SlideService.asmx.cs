using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.IO;
using TheCore.Services;
using PhishPond.Repository.LinqToSql;
using TheCore.Configuration;
using TheCore.Repository;
using TheCore.Helpers;

namespace PhishMarket
{
    /// <summary>
    /// Summary description for SlideService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService()]
    public class SlideService : System.Web.Services.WebService
    {
        private const string connKey = "PhishPond";
        private const string showImagesFolder = "/images/Shows/";

        [WebMethod]
        public AjaxControlToolkit.Slide[] GetSlides(String contextKey)
        {
            if (String.IsNullOrEmpty(contextKey))
                return null;

            Guid userId = new Guid(contextKey.Split(';')[0]);
            Guid showId = new Guid(contextKey.Split(';')[1]);

            try
            {
                IPhotoRepository photoRepo = new PhotoRepository(new PhishDatabase(new ConnectionString(new AppConfigManager(), connKey)));

                PhotoService photoService = new PhotoService(photoRepo);

                var photos = photoService.GetPhotosByUserAndShow(userId, showId).Where(x => x.Thumbnail == false).ToList();

                if (photos == null || photos.Count <= 0)
                    return GetNoImagesFoundDirectory("There are no images for this show", string.Empty);

                // create generic empty list of slides
                List<AjaxControlToolkit.Slide> list = new List<AjaxControlToolkit.Slide>();
                String justFileName;
                String displayedFileTitleOnSlider;
                String displayedFileDescriptionOnSlider;

                foreach (var photo in photos)
                {
                    // get complete filename
                    justFileName = photo.FileName;

                    // get title
                    displayedFileTitleOnSlider = photo.NickName;

                    // set description
                    displayedFileDescriptionOnSlider = photo.Notes;

                    // add file to list of slides
                    list.Add(new AjaxControlToolkit.Slide(showImagesFolder + justFileName, displayedFileTitleOnSlider, displayedFileDescriptionOnSlider));
                }

                return (list.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private AjaxControlToolkit.Slide[] GetNoImagesFoundDirectory(string name, string description)
        {
            AjaxControlToolkit.Slide[] slides = new AjaxControlToolkit.Slide[1];

            // get image from web.config and verify it exists on file system
            var _noImagesFoundWebLocation = System.Configuration.ConfigurationSettings.AppSettings.Get("SlideServiceNoImagesFoundLocation");
            if (!File.Exists(Server.MapPath(_noImagesFoundWebLocation)))
                throw new Exception("SlideService.asmx::GetNoImagesFoundDirectory - NoImagesFoundLocation found in web.config does not exist after server.mappath - " + Server.MapPath(_noImagesFoundWebLocation));

            // create slide from image
            slides[0] = new AjaxControlToolkit.Slide(_noImagesFoundWebLocation, name, description);

            // return slide
            return (slides);
        }

        [WebMethod]
        public AjaxControlToolkit.Slide[] GetShowPictures(String contextKey)
        {
            if (String.IsNullOrEmpty(contextKey))
                return null;

            var showId = new Guid(contextKey);

            try
            {

                IPhotoRepository photoRepo = new PhotoRepository(new PhishDatabase(new ConnectionString(new AppConfigManager(), connKey)));

                PhotoService photoService = new PhotoService(photoRepo);

                var photos = photoService.GetPhotosByShow(showId).Where(x => x.Thumbnail == false).ToList();

                if (photos == null || photos.Count <= 0)
                {
                    var linkBuilder = new LinkBuilder();
                    var myPictureLink = linkBuilder.MyPicturesLink(showId);

                    var link = string.Format("<a href=\"{0}\">Upload your pictures here!</a>", myPictureLink);
                    
                    var name = string.Format("There are no pictures uploaded for this show. {0} You could be the first!", link);

                    return GetNoImagesFoundDirectory(name, string.Empty);
                }

                // create generic empty list of slides
                List<AjaxControlToolkit.Slide> list = new List<AjaxControlToolkit.Slide>();
                String justFileName;
                String displayedFileTitleOnSlider;
                String displayedFileDescriptionOnSlider;

                foreach (var photo in photos)
                {
                    // get complete filename
                    justFileName = photo.FileName;

                    // get title
                    displayedFileTitleOnSlider = photo.NickName;

                    // set description
                    displayedFileDescriptionOnSlider = photo.Notes;

                    // add file to list of slides
                    list.Add(new AjaxControlToolkit.Slide(showImagesFolder + justFileName, displayedFileTitleOnSlider, displayedFileDescriptionOnSlider));
                }

                return (list.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
