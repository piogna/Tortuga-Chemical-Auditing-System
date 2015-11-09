using System;
using System.Collections.Generic;
using TMNT.Models.Enums;

namespace TMNT.Models.Repository {
    public interface IRepository<T> : IDisposable {

        IEnumerable<T> Get();
        T Get(int? i);
        CheckModelState Create(T t);
        CheckModelState Update(T t);
        CheckModelState Delete(int? i);
        void Dispose();
    }
}