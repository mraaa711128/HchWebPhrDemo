using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Data.Models
{
    public class AccountLog
    {
        public int AccountLogId { get; set; }
        public string UserName { get; set; }
        public string LoginIpAddress { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}
