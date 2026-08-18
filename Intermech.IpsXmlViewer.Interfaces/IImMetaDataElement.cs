// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImMetaDataElement
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Абстрактный интерфейс, содержащий Guid (используется для метаданных)
/// </summary>
public interface IImMetaDataElement : 
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
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  string SQLTableName { get; }

  /// <summary>Количество атрибутов, которые есть в записи</summary>
  int SQLAttributes { get; }

  /// <summary>
  /// Уникальный идентификатор элемента (тип атрибута/объекта/связи)
  /// </summary>
  string UniqueID { get; }
}
