
// Type: MWCommon.MWCommon
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace MWCommon;

/// <summary>
/// Common is used for various static methods that are accessed from several places so the code does not have to be duplicated.
/// </summary>
public class MWCommon
{
  /// <summary>
  /// Get the Width of the supplied Control using its Text, Font and Graphics context.
  /// </summary>
  /// <param name="ctl">Control to measure Text of.</param>
  /// <returns>Width of the Text of the Control supplied.</returns>
  public static int GetGraphicalStringWidth(Control ctl)
  {
    return MWCommon.MWCommon.GetGraphicalStringRect(ctl.CreateGraphics(), ctl.Text, ctl.Font).Width;
  }

  /// <summary>
  /// Get the Width of the supplied string using the supplied Font on the supplied Graphics context.
  /// </summary>
  /// <param name="g">Graphics context object to measure string on.</param>
  /// <param name="str">String to measure.</param>
  /// <param name="fnt">Font to use for string.</param>
  /// <returns>Width of the string supplied.</returns>
  public static int GetGraphicalStringWidth(Graphics g, string str, Font fnt)
  {
    return MWCommon.MWCommon.GetGraphicalStringRect(g, str, fnt).Width;
  }

  /// <summary>
  /// Get the smallest encompassing Rectangle for the Text of the Control supplied using its Text, Font and Graphics context.
  /// </summary>
  /// <param name="ctl">Control to measure Text of.</param>
  /// <returns>Smallest Rectangle encompassing the Text of the supplied Control.</returns>
  public static Rectangle GetGraphicalStringRect(Control ctl)
  {
    return MWCommon.MWCommon.GetGraphicalStringRect(ctl.CreateGraphics(), ctl.Text, ctl.Font);
  }

  /// <summary>
  /// Get the smallest encompassing Rectangle for the supplied string, Font and Graphics context.
  /// </summary>
  /// <param name="g">Graphics context object to measure string on.</param>
  /// <param name="str">String to measure.</param>
  /// <param name="fnt">Font to use for string.</param>
  /// <returns>Smallest Rectangle encompassing the supplied string.</returns>
  public static Rectangle GetGraphicalStringRect(Graphics g, string str, Font fnt)
  {
    RectangleF bounds = (!(string.Empty != str) ? new Region(new Rectangle(0, 0, 0, 0)) : MWCommon.MWCommon.GetGraphicalStringRegion(g, str, fnt)).GetBounds(g);
    return new Rectangle((int) Math.Floor((double) bounds.Left), (int) Math.Floor((double) bounds.Top), (int) Math.Ceiling((double) bounds.Width), (int) Math.Ceiling((double) bounds.Height));
  }

  /// <summary>
  /// Get the smallest encompassing Region for the Text of the Control supplied using its Text, Font and Graphics context.
  /// </summary>
  /// <param name="ctl">Control to measure Text of.</param>
  /// <returns>Smallest Region encompassing the Text of the supplied Control.</returns>
  public static Region GetGraphicalStringRegion(Control ctl)
  {
    return MWCommon.MWCommon.GetGraphicalStringRegion(ctl.CreateGraphics(), ctl.Text, ctl.Font);
  }

  /// <summary>
  /// Get the smallest encompassing Region for the supplied string, Font and Graphics context.
  /// </summary>
  /// <param name="g">Graphics context object to measure string on.</param>
  /// <param name="str">String to measure.</param>
  /// <param name="fnt">Font to use for string.</param>
  /// <returns>Smallest Region encompassing the supplied string.</returns>
  public static Region GetGraphicalStringRegion(Graphics g, string str, Font fnt)
  {
    StringFormat stringFormat = new StringFormat();
    RectangleF layoutRect = new RectangleF(0.0f, 0.0f, 1000f, 1000f);
    CharacterRange[] ranges = new CharacterRange[1]
    {
      new CharacterRange(0, str.Length)
    };
    Region[] regionArray = new Region[1];
    stringFormat.SetMeasurableCharacterRanges(ranges);
    return g.MeasureCharacterRanges(str, fnt, layoutRect, stringFormat)[0];
  }

  /// <summary>
  /// Get the Width of the supplied Control using its Text, Font and Graphics context and the supplied StringFormat.
  /// </summary>
  /// <param name="ctl">Control to measure Text of.</param>
  /// <param name="strfmt">StringFormat to use when measuring the string.</param>
  /// <returns>Width of the Text of the Control supplied using the StringFormat supplied.</returns>
  public static int GetStringFormattedStringWidth(Control ctl, StringFormat strfmt)
  {
    return MWCommon.MWCommon.GetStringFormattedStringRectangle(ctl.CreateGraphics(), ctl.Text, ctl.Font, ctl.ClientRectangle, strfmt).Width;
  }

