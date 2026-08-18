// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.IMTextFaceAttributeProxy
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
/// Реализует обертку для COM-объекта атрибута поверхности (интерфейс IMFaceAttr_COM).
/// </summary>
public sealed class IMTextFaceAttributeProxy : 
  CadmechObjectProxy,
  IEquatable<IMTextFaceAttributeProxy>
{
  private IMFaceAttr_COM rawFaceAttribute;
  private Lazy<TechnicalRequirementsAttributeAdapter> technicalRequirementsAdapter;

  /// <summary>Создает прокси-объект.</summary>
  /// <param name="rawObject">COM-объект</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="rawObject" /> не должен быть равен null</exception>
  internal IMTextFaceAttributeProxy(IMFaceAttr_COM rawObject)
  {
    this.rawFaceAttribute = rawObject != null ? rawObject : throw new ArgumentNullException(nameof (rawObject));
    this.technicalRequirementsAdapter = new Lazy<TechnicalRequirementsAttributeAdapter>(new Func<TechnicalRequirementsAttributeAdapter>(this.TryCastToTechnicalRequirements));
  }

  /// <summary>Прочитать значение по имени свойства</summary>
  /// <param name="propName"></param>
  /// <returns></returns>
  public object GetProperty(string propName)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("IMFaceAttr_COM.get_Property()", propName);
    try
    {
      return this.rawFaceAttribute.get_Property(propName);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFaceAttr_COM.get_Property()");
    }
  }

  /// <summary>Задать значение по имени свойства</summary>
  /// <param name="propName"></param>
  /// <param name="propValue"></param>
  /// <returns></returns>
  public void SetProperty(string propName, object propValue)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, object>("IMFaceAttr_COM.set_Property()", propName, propValue);
    try
    {
      this.rawFaceAttribute.set_Property(propName, propValue);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMFaceAttr_COM.set_Property()");
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(IMTextFaceAttributeProxy other) => other != null && this.GUID == other.GUID;

  /// <summary>
  /// Возвращает объект для работы с техническими требованиями атрибута поверхности.
  /// </summary>
  /// <returns>Объект для работы с техническими требованиями</returns>
  public TechnicalRequirementsAttributeAdapter AsTechnicalRequirements()
  {
    return this.technicalRequirementsAdapter.Value ?? throw new NotSupportedException();
  }

  private TechnicalRequirementsAttributeAdapter TryCastToTechnicalRequirements()
  {
    return this.RawObject is IMFaceAttrTT2_COM rawObject ? new TechnicalRequirementsAttributeAdapter(this, rawObject) : (TechnicalRequirementsAttributeAdapter) null;
  }

  /// <summary>
  /// Получить все поверхности, на которые ссылается примитив
  /// </summary>
  public IMTextFaceProxy[] Faces
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFaceAttr_COM.get_Faces()");
      try
      {
        IMFace_COM[] faces = this.rawFaceAttribute.Faces;
        return faces == null ? (IMTextFaceProxy[]) null : ((IEnumerable<IMFace_COM>) faces).Where<IMFace_COM>((Func<IMFace_COM, bool>) (item => item != null)).Select<IMFace_COM, IMTextFaceProxy>((Func<IMFace_COM, IMTextFaceProxy>) (item => new IMTextFaceProxy(item))).ToArray<IMTextFaceProxy>();
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFaceAttr_COM.get_Faces()");
      }
    }
  }

  /// <summary>Уникальный идентификатор атрибута</summary>
  public string GUID
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFaceAttr_COM.get_GUID()");
      try
      {
        return this.rawFaceAttribute.GUID;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFaceAttr_COM.get_GUID()");
      }
    }
  }

  /// <summary>Все значения свойств атрибута</summary>
  public string[] Properties
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFaceAttr_COM.get_Properties()");
      try
      {
        return this.rawFaceAttribute.Properties;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFaceAttr_COM.get_Properties()");
      }
    }
  }

  /// <summary>Тип атрибута</summary>
  public IMTextFaceAttributeType AttrType
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMFaceAttr_COM.get_Type()");
      try
      {
        return (IMTextFaceAttributeType) this.rawFaceAttribute.Type;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IMFaceAttr_COM.get_Type()");
      }
    }
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект IMFaceAttr_COM. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IMFaceAttr_COM RawObject
  {
    [DebuggerStepThrough] get => this.rawFaceAttribute;
  }
}
