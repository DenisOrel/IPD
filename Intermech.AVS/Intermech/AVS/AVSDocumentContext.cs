// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSDocumentContext
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Контекст операций в спецификации</summary>
public class AVSDocumentContext
{
  /// <summary>Загрузка документа</summary>
  public bool IsDocumentLoading;
  /// <summary>Загрузка нового документа</summary>
  public bool IsNewDocument = true;
  /// <summary>Загрузка документа из файла SP, старого формата AVS</summary>
  public bool IsOldSpConverting;
  /// <summary>Загрузка спецификации</summary>
  public bool IsSpecRowUpdate;
  /// <summary>Во время загрузки/обновления данных не обновлять строки документа</summary>
  public bool SuspendUpdateDocRows;
  /// <summary>Во время загрузки/обновления данных обновлять строки заготовк</summary>
  public bool UpdateDraftRows;
  /// <summary>Допустимые разделы документа</summary>
  public List<SpecificationSectionInfo> AllowableSections;
  /// <summary>Раздел спецификации</summary>
  public SpecificationSection Section;
  private AdditionalChapter additionalChapter;
  /// <summary>Раздел</summary>
  public Chapter Chapter;
  /// <summary>Целевые Исполнения</summary>
  public List<ProductInfo> Products = new List<ProductInfo>();
  /// <summary>Индекс записи документа. -1 - не назначен.</summary>
  public int RowIndex = -1;
  /// <summary>Выбранная запись документа</summary>
  public AVSRow Row;
  /// <summary>Идентификатор текущей связи, если она есть</summary>
  public long RelationID = -1;
  /// <summary>Первый индекс исполнения в текущем блоке исполнений для формы Б</summary>
  public int FirstProductIndexInBlock = -1;
  /// <summary>Блокировать конфигуратор состава, даже если его включили</summary>
  public bool BlockConfigureComposition = true;
  /// <summary>Корень конфигурации состава</summary>
  public RelationPair ConfigureCompositionRoot;
  /// <summary>Тип связи по умолчанию</summary>
  public int DefaultRelationType = -1;

  /// <summary>Конструктор</summary>
  /// <param name="isDocumentLoading">Загрузка документа</param>
  /// <param name="section">Раздел документа</param>
  /// <param name="allowableSections">Допустимые разделы документа</param>
  /// <param name="rowIndex">Индекс записи документа</param>
  public AVSDocumentContext(
    bool isDocumentLoading,
    SpecificationSection section,
    List<SpecificationSectionInfo> allowableSections)
  {
    this.IsDocumentLoading = isDocumentLoading;
    this.Section = section;
    this.AllowableSections = allowableSections;
  }

  /// <summary>Конструктор</summary>
  /// <param name="isDocumentLoading">Загрузка документа</param>
  /// <param name="chapter">Часть документа</param>
  /// <param name="products">Исполнения</param>
  /// <param name="section">Раздел документа</param>
  /// <param name="allowableSections">Допустимые разделы документа</param>
  /// <param name="rowIndex">Индекс записи документа</param>
  /// <param name="configureCompositionRoot">Корень конфигурации состава</param>
  /// <param name="blockConfigureComposition">Блокировать конфигуратор состава, даже если его включили</param>
  public AVSDocumentContext(
    bool isDocumentLoading,
    Chapter chapter,
    List<ProductInfo> products,
    SpecificationSection section,
    List<SpecificationSectionInfo> allowableSections,
    int rowIndex,
    RelationPair configureCompositionRoot,
    bool blockConfigureComposition)
    : this(isDocumentLoading, section, allowableSections)
  {
    this.Chapter = chapter;
    this.Products = products ?? new List<ProductInfo>();
    this.RowIndex = rowIndex;
    this.ConfigureCompositionRoot = configureCompositionRoot;
    this.BlockConfigureComposition = blockConfigureComposition;
  }

  /// <summary>Конструктор</summary>
  public AVSDocumentContext()
  {
  }

  /// <summary>Идентификатор раздела спецификации. -1 если не назначен.</summary>
  public long SectionID
  {
    [DebuggerStepThrough] get => this.Section != null ? this.Section.SectionID : -1L;
  }

  /// <summary>Идентификатор исполнения. -1 - общие данные, 0 - не назначен.</summary>
  public long ProductID
  {
    [DebuggerStepThrough] get => this.Product != null ? this.Product.Id : -1L;
  }

  public AdditionalChapter AdditionalChapter
  {
    get
    {
      if (this.additionalChapter == null && this.Chapter != null)
        this.additionalChapter = this.Chapter.GetRootChapter() as AdditionalChapter;
      return this.additionalChapter;
    }
  }

  public Guid? AdditionalChapterGuid
  {
    get
    {
      return this.AdditionalChapter != null ? new Guid?(this.AdditionalChapter.ChapterGuid) : new Guid?();
    }
  }

  /// <summary>Исполнение</summary>
  public ProductInfo Product
  {
    get => this.Products != null && this.Products.Count > 0 ? this.Products[0] : (ProductInfo) null;
    set
    {
      if (this.Products != null)
        this.Products.Clear();
      else
        this.Products = new List<ProductInfo>();
      this.Products.Add(value);
    }
  }

  public RelationAttributeValuesCache CurrentRelationInRow
  {
    get
    {
      return this.Row != null ? this.Row.GetRelation(this.RelationID) : (RelationAttributeValuesCache) null;
    }
  }

  /// <summary>Получить текущие исполнения для выбранной записи.
  /// Если выбрано количество в форме Б, то вернёт конкретное исполнение.
  /// Если не выбрано исполнение, то вернёт все</summary>
  /// <returns></returns>
  public long[] GetCurrentProductsInRow()
  {
    long[] currentProductsInRow = new long[0];
    if (this.Row != null && this.Row.HasRelation)
    {
      if (this.CurrentRelationInRow != null)
      {
        currentProductsInRow = new long[1]
        {
          this.CurrentRelationInRow.ProjectId
        };
      }
      else
      {
        currentProductsInRow = new long[this.Row.Relations.Count];
        for (int index = 0; index < this.Row.Relations.Count; ++index)
          currentProductsInRow[index] = this.Row.Relations[index].ProjectId;
      }
    }
    return currentProductsInRow;
  }

  /// <summary>Получить текущие связи для выбранной записи.
  /// Если выбрано количество в форме Б, то вернёт связь для конкретного исполнения.
  /// Если не выбрано исполнение, то вернёт все</summary>
  /// <returns></returns>
  public Guid[] GetCurrentRelationGuidsInRow()
  {
    Guid[] relationGuidsInRow = new Guid[0];
    if (this.Row != null && this.Row.HasRelation)
    {
      if (this.CurrentRelationInRow != null)
      {
        relationGuidsInRow = new Guid[1]
        {
          this.CurrentRelationInRow.RelationGuid
        };
      }
      else
      {
        relationGuidsInRow = new Guid[this.Row.Relations.Count];
        for (int index = 0; index < this.Row.Relations.Count; ++index)
          relationGuidsInRow[index] = this.Row.Relations[index].RelationGuid;
      }
    }
    return relationGuidsInRow;
  }
}
