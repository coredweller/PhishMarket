using System;
using System.Web.UI.WebControls;
using TheCore.Services;
using TheCore.Infrastructure;
using PhishPond.Concrete;
using TheCore.Repository;

namespace PhishMarket.Admin
{
    public partial class CreateSong : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            ddlJamType.Items.AddRange(GetDropDownFromEnum(typeof(JamType), 1, "Please select a Jam Type"));
            for (int i = 1; i < 100; i++)
            {
                ddlOrder.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            ListItem item = new ListItem("None", "0");

            ddlOrder.Items.Insert(0, item);

            item.Selected = true;
        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {
            SongService service = new SongService(Ioc.GetInstance<ISongRepository>());

            bool success = false;
            double length = 0;
            short? order = null;
            short? jamType = null;

            if (Validated(out length, out order, out jamType))
            {

                Song song = new Song()
                {
                    SongId = Guid.NewGuid(),
                    SongName = txtSongName.Text.Trim(),
                    SpecialAppearances = txtSpecialAppearances.Text.Trim(),
                    Album = txtAlbum.Text.Trim(),
                    Order = order,
                    Length = length,
                    JamStyle = jamType,
                    Cover = chkCover.Checked,
                    Notes = txtNotes.Text.Trim(),
                    Abbreviation = txtAbbreviation.Text.Trim()
                };

                service.SaveCommit(song, out success);
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

        private bool Validated(out double length, out short? order, out short? jamType)
        {
            bool valid = false;
            order = null;
            length = 0;
            jamType = 0;

            try
            {
                if (string.IsNullOrEmpty(txtSongName.Text.Trim()))
                    valid = false;

                if (!string.IsNullOrEmpty(txtLength.Text.Trim()))
                {
                    bool validDouble = double.TryParse(txtLength.Text.Trim(), out length);

                    if (!validDouble)
                        length = 0;
                }

                if (ddlOrder.SelectedValue == "0")
                {
                    order = null;
                }
                else
                {
                    order = short.Parse(ddlOrder.SelectedValue.Trim()); ;
                }

                if (ddlJamType.SelectedValue == "0")
                {
                    jamType = null;
                }
                else
                {
                    jamType = short.Parse(ddlJamType.SelectedValue.Trim());
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
