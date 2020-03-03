using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapFramework
{
    public interface IRepositoryBaseNoObject<TEntity> where TEntity : class
    {
       
        string QuerySelectEntity(TEntity obj);
        void Add(TEntity obj);
        void Dispose();
        TEntity GetByEntity(TEntity obj);
        List<TEntity> GetAll();
        void Update(TEntity obj);
        void Delete(TEntity obj);

    }
}
