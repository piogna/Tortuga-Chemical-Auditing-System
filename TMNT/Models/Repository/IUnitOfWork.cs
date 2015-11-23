using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMNT.Models.Enums;

namespace TMNT.Models.Repository
{
    interface IUnitOfWork : IDisposable
    {
        CheckModelState Commit();
    }
}
