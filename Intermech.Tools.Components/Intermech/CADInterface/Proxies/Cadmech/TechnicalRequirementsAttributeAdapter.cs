// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.TechnicalRequirementsAttributeAdapter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.Cadmech;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies.Cadmech;

/// <summary>
/// Реализует вспомогательный объект для работы с техническими требованиями атрибута поверхности.
/// </summary>
public sealed class TechnicalRequirementsAttributeAdapter : CADObjectProxy
{
  private IMTextFaceAttributeProxy parentAttribute;
  private IMFaceAttrTT2_COM rawObject;

  internal TechnicalRequirementsAttributeAdapter(
    IMTextFaceAttributeProxy parentAttribute,
    IMFaceAttrTT2_COM rawObject)
  {
    if (parentAttribute == null)
      throw new ArgumentNullException(nameof (parentAttribute));
    if (rawObject == null)
      throw new ArgumentNullException(nameof (rawObject));
    this.parentAttribute = parentAttribute;
    this.rawObject = rawObject;
  }

  public int GetItemIndex(string item)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("IMFaceAttrTT2_COM.GetItemIndex()", item);
    try
    {
      return this.rawObject.GetItemIndex(item);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFaceAttrTT2_COM.GetItemIndex()");
    }
  }

  public string GetItemText(string item)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("IMFaceAttrTT2_COM.GetItemText()", item);
    try
    {
      return this.rawObject.GetItemText(item);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFaceAttrTT2_COM.GetItemText()");
    }
  }

  public string[] GetItems()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFaceAttrTT2_COM.GetItems()");
    try
    {
      return this.rawObject.GetItems();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFaceAttrTT2_COM.GetItems()");
    }
  }

  public string[] GetExtRefs(string item)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("IMFaceAttrTT2_COM.GetExtRefs()", item);
    try
    {
      return this.rawObject.GetExtRefs(item);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFaceAttrTT2_COM.GetExtRefs()");
    }
  }

  /// <summary>Возвращает родительский атрибут поверхности.</summary>
  public IMTextFaceAttributeProxy ParentAttribute
  {
    [DebuggerStepThrough] get => this.parentAttribute;
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект IMFaceAttrTT2_COM. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IMFaceAttrTT2_COM RawObject
  {
    [DebuggerStepThrough] get => this.rawObject;
  }
}
