// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelDocumentHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Search.MSOfficeAddins;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;
using Intermech.Tools.Integrators.Simple;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class ExcelDocumentHandler(
  DocumentCaptureChangesDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : SingleFileDocumentHandler(driver, ctx, docItem)
{
  public static void SynchronizeDocumentCompositionWithObjectsFromUrls(
    long documentVersionID,
    string[] objectUrls)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Tuple<long, string>[] source = ((IMSOfficeAddinsServerService) sessionKeeper.Session.GetCustomService(typeof (IMSOfficeAddinsServerService))).SynchronizeDocumentCompositionWithObjectsFromUrls(sessionKeeper.Session.SessionGUID, documentVersionID, objectUrls);
      if (source.Length == 0)
        return;
      int num = (int) MessageBox.Show($"Следующие объекты, ссылки на которые присутствуют в документе, не удалось добавить в состав документа:{Environment.NewLine}{string.Join(Environment.NewLine + ";", ((IEnumerable<Tuple<long, string>>) source).Select<Tuple<long, string>, string>((Func<Tuple<long, string>, string>) (o => o.Item2 ?? "")))}.", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

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
    this.DocumentAttributes.DatabaseSet.Update((StringKey) MSOfficeAddinsConstants.PagesAttributeTypeName, (object) this.GetSheetsCount());
  }

  private void RecalculateMacroFields(OpenComDocument document)
  {
    if (!((ExcelIntegratorSettings) this.settingsSvc.GetSettingsObject()).RunAutoOpenMacro || !this.DocumentAttributes.EmbeddedSet.Bag.HasChanges && !this.DocumentAttributes.DatabaseSet.HasChanges)
      return;
    // ISSUE: reference to a compiler-generated field
    if (ExcelDocumentHandler.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelDocumentHandler.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RunAutoMacros", (IEnumerable<System.Type>) null, typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ExcelDocumentHandler.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) ExcelDocumentHandler.\u003C\u003Eo__5.\u003C\u003Ep__0, document.ComObject, 1);
  }

  private void SynchronizeObjectsReferencesInDocumentWithDocumentComposition()
  {
    if (!((ExcelIntegratorSettings) this.settingsSvc.GetSettingsObject()).SynchronizeObjectsReferencesInDocumentWithDocumentComposition)
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
    // ISSUE: reference to a compiler-generated field
    if (ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (ExcelDocumentHandler)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, IEnumerable> target1 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__6.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, IEnumerable>> p6 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__6;
    // ISSUE: reference to a compiler-generated field
    if (ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Sheets", typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__0, ((OpenComDocument) this.DocumentEntity.Sections.Get<IOpenDocument>()).ComObject);
    foreach (object obj2 in target1((CallSite) p6, obj1))
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (ExcelDocumentHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IEnumerable> target2 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IEnumerable>> p5 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__5;
      // ISSUE: reference to a compiler-generated field
      if (ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Hyperlinks", typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__1, obj2);
      foreach (object obj4 in target2((CallSite) p5, obj3))
      {
        // ISSUE: reference to a compiler-generated field
        if (ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target3 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p3 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2.Target((CallSite) ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__2, obj4, (object) null);
        if (target3((CallSite) p3, obj5))
        {
          // ISSUE: reference to a compiler-generated field
          if (ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Address", typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          string url = ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4.Target((CallSite) ExcelDocumentHandler.\u003C\u003Eo__7.\u003C\u003Ep__4, obj4) as string;
          if (!string.IsNullOrEmpty(url) && MSOfficeAddinsHelper.IsObjectUrl(url))
            stringList.Add(url);
        }
      }
    }
    return stringList.ToArray();
  }

  private long GetSheetsCount()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, long>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (long), typeof (ExcelDocumentHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, long> target1 = ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, long>> p3 = ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2 = CallSite<Func<CallSite, System.Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToInt64", (IEnumerable<System.Type>) null, typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, System.Type, object, object> target2 = ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, System.Type, object, object>> p2 = ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__2;
      System.Type type = typeof (Convert);
      // ISSUE: reference to a compiler-generated field
      if (ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Sheets", typeof (ExcelDocumentHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) ExcelDocumentHandler.\u003C\u003Eo__8.\u003C\u003Ep__0, ((OpenComDocument) this.DocumentEntity.Sections.Get<IOpenDocument>()).ComObject);
      object obj2 = target3((CallSite) p1, obj1);
      object obj3 = target2((CallSite) p2, type, obj2);
      return target1((CallSite) p3, obj3);
    }
    catch
    {
      return -1;
    }
  }
}
