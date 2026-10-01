using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Xml;


public class XmlParser : IConfigParser
{
    public static IConfigParser Instance { get; } = new XmlParser();
    
    public T Deserialization<T>(string str) where T : class, new()
    {
        throw new NotImplementedException();
    }
    
    public string Serialization<T>(T obj, HashSet<string> hiddenMemberList) where T : class, new()
    {
        throw new NotImplementedException();
    }
}