  /// <summary>
  /// Get the Width of the supplied string using the supplied Font, Rectangle and StringFormat on the supplied Graphics context.
  /// </summary>
  /// <param name="g">Graphics context object to measure string on.</param>
  /// <param name="str">String to measure.</param>
  /// <param name="fnt">Font to use for string.</param>
  /// <param name="rct">Rectangle to measure string in.</param>
  /// <param name="strfmt">StringFormat to use when measuring the string.</param>
  /// <returns>Width of the Text of the Control supplied using the StringFormat supplied.</returns>
  public static int GetStringFormattedStringWidth(
    Graphics g,
    string str,
    Font fnt,
    Rectangle rct,
    StringFormat strfmt)
  {
    return MWCommon.MWCommon.GetStringFormattedStringRectangle(g, str, fnt, rct, strfmt).Width;
  }

  /// <summary>
  /// Get the smallest encompassing Rectangle of the supplied Control using its Text, Font and Graphics context and the supplied StringFormat.
  /// </summary>
  /// <param name="ctl">Control to measure Text of.</param>
  /// <param name="strfmt">StringFormat to use when measuring the string.</param>
  /// <returns>Smallest Rectangle encompassing the Text of the Control supplied using the StringFormat supplied.</returns>
  public static Rectangle GetStringFormattedStringRectangle(Control ctl, StringFormat strfmt)
  {
    return MWCommon.MWCommon.GetStringFormattedStringRectangle(ctl.CreateGraphics(), ctl.Text, ctl.Font, ctl.ClientRectangle, strfmt);
  }

  /// <summary>
  /// Get the smallest encompassing Rectangle of the supplied string using the supplied Font, Rectangle and StringFormat on the supplied Graphics context.
  /// </summary>
  /// <param name="g">Graphics context object to measure string on.</param>
  /// <param name="str">String to measure.</param>
  /// <param name="fnt">Font to use for string.</param>
  /// <param name="rct">Rectangle to measure string in.</param>
  /// <param name="strfmt">StringFormat to use when measuring the string.</param>
  /// <returns>Smallest Rectangle encompassing the Text of the Control supplied using the StringFormat supplied.</returns>
  public static Rectangle GetStringFormattedStringRectangle(
    Graphics g,
    string str,
    Font fnt,
    Rectangle rct,
    StringFormat strfmt)
  {
    RectangleF bounds = (!(string.Empty != str) ? new Region(new Rectangle(0, 0, 0, 0)) : MWCommon.MWCommon.GetStringFormattedStringRegion(g, str, fnt, rct, strfmt)).GetBounds(g);
    return new Rectangle((int) Math.Floor((double) bounds.Left), (int) Math.Floor((double) bounds.Top), (int) Math.Ceiling((double) bounds.Width), (int) Math.Ceiling((double) bounds.Height));
  }

  /// <summary>
  /// Get the smallest encompassing Region of the supplied Control using its Text, Font and Graphics context and the supplied StringFormat.
  /// </summary>
  /// <param name="ctl">Control to measure Text of.</param>
  /// <param name="strfmt">StringFormat to use when measuring the string.</param>
  /// <returns>Smallest Region encompassing the Text of the Control supplied using the StringFormat supplied.</returns>
  public static Region GetStringFormattedStringRegion(Control ctl, StringFormat strfmt)
  {
    return MWCommon.MWCommon.GetStringFormattedStringRegion(ctl.CreateGraphics(), ctl.Text, ctl.Font, ctl.ClientRectangle, strfmt);
  }

  /// <summary>
  /// Get the smallest encompassing Region of the supplied string using the supplied Font, Rectangle and StringFormat on the supplied Graphics context.
  /// </summary>
  /// <param name="g">Graphics context object to measure string on.</param>
  /// <param name="str">String to measure.</param>
  /// <param name="fnt">Font to use for string.</param>
  /// <param name="rct">Rectangle to measure string in.</param>
  /// <param name="strfmt">StringFormat to use when measuring the string.</param>
  /// <returns>Smallest Region encompassing the Text of the Control supplied using the StringFormat supplied.</returns>
  public static Region GetStringFormattedStringRegion(
    Graphics g,
    string str,
    Font fnt,
    Rectangle rct,
    StringFormat strfmt)
  {
    RectangleF layoutRect = new RectangleF((float) rct.X, (float) rct.Y, (float) rct.Width, (float) rct.Height);
    CharacterRange[] ranges = new CharacterRange[1]
    {
      new CharacterRange(0, str.Length)
    };
    Region[] regionArray = new Region[1];
    strfmt.SetMeasurableCharacterRanges(ranges);
    return g.MeasureCharacterRanges(str, fnt, layoutRect, strfmt)[0];
  }
}
