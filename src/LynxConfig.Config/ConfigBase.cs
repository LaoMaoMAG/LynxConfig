using LynxConfig.Config.Enums;
using LynxConfig.Core;

namespace LynxConfig.Config;

/// <summary>
/// 配置基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class ConfigBase<T> : ConfigAbstract<T> where T : class, new()
{
    public sealed override string FilePath { get; init; } = null!;

    protected sealed override T ConfigData { get; }
    
    protected ConfigBase(string filePath, EnumConfigFileType configFileType = EnumConfigFileType.Default)
    {
        ConfigData = (this as T)!;
        FilePath = filePath;
        ConfigFileType = configFileType;
        Init();
    }
    
    public ConfigBase(EnumConfigFileType configFileType = EnumConfigFileType.Default)
    {
        ConfigData = (this as T)!;
        ConfigFileType = configFileType;
    }
    
    public ConfigBase()
    {
        ConfigData = (this as T)!;
    }
}