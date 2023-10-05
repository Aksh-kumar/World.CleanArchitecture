using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace World.Domain.Contract.BaseRepository;

public interface IReadRepository<TEntity> : IBaseRepository<TEntity>, ISelectAllService<TEntity>, ISelectService<TEntity>
                                           
      where TEntity : class
{

}