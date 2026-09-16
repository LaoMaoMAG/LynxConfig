using Tomlyn;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Toml;

/// <summary>
/// Toml 解析器
/// </summary>
public class TomlParser : IConfigParser
{
    public static IConfigParser Instance { get; } = new TomlParser();
    
    public string Serialization<T>(T obj, HashSet<string> hiddenMemberList) where T : class, new()
    {
        return TomlSerializer.Serialize(obj);
    }
    
    public T Deserialization<T>(string str) where T : class, new()
    {
        return TomlSerializer.Deserialize<T>(str) ?? throw new TomlException("TOML 反序列化结果为空！");
    }
}