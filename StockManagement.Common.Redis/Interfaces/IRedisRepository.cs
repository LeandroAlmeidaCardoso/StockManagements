namespace StockManagement.Common.Redis.Interfaces;

public interface IRedisRepository : IDisposable, IRedisRepositoryTransaction, IRedisRepositoryCommand
{
}