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
    
    public sealed override T ConfigData { get; }
    
    public sealed override IConfigParser<T> Parser { get; }
    
    public ConfigBinding(T data, string file, IConfigParser<T> parser)
    {
        ConfigData = data;
        FilePath = file;
        // ConfigFileType = configFileType;
        Parser = parser;
        Init();
    }
}