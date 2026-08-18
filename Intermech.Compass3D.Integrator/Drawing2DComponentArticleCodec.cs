// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DComponentArticleCodec
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Data;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.Mechanical;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DComponentArticleCodec : ArticleAttributesCodec
{
  public Drawing2DComponentArticleCodec()
    : base((IValueBagFormatter) new Drawing2DFormatter())
  {
  }

  protected override StringKey GetContainerValueKey(StringKey attributeKey)
  {
    return attributeKey == (StringKey) IDCache.Default.ImbaseKey.Text ? (StringKey) null : base.GetContainerValueKey(attributeKey);
  }
}
