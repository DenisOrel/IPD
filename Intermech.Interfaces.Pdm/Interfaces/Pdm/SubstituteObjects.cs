// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.SubstituteObjects
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using Intermech.Pdm.Substitutes;
using Intermech.Search.Pdm.Substitutes;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Класс, позволяющий создавать список групп заменителей в составе
/// </summary>
[Serializable]
public class SubstituteObjects : ICloneable
{
  public const long UnknownGroupNumber = -1;
  public const long UnknownSubstituteNumber = -1;
  public const long UnknownPositionNumber = -1;
  /// <summary>Номер группы заменителей</summary>
  private static int _substituteGroupNo = -1;
  /// <summary>Номер заменителя в группе</summary>
  private static int _substituteInGroup = -1;
  /// <summary>Имя группы заменителей</summary>
  private static int _substituteGroupName = -1;
  /// <summary>Имя заменителя</summary>
  private static int _substituteName = -1;
  /// <summary>Конструкторский основной вариант</summary>
  private static int _designActualVariant = -1;
  /// <summary>Обозначение</summary>
  private static int _designation = -1;
  /// <summary>Наименование</summary>
  private static int _name = -1;
  /// <summary>Количество</summary>
  private static int _quantity = -1;
  /// <summary>Позиция</summary>
  private static int _position = -1;
  /// <summary>Сортировка</summary>
  private static int _sorting = -1;
  /// <summary>Зона</summary>
  private static int _zone = -1;
  /// <summary>Примечание</summary>
  private static int _remark = -1;
  /// <summary>Условия применения объекта</summary>
  private static int _applicabilities = -1;
  /// <summary>Список идентификаторов атрибутов</summary>
  private static List<int> _attrs = new List<int>();
  /// <summary>Список GUID атрибутов</summary>
  private static List<string> _attrsGUID = new List<string>();
  /// <summary>
  /// Список идентификаторов атрибутов, по которым требуется выполнять сравнение связей в исполнениях (спц. формы А)
  /// </summary>
  private static List<int> _attrsToCompareArtRelations = new List<int>();
  /// <summary>
  /// Список идентификаторов атрибутов, по которым требуется выполнять сравнение связей в исполнениях (спц. формы Б)
  /// </summary>
  private static List<int> _attrsToCompareArtRelationsFormB = new List<int>();
  /// <summary>
  /// Коллекция колонок, которая используется для работы составов с допустимыми заменами
  /// </summary>
  private static List<ColumnDescriptor> _attrColumns = new List<ColumnDescriptor>();
  /// <summary>
  /// Коллекция колонок для сравнения составов. Колонки в списке
  /// именуются идентификаторам атрибутов
  /// </summary>
  private static List<ColumnDescriptor> _compareArtRelationsColumns = new List<ColumnDescriptor>();
  /// <summary>
  /// Колонки атрибутов в составе исполнений.
  /// [(Int32)ID атрибута] = [(Int32)Номер колонки атрибута в таблице состава исполнений]
  /// </summary>
  private static Dictionary<int, int> _compareArtRelationsAttrsIndex = new Dictionary<int, int>();
  /// <summary>
  /// Колонки атрибутов в составе
  /// [(Int32)ID атрибута] = [(Int32)Номер колонки атрибута в таблице состава]
  /// </summary>
  private static Dictionary<int, int> _attrsIndex = new Dictionary<int, int>();
  /// <summary>
  /// Колонки атрибутов в составе
  /// [(string)Guid атрибута] = [(Int32)Номер колонки атрибута в таблице состава]
  /// </summary>
  private static Dictionary<string, int> _attrsGUIDIndex = new Dictionary<string, int>();
  /// <summary>
  /// Список групп заменителей в составе
  /// [(Int64)Номер группы заменителей] = [(List[List[Int64]])Списки заменителей в виде связей]
  /// Нулевой заменитель - List[0][List[Int64]]- актуальный
  /// </summary>
  private Dictionary<long, List<List<long>>> _items = new Dictionary<long, List<List<long>>>();
  /// <summary>
  /// Список номеров групп заменителей в составе (требуется для работы VirtualTree)
  /// </summary>
  private List<long> _groups = new List<long>();
  /// <summary>
  /// Кэш для быстрого поиска версии дочернего объекта у связи
  /// [(Int64)Идентификатор связи] = [(Int64)Идентификатор версии дочернего объекта]
  /// </summary>
  private Dictionary<long, long> _relationObjects = new Dictionary<long, long>();
  /// <summary>
  /// Список атрибутов связей, участвующих в допустимых заменах
  /// </summary>
  private RelationAttributesPackage _relAttrs;
  /// <summary>Названия групп заменителей</summary>
  private Dictionary<long, string> _groupNames = new Dictionary<long, string>();
  private Dictionary<long, string> _remarks = new Dictionary<long, string>();
  /// <summary>Счётчик групп</summary>
  internal static long _groupNameCounter = 1;

