using Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Catalog.Login;

namespace Services.Catalog.Users
{
    public class UserService : IUserService
    {
        private readonly ShopDbContext _context;
        public UserService(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Login(LoginRequest loginRequest)
        {
            string password = GetMD5(loginRequest.Password);

            var checkLogin = await  _context.User.FirstOrDefaultAsync(x => x.Username == loginRequest.Username && x.Password == password);
            if(checkLogin == null)
            { return false; }

            return true;

        }
         
        private  string GetMD5(string str)
        {
            string str_md5_out = string.Empty;
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes_md5_in = Encoding.UTF8.GetBytes(str);
                byte[] bytes_md5_out = md5.ComputeHash(bytes_md5_in);

                str_md5_out = BitConverter.ToString(bytes_md5_out);
                str_md5_out = str_md5_out.Replace("-", "");
            }
            return str_md5_out;
        }
    }
}
