// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AdditionalChapter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.HelperClasses;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Переменные данные исполнений группового документа формы А. Содержит подразделы с данными исполнений</summary>
public class AdditionalChapter : Chapter
{
  private Chapter innerCommonDataChapter;

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  /// <param name="chapterSettings">Параметры дополнительной части</param>
  /// <param name="innerAdditionalChapter">true - часть будет находиться внутри общих и переменных данных, false - часть будет следовать за основными разделами</param>
  public AdditionalChapter(
    AVSDocument avsDocument,
    AdditionalChapterSettings chapterSettings,
    bool innerAdditionalChapter)
    : base(avsDocument, false)
  {
    if (chapterSettings != null)
    {
      this.ChapterGuid = chapterSettings.ChapterGuid;
      this.caption = chapterSettings.Caption;
    }
    this.nodeLevel = Chapter.AdditionalChapter_TypeName;
    if (!innerAdditionalChapter)
    {
      if (avsDocument == null)
        return;
      this.AddChapter(avsDocument.CreateCommonDataChapter(false), false, false, false, (TableData) null);
      if (avsDocument.AvsDocumentForm == AVSDocumentForm.A)
      {
        this.AddChapter((Chapter) new VariableDataChapterFormA(avsDocument, avsDocument.productsInfo, true), false, false, false, (TableData) null);
      }
      else
      {
        if (avsDocument.AvsDocumentForm != AVSDocumentForm.V)
          return;
        this.AddChapter((Chapter) new VariableDataChapterFormV(avsDocument), false, false, false, (TableData) null);
      }
    }
    else
      this.isSectionOwner = true;
  }

  /// <summary>Заголовок</summary>
  public override string Caption
  {
    get
    {
      string caption = base.Caption.Replace("?", "\r\n").Replace("~", " ");
      if (this.avsDocument != null)
        caption = caption.Replace("&", this.avsDocument.DocumentDesignation);
      return caption;
    }
    set => base.Caption = value;
  }

  /// <summary>Узлы документа представляющие этот раздел</summary>
  public override List<TableData> DocNodes
  {
    get => base.DocNodes;
    set
    {
      base.DocNodes = value;
      if (this.Product == null)
        return;
      for (int index = 0; index < this.DocNodes.Count; ++index)
        this.DocNodes[index].SetAttributeValue(Chapter.CaptionFormat_AttributeName, this.caption);
    }
  }

  /// <summary>Пронумеровать позиции записей</summary>
  /// <param name="numerationHelper">Вспомогательный класс для нумерации позиций</param>
  public override void RenumberPositions(NumerationHelper numerationHelper)
  {
    numerationHelper.Chapter = (Chapter) this;
    base.RenumberPositions(numerationHelper);
  }

  /// <summary>Создать узел документа для этого раздела</summary>
  /// <param name="templateNode">Шаблон</param>
  /// <returns>Узел документа</returns>
  public override TableData CreateDocNode(TableData templateNode)
  {
    TableData docNode = base.CreateDocNode(templateNode);
    if (docNode == null)
      return docNode;
    docNode.SetFromNewPage(this.avsDocument.AVSCommonPropertiesSchema.DisplayPartOnNewPageChanged, false, false);
    return docNode;
  }

  /// <summary>Вставить страницы созданные для раздела в документ</summary>
  /// <param name="prevPage">Предыдущая страница</param>
  /// <returns>Возвращает последнюю страницу</returns>
  public override PageData InsertPagesInDocument(PageData prevPage)
  {
    if (this.AVSDocument.AvsDocumentForm == AVSDocumentForm.V && !this.IsExportSP)
    {
      if (this.DocNode != null)
      {
        prevPage = prevPage.FindLastPage();
        int index = prevPage.Index + 1;
        PageData pageData = this.DocNode.Page;
        if (pageData == null)
        {
          pageData = this.avsDocument.avsDocTableTemplate.Page.NextPageTemplate.CloneFromTemplate(true, true) as PageData;
          this.avsDocument.FindMainDocTableFromPage(pageData, false).AddChildNode((DocumentTreeNode) this.DocNode, false, false);
        }
        this.avsDocument.Document.InsertChildNode(index, (DocumentTreeNode) pageData, true, true, false, false, false);
        prevPage = pageData;
      }
      if (this.InnerVariableData_FormV != null)
        prevPage = this.InnerVariableData_FormV.InsertPagesInDocument(prevPage);
    }
    return prevPage;
  }

