// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IObjectsCheckOutService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис "Навигатора", позволяющий брать на изменение группы объектов
/// </summary>
public interface IObjectsCheckOutService
{
  /// <summary>
  /// Событие, которое вызывается после получения редактируемых копий объектов, для дальнейшего анализа и обработки
  /// </summary>
  event Intermech.Interfaces.Client.ObjectsCheckOutEventHandler ObjectsCheckOutEventHandler;

  /// <summary>
  /// Генерирует событие для дальнейшей обработки редактируемых копий объектов. Используется в тех случаях, когда редактируемые объекты создаются другим сервисов IPS.
  /// </summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  void FireObjectsCheckOutEvent(object sender, ObjectsCheckOutEventArgs e);

  /// <summary>Получить список версий объектов для редактирования</summary>
  /// <param name="session">Сессия, в рамках которой выполняется работа</param>
  /// <param name="versions">Список версий объектов, которые требуется взять на изменение</param>
  /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
  /// <returns>Список версий объектов для редактирования. Если возникла ошибка, будет возвращено значение null</returns>
  IList<long> CheckOut(IUserSession session, IList<long> versions, bool throwException);

  /// <summary>Получить список версий объектов для редактирования</summary>
  /// <param name="session">Сессия, в рамках которой выполняется работа</param>
  /// <param name="versions">Список описаний версий объектов, которые требуется взять на изменение</param>
  /// <param name="throwException">true - при ошибке сгенерировать исключение</param>
  /// <returns>Список версий объектов для редактирования. Если возникла ошибка, будет возвращено значение null</returns>
  IList<long> CheckOut(IUserSession session, IList<IDBObject> versions, bool throwException);
}
