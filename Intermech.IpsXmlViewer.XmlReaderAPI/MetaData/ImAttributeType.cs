// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.MetaData.ImAttributeType
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
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XmlReaderAPI.Common;

#nullable disable
namespace XmlReaderAPI.MetaData;

/// <summary>Описание типа атрибута</summary>
[Description("Описание типа атрибута")]
[DebuggerDisplay("[{F_ATTRIBUTE_ID}:{F_ATTRIBUTE_TYPE}] \"{F_NAME}\" ({F_GUID})")]
[XmlRoot("ATTRIBUTE_TYPE")]
public sealed class ImAttributeType : 
  ImMetaDataElement,
  IImAttributeType,
  IImMetaDataElement,
  IImGuidElement,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable,
  IEquatable<IImMetaDataElement>,
  IComparable<ImAttributeType>
{
  /// <summary>
  /// Локальный уникальный идентификатор атрибута в документе - "F_ATTRIBUTE_ID"
  /// </summary>
  [Description("Локальный уникальный идентификатор атрибута в документе")]
  [XmlAttribute("F_ATTRIBUTE_ID")]
  public int F_ATTRIBUTE_ID
  {
    get => this.GetAsInt32(nameof (F_ATTRIBUTE_ID), 0);
    set
    {
    }
  }

  /// <summary>Наименование типа атрибута - "F_NAME"</summary>
  [Description("Наименование типа атрибута")]
  [XmlAttribute("F_NAME")]
  public string F_NAME
  {
    [DebuggerStepThrough] get => this.GetAsString(nameof (F_NAME), string.Empty);
    set
    {
      value = !string.IsNullOrEmpty(value) ? value.Trim() : string.Empty;
      this.SetAsString(nameof (F_NAME), value);
    }
  }

  /// <summary>Псевдоним атрибута - "F_ALIAS"</summary>
  [Description("Псевдоним атрибута")]
  [XmlAttribute("F_ALIAS")]
  public string F_ALIAS
  {
    get => this.GetAsString(nameof (F_ALIAS), string.Empty);
    set
    {
    }
  }

  /// <summary>
  /// Тип данных атрибута (строковое, целочисленное, файл, ccылка на объект) - "F_ATTRIBUTE_TYPE"
  /// </summary>
  [Description("Тип данных атрибута (строковое, целочисленное, файл, ccылка на объект)")]
  [XmlAttribute("F_ATTRIBUTE_TYPE")]
  public int F_ATTRIBUTE_TYPE
  {
    get => this.GetAsInt32(nameof (F_ATTRIBUTE_TYPE), 0);
    set
    {
    }
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public ImAttributeType()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его из указанного объекта-источника
  /// </summary>
  public ImAttributeType(object source) => this.Assign(source);

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="F_ATTRIBUTE_ID">Локальный уникальный идентификатор атрибута в документе</param>
  /// <param name="F_NAME">Наименование типа атрибута</param>
  /// <param name="F_ALIAS">Псевдоним атрибута</param>
  /// <param name="F_GUID">Глобальный идентификатор типа атрибута в системе IPS</param>
  /// <param name="F_ATTRIBUTE_TYPE">Тип данных атрибута (строковое, целочисленное, файл, ccылка на объект)</param>
  public ImAttributeType(
    int F_ATTRIBUTE_ID,
    string F_NAME,
    string F_ALIAS,
    Guid F_GUID,
    int F_ATTRIBUTE_TYPE)
  {
    this.F_ATTRIBUTE_ID = F_ATTRIBUTE_ID;
    this.F_NAME = F_NAME;
    this.F_ALIAS = F_ALIAS;
    this.F_GUID = F_GUID;
    this.F_ATTRIBUTE_TYPE = F_ATTRIBUTE_TYPE;
  }

  /// <summary>
  /// Возвращается имя атрибута для хранения в словарике у объекта/связи
  /// </summary>
  /// <param name="id">Идентификатор атрибута</param>
  /// <returns>Имя атрибута для хранения в словарике у объекта/связи</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetDictAttrKey(string id) => id + ".[ImAttribute]";

  /// <summary>
  /// Возвращается имя атрибута для хранения в словарике у объекта/связи
  /// </summary>
  public string DictAttrKey
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ImAttributeType.GetDictAttrKey(this.F_ATTRIBUTE_ID.ToString());
    }
  }

  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  public override string SQLTableName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return "IMS_ATTRIBUTE_TYPES";
    }
  }

  /// <summary>Уникальный идентификатор элемента (тип атрибута)</summary>
  public override string UniqueID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => "F_ATTRIBUTE_ID";
  }

  /// <summary>Имя атрибута, в котором хранится содержимое элемента</summary>
  public override string MainAttrName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => "ATTRIBUTE_TYPE";
  }

  /// <summary>Guid атрибута</summary>
  public override string GuidAttrName
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => "F_GUID";
  }

  /// <summary>Строка для отображения на экране</summary>
  public override string Text
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.F_NAME;
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
    if (!(source is ImAttributeType imAttributeType))
      return;
    this.F_NAME = imAttributeType.F_NAME;
  }

  /// <summary>Загрузить содержимое из документа XML</summary>
  /// <param name="xml">Документ XML</param>
  /// <param name="kernel">Микроядро</param>
  /// <returns>true - узел считал содержимое корректно</returns>
  public override bool Load(XmlReader xml, IKernel kernel)
  {
    int num = base.Load(xml, kernel) ? 1 : 0;
    IImMetaDataConverter service = kernel != null ? kernel.Services.GetService(typeof (IImMetaDataConverter)) as IImMetaDataConverter : (IImMetaDataConverter) null;
    if (service == null)
      return num != 0;
    if (this.F_GUID == Guid.Empty)
      this.F_GUID = service.GetIPSAttributeTypeGuid(kernel.Services, this.F_ATTRIBUTE_ID);
    int ipsAttributeTypeId = service.GetIPSAttributeTypeID(this.F_GUID);
    if (ipsAttributeTypeId < 0)
      return num != 0;
    this.SetAsString("IPS_F_ATTRIBUTE_ID", ipsAttributeTypeId.ToString());
    return num != 0;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj as ImAttributeType) == 0;

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.F_ATTRIBUTE_ID;

  /// <summary>Вернуть строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"[{this.F_ATTRIBUTE_ID}:{this.F_ATTRIBUTE_TYPE}] \"{this.F_NAME}\" ({this.F_GUID})";
  }

  /// <summary>Сравнить с другим типом атрибута</summary>
  /// <param name="other">Другой тип атрибута</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(ImAttributeType other)
  {
    if (other == null)
      return 1;
    return this == other ? 0 : string.Compare(this.F_NAME, other.F_NAME, StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  /// Загрузить список типов атрибутов из указанного документа
  /// </summary>
  /// <param name="document">Документ</param>
  /// <returns>Список типов атрибутов</returns>
  public static List<ImAttributeType> Load(XDocument document)
  {
    return document != null && document.Element((XName) "METADATABRIEF") != null && document.Element((XName) "METADATABRIEF").Elements((XName) "ATTRIBUTE_TYPES") != null ? document.Element((XName) "METADATABRIEF").Elements((XName) "ATTRIBUTE_TYPES").Elements<XElement>().Where<XElement>((Func<XElement, bool>) (item => item.Name == (XName) "ATTRIBUTE_TYPE")).Select<XElement, ImAttributeType>((Func<XElement, ImAttributeType>) (item =>
    {
      return new ImAttributeType()
      {
        F_ATTRIBUTE_ID = item.Element((XName) "F_ATTRIBUTE_ID") != null ? Convert.ToInt32(item.Element((XName) "F_ATTRIBUTE_ID").Value) : 0,
        F_NAME = item.Element((XName) "F_NAME") != null ? item.Element((XName) "F_NAME").Value : string.Empty,
        F_ALIAS = item.Element((XName) "F_ALIAS") != null ? item.Element((XName) "F_ALIAS").Value : string.Empty,
        F_ATTRIBUTE_TYPE = item.Element((XName) "F_ATTRIBUTE_TYPE") != null ? Convert.ToInt32(item.Element((XName) "F_ATTRIBUTE_TYPE").Value) : 0,
        F_GUID = item.Element((XName) "F_GUID") == null || !GuidHelper.IsGuid(item.Element((XName) "F_GUID").Value) ? Guid.Empty : new Guid(item.Element((XName) "F_GUID").Value)
      };
    })).ToList<ImAttributeType>() : new List<ImAttributeType>();
  }
}
