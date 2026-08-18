// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertServer
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using ICSharpCode.SharpZipLib.Checksums;
using Intermech.Checksums;
using Intermech.Diagnostics;
using Intermech.Document.DBCore;
using Intermech.Expert.Table;
using Intermech.Imbase;
using Intermech.Imbase.Server;
using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.IO;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Signs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Generate.Interfaces;
using Intermech.TechCard.Document.Interfaces.Validate;
using Intermech.TechCard.Document.Interfaces.Validate.IsEmptyDocument;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertServer : LongLifeObject, IExpertServer, IExpertServerEx, IDisposable
{
  internal const int cnt_Data_Compression_Level = 3;
  public static readonly ExpertServer es;
  private System.Timers.Timer cleanCachesTimer;
  public ConcurrentDictionary<AttribPair, PairName> attNames;
  public ConcurrentDictionary<Guid, long> idents;
  public ConcurrentDictionary<AttribPair, ExpertServer.CacheObject<ScriptTreeNode>> attrRules;
  public ConcurrentDictionary<AttribPair, ExpertServer.CacheObject<ScriptTreeNode>> objRules;
  public ConcurrentDictionary<AttribPair, ExpertServer.CacheObject<ScriptTreeNode>> recalcScripts;
  public ConcurrentDictionary<long, ExpertServer.CacheObject<eTableCollection>> expertTables;
  public ConcurrentDictionary<long, ExpertServer.CacheObject<TempFormula>> expertConds;
  public ConcurrentDictionary<long, ExpertServer.CacheObject<ExpertServer.ExpertFormulaInfo>> expertFormulae;
  public ConcurrentDictionary<long, ExpertServer.CacheObject<ScriptTreeNode>> expertScripts;
  public ConcurrentDictionary<Guid, ExpertServer.CacheObject<QuickObjectInfo>> expertObjInfo;
  public ConcurrentDictionary<long, ExpertServer.CacheObject<ScriptTreeNode>> visScripts;
  public ConcurrentDictionary<int, HashSet<int>> objTypeChilds;
  internal Hashtable columns;
  internal ConcurrentDictionary<long, long> imbaseKeys;
  internal ConcurrentDictionary<long, string> imbaseFolderKeys;
  private ExpServerSynchronizer expSync;
  internal ConcurrentDictionary<string, ExpertServer.AttrInfo> attrAliases;
  internal ConcurrentDictionary<ExpertServer.Attr4_OTKey, ExpertServer.Attr4_OTInfo> attr4OT;
  internal ConcurrentDictionary<int, DataType> attrDataTypes;
  private ExpertServer.ExpServTask servTask;
  internal IServiceProvider _serviceProvider;
  internal IFileNamesService _ifns;
  internal IImbaseServer iis;
  internal IImbaseExtendedService iies;
  internal bool compTrace;
  public string logFileName = "";
  public bool needListNumsOnLinks;
  internal int[] objTypesTPDocComplect;
  private IEventLogHelper iLH;
  public static readonly string ExpertNamespace = "http://www.intermech.ru/Expert-System";
  public static readonly string buttonSubstitutesGuid = "{82E381A1-8952-416A-B303-F81BA2945F8F}";
  private const int STEP = 1000;
  public static readonly string attrIspCode = "cad001fa-306c-11d8-b4e9-00304f19f545";
  private const int _fakeObjectType = 56797 /*0xDDDD*/;
  internal static readonly ExpertServer.CalcStages AllStages = ExpertServer.CalcStages.CheckObject | ExpertServer.CalcStages.FindObject | ExpertServer.CalcStages.CalcAttribute;
  internal Hashtable funcIds;
  internal Hashtable funcDatas;
  internal Hashtable funcHandlers;
  internal Hashtable procHandlers;
  internal Hashtable comparers;
  internal static string emptyMeasure = LocalizationHolder.rm.GetString("Expert.Server_85");
  public static Adler32 checksummer = new Adler32();
  private bool disposed;
  internal int taskIdGenerator;
  internal ConcurrentDictionary<int, ExpertServer.ExpServTask> taskList = new ConcurrentDictionary<int, ExpertServer.ExpServTask>();
  internal HashSet<int> abortedTasks = new HashSet<int>();
  internal ReaderWriterLockSlim abortedLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

  public void DeleteObsoleteObjectsVoid<KeyType, ValType>(
    ConcurrentDictionary<KeyType, ExpertServer.CacheObject<ValType>> dict)
  {
    KeyType[] array = dict.Keys.ToArray<KeyType>();
    ExpertServer.CacheObject<ValType> cacheObject = (ExpertServer.CacheObject<ValType>) null;
    foreach (KeyType key in array)
    {
      if (dict[key].IsObsolete())
        dict.TryRemove(key, out cacheObject);
    }
  }

  public List<KeyType> DeleteObsoleteObjects<KeyType, ValType>(
    ConcurrentDictionary<KeyType, ExpertServer.CacheObject<ValType>> dict)
  {
    KeyType[] array = dict.Keys.ToArray<KeyType>();
    ExpertServer.CacheObject<ValType> cacheObject = (ExpertServer.CacheObject<ValType>) null;
    List<KeyType> keyTypeList = new List<KeyType>();
    foreach (KeyType key in array)
    {
      if (dict[key].IsObsolete() && dict.TryRemove(key, out cacheObject))
        keyTypeList.Add(key);
    }
    return keyTypeList;
  }

  public ValType GetValueFromCache<KeyType, ValType>(
    KeyType key,
    ConcurrentDictionary<KeyType, ExpertServer.CacheObject<ValType>> dict)
    where ValType : class
  {
    if (!dict.ContainsKey(key))
      return default (ValType);
    ExpertServer.CacheObject<ValType> cacheObject = dict[key];
    cacheObject.LastUsed = DateTime.Now;
    return cacheObject.Value;
  }

  public void SetValueToCache<KeyType, ValType>(
    KeyType key,
    ValType val,
    ConcurrentDictionary<KeyType, ExpertServer.CacheObject<ValType>> dict)
  {
    this.AddOrUpdate<KeyType, ValType>(dict, key, val);
  }

  public void DelValueFromCache<KeyType, ValType>(
    KeyType key,
    ConcurrentDictionary<KeyType, ExpertServer.CacheObject<ValType>> oldIdents)
  {
    if (!oldIdents.ContainsKey(key))
      return;
    oldIdents.TryRemove(key, out ExpertServer.CacheObject<ValType> _);
  }

  public void CleanCachesByTime(object source, ElapsedEventArgs e)
  {
    this.DeleteObsoleteObjectsVoid<AttribPair, ScriptTreeNode>(this.attrRules);
    this.DeleteObsoleteObjectsVoid<AttribPair, ScriptTreeNode>(this.objRules);
    this.DeleteObsoleteObjectsVoid<AttribPair, ScriptTreeNode>(this.recalcScripts);
    HashSet<long> longSet = new HashSet<long>();
    longSet.UnionWith((IEnumerable<long>) this.DeleteObsoleteObjects<long, eTableCollection>(this.expertTables));
    longSet.UnionWith((IEnumerable<long>) this.DeleteObsoleteObjects<long, TempFormula>(this.expertConds));
    longSet.UnionWith((IEnumerable<long>) this.DeleteObsoleteObjects<long, ExpertServer.ExpertFormulaInfo>(this.expertFormulae));
    longSet.UnionWith((IEnumerable<long>) this.DeleteObsoleteObjects<long, ScriptTreeNode>(this.expertScripts));
    longSet.UnionWith((IEnumerable<long>) this.DeleteObsoleteObjects<long, ScriptTreeNode>(this.visScripts));
    List<Guid> guidList = new List<Guid>();
    foreach (KeyValuePair<Guid, ExpertServer.CacheObject<QuickObjectInfo>> keyValuePair in this.expertObjInfo)
    {
      if (longSet.Contains(keyValuePair.Value.Value.ObjectID))
        guidList.Add(keyValuePair.Key);
    }
    ExpertServer.CacheObject<QuickObjectInfo> cacheObject = (ExpertServer.CacheObject<QuickObjectInfo>) null;
    foreach (Guid key in guidList)
    {
      while (this.expertObjInfo.ContainsKey(key))
        this.expertObjInfo.TryRemove(key, out cacheObject);
    }
  }

  internal ExpertServer.Attr4_OTInfo _GetAT4OTInfo(int attrType, int objType)
  {
    ExpertServer.Attr4_OTKey key = new ExpertServer.Attr4_OTKey(attrType, objType);
    if (this.attr4OT.ContainsKey(key))
      return this.attr4OT[key];
    bool Descr = false;
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objType, attrType);
    if (attribute4ObjectType != null)
      Descr = attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.GetDescriptionEvent);
    ExpertServer.Attr4_OTInfo at4OtInfo = new ExpertServer.Attr4_OTInfo(Descr);
    this.attr4OT.TryAdd(key, at4OtInfo);
    return at4OtInfo;
  }

  public DataType GetAttrDataType(int attrType)
  {
    if (this.attrDataTypes.ContainsKey(attrType))
      return this.attrDataTypes[attrType];
    DataType attrDataType = DataType.Unknown;
    try
    {
      bool flag = false;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrType);
      if (attributeType != null)
      {
        FieldTypes fieldType = attributeType.FieldType;
        if (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
          flag = true;
        attrDataType = flag || fieldType != FieldTypes.ftSystem ? (!flag ? DataTypeConvertor.AttrType2DataType(fieldType) : DataType.Packet) : DataTypeConvertor.AttrType2DataType(ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attrType));
      }
    }
    catch (EInvalidAttrType ex)
    {
    }
    this.attrDataTypes.TryAdd(attrType, attrDataType);
    return attrDataType;
  }

  public DataType GetAttrDataType(Guid g)
  {
    return this.GetAttrDataType(MetaDataHelper.GetAttributeTypeID(g));
  }

  internal void AddOrUpdate<T1, T2>(
    ConcurrentDictionary<T1, ExpertServer.CacheObject<T2>> dict,
    T1 key,
    T2 value)
  {
    ExpertServer.CacheObject<T2> newVal = new ExpertServer.CacheObject<T2>(value);
    dict.AddOrUpdate(key, newVal, (Func<T1, ExpertServer.CacheObject<T2>, ExpertServer.CacheObject<T2>>) ((k, v) => newVal));
  }

  static ExpertServer()
  {
    ExpertServer.es = new ExpertServer();
    ExpertServer.es.servTask = new ExpertServer.ExpServTask(0, Guid.Empty, ExpertTraceFlags.None);
    ExpertServer.es.taskList.GetOrAdd(0, ExpertServer.es.servTask);
  }

  private ExpertServer()
  {
    this.attNames = new ConcurrentDictionary<AttribPair, PairName>();
    this.idents = new ConcurrentDictionary<Guid, long>();
    this.attrRules = new ConcurrentDictionary<AttribPair, ExpertServer.CacheObject<ScriptTreeNode>>();
    this.objRules = new ConcurrentDictionary<AttribPair, ExpertServer.CacheObject<ScriptTreeNode>>();
    this.recalcScripts = new ConcurrentDictionary<AttribPair, ExpertServer.CacheObject<ScriptTreeNode>>();
    this.expertTables = new ConcurrentDictionary<long, ExpertServer.CacheObject<eTableCollection>>();
    this.expertConds = new ConcurrentDictionary<long, ExpertServer.CacheObject<TempFormula>>();
    this.expertFormulae = new ConcurrentDictionary<long, ExpertServer.CacheObject<ExpertServer.ExpertFormulaInfo>>();
    this.expertScripts = new ConcurrentDictionary<long, ExpertServer.CacheObject<ScriptTreeNode>>();
    this.expertObjInfo = new ConcurrentDictionary<Guid, ExpertServer.CacheObject<QuickObjectInfo>>();
    this.visScripts = new ConcurrentDictionary<long, ExpertServer.CacheObject<ScriptTreeNode>>();
    this.objTypeChilds = new ConcurrentDictionary<int, HashSet<int>>();
    this.columns = new Hashtable(1000);
    this.attrAliases = new ConcurrentDictionary<string, ExpertServer.AttrInfo>();
    this.attr4OT = new ConcurrentDictionary<ExpertServer.Attr4_OTKey, ExpertServer.Attr4_OTInfo>();
    this.attrDataTypes = new ConcurrentDictionary<int, DataType>();
    this.imbaseKeys = new ConcurrentDictionary<long, long>();
    this.imbaseFolderKeys = new ConcurrentDictionary<long, string>();
  }

  public void InitDocComplectTypes()
  {
    if (this.objTypesTPDocComplect != null)
      return;
    this.objTypesTPDocComplect = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objTechComplectRoot).ToArray();
  }

  public void StartTimers()
  {
    this.cleanCachesTimer = new System.Timers.Timer(7200000.0);
    this.cleanCachesTimer.AutoReset = true;
    this.cleanCachesTimer.Elapsed += new ElapsedEventHandler(this.CleanCachesByTime);
    this.cleanCachesTimer.Start();
  }

  public void StopTimers() => this.cleanCachesTimer.Stop();

  private void InitOwnIDs()
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Intermech.Expert.Server");
    try
    {
      ExpertConsts.Init(sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone?.Logout("Intermech.Expert.Server");
    }
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertObject), (long) ExpertConsts.Consts.objObject);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertFormula), (long) ExpertConsts.Consts.objFormula);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertCond), (long) ExpertConsts.Consts.objCond);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertTable), (long) ExpertConsts.Consts.objTable);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertScript), (long) ExpertConsts.Consts.objScript);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertFunction), (long) ExpertConsts.Consts.objFunction);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertAttrRules), (long) ExpertConsts.Consts.objAttrRules);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.ExpertObjRules), (long) ExpertConsts.Consts.objObjRules);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.objVisScheme), (long) ExpertConsts.Consts.objVisScheme);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.objVisStyles), (long) ExpertConsts.Consts.objVisStyles);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.Excerpt), (long) ExpertConsts.Consts.objExcerpt);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.DocScript), (long) ExpertConsts.Consts.objDocScript);
    this.idents.GetOrAdd(new Guid("cad00134-306c-11d8-b4e9-00304f19f545"), (long) ExpertConsts.Consts.objTemplate);
    this.idents.GetOrAdd(new Guid(ExpertObjGUIDs.RecalcScript), (long) ExpertConsts.Consts.objRecalcScript);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrContextCount), (long) ExpertConsts.Consts.attrContextCount);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrCurContextId), (long) ExpertConsts.Consts.attrCurContextId);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrCurContextNum), (long) ExpertConsts.Consts.attrCurContextNum);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrCurFldId), (long) ExpertConsts.Consts.attrCurFldId);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrCurFldTemplate), (long) ExpertConsts.Consts.attrCurFldTemplate);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrGUIDs), (long) ExpertConsts.Consts.attrAttrGUIDs);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrObjRelTypeId), (long) ExpertConsts.Consts.attrObjRelType);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attrRoles), (long) ExpertConsts.Consts.attrAttrRoles);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.attTemplateLink), (long) ExpertConsts.Consts.attrTemplateLink);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.condObj), (long) ExpertConsts.Consts.attrCondObj);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.objData), (long) ExpertConsts.Consts.attrObjData);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.objectName), (long) ExpertConsts.Consts.attrObjectName);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.objLinkIDs), (long) ExpertConsts.Consts.attrObjLinkIDs);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.objTypeGUIDs), (long) ExpertConsts.Consts.attrObjTypeGUIDs);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.resAttrGUID), (long) ExpertConsts.Consts.attrResAttrGUID);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.resObjTypeGUID), (long) ExpertConsts.Consts.attrResObjTypeGUID);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.resType), (long) ExpertConsts.Consts.attrResType);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.tableCols), (long) ExpertConsts.Consts.attrTableCols);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.tableEntries), (long) ExpertConsts.Consts.attrTableEntries);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.tableLayers), (long) ExpertConsts.Consts.attrTableLayers);
    this.idents.GetOrAdd(new Guid(ExpertAttrGUIDs.tableRows), (long) ExpertConsts.Consts.attrTableRows);
  }

  internal long[] GetObjectsByType(IUserSession ius, int objTypeId)
  {
    DataTable dataTable = ius.GetObjectCollection(objTypeId).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    }));
    long[] instance = (long[]) Array.CreateInstance(typeof (long), dataTable.Rows.Count);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      instance[index] = Convert.ToInt64(dataTable.Rows[index][0]);
    return instance;
  }

  private IUserSession GetSystemSessionClone(string sessionName)
  {
    return ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone(sessionName);
  }

  internal ScriptTreeNode _LoadAttrRule(IUserSession ius, long id)
  {
    ScriptTreeNode val = (ScriptTreeNode) null;
    try
    {
      ExpertAttrRules expertAttrRules = (ExpertAttrRules) ius.GetObject(id);
      expertAttrRules.Load();
      AttribPair key = new AttribPair(expertAttrRules.resAttrID, expertAttrRules.resObjTypeID);
      if (!this.attrRules.ContainsKey(key))
      {
        try
        {
          expertAttrRules.UnpackXML();
        }
        catch (Exception ex)
        {
          throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_125")}", ex).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(id));
        }
        try
        {
          val = ExpertServer.LoadScriptTree(expertAttrRules.xDoc);
          this.SetValueToCache<AttribPair, ScriptTreeNode>(key, val, this.attrRules);
        }
        catch
        {
          this.SetValueToCache<AttribPair, ScriptTreeNode>(key, (ScriptTreeNode) null, this.attrRules);
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error loading attribute rule {Convert.ToString(id)} {ex.Message}");
    }
    return val;
  }

  internal void LoadAttrRules()
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.LoadAttrRules");
    try
    {
      long[] objectsByType = this.GetObjectsByType(sessionTemporaryClone, ExpertConsts.Consts.objAttrRules);
      this.attrRules.Clear();
      for (int index = 0; index < objectsByType.Length; ++index)
      {
        long id = objectsByType[index];
        this._LoadAttrRule(sessionTemporaryClone, id);
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.LoadAttrRules");
    }
  }

  internal ScriptTreeNode _LoadObjRule(IUserSession ius, long id)
  {
    ScriptTreeNode val = (ScriptTreeNode) null;
    try
    {
      ExpertObjRules expertObjRules = (ExpertObjRules) ius.GetObject(id);
      expertObjRules.Load();
      AttribPair key = new AttribPair(expertObjRules.resAttrID, expertObjRules.resObjTypeID);
      if (!this.objRules.ContainsKey(key))
      {
        try
        {
          expertObjRules.UnpackXML();
        }
        catch (Exception ex)
        {
          throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_125")}", ex).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(id));
        }
        try
        {
          val = ExpertServer.LoadScriptTree(expertObjRules.xDoc);
          this.SetValueToCache<AttribPair, ScriptTreeNode>(key, val, this.objRules);
        }
        catch
        {
          this.SetValueToCache<AttribPair, ScriptTreeNode>(key, (ScriptTreeNode) null, this.objRules);
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error loading object finding rule {Convert.ToString(id)} {ex.Message}");
    }
    return val;
  }

  internal void LoadObjRules()
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.LoadObjRules");
    try
    {
      long[] objectsByType = this.GetObjectsByType(sessionTemporaryClone, ExpertConsts.Consts.objObjRules);
      this.objRules.Clear();
      for (int index = 0; index < objectsByType.Length; ++index)
      {
        long id = objectsByType[index];
        this._LoadObjRule(sessionTemporaryClone, id);
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.LoadObjRules");
    }
  }

  internal ScriptTreeNode _LoadRecalcScript(IUserSession ius, long id)
  {
    ScriptTreeNode val = (ScriptTreeNode) null;
    RecalcScript recalcScript = (RecalcScript) ius.GetObject(id);
    try
    {
      recalcScript.Load();
      AttribPair key = new AttribPair(recalcScript.resAttrID, recalcScript.resObjTypeID);
      if (!this.recalcScripts.ContainsKey(key))
      {
        try
        {
          recalcScript.UnpackXML();
        }
        catch (Exception ex)
        {
          throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_125")}", ex).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(id));
        }
        val = ExpertServer.LoadScriptTree(recalcScript.xDoc);
        this.SetValueToCache<AttribPair, ScriptTreeNode>(key, val, this.recalcScripts);
      }
    }
    catch
    {
    }
    return val;
  }

  internal void LoadRecalcScripts()
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.LoadRecalcScripts");
    try
    {
      long[] objectsByType = this.GetObjectsByType(sessionTemporaryClone, ExpertConsts.Consts.objRecalcScript);
      this.recalcScripts.Clear();
      for (int index = 0; index < objectsByType.Length; ++index)
      {
        long id = objectsByType[index];
        this._LoadRecalcScript(sessionTemporaryClone, id);
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.LoadRecalcScripts");
    }
  }

  internal void LoadAliases()
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.LoadAliases");
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) sessionTemporaryClone.GetAttributeTypeCollection(-1, false).Select("F_ATTRIBUTE_ID ASC").Rows)
      {
        string str = Convert.ToString(row["F_ALIAS"]);
        if (!(str == "") && !this.attrAliases.ContainsKey(str))
        {
          int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
          string N = Convert.ToString(row["F_NAME"]);
          int int32_2 = Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
          string g = Convert.ToString(row["F_GUID"]);
          ExpertServer.AttrInfo attrInfo = new ExpertServer.AttrInfo(int32_1, N, str, int32_2, g);
          this.attrAliases.TryAdd(str, attrInfo);
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.LoadAliases");
    }
  }

  internal void Init()
  {
    this._ifns = (IFileNamesService) this._serviceProvider.GetService(typeof (IFileNamesService));
    this.InitOwnIDs();
    this.funcIds = new Hashtable();
    this.funcDatas = new Hashtable();
    this.funcHandlers = new Hashtable();
    this.procHandlers = new Hashtable();
    this.comparers = new Hashtable();
    this.iLH = this._serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this.logFileName = "expert.log";
    this.LoadAliases();
    IServerSynchronizersManager service = (IServerSynchronizersManager) ServerServices.GetService(typeof (IServerSynchronizersManager));
    if (service == null)
      return;
    this.expSync = new ExpServerSynchronizer(this);
    service.RegisterSynchronizer((IServerSynchronizer) this.expSync);
    ServerServices.AddService(typeof (IExpertServerSynchronizer), (object) this.expSync);
  }

  public HashSet<int> GetChildObjectTypes(int objTypeId)
  {
    if (this.objTypeChilds.ContainsKey(objTypeId))
      return this.objTypeChilds[objTypeId];
    HashSet<int> childObjectTypes = new HashSet<int>((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objTypeId));
    this.objTypeChilds.TryAdd(objTypeId, childObjectTypes);
    return childObjectTypes;
  }

  public void InnerSetParm(ExpertServer.ExpServTask ti, int attrTypeID, object Value)
  {
    lock (ti)
      ti.__SetValue(new CalcAttrPair(-1L, attrTypeID), Value);
  }

  public void InnerSetParm(ExpertServer.ExpServTask ti, CalcAttrPair cap, object Value)
  {
    this.InnerSetParm(ti, cap, Value, AttrState.Unknown);
  }

  public void InnerSetParm(
    ExpertServer.ExpServTask ti,
    CalcAttrPair cap,
    object Value,
    AttrState aState)
  {
    lock (ti)
      ti.__SetValue(cap, Value, aState);
  }

  public object InnerSetParm(
    ExpertServer.ExpServTask ti,
    CalcAttrPair cap,
    object Value,
    AttrState aState,
    int X,
    int Y)
  {
    lock (ti)
    {
      if (X >= 0)
        return ti.__SetValue(cap, Value, aState, X, Y);
      ti.__SetValue(cap, Value, aState);
      return Value;
    }
  }

  public object InnerGetParm(ExpertServer.ExpServTask ti, int attrTypeID)
  {
    lock (ti)
      return ti.__GetValue(-1L, -1, attrTypeID)?.Value;
  }

  private void _SetParmValue(int taskId, long objID, int attrTypeID, object Value, bool byUser)
  {
    this.InnerSetParm(this.GetTask(taskId), new CalcAttrPair(objID, attrTypeID), Value);
  }

  private object _GetParmValue(int taskId, long objID, int attrTypeID)
  {
    return this._GetParmValue(this.GetTask(taskId), objID, -1, attrTypeID);
  }

  internal object _GetParmValue(int taskId, long objId, int objTypeId, int attrTypeId)
  {
    return this._GetParmValue(this.GetTask(taskId), objId, objTypeId, attrTypeId);
  }

  internal object _GetParmValue(
    ExpertServer.ExpServTask ti,
    long objId,
    int objTypeId,
    int attrTypeId)
  {
    lock (ti)
    {
      CalculatedAttr calculatedAttr = ti.__GetValue(objId, objTypeId, attrTypeId);
      if (calculatedAttr != null)
        return calculatedAttr.Value;
    }
    return (object) null;
  }

  internal object _GetParmValue(ExpertServer.ExpServTask ti, CalcAttrPair ca_pair)
  {
    lock (ti)
    {
      CalculatedAttr calculatedAttr = ti.__GetValue(ca_pair);
      if (calculatedAttr != null)
        return calculatedAttr.Value;
    }
    return (object) null;
  }

  private void __ReportSetValue(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    bool byUser,
    CalcAttrPair ca_pair,
    object Value)
  {
    lock (ti)
    {
      if (!this.FlagIn(ExpertTraceFlags.ShowAttrChanges, ti.traceFlags))
        return;
      XmlNode node = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_206")) : (XmlNode) null;
      if (node == null)
        return;
      ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_207"), Convert.ToString(ca_pair.objID));
      if (ca_pair.objTypeID != -1)
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_208"), MetaDataHelper.GetObjectTypeName(ca_pair.objTypeID));
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(ca_pair.attrTypeID);
      ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_209"), attributeTypeName);
      ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_210"), byUser ? ius.UserName : LocalizationHolder.rm.GetString("Expert.Server_211"));
      ti.traceAddAttribute(node, "_OBJ_ID_", Convert.ToString(ca_pair.objID));
      ti.traceAddText(node, Convert.ToString(Value));
    }
  }

  public Dictionary<CalcAttrPair, CalculatedAttr> _GetCalcParms(int taskId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
      return new Dictionary<CalcAttrPair, CalculatedAttr>((IDictionary<CalcAttrPair, CalculatedAttr>) task.CalcAttrs);
  }

  public Dictionary<CalcAttrPair, CalculatedAttr> _GetModifiedParms(int taskId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
    {
      Dictionary<CalcAttrPair, CalculatedAttr> modifiedParms = new Dictionary<CalcAttrPair, CalculatedAttr>();
      foreach (CalcAttrPair key in task.CalcAttrs.Keys)
      {
        CalculatedAttr calculatedAttr = task.CalcAttrs[key];
        if (calculatedAttr.ca_pair.attrTypeID != -10000)
        {
          Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(key.attrTypeID);
          ExpertServer.TempAttrStru tempAttrStru = task.GetTempAttrStru(attributeTypeGuid);
          if (calculatedAttr.attState != AttrState.SetByUser && !tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithObject) && !tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
          {
            if (calculatedAttr.Value is ArrayHolder)
              calculatedAttr = new CalculatedAttr(calculatedAttr.ca_pair, (object) ((ArrayHolder) calculatedAttr.Value).ToArray(), calculatedAttr.attState);
            modifiedParms.Add(key, calculatedAttr);
          }
        }
      }
      return modifiedParms;
    }
  }

  public void ClearCalcParm(int taskId, CalcAttrPair key)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
      task.CalcAttrs.Remove(key);
  }

  public void _SetCalcParms(int taskId, Dictionary<CalcAttrPair, CalculatedAttr> parms)
  {
    foreach (object obj in (IEnumerable) parms.Values)
    {
      if (obj.GetType() != typeof (CalculatedAttr))
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_10"));
    }
    ICollection keys = (ICollection) parms.Keys;
    foreach (object obj in (IEnumerable) keys)
    {
      if (obj.GetType() != typeof (CalcAttrPair))
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_11"));
    }
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
    {
      this.StartJobForTask(taskId);
      try
      {
        task.InitTraceAndLog();
        if (task.makeLog)
          this.iLH.AddToTrace("-------------------SetCalcParms started", Intermech.Consts.traceAlways, this.logFileName);
        IUserSession taskSession = this.GetTaskSession(task);
        foreach (CalcAttrPair calcAttrPair1 in (IEnumerable) keys)
        {
          CalcAttrPair calcAttrPair2 = calcAttrPair1;
          object obj1 = parms[calcAttrPair2].Value;
          if (obj1 is Array)
            obj1 = (object) new ArrayHolder((Array) obj1);
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(calcAttrPair2.attrTypeID);
          if (task.GetTempAttrStru(attributeType.AttributeGuid).HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
            calcAttrPair2 = new CalcAttrPair(-1L, calcAttrPair2.attrTypeID);
          if (calcAttrPair2.objTypeID == -1 && calcAttrPair2.objID != -1L)
          {
            object obj2 = (object) -1;
            ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(calcAttrPair2.objID, -7);
            if (task.attrCache.TryGetValue(key, out obj2))
            {
              calcAttrPair2.objTypeID = Convert.ToInt32(obj2);
            }
            else
            {
              TypedInfoItem itemData = task.DataCache.GetItemData(calcAttrPair2.objID, taskSession);
              if (itemData != (TypedInfoItem) null)
                calcAttrPair2.objTypeID = itemData.ItemTypeID;
              task.attrCache.Add(key, (object) calcAttrPair2.objTypeID);
            }
          }
          if (!task.CalcAttrs.ContainsKey(calcAttrPair2))
            task.__SetValue(new CalculatedAttr(calcAttrPair2, obj1, AttrState.SetByUser));
          else
            task.__SetValue(calcAttrPair2, obj1, AttrState.SetByUser);
          this.__ReportSetValue(task, taskSession, true, calcAttrPair2, obj1);
          if (task.makeLog)
            this.iLH.AddToTrace($"Key={calcAttrPair2.ToString()}; Value={(obj1 != null ? obj1.ToString() : "null")}", Intermech.Consts.traceAlways, this.logFileName);
        }
      }
      finally
      {
        this.EndJobForTask(taskId);
        if (task.makeLog)
          this.iLH.AddToTrace("-------------------SetCalcParms ended", Intermech.Consts.traceAlways, this.logFileName);
      }
    }
  }

  internal void __ApplyParmValue(ExpertServer.ExpServTask ti, CalculatedAttr ca)
  {
    if (ca.Temporary || ca.ca_pair.objID == -1L)
      return;
    IUserSession ius = (IUserSession) null;
    lock (ti)
      ius = this.GetSession(ti);
    bool Relation = false;
    bool flag = false;
    IDBAttributable dbAttributable = ExpertServer.GetAttributable(ius, ca.ca_pair.objID, out Relation);
    if (dbAttributable == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_12") + Convert.ToString(ca.ca_pair.objID) + LocalizationHolder.rm.GetString("Expert.Server_13"));
    if (!Relation)
    {
      if (((IDBObject) dbAttributable).CheckoutBy == 0L)
      {
        dbAttributable = (IDBAttributable) ((IDBObject) dbAttributable).CheckOut();
        flag = true;
      }
      else if (((IDBObject) dbAttributable).CheckoutBy != ius.UserID)
        return;
    }
    try
    {
      if (ca.ca_pair.attrTypeID <= 0)
        return;
      IDBAttribute dbAttribute = dbAttributable.GetAttributeByID(ca.ca_pair.attrTypeID) ?? dbAttributable.Attributes.AddAttribute(ca.ca_pair.attrTypeID, false);
      if (dbAttribute == null)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_14") + Convert.ToString(ca.ca_pair.attrTypeID) + LocalizationHolder.rm.GetString("Expert.Server_15"));
      if (ca.Value.GetType() == typeof (PacketValue))
      {
        PacketValue packetValue = (PacketValue) ca.Value;
        for (int index = 0; index < packetValue.Count; ++index)
          dbAttribute.Values[index] = packetValue[index].Value;
      }
      else
        dbAttribute.Value = ca.Value;
      if (Relation)
        return;
      ((IDBObject) dbAttributable).SaveChanges();
    }
    finally
    {
      if (!Relation & flag)
        ((IDBObject) dbAttributable).CheckIn();
    }
  }

  internal void __ApplyParmValues(
    ExpertServer.ExpServTask ti,
    long objId,
    List<int> attrIDs,
    List<object> values)
  {
    if (objId == -1L)
      return;
    IUserSession ius = (IUserSession) null;
    lock (ti)
      ius = this.GetSession(ti);
    bool Relation = false;
    IDBAttributable dbAttributable = ExpertServer.GetAttributable(ius, objId, out Relation);
    if (dbAttributable == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_16") + Convert.ToString(objId) + LocalizationHolder.rm.GetString("Expert.Server_17"));
    bool flag = false;
    if (!Relation)
    {
      if (((IDBObject) dbAttributable).CheckoutBy == 0L)
      {
        dbAttributable = (IDBAttributable) ((IDBObject) dbAttributable).CheckOut();
        flag = true;
      }
      else if (((IDBObject) dbAttributable).CheckoutBy != ius.UserID)
        return;
    }
    try
    {
      for (int index = 0; index < attrIDs.Count; ++index)
      {
        int attrId = attrIDs[index];
        object obj = values[index];
        if (attrId > 0)
        {
          IDBAttribute dbAttribute = dbAttributable.GetAttributeByID(attrId) ?? dbAttributable.Attributes.AddAttribute(attrId, false);
          if (dbAttribute == null)
            throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_18") + Convert.ToString(attrId) + LocalizationHolder.rm.GetString("Expert.Server_19"));
          try
          {
            dbAttribute.Value = obj;
          }
          catch (OperationNotApplicableException ex)
          {
            throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_275"), (object) obj.ToString(), (object) dbAttribute.Name, (object) attrId));
          }
        }
      }
      if (Relation)
        return;
      ((IDBObject) dbAttributable).SaveChanges();
    }
    finally
    {
      if (!Relation & flag)
        ((IDBObject) dbAttributable).CheckIn();
    }
  }

  internal void _ApplyParmValue(int taskId, long objID, int attrTypeID)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
    {
      bool flag = this.IsJobRunning(taskId);
      try
      {
        if (!flag)
          this.StartJobForTask(taskId, false);
        CalculatedAttr ca = (CalculatedAttr) null;
        if (!task.CalcAttrs.TryGetValue(objID, -1, attrTypeID, out ca))
          throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_20"), (object) objID, (object) attrTypeID));
        this.__ApplyParmValue(task, ca);
      }
      finally
      {
        if (!flag)
          this.EndJobForTask(taskId);
      }
    }
  }

  internal void _DeleteParmValue(int taskId, long objID, int attrTypeID)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
      task.CalcAttrs.Remove(objID, -1, attrTypeID);
  }

  internal void _ApplyCalcParms(int taskId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    bool flag = task.thread != null;
    if (!flag)
      this.StartJobForTask(task);
    try
    {
      lock (task)
      {
        List<long> longList = new List<long>();
        List<List<int>> intListList = new List<List<int>>();
        List<List<object>> objectListList = new List<List<object>>();
        foreach (CalculatedAttr calculatedAttr in task.CalcAttrs.Values)
        {
          int index = longList.IndexOf(calculatedAttr.ca_pair.objID);
          if (index < 0)
          {
            longList.Add(calculatedAttr.ca_pair.objID);
            intListList.Add(new List<int>());
            objectListList.Add(new List<object>());
            index = longList.Count - 1;
          }
          List<int> intList = intListList[index];
          if (intList.IndexOf(calculatedAttr.ca_pair.attrTypeID) < 0)
          {
            intList.Add(calculatedAttr.ca_pair.attrTypeID);
            objectListList[index].Add(calculatedAttr.Value);
          }
        }
        for (int index = 0; index < longList.Count; ++index)
          this.__ApplyParmValues(task, longList[index], intListList[index], objectListList[index]);
      }
    }
    finally
    {
      if (!flag)
        this.EndJobForTask(task);
    }
  }

  internal void _ApplyCalcParms(int taskId, List<CalculatedAttr> list)
  {
    if ((list != null ? (!list.Any<CalculatedAttr>() ? 1 : 0) : 1) != 0)
      return;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
    {
      bool flag = this.IsJobRunning(taskId);
      try
      {
        if (!flag)
          this.StartJobForTask(taskId, false);
        for (int index = 0; index < list.Count; ++index)
          this.__ApplyParmValue(task, list[index]);
      }
      finally
      {
        if (!flag)
          this.EndJobForTask(taskId);
      }
    }
  }

  internal void _ClearCalcParms(int taskId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
      task.CalcAttrs.Clear();
  }

  internal bool IsAttrNeeded(int taskId, long objId, int objTypeId, int attrTypeId)
  {
    return this.IsAttrNeeded(this.GetTask(taskId), objId, objTypeId, attrTypeId);
  }

  internal bool IsAttrNeeded(
    ExpertServer.ExpServTask ti,
    long objId,
    int objTypeId,
    int attrTypeId)
  {
    return ti.NeededAttrs.ContainsAttr(objId, objTypeId, attrTypeId);
  }

  internal void AddNeededAttr(int taskId, long objId, int objTypeId, int attrTypeId)
  {
    this.GetTask(taskId).NeededAttrs.AddAttr(objId, objTypeId, attrTypeId, true);
  }

  internal string GetNeedParmList(ExpertServer.ExpServTask ti)
  {
    this.GetSession(ti);
    int num = 0;
    string needParmList = "";
    foreach (CalcAttrPair key in ti.NeededAttrs.Keys)
    {
      if (key.objTypeID != -1)
        needParmList = $"{needParmList}<{MetaDataHelper.GetObjectTypeName(key.objTypeID)}>.";
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(key.attrTypeID);
      needParmList = $"{needParmList}{attributeTypeName} [{Convert.ToString(key.objID)}]";
      if (num < ti.NeededAttrs.Count - 1)
        needParmList += ", ";
      ++num;
    }
    return needParmList;
  }

  internal void ReportNeededParms(ExpertServer.ExpServTask ti)
  {
    lock (ti)
    {
      if (!ti.makeTrace || ti.NeededAttrs.Count <= 0)
        return;
      XmlNode curNode = ti.curNode;
      XmlNode newCurNode = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_21"));
      if (newCurNode == null)
        return;
      ti.traceSetNode(newCurNode);
      try
      {
        foreach (CalcAttrPair key in ti.NeededAttrs.Keys)
        {
          XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_22"));
          if (node != null)
          {
            ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_207"), Convert.ToString(key.objID));
            ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_209"), Convert.ToString(key.attrTypeID));
            if (key.objTypeID != -1)
              ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_208"), Convert.ToString(key.objTypeID));
          }
        }
      }
      finally
      {
        ti.traceSetNode(curNode);
      }
    }
  }

  public ExpertResult _GenerateDocument(
    int taskId,
    long docScriptID,
    long[] context,
    out byte[] zippedDoc)
  {
    ExpertResult document = ExpertResult.WrongTaskId;
    int docType = -1;
    string docName = "";
    this.StartJobForTask(taskId);
    try
    {
      document = this._GenerateDocument(taskId, docScriptID, context, out docType, out docName);
    }
    finally
    {
      this.EndJobForTask(taskId);
    }
    zippedDoc = (byte[]) null;
    ExpertServer.ExpServTask taskEx = this.GetTaskEx(taskId);
    zippedDoc = this.PackDocumentData(taskEx.docData);
    return document;
  }

  private ExpertResult _GenerateDocument(
    int taskId,
    long docScriptID,
    long[] context,
    out int docType,
    out string docName)
  {
    bool flag = false;
    string Text = "";
    string EventStr1 = "";
    string str1 = "";
    string EventStr2 = "";
    ExpertResult document = ExpertResult.OK;
    docType = -1;
    docName = "";
    try
    {
      ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
      IUserSession ius = (IUserSession) null;
      DocScript docScript = (DocScript) null;
      ExpertTraceFlags traceFlags = ti.traceFlags;
      string str2 = "";
      try
      {
        ius = this.GetSession(ti);
        docScript = (DocScript) ius.GetObjectActualCopy(docScriptID, false);
        str2 = docScript.Caption;
        string asString = docScript.GetAttributeByID(ExpertConsts.Consts.attrGenDocType).AsString;
        if (asString != "")
          docType = MetaDataHelper.GetObjectTypeID(new Guid(asString));
        XmlNode xmlNode = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_224")) : (XmlNode) null;
        if (xmlNode != null)
        {
          ti.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_225"), str2);
          ti.traceSetNode(xmlNode);
        }
      }
      finally
      {
        this.EndModifyTrace(ti);
      }
      ti.docScriptId = docScriptID;
      ExpertServer.GenInfo genInfo;
      ScriptTreeNode root;
      if (ti.cacheScripts != null && ti.cacheScripts.ContainsKey(docScriptID))
      {
        Tuple<ExpertServer.GenInfo, ScriptTreeNode> cacheScript = ti.cacheScripts[docScriptID];
        genInfo = cacheScript.Item1;
        root = cacheScript.Item2;
      }
      else
      {
        if (this.FlagIn(ExpertTraceFlags.ShowContext, traceFlags))
          this.ShowContext(taskId, context, false);
        if (this.FlagIn(ExpertTraceFlags.ShowExpertObjects, traceFlags))
          this.ShowLoadObject(taskId, (ExpertObject) docScript);
        else
          docScript.Load();
        try
        {
          ExpertScriptParms parms;
          root = XMLScripter.LoadScript(docScript.Script, out parms);
          genInfo = new ExpertServer.GenInfo(parms);
        }
        catch (Exception ex)
        {
          throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_23")}", ex);
        }
        if (ti.makeLog)
          this.iLH.AddToTrace("Loading Script...", Intermech.Consts.traceAlways, this.logFileName);
        if (ti.cacheScripts != null)
          ti.cacheScripts.GetOrAdd(docScriptID, new Tuple<ExpertServer.GenInfo, ScriptTreeNode>(genInfo, root));
      }
      docName = genInfo.docName;
      if (!genInfo.Debug)
        ti.makeTrace = false;
      if (ti.makeTrace)
        ti.rootExclaimed = this.PlaceExclamations(root);
      if (ti.makeLog)
        this.iLH.AddToTrace("Finished. Loading Template...", Intermech.Consts.traceAlways, this.logFileName);
      long templateId = docScript.TemplateId;
      if (templateId == -1L)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_240"));
      if (ti.makeLog)
        this.iLH.AddToTrace("Starting LoadTemplateFromObject...", Intermech.Consts.traceAlways, this.logFileName);
      ti.template = this.LoadTemplateFromObject(ius, ti, templateId);
      if (ti.makeLog)
        this.iLH.AddToTrace("... LoadTemplateFromObject finished!", Intermech.Consts.traceAlways, this.logFileName);
      ti.useAllZamens = genInfo.AllZamens;
      lock (ti)
      {
        ti.curScrType = ExpertScriptType.DocScript;
        ti.curDocNode = (DocumentTreeNode) null;
        ti.scriptRoot = root;
      }
      this.iLH.TruncateTraceFile("doc-gen.log", 10000000);
      this.iLH.AddToTrace($"Document generation started... Task={Convert.ToString(taskId)} User={ius.UserName} Comp={ius.ComputerName} Script={str2} Time= {DateTime.Now.ToShortTimeString()}", Intermech.Consts.traceAlways, "doc-gen.log");
      this.DoGenerateDoc(taskId, ius, context, root, ti.template);
      document = ExpertResult.OK;
      return document;
    }
    catch (Exception ex)
    {
      flag = true;
      Text = ex.Message;
      EventStr1 = ex.StackTrace;
      if (ex.InnerException != null)
      {
        str1 = ex.InnerException.Message;
        EventStr2 = ex.InnerException.StackTrace;
      }
      if (ex.GetType() != typeof (EAbort))
      {
        this.LogException(taskId, ex);
        throw;
      }
      EnumTypeHelper.GetCaption((Enum) (ex as EAbort).res);
      switch ((ex as EAbort).res)
      {
        case ExpertResult.NoCondParms:
        case ExpertResult.NoCalcParms:
        case ExpertResult.RuleNotFound:
        case ExpertResult.CircularReference:
          ExpertServer.ExpServTask task = this.taskList[taskId];
          throw new ExpertServerException(ex.Message + LocalizationHolder.rm.GetString("Expert.Server_149") + this.GetNeedParmList(task), ex);
        default:
          return ExpertResult.Aborted;
      }
    }
    finally
    {
      if (!this.abortedTasksContains(taskId))
      {
        ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
        try
        {
          XmlNode curNode = ti.curNode;
          if (flag)
          {
            XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_59"));
            if (node != null)
              ti.traceAddText(node, Text);
            if (ti.makeLog)
            {
              this.iLH.AddToTrace($"Exception - \"{Text}\"", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace("----------  Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace(EventStr1, Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace($"InnerException - \"{str1}\"", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace("----------  Inner Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace(EventStr2, Intermech.Consts.traceAlways, this.logFileName);
            }
          }
          else
          {
            XmlNode node = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_55")) : (XmlNode) null;
            if (node != null)
              ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_58"), Convert.ToString((object) document));
          }
          ti.traceSetNode(curNode);
        }
        finally
        {
          this.EndModifyTrace(ti);
        }
      }
    }
  }

  private ExpertResult _GenerateDocument(
    int taskId,
    long docScriptID,
    long[] context,
    long docObjId)
  {
    bool flag = false;
    string Text = "";
    string EventStr1 = "";
    string str1 = "";
    string EventStr2 = "";
    ExpertResult document = ExpertResult.OK;
    this.StartJobForTask(taskId);
    try
    {
      ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
      IUserSession userSession = (IUserSession) null;
      DocScript docScript = (DocScript) null;
      ExpertTraceFlags traceFlags = ti.traceFlags;
      string str2 = "";
      try
      {
        userSession = this.GetSession(ti);
        docScript = (DocScript) userSession.GetObjectActualCopy(docScriptID, false);
        str2 = docScript.Caption;
        XmlNode xmlNode = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_224"));
        if (xmlNode != null)
        {
          ti.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_225"), str2);
          ti.traceSetNode(xmlNode);
        }
      }
      finally
      {
        this.EndModifyTrace(ti);
      }
      ti.docScriptId = docScriptID;
      ExpertServer.GenInfo genInfo;
      ScriptTreeNode root;
      if (ti.cacheScripts != null && ti.cacheScripts.ContainsKey(docScriptID))
      {
        Tuple<ExpertServer.GenInfo, ScriptTreeNode> cacheScript = ti.cacheScripts[docScriptID];
        genInfo = cacheScript.Item1;
        root = cacheScript.Item2;
      }
      else
      {
        if (this.FlagIn(ExpertTraceFlags.ShowContext, traceFlags))
          this.ShowContext(taskId, context, false);
        if (this.FlagIn(ExpertTraceFlags.ShowExpertObjects, traceFlags))
          this.ShowLoadObject(taskId, (ExpertObject) docScript);
        else
          docScript.Load();
        try
        {
          ExpertScriptParms parms;
          root = XMLScripter.LoadScript(docScript.Script, out parms);
          genInfo = new ExpertServer.GenInfo(parms);
        }
        catch (Exception ex)
        {
          throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_23")}", ex);
        }
        if (ti.makeLog)
          this.iLH.AddToTrace("Loading Script...", Intermech.Consts.traceAlways, this.logFileName);
        if (ti.cacheScripts != null)
          ti.cacheScripts.GetOrAdd(docScriptID, new Tuple<ExpertServer.GenInfo, ScriptTreeNode>(genInfo, root));
      }
      if (!genInfo.Debug)
        ti.makeTrace = false;
      long templateId = docScript.TemplateId;
      if (ti.makeTrace)
        ti.rootExclaimed = this.PlaceExclamations(root);
      if (ti.makeLog)
        this.iLH.AddToTrace("Finished. Loading Template...", Intermech.Consts.traceAlways, this.logFileName);
      if (ti.makeLog)
        this.iLH.AddToTrace("Starting LoadTemplateFromObject...", Intermech.Consts.traceAlways, this.logFileName);
      ti.template = this.LoadTemplateFromObject(userSession, ti, templateId);
      if (ti.makeLog)
        this.iLH.AddToTrace("... LoadTemplateFromObject finished!", Intermech.Consts.traceAlways, this.logFileName);
      ti.useAllZamens = genInfo.AllZamens;
      ti.coWorkerDocs = genInfo.CoWorker;
      lock (ti)
      {
        ti.curScrType = ExpertScriptType.DocScript;
        ti.curDocNode = (DocumentTreeNode) null;
        ti.scriptRoot = root;
      }
      this.iLH.TruncateTraceFile("doc-gen.log", 10000000);
      this.iLH.AddToTrace($"Document generation started... Task={Convert.ToString(taskId)} User={userSession.UserName} Comp={userSession.ComputerName} Script={str2} Time= {DateTime.Now.ToShortTimeString()}", Intermech.Consts.traceAlways, "doc-gen.log");
      ImDocumentData doc = this.DoGenerateDoc(taskId, userSession, context, root, ti.template);
      IFileNamesService service = ServiceUtils.GetService<IFileNamesService>((object) userSession, true);
      string fileName = "document.imdx";
      if (service != null)
        fileName = service.GetUniqueFileName(fileName, docObjId, userSession.SessionGUID);
      BlobInformation info = new BlobInformation(0L, 0L, DateTime.Now, fileName, ArcMethods.ZLibPacked, string.Empty);
      using (BlobWriterStream blobWriterStream = new BlobWriterStream(docObjId, AttributableElements.Object, MetaDataHelper.GetAttributeID((object) new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")), 0, 0, info, userSession))
      {
        doc.SaveToXml((Stream) blobWriterStream);
        blobWriterStream.Commit();
      }
      document = ExpertResult.OK;
      return document;
    }
    catch (Exception ex)
    {
      flag = true;
      Text = ex.Message;
      EventStr1 = ex.StackTrace;
      if (ex.InnerException != null)
      {
        str1 = ex.InnerException.Message;
        EventStr2 = ex.InnerException.StackTrace;
      }
      if (ex.GetType() != typeof (EAbort))
      {
        this.LogException(taskId, ex);
        throw;
      }
      EnumTypeHelper.GetCaption((Enum) (ex as EAbort).res);
      switch ((ex as EAbort).res)
      {
        case ExpertResult.NoCondParms:
        case ExpertResult.NoCalcParms:
        case ExpertResult.RuleNotFound:
        case ExpertResult.CircularReference:
          ExpertServer.ExpServTask task = this.taskList[taskId];
          throw new ExpertServerException(ex.Message + LocalizationHolder.rm.GetString("Expert.Server_149") + this.GetNeedParmList(task));
        default:
          return ExpertResult.Aborted;
      }
    }
    finally
    {
      if (!this.abortedTasksContains(taskId))
      {
        ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
        try
        {
          XmlNode curNode = ti.curNode;
          if (flag)
          {
            XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_59"));
            if (node != null)
              ti.traceAddText(node, Text);
            if (ti.makeLog)
            {
              this.iLH.AddToTrace($"Exception - \"{Text}\"", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace("----------  Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace(EventStr1, Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace($"InnerException - \"{str1}\"", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace("----------  Inner Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace(EventStr2, Intermech.Consts.traceAlways, this.logFileName);
            }
          }
          else
          {
            XmlNode element = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_99"), ExpertServer.ExpertNamespace);
            XmlAttribute attribute = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_58"));
            attribute.Value = Convert.ToString((object) document);
            element.Attributes.Append(attribute);
            ti.curNode.AppendChild(element);
          }
          ti.traceSetNode(curNode);
        }
        finally
        {
          this.EndModifyTrace(ti);
        }
        this.EndJobForTask(taskId);
      }
    }
  }

  private ImDocumentData DoGenerateDoc(
    int taskId,
    IUserSession ius,
    long[] context,
    ScriptTreeNode root,
    ImDocumentData template)
  {
    ImDocumentData doc = (ImDocumentData) null;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
    {
      bool flag = ius.Configurations.ReadBool("Expert_System", "User", "Show_Window", false, DBConfigMode.UserAndGlobal);
      task.makeTrace &= flag;
      if (task.makeLog)
        this.iLH.AddToTrace("Before loading template...", Intermech.Consts.traceAlways, this.logFileName);
      LogManager.CreateLog = true;
      task.docData = new ImDocumentData(template, true, true);
      task.template = task.docData.DocumentTemplate;
      task.docData.SuspendUpdateLayout();
      task.docData.SuspendUpdateGeometryRefreshUI();
      doc = task.docData;
      if (task.makeLog)
        this.iLH.AddToTrace("After loading template.", Intermech.Consts.traceAlways, this.logFileName);
      task.context.Clear();
      foreach (long num in context)
        task.context.Add(num);
    }
    HybridTableExp dTable = (HybridTableExp) null;
    this._SetParmValue(taskId, -1L, ExpertConsts.Consts.attrContextCount, (object) context.Length, false);
    if (task.CompGenMode == GenMode.genModeNone)
    {
      task.OptInitCollectObjectData();
      task.OptCollectScriptAttrTypes();
    }
    task.BreakFlag = false;
    long[] new_context = (long[]) null;
    try
    {
      for (int index = 0; index < root.Items.Count; ++index)
      {
        this.ProcessScriptNode(taskId, (ScriptTreeNode) root.Items[index], context, dTable, false, ref new_context);
        if (!task.BreakFlag)
        {
          if (new_context != null)
          {
            context = (long[]) new_context.Clone();
            task.context.Clear();
            foreach (long num in context)
              task.context.Add(num);
          }
        }
        else
          break;
      }
    }
    catch (Exception ex)
    {
      if (task.makeLog)
      {
        this.iLH.AddToTrace($"Exception - \"{ex.Message}\"", Intermech.Consts.traceAlways, this.logFileName);
        this.iLH.AddToTrace("----------  Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
        this.iLH.AddToTrace(ex.StackTrace, Intermech.Consts.traceAlways, this.logFileName);
        if (ex.InnerException != null)
        {
          this.iLH.AddToTrace($"InnerException - \"{ex.InnerException.Message}\"", Intermech.Consts.traceAlways, this.logFileName);
          this.iLH.AddToTrace("----------  Inner Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
          this.iLH.AddToTrace(ex.InnerException.StackTrace, Intermech.Consts.traceAlways, this.logFileName);
        }
      }
      throw;
    }
    if (task.makeLog)
      this.iLH.AddToTrace("Generation ended!", Intermech.Consts.traceAlways, this.logFileName);
    this.iLH.AddToTrace($"---      generation ended... Task={Convert.ToString(taskId)} User={ius.UserName} Comp={ius.ComputerName} Time= {DateTime.Now.ToShortTimeString()}", Intermech.Consts.traceAlways, "doc-gen.log");
    return doc;
  }

  private void LogException(int taskId, Exception e)
  {
    this.iLH.AddToTrace($"EXCEPTION! Task={Convert.ToString(taskId)} Time= {DateTime.Now.ToShortTimeString()}\nMessage= {e.Message}\nStack trace= {e.StackTrace}", Intermech.Consts.traceAlways, "doc-gen.log");
  }

  private ImDocumentData LoadTemplateFromObject(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    long objectId)
  {
    if (ti.cacheTemplates != null && ti.cacheTemplates.ContainsKey(objectId))
      return ti.cacheTemplates[objectId];
    ImDocumentData imDocumentData;
    try
    {
      imDocumentData = DocumentEditorPluginBase.LoadDocumentFromDBObject(ius, objectId, failIfNotFound: true);
    }
    catch (Exception ex)
    {
      throw new ExpertServerException($"Ошибка загрузки шаблона!{Environment.NewLine}{ex.Message}", ex).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(objectId));
    }
    if (ti.cacheTemplates != null && !ti.cacheTemplates.ContainsKey(objectId))
      ti.cacheTemplates.GetOrAdd(objectId, imDocumentData);
    return imDocumentData;
  }

  public byte[] PackDocumentData(ImDocumentData docData)
  {
    IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
    using (ImChunkedStream inStream = new ImChunkedStream())
    {
      docData.SaveToXml((Stream) inStream);
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        service.PackStream((Stream) outStream, (Stream) inStream, 9);
        return outStream.ToArray();
      }
    }
  }

  public byte[] PackXml(XmlDocument xDoc)
  {
    IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      xDoc.Save((Stream) imChunkedStream);
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        service.PackStream((Stream) outStream, (Stream) imChunkedStream, 9);
        return outStream.ToArray();
      }
    }
  }

  private void _SetDocAttributes(ExpertServer.ExpServTask ti, long docObjectId)
  {
    this.StartJobForTask(ti);
    try
    {
      IUserSession session = ti.GetSession();
      ((UserSession) session).DBObjectsCacheRemoveVersion(docObjectId);
      IDBObject idbO = session.GetObject(docObjectId, false);
      if (idbO == null || idbO.ObjectModifyMode == ObjectModifyModes.CantModify)
        return;
      if (idbO.ObjectModifyMode == ObjectModifyModes.Checkout)
        idbO = idbO.CheckOut();
      IDBObjectType objectType = session.GetObjectType(idbO.ObjectType, false);
      if (objectType == null)
        return;
      this._SetDocAttributes(ti, idbO, objectType);
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  private void _SetDocAttributes(ExpertServer.ExpServTask ti, IDBObject idbO, IDBObjectType idbOT)
  {
    foreach (int docAttr in ti.docAttrs)
    {
      object parm = this.InnerGetParm(ti, docAttr);
      if (parm != null)
      {
        if (idbOT.Attributes.GetAttributeByID(docAttr, false) != null)
        {
          try
          {
            IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(docAttr, false);
            if (dbAttribute != null)
              dbAttribute.Value = parm;
          }
          catch
          {
          }
        }
      }
    }
  }

  private void _SetDocAttributes(
    ExpertServer.SetDocumentInfo sdi,
    IDBObject idbO,
    IDBObjectType idbOT)
  {
    if (sdi.DocAttrs == null)
      return;
    foreach (int key in sdi.DocAttrs.Keys)
    {
      object docAttr = sdi.DocAttrs[key];
      if (docAttr != null && (idbOT.AnyAttributes || idbOT.Attributes.GetAttributeByID(key, false) != null))
      {
        try
        {
          IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(key, false);
          if (dbAttribute != null)
            dbAttribute.Value = docAttr;
        }
        catch
        {
        }
      }
    }
  }

  private ExpertResult _GenerateComplect(
    int taskId,
    long compScriptID,
    long contextID,
    long complectID,
    GenMode gm,
    out List<ChangeInfo> changed,
    bool dopComplects = false)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    changed = new List<ChangeInfo>();
    ti.changed = changed;
    string str1 = "";
    string str2 = "";
    try
    {
      ti.CompGenMode = gm;
      ti.OldComplectId = complectID;
      ti.cacheTemplates = new ConcurrentDictionary<long, ImDocumentData>();
      IUserSession session = this.GetSession(ti);
      IDBObject objectActualCopy = session.GetObjectActualCopy(compScriptID, false);
      ti.compScriptId = objectActualCopy == null ? compScriptID : objectActualCopy.ObjectID;
      ti.contextID = contextID;
      ti.idComplects = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objComplect).ToArray();
      ti.idDocs = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objDocRoot).ToArray();
      ExpertResult complect = ExpertResult.OK;
      ti.allowConcretization = ExpertServer.IsAttributeAllowedForRelType(ExpertConsts.Consts.linkSimpleSortId, ExpertConsts.Consts.attrVerSostav);
      if (this.compTrace)
      {
        str1 = session.UserName;
        str2 = session.ComputerName;
        if (ti.makeLog)
          this.iLH.AddToTrace($"Starting complect generation ({str1}) [{str2}]", Intermech.Consts.traceAlways, this.logFileName);
      }
      ComplectTemplate complectTemplate = (ComplectTemplate) session.GetObject(ti.compScriptId);
      complectTemplate.Load();
      try
      {
        complectTemplate.UnpackXML();
      }
      catch (Exception ex)
      {
        throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_23")}", ex);
      }
      ExpertServer.GenInfo si;
      ScriptTreeNode scriptTreeNode1 = this.LoadScriptTree(complectTemplate.xDoc, out si);
      ti.useAllZamens = si.AllZamens;
      ti.coWorkerDocs = si.CoWorker;
      ti.checkOutDocs = si.CheckOut;
      ti.OptInitCollectObjectData();
      ti.DoCollectComplectAttrTypes(scriptTreeNode1, session);
      int index1 = 0;
      ScriptTreeNode scriptTreeNode2 = (ScriptTreeNode) null;
      if (scriptTreeNode1.Items.Count > 0)
      {
        do
        {
          scriptTreeNode2 = (ScriptTreeNode) scriptTreeNode1.Items[index1];
          ++index1;
        }
        while (scriptTreeNode2.label.StartsWith("#") && index1 < scriptTreeNode1.Items.Count);
      }
      ScriptTreeNode node1;
      if (scriptTreeNode2 != null && (scriptTreeNode2.opTag == ExpertScriptOp.opObjDescendants || scriptTreeNode2.opTag == ExpertScriptOp.opUserProc || scriptTreeNode2.opTag == ExpertScriptOp.opGlobRoot))
      {
        node1 = scriptTreeNode2;
      }
      else
      {
        node1 = new ScriptTreeNode();
        node1.opTag = ExpertScriptOp.opObjDescendants;
        node1.op = (OpParm) new OpParmObject();
        OpParmObject op = (OpParmObject) node1.op;
        op.AddThis = true;
        op.dataAttrGUIDs = new List<string>();
        op.dataAttrGUIDs.Add("cad00020-306c-11d8-b4e9-00304f19f545");
        op.dataAttrGUIDs.Add("cad0001f-306c-11d8-b4e9-00304f19f545");
        op.dataAttrGUIDs.Add("cad009e6-306c-11d8-b4e9-00304f19f545");
        op.dataAttrChecks = new List<string>();
        op.dataAttrChecks.Add("N");
        op.dataAttrChecks.Add("N");
        op.dataAttrChecks.Add("Y");
        op.linkTypeIDs = new List<int>();
        op.linkTypeIDs.Add(ExpertConsts.Consts.linkTechSostId);
        op.saveGlobal = GlobalSave.saveSet;
        op.saveRels = true;
        op.NoSearch = false;
        index1 = 0;
      }
      long[] new_context = (long[]) null;
      this.ProcessScriptNode(taskId, node1, new long[1]
      {
        contextID
      }, (HybridTableExp) null, false, ref new_context);
      for (int index2 = index1; index2 < scriptTreeNode1.Items.Count; ++index2)
      {
        ScriptTreeNode node2 = (ScriptTreeNode) scriptTreeNode1.Items[index2];
        if (node2.opTag != ExpertScriptOp.opCreateComplect && node2.opTag != ExpertScriptOp.opCreateDocument)
          this.ProcessScriptNode(taskId, node2, new long[1]
          {
            contextID
          }, (HybridTableExp) null, false, ref new_context);
        else
          break;
      }
      ti.dopCompTags = new List<string>();
      ti.needSecondPass = false;
      this.GetDocList(ti, session, scriptTreeNode1, contextID, dopComplects);
      this.HackDocList(ti);
      if (complectID == -1L && gm == GenMode.genModeVersion)
        complectID = this.GetPrevKTDVersion(session, contextID, compScriptID);
      if (complectID != -1L)
      {
        this.GetOldComplectData(ti, session, contextID, complectID);
        if (gm == GenMode.genModeRefresh)
          this.MarkInOtherComplects(ti, session);
        for (int index3 = 0; index3 < ti.docList.Count; ++index3)
        {
          DocRecord doc = ti.docList[index3];
          ExpertServer.OldKey key = new ExpertServer.OldKey(Math.Abs(doc.objID), Math.Abs(doc.scriptID));
          if (ti.oldIdents.ContainsKey(key))
            doc.oldObjectID = ti.oldIdents[key].objId;
        }
      }
      this.FindPrevVersions(ti);
      if (ti.oldComplect != null && ti.oldComplect.Count > 0)
      {
        IDBObject dbObject = session.GetObject(ti.oldComplect[complectID].verId, false);
        if (dbObject != null)
        {
          ti.attrChangeGroupId = MetaDataHelper.GetAttributeTypeID(new Guid("cad014d2-306c-11d8-b4e9-00304f19f545"));
          object[] valuesById = dbObject.GetValuesByID(ti.attrChangeGroupId, false);
          if (valuesById != null && valuesById.Length != 0)
            ti.ChangeGroupId = Convert.ToInt64(valuesById[0]);
        }
      }
      if (ti.ChangeGroupId != 0L)
        this.FillIndexesForOldIdents(ti);
      if (this.compTrace && ti.makeLog)
        this.iLH.AddToTrace($"Generating root complects ({session.UserName}) [{session.ComputerName}]", Intermech.Consts.traceAlways, this.logFileName);
      this.GenerateRootComplects(ti, session, scriptTreeNode1, contextID);
      if (this.compTrace && ti.makeLog)
        this.iLH.AddToTrace($"Generating complect documents ({session.UserName}) [{session.ComputerName}] context = {Convert.ToString(contextID)}", Intermech.Consts.traceAlways, this.logFileName);
      this.GenerateDocsInOrder(ti, session);
      if (this.compTrace && ti.makeLog)
        this.iLH.AddToTrace($"Complect docs generated ({session.UserName}) [{session.ComputerName}]  context = {Convert.ToString(contextID)}", Intermech.Consts.traceAlways, this.logFileName);
      if (!ti.testMode)
      {
        if (ti.docList.Count > 0)
        {
          int num = 0;
          DocRecord doc = ti.docList[num];
          while (num < ti.docList.Count)
          {
            this.IsTaskClientDead(ti);
            if (this.IsJobAborting(ti))
              return ExpertResult.Aborted;
            if ((doc.state & (DocState.AnyError | DocState.CondFalse | DocState.Empty | DocState.Aligned | DocState.Complect)) != DocState.NoFlags)
            {
              ++num;
              if (num < ti.docList.Count)
                doc = ti.docList[num];
            }
            else
            {
              if ((doc.state & DocState.Delayed) != DocState.NoFlags)
              {
                this.SetAlignedDoc(new ExpertServer.SetDocumentInfo(ti), ExpertServer.GetSessionGuid(ti), doc, ti.hiddenList[num], num, false);
                ++num;
                if (num < ti.docList.Count)
                  doc = ti.docList[num];
              }
              for (int index4 = 0; index4 < 10; ++index4)
                Thread.Sleep(10);
            }
          }
        }
      }
      else
      {
        for (int index5 = 0; index5 < ti.docList.Count; ++index5)
        {
          DocRecord doc = ti.docList[index5];
          if (!doc.IgnoreDoc())
          {
            ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index5];
            this.SetAlignedDoc(hidden.sDocInfo, ExpertServer.GetSessionGuid(ti), doc, hidden, index5, true);
          }
        }
      }
      if (this.compTrace && ti.makeLog)
        this.iLH.AddToTrace($"Generating sub-complects ({session.UserName}) [{session.ComputerName}]", Intermech.Consts.traceAlways, this.logFileName);
      this.GenerateNonRootComplects(ti, session, scriptTreeNode1, contextID);
      if (this.compTrace && ti.makeLog)
        this.iLH.AddToTrace($"Sub-complects generated. Setting links... ({session.UserName}) [{session.ComputerName}]", Intermech.Consts.traceAlways, this.logFileName);
      for (int index6 = 0; index6 < ti.docList.Count; ++index6)
      {
        DocRecord doc = ti.docList[index6];
        if (doc.IsComplect())
        {
          int totalLists = 0;
          this.CalcTotalLists(ti, index6, ref totalLists);
          ti.hiddenList[index6].totalLists = totalLists;
          if (doc.docObjectID != -1L)
          {
            (session as UserSession).DBObjectsCacheRemoveVersion(doc.docObjectID);
            IDBObject dbObject = session.GetObject(doc.docObjectID, false);
            if (dbObject != null && dbObject.ObjectModifyMode != ObjectModifyModes.CreateVersion && dbObject.ObjectModifyMode != ObjectModifyModes.CantModify)
            {
              if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                dbObject = dbObject.CheckOut(false);
              IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrCompListNum, false);
              if (dbAttribute != null)
                dbAttribute.AsInteger = (long) totalLists;
            }
          }
        }
      }
      this.SetLinks(ti, session);
      if (this.compTrace && ti.makeLog)
        this.iLH.AddToTrace($"Links are set! ({session.UserName}) [{session.ComputerName}]", Intermech.Consts.traceAlways, this.logFileName);
      int num1 = 1;
      for (int index7 = 0; index7 < ti.docList.Count; ++index7)
      {
        DocRecord doc = ti.docList[index7];
        if ((doc.state & DocState.Complect) == DocState.NoFlags && (doc.state & DocState.Ready) != DocState.NoFlags)
        {
          ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index7];
          if (hidden.idbO_ID != 0L && !hidden.DontNumber)
          {
            (session as UserSession).DBObjectsCacheRemoveVersion(hidden.idbO_ID);
            IDBObject dbObject = session.GetObject(hidden.idbO_ID, false);
            if (dbObject != null)
            {
              IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrListsBefore, false);
              if (dbAttribute != null)
              {
                if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                  this.CheckoutComplect(ti.oldComplect, session, dbObject.ObjectID);
                dbAttribute.AsInteger = (long) num1;
              }
              num1 += ti.hiddenList[index7].pageCount;
            }
          }
        }
      }
      for (int index8 = 0; index8 < ti.docList.Count; ++index8)
      {
        DocRecord doc = ti.docList[index8];
        ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index8];
        if ((doc.state & DocState.Complect) != DocState.NoFlags)
        {
          IDBObject dbObject = session.GetObject(doc.docObjectID, false);
          if (dbObject != null)
          {
            if (dbObject.IsCreationMode)
              dbObject.CommitCreation(true);
            if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
              dbObject.CheckOut();
            this.AssignComplectAttrs(session, ti, doc, hidden);
          }
          else
            continue;
        }
        if (((doc.state & DocState.Ready) != DocState.NoFlags || (doc.state & DocState.Delayed) != DocState.NoFlags) && hidden.idbO_ID != 0L)
        {
          IDBObject dbObject = session.GetObject(hidden.idbO_ID, false);
          if (dbObject != null)
          {
            try
            {
              if (dbObject.IsCreationMode)
              {
                dbObject.CommitCreation(true);
                if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                  dbObject = dbObject.CheckOut();
                hidden.idbO_ID = dbObject.ObjectID;
                ti.AddChangedDoc(hidden.idbO_ID, dbObject.ObjectType, DocOperType.Created);
              }
            }
            catch (Exception ex)
            {
              doc.state |= DocState.AccessError;
              doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
              ti.userReport.Add("Exception: " + ex.Message);
              ti.userReport.Add("Stack trace: " + ex.StackTrace);
              if (ex.InnerException != null)
              {
                ti.userReport.Add("Inner exception: " + ex.InnerException.Message);
                ti.userReport.Add("Inner exception stack: " + ex.InnerException.StackTrace);
              }
            }
          }
        }
      }
      if (ti.needSecondPass)
      {
        this.PerformSecondPass(ti, session);
        if (!ti.testMode)
        {
          int index9 = 0;
          while (index9 < ti.docList.Count)
          {
            DocRecord doc = ti.docList[index9];
            this.IsTaskClientDead(ti);
            if (this.IsJobAborting(ti))
              return ExpertResult.Aborted;
            if ((doc.state & (DocState.CondFalse | DocState.Empty | DocState.Complect | DocState.GenError)) != DocState.NoFlags)
              ++index9;
            else if ((doc.state & DocState.Delayed) != DocState.NoFlags && (doc.state & DocState.Aligned) == DocState.NoFlags && (doc.state & DocState.AnyError) == DocState.NoFlags)
            {
              for (int index10 = 0; index10 < 10; ++index10)
                Thread.Sleep(10);
            }
            else
              ++index9;
          }
        }
        else
        {
          for (int index11 = 0; index11 < ti.docList.Count; ++index11)
          {
            DocRecord doc = ti.docList[index11];
            if ((doc.state & DocState.Delayed) != DocState.NoFlags)
            {
              ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index11];
              this.SetAlignedDoc(hidden.sDocInfo, ExpertServer.GetSessionGuid(ti), doc, hidden, index11, true);
            }
          }
        }
      }
      if (gm == GenMode.genModeRefresh)
      {
        foreach (long key in ti.oldComplect.Keys)
        {
          ExpertServer.OldComplectElem oldComplectElem = ti.oldComplect[key];
          if (!oldComplectElem.complect && oldComplectElem.needDelete)
          {
            IDBObject dbObject = session.GetObject(key, false);
            if (dbObject != null)
            {
              ti.RemoveObj(dbObject.ObjectID);
              if (!((DBSessionable) dbObject).Deleted)
                dbObject.Delete(0L);
            }
          }
        }
        foreach (long key in ti.oldComplect.Keys)
        {
          ExpertServer.OldComplectElem oldComplectElem = ti.oldComplect[key];
          if (oldComplectElem.complect && oldComplectElem.needDelete)
          {
            IDBObject dbObject = session.GetObject(key, false);
            if (dbObject != null && !((DBSessionable) dbObject).Deleted)
            {
              ti.RemoveObj(dbObject.ObjectID);
              if (!((DBSessionable) dbObject).Deleted)
                dbObject.Delete(0L);
            }
          }
        }
      }
      List<ExpertServer.SortedItem> sortList = this.SortRelationsNew(ti, session, scriptTreeNode1);
      if (ti.needSecondPass)
      {
        for (int index12 = 0; index12 < sortList.Count; ++index12)
        {
          ExpertServer.SortedItem sortedItem = sortList[index12];
          if (sortedItem.IsComplect)
          {
            int totalLists = 0;
            this.CalcTotalLists(sortList, index12, ref totalLists);
            sortedItem.TotalLists = totalLists;
            if (sortedItem.DocCompId != -1L)
            {
              (session as UserSession).DBObjectsCacheRemoveVersion(sortedItem.DocCompId);
              try
              {
                IDBObject dbObject = session.GetObject(sortedItem.DocCompId, false);
                if (sortedItem.DocCompId < 0L && dbObject == null)
                {
                  dbObject = session.GetObject(-sortedItem.DocCompId, false);
                  if (dbObject != null && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                    dbObject = dbObject.CheckOut();
                }
                if (dbObject != null)
                {
                  if (dbObject.ObjectModifyMode != ObjectModifyModes.CreateVersion)
                  {
                    if (dbObject.ObjectModifyMode != ObjectModifyModes.CantModify)
                    {
                      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                        dbObject = dbObject.CheckOut(false);
                      IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrCompListNum, false);
                      if (dbAttribute != null)
                        dbAttribute.AsInteger = (long) totalLists;
                    }
                  }
                }
              }
              catch
              {
              }
            }
          }
        }
      }
      int totalNum = 0;
      sortList.Sort((Comparison<ExpertServer.SortedItem>) ((si1, si2) => (int) (si1.SortOrder - si2.SortOrder)));
      lock (ti)
      {
        int num2 = 1;
        for (int index13 = 0; index13 < sortList.Count; ++index13)
        {
          ExpertServer.SortedItem sortedItem = sortList[index13];
          if (!sortedItem.IsComplect && sortedItem.DocCompId != 0L && sortedItem.DocCompId != -1L)
          {
            (session as UserSession).DBObjectsCacheRemoveVersion(sortedItem.DocCompId);
            IDBObject dbObject = session.GetObject(sortedItem.DocCompId, false);
            if (sortedItem.DocCompId < 0L && dbObject == null)
            {
              dbObject = session.GetObject(-sortedItem.DocCompId, false);
              if (dbObject != null && dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                dbObject = dbObject.CheckOut();
            }
            if (dbObject != null)
            {
              IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrListsBefore, false);
              if (dbAttribute != null)
              {
                if (sortedItem.NumType == ExpertServer.NumberingType.DontNumber || sortedItem.NumType == ExpertServer.NumberingType.DontCount)
                {
                  dbAttribute.Value = (object) DBNull.Value;
                  if (sortedItem.NumType == ExpertServer.NumberingType.DontNumber)
                  {
                    totalNum += sortedItem.TotalLists;
                    continue;
                  }
                  continue;
                }
                if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                  this.CheckoutComplect(ti.oldComplect, session, dbObject.ObjectID);
                dbAttribute.AsInteger = (long) num2;
              }
              sortedItem.ListsBefore = num2;
              num2 += sortedItem.TotalLists;
              totalNum += sortedItem.TotalLists;
            }
          }
        }
      }
      lock (ti)
      {
        for (int index14 = 0; index14 < sortList.Count; ++index14)
        {
          ExpertServer.SortedItem sortedItem1 = sortList[index14];
          if (!sortedItem1.IsComplect && sortedItem1.DocCompId != 0L)
          {
            (session as UserSession).DBObjectsCacheRemoveVersion(sortedItem1.DocCompId);
            IDBObject dbObject = session.GetObject(sortedItem1.DocCompId, false);
            if (dbObject != null && sortedItem1.ParentIndex >= 0)
            {
              ExpertServer.SortedItem sortedItem2 = sortList[sortedItem1.ParentIndex];
              IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrCompListNum, false);
              if (dbAttribute != null)
              {
                if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                  this.CheckoutComplect(ti.oldComplect, session, dbObject.ObjectID);
                dbAttribute.AsInteger = (long) sortedItem2.TotalLists;
              }
              try
              {
                if (dbObject.IsCreationMode)
                {
                  dbObject.CommitCreation(true);
                  ti.AddChangedDoc(dbObject.ObjectID, dbObject.ObjectType, DocOperType.Created);
                }
              }
              catch (Exception ex)
              {
                ti.userReport.Add("Exception: " + ex.Message);
                ti.userReport.Add("Stack trace: " + ex.StackTrace);
                if (ex.InnerException != null)
                {
                  ti.userReport.Add("Inner exception: " + ex.InnerException.Message);
                  ti.userReport.Add("Inner exception stack: " + ex.InnerException.StackTrace);
                }
              }
            }
          }
        }
      }
      if (this.needListNumsOnLinks)
        this.SetRelListNums2(session, sortList, totalNum);
      for (int index15 = 0; index15 < sortList.Count; ++index15)
      {
        ExpertServer.SortedItem sortedItem = sortList[index15];
        if (sortedItem.DocCompId != 0L)
        {
          IDBObject dbObject = session.GetObject(sortedItem.DocCompId, false);
          if (dbObject == null && sortedItem.DocCompId < 0L)
            dbObject = session.GetObject(-sortedItem.DocCompId, false);
          if (dbObject != null && (dbObject as IDBLifecycleLevel).LevelID != dbObject.Session.IdentHelper.DeletedID)
          {
            if (ti.checkOutDocs)
            {
              if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == 0L)
                dbObject.CheckOut();
            }
            else if (dbObject.CheckoutBy != 0L)
              dbObject.CheckIn();
          }
        }
      }
      return complect;
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (EAbort))
        throw;
      EnumTypeHelper.GetCaption((Enum) (ex as EAbort).res);
      switch ((ex as EAbort).res)
      {
        case ExpertResult.NoCondParms:
        case ExpertResult.NoCalcParms:
        case ExpertResult.RuleNotFound:
        case ExpertResult.CircularReference:
          throw new ExpertServerException(ex.Message + LocalizationHolder.rm.GetString("Expert.Server_149") + this.GetNeedParmList(ti), ex);
        default:
          return ExpertResult.Aborted;
      }
    }
    finally
    {
      ti.CompGenMode = GenMode.genModeNone;
      this.EndJobForTask(ti);
      if (this.compTrace && ti.makeLog)
        this.iLH.AddToTrace($"Ending complect generation ({str1}) [{str2}]", Intermech.Consts.traceAlways, this.logFileName);
      ti.cacheTemplates = (ConcurrentDictionary<long, ImDocumentData>) null;
    }
  }

  internal void CalcTotalLists(ExpertServer.ExpServTask ti, int compIndex, ref int totalLists)
  {
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      DocRecord doc = ti.docList[index];
      if (doc.parentIndex == compIndex)
      {
        ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index];
        if (hidden.dontNumber != ExpertServer.NumberingType.DontCount)
          totalLists += hidden.pageCount;
        if (doc.IsComplect())
          this.CalcTotalLists(ti, index, ref totalLists);
      }
    }
  }

  internal void CalcTotalLists(
    List<ExpertServer.SortedItem> sortList,
    int compIndex,
    ref int totalLists)
  {
    for (int index = 0; index < sortList.Count; ++index)
    {
      ExpertServer.SortedItem sort = sortList[index];
      if (sort.ParentIndex == compIndex)
      {
        if (sort.IsComplect)
          this.CalcTotalLists(sortList, index, ref totalLists);
        else if (sort.NumType != ExpertServer.NumberingType.DontCount)
          totalLists += sort.TotalLists;
      }
    }
  }

  private void GetDocList(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode rootNode,
    long objID,
    bool dopComplects)
  {
    List<DocRecord> docList = new List<DocRecord>();
    List<ExpertServer.HiddenDocInfo> hiddenList = new List<ExpertServer.HiddenDocInfo>();
    HashSet<ExpertServer.OldKey> collectedDocs = new HashSet<ExpertServer.OldKey>();
    for (int index = 0; index < rootNode.Items.Count; ++index)
    {
      ScriptTreeNode node = (ScriptTreeNode) rootNode.Items[index];
      if (!node.label.StartsWith("#") && (node.opTag == ExpertScriptOp.opCreateComplect || node.opTag == ExpertScriptOp.opCreateDocument || node.opTag == ExpertScriptOp.opDocCopy))
        this.GetDocsForNode(ti, ius, node, objID, docList, hiddenList, -1, ti.nodeItems.Count, dopComplects, collectedDocs);
    }
    for (int index = ti.nodeItems.Count - 1; index >= 0; --index)
    {
      if (ti.nodeItems[index] == null)
        ti.nodeItems.RemoveAt(index);
    }
    ti.hiddenList = hiddenList;
    ti.docList = docList;
  }

  private int GetNodeLevel(ScriptTreeNode node)
  {
    int nodeLevel = 0;
    for (; node.parent != null; node = node.parent)
      ++nodeLevel;
    return nodeLevel;
  }

  private void GetDocsForNode(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long objID,
    List<DocRecord> docList,
    List<ExpertServer.HiddenDocInfo> hiddenList,
    int parentIndex,
    int itemsIndex,
    bool dopComplects,
    HashSet<ExpertServer.OldKey> collectedDocs,
    string dopCompTag = null)
  {
    TaskDataCache.ObjDataItem objData1 = ti.DataCache.GetObjData(objID, ius);
    if ((TypedInfoItem) objData1 == (TypedInfoItem) null)
    {
      if (!ti.makeLog)
        return;
      this.iLH.AddToTrace($"Context object NOT FOUND!!! ({objID.ToString()}) [{ius.ComputerName}]", Intermech.Consts.traceAlways, this.logFileName);
    }
    else
    {
      List<long> resList = new List<long>();
      List<int> items;
      if (itemsIndex < ti.nodeItems.Count)
      {
        items = ti.nodeItems[itemsIndex] != null ? ti.nodeItems[itemsIndex].items : new List<int>();
      }
      else
      {
        while (ti.nodeItems.Count <= itemsIndex)
          ti.nodeItems.Add((ExpertServer.NodeList) null);
        items = new List<int>();
      }
      List<int> childIndexes = new List<int>();
      for (int index = 0; index < node.Items.Count; ++index)
        childIndexes.Add(-1);
      int newIndex1 = parentIndex;
      switch (node.opTag)
      {
        case ExpertScriptOp.opCreateDocument:
          if (dopComplects && this.GetNodeLevel(node) < 3)
            return;
          OpCreateDoc op1 = (OpCreateDoc) node.op;
          int num1 = !(op1.objTypeGUID == "") ? MetaDataHelper.GetObjectTypeID(op1.objTypeGUID) : throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_296"));
          if (ExpertServer.IsTypeDescendant(num1, objData1.ItemTypeID))
          {
            resList.Add(objID);
          }
          else
          {
            ti.ClearUsedObjects();
            this.GetAllChildsByType(ti, objID, num1, resList);
          }
          DocRecord docRecord1 = (DocRecord) null;
          using (List<long>.Enumerator enumerator = resList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              long current = enumerator.Current;
              int newIndex2 = parentIndex;
              IDBObject dbObject1 = ius.GetObject(new Guid(op1.scriptGUID));
              if (dbObject1 != null)
              {
                long objectId = dbObject1.ObjectID;
                string str1 = "";
                IDBObject dbObject2 = (IDBObject) null;
                if (op1.groupMode == DocGroupMode.NoGroup)
                {
                  dbObject2 = ius.GetObject(current);
                  if (dbObject2 != null)
                  {
                    IDBAttribute byId = dbObject2.Attributes.FindByID(ExpertConsts.Consts.attrObjectNum);
                    if (byId != null)
                      str1 = byId.AsString;
                  }
                  if (str1 != "")
                    str1 += " ";
                }
                else
                {
                  HybridRowExp hybridRowExp = ti.savedDataByObjId(current);
                  if (op1.cond != null && op1.cond.Count != 0 && !ti.CheckRowCond(current, hybridRowExp ?? (HybridRowExp) null, op1.cond))
                  {
                    if (op1.groupMode == DocGroupMode.GroupCont)
                    {
                      docRecord1 = (DocRecord) null;
                      continue;
                    }
                    continue;
                  }
                }
                if (op1.createCond != null && op1.createCond.Count != 0)
                {
                  HybridRowExp hybridRowExp = ti.savedDataByObjId(current);
                  if (!ti.CheckRowCond(current, hybridRowExp ?? (HybridRowExp) null, op1.createCond))
                    continue;
                }
                if (docRecord1 == null)
                {
                  ExpertServer.OldKey oldKey = new ExpertServer.OldKey(current, objectId);
                  if (!collectedDocs.Contains(oldKey))
                  {
                    DocRecord docRecord2 = new DocRecord(str1 + op1.prefix, dbObject1.ObjectID, current);
                    docRecord2.docType = op1.docType;
                    if (op1.secondPass)
                    {
                      docRecord2.state |= DocState.Delayed;
                      ti.needSecondPass = true;
                    }
                    ExpertServer.HiddenDocInfo hiddenDocInfo = new ExpertServer.HiddenDocInfo(node);
                    docRecord2.parentIndex = parentIndex;
                    docRecord2.docNumber = docList.Count;
                    if (dbObject2 != null)
                      hiddenDocInfo.ID = dbObject2.ID;
                    hiddenDocInfo.prefix = op1.prefix;
                    hiddenDocInfo.dopCompTag = dopCompTag;
                    newIndex2 = docList.Count;
                    items.Add(newIndex2);
                    if (op1.useCoWorkerDoc)
                    {
                      HybridRowExp coWorkerDoc = this.GetCoWorkerDoc(ius, ti, docRecord2.objID, docRecord2.scriptID);
                      if (coWorkerDoc != null)
                      {
                        long int64_1 = Convert.ToInt64(coWorkerDoc[0]);
                        long int64_2 = Convert.ToInt64(coWorkerDoc[1]);
                        string str2 = Convert.ToString(coWorkerDoc[2]);
                        int int32 = Convert.ToInt32(coWorkerDoc[3]);
                        docRecord2.docObjectID = int64_1;
                        docRecord2.docName = str2;
                        hiddenDocInfo.ID = int64_2;
                        hiddenDocInfo.pageCount = int32;
                        hiddenDocInfo.idbO_ID = int64_1;
                        docRecord2.state |= DocState.CoWorker;
                        docRecord2.state |= DocState.Aligned;
                      }
                    }
                    hiddenDocInfo.SortNumber = (long) hiddenList.Count;
                    docList.Add(docRecord2);
                    hiddenList.Add(hiddenDocInfo);
                    if (op1.groupMode != DocGroupMode.NoGroup)
                    {
                      docRecord1 = docRecord2;
                      docRecord1.objIDList = new List<long>();
                    }
                  }
                }
                docRecord1?.objIDList.Add(current);
                this._PerformChildNodes(ti, node, ius, current, docList, hiddenList, newIndex2, childIndexes, dopComplects, collectedDocs, dopCompTag);
              }
            }
            break;
          }
        case ExpertScriptOp.opCreateComplect:
          OpCreateComplect op2 = (OpCreateComplect) node.op;
          int num2 = op2.objTypeGUID != "" ? MetaDataHelper.GetObjectTypeID(op2.objTypeGUID) : -1;
          if (op2.additional != dopComplects && this.GetNodeLevel(node) != 1)
            return;
          bool flag = ExpertServer.IsTypeDescendant(num2, objData1.ItemTypeID);
          if (node.parent.parent == null & flag)
          {
            resList.Add(objID);
          }
          else
          {
            ti.ClearUsedObjects();
            this.GetAllChildsByType(ti, objID, num2, resList);
            if (flag)
              resList.Add(objID);
          }
          using (List<long>.Enumerator enumerator = resList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              long current = enumerator.Current;
              if (op2.cond != null && op2.cond.Count > 0)
              {
                HybridRowExp hybridRowExp = ti.savedDataByObjId(current);
                if (!ti.CheckRowCond(current, hybridRowExp ?? (HybridRowExp) null, op2.cond))
                  continue;
              }
              if (dopComplects && dopCompTag != null && !ti.dopCompTags.Contains(dopCompTag))
                ti.dopCompTags.Add(dopCompTag);
              ExpertServer.HiddenDocInfo hiddenDocInfo = (ExpertServer.HiddenDocInfo) null;
              if (op2.needComplect)
              {
                TaskDataCache.ObjDataItem objData2 = ti.DataCache.GetObjData(current, ius);
                string str = op2.postfix != "" ? op2.postfix : LocalizationHolder.rm.GetString("Expert.Server_249");
                DocRecord docRecord3 = new DocRecord(objData2.Caption.EndsWith(" ") || str.StartsWith(" ") ? objData2.Caption + str : $"{objData2.Caption} {str}", ti.compScriptId, current);
                docRecord3.state = DocState.Complect;
                hiddenDocInfo = new ExpertServer.HiddenDocInfo(node);
                docRecord3.parentIndex = parentIndex;
                docRecord3.docNumber = docList.Count;
                hiddenDocInfo.ID = objData2.Id;
                newIndex1 = docList.Count;
                items.Add(newIndex1);
                hiddenDocInfo.SortNumber = (long) hiddenList.Count;
                docList.Add(docRecord3);
                hiddenList.Add(hiddenDocInfo);
              }
              string docCompTag = dopCompTag;
              if (hiddenDocInfo != null)
              {
                if (op2.additional && node.label != "")
                {
                  docCompTag = node.label;
                  hiddenDocInfo.dopCompTag = docCompTag;
                  if (dopComplects && !ti.dopCompTags.Contains(docCompTag))
                    ti.dopCompTags.Add(docCompTag);
                }
                else
                  hiddenDocInfo.dopCompTag = dopCompTag;
              }
              this._PerformChildNodes(ti, node, ius, current, docList, hiddenList, newIndex1, childIndexes, dopComplects, collectedDocs, docCompTag);
            }
            break;
          }
        case ExpertScriptOp.opDocCopy:
          if (dopComplects && this.GetNodeLevel(node) < 3)
            return;
          OpParmTiLink op3 = (OpParmTiLink) node.op;
          int objectTypeId1 = MetaDataHelper.GetObjectTypeID(op3.TiDocTypeGuid);
          int objectTypeId2 = MetaDataHelper.GetObjectTypeID(op3.NewDocTypeGuid);
          if (objectTypeId1 == -1)
            throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_293"), (object) op3.TiDocTypeGuid));
          if (objectTypeId2 == -1)
            throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_293"), (object) op3.NewDocTypeGuid));
          if (ExpertServer.IsTypeDescendant(objectTypeId1, objData1.ItemTypeID))
          {
            resList.Add(objID);
          }
          else
          {
            ti.ClearUsedObjects();
            this.GetAllChildsByType(ti, objID, objectTypeId1, resList);
          }
          int indexByName = ti.savedData.Columns.GetIndexByName("cad00047-306c-11d8-b4e9-00304f19f545");
          string str3 = "";
          IDBObject dbObject = ius.GetObject(objID);
          if (dbObject != null)
          {
            IDBAttribute byId = dbObject.Attributes.FindByID(ExpertConsts.Consts.attrObjectNum);
            if (byId != null)
              str3 = byId.AsString + " ";
          }
          using (List<long>.Enumerator enumerator = resList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              long current = enumerator.Current;
              HybridRowExp hybridRowExp = ti.savedDataByObjId(current);
              DocRecord docRecord4 = new DocRecord(str3 + LocalizationHolder.rm.GetString("Expert.Server_294") + (indexByName >= 0 ? hybridRowExp[indexByName] : (object) current.ToString()), current, objID);
              docRecord4.state |= DocState.DocLink;
              ExpertServer.HiddenDocInfo hiddenDocInfo = new ExpertServer.HiddenDocInfo(node);
              docRecord4.parentIndex = parentIndex;
              docRecord4.docNumber = docList.Count;
              hiddenDocInfo.dopCompTag = dopCompTag;
              int count = docList.Count;
              items.Add(count);
              hiddenDocInfo.SortNumber = (long) hiddenList.Count;
              docList.Add(docRecord4);
              hiddenList.Add(hiddenDocInfo);
              this._PerformChildNodes(ti, node, ius, current, docList, hiddenList, count, childIndexes, dopComplects, collectedDocs, dopCompTag);
            }
            break;
          }
      }
      if (items.Count <= 0 || ti.nodeItems[itemsIndex] != null)
        return;
      ti.nodeItems[itemsIndex] = new ExpertServer.NodeList(node, items);
    }
  }

  private void _PerformChildNodes(
    ExpertServer.ExpServTask ti,
    ScriptTreeNode node,
    IUserSession ius,
    long oID,
    List<DocRecord> docList,
    List<ExpertServer.HiddenDocInfo> hiddenList,
    int newIndex,
    List<int> childIndexes,
    bool dopComplects,
    HashSet<ExpertServer.OldKey> collectedDocs,
    string docCompTag = null)
  {
    if (node.Items == null || node.Items.Count <= 0)
      return;
    for (int index = 0; index < node.Items.Count; ++index)
    {
      ScriptTreeNode node1 = (ScriptTreeNode) node.Items[index];
      if (!node1.label.StartsWith("#"))
      {
        if (childIndexes[index] < 0)
          childIndexes[index] = this.GetChildIndex(ti.nodeItems, node1);
        this.GetDocsForNode(ti, ius, node1, oID, docList, hiddenList, newIndex, childIndexes[index], dopComplects, collectedDocs, docCompTag);
      }
    }
  }

  private int GetChildIndex(List<ExpertServer.NodeList> nodeItems, ScriptTreeNode node)
  {
    for (int index = 0; index < nodeItems.Count; ++index)
    {
      ExpertServer.NodeList nodeItem = nodeItems[index];
      if (nodeItem != null && nodeItem.node == node)
        return index;
    }
    return nodeItems.Count;
  }

  private void GetAllChildsByType(
    ExpertServer.ExpServTask ti,
    long objId,
    int typeId,
    List<long> resList)
  {
    List<HybridRowExp> rows = ti.savedLinksByProjIndex2(objId);
    if (rows == null || rows.Count == 0)
      return;
    int indexByName = ti.savedLinks.Columns.GetIndexByName(ExpertAttrGUIDs.attrSorting);
    if (rows.Count > 1 && indexByName >= 0)
      ti.savedLinks.SortList(rows, new List<int>(1)
      {
        indexByName + 1
      });
    foreach (HybridRowExp hybridRowExp1 in rows)
    {
      long int64_1 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
      HybridRowExp hybridRowExp2 = ti.savedDataByPartId(int64_1);
      if (hybridRowExp2 != null)
      {
        int int32 = Convert.ToInt32(hybridRowExp2["cad0002e-306c-11d8-b4e9-00304f19f545"]);
        long int64_2 = Convert.ToInt64(hybridRowExp2["cad00029-306c-11d8-b4e9-00304f19f545"]);
        if (ti.UsedObjects.Contains(int64_2))
        {
          if (ti.makeTrace)
          {
            XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_44"));
            string Text = string.Format(LocalizationHolder.rm.GetString("Expert.Server_299"), (object) objId, (object) int64_2);
            ti.traceAddText(node, Text);
          }
        }
        else
        {
          ti.UsedObjects.Add(int64_2);
          try
          {
            if (ExpertServer.IsTypeDescendant(typeId, int32))
              resList.Add(int64_2);
            this.GetAllChildsByType(ti, int64_2, typeId, resList);
          }
          finally
          {
            ti.UsedObjects.Remove(int64_2);
          }
        }
      }
    }
  }

  private void GenerateDocs(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode rootNode,
    long objID)
  {
    for (int index = 0; index < rootNode.Items.Count; ++index)
    {
      ScriptTreeNode node = (ScriptTreeNode) rootNode.Items[index];
      if ((index != 0 || node.opTag != ExpertScriptOp.opObjDescendants) && node.opTag != ExpertScriptOp.opGlobRoot)
      {
        if (this.IsJobAborting(ti))
          break;
        this.PerformNodeForDocs(ti, ius, node);
      }
    }
  }

  private void GenerateDocsInOrder(ExpertServer.ExpServTask ti, IUserSession ius)
  {
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      DocRecord doc = ti.docList[index];
      if (!doc.IsComplect())
      {
        ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index];
        if (this.IsJobAborting(ti))
          break;
        if (hidden.genNode.op is OpCreateDoc op)
        {
          if (ti.makeLog)
            this.iLH.AddToTrace($"=============  Generating document [{doc.docName}]", Intermech.Consts.traceAlways, this.logFileName);
          this.GenerateDoc(ti, ius, index, op);
        }
        else if (hidden.genNode.op is OpParmTiLink)
          this.MakeCopyLink(ti, ius, hidden.genNode);
      }
    }
  }

  private void PerformNodeForDocs(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node)
  {
    if (this.IsTaskClientDead(ti))
      return;
    switch (node.opTag)
    {
      case ExpertScriptOp.opCreateDocument:
        this.MakeDocsForNode(ti, ius, node);
        break;
      case ExpertScriptOp.opDocCopy:
        this.MakeCopyLink(ti, ius, node);
        break;
    }
    if (node.Items == null || node.Items.Count <= 0)
      return;
    for (int index = 0; index < node.Items.Count; ++index)
    {
      ScriptTreeNode node1 = (ScriptTreeNode) node.Items[index];
      this.PerformNodeForDocs(ti, ius, node1);
    }
  }

  private void GenerateRootComplects(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode rootNode,
    long objID)
  {
    for (int index = 0; index < rootNode.Items.Count; ++index)
    {
      ScriptTreeNode node = (ScriptTreeNode) rootNode.Items[index];
      if (index != 0 || node.opTag != ExpertScriptOp.opObjDescendants)
      {
        if (this.IsJobAborting(ti))
          break;
        if (node.opTag == ExpertScriptOp.opCreateComplect)
          this.MakeComplectForNode(ti, ius, node, true);
      }
    }
  }

  private void GenerateNonRootComplects(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode rootNode,
    long objID)
  {
    for (int index = 0; index < rootNode.Items.Count; ++index)
    {
      ScriptTreeNode scriptTreeNode = (ScriptTreeNode) rootNode.Items[index];
      if (index != 0 || scriptTreeNode.opTag != ExpertScriptOp.opObjDescendants)
      {
        if (this.IsJobAborting(ti))
          break;
        if (scriptTreeNode.Items != null)
        {
          foreach (ScriptTreeNode node in scriptTreeNode.Items)
            this.PerformNodeForComplects(ti, ius, node);
        }
      }
    }
  }

  private void PerformNodeForComplects(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node)
  {
    if (node.Items != null && node.Items.Count > 0)
    {
      for (int index = 0; index < node.Items.Count; ++index)
      {
        ScriptTreeNode node1 = (ScriptTreeNode) node.Items[index];
        this.PerformNodeForComplects(ti, ius, node1);
      }
    }
    if (node.opTag != ExpertScriptOp.opCreateComplect)
      return;
    this.MakeComplectForNode(ti, ius, node, false);
  }

  private void GenerateDoc(ExpertServer.ExpServTask ti, IUserSession ius, int i, OpCreateDoc opd)
  {
    if (this.IsJobAborting(ti))
      return;
    ExpertServer.HiddenDocInfo hidden = ti.hiddenList[i];
    DocRecord doc = ti.docList[i];
    if (opd.secondPass)
    {
      IDBObject dbObject = ius.GetObject(doc.scriptID, false);
      if (dbObject == null)
        return;
      string asString = dbObject.GetAttributeByID(ExpertConsts.Consts.attrGenDocType).AsString;
      if (asString != "")
        hidden.docType = MetaDataHelper.GetObjectTypeID(new Guid(asString));
      hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
    }
    else
    {
      hidden.SetDontNumber(opd.dontNumber, opd.dontCount);
      XmlNode xmlNode = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_267"));
      if (xmlNode != null)
        ti.curNode.AppendChild(xmlNode);
      ti.traceAddText(xmlNode, string.Format(LocalizationHolder.rm.GetString("Expert.Server_268"), (object) doc.docName, (object) doc.objID));
      XmlNode curNode = ti.curNode;
      ti.traceSetNode(xmlNode);
      XmlDocument traceInfo = ti.traceInfo;
      try
      {
        if ((doc.state & DocState.CoWorker) != DocState.NoFlags)
        {
          this.ReportCoWorkerDoc(ti, doc);
        }
        else
        {
          if (opd.cond != null && opd.cond.Count > 0)
          {
            HybridRowExp hybridRowExp = ti.savedDataByObjId(doc.objID);
            if (!ti.CheckRowCond(doc.objID, hybridRowExp ?? (HybridRowExp) null, opd.cond))
            {
              doc.state |= DocState.CondFalse;
              return;
            }
          }
          bool flag = false;
          switch (opd.docType)
          {
            case "Y":
              DBReportScenario dbReportScenario = (DBReportScenario) ius.GetObject(doc.scriptID);
              IDBAttribute attributeById = dbReportScenario.GetAttributeByID(ExpertConsts.Consts.attrGenDocType);
              if (attributeById != null && attributeById.Value.NotDBNull())
              {
                Guid objTypeGuid = new Guid(Convert.ToString(attributeById.Value));
                if (objTypeGuid != Guid.Empty)
                  hidden.docType = MetaDataHelper.GetObjectTypeID(objTypeGuid);
              }
              try
              {
                flag = !dbReportScenario.Execute((object) ius.SessionGUID, new long[1]
                {
                  doc.objID
                });
                dbReportScenario.Document.Position = 0L;
                ti.docData = ImDocumentData.LoadFromXml(dbReportScenario.Document);
                IDBObject dbObject = ius.GetObject(doc.objID, false);
                if (dbObject != null)
                  doc.docName = dbObject.Caption + hidden.prefix;
                hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
                if (dbReportScenario.DocumentAttributes != null)
                {
                  using (Dictionary<Guid, string>.Enumerator enumerator = dbReportScenario.DocumentAttributes.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      KeyValuePair<Guid, string> current = enumerator.Current;
                      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(current.Key);
                      if (attributeTypeId != 0 && (!string.IsNullOrEmpty(current.Value) || attributeTypeId != ExpertConsts.Consts._attrObjName && attributeTypeId != ExpertConsts.Consts._attrObjDesign))
                      {
                        if (hidden.sDocInfo.DocAttrs.ContainsKey(attributeTypeId))
                          hidden.sDocInfo.DocAttrs[attributeTypeId] = (object) current.Value;
                        else
                          hidden.sDocInfo.DocAttrs.Add(attributeTypeId, (object) current.Value);
                      }
                    }
                    break;
                  }
                }
                break;
              }
              catch (Exception ex)
              {
                doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
                doc.state |= DocState.GenError;
                break;
              }
            case "":
            case "N":
              ti.traceInfo = (XmlDocument) null;
              ti.InitTraceInfo();
              ti.docData = (ImDocumentData) null;
              ti.defRootNode = (DocumentTreeNode) null;
              ti.curDocNode = (DocumentTreeNode) null;
              this.InnerSetParm(ti, new CalcAttrPair(-1L, ExpertConsts.Consts.attrEmptyDoc), (object) opd.noEmpty);
              try
              {
                if (this.IsTaskClientDead(ti))
                  return;
                int docType = -1;
                bool makeTrace = ti.makeTrace;
                try
                {
                  string docName = "";
                  long[] numArray;
                  if (doc.objIDList == null || doc.objIDList.Count <= 0)
                    numArray = new long[1]{ doc.objID };
                  else
                    numArray = doc.objIDList.ToArray();
                  long[] context = numArray;
                  int document = (int) this._GenerateDocument(ti.taskId, doc.scriptID, context, out docType, out docName);
                  if (docName != "")
                    doc.docName = $"{ExpertServer.GetDocName(ius, ti, context[0], docName)} {doc.docName}";
                }
                finally
                {
                  ti.makeTrace = makeTrace;
                }
                hidden.docType = docType;
                hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
              }
              catch (Exception ex)
              {
                doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
                doc.state |= DocState.GenError;
              }
              flag = Convert.ToBoolean(this.InnerGetParm(ti, ExpertConsts.Consts.attrEmptyDoc));
              break;
            case "T":
              ti.docData = (ImDocumentData) null;
              flag = true;
              ITechCardDocumentService service = (ITechCardDocumentService) this._serviceProvider.GetService(typeof (ITechCardDocumentService));
              if (service == null)
              {
                doc.errorMsg = LocalizationHolder.rm.GetString("Expert.Server_295");
                doc.state |= DocState.GenError;
                ti.docData = (ImDocumentData) null;
                break;
              }
              ImDocumentData documentData;
              if (service.GenerateDocument(ius.SessionGUID, new TechCardDocumentGenerateParameter(doc.scriptID, doc.objID)
              {
                ExpertTaskId = ti.taskId
              }, out documentData))
              {
                ti.docData = documentData;
                flag = ti.docData == null;
                if (ti.docData != null)
                  flag = new IsEmptyDocumentHandler(new IValidateDocumentAction[2]
                  {
                    (IValidateDocumentAction) new IsEmptyDocumentFlowAction(),
                    (IValidateDocumentAction) new IsEmptyDocumentContainersAction()
                  }).Execute(ti.docData);
              }
              hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
              break;
          }
          if (flag && opd.noEmpty)
          {
            doc.state |= DocState.Empty;
          }
          else
          {
            if (ti.docData == null)
              return;
            ti.docData.SetIsPartOfComplectPageNumbering(!hidden.DontNumber, false, false);
            ti.docData.SetIsPartOfComplectPageCount(hidden.dontNumber != ExpertServer.NumberingType.DontCount, false, false);
            hidden.zippedDoc = this.PackDocumentData(ti.docData);
          }
        }
      }
      finally
      {
        if (ti.traceInfo != null)
          hidden.zippedInfo = this.PackXml(ti.traceInfo);
        if (hidden.zippedDoc != null)
        {
          lock (ti)
            doc.state |= DocState.Ready;
        }
        ti.curNode = curNode;
        ti.traceInfo = traceInfo;
      }
    }
  }

  private void MakeDocsForNode(ExpertServer.ExpServTask ti, IUserSession ius, ScriptTreeNode node)
  {
    OpCreateDoc op = (OpCreateDoc) node.op;
    if (node.label.StartsWith("#"))
      return;
    List<int> intList = (List<int>) null;
    for (int index = 0; index < ti.nodeItems.Count; ++index)
    {
      if (ti.nodeItems[index].node == node)
      {
        intList = ti.nodeItems[index].items;
        break;
      }
    }
    if (intList == null)
      return;
    for (int index = 0; index < intList.Count && !this.IsJobAborting(ti); ++index)
    {
      ExpertServer.HiddenDocInfo hidden = ti.hiddenList[intList[index]];
      DocRecord doc = ti.docList[intList[index]];
      if (op.secondPass)
      {
        IDBObject dbObject = ius.GetObject(doc.scriptID, false);
        if (dbObject != null)
        {
          hidden.docType = -1;
          IDBAttribute attributeById = dbObject.GetAttributeByID(ExpertConsts.Consts.attrGenDocType);
          if (attributeById != null)
          {
            string asString = attributeById.AsString;
            if (asString != "")
              hidden.docType = MetaDataHelper.GetObjectTypeID(new Guid(asString));
          }
          hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
        }
      }
      else
      {
        hidden.SetDontNumber(op.dontNumber, op.dontCount);
        XmlNode xmlNode = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_267"));
        if (xmlNode != null)
          ti.curNode.AppendChild(xmlNode);
        ti.traceAddText(xmlNode, string.Format(LocalizationHolder.rm.GetString("Expert.Server_268"), (object) doc.docName, (object) doc.objID));
        XmlNode curNode = ti.curNode;
        ti.traceSetNode(xmlNode);
        XmlDocument traceInfo = ti.traceInfo;
        try
        {
          if ((doc.state & DocState.CoWorker) != DocState.NoFlags)
          {
            this.ReportCoWorkerDoc(ti, doc);
          }
          else
          {
            if (op.cond != null && op.cond.Count > 0)
            {
              HybridRowExp hybridRowExp = ti.savedDataByObjId(doc.objID);
              if (!ti.CheckRowCond(doc.objID, hybridRowExp ?? (HybridRowExp) null, op.cond))
              {
                doc.state |= DocState.CondFalse;
                continue;
              }
            }
            bool flag = false;
            switch (op.docType)
            {
              case "Y":
                DBReportScenario dbReportScenario = (DBReportScenario) ius.GetObject(doc.scriptID);
                IDBAttribute attributeById = dbReportScenario.GetAttributeByID(ExpertConsts.Consts.attrGenDocType);
                if (attributeById != null && attributeById.Value.NotDBNull())
                {
                  Guid objTypeGuid = new Guid(Convert.ToString(attributeById.Value));
                  if (objTypeGuid != Guid.Empty)
                    hidden.docType = MetaDataHelper.GetObjectTypeID(objTypeGuid);
                }
                try
                {
                  flag = !dbReportScenario.Execute((object) ius.SessionGUID, new long[1]
                  {
                    doc.objID
                  });
                  dbReportScenario.Document.Position = 0L;
                  ti.docData = ImDocumentData.LoadFromXml(dbReportScenario.Document);
                  IDBObject dbObject = ius.GetObject(doc.objID, false);
                  if (dbObject != null)
                    doc.docName = dbObject.Caption + hidden.prefix;
                  hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
                  if (dbReportScenario.DocumentAttributes != null)
                  {
                    using (Dictionary<Guid, string>.Enumerator enumerator = dbReportScenario.DocumentAttributes.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        KeyValuePair<Guid, string> current = enumerator.Current;
                        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(current.Key);
                        if (attributeTypeId != 0)
                          hidden.sDocInfo.DocAttrs.Add(attributeTypeId, (object) current.Value);
                      }
                      break;
                    }
                  }
                  break;
                }
                catch (Exception ex)
                {
                  doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
                  doc.state |= DocState.GenError;
                  break;
                }
              case "":
              case "N":
                ti.traceInfo = (XmlDocument) null;
                ti.InitTraceInfo();
                ti.docData = (ImDocumentData) null;
                ti.defRootNode = (DocumentTreeNode) null;
                ti.curDocNode = (DocumentTreeNode) null;
                this.InnerSetParm(ti, new CalcAttrPair(-1L, ExpertConsts.Consts.attrEmptyDoc), (object) op.noEmpty);
                try
                {
                  if (this.IsTaskClientDead(ti))
                    return;
                  int docType = -1;
                  bool makeTrace = ti.makeTrace;
                  try
                  {
                    string docName = "";
                    long[] numArray;
                    if (doc.objIDList == null || doc.objIDList.Count <= 0)
                      numArray = new long[1]{ doc.objID };
                    else
                      numArray = doc.objIDList.ToArray();
                    long[] context = numArray;
                    int document = (int) this._GenerateDocument(ti.taskId, doc.scriptID, context, out docType, out docName);
                    if (docName != "")
                      doc.docName = $"{ExpertServer.GetDocName(ius, ti, context[0], docName)} {doc.docName}";
                  }
                  finally
                  {
                    ti.makeTrace = makeTrace;
                  }
                  hidden.docType = docType;
                  hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
                }
                catch (Exception ex)
                {
                  doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
                  doc.state |= DocState.GenError;
                }
                flag = Convert.ToBoolean(this.InnerGetParm(ti, ExpertConsts.Consts.attrEmptyDoc));
                break;
              case "T":
                ti.docData = (ImDocumentData) null;
                flag = true;
                ITechCardDocumentService service = (ITechCardDocumentService) this._serviceProvider.GetService(typeof (ITechCardDocumentService));
                if (service == null)
                {
                  doc.errorMsg = LocalizationHolder.rm.GetString("Expert.Server_295");
                  doc.state |= DocState.GenError;
                  ti.docData = (ImDocumentData) null;
                  break;
                }
                ImDocumentData documentData;
                if (service.GenerateDocument(ius.SessionGUID, new TechCardDocumentGenerateParameter(doc.scriptID, doc.objID)
                {
                  ExpertTaskId = ti.taskId
                }, out documentData))
                {
                  ti.docData = documentData;
                  flag = ti.docData == null;
                  if (ti.docData != null)
                    flag = new IsEmptyDocumentHandler(new IValidateDocumentAction[2]
                    {
                      (IValidateDocumentAction) new IsEmptyDocumentFlowAction(),
                      (IValidateDocumentAction) new IsEmptyDocumentContainersAction()
                    }).Execute(ti.docData);
                }
                hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
                break;
            }
            if (flag && op.noEmpty)
              doc.state |= DocState.Empty;
            else if (ti.docData != null)
            {
              ti.docData.SetIsPartOfComplectPageNumbering(!hidden.DontNumber, false, false);
              ti.docData.SetIsPartOfComplectPageCount(hidden.dontNumber != ExpertServer.NumberingType.DontCount, false, false);
              hidden.zippedDoc = this.PackDocumentData(ti.docData);
            }
          }
        }
        finally
        {
          if (ti.traceInfo != null)
            hidden.zippedInfo = this.PackXml(ti.traceInfo);
          if (hidden.zippedDoc != null)
          {
            lock (ti)
              doc.state |= DocState.Ready;
          }
          ti.curNode = curNode;
          ti.traceInfo = traceInfo;
        }
      }
    }
  }

  private void ReportCoWorkerDoc(ExpertServer.ExpServTask ti, DocRecord dr)
  {
    if (ti.makeTrace)
    {
      ti.traceInfo = (XmlDocument) null;
      ti.InitTraceInfo();
      XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_224"));
      if (node != null)
      {
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_225"), Convert.ToString(dr.scriptID));
        ti.traceAddText(node, LocalizationHolder.rm.GetString("Expert.Server_282"));
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_283"), dr.docName + Convert.ToString(dr.docObjectID));
      }
    }
    dr.state |= DocState.Ready;
  }

  private void MakeCopyLink(ExpertServer.ExpServTask ti, IUserSession ius, ScriptTreeNode node)
  {
    OpParmTiLink op = (OpParmTiLink) node.op;
    if (node.label.StartsWith("#"))
      return;
    List<int> intList = (List<int>) null;
    for (int index = 0; index < ti.nodeItems.Count; ++index)
    {
      if (ti.nodeItems[index].node == node)
      {
        intList = ti.nodeItems[index].items;
        break;
      }
    }
    if (intList == null)
      return;
    for (int index = 0; index < intList.Count; ++index)
    {
      ExpertServer.HiddenDocInfo hidden = ti.hiddenList[intList[index]];
      DocRecord doc = ti.docList[intList[index]];
      int objectTypeId = MetaDataHelper.GetObjectTypeID(op.NewDocTypeGuid);
      ExpertServer.OldKey key = new ExpertServer.OldKey(Math.Abs(doc.objID), Math.Abs(doc.scriptID));
      if (ti.oldIdents.ContainsKey(key))
      {
        doc.docObjectID = ti.oldIdents[key].objId;
        hidden.idbO_ID = doc.docObjectID;
        if (doc.scriptID < 0L)
        {
          IDBObject dstObj = ius.GetObject(doc.docObjectID, false);
          IDBObject srcObj = ius.GetObject(doc.scriptID, false);
          if (dstObj != null && srcObj != null)
          {
            if (dstObj.ObjectModifyMode == ObjectModifyModes.Checkout)
              dstObj = dstObj.CheckOut();
            dstObj.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"))?.ClearValues();
            DbHelper.CopyFile(srcObj, dstObj);
          }
        }
      }
      else
      {
        try
        {
          IDBObject dstObj = ius.GetObjectCollection(objectTypeId).Create();
          IDBObject srcObj = ius.GetObject(doc.scriptID, false);
          DbHelper.CopyFile(srcObj, dstObj);
          DbHelper.CopyAttrs(srcObj, dstObj, (IEnumerable<string>) op.dataAttrGUIDs);
          IDBAttribute attributeById = dstObj.GetAttributeByID(ExpertConsts.Consts._attrObjName);
          if (attributeById != null)
            attributeById.AsString = doc.docName;
          IDBAttribute dbAttribute = dstObj.Attributes.AddAttribute(ExpertConsts.Consts.attrSourceLink, false);
          if (dbAttribute != null)
            dbAttribute.AsInteger = doc.scriptID;
          dstObj.CommitCreation(true);
          if (dstObj.ObjectModifyMode == ObjectModifyModes.Checkout)
            dstObj = dstObj.CheckOut();
          hidden.idbO_ID = dstObj.ObjectID;
          doc.docObjectID = hidden.idbO_ID;
        }
        catch (Exception ex)
        {
          doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
          doc.state |= DocState.GenError;
        }
      }
      lock (ti)
      {
        hidden.docType = objectTypeId;
        doc.state |= DocState.Ready;
        hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
      }
    }
  }

  private IDBObject GetPrevCompVersion(long ParentID, long compTemplId) => (IDBObject) null;

  private bool MakeComplectForNode(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    bool rootComplect)
  {
    List<int> intList = (List<int>) null;
    List<bool> boolList = (List<bool>) null;
    for (int index = 0; index < ti.nodeItems.Count; ++index)
    {
      if (ti.nodeItems[index].node == node)
      {
        intList = ti.nodeItems[index].items;
        boolList = new List<bool>();
        break;
      }
    }
    if (intList == null)
      return false;
    if (node.parent != null && node.parent.parent != null)
    {
      for (int index = 0; index < intList.Count; ++index)
        boolList.Add(true);
      foreach (DocRecord doc in ti.docList)
      {
        if (doc.docObjectID != -1L)
        {
          int index = intList.IndexOf(doc.parentIndex);
          if (index >= 0)
            boolList[index] = false;
        }
      }
      for (int index = intList.Count - 1; index >= 0; --index)
      {
        if (boolList[index])
          intList.RemoveAt(index);
      }
      if (intList.Count == 0)
        return false;
    }
    for (int index = 0; index < intList.Count; ++index)
    {
      ExpertServer.HiddenDocInfo hidden = ti.hiddenList[intList[index]];
      DocRecord doc = ti.docList[intList[index]];
      if ((doc.state & DocState.CoWorker) == DocState.NoFlags)
      {
        int objectType = ExpertConsts.Consts.objDocTPComplect;
        if (node.opTag == ExpertScriptOp.opCreateComplect && (node.op as OpCreateComplect).compObjTypeGUID != "")
          objectType = MetaDataHelper.GetObjectTypeID((node.op as OpCreateComplect).compObjTypeGUID);
        IDBObjectCollection objectCollection = ius.GetObjectCollection(objectType);
        switch (ti.CompGenMode)
        {
          case GenMode.genModeGenerate:
            if (doc.objID == ti.contextID)
              this.GetPrevCompVersion(doc.objID, ti.compScriptId);
            IDBObject idbO1 = objectCollection.Create();
            try
            {
              this.SetComplectAttrs(ti, idbO1, doc, hidden);
              if (rootComplect)
              {
                IDBRelation dbRelation = ius.GetRelationCollection(ExpertConsts.Consts.linkSimpleSortId).Create(doc.objID, idbO1.ObjectID);
                ti.AddChangedRel(dbRelation.RelationID, dbRelation.RelationType, dbRelation.ProjID, DocOperType.Created);
                hidden.RelationID = dbRelation.RelationID;
                idbO1.CommitCreation(true);
              }
              doc.docObjectID = idbO1.ObjectID;
              ExpertServer.OldKey key = new ExpertServer.OldKey(Math.Abs(doc.objID), Math.Abs(doc.scriptID));
              ti.oldIdents.GetOrAdd(key, new ExpertServer.IdentPair(doc.docObjectID, idbO1.ID));
              ti.docListIndex.GetOrAdd(idbO1.ID, intList[index]);
              hidden.ID = idbO1.ID;
              if (!idbO1.IsCreationMode)
                ti.AddChangedDoc(doc.docObjectID, idbO1.ObjectType, DocOperType.Created);
              doc.state |= DocState.Ready;
              continue;
            }
            catch (Exception ex)
            {
              doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
              doc.state |= DocState.AccessError;
              ti.userReport.Add("Exception: " + ex.Message);
              ti.userReport.Add("Stack trace: " + ex.StackTrace);
              if (ex.InnerException != null)
              {
                ti.userReport.Add("Inner exception: " + ex.InnerException.Message);
                ti.userReport.Add("Inner exception stack: " + ex.InnerException.StackTrace);
                continue;
              }
              continue;
            }
          case GenMode.genModeVersion:
          case GenMode.genModeRefresh:
            IDBObject idbO2 = (IDBObject) null;
            ExpertServer.OldKey key1 = new ExpertServer.OldKey(Math.Abs(doc.objID), Math.Abs(doc.scriptID));
            bool flag1 = ti.oldIdents.ContainsKey(key1);
            if (flag1)
            {
              long objId = ti.oldIdents[key1].objId;
              hidden.prevVerId = objId;
              if (ti.CompGenMode == GenMode.genModeVersion)
              {
                idbO2 = objectCollection.CreateVersion(objId);
              }
              else
              {
                (ius as UserSession).DBObjectsCacheRemoveVersion(objId);
                idbO2 = ius.GetObject(objId);
              }
            }
            bool flag2 = false;
            if (idbO2 == null)
            {
              idbO2 = objectCollection.Create();
              flag2 = true;
            }
            if (idbO2 != null)
            {
              try
              {
                if (idbO2.ObjectModifyMode == ObjectModifyModes.Checkout)
                  idbO2 = idbO2.CheckOut();
                this.SetComplectAttrs(ti, idbO2, doc, hidden);
              }
              catch (Exception ex)
              {
                doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
                doc.state |= DocState.AccessError;
                ti.userReport.Add("Exception: " + ex.Message);
                ti.userReport.Add("Stack trace: " + ex.StackTrace);
                if (ex.InnerException != null)
                {
                  ti.userReport.Add("Inner exception: " + ex.InnerException.Message);
                  ti.userReport.Add("Inner exception stack: " + ex.InnerException.StackTrace);
                }
              }
              doc.docObjectID = idbO2.ObjectID;
              if (flag1)
              {
                if (flag2)
                  ti.oldIdents[key1].objId = idbO2.ObjectID;
              }
              else
              {
                ti.oldIdents.GetOrAdd(new ExpertServer.OldKey(Math.Abs(doc.objID), Math.Abs(doc.scriptID)), new ExpertServer.IdentPair(doc.docObjectID, idbO2.ID));
                if (!idbO2.IsCreationMode)
                  ti.AddChangedDoc(doc.docObjectID, idbO2.ObjectType, DocOperType.Created);
              }
              if (ti.CompGenMode == GenMode.genModeRefresh && hidden.prevVerId != 0L && ti.oldComplect.ContainsKey(hidden.prevVerId))
                ti.oldComplect[hidden.prevVerId].needDelete = false;
              ti.docListIndex.GetOrAdd(idbO2.ID, intList[index]);
              hidden.ID = idbO2.ID;
            }
            doc.state |= DocState.Ready;
            continue;
          default:
            continue;
        }
      }
    }
    return true;
  }

  private void AssignComplectAttrs(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    DocRecord dr,
    ExpertServer.HiddenDocInfo hdi)
  {
    IDBObject idbO = ius.GetObject(dr.docObjectID, false);
    if (idbO == null)
      return;
    IDBObjectType objectType = ius.GetObjectType(idbO.ObjectType);
    bool flag = false;
    hdi.sDocInfo = new ExpertServer.SetDocumentInfo(ti, true);
    if (idbO.ObjectModifyMode == ObjectModifyModes.Checkout)
    {
      idbO = idbO.CheckOut();
      flag = true;
    }
    this._SetDocAttributes(hdi.sDocInfo, idbO, objectType);
    if (!flag)
      return;
    idbO.CheckIn();
  }

  private void PerformSecondPass(ExpertServer.ExpServTask ti, IUserSession ius)
  {
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index];
      DocRecord doc = ti.docList[index];
      if (hidden.genNode.op is OpCreateDoc)
      {
        OpCreateDoc op = (OpCreateDoc) hidden.genNode.op;
        if (op.secondPass)
        {
          if ((doc.state & DocState.CoWorker) != DocState.NoFlags)
          {
            this.ReportCoWorkerDoc(ti, doc);
          }
          else
          {
            if (op.cond != null && op.cond.Count > 0)
            {
              HybridRowExp hybridRowExp = ti.savedDataByObjId(doc.objID);
              if (!ti.CheckRowCond(doc.objID, hybridRowExp ?? (HybridRowExp) null, op.cond))
              {
                doc.state |= DocState.CondFalse;
                if (doc.docObjectID != 0L)
                {
                  ius.GetObject(Math.Abs(doc.docObjectID), false)?.Delete(0L);
                  doc.docObjectID = 0L;
                  continue;
                }
                continue;
              }
            }
            XmlDocument traceInfo = ti.traceInfo;
            XmlNode curNode = ti.curNode;
            ti.traceInfo = (XmlDocument) null;
            try
            {
              ti.InitTraceInfo();
              this.InnerSetParm(ti, new CalcAttrPair(-1L, ExpertConsts.Consts.attrEmptyDoc), (object) op.noEmpty);
              bool flag = false;
              try
              {
                int docType = -1;
                string docName = "";
                ti.docData = (ImDocumentData) null;
                ti.defRootNode = (DocumentTreeNode) null;
                ti.curDocNode = (DocumentTreeNode) null;
                switch (op.docType)
                {
                  case "Y":
                    DBReportScenario dbReportScenario = (DBReportScenario) ius.GetObject(doc.scriptID);
                    flag = !dbReportScenario.Execute((object) ius.SessionGUID, new long[1]
                    {
                      doc.objID
                    });
                    dbReportScenario.Document.Position = 0L;
                    ti.docData = ImDocumentData.LoadFromXml(dbReportScenario.Document);
                    IDBObject dbObject = ius.GetObject(doc.objID, false);
                    if (dbObject != null)
                      doc.docName = dbObject.Caption + hidden.prefix;
                    hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
                    if (dbReportScenario.DocumentAttributes != null)
                    {
                      using (Dictionary<Guid, string>.Enumerator enumerator = dbReportScenario.DocumentAttributes.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          KeyValuePair<Guid, string> current = enumerator.Current;
                          int attributeTypeId = MetaDataHelper.GetAttributeTypeID(current.Key);
                          if (attributeTypeId != 0 && (!string.IsNullOrEmpty(current.Value) || attributeTypeId != ExpertConsts.Consts._attrObjName && attributeTypeId != ExpertConsts.Consts._attrObjDesign))
                          {
                            if (hidden.sDocInfo.DocAttrs.ContainsKey(attributeTypeId))
                              hidden.sDocInfo.DocAttrs[attributeTypeId] = (object) current.Value;
                            else
                              hidden.sDocInfo.DocAttrs.Add(attributeTypeId, (object) current.Value);
                          }
                        }
                        break;
                      }
                    }
                    break;
                  default:
                    int document = (int) this._GenerateDocument(ti.taskId, doc.scriptID, new long[1]
                    {
                      doc.objID
                    }, out docType, out docName);
                    if (docName != "")
                      doc.docName = $"{ExpertServer.GetDocName(ius, ti, doc.objID, docName)} {doc.docName}";
                    hidden.docType = docType;
                    flag = Convert.ToBoolean(this.InnerGetParm(ti, ExpertConsts.Consts.attrEmptyDoc));
                    hidden.sDocInfo = new ExpertServer.SetDocumentInfo(ti);
                    break;
                }
                hidden.SetDontNumber(op.dontNumber, op.dontCount);
              }
              catch (Exception ex)
              {
                doc.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
                doc.state |= DocState.GenError;
              }
              if (flag && op.noEmpty)
              {
                doc.state |= DocState.Empty;
                if (doc.docObjectID != 0L)
                {
                  ius.GetObject(doc.docObjectID, false)?.Delete(0L);
                  if (doc.docObjectID < 0L)
                    ius.GetObject(-doc.docObjectID, false)?.Delete(0L);
                }
              }
              else
              {
                if (ti.docData != null)
                  hidden.zippedDoc = this.PackDocumentData(ti.docData);
                if (ti.traceInfo != null)
                  hidden.zippedInfo = this.PackXml(ti.traceInfo);
              }
              lock (ti)
                doc.state |= DocState.Ready;
            }
            finally
            {
              ti.traceInfo = traceInfo;
              ti.curNode = curNode;
            }
          }
        }
      }
    }
  }

  internal long GetRootComplect(ExpertServer.ExpServTask ti, ScriptTreeNode node)
  {
    long rootComplect = -1;
    do
    {
      node = node.parent;
    }
    while (node.parent != null && node.parent.parent != null);
    List<int> intList = (List<int>) null;
    for (int index = 0; index < ti.nodeItems.Count; ++index)
    {
      if (ti.nodeItems[index].node == node)
      {
        intList = ti.nodeItems[index].items;
        break;
      }
    }
    if (intList == null || intList.Count == 0)
      return rootComplect;
    DocRecord doc = ti.docList[intList[0]];
    return !doc.IsComplect() ? rootComplect : doc.docObjectID;
  }

  private HybridRowExp GetCoWorkerDoc(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    long objId,
    long scriptId)
  {
    HybridRowExp coWorkerSomething1 = this.GetCoWorkerSomething(ius, ti.idDocs, objId, false, scriptId);
    if (coWorkerSomething1 != null)
      return coWorkerSomething1;
    HybridRowExp coWorkerSomething2 = this.GetCoWorkerSomething(ius, ti.idComplects, objId, true);
    if (coWorkerSomething2 != null)
    {
      long int64 = Convert.ToInt64(coWorkerSomething2[0]);
      coWorkerSomething2 = this.GetCoWorkerSomething(ius, ti.idDocs, int64, false, scriptId);
    }
    return coWorkerSomething2;
  }

  private HybridRowExp GetCoWorkerSomething(
    IUserSession ius,
    int[] types,
    long objId,
    bool findComplect,
    long scriptId = 0)
  {
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
    {
      new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) types, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
      new ConditionStructure(ExpertConsts.Consts.attrCreatedByCoWorker, RelationalOperators.Equal, (object) true, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    };
    if (!findComplect)
    {
      ConditionStructure conditionStructure = conditionStructureList[conditionStructureList.Count - 1] with
      {
        LogicalOperator = LogicalOperators.AND
      };
      conditionStructureList[conditionStructureList.Count - 1] = conditionStructure;
      conditionStructureList.Add(new ConditionStructure(ExpertConsts.Consts.attrScriptRef, RelationalOperators.Equal, (object) scriptId, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text));
    }
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts._attrObjName, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1)
    };
    if (!findComplect)
      columnDescriptorList.Add(new ColumnDescriptor((object) ExpertConsts.Consts.attrLists, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1));
    DataTable childSostavData = DataHelper.GetChildSostavData(objId, ius, (IEnumerable<int>) new int[1]
    {
      ExpertConsts.Consts.linkSimpleSortId
    }, false, (IEnumerable<ConditionStructure>) conditionStructureList.ToArray(), (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray());
    return childSostavData == null || childSostavData.Rows.Count == 0 ? (HybridRowExp) null : new HybridTableExp(childSostavData.Rows[0])[0];
  }

  private DataTable GetChildDocs(IUserSession ius, long objID, int[] types)
  {
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) types, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[13]
    {
      new ColumnDescriptor((object) new Guid(ExpertAttrGUIDs.attrSorting), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrObjForDoc, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrScriptRef, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts._attrObjName, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrChecksum, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrCreatedByCoWorker, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrLists, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrDopCompTag, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrDocOperator, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrNumerationMode, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1)
    };
    return DataHelper.GetChildSostavData(objID, ius, (IEnumerable<int>) new int[1]
    {
      ExpertConsts.Consts.linkSimpleSortId
    }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
  }

  private DataTable GetChildComplects(IUserSession ius, long objID, int[] types)
  {
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) types, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[10]
    {
      new ColumnDescriptor((object) new Guid(ExpertAttrGUIDs.attrSorting), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrObjForDoc, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrObjCompRef, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts._attrObjName, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrCreatedByCoWorker, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrDopCompTag, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrDocOperator, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1)
    };
    return DataHelper.GetChildSostavData(objID, ius, (IEnumerable<int>) new int[1]
    {
      ExpertConsts.Consts.linkSimpleSortId
    }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
  }

  private void GetOldComplectData(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    long contextID,
    long complectID)
  {
    ExpertServer.OldComplectElem oldComplectElem = new ExpertServer.OldComplectElem();
    IDBObject dbObject = ius.GetObject(complectID);
    oldComplectElem.verId = dbObject.ObjectID;
    oldComplectElem.ID = dbObject.ID;
    IDBAttribute attributeById = dbObject.GetAttributeByID(ExpertConsts.Consts._attrObjDesign);
    oldComplectElem.Name = attributeById.AsString;
    oldComplectElem.complect = true;
    oldComplectElem.RootObjID = Math.Abs(contextID);
    TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(contextID, ius);
    if ((TypedInfoItem) objData != (TypedInfoItem) null)
      oldComplectElem.RootID = objData.Id;
    oldComplectElem.scriptID = Math.Abs(ti.compScriptId);
    ti.oldComplect.Add(oldComplectElem.verId, oldComplectElem);
    ti.oldIdents.GetOrAdd(new ExpertServer.OldKey(oldComplectElem.RootObjID, oldComplectElem.scriptID), new ExpertServer.IdentPair(oldComplectElem.verId, oldComplectElem.ID));
    this.GetOldComplectChilds(ti, ius, oldComplectElem.verId);
  }

  private void GetOldComplectChilds(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    long parentComplectID)
  {
    DataTable childDocs = this.GetChildDocs(ius, parentComplectID, ti.idDocs);
    if (childDocs != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) childDocs.Rows)
      {
        ExpertServer.OldComplectElem oldComplectElem = new ExpertServer.OldComplectElem();
        oldComplectElem.parentVerId = parentComplectID;
        oldComplectElem.verId = Convert.ToInt64(row[1]);
        oldComplectElem.ID = Convert.ToInt64(row[2]);
        oldComplectElem.RootObjID = row[3].NotDBNull() ? Math.Abs(Convert.ToInt64(row[3])) : 0L;
        oldComplectElem.scriptID = row[4].NotDBNull() ? Math.Abs(Convert.ToInt64(row[4])) : 0L;
        if (row[4].IsNullOrDBNull())
        {
          IDBObject dbObject = ius.GetObject(oldComplectElem.verId, false);
          if (dbObject != null)
          {
            IDBAttribute byId = dbObject.Attributes.FindByID(ExpertConsts.Consts.attrSourceLink);
            if (byId != null)
              oldComplectElem.scriptID = Convert.ToInt64(byId.Value);
          }
        }
        oldComplectElem.Name = Convert.ToString(row[5]);
        oldComplectElem.dopCompTag = Convert.ToString(row[9]);
        if (oldComplectElem.scriptID == 0L)
        {
          IDBObject dbObject = ius.GetObject(oldComplectElem.verId, false);
          if (dbObject != null)
          {
            object[] valuesById = dbObject.GetValuesByID(ExpertConsts.Consts.attrScenarioLink, false);
            if (valuesById != null && valuesById.Length != 0)
              oldComplectElem.scriptID = Convert.ToInt64(valuesById[0]);
          }
        }
        oldComplectElem.checkSum = row[6] == null || !row[6].NotDBNull() ? 0L : Convert.ToInt64(row[6]);
        if ((row[7].NotDBNull() ? (Convert.ToBoolean(row[7]) ? 1 : 0) : 0) != 0)
          oldComplectElem.needDelete = false;
        if (oldComplectElem.dopCompTag.Equals("") && ti.dopCompTags.Count > 0)
          oldComplectElem.needDelete = false;
        if (!oldComplectElem.dopCompTag.Equals("") && !ti.dopCompTags.Contains(oldComplectElem.dopCompTag))
          oldComplectElem.needDelete = false;
        if (row[9].NotNullOrDBNull())
          oldComplectElem.dopCompTag = Convert.ToString(row[9]);
        if (row[10].NotNullOrDBNull())
          oldComplectElem.operLabel = Convert.ToString(row[10]);
        if (row[11].NotNullOrDBNull())
          oldComplectElem.relationID = Math.Abs(Convert.ToInt64(row[11]));
        if (row[0].NotNullOrDBNull())
          oldComplectElem.SortOrder = Convert.ToInt64(row[0]);
        if (row[8].NotNullOrDBNull())
          oldComplectElem.ListCount = Convert.ToInt32(row[8]);
        if (row[12].NotNullOrDBNull())
          oldComplectElem.numType = (ExpertServer.NumberingType) Convert.ToInt32(row[12]);
        TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(oldComplectElem.RootObjID, ius);
        if ((TypedInfoItem) objData != (TypedInfoItem) null)
          oldComplectElem.RootID = objData.Id;
        if (!ti.oldComplect.ContainsKey(oldComplectElem.verId))
          ti.oldComplect.Add(oldComplectElem.verId, oldComplectElem);
        ExpertServer.OldKey key = new ExpertServer.OldKey(oldComplectElem.RootObjID, oldComplectElem.scriptID);
        if (!ti.oldIdents.ContainsKey(key))
          ti.oldIdents.GetOrAdd(key, new ExpertServer.IdentPair(oldComplectElem.verId, oldComplectElem.ID));
      }
    }
    DataTable childComplects = this.GetChildComplects(ius, parentComplectID, ti.idComplects);
    if (childComplects == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) childComplects.Rows)
    {
      ExpertServer.OldComplectElem oldComplectElem = new ExpertServer.OldComplectElem();
      oldComplectElem.parentVerId = parentComplectID;
      oldComplectElem.complect = true;
      oldComplectElem.verId = Convert.ToInt64(row[1]);
      oldComplectElem.ID = Convert.ToInt64(row[2]);
      oldComplectElem.RootObjID = Math.Abs(Convert.ToInt64(row[3]));
      oldComplectElem.scriptID = Math.Abs(Convert.ToInt64(row[4]));
      oldComplectElem.Name = Convert.ToString(row[5]);
      oldComplectElem.dopCompTag = Convert.ToString(row[7]);
      if (!ti.oldComplect.ContainsKey(oldComplectElem.verId))
        ti.oldComplect.Add(oldComplectElem.verId, oldComplectElem);
      bool flag = false;
      if (row[6].NotNullOrDBNull())
        flag = Convert.ToBoolean(row[6]);
      if (flag)
        oldComplectElem.needDelete = false;
      if (row[7].NotNullOrDBNull())
        oldComplectElem.dopCompTag = Convert.ToString(row[7]);
      if (row[8].NotNullOrDBNull())
        oldComplectElem.operLabel = Convert.ToString(row[8]);
      if (row[9].NotNullOrDBNull())
        oldComplectElem.relationID = Math.Abs(Convert.ToInt64(row[9]));
      if (row[0].NotNullOrDBNull())
        oldComplectElem.SortOrder = Convert.ToInt64(row[0]);
      if (oldComplectElem.dopCompTag.Equals("") && ti.dopCompTags.Count > 0)
        oldComplectElem.needDelete = false;
      if (!oldComplectElem.dopCompTag.Equals("") && !ti.dopCompTags.Contains(oldComplectElem.dopCompTag))
        oldComplectElem.needDelete = false;
      ExpertServer.OldKey key = new ExpertServer.OldKey(oldComplectElem.RootObjID, oldComplectElem.scriptID);
      if (ti.oldIdents.ContainsKey(key))
      {
        int num = -1;
        for (int index = ti.replacements.Count - 1; index >= 0; --index)
        {
          ExpertServer.OldKey replacement = ti.replacements[index];
          if (replacement.objectID == oldComplectElem.RootObjID && replacement.scriptID == oldComplectElem.scriptID)
          {
            num = index;
            break;
          }
        }
        if (num < 0)
        {
          ExpertServer.OldKey oldKey = new ExpertServer.OldKey(oldComplectElem.RootObjID, oldComplectElem.scriptID);
          ti.replacements.Add(oldKey);
          num = ti.replacements.Count - 1;
        }
        key.scriptID = (long) num;
        oldComplectElem.scriptID = key.scriptID;
      }
      TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(oldComplectElem.RootObjID, ius);
      if ((TypedInfoItem) objData != (TypedInfoItem) null)
        oldComplectElem.RootID = objData.Id;
      if (!ti.oldIdents.ContainsKey(key))
        ti.oldIdents.GetOrAdd(key, new ExpertServer.IdentPair(oldComplectElem.verId, oldComplectElem.ID));
      this.GetOldComplectChilds(ti, ius, oldComplectElem.verId);
    }
  }

  private void HackDocList(ExpertServer.ExpServTask ti)
  {
    List<ExpertServer.OldKey> oldKeyList = new List<ExpertServer.OldKey>();
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      DocRecord doc = ti.docList[index];
      if (doc.IsComplect())
      {
        ExpertServer.OldKey oldKey1 = new ExpertServer.OldKey(Math.Abs(doc.objID), Math.Abs(doc.scriptID));
        if (oldKeyList.Contains(oldKey1))
        {
          ExpertServer.OldKey oldKey2 = new ExpertServer.OldKey(Math.Abs(doc.objID), Math.Abs(doc.scriptID));
          oldKey1.scriptID = (long) ti.replacements.Count;
          doc.scriptID = oldKey1.scriptID;
          ti.replacements.Add(oldKey2);
        }
        oldKeyList.Add(oldKey1);
      }
    }
  }

  private void MarkInOtherComplects(ExpertServer.ExpServTask ti, IUserSession ius)
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objComplect);
    List<long> partIdList = new List<long>();
    foreach (long key in ti.oldComplect.Keys)
    {
      ExpertServer.OldComplectElem oldComplectElem = ti.oldComplect[key];
      partIdList.Add(key);
    }
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) childrenIdRecursive.ToArray(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts._attrObjName, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 1)
    };
    DataTable parentSostavData = DataHelper.GetParentSostavData((IEnumerable<long>) partIdList, ius, (IEnumerable<int>) new int[1]
    {
      ExpertConsts.Consts.linkSimpleSortId
    }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
    if (parentSostavData == null)
      return;
    foreach (long key in ti.oldComplect.Keys)
    {
      ExpertServer.OldComplectElem oldComplectElem = ti.oldComplect[key];
      TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(key, ius);
      int num = parentSostavData.Select("F_PART_ID = " + Convert.ToString(objData.Id)).Length > 1 ? 1 : 0;
      oldComplectElem.inOtherComplects = num != 0;
    }
  }

  private long GetPrevKTDVersion(IUserSession ius, long objId, long compScriptId)
  {
    string[] strArray = new string[4]
    {
      "F_OBJECT_ID",
      "F_OBJECT_TYPE",
      "F_ID",
      "F_VERSION_ID"
    };
    DataTable allObjectVersions = ius.GetAllObjectVersions(objId, false, false, false, strArray);
    if (allObjectVersions == null || allObjectVersions.Rows.Count == 0)
      return -1;
    DataRow[] dataRowArray = allObjectVersions.Select("", "F_VERSION_ID DESC");
    bool flag = false;
    foreach (DataRow dataRow in dataRowArray)
    {
      long int64 = Convert.ToInt64(dataRow["F_OBJECT_ID"]);
      if (int64 == objId)
        flag = true;
      if (flag)
      {
        long ktdFromRoot = this.GetKTDFromRoot(ius, int64, compScriptId);
        if (ktdFromRoot != -1L)
          return ktdFromRoot;
      }
    }
    return -1;
  }

  private long GetKTDFromRoot(IUserSession ius, long rootId, long compScriptId)
  {
    ConditionStructure[] conditions = new ConditionStructure[2]
    {
      new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.Equal, (object) ExpertConsts.Consts.objDocTPComplect, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
      new ConditionStructure(ExpertConsts.Consts.attrObjCompRef, RelationalOperators.Equal, (object) compScriptId, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.DESC, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 2)
    };
    DataTable childSostavData = DataHelper.GetChildSostavData(rootId, ius, (IEnumerable<int>) new int[1]
    {
      ExpertConsts.Consts.linkSimpleSortId
    }, false, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
    return childSostavData == null || childSostavData.Rows.Count == 0 ? -1L : Convert.ToInt64(childSostavData.Rows[0][0]);
  }

  private void FindPrevVersions(ExpertServer.ExpServTask ti)
  {
    Dictionary<ExpertServer.OldKey, long> dictionary = new Dictionary<ExpertServer.OldKey, long>();
    foreach (ExpertServer.OldComplectElem oldComplectElem in ti.oldComplect.Values)
    {
      ExpertServer.OldKey key = new ExpertServer.OldKey(Math.Abs(oldComplectElem.RootID), Math.Abs(oldComplectElem.scriptID));
      if (!dictionary.ContainsKey(key))
        dictionary.Add(key, oldComplectElem.verId);
    }
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      DocRecord doc = ti.docList[index];
      ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index];
      ExpertServer.OldKey key = new ExpertServer.OldKey(Math.Abs(hidden.ID), Math.Abs(doc.scriptID));
      if (dictionary.ContainsKey(key))
        hidden.prevVerId = dictionary[key];
    }
  }

  private void FillIndexesForOldIdents(ExpertServer.ExpServTask ti)
  {
    IUserSession session = ti.GetSession();
    Dictionary<(long, long), long> dictionary = new Dictionary<(long, long), long>();
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      DocRecord doc = ti.docList[index];
      (long, long) key = (ti.hiddenList[index].ID, doc.scriptID);
      if (!dictionary.ContainsKey(key))
        dictionary.Add(key, doc.objID);
    }
    foreach (ExpertServer.OldKey key1 in ti.oldIdents.Keys.ToList<ExpertServer.OldKey>())
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(key1.objectID);
      ExpertServer.IdentPair identPair;
      if (objectInfo.Empty)
        ti.oldIdents.TryRemove(key1, out identPair);
      (long, long) key2 = (objectInfo.ID, key1.scriptID);
      if (dictionary.ContainsKey(key2))
      {
        long num = dictionary[key2];
        ExpertServer.IdentPair oldIdent = ti.oldIdents[key1];
        ti.oldIdents.TryRemove(key1, out identPair);
        ti.oldIdents.TryAdd(new ExpertServer.OldKey(Math.Abs(num), Math.Abs(key1.scriptID)), oldIdent);
      }
    }
  }

  private void SetDocAttrs(
    ExpertServer.SetDocumentInfo sdi,
    IDBObject idbO,
    DocRecord dr,
    ExpertServer.HiddenDocInfo hdi)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts._attrObjName, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
    {
      (object) dr.docName
    }));
    if (!dr.IsDocLink())
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts._attrObjDesign, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
      {
        (object) dr.docName
      }));
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrShortDocDesign, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
    {
      (object) hdi.prefix
    }));
    if (sdi.CoWorkerDocs)
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrCreatedByCoWorker, FieldTypes.ftBoolean, MultiValueModes.SingleValue, new object[1]
      {
        (object) true
      }));
    AttributeValues[] array1 = attributeValuesList.ToArray();
    idbO.SetAttributesValues(array1);
    attributeValuesList.Clear();
    int attributeID = ExpertConsts.Consts.attrScriptRef;
    switch (dr.docType)
    {
      case "Y":
        attributeID = ExpertConsts.Consts.attrScenarioLink;
        break;
      case "N":
        attributeID = ExpertConsts.Consts.attrScriptRef;
        break;
      case "T":
        attributeID = ExpertConsts.Consts.attrLinkTechcardSetting;
        break;
    }
    if (!dr.IsDocLink())
      attributeValuesList.Add(new AttributeValues(attributeID, FieldTypes.ftObjectLink, MultiValueModes.SingleValue, new object[1]
      {
        (object) dr.scriptID
      }));
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrObjForDoc, FieldTypes.ftObjectLink, MultiValueModes.SingleValue, new object[1]
    {
      (object) dr.objID
    }));
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrOwnerLink, FieldTypes.ftObjectLink, MultiValueModes.SingleValue, new object[1]
    {
      (object) dr.objID
    }));
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrLists, FieldTypes.ftInteger, MultiValueModes.SingleValue, new object[1]
    {
      (object) hdi.pageCount
    }));
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrChecksum, FieldTypes.ftInteger, MultiValueModes.SingleValue, new object[1]
    {
      (object) hdi.checkSum
    }));
    if (hdi.dopCompTag != null)
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrDopCompTag, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
      {
        (object) hdi.dopCompTag
      }));
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrDocOperator, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
    {
      (object) hdi.TemplOperator
    }));
    attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrNumerationMode, FieldTypes.ftInteger, MultiValueModes.SingleValue, new object[1]
    {
      (object) (int) hdi.dontNumber
    }));
    if (sdi.NamedParms != null && sdi.NamedParms.ContainsKey("ArchiveID"))
    {
      long int64 = Convert.ToInt64(sdi.NamedParms["ArchiveID"]);
      if (int64 != 0L)
        attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrArchive, FieldTypes.ftObjectLink, MultiValueModes.SingleValue, new object[1]
        {
          (object) int64
        }));
    }
    try
    {
      AttributeValues[] array2 = attributeValuesList.ToArray();
      idbO.SetAttributesValues(array2);
    }
    catch
    {
    }
    IUserSession session = idbO.Session;
    IDBObjectType objectType = session.GetObjectType(idbO.ObjectType, false);
    if (objectType != null)
      this._SetDocAttributes(sdi, idbO, objectType);
    if (hdi.zippedDoc == null || hdi.zippedDoc.Length == 0)
      return;
    IDBAttribute dbAttribute = idbO.Attributes.AddAttribute(ExpertConsts.Consts.attrAttrFile, false);
    if ((dr.state & DocState.DocLink) != DocState.NoFlags)
    {
      dbAttribute.AddValue((object) null);
      dbAttribute.Index = 1;
    }
    if (!(dbAttribute is IBlobWriter blobWriter))
      return;
    byte[] zippedDoc = hdi.zippedDoc;
    string fileName = string.Empty;
    foreach (string str in dbAttribute.Values)
    {
      if (str.StartsWith("doc") && str.EndsWith(".imdx"))
      {
        fileName = str;
        break;
      }
    }
    if (fileName == string.Empty)
      fileName = this._ifns.GetUniqueFileName("doc.imdx", idbO.ID, session.SessionGUID);
    if ((dr.state & DocState.DocLink) != DocState.NoFlags)
    {
      int length = fileName.LastIndexOf('.');
      if (length > 0)
        fileName = fileName.Substring(0, length);
      fileName += ".imdx";
    }
    BlobInformation blobInfo1 = new BlobInformation((long) zippedDoc.Length, (long) zippedDoc.Length, DateTime.Now, fileName, ArcMethods.ZLibPacked, "");
    if (blobWriter.OpenBlob(blobInfo1, false))
      blobWriter.WriteDataBlock(zippedDoc);
    if ((dr.state & DocState.DocLink) == DocState.NoFlags)
      return;
    dbAttribute.Index = 0;
    if (!(dbAttribute is IBlobReader blobReader))
      return;
    BlobInformation blobInfo2 = blobReader.OpenBlob(-1);
    string lower = blobInfo2.FileName.ToLower();
    if (lower.EndsWith(".docx"))
      blobInfo2.FileName = lower.Replace(".docx", ".imdocx");
    else if (lower.EndsWith(".doc"))
      blobInfo2.FileName = lower.Replace(".doc", ".imdoc");
    (dbAttribute as IBlobWriter).OpenBlob(blobInfo2, true);
    if (blobReader.BlobState == BlobAttributeStates.Closed)
      return;
    blobReader.CloseBlob();
  }

  private void SetComplectAttrs(
    ExpertServer.ExpServTask ti,
    IDBObject idbO,
    DocRecord dr,
    ExpertServer.HiddenDocInfo hdi)
  {
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    long scriptId = dr.scriptID;
    if (Math.Abs(scriptId) < 1000L)
      scriptId = ti.replacements[(int) scriptId].scriptID;
    try
    {
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts._attrObjName, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
      {
        (object) dr.docName
      }));
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts._attrObjDesign, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
      {
        (object) dr.docName
      }));
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrObjCompRef, FieldTypes.ftObjectLink, MultiValueModes.SingleValue, new object[1]
      {
        (object) scriptId
      }));
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrObjForDoc, FieldTypes.ftInteger, MultiValueModes.SingleValue, new object[1]
      {
        (object) dr.objID
      }));
      if (hdi.dopCompTag != null)
        attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrDopCompTag, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
        {
          (object) hdi.dopCompTag
        }));
      attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrDocOperator, FieldTypes.ftString, MultiValueModes.SingleValue, new object[1]
      {
        (object) hdi.TemplOperator
      }));
      if (ti.coWorkerDocs)
        attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrCreatedByCoWorker, FieldTypes.ftBoolean, MultiValueModes.SingleValue, new object[1]
        {
          (object) true
        }));
      if (ti.namedParms.ContainsKey("ArchiveID"))
      {
        long int64 = Convert.ToInt64(ti.namedParms["ArchiveID"]);
        if (int64 != 0L)
          attributeValuesList.Add(new AttributeValues(ExpertConsts.Consts.attrArchive, FieldTypes.ftObjectLink, MultiValueModes.SingleValue, new object[1]
          {
            (object) int64
          }));
      }
      AttributeValues[] array = attributeValuesList.ToArray();
      idbO.SetAttributesValues(array);
    }
    catch
    {
    }
  }

  private void AddDocAttrs(ExpertServer.ExpServTask ti, List<AttributeValues> valList, int objType)
  {
    if (ti.docAttrs == null)
      return;
    IDBObjectType objectType = ti.GetSession().GetObjectType(objType, false);
    if (objectType == null)
      return;
    foreach (int docAttr in ti.docAttrs)
    {
      if (objectType.Attributes.GetAttributeByID(docAttr, false) == null)
        break;
      object parm = this.InnerGetParm(ti, docAttr);
      if (parm != null)
      {
        FieldTypes attributeType = FieldTypes.ftInteger;
        valList.Add(new AttributeValues(ExpertConsts.Consts.attrObjForDoc, attributeType, MultiValueModes.SingleValue, new object[1]
        {
          parm
        }));
      }
    }
  }

  private void SetAlignedDoc(
    ExpertServer.SetDocumentInfo sdi,
    Guid sessionGuid,
    DocRecord dr,
    ExpertServer.HiddenDocInfo hdi,
    int Num,
    bool aligned)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    IDBObjectCollection objectCollection = sessionById.GetObjectCollection(hdi.docType == -1 ? ExpertConsts.Consts.objDocTP : hdi.docType);
    bool flag1 = (dr.state & DocState.Delayed) != 0;
    aligned = aligned || (dr.state & DocState.Aligned) != 0;
    if (this.compTrace && aligned && sdi.MakeLog)
      this.iLH.AddToTrace($"Document aligned ({dr.docName}) user ({sessionById.UserName}) [{sessionById.ComputerName}]", Intermech.Consts.traceAlways, this.logFileName);
    switch (sdi.CompGenMode)
    {
      case GenMode.genModeGenerate:
        IDBObject idbO1;
        if (flag1 & aligned || dr.IsDocLink())
        {
          (sessionById as UserSession).DBObjectsCacheRemoveVersion(hdi.idbO_ID);
          idbO1 = sessionById.GetObject(hdi.idbO_ID);
          if (idbO1.ObjectModifyMode == ObjectModifyModes.Checkout)
            idbO1 = idbO1.CheckOut();
          hdi.idbO_ID = idbO1.ObjectID;
        }
        else
          idbO1 = objectCollection.Create();
        try
        {
          hdi.checkSum = hdi.zippedDoc != null ? ExpertServer.GetCheckSum(hdi.zippedDoc) : 0L;
          this.SetDocAttrs(sdi, idbO1, dr, hdi);
          hdi.idbO_ID = idbO1.ObjectID;
          dr.docObjectID = idbO1.ObjectID;
          if (sdi.DocListIndex != null && !sdi.DocListIndex.ContainsKey(idbO1.ID))
            sdi.DocListIndex.GetOrAdd(idbO1.ID, Num);
          hdi.ID = idbO1.ID;
          ExpertServer.OldKey key = new ExpertServer.OldKey(Math.Abs(dr.objID), Math.Abs(dr.scriptID));
          if (sdi.OldIdents != null && !sdi.OldIdents.ContainsKey(key))
            sdi.OldIdents.GetOrAdd(key, new ExpertServer.IdentPair(dr.docObjectID, idbO1.ID));
          if (aligned)
            dr.state |= DocState.Ready;
          if (idbO1.IsCreationMode)
            break;
          sdi.AddChangedDoc(dr.docObjectID, idbO1.ObjectType, DocOperType.Created);
          break;
        }
        catch (Exception ex)
        {
          dr.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
          dr.state |= DocState.AccessError;
          break;
        }
      case GenMode.genModeVersion:
      case GenMode.genModeRefresh:
        IDBObject idbO2 = (IDBObject) null;
        if (flag1 & aligned)
        {
          ((UserSession) sessionById).DBObjectsCacheRemoveVersion(hdi.idbO_ID);
          ((UserSession) sessionById).DBObjectsCacheRemoveVersion(-hdi.idbO_ID);
          idbO2 = sessionById.GetObjectActualCopy(hdi.idbO_ID, false);
        }
        long checkSum = hdi.zippedDoc != null ? ExpertServer.GetCheckSum(hdi.zippedDoc) : 0L;
        long num1 = 0;
        ExpertServer.OldKey key1 = new ExpertServer.OldKey(Math.Abs(dr.objID), Math.Abs(dr.scriptID));
        bool flag2 = sdi.OldIdents.ContainsKey(key1);
        long num2 = -1;
        bool flag3 = false;
        if (flag2)
        {
          num2 = sdi.OldIdents[key1].objId;
          hdi.prevVerId = num2;
          ExpertServer.OldComplectElem oldComplectElem = (ExpertServer.OldComplectElem) null;
          if (sdi.OldComplect.ContainsKey(num2))
            oldComplectElem = sdi.OldComplect[num2];
          if (oldComplectElem != null)
            num1 = oldComplectElem.checkSum;
          if (checkSum == num1 || sdi.CompGenMode == GenMode.genModeRefresh && oldComplectElem != null && !oldComplectElem.inOtherComplects)
          {
            if (idbO2 == null)
            {
              (sessionById as UserSession).DBObjectsCacheRemoveVersion(num2);
              idbO2 = sessionById.GetObject(num2, false);
              if (idbO2 == null && num2 < 0L)
                idbO2 = sessionById.GetObject(-num2, false);
              if (idbO2.ObjectModifyMode == ObjectModifyModes.Checkout)
                idbO2 = idbO2.CheckOut();
              sdi.AddChangedDoc(num2, idbO2.ObjectType, DocOperType.Changed);
              hdi.idbO_ID = idbO2.ObjectID;
            }
          }
          else if (sdi.CompGenMode == GenMode.genModeVersion && idbO2 == null)
          {
            idbO2 = objectCollection.CreateVersion(num2);
            hdi.idbO_ID = idbO2.ObjectID;
            flag3 = true;
            if (!idbO2.IsCreationMode)
              sdi.AddChangedDoc(idbO2.ObjectID, idbO2.ObjectType, DocOperType.Created);
          }
        }
        bool flag4 = true;
        if (idbO2 == null && sdi.CompGenMode == GenMode.genModeRefresh && hdi.prevVerId != 0L)
        {
          (sessionById as UserSession).DBObjectsCacheRemoveVersion(hdi.prevVerId);
          idbO2 = sessionById.GetObject(hdi.prevVerId, false);
          if (idbO2 != null && idbO2.ObjectModifyMode != ObjectModifyModes.CantModify)
          {
            if (idbO2.ObjectModifyMode == ObjectModifyModes.Checkout)
              idbO2 = idbO2.CheckOut();
            sdi.AddChangedDoc(idbO2.ObjectID, idbO2.ObjectType, DocOperType.Created);
            object[] valuesById = idbO2.GetValuesByID(sdi.AttrGroupChangeNum, false);
            if (valuesById != null && valuesById.Length != 0)
            {
              long int64 = Convert.ToInt64(valuesById[0]);
              if (sdi.RevisionId != 0L && int64 != sdi.RevisionId)
                flag4 = false;
            }
          }
        }
        hdi.checkSum = checkSum;
        if (idbO2 == null)
        {
          idbO2 = objectCollection.Create();
          if (!idbO2.IsCreationMode)
            sdi.AddChangedDoc(idbO2.ObjectID, idbO2.ObjectType, DocOperType.Created);
        }
        if (idbO2 != null)
        {
          bool flag5 = idbO2.ObjectModifyMode == ObjectModifyModes.InBase || idbO2.ObjectModifyMode == ObjectModifyModes.Checkout;
          if (checkSum != num1)
          {
            try
            {
              if (idbO2.ObjectModifyMode == ObjectModifyModes.Checkout)
              {
                long objectId = idbO2.ObjectID;
                (sessionById as UserSession).DBObjectsCacheRemoveVersion(objectId);
                idbO2 = sessionById.GetObject(objectId, true).CheckOut();
                this.CheckoutComplect(sdi.OldComplect, sessionById, num2);
              }
              if (flag5 & flag4)
                this.SetDocAttrs(sdi, idbO2, dr, hdi);
            }
            catch (Exception ex)
            {
              dr.errorMsg = ExceptionServices.GetExtendedExceptionText(ex);
              dr.state |= DocState.AccessError;
              throw;
            }
          }
          hdi.idbO_ID = idbO2.ObjectID;
          dr.docObjectID = idbO2.ObjectID;
          if (flag2)
          {
            if (flag3)
              sdi.OldIdents[key1].objId = idbO2.ObjectID;
          }
          else
          {
            sdi.OldIdents.GetOrAdd(new ExpertServer.OldKey(Math.Abs(dr.objID), Math.Abs(dr.scriptID)), new ExpertServer.IdentPair(dr.docObjectID, idbO2.ID));
            if (!idbO2.IsCreationMode)
              sdi.AddChangedDoc(dr.docObjectID, idbO2.ObjectType, DocOperType.Created);
          }
          if (sdi.CompGenMode == GenMode.genModeRefresh && hdi.prevVerId != 0L && sdi.OldComplect.ContainsKey(hdi.prevVerId))
            sdi.OldComplect[hdi.prevVerId].needDelete = false;
          if (!sdi.DocListIndex.ContainsKey(idbO2.ID))
            sdi.DocListIndex.GetOrAdd(idbO2.ID, Num);
          hdi.ID = idbO2.ID;
          if (flag5 & flag4)
          {
            IDBAttribute attributeById1 = idbO2.GetAttributeByID(ExpertConsts.Consts.attrLists);
            if (attributeById1 != null && attributeById1.AsInteger != (long) hdi.pageCount)
              attributeById1.AsInteger = (long) hdi.pageCount;
            if (dr.docType == "N")
            {
              IDBAttribute attributeById2 = idbO2.GetAttributeByID(ExpertConsts.Consts._attrObjName);
              if (attributeById2 != null && attributeById2.AsString != dr.docName)
                attributeById2.AsString = dr.docName;
              IDBAttribute attributeById3 = idbO2.GetAttributeByID(ExpertConsts.Consts._attrObjDesign);
              if (attributeById3 != null && attributeById3.AsString != dr.docName)
                attributeById3.AsString = dr.docName;
            }
            IDBAttribute attributeById4 = idbO2.GetAttributeByID(ExpertConsts.Consts.attrChecksum);
            if (attributeById4 != null && attributeById4.AsInteger != hdi.checkSum)
              attributeById4.AsInteger = hdi.checkSum;
            IDBAttribute dbAttribute = idbO2.GetAttributeByID(ExpertConsts.Consts.attrCreatedByCoWorker);
            if (sdi.CoWorkerDocs)
            {
              if (dbAttribute == null)
                dbAttribute = idbO2.Attributes.AddAttribute(ExpertConsts.Consts.attrCreatedByCoWorker, false);
              if (dbAttribute != null && !dbAttribute.AsBoolean)
                dbAttribute.AsBoolean = true;
            }
            else if (dbAttribute != null && dbAttribute.AsBoolean)
              dbAttribute.AsBoolean = false;
          }
        }
        if (!aligned)
          break;
        dr.state |= DocState.Ready;
        break;
    }
  }

  private bool CheckoutComplect(
    Dictionary<long, ExpertServer.OldComplectElem> oldComplect,
    IUserSession ius,
    long docObjId)
  {
    ExpertServer.OldComplectElem oldComplectElem = (ExpertServer.OldComplectElem) null;
    if (oldComplect.ContainsKey(docObjId))
      oldComplectElem = oldComplect[docObjId];
    for (; oldComplectElem != null; oldComplectElem = oldComplect[oldComplectElem.parentVerId])
    {
      if (oldComplectElem.complect)
      {
        (ius as UserSession).DBObjectsCacheRemoveVersion(oldComplectElem.verId);
        (ius as UserSession).ClearObjectSmartCache();
        lock (oldComplect)
        {
          IDBObject dbObject1 = ius.GetObject(oldComplectElem.verId, false);
          if (dbObject1 != null)
          {
            if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout)
            {
              if (dbObject1.CheckoutBy == 0L)
              {
                IDBObject dbObject2 = dbObject1.CheckOut();
                oldComplectElem.verId = dbObject2.ObjectID;
              }
              else if (ius.UserID != dbObject1.CheckoutBy)
                return false;
            }
          }
        }
      }
      if (!oldComplect.ContainsKey(oldComplectElem.parentVerId))
        break;
    }
    return true;
  }

  private List<RelObjInfoItem> SetLinks(ExpertServer.ExpServTask ti, IUserSession ius)
  {
    int num1 = 0;
    DataTable dt = (DataTable) null;
    List<long> longList = new List<long>();
    List<long> oldComplects = this._GetOldComplects(ti);
    int[] docsComplects = this._GetDocsComplects(ti);
    SortedList<long, List<int>> prepList = this._GetPrepList(ti);
    HashSet<long> realObjIDs = new HashSet<long>();
    foreach (DocRecord doc in ti.docList)
    {
      if (!realObjIDs.Contains(doc.objID))
        realObjIDs.Add(doc.objID);
    }
    List<RelObjInfoItem> relObjInfoItemList = this._SetObjectLinks(ti, ius, prepList, docsComplects, realObjIDs, longList, oldComplects);
    num1 = 0;
    IDBRelationCollection relationCollection = ius.GetRelationCollection(ExpertConsts.Consts.linkSimpleSortId);
    SortedList<long, List<ExpertServer.IdentPair>> sortedList = new SortedList<long, List<ExpertServer.IdentPair>>();
    for (int index = 0; index < ti.hiddenList.Count; ++index)
    {
      int parentIndex = ti.docList[index].parentIndex;
      if (parentIndex >= 0)
      {
        DocRecord doc = ti.docList[index];
        if ((doc.state & (DocState.Empty | DocState.GenError)) == DocState.NoFlags)
        {
          long docObjectId1 = ti.docList[parentIndex].docObjectID;
          long docObjectId2 = doc.docObjectID;
          long id = ti.hiddenList[index].ID;
          if (docObjectId2 != -1L)
          {
            if (sortedList.ContainsKey(docObjectId1))
              sortedList[docObjectId1].Add(new ExpertServer.IdentPair(docObjectId2, id, index));
            else
              sortedList.Add(docObjectId1, new List<ExpertServer.IdentPair>()
              {
                new ExpertServer.IdentPair(docObjectId2, id, index)
              });
          }
        }
      }
    }
    for (int index1 = 0; index1 < sortedList.Count; ++index1)
    {
      long key = sortedList.Keys[index1];
      List<ExpertServer.IdentPair> identPairList = sortedList[key];
      int num2 = 0;
      bool flag = this.IsObjectValid(ius, key);
      if (ti.CompGenMode != GenMode.genModeGenerate)
      {
        dt = ExpertServer.GetAllRelations(ius, ExpertConsts.Consts.linkSimpleSortId, key);
        if (dt != null)
          this.FillRelIds(dt, longList, ti.dopCompTags);
      }
      for (int index2 = 0; index2 < identPairList.Count; ++index2)
      {
        ExpertServer.IdentPair identPair = identPairList[index2];
        long objId = identPair.objId;
        long id = identPair.ID;
        IDBRelation idbRel = (IDBRelation) null;
        ExpertServer.HiddenDocInfo hidden = ti.hiddenList[identPair.Index];
        if (ti.CompGenMode == GenMode.genModeGenerate)
        {
          if (flag)
          {
            idbRel = relationCollection.Create(key, objId);
            ti.AddChangedRel(idbRel.RelationID, idbRel.RelationType, idbRel.ProjID, DocOperType.Created);
            hidden.RelationID = idbRel.RelationID;
          }
        }
        else
        {
          long rel = this.FindRel(dt, id);
          if (rel != 0L)
          {
            idbRel = ius.GetRelation(rel);
            longList.Remove(rel);
            hidden.RelationID = idbRel.RelationID;
          }
          else if (!ius.GetObjectInfo(objId).Empty & flag)
          {
            idbRel = ius.GetRelation(key, objId, ExpertConsts.Consts.linkSimpleSortId, true);
            if (idbRel == null)
            {
              idbRel = relationCollection.Create(key, objId);
              ti.AddChangedRel(idbRel.RelationID, idbRel.RelationType, idbRel.ProjID, DocOperType.Created);
            }
            else
              longList.Remove(idbRel.RelationID);
            hidden.RelationID = idbRel.RelationID;
          }
        }
        if (idbRel != null)
        {
          IDBAttribute dbAttribute = idbRel.Attributes.AddAttribute(ExpertConsts.Consts.attrSorting, false);
          if (dbAttribute != null)
            dbAttribute.AsInteger = (long) num2;
          if (ti.allowConcretization)
            this.AddConcretization(idbRel, objId);
        }
        num2 += 1000;
      }
      if (ti.CompGenMode != GenMode.genModeGenerate)
        this.DelRels(ius, relationCollection, dt, longList, ti, (IList<long>) oldComplects);
    }
    return relObjInfoItemList;
  }

  internal List<long> _GetOldComplects(ExpertServer.ExpServTask ti)
  {
    List<long> oldComplects = new List<long>();
    foreach (long key in ti.oldComplect.Keys)
    {
      if (ti.oldComplect[key].complect)
        oldComplects.Add(key);
    }
    return oldComplects;
  }

  internal int[] _GetDocsComplects(ExpertServer.ExpServTask ti)
  {
    int[] docsComplects = new int[ti.idComplects.Length + ti.idDocs.Length];
    for (int index = 0; index < ti.idDocs.Length; ++index)
      docsComplects[index] = ti.idDocs[index];
    for (int index = 0; index < ti.idComplects.Length; ++index)
      docsComplects[index + ti.idDocs.Length] = ti.idComplects[index];
    return docsComplects;
  }

  internal SortedList<long, List<int>> _GetPrepList(ExpertServer.ExpServTask ti)
  {
    SortedList<long, List<int>> prepList = new SortedList<long, List<int>>();
    foreach (KeyValuePair<ExpertServer.OldKey, ExpertServer.IdentPair> oldIdent in ti.oldIdents)
    {
      long objectId = oldIdent.Key.objectID;
      long id = oldIdent.Value.ID;
      if (ti.docListIndex.ContainsKey(id))
      {
        int num = ti.docListIndex[id];
        if (prepList.ContainsKey(objectId))
          prepList[objectId].Add(num);
        else
          prepList.Add(objectId, new List<int>() { num });
      }
    }
    return prepList;
  }

  internal List<RelObjInfoItem> _SetObjectLinks(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    SortedList<long, List<int>> prepList,
    int[] docs_comps,
    HashSet<long> realObjIDs,
    List<long> delRels,
    List<long> oldComplects)
  {
    IDBRelationCollection relationCollection = ius.GetRelationCollection(ExpertConsts.Consts.linkSimpleSortId);
    List<RelObjInfoItem> relObjInfoItemList = new List<RelObjInfoItem>();
    for (int index1 = 0; index1 < prepList.Count; ++index1)
    {
      long num1 = prepList.Keys[index1];
      if (realObjIDs.Contains(-num1))
        num1 = -num1;
      bool flag = this.IsObjectValid(ius, num1);
      List<int> intList = prepList.Values[index1];
      int num2 = 0;
      DataTable allRelationsByType = ExpertServer.GetAllRelationsByType(ius, ExpertConsts.Consts.linkSimpleSortId, num1, docs_comps);
      if (allRelationsByType != null && ti.CompGenMode != GenMode.genModeGenerate)
        this.FillRelIds(allRelationsByType, delRels, ti.dopCompTags);
      for (int index2 = 0; index2 < intList.Count; ++index2)
      {
        int index3 = intList[index2];
        DocRecord doc = ti.docList[index3];
        ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index3];
        if ((doc.state & (DocState.Empty | DocState.GenError)) == DocState.NoFlags)
        {
          long docObjectId = doc.docObjectID;
          long id = ti.hiddenList[index3].ID;
          int parentIndex = ti.docList[index3].parentIndex;
          if (!intList.Contains(parentIndex))
          {
            IDBRelation dbRelation1 = (IDBRelation) null;
            long rel = this.FindRel(allRelationsByType, id);
            if (ti.CompGenMode == GenMode.genModeGenerate)
            {
              if (flag)
              {
                if (rel == 0L)
                {
                  dbRelation1 = relationCollection.Create(num1, docObjectId);
                  ti.AddChangedRel(dbRelation1.RelationID, dbRelation1.RelationType, dbRelation1.ProjID, DocOperType.Created);
                  hidden.RelationID = dbRelation1.RelationID;
                }
                else
                  hidden.RelationID = rel;
              }
            }
            else if (rel != 0L)
            {
              dbRelation1 = ius.GetRelation(rel);
              delRels.Remove(rel);
              hidden.RelationID = rel;
            }
            else if (flag)
            {
              dbRelation1 = relationCollection.Create(num1, docObjectId);
              ti.AddChangedRel(dbRelation1.RelationID, dbRelation1.RelationType, dbRelation1.ProjID, DocOperType.Created);
              hidden.RelationID = dbRelation1.RelationID;
            }
            if (dbRelation1 != null)
            {
              if (ti.allowConcretization)
                this.AddConcretization(dbRelation1, docObjectId);
              RelObjInfoItem relObjInfoItem = new RelObjInfoItem(dbRelation1);
              if (dbRelation1 is DBRelation dbRelation2)
              {
                relObjInfoItem.ProjInfo = new ObjInfoItem(dbRelation2.ProjObject);
                relObjInfoItem.PartInfo = new ObjInfoItem(dbRelation2.PartObject);
              }
              relObjInfoItemList.Add(relObjInfoItem);
            }
            num2 += 1000;
          }
        }
      }
      if (ti.CompGenMode != GenMode.genModeGenerate && num1 != ti.contextID)
        this.DelRels(ius, relationCollection, allRelationsByType, delRels, ti, (IList<long>) oldComplects);
    }
    return relObjInfoItemList;
  }

  private bool IsObjectValid(IUserSession ius, long objId)
  {
    if (objId == 0L || objId == -1L)
      return false;
    IDBObject dbObject = ius.GetObject(objId, false);
    return dbObject != null && (dbObject as IDBLifecycleLevel).LevelID != ius.IdentHelper.DeletedID;
  }

  private void SortRelations(IUserSession ius, List<RelObjInfoItem> relationItems)
  {
    ICompositionsAutomaticSortingService service = (ICompositionsAutomaticSortingService) this._serviceProvider.GetService(typeof (ICompositionsAutomaticSortingService));
    ICompositionsAutomaticSortingSession session = service.CreateSession((object) ius);
    try
    {
      List<ObjInfoItem> list = relationItems.Where<RelObjInfoItem>((System.Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.ProjInfo != (TypedInfoItem) null)).Select<RelObjInfoItem, ObjInfoItem>((System.Func<RelObjInfoItem, ObjInfoItem>) (item => item.ProjInfo)).ToList<ObjInfoItem>();
      session.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) list, (object) ius);
      foreach (RelObjInfoItem relationItem in relationItems)
      {
        CompositionSortingProjInfo relationInfo = new CompositionSortingProjInfo(relationItem);
        session.ProceedRelation(relationInfo, (object) ius);
      }
    }
    finally
    {
      service.DisposeSession((object) ius);
    }
  }

  private List<ExpertServer.SortedItem> SortRelationsNew(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode root)
  {
    ExpertServer.TemplateIndexer templateIndexer = new ExpertServer.TemplateIndexer(root, ius);
    SortedList<ExpertServer.SortedItem, bool> sortedList = new SortedList<ExpertServer.SortedItem, bool>();
    HashSet<long> longSet = new HashSet<long>();
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      DocRecord doc = ti.docList[index];
      ExpertServer.HiddenDocInfo hidden = ti.hiddenList[index];
      long parentObjId = doc.parentIndex >= 0 ? ti.docList[doc.parentIndex].docObjectID : -1L;
      ExpertServer.SortedItem key = templateIndexer.ProcessNewElem(doc, hidden, parentObjId);
      if (key != null)
      {
        sortedList.Add(key, true);
        longSet.Add(Math.Abs(doc.docObjectID));
      }
    }
    List<ExpertServer.OldComplectElem> list1 = ti.oldComplect.Values.ToList<ExpertServer.OldComplectElem>();
    for (int index = 0; index < list1.Count; ++index)
    {
      ExpertServer.OldComplectElem oce = list1[index];
      if (!longSet.Contains(Math.Abs(oce.verId)))
      {
        ExpertServer.SortedItem key = templateIndexer.ProcessOldComplectElem(oce);
        if (key != null)
        {
          sortedList.Add(key, false);
          longSet.Add(Math.Abs(oce.verId));
        }
      }
    }
    foreach (ExpertServer.SortedItem key in (IEnumerable<ExpertServer.SortedItem>) sortedList.Keys)
    {
      if (key.RelationId != 0L && ius.GetRelation(key.RelationId, false) == null && ius.GetRelation(-key.RelationId, false) != null)
        key.RelationId = -key.RelationId;
    }
    Dictionary<long, int> dictionary = new Dictionary<long, int>();
    for (int index = 0; index < sortedList.Keys.Count; ++index)
    {
      ExpertServer.SortedItem key1 = sortedList.Keys[index];
      if (key1.DocCompId != -1L)
      {
        long key2 = Math.Abs(key1.DocCompId);
        if (!dictionary.ContainsKey(key2))
          dictionary.Add(key2, index);
      }
    }
    foreach (ExpertServer.SortedItem key3 in (IEnumerable<ExpertServer.SortedItem>) sortedList.Keys)
    {
      if (key3.ParentObjId != 0L && key3.ParentObjId != 0L)
      {
        long key4 = Math.Abs(key3.ParentObjId);
        if (dictionary.ContainsKey(key4))
          key3.ParentIndex = dictionary[key4];
      }
    }
    List<long> list2 = sortedList.Keys.Select<ExpertServer.SortedItem, long>((System.Func<ExpertServer.SortedItem, long>) (sortItem => sortItem.RelationId)).ToList<long>();
    if (list2.Count > 0)
      list2.RemoveAt(0);
    this.DoSortRelations(ius, list2);
    return sortedList.Keys.ToList<ExpertServer.SortedItem>();
  }

  private void DoSortRelations(IUserSession ius, List<long> allLinks)
  {
    long num1 = 1000000;
    long num2 = 1000000;
    foreach (long allLink in allLinks)
    {
      IDBRelation relation = ius.GetRelation(allLink, false);
      if (relation != null)
      {
        IDBAttribute dbAttribute = relation.Attributes.AddAttribute(ExpertConsts.Consts.attrSorting, false);
        if (dbAttribute != null)
        {
          dbAttribute.AsInteger = num1;
          num1 += num2;
        }
      }
    }
  }

  private long FindRel(DataTable dt, long ID)
  {
    if (dt == null)
      return 0;
    string filterExpression = $"[{dt.Columns[2].ColumnName}] = {Convert.ToString(ID)}";
    DataRow[] dataRowArray = dt.Select(filterExpression);
    return dataRowArray.Length != 0 ? Convert.ToInt64(dataRowArray[0][0]) : 0L;
  }

  private void FillRelIds(DataTable dt, List<long> relIds, List<string> dopCompTags)
  {
    relIds.Clear();
    bool flag = dopCompTags != null && dopCompTags.Count > 0;
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      string str = "";
      object obj = row[3];
      if (obj != null && obj != DBNull.Value)
        str = Convert.ToString(obj);
      if (flag)
      {
        if (str == "" || !dopCompTags.Contains(str))
          continue;
      }
      else if (str != "")
        continue;
      relIds.Add(Convert.ToInt64(row[0]));
    }
  }

  private void SetRelListNums(IUserSession ius, ExpertServer.ExpServTask ti, int totalNum)
  {
    for (int index = 0; index < ti.docList.Count; ++index)
    {
      DocRecord doc1 = ti.docList[index];
      if ((doc1.state & DocState.Complect) == DocState.NoFlags && (doc1.state & DocState.Ready) != DocState.NoFlags)
      {
        ExpertServer.HiddenDocInfo hidden1 = ti.hiddenList[index];
        if (hidden1.idbO_ID != 0L && doc1.parentIndex >= 0)
        {
          DocRecord doc2 = ti.docList[doc1.parentIndex];
          ExpertServer.HiddenDocInfo hidden2 = ti.hiddenList[doc1.parentIndex];
          IDBRelation relation = ius.GetRelation(doc2.docObjectID, hidden1.ID, ExpertConsts.Consts.linkSimpleSortId);
          if (relation != null)
          {
            int firstListNum = hidden1.firstListNum;
            IDBAttribute dbAttribute1 = relation.Attributes.AddAttribute(ExpertConsts.Consts.attrCompListNum, false);
            if (dbAttribute1 != null)
              dbAttribute1.AsInteger = (long) totalNum;
            IDBAttribute dbAttribute2 = relation.Attributes.AddAttribute(ExpertConsts.Consts.attrListsBefore, false);
            if (dbAttribute2 != null)
              dbAttribute2.AsInteger = (long) firstListNum;
          }
        }
      }
    }
  }

  private void SetRelListNums2(
    IUserSession ius,
    List<ExpertServer.SortedItem> sortList,
    int totalNum)
  {
    for (int index = 0; index < sortList.Count; ++index)
    {
      ExpertServer.SortedItem sort1 = sortList[index];
      if (!sort1.IsComplect && sort1.DocCompId != 0L && sort1.ParentIndex >= 0)
      {
        ExpertServer.SortedItem sort2 = sortList[sort1.ParentIndex];
        IDBRelation relation = ius.GetRelation(sort1.RelationId, false);
        if (relation != null)
        {
          int listsBefore = sort1.ListsBefore;
          IDBAttribute dbAttribute1 = relation.Attributes.AddAttribute(ExpertConsts.Consts.attrCompListNum, false);
          if (dbAttribute1 != null)
            dbAttribute1.AsInteger = (long) totalNum;
          IDBAttribute dbAttribute2 = relation.Attributes.AddAttribute(ExpertConsts.Consts.attrListsBefore, false);
          if (dbAttribute2 != null)
            dbAttribute2.AsInteger = (long) listsBefore;
        }
      }
    }
  }

  private void DelRels(
    IUserSession ius,
    IDBRelationCollection idbRC,
    DataTable dt,
    List<long> delRels,
    ExpertServer.ExpServTask ti,
    IList<long> parents)
  {
    if (dt == null || delRels.Count <= 0)
      return;
    for (int index = delRels.Count - 1; index >= 0; --index)
    {
      if (!this.CheckRelObject(ti, delRels[index], dt, parents))
        delRels.RemoveAt(index);
    }
    if (delRels.Count <= 0)
      return;
    foreach (long delRel in delRels)
      ti.AddChangedRel(delRel, -1, 0L, DocOperType.Deleted);
    for (int index = delRels.Count - 1; index >= 0; --index)
    {
      long delRel = delRels[index];
      IDBRelation relation = ius.GetRelation(delRel);
      if (relation != null && !((DBSessionable) relation).Deleted)
      {
        IDBObject objectById = ius.GetObjectByID(relation.PartID, false);
        if (objectById != null && !((DBSessionable) objectById).Deleted && objectById.ObjectType == ExpertConsts.Consts.objDocTPComplect)
        {
          IDBAttribute attributeById = objectById.GetAttributeByID(ExpertConsts.Consts.attrObjCompRef);
          if (attributeById != null)
          {
            long int64 = Convert.ToInt64(attributeById.Value);
            if (Math.Abs(ti.compScriptId) == int64)
            {
              DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
              {
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID)
              });
              idbRC.LocalTypesMode = true;
              DataTable dataTable = idbRC.ConsistFrom(paramSet, objectById.ObjectID);
              List<long> longList = new List<long>();
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
                longList.Add(Convert.ToInt64(row[0]));
              if (longList.Count > 0)
                idbRC.Delete(longList.ToArray(), false, 0L);
              objectById.Delete(0L);
              delRels.RemoveAt(index);
            }
          }
        }
      }
    }
    if (delRels.Count <= 0)
      return;
    idbRC.Delete(delRels.ToArray(), false, 0L);
  }

  private bool CheckRelObject(
    ExpertServer.ExpServTask ti,
    long relId,
    DataTable dt,
    IList<long> parents)
  {
    long objectID = 0;
    for (int index = 0; index < dt.Rows.Count; ++index)
    {
      if (Convert.ToInt64(dt.Rows[index][0]) == relId)
      {
        objectID = Convert.ToInt64(dt.Rows[index][1]);
        break;
      }
    }
    if (objectID == 0L)
      return true;
    DataTable dataTable = this.GetSession(ti).GetRelationCollection(ExpertConsts.Consts.linkSimpleSortId).EntersInVersion(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) ExpertServer.es.objTypesTPDocComplect, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    }), objectID);
    if (dataTable.Rows.Count == 0)
      return false;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (!parents.Contains(int64) && !parents.Contains(-int64))
        return false;
    }
    return true;
  }

  private void AddConcretization(IDBRelation idbRel, long docVerId)
  {
    IDBAttribute dbAttribute = idbRel.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
    if (dbAttribute == null)
    {
      dbAttribute = idbRel.Attributes.AddAttribute(ExpertConsts.Consts.attrVerSostav, false);
      if (dbAttribute == null)
        return;
    }
    if (!dbAttribute.Value.IsNullOrDBNull() && dbAttribute.AsInteger == Math.Abs(docVerId))
      return;
    dbAttribute.AsInteger = Math.Abs(docVerId);
  }

  private static bool IsAttributeAllowedForRelType(int relType, int attrType)
  {
    return MetaDataHelper.GetRelationType(relType).AnyAttributes || MetaDataHelper.GetAttribute4RelationType(relType, attrType) != null;
  }

  private void ReportDocList(ExpertServer.ExpServTask ti, string head)
  {
    if (!ti.makeLog)
      return;
    this.iLH.AddToTrace(head, this.logFileName);
    for (int index = 0; index < ti.docList.Count; ++index)
      this.iLH.AddToTrace($"[{index.ToString()}]: {ti.docList[index].ToString()}", this.logFileName);
  }

  private ExpertResult _RunCommandScript(int taskId, long docScriptID, long[] context)
  {
    bool flag = false;
    string Text = "";
    string EventStr1 = "";
    string str = "";
    string EventStr2 = "";
    ExpertResult expertResult = ExpertResult.OK;
    this.StartJobForTask(taskId);
    try
    {
      ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
      CommandScript commandScript = (CommandScript) null;
      ExpertTraceFlags traceFlags = ti.traceFlags;
      try
      {
        commandScript = (CommandScript) this.GetSession(ti).GetObjectActualCopy(docScriptID, false);
        string caption = commandScript.Caption;
        XmlNode xmlNode = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_300"));
        if (xmlNode != null)
        {
          ti.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_225"), caption);
          ti.traceSetNode(xmlNode);
        }
      }
      finally
      {
        this.EndModifyTrace(ti);
      }
      ti.docScriptId = docScriptID;
      ScriptTreeNode root;
      if (ti.cacheScripts != null && ti.cacheScripts.ContainsKey(docScriptID))
      {
        root = ti.cacheScripts[docScriptID].Item2;
      }
      else
      {
        if (this.FlagIn(ExpertTraceFlags.ShowContext, traceFlags))
          this.ShowContext(taskId, context, false);
        if (this.FlagIn(ExpertTraceFlags.ShowExpertObjects, traceFlags))
          this.ShowLoadObject(taskId, (ExpertObject) commandScript);
        else
          commandScript.Load();
        try
        {
          root = XMLScripter.LoadScript(commandScript.Script, out ExpertScriptParms _);
        }
        catch (Exception ex)
        {
          throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_23")}", ex);
        }
        if (ti.cacheScripts != null)
          ti.cacheScripts.GetOrAdd(docScriptID, new Tuple<ExpertServer.GenInfo, ScriptTreeNode>(ExpertServer.GenInfo.Empty, root));
      }
      if (ti.makeTrace)
        ti.rootExclaimed = this.PlaceExclamations(root);
      lock (ti)
      {
        ti.curScrType = ExpertScriptType.CommandScript;
        ti.scriptRoot = root;
      }
      this._SetParmValue(taskId, -1L, ExpertConsts.Consts.attrContextCount, (object) context.Length, false);
      if (ti.CompGenMode == GenMode.genModeNone)
      {
        ti.OptInitCollectObjectData();
        ti.OptCollectScriptAttrTypes();
      }
      ti.BreakFlag = false;
      long[] new_context = (long[]) null;
      for (int index = 0; index < root.Items.Count; ++index)
      {
        this.ProcessScriptNode(taskId, (ScriptTreeNode) root.Items[index], context, (HybridTableExp) null, false, ref new_context);
        if (!ti.BreakFlag)
        {
          if (new_context != null)
          {
            context = (long[]) new_context.Clone();
            ti.context.Clear();
            foreach (long num in context)
              ti.context.Add(num);
          }
        }
        else
          break;
      }
      return ExpertResult.OK;
    }
    catch (Exception ex)
    {
      flag = true;
      Text = ex.Message;
      EventStr1 = ex.StackTrace;
      if (ex.InnerException != null)
      {
        str = ex.InnerException.Message;
        EventStr2 = ex.InnerException.StackTrace;
      }
      if (ex.GetType() != typeof (EAbort))
      {
        this.LogException(taskId, ex);
        throw;
      }
      EnumTypeHelper.GetCaption((Enum) (ex as EAbort).res);
      switch ((ex as EAbort).res)
      {
        case ExpertResult.NoCondParms:
        case ExpertResult.NoCalcParms:
        case ExpertResult.RuleNotFound:
        case ExpertResult.CircularReference:
          ExpertServer.ExpServTask task = this.taskList[taskId];
          throw new ExpertServerException(ex.Message + LocalizationHolder.rm.GetString("Expert.Server_149") + this.GetNeedParmList(task));
        default:
          return ExpertResult.Aborted;
      }
    }
    finally
    {
      if (!this.abortedTasksContains(taskId))
      {
        ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
        try
        {
          XmlNode curNode = ti.curNode;
          if (flag)
          {
            XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_59"));
            if (node != null)
              ti.traceAddText(node, Text);
            if (ti.makeLog)
            {
              this.iLH.AddToTrace($"Exception - \"{Text}\"", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace("----------  Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace(EventStr1, Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace($"InnerException - \"{str}\"", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace("----------  Inner Stack trace  -------------", Intermech.Consts.traceAlways, this.logFileName);
              this.iLH.AddToTrace(EventStr2, Intermech.Consts.traceAlways, this.logFileName);
            }
          }
          else
          {
            XmlNode element = (XmlNode) ti.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_99"), ExpertServer.ExpertNamespace);
            XmlAttribute attribute = ti.traceInfo.CreateAttribute(LocalizationHolder.rm.GetString("Expert.Server_58"));
            attribute.Value = Convert.ToString((object) expertResult);
            element.Attributes.Append(attribute);
            ti.curNode.AppendChild(element);
          }
          ti.traceSetNode(curNode);
        }
        finally
        {
          this.EndModifyTrace(ti);
        }
        this.EndJobForTask(taskId);
      }
    }
  }

  private static void LoadNodeFromXML(XmlNode xmlRoot, ScriptTreeNode rootNode)
  {
    string str = "";
    int modTag = -1;
    int opTag = -1;
    if (xmlRoot.Attributes != null)
    {
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlRoot.Attributes)
      {
        if (attribute.Name == "label")
          str = attribute.Value;
        else if (attribute.Name == "modTag")
          modTag = Convert.ToInt32(attribute.Value);
        else if (attribute.Name == "opTag")
          opTag = Convert.ToInt32(attribute.Value);
      }
    }
    ScriptTreeNode rootNode1;
    switch (opTag)
    {
      case 53:
        rootNode1 = (ScriptTreeNode) new GlobalNode();
        break;
      case 54:
        rootNode1 = (ScriptTreeNode) new GlobalTypeNode();
        break;
      default:
        rootNode1 = new ScriptTreeNode();
        break;
    }
    rootNode1.LoadXML(xmlRoot, modTag, opTag);
    rootNode1.label = str;
    rootNode.Items.Add((object) rootNode1);
    rootNode1.parent = rootNode;
    if (!xmlRoot.HasChildNodes)
      return;
    foreach (XmlNode childNode in xmlRoot.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "node")
        ExpertServer.LoadNodeFromXML(childNode, rootNode1);
    }
  }

  public static ScriptTreeNode LoadScriptTree(XmlDocument xDoc)
  {
    ScriptTreeNode rootNode = new ScriptTreeNode();
    XmlElement documentElement = xDoc.DocumentElement;
    if (documentElement.HasChildNodes)
    {
      foreach (XmlNode childNode1 in documentElement.ChildNodes)
      {
        if (childNode1.NodeType != XmlNodeType.Element || !(childNode1.Name == "DocParms"))
        {
          if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ExpScript")
          {
            foreach (XmlNode childNode2 in childNode1.ChildNodes)
              ExpertServer.LoadNodeFromXML(childNode2, rootNode);
          }
          else
            ExpertServer.LoadNodeFromXML(childNode1, rootNode);
        }
      }
    }
    return rootNode;
  }

  private ScriptTreeNode LoadScriptTree(XmlDocument xDoc, out ExpertServer.GenInfo si)
  {
    ScriptTreeNode rootNode = new ScriptTreeNode();
    si = new ExpertServer.GenInfo();
    XmlElement documentElement = xDoc.DocumentElement;
    if (documentElement.HasChildNodes)
    {
      foreach (XmlNode childNode1 in documentElement.ChildNodes)
      {
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "DocParms")
          si.LoadXml(childNode1);
        else if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "ExpScript")
        {
          foreach (XmlNode childNode2 in childNode1.ChildNodes)
            ExpertServer.LoadNodeFromXML(childNode2, rootNode);
        }
        else
          ExpertServer.LoadNodeFromXML(childNode1, rootNode);
      }
    }
    return rootNode;
  }

  private void _CheckScriptNode(int taskId, ScriptTreeNode node)
  {
    string str = "";
    if (node.opTag == ExpertScriptOp.opUnknown)
      str = LocalizationHolder.rm.GetString("Expert.Server_152");
    if (this.GetTask(taskId).curScrType != ExpertScriptType.DocScript && (node.opTag == ExpertScriptOp.opDocFillText || node.opTag == ExpertScriptOp.opDocNewElem || node.opTag == ExpertScriptOp.opDocSelectElem))
      str = LocalizationHolder.rm.GetString("Expert.Server_153");
    if (str != "")
    {
      this.ReportError(taskId, str);
      throw new ExpertServerException(str);
    }
  }

  private bool PlaceExclamations(ScriptTreeNode root)
  {
    bool flag = root.HasExclamation();
    foreach (ScriptTreeNode root1 in root.Items)
      flag = this.PlaceExclamations(root1) | flag;
    root.ExclamationMarked = flag;
    return flag;
  }

  private void ProcessScriptNode(
    int taskId,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable,
    bool cont_changed,
    ref long[] new_context)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (this.IsJobAborting(task) || this.IsTaskClientDead(task))
      return;
    DocumentTreeNode curDocNode = task.curDocNode;
    IUserSession session = this.GetSession(task);
    List<long> longList1 = new List<long>((IEnumerable<long>) context);
    if (task.makeLog)
      this.iLH.AddToTrace($"-> Performing node \"{node.label}\"", Intermech.Consts.traceAlways, this.logFileName);
    List<long> cyclingObjects = task.FindCyclingObjects(node, longList1);
    if (cyclingObjects.Count > 0)
    {
      XmlNode node1 = task.makeTrace ? task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_244")) : (XmlNode) null;
      if (node1 != null)
      {
        task.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_246"), LocalizationHolder.rm.GetString("Expert.Server_247"));
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < cyclingObjects.Count; ++index)
        {
          stringBuilder.Append(Convert.ToString(cyclingObjects[index]));
          if (index < cyclingObjects.Count - 1)
            stringBuilder.Append(", ");
        }
        task.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_245"), stringBuilder.ToString());
      }
      if (context.Length == cyclingObjects.Count)
        return;
      foreach (long num in cyclingObjects)
        longList1.Remove(num);
      context = longList1.ToArray();
    }
    if (!task.Push(node, longList1))
    {
      XmlNode node2 = task.makeTrace ? task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_244")) : (XmlNode) null;
      if (node2 == null)
        return;
      task.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_38"), "1000");
    }
    else
    {
      XmlNode curNode = (XmlNode) null;
      this.ReportScriptNode(taskId, node, context, cont_changed, out curNode);
      int num1 = 0;
      object parm1 = this.InnerGetParm(task, ExpertConsts.Consts.attrContextCount);
      long num2 = -1;
      if (parm1 != null)
        num1 = (int) parm1;
      object parm2 = this.InnerGetParm(task, ExpertConsts.Consts.attrCurContextId);
      if (parm2 != null)
        num2 = (long) parm2;
      int num3 = 0;
      object parm3 = this.InnerGetParm(task, ExpertConsts.Consts.attrCurContextNum);
      if (parm3 != null)
        num3 = Convert.ToInt32(parm3);
      bool flag1 = false;
      try
      {
        this._CheckScriptNode(taskId, node);
        if (node.label.Length > 0 && node.label[0] == '#')
        {
          if (!task.makeTrace)
            return;
          task.traceAddText(task.curNode, LocalizationHolder.rm.GetString("Expert.Server_154"));
        }
        else
        {
          switch (node.opTag)
          {
            case ExpertScriptOp.opObjParents:
            case ExpertScriptOp.opObjChildren:
            case ExpertScriptOp.opObjSiblings:
            case ExpertScriptOp.opObjLinked:
            case ExpertScriptOp.opObjAncestors:
            case ExpertScriptOp.opObjDescendants:
              OpParmObject op1 = (OpParmObject) node.op;
              ModParm mod1 = node.mod;
              HybridTableExp dt = (HybridTableExp) null;
              object MultiRes = (object) null;
              if (op1.objTypeForGlobalGUID != "" && context.Length != 0)
              {
                int objectTypeId = MetaDataHelper.GetObjectTypeID(op1.objTypeForGlobalGUID);
                List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeId);
                long num4 = context[0];
                int itemTypeId = task.DataCache.GetObjData(num4, session).ItemTypeID;
                if (!childrenIdRecursive.Contains(itemTypeId))
                {
                  IDBAttributable objectWithAttr = this.FindObjectWithAttr(taskId, session, num4, -1, objectTypeId);
                  if (objectWithAttr != null)
                    context = new long[1]
                    {
                      ExpertServer.AttributableId(objectWithAttr)
                    };
                }
              }
              List<long> objects = node.opTag == ExpertScriptOp.opObjAncestors || node.opTag == ExpertScriptOp.opObjDescendants ? (op1.excerptID == 0L ? this.ExecuteObjSelectMulti(taskId, context, node.opTag, node.modTag, op1, mod1, out dt, out MultiRes) : this.ExecuteExcerptMulti(taskId, context, node.opTag, node.modTag, op1, mod1, out dt, out MultiRes)) : (op1.excerptID == 0L ? this.ExecuteObjSelect(taskId, context, node.opTag, node.modTag, op1, mod1, out dt) : this.ExecuteExcerpt(taskId, context, node.opTag, node.modTag, op1, mod1, out dt));
              if (dt == null)
                throw new ExpertServerException("Impossible sutiation: dt == null in object selection operator!");
              if (op1.afterFilter != null && op1.afterFilter.Count > 0)
              {
                objects.Clear();
                for (int index = dt.RowsCount - 1; index >= 0; --index)
                {
                  HybridRowExp row = dt[index];
                  long int64 = Convert.ToInt64(row[0]);
                  if (!task.CheckRowCond(int64, row, op1.afterFilter))
                    dt.RemoveAt(index);
                  else
                    objects.Add(int64);
                }
              }
              if (objects.Count > 0)
              {
                if (op1.dataAttrChecks != null && op1.dataAttrChecks.Count > 0 && node.modTag != ExpertScriptMod.modIfExists && node.modTag != ExpertScriptMod.modIfAll && !op1.InbuiltSort)
                {
                  if (op1.compId != 0)
                  {
                    this.Sort(dt, task, (object) op1.compId);
                  }
                  else
                  {
                    List<int> colNumList = new List<int>();
                    for (int index = 0; index < op1.dataAttrChecks.Count; ++index)
                    {
                      char attrSort = op1.GetAttrSort(index);
                      switch (attrSort)
                      {
                        case 'a':
                        case 'd':
                          string dataAttrGuiD = op1.dataAttrGUIDs[index];
                          int indexByName = dt.Columns.GetIndexByName(dataAttrGuiD);
                          if (indexByName >= 0)
                          {
                            colNumList.Add(attrSort == 'd' ? -(indexByName + 1) : indexByName + 1);
                            break;
                          }
                          break;
                      }
                    }
                    if (colNumList.Count > 0)
                      dt.Sort(colNumList);
                  }
                }
                if (op1.InbuiltSort && task.docScriptId != 0L && dt.RowsCount > 1)
                {
                  if (!task.Anton_Init)
                  {
                    task.InitInbuiltSort(session, ExpertServer.GetTableNode(node));
                    task.Anton_Init = true;
                  }
                  task.BeforeSorting(objects);
                  this.Sort(dt, task, (object) task.docScriptId);
                }
              }
              switch (op1.saveGlobal)
              {
                case GlobalSave.saveClear:
                  task.savedData = (HybridTableExp) null;
                  task.dataObjIndex = (Dictionary<long, int>) null;
                  task.dataPartIndex = (Dictionary<long, int>) null;
                  this.ReportClearingGlobalTable(task);
                  break;
                case GlobalSave.saveAdd:
                  if (task.savedData != null)
                  {
                    HashSet<long> objIDs = new HashSet<long>();
                    for (int index = 0; index < dt.RowsCount; ++index)
                    {
                      HybridRowExp hr = dt[index];
                      long int64_1 = Convert.ToInt64(hr[0]);
                      objIDs.Add(int64_1);
                      if (op1.Dups || task.savedDataByObjId(int64_1) == null)
                      {
                        HybridRowExp hybridRowExp = task.savedData.ImportRow(hr);
                        long int64_2 = Convert.ToInt64(hybridRowExp["cad00029-306c-11d8-b4e9-00304f19f545"]);
                        long int64_3 = Convert.ToInt64(hybridRowExp["cad00035-306c-11d8-b4e9-00304f19f545"]);
                        task.dataObjIndex.Add(int64_2, index);
                        task.dataPartIndex.Add(int64_3, index);
                      }
                    }
                    ExpertServer.MakeLinkIndexes(task);
                    task.OptAddNewObjects(session, objIDs);
                    break;
                  }
                  break;
                case GlobalSave.saveSet:
                  task.savedData = new HybridTableExp();
                  this.ReportClearingGlobalTable(task);
                  List<int> intList1 = new List<int>();
                  for (int index = 0; index < task.RelCondDescs.Length; ++index)
                  {
                    if (task.RelCondDescs[index].AttributeSource == AttributeSourceTypes.Relation)
                    {
                      int indexByName = dt.Columns.GetIndexByName(Convert.ToString(task.RelCondDescs[index].AttributeID));
                      if (indexByName >= 0)
                        intList1.Add(indexByName);
                    }
                  }
                  for (int index = 0; index < dt.Columns.Count; ++index)
                  {
                    if (!intList1.Contains(index))
                      task.savedData.Columns.Add(dt.Columns[index]);
                  }
                  task.savedData.ImportTable(dt);
                  task.dataObjIndex = new Dictionary<long, int>(dt.RowsCount);
                  task.dataPartIndex = new Dictionary<long, int>(dt.RowsCount);
                  for (int index = 0; index < dt.RowsCount; ++index)
                  {
                    HybridRowExp hybridRowExp = dt[index];
                    object obj1 = hybridRowExp["cad00029-306c-11d8-b4e9-00304f19f545"];
                    if (obj1.NotNullOrDBNull())
                    {
                      long int64 = Convert.ToInt64(obj1);
                      if (!task.dataObjIndex.ContainsKey(int64))
                        task.dataObjIndex.Add(int64, index);
                    }
                    object obj2 = hybridRowExp["cad00035-306c-11d8-b4e9-00304f19f545"];
                    if (obj2.NotNullOrDBNull())
                    {
                      long int64 = Convert.ToInt64(obj2);
                      if (!task.dataPartIndex.ContainsKey(int64))
                        task.dataPartIndex.Add(int64, index);
                    }
                  }
                  ExpertServer.MakeLinkIndexes(task);
                  task.OptInitCollectObjectData();
                  task.OptCollectSavedDataObjects(session);
                  task.OptCollectObjectAttrs(session);
                  if (!task.Anton_Init && task.docScriptId != 0L)
                  {
                    task.InitInbuiltSort(session, ExpertServer.GetTableNode(node));
                    task.Anton_Init = true;
                  }
                  if (task.ispList != null && task.ispList.Count > 0)
                  {
                    this.SortIspsAnton(task);
                    break;
                  }
                  break;
              }
              if (op1.saveGlobal == GlobalSave.saveSet)
              {
                this.PerformSubstitutes(task, session);
                this.ReplaceMemos(session, task.savedData, false);
                this.ReplaceMemos(session, task.savedLinks, true);
                this.MakePrimaryIspFirst(session, task);
              }
              int num5 = 0;
              long[] context1;
              switch (MultiRes)
              {
                case HybridRowExp _:
                  context1 = new long[1]
                  {
                    Convert.ToInt64(((HybridRowExp) MultiRes)[0])
                  };
                  break;
                case HybridRowExp[] _:
                  HybridRowExp[] hybridRowExpArray = (HybridRowExp[]) MultiRes;
                  context1 = new long[hybridRowExpArray.Length];
                  foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
                    context1[num5++] = Convert.ToInt64(hybridRowExp[0]);
                  break;
                default:
                  context1 = (long[]) Array.CreateInstance(typeof (long), dt.RowsCount);
                  for (int index = 0; index < dt.RowsCount; ++index)
                  {
                    HybridRowExp hybridRowExp = dt[index];
                    context1[num5++] = Convert.ToInt64(hybridRowExp[0]);
                  }
                  break;
              }
              ExpertTraceFlags b = ExpertTraceFlags.None;
              lock (task)
                b = task.traceFlags;
              if (this.FlagIn(ExpertTraceFlags.ShowObjResults, b))
                this.ShowContext(taskId, context1, true);
              if (context1 == null || context1.Length == 0)
                break;
              bool flag2 = false;
              if (node.mod is ModParmFormula)
                flag2 = ((ModParmFormula) node.mod).saveContext;
              if (node.Items.Count <= 0 && !flag2)
                break;
              switch (node.modTag)
              {
                case ExpertScriptMod.modUnknown:
                  for (int index = 0; index < node.Items.Count; ++index)
                  {
                    this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index], context1, dt, true, ref new_context);
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modForEach:
                  TempFormula cond1 = (TempFormula) null;
                  bool flag3 = false;
                  if (node.mod is ModParmFormula)
                  {
                    cond1 = ((ModParmFormula) node.mod).tf;
                    flag3 = ((ModParmFormula) node.mod).forAllIsps;
                  }
                  if (!flag3)
                  {
                    HybridTableExp dTable1 = (HybridTableExp) null;
                    for (int index1 = 0; index1 < context1.Length; ++index1)
                    {
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context1[index1]);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context1.Length);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) index1);
                      flag1 = true;
                      if (cond1 == null || task.CheckRowCond(context1[index1], dt[index1], cond1))
                      {
                        long[] context2 = new long[1]
                        {
                          context1[index1]
                        };
                        if (dTable1 == null)
                          dTable1 = dt.CloneEmpty();
                        else
                          dTable1.Clear();
                        dTable1.AddRow(dt[index1]);
                        for (int index2 = 0; index2 < node.Items.Count; ++index2)
                        {
                          this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index2], context2, dTable1, true, ref new_context);
                          if (task.BreakFlag)
                          {
                            task.BreakFlag = false;
                            break;
                          }
                        }
                      }
                    }
                    if (!flag2)
                      return;
                    new_context = (long[]) context1.Clone();
                    return;
                  }
                  int currentIsp1 = task.currentIsp;
                  int indexByName1 = task.savedData.Columns.GetIndexByName("cad0001f-306c-11d8-b4e9-00304f19f545");
                  try
                  {
                    for (int index3 = 0; index3 < task.ispList.Count; ++index3)
                    {
                      task.currentIsp = index3;
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurIspNum, (object) index3);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurIspId, (object) task.ispList[index3]);
                      string str = "";
                      if (indexByName1 >= 0)
                      {
                        HybridRowExp hybridRowExp = task.savedDataByObjId(task.ispList[index3]);
                        if (hybridRowExp == null)
                          break;
                        str = Convert.ToString(hybridRowExp[indexByName1]);
                      }
                      else
                      {
                        IDBObject dbObject = session.GetObject(task.ispList[index3]);
                        if (dbObject != null)
                          str = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AsString;
                      }
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurIspDesign, (object) str);
                      for (int index4 = 0; index4 < node.Items.Count; ++index4)
                      {
                        this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index4], context, dt, false, ref new_context);
                        if (task.BreakFlag)
                        {
                          task.BreakFlag = false;
                          break;
                        }
                      }
                    }
                    return;
                  }
                  finally
                  {
                    task.currentIsp = currentIsp1;
                  }
                case ExpertScriptMod.modForFirst:
                  long num6 = -1;
                  HybridRowExp hr1 = (HybridRowExp) null;
                  if (MultiRes != null && MultiRes is HybridRowExp)
                  {
                    hr1 = MultiRes as HybridRowExp;
                    num6 = Convert.ToInt64(hr1[0]);
                  }
                  else
                  {
                    TempFormula tf = ((ModParmFormula) node.mod).tf;
                    for (int index = 0; index < context1.Length; ++index)
                    {
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context1[index]);
                      if (tf == null || task.CheckRowCond(context1[index], dt[index], tf))
                      {
                        num6 = Convert.ToInt64(context1[index]);
                        hr1 = dt[index];
                        break;
                      }
                    }
                  }
                  if (num6 == -1L)
                    return;
                  long[] context3 = new long[1]{ num6 };
                  HybridTableExp dTable2 = (HybridTableExp) null;
                  if (dTable != null)
                  {
                    dTable2 = dt.CloneEmpty();
                    dTable2.AddRow(hr1);
                  }
                  else if (hr1 != null)
                  {
                    dTable2 = hr1.CloneEmptyTable();
                    dTable2.AddRow(hr1);
                  }
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context3.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) num6);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index = 0; index < node.Items.Count; ++index)
                    this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index], context3, dTable2, true, ref new_context);
                  if (!flag2)
                    return;
                  new_context = (long[]) context3.Clone();
                  return;
                case ExpertScriptMod.modForMin:
                case ExpertScriptMod.modForMax:
                  TempFormula tf1 = ((ModParmFormula) node.mod).tf;
                  if (tf1 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_155"));
                  int index5 = -1;
                  object obj3;
                  switch (tf1.resType)
                  {
                    case DataType.Integer:
                      obj3 = (object) (node.modTag == ExpertScriptMod.modForMin ? long.MaxValue : long.MinValue);
                      break;
                    case DataType.Float:
                      obj3 = (object) (node.modTag == ExpertScriptMod.modForMin ? double.MaxValue : double.MinValue);
                      break;
                    case DataType.String:
                      obj3 = node.modTag == ExpertScriptMod.modForMin ? (object) LocalizationHolder.rm.GetString("Expert.Server_156") : (object) "";
                      break;
                    case DataType.Date:
                      obj3 = (object) (node.modTag == ExpertScriptMod.modForMin ? DateTime.MaxValue : DateTime.MinValue);
                      break;
                    default:
                      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_157"));
                  }
                  for (int index6 = 0; index6 < context1.Length; ++index6)
                  {
                    object obj4 = task.CalcRowFormula(context1[index6], dt[index6], tf1, false);
                    if (obj4 != null)
                    {
                      switch (tf1.resType)
                      {
                        case DataType.Integer:
                          if (node.modTag == ExpertScriptMod.modForMax && (long) obj4 > (long) obj3 || node.modTag == ExpertScriptMod.modForMin && (long) obj4 < (long) obj3)
                          {
                            obj3 = obj4;
                            index5 = index6;
                            continue;
                          }
                          continue;
                        case DataType.Float:
                          if (node.modTag == ExpertScriptMod.modForMax && (double) obj4 > (double) obj3 || node.modTag == ExpertScriptMod.modForMin && (double) obj4 < (double) obj3)
                          {
                            obj3 = obj4;
                            index5 = index6;
                            continue;
                          }
                          continue;
                        case DataType.String:
                          int num7 = string.Compare((string) obj4, (string) obj3);
                          if (node.modTag == ExpertScriptMod.modForMax && num7 > 0 || node.modTag == ExpertScriptMod.modForMin && num7 < 0)
                          {
                            obj3 = obj4;
                            index5 = index6;
                            continue;
                          }
                          continue;
                        case DataType.Date:
                          int num8 = DateTime.Compare((DateTime) obj4, (DateTime) obj3);
                          if (node.modTag == ExpertScriptMod.modForMax && num8 > 0 || node.modTag == ExpertScriptMod.modForMin && num8 < 0)
                          {
                            obj3 = obj4;
                            index5 = index6;
                            continue;
                          }
                          continue;
                        default:
                          throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_158"));
                      }
                    }
                  }
                  if (index5 < 0)
                    return;
                  long[] context4 = new long[1]
                  {
                    context1[index5]
                  };
                  HybridTableExp dTable3 = dt.CloneEmpty();
                  dTable3.AddRow(dt[index5]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context4.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context4[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index7 = 0; index7 < node.Items.Count; ++index7)
                    this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index7], context4, dTable3, true, ref new_context);
                  return;
                case ExpertScriptMod.modIfExists:
                  TempFormula tf2 = ((ModParmFormula) node.mod).tf;
                  if (task.makeLog)
                    ExpertServer.es.iLH.AddToTrace($"If Exists: [{node.label}] {Convert.ToString(context1.Length)} object(s)", Intermech.Consts.traceAlways, this.logFileName);
                  bool flag4 = false;
                  long num9 = -1;
                  if (MultiRes != null && MultiRes is bool)
                    flag4 = Convert.ToBoolean(MultiRes);
                  else if (MultiRes != null && (MultiRes is HybridRowExp || MultiRes is HybridRowExp[]))
                    flag4 = true;
                  else if (tf2 == null)
                  {
                    flag4 = true;
                  }
                  else
                  {
                    for (int index8 = 0; index8 < context1.Length; ++index8)
                    {
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context1[index8]);
                      if (task.CheckRowCond(context1[index8], dt[index8], tf2))
                      {
                        flag4 = true;
                        num9 = context1[index8];
                        break;
                      }
                    }
                  }
                  if (!flag4)
                    return;
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  if (flag2)
                    new_context = new long[1]{ num9 };
                  for (int index9 = 0; index9 < node.Items.Count; ++index9)
                  {
                    this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index9], context, dTable, false, ref new_context);
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modIfAll:
                  TempFormula tf3 = ((ModParmFormula) node.mod).tf;
                  bool flag5 = true;
                  if (MultiRes != null && MultiRes is bool)
                    flag5 = Convert.ToBoolean(MultiRes);
                  else if (MultiRes != null && (MultiRes is HybridRowExp || MultiRes is HybridRowExp[]))
                    flag5 = true;
                  else if (tf3 == null)
                  {
                    flag5 = true;
                  }
                  else
                  {
                    for (int index10 = 0; index10 < context1.Length; ++index10)
                    {
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context1[index10]);
                      if (!task.CheckRowCond(context1[index10], dt[index10], tf3))
                      {
                        flag5 = false;
                        break;
                      }
                    }
                  }
                  if (!flag5)
                    return;
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index11 = 0; index11 < node.Items.Count; ++index11)
                    this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index11], context, dTable, true, ref new_context);
                  if (!flag2)
                    return;
                  new_context = (long[]) context1.Clone();
                  return;
                case ExpertScriptMod.modLoop:
                  TempFormula tf4 = ((ModParmLoop) node.mod).tf;
                  HybridTableExp dTable4 = (HybridTableExp) null;
                  for (int index12 = 0; index12 < context1.Length; ++index12)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context1[index12]);
                    if (tf4 != null && !task.CheckRowCond(context1[index12], dt[index12], tf4))
                      break;
                    long[] context5 = new long[1]
                    {
                      context1[index12]
                    };
                    if (dTable4 == null)
                      dTable4 = dt.CloneEmpty();
                    else
                      dTable4.ClearRows();
                    dTable4.AddRow(dt[index12]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context5.Length);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context5[0]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                    flag1 = true;
                    for (int index13 = 0; index13 < node.Items.Count; ++index13)
                    {
                      this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index13], context5, dTable4, true, ref new_context);
                      if (task.BreakFlag)
                      {
                        task.BreakFlag = false;
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modLoopSort:
                  ModParmSort mod2 = (ModParmSort) node.mod;
                  List<HybridRowExp> hybridRowExpList1;
                  if (mod2.useInbuiltSort)
                  {
                    if (task.docScriptId != 0L && dt.RowsCount > 1)
                    {
                      if (!task.Anton_Init)
                      {
                        task.InitInbuiltSort(session, ExpertServer.GetTableNode(node));
                        task.Anton_Init = true;
                      }
                      task.BeforeSorting((List<long>) null);
                      this.Sort(dt, task, (object) task.docScriptId);
                    }
                    hybridRowExpList1 = dt.SortIndex((List<int>) null);
                  }
                  else
                  {
                    List<int> colNumList = new List<int>();
                    if (mod2.sortAttrs != null)
                    {
                      for (int index14 = 0; index14 < mod2.sortAttrs.Count; ++index14)
                      {
                        string sortAttr = mod2.sortAttrs[index14];
                        int indexByName2 = dt.Columns.GetIndexByName(sortAttr);
                        if (indexByName2 >= 0)
                          colNumList.Add(indexByName2 + 1);
                      }
                    }
                    hybridRowExpList1 = dt.SortIndex(colNumList);
                  }
                  long[] instance1 = (long[]) Array.CreateInstance(typeof (long), context1.Length);
                  for (int index15 = 0; index15 < hybridRowExpList1.Count; ++index15)
                    instance1[index15] = Convert.ToInt64(hybridRowExpList1[index15][0]);
                  HybridTableExp dTable5 = dt.CloneEmpty();
                  for (int index16 = 0; index16 < hybridRowExpList1.Count; ++index16)
                  {
                    dTable5.ClearRows();
                    dTable5.AddRow(hybridRowExpList1[index16]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) 1);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) instance1[index16]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                    long[] context6 = new long[1]
                    {
                      instance1[index16]
                    };
                    flag1 = true;
                    for (int index17 = 0; index17 < node.Items.Count; ++index17)
                    {
                      ScriptTreeNode node3 = (ScriptTreeNode) node.Items[index17];
                      this.ProcessScriptNode(taskId, node3, context6, dTable5, true, ref new_context);
                      if (task.BreakFlag)
                      {
                        task.BreakFlag = false;
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modLoopGroup:
                  ModParmSort mod3 = (ModParmSort) node.mod;
                  StringBuilder stringBuilder1 = new StringBuilder();
                  if (task.makeLog)
                    ExpertServer.es.iLH.AddToTrace($"Loop-Group: [{node.label}] {Convert.ToString(dt.RowsCount)} object(s)", Intermech.Consts.traceAlways, this.logFileName);
                  List<int> colNumList1 = new List<int>();
                  if (mod3.sortAttrs != null)
                  {
                    for (int index18 = 0; index18 < mod3.sortAttrs.Count; ++index18)
                    {
                      string sortAttr = mod3.sortAttrs[index18];
                      int indexByName3 = dt.Columns.GetIndexByName(sortAttr);
                      if (indexByName3 >= 0)
                        colNumList1.Add(indexByName3 + 1);
                    }
                  }
                  List<HybridRowExp> rows1 = dt.SortIndex(colNumList1);
                  List<int> intList2 = new List<int>();
                  if (mod3.groupAttrs != null)
                  {
                    for (int index19 = 0; index19 < mod3.groupAttrs.Count; ++index19)
                    {
                      string groupAttr = mod3.groupAttrs[index19];
                      for (int index20 = 0; index20 < dt.Columns.Count; ++index20)
                      {
                        if (groupAttr == dt.Columns[index20].ColumnName)
                        {
                          intList2.Add(index20);
                          break;
                        }
                      }
                    }
                  }
                  if (intList2.Count == 0)
                  {
                    XmlNode node4 = task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_98"));
                    if (node4 != null)
                      task.traceAddText(node4, LocalizationHolder.rm.GetString("Expert.Server_248"));
                  }
                  int[] array1 = intList2.ToArray();
                  HybridTableExp hybridTableExp1 = dt.CloneEmpty();
                  int curRow1 = 1;
                  int firstRow1 = 0;
                  List<long> longList2 = new List<long>();
                  for (; curRow1 < rows1.Count; ++curRow1)
                  {
                    if (this.RowDiffers(rows1, firstRow1, curRow1, array1))
                    {
                      hybridTableExp1.ClearRows();
                      for (int index21 = firstRow1; index21 < curRow1; ++index21)
                      {
                        hybridTableExp1.ImportRow(rows1[index21]);
                        longList2.Add(Convert.ToInt64(rows1[index21][0]));
                      }
                      long[] array2 = longList2.ToArray();
                      this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) array2.Length);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) array2[0]);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                      StringBuilder stringBuilder2 = new StringBuilder();
                      foreach (int index22 in array1)
                        stringBuilder2.Append(Convert.ToString(hybridTableExp1[0][index22]));
                      hybridTableExp1.TableName = stringBuilder2.ToString();
                      flag1 = true;
                      for (int index23 = 0; index23 < node.Items.Count; ++index23)
                      {
                        this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index23], array2, hybridTableExp1, true, ref new_context);
                        if (task.BreakFlag)
                          break;
                      }
                      longList2.Clear();
                      firstRow1 = curRow1;
                    }
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  if (firstRow1 >= rows1.Count)
                    return;
                  hybridTableExp1.ClearRows();
                  for (int index24 = firstRow1; index24 < rows1.Count; ++index24)
                  {
                    ExpertServer.CopyRow(hybridTableExp1, rows1[index24]);
                    longList2.Add(Convert.ToInt64(rows1[index24][0]));
                  }
                  long[] array3 = longList2.ToArray();
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) array3.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) array3[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index25 = 0; index25 < node.Items.Count; ++index25)
                  {
                    this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index25], array3, hybridTableExp1, true, ref new_context);
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modVersions:
                  ModParmVersion mod4 = (ModParmVersion) node.mod;
                  List<long> longList3 = new List<long>();
                  foreach (long objectId in context1)
                  {
                    List<long> requiredVersions = this.GetRequiredVersions(session, task, objectId, mod4);
                    longList3.AddRange((IEnumerable<long>) requiredVersions);
                  }
                  for (int index26 = 0; index26 < longList3.Count; ++index26)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) longList3[index26]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) longList3.Count);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) index26);
                    flag1 = true;
                    long[] context7 = new long[1]
                    {
                      longList3[index26]
                    };
                    for (int index27 = 0; index27 < node.Items.Count; ++index27)
                    {
                      this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index27], context7, (HybridTableExp) null, true, ref new_context);
                      if (task.BreakFlag)
                      {
                        task.BreakFlag = false;
                        break;
                      }
                    }
                  }
                  if (!flag2)
                    return;
                  new_context = (long[]) context1.Clone();
                  return;
                default:
                  return;
              }
            case ExpertScriptOp.opExit:
              switch (node.modTag)
              {
                case ExpertScriptMod.modUnknown:
                  OpParmCond op2 = (OpParmCond) node.op;
                  if (op2.cond != null && !task.CheckGlobalCond(op2.cond, context[0], (HybridRowExp) null))
                    return;
                  task.BreakFlag = true;
                  return;
                case ExpertScriptMod.modIfExists:
                  TempFormula tf5 = ((ModParmFormula) node.mod).tf;
                  if (tf5 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_176"));
                  bool flag6 = false;
                  for (int index28 = 0; index28 < context.Length; ++index28)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index28]);
                    if (task.CheckRowCond(context[index28], dTable?[index28], tf5))
                    {
                      flag6 = true;
                      break;
                    }
                  }
                  if (!flag6)
                    return;
                  task.BreakFlag = true;
                  return;
                case ExpertScriptMod.modIfAll:
                  TempFormula tf6 = ((ModParmFormula) node.mod).tf;
                  if (tf6 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_175"));
                  bool flag7 = true;
                  for (int index29 = 0; index29 < context.Length; ++index29)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index29]);
                    if (!task.CheckRowCond(context[index29], dTable?[index29], tf6))
                    {
                      flag7 = false;
                      break;
                    }
                  }
                  if (!flag7)
                    return;
                  task.BreakFlag = true;
                  return;
                default:
                  return;
              }
            case ExpertScriptOp.opFolder:
            case ExpertScriptOp.opSelFolder:
              OpParmCond op3 = (OpParmCond) node.op;
              TempFormula cond2 = op3.cond;
              if (op3.refAttrGuid != "")
              {
                Guid guid = new Guid(op3.refAttrGuid);
                IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(guid);
                int num10 = attributeType != null ? attributeType.AttributeID : throw new ExpertServerException("Attribute not found " + guid.ToString());
                ExpertServer.TempAttrStru tempAttrStru = task.GetTempAttrStru(guid);
                if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
                {
                  context = new long[1]
                  {
                    Convert.ToInt64(this.InnerGetParm(task, attributeType.AttributeID))
                  };
                  dTable = (HybridTableExp) null;
                  break;
                }
                if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithObject))
                {
                  List<long> longList4 = new List<long>();
                  for (int index30 = 0; index30 < context.Length; ++index30)
                  {
                    long int64 = Convert.ToInt64(this._GetParmValue(task, context[index30], -1, num10));
                    longList4.Add(int64);
                  }
                  context = longList4.ToArray();
                  dTable = (HybridTableExp) null;
                  break;
                }
                if (this.IsLinkAttribute(attributeType.FieldType, num10))
                {
                  int num11 = attributeType.MultiValueMode == MultiValueModes.MultiValues ? 1 : (attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList ? 1 : 0);
                  List<long> longList5 = new List<long>();
                  for (int index31 = 0; index31 < context.Length; ++index31)
                  {
                    IDBObject dbObject = session.GetObject(context[index31], false);
                    if (dbObject != null)
                    {
                      object val = (object) null;
                      int attr = (int) this.CalculateAttr(task.taskId, dbObject.ObjectType, num10, context[index31], ExpertServer.CalcStages.CheckObject | ExpertServer.CalcStages.CalcAttribute, out val);
                      if (val is ArrayHolder)
                      {
                        ArrayHolder arrayHolder = (ArrayHolder) val;
                        for (int x = 0; x < arrayHolder.Width; ++x)
                          longList5.Add(Convert.ToInt64(arrayHolder[x, 0]));
                      }
                      else
                      {
                        long int64 = Convert.ToInt64(val);
                        longList5.Add(int64);
                      }
                    }
                  }
                  if (longList5.Count == 0)
                    break;
                  context = longList5.ToArray();
                  dTable = (HybridTableExp) null;
                }
              }
              if (cond2 != null && (context.Length != 0 && !task.CheckGlobalCond(cond2, context[0], dTable?[0]) || context.Length == 0 && !task.CheckGlobalCond(cond2)))
                break;
              switch (node.modTag)
              {
                case ExpertScriptMod.modUnknown:
                  for (int index32 = 0; index32 < node.Items.Count; ++index32)
                  {
                    ScriptTreeNode node5 = (ScriptTreeNode) node.Items[index32];
                    if (node.opTag == ExpertScriptOp.opFolder)
                      this.ProcessScriptNode(taskId, node5, context, dTable, false, ref new_context);
                    else if (!node5.label.StartsWith("#"))
                    {
                      TempFormula selFolderCond = this.GetSelFolderCond(node5);
                      if (selFolderCond == null || task.CheckGlobalCond(selFolderCond, context[0], dTable?[0]))
                      {
                        this.ProcessScriptNode(taskId, node5, context, dTable, false, ref new_context);
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modForEach:
                  TempFormula tf7 = ((ModParmFormula) node.mod).tf;
                  if (!((ModParmFormula) node.mod).forAllIsps)
                  {
                    HybridTableExp dTable6 = (HybridTableExp) null;
                    for (int index33 = 0; index33 < context.Length; ++index33)
                    {
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index33]);
                      if (tf7 == null || task.CheckRowCond(context[index33], dTable?[index33], tf7))
                      {
                        long[] context8 = new long[1]
                        {
                          context[index33]
                        };
                        if (dTable != null)
                        {
                          if (dTable6 == null)
                            dTable6 = dTable.CloneEmpty();
                          else
                            dTable6.ClearRows();
                          dTable6.AddRow(dTable[index33]);
                        }
                        this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context8.Length);
                        this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) index33);
                        flag1 = true;
                        for (int index34 = 0; index34 < node.Items.Count; ++index34)
                        {
                          ScriptTreeNode node6 = (ScriptTreeNode) node.Items[index34];
                          if (node.opTag == ExpertScriptOp.opFolder)
                          {
                            this.ProcessScriptNode(taskId, node6, context8, dTable6, true, ref new_context);
                          }
                          else
                          {
                            TempFormula selFolderCond = this.GetSelFolderCond(node6);
                            if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                            {
                              this.ProcessScriptNode(taskId, node6, context8, dTable6, true, ref new_context);
                              break;
                            }
                          }
                          if (task.BreakFlag)
                          {
                            task.BreakFlag = false;
                            break;
                          }
                        }
                      }
                    }
                    return;
                  }
                  if (task.ispList == null)
                    return;
                  int currentIsp2 = task.currentIsp;
                  int indexByName4 = task.savedData.Columns.GetIndexByName("cad0001f-306c-11d8-b4e9-00304f19f545");
                  try
                  {
                    for (int index35 = 0; index35 < task.ispList.Count; ++index35)
                    {
                      task.currentIsp = index35;
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurIspNum, (object) index35);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurIspId, (object) task.ispList[index35]);
                      string str = "";
                      HybridRowExp row = (HybridRowExp) null;
                      if (indexByName4 >= 0)
                      {
                        row = task.savedDataByObjId(task.ispList[index35]);
                        if (row == null)
                          break;
                        str = Convert.ToString(row[indexByName4]);
                      }
                      else
                      {
                        IDBObject dbObject = session.GetObject(task.ispList[index35]);
                        if (dbObject != null)
                          str = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")).AsString;
                      }
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurIspDesign, (object) str);
                      long[] context9 = new long[1]
                      {
                        task.ispList[index35]
                      };
                      TempFormula tf8 = ((ModParmFormula) node.mod).tf;
                      if (tf8 == null || tf8.postfixForm.Count == 0 || task.CheckRowCond(task.ispList[index35], row, tf8))
                      {
                        for (int index36 = 0; index36 < node.Items.Count; ++index36)
                        {
                          this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index36], context9, dTable, false, ref new_context);
                          if (task.BreakFlag)
                          {
                            task.BreakFlag = false;
                            break;
                          }
                        }
                      }
                    }
                    return;
                  }
                  finally
                  {
                    task.currentIsp = currentIsp2;
                  }
                case ExpertScriptMod.modForFirst:
                  HybridTableExp dTable7 = (HybridTableExp) null;
                  TempFormula tf9 = ((ModParmFormula) node.mod).tf;
                  for (int index37 = 0; index37 < context.Length; ++index37)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index37]);
                    if (tf9 == null || task.CheckRowCond(context[index37], dTable?[index37], tf9))
                    {
                      long[] context10 = new long[1]
                      {
                        context[index37]
                      };
                      if (dTable != null)
                      {
                        if (dTable7 == null)
                          dTable7 = dTable.CloneEmpty();
                        else
                          dTable7.ClearRows();
                        dTable7.AddRow(dTable[index37]);
                      }
                      this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context10.Length);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                      flag1 = true;
                      for (int index38 = 0; index38 < node.Items.Count; ++index38)
                      {
                        ScriptTreeNode node7 = (ScriptTreeNode) node.Items[index38];
                        if (node.opTag == ExpertScriptOp.opFolder)
                        {
                          this.ProcessScriptNode(taskId, node7, context10, dTable7, true, ref new_context);
                        }
                        else
                        {
                          TempFormula selFolderCond = this.GetSelFolderCond(node7);
                          if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                          {
                            this.ProcessScriptNode(taskId, node7, context10, dTable7, true, ref new_context);
                            break;
                          }
                        }
                      }
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modForMin:
                case ExpertScriptMod.modForMax:
                  TempFormula tf10 = ((ModParmFormula) node.mod).tf;
                  if (tf10 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_161"));
                  int index39 = -1;
                  object obj5;
                  switch (tf10.resType)
                  {
                    case DataType.Integer:
                      obj5 = (object) (node.modTag == ExpertScriptMod.modForMin ? long.MaxValue : long.MinValue);
                      break;
                    case DataType.Float:
                      obj5 = (object) (node.modTag == ExpertScriptMod.modForMin ? double.MaxValue : double.MinValue);
                      break;
                    case DataType.Measured:
                      obj5 = (object) new MeasuredValue(node.modTag == ExpertScriptMod.modForMin ? double.MaxValue : double.MinValue, 0L);
                      break;
                    case DataType.String:
                      obj5 = node.modTag == ExpertScriptMod.modForMin ? (object) LocalizationHolder.rm.GetString("Expert.Server_162") : (object) "";
                      break;
                    case DataType.Date:
                      obj5 = (object) (node.modTag == ExpertScriptMod.modForMin ? DateTime.MaxValue : DateTime.MinValue);
                      break;
                    default:
                      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_163"));
                  }
                  if (dTable != null)
                  {
                    for (int index40 = 0; index40 < context.Length; ++index40)
                    {
                      object obj6 = task.CalcRowFormula(context[index40], dTable[index40], tf10, false);
                      switch (tf10.resType)
                      {
                        case DataType.Integer:
                          if (node.modTag == ExpertScriptMod.modForMax && (long) obj6 > (long) obj5 || node.modTag == ExpertScriptMod.modForMin && (long) obj6 < (long) obj5)
                          {
                            obj5 = obj6;
                            index39 = index40;
                            break;
                          }
                          break;
                        case DataType.Float:
                          if (node.modTag == ExpertScriptMod.modForMax && (double) obj6 > (double) obj5 || node.modTag == ExpertScriptMod.modForMin && (double) obj6 < (double) obj5)
                          {
                            obj5 = obj6;
                            index39 = index40;
                            break;
                          }
                          break;
                        case DataType.Measured:
                          if (!(obj6 is MeasuredValue))
                            obj6 = (object) new MeasuredValue(Convert.ToDouble(obj6), 0L);
                          CompareResult compareResult = MeasureHelper.Compare((MeasuredValue) obj6, (MeasuredValue) obj5);
                          if (node.modTag == ExpertScriptMod.modForMax && compareResult == CompareResult.More || node.modTag == ExpertScriptMod.modForMin && compareResult == CompareResult.Less)
                          {
                            obj5 = obj6;
                            index39 = index40;
                            break;
                          }
                          break;
                        case DataType.String:
                          int num12 = string.Compare((string) obj6, (string) obj5);
                          if (node.modTag == ExpertScriptMod.modForMax && num12 > 0 || node.modTag == ExpertScriptMod.modForMin && num12 < 0)
                          {
                            obj5 = obj6;
                            index39 = index40;
                            break;
                          }
                          break;
                        case DataType.Date:
                          int num13 = DateTime.Compare((DateTime) obj6, (DateTime) obj5);
                          if (node.modTag == ExpertScriptMod.modForMax && num13 > 0 || node.modTag == ExpertScriptMod.modForMin && num13 < 0)
                          {
                            obj5 = obj6;
                            index39 = index40;
                            break;
                          }
                          break;
                        default:
                          throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_164"));
                      }
                    }
                  }
                  if (index39 < 0)
                    return;
                  long[] context11 = new long[1]
                  {
                    context[index39]
                  };
                  HybridTableExp dTable8 = (HybridTableExp) null;
                  if (dTable != null)
                  {
                    dTable8 = dTable.CloneEmpty();
                    dTable8.AddRow(dTable[index39]);
                  }
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context11.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context11[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index41 = 0; index41 < node.Items.Count; ++index41)
                  {
                    ScriptTreeNode node8 = (ScriptTreeNode) node.Items[index41];
                    if (node.opTag == ExpertScriptOp.opFolder)
                    {
                      this.ProcessScriptNode(taskId, node8, context11, dTable8, true, ref new_context);
                    }
                    else
                    {
                      TempFormula selFolderCond = this.GetSelFolderCond(node8);
                      if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                      {
                        this.ProcessScriptNode(taskId, node8, context11, dTable8, true, ref new_context);
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modIfExists:
                  TempFormula tf11 = ((ModParmFormula) node.mod).tf;
                  if (tf11 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_166"));
                  bool flag8 = false;
                  for (int index42 = 0; index42 < context.Length; ++index42)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index42]);
                    if (task.CheckRowCond(context[index42], dTable?[index42], tf11))
                    {
                      flag8 = true;
                      break;
                    }
                  }
                  if (!flag8)
                    return;
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index43 = 0; index43 < node.Items.Count; ++index43)
                  {
                    ScriptTreeNode node9 = (ScriptTreeNode) node.Items[index43];
                    if (node.opTag == ExpertScriptOp.opFolder)
                    {
                      this.ProcessScriptNode(taskId, node9, context, dTable, false, ref new_context);
                    }
                    else
                    {
                      TempFormula selFolderCond = this.GetSelFolderCond(node9);
                      if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                      {
                        this.ProcessScriptNode(taskId, node9, context, dTable, false, ref new_context);
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modIfAll:
                  TempFormula tf12 = ((ModParmFormula) node.mod).tf;
                  if (tf12 == null || tf12.Count == 0)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_165"));
                  bool flag9 = true;
                  for (int index44 = 0; index44 < context.Length; ++index44)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index44]);
                    if (!task.CheckRowCond(context[index44], dTable?[index44], tf12))
                    {
                      flag9 = false;
                      break;
                    }
                  }
                  if (!flag9)
                    return;
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index45 = 0; index45 < node.Items.Count; ++index45)
                  {
                    ScriptTreeNode node10 = (ScriptTreeNode) node.Items[index45];
                    if (node.opTag == ExpertScriptOp.opFolder)
                    {
                      this.ProcessScriptNode(taskId, node10, context, dTable, false, ref new_context);
                    }
                    else
                    {
                      TempFormula selFolderCond = this.GetSelFolderCond(node10);
                      if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                      {
                        this.ProcessScriptNode(taskId, node10, context, dTable, false, ref new_context);
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modLoop:
                  ModParmLoop mod5 = (ModParmLoop) node.mod;
                  if (mod5.startWith == int.MaxValue)
                  {
                    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(mod5.RefGuid);
                    if (attributeTypeId1 == 0)
                      return;
                    bool flag10 = MetaDataHelper.GetAttributeType(attributeTypeId1).FieldType == FieldTypes.ftObjectLink;
                    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(mod5.attrGUID);
                    object val = (object) null;
                    CalculatedAttr calculatedAttr = (CalculatedAttr) null;
                    if (task.CalcAttrs.TryGetValue(context[0], -1, attributeTypeId1, out calculatedAttr))
                      val = calculatedAttr.Value;
                    if (val == null)
                    {
                      int attr = (int) this.CalculateAttr(taskId, -1, attributeTypeId1, context[0], ExpertServer.CalcStages.CheckObject | ExpertServer.CalcStages.FindObject, out val);
                    }
                    if (val is PacketValue)
                      val = (object) new ArrayHolder(val as PacketValue);
                    if (!(val is ArrayHolder))
                      return;
                    ArrayHolder arrayHolder = val as ArrayHolder;
                    this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) arrayHolder.Width);
                    for (int x = 0; x < arrayHolder.Width; ++x)
                    {
                      long[] context12 = context;
                      if (flag10)
                      {
                        long int64 = Convert.ToInt64(arrayHolder[x, 0]);
                        this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) int64);
                        this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) x);
                        flag1 = true;
                        context12 = new long[1]{ int64 };
                      }
                      if (attributeTypeId2 != -1)
                      {
                        long objID = task.IsTempAttrWithoutObject(attributeTypeId2) ? -1L : context12[0];
                        this.InnerSetParm(task, new CalcAttrPair(objID, attributeTypeId2), arrayHolder[x, 0]);
                      }
                      for (int index46 = 0; index46 < node.Items.Count; ++index46)
                      {
                        ScriptTreeNode node11 = (ScriptTreeNode) node.Items[index46];
                        if (node.opTag == ExpertScriptOp.opFolder)
                        {
                          this.ProcessScriptNode(taskId, node11, context12, dTable, false, ref new_context);
                        }
                        else
                        {
                          TempFormula selFolderCond = this.GetSelFolderCond(node11);
                          if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                          {
                            this.ProcessScriptNode(taskId, node11, context12, dTable, false, ref new_context);
                            break;
                          }
                        }
                        if (task.BreakFlag)
                        {
                          task.BreakFlag = false;
                          break;
                        }
                      }
                    }
                    return;
                  }
                  if (mod5.whileLoop)
                  {
                    TempFormula tf13 = ((ModParmLoop) node.mod).tf;
                    if (tf13 == null)
                      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_167"));
                    while (task.CheckRowCond(context[0], dTable?[0], tf13))
                    {
                      this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) context.Length);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[0]);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                      flag1 = true;
                      for (int index47 = 0; index47 < node.Items.Count; ++index47)
                      {
                        ScriptTreeNode node12 = (ScriptTreeNode) node.Items[index47];
                        if (node.opTag == ExpertScriptOp.opFolder)
                        {
                          this.ProcessScriptNode(taskId, node12, context, dTable, false, ref new_context);
                        }
                        else
                        {
                          TempFormula selFolderCond = this.GetSelFolderCond(node12);
                          if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                          {
                            this.ProcessScriptNode(taskId, node12, context, dTable, false, ref new_context);
                            break;
                          }
                        }
                        if (task.BreakFlag)
                        {
                          task.BreakFlag = false;
                          break;
                        }
                      }
                    }
                    return;
                  }
                  IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(new Guid(mod5.attrGUID));
                  object obj7 = (object) null;
                  if (node.label.StartsWith("&&"))
                  {
                    int num14 = (int) task._CalcFormula(new long[1]
                    {
                      context[0]
                    }, (HybridRowExp) null, mod5.tf, out obj7, false);
                  }
                  else
                  {
                    int num15 = (int) task.CalcFormula(new long[1]
                    {
                      context[0]
                    }, (HybridRowExp) null, mod5.tf, out obj7, 0L);
                  }
                  int num16 = -1;
                  if (obj7 != null)
                    num16 = Convert.ToInt32(obj7);
                  for (int startWith = mod5.startWith; startWith <= num16; ++startWith)
                  {
                    task.__SetValue(new CalculatedAttr(new CalcAttrPair(-1L, attributeType1.AttributeID), (object) startWith, AttrState.Unknown));
                    for (int index48 = 0; index48 < node.Items.Count; ++index48)
                    {
                      ScriptTreeNode node13 = (ScriptTreeNode) node.Items[index48];
                      this.ProcessScriptNode(taskId, node13, context, dTable, false, ref new_context);
                    }
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modLoopSort:
                  ModParmSort mod6 = (ModParmSort) node.mod;
                  if (dTable == null)
                    dTable = this.CollectObjectData(session, task, context, mod6);
                  List<HybridRowExp> hybridRowExpList2;
                  if (mod6.useInbuiltSort)
                  {
                    if (task.docScriptId != 0L && dTable.RowsCount > 1)
                    {
                      if (!task.Anton_Init)
                      {
                        task.InitInbuiltSort(session, ExpertServer.GetTableNode(node));
                        task.Anton_Init = true;
                      }
                      task.BeforeSorting((List<long>) null);
                      task.SetTriple(dTable.TableName);
                      this.Sort(dTable, task, (object) task.docScriptId);
                    }
                    hybridRowExpList2 = dTable.SortIndex((List<int>) null);
                  }
                  else
                  {
                    List<int> colNumList2 = new List<int>();
                    if (mod6.sortAttrs != null)
                    {
                      for (int index49 = 0; index49 < mod6.sortAttrs.Count; ++index49)
                      {
                        string sortAttr = mod6.sortAttrs[index49];
                        int indexByName5 = dTable.Columns.GetIndexByName(sortAttr);
                        if (indexByName5 >= 0)
                          colNumList2.Add(indexByName5 + 1);
                      }
                    }
                    hybridRowExpList2 = dTable.SortIndex(colNumList2);
                  }
                  long[] instance2 = (long[]) Array.CreateInstance(typeof (long), context.Length);
                  for (int index50 = 0; index50 < hybridRowExpList2.Count; ++index50)
                    instance2[index50] = Convert.ToInt64(hybridRowExpList2[index50][0]);
                  HybridTableExp dTable9 = dTable.CloneEmpty();
                  for (int index51 = 0; index51 < hybridRowExpList2.Count; ++index51)
                  {
                    dTable9.ClearRows();
                    dTable9.AddRow(hybridRowExpList2[index51]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) 1);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) instance2[index51]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                    long[] context13 = new long[1]
                    {
                      instance2[index51]
                    };
                    flag1 = true;
                    for (int index52 = 0; index52 < node.Items.Count; ++index52)
                    {
                      ScriptTreeNode node14 = (ScriptTreeNode) node.Items[index52];
                      if (node.opTag == ExpertScriptOp.opFolder)
                      {
                        this.ProcessScriptNode(taskId, node14, context13, dTable9, true, ref new_context);
                      }
                      else
                      {
                        TempFormula selFolderCond = this.GetSelFolderCond(node14);
                        if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                        {
                          this.ProcessScriptNode(taskId, node14, context13, dTable9, true, ref new_context);
                          break;
                        }
                      }
                      if (task.BreakFlag)
                      {
                        task.BreakFlag = false;
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modLoopGroup:
                  ModParmSort mod7 = (ModParmSort) node.mod;
                  if (dTable == null)
                    dTable = this.CollectObjectData(session, task, context, mod7);
                  List<int> colNumList3 = new List<int>();
                  for (int index53 = 0; index53 < mod7.sortAttrs.Count; ++index53)
                  {
                    string sortAttr = mod7.sortAttrs[index53];
                    int indexByName6 = dTable.Columns.GetIndexByName(sortAttr);
                    if (indexByName6 >= 0)
                      colNumList3.Add(indexByName6 + 1);
                  }
                  List<HybridRowExp> rows2 = dTable.SortIndex(colNumList3);
                  List<int> intList3 = new List<int>();
                  for (int index54 = 0; index54 < mod7.groupAttrs.Count; ++index54)
                  {
                    string groupAttr = mod7.groupAttrs[index54];
                    for (int index55 = 0; index55 < dTable.Columns.Count; ++index55)
                    {
                      if (groupAttr == dTable.Columns[index55].ColumnName)
                      {
                        intList3.Add(index55);
                        break;
                      }
                    }
                  }
                  int[] array4 = intList3.ToArray();
                  HybridTableExp hybridTableExp2 = dTable.CloneEmpty();
                  int curRow2 = 1;
                  int firstRow2 = 0;
                  List<long> longList6 = new List<long>();
                  for (; curRow2 < rows2.Count; ++curRow2)
                  {
                    if (this.RowDiffers(rows2, firstRow2, curRow2, array4))
                    {
                      hybridTableExp2.ClearRows();
                      for (int index56 = firstRow2; index56 < curRow2; ++index56)
                      {
                        hybridTableExp2.AddRow(rows2[index56]);
                        longList6.Add(Convert.ToInt64(rows2[index56][0]));
                      }
                      long[] array5 = longList6.ToArray();
                      this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) array5.Length);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) array5[0]);
                      this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                      flag1 = true;
                      for (int index57 = 0; index57 < node.Items.Count; ++index57)
                      {
                        ScriptTreeNode node15 = (ScriptTreeNode) node.Items[index57];
                        if (node.opTag == ExpertScriptOp.opFolder)
                        {
                          this.ProcessScriptNode(taskId, node15, array5, hybridTableExp2, true, ref new_context);
                        }
                        else
                        {
                          TempFormula selFolderCond = this.GetSelFolderCond(node15);
                          if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                          {
                            this.ProcessScriptNode(taskId, node15, array5, hybridTableExp2, true, ref new_context);
                            break;
                          }
                        }
                      }
                      longList6.Clear();
                      firstRow2 = curRow2;
                    }
                  }
                  if (firstRow2 >= rows2.Count)
                    return;
                  hybridTableExp2.ClearRows();
                  for (int index58 = firstRow2; index58 < rows2.Count; ++index58)
                  {
                    ExpertServer.CopyRow(hybridTableExp2, rows2[index58]);
                    longList6.Add(Convert.ToInt64(rows2[index58][0]));
                  }
                  long[] array6 = longList6.ToArray();
                  this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) array6.Length);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) array6[0]);
                  this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                  flag1 = true;
                  for (int index59 = 0; index59 < node.Items.Count; ++index59)
                  {
                    ScriptTreeNode node16 = (ScriptTreeNode) node.Items[index59];
                    if (node.opTag == ExpertScriptOp.opFolder)
                    {
                      this.ProcessScriptNode(taskId, node16, array6, hybridTableExp2, true, ref new_context);
                    }
                    else
                    {
                      TempFormula selFolderCond = this.GetSelFolderCond(node16);
                      if (selFolderCond == null || task.CheckGlobalCond(selFolderCond))
                      {
                        this.ProcessScriptNode(taskId, node16, array6, hybridTableExp2, true, ref new_context);
                        break;
                      }
                    }
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modVersions:
                  ModParmVersion mod8 = (ModParmVersion) node.mod;
                  List<long> longList7 = new List<long>();
                  foreach (long objectId in context)
                  {
                    List<long> requiredVersions = this.GetRequiredVersions(session, task, objectId, mod8);
                    longList7.AddRange((IEnumerable<long>) requiredVersions);
                  }
                  for (int index60 = 0; index60 < longList7.Count; ++index60)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) longList7[index60]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) longList7.Count);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) index60);
                    flag1 = true;
                    long[] context14 = new long[1]
                    {
                      longList7[index60]
                    };
                    for (int index61 = 0; index61 < node.Items.Count; ++index61)
                    {
                      this.ProcessScriptNode(taskId, (ScriptTreeNode) node.Items[index61], context14, (HybridTableExp) null, true, ref new_context);
                      if (task.BreakFlag)
                      {
                        task.BreakFlag = false;
                        break;
                      }
                    }
                  }
                  return;
                default:
                  return;
              }
            case ExpertScriptOp.opSetting:
            case ExpertScriptOp.opDocFillText:
            case ExpertScriptOp.opDocNewElem:
            case ExpertScriptOp.opDocSelectElem:
            case ExpertScriptOp.opDocControl:
            case ExpertScriptOp.opRecalc:
            case ExpertScriptOp.opUserProc:
            case ExpertScriptOp.opVersionRule:
            case ExpertScriptOp.opSetInBase:
              switch (node.modTag)
              {
                case ExpertScriptMod.modUnknown:
                  this._Oper(taskId, session, node, context, dTable);
                  return;
                case ExpertScriptMod.modForEach:
                  TempFormula tf14 = ((ModParmFormula) node.mod).tf;
                  HybridTableExp dTable10 = (HybridTableExp) null;
                  for (int index62 = 0; index62 < context.Length; ++index62)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index62]);
                    if (tf14 == null || task.CheckRowCond(context[index62], dTable?[index62], tf14))
                    {
                      long[] context15 = new long[1]
                      {
                        context[index62]
                      };
                      if (dTable != null)
                      {
                        if (dTable10 == null)
                          dTable10 = dTable.CloneEmpty();
                        else
                          dTable10.ClearRows();
                        dTable10.AddRow(dTable[index62]);
                      }
                      this._Oper(taskId, session, node, context15, dTable10);
                      if (task.BreakFlag)
                      {
                        task.BreakFlag = false;
                        break;
                      }
                    }
                  }
                  return;
                case ExpertScriptMod.modForFirst:
                  TempFormula tf15 = ((ModParmFormula) node.mod).tf;
                  HybridTableExp dTable11 = (HybridTableExp) null;
                  for (int index63 = 0; index63 < context.Length; ++index63)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index63]);
                    if (tf15 == null || task.CheckRowCond(context[index63], dTable?[index63], tf15))
                    {
                      long[] context16 = new long[1]
                      {
                        context[index63]
                      };
                      if (dTable != null)
                      {
                        if (dTable11 == null)
                          dTable11 = dTable.CloneEmpty();
                        else
                          dTable11.ClearRows();
                        dTable11.AddRow(dTable[index63]);
                      }
                      this._Oper(taskId, session, node, context16, dTable11);
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modForMin:
                case ExpertScriptMod.modForMax:
                  TempFormula tf16 = ((ModParmFormula) node.mod).tf;
                  if (tf16 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_168"));
                  int index64 = -1;
                  object obj8;
                  switch (tf16.resType)
                  {
                    case DataType.Integer:
                      obj8 = (object) (node.modTag == ExpertScriptMod.modForMin ? long.MaxValue : long.MinValue);
                      break;
                    case DataType.Float:
                      obj8 = (object) (node.modTag == ExpertScriptMod.modForMin ? double.MaxValue : double.MinValue);
                      break;
                    case DataType.Measured:
                      obj8 = (object) new MeasuredValue(node.modTag == ExpertScriptMod.modForMin ? double.MaxValue : double.MinValue, 0L);
                      break;
                    case DataType.String:
                      obj8 = node.modTag == ExpertScriptMod.modForMin ? (object) LocalizationHolder.rm.GetString("Expert.Server_169") : (object) "";
                      break;
                    case DataType.Date:
                      obj8 = (object) (node.modTag == ExpertScriptMod.modForMin ? DateTime.MaxValue : DateTime.MinValue);
                      break;
                    default:
                      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_170"));
                  }
                  if (dTable != null)
                  {
                    for (int index65 = 0; index65 < context.Length; ++index65)
                    {
                      object obj9 = task.CalcRowFormula(context[index65], dTable[index65], tf16, false);
                      switch (tf16.resType)
                      {
                        case DataType.Integer:
                          if (node.modTag == ExpertScriptMod.modForMax && (long) obj9 > (long) obj8 || node.modTag == ExpertScriptMod.modForMin && (long) obj9 < (long) obj8)
                          {
                            obj8 = obj9;
                            index64 = index65;
                            break;
                          }
                          break;
                        case DataType.Float:
                          if (node.modTag == ExpertScriptMod.modForMax && (double) obj9 > (double) obj8 || node.modTag == ExpertScriptMod.modForMin && (double) obj9 < (double) obj8)
                          {
                            obj8 = obj9;
                            index64 = index65;
                            break;
                          }
                          break;
                        case DataType.Measured:
                          if (!(obj9 is MeasuredValue))
                            obj9 = (object) new MeasuredValue(Convert.ToDouble(obj9), 0L);
                          CompareResult compareResult = MeasureHelper.Compare((MeasuredValue) obj9, (MeasuredValue) obj8);
                          if (node.modTag == ExpertScriptMod.modForMax && compareResult == CompareResult.More || node.modTag == ExpertScriptMod.modForMin && compareResult == CompareResult.Less)
                          {
                            obj8 = obj9;
                            index64 = index65;
                            break;
                          }
                          break;
                        case DataType.String:
                          int num17 = string.Compare((string) obj9, (string) obj8);
                          if (node.modTag == ExpertScriptMod.modForMax && num17 > 0 || node.modTag == ExpertScriptMod.modForMin && num17 < 0)
                          {
                            obj8 = obj9;
                            index64 = index65;
                            break;
                          }
                          break;
                        case DataType.Date:
                          int num18 = DateTime.Compare((DateTime) obj9, (DateTime) obj8);
                          if (node.modTag == ExpertScriptMod.modForMax && num18 > 0 || node.modTag == ExpertScriptMod.modForMin && num18 < 0)
                          {
                            obj8 = obj9;
                            index64 = index65;
                            break;
                          }
                          break;
                        default:
                          throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_171"));
                      }
                    }
                  }
                  if (index64 < 0)
                    return;
                  long[] context17 = new long[1]
                  {
                    context[index64]
                  };
                  HybridTableExp dTable12 = (HybridTableExp) null;
                  if (dTable != null)
                  {
                    dTable12 = dTable.CloneEmpty();
                    dTable12.AddRow(dTable[index64]);
                  }
                  this._Oper(taskId, session, node, context17, dTable12);
                  return;
                case ExpertScriptMod.modIfExists:
                  TempFormula tf17 = ((ModParmFormula) node.mod).tf;
                  if (tf17 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_173"));
                  bool flag11 = false;
                  for (int index66 = 0; index66 < context.Length; ++index66)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index66]);
                    if (task.CheckRowCond(context[index66], dTable?[index66], tf17))
                    {
                      flag11 = true;
                      break;
                    }
                  }
                  if (!flag11)
                    return;
                  this._Oper(taskId, session, node, context, dTable);
                  return;
                case ExpertScriptMod.modIfAll:
                  TempFormula tf18 = ((ModParmFormula) node.mod).tf;
                  if (tf18 == null || tf18.Count == 0)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_172"));
                  bool flag12 = true;
                  for (int index67 = 0; index67 < context.Length; ++index67)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index67]);
                    if (!task.CheckRowCond(context[index67], dTable?[index67], tf18))
                    {
                      flag12 = false;
                      break;
                    }
                  }
                  if (!flag12)
                    return;
                  this._Oper(taskId, session, node, context, dTable);
                  return;
                case ExpertScriptMod.modLoop:
                  ModParmLoop mod9 = (ModParmLoop) node.mod;
                  TempFormula tf19 = mod9.tf;
                  if (mod9.whileLoop)
                  {
                    if (tf19 == null)
                      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_174"));
                    while (task.CheckCond(context[0], tf19))
                    {
                      this._Oper(taskId, session, node, context, dTable);
                      if (task.BreakFlag)
                      {
                        task.BreakFlag = false;
                        break;
                      }
                    }
                    return;
                  }
                  IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType(new Guid(mod9.attrGUID));
                  object obj10 = (object) null;
                  if (node.label.StartsWith("&&"))
                  {
                    int num19 = (int) task._CalcFormula(new long[1]
                    {
                      context[0]
                    }, (HybridRowExp) null, mod9.tf, out obj10, false);
                  }
                  else
                  {
                    int num20 = (int) task.CalcFormula(new long[1]
                    {
                      context[0]
                    }, (HybridRowExp) null, mod9.tf, out obj10, 0L);
                  }
                  int num21 = -1;
                  if (obj10 != null)
                    num21 = Convert.ToInt32(obj10);
                  for (int startWith = mod9.startWith; startWith <= num21; ++startWith)
                  {
                    task.__SetValue(new CalculatedAttr(new CalcAttrPair(-1L, attributeType2.AttributeID), (object) startWith, AttrState.Unknown));
                    this._Oper(taskId, session, node, context, dTable);
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modLoopSort:
                  ModParmSort mod10 = (ModParmSort) node.mod;
                  if (dTable == null)
                    dTable = this.CollectObjectData(session, task, context, mod10);
                  List<int> colNumList4 = new List<int>();
                  for (int index68 = 0; index68 < mod10.sortAttrs.Count; ++index68)
                  {
                    string sortAttr = mod10.sortAttrs[index68];
                    int indexByName7 = dTable.Columns.GetIndexByName(sortAttr);
                    if (indexByName7 >= 0)
                      colNumList4.Add(indexByName7);
                  }
                  List<HybridRowExp> hybridRowExpList3 = dTable.SortIndex(colNumList4);
                  long[] instance3 = (long[]) Array.CreateInstance(typeof (long), context.Length);
                  for (int index69 = 0; index69 < hybridRowExpList3.Count; ++index69)
                    instance3[index69] = Convert.ToInt64(hybridRowExpList3[index69][0]);
                  HybridTableExp dTable13 = dTable.CloneEmpty();
                  for (int index70 = 0; index70 < hybridRowExpList3.Count; ++index70)
                  {
                    dTable13.ClearRows();
                    dTable13.AddRow(hybridRowExpList3[index70]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) 1);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) instance3[index70]);
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) 0);
                    long[] context18 = new long[1]
                    {
                      instance3[index70]
                    };
                    flag1 = true;
                    this._Oper(taskId, session, node, context18, dTable13);
                    if (task.BreakFlag)
                    {
                      task.BreakFlag = false;
                      break;
                    }
                  }
                  return;
                case ExpertScriptMod.modLoopGroup:
                  ModParmSort mod11 = (ModParmSort) node.mod;
                  if (dTable == null)
                    dTable = this.CollectObjectData(session, task, context, mod11);
                  List<int> colNumList5 = new List<int>();
                  for (int index71 = 0; index71 < mod11.sortAttrs.Count; ++index71)
                  {
                    string sortAttr = mod11.sortAttrs[index71];
                    int indexByName8 = dTable.Columns.GetIndexByName(sortAttr);
                    if (indexByName8 >= 0)
                      colNumList5.Add(indexByName8);
                  }
                  List<HybridRowExp> rows3 = dTable.SortIndex(colNumList5);
                  List<int> intList4 = new List<int>();
                  for (int index72 = 0; index72 < mod11.groupAttrs.Count; ++index72)
                  {
                    string groupAttr = mod11.groupAttrs[index72];
                    for (int index73 = 0; index73 < dTable.Columns.Count; ++index73)
                    {
                      if (groupAttr == dTable.Columns[index73].ColumnName)
                      {
                        intList4.Add(index73);
                        break;
                      }
                    }
                  }
                  int[] array7 = intList4.ToArray();
                  HybridTableExp hybridTableExp3 = dTable.CloneEmpty();
                  int curRow3 = 1;
                  int firstRow3 = 0;
                  List<long> longList8 = new List<long>();
                  for (; curRow3 < rows3.Count; ++curRow3)
                  {
                    if (this.RowDiffers(rows3, firstRow3, curRow3, array7))
                    {
                      hybridTableExp3.ClearRows();
                      for (int index74 = firstRow3; index74 < curRow3; ++index74)
                      {
                        hybridTableExp3.AddRow(rows3[index74]);
                        longList8.Add(Convert.ToInt64(rows3[index74][0]));
                      }
                      long[] array8 = longList8.ToArray();
                      this._Oper(taskId, session, node, array8, hybridTableExp3);
                      longList8.Clear();
                      firstRow3 = curRow3;
                    }
                  }
                  if (firstRow3 >= rows3.Count)
                    return;
                  hybridTableExp3.ClearRows();
                  for (int index75 = firstRow3; index75 < dTable.RowsCount; ++index75)
                  {
                    ExpertServer.CopyRow(hybridTableExp3, rows3[index75]);
                    longList8.Add(Convert.ToInt64(rows3[index75][0]));
                  }
                  long[] array9 = longList8.ToArray();
                  this._Oper(taskId, session, node, array9, hybridTableExp3);
                  if (!task.BreakFlag)
                    return;
                  task.BreakFlag = false;
                  return;
                default:
                  return;
              }
            case ExpertScriptOp.opObjType:
              if (context.Length == 0)
                break;
              OpParmType op4 = (OpParmType) node.op;
              if (op4.objTypeGUID == "")
                break;
              Guid rootTypeGUID = new Guid(op4.objTypeGUID);
              TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(context[0], session);
              if (TaskDataCache.IsEmpty((TypedInfoItem) objData) || !rootTypeGUID.Equals(Guid.Empty) && !ExpertServer.IsTypeDescendant(rootTypeGUID, objData.ObjTypeID) || op4.cond != null && !task.CheckCondOnly(context[0], op4.cond))
                break;
              for (int index76 = 0; index76 < node.Items.Count; ++index76)
              {
                ScriptTreeNode node17 = (ScriptTreeNode) node.Items[index76];
                this.ProcessScriptNode(taskId, node17, context, dTable, false, ref new_context);
              }
              break;
            case ExpertScriptOp.opReturnObject:
              if (context == null || context.Length == 0)
                throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_177"));
              OpParmCond op5 = (OpParmCond) node.op;
              switch (node.modTag)
              {
                case ExpertScriptMod.modUnknown:
                  if (op5.cond != null && !task.CheckRowCond(context[0], dTable?[0], op5.cond))
                    return;
                  throw new EObjectFound(context[0]);
                case ExpertScriptMod.modIfExists:
                  TempFormula tf20 = ((ModParmFormula) node.mod).tf;
                  if (tf20 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_179"));
                  bool flag13 = false;
                  for (int index77 = 0; index77 < context.Length; ++index77)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index77]);
                    if (task.CheckRowCond(context[index77], dTable?[index77], tf20))
                    {
                      flag13 = true;
                      break;
                    }
                    if (op5.cond != null && !task.CheckRowCond(context[index77], dTable?[index77], op5.cond))
                    {
                      flag13 = false;
                      break;
                    }
                  }
                  if (!flag13)
                    return;
                  throw new EObjectFound(context[0]);
                case ExpertScriptMod.modIfAll:
                  TempFormula tf21 = ((ModParmFormula) node.mod).tf;
                  if (tf21 == null)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_178"));
                  bool flag14 = true;
                  for (int index78 = 0; index78 < context.Length; ++index78)
                  {
                    this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) context[index78]);
                    if (!task.CheckRowCond(context[index78], dTable?[index78], tf21))
                    {
                      flag14 = false;
                      break;
                    }
                    if (op5.cond != null && !task.CheckRowCond(context[index78], dTable?[index78], op5.cond))
                    {
                      flag14 = false;
                      break;
                    }
                  }
                  if (!flag14)
                    return;
                  throw new EObjectFound(context[0]);
                default:
                  return;
              }
            case ExpertScriptOp.opGlobRoot:
              this.New_MakeGlobalTable(taskId, ref context, (GlobalNode) node);
              break;
          }
        }
      }
      finally
      {
        if (node.opTag != ExpertScriptOp.opDocSelectElem)
        {
          if (task.lockCurNode != null)
            task.curDocNode = task.lockCurNode;
          else if (task.curDocNode != curDocNode)
            task.curDocNode = curDocNode;
        }
        if (!this.IsTaskClientDead(task))
        {
          this.RestoreCurNode(taskId, curNode);
          if (flag1)
          {
            this.InnerSetParm(task, ExpertConsts.Consts.attrContextCount, (object) num1);
            this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextId, (object) num2);
            this.InnerSetParm(task, ExpertConsts.Consts.attrCurContextNum, (object) num3);
          }
          task.Pop();
        }
      }
    }
  }

  private bool IsLinkAttribute(IDBAttributeType idbAT)
  {
    if (idbAT.AttributeType == FieldTypes.ftObjectLink)
      return true;
    return idbAT.AttributeType == FieldTypes.ftSystem ? idbAT.AttributeID == -6 || idbAT.AttributeID == -8 || idbAT.AttributeID == -14 || idbAT.AttributeID == -15 : idbAT.AttributeID == ExpertConsts.Consts.attrPrevVersionId;
  }

  private bool IsLinkAttribute(FieldTypes ft, int attributeId)
  {
    switch (ft)
    {
      case FieldTypes.ftObjectLink:
        return true;
      case FieldTypes.ftSystem:
        return attributeId == -6 || attributeId == -8 || attributeId == -14 || attributeId == -15 || attributeId == ExpertConsts.Consts.attrNewPrevVersionId;
      default:
        return attributeId == ExpertConsts.Consts.attrPrevVersionId;
    }
  }

  private void Sort(HybridTableExp dt, ExpertServer.ExpServTask ti, object code)
  {
    this.QuickSort(dt, ti, code, 0, dt.RowsCount - 1);
  }

  private void QuickSort(
    HybridTableExp dt,
    ExpertServer.ExpServTask ti,
    object code,
    int L,
    int R)
  {
    if (L >= R)
      return;
    int num = this._Partition(dt, ti, code, L, R);
    this.QuickSort(dt, ti, code, L, num - 1);
    this.QuickSort(dt, ti, code, num + 1, R);
  }

  private int _Partition(
    HybridTableExp dt,
    ExpertServer.ExpServTask ti,
    object code,
    int left,
    int right)
  {
    long int64_1 = Convert.ToInt64(dt[right][0]);
    int index1 = left;
    for (int index = left; index < right; ++index)
    {
      long int64_2 = Convert.ToInt64(dt[index][0]);
      if ((code is long ? ti.InbuiltCompare(int64_2, int64_1, dt[index], dt[right]) : ExpertServer.Compare((string) code, ti, int64_2, int64_1, dt[index], dt[right])) <= 0)
      {
        this._Swap(dt, index1, index);
        ++index1;
      }
    }
    this._Swap(dt, index1, right);
    return index1;
  }

  private void _Swap(HybridTableExp dt, int index1, int index2)
  {
    HybridRowExp hybridRowExp = dt[index1];
    dt[index1] = dt[index2];
    dt[index2] = hybridRowExp;
  }

  private void QuickSort(
    HybridTableExp dt,
    ExpertServer.ExpServTask ti,
    object code,
    List<int> index,
    int L,
    int R)
  {
    int num1 = L;
    int num2 = R;
    int index1 = (L + R) / 2;
    int index2 = index[index1];
    long int64_1 = Convert.ToInt64(dt[index2][0]);
    do
    {
      int index3 = index[num1];
      long int64_2 = Convert.ToInt64(dt[index3][0]);
      if ((code is long ? ti.InbuiltCompare(int64_2, int64_1, dt[index3], dt[index2]) : ExpertServer.Compare((string) code, ti, int64_2, int64_1, dt[index3], dt[index2])) < 0)
      {
        ++num1;
      }
      else
      {
        while (true)
        {
          int index4 = index[num2];
          long int64_3 = Convert.ToInt64(dt[index4][0]);
          if ((code is long ? ti.InbuiltCompare(int64_1, int64_3, dt[index2], dt[index4]) : ExpertServer.Compare((string) code, ti, int64_1, int64_3, dt[index2], dt[index4])) < 0)
            --num2;
          else
            break;
        }
        if (num1 <= num2)
        {
          int num3 = index[num1];
          index[num1] = index[num2];
          index[num2] = num3;
          ++num1;
          --num2;
        }
      }
    }
    while (num1 <= num2);
    if (L < num2)
      this.QuickSort(dt, ti, code, index, L, num2);
    if (num1 >= R)
      return;
    this.QuickSort(dt, ti, code, index, num1, R);
  }

  private void QuickSort(
    HybridTableExp dt,
    ExpertServer.ExpServTask ti,
    List<int> index,
    int L,
    int R,
    CompareFuncHandler cf)
  {
    int num1 = L;
    int num2 = R;
    int index1 = (L + R) / 2;
    int index2 = index[index1];
    long int64_1 = Convert.ToInt64(dt[index2][0]);
    do
    {
      int index3 = index[num1];
      long int64_2 = Convert.ToInt64(dt[index3][0]);
      if (cf((object) ti, int64_2, int64_1, dt[index3], dt[index2]) < 0)
      {
        ++num1;
      }
      else
      {
        while (true)
        {
          int index4 = index[num2];
          long int64_3 = Convert.ToInt64(dt[index4][0]);
          if (cf((object) ti, int64_1, int64_3, dt[index2], dt[index4]) < 0)
            --num2;
          else
            break;
        }
        if (num1 <= num2)
        {
          int num3 = index[num1];
          index[num1] = index[num2];
          index[num2] = num3;
          ++num1;
          --num2;
        }
      }
    }
    while (num1 <= num2);
    if (L < num2)
      this.QuickSort(dt, ti, index, L, num2, cf);
    if (num1 >= R)
      return;
    this.QuickSort(dt, ti, index, num1, R, cf);
  }

  public int CompareIsps(object ti, long objId1, long objId2, HybridRowExp dr1, HybridRowExp dr2)
  {
    ExpertServer.ExpServTask expServTask = (ExpertServer.ExpServTask) ti;
    int indexByName1 = expServTask.savedData.Columns.GetIndexByName(ExpertServer.attrIspCode);
    int indexByName2 = expServTask.savedData.Columns.GetIndexByName("cad0001f-306c-11d8-b4e9-00304f19f545");
    IDBObject dbObject1 = (IDBObject) null;
    IDBObject dbObject2 = (IDBObject) null;
    IUserSession userSession = (IUserSession) null;
    Guid attributeGuid1 = new Guid(ExpertServer.attrIspCode);
    Guid attributeGuid2 = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
    bool flag1 = false;
    string strA = "";
    string strB = "";
    if (indexByName1 != -1)
    {
      strA = Convert.ToString(dr1[indexByName1]);
      strB = Convert.ToString(dr2[indexByName1]);
      flag1 = true;
    }
    else
    {
      userSession = ExpertServer.es.GetSession((ExpertServer.ExpServTask) ti);
      dbObject1 = userSession.GetObject(objId1, false);
      dbObject2 = userSession.GetObject(objId2, false);
      if (dbObject1 != null && dbObject2 != null)
      {
        IDBAttribute attributeByGuid1 = dbObject1.GetAttributeByGuid(attributeGuid1);
        bool flag2 = attributeByGuid1 != null;
        if (attributeByGuid1 != null)
          strA = attributeByGuid1.AsString;
        IDBAttribute attributeByGuid2 = dbObject2.GetAttributeByGuid(attributeGuid1);
        flag1 = flag2 && attributeByGuid2 != null;
        if (attributeByGuid2 != null)
          strB = attributeByGuid2.AsString;
      }
    }
    if (flag1)
      return string.Compare(strA, strB);
    if (indexByName2 != -1)
    {
      strA = Convert.ToString(dr1[indexByName2]);
      strB = Convert.ToString(dr2[indexByName2]);
    }
    else
    {
      if (userSession == null)
        userSession = ExpertServer.es.GetSession((ExpertServer.ExpServTask) ti);
      if (dbObject1 == null)
        dbObject1 = userSession.GetObject(objId1, false);
      if (dbObject2 == null)
        dbObject2 = userSession.GetObject(objId2, false);
      if (dbObject1 != null && dbObject2 != null)
      {
        IDBAttribute attributeByGuid3 = dbObject1.GetAttributeByGuid(attributeGuid2);
        if (attributeByGuid3 != null)
          strA = attributeByGuid3.AsString;
        IDBAttribute attributeByGuid4 = dbObject2.GetAttributeByGuid(attributeGuid2);
        if (attributeByGuid4 != null)
          strB = attributeByGuid4.AsString;
      }
    }
    if (!(strA != "") || !(strB != ""))
      return 0;
    int num;
    for (num = 0; num < strA.Length && num < strB.Length; ++num)
    {
      if ((int) strA[num] != (int) strB[num])
      {
        --num;
        break;
      }
    }
    if (num > 0)
    {
      string str1 = strA.Remove(0, num);
      string str2 = strB.Remove(0, num);
      strA = str1.TrimStart('-');
      strB = str2.TrimStart('-');
      try
      {
        return Convert.ToInt32(strA) - Convert.ToInt32(strB);
      }
      catch
      {
      }
    }
    return string.Compare(strA, strB);
  }

  private void SortIsps(ExpertServer.ExpServTask ti)
  {
    if (ti.ispList == null || ti.ispList.Count <= 1)
      return;
    List<int> index1 = new List<int>();
    foreach (long isp in ti.ispList)
    {
      int num = ti.savedDataByObjIdIndex(isp);
      index1.Add(num);
    }
    this.QuickSort(ti.savedData, ti, index1, 0, index1.Count - 1, new CompareFuncHandler(this.CompareIsps));
    ti.ispList.Clear();
    for (int index2 = 0; index2 < index1.Count; ++index2)
    {
      HybridRowExp hybridRowExp = ti.savedData[index1[index2]];
      ti.ispList.Add(Convert.ToInt64(hybridRowExp[0]));
    }
  }

  private bool SelByRelation(
    List<ConditionStructure> conds,
    List<ColumnDescriptor> descs,
    out List<int> typeIdList,
    bool Multi,
    IUserSession ius)
  {
    bool flag1 = Multi;
    typeIdList = new List<int>();
    if (!flag1)
    {
      foreach (ColumnDescriptor desc in descs)
      {
        flag1 = desc.AttributeSource == AttributeSourceTypes.Relation;
        if (flag1)
          break;
      }
      if (!flag1)
      {
        foreach (ConditionStructure cond in conds)
        {
          flag1 = cond.AttributeSource == AttributeSourceTypes.Relation;
          if (flag1)
            break;
        }
      }
    }
    List<int> collection = new List<int>();
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    for (int index = 0; index < conds.Count; ++index)
    {
      ConditionStructure cond = conds[index];
      if (cond.Attribute == null)
      {
        if (cond.RelationalOperator == RelationalOperators.EntersIn || cond.RelationalOperator == RelationalOperators.ConsistFrom)
        {
          int int32 = Convert.ToInt32(cond.TypeID);
          if (!collection.Contains(int32))
            collection.Add(int32);
        }
      }
      else if (cond.Attribute is int)
      {
        ObligatoryObjectAttributes attribute = (ObligatoryObjectAttributes) cond.Attribute;
        if (attribute == ObligatoryObjectAttributes.F_RELATION_TYPE)
        {
          if (cond.RelationalOperator == RelationalOperators.Equal)
          {
            int int32 = Convert.ToInt32(cond.Value);
            if (!collection.Contains(int32))
              collection.Add(int32);
          }
          if (cond.RelationalOperator == RelationalOperators.In)
          {
            foreach (int num in (int[]) cond.Value)
            {
              if (!collection.Contains(num))
                collection.Add(num);
            }
          }
        }
        if (attribute == ObligatoryObjectAttributes.F_OBJECT_TYPE)
        {
          if (cond.RelationalOperator == RelationalOperators.Equal)
          {
            int int32 = Convert.ToInt32(cond.Value);
            if (!intList1.Contains(int32))
              intList1.Add(int32);
          }
          if (cond.RelationalOperator == RelationalOperators.In)
          {
            foreach (int num in (int[]) cond.Value)
            {
              if (!intList1.Contains(num))
                intList1.Add(num);
            }
          }
          if (Multi)
          {
            cond.SQL = "X";
            conds[index] = cond;
          }
        }
      }
      else
      {
        if (cond.AttributeSource == AttributeSourceTypes.Object & Multi)
        {
          cond.SQL = "X";
          conds[index] = cond;
        }
        if (cond.Attribute.ToString() == "cad0002e-306c-11d8-b4e9-00304f19f545")
        {
          if (cond.RelationalOperator == RelationalOperators.Equal)
          {
            int int32 = Convert.ToInt32(cond.Value);
            if (!intList1.Contains(int32))
              intList1.Add(int32);
          }
          if (cond.RelationalOperator == RelationalOperators.In)
          {
            foreach (int num in (int[]) cond.Value)
            {
              if (!intList1.Contains(num))
                intList1.Add(num);
            }
          }
          cond.AttributeSource = AttributeSourceTypes.Object;
          conds[index] = cond;
        }
        if (cond.Attribute.ToString() == "cad00036-306c-11d8-b4e9-00304f19f545")
        {
          if (cond.RelationalOperator == RelationalOperators.Equal)
          {
            int int32 = Convert.ToInt32(cond.Value);
            if (!collection.Contains(int32))
              collection.Add(int32);
          }
          if (cond.RelationalOperator == RelationalOperators.In)
          {
            foreach (int num in (int[]) cond.Value)
            {
              if (!collection.Contains(num))
                collection.Add(num);
            }
          }
          cond.AttributeSource = AttributeSourceTypes.Relation;
          conds[index] = cond;
        }
      }
    }
    int index1 = 0;
    bool flag2 = false;
    bool flag3 = false;
    while (index1 < conds.Count)
    {
      ConditionStructure cond = conds[index1];
      if (cond.Attribute == null && (cond.RelationalOperator == RelationalOperators.EntersIn || cond.RelationalOperator == RelationalOperators.ConsistFrom))
      {
        if (flag1 | flag2 || collection.Count > 1)
        {
          DbHelper.DeleteCond(conds, index1);
          ConditionStructure ncs = new ConditionStructure(-21, RelationalOperators.Equal, (object) -1, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation, ColumnContents.ID);
          ncs.SQL = "*";
          if (cond.RelationalOperator == RelationalOperators.ConsistFrom)
            ncs.Attribute = (object) -22;
          this.AddCondStru(conds, ncs);
          continue;
        }
        flag2 = true;
      }
      if (cond.Attribute is int && (int) cond.Attribute == -23 || cond.Attribute is Guid && cond.Attribute.ToString() == "cad00036-306c-11d8-b4e9-00304f19f545")
        DbHelper.DeleteCond(conds, index1);
      if (cond.Attribute is int && (int) cond.Attribute == -7 || cond.Attribute is Guid && cond.Attribute.ToString() == "cad0002e-306c-11d8-b4e9-00304f19f545")
      {
        if (flag3)
        {
          DbHelper.DeleteCond(conds, index1);
          continue;
        }
        if (intList1.Count > 1)
        {
          cond.RelationalOperator = RelationalOperators.In;
          cond.Value = (object) intList1.ToArray();
        }
        else
        {
          cond.RelationalOperator = RelationalOperators.Equal;
          cond.Value = (object) Convert.ToInt32(intList1[0]);
        }
        if (Multi)
          cond.SQL = "X";
        conds[index1] = cond;
        flag3 = true;
      }
      ++index1;
    }
    if (collection.Count > 0)
    {
      int num1 = flag2 ? 1 : 0;
    }
    if (intList1.Count > 0 && !flag3)
    {
      ConditionStructure ncs = new ConditionStructure(-7, RelationalOperators.Equal, (object) -1, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value);
      if (intList1.Count > 1)
      {
        ncs.RelationalOperator = RelationalOperators.In;
        ncs.Value = (object) intList1.ToArray();
      }
      else
        ncs.Value = (object) Convert.ToInt32(intList1[0]);
      if (Multi)
        ncs.SQL = "X";
      this.AddCondStru(conds, ncs);
    }
    if (flag1 && collection.Count > 0)
      typeIdList.AddRange((IEnumerable<int>) collection);
    if (typeIdList.Count > 0)
    {
      for (int index2 = 0; index2 < conds.Count; ++index2)
      {
        ConditionStructure cond = conds[index2];
        if (flag1 && (cond.Attribute is int && (int) cond.Attribute == -23 || cond.Attribute is Guid && cond.Attribute.ToString() == "cad00036-306c-11d8-b4e9-00304f19f545"))
        {
          DbHelper.DeleteCond(conds, index2);
          break;
        }
      }
    }
    if (Multi)
    {
      for (int index3 = 0; index3 < conds.Count; ++index3)
      {
        ConditionStructure cond = conds[index3];
        if (cond.SQL == "X")
        {
          if (cond.Attribute is int)
          {
            cond.Attribute = (object) MetaDataHelper.GetAttributeTypeGuid((int) cond.Attribute);
            conds[index3] = cond;
          }
          if (cond.Attribute is Guid)
          {
            Guid attribute = (Guid) cond.Attribute;
            bool flag4 = false;
            foreach (ColumnDescriptor desc in descs)
            {
              if (desc.AttributeID is Guid && attribute.Equals(desc.AttributeID))
              {
                flag4 = true;
                break;
              }
              if (desc.AttributeID is ObligatoryObjectAttributes)
              {
                Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(Convert.ToInt32(desc.AttributeID));
                if (attribute.Equals(attributeTypeGuid))
                {
                  flag4 = true;
                  break;
                }
              }
            }
            if (!flag4)
            {
              bool measured = false;
              try
              {
                ColumnContents columnContents = DbHelper.GetColumnContents(attribute.ToString(), out measured);
                if (columnContents != ColumnContents.Date)
                {
                  ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) attribute, cond.AttributeSource, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : descs.Count + 1);
                  descs.Add(columnDescriptor);
                }
              }
              catch
              {
              }
            }
          }
        }
      }
    }
    return flag1;
  }

  private ConditionStructure GetLinkTypeConds(OpParmObject op)
  {
    int[] conditionValue = new int[op.linkTypeIDs.Count];
    for (int index = 0; index < op.linkTypeIDs.Count; ++index)
      conditionValue[index] = Convert.ToInt32(op.linkTypeIDs[index]);
    return new ConditionStructure(-23, RelationalOperators.In, (object) conditionValue, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value);
  }

  private void FilterByObjTypes(HybridTableExp dt, OpParmObject op)
  {
    if (op.objTypeIDs == null || op.objTypeIDs.Count <= 0)
      return;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) this.GetOpObjTypes(op));
    for (int index = dt.RowsCount - 1; index >= 0; --index)
    {
      int int32 = Convert.ToInt32(dt[index]["cad0002e-306c-11d8-b4e9-00304f19f545"]);
      if (!childrenIdRecursive.Contains(int32))
        dt.RemoveAt(index);
    }
  }

  private List<int> GetOpObjTypes(OpParmObject op)
  {
    if (op == null || op.objTypeIDs == null)
      return new List<int>();
    List<int> opObjTypes = new List<int>(op.objTypeIDs.Count);
    for (int index = 0; index < op.objTypeIDs.Count; ++index)
      opObjTypes.Add(Convert.ToInt32(op.objTypeIDs[index]));
    return opObjTypes;
  }

  private ConditionStructure GetObjTypeCond(OpParmObject op)
  {
    int[] objTypes = new int[op.objTypeIDs.Count];
    for (int index = 0; index < op.objTypeIDs.Count; ++index)
      objTypes[index] = Convert.ToInt32(op.objTypeIDs[index]);
    return this.GetObjTypeCond(objTypes);
  }

  private ConditionStructure GetObjTypeCond(int[] objTypes)
  {
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    HashSet<int> source = (HashSet<int>) null;
    for (int index = 0; index < objTypes.Length; ++index)
    {
      HashSet<int> childObjectTypes = ExpertServer.es.GetChildObjectTypes(objTypes[index]);
      if (source == null)
        source = childObjectTypes;
      else
        source.UnionWith((IEnumerable<int>) childObjectTypes);
    }
    return new ConditionStructure(-7, RelationalOperators.In, (object) source.ToArray<int>(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Value);
  }

  private void CopyColumns(HybridTableExp dst, HybridTableExp src, ColumnDescriptor[] descs)
  {
    if (src == null)
      return;
    List<HybridColumnsExp.HybridColumnExp> colList = new List<HybridColumnsExp.HybridColumnExp>();
    for (int index = 0; index < src.Columns.Count; ++index)
    {
      HybridColumnsExp.HybridColumnExp column = src.Columns[index];
      HybridColumnsExp.HybridColumnExp hybridColumnExp = new HybridColumnsExp.HybridColumnExp(column.ColumnName, column.DataType);
      if (descs != null && index < descs.Length && descs[index].AttributeID is Guid)
        hybridColumnExp.ColumnName = Convert.ToString(descs[index].AttributeID);
      colList.Add(hybridColumnExp);
    }
    dst.AddColumns(colList);
  }

  private string[] AddColumns(HybridTableExp dst, HybridTableExp src)
  {
    string[] strArray = new string[0];
    if (src == null)
      return strArray;
    List<HybridColumnsExp.HybridColumnExp> colList = new List<HybridColumnsExp.HybridColumnExp>();
    List<string> stringList = new List<string>();
    for (int index = 0; index < src.Columns.Count; ++index)
    {
      HybridColumnsExp.HybridColumnExp column = src.Columns[index];
      if (!dst.Columns.Contains(column.ColumnName))
      {
        colList.Add(column);
        stringList.Add(column.ColumnName);
      }
    }
    dst.AddColumns(colList);
    return stringList.ToArray();
  }

  private void AddColumns(HybridTableExp dst, HybridColumnsExp.HybridColumnExp[] cols)
  {
    List<HybridColumnsExp.HybridColumnExp> colList = new List<HybridColumnsExp.HybridColumnExp>();
    for (int index = 0; index < cols.Length; ++index)
    {
      HybridColumnsExp.HybridColumnExp col = cols[index];
      if (!dst.Columns.Contains(col.ColumnName))
        colList.Add(col);
    }
    dst.AddColumns(colList);
  }

  private bool AddNewColumns(HybridTableExp dst, HybridTableExp src)
  {
    if (dst.Columns.Count == src.Columns.Count)
      return false;
    List<HybridColumnsExp.HybridColumnExp> colList = new List<HybridColumnsExp.HybridColumnExp>();
    for (int count = dst.Columns.Count; count < src.Columns.Count; ++count)
    {
      HybridColumnsExp.HybridColumnExp column = src.Columns[count];
      if (!dst.Columns.Contains(column.ColumnName))
        colList.Add(column);
    }
    dst.AddColumns(colList);
    return true;
  }

  private bool RowDiffers(List<HybridRowExp> rows, int firstRow, int curRow, int[] g_cols)
  {
    for (int index = 0; index < g_cols.Length; ++index)
    {
      if (!rows[firstRow][g_cols[index]].Equals(rows[curRow][g_cols[index]]))
        return true;
    }
    return false;
  }

  private void AddCondStru(List<ConditionStructure> tmp, ConditionStructure ncs)
  {
    if (tmp.Count > 0)
    {
      ConditionStructure conditionStructure = tmp[tmp.Count - 1] with
      {
        LogicalOperator = LogicalOperators.AND
      };
      tmp[tmp.Count - 1] = conditionStructure;
    }
    tmp.Add(ncs);
  }

  private void AddCondStru(List<ConditionStructure> tmp, List<ConditionStructure> condList)
  {
    if (tmp.Count > 0)
    {
      ConditionStructure conditionStructure = tmp[tmp.Count - 1] with
      {
        LogicalOperator = LogicalOperators.AND
      };
      tmp[tmp.Count - 1] = conditionStructure;
    }
    foreach (ConditionStructure cond in condList)
      tmp.Add(cond);
  }

  private ISelectionsService GetSS(int taskId)
  {
    ISelectionsService service = (ISelectionsService) this._serviceProvider.GetService(typeof (ISelectionsService));
    if (service != null)
      return service;
    string str = LocalizationHolder.rm.GetString("Expert.Server_180");
    this.ReportError(taskId, str);
    throw new ExpertServerException(str);
  }

  private bool MarkObjDependent(List<ConditionStructure> conds, long objId)
  {
    bool flag = false;
    for (int index = 0; index < conds.Count; ++index)
    {
      ConditionStructure cond = conds[index];
      if (cond.Value is long && Convert.ToInt64(cond.Value) == objId)
      {
        cond.SQL = "*";
        conds[index] = cond;
        flag = true;
      }
    }
    return flag;
  }

  private void SetObjDependent(ConditionStructure[] conds, long objId)
  {
    if (conds == null)
      return;
    for (int index = 0; index < conds.Length; ++index)
    {
      ConditionStructure cond = conds[index];
      if (cond.SQL == "*")
      {
        cond.SQL = string.Empty;
        cond.Value = (object) objId;
        conds[index] = cond;
      }
    }
  }

  private void DivideConds(List<ConditionStructure> conds, List<ConditionStructure> objConds)
  {
    int index = 0;
    while (index < conds.Count)
    {
      ConditionStructure cond = conds[index];
      if (cond.SQL == "X")
      {
        cond.SQL = "";
        cond.GroupID = 0;
        cond.LogicalOperator = LogicalOperators.NONE;
        this.AddCondStru(objConds, cond);
        conds.RemoveAt(index);
      }
      else
        ++index;
    }
  }

  private void ReplaceObjTypeFilters(ConditionStructure[] structs)
  {
    for (int index = 0; index < structs.Length; ++index)
    {
      ConditionStructure conditionStructure = structs[index];
      if (conditionStructure.RelationalOperator == RelationalOperators.ObjectTypeFilter && conditionStructure.Value is int)
      {
        conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_TYPE;
        HashSet<int> intSet = new HashSet<int>();
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(Convert.ToInt32(conditionStructure.Value));
        conditionStructure.RelationalOperator = RelationalOperators.In;
        conditionStructure.Value = (object) childrenIdRecursive.ToArray();
        structs[index] = conditionStructure;
      }
    }
  }

  private List<long> ExecuteExcerpt(
    int taskId,
    long[] context,
    ExpertScriptOp opTag,
    ExpertScriptMod modTag,
    OpParmObject op,
    ModParm mod,
    out HybridTableExp dt)
  {
    ISelectionsService ss = this.GetSS(taskId);
    dt = new HybridTableExp();
    List<long> longList = new List<long>();
    if (context.Length == 0)
      return longList;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    IUserSession session = this.GetSession(task);
    bool onlyBack = opTag == ExpertScriptOp.opObjParents || opTag == ExpertScriptOp.opObjAncestors;
    List<ColumnDescriptor> columnDescriptorList = task.curScrType == ExpertScriptType.RecalcScript ? this.GenRecalcColumnDescriptors() : this.GenerateColumnDescriptors(session, op, mod as ModParmSort, onlyBack);
    ColumnDescriptor[] descs = (ColumnDescriptor[]) null;
    if (!op.NoSearch && !task.forceSearchByGlobal)
    {
      this.InitRelTable(session, task, op, columnDescriptorList);
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      try
      {
        conditionStructureList.Clear();
        try
        {
          long objectID = Math.Abs(context[0]);
          ConditionStructure[] conditionStructures = ss.GetConditionStructures((object) ExpertServer.GetSessionGuid(task), op.excerptID, objectID);
          conditionStructureList.AddRange((IEnumerable<ConditionStructure>) conditionStructures);
        }
        catch (Exception ex)
        {
          throw new ExpertServerException(string.Format($"{LocalizationHolder.rm.GetString("Expert.Server_200")}: {ex.Message}", (object) op.excerptID), ex);
        }
        if (op.linkTypeIDs != null && op.linkTypeIDs.Count > 0)
        {
          ConditionStructure linkTypeConds = this.GetLinkTypeConds(op);
          this.AddCondStru(conditionStructureList, linkTypeConds);
        }
        if (op.objTypeIDs != null)
        {
          if (op.objTypeIDs.Count > 0)
          {
            ConditionStructure objTypeCond = this.GetObjTypeCond(op);
            this.AddCondStru(conditionStructureList, objTypeCond);
          }
        }
      }
      catch (Exception ex)
      {
        this.ReportError(taskId, ex.Message);
        throw;
      }
      this.MarkObjDependent(conditionStructureList, context[0]);
      List<int> typeIdList = (List<int>) null;
      bool flag = this.SelByRelation(conditionStructureList, columnDescriptorList, out typeIdList, false, session);
      if (op.Dups && !flag)
        ;
      ColumnDescriptor[] array1 = DataHelper.CombineColumnsDescrs(columnDescriptorList.ToArray(), task.DataCache.GetCacheObjOnlyColumnList(TaskDataCache.ColumnsMode.SystemOnly).ToArray(), AttributeSourceTypes.Object).ToArray();
      for (int index1 = 0; index1 < context.Length; ++index1)
      {
        long isp1 = context[index1];
        if (op.ispWork == IspMode.ispCurrentOnly && task.currentIsp != -1)
          isp1 = task.ispList[task.currentIsp];
        if (onlyBack && task.curRelationId != 0L)
          conditionStructureList.Insert(0, new ConditionStructure(-20, RelationalOperators.Equal, (object) task.curRelationId, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value));
        ConditionStructure[] array2 = conditionStructureList.ToArray();
        this.SetObjDependent(array2, isp1);
        if (typeIdList.Count == 0)
          typeIdList.Add(-1);
        DataTable dataTable;
        if (opTag == ExpertScriptOp.opObjLinked)
        {
          this.ReplaceObjTypeFilters(array2);
          dataTable = DataHelper.GetObjectData(-1, session, (IEnumerable<ConditionStructure>) array2, (IEnumerable<ColumnDescriptor>) array1);
        }
        else
          dataTable = !onlyBack ? this.GetSostavData(isp1, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array2, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId) : this.GetPSostavData(isp1, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array2, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
        if (onlyBack && task.curRelationId != 0L && (dataTable == null || dataTable.Rows.Count == 0))
        {
          conditionStructureList.RemoveAt(0);
          ConditionStructure[] array3 = conditionStructureList.ToArray();
          this.SetObjDependent(array3, isp1);
          dataTable = DataHelper.GetParentSostavData(isp1, session, (IEnumerable<int>) typeIdList.ToArray(), false, (IEnumerable<ConditionStructure>) array3, (IEnumerable<ColumnDescriptor>) array1, task.filtr());
        }
        if (dataTable != null)
        {
          task.DataCache.FillCacheData(dataTable);
          HybridTableExp hybridTableExp1 = new HybridTableExp(dataTable, makeIndex: true);
          if (op.ispWork != IspMode.ispNone)
          {
            if (!op.useCurrentIsps)
            {
              ISubstitutesService service = (ISubstitutesService) this._serviceProvider.GetService(typeof (ISubstitutesService));
              task.app = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(task), task.verRuleOwnerId, isp1, typeIdList[0], AVSSpecificationForm.A);
              for (int index2 = 1; index2 < typeIdList.Count; ++index2)
              {
                ArticlesPartsPackage andVariableParts = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(task), task.verRuleOwnerId, isp1, typeIdList[index2], AVSSpecificationForm.A);
                task.app.MergeWith(andVariableParts);
              }
              long[] withoutFiltration = ((IArticleService) this._serviceProvider.GetService(typeof (IArticleService))).FindArticlesByGroupIDWithoutFiltration(isp1, (object) session.SessionGUID);
              task.ispList = new List<long>((IEnumerable<long>) withoutFiltration);
              task.ispNameList = new List<string>();
              foreach (long isp2 in task.ispList)
              {
                List<long> articleVariablePart = task.app.GetArticleVariablePart(isp2);
                if (articleVariablePart != null && articleVariablePart.Count > 0)
                  task.HasVariableParts = true;
                IDBAttribute dbAttribute = (IDBAttribute) null;
                IDBObject dbObject = session.GetObject(isp2, false);
                if (dbObject != null)
                  dbAttribute = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
                if (dbAttribute != null)
                  task.ispNameList.Add(Convert.ToString(dbAttribute.Value));
                else
                  task.ispNameList.Add("");
              }
              this.SortIsps(task);
              this.MakePrimaryIspFirst(session, task);
              this.InnerSetParm(task, ExpertConsts.Consts.attrIsLink, (object) task.HasVariableParts);
              this.InnerSetParm(task, ExpertConsts.Consts.attrIspList, (object) task.ispList);
              this.InnerSetParm(task, ExpertConsts.Consts.attrIspNum, (object) task.ispList.Count);
            }
            if (task.ispList.Count > 1)
            {
              switch (op.ispWork)
              {
                case IspMode.ispCommonPart:
                  List<long> articleCommonPart = task.app.GetArticleCommonPart(isp1);
                  for (int index3 = hybridTableExp1.RowsCount - 1; index3 >= 0; --index3)
                  {
                    long int64 = Convert.ToInt64(hybridTableExp1[index3]["cad00033-306c-11d8-b4e9-00304f19f545"]);
                    if (!articleCommonPart.Contains(int64))
                      hybridTableExp1.RemoveAt(index3);
                  }
                  break;
                case IspMode.ispCurrentOnly:
                  if (task.currentIsp != -1)
                  {
                    List<long> articleVariablePart = task.app.GetArticleVariablePart(task.ispList[task.currentIsp]);
                    for (int index4 = hybridTableExp1.RowsCount - 1; index4 >= 0; --index4)
                    {
                      long int64 = Convert.ToInt64(hybridTableExp1[index4]["cad00033-306c-11d8-b4e9-00304f19f545"]);
                      if (!articleVariablePart.Contains(int64))
                        hybridTableExp1.RemoveAt(index4);
                    }
                    break;
                  }
                  break;
                case IspMode.ispAll:
                  using (List<long>.Enumerator enumerator = task.ispList.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      long current = enumerator.Current;
                      if (Math.Abs(current) != Math.Abs(isp1))
                      {
                        ConditionStructure[] array4 = conditionStructureList.ToArray();
                        this.SetObjDependent(array4, current);
                        DataTable sostavData = this.GetSostavData(current, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array4, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
                        if (sostavData != null)
                        {
                          this.RemoveNonGuidColumns(sostavData);
                          task.DataCache.FillCacheData(sostavData);
                          if (sostavData.Rows.Count > 0)
                          {
                            HybridTableExp hybridTableExp2 = new HybridTableExp(sostavData);
                            for (int index5 = 0; index5 < hybridTableExp2.RowsCount; ++index5)
                            {
                              HybridRowExp row = hybridTableExp2[index5];
                              ExpertServer.CopyRow(hybridTableExp1, row);
                            }
                          }
                        }
                      }
                    }
                    break;
                  }
              }
            }
          }
          HybridDictionary tags = task.filtr();
          this.UseOnlyPrimaryStructure(ref tags, task.useAllZamens, task.clientAllZamens);
          this.ProcessDataTable(session, task, dt, hybridTableExp1, array1, op, mod);
        }
      }
    }
    else
    {
      if (op.UseWholeTable && task.savedData != null)
      {
        dt = (HybridTableExp) task.savedData.CloneShallow();
        this.FilterByObjTypes(dt, op);
        this.AddAdditionalColumns(dt, task, op, session, (HashSet<long>) null);
        for (int index = 0; index < dt.RowsCount; ++index)
        {
          HybridRowExp hybridRowExp = dt[index];
          longList.Add(Convert.ToInt64(hybridRowExp[0]));
        }
        return longList;
      }
      for (int index = 0; index < context.Length; ++index)
      {
        long id = context[index];
        if (onlyBack)
        {
          TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(id, session);
          if ((TypedInfoItem) objData != (TypedInfoItem) null)
            id = objData.Id;
        }
        HybridTableExp resData = this.SearchByGlobal(session, task, opTag, id, (TempFormula) null, op.ispWork, op.linkTypeIDs);
        this.ProcessDataTable(session, task, dt, resData, descs, op, mod);
      }
      this.FilterByObjTypes(dt, op);
    }
    this.CheckDT(session, ref dt, columnDescriptorList);
    this.UseGlobalTable(session, task, dt, op);
    if (op.AddThis)
      this.AddThis(session, dt, task, context[0], op.ispWork, op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet);
    for (int index = 0; index < dt.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = dt[index];
      longList.Add(Convert.ToInt64(hybridRowExp[0]));
    }
    return longList;
  }

  private void SortIspsAnton(ExpertServer.ExpServTask ti)
  {
    lock (ti)
    {
      if (ti.docScriptId == 0L)
        return;
      List<int> index1 = new List<int>(ti.ispList.Count);
      foreach (long isp in ti.ispList)
      {
        int num = ti.savedDataByObjIdIndex(isp);
        index1.Add(num);
      }
      ti.BeforeSorting((List<long>) null);
      this.QuickSort(ti.savedData, ti, (object) ti.docScriptId, index1, 0, index1.Count - 1);
      List<long> longList = new List<long>(ti.ispList.Count);
      for (int index2 = 0; index2 < index1.Count; ++index2)
      {
        int index3 = index1[index2];
        long int64 = Convert.ToInt64(ti.savedData[index3][0]);
        longList.Add(int64);
      }
      ti.ispList = longList;
    }
  }

  private List<long> ExecuteObjSelect(
    int taskId,
    long[] context,
    ExpertScriptOp opTag,
    ExpertScriptMod modTag,
    OpParmObject op,
    ModParm mod,
    out HybridTableExp dt)
  {
    dt = new HybridTableExp();
    List<long> longList = new List<long>();
    if (context.Length == 0)
      return longList;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    IUserSession session = this.GetSession(task);
    bool onlyBack = opTag == ExpertScriptOp.opObjParents || opTag == ExpertScriptOp.opObjAncestors;
    List<ColumnDescriptor> columnDescriptorList = task.curScrType == ExpertScriptType.RecalcScript ? this.GenRecalcColumnDescriptors() : this.GenerateColumnDescriptors(session, op, mod as ModParmSort, onlyBack);
    ColumnDescriptor[] descs = (ColumnDescriptor[]) null;
    MeasuredValue measuredValue = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
    if (!op.NoSearch && !task.forceSearchByGlobal)
    {
      this.InitRelTable(session, task, op, columnDescriptorList);
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if (op.objTypeIDs != null && op.objTypeIDs.Count > 0)
      {
        ConditionStructure objTypeCond = this.GetObjTypeCond(op);
        this.AddCondStru(conditionStructureList, objTypeCond);
      }
      if (op.linkTypeIDs != null && op.linkTypeIDs.Count > 0)
      {
        ConditionStructure linkTypeConds = this.GetLinkTypeConds(op);
        this.AddCondStru(conditionStructureList, linkTypeConds);
      }
      List<int> typeIdList = (List<int>) null;
      this.SelByRelation(conditionStructureList, columnDescriptorList, out typeIdList, false, session);
      this.MarkObjDependent(conditionStructureList, context[0]);
      List<ConditionStructure> objConds = new List<ConditionStructure>();
      this.DivideConds(conditionStructureList, objConds);
      if (op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet)
      {
        this.AddAttribute(columnDescriptorList, op, new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"));
        this.AddAttribute(columnDescriptorList, op, new Guid(ExpertAttrGUIDs.attrSorting));
        ColumnDescriptor columnDescriptor = columnDescriptorList[0] with
        {
          Sort = SortOrders.NONE
        };
        columnDescriptorList[0] = columnDescriptor;
      }
      ColumnDescriptor[] array1 = DataHelper.CombineColumnsDescrs(columnDescriptorList.ToArray(), task.DataCache.GetCacheColumns(TaskDataCache.ColumnsMode.SystemOnly), AttributeSourceTypes.Auto).ToArray();
      bool _settingGlobalTable = op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet;
      if (_settingGlobalTable && context.Length == 1 && !this.OptModTag(modTag))
      {
        long objId = context[0];
        ConditionStructure[] array2 = conditionStructureList.ToArray();
        this.SetObjDependent(array2, objId);
        HybridDictionary tags = task.filtr();
        this.UseOnlyPrimaryStructure(ref tags, task.useAllZamens, task.clientAllZamens);
        if (typeIdList.Count == 0)
          typeIdList.Add(-1);
        DataTable dataTable = onlyBack ? this.GetPSostavData(objId, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array2, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId) : this.GetSostavData(objId, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array2, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
        this.RemoveNonGuidColumns(dataTable);
        if (dataTable != null)
        {
          task.DataCache.FillCacheData(dataTable);
          HybridTableExp hybridTableExp = new HybridTableExp(dataTable, makeIndex: true);
          this.PerformIsps(hybridTableExp, session, task, op, objId, typeIdList, conditionStructureList, array1, true);
          this.Add2SavedLinks(task, hybridTableExp);
          this.CopyColumns(dt, hybridTableExp, array1);
          Hashtable hashtable = new Hashtable();
          for (int index = 0; index < hybridTableExp.RowsCount; ++index)
          {
            HybridRowExp row = hybridTableExp[index];
            long int64 = Convert.ToInt64(row[0]);
            if (op.Dups || !hashtable.ContainsKey((object) int64))
            {
              ExpertServer.CopyRow(dt, row);
              if (!hashtable.ContainsKey((object) int64))
                hashtable.Add((object) int64, (object) null);
            }
          }
          List<int> opObjTypes = this.GetOpObjTypes(op);
          this.FilterDataTable(task, session, dt, opObjTypes, array1, op.cond, _settingGlobalTable);
        }
      }
      else
      {
        for (int index1 = 0; index1 < context.Length; ++index1)
        {
          long num = context[index1];
          long objId = -1;
          objId = num;
          if (op.ispWork == IspMode.ispCurrentOnly && task.currentIsp != -1)
            objId = task.ispList[task.currentIsp];
          bool flag = session.GetObject(objId, false) == null;
          if (onlyBack & flag && task.curRelationId != 0L)
          {
            conditionStructureList.Insert(0, new ConditionStructure(-20, RelationalOperators.Equal, (object) task.curRelationId, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value));
            this.ConvertRelIdToObjId(task, session, ref objId);
          }
          ConditionStructure[] array3 = conditionStructureList.ToArray();
          this.SetObjDependent(array3, objId);
          HybridDictionary tags = task.filtr();
          this.UseOnlyPrimaryStructure(ref tags, task.useAllZamens, task.clientAllZamens);
          if (typeIdList.Count == 0)
            typeIdList.Add(-1);
          DataTable dataTable = onlyBack ? this.GetPSostavData(objId, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array3, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId) : this.GetSostavData(objId, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array3, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
          if (flag && task.curRelationId != 0L && (dataTable == null || dataTable.Rows.Count == 0))
          {
            if (onlyBack)
            {
              conditionStructureList.RemoveAt(0);
              ConditionStructure[] array4 = conditionStructureList.ToArray();
              this.SetObjDependent(array4, objId);
              dataTable = DataHelper.GetParentSostavData(objId, session, (IEnumerable<int>) typeIdList.ToArray(), false, (IEnumerable<ConditionStructure>) array4, (IEnumerable<ColumnDescriptor>) array1, task.filtr());
            }
            else if (objId == task.curRelationId)
            {
              this.ConvertRelIdToObjId(task, session, ref objId);
              ConditionStructure[] array5 = conditionStructureList.ToArray();
              this.SetObjDependent(array5, objId);
              dataTable = this.GetSostavData(objId, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array5, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
            }
          }
          HybridTableExp resData = (HybridTableExp) null;
          if (dataTable != null)
          {
            this.RemoveNonGuidColumns(dataTable);
            task.DataCache.FillCacheData(dataTable);
            resData = new HybridTableExp(dataTable, makeIndex: true);
          }
          if (op.ispWork != IspMode.ispNone)
          {
            if (!op.useCurrentIsps)
            {
              ISubstitutesService service = (ISubstitutesService) this._serviceProvider.GetService(typeof (ISubstitutesService));
              task.app = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(task), task.verRuleOwnerId, objId, typeIdList[0], AVSSpecificationForm.A);
              for (int index2 = 1; index2 < typeIdList.Count; ++index2)
              {
                ArticlesPartsPackage andVariableParts = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(task), task.verRuleOwnerId, objId, typeIdList[index2], AVSSpecificationForm.A);
                task.app.MergeWith(andVariableParts);
              }
              long[] withoutFiltration = ((IArticleService) this._serviceProvider.GetService(typeof (IArticleService))).FindArticlesByGroupIDWithoutFiltration(objId, (object) session.SessionGUID);
              task.ispList = new List<long>((IEnumerable<long>) withoutFiltration);
              task.ispNameList = new List<string>();
              foreach (long isp in task.ispList)
              {
                List<long> articleVariablePart = task.app.GetArticleVariablePart(isp);
                if (articleVariablePart != null && articleVariablePart.Count > 0)
                  task.HasVariableParts = true;
                IDBAttribute dbAttribute = (IDBAttribute) null;
                IDBObject dbObject = session.GetObject(isp, false);
                if (dbObject != null)
                  dbAttribute = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
                if (dbAttribute != null)
                  task.ispNameList.Add(Convert.ToString(dbAttribute.Value));
                else
                  task.ispNameList.Add("");
              }
              this.SortIsps(task);
              this.MakePrimaryIspFirst(session, task);
              this.InnerSetParm(task, ExpertConsts.Consts.attrIsLink, (object) task.HasVariableParts);
              this.InnerSetParm(task, ExpertConsts.Consts.attrIspList, (object) task.ispList);
              this.InnerSetParm(task, ExpertConsts.Consts.attrIspNum, (object) task.ispList.Count);
            }
            if (task.ispList.Count > 1)
            {
              switch (op.ispWork)
              {
                case IspMode.ispCommonPart:
                  List<long> articleCommonPart = task.app.GetArticleCommonPart(objId);
                  if (resData != null)
                  {
                    for (int index3 = resData.RowsCount - 1; index3 >= 0; --index3)
                    {
                      long int64 = Convert.ToInt64(resData[index3]["cad00033-306c-11d8-b4e9-00304f19f545"]);
                      if (!articleCommonPart.Contains(int64))
                        resData.RemoveAt(index3);
                    }
                    break;
                  }
                  break;
                case IspMode.ispCurrentOnly:
                  if (task.currentIsp != -1)
                  {
                    List<long> articleVariablePart = task.app.GetArticleVariablePart(task.ispList[task.currentIsp]);
                    if (resData != null)
                    {
                      for (int index4 = resData.RowsCount - 1; index4 >= 0; --index4)
                      {
                        long int64 = Convert.ToInt64(resData[index4]["cad00033-306c-11d8-b4e9-00304f19f545"]);
                        if (!articleVariablePart.Contains(int64))
                          resData.RemoveAt(index4);
                      }
                      break;
                    }
                    break;
                  }
                  break;
                case IspMode.ispAll:
                  using (List<long>.Enumerator enumerator = task.ispList.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      long current = enumerator.Current;
                      if (Math.Abs(current) != Math.Abs(objId))
                      {
                        ConditionStructure[] array6 = conditionStructureList.ToArray();
                        this.SetObjDependent(array6, current);
                        DataTable sostavData = this.GetSostavData(current, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array6, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
                        if (sostavData != null)
                        {
                          this.RemoveNonGuidColumns(sostavData);
                          task.DataCache.FillCacheData(sostavData);
                          if (sostavData.Rows.Count > 0)
                          {
                            HybridTableExp dst = new HybridTableExp(sostavData);
                            for (int index5 = 0; index5 < dst.RowsCount; ++index5)
                            {
                              HybridRowExp row = dst[index5];
                              ExpertServer.CopyRow(dst, row);
                            }
                          }
                        }
                      }
                    }
                    break;
                  }
              }
            }
          }
          if (resData != null)
            this.ProcessDataTable(session, task, dt, resData, array1, op, mod);
        }
      }
    }
    else
    {
      if (op.UseWholeTable && task.savedData != null)
      {
        dt = (HybridTableExp) task.savedData.CloneShallow();
        this.FilterByObjTypes(dt, op);
        this.AddAdditionalColumns(dt, task, op, session, (HashSet<long>) null);
        for (int index = 0; index < dt.RowsCount; ++index)
        {
          HybridRowExp hybridRowExp = dt[index];
          longList.Add(Convert.ToInt64(hybridRowExp[0]));
        }
        return longList;
      }
      for (int index = 0; index < context.Length; ++index)
      {
        long id = context[index];
        if (onlyBack)
        {
          TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(id, session);
          if ((TypedInfoItem) objData != (TypedInfoItem) null)
            id = objData.Id;
        }
        HybridTableExp resData = this.SearchByGlobal(session, task, opTag, id, (TempFormula) null, op.ispWork, op.linkTypeIDs);
        this.ProcessDataTable(session, task, dt, resData, descs, op, mod);
      }
      this.FilterByObjTypes(dt, op);
    }
    this.CheckDT(session, ref dt, columnDescriptorList);
    this.UseGlobalTable(session, task, dt, op);
    if (op.AddThis)
      this.AddThis(session, dt, task, context[0], op.ispWork, op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet);
    for (int index = 0; index < dt.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = dt[index];
      longList.Add(Convert.ToInt64(hybridRowExp[0]));
    }
    return longList;
  }

  private void ConvertRelIdToObjId(ExpertServer.ExpServTask ti, IUserSession ius, ref long objId)
  {
    IDBRelation relation = ius.GetRelation(ti.curRelationId, false);
    if (relation == null)
      return;
    IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
    if (attributeById != null && attributeById.Value.NotDBNull() && attributeById.AsInteger != 0L)
    {
      objId = attributeById.AsInteger;
    }
    else
    {
      IDBObject objectById = ius.GetObjectByID(relation.PartID, false);
      if (objectById == null)
        return;
      objId = objectById.ObjectID;
    }
  }

  private long ConvertRelIdToObjId(IUserSession ius, long relId)
  {
    IDBRelation relation = ius.GetRelation(relId);
    IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
    if (attributeById != null)
      return attributeById.AsInteger;
    List<long> objectVersions = ius.GetObjectVersions(relation.PartID);
    return objectVersions != null && objectVersions.Count > 0 ? objectVersions[0] : -1L;
  }

  private void ProcessDataTable(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    HybridTableExp dt,
    HybridTableExp resData,
    ColumnDescriptor[] descs,
    OpParmObject op,
    ModParm mod)
  {
    if (dt.Columns.Count == 0)
      dt.Columns = resData.Columns;
    bool tryObject = false;
    HashSet<long> longSet = new HashSet<long>();
    dt.IndexEnabled = true;
    int index1 = 0;
    while (index1 < resData.RowsCount)
    {
      HybridRowExp row = resData[index1];
      long int64 = Convert.ToInt64(row[0]);
      if (op.cond != null && !ti.CheckRowCond(int64, row, op.cond))
        resData.RemoveAt(index1);
      else if (op.UseGlobal == GlobalData.globalMult && ti.savedData != null && ti.savedDataByObjId(int64) == null)
        resData.RemoveAt(index1);
      else if (!op.Dups && dt.Contains(int64))
      {
        resData.RemoveAt(index1);
      }
      else
      {
        longSet.Add(int64);
        ++index1;
      }
    }
    int num = op.saveGlobal == GlobalSave.saveSet ? 0 : (op.saveGlobal != GlobalSave.saveAdd ? 1 : 0);
    if (num != 0 && !op.NoSearch)
      ti.OptAddNewObjects(ius, longSet);
    if (num != 0 && !op.NoSearch && !ti.forceSearchByGlobal)
    {
      for (int index2 = 0; index2 < resData.RowsCount; ++index2)
      {
        HybridRowExp row = resData[index2];
        long int64 = Convert.ToInt64(row[0]);
        if (longSet.Contains(int64))
          this.FillRowColumns(ius, ti, row, tryObject, false);
      }
    }
    this.AddAdditionalColumns(resData, ti, op, ius, longSet);
    for (int index3 = 0; index3 < resData.RowsCount; ++index3)
    {
      HybridRowExp hr = resData[index3];
      long int64 = Convert.ToInt64(hr[0]);
      if (longSet.Contains(int64))
      {
        dt.AddRow(hr);
        if (!op.Dups)
          longSet.Remove(int64);
      }
    }
  }

  private void UseOnlyPrimaryStructure(ref HybridDictionary tags, UseZamens uZam, bool? clientZam)
  {
    if (tags == null)
      tags = new HybridDictionary();
    switch (uZam)
    {
      case UseZamens.AsClient:
        if (!clientZam.HasValue)
          break;
        if (tags.Contains((object) ExpertServer.buttonSubstitutesGuid))
        {
          tags[(object) ExpertServer.buttonSubstitutesGuid] = (object) clientZam.Value;
          break;
        }
        tags.Add((object) ExpertServer.buttonSubstitutesGuid, (object) clientZam.Value);
        break;
      case UseZamens.MainVariant:
        if (!tags.Contains((object) ExpertServer.buttonSubstitutesGuid))
        {
          tags.Add((object) ExpertServer.buttonSubstitutesGuid, (object) true);
          break;
        }
        tags[(object) ExpertServer.buttonSubstitutesGuid] = (object) true;
        break;
      case UseZamens.AllVariants:
        if (!tags.Contains((object) ExpertServer.buttonSubstitutesGuid))
          break;
        tags.Remove((object) ExpertServer.buttonSubstitutesGuid);
        break;
    }
  }

  private object PerformNestedObjects(
    ExpertServer.ExpServTask ti,
    long verId,
    IUserSession ius,
    List<int> typeIdList,
    bool byRel,
    HybridTableExp dt,
    List<ConditionStructure> tmp,
    List<ConditionStructure> objConds,
    ColumnDescriptor[] descs,
    OpParmObject op,
    ModParm mod,
    ExpertScriptOp opTag,
    ExpertScriptMod modTag,
    bool _settingGlobalTable)
  {
    bool flag1 = opTag == ExpertScriptOp.opObjAncestors;
    object obj = (object) null;
    HybridTableExp resData;
    if (!op.NoSearch && !ti.forceSearchByGlobal)
    {
      ConditionStructure[] array = tmp == null ? (ConditionStructure[]) null : tmp.ToArray();
      this.SetObjDependent(array, verId);
      HybridDictionary tags = ti.filtr();
      this.UseOnlyPrimaryStructure(ref tags, ti.useAllZamens, ti.clientAllZamens);
      if (ti.aborting)
        return (object) null;
      DataHelper.CombineColumnsDescrs(descs, ti.DataCache.GetCacheColumns(TaskDataCache.ColumnsMode.SystemOnly), AttributeSourceTypes.Auto);
      if (typeIdList != null && typeIdList.Count == 0)
        typeIdList.Add(-1);
      resData = (HybridTableExp) null;
      DataTable dataTable;
      if (flag1)
      {
        dataTable = DataHelper.GetParentSostavData(verId, ius, typeIdList == null ? (IEnumerable<int>) (int[]) null : (IEnumerable<int>) typeIdList.ToArray(), false, (IEnumerable<ConditionStructure>) array, (IEnumerable<ColumnDescriptor>) descs, ti.filtr());
      }
      else
      {
        new DBRecordSetParams(array, descs).Tags = ti.filtr();
        dataTable = this.GetSostavData(verId, ius, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array, (IEnumerable<ColumnDescriptor>) descs, ti, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: ti.verRuleOwnerId);
      }
      if (dataTable != null)
      {
        ti.DataCache.FillCacheData(dataTable);
        resData = new HybridTableExp(dataTable, makeIndex: true);
        if (op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet)
          this.Add2SavedLinks(ti, resData);
        obj = this.OptimizePass(ti, ius, modTag, op, mod, objConds, ref resData, descs, _settingGlobalTable);
        if (obj != null)
          return obj;
      }
    }
    else
    {
      long objID = verId;
      if (flag1)
      {
        TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(verId, ius);
        if ((TypedInfoItem) objData != (TypedInfoItem) null)
          objID = objData.Id;
      }
      resData = this.SearchByGlobal(ius, ti, opTag, objID, (TempFormula) null, IspMode.ispNone, op.linkTypeIDs);
      obj = this.OptimizePass(ti, ius, modTag, op, mod, objConds, ref resData, descs, _settingGlobalTable);
      if (obj != null)
        return obj;
    }
    if (resData != null)
    {
      this.ProcessDataTable(ius, ti, dt, resData, descs, op, mod);
      int index1 = 0;
      for (int index2 = 0; index2 < resData.RowsCount; ++index2)
      {
        HybridRowExp row = resData[index2];
        long int64 = Convert.ToInt64(row[index1]);
        bool flag2 = op.filter == null || op.filter.Count == 0;
        if (!flag2)
        {
          flag2 = ti.CheckRowCond(int64, row, op.filter);
          if (!flag2 && ti._notExpandedObjIds != null)
            ti._notExpandedObjIds.Add(Math.Abs(int64));
        }
        if (flag2)
          obj = this.PerformNestedObjects(ti, int64, ius, typeIdList, byRel, dt, tmp, objConds, descs, op, mod, opTag, modTag, _settingGlobalTable);
        if (obj != null)
          break;
      }
    }
    return obj;
  }

  private void PerformIsps(
    HybridTableExp resData,
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    OpParmObject op,
    long objId,
    List<int> typeIdList,
    List<ConditionStructure> tmp,
    ColumnDescriptor[] descs,
    bool recursive)
  {
    if (op.ispWork == IspMode.ispNone)
      return;
    if (!op.useCurrentIsps)
    {
      ISubstitutesService service = (ISubstitutesService) this._serviceProvider.GetService(typeof (ISubstitutesService));
      ti.app = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(ti), ti.verRuleOwnerId, objId, typeIdList[0], AVSSpecificationForm.A);
      for (int index = 1; index < typeIdList.Count; ++index)
      {
        ArticlesPartsPackage andVariableParts = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(ti), ti.verRuleOwnerId, objId, typeIdList[index], AVSSpecificationForm.A);
        ti.app.MergeWith(andVariableParts);
      }
      long[] withoutFiltration = ((IArticleService) this._serviceProvider.GetService(typeof (IArticleService))).FindArticlesByGroupIDWithoutFiltration(objId, (object) ius.SessionGUID);
      ti.ispList = new List<long>((IEnumerable<long>) withoutFiltration);
      ti.ispNameList = new List<string>();
      foreach (long isp in ti.ispList)
      {
        List<long> articleVariablePart = ti.app.GetArticleVariablePart(isp);
        if (articleVariablePart != null && articleVariablePart.Count > 0)
          ti.HasVariableParts = true;
        IDBAttribute dbAttribute = (IDBAttribute) null;
        IDBObject dbObject = ius.GetObject(isp, false);
        if (dbObject != null)
          dbAttribute = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
        if (dbAttribute != null)
          ti.ispNameList.Add(Convert.ToString(dbAttribute.Value));
        else
          ti.ispNameList.Add("");
      }
      this.InnerSetParm(ti, ExpertConsts.Consts.attrIsLink, (object) ti.HasVariableParts);
      this.InnerSetParm(ti, ExpertConsts.Consts.attrIspList, (object) ti.ispList);
      this.InnerSetParm(ti, ExpertConsts.Consts.attrIspNum, (object) ti.ispList.Count);
    }
    if (ti.ispList.Count <= 1)
      return;
    switch (op.ispWork)
    {
      case IspMode.ispCommonPart:
        List<long> articleCommonPart = ti.app.GetArticleCommonPart(objId);
        if (resData == null)
          break;
        for (int index = resData.RowsCount - 1; index >= 0; --index)
        {
          long int64 = Convert.ToInt64(resData[index]["cad00033-306c-11d8-b4e9-00304f19f545"]);
          if (!articleCommonPart.Contains(int64))
            resData.RemoveAt(index);
        }
        break;
      case IspMode.ispCurrentOnly:
        if (ti.currentIsp == -1)
          break;
        List<long> articleVariablePart1 = ti.app.GetArticleVariablePart(ti.ispList[ti.currentIsp]);
        if (resData == null)
          break;
        for (int index = resData.RowsCount - 1; index >= 0; --index)
        {
          long int64 = Convert.ToInt64(resData[index]["cad00033-306c-11d8-b4e9-00304f19f545"]);
          if (!articleVariablePart1.Contains(int64))
            resData.RemoveAt(index);
        }
        break;
      case IspMode.ispAll:
        using (List<long>.Enumerator enumerator = ti.ispList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            long current = enumerator.Current;
            if (Math.Abs(current) != Math.Abs(objId))
            {
              ConditionStructure[] array = tmp.ToArray();
              this.SetObjDependent(array, current);
              new DBRecordSetParams(array, descs).Tags = ti.filtr();
              DataTable sostavData = this.GetSostavData(current, ius, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array, (IEnumerable<ColumnDescriptor>) descs, ti, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: ti.verRuleOwnerId);
              if (sostavData != null)
              {
                this.RemoveNonGuidColumns(sostavData);
                ti.DataCache.FillCacheData(sostavData);
                HybridTableExp hybridTableExp = new HybridTableExp(sostavData);
                if (hybridTableExp.RowsCount > 0)
                {
                  for (int index = 0; index < hybridTableExp.RowsCount; ++index)
                  {
                    HybridRowExp row = hybridTableExp[index];
                    ExpertServer.CopyRow(resData, row);
                  }
                }
              }
            }
          }
          break;
        }
    }
  }

  private List<long> ExecuteExcerptMulti(
    int taskId,
    long[] context,
    ExpertScriptOp opTag,
    ExpertScriptMod modTag,
    OpParmObject op,
    ModParm mod,
    out HybridTableExp dt,
    out object MultiRes)
  {
    List<long> longList = new List<long>();
    dt = (HybridTableExp) null;
    MultiRes = (object) null;
    if (context.Length == 0)
      return longList;
    dt = new HybridTableExp();
    bool _settingGlobalTable1 = false;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    bool onlyBack = opTag == ExpertScriptOp.opObjParents || opTag == ExpertScriptOp.opObjAncestors;
    IUserSession session = this.GetSession(task);
    List<ColumnDescriptor> columnDescriptorList = task.curScrType == ExpertScriptType.RecalcScript ? this.GenRecalcColumnDescriptors() : this.GenerateColumnDescriptors(session, op, mod as ModParmSort, onlyBack);
    ColumnDescriptor[] descs = (ColumnDescriptor[]) null;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    if (!op.NoSearch && !task.forceSearchByGlobal)
    {
      ISelectionsService ss = this.GetSS(taskId);
      try
      {
        long objectID = Math.Abs(context[0]);
        ConditionStructure[] conditionStructures = ss.GetConditionStructures((object) ExpertServer.GetSessionGuid(task), op.excerptID, objectID);
        conditionStructureList.AddRange((IEnumerable<ConditionStructure>) conditionStructures);
        if (op.linkTypeIDs != null && op.linkTypeIDs.Count > 0)
        {
          ConditionStructure linkTypeConds = this.GetLinkTypeConds(op);
          this.AddCondStru(conditionStructureList, linkTypeConds);
        }
        if (op.objTypeIDs != null)
        {
          if (op.objTypeIDs.Count > 0)
          {
            ConditionStructure objTypeCond = this.GetObjTypeCond(op);
            this.AddCondStru(conditionStructureList, objTypeCond);
          }
        }
      }
      catch (Exception ex)
      {
        this.ReportError(taskId, ex.Message);
        throw;
      }
      this.MarkObjDependent(conditionStructureList, context[0]);
      List<int> typeIdList = (List<int>) null;
      bool byRel = this.SelByRelation(conditionStructureList, columnDescriptorList, out typeIdList, true, session);
      if (op.Dups && !byRel)
        byRel = true;
      List<ConditionStructure> objConds = new List<ConditionStructure>();
      this.DivideConds(conditionStructureList, objConds);
      if (op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet)
      {
        this.AddAttribute(columnDescriptorList, op, new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"));
        this.AddAttribute(columnDescriptorList, op, new Guid(ExpertAttrGUIDs.attrSorting));
        ColumnDescriptor columnDescriptor = columnDescriptorList[0] with
        {
          Sort = SortOrders.NONE
        };
        columnDescriptorList[0] = columnDescriptor;
      }
      this.InitRelTable(session, task, op, columnDescriptorList);
      ColumnDescriptor[] array1 = DataHelper.CombineColumnsDescrs(columnDescriptorList.ToArray(), task.DataCache.GetCacheObjOnlyColumnList(TaskDataCache.ColumnsMode.SystemOnly).ToArray(), AttributeSourceTypes.Object).ToArray();
      bool _settingGlobalTable2 = op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet;
      if (_settingGlobalTable2)
      {
        if (task._notExpandedObjIds == null)
          task._notExpandedObjIds = new List<long>();
        else
          task._notExpandedObjIds.Clear();
      }
      if (_settingGlobalTable2 && context.Length == 1 && !this.OptModTag(modTag) && (op.filter == null || op.filter.Count == 0))
      {
        long objId = context[0];
        ConditionStructure[] array2 = conditionStructureList.ToArray();
        this.SetObjDependent(array2, objId);
        HybridDictionary tags = task.filtr();
        this.UseOnlyPrimaryStructure(ref tags, task.useAllZamens, task.clientAllZamens);
        if (typeIdList.Count == 0)
          typeIdList.Add(-1);
        new DBRecordSetParams(array2, array1).Tags = task.filtr();
        DataTable sostavData = this.GetSostavData(objId, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array2, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, true, task.verRuleOwnerId);
        if (sostavData != null)
        {
          this.RemoveNonGuidColumns(sostavData);
          task.DataCache.FillCacheData(sostavData);
          HybridTableExp hybridTableExp = new HybridTableExp(sostavData);
          this.PerformIsps(hybridTableExp, session, task, op, objId, typeIdList, conditionStructureList, array1, true);
          this.Add2SavedLinks(task, hybridTableExp);
          this.CopyColumns(dt, hybridTableExp, array1);
          Hashtable hashtable = new Hashtable();
          for (int index = 0; index < hybridTableExp.RowsCount; ++index)
          {
            HybridRowExp row = hybridTableExp[index];
            long int64 = Convert.ToInt64(row[0]);
            if (op.Dups || !hashtable.ContainsKey((object) int64))
            {
              ExpertServer.CopyRow(dt, row);
              if (!hashtable.ContainsKey((object) int64))
                hashtable.Add((object) int64, (object) null);
            }
          }
          List<int> opObjTypes = this.GetOpObjTypes(op);
          this.FilterDataTable(task, session, dt, opObjTypes, array1, op.cond, _settingGlobalTable2);
          this.ReplaceQuantities(task);
        }
      }
      else
      {
        for (int index1 = 0; index1 < context.Length; ++index1)
        {
          long isp = context[index1];
          if (op.ispWork == IspMode.ispCurrentOnly && task.currentIsp != -1)
            isp = task.ispList[task.currentIsp];
          if (onlyBack && task.curRelationId != 0L)
            conditionStructureList.Insert(0, new ConditionStructure(-20, RelationalOperators.Equal, (object) task.curRelationId, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value));
          ConditionStructure[] array3 = conditionStructureList.ToArray();
          this.SetObjDependent(array3, isp);
          HybridDictionary tags = task.filtr();
          this.UseOnlyPrimaryStructure(ref tags, task.useAllZamens, task.clientAllZamens);
          if (typeIdList.Count == 0)
            typeIdList.Add(-1);
          DataTable dataTable = DataHelper.GetObjectData(-1, session, (IEnumerable<ConditionStructure>) array3, (IEnumerable<ColumnDescriptor>) array1);
          if (task.curRelationId != 0L && (dataTable == null || dataTable.Rows.Count == 0))
          {
            if (onlyBack)
            {
              conditionStructureList.RemoveAt(0);
              ConditionStructure[] array4 = conditionStructureList.ToArray();
              this.SetObjDependent(array4, isp);
              dataTable = DataHelper.GetParentSostavData(isp, session, (IEnumerable<int>) typeIdList.ToArray(), false, (IEnumerable<ConditionStructure>) array4, (IEnumerable<ColumnDescriptor>) array1, task.filtr());
            }
            else if (isp == task.curRelationId)
              dataTable = this.GetSostavData(this.ConvertRelIdToObjId(session, task.curRelationId), session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array3, (IEnumerable<ColumnDescriptor>) array1, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
          }
          if (dataTable != null)
          {
            task.DataCache.FillCacheData(dataTable);
            HybridTableExp resData = new HybridTableExp(dataTable);
            this.PerformIsps(resData, session, task, op, isp, typeIdList, conditionStructureList, array1, false);
            if (_settingGlobalTable2)
              this.Add2SavedLinks(task, resData);
            int index2 = 0;
            if (dt.Columns.Count == 0 && resData.Columns.Count > 0)
              this.CopyColumns(dt, resData, array1);
            MultiRes = this.OptimizePass(task, session, modTag, op, mod, objConds, ref resData, array1, _settingGlobalTable2);
            if (MultiRes != null)
            {
              if (MultiRes is HybridRowExp)
              {
                ExpertServer.CopyRow(dt, MultiRes as HybridRowExp);
                longList.Add(Convert.ToInt64((MultiRes as HybridRowExp)[0]));
              }
              return longList;
            }
            Hashtable hashtable = new Hashtable();
            for (int index3 = 0; index3 < resData.RowsCount; ++index3)
            {
              HybridRowExp row = resData[index3];
              this.FillRowColumns(session, task, row, false, false);
              long int64 = Convert.ToInt64(row[index2]);
              if (op.Dups || !hashtable.ContainsKey((object) int64))
              {
                ExpertServer.CopyRow(dt, row);
                if (!hashtable.ContainsKey((object) int64))
                  hashtable.Add((object) int64, (object) null);
              }
            }
            for (int index4 = 0; index4 < resData.RowsCount; ++index4)
            {
              HybridRowExp row = resData[index4];
              long int64 = Convert.ToInt64(row[index2]);
              bool flag = op.filter == null || op.filter.Count == 0;
              if (!flag)
              {
                flag = task.CheckRowCond(int64, row, op.filter);
                if (!flag && task._notExpandedObjIds != null)
                  task._notExpandedObjIds.Add(Math.Abs(int64));
              }
              if (flag)
                MultiRes = this.PerformNestedObjects(task, int64, session, typeIdList, byRel, dt, conditionStructureList, objConds, array1, op, mod, opTag, modTag, _settingGlobalTable2);
              if (MultiRes != null)
              {
                if (MultiRes is HybridRowExp)
                {
                  ExpertServer.CopyRow(dt, MultiRes as HybridRowExp);
                  longList.Add(Convert.ToInt64((MultiRes as HybridRowExp)[0]));
                }
                return longList;
              }
            }
            if (_settingGlobalTable2)
              this.ReplaceQuantities(task);
          }
        }
        if (!this.OptModTag(modTag))
        {
          List<int> opObjTypes = this.GetOpObjTypes(op);
          this.FilterDataTable(task, session, dt, opObjTypes, array1, op.cond, _settingGlobalTable2);
        }
      }
    }
    else
    {
      if (op.UseWholeTable && task.savedData != null && op.ispWork != IspMode.ispCommonPart && op.ispWork != IspMode.ispCurrentOnly)
      {
        dt = (HybridTableExp) task.savedData.CloneShallow();
        this.FilterByObjTypes(dt, op);
        this.AddAdditionalColumns(dt, task, op, session, (HashSet<long>) null);
        for (int index = 0; index < dt.RowsCount; ++index)
        {
          HybridRowExp hybridRowExp = dt[index];
          longList.Add(Convert.ToInt64(hybridRowExp[0]));
        }
        return longList;
      }
      MeasuredValue measuredValue = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
      List<ConditionStructure> objConds = (List<ConditionStructure>) null;
      if (op.objTypeIDs != null && op.objTypeIDs.Count > 0)
        objConds = (List<ConditionStructure>) null;
      for (int index5 = 0; index5 < context.Length; ++index5)
      {
        long id = context[index5];
        if (onlyBack)
        {
          TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(id, session);
          if ((TypedInfoItem) objData != (TypedInfoItem) null)
            id = objData.Id;
        }
        HybridTableExp resData = this.SearchByGlobal(session, task, opTag, id, (TempFormula) null, op.ispWork, op.linkTypeIDs);
        this.ProcessDataTable(session, task, dt, resData, descs, op, mod);
        for (int index6 = 0; index6 < resData.RowsCount; ++index6)
        {
          HybridRowExp row1 = resData[index6];
          long int64 = Convert.ToInt64(row1[0]);
          if (op.filter == null || op.filter.Count == 0 || task.CheckRowCond(int64, row1, op.filter))
            MultiRes = this.PerformNestedObjects(task, int64, session, (List<int>) null, true, dt, (List<ConditionStructure>) null, objConds, descs, op, mod, opTag, modTag, _settingGlobalTable1);
          if (MultiRes != null)
          {
            if (MultiRes is HybridRowExp)
            {
              ExpertServer.CopyRow(dt, MultiRes as HybridRowExp);
              longList.Add(Convert.ToInt64((MultiRes as HybridRowExp)[0]));
            }
            else if (MultiRes is HybridRowExp[])
            {
              dt.ClearRows();
              longList.Clear();
              foreach (HybridRowExp row2 in MultiRes as HybridRowExp[])
              {
                ExpertServer.CopyRow(dt, row2);
                longList.Add(Convert.ToInt64(row2[0]));
              }
            }
            return longList;
          }
        }
      }
      this.FilterByObjTypes(dt, op);
    }
    this.CheckDT(session, ref dt, columnDescriptorList);
    this.UseGlobalTable(session, task, dt, op);
    if (op.AddThis)
      this.AddThis(session, dt, task, context[0], op.ispWork, op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet);
    for (int index = 0; index < dt.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = dt[index];
      longList.Add(Convert.ToInt64(hybridRowExp[0]));
    }
    return longList;
  }

  private List<long> ExecuteObjSelectMulti(
    int taskId,
    long[] context,
    ExpertScriptOp opTag,
    ExpertScriptMod modTag,
    OpParmObject op,
    ModParm mod,
    out HybridTableExp dt,
    out object MultiRes)
  {
    List<long> res_arr = new List<long>();
    dt = (HybridTableExp) null;
    MultiRes = (object) null;
    if (context.Length == 0)
      return res_arr;
    dt = new HybridTableExp();
    bool _settingGlobalTable1 = false;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    bool onlyBack = opTag == ExpertScriptOp.opObjParents || opTag == ExpertScriptOp.opObjAncestors;
    IUserSession session = this.GetSession(task);
    List<ColumnDescriptor> columnDescriptorList = task.curScrType == ExpertScriptType.RecalcScript ? this.GenRecalcColumnDescriptors() : this.GenerateColumnDescriptors(session, op, mod as ModParmSort, onlyBack);
    ColumnDescriptor[] descs = (ColumnDescriptor[]) null;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    if (!op.NoSearch && !task.forceSearchByGlobal)
    {
      if (op.objTypeIDs != null && op.objTypeIDs.Count > 0)
      {
        ConditionStructure objTypeCond = this.GetObjTypeCond(op);
        this.AddCondStru(conditionStructureList, objTypeCond);
      }
      if (op.linkTypeIDs != null && op.linkTypeIDs.Count > 0)
      {
        ConditionStructure linkTypeConds = this.GetLinkTypeConds(op);
        this.AddCondStru(conditionStructureList, linkTypeConds);
      }
      List<int> typeIdList = (List<int>) null;
      bool byRel = this.SelByRelation(conditionStructureList, columnDescriptorList, out typeIdList, true, session);
      this.MarkObjDependent(conditionStructureList, context[0]);
      List<ConditionStructure> objConds = new List<ConditionStructure>();
      this.DivideConds(conditionStructureList, objConds);
      if (op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet)
      {
        this.AddAttribute(columnDescriptorList, op, new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"));
        this.AddAttribute(columnDescriptorList, op, new Guid(ExpertAttrGUIDs.attrSorting));
        ColumnDescriptor columnDescriptor1 = columnDescriptorList[0] with
        {
          Sort = SortOrders.NONE
        };
        columnDescriptorList[0] = columnDescriptor1;
        for (int index = columnDescriptorList.Count - 1; index >= 0; --index)
        {
          ColumnDescriptor columnDescriptor2 = columnDescriptorList[index];
          if (columnDescriptor2.AttributeSource == AttributeSourceTypes.Relation && columnDescriptor2.AttributeID is Guid && Convert.ToString(columnDescriptor2.AttributeID) == "cad00036-306c-11d8-b4e9-00304f19f545")
          {
            columnDescriptorList.RemoveAt(index);
            break;
          }
        }
      }
      this.InitRelTable(session, task, op, columnDescriptorList);
      ColumnDescriptor[] array1 = columnDescriptorList.ToArray();
      bool _settingGlobalTable2 = op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet;
      ColumnDescriptor[] array2 = DataHelper.CombineColumnsDescrs(array1, task.DataCache.GetCacheColumns(_settingGlobalTable2 ? TaskDataCache.ColumnsMode.All : TaskDataCache.ColumnsMode.SystemOnly), AttributeSourceTypes.Auto).ToArray();
      if (_settingGlobalTable2)
      {
        if (task._notExpandedObjIds == null)
          task._notExpandedObjIds = new List<long>();
        else
          task._notExpandedObjIds.Clear();
      }
      if (_settingGlobalTable2 && context.Length == 1 && !this.OptModTag(modTag) && (op.filter == null || op.filter.Count == 0))
      {
        long num = context[0];
        ConditionStructure[] array3 = conditionStructureList.ToArray();
        this.SetObjDependent(array3, num);
        HybridDictionary tags = task.filtr();
        this.UseOnlyPrimaryStructure(ref tags, task.useAllZamens, task.clientAllZamens);
        if (typeIdList.Count == 0)
          typeIdList.Add(-1);
        DataTable dataTable;
        if (onlyBack)
        {
          dataTable = DataHelper.GetParentSostavData(num, session, (IEnumerable<int>) typeIdList.ToArray(), true, (IEnumerable<ConditionStructure>) array3, (IEnumerable<ColumnDescriptor>) array2, task.filtr());
        }
        else
        {
          new DBRecordSetParams(array3, array2).Tags = task.filtr();
          dataTable = this.GetSostavData(num, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array3, (IEnumerable<ColumnDescriptor>) array2, task, op.hiddenMode, op.useConfiguratorOptions, true, task.verRuleOwnerId);
        }
        this.RemoveNonGuidColumns(dataTable);
        task.DataCache.FillCacheData(dataTable);
        HybridTableExp hybridTableExp = new HybridTableExp(dataTable);
        this.PerformIsps(hybridTableExp, session, task, op, num, typeIdList, conditionStructureList, array2, true);
        this.Add2SavedLinks(task, hybridTableExp);
        this.CopyColumns(dt, hybridTableExp, array2);
        Hashtable hashtable = new Hashtable();
        for (int index = 0; index < hybridTableExp.RowsCount; ++index)
        {
          HybridRowExp row = hybridTableExp[index];
          long int64 = Convert.ToInt64(row[0]);
          if (op.Dups || !hashtable.ContainsKey((object) int64))
          {
            ExpertServer.CopyRow(dt, row);
            if (!hashtable.ContainsKey((object) int64))
              hashtable.Add((object) int64, (object) null);
          }
        }
        List<int> opObjTypes = this.GetOpObjTypes(op);
        this.FilterDataTable(task, session, dt, opObjTypes, array2, op.cond, _settingGlobalTable2);
        this.ReplaceQuantities(task);
      }
      else
      {
        for (int index1 = 0; index1 < context.Length; ++index1)
        {
          long isp = context[index1];
          if (op.ispWork == IspMode.ispCurrentOnly && task.currentIsp != -1)
            isp = task.ispList[task.currentIsp];
          bool flag1 = false;
          if (onlyBack && task.curRelationId != 0L)
          {
            conditionStructureList.Insert(0, new ConditionStructure(-20, RelationalOperators.Equal, (object) task.curRelationId, (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value));
            flag1 = true;
          }
          ConditionStructure[] array4 = conditionStructureList.ToArray();
          this.SetObjDependent(array4, isp);
          HybridDictionary tags = task.filtr();
          this.UseOnlyPrimaryStructure(ref tags, task.useAllZamens, task.clientAllZamens);
          if (typeIdList.Count == 0)
            typeIdList.Add(-1);
          DataTable dataTable;
          if (onlyBack)
          {
            dataTable = DataHelper.GetParentSostavData(isp, session, (IEnumerable<int>) typeIdList.ToArray(), false, (IEnumerable<ConditionStructure>) array4, (IEnumerable<ColumnDescriptor>) array2, task.filtr());
          }
          else
          {
            new DBRecordSetParams(array4, array2).Tags = task.filtr();
            dataTable = this.GetSostavData(isp, session, (IEnumerable<int>) typeIdList, (IEnumerable<ConditionStructure>) array4, (IEnumerable<ColumnDescriptor>) array2, task, op.hiddenMode, op.useConfiguratorOptions, filtrationOwnerID: task.verRuleOwnerId);
          }
          if (onlyBack && task.curRelationId != 0L && (dataTable == null || dataTable.Rows.Count == 0) && flag1)
          {
            conditionStructureList.RemoveAt(0);
            flag1 = false;
            ConditionStructure[] array5 = conditionStructureList.ToArray();
            this.SetObjDependent(array5, isp);
            dataTable = DataHelper.GetParentSostavData(isp, session, (IEnumerable<int>) typeIdList.ToArray(), false, (IEnumerable<ConditionStructure>) array5, (IEnumerable<ColumnDescriptor>) array2, task.filtr());
          }
          this.RemoveNonGuidColumns(dataTable);
          task.DataCache.FillCacheData(dataTable);
          HybridTableExp resData = new HybridTableExp(dataTable);
          this.PerformIsps(resData, session, task, op, isp, typeIdList, conditionStructureList, array2, false);
          if (_settingGlobalTable2)
            this.Add2SavedLinks(task, resData);
          int index2 = 0;
          if (index1 == 0)
            this.CopyColumns(dt, resData, array2);
          MultiRes = this.OptimizePass(task, session, modTag, op, mod, objConds, ref resData, array2, _settingGlobalTable2);
          if (MultiRes != null)
          {
            if (MultiRes is HybridRowExp)
            {
              ExpertServer.CopyRow(dt, MultiRes as HybridRowExp);
              res_arr.Add(Convert.ToInt64((MultiRes as HybridRowExp)[0]));
            }
            return res_arr;
          }
          Hashtable hashtable = new Hashtable();
          int index3 = 0;
          while (index3 < resData.RowsCount)
          {
            HybridRowExp row = resData[index3];
            this.FillRowColumns(session, task, row, false, false);
            long int64 = Convert.ToInt64(row[index2]);
            if (op.Dups || !hashtable.ContainsKey((object) int64))
            {
              ExpertServer.CopyRow(dt, row);
              if (!hashtable.ContainsKey((object) int64))
                hashtable.Add((object) int64, (object) null);
              ++index3;
            }
            else
              resData.RemoveAt(index3);
          }
          if (flag1)
            conditionStructureList.RemoveAt(0);
          for (int index4 = 0; index4 < resData.RowsCount; ++index4)
          {
            HybridRowExp row = resData[index4];
            long int64 = Convert.ToInt64(row[index2]);
            bool flag2 = op.filter == null || op.filter.Count == 0;
            if (!flag2)
            {
              flag2 = task.CheckRowCond(int64, row, op.filter);
              if (!flag2 && task._notExpandedObjIds != null)
                task._notExpandedObjIds.Add(Math.Abs(int64));
            }
            if (flag2)
              MultiRes = this.PerformNestedObjects(task, int64, session, typeIdList, byRel, dt, conditionStructureList, objConds, array2, op, mod, opTag, modTag, _settingGlobalTable2);
            if (MultiRes != null)
            {
              if (MultiRes is HybridRowExp)
              {
                ExpertServer.CopyRow(dt, MultiRes as HybridRowExp);
                res_arr.Add(Convert.ToInt64((MultiRes as HybridRowExp)[0]));
              }
              return res_arr;
            }
          }
        }
        if (!this.OptModTag(modTag))
        {
          List<int> opObjTypes = this.GetOpObjTypes(op);
          this.FilterDataTable(task, session, dt, opObjTypes, array2, op.cond, _settingGlobalTable2);
        }
        if (_settingGlobalTable2)
          this.ReplaceQuantities(task);
      }
    }
    else
    {
      if (op.UseWholeTable && task.savedData != null && op.ispWork != IspMode.ispCommonPart && op.ispWork != IspMode.ispCurrentOnly)
      {
        dt = (HybridTableExp) task.savedData.CloneShallow();
        this.FilterByObjTypes(dt, op);
        this.AddAdditionalColumns(dt, task, op, session, (HashSet<long>) null);
        for (int index = 0; index < dt.RowsCount; ++index)
        {
          HybridRowExp hybridRowExp = dt[index];
          res_arr.Add(Convert.ToInt64(hybridRowExp[0]));
        }
        return res_arr;
      }
      MeasuredValue measuredValue = new MeasuredValue(1.0, ExpertConsts.Consts.measureShtuk);
      List<ConditionStructure> objConds = (List<ConditionStructure>) null;
      if (op.objTypeIDs != null && op.objTypeIDs.Count > 0)
        objConds = (List<ConditionStructure>) null;
      for (int index5 = 0; index5 < context.Length; ++index5)
      {
        long id = context[index5];
        if (onlyBack)
        {
          TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(id, session);
          if ((TypedInfoItem) objData != (TypedInfoItem) null)
            id = objData.Id;
        }
        HybridTableExp resData = this.SearchByGlobal(session, task, opTag, id, (TempFormula) null, op.ispWork, op.linkTypeIDs);
        this.ProcessDataTable(session, task, dt, resData, descs, op, mod);
        MultiRes = this.PerformModTag(task, session, modTag, mod, resData, _settingGlobalTable1);
        if (MultiRes != null)
        {
          this.ProcessMultiRes(MultiRes, dt, res_arr);
          return res_arr;
        }
        for (int index6 = 0; index6 < resData.RowsCount; ++index6)
        {
          HybridRowExp row1 = resData[index6];
          long int64 = Convert.ToInt64(row1[0]);
          if (op.filter == null || op.filter.Count == 0 || task.CheckRowCond(int64, row1, op.filter))
            MultiRes = this.PerformNestedObjects(task, int64, session, (List<int>) null, true, dt, (List<ConditionStructure>) null, objConds, descs, op, mod, opTag, modTag, _settingGlobalTable1);
          if (MultiRes != null)
          {
            if (MultiRes is HybridRowExp)
            {
              ExpertServer.CopyRow(dt, MultiRes as HybridRowExp);
              res_arr.Add(Convert.ToInt64((MultiRes as HybridRowExp)[0]));
            }
            else if (MultiRes is HybridRowExp[])
            {
              dt.ClearRows();
              res_arr.Clear();
              foreach (HybridRowExp row2 in MultiRes as HybridRowExp[])
              {
                ExpertServer.CopyRow(dt, row2);
                res_arr.Add(Convert.ToInt64(row2[0]));
              }
            }
            return res_arr;
          }
        }
      }
      this.FilterByObjTypes(dt, op);
    }
    this.CheckDT(session, ref dt, columnDescriptorList);
    this.UseGlobalTable(session, task, dt, op);
    if (op.AddThis)
      this.AddThis(session, dt, task, context[0], op.ispWork, op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet);
    for (int index = 0; index < dt.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = dt[index];
      res_arr.Add(Convert.ToInt64(hybridRowExp[0]));
    }
    return res_arr;
  }

  private void ProcessMultiRes(object MultiRes, HybridTableExp dt, List<long> res_arr)
  {
    switch (MultiRes)
    {
      case HybridRowExp _:
        ExpertServer.CopyRow(dt, MultiRes as HybridRowExp);
        res_arr.Add(Convert.ToInt64((MultiRes as HybridRowExp)[0]));
        break;
      case HybridRowExp[] _:
        dt.ClearRows();
        res_arr.Clear();
        foreach (HybridRowExp multiRe in MultiRes as HybridRowExp[])
        {
          ExpertServer.CopyRow(dt, multiRe);
          res_arr.Add(Convert.ToInt64(multiRe[0]));
        }
        break;
    }
  }

  internal void _AddThis(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    HybridTableExp dt,
    long objId,
    bool globalTable)
  {
    HybridRowExp row = dt.NewRow();
    row[0] = (object) objId;
    if (!globalTable)
    {
      this.FillRowColumns(ius, ti, row, true, false);
    }
    else
    {
      int indexByName1 = row.Columns.GetIndexByName("cad0001f-306c-11d8-b4e9-00304f19f545");
      int indexByName2 = row.Columns.GetIndexByName("cad00020-306c-11d8-b4e9-00304f19f545");
      if (indexByName1 >= 0 || indexByName2 >= 0)
      {
        IDBObject dbObject = ius.GetObject(objId, false);
        if (dbObject != null)
        {
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(ExpertConsts.Consts._attrObjDesign);
          if (attributeById1 != null)
            row[indexByName1] = attributeById1.Value;
          IDBAttribute attributeById2 = dbObject.GetAttributeByID(ExpertConsts.Consts._attrObjName);
          if (attributeById2 != null)
            row[indexByName2] = attributeById2.Value;
          row["cad0002e-306c-11d8-b4e9-00304f19f545"] = (object) dbObject.ObjectType;
          row["cad00047-306c-11d8-b4e9-00304f19f545"] = (object) dbObject.Caption;
        }
      }
      else
      {
        TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(objId, ius);
        if (!TaskDataCache.IsEmpty((TypedInfoItem) objData))
        {
          if (dt.Columns.Contains("cad0002e-306c-11d8-b4e9-00304f19f545"))
            row["cad0002e-306c-11d8-b4e9-00304f19f545"] = (object) objData.ObjTypeID;
          if (dt.Columns.Contains("cad00047-306c-11d8-b4e9-00304f19f545"))
            row["cad00047-306c-11d8-b4e9-00304f19f545"] = (object) objData.Caption;
        }
      }
    }
    if (dt.Columns.Contains(ExpertAttrGUIDs.attrTotalForProduct))
      row[ExpertAttrGUIDs.attrTotalForProduct] = (object) MeasureHelper.ConvertToMeasuredValue(LocalizationHolder.rm.GetString("Expert.Server_25"));
    if (dt.Columns.Contains("cad00267-306c-11d8-b4e9-00304f19f545"))
      row["cad00267-306c-11d8-b4e9-00304f19f545"] = (object) MeasureHelper.ConvertToMeasuredValue(LocalizationHolder.rm.GetString("Expert.Server_25"));
    dt.InsertAt(row, 0);
  }

  internal void AddThis(
    IUserSession ius,
    HybridTableExp dt,
    ExpertServer.ExpServTask ti,
    long objId,
    IspMode ispWork,
    bool globalTable)
  {
    switch (ispWork)
    {
      case IspMode.ispNone:
      case IspMode.ispCommonPart:
      case IspMode.ispCurrentOnly:
        this._AddThis(ius, ti, dt, objId, globalTable);
        break;
      case IspMode.ispAll:
        using (List<long>.Enumerator enumerator = ti.ispList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            long current = enumerator.Current;
            this._AddThis(ius, ti, dt, current, globalTable);
          }
          break;
        }
    }
  }

  internal void MakePrimaryIspFirst(IUserSession ius, ExpertServer.ExpServTask ti)
  {
    if (ti.ispList == null || ti.ispList.Count == 0)
      return;
    Guid guid = Guid.Empty;
    int index1 = 0;
    for (int index2 = 0; index2 < ti.ispList.Count; ++index2)
    {
      long isp = ti.ispList[index2];
      IDBObject dbObject = ius.GetObject(isp);
      if (dbObject != null)
      {
        if (guid == Guid.Empty)
        {
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), false);
          if (attributeByGuid != null && attributeByGuid.Value != null)
          {
            if (attributeByGuid.Value.GetType() == typeof (Guid))
            {
              guid = (Guid) attributeByGuid.Value;
            }
            else
            {
              Guid result = Guid.Empty;
              if (Guid.TryParse(Convert.ToString(attributeByGuid.Value), out result))
                guid = result;
            }
          }
        }
        if (guid != Guid.Empty && guid.Equals(dbObject.ObjectGUID))
        {
          index1 = index2;
          break;
        }
      }
    }
    if (index1 == 0)
      return;
    long isp1 = ti.ispList[0];
    ti.ispList[0] = ti.ispList[index1];
    ti.ispList[index1] = isp1;
  }

  private void RemoveNonGuidColumns(DataTable dt)
  {
    if (dt == null)
      return;
    DataColumnCollection columns = dt.Columns;
    Guid result = Guid.Empty;
    for (int index = columns.Count - 1; index >= 0; --index)
    {
      if (!Guid.TryParse(columns[index].ColumnName, out result))
        columns.RemoveAt(index);
    }
  }

  private List<ColumnDescriptor> GenRecalcColumnDescriptors()
  {
    return new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1)
    };
  }

  private bool HasId(List<ColumnDescriptor> list, int attrId)
  {
    foreach (ColumnDescriptor columnDescriptor in list)
    {
      try
      {
        if (Convert.ToInt32(columnDescriptor.AttributeID) == attrId)
          return true;
      }
      catch
      {
      }
    }
    return false;
  }

  public static string GetScriptCode(IUserSession session, long scriptID)
  {
    IDBObject objectActualCopy = session.GetObjectActualCopy(scriptID, false);
    if (objectActualCopy != null)
    {
      IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad00366-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid != null)
        return attributeByGuid.Value.ToString();
    }
    return "";
  }

  public static void ExecScript(
    IUserSession session,
    long scriptID,
    string method,
    params object[] list)
  {
    string scriptCode = ExpertServer.GetScriptCode(session, scriptID);
    if (scriptCode == null || !(scriptCode.Trim() != ""))
      return;
    string str = ScriptExecHelper.IsolatedExecScript(scriptCode, CSharpScriptInvocationOptions.Default, list);
    if (str != "")
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Expert.Server_269"), (object) scriptID) + str);
  }

  private void AddRelationFields(List<ColumnDescriptor> res)
  {
    if (!this.HasId(res, -20))
      res.Insert(1, new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 1));
    if (!this.HasId(res, -21))
      res.Insert(2, new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 1));
    if (!this.HasId(res, -22))
      res.Insert(3, new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 1));
    if (this.HasId(res, -23))
      return;
    res.Insert(4, new ColumnDescriptor((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 1));
  }

  private List<ColumnDescriptor> GenerateColumnDescriptors(
    IUserSession ius,
    OpParmObject op,
    ModParmSort mod,
    bool onlyBack)
  {
    List<ColumnDescriptor> res = new List<ColumnDescriptor>();
    res.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    List<string> stringList = new List<string>();
    stringList.Add("cad00029-306c-11d8-b4e9-00304f19f545");
    List<bool> boolList = new List<bool>();
    boolList.Add(false);
    bool measured = false;
    if (op != null && (op.saveGlobal == GlobalSave.saveAdd || op.saveGlobal == GlobalSave.saveSet || op.ispWork != IspMode.ispNone))
      this.AddRelationFields(res);
    if (op != null && op.dataAttrGUIDs != null)
    {
      for (int index = 0; index < op.dataAttrGUIDs.Count; ++index)
      {
        string dataAttrGuiD = op.dataAttrGUIDs[index];
        if (!stringList.Contains(dataAttrGuiD))
        {
          if (MetaDataHelper.GetAttributeTypeID(dataAttrGuiD) != -10000)
          {
            try
            {
              ColumnContents columnContents = DbHelper.GetColumnContents(dataAttrGuiD, out measured);
              if (columnContents != ColumnContents.Date)
              {
                ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) new Guid(dataAttrGuiD), op.GetAttrCheck(index) ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : res.Count + 1);
                res.Add(columnDescriptor);
                stringList.Add(dataAttrGuiD);
                boolList.Add(op.GetAttrCheck(index));
              }
            }
            catch
            {
            }
          }
        }
      }
    }
    if (mod != null)
    {
      if (mod.sortAttrTexts != null)
      {
        for (int index1 = 0; index1 < mod.sortAttrTexts.Count; ++index1)
        {
          string sortAttr = mod.sortAttrs[index1];
          try
          {
            ColumnContents columnContents = DbHelper.GetColumnContents(sortAttr, out measured);
            if (columnContents != ColumnContents.Date)
            {
              if (!stringList.Contains(sortAttr))
              {
                ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) new Guid(sortAttr), mod.sortAttrChecks[index1] ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : res.Count + 1);
                res.Add(columnDescriptor);
                stringList.Add(sortAttr);
                boolList.Add(mod.sortAttrChecks[index1]);
              }
              else
              {
                int index2 = stringList.IndexOf(sortAttr);
                bool sortAttrCheck = mod.sortAttrChecks[index1];
                if (sortAttrCheck != boolList[index2])
                {
                  ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) new Guid(sortAttr), sortAttrCheck ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : res.Count + 1);
                  res.Add(columnDescriptor);
                  stringList.Add(sortAttr);
                  boolList.Add(sortAttrCheck);
                }
              }
            }
          }
          catch
          {
          }
        }
      }
      if (mod.groupAttrTexts != null)
      {
        for (int index3 = 0; index3 < mod.groupAttrTexts.Count; ++index3)
        {
          string groupAttr = mod.groupAttrs[index3];
          try
          {
            ColumnContents columnContents = DbHelper.GetColumnContents(groupAttr, out measured);
            if (columnContents != ColumnContents.Date)
            {
              if (!stringList.Contains(groupAttr))
              {
                ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) new Guid(groupAttr), mod.groupAttrChecks[index3] ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : res.Count + 1);
                res.Add(columnDescriptor);
                stringList.Add(groupAttr);
                boolList.Add(mod.groupAttrChecks[index3]);
              }
              else
              {
                int index4 = stringList.IndexOf(groupAttr);
                bool groupAttrCheck = mod.groupAttrChecks[index3];
                if (groupAttrCheck != boolList[index4])
                {
                  ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) new Guid(groupAttr), groupAttrCheck ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : res.Count + 1);
                  res.Add(columnDescriptor);
                  stringList.Add(groupAttr);
                  boolList.Add(groupAttrCheck);
                }
              }
            }
          }
          catch
          {
          }
        }
      }
    }
    return res;
  }

  private void AddAttribute(List<ColumnDescriptor> descs, OpParmObject op, Guid attrGuid)
  {
    for (int index = 0; index < descs.Count; ++index)
    {
      ColumnDescriptor desc = descs[index];
      if (desc.AttributeID is Guid && ((Guid) desc.AttributeID).Equals(attrGuid))
        return;
    }
    ColumnDescriptor columnDescriptor = !(attrGuid.ToString() == ExpertAttrGUIDs.attrSorting) ? new ColumnDescriptor((object) attrGuid, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, descs.Count + 1) : new ColumnDescriptor((object) attrGuid, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, descs.Count + 1);
    descs.Add(columnDescriptor);
  }

  private void AddAdditionalColumns(
    HybridTableExp dt,
    ExpertServer.ExpServTask ti,
    OpParmObject opObj,
    IUserSession ius,
    HashSet<long> objList)
  {
    int count1 = dt.Columns.Count;
    if (opObj.NoSearch && ti.savedLinks != null && opObj.afterFilter != null && opObj.afterFilter.Count > 0)
    {
      for (int index = 0; index < opObj.afterFilter.attrGUIDs.Count; ++index)
      {
        string attrGuiD = opObj.afterFilter.attrGUIDs[index];
        Guid guid = new Guid(attrGuiD);
        if (ti.savedLinks.Columns.Contains(attrGuiD) && !dt.Columns.Contains(attrGuiD))
        {
          HybridColumnsExp.HybridColumnExp column = ti.savedLinks.Columns[attrGuiD];
          HybridColumnsExp.HybridColumnExp col = new HybridColumnsExp.HybridColumnExp(column.ColumnName, column.DataType);
          dt.Columns.Add(col);
        }
      }
      if (dt.Columns.Count > count1)
      {
        for (int index1 = 0; index1 < dt.RowsCount; ++index1)
        {
          HybridRowExp hybridRowExp1 = dt[index1];
          long int64_1 = Convert.ToInt64(hybridRowExp1[0]);
          if (objList == null || objList.Contains(int64_1))
          {
            HybridRowExp hybridRowExp2 = ti.savedDataByObjId(int64_1);
            if (hybridRowExp2 != null)
            {
              object obj = hybridRowExp2["cad00035-306c-11d8-b4e9-00304f19f545"];
              if (obj.NotNullOrDBNull())
              {
                long int64_2 = Convert.ToInt64(obj);
                HybridRowExp[] hybridRowExpArray = ti.savedLinksByPartId(int64_2);
                if (hybridRowExpArray != null && hybridRowExpArray.Length != 0)
                {
                  HybridRowExp hybridRowExp3 = hybridRowExpArray[0];
                  for (int index2 = count1; index2 < dt.Columns.Count; ++index2)
                    hybridRowExp1[index2] = hybridRowExp3[dt.Columns[index2].ColumnName];
                }
              }
            }
          }
        }
      }
    }
    if (opObj.dataAttrGUIDs == null || opObj.dataAttrGUIDs.Count <= 0)
      return;
    int count2 = dt.Columns.Count;
    for (int index = 0; index < opObj.dataAttrGUIDs.Count; ++index)
    {
      string str = Convert.ToString(opObj.dataAttrGUIDs[index]);
      if (!dt.Columns.Contains(str))
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(str));
        if (attributeType != null)
        {
          Type dataType = DataTypeConvertor.FieldType2DataType(attributeType.FieldType, attributeType.AttributeID);
          dt.AddColumn(str, dataType);
        }
      }
    }
    if (count2 >= dt.Columns.Count || opObj.saveGlobal == GlobalSave.saveAdd || opObj.saveGlobal == GlobalSave.saveSet)
      return;
    for (int index = 0; index < dt.RowsCount; ++index)
    {
      HybridRowExp row = dt[index];
      long int64 = Convert.ToInt64(row[0]);
      if (objList == null || objList.Contains(int64))
        this.FillRowColumns(ius, ti, row, true, false, count2);
    }
  }

  private IDBRelationCollection GetRelationCollection(
    IUserSession ius,
    int relType,
    string verRuleOwnerId)
  {
    return verRuleOwnerId == "" ? ius.GetRelationCollection(relType) : ius.GetRelationCollection(relType, verRuleOwnerId);
  }

  private bool FillRowColumns(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    HybridRowExp row,
    bool tryObject,
    bool AsString)
  {
    return this.FillRowColumns(ius, ti, row, tryObject, AsString, 0);
  }

  private bool EmptyVal(object val) => val.IsNullOrDBNull();

  private bool FillRowColumns(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    HybridRowExp row,
    bool tryObject,
    bool AsString,
    int startingCol)
  {
    long int64_1 = Convert.ToInt64(row[0]);
    IDBObject dbObject = (IDBObject) null;
    try
    {
      HybridColumnsExp columns = row.Columns;
      List<int> intList1 = new List<int>();
      List<int> intList2 = new List<int>();
      for (int index = startingCol; index < columns.Count; ++index)
      {
        if (this.EmptyVal(row[index]))
        {
          string columnName = columns[index].ColumnName;
          if (GuidHelper.IsGuid(columnName))
          {
            Guid guid = new Guid(columnName);
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid);
            object val = (object) null;
            if (tryObject)
            {
              if (ti.savedData != null && ti.savedData.Columns.Contains(columnName))
              {
                HybridRowExp hybridRowExp = ti.savedDataByObjId(int64_1);
                if (hybridRowExp != null)
                  val = hybridRowExp[columnName];
              }
              if (ti.savedLinks != null && ti.savedLinks.Columns.Contains(columnName))
              {
                object obj = row["cad00033-306c-11d8-b4e9-00304f19f545"];
                if (obj.NotNullOrDBNull())
                {
                  long int64_2 = Convert.ToInt64(obj);
                  HybridRowExp hybridRowExp = ti.savedLinksByIdIndex(int64_2);
                  try
                  {
                    val = hybridRowExp[columnName];
                  }
                  catch (ArgumentException ex)
                  {
                    if (row.Columns[index].DataType == typeof (MeasuredValue))
                    {
                      if (hybridRowExp.Columns[columnName].DataType == typeof (string))
                        val = (object) MeasureHelper.ConvertToMeasuredValue(Convert.ToString(hybridRowExp[columnName]));
                    }
                  }
                }
              }
            }
            if (this.EmptyVal(val) && !ti.OptGetObjectAttr(int64_1, attributeTypeId, out val) && tryObject)
            {
              if (dbObject == null)
                dbObject = ius.GetObject(int64_1, false);
              if (dbObject != null)
              {
                if (columns[index].ColumnName == "cad0002e-306c-11d8-b4e9-00304f19f545")
                {
                  val = (object) dbObject.ObjectType;
                }
                else
                {
                  try
                  {
                    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(guid);
                    bool flag = attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList;
                    if (AsString)
                    {
                      string[] descriptionsByGuid = dbObject.GetDescriptionsByGuid(guid, false);
                      if (descriptionsByGuid != null)
                      {
                        if (flag)
                        {
                          ArrayHolder arrayHolder = new ArrayHolder(descriptionsByGuid.Length, 1);
                          for (int x = 0; x < descriptionsByGuid.Length; ++x)
                            arrayHolder[x, 0] = (object) descriptionsByGuid[x];
                          val = (object) arrayHolder;
                        }
                        else
                          val = (object) descriptionsByGuid[0];
                      }
                    }
                    else
                    {
                      object[] valuesByGuid = dbObject.GetValuesByGuid(guid, false);
                      if (valuesByGuid != null)
                      {
                        if (flag)
                        {
                          ArrayHolder arrayHolder = new ArrayHolder(valuesByGuid.Length, 1);
                          for (int x = 0; x < valuesByGuid.Length; ++x)
                            arrayHolder[x, 0] = valuesByGuid[x];
                          val = (object) arrayHolder;
                        }
                        else
                          val = valuesByGuid[0];
                      }
                    }
                  }
                  catch (KernelException ex)
                  {
                  }
                }
              }
            }
            if (this.EmptyVal(val))
            {
              val = this._GetParmValue(ti, int64_1, -1, attributeTypeId);
              AttribPair attribPair = new AttribPair(attributeTypeId, -1);
              if (!ti.calcStack.Contains(int64_1, attribPair.objTypeID, attribPair.attribID) && this.HasAttrRule(ius, attribPair.objTypeID, attribPair.attribID))
              {
                int quiet = (int) this.InnerCalculateQuiet(ti, ius, attribPair.objTypeID, attribPair.attribID, int64_1, out val);
              }
            }
            if (!this.EmptyVal(val))
              row[index] = val;
          }
        }
      }
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (EAbort))
      {
        XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_59"));
        if (node != null)
          ti.traceAddText(node, ex.Message);
      }
    }
    return true;
  }

  private HybridTableExp CollectObjectData(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    long[] context,
    ModParmSort mod)
  {
    long[] instance = (long[]) Array.CreateInstance(typeof (long), context.Length);
    for (int index = 0; index < context.Length; ++index)
      instance[index] = context[index];
    return new HybridTableExp(ius.GetObjectCollection(-1).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) instance, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.ID)
    }, this.GenerateColumnDescriptors(ius, (OpParmObject) null, mod, false).ToArray())
    {
      Tags = ti.filtr()
    }));
  }

  private TempFormula GetSelFolderCond(ScriptTreeNode node)
  {
    if (node.modTag == ExpertScriptMod.modIfExists || node.modTag == ExpertScriptMod.modIfAll)
      return (node.mod as ModParmFormula).tf;
    return node.modTag == ExpertScriptMod.modUnknown && node.op is OpParmCond ? (node.op as OpParmCond).cond : (TempFormula) null;
  }

  private bool FilterByCond(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    HybridTableExp dt,
    TempFormula cond,
    bool _settingGlobalTable,
    List<int> objTypes = null)
  {
    bool flag = false;
    HashSet<long> delObjList = new HashSet<long>();
    HashSet<long> longSet = new HashSet<long>();
    HashSet<long> delLinkList = new HashSet<long>();
    int index1 = -1;
    if (objTypes != null)
    {
      index1 = dt.Columns.GetIndexByName("cad0002e-306c-11d8-b4e9-00304f19f545");
      if (index1 < 0)
        objTypes = (List<int>) null;
    }
    Dictionary<long, ExpertServer.dtStru> partIdIndex = (Dictionary<long, ExpertServer.dtStru>) null;
    if (_settingGlobalTable)
    {
      partIdIndex = new Dictionary<long, ExpertServer.dtStru>();
      for (int index2 = 0; index2 < dt.RowsCount; ++index2)
      {
        HybridRowExp hybridRowExp = dt[index2];
        long int64_1 = Convert.ToInt64(hybridRowExp["cad00035-306c-11d8-b4e9-00304f19f545"]);
        long int64_2 = Convert.ToInt64(hybridRowExp[0]);
        if (!partIdIndex.ContainsKey(int64_1))
          partIdIndex.Add(int64_1, new ExpertServer.dtStru(index2, int64_2));
      }
    }
    int index3 = 0;
    while (index3 < dt.RowsCount && !ti.aborting)
    {
      HybridRowExp row = dt[index3];
      if (objTypes != null)
      {
        int int32 = Convert.ToInt32(row[index1]);
        if (!objTypes.Contains(int32))
        {
          flag = true;
          dt.RemoveAt(index3);
          continue;
        }
      }
      long int64_3 = Convert.ToInt64(row[0]);
      if (cond != null && !ti.CheckRowCond(int64_3, row, cond))
      {
        if (_settingGlobalTable)
        {
          long int64_4 = Convert.ToInt64(row["cad00033-306c-11d8-b4e9-00304f19f545"]);
          long int64_5 = Convert.ToInt64(row["cad00035-306c-11d8-b4e9-00304f19f545"]);
          if (!delObjList.Contains(int64_5))
          {
            delObjList.Add(int64_5);
            longSet.Add(int64_3);
          }
          int num = -1;
          if (ti.linksIdIndex.ContainsKey(int64_4))
            num = ti.linksIdIndex[int64_4];
          if (num >= 0)
            delLinkList.Add(int64_4);
          flag = true;
        }
        else
        {
          flag = true;
          dt.RemoveAt(index3);
          continue;
        }
      }
      ++index3;
    }
    if (flag & _settingGlobalTable)
    {
      foreach (long partId in delObjList)
      {
        HybridRowExp[] hybridRowExpArray = ti.savedLinksByPartId(partId);
        if (hybridRowExpArray != null)
        {
          foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
          {
            long int64 = Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]);
            if (!delLinkList.Contains(int64))
              delLinkList.Add(int64);
          }
        }
      }
      foreach (long rootObjId in longSet)
        this.DeleteFromRoot(rootObjId, ti, dt, delObjList, delLinkList, partIdIndex);
      int index4 = 0;
      while (index4 < ti.savedLinks.RowsCount)
      {
        long int64 = Convert.ToInt64(ti.savedLinks[index4]["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (delLinkList.Contains(int64))
          ti.savedLinks.RemoveAt(index4);
        else
          ++index4;
      }
      ExpertServer.CreateLinkIndex(ti);
      int index5 = 0;
      while (index5 < dt.RowsCount)
      {
        long int64 = Convert.ToInt64(dt[index5]["cad00035-306c-11d8-b4e9-00304f19f545"]);
        if (delObjList.Contains(int64) && ti.savedLinksByPartId(int64) == null)
          dt.RemoveAt(index5);
        else
          ++index5;
      }
    }
    return flag;
  }

  private void DeleteFromRoot(
    long rootObjId,
    ExpertServer.ExpServTask ti,
    HybridTableExp dt,
    HashSet<long> delObjList,
    HashSet<long> delLinkList,
    Dictionary<long, ExpertServer.dtStru> partIdIndex)
  {
    HybridRowExp[] hybridRowExpArray1 = ti.savedLinksByProjId(rootObjId);
    if (hybridRowExpArray1 == null)
      return;
    foreach (HybridRowExp hybridRowExp1 in hybridRowExpArray1)
    {
      if (hybridRowExp1 != null)
      {
        long int64_1 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
        long int64_2 = Convert.ToInt64(hybridRowExp1["cad00033-306c-11d8-b4e9-00304f19f545"]);
        if (!delLinkList.Contains(int64_2))
          delLinkList.Add(int64_2);
        if (partIdIndex.ContainsKey(int64_1))
        {
          long objId = partIdIndex[int64_1].objID;
          bool flag = false;
          HybridRowExp[] hybridRowExpArray2 = ti.savedLinksByPartId(int64_1);
          if (hybridRowExpArray2.Length > 1)
          {
            foreach (HybridRowExp hybridRowExp2 in hybridRowExpArray2)
            {
              long int64_3 = Convert.ToInt64(hybridRowExp2["cad00033-306c-11d8-b4e9-00304f19f545"]);
              if (int64_3 != int64_2 && !delLinkList.Contains(int64_3))
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag && !delObjList.Contains(int64_1))
          {
            delObjList.Add(int64_1);
            this.DeleteFromRoot(objId, ti, dt, delObjList, delLinkList, partIdIndex);
          }
        }
      }
    }
  }

  private void UseGlobalTable(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    HybridTableExp dt,
    OpParmObject op)
  {
    if (op.UseGlobal == GlobalData.globalNone || ti.savedData == null)
      return;
    HybridTableExp savedData = ti.savedData;
    string[] strArray = this.AddColumns(dt, savedData);
    if (op.UseGlobal == GlobalData.globalMult)
    {
      int index1 = 0;
      while (index1 < dt.RowsCount)
      {
        HybridRowExp hybridRowExp1 = dt[index1];
        long int64 = Convert.ToInt64(hybridRowExp1[0]);
        int index2 = savedData.SelectFirst(0, (object) int64);
        if (index2 >= 0)
        {
          HybridRowExp hybridRowExp2 = savedData[index2];
          foreach (string columnName in strArray)
            hybridRowExp1[columnName] = hybridRowExp2[columnName];
          ++index1;
        }
        else
          dt.RemoveAt(index1);
      }
    }
    if (op.UseGlobal != GlobalData.globalAdd)
      return;
    dt.IndexEnabled = true;
    for (int index3 = 0; index3 < savedData.RowsCount; ++index3)
    {
      HybridRowExp hybridRowExp = savedData[index3];
      long int64 = Convert.ToInt64(hybridRowExp[0]);
      if (op.Dups || !dt.Contains(int64))
      {
        HybridRowExp hrow = dt.NewRow();
        for (int index4 = 0; index4 < savedData.Columns.Count; ++index4)
        {
          HybridColumnsExp.HybridColumnExp column = savedData.Columns[index4];
          hrow[column.ColumnName] = hybridRowExp[column.ColumnName];
        }
        dt.Add(hrow);
      }
    }
  }

  private void CheckDT(IUserSession ius, ref HybridTableExp dt, List<ColumnDescriptor> t_descs)
  {
    if (dt != null && dt.Columns.Count > 0)
      return;
    DataTable emptyDataTable = DBRecordSet.CreateEmptyDataTable(string.Empty, t_descs.ToArray());
    dt = new HybridTableExp(emptyDataTable);
  }

  private HybridTableExp SearchByGlobal(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    ExpertScriptOp opTag,
    long objID,
    TempFormula cond,
    IspMode ispWork,
    List<int> relTypes)
  {
    HybridTableExp hybridTableExp = new HybridTableExp();
    hybridTableExp.IndexEnabled = true;
    if (ti.savedLinks == null || ti.savedLinks.Columns.Count == 0)
      return hybridTableExp;
    bool flag1 = opTag == ExpertScriptOp.opObjAncestors || opTag == ExpertScriptOp.opObjParents;
    HybridRowExp[] hybridRowExpArray;
    if (flag1 && ti.curRelationId != 0L)
      hybridRowExpArray = new HybridRowExp[1]
      {
        ti.savedLinksByIdIndex(ti.curRelationId)
      };
    else
      hybridRowExpArray = flag1 ? ti.savedLinksByPartId(objID) : ti.savedLinksByProjId(objID);
    if (hybridRowExpArray == null)
      return hybridTableExp;
    if (relTypes != null && relTypes.Count > 0)
    {
      int indexByName = ti.savedLinks.Columns.GetIndexByName("cad00036-306c-11d8-b4e9-00304f19f545");
      if (indexByName >= 0)
      {
        List<HybridRowExp> hybridRowExpList = new List<HybridRowExp>();
        for (int index = 0; index < hybridRowExpArray.Length; ++index)
        {
          int int32 = Convert.ToInt32(hybridRowExpArray[index][indexByName]);
          if (relTypes.Contains(int32))
            hybridRowExpList.Add(hybridRowExpArray[index]);
        }
        hybridRowExpArray = hybridRowExpList.ToArray();
      }
    }
    if (ispWork == IspMode.ispCommonPart || ispWork == IspMode.ispCurrentOnly)
    {
      List<HybridRowExp> hybridRowExpList = new List<HybridRowExp>();
      List<long> list = ispWork == IspMode.ispCommonPart ? ti.app.GetArticleCommonPart(objID) : ti.app.GetArticleVariablePart(objID);
      bool flag2 = false;
      if (list != null)
      {
        GenericListHelper.MakeUnique<long>(list);
        foreach (HybridRowExp hybridRowExp in hybridRowExpArray)
        {
          long int64 = Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]);
          if (list.BinarySearch(int64) >= 0)
            hybridRowExpList.Add(hybridRowExp);
          else
            flag2 = true;
        }
        if (flag2)
          hybridRowExpArray = hybridRowExpList.ToArray();
      }
    }
    for (int index = 0; index < ti.savedData.Columns.Count; ++index)
      hybridTableExp.Columns.Add(ti.savedData.Columns[index]);
    for (int index = 0; index < ti.savedLinks.Columns.Count; ++index)
      hybridTableExp.Columns.AddDuplicate(ti.savedLinks.Columns[index]);
    List<ExpertServer.IndexPair> indexPairList = new List<ExpertServer.IndexPair>();
    for (int index = 0; index < ti.savedLinks.Columns.Count; ++index)
    {
      string columnName = ti.savedLinks.Columns[index].ColumnName;
      int indexByName = ti.savedData.Columns.GetIndexByName(columnName);
      if (indexByName >= 0)
        indexPairList.Add(new ExpertServer.IndexPair(index, indexByName));
    }
    string columnName1 = flag1 ? "cad00034-306c-11d8-b4e9-00304f19f545" : "cad00035-306c-11d8-b4e9-00304f19f545";
    for (int index1 = 0; index1 < hybridRowExpArray.Length; ++index1)
    {
      HybridRowExp Row2 = hybridRowExpArray[index1];
      long int64_1 = Convert.ToInt64(Row2[columnName1]);
      HybridRowExp Row1 = flag1 ? ti.savedDataByObjId(int64_1) : ti.savedDataByPartId(int64_1);
      if (Row1 != null)
      {
        DoubleLinkRowExp doubleLinkRowExp = new DoubleLinkRowExp(hybridTableExp.Columns, Row1, Row2);
        long int64_2 = Convert.ToInt64(doubleLinkRowExp[0]);
        if (cond == null || cond.Count == 0 || ti.CheckRowCond(int64_2, (HybridRowExp) doubleLinkRowExp, cond))
          hybridTableExp.Add((HybridRowExp) doubleLinkRowExp);
        for (int index2 = 0; index2 < indexPairList.Count; ++index2)
        {
          ExpertServer.IndexPair indexPair = indexPairList[index2];
          Row1[indexPair.DataIndex] = Row2[indexPair.LinkIndex];
        }
      }
    }
    return hybridTableExp;
  }

  private void InitRelTable(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    OpParmObject op,
    List<ColumnDescriptor> t_descs)
  {
    if (op.saveGlobal != GlobalSave.saveAdd && op.saveGlobal != GlobalSave.saveSet)
      return;
    lock (ti)
    {
      if (op.saveGlobal == GlobalSave.saveSet)
        ti.savedLinks = (HybridTableExp) null;
      if (ti.savedLinks == null)
      {
        List<ColumnDescriptor> t_descs1 = new List<ColumnDescriptor>();
        for (int index = 0; index < t_descs.Count; ++index)
        {
          ColumnDescriptor tDesc = t_descs[index];
          if (tDesc.AttributeSource == AttributeSourceTypes.Relation && (!(tDesc.AttributeID is Guid) || !(Convert.ToString(tDesc.AttributeID) == "cad00036-306c-11d8-b4e9-00304f19f545")))
            t_descs1.Add(tDesc);
        }
        ColumnDescriptor[] array = t_descs1.ToArray();
        HybridColumnsExp.HybridColumnExp[] dataColumns = ExpertServer.GetDataColumns(ius, t_descs1);
        ti.savedLinks = new HybridTableExp();
        ti.linksIdIndex = new Dictionary<long, int>(200);
        ti.linksProjIndex = new Dictionary<long, List<int>>(200);
        ti.linksPartIndex = new Dictionary<long, List<int>>(200);
        ti.savedLinks.AddColumns(dataColumns);
        ti.RelCondDescs = array;
      }
      if (op.saveGlobal != GlobalSave.saveSet)
        return;
      ti.savedLinks.ClearRows();
      ti.linksIdIndex.Clear();
      ti.linksPartIndex.Clear();
      ti.linksProjIndex.Clear();
    }
  }

  private void InitRelTable(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    List<ColumnDescriptor> t_descs)
  {
    lock (ti)
    {
      if (ti.savedLinks == null)
      {
        List<ColumnDescriptor> t_descs1 = new List<ColumnDescriptor>();
        for (int index = 0; index < t_descs.Count; ++index)
        {
          ColumnDescriptor tDesc = t_descs[index];
          if (tDesc.AttributeSource == AttributeSourceTypes.Relation)
            t_descs1.Add(tDesc);
        }
        ColumnDescriptor[] array = t_descs1.ToArray();
        HybridColumnsExp.HybridColumnExp[] dataColumns = ExpertServer.GetDataColumns(ius, t_descs1);
        ti.savedLinks = new HybridTableExp();
        ti.linksIdIndex = new Dictionary<long, int>(200);
        ti.linksProjIndex = new Dictionary<long, List<int>>(200);
        ti.linksPartIndex = new Dictionary<long, List<int>>(200);
        ti.savedLinks.AddColumns(dataColumns);
        ti.RelCondDescs = array;
      }
      ti.savedLinks.ClearRows();
      ti.linksIdIndex.Clear();
      ti.linksPartIndex.Clear();
      ti.linksProjIndex.Clear();
    }
  }

  private static void MakeLinkIndexes(ExpertServer.ExpServTask ti)
  {
    ti.linksIdIndex.Clear();
    ti.linksProjIndex.Clear();
    ti.linksPartIndex.Clear();
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      long int64_1 = Convert.ToInt64(savedLink["cad00033-306c-11d8-b4e9-00304f19f545"]);
      long int64_2 = Convert.ToInt64(savedLink["cad00034-306c-11d8-b4e9-00304f19f545"]);
      long int64_3 = Convert.ToInt64(savedLink["cad00035-306c-11d8-b4e9-00304f19f545"]);
      ti.linksIdIndex.Add(int64_1, index);
      ti.AddProjIndex(int64_2, index);
      ti.AddPartIndex(int64_3, index);
    }
  }

  public static HybridColumnsExp.HybridColumnExp[] GetDataColumns(
    IUserSession ius,
    List<ColumnDescriptor> t_descs)
  {
    HybridColumnsExp.HybridColumnExp[] dataColumns = new HybridColumnsExp.HybridColumnExp[t_descs.Count];
    List<int> intList = new List<int>();
    for (int index = 0; index < t_descs.Count; ++index)
    {
      ColumnDescriptor tDesc = t_descs[index];
      if (!ExpertServer.es.columns.ContainsKey(tDesc.AttributeID))
        intList.Add(index);
      else
        dataColumns[index] = ExpertServer.CopyColumn((HybridColumnsExp.HybridColumnExp) ExpertServer.es.columns[tDesc.AttributeID]);
    }
    if (intList.Count > 0)
    {
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.Equal, (object) -1, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.ID)
      };
      ColumnDescriptor[] columns = new ColumnDescriptor[intList.Count];
      for (int index = 0; index < intList.Count; ++index)
        columns[index] = t_descs[intList[index]];
      DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns);
      DataTable dataTable = ius.GetRelationCollection(-1).Select(paramSet);
      for (int index = 0; index < intList.Count; ++index)
      {
        HybridColumnsExp.HybridColumnExp hybridColumnExp = new HybridColumnsExp.HybridColumnExp(dataTable.Columns[index].ColumnName, dataTable.Columns[index].DataType);
        ColumnDescriptor tDesc = t_descs[intList[index]];
        IMSAttributeType attrTypeInfo = ExpertServer.GetAttrTypeInfo(tDesc.AttributeID);
        if (attrTypeInfo != null)
        {
          hybridColumnExp.attrTypeId = attrTypeInfo.AttributeID;
          hybridColumnExp.fldType = attrTypeInfo.FieldType;
        }
        dataColumns[intList[index]] = hybridColumnExp;
        if (!ExpertServer.es.columns.ContainsKey(tDesc.AttributeID))
          ExpertServer.es.columns.Add(tDesc.AttributeID, (object) hybridColumnExp);
      }
    }
    return dataColumns;
  }

  private static IMSAttributeType GetAttrTypeInfo(object cdAttrId)
  {
    IMSAttributeType attrTypeInfo = (IMSAttributeType) null;
    switch (cdAttrId)
    {
      case ObligatoryObjectAttributes attrTypeID:
        attrTypeInfo = MetaDataHelper.GetAttributeType((int) attrTypeID);
        break;
      case Guid attrTypeGuid:
        attrTypeInfo = MetaDataHelper.GetAttributeType(attrTypeGuid);
        break;
    }
    return attrTypeInfo;
  }

  public void Add2SavedLinks(ExpertServer.ExpServTask ti, HybridTableExp resData)
  {
    if (resData == null)
      return;
    int indexByName = resData.Columns.GetIndexByName("cad00033-306c-11d8-b4e9-00304f19f545");
    for (int index1 = 0; index1 < resData.RowsCount; ++index1)
    {
      HybridRowExp hybridRowExp = resData[index1];
      long int64_1 = Convert.ToInt64(hybridRowExp[indexByName]);
      if (ti.savedLinksByIdIndex(int64_1) == null)
      {
        HybridRowExp hrow = ti.savedLinks.NewRow();
        for (int index2 = 0; index2 < ti.savedLinks.Columns.Count; ++index2)
        {
          HybridColumnsExp.HybridColumnExp column = ti.savedLinks.Columns[index2];
          object obj = hybridRowExp[column.ColumnName];
          hrow[column.ColumnName] = obj;
        }
        ti.savedLinks.Add(hrow);
        int index3 = ti.savedLinks.RowsCount - 1;
        long int64_2 = Convert.ToInt64(hrow["cad00033-306c-11d8-b4e9-00304f19f545"]);
        long int64_3 = Convert.ToInt64(hrow["cad00034-306c-11d8-b4e9-00304f19f545"]);
        long int64_4 = Convert.ToInt64(hrow["cad00035-306c-11d8-b4e9-00304f19f545"]);
        ti.linksIdIndex.Add(int64_2, index3);
        ti.AddProjIndex(int64_3, index3);
        ti.AddPartIndex(int64_4, index3);
      }
    }
  }

  public void SaveGlobalTables(
    ExpertServer.ExpServTask ti,
    DataTable dt,
    ColumnDescriptor[] descs,
    bool blockDups = true)
  {
    lock (ti)
    {
      this.RemoveNonGuidColumns(dt);
      ti.savedData = new HybridTableExp();
      ti.savedLinks = new HybridTableExp();
      List<HybridColumnsExp.HybridColumnExp> colList1 = new List<HybridColumnsExp.HybridColumnExp>();
      List<HybridColumnsExp.HybridColumnExp> colList2 = new List<HybridColumnsExp.HybridColumnExp>();
      for (int index1 = 0; index1 < dt.Columns.Count; ++index1)
      {
        DataColumn column = dt.Columns[index1];
        bool flag1 = true;
        bool flag2 = false;
        for (int index2 = 0; index2 < descs.Length; ++index2)
        {
          if (column.ColumnName == Convert.ToString(descs[index2].AttributeID))
          {
            ColumnDescriptor desc = descs[index2];
            flag1 = false;
            if (desc.AttributeSource == AttributeSourceTypes.Relation)
              flag2 = true;
          }
        }
        HybridColumnsExp.HybridColumnExp hybridColumnExp = new HybridColumnsExp.HybridColumnExp(column.ColumnName, column.DataType);
        hybridColumnExp.ColumnName = flag1 || !(descs[index1].AttributeID is Guid) ? column.Caption : Convert.ToString(descs[index1].AttributeID);
        if (flag2)
        {
          if (column.ColumnName == "cad00034-306c-11d8-b4e9-00304f19f545" || column.ColumnName == "cad00035-306c-11d8-b4e9-00304f19f545")
            colList1.Add(hybridColumnExp);
          colList2.Add(hybridColumnExp);
        }
        else
          colList1.Add(hybridColumnExp);
      }
      ti.savedData.AddColumns(colList1);
      ti.savedLinks.AddColumns(colList2);
      List<int> intList = new List<int>();
      for (int index = 0; index < ti.savedData.Columns.Count; ++index)
      {
        HybridColumnsExp.HybridColumnExp column = ti.savedData.Columns[index];
        int num = dt.Columns.IndexOf(column.ColumnName);
        intList.Add(num);
      }
      List<long> longList = new List<long>();
      foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      {
        if (blockDups)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (!longList.Contains(int64))
            longList.Add(int64);
          else
            continue;
        }
        HybridRowExp hrow = ti.savedData.NewRow();
        for (int index = 0; index < ti.savedData.Columns.Count; ++index)
        {
          if (intList[index] >= 0)
            hrow[index] = row[intList[index]];
        }
        ti.savedData.Add(hrow);
      }
      ti.dataObjIndex = new Dictionary<long, int>(dt.Rows.Count);
      ti.dataPartIndex = new Dictionary<long, int>(dt.Rows.Count);
      for (int index = 0; index < ti.savedData.RowsCount; ++index)
      {
        HybridRowExp hybridRowExp = ti.savedData[index];
        object obj1 = hybridRowExp["cad00029-306c-11d8-b4e9-00304f19f545"];
        if (obj1.NotNullOrDBNull())
        {
          long int64 = Convert.ToInt64(obj1);
          if (!ti.dataObjIndex.ContainsKey(int64))
            ti.dataObjIndex.Add(int64, index);
        }
        object obj2 = hybridRowExp["cad00035-306c-11d8-b4e9-00304f19f545"];
        if (obj2.NotNullOrDBNull())
        {
          long int64 = Convert.ToInt64(obj2);
          if (!ti.dataPartIndex.ContainsKey(int64))
            ti.dataPartIndex.Add(int64, index);
        }
      }
      ti.linksIdIndex = new Dictionary<long, int>(200);
      ti.linksProjIndex = new Dictionary<long, List<int>>(200);
      ti.linksPartIndex = new Dictionary<long, List<int>>(200);
      HybridTableExp resData = new HybridTableExp(dt);
      this.Add2SavedLinks(ti, resData);
    }
  }

  public static void CreateLinkIndex(ExpertServer.ExpServTask ti)
  {
    ExpertServer._InitLinkIndexes(ti);
    if (ti.savedLinks == null)
      return;
    ExpertServer._RegisterAllRelations(ti);
  }

  internal static void _InitLinkIndexes(ExpertServer.ExpServTask ti)
  {
    if (ti.linksIdIndex == null)
      ti.linksIdIndex = new Dictionary<long, int>();
    else
      ti.linksIdIndex.Clear();
    if (ti.linksPartIndex == null)
      ti.linksPartIndex = new Dictionary<long, List<int>>();
    else
      ti.linksPartIndex.Clear();
    if (ti.linksProjIndex == null)
      ti.linksProjIndex = new Dictionary<long, List<int>>();
    else
      ti.linksProjIndex.Clear();
  }

  internal static void _RegisterAllRelations(ExpertServer.ExpServTask ti)
  {
    int indexByName = ti.savedLinks.Columns.GetIndexByName("cad00033-306c-11d8-b4e9-00304f19f545");
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
      ExpertServer._RegisterRelation(ti, index, indexByName);
  }

  internal static void _RegisterRelation(ExpertServer.ExpServTask ti, int index, int colNum)
  {
    HybridRowExp savedLink = ti.savedLinks[index];
    long int64_1 = Convert.ToInt64(savedLink[colNum]);
    if (ti.linksIdIndex.ContainsKey(int64_1))
      return;
    long int64_2 = Convert.ToInt64(savedLink["cad00034-306c-11d8-b4e9-00304f19f545"]);
    long int64_3 = Convert.ToInt64(savedLink["cad00035-306c-11d8-b4e9-00304f19f545"]);
    ti.linksIdIndex.Add(int64_1, index);
    ti.AddProjIndex(int64_2, index);
    ti.AddPartIndex(int64_3, index);
  }

  internal static void _RegisterLastRelation(ExpertServer.ExpServTask ti, int colNum)
  {
    ExpertServer._RegisterRelation(ti, ti.savedLinks.RowsCount - 1, colNum);
  }

  private void AddContentsTags(HybridDictionary tags, HiddenContentsMode hMode, bool confOptions)
  {
    HiddenCompositionFiltrationMode compositionFiltrationMode = HiddenCompositionFiltrationMode.None;
    switch (hMode)
    {
      case HiddenContentsMode.HideOnlyHidden:
        compositionFiltrationMode = HiddenCompositionFiltrationMode.HideChilds;
        break;
      case HiddenContentsMode.HideHiddenAndRoots:
        compositionFiltrationMode = HiddenCompositionFiltrationMode.HideAll;
        break;
    }
    if (tags.Contains((object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"))
      tags[(object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}"] = (object) compositionFiltrationMode;
    else
      tags.Add((object) "{54C2DCB9-63C7-4736-867B-1EA7539B7645}", (object) compositionFiltrationMode);
    if (tags.Contains((object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"))
      tags[(object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}"] = (object) confOptions;
    else
      tags.Add((object) "{89F3DEDD-EE3A-4A42-ADD0-55BF26E622E1}", (object) confOptions);
    if (confOptions)
    {
      if (tags.Contains((object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"))
        tags[(object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}"] = (object) false;
      else
        tags.Add((object) "{0422E069-0A1D-4235-85E8-C52C3516CFC1}", (object) false);
    }
    if (hMode == HiddenContentsMode.ShowAllHidden)
      return;
    if (tags.Contains((object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"))
      tags[(object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}"] = (object) false;
    else
      tags.Add((object) "{325F5CDB-8B8E-4B2D-9AA9-5624A0A64D7E}", (object) false);
  }

  internal DataTable GetSostavData(
    long objId,
    IUserSession ius,
    IEnumerable<int> relations,
    IEnumerable<ConditionStructure> conds,
    IEnumerable<ColumnDescriptor> descs,
    ExpertServer.ExpServTask ti,
    HiddenContentsMode hcm = HiddenContentsMode.ShowAllHidden,
    bool confOptions = false,
    bool recursive = false,
    string filtrationOwnerID = "")
  {
    HybridDictionary tags = this.CloneDict(ti.filtr());
    this.AddContentsTags(tags, hcm, confOptions);
    if (confOptions)
    {
      QuickObjectInfo objectInfo1 = ius.GetObjectInfo(ti.RootObjID);
      QuickObjectInfo objectInfo2 = ius.GetObjectInfo(ti.RootObjID);
      long rel4Configurator = this.GetParentRel4Configurator(ius, objectInfo2.ID, ti);
      IDBRelation relation = rel4Configurator != 0L ? ius.GetRelation(rel4Configurator, false) : (IDBRelation) null;
      int relType = relation != null ? relation.RelationType : -1;
      tags.Add((object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}", (object) new RelationPair(0L, ti.RootObjID, objectInfo1.ObjectTypeID, rel4Configurator, ius.UserID, objectInfo2.ObjectID, relType, objectInfo2.ObjectTypeID));
    }
    return DataHelper.GetChildSostavData(objId, ius, relations, recursive, conds, descs, tags, filtrationOwnerID);
  }

  internal DataTable GetSostavData(
    IEnumerable<ObjInfoItem> objs,
    IUserSession ius,
    IEnumerable<int> relations,
    IEnumerable<ConditionStructure> conds,
    IEnumerable<ColumnDescriptor> descs,
    ExpertServer.ExpServTask ti,
    HiddenContentsMode hcm = HiddenContentsMode.ShowAllHidden,
    bool confOptions = false,
    bool recursive = false,
    string filtrationOwnerID = "")
  {
    HybridDictionary tags = this.CloneDict(ti.filtr());
    this.AddContentsTags(tags, hcm, confOptions);
    DataTable sostavData = (DataTable) null;
    if (confOptions)
    {
      QuickObjectInfo objectInfo1 = ius.GetObjectInfo(ti.RootObjID);
      foreach (ObjInfoItem objInfoItem in objs)
      {
        QuickObjectInfo objectInfo2 = ius.GetObjectInfo(objInfoItem.ObjectID);
        long rel4Configurator = this.GetParentRel4Configurator(ius, objectInfo2.ID, ti);
        IDBRelation relation = rel4Configurator != 0L ? ius.GetRelation(rel4Configurator, false) : (IDBRelation) null;
        int relType = relation != null ? relation.RelationType : -1;
        RelationPair relationPair = new RelationPair(0L, ti.RootObjID, objectInfo1.ObjectTypeID, rel4Configurator, ius.UserID, objectInfo2.ObjectID, relType, objectInfo2.ObjectTypeID);
        if (tags.Contains((object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"))
          tags[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) relationPair;
        else
          tags.Add((object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}", (object) relationPair);
        DataTable childSostavData = DataHelper.GetChildSostavData(objInfoItem.ObjectID, ius, relations, recursive, conds, descs, tags, filtrationOwnerID);
        if (childSostavData != null)
        {
          if (sostavData == null)
            sostavData = childSostavData;
          else
            sostavData.Merge(childSostavData);
        }
      }
    }
    else
      sostavData = DataHelper.GetChildSostavData(objs, ius, relations, recursive, conds, descs, tags, filtrationOwnerID);
    return sostavData;
  }

  private HybridDictionary CloneDict(HybridDictionary oldIdents)
  {
    if (oldIdents == null)
      return new HybridDictionary();
    HybridDictionary hybridDictionary = new HybridDictionary(oldIdents.Count);
    foreach (DictionaryEntry oldIdent in oldIdents)
      hybridDictionary.Add(oldIdent.Key, oldIdent.Value);
    return hybridDictionary;
  }

  internal DataTable GetPSostavData(
    long objId,
    IUserSession ius,
    IEnumerable<int> relations,
    IEnumerable<ConditionStructure> conds,
    IEnumerable<ColumnDescriptor> descs,
    ExpertServer.ExpServTask ti,
    HiddenContentsMode hcm = HiddenContentsMode.ShowAllHidden,
    bool confOptions = false,
    bool recursive = false,
    string filtrationOwnerID = "")
  {
    HybridDictionary tags = this.CloneDict(ti.filtr());
    this.AddContentsTags(tags, hcm, confOptions);
    if (confOptions)
    {
      QuickObjectInfo objectInfo = ius.GetObjectInfo(objId);
      long rel4Configurator = this.GetParentRel4Configurator(ius, objectInfo.ID, ti);
      tags.Add((object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}", (object) new RelationPair(0L, objectInfo.ObjectID, objectInfo.ObjectTypeID, rel4Configurator, ius.UserID, objectInfo.ObjectID, -1, objectInfo.ObjectTypeID));
    }
    return DataHelper.GetParentSostavData(objId, ius, relations, recursive, conds, descs, tags, filtrationOwnerID);
  }

  internal DataTable GetPSostavData(
    IEnumerable<ObjInfoItem> objs,
    IUserSession ius,
    IEnumerable<int> relations,
    IEnumerable<ConditionStructure> conds,
    IEnumerable<ColumnDescriptor> descs,
    ExpertServer.ExpServTask ti,
    HiddenContentsMode hcm = HiddenContentsMode.ShowAllHidden,
    bool confOptions = false,
    bool recursive = false,
    string filtrationOwnerID = "")
  {
    HybridDictionary tags1 = this.CloneDict(ti.filtr());
    this.AddContentsTags(tags1, hcm, confOptions);
    DataTable psostavData = (DataTable) null;
    if (confOptions)
    {
      foreach (ObjInfoItem objInfoItem in objs)
      {
        QuickObjectInfo objectInfo = ius.GetObjectInfo(objInfoItem.ObjectID);
        long rel4Configurator = this.GetParentRel4Configurator(ius, objectInfo.ID, ti);
        RelationPair relationPair = new RelationPair(0L, objectInfo.ObjectID, objectInfo.ObjectTypeID, rel4Configurator, ius.UserID, objectInfo.ObjectID, -1, objectInfo.ObjectTypeID);
        if (tags1.Contains((object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"))
          tags1[(object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}"] = (object) relationPair;
        else
          tags1.Add((object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}", (object) relationPair);
        DataTable parentSostavData = DataHelper.GetParentSostavData(objInfoItem.ObjectID, ius, relations, recursive, conds, descs, tags1, filtrationOwnerID);
        if (parentSostavData != null)
        {
          if (psostavData == null)
            psostavData = parentSostavData;
          else
            psostavData.Merge(parentSostavData);
        }
      }
    }
    else
    {
      Dictionary<long, HybridDictionary> tags2 = new Dictionary<long, HybridDictionary>();
      foreach (ObjInfoItem objInfoItem in objs)
        tags2.Add(objInfoItem.ObjectID, tags1);
      psostavData = DataHelper.GetParentSostavData(objs, ius, relations, recursive ? -1 : 1, new DBRecordSetParams(conds != null ? conds.ToArray<ConditionStructure>() : (ConditionStructure[]) null, descs.ToArray<ColumnDescriptor>()), (VersionsRule) null, filtrationOwnerID, tags2);
    }
    return psostavData;
  }

  internal long GetParentRel4Configurator(
    IUserSession ius,
    long objId,
    ExpertServer.ExpServTask ti)
  {
    if (ti.savedData == null || ti.savedLinks == null)
      return 0;
    HybridRowExp[] hybridRowExpArray = ti.savedLinksByPartId(objId);
    if (hybridRowExpArray == null || hybridRowExpArray.Length == 0)
      return 0;
    int indexByName = ti.savedLinks.Columns.GetIndexByName("cad00033-306c-11d8-b4e9-00304f19f545");
    return indexByName < 0 ? 0L : Convert.ToInt64(hybridRowExpArray[0][indexByName]);
  }

  private void AddMU_Columns(DataTable dt, ColumnDescriptor[] descs, IUserSession ius)
  {
    int num = 0;
    foreach (ColumnDescriptor desc in descs)
    {
      if (desc.OrderByID == 999)
      {
        Guid empty = Guid.Empty;
        Guid attrTypeGuid;
        try
        {
          attrTypeGuid = new Guid(dt.Columns[num].ColumnName);
        }
        catch
        {
          ++num;
          continue;
        }
        if (MetaDataHelper.GetAttributeType(attrTypeGuid).FieldType == FieldTypes.ftMeasured)
        {
          int count = dt.Columns.Count;
          dt.Columns.Add(dt.Columns[num].ColumnName + "_BS", typeof (double));
          dt.Columns.Add(dt.Columns[num].ColumnName + "_MU", typeof (int));
          foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
          {
            if (row[num].NotDBNull())
            {
              MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(Convert.ToString(row[num]));
              row[count] = (object) (measuredValue.Value * MeasureHelper.FindDescriptor(measuredValue.MeasureID).K);
              MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
              row[count + 1] = (object) MeasureHelper.FindBaseValue(descriptor).MeasureID;
            }
          }
        }
      }
      ++num;
    }
  }

  private void FilterDataTable(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    HybridTableExp dt,
    List<int> desiredObjTypes,
    ColumnDescriptor[] descs,
    TempFormula cond,
    bool _settingGlobalTable)
  {
    if (dt == null || dt.RowsCount == 0)
      return;
    if (desiredObjTypes.Count > 0)
    {
      int indexByName = dt.Columns.GetIndexByName("cad0002e-306c-11d8-b4e9-00304f19f545");
      if (indexByName >= 0)
      {
        int index = 0;
        while (index < dt.RowsCount)
        {
          int int32 = Convert.ToInt32(dt[index][indexByName]);
          if (ExpertServer.IsTypeDescendant(desiredObjTypes, int32))
            ++index;
          else
            dt.RemoveAt(index);
        }
      }
    }
    if (cond == null)
      return;
    this.FilterByCond(ti, ius, dt, cond, _settingGlobalTable);
  }

  private bool OptModTag(ExpertScriptMod modTag)
  {
    return modTag == ExpertScriptMod.modForFirst || modTag == ExpertScriptMod.modIfAll || modTag == ExpertScriptMod.modIfExists;
  }

  private object OptimizePass(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ExpertScriptMod modTag,
    OpParmObject op,
    ModParm mod,
    List<ConditionStructure> objConds,
    ref HybridTableExp resData,
    ColumnDescriptor[] descs,
    bool _settingGlobalTable)
  {
    if (resData == null)
      return (object) null;
    List<int> opObjTypes = this.GetOpObjTypes(op);
    HashSet<int> source = new HashSet<int>((IEnumerable<int>) opObjTypes);
    foreach (int parentTypeID in opObjTypes)
      source.UnionWith((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID));
    List<int> list = source.ToList<int>();
    if (op.cond != null)
      this.FilterByCond(ti, ius, resData, op.cond, _settingGlobalTable);
    return this.PerformModTag(ti, ius, modTag, mod, resData, _settingGlobalTable, list);
  }

  private object PerformModTag(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ExpertScriptMod modTag,
    ModParm mod,
    HybridTableExp resData,
    bool _settingGlobalTable,
    List<int> objTypes = null)
  {
    object obj = (object) null;
    if (this.OptModTag(modTag) && mod is ModParmFormula)
    {
      TempFormula tf = (mod as ModParmFormula).tf;
      if (tf != null || objTypes != null)
      {
        HybridTableExp dt = (HybridTableExp) resData.CloneShallow();
        bool flag = this.FilterByCond(ti, ius, dt, tf, _settingGlobalTable, objTypes);
        switch (modTag)
        {
          case ExpertScriptMod.modForFirst:
            if (dt.RowsCount > 0)
            {
              obj = (object) dt[0];
              break;
            }
            break;
          case ExpertScriptMod.modIfExists:
            if (dt.RowsCount > 0)
            {
              obj = (object) dt[0];
              break;
            }
            break;
          case ExpertScriptMod.modIfAll:
            if (!flag)
            {
              obj = (object) new HybridRowExp[dt.RowsCount];
              for (int index = 0; index < dt.RowsCount; ++index)
                ((HybridRowExp[]) obj)[index] = dt[index];
              break;
            }
            break;
        }
      }
    }
    return obj;
  }

  private void ReportClearingGlobalTable(ExpertServer.ExpServTask ti)
  {
    if (!ti.makeLog)
      return;
    IUserSession session = ti.GetSession();
    this.iLH.AddToTrace($"!!! GLOBAL TABLE INITIATED! User={session.UserName} Comp={session.ComputerName} Time= {DateTime.Now.ToShortTimeString()}", Intermech.Consts.traceAlways, this.logFileName);
  }

  private void New_MakeGlobalTable(int taskId, ref long[] context, GlobalNode gtn)
  {
    List<long> longList1 = new List<long>();
    if (context.Length == 0)
      return;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    IUserSession session = this.GetSession(task);
    task.multiAttrs = new HashSet<object>();
    List<ColumnDescriptor> columnDescriptors = this.GenerateAllColumnDescriptors(session, gtn, task);
    this.ResolveObjectTypes(task, gtn);
    OpParmGlobRoot op = (OpParmGlobRoot) gtn.op;
    if (op.excerptID != 0L)
    {
      IDBObject dbObject = session.GetObject(op.excerptID, false);
      if (dbObject != null)
      {
        List<ConditionStructure> conditions = new List<ConditionStructure>();
        IDBAttribute attributeById = dbObject.GetAttributeByID(ExpertConsts.Consts.attrObjTypeGuids);
        List<int> intList = new List<int>();
        foreach (object obj in attributeById.Values)
        {
          if (obj != null && obj is string)
          {
            string str = Convert.ToString(obj);
            if (GuidHelper.IsGuid(str))
            {
              int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(str));
              switch (objectTypeId)
              {
                case -1:
                case 0:
                  continue;
                default:
                  intList.Add(objectTypeId);
                  continue;
              }
            }
          }
        }
        if (intList.Count > 0)
        {
          ConditionStructure conditionStructure = new ConditionStructure(-7, RelationalOperators.In, (object) intList.ToArray(), (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Value);
          conditions.Add(conditionStructure);
        }
        ISelectionsService ss = this.GetSS(taskId);
        long num = Math.Abs(context[0]);
        IUserSession userSession = session;
        long excerptId = op.excerptID;
        long objectID = num;
        ConditionStructure[] conditionStructures = ss.GetConditionStructures((object) userSession, excerptId, objectID);
        conditions.AddRange((IEnumerable<ConditionStructure>) conditionStructures);
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
        };
        DataTable objectData = DataHelper.GetObjectData(-1, session, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns);
        if (objectData != null)
        {
          List<long> longList2 = new List<long>();
          foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
          {
            if (!(row[0] is DBNull))
              longList2.Add(Convert.ToInt64(row[0]));
          }
          context = longList2.ToArray();
        }
      }
    }
    if (op.ReplaceContextGUID != null && op.ReplaceContextGUID != "")
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(op.ReplaceContextGUID));
      for (int index = 0; index < context.Length; ++index)
      {
        IDBAttributable objectWithAttr = this.FindObjectWithAttr(taskId, session, context[index], -1, objectTypeId);
        if (objectWithAttr != null)
          context[index] = ExpertServer.AttributableId(objectWithAttr);
      }
    }
    task.savedData = new HybridTableExp();
    task.savedLinks = new HybridTableExp();
    task.dataObjIndex = new Dictionary<long, int>();
    this.ReportClearingGlobalTable(task);
    List<ColumnDescriptor> t_descs = new List<ColumnDescriptor>();
    foreach (ColumnDescriptor columnDescriptor in columnDescriptors)
    {
      if (columnDescriptor.AttributeSource == AttributeSourceTypes.Relation)
        t_descs.Add(columnDescriptor);
    }
    HybridColumnsExp.HybridColumnExp[] dataColumns1 = ExpertServer.GetDataColumns(session, t_descs);
    task.savedLinks.AddColumns(dataColumns1);
    t_descs.Clear();
    foreach (ColumnDescriptor columnDescriptor in columnDescriptors)
    {
      string str = columnDescriptor.AttributeID.ToString();
      if (columnDescriptor.AttributeSource != AttributeSourceTypes.Relation || str == "cad00033-306c-11d8-b4e9-00304f19f545" || str == "cad00035-306c-11d8-b4e9-00304f19f545" || str == "cad00034-306c-11d8-b4e9-00304f19f545")
        t_descs.Add(columnDescriptor);
    }
    HybridColumnsExp.HybridColumnExp[] dataColumns2 = ExpertServer.GetDataColumns(session, t_descs);
    task.savedData.AddColumns(dataColumns2);
    ExpertServer._InitLinkIndexes(task);
    List<long> objIds = new List<long>((IEnumerable<long>) context);
    if (op.ispWork != IspMode.ispNone)
      objIds = this._PerformIsps(session, task, objIds[0], op.linkTypeIDs, op.ispWork);
    List<ObjInfoItem> oiList = this.GetOIList(session, objIds);
    List<ColumnDescriptor> descrsForObjTypes = this.GetColDescrsForObjTypes(task, new List<int>()
    {
      oiList[0].ObjTypeID
    }, gtn);
    for (int index = descrsForObjTypes.Count - 1; index >= 0; --index)
    {
      if (descrsForObjTypes[index].AttributeSource == AttributeSourceTypes.Relation && descrsForObjTypes[index].AttributeID.ToString() != "cad00034-306c-11d8-b4e9-00304f19f545" && descrsForObjTypes[index].AttributeID.ToString() != "cad00035-306c-11d8-b4e9-00304f19f545")
        descrsForObjTypes.RemoveAt(index);
    }
    task.RootObjID = objIds[0];
    for (int index1 = 0; index1 < objIds.Count; ++index1)
    {
      long num = objIds[index1];
      IDBObject dbObject = session.GetObject(num, false);
      if (dbObject != null)
      {
        HybridRowExp hrow = task.savedData.NewRow();
        for (int index2 = 0; index2 < descrsForObjTypes.Count; ++index2)
        {
          if (index2 == 0)
          {
            hrow[0] = (object) dbObject.ObjectID;
          }
          else
          {
            string str = descrsForObjTypes[index2].AttributeID.ToString();
            switch (str)
            {
              case "cad00035-306c-11d8-b4e9-00304f19f545":
                hrow[str] = (object) dbObject.ID;
                continue;
              case "cad00034-306c-11d8-b4e9-00304f19f545":
                hrow[str] = (object) dbObject.ObjectID;
                continue;
              default:
                int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(str));
                if (attributeTypeId != 0)
                {
                  object[] valuesById = dbObject.GetValuesByID(attributeTypeId, false);
                  if (valuesById != null)
                  {
                    hrow[str] = valuesById.Length != 1 ? (object) valuesById : valuesById[0];
                    continue;
                  }
                  continue;
                }
                continue;
            }
          }
        }
        hrow["cad0002e-306c-11d8-b4e9-00304f19f545"] = (object) dbObject.ObjectType;
        hrow["cad0002a-306c-11d8-b4e9-00304f19f545"] = (object) dbObject.ID;
        hrow["cad00130-306c-11d8-b4e9-00304f19f545"] = (object) dbObject.GUID;
        hrow["cad00047-306c-11d8-b4e9-00304f19f545"] = (object) dbObject.Caption;
        task.dataObjIndex.Add(num, task.savedData.RowsCount);
        task.savedData.Add(hrow);
      }
    }
    if (op.ispWork != IspMode.ispNone)
    {
      this.SortIsps(task);
      this.MakePrimaryIspFirst(session, task);
      this.InnerSetParm(task, ExpertConsts.Consts.attrIsLink, (object) task.HasVariableParts);
      this.InnerSetParm(task, ExpertConsts.Consts.attrIspList, (object) task.ispList);
      this.InnerSetParm(task, ExpertConsts.Consts.attrIspNum, (object) task.ispList.Count);
    }
    if (task.objectIDs == null)
      task.objectIDs = new HashSet<long>();
    if (task.linkIDs == null)
      task.linkIDs = new HashSet<long>();
    List<ExpertServer.PieceData> portion = new List<ExpertServer.PieceData>();
    if (op.linkTypeIDs != null)
    {
      List<List<int>> objLinkTypes = this._GetObjLinkTypes((ILinkObjTypes) op);
      for (int index = 0; index < op.linkTypeIDs.Count; ++index)
      {
        int linkTypeId = op.linkTypeIDs[index];
        List<int> objTypeIDs = objLinkTypes[index];
        ExpertServer.PieceData pieceData = this._Piece4OneRelType(task, linkTypeId, objTypeIDs, oiList, (ILinkObjTypes) op);
        if (pieceData != null)
        {
          pieceData.cond = op.afterFilter;
          portion.Add(pieceData);
        }
      }
    }
    Dictionary<int, HashSet<long>> dictionary = portion.Count > 0 ? this.ProcessDataPortion(task, portion, gtn) : (Dictionary<int, HashSet<long>>) null;
    List<List<int>> intListList = new List<List<int>>();
    for (int index = 0; index < gtn.Items.Count; ++index)
      intListList.Add(new List<int>());
    List<ObjInfoItem> sourceObjs = new List<ObjInfoItem>();
    while (portion.Count > 0)
    {
      for (int index = 0; index < intListList.Count; ++index)
        intListList[index].Clear();
      foreach (int key in dictionary.Keys)
      {
        int index = -1;
        if (task.objTypesToNodes.TryGetValue(key, out index))
          intListList[index].Add(key);
      }
      portion.Clear();
      for (int index = 0; index < intListList.Count; ++index)
      {
        if (intListList[index].Count != 0)
        {
          GlobalTypeNode gtn1 = (GlobalTypeNode) gtn.Items[index];
          sourceObjs.Clear();
          foreach (int num in intListList[index])
          {
            foreach (long objectId in dictionary[num])
              sourceObjs.Add(new ObjInfoItem(objectId, num));
          }
          this._NewPerformGTNode(task, sourceObjs, gtn1, portion);
          if (this.abortedTasksContains(taskId))
            return;
        }
      }
      dictionary = portion.Count > 0 ? this.ProcessDataPortion(task, portion, gtn) : (Dictionary<int, HashSet<long>>) null;
    }
    task.dataPartIndex = new Dictionary<long, int>();
    for (int index = 0; index < task.savedData.RowsCount; ++index)
    {
      long int64 = Convert.ToInt64(task.savedData[index]["cad00035-306c-11d8-b4e9-00304f19f545"]);
      if (!task.dataPartIndex.ContainsKey(int64))
        task.dataPartIndex.Add(int64, index);
    }
    this.PerformSubstitutes(task, session);
    this.ReplaceMemos(session, task.savedData, false);
    this.ReplaceMemos(session, task.savedLinks, true);
    task.DataCache.FillCacheData(task.savedData);
    this.ReplaceQuantities(task);
    task.objectIDs = (HashSet<long>) null;
    task.linkIDs = (HashSet<long>) null;
  }

  private void MergeTables(ExpertServer.ExpServTask ti, HybridTableExp locDT)
  {
    ti.savedLinks.Merge(locDT);
    for (int index = locDT.RowsCount - 1; index >= 0; --index)
    {
      long int64 = Convert.ToInt64(locDT[index][0]);
      if (ti.objectIDs.Contains(Math.Abs(int64)))
        locDT.RemoveAt(index);
      else
        ti.objectIDs.Add(Math.Abs(int64));
    }
    ti.savedData.Merge(locDT);
  }

  private List<ColumnDescriptor> GenerateAllColumnDescriptors(
    IUserSession ius,
    GlobalNode rootNode,
    ExpertServer.ExpServTask ti)
  {
    List<ColumnDescriptor> res = new List<ColumnDescriptor>();
    List<string> stringList = new List<string>();
    List<string> attrList = new List<string>();
    res.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 0));
    stringList.Add("cad00029-306c-11d8-b4e9-00304f19f545");
    res.Add(new ColumnDescriptor((object) new Guid("cad00033-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 1));
    res.Add(new ColumnDescriptor((object) new Guid("cad00034-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 2));
    res.Add(new ColumnDescriptor((object) new Guid("cad00035-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 3));
    stringList.Add("cad00033-306c-11d8-b4e9-00304f19f545");
    stringList.Add("cad00034-306c-11d8-b4e9-00304f19f545");
    stringList.Add("cad00035-306c-11d8-b4e9-00304f19f545");
    res.Add(new ColumnDescriptor((object) new Guid("cad00036-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, 4));
    stringList.Add("cad00036-306c-11d8-b4e9-00304f19f545");
    res.Add(new ColumnDescriptor((object) new Guid("cad0002e-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, res.Count + 1));
    stringList.Add("cad0002e-306c-11d8-b4e9-00304f19f545");
    res.Add(new ColumnDescriptor((object) new Guid(ExpertAttrGUIDs.attrSorting), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, res.Count + 1));
    stringList.Add(ExpertAttrGUIDs.attrSorting);
    res.Add(new ColumnDescriptor((object) new Guid("cad0002a-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Guid, SortOrders.NONE, res.Count + 1));
    stringList.Add("cad0002a-306c-11d8-b4e9-00304f19f545");
    res.Add(new ColumnDescriptor((object) new Guid("cad00130-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, res.Count + 1));
    stringList.Add("cad00130-306c-11d8-b4e9-00304f19f545");
    res.Add(new ColumnDescriptor((object) new Guid("cad00047-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, res.Count + 1));
    stringList.Add("cad00047-306c-11d8-b4e9-00304f19f545");
    if (rootNode == null)
      return res;
    rootNode.descs = new List<ColumnDescriptor>();
    rootNode.CreateColumnDescs(stringList);
    if (rootNode.descs != null)
    {
      for (int index = rootNode.descs.Count - 1; index >= 0; --index)
      {
        Guid attributeId = (Guid) rootNode.descs[index].AttributeID;
        if (!stringList.Contains(attributeId.ToString()))
        {
          stringList.Add(attributeId.ToString());
          if (attributeId.ToString() != "cad0004b-306c-11d8-b4e9-00304f19f545")
            res.Add(rootNode.descs[index]);
        }
        if (!ti.multiAttrs.Contains((object) attributeId) && this.IsAttrMulti(ius, (object) attributeId))
          ti.multiAttrs.Add((object) attributeId);
      }
      for (int index = 0; index < 10; ++index)
        rootNode.descs.Insert(index, res[index]);
    }
    foreach (GlobalTypeNode globalTypeNode in rootNode.Items)
    {
      globalTypeNode.CreateColumnDescs();
      if (globalTypeNode.descs != null)
      {
        for (int index = globalTypeNode.descs.Count - 1; index >= 0; --index)
        {
          Guid attributeId = (Guid) globalTypeNode.descs[index].AttributeID;
          string str = attributeId.ToString();
          if (stringList.Contains(str))
            globalTypeNode.descs.RemoveAt(index);
          else if (!attrList.Contains(str))
          {
            attrList.Add(str);
            if (str != "cad0004b-306c-11d8-b4e9-00304f19f545")
              res.Add(globalTypeNode.descs[index]);
          }
          if (!ti.multiAttrs.Contains((object) attributeId) && this.IsAttrMulti(ius, (object) attributeId))
            ti.multiAttrs.Add((object) attributeId);
        }
      }
    }
    ti.fileAttrs = (List<string>) null;
    if (stringList.Contains("cad0004b-306c-11d8-b4e9-00304f19f545") || attrList.Contains("cad0004b-306c-11d8-b4e9-00304f19f545"))
    {
      if (ti.fileAttrs == null)
        ti.fileAttrs = new List<string>()
        {
          "cad00702-306c-11d8-b4e9-00304f19f545",
          "cad001ae-306c-11d8-b4e9-00304f19f545",
          "cad014af-306c-11d8-b4e9-00304f19f545",
          "cadd9518-306c-11d8-b4e9-00304f19f545"
        };
      if (stringList.Contains("cad0004b-306c-11d8-b4e9-00304f19f545"))
        this.AddFileAttributes(stringList, ti.fileAttrs, res);
      else if (attrList.Contains("cad0004b-306c-11d8-b4e9-00304f19f545"))
        this.AddFileAttributes(attrList, ti.fileAttrs, res);
    }
    return res;
  }

  private void AddFileAttributes(
    List<string> attrList,
    List<string> fAttrs,
    List<ColumnDescriptor> res)
  {
    foreach (string fAttr in fAttrs)
    {
      if (!attrList.Contains(fAttr))
      {
        attrList.Add(fAttr);
        res.Add(new ColumnDescriptor((object) new Guid(fAttr), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, res.Count + 1));
      }
    }
  }

  private bool IsAttrMulti(IUserSession ius, object attrId)
  {
    IMSAttributeType imsAttributeType = (IMSAttributeType) null;
    if (attrId is Guid attrTypeGuid)
      imsAttributeType = MetaDataHelper.GetAttributeType(attrTypeGuid);
    else if (attrId is int attrTypeID)
      imsAttributeType = MetaDataHelper.GetAttributeType(attrTypeID);
    if (imsAttributeType == null)
      return false;
    return imsAttributeType.MultiValueMode == MultiValueModes.MultiValues || imsAttributeType.MultiValueMode == MultiValueModes.MultiValuesFromList;
  }

  private void ResolveObjectTypes(ExpertServer.ExpServTask ti, GlobalNode rootNode)
  {
    Dictionary<int, List<int>> fixupList = new Dictionary<int, List<int>>();
    for (int index = 0; index < rootNode.Items.Count; ++index)
    {
      GlobalTypeNode globalTypeNode = (GlobalTypeNode) rootNode.Items[index];
      if (!globalTypeNode.label.StartsWith("#"))
        globalTypeNode.InitObjTypes(index, fixupList);
    }
    ti.objTypesToNodes = new Dictionary<int, int>();
    if (fixupList.Keys.Count > 0)
    {
      foreach (int key in fixupList.Keys)
      {
        List<int> intList = fixupList[key];
        int num = intList[0];
        for (int index = 1; index < intList.Count; ++index)
        {
          if (intList[index] < num)
            num = intList[index];
        }
        while (num >= 100000)
          num -= 100000;
        ti.objTypesToNodes.Add(key, num);
      }
    }
    ti.objAttrs4ObjTypes = new Dictionary<int, List<ColumnDescriptor>>();
    ti.relAttrs4ObjTypes = new Dictionary<int, List<ColumnDescriptor>>();
    for (int index1 = 0; index1 < rootNode.Items.Count; ++index1)
    {
      GlobalTypeNode globalTypeNode = (GlobalTypeNode) rootNode.Items[index1];
      if (!globalTypeNode.label.StartsWith("#"))
      {
        List<int> intList = new List<int>();
        foreach (int key in ti.objTypesToNodes.Keys)
        {
          if (ti.objTypesToNodes[key] == index1)
            intList.Add(key);
        }
        if (intList.Count != 0)
        {
          List<ColumnDescriptor> columnDescriptorList1 = (List<ColumnDescriptor>) null;
          List<ColumnDescriptor> columnDescriptorList2 = (List<ColumnDescriptor>) null;
          if (globalTypeNode.descs != null)
          {
            for (int index2 = 0; index2 < globalTypeNode.descs.Count; ++index2)
            {
              ColumnDescriptor desc = globalTypeNode.descs[index2];
              if (desc.AttributeSource == AttributeSourceTypes.Relation)
              {
                if (columnDescriptorList2 == null)
                  columnDescriptorList2 = new List<ColumnDescriptor>();
                columnDescriptorList2.Add(desc);
              }
              else
              {
                if (columnDescriptorList1 == null)
                  columnDescriptorList1 = new List<ColumnDescriptor>();
                columnDescriptorList1.Add(desc);
              }
            }
          }
          if (columnDescriptorList1 != null)
          {
            foreach (int key in intList)
              ti.objAttrs4ObjTypes.Add(key, columnDescriptorList1);
          }
          if (columnDescriptorList2 != null)
          {
            foreach (int key in intList)
              ti.relAttrs4ObjTypes.Add(key, columnDescriptorList2);
          }
        }
      }
    }
    List<ColumnDescriptor> columnDescriptorList3 = new List<ColumnDescriptor>();
    List<ColumnDescriptor> columnDescriptorList4 = new List<ColumnDescriptor>();
    for (int index = 0; index < rootNode.descs.Count; ++index)
    {
      ColumnDescriptor desc = rootNode.descs[index];
      if (desc.AttributeSource == AttributeSourceTypes.Relation)
        columnDescriptorList4.Add(desc);
      else
        columnDescriptorList3.Add(desc);
    }
    ti.objAttrs4ObjTypes.Add(-1, columnDescriptorList3);
    ti.relAttrs4ObjTypes.Add(-1, columnDescriptorList4);
  }

  private ExpertServer.PieceData _Piece4OneRelType(
    ExpertServer.ExpServTask ti,
    int relTypeID,
    List<int> objTypeIDs,
    List<ObjInfoItem> sourceObjs,
    ILinkObjTypes ilot)
  {
    IUserSession session = this.GetSession(ti);
    bool flag = relTypeID > 99000;
    if (flag)
      relTypeID -= 100000;
    session.GetRelationCollection(relTypeID);
    ConditionStructure[] instance = objTypeIDs.Count == 0 ? (ConditionStructure[]) null : (ConditionStructure[]) Array.CreateInstance(typeof (ConditionStructure), 1);
    if (instance != null)
      instance[0] = new ConditionStructure(-7, RelationalOperators.In, (object) objTypeIDs.ToArray(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Value);
    List<ColumnDescriptor> descs = new List<ColumnDescriptor>();
    List<ColumnDescriptor> objAttrs4ObjType = ti.objAttrs4ObjTypes[-1];
    for (int index = 0; index < 2; ++index)
      descs.Add(objAttrs4ObjType[index]);
    List<ColumnDescriptor> relAttrs4ObjType = ti.relAttrs4ObjTypes[-1];
    HashSet<Guid> guidSet = new HashSet<Guid>();
    foreach (ColumnDescriptor columnDescriptor in relAttrs4ObjType)
    {
      Guid attributeId = (Guid) columnDescriptor.AttributeID;
      if (!guidSet.Contains(attributeId))
      {
        descs.Add(columnDescriptor);
        guidSet.Add(attributeId);
      }
    }
    foreach (int objTypeId in objTypeIDs)
    {
      List<ColumnDescriptor> columnDescriptorList = (List<ColumnDescriptor>) null;
      ti.relAttrs4ObjTypes.TryGetValue(objTypeId, out columnDescriptorList);
      if (columnDescriptorList != null)
      {
        foreach (ColumnDescriptor columnDescriptor in columnDescriptorList)
        {
          Guid attributeId = (Guid) columnDescriptor.AttributeID;
          if (!guidSet.Contains(attributeId))
          {
            descs.Add(columnDescriptor);
            guidSet.Add(attributeId);
          }
        }
      }
    }
    DataTable tbl;
    if (!flag)
      tbl = this.GetSostavData((IEnumerable<ObjInfoItem>) sourceObjs, session, (IEnumerable<int>) new int[1]
      {
        relTypeID
      }, (IEnumerable<ConditionStructure>) instance, (IEnumerable<ColumnDescriptor>) descs, ti, ilot.hcMode, (ilot.UseConfigOptions ? 1 : 0) != 0, filtrationOwnerID: ti.verRuleOwnerId);
    else
      tbl = this.GetPSostavData((IEnumerable<ObjInfoItem>) sourceObjs, session, (IEnumerable<int>) new int[1]
      {
        relTypeID
      }, (IEnumerable<ConditionStructure>) instance, (IEnumerable<ColumnDescriptor>) descs, ti, ilot.hcMode, (ilot.UseConfigOptions ? 1 : 0) != 0, filtrationOwnerID: ti.verRuleOwnerId);
    if (tbl == null)
      return (ExpertServer.PieceData) null;
    int columnIndex = tbl.Columns.IndexOf("cad00033-306c-11d8-b4e9-00304f19f545");
    if (columnIndex >= 0)
    {
      for (int index = tbl.Rows.Count - 1; index >= 0; --index)
      {
        DataRow row = tbl.Rows[index];
        Convert.ToInt64(row[0]);
        long int64 = Convert.ToInt64(row[columnIndex]);
        if (ti.linkIDs.Contains(Math.Abs(int64)))
          tbl.Rows.RemoveAt(index);
      }
    }
    ExpertServer.PieceData pieceData = new ExpertServer.PieceData(tbl);
    if (flag)
      pieceData.searchDown = false;
    return pieceData;
  }

  internal void ReplaceMultiAttr(IUserSession ius, HybridRowExp dr, bool byRel, Guid attrGuid)
  {
    int indexByName = dr.Columns.GetIndexByName(attrGuid.ToString());
    if (indexByName < 0)
      return;
    IDBAttributable relation;
    if (byRel)
    {
      long int64 = Convert.ToInt64(dr["cad00033-306c-11d8-b4e9-00304f19f545"]);
      relation = (IDBAttributable) ius.GetRelation(int64, false);
    }
    else
    {
      long int64 = Convert.ToInt64(dr["cad00029-306c-11d8-b4e9-00304f19f545"]);
      relation = (IDBAttributable) ius.GetObject(int64, false);
    }
    if (relation == null)
      return;
    IDBAttribute attributeByGuid = relation.GetAttributeByGuid(attrGuid);
    if (attributeByGuid == null)
      return;
    object[] values = attributeByGuid.Values;
    int num = (int) DataTypeConvertor.AttrType2DataType(attributeByGuid.DataType, attributeByGuid.AttributeID);
    ArrayHolder arrayHolder = new ArrayHolder(values.Length, 1);
    for (int x = 0; x < values.Length; ++x)
      arrayHolder[x, 0] = values[x];
    dr[indexByName] = (object) arrayHolder;
  }

  internal void LoadMemoAttr(IUserSession ius, HybridRowExp dr, bool byRel, Guid attrGuid)
  {
    int indexByName = dr.Columns.GetIndexByName(attrGuid.ToString());
    if (indexByName < 0)
      return;
    IDBAttributable relation;
    if (byRel)
    {
      long int64 = Convert.ToInt64(dr["cad00033-306c-11d8-b4e9-00304f19f545"]);
      relation = (IDBAttributable) ius.GetRelation(int64, false);
    }
    else
    {
      long int64 = Convert.ToInt64(dr["cad00029-306c-11d8-b4e9-00304f19f545"]);
      relation = (IDBAttributable) ius.GetObject(int64, false);
    }
    if (relation == null)
      return;
    IDBAttribute attributeByGuid = relation.GetAttributeByGuid(attrGuid);
    if (attributeByGuid == null)
      return;
    string str = Convert.ToString(attributeByGuid.Value);
    dr[indexByName] = (object) str;
  }

  private Dictionary<int, HashSet<long>> ProcessDataPortion(
    ExpertServer.ExpServTask ti,
    List<ExpertServer.PieceData> portion,
    GlobalNode root)
  {
    OpParmGlobRoot op = (OpParmGlobRoot) root.op;
    List<ColumnDescriptor> descs = root.descs;
    Dictionary<int, HashSet<long>> dictionary1 = new Dictionary<int, HashSet<long>>();
    Dictionary<long, List<ExpertServer.PortionRowInfo>> dictionary2 = new Dictionary<long, List<ExpertServer.PortionRowInfo>>();
    IUserSession session = this.GetSession(ti);
    HybridColumnsExp columns = new HybridColumnsExp(ti.savedData.Columns);
    for (int index = 0; index < ti.savedLinks.Columns.Count; ++index)
      columns.AddDuplicate(ti.savedLinks.Columns[index]);
    int indexByName = ti.savedLinks.Columns.GetIndexByName("cad00033-306c-11d8-b4e9-00304f19f545");
    for (int index1 = 0; index1 < portion.Count; ++index1)
    {
      for (int index2 = 0; index2 < portion[index1].dt.Rows.Count; ++index2)
      {
        DataRow row1 = portion[index1].dt.Rows[index2];
        long int64_1 = Convert.ToInt64(row1[0]);
        long int64_2 = Convert.ToInt64(row1[indexByName]);
        if (!ti.linksIdIndex.ContainsKey(int64_2))
        {
          HybridRowExp Row1 = ti.savedDataByObjId(int64_1);
          if (Row1 != null)
          {
            HybridRowExp hybridRowExp = ti.savedLinks.NewRow();
            this.CopyRowFields(hybridRowExp, row1);
            bool flag = true;
            DoubleLinkRowExp row2 = (DoubleLinkRowExp) null;
            if (portion[index1].cond != null)
            {
              row2 = new DoubleLinkRowExp(columns, Row1, hybridRowExp);
              if (!ti.CheckRowCond(int64_1, (HybridRowExp) row2, portion[index1].cond))
                flag = false;
            }
            if (flag && op.globalFilter != null)
            {
              if (row2 == null)
                row2 = new DoubleLinkRowExp(columns, Row1, hybridRowExp);
              if (!ti.CheckRowCond(int64_1, (HybridRowExp) row2, op.globalFilter))
                flag = false;
            }
            if (flag)
            {
              ti.savedLinks.Add(hybridRowExp);
              ExpertServer._RegisterLastRelation(ti, indexByName);
              long int64_3 = Convert.ToInt64(hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"]);
              ti.linkIDs.Add(Math.Abs(int64_3));
            }
          }
          else
          {
            int int32 = Convert.ToInt32(row1["cad0002e-306c-11d8-b4e9-00304f19f545"]);
            HashSet<long> longSet = (HashSet<long>) null;
            if (!dictionary1.TryGetValue(int32, out longSet))
            {
              longSet = new HashSet<long>();
              dictionary1.Add(int32, longSet);
            }
            if (!longSet.Contains(int64_1))
              longSet.Add(int64_1);
            List<ExpertServer.PortionRowInfo> portionRowInfoList = (List<ExpertServer.PortionRowInfo>) null;
            if (!dictionary2.TryGetValue(int64_1, out portionRowInfoList))
            {
              portionRowInfoList = new List<ExpertServer.PortionRowInfo>();
              dictionary2[int64_1] = portionRowInfoList;
            }
            portionRowInfoList.Add(new ExpertServer.PortionRowInfo(row1, portion[index1].cond));
          }
        }
      }
    }
    List<long> objIdList = new List<long>();
    foreach (int key in dictionary1.Keys)
    {
      HashSet<long> longSet = dictionary1[key];
      List<ColumnDescriptor> columnDescriptorList1 = new List<ColumnDescriptor>();
      HashSet<string> stringSet = new HashSet<string>();
      bool flag1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objSign).Contains(key);
      bool flag2 = false;
      foreach (ColumnDescriptor columnDescriptor in descs)
      {
        string str = Convert.ToString(columnDescriptor.AttributeID);
        if (columnDescriptor.AttributeSource != AttributeSourceTypes.Relation && !stringSet.Contains(str))
        {
          stringSet.Add(str);
          if (flag1 && str == ExpertAttrGUIDs.signStatus)
            flag2 = true;
          else
            columnDescriptorList1.Add(columnDescriptor);
        }
      }
      List<ColumnDescriptor> columnDescriptorList2 = (List<ColumnDescriptor>) null;
      ti.objAttrs4ObjTypes.TryGetValue(key, out columnDescriptorList2);
      List<(Guid, FieldTypes)> valueTupleList = new List<(Guid, FieldTypes)>();
      if (columnDescriptorList2 != null)
      {
        foreach (ColumnDescriptor columnDescriptor in columnDescriptorList2)
        {
          IMSAttributeType imsAttributeType = columnDescriptor.AttributeID is int ? MetaDataHelper.GetAttributeType((int) columnDescriptor.AttributeID) : MetaDataHelper.GetAttributeType((Guid) columnDescriptor.AttributeID);
          if (imsAttributeType.MultiValueMode == MultiValueModes.MultiValues || imsAttributeType.MultiValueMode == MultiValueModes.MultiValuesFromList || imsAttributeType.FieldType == FieldTypes.ftMemo)
          {
            valueTupleList.Add((imsAttributeType.AttributeGuid, imsAttributeType.FieldType));
          }
          else
          {
            string str = Convert.ToString(columnDescriptor.AttributeID);
            if (columnDescriptor.AttributeSource != AttributeSourceTypes.Relation && !stringSet.Contains(str))
            {
              stringSet.Add(str);
              if (flag1 && str == ExpertAttrGUIDs.signStatus)
                flag2 = true;
              else
                columnDescriptorList1.Add(columnDescriptor);
            }
          }
        }
      }
      bool flag3 = flag1 & flag2;
      DBRecordSetParams dbRsp = new DBRecordSetParams((ConditionStructure[]) null, columnDescriptorList1.ToArray(), recordCount: -2);
      objIdList.Clear();
      foreach (long num in longSet)
        objIdList.Add(num);
      DataTable objectData = DataHelper.GetObjectData(key, session, dbRsp, (IEnumerable<long>) objIdList);
      if (objectData != null)
      {
        if (flag3)
        {
          objectData.Columns.Add(new DataColumn(ExpertAttrGUIDs.signStatus, typeof (int)));
          IDBRelationCollection relationCollection = session.GetRelationCollection(ExpertConsts.Consts.linkSign);
          relationCollection.LocalTypesMode = true;
          foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
          {
            long int64_4 = Convert.ToInt64(row[0]);
            IDBObject dbObject = session.GetObject(int64_4, false);
            if (dbObject != null)
            {
              object[] valuesById1 = dbObject.GetValuesByID(ExpertConsts.Consts.attrModContDate, false);
              if (valuesById1 != null && valuesById1.Length != 0)
              {
                DateTime dateTime1 = Convert.ToDateTime(valuesById1[0]);
                DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
                {
                  new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
                });
                DataTable dataTable = relationCollection.EntersInVersion(paramSet, int64_4);
                if (dataTable != null && dataTable.Rows.Count != 0)
                {
                  long int64_5 = Convert.ToInt64(dataTable.Rows[0][0]);
                  object[] valuesById2 = session.GetObject(int64_5, false).GetValuesByID(ExpertConsts.Consts.attrModContDate, false);
                  if (valuesById2 != null && valuesById2.Length != 0)
                  {
                    DateTime dateTime2 = Convert.ToDateTime(valuesById2[0]);
                    SignStatuses signStatuses = SignHelper.TranslateStatus(session, int64_5, int64_4, key, dateTime2, dateTime1);
                    row[ExpertAttrGUIDs.signStatus] = (object) (int) signStatuses;
                  }
                }
              }
            }
          }
        }
        HybridRowExp hybridRowExp1 = (HybridRowExp) null;
        HybridRowExp hybridRowExp2 = (HybridRowExp) null;
        int count = objectData.Rows.Count;
        for (int index = 0; index < count; ++index)
        {
          DataRow row3 = objectData.Rows[index];
          long int64_6 = Convert.ToInt64(row3[0]);
          List<ExpertServer.PortionRowInfo> portionRowInfoList = (List<ExpertServer.PortionRowInfo>) null;
          if (dictionary2.TryGetValue(int64_6, out portionRowInfoList))
          {
            foreach (ExpertServer.PortionRowInfo portionRowInfo in portionRowInfoList)
            {
              DataRow drow = portionRowInfo.drow;
              if (hybridRowExp1 == null)
                hybridRowExp1 = ti.savedData.NewRow();
              if (hybridRowExp2 == null)
                hybridRowExp2 = ti.savedLinks.NewRow();
              if (drow != null)
              {
                this.CopyRowFields(hybridRowExp2, drow);
                this.CopyRowFields(hybridRowExp1, drow);
              }
              this.CopyRowFields(hybridRowExp1, row3);
              if (valueTupleList.Count > 0)
              {
                foreach ((Guid attrGuid, FieldTypes fieldTypes) in valueTupleList)
                {
                  if (attrGuid.ToString() == "cad0004b-306c-11d8-b4e9-00304f19f545")
                  {
                    if (ti.fileAttrs != null)
                    {
                      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
                      BlobReaderStream blobReaderStream = new BlobReaderStream(int64_6, AttributableElements.Object, attributeTypeId, 0, 0, session);
                      BlobInformation blobInformation = blobReaderStream.BlobInformation;
                      string fileAttr1 = ti.fileAttrs[0];
                      string fileAttr2 = ti.fileAttrs[1];
                      string fileAttr3 = ti.fileAttrs[2];
                      string fileAttr4 = ti.fileAttrs[3];
                      hybridRowExp1[fileAttr2] = (object) blobInformation.RealFileSize;
                      hybridRowExp1[fileAttr1] = (object) blobInformation.ModifyDate;
                      hybridRowExp1[fileAttr4] = (object) blobInformation.FileName;
                      ChecksumClass checksumClass = new Crc32Checksum().Compute((Stream) blobReaderStream);
                      if (checksumClass != null)
                        hybridRowExp1[fileAttr3] = (object) Convert.ToInt64(checksumClass.Value);
                    }
                  }
                  else if (fieldTypes == FieldTypes.ftMemo)
                    this.LoadMemoAttr(session, hybridRowExp1, false, attrGuid);
                  else
                    this.ReplaceMultiAttr(session, hybridRowExp1, false, attrGuid);
                }
              }
              bool flag4 = true;
              TempFormula cond = portionRowInfo.cond;
              DoubleLinkRowExp row4 = (DoubleLinkRowExp) null;
              if (cond != null)
              {
                row4 = new DoubleLinkRowExp(columns, hybridRowExp1, hybridRowExp2);
                if (!ti.CheckRowCond(int64_6, (HybridRowExp) row4, cond))
                  flag4 = false;
              }
              if (flag4 && op.globalFilter != null)
              {
                if (row4 == null)
                  row4 = new DoubleLinkRowExp(columns, hybridRowExp1, hybridRowExp2);
                if (!ti.CheckRowCond(int64_6, (HybridRowExp) row4, op.globalFilter))
                  flag4 = false;
              }
              if (flag4)
              {
                if (!ti.dataObjIndex.ContainsKey(int64_6))
                {
                  hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"] = hybridRowExp1["cad0002a-306c-11d8-b4e9-00304f19f545"];
                  ti.savedData.Add(hybridRowExp1);
                  ti.dataObjIndex.Add(int64_6, ti.savedData.RowsCount - 1);
                  hybridRowExp1 = (HybridRowExp) null;
                }
                else
                  hybridRowExp1.PurgeData();
                ti.savedLinks.Add(hybridRowExp2);
                ExpertServer._RegisterLastRelation(ti, indexByName);
                long int64_7 = Convert.ToInt64(hybridRowExp2["cad00033-306c-11d8-b4e9-00304f19f545"]);
                ti.linkIDs.Add(Math.Abs(int64_7));
                hybridRowExp2 = (HybridRowExp) null;
              }
              else
              {
                hybridRowExp1.PurgeData();
                hybridRowExp2.PurgeData();
                longSet.Remove(int64_6);
              }
            }
          }
        }
      }
    }
    return dictionary1;
  }

  private void CopyRowFields(HybridRowExp hRow, DataRow row)
  {
    DataColumnCollection columns = row.Table.Columns;
    for (int index = 0; index < columns.Count; ++index)
    {
      string columnName = columns[index].ColumnName;
      int indexByName = hRow.Columns.GetIndexByName(columnName);
      if (indexByName >= 0)
        hRow[indexByName] = row[index];
    }
  }

  private void _NewPerformGTNode(
    ExpertServer.ExpServTask ti,
    List<ObjInfoItem> sourceObjs,
    GlobalTypeNode gtn,
    List<ExpertServer.PieceData> portion)
  {
    OpParmGlobForType op = (OpParmGlobForType) gtn.op;
    if (op.linkTypeIDs == null || op.linkTypeIDs.Count == 0)
      return;
    List<List<int>> objLinkTypes = this._GetObjLinkTypes((ILinkObjTypes) op);
    for (int index = 0; index < op.linkTypeIDs.Count; ++index)
    {
      int linkTypeId = op.linkTypeIDs[index];
      List<int> objTypeIDs = objLinkTypes[index];
      ExpertServer.PieceData pieceData = this._Piece4OneRelType(ti, linkTypeId, objTypeIDs, sourceObjs, (ILinkObjTypes) op);
      if (pieceData != null)
      {
        pieceData.cond = op.afterFilter;
        portion.Add(pieceData);
      }
    }
  }

  private List<ObjInfoItem> GetOIList(IUserSession ius, List<long> objIds)
  {
    List<ObjInfoItem> objectInfoList = ObjInfoHelper.GetObjectInfoList((IEnumerable<long>) objIds);
    ObjInfoHelper.UpdateUnknownTypes((IEnumerable<ObjInfoItem>) objectInfoList, ius);
    return objectInfoList;
  }

  private List<ObjInfoItem> GetOIList(HybridTableExp dt)
  {
    List<ObjInfoItem> oiList = new List<ObjInfoItem>();
    int indexByName = dt.Columns.GetIndexByName("cad0002e-306c-11d8-b4e9-00304f19f545");
    for (int index = 0; index < dt.RowsCount; ++index)
    {
      HybridRowExp hybridRowExp = dt[index];
      long int64 = Convert.ToInt64(hybridRowExp[0]);
      int int32 = Convert.ToInt32(hybridRowExp[indexByName]);
      oiList.Add(new ObjInfoItem(int64, int32));
    }
    return oiList;
  }

  private List<ColumnDescriptor> GetColDescrsForObjTypes(
    ExpertServer.ExpServTask ti,
    List<int> objTypeIDs,
    GlobalNode root)
  {
    List<ColumnDescriptor> descrsForObjTypes = new List<ColumnDescriptor>();
    SortedSet<string> sortedSet = new SortedSet<string>();
    if (root.descs != null)
    {
      foreach (ColumnDescriptor desc in root.descs)
      {
        descrsForObjTypes.Add(desc);
        sortedSet.Add(desc.AttributeID.ToString());
      }
    }
    List<int> intList = new List<int>();
    foreach (int objTypeId in objTypeIDs)
    {
      if (ti.objTypesToNodes.ContainsKey(objTypeId))
      {
        int objTypesToNode = ti.objTypesToNodes[objTypeId];
        if (!intList.Contains(objTypesToNode))
        {
          GlobalTypeNode globalTypeNode = (GlobalTypeNode) root.Items[objTypesToNode];
          if (globalTypeNode.descs != null)
          {
            foreach (ColumnDescriptor desc in globalTypeNode.descs)
            {
              string str = desc.AttributeID.ToString();
              if (!sortedSet.Contains(str))
              {
                descrsForObjTypes.Add(desc);
                sortedSet.Add(str);
              }
            }
          }
          intList.Add(objTypesToNode);
        }
      }
    }
    return descrsForObjTypes;
  }

  private List<List<int>> _GetObjLinkTypes(ILinkObjTypes ilot)
  {
    List<List<int>> objLinkTypes = new List<List<int>>();
    if (ilot.LinkTypeIDs != null)
    {
      for (int index = 0; index < ilot.LinkTypeIDs.Count; ++index)
        objLinkTypes.Add(new List<int>());
      if (ilot.LinkTypesForObjTypes != null)
      {
        for (int index = 0; index < ilot.LinkTypesForObjTypes.Count; ++index)
        {
          int linkTypesForObjType = ilot.LinkTypesForObjTypes[index];
          if (linkTypesForObjType >= 0 && linkTypesForObjType < objLinkTypes.Count)
            objLinkTypes[linkTypesForObjType].Add(ilot.ObjTypeIDs[index]);
        }
      }
    }
    return objLinkTypes;
  }

  private List<long> _PerformIsps(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    long objId,
    List<int> typeIdList,
    IspMode ispWork)
  {
    ISubstitutesService service = (ISubstitutesService) this._serviceProvider.GetService(typeof (ISubstitutesService));
    ti.app = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(ti), ti.verRuleOwnerId, objId, typeIdList[0], AVSSpecificationForm.A);
    for (int index = 1; index < typeIdList.Count; ++index)
    {
      if (typeIdList[index] <= 100000)
      {
        ArticlesPartsPackage andVariableParts = service.FindCommonAndVariableParts(ExpertServer.GetSessionGuid(ti), ti.verRuleOwnerId, objId, typeIdList[index], AVSSpecificationForm.A);
        ti.app.MergeWith(andVariableParts);
      }
    }
    long[] withoutFiltration = ((IArticleService) this._serviceProvider.GetService(typeof (IArticleService))).FindArticlesByGroupIDWithoutFiltration(objId, (object) ius.SessionGUID);
    ti.ispList = new List<long>((IEnumerable<long>) withoutFiltration);
    ti.ispNameList = new List<string>();
    foreach (long isp in ti.ispList)
    {
      List<long> articleVariablePart = ti.app.GetArticleVariablePart(isp);
      if (articleVariablePart != null && articleVariablePart.Count > 0)
        ti.HasVariableParts = true;
      IDBAttribute dbAttribute = (IDBAttribute) null;
      IDBObject dbObject = ius.GetObject(isp, false);
      if (dbObject != null)
        dbAttribute = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
      if (dbAttribute != null)
        ti.ispNameList.Add(Convert.ToString(dbAttribute.Value));
      else
        ti.ispNameList.Add("");
    }
    return ti.ispList;
  }

  private void PerformSubstitutes(ExpertServer.ExpServTask ti, IUserSession ius)
  {
    if (ti.savedData == null)
      return;
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    for (int index = 0; index < ti.savedData.Columns.Count; ++index)
    {
      string columnName = ti.savedData.Columns[index].ColumnName;
      int attrType = GuidHelper.IsGuid(columnName) ? MetaDataHelper.GetAttributeTypeID(new Guid(columnName)) : Convert.ToInt32(columnName);
      if (this.GetAttrDataType(attrType) == DataType.String)
      {
        intList1.Add(index);
        intList2.Add(attrType);
      }
    }
    if (intList1.Count == 0)
      return;
    for (int index1 = 0; index1 < ti.savedData.RowsCount; ++index1)
    {
      HybridRowExp hybridRowExp = ti.savedData[index1];
      int int32 = Convert.ToInt32(hybridRowExp["cad0002e-306c-11d8-b4e9-00304f19f545"]);
      for (int index2 = 0; index2 < intList1.Count; ++index2)
      {
        int index3 = intList1[index2];
        int attrType = intList2[index2];
        string str = Convert.ToString(hybridRowExp[index3]);
        if (str.Contains("["))
        {
          ExpertServer.Attr4_OTInfo at4OtInfo = this._GetAT4OTInfo(attrType, int32);
          LogManager.AddLine($"------ PerformSubstitutes for attribute {Convert.ToString(attrType)} a4ot.DescriptionEvent={Convert.ToString(at4OtInfo.DescriptionEvent)}", true);
          if (at4OtInfo.DescriptionEvent)
          {
            int startIndex1 = 0;
            bool flag = false;
            while (startIndex1 < str.Length)
            {
              int startIndex2 = str.IndexOf("[", startIndex1);
              int num = startIndex2 >= 0 ? str.IndexOf("]", startIndex2) : -1;
              startIndex1 = num >= 0 ? num + 1 : str.Length;
              LogManager.AddLine($"--  pos1={Convert.ToString(startIndex2)} pos2={Convert.ToString(num)}", true);
              if (startIndex2 >= 0 && num >= 0)
              {
                string key = str.Substring(startIndex2 + 1, num - startIndex2 - 1);
                LogManager.AddLine($"--  pos1={Convert.ToString(startIndex2)} pos2= {Convert.ToString(num)} aliasName= {key}", true);
                if (this.attrAliases.ContainsKey(key))
                {
                  ExpertServer.AttrInfo attrAlias = this.attrAliases[key];
                  int index4 = -1;
                  if (!attrAlias.guid.Equals(Guid.Empty))
                    index4 = ti.savedData.Columns.GetIndexByName(attrAlias.guidStr);
                  if (index4 < 0)
                    index4 = ti.savedData.Columns.GetIndexByName(attrAlias.attrIdStr);
                  if (index4 >= 0)
                  {
                    string newValue = Convert.ToString(hybridRowExp[index4]);
                    string oldValue = str.Substring(startIndex2, num - startIndex2 + 1);
                    str = str.Replace(oldValue, newValue);
                    startIndex1 = startIndex2;
                    flag = true;
                  }
                  LogManager.AddLine($"--  colIndex= {Convert.ToString(index4)} s= {str}", true);
                }
              }
            }
            if (flag)
              hybridRowExp[index3] = (object) str;
          }
          LogManager.AddLine("============================= " + Convert.ToString(attrType), true);
        }
      }
    }
  }

  public string MakeSubstitute(ExpertServer.ExpServTask ti, long objId, int attrId, string s)
  {
    string str = s;
    if (s.Contains("["))
    {
      HybridRowExp hybridRowExp = ti.savedDataByObjId(objId);
      if (hybridRowExp == null)
        return str;
      int int32 = Convert.ToInt32(hybridRowExp["cad0002e-306c-11d8-b4e9-00304f19f545"]);
      if (this._GetAT4OTInfo(attrId, int32).DescriptionEvent)
      {
        int startIndex1 = 0;
        while (startIndex1 < str.Length)
        {
          int startIndex2 = str.IndexOf("[", startIndex1);
          int num = startIndex2 >= 0 ? str.IndexOf("]", startIndex2) : -1;
          startIndex1 = num >= 0 ? num + 1 : str.Length;
          if (startIndex2 >= 0)
          {
            string key = str.Substring(startIndex2 + 1, num - startIndex2 - 1);
            if (this.attrAliases.ContainsKey(key))
            {
              ExpertServer.AttrInfo attrAlias = this.attrAliases[key];
              int index = -1;
              if (!attrAlias.guid.Equals(Guid.Empty))
                index = ti.savedData.Columns.GetIndexByName(attrAlias.guidStr);
              if (index < 0)
                index = ti.savedData.Columns.GetIndexByName(attrAlias.attrIdStr);
              if (index >= 0)
              {
                string newValue = Convert.ToString(hybridRowExp[index]);
                string oldValue = str.Substring(startIndex2, num - startIndex2 + 1);
                str = str.Replace(oldValue, newValue);
                startIndex1 = startIndex2;
              }
            }
          }
        }
      }
    }
    return str;
  }

  private void ReplaceQuantities(ExpertServer.ExpServTask ti)
  {
    if (ti.savedLinks == null)
      return;
    int indexByName = ti.savedLinks.Columns.GetIndexByName(ExpertAttrGUIDs.attrQuantity);
    if (indexByName < 0)
      return;
    for (int index = 0; index < ti.savedLinks.RowsCount; ++index)
    {
      HybridRowExp savedLink = ti.savedLinks[index];
      object obj = savedLink[indexByName];
      if (!obj.IsNullOrDBNull() && obj is string mValue)
        savedLink[indexByName] = (object) MeasureHelper.ConvertToMeasuredValue(mValue);
    }
    HybridColumnsExp.HybridColumnExp column = ti.savedLinks.Columns[indexByName] with
    {
      DataType = typeof (MeasuredValue)
    };
    ti.savedLinks.Columns[indexByName] = column;
  }

  internal bool IsAttrForSign(string guid)
  {
    return SystemGUIDs.IsSystemGUID(guid) && guid.CompareTo(ExpertAttrGUIDs.signSurnameParm) >= 0 && guid.CompareTo(ExpertAttrGUIDs.signStatus) <= 0;
  }

  private void ReplaceMemos(IUserSession ius, HybridTableExp table, bool relTable)
  {
    if (table == null)
      return;
    List<int> intList = new List<int>();
    for (int index = 0; index < table.Columns.Count; ++index)
    {
      if (table.Columns[index].fldType == FieldTypes.ftMemo)
        intList.Add(index);
    }
    if (intList.Count == 0)
      return;
    for (int index1 = 0; index1 < table.RowsCount; ++index1)
    {
      HybridRowExp hybridRowExp = table[index1];
      long int64 = Convert.ToInt64(hybridRowExp[0]);
      for (int index2 = 0; index2 < intList.Count; ++index2)
      {
        int index3 = intList[index2];
        MemoProxyReader memoProxyReader = new MemoProxyReader(int64, table.Columns[index3].attrTypeId, Convert.ToString(hybridRowExp[index3]), relTable);
        hybridRowExp[index3] = (object) memoProxyReader;
      }
    }
  }

  internal List<long> GetRequiredVersions(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    long objectId,
    ModParmVersion mpv)
  {
    DataTable allObjectVersions = ius.GetAllObjectVersions(objectId, false, true, false);
    List<long> requiredVersions = new List<long>();
    if (allObjectVersions == null)
      return requiredVersions;
    HybridTableExp hybridTableExp = new HybridTableExp(allObjectVersions);
    int num1 = 0;
    switch (mpv.sortMode)
    {
      case VerSort.VerId:
        num1 = hybridTableExp.Columns.GetIndexByName("F_OBJECT_ID");
        break;
      case VerSort.LCStepId:
        num1 = hybridTableExp.Columns.GetIndexByName("F_LC_STEP");
        break;
      case VerSort.LevelId:
        num1 = hybridTableExp.Columns.GetIndexByName("F_LEVEL_ID");
        break;
      case VerSort.CreationDate:
        num1 = hybridTableExp.Columns.GetIndexByName("F_OBJ_CREATE");
        break;
      case VerSort.ModifyDate:
        num1 = hybridTableExp.Columns.GetIndexByName("F_MODIFY_DATE");
        break;
      case VerSort.ModGroupId:
        num1 = hybridTableExp.Columns.GetIndexByName("F_MODIFICATION_ID");
        break;
    }
    int num2 = num1 + 1;
    if (mpv.descending)
      num2 = -num2;
    hybridTableExp.Sort(new List<int>() { num2 });
    int indexByName = hybridTableExp.Columns.GetIndexByName("F_OBJECT_ID");
    foreach (HybridRowExp row in hybridTableExp.Rows)
    {
      long int64 = Convert.ToInt64(row[indexByName]);
      if (mpv.cond == null || ti.CheckRowCond(int64, row, mpv.cond))
      {
        requiredVersions.Add(int64);
        if (!mpv.forAllVersions)
          break;
      }
    }
    return requiredVersions;
  }

  private void _Oper(
    int taskId,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    DocumentTreeNode curDocNode = task.curDocNode;
    try
    {
      switch (node.opTag)
      {
        case ExpertScriptOp.opSetting:
          this._OpSetting(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opDocFillText:
          this._OpFillText(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opDocNewElem:
          this._OpCreateElem(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opDocSelectElem:
          this._OpSelectElem(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opDocControl:
          this._OpDocControl(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opRecalc:
          this._OpRecalc(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opUserProc:
          this._OpUserProc(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opVersionRule:
          this._OpVersionRule(task, ius, node, context, dTable);
          break;
        case ExpertScriptOp.opSetInBase:
          this._OpSetInBase(task, ius, node, context, dTable);
          break;
      }
      long[] new_context = (long[]) null;
      for (int index = 0; index < node.Items.Count; ++index)
      {
        ScriptTreeNode node1 = (ScriptTreeNode) node.Items[index];
        this.ProcessScriptNode(taskId, node1, context, dTable, false, ref new_context);
      }
    }
    finally
    {
      if (node.opTag != ExpertScriptOp.opDocSelectElem)
      {
        if (task.lockCurNode != null)
          task.curDocNode = task.lockCurNode;
        else if (task.curDocNode != curDocNode)
          task.curDocNode = curDocNode;
      }
    }
  }

  private void _OpSetting(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    OpParmSetting op = (OpParmSetting) node.op;
    Guid guid1 = new Guid(op.attrGUID);
    Guid guid2 = Guid.Empty;
    if (op.objTypeGUID != "")
      guid2 = new Guid(op.objTypeGUID);
    if (context.Length < 1 && !guid2.Equals(Guid.Empty))
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_34"));
    object obj1 = (object) null;
    long num1 = -1;
    if (context.Length >= 1)
      num1 = context[0];
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(guid1);
    ExpertServer.TempAttrStru tempAttrStru = ti.GetTempAttrStru(guid1);
    CalcAttrPair calcAttrPair = !tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout) ? new CalcAttrPair(num1, 56797 /*0xDDDD*/, attributeTypeId) : new CalcAttrPair(-1L, attributeTypeId);
    HybridRowExp row = (HybridRowExp) null;
    if (dTable != null && dTable.RowsCount > 0)
      row = dTable[0];
    bool blockTrace = node.label.StartsWith("&&");
    if (op.tf != null && op.setKind != ExpertSettingKind.setKindNumber)
    {
      ExpertResult expertResult = ExpertResult.OK;
      try
      {
        if (blockTrace)
          expertResult = ti._CalcFormula(new long[1]{ num1 }, row, op.tf, out obj1, false);
        else
          expertResult = ti.CalcFormula(new long[1]{ num1 }, row, op.tf, out obj1, 0L);
      }
      catch (ExpertServerException ex)
      {
        if (ti.docScriptId == 0L)
          throw;
        if (op.setKind == ExpertSettingKind.setKindByTable)
          obj1 = (object) "";
      }
      if (op.setKind != ExpertSettingKind.setKindByTable && (expertResult != ExpertResult.OK || obj1.IsNullOrDBNull()))
      {
        obj1 = ExpertServer.Calculator.GetDefaultValue(op.tf.resType, attributeTypeId);
        XmlNode node1 = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_44"));
        if (node1 != null)
          ti.traceAddText(node1, $"{LocalizationHolder.rm.GetString("Expert.Server_35")}{op.tf.Text}{LocalizationHolder.rm.GetString("Expert.Server_36")}{Convert.ToString(num1)}]");
      }
    }
    string objTypeGUID = op.objTypeGUID;
    if (op.setMode == "F")
      objTypeGUID = "*";
    FieldTypes fieldTypes = FieldTypes.ftString;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(op.attrGUID));
    if (attributeType != null)
      fieldTypes = attributeType.FieldType;
    if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout) || ti.docAttrs.Contains(attributeTypeId))
      num1 = -1L;
    if (op.setMode == "R" && num1 != -1L)
    {
      long num2 = 0;
      if (dTable != null && dTable.RowsCount > 0)
      {
        object obj2 = dTable.Rows[0]["cad00033-306c-11d8-b4e9-00304f19f545"];
        if (obj2.NotNullOrDBNull())
          num2 = Convert.ToInt64(obj2);
      }
      if (num2 == 0L && ti.savedData != null)
      {
        HybridRowExp hybridRowExp = ti.savedDataByObjId(num1);
        if (hybridRowExp != null)
        {
          object obj3 = hybridRowExp["cad00033-306c-11d8-b4e9-00304f19f545"];
          if (obj3.NotNullOrDBNull())
            num2 = Convert.ToInt64(obj3);
        }
      }
      num1 = num2 != 0L ? num2 : throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_298"));
    }
    if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithObject) && context.Length > 1)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_184"));
    switch (op.setKind)
    {
      case ExpertSettingKind.setKindValue:
        this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, obj1, dTable, op, blockTrace: blockTrace);
        break;
      case ExpertSettingKind.setKindByTable:
        obj1 = (object) this.ConvertResByTable(obj1, op.listTable);
        this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, obj1, dTable, op);
        break;
      case ExpertSettingKind.setKindSum:
        switch (fieldTypes)
        {
          case FieldTypes.ftInteger:
            if (op.I_Val == 936532L)
              op.I_Val = 0L;
            long int64_1 = Convert.ToInt64(obj1);
            op.I_Val += int64_1;
            obj1 = (object) op.I_Val;
            this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) op.I_Val, dTable, op);
            break;
          case FieldTypes.ftDouble:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
              op.Val = 0.0;
            double num3 = Convert.ToDouble(obj1);
            op.Val += num3;
            obj1 = (object) op.Val;
            this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) op.Val, dTable, op);
            break;
          case FieldTypes.ftMeasured:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
            {
              op.Val = 0.0;
              op.measureID = 0L;
            }
            if (!(obj1 is MeasuredValue))
              obj1 = (object) new MeasuredValue(Convert.ToDouble(obj1), 0L);
            if (op.measureID == 0L)
            {
              op.Val = ((MeasuredValue) obj1).Value;
              op.measureID = ((MeasuredValue) obj1).MeasureID;
              break;
            }
            MeasuredValue measuredValue1 = ExpertServer.MeasureSum(new MeasuredValue(op.Val, op.measureID), (MeasuredValue) obj1);
            op.Val = measuredValue1.Value;
            op.measureID = measuredValue1.MeasureID;
            obj1 = (object) measuredValue1;
            this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) measuredValue1, dTable, op);
            break;
        }
        break;
      case ExpertSettingKind.setKindAverage:
        switch (fieldTypes)
        {
          case FieldTypes.ftInteger:
            if (op.I_Val == 936532L)
              op.I_Val = 0L;
            long int64_2 = Convert.ToInt64(obj1);
            op.I_Val += int64_2;
            ++op.Count;
            obj1 = (object) (op.I_Val / (long) op.Count);
            this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, obj1, dTable, op);
            break;
          case FieldTypes.ftDouble:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
              op.Val = 0.0;
            double num4 = Convert.ToDouble(obj1);
            op.Val += num4;
            ++op.Count;
            obj1 = (object) (op.Val / (double) op.Count);
            this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, obj1, dTable, op);
            break;
          case FieldTypes.ftMeasured:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
            {
              op.Val = 0.0;
              op.measureID = 0L;
            }
            ++op.Count;
            if (!(obj1 is MeasuredValue))
              obj1 = (object) new MeasuredValue(Convert.ToDouble(obj1), 0L);
            if (op.measureID == 0L)
            {
              op.Val = ((MeasuredValue) obj1).Value;
              op.measureID = ((MeasuredValue) obj1).MeasureID;
              break;
            }
            MeasuredValue measuredValue2 = ExpertServer.MeasureSum(new MeasuredValue(op.Val, op.measureID), (MeasuredValue) obj1);
            op.Val = measuredValue2.Value;
            op.measureID = measuredValue2.MeasureID;
            obj1 = (object) new MeasuredValue(op.Val / (double) op.Count, op.measureID);
            this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, obj1, dTable, op);
            break;
        }
        break;
      case ExpertSettingKind.setKindNumber:
        ++op.Count;
        obj1 = (object) op.Count;
        this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) op.Count, dTable, op);
        break;
      case ExpertSettingKind.setKindMinimum:
        switch (fieldTypes)
        {
          case FieldTypes.ftInteger:
            if (op.I_Val == 936532L)
              op.Val = (double) long.MaxValue;
            long int64_3 = Convert.ToInt64(obj1);
            if (int64_3 < op.I_Val)
            {
              op.I_Val = int64_3;
              obj1 = (object) int64_3;
              this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) int64_3, dTable, op);
              break;
            }
            break;
          case FieldTypes.ftDouble:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
              op.Val = double.MaxValue;
            double num5 = Convert.ToDouble(obj1);
            if (num5 < op.Val)
            {
              op.Val = num5;
              obj1 = (object) num5;
              this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) num5, dTable, op);
              break;
            }
            break;
          case FieldTypes.ftMeasured:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
            {
              op.Val = double.MaxValue;
              op.measureID = 0L;
            }
            if (!(obj1 is MeasuredValue))
              obj1 = (object) new MeasuredValue(Convert.ToDouble(obj1), 0L);
            if (op.measureID == 0L)
            {
              op.Val = ((MeasuredValue) obj1).Value;
              op.measureID = ((MeasuredValue) obj1).MeasureID;
              break;
            }
            switch (MeasureHelper.Compare((MeasuredValue) obj1, new MeasuredValue(op.Val, op.measureID)))
            {
              case CompareResult.Less:
                op.Val = ((MeasuredValue) obj1).Value;
                op.measureID = ((MeasuredValue) obj1).MeasureID;
                this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, obj1, dTable, op);
                break;
              case CompareResult.NotCompatible:
                throw new ExpertServerException("");
            }
            break;
        }
        break;
      case ExpertSettingKind.setKindMaximum:
        switch (fieldTypes)
        {
          case FieldTypes.ftInteger:
            if (op.I_Val == 936532L)
              op.Val = (double) long.MinValue;
            long int64_4 = Convert.ToInt64(obj1);
            if (int64_4 > op.I_Val)
            {
              op.I_Val = int64_4;
              obj1 = (object) int64_4;
              this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) int64_4, dTable, op);
              break;
            }
            break;
          case FieldTypes.ftDouble:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
              op.Val = double.MinValue;
            double num6 = Convert.ToDouble(obj1);
            if (num6 > op.Val)
            {
              op.Val = num6;
              obj1 = (object) num6;
              this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) num6, dTable, op);
              break;
            }
            break;
          case FieldTypes.ftMeasured:
            if (Math.Abs(op.Val - 1.2345) < 1E-05)
            {
              op.Val = double.MinValue;
              op.measureID = 0L;
            }
            if (!(obj1 is MeasuredValue))
              obj1 = (object) new MeasuredValue(Convert.ToDouble(obj1), 0L);
            if (op.measureID == 0L)
            {
              op.Val = ((MeasuredValue) obj1).Value;
              op.measureID = ((MeasuredValue) obj1).MeasureID;
              break;
            }
            switch (MeasureHelper.Compare((MeasuredValue) obj1, new MeasuredValue(op.Val, op.measureID)))
            {
              case CompareResult.More:
                op.Val = ((MeasuredValue) obj1).Value;
                op.measureID = ((MeasuredValue) obj1).MeasureID;
                this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, obj1, dTable, op);
                break;
              case CompareResult.NotCompatible:
                throw new ExpertServerException("");
            }
            break;
        }
        break;
      case ExpertSettingKind.setKindList:
        string str1 = Convert.ToString(obj1);
        object parmValue = this._GetParmValue(ti, calcAttrPair);
        string str2 = parmValue == null ? "" : Convert.ToString(parmValue);
        string str3 = !(str2.Trim() != "") ? str1 : str2 + op.listDivider + str1;
        obj1 = (object) str3;
        this.InnerSetParm(ti, calcAttrPair, (object) str3);
        this.SetAttrValue(ti, ius, num1, op.attrGUID, objTypeGUID, row, (object) str3, dTable, op);
        break;
    }
    if (op.storeInGlobal && ti.savedData != null && num1 != -1L && attributeTypeId != 0)
    {
      HybridRowExp hybridRowExp1;
      HybridTableExp hybridTableExp;
      if (op.setMode == "R")
      {
        hybridRowExp1 = ti.savedLinksByIdIndex(num1);
        hybridTableExp = ti.savedLinks;
      }
      else
      {
        hybridRowExp1 = ti.savedDataByObjId(num1);
        hybridTableExp = ti.savedData;
      }
      if (hybridRowExp1 != null)
      {
        int index1 = hybridTableExp.Columns.GetIndexByName(op.attrGUID);
        if (index1 == -1)
        {
          Type dataType = DataTypeConvertor.FieldType2DataType(fieldTypes, attributeTypeId);
          hybridTableExp.AddColumn(op.attrGUID, dataType);
          index1 = hybridTableExp.Columns.Count - 1;
          object obj4 = DataTypeConvertor.DefForAttrType(fieldTypes);
          for (int index2 = 0; index2 < hybridTableExp.RowsCount - 1; ++index2)
          {
            HybridRowExp hybridRowExp2 = hybridTableExp[index2];
            if (hybridRowExp2 != hybridRowExp1)
              hybridRowExp2[index1] = obj4;
          }
        }
        hybridRowExp1[index1] = obj1;
      }
    }
    if (blockTrace || !ti.makeTrace || !this.FlagIn(ExpertTraceFlags.ShowSettings, ti.traceFlags))
      return;
    this.StartModifyTrace(ti.taskId);
    try
    {
      XmlNode node2 = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_227"));
      if (node2 == null)
        return;
      ti.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_22"), attributeType != null ? attributeType.Name : LocalizationHolder.rm.GetString("Expert.Server_228"));
      ti.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_99"), Convert.ToString(obj1));
    }
    finally
    {
      this.EndModifyTrace(ti);
    }
  }

  private void _OpSetInBase(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    this._OpSetting(ti, ius, node, context, dTable);
  }

  private string ConvertResByTable(object Result, List<Triple> listTable)
  {
    string strA = Convert.ToString(Result);
    foreach (Triple triple in listTable)
    {
      if ((!(triple.From != "") || string.Compare(strA, triple.From) >= 0) && (!(triple.To != "") || string.Compare(strA, triple.To) < 0))
        return triple.Result;
    }
    return strA;
  }

  private void _OpFillText(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    OpParmFillFld op = (OpParmFillFld) node.op;
    bool flag1 = false;
    Guid guid = Guid.Empty;
    int num1 = -1;
    string fldId = op.FldID;
    long num2 = -1;
    HybridRowExp row = (HybridRowExp) null;
    ArcMethods arcMeth = ArcMethods.NotPacked;
    string str = "";
    if (op.FldID == "" || op.attrGUID == "" && op.tf == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_242"));
    if (context != null && context.Length != 0)
    {
      num2 = context[0];
      TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(num2, ius);
      if ((TypedInfoItem) objData != (TypedInfoItem) null)
        guid = objData.ObjGuid;
    }
    if (dTable != null && dTable.RowsCount > 0)
      row = dTable[0];
    if (row == null && ti.savedData != null && num2 != -1L)
      row = ti.savedDataByObjId(num2);
    if (op.AddAttrGUID != "")
    {
      int num3 = 0;
      try
      {
        num3 = Convert.ToInt32(fldId);
      }
      catch
      {
      }
      if (num3 != 0)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(op.AddAttrGUID));
        ExpertServer.TempAttrStru tempAttrStru = ti.GetTempAttrStru(attributeType.AttributeGuid);
        object obj1 = (object) null;
        if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
        {
          obj1 = this._GetParmValue(ti, -1L, -1, attributeType.AttributeID);
        }
        else
        {
          if (num2 != 0L)
          {
            object obj2 = (object) null;
            ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(num2, -7008);
            if (!ti.attrCache.TryGetValue(key, out obj2))
            {
              TaskDataCache.RelDataItem relData = ti.DataCache.GetRelData(num2, ius);
              if ((TypedInfoItem) relData != (TypedInfoItem) null)
                guid = relData.RelGuid;
              ti.attrCache.Add(key, (object) guid);
            }
            else
              guid = (Guid) obj2;
          }
          if (context != null && context.Length != 0)
          {
            if (dTable != null)
            {
              for (int index = 0; index < dTable.Columns.Count; ++index)
              {
                if (dTable.Columns[index].ColumnName.Equals(op.AddAttrGUID))
                {
                  obj1 = dTable[0][index];
                  break;
                }
              }
            }
            if (obj1 == null)
            {
              num1 = MetaDataHelper.GetAttributeTypeID(new Guid(op.AddAttrGUID));
              object obj3 = (object) null;
              if (ti.GetAttributeValue(num2, num1, out obj3, ExpertServer.ExpServTask.AttrOptions.AsString))
                obj1 = obj3;
            }
          }
        }
        if (obj1 != null)
        {
          try
          {
            fldId = Convert.ToString(num3 + Convert.ToInt32(obj1));
          }
          catch
          {
            fldId += Convert.ToString(obj1);
          }
        }
      }
    }
    RectangleElement ownerNode = (RectangleElement) null;
    ExpertTraceFlags traceFlags;
    lock (ti)
    {
      traceFlags = ti.traceFlags;
      try
      {
        if (ti.curDocNode != null)
          ownerNode = (RectangleElement) ti.curDocNode.FindFirstNodeFromTemplate_Recursive(fldId);
        if (ownerNode == null && ti.defRootNode != null)
          ownerNode = (RectangleElement) ti.defRootNode.FindFirstNodeFromTemplate_Recursive(fldId);
        if (ownerNode == null)
          ownerNode = (RectangleElement) ti.docData.FindFirstNodeFromTemplate_Recursive(fldId);
      }
      catch
      {
      }
    }
    bool flag2 = this.FlagIn(ExpertTraceFlags.ShowFillDocs, traceFlags);
    DataType dataType = DataType.String;
    object obj4;
    if (op.tf != null)
    {
      try
      {
        obj4 = ti.CalcFormula(num2, row, op.tf);
      }
      catch
      {
        obj4 = (object) null;
      }
      dataType = op.tf.resType;
    }
    else
    {
      bool useCache = !node.label.StartsWith("!");
      obj4 = this.GetAttrValue(ti.taskId, ius, num2, op.attrGUID, op.objTypeGUID, row, out arcMeth, useCache);
      FieldTypes attrType = FieldTypes.ftString;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(op.attrGUID));
      if (attributeType != null)
        attrType = attributeType.FieldType;
      DataType dt;
      switch (attrType)
      {
        case FieldTypes.ftShortBlob:
        case FieldTypes.ftBlob:
          obj4 = (object) new MemoryStream();
          BlobProcReader blobProcReader1 = new BlobProcReader(num2, AttributableElements.Object, attributeType.AttributeID, 0, 0, (Stream) obj4, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcReader1.ReadData(ius);
          ((Stream) obj4).Position = 0L;
          str = blobProcReader1.BlobInformation.FileName;
          goto label_74;
        case FieldTypes.ftFile:
          obj4 = (object) new MemoryStream();
          IDBAttribute attributeByGuid = ius.GetObject(num2).GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
          int aIndex = 0;
          if (attributeByGuid != null && op.AuthFile)
          {
            for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
            {
              attributeByGuid.Index = index;
              if (attributeByGuid is IBlobReader blobReader && blobReader.OpenBlob(-1).FileType == FileTypes.ftAuthentical)
              {
                aIndex = index;
                break;
              }
            }
          }
          BlobProcReader blobProcReader2 = new BlobProcReader(num2, AttributableElements.Object, attributeType.AttributeID, aIndex, 0, (Stream) obj4, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcReader2.ReadData(ius);
          ((Stream) obj4).Position = 0L;
          str = blobProcReader2.BlobInformation.FileName;
          goto label_74;
        case FieldTypes.ftSystem:
          if (attributeType != null)
          {
            dt = DataTypeConvertor.AttrType2DataType(ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeType.AttributeID));
            break;
          }
          goto default;
        default:
          dt = DataTypeConvertor.AttrType2DataType(attrType);
          break;
      }
      if (obj4.IsDBNull())
        obj4 = (object) null;
      else if (dt == DataType.Measured && obj4 != null)
        obj4 = (object) MeasureHelper.ConvertToMeasuredValue(Convert.ToString(obj4), ExpertConsts.Consts.mdShtuk, false);
      if (obj4 != null && ownerNode != null && (dt == DataType.Date || dt == DataType.Float || dt == DataType.Measured))
        obj4 = (object) ExpertServer.MakeString(obj4, dt, ti, (ownerNode as TextData).TextFormat);
      if (obj4.IsNullOrDBNull())
        obj4 = (object) "";
    }
label_74:
    if (ownerNode == null)
    {
      XmlNode xmlNode = ti.traceAddElement("Exception");
      if (xmlNode == null)
        return;
      xmlNode.InnerText = string.Format(LocalizationHolder.rm.GetString("Expert.Server_284"), (object) node.label, (object) fldId);
    }
    else
    {
      string format = "";
      if (ownerNode is TextData && (ownerNode as TextData).TextFormat != null)
        format = (ownerNode as TextData).TextFormat;
      string s = "";
      if (obj4 != null)
      {
        if (obj4.GetType() == typeof (MeasuredValue))
        {
          try
          {
            MeasuredValue measuredValue = obj4 as MeasuredValue;
            double num4 = measuredValue.Value;
            if (format == "")
              format = "{0:F9}";
            s = $"{ExpertServer.FixDoubleStr(string.Format(format, (object) num4))} {MeasureHelper.FindDescriptor(measuredValue.MeasureID).ShortName}";
          }
          catch
          {
            if (format == "")
              format = "{0}";
            s = string.Format(format, obj4);
          }
          ExpertServer.ReplaceSeparator(ref s, ti.nfi);
        }
        else if (obj4.GetType() == typeof (double))
        {
          if (format == "")
            format = "{0:F9}";
          s = ExpertServer.FixDoubleStr(string.Format(format, (object) (double) obj4));
          ExpertServer.ReplaceSeparator(ref s, ti.nfi);
        }
        else
        {
          if (format == "")
            format = "{0}";
          s = string.Format(format, obj4);
        }
      }
      if (ti.makeTrace & flag2)
      {
        lock (ti)
        {
          XmlNode node1 = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_234"));
          if (node1 != null)
          {
            ti.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_235"), fldId);
            ti.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_93"), obj4 != null ? s : LocalizationHolder.rm.GetString("Expert.Server_185"));
            ti.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_92"), "yes");
          }
        }
      }
      if (num2 != -1L)
      {
        if (ti.savedData != null && ti.savedLinks != null && row != null && !ti.savedData.Columns.Contains(op.attrGUID) && ti.savedLinks.Columns.Contains(op.attrGUID))
        {
          num2 = 0L;
          HybridColumnsExp columns = row.Columns;
          for (int index = 0; index < columns.Count; ++index)
          {
            if (columns[index].ColumnName == "cad00033-306c-11d8-b4e9-00304f19f545")
            {
              if (row[index].NotNullOrDBNull())
              {
                num2 = Convert.ToInt64(row[index]);
                break;
              }
              break;
            }
          }
          flag1 = true;
        }
        if (num2 != 0L)
        {
          QuickObjectInfo objectInfo = ius.GetObjectInfo(num2);
          if (!objectInfo.Empty)
            guid = objectInfo.VersionGuid;
          switch (ownerNode)
          {
            case TextData _:
              TextData textData = ownerNode as TextData;
              if ((ownerNode.Template as TextData).ReferenceToTextSource is ReferenceToSignBase)
              {
                ReferenceToSignBase referenceToTextSource = (ownerNode.Template as TextData).ReferenceToTextSource as ReferenceToSignBase;
                textData.AssignReferenceToTextSource((ReferenceBase) new ReferenceToSignBase((DocumentTreeNode) ownerNode, RefToDBObjectType.rtUseSignFromObject, (DBObjectInfoBase) new DBObjectInfo(guid, num2), referenceToTextSource.AttributeName)
                {
                  SignField = referenceToTextSource.SignField
                }, true, false, false);
                break;
              }
              RefToDBObjectType refType = RefToDBObjectType.rtSelectedObject;
              if (flag1)
                refType = RefToDBObjectType.rtSelectedRelation;
              else if (op.LinkThisDoc)
                refType = RefToDBObjectType.rtUseParentDocumentObjectLink;
              DBObjectInfoBase dbObjectInfo = (DBObjectInfoBase) null;
              if (flag1)
              {
                IDBRelation relation = ius.GetRelation(num2, false);
                if (relation != null)
                  dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo(relation.GUID, num2, guid, relation.ProjID);
              }
              else
                dbObjectInfo = (DBObjectInfoBase) new DBObjectInfo(guid, num2);
              ReferenceBase referenceBase = !flag1 ? (GuidHelper.IsGuid(op.attrGUID) ? (ReferenceBase) new ReferenceToDBObjectAttributeBase((DocumentTreeNode) ownerNode, refType, dbObjectInfo, new Guid(op.attrGUID), num1, (string) null, !op.ActiveLink) : (ReferenceBase) new ReferenceToDBObjectBase((DocumentTreeNode) ownerNode, refType, dbObjectInfo, !op.ActiveLink)) : (GuidHelper.IsGuid(op.attrGUID) ? (ReferenceBase) new ReferenceToDBObjectAttributeBase((DocumentTreeNode) ownerNode, RefToDBObjectType.rtSelectedRelation, (DBObjectInfoBase) new DBRelationInfo(dbObjectInfo.RelationGuid, Guid.Empty), new Guid(op.attrGUID), num1, (string) null, !op.ActiveLink) : (ReferenceBase) new ReferenceToDBObjectBase((DocumentTreeNode) ownerNode, RefToDBObjectType.rtSelectedRelation, dbObjectInfo, !op.ActiveLink));
              textData.AssignReferenceToTextSource(referenceBase, true, false, false);
              break;
            case ContainerData _:
              (ownerNode as ContainerData).AssignReference(new ReferenceToGraphicsBase(guid, !op.ActiveLink), false, false, false);
              break;
            default:
              throw new ExpertServerException($"Поле {fldId} имеет тип {ownerNode.GetType().ToString()}, которому невозможно присвоить значение!");
          }
        }
      }
      if (obj4 == null)
        return;
      if (ownerNode is TextData)
      {
        if (op._leftInd != null)
        {
          try
          {
            float num5 = (float) ((double) Convert.ToInt64(ti.CalcFormula(num2, row, op._leftInd)) * 1.0 / 10.0);
            ParagraphFormat paragraphFormat = (ownerNode as TextData).ParagraphFormat.Clone();
            paragraphFormat.IdentLeft = new float?(num5);
            (ownerNode as TextData).SetParagraphFormat(paragraphFormat, false, false);
          }
          catch
          {
          }
        }
        if (op.FontName != "" || op.FontSize != 0L || op.Bold || op.Italic || op.Underline || op.Color != 0)
        {
          CharFormat charFormat = (ownerNode as TextData).CharFormat.Clone();
          if (op.FontName != "")
            charFormat.FontFamily = op.FontName;
          if (op.FontSize != 0L)
            charFormat.FontSize = new float?((float) op.FontSize);
          if (op.Bold && op.Italic)
            charFormat.BoldItalic = new BoldItalicStyle?(BoldItalicStyle.BoldItalic);
          else if (op.Bold)
            charFormat.BoldItalic = new BoldItalicStyle?(BoldItalicStyle.Bold);
          else if (op.Italic)
            charFormat.BoldItalic = new BoldItalicStyle?(BoldItalicStyle.Italic);
          if (op.Underline)
            charFormat.Underline = new UnderlineStyle?(UnderlineStyle.Underline);
          if (op.Color != 0)
            charFormat.TextColorForUser = new Color?(Color.FromArgb(op.Color));
          (ownerNode as TextData).SetCharFormat(charFormat, false, false);
        }
      }
      if (num2 != -1L)
        ownerNode.SetAttributeValue(ExpertAttrGUIDs.attrDocFldObject, num2.ToString());
      if (op.Tag != "")
        ownerNode.SetAttributeValue(ExpertAttrGUIDs.attrFillingTag, op.Tag);
      if (ownerNode is TextData)
        (ownerNode as TextData).Text = s;
      if (ownerNode is ContainerData)
      {
        ContainerData containerData = ownerNode as ContainerData;
        try
        {
          if (obj4 is byte[])
            obj4 = (object) new MemoryStream((byte[]) obj4);
          string fileName = str;
          containerData.AssignFileDataStream((Stream) obj4, fileName, ArcMethods.NotPacked, DataSourceType.Unknown, false, false, true);
          if (row != null)
          {
            HybridColumnsExp columns = row.Columns;
            for (int index = 0; index < columns.Count; ++index)
            {
              if (columns[index].ColumnName == ExpertAttrGUIDs.attrLayers)
              {
                object obj5 = row[index];
                if (obj5.NotNullOrDBNull())
                {
                  List<string> stringList = new List<string>((IEnumerable<string>) Convert.ToString(obj5).Split(','));
                  containerData.AssignLayers(stringList, false, false, true);
                  break;
                }
                break;
              }
            }
          }
        }
        catch
        {
        }
        this.InnerSetParm(ti, new CalcAttrPair(-1L, ExpertConsts.Consts.attrEmptyDoc), (object) false);
      }
      if (num2 == -1L)
        return;
      ownerNode.SetAttributeValue(ExpertConsts.Consts.attrDocFldObject.ToString(), num2.ToString());
    }
  }

  public static void ReplaceSeparator(ref string s, NumberFormatInfo nfi)
  {
    if (s.Contains("."))
      s = s.Replace(".", nfi.NumberDecimalSeparator);
    if (!s.Contains(","))
      return;
    s = s.Replace(",", nfi.NumberDecimalSeparator);
  }

  public static string FixDoubleStr(string str)
  {
    string str1 = "0.,";
    int length = str.Length;
    while (length >= 1 && str1.Contains(str[length - 1].ToString()))
    {
      --length;
      if (str[length] != '0')
        break;
    }
    if (length < str.Length)
      str = str.Substring(0, length);
    return str;
  }

  public static string MakeString(
    object Value,
    DataType dt,
    ExpertServer.ExpServTask ti,
    string format = null)
  {
    string s1;
    switch (dt)
    {
      case DataType.Float:
        s1 = ExpertServer.FixDoubleStr(format == null || format == "" ? $"{Value:F9}" : string.Format(format, Value));
        ExpertServer.ReplaceSeparator(ref s1, ti.nfi);
        break;
      case DataType.Measured:
        string caption = ((MeasuredValue) Value).Caption;
        if (caption == "")
          return caption;
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(caption);
        string s2 = ExpertServer.FixDoubleStr(string.Format(format == null || !(format != "") ? "{0:F9}" : format, (object) measuredValue.Value));
        ExpertServer.ReplaceSeparator(ref s2, ti.nfi);
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
        s1 = $"{s2} {descriptor.ShortName}";
        break;
      case DataType.Date:
        s1 = format == null || format == "" ? Convert.ToDateTime(Value).ToString("d", (IFormatProvider) ti.dfi) : string.Format(format, Value);
        break;
      default:
        s1 = Value.ToString();
        break;
    }
    return s1;
  }

  private void _OpSelectElem(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    OpParmSelFld op = (OpParmSelFld) node.op;
    long objId = -1;
    if (context != null && context.Length != 0)
      objId = context[0];
    HybridRowExp row = (HybridRowExp) null;
    if (dTable != null && dTable.RowsCount > 0)
      row = dTable[0];
    string str = op.tf == null ? op.FldId : (string) ti.CalcFormula(objId, row, op.tf);
    if (!(str != "") && !op.selWholeDoc)
      return;
    lock (ti)
    {
      if (op.selWholeDoc)
      {
        ti.curDocNode = (DocumentTreeNode) null;
      }
      else
      {
        DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) ti.docData;
        if (ti.defRootNode != null)
          documentTreeNode1 = ti.defRootNode;
        else if (ti.curDocNode != null)
          documentTreeNode1 = ti.curDocNode;
        DocumentTreeNode documentTreeNode2;
        for (documentTreeNode2 = (DocumentTreeNode) null; documentTreeNode2 == null && documentTreeNode1 != null; documentTreeNode1 = documentTreeNode1.Parent)
        {
          if (!op.selAncestor)
            documentTreeNode2 = op.tf == null || ti.curDocNode == null ? documentTreeNode1.FindFirstNodeFromTemplate_Recursive(str) : ti.curDocNode.FindNode(str);
          else if (documentTreeNode1.Id == str)
            documentTreeNode2 = documentTreeNode1;
        }
        ti.curDocNode = documentTreeNode2;
        if (op.byDefault)
          ti.defRootNode = documentTreeNode2;
      }
      ti.lockCurNode = (DocumentTreeNode) null;
    }
  }

  private void _OpCreateElem(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    OpParmCreateFld op = (OpParmCreateFld) node.op;
    string fldId = op.FldID;
    long num1 = -1;
    bool flag = false;
    Guid guid = Guid.Empty;
    if (context != null && context.Length != 0)
      num1 = context[0];
    if (num1 != -1L)
    {
      ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(num1, -7008);
      object obj = (object) null;
      if (!ti.attrCache.TryGetValue(key, out obj))
      {
        TypedInfoItem itemData = ti.DataCache.GetItemData(num1, ius);
        switch (itemData)
        {
          case TaskDataCache.ObjDataItem _:
            guid = ((TaskDataCache.ObjDataItem) itemData).ObjGuid;
            break;
          case TaskDataCache.RelDataItem _:
            guid = ((TaskDataCache.RelDataItem) itemData).RelGuid;
            break;
        }
        ti.attrCache.Add(key, (object) guid);
      }
      else
        guid = (Guid) obj;
    }
    if (op.AddAttrGUID != "")
    {
      int num2 = 0;
      try
      {
        num2 = Convert.ToInt32(fldId);
      }
      catch
      {
      }
      if (num2 != 0)
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(op.AddAttrGUID);
        object obj1 = this._GetParmValue(ti, -1L, -1, attributeTypeId);
        if (obj1 == null && context != null && context.Length != 0)
        {
          for (int index = 0; index < dTable.Columns.Count; ++index)
          {
            if (dTable.Columns[index].ColumnName.Equals(op.AddAttrGUID))
            {
              obj1 = dTable[0][index];
              break;
            }
          }
          if (obj1 == null)
          {
            ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(num1, attributeTypeId);
            object obj2 = (object) null;
            if (ti.attrCache.TryGetValue(key, out obj2))
              obj1 = obj2;
            else if (ti.GetAttributeValue(num1, op.AddAttrGUID, out obj1))
              ti.attrCache.Add(key, obj1);
          }
        }
        if (obj1 != null)
        {
          try
          {
            fldId = Convert.ToString(num2 + Convert.ToInt32(obj1));
          }
          catch
          {
            fldId += Convert.ToString(obj1);
          }
        }
      }
    }
    DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) null;
    lock (ti)
    {
      int traceFlags = (int) ti.traceFlags;
      try
      {
        documentTreeNode1 = ti.template.FindNode(fldId);
      }
      catch
      {
      }
    }
    if (documentTreeNode1 == null)
      throw new EAbort(ExpertResult.ObjectNotFound, LocalizationHolder.rm.GetString("Expert.Server_187") + fldId + LocalizationHolder.rm.GetString("Expert.Server_188"));
    DocumentTreeNode documentTreeNode2 = (DocumentTreeNode) null;
    if (op.avoidDup && ti.curDocNode != null && ti.curDocNode.NodesCount > 0)
    {
      documentTreeNode2 = ti.curDocNode.Nodes[ti.curDocNode.NodesCount - 1];
      if (documentTreeNode2.TemplateId != fldId)
        documentTreeNode2 = (DocumentTreeNode) null;
    }
    if (documentTreeNode2 == null)
    {
      documentTreeNode2 = documentTreeNode1.CloneFromTemplate();
      lock (ti)
      {
        if (ti.curDocNode != null && documentTreeNode1.IsChildForNode(ti.curDocNode.Template, true) && ti.curDocNode.CanAddChildElement(documentTreeNode2))
          ti.curDocNode.InsertChildNode(ti.curDocNode.NodesCount, documentTreeNode2, false, true, false, false, true);
        else if (ti.defRootNode != null && documentTreeNode1.IsChildForNode(ti.defRootNode.Template, true) && ti.defRootNode.CanAddChildElement(documentTreeNode2))
          ti.defRootNode.InsertChildNode(ti.defRootNode.NodesCount, documentTreeNode2, false, true, false, false, true);
        else if (ti.docData.CanAddChildElement(documentTreeNode2))
        {
          ti.docData.InsertChildNode(ti.docData.NodesCount, documentTreeNode2, false, true, false, false, true);
        }
        else
        {
          DocumentTreeNode templateRecursive = ti.docData.FindFirstNodeFromTemplate_Recursive(documentTreeNode1.Parent.Id);
          if (templateRecursive != null)
          {
            if (templateRecursive.CanAddChildElement(documentTreeNode2))
              templateRecursive.InsertChildNode(templateRecursive.NodesCount, documentTreeNode2, false, true, false, false, true);
          }
        }
      }
    }
    if (documentTreeNode2 == null)
      return;
    lock (ti)
    {
      if (op.makeNewCurrent)
      {
        ti.curDocNode = documentTreeNode2;
        if (op.curForever)
          ti.lockCurNode = documentTreeNode2;
      }
    }
    if (op.SaveIDAttrGUID != "")
    {
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(op.SaveIDAttrGUID));
      this.InnerSetParm(ti, attributeTypeId, (object) documentTreeNode2.Id);
    }
    if (num1 != -1L)
    {
      DBObjectInfoBase dbObjectInfo = (DBObjectInfoBase) null;
      RefToDBObjectType refType = RefToDBObjectType.rtSelectedObject;
      if (flag)
      {
        IDBRelation relation = ius.GetRelation(num1, false);
        if (relation != null)
          dbObjectInfo = (DBObjectInfoBase) new DBRelationInfo(relation.GUID, num1, guid, relation.ProjID);
        refType = RefToDBObjectType.rtSelectedRelation;
      }
      else
        dbObjectInfo = (DBObjectInfoBase) new DBObjectInfo(guid, num1);
      documentTreeNode2.SetAttributeValue(ExpertConsts.Consts.attrDocFldObject.ToString(), num1.ToString());
      if (documentTreeNode2 is TableData)
        ((TableData) documentTreeNode2).Reference = (ReferenceBase) new ReferenceToDBObjectBase(documentTreeNode2, refType, dbObjectInfo);
      if (documentTreeNode2 is TextData)
        ((TextData) documentTreeNode2).ReferenceToTextSource = (ReferenceBase) new ReferenceToDBObjectBase(documentTreeNode2, refType, dbObjectInfo);
    }
    if (op.fillChildren)
    {
      HybridRowExp row = (HybridRowExp) null;
      if (dTable != null && dTable.RowsCount > 0)
        row = dTable[0];
      HybridColumnsExp columns = (HybridColumnsExp) null;
      if (dTable != null)
        columns = dTable.Columns;
      IDBAttributable dbAttributable = (IDBAttributable) null;
      if (num1 != -1L)
        dbAttributable = ExpertServer.GetAttributable(ius, num1);
      AttributeValues[] attributesValues = dbAttributable.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeDescriptions);
      this.FillChildrenFields(ius, ti, documentTreeNode2, attributesValues, row, columns);
    }
    if (op.byDefault)
      ti.defRootNode = documentTreeNode2;
    if (ti.makeTrace && ti.curNode != null)
      ti.traceAddAttribute(ti.curNode, LocalizationHolder.rm.GetString("Expert.Server_236"), documentTreeNode2.Id);
    if (op.Tag != "")
      documentTreeNode1.SetAttributeValue(ExpertAttrGUIDs.attrCreationTag, op.Tag);
    if (op.avoidDup)
      return;
    this.InnerSetParm(ti, new CalcAttrPair(-1L, ExpertConsts.Consts.attrEmptyDoc), (object) false);
  }

  private void _OpVersionRule(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    OpParmVersionRule op = (OpParmVersionRule) node.op;
    IVersionRulesCacheService service = (IVersionRulesCacheService) this._serviceProvider.GetService(typeof (IVersionRulesCacheService));
    if (service == null)
      return;
    string str = ti.RulesList.ContainsKey(op.ruleId) ? ti.RulesList[op.ruleId].ownerId : "";
    FiltrationSettings filtrationSettings;
    if (str != "")
    {
      filtrationSettings = service.GetFiltrationSettings((object) ius, str);
    }
    else
    {
      str = op.ruleGuid;
      filtrationSettings = this.CreateFiltSettings(ti, str, op.ruleId);
      filtrationSettings.CurrentRule = service[op.ruleId];
    }
    service.SetFiltrationSettings((object) ius, str, filtrationSettings);
    ti.verRuleOwnerId = str;
    this.ReportVerRule(ti.taskId, ius, str);
  }

  internal FiltrationSettings CreateFiltSettings(
    ExpertServer.ExpServTask ti,
    string ownerId,
    long ruleId)
  {
    FiltrationSettings filtSettings = new FiltrationSettings();
    filtSettings.OwnerID = ownerId;
    lock (ti)
      ti.RulesList.Add(ruleId, new ExpertServer.RuleIdInfo(ruleId, ownerId));
    return filtSettings;
  }

  private void FillChildrenFields(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    DocumentTreeNode dtn,
    AttributeValues[] attr_vals,
    HybridRowExp row,
    HybridColumnsExp columns)
  {
    long num = -1;
    bool flag1 = false;
    Guid objectGuid = Guid.Empty;
    for (int index = 0; index < attr_vals.Length; ++index)
    {
      if (attr_vals[index].AttributeID == -5 || attr_vals[index].AttributeID == -2)
      {
        num = Convert.ToInt64(attr_vals[index].Values[0]);
        ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(num, -7008);
        object obj = (object) null;
        if (!ti.attrCache.TryGetValue(key, out obj))
        {
          TypedInfoItem itemData = ti.DataCache.GetItemData(num, ius);
          if (itemData is TaskDataCache.ObjDataItem)
          {
            objectGuid = ((TaskDataCache.ObjDataItem) itemData).ObjGuid;
            ti.attrCache.Add(key, (object) objectGuid);
            break;
          }
          if (itemData is TaskDataCache.RelDataItem)
          {
            objectGuid = ((TaskDataCache.RelDataItem) itemData).RelGuid;
            ti.attrCache.Add(key, (object) objectGuid);
            break;
          }
          break;
        }
        objectGuid = (Guid) obj;
        break;
      }
    }
    if (dtn.GetType() == typeof (TextData))
    {
      IDBAttributeType attributeType = ius.GetAttributeType(dtn.Name, false);
      bool flag2 = false;
      if (attributeType != null)
      {
        string str = Convert.ToString((object) attributeType.PropertiesStructure.AttributeGuid);
        if (row != null && columns != null)
        {
          for (int index = 0; index < columns.Count; ++index)
          {
            if (str.Equals(columns[index].ColumnName))
            {
              (dtn as TextData).Text = Convert.ToString(row[index]);
              if (num != -1L)
                dtn.SetAttributeValue(ExpertConsts.Consts.attrDocFldObject.ToString(), num.ToString());
              flag2 = true;
              break;
            }
          }
        }
        if (!flag2)
        {
          for (int index = 0; index < attr_vals.Length; ++index)
          {
            if (attr_vals[index].AttributeGuid.ToString() == str)
            {
              if (num != -1L)
              {
                dtn.SetAttributeValue(ExpertConsts.Consts.attrDocFldObject.ToString(), num.ToString());
                if (dtn is TextData && !flag1)
                  ((TextData) dtn).ReferenceToTextSource = (ReferenceBase) new ReferenceToDBObjectAttributeBase(dtn, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) new DBObjectInfo(objectGuid, num), attributeType.PropertiesStructure.AttributeGuid, attributeType.AttributeID, (string) null);
              }
              bool flag3;
              if (attr_vals[index].Descriptions != null)
              {
                (dtn as TextData).Text = Convert.ToString(attr_vals[index].Descriptions[0]);
                flag3 = true;
                break;
              }
              if (attr_vals[index].Values != null)
              {
                (dtn as TextData).Text = Convert.ToString(attr_vals[index].Values[0]);
                flag3 = true;
                break;
              }
              break;
            }
          }
        }
      }
    }
    if (dtn.Nodes == null)
      return;
    for (int index = 0; index < dtn.Nodes.Count; ++index)
      this.FillChildrenFields(ius, ti, dtn.Nodes[index], attr_vals, row, columns);
  }

  private string ConvertAttrValue(long id, bool rel, int attrId, object Value) => Value.ToString();

  private void _OpDocControl(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    OpParmDocControl op = (OpParmDocControl) node.op;
    PageData child = ti.docData.ClonePageFromTemplate(op.listId, false);
    if (child == null)
      return;
    if (!op.newList)
    {
      PageData node1 = (PageData) ti.docData.Nodes[0];
      for (int index1 = 0; index1 < node1.Flows.Count; ++index1)
      {
        TableData flow = node1.Flows[index1] as TableData;
        TableData tableForFlow = child.FindTableForFlow(flow);
        if (tableForFlow != null)
        {
          List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
          foreach (DocumentTreeNode node2 in flow.Nodes)
            documentTreeNodeList.Add(node2);
          for (int index2 = 0; index2 < documentTreeNodeList.Count; ++index2)
            tableForFlow.AddChildNode(documentTreeNodeList[index2], false, false);
        }
      }
      ti.docData.InsertChildNode(0, (DocumentTreeNode) child, false, true, false, false, false);
      ti.docData.RemoveChildNodeAt(1, false, false);
    }
    else
    {
      ti.docData.AddChildNode((DocumentTreeNode) child, false, false);
      if (!op.makeListCurrent)
        return;
      ti.curDocNode = (DocumentTreeNode) child;
    }
  }

  private void _OpRecalc(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    long objId = -1;
    if (context != null && context.Length != 0)
      objId = context[0];
    HybridRowExp row = (HybridRowExp) null;
    if (dTable != null)
      row = dTable[0];
    this.PerformRecalc(ius, ti, objId, row, (OpParmObject) node.op);
  }

  private void _OpUserProc(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    ScriptTreeNode node,
    long[] context,
    HybridTableExp dTable)
  {
    OpParmUserProc op = (OpParmUserProc) node.op;
    string Name = (string) null;
    XmlNode xmlNode = (XmlNode) null;
    XmlNode curNode = ti.curNode;
    if (ti.makeTrace)
    {
      switch (op.type)
      {
        case ExpertCalling.callProc:
          Name = LocalizationHolder.rm.GetString("Expert.Server_38");
          break;
        case ExpertCalling.callUserProc:
          Name = LocalizationHolder.rm.GetString("Expert.Server_39");
          break;
        case ExpertCalling.callScript:
          Name = LocalizationHolder.rm.GetString("Expert.Server_40");
          break;
        case ExpertCalling.callScenario:
          Name = LocalizationHolder.rm.GetString("Expert.Server_270");
          break;
      }
      xmlNode = ti.traceAddElement(Name);
      if (xmlNode != null)
      {
        ti.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_41"), op.procName);
        ti.traceSetNode(xmlNode);
      }
    }
    try
    {
      long[] new_context = (long[]) null;
      switch (op.type)
      {
        case ExpertCalling.callProc:
          this.ProcessScriptNode(ti.taskId, this.FindNode(ti.scriptRoot, op.procName) ?? throw new ExpertServerException($"{LocalizationHolder.rm.GetString("Expert.Server_42")}{op.procName}\""), context, dTable, true, ref new_context);
          break;
        case ExpertCalling.callUserProc:
          List<object> objectList1 = new List<object>();
          object result1 = (object) null;
          if (op.parm1 != null)
          {
            if (op.parm1.Count > 0)
            {
              try
              {
                int num = (int) ti.CalcFormula(new long[1]
                {
                  context[0]
                }, dTable?[0], op.parm1, out result1, 0L);
              }
              catch (ExpertServerException ex)
              {
                result1 = (object) "";
              }
              objectList1.Add((object) Convert.ToString(result1));
              goto label_21;
            }
          }
          objectList1.Add((object) null);
label_21:
          object result2 = (object) null;
          if (op.parm2 != null)
          {
            if (op.parm2.Count > 0)
            {
              try
              {
                int num = (int) ti.CalcFormula(new long[1]
                {
                  context[0]
                }, dTable?[0], op.parm2, out result2, 0L);
              }
              catch (ExpertServerException ex)
              {
                result2 = (object) "";
              }
              objectList1.Add((object) Convert.ToString(result2));
              goto label_27;
            }
          }
          objectList1.Add((object) null);
label_27:
          objectList1.Add((object) op.parm1);
          objectList1.Add((object) op.parm2);
          ExpertServer.CallProc(op.procName, ti, context, dTable, -1, -1, (object) objectList1);
          break;
        case ExpertCalling.callScript:
          ExpertScriptType scriptType = (ExpertScriptType) op.scriptType;
          DocScript scriptByName1 = (DocScript) this.GetScriptByName(ius, op.procName, scriptType == ExpertScriptType.DocScript ? ExpertConsts.Consts.objDocScript : ExpertConsts.Consts.objScript);
          if (this.FlagIn(ExpertTraceFlags.ShowExpertObjects, ti.traceFlags))
            this.ShowLoadObject(ti.taskId, (ExpertObject) scriptByName1);
          else
            scriptByName1.Load();
          try
          {
            scriptByName1.UnpackXML();
          }
          catch (Exception ex)
          {
            throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_43")}");
          }
          ScriptTreeNode scriptTreeNode = ExpertServer.LoadScriptTree(scriptByName1.xDoc);
          for (int index = 0; index < scriptTreeNode.Items.Count; ++index)
            this.ProcessScriptNode(ti.taskId, (ScriptTreeNode) scriptTreeNode.Items[index], context, dTable, false, ref new_context);
          break;
        case ExpertCalling.callScenario:
          IDBObject scriptByName2 = this.GetScriptByName(ius, op.procName, ExpertConsts.Consts.objExpScenario);
          object result3 = (object) null;
          int num1 = (int) ti.CalcFormula(new long[1]
          {
            context[0]
          }, dTable?[0], op.parm1, out result3, 0L);
          List<object> objectList2 = new List<object>();
          object result4 = (object) null;
          if (op.parm2 != null)
          {
            if (op.parm2.Count > 0)
            {
              try
              {
                int num2 = (int) ti.CalcFormula(new long[1]
                {
                  context[0]
                }, dTable?[0], op.parm2, out result4, 0L);
              }
              catch (ExpertServerException ex)
              {
                result4 = (object) "";
              }
              objectList2.Add((object) Convert.ToString(result4));
            }
          }
          ExpertServer.ExecScript(ius, scriptByName2.ObjectID, Convert.ToString(result3), (object) ti, (object) context, (object) dTable, (object) -1, (object) -1, (object) objectList2);
          break;
      }
    }
    catch (Exception ex)
    {
      if (xmlNode == null)
        return;
      ti.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_44"), ex.Message);
    }
    finally
    {
      ti.traceSetNode(curNode);
    }
  }

  private IDBObject GetScriptByName(IUserSession ius, string ScriptName, int scriptTypeId)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad00047-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) ScriptName, LogicalOperators.NONE, 0)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 1)
    });
    DataTable dataTable = ius.GetObjectCollection(scriptTypeId).Select(paramSet);
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return (IDBObject) null;
    long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
    return ius.GetObject(int64);
  }

  internal ScriptTreeNode FindNode(ScriptTreeNode root, string name)
  {
    if (root.label == name)
      return root;
    if (root.Items != null)
    {
      foreach (ScriptTreeNode root1 in root.Items)
      {
        ScriptTreeNode node = this.FindNode(root1, name);
        if (node != null)
          return node;
      }
    }
    return (ScriptTreeNode) null;
  }

  internal void PerformRecalc(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    long objId,
    HybridRowExp row,
    OpParmObject op)
  {
    long num1 = objId;
    long num2 = -1;
    if (row != null)
    {
      if (row[0] != null)
        num1 = Convert.ToInt64(row[0]);
      object obj = row["cad00033-306c-11d8-b4e9-00304f19f545"];
      if (obj.NotNullOrDBNull())
        num2 = Convert.ToInt64(obj);
    }
    IDBObject idbO1 = ius.GetObject(num1, false);
    IDBRelation idbO2 = (IDBRelation) null;
    if (num2 != -1L)
      idbO2 = ius.GetRelation(num2, false);
    XmlNode curNode = ti.curNode;
    XmlNode xmlNode = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_45")) : (XmlNode) null;
    if (xmlNode != null)
    {
      if (idbO1 != null)
        ti.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_46"), idbO1.Caption);
      ti.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_47"), Convert.ToString(num1));
      ti.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(objId));
      ti.traceSetNode(xmlNode);
    }
    try
    {
      if (op.dataAttrGUIDs == null || op.dataAttrGUIDs.Count <= 0)
        return;
      for (int index = 0; index < op.dataAttrGUIDs.Count; ++index)
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(Convert.ToString(op.dataAttrGUIDs[index])));
        bool attrCheck = op.GetAttrCheck(index);
        object val = (object) null;
        if (attrCheck && idbO2 != null)
        {
          if (idbO2 != null)
            this.PurgeResult(ti, num2, idbO2.RelationType, attributeTypeId);
        }
        else
          this.PurgeResult(ti, num1, idbO1 != null ? idbO1.ObjectType : -1, attributeTypeId);
        if (attrCheck && idbO2 != null)
        {
          if (idbO2 != null && this.CanHaveAttribute(ius, (IDBAttributable) idbO2, attributeTypeId))
          {
            int attr1 = (int) this.CalculateAttr(ti.taskId, idbO2.RelationType, attributeTypeId, num2, ExpertServer.CalcStages.CalcAttribute, out val);
          }
        }
        else
        {
          if (idbO1 == null || this.CanHaveAttribute(ius, (IDBAttributable) idbO1, attributeTypeId))
          {
            int attr2 = (int) this.CalculateAttr(ti.taskId, idbO1 != null ? idbO1.ObjectType : -1, attributeTypeId, num1, ExpertServer.CalcStages.CalcAttribute, out val);
          }
          if (attrCheck && ti.curRelationId != 0L)
          {
            CalcAttrPair key = new CalcAttrPair(objId, idbO1.ObjectType, attributeTypeId);
            long curRelationId = ti.curRelationId;
            int objTypeID = -1;
            CalculatedAttr calculatedAttr;
            if (ti.CalcAttrs.TryGetValue(key, out calculatedAttr))
            {
              IDBRelation relation = ius.GetRelation(ti.curRelationId, false);
              if (relation != null)
                objTypeID = relation.RelationType;
            }
            else if (ti.CalcAttrs.TryGetValue(objId, -1, attributeTypeId, out calculatedAttr))
              key.objTypeID = -1;
            if (calculatedAttr != null)
            {
              ti.CalcAttrs.Remove(key);
              CalcAttrPair calcAttrPair = new CalcAttrPair(curRelationId, objTypeID, attributeTypeId);
              calculatedAttr = new CalculatedAttr(calcAttrPair, calculatedAttr.Value, AttrState.Calculated);
              if (ti.CalcAttrs.ContainsKey(calcAttrPair))
                ti.CalcAttrs[calcAttrPair] = calculatedAttr;
              else
                ti.CalcAttrs.Add(calcAttrPair, calculatedAttr);
            }
          }
        }
      }
    }
    finally
    {
      ti.traceSetNode(curNode);
    }
  }

  internal bool CanHaveAttribute(IUserSession ius, IDBAttributable idbO, int attrTypeID)
  {
    return idbO is IDBObject ? ius.GetObjectType(((IDBObject) idbO).ObjectType).HasAttribute(attrTypeID) : ius.GetRelationType(((IDBRelation) idbO).RelationType).HasAttribute(attrTypeID);
  }

  private long GetPartId(IUserSession ius, long relId)
  {
    IDBRelation relation = ius.GetRelation(relId, false);
    if (relation == null)
      return relId;
    IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
    return attributeById == null ? relation.PartID : Convert.ToInt64(attributeById.Value);
  }

  private void SetAttrValue(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    long objID,
    string attrGUID,
    string objTypeGUID,
    HybridRowExp row,
    object Value,
    HybridTableExp dTable,
    OpParmSetting ops = null,
    long[] addObjIds = null,
    bool blockTrace = false)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGUID);
    if (objTypeGUID == "*")
    {
      if (ti.curDocNode == null)
        return;
      ti.curDocNode.SetAttributeValue(attributeTypeId.ToString(), Value.ToString());
    }
    else
    {
      ExpertServer.TempAttrStru tempAttrStru = ti.GetTempAttrStru(new Guid(attrGUID));
      if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
        objID = -1L;
      Guid guid = objTypeGUID == "" ? Guid.Empty : new Guid(objTypeGUID);
      int num1 = -1;
      if (guid != Guid.Empty)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(guid);
        if (objectType != null)
        {
          num1 = objectType.ObjectTypeID;
        }
        else
        {
          IMSRelationType relationType = MetaDataHelper.GetRelationType(guid);
          if (relationType != null)
            num1 = relationType.RelationTypeID;
        }
      }
      long objID1 = -1;
      bool flag1 = false;
      if (objID != -1L)
      {
        ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(objID, -7007);
        object obj = (object) null;
        long num2 = objID;
        TypedInfoItem itemData1 = ti.DataCache.GetItemData(objID, ius);
        bool flag2 = ti.attrCache.TryGetValue(key, out obj);
        if (itemData1 is TaskDataCache.ObjDataItem)
        {
          if (!flag2)
          {
            obj = (object) MetaDataHelper.GetObjectTypeGuid(itemData1.ItemTypeID);
            ti.attrCache.Add(key, obj);
          }
        }
        else if (itemData1 is TaskDataCache.RelDataItem)
        {
          if (!flag2)
          {
            obj = (object) MetaDataHelper.GetRelationTypeGuid(itemData1.ItemTypeID);
            ti.attrCache.Add(key, obj);
          }
          if (MetaDataHelper.GetObjectTypeID(guid) != -1)
          {
            long partId = this.GetPartId(ius, objID);
            IDBObject objectActualCopy = ius.GetObject(partId, false);
            if (objectActualCopy == null)
            {
              IDBObject objectBaseVersionById = ius.GetObjectBaseVersionByID(partId, false);
              objectActualCopy = ius.GetObjectActualCopy(objectBaseVersionById.ObjectID, false);
            }
            if (objectActualCopy != null)
            {
              obj = (object) MetaDataHelper.GetObjectTypeGuid(objectActualCopy.ObjectType);
              num2 = objectActualCopy.ObjectID;
            }
          }
        }
        if (obj.Equals((object) guid))
        {
          objID1 = num2;
          flag1 = true;
        }
        else if (addObjIds != null)
        {
          foreach (long addObjId in addObjIds)
          {
            TypedInfoItem itemData2 = ti.DataCache.GetItemData(addObjId, ius);
            if (itemData2 != (TypedInfoItem) null && itemData2.ItemTypeID == num1)
            {
              objID1 = addObjId;
              flag1 = false;
            }
          }
        }
      }
      if (objID1 == -1L && guid != Guid.Empty)
        objID1 = ExpertServer.AttributableId(this.FindObjectWithAttr(ti.taskId, ius, objID, ExpertConsts.Consts.attrObjectId, num1) ?? throw new EAbort(ExpertResult.ObjectNotFound, LocalizationHolder.rm.GetString("Expert.Server_48")));
      int X = -1;
      int Y = 0;
      if (ops != null && ops.hasArray)
      {
        if (ops.formX != null)
        {
          object Result;
          if (blockTrace)
          {
            int num3 = (int) ti._CalcFormula(new long[1]
            {
              objID
            }, row, ops.formX, out Result, false);
          }
          else
            Result = ti.CalcFormula(objID, row, ops.formX);
          X = Convert.ToInt32(Result);
        }
        if (ops.formY != null)
        {
          object Result;
          if (blockTrace)
          {
            int num4 = (int) ti._CalcFormula(new long[1]
            {
              objID
            }, row, ops.formY, out Result, false);
          }
          else
            Result = ti.CalcFormula(objID, row, ops.formY);
          Y = Convert.ToInt32(Result);
        }
      }
      bool flag3 = false;
      lock (ti)
        flag3 = !blockTrace && ti.makeTrace && this.FlagIn(ExpertTraceFlags.ShowAttrChanges, ti.traceFlags);
      if (flag3)
      {
        lock (ti)
        {
          XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_227"));
          if (node != null)
          {
            ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_207"), objID1 != -1L ? Convert.ToString(objID1) : "");
            ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_229"), attrGUID);
            ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_93"), Convert.ToString(Value));
            ti.traceAddAttribute(node, "_OBJ_ID_", Convert.ToString(objID1));
            if (X >= 0)
            {
              ti.traceAddAttribute(node, "index1", Convert.ToString(X));
              ti.traceAddAttribute(node, "index2", Convert.ToString(Y));
            }
          }
        }
      }
      CalcAttrPair calcAttrPair = new CalcAttrPair(objID, num1, attributeTypeId);
      AttrState aState = AttrState.Calculated;
      if (objID1 != -1L && objID1 != objID)
        aState = AttrState.SetByUser;
      object obj1 = this.InnerSetParm(ti, calcAttrPair, Value, aState, X, Y);
      Value = obj1;
      if (!blockTrace && aState == AttrState.Calculated)
        this.__ReportSetValue(ti, ius, false, calcAttrPair, Value);
      if (objID1 != -1L)
      {
        CalcAttrPair cap = new CalcAttrPair(objID1, num1, attributeTypeId);
        this.InnerSetParm(ti, cap, Value, AttrState.Calculated, X, Y);
      }
      if (!tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
      {
        if (flag1 && dTable != null)
        {
          for (int index = 0; index < dTable.Columns.Count; ++index)
          {
            if (attrGUID == dTable.Columns[index].ColumnName)
            {
              row[index] = obj1;
              break;
            }
          }
        }
        if (ti.savedData != null && ti.savedData.RowsCount > 0)
        {
          lock (ti)
          {
            HybridRowExp hybridRowExp = ti.savedDataByObjId(objID);
            if (hybridRowExp != null)
            {
              for (int index = 0; index < ti.savedData.Columns.Count; ++index)
              {
                if (attrGUID == ti.savedData.Columns[index].ColumnName)
                {
                  hybridRowExp[index] = obj1;
                  break;
                }
              }
            }
          }
        }
      }
      ExpertServer.ObjAttr key1 = new ExpertServer.ObjAttr(objID, attributeTypeId);
      if (ti.attrCache.ContainsKey(key1))
        ti.attrCache[key1] = obj1;
      else
        ti.attrCache.Add(key1, obj1);
    }
  }

  private object GetAttrValue(
    int taskId,
    IUserSession ius,
    long objID,
    string attrGUID,
    string objTypeGUID,
    HybridRowExp row,
    out ArcMethods arcMeth,
    bool useCache = true)
  {
    IDBAttributable dbAttributable = (IDBAttributable) null;
    arcMeth = ArcMethods.NotPacked;
    ExpertServer.ExpServTask task = this.taskList[taskId];
    if (task == null)
      return (object) null;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(attrGUID));
    if (attributeType == null)
    {
      XmlNode xmlNode = task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_98"));
      if (xmlNode != null)
        xmlNode.InnerText = string.Format(LocalizationHolder.rm.GetString("Expert.Server_257"), (object) attrGUID);
      return (object) null;
    }
    if (task.GetTempAttrStru(attributeType.AttributeGuid).HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
      objID = -1L;
    ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(objID, attributeType.AttributeID);
    object s = (object) null;
    int num = -1;
    if (objTypeGUID != string.Empty)
    {
      Guid guid = new Guid(objTypeGUID);
      IMSObjectType objectType = MetaDataHelper.GetObjectType(guid);
      if (objectType != null)
      {
        num = objectType.ObjectTypeID;
      }
      else
      {
        IMSRelationType relationType = MetaDataHelper.GetRelationType(guid);
        if (relationType != null)
          num = relationType.RelationTypeID;
      }
    }
    TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(objID, ius);
    int childTypeID = (TypedInfoItem) objData != (TypedInfoItem) null ? objData.ObjTypeID : -1;
    bool flag = false;
    try
    {
      s = this._GetParmValue(task, objID, num, attributeType.AttributeID);
      if (s != null)
        return s;
      if ((num == -1 || ExpertServer.IsTypeDescendant(num, childTypeID)) && row != null)
      {
        HybridColumnsExp columns = row.Columns;
        for (int index = 0; index < columns.Count; ++index)
        {
          if (columns[index].ColumnName == attrGUID)
          {
            s = row[index];
            if (s is MemoProxyReader memoProxyReader)
            {
              if (!memoProxyReader.Loaded)
              {
                memoProxyReader.LoadData(ius);
                string str = ExpertServer.es.MakeSubstitute(task, objID, columns[index].attrTypeId, memoProxyReader.Value);
                if (str != memoProxyReader.Value)
                {
                  memoProxyReader.Value = str;
                  ExpertServer.es.SetParmValue(task.taskId, objID, columns[index].attrTypeId, (object) str);
                }
              }
              return (object) memoProxyReader.Value;
            }
            if ((object) (s as Unknown) != null || s != null)
              return s;
          }
        }
      }
      if ((object) (s as Unknown) != null)
        flag = true;
      if (task.attrCache.TryGetValue(key, out s))
      {
        if (s is ExpertServer.PackedValue)
        {
          ExpertServer.PackedValue packedValue = (ExpertServer.PackedValue) s;
          arcMeth = packedValue.am;
          s = packedValue.Val;
        }
        return s;
      }
      if (objID != -1L)
      {
        Guid childTypeGUID = Guid.Empty;
        TypedInfoItem itemData = task.DataCache.GetItemData(objID, ius);
        switch (itemData)
        {
          case TaskDataCache.ObjDataItem _:
            childTypeGUID = MetaDataHelper.GetObjectTypeGuid(itemData.ItemTypeID);
            break;
          case TaskDataCache.RelDataItem _:
            childTypeGUID = MetaDataHelper.GetRelationTypeGuid(itemData.ItemTypeID);
            break;
        }
        Guid rootTypeGUID = Guid.Empty;
        if (objTypeGUID != "")
          rootTypeGUID = new Guid(objTypeGUID);
        if (objTypeGUID == "" || ExpertServer.IsTypeDescendant(rootTypeGUID, childTypeGUID))
          dbAttributable = ExpertServer.GetAttributable(ius, objID);
      }
      if (dbAttributable == null && objTypeGUID != "")
      {
        dbAttributable = this.FindObjectWithAttr(taskId, ius, objID, attributeType.AttributeID, num, useCache);
        if (dbAttributable == null)
        {
          s = (object) null;
          return (object) null;
        }
      }
      if (dbAttributable != null)
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(attrGUID));
        if (attributeTypeId < 0)
        {
          if (attributeType.AttributeID < 0)
          {
            object[] valuesByGuid = dbAttributable.GetValuesByGuid(new Guid(attrGUID), false);
            if (valuesByGuid.Length != 0)
            {
              s = valuesByGuid[0];
              return s;
            }
          }
          s = (object) null;
          return (object) null;
        }
        IDBAttribute attributeById = dbAttributable.GetAttributeByID(attributeTypeId);
        if (attributeById != null)
        {
          if (attributeById.DataType == FieldTypes.ftBlob)
          {
            if (attributeById is IBlobReader blobReader)
            {
              BlobInformation blobInformation = blobReader.OpenBlob(0);
              try
              {
                arcMeth = blobInformation.ArcMethod;
                s = (object) blobReader.ReadDataBlock((int) blobInformation.RealFileSize);
                return s;
              }
              finally
              {
                blobReader.CloseBlob();
              }
            }
          }
          else if (attributeById.DataType == FieldTypes.ftMemo)
          {
            s = (object) Convert.ToString(dbAttributable.GetValuesByGuid(new Guid(attrGUID), true)[0]);
            string str = ExpertServer.es.MakeSubstitute(task, objID, attributeTypeId, (string) s);
            if ((object) str != s)
            {
              s = (object) str;
              ExpertServer.es.SetParmValue(task.taskId, objID, attributeTypeId, (object) str);
            }
            return s;
          }
          s = attributeById.Value;
          return attributeById.Value;
        }
      }
    }
    finally
    {
      if ((object) (s as Unknown) == null)
      {
        if (!task.attrCache.ContainsKey(key))
        {
          if (arcMeth != ArcMethods.NotPacked)
            s = (object) new ExpertServer.PackedValue(s, arcMeth);
          task.attrCache.Add(key, s);
        }
        if (flag && task.savedData != null)
        {
          int indexByName = task.savedData.Columns.GetIndexByName(attrGUID);
          if (indexByName > 0)
          {
            HybridRowExp hybridRowExp = task.savedDataByObjId(objID);
            if (hybridRowExp != null && (object) (hybridRowExp[indexByName] as Unknown) != null)
              hybridRowExp[indexByName] = s;
          }
        }
      }
    }
    return (object) null;
  }

  internal bool _RecalcForAttr(int taskId, long objId, int attrTypeID, long relID)
  {
    bool flag1 = false;
    string Text = "";
    Guid empty = Guid.Empty;
    Guid objTypeGUID = Guid.Empty;
    Guid attrTypeGUID = Guid.Empty;
    string str = "";
    int num = -1;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    ExpertServer.GetSessionGuid(task);
    IUserSession session = this.GetSession(task);
    TypedInfoItem itemData = task.DataCache.GetItemData(objId, session);
    if (itemData is TaskDataCache.ObjDataItem)
      num = itemData.ItemTypeID;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
    if (attributeType != null)
    {
      attrTypeGUID = attributeType.AttributeGuid;
      str = attributeType.Name;
    }
    if (num != -1)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(num);
      if (objectType != null)
        objTypeGUID = objectType.Guid;
    }
    XmlNode curNode = task.curNode;
    bool flag2 = this.IsJobRunning(taskId);
    try
    {
      if (!flag2)
        this.StartJobForTask(taskId);
      this.StartModifyTrace(taskId);
      try
      {
        XmlNode xmlNode = task.makeTrace ? task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_49")) : (XmlNode) null;
        if (xmlNode != null)
        {
          task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_207"), Convert.ToString(objId));
          task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_230"), str);
          task.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(objId));
          task.traceSetNode(xmlNode);
        }
      }
      finally
      {
        this.EndModifyTrace(task);
      }
      ScriptTreeNode scriptTreeNode = this.LoadRecalcScript(task, session, objTypeGUID, attrTypeGUID, num, attrTypeID) ?? this.LoadRecalcScript(task, session, Guid.Empty, attrTypeGUID, -1, attrTypeID);
      if (scriptTreeNode == null)
      {
        task.traceAddText(task.curNode, LocalizationHolder.rm.GetString("Expert.Server_50"));
        return false;
      }
      ExpertScriptType curScrType = task.curScrType;
      long curRelationId = task.curRelationId;
      task.curRelationId = relID;
      try
      {
        task.curScrType = ExpertScriptType.RecalcScript;
        long[] new_context = (long[]) null;
        for (int index = 0; index < scriptTreeNode.Items.Count; ++index)
        {
          try
          {
            long[] context = new long[1]{ objId };
            this.ProcessScriptNode(taskId, (ScriptTreeNode) scriptTreeNode.Items[index], context, (HybridTableExp) null, true, ref new_context);
          }
          catch (EAbort ex)
          {
            break;
          }
          if (task.BreakFlag)
          {
            task.BreakFlag = false;
            break;
          }
        }
      }
      finally
      {
        task.curScrType = curScrType;
        task.curRelationId = curRelationId;
      }
      return true;
    }
    catch (Exception ex)
    {
      flag1 = true;
      Text = ex.Message;
      throw;
    }
    finally
    {
      this.StartModifyTrace(taskId);
      try
      {
        if (flag1)
        {
          XmlNode node = task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_52"));
          if (node != null)
            task.traceAddText(node, Text);
        }
        task.traceSetNode(curNode);
      }
      finally
      {
        this.EndModifyTrace(task);
        if (!flag2)
          this.EndJobForTask(taskId);
      }
    }
  }

  internal bool HasSpecialSort(ScriptTreeNode node)
  {
    if (node.op is OpParmObject op && op.InbuiltSort)
      return true;
    return node.mod is ModParmSort mod && mod.useInbuiltSort;
  }

  internal bool HasSpecSortRecursive(ScriptTreeNode root)
  {
    if (this.HasSpecialSort(root))
      return true;
    if (root.Items != null)
    {
      foreach (ScriptTreeNode root1 in root.Items)
      {
        if (this.HasSpecSortRecursive(root1))
          return true;
      }
    }
    return false;
  }

  internal IDBAttributable FindObjectWithAttr(
    int taskId,
    IUserSession ius,
    long objId,
    int attrTypeId,
    int objTypeId,
    bool useCache = true)
  {
    bool flag1 = false;
    long objectID1 = -1;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (objId != -1L)
    {
      TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(objId, ius);
      int childTypeID1 = (TypedInfoItem) objData != (TypedInfoItem) null ? objData.ObjTypeID : -1;
      try
      {
        if (ExpertServer.IsTypeDescendant(objTypeId, childTypeID1))
        {
          IDBObject objectWithAttr = ius.GetObject(objId);
          if (attrTypeId >= 0)
          {
            if (objectWithAttr.GetAttributeByID(attrTypeId) == null)
              goto label_7;
          }
          return (IDBAttributable) objectWithAttr;
        }
      }
      catch
      {
      }
label_7:
      if (childTypeID1 != -1)
      {
        int childTypeID2 = childTypeID1;
        if (ExpertServer.IsTypeDescendant(objTypeId, childTypeID2))
        {
          IDBAttributable dbAttributable = (IDBAttributable) ius.GetObject(objId, false);
          return dbAttributable == null || attrTypeId >= 0 && dbAttributable.GetAttributeByID(attrTypeId) == null ? (IDBAttributable) null : dbAttributable;
        }
      }
      else
      {
        TaskDataCache.RelDataItem relData = task.DataCache.GetRelData(objId, ius);
        if ((TypedInfoItem) relData != (TypedInfoItem) null && relData.RelTypeID != -1)
        {
          flag1 = true;
          IDBAttributable relation = (IDBAttributable) ius.GetRelation(objId, false);
          if (relation != null)
          {
            IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
            if (attributeById != null)
            {
              objectID1 = Convert.ToInt64(attributeById.Value);
            }
            else
            {
              long partId = ((IDBRelation) relation).PartID;
              objectID1 = ius.GetObjectByVersionsRule(partId, task.verRuleOwnerId, false).ObjectID;
            }
          }
          if (objectID1 != -1L)
          {
            IDBObject objectWithAttr = ius.GetObject(objectID1, false);
            if (objectWithAttr != null && ExpertServer.IsTypeDescendant(objTypeId, objectWithAttr.ObjectType))
              return (IDBAttributable) objectWithAttr;
          }
          IDBObject projObject = ((IDBRelation) relation).ProjObject;
          if (projObject != null && ExpertServer.IsTypeDescendant(objTypeId, projObject.ObjectType))
            return (IDBAttributable) projObject;
        }
      }
    }
    Guid objTypeGUID = Guid.Empty;
    Guid attrTypeGUID = Guid.Empty;
    string str1 = "";
    string str2 = "";
    IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeId);
    if (objectType != null)
    {
      objTypeGUID = objectType.Guid;
      str1 = objectType.ObjectTypeName;
    }
    else
    {
      IMSRelationType relationType = MetaDataHelper.GetRelationType(objTypeId);
      if (relationType != null)
      {
        objTypeGUID = relationType.Guid;
        str1 = relationType.TypeName;
      }
    }
    if (attrTypeId != -1)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeId);
      if (attributeType != null)
      {
        attrTypeGUID = attributeType.AttributeGuid;
        str2 = attributeType.Name;
      }
    }
    ExpertServer.OldKey key = new ExpertServer.OldKey(objId, (long) objTypeId);
    if (useCache && task.foundObjects.ContainsKey((object) key))
    {
      long foundObject = (long) task.foundObjects[(object) key];
      if (this.FlagIn(ExpertTraceFlags.TraceObjectSearch, task.traceFlags))
      {
        XmlNode node = task.makeTrace ? task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_285")) : (XmlNode) null;
        if (node != null)
        {
          task.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_120"), str1);
          task.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_121"), str2);
          task.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_122"), Convert.ToString(objId));
          task.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_123"), Convert.ToString(foundObject));
          task.traceAddAttribute(node, "_OBJ_ID_", Convert.ToString(objId));
        }
      }
      return foundObject == -1L ? (IDBAttributable) null : (IDBAttributable) ius.GetObject(foundObject);
    }
    ScriptTreeNode scriptTreeNode = this.LoadObjRule(task, ius, objTypeGUID, attrTypeGUID, objTypeId, attrTypeId);
    if (scriptTreeNode == null && objTypeId != -1)
      scriptTreeNode = this.LoadObjRule(task, ius, objTypeGUID, Guid.Empty, objTypeId, -1);
    bool flag2 = false;
    lock (task)
    {
      flag2 = this.FlagIn(ExpertTraceFlags.TraceObjectSearch, task.traceFlags);
      if (flag2)
      {
        XmlNode xmlNode = task.makeTrace ? task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_119")) : (XmlNode) null;
        if (xmlNode != null)
        {
          task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_120"), str1);
          task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_121"), str2);
          task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_122"), Convert.ToString(objId));
          task.traceAddText(xmlNode, LocalizationHolder.rm.GetString(scriptTreeNode != null ? "Expert.Server_203" : "Expert.Server_204"));
          task.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(objId));
          task.traceSetNode(xmlNode);
        }
      }
      else
        ++task.blockTrace;
    }
    if (scriptTreeNode != null)
    {
      XmlNode curNode = task.curNode;
      try
      {
        long objectID2 = -1;
        ExpertScriptType curScrType = task.curScrType;
        long[] new_context = (long[]) null;
        long curRelationId = flag1 ? task.curRelationId : 0L;
        try
        {
          task.curScrType = ExpertScriptType.ObjectRule;
          if (flag1)
            task.curRelationId = objId;
          try
          {
            long[] context = new long[1]
            {
              flag1 ? objectID1 : objId
            };
            if (task.savedData != null && task.savedLinks != null)
            {
              task.forceSearchByGlobal = true;
              for (int index = 0; index < scriptTreeNode.Items.Count; ++index)
                this.ProcessScriptNode(taskId, (ScriptTreeNode) scriptTreeNode.Items[index], context, (HybridTableExp) null, true, ref new_context);
              task.forceSearchByGlobal = false;
            }
            for (int index = 0; index < scriptTreeNode.Items.Count; ++index)
              this.ProcessScriptNode(taskId, (ScriptTreeNode) scriptTreeNode.Items[index], context, (HybridTableExp) null, true, ref new_context);
          }
          catch (EObjectFound ex)
          {
            objectID2 = ex.objId;
          }
          catch
          {
          }
          finally
          {
            task.forceSearchByGlobal = false;
          }
          if (task.BreakFlag)
            task.BreakFlag = false;
        }
        finally
        {
          task.curRelationId = curRelationId;
          task.curScrType = curScrType;
        }
        if (objectID2 != -1L)
        {
          IDBObject objectWithAttr = ius.GetObject(objectID2);
          if (flag2 && task.makeTrace)
          {
            task.traceAddAttribute(task.curNode, LocalizationHolder.rm.GetString("Expert.Server_123"), Convert.ToString(objectID2));
            task.traceAddAttribute(task.curNode, "_OBJ_ID_", Convert.ToString(objectID2));
          }
          if (!task.foundObjects.ContainsKey((object) key))
            task.foundObjects.Add((object) key, (object) objectID2);
          return (IDBAttributable) objectWithAttr;
        }
        if (flag2)
          task.traceAddText(task.curNode, LocalizationHolder.rm.GetString("Expert.Server_205"));
        if (!task.foundObjects.ContainsKey((object) key))
          task.foundObjects.Add((object) key, (object) -1L);
      }
      finally
      {
        lock (task)
        {
          task.traceSetNode(curNode);
          --task.blockTrace;
        }
      }
    }
    else
      --task.blockTrace;
    return (IDBAttributable) null;
  }

  private ExpertResult _Calculate(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    out object Value,
    long[] moreObjs = null)
  {
    ExpertResult expertResult1 = ExpertResult.OK;
    Value = (object) null;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    this.StartJobForTask(taskId);
    try
    {
      string Name = "";
      ExpertResult expertResult2 = ExpertResult.Unknown;
      this.GetSession(task);
      Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeID);
      ExpertServer.TempAttrStru tempAttrStru = task.GetTempAttrStru(attributeTypeGuid);
      if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
        objId = -1L;
      CalculatedAttr calculatedAttr = (CalculatedAttr) null;
      if (task.CalcAttrs.TryGetValue(objId, objTypeID, attrTypeID, out calculatedAttr))
      {
        Value = calculatedAttr.Value;
        expertResult2 = ExpertResult.OK;
        Name = LocalizationHolder.rm.GetString("Expert.Server_258");
      }
      if (task.NeededAttrs.ContainsAttr(objId, objTypeID, attrTypeID))
      {
        Value = (object) null;
        expertResult2 = ExpertResult.CircularReference;
        Name = LocalizationHolder.rm.GetString("Expert.Server_259");
      }
      if (Name != "")
      {
        XmlNode curNode = task.curNode;
        try
        {
          if (this.FlagIn(ExpertTraceFlags.TraceAttribSearch, task.traceFlags))
          {
            XmlNode xmlNode = task.makeTrace ? task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_139")) : (XmlNode) null;
            if (xmlNode != null)
            {
              task.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(objId));
              if (objTypeID != -1)
                task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_140"), Convert.ToString(objTypeID));
              string str = "";
              if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
                str = !tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithObject) ? " " + LocalizationHolder.rm.GetString("Expert.Server_271") : " " + LocalizationHolder.rm.GetString("Expert.Server_277");
              else if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithObject))
                str = " " + LocalizationHolder.rm.GetString("Expert.Server_276");
              task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_141"), Convert.ToString(attrTypeID) + str);
              task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_142"), Convert.ToString(objId));
              task.traceSetNode(xmlNode);
              task.traceAddElement(Name);
            }
          }
        }
        finally
        {
          task.traceSetNode(curNode);
        }
        return expertResult2;
      }
      expertResult1 = this.CalculateAttr(taskId, objTypeID, attrTypeID, objId, ExpertServer.CalcStages.CalcAttribute, out Value, moreObjIDs: moreObjs);
      if (expertResult1 == ExpertResult.RuleNotFound)
        expertResult1 = this.CalculateAttr(taskId, objTypeID, attrTypeID, objId, ExpertServer.CalcStages.FindObject, out Value, moreObjIDs: moreObjs);
    }
    finally
    {
      this.EndJobForTask(taskId);
    }
    return expertResult1;
  }

  private ExpertResult _CalculateAllStages(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    bool DisableTrace,
    out object Value)
  {
    ExpertResult allStages = ExpertResult.OK;
    Value = (object) null;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    IUserSession session = this.GetSession(task);
    Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeID);
    if (task.GetTempAttrStru(attributeTypeGuid).HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
      objId = -1L;
    CalcAttrPair key = new CalcAttrPair(objId, objTypeID, attrTypeID);
    CalculatedAttr calculatedAttr1 = task.__GetValue(key);
    if (calculatedAttr1 != null)
    {
      Value = calculatedAttr1.Value;
      return ExpertResult.OK;
    }
    bool flag = false;
    if (key.objTypeID != -1)
      key.objTypeID = -1;
    else if (objId != -1L)
    {
      TypedInfoItem itemData = task.DataCache.GetItemData(objId, session);
      if (itemData != (TypedInfoItem) null)
      {
        key.objTypeID = itemData.ItemTypeID;
        flag = itemData is TaskDataCache.RelDataItem;
      }
    }
    CalculatedAttr calculatedAttr2 = task.__GetValue(key);
    if (calculatedAttr2 != null)
    {
      Value = calculatedAttr2.Value;
      return ExpertResult.OK;
    }
    ExpertTraceFlags traceFlags = task.traceFlags;
    try
    {
      if (DisableTrace)
        task.traceFlags = ExpertTraceFlags.None;
      long[] array = task._addObjs?.ToArray();
      allStages = this.CalculateAttr(taskId, objTypeID, attrTypeID, objId, ExpertServer.AllStages, out Value, moreObjIDs: array);
      if (Value == null & flag)
      {
        IDBRelation relation = session.GetRelation(objId, false);
        if (relation != null)
        {
          IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
          objId = attributeById == null ? session.GetObjectVersions(relation.PartID)[0] : attributeById.AsInteger;
          TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(objId, session);
          key.objTypeID = objData.ObjTypeID;
          CalculatedAttr calculatedAttr3 = task.__GetValue(key);
          if (calculatedAttr3 != null)
          {
            Value = calculatedAttr3.Value;
            return ExpertResult.OK;
          }
          allStages = this.CalculateAttr(taskId, objData.ObjTypeID, attrTypeID, objId, ExpertServer.AllStages, out Value, moreObjIDs: array);
        }
      }
    }
    finally
    {
      if (DisableTrace)
        task.traceFlags = traceFlags;
    }
    return allStages;
  }

  private ExpertResult InnerCalculate(
    int taskId,
    IUserSession ius,
    int objTypeID,
    int attrTypeID,
    Guid objTypeGuid,
    Guid attrTypeGuid,
    long objId,
    out object Value,
    long contObjId = -1,
    long[] moreObjIDs = null)
  {
    bool flag = false;
    string Text1 = "";
    ExpertResult expertResult = ExpertResult.OK;
    Value = (object) null;
    if (contObjId == -1L)
      contObjId = objId;
    try
    {
      ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
      try
      {
        XmlNode xmlNode = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_124")) : (XmlNode) null;
        if (xmlNode != null)
        {
          ti.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(contObjId));
          ti.traceSetNode(xmlNode);
        }
        ti.objectFound = false;
      }
      finally
      {
        this.EndModifyTrace(ti);
      }
      ScriptTreeNode scriptTreeNode = this.LoadAttrRule(ti, ius, objTypeGuid, attrTypeGuid, objTypeID, attrTypeID) ?? this.LoadAttrRule(ti, ius, Guid.Empty, attrTypeGuid, -1, attrTypeID);
      if (scriptTreeNode == null)
      {
        expertResult = ExpertResult.RuleNotFound;
        return expertResult;
      }
      Guid objTypeGUID = Guid.Empty;
      TypedInfoItem itemData = ti.DataCache.GetItemData(objId, ius);
      switch (itemData)
      {
        case TaskDataCache.ObjDataItem _:
          objTypeGUID = MetaDataHelper.GetObjectTypeGuid(itemData.ItemTypeID);
          break;
        case TaskDataCache.RelDataItem _:
          objTypeGUID = MetaDataHelper.GetRelationTypeGuid(itemData.ItemTypeID);
          IDBRelation relation = ius.GetRelation((itemData as TaskDataCache.RelDataItem).RelationID);
          if (relation != null)
          {
            contObjId = relation.PartObjectID;
            if (contObjId == 0L)
            {
              List<long> objectVersions = ius.GetObjectVersions(relation.PartID);
              if (objectVersions.Count > 0)
                contObjId = objectVersions[0];
            }
            ti.curRelationId = relation.RelationID;
            break;
          }
          break;
      }
      ExpertScriptType curScrType = ti.curScrType;
      ScriptTreeNode scriptRoot = ti.scriptRoot;
      try
      {
        ti.curScrType = ExpertScriptType.AttribRule;
        for (int index = 0; index < scriptTreeNode.Items.Count; ++index)
        {
          bool? nullable = new bool?(false);
          try
          {
            nullable = this.ProcessRuleScriptNode(taskId, (ScriptTreeNode) scriptTreeNode.Items[index], objId, objTypeGUID, contObjId, moreObjIDs);
          }
          catch (EAbort ex)
          {
            expertResult = ex.res;
            break;
          }
          if (ti.BreakFlag)
          {
            ti.BreakFlag = false;
            break;
          }
          if (nullable.HasValue)
          {
            if (nullable.Value)
              break;
          }
        }
      }
      finally
      {
        ti.curScrType = curScrType;
        ti.scriptRoot = scriptRoot;
      }
      if (this._resultFound(ti, contObjId))
      {
        expertResult = ExpertResult.OK;
        Value = ti.CalcAttrs[ti.calcStack.curCalcItem].Value;
      }
      else
      {
        if (!ti.objectFound)
          expertResult = ExpertResult.NoSuitableObjects;
        if (expertResult == ExpertResult.NoCalcParms)
          this.ReportNeededParms(ti);
      }
      return expertResult;
    }
    catch (Exception ex)
    {
      flag = true;
      Text1 = ex.Message;
      throw;
    }
    finally
    {
      ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
      XmlNode curNode = ti.curNode;
      try
      {
        if (flag)
        {
          XmlNode node = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_126")) : (XmlNode) null;
          if (node != null)
          {
            ti.traceAddAttribute(node, "_OBJ_ID_", Convert.ToString(contObjId));
            ti.traceAddText(node, Text1);
          }
        }
        else
        {
          XmlNode node = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_127")) : (XmlNode) null;
          if (node != null)
          {
            ti.traceAddAttribute(node, "_OBJ_ID_", Convert.ToString(contObjId));
            ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_128"), Convert.ToString((object) expertResult));
            string Text2 = Value == null ? "<null>" : Convert.ToString(Value);
            ti.traceAddText(node, Text2);
          }
        }
      }
      finally
      {
        ti.traceSetNode(curNode);
        this.EndModifyTrace(ti);
      }
    }
  }

  private ExpertResult InnerCalculateQuiet(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    int objTypeID,
    int attrTypeID,
    long objId,
    out object Value,
    long contObjId = -1,
    long[] moreObjIDs = null)
  {
    ExpertResult quiet = ExpertResult.OK;
    Value = (object) null;
    Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeID);
    Guid objTypeGUID = Guid.Empty;
    if (objTypeID != -1)
      objTypeGUID = MetaDataHelper.GetObjectTypeGuid(objTypeID);
    if (contObjId == -1L)
      contObjId = objId;
    ScriptTreeNode scriptTreeNode = this.LoadAttrRuleQuiet(ius, objTypeGUID, attributeTypeGuid, objTypeID, attrTypeID) ?? this.LoadAttrRuleQuiet(ius, Guid.Empty, attributeTypeGuid, -1, attrTypeID);
    if (scriptTreeNode == null)
      return ExpertResult.RuleNotFound;
    lock (ti)
      ti.calcStack.Push(objId, objTypeID, attrTypeID);
    ExpertScriptType curScrType = ti.curScrType;
    try
    {
      ti.curScrType = ExpertScriptType.AttribRule;
      for (int index = 0; index < scriptTreeNode.Items.Count; ++index)
      {
        try
        {
          this.ProcessRuleScriptNode(ti.taskId, (ScriptTreeNode) scriptTreeNode.Items[index], objId, Guid.Empty, contObjId, moreObjIDs);
        }
        catch (EAbort ex)
        {
          quiet = ex.res;
          break;
        }
        if (ti.BreakFlag)
        {
          ti.BreakFlag = false;
          break;
        }
        if (this._resultFound(ti))
          break;
      }
    }
    finally
    {
      ti.curScrType = curScrType;
      if (this._resultFound(ti))
      {
        quiet = ExpertResult.OK;
        Value = ti.CalcAttrs[ti.calcStack.curCalcItem].Value;
      }
      else if (!ti.objectFound)
        quiet = ExpertResult.NoSuitableObjects;
      ti.calcStack.Pop();
    }
    return quiet;
  }

  private bool? ProcessRuleScriptNode(
    int taskId,
    ScriptTreeNode node,
    long objId,
    Guid objTypeGUID,
    long contObjId = -1,
    long[] moreObjIds = null)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) null;
    try
    {
      this._CheckTaskId(taskId, out ti);
    }
    catch (EAbort ex)
    {
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_243"));
    }
    if (this._resultFound(ti))
      return new bool?(false);
    IUserSession session = this.GetSession(ti);
    if (contObjId == -1L)
      contObjId = objId;
    if (node.label.StartsWith("#"))
      return new bool?(false);
    XmlNode curNode1 = (XmlNode) null;
    this.ReportScriptNode(taskId, node, new long[1]
    {
      contObjId
    }, true, out curNode1);
    bool? nullable = new bool?(false);
    try
    {
      this._CheckScriptNode(taskId, node);
      switch (node.opTag)
      {
        case ExpertScriptOp.opExit:
          ti.BreakFlag = true;
          break;
        case ExpertScriptOp.opFolder:
        case ExpertScriptOp.opSelFolder:
        case ExpertScriptOp.opObjType:
          TempFormula cond1;
          if (node.opTag == ExpertScriptOp.opObjType)
          {
            OpParmType op = (OpParmType) node.op;
            if (op.objTypeGUID == "")
            {
              if (ti.makeTrace)
              {
                ti.traceAddText(ti.curNode, LocalizationHolder.rm.GetString("Expert.Server_241"));
                break;
              }
              break;
            }
            if (!GuidHelper.IsGuid(op.objTypeGUID))
            {
              if (ti.makeTrace)
              {
                ti.traceAddText(ti.curNode, string.Format(LocalizationHolder.rm.GetString("Expert.Server_286"), (object) node.GetNodeDescr()));
                break;
              }
              break;
            }
            Guid guid = new Guid(op.objTypeGUID);
            if (ti.makeTrace && !guid.Equals(objTypeGUID))
            {
              ti.traceAddAttribute(ti.curNode, LocalizationHolder.rm.GetString("Expert.Server_129"), op.objTypeText);
              ti.traceAddAttribute(ti.curNode, "_OBJ_ID_", Convert.ToString(contObjId));
              ti.traceAddText(ti.curNode, LocalizationHolder.rm.GetString("Expert.Server_130"));
              break;
            }
            cond1 = op.cond;
          }
          else
            cond1 = ((OpParmCond) node.op).cond;
          if (cond1 != null)
          {
            if (!ti.CheckCond(contObjId, cond1, 0L))
              break;
          }
          for (int index = 0; index < node.Items.Count; ++index)
          {
            ScriptTreeNode node1 = (ScriptTreeNode) node.Items[index];
            if (node.opTag == ExpertScriptOp.opSelFolder)
            {
              TempFormula selFolderCond = this.GetSelFolderCond(node1);
              if (selFolderCond == null || ti.CheckCond(contObjId, selFolderCond, 0L))
              {
                this.ProcessRuleScriptNode(taskId, node1, objId, objTypeGUID, contObjId, moreObjIds);
                break;
              }
            }
            else
              this.ProcessRuleScriptNode(taskId, node1, objId, objTypeGUID, contObjId, moreObjIds);
            if (ti.BreakFlag)
            {
              ti.BreakFlag = false;
              break;
            }
          }
          break;
        case ExpertScriptOp.opByFormula:
        case ExpertScriptOp.opByTable:
        case ExpertScriptOp.opByScript:
          OpParmExpObj op1 = (OpParmExpObj) node.op;
          string objTypeGuid = op1.objTypeGUID;
          if (!GuidHelper.IsGuid(objTypeGuid))
          {
            if (ti.makeTrace)
            {
              ti.traceAddText(ti.curNode, string.Format(LocalizationHolder.rm.GetString("Expert.Server_286"), (object) node.GetNodeDescr()));
              break;
            }
            break;
          }
          Guid guid1 = new Guid(objTypeGuid);
          bool flag1 = !this.expertObjInfo.ContainsKey(guid1);
          QuickObjectInfo quickObjectInfo = flag1 ? session.GetObjectInfo(guid1) : this.expertObjInfo[guid1].Value;
          ExpertObject expertObject = (ExpertObject) null;
          if (!flag1)
          {
            switch (node.opTag)
            {
              case ExpertScriptOp.opByFormula:
                flag1 = !this.expertFormulae.ContainsKey(quickObjectInfo.ObjectID);
                break;
              case ExpertScriptOp.opByTable:
                flag1 = !this.expertTables.ContainsKey(quickObjectInfo.ObjectID);
                break;
              case ExpertScriptOp.opByScript:
                flag1 = !this.expertScripts.ContainsKey(quickObjectInfo.ObjectID);
                break;
            }
          }
          if (flag1)
          {
            expertObject = (ExpertObject) session.GetObject(guid1, false);
            if (expertObject == null)
            {
              ti.traceAddText(ti.curNode, LocalizationHolder.rm.GetString("Expert.Server_131") + objTypeGuid + LocalizationHolder.rm.GetString("Expert.Server_132"));
              break;
            }
            quickObjectInfo.ObjectID = expertObject.ObjectID;
            this.AddOrUpdate<Guid, QuickObjectInfo>(this.expertObjInfo, guid1, quickObjectInfo);
          }
          long objectId = quickObjectInfo.ObjectID;
          if (op1.cond != null)
          {
            if (!ti.CheckCond(objId, op1.cond, objectId, moreObjIds))
              break;
          }
          if (ti.makeTrace)
            ti.traceAddAttribute(ti.curNode, "_OBJ_ID_", Convert.ToString(objectId));
          eTableCollection tableCollection = (eTableCollection) null;
          if (flag1 && expertObject != null)
          {
            if (ti.makeTrace && this.FlagIn(ExpertTraceFlags.ShowExpertObjects, ti.traceFlags))
            {
              tableCollection = this.ShowLoadObject(taskId, expertObject);
            }
            else
            {
              expertObject.Load();
              if (expertObject.GetType() == typeof (ExpertTable))
                tableCollection = ((ExpertTable) expertObject).LoadTableData();
            }
          }
          TempFormula cond2 = this.expertConds.ContainsKey(objectId) ? this.expertConds[objectId].Value : (TempFormula) null;
          if (cond2 == null && expertObject != null)
            cond2 = expertObject.Cond;
          if (!this.expertConds.ContainsKey(objectId))
            this.expertConds.GetOrAdd(objectId, new ExpertServer.CacheObject<TempFormula>(cond2));
          bool flag2 = false;
          List<long> addedObjs1 = ti.AddAdditionalObjs((IEnumerable<long>) moreObjIds);
          try
          {
            flag2 = cond2 != null && cond2.Count != 0 && !ti.CheckCond(objId, cond2, objectId, moreObjIds);
          }
          finally
          {
            ti.RemoveAdditionalObjs(addedObjs1);
          }
          if (flag2)
          {
            switch (node.opTag)
            {
              case ExpertScriptOp.opByFormula:
                if (!this.expertFormulae.ContainsKey(objectId))
                {
                  ExpertFormula expertFormula = (ExpertFormula) expertObject;
                  if (expertFormula != null)
                  {
                    ExpertServer.ExpertFormulaInfo expertFormulaInfo = new ExpertServer.ExpertFormulaInfo(expertFormula.GetTempFormula(), expertFormula.resAttrGuid, expertFormula.resObjTypeGuid);
                    this.expertFormulae.GetOrAdd(objectId, new ExpertServer.CacheObject<ExpertServer.ExpertFormulaInfo>(expertFormulaInfo));
                    break;
                  }
                  break;
                }
                break;
              case ExpertScriptOp.opByTable:
                if (!this.expertTables.ContainsKey(objectId))
                {
                  this.expertTables.GetOrAdd(objectId, new ExpertServer.CacheObject<eTableCollection>(tableCollection));
                  break;
                }
                break;
              case ExpertScriptOp.opByScript:
                if (!this.expertScripts.ContainsKey(objectId))
                {
                  ExpertScript expertScript = (ExpertScript) expertObject;
                  if (expertScript != null)
                  {
                    try
                    {
                      expertScript.UnpackXML();
                    }
                    catch (Exception ex)
                    {
                      throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_136")}", ex);
                    }
                    ScriptTreeNode scriptTreeNode = ExpertServer.LoadScriptTree(expertScript.xDoc);
                    this.expertScripts.GetOrAdd(objectId, new ExpertServer.CacheObject<ScriptTreeNode>(scriptTreeNode));
                    break;
                  }
                  break;
                }
                break;
            }
          }
          else
          {
            ti.objectFound = true;
            XmlNode node2 = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_133")) : (XmlNode) null;
            if (node2 != null)
            {
              ti.traceAddAttribute(node2, "_OBJ_ID_", Convert.ToString(objectId));
              if (expertObject != null)
                ti.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_134"), EnumTypeHelper.GetCaption((Enum) expertObject._objType));
              ti.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_135"), Convert.ToString(quickObjectInfo.ID));
            }
            bool flag3 = false;
            Dictionary<long, ESFolderInfo> foldersForEsObject = ESFolderKeeper.Keeper.GetAllFoldersForESObject(session, Math.Abs(objectId));
            if (foldersForEsObject != null && foldersForEsObject.Count > 0)
            {
              List<long> longList = new List<long>();
              foreach (KeyValuePair<long, ESFolderInfo> keyValuePair in foldersForEsObject)
              {
                TempFormula cond3 = keyValuePair.Value.Cond;
                if (cond3 == null || cond3.Count == 0)
                  longList.Add(keyValuePair.Key);
              }
              foreach (long key in longList)
                foldersForEsObject.Remove(key);
              if (foldersForEsObject.Count > 0)
              {
                string Name = string.Format(LocalizationHolder.rm.GetString("Expert.Server_280"), (object) foldersForEsObject.Count);
                XmlNode node3 = ti.traceAddElement(Name);
                XmlNode curNode2 = ti.curNode;
                ti.curNode = node3;
                try
                {
                  foreach (ESFolderInfo esFolderInfo in foldersForEsObject.Values)
                  {
                    if (!ti.CheckCond(objId, esFolderInfo.Cond, objectId))
                    {
                      flag3 = true;
                      if (node3 != null)
                      {
                        ti.traceAddAttribute(node3, LocalizationHolder.rm.GetString("Expert.Server_281"), esFolderInfo.Cond.ToString());
                        break;
                      }
                      break;
                    }
                  }
                }
                finally
                {
                  ti.curNode = curNode2;
                }
                if (flag3)
                  break;
              }
            }
            ExpertResult expertResult = ExpertResult.OK;
            try
            {
              switch (node.opTag)
              {
                case ExpertScriptOp.opByFormula:
                  ExpertFormula expertFormula = (ExpertFormula) expertObject;
                  ExpertServer.ExpertFormulaInfo expertFormulaInfo;
                  if (!this.expertFormulae.ContainsKey(objectId) && expertFormula != null)
                  {
                    expertFormulaInfo = new ExpertServer.ExpertFormulaInfo(expertFormula.GetTempFormula(), expertFormula.resAttrGuid, expertFormula.resObjTypeGuid);
                    this.expertFormulae.GetOrAdd(objectId, new ExpertServer.CacheObject<ExpertServer.ExpertFormulaInfo>(expertFormulaInfo));
                  }
                  else
                    expertFormulaInfo = this.expertFormulae[objectId].Value;
                  TempFormula tf = expertFormulaInfo.tf;
                  object result = (object) null;
                  List<long> longList = ExpertServer.ComposeContext(objId, (IEnumerable<long>) moreObjIds);
                  expertResult = ti.CalcFormula(longList.ToArray(), (HybridRowExp) null, tf, out result, 0L);
                  if (tf.DropMeasure && expertFormula != null && expertFormula.resAttrGUID != "" && result is double)
                  {
                    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(expertFormula.resAttrGUID));
                    if (attributeType != null)
                      result = (object) new MeasuredValue(Convert.ToDouble(result), attributeType.SizeType);
                  }
                  this.SetAttrValue(ti, session, objId, expertFormulaInfo.resAttrGuid, expertFormulaInfo.resObjTypeGuid, (HybridRowExp) null, result, (HybridTableExp) null, addObjIds: moreObjIds);
                  nullable = result == null ? new bool?() : new bool?(true);
                  break;
                case ExpertScriptOp.opByTable:
                  if (!this.expertTables.ContainsKey(objectId))
                    this.expertTables.GetOrAdd(objectId, new ExpertServer.CacheObject<eTableCollection>(tableCollection));
                  else
                    tableCollection = this.expertTables[objectId].Value;
                  ResultExpertValue[] Result = (ResultExpertValue[]) null;
                  List<long> addedObjs2 = ti.AddAdditionalObjs((IEnumerable<long>) moreObjIds);
                  try
                  {
                    expertResult = ExpertTableProcessor.CalcTable(ti, session, objId, tableCollection, objectId, out Result);
                    if (expertResult == ExpertResult.OK)
                    {
                      if (Result != null)
                      {
                        if (Result.Length != 0)
                          goto label_139;
                      }
                      throw new EAbort(ExpertResult.Unknown, LocalizationHolder.rm.GetString("Expert.Server_137"));
                    }
                  }
                  finally
                  {
                    ti.RemoveAdditionalObjs(addedObjs2);
                  }
label_139:
                  if (Result != null)
                  {
                    for (int index = 0; index < Result.Length; ++index)
                    {
                      object Val = (object) Result[index].Value;
                      if (Val.GetType() == typeof (ExpertValue))
                        Val = ((ExpertValue) Val).Value;
                      if (Val.GetType() == typeof (string))
                        this.PerformSquareBraces(ti, ref Val, objId);
                      this.SetAttrValue(ti, session, objId, Result[index].AttributeTypeGuid.ToString(), Result[index].ObjectTypeGuid.ToString(), (HybridRowExp) null, Val, (HybridTableExp) null);
                    }
                  }
                  nullable = !this._resultFound(ti, contObjId) ? new bool?() : new bool?(true);
                  break;
                case ExpertScriptOp.opByScript:
                  ExpertScript expertScript = (ExpertScript) expertObject;
                  ScriptTreeNode scriptTreeNode;
                  if (!this.expertScripts.ContainsKey(objectId))
                  {
                    if (expertScript != null)
                    {
                      try
                      {
                        expertScript.UnpackXML();
                      }
                      catch (Exception ex)
                      {
                        throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_136")}", ex);
                      }
                      scriptTreeNode = ExpertServer.LoadScriptTree(expertScript.xDoc);
                      this.expertScripts.GetOrAdd(objectId, new ExpertServer.CacheObject<ScriptTreeNode>(scriptTreeNode));
                      goto label_116;
                    }
                  }
                  scriptTreeNode = this.expertScripts[objectId].Value;
label_116:
                  ExpertServer.OldKey key = (ExpertServer.OldKey) null;
                  if (contObjId != objId)
                  {
                    TypedInfoItem itemData = ti.DataCache.GetItemData(objId, session);
                    key = new ExpertServer.OldKey(objId, (long) itemData.ItemTypeID);
                    if (ti.foundObjects.ContainsKey((object) key))
                      ti.foundObjects[(object) key] = (object) objId;
                    else
                      ti.foundObjects.Add((object) key, (object) objId);
                  }
                  List<long> addedObjs3 = ti.AddAdditionalObjs((IEnumerable<long>) moreObjIds);
                  ScriptTreeNode scriptRoot = ti.scriptRoot;
                  ti.scriptRoot = scriptTreeNode;
                  try
                  {
                    HybridTableExp dTable = (HybridTableExp) null;
                    this._SetParmValue(taskId, -1L, ExpertConsts.Consts.attrContextCount, (object) 1, false);
                    long[] new_context = (long[]) null;
                    for (int index = 0; index < scriptTreeNode.Items.Count; ++index)
                    {
                      long[] context = new long[1]{ objId };
                      this.ProcessScriptNode(taskId, (ScriptTreeNode) scriptTreeNode.Items[index], context, dTable, true, ref new_context);
                      if (ti.BreakFlag)
                      {
                        ti.BreakFlag = false;
                        break;
                      }
                    }
                  }
                  finally
                  {
                    if (key != null)
                      ti.foundObjects.Remove((object) key);
                    ti.RemoveAdditionalObjs(addedObjs3);
                    ti.scriptRoot = scriptRoot;
                  }
                  nullable = !this._resultFound(ti, contObjId) ? new bool?() : new bool?(true);
                  break;
              }
            }
            catch (EAbort ex)
            {
              expertResult = ex.res;
              nullable = new bool?();
            }
            if (ti.makeTrace && node2 != null)
              ti.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_138"), EnumTypeHelper.GetCaption((Enum) expertResult));
            switch (expertResult)
            {
              case ExpertResult.Unknown:
                if (!ti.objectFound)
                  break;
                goto case ExpertResult.NoCalcParms;
              case ExpertResult.NoCalcParms:
                nullable = new bool?();
                break;
            }
          }
          break;
      }
    }
    finally
    {
      this.RestoreCurNode(taskId, curNode1);
    }
    return nullable;
  }

  public static List<long> ComposeContext(long objId, IEnumerable<long> moreObjIDs)
  {
    List<long> longList = new List<long>() { objId };
    if (moreObjIDs != null)
    {
      foreach (long moreObjId in moreObjIDs)
      {
        if (!longList.Contains(moreObjId))
          longList.Add(moreObjId);
      }
    }
    return longList;
  }

  private bool _resultFound(ExpertServer.ExpServTask ti, long replacedContextId = 0)
  {
    lock (ti)
    {
      CalcAttrPair curCalcItem = ti.calcStack.curCalcItem;
      if (replacedContextId != 0L && replacedContextId != curCalcItem.objID)
      {
        CalcAttrPair key = new CalcAttrPair(replacedContextId, curCalcItem.objTypeID, curCalcItem.attrTypeID);
        if (ti.CalcAttrs.ContainsKey(key))
        {
          object obj = ti.CalcAttrs[key].Value;
          ti.__SetValue(curCalcItem, obj);
        }
      }
      if (ti.CalcAttrs.ContainsKey(curCalcItem))
        return true;
      if (curCalcItem.objTypeID != -1)
      {
        if (ti.CalcAttrs.ContainsAttr(curCalcItem.objID, -1, curCalcItem.attrTypeID))
        {
          ti.calcStack.curCalcItem.objTypeID = -1;
          return true;
        }
        if (ti.IsTempAttrWithoutObject(curCalcItem.attrTypeID))
        {
          if (ti.CalcAttrs.ContainsAttr(-1L, -1, curCalcItem.attrTypeID))
          {
            ti.calcStack.curCalcItem = new CalcAttrPair(-1L, ti.calcStack.curCalcItem.attrTypeID);
            return true;
          }
        }
      }
    }
    return false;
  }

  private void PurgeResult(ExpertServer.ExpServTask ti, long objId, int objTypeId, int attrTypeId)
  {
    ti.CalcAttrs.Remove(objId, objTypeId, attrTypeId);
    objTypeId = -1;
    ti.CalcAttrs.Remove(objId, objTypeId, attrTypeId);
    if (attrTypeId == -1 || !ti.IsTempAttrWithoutObject(attrTypeId))
      return;
    objId = -1L;
    ti.CalcAttrs.Remove(objId, objTypeId, attrTypeId);
  }

  public string OperStrList(
    int taskId,
    int objTypeId,
    int attrTypeId,
    int secondAttrTypeId,
    string Divider,
    long objId,
    HybridRowExp row)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    List<object> objectList = new List<object>();
    if (task.IsTempAttrWithoutObject(attrTypeId))
      objId = (long) (objTypeId = -1);
    object parmValue = this._GetParmValue(task, objId, objTypeId, attrTypeId);
    switch (parmValue)
    {
      case null:
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeId);
        bool flag = attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList;
        if (parmValue == null && row != null && !flag)
        {
          Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
          int indexByName = row.Columns.GetIndexByName(attributeTypeGuid.ToString());
          if (indexByName >= 0)
          {
            parmValue = row[indexByName];
            if (parmValue.NotNullOrDBNull())
              objectList.Add(parmValue);
          }
        }
        IUserSession session = task.GetSession();
        if (parmValue == null)
        {
          object[] valuesById = session.GetObject(objId).GetValuesByID(attrTypeId, false);
          if (valuesById != null)
            objectList.AddRange((IEnumerable<object>) valuesById);
        }
        if (((parmValue != null ? 0 : (row != null ? 1 : 0)) & (flag ? 1 : 0)) != 0)
        {
          int indexByName = row.Columns.GetIndexByName("cad00033-306c-11d8-b4e9-00304f19f545");
          if (indexByName >= 0 && row[indexByName].NotNullOrDBNull())
          {
            long int64 = Convert.ToInt64(row[indexByName]);
            IDBRelation relation = session.GetRelation(int64, false);
            if (relation != null)
            {
              object[] valuesById = relation.GetValuesByID(attrTypeId, false);
              if (valuesById != null)
                objectList.AddRange((IEnumerable<object>) valuesById);
            }
          }
        }
        if (attributeType.FieldType == FieldTypes.ftObjectLink)
        {
          for (int index1 = objectList.Count - 1; index1 >= 0; --index1)
          {
            long int64 = Convert.ToInt64(objectList[index1]);
            IDBObject dbObject = (IDBObject) null;
            if (int64 != 0L && int64 != -1L)
              dbObject = session.GetObject(int64, false);
            if (dbObject == null)
            {
              objectList.RemoveAt(index1);
            }
            else
            {
              string str = "";
              if (secondAttrTypeId != -1)
              {
                object[] valuesById = dbObject.GetValuesByID(secondAttrTypeId, false);
                if (valuesById != null)
                {
                  if (valuesById.Length == 1)
                  {
                    str = Convert.ToString(valuesById[0]);
                  }
                  else
                  {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("{ ");
                    for (int index2 = 0; index2 < valuesById.Length; ++index2)
                    {
                      stringBuilder.Append(Convert.ToString(valuesById[index2]));
                      if (index2 < valuesById.Length - 1)
                        stringBuilder.Append(Divider);
                    }
                    str = stringBuilder.ToString();
                  }
                }
              }
              else
                str = dbObject.Caption;
              objectList[index1] = (object) str;
            }
          }
        }
        StringBuilder stringBuilder1 = new StringBuilder();
        for (int index = 0; index < objectList.Count; ++index)
        {
          stringBuilder1.Append(objectList[index].ToString());
          if (index < objectList.Count - 1)
            stringBuilder1.Append(Divider);
        }
        return stringBuilder1.ToString();
      case PacketValue _:
        PacketValue packetValue = (PacketValue) parmValue;
        for (int index = 0; index < packetValue.Count; ++index)
          objectList.Add(packetValue[index].Value);
        goto case null;
      case ArrayHolder _:
        ArrayHolder arrayHolder = (ArrayHolder) parmValue;
        for (int x = 0; x < arrayHolder.Width; ++x)
          objectList.Add(arrayHolder[x, 0]);
        goto case null;
      default:
        objectList.Add(parmValue);
        goto case null;
    }
  }

  internal bool PerformSquareBraces(ExpertServer.ExpServTask ti, ref object Val, long objId)
  {
    if (Val.GetType() != typeof (string))
      return false;
    string str = Convert.ToString(Val);
    bool flag = false;
    IUserSession session = this.GetSession(ti);
    TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(objId, session);
    if (TaskDataCache.IsEmpty((TypedInfoItem) objData))
      return false;
    int objTypeId = objData.ObjTypeID;
    HybridRowExp hybridRowExp = ti.savedDataByObjId(objId);
    int startIndex1 = 0;
    while (startIndex1 < str.Length)
    {
      int startIndex2 = str.IndexOf("[", startIndex1);
      int num = startIndex2 >= 0 ? str.IndexOf("]", startIndex2) : -1;
      startIndex1 = num >= 0 ? num + 1 : str.Length;
      if (startIndex2 >= 0)
      {
        string key = str.Substring(startIndex2 + 1, num - startIndex2 - 1);
        if (this.attrAliases.ContainsKey(key))
        {
          ExpertServer.AttrInfo attrAlias = this.attrAliases[key];
          string newValue = (string) null;
          object parmValue = this._GetParmValue(ti, objId, objTypeId, attrAlias.attrId);
          if (parmValue != null)
            newValue = parmValue.ToString();
          if (newValue == null && hybridRowExp != null)
          {
            int index = -1;
            if (!attrAlias.guid.Equals(Guid.Empty))
              index = ti.savedData.Columns.GetIndexByName(attrAlias.guid.ToString());
            if (index < 0)
              index = ti.savedData.Columns.GetIndexByName(attrAlias.attrId.ToString());
            if (index >= 0)
              newValue = Convert.ToString(hybridRowExp[index]);
          }
          if (newValue == null)
          {
            IDBObject dbObject = session.GetObject(objId, false);
            if (dbObject != null && attrAlias.attrId > 0)
            {
              IDBAttribute attributeById = dbObject.GetAttributeByID(attrAlias.attrId);
              if (attributeById != null && attributeById.Value.NotNullOrDBNull())
                newValue = attributeById.AsString;
            }
          }
          if (newValue == null)
          {
            int quiet = (int) this.InnerCalculateQuiet(ti, session, objTypeId, attrAlias.attrId, objId, out parmValue);
            if (parmValue != null)
              newValue = parmValue.ToString();
          }
          if (newValue != null)
          {
            string oldValue = str.Substring(startIndex2, num - startIndex2 + 1);
            str = str.Replace(oldValue, newValue);
            startIndex1 = startIndex2;
            flag = true;
          }
        }
      }
    }
    if (flag)
      Val = (object) str;
    return flag;
  }

  internal ExpertResult CalculateAttr(
    int taskId,
    int objTypeId,
    int attrTypeId,
    long objId,
    ExpertServer.CalcStages stages,
    out object val,
    long contObjId = -1,
    long[] moreObjIDs = null)
  {
    val = (object) null;
    Guid empty = Guid.Empty;
    string str1 = "";
    Guid objTypeGuid = Guid.Empty;
    if (contObjId == -1L)
      contObjId = objId;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    ExpertServer.GetSessionGuid(task);
    IUserSession session = this.GetSession(task);
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeId);
    if (attributeType == null)
      return ExpertResult.Unknown;
    string name = attributeType.Name;
    Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
    ExpertServer.TempAttrStru tempAttrStru = task.GetTempAttrStru(attributeTypeGuid);
    if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
    {
      objId = -1L;
      contObjId = -1L;
    }
    if (objTypeId != -1)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeId);
      if (objectType != null)
      {
        str1 = objectType.ObjectTypeName;
        objTypeGuid = objectType.Guid;
      }
      else
      {
        IMSRelationType relationType = MetaDataHelper.GetRelationType(objTypeId);
        if (relationType != null)
        {
          str1 = relationType.Description;
          objTypeGuid = relationType.Guid;
        }
      }
    }
    XmlNode curNode = task.curNode;
    lock (task)
    {
      if (this.FlagIn(ExpertTraceFlags.TraceAttribSearch, task.traceFlags))
      {
        XmlNode xmlNode = task.makeTrace ? task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_139")) : (XmlNode) null;
        if (xmlNode != null)
        {
          task.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(contObjId));
          if (str1 != "")
            task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_140"), str1);
          string str2 = "";
          if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
            str2 = !tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithObject) ? " " + LocalizationHolder.rm.GetString("Expert.Server_271") : " " + LocalizationHolder.rm.GetString("Expert.Server_277");
          else if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithObject))
            str2 = " " + LocalizationHolder.rm.GetString("Expert.Server_276");
          task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_141"), name + str2);
          task.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_142"), Convert.ToString(objId));
          task.traceSetNode(xmlNode);
        }
      }
      if (task.calcStack.Contains(objId, objTypeId, attrTypeId))
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_143"));
      task.calcStack.Push(objId, objTypeId, attrTypeId);
    }
    try
    {
      IDBAttributable dbAttributable = (IDBAttributable) null;
      if ((stages & ExpertServer.CalcStages.FindObject) != (ExpertServer.CalcStages) 0 || (stages & ExpertServer.CalcStages.CheckObject) != (ExpertServer.CalcStages) 0)
      {
        TypedInfoItem itemData = task.DataCache.GetItemData(objId, session);
        if (itemData is TaskDataCache.RelDataItem)
        {
          long relationId = ((RelInfoItem) itemData).RelationID;
          IDBRelation relation = session.GetRelation(relationId, false);
          if (relation != null)
          {
            IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
            IDBObject objectById;
            if (attributeById == null)
            {
              objectById = session.GetObjectByID(relation.PartID, false);
            }
            else
            {
              long int64 = Convert.ToInt64(attributeById.Value);
              objectById = session.GetObject(int64, false);
            }
            if (objectById != null && ExpertServer.IsTypeDescendant(objTypeId, objectById.ObjectType))
              dbAttributable = (IDBAttributable) objectById;
          }
        }
      }
      if ((stages & ExpertServer.CalcStages.CheckObject) != (ExpertServer.CalcStages) 0)
      {
        XmlNode node = (XmlNode) null;
        if (this.FlagIn(ExpertTraceFlags.TraceAttribSearch, task.traceFlags))
          node = task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_144"));
        if (dbAttributable == null)
          dbAttributable = this.GetSuitableObject(session, objId, objTypeId, attrTypeId);
        if (dbAttributable == null && moreObjIDs != null)
        {
          foreach (long moreObjId in moreObjIDs)
          {
            if (moreObjId != objId)
            {
              dbAttributable = this.GetSuitableObject(session, moreObjId, objTypeId, attrTypeId);
              if (dbAttributable != null)
                break;
            }
          }
        }
        if (dbAttributable == null && this.FlagIn(ExpertTraceFlags.TraceAttribSearch, task.traceFlags) && node != null)
          task.traceAddText(node, LocalizationHolder.rm.GetString("Expert.Server_145"));
      }
      if (dbAttributable == null && (stages & ExpertServer.CalcStages.FindObject) != (ExpertServer.CalcStages) 0)
      {
        dbAttributable = this.FindObjectWithAttr(taskId, session, objId, attributeType.AttributeID, objTypeId);
        if (dbAttributable == null && moreObjIDs != null)
        {
          foreach (long moreObjId in moreObjIDs)
          {
            dbAttributable = this.FindObjectWithAttr(taskId, session, moreObjId, attributeType.AttributeID, objTypeId);
            if (dbAttributable != null)
              break;
          }
        }
      }
      if (dbAttributable != null)
      {
        object[] valuesByGuid = dbAttributable.GetValuesByGuid(attributeType.AttributeGuid, false);
        if (valuesByGuid != null)
        {
          if (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
          {
            DataType valueType = attributeType.FieldType == FieldTypes.ftSystem ? DataTypeConvertor.AttrType2DataType(attributeType.FieldType) : DataTypeConvertor.AttrType2DataType(ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeType.AttributeID));
            PacketValue packetValue = new PacketValue();
            for (int index = 0; index < valuesByGuid.Length; ++index)
              packetValue.Add(new ExpertValue(valueType, valuesByGuid[index]));
            val = (object) packetValue;
          }
          else
            val = valuesByGuid[0];
          if (val.NotNullOrDBNull())
            return ExpertResult.OK;
        }
      }
      return (stages & ExpertServer.CalcStages.CalcAttribute) != (ExpertServer.CalcStages) 0 ? ExpertServer.es.InnerCalculate(taskId, session, objTypeId, attrTypeId, objTypeGuid, attributeType.AttributeGuid, objId, out val, contObjId, moreObjIDs) : ExpertResult.Unknown;
    }
    finally
    {
      lock (task)
      {
        task.calcStack.Pop();
        task.traceSetNode(curNode);
      }
    }
  }

  private IDBAttributable GetSuitableObject(
    IUserSession ius,
    long objId,
    int objTypeId,
    int attrTypeId)
  {
    bool Relation = false;
    IDBAttributable attributable = ExpertServer.GetAttributable(ius, objId, out Relation);
    return attributable != null && (objTypeId == -1 || ExpertServer.IsTypeDescendant(objTypeId, attributable.TypeID)) && (attrTypeId < 0 || attributable.GetAttributeByID(attrTypeId) != null) ? attributable : (IDBAttributable) null;
  }

  private bool IsObject(ScriptTreeNode root, Guid objGUID)
  {
    if ((root.opTag == ExpertScriptOp.opByFormula || root.opTag == ExpertScriptOp.opByScript || root.opTag == ExpertScriptOp.opByTable) && objGUID.Equals(new Guid((root.op as OpParmExpObj).objTypeGUID)))
      return true;
    for (int index = 0; index < root.Items.Count; ++index)
    {
      if (this.IsObject((ScriptTreeNode) root.Items[index], objGUID))
        return true;
    }
    return false;
  }

  public List<long> GetAttrRulesForObject(Guid sessionGuid, long expertObjId)
  {
    IUserSession session = ExpertServer._CheckGetSession(sessionGuid);
    IDBObject eO = session.GetObject(expertObjId, false);
    if (eO == null || !(eO is ExpertObject))
      return (List<long>) null;
    List<GuidPair> resultPairs = this.GetResultPairs((ExpertObject) eO);
    string lower = eO.ObjectGUID.ToString().ToLower();
    List<long> attrRulesForObject = new List<long>();
    for (int index = 0; index < resultPairs.Count; ++index)
    {
      GuidPair guidPair = resultPairs[index];
      long attrRule = this.GetAttrRule(session, guidPair.objTypeGUID, guidPair.attrGUID);
      bool flag = false;
      if (attrRule != -1L)
      {
        ExpertRules expertRules = (ExpertRules) session.GetObject(attrRule);
        expertRules.Load();
        XMLScripter.LoadScript(expertRules.Script);
        if (expertRules.xDoc.DocumentElement != null && expertRules.xDoc.DocumentElement.HasChildNodes)
        {
          foreach (XmlNode childNode in expertRules.xDoc.DocumentElement.ChildNodes)
            flag = flag || this.ContainsGuid(childNode, lower);
        }
        if (flag)
          attrRulesForObject.Add(expertRules.ObjectID);
      }
    }
    return attrRulesForObject;
  }

  private bool ContainsGuid(XmlNode node, string expertObjGuid)
  {
    if (node.NodeType == XmlNodeType.Element && node.Name == "Op-Parms")
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode.Name == "GUID" && childNode.InnerText == expertObjGuid)
          return true;
      }
    }
    if (node.HasChildNodes)
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (this.ContainsGuid(childNode, expertObjGuid))
          return true;
      }
    }
    return false;
  }

  public List<GuidPair> GetResultPairs(ExpertObject eO)
  {
    List<GuidPair> resultPairs = new List<GuidPair>();
    switch (eO.ObjType)
    {
      case ExpertObjType.Unknown:
        return (List<GuidPair>) null;
      case ExpertObjType.Formula:
        if (!(eO is ExpertFormula expertFormula))
          return (List<GuidPair>) null;
        expertFormula.Load();
        resultPairs.Add(new GuidPair(expertFormula.resAttrGUID, expertFormula.resObjTypeGUID));
        break;
      case ExpertObjType.Table:
        if (!(eO is ExpertTable expertTable))
          return (List<GuidPair>) null;
        expertTable.Load();
        for (int index = 0; index < expertTable.Roles.Count; ++index)
        {
          switch ((AttributeRoles) expertTable.Roles[index])
          {
            case AttributeRoles.argResult:
            case AttributeRoles.Result:
              resultPairs.Add(new GuidPair(expertTable.AttributesList[index].ToString(), expertTable.ObjectTypesList[index].ToString()));
              break;
          }
        }
        break;
      case ExpertObjType.Script:
        if (!(eO is ExpertScriptable expertScriptable))
          return (List<GuidPair>) null;
        expertScriptable.Load();
        for (int index = 0; index < expertScriptable.AttrRoles.Length && index < expertScriptable.attrGUIDs.Length; ++index)
        {
          if (expertScriptable.AttrRoles[index] == AttributeRoles.argResult)
            resultPairs.Add(new GuidPair(expertScriptable.attrGUIDs[index], expertScriptable.objTypeGUIDs[index]));
        }
        break;
    }
    return resultPairs;
  }

  internal bool _CreateExpertFormula(
    Guid sessionGuid,
    string resObjTypeGuid,
    string resAttrTypeGuid,
    TempFormula tf,
    TempFormula cond)
  {
    IExpertFormula expertFormula = (IExpertFormula) ExpertServer._CheckGetSession(sessionGuid).GetObjectCollection(ExpertConsts.Consts.objFormula).Create();
    int att = -1;
    int num = -1;
    string str1 = "";
    string str2 = "";
    if (resObjTypeGuid != "")
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(new Guid(resObjTypeGuid));
      if (objectType != null)
      {
        num = objectType.ObjectTypeID;
        str1 = objectType.ObjectTypeName;
      }
    }
    if (resAttrTypeGuid != "")
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(resAttrTypeGuid));
      if (attributeType != null)
      {
        att = attributeType.AttributeID;
        str2 = attributeType.Name;
      }
    }
    AttribPair attribPair = new AttribPair(att, num);
    expertFormula.Result = attribPair;
    expertFormula.resAttrGuid = resAttrTypeGuid;
    expertFormula.resObjTypeGuid = resObjTypeGuid;
    if (cond != null)
      expertFormula.Cond = cond;
    string str3 = LocalizationHolder.rm.GetString("Expert.Server_198") + " ";
    if (str1 != "")
      str3 = $"{str3}<{str1}>.";
    string str4 = $"{str3}<{str2}>";
    expertFormula.Name = str4;
    expertFormula.UpdateObject(tf);
    expertFormula.CommitCreation(true);
    byte[] traceInfo = (byte[]) null;
    return this._ReflectObjUpdate(sessionGuid, expertFormula.ObjectID, ExpertTraceFlags.None, (TempFormula) null, out traceInfo);
  }

  internal bool _ReflectObjUpdate(
    Guid sessionGuid,
    long objId,
    ExpertTraceFlags traceFlags,
    TempFormula branchCond,
    out byte[] traceInfo)
  {
    this.StartSystemTask(sessionGuid, traceFlags);
    try
    {
      IUserSession session = ExpertServer._CheckGetSession(sessionGuid);
      IDBObject dbObject = session.GetObject(objId);
      if (dbObject == null)
        throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_146"), (object) objId));
      if (!(dbObject is ExpertObject eO))
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_147"));
      this.servTask.InitTraceInfo();
      XmlNode element = (XmlNode) this.servTask.traceInfo.CreateElement(LocalizationHolder.rm.GetString("Expert.Server_148"), ExpertServer.ExpertNamespace);
      XmlAttribute attribute = this.servTask.traceInfo.CreateAttribute(nameof (objId));
      attribute.Value = Convert.ToString(objId);
      element.Attributes.Append(attribute);
      this.servTask.curNode.AppendChild(element);
      this.servTask.curNode = element;
      traceInfo = (byte[]) null;
      List<GuidPair> resultPairs = this.GetResultPairs(eO);
      bool flag = false;
      for (int index = 0; index < resultPairs.Count; ++index)
      {
        GuidPair gp = resultPairs[index];
        AttribPair attribPair = new AttribPair(0, 0);
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(gp.attrGUID);
        if (attributeType != null)
        {
          attribPair.attribID = attributeType.AttributeID;
          if (!this.idents.ContainsKey(gp.attrGUID))
            this.idents.GetOrAdd(gp.attrGUID, (long) attributeType.AttributeID);
          string name = attributeType.Name;
          IMSObjectType objectType = MetaDataHelper.GetObjectType(gp.objTypeGUID);
          string objTypeName;
          if (objectType == null)
          {
            attribPair.objTypeID = -1;
            objTypeName = "";
          }
          else
          {
            attribPair.objTypeID = objectType.ObjectTypeID;
            if (!this.idents.ContainsKey(gp.objTypeGUID))
              this.idents.GetOrAdd(gp.objTypeGUID, (long) objectType.ObjectTypeID);
            objTypeName = objectType.ObjectTypeName;
          }
          XmlNode node1 = this.servTask.makeTrace ? this.servTask.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_64")) : (XmlNode) null;
          if (this.servTask.makeTrace)
            this.servTask.traceAddAttribute(element, "_OBJ_ID_", Convert.ToString(objId));
          if (node1 != null)
          {
            this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_207"), Convert.ToString(attribPair.objTypeID));
            this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_212"), Convert.ToString(attribPair.attribID));
            this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_65"), objTypeName);
            this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_66"), name);
            this.servTask.curNode = node1;
          }
          try
          {
            if (this.servTask.makeTrace)
              this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_213"), LocalizationHolder.rm.GetString("Expert.Server_33"));
            long attrRule = this.GetAttrRule(session, gp.objTypeGUID, gp.attrGUID);
            if (attrRule != -1L)
            {
              ExpertRules er = (ExpertRules) session.GetObject(attrRule);
              if (this.servTask.makeTrace)
                this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_214"), Convert.ToString(attrRule));
              er.Load();
              ScriptTreeNode scriptTreeNode = XMLScripter.LoadScript(er.Script);
              flag = this.AddScriptNode(scriptTreeNode, er, eO, branchCond, this.servTask);
              if (flag)
              {
                if (this.attrRules.ContainsKey(attribPair) && this.servTask.makeTrace)
                  this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_213"), LocalizationHolder.rm.GetString("Expert.Server_32"));
                this.SetValueToCache<AttribPair, ScriptTreeNode>(attribPair, scriptTreeNode, this.attrRules);
              }
            }
            else
            {
              XmlNode node2 = this.servTask.makeTrace ? this.servTask.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_67")) : (XmlNode) null;
              ExpertRules newRule = (ExpertRules) this.CreateNewRule(session, attribPair, gp, eO, name, objTypeName);
              if (node2 != null)
                this.servTask.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_135"), Convert.ToString(newRule.ID));
              flag = true;
              newRule.Load();
              ScriptTreeNode val = XMLScripter.LoadScript(newRule.Script);
              if (this.attrRules.ContainsKey(attribPair) && this.servTask.makeTrace)
                this.servTask.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_213"), LocalizationHolder.rm.GetString("Expert.Server_32"));
              this.SetValueToCache<AttribPair, ScriptTreeNode>(attribPair, val, this.attrRules);
            }
            ((IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer)))?.AddEvent(ExpServerCache.cacheAttrRules, (long) attribPair.attribID, (long) attribPair.objTypeID, ((UserSession) session).DataManager);
          }
          catch (Exception ex)
          {
            if (this.servTask.makeTrace)
            {
              XmlNode xmlNode = this.servTask.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_72"));
              if (xmlNode != null)
                xmlNode.InnerText = ex.Message;
            }
          }
        }
      }
      this.servTask.curNode = this.servTask.curNode.ParentNode;
      traceInfo = this.servTask.GetPackedInfo();
      return flag;
    }
    catch (Exception ex)
    {
      if (this.servTask.makeTrace)
      {
        XmlNode xmlNode = this.servTask.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_73"));
        if (xmlNode != null)
          xmlNode.InnerText = ex.Message;
      }
    }
    finally
    {
      this.EndSystemTask();
    }
    traceInfo = (byte[]) null;
    return false;
  }

  protected bool AddScriptNode(
    ScriptTreeNode root,
    ExpertRules er,
    ExpertObject eO,
    TempFormula branchCond,
    ExpertServer.ExpServTask servTask)
  {
    if (!this.IsObject(root, eO.ObjectGUID))
    {
      XmlNode node = servTask.makeTrace ? servTask.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_68")) : (XmlNode) null;
      if (branchCond != null)
      {
        bool flag = false;
        foreach (ScriptTreeNode scriptTreeNode in root.Items)
        {
          if (scriptTreeNode.opTag == ExpertScriptOp.opFolder && (scriptTreeNode.op as OpParmCond).cond != null && branchCond.Equals((object) (scriptTreeNode.op as OpParmCond).cond))
          {
            flag = true;
            root = scriptTreeNode;
            break;
          }
        }
        if (!flag)
        {
          ScriptTreeNode scriptTreeNode = new ScriptTreeNode(ExpertScriptMod.modUnknown, ExpertScriptOp.opFolder, branchCond.ToString());
          ((OpParmCond) scriptTreeNode.op).cond = (TempFormula) branchCond.Clone();
          root.Items.Add((object) scriptTreeNode);
          root = scriptTreeNode;
        }
      }
      ExpertScriptOp opTag = ExpertScriptOp.opUnknown;
      switch (eO.ObjType)
      {
        case ExpertObjType.Formula:
          opTag = ExpertScriptOp.opByFormula;
          break;
        case ExpertObjType.Table:
          opTag = ExpertScriptOp.opByTable;
          break;
        case ExpertObjType.Script:
          opTag = ExpertScriptOp.opByScript;
          break;
      }
      ScriptTreeNode scriptTreeNode1 = new ScriptTreeNode(ExpertScriptMod.modUnknown, opTag, "");
      OpParmExpObj op = (OpParmExpObj) scriptTreeNode1.op;
      op.objTypeGUID = eO.ObjectGUID.ToString();
      op.objTypeText = eO.ObjectName;
      op.objCond = eO.Cond == null ? (TempFormula) null : (TempFormula) eO.Cond.Clone();
      root.Items.Add((object) scriptTreeNode1);
      byte[] buffer = ScriptTreeNode.SaveToBuffer(root);
      er.UpdateObject(buffer, er.Name);
      if (node != null)
        servTask.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_69"), LocalizationHolder.rm.GetString("Expert.Server_70"));
      return true;
    }
    if (servTask.makeTrace)
      servTask.curNode.InnerText = LocalizationHolder.rm.GetString("Expert.Server_71");
    return false;
  }

  public long GetAttrRule(IUserSession ius, Guid objTypeGUID, Guid attrTypeGUID)
  {
    DataTable dataTable = ius.GetObjectCollection(ExpertConsts.Consts.objAttrRules).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) objTypeGUID.ToString(), (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
      new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) attrTypeGUID.ToString(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    }));
    return dataTable != null && dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : -1L;
  }

  internal ScriptTreeNode LoadAttrRule(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    Guid objTypeGUID,
    Guid attrTypeGUID,
    int objTypeId,
    int attrTypeId)
  {
    XmlNode node = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_74")) : (XmlNode) null;
    if (node != null)
      ti.traceAddText(node, string.Format(LocalizationHolder.rm.GetString("Expert.Server_202"), (object) objTypeId, (object) attrTypeId) + ": ");
    AttribPair key1 = new AttribPair(attrTypeId, objTypeId);
    if (this.attrRules.ContainsKey(key1))
    {
      ScriptTreeNode valueFromCache = this.GetValueFromCache<AttribPair, ScriptTreeNode>(key1, this.attrRules);
      if (node != null)
      {
        if (valueFromCache != null)
          ti.traceAddText(node, LocalizationHolder.rm.GetString("Expert.Server_75"));
        else
          ti.traceAddText(node, LocalizationHolder.rm.GetString("Expert.Server_77"));
      }
      return valueFromCache;
    }
    long attrRule = this.GetAttrRule(ius, objTypeGUID, attrTypeGUID);
    if (attrRule == -1L)
    {
      foreach (Guid guid in MetaDataHelper.GetObjectTypeParentsGuid(objTypeGUID))
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(guid);
        AttribPair key2 = new AttribPair(attrTypeId, objectTypeId);
        if (this.attrRules.ContainsKey(key2))
        {
          ScriptTreeNode valueFromCache = this.GetValueFromCache<AttribPair, ScriptTreeNode>(key2, this.attrRules);
          if (node != null)
          {
            if (valueFromCache != null)
              ti.traceAddText(node, LocalizationHolder.rm.GetString("Expert.Server_75"));
            else
              ti.traceAddText(node, LocalizationHolder.rm.GetString("Expert.Server_77"));
          }
          if (valueFromCache != null)
            return valueFromCache;
        }
        else
        {
          attrRule = this.GetAttrRule(ius, guid, attrTypeGUID);
          if (attrRule != -1L)
          {
            key1 = key2;
            break;
          }
        }
      }
    }
    if (attrRule != -1L)
    {
      ScriptTreeNode val = this._LoadAttrRule(ius, attrRule);
      if (val != null)
      {
        this.SetValueToCache<AttribPair, ScriptTreeNode>(key1, val, this.attrRules);
        return val;
      }
    }
    else
      this.SetValueToCache<AttribPair, ScriptTreeNode>(key1, (ScriptTreeNode) null, this.attrRules);
    if (node != null)
      ti.traceAddText(node, LocalizationHolder.rm.GetString("Expert.Server_77"));
    return (ScriptTreeNode) null;
  }

  internal ScriptTreeNode LoadAttrRuleQuiet(
    IUserSession ius,
    Guid objTypeGUID,
    Guid attrTypeGUID,
    int objTypeId,
    int attrTypeId)
  {
    AttribPair key1 = new AttribPair(attrTypeId, objTypeId);
    if (this.attrRules.ContainsKey(key1))
      return this.GetValueFromCache<AttribPair, ScriptTreeNode>(key1, this.attrRules);
    long attrRule = this.GetAttrRule(ius, objTypeGUID, attrTypeGUID);
    if (attrRule == -1L)
    {
      foreach (Guid guid in MetaDataHelper.GetObjectTypeParentsGuid(objTypeGUID))
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(guid);
        AttribPair key2 = new AttribPair(attrTypeId, objectTypeId);
        if (this.attrRules.ContainsKey(key2))
        {
          ScriptTreeNode valueFromCache = this.GetValueFromCache<AttribPair, ScriptTreeNode>(key2, this.attrRules);
          if (valueFromCache != null)
            return valueFromCache;
        }
        else
        {
          attrRule = this.GetAttrRule(ius, guid, attrTypeGUID);
          if (attrRule != -1L)
          {
            key1 = key2;
            break;
          }
        }
      }
    }
    if (attrRule != -1L)
    {
      ScriptTreeNode val = this._LoadAttrRule(ius, attrRule);
      if (val != null)
      {
        this.SetValueToCache<AttribPair, ScriptTreeNode>(key1, val, this.attrRules);
        return val;
      }
    }
    else
      this.SetValueToCache<AttribPair, ScriptTreeNode>(key1, (ScriptTreeNode) null, this.attrRules);
    return (ScriptTreeNode) null;
  }

  internal bool HasAttrRule(IUserSession ius, int objTypeId, int attrTypeId)
  {
    AttribPair key1 = new AttribPair(attrTypeId, objTypeId);
    if (this.attrRules.ContainsKey(key1))
      return this.GetValueFromCache<AttribPair, ScriptTreeNode>(key1, this.attrRules) != null;
    if (key1.objTypeID != -1)
    {
      AttribPair key2 = new AttribPair(attrTypeId, -1);
      if (this.attrRules.ContainsKey(key2))
        return this.GetValueFromCache<AttribPair, ScriptTreeNode>(key2, this.attrRules) != null;
    }
    Guid objTypeGUID = Guid.Empty;
    if (key1.objTypeID != -1)
      objTypeGUID = MetaDataHelper.GetObjectTypeGuid(objTypeId);
    Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
    long attrRule1 = this.GetAttrRule(ius, objTypeGUID, attributeTypeGuid);
    if (attrRule1 != -1L)
    {
      ScriptTreeNode val = this._LoadAttrRule(ius, attrRule1);
      if (val == null)
        return false;
      this.SetValueToCache<AttribPair, ScriptTreeNode>(key1, val, this.attrRules);
      return true;
    }
    long attrRule2 = this.GetAttrRule(ius, Guid.Empty, attributeTypeGuid);
    if (attrRule2 == -1L)
      return false;
    ScriptTreeNode val1 = this._LoadAttrRule(ius, attrRule2);
    if (val1 == null)
      return false;
    this.SetValueToCache<AttribPair, ScriptTreeNode>(new AttribPair(attrTypeId, -1), val1, this.attrRules);
    return true;
  }

  internal long GetObjRule(IUserSession ius, Guid objTypeGUID, Guid attrTypeGUID)
  {
    IDBObjectCollection objectCollection = ius.GetObjectCollection(ExpertConsts.Consts.objObjRules);
    ConditionStructure[] conditions;
    if (!attrTypeGUID.Equals(Guid.Empty))
      conditions = new ConditionStructure[2]
      {
        new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) objTypeGUID.ToString(), (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
        new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) attrTypeGUID.ToString(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
      };
    else
      conditions = new ConditionStructure[1]
      {
        new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) objTypeGUID.ToString(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
      };
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns);
    DataTable dataTable = objectCollection.Select(paramSet);
    return dataTable != null && dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : -1L;
  }

  internal long GetRecalcScript(IUserSession ius, Guid objTypeGUID, Guid attrTypeGUID)
  {
    DataTable dataTable = ius.GetObjectCollection(ExpertConsts.Consts.objRecalcScript).Select(new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(ExpertConsts.Consts.attrResObjTypeGUID, RelationalOperators.Equal, (object) objTypeGUID.ToString(), (object) 0, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object, ColumnContents.Text),
      new ConditionStructure(ExpertConsts.Consts.attrResAttrGUID, RelationalOperators.Equal, (object) attrTypeGUID.ToString(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
    }));
    return dataTable != null && dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : -1L;
  }

  internal ScriptTreeNode LoadObjRule(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    Guid objTypeGUID,
    Guid attrTypeGUID,
    int objTypeId,
    int attrTypeId)
  {
    AttribPair key = new AttribPair(attrTypeId, objTypeId);
    if (this.objRules.ContainsKey(key))
      return this.GetValueFromCache<AttribPair, ScriptTreeNode>(key, this.objRules);
    long objRule1 = this.GetObjRule(ius, objTypeGUID, attrTypeGUID);
    if (objRule1 != -1L)
    {
      ScriptTreeNode val = this._LoadObjRule(ius, objRule1);
      if (val != null)
      {
        this.SetValueToCache<AttribPair, ScriptTreeNode>(key, val, this.objRules);
        return val;
      }
    }
    key.attribID = -1;
    if (this.objRules.ContainsKey(key))
      return this.GetValueFromCache<AttribPair, ScriptTreeNode>(key, this.objRules);
    long objRule2 = this.GetObjRule(ius, objTypeGUID, attrTypeGUID);
    if (objRule2 != -1L)
    {
      ScriptTreeNode val = this._LoadObjRule(ius, objRule2);
      if (val != null)
      {
        this.SetValueToCache<AttribPair, ScriptTreeNode>(key, val, this.objRules);
        return val;
      }
    }
    return (ScriptTreeNode) null;
  }

  internal ScriptTreeNode LoadRecalcScript(
    ExpertServer.ExpServTask ti,
    IUserSession ius,
    Guid objTypeGUID,
    Guid attrTypeGUID,
    int objTypeId,
    int attrTypeId)
  {
    AttribPair key = new AttribPair(attrTypeId, objTypeId);
    while (!this.recalcScripts.ContainsKey(key))
    {
      long recalcScript = this.GetRecalcScript(ius, objTypeGUID, attrTypeGUID);
      if (recalcScript != -1L)
      {
        ScriptTreeNode val = this._LoadRecalcScript(ius, recalcScript);
        if (val != null)
        {
          this.SetValueToCache<AttribPair, ScriptTreeNode>(key, val, this.recalcScripts);
          return val;
        }
      }
      if (key.objTypeID != -1)
      {
        key.objTypeID = MetaDataHelper.GetObjectTypeParentID(key.objTypeID);
        if (key.objTypeID != -1)
          continue;
      }
      return (ScriptTreeNode) null;
    }
    return this.GetValueFromCache<AttribPair, ScriptTreeNode>(key, this.recalcScripts);
  }

  internal ExpertAttrRules CreateNewRule(
    IUserSession ius,
    AttribPair ap,
    GuidPair gp,
    ExpertObject eO,
    string attrTypeName,
    string objTypeName)
  {
    ExpertAttrRules newRule = (ExpertAttrRules) ius.GetObjectCollection(ExpertConsts.Consts.objAttrRules).Create();
    newRule.Result = ap;
    newRule.resAttrGuid = gp.attrGUID.ToString();
    newRule.resObjTypeGuid = gp.objTypeGUID.ToString();
    string str = "";
    if (objTypeName != "")
      str = $"<{objTypeName}>.";
    string Name = $"{str}<{attrTypeName}>";
    ExpertScriptOp opTag = ExpertScriptOp.opUnknown;
    switch (eO.ObjType)
    {
      case ExpertObjType.Formula:
        opTag = ExpertScriptOp.opByFormula;
        break;
      case ExpertObjType.Table:
        opTag = ExpertScriptOp.opByTable;
        break;
      case ExpertObjType.Script:
        opTag = ExpertScriptOp.opByScript;
        break;
    }
    ScriptTreeNode scriptTreeNode = new ScriptTreeNode(ExpertScriptMod.modUnknown, opTag, "");
    OpParmExpObj op = (OpParmExpObj) scriptTreeNode.op;
    op.objTypeGUID = eO.ObjectGUID.ToString();
    op.objTypeText = eO.ObjectName;
    byte[] buffer = ScriptTreeNode.SaveToBuffer(new ScriptTreeNode[1]
    {
      scriptTreeNode
    });
    newRule.UpdateObject(buffer, Name);
    newRule.CommitCreation(true);
    return newRule;
  }

  internal static bool DeleteRuleNode(ScriptTreeNode root, string objGuid)
  {
    bool flag = false;
    if (root.Items != null)
    {
      int index = 0;
      while (index < root.Items.Count)
      {
        ScriptTreeNode root1 = (ScriptTreeNode) root.Items[index];
        if (root1.opTag == ExpertScriptOp.opByFormula || root1.opTag == ExpertScriptOp.opByScript || root1.opTag == ExpertScriptOp.opByTable)
        {
          if (((OpParmExpObj) root1.op).objTypeGUID == objGuid)
          {
            root.Items.Remove((object) root1);
            flag = true;
          }
          else
          {
            flag = flag || ExpertServer.DeleteRuleNode(root1, objGuid);
            ++index;
          }
        }
        else
        {
          flag = flag || ExpertServer.DeleteRuleNode(root1, objGuid);
          ++index;
        }
      }
    }
    return flag;
  }

  public static void DeleteLinks(ExpertRules er, Guid objGUID)
  {
    er.Load();
    ScriptTreeNode scriptTreeNode = XMLScripter.LoadScript(er.Script);
    if (!ExpertServer.DeleteRuleNode(scriptTreeNode, objGUID.ToString()))
      return;
    byte[] buffer = ScriptTreeNode.SaveToBuffer(scriptTreeNode);
    er.UpdateObject(buffer, er.Name);
  }

  private void ShowContext(int taskId, long[] context, bool RetName)
  {
    ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
    try
    {
      if (!ti.makeTrace)
        return;
      XmlNode node1 = ti.traceAddElement(RetName ? LocalizationHolder.rm.GetString("Expert.Server_133") : LocalizationHolder.rm.GetString("Expert.Server_215"));
      if (node1 == null)
        return;
      IUserSession session = this.GetSession(ti);
      ti.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_216"), Convert.ToString(context.Length));
      if (ti.blockTrace != 0)
        return;
      for (int index = 0; index < context.Length; ++index)
      {
        XmlNode node2 = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_46"));
        ti.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_135"), Convert.ToString(context[index]));
        TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(context[index], session);
        if ((TypedInfoItem) objData != (TypedInfoItem) null)
          ti.traceAddAttribute(node2, LocalizationHolder.rm.GetString("Expert.Server_217"), objData.Caption);
      }
    }
    finally
    {
      this.EndModifyTrace(ti);
    }
  }

  private eTableCollection ShowLoadObject(int taskId, ExpertObject obj)
  {
    eTableCollection eTableCollection = (eTableCollection) null;
    ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
    try
    {
      XmlNode node = ti.makeTrace ? ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_218")) : (XmlNode) null;
      if (node != null)
      {
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_135"), Convert.ToString(obj.ObjectID));
        ti.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_81"), obj.Caption);
      }
      string Text = (string) null;
      try
      {
        obj.Load();
        if (obj.GetType() == typeof (ExpertTable))
          eTableCollection = ((ExpertTable) obj).LoadTableData();
        if (ti.makeTrace)
          Text = LocalizationHolder.rm.GetString("Expert.Server_78");
      }
      catch (Exception ex)
      {
        Text = ex.Message;
      }
      if (Text != null)
        ti.traceAddText(node, Text);
    }
    finally
    {
      this.EndModifyTrace(ti);
    }
    return eTableCollection;
  }

  private void ReportScriptNode(
    int taskId,
    ScriptTreeNode node,
    long[] context,
    bool needShowCont,
    out XmlNode curNode)
  {
    curNode = (XmlNode) null;
    ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
    try
    {
      if (!ti.makeTrace || ti.rootExclaimed && !node.ExclamationMarked || node.label.StartsWith("&&"))
        return;
      ti.lastInfoStr = $"{LocalizationHolder.rm.GetString("Expert.Server_42")}{node.label}\" ({Intermech.Expert.NodeData.GetShortMod((int) node.modTag, node.mod)} : {Intermech.Expert.NodeData.GetShortOp((int) node.opTag, node.op)})";
      if (!this.FlagIn(ExpertTraceFlags.TraceScripts, ti.traceFlags))
        return;
      XmlNode xmlNode1 = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_219"));
      ti.traceAddAttribute(xmlNode1, LocalizationHolder.rm.GetString("Expert.Server_220"), node.label);
      ti.traceAddAttribute(xmlNode1, LocalizationHolder.rm.GetString("Expert.Server_221"), Intermech.Expert.NodeData.GetShortMod((int) node.modTag, node.mod));
      ti.traceAddAttribute(xmlNode1, LocalizationHolder.rm.GetString("Expert.Server_222"), Intermech.Expert.NodeData.GetShortOp((int) node.opTag, node.op));
      if (needShowCont && this.FlagIn(ExpertTraceFlags.ShowContext, ti.traceFlags) && ti.blockTrace == 0)
      {
        IUserSession session = this.GetSession(ti);
        XmlNode xmlNode2 = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_215"));
        if (context.Length == 1)
        {
          ti.traceAddAttribute(xmlNode2, LocalizationHolder.rm.GetString("Expert.Server_216"), "1");
          TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(context[0], session);
          if ((TypedInfoItem) objData != (TypedInfoItem) null)
          {
            ti.traceAddAttribute(xmlNode2, LocalizationHolder.rm.GetString("Expert.Server_135"), Convert.ToString(context[0]));
            ti.traceAddAttribute(xmlNode2, LocalizationHolder.rm.GetString("Expert.Server_217"), objData.Caption);
          }
          else if ((TypedInfoItem) ti.DataCache.GetRelData(context[0], session) != (TypedInfoItem) null)
            ti.traceAddAttribute(xmlNode2, LocalizationHolder.rm.GetString("Expert.Server_279"), Convert.ToString(context[0]));
          else
            ti.traceAddAttribute(xmlNode2, LocalizationHolder.rm.GetString("Expert.Server_122"), Convert.ToString(context[0]));
        }
        else
        {
          ti.traceAddAttribute(xmlNode2, LocalizationHolder.rm.GetString("Expert.Server_216"), Convert.ToString(context.Length));
          for (int index = 0; index < context.Length; ++index)
          {
            XmlNode node1 = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_46"));
            ti.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_135"), Convert.ToString(context[index]));
            TaskDataCache.ObjDataItem objData = ti.DataCache.GetObjData(context[index], session);
            if ((TypedInfoItem) objData != (TypedInfoItem) null)
              ti.traceAddAttribute(node1, LocalizationHolder.rm.GetString("Expert.Server_217"), objData.Caption);
          }
        }
        xmlNode1.AppendChild(xmlNode2);
      }
      if (node.ExclamationMarked)
      {
        if (ti.curDocNode != null)
          ti.traceAddAttribute(xmlNode1, "curDocNode", $"[{ti.curDocNode.Id}] ({ti.curDocNode.Name})");
        else
          ti.traceAddAttribute(xmlNode1, "curDocNode", "null");
        if (ti.defRootNode != null)
          ti.traceAddAttribute(xmlNode1, "defRootNode", $"[{ti.defRootNode.Id}] ({ti.defRootNode.Name})");
        else
          ti.traceAddAttribute(xmlNode1, "defRootNode", "null");
      }
      curNode = ti.curNode;
      ti.traceSetNode(xmlNode1);
    }
    finally
    {
      this.EndModifyTrace(ti);
    }
  }

  private void RestoreCurNode(int taskId, XmlNode curNode)
  {
    if (curNode == null)
      return;
    ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
    try
    {
      ti.traceSetNode(curNode);
    }
    finally
    {
      this.EndModifyTrace(ti);
    }
  }

  private void ReportError(int taskId, string Error)
  {
    ExpertServer.ExpServTask ti = this.StartModifyTrace(taskId);
    try
    {
      XmlNode node = ti.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_91"));
      ti.traceAddText(node, Error);
    }
    finally
    {
      this.EndModifyTrace(ti);
    }
  }

  private string _GetLastInfo(int taskId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
      return task.lastInfoStr;
  }

  private void ReportVerRule(int taskId, IUserSession ius, string ownerId)
  {
    IVersionRulesCacheService service = (IVersionRulesCacheService) this._serviceProvider.GetService(typeof (IVersionRulesCacheService));
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
    {
      FiltrationSettings filtrationSettings = service.GetFiltrationSettings((object) ius, ownerId);
      if (filtrationSettings == null)
        return;
      string ruleObjectCaption = filtrationSettings.CurrentRule.RuleObjectCaption;
      if (!(ruleObjectCaption != "") || !task.makeTrace)
        return;
      XmlNode node = task.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_80"));
      task.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_81"), ruleObjectCaption);
      task.traceAddAttribute(node, LocalizationHolder.rm.GetString("Expert.Server_223"), filtrationSettings.OwnerID);
    }
  }

  public int StartTask(Guid sessionGUID) => this._StartTask(sessionGUID);

  public int StartTask(Guid sessionGUID, ExpertTraceFlags traceFlags)
  {
    return this._StartTask(sessionGUID, traceFlags);
  }

  public void EndTask(int taskId) => this._EndTask(taskId);

  public void ChangeUserSession(int taskId, Guid sessionGuid)
  {
    this._ChangeSession(taskId, sessionGuid);
  }

  public void SetTraceFlags(int taskId, ExpertTraceFlags traceFlags)
  {
    this._SetTraceFlags(taskId, traceFlags);
  }

  public ExpertTraceFlags GetTraceFlags(int taskId) => this._GetTraceFlags(taskId);

  public byte[] GetTraceInfo(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return (byte[]) null;
    ExpertServer.ExpServTask task = this.taskList[taskId];
    if (task == null)
      return (byte[]) null;
    if (task.makeLog)
      this.iLH.AddToTrace("--- GetTraceInfo called ---", Intermech.Consts.traceAlways, this.logFileName);
    return this._GetTraceInfo(taskId);
  }

  public string GetLastInfo(int taskId) => this._GetLastInfo(taskId);

  public void DestroyTraceInfo(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.taskList[taskId];
    if (task == null)
      return;
    lock (task)
    {
      task.traceInfo = (XmlDocument) null;
      task.InitTraceInfo();
    }
  }

  public void SetTimeInterval(int taskId, TimeSpan ti)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.taskList[taskId];
    if (task == null)
      return;
    task.interval = ti;
    task.abortThreshold = DateTime.Now.Add(ti);
  }

  public void IAmAlive(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask expServTask = (ExpertServer.ExpServTask) null;
    if (!this.taskList.TryGetValue(taskId, out expServTask) || expServTask == null)
      return;
    expServTask.abortThreshold = expServTask.abortThreshold.Add(expServTask.interval);
  }

  public bool IsTaskClientDead(ExpertServer.ExpServTask ti)
  {
    if (!(ti.abortThreshold != DateTime.MinValue) || !(DateTime.Now > ti.abortThreshold))
      return false;
    ExpertServer.es.AbortProcess(ti.taskId);
    return true;
  }

  public bool IsTaskValid(int taskId)
  {
    return !this.abortedTasksContains(taskId) && this.taskList.ContainsKey(taskId);
  }

  public void SetVersionRuleOwnerId(int taskId, string versionRuleOwnerId)
  {
    this._SetVersionRuleOwnerId(taskId, versionRuleOwnerId);
  }

  public string GetVersionRuleOwnerId(int taskId) => this._GetVersionRuleOwnerId(taskId);

  public void SetVersionRule(int taskId, VersionsRule rule) => this._SetVersionRule(taskId, rule);

  public void SetDocAttributes(int taskId, long docObjectId)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) null;
    if (!this.taskList.TryGetValue(taskId, out ti) || ti == null)
      return;
    this._SetDocAttributes(ti, docObjectId);
  }

  public VersionsRule GetVersionRule(int taskId) => this._GetVersionRule(taskId);

  public void SetEditingContext(int taskId, long editingContextId)
  {
    this._SetEditingContext(taskId, editingContextId);
  }

  public long GetEditingContext(int taskId) => this._GetEditingContext(taskId);

  public void SetDateTimeFormat(int taskId, DateTimeFormatInfo dfi)
  {
    ExpertServer.ExpServTask expServTask;
    if (!this.taskList.TryGetValue(taskId, out expServTask) || expServTask == null)
      return;
    expServTask.dfi = dfi;
  }

  public void SetNumberFormat(int taskId, NumberFormatInfo nfi)
  {
    ExpertServer.ExpServTask expServTask;
    if (!this.taskList.TryGetValue(taskId, out expServTask) || expServTask == null)
      return;
    expServTask.nfi = nfi;
  }

  public ExpertResult Calculate(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    out object Value)
  {
    return this._Calculate(taskId, objTypeID, attrTypeID, objId, out Value);
  }

  public ExpertResult Calculate(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    long[] moreIDs,
    out object Value)
  {
    return this._Calculate(taskId, objTypeID, attrTypeID, objId, out Value, moreIDs);
  }

  public ExpertResult GetOrCalc(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    bool DisableTrace,
    out object Value)
  {
    return this._CalculateAllStages(taskId, objTypeID, attrTypeID, objId, DisableTrace, out Value);
  }

  public ExpertResult GenerateDocument(
    int taskId,
    long docScriptID,
    long[] context,
    out byte[] zippedDoc)
  {
    return this._GenerateDocument(taskId, docScriptID, context, out zippedDoc);
  }

  public ExpertResult GenerateDocument(
    int taskId,
    long docScriptID,
    long[] context,
    long docObjId)
  {
    return this._GenerateDocument(taskId, docScriptID, context, docObjId);
  }

  public ExpertResult RunCommandScript(int taskId, long docScriptID, long[] context)
  {
    return this._RunCommandScript(taskId, docScriptID, context);
  }

  public object GetParmValue(int taskId, long objID, int attrTypeID)
  {
    return this._GetParmValue(taskId, objID, attrTypeID);
  }

  public void SetParmValue(int taskId, long objID, int attrTypeID, object Value)
  {
    this._SetParmValue(taskId, objID, attrTypeID, Value, true);
  }

  public void ApplyParmValue(int taskId, long objID, int attrTypeID)
  {
    this._ApplyParmValue(taskId, objID, attrTypeID);
  }

  public void DeleteParmValue(int taskId, long objID, int attrTypeID)
  {
    this._DeleteParmValue(taskId, objID, attrTypeID);
  }

  public Dictionary<CalcAttrPair, CalculatedAttr> GetCalcParms(int taskId)
  {
    return this._GetCalcParms(taskId);
  }

  public Dictionary<CalcAttrPair, CalculatedAttr> GetModifiedParms(int taskId)
  {
    return this._GetModifiedParms(taskId);
  }

  public void SetCalcParms(int taskId, Dictionary<CalcAttrPair, CalculatedAttr> parms)
  {
    this._SetCalcParms(taskId, parms);
  }

  public void ApplyCalcParms(int taskId) => this._ApplyCalcParms(taskId);

  public void ApplyCalcParms(int taskId, List<CalculatedAttr> list)
  {
    this._ApplyCalcParms(taskId, list);
  }

  public void ClearCalcParms(int taskId) => this._ClearCalcParms(taskId);

  public ArrayList GetNeededAttrs(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return (ArrayList) null;
    return new ArrayList((ICollection) (this.taskList[taskId] ?? throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_82"))).NeededAttrs.Keys);
  }

  internal IUserSession GetTaskSession(int taskId)
  {
    return this.GetTaskSession(this.GetTask(taskId) ?? throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_82")));
  }

  internal IUserSession GetTaskSession(ExpertServer.ExpServTask ti)
  {
    lock (ti)
      return this.GetSession(ti);
  }

  public void SetWindowFiltration(int taskId, byte[] filtration)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.taskList[taskId];
    if (task == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_83"));
    MemoryStream serializationStream = new MemoryStream(filtration);
    HybridDictionary hybridDictionary = (HybridDictionary) null;
    try
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      hybridDictionary = binaryFormatter.Deserialize((Stream) serializationStream) as HybridDictionary;
      long num = (long) binaryFormatter.Deserialize((Stream) serializationStream);
    }
    catch
    {
    }
    task.window_filtr = hybridDictionary;
    if (hybridDictionary.Contains((object) ExpertServer.buttonSubstitutesGuid))
      task.clientAllZamens = new bool?(Convert.ToBoolean(hybridDictionary[(object) ExpertServer.buttonSubstitutesGuid]));
    else
      task.clientAllZamens = new bool?();
  }

  public ExpertResult CalcFormulaSimpleMode(int taskId, object tf, long objId, out object Value)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId, false);
    try
    {
      ti.SimpleCalcMode = true;
      ti.InitTraceAndLog();
      return ti.CalcFormula(new long[1]{ objId }, (HybridRowExp) null, (TempFormula) tf, out Value, 0L);
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  public ExpertResult CalcFormula(int taskId, long formId, long objId, out object Value)
  {
    IUserSession taskSession = this.GetTaskSession(taskId);
    TempFormula tf = (TempFormula) null;
    long objectID = formId;
    IDBObject dbObject = taskSession.GetObject(objectID);
    if (dbObject is IExpertFormulable)
    {
      IExpertFormulable expertFormulable = dbObject as IExpertFormulable;
      expertFormulable.Load();
      tf = expertFormulable.GetTempFormula();
    }
    if (tf == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_84"));
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) null;
    try
    {
      this._CheckTaskId(taskId, out ti);
    }
    catch (EAbort ex)
    {
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_243"));
    }
    return ti.CalcFormula(new long[1]{ objId }, (HybridRowExp) null, tf, out Value, 0L);
  }

  public ExpertResult CalcCondition(int taskId, long condId, long objId, out bool Value)
  {
    object obj = (object) null;
    int num = (int) this.CalcFormula(taskId, condId, objId, out obj);
    Value = Convert.ToBoolean(obj);
    return (ExpertResult) num;
  }

  public ExpertResult CalcCondition(
    int taskId,
    long objId,
    int attrId,
    long[] contextIds,
    out bool Value)
  {
    object obj = (object) null;
    int num = (int) this.CalcFormula(taskId, (object) (CondHelper.LoadObjectCond(this.GetTaskSession(taskId), objId, attrId) ?? throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_84"))), contextIds, out obj, 0L);
    Value = Convert.ToBoolean(obj);
    return (ExpertResult) num;
  }

  public ExpertResult CalcCondition(
    int taskId,
    long objId,
    int attrId,
    long contextId,
    out bool Value)
  {
    return this.CalcCondition(taskId, objId, attrId, new long[1]
    {
      contextId
    }, out Value);
  }

  public ExpertResult CalcFormula(
    int taskId,
    long objId,
    Guid formAttrGuid,
    long contextId,
    out object Value)
  {
    ExpertResult expertResult = ExpertResult.ObjectNotFound;
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      IUserSession taskSession = this.GetTaskSession(ti);
      Value = (object) null;
      long objID = objId;
      Guid attrGuid = formAttrGuid;
      TempFormula tf = ExpertServer.ReadFormula(taskSession, objID, attrGuid);
      if (tf != null)
        expertResult = this.CalcFormula(taskId, (object) tf, contextId, out Value);
    }
    finally
    {
      this.EndJobForTask(ti);
    }
    return expertResult;
  }

  public ExpertResult CalcFormula(int taskId, object tf, long objId, out object Value)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      this.GetTaskSession(taskId);
      return ti.CalcFormula(new long[1]{ objId }, (HybridRowExp) null, (TempFormula) tf, out Value, 0L);
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  public ExpertResult CalcFormula(
    int taskId,
    object tf,
    long[] objIds,
    out object Value,
    long relId = 0)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      this.GetTaskSession(taskId);
      return ti.CalcFormula(objIds, (HybridRowExp) null, (TempFormula) tf, out Value, relId);
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  public ExpertResult InnerCalculate(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    out object Value)
  {
    IUserSession taskSession = this.GetTaskSession(taskId);
    Guid objTypeGuid = objTypeID <= 0 ? Guid.Empty : MetaDataHelper.GetObjectTypeGuid(objTypeID);
    Guid attrTypeGuid = attrTypeID <= 0 ? Guid.Empty : MetaDataHelper.GetAttributeTypeGuid(attrTypeID);
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task.calcStack.Contains(objId, objTypeID, attrTypeID))
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_143"));
    task.calcStack.Push(objId, objTypeID, attrTypeID);
    try
    {
      return this.InnerCalculate(taskId, taskSession, objTypeID, attrTypeID, objTypeGuid, attrTypeGuid, objId, out Value);
    }
    finally
    {
      task.calcStack.Pop();
    }
  }

  public bool FillExpObjInfo(ref ExpObjInfo eoi, Guid sessionGuid)
  {
    IUserSession sessionById = (IUserSession) (UserSession.GetSessionByID(sessionGuid) as UserSession);
    if (sessionById == null)
      return false;
    IDBObject dbObject = sessionById.GetObject(eoi.objID, false);
    if (dbObject == null || !(dbObject is DocScript))
      return false;
    DocScript docScript = (DocScript) sessionById.GetObject(eoi.objID, false);
    docScript.Load();
    eoi.zippedScript = docScript.Script;
    eoi.scriptName = docScript.Caption;
    try
    {
      docScript.UnpackXML();
    }
    catch (Exception ex)
    {
      throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_23")}", ex);
    }
    IDBAttribute attributeById = docScript.GetAttributeByID(ExpertConsts.Consts.attrTemplateLink);
    eoi.templateID = attributeById != null && !attributeById.Value.IsDBNull() ? Convert.ToInt64(attributeById.Value) : throw new EAbort(ExpertResult.NoSuitableObjects, "No template for the scenario");
    return this._CollectData(ExpertServer.LoadScriptTree(docScript.xDoc), eoi, sessionById);
  }

  private bool _CollectData(ScriptTreeNode node, ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = true;
    if (node.op != null)
      flag = node.op.CollectExpObjInfo(eoi, ius);
    if (node.mod != null)
      flag = flag && node.mod.CollectExpObjInfo(eoi, ius);
    foreach (ScriptTreeNode node1 in node.Items)
    {
      if (!node1.label.StartsWith("#"))
        flag = flag && this._CollectData(node1, eoi, ius);
    }
    return flag;
  }

  public DataTable GetAttrTypesTable(
    SortedDictionary<int, GuidAndName> attrTypeIds,
    out DataTable Groups)
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.GetAttrTypesTable");
    try
    {
      DataTable attrTypesTable = sessionTemporaryClone.GetAttributeTypeCollection(-1).Select("F_ATTRIBUTE_ID ASC", (object[]) null);
      for (int index = attrTypesTable.Rows.Count - 1; index >= 0; --index)
      {
        int int32 = Convert.ToInt32(attrTypesTable.Rows[index][0]);
        if (!attrTypeIds.ContainsKey(int32))
          attrTypesTable.Rows.RemoveAt(index);
      }
      attrTypesTable.Columns.Add("F_GROUPLIST", typeof (string));
      Groups = new DataTable();
      Groups.Columns.Add("F_COLUMN_ID", typeof (int));
      Groups.Columns.Add("F_COLUMN_NAME", typeof (string));
      List<int> intList = new List<int>();
      for (int index1 = 0; index1 < attrTypesTable.Rows.Count; ++index1)
      {
        DataRow row = attrTypesTable.Rows[index1];
        int int32 = Convert.ToInt32(row[0]);
        IDBAttributeType attributeType = sessionTemporaryClone.GetAttributeType(int32, false);
        if (attributeType != null)
        {
          int[] groupsList = attributeType.GetGroupsList();
          StringBuilder stringBuilder = new StringBuilder();
          for (int index2 = 0; index2 < groupsList.Length; ++index2)
          {
            int aGroupID = groupsList[index2];
            if (!intList.Contains(aGroupID))
            {
              IDBAttributesGroup attributesGroup = sessionTemporaryClone.GetAttributesGroup(aGroupID, false);
              if (attributesGroup != null)
                Groups.Rows.Add((object) aGroupID, (object) attributesGroup.GroupName);
              else
                continue;
            }
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", " + Convert.ToString(aGroupID));
            else
              stringBuilder.Append(aGroupID);
          }
          row["F_GROUPLIST"] = (object) stringBuilder.ToString();
        }
      }
      return attrTypesTable;
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.GetAttrTypesTable");
    }
  }

  public DataTable GetObjTypesTable(SortedDictionary<int, GuidAndName> objTypeIds)
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.GetObjTypesTable");
    try
    {
      DataTable objTypesTable = sessionTemporaryClone.GetObjectTypeCollection(-2).Select("F_OBJECT_TYPE ASC", (object[]) null);
      for (int index = objTypesTable.Rows.Count - 1; index >= 0; --index)
      {
        int int32 = Convert.ToInt32(objTypesTable.Rows[index][0]);
        if (!objTypeIds.ContainsKey(int32))
          objTypesTable.Rows.RemoveAt(index);
      }
      return objTypesTable;
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.GetObjTypesTable");
    }
  }

  public string GetFolderConds(long esObjectId)
  {
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.GetFolderConds");
    try
    {
      Dictionary<long, ESFolderInfo> foldersForEsObject = ESFolderKeeper.Keeper.GetAllFoldersForESObject(sessionTemporaryClone, esObjectId);
      StringBuilder stringBuilder = new StringBuilder();
      if (foldersForEsObject != null)
      {
        foreach (ESFolderInfo esFolderInfo in foldersForEsObject.Values)
        {
          if (esFolderInfo.Cond != null && esFolderInfo.Cond.Count != 0)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.AppendLine();
            stringBuilder.Append(esFolderInfo.Cond.ToString());
          }
        }
      }
      return stringBuilder.ToString();
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.GetFolderConds");
    }
  }

  public bool ReflectObjUpdate(
    Guid sessionGuid,
    long objId,
    ExpertTraceFlags traceFlags,
    TempFormula branchCond,
    out byte[] traceInfo)
  {
    return this._ReflectObjUpdate(sessionGuid, objId, traceFlags, branchCond, out traceInfo);
  }

  public bool CreateExpertFormula(
    Guid sessionGuid,
    string resObjTypeGuid,
    string resAttrTypeGuid,
    object _tf,
    object _cond)
  {
    TempFormula tf = (TempFormula) _tf;
    TempFormula cond = (TempFormula) _cond;
    return this._CreateExpertFormula(sessionGuid, resObjTypeGuid, resAttrTypeGuid, tf, cond);
  }

  public ExpertResult CalcTable(int taskId, long tableId, long objId, out object[] Values)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      IUserSession taskSession = this.GetTaskSession(ti);
      ResultExpertValue[] Result = (ResultExpertValue[]) null;
      int num = (int) ExpertTableProcessor.CalcTable(ti, taskSession, objId, tableId, out Result);
      Values = (object[]) Result;
      return (ExpertResult) num;
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  public ExpertResult CalcTable(
    int taskId,
    object tableCollection,
    long objId,
    out object[] Values)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      IUserSession taskSession = this.GetTaskSession(ti);
      ResultExpertValue[] Result = (ResultExpertValue[]) null;
      int num = (int) ExpertTableProcessor.CalcTable(ti, taskSession, objId, tableCollection as eTableCollection, -1L, out Result);
      Values = (object[]) Result;
      return (ExpertResult) num;
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  public bool RecalcForAttr(int taskId, long objId, int attrTypeID, long relID = -1)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      return this._RecalcForAttr(taskId, objId, attrTypeID, relID);
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  public ExpertResult GenerateComplect(
    int taskId,
    long compScriptID,
    long contextID,
    out List<ChangeInfo> changed,
    bool dopComplects = false)
  {
    return this._GenerateComplect(taskId, compScriptID, contextID, -1L, GenMode.genModeGenerate, out changed, dopComplects);
  }

  public ExpertResult GenerateComplect(CompGenParms cgp, out List<ChangeInfo> changed)
  {
    return this._GenerateComplect(cgp.TaskId, cgp.CompScriptId, cgp.ContextId, -1L, GenMode.genModeGenerate, out changed, cgp.DopComplects);
  }

  public ExpertResult CreateComplectVersion(
    int taskId,
    long compScriptID,
    long contextID,
    long complectID,
    out List<ChangeInfo> changed,
    bool dopComplects = false)
  {
    return this._GenerateComplect(taskId, compScriptID, contextID, complectID, GenMode.genModeVersion, out changed, dopComplects);
  }

  public ExpertResult CreateComplectVersion(CompGenParms cgp, out List<ChangeInfo> changed)
  {
    return this._GenerateComplect(cgp.TaskId, cgp.CompScriptId, cgp.ContextId, cgp.ComplectId, GenMode.genModeVersion, out changed, cgp.DopComplects);
  }

  public ExpertResult RefreshComplect(
    int taskId,
    long compScriptID,
    long contextID,
    long complectID,
    out List<ChangeInfo> changed,
    bool dopComplects = false)
  {
    return this._GenerateComplect(taskId, compScriptID, contextID, complectID, GenMode.genModeRefresh, out changed, dopComplects);
  }

  public ExpertResult RefreshComplect(CompGenParms cgp, out List<ChangeInfo> changed)
  {
    return this._GenerateComplect(cgp.TaskId, cgp.CompScriptId, cgp.ContextId, cgp.ComplectId, GenMode.genModeRefresh, out changed, cgp.DopComplects);
  }

  public void SetDebugMode(int taskId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      return;
    task.testMode = true;
  }

  public void SetTrace(int taskId, bool enabled)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      return;
    task.makeTrace = enabled;
  }

  public bool GetTrace(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return false;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    return task != null && task.makeTrace;
  }

  public void SetLog(int taskId, bool enabled)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      return;
    task.makeLog = enabled;
  }

  public bool GetLog(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return false;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    return task != null && task.makeLog;
  }

  public List<string> GetUserReport(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return (List<string>) null;
    return this.GetTask(taskId)?.userReport;
  }

  public HybridTableExp GetGlobalObjectsTable(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return (HybridTableExp) null;
    return this.GetTask(taskId)?.savedData;
  }

  public HybridTableExp GetGlobalLinksTable(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return (HybridTableExp) null;
    return this.GetTask(taskId)?.savedLinks;
  }

  public List<string> CheckExpertObjects() => this._CheckExpertObjects();

  public FuncData GetFuncData(int index)
  {
    return !this.funcIds.ContainsKey((object) index) ? (FuncData) null : (FuncData) this.funcDatas[(object) (string) this.funcIds[(object) index]];
  }

  public List<int> GetFuncIds()
  {
    List<int> funcIds = new List<int>();
    foreach (int key in (IEnumerable) this.funcIds.Keys)
      funcIds.Add(key);
    return funcIds;
  }

  public List<string> GetFuncNames()
  {
    List<string> funcNames = new List<string>();
    foreach (string key in (IEnumerable) this.funcDatas.Keys)
      funcNames.Add(key);
    return funcNames;
  }

  public List<string> GetComparerNames()
  {
    List<string> comparerNames = new List<string>();
    foreach (string key in (IEnumerable) this.comparers.Keys)
      comparerNames.Add(key);
    return comparerNames;
  }

  public List<string> GetProcNames()
  {
    List<string> procNames = new List<string>();
    foreach (string key in (IEnumerable) this.procHandlers.Keys)
      procNames.Add(key);
    return procNames;
  }

  public void RegUserFunction(
    int Id,
    string Name,
    DataType[] parmTypes,
    DataType result,
    string description,
    FuncHandler handler)
  {
    ExpertServer.RegisterUserFunction(Id, Name, parmTypes, result, description, handler);
  }

  public void RegComparer(string Name, CompareFuncHandler cfh)
  {
    ExpertServer.RegisterComparer(Name, cfh);
  }

  public void RegUserProc(string Name, ScriptProcHandler handler)
  {
    ExpertServer.RegisterUserProc(Name, handler);
  }

  public void UnregUserFunction(string Name) => ExpertServer.UnregisterUserFunction(Name);

  public void UnregComparer(string Name) => ExpertServer.UnregisterComparer(Name);

  public void UnregUserProc(string Name) => ExpertServer.UnregisterUserProc(Name);

  public object InvokeFunc(string funcName, ArrayList parms)
  {
    return ExpertServer.Invoke(funcName, parms);
  }

  public object InvokeFunc(int id, ArrayList parms)
  {
    return this.funcIds.Contains((object) id) ? ExpertServer.Invoke((string) this.funcIds[(object) id], parms) : (object) null;
  }

  public string GetDocName(int taskId, long scriptId, long contId)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      IUserSession taskSession = this.GetTaskSession(taskId);
      IDBObject dbObject = taskSession.GetObject(scriptId);
      if (dbObject == null)
        return "";
      IDBAttribute attributeById = dbObject.GetAttributeByID(ExpertConsts.Consts.attrGenDocName);
      if (attributeById == null)
        return "";
      string asString = attributeById.AsString;
      return ExpertServer.GetDocName(taskSession, ti, contId, asString);
    }
    finally
    {
      this.EndJobForTask(taskId);
    }
  }

  public List<string> FixIdentsComplete()
  {
    IUserSession systemSessionClone = this.GetSystemSessionClone("Expert.FixIdentsComplete");
    List<string> stringList = new List<string>();
    try
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objObject);
      string EventStr1 = "----------------  Converting Expert Object Identifiers (start) ------------------";
      this.iLH.AddToTrace(EventStr1, Intermech.Consts.traceAlways, this.logFileName);
      stringList.Add(EventStr1);
      foreach (int objTypeId in childrenIdRecursive)
      {
        if (objTypeId != ExpertConsts.Consts.objObject && objTypeId != ExpertConsts.Consts.objBaseScript && objTypeId != ExpertConsts.Consts.objBaseFormula)
        {
          foreach (long objectID in this.GetObjectsByType(systemSessionClone, objTypeId))
          {
            try
            {
              ExpertObject expertObject = (ExpertObject) systemSessionClone.GetObject(objectID, false);
              if (expertObject != null)
              {
                expertObject.Load();
                if (expertObject.FixIdentsComplete(systemSessionClone))
                {
                  string EventStr2 = $"Object {objectID} was changed";
                  this.iLH.AddToTrace(EventStr2, Intermech.Consts.traceAlways, this.logFileName);
                  stringList.Add(EventStr2);
                }
              }
            }
            catch (Exception ex)
            {
              string EventStr3 = string.Format("Exception when converting object {0}: " + ex.Message, (object) objectID);
              this.iLH.AddToTrace(EventStr3, Intermech.Consts.traceAlways, this.logFileName);
              stringList.Add(EventStr3);
            }
          }
        }
      }
    }
    finally
    {
      string EventStr = "----------------  Converting Expert Object Identifiers (end) ------------------";
      this.iLH.AddToTrace(EventStr, Intermech.Consts.traceAlways, this.logFileName);
      stringList.Add(EventStr);
      systemSessionClone?.Logout("Expert.FixIdentsComplete");
    }
    return stringList;
  }

  public List<string> FixIdentsOne(long objId)
  {
    IUserSession systemSessionClone = this.GetSystemSessionClone("Expert.FixIdentsOne");
    List<string> stringList = new List<string>();
    try
    {
      MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objObject);
      string EventStr1 = "----------------  Converting Expert Object Identifiers (start) ------------------";
      this.iLH.AddToTrace(EventStr1, Intermech.Consts.traceAlways, this.logFileName);
      stringList.Add(EventStr1);
      try
      {
        ExpertObject expertObject = (ExpertObject) systemSessionClone.GetObject(objId, false);
        if (expertObject != null)
        {
          expertObject.Load();
          if (expertObject.FixIdentsComplete(systemSessionClone))
          {
            string EventStr2 = $"Object {objId} was changed";
            this.iLH.AddToTrace(EventStr2, Intermech.Consts.traceAlways, this.logFileName);
            stringList.Add(EventStr2);
          }
        }
      }
      catch (Exception ex)
      {
        string EventStr3 = string.Format("Exception when converting object {0}: " + ex.Message, (object) objId);
        this.iLH.AddToTrace(EventStr3, Intermech.Consts.traceAlways, this.logFileName);
        stringList.Add(EventStr3);
      }
    }
    finally
    {
      systemSessionClone?.Logout("Expert.FixIdentsOne");
      string EventStr = "----------------  Converting Expert Object Identifiers (end) ------------------";
      this.iLH.AddToTrace(EventStr, Intermech.Consts.traceAlways, this.logFileName);
      stringList.Add(EventStr);
    }
    return stringList;
  }

  public List<string> CreateGUIDs()
  {
    IUserSession systemSessionClone = this.GetSystemSessionClone("Expert.CreateGUIDs");
    List<string> guiDs = new List<string>();
    try
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objBaseFormula);
      string EventStr1 = "----------------  Creating GUIDs in formulas (start) ------------------";
      this.iLH.AddToTrace(EventStr1, Intermech.Consts.traceAlways, this.logFileName);
      guiDs.Add(EventStr1);
      foreach (int objTypeId in childrenIdRecursive)
      {
        if (objTypeId != ExpertConsts.Consts.objBaseFormula)
        {
          foreach (long objectID in this.GetObjectsByType(systemSessionClone, objTypeId))
          {
            try
            {
              ExpertObject expertObject = (ExpertObject) systemSessionClone.GetObject(objectID, false);
              if (expertObject != null)
              {
                expertObject.Load();
                if (expertObject.CreateGUIDs(systemSessionClone))
                {
                  string EventStr2 = $"Object {objectID} was changed";
                  this.iLH.AddToTrace(EventStr2, Intermech.Consts.traceAlways, this.logFileName);
                  guiDs.Add(EventStr2);
                }
              }
            }
            catch (Exception ex)
            {
              string EventStr3 = string.Format("Exception when creating GUIDs in object {0}: " + ex.Message, (object) objectID);
              this.iLH.AddToTrace(EventStr3, Intermech.Consts.traceAlways, this.logFileName);
              guiDs.Add(EventStr3);
            }
          }
        }
      }
    }
    finally
    {
      systemSessionClone?.Logout("Expert.CreateGUIDs");
      string EventStr = "----------------  Creating GUIDs in formulas (end) ------------------";
      this.iLH.AddToTrace(EventStr, Intermech.Consts.traceAlways, this.logFileName);
      guiDs.Add(EventStr);
    }
    return guiDs;
  }

  public List<string> CreateGIUDsOne(long objId)
  {
    IUserSession systemSessionClone = this.GetSystemSessionClone("Expert.CreateGIUDsOne");
    List<string> giuDsOne = new List<string>();
    try
    {
      MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objObject);
      string EventStr1 = "----------------  Converting Expert Object Identifiers (start) ------------------";
      this.iLH.AddToTrace(EventStr1, Intermech.Consts.traceAlways, this.logFileName);
      giuDsOne.Add(EventStr1);
      try
      {
        ExpertObject expertObject = (ExpertObject) systemSessionClone.GetObject(objId, false);
        if (expertObject != null)
        {
          expertObject.Load();
          if (expertObject.CreateGUIDs(systemSessionClone))
          {
            string EventStr2 = $"Object {objId} was changed";
            this.iLH.AddToTrace(EventStr2, Intermech.Consts.traceAlways, this.logFileName);
            giuDsOne.Add(EventStr2);
          }
        }
      }
      catch (Exception ex)
      {
        string EventStr3 = string.Format("Exception when converting object {0}: " + ex.Message, (object) objId);
        this.iLH.AddToTrace(EventStr3, Intermech.Consts.traceAlways, this.logFileName);
        giuDsOne.Add(EventStr3);
      }
    }
    finally
    {
      systemSessionClone?.Logout("Expert.CreateGIUDsOne");
      string EventStr = "----------------  Converting Expert Object Identifiers (end) ------------------";
      this.iLH.AddToTrace(EventStr, Intermech.Consts.traceAlways, this.logFileName);
      giuDsOne.Add(EventStr);
    }
    return giuDsOne;
  }

  public void ClearCaches()
  {
    this.attrRules.Clear();
    this.objRules.Clear();
    this.recalcScripts.Clear();
    this.expertTables.Clear();
    this.expertConds.Clear();
    this.expertFormulae.Clear();
    this.expertScripts.Clear();
    this.expertObjInfo.Clear();
    this.visScripts.Clear();
  }

  public void ShowExpertInfo()
  {
    StringBuilder stringBuilder = new StringBuilder();
    Console.WriteLine("================   Expert Server task list  ====================");
    foreach (int key in (IEnumerable<int>) ExpertServer.es.taskList.Keys)
    {
      if (key != 0)
      {
        ExpertServer.ExpServTask task = ExpertServer.es.taskList[key];
        IUserSession session = ExpertServer.es.GetSession(task);
        stringBuilder.Append(session.UserName);
        stringBuilder.Append("\t");
        stringBuilder.Append(session.UserID);
        stringBuilder.Append("\t");
        stringBuilder.Append(session.ComputerName);
        stringBuilder.Append("\t");
        stringBuilder.Append(session.SessionGUID.ToString());
        stringBuilder.Append("\t");
        if (task.makeLog)
          stringBuilder.Append("LOG\t");
        if (task.makeTrace)
          stringBuilder.Append("TRACE\t");
        Console.WriteLine(stringBuilder.ToString());
      }
    }
    Console.WriteLine("================   end of task list  ===========================");
  }

  public void SetTaskParm(int taskId, string Key, object Value)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      return;
    if (task.namedParms.ContainsKey(Key))
      task.namedParms[Key] = Value;
    else
      task.namedParms.Add(Key, Value);
  }

  public object GetTaskParm(int taskId, string Key)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      return (object) null;
    return task.namedParms.ContainsKey(Key) ? task.namedParms[Key] : (object) null;
  }

  public void SetTaskParms(int taskId, Dictionary<string, object> parms)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      return;
    foreach (KeyValuePair<string, object> parm in parms)
    {
      if (task.namedParms.ContainsKey(parm.Key))
        task.namedParms[parm.Key] = parm.Value;
      else
        task.namedParms.Add(parm.Key, parm.Value);
    }
  }

  public List<ObjChangedList> GetAttrChangedList(int taskId)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      if (ti == null)
        return (List<ObjChangedList>) null;
      IUserSession session = ti.GetSession();
      Dictionary<long, ObjChangedList> dictionary = new Dictionary<long, ObjChangedList>();
      foreach (CalculatedAttr calculatedAttr in ti.CalcAttrs.Values)
      {
        if (!calculatedAttr.Temporary && !calculatedAttr.Assigned && calculatedAttr.attState != AttrState.SetByUser)
        {
          IMSAttributeType attributeType;
          try
          {
            attributeType = MetaDataHelper.GetAttributeType(calculatedAttr.ca_pair.attrTypeID);
            if (attributeType == null)
              continue;
          }
          catch
          {
            continue;
          }
          long objId = calculatedAttr.ca_pair.objID;
          if (objId != 0L)
          {
            bool flag1 = false;
            long num = objId;
            object[] objArray = (object[]) null;
            object oldValue = (object) null;
            if (session.GetObjectInfo(objId).Empty)
            {
              IDBRelation relation = session.GetRelation(objId, false);
              if (relation != null)
              {
                num = relation.ProjID;
                flag1 = true;
                objArray = relation.GetValuesByID(calculatedAttr.ca_pair.attrTypeID, false);
              }
              else
                continue;
            }
            else
            {
              IDBObject dbObject = session.GetObject(objId, false);
              if (dbObject != null)
                objArray = dbObject.GetValuesByID(calculatedAttr.ca_pair.attrTypeID, false);
            }
            if (objArray != null)
              oldValue = objArray[0];
            if (!dictionary.ContainsKey(num) && dictionary.ContainsKey(-num))
              num = -num;
            ObjChangedList objChangedList;
            if (dictionary.ContainsKey(num))
            {
              objChangedList = dictionary[num];
            }
            else
            {
              objChangedList = new ObjChangedList(num, session);
              dictionary.Add(num, objChangedList);
            }
            if (flag1)
              objChangedList.InitChangedRels();
            List<AttrChange> attrChangeList = (List<AttrChange>) null;
            if (flag1)
            {
              if (objChangedList.ChangedRels != null)
              {
                foreach (RelChangedList changedRel in objChangedList.ChangedRels)
                {
                  if (changedRel.RelId == objId)
                  {
                    attrChangeList = (List<AttrChange>) changedRel;
                    break;
                  }
                }
                if (attrChangeList == null)
                {
                  RelChangedList relChangedList = new RelChangedList(objId, session);
                  objChangedList.ChangedRels.Add(relChangedList);
                  attrChangeList = (List<AttrChange>) relChangedList;
                }
              }
              else
                continue;
            }
            else
              attrChangeList = (List<AttrChange>) objChangedList;
            if (attrChangeList != null)
            {
              bool flag2 = false;
              foreach (AttrChange attrChange in attrChangeList)
              {
                if (attrChange.AttrId == calculatedAttr.ca_pair.attrTypeID)
                {
                  flag2 = true;
                  attrChange.OldValue = oldValue;
                  attrChange.NewValue = calculatedAttr.Value;
                  attrChange.AttrType = attributeType.FieldType;
                  break;
                }
              }
              if (!flag2 && (oldValue == null || !oldValue.Equals(calculatedAttr.Value)))
              {
                AttrChange attrChange = new AttrChange(calculatedAttr.ca_pair.attrTypeID, oldValue, calculatedAttr.Value, attributeType.FieldType);
                attrChangeList.Add(attrChange);
              }
            }
          }
        }
      }
      if (dictionary.Keys.Count == 0)
        return (List<ObjChangedList>) null;
      List<ObjChangedList> attrChangedList = new List<ObjChangedList>();
      foreach (ObjChangedList objChangedList in dictionary.Values)
        attrChangedList.Add(objChangedList);
      return attrChangedList;
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  public bool ApplyChangesList(int taskId, List<ObjChangedList> changedList)
  {
    ExpertServer.ExpServTask ti = this.StartJobForTask(taskId);
    try
    {
      if (ti == null)
        return false;
      UserSession session = (UserSession) ti.GetSession();
      session.StartTransaction();
      try
      {
        List<long> longList = new List<long>();
        for (int index = 0; index < changedList.Count; ++index)
        {
          ObjChangedList changed = changedList[index];
          IDBObject idbA;
          if (changed.Count > 0)
          {
            idbA = this._prepareObject(changed.ObjVerId, (IUserSession) session, changed);
            this._ApplyAttrs((IDBAttributable) idbA, (List<AttrChange>) changed);
            this.SetRelationAtts(changed.ChangedRels, (IUserSession) session);
          }
          else
          {
            idbA = session.GetObject(changed.ObjVerId);
            try
            {
              this.SetRelationAtts(changed.ChangedRels, (IUserSession) session);
            }
            catch
            {
              idbA = this._prepareObject(changed.ObjVerId, (IUserSession) session, changed);
              if (idbA.ObjectID != changed.ObjVerId)
                this.SetRelationAtts(changed.ChangedRels, (IUserSession) session);
              else
                throw;
            }
          }
          longList.Add(idbA.ObjectID);
        }
        session.Commit();
        for (int index = 0; index < changedList.Count; ++index)
          this.MarkAttrsAssigned(ti, changedList[index], longList[index]);
      }
      catch
      {
        session.Rollback();
        throw;
      }
      return true;
    }
    finally
    {
      this.EndJobForTask(ti);
    }
  }

  internal void _ApplyAttrs(IDBAttributable idbA, List<AttrChange> attrList)
  {
    Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
    for (int index = 0; index < attrList.Count; ++index)
      dictionary1.Add(attrList[index].AttrId, index);
    AttributeValues[] attributesValues = idbA.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeCaption);
    List<int> intList = new List<int>();
    Dictionary<int, MultiValueModes> dictionary2 = new Dictionary<int, MultiValueModes>();
    foreach (AttributeValues attributeValues in attributesValues)
    {
      if (dictionary1.ContainsKey(attributeValues.AttributeID))
      {
        int index1 = dictionary1[attributeValues.AttributeID];
        dictionary2.Add(attributeValues.AttributeID, attributeValues.MultipleValued);
        if (attributeValues.Values.Length != 0)
        {
          if (attributeValues.MultipleValued == MultiValueModes.SingleValue)
          {
            if (attributeValues.Values[0].Equals(attrList[index1].NewValue))
              intList.Add(index1);
          }
          else if (attrList[index1].NewValue is PacketValue newValue && newValue.Count == attributeValues.Values.Length)
          {
            bool flag = true;
            for (int index2 = 0; index2 < newValue.Count; ++index2)
            {
              if (!attributeValues.Values[index2].Equals(newValue[index2].Value))
              {
                flag = false;
                break;
              }
            }
            if (flag)
              intList.Add(index1);
          }
        }
      }
    }
    for (int index = intList.Count - 1; index >= 0; --index)
      attrList.RemoveAt(intList[index]);
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttrChange attr in attrList)
    {
      MultiValueModes multipleValued = !dictionary2.ContainsKey(attr.AttrId) ? MetaDataHelper.GetAttributeType(attr.AttrId).MultiValueMode : dictionary2[attr.AttrId];
      object[] initValues;
      if (multipleValued == MultiValueModes.SingleValue)
        initValues = new object[1]{ attr.NewValue };
      else if (!(attr.NewValue is PacketValue newValue))
      {
        initValues = new object[1]{ attr.NewValue };
      }
      else
      {
        List<object> objectList = new List<object>();
        for (int index = 0; index < newValue.Count; ++index)
          objectList.Add(newValue[index].Value);
        initValues = objectList.ToArray();
      }
      attributeValuesList.Add(new AttributeValues(attr.AttrId, attr.AttrType, multipleValued, initValues));
    }
    AttributeValues[] array = attributeValuesList.ToArray();
    idbA.SetAttributesValues(array);
  }

  internal IDBObject _prepareObject(long objId, IUserSession ius, ObjChangedList ocl)
  {
    ((UserSession) ius).DBObjectsCacheRemoveVersion(objId);
    IDBObject dbObject = ius.GetObject(objId, true);
    bool flag = false;
    foreach (AttrChange attrChange in (List<AttrChange>) ocl)
    {
      if (attrChange.OldValue != attrChange.NewValue && (attrChange.OldValue == null || !attrChange.OldValue.Equals(attrChange.NewValue)))
      {
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, attrChange.AttrId);
        if (attribute4ObjectType == null || (attribute4ObjectType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None)
          flag = true;
      }
    }
    if (flag)
    {
      switch (dbObject.ObjectModifyMode)
      {
        case ObjectModifyModes.InBase:
          return dbObject;
        case ObjectModifyModes.Checkout:
          return dbObject.CheckOut();
        case ObjectModifyModes.CreateVersion:
          IDBObject version = ius.GetObjectCollection(dbObject.ObjectType).CreateVersion(dbObject.ObjectID);
          version.CommitCreation(true);
          return version;
        case ObjectModifyModes.CantModify:
          throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_272"), (object) objId));
      }
    }
    return dbObject;
  }

  internal void SetRelationAtts(List<RelChangedList> changedRels, IUserSession ius)
  {
    if (changedRels == null)
      return;
    foreach (RelChangedList changedRel in changedRels)
      this._ApplyAttrs((IDBAttributable) ius.GetRelation(changedRel.RelId, true), (List<AttrChange>) changedRel);
  }

  internal void MarkAttrsAssigned(ExpertServer.ExpServTask ti, ObjChangedList ocl, long newObjId)
  {
    HashSet<long> longSet = new HashSet<long>();
    longSet.Add(ocl.ObjVerId);
    if (ocl.ChangedRels != null)
    {
      foreach (RelChangedList changedRel in ocl.ChangedRels)
        longSet.Add(changedRel.RelId);
    }
    foreach (KeyValuePair<CalcAttrPair, CalculatedAttr> calcAttr in (Dictionary<CalcAttrPair, CalculatedAttr>) ti.CalcAttrs)
    {
      CalcAttrPair key = calcAttr.Key;
      if (longSet.Contains(key.objID))
      {
        if (key.objID == ocl.ObjVerId)
        {
          for (int index = 0; index < ocl.Count; ++index)
          {
            AttrChange attrChange = ocl[index];
            if (key.attrTypeID == attrChange.AttrId)
              calcAttr.Value.Assigned = true;
          }
        }
        else
        {
          for (int index1 = 0; index1 < ocl.ChangedRels.Count; ++index1)
          {
            RelChangedList changedRel = ocl.ChangedRels[index1];
            if (key.objID == changedRel.RelId)
            {
              for (int index2 = 0; index2 < ocl.Count; ++index2)
              {
                AttrChange attrChange = ocl[index2];
                if (key.attrTypeID == attrChange.AttrId)
                  calcAttr.Value.Assigned = true;
              }
            }
          }
        }
      }
    }
  }

  public IExpertServerTask GetServerTask(int taskId) => (IExpertServerTask) this.GetTask(taskId);

  public IExpertServerTask GetServerTask(Guid sessionGuid)
  {
    foreach (ExpertServer.ExpServTask serverTask in (IEnumerable<ExpertServer.ExpServTask>) this.taskList.Values)
    {
      if (serverTask.clonedSessionGUID.Equals(sessionGuid))
        return (IExpertServerTask) serverTask;
    }
    return (IExpertServerTask) null;
  }

  public ExpertResult Calculate(
    IExpertServerTask ti,
    int objTypeId,
    int attrTypeId,
    long objId,
    out object value,
    long contObjId = -1,
    long[] moreObjIds = null)
  {
    if (!(ti is ExpertServer.ExpServTask expServTask))
      throw new ExpertServerException("Invalid argument 'ti'. ExpServTask is expected/");
    if (expServTask.thread == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_297"));
    Guid objTypeGuid = objTypeId == -1 ? Guid.Empty : MetaDataHelper.GetObjectTypeGuid(objTypeId);
    Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
    if (expServTask.calcStack.Contains(objId, objTypeId, attrTypeId))
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_143"));
    expServTask.calcStack.Push(objId, objTypeId, attrTypeId);
    try
    {
      return this.InnerCalculate(ti.TaskId, ti.Session, objTypeId, attrTypeId, objTypeGuid, attributeTypeGuid, objId, out value, contObjId, moreObjIds);
    }
    finally
    {
      expServTask.calcStack.Pop();
    }
  }

  public ExpertResult CalculateQuiet(
    IExpertServerTask ti,
    int objTypeId,
    int attrTypeId,
    long objId,
    out object value,
    long contObjId = -1,
    long[] moreObjIds = null)
  {
    if (!(ti is ExpertServer.ExpServTask ti1))
      throw new ExpertServerException("Invalid argument 'ti'. ExpServTask is expected/");
    if (ti1.thread == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_297"));
    MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
    return this.InnerCalculateQuiet(ti1, ti1.Session, objTypeId, attrTypeId, objId, out value, contObjId, moreObjIds);
  }

  public static void RegisterUserFunction(
    int Id,
    string Name,
    DataType[] parmTypes,
    DataType result,
    string description,
    FuncHandler handler)
  {
    if (Id <= 1000)
      throw new Exception(LocalizationHolder.rm.GetString("Expert_18"));
    if (ExpertServer.es.funcIds.ContainsKey((object) Id))
      throw new Exception(LocalizationHolder.rm.GetString("Expert_19") + Convert.ToString(Id) + LocalizationHolder.rm.GetString("Expert_20"));
    if (ExpertServer.es.funcIds.ContainsValue((object) Name))
      throw new Exception(LocalizationHolder.rm.GetString("Expert_21") + Name + LocalizationHolder.rm.GetString("Expert_22"));
    ExpertServer.es.funcIds.Add((object) Id, (object) Name);
    ExpertServer.es.funcDatas.Add((object) Name, (object) new FuncData((FormulaFunc) Id, Name, parmTypes, result)
    {
      description = description
    });
    ExpertServer.es.funcHandlers.Add((object) Name, (object) handler);
  }

  public static void UnregisterUserFunction(string Name)
  {
    if (!ExpertServer.es.funcIds.ContainsValue((object) Name))
      return;
    int funcId = (int) ExpertServer.es.funcIds[(object) Name];
    ExpertServer.es.funcIds.Remove((object) Name);
    if (ExpertServer.es.funcDatas.ContainsKey((object) Name))
      ExpertServer.es.funcDatas.Remove((object) Name);
    if (!ExpertServer.es.funcHandlers.ContainsKey((object) Name))
      return;
    ExpertServer.es.funcHandlers.Remove((object) Name);
  }

  public static object Invoke(string funcName, ArrayList parms)
  {
    if (!ExpertServer.es.funcHandlers.ContainsKey((object) funcName))
      throw new Exception($"{LocalizationHolder.rm.GetString("Expert_23")}{funcName})");
    return ((FuncHandler) ExpertServer.es.funcHandlers[(object) funcName])(parms);
  }

  public static object Invoke(int Id, ArrayList parms)
  {
    return ExpertServer.es.funcIds.Contains((object) Id) ? ExpertServer.Invoke((string) ExpertServer.es.funcIds[(object) Id], parms) : (object) null;
  }

  public static void RegisterComparer(string Name, CompareFuncHandler cfh)
  {
    if (ExpertServer.es.comparers.ContainsKey((object) Name))
      throw new Exception(LocalizationHolder.rm.GetString("Expert_27") + Name + LocalizationHolder.rm.GetString("Expert_28"));
    ExpertServer.es.comparers.Add((object) Name, (object) cfh);
  }

  public static void UnregisterComparer(string Name)
  {
    if (!ExpertServer.es.comparers.ContainsKey((object) Name))
      return;
    ExpertServer.es.comparers.Remove((object) Name);
  }

  public static int Compare(
    string Name,
    ExpertServer.ExpServTask ti,
    long obj1,
    long obj2,
    HybridRowExp dr1,
    HybridRowExp dr2)
  {
    if (!ExpertServer.es.comparers.ContainsKey((object) Name))
      throw new Exception($"{LocalizationHolder.rm.GetString("Expert_29")}{Name})");
    return ((CompareFuncHandler) ExpertServer.es.comparers[(object) Name])((object) ti, obj1, obj2, dr1, dr2);
  }

  public static void RegisterUserProc(string Name, ScriptProcHandler handler)
  {
    if (ExpertServer.es.procHandlers.ContainsKey((object) Name))
      throw new Exception(LocalizationHolder.rm.GetString("Expert_33") + Name + LocalizationHolder.rm.GetString("Expert_34"));
    ExpertServer.es.procHandlers.Add((object) Name, (object) handler);
  }

  public static void UnregisterUserProc(string Name)
  {
    if (!ExpertServer.es.procHandlers.ContainsKey((object) Name))
      return;
    ExpertServer.es.procHandlers.Remove((object) Name);
  }

  public static void CallProc(
    string procName,
    ExpertServer.ExpServTask ti,
    long[] context,
    HybridTableExp dTable,
    int objType,
    int attrType,
    object Value)
  {
    if (!ExpertServer.es.procHandlers.ContainsKey((object) procName))
      throw new Exception($"{LocalizationHolder.rm.GetString("Expert_35")}{procName})");
    ((ScriptProcHandler) ExpertServer.es.procHandlers[(object) procName])((object) ti, context, dTable, objType, attrType, Value);
  }

  public DocRecord[] GetDocArray(int taskId)
  {
    ExpertServer.ExpServTask taskEx = this.GetTaskEx(taskId);
    return taskEx.docList == null ? (DocRecord[]) null : taskEx.docList.ToArray();
  }

  public DocRecord GetDocRecord(int taskId, int Num)
  {
    ExpertServer.ExpServTask taskEx = this.GetTaskEx(taskId);
    return taskEx.docList == null || taskEx.docList.Count <= Num ? (DocRecord) null : taskEx.docList[Num];
  }

  public byte[] GetDocument(int taskId, int Num)
  {
    ExpertServer.ExpServTask taskEx = this.GetTaskEx(taskId);
    return taskEx.hiddenList == null || taskEx.hiddenList.Count <= Num ? (byte[]) null : taskEx.hiddenList[Num].zippedDoc;
  }

  public void SetDocument(int taskId, Guid sessionGuid, int Num, byte[] doc, int pageCount)
  {
    ExpertServer.ExpServTask taskEx = this.GetTaskEx(taskId);
    if (taskEx.hiddenList == null || Num >= taskEx.hiddenList.Count)
      return;
    ExpertServer.HiddenDocInfo hidden = taskEx.hiddenList[Num];
    hidden.zippedDoc = doc;
    hidden.pageCount = pageCount;
    DocRecord doc1 = taskEx.docList[Num];
    try
    {
      this.SetAlignedDoc(hidden.sDocInfo, sessionGuid, doc1, hidden, Num, true);
    }
    catch
    {
      doc1.state |= DocState.AccessError;
      throw;
    }
  }

  public void ConfirmDocAligned(int taskId, int Num)
  {
    this.GetTaskEx(taskId).docList[Num].state |= DocState.Aligned;
  }

  public byte[] GetTraceInfo(int taskId, int Num)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) null;
    try
    {
      this._CheckTaskId(taskId, out ti);
    }
    catch (EAbort ex)
    {
      throw new ExpertServerException(ex.Message);
    }
    return ti.hiddenList == null || ti.hiddenList.Count <= Num ? (byte[]) null : ti.hiddenList[Num].zippedInfo;
  }

  public static MemoryStream LoadObjectFile(IUserSession ius, long objId)
  {
    MemoryStream memoryStream = new MemoryStream();
    if (ius.GetObject(objId).GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")) is IBlobReader attributeByGuid)
    {
      BlobInformation blobInformation = attributeByGuid.OpenBlob(0);
      try
      {
        byte[] buffer = attributeByGuid.ReadDataBlock((int) blobInformation.RealFileSize);
        memoryStream.Write(buffer, 0, buffer.Length);
      }
      finally
      {
        attributeByGuid.CloseBlob();
      }
    }
    memoryStream.Position = 0L;
    return memoryStream;
  }

  public static MemoryStream LoadObjectFile(IDBObject iO)
  {
    MemoryStream memoryStream = new MemoryStream();
    if (iO.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545")) is IBlobReader attributeByGuid)
    {
      BlobInformation blobInformation = attributeByGuid.OpenBlob(0);
      try
      {
        byte[] buffer = attributeByGuid.ReadDataBlock((int) blobInformation.RealFileSize);
        memoryStream.Write(buffer, 0, buffer.Length);
      }
      finally
      {
        attributeByGuid.CloseBlob();
      }
    }
    memoryStream.Position = 0L;
    return memoryStream;
  }

  public static IDBAttributable GetAttributable(IUserSession ius, long someId, out bool Relation)
  {
    Relation = false;
    IDBObject attributable = ius.GetObject(someId, false);
    if (attributable != null)
      return (IDBAttributable) attributable;
    IDBRelation relation = ius.GetRelation(someId, false);
    if (relation == null)
      return (IDBAttributable) null;
    Relation = true;
    return (IDBAttributable) relation;
  }

  public static IDBAttributable GetAttributable(IUserSession ius, long someId)
  {
    return (IDBAttributable) ius.GetObject(someId, false) ?? (IDBAttributable) ius.GetRelation(someId, false) ?? (IDBAttributable) null;
  }

  public static IDBAttributableType GetAttributableType(IUserSession ius, Guid guid)
  {
    return (IDBAttributableType) ius.GetObjectType(guid, false) ?? (IDBAttributableType) ius.GetRelationType(guid, false) ?? (IDBAttributableType) null;
  }

  public static long AttributableId(IDBAttributable idbA)
  {
    return idbA is IDBObject ? ((IDBObject) idbA).ObjectID : ((IDBRelation) idbA).RelationID;
  }

  public static int AttributableTypeId(IDBAttributableType idbAT)
  {
    return idbAT is IDBObjectType ? ((IDBObjectType) idbAT).ObjectType : ((IDBRelationType) idbAT).RelationType;
  }

  public static Guid AttributableTypeGuid(IDBAttributableType idbAT)
  {
    return idbAT is IDBObjectType ? ((IDBObjectType) idbAT).PropertiesStructure.ObjectTypeGuid : ((IDBRelationType) idbAT).PropertiesStructure.RelationTypeGuid;
  }

  public static string AttributableTypeName(IDBAttributableType idbAT)
  {
    return idbAT is IDBObjectType ? ((IDBObjectType) idbAT).PropertiesStructure.ObjectTypeName : ((IDBRelationType) idbAT).PropertiesStructure.Description;
  }

  public static int GetTypeId(IUserSession ius, IDBAttributable idbA)
  {
    return idbA is IDBObject ? ius.GetObjectType(((IDBObject) idbA).ObjectType).ObjectType : ius.GetRelationType(((IDBRelation) idbA).RelationType).RelationType;
  }

  public static int GetTypeId(IUserSession ius, long Id)
  {
    QuickObjectInfo objectInfo = ius.GetObjectInfo(Id);
    if (!objectInfo.Empty)
      return objectInfo.ObjectTypeID;
    IDBRelation relation = ius.GetRelation(Id, false);
    return relation != null ? relation.RelationType : -1;
  }

  public static MeasuredValue MeasureMultiple(MeasuredValue val1, MeasuredValue val2)
  {
    long measureId1 = val1.MeasureID;
    long measureId2 = val2.MeasureID;
    if (measureId1 == 0L)
      return new MeasuredValue(val1.Value * val2.Value, measureId2);
    return measureId2 == 0L ? new MeasuredValue(val1.Value * val2.Value, measureId1) : MeasureHelper.Multiply(val1, val2);
  }

  public static MeasuredValue MeasureDivide(MeasuredValue val1, MeasuredValue val2)
  {
    long measureId1 = val1.MeasureID;
    long measureId2 = val2.MeasureID;
    long num1 = measureId1;
    long num2 = measureId2;
    if (measureId1 != 0L)
      num1 = MeasureHelper.FindBaseValue(MeasureHelper.FindDescriptor(measureId1)).MeasureID;
    if (measureId2 != 0L)
      num2 = MeasureHelper.FindBaseValue(MeasureHelper.FindDescriptor(measureId2)).MeasureID;
    if (measureId2 == 0L)
      return new MeasuredValue(val1.Value / val2.Value, measureId1);
    return num1 == num2 ? new MeasuredValue(val1.Value / val2.Value, 0L) : MeasureHelper.Divide(val1, val2);
  }

  public static MeasuredValue MeasureSum(MeasuredValue val1, MeasuredValue val2)
  {
    if (val1.MeasureID == 0L)
      return new MeasuredValue(val1.Value + val2.Value, val2.MeasureID);
    if (val2.MeasureID == 0L)
      return new MeasuredValue(val1.Value + val2.Value, val1.MeasureID);
    try
    {
      return MeasureHelper.Add(val1, val2);
    }
    catch (KernelExceptionID ex)
    {
      long measureId;
      if (val1.MeasureID == ExpertConsts.Consts.measureShtuk)
        measureId = val2.MeasureID;
      else if (val2.MeasureID == ExpertConsts.Consts.measureShtuk)
        measureId = val1.MeasureID;
      else
        throw;
      return new MeasuredValue(val1.Value + val2.Value, measureId);
    }
  }

  public static MeasuredValue MeasureSubtract(MeasuredValue val1, MeasuredValue val2)
  {
    return MeasureHelper.Substract(val1, val2);
  }

  public static bool IsTypeDescendant(int rootTypeID, int childTypeID)
  {
    return rootTypeID == childTypeID || MetaDataHelper.GetObjectTypeParentsID(childTypeID).IndexOf(rootTypeID) >= 0;
  }

  public static bool IsTypeDescendant(List<int> rootTypeList, int childTypeID)
  {
    if (rootTypeList.Contains(childTypeID))
      return true;
    foreach (int num in MetaDataHelper.GetObjectTypeParentsID(childTypeID))
    {
      if (rootTypeList.Contains(num))
        return true;
    }
    return false;
  }

  public static bool IsTypeDescendant(Guid rootTypeGUID, Guid childTypeGUID)
  {
    return rootTypeGUID.Equals(childTypeGUID) || MetaDataHelper.GetObjectTypeParentsGuid(childTypeGUID).IndexOf(rootTypeGUID) >= 0;
  }

  public static bool IsTypeDescendant(Guid rootTypeGUID, int childTypeID)
  {
    return MetaDataHelper.GetObjectTypeID(rootTypeGUID) == childTypeID || MetaDataHelper.GetObjectTypeParentsGuid(childTypeID).IndexOf(rootTypeGUID) >= 0;
  }

  public IDBObject CreateExpertFormula(
    Guid sessionGuid,
    object ap,
    string resAttrGuid,
    string resObjTypeGuid,
    string Name,
    object cond,
    object form)
  {
    IUserSession session = ExpertServer._CheckGetSession(sessionGuid);
    IExpertFormula expertFormula = (IExpertFormula) session.GetObjectCollection(ExpertConsts.Consts.objFormula).Create();
    TempFormula tempFormula = (TempFormula) cond;
    TempFormula tf = (TempFormula) form;
    if (cond != null)
      expertFormula.Cond = tempFormula;
    expertFormula.Result = (AttribPair) ap;
    expertFormula.resAttrGuid = resAttrGuid;
    expertFormula.resObjTypeGuid = resObjTypeGuid;
    expertFormula.Name = Name;
    expertFormula.UpdateObject(tf);
    expertFormula.CommitCreation(true);
    byte[] traceInfo = (byte[]) null;
    this.ReflectObjUpdate(session.SessionGUID, expertFormula.ObjectID, ExpertTraceFlags.None, (TempFormula) null, out traceInfo);
    return (IDBObject) expertFormula;
  }

  public static TempFormula ReadFormula(IUserSession ius, long objID, Guid attrGuid)
  {
    IDBObject dbObject = ius.GetObject(objID);
    if (dbObject == null)
      return (TempFormula) null;
    TempFormula tempFormula = (TempFormula) null;
    if (dbObject.GetAttributeByGuid(attrGuid) is IBlobReader attributeByGuid)
    {
      BlobInformation blobInformation = attributeByGuid.OpenBlob(0);
      if (blobInformation.RealFileSize > 0L)
      {
        try
        {
          byte[] zipScr = attributeByGuid.ReadDataBlock((int) blobInformation.RealFileSize);
          if (zipScr.Length != 0)
          {
            tempFormula = new TempFormula((XmlNode) ZlibHelper.UnpackXmlBuffer(zipScr).DocumentElement);
            tempFormula.FixInfixForm(ius);
          }
        }
        finally
        {
          attributeByGuid.CloseBlob();
        }
      }
    }
    return tempFormula;
  }

  public static long GetCheckSum(byte[] buffer)
  {
    ExpertServer.checksummer.Reset();
    ExpertServer.checksummer.Update(buffer);
    return ExpertServer.checksummer.Value;
  }

  public static DataTable GetAllRelations(IUserSession ius, int relType, long rootID)
  {
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrDopCompTag, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
    });
    long projId = rootID;
    IUserSession userSession = ius;
    List<int> relations = new List<int>();
    relations.Add(relType);
    DBRecordSetParams dbrsp = dbRecordSetParams;
    return DataHelper.GetChildSostavData(projId, userSession, (IEnumerable<int>) relations, false, dbrsp);
  }

  public static DataTable GetAllRelationsByType(
    IUserSession ius,
    int relType,
    long rootID,
    int[] types)
  {
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) types, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
    }, new ColumnDescriptor[4]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ExpertConsts.Consts.attrDopCompTag, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
    });
    long projId = rootID;
    IUserSession userSession = ius;
    List<int> relations = new List<int>();
    relations.Add(relType);
    DBRecordSetParams dbrsp = dbRecordSetParams;
    return DataHelper.GetChildSostavData(projId, userSession, (IEnumerable<int>) relations, false, dbrsp);
  }

  private static HybridColumnsExp.HybridColumnExp CopyColumn(HybridColumnsExp.HybridColumnExp col)
  {
    return new HybridColumnsExp.HybridColumnExp(col.ColumnName, col.DataType)
    {
      attrTypeId = col.attrTypeId,
      fldType = col.fldType
    };
  }

  private static HybridColumnsExp.HybridColumnExp CopyColumn(DataColumn col)
  {
    return new HybridColumnsExp.HybridColumnExp(col.ColumnName, col.DataType);
  }

  private static void CopyRow(HybridTableExp dst, HybridRowExp row)
  {
    HybridRowExp hrow = dst.NewRow();
    HybridColumnsExp columns = row.Columns;
    for (int index = 0; index < dst.Columns.Count; ++index)
    {
      HybridColumnsExp.HybridColumnExp column = dst.Columns[index];
      int indexByName = columns.GetIndexByName(column.ColumnName);
      if (indexByName >= 0)
        hrow[index] = row[indexByName];
    }
    dst.Add(hrow);
  }

  private static List<Triple> GetTNode(ScriptTreeNode root)
  {
    if (root == null)
      return (List<Triple>) null;
    if (root.opTag == ExpertScriptOp.opSetting && root.op is OpParmSetting && (root.op as OpParmSetting).listTable != null)
      return (root.op as OpParmSetting).listTable;
    foreach (ScriptTreeNode root1 in root.Items)
    {
      List<Triple> tnode = ExpertServer.GetTNode(root1);
      if (tnode != null)
        return tnode;
    }
    return (List<Triple>) null;
  }

  private static List<Triple> GetTableNode(ScriptTreeNode root)
  {
    while (root.parent != null)
      root = root.parent;
    for (int index = 0; index < root.Items.Count; ++index)
    {
      List<Triple> tnode = ExpertServer.GetTNode((ScriptTreeNode) root.Items[index]);
      if (tnode != null)
        return tnode;
    }
    return (List<Triple>) null;
  }

  private static string GetDocName(
    IUserSession ius,
    ExpertServer.ExpServTask ti,
    long contId,
    string nameStr)
  {
    IDBObject dbObject = ius.GetObject(contId);
    if (dbObject == null)
      return nameStr;
    StringBuilder stringBuilder1 = new StringBuilder();
    int startIndex = 0;
    while (startIndex < nameStr.Length)
    {
      int num1 = nameStr.IndexOf('{', startIndex);
      int num2 = -1;
      if (num1 >= 0)
        num2 = nameStr.IndexOf('}', num1 + 1);
      if (num1 < 0 || num2 < 0)
      {
        stringBuilder1.Append(nameStr.Substring(startIndex));
        break;
      }
      if (num1 > startIndex)
        stringBuilder1.Append(nameStr.Substring(startIndex, num1 - startIndex));
      string str1 = nameStr.Substring(num1 + 1, num2 - num1 - 1);
      startIndex = num2 + 1;
      DateTime now;
      switch (str1)
      {
        case "DATE":
          StringBuilder stringBuilder2 = stringBuilder1;
          now = DateTime.Now;
          string longDateString = now.ToLongDateString();
          stringBuilder2.Append(longDateString);
          continue;
        case "TIME":
          StringBuilder stringBuilder3 = stringBuilder1;
          now = DateTime.Now;
          string str2 = now.ToString("HH-mm");
          stringBuilder3.Append(str2);
          continue;
        default:
          IDBAttributeType attributeType = ius.GetAttributeType(str1, false);
          bool flag1 = false;
          bool flag2 = false;
          if (attributeType != null)
          {
            Guid g1 = new Guid("cadd9366-306c-11d8-b4e9-00304f19f545");
            Guid g2 = new Guid("cadd9367-306c-11d8-b4e9-00304f19f545");
            AttributeTypeProperties propertiesStructure = attributeType.PropertiesStructure;
            flag1 = propertiesStructure.AttributeGuid.Equals(g1);
            propertiesStructure = attributeType.PropertiesStructure;
            flag2 = propertiesStructure.AttributeGuid.Equals(g2);
          }
          if (flag1)
          {
            stringBuilder1.Append(ius.UserName);
            continue;
          }
          if (flag2)
          {
            stringBuilder1.Append(Convert.ToString(ius.UserID));
            continue;
          }
          object[] valuesByName = dbObject.GetValuesByName(str1, false);
          if (valuesByName != null && valuesByName.Length != 0)
          {
            stringBuilder1.Append(Convert.ToString(valuesByName[0]));
            continue;
          }
          try
          {
            int attributeId = MetaDataHelper.GetAttributeID((object) str1);
            object parm = ExpertServer.es.InnerGetParm(ti, attributeId);
            if (parm != null)
            {
              stringBuilder1.Append(parm.ToString());
              continue;
            }
            continue;
          }
          catch
          {
            continue;
          }
      }
    }
    return stringBuilder1.ToString();
  }

  public static DataRow GetImbaseData(
    IUserSession ius,
    IDBObject idbO,
    List<int> attrIds,
    out List<int> indexes)
  {
    DataRow imbaseData = (DataRow) null;
    IImbaseServer iis = ExpertServer.es.iis;
    indexes = new List<int>();
    long num = -1;
    long recordId = -1;
    Guid sessionGuid = ius.SessionGUID;
    Guid guid = idbO.GUID;
    ref long local1 = ref num;
    ref long local2 = ref recordId;
    if (iis.GetPrototypeDetails(sessionGuid, guid, ref local1, ref local2))
    {
      imbaseData = ImbaseServer.GetRecordRow(ius, num, recordId);
      for (int index = 0; index < attrIds.Count; ++index)
        indexes.Add(-1);
      for (int index1 = 0; index1 < imbaseData.Table.Columns.Count; ++index1)
      {
        int int32 = Convert.ToInt32(imbaseData.Table.Columns[index1].ColumnName);
        int index2 = attrIds.IndexOf(int32);
        if (index2 >= 0)
          indexes[index2] = index1;
      }
    }
    else if (num != -1L && num != idbO.ObjectID)
    {
      IDBObject dbObject = ius.GetObject(num, false);
      if (dbObject != null)
      {
        AttributeValues[] attributesValues = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeDescriptions);
        for (int index = attrIds.Count - 1; index >= 0; --index)
        {
          int attrId = attrIds[index];
          bool flag = false;
          foreach (AttributeValues attributeValues in attributesValues)
          {
            flag = attributeValues.AttributeID == attrId;
            if (flag)
              break;
          }
          if (!flag)
            attrIds.RemoveAt(index);
        }
        DataTable dataTable = new DataTable();
        foreach (AttributeValues attributeValues in attributesValues)
        {
          if (attrIds.Contains(attributeValues.AttributeID))
            dataTable.Columns.Add(Convert.ToString(attributeValues.AttributeID), DataTypeConvertor.FieldType2DataType(attributeValues.AttributeType, attributeValues.AttributeID));
        }
        DataRow dataRow = dataTable.NewRow();
        foreach (AttributeValues attributeValues in attributesValues)
        {
          if (attrIds.Contains(attributeValues.AttributeID))
            dataRow[Convert.ToString(attributeValues.AttributeID)] = attributeValues.Values[0];
        }
        dataTable.AcceptChanges();
        for (int index = 0; index < attrIds.Count; ++index)
          indexes.Add(index);
      }
    }
    return imbaseData;
  }

  protected List<string> _CheckExpertObjects()
  {
    List<string> errList = new List<string>();
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(ExpertObjGUIDs.ExpertObject));
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert._CheckExpertObjects");
    try
    {
      foreach (int num in childrenIdRecursive)
      {
        ConditionStructure[] conditions = new ConditionStructure[1]
        {
          new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.Equal, (object) num, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text)
        };
        ColumnDescriptor[] columns = new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 1)
        };
        foreach (DataRow row in (InternalDataCollectionBase) DataHelper.GetObjectData(num, sessionTemporaryClone, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns).Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          this.CheckExpertObject(num, int64, sessionTemporaryClone, errList);
        }
      }
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert._CheckExpertObjects");
    }
    return errList;
  }

  protected bool CheckExpertObject(
    int objType,
    long objID,
    IUserSession ius,
    List<string> errList)
  {
    IDBObject dbObject = ius.GetObject(objID, false);
    if (dbObject == null)
    {
      errList.Add(string.Format(LocalizationHolder.rm.GetString("Expert.Server_262"), (object) objID));
      return false;
    }
    string str1 = MetaDataHelper.GetObjectType(objType).ObjectName + " ";
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(ExpertConsts.Consts.attrAttrGUIDs);
    if (attributeById1 != null && attributeById1.ValuesCount != 0)
    {
      DataTable table = ((UserSession) ius).DBCache.GetTable("IMS_ATTRIBUTES");
      foreach (object obj in attributeById1.Values)
      {
        if (!obj.IsDBNull())
        {
          string text = Convert.ToString(obj);
          if (!GuidHelper.IsGuid(text))
            errList.Add(str1 + string.Format(LocalizationHolder.rm.GetString("Expert.Server_266"), (object) objID, (object) text));
          else if (table.Select($"F_GUID='{text}'").Length == 0)
            errList.Add(str1 + string.Format(LocalizationHolder.rm.GetString("Expert.Server_263"), (object) objID, (object) text));
        }
      }
    }
    IDBAttribute attributeById2 = dbObject.GetAttributeByID(ExpertConsts.Consts.attrObjTypeGUIDs);
    if (attributeById2 != null && attributeById2.ValuesCount != 0)
    {
      DataTable table = ((UserSession) ius).DBCache.GetTable("IMS_OBJECT_TYPES");
      foreach (object obj in attributeById2.Values)
      {
        if (!obj.IsDBNull())
        {
          string str2 = Convert.ToString(obj);
          if (!(str2.Trim() == ""))
          {
            if (!GuidHelper.IsGuid(str2))
              errList.Add(str1 + string.Format(LocalizationHolder.rm.GetString("Expert.Server_266"), (object) objID, (object) str2));
            else if (!new Guid(str2).Equals(Guid.Empty) && table.Select($"F_GUID='{str2}'").Length == 0)
              errList.Add(str1 + string.Format(LocalizationHolder.rm.GetString("Expert.Server_264"), (object) objID, (object) str2));
          }
        }
      }
    }
    IDBAttribute attributeById3 = dbObject.GetAttributeByID(ExpertConsts.Consts.attrObjLinkIDs);
    if (attributeById3 != null)
    {
      foreach (object obj in attributeById3.Values)
      {
        if (!obj.IsDBNull())
        {
          long int64 = Convert.ToInt64(obj);
          if (ius.GetObjectInfo(int64).Empty)
            errList.Add(str1 + string.Format(LocalizationHolder.rm.GetString("Expert.Server_265"), (object) objID, (object) int64));
        }
      }
    }
    return true;
  }

  public void iel_AfterExcludeAttributeFromGroup(IDBAttributesGroup sender, int attributeID)
  {
    if (sender.GroupID == ExpertConsts.Consts.tempAttrGroup)
      this.TempAttrsChanged(attributeID, false, false);
    if (sender.GroupID != ExpertConsts.Consts.tempAttrObjGroup)
      return;
    this.TempAttrsChanged(attributeID, true, false);
  }

  public void iel_AfterIncludeAttributeToGroup(IDBAttributesGroup sender, int attributeID)
  {
    if (sender.GroupID == ExpertConsts.Consts.tempAttrGroup)
      this.TempAttrsChanged(attributeID, false, true);
    if (sender.GroupID != ExpertConsts.Consts.tempAttrObjGroup)
      return;
    this.TempAttrsChanged(attributeID, true, true);
  }

  internal void TempAttrsChanged(int attrId, bool withObject, bool Included)
  {
    foreach (ExpertServer.ExpServTask expServTask in (IEnumerable<ExpertServer.ExpServTask>) this.taskList.Values)
    {
      lock (expServTask)
      {
        HashSet<int> intSet = withObject ? expServTask.tempAttrsWithObject : expServTask.tempAttrsWithoutObject;
        if (Included)
        {
          if (intSet == null)
            intSet = !withObject ? (expServTask.tempAttrsWithoutObject = new HashSet<int>()) : (expServTask.tempAttrsWithObject = new HashSet<int>());
          if (!intSet.Contains(attrId))
            intSet.Add(attrId);
        }
        else if (intSet != null)
        {
          if (intSet.Contains(attrId))
            intSet.Remove(attrId);
        }
      }
    }
  }

  internal void AttributesCombined(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode)
  {
    int attributeId1 = fromAttribute.AttributeID;
    int attributeId2 = toAttribute.AttributeID;
    string lower = fromAttribute.GUID.ToString().ToLower();
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objObject);
    childrenIdRecursive.Remove(ExpertConsts.Consts.objObject);
    childrenIdRecursive.Remove(ExpertConsts.Consts.objBaseScript);
    childrenIdRecursive.Remove(ExpertConsts.Consts.objBaseFormula);
    IUserSession sessionTemporaryClone = ((IDBTimedEvents) this._serviceProvider.GetService(typeof (IDBTimedEvents))).GetSystemSessionTemporaryClone("Expert.AttributesCombined");
    (sessionTemporaryClone as UserSession).StartTransaction();
    try
    {
      foreach (int objectType in childrenIdRecursive)
      {
        DataTable dataTable = sessionTemporaryClone.GetObjectCollection(objectType).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1)
        }));
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (!row[0].IsDBNull())
            {
              long int64 = Convert.ToInt64(row[0]);
              if (sessionTemporaryClone.GetObjectActualCopy(Math.Abs(int64), false) is IExpertObject expertObject)
              {
                object[] valuesById = expertObject.GetValuesByID(ExpertConsts.Consts.attrAttrGUIDs, false);
                if (valuesById != null)
                {
                  int index1 = -1;
                  for (int index2 = 0; index2 < valuesById.Length; ++index2)
                  {
                    if (!valuesById[index2].IsDBNull() && ((string) valuesById[index2]).ToLower() == lower)
                    {
                      index1 = index2;
                      break;
                    }
                  }
                  bool flag1 = false;
                  IDBAttribute attributeById = expertObject.GetAttributeByID(ExpertConsts.Consts.attrResAttrGUID);
                  if (attributeById != null && attributeById.Value.NotDBNull() && Convert.ToString(attributeById.Value).ToLower() == lower)
                    flag1 = true;
                  if (index1 >= 0 || flag1)
                  {
                    bool flag2 = false;
                    if (expertObject.ObjectModifyMode == ObjectModifyModes.Checkout)
                    {
                      expertObject = expertObject.CheckOut() as IExpertObject;
                      flag2 = true;
                    }
                    try
                    {
                      expertObject.Load();
                      expertObject.ReplaceAttr(fromAttribute, toAttribute, session, combineMode);
                      if (index1 >= 0)
                      {
                        valuesById[index1] = (object) toAttribute.GUID.ToString();
                        expertObject.SetAttributesValues(new AttributeValues[1]
                        {
                          new AttributeValues(ExpertConsts.Consts.attrAttrGUIDs, (object) valuesById)
                        });
                      }
                    }
                    finally
                    {
                      if (flag2)
                        expertObject.CheckIn();
                    }
                  }
                }
              }
            }
          }
        }
      }
      (sessionTemporaryClone as UserSession).Commit();
    }
    catch
    {
      (sessionTemporaryClone as UserSession).Rollback();
      throw;
    }
    finally
    {
      sessionTemporaryClone?.Logout("Expert.AttributesCombined");
    }
  }

  internal void GetUsedAttrs(IUserSession session, UsedAttributesEventArgs args)
  {
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objObject);
    childrenIdRecursive.Remove(ExpertConsts.Consts.objObject);
    childrenIdRecursive.Remove(ExpertConsts.Consts.objBaseScript);
    childrenIdRecursive.Remove(ExpertConsts.Consts.objBaseFormula);
    HashSet<int> intSet = new HashSet<int>();
    foreach (int objectType in childrenIdRecursive)
    {
      DataTable attributeValues = session.GetObjectCollection(objectType).GetAttributeValues(ExpertConsts.Consts.attrAttrGUIDs, false);
      if (attributeValues != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) attributeValues.Rows)
        {
          object obj = row["F_STRING_VALUE"];
          if (!obj.IsNullOrDBNull() && GuidHelper.IsGuid(obj.ToString()))
          {
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(obj.ToString()));
            intSet.Add(attributeTypeId);
          }
        }
      }
    }
    foreach (int objectType in MetaDataHelper.GetObjectTypeChildrenIDRecursive(ExpertConsts.Consts.objFormula))
    {
      DataTable attributeValues = session.GetObjectCollection(objectType).GetAttributeValues(ExpertConsts.Consts.attrResAttrGUID, false);
      if (attributeValues != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) attributeValues.Rows)
        {
          object obj = row["F_STRING_VALUE"];
          if (!obj.IsNullOrDBNull() && GuidHelper.IsGuid(obj.ToString()))
          {
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid(obj.ToString()));
            intSet.Add(attributeTypeId);
          }
        }
      }
    }
    foreach (int attrID in intSet)
      args.AddAttribute(attrID);
  }

  public void Dispose() => this.Dispose(true);

  private void Dispose(bool disposing)
  {
    if (!this.disposed && disposing && this.cleanCachesTimer != null)
      this.cleanCachesTimer.Dispose();
    this.disposed = true;
  }

  ~ExpertServer() => this.Dispose(false);

  internal bool abortedTasksContains(int taskId)
  {
    bool flag = this.abortedLock.TryEnterReadLock(1000);
    try
    {
      return this.abortedTasks.Contains(taskId);
    }
    finally
    {
      if (flag)
        this.abortedLock.ExitReadLock();
    }
  }

  internal void abortedTasksAdd(int taskId)
  {
    bool flag = this.abortedLock.TryEnterWriteLock(1000);
    try
    {
      this.abortedTasks.Add(taskId);
    }
    finally
    {
      if (flag)
        this.abortedLock.ExitWriteLock();
    }
  }

  public int _StartTask(Guid sessionGUID) => this._StartTask(sessionGUID, ExpertTraceFlags.None);

  public int _StartTask(Guid sessionGUID, ExpertTraceFlags traceFlags)
  {
    int num = Interlocked.Increment(ref this.taskIdGenerator);
    ExpertServer.ExpServTask expServTask = new ExpertServer.ExpServTask(num, sessionGUID, traceFlags);
    this.taskList.GetOrAdd(num, expServTask);
    return num;
  }

  internal void _CheckTaskId(int taskId, out ExpertServer.ExpServTask ti)
  {
    if (this.abortedTasksContains(taskId))
      throw new EAbort(ExpertResult.WrongTaskId, LocalizationHolder.rm.GetString("Expert.Server_243"));
    if (!this.taskList.TryGetValue(taskId, out ti))
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_1"));
  }

  internal ExpertServer.ExpServTask GetTask(int taskId)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) null;
    try
    {
      this._CheckTaskId(taskId, out ti);
    }
    catch (EAbort ex)
    {
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_243"));
    }
    return ti;
  }

  internal ExpertServer.ExpServTask GetTaskEx(int taskId)
  {
    ExpertServer.ExpServTask ti = (ExpertServer.ExpServTask) null;
    try
    {
      this._CheckTaskId(taskId, out ti);
    }
    catch (EAbort ex)
    {
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_243"));
    }
    return ti;
  }

  public void _EndTask(int taskId)
  {
    ExpertServer.ExpServTask ti;
    if (this.abortedTasksContains(taskId) || !this.taskList.TryGetValue(taskId, out ti))
      return;
    ti.ReleaseServices();
    if (ti.thread != null)
      ti.thread.Abort();
    ExpertServer.ExpServTask expServTask = (ExpertServer.ExpServTask) null;
    this.taskList.TryRemove(taskId, out expServTask);
    this.EndJobForTask(ti);
    this.abortedTasksAdd(taskId);
  }

  public void _ChangeSession(int taskId, Guid sessionGuid)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer._CheckGetSession(sessionGuid);
    ExpertServer.ExpServTask expServTask;
    if (!this.taskList.TryGetValue(taskId, out expServTask))
      return;
    lock (expServTask)
    {
      if (expServTask.thread != null)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_2"));
      expServTask.sessionGUID = sessionGuid;
    }
  }

  public void AbortProcess(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.taskList[taskId];
    lock (task)
      task.aborting = true;
    this.abortedTasksAdd(taskId);
  }

  internal void StartSystemTask(Guid sessionGuid, ExpertTraceFlags flags)
  {
    this.servTask.sessionGUID = sessionGuid;
    this.servTask.traceFlags = flags;
    Monitor.Enter((object) this.servTask);
  }

  internal void EndSystemTask() => Monitor.Exit((object) this.servTask);

  private bool IsJobRunning(ExpertServer.ExpServTask ti) => ti.thread != null;

  private bool IsJobRunning(int taskId) => this.IsJobRunning(this.GetTask(taskId));

  public bool IsJobAborting(ExpertServer.ExpServTask ti)
  {
    if (!ti.aborting)
      return false;
    this.EndJobForTask(ti);
    return true;
  }

  private ExpertServer.ExpServTask StartJobForTask(int taskId, bool needClone = true)
  {
    return this.StartJobForTask(this.GetTask(taskId), needClone);
  }

  private ExpertServer.ExpServTask StartJobForTask(ExpertServer.ExpServTask ti, bool needClone = true)
  {
    lock (ti)
    {
      ti.thread = ti.thread == null ? Thread.CurrentThread : throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_9"));
      ti.NeededAttrs.Clear();
      ti.aborting = false;
      this.abortedTasks.Remove(ti.taskId);
      if (needClone)
      {
        IServerSession session = ExpertServer._CheckGetSession(ti.sessionGUID) as IServerSession;
        IServerSession serverSession = session.Clone(true, "ExpertTask" + ti.thread.ManagedThreadId.ToString()) as IServerSession;
        ti.clonedSessionGUID = serverSession.SessionGUID;
        serverSession.DBObjectsCacheStart();
        if (session.IsStartedLogHistory)
          serverSession.StartLogHistory();
      }
      else
        ti.clonedSessionGUID = ti.sessionGUID;
    }
    return ti;
  }

  private void EndJobForTask(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      return;
    this.EndJobForTask(task);
  }

  private void EndJobForTask(ExpertServer.ExpServTask ti)
  {
    lock (ti)
    {
      ti.thread = (Thread) null;
      if (ti.clonedSessionGUID != ti.sessionGUID && ti.clonedSessionGUID != Guid.Empty)
      {
        this.FreeMyVerRules(ti);
        IUserSession sessionById = UserSession.GetSessionByID(ti.clonedSessionGUID);
        sessionById.DBObjectsCacheStop();
        if (sessionById.IsStartedLogHistory)
        {
          sessionById.StopLogHistory();
          if (ExpertServer._CheckGetSession(ti.sessionGUID) is UserSession session)
          {
            foreach (CategoryValue modificationsHistory in sessionById.GetModificationsHistoryList())
              session.AddToModificationsHistory(modificationsHistory);
          }
        }
        sessionById.Logout("ExpertTask" + Thread.CurrentThread.ManagedThreadId.ToString());
      }
      ti.clonedSessionGUID = Guid.Empty;
      ti.curNode = (XmlNode) ti.traceInfo.DocumentElement;
    }
  }

  private IUserSession GetSession(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return (IUserSession) null;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    return task != null ? this.GetSession(task) : (IUserSession) null;
  }

  public IUserSession GetSession(ExpertServer.ExpServTask ti)
  {
    lock (ti)
      return ti.GetSession();
  }

  public static Guid GetSessionGuid(ExpertServer.ExpServTask ti)
  {
    IUserSession session = ti.GetSession();
    return session == null ? Guid.Empty : session.SessionGUID;
  }

  private Guid GetSessionGuid(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return Guid.Empty;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    return task != null ? ExpertServer.GetSessionGuid(task) : Guid.Empty;
  }

  public void _SetTraceFlags(int taskId, ExpertTraceFlags traceFlags)
  {
    if (this.abortedTasksContains(taskId))
      return;
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_3"));
    lock (task)
      task.traceFlags = traceFlags;
  }

  public ExpertTraceFlags _GetTraceFlags(int taskId)
  {
    if (this.abortedTasksContains(taskId))
      return ExpertTraceFlags.None;
    ExpertServer.ExpServTask task = this.taskList[taskId];
    if (task == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_4"));
    lock (task)
      return task.traceFlags;
  }

  public byte[] _GetTraceInfo(int taskId)
  {
    ExpertServer.ExpServTask task;
    try
    {
      task = this.GetTask(taskId);
      if (task == null)
        return (byte[]) null;
    }
    catch (EAbort ex)
    {
      return (byte[]) null;
    }
    return task.GetPackedInfo();
  }

  internal static IUserSession _CheckGetSession(Guid sessionGUID)
  {
    return UserSession.GetSessionByID(sessionGUID) ?? throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_5"));
  }

  public bool FlagIn(ExpertTraceFlags a, ExpertTraceFlags b) => (a & b) == a;

  internal XmlElement AddTraceElement(int taskId, string name)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null || task.traceInfo == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_6"));
    if (!task.makeTrace)
      return (XmlElement) null;
    lock (task)
    {
      if (task.blockTrace > 0)
        return (XmlElement) null;
      XmlElement element = task.traceInfo.CreateElement(name);
      task.curNode.AppendChild((XmlNode) element);
      return element;
    }
  }

  internal XmlElement AddTraceGroup(int taskId, string name)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null || task.traceInfo == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_7"));
    if (!task.makeTrace)
      return (XmlElement) null;
    lock (task)
    {
      if (task.blockTrace > 0)
        return (XmlElement) null;
      XmlElement element = task.traceInfo.CreateElement(name);
      task.curNode.AppendChild((XmlNode) element);
      task.curNode = (XmlNode) element;
      return element;
    }
  }

  internal ExpertServer.ExpServTask StartModifyTrace(int taskId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    if (task == null)
      throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_8"));
    Monitor.Enter((object) task);
    return task;
  }

  internal void EndModifyTrace(ExpertServer.ExpServTask ti) => Monitor.Exit((object) ti);

  private void _SetVersionRuleOwnerId(int taskId, string versionRuleOwnerId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
    {
      task.verRuleOwnerId = versionRuleOwnerId;
      IUserSession session = this.GetSession(task);
      this.ReportVerRule(taskId, session, versionRuleOwnerId);
    }
  }

  public string _GetVersionRuleOwnerId(int taskId) => this.GetTask(taskId).verRuleOwnerId;

  private void _SetVersionRule(int taskId, VersionsRule rule)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
      task.curFiltrationRule = rule;
  }

  public VersionsRule _GetVersionRule(int taskId) => this.GetTask(taskId).curFiltrationRule;

  private void _SetEditingContext(int taskId, long editingContextId)
  {
    ExpertServer.ExpServTask task = this.GetTask(taskId);
    lock (task)
      task.editingContextID = editingContextId;
  }

  public long _GetEditingContext(int taskId) => this.GetTask(taskId).editingContextID;

  private void FreeMyVerRules(ExpertServer.ExpServTask ti)
  {
    lock (ti)
    {
      if (ti.RulesList == null || ti.RulesList.Count == 0)
        return;
      IVersionRulesCacheService service = (IVersionRulesCacheService) this._serviceProvider.GetService(typeof (IVersionRulesCacheService));
      foreach (long key in ti.RulesList.Keys)
        service.DeleteRuleTuning((object) ExpertServer.GetSessionGuid(ti), key);
      ti.RulesList.Clear();
    }
  }

  internal class Calculator
  {
    private TempFormula tf;
    internal int curCmd;
    internal List<DataType> typeStack;
    internal List<object> valueStack;
    internal List<int> attrStack;
    internal ExpertServer.ExpServTask ti;
    private int taskId;
    private IUserSession ius;
    private long objID = -1;
    private long[] objIDs;
    private long refObjId = -1;
    private IDBAttributable idbO;
    private HybridRowExp row;
    private ExpertServer expServ;
    public bool calcCond;
    public ExpertResult calcRes = ExpertResult.OK;
    public long relationId;
    private readonly string minus = LocalizationHolder.rm.GetString("Expert.Server_289");
    internal readonly string DoublePattern = "-?(\\d?.?\\d*)E([+-]\\d+)";

    public Calculator(
      ExpertServer.ExpServTask est,
      long[] objID,
      HybridRowExp row,
      TempFormula tf)
    {
      this.expServ = ExpertServer.es;
      this.ti = est;
      this.tf = tf;
      this.taskId = est.taskId;
      this.ius = est.GetSession();
      this.objID = objID[0];
      this.objIDs = objID;
      this.row = row;
      this.typeStack = new List<DataType>();
      this.valueStack = new List<object>();
      this.attrStack = new List<int>();
    }

    public Calculator(
      ExpertServer es,
      int taskId,
      long[] objID,
      HybridRowExp row,
      TempFormula tf)
    {
      this.expServ = es;
      this.tf = tf;
      this.taskId = taskId;
      this.objID = objID[0];
      this.objIDs = objID;
      this.row = row;
      this.ti = ExpertServer.es.GetTask(taskId);
      this.ius = this.ti.GetSession();
      this.typeStack = new List<DataType>();
      this.valueStack = new List<object>();
      this.attrStack = new List<int>();
    }

    internal bool LoadObject()
    {
      if (this.idbO != null)
        return true;
      if (this.objID == -1L)
        return false;
      this.idbO = this.GetObjectQuiet(this.objID);
      if (this.idbO != null)
        return true;
      this.objID = -1L;
      return false;
    }

    internal bool LoadObjectForType(int oType)
    {
      foreach (long objId in this.objIDs)
      {
        if (objId != -1L)
        {
          this.idbO = this.GetObjectQuiet(objId);
          int typeId = ExpertServer.GetTypeId(this.ius, this.idbO);
          if (this.idbO != null && ExpertServer.IsTypeDescendant(oType, typeId))
            return true;
        }
      }
      this.idbO = (IDBAttributable) null;
      return false;
    }

    internal IDBAttributable GetObjectQuiet(long objId)
    {
      try
      {
        return ExpertServer.GetAttributable(this.ius, objId);
      }
      catch
      {
        return (IDBAttributable) null;
      }
    }

    internal DataType topType
    {
      get => this.typeStack[this.typeStack.Count - 1];
      set => this.typeStack[this.typeStack.Count - 1] = value;
    }

    internal object topValue
    {
      get => this.valueStack[this.valueStack.Count - 1];
      set => this.valueStack[this.valueStack.Count - 1] = value;
    }

    internal DataType firstType => this.typeStack[this.typeStack.Count - 2];

    internal object firstValue => this.valueStack[this.valueStack.Count - 2];

    internal void Pop(IList il)
    {
      if (il.Count <= 0)
        return;
      il.RemoveAt(il.Count - 1);
    }

    internal void AddZeroAttr() => this.attrStack.Add(-1);

    public object Perform()
    {
      this.typeStack.Clear();
      this.valueStack.Clear();
      this.attrStack.Clear();
      this.calcRes = ExpertResult.OK;
      this.curCmd = 0;
      try
      {
        while (this.curCmd < this.tf.postfixForm.Count)
          this.PerformCmd();
      }
      catch (EAbort ex)
      {
        this.calcRes = ex.res;
        return (object) null;
      }
      if (this.valueStack.Count <= 0 || this.valueStack.Count > 1)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_60"));
      if (this.topValue is MeasuredValue && ((MeasuredValue) this.topValue).MeasureID == 0L && this.tf.resType != DataType.Measured)
        this.topValue = (object) ((MeasuredValue) this.topValue).Value;
      if (this.tf.resType == DataType.Integer && this.topValue is double)
        this.topValue = (object) Math.Round((double) this.topValue);
      return this.topValue;
    }

    internal bool PerformCmd()
    {
      if (this.curCmd >= this.tf.postfixForm.Count)
        return false;
      Token t = this.tf.postfixForm[this.curCmd];
      int curCmd = this.curCmd;
      switch (t.type)
      {
        case Intermech.Expert.TokenType.UnaryOper:
          if (t.text == "-")
          {
            this.CheckStackType(t, true, DataType.Integer, DataType.Float);
            int topType = (int) this.topType;
            if (topType == 0)
              this.topValue = (object) -(int) this.topValue;
            if (topType == 1)
              this.topValue = (object) -(double) this.topValue;
          }
          if (t.text == LocalizationHolder.rm.GetString("Expert.Server_61"))
          {
            this.CheckStackType(t, true, DataType.Boolean);
            this.topValue = (object) !(bool) this.topValue;
          }
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.BinaryOper:
          if (!this.PerformBinary(t))
            return false;
          break;
        case Intermech.Expert.TokenType.FuncCall:
          if (!this.PerformFunc(t))
            return false;
          break;
        case Intermech.Expert.TokenType.Integer:
          this.typeStack.Add(DataType.Integer);
          this.valueStack.Add((object) t.iValue);
          this.AddZeroAttr();
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Float:
          this.typeStack.Add(DataType.Float);
          this.valueStack.Add((object) t.fValue);
          this.AddZeroAttr();
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.String:
          this.typeStack.Add(DataType.String);
          string str = t.text != "<CR>" ? t.text : "\r\n";
          if (str.StartsWith("\"") && str.EndsWith("\""))
            str = str.Substring(1, str.Length - 2);
          this.valueStack.Add((object) str);
          this.AddZeroAttr();
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Date:
          this.typeStack.Add(DataType.Date);
          this.valueStack.Add((object) new DateTime(t.iValue));
          this.AddZeroAttr();
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.ObjectLink:
          this.typeStack.Add(DataType.ObjectLink);
          this.valueStack.Add((object) t.iValue);
          this.AddZeroAttr();
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Attribute:
          if (!this.PerformAttribute(t))
            return false;
          break;
        case Intermech.Expert.TokenType.Command:
          if (!this.PerformCommand(t))
            return false;
          break;
        case Intermech.Expert.TokenType.Measured:
          if (this.tf.DropMeasure)
          {
            this.typeStack.Add(DataType.Float);
            this.valueStack.Add((object) t.fValue);
          }
          else
          {
            this.typeStack.Add(DataType.Measured);
            this.valueStack.Add((object) new MeasuredValue(t.fValue, t.iValue));
          }
          this.AddZeroAttr();
          ++this.curCmd;
          break;
        case Intermech.Expert.TokenType.Boolean:
          this.typeStack.Add(DataType.Boolean);
          if (t.iValue == 0L)
            this.valueStack.Add((object) false);
          else
            this.valueStack.Add((object) true);
          this.AddZeroAttr();
          ++this.curCmd;
          break;
      }
      if (t.type != Intermech.Expert.TokenType.Attribute)
        this.refObjId = -1L;
      return true;
    }

    public static long GetDefMeasureId(int attrId)
    {
      return MeasureHelper.GetBaseMeasureID(MetaDataHelper.GetAttributeType(attrId).SizeType);
    }

    public static object GetDefaultValue(DataType dt, int attrId)
    {
      object defaultValue = (object) "";
      switch (dt)
      {
        case DataType.Integer:
          defaultValue = (object) 0;
          break;
        case DataType.Float:
          defaultValue = (object) 0.0;
          break;
        case DataType.Measured:
          defaultValue = (object) new MeasuredValue(0.0, ExpertServer.Calculator.GetDefMeasureId(attrId));
          break;
        case DataType.String:
          defaultValue = (object) "";
          break;
        case DataType.Date:
          defaultValue = (object) DateTime.MinValue;
          break;
        case DataType.Boolean:
          defaultValue = (object) false;
          break;
        case DataType.ObjectLink:
          defaultValue = (object) 0L;
          break;
      }
      return defaultValue;
    }

    internal void PerformVal(
      object val,
      FieldTypes ft,
      DataType dt,
      int attrIndex,
      bool DropMeasure,
      bool AsString,
      string attrGUID)
    {
      if (val is MemoProxyReader memoProxyReader)
      {
        if (!memoProxyReader.Loaded)
        {
          memoProxyReader.LoadData(this.ius);
          AttribPair usedAttr = this.tf.usedAttrs[attrIndex];
          string str = this.expServ.MakeSubstitute(this.ti, this.objID, usedAttr.attribID, memoProxyReader.Value);
          if (str != memoProxyReader.Value)
          {
            memoProxyReader.Value = str;
            this.expServ.SetParmValue(this.ti.taskId, this.objID, usedAttr.attribID, (object) str);
          }
        }
        val = (object) memoProxyReader.Value;
      }
      if (AsString)
      {
        if (attrGUID == "cad0002e-306c-11d8-b4e9-00304f19f545")
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(Convert.ToInt32(val));
          if (objectType != null)
          {
            val = (object) objectType.ObjectTypeName;
            ft = FieldTypes.ftString;
            dt = DataType.String;
          }
        }
        else if (ft == FieldTypes.ftObjectLink)
        {
          List<long> longList = new List<long>();
          switch (val)
          {
            case IList _:
              IEnumerator enumerator = (val as IList).GetEnumerator();
              try
              {
                while (enumerator.MoveNext())
                {
                  object current = enumerator.Current;
                  longList.Add(Convert.ToInt64(current));
                }
                break;
              }
              finally
              {
                if (enumerator is IDisposable disposable)
                  disposable.Dispose();
              }
            case ArrayHolder _:
              ArrayHolder arrayHolder = val as ArrayHolder;
              for (int x = 0; x < arrayHolder.Width; ++x)
              {
                for (int y = 0; y < arrayHolder.Height; ++y)
                  longList.Add(Convert.ToInt64(arrayHolder[x, y]));
              }
              break;
            default:
              try
              {
                longList.Add(Convert.ToInt64(val));
                break;
              }
              catch
              {
                break;
              }
          }
          StringBuilder stringBuilder = new StringBuilder();
          foreach (long objectId in longList)
          {
            TaskDataCache.ObjDataItem objData = this.ti.DataCache.GetObjData(objectId, this.ius);
            if ((TypedInfoItem) objData != (TypedInfoItem) null)
            {
              if (stringBuilder.Length > 0)
                stringBuilder.Append(", ");
              stringBuilder.Append(objData.Caption);
            }
          }
          val = (object) stringBuilder.ToString();
          ft = FieldTypes.ftString;
          dt = DataType.String;
        }
        else
        {
          bool flag = false;
          if (val != null)
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(attrGUID));
            if (attributeType != null)
            {
              if (attributeType.PossibleValues != null)
              {
                for (int index = 0; index < attributeType.PossibleValues.Count; ++index)
                {
                  if (Convert.ToString(val) == Convert.ToString(attributeType.PossibleValues[index]))
                  {
                    val = attributeType.PossibleValuesDescriptions[index];
                    dt = DataType.String;
                    flag = true;
                  }
                }
              }
              else
              {
                IDBObject dbObject = this.ius.GetObject(this.objID, false);
                if (dbObject != null)
                {
                  string[] descriptionsByGuid = dbObject.GetDescriptionsByGuid(new Guid(attrGUID), true);
                  if (descriptionsByGuid != null && descriptionsByGuid.Length != 0)
                  {
                    val = (object) descriptionsByGuid[0];
                    dt = DataType.String;
                    flag = true;
                  }
                }
              }
            }
          }
          if (!flag)
          {
            val = (object) Convert.ToString(val);
            dt = DataType.String;
          }
        }
      }
      if (ft == FieldTypes.ftMeasured && val.GetType() == typeof (string))
        val = (object) MeasureHelper.ConvertToMeasuredValue(Convert.ToString(val));
      if (DropMeasure && dt == DataType.Measured && val is MeasuredValue)
      {
        this.typeStack.Add(DataType.Float);
        this.valueStack.Add((object) ((MeasuredValue) val).Value);
      }
      else
      {
        this.typeStack.Add(dt);
        this.valueStack.Add(val);
      }
      this.attrStack.Add(AsString ? -1 : attrIndex);
      ++this.curCmd;
    }

    internal bool PerformAttribute(Token t)
    {
      try
      {
        if (t.iValue != (long) Token._Ref)
          return this._PerformAttr(t.info, false);
        if (!this._PerformAttr(t.info, false))
          return false;
        bool flag = true;
        try
        {
          this.refObjId = Convert.ToInt64(this.topValue);
        }
        catch
        {
          flag = false;
        }
        this.Pop((IList) this.typeStack);
        this.Pop((IList) this.valueStack);
        this.Pop((IList) this.attrStack);
        return flag;
      }
      catch (EAbort ex)
      {
        if (Math.Abs(t.fValue - (double) Token._SIGN) < 1.0000000195414814E-25)
        {
          PairName pairName = this.tf.pairNames[t.info];
          AttribPair usedAttr = this.tf.usedAttrs[t.info];
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(usedAttr.attribID);
          DataType dt = (attributeType.MultiValueMode == MultiValueModes.MultiValues ? 1 : (attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList ? 1 : 0)) != 0 || pairName.ft != FieldTypes.ftSystem ? pairName.GetDataType() : (usedAttr.attribID >= 0 ? DataType.String : DataTypeConvertor.AttrType2DataType(ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) usedAttr.attribID)));
          object defaultValue = ExpertServer.Calculator.GetDefaultValue(dt, usedAttr.attribID);
          this.typeStack.Add(dt);
          this.valueStack.Add(defaultValue);
          this.attrStack.Add(t.info);
          ++this.curCmd;
          return true;
        }
        throw;
      }
    }

    internal bool _PerformAttr(int attrIndex, bool AsString, bool dontCalc = false)
    {
      PairName pairName = this.tf.pairNames[attrIndex];
      AttribPair usedAttr = this.tf.usedAttrs[attrIndex];
      string attrGuiD1 = this.tf.attrGUIDs[attrIndex];
      string objTypeGuiD = this.tf.objTypeGUIDs[attrIndex];
      long objId = this.objID;
      long refObjId = this.refObjId;
      DataType dt = DataType.String;
      Dictionary<object, string> dictionary = (Dictionary<object, string>) null;
      TypedInfoItem typedInfoItem1 = (TypedInfoItem) null;
      try
      {
        if (this.refObjId != -1L)
          this.objID = this.refObjId;
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(attrGuiD1));
        if (attributeType == null)
          throw new EAbort(this.calcCond ? ExpertResult.NoCondParms : ExpertResult.NoCalcParms);
        if (attributeType.MultiValueMode == MultiValueModes.MultiValues || attributeType.MultiValueMode == MultiValueModes.MultiValuesFromList)
          pairName.Multi = true;
        if (attributeType.PossibleValues != null && attributeType.PossibleValues.Count > 0)
        {
          dictionary = new Dictionary<object, string>();
          for (int index = 0; index < attributeType.PossibleValues.Count; ++index)
            dictionary.Add(attributeType.PossibleValues[index], attributeType.PossibleValuesDescriptions[index].ToString());
        }
        ExpertServer.TempAttrStru tempAttrStru = this.ti.GetTempAttrStru(attributeType.AttributeGuid);
        if (tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
          this.objID = -1L;
        if (this.expServ.IsAttrNeeded(this.ti, this.objID, usedAttr.objTypeID, usedAttr.attribID))
          throw new EAbort(this.calcCond ? ExpertResult.NoCondParms : ExpertResult.NoCalcParms);
        try
        {
          dt = pairName.Multi || pairName.ft != FieldTypes.ftSystem ? pairName.GetDataType() : DataTypeConvertor.AttrType2DataType(ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) usedAttr.attribID));
        }
        catch (EInvalidAttrType ex)
        {
          throw new ExpertServerException($"{LocalizationHolder.rm.GetString("Expert.Server_62")}\"{pairName.ShortName}\"");
        }
        object val1 = this.expServ._GetParmValue(this.ti, this.objID, usedAttr.objTypeID, usedAttr.attribID);
        if (val1 != null && val1.GetType() == typeof (string) && (string) val1 == "" && dt == DataType.Float)
          val1 = (object) null;
        if (val1 != null)
        {
          if (this.tf.DropMeasure && dt == DataType.Measured && val1 is MeasuredValue)
          {
            this.typeStack.Add(DataType.Float);
            this.valueStack.Add((object) ((MeasuredValue) val1).Value);
          }
          else
          {
            if (AsString)
            {
              this.PerformVal(val1, pairName.ft, dt, attrIndex, false, true, attrGuiD1);
              return true;
            }
            if (dt == DataType.Measured)
            {
              switch (val1)
              {
                case string _:
                  val1 = val1.Equals((object) "") ? (object) null : (object) MeasureHelper.ConvertToMeasuredValue((string) val1, false);
                  if (val1 == null)
                  {
                    double result = 0.0;
                    val1 = !double.TryParse((string) val1, out result) ? (object) new MeasuredValue(0.0, 0L) : (object) new MeasuredValue(result, 0L);
                    break;
                  }
                  break;
                case double _:
                  val1 = (object) new MeasuredValue((double) val1, 0L);
                  break;
              }
            }
            this.typeStack.Add(dt);
            this.valueStack.Add(val1);
          }
          this.attrStack.Add(attrIndex);
          ++this.curCmd;
          return true;
        }
        bool flag1 = this.refObjId == -1L;
        bool flag2 = false;
        if (flag1 && usedAttr.objTypeID != 0 && usedAttr.objTypeID != -1)
        {
          typedInfoItem1 = this.ti.DataCache.GetItemData(this.objID, this.ius);
          int childTypeID = typedInfoItem1 != (TypedInfoItem) null ? Math.Abs(typedInfoItem1.ItemTypeID) : 0;
          if (childTypeID == 0)
          {
            flag1 = false;
          }
          else
          {
            flag2 = !ExpertServer.IsTypeDescendant(usedAttr.objTypeID, childTypeID);
            flag1 = !flag2;
          }
        }
        string attrGuiD2 = this.tf.attrGUIDs[attrIndex];
        if (flag1 && this.row != null && !pairName.Multi)
        {
          int colIndexByName = this.row.GetColIndexByName(attrGuiD2);
          if (colIndexByName >= 0)
          {
            object val2 = this.row[colIndexByName];
            if (val2.IsNullOrDBNull())
            {
              if (!(attrGuiD2 == ExpertAttrGUIDs.attrQuantity))
                throw new EAbort(this.calcCond ? ExpertResult.NoCondParms : ExpertResult.NoCalcParms, $"{LocalizationHolder.rm.GetString("Expert.Server_63")}\"{pairName.ShortName}\"");
              val2 = (object) new MeasuredValue(0.0, ExpertConsts.Consts.measureShtuk);
            }
            this.PerformVal(val2, pairName.ft, dt, attrIndex, this.tf.DropMeasure, AsString, attrGuiD1);
            return true;
          }
        }
        if (this.ti.savedData != null && !flag2 && this.ti.savedData.Columns.Contains(attrGuiD2))
        {
          HybridRowExp hybridRowExp = this.ti.savedDataByObjId(this.objID);
          if (hybridRowExp != null)
          {
            val1 = hybridRowExp[attrGuiD2];
            if (val1.NotNullOrDBNull())
            {
              this.PerformVal(val1, pairName.ft, dt, attrIndex, this.tf.DropMeasure, AsString, attrGuiD1);
              return true;
            }
          }
        }
        if (!AsString && !flag2 && this.GetLoadedValue(this.taskId, usedAttr, attrGuiD1, this.objID, out val1))
        {
          if (val1.NotNullOrDBNull())
          {
            this.PerformVal(val1, pairName.ft, dt, attrIndex, this.tf.DropMeasure, AsString, attrGuiD1);
            return true;
          }
        }
        else
        {
          IDBAttributable idbA = (IDBAttributable) null;
          bool flag3 = false;
          if (!tempAttrStru.HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
          {
            TypedInfoItem typedInfoItem2 = typedInfoItem1;
            if ((object) typedInfoItem2 == null)
              typedInfoItem2 = this.ti.DataCache.GetItemData(this.objID, this.ius);
            TypedInfoItem typedInfoItem3 = typedInfoItem2;
            if (typedInfoItem3 is TaskDataCache.RelDataItem)
            {
              IDBRelation relation = this.ius.GetRelation(((RelInfoItem) typedInfoItem3).RelationID, false);
              if (relation != null)
              {
                IDBAttribute attributeById = relation.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
                IDBObject dbObject = attributeById != null ? this.ius.GetObject(Convert.ToInt64(attributeById.Value), false) : this.ius.GetObjectByVersionsRule(relation.PartID, this.ti.verRuleOwnerId, false);
                if (dbObject != null && (usedAttr.objTypeID == -1 || ExpertServer.IsTypeDescendant(usedAttr.objTypeID, dbObject.ObjectType)))
                  idbA = (IDBAttributable) dbObject;
              }
            }
            if (idbA == null)
            {
              if (usedAttr.objTypeID != -1 && this.LoadObjectForType(usedAttr.objTypeID))
                idbA = this.idbO;
              else if (this.refObjId != -1L)
                idbA = (IDBAttributable) this.ius.GetObject(this.objID, false);
              else if (usedAttr.objTypeID == -1 && this.LoadObject() && this.idbO != null && (usedAttr.attribID < 0 || this.idbO != null && this.idbO.GetAttributeByGuid(new Guid(attrGuiD1)) != null))
                idbA = this.idbO;
              else if (usedAttr.objTypeID != -1)
              {
                idbA = this.expServ.FindObjectWithAttr(this.taskId, this.ius, this.objID, usedAttr.attribID, usedAttr.objTypeID);
                flag3 = idbA != null;
                if (!flag3 && this.ti._addObjs != null && this.ti._addObjs.Count > 0)
                {
                  foreach (long addObj in this.ti._addObjs)
                  {
                    idbA = this.expServ.FindObjectWithAttr(this.taskId, this.ius, addObj, usedAttr.attribID, usedAttr.objTypeID);
                    flag3 = idbA != null;
                    if (flag3)
                      break;
                  }
                }
              }
            }
            if (idbA == null)
            {
              long aRelationID = 0;
              if (this.ti.curRelationId != 0L)
                aRelationID = this.ti.curRelationId;
              if (aRelationID != 0L)
              {
                IDBRelation relation = this.ius.GetRelation(aRelationID, false);
                if (relation != null && relation.GetAttributeByID(usedAttr.attribID) != null)
                  idbA = (IDBAttributable) relation;
              }
            }
          }
          if (idbA != null)
          {
            long num = ExpertServer.AttributableId(idbA);
            try
            {
              if (usedAttr.attribID < 0)
                val1 = (!AsString ? idbA.GetValuesByGuid(new Guid(attrGuiD1), true) : (object[]) idbA.GetDescriptionsByGuid(new Guid(attrGuiD1), true))[0];
              else if (AsString)
              {
                IDBAttribute attributeByGuid = idbA.GetAttributeByGuid(new Guid(attrGuiD1));
                if (attributeByGuid != null)
                {
                  if (pairName.Multi)
                  {
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int index = 0; index < attributeByGuid.Descriptions.Length; ++index)
                    {
                      stringBuilder.Append(attributeByGuid.Descriptions[index]);
                      if (index < attributeByGuid.Descriptions.Length - 1)
                        stringBuilder.Append(", ");
                    }
                    val1 = (object) stringBuilder.ToString();
                  }
                  else
                    val1 = pairName.ft != FieldTypes.ftMemo ? (object) attributeByGuid.Description : (object) Convert.ToString(idbA.GetValuesByGuid(new Guid(attrGuiD1), true)[0]);
                  if (dictionary != null)
                    val1 = (object) dictionary[val1];
                }
              }
              else
              {
                val1 = this.expServ._GetParmValue(this.ti, ExpertServer.AttributableId(idbA), usedAttr.objTypeID, usedAttr.attribID);
                if (val1 != null)
                {
                  if (this.tf.DropMeasure && dt == DataType.Measured && val1 is MeasuredValue)
                  {
                    this.typeStack.Add(DataType.Float);
                    this.valueStack.Add((object) ((MeasuredValue) val1).Value);
                  }
                  else
                  {
                    this.typeStack.Add(dt);
                    this.valueStack.Add(val1);
                  }
                  this.attrStack.Add(attrIndex);
                  ++this.curCmd;
                  return true;
                }
                ExpertServer.ObjAttr key = new ExpertServer.ObjAttr(ExpertServer.AttributableId(idbA), usedAttr.attribID);
                if (!this.ti.attrCache.TryGetValue(key, out val1))
                {
                  object[] valuesByGuid = idbA.GetValuesByGuid(new Guid(attrGuiD1), true);
                  if (pairName.Multi)
                  {
                    ArrayHolder arrayHolder = new ArrayHolder(valuesByGuid.Length, 1);
                    for (int x = 0; x < valuesByGuid.Length; ++x)
                      arrayHolder[x, 0] = valuesByGuid[x];
                    val1 = (object) arrayHolder;
                  }
                  else if (pairName.ft == FieldTypes.ftMemo)
                  {
                    val1 = (object) Convert.ToString(valuesByGuid[0]);
                    string str = this.expServ.MakeSubstitute(this.ti, num, usedAttr.attribID, (string) val1);
                    if ((object) str != val1)
                    {
                      val1 = (object) str;
                      this.expServ.SetParmValue(this.ti.taskId, num, usedAttr.attribID, (object) str);
                    }
                  }
                  else
                    val1 = valuesByGuid[0];
                  if (this.ti.SimpleCalcMode && val1.IsNullOrDBNull())
                  {
                    if (dt == DataType.String)
                      val1 = (object) "";
                    if (dt == DataType.Integer)
                      val1 = (object) 0;
                    if (dt == DataType.Float)
                      val1 = (object) 0.0;
                  }
                  if (val1 is MeasuredValue measuredValue && measuredValue.Caption != "")
                    val1 = (object) MeasureHelper.ConvertToMeasuredValue(measuredValue.Caption);
                  this.ti.attrCache.Add(key, val1);
                }
              }
            }
            catch
            {
            }
            if (((pairName.ft == FieldTypes.ftString ? 1 : (pairName.ft == FieldTypes.ftMemo ? 1 : 0)) & (flag3 ? 1 : 0)) != 0 && val1.IsDBNull())
              val1 = (object) "";
            if (val1.NotNullOrDBNull())
            {
              if (this.tf.DropMeasure && dt == DataType.Measured && val1 is MeasuredValue)
              {
                this.typeStack.Add(DataType.Float);
                this.valueStack.Add((object) ((MeasuredValue) val1).Value);
              }
              else
              {
                if (AsString)
                {
                  dt = DataType.String;
                  val1 = (object) Convert.ToString(val1);
                }
                this.typeStack.Add(dt);
                this.valueStack.Add(val1);
              }
              this.attrStack.Add(attrIndex);
              ++this.curCmd;
              return true;
            }
          }
        }
        if (!dontCalc)
        {
          ExpertResult attr = this.expServ.CalculateAttr(this.taskId, usedAttr.objTypeID, usedAttr.attribID, this.objID, ExpertServer.CalcStages.CalcAttribute, out val1, objId);
          if (attr != ExpertResult.OK)
            throw new EAbort(attr);
        }
        if (val1 == null)
        {
          this.expServ.AddNeededAttr(this.taskId, this.objID, usedAttr.objTypeID, usedAttr.attribID);
          throw new EAbort(this.calcCond ? ExpertResult.NoCondParms : ExpertResult.NoCalcParms);
        }
        if (val1 is ExpertValue)
          val1 = (val1 as ExpertValue).Value;
        if (AsString)
          val1 = (object) Convert.ToString(val1);
        if (this.tf.DropMeasure && dt == DataType.Measured && val1 is MeasuredValue)
        {
          this.typeStack.Add(DataType.Float);
          this.valueStack.Add((object) ((MeasuredValue) val1).Value);
        }
        else
        {
          if (AsString)
          {
            dt = DataType.String;
            val1 = (object) Convert.ToString(val1);
          }
          this.typeStack.Add(dt);
          this.valueStack.Add(val1);
        }
        this.attrStack.Add(attrIndex);
        ++this.curCmd;
        return true;
      }
      catch
      {
        object obj = (object) null;
        if (this.ti.SimpleCalcMode)
        {
          if (dt == DataType.String)
            obj = (object) "";
          if (dt == DataType.Integer)
            obj = (object) 0;
          if (dt == DataType.Float)
            obj = (object) 0.0;
        }
        if (obj != null)
        {
          this.typeStack.Add(dt);
          this.valueStack.Add(obj);
          this.attrStack.Add(attrIndex);
          ++this.curCmd;
          return true;
        }
        throw;
      }
      finally
      {
        this.objID = objId;
        this.refObjId = refObjId;
      }
    }

    internal bool GetLoadedValue(
      int taskId,
      AttribPair ap,
      string attrGuid,
      long objId,
      out object value)
    {
      value = (object) null;
      ExpertServer.ExpServTask task = ExpertServer.es.GetTask(taskId);
      if (task.dataObjIndex == null)
        return false;
      if (task.dataObjIndex.ContainsKey(objId))
      {
        int index = task.dataObjIndex[objId];
        HybridRowExp hybridRowExp = task.savedData[index];
        int int32 = Convert.ToInt32(hybridRowExp["cad0002e-306c-11d8-b4e9-00304f19f545"]);
        if (ExpertServer.IsTypeDescendant(ap.objTypeID, int32) && task.savedData.Columns.Contains(attrGuid))
        {
          value = hybridRowExp[attrGuid];
          return true;
        }
      }
      return task.OptGetObjectAttr(objId, ap.attribID, out value);
    }

    internal bool PerformBinary(Token t)
    {
      DataType firstType = this.firstType;
      DataType topType = this.topType;
      object obj1 = (object) null;
      string str1;
      if (t.text.StartsWith(" "))
      {
        str1 = t.text.Trim();
        t.text = str1;
      }
      else
        str1 = t.text;
      switch (str1)
      {
        case "*":
        case "-":
        case "/":
        case "^":
          this.CheckStackType(t, false, DataType.Float, DataType.Integer, DataType.Measured);
          this.CheckStackType(t, true, DataType.Float, DataType.Integer, DataType.Measured);
          DataType dataType1;
          if (firstType == DataType.Measured || topType == DataType.Measured)
          {
            MeasuredValue val1 = firstType == DataType.Measured ? (MeasuredValue) this.firstValue : new MeasuredValue(Convert.ToDouble(this.firstValue), 0L);
            MeasuredValue val2 = topType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L);
            switch (str1)
            {
              case "-":
                obj1 = (object) ExpertServer.MeasureSubtract(val1, val2);
                break;
              case "*":
                obj1 = (object) ExpertServer.MeasureMultiple(val1, val2);
                break;
              case "^":
                if (topType == DataType.Measured)
                  throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_103"));
                obj1 = (object) new MeasuredValue(Math.Exp(Convert.ToDouble(this.topValue) * Math.Log(val1.Value)), val1.MeasureID);
                break;
              case "/":
                obj1 = (object) ExpertServer.MeasureDivide(val1, val2);
                break;
            }
            MeasuredValue measuredValue = (MeasuredValue) obj1;
            if (measuredValue.Caption == "")
              measuredValue.Caption = Convert.ToString(measuredValue.Value);
            dataType1 = DataType.Measured;
          }
          else if (firstType == DataType.Float || topType == DataType.Float)
          {
            double d1 = Convert.ToDouble(this.firstValue);
            double num = Convert.ToDouble(this.topValue);
            switch (str1)
            {
              case "-":
                obj1 = (object) (d1 - num);
                break;
              case "*":
                obj1 = (object) (d1 * num);
                break;
              case "^":
                obj1 = (object) Math.Exp(num * Math.Log(d1));
                break;
              case "/":
                obj1 = (object) (d1 / num);
                if (obj1 is double d2 && (double.IsNaN(d2) || double.IsInfinity(d2)))
                  throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_291"));
                break;
            }
            dataType1 = DataType.Float;
          }
          else
          {
            double int64_1 = (double) Convert.ToInt64(this.firstValue);
            double int64_2 = (double) Convert.ToInt64(this.topValue);
            dataType1 = DataType.Integer;
            switch (str1)
            {
              case "-":
                obj1 = (object) (int64_1 - int64_2);
                break;
              case "*":
                obj1 = (object) (int64_1 * int64_2);
                break;
              case "^":
                obj1 = (object) Math.Round(Math.Pow(int64_1, int64_2));
                break;
              case "/":
                obj1 = (object) Convert.ToDouble(int64_1 * 1.0 / int64_2);
                if (obj1 is double d && (double.IsNaN(d) || double.IsInfinity(d)))
                  throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_291"));
                dataType1 = DataType.Float;
                break;
            }
          }
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.attrStack);
          this.Pop((IList) this.attrStack);
          this.typeStack.Add(dataType1);
          this.valueStack.Add(obj1);
          this.AddZeroAttr();
          ++this.curCmd;
          return true;
        case "+":
          this.CheckStackType(t, false, DataType.Float, DataType.Integer, DataType.String, DataType.Measured);
          this.CheckStackType(t, true, DataType.Float, DataType.Integer, DataType.String, DataType.Measured);
          object obj2;
          DataType dataType2;
          if (firstType == DataType.String || topType == DataType.String)
          {
            obj2 = (object) (ExpertServer.MakeString(this.firstValue, firstType, this.ti) + ExpertServer.MakeString(this.topValue, topType, this.ti));
            dataType2 = DataType.String;
          }
          else if (firstType == DataType.Measured || topType == DataType.Measured)
          {
            obj2 = (object) ExpertServer.MeasureSum(firstType == DataType.Measured ? (MeasuredValue) this.firstValue : new MeasuredValue(Convert.ToDouble(this.firstValue), ExpertConsts.Consts.measureShtuk), topType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), ExpertConsts.Consts.measureShtuk));
            dataType2 = DataType.Measured;
          }
          else if (firstType == DataType.Float || topType == DataType.Float)
          {
            obj2 = (object) (Convert.ToDouble(this.firstValue) + Convert.ToDouble(this.topValue));
            dataType2 = DataType.Float;
          }
          else
          {
            obj2 = (object) (Convert.ToInt64(this.firstValue) + Convert.ToInt64(this.topValue));
            dataType2 = DataType.Integer;
          }
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.attrStack);
          this.Pop((IList) this.attrStack);
          this.typeStack.Add(dataType2);
          this.valueStack.Add(obj2);
          this.AddZeroAttr();
          ++this.curCmd;
          return true;
        case ":":
          DiapValue diapValue = new DiapValue(new ExpertValue(this.topType, this.firstValue), new ExpertValue(this.topType, this.topValue));
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.attrStack);
          this.Pop((IList) this.attrStack);
          this.typeStack.Add(DataType.Diap);
          this.valueStack.Add((object) diapValue);
          this.AddZeroAttr();
          ++this.curCmd;
          return true;
        case "<":
        case "<=":
        case "<>":
        case "=":
        case ">":
        case ">=":
          if (str1 == "=" || str1 == "<>")
          {
            switch (firstType)
            {
              case DataType.ObjectLink:
                if (topType != DataType.Integer && topType != DataType.ObjectLink && topType != DataType.Packet)
                  throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_104"));
                object obj3 = (object) false;
                if (topType != DataType.Packet || ((PacketValue) this.topValue).Count == 1 && ((PacketValue) this.topValue)[0].ValueType == DataType.Integer)
                {
                  long int64 = Convert.ToInt64(this.firstValue);
                  long num = topType != DataType.Packet ? Convert.ToInt64(this.topValue) : Convert.ToInt64((object) ((PacketValue) this.topValue)[0]);
                  List<long> folders = new List<long>(1);
                  folders.Add(num);
                  obj3 = (object) folders.Contains(int64);
                  if (!Convert.ToBoolean(obj3))
                  {
                    switch (ExpertServer.Calculator.IsImbaseObject(this.tf.usedAttrs[this.attrStack[this.attrStack.Count - 2]]))
                    {
                      case ImbaseCatalogSelectMode.imcmSelectFolder:
                        obj3 = (object) ExpertServer.Calculator.ImbaseObjectInFolders(int64, folders, this.ius);
                        break;
                      case ImbaseCatalogSelectMode.imcmCreateObject:
                        long childId = -1;
                        if (!ExpertServer.es.imbaseKeys.TryGetValue(int64, out childId))
                        {
                          IDBAttribute attributeById = this.ius.GetObject(int64, false)?.GetAttributeByID(ExpertConsts.Consts.attrIMBASECode);
                          if (attributeById != null && attributeById.Value.NotDBNull())
                            childId = Convert.ToInt64(attributeById.Value);
                          ExpertServer.es.imbaseKeys.TryAdd(int64, childId);
                        }
                        if (childId != -1L)
                        {
                          obj3 = (object) ExpertServer.Calculator.ImbaseObjectInFolders(childId, folders, this.ius);
                          break;
                        }
                        break;
                    }
                  }
                }
                this.Pop((IList) this.typeStack);
                this.Pop((IList) this.typeStack);
                this.Pop((IList) this.valueStack);
                this.Pop((IList) this.valueStack);
                this.Pop((IList) this.attrStack);
                this.Pop((IList) this.attrStack);
                this.typeStack.Add(DataType.Boolean);
                this.valueStack.Add(str1 == "=" ? obj3 : (object) !(bool) obj3);
                this.AddZeroAttr();
                ++this.curCmd;
                return true;
              case DataType.Packet:
                object obj4;
                if (topType == DataType.Packet)
                {
                  obj4 = (object) this.ComparePackets((PacketValue) this.firstValue, (PacketValue) this.topValue);
                }
                else
                {
                  PacketValue pv2 = new PacketValue();
                  pv2.Add(new ExpertValue(topType, this.topValue));
                  obj4 = (object) this.ComparePackets((PacketValue) this.firstValue, pv2);
                }
                this.Pop((IList) this.typeStack);
                this.Pop((IList) this.typeStack);
                this.Pop((IList) this.valueStack);
                this.Pop((IList) this.valueStack);
                this.Pop((IList) this.attrStack);
                this.Pop((IList) this.attrStack);
                this.typeStack.Add(DataType.Boolean);
                this.valueStack.Add(obj4);
                this.AddZeroAttr();
                ++this.curCmd;
                return true;
              default:
                int attr1 = this.attrStack[this.attrStack.Count - 2];
                string str2 = "";
                if (attr1 != -1)
                  str2 = this.tf.attrGUIDs[attr1];
                if (str2 == "cad0002e-306c-11d8-b4e9-00304f19f545")
                {
                  if (topType != DataType.Integer && topType != DataType.ObjType)
                    throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_105"));
                  object obj5 = (object) this.CheckObjectType(Convert.ToInt32(this.firstValue), Convert.ToInt32(this.topValue));
                  if (str1 == "<>")
                    obj5 = (object) !(bool) obj5;
                  this.Pop((IList) this.typeStack);
                  this.Pop((IList) this.typeStack);
                  this.Pop((IList) this.valueStack);
                  this.Pop((IList) this.valueStack);
                  this.Pop((IList) this.attrStack);
                  this.Pop((IList) this.attrStack);
                  this.typeStack.Add(DataType.Boolean);
                  this.valueStack.Add(obj5);
                  this.AddZeroAttr();
                  ++this.curCmd;
                  return true;
                }
                break;
            }
          }
          this.CheckStackType(t, false, DataType.Float, DataType.Integer, DataType.String, DataType.Measured, DataType.Date, DataType.Boolean);
          this.CheckStackType(t, true, DataType.Float, DataType.Integer, DataType.String, DataType.Measured, DataType.Date, DataType.Boolean);
          if (firstType == DataType.Date || topType == DataType.Date)
          {
            if (firstType != topType)
            {
              if (firstType != DataType.Date && firstType != DataType.String || topType != DataType.Date && topType != DataType.String)
                throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_199"));
            }
            else
            {
              int num = DateTime.Compare(Convert.ToDateTime(this.firstValue).Date, Convert.ToDateTime(this.topValue).Date);
              switch (str1)
              {
                case "<":
                  obj1 = (object) (num < 0);
                  break;
                case "<=":
                  obj1 = (object) (num <= 0);
                  break;
                case ">":
                  obj1 = (object) (num > 0);
                  break;
                case ">=":
                  obj1 = (object) (num >= 0);
                  break;
                case "=":
                  obj1 = (object) (num == 0);
                  break;
                case "<>":
                  obj1 = (object) (num != 0);
                  break;
              }
            }
          }
          else if (firstType == DataType.String || topType == DataType.String)
          {
            int num = string.Compare(Convert.ToString(this.firstValue), Convert.ToString(this.topValue));
            switch (str1)
            {
              case "<":
                obj1 = (object) (num < 0);
                break;
              case "<=":
                obj1 = (object) (num <= 0);
                break;
              case ">":
                obj1 = (object) (num > 0);
                break;
              case ">=":
                obj1 = (object) (num >= 0);
                break;
              case "=":
                obj1 = (object) (num == 0);
                break;
              case "<>":
                obj1 = (object) (num != 0);
                break;
            }
          }
          else if (firstType == DataType.Measured || topType == DataType.Measured)
          {
            CompareResult compareResult = MeasureHelper.Compare(firstType == DataType.Measured ? (MeasuredValue) this.firstValue : new MeasuredValue(Convert.ToDouble(this.firstValue), 0L), topType == DataType.Measured ? (MeasuredValue) this.topValue : new MeasuredValue(Convert.ToDouble(this.topValue), 0L));
            switch (str1)
            {
              case "<":
                obj1 = (object) (compareResult == CompareResult.Less);
                break;
              case "<=":
                obj1 = (object) (bool) (compareResult == CompareResult.Less ? 1 : (compareResult == CompareResult.Equal ? 1 : 0));
                break;
              case ">":
                obj1 = (object) (compareResult == CompareResult.More);
                break;
              case ">=":
                obj1 = (object) (bool) (compareResult == CompareResult.More ? 1 : (compareResult == CompareResult.Equal ? 1 : 0));
                break;
              case "=":
                obj1 = (object) (compareResult == CompareResult.Equal);
                break;
              case "<>":
                obj1 = (object) (compareResult != 0);
                break;
            }
          }
          else if (firstType == DataType.Float || topType == DataType.Float)
          {
            double num = Convert.ToDouble(this.firstValue) - Convert.ToDouble(this.topValue);
            switch (str1)
            {
              case "<":
              case "<=":
                obj1 = (object) (num < 1E-25);
                break;
              case ">":
              case ">=":
                obj1 = (object) (num > 1E-25);
                break;
              case "=":
                obj1 = (object) (Math.Abs(num) < 1E-25);
                break;
              case "<>":
                obj1 = (object) (Math.Abs(num) > 1E-25);
                break;
            }
          }
          else if (firstType == DataType.Boolean || topType == DataType.Boolean)
          {
            bool boolean1 = Convert.ToBoolean(this.firstValue);
            bool boolean2 = Convert.ToBoolean(this.topValue);
            switch (str1)
            {
              case "=":
                obj1 = (object) (boolean1 == boolean2);
                break;
              case "<>":
                obj1 = (object) (boolean1 != boolean2);
                break;
            }
          }
          else
          {
            long int64_3 = Convert.ToInt64(this.firstValue);
            long int64_4 = Convert.ToInt64(this.topValue);
            switch (str1)
            {
              case "<":
                obj1 = (object) (int64_3 < int64_4);
                break;
              case "<=":
                obj1 = (object) (int64_3 <= int64_4);
                break;
              case ">":
                obj1 = (object) (int64_3 > int64_4);
                break;
              case ">=":
                obj1 = (object) (int64_3 >= int64_4);
                break;
              case "=":
                obj1 = (object) (int64_3 == int64_4);
                break;
              case "<>":
                obj1 = (object) (int64_3 != int64_4);
                break;
            }
          }
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.attrStack);
          this.Pop((IList) this.attrStack);
          this.typeStack.Add(DataType.Boolean);
          this.valueStack.Add(obj1);
          this.AddZeroAttr();
          ++this.curCmd;
          return true;
        case "?":
          int attr2 = this.attrStack[this.attrStack.Count - 2];
          object obj6;
          if (firstType == DataType.ObjectLink)
          {
            long int64 = Convert.ToInt64(this.firstValue);
            List<long> folders = (List<long>) null;
            if (this.topValue is PacketValue)
            {
              PacketValue topValue = (PacketValue) this.topValue;
              folders = new List<long>(topValue.Count);
              for (int index = 0; index < topValue.Count; ++index)
              {
                ExpertValue expertValue = topValue[index];
                if (expertValue.ValueType == DataType.Integer)
                  folders.Add(Convert.ToInt64(expertValue.Value));
              }
            }
            if (this.topValue is ArrayHolder)
            {
              ArrayHolder topValue = (ArrayHolder) this.topValue;
              folders = new List<long>(topValue.Width);
              for (int x = 0; x < topValue.Width; ++x)
                folders.Add(Convert.ToInt64(topValue[x, 0]));
            }
            obj6 = folders != null ? (object) folders.Contains(int64) : throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_287"));
            if (!Convert.ToBoolean(obj6))
            {
              switch (ExpertServer.Calculator.IsImbaseObject(this.tf.usedAttrs[attr2]))
              {
                case ImbaseCatalogSelectMode.imcmSelectFolder:
                  obj6 = (object) ExpertServer.Calculator.ImbaseObjectInFolders(int64, folders, this.ius);
                  break;
                case ImbaseCatalogSelectMode.imcmCreateObject:
                  long childId = -1;
                  if (!ExpertServer.es.imbaseKeys.TryGetValue(int64, out childId))
                  {
                    if (this.idbO == null)
                      this.idbO = (IDBAttributable) this.ius.GetObject(int64, false);
                    IDBAttribute attributeById = this.idbO != null ? this.idbO.GetAttributeByID(ExpertConsts.Consts.attrIMBASECode) : (IDBAttribute) null;
                    if (attributeById != null && attributeById.Value.NotDBNull())
                      childId = Convert.ToInt64(attributeById.Value);
                    ExpertServer.es.imbaseKeys.TryAdd(int64, childId);
                  }
                  if (childId != -1L)
                  {
                    obj6 = (object) ExpertServer.Calculator.ImbaseObjectInFolders(childId, folders, this.ius);
                    break;
                  }
                  break;
              }
            }
          }
          else
          {
            string str3 = "";
            if (attr2 != -1)
              str3 = this.tf.attrGUIDs[attr2];
            if (str3 == "cad0002e-306c-11d8-b4e9-00304f19f545")
            {
              if (firstType != DataType.Integer)
                throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_106"));
              PacketValue pvalue = this.ArrayToPValue(this.topValue, DataType.Integer);
              obj6 = (object) this.IsObjTypeInPacket(Convert.ToInt32(this.firstValue), pvalue);
            }
            else
            {
              PacketValue pvalue = this.ArrayToPValue(this.topValue, firstType);
              obj6 = (object) this.IsInPacket(this.firstValue, firstType, pvalue);
            }
          }
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.attrStack);
          this.Pop((IList) this.attrStack);
          this.typeStack.Add(DataType.Boolean);
          this.valueStack.Add(obj6);
          this.AddZeroAttr();
          ++this.curCmd;
          return true;
        default:
          return false;
      }
    }

    internal static ImbaseCatalogSelectMode IsImbaseObject(AttribPair ap)
    {
      ImbaseCatalogSelectMode catalogSelectMode1 = ImbaseCatalogSelectMode.imcmNone;
      if (ap == null)
        return catalogSelectMode1;
      ImbaseCatalogSelectMode catalogSelectMode2;
      if (ap.attribID == ExpertConsts.Consts.attrIMBASECode)
      {
        catalogSelectMode2 = ImbaseCatalogSelectMode.imcmSelectFolder;
      }
      else
      {
        ExtendedServiceHelper.ObjTypeInfo objTypeData = ExtendedServiceHelper.GetObjTypeData(ap.objTypeID, ExpertServer.es.iies);
        if (objTypeData == null)
          return catalogSelectMode1;
        ImbaseExtendedItem imbaseExtendedItem = objTypeData.GetValue(ap.attribID, ExpertServer.es.iies);
        if (imbaseExtendedItem == null)
          return catalogSelectMode1;
        catalogSelectMode2 = imbaseExtendedItem.SelectMode;
      }
      if (catalogSelectMode2 == ImbaseCatalogSelectMode.imcmNone || ap.objTypeID == -1 || MetaDataHelper.GetObjectType(ap.objTypeID).AnyAttributes || MetaDataHelper.GetAttribute4ObjectType(ap.objTypeID, ap.attribID) != null)
        return catalogSelectMode2;
      catalogSelectMode2 = ImbaseCatalogSelectMode.imcmNone;
      return catalogSelectMode2;
    }

    internal static bool ImbaseObjectInFolders(long childId, List<long> folders, IUserSession ius)
    {
      if (folders.Contains(childId))
        return true;
      string str1 = (string) null;
      if (!ExpertServer.es.imbaseFolderKeys.TryGetValue(childId, out str1))
      {
        IDBObject dbObject = ius.GetObject(childId, false);
        if (dbObject != null)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(ExpertConsts.Consts.attrImbaseFolderKey);
          if (attributeById != null)
            str1 = Convert.ToString(attributeById.Value);
        }
        ExpertServer.es.imbaseFolderKeys.TryAdd(childId, str1);
      }
      if (str1 != null)
      {
        foreach (long folder in folders)
        {
          string str2 = (string) null;
          if (!ExpertServer.es.imbaseFolderKeys.TryGetValue(folder, out str2))
          {
            IDBObject dbObject = ius.GetObject(folder, false);
            if (dbObject != null)
            {
              IDBAttribute attributeById = dbObject.GetAttributeByID(ExpertConsts.Consts.attrImbaseFolderKey);
              if (attributeById != null)
                str2 = Convert.ToString(attributeById.Value);
            }
            ExpertServer.es.imbaseFolderKeys.TryAdd(folder, str2);
          }
          if (str2 != null && str1.StartsWith(str2))
            return true;
        }
      }
      return false;
    }

    internal bool CheckObjectType(int testId, int rootId)
    {
      return testId == rootId || MetaDataHelper.GetObjectTypeChildrenIDRecursive(rootId).Contains(testId);
    }

    internal bool IsInPacket(object val, DataType valType, PacketValue pv)
    {
      for (int index = 0; index < pv.Count; ++index)
      {
        switch (pv[index].ValueType)
        {
          case DataType.Integer:
            if ((valType == DataType.Integer || valType == DataType.String) && Convert.ToInt64(val) == Convert.ToInt64(pv[index].Value))
              return true;
            break;
          case DataType.Float:
            if ((valType == DataType.Float || valType == DataType.String || valType == DataType.Integer) && Math.Abs(Convert.ToDouble(val) - Convert.ToDouble(pv[index].Value)) < ExpertConsts.Epsilon)
              return true;
            break;
          case DataType.Measured:
            if (val is MeasuredValue && MeasureHelper.Compare((MeasuredValue) pv[index].Value, (MeasuredValue) val) == CompareResult.Equal || valType == DataType.String && Convert.ToString((object) (MeasuredValue) pv[index].Value) == Convert.ToString(val))
              return true;
            break;
          case DataType.String:
            if (Convert.ToString(val) == Convert.ToString(pv[index].Value))
              return true;
            break;
          case DataType.ObjectLink:
            if ((valType == DataType.Integer || valType == DataType.ObjectLink) && !pv[index].Value.IsDBNull() && Convert.ToInt64(val) == Convert.ToInt64(pv[index].Value))
              return true;
            break;
          case DataType.Diap:
            DiapValue diapValue = (DiapValue) pv[index].Value;
            switch (diapValue.Low.ValueType)
            {
              case DataType.Integer:
                long int64 = Convert.ToInt64(val);
                if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && int64 >= Convert.ToInt64(diapValue.Low.Value) && int64 <= Convert.ToInt64(diapValue.High.Value))
                  return true;
                continue;
              case DataType.Float:
                double num = Convert.ToDouble(val);
                if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && num >= Convert.ToDouble(diapValue.Low.Value) - ExpertConsts.Epsilon && num <= Convert.ToDouble(diapValue.High.Value) + ExpertConsts.Epsilon)
                  return true;
                continue;
              case DataType.Measured:
                if (valType == DataType.Measured)
                {
                  MeasuredValue val1_1 = (MeasuredValue) val;
                  MeasuredValue val1_2 = (MeasuredValue) diapValue.Low.Value;
                  MeasuredValue val2_1 = (MeasuredValue) diapValue.High.Value;
                  MeasuredValue val2_2 = val1_1;
                  CompareResult compareResult1 = MeasureHelper.Compare(val1_2, val2_2);
                  CompareResult compareResult2 = MeasureHelper.Compare(val1_1, val2_1);
                  if ((compareResult1 == CompareResult.Equal || compareResult1 == CompareResult.Less) && (compareResult2 == CompareResult.Equal || compareResult2 == CompareResult.Less))
                    return true;
                  continue;
                }
                continue;
              case DataType.String:
                string strA = Convert.ToString(val);
                if ((valType == DataType.Integer || valType == DataType.String || valType == DataType.Float) && string.Compare(strA, Convert.ToString(diapValue.Low.Value)) >= 0 && string.Compare(strA, Convert.ToString(diapValue.High.Value)) <= 0)
                  return true;
                continue;
              default:
                continue;
            }
        }
      }
      return false;
    }

    internal bool IsObjTypeInPacket(int objTypeId, PacketValue pv)
    {
      for (int index = 0; index < pv.Count; ++index)
      {
        switch (pv[index].ValueType)
        {
          case DataType.Integer:
            int int32_1 = Convert.ToInt32(pv[index].Value);
            if (objTypeId == int32_1 || this.CheckObjectType(objTypeId, int32_1))
              return true;
            break;
          case DataType.Diap:
            DiapValue diapValue = (DiapValue) pv[index].Value;
            if (diapValue.Low.ValueType != DataType.Integer || diapValue.High.ValueType != DataType.Integer)
              throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_107"));
            int int32_2 = Convert.ToInt32(diapValue.Low.Value);
            int int32_3 = Convert.ToInt32(diapValue.High.Value);
            if (objTypeId >= int32_2 && objTypeId <= int32_3)
              return true;
            for (int rootId = int32_2; rootId <= int32_3; ++rootId)
            {
              if (this.CheckObjectType(objTypeId, rootId))
                return true;
            }
            break;
          default:
            throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_108"));
        }
      }
      return false;
    }

    private bool ComparePackets(PacketValue pv1, PacketValue pv2)
    {
      if (pv1.Count != pv2.Count)
        return false;
      for (int index = 0; index < pv1.Count; ++index)
      {
        ExpertValue expertValue1 = pv1[index];
        ExpertValue expertValue2 = pv2[index];
        if (expertValue1.ValueType == expertValue2.ValueType)
        {
          if (expertValue1.ValueType == DataType.Packet)
          {
            if (!this.ComparePackets((PacketValue) expertValue1.Value, (PacketValue) expertValue2.Value))
              return false;
          }
          else if (!expertValue1.Value.Equals(expertValue2.Value))
            return false;
        }
        else if ((expertValue1.ValueType == DataType.String || expertValue2.ValueType == DataType.String) && Convert.ToString(expertValue1.Value) != Convert.ToString(expertValue2.Value))
          return false;
      }
      return true;
    }

    internal bool PerformCommand(Token t)
    {
      switch (t.info)
      {
        case 0:
          if (this.typeStack.Count == 0)
          {
            ++this.curCmd;
            break;
          }
          this.CheckStackType(t, true, DataType.Boolean);
          bool boolean = Convert.ToBoolean(this.topValue);
          if (boolean)
            this.curCmd = (int) t.iValue;
          else
            ++this.curCmd;
          if (this.curCmd < this.tf.postfixForm.Count)
          {
            Token token = this.tf.postfixForm[this.curCmd];
            if (!boolean || token.type != Intermech.Expert.TokenType.Command)
            {
              this.Pop((IList) this.typeStack);
              this.Pop((IList) this.valueStack);
              this.Pop((IList) this.attrStack);
              break;
            }
            break;
          }
          break;
        case 1:
          if (this.typeStack.Count == 0)
          {
            ++this.curCmd;
            break;
          }
          this.CheckStackType(t, true, DataType.Boolean);
          bool flag = !Convert.ToBoolean(this.topValue);
          if (flag)
            this.curCmd = (int) t.iValue;
          else
            ++this.curCmd;
          if (this.curCmd < this.tf.postfixForm.Count)
          {
            Token token = this.tf.postfixForm[this.curCmd];
            if (!flag || token.type != Intermech.Expert.TokenType.Command)
            {
              this.Pop((IList) this.typeStack);
              this.Pop((IList) this.valueStack);
              this.Pop((IList) this.attrStack);
              break;
            }
            break;
          }
          break;
        case 2:
          PacketValue packetValue = new PacketValue();
          for (int index = 0; (long) index < t.iValue; ++index)
            packetValue.Add(new ExpertValue(this.typeStack[this.typeStack.Count - (int) t.iValue + index], this.valueStack[this.valueStack.Count - (int) t.iValue + index]));
          for (int index = 0; (long) index < t.iValue; ++index)
          {
            this.Pop((IList) this.typeStack);
            this.Pop((IList) this.valueStack);
            this.Pop((IList) this.attrStack);
          }
          this.typeStack.Add(DataType.Packet);
          this.valueStack.Add((object) packetValue);
          this.AddZeroAttr();
          ++this.curCmd;
          break;
        case 3:
          int y = 0;
          int int32;
          if (t.text.EndsWith("2"))
          {
            int32 = Convert.ToInt32(this.firstValue);
            y = Convert.ToInt32(this.topValue);
            this.Pop((IList) this.typeStack);
            this.Pop((IList) this.valueStack);
            this.Pop((IList) this.attrStack);
          }
          else
            int32 = Convert.ToInt32(this.topValue);
          this.Pop((IList) this.typeStack);
          this.Pop((IList) this.valueStack);
          this.Pop((IList) this.attrStack);
          int iValue = (int) t.iValue;
          this._PerformAttr(iValue, false, true);
          if (!(this.topValue is ArrayHolder))
            throw new EAbort(ExpertResult.Aborted, LocalizationHolder.rm.GetString("Expert.Server_288"));
          object obj = (this.topValue as ArrayHolder)[int32, y];
          this.Pop((IList) this.valueStack);
          this.valueStack.Add(obj);
          DataType dataType = DataTypeConvertor.AttrType2DataType(this.tf.pairNames[iValue].ft);
          this.Pop((IList) this.typeStack);
          this.typeStack.Add(dataType);
          break;
      }
      return true;
    }

    internal bool PerformFunc(Token t)
    {
      FuncData fd = ExpertFunc.funcs(t.info);
      if (fd.func == FormulaFunc.STR)
        return this._PerformAttr((int) t.iValue, true);
      if (fd.func == FormulaFunc.skipNull)
      {
        int iValue = (int) t.iValue;
        bool flag = false;
        try
        {
          flag = this._PerformAttr(iValue, true);
        }
        catch (ExpertServerException ex)
        {
        }
        if (!flag)
        {
          ++this.curCmd;
          this.typeStack.Add(DataType.String);
          this.valueStack.Add((object) "");
          this.attrStack.Add(iValue);
        }
        return true;
      }
      if (fd.func == FormulaFunc.skipNull_0 || fd.func == FormulaFunc.skipNull_1)
      {
        int iValue = (int) t.iValue;
        bool flag = false;
        try
        {
          flag = this._PerformAttr(iValue, false);
        }
        catch (ExpertServerException ex)
        {
        }
        if (!flag)
        {
          ++this.curCmd;
          this.typeStack.Add(DataType.Float);
          this.valueStack.Add((object) (fd.func == FormulaFunc.skipNull_0 ? 0.0 : 1.0));
          this.attrStack.Add(iValue);
        }
        return true;
      }
      if (fd.func == FormulaFunc.def)
      {
        AttribPair usedAttr = this.tf.usedAttrs[(int) t.iValue];
        bool flag = this.OperDef(this.taskId, usedAttr.objTypeID, usedAttr.attribID, this.objID, this.row);
        ++this.curCmd;
        this.typeStack.Add(DataType.Boolean);
        this.valueStack.Add((object) flag);
        this.AddZeroAttr();
        return true;
      }
      if (fd.func == FormulaFunc.flag_a)
      {
        int int32_1 = Convert.ToInt32(t.iValue);
        long objId = this.objID;
        AttribPair usedAttr = this.tf.usedAttrs[int32_1];
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(usedAttr.attribID);
        if (attributeType != null)
        {
          if (this.ti.GetTempAttrStru(attributeType.AttributeGuid).HasFlag((Enum) ExpertServer.TempAttrStru.TempWithout))
            objId = -1L;
          if (attributeType.FieldType != FieldTypes.ftInteger)
            throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_109"), (object) attributeType.Name));
        }
        object parmValue = this.expServ._GetParmValue(this.ti, objId, usedAttr.objTypeID, usedAttr.attribID);
        if (parmValue == null)
        {
          if (this.row != null)
          {
            int indexByName = this.row.Columns.GetIndexByName(this.tf.attrGUIDs[int32_1]);
            if (indexByName >= 0)
              parmValue = this.row[indexByName];
          }
          else
          {
            this.LoadObject();
            if (this.idbO != null)
            {
              if (usedAttr.attribID > 0)
              {
                IDBAttribute attributeById = this.idbO.GetAttributeByID(usedAttr.attribID);
                if (attributeById != null)
                  parmValue = attributeById.Value;
              }
              else
              {
                object[] valuesById = this.idbO.GetValuesByID(usedAttr.attribID, false);
                if (valuesById != null)
                  parmValue = valuesById[0];
              }
            }
          }
        }
        long int64 = !parmValue.IsNullOrDBNull() ? Convert.ToInt64(parmValue) : 0L;
        this.typeStack.Add(DataType.Boolean);
        int int32_2 = Convert.ToInt32(t.fValue);
        this.valueStack.Add((object) (((ulong) int64 & (ulong) (1 << int32_2 - 1)) > 0UL));
        this.AddZeroAttr();
        return true;
      }
      if (fd.func == FormulaFunc.str_list)
      {
        if (this.valueStack.Count < 1 || this.typeStack[this.typeStack.Count - 1] != DataType.String)
          throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_273"));
        string Divider = Convert.ToString(this.valueStack[this.valueStack.Count - 1]);
        this.typeStack.RemoveAt(this.typeStack.Count - 1);
        this.valueStack.RemoveAt(this.valueStack.Count - 1);
        this.attrStack.RemoveAt(this.attrStack.Count - 1);
        AttribPair usedAttr = this.tf.usedAttrs[(int) t.iValue];
        string str = this.expServ.OperStrList(this.taskId, usedAttr.objTypeID, usedAttr.attribID, -1, Divider, this.objID, this.row);
        ++this.curCmd;
        this.typeStack.Add(DataType.String);
        this.valueStack.Add((object) str);
        this.AddZeroAttr();
        return true;
      }
      if (fd.func == FormulaFunc.ref_list)
      {
        if (this.valueStack.Count < 1 || this.typeStack[this.typeStack.Count - 1] != DataType.String)
          throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_274"));
        string Divider = Convert.ToString(this.valueStack[this.valueStack.Count - 1]);
        this.typeStack.RemoveAt(this.typeStack.Count - 1);
        this.valueStack.RemoveAt(this.valueStack.Count - 1);
        this.attrStack.RemoveAt(this.attrStack.Count - 1);
        AttribPair usedAttr1 = this.tf.usedAttrs[(int) t.iValue];
        AttribPair usedAttr2 = this.tf.usedAttrs[Convert.ToInt32(Math.Round(t.fValue))];
        string str = this.expServ.OperStrList(this.taskId, usedAttr2.objTypeID, usedAttr2.attribID, usedAttr1.attribID, Divider, this.objID, this.row);
        ++this.curCmd;
        this.typeStack.Add(DataType.String);
        this.valueStack.Add((object) str);
        this.AddZeroAttr();
        return true;
      }
      int length = fd.parmTypes.Length;
      if (this.typeStack.Count < length)
        throw new ExpertServerException(string.Format(LocalizationHolder.rm.GetString("Expert.Server_110"), (object) fd.text, (object) length.ToString()));
      ArrayList parms = new ArrayList(length);
      for (int index = 0; index < fd.parmTypes.Length; ++index)
      {
        object obj = this.valueStack[this.valueStack.Count - length + index];
        try
        {
          switch (fd.parmTypes[index])
          {
            case DataType.Integer:
            case DataType.ObjectLink:
              parms.Add((object) Convert.ToInt64(obj));
              continue;
            case DataType.Float:
              parms.Add((object) Convert.ToDouble(obj));
              continue;
            case DataType.Measured:
              parms.Add((object) (MeasuredValue) obj);
              continue;
            case DataType.String:
              parms.Add((object) Convert.ToString(obj));
              continue;
            case DataType.Date:
              parms.Add((object) Convert.ToDateTime(obj));
              continue;
            case DataType.Boolean:
              parms.Add((object) Convert.ToBoolean(obj));
              continue;
            case DataType.Packet:
              switch (obj)
              {
                case ArrayHolder _:
                  parms.Add(obj);
                  continue;
                case PacketValue _:
                  parms.Add((object) (PacketValue) obj);
                  continue;
                default:
                  parms.Add((object) new ArrayHolder(1, 1)
                  {
                    [0, 0] = obj
                  });
                  continue;
              }
            default:
              continue;
          }
        }
        catch (Exception ex)
        {
          throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert.Server_111") + ex.Message);
        }
      }
      try
      {
        if (!this.CallFunction(fd, parms))
          return false;
        ++this.curCmd;
        return true;
      }
      catch (Exception ex)
      {
        throw new ExpertServerException($"{LocalizationHolder.rm.GetString("Expert.Server_112")}{fd.text}: {ex.Message}", ex);
      }
    }

    internal bool CallFunction(FuncData fd, ArrayList parms)
    {
      int length = fd.parmTypes.Length;
      object obj1 = (object) null;
      DataType dataType = fd.result;
      if (fd.func > (FormulaFunc) 1000)
      {
        obj1 = ExpertServer.Invoke((int) fd.func, parms);
      }
      else
      {
        string Message = string.Format(LocalizationHolder.rm.GetString("Expert.Server_113"), (object) fd.text);
        MeasuredValue measuredValue1 = (MeasuredValue) null;
        switch (fd.func)
        {
          case FormulaFunc.sin:
            obj1 = (object) Math.Sin((double) parms[0]);
            break;
          case FormulaFunc.cos:
            obj1 = (object) Math.Cos((double) parms[0]);
            break;
          case FormulaFunc.tg:
            obj1 = (object) Math.Tan((double) parms[0]);
            break;
          case FormulaFunc.ln:
            obj1 = (object) Math.Log((double) parms[0]);
            break;
          case FormulaFunc.lg:
            obj1 = (object) Math.Log10((double) parms[0]);
            break;
          case FormulaFunc.atg:
            obj1 = (object) Math.Atan((double) parms[0]);
            break;
          case FormulaFunc.exp:
            obj1 = (object) Math.Exp((double) parms[0]);
            break;
          case FormulaFunc.sqrt:
            obj1 = (object) Math.Sqrt((double) parms[0]);
            break;
          case FormulaFunc.abs:
            if (this.topType == DataType.Integer)
            {
              dataType = DataType.Integer;
              obj1 = (object) Math.Abs(Convert.ToInt32(this.topValue));
              break;
            }
            obj1 = (object) Math.Abs((double) parms[0]);
            break;
          case FormulaFunc.has_child:
          case FormulaFunc.has_parent:
            obj1 = (object) this.HasFunc(fd.func, Convert.ToInt32(parms[0]), -1, this.objID);
            break;
          case FormulaFunc.has_child_link:
          case FormulaFunc.has_parent_link:
            obj1 = (object) this.HasFunc(fd.func, Convert.ToInt32(parms[0]), Convert.ToInt32(parms[1]), this.objID);
            break;
          case FormulaFunc.def:
            AttribPair parm1 = (AttribPair) parms[0];
            obj1 = (object) this.OperDef(this.taskId, parm1.objTypeID, parm1.attribID, this.objID, this.row);
            break;
          case FormulaFunc.nom:
            string str1 = ((string) parms[0]).Trim();
            string str2 = "0123456789.";
            string str3 = "/'\" ";
            bool flag = str1[0] == 'G' || str1[0] == 'R' || str1[0] == 'K';
            if (!str2.Contains(str1.Substring(0, 1)))
              str1 = str1.Substring(1);
            StringBuilder stringBuilder1 = new StringBuilder();
            for (int startIndex = 0; startIndex < str1.Length; ++startIndex)
            {
              string str4 = str1.Substring(startIndex, 1);
              if (str2.Contains(str4))
              {
                stringBuilder1.Append(str4);
              }
              else
              {
                if (str4 == "+" || str4 == "-")
                {
                  if (stringBuilder1.Length == 0)
                    stringBuilder1.Append(str4);
                  else
                    break;
                }
                if (!flag || !str3.Contains(str4))
                  break;
              }
            }
            obj1 = (object) stringBuilder1.ToString();
            break;
          case FormulaFunc.kv:
            throw new ExpertServerException(Message);
          case FormulaFunc.hi:
            throw new ExpertServerException(Message);
          case FormulaFunc.lo:
            throw new ExpertServerException(Message);
          case FormulaFunc.kt:
            throw new ExpertServerException(Message);
          case FormulaFunc.st:
            throw new ExpertServerException(Message);
          case FormulaFunc.ctn:
            throw new ExpertServerException(Message);
          case FormulaFunc.rnd:
            obj1 = (object) Math.Round((double) parms[0] + 1E-14);
            break;
          case FormulaFunc.rnde:
            double parm2 = (double) parms[0];
            int int32_1 = Convert.ToInt32(parms[1]);
            if (int32_1 >= 0)
            {
              obj1 = (object) Math.Round(parm2 + 1E-14, int32_1, MidpointRounding.AwayFromZero);
              break;
            }
            int num1 = 1;
            for (; int32_1 < 0; ++int32_1)
              num1 *= 10;
            obj1 = (object) (Math.Round(parm2 / (double) num1, 0) * (double) num1);
            break;
          case FormulaFunc.rndg:
            double parm3 = (double) parms[0];
            int int32_2 = Convert.ToInt32(parms[1]);
            string input1 = parm3.ToString("E10", (IFormatProvider) CultureInfo.InvariantCulture);
            Match match1 = new Regex(this.DoublePattern, RegexOptions.IgnoreCase).Match(input1);
            string str5 = input1.StartsWith("-") ? "-" : "";
            Group group1 = match1.Groups[1];
            Group group2 = match1.Groups[2];
            if (group1.Success && group2.Success && int32_2 >= 2)
            {
              string str6 = Convert.ToString(Math.Round(double.Parse(group1.Captures[0].Value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture), int32_2 - 1), (IFormatProvider) CultureInfo.InvariantCulture);
              string str7 = group2.Captures[0].Value;
              obj1 = (object) double.Parse($"{str5}{str6}E{str7}", NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture);
              break;
            }
            obj1 = (object) parm3;
            break;
          case FormulaFunc.Int:
            obj1 = (object) Convert.ToInt32(Math.Floor((double) parms[0]));
            if ((int) obj1 < 0)
            {
              obj1 = (object) ((int) obj1 + 1);
              break;
            }
            break;
          case FormulaFunc.frac:
            obj1 = (object) ((double) parms[0] - Math.Floor((double) parms[0]));
            break;
          case FormulaFunc.has:
            obj1 = (object) (((string) parms[0]).IndexOf((string) parms[1]) >= 0);
            break;
          case FormulaFunc.begs:
            obj1 = (object) ((string) parms[0]).StartsWith((string) parms[1]);
            break;
          case FormulaFunc.ends:
            obj1 = (object) ((string) parms[0]).EndsWith((string) parms[1]);
            break;
          case FormulaFunc.upp:
            obj1 = (object) ((string) parms[0]).ToUpper();
            break;
          case FormulaFunc.low:
            obj1 = (object) ((string) parms[0]).ToLower();
            break;
          case FormulaFunc.now:
            obj1 = (object) DateTime.Now;
            break;
          case FormulaFunc.flag:
            obj1 = (object) (((ulong) (1 << Convert.ToInt32(parms[0]) - 1) & (ulong) Convert.ToInt64(parms[1])) > 0UL);
            break;
          case FormulaFunc.rnd_m:
            MeasuredValue measuredValue2 = MeasureHelper.ConvertToMeasuredValue(((MeasuredValue) parms[0]).Caption);
            obj1 = (object) new MeasuredValue(Math.Round(measuredValue2.Value) + 1E-14, measuredValue2.MeasureID);
            break;
          case FormulaFunc.rnde_m:
            MeasuredValue measuredValue3 = MeasureHelper.ConvertToMeasuredValue(((MeasuredValue) parms[0]).Caption);
            double num2 = measuredValue3.Value;
            int int32_3 = Convert.ToInt32(parms[1]);
            if (int32_3 >= 0)
            {
              obj1 = (object) new MeasuredValue(Math.Round(num2 + 1E-14, int32_3, MidpointRounding.AwayFromZero), measuredValue3.MeasureID);
              break;
            }
            int num3 = 1;
            for (; int32_3 < 0; ++int32_3)
              num3 *= 10;
            obj1 = (object) new MeasuredValue(Math.Round(num2 / (double) num3, 0) * (double) num3, measuredValue3.MeasureID);
            break;
          case FormulaFunc.rndg_m:
            MeasuredValue parm4 = (MeasuredValue) parms[0];
            MeasuredValue measuredValue4 = MeasureHelper.ConvertToMeasuredValue(parm4.Caption);
            double num4 = measuredValue4.Value;
            int int32_4 = Convert.ToInt32(parms[1]);
            string input2 = num4.ToString("E10", (IFormatProvider) CultureInfo.InvariantCulture);
            Match match2 = new Regex(this.DoublePattern, RegexOptions.IgnoreCase).Match(input2);
            string str8 = input2.StartsWith("-") ? "-" : "";
            Group group3 = match2.Groups[1];
            Group group4 = match2.Groups[2];
            if (group3.Success && group4.Success && int32_4 >= 2)
            {
              string str9 = Convert.ToString(Math.Round(double.Parse(group3.Captures[0].Value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture), int32_4 - 1), (IFormatProvider) CultureInfo.InvariantCulture);
              string str10 = group4.Captures[0].Value;
              obj1 = (object) new MeasuredValue(double.Parse($"{str8}{str9}E{str10}", NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture), measuredValue4.MeasureID);
              break;
            }
            obj1 = (object) parm4;
            break;
          case FormulaFunc.Int_m:
            MeasuredValue measuredValue5 = MeasureHelper.ConvertToMeasuredValue(((MeasuredValue) parms[0]).Caption);
            MeasuredValue measuredValue6 = new MeasuredValue((double) Convert.ToInt32(Math.Floor(measuredValue5.Value)), measuredValue5.MeasureID);
            if ((int) measuredValue6.Value < 0)
              measuredValue6.Value = (double) ((int) measuredValue6.Value + 1);
            obj1 = (object) measuredValue6;
            break;
          case FormulaFunc.frac_m:
            MeasuredValue measuredValue7 = MeasureHelper.ConvertToMeasuredValue(((MeasuredValue) parms[0]).Caption);
            obj1 = (object) new MeasuredValue(measuredValue7.Value - Math.Floor(measuredValue7.Value), measuredValue7.MeasureID);
            break;
          case FormulaFunc.date:
            DateTime dateTime = Convert.ToDateTime(parms[0]);
            obj1 = this.ti != null ? (object) dateTime.ToString("d", (IFormatProvider) this.ti.dfi) : (object) dateTime.ToString("d");
            break;
          case FormulaFunc.num:
            MeasuredValue parm5 = (MeasuredValue) parms[0];
            measuredValue1 = MeasureHelper.ConvertToMeasuredValue(parm5.Caption);
            obj1 = (object) parm5.Value;
            break;
          case FormulaFunc.s_int:
            obj1 = (object) Convert.ToInt64(Convert.ToString(parms[0]));
            break;
          case FormulaFunc.s_float:
            obj1 = (object) Convert.ToDouble(Convert.ToString(parms[0]));
            break;
          case FormulaFunc.s_measured:
            obj1 = (object) MeasureHelper.ConvertToMeasuredValue(Convert.ToString(parms[0]));
            break;
          case FormulaFunc.isp_num:
            obj1 = this.ti == null || this.ti.ispList == null ? (object) -1 : (object) this.ti.ispList.IndexOf(Convert.ToInt64(parms[0]));
            break;
          case FormulaFunc.len:
            obj1 = (object) ((string) parms[0]).Length;
            break;
          case FormulaFunc.pos:
            obj1 = (object) (Convert.ToString(parms[0]).IndexOf(Convert.ToString(parms[1])) + 1);
            break;
          case FormulaFunc.substr:
            string str11 = Convert.ToString(parms[0]);
            int startIndex1 = Convert.ToInt32(parms[1]) - 1;
            if (startIndex1 < 0)
              startIndex1 = 0;
            int int32_5 = Convert.ToInt32(parms[2]);
            try
            {
              obj1 = startIndex1 + int32_5 >= str11.Length ? (object) str11.Substring(startIndex1) : (object) str11.Substring(startIndex1, int32_5);
              break;
            }
            catch
            {
              obj1 = (object) "";
              break;
            }
          case FormulaFunc.value:
            obj1 = (object) MeasureHelper.ConvertToMeasuredValue(((MeasuredValue) parms[0]).Caption).Value;
            break;
          case FormulaFunc.unit:
            obj1 = (object) MeasureHelper.FindDescriptor(MeasureHelper.ConvertToMeasuredValue(((MeasuredValue) parms[0]).Caption).MeasureID).ShortName;
            break;
          case FormulaFunc.val2:
            string str12 = Convert.ToString(parms[0]);
            string str13 = Convert.ToString(parms[1]);
            obj1 = !(str13 == "") ? (object) (str12 + str13) : (object) "";
            break;
          case FormulaFunc.val3:
            string str14 = Convert.ToString(parms[0]);
            string str15 = Convert.ToString(parms[1]);
            string str16 = Convert.ToString(parms[2]);
            obj1 = !(str15 == "") ? (object) (str14 + str15 + str16) : (object) "";
            break;
          case FormulaFunc.no_sht:
            MeasuredValue parm6 = (MeasuredValue) parms[0];
            MeasuredValue measuredValue8 = MeasureHelper.ConvertToMeasuredValue(parm6.Caption);
            obj1 = MeasureHelper.FindDescriptor(measuredValue8.MeasureID).MeasureID != ExpertConsts.Consts.measureShtuk ? (object) parm6.Caption : (object) Convert.ToString(measuredValue8.Value);
            break;
          case FormulaFunc.child:
          case FormulaFunc.parent:
            long int64_1 = Convert.ToInt64(parms[0]);
            obj1 = (object) this.LinkedObject(fd.func, this.objID, int64_1);
            break;
          case FormulaFunc.to_MU:
            MeasuredValue parm7 = (MeasuredValue) parms[0];
            MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(Convert.ToString(parms[1]));
            obj1 = descriptor1.Empty ? (object) parm7 : (object) new MeasuredValue(MeasureHelper.ConvertToBaseMeasure(parm7).Value / descriptor1.K, descriptor1.MeasureID);
            break;
          case FormulaFunc.expanded:
            long int64_2 = Convert.ToInt64(parms[0]);
            obj1 = (object) true;
            if (this.ti != null && this.ti._notExpandedObjIds != null && this.ti._notExpandedObjIds.Contains(Math.Abs(int64_2)))
            {
              obj1 = (object) false;
              break;
            }
            break;
          case FormulaFunc.unbreak_space:
            obj1 = (object) Convert.ToString(parms[0]).Replace(' ', '\u000E').Replace('-', '\u0017');
            break;
          case FormulaFunc.obj_child:
          case FormulaFunc.obj_parent:
            long int64_3 = Convert.ToInt64(parms[0]);
            long int64_4 = Convert.ToInt64(parms[1]);
            obj1 = (object) this.LinkedObject(fd.func, int64_3, int64_4);
            break;
          case FormulaFunc.clos_min:
          case FormulaFunc.clos_max:
            double num5 = Convert.ToDouble(parms[0]);
            PacketValue pvalue1 = this.ArrayToPValue(parms[1]);
            double num6 = num5;
            for (int index = 0; index < pvalue1.Count; ++index)
            {
              ExpertValue expertValue = pvalue1[index];
              if (expertValue.ValueType == DataType.Float || expertValue.ValueType == DataType.Integer)
              {
                double num7 = Convert.ToDouble(expertValue.Value);
                if (fd.func == FormulaFunc.clos_min)
                {
                  if (num5 < num7)
                    break;
                }
                else if (num5 < num7)
                {
                  num6 = num7;
                  break;
                }
                num6 = num7;
              }
            }
            obj1 = (object) num6;
            break;
          case FormulaFunc.clos_min_m:
          case FormulaFunc.clos_max_m:
            MeasuredValue parm8 = (MeasuredValue) parms[0];
            PacketValue pvalue2 = this.ArrayToPValue(parms[1]);
            MeasuredValue measuredValue9 = parm8;
            for (int index = 0; index < pvalue2.Count; ++index)
            {
              ExpertValue expertValue = pvalue2[index];
              if (expertValue.ValueType == DataType.Measured)
              {
                MeasuredValue measuredValue10 = MeasureHelper.ConvertToMeasuredValue((string) expertValue.Value);
                if (fd.func == FormulaFunc.clos_min_m)
                {
                  if (MeasureHelper.Substract(parm8, measuredValue10).Value < 0.0)
                    break;
                }
                else if (MeasureHelper.Substract(parm8, measuredValue10).Value < 0.0)
                {
                  measuredValue9 = measuredValue10;
                  break;
                }
                measuredValue9 = measuredValue10;
              }
            }
            obj1 = (object) measuredValue9;
            break;
          case FormulaFunc.ref_list:
            break;
          case FormulaFunc.time_diff:
            obj1 = (object) (((DateTime) parms[0]).Ticks - ((DateTime) parms[1]).Ticks);
            break;
          case FormulaFunc.str_by_div:
            ArrayHolder parm9 = parms[0] is ArrayHolder ? (ArrayHolder) parms[0] : (ArrayHolder) null;
            PacketValue pvalue3 = parms[0] is PacketValue ? this.ArrayToPValue(parms[0]) : (PacketValue) null;
            string str17 = Convert.ToString(parms[1]);
            if (this.attrStack.Count > 0 && this.attrStack[0] != -1 && MetaDataHelper.GetAttributeType(this.tf.usedAttrs[this.attrStack[0]].attribID).FieldType == FieldTypes.ftObjectLink)
            {
              if (pvalue3 != null)
              {
                for (int index = 0; index < pvalue3.Count; ++index)
                {
                  QuickObjectInfo objectInfo = this.ius.GetObjectInfo(Convert.ToInt64(pvalue3[index].Value));
                  if (!objectInfo.Empty)
                    pvalue3[index] = new ExpertValue(objectInfo.Caption);
                }
              }
              else
              {
                for (int index1 = 0; index1 < parm9.Height; ++index1)
                {
                  for (int index2 = 0; index2 < parm9.Width; ++index2)
                  {
                    QuickObjectInfo objectInfo = this.ius.GetObjectInfo(Convert.ToInt64(parm9[index2, index1]));
                    if (!objectInfo.Empty)
                      parm9[index1, index2] = (object) new ExpertValue(objectInfo.Caption);
                  }
                }
              }
            }
            StringBuilder stringBuilder2 = new StringBuilder();
            if (pvalue3 != null)
            {
              for (int index = 0; index < pvalue3.Count; ++index)
              {
                ExpertValue expertValue = pvalue3[index];
                if (stringBuilder2.Length > 0 && !stringBuilder2.ToString().EndsWith(str17))
                  stringBuilder2.Append(str17);
                stringBuilder2.Append(Convert.ToString((object) expertValue));
              }
            }
            else
            {
              for (int y = 0; y < parm9.Height; ++y)
              {
                for (int x = 0; x < parm9.Width; ++x)
                {
                  object obj2 = parm9[x, y];
                  if (stringBuilder2.Length > 0 && !stringBuilder2.ToString().EndsWith(str17))
                    stringBuilder2.Append(str17);
                  stringBuilder2.Append(Convert.ToString(obj2));
                }
              }
            }
            if (stringBuilder2.ToString().EndsWith(str17))
              stringBuilder2.Remove(stringBuilder2.Length - str17.Length, str17.Length);
            obj1 = (object) stringBuilder2.ToString();
            break;
          case FormulaFunc.classify:
            obj1 = (object) this.DoClassify((long) parms[0], (long) parms[1]);
            break;
          case FormulaFunc.ra:
          case FormulaFunc.ra2:
            obj1 = (object) this.ra2(Convert.ToDouble(parms[0]), this.ArrayToPValue(parms[1], DataType.Float), fd.func == FormulaFunc.ra ? 0.7 : Convert.ToDouble(parms[2]));
            break;
          case FormulaFunc.ra_m:
          case FormulaFunc.ra2_m:
            obj1 = (object) this.ra2_m((MeasuredValue) parms[0], this.ArrayToPValue(parms[1], DataType.Measured), fd.func == FormulaFunc.ra_m ? 0.7 : Convert.ToDouble(parms[2]));
            break;
          case FormulaFunc.em_Code:
            obj1 = (object) ((MeasuredValue) parms[0]).MeasureID;
            break;
          case FormulaFunc.dt_Name:
            obj1 = (object) this.GetDocumentNameByType(Convert.ToInt32(parms[0]));
            break;
          case FormulaFunc.minus:
            obj1 = (object) this.MinusDouble(Convert.ToDouble(parms[0]));
            break;
          case FormulaFunc.minus_m:
            obj1 = (object) this.MinusMeasured((MeasuredValue) parms[0]);
            break;
          case FormulaFunc.formt:
            obj1 = (object) this.PerformFormt(this.ArrayToPValue(parms[0]), Convert.ToString(parms[1]));
            break;
          case FormulaFunc.MU_coeff:
            MeasureDescriptor descriptor2 = MeasureHelper.FindDescriptor((MeasuredValue) parms[0]);
            obj1 = (object) (descriptor2.Empty ? 1.0 : descriptor2.K);
            break;
          case FormulaFunc.str_replace:
            string str18 = Convert.ToString(parms[0]);
            string str19 = Convert.ToString(parms[1]);
            string str20 = Convert.ToString(parms[2]);
            string oldValue = str19;
            string newValue = str20;
            obj1 = (object) str18.Replace(oldValue, newValue);
            break;
          case FormulaFunc.trim:
            obj1 = (object) Convert.ToString(parms[0]).Trim().TrimEnd('\n');
            break;
          default:
            obj1 = (object) null;
            break;
        }
      }
      for (int index = 0; index < length; ++index)
      {
        this.Pop((IList) this.typeStack);
        this.Pop((IList) this.valueStack);
        this.Pop((IList) this.attrStack);
      }
      if (this.tf.DropMeasure && dataType == DataType.Measured && obj1 is MeasuredValue)
      {
        this.typeStack.Add(DataType.Float);
        this.valueStack.Add((object) ((MeasuredValue) obj1).Value);
      }
      else
      {
        this.typeStack.Add(dataType);
        this.valueStack.Add(obj1);
      }
      this.AddZeroAttr();
      return true;
    }

    public static (bool, ulong, uint) UnpackDouble(double d)
    {
      long int64Bits = BitConverter.DoubleToInt64Bits(d);
      bool flag = (int64Bits & long.MinValue) != 0L;
      uint num1 = (uint) ((ulong) (int64Bits >> 52) & 2047UL /*0x07FF*/);
      ulong num2 = (ulong) (int64Bits & 4503599627370495L /*0x0FFFFFFFFFFFFF*/);
      if (num1 == 0U)
        ++num1;
      else
        num2 |= 4503599627370496UL /*0x10000000000000*/;
      uint num3 = num1 - 1075U;
      if (num2 != 0UL)
      {
        while (((long) num2 & 1L) == 0L)
        {
          num2 >>= 1;
          ++num3;
        }
      }
      return (flag, num2, num3);
    }

    public static double PackDouble(bool i1, ulong i2, uint i3)
    {
      ulong num = i2 & 4503599627370495UL /*0x0FFFFFFFFFFFFF*/ | (ulong) (i3 << 20);
      if (i1)
        num |= 9223372036854775808UL /*0x8000000000000000*/;
      return BitConverter.Int64BitsToDouble((long) num);
    }

    internal bool DoClassify(long objId, long folderId)
    {
      IObjectClassificator objectClassificator = ((ISelectionsService) ServerServices.GetService(typeof (ISelectionsService))).GetObjectClassificator((object) this.ius.SessionGUID, folderId);
      new long[1][0] = objId;
      AttributeValues[] clasificatorAttributes = objectClassificator.GetClasificatorAttributes(objId);
      if (clasificatorAttributes != null)
      {
        foreach (AttributeValues attributeValues in clasificatorAttributes)
        {
          XmlNode xmlNode = this.ti.traceAddElement(string.Format(LocalizationHolder.rm.GetString("Expert.Server_278"), (object) objId));
          XmlNode curNode = this.ti.curNode;
          this.ti.traceSetNode(xmlNode);
          try
          {
            if (attributeValues.Values.Length != 0)
            {
              object obj = attributeValues.Values[0];
              ExpertServer.es.InnerSetParm(this.ti, new CalcAttrPair(objId, attributeValues.AttributeID), obj, AttrState.Calculated);
              this.ti.traceAddAttribute(xmlNode, "Attr_" + Convert.ToString(attributeValues.AttributeID), Convert.ToString(obj));
            }
          }
          finally
          {
            this.ti.traceSetNode(curNode);
          }
        }
      }
      return clasificatorAttributes != null;
    }

    internal PacketValue ArrayToPValue(object Parm, DataType dt)
    {
      switch (Parm)
      {
        case PacketValue _:
          return (PacketValue) Parm;
        case ArrayHolder _:
          return new PacketValue((IEnumerable) Parm, dt);
        default:
          return (PacketValue) null;
      }
    }

    internal PacketValue ArrayToPValue(object Parm)
    {
      switch (Parm)
      {
        case PacketValue _:
          return (PacketValue) Parm;
        case ArrayHolder _:
          ArrayHolder arrayHolder = (ArrayHolder) Parm;
          PacketValue pvalue = new PacketValue();
          for (int y = 0; y < arrayHolder.Height; ++y)
          {
            for (int x = 0; x < arrayHolder.Width; ++x)
            {
              if (arrayHolder[x, y] != null)
              {
                if (arrayHolder[x, y] is long)
                  pvalue.Add(new ExpertValue((double) (long) arrayHolder[x, y]));
                if (arrayHolder[x, y] is double)
                  pvalue.Add(new ExpertValue((double) arrayHolder[x, y]));
                if (arrayHolder[x, y] is MeasuredValue)
                  pvalue.Add(new ExpertValue((MeasuredValue) arrayHolder[x, y]));
              }
            }
          }
          return pvalue;
        default:
          return (PacketValue) null;
      }
    }

    internal string MinusDouble(double d)
    {
      return d < 0.0 ? $"{this.minus} {Convert.ToString(Math.Abs(d))}" : Convert.ToString(d);
    }

    internal string MinusMeasured(MeasuredValue mv)
    {
      return mv.Value < 0.0 ? $"{this.minus} {MeasureHelper.ConvertToString(Math.Abs(mv.Value), mv.MeasureID, false)}" : mv.Caption;
    }

    internal string PerformFormt(PacketValue pv, string format)
    {
      bool flag = format.Contains("m");
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < pv.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(", ");
        ExpertValue expertValue = pv[index];
        switch (expertValue.ValueType)
        {
          case DataType.Integer:
            string str1 = flag ? this.MinusDouble((double) (long) expertValue.Value) : Convert.ToString((long) expertValue.Value);
            stringBuilder.Append(str1);
            break;
          case DataType.Float:
            string str2 = flag ? this.MinusDouble((double) expertValue.Value) : Convert.ToString((double) expertValue.Value);
            stringBuilder.Append(str2);
            break;
          case DataType.Measured:
            string mValue1 = (string) expertValue.Value;
            string str3 = flag ? this.MinusMeasured(MeasureHelper.ConvertToMeasuredValue(mValue1)) : mValue1;
            stringBuilder.Append(str3);
            break;
          case DataType.Diap:
            DiapValue diapValue = (DiapValue) expertValue.Value;
            switch (diapValue.Low.ValueType)
            {
              case DataType.Float:
                string str4 = flag ? this.MinusDouble((double) diapValue.Low.Value) : Convert.ToString((double) diapValue.Low.Value);
                stringBuilder.Append(str4);
                break;
              case DataType.Measured:
                string mValue2 = (string) diapValue.Low.Value;
                string str5 = flag ? this.MinusMeasured(MeasureHelper.ConvertToMeasuredValue(mValue2)) : mValue2;
                stringBuilder.Append(str5);
                break;
              default:
                stringBuilder.Append(diapValue.Low.Value.ToString());
                break;
            }
            stringBuilder.Append(" : ");
            switch (diapValue.High.ValueType)
            {
              case DataType.Float:
                string str6 = flag ? this.MinusDouble((double) diapValue.High.Value) : Convert.ToString((double) diapValue.High.Value);
                stringBuilder.Append(str6);
                continue;
              case DataType.Measured:
                string mValue3 = (string) diapValue.High.Value;
                string str7 = flag ? this.MinusMeasured(MeasureHelper.ConvertToMeasuredValue(mValue3)) : mValue3;
                stringBuilder.Append(str7);
                continue;
              default:
                stringBuilder.Append(diapValue.High.Value.ToString());
                continue;
            }
          default:
            stringBuilder.Append(expertValue.Value.ToString());
            break;
        }
      }
      return stringBuilder.ToString();
    }

    internal void ValidateFactor(ref double factor)
    {
      if (factor > 0.0 && factor < 1.0)
        return;
      factor = 0.7;
    }

    internal double performDiap(double Value, double v1, double v2, double factor)
    {
      return (Value - v1) / (v2 - v1) <= factor ? v1 : v2;
    }

    internal MeasuredValue performDiap_m(
      MeasuredValue Value,
      MeasuredValue v1,
      MeasuredValue v2,
      double factor)
    {
      return MeasureHelper.ConvertToBaseMeasure(MeasureHelper.Substract(Value, v1)).Value / MeasureHelper.ConvertToBaseMeasure(MeasureHelper.Substract(v2, v1)).Value <= factor ? v1 : v2;
    }

    internal double ra2(double value, PacketValue pv, double factor)
    {
      this.ValidateFactor(ref factor);
      double num = value;
      double v1 = double.MinValue;
      for (int index = 0; index < pv.Count; ++index)
      {
        ExpertValue expertValue = pv[index];
        if (expertValue.ValueType == DataType.Float || expertValue.ValueType == DataType.Integer)
        {
          double v2 = Convert.ToDouble(expertValue.Value);
          if (v2 > num)
          {
            num = index <= 0 ? v2 : this.performDiap(value, v1, v2, factor);
            break;
          }
          v1 = v2;
        }
      }
      return num;
    }

    internal MeasuredValue ra2_m(MeasuredValue mv, PacketValue pv, double factor)
    {
      this.ValidateFactor(ref factor);
      MeasuredValue operand2 = mv;
      MeasuredValue v1 = new MeasuredValue(double.MinValue, mv.MeasureID);
      for (int index = 0; index < pv.Count; ++index)
      {
        ExpertValue expertValue = pv[index];
        if (expertValue.ValueType == DataType.Measured)
        {
          MeasuredValue measuredValue = (MeasuredValue) expertValue.Value;
          if (MeasureHelper.Substract(measuredValue, operand2).Value > 0.0)
          {
            operand2 = index <= 0 ? measuredValue : this.performDiap_m(mv, v1, measuredValue, factor);
            break;
          }
          v1 = measuredValue;
        }
      }
      return operand2;
    }

    internal string GetErrorHeading(Token t)
    {
      return $"{LocalizationHolder.rm.GetString("Expert.Server_114")}{t.text.Trim()}\": ";
    }

    internal void CheckStackType(Token t, bool Top, params DataType[] types)
    {
      if (this.typeStack.Count <= 0)
        throw new ExpertServerException(this.GetErrorHeading(t) + LocalizationHolder.rm.GetString("Expert.Server_115"));
      if (!Top && this.typeStack.Count < 2)
        throw new ExpertServerException(this.GetErrorHeading(t) + LocalizationHolder.rm.GetString("Expert.Server_116"));
      DataType dt = !Top ? this.typeStack[this.typeStack.Count - 2] : this.typeStack[this.typeStack.Count - 1];
      bool flag = false;
      for (int index = 0; index < types.Length; ++index)
      {
        if (dt == types[index])
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        throw new ExpertServerException($"{this.GetErrorHeading(t)}{LocalizationHolder.rm.GetString("Expert.Server_117")}{DataTypeConvertor.DataTypeName(dt)})");
    }

    internal bool HasFunc(FormulaFunc ff, int objTypeId, int relTypeId, long objID)
    {
      ExpertServer.ExpServTask task = this.expServ.GetTask(this.taskId);
      if (objID == -1L)
        objID = Convert.ToInt64(this.expServ.InnerGetParm(task, ExpertConsts.Consts.attrCurContextId));
      if (task.savedLinks != null)
      {
        int indexByName = task.savedLinks.Columns.GetIndexByName("cad00036-306c-11d8-b4e9-00304f19f545");
        if (ff == FormulaFunc.has_child || ff == FormulaFunc.has_child_link)
        {
          HybridRowExp[] hybridRowExpArray = task.savedLinksByProjId(objID);
          if (hybridRowExpArray != null)
          {
            foreach (HybridRowExp hybridRowExp1 in hybridRowExpArray)
            {
              if (ff != FormulaFunc.has_child_link || indexByName != -1 && Convert.ToInt32(hybridRowExp1[indexByName]) == relTypeId)
              {
                long int64 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
                HybridRowExp hybridRowExp2 = task.savedDataByPartId(int64);
                if (hybridRowExp2 != null && Convert.ToInt32(hybridRowExp2["cad0002e-306c-11d8-b4e9-00304f19f545"]) == objTypeId)
                  return true;
              }
            }
          }
        }
        else
        {
          HybridRowExp[] hybridRowExpArray = task.savedLinksByPartId(objID);
          if (hybridRowExpArray != null)
          {
            foreach (HybridRowExp hybridRowExp3 in hybridRowExpArray)
            {
              if (ff != FormulaFunc.has_parent_link || indexByName != -1 && Convert.ToInt32(hybridRowExp3["cad00036-306c-11d8-b4e9-00304f19f545"]) == relTypeId)
              {
                long int64 = Convert.ToInt64(hybridRowExp3["cad00034-306c-11d8-b4e9-00304f19f545"]);
                HybridRowExp hybridRowExp4 = task.savedDataByPartId(int64);
                if (hybridRowExp4 != null && Convert.ToInt32(hybridRowExp4["cad0002e-306c-11d8-b4e9-00304f19f545"]) == objTypeId)
                  return true;
              }
            }
          }
        }
      }
      if (this.relationId != 0L && (ff == FormulaFunc.has_parent || ff == FormulaFunc.has_parent_link))
      {
        IDBRelation relation = this.ius.GetRelation(this.relationId, false);
        if (relation != null && (ff != FormulaFunc.has_parent_link || relTypeId == relation.RelationType))
          return this.ius.GetObjectInfo(relation.ProjID).ObjectTypeID == objTypeId;
      }
      TypedInfoItem itemData = task.DataCache.GetItemData(objID, this.ius);
      if (itemData is TaskDataCache.RelDataItem)
      {
        IDBObject relPartObj = ExpertServer.Calculator.GetRelPartObj(this.ius, this.ius.GetRelation((itemData as TaskDataCache.RelDataItem).RelationID, false), task);
        if (relPartObj != null)
          objID = relPartObj.ObjectID;
      }
      IDBRelationCollection relationCollection = this.ius.GetRelationCollection(relTypeId);
      ConditionStructure[] conditions = (ConditionStructure[]) null;
      if (objTypeId != -1)
        conditions = new ConditionStructure[1]
        {
          this.GetObjTypeConds(objTypeId)
        };
      DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[3]
      {
        (object) -21,
        (object) -22,
        (object) -2
      });
      paramSet.Tags = task.filtr();
      relationCollection.LocalTypesMode = true;
      return (ff == FormulaFunc.has_child || ff == FormulaFunc.has_child_link ? relationCollection.ConsistFrom(paramSet, objID) : relationCollection.EntersInVersion(paramSet, objID)).Rows.Count > 0;
    }

    public static IDBObject GetRelPartObj(
      IUserSession ius,
      IDBRelation rel,
      ExpertServer.ExpServTask ti)
    {
      if (rel == null)
        return (IDBObject) null;
      IDBAttribute attributeById = rel.GetAttributeByID(ExpertConsts.Consts.attrVerSostav);
      IDBObject objectByVersionsRule;
      if (attributeById == null)
      {
        objectByVersionsRule = ius.GetObjectByVersionsRule(rel.PartID, ti.verRuleOwnerId, false);
      }
      else
      {
        long int64 = Convert.ToInt64(attributeById.Value);
        objectByVersionsRule = ius.GetObject(int64, false);
      }
      return objectByVersionsRule;
    }

    private ConditionStructure GetObjTypeConds(int objType)
    {
      return new ConditionStructure(-7, RelationalOperators.Equal, (object) objType, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Value);
    }

    internal bool LinkedObject(FormulaFunc ff, long objID, long otherObjID)
    {
      ExpertServer.ExpServTask task = this.expServ.GetTask(this.taskId);
      if (objID == -1L)
        objID = Convert.ToInt64(this.expServ.InnerGetParm(task, ExpertConsts.Consts.attrCurContextId));
      IDBRelationCollection relationCollection = this.ius.GetRelationCollection(-1);
      if (ff == FormulaFunc.parent || ff == FormulaFunc.obj_parent)
      {
        HybridRowExp[] hybridRowExpArray = task.savedLinksByProjId(objID);
        if (hybridRowExpArray != null)
        {
          foreach (HybridRowExp hybridRowExp1 in hybridRowExpArray)
          {
            long int64 = Convert.ToInt64(hybridRowExp1["cad00035-306c-11d8-b4e9-00304f19f545"]);
            HybridRowExp hybridRowExp2 = task.savedDataByPartId(int64);
            if (hybridRowExp2 != null && Math.Abs(Convert.ToInt64(hybridRowExp2[0])) == Math.Abs(otherObjID))
              return true;
          }
        }
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.Equal, (object) otherObjID, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Value)
        }, new object[3]
        {
          (object) -21,
          (object) -22,
          (object) -2
        });
        paramSet.Tags = task.filtr();
        relationCollection.LocalTypesMode = true;
        return relationCollection.ConsistFrom(paramSet, objID).Rows.Count > 0;
      }
      TaskDataCache.ObjDataItem objData = task.DataCache.GetObjData(objID, this.ius);
      HybridRowExp[] hybridRowExpArray1 = task.savedLinksByPartId(objData.Id);
      if (hybridRowExpArray1 != null)
      {
        foreach (HybridRowExp hybridRowExp in hybridRowExpArray1)
        {
          if (Math.Abs(Convert.ToInt64(hybridRowExp["cad00034-306c-11d8-b4e9-00304f19f545"])) == Math.Abs(otherObjID))
            return true;
        }
      }
      DBRecordSetParams paramSet1 = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-21, RelationalOperators.Equal, (object) otherObjID, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation, ColumnContents.ID)
      }, new object[3]
      {
        (object) -21,
        (object) -22,
        (object) -2
      });
      paramSet1.Tags = task.filtr();
      relationCollection.LocalTypesMode = true;
      return relationCollection.EntersIn(paramSet1, objData.Id).Rows.Count > 0;
    }

    private bool OperDef(int taskId, int objTypeId, int attrTypeId, long objId, HybridRowExp row)
    {
      object val = this.expServ._GetParmValue(this.ti, objId, objTypeId, attrTypeId);
      if (val == null && objTypeId != -1)
      {
        int typeId = ExpertServer.GetTypeId(this.ius, objId);
        if (ExpertServer.IsTypeDescendant(objTypeId, typeId))
          val = this.expServ._GetParmValue(this.ti, objId, -1, attrTypeId);
      }
      if (val != null)
        return true;
      if (!this.ti.IsTempAttrWithObject(attrTypeId) && !this.ti.IsTempAttrWithoutObject(attrTypeId))
      {
        if (row != null && objTypeId == -1)
        {
          string columnName = MetaDataHelper.GetAttributeTypeGuid(attrTypeId).ToString();
          if (columnName != "")
          {
            int indexByName = row.Columns.GetIndexByName(columnName);
            if (indexByName >= 0)
              return row[indexByName].NotNullOrDBNull();
          }
        }
        if (objTypeId == -1 && objId != -1L)
        {
          IDBObject dbObject = this.ius.GetObject(objId, false);
          if (dbObject != null && (attrTypeId < 0 || dbObject.GetAttributeByID(attrTypeId) != null))
            return true;
        }
      }
      return this.expServ.CalculateAttr(taskId, objTypeId, attrTypeId, objId, ExpertServer.CalcStages.CheckObject | ExpertServer.CalcStages.FindObject, out val, moreObjIDs: this.objIDs) == ExpertResult.OK;
    }

    internal string GetDocumentNameByType(Guid sessionGuid, int documentType)
    {
      return UserSession.GetSessionByID(sessionGuid).GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService customService ? customService.GetSettings(sessionGuid, documentType).DocumentTypeName : "";
    }

    internal string GetDocumentNameByType(int documentType)
    {
      return this.ius.GetCustomService(typeof (IDocumentTypeSettingsService)) is IDocumentTypeSettingsService customService ? customService.GetSettings(this.ius.SessionGUID, documentType).DocumentTypeName : "";
    }
  }

  public class CacheObject<T>
  {
    private DateTime _lastUsed;
    private T _value;

    public DateTime LastUsed
    {
      get => this._lastUsed;
      set => this._lastUsed = value;
    }

    public T Value
    {
      get => this._value;
      set
      {
        this._value = value;
        this._lastUsed = DateTime.Now;
      }
    }

    public bool IsObsolete() => (DateTime.Now - this._lastUsed).Hours > 2;

    public CacheObject(T value)
    {
      this._value = value;
      this._lastUsed = DateTime.Now;
    }
  }

  public struct ExpertFormulaInfo(TempFormula tf, string raGuid, string roGuid)
  {
    public TempFormula tf = tf;
    public string resAttrGuid = raGuid;
    public string resObjTypeGuid = roGuid;
  }

  internal class AttrInfo
  {
    public int attrId;
    public string Name = "";
    public string Alias = "";
    public FieldTypes attrType;
    public Guid guid = Guid.Empty;
    public string guidStr;
    public string attrIdStr;

    public AttrInfo(int aId, string N, string Al, int fType, string g)
    {
      this.attrId = aId;
      this.attrIdStr = Convert.ToString(this.attrId);
      this.Name = N;
      this.Alias = Al;
      this.attrType = (FieldTypes) fType;
      if (!GuidHelper.IsGuid(g))
        return;
      this.guid = new Guid(g);
      this.guidStr = this.guid.ToString();
    }
  }

  internal class Attr4_OTKey
  {
    public int attrType;
    public int objType;

    public Attr4_OTKey(int aType, int oType)
    {
      this.attrType = aType;
      this.objType = oType;
    }

    public override int GetHashCode() => this.attrType ^ this.objType;
  }

  internal class Attr4_OTInfo
  {
    public bool DescriptionEvent;

    public Attr4_OTInfo(bool Descr) => this.DescriptionEvent = Descr;
  }

  [Flags]
  public enum TempAttrStru
  {
    NoTemp = 0,
    TempWithObject = 1,
    TempWithout = 2,
  }

  public class NodeList
  {
    public ScriptTreeNode node;
    public List<int> items;

    public NodeList(ScriptTreeNode node, List<int> items)
    {
      this.node = node;
      this.items = items;
    }

    public NodeList(ScriptTreeNode node)
    {
      this.node = node;
      this.items = new List<int>();
    }
  }

  internal struct GenInfo
  {
    public static ExpertServer.GenInfo Empty;

    public UseZamens AllZamens { get; set; }

    public bool CoWorker { get; set; }

    public bool CheckOut { get; set; }

    public bool Debug { get; set; }

    public string docName { get; set; }

    public GenInfo(ExpertScriptParms esp)
    {
      switch (esp.allZamens)
      {
        case "Y":
          this.AllZamens = UseZamens.MainVariant;
          break;
        case "N":
          this.AllZamens = UseZamens.AllVariants;
          break;
        default:
          this.AllZamens = UseZamens.AsClient;
          break;
      }
      this.CoWorker = esp.coWorker;
      this.CheckOut = esp.checkOut;
      this.Debug = esp.useTraceInfo;
      this.docName = esp.docName;
    }

    internal void LoadXml(XmlNode n)
    {
      foreach (XmlNode childNode in n.ChildNodes)
      {
        if (childNode.NodeType == XmlNodeType.Element)
        {
          if (childNode.Name == "all_zamens")
          {
            switch (childNode.InnerText)
            {
              case "Y":
                this.AllZamens = UseZamens.MainVariant;
                break;
              case "N":
                this.AllZamens = UseZamens.AllVariants;
                break;
              case "C":
                this.AllZamens = UseZamens.AsClient;
                break;
            }
          }
          if (childNode.Name == "coWorker_Template")
            this.CoWorker = childNode.InnerText == "Y";
          if (childNode.Name == "Checkout_Docs")
            this.CheckOut = childNode.InnerText == "Y";
          if (childNode.Name == "show_info")
            this.Debug = childNode.InnerText == "Y";
          if (childNode.Name == "DocName")
            this.docName = childNode.InnerText;
        }
      }
    }
  }

  private class dtStru
  {
    public int dtIndex = -1;
    public long objID = -1;

    public dtStru(int index, long ID)
    {
      this.dtIndex = index;
      this.objID = ID;
    }
  }

  private class IndexPair
  {
    public int LinkIndex { get; set; }

    public int DataIndex { get; set; }

    public IndexPair(int lIndex, int dIndex)
    {
      this.LinkIndex = lIndex;
      this.DataIndex = dIndex;
    }
  }

  internal class PieceData
  {
    public DataTable dt;
    public TempFormula cond;
    public bool searchDown = true;

    internal PieceData(DataTable tbl) => this.dt = tbl;

    internal PieceData(DataTable tbl, TempFormula c)
    {
      this.dt = tbl;
      this.cond = c;
    }
  }

  internal class PortionRowInfo
  {
    public TempFormula cond;
    public DataRow drow;

    public PortionRowInfo(DataRow dr, TempFormula tf)
    {
      this.cond = tf;
      this.drow = dr;
    }

    public PortionRowInfo(DataRow dr)
    {
      this.cond = (TempFormula) null;
      this.drow = dr;
    }
  }

  [Flags]
  internal enum CalcStages
  {
    [CustomDescription("Attribute.Expert.Server_1")] CheckObject = 1,
    [CustomDescription("Attribute.Expert.Server_2")] FindObject = 2,
    [CustomDescription("Attribute.Expert.Server_3")] CalcAttribute = 4,
  }

  internal class CalcStack : List<CalcAttrPair>
  {
    public void Push(long objId, int objTypeId, int attrTypeId)
    {
      this.Add(new CalcAttrPair(objId, objTypeId, attrTypeId));
    }

    public void Pop()
    {
      if (this.Count <= 0)
        return;
      this.RemoveAt(this.Count - 1);
    }

    public bool Contains(long objId, int objTypeId, int attrTypeId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        CalcAttrPair calcAttrPair = this[index];
        if (objId == calcAttrPair.objID && objTypeId == calcAttrPair.objTypeID && attrTypeId == calcAttrPair.attrTypeID)
          return true;
      }
      return false;
    }

    internal CalcAttrPair curCalcItem
    {
      get => this[this.Count - 1];
      set => this[this.Count - 1] = value;
    }
  }

  internal class RuleIdInfo
  {
    public long objRuleId;
    public string ownerId;

    public override bool Equals(object obj)
    {
      return obj is ExpertServer.RuleIdInfo && ((ExpertServer.RuleIdInfo) obj).objRuleId == this.objRuleId && ((ExpertServer.RuleIdInfo) obj).ownerId == this.ownerId;
    }

    public override int GetHashCode() => (int) this.objRuleId;

    public RuleIdInfo(long ruleId, string oId)
    {
      this.objRuleId = ruleId;
      this.ownerId = oId;
    }
  }

  public class ObjAttr : IEquatable<ExpertServer.ObjAttr>
  {
    public long objID;
    public int attrID;

    public ObjAttr(long oID, int aID)
    {
      this.objID = oID;
      this.attrID = aID;
    }

    public override bool Equals(object obj)
    {
      return !(obj is ExpertServer.ObjAttr objAttr) ? base.Equals(obj) : this.Equals(objAttr);
    }

    public override int GetHashCode() => (int) this.objID ^ this.attrID;

    public bool Equals(ExpertServer.ObjAttr obj)
    {
      return obj != null && obj.objID == this.objID && obj.attrID == this.attrID;
    }
  }

  public class PackedValue
  {
    public object Val;
    public ArcMethods am;

    public PackedValue(object Value, ArcMethods arcMeth)
    {
      this.Val = Value;
      this.am = arcMeth;
    }
  }

  internal class IdentPair
  {
    public long objId = -1;
    public long ID = -1;

    public int Index { get; set; } = -1;

    public IdentPair()
    {
    }

    public IdentPair(long oId, long Id)
    {
      this.objId = oId;
      this.ID = Id;
    }

    public IdentPair(long oId, long Id, int index)
    {
      this.objId = oId;
      this.ID = Id;
      this.Index = index;
    }
  }

  internal enum NumberingType
  {
    Number,
    DontNumber,
    DontCount,
  }

  internal class HiddenDocInfo
  {
    public byte[] zippedDoc;
    public byte[] zippedInfo;
    public long checkSum;
    public long prevVerId;
    public ScriptTreeNode genNode;
    public long idbO_ID;
    public string prefix = "";
    public string dopCompTag = "";
    public int docType = -1;
    public int pageCount;
    public int firstListNum;
    public ExpertServer.SetDocumentInfo sDocInfo;
    internal ExpertServer.NumberingType dontNumber;
    public int totalLists;

    public string TemplOperator { get; private set; } = "";

    public long RelationID { get; set; }

    public long SortNumber { get; set; }

    public long ID { get; set; }

    public bool DontNumber => this.dontNumber != 0;

    public void SetDontNumber(bool dontNumber, bool dontCount)
    {
      if (!dontNumber)
        this.dontNumber = ExpertServer.NumberingType.Number;
      else if (dontCount)
        this.dontNumber = ExpertServer.NumberingType.DontCount;
      else
        this.dontNumber = ExpertServer.NumberingType.DontNumber;
    }

    public HiddenDocInfo()
    {
    }

    public HiddenDocInfo(ScriptTreeNode node)
    {
      this.genNode = node;
      this.TemplOperator = node.label;
    }
  }

  internal class OldComplectElem
  {
    public bool complect;
    public long verId = -1;
    public long parentVerId = -1;
    public long ID = -1;
    public string Name = "";
    public long checkSum = -1;
    public long scriptID = -1;
    public bool needDelete = true;
    public bool inOtherComplects;
    public string dopCompTag = "";
    public string operLabel = "";
    public long relationID;

    public long SortOrder { get; set; }

    public ExpertServer.NumberingType numType { get; set; }

    public long RootObjID { get; set; }

    public long RootID { get; set; }

    public int ListCount { get; set; }

    public override int GetHashCode() => (int) this.verId;

    public override bool Equals(object obj)
    {
      return obj is ExpertServer.OldComplectElem oldComplectElem && this.verId == oldComplectElem.verId;
    }
  }

  internal class SortedItem : IComparable, IComparable<ExpertServer.SortedItem>
  {
    public long DocCompId { get; set; }

    public long RelationId { get; set; }

    public long ObjectId { get; set; }

    public long ScriptId { get; set; }

    public long SortOrder { get; set; }

    public int OperId { get; set; } = -1;

    public string Name { get; set; } = "";

    public long ParentObjId { get; set; }

    public int ParentIndex { get; set; } = -1;

    public bool IsComplect { get; set; }

    public int TotalLists { get; set; }

    public int ListsBefore { get; set; }

    public ExpertServer.NumberingType NumType { get; set; }

    public SortedItem(
      long docCompId,
      long relId,
      long objId,
      long scriptId,
      long objectId,
      long sortOrder,
      int operId)
    {
      long num1 = docCompId;
      long num2 = relId;
      long num3 = scriptId;
      long num4 = objectId;
      long num5 = sortOrder;
      int num6 = operId;
      long num7;
      this.DocCompId = num7 = num1;
      this.RelationId = num7 = num2;
      this.ObjectId = num7 = num3;
      this.ScriptId = num7 = num4;
      this.SortOrder = num7 = num5;
      int num8;
      this.OperId = num8 = num6;
    }

    public SortedItem(ExpertServer.OldComplectElem oce, int operId)
    {
      long verId = oce.verId;
      long relationId = oce.relationID;
      long scriptId = oce.scriptID;
      long rootObjId = oce.RootObjID;
      long sortOrder = oce.SortOrder;
      int num1 = operId;
      string name = oce.Name;
      long parentVerId = oce.parentVerId;
      bool complect = oce.complect;
      int listCount = oce.ListCount;
      ExpertServer.NumberingType numType = oce.numType;
      long num2;
      this.DocCompId = num2 = verId;
      this.RelationId = num2 = relationId;
      this.ObjectId = num2 = scriptId;
      this.ScriptId = num2 = rootObjId;
      this.SortOrder = num2 = sortOrder;
      int num3;
      this.OperId = num3 = num1;
      string str;
      this.Name = str = name;
      this.ParentObjId = num2 = parentVerId;
      bool flag;
      this.IsComplect = flag = complect;
      this.TotalLists = num3 = listCount;
      ExpertServer.NumberingType numberingType;
      this.NumType = numberingType = numType;
    }

    public SortedItem(DocRecord dr, ExpertServer.HiddenDocInfo hdi, int operId, long parentObjId)
    {
      int num1 = dr.IsComplect() ? hdi.totalLists : hdi.pageCount;
      long docObjectId = dr.docObjectID;
      long relationId = hdi.RelationID;
      long objId = dr.objID;
      long scriptId = dr.scriptID;
      long sortNumber = hdi.SortNumber;
      int num2 = operId;
      string docName = dr.docName;
      long num3 = parentObjId;
      bool flag1 = dr.IsComplect();
      int num4 = num1;
      ExpertServer.NumberingType dontNumber = hdi.dontNumber;
      long num5;
      this.DocCompId = num5 = docObjectId;
      this.RelationId = num5 = relationId;
      this.ObjectId = num5 = objId;
      this.ScriptId = num5 = scriptId;
      this.SortOrder = num5 = sortNumber;
      int num6;
      this.OperId = num6 = num2;
      string str;
      this.Name = str = docName;
      this.ParentObjId = num5 = num3;
      bool flag2;
      this.IsComplect = flag2 = flag1;
      this.TotalLists = num6 = num4;
      ExpertServer.NumberingType numberingType;
      this.NumType = numberingType = dontNumber;
    }

    public int CompareTo(object obj)
    {
      return obj is ExpertServer.SortedItem other ? ((IComparable<ExpertServer.SortedItem>) this).CompareTo(other) : throw new ArgumentException("Object is not a SortedItem");
    }

    int IComparable<ExpertServer.SortedItem>.CompareTo(ExpertServer.SortedItem other)
    {
      if (this.OperId < other.OperId)
        return -1;
      if (this.OperId > other.OperId)
        return 1;
      if (this.SortOrder < other.SortOrder)
        return -1;
      return this.SortOrder > other.SortOrder ? 1 : 0;
    }
  }

  internal class TemplateIndexer
  {
    protected Dictionary<string, int> identByLabel = new Dictionary<string, int>();
    protected Dictionary<long, int> identByScriptId = new Dictionary<long, int>();

    protected ScriptTreeNode root { get; set; }

    public TemplateIndexer(ScriptTreeNode root, IUserSession ius)
    {
      this.root = root;
      this.CollectLabels(root);
      this.CollectScripts(ius, root);
    }

    protected void CollectLabels(ScriptTreeNode node)
    {
      int id = node.Id;
      if (node.parent != null && node.parent.opTag == ExpertScriptOp.opCreateComplect && !((OpCreateComplect) node.parent.op).needComplect)
        id = node.parent.Id;
      if (!this.identByLabel.ContainsKey(node.label))
        this.identByLabel.Add(node.label, id);
      for (int index = 0; index < node.Items.Count; ++index)
        this.CollectLabels((ScriptTreeNode) node.Items[index]);
    }

    protected void CollectScripts(IUserSession ius, ScriptTreeNode node)
    {
      if (node.label.StartsWith("#"))
        return;
      if (node.opTag == ExpertScriptOp.opCreateDocument)
      {
        OpCreateDoc op = (OpCreateDoc) node.op;
        QuickObjectInfo objectInfo = ius.GetObjectInfo(new Guid(op.scriptGUID));
        if (objectInfo.Empty)
          return;
        if (!this.identByScriptId.ContainsKey(objectInfo.ObjectID))
          this.identByScriptId.Add(objectInfo.ObjectID, node.Id);
      }
      for (int index = 0; index < node.Items.Count; ++index)
        this.CollectScripts(ius, (ScriptTreeNode) node.Items[index]);
    }

    public ExpertServer.SortedItem ProcessOldComplectElem(ExpertServer.OldComplectElem oce)
    {
      if (oce.needDelete)
        return (ExpertServer.SortedItem) null;
      int operId = this[oce.operLabel];
      if (operId == -1)
        operId = this[oce.scriptID];
      return operId == -1 ? (ExpertServer.SortedItem) null : new ExpertServer.SortedItem(oce, operId);
    }

    public ExpertServer.SortedItem ProcessNewElem(
      DocRecord dr,
      ExpertServer.HiddenDocInfo hdi,
      long parentObjId)
    {
      if (dr.docObjectID == 0L)
        return (ExpertServer.SortedItem) null;
      int operId = this[hdi.TemplOperator];
      if (operId == -1)
        operId = this[dr.scriptID];
      return operId == -1 ? (ExpertServer.SortedItem) null : new ExpertServer.SortedItem(dr, hdi, operId, parentObjId);
    }

    public int this[string str] => this.identByLabel.ContainsKey(str) ? this.identByLabel[str] : -1;

    public int this[long scriptId]
    {
      get => this.identByScriptId.ContainsKey(scriptId) ? this.identByScriptId[scriptId] : -1;
    }
  }

  internal class OldKey : IComparable
  {
    public long objectID = -1;
    public long scriptID = -1;

    public OldKey(long oID, long sId)
    {
      this.objectID = oID;
      this.scriptID = sId;
    }

    public int CompareTo(object obj)
    {
      if (!(obj is ExpertServer.OldKey oldKey) || this.objectID < oldKey.objectID)
        return -1;
      if (this.objectID > oldKey.objectID)
        return 1;
      if (this.scriptID < oldKey.scriptID)
        return -1;
      return this.scriptID > oldKey.scriptID ? 1 : 0;
    }

    public override bool Equals(object obj)
    {
      return obj is ExpertServer.OldKey oldKey && this.objectID == oldKey.objectID && this.scriptID == oldKey.scriptID;
    }

    public override int GetHashCode() => (int) this.objectID ^ (int) this.scriptID;
  }

  internal struct SetDocumentInfo
  {
    internal GenMode _compGenMode;
    internal bool _makeLog;
    internal bool _coWorkerDocs;
    internal ConcurrentDictionary<long, int> _docListIndex;
    internal ConcurrentDictionary<ExpertServer.OldKey, ExpertServer.IdentPair> _oldIdents;
    private List<ChangeInfo> _changed;
    internal Dictionary<long, ExpertServer.OldComplectElem> _oldComplect;
    internal Dictionary<string, object> _namedParms;
    internal Dictionary<int, object> _docAttrs;

    internal GenMode CompGenMode
    {
      get => this._compGenMode;
      set => this._compGenMode = value;
    }

    internal bool MakeLog
    {
      get => this._makeLog;
      set => this._makeLog = value;
    }

    internal bool CoWorkerDocs
    {
      get => this._coWorkerDocs;
      set => this._coWorkerDocs = value;
    }

    internal ConcurrentDictionary<long, int> DocListIndex
    {
      get => this._docListIndex;
      set => this._docListIndex = value;
    }

    internal ConcurrentDictionary<ExpertServer.OldKey, ExpertServer.IdentPair> OldIdents
    {
      get => this._oldIdents;
      set => this._oldIdents = value;
    }

    internal List<ChangeInfo> Changed
    {
      get => this._changed;
      set => this._changed = value;
    }

    internal Dictionary<long, ExpertServer.OldComplectElem> OldComplect
    {
      get => this._oldComplect;
      set => this._oldComplect = value;
    }

    internal Dictionary<string, object> NamedParms
    {
      get => this._namedParms;
      set => this._namedParms = value;
    }

    internal Dictionary<int, object> DocAttrs
    {
      get => this._docAttrs;
      set => this._docAttrs = value;
    }

    internal long RevisionId { get; set; }

    internal int AttrGroupChangeNum { get; set; }

    internal SetDocumentInfo(ExpertServer.ExpServTask ti, bool isComplect = false)
    {
      this._compGenMode = ti.CompGenMode;
      this._makeLog = ti.makeLog;
      this._coWorkerDocs = ti.coWorkerDocs;
      this._docListIndex = ti.docListIndex;
      this._oldIdents = ti.oldIdents;
      this._changed = ti.changed;
      this._oldComplect = ti.oldComplect;
      this._namedParms = ti.namedParms;
      this.RevisionId = ti.ChangeGroupId;
      this.AttrGroupChangeNum = ti.attrChangeGroupId;
      this._docAttrs = new Dictionary<int, object>();
      List<int> intList = isComplect ? ti.compAttrs : ti.docAttrs;
      if (intList == null || intList.Count <= 0)
        return;
      foreach (int num in intList)
      {
        object parm = ExpertServer.es.InnerGetParm(ti, num);
        if (parm != null)
          this.DocAttrs.Add(num, parm);
      }
    }

    internal bool IsInChanged(long Id)
    {
      lock (this._changed)
      {
        foreach (ChangeInfo changeInfo in this.Changed)
        {
          if (changeInfo.ID == Id || changeInfo.ID == -Id)
            return true;
        }
      }
      return false;
    }

    internal void AddChangedDoc(long docObjId, int ObjType, DocOperType dot)
    {
      if (this.IsInChanged(docObjId))
        return;
      ChangeObjInfo changeObjInfo = new ChangeObjInfo(docObjId, ObjType, dot);
      lock (this._changed)
        this.Changed.Add((ChangeInfo) changeObjInfo);
    }
  }

  public class ExpServTask : IExpertServerTask, IExpertGlobalTable
  {
    protected TaskDataCache _dataCache;
    protected CalcAttrCacheBase<bool> _neededAttrs;
    protected CalcAttrCacheBase<CalculatedAttr> _calcAttrs;
    protected ConcurrentDictionary<long, ImDocumentData> _cacheTemplates;
    internal ConcurrentDictionary<long, Tuple<ExpertServer.GenInfo, ScriptTreeNode>> _cacheScripts;
    internal List<long> _addObjs;
    public int taskId = -1;
    public Thread thread;
    public Guid sessionGUID;
    public Guid clonedSessionGUID = Guid.Empty;
    public ExpertTraceFlags traceFlags;
    public XmlDocument traceInfo;
    private XmlNode _curNode;
    public string lastInfoStr = "";
    public ImDocumentData template;
    public ImDocumentData docData;
    public DocumentTreeNode curDocNode;
    public DocumentTreeNode lockCurNode;
    public DocumentTreeNode defRootNode;
    public ExpertScriptType curScrType = ExpertScriptType.CommonCalc;
    internal ExpertServer.CalcStack calcStack;
    public int blockTrace;
    internal bool makeTrace;
    internal bool makeLog;
    public HybridTableExp savedData;
    public HybridTableExp savedLinks;
    public Dictionary<int, int> objTypesToNodes;
    public Dictionary<int, List<ColumnDescriptor>> objAttrs4ObjTypes;
    public Dictionary<int, List<ColumnDescriptor>> relAttrs4ObjTypes;
    public Dictionary<long, int> dataObjIndex;
    public Dictionary<long, int> dataPartIndex;
    public Dictionary<long, List<int>> linksProjIndex;
    public Dictionary<long, List<int>> linksPartIndex;
    public Dictionary<long, int> linksIdIndex;
    public HashSet<object> multiAttrs;
    public List<string> fileAttrs;
    public long fakeObjIdCounter = 504403158265495552 /*0x0700000000000000*/;
    public SortedList<int, ExpertServer.ExpServTask.ObjTypeAttrs> attrTypeStorage;
    public SortedList<int, SortedList<long, object[]>> objAttrsStorage;
    public SortedDictionary<long, int> objTypeIndex;
    internal ColumnDescriptor[] RelCondDescs;
    internal HybridDictionary window_filtr;
    internal HybridDictionary cur_filtr;
    internal ScriptTreeNode scriptRoot;
    internal Dictionary<ExpertServer.ObjAttr, object> attrCache;
    public bool testMode;
    public bool aborting;
    public List<string> userReport;
    public Hashtable foundObjects;
    public List<long> context = new List<long>();
    public UseZamens useAllZamens;
    public bool? clientAllZamens;
    public bool coWorkerDocs;
    public bool checkOutDocs;
    public DateTime abortThreshold = DateTime.MinValue;
    public TimeSpan interval = TimeSpan.MaxValue;
    public DateTimeFormatInfo dfi = CultureInfo.CurrentCulture.DateTimeFormat;
    public NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
    public long curRelationId;
    public List<int> docTypes;
    public ExpertTraceNode rootTraceNode;
    public ExpertTraceNode curTraceNode;
    internal bool SimpleCalcMode;
    public List<long> _notExpandedObjIds;
    public Dictionary<string, object> namedParms;
    public long RootObjID;
    public HashSet<long> objectIDs;
    public HashSet<long> linkIDs;
    public bool BreakFlag;
    public bool objectFound;
    public bool forceSearchByGlobal;
    public bool rootExclaimed;
    public string verRuleOwnerId = "";
    internal Dictionary<long, ExpertServer.RuleIdInfo> RulesList;
    public VersionsRule curFiltrationRule;
    public long editingContextID;
    internal HashSet<int> tempAttrsWithoutObject;
    internal HashSet<int> tempAttrsWithObject;
    internal List<int> docAttrs;
    internal List<int> compAttrs;
    internal GenMode CompGenMode = GenMode.genModeNone;
    internal List<DocRecord> docList;
    internal List<ExpertServer.HiddenDocInfo> hiddenList;
    internal Dictionary<long, ExpertServer.OldComplectElem> oldComplect;
    internal long OldComplectId = -1;
    internal long compScriptId = -1;
    internal List<ExpertServer.OldKey> replacements = new List<ExpertServer.OldKey>();
    internal long contextID = -1;
    internal long ChangeGroupId;
    internal int attrChangeGroupId;
    internal List<ExpertServer.NodeList> nodeItems = new List<ExpertServer.NodeList>();
    internal ConcurrentDictionary<ExpertServer.OldKey, ExpertServer.IdentPair> oldIdents = new ConcurrentDictionary<ExpertServer.OldKey, ExpertServer.IdentPair>();
    internal ConcurrentDictionary<long, int> docListIndex = new ConcurrentDictionary<long, int>();
    internal bool needSecondPass;
    internal int[] idComplects;
    internal int[] idDocs;
    internal List<ChangeInfo> changed;
    internal bool allowConcretization = true;
    internal List<string> dopCompTags;
    private List<long> _ispList;
    public List<string> ispNameList;
    public int currentIsp = -1;
    public ArticlesPartsPackage app;
    public bool HasVariableParts;
    private List<ExpertServer.ExpServTask.CallStackItem> callStack = new List<ExpertServer.ExpServTask.CallStackItem>();
    private Dictionary<ScriptTreeNode, long> callCounter = new Dictionary<ScriptTreeNode, long>();
    public HashSet<long> UsedObjects;
    public static readonly string objTypeDocumentation = "cad00070-306c-11d8-b4e9-00304f19f545";
    private VedomostiSortingCache _vedomostiSortingCache;
    internal bool Anton_Init;
    internal long docScriptId;

    public List<long> AddAdditionalObjs(IEnumerable<long> addObjs)
    {
      if (this._addObjs == null)
        this._addObjs = new List<long>();
      List<long> longList = new List<long>();
      if (addObjs != null)
      {
        foreach (long addObj in addObjs)
        {
          if (!this._addObjs.Contains(addObj))
          {
            longList.Add(addObj);
            this._addObjs.Add(addObj);
          }
        }
      }
      return longList;
    }

    public void RemoveAdditionalObjs(List<long> addedObjs)
    {
      foreach (long addedObj in addedObjs)
        this._addObjs.Remove(addedObj);
    }

    public XmlNode curNode
    {
      get => this._curNode;
      set => this._curNode = value;
    }

    public HybridRowExp savedDataByObjId(long objId)
    {
      if (this.dataObjIndex == null)
        return (HybridRowExp) null;
      int index = -1;
      return this.dataObjIndex.TryGetValue(objId, out index) && index < this.savedData.RowsCount ? this.savedData[index] : (HybridRowExp) null;
    }

    public int savedDataByObjIdIndex(long objId)
    {
      if (this.dataObjIndex == null)
        return -1;
      int num = -1;
      return this.dataObjIndex.TryGetValue(objId, out num) && num < this.savedData.RowsCount ? num : -1;
    }

    public HybridRowExp savedDataByPartId(long partId)
    {
      if (this.dataPartIndex == null)
        return (HybridRowExp) null;
      int index = -1;
      return this.dataPartIndex.TryGetValue(partId, out index) && index < this.savedData.RowsCount ? this.savedData[index] : (HybridRowExp) null;
    }

    public int savedDataByPartIdIndex(long partId)
    {
      if (this.dataPartIndex == null)
        return -1;
      int num = -1;
      return this.dataPartIndex.TryGetValue(partId, out num) && num < this.savedData.RowsCount ? num : -1;
    }

    public HybridRowExp savedLinksByIdIndex(long linkId)
    {
      if (this.linksIdIndex == null)
        return (HybridRowExp) null;
      int index = -1;
      return this.linksIdIndex.TryGetValue(linkId, out index) && index < this.savedLinks.RowsCount ? this.savedLinks[index] : (HybridRowExp) null;
    }

    public HybridRowExp[] savedLinksByProjId(long projId)
    {
      if (this.linksProjIndex == null)
        return (HybridRowExp[]) null;
      List<int> intList = (List<int>) null;
      if (!this.linksProjIndex.TryGetValue(projId, out intList))
        return (HybridRowExp[]) null;
      HybridRowExp[] instance = (HybridRowExp[]) Array.CreateInstance(typeof (HybridRowExp), intList.Count);
      for (int index1 = 0; index1 < intList.Count; ++index1)
      {
        int index2 = intList[index1];
        instance[index1] = index2 < 0 || index2 >= this.savedLinks.RowsCount ? (HybridRowExp) null : this.savedLinks[index2];
      }
      return instance;
    }

    public List<HybridRowExp> savedLinksByProjIndex2(long projId)
    {
      if (this.linksProjIndex == null)
        return (List<HybridRowExp>) null;
      List<int> intList = (List<int>) null;
      if (!this.linksProjIndex.TryGetValue(projId, out intList))
        return (List<HybridRowExp>) null;
      List<HybridRowExp> hybridRowExpList = new List<HybridRowExp>();
      for (int index1 = 0; index1 < intList.Count; ++index1)
      {
        int index2 = intList[index1];
        if (index2 >= 0 && index2 < this.savedLinks.RowsCount)
          hybridRowExpList.Add(this.savedLinks[index2]);
      }
      return hybridRowExpList;
    }

    public void AddProjIndex(long projIndex, int index)
    {
      List<int> intList = (List<int>) null;
      if (this.linksProjIndex.TryGetValue(projIndex, out intList))
      {
        if (intList.Contains(index))
          return;
        intList.Add(index);
      }
      else
        this.linksProjIndex.Add(projIndex, new List<int>()
        {
          index
        });
    }

    public HybridRowExp[] savedLinksByPartId(long partId)
    {
      if (this.linksPartIndex == null)
        return (HybridRowExp[]) null;
      List<int> intList = (List<int>) null;
      if (!this.linksPartIndex.TryGetValue(partId, out intList) || intList == null)
        return (HybridRowExp[]) null;
      HybridRowExp[] instance = (HybridRowExp[]) Array.CreateInstance(typeof (HybridRowExp), intList.Count);
      for (int index1 = 0; index1 < intList.Count; ++index1)
      {
        int index2 = intList[index1];
        instance[index1] = index2 < 0 || index2 >= this.savedLinks.RowsCount ? (HybridRowExp) null : this.savedLinks[index2];
      }
      return instance;
    }

    public List<int> savedLinksByPartIndex(long partId)
    {
      if (this.linksPartIndex == null)
        return (List<int>) null;
      List<int> intList = (List<int>) null;
      return this.linksPartIndex.TryGetValue(partId, out intList) && intList != null ? intList : (List<int>) null;
    }

    public void AddPartIndex(long partIndex, int index)
    {
      List<int> intList = (List<int>) null;
      if (this.linksPartIndex.TryGetValue(partIndex, out intList))
      {
        if (intList.Contains(index))
          return;
        intList.Add(index);
      }
      else
        this.linksPartIndex.Add(partIndex, new List<int>()
        {
          index
        });
    }

    public void RemoveProjLink(long projId, int index)
    {
      List<int> intList = (List<int>) null;
      if (!this.linksProjIndex.TryGetValue(projId, out intList))
        return;
      intList.Remove(index);
      if (intList.Count != 0)
        return;
      this.linksProjIndex.Remove(projId);
    }

    public void RemovePartLink(long partId, int index)
    {
      List<int> intList = (List<int>) null;
      if (!this.linksPartIndex.TryGetValue(partId, out intList))
        return;
      intList.Remove(index);
      if (intList.Count != 0)
        return;
      this.linksPartIndex.Remove(partId);
    }

    public void OptInitCollectObjectData()
    {
      if (this.attrTypeStorage == null)
        this.attrTypeStorage = new SortedList<int, ExpertServer.ExpServTask.ObjTypeAttrs>();
      if (this.objAttrsStorage == null)
        this.objAttrsStorage = new SortedList<int, SortedList<long, object[]>>();
      if (this.objTypeIndex != null)
        return;
      this.objTypeIndex = new SortedDictionary<long, int>();
    }

    public void OptCollectScriptAttrTypes()
    {
      this._DoCollectAttrTypes(this.scriptRoot);
      this._DoAddParentTypesAttrs();
    }

    private void _CheckAddAttr(string attrGuid, string objTypeGuid, List<int> objTypes)
    {
      if (attrGuid == "")
        return;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(attrGuid));
      if (attributeType == null || attributeType.FieldType == FieldTypes.ftMemo)
        return;
      int childTypeID = -1;
      if (GuidHelper.IsGuid(objTypeGuid) && new Guid(objTypeGuid) != Guid.Empty)
        childTypeID = MetaDataHelper.GetObjectTypeID(objTypeGuid);
      if (childTypeID != -1 && !objTypes.Contains(childTypeID))
      {
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(childTypeID);
        bool flag = false;
        foreach (int num in objectTypeParentsId)
        {
          if (objTypes.Contains(num))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return;
      }
      for (int index = 0; index < objTypes.Count; ++index)
      {
        int objType = objTypes[index];
        ExpertServer.ExpServTask.ObjTypeAttrs objTypeAttrs = (ExpertServer.ExpServTask.ObjTypeAttrs) null;
        this.attrTypeStorage.TryGetValue(objType, out objTypeAttrs);
        if (objTypeAttrs == null)
        {
          objTypeAttrs = new ExpertServer.ExpServTask.ObjTypeAttrs(objType);
          this.attrTypeStorage.Add(objType, objTypeAttrs);
        }
        else if (objTypeAttrs.Contains(attributeType.AttributeID))
          continue;
        ExpertServer.ExpServTask.AttrTypeInfo ati = new ExpertServer.ExpServTask.AttrTypeInfo(attributeType.AttributeID, attributeType.AttributeGuid.ToString());
        objTypeAttrs.AddAttr(ati);
      }
    }

    private void _DoCollectFillFldTypes(ScriptTreeNode node, List<int> objTypes)
    {
      if (node.opTag == ExpertScriptOp.opDocFillText)
      {
        OpParmFillFld op = (OpParmFillFld) node.op;
        if (op.tf != null)
        {
          for (int index = 0; index < op.tf.usedAttrs.Count; ++index)
            this._CheckAddAttr(op.tf.attrGUIDs[index], op.tf.objTypeGUIDs[index], objTypes);
        }
        else
          this._CheckAddAttr(op.attrGUID, op.objTypeGUID, objTypes);
      }
      foreach (ScriptTreeNode node1 in node.Items)
      {
        if (!node1.label.StartsWith("#") && !this.IsObjectNode(node1))
          this._DoCollectFillFldTypes(node1, objTypes);
      }
    }

    private void _DoCollectAttrTypes(ScriptTreeNode node)
    {
      if (this.IsObjectNode(node) && node.op is OpParmObject)
      {
        OpParmObject op = (OpParmObject) node.op;
        if (op.saveGlobal != GlobalSave.saveAdd && op.saveGlobal != GlobalSave.saveSet)
        {
          List<int> objTypes = new List<int>();
          if (op.objTypeIDs == null)
          {
            objTypes.Add(-1);
          }
          else
          {
            for (int index = 0; index < op.objTypeIDs.Count; ++index)
              objTypes.Add(Convert.ToInt32(op.objTypeIDs[index]));
          }
          for (int index1 = 0; index1 < objTypes.Count; ++index1)
          {
            int num = objTypes[index1];
            ExpertServer.ExpServTask.ObjTypeAttrs objTypeAttrs = this.attrTypeStorage.ContainsKey(num) ? this.attrTypeStorage[num] : (ExpertServer.ExpServTask.ObjTypeAttrs) null;
            if (op.dataAttrGUIDs != null)
            {
              for (int index2 = 0; index2 < op.dataAttrGUIDs.Count; ++index2)
              {
                string dataAttrGuiD = op.dataAttrGUIDs[index2];
                int attributeTypeId = MetaDataHelper.GetAttributeTypeID(dataAttrGuiD);
                if (objTypeAttrs == null)
                {
                  objTypeAttrs = new ExpertServer.ExpServTask.ObjTypeAttrs(num);
                  this.attrTypeStorage.Add(num, objTypeAttrs);
                }
                else if (objTypeAttrs.Contains(attributeTypeId))
                  continue;
                ExpertServer.ExpServTask.AttrTypeInfo ati = new ExpertServer.ExpServTask.AttrTypeInfo(attributeTypeId, dataAttrGuiD);
                objTypeAttrs.AddAttr(ati);
              }
            }
            if (node.mod != null && node.mod is ModParmSort)
            {
              ModParmSort mod = node.mod as ModParmSort;
              if (mod.sortAttrTexts != null)
              {
                for (int index3 = 0; index3 < mod.sortAttrTexts.Count; ++index3)
                {
                  string sortAttr = mod.sortAttrs[index3];
                  int attributeTypeId = MetaDataHelper.GetAttributeTypeID(sortAttr);
                  if (objTypeAttrs == null)
                  {
                    objTypeAttrs = new ExpertServer.ExpServTask.ObjTypeAttrs(num);
                    this.attrTypeStorage.Add(num, objTypeAttrs);
                  }
                  else if (objTypeAttrs.Contains(attributeTypeId))
                    continue;
                  ExpertServer.ExpServTask.AttrTypeInfo ati = new ExpertServer.ExpServTask.AttrTypeInfo(attributeTypeId, sortAttr);
                  objTypeAttrs.AddAttr(ati);
                }
              }
              if (mod.groupAttrTexts != null)
              {
                for (int index4 = 0; index4 < mod.groupAttrTexts.Count; ++index4)
                {
                  string groupAttr = mod.groupAttrs[index4];
                  int attributeTypeId = MetaDataHelper.GetAttributeTypeID(groupAttr);
                  if (objTypeAttrs == null)
                  {
                    objTypeAttrs = new ExpertServer.ExpServTask.ObjTypeAttrs(num);
                    this.attrTypeStorage.Add(num, objTypeAttrs);
                  }
                  else if (objTypeAttrs.Contains(attributeTypeId))
                    continue;
                  ExpertServer.ExpServTask.AttrTypeInfo ati = new ExpertServer.ExpServTask.AttrTypeInfo(attributeTypeId, groupAttr);
                  objTypeAttrs.AddAttr(ati);
                }
              }
            }
          }
          foreach (ScriptTreeNode node1 in node.Items)
          {
            if (!node1.label.StartsWith("#"))
              this._DoCollectFillFldTypes(node1, objTypes);
          }
        }
      }
      foreach (ScriptTreeNode node2 in node.Items)
      {
        if (!node2.label.StartsWith("#"))
          this._DoCollectAttrTypes(node2);
      }
    }

    public void DoCollectComplectAttrTypes(ScriptTreeNode compRoot, IUserSession ius)
    {
      if (compRoot.op is OpCreateDoc)
      {
        OpCreateDoc op = (OpCreateDoc) compRoot.op;
        if (op.docType == "N")
        {
          DocScript docScript = (DocScript) ius.GetObject(new Guid(op.scriptGUID));
          docScript.Load();
          try
          {
            docScript.UnpackXML();
          }
          catch (Exception ex)
          {
            throw new ExpertServerException($"\"{ex.Message}{LocalizationHolder.rm.GetString("Expert.Server_23")}", ex);
          }
          this._DoCollectAttrTypes(ExpertServer.LoadScriptTree(docScript.xDoc));
        }
      }
      foreach (ScriptTreeNode compRoot1 in compRoot.Items)
      {
        if (!compRoot1.label.StartsWith("#"))
          this.DoCollectComplectAttrTypes(compRoot1, ius);
      }
    }

    private void _DoAddParentTypesAttrs()
    {
      foreach (int key1 in (IEnumerable<int>) this.attrTypeStorage.Keys)
      {
        ExpertServer.ExpServTask.ObjTypeAttrs objTypeAttrs = this.attrTypeStorage[key1];
        List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(key1);
        parentsIdReverse.Add(-1);
        for (int index = 0; index < parentsIdReverse.Count; ++index)
        {
          int key2 = parentsIdReverse[index];
          if (this.attrTypeStorage.ContainsKey(key2))
          {
            foreach (ExpertServer.ExpServTask.AttrTypeInfo attr in this.attrTypeStorage[key2].attrs)
            {
              if (!objTypeAttrs.Contains(attr.Id))
                objTypeAttrs.AddAttr(attr);
            }
          }
        }
      }
    }

    private void _AddObjectType(int objType)
    {
      List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(objType);
      parentsIdReverse.Add(-1);
      ExpertServer.ExpServTask.ObjTypeAttrs objTypeAttrs = new ExpertServer.ExpServTask.ObjTypeAttrs(objType);
      foreach (int key in parentsIdReverse)
      {
        if (this.attrTypeStorage.ContainsKey(key))
        {
          foreach (ExpertServer.ExpServTask.AttrTypeInfo attr in this.attrTypeStorage[key].attrs)
          {
            if (!objTypeAttrs.Contains(attr.Id))
              objTypeAttrs.AddAttr(attr);
          }
        }
      }
      this.attrTypeStorage.Add(objType, objTypeAttrs);
      if (this.objAttrsStorage.ContainsKey(objType))
        return;
      this.objAttrsStorage.Add(objType, new SortedList<long, object[]>());
    }

    public void OptFillSavedDataRow(IUserSession ius, HybridRowExp row)
    {
      long int64 = Convert.ToInt64(row[0]);
      IDBObject dbObject = (IDBObject) null;
      for (int index = 1; index < this.savedData.Columns.Count; ++index)
      {
        if (row[index].IsDBNull())
        {
          string columnName = this.savedData.Columns[index].ColumnName;
          Guid empty = Guid.Empty;
          ref Guid local = ref empty;
          if (Guid.TryParse(columnName, out local))
          {
            dbObject = dbObject ?? ius.GetObject(int64);
            if (dbObject == null)
              break;
            if (MetaDataHelper.GetAttributeTypeID(empty) < 0)
            {
              object[] valuesByGuid = dbObject.GetValuesByGuid(empty, false);
              if (valuesByGuid.Length != 0)
                row[index] = valuesByGuid[0];
            }
            else
            {
              IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(empty);
              if (attributeByGuid != null)
                row[index] = attributeByGuid.Value;
            }
          }
          else
            row[index] = (object) DBNull.Value;
        }
      }
    }

    public void OptCollectSavedDataObjects(IUserSession ius)
    {
      if (this.savedData == null)
        return;
      if (this.objAttrsStorage == null)
        this.objAttrsStorage = new SortedList<int, SortedList<long, object[]>>();
      for (int index = 0; index < this.savedData.RowsCount; ++index)
      {
        HybridRowExp row = this.savedData[index];
        if (row["cad0002e-306c-11d8-b4e9-00304f19f545"].IsNullOrDBNull())
          this.OptFillSavedDataRow(ius, row);
        int int32 = Convert.ToInt32(row["cad0002e-306c-11d8-b4e9-00304f19f545"]);
        long int64 = Convert.ToInt64(row["cad00029-306c-11d8-b4e9-00304f19f545"]);
        if (!this.attrTypeStorage.ContainsKey(int32))
          this._AddObjectType(int32);
        if (!this.objAttrsStorage.ContainsKey(int32))
        {
          this.objAttrsStorage.Add(int32, new SortedList<long, object[]>()
          {
            {
              int64,
              (object[]) null
            }
          });
        }
        else
        {
          SortedList<long, object[]> sortedList = this.objAttrsStorage[int32];
          if (!sortedList.ContainsKey(int64))
            sortedList.Add(int64, (object[]) null);
        }
      }
    }

    public void OptCollectObjectAttrs(IUserSession ius)
    {
      this.objTypeIndex.Clear();
      foreach (int key in (IEnumerable<int>) this.objAttrsStorage.Keys)
      {
        SortedList<long, object[]> sortedList = this.objAttrsStorage[key];
        ExpertServer.ExpServTask.ObjTypeAttrs objTypeAttrs = this.attrTypeStorage[key];
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
        columnDescriptorList.Add(new ColumnDescriptor((object) new Guid("cad00029-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.Guid, SortOrders.NONE, 1));
        int index1 = 0;
        while (index1 < objTypeAttrs.attrs.Count)
        {
          ExpertServer.ExpServTask.AttrTypeInfo attr = objTypeAttrs.attrs[index1];
          if (attr.aGuid == "cad00029-306c-11d8-b4e9-00304f19f545")
          {
            objTypeAttrs.attrs.RemoveAt(index1);
          }
          else
          {
            bool measured = false;
            try
            {
              ColumnContents columnContents = DbHelper.GetColumnContents(attr.aGuid, out measured);
              ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) new Guid(attr.aGuid), AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : columnDescriptorList.Count + 1);
              columnDescriptorList.Add(columnDescriptor);
              ++index1;
            }
            catch
            {
              objTypeAttrs.attrs.RemoveAt(index1);
            }
          }
        }
        List<long> objIdList = new List<long>((IEnumerable<long>) sortedList.Keys);
        DataTable objectData = DataHelper.GetObjectData(key, ius, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<long>) objIdList);
        if (objectData != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            object[] instance = (object[]) Array.CreateInstance(typeof (object), objTypeAttrs.attrs.Count);
            for (int index2 = 0; index2 < objTypeAttrs.attrs.Count; ++index2)
              instance[index2] = !row[index2 + 1].IsDBNull() ? row[index2 + 1] : (object) null;
            sortedList[int64] = instance;
            this.objTypeIndex.Add(int64, key);
          }
        }
      }
    }

    public void OptAddNewObjects(IUserSession ius, HashSet<long> objIDs)
    {
      this.OptInitCollectObjectData();
      SortedList<int, List<long>> sortedList1 = new SortedList<int, List<long>>();
      foreach (long objId in objIDs)
      {
        if (!this.objTypeIndex.ContainsKey(objId))
        {
          TaskDataCache.ObjDataItem objData = this.DataCache.GetObjData(objId, ius);
          int num = (TypedInfoItem) objData != (TypedInfoItem) null ? objData.ObjTypeID : -1;
          if (sortedList1.ContainsKey(num))
            sortedList1[num].Add(objId);
          else
            sortedList1.Add(num, new List<long>() { objId });
          if (!this.attrTypeStorage.ContainsKey(num))
            this._AddObjectType(num);
          if (!this.objAttrsStorage.ContainsKey(num))
          {
            SortedList<long, object[]> sortedList2 = new SortedList<long, object[]>();
            this.objAttrsStorage.Add(num, sortedList2);
          }
        }
      }
      foreach (int key in (IEnumerable<int>) sortedList1.Keys)
      {
        List<long> objIdList = sortedList1[key];
        ExpertServer.ExpServTask.ObjTypeAttrs objTypeAttrs = this.attrTypeStorage[key];
        SortedList<long, object[]> sortedList3 = this.objAttrsStorage[key];
        List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
        columnDescriptorList.Add(new ColumnDescriptor((object) new Guid("cad00029-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.Guid, SortOrders.NONE, 1));
        int index1 = 0;
        while (index1 < objTypeAttrs.attrs.Count)
        {
          ExpertServer.ExpServTask.AttrTypeInfo attr = objTypeAttrs.attrs[index1];
          if (attr.aGuid.ToString() == "cad00029-306c-11d8-b4e9-00304f19f545" || attr.Id == -10000 || attr.Id == 0)
          {
            ++index1;
          }
          else
          {
            bool measured = false;
            try
            {
              ColumnContents columnContents = DbHelper.GetColumnContents(attr.aGuid, out measured);
              ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) new Guid(attr.aGuid), AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : columnDescriptorList.Count + 1);
              columnDescriptorList.Add(columnDescriptor);
              ++index1;
            }
            catch
            {
              objTypeAttrs.attrs.RemoveAt(index1);
            }
          }
        }
        DataTable objectData = DataHelper.GetObjectData(key, ius, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) columnDescriptorList.ToArray(), (IEnumerable<long>) objIdList);
        if (objectData != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            object[] instance = (object[]) Array.CreateInstance(typeof (object), objTypeAttrs.attrs.Count);
            for (int index2 = 0; index2 < objTypeAttrs.attrs.Count; ++index2)
              instance[index2] = index2 + 1 >= objectData.Columns.Count || row[index2 + 1].IsDBNull() ? (object) null : row[index2 + 1];
            sortedList3[int64] = instance;
            this.objTypeIndex.Add(int64, key);
          }
        }
      }
    }

    public bool OptGetObjectAttr(long objId, int attrType, out object Value)
    {
      Value = (object) null;
      if (this.objTypeIndex != null && this.objTypeIndex.ContainsKey(objId))
      {
        int key = this.objTypeIndex[objId];
        if (this.objAttrsStorage.ContainsKey(key))
        {
          SortedList<long, object[]> sortedList = this.objAttrsStorage[key];
          if (sortedList.ContainsKey(objId) && this.attrTypeStorage.ContainsKey(key))
          {
            int attrIndex = this.attrTypeStorage[key].GetAttrIndex(attrType);
            if (attrIndex != -1)
            {
              Value = sortedList[objId][attrIndex];
              return true;
            }
          }
        }
      }
      return false;
    }

    public bool IsObjectNode(ScriptTreeNode node)
    {
      return node.opTag == ExpertScriptOp.opObjAncestors || node.opTag == ExpertScriptOp.opObjChildren || node.opTag == ExpertScriptOp.opObjDescendants || node.opTag == ExpertScriptOp.opObjLinked || node.opTag == ExpertScriptOp.opObjParents || node.opTag == ExpertScriptOp.opObjSiblings;
    }

    internal ConcurrentDictionary<long, ImDocumentData> cacheTemplates
    {
      get => this._cacheTemplates;
      set => this._cacheTemplates = value;
    }

    internal ConcurrentDictionary<long, Tuple<ExpertServer.GenInfo, ScriptTreeNode>> cacheScripts
    {
      get => this._cacheScripts;
      set => this._cacheScripts = value;
    }

    public bool IsTempAttrWithObject(int attrId)
    {
      return this.tempAttrsWithObject != null && this.tempAttrsWithObject.Contains(attrId);
    }

    public bool IsTempAttrWithoutObject(int attrId)
    {
      return this.tempAttrsWithoutObject != null && this.tempAttrsWithoutObject.Contains(attrId);
    }

    public bool IsTempAttribute(int attrId, out bool tempWith, out bool tempWithout)
    {
      tempWith = this.tempAttrsWithObject != null && this.tempAttrsWithObject.Contains(attrId);
      tempWithout = this.tempAttrsWithObject != null && this.tempAttrsWithoutObject.Contains(attrId);
      return tempWith | tempWithout;
    }

    public ExpertServer.TempAttrStru GetTempAttrStru(Guid attrGuid)
    {
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGuid);
      bool tempWith = false;
      bool tempWithout = false;
      this.IsTempAttribute(attributeTypeId, out tempWith, out tempWithout);
      return this.CreateTAS(tempWith, tempWithout);
    }

    public ExpertServer.TempAttrStru CreateTAS(bool tempWith, bool tempWithout)
    {
      return tempWith ? (tempWithout ? ExpertServer.TempAttrStru.NoTemp : ExpertServer.TempAttrStru.TempWithObject) : (tempWithout ? ExpertServer.TempAttrStru.TempWithout : ExpertServer.TempAttrStru.NoTemp);
    }

    public ExpServTask(int taskId, Guid sessionGUID, ExpertTraceFlags traceFlags)
    {
      this.taskId = taskId;
      this.thread = (Thread) null;
      this.traceFlags = traceFlags;
      this.sessionGUID = sessionGUID;
      this.InitTraceInfo();
      this._neededAttrs = new CalcAttrCacheBase<bool>(100);
      this._calcAttrs = new CalcAttrCacheBase<CalculatedAttr>(100);
      this._cacheScripts = new ConcurrentDictionary<long, Tuple<ExpertServer.GenInfo, ScriptTreeNode>>();
      this.calcStack = new ExpertServer.CalcStack();
      this.RulesList = new Dictionary<long, ExpertServer.RuleIdInfo>();
      this.attrCache = new Dictionary<ExpertServer.ObjAttr, object>();
      this.oldComplect = new Dictionary<long, ExpertServer.OldComplectElem>();
      this.userReport = new List<string>();
      this.foundObjects = new Hashtable();
      this._dataCache = new TaskDataCache();
      if (ExpertConsts.Consts != null)
      {
        ExpertServer.es.InnerSetParm(this, ExpertConsts.Consts.attrServerName, (object) EnvironmentConsts.MachineName);
        IUserSession session = ExpertServer._CheckGetSession(sessionGUID);
        ExpertServer.es.InnerSetParm(this, ExpertConsts.Consts.attrUserName, (object) session.UserName);
        ExpertServer.es.InnerSetParm(this, ExpertConsts.Consts.attrUserId, (object) session.UserID);
        ExpertServer.es.InnerSetParm(this, ExpertConsts.Consts.attrUserLink, (object) session.UserID);
        ExpertServer.es.InnerSetParm(this, ExpertConsts.Consts.attrUserRoleLink, (object) session.RoleID);
        List<int> attributesInGroup1 = MetaDataHelper.GetAttributesInGroup(ExpertConsts.Consts.tempAttrObjGroup);
        this.tempAttrsWithObject = new HashSet<int>();
        foreach (int num in attributesInGroup1)
          this.tempAttrsWithObject.Add(num);
        List<int> attributesInGroup2 = MetaDataHelper.GetAttributesInGroup(ExpertConsts.Consts.tempAttrGroup);
        this.tempAttrsWithoutObject = new HashSet<int>();
        foreach (int num in attributesInGroup2)
          this.tempAttrsWithoutObject.Add(num);
        this.docAttrs = ExpertConsts.Consts.docAttrGroup == 0 ? new List<int>() : MetaDataHelper.GetAttributesInGroup(ExpertConsts.Consts.docAttrGroup);
        this.compAttrs = ExpertConsts.Consts.compAttrGroup == 0 ? new List<int>() : MetaDataHelper.GetAttributesInGroup(ExpertConsts.Consts.compAttrGroup);
      }
      this.namedParms = new Dictionary<string, object>();
    }

    public HybridDictionary filtr() => this.cur_filtr != null ? this.cur_filtr : this.window_filtr;

    public override int GetHashCode() => this.taskId;

    public IUserSession GetSession()
    {
      return this.clonedSessionGUID != Guid.Empty ? ExpertServer._CheckGetSession(this.clonedSessionGUID) : throw new ExpertServerException("Attempt to access non-cloned client session!");
    }

    internal bool IsInChanged(long Id)
    {
      lock (this.changed)
      {
        foreach (ChangeInfo changeInfo in this.changed)
        {
          if (changeInfo.ID == Id || changeInfo.ID == -Id)
            return true;
        }
      }
      return false;
    }

    internal void AddChangedDoc(long docObjId, int ObjType, DocOperType dot)
    {
      if (this.IsInChanged(docObjId))
        return;
      ChangeObjInfo changeObjInfo = new ChangeObjInfo(docObjId, ObjType, dot);
      lock (this.changed)
        this.changed.Add((ChangeInfo) changeObjInfo);
    }

    internal void AddChangedRel(long relId, int ObjType, long projId, DocOperType dot)
    {
      if (this.IsInChanged(relId))
        return;
      ChangeRelInfo changeRelInfo = new ChangeRelInfo(relId, ObjType, projId, dot);
      lock (this.changed)
        this.changed.Add((ChangeInfo) changeRelInfo);
    }

    internal void RemoveObj(long Id)
    {
      lock (this.changed)
      {
        foreach (ChangeInfo changeInfo in this.changed)
        {
          if (changeInfo.ID == Id || changeInfo.ID == -Id)
          {
            this.changed.Remove(changeInfo);
            break;
          }
        }
      }
    }

    public List<long> ispList
    {
      get => this._ispList;
      set => this._ispList = value;
    }

    public List<long> FindCyclingObjects(ScriptTreeNode node, List<long> context)
    {
      List<long> accumList = new List<long>();
      foreach (ExpertServer.ExpServTask.CallStackItem call in this.callStack)
      {
        if (call.node == node)
          call.GetCommonPart(context, accumList);
      }
      return accumList;
    }

    public bool Push(ScriptTreeNode n, List<long> l)
    {
      if (this.callCounter.ContainsKey(n))
      {
        long num = this.callCounter[n];
        if (num >= 1000L)
          return false;
        this.callCounter[n] = num + 1L;
      }
      else
        this.callCounter.Add(n, 1L);
      this.callStack.Add(new ExpertServer.ExpServTask.CallStackItem(n, l));
      return true;
    }

    public void Pop()
    {
      ScriptTreeNode node = this.callStack[this.callStack.Count - 1].node;
      this.callStack.RemoveAt(this.callStack.Count - 1);
      if (!this.callCounter.ContainsKey(node))
        return;
      --this.callCounter[node];
    }

    public ScriptTreeNode CurNode()
    {
      return this.callStack.Count == 0 ? (ScriptTreeNode) null : this.callStack[this.callStack.Count - 1].node;
    }

    public void ClearUsedObjects()
    {
      if (this.UsedObjects == null)
        this.UsedObjects = new HashSet<long>();
      else
        this.UsedObjects.Clear();
    }

    public void InitTraceInfo()
    {
      this.traceInfo = new XmlDocument();
      this.traceInfo.LoadXml($"<?xml version='1.0' encoding='utf-16'?><TraceInfo xmlns='{ExpertServer.ExpertNamespace}'></TraceInfo>");
      this.curNode = (XmlNode) this.traceInfo.DocumentElement;
      this.rootTraceNode = new ExpertTraceNode();
      this.curTraceNode = this.rootTraceNode;
    }

    public byte[] GetPackedInfo()
    {
      if (this.traceInfo == null)
        return (byte[]) null;
      lock (this)
        return ZlibHelper.PackXmlBuffer(this.traceInfo);
    }

    public XmlNode traceAddElement(string Name)
    {
      if (!this.makeTrace)
        return (XmlNode) null;
      if (this.rootExclaimed)
      {
        ScriptTreeNode scriptTreeNode = this.CurNode();
        if (scriptTreeNode != null && !scriptTreeNode.ExclamationMarked)
          return (XmlNode) null;
      }
      lock (this)
      {
        if (this.blockTrace > 0)
          return (XmlNode) null;
        Name = Name.Replace(' ', '_');
        XmlNode element = (XmlNode) this.traceInfo.CreateElement(Name, ExpertServer.ExpertNamespace);
        if (this.curNode != null)
          this.curNode.AppendChild(element);
        return element;
      }
    }

    public XmlAttribute traceAddAttribute(XmlNode node, string Name, string Value)
    {
      if (!this.makeTrace)
        return (XmlAttribute) null;
      if (this.rootExclaimed)
      {
        ScriptTreeNode scriptTreeNode = this.CurNode();
        if (scriptTreeNode != null && !scriptTreeNode.ExclamationMarked)
          return (XmlAttribute) null;
      }
      lock (this)
      {
        if (this.blockTrace > 0)
          return (XmlAttribute) null;
        XmlAttribute attribute = this.traceInfo.CreateAttribute(Name);
        attribute.Value = Convert.ToString(Value);
        node.Attributes.Append(attribute);
        return attribute;
      }
    }

    public XmlNode traceAddText(XmlNode node, string Text)
    {
      if (!this.makeTrace)
        return (XmlNode) null;
      if (this.rootExclaimed)
      {
        ScriptTreeNode scriptTreeNode = this.CurNode();
        if (scriptTreeNode != null && !scriptTreeNode.ExclamationMarked)
          return (XmlNode) null;
      }
      lock (this)
      {
        if (this.blockTrace > 0)
          return (XmlNode) null;
        XmlNode textNode = (XmlNode) this.traceInfo.CreateTextNode(Text);
        node.AppendChild(textNode);
        return textNode;
      }
    }

    public void traceSetNode(XmlNode newCurNode)
    {
      if (!this.makeTrace || this._curNode == newCurNode)
        return;
      lock (this)
      {
        if (this.blockTrace > 0)
          return;
        this._curNode = newCurNode;
      }
    }

    public FileStream TraceGlobalTable(string path) => (FileStream) null;

    internal void InitTraceAndLog()
    {
      IDBConfigurations configurations = this.GetSession().Configurations;
      this.makeTrace = configurations.ReadBool("Expert_System", "User", "Show_Window", false, DBConfigMode.UserOnly);
      this.makeLog = configurations.ReadBool("Expert_System", "User", "Generate_ExpertLog", false, DBConfigMode.UserAndGlobal);
      long num = configurations.ReadInteger("Expert_System", "User", "Trace_Flags", 0L, DBConfigMode.UserAndGlobal);
      this.traceFlags = num != 0L ? (ExpertTraceFlags) num : ExpertTraceFlags.ShowObjConds | ExpertTraceFlags.ShowObjResults;
    }

    public TaskDataCache DataCache
    {
      [DebuggerStepThrough] get => this._dataCache;
    }

    public CalcAttrCacheBase<bool> NeededAttrs
    {
      [DebuggerStepThrough] get => this._neededAttrs;
    }

    public CalcAttrCacheBase<CalculatedAttr> CalcAttrs
    {
      [DebuggerStepThrough] get => this._calcAttrs;
    }

    public void InitDocTypes()
    {
      if (this.docTypes != null)
        return;
      this.docTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(ExpertServer.ExpServTask.objTypeDocumentation));
    }

    public bool GetAttributeValue(
      long objId,
      int attrTypeId,
      out object Value,
      ExpertServer.ExpServTask.AttrOptions opts = ExpertServer.ExpServTask.AttrOptions.None,
      HybridRowExp row = null)
    {
      Value = (object) null;
      IUserSession session = this.GetSession();
      if (attrTypeId < 0)
      {
        if (attrTypeId == -2 || attrTypeId == -3 || attrTypeId == -12 || attrTypeId == -50 || attrTypeId == -7)
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(objId);
          if (!objectInfo.Empty)
          {
            switch (attrTypeId)
            {
              case -50:
                Value = (object) objectInfo.Caption;
                break;
              case -12:
                Value = (object) objectInfo.VersionGuid;
                break;
              case -7:
                Value = (object) objectInfo.ObjectTypeID;
                break;
              case -3:
                Value = (object) objectInfo.ID;
                break;
              case -2:
                Value = (object) objectInfo.ObjectID;
                break;
            }
          }
        }
        if (Value == null && row != null)
        {
          Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
          int indexByName = row.Columns.GetIndexByName(attributeTypeGuid.ToString());
          if (indexByName >= 0)
            Value = row[indexByName];
        }
        if (Value == null)
          this.OptGetObjectAttr(objId, attrTypeId, out Value);
        if (Value == null)
        {
          IDBObject dbObject = session.GetObject(objId, false);
          if (dbObject != null)
          {
            object[] valuesById = dbObject.GetValuesByID(attrTypeId, false);
            if (valuesById.Length != 0)
              Value = valuesById[0];
          }
        }
        if (Value != null && (opts & ExpertServer.ExpServTask.AttrOptions.AsString) != ExpertServer.ExpServTask.AttrOptions.None)
          Value = (object) Convert.ToString(Value);
        return Value != null;
      }
      if (Value == null && row != null)
      {
        Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(attrTypeId);
        int indexByName = row.Columns.GetIndexByName(attributeTypeGuid.ToString());
        if (indexByName >= 0)
          Value = row[indexByName];
      }
      if (Value == null)
        this.OptGetObjectAttr(objId, attrTypeId, out Value);
      if (Value == null)
      {
        IDBObject dbObject = session.GetObject(objId, false);
        if (dbObject != null)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(attrTypeId);
          if (attributeById != null)
            Value = (opts & ExpertServer.ExpServTask.AttrOptions.AsString) == ExpertServer.ExpServTask.AttrOptions.None ? attributeById.Value : (object) attributeById.Description;
        }
      }
      return false;
    }

    public bool GetAttributeValue(
      long objId,
      Guid attrGuid,
      out object Value,
      ExpertServer.ExpServTask.AttrOptions opts = ExpertServer.ExpServTask.AttrOptions.None,
      HybridRowExp row = null)
    {
      Value = (object) null;
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGuid);
      return this.GetAttributeValue(objId, attributeTypeId, out Value, opts, row);
    }

    public bool GetAttributeValue(
      long objId,
      string attrGuidStr,
      out object Value,
      ExpertServer.ExpServTask.AttrOptions opts = ExpertServer.ExpServTask.AttrOptions.None,
      HybridRowExp row = null)
    {
      Value = (object) null;
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attrGuidStr);
      return this.GetAttributeValue(objId, attributeTypeId, out Value, opts, row);
    }

    internal object CalcFormula(long objId, HybridRowExp row, TempFormula tf)
    {
      object result = (object) null;
      return this.CalcFormula(new long[1]{ objId }, row, tf, out result, 0L) == ExpertResult.OK ? result : (object) null;
    }

    public ExpertResult CalcFormula(
      long[] objId,
      HybridRowExp row,
      TempFormula tf,
      out object result,
      long relId = 0)
    {
      lock (this)
      {
        XmlNode xmlNode = this.makeTrace ? this.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_231")) : (XmlNode) null;
        if (xmlNode != null)
        {
          this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_207"), Convert.ToString(objId[0]));
          if (objId.Length > 1)
            this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_232"), Convert.ToString(objId[1]));
          this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_233"), tf.Text);
          this.traceSetNode(xmlNode);
        }
      }
      ExpertResult expertResult = ExpertResult.OK;
      result = (object) null;
      XmlNode curNode = this.curNode;
      bool flag = false;
      long curRelationId = this.curRelationId;
      try
      {
        if (relId != 0L)
          this.curRelationId = relId;
        expertResult = this._CalcFormula(objId, row, tf, out result, false, relId);
        flag = true;
      }
      catch (Exception ex)
      {
        lock (this)
        {
          if (this.makeTrace)
            this.traceAddAttribute(this.curNode, LocalizationHolder.rm.GetString("Expert.Server_44"), ex.Message);
        }
      }
      finally
      {
        lock (this)
        {
          if (flag && this.makeTrace)
            this.traceAddAttribute(this.curNode, LocalizationHolder.rm.GetString("Expert.Server_92"), result != null ? result.ToString() : "null");
          this.curRelationId = curRelationId;
          this.traceSetNode(curNode);
        }
      }
      return expertResult;
    }

    internal object CalcRowFormula(long objId, HybridRowExp row, TempFormula tf, bool calcCond)
    {
      object Result = (object) null;
      return this._CalcFormula(new long[1]{ objId }, row, tf, out Result, calcCond) == ExpertResult.OK ? Result : (object) null;
    }

    public bool FlagIn(ExpertTraceFlags a, ExpertTraceFlags b) => (a & b) == a;

    internal bool CheckCond(long objId, TempFormula cond, long substId, long[] moreObjs = null)
    {
      lock (this)
      {
        if (!this.FlagIn(ExpertTraceFlags.ShowScriptConds, this.traceFlags))
          return this.CheckCondOnly(objId, cond, moreObjs);
      }
      object Result = (object) null;
      XmlNode curNode = this.curNode;
      try
      {
        XmlNode xmlNode = this.makeTrace ? this.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_53")) : (XmlNode) null;
        if (xmlNode != null)
        {
          this.traceSetNode(xmlNode);
          this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_54"), cond.Text);
        }
        ExpertResult expertResult = this._CalcFormula(ExpertServer.ComposeContext(objId, (IEnumerable<long>) moreObjs).ToArray(), (HybridRowExp) null, cond, out Result, true);
        if (xmlNode != null)
        {
          this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_55"), Convert.ToBoolean(Result) ? LocalizationHolder.rm.GetString("Expert.Server_56") : LocalizationHolder.rm.GetString("Expert.Server_57"));
          this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_58"), EnumTypeHelper.GetCaption((Enum) expertResult));
          this.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(substId != 0L ? substId : objId));
        }
        return expertResult == ExpertResult.OK && Convert.ToBoolean(Result);
      }
      catch (Exception ex)
      {
        if (this.makeTrace)
          this.traceAddAttribute(this.curNode, LocalizationHolder.rm.GetString("Expert.Server_59"), ex.Message);
      }
      finally
      {
        this.traceSetNode(curNode);
      }
      return false;
    }

    internal bool CheckCond(long objId, TempFormula cond)
    {
      lock (this)
      {
        if (!this.FlagIn(ExpertTraceFlags.ShowScriptConds, this.traceFlags))
          return this.CheckCondOnly(objId, cond);
      }
      object Result = (object) null;
      XmlNode curNode = this.curNode;
      try
      {
        XmlNode xmlNode = this.makeTrace ? this.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_53")) : (XmlNode) null;
        if (xmlNode == null)
          return false;
        this.traceSetNode(xmlNode);
        this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_54"), cond.Text);
        ExpertResult expertResult = this._CalcFormula(new long[1]
        {
          objId
        }, (HybridRowExp) null, cond, out Result, true);
        this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_55"), Convert.ToBoolean(Result) ? LocalizationHolder.rm.GetString("Expert.Server_56") : LocalizationHolder.rm.GetString("Expert.Server_57"));
        this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_58"), EnumTypeHelper.GetCaption((Enum) expertResult));
        this.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(objId));
        return expertResult == ExpertResult.OK && Convert.ToBoolean(Result);
      }
      catch (Exception ex)
      {
        if (this.makeTrace)
          this.traceAddAttribute(this.curNode, LocalizationHolder.rm.GetString("Expert.Server_59"), ex.Message);
      }
      finally
      {
        this.traceSetNode(curNode);
      }
      return false;
    }

    internal bool CheckGlobalCond(TempFormula cond) => this.CheckCond(-1L, cond, 0L);

    internal bool CheckGlobalCond(TempFormula cond, long objId, HybridRowExp row)
    {
      return objId != -1L ? this.CheckRowCond(objId, row, cond) : this.CheckCond(-1L, cond);
    }

    public bool CheckRowCond(long objId, HybridRowExp row, TempFormula cond)
    {
      XmlNode curNode = this.curNode;
      bool flag1 = this.FlagIn(ExpertTraceFlags.ShowObjConds, this.traceFlags);
      if (flag1 && this.makeTrace && this.traceInfo != null)
      {
        lock (this)
        {
          XmlNode xmlNode = this.traceAddElement(LocalizationHolder.rm.GetString("Expert.Server_226"));
          if (xmlNode != null)
          {
            this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_207"), Convert.ToString(objId));
            this.traceAddAttribute(xmlNode, LocalizationHolder.rm.GetString("Expert.Server_194"), cond.Text);
            this.traceAddAttribute(xmlNode, "_OBJ_ID_", Convert.ToString(objId));
            this.traceSetNode(xmlNode);
          }
        }
      }
      bool flag2 = false;
      bool flag3 = false;
      try
      {
        flag2 = Convert.ToBoolean(this.CalcRowFormula(objId, row, cond, true));
        flag3 = true;
      }
      catch (Exception ex)
      {
        if (flag1)
        {
          if (this.makeTrace)
          {
            if (this.traceInfo != null)
            {
              lock (this)
                this.traceAddAttribute(this.curNode, LocalizationHolder.rm.GetString("Expert.Server_44"), ex.Message);
            }
          }
        }
      }
      finally
      {
        if (flag1 && this.makeTrace && this.traceInfo != null)
        {
          lock (this)
          {
            if (flag3)
              this.traceAddAttribute(this.curNode, LocalizationHolder.rm.GetString("Expert.Server_92"), flag2 ? LocalizationHolder.rm.GetString("Expert.Server_32") : LocalizationHolder.rm.GetString("Expert.Server_33"));
          }
        }
        this.traceSetNode(curNode);
      }
      return flag2;
    }

    internal bool CheckCondOnly(long objId, TempFormula cond, long[] moreObjs = null)
    {
      object Result = (object) null;
      return this._CalcFormula(ExpertServer.ComposeContext(objId, (IEnumerable<long>) moreObjs).ToArray(), (HybridRowExp) null, cond, out Result, true) == ExpertResult.OK && Convert.ToBoolean(Result);
    }

    internal bool CheckGlobalCondOnly(TempFormula cond) => this.CheckCondOnly(-1L, cond);

    internal ExpertResult _CalcFormula(
      long[] objID,
      HybridRowExp row,
      TempFormula tf,
      out object Result,
      bool calcCond,
      long relId = 0)
    {
      if (tf.Count == 0)
      {
        if (tf.resType == DataType.Boolean)
        {
          Result = (object) true;
          return ExpertResult.OK;
        }
        Result = (object) null;
        return ExpertResult.Unknown;
      }
      ExpertServer.Calculator calculator = new ExpertServer.Calculator(this, objID, row, tf);
      calculator.calcCond = calcCond;
      calculator.relationId = relId;
      Result = calculator.Perform();
      if (Result != null)
      {
        switch (tf.resType)
        {
          case DataType.Float:
            if (Result is MeasuredValue)
            {
              Result = (object) (Result as MeasuredValue).Value;
              break;
            }
            break;
          case DataType.String:
            if (!(Result is string))
            {
              DataType dataType = DataTypeConvertor.GetDataType(Result);
              switch (dataType)
              {
                case DataType.Float:
                case DataType.Measured:
                case DataType.Date:
                  Result = (object) ExpertServer.MakeString(Result, dataType, this);
                  break;
                default:
                  Result = (object) Convert.ToString(Result);
                  break;
              }
            }
            else
              break;
            break;
        }
      }
      return calculator.calcRes;
    }

    public void InitInbuiltSort(IUserSession ius, List<Triple> list)
    {
      this._vedomostiSortingCache = new VedomostiSortingCache(ius, this.docScriptId, list);
    }

    public void BeforeSorting(List<long> objects)
    {
    }

    public void SetTriple(string s) => this._vedomostiSortingCache.CurrentTriple = s;

    public int InbuiltCompare(long objId1, long objId2, HybridRowExp dr1, HybridRowExp dr2)
    {
      return this._vedomostiSortingCache != null ? this._vedomostiSortingCache.Compare(objId1, objId2, dr1, dr2) : 0;
    }

    internal CalculatedAttr __GetValue(CalcAttrPair key)
    {
      return this.__GetValue(key.objID, key.objTypeID, key.attrTypeID);
    }

    internal CalculatedAttr __GetValue(long aObjId, int aObjTypeId, int aAttrTypeId)
    {
      CalculatedAttr calculatedAttr = (CalculatedAttr) null;
      if (this.CalcAttrs.TryGetValue(aObjId, aObjTypeId, aAttrTypeId, out calculatedAttr) || aObjId == -1L)
        return calculatedAttr;
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(aObjTypeId);
      int objTypeId = -1;
      TaskDataCache.ObjDataItem objData = this.DataCache.GetObjData(aObjId, this.GetSession());
      if ((TypedInfoItem) objData != (TypedInfoItem) null)
        objTypeId = objData.ObjTypeID;
      if (objTypeId != -1 && childrenIdRecursive.Contains(objTypeId))
      {
        if (this.CalcAttrs.TryGetValue(aObjId, -1, aAttrTypeId, out calculatedAttr) || objTypeId != aObjTypeId && this.CalcAttrs.TryGetValue(aObjId, objTypeId, aAttrTypeId, out calculatedAttr))
          return calculatedAttr;
      }
      else if (aObjId != -1L && aObjTypeId == -1)
      {
        if ((TypedInfoItem) objData == (TypedInfoItem) null)
        {
          aObjTypeId = ExpertServer.GetTypeId(this.GetSession(), aObjId);
          if (!this.DataCache.ObjDataCache.ContainsKey(aObjId))
            this.DataCache.ObjDataCache.Add(aObjId, new TaskDataCache.ObjDataItem(aObjId, aObjTypeId));
        }
        else
          aObjTypeId = objData.ObjTypeID;
        if (aObjTypeId != -1 && this.CalcAttrs.TryGetValue(aObjId, aObjTypeId, aAttrTypeId, out calculatedAttr))
          return calculatedAttr;
      }
      return (CalculatedAttr) null;
    }

    internal void __SetValue(CalcAttrPair ca_pair, object Value)
    {
      this.__SetValue(ca_pair, Value, AttrState.Unknown);
    }

    internal void __SetValue(CalcAttrPair ca_pair, object Value, AttrState aState)
    {
      this.__SetValue(new CalculatedAttr(ca_pair, Value, aState));
    }

    internal void __SetValue(CalculatedAttr calc_attr)
    {
      if (calc_attr.Value.IsNullOrDBNull())
        return;
      CalculatedAttr calculatedAttr = (CalculatedAttr) null;
      if (this.CalcAttrs.TryGetValue(calc_attr.ca_pair, out calculatedAttr))
      {
        calculatedAttr.Value = calc_attr.Value;
        calculatedAttr.attState = calc_attr.attState;
      }
      else if (calc_attr.ca_pair.objTypeID != -1 && this.CalcAttrs.TryGetValue(calc_attr.ca_pair.objID, -1, calc_attr.ca_pair.attrTypeID, out calculatedAttr))
      {
        calculatedAttr.Value = calc_attr.Value;
        calculatedAttr.attState = calc_attr.attState;
      }
      else
        this.CalcAttrs.Add(calc_attr.ca_pair, calc_attr);
    }

    internal object __SetValue(
      CalcAttrPair ca_pair,
      object Value,
      AttrState aState,
      int X,
      int Y)
    {
      if (Value.IsNullOrDBNull())
        return Value;
      CalculatedAttr calculatedAttr1 = (CalculatedAttr) null;
      arrayHolder2 = (ArrayHolder) null;
      if (this.CalcAttrs.TryGetValue(ca_pair, out calculatedAttr1))
      {
        if (!(calculatedAttr1.Value is ArrayHolder arrayHolder2))
        {
          arrayHolder2 = new ArrayHolder(X + 1, Y + 1);
          calculatedAttr1.Value = (object) arrayHolder2;
        }
      }
      else if (ca_pair.objTypeID != -1 && this.CalcAttrs.TryGetValue(ca_pair.objID, -1, ca_pair.attrTypeID, out calculatedAttr1) && !(calculatedAttr1.Value is ArrayHolder arrayHolder2))
      {
        arrayHolder2 = new ArrayHolder(X + 1, Y + 1);
        calculatedAttr1.Value = (object) arrayHolder2;
      }
      if (arrayHolder2 != null)
      {
        arrayHolder2[X, Y] = Value;
        return (object) arrayHolder2;
      }
      ArrayHolder Val = new ArrayHolder(X + 1, Y + 1);
      CalculatedAttr calculatedAttr2 = new CalculatedAttr(ca_pair, (object) Val, aState);
      Val[X, Y] = Value;
      this.CalcAttrs.Add(ca_pair, calculatedAttr2);
      return (object) Val;
    }

    public IExpertGlobalTable GlobalTable => (IExpertGlobalTable) this;

    public DocRecord GeneratedComplect
    {
      get
      {
        List<DocRecord> docList = this.docList;
        return docList == null ? (DocRecord) null : docList.FirstOrDefault<DocRecord>((System.Func<DocRecord, bool>) (d => d.IsComplect()));
      }
    }

    public IEnumerable<DocRecord> DocumentRecords => (IEnumerable<DocRecord>) this.docList;

    public int TaskId => this.taskId;

    public IUserSession Session => this.GetSession();

    public ObjInfoCaptionItem GetObjectData(long objId)
    {
      IUserSession session = this.GetSession();
      return (ObjInfoCaptionItem) this._dataCache.GetObjData(objId, session);
    }

    public RelInfoItem GetRelationData(long relId)
    {
      IUserSession session = this.GetSession();
      return (RelInfoItem) this._dataCache.GetRelData(relId, session);
    }

    public List<CalcAttrPair> GetNeededAttrs() => this.NeededAttrs.Keys.ToList<CalcAttrPair>();

    public bool IsAttrNeeded(long objId, int objTypeId, int attrTypeId)
    {
      return this.NeededAttrs.ContainsAttr(objId, objTypeId, attrTypeId);
    }

    public void AddNeededAttr(long objId, int objTypeId, int attrTypeId)
    {
      this.NeededAttrs.AddAttr(objId, objTypeId, attrTypeId, true);
    }

    public void RemoveNeededAttr(long objId, int objTypeId, int attrTypeId)
    {
      this.NeededAttrs.Remove(objId, objTypeId, attrTypeId);
    }

    public void ClearNeededAttrs() => this.NeededAttrs.Clear();

    public object this[CalcAttrPair attr]
    {
      get => (object) this.__GetValue(attr);
      set => this.__SetValue(attr, value);
    }

    public AttrState GetAttributeState(CalcAttrPair attr)
    {
      AttrState attributeState = AttrState.Unknown;
      CalculatedAttr calculatedAttr;
      if (this.CalcAttrs.TryGetValue(attr, out calculatedAttr))
        attributeState = calculatedAttr.attState;
      return attributeState;
    }

    public void SetAttributeState(CalcAttrPair attr, AttrState newState)
    {
      CalculatedAttr calculatedAttr;
      if (!this.CalcAttrs.TryGetValue(attr, out calculatedAttr))
        return;
      calculatedAttr.attState = newState;
      this.CalcAttrs[attr] = calculatedAttr;
    }

    public ImDocumentData GetDocTemplate(long templateId) => this._cacheTemplates[templateId];

    public bool TraceEnabled
    {
      get => this.makeTrace;
      set => this.makeTrace = value;
    }

    public XmlNode TraceAddElement(string name) => this.traceAddElement(name);

    public XmlAttribute TraceAddAttribute(XmlNode node, string name, string value)
    {
      return this.traceAddAttribute(node, name, value);
    }

    public XmlNode TraceAddText(XmlNode node, string text) => this.traceAddText(node, text);

    public XmlNode CurrentNode
    {
      get => this._curNode;
      set => this.traceSetNode(value);
    }

    public IServiceContainer Services { get; private set; } = (IServiceContainer) new ServiceContainer();

    internal void ReleaseServices()
    {
      if (!(this.Services is ServiceContainer services))
        return;
      services.Dispose();
      this.Services = (IServiceContainer) null;
    }

    public ExpertResult CalcFormulaQuiet(
      long[] objId,
      HybridRowExp row,
      TempFormula tf,
      out object result,
      long relId = 0)
    {
      return this._CalcFormula(objId, row, tf, out result, false, relId);
    }

    public bool StartJob(bool needClone = true)
    {
      if (this.thread != null)
        return false;
      ExpertServer.es.StartJobForTask(this, needClone);
      return true;
    }

    public void EndJob() => ExpertServer.es.EndJobForTask(this);

    public bool IsJobRunning() => this.thread != null;

    public HybridTableExp Objects => this.savedData;

    public HybridTableExp Relations => this.savedLinks;

    public int ObjectIndex(long objId) => this.dataObjIndex[objId];

    public int RelIndex(long relId) => this.linksIdIndex[relId];

    public int ObjByPartIndex(long partId) => this.dataPartIndex[partId];

    public HybridRowExp SavedDataByObjId(long objId) => this.savedDataByObjId(objId);

    public HybridRowExp SavedDataByPartId(long partId) => this.savedDataByPartId(partId);

    public HybridRowExp[] SavedLinksByProjId(long projId) => this.savedLinksByProjId(projId);

    public HybridRowExp[] SavedLinksByPartId(long partId) => this.savedLinksByPartId(partId);

    public class AttrTypeInfo
    {
      public int Id;
      public string aGuid;
      public string Name;

      public AttrTypeInfo(int typeId)
      {
        this.Id = typeId;
        this.aGuid = MetaDataHelper.GetAttributeTypeGuid(this.Id).ToString();
        this.Name = MetaDataHelper.GetAttributeTypeName(this.Id);
      }

      public AttrTypeInfo(string sGuid)
      {
        this.aGuid = sGuid;
        Guid attrTypeGuid = new Guid(sGuid);
        this.Id = MetaDataHelper.GetAttributeTypeID(attrTypeGuid);
        this.Name = MetaDataHelper.GetAttributeTypeName(attrTypeGuid);
      }

      public AttrTypeInfo(int typeId, string sGuid)
      {
        this.Id = typeId;
        this.aGuid = sGuid;
        this.Name = MetaDataHelper.GetAttributeTypeName(this.Id);
      }

      public AttrTypeInfo(ExpertServer.ExpServTask.AttrTypeInfo other)
      {
        this.Id = other.Id;
        this.aGuid = other.aGuid;
        this.Name = other.Name;
      }
    }

    public class ObjTypeAttrs
    {
      public int objTypeId;
      public List<ExpertServer.ExpServTask.AttrTypeInfo> attrs;

      public ObjTypeAttrs(int OTId)
      {
        this.objTypeId = OTId;
        this.attrs = new List<ExpertServer.ExpServTask.AttrTypeInfo>(10);
      }

      public bool Contains(int attrType)
      {
        foreach (ExpertServer.ExpServTask.AttrTypeInfo attr in this.attrs)
        {
          if (attr.Id == attrType)
            return true;
        }
        return false;
      }

      public bool AddAttr(int attrType)
      {
        if (this.Contains(attrType))
          return false;
        this.attrs.Add(new ExpertServer.ExpServTask.AttrTypeInfo(attrType));
        return true;
      }

      public bool AddAttr(ExpertServer.ExpServTask.AttrTypeInfo ati)
      {
        if (this.Contains(ati.Id) || ati.aGuid.ToString() == "cad00029-306c-11d8-b4e9-00304f19f545" || ati.Id == -10000 || ati.Id == 0)
          return false;
        this.attrs.Add(ati);
        return true;
      }

      public int GetAttrIndex(int attrId)
      {
        for (int index = 0; index < this.attrs.Count; ++index)
        {
          if (this.attrs[index].Id == attrId)
            return index;
        }
        return -1;
      }
    }

    public class CallStackItem
    {
      public ScriptTreeNode node;
      public List<long> context;

      public CallStackItem(ScriptTreeNode n, List<long> c)
      {
        this.node = n;
        this.context = c;
      }

      public bool GetCommonPart(List<long> listTest, List<long> accumList)
      {
        bool commonPart = false;
        foreach (long num in listTest)
        {
          if (this.context.Contains(num))
          {
            commonPart = true;
            if (!accumList.Contains(num))
              accumList.Add(num);
          }
        }
        return commonPart;
      }
    }

    [Flags]
    public enum AttrOptions
    {
      None = 0,
      AsString = 1,
    }
  }
}