  /// <summary>Индекс сортировки однотипных разделов</summary>
  public override long SortIndex
  {
    get
    {
      if (this.avsDocument != null)
      {
        List<AdditionalChapterSettings> additionalChapters = this.avsDocument.AVSCommonPropertiesSchema.AdditionalChapters;
        if (base.SortIndex >= 0L && base.SortIndex < (long) additionalChapters.Count && additionalChapters[(int) base.SortIndex].ChapterGuid == this.ChapterGuid)
          return base.SortIndex;
        for (int index = 0; index < additionalChapters.Count; ++index)
        {
          if (additionalChapters[index].ChapterGuid == this.ChapterGuid)
          {
            base.SortIndex = (long) index;
            return base.SortIndex;
          }
        }
      }
      return base.SortIndex;
    }
    set => base.SortIndex = value;
  }

  /// <summary>Индекс сортировки разделов по их типам</summary>
  public override long ChapterSortIndex
  {
    [DebuggerStepThrough] get => 50;
  }

  /// <summary>Исполнение которое соответствует разделу</summary>
  public override ProductInfo Product
  {
    get => base.Product ?? new ProductInfo(AVSDocument.AdditionalChapterGuid, -1L, this.Caption);
    set => base.Product = value;
  }

  public override SkipLinesStruct GetSkipLines(
    SkipLinesSchema skipLinesSchema,
    List<SkipLinesStruct> structs)
  {
    SkipLinesStruct skipLines = base.GetSkipLines(skipLinesSchema, structs);
    int num = skipLinesSchema != null ? skipLinesSchema.BeforeAdditional : 1;
    skipLines.SkipAfter = skipLinesSchema != null ? (float) skipLinesSchema.AfterAdditional : 1f;
    skipLines.SkipBefore = (float) num;
    return skipLines;
  }

  public AdditionalChapterSettings GetChapterSettings()
  {
    return new AdditionalChapterSettings(this.ChapterGuid, this.ChapterID, this.caption, this.SortIndex);
  }

  public override int AddChapter(
    Chapter chapter,
    bool sort,
    bool createDocNode,
    bool createListNode,
    TableData docChapterTemplate)
  {
    int num = base.AddChapter(chapter, sort, createDocNode, createListNode, docChapterTemplate);
    if (num == -1 || this.innerCommonDataChapter != null || !chapter.IsCommonDataChapter)
      return num;
    this.innerCommonDataChapter = chapter;
    return num;
  }

  public override List<KeyValuePair<long, RelInfo>> RemoveChapter(
    Chapter chapter,
    bool removeRelations,
    bool removeDocObjectWithoutRelations,
    bool removeDocNode,
    bool removeGridNode)
  {
    List<KeyValuePair<long, RelInfo>> keyValuePairList = base.RemoveChapter(chapter, removeRelations, removeDocObjectWithoutRelations, removeDocNode, removeGridNode);
    if (this.innerCommonDataChapter != chapter)
      return keyValuePairList;
    this.innerCommonDataChapter = (Chapter) null;
    return keyValuePairList;
  }

  /// <summary>Внутренние переменные данные</summary>
  internal Chapter InnerVariableDataChapter
  {
    get
    {
      if (this.avsDocument != null)
      {
        if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.A)
          return (Chapter) this.InnerVariableData_FormA;
        if (this.avsDocument.AvsDocumentForm == AVSDocumentForm.V)
          return (Chapter) this.InnerVariableData_FormV;
      }
      else
      {
        if (this.InnerVariableData_FormA != null)
          return (Chapter) this.InnerVariableData_FormA;
        if (this.InnerVariableData_FormV != null)
          return (Chapter) this.InnerVariableData_FormV;
      }
      return (Chapter) null;
    }
  }

  /// <summary>Внутренние Общие данные</summary>
  internal Chapter InnerCommonDataChapter
  {
    get
    {
      for (int index = 0; this.innerCommonDataChapter == null && index < this.chapters.Count; ++index)
      {
        if (this.chapters[index].IsCommonDataChapter)
          this.innerCommonDataChapter = this.chapters[index];
      }
      return this.innerCommonDataChapter;
    }
  }

  internal VariableDataChapterFormA InnerVariableData_FormA
  {
    get
    {
      VariableDataChapterFormA variableDataFormA = (VariableDataChapterFormA) null;
      for (int index = 0; variableDataFormA == null && index < this.chapters.Count; ++index)
        variableDataFormA = this.chapters[index] as VariableDataChapterFormA;
      return variableDataFormA;
    }
  }

  internal VariableDataChapterFormV InnerVariableData_FormV
  {
    get
    {
      VariableDataChapterFormV variableDataFormV = (VariableDataChapterFormV) null;
      for (int index = 0; variableDataFormV == null && index < this.chapters.Count; ++index)
        variableDataFormV = this.chapters[index] as VariableDataChapterFormV;
      return variableDataFormV;
    }
  }
}
