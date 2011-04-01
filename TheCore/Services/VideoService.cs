using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class VideoService
    {
        IVideoRepository _repo;

        public VideoService(IVideoRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IList<IVideo> GetAllVideos()
        {
            return _repo.FindAll();
        }

        public IVideo GetVideo(Guid id)
        {
            return _repo.FindByVideoId(id);
        }

        public void SaveCommit(IVideo video, bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(video, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(IVideo video, out bool success)
        {
            Checks.Argument.IsNotNull(video, "video");

            success = false;

            if (null == _repo.FindByVideoId(video.VideoId))
            {
                try
                {
                    _repo.Add(video);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete(IVideo video)
        {
            Checks.Argument.IsNotNull(video, "video");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(video);
                u.Commit();
            }
        }
    }
}
