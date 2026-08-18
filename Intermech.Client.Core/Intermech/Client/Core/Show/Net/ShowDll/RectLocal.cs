
// Type: Intermech.Client.Core.Show.Net.ShowDll.RectLocal
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Show.Net.ShowDll;

internal struct RectLocal
{
  internal int left;
  internal int top;
  internal int right;
  internal int bottom;

  internal RectLocal(Rectangle val)
  {
    this.left = val.Left;
    this.top = val.Top;
    this.right = val.Right;
    this.bottom = val.Bottom;
  }
}
