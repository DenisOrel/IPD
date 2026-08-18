// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ParamsStorage.IParamsStorageService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.ParamsStorage;

/// <summary>Интерфейс службы параметров (начальных настроек)</summary>
public interface IParamsStorageService
{
  /// <summary>
  /// Создание / регистрация объекта-контейнера параметров
  /// </summary>
  /// <param name="storagеName">Имя объекта-контейнера</param>
  /// <param name="exceptionIfExists">
  /// True  - генерируется ошибка при попытке создать контейнер с имененем, уже существующим в базе.
  /// False - возвращается ранее созданый контейнер.
  /// </param>
  /// <remarks>В качестве имени контейнера могут выступать любые строковые значения (наприм. тестовые представления GUID и их комбинации),
  /// длиной до 450 символов</remarks>
  /// <returns></returns>
  IParamsStorageObject RegisterObject(string storagеName, bool exceptionIfExists);

  /// <summary>Получение объекта - контейнера по его имени</summary>
  /// <param name="storageName">Имя объекта-контейнера</param>
  /// <returns></returns>
  IParamsStorageObject GetObject(string storageName);

  /// <summary>Удаление объекта - контейнера</summary>
  /// <param name="storageName">Имя объекта-контейнера</param>
  void RemoveObject(string storageName);
}
