using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class PhotoAlbumInfo
    {
        public string ApplyNo { get; set; }
        public string ExamCode { get; set; }
        public string ExamName { get; set; }
        public string ExamType { get; set; }
        public string ExamTypeDesc { get; set; }
        public DateTime ExamDate { get; set; }
        public bool HasImage { get; set; }
        public string ThumbnailImage { get; set; }
        public IList<PhotoInfo> Photos { get; set; }

    }
}
