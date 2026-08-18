// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFScheme
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Project;
using Intermech.Workflow.Base;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFScheme(UserSession uSession, DataTable objectsTable) : 
  DBMailObject(uSession, objectsTable),
  ISchemeCheckOut,
  IScheme,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity,
  ISchemeActivityCreator
{
  private List<WFActivity> _activities = new List<WFActivity>();
  internal bool ForceGetActivities;
  private bool _loaded;
  private bool _showNotOwnedWorkCopies;
  private List<WFLink> _links = new List<WFLink>();
  private bool _linksLoaded;
  private bool _allLinksLoaded;
  private List<WFLink> _allLinks = new List<WFLink>();
  private List<WFLink> _allBlankLinks = new List<WFLink>();
  private bool _variablesInit;
  private VarList _variables;
  private GlobalVariablesList _globalVariables;
  private int _checkOutSchemeWithoutEditable;
  internal WFScheme prototype;
  protected internal Dictionary<Guid, Guid> _objectGuidMapper = new Dictionary<Guid, Guid>();
  private long _linkedTaskObjectID = -1;
  private Start _startActivity;
  private Dictionary<ActionType, ActionCategory> _actionCategories = new Dictionary<ActionType, ActionCategory>();
  internal bool _inAssignAttributes;
  private long[] _validateDeleteObjectsAndLinks = new long[0];

  public virtual ActivityKind Kind => ActivityKind.None;

  public List<WFActivity> Activities
  {
    get
    {
      this.Load();
      return this._activities;
    }
  }

  private void Load()
  {
    if (this.ForceGetActivities)
      this._loaded = false;
    if (this._loaded)
      return;
    this._loaded = true;
    this._activities.Clear();
    long ObjectID = this.ObjectID;
    if (ObjectID > 0L && this.CheckoutBy == this.Session.UserID)
      ObjectID = -ObjectID;
    foreach (long children in this.GetChildrenList(wfConsts.ActivitiesTypeID, ObjectID))
      this.GetDBActivity(children);
  }

  private void Load(long[] blankActIDs, long[] blankLinkIDs, long[] deleted)
  {
    this._loaded = true;
    this._activities.Clear();
    long ObjectID1 = this.ObjectID;
    if (ObjectID1 > 0L && this.CheckoutBy == this.Session.UserID)
      ObjectID1 = -ObjectID1;
    List<long> childrenList = this.GetChildrenList(wfConsts.ActivitiesTypeID, ObjectID1);
    if (blankActIDs != null)
    {
      foreach (long blankActId in blankActIDs)
      {
        if (!childrenList.Contains(blankActId))
          childrenList.Add(blankActId);
      }
    }
    if (deleted != null)
    {
      foreach (long num in deleted)
        childrenList.Remove(num);
    }
    foreach (long ObjectID2 in childrenList)
      this.GetDBActivity(ObjectID2);
  }

  internal WFActivity GetDBActivity(long ObjectID, bool acceptChildren = false)
  {
    foreach (WFActivity activity in this._activities)
    {
      if (activity.ObjectID == ObjectID)
        return activity;
    }
    if (acceptChildren)
    {
      foreach (WFActivity activity in this._activities)
      {
        if (activity.ParentActivityID == ObjectID)
          return activity;
      }
    }
    if (this.UserSession.GetObject(ObjectID) is WFActivity act)
      this.AddActivity(act);
    return act;
  }

  internal void AddActivity(WFActivity act)
  {
    act._process = this;
    this._activities.Add(act);
  }

  private List<long> GetChildrenList(int TypeID, long ObjectID)
  {
    List<long> childrenList = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) this.GetChildren(TypeID, ObjectID).Rows)
      childrenList.Add(Convert.ToInt64(row[0]));
    return childrenList;
  }

  private DataTable GetChildren(int TypeID, long ObjectID)
  {
    IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(TypeID);
    ConditionStructure conditionStructure = new ConditionStructure(-2, RelationalOperators.Less, (object) 0, LogicalOperators.AND, 0, false);
    if (ObjectID > 0L)
      conditionStructure.RelationalOperator = RelationalOperators.Greater;
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) Math.Abs(ObjectID), (object) null, LogicalOperators.AND, 0, true, AttributeSourceTypes.Auto, ColumnContents.ID),
      conditionStructure
    };
    object[] columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    object[] sortColumns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    SortOrders sortOrders = SortOrders.ASC;
    if (ObjectID < 0L || this.CheckoutBy == this.Session.UserID)
      sortOrders = SortOrders.DESC;
    SortOrders[] orders = new SortOrders[1]{ sortOrders };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns, sortColumns, orders);
    if (this._showNotOwnedWorkCopies)
      paramSet.Tags = new HybridDictionary(1, true)
      {
        [(object) "ShowNotOwnedWorkCopies"] = (object) true
      };
    if (paramSet.Tags == null)
      paramSet.Tags = new HybridDictionary();
    paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectRefSelector(wfConsts.AttrProcessID, Math.Abs(ObjectID));
    return objectCollection.Select(paramSet);
  }

  public List<WFLink> Links
  {
    get
    {
      if (!this._linksLoaded)
      {
        this._links.Clear();
        this._linksLoaded = true;
        this.LoadLinks(this._links, LinkDirection.From, new LinkKind[1]
        {
          LinkKind.Backward
        }, true);
      }
      return this._links;
    }
  }

  public List<WFLink> AllLinks
  {
    get
    {
      if (!this._allLinksLoaded)
      {
        this._allLinks.Clear();
        this._allLinksLoaded = true;
        this.LoadLinks(this._allLinks, LinkDirection.From, (LinkKind[]) null, false);
      }
      return this._allLinks;
    }
  }

  public void LoadLinks(
    List<WFLink> linksList,
    LinkDirection dir,
    LinkKind[] kinds,
    bool invertLinkKind)
  {
    List<long> longList = new List<long>()
    {
      Math.Abs(this.ObjectID)
    };
    if (this is WFProcess && ((WFProcess) this).CreateActivitiesOnDemand)
    {
      // ISSUE: explicit non-virtual call
      long prototypeSchemeId = __nonvirtual (((WFProcess) this).PrototypeSchemeID);
      if (prototypeSchemeId != 0L)
        longList.Add(prototypeSchemeId);
    }
    RelationalOperators relationalOperator = RelationalOperators.In;
    List<int> intList = new List<int>();
    if (kinds != null)
    {
      foreach (LinkKind kind in kinds)
        intList.Add(Convert.ToInt32((object) kind));
    }
    if (invertLinkKind)
      relationalOperator = RelationalOperators.NotIn;
    if (intList.Count == 0)
      relationalOperator = RelationalOperators.NOP;
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.GetObjectCollection(wfConsts.LinksTypeID).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.In, (object) longList.ToArray(), (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      new ConditionStructure(wfConsts.AttrLinkKindID, relationalOperator, (object) intList.ToArray(), LogicalOperators.AND, 0, true)
    }, new object[4]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) wfConsts.AttrToActivityID,
      (object) wfConsts.AttrFromActivityID,
      (object) wfConsts.AttrLinkKindID
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, new SortOrders[1]{ SortOrders.ASC })).Rows)
    {
      if (!DBNull.Value.Equals(row[1]) && !DBNull.Value.Equals(row[0]) && !DBNull.Value.Equals(row[2]) && !DBNull.Value.Equals(row[3]))
      {
        WFLink wfLink = this.UserSession.GetObject(Convert.ToInt64(row[0])) as WFLink;
        wfLink.FromID = Convert.ToInt64(row[2]);
        wfLink.ToID = Convert.ToInt64(row[1]);
        wfLink.Kind = (LinkKind) Convert.ToInt64(row[3]);
        wfLink._inherited = false;
        linksList.Add(wfLink);
      }
    }
  }

  public void LoadParallelLink(
    List<WFLink> linksList,
    LinkDirection dir,
    LinkKind[] kinds,
    bool invertLinkKind,
    WFActivity act)
  {
    long conditionValue = act.ParentActivityID;
    bool flag = conditionValue != 0L;
    if (conditionValue == 0L)
      conditionValue = Math.Abs(act.ObjectID);
    List<long> longList = new List<long>();
    longList.Add(Math.Abs(this.ObjectID));
    if (this is WFProcess && ((WFProcess) this).CreateActivitiesOnDemand)
    {
      // ISSUE: explicit non-virtual call
      long prototypeSchemeId = __nonvirtual (((WFProcess) this).PrototypeSchemeID);
      if (prototypeSchemeId != 0L)
        longList.Add(prototypeSchemeId);
    }
    RelationalOperators relationalOperator = RelationalOperators.In;
    List<int> intList = new List<int>();
    if (kinds != null)
    {
      foreach (LinkKind kind in kinds)
        intList.Add(Convert.ToInt32((object) kind));
    }
    if (invertLinkKind)
      relationalOperator = RelationalOperators.NotIn;
    if (intList.Count == 0)
      relationalOperator = RelationalOperators.NOP;
    int num;
    int attributeID;
    if (dir == LinkDirection.From)
    {
      num = wfConsts.AttrToActivityID;
      attributeID = wfConsts.AttrFromActivityID;
    }
    else
    {
      num = wfConsts.AttrFromActivityID;
      attributeID = wfConsts.AttrToActivityID;
    }
    foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.GetObjectCollection(wfConsts.LinksTypeID).Select(new DBRecordSetParams(new ConditionStructure[3]
    {
      new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.In, (object) longList.ToArray(), (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      new ConditionStructure(attributeID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, true),
      new ConditionStructure(wfConsts.AttrLinkKindID, relationalOperator, (object) intList.ToArray(), LogicalOperators.AND, 0, true)
    }, new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) num,
      (object) wfConsts.AttrLinkKindID
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, new SortOrders[1]{ SortOrders.ASC })).Rows)
    {
      if (!DBNull.Value.Equals(row[1]))
      {
        WFLink wfLink = this.UserSession.GetObject(Convert.ToInt64(row[0])) as WFLink;
        if (dir == LinkDirection.From)
        {
          wfLink.FromID = conditionValue;
          wfLink.ToID = Convert.ToInt64(row[1]);
        }
        else
        {
          wfLink.ToID = conditionValue;
          wfLink.FromID = Convert.ToInt64(row[1]);
        }
        wfLink.Kind = (LinkKind) Convert.ToInt64(row[2]);
        wfLink._inherited = flag;
        linksList.Add(wfLink);
      }
    }
  }

  public void ClearVariable()
  {
    this._variables = (VarList) null;
    this._variablesInit = false;
  }

  public VarList Variables
  {
    get
    {
      string key = $"WFScheme_LoadingVars_{this.ObjectID}";
      if (object.Equals(this.UserSession.GetSessionPluginsData((object) key), (object) true))
        return this._variables;
      this.UserSession.SetSessionPluginsData((object) key, (object) true);
      try
      {
        if (this._variables == null || !this._variablesInit)
        {
          this._variablesInit = true;
          this._variables = new VarList((IDBObject) this, true, false);
        }
        if (this._variables != null && !this._variables.SystemAdded)
          this._variables.AddSystemVariables((IDBObject) this);
        return this._variables;
      }
      finally
      {
        this.UserSession.RemoveSessionPluginsData((object) key);
      }
    }
  }

  public List<Var> VariableXML
  {
    get => this.Load(this.GetAttributeByID(wfConsts.AttrVariablesID) as IBlobReader);
  }

  public List<Var> Load(IBlobReader reader)
  {
    if (reader == null)
      return new List<Var>();
    using (MemoryStream memoryStream = new MemoryStream())
    {
      BlobInformation blobInformation = reader.OpenBlob(0);
      try
      {
        if (blobInformation.RealFileSize > 0L)
        {
          byte[] buffer = reader.ReadDataBlock((int) blobInformation.RealFileSize);
          memoryStream.Write(buffer, 0, buffer.Length);
        }
      }
      finally
      {
        reader.CloseBlob();
      }
      memoryStream.Position = 0L;
      return this.LoadFromStream((Stream) memoryStream);
    }
  }

  public List<Var> LoadFromStream(Stream stream)
  {
    if (stream.Length == 0L)
      return new List<Var>();
    if (stream.Position != 0L)
      stream.Position = 0L;
    return (List<Var>) new XmlSerializer(typeof (List<Var>), new XmlRootAttribute("Vars")).Deserialize(stream);
  }

  public GlobalVariablesList GlobalVariables
  {
    get
    {
      if (this._globalVariables == null)
        this._globalVariables = new GlobalVariablesList((IDBObject) this, true, false);
      return this._globalVariables;
    }
  }

  IVariables IScheme.GlobalVariables
  {
    get => (IVariables) new RemotingVarList((VarList) this.GlobalVariables);
  }

  public bool SchemeDebugMode
  {
    get
    {
      IDBAttribute byGuid = this.Attributes.FindByGUID(wfConsts.AttrIsDebugGuid);
      return byGuid != null && byGuid.AsBoolean;
    }
    set
    {
      if (this is WFProcess)
        return;
      this.Attributes.AddAttribute(wfConsts.AttrIsDebugID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public SchemeStatus SchemeStatus
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrActivityStatusID);
      return attributeById == null ? SchemeStatus.Invalid : (SchemeStatus) attributeById.AsInteger;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrActivityStatusID);
      int num = (int) value;
      if ((attributeById == null ? -2 : (int) attributeById.AsInteger) == num)
        return;
      this.Attributes.AddAttribute(wfConsts.AttrActivityStatusID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public string Name
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrNameID);
      return attributeById == null ? this.Caption : attributeById.AsString;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrNameID);
      if (attributeById == null)
        return;
      attributeById.AsString = value;
    }
  }

  public string Description
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrDescriptionID);
      return attributeById == null ? string.Empty : attributeById.AsString;
    }
    set
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrDescriptionID);
      if (attributeById == null)
        return;
      attributeById.Value = (object) value;
    }
  }

  public bool ShowFormWhereActivityBack
  {
    get
    {
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrShowFormWithActivityBackID);
      return attributeById != null && attributeById.AsBoolean;
    }
    set
    {
      this.Attributes.AddAttribute(wfConsts.AttrShowFormWithActivityBackID, false, new object[1]
      {
        (object) value
      });
    }
  }

  public int CheckOutSchemeWithoutEditable
  {
    get => this._checkOutSchemeWithoutEditable;
    set => this._checkOutSchemeWithoutEditable = value;
  }

  public override IDBObject DoCheckout()
  {
    string key = $"WFScheme_InCheckOut_{this.ObjectID}";
    try
    {
      this.UserSession.SetSessionPluginsData((object) key, (object) true);
      if (this._checkOutSchemeWithoutEditable == wfConsts.CheckOutMode)
      {
        if (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) is IEventLogHelper service)
          service.AddToTrace($"Workflow Scheme CheckOut: \"Вызван метод взятия шаблона процесса на редактирование без проверки можно ли редактировать. ID шаблона '{this.ObjectID}'. Пользователь: '{this.UserSession.UserName}'. ID пользователя '{this.UserSession.UserID}'.\"", Intermech.Consts.traceAlways, "wfActivityProxy.log");
      }
      else
        this.CheckIsEditable();
      this.ForceGetActivities = true;
      List<WFActivity> activities = this.Activities;
      this.ForceGetActivities = false;
      for (int index = 0; index < activities.Count; ++index)
        activities[index].CheckOut();
      for (int index = 0; index < this.AllLinks.Count; ++index)
        this.AllLinks[index].CheckOut();
      return base.DoCheckout();
    }
    finally
    {
      this.UserSession.RemoveSessionPluginsData((object) key);
    }
  }

  protected override void DoCheckIn()
  {
    this.DeleteExtraArcCopies();
    this.ForceGetActivities = true;
    List<WFActivity> activities = this.Activities;
    this.ForceGetActivities = false;
    foreach (DBObject dbObject in activities)
      dbObject.CheckIn();
    for (int index = 0; index < this.AllLinks.Count; ++index)
      this.AllLinks[index].CheckIn();
    base.DoCheckIn();
  }

  protected override void DoDelete()
  {
    this.CheckIsEditable();
    this.DeleteActivities();
    base.DoDelete();
  }

  protected void DeleteActivities()
  {
    bool flag = this is WFProcess && GlobalMailSettings.Cfg.DeleteFileLinkObjects;
    long[] array = this.Activities.Select<WFActivity, long>((System.Func<WFActivity, long>) (x => x.ObjectID)).ToArray<long>();
    for (int index1 = this.Activities.Count - 1; index1 >= 0; --index1)
    {
      if (flag)
      {
        List<Attachment> list = this.Activities[index1].Attachments.Where<Attachment>((System.Func<Attachment, bool>) (x => x.TypeID == wfConsts.FileTypeID)).ToList<Attachment>();
        if (list.Count > 0)
        {
          for (int index2 = 0; index2 < list.Count; ++index2)
          {
            Attachment attachment = list[index2];
            IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(-1);
            relationCollection.LocalTypesMode = true;
            object[] columns = new object[1]
            {
              (object) ObligatoryObjectAttributes.F_OBJECT_ID
            };
            DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
            {
              new ConditionStructure(-2, RelationalOperators.NotIn, (object) array, LogicalOperators.NONE, 0, false)
            }, columns);
            if (relationCollection.EntersIn(paramSet, attachment.ID).Rows.Count == 0)
              this.UserSession.GetObject(attachment.ObjectID, false)?.Delete((long) (Intermech.Consts.PurgeMode | 16 /*0x10*/));
          }
        }
      }
      if (this.Activities[index1].InternalDelete(true))
        this.Activities.RemoveAt(index1);
    }
  }

  private void DeleteExtraArcCopies()
  {
    List<long> childrenList1 = this.GetChildrenList(wfConsts.ActivitiesTypeID, -this.ObjectID);
    List<long> childrenList2 = this.GetChildrenList(wfConsts.LinksTypeID, -this.ObjectID);
    childrenList1.AddRange((IEnumerable<long>) childrenList2);
    if (childrenList1.Count <= 0)
      return;
    this.DeleteObjects(childrenList1.ToArray());
  }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    if (anAction == ActionType.wfLaunchProcess && this.ObjectType == wfConsts.SchemesTypeID)
    {
      long processCategory = MiscFunx.GetProcessCategory(this.Session, this.ObjectID);
      if (processCategory != 0L && this.Session.GetObject(processCategory, false) is DBObject dbObject)
        dbObject.CheckAccess(anAction);
    }
    return base.CheckAccess(anAction, aDefaultAccess, flags);
  }

  private void CheckIsEditable()
  {
    IDBAttribute byId = this.Attributes.FindByID(wfConsts.AttrIsDebugID);
    if (byId != null && byId.AsBoolean)
      return;
    ConditionStructure[] conds = new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrPrototypeID, RelationalOperators.Equal, (object) this.ObjectID, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID),
      new ConditionStructure(wfConsts.AttrCreateActivitiesOnDemandID, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, false)
    };
    DataTable dataTable = MiscFunx.SimpleSelect(this.Session, wfConsts.ProcessesTypeID, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, conds, recordCount: -2);
    if (dataTable.Rows.Count > 0)
    {
      long[] objectsID = new long[dataTable.Rows.Count];
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        objectsID[index] = (long) Convert.ToInt32(dataTable.Rows[index][0]);
      throw new WorkflowMakeBaseVersionException($"Шаблон процесса '{this.Caption}' заблокирован от изменений, т.к. имеются процессы на его основе. Модификация шаблона может быть возможна только с созданием новой версии или после удаления всех порожденных процессов.", objectsID, this.ObjectID, "Ошибка");
    }
  }

  public void CopyFromPrototype(IDBObject parent)
  {
    this.prototype = parent as WFScheme;
    this.Attributes.AddAttribute(wfConsts.AttrPrototypeID, false, new object[1]
    {
      (object) parent
    });
  }

  public override void CommitCreation(bool deleteOnException, bool autoCheckout)
  {
    if (this._variablesInit)
      this._variablesInit = false;
    this.SaveVariables(false);
    this._variables = (VarList) null;
    this._variablesInit = false;
    this._Attributes = (IDBAttributeCollection) null;
    base.CommitCreation(deleteOnException, autoCheckout);
  }

  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    bool createSchemeVersion = true;
    if (this.prototype == null)
    {
      createSchemeVersion = false;
      IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrPrototypeID);
      if (attributeById != null)
        this.prototype = this.UserSession.GetObject(attributeById.AsInteger, false) as WFScheme;
    }
    if (this.prototype != null)
      this.CopyStuffFromPrototype(this.prototype, createSchemeVersion);
    this.prototype = (WFScheme) null;
    if (createSchemeVersion)
    {
      foreach (DataRow row1 in (InternalDataCollectionBase) this.GetScripts(this.Activities.Select<WFActivity, long>((System.Func<WFActivity, long>) (act => act.ObjectID)).ToList<long>()).Rows)
      {
        DataRow row = row1;
        if (Convert.ToInt32(row.ItemArray[2]) == wfConsts.WorkflowLocalScript)
        {
          IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row.ItemArray[3]));
          List<WFActivity> list = this.Activities.Where<WFActivity>((System.Func<WFActivity, bool>) (a => Math.Abs(a.ObjectID) == Math.Abs(Convert.ToInt64(row.ItemArray[0])))).ToList<WFActivity>();
          IDBObject prototype = this.UserSession.GetObject(Convert.ToInt64(row.ItemArray[1]));
          if (list.Count > 0)
          {
            string str1 = string.Empty;
            if (list[0].ObjectType != wfConsts.ScriptTypeID)
            {
              long num = -1;
              IDBAttribute attributeById = relation.GetAttributeByID(wfConsts.AttrScriptKindID);
              if (attributeById != null)
                num = attributeById.AsInteger;
              string str2;
              switch (num)
              {
                case -1:
                  goto label_17;
                case 0:
                  str2 = "[Перед] ";
                  break;
                default:
                  str2 = "[После] ";
                  break;
              }
              str1 = str2;
            }
label_17:
            IDBObject dbObject = this.UserSession.GetObjectCollection(prototype.TypeID).Create(prototype);
            dbObject.Caption = $"{str1}{this.Caption}{(this.VersionID > 0 ? $" [{this.VersionID}]" : string.Empty)}. {list[0].Caption}";
            dbObject.CommitCreation(true, false);
            relation.ReplacePartObject(dbObject.ObjectID);
          }
        }
      }
    }
    for (int index = 0; index < this.AllLinks.Count; ++index)
    {
      if (this.AllLinks[index].IsCreationMode)
        this.AllLinks[index].CommitCreation(false);
    }
  }

  public virtual void CopyStuffFromPrototype(WFScheme prototype, bool createSchemeVersion = false)
  {
    this._activities = new List<WFActivity>();
    List<WFActivity> activities = prototype.Activities;
    this._links = prototype.AllLinks;
    IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(wfConsts.LinksTypeID);
    List<WFLink> wfLinkList = new List<WFLink>();
    for (int index = 0; index < activities.Count; ++index)
      this.CreateActivity(activities[index], createSchemeVersion: createSchemeVersion);
    for (int index = 0; index < this._links.Count; ++index)
    {
      WFLink wfLink = objectCollection.Create((IDBObject) this._links[index]) as WFLink;
      wfLink.FromID = this._links[index].FromID;
      wfLink.ToID = this._links[index].ToID;
      wfLink.OldObjectID = Math.Abs(this._links[index].ObjectID);
      wfLinkList.Add(wfLink);
      wfLink.Attributes.FindByID(wfConsts.AttrProcessID).AsInteger = Math.Abs(this.ObjectID);
      wfLink.ProjectID = this.ProjectID;
    }
    for (int index1 = 0; index1 < activities.Count; ++index1)
    {
      for (int index2 = 0; index2 < wfLinkList.Count; ++index2)
      {
        if (wfLinkList[index2].FromID == Math.Abs(activities[index1].ObjectID))
        {
          if (this._activities.Count <= index1)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_44"), (object) wfLinkList[index2].From.Caption, (object) (wfLinkList[index2].From.ObjectID.ToString() + "*")));
          wfLinkList[index2].FromID = this._activities[index1].ObjectID;
        }
        else if (wfLinkList[index2].ToID == Math.Abs(activities[index1].ObjectID))
        {
          if (this._activities.Count <= index1)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_44"), (object) wfLinkList[index2].To.Caption, (object) (wfLinkList[index2].To.ObjectID.ToString() + "*")));
          wfLinkList[index2].ToID = this._activities[index1].ObjectID;
        }
      }
    }
    for (int index = 0; index < wfLinkList.Count; ++index)
      wfLinkList[index].CommitCreation(false);
    for (int index = 0; index < wfLinkList.Count; ++index)
      wfLinkList[index].Copied();
    for (int index = 0; index < this._activities.Count; ++index)
      this._activities[index].Copied();
    this.AfterCopyStuffFromPrototype();
    if (!(this.GetType() == typeof (WFScheme)))
      return;
    long processCategory = MiscFunx.GetProcessCategory((IUserSession) this.UserSession, prototype.ObjectID);
    if (processCategory == 0L)
      return;
    bool autoRollback = this.UserSession.AutoRollback;
    this.UserSession.AutoRollback = false;
    try
    {
      MiscFunx.AddProcessToCategory((IUserSession) this.UserSession, this.ObjectID, processCategory);
    }
    finally
    {
      this.UserSession.AutoRollback = autoRollback;
    }
  }

  protected virtual void AfterCopyStuffFromPrototype()
  {
    for (int index = 0; index < this._activities.Count; ++index)
      this._activities[index].CommitCreation(false);
  }

  private DataTable GetScripts(List<long> objectIDs)
  {
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(wfConsts.ScriptRelationTypeID);
    relationCollection.LocalTypesMode = true;
    object[] columns = new object[4]
    {
      (object) ObligatoryObjectAttributes.F_PROJ_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.In, (object) objectIDs.ToArray(), LogicalOperators.AND, 0, false)
    }, columns, 0L, (object) null, -1);
    return relationCollection.Select(paramSet);
  }

  public IActivity CreateActivity(
    IActivity proto,
    bool setAsParent = false,
    bool createSchemeVersion = false,
    long ownerID = -1)
  {
    return (IActivity) this.CreateActivity(proto as WFActivity, setAsParent, createSchemeVersion, ownerID);
  }

  internal WFActivity CreateActivity(
    WFActivity proto,
    bool setAsParent = false,
    bool createSchemeVersion = false,
    long ownerID = -1)
  {
    WFActivity act = this.UserSession.GetObjectCollection(proto.TypeID).Create((IDBObject) proto) as WFActivity;
    if (createSchemeVersion && act.FormID != 0L)
    {
      IDBObject prototype = this.UserSession.GetObject(act.FormID, false);
      if (prototype != null)
      {
        IDBObject dbObject = this.UserSession.GetObjectCollection(prototype.TypeID).Create(prototype);
        dbObject.Caption = $"{this.Caption}{(this.VersionID > 0 ? $" [{this.VersionID}]" : string.Empty)}. {act.Name}";
        dbObject.CommitCreation(true, false);
        act.FormID = dbObject.ObjectID;
      }
    }
    this.AddActivity(act);
    act.Attachments.AddList(proto.Attachments, false);
    act.SaveAttachments();
    act.ProcessID = Math.Abs(this.ObjectID);
    if (this._startActivity == null && act is Start start)
      this._startActivity = start;
    act.ProjectID = this.ProjectID;
    if (setAsParent)
    {
      act.GetAttributeByID(wfConsts.AttrGraphDataID)?.Delete(0L);
      act.ParentActivityID = proto.ObjectID;
    }
    if (ownerID != -1L)
      act.OwnerID = ownerID;
    if (this is WFProcess)
    {
      // ISSUE: explicit non-virtual call
      act.Priority = __nonvirtual (((WFProcess) this).Priority);
    }
    this._objectGuidMapper.Add(proto.ObjectGUID, act.ObjectGUID);
    return act;
  }

  protected override void DoPurge(long DeleteMode)
  {
    if (this.CheckoutBy != 0L)
    {
      List<long> longList = new List<long>();
      if ((DeleteMode & 16L /*0x10*/) == 16L /*0x10*/ && this.CheckoutBy != this.Session.UserID)
      {
        this._showNotOwnedWorkCopies = true;
        try
        {
          longList = this.GetChildrenList(wfConsts.ActivitiesTypeID, this.ObjectID);
          longList.AddRange((IEnumerable<long>) this.GetChildrenList(wfConsts.LinksTypeID, this.ObjectID));
        }
        finally
        {
          this._showNotOwnedWorkCopies = false;
        }
        foreach (long objectID in longList)
          this.Session.GetObject(objectID, false)?.CancelChanges(true);
      }
      else
      {
        List<DBObject> dbObjectList = new List<DBObject>();
        DateTime checkOutDate = this.GetCheckOutDate();
        dbObjectList.AddRange((IEnumerable<DBObject>) this.Activities.ToArray());
        dbObjectList.AddRange((IEnumerable<DBObject>) this._links.ToArray());
        foreach (DBObject dbObject in dbObjectList)
        {
          if (dbObject.GetCheckOutDate() > checkOutDate)
          {
            long objectId = dbObject.ObjectID;
            longList.Add(objectId);
            longList.Add(-objectId);
          }
        }
        this.DeleteObjects(longList.ToArray());
        foreach (WFActivity activity in this.Activities)
        {
          if (activity.CheckoutBy != 0L)
            activity.CancelChanges((DeleteMode & 16L /*0x10*/) == 16L /*0x10*/);
        }
        for (int index = 0; index < this.AllLinks.Count; ++index)
        {
          if (this.AllLinks[index].CheckoutBy != 0L)
            this.AllLinks[index].CancelChanges((DeleteMode & 16L /*0x10*/) == 16L /*0x10*/);
        }
      }
    }
    base.DoPurge(DeleteMode);
  }

  public void DeleteObjects(long[] ids)
  {
    for (int index = 0; index < ids.Length; ++index)
    {
      long objID = ids[index];
      WFActivity wfActivity1 = this.Activities.FirstOrDefault<WFActivity>((System.Func<WFActivity, bool>) (x => x.ObjectID == objID));
      if (objID < 0L)
        wfActivity1 = this.UserSession.GetObject(Math.Abs(objID), false) as WFActivity;
      WFLink wfLink = (WFLink) null;
      bool flag = wfActivity1 != null;
      if (!flag)
      {
        IDBObject dbObject = this.UserSession.GetObject(objID, false);
        if (dbObject != null)
        {
          if (!(dbObject is WFScheme))
          {
            wfActivity1 = dbObject as WFActivity;
            wfLink = dbObject as WFLink;
          }
          else
            continue;
        }
      }
      if (wfActivity1 != null)
      {
        if (!flag)
        {
          long? objectId = wfActivity1.Process?.ObjectID;
          long num = Math.Abs(this.ObjectID);
          if (!(objectId.GetValueOrDefault() == num & objectId.HasValue))
            continue;
        }
        if (wfActivity1.InternalDelete(true) && flag)
        {
          WFActivity wfActivity2 = this._activities.Find((Predicate<WFActivity>) (x => x.ObjectID == objID));
          if (wfActivity2 != null)
            this._activities.Remove(wfActivity2);
        }
      }
      else if (wfLink != null && wfLink.ProcessID == Math.Abs(this.ObjectID))
        wfLink.InternalDelete(true);
    }
  }

  public void DeleteObject(long id)
  {
    this.DeleteObjects(new long[1]{ id });
  }

  IActivity[] IScheme.Activities
  {
    get
    {
      int count = this.Activities.Count;
      IActivity[] activities = new IActivity[count];
      for (int index = 0; index < count; ++index)
        activities[index] = (IActivity) this.Activities[index];
      return activities;
    }
  }

  IDBObject[] IScheme.AllLinks
  {
    get
    {
      int count = this.AllLinks.Count;
      IDBObject[] allLinks = new IDBObject[count];
      for (int index = 0; index < count; ++index)
        allLinks[index] = (IDBObject) this.AllLinks[index];
      return allLinks;
    }
  }

  public long LinkedTaskObjectID
  {
    get
    {
      if (this._linkedTaskObjectID == -1L)
      {
        IDBRelationCollection relationCollection = this.Session.GetRelationCollection(wfConsts.LinkedTaskRelationTypeID);
        relationCollection.ObjectTypeID = ObjectTypes.Task.ID;
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        }, (object[]) null, (SortOrders[]) null);
        IEnumerator enumerator = relationCollection.EntersInVersion(paramSet, this.ObjectID).Rows.GetEnumerator();
        try
        {
          if (enumerator.MoveNext())
            this._linkedTaskObjectID = (long) Convert.ToInt32(((DataRow) enumerator.Current)[0]);
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
      }
      return this._linkedTaskObjectID;
    }
  }

  IActivity IScheme.StartActivity => (IActivity) this.StartActivity;

  public Start StartActivity
  {
    get
    {
      this._startActivity = (Start) null;
      foreach (WFActivity activity in this.Activities)
      {
        if (activity is Start start)
        {
          this._startActivity = start;
          break;
        }
      }
      return this._startActivity;
    }
  }

  IVariables IScheme.Variables => (IVariables) new RemotingVarList(this.Variables);

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this._actionCategories.Add(ActionType.Edit, ActionCategory.Write);
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this._actionCategories.Add(ActionType.wfAdminProcess, ActionCategory.Admin);
    this._actionCategories.Add(ActionType.wfLaunchProcess, ActionCategory.Read);
    this.AccessActions.Add(ActionType.wfAdminProcess, this.GetDefaultAccess(ActionType.wfAdminProcess));
    if (this.ObjectType != wfConsts.SchemesTypeID)
      return;
    this.AccessActions.Add(ActionType.wfLaunchProcess, this.GetDefaultAccess(ActionType.wfLaunchProcess));
  }

  public override bool GetDefaultAccess(ActionType at)
  {
    if (this.UserSession.IsAdmin)
      return true;
    ActionCategory actionCategory = ActionCategory.NotDefined;
    if (this._actionCategories.ContainsKey(at))
      actionCategory = this._actionCategories[at];
    return actionCategory == ActionCategory.NotDefined ? base.GetDefaultAccess(at) : actionCategory == ActionCategory.Read;
  }

  public IActivity GetActivity(long id) => (IActivity) this.GetDBActivity(this.ObjectID);

  public int AddGlobalVariable(string name, VarType type, object[] addInfo, int attributeTypeID)
  {
    if (attributeTypeID == 0)
    {
      try
      {
        attributeTypeID = VarsHelper.CreateVariableType((IUserSession) this.UserSession, name, type, varKind: VarKind.Global);
      }
      catch (KernelException ex)
      {
        throw new KernelException(ex.Message, (Exception) ex);
      }
    }
    IDBAttribute dbAttribute = this.Attributes.AddAttribute(attributeTypeID, false);
    if (addInfo.Length != 0)
    {
      switch (type)
      {
        case VarType.StringList:
          string str = WFScheme.SetPossibleValuesStringList(addInfo, dbAttribute.AttributeType);
          dbAttribute.Value = (object) str;
          break;
        case VarType.Boolean:
          dbAttribute.Value = (object) (addInfo[0].ToString() != "0");
          break;
        case VarType.Archive:
          Guid result;
          if (Guid.TryParse(addInfo[0].ToString(), out result))
          {
            dbAttribute.Value = (object) result;
            break;
          }
          break;
        default:
          dbAttribute.Values = addInfo;
          break;
      }
    }
    return attributeTypeID;
  }

  public int AddVariable(string name, VarType type, object[] addInfo)
  {
    int variableType;
    try
    {
      variableType = VarsHelper.CreateVariableType((IUserSession) this.UserSession, name, type);
    }
    catch (KernelException ex)
    {
      throw new KernelException(ex.Message, (Exception) ex);
    }
    this.UseVariable(variableType, addInfo);
    return variableType;
  }

  public int UseVariable(int TypeID, object[] addInfo)
  {
    IDBAttributeType attributeType = this.UserSession.GetAttributeTypeCollection(0).GetAttributeType((object) TypeID, true);
    VarType varType = MiscFunx.DetermineVarType(attributeType);
    if (varType == VarType.StringList && addInfo.Length != 0)
      WFScheme.SetPossibleValuesStringList(addInfo, attributeType);
    Variable variable = this.Variables.AddVariable(TypeID);
    variable.VarType = varType;
    variable.AddInfo = addInfo;
    if (variable is SystemVariable && !variable.Calculated)
      ((SystemVariable) variable).Save();
    this.SaveVariables();
    return TypeID;
  }

  private static string SetPossibleValuesStringList(object[] addInfo, IDBAttributeType t)
  {
    if (t.MultipleValued != MultiValueModes.SingleValueFromList)
      t.MultipleValued = MultiValueModes.SingleValueFromList;
    DataTable possibleValues = t.GetPossibleValues();
    StringList stringList = new StringList();
    stringList.Text = addInfo[0].ToString();
    possibleValues.Rows.Clear();
    string empty = string.Empty;
    if (stringList.Count > 0)
    {
      empty = stringList[0];
      stringList.RemoveAt(0);
    }
    if (stringList.Contains(string.Empty))
    {
      t.Options &= ~AttributeOptions.DisableNulls;
      stringList.Remove(string.Empty);
    }
    else
      t.Options |= AttributeOptions.DisableNulls;
    for (int index = 0; index < stringList.Count; ++index)
      possibleValues.Rows.Add((object) (index + 1), (object) stringList[index], (object) string.Empty);
    t.SetNewPossibleValues(possibleValues);
    return empty;
  }

  public void SaveVariables(bool clearAttributes = true)
  {
    if (this.Variables == null || !this.Variables.Modified)
      return;
    this.Variables.Save((IDBObject) this, true);
    this.Variables.Modified = false;
    if (!clearAttributes)
      return;
    this._Attributes = (IDBAttributeCollection) null;
  }

  public void DeleteVariable(int TypeID)
  {
    this.Variables.DeleteVariable(TypeID);
    this.SaveVariables();
  }

  public void DeleteGlobalVariable(int typeID) => this.Attributes.FindByID(typeID)?.Delete(0L);

  private void AddVirtualAttributes()
  {
    string key = $"WFScheme_AddVirtualAttributes_{this.ObjectID}";
    if (object.Equals(this.UserSession.GetSessionPluginsData((object) key), (object) true))
      return;
    this.UserSession.SetSessionPluginsData((object) key, (object) true);
    try
    {
      this.DoAddVirtualAttributes();
    }
    finally
    {
      this.UserSession.RemoveSessionPluginsData((object) key);
    }
  }

  private void DoAddVirtualAttributes() => this.Variables.AddVirtualAttributes((IDBObject) this);

  public virtual bool CanEditAttributes() => this.IsCreationMode;

  public override IDBAttribute GetAttributeByID(int attributeID)
  {
    string key = $"WFScheme_LoadingVars_{this.ObjectID}";
    IDBAttribute attributeById = base.GetAttributeByID(attributeID);
    if (attributeById == null && !object.Equals(this.UserSession.GetSessionPluginsData((object) key), (object) true) && this.Variables.GetVariable(attributeID) != null)
    {
      this.AddVirtualAttributes();
      attributeById = this.Attributes.FindByID(attributeID);
    }
    return attributeById;
  }

  public AttributeValues[] GetGlobalAttributesValues(
    GetAttributeValuesModes modes,
    AttributeValues[] activityValueses,
    List<int> editableVarIDs)
  {
    List<AttributeValues> list1 = ((IEnumerable<AttributeValues>) activityValueses).ToList<AttributeValues>();
    AttributeValues[] attributesValues = this.GetAttributesValues(modes);
    List<AttributeValues> collection = new List<AttributeValues>();
    foreach (Variable globalVariable1 in (VarList) this.GlobalVariables)
    {
      Variable globalVariable = globalVariable1;
      List<AttributeValues> list2 = ((IEnumerable<AttributeValues>) attributesValues).Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == globalVariable.AttrTypeID)).ToList<AttributeValues>();
      if (list2.Count > 0)
      {
        AttributeValues attributeValues = list2[0];
        attributeValues.ReadOnly = false;
        collection.Add(attributeValues);
        editableVarIDs.Add(attributeValues.AttributeID);
      }
    }
    list1.AddRange((IEnumerable<AttributeValues>) collection);
    return list1.ToArray();
  }

  public override AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
  {
    this.AddVirtualAttributes();
    AttributeValues[] attributesValues = base.GetAttributesValues(modes);
    if (this.ObjectType == wfConsts.ProcessesTypeID && (modes & GetAttributeValuesModes.RequestedByForm) == GetAttributeValuesModes.RequestedByForm)
    {
      foreach (Variable globalVariable1 in (VarList) this.GlobalVariables)
      {
        Variable globalVariable = globalVariable1;
        if (globalVariable.VarType == VarType.DateTime && string.IsNullOrEmpty(globalVariable.Value))
        {
          List<AttributeValues> list = ((IEnumerable<AttributeValues>) attributesValues).Where<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == globalVariable.AttrTypeID)).ToList<AttributeValues>();
          if (list.Count > 0)
          {
            bool flag = false;
            DateTime now = DateTime.Now;
            for (int index = 0; index < list.Count; ++index)
            {
              if (list[index].Value == DBNull.Value)
              {
                list[index].Values = new object[1]
                {
                  (object) now
                };
                flag = true;
              }
            }
            if (flag)
              globalVariable.AsDateTime = now;
          }
        }
      }
    }
    if (this.IsCreationMode)
      return attributesValues;
    List<int> editableVarIds = this.Variables.EditableVarIDs;
    editableVarIds.AddRange(this.GlobalVariables.Select<Variable, int>((System.Func<Variable, int>) (x => x.AttrTypeID)));
    bool flag1 = this.CanEditAttributes();
    if (this.ObjectType == wfConsts.SchemesTypeID && (modes & GetAttributeValuesModes.RequestedByForm) == GetAttributeValuesModes.RequestedByForm)
      flag1 = true;
    bool flag2 = false;
    foreach (AttributeValues attributeValues in attributesValues)
    {
      AttributeValues val = attributeValues;
      if (val.AttributeType != FieldTypes.ftSystem && val.AttributeID != wfConsts.SchemeAdministratorID)
      {
        val.ReadOnly = !flag1 || !editableVarIds.Contains(val.AttributeID);
        if (editableVarIds.Contains(val.AttributeID) && this.GlobalVariables.Count<Variable>((System.Func<Variable, bool>) (x => x.AttrTypeID == val.AttributeID)) > 0 && val.AttributeType == FieldTypes.ftDateTime && val.Value == DBNull.Value)
          val.Values = new object[1]
          {
            (object) DateTime.Now
          };
        if (!flag1 && !flag2 && wfConsts.ProtectedAttributeTypes.Contains(val.AttributeID))
          flag2 = true;
      }
    }
    if (!flag2)
      return attributesValues;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttributeValues attributeValues in attributesValues)
    {
      if (!wfConsts.ProtectedAttributeTypes.Contains(attributeValues.AttributeID))
        attributeValuesList.Add(attributeValues);
    }
    return attributeValuesList.ToArray();
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    this.AddVirtualAttributes();
    List<int> attributesInGroup = MetaDataHelper.GetAttributesInGroup(wfConsts.GlobalVariablesGroupID);
    foreach (AttributeValues values in valuesList)
    {
      if (values.AttributeID != wfConsts.SchemeAdministratorID && !attributesInGroup.Contains(values.AttributeID))
      {
        for (int index = 0; index < values.Values.Length; ++index)
        {
          if (DeleteModesEnum.None.Equals(values.Values[index]))
            values.Values[index] = (object) DBNull.Value;
        }
      }
    }
    string key = $"WFScheme_InSetAttrValues_{this.ObjectID}";
    this.UserSession.SetSessionPluginsData((object) key, (object) true);
    try
    {
      AttributeValues[] attributeValuesArray = base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
      if (this.Variables.FillByVirtualAttributes((IDBObject) this))
        this.SaveVariables(false);
      return attributeValuesArray;
    }
    finally
    {
      this.UserSession.RemoveSessionPluginsData((object) key);
    }
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }

  protected void UpdateTempAttributeValue(int AttrTypeID)
  {
    if (this.Variables == null)
      return;
    Variable variable = this.Variables.GetVariable(AttrTypeID);
    if (variable == null)
      return;
    if (variable is CalculatedSystemVariable)
      (variable as CalculatedSystemVariable).ClearCache();
    if (!this.Variables.VirtualAdded)
      return;
    IDBAttribute byId = this.Attributes.FindByID(AttrTypeID);
    if (byId == null)
      return;
    byId.Value = variable.TypedValue;
  }

  public override IDBAttributeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
      {
        this._Attributes = (IDBAttributeCollection) new ActivityAttributeCollection(this.UserSession, this.ObjectID, this.ObjectType, (IDBAttributable) this);
        this.AddVirtualAttributes();
      }
      return this._Attributes;
    }
  }

  public long SaveAs(long newID, string name)
  {
    bool flag = false;
    this._loaded = false;
    WFScheme wfScheme;
    if (newID == 0L)
    {
      wfScheme = this.UserSession.GetObjectCollection(this.TypeID).Create((IDBObject) this) as WFScheme;
    }
    else
    {
      wfScheme = this.UserSession.GetObject(newID, false) as WFScheme;
      flag = true;
    }
    if (wfScheme != null)
    {
      try
      {
        if (flag)
        {
          wfScheme = wfScheme.CheckOut() as WFScheme;
          wfScheme.DeleteActivities();
          wfScheme.Attributes.Assign(this.Attributes);
          wfScheme.CopyStuffFromPrototype(this);
          foreach (DBObject activity in wfScheme.Activities)
            activity.CheckOut();
          if (!string.IsNullOrEmpty(name))
            wfScheme.Caption = name;
        }
        else
        {
          wfScheme.CopyFromPrototype((IDBObject) this);
          if (!string.IsNullOrEmpty(name))
            wfScheme.Caption = name;
          wfScheme.CommitCreation(true);
          wfScheme = wfScheme.CheckOut() as WFScheme;
        }
      }
      catch
      {
        if (flag)
          wfScheme.CancelChanges();
        else
          wfScheme.Delete(0L);
        throw;
      }
    }
    return wfScheme != null ? wfScheme.ObjectID : 0L;
  }

  public virtual string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null)
  {
    int num = 0;
    bool flag = false;
    string str1 = string.Empty;
    string empty1 = string.Empty;
    this._allLinksLoaded = false;
    foreach (WFActivity activity in this.Activities)
    {
      WFActivity act = activity;
      if (act.Kind == ActivityKind.Start)
        ++num;
      if (!flag && act.Kind == ActivityKind.Stop)
        flag = true;
      string s = act.Validate(checkSubProcessSchemes, checkedSchemesList);
      HashSet<long> hashSet = act.AllLinksFromThis.Select<WFLink, long>((System.Func<WFLink, long>) (x => Math.Abs(x.ObjectID))).Except<long>(((IEnumerable<long>) this._validateDeleteObjectsAndLinks).Select<long, long>(new System.Func<long, long>(Math.Abs))).ToHashSet<long>();
      if (act.Kind != ActivityKind.Stop && act.Kind != ActivityKind.Abort && (act.AllLinksFromThis.Count == 0 || hashSet.Count == 0) && (this._allBlankLinks.Count == 0 || this._allBlankLinks.Count<WFLink>((System.Func<WFLink, bool>) (x => x.FromID == Math.Abs(act.ObjectID))) == 0))
        MiscFunx.AddNewLined(ref s, string.Format(LocalizationHolder.rm.GetString("Workflow.Server_21"), (object) act.Name));
      if (!string.IsNullOrEmpty(s))
      {
        string str2 = $" - {act.Name}\r\n{s}";
        if (!string.IsNullOrEmpty(str1))
          str1 += "\r\n";
        str1 += str2;
      }
    }
    string empty2 = string.Empty;
    if (this.Variables.Invalid || this.GlobalVariables.Invalid)
      MiscFunx.AddNewLined(ref empty2, LocalizationHolder.GetString("InvalidVariables"));
    if (num == 0)
      MiscFunx.AddNewLined(ref empty2, LocalizationHolder.rm.GetString("Workflow.Server_22"));
    else if (num > 1)
      MiscFunx.AddNewLined(ref empty2, LocalizationHolder.rm.GetString("Workflow.Server_23"));
    if (!flag)
      MiscFunx.AddNewLined(ref empty2, LocalizationHolder.rm.GetString("Workflow.Server_24"));
    if (!string.IsNullOrEmpty(empty2))
      str1 = $" - {this.Name}\r\n{empty2}\r\n" + str1;
    return str1;
  }

  public string Validate(
    long[] blankActIDs,
    long[] blankLinkIDs,
    long[] deleted,
    bool checkSubProcessSchemes = true)
  {
    this._allBlankLinks.Clear();
    this.Load(blankActIDs, blankLinkIDs, deleted);
    if (blankLinkIDs != null)
    {
      foreach (long blankLinkId in blankLinkIDs)
      {
        if (!((IEnumerable<long>) deleted).Contains<long>(blankLinkId))
        {
          IDBObject dbObject = this.UserSession.GetObject(blankLinkId);
          WFLink wfLink = dbObject as WFLink;
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(wfConsts.AttrToActivityID);
          if (wfLink != null)
          {
            if (attributeById1 != null)
              wfLink.ToID = attributeById1.AsInteger;
            IDBAttribute attributeById2 = dbObject.GetAttributeByID(wfConsts.AttrFromActivityID);
            if (attributeById2 != null)
              wfLink.FromID = attributeById2.AsInteger;
            IDBAttribute attributeById3 = dbObject.GetAttributeByID(wfConsts.AttrLinkKindID);
            if (attributeById3 != null)
              wfLink.Kind = (LinkKind) attributeById3.AsInteger;
            this._allBlankLinks.Add(wfLink);
          }
        }
      }
    }
    this._validateDeleteObjectsAndLinks = deleted;
    return this.Validate(checkSubProcessSchemes, (List<long>) null);
  }

  public bool IsValid() => string.IsNullOrEmpty(this.Validate(true, (List<long>) null));

  public void ForwardDataFlow(WFActivity toAct)
  {
    IDBAttribute attributeById = this.GetAttributeByID(wfConsts.AttrExecHistoryID);
    IDBAttribute dbAttribute = toAct.Attributes.AddAttribute(wfConsts.AttrExecHistoryID, false);
    if (toAct.Collector && dbAttribute != null && !dbAttribute.IsNull && attributeById != null)
    {
      object[] values1 = attributeById.Values;
      object[] values2 = dbAttribute.Values;
      int length1 = values1.Length;
      bool flag = false;
      for (int index = 0; index < length1; ++index)
      {
        object val = values1[index];
        if (!Array.Exists<object>(values2, (Predicate<object>) (obj => obj.Equals(val))))
        {
          int length2 = values2.Length;
          Array.Resize<object>(ref values2, length2 + 1);
          values2[length2] = val;
          flag = true;
        }
      }
      if (flag)
        ((DBAttribute) dbAttribute).DirectSetValues(values2);
    }
    else if (attributeById != null)
      ((DBAttribute) dbAttribute)?.DirectSetValues(attributeById.Values);
    if (dbAttribute != null)
    {
      if (dbAttribute.IsNull)
        dbAttribute.Value = (object) this.ObjectID;
      else if (!Array.Exists<object>(dbAttribute.Values, (Predicate<object>) (obj => obj.Equals((object) this.ObjectID))))
        dbAttribute.AddValue((object) this.ObjectID);
    }
    if (!(this is WFProcess wfProcess))
      return;
    toAct.Priority = wfProcess.Priority;
  }
}
