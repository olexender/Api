﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCo.Api.Data
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
     IEnumerable<TEntity> GetAll();

    TEntity GetById(int id);

    Task Create(TEntity entity);

    Task Update(int id, TEntity entity);
    }
}
