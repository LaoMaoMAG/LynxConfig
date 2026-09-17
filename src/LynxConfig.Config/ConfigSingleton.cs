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
    public sealed override string ConfigFilePath { get; init; }

    protected sealed override T ConfigData { get; }
    
    protected sealed override IConfigParser Parser { get; }
    
    public static T Instance { get; } = new();
    
    protected ConfigSingleton(string filePath, IConfigParser parser)
    {
        // 验证泛型类型
        Utilities.GenericsValidation<T>(this);
        
        ConfigData = (this as T)!;
        ConfigFilePath = filePath;
        Parser = parser;
        Init();
    }
}