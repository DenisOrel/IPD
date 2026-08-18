// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.TableSize
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Размерность таблицы</summary>
[Serializable]
public class TableSize
{
  /// <summary>Количество строк</summary>
  public int Rows;
  /// <summary>Количество столбцов</summary>
  public int Columns;

  /// <summary>Конструктор</summary>
  /// <param name="rows">Количество строк</param>
  /// <param name="columns">Количество столбцов</param>
  public TableSize(int rows, int columns)
  {
    this.Rows = rows;
    this.Columns = columns;
  }

  /// <summary>Конструктор</summary>
  public TableSize()
  {
  }
}
