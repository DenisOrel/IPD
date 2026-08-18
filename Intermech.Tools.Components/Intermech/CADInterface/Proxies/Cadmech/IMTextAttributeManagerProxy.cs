// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.IMTextAttributeManagerProxy
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
/// Реализует обертку для COM-объекта менеджера атрибутов (интерфейс IMAttrManager_COM).
/// </summary>
public sealed class IMTextAttributeManagerProxy : CadmechObjectProxy
{
  private IMAttrManager_COM rawAttributeManager;

  /// <summary>Создает прокси-объект.</summary>
  /// <param name="rawObject">COM-объект</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="rawObject" /> не должен быть равен null</exception>
  internal IMTextAttributeManagerProxy(IMAttrManager_COM rawObject)
  {
    this.rawAttributeManager = rawObject != null ? rawObject : throw new ArgumentNullException(nameof (rawObject));
  }

  /// <summary>Найти атрибут по имени</summary>
  /// <param name="attrGUID"></param>
  /// <returns></returns>
  public IMTextFaceAttributeProxy FindAttr(string attrGUID)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("IMAttrManager_COM.FindAttr()", attrGUID);
    try
    {
      IMFaceAttr_COM attr = this.rawAttributeManager.FindAttr(attrGUID);
      return attr == null ? (IMTextFaceAttributeProxy) null : new IMTextFaceAttributeProxy(attr);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMAttrManager_COM.FindAttr()");
    }
  }

  /// <summary>Получить все атрибуты модели по типу</summary>
  /// <param name="attrType"></param>
  /// <returns></returns>
  public IMTextFaceAttributeProxy[] GetAllFaceAttrsByType(IMTextFaceAttributeType attrType)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<IMTextFaceAttributeType>("IMAttrManager_COM.GetAllFaceAttrsByType()", attrType);
    try
    {
      IMFaceAttr_COM[] allFaceAttrsByType = this.rawAttributeManager.GetAllFaceAttrsByType((EAttrType) attrType);
      return allFaceAttrsByType == null ? (IMTextFaceAttributeProxy[]) null : ((IEnumerable<IMFaceAttr_COM>) allFaceAttrsByType).Where<IMFaceAttr_COM>((Func<IMFaceAttr_COM, bool>) (item => item != null)).Select<IMFaceAttr_COM, IMTextFaceAttributeProxy>((Func<IMFaceAttr_COM, IMTextFaceAttributeProxy>) (item => new IMTextFaceAttributeProxy(item))).ToArray<IMTextFaceAttributeProxy>();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMAttrManager_COM.GetAllFaceAttrsByType()");
    }
  }

  /// <summary>Получить все поверхности модели</summary>
  /// <returns></returns>
  public IMTextFaceProxy[] GetAllFaces()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMAttrManager_COM.GetFaces()");
    try
    {
      IMFace_COM[] faces = this.rawAttributeManager.GetFaces();
      return faces == null ? (IMTextFaceProxy[]) null : ((IEnumerable<IMFace_COM>) faces).Where<IMFace_COM>((Func<IMFace_COM, bool>) (item => item != null)).Select<IMFace_COM, IMTextFaceProxy>((Func<IMFace_COM, IMTextFaceProxy>) (item => new IMTextFaceProxy(item))).ToArray<IMTextFaceProxy>();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMAttrManager_COM.GetFaces()");
    }
  }

  /// <summary>Диалог выбора объекта</summary>
  /// <param name="dlgCaption"></param>
  /// <param name="objFilter"></param>
  /// <returns></returns>
  public object SelectObject(string dlgCaption, IMTextEntityId[] objFilter)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, string>("IMAttrManager_COM.SelectObject()", dlgCaption, Convert.ToString((object) objFilter));
    try
    {
      object rawObject = this.rawAttributeManager.SelectObject(dlgCaption, (object) null, objFilter != null ? ((IEnumerable<IMTextEntityId>) objFilter).Select<IMTextEntityId, IM_EntityId>((Func<IMTextEntityId, IM_EntityId>) (item => (IM_EntityId) item)).ToArray<IM_EntityId>() : (IM_EntityId[]) null);
      switch (rawObject)
      {
        case IMFace_COM _:
          return (object) new IMTextFaceProxy(rawObject as IMFace_COM);
        case IMFaceAttr_COM _:
          return (object) new IMTextFaceAttributeProxy(rawObject as IMFaceAttr_COM);
        default:
          return (object) null;
      }
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMAttrManager_COM.SelectObject()");
    }
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект IMAttrManager_COM. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IMAttrManager_COM RawObject
  {
    [DebuggerStepThrough] get => this.rawAttributeManager;
  }
}
