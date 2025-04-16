using TestApp_Wpf.Models.Common.Abstract;

namespace TestApp_Wpf.Models.DomainModels;

public class TestObject : IDomainModel
{
    public string Name { get; set; } = null!;

    /// <summary>
    /// Горизонтальная координата - М (0-20)
    /// </summary>
    public double Distance { get; set; }
    /// <summary>
    /// Вертикальная координата – Ч (0-12)
    /// </summary>
    public double Angle { get; set; }
    /// <summary>
    /// Горизонтальный размер объекта – М (0-20)
    /// </summary>
    public double Width { get; set; }
    /// <summary>
    /// Вертикальный размер объекта – Ч (0-12)
    /// </summary>
    public double Height { get; set; }

    public bool IsDefect { get; set; }
}
