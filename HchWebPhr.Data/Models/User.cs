using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HchWebPhr.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string ActiveToken { get; set; }
        public string ForgetPasswordToken { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }
        public DateTime ActivateDateTime { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        [ForeignKey("User")]
        public int UserInfoId { get; set; }
        public string Name { get; set; }
        public string ChartNo { get; set; }
        public string IdNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string BloodType { get; set; }
        public string ProfileImage { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
