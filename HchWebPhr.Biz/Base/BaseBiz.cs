using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Biz.Base
{
    public class BaseBiz
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    
        public Exception ErrorException { get; set; }

        protected Logger _Logger
        {
            get
            {
                return LogManager.GetCurrentClassLogger(GetType());
            }
        }
    }
}
