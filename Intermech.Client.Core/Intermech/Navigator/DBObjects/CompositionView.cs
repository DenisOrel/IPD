
// Type: Intermech.Navigator.DBObjects.CompositionView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

public class CompositionView : ObjectsViewBase
{
  private int _imageIndex;

  public CompositionView() => this._imageIndex = -1;

  public override ContentType ViewContentType => ContentType.NonFolders;

  public override int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgContains");
      return this._imageIndex;
    }
  }
}
