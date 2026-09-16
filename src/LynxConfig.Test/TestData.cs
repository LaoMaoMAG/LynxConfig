using LynxConfig.Config;
using LynxConfig.Config.Enums;
using LynxConfig.Parser.Json;

namespace LynxConfig.Test;

public class TestData() : ConfigBase<TestData>("./test.json", new JsonParser<TestData>())
{
    public string Test { get; set; } = "test";
    public int Test2 { get; set; } = 123;
    public bool Test3 { get; set; } = true;
    public float Test4 { get; set; } = 1.23f;
    
    public Test5Data Test5 { get; set; } = new();
    
    public class Test5Data
    {
        public string Test11 { get; set; } = "test";
        public int Test22 { get; set; } = 123;
        public bool Test33 { get; set; } = true;
        public float Test44 { get; set; } = 1.23f;
    }
}