
using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// class đối tượng chung
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// tạo thuộc tính required để sử dụng cho các thuộc tính yêu cầu bắt buộc có dữ liệu trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class Required : Attribute
        {

        }
        /// <summary>
        /// tạo thuộc tính duplicate để sử dụng cho các thuộc tính yêu cầu dữ liệu độc nhất trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class Duplicate : Attribute
        {

        }
        /// <summary>
        /// tạo thuộc tính PrimaryKey để sử dụng cho các thuộc tính khóa chính trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class PrimaryKey : Attribute
        {

        }
        /// <summary>
        /// tạo thuộc tính AEmail để sử dụng cho các thuộc tính là email trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class AEmail : Attribute
        {

        }
        /// <summary>
        /// tạo thuộc tính APhoneNumber để sử dụng cho các thuộc tính là Số điện thoại trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class APhoneNumber : Attribute
        {
            
        }
        /// <summary>
        /// tạo thuộc tính DisplayName để tạo tên hiển thị cho các thuộc tính trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class DisplayName : Attribute
        {
            public string Name { get; set; }
            public DisplayName(string name=null)
            {
                this.Name = name;
            }
        }
        /// <summary>
        /// tạo thuộc tính MaxLength để tạo điều kiện cho chiều dài tối đa của  dữ liệu thuộc tính trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class MaxLength : Attribute
        {
            public int Value { get; set; }
            public string ErrorMsg { get; set; }
            public MaxLength(int value,string errorMsg)
            {
                this.Value = value;
                this.ErrorMsg = errorMsg;
            }
        }
        /// <summary>
        /// tạo thuộc tính RangeLength để tạo điều kiện cho phạm vi chiều dài của  dữ liệu thuộc tính trong một đối tượng
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class RangeLength : Attribute
        {
            public int MaxLength { get; set; }
            public int MinLength { get; set; }
            public string ErrorMsg { get; set; }
            public RangeLength(int minLength, int maxLength, string errorMsg)
            {
                this.MinLength = minLength;
                this.MaxLength = maxLength;
                this.ErrorMsg = errorMsg;
            }
        }


        /// <summary>
        /// Ngày tạo
        /// </summary>
        public EntityState? EntityState { get; set; } = Enum.EntityState.AddNew;

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
