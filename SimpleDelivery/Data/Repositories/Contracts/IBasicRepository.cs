using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Contracts
{
    public interface IBasicRepository<T>
    {
        Task<T> GetByKey(params object[] id);
        Task<IList<T>> GetAll();
        Task DeleteById(params object[] id);
        Task Update(T entity);
        Task Add(T entity);
    }
}
