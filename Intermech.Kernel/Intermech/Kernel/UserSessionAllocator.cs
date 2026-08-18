// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionAllocator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Kernel;

public class UserSessionAllocator : IUserSessionAllocator
{
  private Dictionary<int, UserSessionAllocator.Descriptor> activeSessions;

  public UserSessionAllocator()
  {
    this.activeSessions = new Dictionary<int, UserSessionAllocator.Descriptor>(32 /*0x20*/);
  }

  public IUserSessionDescriptor Allocate()
  {
    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
    lock (this.activeSessions)
    {
      UserSessionAllocator.Descriptor descriptor;
      if (!this.activeSessions.TryGetValue(managedThreadId, out descriptor))
      {
        descriptor = this.AllocateCapturedSession(managedThreadId);
        this.activeSessions.Add(managedThreadId, descriptor);
      }
      ++descriptor.UsageCount;
      return (IUserSessionDescriptor) descriptor;
    }
  }

  private UserSessionAllocator.Descriptor AllocateCapturedSession(int currentThreadKey)
  {
    if (UserSessionContext.ContextCreated.Value)
    {
      Guid sessionGUID = UserSessionContext.CapturedSessionGuid.Value;
      return sessionGUID != Guid.Empty ? new UserSessionAllocator.Descriptor(currentThreadKey, UserSession.GetSessionByID(sessionGUID), false) : this.AllocateSystemSession(currentThreadKey);
    }
    if (this.IsServerCodeCall())
      return this.AllocateSystemSession(currentThreadKey);
    throw new InvalidOperationException("Воспользуйтесь одним из методов класса UserSessionContext для создания контекста, внутри которого допустимо использование SessionKeeper.");
  }

  private UserSessionAllocator.Descriptor AllocateSystemSession(int threadKey)
  {
    IUserSession sessionTemporaryClone = ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true).GetSystemSessionTemporaryClone("KernelSessionAllocator");
    return new UserSessionAllocator.Descriptor(threadKey, sessionTemporaryClone, true);
  }

  private bool IsServerCodeCall() => IPSPrincipal.CurrentPrincipal.IsInRole(IPSBuiltInRole.Server);

  public void Release(IUserSessionDescriptor descriptor)
  {
    UserSessionAllocator.Descriptor descriptor1 = descriptor != null ? (UserSessionAllocator.Descriptor) descriptor : throw new ArgumentNullException(nameof (descriptor));
    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
    if (!object.Equals((object) descriptor1.ThreadKey, (object) managedThreadId))
      throw new InvalidOperationException("Невозможно освободить сессию, так как она была выделена для другого потока приложения.");
    lock (this.activeSessions)
    {
      UserSessionAllocator.Descriptor descriptor2;
      if (!this.activeSessions.TryGetValue(managedThreadId, out descriptor2) || descriptor2 != descriptor1)
        return;
      --descriptor1.UsageCount;
      if (descriptor1.UsageCount != 0)
        return;
      this.activeSessions.Remove(managedThreadId);
      if (!descriptor1.NeedLogout)
        return;
      descriptor1.Session.Logout("KernelSessionAllocator");
    }
  }

  public void Shutdown()
  {
  }

  private sealed class Descriptor : IUserSessionDescriptor
  {
    private readonly int threadKey;
    private readonly IUserSession session;
    private readonly bool needLogout;
    private int usageCount;

    public Descriptor(int threadKey, IUserSession session, bool needLogout)
    {
      this.threadKey = threadKey;
      this.session = session;
      this.needLogout = needLogout;
    }

    public int ThreadKey => this.threadKey;

    public IUserSession Session => this.session;

    public bool NeedLogout => this.needLogout;

    public int UsageCount
    {
      get => this.usageCount;
      set
      {
        this.usageCount = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof (UsageCount));
      }
    }

    public bool IsTopmost
    {
      [DebuggerStepThrough] get => this.usageCount == 1;
    }

    public bool TrySetReleaseMode(UserSessionReleaseMode newReleaseMode)
    {
      return newReleaseMode == UserSessionReleaseMode.Normal;
    }
  }
}
