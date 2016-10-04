using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Data.Repositories
{
    public class TermRepository : GenericRepository<ConditionTerm>
    {
        static TermRepository repository;

        public static TermRepository GetRepository()
        {
            if (repository == null)
            {
                repository = new TermRepository();
            }
            return repository;
        }
    }
}
