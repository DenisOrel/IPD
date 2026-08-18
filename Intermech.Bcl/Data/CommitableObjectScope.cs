
// Type: Intermech.Data.CommitableObjectScope
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data
{
    public struct CommitableObjectScope(ICommitableObjectThreadState threadState, bool isTopLevel) : 
      IDisposable
    {
      private ICommitableObjectThreadState threadState = threadState;
      private bool isTopLevel = isTopLevel;
      private bool isCompleted = false;

      public void Dispose()
      {
        ICommitableObjectThreadState threadState = this.threadState;
        if (threadState == null)
          return;
        if (!this.isCompleted)
          threadState.CanCommit = false;
        if (this.isTopLevel)
        {
          if (threadState.CanCommit)
            threadState.CommitableObject.Commit();
          else
            threadState.CommitableObject.Rollback();
        }
        this.threadState = (ICommitableObjectThreadState) null;
      }

      public void Complete() => this.isCompleted = true;
    }
}
