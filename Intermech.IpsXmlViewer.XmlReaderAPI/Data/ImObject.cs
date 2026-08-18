// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Data.ImObject
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.IpsXmlViewer.Interfaces;
using Intermech.XmlExchange.IpsXml.Interfaces;
using Intermech.XmlExchange.IpsXml.Interfaces.Ips;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using XmlReaderAPI.Common;

#nullable disable
namespace XmlReaderAPI.Data;

/// <summary>Версия объекта</summary>
[System.ComponentModel.Description("Версия объекта")]
[DebuggerDisplay("[{F_OBJECT_ID}] {Text}")]
[XmlRoot("OBJECT")]
public sealed class ImObject : 
  ImDataElement,
  IImObject,
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
  IImXmlObject,
  IXmlObject,
  IXmlDataEntity,
  IXmlEntity,
  IComparable<ImObject>
{
  /// <summary>
  /// 
  /// </summary>
  public const int OptimizedCapacity = 25;

  /// <summary>Создать пустой экземпляр класса</summary>
  public ImObject()
    : this(25)
  {
  }

  /// <summary>
  /// Создать пустой экземпляр класса, указанной размерности
  /// </summary>
  public ImObject(int capacity)
    : base(capacity)
  {
  }

  /// <summary>
  /// Создать пустой экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ImObject(object source)
    : this()
  {
    this.Assign(source);
  }

  /// <summary>Идентификатор версии объекта</summary>
  public string F_OBJECT_ID
  {
    get => this.GetAsString(nameof (F_OBJECT_ID), string.Empty);
    set => this.SetAsString(nameof (F_OBJECT_ID), value);
  }

  /// <summary>Является ли элемент объектом или связью</summary>
  public override bool IsObject
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>
  /// Уникальный идентификатор элемента (версия объекта / идентификатор связи)
  /// </summary>
  public override string UniqueID
  {
    get => this.F_OBJECT_ID;
    internal set => this.F_OBJECT_ID = value;
  }

  /// <summary>Текст, отображаемый на экране</summary>
  public override string Text => this.GetAsString("CAPTION", string.Empty);

  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  public override string SQLTableName
  {
    [DebuggerStepThrough] get => "IMS_OBJECTS";
  }

  /// <summary>Имя атрибута, в котором хранится содержимое элемента</summary>
  public override string MainAttrName
  {
    [DebuggerStepThrough] get => "OBJECT";
  }

  /// <summary>Имя атрибута, который содержит Guid</summary>
  public override string GuidAttrName
  {
    [DebuggerStepThrough] get => "F_OBJECTGUID";
  }

  public override string ToString() => $"{base.ToString()}: F_OBJECT_ID = {this.F_OBJECT_ID}";

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(ImObject other) => 0;

  public IXmlParams XmlParams
  {
    get
    {
      IpsXmlParams xmlParams = new IpsXmlParams();
      foreach (string key in (IEnumerable<string>) this._attributes.Keys)
      {
        IpsXmlParam ipsXmlParam = new IpsXmlParam(key, this._attributes[key]);
        xmlParams.AddParam((IXmlParam) ipsXmlParam);
      }
      return (IXmlParams) xmlParams;
    }
  }

  /// <summary>Описание узла</summary>
  public string Description => this.ToString();
}
