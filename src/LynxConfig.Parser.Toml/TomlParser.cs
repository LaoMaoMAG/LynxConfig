using Tomlyn;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Toml;

/// <summary>
/// Toml 解析器
/// </summary>
public class TomlParser<T> : IConfigParser<T> where T : class, new()
{
    public string Serialization(T obj, HashSet<string> hiddenMemberList)
    {
        return TomlSerializer.Serialize(obj);
    }
    
    public T Deserialization(string str)
    {
        return TomlSerializer.Deserialize<T>(str) ?? throw new TomlException("TOML 反序列化结果为空！");
    }
}