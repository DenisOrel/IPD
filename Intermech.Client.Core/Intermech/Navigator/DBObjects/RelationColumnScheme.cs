
// Type: Intermech.Navigator.DBObjects.RelationColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Схема виртуальных колонок, описывающая необязательные атрибуты объектов.
/// Идентификаторы виртуальных колонок - это имена атрибутов.
/// </summary>
public class RelationColumnScheme : INodeColumnScheme
{
  /// <summary>Название схемы колонок</summary>
  private static readonly string SchemeName = LocalizationHolder.rm.GetString("Client.Core_318");
  /// <summary>Тип схемы колонок (для атрибутов)</summary>
  private static readonly string SchemeTypeName = LocalizationHolder.rm.GetString("Client.Core_319");
  /// <summary>
  /// 
  /// </summary>
  private static readonly string SchemeAttributeInfo = LocalizationHolder.rm.GetString("Client.Core_320");
  /// <summary>
  /// Список атрибутов, значения которых являются списковыми значениями
  /// </summary>
  private static List<int> _listAttrs = new List<int>(0);
  /// <summary>
  /// Список атрибутов, значения которых являются булевыми величинами
  /// </summary>
  private static List<int> _boolAttrs = new List<int>(0);
  /// <summary>Список атрибутов, значения которых являются Guid</summary>
  private static List<int> _guidAttrs = new List<int>(0);
  private static List<int> _dateTimeAttributeTypeIds = new List<int>();
  private static DateTimeNodeColumnTransform _dateTimeNodeColumnTransform = new DateTimeNodeColumnTransform();
  private static List<int> _objectLinkAttributeTypeIds = new List<int>();
  private static ObjectLinkColumnTransform _objectLinkNodeColumnTransform = new ObjectLinkColumnTransform();
  /// <summary>
  /// Преобразователи значений списковых атрибутов в их описания
  /// </summary>
  private Dictionary<int, ListAttributeTransform> _listAttrsTransforms = new Dictionary<int, ListAttributeTransform>();
  /// <summary>
  /// Преобразователи значений булевых атрибутов в строковые
  /// </summary>
  private Dictionary<int, BoolAttributeTransform> _boolAttrsTransforms = new Dictionary<int, BoolAttributeTransform>();
  /// <summary>Преобразователи значений Guid атрибутов в строковые</summary>
  private Dictionary<int, GuidAttributeTransform> _guidAttrsTransforms = new Dictionary<int, GuidAttributeTransform>();
  private static List<int> _doubleAttributeTypeIds = new List<int>();
  private static DoubleNodeColumnTransform _doubleNodeColumnTransform = new DoubleNodeColumnTransform();

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public virtual string Name => RelationColumnScheme.SchemeName;

