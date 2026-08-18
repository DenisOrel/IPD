
// Type: Intermech.UI.Wpf.WinformsInterop.WpfElementHost
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;


namespace Intermech.UI.Wpf.WinformsInterop;

/// <summary>
/// Доработанный ElementHost с исправлением проблем в базовой реализации
/// </summary>
public class WpfElementHost : ElementHost
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создает объект</summary>
  public WpfElementHost()
  {
    WinformsInteropInitializer.Instance.Initialize();
    this.SetStyle(ControlStyles.Opaque, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.InitializeComponent();
    this.AdjustPropertyMap();
    this.AdjustHostContainer();
  }

  private void AdjustPropertyMap()
  {
    this.PropertyMap.Remove("BackColor");
    this.PropertyMap.Remove("BackgroundImage");
    this.PropertyMap.Remove("BackgroundImageLayout");
  }

  private void AdjustHostContainer()
  {
    this.HostContainer.Background = (System.Windows.Media.Brush) this.CreateWpfSolidColorBrush(this.BackColor);
  }

  private System.Windows.Media.Color CreateWpfColor(System.Drawing.Color winformsColor)
  {
    return System.Windows.Media.Color.FromArgb(winformsColor.A, winformsColor.R, winformsColor.G, winformsColor.B);
  }

  private SolidColorBrush CreateWpfSolidColorBrush(System.Drawing.Color winformsColor)
  {
    return new SolidColorBrush(this.CreateWpfColor(winformsColor));
  }

  protected override void OnPaintBackground(PaintEventArgs e)
  {
  }

  protected override void OnPaint(PaintEventArgs e)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
