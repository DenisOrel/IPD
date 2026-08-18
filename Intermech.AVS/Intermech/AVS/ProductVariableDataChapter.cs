// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ProductVariableDataChapter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.Client.Core;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

#nullable disable
namespace Intermech.AVS;

/// <summary>Данные исполнения в разделе переменных данных группового документа формы А. </summary>
public class ProductVariableDataChapter : Chapter
{
  private List<ProductVariableDataChapter> sameProducts;
  private List<ProductVariableDataChapter.CaptionAttribute> сaptionAttributes;

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  /// <param name="productInfo">Информация об исполнении</param>
  /// <param name="sortIndex">Индекс сортировки</param>
  /// <param name="isSectionOwner">Подраздел является владельцем разделов СП</param>
  public ProductVariableDataChapter(
    AVSDocument avsDocument,
    ProductInfo productInfo,
    long sortIndex,
    bool isSectionOwner)
    : base(avsDocument, isSectionOwner)
  {
    if (productInfo == null)
      throw new ArgumentNullException(nameof (productInfo));
    this.NodeLevel = Chapter.ProductVariableData_TypeName;
    this.ChapterID = productInfo.Id;
    this.SortIndex = sortIndex;
    this.ChapterGuid = productInfo.Guid;
    if (avsDocument != null)
      this.ChapterType = avsDocument.ProductType;
    this.Caption = productInfo.Designation;
    this.product = productInfo;
    if (avsDocument == null || avsDocument.IsSpecification)
      return;
    this.Chapters.Add((Chapter) new SpecificationSection(avsDocument, new SpecificationSectionInfo(AVSDocument.ChapterVariableDataRowsGuid, -1L, -1, "Записи исполнения", 0L, "", new int[0], new long[0])));
    this.Chapters[0].Parent = (Chapter) this;
    this.chaptersGuidDictionary.Add(this.Chapters[0].ChapterGuid, this.Chapters[0]);
  }

  /// <summary>Проверить одинаковая ли Литера у всех исполнений</summary>
  /// <param name="spec">Конструкторский документ</param>
  /// <returns>true, если Литера у всех исполнений одинаковая</returns>
  public static bool SameLiters(AVSDocument spec)
  {
    if (spec.productsInfo == null || spec.productsInfo.Count < 2)
      return true;
    string litera = spec.productsInfo[0].Litera;
    for (int index = 1; index < spec.productsInfo.Count; ++index)
    {
      if (spec.productsInfo[index].Litera != litera)
        return false;
    }
    return true;
  }

  /// <summary>Проверить одинаковый ли Код ОКП у всех исполнений</summary>
  /// <param name="spec">Конструкторский документ</param>
  /// <returns>true, если Код ОКП у всех исполнений одинаковый</returns>
  public static bool SameOKPCodes(AVSDocument spec)
  {
    if (spec.productsInfo == null || spec.productsInfo.Count < 2)
      return true;
    string productOkpCode = spec.productsInfo[0].ProductOKPCode;
    for (int index = 1; index < spec.productsInfo.Count; ++index)
    {
      if (spec.productsInfo[index].ProductOKPCode != productOkpCode)
        return false;
    }
    return true;
  }

  /// <summary>Список атрибутов для формирования заголовка исполнения</summary>
  private List<ProductVariableDataChapter.CaptionAttribute> CaptionAttributes
  {
    get => this.сaptionAttributes;
  }

  /// <summary>Узлы документа представляющие этот раздел</summary>
  public override List<TableData> DocNodes
  {
    get => base.DocNodes;
    set
    {
      base.DocNodes = value;
      if (this.DocNodes.Count <= 0 || this.Product == null)
        return;
      this.DocNodes[0].SetAttributeValue(Chapter.Designation_AttributeName, this.Product.Designation);
    }
  }

  /// <summary>Узлы документа представляющие этот раздел</summary>
  public override List<TableData> DocNodesExp
  {
    get => base.DocNodesExp;
    set
    {
      base.DocNodesExp = value;
      if (this.Product == null)
        return;
      for (int index = 0; index < this.DocNodesExp.Count; ++index)
        this.DocNodesExp[index].SetAttributeValue(Chapter.Designation_AttributeName, this.Product.Designation);
    }
  }

