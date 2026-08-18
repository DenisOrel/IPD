
// Type: OfficePickers.Util.GradientPanel
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace OfficePickers.Util;

/// <summary>
/// Enables you to group control in a panel with a gradient background.
/// </summary>
[ToolboxItem(true)]
[Description("Enables you to group control in a panel with a gradient background.")]
[ToolboxBitmap(typeof (GradientPanel), "GradientPanel")]
public class GradientPanel : Panel
{
  private Color _startColor = Color.Black;
  private Color _endColor = Color.White;
  private LinearGradientMode _gradientMode = LinearGradientMode.BackwardDiagonal;

  /// <summary>
  /// Gets or sets the left/upper color for the gradient panel
  /// </summary>
  [Category("Appearance")]
  [Description("The left/upper color for the gradient panel.")]
  public Color StartColor
  {
    get => this._startColor;
    set
    {
      this._startColor = value;
      this.Refresh();
    }
  }

  /// <summary>
  /// Gets or sets the right/lower color for the gradient panel
  /// </summary>
  [Category("Appearance")]
  [Description("The right/lower color for the gradient panel.")]
  public Color EndColor
  {
    get => this._endColor;
    set
    {
      this._endColor = value;
      this.Refresh();
    }
  }

  /// <summary>Gets or sets the direction of the linear gradient</summary>
  [Category("Appearance")]
  [Description("The direction of the linear gradient.")]
  public LinearGradientMode GradientMode
  {
    get => this._gradientMode;
    set
    {
      this._gradientMode = value;
      this.Refresh();
    }
  }

  /// <summary>Paints the background in the gradient brush</summary>
  /// <param name="e"></param>
  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(pevent.ClipRectangle, this._startColor, this._endColor, this._gradientMode);
    pevent.Graphics.FillRectangle((Brush) linearGradientBrush, pevent.ClipRectangle);
    linearGradientBrush.Dispose();
  }
}
