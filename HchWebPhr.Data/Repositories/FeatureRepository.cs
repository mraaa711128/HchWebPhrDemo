using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Data.Repositories
{
    public class FeatureRepository : GenericRepository<Feature>
    {
        static FeatureRepository repository;
        public static FeatureRepository GetRepository()
        {
            //if (repository == null)
            //{
            //    repository = new FeatureRepository();
            //}
            //return repository;
            return new FeatureRepository();
        }
    }
}
