
// Type: Intermech.Search.UI.FontHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.UI;

public static class FontHelper
{
  [Obsolete("Use the method FontHelper.MeasureStringFast instead of this method.", true)]
  public static SizeF MeasureString(Font font, string text)
  {
    using (Bitmap bitmap = new Bitmap(1, 1))
    {
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        return graphics.MeasureString(text, font);
    }
  }

  public static Size MeasureStringFast(Font font, string text)
  {
    return TextRenderer.MeasureText(text, font);
  }
}
