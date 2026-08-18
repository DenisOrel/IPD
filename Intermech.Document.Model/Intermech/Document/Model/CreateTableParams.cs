// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.CreateTableParams
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Параметры таблицы, используется при ее создании</summary>
[Serializable]
public class CreateTableParams
{
  /// <summary>Первая строка - заголовок</summary>
  public bool FirstRowIsHeader;
  /// <summary>К-во однотипных строк</summary>
  public int StdRowCount = -1;
  /// <summary>Параметры строк</summary>
  public List<RowColParams> RowList = new List<RowColParams>();
  /// <summary>Первый столбец - заголовок</summary>
  public bool FirstColIsHeader;
  /// <summary>К-во однотипных столбцов</summary>
  public int StdColCount = -1;
  /// <summary>Параметры столбцов</summary>
  public List<RowColParams> ColumnList = new List<RowColParams>();
}
