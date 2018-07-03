using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Models.ViewModels
{
    public class PregnancyDetailInfo
    {
        public int PregnancyTimes { get; set; }         //懷孕序號
        public DateTime CheckUpDate { get; set; }         //檢查日期
        public string JsonCheckUpDate {
            get {
                return this.CheckUpDate.ToString("yyyy/MM/dd");
            }
        }
        public string TwCheckUpDate {
            get {
                return string.Format("{0:D3}/{1:MM/dd}", this.CheckUpDate.Year - 1911, this.CheckUpDate);
            }
        }

        public decimal Weeks { get; set; }              //週數
        public decimal BodyWeight { get; set; }         //體重(單位:公斤)
        public int bpSystolic { get; set; }             //收縮壓(單位:mmHg)
        public int bpDiastolic { get; set; }            //舒張壓(單位:mmHg)
        public string Albuminuria { get; set; }            //蛋白尿
        public string Glucosuria { get; set; }             //尿糖
    }
}
