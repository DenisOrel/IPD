// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSRow
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using Intermech.AVS.Common_Dialogs;
using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.AVS.Output;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using Intermech.PropertyEditors.AttrProcessor;
using Intermech.Search.Pdm.Substitutes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Строка спецификации</summary>
[Serializable]
public class AVSRow : ICustomTypeDescriptor, IVirtualTreeItem
{
  /// <summary>Множитель для диапазона индексов сортировки выделяемого на раздел</summary>
  public const long SectionSortIndexFactor = 10000000;
  /// <summary>Множитель для диапазона индексов сортировки выделяемого на исполнение</summary>
  public const long ProductSortIndexFactor = 10000000;
  /// <summary>Атрибут документа для сохранения в строке индекса сортировки</summary>
  public static string RowAttr_SortIndex = nameof (SortIndex);
  /// <summary>Атрибут документа для сохранения в строке совместных позиций</summary>
  public static string RowAttr_CommonPositions = "AVS.CommonPositions";
  /// <summary>Атрибут документа для сохранения в строке совместных позиций</summary>
  public static string RowAttr_CommonPositionsOLD = "AVS.CommonPositionsOLD";
  /// <summary>Атрибут документа для сохранения в строке PositionStepAfter</summary>
  public static string RowAttr_PositionStepAfter = "AVS.PositionStepAfter";
  /// <summary>Атрибут документа для сохранения в строке PositionStepBefore</summary>
  public static string RowAttr_PositionStepBefore = "AVS.PositionStepBefore";
  /// <summary>Атрибут документа для сохранения в строке Первичная применяемость</summary>
  public static string RowAttr_FirstApplicability = "AVS.Перв.Применяемость";
  /// <summary>Атрибут документа для сохранения в строке SortBeforeRow по SortIndex</summary>
  public static string RowAttr_SortBeforeRowBySortIndex = "AVS.SortBeforeRow";
  /// <summary>Атрибут документа для сохранения в строке SortBeforeRow по SortIndex</summary>
  public static string RowAttr_SortBeforeRowByID = "AVS.SortBeforeRow.Id";
  /// <summary>Атрибут документа для сохранения в строке SortAfterRow по SortIndex</summary>
  public static string RowAttr_SortAfterRowBySortIndex = "AVS.SortAfterRow";
  /// <summary>Атрибут документа для сохранения в строке SortAfterRow по ID</summary>
  public static string RowAttr_SortAfterRowByID = "AVS.SortAfterRow.Id";
  /// <summary>Атрибут документа для сохранения в строке типа связи</summary>
  public static string RowAttr_RelationType = "RelationType";
  /// <summary>Атрибут строки документа для хранения списка гуидов связей</summary>
  public const string RowAttr_Relations = "Relations";
  /// <summary>Атрибут строки документа для хранения списка гуидов скрытых связей</summary>
  public const string RowAttr_HiddenRelations = "HiddenRelations";
  /// <summary>Атрибут строки документа для хранения режима вывода предельных и номинальных значений</summary>
  public const string DocAttr_LimitAndNominalValueMode = "LimitAndNominalValueMode";
  /// <summary>Атрибут документа для сохранения в строке текстовой ссылки "Смотри"</summary>
  public static string DocAttr_Smotri = "Смотри";
  /// <summary>Атрибут документа для сохранения в строке текстовой ссылки "Заготовка для"</summary>
  public static string DocAttr_ZagotovkaDlya = "Заготовка для";
  /// <summary>Атрибут документа для хранения в записи ссылки на объект, для которого используется эта заготовка</summary>
  public static string DocAttr_PartFromDraftGuid = "PartFromDraftGuid";
  /// <summary>Атрибут документа для сохранения в строке индекса первого исполнения</summary>
  public static string DocAttr_ProductIndex = "Ispolnenie";
  /// <summary>Поле документа "Поз. обозначение"</summary>
  public static string DocAttr_PosDesignation = "Поз. обозначение";
  /// <summary>Поле документа "Поз. обозначение"</summary>
  public const string AttrFullName_PosDesignation = "Позиционное обозначение";
  /// <summary>Поле документа "Поз. обозначение"</summary>
  public static string DocAttr_FGPosDesignation = "Поз. обоз. функциональной группы";
  /// <summary>Поле документа "Обозначение функциональной группы"</summary>
  public static string DocAttr_FGDesignation = "Обозначение функциональной группы";
  /// <summary>Поле документа "Наименование функциональной группы"</summary>
  public static string DocAttr_FGName = "Наименование функциональной группы";
  /// <summary>Флаг записи Заголовок функциональной группы</summary>
  public static string DocAttr_FunctionalGroupHeader = "#FGHeader";
  /// <summary>Поле документа "Группа"</summary>
  public static string DocAttr_Group = "Группа";
  /// <summary>Поле документа "Количество"</summary>
  public static string DocAttr_Count = "Количество";
  /// <summary>Поле документа "Количество на регулировку"</summary>
  public static string DocAttr_CountForAdjustment = "Количество на регулировку";
  /// <summary>Поле документа "Примечание"</summary>
  public static string DocAttr_Note = "Примечание";
  /// <summary>Поле документа "Примечание ПЭ"</summary>
  public static string DocAttr_NotePE = "Примечание ПЭ";
  /// <summary>Имя атрибута документа для сохранения дополнительного текста в графе "Наименование"</summary>
  internal const string DocAttr_NameNote = "NameNote";
  /// <summary>Поле документа "Формат"</summary>
  public static string DocAttr_Format = "Формат";
  /// <summary>Поле документа "Позиция"</summary>
  public static string DocAttr_Position = "Позиция";
  /// <summary>Поле документа "Зона"</summary>
  public static string DocAttr_Zone = "Зона";
  /// <summary>Поле документа "Обозначение"</summary>
  public static string DocAttr_Designation = "Обозначение";
  /// <summary>Поле документа "Наименование"</summary>
  public static string DocAttr_Name = "Наименование";
  /// <summary>Имя атрибута документа для сохранения защищённого текста в начале поля</summary>
  private const string CellAttr_ProtectedFirstCharCount = "ProtectedFirstCharCount";
  /// <summary>Имя атрибута документа для сохранения защищённого текста в конце поля</summary>
  private const string CellAttr_ProtectedEndCharCount = "ProtectedEndCharCount";
  internal const string NotSameIspolnCaption = "см. по исполнениям";
  private static string _defaultMU_Count_str = (string) null;
  private static long defaultCountID = -1;
  private static MeasureDescriptor defaultCountMeasure = (MeasureDescriptor) null;
  public static string _defaultMU_Mass_str = (string) null;
  private static long defaultMassID = -1;
  private static MeasureDescriptor defaultMassMeasure = (MeasureDescriptor) null;
  private AttributeValuesCache objectAttributesCache;
  private ObjectModifyModes? objectModifyMode;
  private AVSRow sortAfterRow;
  private AVSRow sortBeforeRow;
  private AvsRowAttributeInfo attr_Smotri;
  private AvsRowAttributeInfo _field_Note;
  /// <summary>Имя атрибута ячейки документа для текста, который отображается когда редактор неактивен</summary>
  public static string CellAttrName_ViewText = "AVS.ViewText";
  /// <summary>Имя атрибута ячейки документа для текста, который отображается когда редактор неактивен</summary>
  public static string CellAttrName_OldViewText = "AVS.OldViewText";
  /// <summary>Имя атрибута ячейки документа для текста, который назначается методом скрывающим форматы</summary>
  public static string CellAttrName_ViewTextForFormat = "AVS.ViewTextForFormat";
  /// <summary>Имя атрибута ячейки документа для текста, который отображается когда редактор активен
  /// и для сохранения этого текста в файле</summary>
  public static string CellAttrName_EditText = "AVS.OldEditText";
  /// <summary>Имя атрибута ячейки документа для текста, который отображается когда редактор активен
  /// и для сохранения этого текста в файле</summary>
  public static string CellAttrName_FullDesignation = "FullDesignation";
  /// <summary>Имя атрибута ячейки документа в которой текст не должен отображаться когда редактор неактивен</summary>
  public static string CellAttrName_HideText = "#AVS.HideText";
  private int index = -1;
  public static ConvertToMeasuredValueDelegate ConvertToMeasuredValueHandler;
  private CellOutputMapping _noteCellMapping;
  private string commonPositionDocument;
  private bool isNoteRow;
  private AVSRowGroup group;
  private int productGroup = -1;
  private bool _hasNoteAndNoteAttributeCollision;
  internal Dictionary<int, AttributeEditorInfo> relEditors = new Dictionary<int, AttributeEditorInfo>();
  internal Dictionary<int, AttributeEditorInfo> objEditors = new Dictionary<int, AttributeEditorInfo>();
  private bool needUpdateStructure = true;
  private bool _needUpdateNote = true;
  private int _suspendUpdateNote;
  private bool _needUpdateName = true;
  internal AVSDocument avsDocument;
  internal long sortIndex = long.MinValue;
  private bool isSorted;
  private TableData docNode;
  private TableData docNodeExp;
  private List<TableData> docNodes = new List<TableData>();
  private DBRelationInfo rowID;
  private List<RelationAttributeValuesCache> relations;
  private List<RelationAttributeValuesCache> hiddenRelations;
  private SpecificationSection section;
  private int? _skipLinesBefore;
  private int? _skipLinesAfter;
  private int? _positionStepBefore;
  private int? _positionStepAfter;
  private int _skipPagesAfter;
  /// <summary>Родитель в табличном виде</summary>
  private IVirtualTreeItem parentTreeItem;

  /// <summary> Постфикс единицы измерения величины "количество" по-умолчанию </summary>
  public static string DefaultMU_Count_str
  {
    [DebuggerStepThrough] get
    {
      if (AVSRow._defaultMU_Count_str != null)
        return AVSRow._defaultMU_Count_str;
      AVSRow.UpdateDefaultCountMeasure();
      return AVSRow._defaultMU_Count_str;
    }
  }

  /// <summary> Идентификатор единицы измерения по-умолчанию для количества </summary>
  public static long DefaultCountID
  {
    [DebuggerStepThrough] get
    {
      if (AVSRow.defaultCountID != -1L)
        return AVSRow.defaultCountID;
      AVSRow.UpdateDefaultCountMeasure();
      return AVSRow.defaultCountID;
    }
  }

  /// <summary> Единица измерения по-умолчанию для количества </summary>
  public static MeasureDescriptor DefaultCountMeasure
  {
    [DebuggerStepThrough] get
    {
      if (AVSRow.defaultCountMeasure != null)
        return AVSRow.defaultCountMeasure;
      AVSRow.UpdateDefaultCountMeasure();
      return AVSRow.defaultCountMeasure;
    }
  }

  /// <summary>Обновить кэш единиц измерения массы по умолчанию</summary>
  private static void UpdateDefaultMassMeasure()
  {
    AVSRow._defaultMU_Mass_str = "кг";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid("cad002eb-306c-11d8-b4e9-00304f19f545"), false);
      if (dbObject == null)
        return;
      AVSRow.defaultMassID = dbObject.ObjectID;
      AVSRow.defaultMassMeasure = MeasureHelper.FindDescriptor(AVSRow.defaultMassID);
      if (AVSRow.defaultMassMeasure == null)
        return;
      AVSRow._defaultMU_Mass_str = AVSRow.defaultMassMeasure.ShortName;
    }
  }

  /// <summary>Постфикс единицы измерения величины "масса" по-умолчанию </summary>
  public static string DefaultMU_Mass_str
  {
    [DebuggerStepThrough] get
    {
      if (AVSRow._defaultMU_Mass_str != null)
        return AVSRow._defaultMU_Mass_str;
      AVSRow.UpdateDefaultMassMeasure();
      return AVSRow._defaultMU_Mass_str;
    }
  }

  /// <summary> Идентификатор единицы измерения по-умолчанию для массы </summary>
  public static long DefaultMassID
  {
    [DebuggerStepThrough] get
    {
      if (AVSRow.defaultMassID.IsDefinedId())
        return AVSRow.defaultMassID;
      AVSRow.UpdateDefaultMassMeasure();
      return AVSRow.defaultMassID;
    }
  }

  /// <summary> Единица измерения по-умолчанию для массы </summary>
  public static MeasureDescriptor DefaultMassMeasure
  {
    [DebuggerStepThrough] get
    {
      if (AVSRow.defaultMassMeasure != null)
        return AVSRow.defaultMassMeasure;
      AVSRow.UpdateDefaultMassMeasure();
      return AVSRow.defaultMassMeasure;
    }
  }

  /// <summary>Обновить кэш единиц измерения количества по умолчанию</summary>
  private static void UpdateDefaultCountMeasure()
  {
    AVSRow._defaultMU_Count_str = "шт";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      AVSRow.defaultCountID = sessionKeeper.Session.GetObjectInfo(new Guid("cad002e8-306c-11d8-b4e9-00304f19f545")).ObjectID;
      if (!AVSRow.defaultCountID.IsDefinedId())
        return;
      AVSRow.defaultCountMeasure = MeasureHelper.FindDescriptor(AVSRow.defaultCountID);
      if (AVSRow.defaultCountMeasure == null)
        AVSRow.defaultCountMeasure = new MeasureDescriptor(true);
      else
        AVSRow._defaultMU_Count_str = AVSRow.defaultCountMeasure.ShortName;
    }
  }

  /// <summary>Кэш атрибутов связей</summary>
  [TypeConverter(typeof (RelationListConverter))]
  public List<RelationAttributeValuesCache> Relations
  {
    [DebuggerStepThrough] get => this.relations;
  }

  /// <summary>Кэш атрибутов связей</summary>
  [TypeConverter(typeof (RelationListConverter))]
  public List<RelationAttributeValuesCache> HiddenRelations
  {
    [DebuggerStepThrough] get => this.hiddenRelations;
  }

  /// <summary>Получить все связи</summary>
  public List<RelationAttributeValuesCache> GetAllRelations()
  {
    List<RelationAttributeValuesCache> allRelations = new List<RelationAttributeValuesCache>((this.relations != null ? this.relations.Count : 0) + (this.hiddenRelations != null ? this.hiddenRelations.Count : 0));
    if (this.relations != null && this.relations.Count > 0)
      allRelations.AddRange((IEnumerable<RelationAttributeValuesCache>) this.relations);
    if (this.hiddenRelations != null && this.hiddenRelations.Count > 0)
      allRelations.AddRange((IEnumerable<RelationAttributeValuesCache>) this.hiddenRelations);
    return allRelations;
  }

  /// <summary>Получить энумератор по всем связям</summary>
  [Browsable(false)]
  public IEnumerable<RelationAttributeValuesCache> AllRelations
  {
    get
    {
      if (this.relations != null)
      {
        foreach (RelationAttributeValuesCache relation in this.relations)
          yield return relation;
      }
      if (this.hiddenRelations != null)
      {
        foreach (RelationAttributeValuesCache hiddenRelation in this.hiddenRelations)
          yield return hiddenRelation;
      }
    }
  }

  /// <summary>Кэш атрибутов объекта</summary>
  [Browsable(false)]
  public AttributeValuesCache ObjectAttributesCache
  {
    [DebuggerStepThrough] get
    {
      if (this.objectAttributesCache == null && this.relations != null && this.relations.Count > 0)
        this.objectAttributesCache = this.relations[0].ObjectAttributesCache;
      return this.objectAttributesCache;
    }
    set
    {
      if (this.objectAttributesCache == value)
        return;
      this.objectAttributesCache = value;
      if (this.relations == null)
        return;
      for (int index = 0; index < this.relations.Count; ++index)
        this.relations[index].ObjectAttributesCache = this.objectAttributesCache;
    }
  }

  /// <summary>Идентификатор связи</summary>
  [Browsable(false)]
  public DBRelationInfo RowID
  {
    [DebuggerStepThrough] get => this.rowID;
  }

  /// <summary>Позиция записи</summary>
  [Browsable(false)]
  public int Position
  {
    get
    {
      object fieldValue = this.GetFieldValue(this.Field_Position, 0, -1, false, true);
      int result = -1;
      return int.TryParse(Convert.ToString(fieldValue), out result) ? result : 0;
    }
  }

  /// <summary>Индекс сортировки</summary>
  [Browsable(false)]
  public long SortIndex
  {
    get
    {
      if (this.avsDocument == null || !this.avsDocument.IsSpecification)
        return long.MinValue;
      if (this.HasRelation || this.HasHiddenRelation)
      {
        object fieldValue = this.GetFieldValue(this.avsDocument.Attr_SortIndex, 0, -1, this.relations, true, false);
        if (fieldValue != null)
        {
          long int64 = Convert.ToInt64(fieldValue);
          if (!AVSRow.SortIndexIsFree(int64))
            return int64;
        }
      }
      return this.sortIndex;
    }
    set
    {
      this.SetSortIndex(value, this.avsDocument != null && !this.avsDocument.ReadOnly, false, false);
    }
  }

  /// <summary>Проверить и синхронизировать все сортировки на связях</summary>
  internal void SyncSortIndexForRelations()
  {
    if (this.IsFreeSortIndex || !this.HasRelation && !this.HasHiddenRelation)
      return;
    long sortIndex = this.SortIndex;
    List<RelationAttributeValuesCache> allRelations = this.GetAllRelations();
    for (int index = 0; index < allRelations.Count; ++index)
    {
      if (allRelations[index].SortIndex != sortIndex)
        this.SetFieldValue(this.avsDocument.Attr_SortIndex, index, -1, allRelations, (object) sortIndex, false, false, false, false, false, false);
    }
  }

  /// <summary>Назначить новое значение SortIndex</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="saveToDB">Сохранить значение в БД</param>
  /// <param name="updateDocNode">Обновить узел документа</param>
  /// <param name="updateListNode">Обновить узел TreeList</param>
  public void SetSortIndex(long value, bool saveToDB, bool updateDocNode, bool updateListNode)
  {
    if (this.avsDocument == null || !this.avsDocument.IsSpecification)
      return;
    if (saveToDB && this.avsDocument.ReadOnly)
      saveToDB = false;
    long sortIndex = this.SortIndex;
    if (sortIndex == value)
      return;
    if (this.avsDocument != null)
    {
      if (value != 0L && value != long.MinValue)
      {
        AVSRow avsRow;
        if (!this.avsDocument.SortIndexDictionary.TryGetValue(value, out avsRow))
          this.avsDocument.SortIndexDictionary.Add(value, this);
        else if (avsRow != this)
          throw new Exception("Дублирование индекса сортировки!");
      }
      AVSRow avsRow1;
      if (sortIndex != 0L && sortIndex != long.MinValue && this.avsDocument.SortIndexDictionary.TryGetValue(sortIndex, out avsRow1) && avsRow1 == this)
        this.avsDocument.SortIndexDictionary.Remove(sortIndex);
    }
    this.sortIndex = value;
    if (this.avsDocument == null)
      return;
    bool isRowsUpdating = this.avsDocument.IsRowsUpdating;
    try
    {
      this.avsDocument.IsRowsUpdating = true;
      this.SetFieldValue(this.avsDocument.Attr_SortIndex, -1, -1, this.relations, (object) value, saveToDB, false, updateDocNode, updateListNode, false, false, false);
      if (!this.HasHiddenRelation)
        return;
      this.SetFieldValue(this.avsDocument.Attr_SortIndex, -1, -1, this.hiddenRelations, (object) value, saveToDB, false, updateDocNode, updateListNode, false, false, false);
    }
    finally
    {
      this.avsDocument.IsRowsUpdating = isRowsUpdating;
    }
  }

  /// <summary>Эта запись была отсортирована</summary>
  [Browsable(false)]
  public bool IsSorted
  {
    [DebuggerStepThrough] get => this.isSorted;
    set
    {
      if (this.isSorted == value)
        return;
      this.isSorted = value;
    }
  }

  /// <summary>Свободный индекс сортировки</summary>
  [Browsable(false)]
  public bool IsFreeSortIndex
  {
    get
    {
      return this.avsDocument == null || !this.avsDocument.IsSpecification || AVSRow.SortIndexIsFree(this.SortIndex);
    }
  }

  internal static bool SortIndexIsFree(long sortIndex)
  {
    return sortIndex == 0L || sortIndex == long.MinValue;
  }

  /// <summary>При сортировке располагать запись после заданной</summary>
  [Browsable(false)]
  public AVSRow SortAfterRow
  {
    get
    {
      if (this.sortAfterRow != null)
        return this.sortAfterRow;
      if (this.RelType == AvsIDCache.Relation_Podbor && this.HasRelation && this.avsDocument != null && this.avsDocument.SortSchema != null && this.avsDocument.SortSchema.SortPartForPodborAfterBasePart)
      {
        foreach (PosDesignationRecord designationRecord in this.GetPosDesignationRecord(this.Attr_PodborForPosDesignation, 0, this.Relations))
        {
          RelationAttributeValuesCache attributeValuesCache;
          if (this.avsDocument.PosDesignation_Dictionary.TryGetValue(designationRecord.Designation, out attributeValuesCache))
          {
            AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(attributeValuesCache.RelationId);
            if (avsDocRow != null)
              return avsDocRow;
          }
        }
      }
      return (AVSRow) null;
    }
    set
    {
      if (this.sortAfterRow != value)
      {
        if (value == this)
          throw new Exception($"Недопустимая привязка сортировки записи '{this.Caption}'.\r\nНельзя привязывать запись к самой себе!");
        for (AVSRow avsRow = value; avsRow != null; avsRow = avsRow.SortAfterRow)
        {
          if (avsRow == this)
            throw new Exception($"Недопустимая привязка сортировки записи '{this.Caption}'.\r\nЕсли располагать эту запись после записи '{value.Caption}',\r\nто возникает циклическая зависимость, начиная с записи '{avsRow.Caption}'\r\n");
        }
        this.sortAfterRow = value;
        if (this.sortAfterRow != null)
        {
          LogManager.AddLine("AVS.SortAfterRow Сброшена взаимоисключающая настройка SortBeforeRow для записи: " + this.ToString());
          this.SortBeforeRow = (AVSRow) null;
          if (this.sortAfterRow.SortBeforeRow == this)
          {
            this.sortAfterRow.SortBeforeRow = (AVSRow) null;
            LogManager.AddLine("AVS.SortAfterRow Сброшена двойная привязка sortAfterRow.SortBeforeRow для записи: " + this.ToString());
          }
        }
      }
      if (this.sortAfterRow != null)
        return;
      this.SetAttributeValuesToDocNodes(AVSRow.RowAttr_SortAfterRowBySortIndex, (string) null);
      this.SetAttributeValuesToDocNodes(AVSRow.RowAttr_SortAfterRowByID, (string) null);
    }
  }

  /// <summary>Заголовок SortAfterRow для отображения в свойствах записи</summary>
  [DefaultValue("")]
  [DisplayName("Размещать после записи")]
  [Description("При сортировке всегда размещать запись после заданной")]
  [Category("Сортировка")]
  public string SortAfterRowCaption => this.SortAfterRow != null ? this.SortAfterRow.Caption : "";

  /// <summary>При сортировке располагать запись перед заданной</summary>
  [Browsable(false)]
  public AVSRow SortBeforeRow
  {
    get => this.sortBeforeRow;
    set
    {
      if (this.sortBeforeRow != value)
      {
        if (value == this)
          throw new Exception($"Недопустимая привязка сортировки записи '{this.Caption}'.\r\nНельзя привязывать запись к самой себе!");
        for (AVSRow avsRow = value; avsRow != null; avsRow = avsRow.SortBeforeRow)
        {
          if (avsRow == this)
            throw new Exception($"Недопустимая привязка сортировки записи '{this.Caption}'. Если располагать эту запись перед записью '{value.Caption}', то возникает циклическая зависимость, начиная с записи '{avsRow.Caption}'");
        }
        this.sortBeforeRow = value;
        if (this.sortBeforeRow != null)
        {
          this.SortAfterRow = (AVSRow) null;
          LogManager.AddLine("AVS.SortBeforeRow Сброшена взаимоисключающая настройка SortAfterRow для записи: " + this.ToString());
          if (this.sortBeforeRow.SortAfterRow == this)
          {
            this.sortBeforeRow.SortAfterRow = (AVSRow) null;
            LogManager.AddLine("AVS.SortBeforeRow Сброшена двойная привязка sortBeforeRow.SortAfterRow для записи: " + this.ToString());
          }
        }
      }
      if (this.sortBeforeRow != null)
        return;
      this.SetAttributeValuesToDocNodes(AVSRow.RowAttr_SortBeforeRowBySortIndex, (string) null);
      this.SetAttributeValuesToDocNodes(AVSRow.RowAttr_SortBeforeRowByID, (string) null);
    }
  }

  /// <summary>Заголовок SortBeforeRow для отображения в свойствах записи</summary>
  [DefaultValue("")]
  [DisplayName("Размещать перед записью")]
  [Description("При сортировке всегда размещать запись перед заданной")]
  [Category("Сортировка")]
  public string SortBeforeRowCaption
  {
    get => this.SortBeforeRow != null ? this.SortBeforeRow.Caption : "";
  }

  /// <summary>Раздел владелец записи</summary>
  [Browsable(false)]
  public SpecificationSection Section
  {
    [DebuggerStepThrough] get => this.section;
    set => this.section = value;
  }

  /// <summary>Идентификатор раздела</summary>
  [Browsable(false)]
  public long SectionID
  {
    [DebuggerStepThrough] get
    {
      if (this.section != null)
        return this.section.ChapterID;
      if (this.HasRelation)
        return this.GetFieldInt64Value(this.Attr_Section, 0, this.relations, true);
      return this.HasHiddenRelation ? this.GetFieldInt64Value(this.Attr_Section, 0, this.HiddenRelations, true) : -1L;
    }
  }

  /// <summary>Глобальный идентификатор версии дополнительной части спецификации</summary>
  [Browsable(false)]
  public Guid? AdditionalChapterGuid
  {
    get
    {
      return !(this.GetRootChapter() is AdditionalChapter rootChapter) ? new Guid?() : new Guid?(rootChapter.ChapterGuid);
    }
  }

  /// <summary>Исполнение</summary>
  [Browsable(false)]
  public ProductInfo Product
  {
    [DebuggerStepThrough] get
    {
      if (this.section != null && this.section.Product != null)
        return this.section.Product;
      long productId = this.ProductID;
      if (productId != -1L && this.avsDocument != null)
      {
        int productIndex = this.avsDocument.GetProductIndex(productId);
        if (productIndex != -1)
          return this.avsDocument.productsInfo[productIndex];
      }
      return (ProductInfo) null;
    }
  }

  /// <summary>Получить часть верхнего уровня к которой принадлежит запись</summary>
  public Chapter GetRootChapter()
  {
    return this.section != null ? this.section.GetRootChapter() : (Chapter) null;
  }

  /// <summary>Идентификаторы исполнений куда входит запись</summary>
  [Browsable(false)]
  public List<long> ProductIDs
  {
    get
    {
      List<long> productIds = new List<long>();
      if (this.IsFormB)
      {
        if (this.relations != null && this.relations.Count > 0)
          productIds.AddRange(this.relations.Select<RelationAttributeValuesCache, long>((Func<RelationAttributeValuesCache, long>) (x => x.ProjectId)));
      }
      else if (this.ProductID != -1L)
        productIds.Add(this.ProductID);
      return productIds;
    }
  }

  /// <summary>Идентификатор исполнения. -1, если общие данные</summary>
  [Browsable(false)]
  public long ProductID
  {
    [DebuggerStepThrough] get
    {
      long productId = -1;
      if (this.section != null && this.section.Product != null)
        productId = this.section.Product.Id;
      if (productId != -1L)
        return productId;
      if (this.relations != null && this.relations.Count == 1)
      {
        if (this.section == null)
          return this.relations[0].ProjectId;
        Chapter productChapter = this.section.ProductChapter;
        return productChapter != null && productChapter is ProductVariableDataChapter ? productChapter.ChapterID : -1L;
      }
      return this.avsDocument != null && this.avsDocument.AvsDocumentForm == AVSDocumentForm.Single ? this.avsDocument.ProductId : -1L;
    }
  }

  /// <summary>Обозначение</summary>
  [Browsable(false)]
  public string Designation
  {
    [DebuggerStepThrough] get
    {
      object obj = this.ObjectAttributesCache == null ? (object) null : this.ObjectAttributesCache.GetValue(AvsIDCache.Attr_Designation, false);
      if (obj == null)
      {
        TextData cellForAttribute = this.GetDocumentCellForAttribute(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Designation), -1);
        if (cellForAttribute != null)
          obj = (object) cellForAttribute.GetAttributeValue("FullDesignation", false) ?? (object) cellForAttribute.Text;
      }
      return obj != null && obj is string ? (string) obj : (string) null;
    }
  }

  /// <summary>Наименование</summary>
  [Browsable(false)]
  public string Name
  {
    get
    {
      object obj = this.ObjectAttributesCache == null ? (this.DocNode == null ? (object) null : this.GetFieldValue(new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, AvsIDCache.DocAttr_Name), -1, -1, this.relations, true, false)) : this.ObjectAttributesCache.GetValue(AvsIDCache.Attr_Name, false);
      return obj != null && obj is string ? (string) obj : (string) null;
    }
  }

  /// <summary>Обозначение, если оно не пустое или Наименование</summary>
  [Browsable(false)]
  public string DesignationOrName
  {
    [DebuggerStepThrough] get
    {
      string designation = this.Designation;
      return !string.IsNullOrEmpty(designation) ? designation : this.Name;
    }
  }

  /// <summary>Код ОКП</summary>
  [Browsable(false)]
  public string OKPCode
  {
    get
    {
      object obj = this.ObjectAttributesCache == null ? (object) null : this.ObjectAttributesCache.GetValue(AvsIDCache.Attr_OKPCode, false);
      return obj != null && obj is string ? (string) obj : (string) null;
    }
  }

  /// <summary>Графа Наименование</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_Name
  {
    get => this.avsDocument != null ? this.avsDocument.Field_Name : AvsIDCache.StdField_Name;
  }

  /// <summary>Графа Обозначение</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_Designation
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Field_Designation : new AvsRowAttributeInfo(false, AvsIDCache.Attr_Designation);
    }
  }

  /// <summary>Графа Формат</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_Format
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Field_Format : new AvsRowAttributeInfo(false, AvsIDCache.Attr_Format);
    }
  }

  /// <summary>Графа Зона</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_Zone
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Field_Zone : new AvsRowAttributeInfo(true, AvsIDCache.Attr_Zone);
    }
  }

  /// <summary>Графа Позиция</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_Position
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Field_Position : new AvsRowAttributeInfo(true, AvsIDCache.Attr_Position);
    }
  }

  /// <summary>Графа Количество</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_Count
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Field_Count : new AvsRowAttributeInfo(true, AvsIDCache.Attr_Count);
    }
  }

  /// <summary>Графа Позиционное обозначение</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_PosDesignation
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Field_PosDesignation : new AvsRowAttributeInfo(true, AvsIDCache.Attr_PosDesignation);
    }
  }

  /// <summary>Атрибут Подбор</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Attr_Podbor
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Attr_Podbor : new AvsRowAttributeInfo(true, AvsIDCache.Attr_Podbor);
    }
  }

  /// <summary>Атрибут "Смотри". Хранится только в строке документа</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Attr_Smotri
  {
    get
    {
      if (this.attr_Smotri == null)
        this.attr_Smotri = new AvsRowAttributeInfo(FieldSource.DocumentRowField, Guid.Empty, -1, AVSRow.DocAttr_Smotri);
      return this.attr_Smotri;
    }
  }

  /// <summary>Атрибут Подбор для позиционного обозначения</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Attr_PodborForPosDesignation
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Attr_PodborForPosDesignation : new AvsRowAttributeInfo(true, AvsIDCache.Attr_PodborForPosDesignation);
    }
  }

  /// <summary>Атрибут "Элемент перечня элементов"</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Attr_IncludeInElementList
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Attr_IncludeInElementList : new AvsRowAttributeInfo(true, AvsIDCache.Attr_IncludeInElementList);
    }
  }

  /// <summary>Атрибут "Раздел"</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Attr_Section
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Attr_Section : new AvsRowAttributeInfo(FieldSource.Relation, new Guid("cad00266-306c-11d8-b4e9-00304f19f545"), AvsIDCache.Attr_SpecificationSection, "Раздел спецификации", ColumnContents.ID);
    }
  }

  /// <summary>Атрибут "Не отображать в спецификации"</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Attr_HideInSpecification
  {
    get
    {
      return this.avsDocument != null ? this.avsDocument.Attr_HideInSpecification : new AvsRowAttributeInfo(true, AvsIDCache.Attr_HideInSpecification);
    }
  }

  /// <summary>Графа Примечание</summary>
  [Browsable(false)]
  public AvsRowAttributeInfo Field_Note
  {
    get
    {
      if (this._field_Note != null)
        return this._field_Note;
      if (this.avsDocument != null && this.avsDocument.IsElementList && this.NewCellMappingMode && this.NoteCellMapping != null && this.NoteCellMapping.ContainsAttribute((AttributeInfo) AvsIDCache.StdField_NotePE))
        this._field_Note = AvsIDCache.StdField_NotePE.Clone();
      if (this._field_Note == null)
        this._field_Note = this.avsDocument?.Field_Note ?? AvsIDCache.StdField_Note.Clone();
      return this._field_Note;
    }
  }

  /// <summary>
  /// У записей одинаковые или нет идентификаторы группового изделия
  /// </summary>
  /// <param name="row1"></param>
  /// <param name="row2"></param>
  /// <param name="DesignationTrimSchema"></param>
  /// <returns></returns>
  public static bool IsSameArticleGroupID(
    AVSRow row1,
    AVSRow row2,
    DesignationTrimSchema DesignationTrimSchema)
  {
    int num;
    if (row1.ArticleGroupID.HasValue && row2.ArticleGroupID.HasValue)
    {
      Guid? articleGroupId1 = row1.ArticleGroupID;
      Guid? articleGroupId2 = row2.ArticleGroupID;
      num = articleGroupId1.HasValue == articleGroupId2.HasValue ? (articleGroupId1.HasValue ? (articleGroupId1.GetValueOrDefault() == articleGroupId2.GetValueOrDefault() ? 1 : 0) : 1) : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    if (!DesignationTrimSchema.UseGroupNumberAttribute)
      flag = false;
    return flag;
  }

  /// <summary>Идентификатор группового изделия</summary>
  [Browsable(false)]
  public Guid? ArticleGroupID
  {
    get
    {
      object obj = (object) null;
      if (this.ObjectAttributesCache != null)
        obj = this.ObjectAttributesCache.GetValue(AvsIDCache.Attr_ArticleGroupID, false);
      switch (obj)
      {
        case Guid guid:
          return new Guid?(guid);
        case string _:
          if (GuidHelper.IsGuid((string) obj))
            return new Guid?(new Guid((string) obj));
          break;
      }
      return new Guid?();
    }
  }

  /// <summary>Класс стандартного изделия</summary>
  [Browsable(false)]
  public string Class
  {
    get
    {
      object obj = (object) null;
      if (this.ObjectAttributesCache != null)
        obj = this.ObjectAttributesCache.GetValue(this.avsDocument.Attr_Class, false);
      return obj != null && obj is string ? (string) obj : (string) null;
    }
  }

  /// <summary>Смотри</summary>
  [DefaultValue("")]
  [DisplayName("Смотри")]
  [Description("Текст для оформления ссылки на главный конструкторский документ, когда его обозначение значительно отличается от обозначения изделия. Например: \"(см. 123.456.000)\" ")]
  [Category("Дополнительные атрибуты")]
  public string TextLinkToMainDocument
  {
    get => this.DocNode == null ? "" : this.DocNode.GetAttributeValue(AVSRow.DocAttr_Smotri, true);
    set
    {
      if (!this.HasDocNodes || !(this.TextLinkToMainDocument != value))
        return;
      this.SetAttributeValuesToDocNodes(AVSRow.DocAttr_Smotri, value, updateDocument: true);
    }
  }

  /// <summary>
  /// Заголовок записи для показа его пользователю в сообщениях
  /// </summary>
  public string Caption
  {
    get
    {
      string designation = this.Designation;
      string name = this.Name;
      string str1 = "";
      string str2 = !string.IsNullOrEmpty(designation) ? (!string.IsNullOrEmpty(name) ? $"{designation} ({name})" : designation) : name;
      string str3 = "";
      if (this.avsDocument.IsSpecification)
      {
        str3 = this.GetFieldStringValue(this.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false);
        if (!string.IsNullOrEmpty(str3))
          str3 = $"[Поз. {str3}]";
      }
      else if (this.avsDocument.IsElementList)
      {
        str3 = this.GetTextForDocCell((CellOutputMapping) null, this.Field_PosDesignation, 0, -1, false, false);
        if (!string.IsNullOrEmpty(str3))
          str3 = $"[Поз.обозначение: {str3}]";
      }
      return !string.IsNullOrEmpty(str2) ? (!string.IsNullOrEmpty(str3) ? (str1 = $"{str2} {str3}") : str2) : (!string.IsNullOrEmpty(str3) ? str3 : "[Пустая строка]");
    }
  }

  /// <summary>Преобразовать в строку</summary>
  public override string ToString()
  {
    if (this.HasRelation)
      return this.relations[0].ToString();
    if (this.HasObject)
      return this.ObjCaption;
    return this.docNode != null ? $"{base.ToString()} {this.docNode.ToString()}" : base.ToString();
  }

  /// <summary>Для внутреннего использования. Получить значение атрибута типа long</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="relationList">Список связей</param>
  /// <param name="failIfNotFound">Генерировать исключение, если атрибут не найден</param>
  /// <returns>Если атрибут не найден, то значение -1</returns>
  public long GetFieldInt64Value(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    List<RelationAttributeValuesCache> relationList,
    bool failIfNotFound)
  {
    return AvsIDCache.ConvertDbValueToInt64(this.GetFieldValue(attrInfo, relationIndex, -1, relationList, false, failIfNotFound));
  }

  /// <summary>Отсортирована ли запись</summary>
  /// <param name="row">Запись</param>
  /// <returns>Отсортирована ли запись</returns>
  public static bool IsSortedSpecRow(object row)
  {
    AVSRow avsRow = (AVSRow) row;
    return avsRow != null && avsRow.IsSorted;
  }

  internal static bool IsEqualOrFreeSortIndex(
    RelationAttributeValuesCache relation1,
    RelationAttributeValuesCache relation2)
  {
    long sortIndex1 = relation1.SortIndex;
    if (AVSRow.SortIndexIsFree(sortIndex1))
      return true;
    long sortIndex2 = relation2.SortIndex;
    return AVSRow.SortIndexIsFree(sortIndex2) || sortIndex1 == sortIndex2;
  }

  internal static bool IsEqualSortIndex(
    RelationAttributeValuesCache relation1,
    RelationAttributeValuesCache relation2)
  {
    long num1 = relation1.SortIndex;
    if (num1 == long.MinValue)
      num1 = 0L;
    long num2 = relation2.SortIndex;
    if (num2 == long.MinValue)
      num2 = 0L;
    return num1 == num2;
  }

  internal static bool IsEqualCount(
    RelationAttributeValuesCache relation1,
    RelationAttributeValuesCache relation2)
  {
    int attributeID = AvsIDCache.Attr_Count;
    if (relation1.RelationType == AvsIDCache.Relation_Podbor)
      attributeID = AvsIDCache.Attr_CountForAdjustment;
    object obj1 = relation1.GetValue(attributeID, false);
    object obj2 = relation2.GetValue(attributeID, false);
    MeasuredValue measuredValue1 = AVSRow.ConvertCountToMeasuredValue(obj1);
    MeasuredValue measuredValue2 = AVSRow.ConvertCountToMeasuredValue(obj2);
    return measuredValue1 == null || measuredValue2 == null ? measuredValue1 == measuredValue2 : MeasureHelper.Compare(measuredValue1, measuredValue2) == CompareResult.Equal;
  }

  internal static bool IsEqualStringAttributeValues(
    AvsRowAttributeInfo attribute,
    RelationAttributeValuesCache relation1,
    RelationAttributeValuesCache relation2)
  {
    return relation1.GetValueString(attribute, false) == relation2.GetValueString(attribute, false);
  }

  public bool IsEqualsSearchId(string searchArtId)
  {
    if (string.IsNullOrEmpty(searchArtId) || searchArtId == "0")
      return false;
    string fieldStringValue = this.GetFieldStringValue(this.avsDocument.Attr_SearchId, -1, -1, (List<RelationAttributeValuesCache>) null, false);
    if (string.IsNullOrEmpty(fieldStringValue) || string.IsNullOrEmpty(fieldStringValue))
      return false;
    if (searchArtId[0] == 'A')
      searchArtId = searchArtId.Substring(1);
    return searchArtId == fieldStringValue;
  }

  /// <summary>Узел документа представляющий эту строку</summary>
  [Browsable(false)]
  public TableData DocNode
  {
    [DebuggerStepThrough] get => this.docNode;
    set
    {
      if (this.docNode == value)
        return;
      if (value == null)
        this.DocNodes = new List<TableData>();
      else
        this.DocNodes = new List<TableData>() { value };
    }
  }

  /// <summary>Отвязать строку документа от записи</summary>
  /// <param name="docRow">Строка документа</param>
  private void DisconnectDocNodeWithRow(TableData docRow)
  {
    foreach (TextData textData in (IEnumerable<TextData>) docRow.TextCellsEnumerator)
    {
      textData.TextValidating -= new TextValidating_EventHandler(this.cell_TextValidating);
      textData.TextReadOnly -= new TextReadOnly_EventHandler(this.cell_TextReadOnly);
      textData.TextChanged -= new TextChanged_EventHandler(this.cell_TextChanged);
      if (textData is IPageElementWithInterface elementWithInterface)
      {
        elementWithInterface.InplaceEditorActivating -= new CancelEventHandler(this.cell_InplaceEditorActivating);
        elementWithInterface.InplaceEditorActivated -= new EventHandler(this.cell_InplaceEditorActivated);
        elementWithInterface.InplaceEditorDeactivated -= new EventHandler(this.cell_InplaceEditorDeactivated);
      }
    }
    docRow.AttributeValueChanged -= new AttributeValueChanged_EventHandler(this.DocNode_AttributeValueChanged);
    docRow.GetPluginVirtualAttributeNames -= new GetPluginVirtualAttributeNames_EventHandler(this.DocRow_GetPluginVirtualAttributeNames);
    docRow.GetPluginVirtualAttributeValue -= new GetPluginVirtualAttributeValue_EventHandler(this.DocRow_GetPluginVirtualAttributeValue);
    docRow.BeforeDistribute -= new BeforeDistribute_EventHandler(this.docRow_BeforeDistribute);
    docRow.Tag = (object) null;
    docRow.UniteTable();
    docRow.Remove(false, false, false);
  }

  /// <summary>Привязать строку документа к записи, назначить обработчики и установить свойства</summary>
  /// <param name="docRow">Строка документа</param>
  /// <param name="rowIndex">Индекс строки в коллекции DocNodes</param>
  /// <param name="exportRow">Строка экспортной СП</param>
  private void ConnectDocNodeWithRow(TableData docRow, int rowIndex, bool exportRow)
  {
    if (docRow == null)
      return;
    docRow.SetTableCellType(CellType.DataCell, false, false);
    if (!this.HasRelation && rowIndex == 0)
    {
      if (this.RowID != null && this.RelType != -1)
        docRow.SetAttributeValue(AVSRow.RowAttr_RelationType, MetaDataHelper.GetRelationTypeGuid(this.RelType).ToString(), false, false, false);
      else
        docRow.RemoveAttribute(AVSRow.RowAttr_RelationType, false, false);
    }
    if (this.HasRelation)
      docRow.RemoveAttribute(AVSRow.RowAttr_RelationType, false, false);
    if (this.HasRelation || this.HasObject)
      this.SaveRelationsReferencesToDocRow(docRow);
    long sortIndex = this.SortIndex;
    if (!this.IsFreeSortIndex)
      docRow.SetAttributeValue(AVSRow.RowAttr_SortIndex, sortIndex.ToString());
    if (this.IsNoteRow)
      docRow.SetAttributeValue(Chapter.DocNodeType_AttributeName, Chapter.SpecNote_TypeName, false, false, false);
    else
      docRow.SetAttributeValue(Chapter.DocNodeType_AttributeName, Chapter.AVSRow_TypeName, false, false, false);
    if (!exportRow)
    {
      if (this._positionStepBefore.HasValue)
        docRow.SetAttributeValue(AVSRow.RowAttr_PositionStepBefore, this._positionStepBefore.Value.ToString(), false, false, false);
      else
        docRow.RemoveAttribute(AVSRow.RowAttr_PositionStepBefore, false, false);
      if (this._positionStepAfter.HasValue)
        docRow.SetAttributeValue(AVSRow.RowAttr_PositionStepAfter, this._positionStepAfter.Value.ToString(), false, false, false);
      else
        docRow.RemoveAttribute(AVSRow.RowAttr_PositionStepAfter, false, false);
    }
    bool? fromNewPage = this.FromNewPage;
    if (fromNewPage.HasValue)
    {
      TableData tableData = docRow;
      fromNewPage = this.FromNewPage;
      int num = fromNewPage.Value ? 1 : 0;
      tableData.SetFromNewPage(num != 0, false, false);
    }
    List<AvsRowAttributeInfo> rowAttributeInfoList = exportRow ? this.DocRowFields_Exp : this.DocRowFields;
    int index = -1;
    foreach (TextData cell in (IEnumerable<TextData>) docRow.TextCellsEnumerator)
    {
      ++index;
      if (cell != null && rowAttributeInfoList != null)
      {
        if (!this.IsNoteRow && index < rowAttributeInfoList.Count && rowAttributeInfoList[index] != null && rowAttributeInfoList[index].IsObjectAttribute)
        {
          if (rowAttributeInfoList[index].AttributeId == AvsIDCache.Attr_Name)
            cell.ReadOnly = !AvsConfig.General.AllowNoteForSpecRowName || this.IsDocRelation || !this.avsDocument.IsSpecification;
          if (rowAttributeInfoList[index].AttributeId == AvsIDCache.Attr_Format && (this.IsDocRelation || MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing)))
            cell.ReadOnly = true;
        }
        cell.TextValidating += new TextValidating_EventHandler(this.cell_TextValidating);
        cell.TextReadOnly += new TextReadOnly_EventHandler(this.cell_TextReadOnly);
        cell.TextChanged += new TextChanged_EventHandler(this.cell_TextChanged);
        if (cell is IPageElementWithInterface elementWithInterface)
        {
          elementWithInterface.InplaceEditorActivating += new CancelEventHandler(this.cell_InplaceEditorActivating);
          elementWithInterface.InplaceEditorActivated += new EventHandler(this.cell_InplaceEditorActivated);
          elementWithInterface.InplaceEditorDeactivated += new EventHandler(this.cell_InplaceEditorDeactivated);
        }
        if ((AVSRow.IsCountFormBCell(this.IsFormB, cell) || index < rowAttributeInfoList.Count && AVSRow.IsCountAttribute(rowAttributeInfoList[index])) && !this.IsNoteRow)
        {
          cell.CanCallExternalEditor = new CanCallDocNodeEditorDelegate(this.CanCallCountDocCellEditor);
          cell.CallExternalEditor = new CallDocNodeEditorDelegate(this.CallCountDocCellEditor);
        }
        if (index < rowAttributeInfoList.Count && !this.IsNoteRow && this.Field_Name.Equals((AttributeInfo) rowAttributeInfoList[index]))
        {
          cell.CanCallExternalEditor = new CanCallDocNodeEditorDelegate(this.CanCallNameDocCellEditor);
          cell.CallExternalEditor = new CallDocNodeEditorDelegate(this.CallNameDocCellEditor);
        }
      }
    }
    docRow.AttributeValueChanged += new AttributeValueChanged_EventHandler(this.DocNode_AttributeValueChanged);
    docRow.GetPluginVirtualAttributeNames += new GetPluginVirtualAttributeNames_EventHandler(this.DocRow_GetPluginVirtualAttributeNames);
    docRow.GetPluginVirtualAttributeValue += new GetPluginVirtualAttributeValue_EventHandler(this.DocRow_GetPluginVirtualAttributeValue);
    docRow.BeforeDistribute += new BeforeDistribute_EventHandler(this.docRow_BeforeDistribute);
    docRow.Tag = (object) this;
  }

  /// <summary>Обновить заголовки для позиций</summary>
  /// <param name="docRow">Строка документа</param>
  protected void UpdatePositionsCaptions(TableData docRow)
  {
    if (docRow == null || !(docRow.Tag is AVSRow tag) || this.avsDocument.ReadOnly || tag.IsNoteRow || !(this.GetDocumentCellForBaseField(this.Field_Position, docRow, -1) is TextBoxElement cellForBaseField))
      return;
    bool flag = false;
    string valueFromDocCell1 = AVSRow.GetFieldValueFromDocCell((TextData) cellForBaseField);
    if (!string.IsNullOrEmpty(valueFromDocCell1))
    {
      int index = docRow.Index;
      AVSRow avsRow = (AVSRow) null;
      TableData docRow1 = (TableData) null;
      if (index > 0)
        docRow1 = docRow.Parent.Nodes[index - 1] as TableData;
      if (docRow1 != null)
        avsRow = docRow1.Tag as AVSRow;
      if (avsRow != null && avsRow.Section == tag.Section)
      {
        string valueFromDocCell2 = avsRow.GetFieldValueFromDocCell(docRow1, tag.Field_Position);
        if (this.avsDocument.AVSCommonPropertiesSchema.HideEqualNumber && valueFromDocCell1 == valueFromDocCell2)
          flag = true;
      }
    }
    this.avsDocument.Lock_DocCell_TextChanged();
    if (flag)
      AVSRow.SetDocCellText((TextData) cellForBaseField, valueFromDocCell1, "");
    else
      AVSRow.SetDocCellText((TextData) cellForBaseField, valueFromDocCell1);
    this.avsDocument.Unlock_DocCell_TextChanged();
  }

  /// <summary>Обновить обозначения различных исполнений изделий в записях идущих подряд</summary>
  /// <param name="docRow">Строка документа</param>
  /// <param name="designationTrimSchema">Настройки сравнения обозначений</param>
  /// <param name="prevFullFormatElem">Предыдущий элемент с полным форматом</param>
  /// <param name="prevFullRow">Предыдущая полная запись</param>
  public void UpdatePartProductCaptions(
    TableData docRow,
    DesignationTrimSchema designationTrimSchema)
  {
    if (docRow == null || !(docRow.Tag is AVSRow tag) || tag.avsDocument.ReadOnly || tag.IsNoteRow || !(tag.GetDocumentCellForBaseField(tag.Field_Designation, docRow, -1) is TextBoxElement cellForBaseField1) || tag.HasComplexDesignation((TextData) cellForBaseField1))
      return;
    string str1 = cellForBaseField1.ContainsAttribute(AVSRow.CellAttrName_FullDesignation) ? cellForBaseField1.GetAttributeValue(AVSRow.CellAttrName_FullDesignation, true) : AVSRow.GetFieldValueFromDocCell((TextData) cellForBaseField1);
    CellOutputMapping attributeMapping1 = tag.GetCellAttributeMapping((TextData) cellForBaseField1);
    if (attributeMapping1 != null)
    {
      List<OutputMappingBase> items = attributeMapping1.Items;
      if (!((items != null ? items.FirstOrDefault<OutputMappingBase>() : (OutputMappingBase) null) is AttributeMapping attributeMapping2) || !attributeMapping2.Equals((object) tag.Field_Designation))
        return;
    }
    TextBoxElement cellForBaseField2 = tag.GetDocumentCellForBaseField(tag.Field_Format, docRow, -1) as TextBoxElement;
    int index = docRow.Index;
    TableData docRow1 = (TableData) null;
    AVSRow row2 = (AVSRow) null;
    if (index != 0)
    {
      docRow1 = docRow.Parent.Nodes[index - 1] as TableData;
      row2 = docRow1.Tag as AVSRow;
    }
    bool flag1 = false;
    bool documentationSection = tag.Section.IsDocumentationSection;
    if (designationTrimSchema != null)
    {
      bool flag2 = designationTrimSchema.UseSameProductDesignationsInRows;
      if (documentationSection && !designationTrimSchema.UseInDocumentation)
        flag2 = false;
      if (flag2 && row2 != null && docRow1 != null && docRow.Page != null && docRow1.Page != null)
      {
        string designation1 = tag.Designation;
        bool flag3 = AVSRow.IsSameArticleGroupID(tag, row2, designationTrimSchema);
        if (designation1 != null && designation1.Length > designationTrimSchema.LengthBasePart | flag3)
        {
          int startIndex = designation1.Length - 1;
          int num1 = designationTrimSchema.LengthBasePart - 1;
          if (flag3)
            num1 = 0;
          int count = startIndex - num1;
          int num2 = designation1.LastIndexOf("-", startIndex, count);
          bool flag4 = flag3;
          if (!flag4 && num2 != -1 && num2 < designation1.Length - 1 && char.IsDigit(designation1[num2 + 1]))
          {
            string designation2 = row2.Designation;
            string suffiks1 = "";
            string suffiks2 = "";
            string basePart = tag.avsDocument.GetBasePart(designation1, designationTrimSchema, out suffiks1, DocumentTypeSettingsHelper.GetSettings(tag.ObjType));
            string str2 = designation2;
            if (str2 != basePart)
              str2 = tag.avsDocument.GetBasePart(designation2, designationTrimSchema, out suffiks2, DocumentTypeSettingsHelper.GetSettings(row2.ObjType));
            if (basePart == str2 && suffiks1 == suffiks2)
              flag4 = true;
          }
          if (num2 != -1 & flag4)
          {
            str1.Substring(0, num2);
            string editValue = str1.Substring(num2);
            tag.avsDocument.Lock_DocCell_TextChanged();
            bool flag5 = false;
            int num3 = 0;
            while (!flag5)
            {
              if (num3 < 2)
              {
                try
                {
                  ImRtfEditor specificationEditor = tag.avsDocument.SpecificationEditor;
                  if (specificationEditor != null)
                  {
                    specificationEditor.Visible = false;
                    if (cellForBaseField1.TextBox == null)
                      cellForBaseField1.TextBox = new RtfInSiteEditorWrapper((TextData) cellForBaseField1);
                    Rectangle editorBounds;
                    ref Rectangle local = ref editorBounds;
                    int left = (int) cellForBaseField1.Bounds.Left;
                    int top = (int) cellForBaseField1.Bounds.Top;
                    RectangleF bounds = cellForBaseField1.Bounds;
                    int width1 = (int) bounds.Width;
                    bounds = cellForBaseField1.Bounds;
                    int height = (int) bounds.Height;
                    local = new Rectangle(left, top, width1, height);
                    ParagraphFormat paragraphFormat1 = cellForBaseField1.ParagraphFormat.Clone();
                    paragraphFormat1.IdentLeft = new float?(0.0f);
                    if (cellForBaseField1.Template is TextBoxElement template)
                      paragraphFormat1.IdentLeft = template.ParagraphFormat.IdentLeft;
                    cellForBaseField1.TextBox.SetupEditor(specificationEditor, str1, false, -1, paragraphFormat1, cellForBaseField1.Orientation, cellForBaseField1.CharFormat, cellForBaseField1.BackColor, cellForBaseField1.Bounds, editorBounds, new MarginsF(cellForBaseField1.LeftMargin, cellForBaseField1.RightMargin, cellForBaseField1.TopMargin, cellForBaseField1.BottomMargin), 1f, cellForBaseField1.DefaultRowSize);
                    int pX1;
                    int pY;
                    specificationEditor.TerTextPosToPix(0, 0, num2, out pX1, out pY);
                    int pX2;
                    specificationEditor.TerTextPosToPix(0, 0, 0, out pX2, out int _);
                    int twipsX;
                    specificationEditor.TerScrToTwipsX(pX1 - pX2, out twipsX);
                    float num4 = UnitsConverter.TwipsToMm((float) twipsX);
                    specificationEditor.TerTextPosToPix(0, 0, str1.Length, out pX1, out pY);
                    specificationEditor.TerScrToTwipsX(pX1 - pX2, out twipsX);
                    float num5 = UnitsConverter.TwipsToMm((float) twipsX) - num4;
                    double num6 = (double) num4 + (double) num5;
                    bounds = cellForBaseField1.Bounds;
                    double width2 = (double) bounds.Width;
                    if (num6 > width2)
                    {
                      bounds = cellForBaseField1.Bounds;
                      num4 = bounds.Width - num5;
                    }
                    float num7 = num4 / 10f;
                    ParagraphFormat paragraphFormat2 = cellForBaseField1.ParagraphFormat.Clone();
                    paragraphFormat2.IdentLeft = new float?(num7);
                    cellForBaseField1.SetParagraphFormat(paragraphFormat2, false, false, false);
                    flag5 = true;
                  }
                }
                catch
                {
                  ++num3;
                  tag.avsDocument.SpecificationEditor = (ImRtfEditor) null;
                }
              }
              else
                break;
            }
            AVSRow.SetDocCellText((TextData) cellForBaseField1, editValue);
            cellForBaseField1.SetAttributeValue(AVSRow.CellAttrName_FullDesignation, str1, false, false, false);
            if (cellForBaseField2 != null)
            {
              string text = cellForBaseField2.Text;
              string valueFromDocCell1 = AVSRow.GetFieldValueFromDocCell((TextData) cellForBaseField2);
              string valueFromDocCell2 = row2.GetFieldValueFromDocCell(docRow1, tag.Field_Format);
              if (valueFromDocCell1 == valueFromDocCell2)
              {
                AVSRow.SetDocCellText((TextData) cellForBaseField2, valueFromDocCell1, "");
                cellForBaseField2.SetAttributeValue(AVSRow.CellAttrName_ViewTextForFormat, " ", false, false, false);
              }
              else
              {
                AVSRow.SetDocCellText((TextData) cellForBaseField2, valueFromDocCell1);
                cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_OldViewText, false, false);
                cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_ViewTextForFormat, false, false);
              }
              if (cellForBaseField2.Text != text)
                tag.UpdateNoteDocCellText((TextData) cellForBaseField2, tag.Field_Format, valueFromDocCell1, false, false);
            }
            tag.avsDocument.Unlock_DocCell_TextChanged();
            flag1 = true;
          }
        }
      }
    }
    if (flag1)
      return;
    tag.avsDocument.Lock_DocCell_TextChanged();
    AVSRow.SetDocCellText((TextData) cellForBaseField1, str1);
    cellForBaseField1.RemoveAttribute(AVSRow.CellAttrName_FullDesignation, false, false);
    bool updateLayoutFlag = cellForBaseField1.NeedUpdateLayoutFlag;
    cellForBaseField1.AssignNeedUpdateLayoutFlag(true);
    try
    {
      ParagraphFormat paragraphFormat = cellForBaseField1.ParagraphFormat.Clone();
      paragraphFormat.IdentLeft = new float?(0.0f);
      if (cellForBaseField1.Template is TextBoxElement template)
        paragraphFormat.IdentLeft = template.ParagraphFormat.IdentLeft;
      cellForBaseField1.SetParagraphFormat(paragraphFormat, false, false, false);
    }
    finally
    {
      cellForBaseField1.AssignNeedUpdateLayoutFlag(updateLayoutFlag);
    }
    if (cellForBaseField2 != null)
    {
      string fieldStringValue = tag.GetFieldStringValue(tag.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, false);
      if (!string.IsNullOrEmpty(fieldStringValue))
      {
        cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_EditText, false, false);
        cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
        cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_ViewTextForFormat, false, false);
        tag.UpdateNoteDocCellText((TextData) cellForBaseField2, tag.Field_Format, fieldStringValue, false, false);
      }
    }
    tag.avsDocument.Unlock_DocCell_TextChanged();
  }

  /// <summary>Обновить обозначения различных исполнений изделий в записях идущих подряд</summary>
  /// <param name="node">Узел документа</param>
  /// <param name="designationTrimSchema">Настройки сравнения обозначений</param>
  /// <param name="prevFullFormatElem">Предыдущий элемент с полным форматом</param>
  /// <param name="prevFullRow">Предыдущая полная запись</param>
  public virtual void UpdatePartProductCaptions_OLD(
    DocumentTreeNode node,
    DesignationTrimSchema designationTrimSchema)
  {
    if (this.avsDocument.ReadOnly || !(node is TableData docRow))
      return;
    AVSRow tag = docRow.Tag as AVSRow;
    if (tag.IsNoteRow)
      return;
    TextBoxElement cellForBaseField1 = tag.GetDocumentCellForBaseField(this.Field_Designation, docRow, -1) as TextBoxElement;
    TextBoxElement cellForBaseField2 = tag.GetDocumentCellForBaseField(this.Field_Format, docRow, -1) as TextBoxElement;
    if (cellForBaseField1 == null || tag.HasComplexDesignation((TextData) cellForBaseField1))
      return;
    int index = docRow.Index;
    TableData tableData = (TableData) null;
    AVSRow avsRow = (AVSRow) null;
    if (index != 0)
    {
      tableData = docRow.Parent.Nodes[index - 1] as TableData;
      avsRow = tableData.Tag as AVSRow;
    }
    bool flag1 = false;
    bool flag2 = (node as TableData).OwnerSubTable.Name == "Документация";
    if (designationTrimSchema != null)
    {
      bool flag3 = designationTrimSchema.UseSameProductDesignationsInRows;
      if (flag2 && !designationTrimSchema.UseInDocumentation)
        flag3 = false;
      if (flag3 && avsRow != null && tableData != null && docRow.Page != null && tableData.Page != null)
      {
        string designation1 = tag.Designation;
        int num1;
        if (tag.ArticleGroupID.HasValue)
        {
          Guid? articleGroupId1 = avsRow.ArticleGroupID;
          if (articleGroupId1.HasValue)
          {
            articleGroupId1 = tag.ArticleGroupID;
            Guid? articleGroupId2 = avsRow.ArticleGroupID;
            num1 = articleGroupId1.HasValue == articleGroupId2.HasValue ? (articleGroupId1.HasValue ? (articleGroupId1.GetValueOrDefault() == articleGroupId2.GetValueOrDefault() ? 1 : 0) : 1) : 0;
            goto label_16;
          }
        }
        num1 = 0;
label_16:
        bool flag4 = num1 != 0;
        if (designation1 != null && designation1.Length > designationTrimSchema.LengthBasePart | flag4)
        {
          int startIndex = designation1.Length - 1;
          int num2 = designationTrimSchema.LengthBasePart - 1;
          if (flag4)
            num2 = 0;
          int count = startIndex - num2;
          int num3 = designation1.LastIndexOf("-", startIndex, count);
          bool flag5 = flag4;
          if (!flag5 && num3 != -1 && num3 < designation1.Length - 1 && char.IsDigit(designation1[num3 + 1]))
          {
            string designation2 = avsRow.Designation;
            string suffiks1 = "";
            string suffiks2 = "";
            string basePart = this.avsDocument.GetBasePart(designation1, designationTrimSchema, out suffiks1, DocumentTypeSettingsHelper.GetSettings(tag.ObjType));
            string str = designation2;
            if (str != basePart)
              str = this.avsDocument.GetBasePart(designation2, designationTrimSchema, out suffiks2, DocumentTypeSettingsHelper.GetSettings(avsRow.ObjType));
            if (basePart == str && suffiks1 == suffiks2)
              flag5 = true;
          }
          if (num3 != -1 & flag5)
          {
            string str1 = designation1;
            string str2 = designation1.Substring(num3);
            this.avsDocument.Lock_DocCell_TextChanged();
            bool flag6 = false;
            int num4 = 0;
            while (!flag6)
            {
              if (num4 < 2)
              {
                try
                {
                  ImRtfEditor specificationEditor = this.avsDocument.SpecificationEditor;
                  if (specificationEditor != null)
                  {
                    specificationEditor.Visible = false;
                    if (cellForBaseField1.TextBox == null)
                      cellForBaseField1.TextBox = new RtfInSiteEditorWrapper((TextData) cellForBaseField1);
                    Rectangle editorBounds;
                    ref Rectangle local = ref editorBounds;
                    RectangleF bounds = cellForBaseField1.Bounds;
                    int left = (int) bounds.Left;
                    bounds = cellForBaseField1.Bounds;
                    int top = (int) bounds.Top;
                    bounds = cellForBaseField1.Bounds;
                    int width1 = (int) bounds.Width;
                    bounds = cellForBaseField1.Bounds;
                    int height = (int) bounds.Height;
                    local = new Rectangle(left, top, width1, height);
                    ParagraphFormat paragraphFormat1 = cellForBaseField1.ParagraphFormat.Clone();
                    paragraphFormat1.IdentLeft = new float?(0.0f);
                    if (cellForBaseField1.Template is TextBoxElement)
                      paragraphFormat1.IdentLeft = (cellForBaseField1.Template as TextBoxElement).ParagraphFormat.IdentLeft;
                    cellForBaseField1.TextBox.SetupEditor(specificationEditor, str1, false, -1, paragraphFormat1, cellForBaseField1.Orientation, cellForBaseField1.CharFormat, cellForBaseField1.BackColor, cellForBaseField1.Bounds, editorBounds, new MarginsF(cellForBaseField1.LeftMargin, cellForBaseField1.RightMargin, cellForBaseField1.TopMargin, cellForBaseField1.BottomMargin), 1f, cellForBaseField1.DefaultRowSize);
                    int pX;
                    int pY;
                    specificationEditor.TerTextPosToPix(0, 0, num3, out pX, out pY);
                    int twipsX;
                    specificationEditor.TerScrToTwipsX(pX, out twipsX);
                    float num5 = UnitsConverter.TwipsToMm((float) twipsX);
                    specificationEditor.TerTextPosToPix(0, 0, str1.Length + str2.Length, out pX, out pY);
                    specificationEditor.TerScrToTwipsX(pX, out twipsX);
                    float num6 = UnitsConverter.TwipsToMm((float) twipsX) - num5;
                    double num7 = (double) num5 + (double) num6;
                    bounds = cellForBaseField1.Bounds;
                    double width2 = (double) bounds.Width;
                    if (num7 > width2)
                    {
                      bounds = cellForBaseField1.Bounds;
                      num5 = bounds.Width - num6;
                    }
                    float num8 = num5 / 10f;
                    ParagraphFormat paragraphFormat2 = cellForBaseField1.ParagraphFormat.Clone();
                    paragraphFormat2.IdentLeft = new float?(num8);
                    cellForBaseField1.SetParagraphFormat(paragraphFormat2, false, false, false);
                    flag6 = true;
                  }
                }
                catch
                {
                  ++num4;
                  this.avsDocument.SpecificationEditor = (ImRtfEditor) null;
                }
              }
              else
                break;
            }
            cellForBaseField1.AssignText(str2, false, true, false, false, false);
            cellForBaseField1.SetAttributeValue("FullDesignation", str1, false, false, false);
            if (cellForBaseField2 != null)
            {
              string fieldStringValue1 = tag.GetFieldStringValue(this.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, false);
              string fieldStringValue2 = avsRow.GetFieldStringValue(this.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, false);
              string attributeValue = fieldStringValue1;
              if (fieldStringValue1 == fieldStringValue2)
              {
                if (!cellForBaseField2.InPlaceEditorActive)
                  cellForBaseField2.AssignText("", false, true, false, false, false);
                cellForBaseField2.SetAttributeValue(AVSRow.CellAttrName_EditText, attributeValue, false, false, false);
                cellForBaseField2.SetAttributeValue(AVSRow.CellAttrName_ViewText, " ", false, false, false);
                cellForBaseField2.SetAttributeValue(AVSRow.CellAttrName_ViewTextForFormat, " ", false, false, false);
              }
              else
              {
                cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_EditText, false, false);
                cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
                cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_OldViewText, false, false);
                cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_ViewTextForFormat, false, false);
                cellForBaseField2.AssignText(fieldStringValue1, false, true, false, false, false);
              }
              tag.UpdateNoteDocCellText((TextData) cellForBaseField2, this.Field_Format, fieldStringValue1, false, false);
            }
            this.avsDocument.Unlock_DocCell_TextChanged();
            flag1 = true;
          }
        }
      }
    }
    if (flag1)
      return;
    string designation = tag.Designation;
    if (designation == null)
      return;
    this.avsDocument.Lock_DocCell_TextChanged();
    cellForBaseField1.AssignText(designation, false, true, false, false, false);
    cellForBaseField1.RemoveAttribute("FullDesignation", false, false);
    bool updateLayoutFlag = cellForBaseField1.NeedUpdateLayoutFlag;
    cellForBaseField1.AssignNeedUpdateLayoutFlag(true);
    try
    {
      ParagraphFormat paragraphFormat = cellForBaseField1.ParagraphFormat.Clone();
      paragraphFormat.IdentLeft = new float?(0.0f);
      if (cellForBaseField1.Template is TextBoxElement)
        paragraphFormat.IdentLeft = (cellForBaseField1.Template as TextBoxElement).ParagraphFormat.IdentLeft;
      cellForBaseField1.SetParagraphFormat(paragraphFormat, false, false, false);
    }
    finally
    {
      cellForBaseField1.AssignNeedUpdateLayoutFlag(updateLayoutFlag);
    }
    string fieldStringValue = tag.GetFieldStringValue(this.Field_Format, -1, -1, (List<RelationAttributeValuesCache>) null, false);
    if (cellForBaseField2 != null && !string.IsNullOrEmpty(fieldStringValue))
    {
      cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_EditText, false, false);
      cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
      cellForBaseField2.RemoveAttribute(AVSRow.CellAttrName_ViewTextForFormat, false, false);
      tag.UpdateNoteDocCellText((TextData) cellForBaseField2, this.Field_Format, fieldStringValue, false, false);
    }
    this.avsDocument.Unlock_DocCell_TextChanged();
  }

  /// <summary>Обработчик события перед разбивкой строки в документе</summary>
  /// <param name="sender"></param>
  private void docRow_BeforeDistribute(object sender)
  {
    if (!this.avsDocument.IsSpecification || !(sender is TableData docRow))
      return;
    this.UpdatePositionsCaptions(docRow);
    this.UpdatePartProductCaptions(docRow, this.avsDocument.DesignationTrimSchema);
  }

  /// <summary>Есть ли узлы в этой записи</summary>
  internal bool HasDocNodes => this.docNodes != null && this.docNodes.Count > 0;

  /// <summary>Узлы документа представляющие эту строку</summary>
  [Browsable(false)]
  public List<TableData> DocNodes
  {
    [DebuggerStepThrough] get => this.docNodes;
    set
    {
      if (this.docNodes == value)
        return;
      if (this.docNodes != null)
      {
        if (this.docNode != null)
          this.docNode.SetOnOnePageWith((RectangleElement) null, false, false);
        this.SetCommonPositionToDocNodes((string) null);
        this.docNode = (TableData) null;
        for (int index = 0; index < this.docNodes.Count; ++index)
          this.DisconnectDocNodeWithRow(this.docNodes[index]);
      }
      else
        this.DocNode = (TableData) null;
      this.docNodes = value;
      if (this.docNodes == null)
        this.docNodes = new List<TableData>();
      this.docNode = (TableData) null;
      if (!this.HasDocNodes)
        return;
      this.SetCommonPositionToDocNodes(this.commonPositionDocument);
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        TableData docNode = this.docNodes[index];
        this.ConnectDocNodeWithRow(docNode, index, false);
        if (this.IsFreeSortIndex && this.HasRelation)
          docNode.RemoveAttribute(AVSRow.RowAttr_SortIndex, false, false);
      }
      if (this.docNodes.Count <= 0)
        return;
      this.docNode = this.docNodes[0];
      if (this.docNode == null)
        return;
      this.docNode.SetOnOnePageWith((RectangleElement) this.docNodeExp, false, false);
    }
  }

  internal bool HasComplexDesignation(TextData node)
  {
    if (node != null)
    {
      bool flag = false;
      string attributeValue = node.GetAttributeValue(DocumentTreeNode.AttributeName_ComplexDesignation, false);
      bool result;
      if (!string.IsNullOrEmpty(attributeValue) && bool.TryParse(attributeValue, out result))
        flag = result;
      if (flag)
        return node.Text.Contains("|");
    }
    return false;
  }

  /// <summary>Есть ли узел экспортной части СП в этой записи</summary>
  internal bool HasDocNodeExp => this.docNodeExp != null;

  /// <summary>Узел экспортного документа представляющие эту строку</summary>
  [Browsable(false)]
  public TableData DocNodeExp
  {
    [DebuggerStepThrough] get => this.docNodeExp;
    set
    {
      if (this.docNodeExp == value)
        return;
      if (this.docNodeExp != null)
      {
        this.DisconnectDocNodeWithRow(this.docNodeExp);
        this.docNodeExp.SetOnOnePageWith((RectangleElement) null, false, false);
      }
      this.docNodeExp = value;
      if (this.docNodeExp == null)
        return;
      this.ConnectDocNodeWithRow(this.docNodeExp, 0, true);
      this.docNodeExp.SetOnOnePageWith((RectangleElement) this.DocNode, false, false);
      if (!this.IsFreeSortIndex || !this.HasRelation)
        return;
      this.docNodeExp.RemoveAttribute(AVSRow.RowAttr_SortIndex, false, false);
    }
  }

  /// <summary>Обработчик изменения атрибутов строки документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DocNode_AttributeValueChanged(object sender, AttributeValueChanged_EventArgs e)
  {
    if (sender != this.DocNode || !(e.AttributeName == AVSRow.DocAttr_Smotri) && !(e.AttributeName == AVSRow.DocAttr_ZagotovkaDlya))
      return;
    this.UpdateNameDocCellText(e.UpdateUI, e.UpdateLayout);
  }

  /// <summary>Получить виртуальные атрибуты для строки документа</summary>
  /// <param name="sender">Строка документа</param>
  /// <param name="attributeNames">Список атрибутов</param>
  /// <param name="forSaveOnly">Атрибуты только для хранения</param>
  private void DocRow_GetPluginVirtualAttributeNames(
    object sender,
    StringCollection attributeNames,
    bool forSaveOnly)
  {
    if (this.sortBeforeRow != null)
    {
      attributeNames.Add(AVSRow.RowAttr_SortBeforeRowByID);
    }
    else
    {
      if (this.sortAfterRow == null)
        return;
      attributeNames.Add(AVSRow.RowAttr_SortAfterRowByID);
    }
  }

  /// <summary>Вернуть значение виртуального атрибута для строки документа</summary>
  /// <param name="sender">Строка документа</param>
  /// <param name="attributeName">Имя атрибута</param>
  /// <param name="notNull">Вернуть пустую строку вместо значения null</param>
  /// <returns></returns>
  private GetVirtualAttributeResult DocRow_GetPluginVirtualAttributeValue(
    object sender,
    string attributeName,
    bool notNull)
  {
    GetVirtualAttributeResult virtualAttributeValue = new GetVirtualAttributeResult(false, "");
    if (this.sortBeforeRow != null)
    {
      if (attributeName == AVSRow.RowAttr_SortBeforeRowBySortIndex)
        virtualAttributeValue = new GetVirtualAttributeResult(true, this.sortBeforeRow.SortIndex.ToString());
      else if (attributeName == AVSRow.RowAttr_SortBeforeRowByID)
        virtualAttributeValue = new GetVirtualAttributeResult(true, this.sortBeforeRow.DocNode?.Id ?? "");
    }
    else if (this.sortAfterRow != null)
    {
      if (attributeName == AVSRow.RowAttr_SortAfterRowBySortIndex)
        virtualAttributeValue = new GetVirtualAttributeResult(true, this.sortAfterRow.SortIndex.ToString());
      else if (attributeName == AVSRow.RowAttr_SortAfterRowByID)
        virtualAttributeValue = new GetVirtualAttributeResult(true, this.sortAfterRow.DocNode?.Id ?? "");
    }
    if (notNull && virtualAttributeValue.Found && virtualAttributeValue.Value == null)
      virtualAttributeValue.Value = "";
    return virtualAttributeValue;
  }

  private void cell_InplaceEditorActivated(object sender, EventArgs e)
  {
    if (!(sender is TextData cell) || cell.Parent == null)
      return;
    AvsRowAttributeInfo attributeInfoForCell = this.GetAttributeInfoForCell(cell);
    if (this.SectionID != AVSDocument.ObjID_SectionMaterials || !AVSRow.IsCountField(attributeInfoForCell) || !(cell is TextBoxElement textBoxElement) || textBoxElement.TextBox == null)
      return;
    textBoxElement.TextBox.NeedValidate = true;
  }

  /// <summary>Список дополнительных атрибутов</summary>
  [RefreshProperties(RefreshProperties.All)]
  [DefaultValue(null)]
  [Description("Дополнительные атрибуты записи")]
  [DisplayName("Атрибуты записи")]
  [Category("Данные")]
  [Browsable(false)]
  public AdditionalAttributeCollection AdditionalDocRowAttributes
  {
    get
    {
      return this.DocNode != null ? this.DocNode.GetAdditionalAttributes() : (AdditionalAttributeCollection) null;
    }
    set
    {
      if (this.DocNode == null)
        return;
      this.DocNode.SetAdditionalAttributes(value);
    }
  }

  /// <summary>Для внутреннего использования. Добавить в список узлов документа ещё один узел</summary>
  /// <param name="newDocNode">Новый узел документа</param>
  /// <param name="isExportTable">Узел экспортного документа</param>
  /// <returns>Индекс добавленного узла</returns>
  internal int AddDocNode(TableData newDocNode, bool isExportTable = false)
  {
    if (newDocNode == null)
      return -1;
    if (isExportTable)
    {
      this.DocNodeExp = newDocNode;
      return 0;
    }
    if (this.docNodes == null)
    {
      this.docNodes = new List<TableData>();
    }
    else
    {
      int num = this.docNodes.IndexOf(newDocNode);
      if (num != -1)
        return num;
    }
    this.docNodes.Add(newDocNode);
    int rowIndex = this.docNodes.Count - 1;
    this.docNode = this.docNodes[0];
    newDocNode?.SetOnOnePageWith((RectangleElement) this.docNodeExp, false, false);
    this.ConnectDocNodeWithRow(newDocNode, rowIndex, false);
    return rowIndex;
  }

  /// <summary>Структура таблицы формы Б</summary>
  [Browsable(false)]
  public bool IsFormB
  {
    [DebuggerStepThrough] get
    {
      if (this.section != null)
        return this.section.IsFormB;
      return this.avsDocument != null && this.avsDocument.IsFormB;
    }
  }

  /// <summary>Экспортная спецификация</summary>
  [Browsable(false)]
  public bool IsExportSP
  {
    [DebuggerStepThrough] get => this.section != null && this.section.IsExportSP;
  }

  /// <summary>Запись находится в переменных данных формы А или В</summary>
  [Browsable(false)]
  public bool InVariableData_AV
  {
    get
    {
      if (this.section != null && this.section.ProductChapter != null)
      {
        if (this.section.ProductChapter.IsVariableDataChapter)
          return true;
        if (this.section.ProductChapter.Parent != null)
          return this.section.ProductChapter.Parent.IsVariableDataChapter;
      }
      return false;
    }
  }

  /// <summary>Запись находится в общих данных формы А или В</summary>
  [Browsable(false)]
  public bool InCommonData_AV
  {
    get
    {
      if (this.section == null || this.avsDocument.AvsDocumentForm != AVSDocumentForm.A && this.avsDocument.AvsDocumentForm != AVSDocumentForm.V)
        return false;
      Chapter productChapter = this.section.ProductChapter;
      return productChapter != null ? productChapter.IsCommonDataChapter : this.section.IsCommonDataChapter;
    }
  }

  /// <summary>Получить индекс исполнения в productsInfo для ячейки количества в форме Б</summary>
  /// <param name="cell">Ячейка документа</param>
  /// <returns></returns>
  public int GetProductIndexForCountCell(TextData cell)
  {
    int productIndex = -1;
    this.GetCellBaseFieldInfo(cell, out productIndex);
    return productIndex;
  }

  /// <summary>Получить индекс исполнения в productsInfo для связи</summary>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="relationList">Коллекция связей</param>
  /// <returns></returns>
  public int GetProductIndexForRelation(
    int relationIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    return this.avsDocument != null && relationIndex != -1 && !relationList.IsNullOrEmpty<RelationAttributeValuesCache>() ? this.avsDocument.GetProductIndex(relationList[relationIndex].ProjectId) : -1;
  }

  /// <summary>Получить первый индекс исполнения в строке документа</summary>
  /// <param name="node">Узел документа</param>
  /// <returns></returns>
  public int GetFirstProductIndexForDocRow(DocumentTreeNode node)
  {
    int result = 0;
    DocumentTreeNode documentTreeNode = AVSDocument.FindParentSpecRowDocNode(node) ?? AVSDocument.FindParentNoteRowDocNode(node);
    if (documentTreeNode != null)
    {
      string attributeValue = documentTreeNode.GetAttributeValue(AVSRow.DocAttr_ProductIndex, true);
      if (attributeValue == "" || !int.TryParse(attributeValue, out result))
        result = 0;
    }
    return result;
  }

  /// <summary>Проверяем текст количества на наличие единицы измерения</summary>
  /// <param name="text">Текст</param>
  /// <returns></returns>
  public MeasuredValue ValidateMaterialCount(string text, double? defaultValue, int relationIndex)
  {
    MeasuredValue measuredValue1 = (MeasuredValue) null;
    bool flag = false;
    double result = double.MinValue;
    MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
    string strValue = text == null ? "" : text;
    MeasuredValue fieldValue1 = this.GetFieldValue(this.Field_Count, relationIndex, -1, (List<RelationAttributeValuesCache>) null, true, false, true) as MeasuredValue;
    MeasureDescriptor defaultMeasure = (MeasureDescriptor) null;
    if (fieldValue1 != null)
    {
      defaultMeasure = MeasureHelper.FindDescriptor(fieldValue1.MeasureID);
      if (fieldValue1.MeasureID != AVSRow.DefaultCountID)
        defaultMeasure = (MeasureDescriptor) null;
    }
    if (strValue != null)
    {
      flag = MeasureHelper.ConvertToMeasuredValue(AVSRow.ConvertCountToStringForMeasuredValue(strValue), defaultMeasure, out result, out measureDescriptor, false);
      measuredValue1 = AVSRow.ConvertCountToMeasuredValue((object) strValue, false);
      int num = !flag ? 0 : (measureDescriptor != null ? 1 : 0);
    }
    if (!flag && defaultValue.HasValue)
    {
      result = defaultValue.Value;
      flag = true;
    }
    if (((!this.IsFormB ? 0 : (measureDescriptor == null ? 1 : 0)) & (flag ? 1 : 0)) != 0 && this.Relations != null)
    {
      if (relationIndex == -1)
        relationIndex = 0;
      for (int relationIndex1 = 0; relationIndex1 < this.Relations.Count; ++relationIndex1)
      {
        if (this.GetFieldValue(this.Field_Count, relationIndex1, -1, (List<RelationAttributeValuesCache>) null, false, false, true) is MeasuredValue fieldValue2 && fieldValue2.MeasureID != 0L)
          return new MeasuredValue(result, fieldValue2.MeasureID);
      }
    }
    if (!(measureDescriptor == null & flag))
      return (MeasuredValue) null;
    string s = result.ToString();
    long measureID = -1;
    AVSMeasureForm avsMeasureForm = new AVSMeasureForm();
    avsMeasureForm.ShowAllCheckBox = this.IsFormB && this.avsDocument.productsInfo != null && this.avsDocument.productsInfo.Count > 1;
    ArrayList listByAttributeId = MeasureEditor.GetMeasureDescriptorListByAttributeId(AvsIDCache.Attr_Count);
    MeasureDescriptor[] aMeasureDescriptorList = listByAttributeId == null ? MeasureHelper.Instance.Measures : (MeasureDescriptor[]) listByAttributeId.ToArray(typeof (MeasureDescriptor));
    if (avsMeasureForm.ExecuteDialog(ref s, ref measureID, aMeasureDescriptorList, (GetDefaultMeasureIDDelegate) null) != DialogResult.OK)
      throw new Exception("Не задана единица измерения");
    MeasuredValue measuredValue2 = !double.TryParse(s, out result) || measureID == -1L ? (MeasuredValue) null : new MeasuredValue(result, measureID);
    if (avsMeasureForm.AllProducts && measuredValue2 != null)
    {
      for (int productIndex = 0; productIndex < this.avsDocument.productsInfo.Count; ++productIndex)
        this.SetCount(productIndex, (object) measuredValue2, false);
      this.avsDocument.UpdateViewNodes(false, false, true, false, false, EmptyRowUpdateMode.DontChange);
    }
    return measuredValue2;
  }

  /// <summary>Получить информацию об атрибуте для заданной ячейки строки документа</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  [Obsolete("Необходимо проверить все вызовы и перевести на GetCellBaseField")]
  public AvsRowAttributeInfo GetAttributeInfoForCell(TextData cell)
  {
    return this.GetAttributeInfoForCell(cell, out int _);
  }

  /// <summary>Получить информацию об атрибуте для заданной ячейки строки документа</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  [Obsolete("Необходимо проверить все вызовы и перевести на GetCellBaseField")]
  public AvsRowAttributeInfo GetAttributeInfoForCell(TextData cell, out int productIndex)
  {
    AvsRowAttributeInfo attribute = (AvsRowAttributeInfo) null;
    productIndex = -1;
    TableData parentSpecRowDocNode = AVSDocument.FindParentSpecRowDocNode((DocumentTreeNode) cell) as TableData;
    if (cell == null || parentSpecRowDocNode == null)
      return (AvsRowAttributeInfo) null;
    if (this.IsFormB)
      productIndex = this.GetFirstProductIndexForDocRow((DocumentTreeNode) parentSpecRowDocNode);
    List<AvsRowAttributeInfo> rowAttributeInfoList = this.DocRowFields;
    if (this.avsDocument.IsExportSP && this.docNodeExp == parentSpecRowDocNode)
      rowAttributeInfoList = this.DocRowFields_Exp;
    if (rowAttributeInfoList == null)
      return (AvsRowAttributeInfo) null;
    int index1 = -1;
    foreach (TextData textData1 in (IEnumerable<TextData>) parentSpecRowDocNode.TextCellsEnumerator)
    {
      ++index1;
      TextData textData2 = cell;
      if (textData1 == textData2)
        break;
    }
    if (this.IsNoteRow)
    {
      int num = -1;
      if (cell.Name == Chapter.NoteRowTextCellName)
      {
        attribute = this.Field_Name;
        if (attribute != null)
          return attribute;
      }
      else
      {
        num = 0;
        for (int index2 = 0; index2 < parentSpecRowDocNode.Nodes.Count && index2 < index1; ++index2)
        {
          if (parentSpecRowDocNode.Nodes[index2] is RectangleElement node)
          {
            if (node.GridPos == null)
              ++num;
            else if (node.GridPos.SpanCount > 0)
              num += node.GridPos.SpanCount;
          }
        }
      }
      if (num != -1)
        index1 = num;
    }
    if (index1 >= 0 && index1 < rowAttributeInfoList.Count)
    {
      attribute = rowAttributeInfoList[index1];
      if (this.IsFormB && AVSRow.IsCountField(attribute))
      {
        int num = 0;
        for (int index3 = index1 - 1; index3 >= 0; --index3)
        {
          if (AVSRow.IsCountField(rowAttributeInfoList[index3]))
            ++num;
        }
        productIndex = this.GetFirstProductIndexForDocRow((DocumentTreeNode) parentSpecRowDocNode) + num;
      }
    }
    return attribute;
  }

  /// <summary>Обработчик изменения текста в ячейке документа</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Параметры события</param>
  public void cell_TextValidating(object sender, TextValidating_EventArgs e)
  {
    if (this.avsDocument == null || this.avsDocument.DocCell_TextChanged_IsLocked || !this.avsDocument.ValidateValue || !(sender is TextData cell1) || cell1.Parent == null)
      return;
    string str = e.Text;
    AvsRowAttributeInfo cellBaseFieldInfo = this.GetCellBaseFieldInfo(cell1, out int _);
    if (cellBaseFieldInfo == null)
    {
      this.avsDocument.ValidateValue = true;
    }
    else
    {
      bool flag = AVSRow.IsCountField(cellBaseFieldInfo);
      if (flag && this.SectionID != AVSDocument.ObjID_SectionMaterials)
      {
        if (string.IsNullOrEmpty(str) || str[str.Length - 1] == ')')
          return;
        AVSRow.ConvertCountToMeasuredValue((object) str);
      }
      try
      {
        if (flag)
        {
          if (this.SectionID == AVSDocument.ObjID_SectionMaterials)
          {
            this.avsDocument.ValidateValue = false;
            if (cell1 is TextBoxElement cell2)
            {
              if (cell2.InPlaceEditorActive)
              {
                string text = e.Text;
                if (!string.IsNullOrEmpty(text))
                {
                  if (!(text != cell2.Text))
                  {
                    RtfInSiteEditorWrapper textBox = cell2.TextBox;
                    if ((textBox != null ? (textBox.NeedValidate ? 1 : 0) : 0) == 0)
                      goto label_24;
                  }
                  int relationIndex = 0;
                  int indexForCountCell = this.GetProductIndexForCountCell((TextData) cell2);
                  if (indexForCountCell != -1 && this.avsDocument.productsInfo != null && this.avsDocument.productsInfo.Count > indexForCountCell && this.avsDocument.productsInfo[indexForCountCell] != null)
                    relationIndex = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[indexForCountCell].Id, this.relations);
                  str = this.GetTextForDocCell(this.GetCellAttributeMapping(cell1), this.Field_Count, relationIndex, indexForCountCell, true, false);
                  MeasuredValue measuredValue = this.ValidateMaterialCount(text, new double?(1.0), relationIndex);
                  if (measuredValue != null)
                  {
                    if (cell2.TextBox != null)
                      cell2.TextBox.NeedValidate = false;
                    this.avsDocument.ValidateValue = true;
                    e.Text = measuredValue.ToString();
                    str = e.Text;
                  }
                }
              }
            }
          }
        }
      }
      finally
      {
        this.avsDocument.ValidateValue = true;
      }
label_24:
      if (cellBaseFieldInfo.Equals((AttributeInfo) this.Field_Note))
        return;
      e.Cancel = !this.ValidateFieldValue(cellBaseFieldInfo, -1, (object) str);
    }
  }

  /// <summary>Обработчик проверки ReadOnly для текста в ячейке документа</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Параметры события</param>
  public void cell_TextReadOnly(object sender, TextReadOnly_EventArgs e)
  {
    if (!(sender is TextData cell))
      return;
    if (this.IsNoteRow)
    {
      e.ReadOnly = cell.ReadOnly;
    }
    else
    {
      AvsRowAttributeInfo attributeInfoForCell = this.GetAttributeInfoForCell(cell);
      if (attributeInfoForCell == null)
        return;
      if (attributeInfoForCell.AttributeId == AvsIDCache.Attr_Format && this.avsDocument.IsSpecification)
      {
        if (!this.IsDocRelation && !MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing))
          return;
        e.ReadOnly = true;
      }
      else
      {
        if (attributeInfoForCell.ReadOnly && !AVSRow.IsCountField(attributeInfoForCell))
        {
          if (this.avsDocument.IsSpecification && this.Field_Name.Equals((AttributeInfo) attributeInfoForCell))
          {
            e.ReadOnly = !AvsConfig.General.AllowNoteForSpecRowName || this.IsDocRelation || this.HasDynamicGroupHeader;
          }
          else
          {
            e.ReadOnly = true;
            return;
          }
        }
        if (!this.avsDocument.IsSpecification && (AVSRow.IsCountField(attributeInfoForCell) || attributeInfoForCell.IsRelationAttribute) && this.HasRelation)
        {
          if (this.relations[0].ProjectId <= 0L)
            return;
          e.ReadOnly = true;
        }
        else
        {
          if (this.Field_Name.Equals((AttributeInfo) attributeInfoForCell) || this.Field_Designation.Equals((AttributeInfo) attributeInfoForCell))
            return;
          IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(this.RelType, attributeInfoForCell.AttributeId);
          if (attribute4RelationType != null && attribute4RelationType.Options.HasFlag((Enum) AttributeOptions.DisableManualEdit))
          {
            e.ReadOnly = true;
          }
          else
          {
            if (cell.ReadOnly || this.ObjectId <= 0L || !this.objectModifyMode.HasValue)
              return;
            e.ReadOnly = this.objectModifyMode.Value != 0;
          }
        }
      }
    }
  }

  /// <summary>Установить единицу измерения не меняя количество</summary>
  /// <param name="productIndex">Индекс исполнения -1 все исполнения</param>
  /// <param name="value">Значение</param>
  /// <param name="updateNodes">Обновить ячейки</param>
  internal void SetCountMeasure(int productIndex, object value, bool updateNodes)
  {
    foreach (RelationAttributeValuesCache allRelation in this.AllRelations)
    {
      long num = -1;
      if (productIndex != -1)
        num = this.avsDocument.productsInfo[productIndex].Id;
      if ((allRelation.ProjectId == num || num == -1L) && allRelation.GetValue(this.Field_Count, false, true) is MeasuredValue measuredValue)
      {
        MeasuredValue measuredValue1 = MeasureHelper.ConvertToMeasuredValue(measuredValue.Caption, false);
        MeasuredValue measuredValue2 = (MeasuredValue) null;
        if (value is MeasuredValue)
          measuredValue2 = value as MeasuredValue;
        if (value is string)
          measuredValue2 = MeasureHelper.ConvertToMeasuredValue((string) value);
        MeasuredValue measuredValue3 = MeasureHelper.ConvertToMeasuredValue(measuredValue1, measuredValue2.MeasureID);
        RelationPositionInAvsRow positionInAvsRow = new RelationPositionInAvsRow(this, allRelation);
        this.SetFieldValue(this.Field_Count, positionInAvsRow.RelationIndex, -1, positionInAvsRow.RelationList, (object) measuredValue3, true, false, false, false, false, false);
      }
    }
    if (!updateNodes)
      return;
    this.avsDocument.UpdateViewNodes(false, false, true, false, false, EmptyRowUpdateMode.DontChange);
  }

  /// <summary>Установить количество в ячейке для всех исполнений</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateDocNode">Обновить ячейку</param>
  internal void SetCountToAllProducts(object value, bool updateDocNode)
  {
    for (int productIndex = 0; productIndex < this.avsDocument.productsInfo.Count; ++productIndex)
      this.SetCount(productIndex, value, false);
    if (!updateDocNode)
      return;
    this.avsDocument.UpdateViewNodes(false, false, true, false, false, EmptyRowUpdateMode.DontChange);
  }

  /// <summary>Установить количество в ячейке</summary>
  /// <param name="productIndex">Индекс исполнения</param>
  /// <param name="value">Значение</param>
  /// <param name="updateDocNode">Обновить ячейку</param>
  public void SetCount(int productIndex, object value, bool updateDocNode)
  {
    if (this.avsDocument == null)
      return;
    long num1 = -1;
    long projID = -1;
    int num2 = -1;
    long num3 = -1;
    object obj = value;
    bool flag1 = false;
    if (productIndex == -1 && !this.IsFormB && this.Product != null && this.Product.Id != -1L)
      productIndex = this.avsDocument.GetProductIndex(this.Product.Id);
    int num4;
    switch (obj)
    {
      case null:
      case DBNull _:
        num4 = 1;
        break;
      default:
        num4 = obj as string == "" ? 1 : 0;
        break;
    }
    bool flag2 = num4 != 0;
    for (int productIndex1 = 0; productIndex1 < this.avsDocument.productsInfo.Count && (productIndex == -1 || productIndex1 == 0); ++productIndex1)
    {
      int num5 = -1;
      ProductInfo projInfo = (ProductInfo) null;
      if (this.avsDocument.ParentProducts.Count > 0)
      {
        if (productIndex1 <= 0 || productIndex != -1)
        {
          if (this.HasRelation)
            projInfo = this.avsDocument.GetParentProductInfoByObjectID(this.relations[0].ProjectId);
        }
        else
          break;
      }
      else
      {
        if (productIndex != -1)
          productIndex1 = productIndex;
        projInfo = this.avsDocument.GetProductInfoByIndex(productIndex1);
      }
      if (projInfo != null)
        num5 = this.GetRelationIndexForProduct(projInfo.Id, this.relations);
      if (((num5 == -1 ? 0 : (this.IsFormB ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          num3 = this.Relations[num5].RelationId;
          sessionKeeper.Session.GetRelationByPartObjectID(this.Relations[num5].RelationId, this.ObjectId, true).Delete(0L);
        }
        this.SetFieldValue(this.Field_Count, num5, productIndex, this.relations, (object) "", false, false, updateDocNode, this.avsDocument.IsGridViewMode, false, false);
        this.RemoveRelationData(this.relations, num5);
        if (projInfo != null && this.HasHiddenRelation)
        {
          for (int index = this.hiddenRelations.Count - 1; index >= 0; --index)
          {
            if (this.hiddenRelations[index].ProjectId == projInfo.Id)
              this.RemoveRelationData(this.hiddenRelations, index);
          }
        }
      }
      else
      {
        if (!flag2 && num5 == -1 && this.ObjectId != -1L && projInfo != null)
        {
          long attrValue = -1;
          Guid guid;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (this.relations == null)
              this.relations = new List<RelationAttributeValuesCache>(1);
            IDBRelation dbRelation;
            if (this.relations.Count > 0)
            {
              IDBRelation relationByPartObjectId = sessionKeeper.Session.GetRelationByPartObjectID(this.Relations[0].RelationId, this.ObjectId, true);
              NewRelationProperties relationProperties = new NewRelationProperties(relationByPartObjectId.RelationID, projInfo.Id, relationByPartObjectId.PartID, DateTime.MinValue, DateTime.MaxValue, relationByPartObjectId.PartObjectID);
              if (relationByPartObjectId.RelationType == AvsIDCache.Relation_Document)
              {
                IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document);
                dbRelation = sessionKeeper.Session.GetRelation(projInfo.Id, this.ObjectId, this.RelType, true) ?? (this.avsDocument.productsInfo.Count <= 1 ? relationCollection.Create(relationProperties) : AVSDocument.CreateDocRelationWithLockPDMHandler(relationCollection, relationProperties));
              }
              else
              {
                dbRelation = sessionKeeper.Session.GetRelationCollection(this.RelType).Create(relationProperties);
                IDBAttribute attributeById = dbRelation.GetAttributeByID(Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID);
                if (attributeById != null)
                {
                  bool flag3 = false;
                  if (this.NewCellMappingMode && this.NoteCellMapping != null)
                    flag3 = this.NoteCellMapping.ContainsAttribute((AttributeInfo) new AvsRowAttributeInfo(true, Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID));
                  else if (this.avsDocument?.noteFieldSettings != null)
                    flag3 = this.avsDocument.noteFieldSettings.FindAttribute(true, Intermech.Interfaces.PdmConfigurator.Consts.attributeObjectApplicabilityCondID) != null;
                  if (!flag3)
                    attributeById.Delete(0L);
                }
                if (this.IsFormB && this.Relations[0].GetValue(AvsIDCache.Attr_DopZamenGroupNum, false, true) != null)
                {
                  dbRelation.TryToAddOrDelAttribute(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, (object) null);
                  dbRelation.TryToAddOrDelAttribute(SubstitutesConstants.SubstituteGroupNameAttributeTypeID, (object) null);
                  dbRelation.TryToAddOrDelAttribute(SubstitutesConstants.SubstituteNumberAttributeTypeID, (object) null);
                  dbRelation.TryToAddOrDelAttribute(SubstitutesConstants.SubstituteNameAttributeTypeID, (object) null);
                  dbRelation.TryToAddOrDelAttribute(SubstitutesConstants.DesignActualVariantAttributeTypeID, (object) null);
                  dbRelation.TryToAddOrDelAttribute(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID, (object) null);
                  dbRelation.TryToAddOrDelAttribute(SubstitutesConstants.PositionNumberAttributeTypeID, (object) null);
                }
              }
            }
            else
            {
              flag1 = true;
              NewRelationProperties relationProperties = new NewRelationProperties(0L, projInfo.Id, this.Object_F_ID, DateTime.MinValue, DateTime.MaxValue, this.ObjectId);
              if (this.avsDocument.GetRelationType(this, (AVSDocumentContext) null, this.ObjType, this.RelType) == AvsIDCache.Relation_Document)
              {
                IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(AvsIDCache.Relation_Document);
                dbRelation = sessionKeeper.Session.GetRelation(projInfo.Id, this.ObjectId, this.RelType, true) ?? (this.avsDocument.productsInfo.Count <= 1 ? relationCollection.Create(relationProperties) : AVSDocument.CreateDocRelationWithLockPDMHandler(relationCollection, relationProperties));
              }
              else
                dbRelation = sessionKeeper.Session.GetRelationCollection(this.RelType).Create(relationProperties);
            }
            attrValue = dbRelation.RelationID;
            guid = dbRelation.GUID;
            num1 = attrValue;
            projID = dbRelation.ProjID;
            num2 = dbRelation.RelationType;
          }
          RelationAttributeValuesCache relationData;
          if (this.relations.Count > 0)
          {
            relationData = (RelationAttributeValuesCache) this.Relations[0].Clone();
          }
          else
          {
            AttributeValueMap valueMapForRelation = this.avsDocument.GetAttributeValueMapForRelation(num2);
            relationData = new RelationAttributeValuesCache(valueMapForRelation.AttributeDictionary, valueMapForRelation.AttrsInfo, projInfo);
          }
          relationData.SetValue(ObligatoryObjectAttributes.F_PRJLINK_ID, (object) attrValue, false);
          relationData.SetValue(ObligatoryObjectAttributes.F_PRJ_GUID, (object) guid, false);
          relationData.SetValue(ObligatoryObjectAttributes.F_PROJ_ID, (object) projInfo.Id, false);
          relationData.SetValue(ObligatoryObjectAttributes.F_RELATION_TYPE, (object) num2, false);
          relationData.projInfo = projInfo;
          relationData.SetValue(AvsIDCache.Attr_DopZamenGroupNum, (object) null, false);
          relationData.SetValue(this.avsDocument.Attr_DopZamenText, (object) null, false);
          relationData.SetValue(AvsIDCache.Attr_DopZamenSubstituteName, (object) null, false);
          relationData.SetValue(AvsIDCache.Attr_DopZamenGroupName, (object) null, false);
          relationData.SetValue(this.Field_Count, (object) null, false);
          relationData.SetValue(AvsIDCache.Attr_CountForAdjustment, (object) null, false);
          this.AddRowData(relationData);
          num5 = this.Relations.Count - 1;
          if (this.DocNode != null & flag1)
          {
            List<AvsRowAttributeInfo> docRowFields = this.DocRowFields;
            int index1 = -1;
            foreach (TextData cell in (IEnumerable<TextData>) this.DocNode.TextCellsEnumerator)
            {
              ++index1;
              if (docRowFields[index1].IsRelationAttribute && !AVSRow.IsCountField(docRowFields[index1]))
              {
                string valueFromDocCell = this.GetOldBaseFieldValueFromDocCell(cell, docRowFields[index1]);
                if (!string.IsNullOrEmpty(valueFromDocCell))
                {
                  this.SetFieldValue(docRowFields[index1], num5, productIndex, this.relations, (object) valueFromDocCell, true, false, false, this.avsDocument.IsGridViewMode, false, false);
                  if (this.HasHiddenRelation)
                  {
                    for (int index2 = 0; index2 < this.hiddenRelations.Count; ++index2)
                    {
                      if (this.hiddenRelations[index2].ProjectId == projInfo.Id)
                        this.SetFieldValue(docRowFields[index1], index2, productIndex1, this.hiddenRelations, (object) valueFromDocCell, true, false, false, this.avsDocument.IsGridViewMode, false, false);
                    }
                  }
                }
              }
            }
            string attributeValue = this.DocNode.GetAttributeValue(this.Field_PosDesignation.Name, false);
            if (!string.IsNullOrEmpty(attributeValue))
              this.SetFieldValueForAllRelations(this.Field_PosDesignation, (object) attributeValue, true, false, false, false, false, false);
          }
          if (flag1)
          {
            this.SetFieldValue(this.Attr_Section, num5, productIndex, this.relations, (object) this.SectionID, true, false, false, false, false, false);
            this.SetFieldValue(this.avsDocument.Attr_SortIndex, num5, productIndex, this.relations, (object) this.SortIndex, true, true, false, false, false, false);
            int int32 = Convert.ToInt32(this.GetFieldValue(this.avsDocument.Attr_Podbor, -1, -1, true, false));
            if (int32 == 1)
              this.SetFieldValue(this.avsDocument.Attr_Podbor, -1, -1, (object) int32, true, true, false, false, false, false, false);
            if (this.HasHiddenRelation)
            {
              for (int index = 0; index < this.hiddenRelations.Count; ++index)
              {
                if (this.hiddenRelations[index].ProjectId == projInfo.Id)
                {
                  this.SetFieldValue(this.Attr_Section, index, productIndex1, this.hiddenRelations, (object) this.SectionID, true, false, false, false, false, false);
                  this.SetFieldValue(this.avsDocument.Attr_SortIndex, index, productIndex1, this.hiddenRelations, (object) this.SortIndex, true, true, false, false, false, false);
                }
              }
            }
          }
        }
        if (num5 != -1)
        {
          this.SetFieldValue(this.Field_Count, num5, productIndex, this.relations, obj, true, false, updateDocNode, this.avsDocument.IsGridViewMode, false, false);
          if (this.HasHiddenRelation)
          {
            for (int index = 0; index < this.hiddenRelations.Count; ++index)
            {
              if (this.hiddenRelations[index].ProjectId == projInfo.Id)
                this.SetFieldValue(this.Field_Count, index, productIndex1, this.hiddenRelations, obj, true, false, updateDocNode, this.avsDocument.IsGridViewMode, false, false);
            }
          }
        }
        else
        {
          string editValue = Convert.ToString(obj);
          this.SetFieldValueInDocRowsCell(this.Field_Count, (TableData) null, productIndex, editValue);
        }
      }
    }
    this.UpdateNoteDocCellText();
    if (num1.IsDefinedId())
    {
      if ((!this.avsDocument.AvsDocumentNowLoading ? 0 : (ImDocumentData.ShowDebugInfo ? 1 : 0)) != 0)
        return;
      ServicesManager.GetService<INotificationService>(false)?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", num1, projID, num2));
    }
    else
    {
      if (!num3.IsDefinedId())
        return;
      ServicesManager.GetService<INotificationService>(false)?.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", num3));
    }
  }

  /// <summary>Обработчик изменения текста в ячейке документа</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Параметры события</param>
  public void cell_TextChanged(object sender, TextChanged_EventArgs e)
  {
    TextData textData = sender as TextData;
    TableData tableData = (TableData) null;
    AvsRowAttributeInfo rowAttributeInfo = (AvsRowAttributeInfo) null;
    int productIndex = 0;
    if (textData != null)
    {
      tableData = AVSDocument.FindParentSpecRowDocNode((DocumentTreeNode) textData) as TableData;
      rowAttributeInfo = this.GetCellBaseFieldInfo(textData, out productIndex);
    }
    if (this.avsDocument != null)
    {
      if (this.avsDocument.CollectChangeEvents && rowAttributeInfo != null)
        this.avsDocument.AvsRowEventMessageViewer.AddEvent(this, new AvsRowEventMessage(AVSEventType.ChangeRow)
        {
          ProductIndex = productIndex,
          AttrInfo = rowAttributeInfo,
          OriginalValue = e.OldText ?? "",
          NewValue = e.NewText ?? ""
        });
      if (this.avsDocument.DocCell_TextChanged_IsLocked)
        return;
      this.avsDocument.Lock_DocCell_TextChanged();
      if (!e.UpdateLayout)
        this.avsDocument.SuspendDocumentAndGridUpdates(true, false);
    }
    try
    {
      if (textData == null || tableData == null || rowAttributeInfo == null)
        return;
      string text = textData.Text;
      int index = textData.Index;
      List<AvsRowAttributeInfo> docRowFields = this.DocRowFields;
      string templateId = textData.TemplateId;
      if (this.IsNoteRow)
      {
        if (this.IsFormB && AVSRow.IsCountField(rowAttributeInfo))
          this.SetCount(productIndex, (object) text, true);
        else
          this.SetFieldValue(rowAttributeInfo, -1, -1, (List<RelationAttributeValuesCache>) null, (object) text, false, false, true, this.avsDocument.IsGridViewMode, false, false);
      }
      else if (this.IsFormB && AVSRow.IsCountField(rowAttributeInfo))
      {
        if (productIndex == -1 || productIndex >= this.avsDocument.productsInfo.Count)
          return;
        this.SetCount(productIndex, (object) text, true);
      }
      else
      {
        if (!this.IsFormB && AVSRow.IsCountField(rowAttributeInfo) && this.HasRelation)
          this.SetCount(-1, (object) text, true);
        else if (rowAttributeInfo.Equals((AttributeInfo) this.Field_Note))
        {
          if (AVSRow.ExtractTextBetweenProtectedZones(textData as TextBoxElement, out text))
            this.SetFieldValue(rowAttributeInfo, -1, productIndex, (List<RelationAttributeValuesCache>) null, (object) text, true, false, true, this.avsDocument.IsGridViewMode, false, true);
        }
        else if (this.Field_Name.Equals((AttributeInfo) rowAttributeInfo))
        {
          if (AVSRow.ExtractTextBetweenProtectedZones(textData as TextBoxElement, out text))
            this.SetAdditionalNameNote(new FieldContext(this, -1, productIndex, (List<RelationAttributeValuesCache>) null)
            {
              DocRow = tableData,
              DocCell = textData
            }, text);
        }
        else
        {
          this.SetFieldValue(rowAttributeInfo, -1, -1, (object) text, true, false, true, this.avsDocument.IsGridViewMode, false, false);
          if (!string.IsNullOrEmpty(text) && !textData.InPlaceEditorActive && (AVSRow.IsCountField(rowAttributeInfo) || this.Field_PosDesignation.Equals((AttributeInfo) rowAttributeInfo)))
          {
            string textForDocCell = this.GetTextForDocCell(this.GetCellAttributeMapping(textData), rowAttributeInfo, 0, -1, true, true);
            if (textForDocCell != text)
              textData.AssignText(textForDocCell, false, true, false, false, false);
          }
        }
        if (this.Field_Format.Equals((AttributeInfo) rowAttributeInfo))
        {
          this.avsDocument.UpdatePartProductCaptions();
        }
        else
        {
          if (!rowAttributeInfo.IsRelationAttribute || rowAttributeInfo.AttributeId != AvsIDCache.Attr_Position)
            return;
          this.avsDocument.UpdateSkipLines(false, false);
          this.avsDocument.UpdatePositionsCaptions();
        }
      }
    }
    finally
    {
      if (this.avsDocument != null)
      {
        this.avsDocument.Unlock_DocCell_TextChanged();
        if (!e.UpdateLayout)
          this.avsDocument.ResumeDocumentAndGridUpdates(0, e.UpdateUI, e.UpdateUI, true, false);
      }
    }
  }

  internal static bool ExtractTextBetweenProtectedZones(TextBoxElement cell, out string text)
  {
    text = "";
    if (cell == null || cell.ProtectedFirstCharCount == -1)
      return false;
    text = cell.Text;
    if (cell.ProtectedFirstCharCount > 0 || cell.ProtectedEndCharCount > 0)
      text = cell.ProtectedFirstCharCount + cell.ProtectedEndCharCount >= text.Length ? "" : text.Substring(cell.ProtectedFirstCharCount, text.Length - (cell.ProtectedFirstCharCount + cell.ProtectedEndCharCount));
    return true;
  }

  /// <summary>Обработчик события активизации редактора в ячейке документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cell_InplaceEditorActivating(object sender, CancelEventArgs e)
  {
    if (this.avsDocument != null)
      this.avsDocument.Lock_DocCell_TextChanged();
    try
    {
      if (!(sender is TextBoxElement cell) || cell.Parent == null)
        return;
      string text = cell.Text;
      AvsRowAttributeInfo cellBaseFieldInfo = this.GetCellBaseFieldInfo((TextData) cell, out int _);
      CellOutputMapping attributeMapping = this.GetCellAttributeMapping((TextData) cell);
      if (AVSRow.IsCountFormBCell(this.IsFormB, (TextData) cell))
      {
        if (cellBaseFieldInfo != null)
          cell.ReadOnly |= this.GetAttributeReadOnly(cellBaseFieldInfo, 0, this.Relations);
        int indexForCountCell = this.GetProductIndexForCountCell((TextData) cell);
        if (indexForCountCell == -1 || indexForCountCell >= this.avsDocument.productsInfo.Count)
          return;
        int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[indexForCountCell].Id, this.relations);
        if (relationIndexForProduct == -1)
          return;
        string textForDocCell = this.GetTextForDocCell(attributeMapping, this.Field_Count, relationIndexForProduct, indexForCountCell, false, false);
        if (textForDocCell != text)
        {
          cell.AssignText(textForDocCell, false, true, false, false, false);
          cell.SetAttributeValue(AVSRow.CellAttrName_ViewText, text, false, false, false);
        }
        else
          cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
      }
      else
      {
        if (cellBaseFieldInfo == null)
          return;
        cell.ReadOnly |= this.GetAttributeReadOnly(cellBaseFieldInfo, 0, this.Relations);
        if (this.avsDocument.IsSpecification && !this.IsNoteRow && this.Field_Name.Equals((AttributeInfo) cellBaseFieldInfo))
        {
          bool flag = cell.ProtectedFirstCharCount == -1;
          cell.ReadOnly = flag || this.IsDocRelation || this.HasDynamicGroupHeader;
        }
        if (cell.ReadOnlyNow && cell.IsEmptyText)
          e.Cancel = true;
        else if (AVSRow.IsCountField(cellBaseFieldInfo) || this.Field_Format.Equals((AttributeInfo) cellBaseFieldInfo) || this.Field_Zone.Equals((AttributeInfo) cellBaseFieldInfo) || this.Field_Position.Equals((AttributeInfo) cellBaseFieldInfo))
        {
          string attributeValue = !this.HasRelation ? (!cell.ContainsAttribute(AVSRow.CellAttrName_EditText) ? text : cell.GetAttributeValue(AVSRow.CellAttrName_EditText, true)) : this.GetTextForDocCell(attributeMapping, cellBaseFieldInfo, 0, -1, false, false);
          if (attributeValue != text)
          {
            if (cellBaseFieldInfo.AttributeId == AvsIDCache.Attr_Format || cellBaseFieldInfo.AttributeId == AvsIDCache.Attr_Zone)
              cell.SetAttributeValue(AVSRow.CellAttrName_EditText, attributeValue, false, false, false);
            cell.AssignText(attributeValue, false, true, false, false, false);
            cell.SetAttributeValue(AVSRow.CellAttrName_ViewText, text, false, false, false);
          }
          else
            cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
          if (!this.Field_Format.Equals((AttributeInfo) cellBaseFieldInfo) && !this.Field_Zone.Equals((AttributeInfo) cellBaseFieldInfo))
            return;
          cell.SetFlags((byte) 8, true);
        }
        else
        {
          if (!this.Field_PosDesignation.Equals((AttributeInfo) cellBaseFieldInfo))
            return;
          string textForDocCell = this.GetTextForDocCell(attributeMapping, cellBaseFieldInfo, -1, -1, false, false);
          cell.SetAttributeValue(AVSRow.CellAttrName_EditText, textForDocCell, false, false, false);
          cell.AssignText(textForDocCell, false, true, false, false, false);
          cell.SetAttributeValue(AVSRow.CellAttrName_ViewText, text, false, false, false);
        }
      }
    }
    finally
    {
      if (this.avsDocument != null)
        this.avsDocument.Unlock_DocCell_TextChanged();
    }
  }

  /// <summary>Обработчик события деактивизации редактора в ячейке документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cell_InplaceEditorDeactivated(object sender, EventArgs e)
  {
    if (this.IsNoteRow)
      return;
    if (this.avsDocument != null)
      this.avsDocument.Lock_DocCell_TextChanged();
    try
    {
      if (!(sender is TextData cell) || cell.Parent == null)
        return;
      string text = cell.Text;
      AvsRowAttributeInfo cellBaseFieldInfo = this.GetCellBaseFieldInfo(cell, out int _);
      CellOutputMapping attributeMapping = this.GetCellAttributeMapping(cell);
      if (AVSRow.IsCountFormBCell(this.IsFormB, cell))
      {
        int indexForCountCell = this.GetProductIndexForCountCell(cell);
        if (indexForCountCell == -1 || indexForCountCell >= this.avsDocument.productsInfo.Count)
          return;
        int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[indexForCountCell].Id, this.relations);
        if (relationIndexForProduct == -1)
          return;
        this.GetTextForDocCell(attributeMapping, this.Field_Count, relationIndexForProduct, indexForCountCell, true, false);
        if (cell.ContainsAttribute(AVSRow.CellAttrName_ViewText))
        {
          string attributeValue = cell.GetAttributeValue(AVSRow.CellAttrName_ViewText, false);
          cell.AssignText(attributeValue, false, true, false, true, true);
        }
        cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
      }
      else
      {
        if (cellBaseFieldInfo == null)
          return;
        if (AVSRow.IsCountField(cellBaseFieldInfo) || this.Field_Format.Equals((AttributeInfo) cellBaseFieldInfo) || this.Field_Zone.Equals((AttributeInfo) cellBaseFieldInfo) || this.Field_Position.Equals((AttributeInfo) cellBaseFieldInfo))
        {
          if (!cell.ContainsAttribute(AVSRow.CellAttrName_ViewText))
            return;
          if (this.GetTextForDocCell(attributeMapping, cellBaseFieldInfo, 0, -1, false, false) == text)
          {
            string attributeValue = cell.GetAttributeValue(AVSRow.CellAttrName_ViewText, false);
            cell.AssignText(attributeValue, false, true, false, true, true);
          }
          cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
        }
        else
        {
          if (!this.Field_PosDesignation.Equals((AttributeInfo) cellBaseFieldInfo))
            return;
          cell.SetAttributeValue(AVSRow.CellAttrName_EditText, cell.Text, false, false, false);
          string textForDocCell = this.GetTextForDocCell(attributeMapping, cellBaseFieldInfo, 0, -1, true, true);
          cell.AssignText(textForDocCell, false, true, false, false, false);
          cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
        }
      }
    }
    finally
    {
      if (this.avsDocument != null)
        this.avsDocument.Unlock_DocCell_TextChanged();
    }
  }

  /// <summary>Получить родительское окно спецификации</summary>
  /// <returns></returns>
  public AVSWindow GetAVSWindow()
  {
    if (this.avsDocument != null)
      return this.avsDocument.AVSWindow;
    AVSWindow avsWindow = (AVSWindow) null;
    if (this.docNode != null)
    {
      Control control = (Control) ((TableElement) this.docNode).PageUI.DocumentControl;
      while (true)
      {
        switch (control)
        {
          case null:
          case AVSWindow _:
            goto label_6;
          default:
            control = control.Parent;
            continue;
        }
      }
label_6:
      avsWindow = control as AVSWindow;
    }
    return avsWindow;
  }

  /// <summary>Получить предыдущую запись (только в текущем разделе)</summary>
  public AVSRow GetPrevRow() => this.GetPrevRow(true);

  /// <summary>Получить предыдущую запись</summary>
  /// <param name="onlyThisSection"> Если true и предыдущая запись принадлежит иной секции, то вернёт null </param>
  public AVSRow GetPrevRow(bool onlyThisSection)
  {
    SpecificationSection section = this.Section;
    if (section == null)
      return (AVSRow) null;
    int index = this.Index;
    if (index != 0)
      return section.Rows[index - 1];
    if (onlyThisSection)
      return (AVSRow) null;
    SpecificationSection specificationSection = section;
    do
    {
      specificationSection = specificationSection.GetPrevSection();
      if (specificationSection == null)
        goto label_9;
    }
    while (specificationSection.Rows.Count <= 0);
    return specificationSection.Rows[specificationSection.Rows.Count - 1];
label_9:
    return (AVSRow) null;
  }

  /// <summary>Получить предыдущую запись (только в текущем разделе) </summary>
  public AVSRow GetNextRow() => this.GetNextRow(true);

  /// <summary>Получить следующую запись</summary>
  /// <param name="onlyThisSection"> Если true и следующая запись принадлежит иной секции, то вернёт null </param>
  public AVSRow GetNextRow(bool onlyThisSection)
  {
    SpecificationSection section = this.Section;
    if (section == null)
      return (AVSRow) null;
    int index = this.Index;
    if (index < section.Rows.Count - 1)
      return section.Rows[index + 1];
    if (onlyThisSection)
      return (AVSRow) null;
    SpecificationSection specificationSection = section;
    while (specificationSection != null)
    {
      specificationSection = specificationSection.GetNextSection();
      if (specificationSection != null && specificationSection.Rows.Count > 0)
        return specificationSection.Rows[0];
    }
    return (AVSRow) null;
  }

  internal bool CanAddAsNotHiddenRelation(RelationAttributeValuesCache relationData)
  {
    if (relationData == null)
      throw new ArgumentNullException(nameof (relationData));
    return !this.HasRelation || !this.ContainsRelationForProduct(relationData.ProjectId);
  }

  /// <summary>Добавить данные связи в кэш атрибутов</summary>
  /// <param name="relationData">Данные связи</param>
  /// <param name="objectData">Атрибуты объекта, если relationData задан, то можно не задавать</param>
  /// <param name="addToHidden">Добавить в скрытые записи</param>
  public void AddRowData(
    RelationAttributeValuesCache relationData,
    AttributeValuesCache objectData = null,
    bool addToHidden = false)
  {
    if (relationData != null && (this.ObjectId.IsDefinedId() && this.ObjectId != relationData.ObjectId && relationData.ObjectId.IsDefinedId() || this.relations != null && this.relations.Count > 0 && this.relations[0].ObjectId != relationData.ObjectId))
      throw new Exception($"Попытка вставить в запись с объектом \"{this.ObjCaption}\" [{this.ObjectId.ToString()}] и связью [{this.RelId.ToString()}] связь с другим объектом");
    if (objectData != null && this.ObjectId.IsDefinedId() && this.ObjectId != objectData.ObjectId)
      throw new Exception($"Попытка вставить в запись с объектом \"{this.ObjCaption}\" [{this.ObjectId.ToString()}] связь с другим объектом");
    if (!addToHidden && relationData != null && !this.CanAddAsNotHiddenRelation(relationData))
      throw new Exception($"Попытка вставить в запись с объектом \"{this.ObjCaption}\" [{this.ObjectId}] и связью [{this.RelId}] связь [{relationData.RelationId}] из состава того же исполнения");
    int index1 = -1;
    if (objectData != null)
      this.objectAttributesCache = objectData;
    List<RelationAttributeValuesCache> attributeValuesCacheList = (List<RelationAttributeValuesCache>) null;
    if (relationData != null)
    {
      if (addToHidden)
      {
        if (this.hiddenRelations == null)
          this.hiddenRelations = new List<RelationAttributeValuesCache>(1);
        attributeValuesCacheList = this.hiddenRelations;
      }
      else
      {
        if (this.relations == null)
          this.relations = new List<RelationAttributeValuesCache>(1);
        attributeValuesCacheList = this.relations;
      }
      attributeValuesCacheList.Add(relationData);
      if (relationData.ObjectAttributesCache == null)
        relationData.ObjectAttributesCache = this.ObjectAttributesCache;
      if (this.objectAttributesCache == null)
        this.objectAttributesCache = relationData.ObjectAttributesCache;
      index1 = attributeValuesCacheList.Count - 1;
    }
    if (relationData != null && attributeValuesCacheList != null && attributeValuesCacheList.Count == 1)
    {
      if (this.rowID == null)
        this.rowID = new DBRelationInfo(relationData.RelationGuid, relationData.RelationId, relationData.RelationType, relationData.ProjectGuid, relationData.ProjectId, relationData.ObjectGuid, relationData.ObjectId, relationData.ObjectType, relationData.ObjectCaption);
      else
        this.rowID.SetDBRelationInfo(relationData.RelationGuid, relationData.RelationId, relationData.RelationType, relationData.ProjectGuid, relationData.ProjectId, relationData.ObjectGuid, relationData.ObjectId, relationData.ObjectType, relationData.ObjectCaption);
    }
    else if (objectData != null)
    {
      if (attributeValuesCacheList != null && attributeValuesCacheList.Count > 0)
      {
        if (this.rowID == null)
          this.rowID = new DBRelationInfo(attributeValuesCacheList[0].RelationGuid, attributeValuesCacheList[0].RelationId, attributeValuesCacheList[0].RelationType, attributeValuesCacheList[0].ProjectGuid, attributeValuesCacheList[0].ProjectId, attributeValuesCacheList[0].ObjectGuid, attributeValuesCacheList[0].ObjectId, attributeValuesCacheList[0].ObjectType, attributeValuesCacheList[0].ObjectCaption);
        else
          this.rowID.SetDBRelationInfo(attributeValuesCacheList[0].RelationGuid, attributeValuesCacheList[0].RelationId, attributeValuesCacheList[0].RelationType, attributeValuesCacheList[0].ProjectGuid, attributeValuesCacheList[0].ProjectId, attributeValuesCacheList[0].ObjectGuid, attributeValuesCacheList[0].ObjectId, attributeValuesCacheList[0].ObjectType, attributeValuesCacheList[0].ObjectCaption);
      }
      else if (this.rowID == null)
        this.rowID = new DBRelationInfo(Guid.Empty, -1L, -1, Guid.Empty, -1L, objectData.ObjectGuid, objectData.ObjectId, objectData.ObjectType, objectData.ObjectCaption);
      else
        this.rowID.SetDBRelationInfo(Guid.Empty, -1L, this.rowID.RelationType, Guid.Empty, -1L, objectData.ObjectGuid, objectData.ObjectId, objectData.ObjectType, objectData.ObjectCaption);
    }
    foreach (RelationAttributeValuesCache allRelation in this.AllRelations)
      allRelation.ObjectAttributesCache = this.ObjectAttributesCache;
    if (this.avsDocument != null)
    {
      if (index1 != -1 && attributeValuesCacheList != null && index1 < attributeValuesCacheList.Count)
        this.avsDocument.RegisterAVSRowRelationWithObjectInDictionaries(this, attributeValuesCacheList[index1]);
      else
        this.avsDocument.RegisterAVSRowInDictionaries(this);
    }
    this.SaveRelationsReferencesToDocRows();
    if (addToHidden && !this.HasDocNodes)
      this.IsSorted &= !string.IsNullOrEmpty(relationData.GetValueString(this.Field_PosDesignation, false));
    if (addToHidden || this.DocNodes.Count <= 0 && this.docNodeExp == null || this.relations == null || this.relations.Count != 1)
      return;
    long sortIndex = this.SortIndex;
    if (sortIndex < 0L)
      return;
    string attributeValue = sortIndex.ToString();
    for (int index2 = 0; index2 < this.DocNodes.Count; ++index2)
      this.docNodes[index2].SetAttributeValue(AVSRow.RowAttr_SortIndex, attributeValue);
    if (this.docNodeExp == null)
      return;
    this.docNodeExp.SetAttributeValue(AVSRow.RowAttr_SortIndex, attributeValue);
  }

  /// <summary>Удалить запись из раздела</summary>
  /// <param name="removeFromDictionary">Удалить из словарей</param>
  /// <param name="removeRelation">Удалить связь из базы</param>
  /// <param name="removeDocNode">Удалить из документа</param>
  /// <param name="removeTreeNode">Удалить из табличного вида</param>
  /// <param name="removeDocObjectWithoutRelations">Удалять документы без связей</param>
  public List<KeyValuePair<long, RelInfo>> Remove(
    bool removeFromDictionary = true,
    bool removeRelation = true,
    bool removeDocNode = true,
    bool removeTreeNode = true,
    bool removeDocObjectWithoutRelations = false)
  {
    return this.Section != null ? this.Section.RemoveRow(this, removeFromDictionary, removeRelation, removeDocNode, removeTreeNode, removeDocObjectWithoutRelations) : new List<KeyValuePair<long, RelInfo>>();
  }

  internal void RemoveRelationData(RelationAttributeValuesCache relation, bool removeRelation = false)
  {
    if (this.Relations != null && this.Relations.Contains(relation))
      this.RemoveRelationData(this.Relations, this.Relations.IndexOf(relation), removeRelation);
    if (this.HiddenRelations == null || !this.HiddenRelations.Contains(relation))
      return;
    this.RemoveRelationData(this.HiddenRelations, this.HiddenRelations.IndexOf(relation), removeRelation);
  }

  /// <summary>Удалить данные связи из кэша атрибутов</summary>
  /// <param name="relationList">Список связей</param>
  /// <param name="relationIndex">Индекс связи в списке</param>
  internal void RemoveRelationData(
    List<RelationAttributeValuesCache> relationList,
    int relationIndex,
    bool removeRelation = false)
  {
    if (relationList == null)
      relationList = this.relations;
    if (relationList == null)
      return;
    if (relationIndex < 0 || relationIndex >= relationList.Count)
      throw new ArgumentOutOfRangeException(nameof (relationIndex));
    if (this.avsDocument != null)
      this.avsDocument.UnregisterAVSRowRelationInDictionaries(this, relationList[relationIndex]);
    long sortIndex = this.SortIndex;
    if (removeRelation)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        bool flag = this.IsDocRelation && !AVSDocument.IsParentObjectType(AvsIDCache.ObjType_ConstructorDocument, this.ObjType);
        IDBRelation relationByPartObjectId = sessionKeeper.Session.GetRelationByPartObjectID(relationList[relationIndex].RelationId, this.ObjectId, true);
        if (relationByPartObjectId != null)
        {
          if (flag)
            relationByPartObjectId.SetAttributesValues(new AttributeValues[1]
            {
              new AttributeValues(AvsIDCache.Attr_SpecificationSection, (object) null)
            });
          else
            relationByPartObjectId.Delete(0L);
        }
      }
    }
    relationList.RemoveAt(relationIndex);
    AVSRow avsRow;
    if (this.SortIndex != sortIndex && this.avsDocument.SortIndexDictionary.TryGetValue(sortIndex, out avsRow) && avsRow == this)
      this.avsDocument.SortIndexDictionary.Remove(sortIndex);
    if (relationList == this.relations)
    {
      if (relationList.Count == 0)
        this.RowID.SetDBRelationInfo(Guid.Empty, -1L, -1, Guid.Empty, -1L, this.RowID.ObjectGuid, this.RowID.ObjectID, this.RowID.ObjectType, this.RowID.ObjectCaption);
      else if (relationIndex == 0)
        this.RowID.SetDBRelationInfo(relationList[0].RelationGuid, relationList[0].RelationId, relationList[0].RelationType, relationList[0].ProjectGuid, relationList[0].ProjectId, this.RowID.ObjectGuid, this.RowID.ObjectID, this.RowID.ObjectType, this.RowID.ObjectCaption);
    }
    this.SaveRelationsReferencesToDocRows();
  }

  /// <summary>Переместить связь из источника в эту запись</summary>
  internal void MoveRelationInInternalLists(int srcRelationIndex, bool toHidden)
  {
    if (this.relations == null)
      this.relations = new List<RelationAttributeValuesCache>();
    if (this.hiddenRelations == null)
      this.hiddenRelations = new List<RelationAttributeValuesCache>();
    List<RelationAttributeValuesCache> attributeValuesCacheList1 = toHidden ? this.Relations : this.HiddenRelations;
    List<RelationAttributeValuesCache> attributeValuesCacheList2 = toHidden ? this.HiddenRelations : this.Relations;
    RelationAttributeValuesCache attributeValuesCache = attributeValuesCacheList1[srcRelationIndex];
    attributeValuesCacheList1.RemoveAt(srcRelationIndex);
    attributeValuesCacheList2.Add(attributeValuesCache);
  }

  /// <summary>
  /// Если список основных связей пуст, то переместить связь из скрытых
  /// </summary>
  internal void RestoreBaseRelationsFromHidden()
  {
    if (this.HasRelation || !this.HasHiddenRelation)
      return;
    RelationAttributeValuesCache hiddenRelation = this.HiddenRelations[0];
    this.HiddenRelations.RemoveAt(0);
    if (this.relations == null)
      this.relations = new List<RelationAttributeValuesCache>();
    this.relations.Add(hiddenRelation);
  }

  /// <summary>Очистить списки со связями. Используется при удалении</summary>
  internal void ClearRelations()
  {
    this.relations = (List<RelationAttributeValuesCache>) null;
    this.hiddenRelations = (List<RelationAttributeValuesCache>) null;
  }

  /// <summary>Получить индекс связи для исполнения</summary>
  /// <param name="productIndex">Индекс исполнения в основном списке документа</param>
  /// <param name="relationList">Список связей</param>
  /// <returns>Индекс связи</returns>
  public int GetRelationForProductIndex(
    int productIndex,
    List<RelationAttributeValuesCache> relationList = null)
  {
    if (productIndex < 0 || productIndex >= this.avsDocument.ProductsInfo.Count)
      return -1;
    long id = this.avsDocument.ProductsInfo[productIndex].Id;
    if (id.IsUndefinedId() && this.avsDocument.IsElementList && this.avsDocument.IsSingleForm && !this.avsDocument.ParentProducts.IsEmpty<ProductInfo>())
      id = this.avsDocument.ParentProducts[0].Id;
    return this.GetRelationIndexForProduct(id, relationList);
  }

  /// <summary>Получить индекс связи для исполнения</summary>
  /// <param name="productId">Идентификатор исполнения</param>
  /// <param name="relationList">Список связей</param>
  /// <returns>Индекс связи</returns>
  public int GetRelationIndexForProduct(
    long productId,
    List<RelationAttributeValuesCache> relationList = null)
  {
    if (relationList == null)
      relationList = this.relations;
    if (productId.IsUndefinedId() || relationList == null)
      return -1;
    for (int index = 0; index < relationList.Count; ++index)
    {
      if (relationList[index].ProjectId == productId)
        return index;
    }
    return -1;
  }

  /// <summary>Есть связь с заданным исполнением</summary>
  /// <param name="productId">Идентификатор исполнения</param>
  /// <param name="relationList">Список связей, в котором нужно искать. null, если по умолчанию (видимые связи по исполнениям)</param>
  public bool ContainsRelationForProduct(
    long productId,
    List<RelationAttributeValuesCache> relationList = null)
  {
    return this.GetRelationForProduct(productId, relationList) != null;
  }

  /// <summary>Получить связь для исполнения</summary>
  /// <param name="productId">Идентификатор исполнения</param>
  /// <param name="relationList">Список связей, в котором нужно искать. null, если по умолчанию (видимые связи по исполнениям)</param>
  /// <returns>Данные связи. null если не найдена</returns>
  public RelationAttributeValuesCache GetRelationForProduct(
    long productId,
    List<RelationAttributeValuesCache> relationList = null)
  {
    if (relationList == null)
      relationList = this.relations;
    int relationIndexForProduct = this.GetRelationIndexForProduct(productId, relationList);
    return relationIndexForProduct != -1 ? relationList[relationIndexForProduct] : (RelationAttributeValuesCache) null;
  }

  public int GetRelationIndex(
    long relationId,
    out List<RelationAttributeValuesCache> relationList)
  {
    int relationIndex = -1;
    relationList = (List<RelationAttributeValuesCache>) null;
    if (this.Relations != null)
    {
      relationIndex = this.GetRelationIndex(this.Relations, relationId);
      if (relationIndex != -1)
        relationList = this.Relations;
    }
    if (this.HiddenRelations != null && relationIndex == -1)
    {
      relationIndex = this.GetRelationIndex(this.HiddenRelations, relationId);
      if (relationIndex != -1)
        relationList = this.HiddenRelations;
    }
    return relationIndex;
  }

  /// <summary>Найти связь по её идентификатору</summary>
  /// <param name="relationList">Список связей</param>
  /// <param name="relationId">Идентификатор связи</param>
  /// <returns>Индекс связи</returns>
  public int GetRelationIndex(List<RelationAttributeValuesCache> relationList, long relationId)
  {
    if (relationList == null)
      relationList = this.relations;
    if (relationList == null || relationId == -1L)
      return -1;
    for (int index = 0; index < relationList.Count; ++index)
    {
      if (relationList[index].RelationId == relationId)
        return index;
    }
    return -1;
  }

  /// <summary>Найти связь по её идентификатору</summary>
  /// <param name="relationId">Идентификатор связи</param>
  /// <param name="relationList">Список связей</param>
  /// <returns>Индекс связи</returns>
  public RelationAttributeValuesCache GetRelation(
    long relationId,
    List<RelationAttributeValuesCache> relationList = null)
  {
    if (relationList == null)
      relationList = this.relations;
    if (relationList == null || relationId == -1L)
      return (RelationAttributeValuesCache) null;
    for (int index = 0; index < relationList.Count; ++index)
    {
      if (relationList[index].RelationId == relationId)
        return relationList[index];
    }
    return (RelationAttributeValuesCache) null;
  }

  /// <summary>Удалить связи исполнений</summary>
  /// <param name="productsId">Идентификаторы исполнений, которые удаляются</param>
  /// <param name="updateDocCell">Обновить ячейку документа</param>
  /// <param name="updateGridCell">Обновить ячейку табличного вида</param>
  public void RemoveProducts(IList<long> productsId, bool updateDocCell, bool updateGridCell)
  {
    updateDocCell = updateDocCell && this.IsFormB;
    updateGridCell = updateGridCell && this.IsFormB;
    if (this.relations != null)
    {
      for (int index = this.relations.Count - 1; index >= 0; --index)
      {
        if (productsId.Contains(this.relations[index].ProjectId))
        {
          this.RemoveRelationData(this.relations, index);
          if (updateGridCell)
            this.UpdateGridRow();
        }
      }
    }
    if (this.hiddenRelations != null)
    {
      for (int index = this.hiddenRelations.Count - 1; index >= 0; --index)
      {
        if (productsId.Contains(this.hiddenRelations[index].ProjectId))
          this.RemoveRelationData(this.hiddenRelations, index);
      }
    }
    if (!updateDocCell)
      return;
    this.UpdateDocRow();
  }

  /// <summary>Получить значение перекрытых атрибутов</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="found">Возвращает true, если этот атрибут был перекрыт</param>
  /// <param name="context">Контекст получения поля</param>
  /// <param name="replaceDBNull">Заменять DBNull на null</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <returns>Возвращает значение атрибута, если он перекрыт</returns>
  private object GetOverridedFieldValue(
    AvsRowAttributeInfo attrInfo,
    out bool found,
    FieldContext context,
    bool replaceDBNull,
    bool failIfNotFound,
    bool originalValue = false)
  {
    found = false;
    if (attrInfo.AttributeId == AvsIDCache.Attr_NameForSpecification.AttributeId)
    {
      found = true;
      return (object) this.GetVirtualNameForSpecification(failIfNotFound);
    }
    if (attrInfo.AttributeId == AvsIDCache.Attr_PartForPodbor_NoteText.AttributeId)
    {
      found = true;
      return this.RelType != AvsIDCache.Relation_Podbor ? (object) null : (object) AvsConfig.Podbor.TextInNoteFieldOfPodborRow;
    }
    if (attrInfo.AttributeId == AvsIDCache.Attr_NominalAndLimitValues_NoteText.AttributeId)
    {
      found = true;
      string overridedFieldValue = (string) null;
      if (this.RelType == AvsIDCache.Relation_Project && this.IsBaseComponentForPodbor(context.RelationIndex, context.RelationList))
        overridedFieldValue = this.GetNominalAndLimitValues(context.RelationIndex, context.ProductIndex, context.RelationList, this.UseLimitValuesOnly);
      return (object) overridedFieldValue;
    }
    if (attrInfo.AttributeId == AvsIDCache.Attr_DraftForPartTextLink.AttributeId)
    {
      found = true;
      return (object) this.CalcVirtualAttrDraftForPart();
    }
    if (attrInfo.AttributeId == AvsIDCache.Attr_LookMainDocTextLink.AttributeId)
    {
      found = true;
      return (object) this.CaclVirtualAttrSmotri();
    }
    if (attrInfo.AttributeId == AvsIDCache.Attr_AdditionalNameNote.AttributeId)
    {
      found = true;
      return (object) this.GetAdditionalNameNote(context);
    }
    if (attrInfo.AttributeId == AvsIDCache.CountMeasureAttrInfo.AttributeId)
    {
      found = true;
      return (object) this.FindFirstCountMeasure();
    }
    if (!found && attrInfo.AttributeId == AvsIDCache.Attr_FirstApplicability && attrInfo.IsObjectAttribute && !this.IsDocRelation && this.DocNode != null)
    {
      string attributeValue = this.DocNode.GetAttributeValue(AVSRow.RowAttr_FirstApplicability, false);
      found = string.IsNullOrEmpty(attributeValue);
      if (found)
        return (object) attributeValue;
    }
    if (!found && !originalValue)
    {
      if (!this.IsDocRelation && attrInfo.Equals((AttributeInfo) this.Field_Format))
      {
        found = true;
        string overridedFieldValue = this.GetFieldStringValue(attrInfo, -1, -1, (List<RelationAttributeValuesCache>) null, failIfNotFound, true);
        if (string.IsNullOrWhiteSpace(overridedFieldValue))
        {
          TextData cellForAttribute = this.GetDocumentCellForAttribute(attrInfo, 0);
          if (cellForAttribute != null)
            overridedFieldValue = AVSRow.GetFieldValueFromDocCell(cellForAttribute);
          if (string.IsNullOrWhiteSpace(overridedFieldValue))
            overridedFieldValue = this.avsDocument.GetDefaultFormat(this.ObjType);
        }
        return (object) overridedFieldValue;
      }
      if (attrInfo.Equals((AttributeInfo) this.Field_Count))
      {
        found = true;
        return (object) this.GetCount(context.RelationIndex, context.ProductIndex, context.RelationList);
      }
      if ((attrInfo.Equals((AttributeInfo) this.Field_PosDesignation) || attrInfo.Equals((AttributeInfo) this.Attr_PodborForPosDesignation)) && this.HasRelation)
        return (object) this.GetOverridedPositionDesignation(attrInfo, out found);
    }
    return (object) null;
  }

  public string GetAdditionalNameNote(int productIndex)
  {
    return this.GetAdditionalNameNote(new FieldContext(this, -1, productIndex, (List<RelationAttributeValuesCache>) null));
  }

  private string GetAdditionalNameNote(FieldContext context)
  {
    if (context.DocRow == null)
      context.DocRow = this.FindDocRowForProduct(context.ProductIndex);
    string additionalNameNote = context.DocRow?.GetAttributeValue("NameNote", true) ?? "";
    if (!string.IsNullOrEmpty(additionalNameNote))
      return additionalNameNote;
    TextData textData = (TextData) null;
    int productIndex;
    if (context.DocCell != null && this.Field_Name.Equals((AttributeInfo) this.GetCellBaseFieldInfo(context.DocCell, out productIndex)) && context.ProductIndex == productIndex)
      textData = context.DocCell;
    if (textData == null)
      textData = this.GetDocumentCellForBaseField(this.Field_Name, context.DocRow, context.ProductIndex);
    return textData?.GetAttributeValue("NameNote", true) ?? "";
  }

  private void SetAdditionalNameNote(FieldContext context, string value)
  {
    if (context.DocRow == null)
      context.DocRow = this.FindDocRowForProduct(context.ProductIndex);
    if (context.DocRow == null)
      return;
    context.DocRow.SetAttributeValue("NameNote", value, false, false, false);
    TextData textData = (TextData) null;
    int productIndex;
    if (context.DocCell != null && this.Field_Name.Equals((AttributeInfo) this.GetCellBaseFieldInfo(context.DocCell, out productIndex)) && context.ProductIndex == productIndex)
      textData = context.DocCell;
    if (textData == null)
      textData = this.GetDocumentCellForBaseField(this.Field_Name, context.DocRow, context.ProductIndex);
    textData?.RemoveAttribute("NameNote", false, false);
  }

  /// <summary>
  /// Получить виртуальный атрибут "Наименование для графы в спецификации"
  /// </summary>
  /// <param name="failIfNotFound">Генерировать исключение, если атрибут Наименование не найден</param>
  /// <returns></returns>
  private string GetVirtualNameForSpecification(bool failIfNotFound)
  {
    string documentName = "";
    if ((!this.IsDocRelation || this.avsDocument.AVSCommonPropertiesSchema.UseUserAttributeForNameFieldForDocuments) && this.avsDocument.Attr_UserAttributeForNameField.AttributeGuid != Guid.Empty && !this.Field_Name.Equals((AttributeInfo) this.avsDocument.Attr_UserAttributeForNameField))
      documentName = this.GetFieldStringValue(this.avsDocument.Attr_UserAttributeForNameField, -1, -1, (List<RelationAttributeValuesCache>) null, false);
    if (documentName.IsEmpty())
      documentName = this.GetFieldStringValue(this.Field_Name, -1, -1, (List<RelationAttributeValuesCache>) null, failIfNotFound, true);
    else if (this.avsDocument.AVSCommonPropertiesSchema.UserAttributeForNamePosition != AttributeForNamePosition.Instead)
    {
      string newLine = Environment.NewLine;
      string fieldStringValue = this.GetFieldStringValue(this.Field_Name, -1, -1, (List<RelationAttributeValuesCache>) null, failIfNotFound, true);
      switch (this.avsDocument.AVSCommonPropertiesSchema.UserAttributeForNamePosition)
      {
        case AttributeForNamePosition.Before:
          documentName = documentName + newLine + fieldStringValue;
          break;
        case AttributeForNamePosition.After:
          documentName = fieldStringValue + newLine + documentName;
          break;
      }
    }
    if (this.IsDocRelation)
      documentName = this.GetDocumentNameInDocumentsSection(documentName);
    if (!this.IsDocRelation && this.HasObject)
    {
      string valueString = this.ObjectAttributesCache.GetValueString(AvsIDCache.Attr_ProductConventionalName, false);
      if (!string.IsNullOrEmpty(valueString))
        documentName = !(documentName != "") ? valueString : documentName + this.avsDocument.AVSCommonPropertiesSchema.NameDivider + valueString;
    }
    return documentName;
  }

  /// <summary>Получить значение для графы "Наименование" в разделе документов</summary>
  /// <param name="specificationDesignation">Обозначение спецификации</param>
  /// <param name="documentDesignation">Обозначение документа</param>
  /// <param name="documentName">Наименование документа</param>
  /// <param name="documentTypeID">Ид типа документа</param>
  /// <param name="documentTypeName">Наименование типа документа. Если не задано, то используется наименование из настроек</param>
  /// <returns></returns>
  public string GetDocumentNameInDocumentsSection(string documentName)
  {
    string documentsSection = documentName;
    string str1 = "";
    if (this.avsDocument.Attr_UserAttributeForDocType.AttributeGuid != Guid.Empty && this.HasObject)
      str1 = this.ObjectAttributesCache.GetValueString(this.avsDocument.Attr_UserAttributeForDocType, false);
    if (string.IsNullOrEmpty(str1) && this.ObjType.IsDefinedTypeId())
      str1 = this.avsDocument.GetDocTypeName(this.ObjType);
    string str2 = this.avsDocument.DocumentDesignation;
    if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(this.Designation) && !string.IsNullOrEmpty(str2))
    {
      if (str2.EndsWith("СП", StringComparison.OrdinalIgnoreCase))
        str2 = str2.Substring(0, str2.Length - 2).TrimEnd();
      documentsSection = this.Designation.IndexOf(str2) == -1 ? (this.GetFieldBoolValue(this.avsDocument.Attr_InMainDocComplect, 0, 0, this.Relations, false) || string.IsNullOrEmpty(documentName) ? str1 : $"{documentName}. {str1}") : str1;
    }
    return documentsSection;
  }

  private string GetDocRowAttribute(FieldContext context, string fieldName)
  {
    return (context.DocRow ?? this.FindDocRowForProduct(context.ProductIndex))?.GetAttributeValue(fieldName, true) ?? "";
  }

  private void SetDocRowAttribute(FieldContext context, string fieldName, string value)
  {
    (context.DocRow ?? this.FindDocRowForProduct(context.ProductIndex))?.SetAttributeValue(fieldName, value, false, false, false);
  }

  /// <summary>Использовать в примечании только атрибут Предельные значения, даже если есть подборы с заполненным атрибутом "Значение номинала"</summary>
  private bool UseLimitValuesOnly
  {
    get => this.LimitAndNominalValueMode == LimitAndNominalValueMode.UseLimitValuesOnly;
  }

  /// <summary>Получить значение LimitAndNominalValueMode, хранимое в узле документа</summary>
  /// <param name="docNode">Узел документа</param>
  /// <returns></returns>
  internal static LimitAndNominalValueMode? GetLimitAndNominalValueMode(DocumentTreeNode docNode)
  {
    LimitAndNominalValueMode? nominalValueMode = new LimitAndNominalValueMode?();
    if (docNode != null)
    {
      string attributeValue = docNode.GetAttributeValue("LimitAndNominalValueMode", true);
      LimitAndNominalValueMode result;
      if (attributeValue != "" && Enum.TryParse<LimitAndNominalValueMode>(attributeValue, out result))
        nominalValueMode = new LimitAndNominalValueMode?(result);
    }
    return nominalValueMode;
  }

  /// <summary>Режим вывода Предельных значений и Значений номинала</summary>
  [DisplayName("Режим вывода Значений номинала и Предельных значений")]
  [Description("Режим вывода атрибутов \"Значение номинала\" и \"Предельные значения\" в графе \"Примечание\"")]
  [Category("Подборы")]
  public LimitAndNominalValueMode LimitAndNominalValueMode
  {
    get
    {
      LimitAndNominalValueMode? nullable = AVSRow.GetLimitAndNominalValueMode((DocumentTreeNode) this.DocNode);
      if (!nullable.HasValue)
        nullable = new LimitAndNominalValueMode?(this.avsDocument.LimitAndNominalValueModeForNote);
      return nullable.Value;
    }
    set
    {
      if (this.LimitAndNominalValueMode == value || !this.HasDocNodes)
        return;
      this.DocNode.SetAttributeValue(nameof (LimitAndNominalValueMode), value.ToString(), false, false, false);
      this.UpdateNoteDocCellText(true);
    }
  }

  private string GetNominalAndLimitValues(
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList,
    bool useLimitValuesOnly)
  {
    string nominalAndLimitValues = "";
    if (!useLimitValuesOnly)
      nominalAndLimitValues = this.GetNominalValuesForNote(relationIndex, productIndex, relationList);
    if (string.IsNullOrEmpty(nominalAndLimitValues))
      nominalAndLimitValues = this.GetLimintValuesForNote(relationIndex, productIndex, relationList);
    return nominalAndLimitValues;
  }

  private string GetLimintValuesForNote(
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    string limintValuesForNote = "";
    if (relationIndex != -1 && relationIndex < relationList.Count)
      limintValuesForNote = relationList[relationIndex].GetValueString(this.avsDocument.Attr_LimitValues, false);
    if (!string.IsNullOrEmpty(limintValuesForNote) && this.LimitAndNominalValueMode != LimitAndNominalValueMode.UseLimitValuesOnly)
    {
      List<MeasuredValue> valueList = new List<MeasuredValue>();
      string[] strArray1 = limintValuesForNote.Split(new string[1]
      {
        ", "
      }, StringSplitOptions.None);
      string[] separator = new string[2]{ "-", "..." };
      foreach (string str in strArray1)
      {
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray2 = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
          if (strArray2.Length != 0)
          {
            MeasuredValue measuredValue1 = AVSRow.ConvertToMeasuredValue((object) strArray2[strArray2.Length - 1], exceptionIfFail: false);
            MeasureDescriptor defaultMeasure = (MeasureDescriptor) null;
            if (measuredValue1 != null)
              MeasureHelper.FindDescriptor(measuredValue1.MeasureID);
            for (int index = 0; index < strArray2.Length - 1; ++index)
            {
              if (!string.IsNullOrEmpty(strArray2[index]))
              {
                MeasuredValue measuredValue2 = AVSRow.ConvertToMeasuredValue((object) strArray2[index].Trim(), defaultMeasure, false);
                if (measuredValue2 != null)
                  valueList.Add(measuredValue2);
              }
            }
            if (measuredValue1 != null)
              valueList.Add(measuredValue1);
          }
        }
      }
      limintValuesForNote = this.ConvertMeasuredValueListToString(valueList, this.LimitAndNominalValueMode);
    }
    return limintValuesForNote;
  }

  private string GetNominalValuesForNote(
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    long num = -1;
    if (relationIndex != -1 && relationIndex < relationList.Count)
      num = relationList[relationIndex].ProjectId;
    if (num == -1L && productIndex != -1)
    {
      ProductInfo productInfoByIndex = this.avsDocument.GetProductInfoByIndex(productIndex);
      if (productInfoByIndex != null)
        num = productInfoByIndex.Id;
    }
    List<PosDesignationRecord> positionalDesignation = PosDesignationRecord.ParsePositionalDesignation(this.GetFieldStringValue(this.Field_PosDesignation, relationIndex, productIndex, relationList, false));
    List<AttributeValuesCache> attributeValuesCacheList1 = new List<AttributeValuesCache>();
    foreach (PosDesignationRecord designationRecord in positionalDesignation)
    {
      List<RelationAttributeValuesCache> attributeValuesCacheList2;
      if (this.avsDocument.PodborForPosDesignation_Dictionary.TryGetValue(designationRecord.Designation, out attributeValuesCacheList2))
      {
        foreach (RelationAttributeValuesCache attributeValuesCache in attributeValuesCacheList2)
        {
          if (attributeValuesCache.ProjectId == num && !attributeValuesCacheList1.Contains(attributeValuesCache.ObjectAttributesCache))
            attributeValuesCacheList1.Add(attributeValuesCache.ObjectAttributesCache);
        }
      }
    }
    List<MeasuredValue> valueList = new List<MeasuredValue>();
    foreach (AttributeValuesCache attributeValuesCache in attributeValuesCacheList1)
    {
      MeasuredValue measuredValue = AVSRow.ConvertToMeasuredValue(attributeValuesCache.GetValue(this.avsDocument.Attr_NominalValue, false));
      if (measuredValue != null)
        valueList.Add(measuredValue);
    }
    return this.ConvertMeasuredValueListToString(valueList, this.LimitAndNominalValueMode);
  }

  private string ConvertMeasuredValueListToString(
    List<MeasuredValue> valueList,
    LimitAndNominalValueMode mode)
  {
    string str = "";
    if (valueList.Count > 0)
    {
      valueList.Sort((IComparer<MeasuredValue>) new MeasuredValueComparer());
      switch (mode)
      {
        case LimitAndNominalValueMode.Range:
          str = valueList.Count != 1 ? $"{valueList[0].Caption} - {valueList[valueList.Count - 1].Caption}" : valueList[0].Caption;
          break;
        case LimitAndNominalValueMode.List:
          using (List<MeasuredValue>.Enumerator enumerator = valueList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              MeasuredValue current = enumerator.Current;
              str = !(str == "") ? $"{str}, {current.Caption}" : current.Caption;
            }
            break;
          }
      }
    }
    return str;
  }

  /// <summary>Запись является основным компонентом для подбора</summary>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="relationList">Список связей</param>
  internal bool IsBaseComponentForPodbor(
    int relationIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    return this.HasRelation && this.RelType == AvsIDCache.Relation_Project && this.GetFieldBoolValue(this.avsDocument.Attr_Podbor, relationIndex, -1, relationList, true);
  }

  /// <summary>Получить родительский подраздел исполнения переменных данных формы А</summary>
  /// <returns></returns>
  internal ProductVariableDataChapter GetParentProductChapter()
  {
    Chapter chapter = (Chapter) this.section;
    ProductVariableDataChapter parentProductChapter;
    for (parentProductChapter = (ProductVariableDataChapter) null; parentProductChapter == null && chapter != null; chapter = chapter.Parent)
      parentProductChapter = chapter as ProductVariableDataChapter;
    return parentProductChapter;
  }

  /// <summary>Запись может принимать связи только с одним исполнением. Т.е. находится в переменных данных формы А</summary>
  [Browsable(false)]
  public bool IsOneProductRow
  {
    get
    {
      return this.avsDocument != null && this.avsDocument.IsFormA && this.GetParentProductChapter() != null;
    }
  }

  /// <summary>Можно ли добавлять данную связь в эту запись</summary>
  /// <returns></returns>
  internal bool IsAllowableRelation(
    RelationAttributeValuesCache relation,
    bool forAddToRow = true,
    bool notHiddenOnly = false,
    bool? oneProductRowOnly = null)
  {
    if (this.ObjGuid == Guid.Empty || this.RelGuid == relation.RelationGuid)
      return true;
    if (this.ObjGuid != relation.ObjectGuid)
      return false;
    if (!this.HasRelation)
      return true;
    if (this.IsHiddenRow != this.avsDocument.IsHiddenRowRelation(relation) || this.avsDocument.IsSpecification && this.GetFieldStringValue(this.Field_Position, 0, -1, (List<RelationAttributeValuesCache>) null, false, true) != relation.GetValueString(this.Field_Position, false))
      return false;
    int relationIndexForProduct = this.GetRelationIndexForProduct(relation.ProjectId, this.relations);
    if (relationIndexForProduct != -1 && relation.RelationId == this.relations[relationIndexForProduct].RelationId || this.HasHiddenRelation && this.HiddenRelations.Contains<RelationAttributeValuesCache>((Predicate<RelationAttributeValuesCache>) (r => r.RelationId == relation.RelationId)))
      return true;
    if (!this.avsDocument.IsSpecification || relationIndexForProduct != -1)
    {
      if ((notHiddenOnly ? 0 : (forAddToRow ? (this.IsAllowableForHidden(relation) ? 1 : 0) : (this.CheckRelation_IsHiddenRelation(relation) ? 1 : 0))) != 0)
        return true;
      if (relationIndexForProduct != -1)
        return false;
    }
    return this.avsDocument.ProductsInfo.Count != 1 && (oneProductRowOnly.HasValue ? (oneProductRowOnly.Value ? 1 : 0) : (this.IsOneProductRow ? 1 : 0)) == 0 && this.IsAllowableRelationByNoteField(relation);
  }

  /// <summary>
  /// Проверить что все атрибуты связей в примечаниях совпадают, кроме поз.обозначения и допзамен
  /// </summary>
  /// <returns></returns>
  private bool IsAllowableRelationByNoteField(RelationAttributeValuesCache relation)
  {
    IEnumerable<AvsRowAttributeInfo> rowAttributeInfos1 = this.NoteCellMapping?.Attributes;
    if (rowAttributeInfos1 == null)
    {
      AVSDocument avsDocument = this.avsDocument;
      if (avsDocument == null)
      {
        rowAttributeInfos1 = (IEnumerable<AvsRowAttributeInfo>) null;
      }
      else
      {
        NoteFieldSettings noteFieldSettings = avsDocument.noteFieldSettings;
        rowAttributeInfos1 = noteFieldSettings != null ? noteFieldSettings.Items.Select<RemarkAttribute, AvsRowAttributeInfo>((Func<RemarkAttribute, AvsRowAttributeInfo>) (ra => ra.CreateRowAttrInfo())) : (IEnumerable<AvsRowAttributeInfo>) null;
      }
    }
    IEnumerable<AvsRowAttributeInfo> rowAttributeInfos2 = rowAttributeInfos1;
    if (rowAttributeInfos2 == null)
      return true;
    foreach (AvsRowAttributeInfo attr in rowAttributeInfos2)
    {
      if (attr.IsRelationAttribute && attr.AttributeId != this.Field_PosDesignation.AttributeId && attr.AttributeId != AvsIDCache.Attr_DopZamenText)
      {
        string valueString1 = relation.GetValueString(attr, false);
        foreach (AttributeValuesCache relation1 in this.Relations)
        {
          string valueString2 = relation1.GetValueString(attr, false);
          if (valueString1 != valueString2)
            return false;
        }
      }
    }
    return true;
  }

  /// <summary>Получить символ, который вставляется после поз.обозначения подборных компонент</summary>
  /// <returns></returns>
  private string GetSymbolAfterPosDesignation()
  {
    string afterPosDesignation = "*";
    if (AvsConfig.Podbor.SymbolAfterPosDesignationGetFromCAD)
    {
      string str = Convert.ToString(this.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_SymbolForPosDesignation), -1, -1, true, false));
      if (!string.IsNullOrEmpty(str))
        afterPosDesignation = str;
    }
    return afterPosDesignation;
  }

  /// <summary>Получить перекрытое значение атрибута "Позиционное обозначение"</summary>
  /// <param name="rowAttribute">Атрибут для позиционного обозначения.
  /// "Позиционное обозначение" или "Подбор для позиционного обозначения"</param>
  /// <param name="found">Возвращает true, если этот атрибут был перекрыт</param>
  /// <returns></returns>
  private string GetOverridedPositionDesignation(AvsRowAttributeInfo rowAttribute, out bool found)
  {
    found = true;
    return this.GetPosDesignationForNoteField(rowAttribute);
  }

  /// <summary>Получить значение атрибута "Позиционное обозначение" с дополнительным символом согласно настройкам</summary>
  /// <param name="rowAttribute">Атрибут для позиционного обозначения.
  /// "Позиционное обозначение" или "Подбор для позиционного обозначения"</param>
  /// <param name="found">Возвращает true, если этот атрибут был перекрыт</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="relationList">Список связей</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <returns></returns>
  internal string GetPositionDesignationWithAdditionalSymbol(
    AvsRowAttributeInfo rowAttribute,
    out bool found,
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList,
    bool failIfNotFound)
  {
    string additionalSymbol = Convert.ToString(this.GetFieldValue(rowAttribute, relationIndex, productIndex, relationList, true, failIfNotFound, true));
    if (!string.IsNullOrEmpty(additionalSymbol) && this.avsDocument.InsertStarAfterPositionDesignation && this.IsBaseComponentForPodbor(relationIndex, relationList))
      additionalSymbol += this.GetSymbolAfterPosDesignation();
    found = true;
    return additionalSymbol;
  }

  /// <summary>Постобработка полученного значения атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="value">Полученное значение</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен</param>
  /// <returns></returns>
  private object PostProcessFieldValue(
    AvsRowAttributeInfo attrInfo,
    object value,
    bool originalValue = false)
  {
    object obj1 = value;
    if (!originalValue && attrInfo.AttributeId != -1)
    {
      switch (value)
      {
        case null:
        case DBNull _:
          break;
        default:
          Dictionary<object, object> collection = (Dictionary<object, object>) null;
          if (!this.avsDocument.AttributeDescriptionsCache.TryGetValue(attrInfo.AttributeId, out collection))
          {
            IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrInfo.AttributeId);
            if (attributeType != null && attributeType.PossibleValues != null && attributeType.PossibleValues.Count > 0)
            {
              collection = new Dictionary<object, object>();
              for (int index = 0; index < attributeType.PossibleValues.Count; ++index)
                collection.Add(attributeType.PossibleValues[index], attributeType.PossibleValuesDescriptions[index]);
            }
            this.avsDocument.AttributeDescriptionsCache.Add(attrInfo.AttributeId, collection);
          }
          if (!collection.IsNullOrEmpty<KeyValuePair<object, object>>())
          {
            object key = value;
            if (key.GetType() != collection.Keys.First<object>().GetType())
              key = this.NormalizeDBValueTypeForCompareWithPossibleValues(attrInfo, value);
            object obj2;
            if (collection.TryGetValue(key, out obj2) && !string.IsNullOrEmpty(Convert.ToString(obj2)))
            {
              obj1 = obj2;
              break;
            }
            break;
          }
          break;
      }
    }
    if (attrInfo.AttributeId == AvsIDCache.Attr_Podbor)
    {
      if (value == null || value == DBNull.Value)
        obj1 = (object) 0;
      else if (!(value is int))
      {
        string str = Convert.ToString(value);
        obj1 = str == "" || str == "0" || str == "False" ? (object) 0 : (object) 1;
      }
    }
    return obj1;
  }

  private object NormalizeDBValueTypeForCompareWithPossibleValues(
    AvsRowAttributeInfo attrInfo,
    object value)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if (value == null || value is DBNull)
      return (object) null;
    switch (attrInfo.FieldType)
    {
      case FieldTypes.ftInteger:
        return (object) AvsIDCache.ConvertDbValueToInt64(value);
      case FieldTypes.ftDouble:
        return (object) Convert.ToDouble(value);
      case FieldTypes.ftDateTime:
        return (object) Convert.ToDateTime(value);
      case FieldTypes.ftObjectLink:
        return (object) AvsIDCache.ConvertDbValueToInt64(value);
      case FieldTypes.ftMeasured:
        return (object) AVSRow.ConvertToMeasuredValue(value).Caption;
      default:
        return value;
    }
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="relationList">Список связей</param>
  /// <param name="replaceDBNull">Заменять DBNull на null</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <param name="ignoreCellValue"> Не пытаться взять значение из ячейки документа</param>
  /// <returns>Значение атрибута</returns>
  public object GetFieldValue(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList,
    bool replaceDBNull,
    bool failIfNotFound,
    bool originalValue = false,
    bool ignoreCellValue = false)
  {
    if (relationIndex == -1 && productIndex == -1)
      relationIndex = 0;
    FieldContext context = new FieldContext(this, relationIndex, productIndex, relationList);
    return this.GetFieldValue(attrInfo, context, failIfNotFound, originalValue, ignoreCellValue);
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="context">Контекст получения поля</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <param name="ignoreCellValue"> Не пытаться взять значение из ячейки документа</param>
  /// <returns>Значение атрибута</returns>
  internal object GetFieldValue(
    AvsRowAttributeInfo attrInfo,
    FieldContext context,
    bool failIfNotFound,
    bool originalValue = false,
    bool ignoreCellValue = false)
  {
    TextData cell = (TextData) null;
    bool found = false;
    if (context == null)
      context = new FieldContext(this, 0, -1, this.Relations);
    object obj = this.GetOverridedFieldValue(attrInfo, out found, context, true, failIfNotFound, originalValue);
    if (!found)
    {
      int index = this.UpdateAttrValueIndex(attrInfo, false);
      if (attrInfo.IsRelationAttribute && attrInfo.IndexInValueList != -1)
      {
        found = true;
        obj = context.Relation?.GetValue(attrInfo, false, true);
      }
      else if (attrInfo.IsObjectAttribute && attrInfo.IndexInValueList != -1)
      {
        found = true;
        obj = this.ObjectAttributesCache.GetValue(attrInfo, false, true);
      }
      else if (this.DocNode != null)
      {
        bool flag1 = false;
        bool flag2 = this._hasNoteAndNoteAttributeCollision && attrInfo.Equals((AttributeInfo) this.Field_Note);
        if (ignoreCellValue & flag2 && !this.DocNode.ContainsAttribute(AVSRow.DocAttr_Note) && !this.DocNode.ContainsAttribute(AVSRow.DocAttr_NotePE))
          flag1 = true;
        string str = (string) null;
        if (string.IsNullOrEmpty(attrInfo.Name))
          attrInfo.UpdateName();
        if (this.DocNode.ContainsAttribute(attrInfo.Name))
        {
          str = this.DocNode.GetAttributeValue(attrInfo.Name, false);
        }
        else
        {
          if (!AVSRow.IsCountAttribute(attrInfo) && attrInfo.AttrSrc == FieldSource.DocumentRowField && index != -1 && index < this.DocNode.Nodes.Count)
            cell = context.DocCell ?? this.DocNode.Nodes[index] as TextData;
          else if ((flag1 || !ignoreCellValue) && (attrInfo.Name != AVSRow.DocAttr_Note || !this.DocNode.ContainsAttribute(this.Field_Note.Name)))
            cell = context.DocCell ?? this.GetDocumentCellForAttribute(attrInfo, context.ProductIndex);
          if (cell != null)
            str = this.GetOldBaseFieldValueFromDocCell(cell, attrInfo);
        }
        obj = (object) str;
        found = true;
      }
    }
    if (!found & failIfNotFound)
    {
      if (attrInfo.IsRelationAttribute && this.HasRelation)
        throw new Exception($"Атрибут {attrInfo.AttributeId.ToString()} связи не найден!");
      if (attrInfo.IsObjectAttribute && this.HasObject)
        throw new Exception($"Атрибут {attrInfo.AttributeId.ToString()} объекта не найден!");
    }
    return this.PostProcessFieldValue(attrInfo, obj, originalValue);
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="replaceDBNull">Заменять DBNull на null</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <returns>Значение атрибута</returns>
  public object GetFieldValue(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex,
    bool replaceDBNull,
    bool failIfNotFound,
    bool originalValue = false)
  {
    return this.GetFieldValue(attrInfo, relationIndex, productIndex, (List<RelationAttributeValuesCache>) null, replaceDBNull, failIfNotFound, originalValue);
  }

  /// <summary>Текстовое значение атрибута</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="relationList">Список связей</param>
  /// <param name="failIfNotFound">Выдавать исключение, если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <param name="ignoreCellValue"> Не пытаться взять значение из ячейки документа</param>
  /// <returns>Возвращает текстовое значение атрибута. Если значение null, то возвращает пустую строку</returns>
  public string GetFieldStringValue(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList,
    bool failIfNotFound,
    bool originalValue = false,
    bool ignoreCellValue = false)
  {
    if (attrInfo.IsRelationAttribute && relationIndex == -1 && productIndex == -1)
      relationIndex = 0;
    FieldContext context = new FieldContext(this, relationIndex, productIndex, relationList);
    return this.GetFieldStringValue(attrInfo, context, failIfNotFound, originalValue, ignoreCellValue);
  }

  /// <summary>Текстовое значение атрибута</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="context">Контекст получения поля</param>
  /// <param name="failIfNotFound">Выдавать исключение, если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <param name="ignoreCellValue"> Не пытаться взять значение из ячейки документа</param>
  /// <returns>Возвращает текстовое значение атрибута. Если значение null, то возвращает пустую строку</returns>
  internal string GetFieldStringValue(
    AvsRowAttributeInfo attrInfo,
    FieldContext context,
    bool failIfNotFound,
    bool originalValue = false,
    bool ignoreCellValue = false)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if (context == null)
      context = new FieldContext(this, 0, -1, this.Relations);
    string fieldStringValue = "";
    bool flag = AVSRow.IsCountAttribute(attrInfo);
    if (flag && this.IsDocRelation)
    {
      if (this.IsFormB && context.Relation != null)
        fieldStringValue = "X";
      if (this.IsFormB || this.HideCountForDocuments)
        return fieldStringValue;
    }
    object fieldValue = this.GetFieldValue(attrInfo, context, failIfNotFound, originalValue, ignoreCellValue);
    string str = fieldValue as string;
    if (fieldValue != null && str == null)
    {
      if (fieldValue is MeasuredValue countValue)
      {
        if (flag)
        {
          TextData cellForAttribute = this.GetDocumentCellForAttribute(attrInfo, context.ProductIndex);
          str = this.ConvertMeasuredValueCountToString(countValue, cellForAttribute);
        }
        else
        {
          str = countValue.ToString();
          if (string.IsNullOrEmpty(str))
            str = MeasureHelper.ConvertToString(countValue.Value, countValue.MeasureID, true);
        }
      }
      else
        str = attrInfo.FieldType != FieldTypes.ftBoolean ? fieldValue.ToString() : new CustomBooleanConverter().ConvertToString((object) Convert.ToBoolean(fieldValue));
    }
    return str ?? "";
  }

  private string ConvertMeasuredValueCountToString(MeasuredValue countValue, TextData cell)
  {
    if (countValue == null)
      return "";
    string source = countValue.ToString();
    if (string.IsNullOrEmpty(source))
      source = MeasureHelper.ConvertToString(countValue.Value, countValue.MeasureID, true);
    if (source.Contains<char>('/'))
      return source;
    string valueFromDocCell = AVSRow.GetFieldValueFromDocCell(cell);
    if (!string.IsNullOrEmpty(valueFromDocCell) && valueFromDocCell.Contains<char>('/') && MeasureHelper.Compare(AVSRow.ConvertCountToMeasuredValue((object) valueFromDocCell, false), countValue) == CompareResult.Equal)
      source = valueFromDocCell;
    return source;
  }

  /// <summary>Булевское значение атрибута</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="relationList">Список связей</param>
  /// <param name="failIfNotFound">Выдавать исключение, если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен</param>
  /// <param name="defaultValue">Значение по умолчанию</param>
  /// <returns>Возвращает текстовое значение атрибута. Если значение null, то возвращает пустую строку</returns>
  public bool GetFieldBoolValue(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList,
    bool failIfNotFound,
    bool originalValue = false,
    bool defaultValue = false)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    return AttributeValuesCache.ConvertToBool(this.GetFieldValue(attrInfo, relationIndex, productIndex, relationList, true, failIfNotFound, originalValue), defaultValue);
  }

  /// <summary>Получить значение атрибута для ячейки документа</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="originalValue">Значение в кэше атрибутов БД</param>
  /// <param name="isCellViewValue">Получить значение для отображения в неактивной ячейке</param>
  /// <param name="failIfNotFound">Выдавать исключение, если атрибут не найден</param>
  /// <returns></returns>
  public virtual string ConvertFieldValueForDocCell(
    AvsRowAttributeInfo attrInfo,
    string originalValue,
    bool isCellViewValue,
    bool failIfNotFound)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if (string.IsNullOrEmpty(originalValue) || !AVSRow.IsCountAttribute(attrInfo))
      return originalValue;
    if (this.IsFormB && this.IsDocRelation)
      return "X";
    if (isCellViewValue && this.ShowMeasureUnitsInNote)
    {
      MeasureDescriptor md = (MeasureDescriptor) null;
      string countMeasure = (string) null;
      originalValue = this.ConvertCountToValueAndMeasure((object) originalValue, ref md, out countMeasure);
    }
    else
    {
      int length = originalValue.IndexOf(AVSRow.DefaultCountMeasure.ShortName);
      if (length != -1)
        originalValue = originalValue.Substring(0, length).Trim();
    }
    return originalValue;
  }

  /// <summary>Получить значение количества для ячейки документа</summary>
  /// <param name="originalValue">Значение количества</param>
  /// <param name="cell">Ячейка количества</param>
  /// <param name="cellEditValue">Возвращает значение для редактирования в активной ячейке</param>
  /// <param name="countMeasure">Возвращает единицы измерения</param>
  /// <returns>Возвращает значение для отображения в неактивной ячейке</returns>
  private string ConvertCountToStringForDocCell(
    MeasuredValue originalValue,
    TextData cell,
    bool showMeasureUnitsInNote,
    out string cellEditValue,
    out string countMeasure)
  {
    countMeasure = "";
    cellEditValue = "";
    if (originalValue == null)
      return "";
    if (this.IsFormB && this.IsDocRelation)
      return originalValue.Value == 0.0 ? "" : "X";
    cellEditValue = this.ConvertMeasuredValueCountToString(originalValue, cell);
    string stringForDocCell;
    if (originalValue.MeasureID == AVSRow.DefaultCountID)
    {
      countMeasure = "";
      cellEditValue = cellEditValue.Replace(AVSRow.DefaultCountMeasure.ShortName, "").Trim();
      stringForDocCell = cellEditValue;
    }
    else if (showMeasureUnitsInNote)
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(originalValue.MeasureID);
      stringForDocCell = this.ConvertCountToValueAndMeasure((object) cellEditValue, ref descriptor, out countMeasure);
    }
    else
      stringForDocCell = cellEditValue;
    return stringForDocCell;
  }

  /// <summary>Получить текст для ячейки документа согласно настройкам вывода атрибутов в графу</summary>
  /// <param name="cellMapping">Настройки сбора значения графы</param>
  /// <param name="baseField">Идентификатор атрибута</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="isCellViewValue">Получить значение для отображения в неактивной ячейке</param>
  /// <param name="failIfNotFound">Выдавать исключение, если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <param name="ignoreCellValue"> Не пытаться взять значение из ячейки документа</param>
  /// <returns></returns>
  public virtual string GetTextForDocCell(
    CellOutputMapping cellMapping,
    AvsRowAttributeInfo baseField,
    int relationIndex,
    int productIndex,
    bool isCellViewValue,
    bool failIfNotFound,
    bool originalValue = false,
    bool ignoreCellValue = false)
  {
    return cellMapping == null ? this.GetFieldStringValue(baseField, relationIndex, productIndex, (List<RelationAttributeValuesCache>) null, failIfNotFound, originalValue, ignoreCellValue) : cellMapping.ConcatenateAttributesValues((Intermech.AVS.GetFieldValueByCellOutputMapping) (attrMapping => this.GetFieldValueForDocCell(new AvsRowAttributeInfo(attrMapping.AttributeInfo), relationIndex, productIndex, isCellViewValue, failIfNotFound, originalValue, ignoreCellValue)));
  }

  /// <summary>Получить значение атрибута для ячейки документа</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="isCellViewValue">Получить значение для отображения в неактивной ячейке</param>
  /// <param name="failIfNotFound">Выдавать исключение, если атрибут не найден</param>
  /// <param name="originalValue">Получать оригинальные значения без автоматических подмен (наименование)</param>
  /// <param name="ignoreCellValue"> Не пытаться взять значение из ячейки документа</param>
  /// <returns></returns>
  public virtual string GetFieldValueForDocCell(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex,
    bool isCellViewValue,
    bool failIfNotFound,
    bool originalValue = false,
    bool ignoreCellValue = false)
  {
    string fieldStringValue = this.GetFieldStringValue(attrInfo, relationIndex, productIndex, (List<RelationAttributeValuesCache>) null, failIfNotFound, originalValue, ignoreCellValue);
    return this.ConvertFieldValueForDocCell(attrInfo, fieldStringValue, isCellViewValue, failIfNotFound);
  }

  /// <summary>Получить значение атрибута в текстовой ячейке</summary>
  /// <param name="docRow">Строка документа</param>
  /// <param name="baseFieldInfo">Информация об атрибуте</param>
  /// <param name="productIndex">Индекс исполнения для количества. Если -1, то для всех исполнений</param>
  /// <returns></returns>
  internal string GetFieldValueFromDocCell(
    TableData docRow,
    AvsRowAttributeInfo baseFieldInfo,
    int productIndex = -1)
  {
    return AVSRow.GetFieldValueFromDocCell(this.GetDocumentCellForBaseField(baseFieldInfo, docRow, productIndex));
  }

  /// <summary>Получить значение атрибута в текстовой ячейке</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <returns></returns>
  internal string GetOldBaseFieldValueFromDocCell(TextData cell, AvsRowAttributeInfo baseFieldInfo)
  {
    if (cell == null)
      return (string) null;
    string text = AVSRow.GetFieldValueFromDocCell(cell);
    if (this.avsDocument.IsNoteField(baseFieldInfo) && cell is TextBoxElement cell1 && !AVSRow.ExtractTextBetweenProtectedZones(cell1, out text))
      text = (string) null;
    return text;
  }

  /// <summary>Получить значение атрибута в текстовой ячейке</summary>
  /// <param name="cell">Ячейка</param>
  /// <returns></returns>
  internal static string GetFieldValueFromDocCell(TextData cell)
  {
    if (cell == null)
      return (string) null;
    string str = !(cell is TextBoxElement textBoxElement) || !textBoxElement.InPlaceEditorActive ? cell.Text : textBoxElement.GetActiveEditorText();
    return cell.ContainsAttribute(AVSRow.CellAttrName_EditText) ? cell.GetAttributeValue(AVSRow.CellAttrName_EditText, true) : str;
  }

  /// <summary>Сравнение значения атрибутов. Учитывает null и DBNull</summary>
  /// <param name="value1">Первое значение</param>
  /// <param name="value2">Второе значение</param>
  /// <returns></returns>
  protected bool AttrValuesIsEqual(object value1, object value2)
  {
    if (value1 is DBNull)
      value1 = (object) null;
    if (value2 is DBNull)
      value2 = (object) null;
    if (value1 == null && value2 == null)
      return true;
    return value1 != null && value1.Equals(value2);
  }

  /// <summary>Поля отображаемые в документе</summary>
  [Browsable(false)]
  public List<AvsRowAttributeInfo> DocRowFields
  {
    [DebuggerStepThrough] get
    {
      if (this.section != null)
        return this.section.DocRowFields;
      return this.avsDocument != null ? this.avsDocument.docRowFields : new List<AvsRowAttributeInfo>();
    }
  }

  /// <summary>Поля отображаемые в экспортном документе</summary>
  [Browsable(false)]
  public List<AvsRowAttributeInfo> DocRowFields_Exp
  {
    [DebuggerStepThrough] get
    {
      if (this.section != null)
        return this.section.DocRowFields_Exp;
      return this.avsDocument != null ? this.avsDocument.docRowFields_Exp : new List<AvsRowAttributeInfo>();
    }
  }

  /// <summary>Для внутреннего использования. Обновить значение индекса атрибута в кэше атрибутов</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="findDocCellForAttr">Сравнение в контексте поиска ячейки документа для атрибута</param>
  /// <returns>Индекс атрибута</returns>
  protected int UpdateAttrValueIndex(AvsRowAttributeInfo attrInfo, bool findDocCellForAttr)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if (this.avsDocument == null)
    {
      attrInfo.IndexInValueList = -1;
      return attrInfo.IndexInValueList;
    }
    if (attrInfo.AttrSrc == FieldSource.DocumentRowField)
    {
      if (this.docNode == null)
      {
        attrInfo.IndexInValueList = -1;
      }
      else
      {
        List<AvsRowAttributeInfo> docRowFields = this.DocRowFields;
        if (docRowFields == null)
          return attrInfo.IndexInValueList = -1;
        if (attrInfo.IndexInValueList == -1 || attrInfo.IndexInValueList >= docRowFields.Count || !attrInfo.EqualAttrs(docRowFields[attrInfo.IndexInValueList], findDocCellForAttr))
        {
          for (int index = 0; index < docRowFields.Count; ++index)
          {
            if (attrInfo.EqualAttrs(docRowFields[index], findDocCellForAttr))
              return attrInfo.IndexInValueList = index;
          }
          attrInfo.IndexInValueList = -1;
        }
      }
    }
    else
    {
      AttributeValueMap attributeValueMap = !attrInfo.IsRelationAttribute ? (AttributeValueMap) this.ObjectAttributesCache : (!this.HasRelation ? (!this.HasHiddenRelation ? (AttributeValueMap) null : (AttributeValueMap) this.hiddenRelations[0]) : (AttributeValueMap) this.relations[0]);
      attrInfo.IndexInValueList = attributeValueMap == null ? -1 : attributeValueMap.GetUpdatedValueIndex(attrInfo.AttributeId, attrInfo.IndexInValueList);
    }
    return attrInfo.IndexInValueList;
  }

  /// <summary>Установить значение атрибута в строке спецификации (в кэше и базе), в документе и табличном виде</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="value">Значение</param>
  /// <param name="saveToDB">Сохранять в БД</param>
  /// <param name="forceSaveDB">Сохранять в БД и наименование</param>
  /// <param name="updateDocNode">Обновить узел документа</param>
  /// <param name="updateListNode">Обновить узел TreeList</param>
  /// <param name="failIfNotFound">Сгенерировать исключение если в строке нет такого атрибута</param>
  /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
  /// <param name="fireNotificationEvent">Генерировать событие для INotificationSevice</param>
  /// <returns>Возвращает true, если изменилось значение в кэше</returns>
  public bool SetFieldValueForAllRelations(
    AvsRowAttributeInfo attrInfo,
    object value,
    bool saveToDB,
    bool forceSaveDB,
    bool updateDocNode,
    bool updateListNode,
    bool failIfNotFound,
    bool exceptionIfFail,
    bool fireNotificationEvent = true)
  {
    int num = this.SetFieldValue(attrInfo, -1, -1, this.relations, value, saveToDB, forceSaveDB, updateDocNode, updateListNode, failIfNotFound, exceptionIfFail, fireNotificationEvent) ? 1 : 0;
    if (!this.HasHiddenRelation)
      return num != 0;
    this.SetFieldValue(attrInfo, -1, -1, this.HiddenRelations, value, saveToDB, forceSaveDB, false, false, failIfNotFound, exceptionIfFail, fireNotificationEvent);
    return num != 0;
  }

  /// <summary>Установить значение атрибута в строке спецификации (в кэше и базе), в документе и табличном виде</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex.
  /// Если и productIndex -1, то заносится во все связи</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex.
  /// Если и relationIndex -1, то заносится во все связи</param>
  /// <param name="value">Значение</param>
  /// <param name="saveToDB">Сохранять в БД</param>
  /// <param name="forceSaveDB">Сохранять в БД и наименование</param>
  /// <param name="updateDocNode">Обновить узел документа</param>
  /// <param name="updateListNode">Обновить узел TreeList</param>
  /// <param name="failIfNotFound">Сгенерировать исключение если в строке нет такого атрибута</param>
  /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
  /// <param name="fireNotificationEvent">Генерировать событие для INotificationSevice</param>
  /// <param name="originalAttribute">Сохранять для оригинальных атрибутов, без подмен</param>
  /// <returns>Возвращает true, если изменилось значение в кэше</returns>
  public bool SetFieldValue(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex,
    object value,
    bool saveToDB,
    bool forceSaveDB,
    bool updateDocNode,
    bool updateListNode,
    bool failIfNotFound,
    bool exceptionIfFail,
    bool fireNotificationEvent = true,
    bool originalAttribute = false)
  {
    return attrInfo.IsRelationAttribute && relationIndex == -1 && productIndex == -1 ? this.SetFieldValueForAllRelations(attrInfo, value, saveToDB, forceSaveDB, updateDocNode, updateListNode, failIfNotFound, exceptionIfFail, fireNotificationEvent) : this.SetFieldValue(attrInfo, relationIndex, productIndex, (List<RelationAttributeValuesCache>) null, value, saveToDB, forceSaveDB, updateDocNode, updateListNode, failIfNotFound, exceptionIfFail, fireNotificationEvent, originalAttribute);
  }

  /// <summary>Установить значение атрибута в строке спецификации (в кэше и базе), в документе и табличном виде</summary>
  /// <param name="attrInfo">Идентификатор атрибута</param>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex.
  /// Если и productIndex -1, то заносится во все связи</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex.
  /// Если и relationIndex -1, то заносится во все связи</param>
  /// <param name="relationList">Список связей</param>
  /// <param name="value">Значение</param>
  /// <param name="saveToDB">Сохранять в БД</param>
  /// <param name="forceSaveDB">Сохранять в БД и наименование</param>
  /// <param name="updateDocNode">Обновить узел документа</param>
  /// <param name="updateListNode">Обновить узел TreeList</param>
  /// <param name="failIfNotFound">Сгенерировать исключение если в строке нет такого атрибута</param>
  /// <param name="exceptionIfFail">Генерировать исключение если конвертировать в Double нельзя</param>
  /// <param name="fireNotificationEvent">Генерировать событие для INotificationSevice</param>
  /// <param name="originalAttribute">Сохранять для оригинальных атрибутов, без подмен</param>
  /// <returns>Возвращает true, если изменилось значение в кэше</returns>
  public bool SetFieldValue(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList,
    object value,
    bool saveToDB,
    bool forceSaveDB,
    bool updateDocNode,
    bool updateListNode,
    bool failIfNotFound,
    bool exceptionIfFail,
    bool fireNotificationEvent = true,
    bool originalAttribute = false)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if (this.avsDocument != null)
      this.avsDocument.SuspendDocumentAndGridUpdates(true, false);
    bool flag1 = false;
    bool notFound = true;
    try
    {
      if (!attrInfo.IsDocField)
      {
        if (!this.IsNoteRow)
        {
          try
          {
            flag1 = this.SetFieldValueToCacheAndDB(attrInfo, relationIndex, relationList, value, saveToDB, forceSaveDB, failIfNotFound, out notFound, fireNotificationEvent, originalAttribute);
            bool flag2 = !flag1 && (attrInfo.IsRelationAttribute && !this.HasRelation || attrInfo.IsObjectAttribute && !this.HasObject);
            updateDocNode |= flag2;
            if (this.HasDocNodes && this.avsDocument != null && !this.avsDocument.newAttributesLoading && attrInfo.Equals((AttributeInfo) this.avsDocument.Attr_SortIndex))
              this.SaveSortIndexInDocRowAttribute(value);
            else if (!flag1 && attrInfo.IsVirtualAttribute && !attrInfo.ReadOnly)
            {
              if (attrInfo.AttributeId == AvsIDCache.Attr_AdditionalNameNote.AttributeId)
                this.SetAdditionalNameNote(new FieldContext(this, relationIndex, productIndex, (List<RelationAttributeValuesCache>) null), Convert.ToString(value));
            }
            else if (flag2 && !this.IsNoteRow)
              this.DocNode?.SetAttributeValue(attrInfo.Name, Convert.ToString(value), false, false, false);
            if (attrInfo.Equals((AttributeInfo) this.Field_Position) & flag1 || !this.HasRelation)
              this.SetCommonPositions(productIndex, relationList, value);
            if (!updateDocNode)
            {
              if (!flag1)
                return flag1;
              goto label_25;
            }
            goto label_25;
          }
          catch
          {
            if (!exceptionIfFail)
              return false;
            throw;
          }
        }
      }
      this.DocNode?.SetAttributeValue(attrInfo.Name, Convert.ToString(value), false, false, false);
label_25:
      if (updateDocNode)
        this.UpdateAttributeInDocCells(attrInfo, relationIndex, productIndex);
      if (updateListNode)
        this.UpdateGridRow();
      SpecificationSection section = this.Section;
      if (section != null & flag1)
      {
        if (!this.avsDocument.IsRowsUpdating)
        {
          if (this.NeedUpdateDocRow && !this.avsDocument.AvsDocumentNowLoading)
            section.UpdateViewNodes(this.avsDocument.skipLinesSchema, false, false, false, false, false, EmptyRowUpdateMode.DontChange);
          this.CheckIsSortedFlag(attrInfo, section);
        }
      }
    }
    finally
    {
      if (this.avsDocument != null)
        this.avsDocument.ResumeDocumentAndGridUpdates(0, flag1 & updateDocNode, flag1 & updateDocNode, true, false);
    }
    return flag1;
  }

  private void CheckIsSortedFlag(AvsRowAttributeInfo attrInfo, SpecificationSection section)
  {
    int index1 = this.Index;
    if (index1 != -1)
    {
      if (attrInfo.Equals((AttributeInfo) this.avsDocument.Attr_SortIndex))
        return;
      bool flag = true;
      int index2 = index1 - 1;
      while (index2 > -1 && !section.Rows[index2].IsSorted)
        --index2;
      if (index2 >= 0)
        flag = section.Compare(section.Rows[index2], this) <= 0;
      int index3 = index1 + 1;
      while (index3 < section.Rows.Count && !section.Rows[index3].IsSorted)
        ++index3;
      if (index3 < section.Rows.Count)
        flag &= section.Compare(this, section.Rows[index3]) <= 0;
      this.IsSorted = flag;
    }
    else
      this.IsSorted = false;
  }

  private void SaveSortIndexInDocRowAttribute(object value)
  {
    long num = AvsIDCache.ConvertDbValueToInt64(value, 0L);
    if (num == long.MaxValue)
      num = 0L;
    string attributeValue = num.ToString();
    for (int index = 0; index < this.docNodes.Count; ++index)
    {
      if (num != 0L)
        this.docNodes[index].SetAttributeValue(AVSRow.RowAttr_SortIndex, attributeValue);
      else
        this.docNodes[index].RemoveAttribute(AVSRow.RowAttr_SortIndex, false, false);
    }
    if (this.docNodeExp == null)
      return;
    if (num != 0L)
      this.docNodeExp.SetAttributeValue(AVSRow.RowAttr_SortIndex, attributeValue);
    else
      this.docNodeExp.RemoveAttribute(AVSRow.RowAttr_SortIndex, false, false);
  }

  /// <summary>Заменить атрибут "Количество" на "Количество для подбора" если тип связи "Подборный компонент"</summary>
  /// <param name="attrInfo"></param>
  /// <returns></returns>
  private AvsRowAttributeInfo OverrideCountAttributeInPodborRelation(AvsRowAttributeInfo attrInfo)
  {
    AvsRowAttributeInfo rowAttributeInfo = attrInfo != null ? attrInfo : throw new ArgumentNullException(nameof (attrInfo));
    if (attrInfo.IsRelationAttribute && attrInfo.AttributeId == AvsIDCache.Attr_Count && this.RelType == AvsIDCache.Relation_Podbor)
      rowAttributeInfo = this.avsDocument.Attr_CountForAdjustment;
    return rowAttributeInfo;
  }

  internal static bool IsDocumentCountX(string countFieldValue)
  {
    return countFieldValue == "X" || countFieldValue == "x" || countFieldValue == "Х" || countFieldValue == "х";
  }

  /// <summary>Установить значение атрибута. Сохраняет в базу и кэш</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="relationList">Список связей</param>
  /// <param name="value">Значение атрибута</param>
  /// <param name="saveToDB">Сохранять в БД</param>
  /// <param name="forceSaveDB">Сохранять в БД и наименование</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="notFound">Возвращает true, если в кэше нет атрибута этого атрибута или запись не связана с БД</param>
  /// <param name="fireNotificationEvent">Генерировать событие для INotificationSevice</param>
  /// <param name="originalAttribute">Сохранять для оригинальных атрибутов, без подмен</param>
  /// <returns>Возвращает true, если изменилось значение в кэше</returns>
  private bool SetFieldValueToCacheAndDB(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    List<RelationAttributeValuesCache> relationList,
    object value,
    bool saveToDB,
    bool forceSaveDB,
    bool failIfNotFound,
    out bool notFound,
    bool fireNotificationEvent = true,
    bool originalAttribute = false)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if (relationList == null)
      relationList = this.relations;
    if (!originalAttribute)
      attrInfo = this.OverrideCountAttributeInPodborRelation(attrInfo);
    notFound = true;
    if (attrInfo.IsDocField || attrInfo.IsRelationAttribute && (relationList == null || relationList.Count == 0) || attrInfo.IsObjectAttribute && !this.HasObject)
      return false;
    this.UpdateAttrValueIndex(attrInfo, false);
    if (attrInfo.IndexInValueList == -1)
    {
      if (failIfNotFound)
        throw new Exception($"Атрибут {attrInfo.AttributeId.ToString()}{(attrInfo.IsRelationAttribute ? " связи " : " объекта ")} не найден!");
      return false;
    }
    notFound = false;
    bool cacheAndDb = false;
    if (AVSRow.IsCountAttribute(attrInfo))
      value = (object) AVSRow.ConvertCountToMeasuredValue(value);
    else if (AvsIDCache.DopZamenTextAttrInfo.Equals((AttributeInfo) attrInfo))
    {
      if (value is "")
        value = (object) null;
    }
    else
    {
      if (saveToDB && !this.IsDocRelation && (attrInfo.AttributeId == AvsIDCache.Attr_Format || attrInfo.AttributeId == AvsIDCache.Attr_FirstApplicability))
        saveToDB = false;
      if (saveToDB && !forceSaveDB && this.Field_Name.Equals((AttributeInfo) attrInfo))
        saveToDB = false;
    }
    if (attrInfo.IsObjectAttribute && attrInfo.AttributeId == -2)
    {
      this.rowID.ObjectID = Convert.ToInt64(value);
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (this.docNodes[index].Reference is ReferenceToDBObject reference && reference.DBObjectInfo != null)
          reference.DBObjectInfo.AssignObjectID(this.rowID.ObjectID);
      }
    }
    if (attrInfo.IsObjectAttribute && attrInfo.AttributeId == -7)
    {
      int objectType = this.rowID.ObjectType;
      int int32 = AvsIDCache.ConvertDbValueToInt32(value);
      if (int32 != objectType)
      {
        this.rowID.AssignObjectType(int32);
        SpecificationSectionInfo defaultSectionForType1 = AVSDocument.GetDefaultSectionForType(objectType);
        if (defaultSectionForType1 != null && this.Section != null && defaultSectionForType1.SectionID == this.Section.SectionID)
        {
          this.avsDocument.SuspendDocumentAndGridUpdates();
          try
          {
            Chapter parent = this.Section.Parent;
            SpecificationSectionInfo defaultSectionForType2 = AVSDocument.GetDefaultSectionForType(this.rowID.ObjectType);
            if (!(parent.FindChildChapterByID(defaultSectionForType2.SectionID) is SpecificationSection newSection))
            {
              newSection = this.avsDocument.CreateSection(defaultSectionForType2);
              parent.AddChapter((Chapter) newSection, true, true, this.avsDocument.ViewMode == AVSViewMode.Grid, parent.GetSectionTemplate());
            }
            this.Section.MoveRow(this, newSection, true, this.avsDocument.ViewMode == AVSViewMode.Grid, true);
            this.avsDocument.IndexAVSDocument(true);
          }
          finally
          {
            this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
          }
        }
      }
    }
    if (attrInfo.IsRelationAttribute)
    {
      for (int index = 0; relationList != null && index < relationList.Count && relationIndex < relationList.Count; ++index)
      {
        if (relationIndex != -1)
          index = relationIndex;
        object obj = relationList[index].GetValue(attrInfo, false);
        if (!this.AttrValuesIsEqual(obj, value))
        {
          cacheAndDb = true;
          relationList[index].SetValue(attrInfo, value, true);
          if (AVSRow.IsCountAttribute(attrInfo) && !this.IsFormB)
            this.NeedUpdateStructure = true;
          if (saveToDB && (!this.IsDocRelation || !AVSRow.IsCountAttribute(attrInfo)))
          {
            relationList[index].PersistentAttrs[attrInfo.AttributeId] = true;
            try
            {
              if (value is AVSObjectInfo)
                value = (object) (value as AVSObjectInfo).Id;
              if (AVSRow.IsCountAttribute(attrInfo) && value is MeasuredValue measuredValue)
                value = (object) new MeasuredValue(measuredValue.Value, measuredValue.MeasureID);
              using (SessionKeeper sessionKeeper = new SessionKeeper())
              {
                AttributeValues newAttrValues = new AttributeValues(attrInfo.AttributeId, value);
                if (this.avsDocument.IsAttributeSaveBatchModeEnabled)
                {
                  RelationAttributeValues rav = new RelationAttributeValues(relationList[index].RelationId, relationList[index].ProjectId, new AttributeValues[1]
                  {
                    newAttrValues
                  });
                  this.avsDocument.PendingRelationUpdates.Add(relationList[index].ProjectId, rav);
                }
                else
                  sessionKeeper.Session.GetRelationByPartObjectID(relationList[index].RelationId, this.ObjectId, true).SetAttributesValues(new AttributeValues[1]
                  {
                    newAttrValues
                  });
                object avsDocument = (object) this.avsDocument;
                if (this.avsDocument.suspendReloadDopZamenText == 0 && attrInfo.AttributeId != AvsIDCache.Attr_DopZamenText && this.CheckNeedUpdateDopZamenyText(attrInfo))
                {
                  object fieldValue = this.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenGroupNum), index, -1, relationList, false, false);
                  switch (fieldValue)
                  {
                    case null:
                    case DBNull _:
                      break;
                    default:
                      long result = 0;
                      if (fieldValue is string)
                      {
                        if (!long.TryParse((string) fieldValue, out result))
                          result = 0L;
                      }
                      else
                        result = Convert.ToInt64(fieldValue);
                      if (result != 0L)
                      {
                        this.avsDocument.ReloadDopzamenTextForGroup(new List<long>((IEnumerable<long>) new long[1]
                        {
                          result
                        }), relationList[index].ProjectId, (List<AVSRow>) null, out List<AVSRow> _, true);
                        break;
                      }
                      break;
                  }
                }
                if (fireNotificationEvent)
                {
                  if (AVSPlugin.NotificationService != null)
                    AVSPlugin.NotificationService.FireEvent(avsDocument, (NotificationEventArgs) new DBRelationsExtendedEventArgs(relationList[index].RelationId, relationList[index].RelationType, new AttributeValues(attrInfo.AttributeId, obj), newAttrValues));
                }
              }
            }
            catch
            {
              relationList[index].SetValue(attrInfo, obj, false);
              throw;
            }
          }
        }
        if (relationIndex != -1)
          return cacheAndDb;
      }
    }
    else if (this.ObjectAttributesCache != null)
    {
      object attrValue = this.ObjectAttributesCache.GetValue(attrInfo, false);
      if (!this.AttrValuesIsEqual(attrValue, value))
      {
        cacheAndDb = true;
        this.ObjectAttributesCache.SetValue(attrInfo, value, true);
        if (saveToDB)
        {
          try
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(this.ObjectAttributesCache.ObjectId, true);
              object initValue = value;
              if (attrInfo.FieldType == FieldTypes.ftObjectLink)
                initValue = (object) (value as AVSObjectInfo).Id;
              AttributeValues[] valuesList = new AttributeValues[1]
              {
                new AttributeValues(attrInfo.AttributeId, initValue)
              };
              objectActualCopy.SetAttributesValues(valuesList);
              AVSDocument avsDocument = this.avsDocument;
              if (this.CheckNeedUpdateDopZamenyText(attrInfo))
              {
                string fieldStringValue = this.GetFieldStringValue(this.avsDocument.Attr_DopZamenText, 0, -1, (List<RelationAttributeValuesCache>) null, false);
                if (fieldStringValue != null)
                {
                  int num = fieldStringValue != "" ? 1 : 0;
                }
              }
            }
          }
          catch
          {
            this.ObjectAttributesCache.SetValue(attrInfo, attrValue, false);
            throw;
          }
        }
        foreach (RelationAttributeValuesCache attributeValuesCache in this.AllRelations.Where<RelationAttributeValuesCache>((Func<RelationAttributeValuesCache, bool>) (r => r.ObjectAttributesCache != this.ObjectAttributesCache)))
          attributeValuesCache.ObjectAttributesCache = this.ObjectAttributesCache;
      }
      return cacheAndDb;
    }
    return cacheAndDb;
  }

  private void UpdateAttributeInDocCells(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    int productIndex)
  {
    if (!this.HasDocNodes)
      return;
    this.UpdateDocRow();
  }

  /// <summary>Назначить найти ячейку для графы по основному атрибуту и поместить в неё значение</summary>
  /// <param name="baseAttrInfo">Информация об атрибуте</param>
  /// <param name="docRow">Строка документа. Если null, то для всех строк записи</param>
  /// <param name="productIndex">Индекс исполнения, если -1, то для всех исполнений</param>
  /// <param name="editValue">Значение, которое редактируется в ячейке</param>
  /// <param name="viewValue">Значение, которое отображается, когда ячейка не редактируется.
  /// "" - если на просмотре ячейка должна быть пустой, null - если ячейка не меняет значение</param>
  public void SetFieldValueInDocRowsCell(
    AvsRowAttributeInfo baseAttrInfo,
    TableData docRow,
    int productIndex,
    string editValue,
    string viewValue = null)
  {
    if (baseAttrInfo == null)
      throw new ArgumentNullException("attrInfo");
    bool flag = false;
    this.avsDocument.Lock_DocCell_TextChanged();
    try
    {
      if (!this.HasDocNodes && docRow == null || this.DocRowFields == null)
        return;
      if (this.IsNoteRow)
      {
        List<TextData> cellsForBaseField = this.GetDocumentCellsForBaseField(baseAttrInfo, productIndex);
        if (cellsForBaseField.Count > 0)
        {
          flag = true;
          for (int index = 0; index < cellsForBaseField.Count; ++index)
            cellsForBaseField[index].AssignText(editValue, false, true, false, false, false);
        }
      }
      else
      {
        List<TableData> tableDataList;
        if (docRow == null)
        {
          tableDataList = this.DocNodes;
        }
        else
        {
          tableDataList = new List<TableData>(1);
          tableDataList.Add(docRow);
        }
        List<TextData> textDataList = new List<TextData>(tableDataList.Count);
        foreach (TableData docRow1 in tableDataList)
        {
          textDataList.Clear();
          this.GetDocumentCellsForBaseField(baseAttrInfo, docRow1, productIndex, textDataList);
          flag = !textDataList.IsNullOrEmpty<TextData>();
          foreach (TextData cell in textDataList)
            this.SetFieldValueInDocCell(cell, baseAttrInfo, productIndex, editValue, viewValue);
        }
      }
      if (!this.IsDocRelation && baseAttrInfo.AttributeId == AvsIDCache.Attr_FirstApplicability && baseAttrInfo.IsObjectAttribute)
      {
        this.SetAttributeValuesToDocNodes(AVSRow.RowAttr_FirstApplicability, editValue);
      }
      else
      {
        if (flag || this.avsDocument.Attr_SortIndex.Equals((AttributeInfo) baseAttrInfo) || !baseAttrInfo.IsDocField && (!baseAttrInfo.IsObjectAttribute || this.HasObject) && (!baseAttrInfo.IsRelationAttribute || this.HasRelation))
          return;
        string name = baseAttrInfo.Name;
        if (name.IsEmpty() && baseAttrInfo.AttributeGuid != Guid.Empty)
          name = baseAttrInfo.AttributeGuid.ToString();
        if (name.IsEmpty() && baseAttrInfo.AttributeId != -1)
          name = baseAttrInfo.AttributeId.ToString();
        this.SetAttributeValuesToDocNodes(name, editValue);
      }
    }
    finally
    {
      this.avsDocument.Unlock_DocCell_TextChanged();
    }
  }

  private void SetFieldValueInDocCell(
    TextData cell,
    AvsRowAttributeInfo fieldAttr,
    int productIndex,
    string editValue,
    string viewValue)
  {
    if (this.Field_Designation.Equals((AttributeInfo) fieldAttr))
    {
      string attributeValue = cell.GetAttributeValue("FullDesignation", true);
      if (editValue != "" && editValue == attributeValue)
        editValue = cell.Text;
      else
        cell.RemoveAttribute("FullDesignation", false, false);
    }
    AVSRow.SetDocCellText(cell, editValue, this.avsDocument.IsSpecification ? viewValue : editValue);
  }

  private static void HideValueInDocCell(TextData cell, string editValue)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    cell.SetAttributeValue(AVSRow.CellAttrName_HideText, "1", false, false, false);
    AVSRow.SetDocCellText(cell, editValue);
  }

  private static void SetDocCellText(TextData cell, string editValue, string viewValue = null)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    if (cell.InPlaceEditorActive || viewValue == null)
      cell.AssignText(editValue, false, true, false, false, false);
    else
      cell.AssignText(viewValue, false, true, false, false, false);
    if (viewValue != null && viewValue != editValue)
    {
      cell.SetAttributeValue(AVSRow.CellAttrName_EditText, editValue, false, false, false);
      cell.SetAttributeValue(AVSRow.CellAttrName_ViewText, viewValue, false, false, false);
    }
    else
    {
      cell.RemoveAttribute(AVSRow.CellAttrName_EditText, false, false);
      cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
    }
  }

  /// <summary>Назначить значение поля в ячейке табличного вида</summary>
  public void UpdateGridRow()
  {
    this.avsDocument?.AVSWindow?.virtualTree?.RefreshRow((IVirtualTreeItem) this);
  }

  /// <summary>Проверить значение поля на допустимость</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="value">Значение</param>
  /// <returns>Возвращает true, если проверка прошла успешно</returns>
  public bool ValidateFieldValue(AvsRowAttributeInfo attrInfo, int relationIndex, object value)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if (!this.HasRelation || this.UpdateAttrValueIndex(attrInfo, false) == -1)
      return true;
    if (AVSRow.IsCountField(attrInfo))
    {
      if (value == null)
        return true;
      if (value is string str1)
      {
        string str = str1.Trim();
        if (str == "" || this.IsFormB && this.IsDocRelation)
          return true;
        value = (object) AVSRow.ConvertCountToMeasuredValue((object) str, false);
      }
      else
        value = (object) AVSRow.ConvertCountToMeasuredValue(value);
      return true;
    }
    AvsIDCache.Attr_DopZamenText.Equals((object) attrInfo);
    return true;
  }

  /// <summary>Проверить, влияет ли атрибут на допзамены</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <returns></returns>
  public bool CheckNeedUpdateDopZamenyText(AvsRowAttributeInfo attrInfo)
  {
    if (this.IsDocRelation)
      return false;
    if (attrInfo.IsRelationAttribute && (attrInfo.AttributeId == AvsIDCache.Attr_Position || AVSRow.IsCountAttribute(attrInfo)))
      return true;
    if (attrInfo.IsObjectAttribute && attrInfo.AttributeId == AvsIDCache.Attr_Designation && this.relations != null && this.relations.Count > 0)
    {
      object obj = this.relations[0].GetValue(AvsIDCache.Attr_Position, true);
      if (obj == null || obj as string == "")
        return true;
    }
    return false;
  }

  /// <summary>Допустима ли заданная связь как скрытая для этой записи</summary>
  /// <param name="relation">Связь</param>
  /// <returns></returns>
  internal bool IsAllowableForHidden(RelationAttributeValuesCache relation)
  {
    return !this.IsHiddenRow && this.CheckRelation_IsHiddenDopZamen(relation) || this.CheckRelation_IsHiddenForPosDesignationSumm(relation);
  }

  /// <summary>Проверить атрибуты заданной связи. Является ли она скрытой</summary>
  /// <param name="relation">Связь</param>
  /// <returns></returns>
  public bool CheckRelation_IsHiddenRelation(RelationAttributeValuesCache relation)
  {
    return relation != null && this.CheckRelation_IsHiddenForPosDesignationSumm(relation) | this.CheckRelation_IsHiddenDopZamen(relation);
  }

  /// <summary>Проверить атрибуты заданной связи. Является ли она скрытой для суммированной записи с позиционными обозначениями</summary>
  /// <param name="relation">Связь</param>
  /// <returns></returns>
  public bool CheckRelation_IsHiddenForPosDesignationSumm(RelationAttributeValuesCache relation)
  {
    if (this.GetRelationIndexForProduct(relation.ProjectId, this.relations) == -1)
      return false;
    string valueString = relation.GetValueString(this.Field_PosDesignation, false);
    if (string.IsNullOrEmpty(valueString) && relation.RelationType == AvsIDCache.Relation_Podbor)
      valueString = relation.GetValueString(this.Attr_PodborForPosDesignation, false);
    return !string.IsNullOrEmpty(valueString) && this.avsDocument.CanSummThisRelations(new AvsRowData(this), new AvsRowData((AttributeValuesCache) relation), this.NoteCellMapping);
  }

  /// <summary>Проверить атрибуты заданной связи. Является ли данная связь дополнительной для допзамен,
  /// которую необходимо скрывать в этой записи</summary>
  /// <param name="relation">Связь</param>
  /// <returns></returns>
  public bool CheckRelation_IsHiddenDopZamen(RelationAttributeValuesCache relation)
  {
    return this.CheckRelation_IsHiddenDopZamen(relation.GetValueInt64(this.avsDocument.Attr_DopZamenGroupNum, false), relation.GetValueString(this.Field_Position, false), relation.ProjectId);
  }

  /// <summary>Проверить атрибуты заданной связи. Является ли данная связь дополнительной для допзамен,
  /// которую необходимо скрывать в этой записи</summary>
  /// <param name="dopZamenyGroup">Номер группы допзамен для проверяемой связи</param>
  /// <param name="position">Позиция</param>
  /// <param name="productId">Идентификатор исполнения - владельца связи</param>
  /// <returns></returns>
  public bool CheckRelation_IsHiddenDopZamen(long dopZamenyGroup, string position, long productId)
  {
    bool flag = false;
    if (dopZamenyGroup == -1L)
      return flag;
    int relationIndexForProduct = this.GetRelationIndexForProduct(productId, this.relations);
    if (relationIndexForProduct != -1)
    {
      long valueInt64 = this.Relations[relationIndexForProduct].GetValueInt64(this.avsDocument.Attr_DopZamenGroupNum, false);
      string valueString = this.Relations[relationIndexForProduct].GetValueString(this.Field_Position, false);
      long num = dopZamenyGroup;
      flag = valueInt64 == num && valueString == (position ?? "");
    }
    return flag;
  }

  /// <summary>Обновить кэш атрибутов новыми значениями</summary>
  /// <param name="relationData">Новые значения атрибутов связи</param>
  /// <param name="objectData">Новые значения атрибутов объекта</param>
  /// <param name="updateDocNode">Обновлять документ</param>
  /// <param name="updateTreeList">Обновлять табличный вид</param>
  /// <param name="updateNote">Обновлять ячейку примечания в строке документа</param>
  public void UpdateAttributes(
    ColumnInfo[] loadedColumns,
    RelationAttributeValuesCache relationData,
    AttributeValuesCache objectData,
    bool updateDocNode,
    bool updateTreeList,
    bool updateNote)
  {
    if (this.ObjectAttributesCache == null && (objectData != null || relationData != null))
    {
      this.AddRowData(relationData, objectData);
      if (!updateDocNode)
        return;
      this.UpdateDocRow();
    }
    else
    {
      ++this._suspendUpdateNote;
      try
      {
        List<RelationAttributeValuesCache> relationList = (List<RelationAttributeValuesCache>) null;
        int num = -1;
        if (relationData != null)
        {
          num = this.GetRelationIndex(this.relations, relationData.RelationId);
          if (num != -1)
          {
            relationList = this.relations;
          }
          else
          {
            num = this.GetRelationIndex(this.hiddenRelations, relationData.RelationId);
            if (num != -1)
              relationList = this.hiddenRelations;
          }
          List<RelationAttributeValuesCache> allRelations = this.GetAllRelations();
          for (int index = 0; index < allRelations.Count; ++index)
            allRelations[index].UpdateValuesForAttrsInfoCount();
        }
        if (this.ObjectAttributesCache != null)
          this.ObjectAttributesCache.UpdateValuesForAttrsInfoCount();
        AttributeValuesCache attributeValuesCache = objectData;
        if (attributeValuesCache == null && relationData != null)
          attributeValuesCache = relationData.ObjectAttributesCache;
        this.ObjectAttributesCache = (AttributeValuesCache) this.ObjectAttributesCache.Clone();
        AttributeValuesCache objectAttributesCache = this.ObjectAttributesCache;
        if (attributeValuesCache != null)
          this.rowID.SetDBObjectInfo(attributeValuesCache.ObjectGuid, attributeValuesCache.ObjectId, attributeValuesCache.ObjectType, attributeValuesCache.ObjectCaption);
        foreach (ColumnInfo loadedColumn in loadedColumns)
        {
          if (loadedColumn.AttributeSource == AttributeSourceTypes.Relation && relationData != null && num != -1)
          {
            AvsRowAttributeInfo attributeInfo = relationList[num].GetAttributeInfo((int) loadedColumn.AttributeID);
            this.SetFieldValue(attributeInfo, num, -1, relationList, relationData.GetValue(attributeInfo, true), false, false, false, false, true, false, originalAttribute: true);
          }
          else if (loadedColumn.AttributeSource == AttributeSourceTypes.Object && attributeValuesCache != null && objectAttributesCache != null)
          {
            AvsRowAttributeInfo attributeInfo = objectAttributesCache.GetAttributeInfo((int) loadedColumn.AttributeID);
            if (attributeInfo != null)
            {
              if (this.Field_Name.Equals((AttributeInfo) attributeInfo))
              {
                object obj = attributeValuesCache.GetValue(attributeInfo, true);
                obj?.ToString();
                this.SetFieldValue(attributeInfo, 0, -1, relationList, obj, false, false, false, false, true, false, originalAttribute: true);
                this._needUpdateName = true;
              }
              else if (attributeInfo.AttributeId != AvsIDCache.Attr_FirstApplicability || this.IsDocRelation)
              {
                if (attributeInfo.AttributeId != AvsIDCache.Attr_Format || this.IsDocRelation || MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing))
                {
                  object obj = attributeValuesCache.GetValue(attributeInfo, true);
                  this.SetFieldValue(attributeInfo, 0, -1, relationList, obj, false, false, false, false, true, false, originalAttribute: true);
                }
                if (attributeInfo.AttributeId == AvsIDCache.Attr_Gost && attributeInfo.IsObjectAttribute)
                  this._needUpdateName = true;
              }
            }
          }
        }
      }
      finally
      {
        --this._suspendUpdateNote;
        if (updateDocNode)
          this.UpdateDocRow();
      }
    }
  }

  /// <summary>Получить AttributeProcessor для атрибутов связи из кэша</summary>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="autoCreate">Создавать если нет в кэше</param>
  /// <param name="autoLoad">Загружать данные при создании</param>
  /// <returns></returns>
  public AttributeProcessor GetRelationAttributeProcessor(
    int relationIndex,
    bool autoCreate,
    bool autoLoad)
  {
    if (relationIndex == -1 || this.relations == null || relationIndex >= this.relations.Count)
      return (AttributeProcessor) null;
    AttributeProcessor attributeProcessor = (AttributeProcessor) null;
    if (this.avsDocument.AttributeProcessorDictionary != null)
    {
      if (this.avsDocument.AttributeProcessorDictionary.ContainsKey(this.relations[relationIndex].RelationId))
        attributeProcessor = this.avsDocument.AttributeProcessorDictionary[this.relations[relationIndex].RelationId];
      else if (autoCreate)
      {
        attributeProcessor = new AttributeProcessor(this.relations[relationIndex].RelationId, this.relations[relationIndex].RelationType, AttributableElements.Relation);
        this.avsDocument.AttributeProcessorDictionary.Add(this.relations[relationIndex].RelationId, attributeProcessor);
      }
    }
    if (attributeProcessor == null)
      return (AttributeProcessor) null;
    if (autoLoad && (!attributeProcessor.Loaded || attributeProcessor.Id != this.relations[relationIndex].RelationId))
      attributeProcessor.Load(this.relations[relationIndex].RelationId, AttributableElements.Relation, GetAttributeValuesModes.None, false);
    return attributeProcessor;
  }

  /// <summary>Получить AttributeProcessor для атрибутов объекта из кэша</summary>
  /// <param name="autoCreate">Создавать если нет в кэше</param>
  /// <param name="autoLoad">Загружать данные при создании</param>
  /// <returns></returns>
  public AttributeProcessor GetObjectAttributeProcessor(bool autoCreate, bool autoLoad)
  {
    return this.GetObjectAttributeProcessor(autoCreate, autoLoad, this.ObjectId);
  }

  /// <summary>Получить AttributeProcessor для атрибутов объекта из кэша</summary>
  /// <param name="autoCreate">Создавать если нет в кэше</param>
  /// <param name="autoLoad">Загружать данные при создании</param>
  /// <returns></returns>
  public AttributeProcessor GetObjectAttributeProcessor(
    bool autoCreate,
    bool autoLoad,
    long objectId)
  {
    if (objectId == -1L)
      return (AttributeProcessor) null;
    AttributeProcessor attributeProcessor = (AttributeProcessor) null;
    if (this.avsDocument.AttributeProcessorDictionary != null)
    {
      if (this.avsDocument.AttributeProcessorDictionary.ContainsKey(objectId))
        attributeProcessor = this.avsDocument.AttributeProcessorDictionary[objectId];
      else if (autoCreate)
      {
        attributeProcessor = new AttributeProcessor(objectId, this.ObjType, AttributableElements.Object);
        this.avsDocument.AttributeProcessorDictionary.Add(objectId, attributeProcessor);
      }
    }
    if (attributeProcessor == null)
      return (AttributeProcessor) null;
    if (autoLoad && (!attributeProcessor.Loaded || attributeProcessor.Id != objectId))
      attributeProcessor.Load(objectId, AttributableElements.Object, GetAttributeValuesModes.None, false);
    return attributeProcessor;
  }

  /// <summary>Получить стили редактирования для атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="relationIndex">Индекс связи</param>
  /// <returns>Список стилей редактирования</returns>
  public List<UITypeEditorEditStyle> GetEditorStyles(
    AvsRowAttributeInfo attrInfo,
    int relationIndex)
  {
    if (attrInfo.IsDocField)
      return new List<UITypeEditorEditStyle>();
    AttributeEditorInfo attributeEditorInfo = (AttributeEditorInfo) null;
    if (attrInfo.IsRelationAttribute)
      this.relEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo);
    else if (attrInfo.IsObjectAttribute)
      this.objEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo);
    if (attributeEditorInfo != null && attributeEditorInfo.EditorStyleList != null)
      return attributeEditorInfo.EditorStyleList.Count > 0 ? attributeEditorInfo.EditorStyleList : (List<UITypeEditorEditStyle>) null;
    if (attributeEditorInfo == null)
    {
      attributeEditorInfo = new AttributeEditorInfo();
      if (attrInfo.IsRelationAttribute)
        this.relEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
      else if (attrInfo.IsObjectAttribute)
        this.objEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
    }
    if (!attributeEditorInfo.ReadOnly.HasValue)
      attributeEditorInfo.ReadOnly = new bool?(this.GetAttributeReadOnly(attrInfo, relationIndex, this.Relations));
    if (attributeEditorInfo.ReadOnly.Value)
    {
      attributeEditorInfo.EditorStyleList = new List<UITypeEditorEditStyle>();
      return (List<UITypeEditorEditStyle>) null;
    }
    AttributeProcessor attributeProcessor = (AttributeProcessor) null;
    if (attrInfo.IsRelationAttribute)
      attributeProcessor = this.GetRelationAttributeProcessor(relationIndex, true, false);
    else if (attrInfo.IsObjectAttribute)
      attributeProcessor = this.GetObjectAttributeProcessor(true, false);
    if (attributeProcessor != null)
    {
      if (this.IsDocRelation && AVSRow.IsCountAttribute(attrInfo))
      {
        attributeEditorInfo.EditorStyleList = (List<UITypeEditorEditStyle>) null;
      }
      else
      {
        attributeEditorInfo.EditorStyleList = new List<UITypeEditorEditStyle>();
        attributeEditorInfo.EditorStyleList.Add(attributeProcessor.GetEditorStyle(new AttributeValues(attrInfo.AttributeId, (object) null)));
      }
    }
    if (attributeEditorInfo.EditorStyleList == null)
      attributeEditorInfo.EditorStyleList = new List<UITypeEditorEditStyle>();
    return attributeEditorInfo.EditorStyleList.Count == 0 ? (List<UITypeEditorEditStyle>) null : attributeEditorInfo.EditorStyleList;
  }

  /// <summary>Атрибут можно редактировать в ячейке как текст</summary>
  /// <param name="isRelationAttr">Атрибут связи</param>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  public virtual bool CanInplaceEdit(AvsRowAttributeInfo attrInfo, int relationIndex)
  {
    if (!this.avsDocument.IsSpecification && this.HasRelation && (attrInfo.IsRelationAttribute || AVSRow.IsCountField(attrInfo)) && this.relations[0].ProjectId > 0L)
      return false;
    if (attrInfo.IsDocField || !this.HasRelation && this.DocRowFields != null && this.DocRowFields.Find((Predicate<AvsRowAttributeInfo>) (x => x.AttributeId == attrInfo.AttributeId && x.IsRelationAttribute == attrInfo.IsRelationAttribute)) != null)
      return true;
    if (attrInfo.IsObjectAttribute && attrInfo.AttributeId == AvsIDCache.Attr_Material)
      return false;
    AttributeEditorInfo attributeEditorInfo = (AttributeEditorInfo) null;
    if (attrInfo.IsRelationAttribute)
      this.relEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo);
    else if (attrInfo.IsObjectAttribute)
      this.objEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo);
    if (attributeEditorInfo != null && attributeEditorInfo.CanInplaceEdit.HasValue)
      return attributeEditorInfo.CanInplaceEdit.Value;
    if (attributeEditorInfo == null)
    {
      attributeEditorInfo = new AttributeEditorInfo();
      if (attrInfo.IsRelationAttribute)
        this.relEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
      else if (attrInfo.IsObjectAttribute)
        this.objEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
    }
    if (!attributeEditorInfo.ReadOnly.HasValue)
      attributeEditorInfo.ReadOnly = new bool?(this.GetAttributeReadOnly(attrInfo, relationIndex, this.Relations));
    if (attributeEditorInfo.ReadOnly.Value)
      return (attributeEditorInfo.CanInplaceEdit = new bool?(false)).Value;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrInfo.AttributeId);
    if (attributeType != null && attributeType.PossibleValues != null && attributeType.PossibleValues.Count > 0)
      return (attributeEditorInfo.CanInplaceEdit = new bool?(false)).Value;
    AttributeProcessor attributeProcessor = (AttributeProcessor) null;
    if (attrInfo.IsRelationAttribute)
      attributeProcessor = this.GetRelationAttributeProcessor(relationIndex, true, false);
    else if (attrInfo.IsObjectAttribute)
      attributeProcessor = this.GetObjectAttributeProcessor(true, false);
    if (attributeProcessor != null)
    {
      TypeConverter typeConverter = attributeProcessor.GetTypeConverter(new AttributeValues(attrInfo.AttributeId, (object) null));
      if (typeConverter != null)
        attributeEditorInfo.CanInplaceEdit = new bool?(typeConverter.CanConvertFrom(attributeProcessor.GetAttributeContext(new AttributeValues(attrInfo.AttributeId, (object) null)), typeof (string)));
    }
    if (!attributeEditorInfo.CanInplaceEdit.HasValue)
      attributeEditorInfo.CanInplaceEdit = new bool?(false);
    return attributeEditorInfo.CanInplaceEdit.Value;
  }

  /// <summary>Проверить можно ли редактировать атрибут Количество</summary>
  /// <param name="productIndex">Индекс исполнения</param>
  /// <returns>true, если атрибут только для чтения</returns>
  public bool GetReadOnlyCount(int productIndex)
  {
    return this.GetAttributeReadOnly(this.Field_Count, this.GetRelationIndexForProduct((long) productIndex), this.Relations);
  }

  /// <summary>Проверить можно ли редактировать атрибут</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="relationList">Коллекция связей</param>
  /// <returns>true, если атрибут только для чтения</returns>
  public virtual bool GetAttributeReadOnly(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    if (this.IsNoteRow)
    {
      int indexForRelation = this.GetProductIndexForRelation(relationIndex, relationList);
      TextData cellForAttribute = this.GetDocumentCellForAttribute(attrInfo, indexForRelation);
      if (cellForAttribute != null)
        return cellForAttribute.ReadOnly;
    }
    if (attrInfo.IsDocField || !AVSRow.IsCountAttribute(attrInfo) && !this.HasRelation && this.DocRowFields != null && this.DocRowFields.Find((Predicate<AvsRowAttributeInfo>) (x => x.AttributeId == attrInfo.AttributeId && x.IsRelationAttribute == attrInfo.IsRelationAttribute)) != null)
      return attrInfo.ReadOnly;
    if ((this.Field_PosDesignation.Equals((AttributeInfo) attrInfo) || AVSRow.IsCountAttribute(attrInfo)) && this.HasHiddenRelationForPosDesignationSumm)
      return true;
    if (this.IsFormB && this.IsDocRelation && AVSRow.IsCountAttribute(attrInfo))
      return false;
    AttributeEditorInfo attributeEditorInfo = (AttributeEditorInfo) null;
    if (attrInfo.IsRelationAttribute)
    {
      if (attrInfo.AttributeId == AvsIDCache.Attr_DopZamenText || attrInfo.AttributeId == AvsIDCache.Attr_DopZamenNumInGroup || attrInfo.AttributeId == AvsIDCache.Attr_DesignerActualVariant || attrInfo.AttributeId == AvsIDCache.Attr_DopZamenGroupNum || MetaDataHelper.GetAttribute4RelationType(this.RelType, attrInfo.AttributeId) == null)
        return true;
      if (this.relEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo) && attributeEditorInfo != null && attributeEditorInfo.ReadOnly.HasValue)
        return attributeEditorInfo.ReadOnly.Value;
    }
    else if (attrInfo.IsObjectAttribute)
    {
      if (attrInfo.AttributeId == this.Field_Name.AttributeId || attrInfo.AttributeId == this.Field_Designation.AttributeId)
        return true;
      if (attrInfo.AttributeId == AvsIDCache.Attr_Format && this.ObjType != -1)
        return MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_Document) || MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing);
      if (this.objEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo) && attributeEditorInfo != null && attributeEditorInfo.ReadOnly.HasValue)
        return attributeEditorInfo.ReadOnly.Value;
    }
    if (attributeEditorInfo == null)
    {
      attributeEditorInfo = new AttributeEditorInfo();
      if (attrInfo.IsRelationAttribute)
        this.relEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
      else if (attrInfo.IsObjectAttribute)
        this.objEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
    }
    if (this.IsDocRelation)
    {
      if (attrInfo.IsRelationAttribute && (attrInfo.AttributeId == AvsIDCache.Attr_Position || attrInfo.AttributeId == AvsIDCache.Attr_Zone))
        return (attributeEditorInfo.ReadOnly = new bool?(true)).Value;
      if (attrInfo.AttributeId == AvsIDCache.Attr_Format && attrInfo.IsObjectAttribute)
        return true;
    }
    else if (attrInfo.AttributeId == AvsIDCache.Attr_Format && attrInfo.IsObjectAttribute && MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing))
      return true;
    if (this.docNode != null)
    {
      int index = 0;
      for (int count = this.docNode.Nodes.Count; index < count; ++index)
      {
        if (this.docNode.Nodes[index] is TextData node && node.ReferenceToTextSource is ReferenceToDBObjectAttributeBase referenceToTextSource && referenceToTextSource.AttributeID == attrInfo.AttributeId && attrInfo.AttrSrc != FieldSource.DocumentRowField && referenceToTextSource.IsRelationAttribute == attrInfo.IsRelationAttribute)
        {
          if (node.ReadOnly)
          {
            attributeEditorInfo.ReadOnly = new bool?(true);
          }
          else
          {
            attributeEditorInfo.ReadOnly = new bool?(false);
            break;
          }
        }
      }
    }
    else
    {
      List<AvsRowAttributeInfo> docRowFields = this.DocRowFields;
      if (docRowFields != null)
      {
        int index = 0;
        for (int count = docRowFields.Count; index < count; ++index)
        {
          if (object.Equals((object) docRowFields[index], (object) attrInfo))
          {
            if (docRowFields[index].ReadOnly)
            {
              attributeEditorInfo.ReadOnly = new bool?(true);
            }
            else
            {
              attributeEditorInfo.ReadOnly = new bool?(false);
              break;
            }
          }
        }
      }
    }
    if (attributeEditorInfo.ReadOnly.HasValue && attributeEditorInfo.ReadOnly.Value)
      return true;
    if (attrInfo.IsVirtualAttribute)
      return attrInfo.ReadOnly;
    AttributeProcessor attributeProcessor = (AttributeProcessor) null;
    if (attrInfo.IsRelationAttribute)
      attributeProcessor = this.GetRelationAttributeProcessor(relationIndex, false, false);
    else if (attrInfo.IsObjectAttribute)
      attributeProcessor = this.GetObjectAttributeProcessor(false, false);
    if (attributeProcessor != null)
    {
      AttributeValues attributeValues = (AttributeValues) null;
      if (attributeProcessor.Loaded)
        attributeValues = attributeProcessor.FindAttributeValues(attrInfo.AttributeId);
      if (attributeValues != null)
        return (attributeEditorInfo.ReadOnly = new bool?(attributeValues.ReadOnly)).Value;
    }
    if (attrInfo.IsRelationAttribute)
    {
      if (MetaDataHelper.GetAttribute4RelationType(this.RelType, attrInfo.AttributeId) == null)
      {
        attributeEditorInfo.ReadOnly = new bool?(true);
        return true;
      }
      attributeEditorInfo.ReadOnly = new bool?(false);
    }
    else if (attrInfo.IsObjectAttribute)
    {
      if (this.ObjType == -1 || !AttributeCacheHelper.IsEnabledObjectTypeAttribute(attrInfo.AttributeId, this.ObjType))
      {
        attributeEditorInfo.ReadOnly = new bool?(true);
        return true;
      }
      if (this.ObjectId < 0L)
      {
        attributeEditorInfo.ReadOnly = new bool?(false);
      }
      else
      {
        attributeEditorInfo.ReadOnly = new bool?(true);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId, false);
          if (dbObject != null)
          {
            switch (dbObject.ObjectModifyMode)
            {
              case ObjectModifyModes.InBase:
                attributeEditorInfo.ReadOnly = new bool?(false);
                break;
              case ObjectModifyModes.Checkout:
              case ObjectModifyModes.CreateVersion:
                attributeEditorInfo.ReadOnly = new bool?(dbObject.CheckoutBy != sessionKeeper.Session.UserID);
                break;
            }
          }
        }
      }
    }
    return attributeEditorInfo.ReadOnly.Value;
  }

  /// <summary>Получить ControlPages редактора атрибута</summary>
  /// <param name="isRelationAttr">Атрибут связи</param>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="style">Стиль редактирования</param>
  /// <returns></returns>
  public IAttributeEditorControl GetEditorControl1(
    bool isRelationAttr,
    int attributeID,
    UITypeEditorEditStyle style)
  {
    return this.GetEditorControl1(new AvsRowAttributeInfo(isRelationAttr, attributeID), 0, style, false);
  }

  public AttributeProcessor GetAttributeProcessor(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    bool autoLoadAttrProcessor)
  {
    AttributeProcessor attributeProcessor = (AttributeProcessor) null;
    if (attrInfo.IsRelationAttribute)
      attributeProcessor = this.GetRelationAttributeProcessor(relationIndex, true, autoLoadAttrProcessor);
    else if (attrInfo.IsObjectAttribute)
      attributeProcessor = this.GetObjectAttributeProcessor(true, autoLoadAttrProcessor);
    return attributeProcessor;
  }

  /// <summary>Получить ControlPages редактора атрибута</summary>
  /// <param name="attrInfo">Информация об атрибуте</param>
  /// <param name="relationIndex">Индекс связи</param>
  /// <param name="style">Стиль редактирования</param>
  /// <param name="autoLoadAttrProcessor">Загружать данные для AttributeProcessor</param>
  /// <returns></returns>
  public IAttributeEditorControl GetEditorControl1(
    AvsRowAttributeInfo attrInfo,
    int relationIndex,
    UITypeEditorEditStyle style,
    bool autoLoadAttrProcessor)
  {
    if (attrInfo.IsDocField)
      return (IAttributeEditorControl) null;
    AttributeEditorInfo attributeEditorInfo = (AttributeEditorInfo) null;
    if (attrInfo.IsRelationAttribute)
      this.relEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo);
    else if (attrInfo.IsObjectAttribute)
      this.objEditors.TryGetValue(attrInfo.AttributeId, out attributeEditorInfo);
    if (attributeEditorInfo == null)
    {
      attributeEditorInfo = new AttributeEditorInfo();
      if (attrInfo.IsRelationAttribute)
        this.relEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
      else if (attrInfo.IsObjectAttribute)
        this.objEditors.Add(attrInfo.AttributeId, attributeEditorInfo);
    }
    if (!attributeEditorInfo.ReadOnly.HasValue)
      attributeEditorInfo.ReadOnly = new bool?(this.GetAttributeReadOnly(attrInfo, relationIndex, this.Relations));
    if (attributeEditorInfo.ReadOnly.Value)
      return (IAttributeEditorControl) null;
    AttributeProcessor attributeProcessor = (AttributeProcessor) null;
    if (attrInfo.IsRelationAttribute)
      attributeProcessor = this.GetRelationAttributeProcessor(relationIndex, true, autoLoadAttrProcessor);
    else if (attrInfo.IsObjectAttribute)
      attributeProcessor = this.GetObjectAttributeProcessor(true, autoLoadAttrProcessor);
    return attributeProcessor?.GetEditorControl(attrInfo.AttributeId, new int?(0), style);
  }

  /// <summary>Можно ли вызвать редактор количества для ячейки документа</summary>
  /// <param name="docNode">Ячейка количества</param>
  protected bool CanCallCountDocCellEditor(DocumentTreeNode node)
  {
    if (this.IsDocRelation)
      return false;
    IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(this.RelType, this.Field_Count.AttributeId);
    if (attribute4RelationType != null && attribute4RelationType.Options.HasFlag((Enum) AttributeOptions.DisableManualEdit))
      return false;
    if (!this.IsFormB)
      return true;
    TextData cell = node as TextData;
    int num = -1;
    if (cell != null)
      num = this.GetProductIndexForCountCell(cell);
    return num != -1 && num < this.avsDocument.productsInfo.Count;
  }

  /// <summary>Вызвать редактор количества для ячейки количества</summary>
  internal bool CallCountDocCellEditor(
    int productIndex,
    object attrValue,
    out object res,
    ref bool allProducts)
  {
    int num = -1;
    if (productIndex != -1 && productIndex < this.avsDocument.productsInfo.Count)
      num = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[productIndex].Id, this.relations);
    MeasuredValue mValue = (MeasuredValue) null;
    bool flag1 = false;
    bool flag2 = false;
    double result = double.MinValue;
    MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
    bool flag3 = false;
    if (attrValue != null && attrValue is DBNull)
      attrValue = (object) null;
    MeasuredValue measuredValue = (MeasuredValue) null;
    string str1 = Convert.ToString(attrValue);
    if (str1 != "")
    {
      mValue = AVSRow.ConvertCountToMeasuredValue((object) str1, false);
      if (mValue != null)
      {
        measureDescriptor = MeasureHelper.FindDescriptor(mValue);
        result = mValue.Value;
        flag2 = true;
      }
      flag3 = true;
      flag1 = mValue != null && measureDescriptor != null;
    }
    if (flag3)
    {
      if (flag1 && mValue != null)
      {
        measuredValue = mValue;
        attrValue = (object) mValue;
      }
      else if (measureDescriptor != null)
        measuredValue = new MeasuredValue(double.NaN, measureDescriptor.MeasureID);
      else if (flag2)
        measuredValue = new MeasuredValue(result, -1L);
    }
    if (attrValue != null | flag3)
      attrValue = (object) measuredValue;
    this.avsDocument.ValidateValue = false;
    string s = result.ToString();
    if (!flag2)
      s = "";
    long measureID = -1;
    if (measureDescriptor != null)
      measureID = measureDescriptor.MeasureID;
    if (string.IsNullOrEmpty(s))
      measureID = AVSRow.DefaultCountID;
    AVSMeasureForm avsMeasureForm = new AVSMeasureForm();
    bool flag4 = this.IsFormB && this.avsDocument.productsInfo != null && this.avsDocument.productsInfo.Count > 1;
    avsMeasureForm.ReadOnlyCount = this.GetAttributeReadOnly(this.Field_Count, num != -1 ? num : 0, this.Relations);
    avsMeasureForm.ShowAllCheckBox = flag4 & allProducts;
    ArrayList listByAttributeId = MeasureEditor.GetMeasureDescriptorListByAttributeId(AvsIDCache.Attr_Count);
    MeasureDescriptor[] aMeasureDescriptorList = listByAttributeId == null ? MeasureHelper.Instance.Measures : (MeasureDescriptor[]) listByAttributeId.ToArray(typeof (MeasureDescriptor));
    if (avsMeasureForm.ExecuteDialog(ref s, ref measureID, aMeasureDescriptorList, (GetDefaultMeasureIDDelegate) null) == DialogResult.OK && double.TryParse(s, out result) && measureID != -1L)
    {
      MeasuredValue countValue = new MeasuredValue(result, measureID);
      allProducts = avsMeasureForm.ShowAllCheckBox && avsMeasureForm.AllProducts;
      MeasureDescriptor md = (MeasureDescriptor) null;
      string countMeasure = (string) null;
      string str2 = measureID == AVSRow.DefaultCountID ? this.ConvertCountToValueAndMeasure((object) countValue, ref md, out countMeasure) : countValue.ToString();
      res = (object) str2;
      return true;
    }
    res = (object) null;
    return false;
  }

  /// <summary>Вызвать редактор количества для ячейки количества</summary>
  /// <param name="docNode">Ячейка количества</param>
  protected void CallCountDocCellEditor(DocumentTreeNode docNode)
  {
    if (this.IsDocRelation)
      return;
    int num1 = -1;
    int num2 = -1;
    if (docNode is TextData cell)
    {
      int index = cell.Index;
      if (AVSRow.IsCountFormBCell(this.IsFormB, cell))
      {
        num2 = this.GetProductIndexForCountCell(cell);
        if (num2 != -1 && num2 < this.avsDocument.productsInfo.Count)
          num1 = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[num2].Id, this.relations);
        if (num2 == -1 || num2 >= this.avsDocument.productsInfo.Count)
          return;
      }
    }
    MeasuredValue mValue = (MeasuredValue) null;
    TextBoxElement textBoxElement = cell as TextBoxElement;
    bool flag1 = false;
    bool flag2 = false;
    double result = double.MinValue;
    MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
    bool flag3 = false;
    object obj = this.GetFieldValue(this.Field_Count, num1 != -1 ? num1 : 0, num2, this.relations, false, true);
    if (obj != null && obj is DBNull)
      obj = (object) null;
    MeasuredValue measuredValue = (MeasuredValue) null;
    if (textBoxElement != null)
    {
      string activeEditorText = textBoxElement.GetActiveEditorText();
      if (!string.IsNullOrEmpty(activeEditorText))
      {
        mValue = AVSRow.ConvertCountToMeasuredValue((object) activeEditorText, false);
        if (mValue != null)
        {
          measureDescriptor = MeasureHelper.FindDescriptor(mValue);
          result = mValue.Value;
          flag2 = true;
        }
        flag3 = true;
        flag1 = mValue != null && measureDescriptor != null;
      }
    }
    if (flag3)
    {
      if (flag1 && mValue != null)
      {
        measuredValue = mValue;
        obj = (object) mValue;
      }
      else if (measureDescriptor != null)
        measuredValue = new MeasuredValue(double.NaN, measureDescriptor.MeasureID);
      else if (flag2)
        measuredValue = new MeasuredValue(result, -1L);
    }
    if (obj != null | flag3)
      ;
    bool flag4 = false;
    try
    {
      this.avsDocument.ValidateValue = false;
      string s = result.ToString();
      if (!flag2)
        s = "";
      long measureID = -1;
      if (measureDescriptor != null)
        measureID = measureDescriptor.MeasureID;
      if (string.IsNullOrEmpty(s))
        measureID = AVSRow.DefaultCountID;
      AVSMeasureForm avsMeasureForm = new AVSMeasureForm();
      avsMeasureForm.ReadOnlyCount = (docNode as TextData).ReadOnlyNow;
      bool flag5 = this.IsFormB && this.avsDocument.productsInfo != null && this.avsDocument.productsInfo.Count > 1;
      avsMeasureForm.ShowAllCheckBox = flag5;
      ArrayList listByAttributeId = MeasureEditor.GetMeasureDescriptorListByAttributeId(AvsIDCache.Attr_Count);
      MeasureDescriptor[] aMeasureDescriptorList = listByAttributeId == null ? MeasureHelper.Instance.Measures : (MeasureDescriptor[]) listByAttributeId.ToArray(typeof (MeasureDescriptor));
      flag4 = avsMeasureForm.ExecuteDialog(ref s, ref measureID, aMeasureDescriptorList, (GetDefaultMeasureIDDelegate) null) == DialogResult.OK;
      if (!flag4)
        return;
      if (double.TryParse(s, out result) && measureID != -1L)
      {
        MeasuredValue countValue = new MeasuredValue(result, measureID);
        this.avsDocument.ValidateValue = true;
        if (textBoxElement == null)
          return;
        MeasureDescriptor md = (MeasureDescriptor) null;
        string countMeasure = (string) null;
        string str = measureID == AVSRow.DefaultCountID ? this.ConvertCountToString((object) countValue, ref md, out countMeasure) : countValue.ToString();
        if (!avsMeasureForm.ReadOnlyCount)
        {
          if (!avsMeasureForm.AllProducts)
          {
            this.avsDocument.ValidateValue = false;
            try
            {
              textBoxElement.AssignText(str, false, true, false, true, true);
            }
            finally
            {
              this.avsDocument.ValidateValue = true;
            }
          }
          else
            this.SetCountToAllProducts((object) str, true);
        }
        else
          this.SetCountMeasure(avsMeasureForm.AllProducts ? -1 : num2, (object) countValue, true);
      }
      else
        flag4 = false;
    }
    finally
    {
      this.avsDocument.ValidateValue = true;
      if (!flag4 && textBoxElement != null && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl)
        placeEditorControl.Focus();
    }
  }

  /// <summary>Можно ли вызвать редактор наименования для ячейки документа</summary>
  /// <param name="docNode">Ячейка наименования</param>
  protected bool CanCallNameDocCellEditor(DocumentTreeNode docNode)
  {
    if (this.IsDocRelation || this.ObjType == AvsIDCache.ObjType_DetailWithoutDrawing)
      return false;
    IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(this.ObjType, this.Field_Name.AttributeId);
    return attribute4ObjectType == null || !attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.DisableManualEdit);
  }

  /// <summary>Вызвать редактор наименования для ячейки документа</summary>
  /// <param name="docNode">Ячейка наименования</param>
  protected void CallNameDocCellEditor(DocumentTreeNode docNode)
  {
    if (this.IsDocRelation)
      return;
    string fieldStringValue = this.GetFieldStringValue(this.Field_Name, 0, -1, (List<RelationAttributeValuesCache>) null, true);
    if (RtfInSiteEditorWrapper.HasMaterialKeyword(fieldStringValue, this.avsDocument.Document.MaterialKeyWords))
    {
      int num1 = (int) MessageBox.Show($"Наименование \"{fieldStringValue}\" нельзя модифицировать, так как оно содержит ключевые слова для материалов и преобразование происходит автоматически", "Внимание!");
    }
    else
    {
      bool flag = false;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId, false);
        if (dbObject == null && this.ObjectId < -1L)
        {
          this.ObjectId = -this.ObjectId;
          dbObject = sessionKeeper.Session.GetObject(this.ObjectId, false);
        }
        if (dbObject != null)
        {
          if (dbObject.CheckoutBy == sessionKeeper.Session.UserID)
          {
            if (this.ObjectId > 0L)
              this.ObjectId = -this.ObjectId;
          }
          else
          {
            switch (dbObject.ObjectModifyMode)
            {
              case ObjectModifyModes.Checkout:
                if (dbObject.ObjectID > 0L)
                {
                  if (dbObject.CheckoutBy == 0L)
                  {
                    if (MessageBox.Show(string.Format("Чтобы редактировать \"{0}\", объект нужно взять на изменение(по окончании редактирования объект будет сдан).{1}Взять \"{0}\" на изменение?", (object) dbObject.Caption, (object) Environment.NewLine), "Внимание!", MessageBoxButtons.OKCancel) != DialogResult.OK)
                      return;
                    this.ObjectId = dbObject.CheckOut().ObjectID;
                    flag = true;
                    break;
                  }
                  if (dbObject.CheckoutBy != sessionKeeper.Session.UserID)
                  {
                    int num2 = (int) MessageBox.Show($"Наименование \"{dbObject.Caption}\" нельзя модифицировать, т.к. объект взят на изменение другим пользователем.", "Внимание!");
                    return;
                  }
                  break;
                }
                break;
              case ObjectModifyModes.CreateVersion:
                int num3 = (int) MessageBox.Show($"Чтобы редактировать наименование \"{dbObject.Caption}\" нужно выпустить версию объекта.", "Внимание!");
                return;
              case ObjectModifyModes.CantModify:
                int num4 = (int) MessageBox.Show($"Наименование \"{dbObject.Caption}\" нельзя модифицировать", "Внимание!");
                return;
            }
          }
        }
      }
      if (MaterialFormulaDlg.Execute(ref fieldStringValue, this.avsDocument.Document.MaterialKeyWords) == DialogResult.OK)
      {
        foreach (TextData textData in this.GetDocumentCellsForBaseField(this.Field_Name, -1))
        {
          if (fieldStringValue.Contains("\\S"))
            textData.AssignReplaceAVSMaterial(true, true);
          else
            textData.AssignReplaceAVSMaterial(false, true);
        }
        this.SetFieldValue(this.Field_Name, -1, -1, (List<RelationAttributeValuesCache>) null, (object) fieldStringValue, true, true, true, true, true, false);
      }
      if (!flag)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.GetObject(this.ObjectId).CheckIn();
        this.ObjectId = -this.ObjectId;
      }
    }
  }

  /// <summary>Получить ячейку в строке документа для заданной строки спецификации и атрибута</summary>
  /// <param name="attrInfo">Атрибут</param>
  /// <returns>Возвращает ячейку документа</returns>
  public TextData GetDocumentCellForAttribute(AvsRowAttributeInfo attrInfo, int productIndex)
  {
    List<TextData> cellsForBaseField = this.GetDocumentCellsForBaseField(attrInfo, productIndex);
    return cellsForBaseField.Count > 0 ? cellsForBaseField[0] : (TextData) null;
  }

  /// <summary>Получить ячейку в строке документа для заданной строки спецификации и атрибута</summary>
  /// <param name="baseFieldInfo">Атрибут</param>
  /// <param name="docRow">Строка документа</param>
  /// <param name="productIndex">Индекс исполнения для количества. Если -1, то для всех исполнений</param>
  /// <returns>Возвращает ячейку документа</returns>
  internal TextData GetDocumentCellForBaseField(
    AvsRowAttributeInfo baseFieldInfo,
    TableData docRow,
    int productIndex)
  {
    List<TextData> textDataList;
    if (docRow != null)
    {
      textDataList = new List<TextData>();
      this.GetDocumentCellsForBaseField(baseFieldInfo, docRow, productIndex, textDataList);
    }
    else
      textDataList = this.GetDocumentCellsForBaseField(baseFieldInfo, productIndex);
    return textDataList.FirstOrDefault<TextData>();
  }

  /// <summary>Получить ячейки в строке документа для заданной строки спецификации и атрибута</summary>
  /// <param name="baseFieldInfo">Атрибут</param>
  /// <param name="docRow">Строка документа</param>
  /// <param name="productIndex">Индекс исполнения для количества. Если -1, то для всех исполнений</param>
  /// <param name="cells">Возвращает найденные ячейки. Ищет только 1 в строке</param>
  /// <returns>Возвращает список ячеек документа</returns>
  internal void GetDocumentCellsForBaseField(
    AvsRowAttributeInfo baseFieldInfo,
    TableData docRow,
    int productIndex,
    List<TextData> cells)
  {
    if (baseFieldInfo == null)
      throw new ArgumentNullException(nameof (baseFieldInfo));
    if (docRow == null)
      throw new ArgumentNullException(nameof (docRow));
    if (cells == null)
      throw new ArgumentNullException(nameof (cells));
    List<AvsRowAttributeInfo> docRowFields = this.DocRowFields;
    if (docRowFields == null)
      return;
    bool flag = AVSRow.IsCountField(baseFieldInfo);
    List<TextData> collection = new List<TextData>();
    TextCellEnumerator textCellEnumerator = new TextCellEnumerator(docRow);
    int productIndexForDocRow = this.GetFirstProductIndexForDocRow((DocumentTreeNode) docRow);
    foreach (AvsRowAttributeInfo rowAttributeInfo in docRowFields)
    {
      if (textCellEnumerator.MoveNext())
      {
        TextData current = textCellEnumerator.Current;
        if (rowAttributeInfo != null)
        {
          if (this.IsFormB & flag && AVSRow.IsCountFormBCell(true, current) && productIndex != -1)
          {
            if (productIndex == productIndexForDocRow)
            {
              cells.Add(current);
              break;
            }
            ++productIndexForDocRow;
          }
          else
          {
            if (rowAttributeInfo.AttrSrc == FieldSource.DocumentRowField && current.Id == baseFieldInfo.Name)
            {
              cells.Add(current);
              break;
            }
            if (rowAttributeInfo.Equals((AttributeInfo) baseFieldInfo))
            {
              cells.Add(current);
              break;
            }
            if (cells.IsEmpty<TextData>() && this.IsCorrespondingCellForAttribute((AttributeInfo) baseFieldInfo, current.Name))
              collection.Add(current);
          }
        }
      }
      else
        break;
    }
    if (cells.Count != 0 || collection.Count <= 0)
      return;
    cells.AddRange((IEnumerable<TextData>) collection);
  }

  /// <summary>Получить ячейку в строке документа для заданной строки спецификации и атрибута</summary>
  /// <param name="baseFieldInfo">Атрибут</param>
  /// <param name="productIndex">Индекс исполнения для количества. Если -1, то для всех исполнений</param>
  /// <returns>Возвращает коллекцию ячеек документа для заданного атрибута</returns>
  internal List<TextData> GetDocumentCellsForBaseField(
    AvsRowAttributeInfo baseFieldInfo,
    int productIndex)
  {
    List<TextData> cells = new List<TextData>();
    if (!this.HasDocNodes)
      return cells;
    foreach (TableData docNode in this.DocNodes)
      this.GetDocumentCellsForBaseField(baseFieldInfo, docNode, productIndex, cells);
    return cells;
  }

  private bool IsCorrespondingAttributes(AttributeInfo attrInfo, AttributeInfo cellAttrInfo)
  {
    return attrInfo.Equals(cellAttrInfo);
  }

  private bool IsCorrespondingCellForAttribute(AttributeInfo attrInfo, string cellName)
  {
    return cellName == attrInfo.Name || (!this.IsNoteRow ? 0 : (cellName == Chapter.NoteRowTextCellName ? 1 : 0)) != 0 && (this.Field_Name.Equals(attrInfo) || attrInfo.IsDocField && attrInfo.Name == AVSRow.DocAttr_Name);
  }

  internal static bool IsPosDesignationFieldName(string attributeName)
  {
    return attributeName == AVSRow.DocAttr_PosDesignation;
  }

  /// <summary>Получить ячейки заданной строки документа для заданного исполнения, в которых присутствует атрибут</summary>
  /// <param name="attrInfo">Атрибут</param>
  /// <param name="docRow">Строка документа</param>
  /// <param name="productIndex">Индекс исполнения для количества. Если -1, то для всех исполнений</param>
  /// <param name="useDefaultCellByName">Искать ячейку с совпадающим именем, если атрибут не используется в строке</param>
  /// <param name="cells">Возвращает найденные ячейки. Ищет только 1 в строке</param>
  /// <returns>Возвращает список ячеек документа</returns>
  internal void GetDocumentCellsForAttribute(
    AvsRowAttributeInfo attrInfo,
    TableData docRow,
    int productIndex,
    bool useDefaultCellByName,
    List<TextData> cells)
  {
    if (cells == null)
      throw new ArgumentNullException(nameof (cells));
    bool flag = AVSRow.IsCountField(attrInfo);
    List<TextData> collection = new List<TextData>(1);
    int productIndexForDocRow = this.GetFirstProductIndexForDocRow((DocumentTreeNode) docRow);
    foreach (TextData cell in (IEnumerable<TextData>) docRow.TextCellsEnumerator)
    {
      if (this.IsFormB & flag && AVSRow.IsCountFormBCell(true, cell) && productIndex != -1)
      {
        if (productIndex == productIndexForDocRow)
        {
          cells.Add(cell);
          break;
        }
        ++productIndexForDocRow;
      }
      if (attrInfo.AttrSrc == FieldSource.DocumentRowField)
      {
        if (cell.Id == attrInfo.Name)
        {
          cells.Add(cell);
          break;
        }
        break;
      }
      CellOutputMapping attributeMapping = this.GetCellAttributeMapping(cell);
      if (attributeMapping == null || !attributeMapping.IsEmpty)
      {
        if (attributeMapping.ContainsAttribute((Func<AttributeInfo, bool>) (a => this.IsCorrespondingAttributes(a, (AttributeInfo) attrInfo))))
        {
          cells.Add(cell);
          break;
        }
        if (useDefaultCellByName && cells.IsEmpty<TextData>() && this.IsCorrespondingCellForAttribute((AttributeInfo) attrInfo, cell.Name))
          collection.Add(cell);
      }
    }
    if (!useDefaultCellByName || cells.Count != 0 || collection.Count <= 0)
      return;
    cells.AddRange((IEnumerable<TextData>) collection);
  }

  /// <summary>Получить ячейки строки документа для заданного исполнения, в которых присутствует атрибут</summary>
  /// <param name="attrInfo">Атрибут</param>
  /// <param name="productIndex">Индекс исполнения для количества. Если -1, то для всех исполнений</param>
  /// <param name="useDefaultCellByName">Искать ячейку с совпадающим именем, если атрибут не используется в строке</param>
  /// <returns>Возвращает коллекцию ячеек документа для заданного атрибута</returns>
  internal List<TextData> GetDocumentCellsForAttribute(
    AvsRowAttributeInfo attrInfo,
    int productIndex,
    bool useDefaultCellByName = false)
  {
    List<TextData> cells = new List<TextData>();
    if (!this.HasDocNodes)
      return cells;
    foreach (TableData docNode in this.DocNodes)
      this.GetDocumentCellsForAttribute(attrInfo, docNode, productIndex, useDefaultCellByName, cells);
    return cells;
  }

  /// <summary>Подсчитать количество ячеек "Количество" в строке документа</summary>
  /// <param name="docRowFields"></param>
  /// <returns></returns>
  public static int CalcCountCellsCount(List<AvsRowAttributeInfo> docRowFields)
  {
    if (docRowFields == null)
      throw new ArgumentNullException(nameof (docRowFields));
    int num = 0;
    for (int index = 0; index < docRowFields.Count; ++index)
    {
      if (AVSRow.IsCountField(docRowFields[index]))
        ++num;
    }
    return num;
  }

  /// <summary>Пересчёт количество пропусков ПЕРЕД записью</summary>
  /// <param name="skipLinesSchema">Настройки пропусков</param>
  /// <param name="page">Страница</param>
  /// <param name="calledFromSectionUpdate">Вызов из общего обновления пропусков раздела.
  /// Для блокировки лишних вызовов UpdateSkipLinesAfter</param>
  /// <param name="updateUI">Обновлять интерфейс</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateSkipLinesBefore(
    SkipLinesSchema skipLinesSchema,
    PageData page,
    bool calledFromSectionUpdate,
    bool updateUI,
    bool updateLayout)
  {
    if (!this.HasDocNodes)
      return;
    if (this._skipLinesBefore.HasValue)
    {
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (page == null || this.docNodes[index].Page == page)
        {
          this.docNodes[index].SetAttributeValue(AVSDocument.DocAttr_SkipLinesBefore, this._skipLinesBefore.ToString(), false, false, false);
          this.docNodes[index].SetSkipCellsBefore((float) this._skipLinesBefore.Value, false, false, false);
          if (updateLayout)
            this.docNodes[index].SetNeedUpdateLayoutFlag(true, true, updateUI && index == this.docNodes.Count - 1, index == this.docNodes.Count - 1);
        }
      }
    }
    else if (this.IsNoteRow)
    {
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (page == null || this.docNodes[index].Page == page)
        {
          this.docNodes[index].SetSkipCellsBefore(skipLinesSchema != null ? (float) skipLinesSchema.BeforeNote : 0.0f, true, false, false);
          this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesBefore, false, false);
          if (updateLayout)
            this.docNodes[index].SetNeedUpdateLayoutFlag(true, true, updateUI, true);
        }
      }
    }
    else
    {
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (page == null || this.docNodes[index].Page == page)
        {
          this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesBefore, false, false);
          this.docNodes[index].SetSkipCellsBefore(0.0f, true, false, false);
          if (updateLayout)
            this.docNodes[index].SetNeedUpdateLayoutFlag(true, true, updateUI && index == this.docNodes.Count - 1, updateUI && index == this.docNodes.Count - 1);
        }
      }
      if (calledFromSectionUpdate)
        return;
      AVSRow prevRow = this.GetPrevRow();
      if (prevRow != null)
        prevRow.UpdateSkipLinesAfter(skipLinesSchema, page, updateUI, updateLayout);
      else
        this.Section?.UpdateSkipLinesAfter(skipLinesSchema, page, updateUI, updateLayout);
    }
  }

  /// <summary>Индекс строки в разделе</summary>
  [Browsable(false)]
  public int Index
  {
    get
    {
      if (this.Section != null && (this.index == -1 || this.index >= this.Section.Rows.Count || this.Section.Rows[this.index] != this))
        this.index = this.Section.Rows.IndexOf(this);
      return this.index;
    }
    set => this.index = value;
  }

  /// <summary>Пересчёт количество пропусков ПОСЛЕ записи</summary>
  /// <param name="skipLinesSchema">Настройки пропусков</param>
  /// <param name="page">Страница</param>
  /// <param name="updateUI">Обновлять интерфейс</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateSkipLinesAfter(
    SkipLinesSchema skipLinesSchema,
    PageData page,
    bool updateUI,
    bool updateLayout)
  {
    if (!this.HasDocNodes)
      return;
    if (this._skipLinesAfter.HasValue)
    {
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (page == null || this.docNodes[index].Page == page)
        {
          this.docNodes[index].SetAttributeValue(AVSDocument.DocAttr_SkipLinesAfter, this._skipLinesAfter.ToString(), false, false, false);
          this.docNodes[index].SetSkipCellsAfter((float) this._skipLinesAfter.Value, false, updateUI, updateLayout);
        }
      }
    }
    else if (this.IsNoteRow)
    {
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (page == null || this.docNodes[index].Page == page)
        {
          this.docNodes[index].SetSkipCellsAfter(skipLinesSchema != null ? (float) skipLinesSchema.AfterNote : 0.0f, true, false, false);
          this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
          if (updateLayout)
            this.docNodes[index].SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
        }
      }
    }
    else
    {
      AVSRow nextRow = this.GetNextRow();
      if (nextRow != null)
      {
        if (this.Class != null && nextRow.Class != null)
        {
          if (nextRow.Class != this.Class)
          {
            for (int index = 0; index < this.docNodes.Count; ++index)
            {
              if (page == null || this.docNodes[index].Page == page)
              {
                this.docNodes[index].SetSkipCellsAfter(skipLinesSchema != null ? (float) skipLinesSchema.BetweenDifferentObjTypes : 0.0f, true, updateUI, updateLayout);
                this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
              }
            }
          }
          else
          {
            for (int index = 0; index < this.docNodes.Count; ++index)
            {
              if (page == null || this.docNodes[index].Page == page)
              {
                this.docNodes[index].SetSkipCellsAfter(skipLinesSchema != null ? (float) skipLinesSchema.BetweenSameObjTypes : 0.0f, true, updateUI, updateLayout);
                this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
              }
            }
          }
        }
        else if (skipLinesSchema != null && this.avsDocument.IsSameProductDesignations(this, nextRow))
        {
          for (int index = 0; index < this.docNodes.Count; ++index)
          {
            if (page == null || this.docNodes[index].Page == page)
            {
              this.docNodes[index].SetSkipCellsAfter((float) skipLinesSchema.BetweenArtVariants, true, updateUI, updateLayout);
              this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
            }
          }
        }
        else
        {
          if (this.Designation == null || nextRow.Designation == null)
            return;
          if (skipLinesSchema != null && skipLinesSchema.CompareDesignationSchema.IsDesiagnationsAreSame(this.Designation, nextRow.Designation))
          {
            for (int index = 0; index < this.docNodes.Count; ++index)
            {
              if (page == null || this.docNodes[index].Page == page)
              {
                this.docNodes[index].SetSkipCellsAfter((float) skipLinesSchema.BetweenSameDesignations, true, updateUI, updateLayout);
                this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
              }
            }
          }
          else
          {
            for (int index = 0; index < this.docNodes.Count; ++index)
            {
              if (page == null || this.docNodes[index].Page == page)
              {
                this.docNodes[index].SetSkipCellsAfter(skipLinesSchema != null ? (float) skipLinesSchema.BetweenDifferentDesignations : 0.0f, true, updateUI, updateLayout);
                this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
              }
            }
          }
        }
      }
      else if (this.Section != null)
      {
        for (int index = 0; index < this.docNodes.Count; ++index)
        {
          if (page == null || this.docNodes[index].Page == page)
          {
            this.docNodes[index].SetSkipCellsAfter(0.0f, true, updateUI, updateLayout);
            this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
          }
        }
      }
      else
      {
        for (int index = 0; index < this.docNodes.Count; ++index)
        {
          if (page == null || this.docNodes[index].Page == page)
          {
            this.docNodes[index].SetSkipCellsAfter(0.0f, true, updateUI, updateLayout);
            this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
          }
        }
      }
    }
  }

  /// <summary>Пересчёт количество пропусков ПЕРЕД и ПОСЛЕ записи</summary>
  /// <param name="skipLinesSchema">Настройки пропусков</param>
  /// <param name="page">Страница</param>
  /// <param name="calledFromSectionUpdate">Вызов из общего обновления пропусков раздела.
  /// Для блокировки лишних вызовов UpdateSkipLinesAfter</param>
  /// <param name="checkFirstOnPage">Проверяем элементы только первые на странице</param>
  /// <param name="updateUI">Обновлять интерфейс</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateSkipLinesOld(
    SkipLinesSchema skipLinesSchema,
    PageData page,
    bool calledFromSectionUpdate,
    bool updateUI,
    bool updateLayout,
    bool checkFirstOnPage = false)
  {
    this.UpdateSkipLinesBefore(skipLinesSchema, page, calledFromSectionUpdate, updateUI, updateLayout);
    this.UpdateSkipLinesAfter(skipLinesSchema, page, updateUI, updateLayout);
    if (checkFirstOnPage)
      return;
    SkipLinesStruct.UpdatePrevElement((object) this, false, false);
  }

  public void UpdateSkipLines(SkipLinesSchema skipLinesSchema, SkipLinesStruct str)
  {
    for (int index = 0; index < this.docNodes.Count; ++index)
    {
      if (this._skipLinesBefore.HasValue)
        this.docNodes[index].SetAttributeValue(AVSDocument.DocAttr_SkipLinesBefore, this._skipLinesBefore.ToString(), false, false, false);
      else
        this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesBefore, false, false);
      if (!float.IsNaN(str.SkipBefore))
        this.docNodes[index].SetSkipCellsBefore(str.SkipBefore, !this._skipLinesBefore.HasValue, false, false);
      if (this._skipLinesAfter.HasValue)
        this.docNodes[index].SetAttributeValue(AVSDocument.DocAttr_SkipLinesAfter, this._skipLinesAfter.ToString(), false, false, false);
      else
        this.docNodes[index].RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
      if (!float.IsNaN(str.SkipAfter))
        this.docNodes[index].SetSkipCellsAfter(str.SkipAfter, !this._skipLinesAfter.HasValue, false, false);
    }
  }

  public SkipLinesStruct GetSkipLines(
    SkipLinesSchema skipLinesSchema,
    List<SkipLinesStruct> structs)
  {
    int num1 = 0;
    int num2 = 0;
    if (this._skipLinesBefore.HasValue)
      num1 = this._skipLinesBefore.Value;
    else if (this.IsNoteRow)
    {
      if (this.DocNode != null)
        num1 = !this.DocNode.IsDynamicGroupHeader ? (skipLinesSchema != null ? skipLinesSchema.BeforeNote : 0) : (skipLinesSchema != null ? skipLinesSchema.BeforeDynamicGroup : (int) this.DocNode.SkipCellsBefore);
    }
    else
      num1 = 0;
    if (this._skipLinesAfter.HasValue)
      num2 = this._skipLinesAfter.Value;
    else if (skipLinesSchema != null)
    {
      if (this.IsNoteRow)
      {
        if (this.DocNode != null)
          num2 = !this.DocNode.IsDynamicGroupHeader ? skipLinesSchema.AfterNote : (skipLinesSchema != null ? skipLinesSchema.AfterDynamicGroup : (int) this.DocNode.SkipCellsAfter);
      }
      else
      {
        AVSRow nextRow = this.GetNextRow();
        if (nextRow != null)
        {
          bool flag = false;
          if (skipLinesSchema.NumberingPositions != NumberingPositionsEnum.NotUse)
          {
            AvsRowAttributeInfo fieldPosition = this.Field_Position;
            string s1 = Convert.ToString(this.GetFieldValue(fieldPosition, 0, -1, false, true));
            string s2 = Convert.ToString(nextRow.GetFieldValue(fieldPosition, 0, -1, false, true));
            int num3 = -1;
            int result = -1;
            ref int local = ref num3;
            if (int.TryParse(s1, out local) && int.TryParse(s2, out result) && result >= num3)
            {
              flag = true;
              num2 = skipLinesSchema.NumberingPositions != NumberingPositionsEnum.Use ? result - num3 : result - num3 - 1;
            }
          }
          if (!flag)
            num2 = this.Class == null || nextRow.Class == null ? (!this.avsDocument.IsSameProductDesignations(this, nextRow) ? (!skipLinesSchema.CompareDesignationSchema.IsDesiagnationsAreSame(this.Designation, nextRow.Designation) ? skipLinesSchema.BetweenDifferentDesignations : skipLinesSchema.BetweenSameDesignations) : skipLinesSchema.BetweenArtVariants) : (!(nextRow.Class != this.Class) ? skipLinesSchema.BetweenSameObjTypes : skipLinesSchema.BetweenDifferentObjTypes);
        }
      }
    }
    else if (this.DocNode != null)
      num2 = (int) this.DocNode.SkipCellsBefore;
    SkipLinesStruct skipLines = new SkipLinesStruct(this);
    skipLines.SkipBefore = (float) num1;
    skipLines.SkipAfter = (float) num2;
    skipLines.BeforeSetted = this.SkipLinesBeforeIsOverriden;
    skipLines.AfterSetted = this.SkipLinesAfterIsOverriden;
    structs.Add(skipLines);
    return skipLines;
  }

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="_objType">Тип объекта</param>
  /// <param name="relId">Идентификатор связи</param>
  /// <param name="relGuid">Глобальный идентификатор связи</param>
  /// <param name="relType">Тип связи</param>
  /// <param name="projGuid">Глобальный идентификатор исполнения</param>
  /// <param name="projId">Идентификатор исполнения</param>
  public AVSRow(
    AVSDocument avsDocument,
    long objId,
    Guid objGuid,
    int objType,
    long relId,
    Guid relGuid,
    int relType,
    Guid projGuid,
    long projId)
  {
    this.avsDocument = avsDocument;
    this.rowID = new DBRelationInfo(relGuid, relId, relType, projGuid, projId, objGuid, objId, objType, (string) null);
    this.Init();
  }

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="objGuid">Глобальный идентификатор объекта</param>
  /// <param name="_objType">Тип объекта</param>
  /// <param name="relId">Идентификатор связи</param>
  /// <param name="relGuid">Глобальный идентификатор связи</param>
  /// <param name="relType">Тип связи</param>
  /// <param name="projGuid">Глобальный идентификатор исполнения</param>
  /// <param name="projId">Идентификатор исполнения</param>
  /// <param name="relationData">Атрибуты связи</param>
  /// <param name="objectData">Атрибуты объекта</param>
  public AVSRow(
    AVSDocument avsDocument,
    long objId,
    Guid objGuid,
    int objType,
    long relId,
    Guid relGuid,
    int relType,
    Guid projGuid,
    long projId,
    RelationAttributeValuesCache relationData,
    AttributeValuesCache objectData)
  {
    this.avsDocument = avsDocument;
    string objectCaption = (string) null;
    if (relationData != null)
    {
      this.relations = new List<RelationAttributeValuesCache>(1);
      this.relations.Add(relationData);
      objectCaption = relationData.ObjectCaption;
    }
    if (objectData != null)
    {
      this.objectAttributesCache = objectData;
      objectCaption = objectData.ObjectCaption;
    }
    else if (relationData != null)
      this.objectAttributesCache = relationData.ObjectAttributesCache;
    this.rowID = new DBRelationInfo(relGuid, relId, relType, projGuid, projId, objGuid, objId, objType, objectCaption);
    this.Init();
  }

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="relationData">Атрибуты связи</param>
  /// <param name="objectData">Атрибуты объекта</param>
  public AVSRow(
    AVSDocument avsDocument,
    RelationAttributeValuesCache relationData,
    AttributeValuesCache objectData)
  {
    this.avsDocument = avsDocument;
    if (relationData != null)
    {
      this.relations = new List<RelationAttributeValuesCache>(1);
      this.relations.Add(relationData);
    }
    if (objectData != null)
      this.objectAttributesCache = objectData;
    else if (relationData != null)
      this.objectAttributesCache = relationData.ObjectAttributesCache;
    if (relationData != null)
      this.rowID = new DBRelationInfo(relationData.RelationGuid, relationData.RelationId, relationData.RelationType, relationData.ProjectGuid, relationData.ProjectId, relationData.ObjectGuid, relationData.ObjectId, relationData.ObjectType, relationData.ObjectCaption);
    else if (objectData != null)
      this.rowID = new DBRelationInfo(Guid.Empty, -1L, -1, Guid.Empty, -1L, objectData.ObjectGuid, objectData.ObjectId, objectData.ObjectType, objectData.ObjectCaption);
    this.Init();
  }

  /// <summary>Конструктор</summary>
  public AVSRow(AVSDocument avsDocument)
  {
    this.avsDocument = avsDocument;
    this.Init();
  }

  public void Init()
  {
  }

  /// <summary>Скопировать данные из ячеек строки документа</summary>
  /// <param name="srcDocRowNode">Строка источник данных</param>
  /// <param name="dstDocRowNode">Строка приёмник данных</param>
  public static void CopyDataFromToDocRow(TableData srcDocRowNode, TableData dstDocRowNode)
  {
    if (srcDocRowNode.NodesCount == 0)
      return;
    List<TextData> list1 = srcDocRowNode.TextCellsEnumerator.ToList<TextData>();
    List<TextData> list2 = dstDocRowNode.TextCellsEnumerator.ToList<TextData>();
    foreach (TextData textData in (IEnumerable<TextData>) srcDocRowNode.TextCellsEnumerator)
    {
      TextData srcTextCell = textData;
      if (!string.IsNullOrEmpty(srcTextCell.Name))
      {
        TextData destination = list2.Find((Predicate<TextData>) (dst => dst.Name == srcTextCell.Name));
        if (destination != null)
        {
          srcTextCell.CopyTextAndFormatTo(destination);
          if (srcTextCell.Name == "Обозначение")
            destination.ParagraphFormat.IdentLeft = new float?(0.0f);
          list2.Remove(destination);
          list1.Remove(srcTextCell);
        }
      }
    }
    foreach (TextData textData in list1.OfType<TextData>())
    {
      if (!string.IsNullOrEmpty(textData.TemplateId) && !textData.TemplateId.Contains("Количество"))
        dstDocRowNode.SetAttributeValue(textData.TemplateId, textData.Text, false, false, false);
    }
    if ((srcDocRowNode.overrideFlags & OverrideFlags.SkipBefore) != OverrideFlags.None && (srcDocRowNode.overrideFlags2 & OverrideFlags2.SkipBeforeForPlugin) == OverrideFlags2.None)
    {
      dstDocRowNode.SetAttributeValue(AVSDocument.DocAttr_SkipLinesBefore, srcDocRowNode.SkipCellsBefore.ToString(), false, false, false);
      dstDocRowNode.SetSkipCellsBefore(srcDocRowNode.SkipCellsBefore, false, false, false);
      dstDocRowNode.SetNeedUpdateLayoutFlag(true, true, false, false);
    }
    else
      dstDocRowNode.RemoveAttribute(AVSDocument.DocAttr_SkipLinesBefore, false, false);
    if ((srcDocRowNode.overrideFlags & OverrideFlags.SkipAfter) != OverrideFlags.None && (srcDocRowNode.overrideFlags2 & OverrideFlags2.SkipAfterForPlugin) == OverrideFlags2.None)
    {
      dstDocRowNode.SetAttributeValue(AVSDocument.DocAttr_SkipLinesAfter, srcDocRowNode.SkipCellsAfter.ToString(), false, false, false);
      dstDocRowNode.SetSkipCellsAfter(srcDocRowNode.SkipCellsAfter, false, false, false);
    }
    else
      dstDocRowNode.RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
    if ((srcDocRowNode.overrideFlags & OverrideFlags.FromNewPage) != OverrideFlags.None)
      dstDocRowNode.SetFromNewPage(srcDocRowNode.FromNewPage, false, false);
    if ((srcDocRowNode.overrideFlags & OverrideFlags.KeepWithNext) != OverrideFlags.None)
      dstDocRowNode.SetKeepWithNext(srcDocRowNode.KeepWithNext, false, false);
    HybridDictionary hybridDictionary = new HybridDictionary();
    srcDocRowNode.GetAttributes((IDictionary) hybridDictionary, false);
    dstDocRowNode.AddAdditionalAttributes((IDictionary) hybridDictionary);
  }

  /// <summary>Получить шаблон строки документа для записи</summary>
  /// <returns></returns>
  public TableData GetDocRowTemplate() => this.GetDocRowTemplate(this.DocNode);

  /// <summary>Получить шаблон строки документа для записи</summary>
  /// <param name="docRowNode">Строка документа у которой можно получить шаблон</param>
  /// <returns></returns>
  public TableData GetDocRowTemplate(TableData docRowNode)
  {
    TableData docRowTemplate = (TableData) null;
    if (this.IsNoteRow && docRowNode != null && docRowNode.TemplateId != null && docRowNode.TemplateId != "" && this.avsDocument.Document != null)
    {
      docRowTemplate = this.avsDocument.Document.Template.FindNode(docRowNode.TemplateId) as TableData;
      if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
      {
        if (docRowTemplate == null || docRowTemplate.Page != null && this.avsDocument.IsFormBPage(docRowTemplate.Page) != this.IsFormB)
          docRowTemplate = !this.IsFormB ? this.avsDocument.FindNoteTemplateByName(docRowNode.Name) : this.avsDocument.FindNoteTemplateByName_VarDataFormV(docRowNode.Name);
      }
      else if (docRowTemplate == null)
        docRowTemplate = this.avsDocument.FindNoteTemplateByName(docRowNode.Name);
    }
    if (docRowTemplate == null)
      docRowTemplate = this.avsDocument.AvsDocumentForm != AVSDocumentForm.V || !this.IsFormB ? this.avsDocument.avsRowTemplate : this.avsDocument.avsRowFormBTemplate;
    return docRowTemplate;
  }

  /// <summary>Получить шаблон строки экспортного документа для записи</summary>
  /// <param name="docRowNode">Строка документа у которой можно получить шаблон</param>
  /// <returns></returns>
  public TableData GetDocRowTemplate_Exp(TableData docRowNode)
  {
    TableData docRowTemplateExp = (TableData) null;
    if (this.IsNoteRow && docRowNode != null && docRowNode.TemplateId != null && docRowNode.TemplateId != "" && this.avsDocument.Document != null)
    {
      docRowTemplateExp = this.avsDocument.Document.Template.FindNode(docRowNode.TemplateId) as TableData;
      if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
      {
        if (docRowTemplateExp == null || docRowTemplateExp.Page != null && this.avsDocument.IsFormBPage(docRowTemplateExp.Page) != this.IsFormB)
          docRowTemplateExp = !this.IsFormB ? this.avsDocument.FindNoteTemplateByName(docRowNode.Name) : this.avsDocument.FindNoteTemplateByName_VarDataFormV(docRowNode.Name);
      }
      else if (docRowTemplateExp == null)
        docRowTemplateExp = this.avsDocument.FindNoteTemplateByName(docRowNode.Name);
    }
    if (docRowTemplateExp == null)
      docRowTemplateExp = this.avsDocument.AvsDocumentForm != AVSDocumentForm.V || !this.IsFormB ? this.avsDocument.avsRowExpTemplate : this.avsDocument.avsRowFormBTemplate;
    return docRowTemplateExp;
  }

  public TableData FindDocRowForProduct(int productIndex)
  {
    if (this.DocNodes.IsNullOrEmpty<TableData>())
      return (TableData) null;
    if (!this.IsFormB)
      return this.DocNodes[0];
    for (int index = 0; index < this.DocNodes.Count; ++index)
    {
      int productIndexForDocRow = this.GetFirstProductIndexForDocRow((DocumentTreeNode) this.DocNodes[index]);
      if (productIndex >= productIndexForDocRow && productIndex < productIndexForDocRow + this.avsDocument.RowProductCount)
        return this.DocNodes[index];
    }
    return (TableData) null;
  }

  public TableData UpdateDocRow()
  {
    return this.UpdateDocRow((TableData) null, (List<AvsRowAttributeInfo>) null, false, true, false, EmptyRowUpdateMode.DontChange);
  }

  /// <summary>Создать строку спецификации в документе или обновить данные</summary>
  /// <param name="specRowTemplate">Шаблон строки. Если аргумент null, то вызывается метод GetDocRowTemplate()</param>
  /// <param name="docRowFields">Список атрибутов для граф строки</param>
  /// <param name="reCreateDocNode">Пересоздавать строки документа</param>
  /// <param name="updateCountB">Обновить графу "Количество" для формы Б</param>
  /// <param name="updateTemplate">Обновить шаблон строки</param>
  /// <param name="updateMode">Режим обновления записей с пустым количеством</param>
  /// <returns>Строка спецификации в документе</returns>
  public TableData UpdateDocRow(
    TableData specRowTemplate,
    List<AvsRowAttributeInfo> docRowFields,
    bool reCreateDocNode,
    bool updateCountB,
    bool updateTemplate,
    EmptyRowUpdateMode updateMode)
  {
    if (this.avsDocument == null || !this.avsDocument.IsGeneratedDoc && this.avsDocument.ReadOnly)
      return (TableData) null;
    if (this.IsHiddenRow)
    {
      this.DocNodes = new List<TableData>();
      return (TableData) null;
    }
    if (specRowTemplate == null)
      specRowTemplate = this.GetDocRowTemplate();
    if (docRowFields == null)
      docRowFields = this.DocRowFields;
    this.avsDocument.Lock_DocCell_TextChanged();
    ++this._suspendUpdateNote;
    try
    {
      if (!reCreateDocNode & updateTemplate)
        reCreateDocNode = !this.ValidateDocNodesTemplate(this.DocNodes, specRowTemplate);
      TableData docNode = this.docNode;
      if (reCreateDocNode)
        this.DocNodes = new List<TableData>();
      List<TableData> docRows = new List<TableData>(this.avsDocument.productsInfo.Count / 10 + 1);
      if (this.docNodes.Count > 0)
        docRows.AddRange((IEnumerable<TableData>) this.docNodes);
      int num1 = 0;
      if (this.avsDocument.productsInfo.Count == 0)
        num1 = -1;
      int rowIndex = 0;
      bool flag = false;
      while (num1 < this.avsDocument.productsInfo.Count)
      {
        int num2 = num1;
        bool curRowChanged;
        bool newRow;
        TableData docRow = this.UpdateDocRowNodeForProduct(docRows, rowIndex, num1, docNode, reCreateDocNode, updateMode, out curRowChanged, out newRow);
        flag |= curRowChanged;
        if (docRow == null)
        {
          num1 += this.avsDocument.RowProductCount;
        }
        else
        {
          this.docNode = docRow;
          this.docNode.Tag = (object) this;
          this.UpdateSortLinksInDocRow(rowIndex, docRow);
          num1 = !this.NewCellMappingMode ? this.UpdateDocRowFields_OLD(docRowFields, docRow, newRow, num1, updateCountB) : this.UpdateDocRowFields(docRow, num1);
          if (this.IsFormB && this.IsNoteRow)
            num1 += this.avsDocument.RowProductCount;
          ++rowIndex;
          if (num2 == num1)
            break;
        }
      }
      if (rowIndex < docRows.Count)
      {
        for (int index = docRows.Count - 1; index >= rowIndex; --index)
          docRows.RemoveAt(index);
        flag = true;
      }
      this.docNode = docNode;
      if (flag | reCreateDocNode)
        this.DocNodes = docRows;
    }
    finally
    {
      --this._suspendUpdateNote;
      if (this._suspendUpdateNote == 0 && this._needUpdateNote)
        this.UpdateNoteDocCellText();
      this.avsDocument.Unlock_DocCell_TextChanged();
    }
    return this.DocNode;
  }

  internal CellOutputMapping GetCellAttributeMapping(TextData cell)
  {
    string cellId = cell != null ? cell.TemplateId : throw new ArgumentNullException(nameof (cell));
    if (this.IsFormB && cellId.IndexOf(AVSRow.DocAttr_Count) == 0)
      cellId = AVSRow.DocAttr_Count;
    return this.GetCellAttributeMapping(cellId);
  }

  internal bool ShowMeasureUnitsInNote
  {
    get
    {
      if (this.NewCellMappingMode && this.NoteCellMapping != null)
        return this.NoteCellMapping.ContainsAttribute((AttributeInfo) AvsIDCache.CountMeasureAttrInfo);
      return this.avsDocument?.noteFieldSettings == null || (this.avsDocument.noteFieldSettings.Options & NoteFieldOptions.ShowMeasureUnits) != 0;
    }
  }

  public CellOutputMapping GetCellAttributeMapping(string cellId)
  {
    string sectionGuid = this.section?.ChapterGuid.ToString() ?? "00000000-0000-0000-0000-000000000000";
    string objTypeGuid = MetaDataHelper.GetObjectTypeGuid(this.ObjType).ToString();
    return this.avsDocument.CellTextOutputAttributeMappingSettings.GetCellMapping(sectionGuid, cellId, objTypeGuid);
  }

  /// <summary>Возвращает базовое поле соответствующее графе документа.
  /// Например, для "Наименования" в настройках может быть прописано несколько атрибутов,
  /// но при обработке необходимо знать, что поле соответствует графе "Наименование"</summary>
  /// <param name="cell">Поле записи</param>
  /// <returns></returns>
  private AvsRowAttributeInfo GetCellBaseFieldInfo(TextData cell, out int productIndex)
  {
    AvsRowAttributeInfo attributeInfoForCell = this.GetAttributeInfoForCell(cell, out productIndex);
    if (attributeInfoForCell != null && attributeInfoForCell.IsDocField)
    {
      if (cell.Name == AVSRow.DocAttr_Name)
        return this.Field_Name;
      if (cell.Name == AVSRow.DocAttr_Designation)
        return this.Field_Designation;
      if (cell.Name == AVSRow.DocAttr_Zone)
        return this.Field_Zone;
      if (cell.Name == AVSRow.DocAttr_Format)
        return this.Field_Format;
      if (cell.Name.Contains(AVSRow.DocAttr_Count))
        return this.Field_Count;
      if (cell.Name.Contains(AVSRow.DocAttr_Note))
        return this.Field_Note;
    }
    return attributeInfoForCell;
  }

  private int UpdateDocRowFields(TableData docRow, int startProductIndex)
  {
    if (docRow == null)
      throw new ArgumentNullException(nameof (docRow));
    int productIndex = startProductIndex;
    if (this.IsNoteRow)
      return productIndex;
    foreach (TextData cell in (IEnumerable<TextData>) docRow.TextCellsEnumerator)
    {
      CellOutputMapping attributeMapping = this.GetCellAttributeMapping(cell);
      AvsRowAttributeInfo cellBaseFieldInfo = this.GetCellBaseFieldInfo(cell, out int _);
      productIndex = this.UpdateCell(productIndex, cell, attributeMapping, cellBaseFieldInfo);
    }
    return productIndex;
  }

  private int UpdateDocRowFields_OLD(
    List<AvsRowAttributeInfo> docRowFields,
    TableData docRow,
    bool newRow,
    int productIndex,
    bool updateCountB)
  {
    TextCellEnumerator textCellEnumerator = new TextCellEnumerator(docRow);
    foreach (TextData cell in (IEnumerable<TextData>) textCellEnumerator)
    {
      AVSRow.UpdateProtectedCharsZoneInCell(cell);
      AvsRowAttributeInfo docRowField = docRowFields == null || docRowFields.Count <= textCellEnumerator.Index ? (AvsRowAttributeInfo) null : docRowFields[textCellEnumerator.Index];
      if (docRowField != null)
      {
        cell.AssignReplaceOldAVSSpecChars(true, true);
        if (this.avsDocument.IsSpecification && this.Field_Name.Equals((AttributeInfo) docRowField) && !cell.IsOverridden3(OverrideFlags3.ReplaceAVSMaterial))
          cell.AssignReplaceAVSMaterial(true, true);
        if (!this.IsNoteRow)
          productIndex = this.UpdateCell(productIndex, cell, (CellOutputMapping) null, docRowField);
      }
    }
    return productIndex;
  }

  private int UpdateCell(
    int productIndex,
    TextData cell,
    CellOutputMapping cellMapping,
    AvsRowAttributeInfo baseField)
  {
    if (cellMapping == null && baseField == null)
      return productIndex;
    cell.AssignReplaceOldAVSSpecChars(true, true);
    if (this.avsDocument.IsSpecification && this.Field_Name.Equals((AttributeInfo) baseField) && !cell.IsOverridden3(OverrideFlags3.ReplaceAVSMaterial))
      cell.AssignReplaceAVSMaterial(true, true);
    if (this.IsFormB && AVSRow.IsCountField(baseField))
    {
      if (!this.NewCellMappingMode)
        this.UpdateCellRefToTextSource(cell, baseField);
      this.UpdateCountCellFormBReadonly(productIndex, cell);
      this.UpdateNoteDocCellText();
      ++productIndex;
    }
    else
    {
      if (baseField != null && baseField.AttrSrc != FieldSource.DocumentRowField)
        this.UpdateCellRefToTextSource(cell, baseField);
      this.UpdateCellReadonly(cell, baseField);
      if (this.Field_Name.Equals((AttributeInfo) baseField))
        this.UpdateNameDocCellText(cell, false, false);
      else if (baseField != null && this.IsFieldСorrelatedWithNote(baseField))
      {
        this.UpdateNoteDocCellText();
      }
      else
      {
        string textForDocCell = this.GetTextForDocCell(cellMapping, baseField, 0, productIndex, true, false);
        AVSRow.SetDocCellText(cell, textForDocCell);
        if (this.Field_Designation.Equals((AttributeInfo) baseField))
          cell.RemoveAttribute(AVSRow.CellAttrName_FullDesignation, false, false);
      }
    }
    return productIndex;
  }

  private void UpdateCellReadonly(TextData cell, AvsRowAttributeInfo fieldInfo)
  {
    if (fieldInfo == null)
      return;
    if (AVSRow.IsCountField(fieldInfo))
      cell.ReadOnly = this.HideCountForDocuments || !this.IsDocRelation && this.HasHiddenRelationForPosDesignationSumm;
    else if (this.Field_Format.Equals((AttributeInfo) fieldInfo))
    {
      cell.ReadOnly = this.IsDocRelation || MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing);
    }
    else
    {
      if (!this.IsDocRelation || !fieldInfo.IsRelationAttribute || fieldInfo.AttributeId != AvsIDCache.Attr_Position && fieldInfo.AttributeId != AvsIDCache.Attr_Zone)
        return;
      cell.ReadOnly = true;
    }
  }

  private void UpdateCountCellFormBReadonly(int productIndex, TextData cell)
  {
    if (!this.IsFormB)
      return;
    cell.ReadOnly = productIndex >= this.avsDocument.productsInfo.Count;
    if (this.GetRelationForProductIndex(productIndex, this.relations) != -1)
    {
      if (this.IsDocRelation)
        cell.ReadOnly = false;
      else if (!this.avsDocument.IsSpecification)
        cell.ReadOnly = true;
    }
    cell.ReadOnly |= this.HasHiddenRelationForPosDesignationSumm;
  }

  private TableData UpdateDocRowNodeForProduct(
    List<TableData> docRows,
    int rowIndex,
    int productIndex,
    TableData oldFirstDocNode,
    bool reCreateDocNode,
    EmptyRowUpdateMode updateMode,
    out bool curRowChanged,
    out bool newRow)
  {
    curRowChanged = false;
    newRow = false;
    int num1 = this.ProductGroup;
    bool flag = true;
    if (this.IsFormB && !this.IsNoteRow && (updateMode != EmptyRowUpdateMode.Create || num1 != -1))
    {
      flag = this.HasCountForProductInDocRow(productIndex);
      if (!flag && num1 == -1 && docRows.IsNullOrEmpty<TableData>())
        num1 = 0;
    }
    TableData tableData = this.FindDocRowForProduct(docRows, productIndex, rowIndex);
    int num2 = (int) ((double) productIndex / (double) this.avsDocument.RowProductCount);
    if (tableData == null)
    {
      if ((this.IsFormB || productIndex == 0) && (flag && !this.IsNoteRow | reCreateDocNode || updateMode == EmptyRowUpdateMode.Create || num1 != -1 && num2 == num1))
      {
        tableData = (TableData) this.GetDocRowTemplate(oldFirstDocNode).CloneFromTemplate(true, true);
        tableData.SetSkipCellsBefore(0.0f, true, false, false);
        tableData.SetSkipCellsAfter(0.0f, true, false, false);
        curRowChanged = true;
        newRow = true;
        docRows.Insert(rowIndex, tableData);
        if (oldFirstDocNode != null)
          AVSRow.CopyDataFromToDocRow(oldFirstDocNode, tableData);
      }
    }
    else if (updateMode == EmptyRowUpdateMode.Delete && !flag && (num1 == -1 || num2 != num1))
    {
      docRows.Remove(tableData);
      tableData = (TableData) null;
      curRowChanged = true;
    }
    else if (docRows[rowIndex] != tableData)
    {
      docRows.Remove(tableData);
      docRows.Insert(rowIndex, tableData);
      curRowChanged = true;
    }
    if (tableData != null)
    {
      if (!this.IsNoteRow)
        this.SaveRelationsReferencesToDocRow(tableData);
      if (productIndex > 0)
        tableData.SetAttributeValue(AVSRow.DocAttr_ProductIndex, productIndex.ToString(), false, false, false);
      else
        tableData.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
      if (tableData.Parent == null)
      {
        string nodeId = this.GetFieldStringValue(this.Field_Designation, 0, -1, (List<RelationAttributeValuesCache>) null, false) + (productIndex == 0 ? "" : $" (-{productIndex.ToString()})");
        if (this.avsDocument.Document != null && !string.IsNullOrEmpty(nodeId))
        {
          DocumentTreeNode node = this.avsDocument.Document.FindNode(nodeId);
          if (node != null && node != tableData)
            nodeId = (string) null;
        }
        if (!string.IsNullOrEmpty(nodeId))
          tableData.Id = nodeId;
      }
      if (!this.IsNoteRow)
      {
        string str = this.GetFieldStringValue(this.Field_Name, 0, -1, (List<RelationAttributeValuesCache>) null, false);
        if (str != null && str.Length > 10)
          str = str.Remove(7) + "...";
        tableData.SetName(str, false, false);
      }
    }
    return tableData;
  }

  /// <summary>Найти строку соответствующую блоку исполнений для заданного исполнения</summary>
  private TableData FindDocRowForProduct(
    List<TableData> docRows,
    int productIndex,
    int startRowIndex = 0)
  {
    if (startRowIndex < docRows.Count)
    {
      for (int index = startRowIndex; index < docRows.Count; ++index)
      {
        if (this.GetFirstProductIndexForDocRow((DocumentTreeNode) docRows[index]) == productIndex)
          return docRows[index];
      }
    }
    return (TableData) null;
  }

  private static void UpdateProtectedCharsZoneInCell(TextData cell)
  {
    if (!(cell is TextBoxElement textBoxElement))
      return;
    int result = -1;
    if (int.TryParse(textBoxElement.GetAttributeValue("ProtectedFirstCharCount", true), out result))
      textBoxElement.AssignProtectedFirstCharCount(result);
    if (!int.TryParse(textBoxElement.GetAttributeValue("ProtectedEndCharCount", true), out result))
      return;
    textBoxElement.AssignProtectedEndCharCount(result);
  }

  private void UpdateSortLinksInDocRow(int rowIndex, TableData docRow)
  {
    if (rowIndex != 0)
      return;
    if (this.SortAfterRow == null)
    {
      AVSRow avsRow = (AVSRow) null;
      long result;
      if (long.TryParse(docRow.GetAttributeValue(AVSRow.RowAttr_SortAfterRowBySortIndex, true), out result))
        avsRow = this.avsDocument.GetAvsDocRowBySortIndex(result);
      if (avsRow == null)
      {
        string attributeValue = docRow.GetAttributeValue(AVSRow.RowAttr_SortAfterRowByID, true);
        if (!string.IsNullOrEmpty(attributeValue))
        {
          DocumentTreeNode node = this.avsDocument.Document.FindNode(attributeValue);
          if (node != null)
            avsRow = this.avsDocument.GetAvsDocRow(node);
        }
      }
      if (avsRow != null)
      {
        if (avsRow != this)
        {
          try
          {
            this.SortAfterRow = avsRow;
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
          docRow.RemoveAttributeWithoutEvents(AVSRow.RowAttr_SortAfterRowBySortIndex);
          docRow.RemoveAttributeWithoutEvents(AVSRow.RowAttr_SortAfterRowByID);
        }
      }
    }
    if (this.SortBeforeRow != null)
      return;
    AVSRow avsRow1 = (AVSRow) null;
    long result1;
    if (long.TryParse(docRow.GetAttributeValue(AVSRow.RowAttr_SortBeforeRowBySortIndex, true), out result1))
      avsRow1 = this.avsDocument.GetAvsDocRowBySortIndex(result1);
    if (avsRow1 == null)
    {
      string attributeValue = docRow.GetAttributeValue(AVSRow.RowAttr_SortBeforeRowByID, true);
      if (!string.IsNullOrEmpty(attributeValue))
      {
        DocumentTreeNode node = this.avsDocument.Document.FindNode(attributeValue);
        if (node != null)
          avsRow1 = this.avsDocument.GetAvsDocRow(node);
      }
    }
    if (avsRow1 == null)
      return;
    if (avsRow1 == this)
      return;
    try
    {
      this.SortBeforeRow = avsRow1;
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    docRow.RemoveAttributeWithoutEvents(AVSRow.RowAttr_SortBeforeRowBySortIndex);
    docRow.RemoveAttributeWithoutEvents(AVSRow.RowAttr_SortBeforeRowByID);
  }

  /// <summary>Проверить соответствие шаблона в строках документа</summary>
  /// <param name="rowDocNodes"></param>
  /// <param name="rowTemplate"></param>
  /// <returns></returns>
  private bool ValidateDocNodesTemplate(List<TableData> rowDocNodes, TableData rowTemplate)
  {
    for (int index = 0; index < rowDocNodes.Count; ++index)
    {
      if (rowDocNodes[index].TemplateId != rowTemplate.Id || rowDocNodes[index].NodesCount != rowTemplate.NodesCount)
        return false;
    }
    return true;
  }

  private bool HasCountForProductInDocRow(int productIndex)
  {
    bool flag = false;
    for (int index = productIndex; index < productIndex + this.avsDocument.RowProductCount && index < this.avsDocument.productsInfo.Count; ++index)
    {
      int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[index].Id, this.relations);
      if (relationIndexForProduct != -1 && (this.IsDocRelation || this.RelType == AvsIDCache.Relation_Podbor || this.GetFieldValue(this.Field_Count, relationIndexForProduct, index, this.relations, true, false) != null))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  private void UpdateCellRefToTextSource(TextData cell, AvsRowAttributeInfo fieldInfo)
  {
    if (this.IsDynamicGroupHeaderRow || this.HasDynamicGroupHeader && this.Field_Name.Equals((AttributeInfo) fieldInfo))
      return;
    ReferenceToDBObjectAttribute referenceToTextSource = cell.ReferenceToTextSource as ReferenceToDBObjectAttribute;
    RefToDBObjectType refType = !fieldInfo.IsRelationAttribute ? RefToDBObjectType.rtUseParentObjectLink : RefToDBObjectType.rtUseParentRelationLink;
    if (referenceToTextSource == null)
    {
      ReferenceToDBObjectAttribute dbObjectAttribute = new ReferenceToDBObjectAttribute((DocumentTreeNode) cell, refType, (DBObjectInfoBase) this.RowID, fieldInfo.AttributeGuid, fieldInfo.AttributeId, fieldInfo.Name, true);
      cell.AssignReferenceToTextSource((ReferenceBase) dbObjectAttribute, true, false, false);
    }
    else
    {
      referenceToTextSource.AssignReferenceType(refType);
      referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) this.RowID, false);
      referenceToTextSource.PassiveLink = true;
      referenceToTextSource.AssignAttributeInfo(fieldInfo.AttributeGuid, fieldInfo.AttributeId, fieldInfo.Name);
    }
  }

  private string GetFieldStringValue(OutputMappingBase attrMapping)
  {
    switch (attrMapping)
    {
      case AttributeMapping attributeMapping:
        return this.GetFieldStringValue(new AvsRowAttributeInfo(attributeMapping.AttributeInfo), -1, -1, (List<RelationAttributeValuesCache>) null, false);
      case DelimiterMapping delimiterMapping:
        return delimiterMapping.DelimiterRTF;
      default:
        return (string) null;
    }
  }

  private string GetFieldStringValueForDynamicHeader(AttributeMapping attrMapping)
  {
    AvsRowAttributeInfo rowAttributeInfo = new AvsRowAttributeInfo(attrMapping.AttributeInfo);
    string attributeValue = this.GetFieldStringValue(rowAttributeInfo, -1, -1, (List<RelationAttributeValuesCache>) null, false);
    if (!string.IsNullOrEmpty(attributeValue))
      attributeValue = this.ReplaceClassWords(rowAttributeInfo, attributeValue);
    return attributeValue;
  }

  public void UpdateDynamicHeaderSettings(
    DynamicGroupHeaderSettings dynamicGroupHeaderSettings)
  {
    if (this.DocNode == null || this.IsNoteRow || !this.UpdateDocRowDynamicHeader(dynamicGroupHeaderSettings, out string _))
      return;
    this.UpdateNameDocCellText(false, false);
  }

  private void UpdateDocRowDynamicHeaderTextVariants(
    DocumentTreeNode docNode,
    string originalText,
    string textForGroup)
  {
    AVSRow.SetAttributeValueToDocNode("GroupHeaderCellOriginalText", originalText, docNode, true);
    AVSRow.SetAttributeValueToDocNode("GroupHeaderCellTextForGroup", textForGroup, docNode, true);
  }

  private bool UpdateDocRowDynamicHeader(
    DynamicGroupHeaderSettings dynamicGroupHeaderSettings,
    out string header)
  {
    string attributeValue = this.DocNode.GetAttributeValue("GroupHeaderText", true);
    header = this.CalcDynamicHeader(dynamicGroupHeaderSettings);
    int num = header != attributeValue ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.SetAttributeValuesToDocNodes("GroupHeaderText", header, true);
    return num != 0;
  }

  private string CalcRowNameInDynamicGroup()
  {
    string text = this.GetFieldStringValue(this.avsDocument.Attr_SizeAndParams, -1, -1, (List<RelationAttributeValuesCache>) null, false);
    if (string.IsNullOrEmpty(text))
      return this.GetFieldStringValue(this.Field_Name, -1, -1, (List<RelationAttributeValuesCache>) null, false);
    string textBeforeNumber;
    string textAfterNumber;
    if (NumberParserAdvanced.ParseNumber(text, true, out double _, out textBeforeNumber, out textAfterNumber) && string.IsNullOrEmpty(textBeforeNumber) && string.IsNullOrEmpty(textAfterNumber) || text.Trim().Length == 1)
    {
      string str = this.GetFieldValue(this.avsDocument.Attr_GroupWithoutClass, -1, -1, (List<RelationAttributeValuesCache>) null, true, false)?.ToString() ?? "0";
      text = $"{((str == "1" || str == "True" ? 1 : (str == "Да" ? 1 : 0)) != 0 ? string.Empty : this.GetFieldStringValue(this.avsDocument.Attr_Class, -1, -1, (List<RelationAttributeValuesCache>) null, false))} {text}".Trim();
    }
    return text;
  }

  private string CalcDynamicHeader(
    DynamicGroupHeaderSettings dynamicGroupHeaderSettings)
  {
    return !this.avsDocument.Document.DynamicGroupHeaderIsEnabled ? "" : dynamicGroupHeaderSettings?.DynamicHeaderCaptionSettings?.СoncatenateAttributesValues(new Intermech.AVS.GetFieldValueByCellOutputMapping(this.GetFieldStringValueForDynamicHeader)) ?? "";
  }

  internal string ReplaceClassWords(AvsRowAttributeInfo attr, string attributeValue)
  {
    if (attr == null)
      throw new ArgumentNullException(nameof (attr));
    string str1 = attributeValue;
    if (!string.IsNullOrEmpty(str1) && attr.Equals((AttributeInfo) this.avsDocument.Attr_Class))
    {
      string str2 = this.avsDocument.ReplaceClassInGroupHeaderDictionary.Keys.FirstOrDefault<string>((Func<string, bool>) (k => attributeValue.Contains(k)));
      string newValue;
      if (str2 != null && this.avsDocument.ReplaceClassInGroupHeaderDictionary.TryGetValue(str2, out newValue))
        str1 = str1.Replace(str2, newValue);
    }
    return str1;
  }

  /// <summary>Сохранить данные о связях или объектах БД в строке документа</summary>
  /// <param name="docRow">Строка документа, в которой хранятся ссылки</param>
  private void SaveRelationsReferencesToDocRow(TableData docRow)
  {
    if (docRow == null)
      throw new ArgumentNullException(nameof (docRow));
    if (this.avsDocument != null && this.avsDocument.IsElementList)
      docRow.BeginChanges(true);
    try
    {
      if (this.HasRelation)
        this.rowID = new DBRelationInfo(this.relations[0].RelationGuid, this.relations[0].RelationId, this.relations[0].RelationType, this.relations[0].ProjectGuid, this.relations[0].ProjectId, this.relations[0].ObjectGuid, this.relations[0].ObjectId, this.relations[0].ObjectType, this.relations[0].ObjectCaption);
      else if (this.HasObject)
      {
        int relationType = -1;
        if (this.rowID != null)
          relationType = this.rowID.RelationType;
        this.rowID = new DBRelationInfo(Guid.Empty, -1L, relationType, Guid.Empty, -1L, this.objectAttributesCache.ObjectGuid, this.objectAttributesCache.ObjectId, this.objectAttributesCache.ObjectType, this.objectAttributesCache.ObjectCaption);
      }
      if (!(docRow.Reference is ReferenceToDBObject reference))
      {
        ReferenceToDBObject referenceToDbObject = !this.HasRelation ? new ReferenceToDBObject((DocumentTreeNode) docRow, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) this.RowID, true) : new ReferenceToDBObject((DocumentTreeNode) docRow, RefToDBObjectType.rtSelectedRelation, (DBObjectInfoBase) this.RowID, true);
        referenceToDbObject.PassiveLink = true;
        docRow.AssignReference((ReferenceBase) referenceToDbObject, false, false);
      }
      else
        reference.AssignDBObjectInfo((DBObjectInfoBase) this.RowID, true);
      if (this.HasRelation && this.Relations.Count > 1)
      {
        string attributeValue = string.Join<Guid>(";", this.Relations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.RelationGuid)));
        docRow.SetAttributeValue("Relations", attributeValue, false, false, false);
      }
      else
        docRow.RemoveAttributeWithoutEvents("Relations");
      if (this.HasHiddenRelation)
      {
        string attributeValue = string.Join<Guid>(";", this.HiddenRelations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.RelationGuid)));
        docRow.SetAttributeValue("HiddenRelations", attributeValue, false, false, false);
      }
      else
        docRow.RemoveAttributeWithoutEvents("HiddenRelations");
    }
    finally
    {
      if (this.avsDocument != null && this.avsDocument.IsElementList)
        docRow.EndChanges(true);
    }
  }

  /// <summary>Сохранить ссылки на связи во все строки записи</summary>
  internal void SaveRelationsReferencesToDocRows()
  {
    if (this.HasDocNodes)
    {
      foreach (TableData docNode in this.DocNodes)
        this.SaveRelationsReferencesToDocRow(docNode);
    }
    if (!this.HasDocNodeExp)
      return;
    this.SaveRelationsReferencesToDocRow(this.docNodeExp);
  }

  internal static List<Guid> GetRelationsGuidsFromDocRow(TableData docRow)
  {
    List<Guid> rowAttributeValue = AVSRow.GetGuidListFromRowAttributeValue(docRow, "Relations");
    if (rowAttributeValue.Count == 0)
    {
      INodeWithReference nodeWithReference = (INodeWithReference) docRow;
      if (nodeWithReference == null)
        return rowAttributeValue;
      if (nodeWithReference.Reference is ReferenceToDBObject reference && reference.DBRelationGuid != Guid.Empty)
        rowAttributeValue.Add(reference.DBRelationGuid);
    }
    rowAttributeValue.AddRange((IEnumerable<Guid>) AVSRow.GetGuidListFromRowAttributeValue(docRow, "HiddenRelations"));
    return rowAttributeValue;
  }

  private static List<Guid> GetGuidListFromRowAttributeValue(TableData docRow, string attrName)
  {
    List<Guid> rowAttributeValue = new List<Guid>();
    string attributeValue = docRow.GetAttributeValue(attrName, true);
    if (attributeValue != "")
    {
      string str = attributeValue;
      char[] chArray = new char[1]{ ';' };
      foreach (string g in str.Split(chArray))
        rowAttributeValue.Add(new Guid(g));
    }
    return rowAttributeValue;
  }

  private Image GetStatus()
  {
    object fieldValue1 = this.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenGroupNum), 0, -1, this.relations, false, false);
    Image status = (Image) null;
    switch (fieldValue1)
    {
      case null:
      case DBNull _:
        if (status == null)
          status = StatusIcons.None;
        return status;
      default:
        long result = 0;
        if (fieldValue1 is string)
        {
          if (!long.TryParse((string) fieldValue1, out result))
            result = 0L;
        }
        else
          result = Convert.ToInt64(fieldValue1);
        if (result > 0L)
        {
          object fieldValue2 = this.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenNumInGroup), 0, -1, this.relations, false, false);
          if (fieldValue2 != null && !(fieldValue2 is DBNull))
          {
            status = Convert.ToInt64(fieldValue2) != 0L ? StatusIcons.Substitute : StatusIcons.ActualSubstitute;
            goto case null;
          }
          goto case null;
        }
        goto case null;
    }
  }

  public static string JoinWithoutEmptyValues(string separator, params string[] values)
  {
    if (values == null)
      throw new ArgumentNullException(nameof (values));
    return string.Join(separator, ((IEnumerable<string>) values).Where<string>((Func<string, bool>) (s => !string.IsNullOrEmpty(s))));
  }

  /// <summary>Данная ячейка хранит количество для исполнения по форме Б</summary>
  /// <param name="isFormB">Форма Б</param>
  /// <param name="cell">Ячейка документа</param>
  /// <returns></returns>
  public static bool IsCountFormBCell(bool isFormB, TextData cell)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    if (!isFormB)
      return false;
    string str = cell.IsTemplate ? cell.Id : cell.TemplateId;
    return str != null && str.ToLower().Contains("количество");
  }

  /// <summary>Данная ячейка хранит Количество</summary>
  /// <param name="cell">Ячейка документа</param>
  /// <returns></returns>
  public static bool IsCountCell(TextData cell)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    return (cell.IsTemplate ? cell.Id : cell.TemplateId) == AVSRow.DocAttr_Count;
  }

  /// <summary>Данная ячейка хранит Количество</summary>
  /// <param name="cell">Ячейка документа</param>
  /// <returns></returns>
  public static bool IsNoteCell(TextData cell)
  {
    if (cell == null)
      throw new ArgumentNullException(nameof (cell));
    string str = cell.IsTemplate ? cell.Id : cell.TemplateId;
    return str == AVSRow.DocAttr_Note || str == AVSRow.DocAttr_NotePE;
  }

  internal static AvsRowAttributeInfo ConvertOldCellDocAttrInfo(
    AvsRowAttributeInfo attrInfo,
    TextData cell,
    bool isElementList)
  {
    if (isElementList && attrInfo.IsDocField)
    {
      if (AVSRow.IsCountCell(cell))
        attrInfo = AvsIDCache.StdField_Count;
      else if (AVSRow.IsNoteCell(cell))
        attrInfo = AvsIDCache.StdField_NotePE;
    }
    return attrInfo;
  }

  /// <summary>Атрибут "Количество" или "Количество на регулировку"</summary>
  /// <param name="attribute">Информация об атрибуте</param>
  /// <returns></returns>
  public static bool IsCountAttribute(AvsRowAttributeInfo attribute)
  {
    if (attribute == null)
      return false;
    if (attribute.IsRelationAttribute && (attribute.AttributeId == AvsIDCache.Attr_Count || attribute.AttributeId == AvsIDCache.Attr_CountForAdjustment))
      return true;
    return attribute.IsDocField && attribute.Name == AVSRow.DocAttr_Count;
  }

  /// <summary>Атрибут который отображается в графе "Количество"</summary>
  /// <param name="attribute">Информация об атрибуте</param>
  /// <returns></returns>
  public static bool IsCountField(AvsRowAttributeInfo attribute)
  {
    if (attribute == null)
      return false;
    if (attribute.IsRelationAttribute && attribute.AttributeId == AvsIDCache.Attr_Count)
      return true;
    return attribute.IsDocField && attribute.Name == AVSRow.DocAttr_Count;
  }

  /// <summary>Преобразовываем строку, чтобы ее мог опознать MeasureHelper</summary>
  /// <param name="strValue"></param>
  /// <returns></returns>
  public static string ConvertCountToStringForMeasuredValue(object value)
  {
    return AVSRow.ConvertCountToStringForMeasuredValue(Convert.ToString((object) AVSRow.ConvertCountToMeasuredValue(value)));
  }

  /// <summary>Преобразовываем строку, чтобы ее мог опознать MeasureHelper</summary>
  /// <param name="strValue">Строковое значение Количества</param>
  /// <returns></returns>
  public static string ConvertCountToStringForMeasuredValue(string strValue)
  {
    if (string.IsNullOrEmpty(strValue))
      return "";
    if (AVSRow.IsDocumentCountX(strValue))
      return "1";
    strValue = strValue.Replace("?", "");
    int length = strValue.IndexOf('/');
    double doubleValue;
    double number;
    string textAfterNumber;
    return length > 0 && length < strValue.Length - 1 && NumberParserAdvanced.TryParseDouble(strValue.Substring(0, length), out doubleValue) && NumberParserAdvanced.ParseNumber(strValue.Substring(length + 1), true, out number, out string _, out textAfterNumber) ? (doubleValue / number).ToString() + textAfterNumber : strValue;
  }

  /// <summary>Преобразовать значение количества в формат MeasuredValue</summary>
  /// <param name="value">Значение Количество</param>
  /// <param name="exceptionIfFail">Генерировать исключение, если нельзя конвертировать</param>
  /// <returns></returns>
  public static MeasuredValue ConvertCountToMeasuredValue(object value, bool exceptionIfFail = true)
  {
    mvalue = (MeasuredValue) null;
    switch (value)
    {
      case null:
      case DBNull _:
        return mvalue;
      case MeasuredValue mvalue:
        return mvalue;
      default:
        string strValue = value.ToString();
        string str = strValue;
        if (strValue != "")
        {
          mvalue = AVSRow.ConvertToMeasuredValue((object) AVSRow.ConvertCountToStringForMeasuredValue(strValue), AVSRow.DefaultCountMeasure, exceptionIfFail);
          if (mvalue != null)
          {
            mvalue.Caption = str;
            mvalue.AppendShortNameToCaption();
            goto case null;
          }
          goto case null;
        }
        goto case null;
    }
  }

  /// <summary>Преобразовать значение в формат MeasuredValue</summary>
  /// <param name="value">Значение</param>
  /// <param name="defaultMeasure">Физическая величина по умолчанию</param>
  /// <param name="exceptionIfFail">Генерировать исключение, если нельзя конвертировать</param>
  /// <returns></returns>
  public static MeasuredValue ConvertToMeasuredValue(
    object value,
    MeasureDescriptor defaultMeasure = null,
    bool exceptionIfFail = true)
  {
    return AVSRow.ConvertToMeasuredValueHandler == null ? AVSRow.ConvertToMeasuredValueInternal(value, defaultMeasure, exceptionIfFail) : AVSRow.ConvertToMeasuredValueHandler(value, defaultMeasure, exceptionIfFail);
  }

  /// <summary>Преобразовать значение в формат MeasuredValue</summary>
  /// <param name="value">Значение</param>
  /// <param name="defaultMeasure">Физическая величина по умолчанию</param>
  /// <param name="exceptionIfFail">Генерировать исключение, если нельзя конвертировать</param>
  /// <returns></returns>
  private static MeasuredValue ConvertToMeasuredValueInternal(
    object value,
    MeasureDescriptor defaultMeasure,
    bool exceptionIfFail = true)
  {
    measuredValueInternal = (MeasuredValue) null;
    switch (value)
    {
      case null:
      case DBNull _:
        return measuredValueInternal;
      case MeasuredValue measuredValueInternal:
        return measuredValueInternal;
      default:
        string mValue = value.ToString().Trim();
        if (mValue != "")
        {
          try
          {
            measuredValueInternal = MeasureHelper.ConvertToMeasuredValue(mValue, defaultMeasure, exceptionIfFail);
            measuredValueInternal.Caption = mValue;
            goto case null;
          }
          catch (Exception ex)
          {
            if (exceptionIfFail)
              throw;
            measuredValueInternal = (MeasuredValue) null;
            goto case null;
          }
        }
        else
          goto case null;
    }
  }

  /// <summary>Получить количество записи</summary>
  /// <param name="relationIndex">Индекс связи. Если -1, то вычисляется из productIndex или берётся 0</param>
  /// <param name="productIndex">Индекс исполнения. Если -1, то вычисляется из relationIndex или берётся 0</param>
  /// <param name="relationList">Список связей</param>
  /// <returns></returns>
  public MeasuredValue GetCount(
    int relationIndex,
    int productIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    if (relationList == null)
      relationList = this.Relations;
    if (relationIndex == -1 && productIndex == -1)
      relationIndex = 0;
    if (relationIndex == -1 && productIndex != -1)
    {
      if (productIndex >= this.avsDocument.productsInfo.Count)
        return (MeasuredValue) null;
      relationIndex = this.GetRelationIndexForProduct(this.avsDocument.FindProductByIndex(productIndex).Id, relationList);
    }
    AvsRowAttributeInfo attrInfo = this.OverrideCountAttributeInPodborRelation(this.Field_Count);
    MeasuredValue count1 = AVSRow.ConvertCountToMeasuredValue(this.GetFieldValue(attrInfo, relationIndex, productIndex, relationList, true, false, true));
    string fieldStringValue = this.avsDocument.IsElementList ? this.GetFieldStringValue(this.avsDocument.Attr_FunctionalGroupPosDesignation, relationIndex, productIndex, relationList, false, true, true) : "";
    if (this.HasHiddenRelation && relationList == this.Relations)
    {
      long num = -1;
      if (relationIndex != -1 && this.HasRelation)
        num = relationList[relationIndex].ProjectId;
      else if (productIndex != -1)
        num = this.avsDocument.FindProductByIndex(productIndex).Id;
      for (int index = 0; index < this.HiddenRelations.Count; ++index)
      {
        if (this.HiddenRelations[index].ProjectId == num && !this.CheckRelation_IsHiddenDopZamen(this.HiddenRelations[index]) && (this.avsDocument.IsElementList ? this.GetFieldStringValue(this.avsDocument.Attr_FunctionalGroupPosDesignation, index, productIndex, this.HiddenRelations, false, true, true) : "") == fieldStringValue)
        {
          MeasuredValue measuredValue = AVSRow.ConvertCountToMeasuredValue(this.GetFieldValue(attrInfo, index, -1, this.HiddenRelations, true, false, true));
          count1 = this.avsDocument.SummCountValues(count1, measuredValue);
        }
      }
    }
    if (this.avsDocument.IsElementList && count1 != null && !string.IsNullOrEmpty(fieldStringValue))
    {
      int count = PosDesignationRecord.ParsePositionalDesignation(fieldStringValue).Count;
      if (count > 1)
        count1 = new MeasuredValue(Math.Round(count1.Value / (double) count), count1.MeasureID);
    }
    return count1;
  }

  public MeasuredValue GetCount(int relationIndex, int productIndex)
  {
    return this.GetCount(relationIndex, productIndex, this.relations);
  }

  /// <summary>Получить позиционное обозначение для графы примечания.
  /// Суммируются значения всех связей записи
  /// с поз. обозначением функциональной группы и звёздочками для подбора, согласно настройкам</summary>
  /// <param name="rowAttribute">Атрибут для позиционного обозначения.
  /// "Позиционное обозначение" или "Подбор для позиционного обозначения"</param>
  /// <returns></returns>
  public string GetPosDesignationForNoteField(AvsRowAttributeInfo rowAttribute)
  {
    List<PosDesignationRecord> posDesignations = new List<PosDesignationRecord>();
    if (this.HasRelation)
    {
      for (int relationIndex = 0; relationIndex < this.Relations.Count; ++relationIndex)
        posDesignations.AddRange((IEnumerable<PosDesignationRecord>) this.GetPosDesignationRecord(rowAttribute, relationIndex, this.Relations));
    }
    else
      posDesignations.AddRange((IEnumerable<PosDesignationRecord>) this.GetPosDesignationRecord(rowAttribute, -1, (List<RelationAttributeValuesCache>) null));
    if (this.HasHiddenRelation)
    {
      for (int relationIndex = 0; relationIndex < this.HiddenRelations.Count; ++relationIndex)
        posDesignations.AddRange((IEnumerable<PosDesignationRecord>) this.GetPosDesignationRecord(rowAttribute, relationIndex, this.HiddenRelations));
    }
    return PosDesignationHelper.Summ(posDesignations, AvsConfig.PositionDesignation.SpliterForSummPositionDesignation, AvsConfig.PositionDesignation.SpliterForFunctionalGroupInPositionDesignation);
  }

  /// <summary>Получить список специальных структур для позиционного обозначения заданного текстовой строкой в атрибуте</summary>
  /// <param name="rowAttribute">Атрибут для позиционного обозначения.
  /// "Позиционное обозначение" или "Подбор для позиционного обозначения"</param>
  /// <param name="relationIndex">Индекс связи в списке</param>
  /// <param name="relationList">Список связей</param>
  /// <returns></returns>
  private List<PosDesignationRecord> GetPosDesignationRecord(
    AvsRowAttributeInfo rowAttribute,
    int relationIndex,
    List<RelationAttributeValuesCache> relationList)
  {
    string fieldStringValue = this.GetFieldStringValue(rowAttribute, relationIndex, -1, relationList, false, true);
    string str1 = (string) null;
    if (this.avsDocument.IsSpecification && AvsConfig.PositionDesignation.IncludeFunctionalGroupInPositionDesignation)
      str1 = this.GetFieldStringValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_FGPosDesignation), relationIndex, -1, relationList, false, true);
    string str2 = (string) null;
    if (this.IsBaseComponentForPodbor(relationIndex, relationList) && this.avsDocument.InsertStarAfterPositionDesignation)
    {
      if (AvsConfig.Podbor.SymbolAfterPosDesignationGetFromCAD)
        str2 = this.GetFieldStringValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_SymbolForPosDesignation), relationIndex, -1, relationList, false, true);
      if (string.IsNullOrEmpty(str2))
        str2 = "*";
    }
    string functionalGroup = str1;
    string additionalSymbol = str2;
    string positionDesignation = AvsConfig.PositionDesignation.SpliterForSummPositionDesignation;
    return PosDesignationRecord.ParsePositionalDesignation(fieldStringValue, functionalGroup, additionalSymbol, positionDesignation);
  }

  /// <summary>Получить раздельно числовое значение количества и единицы измерения</summary>
  /// <param name="countValue">Количество</param>
  /// <param name="md">Описатель единиц измерения количества</param>
  /// <param name="countMeasure">Единицы измерения количества</param>
  /// <returns>Числовое значение количества</returns>
  [Obsolete("Метод устарел, необходимо заменить его на ConvertCountToValueAndMeasure")]
  public string ConvertCountToString(
    object countValue,
    ref MeasureDescriptor md,
    out string countMeasure)
  {
    return this.ConvertCountToValueAndMeasure(countValue, ref md, out countMeasure);
  }

  /// <summary>Получить раздельно числовое значение количества и единицы измерения</summary>
  /// <param name="countValue">Количество</param>
  /// <param name="md">Описатель единиц измерения количества</param>
  /// <param name="countMeasure">Единицы измерения количества</param>
  /// <returns>Числовое значение количества</returns>
  public string ConvertCountToValueAndMeasure(
    object countValue,
    ref MeasureDescriptor md,
    out string countMeasure)
  {
    countMeasure = "";
    if (countValue == null || countValue is DBNull)
      return "";
    if (this.IsDocRelation && (this.IsFormB || this.HideCountForDocuments))
      return Convert.ToString(countValue);
    string valueAndMeasure = "";
    string stringValue = countValue as string;
    MeasuredValue measuredValue = (MeasuredValue) null;
    if (string.IsNullOrEmpty(stringValue))
    {
      try
      {
        measuredValue = AVSRow.ConvertCountToMeasuredValue(countValue);
        if (measuredValue != null)
          stringValue = measuredValue.Caption;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("В графе \"Количество\" содержатся ошибочные данные: " + ex.Message, "Ошибка!");
      }
    }
    if (!string.IsNullOrEmpty(stringValue))
    {
      int num = stringValue.IndexOf('/');
      if (num > 0 && num < stringValue.Length - 1)
      {
        this.SplitMeasureInStringValue(stringValue.Substring(num + 1), ref countMeasure);
        valueAndMeasure = string.IsNullOrEmpty(countMeasure) ? stringValue : stringValue.Remove(stringValue.Length - countMeasure.Length).Trim();
      }
      else
        valueAndMeasure = this.SplitMeasureInStringValue(stringValue, ref countMeasure);
      if (countMeasure == AVSRow.DefaultMU_Count_str)
        countMeasure = "";
    }
    else if (measuredValue != null)
    {
      if (measuredValue.MeasureID != AVSRow.DefaultCountID)
      {
        if (md == null)
          md = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
        countMeasure = md.ShortName;
      }
      valueAndMeasure = measuredValue.Value.ToString();
    }
    return valueAndMeasure;
  }

  public string SplitMeasureInStringValue(string stringValue, ref string countMeasure)
  {
    if (string.IsNullOrEmpty(stringValue))
      return stringValue;
    string str = stringValue;
    if (NumberParserAdvanced.ParseNumber(stringValue, true, out string _, out string _, out countMeasure) && !string.IsNullOrEmpty(countMeasure))
    {
      str = stringValue.Remove(stringValue.Length - countMeasure.Length);
      countMeasure = countMeasure.Trim();
    }
    return str;
  }

  /// <summary>Заданное поле используется в графе примечания</summary>
  /// <param name="baseFieldInfo"></param>
  /// <returns></returns>
  private bool IsFieldСorrelatedWithNote(AvsRowAttributeInfo baseFieldInfo)
  {
    if (baseFieldInfo == null)
      throw new ArgumentNullException(nameof (baseFieldInfo));
    return baseFieldInfo.Equals((AttributeInfo) this.Field_Note) || baseFieldInfo.Equals((AttributeInfo) this.Field_Zone) || baseFieldInfo.Equals((AttributeInfo) this.Field_Format) || AVSRow.IsCountField(baseFieldInfo);
  }

  /// <summary>Заданный атрибут влияет на графу Примечание</summary>
  /// <param name="baseFieldInfo"></param>
  /// <returns></returns>
  private bool IsAttributeUsedInNoteField(AvsRowAttributeInfo attrInfo)
  {
    if (attrInfo == null)
      throw new ArgumentNullException(nameof (attrInfo));
    if ((this.IsFieldСorrelatedWithNote(attrInfo) ? 1 : (!attrInfo.IsRelationAttribute ? 0 : (attrInfo.AttributeId == AvsIDCache.Attr_FGPosDesignation || attrInfo.AttributeId == AvsIDCache.Attr_SymbolForPosDesignation ? 1 : (attrInfo.AttributeId == AvsIDCache.Attr_Podbor ? 1 : 0)))) != 0)
      return true;
    if (this.NewCellMappingMode && this.NoteCellMapping != null)
      return this.NoteCellMapping.ContainsAttribute((AttributeInfo) attrInfo);
    RemarkAttribute attribute = this.avsDocument.noteFieldSettings.FindAttribute(attrInfo.IsRelationAttribute, attrInfo.AttributeId);
    if (attribute == null)
      return false;
    return !attribute.WithoutDrawing || MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing);
  }

  /// <summary>Обновить сборное значение в графе "Примечание"</summary>
  /// <param name="cell">Ячейка, значение которой изменилось</param>
  /// <param name="attrInfo">Информация об атрибуте, значение которого изменилось</param>
  /// <param name="value">Новое значение атрибута</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateNoteDocCellText(
    TextData cell,
    AvsRowAttributeInfo attrInfo,
    string value,
    bool updateUI,
    bool updateLayout)
  {
    if (this.NewCellMappingMode)
      this.UpdateNoteDocCellText_NEW(updateLayout);
    else
      this.UpdateNoteDocCellText_OLD(cell, attrInfo, value, updateUI, updateLayout);
  }

  public void UpdateNoteDocCellText(bool updateLayout = false)
  {
    if (this.NewCellMappingMode)
      this.UpdateNoteDocCellText_NEW(updateLayout);
    else
      this.UpdateNoteDocCellText_OLD((TextData) null, (AvsRowAttributeInfo) null, (string) null, false, false);
  }

  internal CellOutputMapping NoteCellMapping
  {
    get
    {
      return this._noteCellMapping ?? (this._noteCellMapping = this.GetCellAttributeMapping(AVSRow.DocAttr_Note));
    }
    set => this._noteCellMapping = value;
  }

  internal void ResetCellMappingCache()
  {
    this._noteCellMapping = (CellOutputMapping) null;
    this._field_Note = (AvsRowAttributeInfo) null;
    this._hasNoteAndNoteAttributeCollision = false;
  }

  /// <summary>Обновить сборное значение в графе "Примечание"</summary>
  public void UpdateNoteDocCellText_NEW(bool updateLayout)
  {
    if (this.IsNoteRow)
      return;
    if (this._suspendUpdateNote > 0)
    {
      this._needUpdateNote = true;
    }
    else
    {
      if (this.avsDocument != null)
        this.avsDocument.Lock_DocCell_TextChanged();
      try
      {
        this._needUpdateNote = false;
        this._noteCellMapping = this.GetCellAttributeMapping(AVSRow.DocAttr_Note);
        if (this._noteCellMapping == null)
          return;
        this._hasNoteAndNoteAttributeCollision = this.avsDocument.IsElementList && !this.HasRelation && this._noteCellMapping.ContainsAttribute((AttributeInfo) AvsIDCache.StdField_Note) && this._noteCellMapping.ContainsAttribute((AttributeInfo) AvsIDCache.StdField_NotePE);
        bool flag1 = this._noteCellMapping.ContainsAttribute((AttributeInfo) this.Field_Format);
        string fieldStringValue1 = this.GetFieldStringValue(this.Field_Format, 0, -1, (List<RelationAttributeValuesCache>) null, false);
        string fieldNewText1 = (string) null;
        bool flag2 = this._noteCellMapping.ContainsAttribute((AttributeInfo) this.Field_Zone);
        string fieldStringValue2 = this.GetFieldStringValue(this.Field_Zone, 0, -1, (List<RelationAttributeValuesCache>) null, false);
        string fieldNewText2 = (string) null;
        int countCellCount = this.CountCellCount;
        bool flag3 = !this.IsDocRelation && this._noteCellMapping.ContainsAttribute((AttributeInfo) this.avsDocument.Attr_DopZamenText);
        List<string> stringList = new List<string>(countCellCount);
        int[] dopZamenIndexes = new int[countCellCount];
        string[] editValues = new string[countCellCount];
        string[] viewValues = new string[countCellCount];
        bool showMeasureUnitsInNote = !this.HideCountForDocuments && this._noteCellMapping.ContainsAttribute((AttributeInfo) AvsIDCache.CountMeasureAttrInfo);
        foreach (TableData docNode in this.DocNodes)
        {
          TextData cellForBaseField1 = this.GetDocumentCellForBaseField(this.Field_Note, docNode, 0);
          if (cellForBaseField1 != null)
          {
            TextData cellForBaseField2 = this.GetDocumentCellForBaseField(this.Field_Format, docNode, 0);
            string fieldValue = fieldStringValue1;
            if (cellForBaseField2 != null && cellForBaseField2.ContainsAttribute(AVSRow.CellAttrName_ViewTextForFormat))
              fieldValue = "";
            int productIndexForDocRow = this.GetFirstProductIndexForDocRow((DocumentTreeNode) docNode);
            string countMeasure = "";
            int noteReferenceCount = 0;
            int refIndex1 = -1;
            int refIndex2 = -1;
            int startDopZamenIndex = -1;
            for (int index = 0; index < editValues.Length; ++index)
              editValues[index] = (string) null;
            for (int index = 0; index < viewValues.Length; ++index)
              viewValues[index] = (string) null;
            if (flag3)
            {
              stringList.Clear();
              for (int index = 0; index < dopZamenIndexes.Length; ++index)
                dopZamenIndexes[index] = 0;
            }
            bool flag4 = true;
            foreach (TextData cell in (IEnumerable<TextData>) docNode.TextCellsEnumerator)
            {
              AvsRowAttributeInfo cellBaseFieldInfo = this.GetCellBaseFieldInfo(cell, out int _);
              if (flag1 && object.Equals((object) cellBaseFieldInfo, (object) this.Field_Format))
              {
                if (fieldValue != null && fieldValue.Length > AvsConfig.General.FormatSize)
                  refIndex1 = ++noteReferenceCount;
              }
              else if (flag2 && object.Equals((object) cellBaseFieldInfo, (object) this.Field_Zone))
              {
                if (fieldStringValue2 != null && fieldStringValue2.Length > AvsConfig.General.ZoneSize)
                  refIndex2 = ++noteReferenceCount;
              }
              else if (flag4 && object.Equals((object) cellBaseFieldInfo, (object) this.Field_Count))
              {
                flag4 = false;
                this.CollectCountValuesForRow(docNode, productIndexForDocRow, showMeasureUnitsInNote, editValues, viewValues, out countMeasure);
                if (flag3)
                  this.PrepareSubsituitesTextForNote(stringList, dopZamenIndexes, productIndexForDocRow, ref noteReferenceCount, out startDopZamenIndex);
              }
            }
            int index1 = -1;
            List<string> itemValues = new List<string>(this._noteCellMapping.Items.Count);
            for (int index2 = 0; index2 < this._noteCellMapping.Items.Count; ++index2)
            {
              string noteItemValue1;
              if (this._noteCellMapping.Items[index2] is AttributeMapping attributeMapping)
              {
                if (flag1 && attributeMapping.Equals((object) this.Field_Format))
                  AVSRow.CreateNoteReference(fieldValue, refIndex1, noteReferenceCount, out fieldNewText1, out noteItemValue1);
                else if (flag2 && attributeMapping.Equals((object) this.Field_Zone))
                  AVSRow.CreateNoteReference(fieldStringValue2, refIndex2, noteReferenceCount, out fieldNewText2, out noteItemValue1);
                else if (flag3 && attributeMapping.Equals((object) this.avsDocument.Attr_DopZamenText))
                {
                  if (!stringList.IsNullOrEmpty<string>())
                  {
                    if (this.IsFormB)
                    {
                      string str = "";
                      for (int index3 = 0; index3 < stringList.Count; ++index3)
                      {
                        string noteItemValue2;
                        AVSRow.CreateNoteReference(stringList[index3], startDopZamenIndex + index3, noteReferenceCount, out string _, out noteItemValue2);
                        str += str == "" ? noteItemValue2 : "\r\n" + noteItemValue2;
                      }
                      noteItemValue1 = str;
                      for (int index4 = 0; index4 < countCellCount && index4 < dopZamenIndexes.Length; ++index4)
                      {
                        if (dopZamenIndexes[index4] > 0)
                          viewValues[index4] = AVSRow.CreateFieldReferenceText(dopZamenIndexes[index4], noteReferenceCount);
                      }
                    }
                    else
                    {
                      noteItemValue1 = stringList.First<string>();
                      viewValues[0] = "";
                    }
                  }
                  else
                    noteItemValue1 = "";
                }
                else if (attributeMapping.Equals((object) this.Field_PosDesignation))
                  noteItemValue1 = stringList.IsNullOrEmpty<string>() ? this.GetPosDesignationForNoteField(this.Field_PosDesignation) : "";
                else if (attributeMapping.Equals((object) this.Attr_PodborForPosDesignation))
                  noteItemValue1 = this.GetPosDesignationForNoteField(this.Attr_PodborForPosDesignation);
                else if (attributeMapping.Equals((object) AvsIDCache.CountMeasureAttrInfo))
                  noteItemValue1 = !stringList.IsNullOrEmpty<string>() ? "" : countMeasure;
                else if (object.Equals((object) this.Field_Note, (object) attributeMapping.AttributeInfo))
                {
                  index1 = index2;
                  noteItemValue1 = this.GetFieldStringValue(this.Field_Note, 0, -1, (List<RelationAttributeValuesCache>) null, false, ignoreCellValue: this._hasNoteAndNoteAttributeCollision);
                }
                else
                {
                  bool ignoreCellValue = this._hasNoteAndNoteAttributeCollision && attributeMapping.AttributeName == "Примечание";
                  noteItemValue1 = this.GetFieldValueForDocCell(new AvsRowAttributeInfo(attributeMapping.AttributeInfo), 0, -1, false, false, ignoreCellValue: ignoreCellValue);
                }
              }
              else
                noteItemValue1 = ((DelimiterMapping) this._noteCellMapping.Items[index2]).DelimiterRTF;
              itemValues.Add(noteItemValue1);
            }
            this.SetFieldValueInDocRowsCell(this.Field_Format, docNode, -1, fieldStringValue1, fieldNewText1);
            this.SetFieldValueInDocRowsCell(this.Field_Zone, docNode, -1, fieldStringValue2, fieldNewText2);
            for (int index5 = 0; index5 < countCellCount; ++index5)
            {
              int productIndex = productIndexForDocRow + index5;
              this.SetFieldValueInDocRowsCell(this.Field_Count, docNode, productIndex, editValues[index5], viewValues[index5]);
            }
            if (cellForBaseField1 is TextBoxElement textBox)
              textBox.AssignProtectedZone(0, 0);
            IList<string> itemValuesWithoutUnnecessaryDelimeters;
            string text = this._noteCellMapping.ConcatenateAttributesValues((IList<string>) itemValues, out itemValuesWithoutUnnecessaryDelimeters);
            cellForBaseField1.AssignText(text, false, true, false, false, false);
            if (textBox != null)
            {
              int protectedStartZone = 0;
              int protectedEndZone = 0;
              if (index1 != -1)
              {
                for (int index6 = 0; index6 < index1; ++index6)
                  protectedStartZone += TextData.CharCountInEditor(itemValuesWithoutUnnecessaryDelimeters[index6]);
                int num = TextData.CharCountInEditor(itemValuesWithoutUnnecessaryDelimeters[index1]);
                protectedEndZone = TextData.CharCountInEditor(text) - (protectedStartZone + num);
              }
              this.SetupProtectedZonesInTextBox(textBox, protectedStartZone, protectedEndZone);
            }
          }
        }
      }
      finally
      {
        if (this.avsDocument != null)
          this.avsDocument.Unlock_DocCell_TextChanged();
      }
      if (!updateLayout || !this.HasDocNodes)
        return;
      this.avsDocument.Document.UpdateLayout(this.FindFirstPageForDocNodes(), false, true);
    }
  }

  private int FindFirstPageForDocNodes()
  {
    int firstPageForDocNodes = int.MaxValue;
    foreach (TableData docNode in this.DocNodes)
    {
      if (docNode.Page.Index < firstPageForDocNodes)
        firstPageForDocNodes = docNode.Page.Index;
    }
    if (firstPageForDocNodes == int.MaxValue)
      firstPageForDocNodes = 0;
    return firstPageForDocNodes;
  }

  private static string CreateFieldReferenceText(int refIndex, int noteReferenceCount)
  {
    return noteReferenceCount != 1 ? $"*{refIndex})" : "*)";
  }

  private static void CreateNoteReference(
    string fieldValue,
    int refIndex,
    int noteReferenceCount,
    out string fieldNewText,
    out string noteItemValue)
  {
    if (refIndex > 0)
    {
      string fieldReferenceText = AVSRow.CreateFieldReferenceText(refIndex, noteReferenceCount);
      noteItemValue = $"{fieldReferenceText}\u000E{fieldValue}";
      fieldNewText = fieldReferenceText;
    }
    else
    {
      noteItemValue = "";
      fieldNewText = fieldValue;
    }
  }

  private void PrepareSubsituitesTextForNote(
    List<string> dopZamenValues,
    int[] dopZamenIndexes,
    int firstProductIndex,
    ref int noteReferenceCount,
    out int startDopZamenIndex)
  {
    if (this.IsFormB)
    {
      startDopZamenIndex = noteReferenceCount + 1;
      for (int index = 0; index < this.avsDocument.RowProductCount && firstProductIndex + index < this.avsDocument.productsInfo.Count; ++index)
      {
        int productIndex = firstProductIndex + index;
        int relationForProductIndex = this.GetRelationForProductIndex(productIndex, this.relations);
        if (relationForProductIndex != -1)
        {
          string fieldStringValue = this.GetFieldStringValue(this.avsDocument.Attr_DopZamenText, relationForProductIndex, productIndex, (List<RelationAttributeValuesCache>) null, false);
          if (!string.IsNullOrEmpty(fieldStringValue))
          {
            int num = dopZamenValues.IndexOf(fieldStringValue);
            if (num != -1)
            {
              dopZamenIndexes[index] = startDopZamenIndex + num;
            }
            else
            {
              dopZamenIndexes[index] = ++noteReferenceCount;
              dopZamenValues.Add(fieldStringValue);
            }
          }
        }
      }
      if (!dopZamenValues.IsNullOrEmpty<string>())
        return;
      startDopZamenIndex = -1;
    }
    else
    {
      startDopZamenIndex = -1;
      string fieldStringValue = this.GetFieldStringValue(this.avsDocument.Attr_DopZamenText, 0, -1, (List<RelationAttributeValuesCache>) null, false);
      if (string.IsNullOrEmpty(fieldStringValue))
        return;
      dopZamenValues.Add(fieldStringValue);
    }
  }

  private string FindFirstCountMeasure()
  {
    string countMeasure = "";
    for (int productIndex = 0; productIndex < this.avsDocument.ProductsInfo.Count; ++productIndex)
    {
      int relationForProductIndex = this.GetRelationForProductIndex(productIndex, this.relations);
      if (relationForProductIndex != -1)
      {
        MeasuredValue count = this.GetCount(relationForProductIndex, productIndex, this.relations);
        if (count != null)
        {
          MeasureDescriptor md = (MeasureDescriptor) null;
          this.ConvertCountToValueAndMeasure((object) count, ref md, out countMeasure);
          break;
        }
      }
    }
    return countMeasure;
  }

  private void CollectCountValuesForRow(
    TableData docRow,
    int firstProductIndex,
    bool showMeasureUnitsInNote,
    string[] editValues,
    string[] viewValues,
    out string countMeasure)
  {
    countMeasure = "";
    int countCellCount = this.CountCellCount;
    for (int index = 0; index < countCellCount; ++index)
    {
      int productIndex = firstProductIndex + index;
      int relationIndex;
      if (this.avsDocument.IsFormA && this.ProductID.IsDefinedId())
      {
        relationIndex = this.GetRelationIndexForProduct(this.ProductID, this.relations);
        productIndex = this.avsDocument.GetParentProductIndex(this.ProductID);
        if (productIndex == -1)
          productIndex = this.avsDocument.GetProductIndex(this.ProductID);
      }
      else
        relationIndex = this.GetRelationForProductIndex(productIndex, this.relations);
      if (relationIndex != -1 || !this.HasAnyRelations)
      {
        viewValues[index] = this.LocalGetCountInCellContext(docRow, relationIndex, productIndex, showMeasureUnitsInNote, out editValues[index], ref countMeasure);
      }
      else
      {
        editValues[index] = "";
        viewValues[index] = (string) null;
      }
    }
  }

  private int CountCellCount
  {
    get => !this.IsFormB || this.avsDocument == null ? 1 : this.avsDocument.RowProductCount;
  }

  /// <summary>Обновить сборное значение в графе "Примечание"</summary>
  /// <param name="cell">Ячейка, значение которой изменилось</param>
  /// <param name="attrInfo">Информация об атрибуте, значение которого изменилось</param>
  /// <param name="value">Новое значение атрибута</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateNoteDocCellText_OLD(
    TextData cell,
    AvsRowAttributeInfo attrInfo,
    string value,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsNoteRow)
      return;
    if (this._suspendUpdateNote > 0)
    {
      this._needUpdateNote = true;
    }
    else
    {
      if (this.avsDocument != null)
        this.avsDocument.Lock_DocCell_TextChanged();
      try
      {
        this._needUpdateNote = false;
        bool showMeasureUnitsInNote = (this.avsDocument.noteFieldSettings.Options & NoteFieldOptions.ShowMeasureUnits) != 0;
        bool flag1 = true;
        if (this.IsFormB && (cell == null || this.docNodes.Count > 1))
          flag1 = false;
        TableData tableData = (TableData) null;
        int num1 = 0;
        if (cell != null & flag1)
        {
          tableData = AVSDocument.FindParentSpecRowDocNode((DocumentTreeNode) cell) as TableData;
          num1 = this.docNodes.IndexOf(tableData);
        }
        bool isCountCell1 = false;
        int num2 = -1;
        int num3 = 0;
        int num4 = 1;
        if (this.IsFormB)
        {
          num3 = this.GetFirstProductIndexForDocRow((DocumentTreeNode) tableData);
          num4 = AVSRow.CalcCountCellsCount(this.DocRowFields);
        }
        if (cell != null && AVSRow.IsCountField(attrInfo))
        {
          isCountCell1 = true;
          if (this.IsFormB)
            num2 = this.GetProductIndexForCountCell(cell);
        }
        bool flag2 = true;
        string str1 = "\r\n";
        string attributeValue1 = (string) null;
        MeasureDescriptor md = (MeasureDescriptor) null;
        string countStrValue = (string) null;
        List<RemarkAttribute> items = this.avsDocument.noteFieldSettings.Items;
        this._hasNoteAndNoteAttributeCollision = this.avsDocument.IsElementList && !this.HasRelation && items.Contains<RemarkAttribute>((Predicate<RemarkAttribute>) (f => f.Name == AVSRow.DocAttr_NotePE)) && items.Contains<RemarkAttribute>((Predicate<RemarkAttribute>) (f => f.Name == AVSRow.DocAttr_Note));
        string str2;
        if (!isCountCell1 && this.Field_Note.Equals((AttributeInfo) attrInfo))
        {
          str2 = value;
          flag2 = false;
        }
        else
          str2 = this.GetFieldStringValue(this.Field_Note, 0, -1, (List<RelationAttributeValuesCache>) null, false, ignoreCellValue: this._hasNoteAndNoteAttributeCollision);
        string str3 = (string) null;
        string editValue1 = !this.Field_Format.Equals((AttributeInfo) attrInfo) ? this.GetFieldStringValue(this.Field_Format, 0, -1, (List<RelationAttributeValuesCache>) null, false) : value;
        bool flag3 = false;
        this.GetFirstProductIndexForDocRow((DocumentTreeNode) cell);
        AvsRowAttributeInfo attributeInfoForCell = this.GetAttributeInfoForCell(cell);
        DocumentTreeNode documentTreeNode = attributeInfoForCell == null || attributeInfoForCell.AttributeId != AvsIDCache.Attr_Format ? (DocumentTreeNode) this.GetDocumentCellForAttribute(this.Field_Format, -1) : (DocumentTreeNode) cell;
        if (documentTreeNode != null)
        {
          string attributeValue2 = documentTreeNode.GetAttributeValue(AVSRow.CellAttrName_ViewTextForFormat, false);
          if (attributeValue2 != null)
          {
            flag3 = true;
            str3 = attributeValue2;
          }
        }
        string editValue2 = isCountCell1 || !this.Field_Zone.Equals((AttributeInfo) attrInfo) ? this.GetFieldStringValue(this.Field_Zone, 0, -1, (List<RelationAttributeValuesCache>) null, false) : value;
        bool flag4 = (this.avsDocument.noteFieldSettings.Options & NoteFieldOptions.ShowMeasureUnits) != 0;
        for (int index1 = num1; flag1 || index1 < this.docNodes.Count; ++index1)
        {
          string countMeasure = (string) null;
          int num5 = -1;
          int num6 = -1;
          int num7 = 0;
          bool flag5 = this.IsFormB || !AVSRow.IsCountField(attrInfo);
          if (!flag1)
          {
            tableData = this.docNodes[index1];
            num3 = this.GetFirstProductIndexForDocRow((DocumentTreeNode) tableData);
            num4 = AVSRow.CalcCountCellsCount(this.DocRowFields);
          }
          TextData textData = ((!this.Field_Note.Equals((AttributeInfo) attrInfo) ? 0 : (cell != null ? 1 : 0)) & (flag1 ? 1 : 0)) == 0 ? this.GetDocumentCellForBaseField(this.Field_Note, tableData, 0) : cell;
          if (textData == null)
            return;
          string str4 = (string) null;
          List<string> stringList = (List<string>) null;
          int[] numArray = (int[]) null;
          int num8 = 1;
          int num9 = 0;
          int num10 = 0;
          int num11 = 0;
          for (int index2 = 0; index2 < items.Count; ++index2)
          {
            if (items[index2].ID == AvsIDCache.Attr_Format)
            {
              if (items[index2].AttrSource == AttributeSourceTypes.Object && editValue1 != null && editValue1.Length > AvsConfig.General.FormatSize)
                num5 = ++num7;
            }
            else if (items[index2].ID == AvsIDCache.Attr_Zone)
            {
              if (items[index2].AttrSource == AttributeSourceTypes.Relation && editValue2 != null && editValue2.Length > AvsConfig.General.ZoneSize)
                num6 = ++num7;
            }
            else if (items[index2].ID == AvsIDCache.Attr_DopZamenText && !this.IsDocRelation && this.RelType != AvsIDCache.Relation_AddComplect && items[index2].AttrSource == AttributeSourceTypes.Relation)
            {
              flag4 = false;
              if (this.IsFormB)
              {
                stringList = new List<string>(this.avsDocument.productsInfo.Count);
                numArray = new int[this.avsDocument.productsInfo.Count];
                num8 = num7 + 1;
                bool flag6 = false;
                for (int index3 = num3; index3 - num3 < num4 && index3 < this.avsDocument.productsInfo.Count; ++index3)
                {
                  ++num11;
                  int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[index3].Id, this.relations);
                  if (relationIndexForProduct != -1)
                  {
                    ++num10;
                    str4 = this.GetFieldStringValue(this.avsDocument.Attr_DopZamenText, relationIndexForProduct, -1, (List<RelationAttributeValuesCache>) null, false);
                    if (!string.IsNullOrEmpty(str4))
                    {
                      ++num9;
                      flag6 = true;
                      int num12 = stringList.IndexOf(str4);
                      if (num12 != -1)
                      {
                        numArray[index3] = num8 + num12;
                      }
                      else
                      {
                        numArray[index3] = ++num7;
                        stringList.Add(str4);
                      }
                    }
                    else if (!flag6 && string.IsNullOrEmpty(countMeasure))
                      countStrValue = this.ConvertCountToValueAndMeasure(num2 != index3 || num2 == -1 ? this.GetFieldValue(this.Field_Count, relationIndexForProduct, -1, this.relations, true, false) : (object) value, ref md, out countMeasure);
                  }
                }
                if (flag6)
                  countMeasure = "";
              }
              else
              {
                str4 = this.GetFieldStringValue(this.avsDocument.Attr_DopZamenText, 0, -1, (List<RelationAttributeValuesCache>) null, false);
                if (string.IsNullOrEmpty(str4))
                {
                  isCountCell1 = AVSRow.IsCountField(attrInfo);
                  countStrValue = this.ConvertCountToValueAndMeasure(!isCountCell1 ? this.GetFieldValue(this.Field_Count, 0, -1, this.relations, true, false) : (object) value, ref md, out countMeasure);
                }
                else
                  countMeasure = "";
              }
            }
          }
          if (flag4 && !this.IsDocRelation)
          {
            flag4 = false;
            if (!this.IsDocRelation)
            {
              if (this.IsFormB)
              {
                num8 = num7 + 1;
                for (int index4 = num3; index4 - num3 < num4 && index4 < this.avsDocument.productsInfo.Count; ++index4)
                {
                  countStrValue = (string) null;
                  int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[index4].Id, this.relations);
                  if (relationIndexForProduct != -1 && string.IsNullOrEmpty(countMeasure))
                  {
                    countStrValue = this.ConvertCountToValueAndMeasure(num2 != index4 || num2 == -1 ? (object) this.GetCount(relationIndexForProduct, -1, this.relations) : (object) value, ref md, out countMeasure);
                    break;
                  }
                }
              }
              else
              {
                isCountCell1 = AVSRow.IsCountField(attrInfo);
                countStrValue = this.ConvertCountToValueAndMeasure(!isCountCell1 ? (object) this.GetCount(0, -1, this.relations) : (object) value, ref md, out countMeasure);
              }
            }
          }
          int num13 = 0;
          int num14 = -1;
          int num15 = -1;
          if (num5 == -1 && editValue1 != null && editValue1.Length > AvsConfig.General.FormatSize)
            num14 = ++num13;
          if (flag3)
          {
            num5 = -1;
            num14 = -1;
          }
          if (num6 == -1 && editValue2 != null && editValue2.Length > AvsConfig.General.ZoneSize)
            num15 = ++num13;
          string planeText = "";
          int protectedStartZone = -1;
          int num16 = -1;
          string str5 = "";
          string str6 = "";
          if (this.avsDocument.noteFieldSettings.Items.Find((Predicate<RemarkAttribute>) (x => x.ID == AvsIDCache.Attr_Count)) == null && !string.IsNullOrEmpty(countMeasure) && (this.avsDocument.noteFieldSettings.Options & NoteFieldOptions.ShowMeasureUnits) != NoteFieldOptions.None)
          {
            if (planeText != "")
              planeText += " ";
            planeText += countMeasure;
          }
          int num17;
          if (num14 != -1)
          {
            if (planeText != "")
              planeText += " ";
            string str7;
            if (num7 + num13 != 1)
            {
              num17 = num13 + num15;
              str7 = $"*{num17.ToString()})";
            }
            else
              str7 = "*)";
            string viewValue = str7;
            planeText = $"{planeText}{viewValue}\u000E{editValue1}";
            if (this.Field_Format.Equals((AttributeInfo) attrInfo) && cell != null)
              attributeValue1 = viewValue;
            else
              this.SetFieldValueInDocRowsCell(this.Field_Format, tableData, -1, editValue1, viewValue);
          }
          else if (num5 == -1 && (!this.Field_Format.Equals((AttributeInfo) attrInfo) || cell == null))
          {
            TextData cellForAttribute = this.GetDocumentCellForAttribute(this.Field_Format, -1);
            string viewValue = (string) null;
            if (cellForAttribute != null)
            {
              viewValue = cellForAttribute.GetAttributeValue(AVSRow.CellAttrName_ViewTextForFormat, false);
              if (viewValue == null)
                viewValue = (string) null;
              else
                str3 = viewValue;
            }
            this.SetFieldValueInDocRowsCell(this.Field_Format, tableData, -1, editValue1, viewValue);
          }
          if (num15 != -1)
          {
            if (planeText != "")
              planeText += " ";
            string str8;
            if (num7 + num13 != 1)
            {
              num17 = num13 + num15;
              str8 = $"*{num17.ToString()})";
            }
            else
              str8 = "*)";
            string viewValue = str8;
            planeText = $"{planeText}{viewValue}\u000E{editValue2}";
            if (this.Field_Zone.Equals((AttributeInfo) attrInfo) && cell != null)
              attributeValue1 = viewValue;
            else
              this.SetFieldValueInDocRowsCell(this.Field_Zone, tableData, -1, editValue2, viewValue);
          }
          else if (num6 == -1 && (!this.Field_Zone.Equals((AttributeInfo) attrInfo) || cell == null))
            this.SetFieldValueInDocRowsCell(this.Field_Zone, tableData, -1, editValue2);
          string str9 = " ";
          string str10;
          for (int index5 = 0; index5 < items.Count; ++index5)
          {
            if (items[index5].ID == AvsIDCache.Attr_Format)
            {
              if (items[index5].AttrSource == AttributeSourceTypes.Object && num5 != -1 && editValue1 != "")
              {
                if (planeText != "")
                {
                  string str11;
                  if (index5 <= 0)
                    str11 = str10 = planeText + " ";
                  else
                    str10 = str11 = planeText + str9;
                  planeText = str11;
                }
                string str12;
                if (num7 + num13 != 1)
                {
                  num17 = num13 + num5;
                  str12 = $"*{num17.ToString()})";
                }
                else
                  str12 = "*)";
                string viewValue = str12;
                planeText = $"{planeText}{viewValue}\u000E{editValue1}";
                str9 = items[index5].Separator;
                if (this.Field_Format.Equals((AttributeInfo) attrInfo) && cell != null)
                  attributeValue1 = viewValue;
                else
                  this.SetFieldValueInDocRowsCell(this.Field_Format, tableData, -1, editValue1, viewValue);
              }
            }
            else if (items[index5].ID == AvsIDCache.Attr_Zone)
            {
              if (items[index5].AttrSource == AttributeSourceTypes.Relation && (num6 != -1 || !this.avsDocument.IsSpecification) && !string.IsNullOrEmpty(editValue2))
              {
                if (planeText != "")
                {
                  string str13;
                  if (index5 <= 0)
                    str13 = str10 = planeText + " ";
                  else
                    str10 = str13 = planeText + str9;
                  planeText = str13;
                }
                string viewValue;
                if (num6 != -1)
                {
                  string str14;
                  if (num7 + num13 != 1)
                  {
                    num17 = num13 + num6;
                    str14 = $"*{num17.ToString()})";
                  }
                  else
                    str14 = "*)";
                  viewValue = str14;
                  planeText = $"{planeText}{viewValue}\u000E{editValue2}";
                }
                else
                {
                  viewValue = (string) null;
                  planeText += editValue2;
                }
                str9 = items[index5].Separator;
                if (this.Field_Zone.Equals((AttributeInfo) attrInfo) && cell != null && !string.IsNullOrEmpty(viewValue))
                  attributeValue1 = viewValue;
                else
                  this.SetFieldValueInDocRowsCell(this.Field_Zone, tableData, -1, editValue2, viewValue);
              }
            }
            else if (items[index5].ID == AvsIDCache.Attr_DopZamenText)
            {
              if (items[index5].AttrSource == AttributeSourceTypes.Relation)
              {
                if (this.IsFormB)
                {
                  if (stringList != null && stringList.Count > 0)
                  {
                    flag5 = false;
                    string str15;
                    for (int index6 = 0; index6 < stringList.Count; ++index6)
                    {
                      if (index6 > 0 || str5 != "")
                        str5 += str1;
                      str15 = "";
                      string str16;
                      if (num13 + num7 == 1)
                      {
                        str16 = "*)";
                      }
                      else
                      {
                        num17 = num13 + num8 + index6;
                        str16 = $"*{num17.ToString()})";
                      }
                      str5 = !(str16 != "") ? str5 + stringList[index6] : $"{str5}{str16}\u000E{stringList[index6]}";
                    }
                    for (int index7 = num3; index7 - num3 < num4 && index7 < numArray.Length; ++index7)
                    {
                      bool isCountCell2 = num2 == index7 & isCountCell1 && cell != null;
                      if (numArray[index7] > 0)
                      {
                        str15 = "";
                        string viewValue = num13 + num7 != 1 ? $"*{numArray[index7].ToString()})" : "*)";
                        if (isCountCell2)
                        {
                          attributeValue1 = viewValue;
                          countStrValue = attributeValue1;
                        }
                        else
                        {
                          int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[index7].Id, this.relations);
                          this.LocalGetCountInCellContext(isCountCell2, cell, tableData, value, relationIndexForProduct, index7, showMeasureUnitsInNote, out countStrValue, ref countMeasure);
                          this.SetFieldValueInDocRowsCell(this.Field_Count, tableData, index7, countStrValue, viewValue);
                        }
                      }
                      else if (index7 < this.avsDocument.productsInfo.Count)
                      {
                        int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[index7].Id, this.relations);
                        string countInCellContext = this.LocalGetCountInCellContext(isCountCell2, cell, tableData, value, relationIndexForProduct, index7, showMeasureUnitsInNote, out countStrValue, ref countMeasure);
                        if (isCountCell2)
                          attributeValue1 = countStrValue;
                        else
                          this.SetFieldValueInDocRowsCell(this.Field_Count, tableData, index7, countStrValue, countInCellContext);
                      }
                    }
                  }
                }
                else if (!string.IsNullOrEmpty(str4))
                {
                  str5 = str4;
                  if (AVSRow.IsCountField(attrInfo) && cell != null && this.avsDocument.IsSpecification)
                  {
                    countStrValue = attributeValue1 = "";
                  }
                  else
                  {
                    string viewValue = this.LocalGetCountInCellContext(isCountCell1, cell, tableData, value, 0, -1, showMeasureUnitsInNote, out countStrValue, ref countMeasure);
                    if (this.avsDocument.IsSpecification)
                      viewValue = "";
                    this.SetFieldValueInDocRowsCell(this.Field_Count, tableData, -1, countStrValue, viewValue);
                  }
                  flag5 = false;
                }
                if (str5 != "")
                {
                  if (planeText != "")
                  {
                    string str17;
                    if (index5 <= 0)
                      str17 = str10 = planeText + " ";
                    else
                      str10 = str17 = planeText + str9;
                    planeText = str17;
                  }
                  planeText += str5;
                  str9 = items[index5].Separator;
                }
              }
            }
            else if (items[index5].ID == this.Field_Note.AttributeId)
            {
              if (items[index5].AttrSource == AttributeSourceTypes.Relation)
              {
                protectedStartZone = planeText.Length;
                if (str2 != "" && str2 != null)
                {
                  if (planeText != "")
                  {
                    planeText = index5 <= 0 ? planeText + " " : planeText + str9;
                    if (items[index5].Separator != null)
                    {
                      int length = items[index5].Separator.Length;
                    }
                  }
                  protectedStartZone = planeText.Length;
                  planeText += str2;
                  str9 = items[index5].Separator;
                }
                num16 = planeText.Length;
                if (this.Field_Note.Equals((AttributeInfo) attrInfo))
                  flag2 = false;
              }
            }
            else if (items[index5].ID == AvsIDCache.Attr_Count && (this.avsDocument.noteFieldSettings.Options & NoteFieldOptions.ShowMeasureUnits) != NoteFieldOptions.None)
            {
              if (items[index5].AttrSource == AttributeSourceTypes.Relation)
              {
                str6 = countMeasure;
                if (!string.IsNullOrEmpty(str6))
                {
                  if (planeText != "")
                    planeText = index5 <= 0 ? planeText + " " : planeText + str9;
                  planeText += str6;
                  str9 = items[index5].Separator;
                }
              }
            }
            else if (!items[index5].WithoutDrawing || MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing))
            {
              if (items[index5].ID == AvsIDCache.Attr_PosDesignation)
              {
                if (string.IsNullOrEmpty(str4) && (stringList == null || stringList.Count == 0))
                  str6 = this.GetPosDesignationForNoteField(this.Field_PosDesignation);
              }
              else if (items[index5].ID == AvsIDCache.Attr_PodborForPosDesignation)
              {
                str6 = this.GetPosDesignationForNoteField(this.Attr_PodborForPosDesignation);
              }
              else
              {
                bool ignoreCellValue = this._hasNoteAndNoteAttributeCollision && items[index5].Name == "Примечание";
                AvsRowAttributeInfo attrInfo1 = new AvsRowAttributeInfo(items[index5].AttrSource == AttributeSourceTypes.Relation, items[index5].ID);
                attrInfo1.Name = items[index5].Name;
                str6 = this.GetFieldStringValue(attrInfo1, 0, -1, (List<RelationAttributeValuesCache>) null, false, ignoreCellValue: ignoreCellValue);
              }
              if (str6 != "")
              {
                if (planeText != "")
                {
                  string str18;
                  if (index5 <= 0)
                    str18 = str10 = planeText + " ";
                  else
                    str10 = str18 = planeText + str9;
                  planeText = str18;
                }
                planeText += str6;
                str9 = items[index5].Separator;
              }
            }
          }
          if (flag5 && this.HideCountForDocuments)
          {
            if (this.IsFormB)
            {
              for (int index8 = num3; index8 - num3 < num4 && index8 < this.avsDocument.productsInfo.Count; ++index8)
              {
                int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[index8].Id, this.relations);
                bool isCountCell3 = num2 == index8 & isCountCell1;
                string countInCellContext = this.LocalGetCountInCellContext(isCountCell3, cell, tableData, value, relationIndexForProduct, index8, showMeasureUnitsInNote, out countStrValue, ref countMeasure);
                if (!isCountCell3)
                  this.SetFieldValueInDocRowsCell(this.Field_Count, tableData, index8, countStrValue, countInCellContext);
              }
            }
            else
            {
              string countInCellContext = this.LocalGetCountInCellContext(isCountCell1, cell, tableData, value, 0, -1, showMeasureUnitsInNote, out countStrValue, ref countMeasure);
              this.SetFieldValueInDocRowsCell(this.Field_Count, tableData, -1, countStrValue, countInCellContext);
            }
          }
          if (textData is TextBoxElement textBox)
          {
            textBox.AssignProtectedFirstCharCount(0);
            textBox.AssignProtectedEndCharCount(0);
          }
          int length1 = planeText.Length;
          string str19 = TextData.DeleteLastEndLine(planeText, false);
          int length2 = str19.Length;
          int num18 = length1 - length2;
          if (num18 != 0)
            num16 -= num18;
          if (protectedStartZone != 0 && protectedStartZone == str19.Length)
          {
            str19 += " ";
            ++protectedStartZone;
            ++num16;
          }
          textData.AssignText(str19, false, true, false, updateUI, updateLayout);
          int protectedEndZone = -1;
          if (num16 != -1)
            protectedEndZone = str19.Length - num16;
          this.SetupProtectedZonesInTextBox(textBox, protectedStartZone, protectedEndZone);
          if (flag1)
            break;
        }
        if (!(cell != null & flag2))
          return;
        if (AVSRow.IsCountField(attrInfo))
        {
          if (cell.InPlaceEditorActive || countStrValue == null)
            cell.AssignText(value, false, true, false, false, false);
          else
            cell.AssignText(countStrValue, false, true, false, false, false);
          if (attributeValue1 != null && attributeValue1 != value)
          {
            cell.SetAttributeValue(AVSRow.CellAttrName_EditText, value, false, false, false);
            cell.SetAttributeValue(AVSRow.CellAttrName_ViewText, attributeValue1, false, false, false);
          }
          else
          {
            cell.RemoveAttribute(AVSRow.CellAttrName_EditText, false, false);
            cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
          }
        }
        else if (!cell.InPlaceEditorActive && attributeValue1 != null)
        {
          cell.AssignText(attributeValue1, false, true, false, false, false);
          if (!this.Field_Format.Equals((AttributeInfo) attrInfo) && !this.Field_Zone.Equals((AttributeInfo) attrInfo))
            return;
          cell.SetAttributeValue(AVSRow.CellAttrName_EditText, value, false, false, false);
        }
        else
        {
          string attributeValue3 = attributeValue1;
          if (this.Field_Format.Equals((AttributeInfo) attrInfo) && str3 != null)
            attributeValue3 = str3;
          if (attributeValue3 != null && attributeValue3 != value)
            cell.AssignText(attributeValue3, false, true, false, false, false);
          else
            cell.AssignText(value, false, true, false, false, false);
          if (attributeValue3 == null)
            cell.RemoveAttribute(AVSRow.CellAttrName_ViewText, false, false);
          else
            cell.SetAttributeValue(AVSRow.CellAttrName_ViewText, attributeValue3, false, false, false);
          if (!this.Field_Format.Equals((AttributeInfo) attrInfo) && !this.Field_Zone.Equals((AttributeInfo) attrInfo))
            return;
          cell.SetAttributeValue(AVSRow.CellAttrName_EditText, value, false, false, false);
        }
      }
      finally
      {
        if (this.avsDocument != null)
          this.avsDocument.Unlock_DocCell_TextChanged();
      }
    }
  }

  private void SetupProtectedZonesInTextBox(
    TextBoxElement textBox,
    int protectedStartZone,
    int protectedEndZone)
  {
    if (textBox == null)
      return;
    if (protectedStartZone == -1)
      textBox.ReadOnly = true;
    if (protectedEndZone == -1)
      protectedEndZone = 0;
    textBox.AssignProtectedZone(protectedStartZone, protectedEndZone);
    int num;
    if (protectedStartZone > 0)
    {
      TextBoxElement textBoxElement = textBox;
      num = textBox.ProtectedFirstCharCount;
      string attributeValue = num.ToString();
      textBoxElement.SetAttributeValue("ProtectedFirstCharCount", attributeValue, false, false, false);
    }
    else
      textBox.RemoveAttribute("ProtectedFirstCharCount", false, false);
    if (protectedEndZone > 0)
    {
      TextBoxElement textBoxElement = textBox;
      num = textBox.ProtectedEndCharCount;
      string attributeValue = num.ToString();
      textBoxElement.SetAttributeValue("ProtectedEndCharCount", attributeValue, false, false, false);
    }
    else
      textBox.RemoveAttribute("ProtectedEndCharCount", false, false);
  }

  private string LocalGetCountInCellContext(
    TableData docRow,
    int relationIndex,
    int productIndex,
    bool showMeasureUnitsInNote,
    out string countStrValue,
    ref string countMeasure)
  {
    return this.LocalGetCountInCellContext(false, (TextData) null, docRow, (string) null, relationIndex, productIndex, showMeasureUnitsInNote, out countStrValue, ref countMeasure);
  }

  private string LocalGetCountInCellContext(
    bool isCountCell,
    TextData cell,
    TableData docRow,
    string value,
    int relationIndex,
    int productIndex,
    bool showMeasureUnitsInNote,
    out string countStrValue,
    ref string countMeasure)
  {
    MeasuredValue measuredValue;
    TextData cell1;
    if (isCountCell)
    {
      measuredValue = AVSRow.ConvertCountToMeasuredValue((object) value);
      cell1 = cell;
    }
    else
    {
      measuredValue = this.GetCount(relationIndex, productIndex, this.relations);
      if (this.IsFormB && measuredValue == null && this.IsDocRelation && relationIndex != -1)
        measuredValue = new MeasuredValue(1.0, AVSRow.DefaultCountID);
      cell1 = this.GetDocumentCellForBaseField(this.Field_Count, docRow, productIndex);
    }
    bool flag = true;
    if (measuredValue != null && !string.IsNullOrEmpty(countMeasure) && measuredValue.MeasureID != AVSRow.DefaultCountID)
    {
      MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
      MeasureDescriptor descriptor2 = MeasureHelper.FindDescriptor(countMeasure);
      if (descriptor2 != null && !descriptor2.Empty)
      {
        if (descriptor2.MeasureID != measuredValue.MeasureID)
        {
          if (descriptor1.PhysicalQuantityID == descriptor2.PhysicalQuantityID)
            measuredValue = MeasureHelper.ConvertToMeasuredValue(measuredValue, descriptor2.MeasureID);
          else
            flag = false;
        }
        else
        {
          string countMeasure1;
          this.ConvertCountToStringForDocCell(measuredValue, cell1, true, out string _, out countMeasure1);
          if (countMeasure1 != countMeasure)
            measuredValue = new MeasuredValue(measuredValue.Value, measuredValue.MeasureID);
        }
      }
    }
    string countMeasure2;
    string countInCellContext = this.ConvertCountToStringForDocCell(measuredValue, cell1, showMeasureUnitsInNote & flag, out countStrValue, out countMeasure2);
    if (string.IsNullOrEmpty(countMeasure))
      countMeasure = countMeasure2;
    if (countInCellContext == countStrValue)
      countInCellContext = (string) null;
    return countInCellContext;
  }

  public void UpdateNameDocCellText(bool updateUI, bool updateLayout)
  {
    if (!this.HasDocNodes)
      return;
    this.UpdateNameDocCellText((TextData) null, updateUI, updateLayout);
  }

  /// <summary>Обновить ячейку наименования</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="value">Значение атрибута "Наименование"</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  public void UpdateNameDocCellText(TextData cell, bool updateUI, bool updateLayout)
  {
    if (this.IsNoteRow || !this.HasDocNodes && cell == null)
      return;
    List<TextData> source;
    if (cell != null)
      source = new List<TextData>() { cell };
    else
      source = this.GetDocumentCellsForBaseField(this.Field_Name, -1);
    if (source.Count == 0)
      return;
    CellOutputMapping attributeMapping1 = this.GetCellAttributeMapping(source.First<TextData>());
    if (attributeMapping1 == null || attributeMapping1.IsHidden)
    {
      string forSpecification = this.GetVirtualNameForSpecification(false);
      this.UpdateNameDocCellText_OLD(cell, forSpecification, updateUI, updateLayout);
    }
    else
    {
      if (this.avsDocument != null)
      {
        this.avsDocument.Lock_DocCell_TextChanged();
        if (updateUI | updateLayout)
          this.avsDocument.SuspendDocumentAndGridUpdates();
      }
      try
      {
        int index1 = -1;
        int index2 = -1;
        List<string> itemValues = new List<string>(attributeMapping1.Items.Count);
        for (int index3 = 0; index3 < attributeMapping1.Items.Count; ++index3)
        {
          string str;
          if (attributeMapping1.Items[index3] is AttributeMapping attributeMapping2)
          {
            if (attributeMapping2.AttributeID == AvsIDCache.Attr_AdditionalNameNote.AttributeId)
            {
              str = "";
              index1 = index3;
            }
            else
            {
              AvsRowAttributeInfo attrInfo = new AvsRowAttributeInfo(attributeMapping2.AttributeInfo);
              str = this.GetFieldValueForDocCell(attrInfo, -1, -1, false, false);
              if (this.Field_Name.Equals((AttributeInfo) attrInfo) || AvsIDCache.Attr_NameForSpecification.Equals((AttributeInfo) attrInfo))
                index2 = index3;
            }
          }
          else
            str = ((DelimiterMapping) attributeMapping1.Items[index3]).DelimiterRTF;
          itemValues.Add(str);
        }
        string str1 = "";
        string str2 = "";
        string header = "";
        if (index2 != -1)
        {
          str1 = itemValues[index2];
          this.UpdateDocRowDynamicHeader(this.avsDocument.DynamicGroupHeaderSettings, out header);
          if (!string.IsNullOrEmpty(header))
            str2 = this.CalcRowNameInDynamicGroup();
        }
        for (int index4 = 0; index4 < source.Count; ++index4)
        {
          TextBoxElement textBox = source[index4] as TextBoxElement;
          TableData tableData = (AVSDocument.FindParentNoteRowDocNode((DocumentTreeNode) source[index4]) ?? AVSDocument.FindParentSpecRowDocNode((DocumentTreeNode) source[index4])) as TableData;
          int productIndex = -1;
          if (this.IsFormB)
            productIndex = this.GetFirstProductIndexForDocRow((DocumentTreeNode) tableData);
          int protectedStartZone = -1;
          int protectedEndZone = 0;
          if (this.avsDocument.IsSpecification)
          {
            textBox.AssignProtectedZone(0, 0);
            if (index1 != -1)
            {
              FieldContext context = new FieldContext(this, -1, productIndex, (List<RelationAttributeValuesCache>) null)
              {
                DocCell = source[index4],
                DocRow = tableData
              };
              itemValues[index1] = this.GetFieldStringValue(AvsIDCache.Attr_AdditionalNameNote, context, false);
            }
          }
          IList<string> itemValuesWithoutUnnecessaryDelimeters;
          string str3 = attributeMapping1.ConcatenateAttributesValues((IList<string>) itemValues, out itemValuesWithoutUnnecessaryDelimeters);
          if (string.IsNullOrEmpty(header))
          {
            this.UpdateCellRefToTextSource(source[index4], this.Field_Name);
            this.UpdateDocRowDynamicHeaderTextVariants((DocumentTreeNode) source[index4], "", "");
            source[index4].AssignText(str3, false, true, false, updateUI, updateLayout);
            if (index1 != -1)
            {
              protectedStartZone = 0;
              for (int index5 = 0; index5 < index1; ++index5)
                protectedStartZone += TextData.CharCountInEditor(itemValuesWithoutUnnecessaryDelimeters[index5]);
              int num = TextData.CharCountInEditor(itemValuesWithoutUnnecessaryDelimeters[index1]);
              protectedEndZone = TextData.CharCountInEditor(str3) - (protectedStartZone + num);
            }
          }
          else
          {
            string textForGroup;
            if (index2 != -1)
            {
              itemValuesWithoutUnnecessaryDelimeters[index2] = str2;
              textForGroup = string.Concat((IEnumerable<string>) itemValuesWithoutUnnecessaryDelimeters);
              itemValuesWithoutUnnecessaryDelimeters[index2] = str1;
            }
            else
              textForGroup = str3;
            this.UpdateDocRowDynamicHeaderTextVariants((DocumentTreeNode) tableData, str3, textForGroup);
            protectedStartZone = -1;
            if (source[index4].ReferenceToTextSource is ReferenceToNodeAttribute referenceToTextSource)
            {
              if (referenceToTextSource.NodeLink != tableData)
                referenceToTextSource.AssignNodeLink((DocumentTreeNode) tableData);
            }
            else
            {
              tableData.SetAttributeValue("GroupHeaderCellText", source[index4].Text, false, false, false);
              source[index4].AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) source[index4], BaseReferenceNodeType.ntSelectedNode, tableData.Id, "GroupHeaderCellText"), true, false, false);
            }
          }
          if (textBox != null)
          {
            if (this.avsDocument.IsSpecification)
              this.SetupProtectedZonesInTextBox(textBox, protectedStartZone, protectedEndZone);
            string attributeValue = textBox.GetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, false);
            string fieldStringValue = this.GetFieldStringValue(this.avsDocument.Attr_GOST, -1, -1, (List<RelationAttributeValuesCache>) null, false);
            string str4 = fieldStringValue;
            if (attributeValue != str4)
            {
              if (!string.IsNullOrEmpty(fieldStringValue))
                textBox.SetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, fieldStringValue, false, false, false);
              else
                textBox.RemoveAttribute(DocumentTreeNode.AttributeName_NBreakTxt, false, false);
              textBox.ResetTextBoxPaintCache();
            }
          }
        }
      }
      finally
      {
        if (this.avsDocument != null)
        {
          this.avsDocument.Unlock_DocCell_TextChanged();
          if (updateUI | updateLayout)
            this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
        }
      }
    }
  }

  /// <summary>Обновить ячейку наименования. Устаревшая версия метода. Нужна на случай отсутствия настроек для новой версии</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="value">Значение атрибута "Наименование"</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  internal void UpdateNameDocCellText_OLD(
    TextData cell,
    string value,
    bool updateUI,
    bool updateLayout)
  {
    if (this.IsNoteRow || !this.HasDocNodes && cell == null)
      return;
    string str1 = TextData.DeleteLastEndLine(value, true);
    List<TextData> textDataList;
    if (cell != null)
    {
      textDataList = new List<TextData>();
      textDataList.Add(cell);
    }
    else
      textDataList = this.GetDocumentCellsForBaseField(this.Field_Name, -1);
    if (textDataList.Count == 0)
      return;
    if (this.avsDocument != null)
    {
      this.avsDocument.Lock_DocCell_TextChanged();
      if (updateUI | updateLayout)
        this.avsDocument.SuspendDocumentAndGridUpdates();
    }
    try
    {
      string str2 = "";
      string str3 = "";
      if (this.avsDocument.IsSpecification)
      {
        if (this.avsDocument.AVSCommonPropertiesSchema.AutoGenerateTextLinkToMainDocumentInNameField)
          str2 = this.GetFieldStringValue(AvsIDCache.Attr_LookMainDocTextLink, -1, -1, (List<RelationAttributeValuesCache>) null, false);
        str3 = this.GetFieldStringValue(AvsIDCache.Attr_DraftForPartTextLink, -1, -1, (List<RelationAttributeValuesCache>) null, false);
      }
      string str4 = AVSRow.JoinWithoutEmptyValues(Environment.NewLine, str1, str2, str3);
      string str5 = "";
      string header;
      this.UpdateDocRowDynamicHeader(this.avsDocument.DynamicGroupHeaderSettings, out header);
      if (!string.IsNullOrEmpty(header))
        str5 = AVSRow.JoinWithoutEmptyValues(Environment.NewLine, this.CalcRowNameInDynamicGroup(), str2, str3);
      int num = 0;
      for (int index = 0; index < textDataList.Count; ++index)
      {
        TextBoxElement textBoxElement = textDataList[index] as TextBoxElement;
        string str6 = "";
        TableData tableData = (AVSDocument.FindParentNoteRowDocNode((DocumentTreeNode) textDataList[index]) ?? AVSDocument.FindParentSpecRowDocNode((DocumentTreeNode) textDataList[index])) as TableData;
        int productIndex = -1;
        if (this.IsFormB)
          productIndex = this.GetFirstProductIndexForDocRow((DocumentTreeNode) tableData);
        if (this.avsDocument.IsSpecification)
        {
          textBoxElement?.AssignProtectedFirstCharCount(0);
          if (str4 != null)
            num = str4.Length;
          FieldContext context = new FieldContext(this, -1, productIndex, (List<RelationAttributeValuesCache>) null)
          {
            DocCell = textDataList[index],
            DocRow = tableData
          };
          str6 = this.GetFieldStringValue(AvsIDCache.Attr_AdditionalNameNote, context, false);
        }
        string originalText = str4 + str6;
        if (string.IsNullOrEmpty(header))
        {
          this.UpdateCellRefToTextSource(textDataList[index], this.Field_Name);
          this.UpdateDocRowDynamicHeaderTextVariants((DocumentTreeNode) textDataList[index], "", "");
          textDataList[index].AssignText(originalText, false, true, false, updateUI, updateLayout);
        }
        else
        {
          string textForGroup = str5 + str6;
          this.UpdateDocRowDynamicHeaderTextVariants((DocumentTreeNode) tableData, originalText, textForGroup);
          textDataList[index].ReadOnly = true;
          num = -1;
          if (textDataList[index].ReferenceToTextSource == null || !(textDataList[index].ReferenceToTextSource is ReferenceToNodeAttribute referenceToTextSource) || referenceToTextSource.NodeId != tableData.Id)
          {
            tableData.SetAttributeValue("GroupHeaderCellText", textDataList[index].Text, false, false, false);
            textDataList[index].AssignReferenceToTextSource((ReferenceBase) new ReferenceToNodeAttribute((DocumentTreeNode) textDataList[index], BaseReferenceNodeType.ntSelectedNode, tableData.Id, "GroupHeaderCellText"), true, false, false);
          }
        }
        if (textBoxElement != null)
        {
          if (this.avsDocument.IsSpecification)
            textBoxElement.AssignProtectedFirstCharCount(num);
          string attributeValue = textBoxElement.GetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, false);
          string fieldStringValue = this.GetFieldStringValue(this.avsDocument.Attr_GOST, -1, -1, (List<RelationAttributeValuesCache>) null, false);
          string str7 = fieldStringValue;
          if (attributeValue != str7)
          {
            if (!string.IsNullOrEmpty(fieldStringValue))
              textBoxElement.SetAttributeValue(DocumentTreeNode.AttributeName_NBreakTxt, fieldStringValue, false, false, false);
            else
              textBoxElement.RemoveAttribute(DocumentTreeNode.AttributeName_NBreakTxt, false, false);
            textBoxElement.ResetTextBoxPaintCache();
          }
        }
        if (this.avsDocument.IsSpecification)
        {
          if (num > 0)
            textDataList[index].SetAttributeValue("ProtectedFirstCharCount", num.ToString(), false, false, false);
          else
            textDataList[index].RemoveAttribute("ProtectedFirstCharCount", false, false);
        }
      }
    }
    finally
    {
      if (this.avsDocument != null)
      {
        this.avsDocument.Unlock_DocCell_TextChanged();
        if (updateUI | updateLayout)
          this.avsDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
      }
    }
  }

  private string CalcVirtualAttrDraftForPart()
  {
    string str1 = "";
    if (this.RelType == AvsIDCache.Relation_Zagotovka)
    {
      string str2 = this.DocNode.GetAttributeValue(AVSRow.DocAttr_ZagotovkaDlya, true);
      if (string.IsNullOrWhiteSpace(str2))
      {
        long num = this.GetFieldInt64Value(new AvsRowAttributeInfo(true, AvsIDCache.Attr_ArticleID), 0, (List<RelationAttributeValuesCache>) null, false);
        if (num.IsUndefinedId())
        {
          Guid guid = AvsIDCache.ConvertToGuid((object) this.DocNode.GetAttributeValue(AVSRow.DocAttr_PartFromDraftGuid, false));
          if (guid != Guid.Empty)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              num = sessionKeeper.Session.GetObjectInfo(guid).ObjectID;
          }
        }
        if (num.IsDefinedId())
        {
          AVSRow avsRow = this.avsDocument.GetAvsRowsByObjectId(num).FirstOrDefault<AVSRow>();
          if (avsRow != null)
            str2 = avsRow.DesignationOrName;
        }
      }
      if (!string.IsNullOrWhiteSpace(str2))
        str1 = $"({AvsConfig.General.ZagotovkaDlya} {str2})";
    }
    return str1;
  }

  /// <summary>Вычисляет значение для виртуального атрибута "Смотри" для вставки в Наименование</summary>
  /// <returns></returns>
  private string CaclVirtualAttrSmotri()
  {
    string attributeValue = this.DocNode?.GetAttributeValue(AVSRow.DocAttr_Smotri, true);
    return !string.IsNullOrEmpty(attributeValue) ? $"(см. {attributeValue})" : "";
  }

  /// <summary>Синхронизировать строку спецификации со строкой документа</summary>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку</param>
  /// <param name="updateNote">Обновлять ячейку примечания в строке документа</param>
  public void LoadDataFromDocRow(
    TableData docRow,
    bool updateUI,
    bool updateLayout,
    bool updateNote)
  {
    this._skipLinesBefore = docRow != null ? this.ReadSkipBeforeValueFromDocRowAttribute(docRow) : throw new ArgumentNullException(nameof (docRow));
    this._skipLinesAfter = this.ReadSkipAfterValueFromDocRowAttribute(docRow);
    if (this.avsDocument.ReadOnly || this.IsNoteRow || this.IsHiddenRow)
      return;
    this.avsDocument.Lock_DocCell_TextChanged();
    try
    {
      foreach (TextData cell in (IEnumerable<TextData>) docRow.TextCellsEnumerator)
      {
        CellOutputMapping attributeMapping = this.GetCellAttributeMapping(cell);
        int productIndex;
        AvsRowAttributeInfo cellBaseFieldInfo = this.GetCellBaseFieldInfo(cell, out productIndex);
        AVSRow.UpdateProtectedCharsZoneInCell(cell);
        if (this.Field_Format.Equals((AttributeInfo) cellBaseFieldInfo))
        {
          if ((!this.IsDocRelation ? 1 : (MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_DetailWithoutDrawing) ? 1 : 0)) != 0)
          {
            string str = cell.GetAttributeValue(AVSRow.CellAttrName_EditText, false);
            if (string.IsNullOrEmpty(str))
              str = cell.Text;
            this.SetFieldValue(cellBaseFieldInfo, -1, -1, (List<RelationAttributeValuesCache>) null, (object) str, false, false, false, false, false, false);
          }
        }
        else if (AVSRow.IsCountField(cellBaseFieldInfo))
        {
          if (this.HasRelation && cell.Text.IndexOf('/') != -1)
          {
            int relationIndex = -1;
            bool flag = true;
            if (this.IsFormB)
            {
              if (productIndex < this.avsDocument.productsInfo.Count)
                relationIndex = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[productIndex].Id, this.relations);
              flag = relationIndex != -1;
            }
            if (flag)
            {
              string valueFromDocCell = AVSRow.GetFieldValueFromDocCell(cell);
              MeasuredValue measuredValue1 = AVSRow.ConvertCountToMeasuredValue((object) valueFromDocCell, false);
              MeasuredValue measuredValue2 = AVSRow.ConvertCountToMeasuredValue(this.GetFieldValue(cellBaseFieldInfo, relationIndex, productIndex, this.relations, true, false));
              if (measuredValue1 != null && measuredValue2 != null && MeasureHelper.Compare(measuredValue1, measuredValue2) == CompareResult.Equal)
                measuredValue2.Caption = valueFromDocCell;
            }
          }
        }
        else if (this.Field_Designation.Equals((AttributeInfo) cellBaseFieldInfo))
        {
          string attributeValue = cell.GetAttributeValue("FullDesignation", true);
          string textForDocCell = this.GetTextForDocCell(attributeMapping, cellBaseFieldInfo, 0, -1, true, false);
          if (string.IsNullOrEmpty(textForDocCell) || textForDocCell != attributeValue)
            cell.RemoveAttribute("FullDesignation", false, false);
        }
      }
    }
    finally
    {
      this.avsDocument.Unlock_DocCell_TextChanged();
    }
  }

  private int? ReadSkipBeforeValueFromDocRowAttribute(TableData docRow)
  {
    int? nullable = new int?();
    string attributeValue = docRow.GetAttributeValue(AVSDocument.DocAttr_SkipLinesBefore, true);
    int result;
    if (!string.IsNullOrEmpty(attributeValue) && int.TryParse(attributeValue, out result))
      nullable = new int?(result);
    else if (!docRow.IsOverridden2(OverrideFlags2.SkipBeforeForPlugin) && !this.IsNoteRow)
      nullable = new int?((int) docRow.SkipCellsBefore);
    return nullable;
  }

  private int? ReadSkipAfterValueFromDocRowAttribute(TableData docRow)
  {
    int? nullable = new int?();
    string attributeValue = docRow.GetAttributeValue(AVSDocument.DocAttr_SkipLinesAfter, true);
    int result;
    if (!string.IsNullOrEmpty(attributeValue) && int.TryParse(attributeValue, out result))
      nullable = new int?(result);
    else if (!docRow.IsOverridden2(OverrideFlags2.SkipAfterForPlugin) && !this.IsNoteRow)
      nullable = new int?((int) docRow.SkipCellsAfter);
    return nullable;
  }

  /// <summary>Совместная позиция записи записывающаяся в документе, теперь не используется осталась для поддержки старых документов</summary>
  [Browsable(false)]
  public string CommonPositionDocument
  {
    get
    {
      if (this.commonPositionDocument == null && this.DocNode != null && this.DocNode.ContainsAttribute(AVSRow.RowAttr_CommonPositions))
        this.commonPositionDocument = this.DocNode.GetAttributeValue(AVSRow.RowAttr_CommonPositions, false);
      return this.commonPositionDocument;
    }
    set
    {
      this.commonPositionDocument = value;
      this.SetCommonPositionToDocNodes(this.commonPositionDocument);
    }
  }

  /// <summary>Совместная позиция записи</summary>
  [DisplayName("Условная позиция")]
  public string CommonPosition
  {
    get
    {
      string commonPosition = (string) null;
      object fieldValue = this.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_CommonPosition), 0, -1, true, false);
      if (fieldValue != null)
        commonPosition = Convert.ToString(fieldValue);
      return commonPosition;
    }
    set
    {
      this.SetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_CommonPosition), -1, -1, (object) value, true, false, false, false, false, false);
      string commonPosition = this.CommonPosition;
    }
  }

  /// <summary>Установить общие позиции в узел</summary>
  /// <param name="commonPosition">Значение</param>
  private void SetCommonPositionToDocNodes(string commonPosition)
  {
    this.SetAttributeValuesToDocNodes(AVSRow.RowAttr_CommonPositions, commonPosition);
  }

  private void SetAttributeValuesToDocNodes(
    string docAtributeName,
    string value,
    bool setNeedUpdateLayoutFlag = false,
    bool updateDocument = false)
  {
    List<TableData> collection = this.DocNodes;
    if (collection.IsNullOrEmpty<TableData>() && this.DocNode != null)
      collection = new List<TableData>() { this.DocNode };
    foreach (DocumentTreeNode node in collection)
      AVSRow.SetAttributeValueToDocNode(docAtributeName, value, node, setNeedUpdateLayoutFlag, updateDocument);
  }

  private static void SetAttributeValueToDocNode(
    string docAtributeName,
    string value,
    DocumentTreeNode node,
    bool setNeedUpdateLayoutFlag,
    bool updateDocument = false)
  {
    bool flag = !setNeedUpdateLayoutFlag & updateDocument;
    if (!string.IsNullOrEmpty(value))
      node.SetAttributeValue(docAtributeName, value, false, flag, flag);
    else
      node.RemoveAttribute(docAtributeName, flag, flag);
    if (!setNeedUpdateLayoutFlag)
      return;
    node.SetNeedUpdateLayoutFlag(true, true, updateDocument, updateDocument);
  }

  /// <summary>Получение списка строк с одинаковыми условными позициями</summary>
  /// <returns>Список строк</returns>
  public List<AVSRow> GetCommonPositionRows()
  {
    string commonPosition = this.CommonPosition;
    if (string.IsNullOrEmpty(commonPosition))
      return (List<AVSRow>) null;
    List<AVSRow> allRows = this.avsDocument.GetAllRows(false, true);
    List<AVSRow> commonPositionRows = new List<AVSRow>();
    foreach (AVSRow avsRow in allRows)
    {
      if (avsRow.CommonPosition == commonPosition)
        commonPositionRows.Add(avsRow);
    }
    return commonPositionRows;
  }

  public void SetCommonPositions(
    int productIndex,
    List<RelationAttributeValuesCache> relationList,
    object value)
  {
    if (this.DocNode != null && this.DocNode.ContainsAttribute("AVS.Parent.CommonPositions"))
    {
      string attributeValue = this.DocNode.GetAttributeValue("AVS.Parent.CommonPositions", true);
      if (attributeValue != "")
        value = this.avsDocument.GetAvsDocRow(new Guid(attributeValue))?.GetFieldValue(this.Field_Position, -1, productIndex, (List<RelationAttributeValuesCache>) null, false, false);
    }
    this.GetFieldValue(this.Field_Position, -1, productIndex, relationList, false, true);
    this.SetCommonPositions();
    this.SetCADPositions(value);
  }

  /// <summary>Установка совместных позиций</summary>
  public void SetCommonPositions()
  {
    AvsRowAttributeInfo fieldPosition = this.Field_Position;
    object fieldValue = this.GetFieldValue(fieldPosition, -1, -1, this.relations, true, false);
    bool isGridViewMode = this.avsDocument.IsGridViewMode;
    List<AVSRow> commonPositionRows = this.GetCommonPositionRows();
    if (commonPositionRows == null)
      return;
    foreach (AVSRow avsRow in commonPositionRows)
    {
      if (!object.Equals(avsRow.GetFieldValue(fieldPosition, -1, -1, (List<RelationAttributeValuesCache>) null, true, false), fieldValue) && avsRow != this)
        avsRow.SetFieldValueForAllRelations(fieldPosition, fieldValue, true, false, true, isGridViewMode, true, false);
    }
  }

  /// <summary>Загрузка Guidов строк спецификации созданных по CAD модели</summary>
  /// <returns></returns>
  public List<Guid> LoadCADGuids()
  {
    List<AVSRow> allRows = this.avsDocument.GetAllRows(true, true);
    List<Guid> guidList = new List<Guid>();
    if (this.relations == null || this.relations.Count == 0 || this.RelId == -1L || Convert.ToInt64(this.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_BasedOnCADModel), -1, -1, this.relations, true, false)) != 1L)
      return guidList;
    object fieldValue1 = this.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_OccurenceKey), -1, -1, this.relations, true, false);
    foreach (AVSRow avsRow in allRows)
    {
      if (avsRow.RelGuid != this.RelGuid)
      {
        object fieldValue2 = avsRow.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_OccurenceKey), -1, -1, (List<RelationAttributeValuesCache>) null, true, false);
        if (Convert.ToInt64(avsRow.GetFieldValue(new AvsRowAttributeInfo(true, AvsIDCache.Attr_BasedOnCADModel), -1, -1, (List<RelationAttributeValuesCache>) null, true, false)) == 1L && fieldValue1 != null && fieldValue2 != null && !(fieldValue1 is DBNull) && fieldValue1.Equals(fieldValue2))
          guidList.Add(avsRow.RelGuid);
      }
    }
    return guidList;
  }

  /// <summary>Скрыть запись , чтобы не отображалась в спецификации</summary>
  public void Hide()
  {
    if (this.avsDocument.IsSpecification)
      this.SetFieldValue(this.Attr_HideInSpecification, -1, -1, (object) true, true, false, true, true, true, false);
    if (this.avsDocument.IsElementList)
      this.SetFieldValue(this.Attr_IncludeInElementList, -1, -1, (object) false, true, false, true, true, true, false);
    this.UpdateDocRow();
  }

  /// <summary>Показать скрытую запись</summary>
  public void UnHide()
  {
    if (this.avsDocument.IsSpecification)
      this.SetFieldValue(this.Attr_HideInSpecification, -1, -1, (object) false, true, false, true, true, false, false);
    if (this.avsDocument.IsElementList)
      this.SetFieldValue(this.Attr_IncludeInElementList, -1, -1, (object) true, true, false, true, true, false, false);
    this.UpdateDocRow();
  }

  /// <summary>Установка позиций строк созданных по CAD модели</summary>
  /// <param name="value"></param>
  public void SetCADPositions(object value)
  {
    bool isGridViewMode = this.avsDocument.IsGridViewMode;
    AvsRowAttributeInfo fieldPosition = this.Field_Position;
    List<Guid> guidList = this.LoadCADGuids();
    if (guidList.Count <= 0)
      return;
    foreach (Guid relationGuid in guidList)
    {
      AVSRow avsDocRow = this.avsDocument.GetAvsDocRow(relationGuid);
      if (avsDocRow != null && avsDocRow != this)
        avsDocRow.SetFieldValueForAllRelations(fieldPosition, value, true, false, true, isGridViewMode, true, false);
    }
  }

  /// <summary>Получить идентификатор спецификации</summary>
  /// <returns></returns>
  public long GetSpecificationID()
  {
    if (this.avsDocument != null)
      return this.avsDocument.DocumentID;
    long specificationId = -1;
    TableData docNode = this.DocNode;
    if (docNode != null)
    {
      ImDocumentData ownerDocument = docNode.OwnerDocument;
      if (ownerDocument != null && ownerDocument.Reference is ReferenceToDBObject reference)
        specificationId = reference.DBObjectID;
    }
    return specificationId;
  }

  /// <summary>Глобальный идентификатор объекта</summary>
  [Browsable(false)]
  public virtual Guid ObjGuid
  {
    [DebuggerStepThrough] get => this.rowID != null ? this.rowID.ObjectGuid : Guid.Empty;
  }

  /// <summary>Идентификатор объекта</summary>
  [Browsable(false)]
  public virtual long Object_F_ID
  {
    [DebuggerStepThrough] get
    {
      return this.objectAttributesCache != null ? this.objectAttributesCache.F_ID : -1L;
    }
    set
    {
      this.SetFieldValue(new AvsRowAttributeInfo(false, -3), -1, -1, (List<RelationAttributeValuesCache>) null, (object) value, false, false, false, false, true, false);
    }
  }

  /// <summary>Идентификатор версии объекта</summary>
  [Browsable(false)]
  public virtual long ObjectId
  {
    [DebuggerStepThrough] get => this.rowID != null ? this.rowID.ObjectID : -1L;
    set
    {
      this.SetFieldValue(new AvsRowAttributeInfo(false, -2), -1, -1, (List<RelationAttributeValuesCache>) null, (object) value, false, false, false, false, true, false);
    }
  }

  /// <summary>Запись является примечанием</summary>
  [Browsable(false)]
  public bool IsNoteRow
  {
    [DebuggerStepThrough] get => this.isNoteRow;
    set => this.isNoteRow = value;
  }

  [Browsable(false)]
  public bool IsHeaderNoteRow
  {
    get => this.IsNoteRow && this.DocNode != null && this.DocNode.TableCellType == CellType.Header;
  }

  [Browsable(false)]
  public bool IsDynamicGroupHeaderRow
  {
    get => this.IsNoteRow && this.DocNode != null && this.DocNode.IsDynamicGroupHeader;
  }

  [Browsable(false)]
  public bool HasDynamicGroupHeader => this.DocNode != null && this.DocNode.HasGroupHeaderText;

  [Browsable(false)]
  public string GroupHeaderText
  {
    get => this.DocNode == null ? (string) null : this.DocNode.GroupHeaderText;
  }

  public List<AVSRow> GetDynamicGroupRows()
  {
    List<AVSRow> dynamicGroupRows = new List<AVSRow>();
    if (this.IsDynamicGroupHeaderRow && this.Section != null)
    {
      List<AVSRow> list = this.Section.GetRows(false, false).ToList<AVSRow>();
      List<AVSRow> collection1 = new List<AVSRow>();
      if (this.Index > 0)
      {
        for (int index = this.Index - 1; index >= 0; --index)
        {
          AVSRow avsRow = list[index];
          if (avsRow.GroupHeaderText == this.GroupHeaderText)
          {
            if (avsRow.IsDynamicGroupHeaderRow)
              return dynamicGroupRows;
            collection1.Add(avsRow);
          }
          else
            break;
        }
      }
      List<AVSRow> collection2 = new List<AVSRow>();
      for (int index = this.Index + 1; index < list.Count; ++index)
      {
        AVSRow avsRow = list[index];
        if (avsRow.GroupHeaderText == this.GroupHeaderText)
        {
          if (!avsRow.IsDynamicGroupHeaderRow)
            collection2.Add(avsRow);
        }
        else
          break;
      }
      dynamicGroupRows.AddRange((IEnumerable<AVSRow>) collection1);
      dynamicGroupRows.AddRange((IEnumerable<AVSRow>) collection2);
    }
    return dynamicGroupRows;
  }

  [Browsable(false)]
  public bool IsFunctionalGroupHeaderRow
  {
    get
    {
      return this.IsNoteRow && this.HasDocNodes && this.DocNode.GetAttributeValue(AVSRow.DocAttr_FunctionalGroupHeader, true) == "1";
    }
    set
    {
      if (this.DocNode == null)
        return;
      this.DocNode.SetAttributeValue(AVSRow.DocAttr_FunctionalGroupHeader, "1", false, false, false);
    }
  }

  /// <summary>Это запись, которая не должна отображаться в документе</summary>
  [Browsable(false)]
  public bool IsHiddenRow
  {
    get => this.HasRelation && this.avsDocument.IsHiddenRowRelation(this.Relations[0]);
  }

  /// <summary>Нужно ли вызывать обновление строки документа, чтобы создать или скрыть строку</summary>
  [Browsable(false)]
  internal bool NeedUpdateDocRow => this.IsHiddenRow ? this.HasDocNodes : !this.HasDocNodes;

  /// <summary>Запись является записью об объекте без связи</summary>
  [Browsable(false)]
  public bool HasRelation
  {
    [DebuggerStepThrough] get => this.relations != null && this.relations.Count > 0;
  }

  /// <summary>Запись имеет скрытые связи</summary>
  [Browsable(false)]
  public bool HasHiddenRelation
  {
    [DebuggerStepThrough] get => this.hiddenRelations != null && this.hiddenRelations.Count > 0;
  }

  /// <summary>Запись является записью об объекте без связи</summary>
  [Browsable(false)]
  public bool HasAnyRelations
  {
    [DebuggerStepThrough] get => this.HasRelation || this.HasHiddenRelation;
  }

  /// <summary>Запись имеет скрытые связи для суммирования поз. обозначения</summary>
  [Browsable(false)]
  public bool HasHiddenRelationForPosDesignationSumm
  {
    [DebuggerStepThrough] get
    {
      if (this.HasHiddenRelation)
      {
        foreach (RelationAttributeValuesCache hiddenRelation in this.HiddenRelations)
        {
          if (this.CheckRelation_IsHiddenForPosDesignationSumm(hiddenRelation))
            return true;
        }
      }
      return false;
    }
  }

  /// <summary>Запись является записью об объекте без связи</summary>
  [Browsable(false)]
  public bool HasObject
  {
    [DebuggerStepThrough] get
    {
      return this.ObjectId != -1L && this.ObjectId != 0L && this.ObjectAttributesCache != null;
    }
  }

  /// <summary>Тип объекта</summary>
  [Browsable(false)]
  public virtual int ObjType
  {
    [DebuggerStepThrough] get => this.rowID != null ? this.rowID.ObjectType : -1;
  }

  /// <summary>Заголовок объекта</summary>
  [Browsable(false)]
  public string ObjCaption
  {
    [DebuggerStepThrough] get
    {
      string objCaption = (string) null;
      if (this.rowID != null)
        objCaption = this.rowID.ObjectCaption;
      if (objCaption == null || objCaption == "")
        objCaption = this.Designation == "" || this.Designation == null ? this.Name : (this.Name == "" || this.Name == null ? this.Designation : $"{this.Designation} ({this.Name})");
      return objCaption;
    }
  }

  /// <summary>Глобальный идентификатор первой связи</summary>
  [Browsable(false)]
  public virtual Guid RelGuid
  {
    [DebuggerStepThrough] get => this.rowID != null ? this.rowID.RelationGuid : Guid.Empty;
  }

  /// <summary>Идентификатор связи</summary>
  [Browsable(false)]
  public virtual long RelId
  {
    [DebuggerStepThrough] get => this.rowID != null ? this.rowID.RelationID : -1L;
  }

  /// <summary>Тип связи</summary>
  [Browsable(false)]
  public virtual int RelType
  {
    [DebuggerStepThrough] get
    {
      if (this.rowID != null && this.rowID.RelationType != -1)
        return this.rowID.RelationType;
      if (this.avsDocument != null)
        return this.avsDocument.GetRelationType(this, (AVSDocumentContext) null, this.ObjType, -1);
      return MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_Document) ? AvsIDCache.Relation_Document : AvsIDCache.Relation_Project;
    }
    set
    {
      if (this.RelType == value)
        return;
      if (this.rowID != null)
      {
        this.rowID.SetRelationType(value);
      }
      else
      {
        this.rowID = new DBRelationInfo();
        this.rowID.SetRelationType(value);
      }
      if (this.DocNode == null)
        return;
      if ((this.relations == null || this.relations.Count == 0) && value != -1)
        this.DocNode.SetAttributeValue(AVSRow.RowAttr_RelationType, MetaDataHelper.GetRelationTypeGuid(value).ToString(), false, false, false);
      else
        this.DocNode.RemoveAttribute(AVSRow.RowAttr_RelationType, false, false);
    }
  }

  /// <summary>Группа в составе которой отображается запись</summary>
  [Browsable(false)]
  public AVSRowGroup Group
  {
    get => this.group;
    set => this.group = value;
  }

  [Browsable(false)]
  private bool NeedGroup
  {
    get
    {
      return this.Relations != null && this.Relations.Count > 0 && this.Relations[0].RelationType == AvsIDCache.Relation_AddComplect;
    }
  }

  public void GetGroup(SpecificationSection section)
  {
    if (this.RelType == AvsIDCache.Relation_AddComplect)
    {
      this.Group = (AVSRowGroup) section.GetGroup<AVSAdditionalComplectRowGroup>();
    }
    else
    {
      if (this.Relations == null || this.Relations.Count <= 0 || this.Relations[0].RelationType != AvsIDCache.Relation_AddComplect)
        return;
      this.Group = (AVSRowGroup) section.GetGroup<AVSAdditionalComplectRowGroup>();
    }
  }

  /// <summary>Группа исполнений в составе которой отображать запись</summary>
  [Browsable(false)]
  public int ProductGroup
  {
    get => this.productGroup;
    set => this.productGroup = value;
  }

  /// <summary>Пропуск строк перед записью </summary>
  [DefaultValue(null)]
  [Description("Пропуск строк перед записью")]
  [DisplayName("Перед записью")]
  [Category("Пропуск строк")]
  [RefreshProperties(RefreshProperties.All)]
  public int? SkipLinesBefore
  {
    get => this._skipLinesBefore;
    set
    {
      int? skipLinesBefore = this._skipLinesBefore;
      int? nullable1 = value;
      if (skipLinesBefore.GetValueOrDefault() == nullable1.GetValueOrDefault() & skipLinesBefore.HasValue == nullable1.HasValue)
        return;
      if (value.HasValue)
      {
        int? nullable2 = value;
        int num = 0;
        if (!(nullable2.GetValueOrDefault() >= num & nullable2.HasValue))
          return;
      }
      this._skipLinesBefore = value;
      this.UpdateSkipLinesBefore(this.avsDocument != null ? this.avsDocument.GetSkipLinesSchema() : (SkipLinesSchema) null, (PageData) null, false, false, false);
      this.avsDocument.UpdateSkipLines(true, true);
    }
  }

  [Browsable(false)]
  internal bool SkipLinesBeforeIsOverriden => this._skipLinesBefore.HasValue;

  /// <summary>Пропуск строк после записи </summary>
  [DefaultValue(null)]
  [Description("Пропуск строк после записи")]
  [DisplayName("После записи")]
  [Category("Пропуск строк")]
  [RefreshProperties(RefreshProperties.All)]
  public int? SkipLinesAfter
  {
    [DebuggerStepThrough] get => this._skipLinesAfter;
    set
    {
      int? skipLinesAfter = this._skipLinesAfter;
      int? nullable1 = value;
      if (skipLinesAfter.GetValueOrDefault() == nullable1.GetValueOrDefault() & skipLinesAfter.HasValue == nullable1.HasValue)
        return;
      if (value.HasValue)
      {
        int? nullable2 = value;
        int num = 0;
        if (!(nullable2.GetValueOrDefault() >= num & nullable2.HasValue))
          return;
      }
      this._skipLinesAfter = value;
      this.UpdateSkipLinesAfter(this.avsDocument != null ? this.avsDocument.GetSkipLinesSchema() : (SkipLinesSchema) null, (PageData) null, false, false);
      this.avsDocument.UpdateSkipLines(true, true);
    }
  }

  [Browsable(false)]
  internal bool SkipLinesAfterIsOverriden => this._skipLinesAfter.HasValue;

  /// <summary>Шаг позиции перед записью </summary>
  [DefaultValue(null)]
  [Description("Шаг позиции перед записью")]
  [DisplayName("Перед записью")]
  [Category("Шаг позиции")]
  public int? PositionStepBefore
  {
    [DebuggerStepThrough] get
    {
      int result;
      if (!this._positionStepBefore.HasValue && this.DocNode != null && this.DocNode.ContainsAttribute(AVSRow.RowAttr_PositionStepBefore) && int.TryParse(this.DocNode.GetAttributeValue(AVSRow.RowAttr_PositionStepBefore, true), out result))
        this._positionStepBefore = new int?(result);
      return this._positionStepBefore;
    }
    set
    {
      if (value.HasValue && value.Value <= 0)
        return;
      this._positionStepBefore = value;
      foreach (DocumentTreeNode docNode in this.DocNodes)
      {
        if (!value.HasValue)
          docNode.RemoveAttribute(AVSRow.RowAttr_PositionStepBefore, true, true);
        else
          docNode.SetAttributeValue(AVSRow.RowAttr_PositionStepBefore, this._positionStepBefore.ToString());
      }
    }
  }

  public void UpdatePositionsStepFromDocNode(TableData docNode)
  {
    int result1;
    if (docNode != null && docNode.ContainsAttribute(AVSRow.RowAttr_PositionStepAfter) && int.TryParse(docNode.GetAttributeValue(AVSRow.RowAttr_PositionStepAfter, true), out result1))
      this._positionStepAfter = new int?(result1);
    int result2;
    if (docNode == null || !docNode.ContainsAttribute(AVSRow.RowAttr_PositionStepBefore) || !int.TryParse(docNode.GetAttributeValue(AVSRow.RowAttr_PositionStepBefore, true), out result2))
      return;
    this._positionStepBefore = new int?(result2);
  }

  /// <summary>Шаг позиции после записи </summary>
  [DefaultValue(null)]
  [Description("Шаг позиции после записи")]
  [DisplayName("После записи")]
  [Category("Шаг позиции")]
  public int? PositionStepAfter
  {
    [DebuggerStepThrough] get
    {
      int result;
      if (!this._positionStepAfter.HasValue && this.DocNode != null && this.DocNode.ContainsAttribute(AVSRow.RowAttr_PositionStepAfter) && int.TryParse(this.DocNode.GetAttributeValue(AVSRow.RowAttr_PositionStepAfter, true), out result))
        this._positionStepAfter = new int?(result);
      return this._positionStepAfter;
    }
    set
    {
      if (value.HasValue && value.Value < 0)
        return;
      this._positionStepAfter = value;
      foreach (DocumentTreeNode docNode in this.DocNodes)
      {
        if (!value.HasValue)
          docNode.RemoveAttribute(AVSRow.RowAttr_PositionStepAfter, true, true);
        else
          docNode.SetAttributeValue(AVSRow.RowAttr_PositionStepAfter, this._positionStepAfter.ToString());
      }
    }
  }

  /// <summary>Начинать ли запись с новой страницы </summary>
  [DefaultValue(false)]
  [Description("Начинать ли запись с новой страницы")]
  [DisplayName("C новой страницы")]
  [Category("Страницы")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public bool? FromNewPage
  {
    [DebuggerStepThrough] get
    {
      return this.docNode != null ? new bool?(this.docNode.FromNewPage) : new bool?();
    }
    set
    {
      if (!this.HasDocNodes)
        return;
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (value.HasValue)
        {
          this.docNodes[index].SetFromNewPage(value.Value, false, true);
        }
        else
        {
          this.docNodes[index].overrideFlags |= OverrideFlags.FromNewPage;
          this.docNodes[index].ApplyTemplateProperties(false, false);
        }
      }
      this.avsDocument.Document.UpdateLayout(this.DocNode.Page.Index, true, false);
      this.avsDocument.UpdateProductHeadersOnPages(true, true);
    }
  }

  /// <summary>Игнорировать пропуски в начале страницы</summary>
  [DefaultValue(null)]
  [Description("Игнорировать пропуски в начале страницы")]
  [DisplayName("Игнорировать пропуски строк перед записью в начале страницы")]
  [Category("Пропуск строк")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public bool? NonSkipBeforeAtStartPage
  {
    [DebuggerStepThrough] get
    {
      return this.docNode != null && (this.docNode.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage) || this.docNode.Template != null && this.docNode.Template.IsOverridden3(OverrideFlags3.NonSkipBeforeAtStartPage)) ? new bool?(this.docNode.NonSkipBeforeAtStartPage) : new bool?();
    }
    set
    {
      if (!this.HasDocNodes)
        return;
      for (int index = 0; index < this.docNodes.Count; ++index)
      {
        if (value.HasValue)
        {
          bool beforeAtStartPage = this.docNodes[index].NonSkipBeforeAtStartPage;
          if (this.docNodes[index].NonSkipBeforeAtStartPage != beforeAtStartPage)
          {
            this.docNodes[index].SetNonSkipBeforeAtStartPage(value.Value, false, false, false);
          }
          else
          {
            this.docNodes[index].overrideFlags3 |= OverrideFlags3.NonSkipBeforeAtStartPage;
            this.docNodes[index].SetNonSkipBeforeAtStartPage(value.Value, false, false, false);
            this.docNodes[index].SetNeedUpdateLayoutFlag(true, true, false, false);
          }
        }
        else
        {
          this.docNodes[index].overrideFlags3 &= ~OverrideFlags3.NonSkipBeforeAtStartPage;
          this.docNodes[index].SetNeedUpdateLayoutFlag(true, true, false, false);
        }
      }
      this.avsDocument.Document.UpdateLayout(true);
    }
  }

  /// <summary>Количество страниц которые требуется пропустить после данной записи </summary>
  [DefaultValue(0)]
  [Description("Количество страниц которые требуется пропустить после данной записи")]
  [DisplayName("Пропустить страниц")]
  [Category("Страницы")]
  [Browsable(false)]
  public int SkipPagesAfter
  {
    [DebuggerStepThrough] get => this._skipPagesAfter;
    set
    {
      if (value < 0)
        return;
      this._skipPagesAfter = value;
    }
  }

  /// <summary>Запись необходимо обновить в методе UpdateSpecificationStructure</summary>
  [Browsable(false)]
  public bool NeedUpdateStructure
  {
    [DebuggerStepThrough] get => this.needUpdateStructure;
    set => this.needUpdateStructure = value;
  }

  /// <summary>Привязана ли текущая запись к следующей записи</summary>
  [DefaultValue(false)]
  [Description("Привязать к следующей записи")]
  [DisplayName("К следующей записи")]
  [Category("Привязка")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public bool IsLinkedToNextRow
  {
    [DebuggerStepThrough] get
    {
      return this.SortBeforeRow != null && this.Section != null && this.Index >= 0 && this.Index < this.Section.Rows.Count - 1 && this.SortBeforeRow == this.Section.Rows[this.Index + 1];
    }
    set
    {
      if (value == this.IsLinkedToNextRow || this.Section == null || this.Index < 0 || this.Index >= this.Section.Rows.Count - 1)
        return;
      this.SortBeforeRow = value ? this.Section.Rows[this.Index + 1] : (AVSRow) null;
    }
  }

  /// <summary>Привязана ли текущая запись к предыдущей записи</summary>
  [DefaultValue(false)]
  [Description("Привязать к предыдущей записи")]
  [DisplayName("К предыдущей записи")]
  [Category("Привязка")]
  [TypeConverter(typeof (CustomBooleanNullableConverter))]
  public bool IsLinkedToPrevRow
  {
    [DebuggerStepThrough] get
    {
      return this.SortAfterRow != null && this.Section != null && this.Section.Rows.Count > 0 && this.Index > 0 && this.SortAfterRow == this.Section.Rows[this.Index - 1];
    }
    set
    {
      if (value == this.IsLinkedToPrevRow || this.Section == null || this.Section.Rows.Count <= 0 || this.Index <= 0)
        return;
      this.SortAfterRow = value ? this.Section.Rows[this.Index - 1] : (AVSRow) null;
    }
  }

  AttributeCollection ICustomTypeDescriptor.GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this, true);
  }

  string ICustomTypeDescriptor.GetClassName() => TypeDescriptor.GetClassName((object) this, true);

  string ICustomTypeDescriptor.GetComponentName()
  {
    return TypeDescriptor.GetComponentName((object) this, true);
  }

  TypeConverter ICustomTypeDescriptor.GetConverter()
  {
    return TypeDescriptor.GetConverter((object) this, true);
  }

  EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this, true);
  }

  PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  object ICustomTypeDescriptor.GetEditor(System.Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this, editorBaseType, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this, true);
  }

  EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this, attributes, true);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
  {
    AttributeCollection attributes1 = ((ICustomTypeDescriptor) this).GetAttributes();
    if (attributes1 == null || attributes1.Count <= 0)
      return this.GetProperties(new Attribute[0]);
    Attribute[] attributes2 = new Attribute[attributes1.Count];
    attributes1.CopyTo((Array) attributes2, 0);
    return this.GetProperties(attributes2);
  }

  PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
  {
    return this.GetProperties(attributes);
  }

  /// <summary>Получить дескрипторы для свойств</summary>
  /// <param name="attributes">Атрибуты свойств</param>
  /// <returns>Коллекция дескрипторов свойств</returns>
  protected virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    PropertyDescriptorCollection properties1 = TypeDescriptor.GetProperties((object) this, attributes, true);
    HybridDictionary properties2 = new HybridDictionary(200);
    foreach (PropertyDescriptor PropDesc in properties1)
    {
      if (!(PropDesc is CustomPropertyDescriptor propertyDescriptor))
      {
        propertyDescriptor = new CustomPropertyDescriptor(PropDesc);
        if (propertyDescriptor.Name == "SkipLinesAfterVisual")
        {
          propertyDescriptor.SetName(propertyDescriptor.DisplayName);
          propertyDescriptor.SerializeValue = new bool?(this.SkipLinesAfter.HasValue);
        }
        if (propertyDescriptor.Name == "SkipLinesBeforeVisual")
        {
          propertyDescriptor.SetName(propertyDescriptor.DisplayName);
          propertyDescriptor.SerializeValue = new bool?(this.SkipLinesBefore.HasValue);
        }
        if (propertyDescriptor.Name == "SkipLinesAfter")
          propertyDescriptor.SetName(propertyDescriptor.DisplayName);
        if (propertyDescriptor.Name == "SkipLinesBefore")
          propertyDescriptor.SetName(propertyDescriptor.DisplayName);
        if (this.avsDocument.ReadOnly)
          propertyDescriptor.SetIsReadOnly(true);
      }
      properties2.Add((object) propertyDescriptor.Name, (object) propertyDescriptor);
    }
    if (ImDocumentData.ShowDebugInfo)
    {
      foreach (DocRowAttributePropertyDescriptor additionalProperty in this.GetDocRowAdditionalProperties())
      {
        if (!properties2.Contains((object) additionalProperty.Name))
          properties2.Add((object) additionalProperty.Name, (object) additionalProperty);
      }
    }
    this.FilterProperties((IDictionary) properties2, attributes);
    PropertyDescriptorCollection properties3 = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    foreach (DictionaryEntry dictionaryEntry in properties2)
      properties3.Add((PropertyDescriptor) dictionaryEntry.Value);
    return properties3;
  }

  private List<DocRowAttributePropertyDescriptor> GetDocRowAdditionalProperties()
  {
    List<DocRowAttributePropertyDescriptor> additionalProperties = new List<DocRowAttributePropertyDescriptor>();
    if (this.DocNode != null)
    {
      foreach (string attributeName in this.DocNode.GetAttributeNames(false))
        additionalProperties.Add(new DocRowAttributePropertyDescriptor(attributeName));
    }
    return additionalProperties;
  }

  /// <summary>Удалить свойство из списка</summary>
  /// <param name="properties">Список свойств</param>
  /// <param name="propertyName">Имя свойства</param>
  protected void RemoveProperty(IDictionary properties, string propertyName)
  {
    properties.Remove((object) propertyName);
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected virtual void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    if (this.IsNoteRow)
    {
      this.RemoveProperty(properties, "PositionStepAfter");
      this.RemoveProperty(properties, "PositionStepBefore");
    }
    this.RemoveProperty(properties, Chapter.DocNodeType_AttributeName);
    this.RemoveProperty(properties, AVSRow.RowAttr_RelationType);
    this.RemoveProperty(properties, AVSDocument.DocAttr_SkipLinesAfter);
    this.RemoveProperty(properties, AVSDocument.DocAttr_SkipLinesBefore);
    this.RemoveProperty(properties, AVSRow.RowAttr_PositionStepAfter);
    this.RemoveProperty(properties, AVSRow.RowAttr_PositionStepBefore);
    if (!this.avsDocument.IsSpecification)
      this.RemoveProperty(properties, "TextLinkToMainDocument");
    if (!this.IsBaseComponentForPodbor(0, this.Relations))
      this.RemoveProperty(properties, "LimitAndNominalValueMode");
    if (ImDocumentData.ShowDebugInfo)
      return;
    if (this.IsDynamicGroupHeaderRow)
    {
      properties.Clear();
    }
    else
    {
      this.RemoveProperty(properties, "HiddenRelations");
      this.RemoveProperty(properties, "Relations");
      this.RemoveProperty(properties, "AdditionalDocRowAttributes");
    }
    this.RemoveProperty(properties, "Caption");
  }

  object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => (object) this;

  public List<IVirtualTreeItem> GetTreeChildren()
  {
    if (!this.IsDynamicGroupHeaderRow)
      return (List<IVirtualTreeItem>) null;
    List<IVirtualTreeItem> treeChildren = new List<IVirtualTreeItem>();
    foreach (AVSRow dynamicGroupRow in this.GetDynamicGroupRows())
    {
      treeChildren.Add((IVirtualTreeItem) dynamicGroupRow);
      ((IVirtualTreeItem) dynamicGroupRow).ParentItem = (IVirtualTreeItem) this;
    }
    return treeChildren;
  }

  IVirtualTreeItem IVirtualTreeItem.ParentItem
  {
    get
    {
      if (this.parentTreeItem != null)
        return this.parentTreeItem;
      return this.Section != null && this.Section.UseParentDocNode ? (IVirtualTreeItem) this.Section.Parent : (IVirtualTreeItem) this.Section;
    }
    set => this.parentTreeItem = value;
  }

  public void GetRowData(RowData data)
  {
    data.EvenStyle.BackColor = Color.White;
    data.OddStyle.BackColor = Color.White;
  }

  public bool CanTreeShow() => true;

  public void GetCellData(AVSColumn column, CellData data)
  {
    AvsRowAttributeInfo attrInfo = (AvsRowAttributeInfo) null;
    ColumnTag tag = column.Tag;
    if (this.IsDynamicGroupHeaderRow)
      data.Value = (object) this.GroupHeaderText;
    else if (column.Name != "AVS.Status" && tag != null)
    {
      attrInfo = tag.SpecRowAttributeInfo;
      int productIndex = -1;
      if ((this.IsFormB || this.avsDocument.AvsDocumentForm == AVSDocumentForm.V) && AVSRow.IsCountField(attrInfo) && tag.ProductIndex < this.avsDocument.productsInfo.Count)
        productIndex = tag.ProductIndex;
      try
      {
        object text = (object) "";
        if (this.IsNoteRow || attrInfo.IsVirtualAttribute)
        {
          text = (object) this.GetFieldStringValue(attrInfo, -1, productIndex, (List<RelationAttributeValuesCache>) null, false);
        }
        else
        {
          int relationIndex = !this.IsFormB || !AVSRow.IsCountField(attrInfo) ? 0 : (tag.ProductIndex >= this.avsDocument.productsInfo.Count ? -1 : this.GetRelationIndexForProduct(this.avsDocument.productsInfo[tag.ProductIndex].Id, this.relations));
          if (this.IsFormB && attrInfo.IsRelationAttribute && !AVSRow.IsCountField(attrInfo) && this.DocRowFields.Find((Predicate<AvsRowAttributeInfo>) (x => x.AttributeId == attrInfo.AttributeId && x.IsRelationAttribute == attrInfo.IsRelationAttribute)) == null)
          {
            string str = (string) null;
            for (int index = 0; index < this.avsDocument.productsInfo.Count; ++index)
            {
              int relationIndexForProduct = this.GetRelationIndexForProduct(this.avsDocument.productsInfo[index].Id, this.relations);
              if (relationIndexForProduct != -1)
              {
                string fieldStringValue = this.GetFieldStringValue(attrInfo, relationIndexForProduct, productIndex, (List<RelationAttributeValuesCache>) null, false);
                if (str != fieldStringValue)
                  str = str != null ? "см. по исполнениям" : fieldStringValue;
              }
            }
            text = (object) str;
          }
          else if (attrInfo.AttributeId == this.Attr_Section.AttributeId)
            text = this.DocNode == null || this.avsDocument.GetSection((DocumentTreeNode) this.DocNode) == null ? (object) string.Empty : (object) this.avsDocument.GetSection((DocumentTreeNode) this.DocNode).Caption;
          else if (relationIndex != -1)
          {
            string str = this.GetFieldStringValue(attrInfo, relationIndex, productIndex, (List<RelationAttributeValuesCache>) null, false);
            if (str != null)
              str = str.Replace(Environment.NewLine, "");
            text = (object) str;
            if (AVSRow.IsCountField(attrInfo))
              text = (object) this.ConvertFieldValueForDocCell(attrInfo, Convert.ToString(text), false, false);
          }
        }
        if (attrInfo.FieldType == FieldTypes.ftBoolean)
        {
          if (text is string)
          {
            try
            {
              text = new CustomBooleanConverter().ConvertFromString((string) text);
            }
            catch
            {
            }
          }
        }
        data.Value = text;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
    else
    {
      if (!(column.Name == "AVS.Status"))
        return;
      if (this.IsNoteRow)
      {
        data.Value = (object) StatusIcons.None;
      }
      else
      {
        Image status = this.GetStatus();
        data.Value = (object) status;
      }
    }
  }

  bool IVirtualTreeItem.HeaderRow => this.IsDynamicGroupHeaderRow;

  /// <summary>Запись со связью типа документация на изделие</summary>
  [Browsable(false)]
  public bool IsDocRelation => this.RelType == AvsIDCache.Relation_Document;

  /// <summary>Запись со связью типа документация на изделие</summary>
  [Browsable(false)]
  public bool IsDocObject
  {
    get
    {
      return this.ObjType.IsDefinedTypeId() && MetaDataHelper.IsObjectTypeChildOf(this.ObjType, AvsIDCache.ObjType_Document);
    }
  }

  [Browsable(false)]
  public bool NewCellMappingMode => this.avsDocument != null && this.avsDocument.NewCellMappingMode;

  [Browsable(false)]
  public bool HideCountForDocuments
  {
    get => this.IsDocRelation && this.avsDocument != null && this.avsDocument.HideCountForDocuments;
  }

  internal void ReplaceObjectID(
    long f_ID,
    long objectID,
    Guid objectGuid,
    int objectType,
    string objectCaption)
  {
    this.RowID.SetDBObjectInfo(objectGuid, objectID, objectType, objectCaption);
    if (this.HasRelation || this.HasHiddenRelation)
    {
      foreach (RelationAttributeValuesCache allRelation in this.AllRelations)
        allRelation.ObjectAttributesCache.SetObjectID(f_ID, objectID, objectGuid, objectType, objectCaption);
    }
    else
    {
      if (!this.HasObject)
        return;
      this.ObjectAttributesCache.SetObjectID(f_ID, objectID, objectGuid, objectType, objectCaption);
    }
  }

  internal void SetLinkFromDraftToPart(AVSRow partFromDraft)
  {
    if (partFromDraft == null)
      throw new ArgumentNullException(nameof (partFromDraft));
    if (!this.HasDocNodes)
      return;
    this.DocNode.SetAttributeValue(AVSRow.DocAttr_ZagotovkaDlya, partFromDraft.Designation, false, false, false);
    if (this.GetFieldInt64Value(new AvsRowAttributeInfo(true, AvsIDCache.Attr_ArticleID), 0, (List<RelationAttributeValuesCache>) null, false).IsDefinedId())
      this.DocNode.RemoveAttribute(AVSRow.DocAttr_PartFromDraftGuid, false, false);
    else
      this.DocNode.SetAttributeValue(AVSRow.DocAttr_PartFromDraftGuid, partFromDraft.ObjGuid.ToString(), false, false, false);
    this.UpdateNameDocCellText(false, false);
  }

  public long CheckAdditionalChapter()
  {
    long id = -1;
    AdditionalChapter addChapter = this.Section?.GetRootChapter() as AdditionalChapter;
    if (addChapter != null)
    {
      AdditionalChapterSettings additionalChapterSettings = this.avsDocument.AVSCommonPropertiesSchema.AdditionalChapters.OfType<AdditionalChapterSettings>().FirstOrDefault<AdditionalChapterSettings>((Func<AdditionalChapterSettings, bool>) (p => p.ChapterGuid == addChapter.ChapterGuid || p.Caption.Equals(addChapter.Caption, StringComparison.CurrentCultureIgnoreCase)));
      id = additionalChapterSettings != null ? additionalChapterSettings.ChapterID : -1L;
      this.SetFieldValue(this.avsDocument.Attr_AdditionalChapter, -1, -1, id.IsDefinedId() ? (object) id : (object) null, !this.avsDocument.ReadOnly, true, true, this.avsDocument.IsGridViewMode, false, true, false);
    }
    return id;
  }

  internal delegate string GetFieldValueByCellOutputMapping(OutputMappingBase attrMapping);
}
