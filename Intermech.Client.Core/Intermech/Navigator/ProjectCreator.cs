
// Type: Intermech.Navigator.ProjectCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Projects;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator;

internal class ProjectCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private IDictionary<ObjectCreatePages, bool> _createPages;
  /// <summary>Подписанные типы объектов</summary>
  private static List<int> _attachedObjectTypes;
  private CreatedObjectItem createdObject;
  /// <summary>Форма для управления участниками проектов</summary>
  private ProjectTeamsForm teamsForm;
  /// <summary>Нужна ли в креаторе страница с классификатором</summary>
  private bool _isClassified;

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject containerForObjectType = (sessionKeeper.Session.GetCustomService(typeof (IContainerService)) as IContainerService).GetContainerForObjectType((object) sessionKeeper.Session.SessionGUID, ObjectTypeID);
      if (containerForObjectType == null)
        return false;
      IDBAttribute attributeByGuid = containerForObjectType.GetAttributeByGuid(new Guid("cad001d9-306c-11d8-b4e9-00304f19f545"));
      this._isClassified = attributeByGuid != null && Convert.ToInt32(attributeByGuid.Value) > 0;
    }
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
        if (this._isClassified)
          this._createPages.Add(ObjectCreatePages.Classifier, true);
        this._createPages.Add(ObjectCreatePages.Properties, true);
        this._createPages.Add(ObjectCreatePages.Template, true);
      }
      return this._createPages;
    }
  }

  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBProjectObject dbProjectObject = sessionKeeper.Session.GetObject(newObjectID) as IDBProjectObject;
      if (this.teamsForm.Participant != null)
      {
        List<ProjectParticipantInfo> projectParticipantInfoList = new List<ProjectParticipantInfo>((IEnumerable<ProjectParticipantInfo>) this.teamsForm.Participant);
        projectParticipantInfoList.Remove(new ProjectParticipantInfo((dbProjectObject as IDBObject).OwnerID, false));
        dbProjectObject.IncludeParticipants(projectParticipantInfoList.ToArray());
      }
    }
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
    this.createdObject = CreatedObject as CreatedObjectItem;
    if (this.createdObject != null)
    {
      ProjectTeamsViewControl teamsViewControl = new ProjectTeamsViewControl(this.createdObject);
      if (this.teamsForm == null)
      {
        this.teamsForm = new ProjectTeamsForm();
        this.teamsForm.ObjectID = this.createdObject.ObjectID;
        this.teamsForm.SetParent((Control) teamsViewControl);
        this.teamsForm.LoadViewData();
      }
      dictionary.Add((UserControl) teamsViewControl, 2);
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
      (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(new Guid("cad00812-306c-11d8-b4e9-00304f19f545"), true).ObjectType
    };
  }

  /// <summary>Зарегистрировать класс</summary>
  /// <param name="service"> Служба, позволяющая создавать новые объекты</param>
  public static void Attach(IObjectCreatorService service)
  {
    if (ProjectCreator._attachedObjectTypes == null)
      ProjectCreator._attachedObjectTypes = ProjectCreator.GetAllObjectTypes();
    foreach (int attachedObjectType in ProjectCreator._attachedObjectTypes)
      service.RegisterCreatorCustomService(attachedObjectType, typeof (ProjectCreator));
  }

  /// <summary>Разрегистрировать класс</summary>
  /// <param name="service">Служба, позволяющая создавать новые объекты</param>
  public static void Detach(IObjectCreatorService service)
  {
    if (ProjectCreator._attachedObjectTypes == null || ProjectCreator._attachedObjectTypes.Count <= 0)
      return;
    foreach (int attachedObjectType in ProjectCreator._attachedObjectTypes)
      service.UnregisterCreatorCustomService(attachedObjectType, typeof (ProjectCreator));
  }
}