  /// <summary>Обработка событий от NotificationService</summary>
  public void UpdateNotificationData(NotificationEventArgs e)
  {
    if (!(e is DBObjectsExtendedEventArgs extendedEventArgs) || !MetaDataHelper.IsObjectTypeChildOf(extendedEventArgs.ObjectType, AvsIDCache.ObjType_ConstructorDocumentTemplate) && (extendedEventArgs.ObjectIDs.Count <= 0 || !extendedEventArgs.ObjectIDs.Contains(this.ProductId)))
      return;
    this.UpdateCaptionAttributes();
    this.UpdateChapterCaption();
    if (this.DocNode == null)
      return;
    this.DocNode.UpdateLayout(true);
  }

  /// <summary>Список исполнений одинаковых по составу с данным</summary>
  internal List<ProductVariableDataChapter> SameProducts
  {
    get => this.sameProducts;
    set => this.sameProducts = value;
  }

  /// <summary>
  /// Первое исполнение которое по составу одинаково с данным
  /// </summary>
  internal ProductVariableDataChapter FirstSameProduct
  {
    get
    {
      if (this.SameProducts == null || this.SameProducts.Count < 2)
        return (ProductVariableDataChapter) null;
      return this.SameProducts[0] == this ? (ProductVariableDataChapter) null : this.SameProducts[0];
    }
  }

  /// <summary>
  /// Строка состава исполнения, формируется на основе полей всех строк
  /// </summary>
  /// <returns></returns>
  internal string GetChapterCode()
  {
    List<AVSRow> rowList = new List<AVSRow>();
    string chapterCode = "";
    foreach (RectangleElement docNode in this.DocNodes)
    {
      for (RectangleElement rectangleElement = docNode; rectangleElement != null; rectangleElement = rectangleElement.NextCell)
      {
        if (rectangleElement.Page.IsAdditionalPage)
          return chapterCode;
      }
    }
    foreach (Chapter allChapter in this.GetAllChapters())
      chapterCode = $"{chapterCode}{allChapter.Caption.ToString()};";
    this.GetAllRowsList(false, false, rowList);
    foreach (AVSRow avsRow in rowList)
    {
      if (!avsRow.IsDynamicGroupHeaderRow)
      {
        chapterCode = $"{chapterCode}{avsRow.ObjectId.ToString()};";
        if (avsRow.DocNode != null)
        {
          foreach (AvsRowAttributeInfo docRowField in this.DocRowFields)
          {
            string str = !this.avsDocument.Field_Name.EqualAttrs(docRowField, false) || avsRow.DocNode == null || !avsRow.HasDynamicGroupHeader ? (!this.avsDocument.Field_Designation.EqualAttrs(docRowField, false) ? avsRow.GetFieldValueFromDocCell(avsRow.DocNode, docRowField, 0) : avsRow.GetFieldStringValue(docRowField, -1, -1, (List<RelationAttributeValuesCache>) null, false)) : avsRow.DocNode.GroupCellOriginalText;
            chapterCode = $"{chapterCode}{str};";
          }
        }
        chapterCode += Environment.NewLine;
      }
    }
    return chapterCode;
  }

  /// <summary>Обновление кэша атрибутов</summary>
  internal void UpdateCaptionAttributes()
  {
    VersionAttributesHelper attributesHelper = this.AVSDocument.versionAttributesHelper;
    this.сaptionAttributes = new List<ProductVariableDataChapter.CaptionAttribute>();
    if (this.ProductId == -1L && !this.AVSDocument.IsElementList)
      return;
    for (int index = 0; index < attributesHelper.Items.Count; ++index)
    {
      if (this.Product.HasAttribute(attributesHelper.Items[index].ID))
        this.сaptionAttributes.Add(new ProductVariableDataChapter.CaptionAttribute(this.Product.GetAttributeValue(attributesHelper.Items[index].ID), (long) attributesHelper.Items[index].ID, attributesHelper.Items[index].Separator));
    }
  }

