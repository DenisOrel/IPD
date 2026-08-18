// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MSOffice.Word.DocsComparison.DocsComparisonCommandsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using Intermech.Tools.Integrators;
using Intermech.Tools.MSOffice.Word;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.MSOffice.Word.DocsComparison;

internal sealed class DocsComparisonCommandsProvider
{
  private IFileVault fileVault;
  private IntegratorObject msWordIntegratorRef;

  public DocsComparisonCommandsProvider(IFileVault fileVault)
  {
    this.fileVault = fileVault != null ? fileVault : throw new ArgumentNullException(nameof (fileVault));
    this.msWordIntegratorRef = MSWordConsts.IntegratorRef;
  }

  private void CompareDocs(
    DocsComparisonCommandsProvider.PublishedDocuments publishedDocs)
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(IntegratorServices.GetService<IApplicationApiService>(this.msWordIntegratorRef, true)))
    {
      // ISSUE: reference to a compiler-generated field
      if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p2 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Visible", typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__0, msWordApiSession.Application);
      object obj2 = target2((CallSite) p1, obj1);
      if (target1((CallSite) p2, obj2))
      {
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Visible", typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__3.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__3, msWordApiSession.Application, true);
      }
      // ISSUE: reference to a compiler-generated field
      if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Activate", (IEnumerable<System.Type>) null, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__4.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__4, msWordApiSession.Application);
      object obj4 = (object) null;
      object obj5 = (object) null;
      try
      {
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__5 = CallSite<Func<CallSite, System.Type, object, string, bool, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "OpenFile", (IEnumerable<System.Type>) null, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj4 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__5.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__5, typeof (MSWordApiHelper), msWordApiSession.Application, publishedDocs.OriginalDocument.FullName, false, (object) null);
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__6 = CallSite<Func<CallSite, System.Type, object, string, bool, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "OpenFile", (IEnumerable<System.Type>) null, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj5 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__6.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__6, typeof (MSWordApiHelper), msWordApiSession.Application, publishedDocs.RevisedDocument.FullName, false, (object) null);
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "CompareDocuments", (IEnumerable<System.Type>) null, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__7.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__7, msWordApiSession.Application, obj4, obj5);
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__8 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Activate", (IEnumerable<System.Type>) null, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__8.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__8, obj6);
      }
      finally
      {
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target3 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__10.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p10 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__10;
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__9.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__9, obj4, (object) null);
        if (target3((CallSite) p10, obj7))
        {
          // ISSUE: reference to a compiler-generated field
          if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__11 == null)
          {
            // ISSUE: reference to a compiler-generated field
            DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__11 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<System.Type>) null, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__11.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__11, obj4);
        }
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target4 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__13.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p13 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__13;
        // ISSUE: reference to a compiler-generated field
        if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__12.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__12, obj5, (object) null);
        if (target4((CallSite) p13, obj8))
        {
          // ISSUE: reference to a compiler-generated field
          if (DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__14 == null)
          {
            // ISSUE: reference to a compiler-generated field
            DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__14 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<System.Type>) null, typeof (DocsComparisonCommandsProvider), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__14.Target((CallSite) DocsComparisonCommandsProvider.\u003C\u003Eo__3.\u003C\u003Ep__14, obj5);
        }
      }
    }
  }

  internal void CompareObjects(
    DocsComparisonCommandsProvider.ObjectInfo originalObject,
    DocsComparisonCommandsProvider.ObjectInfo revisedObject)
  {
    PublishedFile publishedDocument1 = this.GetPublishedDocument(originalObject.Id);
    if (publishedDocument1 == null)
    {
      this.ShowMissingFileMessageBox(originalObject.Caption);
    }
    else
    {
      PublishedFile publishedDocument2 = this.GetPublishedDocument(revisedObject.Id);
      if (publishedDocument2 == null)
        this.ShowMissingFileMessageBox(revisedObject.Caption);
      else if (originalObject.Id == -revisedObject.Id && publishedDocument1.FullName == publishedDocument2.FullName)
      {
        long objectId = Math.Abs(originalObject.Id);
        this.ShowIdenticalFilesMessageBox(originalObject.Caption, objectId);
      }
      else
        this.CompareDocs(new DocsComparisonCommandsProvider.PublishedDocuments(publishedDocument1, publishedDocument2));
    }
  }

  private PublishedFile GetPublishedDocument(long objectId)
  {
    return this.fileVault.ViewArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForSingleObject(objectId)).MasterFile;
  }

  private void ShowMissingFileMessageBox(string objectCaption)
  {
    int num = (int) MessageBox.Show($"Невозможно сравнить документы: объект \"{objectCaption}\" не содержит файлов.", "Сравнение документов MS Word", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void ShowIdenticalFilesMessageBox(string objectCaption, long objectId)
  {
    int num = (int) MessageBox.Show($"Файлы документа \"{objectCaption}\" (ид. версии = {objectId}) идентичны.", "Сравнение документов MS Word", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  internal class ObjectInfo
  {
    public ObjectInfo(string caption, long id)
    {
      this.Caption = caption;
      this.Id = id;
    }

    public string Caption { get; }

    public long Id { get; }
  }

  private class PublishedDocuments
  {
    public PublishedDocuments(PublishedFile originalDocument, PublishedFile revisedDocument)
    {
      this.OriginalDocument = originalDocument;
      this.RevisedDocument = revisedDocument;
    }

    public PublishedFile OriginalDocument { get; }

    public PublishedFile RevisedDocument { get; }
  }
}
