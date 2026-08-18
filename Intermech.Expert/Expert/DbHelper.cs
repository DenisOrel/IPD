// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.DbHelper
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Expert;

/// <summary>
/// Статические функции для работы с ConditionStructure и ColumnDescriptor'ами.
/// Вынес все в одно место, поскольку надоело каждый раз задаваться вопросом,
/// как сделать очередной элементарный поиск в базе.
/// </summary>
public static class DbHelper
{
  /// <summary>
  /// Вернуть ConditionStructure для поиска объектов одного типа
  /// </summary>
  /// <param name="objType">Искомый тип объекта</param>
  /// <returns>ConditionStructure для поиска объектов заданного типа</returns>
  public static ConditionStructure CS_FindObjectType(int objType)
  {
    return new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.Equal, (object) objType, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text);
  }

  /// <summary>
  /// Вернуть ConditionStructure для поиска объектов нескольких типов
  /// </summary>
  /// <param name="objTypes">Искомые типы объектов</param>
  /// <returns>ConditionStructure для поиска объектов заданных типов</returns>
  public static ConditionStructure CS_FindObjectTypes(IEnumerable<int> objTypes)
  {
    return new ConditionStructure(ExpertConsts.Consts.attrObjectType, RelationalOperators.In, (object) objTypes.ToArray<int>(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.Text);
  }

  /// <summary>
  /// Вернуть ConditionStructure для поиска по связям одного типа
  /// </summary>
  /// <param name="objType">Искомый тип связи</param>
  /// <returns>ConditionStructure для поиска по связям заданного типа</returns>
  public static ConditionStructure CS_RelationType(int relType)
  {
    return new ConditionStructure(-23, RelationalOperators.Equal, (object) relType, (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value);
  }

  /// <summary>
  /// Вернуть ConditionStructure для поиска по связям нескольких типов
  /// </summary>
  /// <param name="objType">Искомые типы связей</param>
  /// <returns>ConditionStructure для поиска по связям заданных типов</returns>
  public static ConditionStructure CS_RelationTypes(IEnumerable<int> relTypes)
  {
    return new ConditionStructure(-23, RelationalOperators.In, (object) relTypes.ToArray<int>(), (object) 0, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Relation, ColumnContents.Value);
  }

  public static ColumnContents FldType2ColContents(FieldTypes ft, out bool measured)
  {
    measured = false;
    switch (ft)
    {
      case FieldTypes.ftDateTime:
        return ColumnContents.Text;
      case FieldTypes.ftShortBlob:
      case FieldTypes.ftBlob:
      case FieldTypes.ftSystem:
        return ColumnContents.Value;
      case FieldTypes.ftFile:
      case FieldTypes.ftExternalLink:
      case FieldTypes.ftObjectLink:
      case FieldTypes.ftObjectLinkByID:
        return ColumnContents.ID;
      case FieldTypes.ftMeasured:
        measured = true;
        return ColumnContents.Text;
      default:
        return ColumnContents.Text;
    }
  }

  /// <summary>Возвращает вид данных (НЕ ТИП) для колонки.</summary>
  /// <param name="attrGuid">Строковое представление GUID'а атрибута</param>
  /// <param name="measured">Возвращает, является ли атрибут числом с плав. точкой в единицах измерения</param>
  /// <returns>Вид данных (ColumnContents) или ColumnContents.Date, если атрибута нет</returns>
  public static ColumnContents GetColumnContents(string attrGuid, out bool measured)
  {
    measured = false;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(attrGuid));
    return attributeType != null ? DbHelper.FldType2ColContents(attributeType.FieldType, out measured) : ColumnContents.Date;
  }

  /// <summary>Возвращает вид данных (НЕ ТИП) для колонки.</summary>
  /// <param name="attrId">Идентификатор атрибута</param>
  /// <param name="measured">Возвращает, является ли атрибут числом с плав. точкой в единицах измерения</param>
  /// <returns>Вид данных (ColumnContents) или ColumnContents.Date, если атрибута нет</returns>
  public static ColumnContents GetColumnContents(int attrId, out bool measured)
  {
    measured = false;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
    return attributeType != null ? DbHelper.FldType2ColContents(attributeType.FieldType, out measured) : ColumnContents.Date;
  }

  /// <summary>Создать ColumnDescriptor с именованием по Guid.</summary>
  /// <param name="attrGuid">Строка GUID'а</param>
  /// <param name="attrforRel">true, если атрибут для связи, False, если для объекта</param>
  /// <param name="orderBy">Порядок сортировки</param>
  /// <returns>ColumnDescriptor</returns>
  public static ColumnDescriptor MakeColumnDescriptor(
    string attrGuid,
    bool attrforRel,
    int orderBy)
  {
    bool measured;
    ColumnContents columnContents = DbHelper.GetColumnContents(attrGuid, out measured);
    return new ColumnDescriptor((object) new Guid(attrGuid), attrforRel ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : orderBy);
  }

  /// <summary>Создать ColumnDescriptor с именованием по Guid.</summary>
  /// <param name="attrGuid">GUID</param>
  /// <param name="attrforRel">true, если атрибут для связи, False, если для объекта</param>
  /// <param name="orderBy">Порядок сортировки</param>
  /// <returns>ColumnDescriptor</returns>
  public static ColumnDescriptor MakeColumnDescriptor(Guid attrGuid, bool attrforRel, int orderBy)
  {
    bool measured;
    ColumnContents columnContents = DbHelper.GetColumnContents(attrGuid.ToString(), out measured);
    return new ColumnDescriptor((object) attrGuid, attrforRel ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : orderBy);
  }

  /// <summary>Создать ColumnDescriptor с именованием по Guid.</summary>
  /// <param name="attrId">ИД атрибута</param>
  /// <param name="attrforRel">true, если атрибут для связи, False, если для объекта</param>
  /// <param name="orderBy">Порядок сортировки</param>
  /// <returns>ColumnDescriptor</returns>
  public static ColumnDescriptor MakeColumnDescriptor(int attrId, bool attrforRel, int orderBy)
  {
    bool measured;
    ColumnContents columnContents = DbHelper.GetColumnContents(attrId, out measured);
    return new ColumnDescriptor((object) attrId, attrforRel ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : orderBy);
  }

  public static List<ColumnDescriptor> DefaultDescriptors()
  {
    return new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.ASC, 1),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.ASC, 2)
    };
  }

  public static ConditionStructure[] CondToArray(ConditionStructure cs)
  {
    return new ConditionStructure[1]{ cs };
  }

  /// <summary>
  /// Создать DBRecordSetParams из условий и дескрипторов столбцов
  /// </summary>
  /// <param name="conds">Набор условий</param>
  /// <param name="descs">Набор дескрипторов столбцов</param>
  /// <returns>DBRecordSetParams с заданными условиями и столбцами</returns>
  public static DBRecordSetParams MakeParams(
    IEnumerable<ConditionStructure> conds,
    IEnumerable<ColumnDescriptor> descs)
  {
    return new DBRecordSetParams(conds != null ? conds.ToArray<ConditionStructure>() : (ConditionStructure[]) null, descs.ToArray<ColumnDescriptor>());
  }

  /// <summary>
  /// Создать DBRecordSetParams без условий, только из дескрипторов столбцов
  /// </summary>
  /// <param name="descs">Набор дескрипторов столбцов</param>
  /// <returns>DBRecordSetParams с заданными столбцами</returns>
  public static DBRecordSetParams MakeParams(IEnumerable<ColumnDescriptor> descs)
  {
    return new DBRecordSetParams((ConditionStructure[]) null, descs.ToArray<ColumnDescriptor>());
  }

  /// <summary>
  /// Создать DBRecordSetParams без условий, только с двумя столбцами - ObjectID и Caption
  /// </summary>
  /// <returns>DBRecordSetParams</returns>
  public static DBRecordSetParams MakeParams()
  {
    return new DBRecordSetParams((ConditionStructure[]) null, DbHelper.DefaultDescriptors().ToArray());
  }

  /// <summary>
  /// Получение состава объекта по заданным параметрам (метод самого нижнего уровня)
  /// </summary>
  /// <param name="coll">Коллекция связей, по которым ведем поиск</param>
  /// <param name="objId">ИД родительского объекта</param>
  /// <param name="dbrsp">Параметры поиска</param>
  /// <returns>Таблица данных с составом объекта</returns>
  public static DataTable GetChilds(
    IDBRelationCollection coll,
    long objId,
    DBRecordSetParams dbrsp)
  {
    return coll.EntersInVersion(dbrsp, objId);
  }

  /// <summary>
  /// Получение состава объекта без условий, но с набором полей
  /// </summary>
  /// <param name="coll">Коллекция связей, по которым ведем поиск</param>
  /// <param name="objId">ИД родительского объекта</param>
  /// <param name="descs">Набор дескрипторов столбцов</param>
  /// <returns>Таблица данных с составом объекта</returns>
  public static DataTable GetChilds(
    IDBRelationCollection coll,
    long objId,
    IEnumerable<ColumnDescriptor> descs)
  {
    return coll.EntersInVersion(DbHelper.MakeParams(descs), objId);
  }

  /// <summary>
  /// Получение состава объекта без условий, но с набором полей
  /// </summary>
  /// <param name="coll">Коллекция связей, по которым ведем поиск</param>
  /// <param name="objId">ИД родительского объекта</param>
  /// <param name="objTypeId">ИД типа объекта</param>
  /// <param name="descs">Набор дескрипторов столбцов</param>
  /// <returns>Таблица данных с составом объекта</returns>
  public static DataTable GetChilds(
    IDBRelationCollection coll,
    long objId,
    int objTypeId,
    IEnumerable<ColumnDescriptor> descs)
  {
    return coll.EntersInVersion(DbHelper.MakeParams((IEnumerable<ConditionStructure>) DbHelper.CondToArray(DbHelper.CS_FindObjectType(objTypeId)), descs), objId);
  }

  /// <summary>
  /// Получение состава объекта без условий, но с набором полей
  /// </summary>
  /// <param name="coll">Коллекция связей, по которым ведем поиск</param>
  /// <param name="objId">ИД родительского объекта</param>
  /// <param name="objTypeId">ИД типа объекта</param>
  /// <returns>Таблица данных с составом объекта</returns>
  public static DataTable GetChilds(IDBRelationCollection coll, long objId, int objTypeId)
  {
    return coll.EntersInVersion(DbHelper.MakeParams((IEnumerable<ConditionStructure>) DbHelper.CondToArray(DbHelper.CS_FindObjectType(objTypeId)), (IEnumerable<ColumnDescriptor>) DbHelper.DefaultDescriptors()), objId);
  }

  public static void DeleteCond(List<ConditionStructure> conds, int index)
  {
    ConditionStructure cond1 = conds[index];
    if (cond1.GroupID > 0 && index < conds.Count - 1)
    {
      ConditionStructure cond2 = conds[index + 1];
      cond2.GroupID += cond1.GroupID;
      conds[index + 1] = cond2;
    }
    if (cond1.GroupID < 0 && index > 0)
    {
      ConditionStructure cond3 = conds[index - 1];
      cond3.GroupID += cond1.GroupID;
      conds[index - 1] = cond3;
    }
    if (index > 0)
    {
      ConditionStructure cond4 = conds[index - 1];
      if (cond4.LogicalOperator == LogicalOperators.AND && cond1.LogicalOperator == LogicalOperators.NONE)
      {
        cond4.LogicalOperator = LogicalOperators.NONE;
        conds[index - 1] = cond4;
      }
    }
    conds.RemoveAt(index);
  }

  /// <summary>Скопировать файл из исходного объекта в целевой</summary>
  /// <param name="srcObj">Исходный объект</param>
  /// <param name="dstObj">Результирующий объект</param>
  public static void CopyFile(IDBObject srcObj, IDBObject dstObj)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
    if (srcObj.GetAttributeByID(attributeTypeId) == null)
      return;
    using (MemoryStream memoryStream = new MemoryStream())
    {
      BlobProcReader blobProcReader = new BlobProcReader(srcObj.ObjectID, AttributableElements.Object, attributeTypeId, 0, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
      blobProcReader.ReadData(srcObj.Session);
      string str = Path.GetExtension(blobProcReader.BlobInformation.FileName);
      memoryStream.Position = 0L;
      new BlobProcWriter(dstObj.Attributes.AddAttribute(attributeTypeId, false), 0, new BlobInformation(memoryStream.Length, 0L, DateTime.Now, Math.Abs(dstObj.ObjectID).ToString() + str, ArcMethods.ZLibPacked, ""), (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(dstObj.Session);
    }
  }

  /// <summary>
  /// Скопировать набор атрибутов из одного объекта в другой
  /// </summary>
  /// <param name="srcObj">Исходный объект</param>
  /// <param name="dstObj"></param>
  /// <param name="attrGuids"></param>
  public static void CopyAttrs(IDBObject srcObj, IDBObject dstObj, IEnumerable<string> attrGuids)
  {
    if (attrGuids == null || attrGuids.Count<string>() == 0)
      return;
    AttributeValues[] attributesValues = srcObj.GetAttributesValues(GetAttributeValuesModes.IncludeBlobs | GetAttributeValuesModes.IncludeObligatoryAttributes);
    HashSet<int> intSet = new HashSet<int>();
    foreach (string attrGuid in attrGuids)
      intSet.Add(MetaDataHelper.GetAttributeTypeID(new Guid(attrGuid)));
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AttributeValues attributeValues in attributesValues)
    {
      if (intSet.Contains(attributeValues.AttributeID))
        attributeValuesList.Add(attributeValues);
    }
    dstObj.SetAttributesValuesEx(attributeValuesList.ToArray(), false, true, false, GetAttributeValuesModes.IncludeObligatoryAttributes);
  }
}
