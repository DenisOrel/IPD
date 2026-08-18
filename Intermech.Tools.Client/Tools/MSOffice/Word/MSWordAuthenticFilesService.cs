// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordAuthenticFilesService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class MSWordAuthenticFilesService(IIntegrator owner) : 
  IntegratorService(owner),
  IAuthenticFilesService
{
  private IApplicationApiService _apiService;

  public IApplicationApiService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this._apiService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this._apiService = value;
      }
    }
  }

  public ICollection<string> GetPossibleFileTypes(int documentType)
  {
    return (ICollection<string>) new List<string>()
    {
      ".pdf"
    };
  }

  public string MakeFilePath(string documentFilePath, string authenticFileType)
  {
    if (documentFilePath == null)
      throw new ArgumentNullException(nameof (documentFilePath));
    if (authenticFileType == null)
      throw new ArgumentNullException(nameof (authenticFileType));
    this.RequireReadyState();
    return documentFilePath + authenticFileType;
  }

  public void MakeFile(string documentFilePath, string authenticFilePath)
  {
    if (documentFilePath == null)
      throw new ArgumentNullException(nameof (documentFilePath));
    if (authenticFilePath == null)
      throw new ArgumentNullException(nameof (authenticFilePath));
    this.RequireReadyState();
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this._apiService))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (MSWordAuthenticFilesService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__0, typeof (MSWordApiHelper), application, documentFilePath);
      if (obj1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        if (MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Open", (IEnumerable<Type>) null, typeof (MSWordAuthenticFilesService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string, object> target = MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__2.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string, object>> p2 = MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__2;
        // ISSUE: reference to a compiler-generated field
        if (MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (MSWordAuthenticFilesService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__1.Target((CallSite) MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__1, application);
        string str = documentFilePath;
        obj1 = target((CallSite) p2, obj2, str);
      }
      object obj3 = obj1;
      // ISSUE: reference to a compiler-generated field
      if (MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__3 = CallSite<Action<CallSite, object, string, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ExportAsFixedFormat", (IEnumerable<Type>) null, typeof (MSWordAuthenticFilesService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__3.Target((CallSite) MSWordAuthenticFilesService.\u003C\u003Eo__6.\u003C\u003Ep__3, obj3, authenticFilePath, 17);
    }
  }
}
