using System;
using System.Collections.Generic;

namespace TMNT.Models.Repository {
    public interface IRepository<T> : IDisposable {

        IEnumerable<T> Get();
        T Get(int? i);
        void Create(T t);
        void Update(T t);
        void Delete(int? i);
        void Dispose();
    }
}