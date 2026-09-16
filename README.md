# LynxConfig

一个轻量的 .NET 配置库，让配置可以像普通 C# 对象一样直接使用，通过强类型结构减少错误，并通过独立解析器按需支持 JSON、TOML、YAML 和 XML。

> **项目目前仍在开发中，大部分功能尚未完成，API 也可能发生变化。**

## 特点

- 使用普通 C# 类表示配置
- 通过强类型结构提供严谨的配置数据
- 支持加载和保存配置文件
- JSON、TOML、YAML、XML 解析器独立拆分
- 根据需要安装对应的解析器，避免不必要的依赖

## 基本使用

> API 已更改，以下方法不再适用

定义一个配置类：

```csharp
using LynxConfig.Config;

public class MyConfig : ConfigBase<MyConfig>
{
    public string Name { get; set; } = "LaoMao";

    public int Value { get; set; } = 100;
}
```

之后可以像普通 C# 对象一样使用：

```csharp
var config = new MyConfig();
config.Load();

config.Value++;

config.Save();
```

也可以读取已有的配置文件：

```csharp
var config = new MyConfig();
config.Load();

Console.WriteLine(config.Name);
Console.WriteLine(config.Value);
```

## 配置文件格式

LynxConfig 将不同的配置格式拆分为独立的 Parser 项目，目前计划支持：

| 格式 | Parser |
| --- | --- |
| JSON | `LynxConfig.Parser.Json` |
| TOML | `LynxConfig.Parser.Toml` |
| YAML | `LynxConfig.Parser.Yaml` |
| XML | `LynxConfig.Parser.Xml` |

这样可以根据项目实际需要选择配置格式，而不需要让核心库强制依赖所有序列化库

例如需要 TOML 时使用对应的 Tomlyn 库

## 配置文件类型

创建配置对象时可以指定配置文件类型：

```csharp
public class MyConfig() : ConfigBase<MyConfig>("./test.json", EnumConfigFileType.Json)
{
    // ...
};
```

具体配置文件类型和相关 API 仍在开发中，请以当前代码为准。

## Singleton

对于希望全局使用的配置，也提供 Singleton 形式：

```csharp
public class MyConfig() : ConfigSingleton<MyConfig>("./test.json", EnumConfigFileType.Json)
{
    public string Name { get; set; } = "下北泽特级厨师";
}
```

使用：

```csharp
MyConfig.Instance.Name = "Test";
MyConfig.Instance.Save();
```

## 项目结构

当前项目主要由以下部分组成：

```text
LynxConfig
├── LynxConfig.Core
├── LynxConfig.Config
├── LynxConfig.Database
├── LynxConfig.Parser.Json
├── LynxConfig.Parser.Toml
├── LynxConfig.Parser.Yaml
├── LynxConfig.Parser.Xml
└── LynxConfig.Test
```

其中：

- `LynxConfig.Core`：核心接口和基础功能
- `LynxConfig.Config`：配置对象相关实现
- `LynxConfig.Database`：不在当前计划
- `LynxConfig.Parser.*`：不同配置格式的解析器
- `LynxConfig.Test`：测试和使用示例

## 开发状态

目前项目仍处于开发阶段。

因此不建议将当前版本作为稳定 API 使用。

## 设计目标

LynxConfig 的目标并不是提供一个复杂的配置框架，而是尽可能让配置回到比较简单的使用方式：

```text
配置文件
   ↓
C# 配置对象
   ↓
直接读取或修改
   ↓
Save()
```

希望在不引入复杂配置体系的情况下，让配置能够直接融入普通的 C# 项目。

## License

License 信息请以仓库中的 `LICENSE` 文件为准。
