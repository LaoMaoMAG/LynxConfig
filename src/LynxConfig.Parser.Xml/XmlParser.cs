using LynxConfig.Core.Interfaces;
using System.Xml.Serialization;


namespace LynxConfig.Parser.Xml;


public class XmlParser : IConfigParser
{
    public static IConfigParser Instance { get; } = new XmlParser();
    
    public T Deserialization<T>(string str) where T : class, new()
    {
        using var sr = new StringReader(str);
        var serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(sr)!;
    }
    
    public string Serialization<T>(T obj, HashSet<string> hiddenMemberList) where T : class, new()
    {
        using var stringWriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(stringWriter, obj);
        return stringWriter.ToString();
    }
}