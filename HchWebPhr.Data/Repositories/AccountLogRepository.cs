﻿using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Data.Repositories
{
    public class AccountLogRepository :GenericRepository<AccountLog>
    {
        static AccountLogRepository repository;
        public static AccountLogRepository GetRepository()
        {
            //if (repository == null)
            //{
            //    repository = new AccountLogRepository();
            //}
            //return repository;
            return new AccountLogRepository();
        }
    }
}
