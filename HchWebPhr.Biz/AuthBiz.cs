using HchWebPhr.Biz.Base;
using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repositories;
using HchWebPhr.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Biz
{
    public class AuthBiz : BaseBiz
    {
        public IList<Feature> GetAllFeatures()
        {
            var ft = FeatureRepository.GetRepository();
            var features = ft.GetAll();
            return features.ToList();
        }

        public bool SaveFeature(FeatureModel FeatureObj)
        {
            var ft = FeatureRepository.GetRepository();
            var feature = ft.Get(x => x.FeatureId.Equals(FeatureObj.FeatureId));
            if (feature == null)
            {
                feature = new Feature
                {
                    FeatureId = FeatureObj.FeatureId,
                    FeatureName = FeatureObj.FeatureName,
                    Remark = FeatureObj.Remark
                };
                if (ft.Create(feature) <=0)
                {
                    ErrorCode = "501";
                    ErrorMessage = "儲存功能失敗！";
                    return false;
                }
            } else
            {
                feature.FeatureName = FeatureObj.FeatureName;
                feature.Remark = FeatureObj.Remark;
                if (ft.Update(feature) <= 0)
                {
                    ErrorCode = "501";
                    ErrorMessage = "儲存功能失敗！";
                    return false;
                }
            }
            return true;
        }

        public bool DeleteFeature(int FeatureId)
        {
            var ft = FeatureRepository.GetRepository();
            var feature = ft.Get(x => x.FeatureId.Equals(FeatureId));
            if (feature == null)
            {
                return true;
            }
            if (ft.Delete(feature) <= 0)
            {
                ErrorCode = "501";
                ErrorMessage = "刪除功能失敗！";
                return false;
            }
            return true;
        }
    }
}
