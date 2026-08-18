
// Type: Intermech.Client.Core.Navigator.Drawing.ColorsElementProprties
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;
using System.Drawing.Drawing2D;


namespace Intermech.Client.Core.Navigator.Drawing;

public class ColorsElementProprties
{
  /// <summary>Цвет фона  элемента</summary>
  public Color Background = SystemColors.Window;
  /// <summary>Цвет текста элемента</summary>
  public Color Foreground = SystemColors.WindowText;
  /// <summary>Начальный цвет градиентной фона элемента</summary>
  public Color BkStartColor = SystemColors.Window;
  /// <summary>Конечный цвет градиентной заливки фона элемента.</summary>
  public Color BkEndColor = SystemColors.Window;
  /// <summary>Цвет фона  элемента</summary>
  public Color DefaultBackground = SystemColors.Window;
  /// <summary>Цвет текста элемента</summary>
  public Color DefaultForeground = SystemColors.WindowText;
  /// <summary>Начальный цвет градиентной фона элемента</summary>
  public Color DefaultBkStartColor = SystemColors.Window;
  /// <summary>Конечный цвет градиентной заливки фона элемента.</summary>
  public Color DefaultBkEndColor = SystemColors.Window;
  /// <summary>Режим градиентной заливки.</summary>
  public LinearGradientMode DefaultGradientMode = LinearGradientMode.ForwardDiagonal;
  /// <summary>Режим градиентной заливки.</summary>
  public LinearGradientMode GradientMode = LinearGradientMode.ForwardDiagonal;
  /// <summary>
  /// разрешено ли использование вообще для данного элемента градиента
  /// </summary>
  private bool canUseGradient = true;
  /// <summary>используется ли градиент</summary>
  private bool useGradient = true;
  private string Name = string.Empty;

  public bool CanUseGradient
  {
    set
    {
      if (!value)
        this.canUseGradient = this.useGradient = value;
      else
        this.canUseGradient = value;
    }
    get => this.canUseGradient;
  }

  public bool UseGradient
  {
    set
    {
      if (value)
        this.canUseGradient = this.useGradient = value;
      else
        this.useGradient = value;
    }
    get => this.useGradient;
  }

  public ColorsElementProprties(
    string Name,
    Color Background,
    Color Foreground,
    Color BkStartColor,
    Color BkEndColor,
    LinearGradientMode GradientMode,
    bool UseGradient)
  {
    this.Name = Name;
    this.Background = Background;
    this.Foreground = Foreground;
    this.BkStartColor = BkStartColor;
    this.BkEndColor = BkEndColor;
    this.GradientMode = GradientMode;
    this.UseGradient = UseGradient;
  }

  public ColorsElementProprties(string Name, Color Background, Color Foreground)
  {
    this.Name = Name;
    this.Background = Background;
    this.Foreground = Foreground;
    this.BkStartColor = Color.Empty;
    this.BkEndColor = Color.Empty;
    this.UseGradient = this.CanUseGradient = false;
  }

  public ColorsElementProprties(string Name) => this.Name = Name;

  public override string ToString() => this.Name;
}
