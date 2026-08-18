
// Type: Intermech.PropertyEditors.ObjectTypesHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.PropertyEditors;

/// <summary>
/// Класс, выполняющий вспомогательные работы с типами объектов
/// </summary>
public static class ObjectTypesHelper
{
  /// <summary>Вернуть ID типа объекта по его GUID</summary>
  /// <param name="ObjGUID">GUID типа объекта</param>
  /// <returns>ID объекта или 0 если тип не найден</returns>
  public static int GetObjTypeID(string ObjGUID)
  {
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(new Guid(ObjGUID), false);
    return objectType != null ? objectType.ObjectType : 0;
  }
}
