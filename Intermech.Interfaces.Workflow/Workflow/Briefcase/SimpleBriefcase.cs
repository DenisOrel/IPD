// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Briefcase.SimpleBriefcase
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow.Briefcase;

public class SimpleBriefcase
{
  private IUserSession _userSession;
  private List<long> _objectIDs = new List<long>();
  private HashSet<long> _roots = new HashSet<long>();
  private Dictionary<int, SpecialRule> _specialRules = new Dictionary<int, SpecialRule>();
  private Dictionary<int, Tuple<int, SelectFunction, int>> _exportedRelations = new Dictionary<int, Tuple<int, SelectFunction, int>>();
  private string _debug = "";
  private bool PackedFormat = true;
  public int Version = 1;
  private SimpleBriefcase.Mapper _map = new SimpleBriefcase.Mapper();
  private SimpleBriefcase.BriefcaseItemList _items = new SimpleBriefcase.BriefcaseItemList();
  private XmlSerializer _formatter;
  private string _errors = "";
  private string _fileName = "";
  private Dictionary<long, IDBObject> _objs = new Dictionary<long, IDBObject>();
  private static XmlSerializerNamespaces _emptyNamespace;
  private bool? _createVariables;

  public IUserSession Session => this._userSession;

  public SimpleBriefcase(IUserSession session) => this._userSession = session;

  /// <summary>
  /// Конструктор для серилизации, если нужно использовать внутренние методы с сессией, обязательно использовать конструктор с параметром
  /// </summary>
  public SimpleBriefcase()
  {
  }

  public void AddObject(long objectID, bool root = false)
  {
    objectID = Math.Abs(objectID);
    if (!this._objectIDs.Contains(objectID))
      this._objectIDs.Add(objectID);
    if (!root)
      return;
    this._roots.Add(objectID);
  }

  /// <summary>
  /// Позволяет регистрировать правила, меняющие логику сохранения отдельных атрибутов/объектов
  /// </summary>
  public void RegisterSpecialRule(int typeID, SpecialRule rule)
  {
    SpecialRule specialRule;
    this._specialRules.TryGetValue(typeID, out specialRule);
    this._specialRules[typeID] = specialRule | rule;
  }

  public void RegisterExportedRelation(
    int typeID,
    int relationType,
    SelectFunction direction,
    int toTypeID = 0)
  {
    this._exportedRelations.Add(typeID, new Tuple<int, SelectFunction, int>(relationType, direction, toTypeID));
  }

  public SimpleBriefcase.Mapper Map
  {
    get => this._map;
    set => this._map = value;
  }

  public SimpleBriefcase.BriefcaseItemList Items
  {
    get => this._items;
    set => this._items = value;
  }

  public void Export(string filename)
  {
    this._errors = "";
    bool flag = ControlFuncs.IsKeyPressed(Keys.ControlKey) && ControlFuncs.IsKeyPressed(Keys.ShiftKey);
    this._items.Clear();
    this._map.Clear();
    this._debug = "";
    for (int index = 0; index < this._objectIDs.Count; ++index)
    {
      IDBObject objectActualCopy = this.Session.GetObjectActualCopy(this._objectIDs[index], false);
      if (objectActualCopy != null)
        this.Export(objectActualCopy, this._roots.Contains(this._objectIDs[index]));
    }
    using (FileStream fileStream = new FileStream(filename, FileMode.Create))
    {
      using (MemoryStream inStream = new MemoryStream())
      {
        byte[] preamble = Encoding.UTF8.GetPreamble();
        inStream.Write(preamble, 0, preamble.Length);
        this.Formatter.Serialize((Stream) inStream, (object) this);
        inStream.Position = 0L;
        if (this.PackedFormat && !flag)
          ZLibStreamHelper.PackStream((Stream) inStream, ZLibCompressLevels.LevelNormal, (Stream) fileStream);
        else
          inStream.CopyTo((Stream) fileStream);
      }
    }
  }

  public event SimpleBriefcase.ObjectExportingDelegate ObjectExporting;

