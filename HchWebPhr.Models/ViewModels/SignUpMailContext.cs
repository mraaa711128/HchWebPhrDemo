using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class SignUpMailContext
    {
        public string IdNo { get; set; }
        public string PatientName { get; set; }
        public string EMail { get; set; }
        public DateTime BirthDate { get; set; }
        public string ActiveToken { get; set; }
    }
}
