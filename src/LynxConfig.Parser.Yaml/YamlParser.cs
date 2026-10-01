using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Yaml;

/// <summary>
/// Yaml 解析器
/// </summary>
public class YamlParser : IConfigParser
{
    public static IConfigParser Instance { get; } = new YamlParser();
    
    public T Deserialization<T>(string str) where T : class, new()
    {
        throw new NotImplementedException();
    }
    
    public string Serialization<T>(T obj, HashSet<string> hiddenMemberList) where T : class, new()
    {
        throw new NotImplementedException();
    }
}