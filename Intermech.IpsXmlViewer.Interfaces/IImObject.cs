// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImObject
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Версия объекта</summary>
public interface IImObject : 
  IImDataElement,
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
  /// <summary>Идентификатор версии объекта</summary>
  string F_OBJECT_ID { get; }
}
