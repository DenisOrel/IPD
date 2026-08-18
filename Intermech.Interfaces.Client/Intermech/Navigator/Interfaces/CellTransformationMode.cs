// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.CellTransformationMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Перечислитель указывает, каким образом будут преобразовываться ячейки в колонке
/// </summary>
[Serializable]
public enum CellTransformationMode
{
  /// <summary>Значения преобразовываться не будут</summary>
  WithoutTransformation,
  /// <summary>
  /// Значения будут преобразованы в строки для отображения на экране
  /// </summary>
  ConvertToString,
  /// <summary>
  /// Значения будут преобразованы в составной класс - [Оригинальное значение] + [Значение для отображения на экране]
  /// </summary>
  ConvertToCellValue,
}
