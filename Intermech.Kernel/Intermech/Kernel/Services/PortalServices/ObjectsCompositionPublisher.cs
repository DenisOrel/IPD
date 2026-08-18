// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ObjectsCompositionPublisher
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

public class ObjectsCompositionPublisher : Publisher
{
  protected PublishComposition composition;
  protected ExtendedPublishOptions options;
  private readonly Dictionary<int, List<int>> _freeAttributesCache;
  private readonly IPublishTypesConfiguration _typesConfiguration;

  public ObjectsCompositionPublisher(
    PublishComposition composition,
    ExtendedPublishOptions options,
    PublishType publishType)
    : base(publishType)
  {
    this.composition = composition;
    this.options = options;
    this._freeAttributesCache = new Dictionary<int, List<int>>();
    this._typesConfiguration = ApplicationServices.Container.GetService<IPublishTypesConfiguration>(false);
  }

  private ITransferedObject CreateTransferedObject(
    IUserSession session,
    PublishCompositionObject pco,
    ISitesCacheService cacheService,
    IBackupWriter writer,
    IDBObject obj)
  {
    bool isLink = pco.Include == IncludeTypes.ObjectLink;
    ObjectTag objectTag = new ObjectTag()
    {
      InComposition = !pco.Root
    };
    objectTag.WithComposition = this.options.CountLevels < 0 || this.options.CountLevels > 1 || this.options.CountLevels != 0 && pco.Root;
    objectTag.EnableSites = pco.EnableSites;
    this.WriteCodes(objectTag, cacheService.Info.Code, obj);
    if (pco.Include == IncludeTypes.FCAttributesOnly || pco.Include == IncludeTypes.FCFileAttributesOnly)
    {
      List<int> attributes;
      Helper.FreeChangeAttributesPresent(session, obj, out attributes, pco.Include == IncludeTypes.FCFileAttributesOnly);
      ExtendedTransferedObject unit = new ExtendedTransferedObject(ChangeType.ctCreate, TransferedObjectCategory.AttributesContainer, (TransferedObjectTag) objectTag);
      new RedLineXMLFileFormer(session, unit, writer, obj, attributes, new Attributes4ObjectTag(PublishObjectRootType.rtUnknown, pco.LinkedGuid), objectTag).SaveAttributes();
      return (ITransferedObject) unit;
    }
    List<Guid> objectTypeParentsGuid = MetaDataHelper.GetObjectTypeParentsGuid(MetaDataHelper.GetObjectTypeGuid(obj.ObjectType));
    objectTag.RootType = objectTypeParentsGuid.Count <= 0 ? PublishObjectRootType.rtUnknown : (objectTypeParentsGuid[objectTypeParentsGuid.Count - 1] == new Guid("cad00268-306c-11d8-b4e9-00304f19f545") || objectTypeParentsGuid[objectTypeParentsGuid.Count - 1] == new Guid("cad00170-306c-11d8-b4e9-00304f19f545") ? PublishObjectRootType.rtArticle : (!(objectTypeParentsGuid[objectTypeParentsGuid.Count - 1] == new Guid("cad00070-306c-11d8-b4e9-00304f19f545")) ? PublishObjectRootType.rtUnknown : PublishObjectRootType.rtDocument));
    ITransferedObject transferedObject = this.CheckTransferedObject(session, obj, pco, objectTag, writer, isLink);
    this.AfterObjectPack(obj, pco);
    return transferedObject;
  }

  private ITransferedObject CheckTransferedObject(
    IUserSession session,
    IDBObject obj,
    PublishCompositionObject pco,
    ObjectTag tag,
    IBackupWriter writer,
    bool isLink)
  {
    IDBLifecycleStep lifecycleStep = session.GetLifecycleStep(obj.LCStep);
    if ((lifecycleStep.ObjectModifyMode == ObjectModifyModes.CantModify ? 1 : (lifecycleStep.ObjectModifyMode == ObjectModifyModes.CreateVersion ? 1 : 0)) != 0 && !MetaDataHelper.IsObjectTypeEditingContext(obj.ObjectType))
      return (ITransferedObject) new PersistentObject(obj.ObjectID, pco.LinkedGuid, isLink, tag);
    ExtendedTransferedObject unit = new ExtendedTransferedObject(ChangeType.ctCreate, isLink ? TransferedObjectCategory.ObjectLink : TransferedObjectCategory.Object, (TransferedObjectTag) tag);
    this.GetObjectXMLFileFormer(session, unit, writer, obj, new Attributes4ObjectTag(tag.RootType, pco.LinkedGuid)).SaveAttributes();
    return (ITransferedObject) unit;
  }

