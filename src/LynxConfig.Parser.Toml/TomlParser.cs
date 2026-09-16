using Tomlyn;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Toml;

/// <summary>
/// Toml 解析器
/// </summary>
public class TomlParser<T> : IConfigParser<T> where T : class, new()
{
    public string Serialization(T obj)
    {
        return TomlSerializer.Serialize(obj);
    }
    
    public void Deserialization(string str, out T obj)
    {
        obj = TomlSerializer.Deserialize<T>(str) ?? throw new TomlException("TOML 反序列化结果为空！");
    }
}