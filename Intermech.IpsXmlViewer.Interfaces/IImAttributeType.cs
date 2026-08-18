// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImAttributeType
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Описание типа атрибута</summary>
public interface IImAttributeType : 
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
  /// Локальный уникальный идентификатор атрибута в документе - "F_ATTRIBUTE_ID"
  /// </summary>
  int F_ATTRIBUTE_ID { get; set; }

  /// <summary>Наименование типа атрибута - "F_NAME"</summary>
  string F_NAME { get; set; }

  /// <summary>Псевдоним атрибута - "F_ALIAS"</summary>
  string F_ALIAS { get; set; }

  /// <summary>
  /// Тип данных атрибута (строковое, целочисленное, файл, ccылка на объект) - "F_ATTRIBUTE_TYPE"
  /// </summary>
  int F_ATTRIBUTE_TYPE { get; set; }

  /// <summary>
  /// Возвращается имя атрибута для хранения в словарике у объекта/связи
  /// </summary>
  string DictAttrKey { get; }
}
