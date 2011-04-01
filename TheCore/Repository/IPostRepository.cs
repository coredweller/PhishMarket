using System;
using System.Collections.Generic;
using TheCore.Interfaces;

namespace TheCore.Repository
{
    public interface IPostRepository
    {
        IList<IPost> FindAll();
        IPost FindByPostId(Guid id);
        IPost FindByTitle(string title);
        void Add(IPost entity);
        void Remove(IPost entity);
    }
}
