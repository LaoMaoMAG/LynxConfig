using Tomlyn;
using Tomlyn.Model;

namespace LynxConfig.Config.Toml;

/// <summary>
/// TOML 配置抽象基类
/// </summary>
public abstract class TomlConfigAbstract<T> : ConfigAbstract<T> where T : class, new()
{
    protected override string Serialization(T obj)
    {
        return TomlSerializer.Serialize(obj);
    }

    protected override T Deserialization(string str)
    {
        return TomlSerializer.Deserialize<T>(str) ?? throw new TomlException("TOML 反序列化结果为空！");
    }
}