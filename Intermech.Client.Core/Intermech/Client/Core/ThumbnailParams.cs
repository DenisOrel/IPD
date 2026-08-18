
// Type: Intermech.Client.Core.ThumbnailParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core;

/// <summary>Класс для сохранения параметров Thumbnail видов.</summary>
public class ThumbnailParams
{
  private static Size _panelSize = new Size(0, 0);

  private ThumbnailParams()
  {
  }

  public static Size PanelSize
  {
    get => ThumbnailParams._panelSize;
    set => ThumbnailParams._panelSize = value;
  }
}
