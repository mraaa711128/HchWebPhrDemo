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
    public class MedBiz : BaseBiz
    {
        public IList<SelectListModel> GetClinicDivs()
        {
            var results = new List<SelectListModel> {
                new SelectListModel { Value = "*", Text = "全部", Selected = true },
                new SelectListModel { Value = "01", Text = "家庭醫學科", Selected = false },
                new SelectListModel { Value = "03", Text ="健兒門診", Selected = false },
                new SelectListModel { Value = "04", Text ="小兒科", Selected = false },
                new SelectListModel { Value = "05", Text = "婦產科", Selected = false }
            };
            return results;
        }

        public bool GetMedListByChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate, out IList<MedListInfo> MedList)
        {
            MedList = new List<MedListInfo>();
            var svc = new HchService();
            var medList = svc.GetMedListByChartNoAndDateRange(ChartNo, StartDate, EndDate);
            if (medList.IsNullOrEmpty())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到任何藥品紀錄!";
                return false;
            }
            MedList = medList.Select(x => new MedListInfo
            {
                ClinicDate = x.ClinicDate.toDateTime(),
                DivNo = x.DivNo,
                DivName = x.DivName
            }).ToList();
            return true;
        }

        public bool GetMedDetailByChartNoClinicDateAndDivNo(string ChartNo, DateTime ClinicDate, string DivNo, out IList<MedItemInfo> MedItems)
        {
            MedItems = new List<MedItemInfo>();
            var svc = new HchService();
            var medItems = svc.GetMedDetailByChartNoDivNoAndDate(ChartNo, ClinicDate, DivNo);
            if (medItems.IsNullOrEmpty())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到任何藥品紀錄！";
                return false;
            }
            MedItems = medItems.Select(x => new MedItemInfo
            {
                MedCode = x.MedCode,
                MedName = x.MedName,
                ChineseName = x.ChiName,
                EnglishName = x.EngName,
                MethodCode = x.MethodCode,
                MethodName = x.MethodName,
                FreqCode = x.FreqCode,
                FreqName = x.FreqName,
                DoseQty = x.DoseQty,
                DoseUnit = x.DoseUnit,
                AppearanceCode = x.AppearanceCode,
                AppearanceName = x.AppearanceName,
                IndicationCode = x.IndicationCode,
                IndicationName = x.IndicationName,
                SideEffectCode = x.SideEffectCode,
                SideEffectName = x.SideEffectName
            }).ToList();
            return true;
        }
    }
}
