namespace LynxConfig.Core.Interfaces;

/// <summary>
/// 配置序列化接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IConfigParser<T> where T : class, new()
{
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="obj">配置对象</param>
    /// <param name="hiddenMemberList">隐藏成员列表</param>
    /// <returns>配置字符串</returns>
    string Serialization(T obj, HashSet<string> hiddenMemberList);

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="str">配置字符串</param>
    /// <param name="obj">配置对象</param>
    T Deserialization(string str);
}