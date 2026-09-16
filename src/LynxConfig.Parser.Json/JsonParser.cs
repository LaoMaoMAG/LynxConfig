using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Parser.Json;

/// <summary>
/// Json 解析器
/// </summary>
/// <typeparam name="T"></typeparam>
public class JsonParser<T> : IConfigParser<T> where T : class, new()
{
    public string Serialization(T obj, HashSet<string> hiddenMemberList)
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

    public T Deserialization(string str)
    {
        return JsonSerializer.Deserialize<T>(str)!;
    }
}