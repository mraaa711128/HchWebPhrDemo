using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class ForgetPasswordMailContext
    {
        public string UserName { get; set; }
        public string ForgetPasswordToken { get; set; }
    }
}
