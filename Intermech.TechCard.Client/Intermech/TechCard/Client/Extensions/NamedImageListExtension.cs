// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Extensions.NamedImageListExtension
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Drawing;

#nullable disable
namespace Intermech.TechCard.Client.Extensions;

internal static class NamedImageListExtension
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandsProvider"></param>
  /// <param name="factory"></param>
  public static void RegisterIconForObjectType(
    this INamedImageList namedImageList,
    string iconImageName,
    int objectTypeId)
  {
    if (namedImageList.ImageIndex(iconImageName) != -1)
      return;
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    Icon icon1 = service.GetIcon(4, objectTypeId);
    if (icon1 == null)
      return;
    using (Icon icon2 = ImagesResizeHelper.ResizeIconTo16x16(icon1, Color.Transparent))
      namedImageList.Add((Image) icon2.ToBitmap(), iconImageName);
  }
}