  public override ITransferedObject[] Pack(IUserSession session, IBackupWriter writer)
  {
    IIDLinkTranslate customService1 = (IIDLinkTranslate) session.GetCustomService(typeof (IIDLinkTranslate));
    ISitesCacheService customService2 = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    ObjectAnalyzerHelper.GetAnalyzer(session, this.publishType, this.options, this._freeAttributesCache);
    List<ITransferedObject> transObjs = new List<ITransferedObject>();
    this.BeforeCompositionPack(session, customService2.Info, writer, transObjs);
    List<PublishCompositionObject> compositionObjectList = new List<PublishCompositionObject>();
    List<IDBObject> dbObjectList = new List<IDBObject>();
    List<Tuple<long, long>> tupleList1 = new List<Tuple<long, long>>();
    List<Tuple<long, long, string>> tupleList2 = new List<Tuple<long, long, string>>();
    List<long> longList = new List<long>();
    foreach (PublishCompositionObject compositionObject1 in this.composition.Objects)
    {
      PublishCompositionObject pco = compositionObject1;
      IDBObject dbObject = session.GetObject(pco.ObjectID, true);
      dbObjectList.Add(dbObject);
      if (pco.Include == IncludeTypes.FCAttributesOnly || pco.Include == IncludeTypes.FCFileAttributesOnly)
      {
        if (!this.FCAttributesOnlyEnable && SiteIDHelper.IsOwner(customService2.Info.Code, dbObject.SiteID))
        {
          compositionObjectList.Add((PublishCompositionObject) pco.Clone());
          pco.Include = IncludeTypes.Include;
        }
      }
      else if (pco.Include == IncludeTypes.Include)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(session.IdentHelper.FileAttributeID);
        if (attributeById != null)
        {
          for (int index = 0; index < attributeById.ValuesCount; ++index)
          {
            attributeById.Index = index;
            if (!string.IsNullOrEmpty(attributeById.AsString))
            {
              FileInfo fileInfo = new FileInfo(attributeById.AsString);
              BlobInformation blobInformation = (attributeById as IBlobReader).OpenBlob(-1);
              if ((!string.IsNullOrEmpty(fileInfo.Extension) && fileInfo.Extension.ToLower() == ".rxml" || blobInformation.FileType == FileTypes.ftNotContent || blobInformation.FileType == FileTypes.ftRedlining) && !compositionObjectList.Exists((Predicate<PublishCompositionObject>) (_ => _.ObjectID == pco.ObjectID)))
              {
                PublishCompositionObject compositionObject2 = (PublishCompositionObject) pco.Clone();
                compositionObject2.Include = IncludeTypes.FCFileAttributesOnly;
                compositionObjectList.Add(compositionObject2);
                break;
              }
            }
          }
        }
      }
      ITransferedObject transferedObject = this.CreateTransferedObject(session, pco, customService2, writer, dbObject);
      tupleList2.Add(new Tuple<long, long, string>(dbObject.ObjectID, dbObject.ID, transferedObject.GUID));
      transObjs.Add(transferedObject);
    }
    if (compositionObjectList.Count > 0)
    {
      foreach (PublishCompositionObject compositionObject in compositionObjectList)
      {
        PublishCompositionObject pco = compositionObject;
        IDBObject dbObject = dbObjectList.Find((Predicate<IDBObject>) (x => x.ObjectID == pco.ObjectID)) ?? session.GetObject(pco.ObjectID, true);
        ITransferedObject transferedObject = this.CreateTransferedObject(session, pco, customService2, writer, dbObject);
        tupleList2.Add(new Tuple<long, long, string>(dbObject.ObjectID, dbObject.ID, transferedObject.GUID));
        transObjs.Add(transferedObject);
      }
    }
    dbObjectList.Clear();
    foreach (PublishCompositionRelation relation in this.composition.Relations)
    {
      IDBRelation rel = session.GetRelation(relation.PrjLinkID);
      Guid objectGuid = this.composition.Objects.Find((Predicate<PublishCompositionObject>) (x => x.ObjectID == rel.ProjID)).ObjectGuid;
      Tuple<long, long, string> projObject = tupleList2.Find((Predicate<Tuple<long, long, string>>) (x => x.Item1.Equals(rel.ProjID)));
      ExtendedTransferedObject unit;
      if (PublishOptionsHelper.DummyPublish(relation.Include))
      {
        unit = new ExtendedTransferedObject(ChangeType.ctUpdate, TransferedObjectCategory.IncompleteRelation, (TransferedObjectTag) new IncompleteRelationTag(rel.GUID.ToString(), objectGuid.ToString(), relation.PartGuid.ToString()));
        ((ObjectTag) transObjs.Find((Predicate<ITransferedObject>) (x => x.GUID.Equals(projObject.Item3))).Tag).WithComposition = false;
      }
      else
      {
        Tuple<long, long, string> tuple = tupleList2.Find((Predicate<Tuple<long, long, string>>) (x => x.Item2.Equals(rel.PartID)));
        unit = new ExtendedTransferedObject(ChangeType.ctUpdate, TransferedObjectCategory.Relation, (TransferedObjectTag) new RelationTag(projObject.Item3, tuple.Item3));
        new RelationXMLFileFormer(session, unit, writer, rel, new Attributes4RelationTag(objectGuid, relation.PartGuid)).SaveAttributes();
      }
      transObjs.Add((ITransferedObject) unit);
    }
    this.AfterCompositionPack(session, customService2.Info, writer, transObjs);
    return transObjs.ToArray();
  }

  protected virtual bool FCAttributesOnlyEnable => true;

  public override ITask GetExportTask(
    IUserSession session,
    long userID,
    string taskName,
    Guid userGuid,
    TaskPriority priority,
    ITransferedObject[] units,
    IDBAttribute attributeTaskFiles)
  {
    return (ITask) new PublishTask(userID, userGuid, taskName, TaskType.Publish, priority, this.composition.Objects, this.options, units, this.Packet, this.ReceiptID, attributeTaskFiles);
  }

  protected virtual void WriteCodes(ObjectTag tag, char currentSiteCode, IDBObject obj)
  {
    if (obj.SiteID == null || obj.SiteID.Length == 0)
    {
      tag.CreatorCode = currentSiteCode;
      tag.OwnerCode = new char?(currentSiteCode);
      tag.CompositionOwnerCode = new char?(currentSiteCode);
    }
    else
    {
      tag.CreatorCode = obj.SiteID[0];
      char ch;
      if (obj.SiteID.Length >= 2)
      {
        ObjectTag objectTag = tag;
        ch = obj.SiteID[1];
        char? nullable = new char?(!ch.Equals(Consts.NoSymbol) ? obj.SiteID[1] : currentSiteCode);
        objectTag.OwnerCode = nullable;
      }
      else
        tag.OwnerCode = new char?(currentSiteCode);
      if (obj.SiteID.Length >= 3)
      {
        ObjectTag objectTag = tag;
        ch = obj.SiteID[2];
        char? nullable = new char?(!ch.Equals(Consts.NoSymbol) ? obj.SiteID[2] : currentSiteCode);
        objectTag.CompositionOwnerCode = nullable;
      }
      else
        tag.CompositionOwnerCode = new char?(currentSiteCode);
    }
  }

  protected override ObjectXMLFileFormer GetObjectXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    IDBObject obj,
    Attributes4ObjectTag tag)
  {
    return MetaDataHelper.IsObjectTypeEditingContext(obj.ObjectType) ? (ObjectXMLFileFormer) new ContextXMLFileFormer(session, unit, writer, obj, tag, this.composition) : base.GetObjectXMLFileFormer(session, unit, writer, obj, tag);
  }

  protected virtual Packet4Publish Packet => (Packet4Publish) null;

  protected virtual long ReceiptID => 0;

  public override string PublicationInfo
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.composition != null)
      {
        stringBuilder.AppendLine($"Публикуются {(this.composition.Objects != null ? this.composition.Objects.Count : 0)} объектов и {(this.composition.Relations != null ? this.composition.Relations.Count : 0)} связей.");
        stringBuilder.Append("Рутовые объекты:");
        foreach (PublishCompositionObject compositionObject in this.composition.Objects)
        {
          if (compositionObject.Root)
            stringBuilder.AppendLine($"{compositionObject.Caption}(ObjectID={compositionObject.ObjectID})");
        }
        stringBuilder.AppendLine("Количество уровней состава: " + (this.options.CountLevels == -1 ? "полный состав" : this.options.CountLevels.ToString()));
      }
      return stringBuilder.ToString();
    }
  }

  protected virtual void BeforeCompositionPack(
    IUserSession session,
    SiteInfo info,
    IBackupWriter writer,
    List<ITransferedObject> transObjs)
  {
  }

  protected virtual void AfterObjectPack(IDBObject obj, PublishCompositionObject pco)
  {
  }

  protected virtual void AfterCompositionPack(
    IUserSession session,
    SiteInfo info,
    IBackupWriter writer,
    List<ITransferedObject> transObjs)
  {
  }

  public override void CheckBeforePublication(IUserSession session)
  {
    if (this.publishType != PublishType.Packet)
      return;
    IPublishRulesService service = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
    if (!service.OTDFiltering)
      return;
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    string str1 = this.options.EnableSites.Trim(customService.Info.Code);
    if (str1.Length <= 1 || service.BeSurePublishForSites == null || service.BeSurePublishForSites.Count <= 0)
      return;
    List<char> charList = new List<char>();
    foreach (long surePublishForSite in service.BeSurePublishForSites)
    {
      SiteInfo site = customService.GetSite(surePublishForSite);
      if (site != null)
        str1 = str1.Replace(site.Code.ToString(), string.Empty);
    }
    if (str1.Length <= 0)
      return;
    bool flag = false;
    string str2 = string.Empty;
    for (int index = 0; index < this.composition.Objects.Count; ++index)
    {
      if (index == 0)
        str2 = this.composition.Objects[index].EnableSites;
      else if (!str2.Equals(this.composition.Objects[index].EnableSites))
      {
        flag = true;
        break;
      }
    }
    if (flag)
      throw new Exception("В публикуемом пакете содержатся объекты с различными значениями разрешенных узлов. Публикация такого состава для нескольких узлов в пакетном режиме невозможна!");
  }
}
