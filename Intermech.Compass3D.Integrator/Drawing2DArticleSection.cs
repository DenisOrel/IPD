// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DArticleSection
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DArticleSection
{
  private Drawing2DArticleKind articleKind;

  public Drawing2DArticleKind ArticleKind
  {
    get => this.articleKind;
    set => this.articleKind = value;
  }
}
