// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseRootNodeThumbnailView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core.Thumbnail;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

[ViewDescriptionProvider(typeof (ImbaseRootNodeThumbnailView.ImbaseRootNodeThumbnailViewDescriptionProvider))]
public class ImbaseRootNodeThumbnailView : ThumbnailView
{
  private IContainer components;

  protected override ContentType ContentType => ContentType.NonFolders;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }

  private sealed class ImbaseRootNodeThumbnailViewDescriptionProvider : 
    ThumbnailView.ThumbnailViewDescriptionProvider
  {
  }
}
