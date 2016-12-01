using HchWebPhr.Biz.Base;
using HchWebPhr.Data.Models;
using HchWebPhr.Data.Repositories;
using HchWebPhr.Models.FormModels;
using HchWebPhr.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HchWebPhr.Biz
{
    public class ConditionTermBiz : BaseBiz
    {
        protected internal TermRepository TermRepo { get; set; }
        public ConditionTermBiz()
        {
            TermRepo = TermRepository.GetRepository();
        }
        public ConditionTerm GetEffectiveTerm(DateTime EffectiveDateTime)
        {
            var term = TermRepo.GetMany(x => x.EffectiveDateTime >= EffectiveDateTime && x.EffectiveDateTime <= DateTime.Now)
                               .OrderByDescending(x => x.EffectiveDateTime)
                               .ThenByDescending(x => x.LastModifyDateTime)
                               .FirstOrDefault();
            return term;
        }

        public IList<ConditionTermInfo> GetAllTerms()
        {
            return TermRepo.GetAll().OrderByDescending(x => x.EffectiveDateTime)
                                    .ThenByDescending(x => x.LastModifyDateTime)
                                    .Select(x => new ConditionTermInfo
                                    {
                                        Id = x.ConditionTermId.ToString(),
                                        EffectiveDateTime = x.EffectiveDateTime,
                                        TermContent = x.TermContent,
                                        LastModifyDateTime = x.LastModifyDateTime
                                    }).ToList();
        }

        public ConditionTerm GetTerm(int TermId)
        {
            return TermRepo.Get(x => x.ConditionTermId.Equals(TermId));
        }

        public bool SaveTerm(NewEditTermModel termModel)
        {
            var needCreate = termModel.Id <= 0 ? true : false;
            ConditionTerm term = null;
            if (needCreate)
            {
                term = new ConditionTerm();
            } else
            {
                term = TermRepo.Get(x => x.ConditionTermId.Equals(termModel.Id));
            }
            term.EffectiveDateTime = termModel.EffectiveDateTime;
            term.TermContent = termModel.ConditionTerm;
            term.LastModifyDateTime = DateTime.Now;
            int rtn = -1;
            if (needCreate)
            {
                rtn = TermRepo.Create(term);
            }
            else
            {
                rtn = TermRepo.Update(term);
            }
            if (rtn <= 0)
            {
                ErrorCode = "404";
                ErrorMessage = "儲存使用者條款失敗，請聯絡系統管理員！";
                return false;
            }
            return true;
        }

        public bool DeleteTerm(int termId)
        {
            var term = TermRepo.Get(x => x.ConditionTermId.Equals(termId));
            if (term != null)
            {
                var rtn = TermRepo.Delete(term);
                if (rtn > 0 ) { return true; }
            }
            ErrorCode = "404";
            ErrorMessage = "使用者條款已刪除或不存在！";
            return false;
        }
    }
}
