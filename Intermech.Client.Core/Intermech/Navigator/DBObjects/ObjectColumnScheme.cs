
// Type: Intermech.Navigator.DBObjects.ObjectColumnScheme
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Схема виртуальных колонок, описывающая необязательные атрибуты объектов.
/// Идентификаторы виртуальных колонок - это имена атрибутов.
/// </summary>
public class ObjectColumnScheme : INodeColumnScheme
{
  /// <summary>Название схемы колонок</summary>
  private static readonly string SchemeName = LocalizationHolder.rm.GetString("Client.Core_298");
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
  private static List<int> _objectLinkAttributeIds = new List<int>();
  private static List<int> _dateTimeAttributeTypeIds = new List<int>();
  private static ObjectLinkColumnTransform _objectLinkColumnTransform = new ObjectLinkColumnTransform();
  private static DateTimeNodeColumnTransform _dateTimeNodeColumnTransform = new DateTimeNodeColumnTransform();
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
  /// <summary>Коллекция общих преобразователей</summary>
  private IDictionary _transforms = (IDictionary) new HybridDictionary();

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public virtual string Name => ObjectColumnScheme.SchemeName;

  /// <summary>
  /// Добавить атрибут во внутренние коллекции при необходимости
  /// </summary>
  /// <param name="attr"></param>
  protected void InternalAddAttribute(IMSAttributeType attr)
  {
    if (attr == null)
      return;
    if (attr.MultiValueMode == MultiValueModes.SingleValueFromList && !ObjectColumnScheme._listAttrs.Contains(attr.AttributeID))
    {
      lock (ObjectColumnScheme._listAttrs)
      {
        ObjectColumnScheme._listAttrs.Add(attr.AttributeID);
        if (!this._listAttrsTransforms.ContainsKey(attr.AttributeID))
          this._listAttrsTransforms.Add(attr.AttributeID, new ListAttributeTransform(attr.AttributeID));
      }
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftBoolean)
    {
      lock (ObjectColumnScheme._boolAttrs)
      {
        ObjectColumnScheme._boolAttrs.Add(attr.AttributeID);
        if (!this._boolAttrsTransforms.ContainsKey(attr.AttributeID))
          this._boolAttrsTransforms.Add(attr.AttributeID, new BoolAttributeTransform(attr.AttributeID));
      }
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftGuid)
    {
      lock (ObjectColumnScheme._guidAttrs)
      {
        ObjectColumnScheme._guidAttrs.Add(attr.AttributeID);
        if (!this._guidAttrsTransforms.ContainsKey(attr.AttributeID))
          this._guidAttrsTransforms.Add(attr.AttributeID, new GuidAttributeTransform(attr.AttributeID));
      }
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftObjectLink)
    {
      lock (ObjectColumnScheme._objectLinkAttributeIds)
        ObjectColumnScheme._objectLinkAttributeIds.Add(attr.AttributeID);
    }
    if (attr != null && attr.RealFieldType == FieldTypes.ftDateTime)
    {
      lock (ObjectColumnScheme._dateTimeAttributeTypeIds)
        ObjectColumnScheme._dateTimeAttributeTypeIds.Add(attr.AttributeID);
    }
    if (attr == null || attr.FieldType != FieldTypes.ftDouble)
      return;
    lock (ObjectColumnScheme._doubleAttributeTypeIds)
      ObjectColumnScheme._doubleAttributeTypeIds.Add(attr.AttributeID);
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
    lock (this._transforms)
    {
      if (this._transforms.Count == 0)
      {
        CaptionTransform captionTransform = new CaptionTransform();
        this._transforms.Add((object) ObligatoryObjectAttributes.CAPTION, (object) captionTransform);
        this._transforms.Add((object) "F_CAPTION", (object) captionTransform);
        this._transforms.Add((object) "CAPTION", (object) captionTransform);
        this._transforms.Add((object) ObligatoryObjectAttributes.F_CHKOUT_BY, (object) new UserNameTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_OWNER_ID, (object) new UserNameTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_USER_ID, (object) new UserNameTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_PROJECT_ID, (object) new ProjectNameTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, (object) new ObjectTypeNameTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_LC_STEP, (object) new ObjectLCStepTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_LEVEL_ID, (object) new ObjectLevelIDTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_SITE_ID, (object) new SiteNameTransform());
        this._transforms.Add((object) MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545"), (object) new SeriesDatesTransform());
        this._transforms.Add((object) -50, (object) captionTransform);
        this._transforms.Add((object) -6, (object) new UserNameTransform());
        this._transforms.Add((object) -8, (object) new UserNameTransform());
        this._transforms.Add((object) -36, (object) new UserNameTransform());
        this._transforms.Add((object) -14, (object) new ProjectNameTransform());
        this._transforms.Add((object) -7, (object) new ObjectTypeNameTransform());
        this._transforms.Add((object) -4, (object) new ObjectLCStepTransform());
        this._transforms.Add((object) -9, (object) new ObjectLevelIDTransform());
        this._transforms.Add((object) -17, (object) new SiteNameTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_ACCESS, (object) new AccessNodeColumnTransform());
        this._transforms.Add((object) -80, (object) new AccessNodeColumnTransform());
      }
    }
    if (columnID != null)
    {
      int num = (int) columnID;
      lock (ObjectColumnScheme._listAttrs)
      {
        lock (ObjectColumnScheme._boolAttrs)
        {
          if (!ObjectColumnScheme._listAttrs.Contains(num) && !ObjectColumnScheme._boolAttrs.Contains(num) && !ObjectColumnScheme._guidAttrs.Contains(num) && !ObjectColumnScheme._objectLinkAttributeIds.Contains(num) && !ObjectColumnScheme._dateTimeAttributeTypeIds.Contains(num) && !ObjectColumnScheme._doubleAttributeTypeIds.Contains(num))
          {
            lock (this._transforms)
            {
              if (this._transforms.Contains(columnID))
                return (INodeColumnTransform) this._transforms[columnID];
            }
            return (INodeColumnTransform) null;
          }
          if (ObjectColumnScheme._boolAttrs.Contains(num))
          {
            if (!this._boolAttrsTransforms.ContainsKey(num))
              this._boolAttrsTransforms.Add(num, new BoolAttributeTransform(num));
            return (INodeColumnTransform) this._boolAttrsTransforms[num];
          }
          if (ObjectColumnScheme._guidAttrs.Contains(num))
          {
            if (!this._guidAttrsTransforms.ContainsKey(num))
              this._guidAttrsTransforms.Add(num, new GuidAttributeTransform(num));
            return (INodeColumnTransform) this._guidAttrsTransforms[num];
          }
          if (ObjectColumnScheme._listAttrs.Contains(num))
          {
            if (!this._listAttrsTransforms.ContainsKey(num))
              this._listAttrsTransforms.Add(num, new ListAttributeTransform(num));
            return (INodeColumnTransform) this._listAttrsTransforms[num];
          }
          if (ObjectColumnScheme._objectLinkAttributeIds.Contains(num))
            return (INodeColumnTransform) ObjectColumnScheme._objectLinkColumnTransform;
          if (ObjectColumnScheme._dateTimeAttributeTypeIds.Contains(num))
            return (INodeColumnTransform) ObjectColumnScheme._dateTimeNodeColumnTransform;
          if (ObjectColumnScheme._doubleAttributeTypeIds.Contains(num))
            return (INodeColumnTransform) ObjectColumnScheme._doubleNodeColumnTransform;
        }
      }
    }
    return (INodeColumnTransform) null;
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
    return new NodeColumn(schemeGuid, (object) attrType.AttributeID, Helper.ConvertType(attrType.RealFieldType), attrType.RealFieldType, attrType.Name, sortOrder, sortIndex, attrType.ShortName, attrType.Name, (attrType.Options & AttributeOptions.Internal) != 0);
  }
}
