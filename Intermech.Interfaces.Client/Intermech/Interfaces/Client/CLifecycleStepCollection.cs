// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.CLifecycleStepCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.LifeCycles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for CLifecycleStepCollection.</summary>
internal class CLifecycleStepCollection : CacheObjectsCollection, IDBLifecycleStepCollection
{
  private IDBLCSchema _Schema;
  private int _ObjectTypeID;

  public CLifecycleStepCollection(ClientSession uSession, IDBLCSchema schema, int objectTypeID)
    : base(uSession, false)
  {
    this.InitOptions("IMS_LC_STEPS", "F_LC_STEP");
    this._Schema = schema;
    this.ParentID = (object) schema.SchemaID;
    this._ObjectTypeID = objectTypeID;
  }

  public IDBLifecycleStep FindSameStep(IDBLifecycleStep oldStep, out string errorMsg)
  {
    this._clientSession.Guard.ValidateCall();
    return this._clientSession.Session.GetLifecycleStepCollection(this.SchemaID, this._ObjectTypeID).FindSameStep(oldStep, out errorMsg);
  }

  /// <summary>
  /// Ид. схемы жизненного цикла, которой принадлежит данная коллекция шагов
  /// </summary>
  public int SchemaID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._Schema.SchemaID;
    }
  }

  public void DeleteLink(int fromStepID, int toStepID)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetLifecycleStepCollection(this.SchemaID, this._ObjectTypeID).DeleteLink(fromStepID, toStepID);
    this._clientSession.ClientCache.ReloadCache(this._clientSession.Session);
  }

  public void SetLinks(DataTable linksList, bool deleteNotExists)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetLifecycleStepCollection(this.SchemaID, this._ObjectTypeID).SetLinks(linksList, deleteNotExists);
    this._clientSession.ClientCache.ReloadCache(this._clientSession.Session);
  }

  public int ObjectTypeID
  {
    [DebuggerStepThrough] get
    {
      this._clientSession.Guard.ValidateCall();
      return this._ObjectTypeID;
    }
  }

  public ObjectSteps[] GetObjectsSteps(long[] objectIDs)
  {
    this._clientSession.Guard.ValidateCall();
    LifecycleSteps lifecycleSteps = new LifecycleSteps();
    for (int index = 0; index < objectIDs.Length; ++index)
    {
      IDBObject dbObject = this._clientSession.GetObject(objectIDs[index]);
      IDBLifecycleStep lifecycleStep = this._clientSession.GetLifecycleStep(dbObject.LCStep);
      lifecycleSteps.Add(new LifecycleStep(dbObject.LCStep, -1));
      foreach (int nextStep in lifecycleStep.GetNextSteps())
        lifecycleSteps.Add(new LifecycleStep(nextStep, 1));
    }
    int length = lifecycleSteps.GoodCount(objectIDs.Length);
    if (length <= 0)
      return (ObjectSteps[]) null;
    ObjectSteps[] objectsSteps = new ObjectSteps[length];
    int index1 = 0;
    foreach (LifecycleStep lcStep in lifecycleSteps._LCStepList)
    {
      if (lcStep.Attr == -1 || lcStep.Attr == objectIDs.Length)
      {
        IDBLifecycleStep lifecycleStep = this._clientSession.GetLifecycleStep(lcStep.Step);
        int atribute = lcStep.Attr == -1 ? 0 : 1;
        objectsSteps[index1] = new ObjectSteps(lcStep.Step, lifecycleStep.LCName, atribute, ((IDBLifecycleLevel) lifecycleStep).LevelIcon);
        ++index1;
      }
    }
    return objectsSteps;
  }

  public ObjectSteps[] GetObjectsSteps(List<int> stepsID)
  {
    this._clientSession.Guard.ValidateCall();
    if (stepsID.Count == 0)
      return (ObjectSteps[]) null;
    List<ObjectSteps> objectStepsList = new List<ObjectSteps>();
    int[] nextSteps1 = this._clientSession.GetLifecycleStep(stepsID[0]).GetNextSteps();
    List<int> intList = new List<int>(nextSteps1.Length);
    intList.AddRange((IEnumerable<int>) nextSteps1);
    for (int index1 = 1; index1 < stepsID.Count; ++index1)
    {
      int[] nextSteps2 = this._clientSession.GetLifecycleStep(stepsID[index1]).GetNextSteps();
      for (int index2 = intList.Count - 1; index2 >= 0; --index2)
      {
        bool flag = false;
        for (int index3 = 0; index3 < nextSteps2.Length; ++index3)
        {
          if (nextSteps2[index3] == intList[index2])
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          intList.RemoveAt(index2);
      }
    }
    if (intList.Count == 0)
      return (ObjectSteps[]) null;
    for (int index = 0; index < stepsID.Count; ++index)
    {
      IDBLifecycleStep lifecycleStep = this._clientSession.GetLifecycleStep(stepsID[index]);
      objectStepsList.Add(new ObjectSteps(stepsID[index], lifecycleStep.LCName, 0, ((IDBLifecycleLevel) lifecycleStep).LevelIcon));
    }
    for (int index = 0; index < intList.Count; ++index)
    {
      IDBLifecycleStep lifecycleStep = this._clientSession.GetLifecycleStep(intList[index]);
      objectStepsList.Add(new ObjectSteps(intList[index], lifecycleStep.LCName, -1, ((IDBLifecycleLevel) lifecycleStep).LevelIcon));
    }
    return objectStepsList.ToArray();
  }

  public void CopyTo(int toObjectTypeID) => this._clientSession.Guard.ValidateCall();

  public int GetFirstStep()
  {
    this._clientSession.Guard.ValidateCall();
    int firstStep = -1;
    DataRow[] dataRowArray1 = this._clientSession.ClientCache.GetTable(this._DBTableName).Select($"F_SCHEMA_ID = {this.ParentID} AND F_FIRST <> 0");
    if (dataRowArray1.Length != 0)
      return Convert.ToInt32(dataRowArray1[0][this._DBKeyField]);
    DataRow[] dataRowArray2 = this._clientSession.ClientCache.GetTable(this._DBTableName).Select("F_SCHEMA_ID = " + this.ParentID.ToString());
    if (dataRowArray2.Length == 0)
      throw new KernelExceptionID(111, (object) this._Schema.Name);
    foreach (DataRow dataRow in dataRowArray2)
    {
      IDBLifecycleLevelType lifecycleLevel = this._clientSession.GetLifecycleLevel(Convert.ToInt32(dataRow["F_LEVEL_ID"]));
      if (lifecycleLevel.IsDefaultLevel)
      {
        firstStep = Convert.ToInt32(dataRow[this._DBKeyField]);
        break;
      }
      if (lifecycleLevel.LevelID == this._clientSession.IdentHelper.CreatedLevelID || firstStep == -1)
        firstStep = Convert.ToInt32(dataRow[this._DBKeyField]);
    }
    return firstStep;
  }

  public IDBLifecycleStep Create(DBLifecycleStepProperties lcProps)
  {
    this._clientSession.Guard.ValidateCall();
    IDBLifecycleStep dbLifecycleStep = this._clientSession.Session.GetLifecycleStepCollection(this.SchemaID, this._ObjectTypeID).Create(lcProps);
    if (dbLifecycleStep == null)
      return dbLifecycleStep;
    this.ReloadCache(0);
    return dbLifecycleStep;
  }

  public DataSet GetSchema()
  {
    this._clientSession.Guard.ValidateCall();
    DataSet schema = new DataSet();
    DataTable dataTable1 = this._clientSession.ClientCache.GetTable(this._DBTableName).Clone();
    DataTable dataTable2 = this._clientSession.ClientCache.GetTable("IMS_LC_LINKS").Clone();
    DataSetProcessor.AssignRows(dataTable1, (IEnumerable<DataRow>) this._clientSession.ClientCache.GetTable(this._DBTableName).Select($"F_SCHEMA_ID = {this.ParentID} AND F_DELETED = 0"));
    if (dataTable1.Rows.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(" OR ");
        stringBuilder.AppendFormat("F_FROM_STEP = {0} OR F_TO_STEP = {0}", dataTable1.Rows[index][this._DBKeyField]);
        if (dataTable1.Rows[index]["F_GUID"] == DBNull.Value)
        {
          IDBGuid lifecycleStep = this._clientSession.GetLifecycleStep(Convert.ToInt32(dataTable1.Rows[index][this._DBKeyField])) as IDBGuid;
          dataTable1.Rows[index]["F_GUID"] = (object) lifecycleStep.GUID.ToString();
        }
      }
      DataSetProcessor.AssignRows(dataTable2, (IEnumerable<DataRow>) this._clientSession.ClientCache.GetTable("IMS_LC_LINKS").Select(stringBuilder.ToString()));
    }
    schema.Tables.Add(dataTable1);
    schema.Tables.Add(dataTable2);
    return schema;
  }

  public void SetSchema(DataSet dsSchema)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetLifecycleStepCollection(this.SchemaID, this._ObjectTypeID).SetSchema(dsSchema);
    this._clientSession.ClientCache.ReloadCache(this._clientSession.Session);
  }

  public void SetObjectsLCStep(long[] objectIDs, int lcStep)
  {
    this._clientSession.Guard.ValidateCall();
    this._clientSession.Session.GetLifecycleStepCollection(this.SchemaID, this._ObjectTypeID).SetObjectsLCStep(objectIDs, lcStep);
  }
}
