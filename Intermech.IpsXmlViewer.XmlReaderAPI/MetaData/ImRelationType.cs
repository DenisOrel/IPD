// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.MetaData.ImRelationType
// Assembly: Intermech.IpsXmlViewer.XmlReaderAPI, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 197F841C-E5B9-4815-BCCD-9737649DED5C
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.XmlReaderAPI.xml

using Intermech.IpsXmlViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XmlReaderAPI.Common;

#nullable disable
namespace XmlReaderAPI.MetaData;

/// <summary>Описание типа связи</summary>
[Description("Описание типа объекта")]
[DebuggerDisplay("[{F_RELATION_TYPE}] \"{F_TYPE_NAME}\" ({F_GUID})")]
[XmlRoot("RELATION_TYPE")]
public sealed class ImRelationType : 
  ImMetaDataElement,
  IImRelationType,
  IImMetaDataElement,
  IImGuidElement,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable,
  IEquatable<IImMetaDataElement>,
  IComparable<ImRelationType>
{
  /// <summary>
  /// Локальный уникальный идентификатор типа связи в документе - "F_RELATION_TYPE"
  /// </summary>
  [Description("Локальный уникальный идентификатор типа связи в документе")]
  [XmlAttribute("F_RELATION_TYPE")]
  public int F_RELATION_TYPE
  {
    get => this.GetAsInt32(nameof (F_RELATION_TYPE), 0);
    set => this.GetAsInt32(nameof (F_RELATION_TYPE), value);
  }

  /// <summary>Наименование типа связи - "F_TYPE_NAME"</summary>
  [Description("Наименование типа связи")]
  [XmlAttribute("F_TYPE_NAME")]
  public string F_TYPE_NAME
  {
    [DebuggerStepThrough] get => this.GetAsString(nameof (F_TYPE_NAME), string.Empty);
    set
    {
      value = !string.IsNullOrEmpty(value) ? value.Trim() : string.Empty;
      this.SetAsString(nameof (F_TYPE_NAME), value);
    }
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public ImRelationType()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его из указанного объекта-источника
  /// </summary>
  public ImRelationType(object source) => this.Assign(source);

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="F_RELATION_TYPE">Локальный уникальный идентификатор типа связи в документе</param>
  /// <param name="F_TYPE_NAME">Наименование типа связи</param>
  /// <param name="F_GUID">Глобальный идентификатор типа связи в системе IPS</param>
  public ImRelationType(int F_RELATION_TYPE, string F_TYPE_NAME, Guid F_GUID)
  {
    this.F_RELATION_TYPE = F_RELATION_TYPE;
    this.F_TYPE_NAME = F_TYPE_NAME;
    this.F_GUID = F_GUID;
  }

  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  public override string SQLTableName
  {
    [DebuggerStepThrough] get => "IMS_RELATION_TYPES";
  }

  /// <summary>Уникальный идентификатор элемента (тип связи)</summary>
  public override string UniqueID
  {
    [DebuggerStepThrough] get => "F_RELATION_TYPE";
  }

  /// <summary>Имя атрибута, в котором хранится содержимое элемента</summary>
  public override string MainAttrName
  {
    [DebuggerStepThrough] get => "RELATION_TYPE";
  }

  /// <summary>Guid атрибута</summary>
  public override string GuidAttrName => "F_GUID";

  /// <summary>Строка для отображения на экране</summary>
  public override string Text
  {
    [DebuggerStepThrough] get => this.F_TYPE_NAME;
  }

  /// <summary>
  /// Заполнить поля класса информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is ImRelationType))
      return;
    this.F_RELATION_TYPE = this.F_RELATION_TYPE;
    this.F_TYPE_NAME = this.F_TYPE_NAME;
  }

  /// <summary>Загрузить содержимое из документа XML</summary>
  /// <param name="xml">Документ XML</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>true - узел считал содержимое коректно</returns>
  public override bool Load(XmlReader xml, IKernel kernel)
  {
    int num = base.Load(xml, kernel) ? 1 : 0;
    IImMetaDataConverter service = kernel != null ? kernel.Services.GetService(typeof (IImMetaDataConverter)) as IImMetaDataConverter : (IImMetaDataConverter) null;
    if (service == null)
      return num != 0;
    if (this.F_GUID == Guid.Empty)
      this.F_GUID = service.GetIPSRelationTypeGuid(this.F_TYPE_NAME);
    if (this.F_GUID == Guid.Empty)
      this.F_GUID = service.GetIPSRelationTypeGuid(kernel.Services, this.F_RELATION_TYPE);
    int ipsRelationTypeId = service.GetIPSRelationTypeID(this.F_GUID);
    if (ipsRelationTypeId < 0)
      return num != 0;
    this.SetAsString("IPS_F_RELATION_TYPE", ipsRelationTypeId.ToString());
    return num != 0;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj as ImRelationType) == 0;

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.F_RELATION_TYPE;

  /// <summary>Вернуть строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"[{this.F_RELATION_TYPE}] \"{this.F_TYPE_NAME}\" ({this.F_GUID})";
  }

  /// <summary>Сравнить с другим типом связи</summary>
  /// <param name="other">Другой тип связи</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(ImRelationType other)
  {
    if (other == null)
      return 1;
    return this == other ? 0 : string.Compare(this.F_TYPE_NAME, other.F_TYPE_NAME, true);
  }

  /// <summary>Загрузить список типов связей из указанного документа</summary>
  /// <param name="document">Документ</param>
  /// <returns>Список типов связей</returns>
  public static List<ImRelationType> Load(XDocument document)
  {
    return document != null && document.Element((XName) "METADATABRIEF") != null && document.Element((XName) "METADATABRIEF").Elements((XName) "RELATION_TYPES") != null ? document.Element((XName) "METADATABRIEF").Elements((XName) "RELATION_TYPES").Elements<XElement>().Where<XElement>((Func<XElement, bool>) (item => item.Name == (XName) "RELATION_TYPE")).Select<XElement, ImRelationType>((Func<XElement, ImRelationType>) (item =>
    {
      return new ImRelationType()
      {
        F_RELATION_TYPE = item.Element((XName) "F_RELATION_TYPE") != null ? Convert.ToInt32(item.Element((XName) "F_RELATION_TYPE").Value) : 0,
        F_TYPE_NAME = item.Element((XName) "F_TYPE_NAME") != null ? item.Element((XName) "F_TYPE_NAME").Value : string.Empty,
        F_GUID = item.Element((XName) "F_GUID") == null || !GuidHelper.IsGuid(item.Element((XName) "F_GUID").Value) ? Guid.Empty : new Guid(item.Element((XName) "F_GUID").Value)
      };
    })).ToList<ImRelationType>() : new List<ImRelationType>();
  }
}
