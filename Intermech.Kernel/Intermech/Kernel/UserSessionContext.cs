// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionContext
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.ControlFlow;
using Intermech.Interfaces;
using System;


namespace Intermech.Kernel;

public static class UserSessionContext
{
  internal static readonly DynamicVariable<bool> ContextCreated = new DynamicVariable<bool>("UserSessionContext.ContextCreated", false);
  internal static readonly DynamicVariable<Guid> CapturedSessionGuid = new DynamicVariable<Guid>("UserSessionContext.CapturedSessionGuid", Guid.Empty);

  public static IDisposable CaptureSession(Guid sessionGuid)
  {
    if (sessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (UserSessionContext.ContextCreated.Value && UserSessionContext.CapturedSessionGuid.Value != sessionGuid)
      throw new InvalidOperationException("Повторый вызов метода CaptureSession недопустим.");
    DynamicScope dynamicScope = new DynamicScope();
    UserSessionContext.ContextCreated.Declare(true);
    UserSessionContext.CapturedSessionGuid.Declare(sessionGuid);
    return (IDisposable) dynamicScope;
  }

  public static IDisposable CaptureSession(IDBSessionable dbSessionable)
  {
    if (dbSessionable == null)
      throw new ArgumentNullException(nameof (dbSessionable));
    return UserSessionContext.CaptureSession(dbSessionable.Session.SessionGUID);
  }

  public static IDisposable CaptureSession(IUserSession session)
  {
    return session != null ? UserSessionContext.CaptureSession(session.SessionGUID) : throw new ArgumentNullException(nameof (session));
  }

  public static IDisposable CaptureSystemSession()
  {
    if (UserSessionContext.ContextCreated.Value && UserSessionContext.CapturedSessionGuid.Value != Guid.Empty)
      throw new InvalidOperationException("Повторый вызов метода CaptureSession недопустим.");
    DynamicScope dynamicScope = new DynamicScope();
    UserSessionContext.ContextCreated.Declare(true);
    return (IDisposable) dynamicScope;
  }
}
