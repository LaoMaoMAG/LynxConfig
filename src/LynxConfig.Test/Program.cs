using LynxConfig.Config;
using LynxConfig.Config.Enums;
using LynxConfig.Parser.Json;

namespace LynxConfig.Test;

class Program
{
    static void Main(string[] args)
    {
        // Base 模式
        TestData data = new();
        data.Init();
        data.Load();
        Console.WriteLine(data.Test);
        Console.WriteLine(data.Test2);
        data.Test = "bbbb";
        data.Test2 = 114514;
        data.Save();
        
        // Singleton 模式
        TestData1.Instance.Load();
        Console.WriteLine(TestData1.Instance.Test);
        Console.WriteLine(TestData1.Instance.Test2);
        TestData1.Instance.Test = "bbbb";
        TestData1.Instance.Test2 = 114514;
        TestData1.Instance.Save();
        
        // Binding 模式
        TestData2 data2 = new();
        var binding = new ConfigBinding<TestData2>(data2, "./test2.json", JsonParser.Instance);
        binding.Load();
        Console.WriteLine(data2.Test);
        Console.WriteLine(data2.Test2);
        data2.Test = "bbbb";
        data2.Test2 = 114514;
        binding.Save();
    }
}
