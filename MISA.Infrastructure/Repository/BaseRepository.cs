
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Enum;
using MISA.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MISA.Infrastructure
{
    public class BaseRepository<MISAEntity>:IBaseRepository<MISAEntity>,IDisposable where MISAEntity: BaseEntity // điều kiện thằng con kế thừa phải là class
    {

        #region Fields
        IConfiguration _configuration;
        string _connectionString=string.Empty;
        string _className;
        //Khởi tạo kết nối
        protected IDbConnection DbConnection;
        #endregion
        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            //Khai báo thông tin kết nối:
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            DbConnection = new MySqlConnection(_connectionString);
            _className = typeof(MISAEntity).Name;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Lấy toàn bộ dữ liệu:
        /// </summary>
        /// <returns>
        /// Danh sách Object
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        public IEnumerable<MISAEntity> GetAll()
        {
            try
            {
                
                //Khởi tạo các CommandText:
                var entitites = DbConnection.Query<MISAEntity>($"Proc_Get{_className}s",null,null,true,null,CommandType.StoredProcedure);
                //Trả về dữ liệu:
                return entitites;

            }
            catch (Exception e)
            {
                var t = e.Message;

                return null;
            }


        }


        /// <summary>
        /// Lấy dữ liệu theo mã
        /// </summary>
        /// <param name="entityId"> Object Id</param>
        /// <returns>
        /// Dữ liệu được lấy theo Id
        /// </returns>
        /// /// CreatedBy: TTLONG (28/7/2021)
        public MISAEntity GetById(Guid entityId)
        {      
            
            
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_className}Id", entityId);
            //Khởi tạo các CommandText:
            var entity = DbConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{_className}ById", parameters,null,null,CommandType.StoredProcedure);
            //Trả về dữ liệu:

            return entity;

        }
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity">object</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy: TTLONG(28/7/2021)
        public int Add(MISAEntity entity)
        {
            var rowAffects=0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Xử lý các dữ liệu:
                    var parameters = MappingDbType(entity);
                    //THực thi commandText:

                    rowAffects = DbConnection.Execute($"Proc_Insert{_className}", parameters, transaction: transaction, null, CommandType.StoredProcedure);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                    DbConnection.Close();
                }

            }
                
            //Trả vệ số bản ghi được thêm
            return rowAffects;
        }
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">object</param>
        /// <param name="entityId">Id của Object</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy: TTLONG(28/7/2021)
        public int Update(MISAEntity entity, Guid entityId)
        {

            var rowAffects = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Xử lý các dữ liệu:
                    var parameters = MappingDbType(entity);
                    //THực thi commandText:

                    rowAffects = DbConnection.Execute($"Proc_Update{_className}", parameters, transaction: transaction, null, CommandType.StoredProcedure);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
               /* finally
                {
                    transaction.Dispose();
                    DbConnection.Close();
                }*/

            }

           
            //Trả vệ số bản ghi được thêm
            return rowAffects;
        }
        /// <summary>
        /// Xóa dữ liệu theo khóa chính
        /// </summary>
        /// <param name="entityId">Id của Object</param>
        /// <returns>
        /// Số bản ghi bị ảnh hưởng
        /// </returns>
        /// CreatedBy: TTLONG(28/7/2021)
        public int Delete(Guid entityId)
        {
            var rowAffects = 0;
            DbConnection.Open();
            using (var transaction = DbConnection.BeginTransaction())
            {
                try
                {
                    //Xử lý các dữ liệu:
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add($"@{_className}Id", entityId);
                    //THực thi commandText:

                    rowAffects = DbConnection.Execute($"Proc_Delete{_className}ById", parameters, transaction: transaction, null, CommandType.StoredProcedure);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                /*finally
                {
                    transaction.Dispose();
                    DbConnection.Close();
                }*/

            }
            
           
            //Trả về dữ liệu:

            return rowAffects;
        }
        /// <summary>
        /// Lấy mã mới
        /// </summary> 
        /// <returns>
        /// Mã mới
        /// </returns>
        /// CreatedBy: TTLONG(29/7/2021)
        public string GetNewCode()
        {

            //Khởi tạo các CommandText:
            var entityCode = DbConnection.QueryFirstOrDefault<string>($"Proc_GetNew{_className}Code", null, null, null, CommandType.StoredProcedure);
            //Trả về dữ liệu:

            return entityCode;
        }
        /// <summary>
        /// Xử lý dữ liệu
        /// </summary>
        /// <typeparam name="MISAEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: TTLONG(02/08/2021)
        private DynamicParameters MappingDbType(MISAEntity entity)
        {

            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }
        /// <summary>
        /// Lấy dữ liệu theo thuộc tính bất kì
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="property">Thuộc tính của đối tượng</param>
        /// <returns>Danh sách thông tin</returns>
        /// CreatedBy: TTLONG(02/08/2021)

        public MISAEntity GetEntityByProperty(MISAEntity entity, PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            /*var keyName = GetKeyProperty().Name;
            var keyValue = GetKeyProperty().GetValue(entity);*/
            var keyName = entity.GetType().GetProperty($"{_className}Id").GetValue(entity);

            string query = string.Empty;

            if (entity.EntityState == EntityState.AddNew)
                query = $"SELECT * FROM {_className} WHERE {propertyName} = '{propertyValue}'";
            else if (entity.EntityState == EntityState.Update)
                query = $"SELECT * FROM {_className} WHERE {propertyName} = '{propertyValue}' AND {_className}Id <> '{keyName}'";
            else
                return null;

            var entityReturn = DbConnection.Query<MISAEntity>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
        /// <summary>
        /// Lấy thông tin entity theo thuộc tính
        /// </summary>
        /// <param name="propertyName">Tên thuộc tính</param>
        /// <param name="propertyValue">giá trị thuộc tính</param>
        /// <returns> Danh sách thông tin</returns>
        /// CreatedBy: TTLONG (02/08/2021)
        public MISAEntity GetEntityByProperty(string propertyName, object propertyValue)
        {
            string query = $"SELECT * FROM {_className} WHERE {propertyName} = '{propertyValue}'";
            var entityReturn = DbConnection.Query<MISAEntity>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
        /// <summary>
        /// Đóng kết nối DB (luôn thực thi khi object không sử dụng nữa)
        /// </summary>
        public void Dispose()
        {
            if (DbConnection.State == ConnectionState.Open)
            {
                DbConnection.Close();
            }
        }
    }

        
        #endregion
    
}
