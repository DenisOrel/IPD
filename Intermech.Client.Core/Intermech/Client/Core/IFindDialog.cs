
// Type: Intermech.Client.Core.IFindDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core;

public interface IFindDialog
{
  Point GetScreenCoords();

  void SetScreenCoords(Point point);

  Size GetSize();
}
