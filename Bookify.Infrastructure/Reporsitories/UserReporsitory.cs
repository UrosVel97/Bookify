using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Reporsitories;

internal sealed class UserReporsitory : Reporsitory<User>, IUserReporsitory
{
    public UserReporsitory(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
