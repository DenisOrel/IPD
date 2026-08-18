// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.VisTask
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.Compositions;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.Pdm.Server;

internal class VisTask
{
  private IUserSession _session;
  private IUserSession _clonedSession;
  private CompositionTaskBooster _task;
  private long _state;
  internal long ProjId;
  internal long ProjVId;
  internal string ClientFiltrOwnerId;
  internal ICompositionsAutosortRule Rule;
  internal bool ClientShowHiddenObjs;
  internal bool ClientShowHiddenSostav;
  internal RelFilter RelFilter;
  internal HybridDictionary Dict;
  internal long[] ObjIds;
  private int levelsOverride;
  private readonly BackgroundWorker _bw;
  private readonly BackgroundWorker _pw;
  public int PreviewMode = 1;
  private readonly byte[] noPreview = new byte[0];
  private static readonly string _relTypeCAD = "cadd94da-306c-11d8-b4e9-00304f19f545";
  private int _idRelTypeCad;
  private readonly string StructLinksKey = "RELVISSHOWSTRUCTURELINKS";
  private readonly string AssocLinksKey = "RELVISSHOWASSOCIATIVELINKS";
  private readonly string attributeFlag = "cad0147c-306c-11d8-b4e9-00304f19f545";
  private readonly string _wrongSession = "Session == null";

  public bool SeekChilds { get; set; }

  public long TaskId { get; private set; }

  public long VisSchemeId { get; private set; }

  public IUserSession Session
  {
    get => this._clonedSession ?? this._session;
    private set => this._session = value;
  }

  public Exception Error { get; private set; }

  public RelVisState State
  {
    get => (RelVisState) Interlocked.Read(ref this._state);
    set => Interlocked.Exchange(ref this._state, (long) value);
  }

  public HybridTableExp ResTable { get; set; }

