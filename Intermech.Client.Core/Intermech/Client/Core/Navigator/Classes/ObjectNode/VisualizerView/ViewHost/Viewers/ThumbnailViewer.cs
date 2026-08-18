
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ThumbnailViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net;
using Intermech.Interfaces.Compositions;
using Intermech.Map;
using System.Drawing;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class ThumbnailViewer : Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer
{
  protected internal override MapObject GetMapObject(
    FileItem fileItemInfo,
    RelationPair relationPairKey)
  {
    Bitmap thumbnail = WindowsThumbnailProvider.GetThumbnail(fileItemInfo.FileFullName, this.Width, this.Height, ThumbnailOptions.BiggerSizeOk);
    return thumbnail == null ? (MapObject) null : (MapObject) new ImageObject((Image) thumbnail, true);
  }
}
