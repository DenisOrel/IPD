
// Type: OfficePickers.Util.CustomColors
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;


namespace OfficePickers.Util;

/// <summary>
/// Provides custom colors members to use in the OfficeColorPicker.
/// </summary>
internal class CustomColors
{
  internal static Color ButtonHoverLight = Color.FromArgb((int) byte.MaxValue, 240 /*0xF0*/, 207);
  internal static Color ButtonHoverDark = Color.FromArgb(249, 179, 48 /*0x30*/);
  internal static Color SelectedAndHover = Color.FromArgb(254, 128 /*0x80*/, 62);
  internal static Color ButtonBorder = Color.FromArgb(172, 168, 153);
  internal static Color FocusDashedBorder = Color.FromArgb(83, 87, 102);
  internal static Color SelectedBorder = Color.FromArgb(0, 0, 128 /*0x80*/);
  internal static Color ColorPickerBackgroundDocked = Color.FromArgb(248, 248, 248);
  internal static Color Black = Color.Black;
  internal static Color Brown = Color.FromArgb(153, 51, 0);
  internal static Color OliveGreen = Color.FromArgb(51, 51, 0);
  internal static Color DarkGreen = Color.FromArgb(0, 51, 0);
  internal static Color DarkTeal = Color.FromArgb(0, 51, 102);
  internal static Color DarkBlue = Color.FromArgb(0, 0, 128 /*0x80*/);
  internal static Color Indigo = Color.FromArgb(51, 51, 153);
  internal static Color Gray80 = Color.FromArgb(51, 51, 51);
  internal static Color DarkRed = Color.FromArgb(128 /*0x80*/, 0, 0);
  internal static Color Orange = Color.FromArgb((int) byte.MaxValue, 102, 0);
  internal static Color DarkYellow = Color.FromArgb(128 /*0x80*/, 128 /*0x80*/, 0);
  internal static Color Green = Color.Green;
  internal static Color Teal = Color.Teal;
  internal static Color Blue = Color.Blue;
  internal static Color BlueGray = Color.FromArgb(102, 102, 153);
  internal static Color Gray50 = Color.FromArgb(128 /*0x80*/, 128 /*0x80*/, 128 /*0x80*/);
  internal static Color Red = Color.Red;
  internal static Color LightOrange = Color.FromArgb((int) byte.MaxValue, 153, 0);
  internal static Color Lime = Color.FromArgb(153, 204, 0);
  internal static Color SeaGreen = Color.FromArgb(51, 153, 102);
  internal static Color Aqua = Color.FromArgb(51, 204, 204);
  internal static Color LightBlue = Color.FromArgb(51, 102, (int) byte.MaxValue);
  internal static Color Violet = Color.FromArgb(128 /*0x80*/, 0, 128 /*0x80*/);
  internal static Color Gray40 = Color.FromArgb(153, 153, 153);
  internal static Color Pink = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
  internal static Color Gold = Color.FromArgb((int) byte.MaxValue, 204, 0);
  internal static Color Yellow = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
  internal static Color BrightGreen = Color.FromArgb(0, (int) byte.MaxValue, 0);
  internal static Color Turquoise = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue);
  internal static Color SkyBlue = Color.FromArgb(0, 204, (int) byte.MaxValue);
  internal static Color Plum = Color.FromArgb(153, 51, 102);
  internal static Color Gray25 = Color.FromArgb(192 /*0xC0*/, 192 /*0xC0*/, 192 /*0xC0*/);
  internal static Color Rose = Color.FromArgb((int) byte.MaxValue, 153, 204);
  internal static Color Tan = Color.FromArgb((int) byte.MaxValue, 204, 153);
  internal static Color LightYellow = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 153);
  internal static Color LightGreen = Color.FromArgb(204, (int) byte.MaxValue, 204);
  internal static Color LightTurquoise = Color.FromArgb(204, (int) byte.MaxValue, (int) byte.MaxValue);
  internal static Color PaleBlue = Color.FromArgb(153, 204, (int) byte.MaxValue);
  internal static Color Lavender = Color.FromArgb(204, 153, (int) byte.MaxValue);
  internal static Color White = Color.White;
  internal static Color[] SelectableColors = new Color[40]
  {
    CustomColors.Black,
    CustomColors.Brown,
    CustomColors.OliveGreen,
    CustomColors.DarkGreen,
    CustomColors.DarkTeal,
    CustomColors.DarkBlue,
    CustomColors.Indigo,
    CustomColors.Gray80,
    CustomColors.DarkRed,
    CustomColors.Orange,
    CustomColors.DarkYellow,
    CustomColors.Green,
    CustomColors.Teal,
    CustomColors.Blue,
    CustomColors.BlueGray,
    CustomColors.Gray50,
    CustomColors.Red,
    CustomColors.LightOrange,
    CustomColors.Lime,
    CustomColors.SeaGreen,
    CustomColors.Aqua,
    CustomColors.LightBlue,
    CustomColors.Violet,
    CustomColors.Gray40,
    CustomColors.Pink,
    CustomColors.Gold,
    CustomColors.Yellow,
    CustomColors.BrightGreen,
    CustomColors.Turquoise,
    CustomColors.SkyBlue,
    CustomColors.Plum,
    CustomColors.Gray25,
    CustomColors.Rose,
    CustomColors.Tan,
    CustomColors.LightYellow,
    CustomColors.LightGreen,
    CustomColors.LightTurquoise,
    CustomColors.PaleBlue,
    CustomColors.Lavender,
    CustomColors.White
  };
  /// <summary>
  /// Provides a list of color names that matches the SelectableColors array.
  /// </summary>
  internal static string[] SelectableColorsNames = new string[41]
  {
    "Черный",
    "Коричневый",
    "Оливковый",
    "Темно-зеленый",
    "Темно-сизый",
    "Темно-синий",
    "Индиго",
    "Серый 80%",
    "Темно-красный",
    "Оранжевый",
    "Коричнево-зеленый",
    "Зеленый",
    "Сине-зеленый",
    "Синий",
    "Сизый",
    "Серый 50%",
    "Красный",
    "Светло-оранжевый",
    "Травяной",
    "Изумрудный",
    "Темно-бирюзовый",
    "Темно-голубой",
    "Фиолетовый",
    "Серый 40%",
    "Лиловый",
    "Золотистый",
    "Желтый",
    "Ярко-зеленый",
    "Бирюзовый",
    "Голубой",
    "Вишневый",
    "Серый 25%",
    "Розовый",
    "Светло-коричневый",
    "Светло-желтый",
    "Бледно-зеленый",
    "Светло-бирюзовый",
    "Светло-голубой",
    "Сиреневый",
    "Белый",
    "Дополнительные цвета"
  };

  /// <summary>Compare 2 colors by their RGB properties.</summary>
  /// <param name="color1"></param>
  /// <param name="color2"></param>
  /// <returns>True when R,G and B properties of both colors are equals</returns>
  internal static bool ColorEquals(Color color1, Color color2)
  {
    return (int) color1.R == (int) color2.R && (int) color1.G == (int) color2.G && (int) color1.B == (int) color2.B;
  }
}
