using LynxConfig.Config.Enums;
using LynxConfig.Core;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Config;

/// <summary>
/// 配置单例类
/// 单例模式
/// </summary>
public class ConfigSingleton<T> : ConfigAbstract<T> where T : class, new()
{
    public sealed override string FilePath { get; init; }
    
    public sealed override T ConfigData { get; }
    
    public sealed override IConfigParser<T> Parser { get; }
    
    public static T Instance { get; } = new();
    
    protected ConfigSingleton(string filePath, IConfigParser<T> parser)
    {
        ConfigData = (this as T)!;
        FilePath = filePath;
        Parser = parser;
        Init();
    }
}