using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Contract.BaseRepository
{
    public interface IWriteRepository<TEntity> :
            IBaseRepository<TEntity>,
            IAddService<TEntity>, 
            IUpdateService<TEntity>,
            IDeleteService<TEntity> where TEntity : class
    {
    }
}
