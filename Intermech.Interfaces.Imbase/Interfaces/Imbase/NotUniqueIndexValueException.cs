// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.NotUniqueIndexValueException
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
[Serializable]
public class NotUniqueIndexValueException : ApplicationException
{
  /// <summary>Список неуникальных индексов.</summary>
  public List<int> NotUniqueIndexes { get; set; }

  /// <summary>Список номеров строк с неуникальными значениями.</summary>
  public List<long> RowNumbers { get; set; }

  /// <summary>
  /// Таблица с информацией о неуникальных данных в других ссылках.
  /// </summary>
  public DataTable Table { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public NotUniqueIndexValueException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.NotUniqueIndexes = info.GetValue(nameof (NotUniqueIndexes), typeof (List<int>)) as List<int>;
    this.RowNumbers = info.GetValue(nameof (RowNumbers), typeof (List<long>)) as List<long>;
    this.Table = info.GetValue(nameof (Table), typeof (DataTable)) as DataTable;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="msg">Текст сообщения</param>
  /// <param name="innerEx">Системная ошибка</param>
  public NotUniqueIndexValueException(string msg, Exception innerEx = null)
    : base(msg, innerEx)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("NotUniqueIndexes", (object) this.NotUniqueIndexes);
    info.AddValue("RowNumbers", (object) this.RowNumbers);
    if (this.Table != null)
      this.Table.RemotingFormat = SerializationFormat.Binary;
    info.AddValue("Table", (object) this.Table);
  }
}
