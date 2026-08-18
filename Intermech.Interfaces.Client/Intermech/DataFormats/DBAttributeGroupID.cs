// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBAttributeGroupID
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
public class DBAttributeGroupID : IDBAttributeGroupID
{
  private int _attrGroupID;

  public DBAttributeGroupID(int aAttrGroupId) => this._attrGroupID = aAttrGroupId;

  public int AttributeGroupID => this._attrGroupID;

  public override int GetHashCode() => this._attrGroupID.GetHashCode();

  public override string ToString()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributesGroup attributesGroup = sessionKeeper.Session.GetAttributesGroup(this._attrGroupID);
      return attributesGroup == null ? string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_GroupNotFound"), (object) this._attrGroupID) : attributesGroup.GroupName;
    }
  }
}
