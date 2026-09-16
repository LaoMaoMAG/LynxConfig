using LynxConfig.Config.Enums;
using LynxConfig.Core;

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

    /// <summary>
    /// 隐藏成员列表
    /// </summary>
    public static List<string> HiddenMemberList { get; } = [];

    static GlobalConfigSettings()
    {
        // 添加隐藏成员
        HiddenMemberList.AddRange(Utilities.GetPublicMemberNames<ConfigBase<object>>());
        HiddenMemberList.AddRange(Utilities.GetPublicMemberNames<ConfigSingleton<object>>());
        HiddenMemberList = [.. HiddenMemberList.Distinct()];
    }
}