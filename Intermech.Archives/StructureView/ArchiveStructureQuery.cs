// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureQuery
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.Archives.StructureView;

/// <summary>
/// для получения типов атрбутов, назначенный структуре
/// выбранного архива
/// </summary>
public class ArchiveStructureQuery : BaseNodeQuery
{
  /// <summary>Подготовка запроса к выполнению</summary>
  private INodeQuerySupport _support;
  /// <summary>Идентификатор архива, для которого выполняется запрос</summary>
  protected long _arсhiveID;
  /// <summary>Таблица с заполненными данными об атрибутах</summary>
  private DataTable _attributeInfoTable = new DataTable();
  /// <summary>Поля для сортировки</summary>
  public static readonly object[] FieldsOrder = new object[1]
  {
    (object) "F_ATTRIBUTE_ID"
  };
  private string _asc = " ASC";
  private string _desc = " DESC";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="arсhiveId">Идентификатор архива, для которой создаётся запрос</param>
  /// <param name="support">Подготовка запроса к выполнению</param>
  public ArchiveStructureQuery(long arсhiveId, INodeQuerySupport support)
  {
    this._support = support;
    this._arсhiveID = arсhiveId;
    foreach (string archiveStructureColumn in ConstsHolder.ArchiveStructureColumns)
    {
      if (archiveStructureColumn == "F_ATTRIBUTE_ID" || archiveStructureColumn == "F_SIZE_TYPE" || archiveStructureColumn == "F_MASTER_ID" || archiveStructureColumn == "F_SOURCE_ID")
        this._attributeInfoTable.Columns.Add(archiveStructureColumn, typeof (long));
      else
        this._attributeInfoTable.Columns.Add(archiveStructureColumn, typeof (string));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IArchiveService service = ServiceUtils.GetService<IArchiveService>((object) sessionKeeper.Session, true);
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this._arсhiveID, ConstsHolder.ArchiveStructureAttrID);
      if (objectAttributeById == null)
        return;
      AttributeProcessor attrProcessor = new AttributeProcessor(this._arсhiveID, AttributableElements.Object);
      attrProcessor.Load(this._arсhiveID, AttributableElements.Object, GetAttributeValuesModes.None, false);
      foreach (object obj in objectAttributeById.Values)
      {
        if (!DBNull.Value.Equals(obj) && GuidHelper.IsGuid(obj.ToString()))
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(Convert.ToString(obj)));
          if (attributeType != null)
          {
            string str1 = Convert.ToInt32(attributeType.LevelID).Equals(0) ? ServiceHolder.rm.GetString("Archives_35") : DataHolders.LevelsHolder.GetNamebyID(Convert.ToInt32(attributeType.LevelID));
            string str2 = Convert.ToBoolean(attributeType.IsContent) ? ServiceHolder.rm.GetString("Archives_36") : ServiceHolder.rm.GetString("Archives_37");
            Dictionary<Guid, object> defaultAttrValues = service.GetArchiveStructureDefaultAttrValues(this._arсhiveID, sessionKeeper.Session.SessionGUID);
            object defaultAttrValue = this.GetViewedDefaultAttrValue(attrProcessor, attributeType, defaultAttrValues);
            this._attributeInfoTable.Rows.Add((object) attributeType.AttributeID, (object) attributeType.Name, (object) attributeType.ShortName, (object) attributeType.Alias, (object) attributeType.Note, defaultAttrValue, (object) MultiValueModesHelper.GetCaption(attributeType.MultiValueMode), (object) AttributesTypeHelper.GetCaption(attributeType.RealFieldType), (object) attributeType.SizeType, (object) str1, (object) attributeType.Formula, (object) DataHolders.LanguagesHolder.GetNamebyID(attributeType.LanguageID), (object) attributeType.AttributeGuid, (object) DataHolders.SubjectAreasHolder.GetNamesbyIDs(attributeType.AreaID), (object) UniqueValueModesHelper.GetCaption(attributeType.Unique), (object) OptimizationModesHelper.GetCaption(attributeType.OptimizationMode), (object) str2, (object) AttributeOptionsHelper.GetCaptions(attributeType.Options), (object) attributeType.Mask, (object) attributeType.MasterAttributeID, (object) attributeType.SourceAttributeID);
          }
        }
      }
    }
  }

  /// <summary>
  /// Возвращает отображаемое значение по умолчанию для атрибута.
  /// </summary>
  /// <param name="attrProcessor">The attribute processor.</param>
  /// <param name="imsCurrentAttribute">Информация о типе атрибута.</param>
  /// <param name="defaultAttrValues">Словарь со специальными значениями атрибутов по умолчанию.</param>
  /// <returns>Отображаемое значение по умолчанию для атрибута</returns>
  private object GetViewedDefaultAttrValue(
    AttributeProcessor attrProcessor,
    IMSAttributeType imsCurrentAttribute,
    Dictionary<Guid, object> defaultAttrValues)
  {
    object initValue;
    object defaultAttrValue;
    if (defaultAttrValues.TryGetValue(imsCurrentAttribute.AttributeGuid, out initValue))
    {
      switch (imsCurrentAttribute.FieldType)
      {
        case FieldTypes.ftObjectLink:
          initValue = (object) Convert.ToInt64(initValue);
          break;
        case FieldTypes.ftBoolean:
          initValue = (object) Convert.ToBoolean(initValue);
          break;
      }
      defaultAttrValue = (object) attrProcessor.GetViewValue(new AttributeValues(imsCurrentAttribute.AttributeID, initValue));
    }
    else
      defaultAttrValue = imsCurrentAttribute.FieldType != FieldTypes.ftBoolean ? (object) imsCurrentAttribute.DefaultValue.ToString() : (Convert.ToBoolean(imsCurrentAttribute.DefaultValue) ? (object) ServiceHolder.rm.GetString("Archives_36") : (object) ServiceHolder.rm.GetString("Archives_37"));
    return defaultAttrValue;
  }

  /// <summary>
  /// Возвращает объект, помогающий подготовить запрос к выполнению и обработать
  /// его результаты.
  /// </summary>
  protected override INodeQuerySupport Support => this._support;

  /// <summary>
  /// Выполняет запрос на чтение порции дочерних элементов. Позиция для
  /// чтения определяется закладкой (bookmark). Если закладка = null,
  /// то будет прочитана первая порция, иначе будет прочитана порция с
  /// позиции, указанной в закладке.
  /// </summary>
  /// <param name="bookmark">Закладка, указывающая позицию для чтения</param>
  /// <param name="count">Количество записей в порции.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping)
  {
    this._attributeInfoTable.DefaultView.Sort = this.GetSortOrder(mapping);
    return new NodeQueryResult(this._attributeInfoTable.Rows.Count, this.TotalRecordCount, this.GetFieldsOrder(this._attributeInfoTable));
  }

  private string GetSortOrder(RecordMapping mapping)
  {
    if (mapping.SortFields == null)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append((string) mapping.SortFields[0]);
    stringBuilder.Append(mapping.SortOrders[0] == NodeColumnSortOrder.Ascending ? this._asc : this._desc);
    for (int index = 1; index < mapping.SortFields.Length; ++index)
    {
      stringBuilder.Append(',');
      stringBuilder.Append((string) mapping.SortFields[index]);
      stringBuilder.Append(mapping.SortOrders[index] == NodeColumnSortOrder.Ascending ? this._asc : this._desc);
    }
    return stringBuilder.ToString();
  }

  private object[] GetFieldsOrder(DataTable dataTable)
  {
    object[] fieldsOrder = new object[dataTable.Columns.Count];
    for (int index = 0; index < fieldsOrder.Length; ++index)
      fieldsOrder[index] = (object) dataTable.Columns[index].ColumnName;
    return fieldsOrder;
  }

  /// <summary>
  /// Читает сведения об указанных элементах источника данных.
  /// </summary>
  /// <param name="recordIds">Идентификаторы элементов источника данных.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping mapping)
  {
    return new NodeQueryResult(this._attributeInfoTable.Rows.Count, this.TotalRecordCount, ArchiveStructureQuery.FieldsOrder);
  }

  /// <summary>
  /// Возвращает запись, полученную из источника данных в результате выполнения запроса.
  /// </summary>
  /// <param name="index">Порядковый номер записи в порции</param>
  /// <returns>Массив значений полей записи</returns>
  protected override object[] GetFieldValues(int index)
  {
    return this._attributeInfoTable.DefaultView[index].Row.ItemArray;
  }
}
