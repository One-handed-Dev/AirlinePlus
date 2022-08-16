using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Common.Application.Contracts;

namespace Common.Application
{
    public sealed class PasswordService : IPasswordService
    {
        #region Init
        private HashingOptions Options { get; }
        private const int KeySize = 32; // 256 bit
        private const int SaltSize = 16; // 128 bit 

        public PasswordService(IOptions<HashingOptions> options) => Options = options.Value;
        #endregion

        public string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Options.Iterations, HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Options.Iterations}.{salt}.{key}";
        }

        public PasswordCheckResponse Check(PasswordCheckRequest command)
        {
            var parts = command.Hash.Split('.', 3);

            if (parts.Length != 3) return default;

            var iterations = Convert.ToInt32(parts[0]);
            var key = Convert.FromBase64String(parts[2]);
            var salt = Convert.FromBase64String(parts[1]);
            var needsUpgrade = iterations != Options.Iterations;

            using var algorithm = new Rfc2898DeriveBytes(command.Password, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = algorithm.GetBytes(KeySize);
            var verified = keyToCheck.SequenceEqual(key);

            return new(verified, needsUpgrade);
        }
    }
}