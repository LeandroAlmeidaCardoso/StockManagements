namespace StockManagement.Common.Redis.Interfaces;

public interface IRedisRepositoryTransaction
{
    void CriarTransacao();

    Task<bool> ExecutarTransacao();
}