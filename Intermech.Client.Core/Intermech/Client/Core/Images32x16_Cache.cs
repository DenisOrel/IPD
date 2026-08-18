
// Type: Intermech.Client.Core.Images32x16_Cache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Кэш значков 32x16</summary>
public static class Images32x16_Cache
{
  /// <summary>Объект для потокобезопасного доступа</summary>
  private static object SyncRoot = new object();
  /// <summary>Сервис для получения значков по категориям и типам</summary>
  private static ICategoryTypeIconService ObjtypesIcons;
  /// <summary>Кэш значков по категориям и типам</summary>
  internal static Dictionary<Icon, Icon> Icons = new Dictionary<Icon, Icon>();
  /// <summary>Кэш значков по категориям и типам</summary>
  internal static Dictionary<ImagesCacheIndex, Image> Images = new Dictionary<ImagesCacheIndex, Image>();

  /// <summary>Статический конструктор</summary>
  static Images32x16_Cache()
  {
    Images32x16_Cache.ObjtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
  }

  /// <summary>Очистить кэш</summary>
  public static void Reset()
  {
    foreach (KeyValuePair<Icon, Icon> icon in Images32x16_Cache.Icons)
    {
      if (icon.Value != null)
        icon.Value.Dispose();
    }
    foreach (KeyValuePair<ImagesCacheIndex, Image> image1 in Images32x16_Cache.Images)
    {
      Image image2 = image1.Value;
    }
    Images32x16_Cache.Icons.Clear();
    Images32x16_Cache.Images.Clear();
  }

  /// <summary>Вернуть значок для указанных категории, типа и данных</summary>
  /// <param name="category">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="data">Данные</param>
  /// <returns></returns>
  public static Icon GetIcon32x16(int category, int typeID, NavigatorTreeNode data)
  {
    INavigatorIconInformation data1 = data == null || data.NodeID == null || data.Parent == null || data.Parent.Handler == null ? (INavigatorIconInformation) null : data.Parent.Handler.GetData(data.NodeID, typeof (INavigatorIconInformation)) as INavigatorIconInformation;
    return Images32x16_Cache.GetIcon32x16(category, typeID, (object) data1);
  }

  /// <summary>Вернуть значок для указанных категории, типа и данных</summary>
  /// <param name="category">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="value">Данные</param>
  /// <returns></returns>
  public static Icon GetIcon32x16(int category, int typeID, object value)
  {
    Icon icon = Images32x16_Cache.ObjtypesIcons.GetIcon(category, typeID, value);
    if (icon == null)
      return (Icon) null;
    lock (Images32x16_Cache.SyncRoot)
    {
      if (Images32x16_Cache.Icons.ContainsKey(icon))
        return Images32x16_Cache.Icons[icon];
      Icon icon32x16 = ImagesResizeHelper.ResizeIconTo32x16(icon, Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue));
      Images32x16_Cache.Icons[icon] = icon32x16;
      return icon32x16;
    }
  }

  /// <summary>
  /// Вернуть изображение для указанных категории, типа и данных
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="data">Данные</param>
  /// <returns></returns>
  public static Image GetImage32x16(int category, int typeID, NavigatorTreeNode data)
  {
    INavigatorIconInformation data1 = data == null || data.NodeID == null || data.Parent == null || data.Parent.Handler == null ? (INavigatorIconInformation) null : data.Parent.Handler.GetData(data.NodeID, typeof (INavigatorIconInformation)) as INavigatorIconInformation;
    return Images32x16_Cache.GetImage32x16(category, typeID, (object) data1);
  }

  /// <summary>
  /// Вернуть изображение для указанных категории, типа и данных
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="value">Данные</param>
  /// <returns></returns>
  public static Image GetImage32x16(int category, int typeID, object value)
  {
    int index = Images32x16_Cache.ObjtypesIcons.IndexOf(category, typeID, value);
    return index < 0 ? (Image) null : Images32x16_Cache.ObjtypesIcons.ImageList.Images[index];
  }

  /// <summary>
  /// Вернуть список изображений для категорий и типов, с размерностью изображений 32x16
  /// </summary>
  /// <returns>Список изображений для категорий и типов, с размерностью изображений 32x16</returns>
  public static ImageList GetImageList32x16()
  {
    return Images32x16_Cache.ObjtypesIcons == null ? (ImageList) null : Images32x16_Cache.ObjtypesIcons.ImageList;
  }

  /// <summary>
  /// Вернуть индекс изображения для указанных категории, типа и данных
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="data">Данные</param>
  /// <returns></returns>
  public static int GetImage32x16Index(int category, int typeID, NavigatorTreeNode data)
  {
    INavigatorIconInformation data1 = data == null || data.NodeID == null || data.Parent == null || data.Parent.Handler == null ? (INavigatorIconInformation) null : data.Parent.Handler.GetData(data.NodeID, typeof (INavigatorIconInformation)) as INavigatorIconInformation;
    return Images32x16_Cache.ObjtypesIcons.IndexOf(category, typeID, (object) data1);
  }

  /// <summary>
  /// Вернуть индекс изображения для указанных категории, типа и данных
  /// </summary>
  /// <param name="category">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="value">Данные</param>
  /// <returns></returns>
  public static int GetImage32x16Index(int category, int typeID, object value)
  {
    return Images32x16_Cache.ObjtypesIcons.IndexOf(category, typeID, value);
  }
}
