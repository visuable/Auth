using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public static class Encrypts
    {
        private const string Salt = "NZsP6NnmfBuYeJrrAKNuVQ==";
        public static string GenerateToken()
        {
            var token = new JwtSecurityToken(
                            issuer: Settings.Issuer,
                            audience: Settings.Audience,
                            notBefore: DateTime.Now,
                            expires: DateTime.Now.Add(TimeSpan.FromMinutes(Settings.Lifetime)),
                            signingCredentials: new SigningCredentials(Settings.GetSymmetricKey(), SecurityAlgorithms.HmacSha256)
                            );
            var encoded = new JwtSecurityTokenHandler().WriteToken(token);
            return encoded;
        }
        public static string EncryptPassword(string pass, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        public static string GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
