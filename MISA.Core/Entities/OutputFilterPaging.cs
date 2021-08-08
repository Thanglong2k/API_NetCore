using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin trả về khi filter
    /// CreatedBy: TTLONG (2/8/2021)
    /// </summary>
    public class OutputFilterPaging
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// TỔng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// TỔng số trang
        /// </summary>
        public int TotalPage { get; set; }
    }
}
