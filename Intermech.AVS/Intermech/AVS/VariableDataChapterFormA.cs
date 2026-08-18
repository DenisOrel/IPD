// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.VariableDataChapterFormA
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Переменные данные исполнений группового документа формы А. Содержит подразделы с данными исполнений</summary>
public class VariableDataChapterFormA : Chapter
{
  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  /// <param name="products">Идентификаторы версий исполнений, для которых создать подразделы переменных данных</param>
  /// <param name="sectionInChapters">Подразделы являются владельцами разделов СП</param>
  public VariableDataChapterFormA(
    AVSDocument avsDocument,
    List<ProductInfo> products,
    bool sectionInChapters)
    : base(avsDocument, false)
  {
    this.Product = new ProductInfo(AVSDocument.ChapterVariableDataGuid, -1L, "Переменные данные");
    if (products != null)
    {
      for (int index = 0; index < products.Count; ++index)
        this.AddChapter((Chapter) new ProductVariableDataChapter(avsDocument, products[index], (long) (index * 100), sectionInChapters), false, false, false, (TableData) null);
    }
    this.ChapterGuid = AVSDocument.ChapterVariableDataGuid;
    this.Caption = avsDocument.versionAttributesHelper.VariableDataCaption;
    this.CaptionExp = "Variable data:";
    this.nodeLevel = Chapter.VariableData_TypeName;
  }

  internal void AddProduct(NewProductParams newProductParams)
  {
    ProductVariableDataChapter variableDataChapter = new ProductVariableDataChapter(this.avsDocument, this.avsDocument.productsInfo[newProductParams.ProductIndex], (long) (newProductParams.ProductIndex * 100), true);
    this.AddChapter((Chapter) variableDataChapter, true, false, false, (TableData) null);
    SpecificationSection specificationSection = (SpecificationSection) null;
    if (variableDataChapter.Chapters.Count > 0)
      specificationSection = variableDataChapter.Chapters[0] as SpecificationSection;
    if (specificationSection == null)
    {
      specificationSection = new SpecificationSection(this.avsDocument, new SpecificationSectionInfo(Guid.Empty, -1L, -1, variableDataChapter.Caption, 0L, (string) null, new int[0], new long[0]));
      variableDataChapter.AddChapter((Chapter) specificationSection, false, false, false, (TableData) null);
    }
    if (newProductParams.SrcProductIndex == -1)
      return;
    ProductVariableDataChapter chapter = this.Chapters[newProductParams.SrcProductIndex] as ProductVariableDataChapter;
    List<AVSRow> avsRowList = new List<AVSRow>();
    List<AVSRow> rowList = avsRowList;
    chapter.GetAllRowsList(false, false, rowList);
    for (int index1 = 0; index1 < avsRowList.Count; ++index1)
    {
      AVSRow row = new AVSRow(this.avsDocument, (RelationAttributeValuesCache) null, avsRowList[index1].ObjectAttributesCache);
      for (int index2 = 0; index2 < avsRowList[index1].DocNodes.Count; ++index2)
        row.AddDocNode((TableData) avsRowList[index1].DocNodes[index2].Clone(true, true));
      specificationSection.AddRow(row, false);
    }
  }

  /// <summary>Получить подраздел переменных данных для исполнения</summary>
  /// <param name="product">Исполнение</param>
  /// <returns></returns>
  public Chapter GetProductChapter(ProductInfo product)
  {
    if (product == null)
      return (Chapter) null;
    if (this.avsDocument != null)
    {
      int index = this.avsDocument.productsInfo.IndexOf(product);
      if (index != -1 && index < this.chapters.Count && this.chapters[index].Product == product)
        return this.chapters[index];
    }
    for (int index = 0; index < this.chapters.Count; ++index)
    {
      if (product.IsEqualProducts(this.chapters[index].Product))
        return this.chapters[index];
    }
    return (Chapter) null;
  }

  protected override bool IgnoreCreateForEmptyChapters => true;

  /// <summary>Заголовок</summary>
  public override string Caption
  {
    get
    {
      if (!this.IsEmpty)
        return base.Caption;
      bool flag = false;
      foreach (Chapter chapter in this.chapters)
      {
        if (chapter != null && chapter.DocNode != null && chapter.DocNode.Visible)
        {
          flag = true;
          break;
        }
      }
      return flag ? base.Caption : this.GetEmptyChapterCaption();
    }
    set
    {
      if (!(base.Caption != value))
        return;
      base.Caption = value;
      for (int index = 0; index < this.DocNodes.Count; ++index)
        this.DocNodes[index].SetName(this.Caption, false, false);
    }
  }

