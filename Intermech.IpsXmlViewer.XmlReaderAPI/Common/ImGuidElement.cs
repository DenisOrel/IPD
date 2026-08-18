// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Common.ImGuidElement
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.IpsXmlViewer.Interfaces;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace XmlReaderAPI.Common;

/// <summary>
/// Абстрактный базовый класс, содержащий список ключей и значения, а также Guid (используется для объектов и связей)
/// </summary>
public abstract class ImGuidElement(int capacity) : 
  ImCompositeAttrElement(capacity),
  IImGuidElement,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable
{
  protected ImGuidElement()
    : this(0)
  {
  }

  /// <summary>Имя атрибута, который содержит Guid</summary>
  public abstract string GuidAttrName { get; }

  /// <summary>
  /// Глобальный идентификатор типа элемента в системе IPS - "F_GUID"
  /// </summary>
  [Description("Глобальный идентификатор типа элемента в системе IPS")]
  [XmlAttribute("F_GUID")]
  public virtual Guid F_GUID
  {
    get => this.GetAsGuid(this.GuidAttrName, Guid.Empty);
    set
    {
      if (value != Guid.Empty)
      {
        this.SetAsString(this.GuidAttrName, value.ToString());
      }
      else
      {
        if (!this.Attributes.ContainsKey(this.GuidAttrName))
          return;
        this.Attributes.Remove(this.GuidAttrName);
      }
    }
  }
}
