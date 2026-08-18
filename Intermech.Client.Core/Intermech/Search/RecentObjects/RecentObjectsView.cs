
// Type: Intermech.Search.RecentObjects.RecentObjectsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsView : ObjectsViewBase
{
  private int? _imageIndex;

  public static bool CheckParams(ISelectedItems selectedItems)
  {
    return selectedItems.Count == 1 && selectedItems.GetItemID(0).CategoryID == Intermech.Navigator.Consts.CategoryRecentObjectsNode;
  }

  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_296");

  public override ContentType ViewContentType => ContentType.Folders;

  public override int ImageIndex
  {
    get
    {
      if (!this._imageIndex.HasValue)
        this._imageIndex = new int?((ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgRecentObjects"));
      return this._imageIndex.Value;
    }
  }
}
