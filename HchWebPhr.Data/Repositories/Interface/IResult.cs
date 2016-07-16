using System;
using System.Collections.Generic;

namespace HchWebPhr.Data.Repository
{
    /// <summary>
    /// 回傳結果
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        bool Success { get; set; }
        /// <summary>
        /// 訊息
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// 錯誤
        /// </summary>
        Exception Exception { get; set; }
        /// <summary>
        /// 錯誤集合
        /// </summary>
        List<IResult> InnerResults { get; }
    }
}
