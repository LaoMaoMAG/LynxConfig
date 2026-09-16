using LynxConfig.Config.Enums;
using LynxConfig.Core.Interfaces;
using LynxConfig.Parser.Json;
using LynxConfig.Parser.Toml;
using LynxConfig.Core;



namespace LynxConfig.Config;

public abstract class ConfigAbstract<T> : IConfigLifecycle where T : class, new()
{
    /// <summary>
    /// 配置文件路径
    /// </summary>
    public abstract string FilePath { get; init; }

    /// <summary>
    /// 配置文件类型
    /// </summary>
    protected EnumConfigFileType ConfigFileType
    {
        get => _configFileType == EnumConfigFileType.Default
            ? GlobalConfigSettings.DefaultConfigFileType
            : _configFileType;
        init => _configFileType = value;
    }
    private readonly EnumConfigFileType _configFileType = EnumConfigFileType.Default;

    /// <summary>
    /// 配置数据
    /// </summary>
    public abstract T ConfigData { get; }

    private IConfigParser<T>? _parser;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        IntiParser();
        InitFile();
    }
    
    /// <summary>
    /// 初始化解析器
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void IntiParser()
    {
        if (_parser != null) return;
        _parser = ConfigFileType switch
        {
            EnumConfigFileType.Json => new JsonParser<T>(),
            EnumConfigFileType.Toml => new TomlParser<T>(),
            EnumConfigFileType.Yaml => throw new ArgumentOutOfRangeException(),
            EnumConfigFileType.Xml => throw new ArgumentOutOfRangeException(),
            EnumConfigFileType.Default => throw new ArgumentOutOfRangeException(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    /// <summary>
    /// 初始化文件
    /// </summary>
    private void InitFile()
    {
        if (File.Exists(FilePath)) return;
        File.Create(FilePath).Dispose();
        Save();
    }
    
    /// <summary>
    /// 加载配置
    /// </summary>
    public void Load()
    {
        var str = File.ReadAllText(FilePath);
        var loadedData = _parser!.Deserialization(str);
        Utilities.CopyProperties(loadedData, ConfigData);
    }

    /// <summary>
    /// 保存配置
    /// </summary>
    public void Save()
    {
        var str = _parser!.Serialization(ConfigData, GlobalConfigSettings.HiddenMemberList);
        File.WriteAllText(FilePath, str);
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