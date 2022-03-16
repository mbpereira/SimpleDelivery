using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Contracts
{
    public interface IBasicRepository<T>
    {
        Task<T> GetByKey(params object[] key);
        Task<IList<T>> GetAll();
        Task DeleteByKey(params object[] key);
        Task Update(T entity);
        Task Add(T entity);
    }
}
