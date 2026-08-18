
// Type: Intermech.Client.Core.UserCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Служба для создания объектов типа "Пользователь"</summary>
internal class UserCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  /// <summary>Форма для редактирования списка ролей пользователя</summary>
  private UserToRolesForm _editorForm;
  /// <summary>
  /// Список типов объектов, на которые "подписан" создатель
  /// </summary>
  private static List<int> _attachedObjectTypes;

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

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
  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return false;
  }

  /// <summary>
  /// Метод вызывается сразу после создания новой заготовки ДО отображения диалога создания
  /// </summary>
  /// <param name="newObjectID">ID заготовки</param>
  /// <returns></returns>
  public bool AfterCreate(long newObjectID)
  {
    if (this._editorForm == null)
    {
      this._editorForm = new UserToRolesForm();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._editorForm.ObjectID = newObjectID;
        this._editorForm.ObjectName = sessionKeeper.Session.GetObjectInfo(newObjectID).Caption;
        ArrayList roles = new ArrayList();
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
        if (relationCollection != null)
        {
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(-7, RelationalOperators.Equal, (object) sessionKeeper.Session.IdentHelper.RolesTypeID, LogicalOperators.NONE, 0, true)
          }, new ColumnDescriptor[3]
          {
            new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
            new ColumnDescriptor((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
          });
          foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersIn(paramSet, newObjectID).Rows)
            roles.Add((object) new UserToRoles(Convert.ToInt64(row[1]), Convert.ToString(row[2]), Convert.ToInt64(row[0])));
          this._editorForm.IconAsByteArray = sessionKeeper.Session.GetObjectType(new Guid("cad00007-306c-11d8-b4e9-00304f19f545")).Icon;
        }
        this._editorForm.LoadObjectData(roles, sessionKeeper.Session.IsAdmin);
      }
    }
    return true;
  }

  /// <summary>
  /// Возвращает коллекцию страниц (наследованные от ObjectCreatorControl),
  /// которые будут присутствовать в мастера создания объекта, значение в коллекции
  /// обозначает отображать ли эту страницу в мастере
  /// </summary>
  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get => (IDictionary<ObjectCreatePages, bool>) null;
  }

  /// <summary>
  /// Метод вызывается по нажатию на кнопку готово
  /// Внутри не выводить никаких форм !!!!! Этот метод вызывается внутри транзакции !!!
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="newObjectID">id заготовки</param>
  /// <param name="nea">В список размещать события</param>
  /// <returns></returns>
  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    this._editorForm.ObjectID = newObjectID;
    this._editorForm.SaveObjectData();
    return true;
  }

  /// <summary>
  /// Метод вызывается по нажатию на кнопку отмена
  /// Внутри не выводить никаких форм !!!!! Этот метод вызывается внутри транзакции !!!
  /// </summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="newObjectID">id заготовки</param>
  /// <param name="nea">В список размещать события</param>
  /// <returns></returns>
  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  /// <summary>
  /// Добавить в мастер свои страницы (наследованные от ObjectCreatorControl), с порядковым номером
  /// следования в мастере (если -1 добавиться в конец)
  /// </summary>
  /// <param name="CreatedObject"></param>
  /// <param name="propPageIndex"></param>
  /// <returns></returns>
  public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
  {
    Dictionary<UserControl, int> dictionary = new Dictionary<UserControl, int>();
    if (CreatedObject is CreatedObjectItem createdObject && MetaDataHelper.IsObjectTypeChildOf(createdObject.ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")))
    {
      UserCreatorControl userCreatorControl = new UserCreatorControl(createdObject);
      this._editorForm.SetParent((Control) userCreatorControl);
      dictionary.Add((UserControl) userCreatorControl, 0);
    }
    return dictionary.Count > 0 ? dictionary : (Dictionary<UserControl, int>) null;
  }

  /// <summary>
  /// Вызов диалога создания нового объекта (по прототипу) c созданием заданных связей с указанными объектами
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="TemplateObjectID">Идентификатор объекта-прототипа</param>
  /// <param name="RelationTypeIDs">массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="RelatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="StartDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">признак, нужно ли создавать версию объекта</param>
  /// <returns>Идентификатор созданного объекта</returns>
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return -1;
  }

  /// <summary>
  /// Получить список типов объектов, связанных с создателем
  /// </summary>
  /// <returns></returns>
  private static List<int> GetAllObjectTypes()
  {
    return MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"));
  }

  /// <summary>Зарегистрировать создателя</summary>
  /// <param name="service">Сервис по созданию новых объектов</param>
  public static void Attach(IObjectCreatorService service)
  {
    if (UserCreator._attachedObjectTypes == null)
      UserCreator._attachedObjectTypes = UserCreator.GetAllObjectTypes();
    foreach (int attachedObjectType in UserCreator._attachedObjectTypes)
      service.RegisterCreatorCustomService(attachedObjectType, typeof (UserCreator));
  }

  /// <summary>Разрегистрировать создателя</summary>
  /// <param name="service">Сервис по созданию новых объектов</param>
  public static void Detach(IObjectCreatorService service)
  {
    if (UserCreator._attachedObjectTypes == null || UserCreator._attachedObjectTypes.Count <= 0)
      return;
    foreach (int attachedObjectType in UserCreator._attachedObjectTypes)
      service.UnregisterCreatorCustomService(attachedObjectType, typeof (UserCreator));
  }
}