  /// <summary>
  /// Добавить атрибут во внутренние коллекции при необходимости
  /// </summary>
  /// <param name="attr"></param>
  protected void InternalAddAttribute(IMSAttributeType attr)
  {
    if (attr == null)
      return;
    if (attr.MultiValueMode == MultiValueModes.SingleValueFromList && !RelationColumnScheme._listAttrs.Contains(attr.AttributeID))
    {
      lock (RelationColumnScheme._listAttrs)
      {
        RelationColumnScheme._listAttrs.Add(attr.AttributeID);
        if (!this._listAttrsTransforms.ContainsKey(attr.AttributeID))
          this._listAttrsTransforms.Add(attr.AttributeID, new ListAttributeTransform(attr.AttributeID));
      }
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftBoolean)
    {
      lock (RelationColumnScheme._boolAttrs)
      {
        RelationColumnScheme._boolAttrs.Add(attr.AttributeID);
        if (!this._boolAttrsTransforms.ContainsKey(attr.AttributeID))
          this._boolAttrsTransforms.Add(attr.AttributeID, new BoolAttributeTransform(attr.AttributeID));
      }
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftGuid)
    {
      lock (RelationColumnScheme._guidAttrs)
      {
        RelationColumnScheme._guidAttrs.Add(attr.AttributeID);
        if (!this._guidAttrsTransforms.ContainsKey(attr.AttributeID))
          this._guidAttrsTransforms.Add(attr.AttributeID, new GuidAttributeTransform(attr.AttributeID));
      }
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftDateTime)
    {
      lock (RelationColumnScheme._dateTimeAttributeTypeIds)
        RelationColumnScheme._dateTimeAttributeTypeIds.Add(attr.AttributeID);
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftObjectLink)
    {
      lock (RelationColumnScheme._objectLinkAttributeTypeIds)
        RelationColumnScheme._objectLinkAttributeTypeIds.Add(attr.AttributeID);
    }
    if (attr == null || attr.FieldType != FieldTypes.ftDouble)
      return;
    lock (RelationColumnScheme._doubleAttributeTypeIds)
      RelationColumnScheme._doubleAttributeTypeIds.Add(attr.AttributeID);
  }

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType((int) columnID);
    this.InternalAddAttribute(attributeType);
    return attributeType != null ? attributeType.AttributeGuid.ToString() : string.Empty;
  }

  /// <summary>
  /// Восстанавливает идентификатор виртуальной колонки по ее
  /// постоянному имени, которое действительно только на текущий сеанс
  /// работы универсального клиента. Если восстанавливаемая колонка не
  /// существует, то метод должен вернуть null.
  /// </summary>
  /// <param name="persistName">Постоянное имя колонки</param>
  /// <returns>Идентификатор виртуальной колонки</returns>
  public object PersistNameToColumnID(string persistName)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(persistName));
    this.InternalAddAttribute(attributeType);
    return attributeType != null ? (object) attributeType.AttributeID : (object) null;
  }

  /// <summary>
  /// Создает виртуальную колонку без сортировки по указанному
  /// идентификатору. Если колонки с заданным идентификатором в схеме нет -
  /// то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Виртуальная колонка</returns>
  public NodeColumn CreateColumn(Guid schemeGuid, object columnID)
  {
    return this.CreateColumn(schemeGuid, columnID, NodeColumnSortOrder.None, -1);
  }

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  public NodeColumn CreateColumn(
    Guid schemeGuid,
    object columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    if (columnID is int attrTypeID)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
      this.InternalAddAttribute(attributeType);
      if (attributeType != null)
        return this.CreateColumn(schemeGuid, attributeType, sortOrder, sortIndex);
    }
    return (NodeColumn) null;
  }

  /// <summary>
  /// Возвращает преобразование по умолчанию для указанной виртуальной
  /// колонки. Если преобразование не задано, то метод вернет null.
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Преобразование по умолчанию</returns>
  public INodeColumnTransform GetDefaultTransform(object columnID)
  {
    if (columnID == null)
      return (INodeColumnTransform) null;
    int num = (int) columnID;
    lock (RelationColumnScheme._listAttrs)
    {
      lock (RelationColumnScheme._boolAttrs)
      {
        if (!RelationColumnScheme._listAttrs.Contains(num) && !RelationColumnScheme._boolAttrs.Contains(num) && !RelationColumnScheme._guidAttrs.Contains(num) && !RelationColumnScheme._dateTimeAttributeTypeIds.Contains(num) && !RelationColumnScheme._objectLinkAttributeTypeIds.Contains(num) && !RelationColumnScheme._doubleAttributeTypeIds.Contains(num))
          return (INodeColumnTransform) null;
        if (RelationColumnScheme._boolAttrs.Contains(num))
        {
          if (!this._boolAttrsTransforms.ContainsKey(num))
            this._boolAttrsTransforms.Add(num, new BoolAttributeTransform(num));
          return (INodeColumnTransform) this._boolAttrsTransforms[num];
        }
        if (RelationColumnScheme._guidAttrs.Contains(num))
        {
          if (!this._guidAttrsTransforms.ContainsKey(num))
            this._guidAttrsTransforms.Add(num, new GuidAttributeTransform(num));
          return (INodeColumnTransform) this._guidAttrsTransforms[num];
        }
        if (RelationColumnScheme._dateTimeAttributeTypeIds.Contains(num))
          return (INodeColumnTransform) RelationColumnScheme._dateTimeNodeColumnTransform;
        if (RelationColumnScheme._objectLinkAttributeTypeIds.Contains(num))
          return (INodeColumnTransform) RelationColumnScheme._objectLinkNodeColumnTransform;
        if (RelationColumnScheme._doubleAttributeTypeIds.Contains(num))
          return (INodeColumnTransform) RelationColumnScheme._doubleNodeColumnTransform;
        if (!this._listAttrsTransforms.ContainsKey(num))
          this._listAttrsTransforms.Add(num, new ListAttributeTransform(num));
        return (INodeColumnTransform) this._listAttrsTransforms[num];
      }
    }
  }

  /// <summary>
  /// Создает виртуальную колонку с заданным направлением сортировки по
  /// указанному идентификатору. Если колонки с такми идентификатором в
  /// схеме нет - то метод вернет null.
  /// </summary>
  /// <param name="schemeGuid">Guid схемы</param>
  /// <param name="attrType">Описание атрибута, для которого создаётся колонка</param>
  /// <param name="sortOrder">Направление сортировки</param>
  /// <param name="sortIndex">Очерёдность сортировки (-1 - не сортируется)</param>
  /// <returns>Виртуальная колонка</returns>
  private NodeColumn CreateColumn(
    Guid schemeGuid,
    IMSAttributeType attrType,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    return new NodeColumn(schemeGuid, (object) attrType.AttributeID, Helper.ConvertType(attrType.RealFieldType), attrType.RealFieldType, attrType.Name + RelationColumnScheme.SchemeTypeName, sortOrder, sortIndex, attrType.ShortName + RelationColumnScheme.SchemeTypeName, attrType.Name + RelationColumnScheme.SchemeAttributeInfo, (attrType.Options & AttributeOptions.Internal) != 0);
  }
}
