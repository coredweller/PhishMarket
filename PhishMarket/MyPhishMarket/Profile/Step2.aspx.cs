using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using TheCore.Repository;
using System.Text;
using System.Collections.Generic;
using System.Web.Services;
using TheCore.Helpers;

namespace PhishMarket.MyPhishMarket.ProfilePages
{
    public partial class Step2 : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageTitle("PhishMarket Profile Step 2");
            JSONifier j = new JSONifier("record", "ID", "Show");
            if (!IsPostBack)
            {
                BindLists();

                BindProfile();
            }
        }

        private void BindLists()
        {
            TourService tourService = new TourService(Ioc.GetInstance<ITourRepository>());
            //ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var tours = tourService.GetAllToursDescending().ToList();

            if (tours != null && tours.Count > 0)
            {
                tours.ForEach(x =>
                {
                    ddlFavoriteTour.Items.Add(new ListItem(x.TourName, x.TourId.ToString()));
                    ddlFavoriteLiveShowTour.Items.Add(new ListItem(x.TourName, x.TourId.ToString()));
                }
                    );

                var item = new ListItem("Please select a tour", "-1");

                ddlFavoriteTour.Items.Insert(0, item);
                ddlFavoriteLiveShowTour.Items.Insert(0, item);

                
                //ddlFavoriteLiveShow.Items.Insert(0, item);

                item.Selected = true;
            }
        }

        private void BindProfile()
        {
            var profile = GetProfile();

            if (profile != null)
            {
                ddlFavoriteTour.SelectedValue = profile.FavoriteTour != null ? profile.FavoriteTour.ToString() : string.Empty;

                if(profile.FavoriteLiveShow != null)
                {
                    //ddlFavoriteLiveShow.Items.Clear();

                    var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

                    var show = showService.GetShow(profile.FavoriteLiveShow.Value);

                    //var item = new ListItem(show.GetShowName(), show.ShowId.ToString());

                    lblCurrentSelection.Text = show.GetShowName();

                    //ddlFavoriteLiveShow.Items.Insert(0, item);

                    //item.Selected = true;
                }
            }
        }

        public void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(LinkBuilder.ProfileStep1Link());
        }

        public void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(LinkBuilder.ProfileStep3Link());
        }

        public void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            using (IUnitOfWork uow = TheCore.Infrastructure.UnitOfWork.Begin())
            {
                bool set = false;
                var profile = (Profile)GetProfile();

                if (ddlFavoriteTour.SelectedValue != "-1")
                {
                    set = true;
                    profile.FavoriteTour = new Guid(ddlFavoriteTour.SelectedValue);
                }

                var favoriteLiveShow = Request.Form["ddlFavoriteLiveShow"];

                if (favoriteLiveShow != null && favoriteLiveShow != "-1")
                {
                    set = true;
                    var favoriteLiveShowId = new Guid(favoriteLiveShow);
                    profile.FavoriteLiveShow = favoriteLiveShowId;
                    var showService = new ShowService(Ioc.GetInstance<IShowRepository>());
                    var show = showService.GetShow(favoriteLiveShowId);

                    lblCurrentSelection.Text = show.GetShowName();
                    
                }

                ddlFavoriteLiveShowTour.SelectedIndex = 0;

                if (set)
                {
                    uow.Commit();
                    var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", "You have successfully saved your profile. Proceed to Step 3 by clicking NEXT below!");
                    Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
                }
                else
                {
                    var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "Please select your Favorite Tour or Favorite Live Show");
                    Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetWarningScript());
                }
            }
        }
    }
}
