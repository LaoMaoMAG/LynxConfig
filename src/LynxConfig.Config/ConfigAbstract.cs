using LynxConfig.Core.Interfaces;

namespace LynxConfig.Config;

public abstract class ConfigAbstract<T> : IConfigLifecycle where T : class, new()
{
    public abstract string FilePath { get; set; }

    protected abstract T ConfigData { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
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
        ConfigData = Deserialization(str);
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
    protected abstract string Serialization(T obj);

    /// <summary>
    /// 反序列化
    /// </summary>
    protected abstract T Deserialization(string str);
}