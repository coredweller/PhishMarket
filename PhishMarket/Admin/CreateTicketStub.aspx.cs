using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using System.Web.Security;
using PhishPond.Concrete;
using System.IO;
using TheCore.Interfaces;

namespace PhishMarket.Admin
{
    public partial class CreateTicketStub : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (EmptyNullUndefined(ddlShows.SelectedValue) || EmptyNullUndefined(txtFileName.Text))
            {
                //Show Some error here eventually, 
                //  but for now, no NOOOOOO No error for you!
                return;
            }

            var ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());
            var photoService = new PhotoService(Ioc.GetInstance<IPhotoRepository>());

            var showId = new Guid(ddlShows.SelectedValue);
            var photoId = Guid.NewGuid();
            var ticketStubId = Guid.NewGuid();
            var fileName = txtFileName.Text;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                IImageMediaFormats imageMediaFormats = Ioc.GetInstance<IImageMediaFormats>();

                var fileExt = Path.GetExtension(fileName.ToLower());
                var mediaFormat = imageMediaFormats.GetSpecByExtension(fileExt);

                var photo = new Photo
                {
                    PhotoId = photoId,
                    ShowId = showId,
                    FileName = fileName,
                    CreatedDate = DateTime.Now,
                    ContentType = mediaFormat.ContentType,
                    Thumbnail = false,
                    Type = (short)PhotoType.TicketStub
                };

                var ticketStub = new TicketStub
                {
                    ShowId = showId,
                    TicketStubId = ticketStubId,
                    Original = chkPTBM.Checked,
                    CreatedDate = DateTime.Now,
                    PhotoId = photoId
                };

                bool photoSuccess = false;
                bool ticketStubsuccess = false;

                photoService.Save(photo, mediaFormat, out photoSuccess);

                ticketStubService.Save(ticketStub, out ticketStubsuccess);

                if (photoSuccess && ticketStubsuccess)
                {
                    uow.Commit();
                    //Show Success
                    imgTheImage.ImageUrl = DefaultTicketStubImageLocation + fileName;
                }
                else
                {
                    //Show Error
                }
            }
        }

        private void Bind()
        {
            var tourService = new TourService(Ioc.GetInstance<ITourRepository>());

            var tours = tourService.GetAllToursDescending();

            ddlTours.Items.AddRange((from t in tours
                                     select new ListItem(t.TourName, t.TourId.ToString())).ToArray());

            var noneItem = new ListItem("None", "none");

            ddlTours.Items.Insert(0, noneItem);

            noneItem.Selected = true;
        }

        private void SetShows(Guid tourId)
        {

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var shows = showService.GetOfficialShows(tourId);

            ddlShows.Items.Clear();

            ddlShows.Items.AddRange((from s in shows
                                     select new ListItem(s.GetShowName(), s.ShowId.ToString())).ToArray());
        }

        public void ddlTours_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = ((DropDownList)sender).SelectedValue;

            switch (selectedValue)
            {
                case "":
                case "none":
                    return;
                default:
                    SetShows(new Guid(selectedValue));
                    break;
            }

        }

    }
}
