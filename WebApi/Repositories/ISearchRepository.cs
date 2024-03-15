using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public interface ISearchRepository<T>
    {
        Task<IEnumerable<T>> Search(string query);

        Task Add(IEnumerable<T> items);
    }
}
