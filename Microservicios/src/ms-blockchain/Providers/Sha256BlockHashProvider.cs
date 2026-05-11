using System.Security.Cryptography;
using System.Text;

namespace BlockchainService.Providers;

public sealed class Sha256BlockHashProvider : IBlockHashProvider
{
    public string ComputeHash(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToHexString(hashBytes);
    }
}
