
// Type: Intermech.Client.Core.Organizer.ColorTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class ColorTable
{
  public readonly Color DarkBorder = Color.FromArgb(101, 147, 207);
  public readonly Color Background = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  public readonly Color ShapesFront = Color.FromArgb(86, 125, 177);
  public readonly Color Text = Color.FromArgb(21, 66, 139);
  public readonly Color ButtonLight = Color.FromArgb(192 /*0xC0*/, 219, (int) byte.MaxValue);
  public readonly Color ButtonDark = Color.FromArgb(173, 209, (int) byte.MaxValue);
  public readonly Color ButtonHighlightDark = Color.FromArgb(196, 221, (int) byte.MaxValue);
  public readonly Color ButtonHighlightLight = Color.FromArgb(227, 239, (int) byte.MaxValue);
  public readonly Color ButtonHoveredLight = Color.FromArgb((int) byte.MaxValue, 230, 159);
  public readonly Color ButtonHoveredDark = Color.FromArgb((int) byte.MaxValue, 215, 103);
  public readonly Color ButtonHoveredHighlightDark = Color.FromArgb((int) byte.MaxValue, 233, 168);
  public readonly Color ButtonHoveredHighlightLight = Color.FromArgb((int) byte.MaxValue, 254, 228);
  public readonly Color ButtonClickedLight = Color.FromArgb((int) byte.MaxValue, 211, 101);
  public readonly Color ButtonClickedDark = Color.FromArgb(251, 140, 60);
  public readonly Color ButtonClickedHighlightDark = Color.FromArgb((int) byte.MaxValue, 173, 67);
  public readonly Color ButtonClickedHighlightLight = Color.FromArgb((int) byte.MaxValue, 189, 105);
  public readonly Color ButtonActiveLight = Color.FromArgb(254, 225, 122);
  public readonly Color ButtonActiveDark = Color.FromArgb((int) byte.MaxValue, 171, 63 /*0x3F*/);
  public readonly Color ButtonActiveHighlightDark = Color.FromArgb((int) byte.MaxValue, 188, 111);
  public readonly Color ButtonActiveHighlightLight = Color.FromArgb((int) byte.MaxValue, 217, 170);
  public readonly Color HeaderBgDark = Color.FromArgb(175, 210, (int) byte.MaxValue);
  public readonly Color HeaderBgLight = Color.FromArgb(227, 239, (int) byte.MaxValue);
  public readonly Color HeaderBgInnerBorder = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  public readonly Color SplitterDark = Color.FromArgb(182, 214, (int) byte.MaxValue);
  public readonly Color SplitterLight = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  public readonly Color SplitterHighlights = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  public readonly Color CollapseButtonHoveredDark = Color.FromArgb(248, 194, 94);
  public readonly Color CollapseButtonHoveredLight = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 220);
  public readonly Color CollapseButtonDownDark = Color.FromArgb(232, (int) sbyte.MaxValue, 8);
  public readonly Color CollapseButtonDownLight = Color.FromArgb(247, 217, 121);
  public readonly Color BandCollapsedBg = Color.FromArgb(213, 228, 242);
  public readonly Color BandCollapsedFocused = Color.FromArgb((int) byte.MaxValue, 231, 162);
  public readonly Color BandCollapsedClicked = Color.FromArgb(251, 140, 60);
}
