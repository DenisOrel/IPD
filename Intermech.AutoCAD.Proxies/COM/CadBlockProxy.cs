// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadBlockProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Прокси-объект для COM-объекта блока документа CAD-системы.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
public class CadBlockProxy : CadObjectProxy
{
  private object rawBlock;
  private CadDocumentProxy cadDocument;

  /// <summary>Создает объект.</summary>
  /// <param name="rawBlock">Исходный необернутый COM-объект блока документа CAD-системы</param>
  /// <param name="cadDocument">Прокси-объект родительского документа</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="rawBlock" /> содержит null; параметр <paramref name="cadDocument" /> содержит null</exception>
  public CadBlockProxy(object rawBlock, CadDocumentProxy cadDocument)
  {
    if (rawBlock == null)
      throw new ArgumentNullException(nameof (rawBlock));
    if (cadDocument == null)
      throw new ArgumentNullException(nameof (cadDocument));
    this.rawBlock = rawBlock;
    this.cadDocument = cadDocument;
  }

  /// <summary>Возвращает путь к блоку документа CAD-системы</summary>
  public string Path
  {
    get
    {
      try
      {
        if (CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
          CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadBlockProxy)));
        Func<CallSite, object, string> target = CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, string>> p1 = CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__1;
        if (CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
          CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, nameof (Path), typeof (CadBlockProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj = CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) CadBlockProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, this.rawBlock);
        return target((CallSite) p1, obj);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.CADDocument.CADSystem.ApplicationName, "IAcadBlock.Path");
      }
    }
  }

  /// <summary>Возвращает прокси-объект родительского документа.</summary>
  public ICadDocumentProxy CADDocument
  {
    [DebuggerStepThrough] get => (ICadDocumentProxy) this.cadDocument;
  }
}
