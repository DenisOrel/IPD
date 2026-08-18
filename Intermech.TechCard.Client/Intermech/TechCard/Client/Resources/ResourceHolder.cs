// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Resources.ResourceHolder
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

#nullable disable
namespace Intermech.TechCard.Client.Resources;

/// <summary>
/// 
/// </summary>
public class ResourceHolder
{
  /// <summary>
  /// 
  /// </summary>
  private static Image _loadingImage;

  /// <summary>
  /// 
  /// </summary>
  public static Image LoadingImage
  {
    get
    {
      if (ResourceHolder._loadingImage == null)
        ResourceHolder._loadingImage = (Image) ResourceHolder.LoadImageFromResources("Intermech.TechCard.Client.Resources.loading.gif");
      return ResourceHolder._loadingImage;
    }
  }

  /// <summary>Загрузка image из ресурсов текущей сборки</summary>
  /// <param name="resourceName">Наменование ресурса</param>
  public static Bitmap LoadImageFromResources(string resourceName)
  {
    return ResourceHolder.LoadImageFromResources(typeof (ResourceHolder).Assembly, resourceName);
  }

  /// <summary>Загрузка image из ресурсов указанной сборки</summary>
  /// <param name="assembly"></param>
  /// <param name="resourceName">Наменование ресурса</param>
  public static Bitmap LoadImageFromResources(Assembly assembly, string resourceName)
  {
    if (assembly == (Assembly) null)
      throw new ArgumentNullException(nameof (assembly));
    if (resourceName == string.Empty)
      return (Bitmap) null;
    Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
    if (manifestResourceStream == null)
      return (Bitmap) null;
    try
    {
      if (Image.FromStream(manifestResourceStream) is Bitmap bitmap && bitmap.RawFormat.Guid != ImageFormat.Icon.Guid)
        bitmap.MakeTransparent();
      return bitmap;
    }
    catch (Exception ex)
    {
    }
    return (Bitmap) null;
  }
}
