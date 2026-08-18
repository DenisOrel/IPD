
// Type: Intermech.Interfaces.DBPatches.AbstractPatch
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Interfaces.DBPatches
{
    public abstract class AbstractPatch : IAction
    {
      private bool isAtomic;

      /// <summary>
      /// Включает и выключает режим атомарного применения патча. В случае включения режима код применения патча будет выполнен в отдельной транзакции СУБД.
      /// </summary>
      public bool IsAtomic
      {
        get => this.isAtomic;
        set => this.isAtomic = value;
      }

      public void Perform()
      {
        if (this.isAtomic)
          this.ApplyTransaction(new Action(this.PerformCore));
        else
          this.PerformCore();
      }

      private void PerformCore()
      {
        try
        {
          this.DoInitialize();
          this.DoPatch();
        }
        finally
        {
          this.DoCleanup();
        }
      }

      protected virtual void DoInitialize()
      {
      }

      protected virtual void DoPatch()
      {
      }

      protected virtual void DoCleanup()
      {
      }

      private void ApplyTransaction(Action action)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBTransactions session = (IDBTransactions) sessionKeeper.Session;
          session.StartTransaction();
          try
          {
            action();
            session.Commit();
          }
          catch
          {
            session.Rollback();
            throw;
          }
        }
      }
    }
}