  /// <summary>Пронумеровать позиции записей</summary>
  /// <param name="numerationHelper">Вспомогательный класс для нумерации позиций</param>
  public override void RenumberPositions(NumerationHelper numerationHelper)
  {
    numerationHelper.Chapter = (Chapter) this;
    base.RenumberPositions(numerationHelper);
  }

  /// <summary>Обновить заголовки в документе и табличном виде</summary>
  public override void UpdateChapterCaption()
  {
    this.Caption = this.avsDocument.versionAttributesHelper.VariableDataCaption;
    base.UpdateChapterCaption();
    this.UpdateDifferentCaption();
  }

  /// <summary>Обработка событий от NotificationService</summary>
  public void UpdateNotificationData(NotificationEventArgs e)
  {
    foreach (Chapter chapter in this.chapters)
    {
      if (chapter is ProductVariableDataChapter variableDataChapter)
        variableDataChapter.UpdateNotificationData(e);
    }
  }

  public override SkipLinesStruct GetSkipLines(
    SkipLinesSchema skipLinesSchema,
    List<SkipLinesStruct> structs)
  {
    SkipLinesStruct skipLines = base.GetSkipLines(skipLinesSchema, structs);
    int beforeVariableData = skipLinesSchema != null ? skipLinesSchema.BeforeVariableData : 0;
    skipLines.SkipAfter = skipLinesSchema != null ? (float) skipLinesSchema.AfterVariableData : 0.0f;
    skipLines.SkipBefore = (float) beforeVariableData;
    return skipLines;
  }

