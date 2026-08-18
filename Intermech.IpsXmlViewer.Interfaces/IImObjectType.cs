// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImObjectType
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Описание типа объекта</summary>
public interface IImObjectType : 
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
  /// Локальный уникальный идентификатор типа объекта в документе - "F_OBJ_TYPE"
  /// </summary>
  int F_OBJ_TYPE { get; set; }

  /// <summary>Наименование типа объекта - "F_OBJ_TYPE_NAME"</summary>
  string F_OBJ_TYPE_NAME { get; set; }

  /// <summary>Имя файла для иконки типа объекта - "F_ICON"</summary>
  string F_ICON { get; set; }
}
