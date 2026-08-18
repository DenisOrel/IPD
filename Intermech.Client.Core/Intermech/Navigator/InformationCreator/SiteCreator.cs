
// Type: Intermech.Navigator.InformationCreator.SiteCreator
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


namespace Intermech.Navigator.InformationCreator;

/// <summary>мастер создания объекта узел информационной системы</summary>
internal class SiteCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private SiteCreatorStepOne ics1;
  private SiteCreatorStepTwo ics2;
  private static List<int> _attachedObjectTypes = new List<int>();
  private IDictionary<ObjectCreatePages, bool> _createPages;

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

  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    this.ics2.DeleteUsers();
    return true;
  }

  public bool OnCommitAction(
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
      this.ics1 = new SiteCreatorStepOne(createdObject);
      dictionary.Add((UserControl) this.ics1, 0);
      this.ics2 = new SiteCreatorStepTwo(createdObject);
      dictionary.Add((UserControl) this.ics2, 1);
    }
    return dictionary.Count > 0 ? dictionary : (Dictionary<UserControl, int>) null;
  }

  public bool AfterCreate(long newObjectID) => true;

  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      if (this._createPages == null)
        this._createPages = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>();
      return this._createPages;
    }
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
      (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(new Guid("cad0148c-306c-11d8-b4e9-00304f19f545"), true).ObjectType
    };
  }

  /// <summary>Зарегистрировать класс</summary>
  /// <param name="service"></param>
  public static void Attach(IObjectCreatorService service)
  {
    SiteCreator._attachedObjectTypes = SiteCreator.GetAllObjectTypes();
    foreach (int attachedObjectType in SiteCreator._attachedObjectTypes)
      service.RegisterCreatorCustomService(attachedObjectType, typeof (SiteCreator));
  }

  /// <summary>Разрегистрировать класс</summary>
  /// <param name="service"></param>
  public static void Detach(IObjectCreatorService service)
  {
    if (SiteCreator._attachedObjectTypes == null || SiteCreator._attachedObjectTypes.Count <= 0)
      return;
    foreach (int attachedObjectType in SiteCreator._attachedObjectTypes)
      service.UnregisterCreatorCustomService(attachedObjectType, typeof (SiteCreator));
  }
}
