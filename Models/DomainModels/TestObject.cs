using TestApp_Wpf.Models.SubModels;

namespace TestApp_Wpf.Models.DomainModels;

public class TestObject
{
    public string Name { get; } = null!;
    public Measure Data { get; } = null!;
    public bool IsDefect { get; }
}
