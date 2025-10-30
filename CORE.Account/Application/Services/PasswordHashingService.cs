using System.Security.Cryptography;
using System.Text;

namespace CORE.Account.Application.Services
{
    /// <summary>
    /// Service for securely hashing passwords using SHA256.
    /// Follows Single Responsibility Principle.
    /// </summary>
    public interface IPasswordHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }

    internal class PasswordHashingService : IPasswordHashingService
    {
        /// <summary>
        /// Hashes a password using SHA256.
        /// </summary>
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

        /// <summary>
        /// Verifies if a password matches the hashed password.
        /// </summary>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            var passwordHash = HashPassword(password);
            return passwordHash.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
