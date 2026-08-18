// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.TableClone
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Клон таблицы</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
/// <param name="origin">Примитив</param>
public class TableClone(GroupClone owner, RectPrimitive origin) : GroupClone(owner, origin)
{
  /// <summary>Ширины столбцов</summary>
  public List<int> colWidths = new List<int>();
  /// <summary>высоты строк</summary>
  public List<int> rowHeights = new List<int>();

  /// <summary>Ширины столбцов</summary>
  public List<int> ColWidths => this.colWidths;

  /// <summary>высоты строк</summary>
  public List<int> RowHeights => this.rowHeights;

  /// <summary>Загрузить</summary>
  /// <param name="ueDoc">Документ</param>
  public override void Load(UEditDocument ueDoc)
  {
    BinaryReader reader = ueDoc.Reader;
    base.Load(ueDoc);
    PrimitiveLoader.LoadIntList(this.colWidths, reader);
    PrimitiveLoader.LoadIntList(this.rowHeights, reader);
  }
}
