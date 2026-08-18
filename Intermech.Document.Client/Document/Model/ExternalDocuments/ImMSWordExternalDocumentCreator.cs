// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ExternalDocuments.ImMSWordExternalDocumentCreator
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Signs.Interfaces;
using Intermech.Tools.MSOffice.Word;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Document.Model.ExternalDocuments;

public class ImMSWordExternalDocumentCreator : ExternalDocumentCreator
{
  protected override ImExternalDocument CreateDocument(
    Stream stream,
    IDBObject docObject,
    bool updateLinks)
  {
    ImExternalDocument document = new ImExternalDocument(false);
    document.ExternalDocumentType = "MSWord";
    document.Clear(false, false);
    this.CreatePages(document, docObject, updateLinks);
    DocumentEditorPlugin.UpdateDocumentDBObject((ImDocument) document, docObject.ObjectID, updateLinks, false);
    return document;
  }

  public override void UpdateDocumentDBObject(
    ImExternalDocument doc,
    IDBObject docObject,
    bool updateDocumentLinks)
  {
    doc.Clear(false, false);
    this.CreatePages(doc, docObject, updateDocumentLinks);
    DocumentEditorPlugin.UpdateDocumentDBObject((ImDocument) doc, docObject.ObjectID, updateDocumentLinks, false);
  }

  private void FillAttributes(object msdoc, IDBObject docObject)
  {
  }

