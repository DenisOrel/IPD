// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImGuidElement
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Абстрактный интерфейс, содержащий список ключей и значения, а также Guid (используется для объектов и связей)
/// </summary>
public interface IImGuidElement : 
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable
{
  /// <summary>Имя атрибута, который содержит Guid</summary>
  string GuidAttrName { get; }

  /// <summary>
  /// Глобальный идентификатор типа элемента в системе IPS - "F_GUID"
  /// </summary>
  Guid F_GUID { get; set; }
}
