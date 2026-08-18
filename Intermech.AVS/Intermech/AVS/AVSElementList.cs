// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSElementList
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Output;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Intermech.AVS;

public class AVSElementList : AVSDocument
{
  /// <summary>Вставить исполнения</summary>
  /// <param name="newProductParamsList">Список с параметрами новых исполнений</param>
  /// <param name="updateViewNodes">Обновить узлы документа</param>
  public override void InsertNewProducts(
    IList<NewProductParams> newProductParamsList,
    bool updateViewNodes = true)
  {
    for (int index = 0; index < newProductParamsList.Count; ++index)
    {
      ProductInfo newProductInfo = this.CreateNewProductInfo(newProductParamsList[index]);
      if (newProductInfo.Id.IsDefinedId())
        newProductInfo.UpdateInfo(this.productAttributeList, (string) null);
      if (newProductParamsList[index].ProductIndex < 0)
        newProductParamsList[index].ProductIndex = this.productsInfo.Count;
      this.productsInfo.Insert(newProductParamsList[index].ProductIndex, newProductInfo);
      if (this.AvsDocumentForm == AVSDocumentForm.A)
        this.AddProductChapters_FormA(newProductParamsList[index]);
      else
        this.LoadProductData(newProductInfo);
    }
    if (!updateViewNodes)
      return;
    this.UpdateViewNodes(false, false, false, true, true, EmptyRowUpdateMode.DontChange);
  }

  private ProductInfo CreateNewProductInfo(NewProductParams newProductParams)
  {
    string name = this.DocumentName;
    if (newProductParams.SrcProductIndex != -1)
    {
      ProductInfo productInfo = this.productsInfo[newProductParams.SrcProductIndex];
      if (!string.IsNullOrEmpty(productInfo.Name))
        name = productInfo.Name;
    }
    Guid guid = Guid.Empty;
    if (newProductParams.ProductID == -1L)
      guid = Guid.NewGuid();
    return new ProductInfo(guid, newProductParams.ProductID, name, newProductParams.ProductDesignation);
  }

  private void AddProductChapters_FormA(NewProductParams newProductParams)
  {
    this.variableDataChapter_FormA.AddProduct(newProductParams);
    for (int index = 0; index < this.rootChapters.Count; ++index)
    {
      if (this.rootChapters[index] is AdditionalChapter rootChapter)
        rootChapter.InnerVariableData_FormA.AddProduct(newProductParams);
    }
  }

  /// <summary>Получить список типов связей по которым загружаются данные для записей</summary>
  /// <returns></returns>
  internal override List<int> GetRelationTypesUsedInDocument()
  {
    return new List<int>()
    {
      AvsIDCache.Relation_Project,
      AvsIDCache.Relation_Podbor
    };
  }

  /// <summary>Проверить наличие базовых таблиц в документе и выбросить исключение, если их нет</summary>
  protected override void CheckMainDocumentTablesAndThrowException()
  {
    if (this.avsDocTable == null && this.avsFormB_Table == null)
      throw new Exception($"Нарушена структура документа! В перечне элементов \"{this.DocumentCaption}\" не найдена таблица \"Перечень элементов\".");
  }

  /// <summary>Объект хранящий настройки графы "Примечание"</summary>
  internal override long NoteFieldSettingsObjectID => this.AVSDocumentTemplateID;

  /// <summary>Атрибут который отображается в графе "Количество"</summary>
  /// <param name="attribute">Информация об атрибуте</param>
  /// <returns></returns>
  internal override bool IsNoteField(AvsRowAttributeInfo attribute)
  {
    if (attribute == null)
      return false;
    if (base.IsNoteField(attribute) || attribute.IsRelationAttribute && (attribute.AttributeId == AvsIDCache.Attr_Note || attribute.AttributeId == AvsIDCache.Attr_NotePE))
      return true;
    if (!attribute.IsDocField)
      return false;
    return attribute.Name == AVSRow.DocAttr_Note || attribute.Name == AVSRow.DocAttr_NotePE;
  }

  /// <summary>Выводить символ «*» рядом с Позиционным обозначением основного компонента</summary>
  internal override bool InsertStarAfterPositionDesignation
  {
    get => AvsConfig.Podbor.InsertStarAfterPositionDesignationInPE;
  }

