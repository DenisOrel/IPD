// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.SelectPublishCompositionThread
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.PortalServices.Composition;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class SelectPublishCompositionThread : CustomSelectThread<PublishComposition>
{
  private readonly List<long> _rootObjectIDs;
  private readonly ExtendedPublishOptions _options;
  private readonly Dictionary<int, List<int>> _freeAttributesCache;
  private readonly PublishType _publishType;
  private readonly FieldsMapper _fieldsMapper;
  private readonly bool _throwCheckException;
  private ICustomObjectAnalyzer _analyzer;
  private readonly CaptionCache _cacheCaptions;
  private readonly IIDLinkTranslate _linkService;
  private const int _firstLevel = 1;

  public SelectPublishCompositionThread(
    Guid id,
    Guid userSessionGuid,
    List<long> rootObjectIDs,
    PublishType publishType,
    ExtendedPublishOptions options,
    bool throwCheckException)
    : base(id, userSessionGuid)
  {
    this._rootObjectIDs = rootObjectIDs;
    this._options = options;
    this._publishType = publishType;
    this._fieldsMapper = new FieldsMapper();
    this._throwCheckException = throwCheckException;
    if ((this._options.CompositionOptions & PublishCompositionOptions.IncludeFreeChangeAttributes) > PublishCompositionOptions.None)
      this._freeAttributesCache = new Dictionary<int, List<int>>();
    if (this._options.AccessLevel == -1)
      this._options.AccessLevel = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true).MaxAccessLevel;
    this._cacheCaptions = new CaptionCache(this._infoRequired);
    this._linkService = ServerServices.GetService(typeof (IIDLinkTranslate)) as IIDLinkTranslate;
  }

  private bool _infoRequired
  {
    get
    {
      return (this._options.CompositionOptions & PublishCompositionOptions.InfoRequired) > PublishCompositionOptions.None;
    }
  }

  protected override void ThreadMethod()
  {
    string sessionName = $"SelectPublishCompositionThread_{Guid.NewGuid()}";
    IUserSession cloneSession = PortalServicesSessionHelper.GetCloneSession(this.userSessionGuid, sessionName, "SelectPublishCompositionThread.ThreadMethod", true);
    this.SetPercent(0);
    try
    {
      this._analyzer = ObjectAnalyzerHelper.GetAnalyzer(cloneSession, this._publishType, this._options, this._freeAttributesCache);
      List<PublishCompositionObject> inRootObjects = new List<PublishCompositionObject>();
      PublishComposition publishComposition = new PublishComposition();
      List<long> contexts = new List<long>();
      foreach (long rootObjectId in this._rootObjectIDs)
      {
        PublishCompositionObject objectInfo = this._analyzer.GetObjectInfo(cloneSession, rootObjectId, true);
        if (!PublishOptionsHelper.ForbiddenForPublish(objectInfo.Include) && MetaDataHelper.IsObjectTypeEditingContext(objectInfo.ObjectType))
          contexts.Add(objectInfo.ObjectID);
        inRootObjects.Add(objectInfo);
      }
      this.SelectComposition(cloneSession, inRootObjects, publishComposition.Objects, publishComposition.Relations, false, contexts.Count > 0 ? this.GetContextTags(contexts) : (HybridDictionary) null);
      if (publishComposition.Objects != null)
      {
        List<PublishCompositionObject> all = publishComposition.Objects.FindAll((Predicate<PublishCompositionObject>) (x => x.ObjectID < 0L || x.CheckOutBy != 0L));
        if (all != null && all.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (PublishCompositionObject compositionObject in all)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", ");
            if (string.IsNullOrEmpty(compositionObject.Caption))
            {
              IDBObject dbObject = cloneSession.GetObject(compositionObject.ObjectID);
              stringBuilder.Append(dbObject.NameInMessages);
            }
            else
              stringBuilder.Append($"{compositionObject.Caption}({compositionObject.ObjectID})");
          }
          Exception exception = new Exception($"В публикуемом составе обнаружены рабочие копии или взятые на изменение другим пользователем объекты: {stringBuilder.ToString()}. Публикация таких объектов запрещена.");
          if (this._throwCheckException)
            throw exception;
          this.ErrorException = exception;
          this.IsError = false;
        }
      }
      this.result = publishComposition;
      this.SetPercent(100);
    }
    catch (Exception ex)
    {
      this.IsError = true;
      this.ErrorException = ex;
      this.SetPercent(100);
    }
    finally
    {
      if (cloneSession != null)
        PortalServicesSessionHelper.LogoutSession(cloneSession, sessionName, "SelectPublishCompositionThread.ThreadMethod");
    }
  }

  private void SelectComposition(
    IUserSession session,
    List<PublishCompositionObject> inRootObjects,
    List<PublishCompositionObject> objects,
    List<PublishCompositionRelation> relations,
    bool linked,
    HybridDictionary tags)
  {
    List<PublishCompositionObject> compositionObjectList = new List<PublishCompositionObject>(inRootObjects.Count);
    CompositionObjectHandler handler = new CompositionObjectHandler(this._cacheCaptions, this._infoRequired, true, linked, this._fieldsMapper.CustomIndexes);
    foreach (PublishCompositionObject inRootObject in inRootObjects)
      handler.HandleObject(session, inRootObject, (PublishCompositionRelation) null, compositionObjectList, objects, relations);
    IPublishTypesConfiguration service1 = ServerServices.GetService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration;
    if (compositionObjectList.Count <= 0)
      return;
    this.AddLinkedObjects(session, handler, compositionObjectList, objects, relations);
    List<int> intList = new List<int>();
    List<int> selectedRelationTypes = new List<int>();
    List<int> alwaysRelationTypes = service1.AlwaysRelationTypes;
    int countLevels = this._options.CountLevels;
    if (this._options.CountLevels == 0)
    {
      if (alwaysRelationTypes != null)
      {
        countLevels = 1;
        intList = alwaysRelationTypes;
        selectedRelationTypes = alwaysRelationTypes;
      }
    }
    else
    {
      selectedRelationTypes = service1.PublishRelationTypes;
      if (this._options.EnableRelationTypes != null && this._options.EnableRelationTypes.Count > 0)
        intList.AddRange((IEnumerable<int>) this._options.EnableRelationTypes);
      if (alwaysRelationTypes != null)
        intList = intList.Union<int>((IEnumerable<int>) alwaysRelationTypes).ToList<int>();
    }
    if (countLevels != 0)
    {
      ICompositionLoadService service2 = ServerServices.GetService(typeof (ICompositionLoadService)) as ICompositionLoadService;
      List<ObjInfoItem> list = compositionObjectList.Select<PublishCompositionObject, ObjInfoItem>((System.Func<PublishCompositionObject, ObjInfoItem>) (x => new ObjInfoItem(x.ObjectID, x.ObjectType))).ToList<ObjInfoItem>();
      handler.FirstLevel = false;
      this.SelectLevelComposition(service2, handler, session, service1.PublishObjectTypes, selectedRelationTypes, intList, list, objects, relations, tags, countLevels, 1);
    }
    if (service1.ObjectWithLinksPresent)
    {
      List<PublishCompositionObject> objects1 = new LinkedObjectFromObjectAttribute(this._options, this._analyzer, objects).GetObjects(session, objects, new ObjectLinkFromObjectHandler(this._infoRequired));
      if (objects1.Count > 0)
        objects.InsertRange(0, (IEnumerable<PublishCompositionObject>) objects1);
      if (new LinkedObjectFromRelationAttribute(this._options, this._analyzer, objects).GetObjects(session, relations, new ObjectLinkFromRelationHandler(this._infoRequired)).Count > 0)
        objects.InsertRange(0, (IEnumerable<PublishCompositionObject>) objects1);
    }
    if (this._options.EnableTypes != null && !this._options.EnableTypes.Contains(session.IdentHelper.UsersTypeID))
      return;
    List<PublishCompositionObject> objects2 = new LinkedUserFromOwnerAttribute(this._analyzer, objects).GetObjects(session, objects, new OwnerHandler(this._infoRequired));
    if (objects2.Count <= 0)
      return;
    objects.InsertRange(0, (IEnumerable<PublishCompositionObject>) objects2);
  }

  private List<Tuple<PublishCompositionObject, PublishCompositionRelation>> GetLinked(
    IUserSession session,
    List<PublishCompositionObject> source)
  {
    ILinkedObjectsService service = ServerServices.GetService(typeof (ILinkedObjectsService)) as ILinkedObjectsService;
    List<Tuple<PublishCompositionObject, PublishCompositionRelation>> list = new List<Tuple<PublishCompositionObject, PublishCompositionRelation>>();
    foreach (PublishCompositionObject compositionObject1 in source)
    {
      Dictionary<string, List<LinkedObject>> linkedObjectsEx = service.GetLinkedObjectsEx(session, compositionObject1.ObjectID, compositionObject1.ObjectType, this._options.Filtration.OwnerID);
      if (linkedObjectsEx != null && linkedObjectsEx.Count != 0)
      {
        string linkedGuid;
        if (string.IsNullOrEmpty(compositionObject1.LinkedGuid))
        {
          linkedGuid = Guid.NewGuid().ToString();
          compositionObject1.LinkedGuid = linkedGuid;
        }
        else
          linkedGuid = compositionObject1.LinkedGuid;
        foreach (KeyValuePair<string, List<LinkedObject>> keyValuePair in linkedObjectsEx)
        {
          foreach (LinkedObject linkedObject1 in keyValuePair.Value)
          {
            LinkedObject linkedObject = linkedObject1;
            PublishCompositionObject compositionObject2 = source.Find((Predicate<PublishCompositionObject>) (p => p.ObjectID == linkedObject.ObjectID));
            if (compositionObject2 == null)
            {
              Tuple<PublishCompositionObject, PublishCompositionRelation> tuple = list.Find((Predicate<Tuple<PublishCompositionObject, PublishCompositionRelation>>) (p => p.Item1.ObjectID == linkedObject.ObjectID));
              if (tuple != null)
                compositionObject2 = tuple.Item1;
            }
            if (compositionObject2 != null)
            {
              if (GuidHelper.IsGuid(compositionObject2.LinkedGuid))
              {
                this.SetNewLinkedGuid(compositionObject2.LinkedGuid, linkedGuid, source);
                if (list.Count > 0)
                  this.SetNewLinkedGuid(compositionObject2.LinkedGuid, linkedGuid, list);
              }
              compositionObject2.LinkedGuid = linkedGuid;
              if (this._infoRequired)
              {
                compositionObject2.ReasonInfo += string.IsNullOrEmpty(compositionObject2.ReasonInfo) ? "" : ", ";
                compositionObject2.ReasonInfo += $"Добавлен \"{keyValuePair.Key}\" для {this._cacheCaptions.GetCaption(compositionObject1.ObjectID)}";
              }
            }
            else
            {
              PublishCompositionObject objectInfo = this._analyzer.GetObjectInfo(session, linkedObject.ObjectID, false);
              objectInfo.LinkedGuid = linkedGuid;
              if (this._infoRequired)
              {
                objectInfo.ReasonInfo = $"Добавлен \"{keyValuePair.Key}\" для {this._cacheCaptions.GetCaption(compositionObject1.ObjectID)}";
                if (objectInfo.Include == IncludeTypes.FCAttributesOnly || objectInfo.Include == IncludeTypes.FCFileAttributesOnly)
                  objectInfo.ReasonInfo += $", {Helper.MessageFCAttribute}";
              }
              PublishCompositionRelation compositionRelation = (PublishCompositionRelation) null;
              if (linkedObject.RelationID != 0L)
              {
                IDBRelation relation = session.GetRelation(linkedObject.RelationID, true);
                Guid partGuid = Guid.Empty;
                if (relation.PartID.Equals(objectInfo.ID))
                  partGuid = objectInfo.ObjectGuid;
                else if (relation.PartID.Equals(compositionObject1.ID))
                  partGuid = compositionObject1.ObjectGuid;
                if (partGuid != Guid.Empty)
                  compositionRelation = new PublishCompositionRelation(linkedObject.RelationID, partGuid, relation.RelationType);
              }
              list.Add(new Tuple<PublishCompositionObject, PublishCompositionRelation>(objectInfo, compositionRelation));
            }
          }
        }
      }
    }
    return list;
  }

  private void SetNewLinkedGuid(string p1, string p2, List<PublishCompositionObject> list)
  {
    foreach (PublishCompositionObject compositionObject in list.FindAll((Predicate<PublishCompositionObject>) (p => p.LinkedGuid == p1)))
      compositionObject.LinkedGuid = p2;
  }

  private void SetNewLinkedGuid(
    string p1,
    string p2,
    List<Tuple<PublishCompositionObject, PublishCompositionRelation>> list)
  {
    foreach (Tuple<PublishCompositionObject, PublishCompositionRelation> tuple in list.FindAll((Predicate<Tuple<PublishCompositionObject, PublishCompositionRelation>>) (p => p.Item1.LinkedGuid == p1)))
      tuple.Item1.LinkedGuid = p2;
  }

  private DataTable GetLevelTable(
    ICompositionLoadService compositionService,
    IUserSession session,
    List<int> objectTypes,
    List<int> relationTypes,
    List<ObjInfoItem> rootObjects,
    HybridDictionary tags)
  {
    if (rootObjects.Count == 1)
    {
      ObjInfoItem rootObject = rootObjects[0];
      return compositionService.LoadComposition((object) session, rootObject.ObjectID, rootObject.ObjTypeID, (IEnumerable<int>) relationTypes, (IEnumerable<int>) objectTypes, (IEnumerable<ColumnDescriptor>) this._fieldsMapper.Columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, this._options.Filtration.OwnerID, tags, 1);
    }
    Dictionary<long, HybridDictionary> dbParams = (Dictionary<long, HybridDictionary>) null;
    if (tags != null)
    {
      dbParams = new Dictionary<long, HybridDictionary>();
      foreach (long rootObjectId in this._rootObjectIDs)
        dbParams.Add(rootObjectId, tags);
    }
    return compositionService.LoadComplexCompositions((object) session, (IEnumerable<ObjInfoItem>) rootObjects, (IEnumerable<int>) relationTypes, (IEnumerable<int>) objectTypes, (IEnumerable<ColumnDescriptor>) this._fieldsMapper.Columns, true, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, this._options.Filtration.OwnerID, dbParams, 1);
  }

  private void SelectLevelComposition(
    ICompositionLoadService compositionService,
    CompositionObjectHandler handler,
    IUserSession session,
    List<int> selectedObjectTypes,
    List<int> selectedRelationTypes,
    List<int> enabledRelationTypes,
    List<ObjInfoItem> rootObjects,
    List<PublishCompositionObject> objects,
    List<PublishCompositionRelation> relations,
    HybridDictionary tags,
    int countLevels,
    int currentLevel)
  {
    DataTable levelTable = this.GetLevelTable(compositionService, session, selectedObjectTypes, selectedRelationTypes, rootObjects, tags);
    if (levelTable == null)
      return;
    List<PublishCompositionObject> compositionObjectList = new List<PublishCompositionObject>();
    foreach (DataRow row in (InternalDataCollectionBase) levelTable.Rows)
    {
      PublishCompositionObject pco;
      PublishCompositionRelation pcr;
      this._analyzer.GetRecordInfo(session, objects, row, this._fieldsMapper, enabledRelationTypes, out pco, out pcr);
      handler.HandleObject(session, pco, pcr, compositionObjectList, objects, relations);
    }
    if (compositionObjectList.Count == 0)
      return;
    this.AddLinkedObjects(session, handler, compositionObjectList, objects, relations);
    if (compositionObjectList.Count <= 0 || countLevels != -1 && currentLevel >= countLevels)
      return;
    List<ObjInfoItem> list = compositionObjectList.Select<PublishCompositionObject, ObjInfoItem>((System.Func<PublishCompositionObject, ObjInfoItem>) (x => new ObjInfoItem(x.ObjectID, x.ObjectType))).ToList<ObjInfoItem>();
    this.SelectLevelComposition(compositionService, handler, session, selectedObjectTypes, selectedRelationTypes, enabledRelationTypes, list, objects, relations, tags, countLevels, currentLevel + 1);
  }

  private void AddLinkedObjects(
    IUserSession session,
    CompositionObjectHandler handler,
    List<PublishCompositionObject> levelObjects,
    List<PublishCompositionObject> compositionObjects,
    List<PublishCompositionRelation> compositionRelations)
  {
    if ((this._options.CompositionOptions & PublishCompositionOptions.WithLinkedObjects) <= PublishCompositionOptions.None)
      return;
    List<Tuple<PublishCompositionObject, PublishCompositionRelation>> linked = this.GetLinked(session, levelObjects);
    if (linked.Count <= 0)
      return;
    foreach (Tuple<PublishCompositionObject, PublishCompositionRelation> tuple in linked)
      handler.HandleObject(session, tuple.Item1, tuple.Item2, levelObjects, compositionObjects, compositionRelations, true);
  }

  private HybridDictionary GetContextTags(List<long> contexts)
  {
    return new HybridDictionary()
    {
      [(object) "{2FACA180-73B8-4F24-9928-5623661BBBE6}"] = (object) true,
      [(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true,
      [(object) "{529FFE92-FDA7-48B8-AADF-ADB1EE6EF584}"] = (object) false,
      [(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) true,
      [(object) "{AB419A02-DE8A-4A8E-905A-D782F5B720E5}"] = (object) contexts
    };
  }
}
