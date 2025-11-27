using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments;

public interface IApartmentReporsitory
{
    //Gets an apartment by ID
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);
}
