// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ResolutionExecution
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Исполнение поручения</summary>
public enum ResolutionExecution
{
  /// <summary>Параллельное</summary>
  Parallel,
  /// <summary>Последовательное</summary>
  Successive,
  /// <summary>Комбинированное</summary>
  Combined,
}