  public override void SumPositionalDesignation()
  {
    foreach (AVSRow allRow in this.GetAllRows(true, false))
      this.SplitRelationsByDiscontinuityInPosDesignations(allRow);
    base.SumPositionalDesignation();
    this.UpdateViewNodes(false, false, false, false, false, EmptyRowUpdateMode.DontChange);
  }

  private void SplitRelationsByDiscontinuityInPosDesignations(AVSRow row)
  {
    if (!row.HasRelation)
      return;
    List<AVSElementList.RelationWithPosDesignations> list = row.GetAllRelations().GroupBy<RelationAttributeValuesCache, string>((Func<RelationAttributeValuesCache, string>) (r => r.GetValueString(this.Field_PosDesignation, false))).Select<IGrouping<string, RelationAttributeValuesCache>, AVSElementList.RelationWithPosDesignations>((Func<IGrouping<string, RelationAttributeValuesCache>, AVSElementList.RelationWithPosDesignations>) (group => new AVSElementList.RelationWithPosDesignations((IEnumerable<RelationAttributeValuesCache>) group, PosDesignationRecord.ParsePositionalDesignation(group.Key)))).ToList<AVSElementList.RelationWithPosDesignations>();
    if (list.Count < 2)
      return;
    list.Sort();
    List<RelationAttributeValuesCache> relations = new List<RelationAttributeValuesCache>();
    bool flag = true;
    AVSElementList.RelationWithPosDesignations withPosDesignations1 = list.First<AVSElementList.RelationWithPosDesignations>();
    foreach (AVSElementList.RelationWithPosDesignations withPosDesignations2 in list.Skip<AVSElementList.RelationWithPosDesignations>(1))
    {
      if (!AVSDocument.IsContinuousSequencePosDesignation(withPosDesignations1.LastPosDesignation, withPosDesignations2.FirstPosDesignation))
      {
        if (flag)
        {
          flag = false;
        }
        else
        {
          AVSRow newAvsRow = this.MoveRelationsToNewAvsRow(row, (IEnumerable<RelationAttributeValuesCache>) relations);
          row.Section.AddRow(newAvsRow, true);
          relations.Clear();
        }
      }
      if (!flag)
        relations.AddRange((IEnumerable<RelationAttributeValuesCache>) withPosDesignations2.Relations);
      withPosDesignations1 = withPosDesignations2;
    }
    if (relations.Count <= 0)
      return;
    AVSRow newAvsRow1 = this.MoveRelationsToNewAvsRow(row, (IEnumerable<RelationAttributeValuesCache>) relations);
    row.Section.AddRow(newAvsRow1, true);
  }

  internal override bool CanSummThisRelations(
    AvsRowData rowData1,
    AvsRowData rowData2,
    CellOutputMapping noteCellMapping)
  {
    if (!base.CanSummThisRelations(rowData1, rowData2, noteCellMapping) || rowData1.RelationType == AvsIDCache.Relation_Podbor)
      return false;
    string fieldStringValue1 = rowData1.GetFieldStringValue(this.Attr_FunctionalGroupDesignation, true);
    string fieldStringValue2 = rowData1.GetFieldStringValue(this.Attr_FunctionalGroupName, true);
    string fieldStringValue3 = rowData2.GetFieldStringValue(this.Attr_FunctionalGroupDesignation, true);
    string fieldStringValue4 = rowData2.GetFieldStringValue(this.Attr_FunctionalGroupName, true);
    string str = fieldStringValue3;
    if (fieldStringValue1 != str || fieldStringValue2 != fieldStringValue4)
      return false;
    bool fieldBoolValue1 = rowData1.GetFieldBoolValue(this.Attr_Podbor, false);
    bool fieldBoolValue2 = rowData2.GetFieldBoolValue(this.Attr_Podbor, false);
    return fieldBoolValue1 == fieldBoolValue2 && (!fieldBoolValue1 || !(rowData1.GetFieldStringValue(this.Attr_LimitValues, false) != rowData2.GetFieldStringValue(this.Attr_LimitValues, false)));
  }

  protected override bool AllowIncludeRelationInDocument(RelationAttributeValuesCache relation)
  {
    return relation != null ? base.AllowIncludeRelationInDocument(relation) : throw new ArgumentNullException(nameof (relation));
  }

