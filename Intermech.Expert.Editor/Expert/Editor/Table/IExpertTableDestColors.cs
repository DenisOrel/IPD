// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.IExpertTableDestColors
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System;

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Интерфейс для одного ряда или столбца</summary>
public interface IExpertTableDestColors
{
  /// <summary>Цвета для ячейки заголовка</summary>
  IExpertTableItemColors Header { get; }

  /// <summary>Цвета для ячейки данных</summary>
  IExpertTableItemColors Data { get; }

  /// <summary>Событие на изменение цвета</summary>
  event EventHandler Changed;
}
