using LynxConfig.Config.Enums;
using LynxConfig.Core;
using LynxConfig.Core.Interfaces;

namespace LynxConfig.Config;

/// <summary>
/// 配置基类
/// 基础继承模式
/// </summary>
/// <typeparam name="T"></typeparam>
public class ConfigBase<T> : ConfigAbstract<T> where T : class, new()
{
    public sealed override string FilePath { get; init; } = null!;

    public sealed override T ConfigData { get; }
    
    public sealed override IConfigParser<T> Parser { get; }
    
    protected ConfigBase(string filePath, IConfigParser<T> parser)
    {
        ConfigData = (this as T)!;
        FilePath = filePath;
        // ConfigFileType = configFileType;
        Parser = parser;
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