﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public static class Settings
    {
        public const string Issuer = "Server";
        public const string Audience = "Client";

        public const string SecretKey = "qwerty1234567890!super_secret_key";

        public const int Lifetime = 10;
        public static SymmetricSecurityKey GetSymmetricKey()
        {
            return new SymmetricSecurityKey(Encoding.Unicode.GetBytes(SecretKey));
        }
    }
}
