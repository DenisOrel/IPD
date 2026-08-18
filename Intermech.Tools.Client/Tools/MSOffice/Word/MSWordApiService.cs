// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordApiService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class MSWordApiService(IIntegrator owner) : MsoApiService(owner, "Microsoft Word", "Word.Application")
{
  private IMsoApiResourceTracker apiResourceTracker;

  protected override IOpenDocument FindOpenDocument(string fullPath)
  {
    object applicationObject = this.GetApplicationObject();
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, OpenComDocument>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (OpenComDocument), typeof (MSWordApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, OpenComDocument> target = MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, OpenComDocument>> p1 = MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, nameof (FindOpenDocument), (IEnumerable<Type>) null, typeof (MSWordApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) MSWordApiService.\u003C\u003Eo__1.\u003C\u003Ep__0, typeof (MSWordApiHelper), applicationObject, fullPath);
    return (IOpenDocument) target((CallSite) p1, obj);
  }

  protected override IOpenDocument OpenDocument(string fullPath)
  {
    object applicationObject = this.GetApplicationObject();
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, OpenComDocument>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (OpenComDocument), typeof (MSWordApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, OpenComDocument> target = MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, OpenComDocument>> p1 = MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, IMsoApiResourceTracker, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, nameof (OpenDocument), (IEnumerable<Type>) null, typeof (MSWordApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) MSWordApiService.\u003C\u003Eo__2.\u003C\u003Ep__0, typeof (MSWordApiHelper), applicationObject, fullPath, this.apiResourceTracker);
    return (IOpenDocument) target((CallSite) p1, obj);
  }

  protected override ApplicationApiResourceManager TryCreateApiResourceManager(
    object applicationObject)
  {
    return (ApplicationApiResourceManager) new MSWordApiResourceManager(applicationObject);
  }

  protected override void UpdateApiResourceManagerReferences(object applicationObject)
  {
    base.UpdateApiResourceManagerReferences(applicationObject);
    if (this.ApiResourceManager is MSWordApiResourceManager apiResourceManager)
      this.apiResourceTracker = (IMsoApiResourceTracker) apiResourceManager;
    else
      this.apiResourceTracker = (IMsoApiResourceTracker) null;
  }

  protected override void EnsureApplicationWindowIsAvailableToUser(object applicationObject)
  {
    base.EnsureApplicationWindowIsAvailableToUser(applicationObject);
    MSWordApiHelper.EnsureApplicationWindowIsAvailableToUser(applicationObject);
  }
}