  private void CreatePages(ImExternalDocument imdoc, IDBObject docObject, bool updateLinks)
  {
    List<DBObjectState> objectStates = ClientContext.FileVault.DBObjectsInfo.CreateStateListForSingleObject(docObject.ObjectID);
    EventHandler<CanControlFileAttributeEventArgs> eventHandler = (EventHandler<CanControlFileAttributeEventArgs>) ((s, e) =>
    {
      if (e.DBObject != objectStates[0])
        return;
      e.CanControl = false;
    });
    ClientContext.FileVault.ReadOnlyLocalFiles.CanControlAttributeEvent += eventHandler;
    PublishedObject publishedObject = (PublishedObject) null;
    try
    {
      publishedObject = ClientContext.FileVault.ViewArea.Publish((IList<DBObjectState>) objectStates);
    }
    finally
    {
      ClientContext.FileVault.ReadOnlyLocalFiles.CanControlAttributeEvent -= eventHandler;
    }
    string path = (string) null;
    foreach (PublishedFile objectFile in publishedObject.ObjectFiles)
    {
      string @extension = Path.GetExtension(objectFile.FullName).ToLower();
      if (ImDocumentData.ImDocumentExternalFileExtensions.Find((Predicate<string>) (x => @extension.EndsWith(x))) != null)
        path = objectFile.FullName;
    }
    if (path == null || !File.Exists(path))
      throw new Exception("Не найден файл Word");
    MSWordApiSession msWordApiSession = MSWordApiSession.CreateDefault();
    object application = msWordApiSession.Application;
    try
    {
      IDBAttribute attributeById = docObject.GetAttributeByID(DocIDCache.Attr_Format);
      string pageFormat = "A4";
      if (attributeById != null && attributeById.Value != null && !(attributeById.Value is DBNull))
        pageFormat = attributeById.Value.ToString();
      SizeF sizeF = new SizeF(210f, 297f);
      if (pageFormat != null && pageFormat != "")
        sizeF = PageData.GetSizeForPageFormat(pageFormat);
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p2 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Visible", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__0, application);
      object obj2 = target2((CallSite) p1, obj1);
      if (target1((CallSite) p2, obj2))
      {
        int num1 = 2;
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target3 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p7 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__7;
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target4 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p4 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowState", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__3.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__3, application);
        int num2 = num1;
        object obj4 = target4((CallSite) p4, obj3, num2);
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        object obj5;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (!ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__6.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__6, obj4))
        {
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          obj5 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__5.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__5, obj4, !ImDocumentData.ShowDebugInfo);
        }
        else
          obj5 = obj4;
        if (target3((CallSite) p7, obj5))
        {
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj6 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__8.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__8, application, num1);
        }
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Visible", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__9.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__9, application, true);
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target5 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p14 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target6 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p11 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__11;
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowState", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__10.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__10, application);
        int num3 = num1;
        object obj9 = target6((CallSite) p11, obj8, num3);
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        object obj10;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (!ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__13.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__13, obj9))
        {
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__12 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          obj10 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__12.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__12, obj9, !ImDocumentData.ShowDebugInfo);
        }
        else
          obj10 = obj9;
        if (target5((CallSite) p14, obj10))
        {
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__15 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj11 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__15.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__15, application, num1);
        }
      }
      object obj12 = (object) null;
      int num4 = 0;
      while (true)
      {
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__19 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target7 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__19.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p19 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__19;
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj13 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__16.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__16, obj12, (object) null);
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__18 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        object obj14;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (!ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__18.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__18, obj13))
        {
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__17 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          obj14 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__17.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__17, obj13, num4 < 10);
        }
        else
          obj14 = obj13;
        if (target7((CallSite) p19, obj14))
        {
          try
          {
            ++num4;
            // ISSUE: reference to a compiler-generated field
            if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__21 == null)
            {
              // ISSUE: reference to a compiler-generated field
              ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__21 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Open", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string, object> target8 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__21.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string, object>> p21 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__21;
            // ISSUE: reference to a compiler-generated field
            if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__20 == null)
            {
              // ISSUE: reference to a compiler-generated field
              ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__20 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj15 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__20.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__20, application);
            string str = path;
            obj12 = target8((CallSite) p21, obj15, str);
          }
          catch
          {
            Thread.Sleep(500);
          }
        }
        else
          break;
      }
      try
      {
        if (updateLinks)
        {
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__22 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__22 = CallSite<Action<CallSite, ImMSWordExternalDocumentCreator, ImExternalDocument, object, IDBObject>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "UpdateLinks", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__22.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__22, this, imdoc, obj12, docObject);
        }
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__35 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__35 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (ImMSWordExternalDocumentCreator)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, IEnumerable> target9 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__35.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, IEnumerable>> p35 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__35;
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__23 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__23 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Windows", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj16 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__23.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__23, obj12);
        foreach (object obj17 in target9((CallSite) p35, obj16))
        {
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__34 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__34 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (ImMSWordExternalDocumentCreator)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, IEnumerable> target10 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__34.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, IEnumerable>> p34 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__34;
          // ISSUE: reference to a compiler-generated field
          if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__24 == null)
          {
            // ISSUE: reference to a compiler-generated field
            ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__24 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Panes", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj18 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__24.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__24, obj17);
          foreach (object obj19 in target10((CallSite) p34, obj18))
          {
            // ISSUE: reference to a compiler-generated field
            if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__27 == null)
            {
              // ISSUE: reference to a compiler-generated field
              ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__27 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (int), typeof (ImMSWordExternalDocumentCreator)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, int> target11 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__27.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, int>> p27 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__27;
            // ISSUE: reference to a compiler-generated field
            if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__26 == null)
            {
              // ISSUE: reference to a compiler-generated field
              ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__26 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, object> target12 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__26.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, object>> p26 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__26;
            // ISSUE: reference to a compiler-generated field
            if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__25 == null)
            {
              // ISSUE: reference to a compiler-generated field
              ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__25 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Pages", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj20 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__25.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__25, obj19);
            object obj21 = target12((CallSite) p26, obj20);
            int num5 = target11((CallSite) p27, obj21);
            int num6 = 1;
            while (true)
            {
              // ISSUE: reference to a compiler-generated field
              if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__31 == null)
              {
                // ISSUE: reference to a compiler-generated field
                ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__31 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, bool> target13 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__31.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, bool>> p31 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__31;
              // ISSUE: reference to a compiler-generated field
              if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__30 == null)
              {
                // ISSUE: reference to a compiler-generated field
                ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__30 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.LessThanOrEqual, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, int, object, object> target14 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__30.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, int, object, object>> p30 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__30;
              int num7 = num6;
              // ISSUE: reference to a compiler-generated field
              if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__29 == null)
              {
                // ISSUE: reference to a compiler-generated field
                ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__29 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, object> target15 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__29.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, object>> p29 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__29;
              // ISSUE: reference to a compiler-generated field
              if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__28 == null)
              {
                // ISSUE: reference to a compiler-generated field
                ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__28 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Pages", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj22 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__28.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__28, obj19);
              object obj23 = target15((CallSite) p29, obj22);
              object obj24 = target14((CallSite) p30, num7, obj23);
              if (target13((CallSite) p31, obj24))
              {
                // ISSUE: reference to a compiler-generated field
                if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__32 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__32 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Pages", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj25 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__32.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__32, obj19, num6);
                // ISSUE: reference to a compiler-generated field
                if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__33 == null)
                {
                  // ISSUE: reference to a compiler-generated field
                  ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__33 = CallSite<Action<CallSite, ImMSWordExternalDocumentCreator, ImExternalDocument, object, SizeF>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "CreatePage", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
                  {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                  }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__33.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__33, this, imdoc, obj25, sizeF);
                ++num6;
              }
              else
                goto label_115;
            }
label_115:;
          }
        }
      }
      finally
      {
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__36 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__36 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Saved", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj26 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__36.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__36, obj12, true);
        // ISSUE: reference to a compiler-generated field
        if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__37 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__37 = CallSite<Action<CallSite, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__37.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__3.\u003C\u003Ep__37, obj12, Type.Missing, Type.Missing, Type.Missing);
      }
    }
    finally
    {
      msWordApiSession.Dispose();
    }
  }

  private void ReadOnlyLocalFiles_CanControlAttributeEvent(
    object sender,
    CanControlFileAttributeEventArgs e)
  {
    e.CanControl = false;
  }

  private void CreatePage(ImExternalDocument imdoc, object mspage, SizeF pageSize)
  {
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, byte[]>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (byte[]), typeof (ImMSWordExternalDocumentCreator)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, byte[]> target = ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, byte[]>> p1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "EnhMetaFileBits", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__5.\u003C\u003Ep__0, mspage);
    byte[] buffer = target((CallSite) p1, obj);
    try
    {
      if (imdoc.NewPage() is Page parent)
        parent.Size = pageSize;
      using (MemoryStream memoryStream = new MemoryStream(buffer))
      {
        Image image = Image.FromStream((Stream) memoryStream);
        if (image.Width > image.Height)
          parent.Landscape = true;
        ContainerElement containerElement = new ContainerElement((DocumentTreeNode) parent, new RectangleF((PointF) new Point(0, 0), parent.Size), true);
        containerElement.ScaleMode = ImageScaleMode.FitWidthHeight;
        containerElement.AssignImage(image, parent.Size, false, false, false);
      }
    }
    catch (Exception ex)
    {
    }
  }

  private void UpdateLinks(ImExternalDocument imdoc, object msdoc, IDBObject docObject)
  {
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Action<CallSite, ImMSWordExternalDocumentCreator, ImExternalDocument, object, IDBObject>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "UpdatePagesNumbers", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__0, this, imdoc, msdoc, docObject);
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "CustomDocumentProperties", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__1.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__1, msdoc);
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (ImMSWordExternalDocumentCreator)));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    foreach (object obj2 in ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__3.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__3, obj1))
    {
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Action<CallSite, ImMSWordExternalDocumentCreator, ImExternalDocument, object, IDBObject>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "UpdateLink", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__2.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__2, this, imdoc, obj2, docObject);
    }
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__5 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Update", (IEnumerable<Type>) null, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Action<CallSite, object> target = ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__5.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Action<CallSite, object>> p5 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__5;
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Fields", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__4.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__6.\u003C\u003Ep__4, msdoc);
    target((CallSite) p5, obj3);
  }

  public void UpdatePagesNumbers(ImExternalDocument imdoc, object msdoc, IDBObject docObject)
  {
    int num1 = 0;
    IDBAttribute attributeByGuid = docObject.GetAttributeByGuid(DocIDCache.FirstPageNumberInDocumentComplect_Guid, false);
    if (attributeByGuid != null)
      num1 = Convert.ToInt32(attributeByGuid.AsInteger);
    int num2 = 1;
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (ImMSWordExternalDocumentCreator)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, IEnumerable> target1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__9.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, IEnumerable>> p9 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__9;
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Sections", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__0, msdoc);
    foreach (object obj2 in target1((CallSite) p9, obj1))
    {
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RestartNumberingAtSection", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool, object> target2 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool, object>> p4 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "PageNumbers", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p3 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target4 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p2 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Footers", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__1, obj2);
      int num3 = num2;
      object obj4 = target4((CallSite) p2, obj3, num3);
      object obj5 = target3((CallSite) p3, obj4);
      object obj6 = target2((CallSite) p4, obj5, true);
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "StartingNumber", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target5 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p8 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__8;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "PageNumbers", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target6 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__7.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p7 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__7;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target7 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p6 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Footers", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__5.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__7.\u003C\u003Ep__5, obj2);
      int num4 = num2;
      object obj8 = target7((CallSite) p6, obj7, num4);
      object obj9 = target6((CallSite) p7, obj8);
      int num5 = num1 + 1;
      object obj10 = target5((CallSite) p8, obj9, num5);
    }
  }

  public void UpdateLink(ImExternalDocument imdoc, object prop, IDBObject docObject)
  {
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (ImMSWordExternalDocumentCreator)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string> target = ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string>> p1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__0, prop);
    string[] names = target((CallSite) p1, obj1).Split('.');
    string linkValue = this.GetLinkValue(imdoc, names, docObject);
    // ISSUE: reference to a compiler-generated field
    if (ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Value", typeof (ImMSWordExternalDocumentCreator), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__2.Target((CallSite) ImMSWordExternalDocumentCreator.\u003C\u003Eo__8.\u003C\u003Ep__2, prop, linkValue);
  }

  /// <summary>Получить значение ссылки</summary>
  /// <param name="names">Список названий параметров, например Источник данных \ Подпись \ Утвердил \ Дата</param>
  /// <param name="dbObject">Объект из которого брать значения аттрибутов</param>
  /// <returns></returns>
  private string GetLinkValue(ImExternalDocument imdoc, string[] names, IDBObject dbObject)
  {
    string name1 = names[0];
    string linkValue = "";
    long objectId1 = dbObject.ObjectID;
    if (name1.StartsWith("Подпись"))
    {
      if (names.Length > 2)
      {
        string graphName = names[1];
        string name2 = names[2];
        if (!imdoc.Signes.ContainsKey(objectId1) && dbObject.Session.GetCustomService(typeof (ISignsService)) is ISignsService customService)
        {
          List<SignParams> objectSignsParams = customService.GetObjectSignsParams(objectId1, dbObject.Session.SessionGUID, true);
          imdoc.Signes[objectId1] = new ArrayList((ICollection) objectSignsParams);
        }
        if (imdoc.Signes.ContainsKey(objectId1))
        {
          List<SignParams> signParamsList = new List<SignParams>();
          foreach (SignParams signParams in imdoc.Signes[objectId1])
            signParamsList.Add(signParams);
          List<SignParams> list = signParamsList.FindAll((Predicate<SignParams>) (x => x.GraphName == graphName)).OrderByDescending<SignParams, DateTime>((Func<SignParams, DateTime>) (x => x.SignDate)).ToList<SignParams>();
          if (list.Count > 0)
          {
            string str = "";
            SignParams signParams = list[0];
            if (name2 == "Фамилия")
              str = signParams.Surname;
            if (name2 == "Значение")
              str = signParams.SignValue;
            if (name2 == "Дата")
              str = signParams.SignDate.ToShortDateString();
            if (name2 == "Должность")
              str = signParams.Rank;
            if (name2 == "Графа")
              str = signParams.GraphName;
            linkValue = str;
          }
        }
      }
    }
    else
    {
      int attributeId = MetaDataHelper.GetAttributeID((object) name1);
      long objectId2 = dbObject.ObjectID;
      string caption = dbObject.Caption;
      if (attributeId < 0)
      {
        object[] valuesByName = dbObject.GetValuesByName(name1, false);
        if (valuesByName != null && valuesByName.Length != 0)
          linkValue = Convert.ToString(valuesByName[0]);
      }
      else
      {
        IDBAttribute attributeByName = dbObject.GetAttributeByName(name1, false);
        if (attributeByName != null)
        {
          if (attributeByName.DataType == FieldTypes.ftObjectLink)
          {
            List<string> list = ((IEnumerable<string>) names).ToList<string>();
            if (list.Count == 1)
            {
              linkValue = attributeByName.AsString;
            }
            else
            {
              list.RemoveAt(0);
              IDBObject objectActualCopy = dbObject.Session.GetObjectActualCopy(Convert.ToInt64(attributeByName.Value), false);
              if (objectActualCopy != null)
                return this.GetLinkValue(imdoc, list.ToArray(), objectActualCopy);
            }
          }
          else
            linkValue = attributeByName.AsString;
        }
      }
    }
    return linkValue;
  }
}