  /// <summary>Убрать выделение с "Отсутствуют" и списка атрибутов</summary>
  private void UpdateEmptyCaption()
  {
    if (this.DocNode == null)
      return;
    this.DocNode.SetName(this.Caption, false, false);
    DocumentTreeNode chapterCaptionRow = (DocumentTreeNode) this.GetChapterCaptionRow();
    TextBoxElement owner = (TextBoxElement) null;
    if (chapterCaptionRow != null && chapterCaptionRow.Nodes != null && chapterCaptionRow.Nodes.Count > 0)
    {
      for (int index = 0; index < chapterCaptionRow.Nodes.Count; ++index)
      {
        if (chapterCaptionRow.Nodes[index].Name == "Наименование")
        {
          owner = chapterCaptionRow.Nodes[index] as TextBoxElement;
          break;
        }
      }
    }
    if (owner == null)
      return;
    owner.SetRtfText((string) null, false, false);
    ImRtfEditor specificationEditor = this.avsDocument.SpecificationEditor;
    if (specificationEditor == null)
      return;
    if (owner.TextBox == null)
      owner.TextBox = new RtfInSiteEditorWrapper((TextData) owner);
    Rectangle editorBounds;
    ref Rectangle local = ref editorBounds;
    RectangleF bounds1 = owner.Bounds;
    int left = (int) bounds1.Left;
    bounds1 = owner.Bounds;
    int top = (int) bounds1.Top;
    RectangleF bounds2 = owner.Bounds;
    int width = (int) bounds2.Width;
    bounds2 = owner.Bounds;
    int height = (int) bounds2.Height;
    local = new Rectangle(left, top, width, height);
    owner.TextBox.SetupEditor(specificationEditor, owner.Text, false, -1, owner.ParagraphFormat, owner.Orientation, owner.CharFormat, owner.BackColor, owner.Bounds, editorBounds, new MarginsF(owner.LeftMargin, owner.RightMargin, owner.TopMargin, owner.BottomMargin), 1f, owner.DefaultRowSize);
    int row1 = specificationEditor.TotalLines - 1;
    int col1 = specificationEditor.TerGetLineWidth(row1) - 1;
    string valuesForCaption = this.GetAttributeValuesForCaption(false);
    if (valuesForCaption != null && valuesForCaption != "")
    {
      int num = specificationEditor.TerSearchReplace2(valuesForCaption, valuesForCaption, 2, 0, specificationEditor.TerRowColToAbs(row1, col1));
      if (num != -1)
      {
        specificationEditor.TerAbsToRowCol(num, out row1, out col1);
        if (row1 > 0)
        {
          specificationEditor.SelectTerText(num, -1, num + valuesForCaption.Length, -1, false);
          specificationEditor.SetTerCharStyle(1, false, false);
        }
      }
    }
    bool isLoading = this.avsDocument.Document.IsLoading;
    if (this.avsDocument.AvsDocumentNowLoading)
      this.avsDocument.Document.IsLoading = true;
    string str1 = "(то же, как для";
    int abs = specificationEditor.TerSearchReplace2(str1, str1, 2, 0, specificationEditor.TerRowColToAbs(row1, col1));
    if (abs != -1)
    {
      int row2;
      int col2;
      specificationEditor.TerAbsToRowCol(abs, out row2, out col2);
      int lineWidth = specificationEditor.TerGetLineWidth(row2);
      specificationEditor.SelectTerText(row2, col2, row2, lineWidth, false);
      specificationEditor.SetTerCharStyle(1, false, false);
    }
    string str2 = "Отсутствуют";
    int FirstLine = specificationEditor.TerSearchReplace2(str2, str2, 2, 0, specificationEditor.TerRowColToAbs(row1, col1));
    if (FirstLine != -1)
    {
      specificationEditor.SelectTerText(FirstLine, -1, FirstLine + str2.Length, -1, false);
      specificationEditor.SetTerCharStyle(1, false, false);
    }
    string rtfText = (string) null;
    if (!specificationEditor.CheckPlaneText())
      rtfText = specificationEditor.RtfText;
    owner.AssignText(owner.Text, rtfText, false, false, false);
    this.avsDocument.Document.IsLoading = isLoading;
  }

  /// <summary>Пронумеровать позиции записей</summary>
  /// <param name="numerationHelper">Вспомогательный класс для нумерации позиций</param>
  public override void RenumberPositions(NumerationHelper numerationHelper)
  {
    if (!(numerationHelper.Chapter is VariableDataChapterFormA))
      numerationHelper.Chapter = (Chapter) this;
    base.RenumberPositions(numerationHelper);
  }

