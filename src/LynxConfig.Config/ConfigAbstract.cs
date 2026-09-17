using LynxConfig.Config.Enums;
using LynxConfig.Core.Interfaces;
using LynxConfig.Core;

namespace LynxConfig.Config;

/// <summary>
/// 配置抽象类
/// </summary>
public abstract class ConfigAbstract<T> : IConfigLifecycle where T : class, new()
{
    /// <summary>
    /// 配置文件路径
    /// </summary>
    public abstract string ConfigFilePath { get; init; }

    /// <summary>
    /// 配置数据
    /// </summary>
    protected abstract T ConfigData { get; }

    /// <summary>
    /// 配置文件解析器
    /// </summary>
    protected abstract IConfigParser Parser { get; }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        InitFile();
    }
    
    /// <summary>
    /// 初始化文件
    /// </summary>
    private void InitFile()
    {
        if (string.IsNullOrWhiteSpace(ConfigFilePath)) throw new InvalidOperationException("ConfigFilePath 未设置");
        if (File.Exists(ConfigFilePath)) return;
        File.Create(ConfigFilePath).Dispose();
        Save();
    }
    
    /// <summary>
    /// 加载配置
    /// </summary>
    public void Load()
    {
        var str = File.ReadAllText(ConfigFilePath);
        var loadedData = Parser!.Deserialization<T>(str);
        Utilities.CopyProperties(loadedData, ConfigData, GlobalConfigSettings.HiddenMemberList);
    }

    /// <summary>
    /// 保存配置
    /// </summary>
    public void Save()
    {
        var str = Parser!.Serialization(ConfigData, GlobalConfigSettings.HiddenMemberList);
        File.WriteAllText(ConfigFilePath, str);
    }

    public bool TryLoad() => TryLoad(out _);

    public bool TryLoad(out Exception? error)
    {
        try
        {
            Load();
            error = null;
            return true;
        }
        catch (Exception ex)
        {
            error = ex;
            return false;
        }
    }

    public bool TrySave() => TrySave(out _);

    public bool TrySave(out Exception? error)
    {
        try
        {
            Save();
            error = null;
            return true;
        }
        catch (Exception ex)
        {
            error = ex;
            return false;
        }
    }
}