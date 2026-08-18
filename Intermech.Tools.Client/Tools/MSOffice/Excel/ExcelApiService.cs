// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelApiService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelApiService(IIntegrator owner) : MsoApiService(owner, "Microsoft Excel", "Excel.Application")
{
  private IMsoApiResourceTracker apiResourceTracker;

  protected override void DoTestApplicationObject(object applicationObject)
  {
    base.DoTestApplicationObject(applicationObject);
    object obj1 = applicationObject;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (ExcelApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Visible", typeof (ExcelApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__0, obj1);
    bool flag = target1((CallSite) p1, obj2);
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (int), typeof (ExcelApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, int> target2 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, int>> p4 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (ExcelApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, object> target3 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, object>> p3 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Workbooks", typeof (ExcelApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__2.Target((CallSite) ExcelApiService.\u003C\u003Eo__1.\u003C\u003Ep__2, obj1);
    object obj4 = target3((CallSite) p3, obj3);
    int num = target2((CallSite) p4, obj4);
    if (!flag && num == 0)
      throw new Exception("COM-object is dead.");
  }

  protected override void DoReleaseApplicationObject(object applicationObject)
  {
    base.DoReleaseApplicationObject(applicationObject);
    Marshal.FinalReleaseComObject(applicationObject);
    Process process = ((IEnumerable<Process>) Process.GetProcessesByName("excel")).FirstOrDefault<Process>();
    if (process == null)
      return;
    try
    {
      process.Kill();
    }
    catch
    {
    }
    finally
    {
      process.Dispose();
    }
  }

  protected override IOpenDocument FindOpenDocument(string fullPath)
  {
    object applicationObject = this.GetApplicationObject();
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, OpenComDocument>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (OpenComDocument), typeof (ExcelApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, OpenComDocument> target = ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, OpenComDocument>> p1 = ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, nameof (FindOpenDocument), (IEnumerable<Type>) null, typeof (ExcelApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) ExcelApiService.\u003C\u003Eo__3.\u003C\u003Ep__0, typeof (ExcelApiHelper), applicationObject, fullPath);
    return (IOpenDocument) target((CallSite) p1, obj);
  }

  protected override IOpenDocument OpenDocument(string fullPath)
  {
    object applicationObject = this.GetApplicationObject();
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, OpenComDocument>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (OpenComDocument), typeof (ExcelApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, OpenComDocument> target = ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, OpenComDocument>> p1 = ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, IMsoApiResourceTracker, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, nameof (OpenDocument), (IEnumerable<Type>) null, typeof (ExcelApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) ExcelApiService.\u003C\u003Eo__4.\u003C\u003Ep__0, typeof (ExcelApiHelper), applicationObject, fullPath, this.apiResourceTracker);
    return (IOpenDocument) target((CallSite) p1, obj);
  }

  protected override ApplicationApiResourceManager TryCreateApiResourceManager(
    object applicationObject)
  {
    return (ApplicationApiResourceManager) new ExcelApiResourceManager(applicationObject);
  }

  protected override void UpdateApiResourceManagerReferences(object applicationObject)
  {
    base.UpdateApiResourceManagerReferences(applicationObject);
    if (this.ApiResourceManager is ExcelApiResourceManager apiResourceManager)
      this.apiResourceTracker = (IMsoApiResourceTracker) apiResourceManager;
    else
      this.apiResourceTracker = (IMsoApiResourceTracker) null;
  }

  protected override void EnsureApplicationWindowIsAvailableToUser(object applicationObject)
  {
    base.EnsureApplicationWindowIsAvailableToUser(applicationObject);
    ExcelApiHelper.EnsureApplicationWindowIsAvailableToUser(applicationObject);
  }
}
