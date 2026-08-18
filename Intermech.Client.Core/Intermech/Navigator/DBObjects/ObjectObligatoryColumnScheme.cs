
// Type: Intermech.Navigator.DBObjects.ObjectObligatoryColumnScheme
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
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Схема виртуальных колонок, описывающая обязательные атрибуты объектов.
/// Идентификаторы виртуальных колонок - это значения перечисления
/// ObligatoryObjectAttributes.
/// </summary>
public class ObjectObligatoryColumnScheme : INodeColumnScheme
{
  /// <summary>Коллекция преобразователей</summary>
  private IDictionary _transforms = (IDictionary) new HybridDictionary();
  /// <summary>Название схемы колонок</summary>
  private static readonly string _schemeName = LocalizationHolder.rm.GetString("Client.Core_308");

  /// <summary>
  /// Возвращает название схемы колонок, которое выводится в диалоге настройки
  /// колонок.
  /// </summary>
  public string Name => ObjectObligatoryColumnScheme._schemeName;

  /// <summary>
  /// Возвращает постоянное имя колонки, которое можно использовать
  /// для долговременного хранения (т.е. между сеансами работы
  /// универсального клиента).
  /// </summary>
  /// <param name="columnID">Идентификатор виртуальной колонки</param>
  /// <returns>Постоянное имя колонки</returns>
  public string ColumnIDToPersistName(object columnID)
  {
    return columnID is ObligatoryObjectAttributes ? ((int) columnID).ToString() : string.Empty;
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
    try
    {
      return (object) (ObligatoryObjectAttributes) int.Parse(persistName);
    }
    catch
    {
    }
    return (object) null;
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
    return columnID is ObligatoryObjectAttributes columnID1 ? this.CreateColumn(schemeGuid, columnID1, NodeColumnSortOrder.None, -1) : (NodeColumn) null;
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
    return columnID is ObligatoryObjectAttributes columnID1 ? this.CreateColumn(schemeGuid, columnID1, sortOrder, sortIndex) : (NodeColumn) null;
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
        this._transforms.Add((object) ObligatoryObjectAttributes.F_CREATOR_ID, (object) new UserNameTransform());
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
        this._transforms.Add((object) -81, (object) new UserNameTransform());
        this._transforms.Add((object) -14, (object) new ProjectNameTransform());
        this._transforms.Add((object) -7, (object) new ObjectTypeNameTransform());
        this._transforms.Add((object) -4, (object) new ObjectLCStepTransform());
        this._transforms.Add((object) -9, (object) new ObjectLevelIDTransform());
        this._transforms.Add((object) -17, (object) new SiteNameTransform());
        this._transforms.Add((object) ObligatoryObjectAttributes.F_ACCESS, (object) new AccessNodeColumnTransform());
        this._transforms.Add((object) -80, (object) new AccessNodeColumnTransform());
      }
      return this._transforms.Contains(columnID) ? (INodeColumnTransform) this._transforms[columnID] : (INodeColumnTransform) null;
    }
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
  private NodeColumn CreateColumn(
    Guid schemeGuid,
    ObligatoryObjectAttributes columnID,
    NodeColumnSortOrder sortOrder,
    int sortIndex)
  {
    return new NodeColumn(schemeGuid, (object) columnID, this.GetColumnType(columnID), this.GetColumnAttrType(columnID), ObligatoryObjectAttributesHelper.GetCaption(columnID), sortOrder, sortIndex);
  }

  /// <summary>Вернуть тип данных для колонки</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Тип данных для колонки</returns>
  private Type GetColumnType(ObligatoryObjectAttributes columnID) => Helper.GetColumnType(columnID);

  /// <summary>Вернуть тип данных FieldTypes для колонки</summary>
  /// <param name="columnID">Идентификатор колонки</param>
  /// <returns>Тип данных FieldTypes для колонки</returns>
  private FieldTypes GetColumnAttrType(ObligatoryObjectAttributes columnID)
  {
    return Helper.GetColumnAttrType(columnID);
  }
}
