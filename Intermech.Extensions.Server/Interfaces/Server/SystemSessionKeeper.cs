// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.SystemSessionKeeper
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces.Server;

public class SystemSessionKeeper : IDisposable
{
  [NotNull]
  [NotWhitespace]
  private readonly string _sessionName;
  [CanBeNull]
  private IUserSession _session;
  private volatile bool _isDisposed;
  [NotNull]
  private readonly object _syncObject = new object();

  public SystemSessionKeeper([NotNull, NotWhitespace] string sessionName)
  {
    this._sessionName = sessionName;
  }

  public void Dispose()
  {
    Intermech.Diagnostics.Check.NotDisposed<SystemSessionKeeper>(this._isDisposed);
    lock (this._syncObject)
    {
      Intermech.Diagnostics.Check.NotDisposed<SystemSessionKeeper>(this._isDisposed);
      this._isDisposed = true;
      Interlocked.Exchange<IUserSession>(ref this._session, (IUserSession) null)?.Logout(this._sessionName);
    }
  }

  [NotNull]
  public IUserSession Session
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Intermech.Diagnostics.Check.NotDisposed<SystemSessionKeeper>(this._isDisposed);
      lock (this._syncObject)
      {
        Intermech.Diagnostics.Check.NotDisposed<SystemSessionKeeper>(this._isDisposed);
        return this._session ?? (this._session = ApplicationServices.Container.GetService<IDBTimedEvents>().GetSystemSessionTemporaryClone(this._sessionName));
      }
    }
  }

  [NotNull]
  [NotWhitespace]
  public string SessionName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Intermech.Diagnostics.Check.NotDisposed<SystemSessionKeeper>(this._isDisposed);
      return this._sessionName;
    }
  }
}