  public VisTask(object session, bool childs, long taskid, long schemeId, long[] objIds = null)
  {
    this.SeekChilds = childs;
    this.TaskId = taskid;
    this.VisSchemeId = schemeId;
    this._session = this.GetUserSession(session);
    this._task = (CompositionTaskBooster) null;
    this.State = RelVisState.Unknown;
    this.ResTable = (HybridTableExp) null;
    this.ObjIds = objIds;
    this._bw = new BackgroundWorker();
    if (objIds != null)
      this._bw.DoWork += new DoWorkEventHandler(this._DoLoadPreviews);
    else
      this._bw.DoWork += new DoWorkEventHandler(this._DoLoadData);
    this._bw.WorkerReportsProgress = false;
    this._bw.WorkerSupportsCancellation = true;
    this._bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.Bw_RunWorkerCompleted);
  }

  public void SetParms(
    long projVId,
    string filtrOwnId,
    ICompositionsAutosortRule r,
    RelFilter relFilter,
    HybridDictionary dict,
    long projId = -1,
    HiddenCompositionFiltrationMode hcfm = HiddenCompositionFiltrationMode.None,
    int levelsOver = -1,
    int previewMode = 1)
  {
    this.ProjId = projId;
    this.ClientFiltrOwnerId = filtrOwnId;
    this.Rule = r;
    this.ProjVId = projVId;
    switch (hcfm)
    {
      case HiddenCompositionFiltrationMode.None:
        this.ClientShowHiddenObjs = true;
        this.ClientShowHiddenSostav = true;
        break;
      case HiddenCompositionFiltrationMode.HideChilds:
        this.ClientShowHiddenObjs = true;
        this.ClientShowHiddenSostav = false;
        break;
      case HiddenCompositionFiltrationMode.HideAll:
        this.ClientShowHiddenObjs = false;
        this.ClientShowHiddenSostav = false;
        break;
    }
    this.RelFilter = relFilter;
    this.Dict = dict;
    this.levelsOverride = levelsOver;
    this.PreviewMode = previewMode;
  }

  private IUserSession GetUserSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession userSession:
        return userSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string g:
        return UserSession.GetSessionByID(new Guid(g));
      default:
        return (IUserSession) null;
    }
  }

  private List<ColumnDescriptor> GenerateColumnDescriptors(List<int> objAttrs, List<int> relAttrs)
  {
    List<ColumnDescriptor> res = new List<ColumnDescriptor>();
    HashSet<Guid> objAttrGuids = new HashSet<Guid>();
    HashSet<Guid> relAttrGuids = new HashSet<Guid>();
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00029-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00033-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 1));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00034-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 2));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00035-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 3));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00036-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 4));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, res.Count + 1));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad0002a-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, res.Count + 1));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00130-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, res.Count + 1));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00047-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, res.Count + 1));
    AddDescriptor(new ColumnDescriptor((object) new Guid(VisTask._relTypeCAD), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    res.Add(new ColumnDescriptor((object) -77, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    AddDescriptor(new ColumnDescriptor((object) new Guid("cad00030-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    foreach (int objAttr in objAttrs)
      AddDescriptor(GlobalNode.CreateColDescr(MetaDataHelper.GetAttributeTypeGuid(objAttr).ToString(), false, 0));
    foreach (int relAttr in relAttrs)
      AddDescriptor(GlobalNode.CreateColDescr(MetaDataHelper.GetAttributeTypeGuid(relAttr).ToString(), true, 0));
    return res;

    void AddDescriptor(ColumnDescriptor cd)
    {
      if (cd.AttributeID.GetType() != typeof (Guid))
        return;
      Guid attributeId = (Guid) cd.AttributeID;
      if (cd.AttributeSource == AttributeSourceTypes.Object && objAttrGuids.Contains(attributeId) || cd.AttributeSource == AttributeSourceTypes.Relation && relAttrGuids.Contains(attributeId))
        return;
      res.Add(cd);
      if (cd.AttributeSource == AttributeSourceTypes.Object)
        objAttrGuids.Add(attributeId);
      if (cd.AttributeSource != AttributeSourceTypes.Relation)
        return;
      relAttrGuids.Add(attributeId);
    }
  }

  private List<ConditionStructure> GetConditionStructures(long objId, long excerptId)
  {
    bool flag1 = !this.Dict.Contains((object) this.StructLinksKey) || this.Dict[(object) this.StructLinksKey].Equals((object) true);
    bool flag2 = !this.Dict.Contains((object) this.AssocLinksKey) || this.Dict[(object) this.AssocLinksKey].Equals((object) true);
    List<ConditionStructure> conditionStructures;
    if (excerptId != 0L)
    {
      conditionStructures = new List<ConditionStructure>((IEnumerable<ConditionStructure>) VisServer.GetSelectionsService().GetConditionStructures((object) this.Session.SessionGUID, excerptId, objId));
      if (flag1 & flag2)
        return conditionStructures;
      if (this._idRelTypeCad == 0)
        this._idRelTypeCad = MetaDataHelper.GetAttributeTypeID(new Guid(VisTask._relTypeCAD));
      if (flag1)
      {
        conditionStructures.Insert(0, new ConditionStructure(this._idRelTypeCad, RelationalOperators.NotExistsOrEmpty, (object) 0, (object) 0, LogicalOperators.OR, 1, false, AttributeSourceTypes.Relation));
        conditionStructures.Insert(1, new ConditionStructure(this._idRelTypeCad, RelationalOperators.Equal, (object) 0, (object) 0, LogicalOperators.AND, -1, false, AttributeSourceTypes.Relation));
      }
      else
      {
        conditionStructures.Insert(0, new ConditionStructure(this._idRelTypeCad, RelationalOperators.NotExistsOrEmpty, (object) 0, (object) 0, LogicalOperators.OR, 1, false, AttributeSourceTypes.Relation));
        conditionStructures.Insert(1, new ConditionStructure(this._idRelTypeCad, RelationalOperators.Equal, (object) 1, (object) 0, LogicalOperators.AND, -1, false, AttributeSourceTypes.Relation));
      }
    }
    else
    {
      if (flag1 & flag2)
        return (List<ConditionStructure>) null;
      conditionStructures = new List<ConditionStructure>()
      {
        new ConditionStructure(this._idRelTypeCad, RelationalOperators.NotExistsOrEmpty, (object) 0, (object) 0, LogicalOperators.OR, 0, false, AttributeSourceTypes.Relation),
        new ConditionStructure(this._idRelTypeCad, RelationalOperators.Equal, (object) (flag2 ? 1 : 0), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation)
      };
    }
    return conditionStructures;
  }

  private void ProcessUseZamens(UseZamens useZamens)
  {
    switch (useZamens)
    {
      case UseZamens.MainVariant:
        if (!this.Dict.Contains((object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"))
        {
          this.Dict.Add((object) "{82E381A1-8952-416A-B303-F81BA2945F8F}", (object) true);
          break;
        }
        this.Dict[(object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"] = (object) true;
        break;
      case UseZamens.AllVariants:
        if (!this.Dict.Contains((object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"))
          break;
        this.Dict.Remove((object) "{82E381A1-8952-416A-B303-F81BA2945F8F}");
        break;
    }
  }

  private void ProcessHiddenMode(HiddenContentsMode hcm)
  {
    HiddenCompositionFiltrationMode compositionFiltrationMode = HiddenCompositionFiltrationMode.None;
    if (hcm == HiddenContentsMode.HiddenAsClient)
      hcm = !this.ClientShowHiddenObjs || !this.ClientShowHiddenSostav ? (!this.ClientShowHiddenObjs ? HiddenContentsMode.HideHiddenAndRoots : HiddenContentsMode.HideOnlyHidden) : HiddenContentsMode.ShowAllHidden;
    switch (hcm)
    {
      case HiddenContentsMode.ShowAllHidden:
        if (this.Dict.Contains((object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"))
        {
          this.Dict[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) true;
          break;
        }
        this.Dict.Add((object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}", (object) true);
        break;
      case HiddenContentsMode.HideOnlyHidden:
        compositionFiltrationMode = HiddenCompositionFiltrationMode.HideChilds;
        break;
      case HiddenContentsMode.HideHiddenAndRoots:
        compositionFiltrationMode = HiddenCompositionFiltrationMode.HideAll;
        break;
    }
    if (this.Dict.Contains((object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"))
      this.Dict[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"] = (object) compositionFiltrationMode;
    else
      this.Dict.Add((object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}", (object) compositionFiltrationMode);
    if (hcm == HiddenContentsMode.ShowAllHidden)
      return;
    if (this.Dict.Contains((object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"))
      this.Dict[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) false;
    else
      this.Dict.Add((object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}", (object) false);
  }

  private void _DoLoadData(object sender, DoWorkEventArgs e)
  {
    BackgroundWorker worker = sender as BackgroundWorker;
    QuickObjectInfo objectInfo = this.Session.GetObjectInfo(this.ProjVId);
    if (objectInfo.Empty)
      return;
    VisSchemeParms scheme;
    if (!ServerPDMPlugin.VisCache.TryGetValue(this.VisSchemeId, out scheme))
    {
      try
      {
        scheme = new VisSchemeParms(this.VisSchemeId, this.Session);
      }
      catch
      {
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Pdm.Server_54"), (object) this.VisSchemeId));
      }
      ServerPDMPlugin.VisCache.SaveValue(this.VisSchemeId, scheme);
    }
    if (this.NeedCancel(worker, e))
      return;
    int loadLevels = scheme.maxLevels;
    if (this.levelsOverride > 0)
      loadLevels = this.levelsOverride;
    if (loadLevels == 0)
      loadLevels = -1;
    UseZamens useZamens = scheme.useZamens;
    HiddenContentsMode hiddenMode = scheme.hiddenMode;
    long selectionId = scheme.SelectionId;
    Guid versionRule = scheme.VersionRule;
    List<int> intList = scheme.PreviewTypes.ConvertAll<int>((Converter<GlobalType, int>) (gt => gt.TypeID));
    List<int> searchRelationTypes = scheme.RelationTypes.Count > 0 ? scheme.RelationTypes.ConvertAll<int>((Converter<GlobalType, int>) (gt => gt.TypeID)) : (List<int>) null;
    List<int> objAttrs = scheme.ObjectAttrs.ConvertAll<int>((Converter<GlobalType, int>) (gt => gt.TypeID));
    List<int> relAttrs = scheme.RelationAttrs.ConvertAll<int>((Converter<GlobalType, int>) (gt => gt.TypeID));
    List<int> searchObjectTypes = (List<int>) null;
    if (scheme.ObjectTypes.Count > 0)
    {
      HashSet<int> collection = new HashSet<int>();
      foreach (GlobalType objectType in scheme.ObjectTypes)
        collection.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectType.TypeID));
      searchObjectTypes = new List<int>((IEnumerable<int>) collection);
    }
    if (this.NeedCancel(worker, e))
      return;
    List<int> expandObjectTypes = (List<int>) null;
    if (scheme.TypesToExpand.Count > 0 || scheme.TypesToDisableExpand.Count > 0)
    {
      HashSet<int> expTypes = new HashSet<int>();
      if (scheme.TypesToExpand.Count > 0)
      {
        foreach (GlobalType globalType in scheme.TypesToExpand)
          expTypes.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(globalType.TypeID));
      }
      else
        MetaDataHelper.GetTopObjectTypesIDs().ForEach((Action<int>) (objTypeId => expTypes.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeId))));
      foreach (GlobalType globalType in scheme.TypesToDisableExpand)
        expTypes.ExceptWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(globalType.TypeID));
      expandObjectTypes = new List<int>((IEnumerable<int>) expTypes);
    }
    if (searchRelationTypes == null)
      searchRelationTypes = VisServer.AllRelationList;
    if (this.NeedCancel(worker, e))
      return;
    List<ColumnDescriptor> columnDescriptors = this.GenerateColumnDescriptors(objAttrs, relAttrs);
    List<ConditionStructure> conditionStructures = this.GetConditionStructures(this.ProjVId, selectionId);
    if (this.NeedCancel(worker, e))
      return;
    this.ProcessUseZamens(useZamens);
    this.ProcessHiddenMode(hiddenMode);
    Dictionary<long, HybridDictionary> dbParams = new Dictionary<long, HybridDictionary>();
    dbParams.Add(this.ProjVId, this.Dict);
    string filtrationOwnerId = versionRule.Equals(Guid.Empty) ? this.ClientFiltrOwnerId : versionRule.ToString();
    if (this.NeedCancel(worker, e))
      return;
    lock (this)
      this._task = new CompositionTaskBooster(this.Session, VisServer.GetCompLoadService());
    DataTable table = this._task.Execute(new CompositionLoadingParams((IEnumerable<ObjInfoItem>) new List<ObjInfoItem>()
    {
      new ObjInfoItem(this.ProjVId, objectInfo.ObjectTypeID)
    }, (IEnumerable<int>) searchObjectTypes, (IEnumerable<int>) expandObjectTypes, (IEnumerable<int>) searchRelationTypes, (IEnumerable<ColumnDescriptor>) columnDescriptors, (IEnumerable<ConditionStructure>) conditionStructures, this.SeekChilds, false, loadLevels, (VersionsRule) null, filtrationOwnerId, (IDictionary<long, HybridDictionary>) dbParams));
    lock (this)
      this._task = (CompositionTaskBooster) null;
    if (this.NeedCancel(worker, e))
      return;
    this.ResTable = table != null ? new HybridTableExp(table, makeIndex: true) : (HybridTableExp) null;
    if (this.ResTable == null)
      return;
    int count1 = this.ResTable.Columns.Count;
    this.ResTable.Columns.Add(new HybridColumnsExp.HybridColumnExp(SystemGUIDs.attributePreview.ToString(), typeof (byte[])));
    int count2 = this.ResTable.Columns.Count;
    this.ResTable.Columns.Add(new HybridColumnsExp.HybridColumnExp(this.attributeFlag, typeof (bool)));
    this.ResTable.Rows.ForEach((Action<HybridRowExp>) (row => row.AddNullsForNewColumns()));
    HashSet<int> allPreviewTypes = new HashSet<int>();
    intList.ForEach((Action<int>) (objType => allPreviewTypes.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objType))));
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributePreview);
    int indexByName1 = this.ResTable.Columns.GetIndexByName("cad00029-306c-11d8-b4e9-00304f19f545");
    int indexByName2 = this.ResTable.Columns.GetIndexByName("cad0002e-306c-11d8-b4e9-00304f19f545");
    foreach (HybridRowExp row in this.ResTable.Rows)
    {
      int int32 = Convert.ToInt32(row[indexByName2]);
      row[count2] = (object) allPreviewTypes.Contains(int32);
      if ((this.PreviewMode != 1 || allPreviewTypes.Contains(int32)) && this.PreviewMode != 0)
      {
        BlobReaderStream blobReaderStream = new BlobReaderStream(Convert.ToInt64(row[indexByName1]), AttributableElements.Object, attributeTypeId, 0, 0, this.Session);
        BlobInformation blobInformation = blobReaderStream.BlobInformation;
        if (blobReaderStream.CanRead)
        {
          int realFileSize = (int) blobInformation.RealFileSize;
          byte[] buffer = new byte[realFileSize];
          blobReaderStream.Read(buffer, 0, realFileSize);
          row[count1] = (object) buffer;
        }
        else
          row[count1] = (object) this.noPreview;
        if (this.NeedCancel(worker, e))
          break;
      }
    }
  }

  private void _DoLoadPreviews(object sender, DoWorkEventArgs e)
  {
    BackgroundWorker worker = sender as BackgroundWorker;
    List<int> intList = (List<int>) null;
    if (this.VisSchemeId != 0L)
    {
      if (!ServerPDMPlugin.VisCache.TryGetValue(this.VisSchemeId, out VisSchemeParms _))
      {
        VisSchemeParms scheme;
        try
        {
          scheme = new VisSchemeParms(this.VisSchemeId, this.Session);
        }
        catch
        {
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Pdm.Server_54"), (object) this.VisSchemeId));
        }
        ServerPDMPlugin.VisCache.SaveValue(this.VisSchemeId, scheme);
        intList = scheme.PreviewTypes.ConvertAll<int>((Converter<GlobalType, int>) (gt => gt.TypeID));
      }
    }
    if (this.NeedCancel(worker, e))
      return;
    this.ResTable = new HybridTableExp();
    int count1 = this.ResTable.Columns.Count;
    this.ResTable.Columns.Add(new HybridColumnsExp.HybridColumnExp("cad00029-306c-11d8-b4e9-00304f19f545", typeof (long)));
    int count2 = this.ResTable.Columns.Count;
    this.ResTable.Columns.Add(new HybridColumnsExp.HybridColumnExp(SystemGUIDs.attributePreview.ToString(), typeof (byte[])));
    int count3 = this.ResTable.Columns.Count;
    this.ResTable.Columns.Add(new HybridColumnsExp.HybridColumnExp(this.attributeFlag, typeof (bool)));
    this.ResTable.Rows.ForEach((Action<HybridRowExp>) (row => row.AddNullsForNewColumns()));
    HashSet<int> allPreviewTypes = new HashSet<int>();
    if (this.PreviewMode == 1 && intList != null)
      intList.ForEach((Action<int>) (objType => allPreviewTypes.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objType))));
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributePreview);
    foreach (long objId in this.ObjIds)
    {
      int objectTypeId = this.Session.GetObjectInfo(objId).ObjectTypeID;
      HybridRowExp hrow = this.ResTable.NewRow();
      hrow[count1] = (object) objId;
      hrow[count3] = (object) allPreviewTypes.Contains(objectTypeId);
      if (this.PreviewMode != 1 || allPreviewTypes.Contains(objectTypeId))
      {
        BlobReaderStream blobReaderStream = new BlobReaderStream(objId, AttributableElements.Object, attributeTypeId, 0, 0, this.Session);
        BlobInformation blobInformation = blobReaderStream.BlobInformation;
        if (blobReaderStream.CanRead)
        {
          int realFileSize = (int) blobInformation.RealFileSize;
          byte[] buffer = new byte[realFileSize];
          blobReaderStream.Read(buffer, 0, realFileSize);
          hrow[count2] = (object) buffer;
        }
        else
          hrow[count2] = (object) this.noPreview;
        this.ResTable.Add(hrow);
        if (this.NeedCancel(worker, e))
          break;
      }
    }
  }

  private bool NeedCancel(BackgroundWorker worker, DoWorkEventArgs e)
  {
    if (!worker.CancellationPending)
      return false;
    e.Cancel = true;
    return true;
  }

  private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    if (this._clonedSession != null)
    {
      this._clonedSession.DBObjectsCacheStop();
      if (this._clonedSession.IsStartedLogHistory && this._session is UserSession session)
      {
        this._clonedSession.StopLogHistory();
        foreach (CategoryValue modificationsHistory in this._clonedSession.GetModificationsHistoryList())
          session.AddToModificationsHistory(modificationsHistory);
      }
      this._clonedSession.Logout(nameof (VisTask) + (object) this.TaskId);
    }
    if (e.Cancelled)
    {
      this.State = RelVisState.Unknown;
      this.ResTable = (HybridTableExp) null;
    }
    else if (e.Error != null)
    {
      this.ResTable = (HybridTableExp) null;
      this.Error = e.Error;
      this.State = RelVisState.Error;
    }
    else
      this.State = RelVisState.Ready;
  }

  public void StartLoadData()
  {
    this._clonedSession = this._session is IServerSession session ? (IUserSession) (session.Clone(true, nameof (VisTask) + (object) this.TaskId) as IServerSession) : throw new Exception(this._wrongSession);
    if (this._clonedSession == null)
      throw new Exception(this._wrongSession);
    this._clonedSession.DBObjectsCacheStart();
    if (session.IsStartedLogHistory)
      this._clonedSession.StartLogHistory();
    this.State = RelVisState.Working;
    this._bw.RunWorkerAsync((object) this._bw);
  }

  public void KillTask()
  {
    lock (this)
    {
      if (this._task != null)
        this._task.Terminated = true;
      this._task = (CompositionTaskBooster) null;
      if (!this._bw.IsBusy)
        return;
      this._bw.CancelAsync();
    }
  }
}
