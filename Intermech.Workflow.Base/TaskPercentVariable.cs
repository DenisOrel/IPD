// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.TaskPercentVariable
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Project;
using System;


namespace Intermech.Workflow
{
    /// <summary>Процент выполнения задачи ImProject</summary>
    [Serializable]
    public class TaskPercentVariable(VarList owner, IDBObject obj) : CalculatedSystemVariable(owner, obj, wfConsts.SysVarTaskPercentID)
    {
      private Task _task;
      private bool _taskLoaded;

      public override bool Calculated => false;

      protected override string CalcValue()
      {
        return this.Task != null ? this.Task.PercentCompleted.ToString() : "-1";
      }

      protected override void AfterSetValue()
      {
        base.AfterSetValue();
        Task task = this.Task;
        double result;
        if (task == null || !double.TryParse(this.Value, out result))
          return;
        IDBObject dbObject = task.GetObject();
        try
        {
          task.SetRuntimeFlag(dbObject, RuntimeFlags.AutoComplete);
          try
          {
            if (Math.Round(result) == 100.0 && task.Status == TaskStatus.Sent)
              task.Status = TaskStatus.Executed;
            this.Task.PercentCompleted = result;
          }
          finally
          {
            task.SetRuntimeFlag(dbObject, RuntimeFlags.AutoComplete, false);
          }
        }
        finally
        {
          task.ReleaseObject();
        }
      }

      protected Task Task
      {
        get
        {
          if (!this._taskLoaded)
          {
            IScheme scheme = this.GetObject() as IScheme;
            try
            {
              if (scheme != null && scheme.LinkedTaskObjectID != 0L)
                this._task = StandaloneTask.Get(this._owner.Session, scheme.LinkedTaskObjectID);
              this._taskLoaded = true;
            }
            finally
            {
              this.ReleaseObject();
            }
          }
          return this._task;
        }
      }
    }
}
