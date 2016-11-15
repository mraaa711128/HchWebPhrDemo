using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        static UserRepository repository;
        public static UserRepository GetRepository()
        {
            //if (repository == null)
            //{
            //    repository = new UserRepository();
            //}
            //return repository;
            return new UserRepository();
        }
    }
}
