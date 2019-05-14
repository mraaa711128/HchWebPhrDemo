using HchWebPhr.Biz.Base;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Service;
using HchWebPhr.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Biz
{
    public class BabyBiz : BaseBiz
    {
        public bool GetChildrenList(string MomChartNo, out IList<PatientChildInfo> ChildrenList)
        {
            ChildrenList = new List<PatientChildInfo>();
            var svc = new HchService();
            var list = svc.GetChildrenListByMomChartNo(MomChartNo);
            if (list.IsNullOrEmpty())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到任何子女紀錄!";
                return false;
            }
            ChildrenList = list.Select(c => new PatientChildInfo
            {
                ChartNo = c.ChartNo,
                PtName = c.PtName,
                Sex = c.Sex,
                BirthDate = c.BirthDate.toDateTime()
            }).ToList();
            return true;
        }
    }
}
