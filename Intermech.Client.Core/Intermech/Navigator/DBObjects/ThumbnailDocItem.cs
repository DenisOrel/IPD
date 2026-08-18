
// Type: Intermech.Navigator.DBObjects.ThumbnailDocItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Thumbnail;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

internal class ThumbnailDocItem : ThumbnailItem
{
  private object _preview;

  public object Preview => this._preview;

  public ThumbnailDocItem(
    INodeID nodeID,
    string caption,
    long objectId,
    int typeId,
    object preview)
    : base(nodeID, caption, objectId, typeId)
  {
    this._preview = preview;
  }
}
