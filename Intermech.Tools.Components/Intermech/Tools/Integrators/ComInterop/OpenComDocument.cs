// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ComInterop.OpenComDocument
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.Integrators.ComInterop;

/// <summary>
/// Реализует класс для представления документов, загруженных в память приложения. Доступ к содержимому таких документов осуществляется через COM-объект документа.
/// </summary>
public class OpenComDocument : IOpenDocument, IValueBagContainer
{
  private readonly string fileName;
  private readonly object comObject;

  /// <summary>Создает объект.</summary>
  /// <param name="fileName">Имя файла документа</param>
  /// <param name="comObject">COM-объект документа</param>
  public OpenComDocument(string fileName, object comObject)
  {
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException();
    // ISSUE: reference to a compiler-generated field
    if (OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (OpenComDocument), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (OpenComDocument), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__0.Target((CallSite) OpenComDocument.\u003C\u003Eo__0.\u003C\u003Ep__0, comObject, (object) null);
    if (target((CallSite) p1, obj))
      throw new ArgumentNullException(nameof (comObject));
    this.fileName = fileName;
    this.comObject = comObject;
  }

  /// <summary>Возвращает имя файла документа.</summary>
  public string FileName => this.fileName;

  /// <summary>Возвращает COM-объект документа.</summary>
  public object ComObject => this.comObject;

  IValueBagContainer IOpenDocument.Properties => (IValueBagContainer) this;
}
