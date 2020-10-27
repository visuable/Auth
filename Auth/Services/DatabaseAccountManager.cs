using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class DatabaseAccountManager : IAccountManager
    {
        private AppContext context;
        public DatabaseAccountManager(AppContext context)
        {
            this.context = context;
        }
        public string LoginUser(ViewUser viewUserModel)
        {
            var dbUser = context.Users.Find(viewUserModel.Login);
            if (dbUser != null &&
                dbUser.Password == Encrypts.EncryptPassword(viewUserModel.Password, dbUser.Salt))
            {
                string encoded = Encrypts.GenerateToken();
                return encoded;
            }
            return string.Empty;
        }

        public void RegisterUser(ViewUser viewUserModel)
        {
            var contextUser = new User();
            if (context.Users.Find(viewUserModel.Login) == null)
            {
                contextUser.Role = Settings.Roles.User;
                contextUser.Login = viewUserModel.Login;
                contextUser.Salt = Encrypts.GenerateSalt();
                contextUser.Password = Encrypts.EncryptPassword(viewUserModel.Password, contextUser.Salt);
                context.Users.Add(contextUser);
            }
            context.SaveChanges();
        }
    }
}
