
// Type: Intermech.Client.Core.Organizer.ImageButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>Кнокпа органайзера.</summary>
[Designer(typeof (ImageButtonDesigner))]
[ToolboxItem(false)]
public class ImageButton : Control
{
  private Image _img;
  private Image _imgFocused;
  private Image _imgPressed;
  private ImageButton.State _btnState = ImageButton.State.None;
  private Image _currentImg;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Кнопка в обычном состоянии.</summary>
  [DefaultValue(null)]
  public Image Image
  {
    get => this._img;
    set
    {
      this._img = value;
      int width = 24;
      int height = 24;
      if (this._img != null)
      {
        width = this._img.Width;
        height = this._img.Height % 2 != 0 ? this._img.Height + 1 : this._img.Height;
      }
      this.Size = new Size(width, height);
      this._currentImg = this._img;
    }
  }

  /// <summary>Кнопка в фокусе.</summary>
  [DefaultValue(null)]
  public Image ImageFocused
  {
    get => this._imgFocused;
    set => this._imgFocused = value;
  }

  /// <summary>Кнопка нажата.</summary>
  [DefaultValue(null)]
  public Image ImagePressed
  {
    get => this._imgPressed;
    set => this._imgPressed = value;
  }

  /// <summary>Конструктор.</summary>
  public ImageButton()
  {
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this.DoubleBuffered = true;
    this.BackColor = Color.Transparent;
  }

  /// <summary>Нажатие кнопки мышью.</summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    this._btnState |= ImageButton.State.Pressed;
    this.SetButtonImage();
  }

  /// <summary>Наведение курсора мыши на кнопку.</summary>
  /// <param name="e"></param>
  protected override void OnMouseHover(EventArgs e)
  {
    base.OnMouseHover(e);
    this._btnState |= ImageButton.State.Focused;
    this.SetButtonImage();
  }

  /// <summary>Уход курсора мыши с кнопки.</summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this._btnState ^= ImageButton.State.Focused;
    this.SetButtonImage();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    this._btnState ^= ImageButton.State.Pressed;
    this.SetButtonImage();
  }

  /// <summary>Отрисовка контрола.</summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    e.Graphics.DrawImage(this._currentImg, 0, 0);
  }

  /// <summary>Установка текущей картинки кнопки.</summary>
  private void SetButtonImage()
  {
    this._currentImg = (this._btnState & ImageButton.State.Pressed) == (ImageButton.State) 0 ? ((this._btnState & ImageButton.State.Focused) == (ImageButton.State) 0 ? this._img : this._imgFocused) : this._imgPressed;
    this.Invalidate();
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
  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.ResumeLayout(false);
  }

  /// <summary>Состояние кнопки.</summary>
  [Flags]
  private enum State
  {
    None = 1,
    Focused = 2,
    Pressed = 4,
  }
}
