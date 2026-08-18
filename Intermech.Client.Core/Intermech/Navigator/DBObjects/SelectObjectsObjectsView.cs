
// Type: Intermech.Navigator.DBObjects.SelectObjectsObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

/// <summary>Закладка, отображающая список недавних объектов</summary>
public sealed class SelectObjectsObjectsView : ObjectsViewBase
{
  /// <summary>Индекс значка закладки</summary>
  private static int _imageIndex = -1;

  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_1351");

  public override int ImageIndex
  {
    get
    {
      if (SelectObjectsObjectsView._imageIndex >= 0)
        return SelectObjectsObjectsView._imageIndex;
      SelectObjectsObjectsView._imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgSelectObjects");
      return SelectObjectsObjectsView._imageIndex;
    }
  }

  public override ContentType ViewContentType => ContentType.NonFolders;
}
