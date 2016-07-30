using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class EmailVerifyMailContext
    {
        public string UserName { get; set; }
        public string PatientName { get; set; }
        public string NewEmail { get; set; }
        public string VerifyToken { get; set; }
    }
}
