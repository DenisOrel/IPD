// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecificationSection
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.Document.UI;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Раздел спецификации с записями</summary>
[Serializable]
public class SpecificationSection : Chapter, IComparer, IComparer<AVSRow>
{
  public SectionSortSchema sectionSortSchema;
  private int? _skipLinesBefore;
  private int? _skipLinesAfter;
  /// <summary>Строки спецификации</summary>
  public List<AVSRow> Rows = new List<AVSRow>();
  public List<AVSRowGroup> Groups = new List<AVSRowGroup>();
  /// <summary>Допустимые типы изделий</summary>
  public int[] PartTypes;

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="sectionGuid">GUID раздела спецификации</param>
  /// <param name="sectionID">ID раздела спецификации</param>
  /// <param name="sectionType">Тип раздела спецификации</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="sortIndex">Индекс сортировки</param>
  /// <param name="partTypes">Допустимые типы объектов, для этого раздела</param>
  public SpecificationSection(
    AVSDocument avsDocument,
    Guid sectionGuid,
    long sectionID,
    int sectionType,
    string caption,
    long sortIndex,
    int[] partTypes)
    : base(avsDocument, sectionGuid, sectionID, sectionType, caption, sortIndex, false)
  {
    this.nodeLevel = Chapter.Section_TypeName;
    this.PartTypes = partTypes;
  }

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация</param>
  /// <param name="sectionInfo">Информация о разделе спецификации</param>
  public SpecificationSection(AVSDocument avsDocument, SpecificationSectionInfo sectionInfo)
    : this(avsDocument, sectionInfo.SectionGuid, sectionInfo.SectionID, sectionInfo.SectionType, sectionInfo.Caption, sectionInfo.SortIndex, sectionInfo.PartTypes)
  {
    this.UpdateCaption();
  }

  /// <summary>Конструктор</summary>
  protected SpecificationSection() => this.nodeLevel = Chapter.Section_TypeName;

  /// <summary>Обновление заголовка спецификации</summary>
  public void UpdateCaption()
  {
    this.Caption = this.GetSectionCaption();
    this.CaptionExp = this.GetSectionExportCaption();
  }

  protected override void ConnectDocNode(TableData docNode, bool isExportTable)
  {
    base.ConnectDocNode(docNode, isExportTable);
    this.UpdateDocNodeDynamicHeaderSettings(docNode, this.avsDocument.DynamicGroupHeaderSettings);
    docNode.ChildNodeAddedInFlowChain += new ChildNodeAdded_EventHandler(this.DocNode_ChildNodeAdded);
    docNode.ChildNodeRemovedInFlowChain += new ChildNodeRemoved_EventHandler(this.DocNode_ChildNodeRemoved);
  }

  protected override void DisconnectDocNode(TableData docNode, bool isExportTable)
  {
    base.DisconnectDocNode(docNode, isExportTable);
    docNode.ChildNodeAddedInFlowChain -= new ChildNodeAdded_EventHandler(this.DocNode_ChildNodeAdded);
    docNode.ChildNodeRemovedInFlowChain -= new ChildNodeRemoved_EventHandler(this.DocNode_ChildNodeRemoved);
  }

  private void DocNode_ChildNodeRemoved(object sender, ChildNode_EventArgs e)
  {
    if (e.ByShift || !(e.Child is TableData child) || !child.IsDynamicGroupHeader)
      return;
    AVSRow avsDocRow = this.avsDocument.GetAvsDocRow((DocumentTreeNode) child);
    if (avsDocRow == null)
      return;
    if (avsDocRow.DocNodes.Contains(child))
    {
      this.DisconnectDocNode(child, false);
      avsDocRow.DocNodes.Remove(child);
    }
    if (!avsDocRow.HasDocNodes)
      this.RemoveRow(avsDocRow, true, false, false, true, false);
    this.AVSDocument.UpdateSkipLines(false, false, this);
  }

  private void DocNode_ChildNodeAdded(object sender, ChildNode_EventArgs e)
  {
    if (e.ByShift || !(e.Child is TableData child) || !child.IsDynamicGroupHeader)
      return;
    AVSRow row = this.avsDocument.GetAvsDocRow((DocumentTreeNode) child);
    if (row == null)
    {
      int index = 0;
      AVSRow prevAvsRowForDocRow = this.FindPrevAvsRowForDocRow(child);
      if (prevAvsRowForDocRow != null)
      {
        index = prevAvsRowForDocRow.Index + 1;
        if (prevAvsRowForDocRow.IsDynamicGroupHeaderRow)
          row = prevAvsRowForDocRow;
      }
      if (row == null)
      {
        row = new AVSRow(this.avsDocument);
        row.IsNoteRow = true;
        this.InsertRow(index, row);
      }
      row.AddDocNode(child);
      if (row.Index + 1 < this.Rows.Count)
        row.SortBeforeRow = this.Rows[row.Index + 1];
    }
    this.AVSDocument.UpdateSkipLines(false, false, this);
  }

  private AVSRow FindPrevAvsRowForDocRow(TableData docRow)
  {
    RectangleElement rowDocNode = (RectangleElement) docRow;
    AVSRow prevAvsRowForDocRow;
    for (prevAvsRowForDocRow = (AVSRow) null; rowDocNode != null && prevAvsRowForDocRow == null; prevAvsRowForDocRow = rowDocNode != null ? this.avsDocument.GetAvsDocRow((DocumentTreeNode) rowDocNode) : (AVSRow) null)
      rowDocNode = rowDocNode.ParentCell.FindPrevDataCellInFlow(rowDocNode.Index);
    return prevAvsRowForDocRow;
  }

  /// <summary>Обработчик события при добавлении узла документа</summary>
  /// <param name="newDocNode">Новый узел документа</param>
  protected override void OnDocNodeAdded(TableData newDocNode)
  {
    TableData chapterCaptionRow = this.GetChapterCaptionRow(newDocNode);
    if (chapterCaptionRow == null)
      return;
    string attributeValue1 = chapterCaptionRow.GetAttributeValue(AVSDocument.DocAttr_SkipLinesBefore, true);
    int result;
    this._skipLinesBefore = !(attributeValue1 != "") || !int.TryParse(attributeValue1, out result) ? ((chapterCaptionRow.overrideFlags2 & OverrideFlags2.SkipBeforeForPlugin) != OverrideFlags2.None ? new int?() : new int?((int) chapterCaptionRow.SkipCellsBefore)) : new int?(result);
    string attributeValue2 = chapterCaptionRow.GetAttributeValue(AVSDocument.DocAttr_SkipLinesAfter, true);
    if (attributeValue2 != "" && int.TryParse(attributeValue2, out result))
      this._skipLinesAfter = new int?(result);
    else if ((chapterCaptionRow.overrideFlags2 & OverrideFlags2.SkipAfterForPlugin) == OverrideFlags2.None)
      this._skipLinesAfter = new int?((int) chapterCaptionRow.SkipCellsAfter);
    else
      this._skipLinesAfter = new int?();
  }

  /// <summary>Идентификатор раздела СП</summary>
  [Browsable(false)]
  public long SectionID
  {
    [DebuggerStepThrough] get => this.ChapterID;
    set => this.ChapterID = value;
  }

  public bool IsDocumentationSection
  {
    get => this.ChapterGuid == SpecificationSectionInfo.DocumentSectionGuid;
  }

