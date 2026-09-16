using System.Text.Json;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Json;

/// <summary>
/// Json 解析器
/// </summary>
/// <typeparam name="T"></typeparam>
public class JsonParser<T> : IConfigParser<T> where T : class, new()
{
    public string Serialization(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }
    
    public T Deserialization(string str)
    {
        return JsonSerializer.Deserialize<T>(str)!;
    }
}