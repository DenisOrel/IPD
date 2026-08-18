// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.RestructuringTablesExteption
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
[Serializable]
public class RestructuringTablesExteption : ApplicationException
{
  /// <summary>
  /// 
  /// </summary>
  public RestructuringTablesExteption(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.ID = info.GetInt64(nameof (ID));
    this.Caption = info.GetString(nameof (Caption));
  }

  /// <summary>Конструктор.</summary>
  /// <param name="msg">Текст сообщения</param>
  /// <param name="id">Идентификатор версии объектов</param>
  /// <param name="caption">Наименование объекта</param>
  public RestructuringTablesExteption(string msg, long id = 0, string caption = "")
    : base(msg)
  {
    this.ID = id;
    this.Caption = caption;
  }

  /// <summary>Идентификатор версии объектов.</summary>
  public long ID { get; private set; }

  /// <summary>Наименование объекта.</summary>
  public string Caption { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ID", this.ID);
    info.AddValue("Caption", (object) this.Caption);
  }
}