  /// <summary>Атрибут отвечающий за примечание в документе</summary>
  [Browsable(false)]
  public override AvsRowAttributeInfo Attr_Note
  {
    get => this._attr_Note ?? (this._attr_Note = AvsIDCache.StdField_NotePE.Clone());
  }

  protected override void RemoveDocumentTypeSuffixFromProductDesignations()
  {
    if (this.AvsDocumentForm != AVSDocumentForm.A || this.ProductsInfo.Count <= 1 || this.ProductsInfo[0].Id != -1L)
      return;
    bool flag = false;
    foreach (ProductInfo productInfo in this.ProductsInfo)
    {
      if (productInfo.Designation.Contains(this.DocumentDesignationSuffix))
      {
        flag = true;
        int startIndex = productInfo.Designation.LastIndexOf(this.DocumentDesignationSuffix);
        productInfo.Designation = productInfo.Designation.Remove(startIndex, this.DocumentDesignationSuffix.Length);
      }
    }
    if (!flag)
      return;
    this.UpdateProductHeadersOnPages(true, true);
  }

  public override string BaseProductDesignation
  {
    get => base.BaseProductDesignation;
    set
    {
      if (!(this.BaseProductDesignation != value))
        return;
      base.BaseProductDesignation = value;
      if (this.AvsDocumentForm != AVSDocumentForm.A || this.ProductsInfo.Count <= 1 || this.ProductsInfo[0].Id != -1L)
        return;
      foreach (ProductInfo productInfo in this.ProductsInfo)
        ;
    }
  }

  /// <summary>Обновить обозначения исполнений для документов с виртуальными исполнениями</summary>
  protected override void UpdateVirtualProductsDesignations()
  {
    if (this.Document == null || string.IsNullOrEmpty(this.Document.LoadedFromXMLDesignation) || !(this.Document.LoadedFromXMLDesignation != this.DocumentDesignation))
      return;
    string productDesignation = AVSDocument.FindProductDesignation(this.Document.LoadedFromXMLDesignation, this.DocumentDesignationSuffix);
    foreach (ProductInfo product in this.ProductsInfo)
      this.UpdateProductsDesignations(product, productDesignation, this.BaseProductDesignation, (IUserSession) null);
  }

  public static void PatchFieldReferences(ImDocument documentTemplate)
  {
    if (documentTemplate == null)
      throw new ArgumentNullException(nameof (documentTemplate));
    if (!documentTemplate.IsTemplate)
      throw new ArgumentException("Аргумент должен быть шаблоном документа!", nameof (documentTemplate));
    if (!(documentTemplate.FindNode(AVSRow.DocAttr_PosDesignation) is TextData node) || node.ReferenceToTextSource is ReferenceToDBObjectAttribute)
      return;
    ReferenceToDBObjectAttribute dbObjectAttribute = new ReferenceToDBObjectAttribute((DocumentTreeNode) node, RefToDBObjectType.rtUseParentRelationLink, (DBObjectInfoBase) new DBRelationInfo(Guid.Empty, Guid.Empty), new Guid("cad01478-306c-11d8-b4e9-00304f19f545"), -1, "Позиционное обозначение", true);
    node.AssignReferenceToTextSource((ReferenceBase) dbObjectAttribute, false, false, false);
  }

  internal void UpdateFunctionalGroups()
  {
    this.Document?.SuspendUpdateLayout();
    try
    {
      AVSElementList.FunctionalGroup[] functionalGroupStructure = AVSElementList.CreateFunctionalGroupStructure(this.CollectRowsByFunctionalGroups());
      this.CollectFunctionalGroupPosDesignation((IList<AVSElementList.FunctionalGroup>) functionalGroupStructure);
      this.UpdateFunctionalGroupHeader((IList<AVSElementList.FunctionalGroup>) functionalGroupStructure);
    }
    finally
    {
      this.Document?.ResumeUpdateLayout(true, true);
    }
  }

