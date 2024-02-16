﻿using Microsoft.AspNetCore.Mvc;
using TP4P1.Models.EntityFramework;

namespace TP4P1.Models.Repository
{
    
 public interface IDataRepository<TEntity>
    {
        Task <ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task <ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity utilisateur);
    }
}
