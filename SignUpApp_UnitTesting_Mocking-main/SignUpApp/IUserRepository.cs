using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpApp
{
    public interface IUserRepository
    {
        Task<bool> Insert(User user);
    }
}
