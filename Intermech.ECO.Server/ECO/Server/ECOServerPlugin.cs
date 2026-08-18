// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.ECOServerPlugin
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.ECO;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.ECO.Server;

internal class ECOServerPlugin : IPackage, IConfigurable
{
  public IServiceProvider _serviceProvider;
  public string AnnulName = "";
  public string DelName = "";
  public static readonly string noChangeNumber = "──";

  public void Unload() => ECOServer.ecos._treadStop();

  public string Name => Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server8");

  public void Load(IServiceProvider serviceProvider)
  {
    this._serviceProvider = serviceProvider;
    if (ServerServices.GetService(typeof (IPluginManager)) is IPluginManager service1)
      service1.LoadComplete += new EventHandler(this.pluginManager_LoadComplete);
    ECOServer.ecos._serviceProvider = serviceProvider;
    IEventLogHelper service2 = this._serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    ECOServer.ecos._iLogH = service2;
    ECOServer._idbTE = this._serviceProvider.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    ECOServer.ecos.Init();
    RevisionComplect.Load(this._serviceProvider);
    ICreatorContainer service3 = this._serviceProvider.GetService(typeof (IDBObjectService)) as ICreatorContainer;
    service3.AddCreator((object) new Guid(ECOServer.ECO_Guid), (object) new ECORootCreator(), true);
    foreach (Guid creatorType in MetaDataHelper.GetObjectTypeChildrenGuidRecursive(new Guid(ECOServer.ECO_II)))
      service3.AddCreator((object) creatorType, (object) new II_ObjCreator(), true);
    foreach (Guid creatorType in MetaDataHelper.GetObjectTypeChildrenGuidRecursive(new Guid(ECOServer.ECO_PR)))
      service3.AddCreator((object) creatorType, (object) new PR_ObjCreator(), true);
    foreach (Guid creatorType in MetaDataHelper.GetObjectTypeChildrenGuidRecursive(new Guid(ECOServer.ECO_PI)))
      service3.AddCreator((object) creatorType, (object) new PI_ObjCreator(), true);
    foreach (Guid creatorType in MetaDataHelper.GetObjectTypeChildrenGuidRecursive(new Guid(ECOServer.ECO_SN)))
      service3.AddCreator((object) creatorType, (object) new PI_ObjCreator(), true);
    Dop_ObjCreator creatorInstance1 = new Dop_ObjCreator();
    service3.AddCreator((object) new Guid(ECOServer.guidObj_DI), (object) creatorInstance1, true);
    service3.AddCreator((object) new Guid(ECOServer.guidObj_DPI), (object) creatorInstance1, true);
    service3.AddCreator((object) new Guid(ECOServer.guidObj_CJ), (object) new CJ_ObjCreator(), true);
    service3.AddCreator((object) new Guid(ECOServer.guidObjCJRecord), (object) new CJRec_ObjCreator(), true);
    ICreatorContainer service4 = this._serviceProvider.GetService(typeof (IDBRelationService)) as ICreatorContainer;
    IDBRelationCreator dbRelationCreator = (IDBRelationCreator) new RevRelationCreator();
    // ISSUE: variable of a boxed type
    __Boxed<Guid> creatorType1 = (ValueType) new Guid("cad0036b-306c-11d8-b4e9-00304f19f545");
    IDBRelationCreator creatorInstance2 = dbRelationCreator;
    service4.AddCreator((object) creatorType1, (object) creatorInstance2, true);
    ICustomServices service5 = this._serviceProvider.GetService(typeof (ICustomServices)) as ICustomServices;
    service5.AddService(typeof (IECOServer), (object) ECOServer.ecos);
    ServerServices.AddService(typeof (IECOServer), (object) ECOServer.ecos);
    this.AnnulName = Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server5");
    this.DelName = Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server6");
    if (this._serviceProvider.GetService(typeof (IEventLogHelper)) is IEventLogHelper service6)
      service6.AddAttributeWriteHandler((object) new Guid("cad00770-306c-11d8-b4e9-00304f19f545"), new WriteAttributeValueHandler(this._SetChangeNoAttr));
    if (service5.GetService(typeof (IObjectsDeleteAnalyzerService)) is IObjectsDeleteAnalyzerService service7)
      service7.RegisterAnalyzer((IObjectsDeleteAnalyzer) new ECOObjectsDeleteAnalyzer());
    if (serviceProvider.GetService(typeof (IEventLogHelper)) is IEventLogHelper service8)
      service8.AfterCreateObjectEvent += new AfterCreateObjectHandler(this.eventLogHelper_AfterCreateObjectEvent);
    INotifySubscriberService service9 = serviceProvider.GetService<INotifySubscriberService>(false);
    if (service9 != null)
      service9.GetEcoDocumentsListEvent += new GetEcoDocumentsHandler(ECOServer.ecos.notifSS_GetEcoDocumentsListEvent);
    if (!(ServerServices.GetService(typeof (IPortalEventsService)) is IPortalEventsService service10))
      return;
    service10.ReadImportedObjectAttributesEvent += new ReadImportedObjectAttributesEventHandler(this.PortalEvents_ReadImportedObjectAttributesEvent);
  }

