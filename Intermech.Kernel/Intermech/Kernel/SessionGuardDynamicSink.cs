// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SessionGuardDynamicSink
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Remoting;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;


namespace Intermech.Kernel;

internal sealed class SessionGuardDynamicSink : SessionGuardRemotingValidator, IDynamicMessageSink
{
  private static readonly ConcurrentDictionary<MethodBase, bool> methodCache = new ConcurrentDictionary<MethodBase, bool>();
  private static readonly Type SessionGuardAttributeType = typeof (SessionGuardAttribute);
  private static readonly Type MarshalByRefObjectType = typeof (MarshalByRefObject);
  private readonly IRemotingObjectResolver mbrResolver;

  public SessionGuardDynamicSink(IRemotingObjectResolver mbrResolver)
  {
    this.mbrResolver = mbrResolver;
  }

  public void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync)
  {
    this.ValidateClientCall(reqMsg);
  }

  public void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync)
  {
  }

  private void ValidateClientCall(IMessage reqMsg)
  {
    if (!(reqMsg is IMethodMessage message) || message.MethodBase.DeclaringType == (Type) null)
      return;
    MarshalByRefObject marshalByRefObject = this.mbrResolver.TryGetObject(message.Uri);
    switch (marshalByRefObject)
    {
      case IUserSession _:
        this.ValidateCall((IUserSession) marshalByRefObject, message);
        break;
      case IDBSessionable _:
        this.ValidateCall(((IDBSessionable) marshalByRefObject).Session, message);
        break;
    }
  }

  protected override bool CanValidateCall(IUserSession userSession, IMethodMessage mm)
  {
    return base.CanValidateCall(userSession, mm) && this.IsDBSessionableCall(mm);
  }

  private bool IsDBSessionableCall(IMethodMessage mm)
  {
    return SessionGuardDynamicSink.methodCache.GetOrAdd(mm.MethodBase, (Func<MethodBase, bool>) (methodBase => this.IsMethodMarkedAsGuarded(mm) && !this.IsRemotingInfrastructureCall(mm)));
  }

  private bool IsRemotingInfrastructureCall(IMethodMessage mm)
  {
    return Type.GetType(mm.TypeName, false) == SessionGuardDynamicSink.MarshalByRefObjectType;
  }

  private bool IsMethodMarkedAsGuarded(IMethodMessage mm)
  {
    if (this.IsTargetTypeGuarded(mm))
    {
      SessionGuardMode? guardMode = SessionGuardDynamicSink.GetGuardMode((ICustomAttributeProvider) mm.MethodBase);
      if (!guardMode.HasValue || guardMode.Value != SessionGuardMode.Disabled)
        return true;
    }
    return false;
  }

  private bool IsTargetTypeGuarded(IMethodMessage mm)
  {
    SessionGuardMode? guardMode = SessionGuardDynamicSink.GetGuardMode((ICustomAttributeProvider) mm.MethodBase.DeclaringType);
    if (guardMode.HasValue && guardMode.Value != SessionGuardMode.Disabled)
      return true;
    switch (this.mbrResolver.TryGetObject(mm.Uri))
    {
      case UserSession _:
      case DBSessionable _:
        return true;
      default:
        return false;
    }
  }

  private static SessionGuardMode? GetGuardMode(ICustomAttributeProvider element)
  {
    object[] customAttributes = element.GetCustomAttributes(SessionGuardDynamicSink.SessionGuardAttributeType, true);
    return customAttributes != null && customAttributes.Length != 0 ? new SessionGuardMode?(((SessionGuardAttribute) customAttributes[0]).Mode) : new SessionGuardMode?();
  }
}
