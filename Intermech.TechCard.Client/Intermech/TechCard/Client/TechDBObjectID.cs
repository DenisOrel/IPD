// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechDBObjectID
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>TechCard object id</summary>
/// <summary>Конструктор</summary>
/// <param name="objId">Идентификатор версии объекта (F_OBJECT_ID)</param>
/// <param name="id">Идентификатор объекта</param>
/// <param name="caption">Заголовок объекта</param>
public class TechDBObjectID(long objId, long id, string caption) : DBObjectID(objId, id, caption, 0L)
{
  /// <summary>Override base to string methods</summary>
  /// <returns></returns>
  public override string ToString() => this.Caption;
}
