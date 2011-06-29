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
using TheCore.Helpers;

namespace PhishMarket.MyPhishMarket
{
    public partial class ShowAnalysis : PhishMarketBasePage
    {
        public Guid ShowId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private IMyShow GetMyShow(string myShowIdStr)
        {
            ResetPanels();

            if (string.IsNullOrEmpty(hdnMyShowId.Value))
            {
                var scriptHelper3 = new ScriptHelper("ErrorAlert", "alertDiv", "There was an error saving your review.");
                Page.RegisterStartupScript(scriptHelper3.ScriptName, scriptHelper3.GetFatalScript());
                return null;
            }

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShowId = new Guid(myShowIdStr);

            return myShowService.GetMyShow(myShowId);
        }

        public void ajaxShowRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            var myShow = GetMyShow(hdnMyShowId.Value);

            if (myShow == null) return;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.Rating = int.Parse(e.Value);
                uow.Commit();
            }
        }

        public void ajaxBustoutRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            var myShow = GetMyShow(hdnMyShowId.Value);

            if (myShow == null) return;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.BustoutRating = int.Parse(e.Value);
                uow.Commit();
            }
        }

        public void ajaxType2JamRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            var myShow = GetMyShow(hdnMyShowId.Value);

            if (myShow == null) return;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.Type2JamRating = int.Parse(e.Value);
                uow.Commit();
            }
        }

        public void ajaxType1JamRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            var myShow = GetMyShow(hdnMyShowId.Value);

            if (myShow == null) return;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.Type1JamRating = int.Parse(e.Value);
                uow.Commit();
            }
        }

        public void ajaxSegueRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            var myShow = GetMyShow(hdnMyShowId.Value);

            if (myShow == null) return;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.SegueRating = int.Parse(e.Value);
                uow.Commit();
            }
        }

        public void ajaxFlowRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            var myShow = GetMyShow(hdnMyShowId.Value);

            if (myShow == null) return;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.FlowRating = int.Parse(e.Value);
                uow.Commit();
            }
        }

        public void ajaxEnergyRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            var myShow = GetMyShow(hdnMyShowId.Value);

            if (myShow == null) return;

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                myShow.EnergyRating = int.Parse(e.Value);
                uow.Commit();
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
            ShowId = showId;
            hdnShowId.Value = showId.ToString();
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
                ajaxBustoutRating.CurrentRating = myShow.BustoutRating == null ? 0 : int.Parse(myShow.BustoutRating.Value.ToString());
                ajaxEnergyRating.CurrentRating = myShow.EnergyRating == null ? 0 : int.Parse(myShow.EnergyRating.Value.ToString());
                ajaxFlowRating.CurrentRating = myShow.FlowRating == null ? 0 : int.Parse(myShow.FlowRating.Value.ToString());
                ajaxSegueRating.CurrentRating = myShow.SegueRating == null ? 0 : int.Parse(myShow.SegueRating.Value.ToString());
                ajaxType1JamRating.CurrentRating = myShow.Type1JamRating == null ? 0 : int.Parse(myShow.Type1JamRating.Value.ToString());
                ajaxType2JamRating.CurrentRating = myShow.Type2JamRating == null ? 0 : int.Parse(myShow.Type2JamRating.Value.ToString());

                txtFree.Text = myShow.Notes;
                phMyShow.Visible = true;
                phMyShowRating.Visible = true;
                phNotMyShow.Visible = false;
            }

            //var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
            var setService = new SetService(Ioc.GetInstance<ISetRepository>());
            //var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());
            var showService = new ShowService(Ioc.GetInstance<IShowRepository>());

            var show = showService.GetShow(showId);

            SetPageTitle("Analyze " + show.GetShowName());

            lblShow.Text = show.GetShowName();
            //var sets = setService.GetSetsForShow(showId).ToList();  //sets NEEDS to be a list here! DO NOT CHANGE DAN!

            //var ss = (from set in sets
            //          from song in setSongService.GetSetSongsBySet(set.SetId).OrderBy(z => z.Order.Value).DefaultIfEmpty()
            //          from analysis in analysisService.GetAnalysisBySetSongAndUser(song.SetSongId, userId).DefaultIfEmpty()
            //          select new { Set = set, Song = song, Analysis = analysis }).ToList();

            //rptSongs.DataSource = ss;
            //rptSongs.DataBind();
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

            ShowId = showId;

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
                var scriptHelper3 = new ScriptHelper("ErrorAlert", "alertDiv", "There was an error saving your review.");
                Page.RegisterStartupScript(scriptHelper3.ScriptName, scriptHelper3.GetFatalScript());
                return;
            }

            var myShowService = new MyShowService(Ioc.GetInstance<IMyShowRepository>());

            var myShowId = new Guid(hdnMyShowId.Value);

            var myShow = myShowService.GetMyShow(myShowId);

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                if (txtFree.Text.Length > 3000)
                {
                    var scriptHelper2 = new ScriptHelper("ErrorAlert", "alertDiv", "Your review was too long. Please keep it under 3000 characters.");
                    Page.RegisterStartupScript(scriptHelper2.ScriptName, scriptHelper2.GetFatalScript());
                    return;
                    }

                myShow.Notes = txtFree.Text;
                myShow.NotesUpdatedDate = DateTime.Now;

                uow.Commit();

                var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", "Congratulations you have successfully saved a review for this show.");
                Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
            }

            ShowId = new Guid(hdnShowId.Value);
        }

        //public void btnSubmitNotes_Click(object sender, EventArgs e)
        //{
        //    ResetPanels();

        //    var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());

        //    if (string.IsNullOrEmpty(hdnSetSongId.Value))
        //    {
        //        var scriptHelper = new ScriptHelper("ErrorAlert", "alertDiv", "Sorry there was an error. Please click another song and try again.");
        //        Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetFatalScript());
                
        //        return;
        //    }

        //    Guid setSongId = new Guid(hdnSetSongId.Value);
        //    var userId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

        //    var analysis = analysisService.GetAnalysisBySetSongAndUser(setSongId, userId).SingleOrDefault();

        //    bool success = false;

        //    if (analysis != null)
        //    {
        //        using (IUnitOfWork uow = UnitOfWork.Begin())
        //        {
        //            //analysis.Rating = ddlRating.SelectedValue == "0" ? null : (double?)double.Parse(ddlRating.SelectedValue);
        //            analysis.Notes = txtSetSongNotes.Text;
        //            analysis.UpdatedDate = DateTime.Now;
                    
        //            uow.Commit();

        //            var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", "Congratulations you have successfully updated your analysis.");
        //            Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
        //        }
        //    }
        //    else
        //    {
        //        Guid? myShowId = null;

        //        if (!string.IsNullOrEmpty(hdnMyShowId.Value))
        //        {
        //            myShowId = new Guid(hdnMyShowId.Value);
        //        }

        //        Analysis newAnalysis = new Analysis
        //        {
        //            AnalysisId = Guid.NewGuid(),
        //            CreatedDate = DateTime.Now,
        //            MyShowId = myShowId,
        //            Notes = txtSetSongNotes.Text,
        //            SetSongId = setSongId,
        //            UserId = userId,
        //            UpdatedDate = DateTime.Now
        //        };

        //        analysisService.SaveCommit(newAnalysis, out success);

        //        if (success)
        //        {
        //            var scriptHelper = new ScriptHelper("SuccessAlert", "alertDiv", "Congratulations you have successfully added a new analysis.");
        //            Page.RegisterStartupScript(scriptHelper.ScriptName, scriptHelper.GetSuccessScript());
        //        }
        //    }

        //    Bind();
        //}

        public string GetShortenedNote(string note)
        {
            if(string.IsNullOrEmpty(note))
                return string.Empty;

            if(note.Length < 30)
                return note;

            return note.Substring(0, 30);
        }

        //public void rptSongs_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    ResetPanels();
            
        //    var id = new Guid(e.CommandArgument.ToString());

        //    switch (e.CommandName.ToLower())
        //    {
        //        case "editnotes":
        //            FillNotes(id);
        //            break;
        //    }
        //}

        //private void FillNotes(Guid setSongId)
        //{
        //    var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
        //    var analysisService = new AnalysisService(Ioc.GetInstance<IAnalysisRepository>());

        //    var setSong = setSongService.GetSetSong(setSongId);
        //    var analysis = analysisService.GetAnalysisBySetSong(setSongId).SingleOrDefault();

        //    hdnSetSongId.Value = setSongId.ToString();

        //    lblSetSongName.Text = string.Empty;

        //    if (setSong != null)
        //    {
        //        lblSetSongName.Text = setSong.SongName;
        //    }

        //    txtSetSongNotes.Text = string.Empty;

        //    if (analysis != null)
        //    {
        //        txtSetSongNotes.Text = !string.IsNullOrEmpty(analysis.Notes) ? analysis.Notes : string.Empty;
        //    }

        //    ShowId = new Guid(hdnShowId.Value);
        //}

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
        }
    }
}
