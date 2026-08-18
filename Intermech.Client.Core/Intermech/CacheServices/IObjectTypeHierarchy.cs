
// Type: Intermech.CacheServices.IObjectTypeHierarchy
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.CacheServices;

public interface IObjectTypeHierarchy : ICacheService
{
  /// <summary>
  /// Проверить, доступен ли по правам доступа и предметным областям указанный тип объектов
  /// </summary>
  /// <param name="objTypeID">Проверяемый тип объектов</param>
  /// <returns>true - объект доступен</returns>
  bool EnabledObjectType(int objTypeID);

  /// <summary>
  /// Возвращает идентификатор родительского типа объектов. Если результат равен -1, то
  /// это корневой тип объектов.
  /// </summary>
  /// <param name="childTypeID">Идентификатор дочернего типа объектов</param>
  /// <returns>Идентификатор родительского типа объектов</returns>
  int GetParentType(int childTypeID);

  /// <summary>
  /// Возвращает идентификаторы всех родительских типов объектов для указанного типа объектов.
  /// </summary>
  /// <param name="childTypeID">Идентификатор дочернего типа объектов</param>
  /// <returns>Массив идентификаторов родительских типов объектов</returns>
  int[] GetParentTypes(int childTypeID);
}