  public override SkipLinesStruct GetSkipLines(
    SkipLinesSchema skipLinesSchema,
    List<SkipLinesStruct> structs)
  {
    SkipLinesStruct skipLines = base.GetSkipLines(skipLinesSchema, structs);
    int beforeVariantNumber = skipLinesSchema != null ? skipLinesSchema.BeforeVariantNumber : 0;
    skipLines.SkipAfter = skipLinesSchema != null ? (float) skipLinesSchema.AfterVariantNumber : 0.0f;
    skipLines.SkipBefore = (float) beforeVariantNumber;
    return skipLines;
  }

  protected override bool IgnoreCreateForEmptyChapters => this.Parent?.DocNode == null;

  /// <summary>Идентификатор версии исполнения</summary>
  public long ProductId
  {
    [DebuggerStepThrough] get => this.ChapterID;
  }

  private string GetCaptionProductDesignation()
  {
    return this.product == null ? base.Caption : this.product.Designation;
  }

  /// <summary>Получить заголовок для переменных данных исполнения</summary>
  /// <param name="withEmpty">Добавлять фразу "Отсутствуют" для пустых переменных данных</param>
  /// <returns></returns>
  public string GetCaption(bool withEmpty)
  {
    this.caption = this.GetFullProductCaption(this.GetCaptionProductDesignation());
    if (this.avsDocument != null && (this.avsDocument.IsSpecification || this.avsDocument.IsElementList))
    {
      if (this.IsEmpty)
      {
        if (withEmpty)
          this.caption = $"{this.caption}{Environment.NewLine}{Environment.NewLine}Отсутствуют";
      }
      else if (this.FirstSameProduct != null && this.GetRows().Any<AVSRow>())
      {
        bool flag = true;
        foreach (Chapter chapter in this.Chapters)
        {
          foreach (VisualNode docNode in chapter.DocNodes)
          {
            if (docNode.Visible)
            {
              flag = false;
              break;
            }
          }
        }
        if (flag)
          this.caption = $"{this.caption}{Environment.NewLine}(то же, как для {this.FirstSameProduct.GetCaptionProductDesignation()})";
      }
    }
    if (this.DocNodes.Count > 0 && this.Product != null)
      this.DocNodes[0].SetAttributeValue(Chapter.Designation_AttributeName, this.Product.Designation);
    return this.caption;
  }

  /// <summary>Заголовок</summary>
  public override string Caption
  {
    get => this.GetCaption(true);
    set
    {
      if (!(base.Caption != value))
        return;
      base.Caption = value;
      for (int index = 0; index < this.DocNodes.Count; ++index)
        this.DocNodes[index].SetName(this.Caption, false, false);
    }
  }

  /// <summary>Получить список атрибутов в заголовке</summary>
  /// <param name="designation">Обозначение исполнения</param>
  /// <returns></returns>
  private string GetFullProductCaption(string designation)
  {
    return designation.Replace(' ', ' ') + this.GetAttributeValuesForCaption(true);
  }

  /// <summary>Получить список атрибутов для заголовка исполнения</summary>
  /// <param name="withFirstSeparator">Включая разделитель в начале строки, перед обозначением</param>
  /// <returns></returns>
  private string GetAttributeValuesForCaption(bool withFirstSeparator)
  {
    string valuesForCaption = "";
    if (this.CaptionAttributes != null && this.CaptionAttributes.Count > 0)
    {
      long attributeTypeId = (long) MetaDataHelper.GetAttributeTypeID("cad0038b-306c-11d8-b4e9-00304f19f545");
      for (int index = 0; index < this.CaptionAttributes.Count; ++index)
      {
        string str = this.CaptionAttributes[index].Value;
        if (this.CaptionAttributes[index].Id == attributeTypeId && !string.IsNullOrEmpty(str))
          str = ProductVariableDataChapter.SameLiters(this.avsDocument) ? "" : $"Лит. \"{str}\"";
        if (!string.IsNullOrEmpty(str))
          valuesForCaption = withFirstSeparator || !string.IsNullOrEmpty(valuesForCaption) ? valuesForCaption + this.CaptionAttributes[index].Separator + str : str;
      }
    }
    return valuesForCaption;
  }

