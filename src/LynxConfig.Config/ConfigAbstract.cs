using LynxConfig.Config.Enums;
using LynxConfig.Core.Interfaces;
using LynxConfig.Parser.Toml;


namespace LynxConfig.Config;

public abstract class ConfigAbstract<T> : IConfigLifecycle where T : class, new()
{
    /// <summary>
    /// 配置文件路径
    /// </summary>
    public abstract string FilePath { get; set; }

    /// <summary>
    /// 配置文件类型
    /// </summary>
    public EnumConfigFileType ConfigFileType
    {
        get => _configFileType == EnumConfigFileType.Default
            ? GlobalConfigSettings.DefaultConfigFileType
            : _configFileType;
        set => _configFileType = value;
    }
    private EnumConfigFileType _configFileType = EnumConfigFileType.Default;

    /// <summary>
    /// 配置数据
    /// </summary>
    protected abstract T ConfigData { get; set; }
    
    protected IConfigParser<T> Parser;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        Parser = ConfigFileType switch
        {
            EnumConfigFileType.Json => throw new ArgumentOutOfRangeException(),
            EnumConfigFileType.Toml => new TomlParser<T>(),
            EnumConfigFileType.Yaml => throw new ArgumentOutOfRangeException(),
            EnumConfigFileType.Xml => throw new ArgumentOutOfRangeException(),
            EnumConfigFileType.Default => throw new ArgumentOutOfRangeException(),
            _ => throw new ArgumentOutOfRangeException()
        };
        
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
        Deserialization(str, out var tempData);
        ConfigData = tempData;
    }

    /// <summary>
    /// 保存配置
    /// </summary>
    public void Save()
    {
        File.WriteAllText(FilePath, Serialization(ConfigData));
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

    /// <summary>
    /// 序列化
    /// </summary>
    protected string Serialization(T obj)
    {
        return Parser.Serialization(obj);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    protected void Deserialization(string str, out T obj)
    {
        Parser.Deserialization(str, out obj);
    }
}