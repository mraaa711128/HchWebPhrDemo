using HchWebPhr.Biz.Base;
using HchWebPhr.Models.ViewModels;
using HchWebPhr.Service;
using HchWebPhr.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShareDll.PHR;

namespace HchWebPhr.Biz
{
    public class LabBiz : BaseBiz
    {
        public bool GetLabList(string ChartNo, DateTime StartDate, DateTime EndDate, out IList<LabListInfo> LabList)
        {
            LabList = new List<LabListInfo>();
            var svc = new HchService();
            var labList = svc.GetLabListByChartNoAndDateRange(ChartNo, StartDate, EndDate);
            if (labList.IsNullOrEmpty())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到任何檢驗檢查!";
                return false;
            }
            LabList = labList.Select(x => new LabListInfo
            {
                LabNo = x.LabNo,
                LabName = x.LabName,
                LabDate = x.LabDate.toDateTime(),
                LabType = x.LabType,
                LabGroupCode = x.LabGroupCode,
                LabGroupName = x.LabGroupName
            }).ToList();
            return true;
        }

        public bool GetLabResultX(string LabNo, out IList<LabItemXInfo> LabResultX)
        {
            LabResultX = new List<LabItemXInfo>();
            var svc = new HchService();
            var resultList = svc.GetNormalLabResultByLabNo(LabNo);
            if (resultList.IsNullOrEmpty())
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到此檢驗單(" + LabNo + ")的檢驗結果";
                return false;
            }
            LabResultX = resultList.OrderBy(x => x.Seq).Select(x => new LabItemXInfo
            {
                LabCode = x.LabCode,
                LabName = x.LabName,
                Result = x.Result,
                Unit = x.Unit,
                IsParent = x.IsParent,
                NeedCompare = x.NeedCompare,
                Reference = x.Reference,
                LowerLimit = x.LowerLimit,
                UpperLimit = x.UpperLimit,
                RiskLowerLimit = x.RiskLowerLimit,
                RiskUpperLimit = x.RiskUpperLimit,
                LabSubItems = x.IsParent == false || x.SubLabDetail == null ? null: x.SubLabDetail.OrderBy(y => y.Seq).Select(y => new LabItemXInfo
                {
                    LabCode = y.LabCode,
                    LabName = y.LabName,
                    Result = y.Result,
                    Unit = y.Unit,
                    IsParent = false,
                    NeedCompare = y.NeedCompare,
                    Reference = y.Reference,
                    LowerLimit = y.LowerLimit,
                    UpperLimit = y.UpperLimit,
                    RiskLowerLimit = y.RiskLowerLimit,
                    RiskUpperLimit = y.RiskUpperLimit,
                    LabSubItems = null
                }).ToList()
            }).ToList();
            return true;
        }

        public bool GetLabResultG(string LabNo, out LabItemGInfo LabResultG)
        {
            LabResultG = null;
            var diagnosis = "";
            var comment = "";
            var svc = new HchService();
            if (svc.GetDiagnosisLabResultByLabNo(LabNo, out diagnosis, out comment) == false)
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到此檢驗單(" + LabNo + ")的檢驗結果";
                return false;
            }
            LabResultG = new LabItemGInfo
            {
                Diagnosis = diagnosis,
                Comment = comment
            };
            return true;
        }

        public bool GetLabResultN(string LabNo, out LabItemNInfo LabResultN)
        {
            LabResultN = null;
            var PdfFilePath = "";
            var svc = new HchService();
            if (svc.GetPdfLabResultByLabNo(LabNo, out PdfFilePath) == false)
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "找不到此檢驗單(" + LabNo + ")的檢驗結果";
                return false;
            }
            LabResultN = new LabItemNInfo
            {
                PdfFilePath = PdfFilePath.Replace("\\", "/")
            };
            return true;
        }
    }
}
