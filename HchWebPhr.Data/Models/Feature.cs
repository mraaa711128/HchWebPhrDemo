using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Data.Models
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool RemarkDisplay { get; set; }
        [MaxLength]
        [Column(TypeName = "ntext")]
        public string Remark { get; set; }
        public bool MenuDisplay { get; set; }
        public int MenuDisplayOrder { get; set; }

    }
}
