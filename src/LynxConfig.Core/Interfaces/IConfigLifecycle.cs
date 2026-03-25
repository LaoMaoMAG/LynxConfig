namespace LynxConfig.Core.Interfaces;

/// <summary>
/// 配置文件生命周期接口
/// </summary>
public interface IConfigLifecycle
{
    /// <summary>
    /// 初始化
    /// </summary>
    void Init();
    
    /// <summary>
    /// 加载配置文件（失败抛异常）
    /// </summary>
    void Load();
    
    /// <summary>
    /// 保存配置文件（失败抛异常）
    /// </summary>
    void Save();
    
    /// <summary>
    /// 尝试加载配置文件（失败返回 false）
    /// </summary>
    bool TryLoad();
    
    /// <summary>
    /// 尝试加载配置文件（失败返回 false 并输出异常）
    /// </summary>
    bool TryLoad(out Exception? error);
    
    /// <summary>
    /// 尝试保存配置文件（失败返回 false）
    /// </summary>
    bool TrySave();
    
    /// <summary>
    /// 尝试保存配置文件（失败返回 false 并输出异常）
    /// </summary>
    bool TrySave(out Exception? error);
}