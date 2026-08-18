
// Type: Intermech.Navigator.IClientObjectsInfoCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Navigator;

/// <summary>
/// Интерфейс для извлечения из кэша заголовков и кратких описаний объектов по их идентификаторам в базе данных
/// </summary>
public interface IClientObjectsInfoCache : ICache, IObjectsInfoCache
{
  /// <summary>Удаляет из кэша описание для объекта</summary>
  /// <param name="objectId">Идентификатор объекта для удаления описания</param>
  bool ResetInfo(long objectId);
}
