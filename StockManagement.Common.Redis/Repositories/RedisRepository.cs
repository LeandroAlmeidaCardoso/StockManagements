#region

using System.Text.Json;
using System.Text.Json.Serialization;
using StackExchange.Redis;
using StockManagement.Common.Redis.Interfaces;

#endregion

namespace StockManagement.Common.Redis.Repositories;

public class RedisRepository : IRedisRepository
{
    private readonly IDatabase _bancoDeDados;
    private ITransaction? _transacao;

    public RedisRepository(IConnectionMultiplexer conexao)
    {
        _bancoDeDados = conexao.GetDatabase();
    }

    public void CriarTransacao()
    {
        _transacao = _bancoDeDados.CreateTransaction();
    }

    public async Task<bool> ExecutarTransacao()
    {
        if (_transacao is null)
            return false;

        return await _transacao.ExecuteAsync();
    }

    public async Task<bool> Existe(string chave)
    {
        return await _bancoDeDados.KeyExistsAsync(chave);
    }

    public async Task<bool> ExisteHash(string chave, string atributo)
    {
        return await _bancoDeDados.HashExistsAsync(chave, atributo);
    }

    public async Task<string?> Buscar(string chave)
    {
        return await _bancoDeDados.StringGetAsync(chave);
    }

    public async Task<string> BuscarHash(string chave, string atributo)
    {
        var redisValue = await _bancoDeDados.HashGetAsync(chave, atributo);

        return redisValue.ToString();
    }

    public async Task<ICollection<string>> BuscarHashEmLote(string chave, ICollection<string> atributos)
    {
        var atributosArray = atributos.Select(atributo => new RedisValue(atributo)).ToArray();

        var redisValues = await _bancoDeDados.HashGetAsync(chave, atributosArray);

        return redisValues.Select(value => value.ToString()).ToList();
    }

    public async Task<bool> Adicionar(string chave, string valor)
    {
        if (_transacao is null)
            return await _bancoDeDados.StringSetAsync(chave, valor, null, When.NotExists,
                CommandFlags.None);

        _transacao?.StringSetAsync(chave, valor, null, When.NotExists, CommandFlags.None);

        return true;
    }

    public async Task<bool> Adicionar(string chave, object valor)
    {
        return await Adicionar(chave,
            JsonSerializer.Serialize(valor,
                new JsonSerializerOptions {ReferenceHandler = ReferenceHandler.IgnoreCycles}));
    }

    public async Task<bool> AdicionarHash(string chave, string atributo, string valor)
    {
        if (_transacao is null)
            return await _bancoDeDados.HashSetAsync(chave, atributo, valor, When.NotExists);

        _transacao?.HashSetAsync(chave, atributo, valor, When.NotExists);

        return true;
    }

    public async Task<bool> AdicionarEmLote(ICollection<KeyValuePair<string, string>> atributosValores)
    {
        var atributosValoresArray = atributosValores.Select(atributoValor =>
                new KeyValuePair<RedisKey, RedisValue>(new RedisKey(atributoValor.Key),
                    new RedisValue(atributoValor.Value)))
            .ToArray();

        if (_transacao is null)
            return await _bancoDeDados.StringSetAsync(atributosValoresArray, When.NotExists);

        _transacao?.StringSetAsync(atributosValoresArray, When.NotExists);

        return true;
    }

    public async Task<bool> AdicionarEmLote(ICollection<KeyValuePair<string, object>> atributosValores)
    {
        var atributosValoresArray = atributosValores.Select(atributoValor =>
            new KeyValuePair<string, string>(atributoValor.Key,
                JsonSerializer.Serialize(atributoValor.Value,
                    new JsonSerializerOptions {ReferenceHandler = ReferenceHandler.IgnoreCycles}))).ToList();

        return await AdicionarEmLote(atributosValoresArray);
    }

    public async Task<bool> Atualizar(string chave, string valor)
    {
        if (_transacao is null)
            return await _bancoDeDados.StringSetAsync(chave, valor, null, When.Exists,
                CommandFlags.None);

        _transacao?.StringSetAsync(chave, valor, null, When.Exists, CommandFlags.None);

        return true;
    }

