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
using System.Collections;
using TheCore.Interfaces;

namespace PhishMarket.MyPhishMarket
{
    public partial class FindForShow : PhishMarketBasePage
    {
        public string ShowIdStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["showId"]))
            {
                Response.Redirect(LinkBuilder.DashboardLink());
            }

            var showId = new Guid(Request.QueryString["showId"]);
            ShowIdStr = showId.ToString();
            hdnShowId.Value = showId.ToString();

            if (!IsPostBack)
            {
                Bind(showId);
            }
        }

        private void ResetPanels()
        {
            phError.Visible = false;
            phAddSuccess.Visible = false;
            phAlreadyAddedError.Visible = false;
        }

        public void rptTicketStubs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var id = new Guid(e.CommandArgument.ToString());

            switch (e.CommandName.ToLower())
            {
                case "addticketstub":
                    AddTicketStub(id);
                    break;
            }
        }

        private void AddTicketStub(Guid ticketStubId)
        {
            ResetPanels();

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var myShowTicketStubService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var showId = new Guid(hdnShowId.Value);
            var myShowTicketStubId = Guid.NewGuid();

            var myShow = myShowService.GetMyShow(showId, userId);

            var myShowTicketStub = myShowTicketStubService.GetMyShowTicketStubByMyShowAndTicketStubId(myShow.MyShowId, ticketStubId);

            bool success = false;

            if (myShowTicketStub != null)
            {
                phAlreadyAddedError.Visible = true;
                return;
            }

            var newMyShowTicketStub = new MyShowTicketStub
            {
                CreatedDate = DateTime.Now,
                MyShowId = myShow.MyShowId,
                MyShowTicketStubId = myShowTicketStubId,
                TicketStubId = ticketStubId,
            };

            myShowTicketStubService.SaveCommit(newMyShowTicketStub, out success);

            if (success)
            {
                phAddSuccess.Visible = true;
            }
            else
            {
                phError.Visible = true;
            }
        }

        public void rptArt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var id = new Guid(e.CommandArgument.ToString());

            switch (e.CommandName.ToLower())
            {
                case "addart":
                    AddArt(id);
                    break;
            }
        }

        private void AddArt(Guid artId)
        {
            ResetPanels();

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var showId = new Guid(hdnShowId.Value);
            var myShowArtId = Guid.NewGuid();

            var myShow = myShowService.GetMyShow(showId, userId);

            var myShowArt = myShowArtService.GetMyShowArtByMyShowAndArtId(myShow.MyShowId, artId);

            bool success = false;

            if (myShowArt != null)
            {
                phAlreadyAddedError.Visible = true;
                return;
            }

            var newMyShowArt = new MyShowArt
            {
                CreatedDate = DateTime.Now,
                MyShowId = myShow.MyShowId,
                MyShowArtId = myShowArtId,
                ArtId = artId,
            };

            myShowArtService.SaveCommit(newMyShowArt, out success);

            if (success)
            {
                phAddSuccess.Visible = true;
            }
            else
            {
                phError.Visible = true;
            }
        }

        public void rptPosters_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var id = new Guid(e.CommandArgument.ToString());

            switch (e.CommandName.ToLower())
            {
                case "addposter":
                    AddPoster(id);
                    break;
            }
        }

        private void AddPoster(Guid posterId)
        {
            ResetPanels();

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());

            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var showId = new Guid(hdnShowId.Value);
            var myShowPosterId = Guid.NewGuid();

            var myShow = myShowService.GetMyShow(showId, userId);

            var myShowPoster = myShowPosterService.GetMyShowPosterByMyShowAndPosterId(myShow.MyShowId, posterId);

            bool success = false;

            if (myShowPoster != null)
            {
                phAlreadyAddedError.Visible = true;
                return;
            }

            var newMyShowPoster = new MyShowPoster
                                                {
                                                    CreatedDate = DateTime.Now,
                                                    MyShowId = myShow.MyShowId,
                                                    MyShowPosterId = myShowPosterId,
                                                    PosterId = posterId,
                                                };

            myShowPosterService.SaveCommit(newMyShowPoster, out success);

            if (success)
            {
                phAddSuccess.Visible = true;
            }
            else
            {
                phError.Visible = true;
            }
        }

        private void Bind(Guid showId)
        {
            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShows = myShowService.GetMyShowsForShow(showId);

            BindPosters(myShows, userId, showId);

            BindTicketStubs(myShows, userId, showId);

            BindArt(myShows, userId, showId);
        }

        private void BindArt(IQueryable<IMyShow> myShows, Guid userId, Guid showId)
        {
            var myShowArtService = new MyShowArtService(Ioc.GetInstance<IMyShowArtRepository>());
            var artService = new ArtService(Ioc.GetInstance<IArtRepository>());

            var art = (from m in myShows
                       from p in myShowArtService.GetAllMyShowArt().Where(x => x.MyShowId == m.MyShowId).DefaultIfEmpty()
                       from a in artService.GetAllArt().Where(z => z.ArtId == p.ArtId).DefaultIfEmpty()
                       select new { MyShow = m, MyShowArt = p, Art = a }).ToList();

            var fullArt = art.Where(x => x.Art != null).ToList();

            IList<Guid> ids = new List<Guid>();

            foreach (var p in fullArt)
            {
                var matches = fullArt.Where(r => r.Art.ArtId == p.Art.ArtId).ToArray();

                if (matches.Count() > 1)
                {
                    //Start at one because you just want to delete the extras
                    for (int i = 1; i < matches.Count(); i++)
                    {
                        if (!ids.Contains(matches[i].MyShowArt.MyShowArtId))
                        {
                            ids.Add(matches[i].MyShowArt.MyShowArtId);
                        }
                    }
                }
            }

            foreach (var i in ids)
            {
                fullArt = fullArt.Where(x => x.MyShowArt.MyShowArtId != i).ToList();
            }

            if (fullArt == null || fullArt.Count() <= 0)
            {
                phNoArt.Visible = true;
                lnkNoArt.NavigateUrl = LinkBuilder.AddPhotoLink(showId, PhotoType.Art, LinkBuilder.FindForShowLink(showId));
                return;
            }

            rptArt.DataSource = fullArt;
            rptArt.DataBind();
        }

        private void BindTicketStubs(IQueryable<IMyShow> myShows, Guid userId, Guid showId)
        {
            var myShowTicketStubService = new MyShowTicketStubService(Ioc.GetInstance<IMyShowTicketStubRepository>());
            var ticketStubService = new TicketStubService(Ioc.GetInstance<ITicketStubRepository>());

            var ticketStubs = (from m in myShows
                               from p in myShowTicketStubService.GetAllMyShowTicketStubs().Where(x => x.MyShowId == m.MyShowId).DefaultIfEmpty()
                               from ts in ticketStubService.GetAllTicketStubs().Where(z => z.TicketStubId == p.TicketStubId).DefaultIfEmpty()
                               select new { MyShow = m, MyShowTicketStub = p, TicketStub = ts }).ToList();

            var fullTickets = ticketStubs.Where(x => x.TicketStub != null).ToList();

            IList<Guid> ids = new List<Guid>();

            foreach (var p in fullTickets)
            {
                var matches = fullTickets.Where(r => r.TicketStub.TicketStubId == p.TicketStub.TicketStubId).ToArray();

                if (matches.Count() > 1)
                {
                    //Start at one because you just want to delete the extras
                    for (int i = 1; i < matches.Count(); i++)
                    {
                        if (!ids.Contains(matches[i].MyShowTicketStub.MyShowTicketStubId))
                        {
                            ids.Add(matches[i].MyShowTicketStub.MyShowTicketStubId);
                        }
                    }
                }
            }

            foreach (var i in ids)
            {
                fullTickets = fullTickets.Where(x => x.MyShowTicketStub.MyShowTicketStubId != i).ToList();
            }

            if (fullTickets == null || fullTickets.Count() <= 0)
            {
                phNoTicketStubs.Visible = true;
                lnkNoTicketStubs.NavigateUrl = LinkBuilder.AddPhotoLink(showId, PhotoType.TicketStub, LinkBuilder.FindForShowLink(showId));
                return;
            }

            rptTicketStubs.DataSource = fullTickets;
            rptTicketStubs.DataBind();
        }

        private void BindPosters(IQueryable<IMyShow> myShows, Guid userId, Guid showId)
        {
            var myShowPosterService = new MyShowPosterService(Ioc.GetInstance<IMyShowPosterRepository>());
            var posterService = new PosterService(Ioc.GetInstance<IPosterRepository>());

            var posters = (from m in myShows
                           from p in myShowPosterService.GetAllMyShowPosters().Where(x => x.MyShowId == m.MyShowId).DefaultIfEmpty()
                           from poster in posterService.GetAllPosters().Where(z => z.PosterId == p.PosterId).DefaultIfEmpty()
                           select new { MyShow = m, MyShowPoster = p, Poster = poster }).ToList();

            var fullPosters = posters.Where(x => x.Poster != null && x.MyShow.UserId != userId);

            IList<Guid> ids = new List<Guid>();

            foreach (var p in fullPosters)
            {
                var matches = fullPosters.Where(r => r.Poster.PosterId == p.Poster.PosterId).ToArray();

                if (matches.Count() > 1)
                {
                    //Start at one because you just want to delete the extras
                    for (int i = 1; i < matches.Count(); i++)
                    {
                        if (!ids.Contains(matches[i].MyShowPoster.MyShowPosterId))
                        {
                            ids.Add(matches[i].MyShowPoster.MyShowPosterId);
                        }
                    }
                }
            }

            foreach (var i in ids)
            {
                fullPosters = fullPosters.Where(x => x.MyShowPoster.MyShowPosterId != i).ToList();
            }

            if (fullPosters == null || fullPosters.Count() <= 0)
            {
                phNoPosters.Visible = true;
                lnkNoPosters.NavigateUrl = LinkBuilder.AddPhotoLink(showId, PhotoType.Poster, LinkBuilder.FindForShowLink(showId));
                return;
            }

            rptPosters.DataSource = fullPosters;
            rptPosters.DataBind();
        }
    }
}
