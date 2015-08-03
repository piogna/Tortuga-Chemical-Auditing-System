using System;
using System.Collections.Generic;

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
