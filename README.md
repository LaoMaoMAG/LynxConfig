![LynxConfig](https://socialify.dev/LaoMaoMAG/LynxConfig/image?description=1&font=KoHo&forks=1&issues=1&logo=https%3A%2F%2Fraw.githubusercontent.com%2FLaoMaoMAG%2FLynxConfig%2Frefs%2Fheads%2Fmaster%2Ficon.png&name=1&pattern=Diagonal+Stripes&pulls=1&stargazers=1&theme=Auto)

## 用途

LynxConfig 的目标是让配置能够直接以普通 C# 对象的形式存在，同时将**配置对象生命周期**与**配置文件解析格式**分离。

配置类本身只需要描述数据结构，具体使用哪一种配置文件格式，则由独立的 Parser 决定。

> **项目目前仍处于开发阶段，基础功能尚未全部完成，API 和内部设计都可能发生变化。**
>
> 当前仓库中的代码和测试主要用于验证设计，因此暂时不建议将其用于生产环境。

---

## 特点

- 使用普通 C# 类表示配置
- 通过强类型结构提供严谨的配置数据
- 支持加载和保存配置文件
- JSON、TOML、YAML、XML 解析器独立拆分
- 根据需要安装对应的解析器，避免不必要的依赖

---

## 开发状态


LynxConfig 目前仍然是一个**开发中的项目**

当前代码已经建立了核心架构以及三种配置使用模式，但基础功能、Parser 和测试仍在持续完善

| 功能               | 完成度   |
|--------------------|----------|
| 配置类（基础模式） | 基本完成 |
| 配置类（绑定模式） | 基本完成 |
| 配置类（单例模式） | 基本完成 |
| 多配置文件格式     | 正在进行 |
| 配置文件注释       | 正在进行 |
| 性能优化           | 正在进行 |
| KV 数据库配置      | 后续计划 |
| 自定义格式         | 后续计划 |
| 本地化注释 I18n    | 后续计划 |

因此目前不保证 API 稳定，也不保证所有功能已经可以用于实际生产环境

---

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
- `LynxConfig.Database`：KV 数据库配置库
- `LynxConfig.Parser.*`：不同配置格式的解析器
- `LynxConfig.Test`：测试和使用示例

---

## 配置文件格式

LynxConfig 将不同的配置格式拆分为独立的 Parser 项目，目前计划支持：

| 格式 | 命名空间                 | 完成度       |
| --- |--------------------------|--------------|
| JSON | `LynxConfig.Parser.Json` | 部分功能完成 |
| TOML | `LynxConfig.Parser.Toml` | 部分功能完成 |
| YAML | `LynxConfig.Parser.Yaml` | 计划中       |
| XML | `LynxConfig.Parser.Xml`  | 计划中       |

这种设计的目标是避免让核心配置库强制依赖所有格式的序列化库。

例如：

```text
只需要 JSON

LynxConfig + LynxConfig.Parser.Json
```

或者：

```text
只需要 TOML

LynxConfig + LynxConfig.Parser.Toml
```

从而让不同项目可以根据需要选择配置格式。

---

# 三种配置模式

LynxConfig 当前设计了三种不同的配置使用方式：

- Base
- Singleton
- Binding

它们使用相同的配置生命周期，但针对不同的使用场景。

---

## Base 模式

Base 模式是最普通的配置类使用方式。

只需要继承 `ConfigBase<T>`：

```csharp
using LynxConfig.Config;
using LynxConfig.Parser.Json;

public class MyConfig : ConfigBase<MyConfig>
{
    public MyConfig() : base("./config.json", JsonParser.Instance)
    {
    }

    public string Name { get; set; } = "下北泽特级厨师";

    public int Value { get; set; } = 114514;
}
```

然后像普通对象一样使用：

```csharp
var config = new MyConfig();

config.Load();

Console.WriteLine(config.Name);
Console.WriteLine(config.Value);

config.Value++;

config.Save();
```

测试项目中的 `TestData` 就采用了这种方式。

对应的使用模型可以简单理解为：

```text
new MyConfig()
      │
      ▼
  独立配置实例
      │
    Load()
      │
      ▼
  修改配置数据
      │
    Save()
```

适用于一个程序中存在多个独立配置实例的情况。

---

# Singleton 模式

如果某个配置在整个程序中只需要存在一个实例，可以使用 `ConfigSingleton<T>`。

例如：

```csharp
using LynxConfig.Config;
using LynxConfig.Parser.Json;

public class MyConfig : ConfigSingleton<MyConfig>
{
    public MyConfig() : base("./config.json", JsonParser.Instance)
    {
    }

    public string Name { get; set; } = "下北泽特级厨师";

    public int Value { get; set; } = 114514;
}
```

通过 `Instance` 获取配置对象：

```csharp
MyConfig.Instance.Load();

Console.WriteLine(MyConfig.Instance.Name);

MyConfig.Instance.Value++;

MyConfig.Instance.Save();
```

`ConfigSingleton<T>` 内部提供：

```csharp
public static T Instance { get; } = new();
```

因此应用程序可以直接通过 `Instance` 使用同一个配置对象。

测试项目中的 `TestData1` 展示了这种用法。

---

# Binding 模式

Binding 模式与前两种模式不同。

它并不负责创建配置数据对象，而是将一个**已经存在的对象绑定到配置文件和 Parser**。

例如：

```csharp
public class MyConfig
{
    public string Name { get; set; } = "下北泽特级厨师";

    public int Value { get; set; } = 114514;
}
```

然后：

```csharp
var data = new MyConfig();

var binding = new ConfigBinding<MyConfig> (
    data,
    "./config.json",
    JsonParser.Instance
);

binding.Load();

Console.WriteLine(data.Name);
Console.WriteLine(data.Value);

data.Value++;

binding.Save();
```

`ConfigBinding<T>` 接收三个核心参数：

```text
配置对象
   +
配置文件路径
   +
Parser
```

然后负责将配置生命周期绑定到这个对象上。

测试项目中的 `TestData2` 正是用于验证这种模式。

这使 Binding 模式可以用于：

```text
一个配置结构
      │
      ├── 配置实例 A
      ├── 配置实例 B
      ├── 配置实例 C
      └── 配置实例 D
```

多个对象可以拥有相同的配置结构，而分别绑定到不同的配置文件。

这也是 Binding 模式与 Base / Singleton 模式之间最重要的区别之一。

---

# 配置对象

LynxConfig 使用普通 C# 类型作为配置数据。

例如：

```csharp
public class MyConfig : ConfigBase<MyConfig>
{
    public string Name { get; set; } = "LaoMao";

    public int Value { get; set; } = 100;

    public bool Enabled { get; set; } = true;
}
```

也支持嵌套对象：

```csharp
public class MyConfig : ConfigBase<MyConfig>
{
    public string Name { get; set; } = "LaoMao";

    public SettingsData Settings { get; set; } = new();

    public class SettingsData
    {
        public bool Enabled { get; set; } = true;

        public int Value { get; set; } = 100;
    }
}
```

目前 Test 项目已经包含基础类型以及嵌套对象的测试数据。

因此配置在代码中的形态仍然是普通的强类型 C# 对象：

```csharp
config.Settings.Enabled = true;
config.Settings.Value = 100;
```

而不是通过大量字符串 Key 操作配置。

---

目前 Test 项目已经包含基础类型以及嵌套对象的测试数据。

因此配置在代码中的形态仍然是普通的强类型 C# 对象：

```csharp
config.Settings.Enabled = true;
config.Settings.Value = 100;
```

而不是通过大量字符串 Key 操作配置。

---

# Load / Save

所有配置模式最终都实现统一的生命周期接口。

当前配置生命周期主要包括：

```csharp
Init();
Load();
Save();
```

同时还提供：

```csharp
TryLoad();
TryLoad(out Exception? error);

TrySave();
TrySave(out Exception? error);
```

其中：

- `Init()`：初始化配置文件
- `Load()`：从配置文件读取数据
- `Save()`：将当前配置数据写入文件
- `TryLoad()` / `TrySave()`：以返回 `bool` 的方式处理异常

配置对象的核心生命周期由 `ConfigAbstract<T>` 统一实现。

---

# 设计目标

LynxConfig 希望解决的是：

> **让配置成为真正的 C# 对象，同时把“配置对象是什么”和“配置文件是什么格式”分开。**

理想的使用方式是：

```text
        配置类
          │
          ▼
    普通 C# 对象
          │
      ┌───┴───┐
      │       │
    Load     Save
      │       │
      └───┬───┘
          ▼
       Parser
          │
   ┌──────┼──────┐
   ▼      ▼      ▼
 JSON   TOML    YAML ...
```

而 Base、Singleton 和 Binding 则解决的是：

```text
“配置对象由谁管理？”
```

Parser 解决的是：

```text
“配置数据如何与外部格式交换？”
```

这两个问题彼此独立

---

## License

Copyright © 2026 laomaomag

本项目使用的许可证请参阅仓库中的 [`LICENSE`](./LICENSE) 文件。
