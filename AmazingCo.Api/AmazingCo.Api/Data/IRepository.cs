﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCo.Api.Data
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
    IQueryable<TEntity> GetAll();

    Task<TEntity> GetById(int id);

    Task Create(TEntity entity);

    Task Update(int id, TEntity entity);

    Task Delete(int id);
    }
}
