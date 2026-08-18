// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBAttributeID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений об идентификаторах
/// атрибутов базы данных между различными частями системы. Доступ
/// к передаваемой информации осуществляется через интерфейс
/// IDBAttributeID.
/// </summary>
public class DBAttributeID : IDBAttributeID
{
  private int _attrID;

  public DBAttributeID(int aAttrId) => this._attrID = aAttrId;

  public int AttribyteID => this._attrID;

  public override int GetHashCode() => this._attrID.GetHashCode();

  public override string ToString()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this._attrID);
      return attributeType == null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_60"), (object) this._attrID) : attributeType.Name;
    }
  }
}
