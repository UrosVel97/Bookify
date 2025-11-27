using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Abstractions;

public interface IUnitOfWork
{
    //Persists all pending changes to the repository
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
}
