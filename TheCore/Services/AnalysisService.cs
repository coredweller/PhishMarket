using System;
using System.Linq;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class AnalysisService
    {
        IAnalysisRepository _repo;

        public AnalysisService(IAnalysisRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");
            _repo = repo;
        }

        public IQueryable<IAnalysis> GetAllAnalysis()
        {
            return _repo.FindAll();
        }
        
        public IQueryable<IAnalysis> GetAnalysisByMyShow(Guid myShowId)
        {
            return _repo.FindAll().Where(x => x.MyShowId == myShowId);
        }

        public IQueryable<IAnalysis> GetAnalysisBySetSong(Guid setSongId)
        {
            return _repo.FindAll().Where(x => x.SetSongId == setSongId);
        }

        public IQueryable<IAnalysis> GetAnalysisBySong(Guid songId)
        {
            var setSongService = new SetSongService(Ioc.GetInstance<ISetSongRepository>());
            var setSongs = setSongService.GetSetSongBySong(songId);

            var setSongIds = (from s in setSongs
                              select s.SetSongId);

            return _repo.FindAll().Where(x => setSongIds.Contains(x.SetSongId));
        }

        public IQueryable<IAnalysis> GetAnalysisBySetSongAndUser(Guid setSongId, Guid userId)
        {
            return _repo.FindAll().Where(x => x.SetSongId == setSongId && x.UserId == userId);
        }

        public void SaveCommit(IAnalysis analysis, out bool success)
        {
            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                Save(analysis, out success);
                if (success)
                    u.Commit();
            }
        }

        public void Save(IAnalysis analysis, out bool success)
        {
            Checks.Argument.IsNotNull(analysis, "myShow");

            success = false;

            if (null == _repo.FindById(analysis.AnalysisId).SingleOrDefault())
            {
                try
                {
                    _repo.Add(analysis);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        public void DeleteCommit(IAnalysis analysis)
        {
            Checks.Argument.IsNotNull(analysis, "analysis");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(analysis);
                u.Commit();
            }
        }

        public void Delete(IAnalysis analysis)
        {
            Checks.Argument.IsNotNull(analysis, "analysis");

            _repo.Remove(analysis);
        }
    }
}
