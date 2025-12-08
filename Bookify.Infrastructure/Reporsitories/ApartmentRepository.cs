using Bookify.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Reporsitories;

internal sealed class ApartmentRepository : Reporsitory<Apartment>, IApartmentReporsitory
{
    public ApartmentRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

}
