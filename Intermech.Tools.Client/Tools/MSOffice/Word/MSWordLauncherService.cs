// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordLauncherService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class MSWordLauncherService(IIntegrator owner) : 
  ApplicationLauncherService(owner),
  IApplicationLauncherService
{
  protected override void DoLaunchApplication()
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.Integrator))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Visible", typeof (MSWordLauncherService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__0, application, true);
      // ISSUE: reference to a compiler-generated field
      if (MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Activate", (IEnumerable<Type>) null, typeof (MSWordLauncherService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) MSWordLauncherService.\u003C\u003Eo__1.\u003C\u003Ep__1, application);
    }
  }
}
