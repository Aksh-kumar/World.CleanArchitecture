using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World.Domain.Contract.BaseRepository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        public IUnitOfWork UnitOfWorkPropery { get; }
    }
}
