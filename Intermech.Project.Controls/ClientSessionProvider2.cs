// Decompiled with JetBrains decompiler
// Type: Intermech.ClientSessionProvider2
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech;

public static class ClientSessionProvider2
{
  [NotNull]
  private static readonly ThreadLocal<ClientSessionProvider2.SessionProvider> _threadLocalInstance = new ThreadLocal<ClientSessionProvider2.SessionProvider>((Func<ClientSessionProvider2.SessionProvider>) (() => new ClientSessionProvider2.SessionProvider()));

  [NotNull]
  public static ISessionProvider Provider
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (ISessionProvider) ClientSessionProvider2._threadLocalInstance.Value;
    }
  }

  [NotNull]
  public static IUserSession GetSession() => ClientSessionProvider2.Provider.GetSession();

  public static bool ReleaseSession() => ClientSessionProvider2.Provider.ReleaseSession();

  internal class SessionProvider : ISessionProvider, IDisposable
  {
    private readonly int _createdInThreadID = Thread.CurrentThread.ManagedThreadId;
    [CanBeNull]
    private SessionKeeper _sk;
    private int _counter;
    private bool _disposed;

    public void Dispose()
    {
      Intermech.Diagnostics.Check.NotDisposed<ClientSessionProvider2.SessionProvider>(this._disposed);
      SessionKeeper sessionKeeper = Interlocked.Exchange<SessionKeeper>(ref this._sk, (SessionKeeper) null);
      this._disposed = true;
      sessionKeeper?.Dispose();
    }

    [NotNull]
    IUserSession ISessionProvider.GetSession()
    {
      Intermech.Diagnostics.Check.NotDisposed<ClientSessionProvider2.SessionProvider>(this._disposed);
      return this._createdInThreadID != Thread.CurrentThread.ManagedThreadId ? ClientSessionProvider2._threadLocalInstance.Value.GetSessionInternal() : this.GetSessionInternal();
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IUserSession GetSessionInternal()
    {
      Intermech.Diagnostics.Check.NotDisposed<ClientSessionProvider2.SessionProvider>(this._disposed);
      if (this._sk == null)
        this._sk = new SessionKeeper();
      ++this._counter;
      return this._sk.Session;
    }

    bool ISessionProvider.ReleaseSession()
    {
      return this._createdInThreadID != Thread.CurrentThread.ManagedThreadId ? ClientSessionProvider2._threadLocalInstance.Value.ReleaseSessionInternal() : this.ReleaseSessionInternal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool ReleaseSessionInternal()
    {
      Intermech.Diagnostics.Check.NotDisposed<ClientSessionProvider2.SessionProvider>(this._disposed);
      if (--this._counter != 0)
        return false;
      Interlocked.Exchange<SessionKeeper>(ref this._sk, (SessionKeeper) null)?.Dispose();
      return true;
    }
  }
}
