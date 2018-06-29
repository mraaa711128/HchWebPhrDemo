using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class PregnancyListInfo
    {
        public int PregnancyTime { get; set; }
        public DateTime? FirstCheckDate {
            get {
                return this.DetailList.Min(x => x.CheckUpDate);
            }
        }
        public string JsonFirstCheckDate {
            get {
                if (this.FirstCheckDate == null) { return string.Empty; }
                return this.FirstCheckDate?.ToString("yyyy/MM/dd");
            }
        }
        public string TwFirstCheckDate {
            get {
                if (this.FirstCheckDate == null) { return string.Empty; }
                return string.Format("{0:D3}/{1:MM/dd}", this.FirstCheckDate?.Year - 1911, this.FirstCheckDate);
            }
        }
        public DateTime? LastCheckDate {
            get {
                return this.DetailList.Max(x => x.CheckUpDate);
            }
        }
        public string JsonLastCheckDate {
            get {
                if (this.LastCheckDate == null) { return string.Empty; }
                return this.LastCheckDate?.ToString("yyyy/MM/dd");
            }
        }
        public string TwLastCheckDate {
            get {
                if (this.LastCheckDate == null) { return string.Empty; }
                return string.Format("{0:D3}/{1:MM/dd}", this.LastCheckDate?.Year - 1911, this.LastCheckDate);
            }
        }
        public IList<PregnancyDetailInfo> DetailList { get; set; } = new List<PregnancyDetailInfo>();
    }
}
