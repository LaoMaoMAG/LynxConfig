namespace LynxConfig.Test;

public class TestData2
{
    public string Test { get; set; } = "test";
    public int Test2 { get; set; } = 123;
    public bool Test3 { get; set; } = true;
    public float Test4 { get; set; } = 1.23f;
    
    public Test5Data Test5 { get; set; } = new();
    
    public class Test5Data
    {
        public string Test { get; set; } = "test";
        public int Test2 { get; set; } = 123;
        public bool Test3 { get; set; } = true;
        public float Test4 { get; set; } = 1.23f;
    }
}