using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MISA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infarstructure
{
    public class PositionRepository:BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #region Methods

        /// <summary>
        /// Hàm kiểm tra mã phòng bahn đã tồn tại hay chưa
        /// </summary>
        /// <param name="positionCode">Mã phòng ban</param>
        /// <returns>true- đã tồn tại; false- chưa tồn tại</returns>
        /// CreatedBy: TTLONG (26/7/2021)
        public bool CheckDuplicatePositionCode(string positionCode)
        {
            var isDuplicate = false;

            var sqlCommand = $"SELECT PositionCode FROM Positions where PositionCode = @PositionCode";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@PositionCode", positionCode);

            //Lấy dữ liệu với Dapper:
            var positions = DbConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: paramaters);
            if (positions != null)
                isDuplicate = true;

            return isDuplicate;
        }
        #endregion
    }
}
