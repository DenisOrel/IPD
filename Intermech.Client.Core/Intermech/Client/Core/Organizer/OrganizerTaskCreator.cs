
// Type: Intermech.Client.Core.Organizer.OrganizerTaskCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// Создатель для объектов типа "Задачи органайзера" стандартного создателя объектов.
/// </summary>
public class OrganizerTaskCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  /// <summary>Набор страниц мастера создания объектов.</summary>
  private IDictionary<ObjectCreatePages, bool> _pages;
  private OrganizerTaskCtrl _ctrl;

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  /// <summary>Вызов собственного диалога.</summary>
  /// <param name="objTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="templateObjID">Идентификатор объекта-прототипа</param>
  /// <param name="relTypeIDs">Массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="relatedObjIDs">Массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="startDate">Время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">Признак, нужно ли создавать версию объекта</param>
  /// <returns></returns>
  public bool AcceptDialog(
    int objTypeID,
    long templateObjID,
    int[] relTypeIDs,
    long[] relatedObjIDs,
    DateTime startDate,
    bool isVersion)
  {
    return false;
  }

  /// <summary>
  /// Метод вызывается после создания новой заготовки ДО отображения диалога создания.
  /// </summary>
  /// <param name="newObjID">ID заготовки</param>
  /// <returns></returns>
  public bool AfterCreate(long newObjID) => true;

  /// <summary>
  /// Возвращает коллекцию страниц (которые будут присутствовать в мастера создания объекта),
  /// значение в коллекции обозначает отображать ли эту страницу в мастере.
  /// </summary>
  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      if (this._pages != null)
      {
        this._pages = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>();
        this._pages.Add(ObjectCreatePages.Properties, true);
        this._pages.Add(ObjectCreatePages.Template, true);
      }
      return this._pages;
    }
  }

  /// <summary>
  /// Метод вызывается по нажатию на кнопку, вызывается внутри транзакции.
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="newObjID">ID заготовки</param>
  /// <param name="nea">Список событий</param>
  /// <returns></returns>
  public bool OnCommitAction(IUserSession session, long newObjID, List<NotificationEventArgs> nea)
  {
    bool flag = false;
    if (session != null && newObjID != 0L)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(newObjID, false);
      if (objectActualCopy != null)
      {
        IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad00628-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null && attributeByGuid.Values != null)
        {
          int relationTypeId = MetaDataHelper.GetRelationTypeID("cadd938e-306c-11d8-b4e9-00304f19f545");
          if (relationTypeId != 0)
          {
            IDBRelationCollection relationCollection = session.GetRelationCollection(relationTypeId);
            if (relationCollection != null)
            {
              try
              {
                List<long> longList = new List<long>();
                foreach (object obj in attributeByGuid.Values)
                {
                  if (obj != null && obj != DBNull.Value)
                  {
                    long int64 = Convert.ToInt64(obj);
                    if (!longList.Contains(int64))
                    {
                      longList.Add(int64);
                      this.SetAttributesValues(relationCollection.Create(newObjID, int64));
                    }
                  }
                }
                long ownerId = objectActualCopy.OwnerID;
                if (!longList.Contains(ownerId))
                  this.SetAttributesValues(relationCollection.Create(newObjID, objectActualCopy.OwnerID));
                flag = true;
              }
              catch (Exception ex)
              {
                ExceptionHelper.ExceptionService.ShowException(ex);
              }
            }
          }
        }
      }
    }
    return flag;
  }

  /// <summary>
  /// Метод вызывается по нажатию на кнопку отмена, вызывается внутри транзакции.
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="newObjID">ID заготовки</param>
  /// <param name="nea">Список событий</param>
  /// <returns></returns>
  public bool OnCancelAction(IUserSession session, long newObjID, List<NotificationEventArgs> nea)
  {
    return true;
  }

  /// <summary>
  /// Добавить в мастер свои страницы, с порядковым номером следования в мастере (если -1 добавиться в конец).
  /// </summary>
  /// <param name="createdObj"></param>
  /// <param name="propPageIndex"></param>
  /// <returns></returns>
  public Dictionary<UserControl, int> AddPages(object createdObj, int propPageIndex)
  {
    if (!(createdObj is CreatedObjectItem createdObjectItem))
      return (Dictionary<UserControl, int>) null;
    Dictionary<UserControl, int> dictionary = new Dictionary<UserControl, int>(1);
    this._ctrl = new OrganizerTaskCtrl(createdObjectItem.ObjectID);
    dictionary.Add((UserControl) this._ctrl, 0);
    return dictionary;
  }

  /// <summary>
  /// Вызов диалога создания нового объекта (по прототипу) c созданием заданных связей с указанными объектами.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="templateObjID">Идентификатор объекта-прототипа</param>
  /// <param name="relationTypeIDs">Массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="relatedObjIDs">Массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="startDate">Время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">Признак, нужно ли создавать версию объекта</param>
  /// <returns>Идентификатор созданного объекта</returns>
  public long CreateObjectDialog(
    int objTypeID,
    long templateObjID,
    int[] relationTypeIDs,
    long[] relatedObjIDs,
    DateTime startDate,
    bool isVersion)
  {
    return -1;
  }

  /// <summary>Заполнение значений атрибутов для созданной связи.</summary>
  /// <param name="relation">Созданная связь</param>
  private void SetAttributesValues(IDBRelation relation)
  {
    if (relation == null)
      return;
    IDBAttribute attributeByGuid1 = relation.GetAttributeByGuid(new Guid("cad015d5-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 != null)
      attributeByGuid1.Value = (object) this._ctrl.Reminder;
    IDBAttribute attributeByGuid2 = relation.GetAttributeByGuid(new Guid("cad015d4-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid2 == null)
      return;
    attributeByGuid2.Value = this._ctrl.DateReminder;
  }
}
