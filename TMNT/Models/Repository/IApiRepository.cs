using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMNT.Models.Repository
{
    public interface IApiRepository<T> : IDisposable
    {
        IEnumerable<T> Get();
        T Get(string i);
        void Create(T t);
        void Update(T t);
        void Delete(int? i);
        void Dispose();
    }
}
