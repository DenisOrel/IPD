// Decompiled with JetBrains decompiler
// Type: Intermech.BaseHolder
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

#nullable disable
namespace Intermech;

public class BaseHolder
{
  public static IPackage Plugin = (IPackage) null;
  public static ICategoryTypeIconService IconService = (ICategoryTypeIconService) null;
  public static IGuidMapper GuidMapper = (IGuidMapper) null;
  public static IFactory Factory = (IFactory) null;
  public static INotificationService NotificationService = (INotificationService) null;
  public static INamedImageList NamedList = (INamedImageList) null;
  public static IPopupMenuHost PopupHost = (IPopupMenuHost) null;
  public static IHotKeysManager HotKeysManager = (IHotKeysManager) null;
  public static ICommandManager CommandManager = (ICommandManager) null;
  /// <summary>
  /// Ключевой пункт главного меню, перед которым будем вставлять свои пункты. В IPS 4 это был "Окна", в IPS 5 - "Вид"
  /// </summary>
  public static readonly string KeyMenuBarName = "View";
  protected static List<Type> _initedList = new List<Type>();
  public static HashSet<string> ExtraOpenCommands = new HashSet<string>((IEnumerable<string>) new string[3]
  {
    "OpenWith",
    "ViewWithOptions",
    "OpenDocument"
  });

  /// <summary>
  /// 
  /// </summary>
  /// <param name="plugin"></param>
  /// <param name="serviceProvider"></param>
  /// <returns>True if inited first time, otherwise False</returns>
  public static void Init(IPackage plugin, IServiceProvider serviceProvider)
  {
    if (BaseHolder.Inited(typeof (BaseHolder)))
      return;
    BaseHolder.Plugin = plugin;
    BaseHolder.IconService = (ICategoryTypeIconService) serviceProvider.GetService(typeof (ICategoryTypeIconService));
    BaseHolder.GuidMapper = (IGuidMapper) serviceProvider.GetService(typeof (IGuidMapper));
    BaseHolder.Factory = (IFactory) serviceProvider.GetService(typeof (IFactory));
    BaseHolder.NotificationService = (INotificationService) serviceProvider.GetService(typeof (INotificationService));
    BaseHolder.NamedList = (INamedImageList) serviceProvider.GetService(typeof (INamedImageList));
    BaseHolder.PopupHost = (IPopupMenuHost) serviceProvider.GetService(typeof (IPopupMenuHost));
    BaseHolder.HotKeysManager = (IHotKeysManager) serviceProvider.GetService(typeof (IHotKeysManager));
    BaseHolder.CommandManager = (ICommandManager) serviceProvider.GetService(typeof (ICommandManager));
  }

  protected static bool Inited(Type t)
  {
    if (BaseHolder._initedList.Contains(t))
      return true;
    BaseHolder._initedList.Add(t);
    return false;
  }

  public static Image ImageTo16x16(Image bmp)
  {
    if (bmp.Width <= 16 /*0x10*/)
      return bmp;
    Bitmap bitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/, bmp.PixelFormat);
    using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      graphics.DrawImage(bmp, 0, 0, new Rectangle(0, 0, 16 /*0x10*/, 16 /*0x10*/), GraphicsUnit.Pixel);
    return (Image) bitmap;
  }

  [NotNull]
  protected static Bitmap LoadResImage(Assembly assembly, string name)
  {
    bitmap = (Bitmap) null;
    Stream manifestResourceStream = assembly.GetManifestResourceStream(assembly.GetName().Name + name);
    if (manifestResourceStream != null && Image.FromStream(manifestResourceStream) is Bitmap bitmap && bitmap.RawFormat.Guid != ImageFormat.Icon.Guid)
      bitmap.MakeTransparent();
    return bitmap;
  }

  protected static Assembly MyAssembly => typeof (BaseHolder).Assembly;

  protected static Bitmap LoadResImage(string name)
  {
    return BaseHolder.LoadResImage(BaseHolder.MyAssembly, name);
  }

  public static int GetObjectTypeImageIndex(int typeID)
  {
    return BaseHolder.IconService != null ? BaseHolder.IconService.IndexOf(4, typeID) : -1;
  }

  public static List<Image> LoadImages(string[] ids, string[] names)
  {
    return BaseHolder.LoadImages(BaseHolder.MyAssembly, ids, names);
  }

  public static List<Image> LoadImages(Assembly assembly, string[] ids, string[] names)
  {
    List<Image> imageList = new List<Image>();
    for (int index = 0; index < ids.Length; ++index)
    {
      string id = ids[index];
      string str = "";
      if (!Path.HasExtension(id))
        str = ".bmp";
      Image image = (Image) BaseHolder.LoadResImage(assembly, $".img.{ids[index]}{str}");
      if (image != null)
        BaseHolder.NamedList.Add(image, names[index]);
      imageList.Add(image);
    }
    return imageList;
  }
}
