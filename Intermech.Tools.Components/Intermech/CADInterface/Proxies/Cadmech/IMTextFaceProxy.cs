// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.IMTextFaceProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.Cadmech;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies.Cadmech;

/// <summary>
/// Реализует обертку для COM-объекта поверхности (интерфейс IMFace_COM).
/// </summary>
public sealed class IMTextFaceProxy : CadmechObjectProxy, IEquatable<IMTextFaceProxy>
{
  private IMFace_COM rawFace;

  /// <summary>Создает прокси-объект.</summary>
  /// <param name="rawObject">COM-объект</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="rawObject" /> не должен быть равен null</exception>
  internal IMTextFaceProxy(IMFace_COM rawObject)
  {
    this.rawFace = rawObject != null ? rawObject : throw new ArgumentNullException(nameof (rawObject));
  }

  /// <summary>получить все атрибуты к поверхности</summary>
  /// <returns></returns>
  public IMTextFaceAttributeProxy[] GetRefAttrs()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFace_COM.GetRefAttrs()");
    try
    {
      IMFaceAttr_COM[] refAttrs = this.rawFace.GetRefAttrs();
      return refAttrs == null ? (IMTextFaceAttributeProxy[]) null : ((IEnumerable<IMFaceAttr_COM>) refAttrs).Where<IMFaceAttr_COM>((Func<IMFaceAttr_COM, bool>) (item => item != null)).Select<IMFaceAttr_COM, IMTextFaceAttributeProxy>((Func<IMFaceAttr_COM, IMTextFaceAttributeProxy>) (item => new IMTextFaceAttributeProxy(item))).ToArray<IMTextFaceAttributeProxy>();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFace_COM.GetRefAttrs()");
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="Highlight"></param>
  public void Highlight(bool highlight)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<bool>("IMFace_COM.Highlight()", highlight);
    try
    {
      this.rawFace.Highlight(highlight);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFace_COM.Highlight()");
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(IMTextFaceProxy other) => other != null && this.GUID == other.GUID;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    return obj is IMTextFaceProxy other ? this.Equals(other) : base.Equals(obj);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this.GUID.GetHashCode();

  /// <summary>Наименование поверхности</summary>
  public string Description
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFace_COM.get_Description()");
      try
      {
        return this.rawFace.Description;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFace_COM.get_Description()");
      }
    }
    set
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("IMFace_COM.set_Description()", value);
      try
      {
        this.rawFace.Description = value;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFace_COM.set_Description()");
      }
    }
  }

  /// <summary>Cсылка на примитив</summary>
  public IMTextGeEntityProxy GeEntity
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFace_COM.GeEntity()");
      try
      {
        GeEntity_COM geEntity = this.rawFace.GeEntity;
        return geEntity == null ? (IMTextGeEntityProxy) null : new IMTextGeEntityProxy(geEntity);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFace_COM.GeEntity()");
      }
    }
  }

  /// <summary>Уникальный идентификатор поверхности</summary>
  public string GUID
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFace_COM.get_GUID()");
      try
      {
        return this.rawFace.GUID;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFace_COM.get_GUID()");
      }
    }
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект IMFace_COM. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IMFace_COM RawObject
  {
    [DebuggerStepThrough] get => this.rawFace;
  }
}
