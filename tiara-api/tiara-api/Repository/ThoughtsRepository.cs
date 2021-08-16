using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tiara_api.DataContext;
using tiara_api.Models;
using tiara_api.Repository.IRepository;

namespace tiara_api.Repository
{
    public class ThoughtsRepository : IThoughtsRepository
    {

        private readonly DataContextDB db;
        public ThoughtsRepository(DataContextDB _db)
        {
            db = _db;
        }

        public IQueryable<Thought> GetThoughts()
        {
            IQueryable<Thought> lines;

            lines = db.Set<Thought>();

            return lines.AsQueryable();
        }

        public Thought GetThought(int thoughtId)
        {
            throw new NotImplementedException();
        }

        public bool AddThought(Thought thought)
        {
            throw new NotImplementedException();
        }

        public bool DeleteThought(int thoughtId)
        {
            throw new NotImplementedException();
        }

        public bool Updatethought(Thought thought)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    
    }
}
