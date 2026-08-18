// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBObjectTypeID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи сведений об идентификаторах типов объектов
/// базы данных между различными частями системы. Доступ к передаваемой
/// информации осуществляется через интерфейс IDBObjectTypeID.
/// </summary>
public class DBObjectTypeID : IDBObjectTypeID
{
  private int _objTypeID;

  public DBObjectTypeID(int objTypeID) => this._objTypeID = objTypeID;

  public int Value => this._objTypeID;

  public override bool Equals(object obj)
  {
    return obj is DBObjectTypeID && this._objTypeID == (obj as DBObjectTypeID)._objTypeID;
  }

  public override int GetHashCode() => this._objTypeID;
}
