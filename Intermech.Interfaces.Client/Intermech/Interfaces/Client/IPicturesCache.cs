// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPicturesCache
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для кеширования изображений для объектов со стороны клиента.
/// </summary>
public interface IPicturesCache
{
  /// <summary>Сигнал об окончании загрузки рисунка</summary>
  event LoadCompleteEventHandler LoadComplete;

  event CacheChangedEventHandler CacheChanged;

  event TranslateObjectIdEventHandler TranslateObject;

  /// <summary>
  /// Возвращает или текущую строку фильтра имен файлов, которая определяет позиции, отображающиеся в окне «Файлы типа» диалогового окна.
  /// </summary>
  string Filter { get; }

  /// <summary>
  /// Получение идентификатора сессии. Этот идентификатор используется в
  /// дальнейшем для передачи в метод загрузки изображения и при получении события об
  /// окончании загрузки для идентификации окна, которое запросило этот рисунок.
  /// </summary>
  int Session { get; }

  /// <summary>
  /// Загружает для объекта изображение из атрибута изображение.
  /// </summary>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <returns></returns>
  object GetPicture(long objectId);

  /// <summary>
  /// Загружает для объекта изображение из атрибута изображение.
  /// </summary>
  /// <param name="objectGuid">Guid версии объекта</param>
  /// <returns></returns>
  object GetPicture(Guid objectGuid);

  /// <summary>Получение изображения без постановки в очередь.</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="newObjectId">Новый идентификатор объекта</param>
  /// <returns>
  /// Возвращает загруженное изображение или DbNull.Value, если для этого объекта изображение не определено.
  /// Иначе возвращает загруженное изображение.
  /// </returns>
  object GetPicture(int objectType, long objectId, out long newObjectId);

  /// <summary>Постановка в очередь загрузки изображения.</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="sessionId">Идентификатор сессии</param>
  /// <param name="newObjectId">Новый идентификатор объекта</param>
  /// <returns>Возвращает null, если в кеше нет рисунка и загрузка поставлена в очередь</returns>
  object LoadPicture(int objectType, long objectId, int sessionId, out long newObjectId);

  /// <summary>Регистрирует интерфейс для создания изображений</summary>
  /// <param name="creator">Создатель объекта</param>
  /// <param name="ext">Расширение файла( без '.')</param>
  /// <param name="description">Описание</param>
  void RegisterPictureFile(IThumbImageCreator creator, string ext, string description);

  /// <summary>Обновляет блоб с изображением.</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <returns></returns>
  bool UpdateItem(int objectType, long objectId);

  /// <summary>Обновляет блоб с изображением.</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор объекта</param>
  /// <param name="fileName">Полное наименование файла</param>
  /// <returns></returns>
  bool UpdateItem(int objectType, long objectId, string fileName);
}