    public async Task<bool> Atualizar(string chave, object valor)
    {
        return await Atualizar(chave,
            JsonSerializer.Serialize(valor,
                new JsonSerializerOptions {ReferenceHandler = ReferenceHandler.IgnoreCycles}));
    }

    public async Task<bool> AtualizarHash(string chave, string atributo, string valor)
    {
        if (_transacao is null)
            return await _bancoDeDados.HashSetAsync(chave, atributo, valor, When.Exists);

        _transacao?.HashSetAsync(chave, atributo, valor, When.Exists);

        return true;
    }

    public async Task<bool> AtualizarEmLote(ICollection<KeyValuePair<string, string>> atributosValores)
    {
        var atributosValoresArray = atributosValores.Select(atributoValor =>
                new KeyValuePair<RedisKey, RedisValue>(new RedisKey(atributoValor.Key),
                    new RedisValue(atributoValor.Value)))
            .ToArray();

        if (_transacao is null)
            return await _bancoDeDados.StringSetAsync(atributosValoresArray, When.Exists);

        _transacao?.StringSetAsync(atributosValoresArray, When.Exists);

        return true;
    }

    public async Task<bool> AtualizarEmLote(ICollection<KeyValuePair<string, object>> atributosValores)
    {
        var atributosValoresArray = atributosValores.Select(atributoValor =>
                new KeyValuePair<RedisKey, RedisValue>(new RedisKey(atributoValor.Key),
                    new RedisValue(JsonSerializer.Serialize(atributoValor.Value,
                        new JsonSerializerOptions {ReferenceHandler = ReferenceHandler.IgnoreCycles}))))
            .ToArray();

        if (_transacao is null)
            return await _bancoDeDados.StringSetAsync(atributosValoresArray, When.Exists);

        _transacao?.StringSetAsync(atributosValoresArray, When.Exists);

        return true;
    }

    public async Task AdicionarOuAtualizarHashEmLote(string chave,
        ICollection<KeyValuePair<string, string>> atributosValores)
    {
        var atributosValoresArray = atributosValores
            .Select(atributoValor => new HashEntry(atributoValor.Key, atributoValor.Value)).ToArray();

        if (_transacao is null)
        {
            await _bancoDeDados.HashSetAsync(chave, atributosValoresArray);

            return;
        }

        _transacao?.HashSetAsync(chave, atributosValoresArray);
    }

    public async Task AdicionarOuAtualizarHashEmLote(string chave, object valor)
    {
        var atributosValoresArray = valor.GetType().GetProperties()
            .Select(property =>
                new KeyValuePair<string, string>(property.Name, property.GetValue(valor) as string ?? string.Empty))
            .Where(keyValuePair => keyValuePair.Value != string.Empty)
            .ToList();

        await AdicionarOuAtualizarHashEmLote(chave, atributosValoresArray);
    }

    public async Task<bool> Remover(string chave)
    {
        if (_transacao is null)
            return await _bancoDeDados.KeyDeleteAsync(chave);

        _transacao?.KeyDeleteAsync(chave);

        return true;
    }

    public async Task<bool> RemoverHash(string chave, string atributo)
    {
        if (_transacao is null)
            return await _bancoDeDados.HashDeleteAsync(chave, atributo);

        _transacao?.HashDeleteAsync(chave, atributo);

        return true;
    }

    public async Task<long> RemoverEmLote(ICollection<string> chaves)
    {
        var chavesArray = chaves.Select(chave => new RedisKey(chave)).ToArray();

        if (_transacao is null)
            return await _bancoDeDados.KeyDeleteAsync(chavesArray);

        _transacao?.KeyDeleteAsync(chavesArray);

        return 0;
    }

    public async Task RemoverHashEmLote(string chave, ICollection<string> atributos)
    {
        var atributosArray = atributos.Select(atributo => new RedisValue(atributo)).ToArray();

        if (_transacao is null)
        {
            await _bancoDeDados.HashDeleteAsync(chave, atributosArray);

            return;
        }

        _transacao?.HashDeleteAsync(chave, atributosArray);
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _transacao = null;
    }
}