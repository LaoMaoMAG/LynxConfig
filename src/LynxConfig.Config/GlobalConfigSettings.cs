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
    public static HashSet<string> HiddenMemberList { get; }

    static GlobalConfigSettings()
    {
        // 添加隐藏成员
        var dataList = new List<string>();
        dataList.AddRange(Utilities.GetPublicMemberNames<ConfigBase<object>>());
        dataList.AddRange(Utilities.GetPublicMemberNames<ConfigSingleton<object>>());
        HiddenMemberList = new HashSet<string>(dataList.Distinct());
    }
}