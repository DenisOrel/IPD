// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.SelectionStructureCopier
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;


namespace Intermech.Kernel.Services;

internal sealed class SelectionStructureCopier
{
  private readonly IUserSession _session;
  private readonly long _prototypeID;
  private readonly long _parentID;
  private readonly string _sessionName;
  private readonly Thread _thread;
  private readonly string _name;
  private readonly int _objTypeSelectionPersonID;
  private readonly int _objTypeClassifierPersonID;
  private IDBRelationCollection _relationCollection;
  private readonly List<Tuple<int, IDBObjectCollection>> _objectCollections;

  public StructureCopierStateInfo StateInfo { get; }

  public Guid GUID { get; }

  public SelectionStructureCopier(
    IUserSession session,
    string name,
    long prototypeID,
    long parentID)
  {
    this._sessionName = $"SelectionStructureCopier_{Guid.NewGuid()}";
    this._session = session;
    this._name = name;
    this._prototypeID = prototypeID;
    this._parentID = parentID;
    this._objTypeSelectionPersonID = MetaDataHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545");
    this._objTypeClassifierPersonID = MetaDataHelper.GetObjectTypeID("cad0014f-306c-11d8-b4e9-00304f19f545");
    this._objectCollections = new List<Tuple<int, IDBObjectCollection>>();
    this._thread = new Thread(new ThreadStart(this.ThreadMethod))
    {
      IsBackground = true,
      Name = this._sessionName
    };
    this.StateInfo = new StructureCopierStateInfo(name);
    this.GUID = Guid.NewGuid();
  }

  private void ThreadMethod()
  {
    try
    {
      UserSession session = this._session.Clone(this._sessionName) as UserSession;
      try
      {
        this._relationCollection = session.GetRelationCollection(session.IdentHelper.SortedRelationTypeID);
        this._relationCollection.LocalTypesMode = true;
        this.StateInfo.SessionGuid = session.SessionGUID;
        session.StartTransaction();
        this.CopyStructure((IUserSession) session, this._prototypeID, this._parentID, true);
        session.Commit();
        this.StateInfo.State = OperationStates.Done;
      }
      catch (Exception ex)
      {
        if (ex is ThreadAbortException)
        {
          this.StateInfo.State = OperationStates.Done;
        }
        else
        {
          this.StateInfo.State = OperationStates.Error;
          this.StateInfo.Exception = ex;
          (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"Ошибка при выполнении задачи \"{this._name}\": {ex.Message}");
        }
        if (!session.InTransaction)
          return;
        session.Rollback();
      }
      finally
      {
        session.Logout(this._sessionName);
      }
    }
    catch (Exception ex)
    {
      if (!(ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service))
        return;
      service.AddToTrace($"Ошибка при выполнении задачи \"{this._name}\": {ex.Message}");
      service.AddToTrace(ex.StackTrace);
    }
  }

  public void Start() => this._thread.Start();

  public void Stop()
  {
    if (!this._thread.IsAlive)
      return;
    this._thread.Abort();
  }

  private void CopyStructure(
    IUserSession session,
    long prototypeID,
    long parentID,
    bool firstLevel)
  {
    this._relationCollection.LocalTypesMode = true;
    DataTable dataTable = this._relationCollection.ConsistFrom(new DBRecordSetParams(new ConditionStructure[6]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) this._objTypeSelectionPersonID, LogicalOperators.AND, 2, false),
      new ConditionStructure(-8, RelationalOperators.Equal, (object) this._session.UserID, LogicalOperators.OR, -1, false),
      new ConditionStructure(-7, RelationalOperators.NotEqual, (object) this._objTypeSelectionPersonID, LogicalOperators.OR, 0, false),
      new ConditionStructure(-7, RelationalOperators.Equal, (object) this._objTypeClassifierPersonID, LogicalOperators.AND, 1, false),
      new ConditionStructure(-8, RelationalOperators.Equal, (object) this._session.UserID, LogicalOperators.OR, -1, false),
      new ConditionStructure(-7, RelationalOperators.NotEqual, (object) this._objTypeClassifierPersonID, LogicalOperators.OR, -1, false)
    }, new object[3]
    {
      (object) -2,
      (object) -20,
      (object) -7
    }), prototypeID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      DataRow row = dataTable.Rows[index];
      long int64 = Convert.ToInt64(row[0]);
      int childObjectTypeID = Convert.ToInt32(row[2]);
      Tuple<int, IDBObjectCollection> tuple = this._objectCollections.Find((Predicate<Tuple<int, IDBObjectCollection>>) (x => x.Item1.Equals(childObjectTypeID)));
      IDBObjectCollection objectCollection;
      if (tuple != null)
      {
        objectCollection = tuple.Item2;
      }
      else
      {
        objectCollection = session.GetObjectCollection(childObjectTypeID);
        this._objectCollections.Add(new Tuple<int, IDBObjectCollection>(childObjectTypeID, objectCollection));
      }
      IDBObject dbObject = objectCollection.Create(int64);
      IDBRelation dbRelation = this._relationCollection.Create(new NewRelationProperties(Convert.ToInt64(row[1]), parentID, dbObject.ID, DateTime.Now, DateTime.MaxValue, dbObject.ObjectID));
      dbObject.CommitCreation(true);
      this.StateInfo.CreatedObjectIDs.Add(dbObject.ObjectID);
      this.StateInfo.CreatedRelationIDs.Add(dbRelation.RelationID);
      this.CopyStructure(session, int64, dbObject.ObjectID, false);
      if (firstLevel)
        this.StateInfo.CurrentUnit = 100 / dataTable.Rows.Count * (index + 1);
    }
  }
}
