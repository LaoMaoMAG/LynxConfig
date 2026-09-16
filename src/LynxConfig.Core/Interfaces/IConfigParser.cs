namespace LynxConfig.Core.Interfaces;

/// <summary>
/// 配置序列化接口
/// </summary>
public interface IConfigParser
{
    /// <summary>
    /// 配置解析器实例
    /// </summary>
    static abstract IConfigParser Instance { get; }
    
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="obj">配置对象</param>
    /// <param name="hiddenMemberList">隐藏成员列表</param>
    /// <returns>配置字符串</returns>
    string Serialization<T>(T obj, HashSet<string> hiddenMemberList) where T : class, new();

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="str">配置字符串</param>
    /// <returns>配置对象</returns>
    T Deserialization<T>(string str) where T : class, new();
}