  /// <summary>Инициализировать статические поля</summary>
  /// <param name="session">Сессия</param>
  public static void InitStaticFields(IUserSession session)
  {
    if (session == null || SubstituteObjects._substituteGroupNo != -1)
      return;
    SubstituteObjects._substituteGroupNo = session.IdentHelper.GetAttributeID("cad001c0-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._substituteInGroup = session.IdentHelper.GetAttributeID("cad001c1-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._substituteGroupName = session.IdentHelper.GetAttributeID("cad00817-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._substituteName = session.IdentHelper.GetAttributeID("cad00818-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._designActualVariant = session.IdentHelper.GetAttributeID("cad00654-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._designation = session.IdentHelper.GetAttributeID("cad0001f-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._name = session.IdentHelper.GetAttributeID("cad00020-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._quantity = session.IdentHelper.GetAttributeID("cad00267-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._position = session.IdentHelper.GetAttributeID("cad00270-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._sorting = session.IdentHelper.GetAttributeID("cad00202-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._zone = session.IdentHelper.GetAttributeID("cad0027a-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._remark = session.IdentHelper.GetAttributeID("cad00021-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._applicabilities = MetaDataHelper.GetAttributeTypeID("cad015ac-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Clear();
    SubstituteObjects._attrsGUID.Clear();
    SubstituteObjects._attrs.Add(SubstituteObjects._substituteGroupNo);
    SubstituteObjects._attrsGUID.Add("cad001c0-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._substituteInGroup);
    SubstituteObjects._attrsGUID.Add("cad001c1-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._substituteGroupName);
    SubstituteObjects._attrsGUID.Add("cad00817-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._substituteName);
    SubstituteObjects._attrsGUID.Add("cad00818-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._designActualVariant);
    SubstituteObjects._attrsGUID.Add("cad00654-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._designation);
    SubstituteObjects._attrsGUID.Add("cad0001f-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._name);
    SubstituteObjects._attrsGUID.Add("cad00020-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._position);
    SubstituteObjects._attrsGUID.Add("cad00270-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._quantity);
    SubstituteObjects._attrsGUID.Add("cad00267-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._sorting);
    SubstituteObjects._attrsGUID.Add("cad00202-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._zone);
    SubstituteObjects._attrsGUID.Add("cad0027a-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstituteObjects._remark);
    SubstituteObjects._attrsGUID.Add("cad00021-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(-7);
    SubstituteObjects._attrsGUID.Add("cad0002e-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(-50);
    SubstituteObjects._attrsGUID.Add("cad00047-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(-3);
    SubstituteObjects._attrsGUID.Add("cad0002a-306c-11d8-b4e9-00304f19f545");
    SubstituteObjects._attrs.Add(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID);
    SubstituteObjects._attrsGUID.Add(SubstitutesConstants.SubstitutePositionTypeAttributeTypeGuid.ToString());
    SubstituteObjects._attrs.Add(SubstitutesConstants.PositionDesignationAttributeTypeID);
    SubstituteObjects._attrsGUID.Add(SubstitutesConstants.PositionDesignationAttributeTypeGuid.ToString());
    SubstituteObjects._attrs.Add(SubstitutesConstants.PositionNumberAttributeTypeID);
    SubstituteObjects._attrsGUID.Add(SubstitutesConstants.PositionNumberAttributeTypeGuid.ToString());
    SubstituteObjects._attrsIndex.Clear();
    SubstituteObjects._attrsGUIDIndex.Clear();
    SubstituteObjects._attrsIndex.Add(-2, 0);
    SubstituteObjects._attrsGUIDIndex.Add("cad00029-306c-11d8-b4e9-00304f19f545", 0);
    SubstituteObjects._attrsIndex.Add(-22, 1);
    SubstituteObjects._attrsGUIDIndex.Add("cad00035-306c-11d8-b4e9-00304f19f545", 1);
    SubstituteObjects._attrsIndex.Add(-20, 2);
    SubstituteObjects._attrsGUIDIndex.Add("cad00033-306c-11d8-b4e9-00304f19f545", 2);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad001c0-306c-11d8-b4e9-00304f19f545"), 3);
    SubstituteObjects._attrsGUIDIndex.Add("cad001c0-306c-11d8-b4e9-00304f19f545", 3);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad001c1-306c-11d8-b4e9-00304f19f545"), 4);
    SubstituteObjects._attrsGUIDIndex.Add("cad001c1-306c-11d8-b4e9-00304f19f545", 4);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00817-306c-11d8-b4e9-00304f19f545"), 5);
    SubstituteObjects._attrsGUIDIndex.Add("cad00817-306c-11d8-b4e9-00304f19f545", 5);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00818-306c-11d8-b4e9-00304f19f545"), 6);
    SubstituteObjects._attrsGUIDIndex.Add("cad00818-306c-11d8-b4e9-00304f19f545", 6);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00654-306c-11d8-b4e9-00304f19f545"), 7);
    SubstituteObjects._attrsGUIDIndex.Add("cad00654-306c-11d8-b4e9-00304f19f545", 7);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), 8);
    SubstituteObjects._attrsGUIDIndex.Add("cad00267-306c-11d8-b4e9-00304f19f545", 8);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), 9);
    SubstituteObjects._attrsGUIDIndex.Add("cad0001f-306c-11d8-b4e9-00304f19f545", 9);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), 10);
    SubstituteObjects._attrsGUIDIndex.Add("cad00020-306c-11d8-b4e9-00304f19f545", 10);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545"), 11);
    SubstituteObjects._attrsGUIDIndex.Add("cad00270-306c-11d8-b4e9-00304f19f545", 11);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), 12);
    SubstituteObjects._attrsGUIDIndex.Add("cad00202-306c-11d8-b4e9-00304f19f545", 12);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545"), 13);
    SubstituteObjects._attrsGUIDIndex.Add("cad0027a-306c-11d8-b4e9-00304f19f545", 13);
    SubstituteObjects._attrsIndex.Add(MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545"), 14);
    SubstituteObjects._attrsGUIDIndex.Add("cad00021-306c-11d8-b4e9-00304f19f545", 14);
    SubstituteObjects._attrsIndex.Add(-7, 15);
    SubstituteObjects._attrsGUIDIndex.Add("cad0002e-306c-11d8-b4e9-00304f19f545", 15);
    SubstituteObjects._attrsIndex.Add(-50, 16 /*0x10*/);
    SubstituteObjects._attrsGUIDIndex.Add("cad00047-306c-11d8-b4e9-00304f19f545", 16 /*0x10*/);
    SubstituteObjects._attrsIndex.Add(-3, 17);
    SubstituteObjects._attrsGUIDIndex.Add("cad0002a-306c-11d8-b4e9-00304f19f545", 17);
    SubstituteObjects._attrsIndex.Add(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, 18);
    SubstituteObjects._attrsGUIDIndex.Add(SubstitutesConstants.SubstitutePositionTypeAttributeTypeGuid.ToString(), 18);
    SubstituteObjects._attrsIndex.Add(SubstitutesConstants.PositionDesignationAttributeTypeID, 19);
    SubstituteObjects._attrsGUIDIndex.Add(SubstitutesConstants.PositionDesignationAttributeTypeGuid.ToString(), 19);
    SubstituteObjects._attrsIndex.Add(SubstitutesConstants.PositionNumberAttributeTypeID, 20);
    SubstituteObjects._attrsGUIDIndex.Add(SubstitutesConstants.PositionNumberAttributeTypeGuid.ToString(), 20);
    SubstituteObjects._attrColumns.Clear();
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad001c0-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad001c1-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00817-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00818-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00654-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 3));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) SubstitutesConstants.PositionDesignationAttributeTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1));
    SubstituteObjects._attrColumns.Add(new ColumnDescriptor((object) SubstitutesConstants.PositionNumberAttributeTypeID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 2));
    SubstituteObjects._attrsToCompareArtRelations.Add(-22);
    SubstituteObjects._attrsToCompareArtRelations.Add(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelations.Add(MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelations.Add(MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelations.Add(MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelations.Add(MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelationsFormB.Add(-22);
    SubstituteObjects._attrsToCompareArtRelationsFormB.Add(MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelationsFormB.Add(MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelationsFormB.Add(MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545"));
    SubstituteObjects._attrsToCompareArtRelationsFormB.Add(MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545"));
    List<int> intList = new List<int>();
    SubstituteObjects._compareArtRelationsColumns.Clear();
    SubstituteObjects._compareArtRelationsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0));
    intList.Add(-21);
    SubstituteObjects._compareArtRelationsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PART_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 1));
    intList.Add(-22);
    SubstituteObjects._compareArtRelationsColumns.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, -1));
    intList.Add(-20);
    for (int index = 0; index < SubstituteObjects._attrsToCompareArtRelations.Count; ++index)
    {
      if (intList.IndexOf(SubstituteObjects._attrsToCompareArtRelations[index]) < 0)
      {
        SubstituteObjects._compareArtRelationsColumns.Add(new ColumnDescriptor((object) SubstituteObjects._attrsToCompareArtRelations[index], AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, -1));
        intList.Add(SubstituteObjects._attrsToCompareArtRelations[index]);
      }
    }
    SubstituteObjects._compareArtRelationsAttrsIndex.Clear();
    for (int index = 0; index < SubstituteObjects._compareArtRelationsColumns.Count; ++index)
      SubstituteObjects._compareArtRelationsAttrsIndex[(int) SubstituteObjects._compareArtRelationsColumns[index].AttributeID] = index;
  }

  /// <summary>Создать экземпляр объекта</summary>
  public SubstituteObjects()
  {
    this._relAttrs = new RelationAttributesPackage(SubstituteObjects._attrs);
  }

  /// <summary>
  /// Создать экземпляр объекта, задать список атрибутов, которые разрешено записывать в базу данных
  /// </summary>
  /// <param name="writeableAttributes">Список атрибутов, которые можно записывать в базу данных.
  /// Если список пуст или равен null, то все атрибуты можно записывать в базу данных</param>
  public SubstituteObjects(List<int> writeableAttributes)
  {
    this._relAttrs = new RelationAttributesPackage(SubstituteObjects._attrs, writeableAttributes);
  }

  /// <summary>Создать экземпляр объекта, инициализировать все поля</summary>
  /// <param name="session">Сессия (проверок на null не выполняется)</param>
  public SubstituteObjects(IUserSession session)
  {
    SubstituteObjects.InitStaticFields(session);
    this._relAttrs = new RelationAttributesPackage(SubstituteObjects._attrs);
  }

  /// <summary>Создать экземпляр объекта, инициализировать все поля</summary>
  /// <param name="session">Сессия (проверок на null не выполняется)</param>
  /// <param name="writeableAttributes">Список атрибутов, которые можно записывать в базу данных.
  /// Если список пуст или равен null, то все атрибуты можно записывать в базу данных</param>
  public SubstituteObjects(IUserSession session, List<int> writeableAttributes)
  {
    SubstituteObjects.InitStaticFields(session);
    this._relAttrs = new RelationAttributesPackage(SubstituteObjects._attrs, writeableAttributes);
  }

  /// <summary>
  /// 
  /// </summary>
  public virtual Dictionary<long, string> Remarks
  {
    get => this._remarks;
    set => this._remarks = new Dictionary<long, string>((IDictionary<long, string>) value);
  }

  /// <summary>
  /// Список идентификаторов атрибутов, которые надо записывать в базу данных.
  /// Если список пустой или null, то в базу будут записаны все атрибуты
  /// </summary>
  public virtual List<int> WriteableAttributes
  {
    get => this._relAttrs.WriteableAttributes;
    set
    {
      if (this._relAttrs == null)
        return;
      this._relAttrs.WriteableAttributes = value;
    }
  }

  /// <summary>Количество групп заменителей</summary>
  public virtual int Count => this._items.Count;

  /// <summary>
  /// Список номеров групп заменителей в составе (требуется для работы VirtualTree)
  /// </summary>
  public virtual List<long> Groups
  {
    [DebuggerStepThrough] get => this._groups;
  }

  /// <summary>Вернуть список заменителей в указанной группе</summary>
  /// <param name="Group">Номер группы заменителей</param>
  /// <returns>Список заменителей в указанной группе или исключение</returns>
  public virtual List<List<long>> this[long Group]
  {
    get => !this._items.ContainsKey(Group) ? (List<List<long>>) null : this._items[Group];
  }

  /// <summary>
  /// Список групп заменителей в составе
  /// [(Int64)Номер группы заменителей] = [(List[List[Int64]])Списки заменителей в виде связей]
  /// Нулевой заменитель - List[0][List[Int64]]- актуальный
  /// </summary>
  public Dictionary<long, List<List<long>>> Items
  {
    [DebuggerStepThrough] get => this._items;
  }

  /// <summary>
  /// Вернуть список связей указанного заменителя в указанной группе
  /// </summary>
  /// <param name="Group">Номер группы заменителей</param>
  /// <param name="SubstInGroup">Номер заменителя в группе</param>
  /// <returns>Список связей указанного заменителя в указанной группе или исключение</returns>
  public virtual List<long> this[long Group, long SubstInGroup]
  {
    get
    {
      if (SubstInGroup < 0L)
        throw new ArgumentException();
      List<List<long>> longListList = (List<List<long>>) null;
      return this._items.TryGetValue(Group, out longListList) && SubstInGroup <= (long) (longListList.Count - 1) ? longListList[(int) SubstInGroup] : new List<long>(0);
    }
  }

  /// <summary>
  /// Кэш для быстрого поиска версии дочернего объекта у связи
  /// [(Int64)Идентификатор связи] = [(Int64)Идентификатор версии дочернего объекта]
  /// </summary>
  public virtual Dictionary<long, long> RelationObjects
  {
    [DebuggerStepThrough] get => this._relationObjects;
  }

  /// <summary>
  /// Копия пакета атрибутов связей, участвующих в допустимых заменах.
  /// Из оригинальных данных будут удалены все ссылки на отсутствующие
  /// в группах связи.
  /// </summary>
  public virtual RelationAttributesPackage RelationAttributes
  {
    get
    {
      RelationAttributesPackage relationAttributes = new RelationAttributesPackage(SubstituteObjects._attrs);
      foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
      {
        long key = keyValuePair.Key;
        object obj1 = this._groupNames.ContainsKey(key) ? (object) this._groupNames[key] : (object) keyValuePair.Key.ToString();
        for (int index1 = 0; index1 < keyValuePair.Value.Count; ++index1)
        {
          long num = (long) index1;
          object obj2 = (object) $"{obj1}.{num.ToString()}";
          for (int index2 = 0; index2 < keyValuePair.Value[index1].Count; ++index2)
          {
            long prjLinkID = keyValuePair.Value[index1][index2];
            relationAttributes[prjLinkID] = this._relAttrs[prjLinkID, true];
            relationAttributes[prjLinkID, SubstituteObjects._substituteGroupNo] = (object) key;
            relationAttributes[prjLinkID, SubstituteObjects._substituteInGroup] = (object) num;
            relationAttributes[prjLinkID, SubstituteObjects._substituteGroupName] = obj1;
            relationAttributes[prjLinkID, SubstituteObjects._substituteName] = obj2;
          }
        }
      }
      return relationAttributes;
    }
    set => this._relAttrs = value;
  }

  /// <summary>Номер группы заменителей</summary>
  public static int attrSubstituteGroupNo
  {
    [DebuggerStepThrough] get => SubstituteObjects._substituteGroupNo;
  }

  /// <summary>Номер заменителя в группе</summary>
  public static int attrSubstituteInGroup
  {
    [DebuggerStepThrough] get => SubstituteObjects._substituteInGroup;
  }

  /// <summary>Имя группы заменителей</summary>
  public static int attrSubstituteGroupName
  {
    [DebuggerStepThrough] get => SubstituteObjects._substituteGroupName;
  }

  /// <summary>Имя заменителя</summary>
  public static int attrSubstituteName
  {
    [DebuggerStepThrough] get => SubstituteObjects._substituteName;
  }

  /// <summary>Конструкторский основной вариант</summary>
  public static int attrDesignActualVariant
  {
    [DebuggerStepThrough] get => SubstituteObjects._designActualVariant;
  }

  /// <summary>Обозначение</summary>
  public static int attrDesignation
  {
    [DebuggerStepThrough] get => SubstituteObjects._designation;
  }

  /// <summary>Наименование</summary>
  public static int attrName
  {
    [DebuggerStepThrough] get => SubstituteObjects._name;
  }

  /// <summary>Количество</summary>
  public static int attrQuantity
  {
    [DebuggerStepThrough] get => SubstituteObjects._quantity;
  }

  /// <summary>Позиция</summary>
  public static int attrPosition
  {
    [DebuggerStepThrough] get => SubstituteObjects._position;
  }

  /// <summary>Сортировка</summary>
  public static int attrSorting
  {
    [DebuggerStepThrough] get => SubstituteObjects._sorting;
  }

  /// <summary>Зона</summary>
  public static int attrZone
  {
    [DebuggerStepThrough] get => SubstituteObjects._zone;
  }

  /// <summary>Примечание</summary>
  public static int attrNote
  {
    [DebuggerStepThrough] get => SubstituteObjects._remark;
  }

  /// <summary>Условия применения объекта</summary>
  public static int attrApplicabilities
  {
    [DebuggerStepThrough] get => SubstituteObjects._applicabilities;
  }

  /// <summary>Список идентификаторов атрибутов</summary>
  public static List<int> Attrs
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrs;
  }

  /// <summary>
  /// Коллекция колонок, которая используется для работы составов с допустимыми заменами
  /// </summary>
  public static List<ColumnDescriptor> AttrColumns
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrColumns;
  }

  /// <summary>
  /// Коллекция колонок для сравнения составов. Колонки в списке
  /// именуются идентификаторам атрибутов
  /// </summary>
  public static List<ColumnDescriptor> CompareArtRelationsColumns
  {
    [DebuggerStepThrough] get => SubstituteObjects._compareArtRelationsColumns;
  }

  /// <summary>Список GUID атрибутов</summary>
  public static List<string> AttrsGUID
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrsGUID;
  }

  /// <summary>
  /// Колонки атрибутов в составе
  /// [(Int32)ID атрибута] = [(Int32)Номер колонки атрибута в таблице состава]
  /// </summary>
  public static Dictionary<int, int> AttrsIndex
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrsIndex;
  }

  /// <summary>
  /// Колонки атрибутов в составе исполнений.
  /// [(Int32)ID атрибута] = [(Int32)Номер колонки атрибута в таблице состава исполнений]
  /// </summary>
  public static Dictionary<int, int> CompareArtRelationsAttrsIndex
  {
    [DebuggerStepThrough] get => SubstituteObjects._compareArtRelationsAttrsIndex;
  }

  /// <summary>
  /// Колонки атрибутов в составе
  /// [(string)Guid атрибута] = [(Int32)Номер колонки атрибута в таблице состава]
  /// </summary>
  public static Dictionary<string, int> AttrsGUIDIndex
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrsGUIDIndex;
  }

  /// <summary>
  /// Получить список описателей колонок для работы с допустимыми заменами в составе. Колонки в списке
  /// именуются по индексам, а не по Guid
  /// </summary>
  public static List<ColumnDescriptor> SubstitutesColumns
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrColumns;
  }

  /// <summary>
  /// Список идентификаторов атрибутов, по которым требуется выполнять сравнение связей в исполнениях (спц. формы А)
  /// </summary>
  public static List<int> AttrsToCompareArtRelations
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrsToCompareArtRelations;
  }

  /// <summary>
  /// Список идентификаторов атрибутов, по которым требуется выполнять сравнение связей в исполнениях (спц. формы Б)
  /// </summary>
  public static List<int> AttrsToCompareArtRelationsFormB
  {
    [DebuggerStepThrough] get => SubstituteObjects._attrsToCompareArtRelationsFormB;
  }

  public Dictionary<long, string> GroupsAffected { get; set; }

  /// <summary>Отыскать номер группы заменителей для указанной связи</summary>
  /// <param name="PrjLinkID">Искомая связь ("F_PRJLINK_ID")</param>
  /// <returns>-1, если связь не входит в текущие группы заменителей, иначе - идентификатор группы заменителей</returns>
  public virtual long IndexOf(long PrjLinkID)
  {
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
    {
      if (keyValuePair.Value.Count != 0)
      {
        for (int index = 0; index < keyValuePair.Value.Count; ++index)
        {
          if (keyValuePair.Value[index].Contains(PrjLinkID))
            return keyValuePair.Key;
        }
      }
    }
    return -1;
  }

  /// <summary>
  /// Отыскать номер заменителя в группе для указанной связи
  /// </summary>
  /// <param name="PrjLinkID"></param>
  /// <returns></returns>
  public virtual long IndexInGroup(long PrjLinkID)
  {
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
    {
      if (keyValuePair.Value.Count != 0)
      {
        for (int index = 0; index < keyValuePair.Value.Count; ++index)
        {
          if (keyValuePair.Value[index].Contains(PrjLinkID))
            return (long) index;
        }
      }
    }
    return -1;
  }

  /// <summary>
  /// Отыскать номер группы и номер заменителя в группе для указанной связи
  /// </summary>
  /// <param name="PrjLinkID">Идентификатор связи</param>
  /// <param name="Group">Номер группы заменителей или -1, если связь не найдена</param>
  /// <param name="SubstInGroup">Номер заменителя в группе или -1, если связь не найдена</param>
  /// <returns>true, если связь найдена</returns>
  public virtual bool IndexOf(long PrjLinkID, out long Group, out long SubstInGroup)
  {
    Group = 0L;
    SubstInGroup = 0L;
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
    {
      if (keyValuePair.Value.Count != 0)
      {
        for (int index = 0; index < keyValuePair.Value.Count; ++index)
        {
          if (keyValuePair.Value[index].Contains(PrjLinkID))
          {
            Group = keyValuePair.Key;
            SubstInGroup = (long) index;
            return true;
          }
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Добавить связь в указанную группу заменителей.
  /// Если группы с таким номером нет, то она будет создана автоматически.
  /// </summary>
  /// <param name="Group">Номер группы заменителей.
  /// Ноль - связь игнорируется.
  /// Меньше нуля - новое автоматически получаемое значение группы</param>
  /// <param name="subGroup">Номер заменителя в группе. Меньше или равно нулю - в актуальный заменитель</param>
  /// <param name="PrjLinkID">Идентификатор связи</param>
  /// <returns>Номер группы заменителей, в которую была добавлена связь, или -1</returns>
  public virtual long AddRelation(long Group, long subGroup, long PrjLinkID)
  {
    if (PrjLinkID == 0L || Group == 0L)
      return -1;
    long num = this.IndexOf(PrjLinkID);
    if (num > 0L)
      return num;
    int int32_1 = Convert.ToInt32(subGroup);
    if (Group < 0L || !this._items.ContainsKey(Group))
      Group = this.NewGroup(Group);
    List<long> longList = int32_1 < 0 || int32_1 >= this._items[Group].Count ? (List<long>) null : this._items[Group][int32_1];
    if (longList == null)
    {
      subGroup = this.NewSubstitute(Group, subGroup);
      int int32_2 = Convert.ToInt32(subGroup);
      longList = this._items[Group][int32_2];
    }
    if (!longList.Contains(PrjLinkID))
      longList.Add(PrjLinkID);
    this._relAttrs[PrjLinkID, SubstituteObjects._substituteGroupNo] = (object) Group;
    this._relAttrs[PrjLinkID, SubstituteObjects._substituteInGroup] = (object) subGroup;
    this._relAttrs[PrjLinkID, SubstituteObjects._substituteGroupName] = (object) this.GetSubstGroupName(Group);
    this._remarks[PrjLinkID] = string.Empty;
    return Group;
  }

  /// <summary>
  /// Добавить связь в указанную группу заменителей.
  /// Если группы с таким номером нет, то она будет создана автоматически.
  /// </summary>
  /// <param name="Group">Номер группы заменителей.
  /// Ноль - связь игнорируется.
  /// Меньше нуля - новое автоматически получаемое значение группы</param>
  /// <param name="subGroup">Номер заменителя в группе. Меньше или равно нулю - в актуальный заменитель</param>
  /// <param name="PrjLinkID">Идентификатор связи</param>
  /// <param name="partID">Идентификатор версии дочернего объекта</param>
  /// <returns>Номер группы заменителей, в которую была добавлена связь, или -1</returns>
  public virtual long AddRelation(long Group, long subGroup, long PrjLinkID, long partID)
  {
    long num = this.AddRelation(Group, subGroup, PrjLinkID);
    if (num == -1L || this._relationObjects.ContainsKey(PrjLinkID))
      return num;
    this._relationObjects.Add(PrjLinkID, partID);
    return num;
  }

  /// <summary>Очистить список групп заменителей</summary>
  public virtual void Clear()
  {
    this._items.Clear();
    this._groups.Clear();
    this._relationObjects.Clear();
    this._relAttrs.Values.Clear();
    this._groupNames.Clear();
    this._remarks.Clear();
  }

  /// <summary>
  /// Перестроить номера групп заменителей (по возрастанию номеров групп)
  /// </summary>
  public virtual void RebuildGroups()
  {
    long key = 1;
    if (this._items.Count == 0)
      return;
    Dictionary<long, List<List<long>>> dictionary1 = new Dictionary<long, List<List<long>>>(this._items.Count);
    Dictionary<long, string> dictionary2 = new Dictionary<long, string>();
    List<long> longList = new List<long>(this._items.Count);
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
      longList.Add(keyValuePair.Key);
    longList.Sort();
    this._groups.Clear();
    for (int index = 0; index < longList.Count; ++index)
    {
      dictionary2[key] = this._groupNames[longList[index]];
      dictionary1[key] = this._items[longList[index]];
      this._groups.Add(key);
      ++key;
    }
    this._items = dictionary1;
    this._groupNames = dictionary2;
    this._relAttrs = this.RelationAttributes;
  }

  /// <summary>Создать новую группу допустимых замен</summary>
  /// <param name="group">Желаемый номер новой группы</param>
  /// <returns>Номер новой группы допустимых замен</returns>
  public virtual long NewGroup(long group)
  {
    if (this._items.ContainsKey(group))
      return group;
    long key = group > 0L ? group : 1L;
    while (this._items.ContainsKey(key))
      ++key;
    this._items.Add(key, new List<List<long>>());
    this._groups.Add(key);
    if (!this._groupNames.ContainsKey(key))
      this._groupNames.Add(key, key.ToString());
    return key;
  }

  /// <summary>Удалить указанную группу допустимых замен</summary>
  /// <param name="group">Номер удаляемой группы допустимых замен</param>
  /// <returns>true, если группа была успешно удалена</returns>
  public virtual bool RemoveGroup(long group)
  {
    if (!this._items.ContainsKey(group))
      return false;
    this._items.Remove(group);
    this._groups.Remove(group);
    if (this._groupNames.ContainsKey(group))
      this._groupNames.Remove(group);
    this._relAttrs = this.RelationAttributes;
    return true;
  }

  /// <summary>
  /// Добавить новый допустимый заменитель в указанную группу
  /// </summary>
  /// <param name="group">Номер группы</param>
  /// <param name="substsNo">Требуемый номер заменителя или -1 - получить номер автоматически</param>
  /// <returns>Номер нового допустимого заменителя</returns>
  public virtual long NewSubstitute(long group, long substsNo)
  {
    if (!this._items.ContainsKey(group))
      return -1;
    List<List<long>> longListList = this._items[group];
    if (substsNo < (long) longListList.Count)
    {
      longListList.Add(new List<long>());
      return (long) (longListList.Count - 1);
    }
    int num = Convert.ToInt32(substsNo) - longListList.Count + 1;
    for (int index = 0; index < num; ++index)
      longListList.Add(new List<long>());
    return (long) (longListList.Count - 1);
  }

  /// <summary>Удалить указанный заменитель из группы</summary>
  /// <param name="group">Номер группы заменителей</param>
  /// <param name="substitute">Номер заменителя в группе</param>
  /// <returns>true, если заменитель был найден и удалён</returns>
  public virtual bool RemoveSubstitute(long group, long substitute)
  {
    if (this._items.ContainsKey(group))
    {
      List<List<long>> longListList = this._items[group];
      if (substitute >= 0L && substitute < (long) longListList.Count)
      {
        longListList.RemoveAt(Convert.ToInt32(substitute));
        this._relAttrs = this.RelationAttributes;
        return true;
      }
    }
    return false;
  }

  /// <summary>Удалить указанные связи из списка групп заменителей.</summary>
  /// <param name="PrjLinkIDs">Список удаляемых идентификаторов связи</param>
  /// <returns>true, если хотя бы одна связь из списка была найдена и удалена</returns>
  public virtual bool RemoveRelations(List<long> PrjLinkIDs)
  {
    bool flag = false;
    if (PrjLinkIDs == null || PrjLinkIDs.Count == 0)
      return flag;
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
    {
      if (keyValuePair.Value.Count != 0)
      {
        for (int index1 = 0; index1 < keyValuePair.Value.Count; ++index1)
        {
          for (int index2 = 0; index2 < PrjLinkIDs.Count; ++index2)
          {
            if (keyValuePair.Value[index1].Contains(PrjLinkIDs[index2]))
            {
              keyValuePair.Value[index1].Remove(PrjLinkIDs[index2]);
              if (this._relationObjects.ContainsKey(PrjLinkIDs[index2]))
                this._relationObjects.Remove(PrjLinkIDs[index2]);
              if (this._relAttrs.Values.ContainsKey(PrjLinkIDs[index2]))
                this._relAttrs.Values.Remove(PrjLinkIDs[index2]);
              if (this._remarks.ContainsKey(PrjLinkIDs[index2]))
                this._remarks.Remove(PrjLinkIDs[index2]);
              flag = true;
            }
          }
        }
      }
    }
    return flag;
  }

  /// <summary>Обменять два допустимых заменителя местами</summary>
  /// <param name="groupNumber">Номер группы заменителей</param>
  /// <param name="firstSubstituteNumber"></param>
  /// <param name="secondSubstituteNumber"></param>
  /// <returns></returns>
  public virtual bool SwapSubstitutes(
    long groupNumber,
    long firstSubstituteNumber,
    long secondSubstituteNumber)
  {
    List<List<long>> longListList = (List<List<long>>) null;
    if (!this._items.TryGetValue(groupNumber, out longListList))
      throw new ArgumentException("Указанной группы не существует");
    if (firstSubstituteNumber < 0L || firstSubstituteNumber >= (long) longListList.Count || secondSubstituteNumber < 0L || secondSubstituteNumber >= (long) longListList.Count || firstSubstituteNumber == secondSubstituteNumber)
      throw new ArgumentException("Неверно указаны номера заменителей или заменителей не существует");
    int index1 = Math.Min(Convert.ToInt32(firstSubstituteNumber), Convert.ToInt32(secondSubstituteNumber));
    int index2 = Math.Max(Convert.ToInt32(firstSubstituteNumber), Convert.ToInt32(secondSubstituteNumber));
    List<long> longList1 = longListList[index1];
    List<long> longList2 = longListList[index2];
    longListList.RemoveAt(index2);
    longListList.RemoveAt(index1);
    longListList.Insert(index1, longList2);
    longListList.Insert(index2, longList1);
    this._relAttrs = this.RelationAttributes;
    return true;
  }

  /// <summary>
  /// Собрать в коллекцию все связи у указанного допустимого заменителя
  /// </summary>
  /// <param name="group">Группа</param>
  /// <param name="subst">Заменитель в группе</param>
  /// <param name="relations">Список связей</param>
  public virtual void GatherRelations(long group, long subst, ref List<long> relations)
  {
    if (relations == null)
      relations = new List<long>();
    if (!this._items.ContainsKey(group))
      return;
    List<List<long>> longListList = this._items[group];
    if (subst < 0L || subst >= (long) longListList.Count)
      return;
    List<long> longList = longListList[Convert.ToInt32(subst)];
    for (int index = 0; index < longList.Count; ++index)
    {
      if (!relations.Contains(longList[index]))
        relations.Add(longList[index]);
    }
  }

  /// <summary>Собрать в коллекцию все связи у указанной группы</summary>
  /// <param name="group">Группа</param>
  /// <param name="relations">Список связей</param>
  public virtual void GatherRelations(long group, ref List<long> relations)
  {
    if (relations == null)
      relations = new List<long>();
    if (!this._items.ContainsKey(group))
      return;
    List<List<long>> longListList = this._items[group];
    for (int index1 = 0; index1 < longListList.Count; ++index1)
    {
      List<long> longList = longListList[index1];
      for (int index2 = 0; index2 < longList.Count; ++index2)
      {
        if (!relations.Contains(longList[index2]))
          relations.Add(longList[index2]);
      }
    }
  }

  /// <summary>Собрать в коллекцию все связи</summary>
  /// <param name="relations">Список связей</param>
  public virtual void GatherRelations(ref List<long> relations)
  {
    if (relations == null)
      relations = new List<long>();
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
      this.GatherRelations(keyValuePair.Key, ref relations);
  }

  /// <summary>Проверить, пустая ли группа (без связей)</summary>
  /// <param name="groupNo">Номер группы</param>
  /// <returns>true, если группа не найдена, либо в ней нет ни одной связи</returns>
  public virtual bool IsGroupEmpty(long groupNo)
  {
    if (!this._items.ContainsKey(groupNo))
      return true;
    bool flag = true;
    List<List<long>> longListList = this._items[groupNo];
    for (int index = 0; index < longListList.Count; ++index)
      flag &= longListList[index].Count == 0;
    return flag;
  }

  /// <summary>
  /// Заполнить в пакете значениями атрибуты указанной связи из строки таблицы с данными.
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="row">Строка с данными</param>
  public virtual void SetRelationAttributes(long prjLinkID, DataRow row)
  {
    if (row == null)
      return;
    DataTable table = row.Table;
    for (int index = 0; index < SubstituteObjects._attrs.Count; ++index)
      this._relAttrs[prjLinkID, SubstituteObjects._attrs[index]] = row[SubstituteObjects._attrsIndex[SubstituteObjects._attrs[index]]];
    if (this._remarks.ContainsKey(prjLinkID))
      return;
    this._remarks[prjLinkID] = string.Empty;
  }

  /// <summary>
  /// Заполнить для указанной связи поля с дополнительными атрибутами из полученного массива значений
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <param name="attributes">Список атрибутов, значения которых хранятся в values</param>
  /// <param name="values">Список значений атрибутов attributes</param>
  public virtual void SetRelationAttributes(
    long prjLinkID,
    List<NodeColumnID> attributes,
    object[] values)
  {
    if (attributes == null || attributes.Count == 0 || values == null || attributes.Count != values.Length)
      return;
    for (int index1 = 0; index1 < SubstituteObjects._attrs.Count; ++index1)
    {
      NodeColumnID nodeColumnId1 = new NodeColumnID((object) SubstituteObjects._attrs[index1], AttributeSourceTypes.Relation);
      NodeColumnID nodeColumnId2 = new NodeColumnID((object) SubstituteObjects._attrs[index1], AttributeSourceTypes.Object);
      NodeColumnID nodeColumnId3 = new NodeColumnID((object) SubstituteObjects._attrs[index1], AttributeSourceTypes.Auto);
      int index2 = attributes.IndexOf(nodeColumnId1);
      if (index2 < 0)
        index2 = attributes.IndexOf(nodeColumnId2);
      if (index2 < 0)
        index2 = attributes.IndexOf(nodeColumnId3);
      if (index2 >= 0)
        this._relAttrs[prjLinkID, SubstituteObjects._attrs[index1]] = values[index2];
    }
    if (this._remarks.ContainsKey(prjLinkID))
      return;
    this._remarks[prjLinkID] = string.Empty;
  }

  /// <summary>
  /// Принадлежит ли указанная связь конструкторскому основному варианту
  /// </summary>
  /// <param name="prjLinkID">Идентификатор связи</param>
  /// <returns>true, если указанная связь принадлежит конструкторскому основному варианту</returns>
  public virtual bool IsRelationDesignerActualVariant(long prjLinkID)
  {
    object relAttr = this._relAttrs[prjLinkID, SubstituteObjects.attrDesignActualVariant];
    return relAttr != null && relAttr.ToString().Equals("1");
  }

  /// <summary>
  /// Проверить, является ли хотя бы одна из связей указанной группы конструкторским основным вариантом
  /// </summary>
  /// <param name="groupNo"></param>
  /// <returns>true, если хотя бы одна из связей указанной группы является конструкторским основным вариантом</returns>
  public virtual bool HasRelationsDesignerActualVariant(long groupNo)
  {
    List<long> relations = new List<long>();
    this.GatherRelations(groupNo, ref relations);
    for (int index = 0; index < relations.Count; ++index)
    {
      if (this.IsRelationDesignerActualVariant(relations[index]))
        return true;
    }
    return false;
  }

  /// <summary>
  /// Получить текущее имя указанной группы допустимых заменителей.
  /// Если имя недоступно - будет сгенерировано стандартное имя - номер группы
  /// </summary>
  /// <param name="groupNo">Номер группы заменителей</param>
  /// <returns>Имя указанной группы допустимых заменителей.</returns>
  public virtual string GetSubstGroupName(long groupNo)
  {
    if (this._groupNames.ContainsKey(groupNo))
      return this._groupNames[groupNo];
    List<long> relations = new List<long>();
    this.GatherRelations(groupNo, ref relations);
    if (relations.Count == 0)
      return SubstituteObjects._groupNameCounter++.ToString();
    Dictionary<string, long> dictionary = new Dictionary<string, long>();
    for (int index = 0; index < relations.Count; ++index)
    {
      object relAttr = this._relAttrs[relations[index], SubstituteObjects._substituteGroupName];
      if (relAttr != null && !(relAttr.ToString() == string.Empty) && relAttr != DBNull.Value)
      {
        string key = relAttr.ToString();
        if (!dictionary.ContainsKey(key))
          dictionary.Add(key, 0L);
        ++dictionary[key];
      }
    }
    if (dictionary.Count == 0)
      return SubstituteObjects._groupNameCounter++.ToString();
    string substGroupName = string.Empty;
    long num = 0;
    foreach (KeyValuePair<string, long> keyValuePair in dictionary)
    {
      if (substGroupName == string.Empty)
      {
        substGroupName = keyValuePair.Key;
        num = keyValuePair.Value;
      }
      else if (num < keyValuePair.Value)
      {
        substGroupName = keyValuePair.Key;
        num = keyValuePair.Value;
      }
    }
    if (substGroupName == string.Empty)
      substGroupName = SubstituteObjects._groupNameCounter++.ToString();
    this._groupNames.Add(groupNo, substGroupName);
    return substGroupName;
  }

  /// <summary>Назначить указанной группе заменителей новое имя</summary>
  /// <param name="groupNo">Номер группы заменителей</param>
  /// <param name="newName">Новое имя</param>
  public virtual void SetSubstGroupName(long groupNo, string newName)
  {
    this._groupNames[groupNo] = newName;
    this.SetGroupAttrValue(groupNo, SubstituteObjects._substituteGroupName, (object) newName);
  }

  /// <summary>
  /// Задать значение указанного атрибута всем связям указанной группы
  /// </summary>
  /// <param name="groupNo">Номер группы заменителей</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="value">Значение атрибута</param>
  public virtual void SetGroupAttrValue(long groupNo, int attrID, object value)
  {
    List<long> relations = new List<long>();
    this.GatherRelations(groupNo, ref relations);
    this._relAttrs.SetRelationsAttrValue(relations, attrID, value);
  }

  /// <summary>
  /// Задать значение указанного атрибута всем связям указанного заменителя
  /// </summary>
  /// <param name="groupNo">Номер группы заменителей</param>
  /// <param name="substNo">Номер заменителя в группе</param>
  /// <param name="attrID">Идентификатор атрибута</param>
  /// <param name="value">Значение атрибута</param>
  public virtual void SetSubstAttrValue(long groupNo, long substNo, int attrID, object value)
  {
    List<long> relations = new List<long>();
    this.GatherRelations(groupNo, substNo, ref relations);
    this._relAttrs.SetRelationsAttrValue(relations, attrID, value);
  }

  /// <summary>Создать копию объекта, идентичную натуральной</summary>
  /// <returns>Копия объекта, идентичная натуральной</returns>
  public object Clone()
  {
    SubstituteObjects substituteObjects = new SubstituteObjects();
    substituteObjects._items = new Dictionary<long, List<List<long>>>(this._items.Count);
    substituteObjects._groups = new List<long>(this._groups.Count);
    substituteObjects._groupNames = new Dictionary<long, string>((IDictionary<long, string>) this._groupNames);
    substituteObjects._relationObjects = new Dictionary<long, long>();
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this._items)
    {
      substituteObjects._groups.Add(keyValuePair.Key);
      List<List<long>> longListList = new List<List<long>>(keyValuePair.Value.Count);
      substituteObjects._items.Add(keyValuePair.Key, longListList);
      for (int index1 = 0; index1 < keyValuePair.Value.Count; ++index1)
      {
        List<long> longList1 = keyValuePair.Value[index1];
        List<long> longList2 = new List<long>(longList1.Count);
        longListList.Add(longList2);
        for (int index2 = 0; index2 < longList1.Count; ++index2)
        {
          longList2.Add(longList1[index2]);
          if (this._relationObjects.ContainsKey(longList1[index2]))
            substituteObjects._relationObjects.Add(longList1[index2], this._relationObjects[longList1[index2]]);
        }
      }
    }
    substituteObjects._relAttrs = this._relAttrs.Clone() as RelationAttributesPackage;
    substituteObjects._remarks = new Dictionary<long, string>((IDictionary<long, string>) this._remarks);
    return (object) substituteObjects;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="prjLinkID"></param>
  /// <returns></returns>
  public virtual string GetRemark(long prjLinkID)
  {
    return !this._remarks.ContainsKey(prjLinkID) ? string.Empty : this._remarks[prjLinkID];
  }

  public bool IsDesignActualVariant(long substituteGroupNumber, long substituteNumber)
  {
    long[] relationIdsInSubstitute = this.GetRelationIdsInSubstitute(substituteGroupNumber, substituteNumber);
    return relationIdsInSubstitute.Length != 0 && this.HasDesignActualVariantMark(relationIdsInSubstitute[0]);
  }

  public void SetDesignActualVariant(long substituteGroupNumber, long substituteNumber, bool value)
  {
    foreach (long relationID in this.GetRelationIdsInGroup(substituteGroupNumber))
      this.SetDesignActualVariantMark(relationID, false);
    if (!value)
      return;
    foreach (long relationID in this.GetRelationIdsInSubstitute(substituteGroupNumber, substituteNumber))
      this.SetDesignActualVariantMark(relationID, true);
  }

  /// <summary>
  /// Принадлежит ли указанная связь конструкторскому основному варианту
  /// </summary>
  /// <param name="relationID">Идентификатор связи</param>
  /// <returns>true, если указанная связь принадлежит конструкторскому основному варианту</returns>
  public virtual bool HasDesignActualVariantMark(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    object relAttr = this._relAttrs[relationID, SubstitutesConstants.DesignActualVariantAttributeTypeID];
    return relAttr != null && relAttr.ToString().Equals("1");
  }

  public void SetDesignActualVariantMark(long relationID, bool value)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    this._relAttrs[relationID, SubstitutesConstants.DesignActualVariantAttributeTypeID] = value ? (object) 1L : (object) null;
  }

  public long GetObjectID(long relationID)
  {
    return !RelationHelper.IsUnknownRelationID(relationID) ? DataSetProcessor.GetInt64Value(this._relAttrs[relationID, -3], 0L) : throw new ArgumentException();
  }

  public long GetObjectVersionID(long relationID)
  {
    return !RelationHelper.IsUnknownRelationID(relationID) ? Convert.ToInt64(this._relAttrs[relationID, -2]) : throw new ArgumentException();
  }

  public void SetObjectID(long relationID, long objectID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    this._relAttrs[relationID, -3] = !ObjectHelper.IsUnknownObjectID(objectID) ? (object) objectID : throw new ArgumentException();
  }

  /// <summary>
  /// Установить признак вспомогательной позиции, если нужно
  /// </summary>
  public void SetAuxiliaryFlagIfNeed(long relationID, long objectID, string positionNumber)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectID(objectID))
      throw new ArgumentException();
    long groupNumber = this.GetGroupNumber(relationID);
    if (groupNumber == -1L || this.GetAuxPositionRelationIds(groupNumber, positionNumber, objectID).Length <= 1)
      return;
    this.SetAuxiliaryPosition(relationID, true);
  }

  public long[] GetRelationIds() => this._relAttrs.Values.Keys.ToArray<long>();

  public long[] GetRelationIdsInGroup(long substituteGroupNumber)
  {
    List<long> longList = new List<long>();
    List<List<long>> longListList = this[substituteGroupNumber];
    if (longListList != null)
    {
      foreach (List<long> collection in longListList)
        longList.AddRange((IEnumerable<long>) collection);
    }
    return longList.ToArray();
  }

  public long[] GetRelationIdsInSubstitute(long substituteGroupNumber, long substituteNumber)
  {
    List<long> longList = this[substituteGroupNumber, substituteNumber];
    return longList == null ? new long[0] : longList.ToArray();
  }

  public long[] GetRelationIdsForObjectID(long substituteGroupNumber, long objectID)
  {
    List<long> longList1 = new List<long>();
    List<List<long>> longListList = this[substituteGroupNumber];
    if (longListList != null)
    {
      foreach (List<long> longList2 in longListList)
      {
        if (longList2 != null)
        {
          foreach (long relationID in longList2)
          {
            if (this.GetObjectID(relationID) == objectID)
              longList1.Add(relationID);
          }
        }
      }
    }
    return longList1.ToArray();
  }

  public long[] GetAuxPositionRelationIds(
    long substituteGroupNumber,
    string relPositionNumber,
    long objectID)
  {
    List<long> longList1 = new List<long>();
    List<List<long>> longListList = this[substituteGroupNumber];
    if (longListList != null)
    {
      foreach (List<long> longList2 in longListList)
      {
        if (longList2 != null)
        {
          foreach (long relationID in longList2)
          {
            if (this.GetObjectID(relationID) == objectID && (relPositionNumber.Equals($"{-1L}", StringComparison.CurrentCulture) || this.GetRelationPositionNumber(relationID).Equals(relPositionNumber, StringComparison.CurrentCulture)))
              longList1.Add(relationID);
          }
        }
      }
    }
    return longList1.ToArray();
  }

  public long GetGroupNumber(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    object relAttr = this._relAttrs[relationID, SubstitutesConstants.SubstituteGroupNumberAttributeTypeID];
    return relAttr == null ? -1L : Convert.ToInt64(relAttr);
  }

  public long GetSubstituteNumber(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    object relAttr = this._relAttrs[relationID, SubstitutesConstants.SubstituteNumberAttributeTypeID];
    return relAttr == null ? -1L : Convert.ToInt64(relAttr);
  }

  public bool IsAuxiliaryPosition(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    return this.IsAuxiliaryPositionMarked(relationID) || this.IsNotEditableAuxiliaryPosition(relationID);
  }

  public bool IsEqualPosition(long relationID)
  {
    return !RelationHelper.IsUnknownRelationID(relationID) ? this.IsEqualPositionMarked(relationID) : throw new ArgumentException();
  }

  public bool IsAuxiliaryPositionMarked(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    object relAttr = this._relAttrs[relationID, SubstitutesConstants.SubstitutePositionTypeAttributeTypeID];
    switch (relAttr)
    {
      case Decimal _:
      case long _:
        return Convert.ToInt64(relAttr) == 3L;
      default:
        return false;
    }
  }

  public bool IsEqualPositionMarked(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    object relAttr = this._relAttrs[relationID, SubstitutesConstants.SubstitutePositionTypeAttributeTypeID];
    switch (relAttr)
    {
      case Decimal _:
      case long _:
        return Convert.ToInt64(relAttr) == 4L;
      default:
        return false;
    }
  }

  public bool IsNotEditableAuxiliaryPosition(long relationID)
  {
    long id = !RelationHelper.IsUnknownRelationID(relationID) ? this.GetObjectID(relationID) : throw new ArgumentException();
    if (!ObjectHelper.IsUnknownObjectID(id))
    {
      long groupNumber = this.GetGroupNumber(relationID);
      string relationPositionNumber = this.GetRelationPositionNumber(relationID);
      if (groupNumber != -1L)
      {
        foreach (long relationID1 in this.GetRelationIdsInGroup(groupNumber))
        {
          if (relationID1 != relationID && this.GetObjectID(relationID1) == id && (relationPositionNumber.Equals($"{-1L}", StringComparison.CurrentCulture) || this.GetRelationPositionNumber(relationID1).Equals(relationPositionNumber, StringComparison.CurrentCulture)))
            return true;
        }
      }
    }
    return false;
  }

  public void SetAuxiliaryPosition(long relationID, bool value)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (this.IsNotEditableAuxiliaryPosition(relationID) && !value)
      throw new ArgumentException();
    this._relAttrs[relationID, SubstitutesConstants.SubstitutePositionTypeAttributeTypeID] = value ? (object) 3L : (object) null;
  }

  public void SetEqualPosition(long relationID, bool value)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (this.IsNotEditableAuxiliaryPosition(relationID) && !value)
      throw new ArgumentException();
    this._relAttrs[relationID, SubstitutesConstants.SubstitutePositionTypeAttributeTypeID] = value ? (object) 4L : (object) null;
  }

  public long[] GetRelationIdsInSubstituteWithRelation(long substituteGroupNumber, long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    foreach (List<long> source in this[substituteGroupNumber])
    {
      if (source.Contains(relationID))
        return source.Where<long>((System.Func<long, bool>) (o => o != relationID)).ToArray<long>();
    }
    return new long[0];
  }

  public void SetRelationAttributeValue(
    long relationID,
    int attributeTypeID,
    object attributeValue)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
      throw new ArgumentException();
    this._relAttrs[relationID, attributeTypeID] = attributeValue;
  }

  public long GetPositionNumber(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    object relAttr = this._relAttrs[relationID, SubstitutesConstants.PositionNumberAttributeTypeID];
    return !(relAttr is IConvertible) ? -1L : ((IConvertible) relAttr).ToInt64((IFormatProvider) CultureInfo.CurrentCulture);
  }

  public string GetRelationPositionNumber(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    string str = this._relAttrs[relationID, Constants.PositionAttributeTypeID]?.ToString();
    return !string.IsNullOrWhiteSpace(str) ? str : $"{-1L}";
  }

  public void SetPositionNumber(long relationID, long value)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    this._relAttrs[relationID, SubstitutesConstants.PositionNumberAttributeTypeID] = (object) value;
  }

  public void MovePositionsUp(long groupNumber, long substituteNumber, long[] relationIds)
  {
    List<long> relationIdsInSubstitute = this.Items[groupNumber][(int) substituteNumber];
    long[] array = relationIdsInSubstitute.OrderBy<long, long>((System.Func<long, long>) (o => this.GetPositionNumber(o))).ToArray<long>();
    relationIdsInSubstitute.Clear();
    relationIdsInSubstitute.AddRange((IEnumerable<long>) array);
    relationIds = ((IEnumerable<long>) relationIds).OrderBy<long, int>((System.Func<long, int>) (o => relationIdsInSubstitute.IndexOf(o))).ToArray<long>();
    foreach (long relationId in relationIds)
    {
      int index = relationIdsInSubstitute.IndexOf(relationId);
      if (index > 0)
      {
        relationIdsInSubstitute[index] = relationIdsInSubstitute[index - 1];
        relationIdsInSubstitute[index - 1] = relationId;
      }
    }
    for (int index = 0; index < relationIdsInSubstitute.Count; ++index)
      this.SetPositionNumber(relationIdsInSubstitute[index], (long) index);
  }

  public void MovePositionsDown(long groupNumber, long substituteNumber, long[] relationIds)
  {
    List<long> relationIdsInSubstitute = this.Items[groupNumber][(int) substituteNumber];
    long[] array = relationIdsInSubstitute.OrderBy<long, long>((System.Func<long, long>) (o => this.GetPositionNumber(o))).ToArray<long>();
    relationIdsInSubstitute.Clear();
    relationIdsInSubstitute.AddRange((IEnumerable<long>) array);
    relationIds = ((IEnumerable<long>) relationIds).OrderBy<long, int>((System.Func<long, int>) (o => relationIdsInSubstitute.IndexOf(o))).ToArray<long>();
    foreach (long num in ((IEnumerable<long>) relationIds).Reverse<long>())
    {
      int index = relationIdsInSubstitute.IndexOf(num);
      if (index < relationIdsInSubstitute.Count - 1)
      {
        relationIdsInSubstitute[index] = relationIdsInSubstitute[index + 1];
        relationIdsInSubstitute[index + 1] = num;
      }
    }
    for (int index = 0; index < relationIdsInSubstitute.Count; ++index)
      this.SetPositionNumber(relationIdsInSubstitute[index], (long) index);
  }

  public void SortPositions()
  {
    foreach (KeyValuePair<long, List<List<long>>> keyValuePair in this.Items)
    {
      foreach (List<long> source in keyValuePair.Value)
      {
        long[] array = source.OrderBy<long, long>((System.Func<long, long>) (o => this.GetPositionNumber(o))).ToArray<long>();
        source.Clear();
        source.AddRange((IEnumerable<long>) array);
      }
    }
  }
}
