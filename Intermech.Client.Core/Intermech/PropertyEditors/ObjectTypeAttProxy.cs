
// Type: Intermech.PropertyEditors.ObjectTypeAttProxy
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjectTypeProxy.</summary>
public class ObjectTypeAttProxy
{
  private Guid _guid;
  private string _typeName;

  public ObjectTypeAttProxy(Guid guid)
  {
    this._guid = guid;
    this._typeName = string.Empty;
  }

  public override string ToString()
  {
    if (this._guid == Guid.Empty)
      return LocalizationHolder.rm.GetString("Client.Core_929");
    if (this._typeName.Length == 0)
    {
      IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(this._guid, false);
      this._typeName = objectType != null ? objectType.ObjectTypeName : "Тип объектов не найден в базе данных: " + (object) this._guid;
    }
    return this._typeName;
  }

  public Guid Guid => this._guid;
}
