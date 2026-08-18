// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadExternalReferenceEntityProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Прокси-объект для COM-объекта элемента документа типа external reference (XRef).
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="rawEntity">исходный необернутый COM-объект элемента документа CAD-системы</param>
/// <param name="cadDocument">прокси-объект родительского документа</param>
/// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawEntity" /> содержит null; параметр <paramref name="cadDocument" /> содержит null</exception>
public class CadExternalReferenceEntityProxy(object rawEntity, CadDocumentProxy cadDocument) : 
  CadEntityProxy(rawEntity, cadDocument),
  IСadEntityProxyWithFile
{
  /// <summary>
  /// Возвращает путь к файлу элемента документа CAD-системы.
  /// Значение может включать абсолютный путь, относительный путь или только имя файла без пути.
  /// </summary>
  /// <returns>Путь к файлу или null, если содержимое элемента документа хранится в файле самого документа</returns>
  public string TryGetFilePath() => !this.DoTestIfFilePresent() ? (string) null : this.RawGetPath();

  /// <summary>
  /// Проверяет наличие файла у элемента документа CAD-системы.
  /// </summary>
  /// <returns>Признак наличия файла у документа CAD-системы</returns>
  protected virtual bool DoTestIfFilePresent() => true;

  /// <summary>
  /// Возвращает значение свойства IAcadExternalReference.Path.
  /// </summary>
  /// <returns>Значение свойства</returns>
  protected virtual string RawGetPath()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CadExternalReferenceEntityProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Path", typeof (CadExternalReferenceEntityProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) CadExternalReferenceEntityProxy.\u003C\u003Eo__3.\u003C\u003Ep__0, this.RawObject);
      return target((CallSite) p1, obj);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.CADDocument.CADSystem.ApplicationName, "IAcadExternalReference.Path");
    }
  }
}
