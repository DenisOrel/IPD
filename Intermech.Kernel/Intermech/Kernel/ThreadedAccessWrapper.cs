// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ThreadedAccessWrapper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Kernel.NotifySamples;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class ThreadedAccessWrapper
{
  private readonly UserSession _session;

  internal ThreadedAccessWrapper(UserSession session) => this._session = session;

  public UserSession Session
  {
    [DebuggerStepThrough] get => this._session;
  }

  public Guid SessionGUID
  {
    [DebuggerStepThrough] get => this._session.SessionGUID;
  }

  public Guid MasterSessionGUID
  {
    [DebuggerStepThrough] get => this._session.MasterSessionGUID;
  }

  public bool IsPermanent
  {
    [DebuggerStepThrough] get => this._session.IsPermanent;
  }

  public string ComputerName
  {
    [DebuggerStepThrough] get => this._session.ComputerName;
  }

  public long UserID
  {
    [DebuggerStepThrough] get => this._session.UserID;
  }

  public string UserName
  {
    [DebuggerStepThrough] get => this._session.UserName;
  }

  public DateTime LastCallTime
  {
    [DebuggerStepThrough] get => this._session.LastCallTime;
  }

  public UserSession ParentSession
  {
    [DebuggerStepThrough] get => this._session.ParentSession;
  }

  public UserSessionStatus SessionStatus
  {
    [DebuggerStepThrough] get => this._session.SessionStatus;
  }

  public bool IsNotLogged
  {
    [DebuggerStepThrough] get => this._session.IsNotLogged;
  }

  public bool IsClosingOrDisposed
  {
    [DebuggerStepThrough] get => this._session.IsClosingOrDisposed;
  }

  public int CallCounter
  {
    [DebuggerStepThrough] get => this._session.CallCounter;
  }

  public long ActiveStorageID
  {
    [DebuggerStepThrough] get => this._session.ActiveStorageID;
  }

  public bool InTransaction
  {
    [DebuggerStepThrough] get
    {
      Thread.MemoryBarrier();
      return this._session.InTransaction;
    }
  }

  public DBSecurity TryGetDBSecurity() => this._session.RaceGetCurrentDBSecurity();

  public NotifySamplesProcessor TryGetNSProcessor() => this._session.RaceGetCurrentNSProcessor();

  public void ModifyActiveStorageID(long oldValue, long newValue)
  {
    this._session.RaceModifyActiveStorageID(oldValue, newValue);
  }

  public bool TrySetClosingState() => this._session.RaceSetClosingState();
}
