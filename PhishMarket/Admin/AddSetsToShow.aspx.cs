using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using TheCore.Repository;

namespace PhishMarket.Admin
{
    public partial class AddSetsToShow : PhishMarketBasePage
    {
        public int FinalSetNumber { get; set; }
        ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
                Response.Redirect("/Admin/CreateShow.aspx");

            Guid showId = new Guid(Request.QueryString["id"]);

            hdnId.Value = showId.ToString();

            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void ResetPanels()
        {
            phError.Visible = false;
            phSuccess.Visible = false;
        }

        public void lnkAddSetToShow_Click(object sender, EventArgs e)
        {
            ResetPanels();

            Guid showId = new Guid(hdnId.Value);

            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            var show = (Show)showService.GetShow(showId);
            bool success = false;

            if(show != null)
            {
                short? setNumber = (short)show.Sets.Count;
                setNumber++;

                Guid setId = Guid.NewGuid();

                Set set = new Set()
                {
                    CreatedDate = DateTime.UtcNow,
                    Encore = chkEncore.Checked,
                    SetId = setId,
                    SetNumber = setNumber,
                    ShowId = showId,
                    Official = true
                };

                setService.SaveCommit(set, out success);
            }

            if (success)
            {
                phSuccess.Visible = true;
                phError.Visible = false;
            }
            else
            {
                phSuccess.Visible = false;
                phError.Visible = true;
            }

            Bind();
        }

        public void rptSets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ResetPanels();

            bool success = true;
            bool needToCommit = false;

            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            Guid showId = new Guid(hdnId.Value);

            var show = (Show)showService.GetShow(showId);

            using (IUnitOfWork uow = TheCore.Infrastructure.UnitOfWork.Begin())
            {
                Guid setId = new Guid(e.CommandArgument.ToString());

                var set = (Set)setService.GetSet(setId);

                if (set != null)
                {
                    if (e.CommandName.ToLower() == "remove")
                    {
                        needToCommit = true;
                        RemoveSet(set, show, ref setService);
                    }
                    else if (e.CommandName.ToLower() == "up")
                    {
                        needToCommit = true;
                        MoveSetUp(set, show, ref setService);
                    }
                    else if (e.CommandName.ToLower() == "down")
                    {
                        needToCommit = true;
                        MoveSetDown(set, show, ref setService);
                    }
                    else if (e.CommandName.ToLower() == "change")
                    {
                        needToCommit = true;
                        Response.Redirect(LinkBuilder.AddSongsToSetControlLink(set.SetId, string.Format("/Admin/AddSetsToShow.aspx?id={0}", showId)));
                    }

                    try
                    {
                        if (needToCommit)
                        {
                            uow.Commit();

                            success = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                    }
                }
            }

            DetermineSuccess(success);

            Bind();
        }

        private void DetermineSuccess(bool success)
        {
            if (success)
            {
                phError.Visible = false;
                phSuccess.Visible = true;
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
        }

        private void MoveSetDown(Set set, Show show, ref SetService setService)
        {
            if (set.SetNumber != show.Sets.OrderBy(x => x.SetNumber).Last().SetNumber)
            {
                var songAfter = (Set)setService.GetSet(show.Sets.Where(x => x.SetNumber == set.SetNumber + 1).First().SetId);

                set.SetNumber++;
                songAfter.SetNumber--;
            }
        }

        private void MoveSetUp(Set set, Show show, ref SetService setService)
        {
                if (set.SetNumber != 1)
                {
                    var setBefore = (Set)setService.GetSet(show.Sets.Where(x => x.SetNumber == set.SetNumber - 1).First().SetId);

                    set.SetNumber--;
                    setBefore.SetNumber++;
                }
        }

        private void RemoveSet(Set set, Show show, ref SetService setService)
        {
            set.Deleted = true;
            set.DeletedDate = DateTime.Now;

            setService.Delete(set);

            short? setNumber = 1;

            var setList = show.Sets.OrderBy(x => x.SetNumber).Where(x => x.SetId != set.SetId);

            foreach (var s in setList)
            {
                s.SetNumber = setNumber;
                setNumber++;
            }
        }

        private void Bind()
        {
            Guid showId = new Guid(hdnId.Value);

            SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            var sets = setService.GetSetsForShow(showId).ToList();

            FinalSetNumber = 1;

            if (sets != null)
            {
                if (sets.Count > 0)
                {
                    FinalSetNumber = sets.Count;
                }

                rptSets.DataSource = sets;
                rptSets.DataBind();
            }
        }
    }
}
