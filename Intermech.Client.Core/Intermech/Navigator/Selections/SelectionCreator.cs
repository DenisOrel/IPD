
// Type: Intermech.Navigator.Selections.SelectionCreator
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


namespace Intermech.Navigator.Selections;

internal class SelectionCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private IDictionary<ObjectCreatePages, bool> _createPages;
  /// <summary>Подписанные типы объектов</summary>
  private static List<int> _attachedObjectTypes;

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

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

  public bool AfterCreate(long newObjectID) => true;

  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      if (this._createPages == null)
        this._createPages = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>()
        {
          {
            ObjectCreatePages.Properties,
            true
          },
          {
            ObjectCreatePages.Relations,
            true
          },
          {
            ObjectCreatePages.Template,
            true
          }
        };
      return this._createPages;
    }
  }

  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
  {
    Dictionary<UserControl, int> dictionary = new Dictionary<UserControl, int>();
    if (CreatedObject is CreatedObjectItem createdObject)
    {
      if (createdObject.ObjectTypeID != MetaDataHelper.GetObjectTypeID("cadd96c2-306c-11d8-b4e9-00304f19f545"))
      {
        SelectionDialogControl key = new SelectionDialogControl(createdObject);
        dictionary.Add((UserControl) key, 0);
      }
      SelectionCreatorControl key1 = new SelectionCreatorControl(createdObject);
      dictionary.Add((UserControl) key1, dictionary.Count > 0 ? 1 : 0);
    }
    return dictionary.Count > 0 ? dictionary : (Dictionary<UserControl, int>) null;
  }

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

  private static List<int> GetAllObjectTypes()
  {
    return new List<int>()
    {
      MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545")
    };
  }

  /// <summary>Зарегистрировать класс</summary>
  public static void Attach(IObjectCreatorService service)
  {
    if (SelectionCreator._attachedObjectTypes == null)
      SelectionCreator._attachedObjectTypes = SelectionCreator.GetAllObjectTypes();
    foreach (int attachedObjectType in SelectionCreator._attachedObjectTypes)
      service.RegisterCreatorCustomService(attachedObjectType, typeof (SelectionCreator));
  }

  /// <summary>Разрегистрировать класс</summary>
  public static void Detach(IObjectCreatorService service)
  {
    if (SelectionCreator._attachedObjectTypes == null || SelectionCreator._attachedObjectTypes.Count <= 0)
      return;
    foreach (int attachedObjectType in SelectionCreator._attachedObjectTypes)
      service.UnregisterCreatorCustomService(attachedObjectType, typeof (SelectionCreator));
  }
}
