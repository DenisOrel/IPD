
// Type: Intermech.Navigator.Selections.ObjectTemplateCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections;

internal class ObjectTemplateCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private IDictionary<ObjectCreatePages, bool> _createPages;
  /// <summary>Подписанные типы объектов</summary>
  private static List<int> _attachedObjectTypes;
  private static ObjectPropertiesControl opc;

  public void AttributeObjectTypeGuidChange(object sender, PropertyValueChangedEventArgs e)
  {
    string name = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"), true).Name;
    if (!e.ChangedItem.PropertyDescriptor.Name.Equals(name))
      return;
    ObjectTemplateCreator.opc.Save(new PageSaveArgs(-1));
    ObjectTemplateCreator.opc.Refresh(new PageRefreshArgs());
  }

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
      {
        this._createPages = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>();
        this._createPages.Add(ObjectCreatePages.Relations, true);
      }
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
      ObjectTemplateCreator.opc = new ObjectPropertiesControl(createdObject);
      ObjectTemplateCreator.opc.PropertyValueChangedEvent += new PropertyValueChangedHendler(this.AttributeObjectTypeGuidChange);
      dictionary.Add((UserControl) ObjectTemplateCreator.opc, 0);
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
      (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(new Guid("cad00822-306c-11d8-b4e9-00304f19f545"), true).ObjectType
    };
  }

  /// <summary>Зарегистрировать класс</summary>
  /// <param name="service"> Служба, позволяющая создавать новые объекты</param>
  public static void Attach(IObjectCreatorService service)
  {
    if (ObjectTemplateCreator._attachedObjectTypes == null)
      ObjectTemplateCreator._attachedObjectTypes = ObjectTemplateCreator.GetAllObjectTypes();
    foreach (int attachedObjectType in ObjectTemplateCreator._attachedObjectTypes)
      service.RegisterCreatorCustomService(attachedObjectType, typeof (ObjectTemplateCreator));
  }

  /// <summary>Разрегистрировать класс</summary>
  /// <param name="service">Служба, позволяющая создавать новые объекты</param>
  public static void Detach(IObjectCreatorService service)
  {
    if (ObjectTemplateCreator._attachedObjectTypes == null || ObjectTemplateCreator._attachedObjectTypes.Count <= 0)
      return;
    foreach (int attachedObjectType in ObjectTemplateCreator._attachedObjectTypes)
      service.UnregisterCreatorCustomService(attachedObjectType, typeof (ObjectTemplateCreator));
  }
}
