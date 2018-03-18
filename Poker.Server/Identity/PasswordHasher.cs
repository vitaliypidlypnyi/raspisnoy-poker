using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity;
using AspNetPaswordHasher = Microsoft.AspNet.Identity.PasswordHasher;

namespace Poker.Server.Identity
{
    internal class PasswordHasher : AspNetPaswordHasher
    {
        public override string HashPassword(string password)
        {
            var hash = string.Empty;
            var sha256 = new SHA256Managed();
            var hashBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(password));
            foreach (var @byte in hashBytes)
            {
                hash += @byte.ToString("x2");
            }
            return hash;
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (HashPassword(providedPassword) == hashedPassword)
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}