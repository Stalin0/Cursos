namespace BlockchainService.Providers;

public interface IBlockHashProvider
{
    string ComputeHash(string value);
}
