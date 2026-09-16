using LynxConfig.Config.Enums;
using LynxConfig.Core;

namespace LynxConfig.Config;

/// <summary>
/// 配置单例类
/// </summary>
public class ConfigSingleton<T> : ConfigAbstract<T> where T : class, new()
{
    public sealed override string FilePath { get; init; }
    
    protected sealed override T ConfigData { get; }
    
    public static T Instance { get; } = new();
    
    protected ConfigSingleton(string filePath, EnumConfigFileType configFileType = EnumConfigFileType.Default)
    {
        ConfigData = (this as T)!;
        FilePath = filePath;
        ConfigFileType = configFileType;
        Init();
    }
}