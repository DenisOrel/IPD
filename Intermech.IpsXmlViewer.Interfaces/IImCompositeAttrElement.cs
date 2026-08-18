// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImCompositeAttrElement
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Абстрактный интерфейс, содержащий список ключей и значения (используется для атрибутов)
/// </summary>
public interface IImCompositeAttrElement : 
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable
{
  /// <summary>
  /// Прочитать/установить значение свойства с указанным Guid
  /// </summary>
  /// <param name="attrGuid">Guid атрибута</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>Значение атрибута или null, если атрибут с таким именем не найден</returns>
  IImAttribute this[string attrGuid, IKernel kernel] { get; set; }

  /// <summary>
  /// Прочитать/установить значение свойства с указанным Guid
  /// </summary>
  /// <param name="attrGuid">Guid атрибута</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>Значение атрибута или null, если атрибут с таким именем не найден</returns>
  IImAttribute this[Guid attrGuid, IKernel kernel] { get; set; }
}