  /// <summary>Собрать данные о позиционных группах в специальные структуры</summary>
  /// <returns></returns>
  private static AVSElementList.FunctionalGroup[] CreateFunctionalGroupStructure(
    Dictionary<string, List<AVSRow>> functionalGroupsDict)
  {
    AVSElementList.FunctionalGroup[] array = functionalGroupsDict.Select<KeyValuePair<string, List<AVSRow>>, AVSElementList.FunctionalGroup>((Func<KeyValuePair<string, List<AVSRow>>, AVSElementList.FunctionalGroup>) (fgEntry => new AVSElementList.FunctionalGroup()
    {
      Title = fgEntry.Key,
      Rows = fgEntry.Value
    })).ToArray<AVSElementList.FunctionalGroup>();
    foreach (AVSElementList.FunctionalGroup functionalGroup in array)
    {
      AVSRow avsRow = functionalGroup.Rows.FirstOrDefault<AVSRow>();
      if (avsRow != null && avsRow.IsFunctionalGroupHeaderRow)
      {
        functionalGroup.HeaderRow = avsRow;
        functionalGroup.Rows.Remove(avsRow);
      }
    }
    return array;
  }

  /// <summary>Обновить или создать заголовок функциональной группы</summary>
  /// <param name="functionalGroups"></param>
  private void UpdateFunctionalGroupHeader(
    IList<AVSElementList.FunctionalGroup> functionalGroups)
  {
    foreach (AVSElementList.FunctionalGroup functionalGroup in (IEnumerable<AVSElementList.FunctionalGroup>) functionalGroups)
    {
      if (functionalGroup.Rows.Count == 0)
      {
        if (functionalGroup.HeaderRow != null)
          functionalGroup.HeaderRow.Remove();
      }
      else
      {
        AVSRow avsRow = functionalGroup.Rows.FirstOrDefault<AVSRow>();
        if (functionalGroup.HeaderRow == null)
        {
          int index = avsRow.Index;
          if (index > 0)
          {
            AVSRow row = avsRow.Section.Rows[index - 1];
            if (row.IsFunctionalGroupHeaderRow)
              functionalGroup.HeaderRow = row;
          }
        }
        if (functionalGroup.HeaderRow == null)
          functionalGroup.HeaderRow = this.CreateNewFunctionalGroupHeader(functionalGroup);
        functionalGroup.HeaderRow.SetFieldValueInDocRowsCell(this.Field_Name, (TableData) null, -1, functionalGroup.Title);
        functionalGroup.HeaderRow.SetFieldValueInDocRowsCell(this.Field_PosDesignation, (TableData) null, -1, functionalGroup.PositionalDesignation);
        functionalGroup.HeaderRow.SortBeforeRow = avsRow;
        int count = PosDesignationRecord.ParsePositionalDesignation(functionalGroup.PositionalDesignation).Count;
        functionalGroup.HeaderRow.SetFieldValueInDocRowsCell(this.Field_Count, (TableData) null, -1, count.ToString());
      }
    }
  }

  private AVSRow CreateNewFunctionalGroupHeader(AVSElementList.FunctionalGroup fg)
  {
    AVSRow row = new AVSRow((AVSDocument) this);
    row.IsNoteRow = true;
    TableData noteDocRow = this.CreateNoteDocRow(this.functionalGroupHeaderTemplate, fg.Title);
    noteDocRow.SetKeepWithNext(true, false, false);
    AVSRow avsRow = fg.Rows.FirstOrDefault<AVSRow>();
    if (avsRow.HasDocNodes)
      avsRow.DocNode.ParentCell.InsertChildNode(avsRow.DocNode.Index, (DocumentTreeNode) noteDocRow, false, false, false, false, true);
    row.AddDocNode(noteDocRow);
    row.IsFunctionalGroupHeaderRow = true;
    row.SkipLinesAfter = new int?(1);
    avsRow.Section.InsertRow(avsRow.Index, row);
    row.SortBeforeRow = avsRow;
    return row;
  }

  /// <summary>
  /// Собрать позиционные обозначения функциональных групп по всем связям записей в списке групп
  /// </summary>
  private void CollectFunctionalGroupPosDesignation(
    IList<AVSElementList.FunctionalGroup> functionalGroups)
  {
    foreach (AVSElementList.FunctionalGroup functionalGroup in (IEnumerable<AVSElementList.FunctionalGroup>) functionalGroups)
    {
      List<string> posDesignations = new List<string>();
      foreach (AVSRow row in functionalGroup.Rows)
      {
        if (row.HasRelation)
          posDesignations.AddRange(row.AllRelations.Select<RelationAttributeValuesCache, string>((Func<RelationAttributeValuesCache, string>) (r => r.GetValueString(this.Attr_FunctionalGroupPosDesignation, false))));
        else
          posDesignations.Add(row.GetFieldStringValue(this.Attr_FunctionalGroupPosDesignation, -1, -1, (List<RelationAttributeValuesCache>) null, false));
      }
      functionalGroup.PositionalDesignation = PosDesignationHelper.Summ(posDesignations);
    }
  }

