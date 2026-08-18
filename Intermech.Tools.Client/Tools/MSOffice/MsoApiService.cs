// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.MsoApiService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice;

internal abstract class MsoApiService : 
  ComApplicationApiService,
  IDocumentApiService,
  IExternalApiService,
  IIntegratorService
{
  private MsoDocumentCodec fileCodec;
  private IApplicationFileTypes fileTypeService;
  private OpenDocumentsApi openDocumentsApi;

  public MsoApiService(IIntegrator owner, string applicationName, string progId)
    : base(owner, applicationName, progId)
  {
  }

  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileTypeService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileTypeService = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
    this.fileCodec = new MsoDocumentCodec();
    this.openDocumentsApi = new OpenDocumentsApi(this.FileTypeService, (IExternalApiService) this);
    this.openDocumentsApi.OnFindOpenDocument += new Func<string, IOpenDocument>(this.FindOpenDocument);
    this.openDocumentsApi.OnOpenDocument += new Func<string, IOpenDocument>(this.OpenDocument);
    this.openDocumentsApi.OnValidateDocument += new Action<IOpenDocument>(this.ValidateDocument);
    this.openDocumentsApi.OnGetDocumentCodec += new Func<IOpenDocument, IAttributeCodec>(this.GetDocumentCodec);
    this.openDocumentsApi.OnGetDocumentAttributeContainer += new Func<IOpenDocument, IValueBagContainer>(this.GetDocumentAttributeContainer);
    this.openDocumentsApi.OnSaveDocument += new Action<IOpenDocument>(this.SaveDocument);
    this.openDocumentsApi.OnCloseDocument += new Action<IOpenDocument>(this.CloseDocument);
  }

  protected override void DoTestApplicationObject(object applicationObject)
  {
    object obj1 = applicationObject;
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MsoApiService)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string> target = MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string>> p1 = MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Version", typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) MsoApiService.\u003C\u003Eo__5.\u003C\u003Ep__0, obj1);
    if (target((CallSite) p1, obj2) == null)
      throw new Exception("COM object is dead.");
  }

  public IOpenDocumentsApi OpenDocuments
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return (IOpenDocumentsApi) this.openDocumentsApi;
    }
  }

  protected abstract IOpenDocument FindOpenDocument(string fullPath);

  protected abstract IOpenDocument OpenDocument(string fullPath);

  private void ValidateDocument(IOpenDocument openDocument)
  {
    if (!(openDocument is OpenComDocument))
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_223"));
  }

  private void SaveDocument(IOpenDocument openDocument)
  {
    OpenComDocument openComDocument = (OpenComDocument) openDocument;
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__6.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p6 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__6;
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, object> target2 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, object>> p1 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Saved", typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__0.Target((CallSite) MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__0, openComDocument.ComObject);
    object obj2 = target2((CallSite) p1, obj1);
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    object obj3;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__5.Target((CallSite) MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__5, obj2))
    {
      // ISSUE: reference to a compiler-generated field
      if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> target3 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> p4 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__4;
      object obj4 = obj2;
      // ISSUE: reference to a compiler-generated field
      if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target4 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p3 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ReadOnly", typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__2.Target((CallSite) MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__2, openComDocument.ComObject);
      object obj6 = target4((CallSite) p3, obj5);
      obj3 = target3((CallSite) p4, obj4, obj6);
    }
    else
      obj3 = obj2;
    if (!target1((CallSite) p6, obj3))
      return;
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__7 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Save", (IEnumerable<Type>) null, typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__7.Target((CallSite) MsoApiService.\u003C\u003Eo__11.\u003C\u003Ep__7, openComDocument.ComObject);
  }

  private void CloseDocument(IOpenDocument openDocument)
  {
    OpenComDocument openComDocument = (OpenComDocument) openDocument;
    // ISSUE: reference to a compiler-generated field
    if (MsoApiService.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MsoApiService.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (MsoApiService), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MsoApiService.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) MsoApiService.\u003C\u003Eo__12.\u003C\u003Ep__0, openComDocument.ComObject);
  }

  private IAttributeCodec GetDocumentCodec(IOpenDocument openDocument)
  {
    return (IAttributeCodec) this.fileCodec;
  }

  private IValueBagContainer GetDocumentAttributeContainer(IOpenDocument openDocument)
  {
    return (IValueBagContainer) openDocument;
  }
}
