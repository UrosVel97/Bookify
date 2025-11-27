using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Users
{
    public interface IUserReporsitory
    {
        //Gets a user by ID
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);

        //Adds a new user to the repository
        void Add(User user);
    }
}