  /// <summary>Собрать словарь записей по функциональным группам</summary>
  private Dictionary<string, List<AVSRow>> CollectRowsByFunctionalGroups()
  {
    Dictionary<string, List<AVSRow>> dict = new Dictionary<string, List<AVSRow>>();
    this.GetRows().Where<AVSRow>((Func<AVSRow, bool>) (r => !r.IsDynamicGroupHeaderRow && !r.IsFunctionalGroupHeaderRow));
    foreach (AVSRow allRow in this.GetAllRows(false, false))
    {
      if (!allRow.IsDynamicGroupHeaderRow)
      {
        string key;
        if (allRow.IsFunctionalGroupHeaderRow)
          key = allRow.GetFieldStringValue(this.Field_Name, -1, -1, (List<RelationAttributeValuesCache>) null, false);
        else
          key = AVSRow.JoinWithoutEmptyValues(" ", allRow.GetFieldStringValue(this.Attr_FunctionalGroupName, -1, -1, (List<RelationAttributeValuesCache>) null, false), allRow.GetFieldStringValue(this.Attr_FunctionalGroupDesignation, -1, -1, (List<RelationAttributeValuesCache>) null, false));
        if (!string.IsNullOrEmpty(key))
          AddRowToDictionary(dict, key, allRow);
      }
    }
    return dict;

    static void AddRowToDictionary(
      Dictionary<string, List<AVSRow>> dict,
      string key,
      AVSRow newRow)
    {
      List<AVSRow> avsRowList;
      if (!dict.TryGetValue(key, out avsRowList))
      {
        avsRowList = new List<AVSRow>();
        dict.Add(key, avsRowList);
      }
      avsRowList.Add(newRow);
    }
  }

