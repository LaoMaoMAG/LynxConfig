using LynxConfig.Config.Enums;

namespace LynxConfig.Config;

/// <summary>
/// 配置类
/// </summary>
public static class GlobalConfigSettings
{
    /// <summary>
    /// 默认配置文件类型
    /// </summary>
    public static EnumConfigFileType DefaultConfigFileType { get; set; } = EnumConfigFileType.Json;
}