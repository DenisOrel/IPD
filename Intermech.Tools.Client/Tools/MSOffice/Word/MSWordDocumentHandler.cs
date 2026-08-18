// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordDocumentHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Search.MSOfficeAddins;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;
using Intermech.Tools.Integrators.Simple;
using Intermech.Tools.MSOffice.Excel;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class MSWordDocumentHandler(
  DocumentCaptureChangesDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : SingleFileDocumentHandler(driver, ctx, docItem)
{
  protected override void ProcessRelations()
  {
    base.ProcessRelations();
    this.SynchronizeObjectsReferencesInDocumentWithDocumentComposition();
  }

  protected override void SaveModifiedDocument(IOpenDocument document)
  {
    this.RecalculateMacroFields((OpenComDocument) document);
    base.SaveModifiedDocument(document);
  }

  protected override void UpdateDBOnlyAttributes()
  {
    base.UpdateDBOnlyAttributes();
    this.SynchronizePageCount();
  }

  private void RecalculateMacroFields(OpenComDocument document)
  {
    if (!((MSWordIntegratorSettings) this.settingsSvc.GetSettingsObject()).RunAutoOpenMacro || !this.DocumentAttributes.EmbeddedSet.Bag.HasChanges && !this.DocumentAttributes.DatabaseSet.HasChanges)
      return;
    // ISSUE: reference to a compiler-generated field
    if (MSWordDocumentHandler.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordDocumentHandler.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RunAutoMacro", (IEnumerable<Type>) null, typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MSWordDocumentHandler.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) MSWordDocumentHandler.\u003C\u003Eo__4.\u003C\u003Ep__0, document.ComObject, 2);
  }

  private void SynchronizePageCount()
  {
    string attributeTypeName = MSOfficeAddinsConstants.PagesAttributeTypeName;
    if (!this.GetSynchronizedAttributes().Contains((StringKey) attributeTypeName))
      return;
    ValueBag databaseSet = this.DocumentAttributes.DatabaseSet;
    if (databaseSet.Read<long>((StringKey) attributeTypeName, -1L) != -1L)
      return;
    databaseSet.Update((StringKey) attributeTypeName, (object) this.GetPageCount());
  }

  private void SynchronizeObjectsReferencesInDocumentWithDocumentComposition()
  {
    if (!((MSWordIntegratorSettings) this.settingsSvc.GetSettingsObject()).SynchronizeObjectsReferencesInDocumentWithDocumentComposition)
      return;
    try
    {
      ExcelDocumentHandler.SynchronizeDocumentCompositionWithObjectsFromUrls(this.DocumentObject.ObjectId, this.GetObjectsUrls());
    }
    catch
    {
    }
  }

  private string[] GetObjectsUrls()
  {
    List<string> stringList = new List<string>();
    object comObject = ((OpenComDocument) this.DocumentEntity.Sections.Get<IOpenDocument>()).ComObject;
    // ISSUE: reference to a compiler-generated field
    if (MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "AcceptAllRevisions", (IEnumerable<Type>) null, typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0, comObject);
    // ISSUE: reference to a compiler-generated field
    if (MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (MSWordDocumentHandler)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, IEnumerable> target1 = MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, IEnumerable>> p5 = MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5;
    // ISSUE: reference to a compiler-generated field
    if (MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Hyperlinks", typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1, comObject);
    foreach (object obj2 in target1((CallSite) p5, obj1))
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target2 = MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p3 = MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2.Target((CallSite) MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2, obj2, (object) null);
      if (target2((CallSite) p3, obj3))
      {
        // ISSUE: reference to a compiler-generated field
        if (MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Address", typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        string url = MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4.Target((CallSite) MSWordDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4, obj2) as string;
        if (!string.IsNullOrEmpty(url) && MSOfficeAddinsHelper.IsObjectUrl(url))
          stringList.Add(url);
      }
    }
    return stringList.ToArray();
  }

  private long GetPageCount()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (long), typeof (MSWordDocumentHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, long> target1 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, long>> p6 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__5 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToInt64", (IEnumerable<Type>) null, typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target2 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p5 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__5;
      Type type = typeof (Convert);
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p4 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Pages", typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target4 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p3 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target5 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p2 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Panes", typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target6 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveWindow", typeof (MSWordDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) MSWordDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0, ((OpenComDocument) this.DocumentEntity.Sections.Get<IOpenDocument>()).ComObject);
      object obj2 = target6((CallSite) p1, obj1);
      object obj3 = target5((CallSite) p2, obj2, 1);
      object obj4 = target4((CallSite) p3, obj3);
      object obj5 = target3((CallSite) p4, obj4);
      object obj6 = target2((CallSite) p5, type, obj5);
      return target1((CallSite) p6, obj6);
    }
    catch
    {
      return -1;
    }
  }
}
