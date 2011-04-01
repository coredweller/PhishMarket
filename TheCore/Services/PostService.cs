using System;
using System.Collections.Generic;
using TheCore.Repository;
using TheCore.Helpers;
using TheCore.Interfaces;
using TheCore.Infrastructure;

namespace TheCore.Services
{
    public class PostService
    {
        IPostRepository _repo;

        public PostService(IPostRepository repo)
        {
            Checks.Argument.IsNotNull(repo, "repo");

            _repo = repo;
        }

        public IList<IPost> GetAllPosts()
        {
            return _repo.FindAll();
        }

        public IPost GetPost(Guid id)
        {
            return _repo.FindByPostId(id);
        }

        public IPost GetPost(string title)
        {
            return _repo.FindByTitle(title);
        }

        public void SaveCommit(IPost post, out bool success)
        {
            using (IUnitOfWork uow = UnitOfWork.Begin())
            {
                Save(post, out success);
                if (success)
                    uow.Commit();
            }
        }

        //consider changing the out parameter to a validation type object
        public void Save(IPost post, out bool success)
        {
            Checks.Argument.IsNotNull(post, "post");

            success = false;

            if (null == _repo.FindByPostId(post.PostId))
            {
                try
                {
                    _repo.Add(post);
                    success = true;
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }
        }

        //make it delete any shows it is related to.  or not if you want those always kept.
        public void Delete(IPost post)
        {
            Checks.Argument.IsNotNull(post, "post");

            using (IUnitOfWork u = UnitOfWork.Begin())
            {
                _repo.Remove(post);
                u.Commit();
            }
        }
    }
}