  private void PortalEvents_ReadImportedObjectAttributesEvent(
    object sender,
    ReadImportedObjectAttributesEventArgs e)
  {
    XmlNode node = e.RootNode.SelectSingleNode("SYSATTRIBUTE");
    if (node == null)
      return;
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(ECOServer.attrChangeDate);
    XmlAttribute nodeAttribute1 = this.GetNodeAttribute(node, e.Object.Attributes, "F_START_DATE", attributeTypeId1);
    DateTime result;
    if (nodeAttribute1 != null && DateTime.TryParse(nodeAttribute1.Value, out result))
    {
      AttributeRecord attributeRecord = new AttributeRecord(attributeTypeId1)
      {
        DateValue = (object) result
      };
      e.Object.Attributes.Add(attributeRecord);
    }
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(ECOServer.guidattrChangeDateEnd);
    XmlAttribute nodeAttribute2 = this.GetNodeAttribute(node, e.Object.Attributes, "F_FINISH_DATE", attributeTypeId2);
    if (nodeAttribute2 == null || !DateTime.TryParse(nodeAttribute2.Value, out result))
      return;
    AttributeRecord attributeRecord1 = new AttributeRecord(attributeTypeId2)
    {
      DateValue = (object) result
    };
    e.Object.Attributes.Add(attributeRecord1);
  }

  private XmlAttribute GetNodeAttribute(
    XmlNode node,
    List<AttributeRecord> attributesCollection,
    string attributeName,
    int attributeID)
  {
    XmlAttribute attribute = node.Attributes[attributeName];
    return attribute != null && attribute.Value != string.Empty && !attributesCollection.Exists((Predicate<AttributeRecord>) (x => x.AttributeId.Equals(attributeID))) ? attribute : (XmlAttribute) null;
  }

  private void eventLogHelper_AfterCreateObjectEvent(
    IDBObject newobject,
    IDBObject prototype,
    IUserSession session)
  {
    IDBAttribute attributeById1 = newobject.GetAttributeByID(ECOServer.ecos.attrChangeDateId);
    if (attributeById1 != null && attributeById1.Value != DBNull.Value)
      attributeById1.Value = (object) DBNull.Value;
    IDBAttribute attributeById2 = newobject.GetAttributeByID(ECOServer.ecos.attrChangeDateEndId);
    if (attributeById2 == null || attributeById2.Value == DBNull.Value)
      return;
    attributeById2.Value = (object) DBNull.Value;
  }

  private void _SetChangeNoAttr(IDBAttribute idbA, AttributeValueEventArgs e)
  {
    if (!(idbA is DBAttribute dbAttribute) || !dbAttribute.IsObjectAttribute)
      return;
    string str = e.Value.ToString();
    if (str == ECOServerPlugin.noChangeNumber)
      return;
    if ((dbAttribute.ParentObject.AttributesState & Consts.CreateMode) == Consts.CreateMode)
    {
      e.NewValue = (object) DBNull.Value;
    }
    else
    {
      long dbObjectId = idbA.DBObjectID;
      IUserSession session = idbA.Session;
      List<long> objectIdVersions = session.GetObjectIDVersions(dbObjectId);
      if (objectIdVersions.Count <= 1)
        return;
      foreach (long objectID in objectIdVersions)
      {
        if (Math.Abs(objectID) != Math.Abs(dbObjectId))
        {
          IDBObject dbObject = session.GetObject(objectID, false);
          if (dbObject == null)
            break;
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null && attributeByGuid.Value != DBNull.Value && attributeByGuid.AsString != string.Empty && attributeByGuid.AsString == str)
            throw new Exception(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("ECO_Server7"), (object) str, (object) dbObject.ObjectID));
        }
      }
    }
  }

  private void pluginManager_LoadComplete(object sender, EventArgs e)
  {
    ICustomServices service1 = ServerServices.GetService(typeof (ICustomServices)) as ICustomServices;
    IDBTimedEvents service2 = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    UserSession sessionTemporaryClone = service2.GetSystemSessionTemporaryClone("RevActivateTask") as UserSession;
    try
    {
      LinkIzvObject.Init((IUserSession) sessionTemporaryClone);
      ECOObject.InitECOObject((IUserSession) sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone.Logout("RevActivateTask");
    }
    RevActivateTask timedService = new RevActivateTask(ECOServer.ecos);
    service2.RegisterService((object) timedService);
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Open("ECOServer");
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Create("ECOServer");
  }
}