  public override void UpdateViewNodes(
    bool reCreateDocNode,
    bool reCreateListNode,
    bool updateCountB,
    bool createForEmptyChapters,
    bool updateTemplate,
    EmptyRowUpdateMode updateMode)
  {
    this.SuspendDocumentAndGridUpdates();
    try
    {
      base.UpdateViewNodes(reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
      this.UpdateFunctionalGroups();
    }
    finally
    {
      this.ResumeDocumentAndGridUpdates(0, true, this.Document.NeedUpdateLayoutFlag, true, true);
    }
  }

  internal override void LoadNoteFieldSettings()
  {
    base.LoadNoteFieldSettings();
    this._attr_Note = (AvsRowAttributeInfo) null;
    if (this.NewCellMappingMode)
    {
      CellOutputMapping cellMapping = this.CellTextOutputAttributeMappingSettings.GetCellMapping("00000000-0000-0000-0000-000000000000", AVSRow.DocAttr_Note, "00000000-0000-0000-0000-000000000000");
      if (cellMapping != null && cellMapping.ContainsAttribute((AttributeInfo) AvsIDCache.StdField_NotePE))
        this._attr_Note = AvsIDCache.StdField_NotePE.Clone();
    }
    if (this._attr_Note != null)
      return;
    if (this.noteFieldSettings.Items.Contains<RemarkAttribute>((Predicate<RemarkAttribute>) (f => f.ID == AvsIDCache.Attr_NotePE && f.AttrSource == AttributeSourceTypes.Relation)))
      this._attr_Note = AvsIDCache.StdField_NotePE.Clone();
    else
      this._attr_Note = AvsIDCache.StdField_Note.Clone();
    this.Field_Note = this._attr_Note;
  }

  public static List<AvsRowAttributeInfo> GetVirtualAttributeListForEL()
  {
    List<AvsRowAttributeInfo> attributeListForDocument = AVSDocument.GetVirtualAttributeListForDocument();
    attributeListForDocument.Add(AvsIDCache.Attr_NominalAndLimitValues_NoteText);
    return attributeListForDocument;
  }

  public override List<AvsRowAttributeInfo> GetVirtualAttributeList()
  {
    return AVSElementList.GetVirtualAttributeListForEL();
  }

  public override void CheckErrorsInRows(
    AVSCheckType checkType,
    AVSCheckMode checkMode,
    ICollection<AVSRow> avsRows,
    Dictionary<AVSRow, List<SpecRowCheckMessage>> errorRows)
  {
    base.CheckErrorsInRows(checkType, checkMode, avsRows, errorRows);
    if (checkType == AVSCheckType.None || checkMode == AVSCheckMode.ChangeForm)
      return;
    if (avsRows == null)
      avsRows = (ICollection<AVSRow>) this.GetAllRows(false, true);
    if (avsRows.Count == 0)
      return;
    if (this.DocumentControl != null)
    {
      CancelEventArgs cancelArgs = new CancelEventArgs();
      this.DocumentControl.EditorValidating(cancelArgs);
      if (cancelArgs.Cancel)
        return;
    }
    object obj1 = SpecificationSectionInfo.SectionDictionaryByGuid[(object) new Guid("cad0025d-306c-11d8-b4e9-00304f19f545")];
    int relationDocument = AvsIDCache.Relation_Document;
    List<SpecRowCheckMessage> specRowCheckMessageList = new List<SpecRowCheckMessage>();
    bool flag1 = (checkType & AVSCheckType.EmptyPositionDesignation) != 0;
    bool flag2 = (checkType & AVSCheckType.EmptyCount) != 0;
    foreach (AVSRow key in avsRows.Where<AVSRow>((Func<AVSRow, bool>) (r => !r.IsHiddenRow && !r.IsDocRelation)))
    {
      int num = !errorRows.TryGetValue(key, out specRowCheckMessageList) ? 1 : 0;
      if (num != 0)
        specRowCheckMessageList = new List<SpecRowCheckMessage>();
      if (flag1)
      {
        string fieldStringValue = key.GetFieldStringValue(this.Field_PosDesignation, -1, -1, (List<RelationAttributeValuesCache>) null, false);
        object obj2 = SpecificationSectionInfo.SectionDictionaryByID[(object) key.SectionID];
        if (string.IsNullOrWhiteSpace(fieldStringValue))
          specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.EmptyPositionDesignation, (string) null));
      }
      if (flag2 && key.GetFieldValue(this.Field_Count, 0, -1, true, false) == null)
        specRowCheckMessageList.Add(new SpecRowCheckMessage(AVSCheckType.EmptyCount, (string) null));
      if (num != 0 && specRowCheckMessageList.Count > 0)
        errorRows.Add(key, specRowCheckMessageList);
    }
  }

  private class RelationWithPosDesignations : IComparable<AVSElementList.RelationWithPosDesignations>
  {
    public readonly List<PosDesignationRecord> PosDesignationList;
    public readonly List<RelationAttributeValuesCache> Relations;

    public RelationWithPosDesignations(
      IEnumerable<RelationAttributeValuesCache> relations,
      List<PosDesignationRecord> posDesignations)
    {
      if (relations == null)
        throw new ArgumentNullException("relation");
      if (posDesignations == null)
        throw new ArgumentNullException(nameof (posDesignations));
      this.Relations = relations.ToList<RelationAttributeValuesCache>();
      this.PosDesignationList = posDesignations;
      this.PosDesignationList.Sort();
    }

    /// <summary>Закрытый конструктор</summary>
    private RelationWithPosDesignations()
    {
    }

    public PosDesignationRecord FirstPosDesignation
    {
      get => this.PosDesignationList.FirstOrDefault<PosDesignationRecord>();
    }

    public PosDesignationRecord LastPosDesignation
    {
      get => this.PosDesignationList.LastOrDefault<PosDesignationRecord>();
    }

    public int CompareTo(AVSElementList.RelationWithPosDesignations other)
    {
      return this.PosDesignationList.Count == 0 ? (other.PosDesignationList.Count <= 0 ? 0 : -1) : (other.PosDesignationList.Count == 0 ? 1 : this.PosDesignationList.First<PosDesignationRecord>().CompareTo((object) other.PosDesignationList.First<PosDesignationRecord>()));
    }
  }

  private class FunctionalGroup
  {
    public string Title;
    public string PositionalDesignation;
    public AVSRow HeaderRow;
    public List<AVSRow> Rows;
  }
}
