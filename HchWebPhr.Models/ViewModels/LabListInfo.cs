using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class LabListInfo
    {
        public string LabNo { get; set; }
        public string LabName { get; set; }
        public DateTime LabDate { get; set; }
        public string JsonLabDate
        {
            get
            {
                return this.LabDate.ToString("yyyy/MM/dd");
            }
        }
        public string TwLabDate
        {
            get
            {
                return string.Format("{0:D3}/{1:MM/dd}", this.LabDate.Year - 1911, this.LabDate);
            }
        }
        public string LabType { get; set; }
        public string LabGroupCode { get; set; }
        public string LabGroupName { get; set; }
    }
}
