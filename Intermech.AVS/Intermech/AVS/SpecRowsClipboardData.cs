// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecRowsClipboardData
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>Данные для помещения записей в буфер</summary>
[Serializable]
public class SpecRowsClipboardData
{
  /// <summary>Идентификаторы строки спецификации</summary>
  public SpecificationRowID[] RowsID;

  /// <summary>Конструктор</summary>
  /// <param name="rowsID">Идентификаторы строки спецификации</param>
  public SpecRowsClipboardData(SpecificationRowID[] rowsID) => this.RowsID = rowsID;
}
