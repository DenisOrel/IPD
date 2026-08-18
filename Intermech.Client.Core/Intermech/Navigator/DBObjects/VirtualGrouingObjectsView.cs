
// Type: Intermech.Navigator.DBObjects.VirtualGrouingObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Закладка, отображающая список найденных группирующих объектов
/// </summary>
public class VirtualGrouingObjectsView : ObjectsViewBase
{
  /// <summary>Индекс значка закладки</summary>
  private static int _imageIndex = -1;

  /// <summary>Название закладки</summary>
  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_333");

  /// <summary>Индекс значка закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (VirtualGrouingObjectsView._imageIndex >= 0)
        return VirtualGrouingObjectsView._imageIndex;
      VirtualGrouingObjectsView._imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgGroupingObjects");
      return VirtualGrouingObjectsView._imageIndex;
    }
  }
}
