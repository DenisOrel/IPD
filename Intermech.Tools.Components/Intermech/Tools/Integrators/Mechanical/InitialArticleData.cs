// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.InitialArticleData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class InitialArticleData
{
  private MechanicalArticleKind articleKind;
  private string displayName;
  private string articleKey;
  private long objectId;
  private ArticleInitialDocumentType initialDocumentType;
  private SectionCollection customSections;

  public InitialArticleData(MechanicalArticleKind articleKind)
  {
    this.articleKind = articleKind;
    this.objectId = 0L;
    this.initialDocumentType = ArticleInitialDocumentType.None;
    this.customSections = new SectionCollection();
  }

  public MechanicalArticleKind ArticleKind
  {
    get => this.articleKind;
    set => this.articleKind = value;
  }

  public string DisplayName
  {
    get => this.displayName;
    set => this.displayName = value;
  }

  public string ArticleKey
  {
    get => this.articleKey;
    set => this.articleKey = value;
  }

  public long ObjectId
  {
    get => this.objectId;
    set => this.objectId = value;
  }

  /// <summary>
  /// Возвращает или задает тип исходного документа для изделия, который использовался интегратором для получения информации об изделии.
  /// </summary>
  public ArticleInitialDocumentType InitialDocumentType
  {
    get => this.initialDocumentType;
    set => this.initialDocumentType = value;
  }

  public SectionCollection CustomSections => this.customSections;
}
