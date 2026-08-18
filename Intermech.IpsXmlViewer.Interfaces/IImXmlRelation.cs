// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImXmlRelation
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using Intermech.XmlExchange.IpsXml.Interfaces;
using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Расширение интерфейса связей</summary>
public interface IImXmlRelation : 
  IImRelation,
  IImDataElement,
  IImMetaDataElement,
  IImGuidElement,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable,
  IEquatable<IImMetaDataElement>,
  IXmlRelation,
  IXmlDataEntity,
  IXmlEntity
{
}
