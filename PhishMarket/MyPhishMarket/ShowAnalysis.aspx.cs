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
using TheCore.Interfaces;

namespace PhishMarket.MyPhishMarket
{
    public partial class ShowAnalysis : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void ajaxShowRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            ResetPanels();

            if (string.IsNullOrEmpty(hdnMyShowId.Value))
            {
                phMyShowFailure.Visible = true;
                return;
            }

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShowId = new Guid(hdnMyShowId.Value);

            var myShow = myShowService.GetMyShow(myShowId);

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.Rating = int.Parse(e.Value);

                uow.Commit();
                phMyShowSuccess.Visible = true;
            }
        }

        private void Bind()
        {
            Guid showId;
            IMyShow myShow;
            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            if (string.IsNullOrEmpty(Request.QueryString["showId"]))
            {
                Response.Redirect(LinkBuilder.DashboardLink());
            }

            showId = new Guid(Request.QueryString["showId"]);
            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            if (!string.IsNullOrEmpty(Request.QueryString["myShowId"]))
            {
                var myShowId = new Guid(Request.QueryString["myShowId"]);
                myShow = myShowService.GetMyShow(myShowId);
            }
            else
            {
                
                myShow = myShowService.GetMyShow(showId, userId);
            }

            if (myShow != null)
            {
                hdnMyShowId.Value = myShow.MyShowId.ToString();
                ajaxShowRating.CurrentRating = myShow.Rating == null ? 0 : int.Parse(myShow.Rating.Value.ToString());
                txtNotes.Text = myShow.Notes;
                phMyShow.Visible = true;
                phMyShowRating.Visible = true;
                phNotMyShow.Visible = false;
            }

            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
            var setService = new SetService(Ioc.GetInstance<ISetRepository>());
            var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var show = showService.GetShow(showId);

            SetPageTitle("Analyze " + show.GetShowName());

            lblShow.Text = show.GetShowName();
            var sets = setService.GetSetsForShow(showId).ToList();  //sets NEEDS to be a list here! DO NOT CHANGE DAN!

            var ss = (from set in sets
                      from song in setSongService.GetSetSongsBySet(set.SetId).OrderBy(z => z.Order.Value).DefaultIfEmpty()
                      from analysis in analysisService.GetAnalysisBySetSongAndUser(song.SetSongId, userId).DefaultIfEmpty()
                      select new { Set = set, Song = song, Analysis = analysis }).ToList();

            rptSongs.DataSource = ss;
            rptSongs.DataBind();
        }

        public void btnAddMyShow_Click(object sender, EventArgs e)
        {
            MyShowService myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            Guid userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var showId = new Guid(Request.QueryString["showId"]);

            var myShow = myShowService.GetMyShow(showId, userId);

            if (myShow != null)
            {
                phMyShow.Visible = true;
                phMyShowRating.Visible = true;
                phNotMyShow.Visible = false;
                return;
            }

            var myShowId = Guid.NewGuid();

            MyShow newMyShow = new MyShow
            {
                CreatedDate = DateTime.Now,
                MyShowId = myShowId,
                ShowId = showId,
                UserId = userId
            };

            bool success = false;

            myShowService.SaveCommit(newMyShow, out success);

            if (success)
            {
                phMyShow.Visible = true;
                phMyShowRating.Visible = true;
                phNotMyShow.Visible = false;
                hdnMyShowId.Value = myShowId.ToString();
            }
            else
            {
                phNotMyShowFailure.Visible = true;
            }
        }

        public void ajaxSongRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            ResetPanels();

            var setSongId = new Guid(e.Tag);
            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());
            var rating = (int?)int.Parse(e.Value);

            var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());

            var analysis = analysisService.GetAnalysisBySetSongAndUser(setSongId, userId).SingleOrDefault();

            if (analysis != null)
            {
                using (IUnitOfWork uow = UnitOfWork.Begin())
                {
                    analysis.Rating = rating;

                    uow.Commit();
                }

                phRatingSuccess.Visible = true;
                
            }
            else
            {
                Guid? myShowId = null;

                if (!string.IsNullOrEmpty(hdnMyShowId.Value))
                {
                    myShowId = new Guid(hdnMyShowId.Value);
                }

                Analysis newAnalysis = new Analysis
                {
                    AnalysisId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    MyShowId = myShowId,
                    Rating = rating,
                    SetSongId = setSongId,
                    UserId = userId
                };

                bool success = false;

                analysisService.SaveCommit(newAnalysis, out success);

                if (success)
                {
                    phRatingSuccess.Visible = true;
                }
                else
                {
                    phRatingError.Visible = true;
                }
            }
        }

        

        public string GetSetNumber(short setNumber, bool encore)
        {
            if (encore)
                return "Encore";

            return "Set " + setNumber.ToString();
        }

        public void btnSubmitShowNotes_Click(object sender, EventArgs e)
        {
            ResetPanels();

            if (string.IsNullOrEmpty(hdnMyShowId.Value))
            {
                phMyShowFailure.Visible = true;
                return;
            }

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShowId = new Guid(hdnMyShowId.Value);

            var myShow = myShowService.GetMyShow(myShowId);

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                if (txtNotes.Text.Length > 3000)
                {
                    phMyShowReviewTooLong.Visible = true;
                    return;
                }

                myShow.Notes = txtNotes.Text;
                myShow.NotesUpdatedDate = DateTime.Now;

                uow.Commit();
                phMyShowSuccess.Visible = true;
            }

        }

        public void btnSubmitNotes_Click(object sender, EventArgs e)
        {
            ResetPanels();

            var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());

            if (string.IsNullOrEmpty(hdnSetSongId.Value))
            {
                phGeneralError.Visible = true;
                return;
            }

            Guid setSongId = new Guid(hdnSetSongId.Value);
            var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            var analysis = analysisService.GetAnalysisBySetSongAndUser(setSongId, userId).SingleOrDefault();

            bool success = false;

            if (analysis != null)
            {
                using (IUnitOfWork uow = UnitOfWork.Begin())
                {
                    //analysis.Rating = ddlRating.SelectedValue == "0" ? null : (double?)double.Parse(ddlRating.SelectedValue);
                    analysis.Notes = txtSetSongNotes.Text;
                    analysis.UpdatedDate = DateTime.Now;
                    
                    uow.Commit();
                    phUpdatingSuccess.Visible = true;
                }
            }
            else
            {
                Guid? myShowId = null;

                if (!string.IsNullOrEmpty(hdnMyShowId.Value))
                {
                    myShowId = new Guid(hdnMyShowId.Value);
                }

                Analysis newAnalysis = new Analysis
                {
                    AnalysisId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    MyShowId = myShowId,
                    Notes = txtSetSongNotes.Text,
                    SetSongId = setSongId,
                    UserId = userId,
                    UpdatedDate = DateTime.Now
                };

                analysisService.SaveCommit(newAnalysis, out success);

                if (success)
                {
                    phNewSuccess.Visible = true;
                }
            }

            Bind();
        }

        public string GetShortenedNote(string note)
        {
            if(string.IsNullOrEmpty(note))
                return string.Empty;

            if(note.Length < 30)
                return note;

            return note.Substring(0, 30);
        }

        public void rptSongs_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ResetPanels();
            
            var id = new Guid(e.CommandArgument.ToString());

            switch (e.CommandName.ToLower())
            {
                case "editnotes":
                    FillNotes(id);
                    break;
            }
        }

        private void FillNotes(Guid setSongId)
        {
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
            var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());

            var setSong = setSongService.GetSetSong(setSongId);
            var analysis = analysisService.GetAnalysisBySetSong(setSongId).SingleOrDefault();

            hdnSetSongId.Value = setSongId.ToString();

            lblSetSongName.Text = string.Empty;

            if (setSong != null)
            {
                lblSetSongName.Text = setSong.SongName;
            }

            txtSetSongNotes.Text = string.Empty;

            if (analysis != null)
            {
                txtSetSongNotes.Text = !string.IsNullOrEmpty(analysis.Notes) ? analysis.Notes : string.Empty;
            }
        }

        public string OutputRating(double? rating)
        {
            if(!rating.HasValue)
                return "No Rating";

            return rating.Value.ToString();
        }

        public void ResetPanels()
        {
            phNewSuccess.Visible = false;
            phUpdatingSuccess.Visible = false;
            phGeneralError.Visible = false;
            phMyShowFailure.Visible = false;
            phMyShowSuccess.Visible = false;
            phRatingError.Visible = false;
            phRatingSuccess.Visible = false;
            phMyShowReviewTooLong.Visible = false;
        }
    }
}
