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

    protected sealed override T ConfigData { get; }

    protected sealed override IConfigParser Parser { get; }

    protected ConfigBase(string filePath, IConfigParser parser)
    {
        // 验证泛型类型
        Utilities.GenericsValidation<T>(this);
        
        ConfigData = (this as T)!;
        FilePath = filePath;
        Parser = parser;
        Init();
    }

    public ConfigBase(EnumConfigFileType configFileType = EnumConfigFileType.Default)
    {
        // 验证泛型类型
        Utilities.GenericsValidation<T>(this);
        
        ConfigData = (this as T)!;
    }

    public ConfigBase()
    {
        // 验证泛型类型
        Utilities.GenericsValidation<T>(this);
        ConfigData = (this as T)!;
    }
}