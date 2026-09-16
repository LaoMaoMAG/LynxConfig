using LynxConfig.Config.Enums;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Config;

/// <summary>
/// 配置绑定类
/// 绑定模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class ConfigBinding<T> : ConfigAbstract<T> where T : class, new()
{
    public sealed override string FilePath { get; init; }

    protected sealed override T ConfigData { get; }

    protected sealed override IConfigParser Parser { get; }
    
    public ConfigBinding(T data, string file, IConfigParser parser)
    {
        ConfigData = data;
        FilePath = file;
        Parser = parser;
        Init();
    }
}