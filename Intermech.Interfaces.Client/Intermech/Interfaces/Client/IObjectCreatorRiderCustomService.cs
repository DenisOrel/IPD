// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IObjectCreatorRiderCustomService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Расширение интерфейса IObjectCreatorCustomService</summary>
public interface IObjectCreatorRiderCustomService : IObjectCreatorCustomService
{
  /// <summary>
  /// Вызывать собственный диалог ?
  /// Если здесь вернуть true, то вызовется диалог создания объектов реализованный в функции CreateObjectDialog подписчика
  /// на конкретный тип объектов, если же вернуть false, то вызоветься стандартный диалог создания объекта
  /// с изменениями, реализованными подписчиком (см. функции интерфейса)
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="TemplateObjectID">Идентификатор объекта-прототипа</param>
  /// <param name="RelationTypeIDs">массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="RelatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="StartDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">признак, нужно ли создавать версию объекта</param>
  /// <returns></returns>
  bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion);

  /// <summary>
  /// Метод вызывается сразу-же после создания новой заготовки ДО отображения диалога создания
  /// </summary>
  /// <param name="newObjectID">ID заготовки</param>
  /// <returns></returns>
  bool AfterCreate(long newObjectID);

  /// <summary>
  /// Возвращает коллекцию страниц (наследованные от ObjectCreatorControl), которые будут присутствовать в мастера создания объекта,
  /// значение в коллекции обозначает отображать ли эту страницу в мастере
  /// </summary>
  IDictionary<ObjectCreatePages, bool> VisiblePages { get; }

  /// <summary>
  /// Метод вызывается по нажатию на кнопку готово
  /// Внутри не выводить никаких форм !!!!! Этот метод вызывается внутри транзакции !!!
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="newObjectID">id заготовки</param>
  /// <param name="nea">сида ложить евенты</param>
  /// <returns></returns>
  bool OnCommitAction(IUserSession session, long newObjectID, List<NotificationEventArgs> nea);

  /// <summary>
  /// Метод вызывается по нажатию на кнопку готово перед коммитом для нового объекта (т.е. создаваемый объект еще заготовка)
  /// Внутри не выводить никаких форм !!!!! Этот метод вызывается внутри транзакции !!!
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="newObject">Заготовка</param>
  /// <returns></returns>
  bool OnBeforeCommitAction(IUserSession session, IDBObject newObject);

  /// <summary>
  /// Метод вызывается по нажатию на кнопку отмена
  /// Внутри не выводить никаких форм !!!!! Этот метод вызывается внутри транзакции !!!
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="newObjectID">id заготовки</param>
  /// <param name="nea">Список событий</param>
  /// <returns></returns>
  bool OnCancelAction(IUserSession session, long newObjectID, List<NotificationEventArgs> nea);

  /// <summary>
  /// Добавить в мастер свои страницы (наследованные от ObjectCreatorControl), с порядковым номером
  /// следования в мастере (если -1 добавиться в конец)
  /// </summary>
  /// <param name="CreatedObject"></param>
  /// <param name="propPageIndex"></param>
  /// <returns></returns>
  Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex);
}
