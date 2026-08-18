// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class ArticleSection
{
  public static readonly SectionPropertyReference ArticleKeyRef = new SectionPropertyReference(typeof (ArticleSection), nameof (ArticleKey));
  private string articleKey;
  private SectionEntity initialDocument;
  private ArticleInitialDocumentType initialDocumentType = ArticleInitialDocumentType.None;

  [Indexable(IndexType.Auto, false)]
  [Comparer(typeof (ServiceObjectAttribute.NewObject), new object[] {typeof (PathComparer)})]
  public string ArticleKey
  {
    get => this.articleKey;
    set
    {
      if (PathUtils.IsSamePath(this.articleKey, value))
        return;
      this.articleKey = value;
      if (this.ArticleKeyChanged == null)
        return;
      this.ArticleKeyChanged((object) this, EventArgs.Empty);
    }
  }

  public void SetInitialDocument(
    ArticleInitialDocumentType documentType,
    SectionEntity documentItem)
  {
    this.initialDocumentType = documentType == ArticleInitialDocumentType.None || documentItem != null ? documentType : throw new ArgumentNullException(nameof (documentItem));
    this.initialDocument = documentItem;
  }

  /// <summary>
  /// Возвращает тип исходного документа для изделия, который использовался интегратором для получения информации об изделии.
  /// </summary>
  public ArticleInitialDocumentType InitialDocumentType => this.initialDocumentType;

  /// <summary>
  /// Возвращает исходный документ для изделия, который использовался интегратором для получения информации об изделии.
  /// Может быть null, если изделие было сформировано интегратором
  /// </summary>
  public SectionEntity InitialDocument => this.initialDocument;

  public event EventHandler ArticleKeyChanged;

  public static SectionEntity FindArticleByKey(CaptureChangesDatabase db, string articleKey)
  {
    if (db == null)
      throw new ArgumentNullException(nameof (db));
    return db.QueryFirst((IQueryCondition) new BinaryCondition((object) ArticleSection.ArticleKeyRef, BinaryOperator.Equal, (object) articleKey));
  }
}
