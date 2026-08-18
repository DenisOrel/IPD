// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.RemoveCellOptions
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Вариант удаления ячейки</summary>
public enum RemoveCellOptions
{
  /// <summary>Удалить объединив с ячейкой слева</summary>
  MergeWithLeft,
  /// <summary>Удалить объединив с ячейкой справа</summary>
  MergeWithRight,
  /// <summary>Удалить объединив с ячейкой сверху</summary>
  MergeWithTop,
  /// <summary>Удалить объединив с ячейкой снизу</summary>
  MergeWithBottom,
}
