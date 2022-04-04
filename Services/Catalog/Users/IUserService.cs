using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Catalog.Login;

namespace Services.Catalog.Users
{
    public interface IUserService
    {
        public Task<bool> Login(LoginRequest loginRequest);
    }
}
