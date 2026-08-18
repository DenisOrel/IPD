
// Type: Intermech.Navigator.ObjectTypeNamesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Navigator;

/// <summary>
/// Кэш названий типов объектов. При отсутствии в нем названия для указанного
/// идентификатора кэш лезет в базу.
/// </summary>
public class ObjectTypeNamesCache : ICache, IObjectTypeNamesCache
{
  /// <summary>Сбросить содержимое кэша</summary>
  public void Reset()
  {
  }

  /// <summary>Получить название типа объекта</summary>
  /// <param name="objectTypeID">Идентификатор типа объекта</param>
  /// <returns>Название типа объекта</returns>
  public string GetTypeName(int objectTypeID) => MetaDataHelper.GetObjectTypeName(objectTypeID);
}
