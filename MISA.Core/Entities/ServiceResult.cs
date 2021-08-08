using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Kết quả trả về
    /// Createdby: TTLONG (28/7/2021)
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// kiểm tra thành công hay không
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Thông báo cho lập trình viên
        /// </summary>
        public string DevMsg { get; set; }
        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string UserMsg { get; set; }
        /// <summary>
        /// Mã code
        /// </summary>
        public string MISACode { get; set; }
    }
}
