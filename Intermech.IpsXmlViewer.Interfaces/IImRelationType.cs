// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImRelationType
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Описание типа связи</summary>
public interface IImRelationType : 
  IImMetaDataElement,
  IImGuidElement,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable,
  IEquatable<IImMetaDataElement>
{
  /// <summary>
  /// Локальный уникальный идентификатор типа связи в документе - "F_RELATION_TYPE"
  /// </summary>
  int F_RELATION_TYPE { get; set; }

  /// <summary>Наименование типа связи - "F_TYPE_NAME"</summary>
  string F_TYPE_NAME { get; set; }
}
