using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tiara_api.Models;

namespace tiara_api.Repository.IRepository
{
    public interface IThoughtsRepository
    {
        IQueryable<Thought> GetThoughts();
        Thought GetThought(int thoughtId);
        bool AddThought(Thought thought);
        bool Updatethought(Thought thought);
        bool DeleteThought(int thoughtId);
        bool Save();
    }
}
