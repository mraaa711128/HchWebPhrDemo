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
    public class VaccineBiz : BaseBiz
    {

        public bool GetChildrenList(string MomChartNo, out IList<VaccineChildInfo> ChildrenList)
        {
            ChildrenList = new List<VaccineChildInfo>();
            var svc = new HchService();
            var list = svc.GetChildrenListByMomChartNo(MomChartNo);
            if (list.IsNullOrEmpty())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到任何子女紀錄!";
                return false;
            }
            ChildrenList = list.Select(c => new VaccineChildInfo
            {
                ChartNo = c.ChartNo,
                PtName = c.PtName,
                Sex = c.Sex,
                BirthDate = c.BirthDate.toDateTime()
            }).ToList();
            return true;
        }

        public bool GetChildVaccineList(string ChildChartNo, out IList<VaccineDetailInfo> VaccineList)
        {
            VaccineList = new List<VaccineDetailInfo>();
            var svc = new HchService();
            var list = svc.GetChildrenVaccineDetailListByChildChartNo(ChildChartNo);
            if (list.IsNullOrEmpty ())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到任何疫苗注射紀錄!";
                return false;
            }
            VaccineList = list.Select(v => new VaccineDetailInfo
            {
                InjectDate = v.InoculateDate.toDateTime(),
                VaccineCode = v.VaccineCode,
                VaccineName = v.VaccineName,
                Height = v.Height,
                HeightRate = v.HeightRate,
                Weight = v.Weight,
                WeightRate = v.WeightRate,
                HeadCircumference = v.HeadCircumference,
                HeadCircumferenceRate = v.HeadCircumferenceRate,
                Temperature = v.Temperature
            }).ToList();
            return true;
        }
    }
}
