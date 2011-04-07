using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;
using System.Web.Security;
using System.Net;
using System.IO;
using System.Collections;
using TheCore.Helpers;
using TheCore.Interfaces;

namespace PhishMarket.MyPhishMarket
{
    public partial class AddMyShow : PhishMarketBasePage
    {
        public string TourName;
        MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
        TourService tourService = new TourService(Ioc.GetInstance<ITourRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("Add Shows to your collection");

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var tours = tourService.GetAllToursDescending();

            ddlTours.Items.AddRange((from tour in tours select new ListItem(tour.TourName, tour.TourId.ToString())).ToArray());
        }

        public void btnGetShows_Click(object sender, EventArgs e)
        {
            BindFromPhishNet();
        }

        private void BindFromPhishNet()
        {
            const string apiCall = "http://api.phish.net/api.js?api=2.0&method=pnet.user.myshows.get&format=json&apikey=6D31B9439E9F9B550B42&username={0}";

            if (string.IsNullOrEmpty(txtUserName.Text))
                return;

            hdnBindFrom.Value = "phishnet";

            var url = string.Format(apiCall, txtUserName.Text);

            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();

            var ver = response.ProtocolVersion.ToString();

            var reader = new StreamReader(response.GetResponseStream());

            var resp = reader.ReadToEnd();

            var showDates = JSONParser.AddMyShowJSON(resp);

            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var shows = showService.GetShows(showDates);

            var finalShows = myShowService.GetShowsNotInUsersMyShows(userId, shows);

            var labelName = txtUserName.Text + "'s Shows From Phish.Net";

            BindShows(labelName, false, finalShows);
        }


        //private void ResetPanels()
        //{
        //    phError.Visible = false;
        //    phSuccess.Visible = false;
        //    phAlreadyAdded.Visible = false;
        //}

        public void rptShows_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //ResetPanels();

            if (e.CommandName.ToLower() == "add")
            {
                Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
                var showId = new Guid(e.CommandArgument.ToString());

                var myShow = myShowService.GetMyShow(showId, userId);

                if (myShow != null)
                {
                    //phAlreadyAdded.Visible = true;
                    return;
                }

                MyShow newMyShow = new MyShow
                {
                    CreatedDate = DateTime.Now,
                    MyShowId = Guid.NewGuid(),
                    ShowId = showId,
                    UserId = userId
                };

                bool success = false;

                myShowService.SaveCommit(newMyShow, out success);

                if (success)
                {
                    //phSuccess.Visible = true;
                    //phError.Visible = false;
                }
                else
                {
                    //phSuccess.Visible = false;
                    //phError.Visible = true;
                }

                if (hdnBindFrom.Value == "phishnet")
                    BindFromPhishNet();
                else
                    BindFromTour();
            }
        }

        public void btnSelectTour_Click(object sender, EventArgs e)
        {
            BindFromTour();
        }

        private void BindFromTour()
        {
            var g = new Guid(ddlTours.SelectedItem.Value);
            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            hdnBindFrom.Value = "tour";

            var tour = tourService.GetTour(g);

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var notMyShows = myShowService.GetShowsNotInUsersMyShows(userId, tour.TourId);

            BindShows(tour.TourName, true, notMyShows);
        }

        private void BindShows(string labelName, bool tour, IList<IShow> shows)
        {
            if (!string.IsNullOrEmpty(labelName))
            {
                if (tour)
                    labelName = "Tour: " + labelName;

                TourName = labelName;
            }

            rptShows.DataSource = shows;
            rptShows.DataBind();
        }
    }
}
