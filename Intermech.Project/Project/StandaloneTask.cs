// Decompiled with JetBrains decompiler
// Type: Intermech.Project.StandaloneTask
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Project;

public class StandaloneTask : Task
{
  [CanBeNull]
  public static Task Get([NotNull] IUserSession session, [NotEmpty] long objectID)
  {
    return StandaloneTask.Get((ISessionProvider) new SessionProvider(session), objectID, typeof (StandaloneTask));
  }

  [CanBeNull]
  public static Task Get([NotNull] ISessionProvider sessionProvider, long objectID)
  {
    return StandaloneTask.Get(sessionProvider, objectID, typeof (StandaloneTask));
  }

  [CanBeNull]
  internal static Task Get([NotNull] ISessionProvider sessionProvider, [NotEmpty] long objectID, [NotNull] Type taskType)
  {
    Task task = (Task) null;
    IUserSession session = sessionProvider.GetSession();
    try
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(objectID, false);
      if (objectActualCopy != null)
      {
        if (objectActualCopy.ObjectType == (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project)
          task = (Task) new Intermech.Project.Project(objectID);
        else
          task = (Task) taskType.GetConstructor(new Type[1]
          {
            typeof (long)
          }).Invoke(new object[1]{ (object) objectID });
        task._SessionProvider = sessionProvider;
        task._Partial = true;
        task.Load(objectActualCopy, new bool?(false));
      }
    }
    finally
    {
      sessionProvider.ReleaseSession();
    }
    return task;
  }

  public StandaloneTask()
  {
  }

  public StandaloneTask(long objectID)
    : base(objectID)
  {
    this._Partial = true;
  }

  public override double PercentCompleted
  {
    get => base.PercentCompleted;
    set
    {
      double percentCompleted = this.PercentCompleted;
      if (value == percentCompleted)
        return;
      base.PercentCompleted = value;
      if (this.HasState(TaskState.Loading))
        return;
      IUserSession session = this.GetSession();
      try
      {
        IDBTransactions customService = session.GetCustomService<IDBTransactions>();
        customService.StartTransaction();
        try
        {
          this.GetObject(true);
          try
          {
            IDBAttribute attributeById = this._Object.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted);
            if (attributeById != null)
              attributeById.AsDouble = value;
            else
              this._Object.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted, false, new object[1]
              {
                (object) value
              });
          }
          finally
          {
            this.ReleaseObject();
          }
          if (value == 100.0)
          {
            bool autoRollback = customService.AutoRollback;
            customService.AutoRollback = false;
            try
            {
              this.ProjectNeeded();
              if (this.Project != null)
              {
                if (this.Project.VerifyTaskCompleted((Task) this))
                  this.Status = TaskStatus.Completed;
              }
            }
            finally
            {
              customService.AutoRollback = autoRollback;
            }
          }
          if (this.Status != TaskStatus.NotStarted)
            this.UpdateParentPercentCompleted(percentCompleted);
          customService.Commit();
        }
        catch
        {
          customService.Rollback();
          base.PercentCompleted = percentCompleted;
          throw;
        }
      }
      finally
      {
        this.ReleaseSession();
      }
    }
  }

  public override string NameInMessages
  {
    get
    {
      string seed = this.Name;
      if (seed == string.Empty)
        seed = $"(ID={this.ObjectID})";
      IUserSession session = this.GetSession();
      try
      {
        DataTable dataTable = session.GetCustomService<ICompositionLoadService>().LoadComposition((object) session.SessionGUID, this.ObjectID, (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task, (IEnumerable<int>) ListFactory.Create<int>((int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition), (IEnumerable<int>) ListFactory.Create<int>((int) (IpsMetadataEntityBase<int>) ObjectTypes.Task), (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
        {
          new ColumnDescriptor((object) -50)
        }, false, false, (VersionsRule) null, (IEnumerable<ConditionStructure>) null, string.Empty, (HybridDictionary) null, -1);
        string str1 = dataTable != null ? dataTable.Aggregate<string>(seed, (Func<string, DataRow, string>) ((current, row) =>
        {
          string str2 = row[0]?.ToString();
          return string.IsNullOrWhiteSpace(str2) ? $"{str2}\\{current}" : "{Без имени}\\" + current;
        })) : (string) null;
        return string.IsNullOrWhiteSpace(str1) ? this.Name : $"{str1}\\{this.Name}";
      }
      finally
      {
        this.ReleaseSession();
      }
    }
  }
}
