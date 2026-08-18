// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPProgress
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Интерфейс позволяет читать/записывать значения для текущего прогресса какого-то действия
/// </summary>
public interface IMRPProgress
{
  /// <summary>Минимальное значение для прогресс-бара</summary>
  int MinProgress { get; set; }

  /// <summary>Максимальное значение для прогресс-бара</summary>
  int MaxProgress { get; set; }

  /// <summary>Текущее значение для прогресс-бара</summary>
  int Progress { get; set; }
}
