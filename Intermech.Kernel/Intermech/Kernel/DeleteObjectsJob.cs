// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DeleteObjectsJob
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Kernel;

public class DeleteObjectsJob
{
  private DeleteObjectsJobProgress oldProgress;
  public Guid Guid;
  public Guid SessionGuid;
  public Thread Thread;
  public DeletingObjects Items;
  public List<long> DeletedItems;
  public List<long> DeletedRelations;
  public List<long> DeletedRelationsProjIDs;
  public List<int> DeletedRelationsTypeIDs;
  private DeleteObjectsJobStatus status;

  public DeleteObjectsJobStatus Status
  {
    get
    {
      lock (this.status)
        return this.status.Clone() as DeleteObjectsJobStatus;
    }
  }

  public DeleteObjectsJob(Guid sessionGuid, DeletingObjects items)
  {
    this.Guid = Guid.NewGuid();
    this.SessionGuid = sessionGuid;
    this.Thread = (Thread) null;
    this.Items = items;
    if (items != null)
    {
      items.Sort();
      items.BaseVersionsDown();
    }
    this.status = new DeleteObjectsJobStatus();
  }

  public override bool Equals(object obj)
  {
    return !(obj is DeleteObjectsJob deleteObjectsJob) ? base.Equals(obj) : this.Guid.Equals(deleteObjectsJob.Guid);
  }

  public override int GetHashCode() => this.Guid.GetHashCode();

  protected virtual void SleepIfNeed()
  {
    bool flag = true;
    while (flag)
    {
      lock (this.status)
      {
        if (this.status.Progress != DeleteObjectsJobProgress.Idle)
          flag = false;
      }
      if (flag)
        Thread.Sleep(1000);
    }
  }

  protected virtual void AnalyzeLogHistory(
    IUserSession session,
    List<long> tempDelRels,
    List<long> tempDelRelsProjIDs,
    List<int> tempDelRelsTypeIDs,
    List<long> tempDelObjs)
  {
    if (session == null || tempDelRels == null || tempDelRelsProjIDs == null || tempDelRelsTypeIDs == null || tempDelObjs == null)
      return;
    foreach (CategoryValue modificationsHistory in session.GetModificationsHistoryList())
    {
      if (modificationsHistory.CategoryType == 1 && (modificationsHistory.ActionID == ActionType.Delete || modificationsHistory.ActionID == ActionType.Purge) && tempDelObjs.IndexOf(modificationsHistory.CategoryID) < 0)
        tempDelObjs.Add(modificationsHistory.CategoryID);
      if (modificationsHistory.CategoryType == 5 && (modificationsHistory.ActionID == ActionType.Delete || modificationsHistory.ActionID == ActionType.Purge) && tempDelRels.IndexOf(modificationsHistory.CategoryID) < 0)
      {
        tempDelRels.Add(modificationsHistory.CategoryID);
        tempDelRelsProjIDs.Add(0L);
        tempDelRelsTypeIDs.Add(-1);
      }
    }
  }

