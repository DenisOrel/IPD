// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.AvsRowAttributeInfo
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Attributes;
using Intermech.Kernel.Search;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Информация об атрибуте в записи документа AVS</summary>
[Serializable]
public class AvsRowAttributeInfo : AttributeInfo
{
  /// <summary>Индекс атрибута в кэше данных записи</summary>
  public int IndexInValueList = -1;
  /// <summary>Вид информации для формирования запросов</summary>
  public ColumnContents ColumnContent;
  private bool _pinned;
  private int _columnWidth = 150;
  private bool _readOnly;

  protected AvsRowAttributeInfo(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ColumnContent", (int) this.ColumnContent);
    info.AddValue("_columnWidth", this.TableViewColumnWidth);
    info.AddValue("IndexInValueList", this.IndexInValueList);
    info.AddValue("readOnly", this.ReadOnly);
    info.AddValue("pinned", this.Pinned);
  }

  public override void SetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.SetObjectData(info, context);
    this.ColumnContent = (ColumnContents) info.GetInt32("ColumnContent");
    this.TableViewColumnWidth = info.GetInt32("_columnWidth");
    this.IndexInValueList = info.GetInt32("IndexInValueList");
    this.ReadOnly = info.GetBoolean("readOnly");
    if (!this.HasValue(info, "pinned"))
      return;
    this.Pinned = info.GetBoolean("pinned");
  }

  /// <summary>Сравнить данные об атрибуте</summary>
  /// <param name="attrInfo">Данные о втором атрибуте</param>
  /// <param name="findDocCellForAttr">Сравнение в контексте поиска ячейки документа для атрибута</param>
  /// <returns>true, если данные об одном и том же атрибуте</returns>
  public bool EqualAttrs(AvsRowAttributeInfo attrInfo, bool findDocCellForAttr)
  {
    if (attrInfo == null)
      return false;
    if (attrInfo.AttrSrc == this.AttrSrc)
    {
      if (attrInfo.AttrSrc == FieldSource.DocumentRowField)
        return attrInfo.AttributeId == AvsIDCache.Attr_PosDesignation && this.Name == "Поз. обозначение" || this.AttributeId == AvsIDCache.Attr_PosDesignation && attrInfo.Name == "Поз. обозначение" || this.Name == attrInfo.Name;
      if (this.AttributeId != -1 && attrInfo.AttributeId != -1)
        return this.AttributeId == attrInfo.AttributeId;
      if (this.AttributeGuid != Guid.Empty && attrInfo.AttributeGuid != Guid.Empty)
        return this.AttributeGuid == attrInfo.AttributeGuid;
      if (this.AttributeId == -1 && attrInfo.AttributeId == -1 && this.AttributeGuid == Guid.Empty && attrInfo.AttributeGuid == Guid.Empty && !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(attrInfo.Name))
        return this.Name == attrInfo.Name;
      return this.AttributeId == attrInfo.AttributeId && this.AttributeGuid == attrInfo.AttributeGuid && this.Name == attrInfo.Name;
    }
    if (!findDocCellForAttr || this.AttrSrc != FieldSource.DocumentRowField && attrInfo.AttrSrc != FieldSource.DocumentRowField)
      return false;
    return attrInfo.AttributeId == AvsIDCache.Attr_PosDesignation && this.Name == "Поз. обозначение" || this.AttributeId == AvsIDCache.Attr_PosDesignation && attrInfo.Name == "Поз. обозначение" || this.Name == attrInfo.Name || this.AttributeId == attrInfo.AttributeId;
  }

  /// <summary>Фиксация колонки слева в табличном виде. Для хранения настроек табличного вида</summary>
  public bool Pinned
  {
    get => this._pinned;
    set => this._pinned = value;
  }

  /// <summary>Ширина колонок в табличном виде. Для хранения настроек табличного вида</summary>
  public int TableViewColumnWidth
  {
    get => this._columnWidth;
    set => this._columnWidth = value;
  }

  /// <summary>Только для чтения</summary>
  public bool ReadOnly
  {
    [DebuggerStepThrough] get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
    }
  }

  /// <summary>Конструктор</summary>
  public AvsRowAttributeInfo()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="isRelationAttribute">Атрибут связи (если ture) или атрибут объекта (если false)</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  public AvsRowAttributeInfo(bool isRelationAttribute, int attributeId)
    : base(isRelationAttribute, attributeId)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="attributeName">Имя атрибута</param>
  public AvsRowAttributeInfo(
    FieldSource attrSrc,
    Guid attributeGuid,
    int attributeId,
    string attributeName,
    bool readOnly = false)
    : base(attrSrc, attributeGuid, attributeId, attributeName)
  {
    this._readOnly = readOnly;
  }

  /// <summary>Конструктор</summary>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="columnContent">Вид информации для формирования запросов</param>
  /// <param name="fieldType">Тип данных атрибута</param>
  public AvsRowAttributeInfo(
    FieldSource attrSrc,
    Guid attributeGuid,
    int attributeId,
    string attributeName,
    ColumnContents columnContent,
    FieldTypes? fieldType = null)
    : base(attrSrc, attributeGuid, attributeId, attributeName)
  {
    this.ColumnContent = columnContent;
    this._type = fieldType;
  }

  /// <summary>Конструктор</summary>
  /// <param name="attrInfo">Базовая информация об атрибуте</param>
  public AvsRowAttributeInfo(AttributeInfo attrInfo)
    : this(attrInfo.AttrSrc, attrInfo.AttributeGuid, attrInfo.AttributeId, attrInfo.Name)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="columnContent">Вид информации для формирования запросов</param>
  /// <param name="gridColumnWidth">Ширина колонки в табличном виде</param>
  public AvsRowAttributeInfo(
    FieldSource attrSrc,
    Guid attributeGuid,
    int attributeId,
    string attributeName,
    ColumnContents columnContent,
    int gridColumnWidth)
    : this(attrSrc, attributeGuid, attributeId, attributeName, columnContent)
  {
    this._columnWidth = gridColumnWidth;
  }

  /// <summary>Создать по Guid и загрузить остальную информацию из кэша метаданных</summary>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
  /// <param name="columnContent">Вид информации для формирования запросов</param>
  public static AvsRowAttributeInfo CreateByGuid(
    FieldSource attrSrc,
    string attributeGuid,
    ColumnContents columnContent = ColumnContents.Text)
  {
    return AvsRowAttributeInfo.CreateByGuid(attrSrc, new Guid(attributeGuid), columnContent);
  }

  /// <summary>Создать по Guid и загрузить остальную информацию из кэша метаданных</summary>
  /// <param name="attrSrc">Источник данных поля записи AVS</param>
  /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
  /// <param name="columnContent">Вид информации для формирования запросов</param>
  public static AvsRowAttributeInfo CreateByGuid(
    FieldSource attrSrc,
    Guid attributeGuid,
    ColumnContents columnContent = ColumnContents.Text)
  {
    int attributeId = 0;
    string attributeName = "";
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeGuid);
    if (attributeType != null)
    {
      attributeId = attributeType.AttributeID;
      attributeName = attributeType.Name;
    }
    return new AvsRowAttributeInfo(attrSrc, attributeGuid, attributeId, attributeName, columnContent);
  }

  /// <summary>Создать копию экземпляра класса</summary>
  public AvsRowAttributeInfo Clone()
  {
    return new AvsRowAttributeInfo(this.AttrSrc, this.AttributeGuid, this.AttributeId, this.Name, this.ColumnContent, this.TableViewColumnWidth)
    {
      IndexInValueList = this.IndexInValueList,
      _readOnly = this._readOnly
    };
  }

  /// <summary>Создает экземпляр атрибута строки документа</summary>
  /// <param name="name">Имя атрибута</param>
  /// <returns>Атрибут строки документа</returns>
  public static AvsRowAttributeInfo CreateDocRowFieldAttributeInfo(string name)
  {
    return !string.IsNullOrWhiteSpace(name) ? new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, name) : throw new ArgumentEmptyStringNotAllowedException(nameof (name));
  }
}
