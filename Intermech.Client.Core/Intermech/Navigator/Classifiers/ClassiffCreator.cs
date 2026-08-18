
// Type: Intermech.Navigator.Classifiers.ClassiffCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Classifiers;

/// <summary>Создание классификатора или папки классификатора</summary>
internal sealed class ClassiffCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private IDictionary<ObjectCreatePages, bool> _createPages;
  private int _objectTypeID;
  private long _templateObjectID;
  /// <summary>Подписанные типы объектов</summary>
  private static List<int> _attachedObjectTypes;

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool AcceptDialog(
    int objectTypeID,
    long templateObjectID,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    this._objectTypeID = objectTypeID;
    this._templateObjectID = templateObjectID;
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
            ObjectCreatePages.Classifier,
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
    if (this._templateObjectID != -1L && this._templateObjectID != 0L && (this._objectTypeID == MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545") || this._objectTypeID == MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545")) && MessageBox.Show("Cоздать в составе нового классификатора структуру папок, аналогичную прототипу?", "Создание классификатора по прототипу", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      CompositionCopierTask.BeginCreate(newObjectID, this._templateObjectID);
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
      int num = 0;
      if (createdObject.ObjectTypeID == MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545") || createdObject.ObjectTypeID == MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545"))
      {
        SelectionDialogControl key = new SelectionDialogControl(createdObject);
        dictionary.Add((UserControl) key, num);
        ++num;
      }
      ClassifCreatorControl key1 = new ClassifCreatorControl(createdObject);
      dictionary.Add((UserControl) key1, num);
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
      MetaDataHelper.GetObjectTypeID("cad0014e-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad00150-306c-11d8-b4e9-00304f19f545")
    };
  }

  /// <summary>Зарегистрировать класс</summary>
  public static void Attach(IObjectCreatorService service)
  {
    if (ClassiffCreator._attachedObjectTypes == null)
      ClassiffCreator._attachedObjectTypes = ClassiffCreator.GetAllObjectTypes();
    foreach (int attachedObjectType in ClassiffCreator._attachedObjectTypes)
      service.RegisterCreatorCustomService(attachedObjectType, typeof (ClassiffCreator));
  }
}