  public override Chapter GetChapter(long chapterId)
  {
    return this.avsDocument.IsSpecification ? base.GetChapter(chapterId) : this.GetChapter(AVSDocument.ChapterVariableDataRowsGuid);
  }

  /// <summary>Добавить подраздел</summary>
  /// <param name="chapter">Подраздел</param>
  /// <param name="sort">Вставить согласно сортировке</param>
  /// <param name="createDocNode">Создать узел документа</param>
  /// <param name="createListNode">Создать узел дерева табличного вида</param>
  /// <param name="docChapterTemplate">Шаблон раздела документа</param>
  /// <returns>Индекс вставленного подраздела</returns>
  public override int AddChapter(
    Chapter chapter,
    bool sort,
    bool createDocNode,
    bool createListNode,
    TableData docChapterTemplate)
  {
    int num = base.AddChapter(chapter, sort, createDocNode, createListNode, docChapterTemplate);
    if (!this.avsDocument.AvsDocumentNowLoading)
    {
      for (int index = 0; index < this.DocNodes.Count; ++index)
        this.DocNodes[index].SetName(this.Caption, false, false);
    }
    return num;
  }

  /// <summary>Вставить подраздел</summary>
  /// <param name="chapter">Подраздел</param>
  /// <param name="chapterIndex">Индекс подраздела</param>
  /// <param name="createDocNode">Создать узел документа</param>
  /// <param name="createListNode">Создать узел дерева табличного вида</param>
  /// <param name="docChapterTemplate">Шаблон раздела документа</param>
  public override void InsertChapter(
    Chapter chapter,
    int chapterIndex,
    bool createDocNode,
    bool createListNode,
    TableData docChapterTemplate)
  {
    base.InsertChapter(chapter, chapterIndex, createDocNode, createListNode, docChapterTemplate);
    if (this.avsDocument.AvsDocumentNowLoading)
      return;
    for (int index = 0; index < this.DocNodes.Count; ++index)
      this.DocNodes[index].SetName(this.Caption, false, false);
  }

  /// <summary>Удалить подраздел</summary>
  /// <param name="chapter">Подраздел</param>
  /// <param name="removeRelations">Удалить связи, принадлежащие записям, из базы</param>
  /// <param name="removeDocObjectWithoutRelations">Удалять документы без связей</param>
  /// <param name="removeDocNode">Удалить узлы документа</param>
  /// <param name="removeGridNode">Удалить узлы табличного вида</param>
  public override List<KeyValuePair<long, RelInfo>> RemoveChapter(
    Chapter chapter,
    bool removeRelations,
    bool removeDocObjectWithoutRelations,
    bool removeDocNode,
    bool removeGridNode)
  {
    List<KeyValuePair<long, RelInfo>> keyValuePairList = base.RemoveChapter(chapter, removeRelations, removeDocObjectWithoutRelations, removeDocNode, removeGridNode);
    for (int index = 0; index < this.DocNodes.Count; ++index)
      this.DocNodes[index].SetName(this.Caption, false, false);
    this.DocNodeExp?.SetName(this.Caption, false, false);
    return keyValuePairList;
  }

  /// <summary>Получить шаблон узла документа для этого подраздела</summary>
  public override TableData GetDocNodeTemplate()
  {
    return this.avsDocument.productVariableDataChapterTemplate != null ? this.avsDocument.productVariableDataChapterTemplate : base.GetDocNodeTemplate();
  }

  /// <summary>Удалить пустые разделы</summary>
  /// <param name="keepWithDocNode">Сохранять разделы для которых есть узлы документов</param>
  public override void RemoveEmptySections(bool keepWithDocNode)
  {
    if (this.Parent.DocNodes == null)
      this.DocNodes = new List<TableData>();
    base.RemoveEmptySections(keepWithDocNode);
  }

  /// <summary>Получить строку заголовка раздела</summary>
  /// <returns></returns>
  public override TableData GetChapterCaptionRow()
  {
    TableData chapterCaptionRow = base.GetChapterCaptionRow();
    return chapterCaptionRow != null && chapterCaptionRow.TemplateId == Chapter.ProductCaptionRowID || this.DocNode == null || this.DocNode.Nodes == null || this.DocNode.Nodes.Count <= 0 || !(this.DocNode.FindFirstNodeFromTemplate_Recursive(Chapter.ProductCaptionRowID, true) is TableData templateRecursive) ? chapterCaptionRow : templateRecursive;
  }

