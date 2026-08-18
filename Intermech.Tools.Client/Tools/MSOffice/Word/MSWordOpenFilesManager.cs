// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordOpenFilesManager
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class MSWordOpenFilesManager : OpenFilesApiResourceManager
{
  private readonly object applicationObject;

  public MSWordOpenFilesManager(object applicationObject)
  {
    this.applicationObject = applicationObject != null ? applicationObject : throw new ArgumentNullException(nameof (applicationObject));
  }

  protected override void DoCloseFileIfOpen(string fullPath)
  {
    object openFile = MSWordApiHelper.FindOpenFile(this.applicationObject, fullPath);
    // ISSUE: reference to a compiler-generated field
    if (MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordOpenFilesManager), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MSWordOpenFilesManager), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__0, openFile, (object) null);
    if (!target((CallSite) p1, obj))
      return;
    // ISSUE: reference to a compiler-generated field
    if (MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Action<CallSite, object, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (MSWordOpenFilesManager), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__2.Target((CallSite) MSWordOpenFilesManager.\u003C\u003Eo__1.\u003C\u003Ep__2, openFile, false);
  }
}
