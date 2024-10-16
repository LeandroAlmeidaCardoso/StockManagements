namespace StockManagement.Common.Redis.Interfaces;

public interface IRedisRepositoryCommand
{
    Task<bool> Existe(string chave);

    Task<bool> ExisteHash(string chave, string atributo);

    Task<string?> Buscar(string chave);

    Task<string> BuscarHash(string chave, string atributo);

    Task<ICollection<string>> BuscarHashEmLote(string chave, ICollection<string> atributos);

    Task<bool> Adicionar(string chave, string valor);

    Task<bool> Adicionar(string chave, object valor);

    Task<bool> AdicionarHash(string chave, string atributo, string valor);

    Task<bool> AdicionarEmLote(ICollection<KeyValuePair<string, string>> atributosValores);

    Task<bool> AdicionarEmLote(ICollection<KeyValuePair<string, object>> atributosValores);

    Task<bool> Atualizar(string chave, string valor);

    Task<bool> Atualizar(string chave, object valor);

    Task<bool> AtualizarHash(string chave, string atributo, string valor);

    Task<bool> AtualizarEmLote(ICollection<KeyValuePair<string, string>> atributosValores);

    Task<bool> AtualizarEmLote(ICollection<KeyValuePair<string, object>> atributosValores);

    Task AdicionarOuAtualizarHashEmLote(string chave,
        ICollection<KeyValuePair<string, string>> atributosValores);

    Task AdicionarOuAtualizarHashEmLote(string chave, object valor);

    Task<bool> Remover(string chave);

    Task<bool> RemoverHash(string chave, string atributo);

    Task<long> RemoverEmLote(ICollection<string> chaves);

    Task RemoverHashEmLote(string chave, ICollection<string> atributos);
}