  /// <summary>Обновить заголовки в документе и табличном виде</summary>
  public override void UpdateChapterCaption()
  {
    if (this.DocNodes == null && !this.IsEmpty && !this.AVSDocument.ReadOnly)
      this.UpdateViewNodes(this.avsDocument.skipLinesSchema, false, false, false, false, false, EmptyRowUpdateMode.DontChange);
    this.UpdateCaptionAttributes();
    base.UpdateChapterCaption();
    this.UpdateEmptyCaption();
    if (this.Product == null)
      return;
    DocumentTreeNode chapterCaptionRow = (DocumentTreeNode) this.GetChapterCaptionRow();
    if (chapterCaptionRow == null || chapterCaptionRow.Nodes == null || chapterCaptionRow.Nodes.Count <= 0)
      return;
    chapterCaptionRow.SetAttributeValue(Chapter.Designation_AttributeName, this.Product.Designation);
    for (int index1 = 0; index1 < chapterCaptionRow.Nodes.Count; ++index1)
    {
      if (chapterCaptionRow.Nodes[index1] is TextData node && node.Name != "Наименование" && node.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource)
      {
        if (referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
        {
          referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, DBHelper.GetAttributeTypeIDFromAttributeGuid(referenceToTextSource.AttributeGuid), referenceToTextSource.AttributeName);
          referenceToTextSource.AssignReferenceType(RefToDBObjectType.rtUseParentObjectLink);
          if (referenceToTextSource.DBObjectInfo == null)
            referenceToTextSource.AssignDBObjectInfo((DBObjectInfoBase) new DBObjectInfo(), true);
        }
        if (referenceToTextSource.AttributeID != -1)
        {
          string attributeValue = this.product.GetAttributeValue(referenceToTextSource.AttributeID);
          if (this.listNode != null && this.avsDocument.IsGridViewMode)
          {
            List<AvsRowAttributeInfo> gridViewColumns = this.avsDocument.AVSWindow.GetGridViewColumns();
            for (int index2 = 0; index2 < gridViewColumns.Count; ++index2)
            {
              if (gridViewColumns[index2].AttrSrc == FieldSource.Object && gridViewColumns[index2].AttributeId == referenceToTextSource.AttributeID && this.listNode[(object) index2] != null && this.listNode[(object) index2].ToString() != attributeValue)
                this.listNode[(object) index2] = (object) attributeValue;
            }
          }
          node.AssignText(attributeValue, false, true, false, false, false);
        }
      }
    }
  }

  public override void GetCellData(AVSColumn column, CellData data)
  {
    base.GetCellData(column, data);
    if (this.avsDocument.AVSDocType == AVSDocumentType.AutoIndustrySpecification && this.Product != null)
    {
      if (this.avsDocument.variableDataChapterTemplate == null)
        return;
      DocumentTreeNode node1 = this.avsDocument.variableDataChapterTemplate.FindNode(Chapter.ProductCaptionRowID);
      if (node1 == null || node1.NodesCount <= 0)
        return;
      for (int index = 0; index < node1.NodesCount; ++index)
      {
        if (node1.Nodes[index] is TextData node2)
        {
          if (node2.ReferenceToTextSource is ReferenceToDBObjectAttribute referenceToTextSource && referenceToTextSource.AttributeID == -1 && referenceToTextSource.AttributeGuid != Guid.Empty)
            referenceToTextSource.AssignAttributeInfo(referenceToTextSource.AttributeGuid, DBHelper.GetAttributeTypeIDFromAttributeGuid(referenceToTextSource.AttributeGuid), referenceToTextSource.AttributeName);
          if (referenceToTextSource != null && referenceToTextSource.AttributeID != -1)
          {
            string attributeValue = this.product.GetAttributeValue(referenceToTextSource.AttributeID);
            data.Value = (object) attributeValue;
          }
        }
      }
    }
    else
    {
      if (this.UseParentDocNode)
        return;
      data.Value = (object) this.GetCaption(false);
    }
  }

  private struct CaptionAttribute(string value, long id, string separator)
  {
    public string Value = value;
    public long Id = id;
    public string Separator = separator;
  }
}
