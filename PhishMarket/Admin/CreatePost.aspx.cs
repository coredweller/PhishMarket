using System;
using TheCore.Services;
using TheCore.Infrastructure;
using TheCore.Repository;
using PhishPond.Concrete;
using System.Web.Security;
using TheCore.Helpers;

namespace PhishMarket.Admin
{
    public partial class CreatePost : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Roles.IsUserInRole("Administrators"))
            {
                Response.Redirect(new LinkBuilder().DashboardLink());
            }
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            PostService service = new PostService(Ioc.GetInstance<IPostRepository>());

            bool success = false;
            DateTime postedDate;

            if (Validated(out postedDate))
            {
                Post post = new Post()
                {
                    PostId = Guid.NewGuid(),
                    Entry = txtEntry.Text.Trim(),
                    PostedBy = txtPostedBy.Text.Trim(),
                    PostedDate = postedDate,
                    Title = txtTitle.Text.Trim(),
                    TitleUrl = txtTitleUrl.Text.Trim(),
                };

                service.SaveCommit(post, out success);
            }

            if (success)
            {
                phSuccess.Visible = true;
                phError.Visible = false;
            }
            else
            {
                phError.Visible = true;
                phSuccess.Visible = false;
            }
        }

        private bool Validated(out DateTime postedDate)
        {
            bool valid = false;
            postedDate = DateTime.MinValue;

            try
            {
                if (!string.IsNullOrEmpty(txtPostedDate.Text.Trim()))
                {
                    DateTime tempDate;

                    bool validDate = DateTime.TryParse(txtPostedDate.Text.Trim(), out tempDate);

                    if (validDate) 
                        postedDate = tempDate;
                    else 
                        postedDate = DateTime.Now;
                }
                else
                {
                    postedDate = DateTime.Now;
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