  protected virtual void ThreadMethod()
  {
    lock (this.status)
    {
      if (this.status.Progress != DeleteObjectsJobProgress.NotStarted)
        return;
      this.status.Start();
    }
    this.DeletedItems = new List<long>();
    this.DeletedRelations = new List<long>();
    this.DeletedRelationsProjIDs = new List<long>();
    this.DeletedRelationsTypeIDs = new List<int>();
    int objects = 0;
    int num1 = 0;
    int num2 = 0;
    try
    {
      IUserSession session = (UserSession.GetSessionByID(this.SessionGuid) as UserSession).Clone(nameof (DeleteObjectsJob));
      IDBTransactions customService = session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      List<long> tempDelRels = new List<long>();
      List<long> tempDelRelsProjIDs = new List<long>();
      List<int> tempDelRelsTypeIDs = new List<int>();
      List<long> tempDelObjs = new List<long>();
      try
      {
        List<DeletingObject> absDeletingObjects = this.Items.ExtractAbsDeletingObjects();
        (session as UserSession).RemovableObjectsList.AddObjects(absDeletingObjects);
        for (int index1 = 0; index1 < absDeletingObjects.Count; ++index1)
        {
          DBObject dbObject1 = (DBObject) null;
          try
          {
            DeletingObject deletingObject = absDeletingObjects[index1];
            if (!deletingObject.RemoveObject)
            {
              ++num1;
            }
            else
            {
              if (this.Thread == null)
              {
                lock (this.status)
                {
                  this.status.Progress = DeleteObjectsJobProgress.Cancelled;
                  break;
                }
              }
              this.SleepIfNeed();
              customService.StartTransaction();
              session.StartLogHistory();
              tempDelRels.Clear();
              tempDelRelsProjIDs.Clear();
              tempDelRelsTypeIDs.Clear();
              tempDelObjs.Clear();
              session.ClearObjectSmartCache();
              IDBObject dbObject2 = session.GetObject(absDeletingObjects[index1].ObjectID, false);
              if (dbObject2 != null)
              {
                try
                {
                  (dbObject2 as IDBSecurity).CheckAccess(ActionType.Delete);
                }
                catch
                {
                  dbObject1 = dbObject2 as DBObject;
                  throw;
                }
              }
              else
                session.GetObject(-absDeletingObjects[index1].ObjectID, false)?.Delete((long) Consts.PurgeMode);
              bool flag = false;
              if (deletingObject.PrjLinkIDs.Count > 0)
              {
                for (int index2 = 0; index2 < deletingObject.PrjLinkIDs.Count; ++index2)
                {
                  IDBRelation relation = session.GetRelation(deletingObject.PrjLinkIDs[index2], false);
                  if (relation != null && Math.Abs(relation.ProjID) != Math.Abs(deletingObject.ObjectID))
                  {
                    long relationId = relation.RelationID;
                    long projId = relation.ProjID;
                    relation.Delete((long) Consts.DontCheckApplicabilityModes);
                    if (relationId < 0L)
                      session.GetRelation(Math.Abs(relationId), false)?.Delete((long) Consts.PurgeMode);
                    if (tempDelRels.IndexOf(deletingObject.PrjLinkIDs[index2]) < 0)
                    {
                      tempDelRels.Add(deletingObject.PrjLinkIDs[index2]);
                      tempDelRelsProjIDs.Add(relation.ProjID);
                      tempDelRelsTypeIDs.Add(relation.RelationType);
                      ++num2;
                    }
                  }
                  else if (tempDelRels.IndexOf(deletingObject.PrjLinkIDs[index2]) < 0)
                  {
                    tempDelRels.Add(deletingObject.PrjLinkIDs[index2]);
                    tempDelRelsProjIDs.Add(0L);
                    tempDelRelsTypeIDs.Add(-1);
                    ++num2;
                  }
                }
                flag = true;
              }
              if (dbObject2 != null && dbObject2.CheckoutBy != 0L)
              {
                IDBObject dbObject3 = session.GetObject(-deletingObject.ObjectID, false);
                if (dbObject3 != null)
                {
                  dbObject3.Delete(0L);
                  flag = true;
                }
              }
              if (absDeletingObjects[index1].ObjectID != 0L && absDeletingObjects[index1].ObjectID != 0L && tempDelObjs.IndexOf(-absDeletingObjects[index1].ObjectID) < 0)
                tempDelObjs.Add(-absDeletingObjects[index1].ObjectID);
              if (flag)
                dbObject2 = session.GetObject(absDeletingObjects[index1].ObjectID, false);
              if (dbObject2 != null)
              {
                (dbObject2 as DBStoredObject).SetParamsTableValue(152, (object) 0);
                dbObject2.Delete(0L);
              }
              ++objects;
              if (absDeletingObjects[index1].ObjectID != 0L && absDeletingObjects[index1].ObjectID != 0L && tempDelObjs.IndexOf(absDeletingObjects[index1].ObjectID) < 0)
                tempDelObjs.Add(absDeletingObjects[index1].ObjectID);
              if (customService.InTransaction)
              {
                customService.Commit();
                this.AnalyzeLogHistory(session, tempDelRels, tempDelRelsProjIDs, tempDelRelsTypeIDs, tempDelObjs);
                session.StopLogHistory();
                int index = 0;
                tempDelRels.ForEach((Action<long>) (item =>
                {
                  if (this.DeletedRelations.IndexOf(item) < 0)
                  {
                    this.DeletedRelations.Add(item);
                    this.DeletedRelationsProjIDs.Add(tempDelRelsProjIDs[index]);
                    this.DeletedRelationsTypeIDs.Add(tempDelRelsTypeIDs[index]);
                  }
                  ++index;
                }));
                tempDelObjs.ForEach((Action<long>) (item =>
                {
                  if (this.DeletedItems.IndexOf(item) >= 0)
                    return;
                  this.DeletedItems.Add(item);
                }));
                lock (this.status)
                {
                  this.status.Objects = objects;
                  this.status.Skipped = num1;
                  this.status.RelationsCount = num2;
                  this.status.Items = this.DeletedItems;
                  this.status.Relations = this.DeletedRelations;
                  this.status.RelationsProjIDs = this.DeletedRelationsProjIDs;
                  this.status.RelationsTypeIDs = this.DeletedRelationsTypeIDs;
                  if (this.status.Progress == DeleteObjectsJobProgress.Cancelled)
                  {
                    this.status.Cancel(objects, num1, num2, this.DeletedItems, this.DeletedRelations, this.DeletedRelationsProjIDs, this.DeletedRelationsTypeIDs);
                    break;
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            session.StopLogHistory();
            customService.Rollback();
            dbObject1?.AddEvent(dbObject1.ObjectID, ActionType.Delete, EventlogRecordType.AccessDenied);
            lock (this.status)
            {
              ++num1;
              this.status.Exception = ex;
              if (this.status.Mode == DeleteObjectsJobMode.AscOnError)
                this.Pause();
            }
            this.SleepIfNeed();
            lock (this.status)
            {
              if (this.status.Mode == DeleteObjectsJobMode.AbortOnError)
              {
                this.status.Error(ex, objects, num1, num2, this.DeletedItems, this.DeletedRelations, this.DeletedRelationsProjIDs, this.DeletedRelationsTypeIDs);
                break;
              }
              this.status.Exception = ex;
            }
          }
        }
      }
      finally
      {
        (session as UserSession).RemovableObjectsList.Clear();
        try
        {
          if (customService.InTransaction)
            customService.Commit();
        }
        catch
        {
        }
        this.AnalyzeLogHistory(session, tempDelRels, tempDelRelsProjIDs, tempDelRelsTypeIDs, tempDelObjs);
        session.StopLogHistory();
        session.Logout(nameof (DeleteObjectsJob));
        lock (this.status)
        {
          this.status.Objects = objects;
          this.status.Skipped = num1;
          this.status.Items = this.DeletedItems;
          this.status.Relations = this.DeletedRelations;
          if (this.status.Progress != DeleteObjectsJobProgress.Cancelled && this.status.Exception == null)
            this.status.Complete(objects, num1, num2, this.DeletedItems, this.DeletedRelations, this.DeletedRelationsProjIDs, this.DeletedRelationsTypeIDs);
          else
            this.status.Error(this.status.Exception, objects, num2, num1, this.DeletedItems, this.DeletedRelations, this.DeletedRelationsProjIDs, this.DeletedRelationsTypeIDs);
        }
      }
    }
    catch (Exception ex)
    {
      lock (this.status)
        this.status.Error(ex, objects, num1, num2, this.DeletedItems, this.DeletedRelations, this.DeletedRelationsProjIDs, this.DeletedRelationsTypeIDs);
    }
  }

  protected internal virtual void Start(DeleteObjectsJobMode mode)
  {
    lock (this.status)
      this.status.Mode = mode;
    this.Thread = new Thread(new ThreadStart(this.ThreadMethod));
    this.Thread.IsBackground = true;
    this.Thread.Name = "DeleteObjectsJob." + this.Guid.ToString();
    this.Thread.Start();
  }

  protected internal virtual void Stop() => this.Thread = (Thread) null;

  protected internal virtual void Pause()
  {
    if (this.Thread == null || this.status.Progress != DeleteObjectsJobProgress.NotStarted && this.status.Progress != DeleteObjectsJobProgress.Working)
      return;
    lock (this.status)
    {
      this.oldProgress = this.status.Progress;
      this.status.Progress = DeleteObjectsJobProgress.Idle;
    }
  }

  protected internal virtual void Cancel()
  {
    lock (this.status)
      this.status.Progress = DeleteObjectsJobProgress.Cancelled;
  }

  protected internal virtual void Resume(DeleteObjectsJobMode mode)
  {
    if (this.Thread == null || this.status.Progress != DeleteObjectsJobProgress.Idle)
      return;
    lock (this.status)
    {
      this.status.Progress = this.oldProgress;
      this.status.Mode = mode;
    }
  }
}
