using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Json;

/// <summary>
/// Json 解析器
/// </summary>
/// <typeparam name="T"></typeparam>
public class JsonParser : IConfigParser
{
    public static IConfigParser Instance { get; } = new JsonParser();

    public string Serialization<T>(T obj, HashSet<string> hiddenMemberList) where T : class, new()
    {
        var typeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                typeInfo =>
                {
                    foreach (var prop in typeInfo.Properties)
                        if (hiddenMemberList.Contains(prop.Name))
                            prop.ShouldSerialize = (_, _) => false;
                }
            }
        };
            
        var options = new JsonSerializerOptions
        {
            WriteIndented = true, // 启用格式化
            TypeInfoResolver = typeInfoResolver
        };
        
        return JsonSerializer.Serialize(obj, options);
    }

    public T Deserialization<T>(string str) where T : class, new()
    {
        return JsonSerializer.Deserialize<T>(str) ?? throw new JsonException();
    }
}