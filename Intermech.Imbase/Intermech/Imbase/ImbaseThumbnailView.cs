// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseThumbnailView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core.Thumbnail;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;

#nullable disable
namespace Intermech.Imbase;

[ViewDescriptionProvider(typeof (ImbaseThumbnailView.ImbaseThumbnailViewDescriptionProvider))]
internal sealed class ImbaseThumbnailView : ThumbnailView
{
  protected override ContentType ContentType => ContentType.Folders;

  private sealed class ImbaseThumbnailViewDescriptionProvider : 
    ThumbnailView.ThumbnailViewDescriptionProvider
  {
  }
}
