
// Type: OfficePickers.ColorPicker.ToolStripColorPickerDisplayType
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace OfficePickers.ColorPicker;

/// <summary>
/// Specifies the display options for the ToolStripColorPicker such as
/// image, text and underline.
/// </summary>
public enum ToolStripColorPickerDisplayType
{
  NormalImage,
  /// <summary>
  /// Specifies that both image and text are to be rendered for this ToolStripColorPicker
  /// </summary>
  NormalImageAndText,
  /// <summary>
  /// Specifies that both color under-line and image are to be rendered for this ToolStripColorPicker
  /// </summary>
  UnderLineAndImage,
  /// <summary>
  /// Specifies that both color under-line and text are to be rendered for this ToolStripColorPicker
  /// </summary>
  UnderLineAndText,
  /// <summary>
  /// Specifies that both color under-line, text and image are to be rendered for this ToolStripColorPicker
  /// </summary>
  UnderLineTextAndImage,
  /// <summary>
  /// Specifies that only a color under-line is to be rendered for this ToolStripColorPicker
  /// </summary>
  UnderLineOnly,
  /// <summary>
  /// Specified that neither image, text nor under-line is to be rendered for this ToolStripColorPicker
  /// </summary>
  None,
  /// <summary>
  /// Specifies that only text is to be rendered for this ToolStripColorPicker
  /// </summary>
  Text,
}
