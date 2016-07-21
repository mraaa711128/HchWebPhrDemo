using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class ForgetUserNameMailContext
    {
        public string UserName { get; set; }
        public string PatientName { get; set; }
        public DateTime BirthDate { get; set; }
        public string TwBirthDate
        {
            get
            {
                return string.Format("{0:000}/{1:MM/dd}", BirthDate.Year, BirthDate);
            }
        }
        public string IdNo { get; set; }
    }
}
