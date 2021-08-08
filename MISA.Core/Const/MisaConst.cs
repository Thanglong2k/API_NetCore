using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Const
{
    /// <summary>
    /// Class khai báo các mã lỗi const
    /// </summary>
    public static class MisaConst
    {
        /// <summary>
        /// Mã lỗi ngoại lệ
        /// </summary>
        public const string MISACodeErrorException="MISA-500";
        /// <summary>
        /// Mã lỗi rỗng
        /// </summary>
        public const string MISACodeEmpty="MISA-001";
        /// <summary>
        /// Mã lỗi trùng mã
        /// </summary>
        public const string MISACodeDuplicate="MISA-002";
        /// <summary>
        /// Mã lỗi định dạng
        /// </summary>
        public const string MISACodeValidate = "MISA-003";
        /// <summary>
        /// Mã lỗi dữ liệu không hợp lệ
        /// </summary>
        public const string MISACodeInValid = "MISA-003";
    }
}
