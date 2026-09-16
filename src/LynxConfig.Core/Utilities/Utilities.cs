using System.Reflection;

namespace LynxConfig.Core;

/// <summary>
/// 工具类
/// </summary>
public static class Utilities
{
    /// <summary>
    /// 拷贝属性
    /// </summary>
    /// <typeparam name="TObj">对象类型</typeparam>
    /// <param name="source">源对象</param>
    /// <param name="target">目标对象</param>
    public static void CopyProperties<TObj>(TObj source, TObj target)
    {
        foreach (var prop in typeof(TObj).GetProperties())
        {
            if (prop.CanWrite && prop.GetAccessors().Any(x => x.IsPublic))
            {
                prop.SetValue(target, prop.GetValue(source));
            }
        }
    }
    
    /// <summary>
    /// 获取公共成员名称
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <returns>公共成员名称列表</returns>
    public static List<string> GetPublicMemberNames<T>()
    {
        var type = typeof(T); 
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
        var propertyNames = type.GetProperties(flags).Select(p => p.Name);
        var fieldNames = type.GetFields(flags).Select(f => f.Name);
        return [.. propertyNames.Concat(fieldNames).Distinct()];
    }
}