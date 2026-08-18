// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.UserSessionKeeper
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces;

public static class UserSessionKeeper
{
  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ISessionKeeper Get([CanBeNull] IUserSession session = null, bool disposeSessionOnDisposeKeeper = false)
  {
    return session != null ? (ISessionKeeper) new UserSessionKeeper.SessionWrapper(session, disposeSessionOnDisposeKeeper) : (ISessionKeeper) new UserSessionKeeper.SessionKeeperWrapper();
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ISessionKeeper Get(
    [CanBeNull] SessionKeeper sessionKeeper,
    bool disposeSessionKeeperOnDisposeISessionKeeper = false)
  {
    return sessionKeeper == null ? (ISessionKeeper) new UserSessionKeeper.SessionKeeperWrapper(new SessionKeeper(), true) : (ISessionKeeper) new UserSessionKeeper.SessionKeeperWrapper(sessionKeeper, disposeSessionKeeperOnDisposeISessionKeeper);
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ISessionKeeper Get(
    [CanBeNull] IDBSessionable sessionable,
    bool disposeSessionKeeperOnDisposeISessionKeeper = false)
  {
    return sessionable == null ? (ISessionKeeper) new UserSessionKeeper.SessionKeeperWrapper() : (ISessionKeeper) new UserSessionKeeper.SessionableKeeper(sessionable, disposeSessionKeeperOnDisposeISessionKeeper);
  }

  private class SessionWrapper : ISessionKeeper, IDisposable
  {
    [CanBeNull]
    private volatile IUserSession _session;
    [NotNull]
    private readonly object _syncObj = new object();
    private readonly bool _disposeSessionOnDisposeWrapper;

    public SessionWrapper([NotNull] IUserSession session, bool disposeSessionOnDisposeWrapper = false)
    {
      this._session = session;
      this._disposeSessionOnDisposeWrapper = disposeSessionOnDisposeWrapper;
    }

    [NotNull]
    public IUserSession Session
    {
      [MustUseReturnValue, MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionWrapper>((object) this._session);
        lock (this._syncObj)
        {
          Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionWrapper>((object) this._session);
          return this._session;
        }
      }
    }

    public void Dispose()
    {
      Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionWrapper>((object) this._session);
      lock (this._syncObj)
      {
        IUserSession notNullRef = Interlocked.Exchange<IUserSession>(ref this._session, (IUserSession) null);
        Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionWrapper>((object) notNullRef);
        if (!this._disposeSessionOnDisposeWrapper || !(notNullRef is IDisposable disposable))
          return;
        disposable.Dispose();
      }
    }
  }

  private class SessionKeeperWrapper : ISessionKeeper, IDisposable
  {
    [CanBeNull]
    private volatile SessionKeeper _sk;
    [NotNull]
    private readonly object _syncObj = new object();
    private readonly bool _disposeKeeperOnDisposeWrapper;

    public SessionKeeperWrapper()
    {
      this._sk = new SessionKeeper();
      this._disposeKeeperOnDisposeWrapper = true;
    }

    public SessionKeeperWrapper([NotNull] SessionKeeper sk, bool disposeKeeperOnDisposeWrapper)
    {
      this._sk = sk;
      this._disposeKeeperOnDisposeWrapper = disposeKeeperOnDisposeWrapper;
    }

    [NotNull]
    public IUserSession Session
    {
      [MustUseReturnValue, MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionKeeperWrapper>((object) this._sk);
        lock (this._syncObj)
        {
          Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionKeeperWrapper>((object) this._sk);
          return this._sk.Session;
        }
      }
    }

    public void Dispose()
    {
      Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionKeeperWrapper>((object) this._sk);
      lock (this._syncObj)
      {
        Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionKeeperWrapper>((object) this._sk);
        SessionKeeper sessionKeeper = Interlocked.Exchange<SessionKeeper>(ref this._sk, (SessionKeeper) null);
        if (!this._disposeKeeperOnDisposeWrapper || sessionKeeper == null)
          return;
        sessionKeeper.Dispose();
      }
    }
  }

  private class SessionableKeeper : ISessionKeeper, IDisposable
  {
    [NotNull]
    private volatile IDBSessionable _sessionable;
    [NotNull]
    private readonly object _syncObj = new object();
    private readonly bool _disposeSessionableOnDisposeWrapper;

    public SessionableKeeper([NotNull] IDBSessionable sessionable, bool disposeSessionableOnDisposeWrapper = false)
    {
      this._sessionable = sessionable;
      this._disposeSessionableOnDisposeWrapper = disposeSessionableOnDisposeWrapper;
    }

    [NotNull]
    public IUserSession Session
    {
      [MustUseReturnValue, MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionableKeeper>((object) this._sessionable);
        lock (this._syncObj)
        {
          Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionableKeeper>((object) this._sessionable);
          return this._sessionable.Session;
        }
      }
    }

    public void Dispose()
    {
      Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionableKeeper>((object) this._sessionable);
      lock (this._syncObj)
      {
        IDBSessionable notNullRef = Interlocked.Exchange<IDBSessionable>(ref this._sessionable, (IDBSessionable) null);
        Intermech.Diagnostics.Check.NotDisposed<UserSessionKeeper.SessionableKeeper>((object) notNullRef);
        if (!this._disposeSessionableOnDisposeWrapper || !(notNullRef is IDisposable disposable))
          return;
        disposable.Dispose();
      }
    }
  }
}
