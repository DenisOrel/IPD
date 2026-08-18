// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public abstract class DBAttributeCollection : DBSessionable, IDBAttributeCollection
{
  private List<DBAttribute> _attrList;
  private int _ObjectType;
  private long _ObjectID;
  private List<int> _DeltaList;
  internal static IDBAttributeService AttributeCreatorService;
  protected IDBAttributable _Parent;
  private bool _AlreadyAdd;
  public bool ValidatingOn = true;
  public int _AssignMode;
  private bool _AllAttributesMode;
  internal UpdateViewsHelper _UpdateViews;
  internal List<int> _SkipAttributesList;
  private bool _CheckExistMode;

  public DBAttributeCollection(
    UserSession uSession,
    long objectID,
    int objectType,
    IDBAttributable parent)
    : base(uSession)
  {
    this._Parent = parent;
    this._ObjectID = objectID;
    this._ObjectType = objectType;
  }

  internal bool IsAttrListLoaded => this._attrList != null;

  public bool AllAttributesMode
  {
    get => this._AllAttributesMode;
    set
    {
      if (this._AllAttributesMode == value)
        return;
      if (value)
      {
        if (this._attrList == null)
          this.InitAdditionalAttributes(this.GetAttributesDataTable(this.ObjectID));
        this.InitSystemAttributes();
      }
      else
        this.ExcludeSystemAttributes();
      this._AllAttributesMode = value;
    }
  }

  private void ExcludeSystemAttributes()
  {
    for (int index = this._AttributesList.Count - 1; index > 0; --index)
    {
      if (this._AttributesList[index].AttributeID < 0)
        this._AttributesList.RemoveAt(index);
    }
  }

  protected abstract void InitSystemAttributes();

  private void InitAdditionalAttributes(DataTable attributesDataTable)
  {
    this._attrList = new List<DBAttribute>();
    IDBAttributeService service = ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService;
    int num = 0;
    for (int index = 0; index < attributesDataTable.Rows.Count; ++index)
    {
      if (num != Convert.ToInt32(attributesDataTable.Rows[index]["F_ATTRIBUTE_ID"]))
      {
        num = Convert.ToInt32(attributesDataTable.Rows[index]["F_ATTRIBUTE_ID"]);
        if (service.CreateAttribute((IUserSession) this.UserSession, attributesDataTable, index, false, this._Parent) is DBAttribute attribute)
        {
          this._attrList.Add(attribute);
          attribute._Attributes = (IDBAttributeCollection) this;
        }
      }
    }
  }

  protected List<DBAttribute> _AttributesList
  {
    get
    {
      if (this._attrList == null)
        this.InitAdditionalAttributes(this.GetAttributesDataTable(this.ObjectID));
      return this._attrList;
    }
  }

  internal void AttrsListClear() => this._attrList.Clear();

  private DataTable GetAttributesDataTable(long objectID)
  {
    return this.UserSession.DataManager.ExecuteDataTable($"SELECT * FROM {this.AttributesTableName} WHERE {this.AttributesKeyName} = :objrelID ORDER BY F_ATTRIBUTE_ID, F_INLIST_ID", this.UserSession.DataManager.Parameter("objrelID", (object) objectID));
  }

  public int AssignMode => this._AssignMode;

  internal bool RemoveAttribute(int attributeID)
  {
    for (int index = 0; index < this._AttributesList.Count; ++index)
    {
      if (this._AttributesList[index].AttributeID == attributeID)
      {
        this._AttributesList.RemoveAt(index);
        return true;
      }
    }
    return false;
  }

  protected virtual string AttributesTableName => "";

  protected virtual string AttributesKeyName => "";

  public IDBAttribute this[int AttrIndex] => (IDBAttribute) this._AttributesList[AttrIndex];

  public int Count => this._AttributesList.Count;

  public int ObjectType
  {
    get => this._ObjectType;
    set
    {
      if (this._ObjectType == value)
        return;
      this._ObjectType = value;
    }
  }

  public override long ObjectID => this._ObjectID;

  public IDBAttribute FindByName(string AttributeName)
  {
    foreach (DBAttribute attributes in this._AttributesList)
    {
      if (attributes.Name.ToUpper() == AttributeName.ToUpper())
        return (IDBAttribute) attributes;
    }
    return (IDBAttribute) null;
  }

  public IDBAttribute FindByID(int AttributeID)
  {
    foreach (DBAttribute attributes in this._AttributesList)
    {
      if (attributes.AttributeID == AttributeID)
        return (IDBAttribute) attributes;
    }
    return (IDBAttribute) null;
  }

  public IDBAttribute FindByGUID(Guid AttributeGUID)
  {
    foreach (DBAttribute attributes in this._AttributesList)
    {
      if (attributes.GUID == AttributeGUID)
        return (IDBAttribute) attributes;
    }
    return (IDBAttribute) null;
  }

  public IDBAttribute FindByAlias(string attributeAlias)
  {
    foreach (DBAttribute attributes in this._AttributesList)
    {
      if (attributes.AttributeType.Alias == attributeAlias)
        return (IDBAttribute) attributes;
    }
    return (IDBAttribute) null;
  }

  public virtual void Assign(IDBAttributeCollection sourceAttributes, int assignMode)
  {
    (this._Parent as DBAttributable).SetAttributesState(Consts.AssignValuesMode, sourceAttributes);
    this._AssignMode = assignMode;
    try
    {
      this.UserSession.StartTransaction();
      try
      {
        for (int AttrIndex = 0; AttrIndex < sourceAttributes.Count; ++AttrIndex)
        {
          IDBAttribute sourceAttribute = sourceAttributes[AttrIndex];
          if ((this._SkipAttributesList == null || this._SkipAttributesList.IndexOf(sourceAttribute.AttributeID) < 0) && sourceAttribute.AttributeID >= 0 && ((assignMode & 1024 /*0x0400*/) != 1024 /*0x0400*/ || (sourceAttribute.AttributeType.Options & AttributeOptions.DontCopyVersionValue) != AttributeOptions.DontCopyVersionValue) && ((sourceAttribute.AttributeType.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.None || (sourceAttribute.AttributeType.Options & AttributeOptions.DontCopyVersionValue) == AttributeOptions.None && (assignMode & 1024 /*0x0400*/) == 1024 /*0x0400*/ || (assignMode & Consts.CheckInMode) == Consts.CheckInMode || (assignMode & Consts.CheckOutMode) == Consts.CheckOutMode))
          {
            if (DBAttributeType.CanSkipInit(sourceAttribute.AttributeType.AttributeType))
            {
              if (sourceAttribute.TemporaryAttribute)
              {
                this.AddTemporaryAttribute(sourceAttribute.AttributeID, false, sourceAttribute.Values);
              }
              else
              {
                IDBAttributeType attributeType = this._Parent.GetAttributeType(sourceAttribute.AttributeID);
                if (attributeType.Computed == ComputeValueModes.NotComputableValue)
                  this.AddAttribute(sourceAttribute.AttributeID, false, this.ValidatingOn, sourceAttribute.Values);
                else
                  this.AddAttribute(sourceAttribute.AttributeID, false, this.ValidatingOn);
                IDBAttribute byId = this.FindByID(sourceAttribute.AttributeID);
                if (attributeType.Computed != ComputeValueModes.JITValue)
                {
                  if (byId != null)
                    (byId as DBAttribute).Compute(true);
                  else
                    this.AddAttribute(sourceAttribute.AttributeID, false, this.ValidatingOn);
                }
              }
            }
            else
            {
              DBAttribute dbAttribute = !sourceAttribute.TemporaryAttribute ? this.AddAttribute(sourceAttribute.AttributeID, false, this.ValidatingOn) as DBAttribute : this.AddTemporaryAttribute(sourceAttribute.AttributeID, false) as DBAttribute;
              if (dbAttribute.AttributeType.Computed == ComputeValueModes.NotComputableValue)
              {
                dbAttribute.ValidatingOn = this.ValidatingOn;
                dbAttribute.Assign(sourceAttribute);
              }
            }
          }
        }
        if ((assignMode & Consts.DeleteInstances) == Consts.DeleteInstances)
        {
          for (int AttrIndex1 = this.Count - 1; AttrIndex1 >= 0; --AttrIndex1)
          {
            if (this[AttrIndex1].AttributeID >= 0)
            {
              bool flag = false;
              for (int AttrIndex2 = 0; AttrIndex2 < sourceAttributes.Count; ++AttrIndex2)
              {
                if (sourceAttributes[AttrIndex2].AttributeID == this[AttrIndex1].AttributeID)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                (this[AttrIndex1] as DBAttribute).ValidatingOn = this.ValidatingOn;
                this[AttrIndex1].Delete((long) Consts.PurgeMode);
              }
            }
          }
        }
        (this._Parent as DBAttributable).CommitComputedValues();
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    finally
    {
      this._AssignMode = 0;
      (this._Parent as DBAttributable).ClearAttributesState(Consts.AssignValuesMode);
    }
  }

  public void Assign(IDBAttributeCollection sourceAttributes) => this.Assign(sourceAttributes, 0);

  public virtual int[] AssignPossibleAttributes(
    IDBAttributeCollection sourceAttributes,
    int assignMode)
  {
    (this._Parent as DBAttributable).SetAttributesState(Consts.AssignValuesMode, sourceAttributes);
    this._AssignMode = assignMode;
    try
    {
      List<int> intList = (List<int>) null;
      IDBAttributableType parentType = this.ParentType;
      this.UserSession.StartTransaction();
      try
      {
        for (int AttrIndex = 0; AttrIndex < sourceAttributes.Count; ++AttrIndex)
        {
          IDBAttribute sourceAttribute = sourceAttributes[AttrIndex];
          if ((this._SkipAttributesList == null || this._SkipAttributesList.IndexOf(sourceAttribute.AttributeID) < 0) && sourceAttribute.AttributeID >= 0)
          {
            if (!parentType.AnyAttributes && parentType.Attributes.GetAttributeByID(sourceAttribute.AttributeID, false) == null)
            {
              if (intList == null)
                intList = new List<int>();
              intList.Add(sourceAttribute.AttributeID);
            }
            else if ((sourceAttribute.AttributeType.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.None)
            {
              if (DBAttributeType.CanSkipInit(sourceAttribute.AttributeType.AttributeType))
              {
                if (sourceAttribute.TemporaryAttribute)
                  this.AddTemporaryAttribute(sourceAttribute.AttributeID, false, sourceAttribute.Values);
                else if (this._Parent.GetAttributeType(sourceAttribute.AttributeID).Computed == ComputeValueModes.NotComputableValue)
                  this.AddAttribute(sourceAttribute.AttributeID, false, this.ValidatingOn, sourceAttribute.Values);
                else
                  this.AddAttribute(sourceAttribute.AttributeID, false, this.ValidatingOn);
              }
              else
              {
                DBAttribute dbAttribute = !sourceAttribute.TemporaryAttribute ? this.AddAttribute(sourceAttribute.AttributeID, false, this.ValidatingOn) as DBAttribute : this.AddTemporaryAttribute(sourceAttribute.AttributeID, false) as DBAttribute;
                if (dbAttribute.AttributeType.Computed == ComputeValueModes.NotComputableValue)
                {
                  dbAttribute.ValidatingOn = this.ValidatingOn;
                  dbAttribute.Assign(sourceAttribute);
                }
              }
            }
          }
        }
        (this._Parent as DBAttributable).CommitComputedValues();
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
      if (intList == null)
        return new int[0];
      return intList.ToArray();
    }
    finally
    {
      this._AssignMode = 0;
      (this._Parent as DBAttributable).ClearAttributesState(Consts.AssignValuesMode);
    }
  }

  protected abstract IDBAttributableType ParentType { get; }

  public object CheckAddAtribute(int attributeID, out bool readOnly)
  {
    if (this.UserSession.DataManager.InTransaction)
      throw new KernelExceptionID(sc_12387.ssp_appserver_12388(2073096469), (object) nameof (CheckAddAtribute));
    this.UserSession.StartTransaction();
    try
    {
      IDBAttribute dbAttribute = this.AddAttribute(attributeID, true, true);
      object obj = dbAttribute.Value;
      readOnly = dbAttribute.ReadOnly;
      this.UserSession.Rollback();
      return obj;
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  protected abstract IDBAttribute DoAddAttribute(int attributeID, bool checkEnabled);

  protected abstract IDBAttribute DoAddTemporaryAttribute(int attributeID);

  public bool CheckExistMode
  {
    get => this._CheckExistMode;
    set => this._CheckExistMode = value;
  }

  internal abstract string[] GetUpdateTables(int attrID);

  internal bool QuickAddAttributes(
    long newID,
    bool copyOptimViews,
    bool createVersionMode,
    bool prototypeMode)
  {
    if (this.Count < 3 || this.AllAttributesMode)
      return false;
    bool flag1 = true;
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter(nameof (newID), (object) newID);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter("objID", (object) this.ObjectID);
    bool flag2 = false;
    bool flag3 = false;
    StringBuilder stringBuilder = new StringBuilder();
    for (int AttrIndex = 0; AttrIndex < this.Count; ++AttrIndex)
    {
      if (this[AttrIndex].DataType == FieldTypes.ftObjectLink)
        flag2 = true;
      if (this[AttrIndex].DataType == FieldTypes.ftObjectLinkByID)
        flag3 = true;
      if (!DBAttributeType.CanQuickCopy(this[AttrIndex].DataType))
        stringBuilder.Append(this[AttrIndex].AttributeID.ToString() + ",");
      else if (createVersionMode && (this[AttrIndex].AttributeType.Options & AttributeOptions.DontCopyVersionValue) == AttributeOptions.DontCopyVersionValue)
        stringBuilder.Append(this[AttrIndex].AttributeID.ToString() + ",");
      else if (prototypeMode && (this[AttrIndex].AttributeType.Options & AttributeOptions.DontCopyPrototypeValue) == AttributeOptions.DontCopyPrototypeValue)
        stringBuilder.Append(this[AttrIndex].AttributeID.ToString() + ",");
      else if (!this[AttrIndex].TemporaryAttribute & copyOptimViews)
      {
        string[] updateTables = this.GetUpdateTables(this[AttrIndex].AttributeID);
        if (updateTables != null && updateTables.Length != 0)
        {
          string[] fieldNames = this[AttrIndex].AttributeType.FieldNames;
          if (dataManager.DataProvider.Name == "Linter")
          {
            for (int index1 = 0; index1 < updateTables.Length; ++index1)
            {
              for (int index2 = 0; index2 < fieldNames.Length; ++index2)
                dataManager.ExecuteNonQuery(string.Format("UPDATE {0} DST SET {1} = (SELECT SRC.{1} FROM {0} SRC WHERE SRC.{2} = :objID) WHERE DST.{2} = :newID", (object) updateTables[index1], (object) fieldNames[index2], (object) this.AttributesKeyName), dbDataParameter1, dbDataParameter2);
            }
          }
          else
          {
            for (int index = 0; index < updateTables.Length; ++index)
              dictionary[updateTables[index]] = !dictionary.ContainsKey(updateTables[index]) ? this.GetStringFromArray(fieldNames) : $"{dictionary[updateTables[index]]},{this.GetStringFromArray(fieldNames)}";
          }
        }
      }
    }
    if (stringBuilder.Length > 0)
    {
      stringBuilder.Insert(0, " AND F_ATTRIBUTE_ID NOT IN (");
      stringBuilder[stringBuilder.Length - 1] = ')';
      flag1 = false;
    }
    string str = stringBuilder.ToString();
    dataManager.ExecuteNonQuery(string.Format("INSERT INTO {0} (F_ATTRIBUTE_ID, {2}, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) SELECT F_ATTRIBUTE_ID, :newID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE FROM {0} WHERE {2} = :objID{1}", (object) this.AttributesTableName, (object) str, (object) this.AttributesKeyName), dbDataParameter2, dbDataParameter1);
    if (flag2 && !createVersionMode)
      dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) SELECT :newID, A.F_ATTRIBUTE_ID, A.F_INLIST_ID, A.F_TOOBJECT_ID FROM IMS_OBJECT_LINKS A WHERE A.F_OBJECT_ID = :objID" + str, dbDataParameter2, dbDataParameter1);
    if (flag3 && !createVersionMode)
      dataManager.ExecuteNonQuery("INSERT INTO IMS_ID_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TO_ID) SELECT :newID, A.F_ATTRIBUTE_ID, A.F_INLIST_ID, A.F_TO_ID FROM IMS_ID_LINKS A WHERE A.F_OBJECT_ID = :objID" + str, dbDataParameter2, dbDataParameter1);
    if (copyOptimViews && dictionary.Count > 0)
    {
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
      {
        if (dataManager.DataProvider.Name == "Oracle" || dataManager.DataProvider.Name == "PostgreSQL")
          dataManager.ExecuteNonQuery(string.Format("UPDATE {0} DST SET ({1}) = (SELECT {1} FROM {0} SRC WHERE SRC.{2} = :objID) WHERE DST.{2} = :newID", (object) keyValuePair.Key, (object) keyValuePair.Value, (object) this.AttributesKeyName), dbDataParameter1, dbDataParameter2);
        else if (dataManager.DataProvider.Name == "Sql")
          dataManager.ExecuteNonQuery(string.Format("UPDATE {0} SET {1} FROM {0}, {0} SRC WHERE {0}.{2} = :newID AND SRC.{2} = :objID", (object) keyValuePair.Key, (object) keyValuePair.Value, (object) this.AttributesKeyName), dbDataParameter1, dbDataParameter2);
      }
    }
    return flag1;
  }

  protected string GetStringFromArray(string[] flds)
  {
    string stringFromArray;
    if (this.UserSession.DataManager.DataProvider.Name != "Sql")
    {
      stringFromArray = flds[0];
      for (int index = 1; index < flds.Length; ++index)
        stringFromArray = $"{stringFromArray},{flds[index]}";
    }
    else
    {
      stringFromArray = $"{flds[0]} = SRC.{flds[0]}";
      for (int index = 1; index < flds.Length; ++index)
        stringFromArray = $"{stringFromArray},{flds[index]} = SRC.{flds[index]}";
    }
    return stringFromArray;
  }

  public IDBAttribute AddAttribute(
    int attributeID,
    bool failIfExists,
    bool checkEnabled,
    object[] initValues)
  {
    if (attributeID < 0)
      throw new KernelExceptionID(sc_12387.ssp_appserver_12389(2139639174));
    IDBAttribute dbAttribute1;
    if (this.IsAttrListLoaded)
    {
      dbAttribute1 = this.FindByID(attributeID);
    }
    else
    {
      dbAttribute1 = !this._AlreadyAdd ? this._Parent.GetAttributeByID(attributeID) : this.FindByID(attributeID);
      this._AlreadyAdd = true;
    }
    if (dbAttribute1 != null)
    {
      if (failIfExists)
      {
        IDBAttributeType attributeType = this.UserSession.GetAttributeType(attributeID);
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12387.ssp_appserver_12390()), (object) attributeType.Name));
      }
      if (initValues != null)
      {
        bool validatingOn = (dbAttribute1 as DBAttribute).ValidatingOn;
        (dbAttribute1 as DBAttribute).ValidatingOn = this.ValidatingOn;
        try
        {
          dbAttribute1.Values = initValues;
        }
        finally
        {
          (dbAttribute1 as DBAttribute).ValidatingOn = validatingOn;
        }
      }
      return dbAttribute1;
    }
    this.UserSession.StartTransaction();
    try
    {
      if ((this.AssignMode & Consts.CheckOutMode) == 0)
        (this._Parent as DBAttributable).BeforeAddAttribute(attributeID, initValues);
      DBAttribute dbAttribute2 = this.DoAddAttribute(attributeID, checkEnabled) as DBAttribute;
      dbAttribute2.ValidatingOn = false;
      dbAttribute2._TypeID = this._ObjectType;
      if (this.IsAttrListLoaded)
        this._attrList.Add(dbAttribute2);
      dbAttribute2._Attributes = (IDBAttributeCollection) this;
      if (initValues != null)
      {
        if (!DBAttributeType.CanSkipInit(dbAttribute2.AttributeType.AttributeType))
        {
          dbAttribute2.DoAfterCreate();
          dbAttribute2.Values = initValues;
        }
        else
        {
          dbAttribute2.Values = initValues;
          (this.EventHelper as EventLogHelper).OnCreateAttribute((IDBAttribute) dbAttribute2, (IUserSession) this.UserSession);
        }
      }
      else if ((this._AssignMode & 4096 /*0x1000*/) == 4096 /*0x1000*/)
      {
        try
        {
          dbAttribute2.DoAfterCreate();
        }
        catch (ObjectAlreadyExists ex)
        {
          this.UserSession.EventLogHelper.AddToTrace(ex.Message, Consts.traceAlways, string.Empty);
        }
      }
      else
        dbAttribute2.DoAfterCreate();
      dbAttribute2.ValidatingOn = true;
      if ((this.AssignMode & Consts.CheckOutMode) == 0)
        (this._Parent as DBAttributable).AfterAddAttribute((IDBAttribute) dbAttribute2);
      this.UserSession.Commit();
      return (IDBAttribute) dbAttribute2;
    }
    catch
    {
      this.UserSession.Rollback();
      throw;
    }
  }

  public IDBAttribute AddAttribute(int attributeID, bool failIfExists, bool checkEnabled)
  {
    return this.AddAttribute(attributeID, failIfExists, checkEnabled, (object[]) null);
  }

  public IDBAttribute AddAttribute(int attributeID, bool failIfExists)
  {
    return this.AddAttribute(attributeID, failIfExists, true, (object[]) null);
  }

  public IDBAttribute AddAttribute(int attributeID, bool failIfExists, object[] initValues)
  {
    return this.AddAttribute(attributeID, failIfExists, true, initValues);
  }

  public IDBAttribute AddAttribute(Guid attributeGuid, bool failIfExists)
  {
    return this.AddAttribute(MetaDataHelper.GetAttributeID((object) attributeGuid), failIfExists, true, (object[]) null);
  }

  public IDBAttribute AddAttribute(Guid attributeGuid, bool failIfExists, object[] initValues)
  {
    return this.AddAttribute(MetaDataHelper.GetAttributeID((object) attributeGuid), failIfExists, true, initValues);
  }

  public IDBAttribute AddTemporaryAttribute(
    int attributeID,
    bool failIfExists,
    object[] initValues)
  {
    if (this.FindByID(attributeID) is DBAttribute byId)
    {
      if (failIfExists)
      {
        IDBAttributeType attributeType = this.UserSession.GetAttributeType(attributeID);
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12387.ssp_appserver_12391()), (object) attributeType.Name));
      }
      if (initValues != null)
        byId.Values = initValues;
      return (IDBAttribute) byId;
    }
    DBAttribute sender = this.DoAddTemporaryAttribute(attributeID) as DBAttribute;
    sender._TypeID = this._ObjectType;
    sender._Attributes = (IDBAttributeCollection) this;
    this._AttributesList.Add(sender);
    if (initValues != null)
    {
      sender.Values = initValues;
      (this.EventHelper as EventLogHelper).OnCreateAttribute((IDBAttribute) sender, (IUserSession) this.UserSession);
    }
    else
      sender.DoAfterCreate();
    return (IDBAttribute) sender;
  }

  public IDBAttribute AddTemporaryAttribute(int attributeID, bool failIfExists)
  {
    return this.AddTemporaryAttribute(attributeID, failIfExists, (object[]) null);
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this._AttributesList.GetEnumerator();

  internal void Purge()
  {
    foreach (DBAttribute attributes in this._AttributesList)
    {
      if (attributes.AttributeID > 0)
        attributes.Purge(true);
    }
    this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this.AttributesTableName} WHERE {this.AttributesKeyName} = :objID", this.UserSession.DataManager.Parameter("objID", (object) this._ObjectID));
    this._AttributesList.Clear();
  }

  public AttributeValues[] GetDeltaValues(GetAttributeValuesModes modes, bool defaultWriteMode)
  {
    if (this._DeltaList == null)
      return (AttributeValues[]) null;
    AttributeValues[] deltaValues = new AttributeValues[this._DeltaList.Count];
    DataTable dataTable1 = (DataTable) null;
    DataTable dataTable2 = (DataTable) null;
    bool flag = false;
    for (int index = 0; index < deltaValues.Length; ++index)
    {
      int delta = this._DeltaList[index];
      if (delta > 0)
      {
        IDBAttribute byId = this.FindByID(delta);
        if (byId == null)
          throw new KernelExceptionID(sc_12387.ssp_appserver_12392(2031288853), (object) delta);
        deltaValues[index] = new AttributeValues(byId.AttributeID, byId.DataType, byId.AttributeType.MultipleValued, byId.AttributeType.Computed);
        deltaValues[index].Values = byId.Values;
        if ((modes & GetAttributeValuesModes.IncludeAlias) == GetAttributeValuesModes.IncludeAlias)
          deltaValues[index].AttributeAlias = byId.AttributeType.Alias;
        if ((modes & GetAttributeValuesModes.IncludeGuid) == GetAttributeValuesModes.IncludeGuid)
          deltaValues[index].AttributeGuid = (byId as IDBGuid).GUID;
        if ((modes & GetAttributeValuesModes.IncludeName) == GetAttributeValuesModes.IncludeName)
          deltaValues[index].AttributeName = byId.Name;
        if ((modes & GetAttributeValuesModes.CheckWriteAccess) == GetAttributeValuesModes.CheckWriteAccess)
          deltaValues[index].ReadOnly = !(byId as DBAttribute).CheckAccess(ActionType.Write);
        if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions)
          deltaValues[index].Descriptions = (object[]) byId.Descriptions;
        if ((modes & GetAttributeValuesModes.CheckWriteAccess) == GetAttributeValuesModes.CheckWriteAccess)
        {
          deltaValues[index].ReadOnly = flag | byId.AttributeType.Computed != 0;
          deltaValues[index].ReadOnly |= !(byId as DBAttribute).CheckAccess(ActionType.Write, defaultWriteMode, false);
        }
        if ((modes & GetAttributeValuesModes.IncludeGroupName) == GetAttributeValuesModes.IncludeGroupName)
        {
          if (dataTable1 == null)
          {
            dataTable1 = this.UserSession.DBCache.GetTable("IMS_ATTR_IN_GROUPS");
            dataTable2 = this.UserSession.DBCache.GetTable("IMS_ATTR_GROUPS");
          }
          DataRow[] dataRowArray = dataTable1.Select("F_ATTRIBUTE_ID = " + byId.AttributeID.ToString());
          if (dataRowArray.Length != 0)
          {
            DataRow dataRow = dataTable2.Rows.Find(dataRowArray[0][0]);
            deltaValues[index].GroupName = dataRow["F_GROUP_NAME"].ToString();
          }
        }
      }
      else
      {
        deltaValues[index] = new AttributeValues(delta);
        deltaValues[index].Values = this._Parent.GetValuesByID(delta, true);
        deltaValues[index].AttributeType = FieldTypes.ftSystem;
        deltaValues[index].MultipleValued = MultiValueModes.SingleValue;
        if ((modes & GetAttributeValuesModes.IncludeDescriptions) == GetAttributeValuesModes.IncludeDescriptions && deltaValues[index].Values != null && deltaValues[index].Values.Length != 0)
          deltaValues[index].Descriptions = deltaValues[index].Values;
        if ((modes & GetAttributeValuesModes.IncludeGroupName) == GetAttributeValuesModes.IncludeGroupName)
          deltaValues[index].GroupName = Consts.SystemAttributesGroupName;
        IDBAttributeType dbAttributeType = (IDBAttributeType) null;
        if ((modes & GetAttributeValuesModes.IncludeGuid) == GetAttributeValuesModes.IncludeGuid)
        {
          dbAttributeType = this.UserSession.GetAttributeType(delta);
          deltaValues[index].AttributeGuid = (dbAttributeType as IDBGuid).GUID;
        }
        if ((modes & GetAttributeValuesModes.IncludeName) == GetAttributeValuesModes.IncludeName)
        {
          if (dbAttributeType == null)
            dbAttributeType = this.UserSession.GetAttributeType(delta);
          deltaValues[index].AttributeName = dbAttributeType.Name;
        }
      }
    }
    return deltaValues;
  }

  public AttributeValues[] GetDeltaValues(GetAttributeValuesModes modes)
  {
    return this.GetDeltaValues(modes, false);
  }

  public void ClearDeltaValues() => this._DeltaList = (List<int>) null;

  public void AddDeltaValue(int attributeID)
  {
    if (this._DeltaList == null)
      this._DeltaList = new List<int>();
    if (attributeID == 0 || this._DeltaList.IndexOf(attributeID) >= 0 || !this.IsAttrListLoaded)
      return;
    if (attributeID > 0 && this.FindByID(attributeID) == null)
      throw new KernelExceptionID(sc_12387.ssp_appserver_12393(1427890147), (object) attributeID);
    this._DeltaList.Add(attributeID);
  }

  public IDBAttribute[] GetAttributesByType(FieldTypes ft)
  {
    List<IDBAttribute> dbAttributeList = new List<IDBAttribute>();
    foreach (IDBAttribute attributes in this._AttributesList)
    {
      if (attributes.AttributeType.AttributeType == ft)
        dbAttributeList.Add(attributes);
    }
    return dbAttributeList.ToArray();
  }

  public DataTable GetAttributesDataTable()
  {
    List<int> intList = (List<int>) null;
    DataTable attributesDataTable = this.GetAttributesDataTable(this.ObjectID);
    if (this._attrList == null)
      this.InitAdditionalAttributes(attributesDataTable);
    attributesDataTable.Columns["F_DATE_VALUE"].DateTimeMode = DataSetDateTime.Unspecified;
    for (int AttrIndex = 0; AttrIndex < this.Count; ++AttrIndex)
    {
      if (!(this[AttrIndex] as DBAttribute).CheckAccess(ActionType.Read, (this._Parent as DBAttributable).GetDefaultAccess(ActionType.Read), false))
      {
        if (intList == null)
          intList = new List<int>();
        intList.Add(this[AttrIndex].AttributeID);
      }
    }
    if (intList == null)
      return attributesDataTable;
    DataTable toTable = attributesDataTable.Clone();
    int columnIndex = attributesDataTable.Columns.IndexOf("F_ATTRIBUTE_ID");
    for (int index1 = 0; index1 < attributesDataTable.Rows.Count; ++index1)
    {
      int int32 = Convert.ToInt32(attributesDataTable.Rows[index1][columnIndex]);
      bool flag = false;
      for (int index2 = 0; index2 < intList.Count; ++index2)
      {
        if (int32 != index2)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        SqlHelper.AssignRow(toTable, attributesDataTable.Rows[index1]);
    }
    toTable.ExtendedProperties.Add((object) "Hidden", (object) 1);
    return toTable;
  }

  public void SetDependentAttributes(int[] masterIDs)
  {
    for (int index1 = 0; index1 < masterIDs.Length; ++index1)
    {
      IDBAttribute byId = this.FindByID(masterIDs[index1]);
      if (byId != null && !byId.IsNull)
      {
        byId.Index = 0;
        IDBObject dbObject = this.UserSession.GetObject(byId.AsInteger, false);
        if (dbObject != null)
        {
          DataTable dataTable = this.ParentType.Attributes.Select(string.Empty);
          for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
          {
            if (Convert.ToInt32(dataTable.Rows[index2]["F_MASTER_ID"]) == masterIDs[index1])
            {
              int attributeID = Convert.ToInt32(dataTable.Rows[index2]["F_SOURCE_ID"]) <= 0 ? Convert.ToInt32(dataTable.Rows[index2]["F_ATTRIBUTE_ID"]) : Convert.ToInt32(dataTable.Rows[index2]["F_SOURCE_ID"]);
              IDBAttribute attributeById = dbObject.GetAttributeByID(attributeID);
              if (attributeById != null)
                this.AddAttribute(Convert.ToInt32(dataTable.Rows[index2]["F_ATTRIBUTE_ID"]), false, false, attributeById.Values);
            }
          }
        }
      }
    }
  }

  public List<IDBAttribute> ToList()
  {
    return new List<IDBAttribute>((IEnumerable<IDBAttribute>) this._AttributesList);
  }

  internal void ReplaceAttributeClass(DBAttribute attr)
  {
    if (!this.IsAttrListLoaded)
      return;
    for (int index = 0; index < this._attrList.Count; ++index)
    {
      if (this._attrList[index].AttributeID == attr.AttributeID)
      {
        this._attrList.RemoveAt(index);
        this._attrList.Add(attr);
        break;
      }
    }
  }

  public int[] GetExistsAttributes()
  {
    List<int> intList = new List<int>();
    if (this.IsAttrListLoaded)
    {
      for (int index = 0; index < this._attrList.Count; ++index)
        intList.Add(this._attrList[index].AttributeID);
    }
    else
    {
      foreach (DataRow row in (InternalDataCollectionBase) this.UserSession.DataManager.ExecuteDataTable($"SELECT DISTINCT F_ATTRIBUTE_ID FROM {this.AttributesTableName} WHERE {this.AttributesKeyName} = :oID", this.UserSession.DataManager.Parameter("oID", (object) this.ObjectID)).Rows)
        intList.Add(Convert.ToInt32(row[0]));
    }
    return intList.ToArray();
  }
}
