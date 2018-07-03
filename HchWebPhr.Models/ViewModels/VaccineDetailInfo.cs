using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class VaccineDetailInfo
    {
        public DateTime InjectDate { get; set; }           //注射日期
        public string JsonInjectDate {
            get {
                return this.InjectDate.ToString("yyyy/MM/dd");
            }
        }
        public string TwInjectDate {
            get {
                return string.Format("{0:D3}/{1:MM/dd}", this.InjectDate.Year - 1911, this.InjectDate);
            }
        }
        public string VaccineCode { get; set; }             //疫苗代碼
        public string VaccineName { get; set; }             //疫苗名稱
        public decimal Height { get; set; }                 //身高
        public string HeightRate { get; set; }              //身高生長比率
        public decimal Weight { get; set; }                 //體重
        public string WeightRate { get; set; }              //體重生長比率
        public decimal HeadCircumference { get; set; }      //頭圍
        public string HeadCircumferenceRate { get; set; }   //頭圍生長比率
        public decimal Temperature { get; set; }            //體溫
    }
}
