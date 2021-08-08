using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Enum
{
    /// <summary>
    /// Giới tính
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Nam
        /// </summary>
        Male=1,
        /// <summary>
        /// Nữ
        /// </summary>
        Female=0,
        /// <summary>
        /// Khác
        /// </summary>
        Other=2
    }
    /// <summary>
    /// Tình trạng làm việc
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// Đang làm việc
        /// </summary>
        Working = 0,
        /// <summary>
        /// Đã nghỉ việc
        /// </summary>
        Retired = 1,
        /// <summary>
        /// Khác
        /// </summary>
        Other = 2
    }
    /// <summary>
    /// Trạng thái của Object
    /// </summary>
    public enum EntityState
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        AddNew = 1,
        /// <summary>
        /// Cập nhật
        /// </summary>
        Update = 2,
        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3
    }
    /// <summary>
    /// Trạng thái của Object
    /// </summary>
    public enum MISACode
    {
        /// <summary>
        /// Hợp lệ
        /// </summary>
        Valid = 100,

        /// <summary>
        /// Không hợp lệ
        /// </summary>
        InValid = 200,

        /// <summary>
        /// Thành công  
        /// </summary>
        Success = 900,
    }
}
