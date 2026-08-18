// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DOperations
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DOperations
{
  private K3DIntegratorSettings k3dSettings;

  public Drawing2DOperations(K3DIntegratorSettings k3dSettings)
  {
    this.k3dSettings = k3dSettings != null ? k3dSettings : throw new ArgumentNullException(nameof (k3dSettings));
  }

  public bool IsDrawing2D(SectionEntity docItem) => this.IsDrawing2D(docItem.Sections);

  public bool IsDrawing2D(SectionCollection sections)
  {
    return sections.Contains<Drawing2DDocumentSection>();
  }

  public void AddCustomDocumentData(SectionCollection sections)
  {
    Drawing2DDocumentSection sectionObject = new Drawing2DDocumentSection();
    sections.Set((object) sectionObject);
  }

  public void RemoveCustomDocumentData(SectionEntity docItem)
  {
    docItem.Sections.Remove(typeof (Drawing2DDocumentSection));
  }

  public void AddCustomArticleData(SectionCollection sections, Drawing2DArticleKind articleKind)
  {
    sections.Set((object) new Drawing2DArticleSection()
    {
      ArticleKind = articleKind
    });
  }

  public void RemoveCustomArticleData(SectionEntity articleItem)
  {
    articleItem.Sections.Remove(typeof (Drawing2DArticleSection));
  }

  public bool IsDrawing2DArticle(SectionEntity articleItem)
  {
    return articleItem.Sections.Contains<Drawing2DArticleSection>();
  }

  public Drawing2DArticleKind GetArticleKind(SectionEntity articleItem)
  {
    return articleItem.Sections.Get<Drawing2DArticleSection>().ArticleKind;
  }

  public bool IsHeadArticle(SectionEntity articleItem)
  {
    return this.IsDrawing2DArticle(articleItem) && this.GetArticleKind(articleItem) == Drawing2DArticleKind.HeadArticle;
  }

  public bool IsComponentArticle(SectionEntity articleItem)
  {
    return this.IsDrawing2DArticle(articleItem) && this.GetArticleKind(articleItem) == Drawing2DArticleKind.ComponentArticle;
  }
}
