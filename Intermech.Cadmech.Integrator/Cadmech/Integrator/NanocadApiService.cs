// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.NanocadApiService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies.COM;
using Intermech.Runtime.ComInterop;
using Intermech.Tools.Integrators;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class NanocadApiService(
  IIntegrator owner,
  string applicationName,
  ComObjectProvider comObjectProvider) : CadApiService(owner, applicationName, comObjectProvider)
{
  protected override CadProxy DoCreateCADSystemProxy(object rawCADSystem)
  {
    // ISSUE: reference to a compiler-generated field
    if (NanocadApiService.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadApiService.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, NanocadProxy>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (NanocadApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (CadProxy) NanocadApiService.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) NanocadApiService.\u003C\u003Eo__1.\u003C\u003Ep__0, typeof (NanocadProxy), rawCADSystem, this.ApplicationName);
  }
}
