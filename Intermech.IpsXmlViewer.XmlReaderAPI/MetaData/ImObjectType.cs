// Decompiled with JetBrains decompiler
// Type: XmlReaderAPI.MetaData.ImObjectType
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

/// <summary>Описание типа объекта</summary>
[Description("Описание типа объекта")]
[DebuggerDisplay("[{F_OBJ_TYPE}] \"{F_OBJ_TYPE_NAME}\" ({F_GUID})")]
[XmlRoot("OBJECT_TYPE")]
public sealed class ImObjectType : 
  ImMetaDataElement,
  IImObjectType,
  IImMetaDataElement,
  IImGuidElement,
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable,
  IEquatable<IImMetaDataElement>,
  IComparable<ImObjectType>
{
  /// <summary>
  /// Локальный уникальный идентификатор типа объекта в документе - "F_OBJ_TYPE"
  /// </summary>
  [Description("Локальный уникальный идентификатор типа объекта в документе")]
  [XmlAttribute("F_OBJ_TYPE")]
  public int F_OBJ_TYPE
  {
    get => this.GetAsInt32(nameof (F_OBJ_TYPE), 0);
    set => this.SetAsInt32(nameof (F_OBJ_TYPE), value);
  }

  /// <summary>Наименование типа объекта - "F_OBJ_TYPE_NAME"</summary>
  [Description("Наименование типа объекта")]
  [XmlAttribute("F_OBJ_TYPE_NAME")]
  public string F_OBJ_TYPE_NAME
  {
    [DebuggerStepThrough] get => this.GetAsString(nameof (F_OBJ_TYPE_NAME), string.Empty);
    set
    {
      value = !string.IsNullOrEmpty(value) ? value.Trim() : string.Empty;
      this.SetAsString(nameof (F_OBJ_TYPE_NAME), value);
    }
  }

  /// <summary>Имя файла для иконки типа объекта - "F_ICON"</summary>
  [Description("Имя файла для иконки типа объекта")]
  [XmlAttribute("F_ICON")]
  public string F_ICON
  {
    get => this.GetAsString(nameof (F_ICON), string.Empty);
    set => this.GetAsString(nameof (F_ICON), value);
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public ImObjectType()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его из указанного объекта-источника
  /// </summary>
  public ImObjectType(object source) => this.Assign(source);

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="F_OBJ_TYPE">Локальный уникальный идентификатор типа объекта в документе</param>
  /// <param name="F_OBJ_TYPE_NAME">Наименование типа объекта</param>
  /// <param name="F_GUID">Глобальный идентификатор типа объекта в системе IPS</param>
  /// <param name="F_ICON">Имя файла для иконки типа объекта</param>
  public ImObjectType(int F_OBJ_TYPE, string F_OBJ_TYPE_NAME, Guid F_GUID, string F_ICON)
  {
    this.F_OBJ_TYPE = F_OBJ_TYPE;
    this.F_OBJ_TYPE_NAME = F_OBJ_TYPE_NAME;
    this.F_GUID = F_GUID;
    this.F_ICON = F_ICON;
  }

  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  public override string SQLTableName
  {
    [DebuggerStepThrough] get => "IMS_OBJECT_TYPES";
  }

  /// <summary>Уникальный идентификатор элемента (тип объекта)</summary>
  public override string UniqueID
  {
    [DebuggerStepThrough] get => "F_OBJ_TYPE";
  }

  /// <summary>Имя атрибута, в котором хранится содержимое элемента</summary>
  public override string MainAttrName
  {
    [DebuggerStepThrough] get => "OBJECT_TYPE";
  }

  /// <summary>Guid атрибута</summary>
  public override string GuidAttrName
  {
    [DebuggerStepThrough] get => "F_GUID";
  }

  /// <summary>Строка для отображения на экране</summary>
  public override string Text
  {
    [DebuggerStepThrough] get => this.F_OBJ_TYPE_NAME;
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
    if (!(source is ImObjectType))
      return;
    this.F_OBJ_TYPE = this.F_OBJ_TYPE;
    this.F_OBJ_TYPE_NAME = this.F_OBJ_TYPE_NAME;
    this.F_ICON = this.F_ICON;
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
      this.F_GUID = service.GetIPSObjectTypeGuid(this.F_OBJ_TYPE_NAME);
    if (this.F_GUID == Guid.Empty)
      this.F_GUID = service.GetIPSObjectTypeGuid(kernel.Services, this.F_OBJ_TYPE);
    int ipsObjectTypeId = service.GetIPSObjectTypeID(this.F_GUID);
    if (ipsObjectTypeId < 0)
      return num != 0;
    this.SetAsString("IPS_F_OBJ_TYPE", ipsObjectTypeId.ToString());
    return num != 0;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj as ImObjectType) == 0;

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.F_OBJ_TYPE;

  /// <summary>Вернуть строковое представление экземпляра класса</summary>
  /// <returns>Строковое представление экземпляра класса</returns>
  public override string ToString()
  {
    return $"[{this.F_OBJ_TYPE}] \"{this.F_OBJ_TYPE_NAME}\" ({this.F_GUID})";
  }

  /// <summary>Сравнить с другим типом объекта</summary>
  /// <param name="other">Другой тип объекта</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(ImObjectType other)
  {
    if (other == null)
      return 1;
    return this == other ? 0 : string.Compare(this.F_OBJ_TYPE_NAME, other.F_OBJ_TYPE_NAME, true);
  }

  /// <summary>
  /// Загрузить список типов объектов из указанного документа
  /// </summary>
  /// <param name="document">Документ</param>
  /// <returns>Список типов объектов</returns>
  public static List<ImObjectType> Load(XDocument document)
  {
    return document != null && document.Element((XName) "METADATABRIEF") != null && document.Element((XName) "METADATABRIEF").Elements((XName) "OBJECT_TYPES") != null ? document.Element((XName) "METADATABRIEF").Elements((XName) "OBJECT_TYPES").Elements<XElement>().Where<XElement>((Func<XElement, bool>) (item => item.Name == (XName) "OBJECT_TYPE")).Select<XElement, ImObjectType>((Func<XElement, ImObjectType>) (item =>
    {
      return new ImObjectType()
      {
        F_OBJ_TYPE = item.Element((XName) "F_OBJ_TYPE") != null ? Convert.ToInt32(item.Element((XName) "F_OBJ_TYPE").Value) : 0,
        F_OBJ_TYPE_NAME = item.Element((XName) "F_OBJ_TYPE_NAME") != null ? item.Element((XName) "F_OBJ_TYPE_NAME").Value : string.Empty,
        F_GUID = item.Element((XName) "F_GUID") == null || !GuidHelper.IsGuid(item.Element((XName) "F_GUID").Value) ? Guid.Empty : new Guid(item.Element((XName) "F_GUID").Value),
        F_ICON = item.Element((XName) "F_ICON") != null ? item.Element((XName) "F_ICON").Value : string.Empty
      };
    })).ToList<ImObjectType>() : new List<ImObjectType>();
  }
}
