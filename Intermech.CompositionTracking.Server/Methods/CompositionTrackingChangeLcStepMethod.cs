// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Methods.CompositionTrackingChangeLcStepMethod
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Params;
using Intermech.CompositionTracking.Server.Session;
using Intermech.Interfaces;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Interfaces.LifeCycles;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.CompositionTracking.Server.Methods;

internal class CompositionTrackingChangeLcStepMethod : CompositionTrackingBaseMethod
{
  private readonly IDictionary<CompositionTrackSettingData, List<int>> _lcStepAllowed;

  private bool GetNextEnabledStepId(
    IDBObject sourceDbObject,
    IDBObject targetDbObject,
    out int targetNextStepId)
  {
    targetNextStepId = 0;
    IMSLifeCycleStep imsLifeCycleStep = (IMSLifeCycleStep) null;
    CompositionTrackingSessionData data = CompositionTrackingSessionDataHolder.GetData(sourceDbObject.Session, false);
    if (data != null)
    {
      if (data.BeforeLifeCycleSteps.ContainsKey((long) targetNextStepId))
        return false;
      data.BeforeLifeCycleSteps.TryGetValue(sourceDbObject.ObjectID, out imsLifeCycleStep);
    }
    IUserSession session = targetDbObject.Session;
    IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(targetDbObject.LCStep);
    IMSLifeCycleStep imsSourceCurrentLcStep = MetaDataHelper.GetLCStep(sourceDbObject.LCStep);
    if (lcStep == null || imsSourceCurrentLcStep == null)
      return false;
    int[] numArray = (int[]) null;
    if (lcStep.SchemaID == imsSourceCurrentLcStep.SchemaID)
    {
      targetNextStepId = sourceDbObject.LCStep;
    }
    else
    {
      IMSObjectType objectType1 = MetaDataHelper.GetObjectType(targetDbObject.ObjectType);
      IMSObjectType objectType2 = MetaDataHelper.GetObjectType(sourceDbObject.ObjectType);
      if (objectType1 == null || objectType2 == null)
        return false;
      IDBLCSchema dblcSchema = (IDBLCSchema) null;
      CompositionTrackSettingData key = lcStep.SchemaID != objectType1.SchemaID || imsSourceCurrentLcStep.SchemaID != objectType2.SchemaID ? (CompositionTrackSettingData) null : new CompositionTrackSettingData(targetDbObject.ObjectType, sourceDbObject.ObjectType);
      List<int> intList;
      if (key == null || !this._lcStepAllowed.TryGetValue(key, out intList))
      {
        intList = new List<int>();
        try
        {
          dblcSchema = session.GetLCSchema(lcStep.SchemaID);
          if (dblcSchema == null)
            return false;
          DataSet schema = dblcSchema.GetStepsCollection().GetSchema();
          if (schema == null)
            return false;
          DataTable table = schema.Tables["IMS_LC_STEPS"];
          if (table == null)
            return false;
          DataRow[] source = table.Select("[F_LEVEL_ID] = " + (object) imsSourceCurrentLcStep.LevelID);
          if (source.Length == 0)
            return false;
          intList.AddRange(((IEnumerable<DataRow>) source).Select<DataRow, int>((System.Func<DataRow, int>) (dbRow => Convert.ToInt32(dbRow["F_LC_STEP"]))));
        }
        finally
        {
          if (key != null)
            this._lcStepAllowed.Add(key, intList);
        }
      }
      if (intList.Count == 0 || intList.Contains(targetDbObject.LCStep))
        return false;
      if (intList.Count > 1)
      {
        numArray = ((DBObject) targetDbObject).LCStepObject.GetNextSteps();
        List<int> resultData;
        GenericListHelper.GetDifference<int>((IList<int>) intList, (IList<int>) new List<int>((IEnumerable<int>) numArray), GenericListHelper.SearchMode.smExistInBoth, out resultData);
        intList = resultData;
      }
      if (intList.Count > 1)
      {
        List<int> list = intList.Where<int>((System.Func<int, bool>) (lcStepId => MetaDataHelper.GetLCStep(lcStepId).Name == imsSourceCurrentLcStep.Name)).ToList<int>();
        if (list.Count == 1)
        {
          intList = list;
        }
        else
        {
          if (dblcSchema == null)
            dblcSchema = session.GetLCSchema(lcStep.SchemaID);
          IMSLifeCycleLevel lcLevel = MetaDataHelper.GetLCLevel(imsSourceCurrentLcStep.LevelID);
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("CompositionTracking.LcStepAmbiguous"), (object) targetDbObject.Caption, (object) targetDbObject.ObjectID, (object) dblcSchema?.Name, (object) lcLevel?.Name));
        }
      }
      targetNextStepId = intList.Count != 0 ? intList[0] : -1;
    }
    if (targetNextStepId == -1 || targetNextStepId == targetDbObject.LCStep)
      return false;
    if (numArray == null)
      numArray = ((DBObject) targetDbObject).LCStepObject.GetNextSteps();
    return Array.IndexOf<int>(numArray, targetNextStepId) != -1;
  }

  public CompositionTrackingChangeLcStepMethod()
  {
    this._lcStepAllowed = (IDictionary<CompositionTrackSettingData, List<int>>) new Dictionary<CompositionTrackSettingData, List<int>>();
  }

  public override CompositionTrackingCommands Command => CompositionTrackingCommands.ctcNextLCStep;

  internal override bool Validate(CompositionTrackingParams trackingParams)
  {
    return base.Validate(trackingParams) && trackingParams.DbObject is DBObject dbObject && !dbObject.DenyChangeLCStep;
  }

  internal override bool Execute(
    CompositionTrackingParams trackingParams,
    IDBObject sourceDbObject,
    ref IDBObject targetDbObject)
  {
    int targetNextStepId;
    if (sourceDbObject == null || targetDbObject == null || targetDbObject.CheckoutBy != 0L || !this.GetNextEnabledStepId(sourceDbObject, targetDbObject, out targetNextStepId))
      return false;
    targetDbObject.LCStep = targetNextStepId;
    return true;
  }
}
