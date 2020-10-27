using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IAccountManager
    {
        void RegisterUser(ViewUser viewUserModel);
        string LoginUser(ViewUser viewUserModel);
    }
}