  /// <summary>Идентификатор раздела СП</summary>
  [Browsable(false)]
  public SpecificationSectionInfo SectionInfo
  {
    [DebuggerStepThrough] get
    {
      if (!SpecificationSectionInfo.Cached)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session);
      }
      return SpecificationSectionInfo.FindSectionById(this.SectionID) ?? new SpecificationSectionInfo(this.ChapterGuid, this.SectionID, this.ChapterType, this.Caption, this.SortIndex, "", this.PartTypes, (long[]) null);
    }
  }

  /// <summary>Часть исполнения или общих данных владеющая разделом</summary>
  [Browsable(false)]
  public Chapter ProductChapter
  {
    [DebuggerStepThrough] get
    {
      if (this.parent == null)
        return (Chapter) null;
      Chapter parent = this.parent;
      while (parent != null && !(parent is ProductVariableDataChapter) && !parent.IsCommonDataChapter)
        parent = parent.Parent;
      return parent;
    }
  }

  /// <summary>Получить заголовок для раздела</summary>
  /// <returns></returns>
  private string GetSectionCaption()
  {
    string sectionCaption = "";
    if (this.ChapterGuid != Guid.Empty && this.avsDocument != null)
      sectionCaption = this.avsDocument.AVSCommonPropertiesSchema.GetSectionCaption(this.ChapterGuid);
    if (sectionCaption == null || sectionCaption == "")
    {
      SpecificationSectionInfo sectionById = SpecificationSectionInfo.FindSectionById(this.ChapterGuid);
      if (sectionById != null && this.avsDocument != null)
        sectionCaption = sectionById.GetDefaultCaption(this.avsDocument.AVSDocType) ?? sectionById.Caption;
    }
    return sectionCaption;
  }

  /// <summary>Получить заголовок для раздела экспортной СП</summary>
  /// <returns></returns>
  private string GetSectionExportCaption()
  {
    string sectionExportCaption = "";
    if (this.ChapterGuid != Guid.Empty && this.avsDocument != null)
      sectionExportCaption = this.avsDocument.AVSCommonPropertiesSchema.GetSectionExportCaption(this.ChapterGuid);
    if (sectionExportCaption == null || sectionExportCaption == "")
    {
      SpecificationSectionInfo sectionById = SpecificationSectionInfo.FindSectionById(this.ChapterGuid);
      if (sectionById != null && this.avsDocument != null)
        sectionExportCaption = sectionById.GetDefaultExportCaption();
    }
    return sectionExportCaption;
  }

  [Browsable(false)]
  public SectionSortSchema SectionSortSchema
  {
    get
    {
      if (this.sectionSortSchema != null)
        return this.sectionSortSchema;
      if (this.avsDocument != null && this.avsDocument.SortSchema != null)
      {
        if (this.avsDocument.IsSpecification)
          return this.avsDocument.SortSchema.GetSectionSchemaBySectionGuid(this.ChapterGuid);
        if (this.avsDocument.SortSchema.SectionSortSchemas.Length != 0)
          return this.avsDocument.SortSchema.SectionSortSchemas[0];
      }
      return (SectionSortSchema) null;
    }
    set => this.sectionSortSchema = value;
  }

  /// <summary>Установить признак сортировки записям</summary>
  /// <param name="value">Значение</param>
  public void MarkRowsAsSorted(List<AVSRow> rows, bool value)
  {
    for (int index = 0; index < rows.Count; ++index)
      rows[index].IsSorted = value;
  }

  /// <summary>Сортировать</summary>
  public override void Sort()
  {
    this.Sort(this.Rows);
    foreach (AVSRowGroup group in this.Groups)
    {
      if (group != null)
        this.Sort(group.Rows);
    }
  }

  public void Sort(List<AVSRow> rows)
  {
    if (this.SectionSortSchema != null || this.avsDocument == null || this.avsDocument.SortSchema == null)
      rows.Sort((IComparer<AVSRow>) this);
    for (int index = 0; index < rows.Count; ++index)
      rows[index].Index = index;
    this.MarkRowsAsSorted(rows, true);
  }

  /// <summary>У раздела есть настройки сортировки</summary>
  [Browsable(false)]
  public bool HasSortingSettings
  {
    get
    {
      return this.SectionSortSchema != null || this.avsDocument == null || this.avsDocument.SortSchema == null;
    }
  }

  /// <summary>Обновить кэш настроек сортировки</summary>
  /// <param name="sortSchema">Настройки сортировки</param>
  public override void UpdateSortSchema(SortSchema sortSchema)
  {
    if (sortSchema == null)
      throw new ArgumentNullException(nameof (sortSchema));
    if (this.avsDocument != null && !this.avsDocument.IsSpecification || this.ChapterID == -1L)
    {
      if (sortSchema.SectionSortSchemas == null || sortSchema.SectionSortSchemas.Length == 0)
        return;
      this.SectionSortSchema = sortSchema.SectionSortSchemas[0];
    }
    else
      this.SectionSortSchema = sortSchema.GetSectionSchemaBySectionGuid(this.ChapterGuid);
  }

  /// <summary>Пронумеровать позиции записей</summary>
  /// <param name="numerationHelper">Вспомогательный класс для нумерации позиций</param>
  public override void RenumberPositions(NumerationHelper numerationHelper)
  {
    bool isGridViewMode = this.avsDocument.IsGridViewMode;
    AvsRowAttributeInfo fieldPosition = this.avsDocument.Field_Position;
    AvsRowAttributeInfo attrInfo = new AvsRowAttributeInfo(true, AvsIDCache.Attr_DopZamenGroupNum);
    int count = this.AVSDocument.productsInfo.Count;
    List<long> longList = new List<long>();
    int relationDocument = AvsIDCache.Relation_Document;
    Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>();
    for (int index1 = 0; index1 < this.Rows.Count; ++index1)
    {
      int num1 = -1;
      AVSRow row = this.Rows[index1];
      AVSRow avsRow = (AVSRow) null;
      if (index1 != 0)
        avsRow = this.Rows[index1 - 1];
      if (this.Rows[index1].RelType == AvsIDCache.Relation_Project && this.Rows[index1].ObjType != AvsIDCache.ObjType_Complect)
      {
        Guid? sectionGuid = this.SectionInfo?.SectionGuid;
        Guid complectSectionGuid = SpecificationSectionInfo.ComplectSectionGuid;
        if ((sectionGuid.HasValue ? (sectionGuid.HasValue ? (sectionGuid.GetValueOrDefault() == complectSectionGuid ? 1 : 0) : 1) : 0) == 0 && this.Rows[index1].DocNode != null && !AVSDocument.IsNoteRowDocNode((DocumentTreeNode) this.Rows[index1].DocNode))
        {
          SpecifNumbering specifNumbering = numerationHelper.SpecifNumberingFull.SpecifRazdelNumbering.GetSpecifNumbering(this) ?? (SpecifNumbering) numerationHelper.SpecifNumberingFull;
          if (!row.InCommonData_AV && specifNumbering.IzdelieSameNumbers)
          {
            Dictionary<AVSRow, AVSRow[]> formBnumbers = numerationHelper.GetRowItem(row).FormBNumbers;
            List<int> intList = new List<int>();
            if (row.Relations != null)
            {
              for (int relationIndex = 0; relationIndex < row.Relations.Count; ++relationIndex)
              {
                int indexForRelation = row.GetProductIndexForRelation(relationIndex, row.Relations);
                intList.Add(indexForRelation);
              }
            }
            AVSRow key = (AVSRow) null;
            foreach (KeyValuePair<AVSRow, AVSRow[]> keyValuePair in formBnumbers)
            {
              AVSRow[] avsRowArray = keyValuePair.Value;
              bool flag = true;
              foreach (int index2 in intList)
              {
                if (avsRowArray[index2] != null)
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
                key = keyValuePair.Key;
            }
            if (key != null)
            {
              foreach (int index3 in intList)
                formBnumbers[key][index3] = row;
              string s = Convert.ToString(key.GetFieldValue(fieldPosition, 0, -1, false, true));
              int num2 = -1;
              ref int local = ref num2;
              if (int.TryParse(s, out local))
                num1 = num2;
            }
            else
            {
              formBnumbers[row] = new AVSRow[this.AVSDocument.ProductsInfo.Count];
              foreach (int index4 in intList)
                formBnumbers[row][index4] = row;
            }
          }
          object fieldValue1 = this.Rows[index1].GetFieldValue(fieldPosition, 0, -1, (List<RelationAttributeValuesCache>) null, false, true);
          switch (fieldValue1)
          {
            case null:
            case DBNull _:
              if (num1 == -1)
                num1 = numerationHelper.GetNextPosition(this.Rows[index1]);
              numerationHelper.Chapter = (Chapter) null;
              if (num1 != -1)
              {
                string str = num1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
                row.SetFieldValueForAllRelations(fieldPosition, (object) str, true, false, true, isGridViewMode, true, false);
                if (!numerationHelper.ExistNumbers.Contains(num1))
                  numerationHelper.ExistNumbers.Add(num1);
                if (avsRow != null && avsRow.Position > num1)
                  this.avsDocument.AVSWindow.ErrorsUserControl.AddError((ImErrorMessage) new AVSRowErrorMessage(row, new SpecRowCheckMessage(AVSCheckType.All, "Позиция записи меньше чем у предыдущей записи")));
              }
              for (int relationIndex = 0; this.Rows[index1].Relations != null && relationIndex < this.Rows[index1].Relations.Count; ++relationIndex)
              {
                long num3 = 0;
                object fieldValue2 = this.Rows[index1].GetFieldValue(attrInfo, relationIndex, -1, false, false);
                switch (fieldValue2)
                {
                  case null:
                  case DBNull _:
                    if (num3 > 0L && !longList.Contains(num3))
                    {
                      longList.Add(num3);
                      continue;
                    }
                    continue;
                  default:
                    num3 = Convert.ToInt64(fieldValue2);
                    goto case null;
                }
              }
              continue;
            case string _:
              if (!(((string) fieldValue1).Trim() == string.Empty))
                break;
              goto case null;
          }
          double number;
          if (NumberParserAdvanced.ParseNumber(fieldValue1.ToString(), false, out number, out string _, out string _))
          {
            int int32 = Convert.ToInt32(number);
            numerationHelper.TrySetCurrentPosition(int32, this.Rows[index1]);
          }
        }
      }
    }
  }

  /// <summary>Пересчёт количество пропусков после заголовка спецификации</summary>
  /// <param name="skipLinesSchema">Настройки пропусков строк</param>
  /// <param name="page">Страница</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку документа</param>
  public void UpdateSkipLinesAfter(
    SkipLinesSchema skipLinesSchema,
    PageData page,
    bool updateUI,
    bool updateLayout)
  {
    if (this.UseParentDocNode || this.GetAVSWindow() == null || this.docNodes == null || this.parent == null)
      return;
    foreach (TableData docNode in this.docNodes)
    {
      if (page == null || docNode.Page == page)
      {
        TableData chapterCaptionRow = this.GetChapterCaptionRow(docNode);
        if (chapterCaptionRow != null)
        {
          int num = skipLinesSchema != null ? skipLinesSchema.AfterSectionName : 1;
          if (this._skipLinesAfter.HasValue)
            num = this._skipLinesAfter.Value;
          if (this._skipLinesAfter.HasValue)
            chapterCaptionRow.SetAttributeValue(AVSDocument.DocAttr_SkipLinesAfter, this._skipLinesAfter.ToString(), false, false, false);
          else
            chapterCaptionRow.RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
          chapterCaptionRow.SetSkipCellsAfter((float) num, !this._skipLinesAfter.HasValue, updateUI, updateLayout);
        }
      }
    }
  }

  public override TableData CreateDocNode(TableData templateNode)
  {
    TableData docNode = base.CreateDocNode(templateNode);
    TableData chapterCaptionRow = this.GetChapterCaptionRow(docNode);
    if (chapterCaptionRow != null)
    {
      chapterCaptionRow.SetSkipCellsBefore(0.0f, true, false, false);
      chapterCaptionRow.SetSkipCellsAfter(0.0f, true, false, false);
    }
    return docNode;
  }

  public override void UpdateSkipLines(SkipLinesSchema skipLinesSchema, SkipLinesStruct str)
  {
    if (this.docNodes == null || this.parent == null || this.UseParentDocNode)
      return;
    foreach (TableData docNode in this.docNodes)
    {
      TableData tableData = this.GetChapterCaptionRow(docNode) ?? docNode;
      if (this._skipLinesBefore.HasValue)
        tableData.SetAttributeValue(AVSDocument.DocAttr_SkipLinesBefore, this._skipLinesBefore.ToString(), false, false, false);
      else
        tableData.RemoveAttribute(AVSDocument.DocAttr_SkipLinesBefore, false, false);
      if (!float.IsNaN(str.SkipBefore))
        tableData.SetSkipCellsBefore(str.SkipBefore, !this._skipLinesBefore.HasValue, false, false);
      if (this._skipLinesAfter.HasValue)
        tableData.SetAttributeValue(AVSDocument.DocAttr_SkipLinesAfter, this._skipLinesAfter.ToString(), false, false, false);
      else
        tableData.RemoveAttribute(AVSDocument.DocAttr_SkipLinesAfter, false, false);
      if (!float.IsNaN(str.SkipBefore))
        tableData.SetSkipCellsAfter(str.SkipAfter, !this._skipLinesAfter.HasValue, false, false);
    }
  }

  public override SkipLinesStruct GetSkipLines(
    SkipLinesSchema skipLinesSchema,
    List<SkipLinesStruct> structs)
  {
    List<SkipLinesStruct> structs1 = new List<SkipLinesStruct>();
    SkipLinesStruct skipLines = new SkipLinesStruct((Chapter) this);
    if (!this.UseParentDocNode)
    {
      skipLines = base.GetSkipLines(skipLinesSchema, structs1);
      int? nullable = this.SkipLinesAfter;
      if (nullable.HasValue)
      {
        SkipLinesStruct skipLinesStruct = skipLines;
        nullable = this.SkipLinesAfter;
        double num = (double) nullable.Value;
        skipLinesStruct.SkipAfter = (float) num;
      }
      else
      {
        int afterSectionName = skipLinesSchema != null ? skipLinesSchema.AfterSectionName : 0;
        skipLines.SkipAfter = (float) afterSectionName;
      }
      nullable = this.SkipLinesBefore;
      if (nullable.HasValue)
      {
        SkipLinesStruct skipLinesStruct = skipLines;
        nullable = this.SkipLinesBefore;
        double num = (double) nullable.Value;
        skipLinesStruct.SkipBefore = (float) num;
      }
      else
      {
        int beforeSectionName = skipLinesSchema != null ? skipLinesSchema.BeforeSectionName : 0;
        skipLines.SkipBefore = (float) beforeSectionName;
      }
      SkipLinesStruct skipLinesStruct1 = skipLines;
      nullable = this.SkipLinesBefore;
      int num1 = nullable.HasValue ? 1 : 0;
      skipLinesStruct1.BeforeSetted = num1 != 0;
      SkipLinesStruct skipLinesStruct2 = skipLines;
      nullable = this.SkipLinesAfter;
      int num2 = nullable.HasValue ? 1 : 0;
      skipLinesStruct2.AfterSetted = num2 != 0;
    }
    if (this.Rows != null)
    {
      for (int index = 0; index < this.Rows.Count; ++index)
      {
        if (this.Rows[index] != null && this.Rows[index].DocNode != null)
          this.Rows[index].GetSkipLines(skipLinesSchema, structs1);
      }
    }
    foreach (AVSRowGroup group in this.Groups)
      group?.GetSkipLines(skipLinesSchema, structs1);
    foreach (SkipLinesStruct skipLinesStruct in structs1)
    {
      skipLinesStruct.SpecSection = this;
      structs.Add(skipLinesStruct);
    }
    return skipLines;
  }

  /// <summary>Получение предыдущего раздела спецификации</summary>
  /// <returns> предыдущий раздел спецификации </returns>
  public SpecificationSection GetPrevSection()
  {
    if (this.Parent == null)
      return (SpecificationSection) null;
    int num = this.Parent.Chapters.IndexOf((Chapter) this);
    return num <= 0 ? (SpecificationSection) null : (SpecificationSection) this.Parent.Chapters[num - 1];
  }

  [Browsable(false)]
  public SpecificationSection GetNextSection()
  {
    if (this.Parent == null)
      return (SpecificationSection) null;
    int num = this.Parent.Chapters.IndexOf((Chapter) this);
    return num >= this.Parent.Chapters.Count - 1 ? (SpecificationSection) null : this.Parent.Chapters[num + 1] as SpecificationSection;
  }

  /// <summary>Получить родительское окно спецификации</summary>
  /// <returns>родительское окно спецификации</returns>
  public AVSWindow GetAVSWindow()
  {
    return this.avsDocument != null ? this.avsDocument.AVSWindow : (AVSWindow) null;
  }

  /// <summary>Получить идентификатор спецификации</summary>
  /// <returns></returns>
  public long GetSpecificationID() => this.avsDocument != null ? this.avsDocument.DocumentID : -1L;

  public void AddGroup(AVSRowGroup group, bool createDocNode, bool createListNode)
  {
    if (this.Groups.Count == 0)
      this.Groups.Add((AVSRowGroup) null);
    int index = this.Groups.IndexOf((AVSRowGroup) null);
    group.Section = this;
    group.AVSDocument = this.AVSDocument;
    if (group.GroupPosition == AVRowGroupPosition.BeforeRowsGroup)
      this.Groups.Insert(index, group);
    if (group.GroupPosition == AVRowGroupPosition.AfterRowsGroup)
      this.Groups.Add(group);
    if (createDocNode && group.DocNode == null)
    {
      TableData docNodeTemplate = group.GetDocNodeTemplate();
      TableData docNode = group.CreateDocNode(docNodeTemplate);
      group.AddDocNode(docNode);
    }
    if (!createListNode || !this.avsDocument.IsGridViewMode)
      return;
    this.avsDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) this);
  }

  public T GetGroup<T>(string name = null) where T : AVSRowGroup
  {
    T group = this.FindGroup<T>(name);
    if ((object) group == null)
    {
      group = Activator.CreateInstance<T>();
      group.AVSDocument = this.AVSDocument;
      if (name != null)
        group.Name = name;
      this.AddGroup((AVSRowGroup) group, true, true);
    }
    return group;
  }

  public T FindGroup<T>(string name = null) where T : AVSRowGroup
  {
    foreach (AVSRowGroup group in this.Groups)
    {
      if (group is T && (name == null || group.Name == name))
        return (T) group;
    }
    return default (T);
  }

  public void RemoveGroup(AVSRowGroup group, bool removeDocNode)
  {
    this.Groups.Remove(group);
    if (this.Groups.Count == 1 && this.Groups[0] == null)
      this.Groups.Clear();
    if (!removeDocNode)
      return;
    for (int index = 0; index < group.DocNodes.Count; ++index)
    {
      group.DocNodes[index].UniteTable();
      group.DocNodes[index].Remove(true, true);
    }
  }

  public List<AVSRow> GetRowsForRow(AVSRow row)
  {
    row.GetGroup(this);
    return row.Group == null ? this.Rows : row.Group.Rows;
  }

  /// <summary>Добавить запись в раздел</summary>
  /// <param name="row">Запись</param>
  /// <param name="sort">Вставить согласно сортировке</param>
  /// <returns>Индекс записи в разделе</returns>
  public int AddRow(AVSRow row, bool sort)
  {
    if (row == null)
      return -1;
    row.GetGroup(this);
    int index;
    if (sort)
    {
      index = row.Group != null ? AVSDocument.FindIndexInPartlySortedList((object) row, (IList) row.Group.Rows, true, 0, (IComparer) this, new IsSortedItemDelegate(AVSRow.IsSortedSpecRow)) : AVSDocument.FindIndexInPartlySortedList((object) row, (IList) this.Rows, true, 0, (IComparer) this, new IsSortedItemDelegate(AVSRow.IsSortedSpecRow));
      row.IsSorted = true;
    }
    else
      index = row.Group != null ? row.Group.Rows.Count : this.Rows.Count;
    this.InsertRow(index, row);
    return index;
  }

  /// <summary>Добавить запись в раздел, в позицию согласно индексу сортировки</summary>
  /// <param name="row">Запись</param>
  /// <param name="sortIndex">Индекс сортировки. Если -1 или 0, то добавить в конец раздела</param>
  /// <returns>Индекс записи в разделе</returns>
  public int AddRow(AVSRow row, long sortIndex)
  {
    if (row == null)
      return -1;
    List<AVSRow> rows = this.Rows;
    row.GetGroup(this);
    if (row.Group != null)
      rows = row.Group.Rows;
    int index = rows.Count;
    if (sortIndex != -1L && sortIndex != 0L)
    {
      index = 0;
      while (index < rows.Count && (rows[index].SortIndex == 0L || rows[index].SortIndex == -1L || rows[index].SortIndex == long.MaxValue || sortIndex >= rows[index].SortIndex))
        ++index;
    }
    this.InsertRow(index, row);
    return index;
  }

  /// <summary>Вставить запись в раздел</summary>
  /// <param name="index">Индекс записи</param>
  /// <param name="row">Запись</param>
  public void InsertRow(int index, AVSRow row)
  {
    if (row == null)
      return;
    row.GetGroup(this);
    SpecificationSection section = row.Section;
    if (section != null && section != this)
      section.RemoveRow(row, false, false, false, false, false);
    row.Section = this;
    if (row.Group != null)
    {
      row.Group.InsertRow(index, row);
    }
    else
    {
      this.Rows.Insert(index, row);
      for (int index1 = index; index1 < this.Rows.Count; ++index1)
        this.Rows[index1].Index = index1;
    }
    if (this.avsDocument == null)
      return;
    this.avsDocument.RegisterAVSRowInDictionaries(row);
  }

  /// <summary>Переместить запись внутри раздела</summary>
  /// <param name="toRow">Запись после или перед которой должна идти устанавливаемая, если null, то устанавливается в 0 позицию</param>
  /// <param name="row">Перемещаемая строка</param>
  /// <param name="setAfter">True - установить после, False - перед</param>
  public void MoveRow(AVSRow row, AVSRow toRow, bool setAfter)
  {
    if (row.Section != this)
      return;
    this.Rows.Remove(row);
    int index1 = 0;
    if (toRow != null)
    {
      index1 = this.Rows.IndexOf(toRow);
      if (setAfter)
        ++index1;
    }
    this.InsertRow(index1, row);
    for (int index2 = 0; index2 < this.Rows.Count; ++index2)
      this.Rows[index2].Index = index2;
  }

  /// <summary>Переместить запись внутри раздела</summary>
  /// <param name="index"></param>
  /// <param name="row"></param>
  public void MoveRow(int index, AVSRow row)
  {
    if (row.Section != this)
      return;
    this.Rows.Remove(row);
    this.InsertRow(index, row);
    for (int index1 = 0; index1 < this.Rows.Count; ++index1)
      this.Rows[index1].Index = index1;
  }

  /// <summary>Удалить запись из раздела</summary>
  /// <param name="row">Запись</param>
  /// <param name="removeFromDictionary">Удалить из словарей</param>
  /// <param name="removeRelation">Удалить связь из базы</param>
  /// <param name="removeDocNode">Удалить из документа</param>
  /// <param name="removeTreeNode">Удалить из табличного вида</param>
  /// <param name="removeDocObjectWithoutRelations">Удалять документы без связей</param>
  public List<KeyValuePair<long, RelInfo>> RemoveRow(
    AVSRow row,
    bool removeFromDictionary,
    bool removeRelation,
    bool removeDocNode,
    bool removeTreeNode,
    bool removeDocObjectWithoutRelations)
  {
    List<KeyValuePair<long, RelInfo>> keyValuePairList = new List<KeyValuePair<long, RelInfo>>();
    if (row == null)
      return keyValuePairList;
    List<string> stringList = new List<string>();
    if (removeRelation)
    {
      bool flag = row.RelType == AvsIDCache.Relation_Document && !AVSDocument.IsParentObjectType(AvsIDCache.ObjType_ConstructorDocument, row.ObjType);
      long num = row.Object_F_ID;
      long objectId = row.ObjectId;
      string objCaption = row.ObjCaption;
      using (SessionKeeper sessionKeeper1 = new SessionKeeper())
      {
        foreach (RelationAttributeValuesCache allRelation in row.GetAllRelations())
        {
          if (allRelation.RelationType == AvsIDCache.Relation_Project && allRelation.GetValueBool(this.avsDocument.Attr_Podbor, false))
          {
            string valueString = allRelation.GetValueString(this.AVSDocument.Field_PosDesignation, false);
            if (!string.IsNullOrEmpty(valueString) && !stringList.Contains(valueString))
              stringList.Add(valueString);
          }
          IDBRelation relationByPartObjectId = sessionKeeper1.Session.GetRelationByPartObjectID(allRelation.RelationId, row.ObjectId, false);
          if (relationByPartObjectId != null)
          {
            if (flag)
            {
              relationByPartObjectId.SetAttributesValues(new AttributeValues[1]
              {
                new AttributeValues(AvsIDCache.Attr_SpecificationSection, (object) null)
              });
            }
            else
            {
              keyValuePairList.Add(new KeyValuePair<long, RelInfo>(relationByPartObjectId.RelationID, new RelInfo(relationByPartObjectId.ProjID, row.ObjType, relationByPartObjectId.RelationType)));
              relationByPartObjectId.Delete(0L);
            }
          }
        }
        if (removeDocObjectWithoutRelations)
        {
          if (row.RelType == AvsIDCache.Relation_Document)
          {
            if (num == -1L)
            {
              IDBObject dbObject = sessionKeeper1.Session.GetObject(objectId, false);
              if (dbObject != null)
                num = dbObject.ID;
            }
            if (num != -1L)
            {
              IDBRelationCollection relationCollection = sessionKeeper1.Session.GetRelationCollection(-1, this.avsDocument.FiltrationOwnerID);
              relationCollection.ObjectTypeID = -1;
              DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
              {
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0),
                new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Value, ColumnNameMapping.ID, SortOrders.NONE, 0)
              }, recordCount: 1);
              AVSDocument.SetFiltrationTags(ref paramSet, new AVSDocumentContext());
              if (relationCollection.EntersIn(paramSet, num).Rows.Count == 0)
              {
                if (sessionKeeper1.Session.GetObject(objectId, false) != null)
                {
                  if (MessageBox.Show($"Документ \"{objCaption}\" ни с чем не связан. Удалить документ вместе с записью?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                  {
                    List<long> objectVersions = sessionKeeper1.Session.GetObjectVersions(num);
                    if (objectVersions != null && objectVersions.Count > 1)
                    {
                      DeletingObjects deletingObjects = new DeletingObjects();
                      for (int index = 0; index < objectVersions.Count; ++index)
                        deletingObjects.Add(0L, num, objectVersions[index], true);
                      using (SessionKeeper sessionKeeper2 = new SessionKeeper())
                      {
                        if (sessionKeeper2.Session.GetCustomService(typeof (IObjectsDeleteAnalyzerService)) is IObjectsDeleteAnalyzerService customService)
                          deletingObjects = customService.LoadDescriptions(sessionKeeper2.Session.SessionGUID, deletingObjects);
                      }
                      AdvancedServiceContainer services = new AdvancedServiceContainer();
                      services.AddService(typeof (IViewState), (object) new ViewStateService());
                      DeleteAnalyzerJobStatus analyzerJobStatus;
                      while (true)
                      {
                        DialogResult dialogResult = DeleteObjectsForm.Execute((System.IServiceProvider) services, deletingObjects, ref ObjectCommands.DeleteOptions);
                        switch (dialogResult)
                        {
                          case DialogResult.Yes:
                          case DialogResult.No:
                            if (dialogResult == DialogResult.No)
                            {
                              for (int index = 0; index < deletingObjects.Count; ++index)
                                deletingObjects[index].Items.Clear();
                              analyzerJobStatus = DeleteAnalyzerForm.Execute(deletingObjects, ObjectCommands.DeleteOptions);
                              if (analyzerJobStatus != null && analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Cancelled && analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Working)
                              {
                                if (analyzerJobStatus.Progress != DeleteAnalyzerJobProgress.Error)
                                {
                                  deletingObjects = analyzerJobStatus.Items;
                                  continue;
                                }
                                goto label_44;
                              }
                              goto label_42;
                            }
                            goto label_48;
                          default:
                            goto label_36;
                        }
                      }
label_36:
                      deletingObjects = (DeletingObjects) null;
                      goto label_48;
label_42:
                      deletingObjects = (DeletingObjects) null;
                      goto label_48;
label_44:
                      if (analyzerJobStatus.Exception != null)
                        ExceptionHelper.ExceptionService.ShowException(analyzerJobStatus.Exception);
                      deletingObjects = (DeletingObjects) null;
label_48:
                      ObjectCommands.DeleteOptions &= ~DeleteAnalyzerOptions.FindAllVersions;
                      List<long> objectIDs = (List<long>) null;
                      try
                      {
                        if (deletingObjects != null)
                        {
                          if (deletingObjects.Count > 0)
                          {
                            DeleteObjectsJobStatus objectsJobStatus = DeleteProgressForm.Execute(deletingObjects);
                            objectIDs = objectsJobStatus?.Items;
                            if (objectsJobStatus.Progress == DeleteObjectsJobProgress.Error)
                            {
                              if (objectsJobStatus.Exception != null)
                                ExceptionHelper.ExceptionService.ShowException(objectsJobStatus.Exception);
                            }
                          }
                        }
                      }
                      finally
                      {
                        INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
                        if (service != null && objectIDs != null && objectIDs.Count > 0)
                        {
                          DBObjectsEventArgs e = new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) objectIDs);
                          service.FireEvent((object) this.avsDocument, (NotificationEventArgs) e);
                        }
                      }
                    }
                    else
                    {
                      IDBObject dbObject = sessionKeeper1.Session.GetObject(Math.Abs(objectId), false);
                      if (dbObject != null)
                      {
                        dbObject.Delete(0L);
                        INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
                        if (service != null)
                        {
                          List<long> longList = new List<long>();
                          longList.Add(objectId);
                          if (objectId < 0L)
                            longList.Add(-objectId);
                          service.FireEvent((object) this.avsDocument, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", objectId));
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    if (((!row.HasPartAsMaterial(out long _) ? 0 : (row.HasRelation ? 1 : 0)) & (removeFromDictionary ? 1 : 0)) != 0)
    {
      List<KeyValuePair<long, RelInfo>> collection = row.RemoveZagotovka();
      keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
    }
    if (this.avsDocument != null & removeFromDictionary)
      this.avsDocument.UnregisterSpecRowInDictionaries(row);
    if (removeRelation)
      row.ClearRelations();
    if (row.Group != null)
    {
      row.Group.RemoveRow(row);
      if (row.Group.IsEmpty)
        this.RemoveGroup(row.Group, removeDocNode);
    }
    else
    {
      this.Rows.Remove(row);
      row.Index = -1;
      row.Section = (SpecificationSection) null;
      for (int index = 0; index < this.Rows.Count; ++index)
        this.Rows[index].Index = index;
    }
    if (removeRelation && !stringList.IsNullOrEmpty<string>())
    {
      List<KeyValuePair<long, RelInfo>> collection = this.avsDocument.RemovePodborForPosDesignationRows(stringList);
      keyValuePairList.AddRange((IEnumerable<KeyValuePair<long, RelInfo>>) collection);
    }
    if (removeDocNode)
    {
      if (this.avsDocument.CollectChangeEvents)
        this.avsDocument.AvsRowEventMessageViewer.AddEvent(row, new AvsRowEventMessage(AVSEventType.RemoveRow));
      for (int index = 0; index < row.DocNodes.Count; ++index)
      {
        row.DocNodes[index].UniteTable();
        row.DocNodes[index].Remove(true, true);
      }
      if (row.DocNodeExp != null)
      {
        row.DocNodeExp.UniteTable();
        row.DocNodeExp.Remove(true, true);
      }
    }
    if (removeTreeNode)
    {
      if (this.AVSDocument.IsGridViewMode)
        this.AVSDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) row.Section);
      if (this.AVSDocument.IsGridViewMode)
        this.AVSDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) this);
    }
    return keyValuePairList;
  }

  /// <summary>Переместить записи спецификации в другой раздел. В пределах одной спецификации.
  /// Не меняет значений атрибутов!</summary>
  /// <param name="row">Запись</param>
  /// <param name="newSection">Раздел в который нужно переместить запись</param>
  /// <param name="moveDocNode">Переместить узел документа</param>
  /// <param name="moveTreeNode">Переместить узел дерева табличного вида</param>
  /// <param name="resetSortIndex">Сбросить индекс сортировки</param>
  public void MoveRow(
    AVSRow row,
    SpecificationSection newSection,
    bool moveDocNode,
    bool moveTreeNode,
    bool resetSortIndex)
  {
    if (newSection == null)
      throw new ArgumentNullException(nameof (newSection));
    SpecificationSection section = row.Section;
    if (section == newSection)
      return;
    section.RemoveRow(row, false, false, false, false, false);
    if (resetSortIndex)
      row.SortIndex = 0L;
    row.SetFieldValue(this.avsDocument.Attr_Section, -1, -1, newSection.ChapterID != -1L ? (object) newSection.ChapterID : (object) null, !this.avsDocument.ReadOnly, true, false, false, false, true, false);
    int partlySortedList = AVSDocument.FindIndexInPartlySortedList((object) row, (IList) newSection.Rows, true, 0, (IComparer) newSection, new IsSortedItemDelegate(AVSRow.IsSortedSpecRow));
    row.Section = newSection;
    newSection.InsertRow(partlySortedList, row);
    row.IsSorted = true;
    row.CheckAdditionalChapter();
    if (moveDocNode)
      row.UpdateDocRow((TableData) null, row.Section.DocRowFields, true, false, false, EmptyRowUpdateMode.DontChange);
    if (!moveTreeNode || !this.AVSDocument.IsGridViewMode)
      return;
    this.AVSDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) section);
    this.AVSDocument.AVSWindow.virtualTree.RefreshRows((IVirtualTreeItem) newSection);
  }

  /// <summary>Получить все записи раздела</summary>
  /// <param name="withRelationsOnly">Получить только записи со связями</param>
  /// <param name="withObjectsOnly">Получить только записи с объектами. Если onlyRelations и onlyObjects имеют значение false,
  /// то получают и информационные записи примечания</param>
  /// <param name="rowList">Список полученных записей</param>
  public override void GetAllRowsList(
    bool withRelationsOnly,
    bool withObjectsOnly,
    List<AVSRow> rowList)
  {
    if (!withRelationsOnly && !withObjectsOnly)
    {
      rowList.AddRange((IEnumerable<AVSRow>) this.Rows);
    }
    else
    {
      for (int index = 0; index < this.Rows.Count; ++index)
      {
        if (withRelationsOnly)
        {
          if (this.Rows[index].HasRelation)
            rowList.Add(this.Rows[index]);
        }
        else if (!withObjectsOnly || this.Rows[index].ObjectId != -1L)
          rowList.Add(this.Rows[index]);
      }
    }
    foreach (AVSRowGroup group in this.Groups)
      group?.GetAllRows(withRelationsOnly, withObjectsOnly, rowList);
  }

  /// <summary>Получить все записи раздела</summary>
  /// <param name="withRelationsOnly">Получить только записи со связями</param>
  /// <param name="withObjectsOnly">Получить только записи с объектами. Если onlyRelations и onlyObjects имеют значение false,
  /// то получают и информационные записи примечания</param>
  /// <param name="rowList">Список полученных записей</param>
  public override IEnumerable<AVSRow> GetRows(bool withRelationsOnly = false, bool withObjectsOnly = false)
  {
    foreach (AVSRow row in this.Rows)
    {
      if (((!withRelationsOnly ? 1 : (row.HasRelation ? 1 : 0)) & (!withObjectsOnly ? (true ? 1 : 0) : (row.HasObject ? 1 : 0))) != 0)
        yield return row;
    }
    foreach (AVSRowGroup group in this.Groups)
    {
      if (group != null)
      {
        foreach (AVSRow row in group.GetRows(withRelationsOnly, withObjectsOnly))
          yield return row;
      }
    }
  }

  /// <summary>Раздел пустой</summary>
  public override bool IsEmpty
  {
    [DebuggerStepThrough] get
    {
      return !this.Rows.Any<AVSRow>((Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow)) && this.Groups.Count == 0;
    }
  }

  /// <summary>В разделе есть записи, которые отображаются в документе</summary>
  [Browsable(false)]
  public bool HasRowsWithDocNode
  {
    [DebuggerStepThrough] get
    {
      foreach (AVSRow row in this.Rows)
      {
        if (row.HasDocNodes)
          return true;
      }
      return false;
    }
  }

  public override List<IVirtualTreeItem> GetTreeChildren()
  {
    List<IVirtualTreeItem> treeChildren = new List<IVirtualTreeItem>();
    IEnumerable<AVSRow> rows = this.GetRows(false, false);
    List<AVSRow> avsRowList = new List<AVSRow>();
    foreach (AVSRow avsRow1 in rows)
    {
      AVSRow item = avsRow1;
      if (item.Group == null)
      {
        if (item.IsDynamicGroupHeaderRow)
        {
          List<AVSRow> dynamicGroupRows = item.GetDynamicGroupRows();
          if (treeChildren.Find((Predicate<IVirtualTreeItem>) (x => (x as AVSRow).GroupHeaderText == item.GroupHeaderText)) == null || dynamicGroupRows.Count != 0)
          {
            treeChildren.Add((IVirtualTreeItem) item);
            avsRowList.Add(item);
            avsRowList.AddRange((IEnumerable<AVSRow>) dynamicGroupRows);
            foreach (AVSRow avsRow2 in dynamicGroupRows)
            {
              ((IVirtualTreeItem) avsRow2).ParentItem = (IVirtualTreeItem) item;
              if (treeChildren.Contains((IVirtualTreeItem) avsRow2))
                treeChildren.Remove((IVirtualTreeItem) avsRow2);
            }
          }
        }
        else if (!avsRowList.Contains(item))
          treeChildren.Add((IVirtualTreeItem) item);
      }
    }
    foreach (AVSRowGroup group in this.Groups)
    {
      if (group != null)
        treeChildren.Add((IVirtualTreeItem) group);
    }
    return treeChildren;
  }

  /// <summary>Обновить узлы для страничного и табличного видов</summary>
  /// <param name="skipLinesSchema">Настройки пропусков строк</param>
  /// <param name="reCreateDocNode">Пересоздавать узлы документа</param>
  /// <param name="reCreateListNode">Пересоздавать узлы табличного вида</param>
  /// <param name="updateCountB">Обновить количество для групповой СП формы Б</param>
  /// <param name="createForEmptyChapters">Создавать узлы для пустых разделов</param>
  /// <param name="updateTemplate">Обновить шаблоны узлов документа</param>
  /// <param name="updateMode">Режим обновления записей с пустым количеством</param>
  public override void UpdateViewNodes(
    SkipLinesSchema skipLinesSchema,
    bool reCreateDocNode,
    bool reCreateListNode,
    bool updateCountB,
    bool createForEmptyChapters,
    bool updateTemplate,
    EmptyRowUpdateMode updateMode)
  {
    if (reCreateDocNode)
    {
      this.DocNodes = new List<TableData>();
      this.DocNodesExp = new List<TableData>();
      this.DocNodesBlocks = (List<DocNodesBlock>) null;
    }
    int num1 = this.avsDocument.IsGridViewMode ? 1 : 0;
    List<int> intList = new List<int>();
    int num2 = -1;
    TableData dataOwner1 = (TableData) null;
    TableData dataOwner2 = (TableData) null;
    DocNodesBlock docNodesBlock = (DocNodesBlock) null;
    List<TableData> sectionDocNodes = new List<TableData>();
    List<TableData> tableDataList1 = new List<TableData>();
    List<DocNodesBlock> docNodesBlockList = new List<DocNodesBlock>();
    int index1 = 0;
    int index2 = 0;
    List<TableData> tableDataList2 = new List<TableData>();
    TableData tableData = (TableData) null;
    int num3 = this.UseParentDocNode ? 1 : 0;
    int key = 0;
    TableData templateNode = num3 == 0 ? this.GetDocNodeTemplate() : this.parent.GetDocNodeTemplate();
    TableData docNodeExpTemplate = this.IsExportSP ? this.GetDocNodeExpTemplate() : (TableData) null;
    Dictionary<int, TableData> dictionary = new Dictionary<int, TableData>();
    if (!this.avsDocument.IsExportSP)
    {
      if (this.HasDocNodes)
      {
        for (int index3 = 0; index3 < this.docNodes.Count; ++index3)
        {
          key = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.docNodes[index3]);
          if (!dictionary.ContainsKey(key))
          {
            dictionary.Add(key, this.docNodes[index3]);
            sectionDocNodes.Add(this.docNodes[index3]);
            tableDataList2.Add((TableData) null);
            intList.Add(-1);
          }
          else
          {
            dataOwner1 = this.docNodes[index3];
            this.docNodes.RemoveAt(index3);
            --index3;
            dataOwner1.Remove(false, false);
          }
        }
      }
    }
    else
    {
      if (this.HasDocNodes)
      {
        for (int index4 = 0; index4 < this.docNodes.Count; ++index4)
        {
          sectionDocNodes.Add(this.docNodes[index4]);
          tableDataList2.Add((TableData) null);
          intList.Add(-1);
        }
      }
      if (this.HasDocNodesExp)
      {
        tableDataList1.AddRange((IEnumerable<TableData>) this.DocNodesExp);
        tableData = (TableData) null;
        num2 = -1;
      }
    }
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    List<AvsRowAttributeInfo> docRowFields = this.DocRowFields;
    for (int index5 = 0; index5 < this.Rows.Count; ++index5)
    {
      bool flag4 = false;
      AVSRow row = this.Rows[index5];
      row.UpdateDocRow((TableData) null, docRowFields, reCreateDocNode, updateCountB, updateTemplate, updateMode);
      bool hasDocNodes = row.HasDocNodes;
      bool hasDocNodeExp = row.HasDocNodeExp;
      if (this.avsDocument.IsExportSP)
      {
        if (index1 < sectionDocNodes.Count)
          dataOwner1 = sectionDocNodes[index1];
        if (dataOwner1 != null && !this.TopTableIsSuitable_Normal(dataOwner1, hasDocNodes, hasDocNodeExp))
        {
          ++index1;
          docNodesBlock = (DocNodesBlock) null;
          dataOwner1 = index1 >= sectionDocNodes.Count ? (TableData) null : sectionDocNodes[index1];
          if (dataOwner1 != null && !this.TopTableIsSuitable_Normal(dataOwner1, hasDocNodes, hasDocNodeExp))
            dataOwner1 = (TableData) null;
        }
        if (index2 < tableDataList1.Count)
          dataOwner2 = tableDataList1[index2];
        if (dataOwner2 != null && !this.TopTableIsSuitable_Export(dataOwner2, hasDocNodes, hasDocNodeExp))
        {
          ++index2;
          docNodesBlock = (DocNodesBlock) null;
          dataOwner2 = index2 >= tableDataList1.Count ? (TableData) null : tableDataList1[index2];
          if (dataOwner2 != null && !this.TopTableIsSuitable_Export(dataOwner2, hasDocNodes, hasDocNodeExp))
            dataOwner2 = (TableData) null;
        }
      }
      if (row.HasDocNodes)
      {
        for (int index6 = 0; index6 < row.DocNodes.Count; ++index6)
        {
          bool flag5 = row.DocNodes[index6].Parent == null;
          flag4 |= flag5;
          if (!this.avsDocument.IsExportSP)
          {
            key = row.GetFirstProductIndexForDocRow((DocumentTreeNode) row.DocNodes[index6]);
            dictionary.TryGetValue(key, out dataOwner1);
            if (dataOwner1 != null)
            {
              if (sectionDocNodes.Count == 1)
              {
                index1 = 0;
              }
              else
              {
                for (int index7 = 0; index7 < sectionDocNodes.Count; ++index7)
                {
                  if (sectionDocNodes[index7].FindFirstTable() == dataOwner1)
                  {
                    index1 = index7;
                    break;
                  }
                }
              }
            }
          }
          if (dataOwner1 == null)
          {
            dataOwner1 = this.CreateDocNode(templateNode);
            flag2 = true;
            if (this.avsDocument.IsExportSP)
            {
              if (index1 > 0)
              {
                this.HideChapterHeaderRow(dataOwner1);
                dataOwner1.SetFromNewPage(true, false, false);
              }
            }
            else
            {
              dictionary.Add(key, dataOwner1);
              index1 = 0;
              while (index1 < sectionDocNodes.Count && Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) sectionDocNodes[index1]) <= key)
                ++index1;
              if (key != 0)
                dataOwner1.SetAttributeValue(AVSRow.DocAttr_ProductIndex, key.ToString(), false, false, false);
              else
                dataOwner1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
            }
            sectionDocNodes.Insert(index1, dataOwner1);
            intList.Add(-1);
            tableDataList2.Insert(index1, (TableData) null);
          }
          if (tableDataList2[index1] == null)
            intList[index1] = dataOwner1.FindDataPositionInFlow(0, out dataOwner1);
          else if (intList[index1] == -1)
          {
            if (tableDataList2[index1].ParentCell.FindNextDataCellInFlow(tableDataList2[index1].Index) == row.DocNodes[index6])
            {
              dataOwner1 = row.DocNodes[index6].ParentCell;
              intList[index1] = row.DocNodes[index6].Index;
            }
            else
            {
              RectangleElement lastCell = tableDataList2[index1].FindLastCell();
              TableData parentCell = lastCell.ParentCell;
              int num4 = lastCell.Index + 1;
              dataOwner1 = parentCell;
              intList[index1] = num4;
            }
          }
          if (intList[index1] < 0)
          {
            LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes #1. row id \"{row.DocNodes[index6].Id}\"; row index {index1}; node index {intList[index1]}; section node id \"{dataOwner1.Id}\"", true);
            intList[index1] = 0;
          }
          else if (intList[index1] > dataOwner1.Nodes.Count)
          {
            LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes #2. row id \"{row.DocNodes[index6].Id}\"; row index {index1}; node index {intList[index1]}; nodes Count {dataOwner1.Nodes.Count}; section node id \"{dataOwner1.Id}\"", true);
            intList[index1] = dataOwner1.Nodes.Count;
          }
          if (flag5 && this.AVSDocument.CollectChangeEvents)
            this.avsDocument.AvsRowEventMessageViewer.AddEvent(row, new AvsRowEventMessage(AVSEventType.AddRow));
          dataOwner1.InsertChildNode(intList[index1]++, (DocumentTreeNode) row.DocNodes[index6], row.DocNodes[index6].Parent != null, true, false, false, false);
          row.DocNodes[index6].SetVisible(true, false, false, false, true);
          sectionDocNodes[index1] = dataOwner1;
          intList[index1] = -1;
          tableDataList2[index1] = row.DocNodes[index6];
        }
      }
      if (row.HasDocNodeExp)
      {
        bool flag6 = row.DocNodeExp.Parent == null;
        flag1 = flag4 | flag6;
        if (dataOwner2 == null)
        {
          dataOwner2 = this.CreateDocNode(docNodeExpTemplate);
          flag3 = true;
          tableDataList1.Insert(index2, dataOwner2);
          num2 = -1;
          tableData = (TableData) null;
          if (index2 > 0)
          {
            this.HideChapterHeaderRow(dataOwner2);
            dataOwner2.SetFromNewPage(true, false, false);
          }
        }
        if (tableData == null)
        {
          num2 = dataOwner2.FindDataPositionInFlow(0, out dataOwner2);
        }
        else
        {
          dataOwner2 = tableData.Parent as TableData;
          if (num2 == -1)
            num2 = dataOwner2.FindNextDataPositionInFlow(tableData.Index, out dataOwner2);
        }
        if (num2 < 0)
        {
          LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes Warning#3. row id \"{row.DocNodeExp.Id}\"; {$"node index {num2}; section node id \"{dataOwner2.Id}\""}", true);
          num2 = 0;
        }
        else if (num2 > dataOwner2.Nodes.Count)
        {
          LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes #4. row id \"{row.DocNodeExp.Id}\"; {$"node index {num2}; nodes Count {dataOwner2.Nodes.Count}; "}section node id \"{dataOwner2.Id}\"", true);
          num2 = dataOwner2.Nodes.Count;
        }
        dataOwner2.InsertChildNode(num2++, (DocumentTreeNode) row.DocNodeExp, row.DocNodeExp.Parent != null, true, false, false, false);
        tableDataList1[index2] = dataOwner2;
        if (!flag6)
          num2 = -1;
        tableData = row.DocNodeExp;
      }
      if (docNodesBlock == null && this.avsDocument.IsExportSP)
      {
        docNodesBlock = new DocNodesBlock(dataOwner1, dataOwner2);
        docNodesBlockList.Add(docNodesBlock);
      }
    }
    for (int index8 = 0; index8 < this.Groups.Count; ++index8)
    {
      AVSRowGroup group = this.Groups[index8];
      if (group != null)
      {
        dataOwner1 = (TableData) null;
        group.UpdateViewNodes(skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
        for (int index9 = 0; index9 < group.DocNodes.Count; ++index9)
        {
          if (!this.avsDocument.IsExportSP)
          {
            key = group.GetFirstProductIndexForDocRow((DocumentTreeNode) group.DocNodes[index9]);
            dictionary.TryGetValue(key, out dataOwner1);
            if (dataOwner1 != null)
            {
              if (sectionDocNodes.Count == 1)
              {
                index1 = 0;
              }
              else
              {
                for (int index10 = 0; index10 < sectionDocNodes.Count; ++index10)
                {
                  if (sectionDocNodes[index10].FindFirstTable() == dataOwner1)
                  {
                    index1 = index10;
                    break;
                  }
                }
              }
            }
          }
          if (dataOwner1 == null)
          {
            dataOwner1 = this.CreateDocNode(templateNode);
            flag2 = true;
            if (this.avsDocument.IsExportSP)
            {
              if (index1 > 0)
              {
                this.HideChapterHeaderRow(dataOwner1);
                dataOwner1.SetFromNewPage(true, false, false);
              }
            }
            else
            {
              dictionary.Add(key, dataOwner1);
              index1 = 0;
              while (index1 < sectionDocNodes.Count && Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) sectionDocNodes[index1]) <= key)
                ++index1;
              if (key != 0)
                dataOwner1.SetAttributeValue(AVSRow.DocAttr_ProductIndex, key.ToString(), false, false, false);
              else
                dataOwner1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
            }
            sectionDocNodes.Insert(index1, dataOwner1);
            intList.Add(-1);
          }
          if (group.GroupPosition == AVRowGroupPosition.BeforeRowsGroup)
            dataOwner1.InsertChildNode(0, (DocumentTreeNode) group.DocNodes[index9], group.DocNodes[index9].Parent != null, true, false, false, false);
          else
            dataOwner1.AddChildNode((DocumentTreeNode) group.DocNodes[index9], group.DocNodes[index9].Parent != null, true, false, false);
          group.DocNodes[index9].SetVisible(true, false, false, false, true);
        }
      }
    }
    for (int index11 = 0; index11 < sectionDocNodes.Count; ++index11)
      sectionDocNodes[index11] = sectionDocNodes[index11].FindFirstTable();
    for (int index12 = 0; index12 < tableDataList1.Count; ++index12)
      tableDataList1[index12] = tableDataList1[index12].FindFirstTable();
    if (!this.IsExportSP)
      this.RemoveEmptySectionDocNodes(sectionDocNodes, updateMode);
    if (((this.avsDocument.ReadOnly ? 0 : (sectionDocNodes.Count == 0 ? 1 : 0)) & (createForEmptyChapters ? 1 : 0)) != 0)
    {
      for (int index13 = 0; index13 * this.avsDocument.RowProductCount < this.avsDocument.productsInfo.Count && (index13 <= 0 || this.IsFormB); ++index13)
      {
        flag2 = true;
        dataOwner1 = this.CreateDocNode(templateNode);
        if (index13 > 0)
          dataOwner1.SetAttributeValue(AVSRow.DocAttr_ProductIndex, (index13 * this.avsDocument.RowProductCount).ToString(), false, false, false);
        else
          dataOwner1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
        sectionDocNodes.Add(dataOwner1);
      }
    }
    if (flag2)
      this.DocNodes = sectionDocNodes;
    if (flag3)
      this.DocNodesExp = tableDataList1;
    this.DocNodesBlocks = docNodesBlockList;
    if (!AvsConfig.General.ShowSectionNumInNote)
      return;
    TableData chapterCaptionRow = this.GetChapterCaptionRow();
    DocumentTreeNode documentTreeNode;
    if (chapterCaptionRow == null)
    {
      documentTreeNode = (DocumentTreeNode) null;
    }
    else
    {
      DocumentTreeNodeCollection nodes = chapterCaptionRow.Nodes;
      documentTreeNode = nodes != null ? nodes.Where<DocumentTreeNode>((Func<DocumentTreeNode, bool>) (n => n is TextData textData && textData.Name.Equals(AVSRow.DocAttr_Note, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault<DocumentTreeNode>() : (DocumentTreeNode) null;
    }
    if (!(documentTreeNode is TextData textData1))
      return;
    textData1.AssignText(this.SectionInfo.RazdelSP, false, true, false, false, false);
  }

  internal bool RemoveEmptySectionDocNodes(
    List<TableData> sectionDocNodes,
    EmptyRowUpdateMode updateMode)
  {
    bool flag1 = false;
    for (int index = sectionDocNodes.Count - 1; index >= 0; --index)
    {
      int indexForDocChapter = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) sectionDocNodes[index]);
      bool flag2 = sectionDocNodes[index].CalcDataCellCount() == 0;
      bool flag3 = updateMode == EmptyRowUpdateMode.Delete & flag2;
      int count = this.avsDocument.productsInfo.Count;
      if (indexForDocChapter >= count | flag3)
      {
        sectionDocNodes[index].Tag = (object) null;
        sectionDocNodes[index].UniteTable();
        sectionDocNodes[index].Remove(false, false);
        sectionDocNodes.RemoveAt(index);
        flag1 = true;
      }
    }
    return flag1;
  }

  /// <summary>Получить шаблон для узла документа</summary>
  public override TableData GetDocNodeTemplate()
  {
    if (this.avsDocument == null)
      return (TableData) null;
    return this.parent != null && this.UseParentDocNode ? this.parent.GetDocNodeTemplate() : this.GetSectionTemplate();
  }

  /// <summary>Получить шаблон для узла документа</summary>
  public override TableData GetDocNodeExpTemplate()
  {
    return this.avsDocument == null ? (TableData) null : this.GetSectionExpTemplate();
  }

  /// <summary>Использовать родительский узел документа</summary>
  public override bool UseParentDocNode
  {
    [DebuggerStepThrough] get
    {
      return this.avsDocument != null && !this.avsDocument.IsSpecification && this.parent != null && this.parent is ProductVariableDataChapter;
    }
  }

  /// <summary>Индексировать записи документа</summary>
  /// <param name="startIndex">Начальный индекс</param>
  /// <param name="endIndex">Последний индекс диапазона</param>
  /// <param name="onlyNew">Не менять уже установленные индексы</param>
  /// <param name="session">Сессия</param>
  public override void IndexSpecificationRows(
    long startIndex,
    out long endIndex,
    bool onlyNew,
    IUserSession session)
  {
    endIndex = startIndex + 10000000L;
    this.avsDocument.IndexSpecificationRows(this, onlyNew, startIndex, ref endIndex, session);
  }

  /// <summary>Пропуск строк перед записью </summary>
  [DefaultValue(null)]
  [Description("Пропуск строк перед записью")]
  [DisplayName("Перед записью")]
  [Category("Пропуск строк")]
  public int? SkipLinesBefore
  {
    [DebuggerStepThrough] get => this._skipLinesBefore;
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
      this.avsDocument.UpdateSkipLines(true, true);
    }
  }

  /// <summary>Пропуск строк после записи </summary>
  [DefaultValue(null)]
  [Description("Пропуск строк после записи")]
  [DisplayName("После записи")]
  [Category("Пропуск строк")]
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
      this.avsDocument.UpdateSkipLines(true, true);
    }
  }

  private void SetAttributeValuesToDocNodes(
    string docAtributeName,
    string value,
    bool setNeedUpdateLayoutFlag = false)
  {
    foreach (TableData docNode in this.DocNodes)
      SpecificationSection.SetAttributeValuesToDocNode(docNode, docAtributeName, value, setNeedUpdateLayoutFlag);
  }

  private static void SetAttributeValuesToDocNode(
    TableData docNode,
    string docAtributeName,
    string value,
    bool setNeedUpdateLayoutFlag)
  {
    if (!string.IsNullOrEmpty(value))
      docNode.SetAttributeValue(docAtributeName, value, false, false, false);
    else
      docNode.RemoveAttribute(docAtributeName, false, false);
    if (!setNeedUpdateLayoutFlag)
      return;
    docNode.SetNeedUpdateLayoutFlag(true, true, false, false);
  }

  internal void UpdateDynamicHeaderSettings(
    DynamicGroupHeaderSettings dynamicGroupHeaderSettings)
  {
    foreach (TableData docNode in this.DocNodes)
      this.UpdateDocNodeDynamicHeaderSettings(docNode, dynamicGroupHeaderSettings);
    foreach (AVSRow row in this.Rows)
      row.UpdateDynamicHeaderSettings(dynamicGroupHeaderSettings);
  }

  private void UpdateDocNodeDynamicHeaderSettings(
    TableData docNode,
    DynamicGroupHeaderSettings dynamicGroupHeaderSettings)
  {
    string str1 = dynamicGroupHeaderSettings.MinRowsForDynamicHeaderGroup > 0 ? dynamicGroupHeaderSettings.MinRowsForDynamicHeaderGroup.ToString() : "";
    SpecificationSection.SetAttributeValuesToDocNode(docNode, "GroupHeaderRowCount", str1, true);
    string str2 = !this.IsFormB || this.AVSDocument.AvsDocumentForm != AVSDocumentForm.V ? "Заголовок группы записей" : "Заголовок группы записей формы Б";
    SpecificationSection.SetAttributeValuesToDocNode(docNode, "GroupHeaderTemplate", str2, true);
  }

  /// <summary>Метод сравнения записей</summary>
  /// <param name="x">Первая запись</param>
  /// <param name="y">Вторая запись</param>
  /// <returns>Результат сравнения.
  /// -1 означает x меньше y
  /// 0 означает x равно y
  /// 1 означает x больше y
  /// </returns>
  public int Compare(object x, object y) => this.Compare(x as AVSRow, y as AVSRow);

  /// <summary>Метод сравнения записей</summary>
  /// <param name="x">Первая запись</param>
  /// <param name="y">Вторая запись</param>
  /// <returns>Результат сравнения.
  /// -1 означает x меньше y
  /// 0 означает x равно y
  /// 1 означает x больше y
  /// </returns>
  public int Compare(AVSRow x, AVSRow y) => this.CompareCore(x, y);

  /// <summary>Метод сравнения записей</summary>
  /// <param name="x">Первая запись</param>
  /// <param name="y">Вторая запись</param>
  /// <param name="callChain">Цепочка вызовов. null, если не нужно отслеживать циклические ссылки в сортировке</param>
  /// <returns>Результат сравнения.
  /// -1 означает x меньше y
  /// 0 означает x равно y
  /// 1 означает x больше y
  /// </returns>
  private int CompareCore(AVSRow x, AVSRow y, List<AVSRow[]> callChain = null)
  {
    int num1 = x.IsDynamicGroupHeaderRow ? 1 : 0;
    int num2 = y.IsDynamicGroupHeaderRow ? 1 : 0;
    if (x == y)
      return 0;
    if (x == null)
      return -1;
    if (y == null)
      return 1;
    AVSRow rowX = x;
    AVSRow rowY = y;
    int num3 = 0;
    AVSRow sortBeforeRow1 = rowX.SortBeforeRow;
    AVSRow sortBeforeRow2 = rowY.SortBeforeRow;
    AVSRow avsRow1 = rowX.SortAfterRow;
    AVSRow avsRow2 = rowY.SortAfterRow;
    if (avsRow1 != null && avsRow1.SortBeforeRow == rowX)
      avsRow1 = (AVSRow) null;
    if (avsRow2 != null && avsRow2.SortBeforeRow == rowY)
      avsRow2 = (AVSRow) null;
    if (sortBeforeRow1 != null || avsRow1 != null || sortBeforeRow2 != null || avsRow2 != null)
    {
      if (sortBeforeRow1 == rowY)
        return -1;
      if (avsRow1 == rowY)
        return 1;
      if (avsRow2 == rowX)
        return -1;
      if (sortBeforeRow2 == rowX)
        return 1;
      rowX = sortBeforeRow1 ?? avsRow1 ?? rowX;
      rowY = sortBeforeRow2 ?? avsRow2 ?? rowY;
      if (callChain != null && callChain.Any<AVSRow[]>((Func<AVSRow[], bool>) (rows =>
      {
        if (rows[0] == rowX && rows[1] == rowY)
          return true;
        return rows[1] == rowX && rows[0] == rowY;
      })))
      {
        string format = "AVS. При сортировке записей обнаружено зацикливание ручной привязки записей '{0}' и '{1}' в документе [{2}] '{3}'";
        string message;
        if (!ImDocumentData.ShowDebugInfo)
          message = string.Format(format, (object) rowX, (object) rowY, (object) this.avsDocument.DocumentID, (object) this.avsDocument.DocumentCaption);
        else
          message = string.Format(format, (object) rowX.Caption, (object) rowY.Caption, (object) this.avsDocument.DocumentID, (object) this.avsDocument.DocumentCaption);
        throw new Exception(message);
      }
      List<AVSRow[]> callChain1 = callChain ?? new List<AVSRow[]>();
      callChain1.Add(new AVSRow[2]{ x, y });
      num3 = this.CompareCore(rowX, rowY, callChain1);
      int num4 = x.IsDynamicGroupHeaderRow ? 1 : 0;
      int num5 = y.IsDynamicGroupHeaderRow ? 1 : 0;
      if (num3 != 0)
        return num3;
      rowX = x;
      rowY = y;
    }
    bool flag1 = this.SectionInfo.SectionGuid == SpecificationSectionInfo.ComplectSectionGuid;
    bool flag2 = true;
    if (this.avsDocument != null && this.avsDocument.SortSchema != null)
      flag2 = this.avsDocument.SortSchema.SortDocumentsByType;
    if (flag2 && ((rowX.RelType != AvsIDCache.Relation_Document ? 0 : (rowY.RelType == AvsIDCache.Relation_Document ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
    {
      if (!flag1 && this.avsDocument != null)
      {
        string fieldStringValue1 = rowX.GetFieldStringValue(rowX.Field_Designation, 0, -1, (List<RelationAttributeValuesCache>) null, false);
        string fieldStringValue2 = rowY.GetFieldStringValue(rowY.Field_Designation, 0, -1, (List<RelationAttributeValuesCache>) null, false);
        bool flag3 = !string.IsNullOrEmpty(fieldStringValue1) && fieldStringValue1.Contains(this.avsDocument.DocumentDesignation);
        bool flag4 = !string.IsNullOrEmpty(fieldStringValue2) && fieldStringValue2.Contains(this.avsDocument.DocumentDesignation);
        if (flag3 != flag4)
          return flag3 ? -1 : 1;
      }
      long index1;
      long index2;
      if (rowX.RelType == AvsIDCache.Relation_Document && rowY.RelType == AvsIDCache.Relation_Document)
      {
        index1 = DocumentTypeWeightHelper.items.GetWeight(rowX.ObjType);
        index2 = DocumentTypeWeightHelper.items.GetWeight(rowY.ObjType);
      }
      else
      {
        string[] strArray = new string[7]
        {
          "комплект монтажных частей",
          "комплект сменных частей",
          "комплект запасных частей",
          "комплект инструмента и принадлежностей",
          "комплект укладочных средств",
          "комплект",
          "упаковка"
        };
        string lower1 = rowX.GetFieldStringValue(rowX.Field_Name, 0, -1, (List<RelationAttributeValuesCache>) null, false).ToLower();
        index1 = 0L;
        while (index1 < (long) strArray.Length && !lower1.Contains(strArray[index1]))
          ++index1;
        string lower2 = rowY.GetFieldStringValue(rowY.Field_Name, 0, -1, (List<RelationAttributeValuesCache>) null, false).ToLower();
        index2 = 0L;
        while (index2 < (long) strArray.Length && !lower2.Contains(strArray[index2]))
          ++index2;
      }
      if (index1 < index2)
        return -1;
      if (index1 > index2)
        return 1;
      num3 = 0;
    }
    if (this.SectionSortSchema != null)
    {
      for (int index = 0; index < this.SectionSortSchema.AttributeSortSchemas.Length; ++index)
      {
        AvsRowAttributeInfo attrInfo = this.SectionSortSchema.AttributeSortSchemas[index].GetAttrInfo();
        object fieldValue1 = rowX.GetFieldValue(attrInfo, 0, -1, true, false);
        object fieldValue2 = rowY.GetFieldValue(attrInfo, 0, -1, true, false);
        if (fieldValue1 != null && fieldValue2 != null && fieldValue1 is MeasuredValue || AVSRow.IsCountAttribute(attrInfo) || attrInfo.AttributeId == AvsIDCache.Attr_Weight || attrInfo.AttributeId == AvsIDCache.Attr_UnitWeight)
        {
          switch (MeasureHelper.Compare(AVSRow.ConvertCountToMeasuredValue(fieldValue1), AVSRow.ConvertCountToMeasuredValue(fieldValue2)))
          {
            case CompareResult.Equal:
            case CompareResult.NotCompatible:
              num3 = 0;
              break;
            case CompareResult.More:
              num3 = 1;
              break;
            case CompareResult.Less:
              num3 = -1;
              break;
          }
        }
        else
        {
          string strX = Convert.ToString(fieldValue1);
          string strY = Convert.ToString(fieldValue2);
          num3 = this.SectionSortSchema.AttributeSortSchemas[index].Compare(strX, strY);
        }
        if (num3 != 0)
          return num3;
      }
    }
    else if (this.avsDocument == null || this.avsDocument.SortSchema == null)
    {
      num3 = Convert.ToString(rowX.GetFieldValue(rowX.Field_Name, 0, -1, true, false)).CompareTo(Convert.ToString(rowY.GetFieldValue(rowY.Field_Name, 0, -1, true, false)));
      if (num3 != 0)
        return num3;
    }
    else
    {
      int num6 = -1;
      if (rowX.Section != null)
        num6 = rowX.Section.Rows.IndexOf(rowX);
      int num7 = -1;
      if (rowY.Section != null)
        num7 = rowY.Section.Rows.IndexOf(rowY);
      int num8 = num6.CompareTo(num7);
      return num8 != 0 ? num8 : rowX.GetHashCode().CompareTo(rowY.GetHashCode());
    }
    if (rowX.DocNode != null && rowY.DocNode != null)
    {
      DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) rowX.DocNode;
      DocumentTreeNode documentTreeNode2 = (DocumentTreeNode) rowY.DocNode;
      for (int index3 = 0; index3 < x.DocNodes.Count; ++index3)
      {
        TableData firstTable1 = x.DocNodes[index3].TopLevelTable.FindFirstTable();
        for (int index4 = 0; index4 < y.DocNodes.Count; ++index4)
        {
          TableData firstTable2 = y.DocNodes[index4].TopLevelTable.FindFirstTable();
          if (firstTable1 == firstTable2)
          {
            documentTreeNode1 = (DocumentTreeNode) x.DocNodes[index3];
            documentTreeNode2 = (DocumentTreeNode) y.DocNodes[index4];
            break;
          }
        }
      }
      for (; documentTreeNode1.Parent != documentTreeNode2.Parent && documentTreeNode1.Parent != null && documentTreeNode2.Parent != null; documentTreeNode2 = documentTreeNode2.Parent)
        documentTreeNode1 = documentTreeNode1.Parent;
      if (documentTreeNode1.Parent != null && documentTreeNode2.Parent != null)
        return documentTreeNode1.Index.CompareTo(documentTreeNode2.Index);
      if (documentTreeNode1.Parent != null)
        return -1;
      if (documentTreeNode2.Parent != null)
        return 1;
    }
    else
    {
      if (rowX.DocNode != null)
        return -1;
      if (rowY.DocNode != null)
        return 1;
    }
    if (rowX.IsFreeSortIndex == rowY.IsFreeSortIndex)
      num3 = 0;
    else if (!rowX.IsFreeSortIndex && !rowY.IsFreeSortIndex)
    {
      num3 = rowX.SortIndex.CompareTo(rowY.SortIndex);
    }
    else
    {
      if (rowX.IsFreeSortIndex)
        return 1;
      if (rowY.IsFreeSortIndex)
        return -1;
    }
    return num3 != 0 ? num3 : (!rowX.HasRelation ? (rowX.ObjectId == -1L ? (long) rowX.GetHashCode() : Math.Abs(rowX.ObjectId)) : Math.Abs(rowX.Relations[0].RelationId)).CompareTo(!rowY.HasRelation ? (rowY.ObjectId == -1L ? (long) rowY.GetHashCode() : Math.Abs(rowY.ObjectId)) : Math.Abs(rowY.Relations[0].RelationId));
  }

  /// <summary>Получить IEnumerator для сквозного цикла по записям спецификации всех внутренних подразделов</summary>
  public override IEnumerator<AVSRow> GetEnumerator()
  {
    return (IEnumerator<AVSRow>) new SectionRowEnumerator(this);
  }
}
