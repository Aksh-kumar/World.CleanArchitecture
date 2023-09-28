using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace World.Domain.Contract.BaseRepository;

public interface IBaseRepository<TEntity> : ISelectAllService<TEntity>, ISelectService<TEntity>,
                                            IAddService<TEntity>, IUpdateService<TEntity>,
                                            IDeleteService<TEntity>
      where TEntity : class
{

}