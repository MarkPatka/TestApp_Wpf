namespace TestApp_Wpf.Models.DomainModels.AggregateObjects;

/// <summary>
/// </summary>
/// <param name="Distance">Горизонтальная координата - М (0-20)</param>
/// <param name="Angle">Вертикальная координата – Ч (0-12)</param>
/// <param name="Width">Горизонтальный размер объекта – М (0-20)</param>
/// <param name="Height">Вертикальный размер объекта – Ч (0-12)</param>
public record Measure(
    double Distance,
    double Angle,
    double Width,
    double Height);
