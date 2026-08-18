// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.Data.ImRelation
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

/// <summary>Связь</summary>
[System.ComponentModel.Description("Связь")]
[DebuggerDisplay("{Text}")]
[XmlRoot("RELATION")]
public sealed class ImRelation : 
  ImDataElement,
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
  IImXmlRelation,
  IXmlRelation,
  IXmlDataEntity,
  IXmlEntity,
  IComparable<ImRelation>
{
  /// <summary>
  /// 
  /// </summary>
  public const int OptimizedCapacity = 25;

  /// <summary>Создать пустой экземпляр класса</summary>
  public ImRelation()
    : base(25)
  {
  }

  /// <summary>
  /// Создать пустой экземпляр класса, указанной размерности
  /// </summary>
  public ImRelation(int capacity)
    : base(capacity)
  {
  }

  /// <summary>
  /// Создать пустой экземпляр класса и заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ImRelation(object source)
    : this()
  {
    this.Assign(source);
  }

  /// <summary>Текст, отображаемый на экране</summary>
  public override string Text
  {
    get
    {
      return $"[{this.GetAsString("F_RELATION_TYPE", string.Empty)}] {this.GetAsString("F_PRJLINK_ID", string.Empty)}";
    }
  }

  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  public override string SQLTableName
  {
    [DebuggerStepThrough] get => "IMS_RELATIONS";
  }

  /// <summary>Имя атрибута, в котором хранится содержимое элемента</summary>
  public override string MainAttrName
  {
    [DebuggerStepThrough] get => "RELATION";
  }

  /// <summary>Имя атрибута, который содержит Guid</summary>
  public override string GuidAttrName
  {
    [DebuggerStepThrough] get => "F_PRJ_GUID";
  }

  /// <summary>Является ли элемент объектом или связью</summary>
  public override bool IsObject
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>
  /// Уникальный идентификатор элемента (версия объекта / идентификатор связи)
  /// </summary>
  public override string UniqueID
  {
    get => this.GetAsString("F_PRJLINK_ID", string.Empty);
    internal set => this.SetAsString("F_PRJLINK_ID", value);
  }

  public override string ToString()
  {
    return base.ToString() + string.Format(": attrs = " + this._attributes.ToString());
  }

  /// <summary>Сравнить с указанной связью</summary>
  /// <param name="other">Связь для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(ImRelation other) => 0;

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
