// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Common.ImCompositeAttrElement
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.IpsXmlViewer.Interfaces;
using System;

#nullable disable
namespace XmlReaderAPI.Common;

/// <summary>
/// Абстрактный базовый класс, содержащий список ключей и значения (используется для атрибутов)
/// </summary>
public abstract class ImCompositeAttrElement(int capacity) : 
  ImCompositeElement(capacity),
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable
{
  protected ImCompositeAttrElement()
    : this(0)
  {
  }

  /// <summary>Получить уникальное имя атрибута в словарике</summary>
  /// <param name="attrGuid">Guid атрибута</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>Уникальное имя атрибута в словарике или String.Empty</returns>
  protected virtual string GetAttrKey(Guid attrGuid, IKernel kernel)
  {
    if (attrGuid == Guid.Empty || kernel == null || kernel.Indexer == null || kernel.Indexer.MetaData == null)
      return string.Empty;
    IImAttributeType attributeType = kernel.Indexer.MetaData.GetAttributeType(attrGuid);
    return attributeType == null ? string.Empty : attributeType.DictAttrKey;
  }

  /// <summary>Получить уникальное имя атрибута в словарике</summary>
  /// <param name="attrGuid">Guid атрибута</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>Уникальное имя атрибута в словарике или String.Empty</returns>
  protected virtual string GetAttrKey(string attrGuid, IKernel kernel)
  {
    return string.IsNullOrEmpty(attrGuid) || !GuidHelper.IsGuid(attrGuid) ? string.Empty : this.GetAttrKey(new Guid(attrGuid), kernel);
  }

  /// <summary>
  /// Прочитать/установить значение свойства с указанным Guid
  /// </summary>
  /// <param name="attrGuid">Guid атрибута</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>Значение атрибута или null, если атрибут с таким именем не найден</returns>
  public IImAttribute this[Guid attrGuid, IKernel kernel]
  {
    get
    {
      string attrKey = this.GetAttrKey(attrGuid, kernel);
      return string.IsNullOrEmpty(attrKey) ? (IImAttribute) null : this[attrKey] as IImAttribute;
    }
    set
    {
      string attrKey = this.GetAttrKey(attrGuid, kernel);
      if (string.IsNullOrEmpty(attrKey))
        return;
      this.SetAsObject(attrKey, (object) value);
    }
  }

  /// <summary>
  /// Прочитать/установить значение свойства с указанным Guid
  /// </summary>
  /// <param name="attrGuid">Guid атрибута</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>Значение атрибута или null, если атрибут с таким именем не найден</returns>
  public IImAttribute this[string attrGuid, IKernel kernel]
  {
    get
    {
      string attrKey = this.GetAttrKey(attrGuid, kernel);
      return string.IsNullOrEmpty(attrKey) ? (IImAttribute) null : this[attrKey] as IImAttribute;
    }
    set
    {
      string attrKey = this.GetAttrKey(attrGuid, kernel);
      if (string.IsNullOrEmpty(attrKey))
        return;
      this.SetAsObject(attrKey, (object) value);
    }
  }
}
