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

namespace PhishMarket
{
    public partial class AddSetsSpecial : PhishMarketBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Roles.IsUserInRole("Administrators"))
                Response.Redirect(LinkBuilder.DashboardLink());
        }

        public void btnBrih_Click(object sender, EventArgs e)
        {
            var setService = new SetService(Ioc.GetInstance<ISetRepository>());
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());

            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                //bool success = false;
                //bool success2 = false;
                //bool success3 = false;

                //setService.Save(newSet, out success);

                //setSongService.Save(davidbowie, out success2);

                ////setSongService.Save(highway, out success3);

                //if (success && success2)
                //{
                //    uow.Commit();
                //}
            }
        }

        public void Old()
        {
            //11/10/1989
            //var set1Id = Guid.NewGuid();
            //var newSet = new Set
            //{
            //    SetId = set1Id,
            //    CreatedDate = DateTime.Now,
            //    Encore = true,
            //    Official = true,
            //    SetNumber = 4,
            //    ShowId = new Guid("8DD94D9D-5990-4AE5-B2DB-9ED97A74932C")
            //};

            //var newSetSong1 = new SetSong
            //{
            //    Album = "Live ONLY",
            //    Cover = true,
            //    CreatedDate = DateTime.Now,
            //    Order = 1,
            //    Segue = true,
            //    SetId = set1Id,
            //    SetSongId = Guid.NewGuid(),
            //    SongId = new Guid("145A0F0C-11AE-4802-BDFF-EB6A5022511F"),
            //    SongName = "Take the 'A' Train"
            //};

            //var newSetSong2 = new SetSong
            //{
            //    Album = "Lawn Boy",
            //    Cover = false,
            //    CreatedDate = DateTime.Now,
            //    Order = 2,
            //    Segue = false,
            //    SetId = set1Id,
            //    SetSongId = Guid.NewGuid(),
            //    SongId = new Guid("83DECDA9-20A8-47E5-B284-838F9B1C98D2"),
            //    SongName = "Run Like an Antelope"
            //};

            //10/19/1990
            //var set2Id = Guid.NewGuid();
            //var newSet = new Set
            //{
            //    SetId = set2Id,
            //    CreatedDate = DateTime.Now,
            //    Encore = true,
            //    Official = true,
            //    SetNumber = 3,
            //    ShowId = new Guid("0EA43936-D277-48FA-BB6D-CD351F19F9B4")
            //};

            //var newSetSong = new SetSong
            //{
            //    Album = "Live ONLY",
            //    Cover = true,
            //    CreatedDate = DateTime.Now,
            //    Order = 1,
            //    Segue = false,
            //    SetId = set2Id,
            //    SetSongId = Guid.NewGuid(),
            //    SongId = new Guid("9DB8A27D-8165-4A34-965D-47807732B5D9"),
            //    SongName = "Paul and Silas"
            //};

            //12/8/1990
            //var set1Id = Guid.NewGuid();
            //var newSet = new Set
            //{
            //    SetId = set1Id,
            //    CreatedDate = DateTime.Now,
            //    Encore = true,
            //    Official = true,
            //    SetNumber = 4,
            //    ShowId = new Guid("0C720940-4963-4B8D-B9F3-47673C00D0CC")
            //};

            //var contact = new SetSong
            //{
            //    Album = "Junta",
            //    Cover = false,
            //    CreatedDate = DateTime.Now,
            //    Order = 1,
            //    Segue = false,
            //    SetId = set1Id,
            //    SetSongId = Guid.NewGuid(),
            //    SongId = new Guid("5D33C2D5-D4AB-4DA5-AA9B-4F555EF0E77D"),
            //    SongName = "Contact"
            //};

            //var highway = new SetSong
            //{
            //    Album = "Live ONLY",
            //    Cover = true,
            //    CreatedDate = DateTime.Now,
            //    Order = 2,
            //    Segue = false,
            //    SetId = set1Id,
            //    SetSongId = Guid.NewGuid(),
            //    SongId = new Guid("839EB925-0FBC-4E65-A4B7-524535C79D7A"),
            //    SongName = "Highway to Hell"
            //};

            //5/1/1992
            //var set1Id = Guid.NewGuid();
            //var newSet = new Set
            //{
            //    SetId = set1Id,
            //    CreatedDate = DateTime.Now,
            //    Encore = true,
            //    Official = true,
            //    SetNumber = 4,
            //    ShowId = new Guid("876E292A-58F0-4D86-A8DA-CF5B9581B5B3")
            //};

            //var rocky = new SetSong
            //{
            //    Album = "Live ONLY",
            //    Cover = true,
            //    CreatedDate = DateTime.Now,
            //    Order = 1,
            //    Segue = false,
            //    SetId = set1Id,
            //    SetSongId = Guid.NewGuid(),
            //    SongId = new Guid("C595A2AC-7C3E-4C9C-A571-6C7C88AF6342"),
            //    SongName = "Rocky Top"
            //};

            //7/2/1997
            //var set1Id = Guid.NewGuid();
            //var newSet = new Set
            //{
            //    SetId = set1Id,
            //    CreatedDate = DateTime.Now,
            //    Encore = true,
            //    Official = true,
            //    SetNumber = 4,
            //    ShowId = new Guid("C73B6914-29CB-4A47-B8A0-399A38CC9C20")
            //};

            //var davidbowie = new SetSong
            //{
            //    Album = "Junta",
            //    Cover = false,
            //    CreatedDate = DateTime.Now,
            //    Order = 1,
            //    Segue = false,
            //    SetId = set1Id,
            //    SetSongId = Guid.NewGuid(),
            //    SongId = new Guid("F962F56F-F78B-4061-AE0F-4106F99C31F7"),
            //    SongName = "David Bowie"
            //};
        }
    }
}