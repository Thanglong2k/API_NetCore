using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Dữ liệu trả về thông báo lỗi
    /// </summary>
    public class ResponseError
    {
        /// <summary>
        /// Message thông báo cho Dev
        /// </summary>
        public string DevMsg { get; set; } = string.Empty;
        /// <summary>
        /// Message thông báo cho User
        /// </summary>
        public string UserMsg { get; set; } = string.Empty;
        /// <summary>
        /// Mã code
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;
    }
}
