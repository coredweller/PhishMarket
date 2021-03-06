﻿using System;
using System.Linq;
using System.Web.UI.WebControls;
using TheCore.Interfaces;
using TheCore.Services;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using System.Web.Security;
using TheCore.Repository;

namespace PhishMarket.Admin
{
    public partial class CreateShow : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void rptSets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Guid g = new Guid(e.CommandArgument.ToString());
            //bool needToCommit = false;

            //SetService setService = new SetService(Ioc.GetInstance<ISetRepository>());

            //var set = (Set)setService.GetSet(g);

            //if (e.CommandName.ToLower() == "remove")
            //{
            //    needToCommit = true;
            //    RemoveSet(set, setService);
            //}
            //else if (e.CommandName.ToLower() == "up")
            //{

            //}
            //else if (e.CommandName.ToLower() == "down")
            //{

            //}
            //else if (e.CommandName.ToLower() == "change")
            //{

            //}
        }

        //private void RemoveSet(Set set, SetService setService)
        //{
        //    ShowService showService = new ShowService(Ioc.GetInstance<IShowRepository>());
            



        //    set.Deleted = true;
        //    set.DeletedDate = DateTime.Now;

        //    setService.Delete(set);

        //}

        private void Bind()
        {
            //Show Rank
            var items = GetDropDownFromEnum(typeof(ShowRank), 1, "Please select a Show Rank");

            ddlRank.Items.AddRange(items);
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowService service = new ShowService(Ioc.GetInstance<IShowRepository>());

            bool success = false;
            short? rank;
            DateTime? showDate;
            Guid? showId = null;
            Guid? tourId = null;

            if (Validated(out rank, out showDate))
            {
                //order = ddlOrder.SelectedValue != "0" ? (short?)short.Parse(ddlOrder.SelectedValue) : null;

                if (string.IsNullOrEmpty(txtGuid.Text))
                {
                    showId = Guid.NewGuid();
                }
                else
                {
                    showId = new Guid(txtGuid.Text);
                }

                Show show = new Show()
                {
                    ShowId = showId.Value,
                    //ShowName = txtShowName.Text.Trim(),
                    VenueName = txtVenueName.Text.Trim(),
                    City = txtCity.Text.Trim(),
                    State = ddlStates.SelectedValue.Trim(),
                    Country = ddlCountry.Text.Trim(),
                    //Order = order,
                    //TicketPrice = ticketPrice,
                    Notes = txtNotes.Text.Trim(),
                    ShowDate = showDate,
                    //Official = chkOfficial.Checked,
                    UserId = new Guid(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString()),
                    TourId = tourId
                };

                service.SaveCommit(show, out success);
            }

            if (success)
            {
                lnkAddSetsToShow.NavigateUrl = LinkBuilder.AddSetsToShowLink(showId.Value);
                phAddSetsToShow.Visible = true;
                phSuccess.Visible = true;
                phError.Visible = false;
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
        }

        private bool Validated(out short? rank, out DateTime? showDate)
        {
            bool valid = false;
            //order = null;
            //ticketPrice = 0;
            rank = null;
            showDate = null;

            try
            {
                //if (string.IsNullOrEmpty(txtShowName.Text.Trim()))
                //    valid = false;

                //if (!string.IsNullOrEmpty(txtTicketPrice.Text.Trim()))
                //{
                //    bool validDouble = decimal.TryParse(txtTicketPrice.Text.Trim(), out ticketPrice);

                //    if (!validDouble)
                //        ticketPrice = 0;
                //}

                if (ddlRank.SelectedValue == "0")
                {
                    rank = null;
                }
                else
                {
                    rank = short.Parse(ddlRank.SelectedValue.Trim());
                }

                if (!string.IsNullOrEmpty(txtShowDate.Text.Trim()))
                {
                    DateTime tempDate;

                    bool validDate = DateTime.TryParse(txtShowDate.Text.Trim(), out tempDate);

                    if (!validDate)
                        showDate = null;
                    else
                        showDate = tempDate;
                }

                valid = true;
            }
            catch (Exception ex)
            {
                valid = false;
            }

            return valid;

        }
    }
}
