
using Microsoft.Extensions.Options;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Infraestructure.Options;
using System.Security.Cryptography;

namespace SocialMedia.Infraestructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOption _option;
        public PasswordService(IOptions<PasswordOption> option) { 
        
            _option = option.Value;
        
        }
        public bool Check(string hash, string password)
        {
            var parts=hash.Split('.',3);

            if (parts.Length !=  3)
            {
                throw new FormatException("Unexpected hash format");
            }

            var iterations=Convert.ToInt32( parts[0]);
            var salt=Convert.FromBase64String( parts[1]);
            var key = Convert.FromBase64String( parts[3]);
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations

               ))
            {
                var keyToCheck = algorithm.GetBytes(_option.KeysSize);
                return keyToCheck.SequenceEqual(key);
          
            };
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                 password,
                _option.SaltSize,
                _option.Iterations

                ))
            {
                var key = Convert.ToBase64String( algorithm.GetBytes(_option.KeysSize));
                var salt=Convert.ToBase64String(algorithm.Salt);
                return $"{_option.Iterations}.{salt}.{key}";
            };


            
        }
    }
}
