using Microsoft.AspNetCore.Mvc;
using TP4P1.Models.EntityFramework;

namespace TP4P1.Models.Repository
{
    
 public interface IDataRepository<TEntity>
    {
        ActionResult<IEnumerable<TEntity>> GetAll();
        ActionResult<TEntity> GetById(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(Utilisateur utilisateur);
    }
}
