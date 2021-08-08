using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Service
{
    public interface IBaseService<MISAEntity>
    {
        ServiceResult GetAll();
        ServiceResult GetById(Guid entityId);
        ServiceResult Add(MISAEntity entity);
        ServiceResult Update(MISAEntity entity, Guid entityId);
        ServiceResult Delete(Guid entityId);
        ServiceResult GetNewCode();
        
    }
}
