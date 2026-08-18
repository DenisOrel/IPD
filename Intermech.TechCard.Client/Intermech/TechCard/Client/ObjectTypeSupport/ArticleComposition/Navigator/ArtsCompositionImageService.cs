// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionImageService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Extensions;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>
/// 
/// </summary>
internal class ArtsCompositionImageService : IArtsCompositionImageService, IDisposable
{
  /// <summary>
  /// 
  /// </summary>
  private static readonly Size DefaultImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
  /// <summary>
  /// 
  /// </summary>
  private readonly ImageList _imageList;
  /// <summary>
  /// 
  /// </summary>
  private readonly IArtsCompositionParams _compositionParams;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeData()
  {
    IReadOnlyList<ArtsCompositionItemStatus> valuesList = EnumType.GetValuesList<ArtsCompositionItemStatus>();
    for (int index = 0; index < valuesList.Count; ++index)
    {
      Size imageSize = this.ImageList.ImageSize;
      int width = imageSize.Width;
      imageSize = this.ImageList.ImageSize;
      int height = imageSize.Height;
      Image image = (Image) new Bitmap(width, height);
      this._imageList.Images.Add(image);
      ArtsCompositionItemStatus itemStatus = valuesList[index];
      IArtsCompositionStatusParams compositionStatusParams = this._compositionParams.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item.Status == itemStatus));
      if (compositionStatusParams != null)
      {
        using (Graphics graphics = Graphics.FromImage(image))
          graphics.Clear(compositionStatusParams.Color);
        this._imageList.Images[index] = image;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="compositionParams"></param>
  public ArtsCompositionImageService(IArtsCompositionParams compositionParams)
  {
    this._compositionParams = compositionParams;
    this._imageList = new ImageList()
    {
      ColorDepth = ColorDepth.Depth24Bit,
      ImageSize = ArtsCompositionImageService.DefaultImageSize
    };
    this.InitializeData();
  }

  /// <summary>
  /// 
  /// </summary>
  public ImageList ImageList => this._imageList;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="status"></param>
  /// <returns></returns>
  public int ImageIndex(ArtsCompositionItemStatus status)
  {
    return EnumType.GetValuesList<ArtsCompositionItemStatus>().IndexOfFirst<ArtsCompositionItemStatus>((Predicate<ArtsCompositionItemStatus>) (item => item == status));
  }

  /// <summary>
  /// 
  /// </summary>
  public void Dispose() => this._imageList.Dispose();
}
