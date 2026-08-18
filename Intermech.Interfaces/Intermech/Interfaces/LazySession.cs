
// Type: Intermech.Interfaces.LazySession
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;


namespace Intermech.Interfaces
{
    /// <summary>"Ленивый хранитель сессии", который по сути является оболочкой над обычным SessionKeeper, который создаётся только в случае
    /// первого обращения к сессии</summary>
    public class LazySession : IDisposable
    {
      [CanBeNull]
      private SessionKeeper _sk;
      [NotNull]
      private readonly object _sync = new object();
      private bool _disposed;

      /// <summary>Пользовательская сессия</summary>
      [NotNull]
      public IUserSession Session
      {
        get
        {
          if (this._disposed)
            throw new ObjectDisposedException(nameof (LazySession));
          if (this._sk == null)
          {
            lock (this._sync)
            {
              if (this._sk == null)
                this._sk = new SessionKeeper();
            }
          }
          return this._sk.Session;
        }
      }

      public void Dispose()
      {
        this._disposed = true;
        if (this._sk == null)
          return;
        lock (this._sync)
        {
          if (this._sk == null)
            return;
          this._sk.Dispose();
          this._sk = (SessionKeeper) null;
        }
      }
    }
}