  protected void Export(IDBObject obj, bool isRoot)
  {
    SimpleBriefcase.ObjectExportingDelegate objectExporting = this.ObjectExporting;
    if (objectExporting != null)
      objectExporting(this, obj);
    if (this._debug != "")
      this._debug += "\r\n";
    this._debug += $"OBJ NAME={obj.Caption} TYPE={MetaDataHelper.GetObjectTypeGuid(obj.ObjectType)} ID={obj.ObjectID} GUID={obj.ObjectGUID} \r\n";
    IMSObjectType objectType = MetaDataHelper.GetObjectType(obj.ObjectType);
    this._map.Add(Domain.ObjectTypes, (long) obj.ObjectType, objectType.Guid, objectType.ObjectTypeName);
    this._map.Add(Domain.Objects, obj.ObjectID, obj.ObjectGUID, obj.Caption);
    this.AddMapping(Domain.Steps, (long) obj.LCStep);
    this._items.Add((SimpleBriefcase.BriefcaseItem) new SimpleBriefcase.BriefcaseObject(obj.ObjectID, obj.ObjectType, obj.Caption, obj.LCStep, isRoot));
    foreach (IDBAttribute attr in obj.Attributes.ToList())
    {
      if (attr.AttributeID != objectType.CaptionAttribute)
        this.Export((IDBAttributable) obj, attr);
    }
    foreach (KeyValuePair<int, Tuple<int, SelectFunction, int>> exportedRelation in this._exportedRelations)
    {
      if (MetaDataHelper.IsObjectTypeChildOf(obj.TypeID, exportedRelation.Key))
      {
        IDBRelationCollection relationCollection = this.Session.GetRelationCollection(exportedRelation.Value.Item1);
        relationCollection.LocalTypesMode = true;
        object[] columns = new object[2]
        {
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        };
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure((int) (exportedRelation.Value.Item2 == SelectFunction.ConsistFrom ? ObligatoryObjectAttributes.F_PROJ_ID : ObligatoryObjectAttributes.F_PART_ID), RelationalOperators.Equal, (object) obj.ObjectID, LogicalOperators.AND, 0, false)
        }, columns, 0L, (object) null, -1);
        foreach (DataRow row in (InternalDataCollectionBase) relationCollection.Select(paramSet).Rows)
        {
          long int64 = Convert.ToInt64(row[1]);
          this.AddObject(int64);
          this.Export(this.Session.GetRelation(Convert.ToInt64(row[0])), int64);
        }
      }
    }
  }

  public event SimpleBriefcase.AttributeExportingDelegate AttributeExporting;

  protected void Export(IDBAttributable obj, IDBAttribute attr)
  {
    if (attr.TemporaryAttribute)
      return;
    List<object> objectList = new List<object>();
    SimpleBriefcase.BlobInfo blob = (SimpleBriefcase.BlobInfo) null;
    MemoryStream ms = (MemoryStream) null;
    try
    {
      IDBAttributesGroup attributesGroup = obj.Session.GetAttributesGroup(wfConsts.GlobalVariablesGroupID);
      int valuesCount = attr.ValuesCount;
      for (int index = 0; index < valuesCount; ++index)
      {
        if (attr.Values[index] != null)
        {
          object obj1 = attr.Values[index];
          string name = attr.Name;
          int dataType = (int) attr.DataType;
          int attributeId = attr.AttributeID;
          if (dataType == 5)
          {
            if (blob != null)
              throw new InvalidOperationException("Multiple valued blobs are not supported");
            if (attr is IBlobReader blobReader)
            {
              if (ms == null)
                ms = new MemoryStream();
              BlobInformation blobInformation = blobReader.OpenBlob(0);
              try
              {
                if (blobInformation.RealFileSize > 0L)
                {
                  byte[] buffer = blobReader.ReadDataBlock(0);
                  ms.Write(buffer, 0, buffer.Length);
                  ms.Position = 0L;
                }
              }
              finally
              {
                blobReader.CloseBlob();
              }
              string base64String = Convert.ToBase64String(ms.ToArray());
              if (base64String != "")
              {
                blob = new SimpleBriefcase.BlobInfo();
                blob.Arc = blobInformation.ArcMethod;
                blob.RealFileSize = blobInformation.RealFileSize;
                blob.Binary = base64String;
              }
            }
          }
          if (DBNull.Value.Equals(obj1))
            obj1 = (object) null;
          if (obj1 is string && obj1.ToString() == "")
            obj1 = (object) null;
          if (obj1 != null || blob != null)
            objectList.Add(obj1);
        }
      }
      if (objectList.Count <= 0 && blob == null)
        return;
      SimpleBriefcase.BriefcaseAttribute attr1 = new SimpleBriefcase.BriefcaseAttribute((long) attr.AttributeID, objectList.ToArray(), blob);
      SimpleBriefcase.AttributeExportingDelegate attributeExporting = this.AttributeExporting;
      if (attributeExporting != null && !attributeExporting(this, obj, attr1, ms))
        return;
      if (attr.GroupName == attributesGroup.GroupName)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attr.AttributeID);
        if (attributeType != null)
        {
          MapperVariable mapperVariable = new MapperVariable(attributeType);
          this._map.Add(Domain.GlobalVariables, (long) attr.AttributeID, (MapperObject) mapperVariable);
        }
      }
      this._map.Add(Domain.Attributes, (long) attr.AttributeID, attr.AttributeType.GUID, attr.Name);
      if (attr.DataType == FieldTypes.ftObjectLink)
      {
        foreach (object obj2 in objectList)
          this.AddMapping(Domain.Objects, Convert.ToInt64(obj2));
      }
      this._items.Add((SimpleBriefcase.BriefcaseItem) attr1);
    }
    finally
    {
      ms?.Dispose();
    }
  }

  protected void Export(IDBRelation rel, long partObjectID)
  {
    IMSRelationType relationType = MetaDataHelper.GetRelationType(rel.RelationType);
    this._map.Add(Domain.RelTypes, (long) rel.RelationType, relationType.Guid, relationType.Text);
    long id = Math.Abs(rel.RelationID);
    this._map.Add(Domain.Relations, id, rel.GUID, "");
    this._items.Add((SimpleBriefcase.BriefcaseItem) new SimpleBriefcase.BriefcaseRelation(id, rel.TypeID, rel.ProjID, partObjectID));
    foreach (IDBAttribute attr in rel.Attributes.ToList())
      this.Export((IDBAttributable) rel, attr);
  }

  internal XmlSerializer Formatter
  {
    get
    {
      if (this._formatter == null)
        this._formatter = new XmlSerializer(typeof (SimpleBriefcase));
      return this._formatter;
    }
  }

  public string Errors => this._errors;

  private void Error(string msg)
  {
    if (this._errors != "")
      this._errors += "\r\n";
    this._errors += msg;
  }

  public string FileName => this._fileName;

  public void Load(string filename)
  {
    using (FileStream fileStream = new FileStream(filename, FileMode.Open))
    {
      this._fileName = filename;
      this.Load((Stream) fileStream);
    }
  }

  public void Load(Stream stream)
  {
    this._objectIDs.Clear();
    this._roots.Clear();
    this._items.Clear();
    this._map.Clear();
    byte[] preamble = Encoding.UTF8.GetPreamble();
    byte[] numArray = new byte[preamble.Length];
    stream.Position = 0L;
    stream.Read(numArray, 0, preamble.Length);
    int num = ((IEnumerable<byte>) numArray).SequenceEqual<byte>((IEnumerable<byte>) preamble) ? 1 : 0;
    stream.Position = 0L;
    MemoryStream outStream = (MemoryStream) null;
    if (num == 0)
    {
      outStream = new MemoryStream();
      ZLibStreamHelper.UnpackStream(stream, (Stream) outStream);
      stream.Position = 0L;
      if (stream is FileStream && ControlFuncs.IsKeyPressed(Keys.ControlKey) && ControlFuncs.IsKeyPressed(Keys.ShiftKey))
      {
        using (FileStream destination = new FileStream((stream as FileStream).Name + ".xml", FileMode.Create))
        {
          outStream.CopyTo((Stream) destination);
          throw new AbortException();
        }
      }
      stream = (Stream) outStream;
    }
    if (stream.Length > 0L)
    {
      SimpleBriefcase simpleBriefcase = this.Formatter.Deserialize(stream) as SimpleBriefcase;
      this._items = simpleBriefcase.Items;
      this._map = simpleBriefcase.Map;
    }
    outStream?.Dispose();
  }

  public static SimpleBriefcase Load(IDBObject obj)
  {
    SimpleBriefcase simpleBriefcase = (SimpleBriefcase) null;
    IDBAttribute attributeById = obj.GetAttributeByID(wfConsts.AttrBriefcaseID);
    if (attributeById != null)
    {
      using (MemoryStream aDestStream = new MemoryStream())
      {
        BlobProcReader blobProcReader = new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
        blobProcReader.ReadData();
        if (blobProcReader.Result)
        {
          simpleBriefcase = new SimpleBriefcase(obj.Session);
          simpleBriefcase.Load((Stream) aDestStream);
        }
      }
    }
    return simpleBriefcase;
  }

  /// <summary>
  /// Срабатывает в ситуации, когда импортируемый объект найден в базе по гуиду. Можно сбросить привязку, присвоив объекту null, или отменить процедуру импорта, сгенерировав Intermech.AbortException
  /// </summary>
  public event SimpleBriefcase.BriefcaseObjectFound ObjectFound;

  /// <summary>
  /// Срабатывает в конце импорта, перед возвратом объектов в архив
  /// </summary>
  public event SimpleBriefcase.BriefcaseImportedDelegate BriefcaseImported;

  private long GetNewObjectID(IUserSession session, long id)
  {
    IDBObject dbObject = (IDBObject) null;
    if (!this._objs.TryGetValue(id, out dbObject))
    {
      MapperObject mapperObject = this._map.Get(Domain.Objects, id);
      if (mapperObject != null)
        dbObject = session.GetObject(mapperObject.Guid, false);
    }
    return dbObject != null ? dbObject.ObjectID : 0L;
  }

  public List<SimpleBriefcase.ImportedObjectInfo> Import(string filename)
  {
    this._errors = string.Empty;
    this.Load(filename);
    Func<string, SimpleBriefcase, bool> importPrompt = this.ImportPrompt;
    if (importPrompt != null && !importPrompt(filename, this))
      return (List<SimpleBriefcase.ImportedObjectInfo>) null;
    List<SimpleBriefcase.ImportedObjectInfo> importedObjectInfoList = new List<SimpleBriefcase.ImportedObjectInfo>();
    bool flag1 = false;
    try
    {
      bool flag2 = false;
      SerializableDictionary<long, MapperObject> serializableDictionary1 = (SerializableDictionary<long, MapperObject>) null;
      if (this.Map.Items.TryGetValue(Domain.Variables, out serializableDictionary1))
      {
        foreach (KeyValuePair<long, MapperObject> keyValuePair in (Dictionary<long, MapperObject>) serializableDictionary1)
        {
          MapperVariable mapperVariable = keyValuePair.Value as MapperVariable;
          if (MetaDataHelper.GetAttributeTypeID(mapperVariable.Guid) <= 0)
          {
            if (this.CreateVariables)
            {
              VarsHelper.CreateVariableType(this.Session, mapperVariable.Caption, mapperVariable.Type, mapperVariable.Guid, mapperVariable.ValuesList);
              flag1 = true;
            }
            else
              break;
          }
        }
      }
      SerializableDictionary<long, MapperObject> serializableDictionary2 = (SerializableDictionary<long, MapperObject>) null;
      if (this.Map.Items.TryGetValue(Domain.GlobalVariables, out serializableDictionary2))
      {
        foreach (KeyValuePair<long, MapperObject> keyValuePair in (Dictionary<long, MapperObject>) serializableDictionary2)
        {
          MapperVariable mapperVariable = keyValuePair.Value as MapperVariable;
          if (MetaDataHelper.GetAttributeTypeID(mapperVariable.Guid) <= 0)
          {
            if (this.CreateVariables)
            {
              VarsHelper.CreateVariableType(this.Session, mapperVariable.Caption, mapperVariable.Type, mapperVariable.Guid, mapperVariable.ValuesList, VarKind.Global);
              flag1 = true;
            }
            else
              break;
          }
        }
      }
      if (flag1)
      {
        if (ApplicationServices.Container.GetService(typeof (IClientCache)) is IClientCache service)
          service.ReloadCacheCategory(3, this.Session);
        MiscFunx.ReloadVariablesCache(this.Session);
      }
      SimpleBriefcase.BriefcaseItemList briefcaseItemList = new SimpleBriefcase.BriefcaseItemList();
      SimpleBriefcase.BriefcaseItemList collection = new SimpleBriefcase.BriefcaseItemList();
      bool flag3 = false;
      foreach (SimpleBriefcase.BriefcaseItem briefcaseItem in (List<SimpleBriefcase.BriefcaseItem>) this._items)
      {
        if (briefcaseItem is SimpleBriefcase.BriefcaseRelation)
          flag3 = true;
        else if (briefcaseItem is SimpleBriefcase.BriefcaseObject)
          flag3 = false;
        if (flag3)
          collection.Add(briefcaseItem);
        else
          briefcaseItemList.Add(briefcaseItem);
      }
      briefcaseItemList.AddRange((IEnumerable<SimpleBriefcase.BriefcaseItem>) collection);
      this._items = briefcaseItemList;
      List<Tuple<SimpleBriefcase.BriefcaseObject, SimpleBriefcase.BriefcaseAttribute>> tupleList = new List<Tuple<SimpleBriefcase.BriefcaseObject, SimpleBriefcase.BriefcaseAttribute>>();
      this._objs.Clear();
      SimpleBriefcase.BriefcaseObject briefcaseObject1 = (SimpleBriefcase.BriefcaseObject) null;
      SimpleBriefcase.BriefcaseRelation briefcaseRelation = (SimpleBriefcase.BriefcaseRelation) null;
      IDBObject dbObject1 = (IDBObject) null;
      IDBRelation dbRelation = (IDBRelation) null;
      foreach (SimpleBriefcase.BriefcaseItem briefcaseItem in (List<SimpleBriefcase.BriefcaseItem>) this._items)
      {
        switch (briefcaseItem)
        {
          case SimpleBriefcase.BriefcaseObject _:
            briefcaseRelation = (SimpleBriefcase.BriefcaseRelation) null;
            briefcaseObject1 = (SimpleBriefcase.BriefcaseObject) briefcaseItem;
            int newType = this._map.GetNewType((long) briefcaseObject1.TypeID);
            flag2 = newType == 0;
            if (flag2)
            {
              this.Error($"Тип объектов \"{briefcaseObject1.TypeID}\" не найден, объект \"{briefcaseObject1.Caption}\" не создан");
              flag2 = true;
              continue;
            }
            dbObject1 = (IDBObject) null;
            MapperObject mapperObject1 = this._map.Get(Domain.Objects, briefcaseObject1.ID);
            if (mapperObject1 == null)
              throw new Exception($"Не найдена информация об объекте ObjectID=\"{briefcaseObject1.ID}\"");
            dbObject1 = this.Session.GetObject(mapperObject1.Guid, false);
            SimpleBriefcase.BriefcaseObjectFound objectFound = this.ObjectFound;
            if (dbObject1 != null && objectFound != null)
              objectFound(this, ref dbObject1);
            if (dbObject1 == null)
            {
              IDBObjectCollection objectCollection = this.Session.GetObjectCollection(newType);
              if (objectCollection != null)
              {
                dbObject1 = objectCollection.Create(mapperObject1.Guid);
                if (dbObject1 is IActivity activity2)
                  activity2.Flags |= ActivityFlags.Importing;
                else if (dbObject1 is IProcess activity1)
                {
                  ExtProperties extProperties = new ExtProperties((IDBObject) activity1, wfConsts.AttrAddInfoID);
                  extProperties.WriteBool("ImportingProcess", true, ExtPropertiesFlag.ThreadID);
                  extProperties.Save((IDBObject) activity1);
                }
                try
                {
                  dbObject1?.CommitCreation(false);
                }
                finally
                {
                  if (dbObject1 is IProcess activity3)
                  {
                    ExtProperties extProperties = new ExtProperties((IDBObject) activity3, wfConsts.AttrAddInfoID);
                    extProperties.WriteBool("ImportingProcess", false, ExtPropertiesFlag.ThreadID);
                    extProperties.Save((IDBObject) activity3);
                  }
                }
              }
            }
            if (dbObject1 != null)
            {
              if (briefcaseObject1.LCStep != 0)
              {
                MapperObject mapperObject2 = this._map.Get(Domain.Steps, (long) briefcaseObject1.LCStep);
                if (mapperObject2 != null)
                {
                  IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(mapperObject2.Guid);
                  if (lcStep != null)
                    briefcaseObject1.LCStep = lcStep.LCStepID;
                }
                try
                {
                  dbObject1.LCStep = briefcaseObject1.LCStep;
                }
                catch (Exception ex)
                {
                  string str = briefcaseObject1.LCStep.ToString();
                  if (mapperObject2 != null)
                    str = $"{str} ({mapperObject2.Caption})";
                  this.Error($"Ошибка установки шага ЖЦ объекта \"{briefcaseObject1.Caption}\" в \"{str}\": {ex.Message}");
                }
              }
              if (dbObject1.ObjectModifyMode == ObjectModifyModes.Checkout)
                dbObject1 = dbObject1.CheckOut();
              string caption = briefcaseObject1.Caption;
              int num = 1;
              while (num <= 50)
              {
                try
                {
                  ++num;
                  dbObject1.Caption = caption;
                  break;
                }
                catch (ObjectAlreadyExists ex)
                {
                  caption = briefcaseObject1.Caption + num.ToString();
                  if (num > 50)
                    throw;
                }
              }
              this._objs[briefcaseObject1.ID] = dbObject1;
              this._objectIDs.Add(Math.Abs(dbObject1.ObjectID));
              importedObjectInfoList.Add(new SimpleBriefcase.ImportedObjectInfo(Math.Abs(dbObject1.ObjectID), caption, newType, briefcaseObject1.IsRoot));
              continue;
            }
            continue;
          case SimpleBriefcase.BriefcaseRelation _:
            briefcaseObject1 = (SimpleBriefcase.BriefcaseObject) null;
            briefcaseRelation = (SimpleBriefcase.BriefcaseRelation) briefcaseItem;
            dbObject1 = (IDBObject) null;
            int newRelType = this._map.GetNewRelType((long) briefcaseRelation.TypeID);
            flag2 = newRelType == 0;
            if (flag2)
            {
              this.Error($"Тип связей \"{briefcaseRelation.TypeID}\" не найден, связь  не создана");
              flag2 = true;
              continue;
            }
            MapperObject mapperObject3 = this._map.Get(Domain.Relations, briefcaseRelation.ID);
            if (mapperObject3 == null)
              throw new Exception($"Не найдена информация о связи RelationID=\"{briefcaseRelation.ID}\"");
            dbRelation = this.Session.GetRelation(mapperObject3.Guid, false);
            if (dbRelation == null)
            {
              IDBRelationCollection relationCollection = this.Session.GetRelationCollection(newRelType);
              if (relationCollection != null)
              {
                long newObjectId1 = this.GetNewObjectID(this.Session, briefcaseRelation.ProjObjectID);
                long newObjectId2 = this.GetNewObjectID(this.Session, briefcaseRelation.PartObjectID);
                if (newObjectId1 == 0L || newObjectId2 == 0L)
                {
                  this.Error($"Ошибка восстановления связи ({newRelType}) между объектами ({briefcaseRelation.ProjObjectID})->({briefcaseRelation.PartObjectID}): объект не найден");
                  continue;
                }
                dbRelation = relationCollection.Create(newObjectId1, newObjectId2);
                dbRelation.GUID = mapperObject3.Guid;
                continue;
              }
              continue;
            }
            continue;
          case SimpleBriefcase.BriefcaseAttribute _:
            if (!flag2 && (dbObject1 != null || dbRelation != null))
            {
              SimpleBriefcase.BriefcaseAttribute briefcaseAttribute = (SimpleBriefcase.BriefcaseAttribute) briefcaseItem;
              int newAttrType = this._map.GetNewAttrType(briefcaseAttribute.ID, this.Session);
              switch (newAttrType)
              {
                case -10000:
                case 0:
                  this.Error(briefcaseRelation != null ? $"Ошибка записи атрибута {briefcaseAttribute.ID} связи \"{briefcaseRelation}\": тип не найден" : $"Ошибка записи атрибута {briefcaseAttribute.ID} объекта \"{(briefcaseObject1 != null ? (object) briefcaseObject1.Caption : (object) "?")}\": тип не найден");
                  continue;
                default:
                  IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(newAttrType);
                  if (attributeType != null)
                  {
                    bool flag4 = attributeType.RealFieldType == FieldTypes.ftObjectLink;
                    SpecialRule specialRule;
                    if (!flag4 && this._specialRules.TryGetValue(newAttrType, out specialRule) && specialRule.HasFlag((Enum) SpecialRule.ObjectLinkAttribute))
                      flag4 = true;
                    if (flag4)
                    {
                      if (briefcaseObject1 == null)
                        throw new Exception("Ссылка на объект для связей не поддерживается.");
                      tupleList.Add(new Tuple<SimpleBriefcase.BriefcaseObject, SimpleBriefcase.BriefcaseAttribute>(briefcaseObject1, briefcaseAttribute));
                      continue;
                    }
                    string empty = string.Empty;
                    if (attributeType.FieldType == FieldTypes.ftShortBlob)
                    {
                      if (briefcaseAttribute.Values != null && briefcaseAttribute.Values.Length != 0 && briefcaseAttribute.Values[0] is string)
                        empty = briefcaseAttribute.Values[0].ToString();
                      briefcaseAttribute.Values = (object[]) null;
                    }
                    IDBAttribute dbAttribute = (IDBAttribute) null;
                    if (dbObject1 != null)
                      dbAttribute = dbObject1.Attributes.AddAttribute(newAttrType, false, briefcaseAttribute.Values);
                    else if (dbRelation != null)
                      dbAttribute = dbRelation.Attributes.AddAttribute(newAttrType, false, briefcaseAttribute.Values);
                    if (briefcaseAttribute.Blob != null && dbAttribute != null && dbAttribute is IBlobWriter blobWriter)
                    {
                      byte[] source = Convert.FromBase64String(briefcaseAttribute.Blob.Binary);
                      BlobInformation blobInfo = new BlobInformation(briefcaseAttribute.Blob.RealFileSize, (long) source.Length, DateTime.Now, string.Empty, briefcaseAttribute.Blob.Arc, empty);
                      if (blobWriter.OpenBlob(blobInfo, false))
                      {
                        blobWriter.WriteDataBlock(((IEnumerable<byte>) source).ToArray<byte>());
                        continue;
                      }
                      continue;
                    }
                    continue;
                  }
                  if (dbObject1 != null)
                  {
                    dbObject1.Attributes.AddAttribute(newAttrType, false, briefcaseAttribute.Values);
                    continue;
                  }
                  if (dbRelation != null)
                  {
                    dbRelation.Attributes.AddAttribute(newAttrType, false, briefcaseAttribute.Values);
                    continue;
                  }
                  continue;
              }
            }
            else
              continue;
          default:
            continue;
        }
      }
      foreach (Tuple<SimpleBriefcase.BriefcaseObject, SimpleBriefcase.BriefcaseAttribute> tuple in tupleList)
      {
        bool flag5 = false;
        string str = "";
        SimpleBriefcase.BriefcaseObject briefcaseObject2 = tuple.Item1;
        SimpleBriefcase.BriefcaseAttribute briefcaseAttribute = tuple.Item2;
        IDBObject dbObject2 = (IDBObject) null;
        if (!this._objs.TryGetValue(briefcaseObject2.ID, out dbObject2))
        {
          flag5 = true;
        }
        else
        {
          for (int index = 0; index < briefcaseAttribute.Values.Length; ++index)
          {
            long num = Convert.ToInt64(briefcaseAttribute.Values[index]);
            IDBObject dbObject3 = (IDBObject) null;
            MapperObject mapperObject = this._map.Get(Domain.Objects, num);
            if (!this._objs.TryGetValue(num, out dbObject3) && mapperObject != null)
              dbObject3 = this.Session.GetObject(mapperObject.Guid, false);
            SpecialRule specialRule;
            if (dbObject3 == null && mapperObject != null && this._specialRules.TryGetValue((int) briefcaseAttribute.ID, out specialRule) && specialRule.HasFlag((Enum) SpecialRule.CreateSurrogateObjects))
            {
              IDBObjectCollection objectCollection = this.Session.GetObjectCollection(wfConsts.IncompleteObjectType);
              if (objectCollection != null)
              {
                dbObject3 = objectCollection.Create(mapperObject.Guid);
                dbObject3.CommitCreation(false);
                dbObject3.Caption = mapperObject.Caption;
                this._objs[num] = dbObject3;
                this._objectIDs.Add(Math.Abs(dbObject3.ObjectID));
              }
            }
            if (dbObject3 != null)
            {
              num = Math.Abs(dbObject3.ObjectID);
              briefcaseAttribute.Values[index] = (object) num;
            }
            else
            {
              flag5 = true;
              if (str != "")
                str += ", ";
              str = mapperObject == null ? $"{str}\"ObjectID={num.ToString()}\"" : str + $"\"{mapperObject.Caption}\" (ObjectID={num})";
            }
          }
        }
        if (flag5)
        {
          this.Error($"Ошибка записи атрибута \"{this.GetCaption(Domain.Attributes, briefcaseAttribute.ID)}\" ({briefcaseAttribute.ID}) объекта \"{briefcaseObject2.Caption}\": ссылка на {str} не может быть восстановлена");
        }
        else
        {
          int newAttrType = this._map.GetNewAttrType(briefcaseAttribute.ID, this.Session);
          dbObject2.Attributes.AddAttribute(newAttrType, false, briefcaseAttribute.Values);
        }
      }
      SimpleBriefcase.BriefcaseImportedDelegate briefcaseImported = this.BriefcaseImported;
      if (briefcaseImported != null)
        briefcaseImported(this, this._objs);
      foreach (long objectId in this._objectIDs)
      {
        IDBObject objectActualCopy = this.Session.GetObjectActualCopy(objectId, false);
        if (objectActualCopy != null && objectActualCopy.CheckoutBy == this.Session.UserID)
          objectActualCopy.CheckIn();
      }
      return importedObjectInfoList;
    }
    catch
    {
      if (flag1)
        MiscFunx.ReloadVariablesCache(this.Session);
      throw;
    }
  }

  public static XmlSerializerNamespaces EmptyNamespace
  {
    get
    {
      if (SimpleBriefcase._emptyNamespace == null)
      {
        SimpleBriefcase._emptyNamespace = new XmlSerializerNamespaces();
        SimpleBriefcase._emptyNamespace.Add("", "");
      }
      return SimpleBriefcase._emptyNamespace;
    }
  }

  public void AddMapping(Domain domain, IEnumerable<long> ids)
  {
    foreach (long id in ids)
      this.AddMapping(domain, id);
  }

  public bool AddMapping(Domain domain, long id)
  {
    bool flag = true;
    switch (domain)
    {
      case Domain.ObjectTypes:
        IMSObjectType objectType = MetaDataHelper.GetObjectType((int) id);
        if (objectType != null)
        {
          this._map.Add(Domain.ObjectTypes, id, objectType.Guid, objectType.ObjectTypeName);
          break;
        }
        flag = false;
        break;
      case Domain.Objects:
        IDBObject objectActualCopy = this.Session.GetObjectActualCopy(id, false);
        if (objectActualCopy != null)
        {
          this._map.Add(domain, id, objectActualCopy.ObjectGUID, objectActualCopy.Caption);
          break;
        }
        flag = false;
        break;
      case Domain.Attributes:
        IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType((int) id);
        if (attributeType1 != null)
        {
          this._map.Add(domain, id, attributeType1.AttributeGuid, attributeType1.Name);
          break;
        }
        flag = false;
        break;
      case Domain.Variables:
        IMSAttributeType attributeType2 = MetaDataHelper.GetAttributeType((int) id);
        if (attributeType2 != null)
        {
          MapperVariable mapperVariable = new MapperVariable(attributeType2);
          this._map.Add(domain, id, (MapperObject) mapperVariable);
          break;
        }
        flag = false;
        break;
      case Domain.Levels:
        IMSLifeCycleLevel lcLevel = MetaDataHelper.GetLCLevel((int) id);
        if (lcLevel != null)
        {
          this._map.Add(domain, id, lcLevel.Guid, lcLevel.Name);
          break;
        }
        flag = false;
        break;
      case Domain.Steps:
        IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep((int) id);
        if (lcStep != null)
        {
          this._map.Add(domain, id, lcStep.Guid, lcStep.Name);
          break;
        }
        flag = false;
        break;
      default:
        throw new ArgumentException();
    }
    if (!flag)
      this.Error($"Ошибка экспорта данных объекта ({domain}) ID={id}");
    return flag;
  }

  public string GetCaption(Domain domain, long id) => this._map.Get(domain, id)?.Caption;

  public event Func<string, SimpleBriefcase, bool> ImportPrompt;

  public event System.Func<SimpleBriefcase, bool> CreateVariablesPrompt;

  /// <summary>
  /// Создавать ли при импорте типы атрибутов для переменных маршрутизатора.
  /// </summary>
  public bool CreateVariables
  {
    get
    {
      if (!this._createVariables.HasValue)
      {
        System.Func<SimpleBriefcase, bool> createVariablesPrompt = this.CreateVariablesPrompt;
        if (createVariablesPrompt == null)
          return false;
        this._createVariables = new bool?(createVariablesPrompt(this));
      }
      return this._createVariables.Value;
    }
    set => this._createVariables = new bool?(value);
  }

  public SimpleBriefcase.BriefcaseObject RootObject
  {
    get
    {
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index] is SimpleBriefcase.BriefcaseObject)
        {
          SimpleBriefcase.BriefcaseObject rootObject = this.Items[index] as SimpleBriefcase.BriefcaseObject;
          if (rootObject.IsRoot)
            return rootObject;
        }
      }
      return (SimpleBriefcase.BriefcaseObject) null;
    }
  }

  public string RootObjectTypeName
  {
    get
    {
      string rootObjectTypeName = "";
      SimpleBriefcase.BriefcaseObject rootObject = this.RootObject;
      if (rootObject != null)
        rootObjectTypeName = MetaDataHelper.GetObjectName(this._map.GetNewType((long) rootObject.TypeID));
      if (rootObjectTypeName == "")
        rootObjectTypeName = "Объект";
      return rootObjectTypeName;
    }
  }

  public delegate void ObjectExportingDelegate(SimpleBriefcase brief, IDBObject obj);

  public delegate bool AttributeExportingDelegate(
    SimpleBriefcase brief,
    IDBAttributable obj,
    SimpleBriefcase.BriefcaseAttribute attr,
    MemoryStream ms);

  public delegate void BriefcaseObjectFound(SimpleBriefcase brief, ref IDBObject obj);

  public delegate void BriefcaseImportedDelegate(
    SimpleBriefcase brief,
    Dictionary<long, IDBObject> objs);

  [XmlInclude(typeof (SimpleBriefcase.BriefcaseObject))]
  [XmlInclude(typeof (SimpleBriefcase.BriefcaseAttribute))]
  [XmlInclude(typeof (SimpleBriefcase.BriefcaseRelation))]
  [Serializable]
  public class BriefcaseItem
  {
  }

  public class BriefcaseItemList : List<SimpleBriefcase.BriefcaseItem>
  {
  }

  [Serializable]
  public class BriefcaseObject : SimpleBriefcase.BriefcaseItem
  {
    public long ID;
    public int TypeID;
    public string Caption;
    public int LCStep;
    public bool IsRoot;

    public BriefcaseObject()
    {
    }

    public BriefcaseObject(long id, int typeID, string caption, int lcstep, bool isRoot)
    {
      this.ID = Math.Abs(id);
      this.TypeID = typeID;
      this.Caption = caption;
      this.LCStep = lcstep;
      this.IsRoot = isRoot;
    }

    public bool ShouldSerializeIsRoot() => this.IsRoot;
  }

  public class BlobInfo
  {
    public ArcMethods Arc;
    public long RealFileSize;
    public string Binary = "";
  }

  [Serializable]
  public class BriefcaseAttribute : SimpleBriefcase.BriefcaseItem
  {
    public long ID;
    public object[] Values;
    public SimpleBriefcase.BlobInfo Blob;

    public object Value
    {
      get => this.Values.Length == 0 ? (object) null : this.Values[0];
      set => this.Values = new object[1]{ value };
    }

    public BriefcaseAttribute()
    {
    }

    public BriefcaseAttribute(long id, object[] values, SimpleBriefcase.BlobInfo blob)
    {
      this.ID = id;
      this.Values = values;
      this.Blob = blob;
    }

    public bool ShouldSerializeBlob() => this.Blob != null;

    public bool ShouldSerializeValues() => this.Values.Length > 1;

    public bool ShouldSerializeValue() => !this.ShouldSerializeValues();
  }

  [Serializable]
  public class BriefcaseRelation : SimpleBriefcase.BriefcaseItem
  {
    public long ID;
    public int TypeID;
    public long ProjObjectID;
    public long PartObjectID;

    public BriefcaseRelation()
    {
    }

    public BriefcaseRelation(long id, int typeID, long projObjectID, long partObjectID)
    {
      this.ID = id;
      this.TypeID = typeID;
      this.ProjObjectID = projObjectID;
      this.PartObjectID = partObjectID;
    }

    public override string ToString() => $"{this.ProjObjectID} -> {this.PartObjectID}";
  }

  [Serializable]
  public class Mapper
  {
    private SerializableDictionary<Domain, SerializableDictionary<long, MapperObject>> _data = new SerializableDictionary<Domain, SerializableDictionary<long, MapperObject>>();

    public SerializableDictionary<Domain, SerializableDictionary<long, MapperObject>> Items
    {
      get => this._data;
      set => this._data = value;
    }

    public void Add(Domain domain, long id, MapperObject obj)
    {
      SerializableDictionary<long, MapperObject> serializableDictionary = (SerializableDictionary<long, MapperObject>) null;
      if (!this._data.TryGetValue(domain, out serializableDictionary))
      {
        serializableDictionary = new SerializableDictionary<long, MapperObject>();
        this._data.Add(domain, serializableDictionary);
      }
      if (serializableDictionary.ContainsKey(id))
        return;
      serializableDictionary.Add(id, obj);
    }

    public void Add(Domain domain, long id, Guid guid, string caption)
    {
      if (domain == Domain.Objects && id < 0L)
        id = Math.Abs(id);
      MapperObject mapperObject = new MapperObject(guid, caption);
      this.Add(domain, id, mapperObject);
    }

    public MapperObject Get(Domain domain, long id)
    {
      SerializableDictionary<long, MapperObject> serializableDictionary = (SerializableDictionary<long, MapperObject>) null;
      if (this._data.TryGetValue(domain, out serializableDictionary))
      {
        MapperObject mapperObject = (MapperObject) null;
        if (serializableDictionary.TryGetValue(id, out mapperObject))
          return mapperObject;
      }
      return (MapperObject) null;
    }

    public MapperObject Get(Domain domain, Guid guid)
    {
      SerializableDictionary<long, MapperObject> serializableDictionary = (SerializableDictionary<long, MapperObject>) null;
      if (this._data.TryGetValue(domain, out serializableDictionary))
      {
        foreach (KeyValuePair<long, MapperObject> keyValuePair in (Dictionary<long, MapperObject>) serializableDictionary)
        {
          if (keyValuePair.Value.Guid == guid)
            return keyValuePair.Value;
        }
      }
      return (MapperObject) null;
    }

    /// <summary>
    /// Возвращает числовой идентификатор типа в текущей базе, соответствующий типу из портфеля
    /// </summary>
    public int GetNewType(long type)
    {
      MapperObject mapperObject = this.Get(Domain.ObjectTypes, type);
      return mapperObject != null ? MetaDataHelper.GetObjectTypeID(mapperObject.Guid) : 0;
    }

    /// <summary>
    /// Возвращает числовой идентификатор типа атрибута в текущей базе, соответствующий типу из портфеля
    /// </summary>
    public int GetNewAttrType(long type, IUserSession session)
    {
      MapperObject mapperObject = this.Get(Domain.Attributes, type);
      if (mapperObject == null)
        return 0;
      int attributeId = MetaDataHelper.GetAttributeID((object) mapperObject.Guid);
      switch (attributeId)
      {
        case -10000:
        case 0:
          IDBAttributeType attributeType = session.GetAttributeType(mapperObject.Guid);
          if (attributeType != null)
          {
            attributeId = attributeType.AttributeID;
            break;
          }
          break;
      }
      return attributeId;
    }

    /// <summary>
    /// Возвращает числовой идентификатор типа связи в текущей базе, соответствующий типу из портфеля
    /// </summary>
    public int GetNewRelType(long type)
    {
      MapperObject mapperObject = this.Get(Domain.RelTypes, type);
      return mapperObject != null ? MetaDataHelper.GetRelationTypeID(mapperObject.Guid) : 0;
    }

    public void Clear() => this._data.Clear();
  }

  public class ImportedObjectInfo
  {
    public readonly long ObjectID;
    public readonly string Caption;
    public readonly int ObjectTypeID;
    public readonly bool IsRoot;

    public ImportedObjectInfo(long objectID, string caption, int objectTypeID, bool isRoot)
    {
      this.ObjectID = objectID;
      this.Caption = caption;
      this.ObjectTypeID = objectTypeID;
      this.IsRoot = isRoot;
    }
  }
}
