// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgCreator.DwgCreator
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Client.Core.ObjectCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgCreator;

internal class DwgCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private static readonly string _objTypeDwg = "cad00261-306c-11d8-b4e9-00304f19f545";
  private static readonly Guid _fixedRelationAttr = new Guid("CAD001C2-306C-11D8-B4E9-00304F19F545");
  private static readonly Guid _fixedRelationModeAttr = new Guid("CADD9609-306C-11D8-B4E9-00304F19F545");
  private static List<int> _attachedObjectTypes = new List<int>();
  private long[] _articleID;
  private long[] _relatedObjectIDs;
  private IDictionary<ObjectCreatePages, bool> _createPages;
  private long _templateObjectID = -1;
  private Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode _mode;
  private Guid _newObjTypeGuid = Guid.Empty;

  private void cnot_ObjectTypeChangedEvent(Guid objTypeGuid) => this._newObjTypeGuid = objTypeGuid;

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    this._mode = Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.newObject;
    if (isVersion)
      this._mode = Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.version;
    if (RelationTypeIDs != null && RelationTypeIDs.Length != 0)
      this._mode = Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.inComposition;
    this._relatedObjectIDs = RelatedObjectIDs;
    this._templateObjectID = TemplateObjectID;
    return false;
  }

  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    IDBObject dwg = session.GetObject(newObjectID);
    if (this._mode == Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.newObject && this._newObjTypeGuid != Guid.Empty)
    {
      IDBObjectType objectType1 = session.GetObjectType(dwg.ObjectType);
      IDBObjectType objectType2 = session.GetObjectType(this._newObjTypeGuid);
      Hashtable allChildren = objectType2.GetAllChildren();
      if (!allChildren.ContainsKey((object) dwg.ObjectType))
        throw new Exception($"Объект \"{objectType2.ObjectInstanceName}\" не может выпускаться по объекту \"{objectType1.ObjectInstanceName}\"");
      int relationType = (int) allChildren[(object) dwg.ObjectType];
      if (relationType != -1)
      {
        IDBRelationsApplicabilityCollection applicabilityCollection = session.GetRelationsApplicabilityCollection();
        if (this._articleID != null)
        {
          foreach (long objectID in this._articleID)
          {
            IDBObject dbObject = session.GetObject(objectID);
            DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, objectType1.ObjectType, dbObject.ObjectType);
            if (applicabilitiesList.Rows.Count > 0)
            {
              IDBRelation dbRelation = session.GetRelationCollection(Convert.ToInt32(applicabilitiesList.Rows[0]["F_RELATION_TYPE"])).Create(dbObject.ObjectID, dwg.ObjectID);
              dbRelation.SetAttributesValues(new AttributeValues[2]
              {
                new AttributeValues(MetaDataHelper.GetAttributeTypeID(Intermech.Cadmech.Integrator.DwgCreator.DwgCreator._fixedRelationModeAttr), (object) RevisionInstantiationMode.Hard),
                new AttributeValues(MetaDataHelper.GetAttributeTypeID(Intermech.Cadmech.Integrator.DwgCreator.DwgCreator._fixedRelationAttr), (object) Math.Abs(dwg.ObjectID))
              });
              nea.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, dbRelation.ProjID, dbRelation.RelationType));
            }
          }
        }
        else
        {
          long prototypeID = 0;
          IDBRelationCollection relationCollection = session.GetRelationCollection(relationType);
          if (this._templateObjectID != -1L)
          {
            DataTable dataTable = relationCollection.EntersInVersion(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
            {
              (object) -7,
              (object) -2
            }), this._templateObjectID);
            if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0][0]) == objectType2.ObjectType)
              prototypeID = Convert.ToInt64(dataTable.Rows[0][1]);
          }
          IDBObjectCollection objectCollection = session.GetObjectCollection(objectType2.ObjectType);
          IDBObject newArticle = prototypeID != 0L ? objectCollection.Create(prototypeID) : objectCollection.Create();
          this.AddAttributesForArticle(session, dwg, newArticle);
          nea.Add((NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", newArticle.ObjectID));
          IDBRelation dbRelation = relationCollection.Create(newArticle.ObjectID, dwg.ObjectID);
          dbRelation.SetAttributesValues(new AttributeValues[2]
          {
            new AttributeValues(MetaDataHelper.GetAttributeTypeID(Intermech.Cadmech.Integrator.DwgCreator.DwgCreator._fixedRelationModeAttr), (object) RevisionInstantiationMode.Hard),
            new AttributeValues(MetaDataHelper.GetAttributeTypeID(Intermech.Cadmech.Integrator.DwgCreator.DwgCreator._fixedRelationAttr), (object) Math.Abs(dwg.ObjectID))
          });
          nea.Add((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, dbRelation.ProjID, dbRelation.RelationType));
          newArticle.CommitCreation(true);
          newArticle.CheckOut(false);
        }
      }
    }
    else if (this._mode == Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.inComposition && this._relatedObjectIDs != null && this._relatedObjectIDs.Length != 0)
    {
      IDBObject dbObject = session.GetObject(this._relatedObjectIDs[0]);
      ISelectionsService customService = session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      if (dbObject != null && customService != null && DocumentsHelper.DefiningDocument(session, dwg.ObjectType, dbObject.ObjectType))
      {
        long classifierForObject = customService.GetClassifierForObject((object) session.SessionGUID, dbObject.ID);
        if (classifierForObject != -1L)
          customService.IncludeObjects((object) session.SessionGUID, classifierForObject, new long[1]
          {
            newObjectID
          });
      }
    }
    return true;
  }

  public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
  {
    Dictionary<UserControl, int> dictionary = new Dictionary<UserControl, int>();
    if (this._mode == Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.newObject && CreatedObject is CreatedObjectItem objItem)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) sessionKeeper.Session.GetCustomService(typeof (IDocumentTypeSettingsService));
        if (customService != null)
        {
          DocumentTypeSettings settings = customService.GetSettings(sessionKeeper.Session.SessionGUID, objItem.ObjectTypeID);
          if (settings.OutputObjectTypes == string.Empty)
            return (Dictionary<UserControl, int>) null;
          foreach (string outputObjectType in DocumentTypeSettings.SplitOutputObjectTypes(settings.OutputObjectTypes))
          {
            if (!GuidHelper.IsGuid(outputObjectType))
              return (Dictionary<UserControl, int>) null;
          }
        }
      }
      ChoiceNewObjectType key = new ChoiceNewObjectType(objItem);
      key.ObjectTypeChangedEvent += new ChoiceNewObjectType.ObjectTypeChanged(this.cnot_ObjectTypeChangedEvent);
      key.NewObjectSelectedEvent += new ChoiceNewObjectType.NewObjectSelected(this.cnot_NewObjectSelectedEvent);
      key.SetPageData();
      dictionary.Add((UserControl) key, -1);
    }
    return dictionary.Count > 0 ? dictionary : (Dictionary<UserControl, int>) null;
  }

  private void cnot_NewObjectSelectedEvent(long[] objectID) => this._articleID = objectID;

  public bool AfterCreate(long newObjectID)
  {
    if (this._mode == Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.inComposition && this._relatedObjectIDs != null && this._relatedObjectIDs.Length != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject article = sessionKeeper.Session.GetObject(this._relatedObjectIDs[0]);
        IDBObject newDwg = sessionKeeper.Session.GetObject(newObjectID);
        if (article != null)
        {
          if (DocumentsHelper.DefiningDocument(sessionKeeper.Session, newDwg.ObjectType, article.ObjectType))
            this.AddAttributesForDwg(sessionKeeper.Session, article, newDwg);
        }
      }
    }
    return true;
  }

  private void AddAttributesForArticle(IUserSession session, IDBObject dwg, IDBObject newArticle)
  {
    IDBAttribute byGuid1 = dwg.Attributes.FindByGUID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    string str1 = byGuid1 != null ? byGuid1.AsString : string.Empty;
    IDBAttribute byGuid2 = dwg.Attributes.FindByGUID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    string designation = byGuid2 != null ? byGuid2.AsString : string.Empty;
    IDBAttribute byGuid3 = dwg.Attributes.FindByGUID(new Guid("cad0038a-306c-11d8-b4e9-00304f19f545"));
    string str2 = byGuid3 != null ? byGuid3.AsString : string.Empty;
    IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
    if (customService != null)
    {
      DocumentTypeSettings settings = customService.GetSettings(session.SessionGUID, dwg.ObjectType);
      if (settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
        designation = DocumentsHelper.RemoveDocCode(session, designation, settings.DocumentTypeCode);
    }
    newArticle.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) str1
    });
    newArticle.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) designation
    });
    newArticle.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0038a-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) str2
    });
  }

  private void AddAttributesForDwg(IUserSession session, IDBObject article, IDBObject newDwg)
  {
    IDBAttribute byGuid1 = article.Attributes.FindByGUID(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"));
    string str = byGuid1 != null ? byGuid1.AsString : string.Empty;
    IDBAttribute byGuid2 = article.Attributes.FindByGUID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"));
    string designation = byGuid2 != null ? byGuid2.AsString : string.Empty;
    IDBAttribute byGuid3 = article.Attributes.FindByGUID(new Guid("cad0038a-306c-11d8-b4e9-00304f19f545"));
    if (byGuid3 == null)
    {
      string empty = string.Empty;
    }
    else
    {
      string asString = byGuid3.AsString;
    }
    IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
    if (customService != null)
    {
      DocumentTypeSettings settings = customService.GetSettings(session.SessionGUID, newDwg.ObjectType);
      if (settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
        designation = DocumentsHelper.AppendDocCode(session, designation, settings.DocumentTypeCode);
    }
    newDwg.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) str
    });
    newDwg.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) designation
    });
  }

  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      if (this._createPages == null)
      {
        this._createPages = (IDictionary<ObjectCreatePages, bool>) new Dictionary<ObjectCreatePages, bool>();
        this._createPages.Add(ObjectCreatePages.Classifier, this._mode != Intermech.Cadmech.Integrator.DwgCreator.DwgCreator.CreatorMode.inComposition);
        this._createPages.Add(ObjectCreatePages.Properties, true);
        this._createPages.Add(ObjectCreatePages.FileAttributes, true);
        this._createPages.Add(ObjectCreatePages.Relations, false);
        this._createPages.Add(ObjectCreatePages.Template, true);
      }
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

  private enum CreatorMode
  {
    newObject,
    version,
    inComposition,
  }
}
