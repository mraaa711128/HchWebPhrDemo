using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HchWebPhr.Biz.Base;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Service;
using HchWebPhr.Utilities.Extensions;

namespace HchWebPhr.Biz
{
    public class PregnancyBiz : BaseBiz
    {
        public bool GetPregnancyListByChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate, out IList<PregnancyListInfo> PregList)
        {
            PregList = new List<PregnancyListInfo>();
            var svc = new HchService();
            var pList = svc.GetPregnancyListByChartNoAndDateRange(ChartNo, StartDate, EndDate);
            //var medList = svc.GetMedListByChartNoAndDateRange(ChartNo, StartDate, EndDate);
            if (pList.IsNullOrEmpty())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到任何產檢紀錄!";
                return false;
            }
            PregList = pList.GroupBy(x => x.PregnancyTimes)
                            .Select(pg => new PregnancyListInfo
                            {
                                PregnancyTime = pg.Key,
                                DetailList = pList.Where(p => p.PregnancyTimes == pg.Key)
                                                  .Select(d => new PregnancyDetailInfo
                                                  {
                                                      PregnancyTimes = d.PregnancyTimes,
                                                      CheckUpDate = d.CheckUpDate.toDateTime(),
                                                      Weeks = d.Weeks,
                                                      BodyWeight = d.BodyWeight,
                                                      bpSystolic = d.bpSystolic,
                                                      bpDiastolic = d.bpDiastolic,
                                                      Albuminuria = d.Albuminuria <= 0 ? "-" : new string('+', d.Albuminuria),
                                                      Glucosuria = d.Glucosuria <= 0 ? "-" : new string('+', d.Glucosuria)
                                                  })
                                                  .OrderByDescending(d => d.CheckUpDate)
                                                  .ToList()
                            })
                            .OrderBy(pg => pg.PregnancyTime)
                            .ToList();
            //PregList = pList.Select(x => new PregnancyDetailInfo
            //{
            //    PregnancyTimes = x.PregnancyTimes,
            //    CheckUpDate = x.CheckUpDate.toDateTime(),
            //    Weeks = x.Weeks ,
            //    BodyWeight = x.BodyWeight,
            //    bpSystolic = x.bpSystolic,
            //    bpDiastolic = x.bpDiastolic,
            //    Albuminuria = x.Albuminuria <= 0 ? "-" : new string ('+', x.Albuminuria),
            //    Glucosuria = x.Glucosuria <= 0 ? "-" : new string ('+', x.Glucosuria)
            //}).OrderByDescending(x => x.CheckUpDate).ToList();
            return true;
        }

    }
}