  /// <summary>Удалить пустые разделы</summary>
  /// <param name="keepWithDocNode">Сохранять разделы для которых есть узлы документов</param>
  public override void RemoveEmptySections(bool keepWithDocNode)
  {
    base.RemoveEmptySections(keepWithDocNode);
    if (!this.IsEmpty || keepWithDocNode || !ProductVariableDataChapter.SameLiters(this.AVSDocument))
      return;
    for (int index1 = 0; index1 < this.chapters.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.chapters[index1].DocNodes.Count; ++index2)
      {
        this.chapters[index1].DocNodes[index2].UniteTable();
        this.chapters[index1].DocNodes[index2].Remove(false, false);
      }
      this.chapters[index1].DocNodes = new List<TableData>();
    }
  }

  /// <summary>Надо ли скрывать одинаковые по составу разделы</summary>
  public bool HideSameProductChapters
  {
    get => this.DocNode != null && this.DocNode.ContainsAttribute(nameof (HideSameProductChapters));
    set
    {
      if (value)
        this.DocNode?.SetAttributeValue(nameof (HideSameProductChapters), true.ToString(), updateUI: false, updateLayout: false);
      else
        this.DocNode?.RemoveAttribute(nameof (HideSameProductChapters), false, false);
    }
  }

  /// <summary>Раздел пуст</summary>
  public override bool IsEmpty
  {
    [DebuggerStepThrough] get
    {
      for (int index = 0; index < this.chapters.Count; ++index)
      {
        if (!this.chapters[index].IsEmpty)
          return false;
      }
      return true;
    }
  }

  /// <summary>Получить заголовок для переменных данных без содержимого</summary>
  public string GetEmptyChapterCaption()
  {
    if (!this.IsEmpty)
      return "";
    string str1 = "Различия исполнений ";
    List<List<Chapter>> chapterListList = new List<List<Chapter>>();
    List<Chapter> chapterList1 = (List<Chapter>) null;
    for (int index = 0; index < this.chapters.Count; ++index)
    {
      Chapter chapter = this.chapters[index];
      if (chapter != null)
      {
        int productNumber = chapter.ProductNumber;
        if (index == 0 || !this.avsDocument.AVSCommonPropertiesSchema.MergeVariableChapters)
        {
          chapterList1 = new List<Chapter>();
          chapterList1.Add(chapter);
          chapterListList.Add(chapterList1);
        }
        else if (this.chapters[index - 1].ProductNumber != productNumber - 1)
        {
          chapterList1 = new List<Chapter>();
          chapterList1.Add(chapter);
          chapterListList.Add(chapterList1);
        }
        else
          chapterList1.Add(chapter);
      }
    }
    for (int index = 0; index < chapterListList.Count; ++index)
    {
      List<Chapter> chapterList2 = chapterListList[index];
      str1 += index == 0 ? string.Empty : (index != chapterListList.Count - 1 || chapterList2.Count == 2 ? ", " : " и ");
      Chapter chapter1 = chapterList2[0];
      if (chapter1 != null)
      {
        string str2 = chapter1.Product == null ? chapter1.Caption : chapter1.Product.Designation;
        if (!string.IsNullOrEmpty(str2))
          str2 = str2.Replace(' ', ' ');
        str1 += str2;
      }
      if (chapterList2.Count > 1)
      {
        Chapter chapter2 = chapterList2[chapterList2.Count - 1];
        string str3 = (chapter2.Product == null ? chapter2.Caption : chapter2.Product.Designation).Replace(' ', ' ');
        str1 = str1 + (chapterList2.Count == 2 ? (index == chapterListList.Count - 1 ? " и " : ", ") : "...") + str3;
      }
    }
    return str1.Replace('-', '\u0017') + " по сборочному чертежу.";
  }

  public override void GetCellData(AVSColumn column, CellData data)
  {
    if (this.UseParentDocNode)
      base.GetCellData(column, data);
    else
      data.Value = (object) base.Caption;
  }

  /// <summary>Получить шаблон узла документа для этого подраздела</summary>
  public override TableData GetDocNodeTemplate()
  {
    return this.avsDocument.variableDataChapterTemplate != null ? this.avsDocument.variableDataChapterTemplate : base.GetDocNodeTemplate();
  }

  /// <summary>Убрать выделение с "Различия исполнений"</summary>
  private void UpdateDifferentCaption()
  {
    DocumentTreeNode chapterCaptionRow = (DocumentTreeNode) this.GetChapterCaptionRow();
    if (chapterCaptionRow == null || chapterCaptionRow.Nodes.Count <= 0)
      return;
    TextBoxElement owner = (TextBoxElement) null;
    for (int index = 0; index < chapterCaptionRow.Nodes.Count; ++index)
    {
      if (chapterCaptionRow.Nodes[index].Name == "Наименование")
      {
        owner = chapterCaptionRow.Nodes[index] as TextBoxElement;
        break;
      }
    }
    if (owner == null)
      return;
    if (owner.OwnerDocument is ImDocument)
      owner.CharFormat.Underline = new UnderlineStyle?(UnderlineStyle.Underline);
    ImRtfEditor specificationEditor = this.avsDocument.SpecificationEditor;
    if (specificationEditor == null)
      return;
    if (owner.TextBox == null)
      owner.TextBox = new RtfInSiteEditorWrapper((TextData) owner);
    Rectangle editorBounds;
    ref Rectangle local = ref editorBounds;
    RectangleF bounds = owner.Bounds;
    int left = (int) bounds.Left;
    bounds = owner.Bounds;
    int top = (int) bounds.Top;
    bounds = owner.Bounds;
    int width = (int) bounds.Width;
    bounds = owner.Bounds;
    int height = (int) bounds.Height;
    local = new Rectangle(left, top, width, height);
    string rtf1 = owner.Rtf;
    string rtf2 = owner.Rtf;
    if (rtf1 != null)
      owner.TextBox.SetupEditor(specificationEditor, owner.Rtf, true, -1, owner.ParagraphFormat, owner.Orientation, owner.CharFormat, owner.BackColor, owner.Bounds, editorBounds, new MarginsF(owner.LeftMargin, owner.RightMargin, owner.TopMargin, owner.BottomMargin), 1f, owner.DefaultRowSize);
    else
      owner.TextBox.SetupEditor(specificationEditor, owner.Text, false, -1, owner.ParagraphFormat, owner.Orientation, owner.CharFormat, owner.BackColor, owner.Bounds, editorBounds, new MarginsF(owner.LeftMargin, owner.RightMargin, owner.TopMargin, owner.BottomMargin), 1f, owner.DefaultRowSize);
    int num = specificationEditor.TotalLines - 1;
    int col = specificationEditor.TerGetLineWidth(num) - 1;
    string emptyChapterCaption = this.GetEmptyChapterCaption();
    int FirstLine = specificationEditor.TerSearchReplace2(emptyChapterCaption, emptyChapterCaption, 2, 0, specificationEditor.TerRowColToAbs(num, col));
    if (FirstLine != -1 && emptyChapterCaption != "")
    {
      specificationEditor.SelectTerText(FirstLine, -1, FirstLine + emptyChapterCaption.Length, -1, false);
      specificationEditor.SetTerCharStyle(1, false, false);
      owner.AssignText(owner.Text, specificationEditor.RtfText, false, false, false);
      if (!(owner.Text == specificationEditor.PlaneText))
        return;
      owner.CharFormat.Underline = new UnderlineStyle?(UnderlineStyle.None);
    }
    else
      owner.SetRtfText(rtf2, false, false);
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
    base.UpdateViewNodes(skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
    if (createForEmptyChapters)
      return;
    for (int index = 0; index < this.chapters.Count; ++index)
    {
      if (this.chapters[index].DocNode != null)
      {
        createForEmptyChapters = true;
        base.UpdateViewNodes(skipLinesSchema, reCreateDocNode, reCreateListNode, updateCountB, createForEmptyChapters, updateTemplate, updateMode);
        break;
      }
    }
  }

  /// <summary>Обновить список одинаковых исполнений</summary>
  internal void UpdateSameProductChapters(bool hideSame)
  {
    if (((ImDocumentData) this.avsDocument.Document).IsDistributing)
      return;
    if (hideSame && this.HideSameProductChapters)
    {
      List<string> list = this.Chapters.Where<Chapter>((Func<Chapter, bool>) (c => c.DocNode?.Page?.IsAdditionalPage ?? false)).Select<Chapter, string>((Func<Chapter, string>) (ch => ch.Caption)).ToList<string>();
      if (list.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Внимание!");
        stringBuilder.AppendLine("В документе активирован режим дополнительных листов.");
        stringBuilder.AppendLine("Данные следующих исполнений, расположенные на дополнительных листах, не будут скрыты:");
        stringBuilder.AppendLine("");
        foreach (string str in list)
          stringBuilder.AppendLine(str);
        int num = (int) MessageBox.Show(stringBuilder.ToString(), "Предупреждение");
      }
    }
    List<ProductVariableDataChapter> variableDataChapterList = new List<ProductVariableDataChapter>();
    Dictionary<string, List<ProductVariableDataChapter>> dictionary = new Dictionary<string, List<ProductVariableDataChapter>>();
    foreach (Chapter chapter in this.Chapters)
    {
      if (chapter is ProductVariableDataChapter variableDataChapter)
      {
        variableDataChapterList.Add(variableDataChapter);
        if (this.HideSameProductChapters && variableDataChapter.GetRows().Any<AVSRow>())
        {
          string chapterCode = variableDataChapter.GetChapterCode();
          if (!dictionary.ContainsKey(chapterCode))
            dictionary[chapterCode] = new List<ProductVariableDataChapter>();
          dictionary[chapterCode].Add(variableDataChapter);
          variableDataChapter.SameProducts = dictionary[chapterCode];
        }
        else
          variableDataChapter.SameProducts = (List<ProductVariableDataChapter>) null;
      }
    }
    bool flag1 = false;
    foreach (ProductVariableDataChapter variableDataChapter in variableDataChapterList)
    {
      bool flag2 = !this.HideSameProductChapters || variableDataChapter.FirstSameProduct == null;
      foreach (Chapter chapter in variableDataChapter.Chapters)
      {
        foreach (VisualNode docNode in chapter.DocNodes)
        {
          if (docNode.Visible != flag2)
          {
            flag1 = true;
            break;
          }
        }
        if (flag1)
          break;
      }
      if (flag1)
        break;
    }
    if (!flag1)
      return;
    foreach (ProductVariableDataChapter variableDataChapter in variableDataChapterList)
    {
      bool flag3 = !this.HideSameProductChapters || variableDataChapter.FirstSameProduct == null;
      if (flag3 || hideSame)
      {
        foreach (Chapter chapter in variableDataChapter.Chapters)
        {
          foreach (TableData docNode in chapter.DocNodes)
          {
            if (!flag3 && docNode.Visible)
              docNode.UniteTable();
            docNode.SetVisible(flag3, false, false, false, false);
            docNode.SetAttributeValue("ignoreSetVisible", true.ToString());
          }
        }
        variableDataChapter.UpdateChapterCaption();
      }
    }
    this.AVSDocument.Document.UpdateLayout(0, true, true);
  }

  /// <summary>Индекс сортировки</summary>
  public override long ChapterSortIndex
  {
    [DebuggerStepThrough] get => 20;
  }
}
