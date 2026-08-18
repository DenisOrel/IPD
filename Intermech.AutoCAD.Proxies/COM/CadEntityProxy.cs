// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadEntityProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Прокси-объект для COM-объекта элемента документа CAD-системы.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
public class CadEntityProxy : CadObjectProxy
{
  private object rawEntity;
  private CadDocumentProxy cadDocument;

  /// <summary>Создает объект.</summary>
  /// <param name="rawEntity">исходный необернутый COM-объект элемента документа CAD-системы</param>
  /// <param name="cadDocument">прокси-объект родительского документа</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawEntity" /> содержит null; параметр <paramref name="cadDocument" /> содержит null</exception>
  public CadEntityProxy(object rawEntity, CadDocumentProxy cadDocument)
  {
    if (rawEntity == null)
      throw new ArgumentNullException(nameof (rawEntity));
    if (cadDocument == null)
      throw new ArgumentNullException(nameof (cadDocument));
    this.rawEntity = rawEntity;
    this.cadDocument = cadDocument;
  }

  /// <summary>
  /// Возвращает исходный необернутый COM-объект элемента документа CAD-системы.
  /// Это свойство должно использоваться только в тех случаях, когда
  /// COM-объект требуется передать в другое приложение.
  /// Внутри IPS должен использоваться только прокси-объект.
  /// </summary>
  public object RawObject
  {
    [DebuggerStepThrough] get => this.rawEntity;
  }

  /// <summary>Возвращает прокси-объект родительского документа.</summary>
  public ICadDocumentProxy CADDocument
  {
    [DebuggerStepThrough] get => (ICadDocumentProxy) this.cadDocument;
  }
}
