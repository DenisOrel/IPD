
// Type: Intermech.UI.Colour
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;


namespace Intermech.UI;

/// <summary>
/// Stores a colour and provides conversion between the RGB and HLS colour models
/// </summary>
internal class Colour
{
  public const int HUEMAX = 360;
  public const float SATMAX = 1f;
  public const float BRIGHTMAX = 1f;
  public const int RGBMAX = 255 /*0xFF*/;
  private Color m_clrCurrent = Color.Red;

  /// <summary>The current colour (RGB model)</summary>
  public Color CurrentColour
  {
    get => this.m_clrCurrent;
    set => this.m_clrCurrent = value;
  }

  /// <summary>The Red component of the current colour</summary>
  public byte Red
  {
    get => this.m_clrCurrent.R;
    set => this.m_clrCurrent = Color.FromArgb((int) value, (int) this.Green, (int) this.Blue);
  }

  /// <summary>The Green component of the current colour</summary>
  public byte Green
  {
    get => this.m_clrCurrent.G;
    set => this.m_clrCurrent = Color.FromArgb((int) this.Red, (int) value, (int) this.Blue);
  }

  /// <summary>The Blue component of the current colour</summary>
  public byte Blue
  {
    get => this.m_clrCurrent.B;
    set => this.m_clrCurrent = Color.FromArgb((int) this.Red, (int) this.Green, (int) value);
  }

  /// <summary>The Hue component of the current colour</summary>
  public int Hue
  {
    get => (int) this.m_clrCurrent.GetHue();
    set
    {
      this.m_clrCurrent = Colour.HSBToRGB(value, this.m_clrCurrent.GetSaturation(), this.m_clrCurrent.GetBrightness());
    }
  }

  public float GetHue()
  {
    return (float) Math.Acos((double) (2 * (int) this.Red - (int) this.Green - (int) this.Blue) / 510.0 / Math.Sqrt((double) ((((int) this.Red - (int) this.Green) * ((int) this.Red - (int) this.Green) + ((int) this.Red - (int) this.Blue) * ((int) this.Green - (int) this.Blue)) / (int) byte.MaxValue)));
  }

  public float GetSaturation()
  {
    return (float) (((double) byte.MaxValue - (double) ((int) this.Red + (int) this.Green + (int) this.Blue) / 3.0 * (double) Math.Min(this.Red, Math.Min(this.Green, this.Blue))) / (double) byte.MaxValue);
  }

  public float GetBrightness()
  {
    return (float) ((int) this.Red + (int) this.Green + (int) this.Blue) / 765f;
  }

  /// <summary>The Saturation component of the current colour</summary>
  public float Saturation
  {
    get
    {
      if (0.0 == (double) this.Brightness)
        return 0.0f;
      float num1 = (float) Math.Max(this.Red, Math.Max(this.Green, this.Blue));
      float num2 = (float) Math.Min(this.Red, Math.Min(this.Green, this.Blue));
      return (num1 - num2) / num1;
    }
    set
    {
      this.m_clrCurrent = Colour.HSBToRGB((int) this.m_clrCurrent.GetHue(), value, this.m_clrCurrent.GetBrightness());
    }
  }

  /// <summary>The Brightness component of the current colour</summary>
  public float Brightness
  {
    get => (float) Math.Max(this.Red, Math.Max(this.Green, this.Blue)) / (float) byte.MaxValue;
    set
    {
      this.m_clrCurrent = Colour.HSBToRGB((int) this.m_clrCurrent.GetHue(), this.m_clrCurrent.GetSaturation(), value);
    }
  }

  /// <summary>
  /// Converts HSB colour components to an RGB System.Drawing.Color
  /// </summary>
  /// <param name="Hue">Hue component</param>
  /// <param name="Saturation">Saturation component</param>
  /// <param name="Brightness">Brightness component</param>
  /// <returns>Returns the RGB value as a System.Drawing.Color</returns>
  public static Color HSBToRGB(int Hue, float Saturation, float Brightness)
  {
    int blue;
    int green;
    int red;
    if ((double) Saturation == 0.0)
    {
      int num;
      blue = num = (int) ((double) Brightness * (double) byte.MaxValue);
      green = num;
      red = num;
    }
    else
    {
      double num1;
      float num2 = (float) Math.Floor(num1 = 0.01666666753590107 * (double) Hue);
      double num3 = (double) num2;
      float num4 = (float) (num1 - num3);
      float num5 = Brightness * (float) byte.MaxValue;
      float num6 = Saturation;
      byte num7 = (byte) (0.5 + (double) num5 * (1.0 - (double) num6));
      byte num8 = (byte) (0.5 + (double) num5 * (1.0 - (double) num6 * (double) num4));
      byte num9 = (byte) (0.5 + (double) num5 * (1.0 - (double) num6 * (1.0 - (double) num4)));
      switch ((int) num2)
      {
        case 0:
          red = (int) ((double) Brightness * (double) byte.MaxValue);
          green = (int) num9;
          blue = (int) num7;
          break;
        case 1:
          red = (int) num8;
          green = (int) ((double) Brightness * (double) byte.MaxValue);
          blue = (int) num7;
          break;
        case 2:
          red = (int) num7;
          green = (int) ((double) Brightness * (double) byte.MaxValue);
          blue = (int) num9;
          break;
        case 3:
          red = (int) num7;
          green = (int) num8;
          blue = (int) ((double) Brightness * (double) byte.MaxValue);
          break;
        case 4:
          red = (int) num9;
          green = (int) num7;
          blue = (int) ((double) Brightness * (double) byte.MaxValue);
          break;
        case 5:
          red = (int) ((double) Brightness * (double) byte.MaxValue);
          green = (int) num7;
          blue = (int) num8;
          break;
        default:
          red = 0;
          green = 0;
          blue = 0;
          break;
      }
    }
    return Color.FromArgb(red, green, blue);
  }
}
