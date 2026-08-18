
// Type: MWCommon.CheckBoxPaintOrder
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace MWCommon;

/// <summary>
/// Decides which order to Paint the Check, the Image and the Text for MWCheckBoxes (last is topmost).
/// </summary>
public enum CheckBoxPaintOrder
{
  CheckImageText,
  CheckTextImage,
  ImageCheckText,
  ImageTextCheck,
  TextCheckImage,
  TextImageCheck,
}
