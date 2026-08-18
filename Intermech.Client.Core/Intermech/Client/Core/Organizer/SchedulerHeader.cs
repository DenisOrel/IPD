
// Type: Intermech.Client.Core.Organizer.SchedulerHeader
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
internal class SchedulerHeader
{
  private Control _parent;
  private List<SchedulerHeaderButton> _buttons = new List<SchedulerHeaderButton>(3);
  private List<SchedulerHeaderRadioButton> _radioButtons = new List<SchedulerHeaderRadioButton>(2);
  private SchedulerHeaderRenderer _renderer = new SchedulerHeaderRenderer();
  private Rectangle _bounds = new Rectangle(1, 1, 0, 27);
  private Font _font;
  private bool _focused;
  private Rectangle _summaryButtonsBounds = Rectangle.Empty;
  private ImageList _imgList = new ImageList();

  /// <summary>
  /// 
  /// </summary>
  internal Rectangle Bounds
  {
    get => this._bounds;
    set => this._bounds = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal List<SchedulerHeaderButton> Buttons => this._buttons;

  /// <summary>
  /// 
  /// </summary>
  internal bool Focused
  {
    get => this._focused;
    set => this._focused = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal Font Font => this._font;

  [DefaultValue(27)]
  internal int Height
  {
    get => this._bounds.Height;
    set
    {
      this._bounds.Height = value;
      foreach (SchedulerHeaderButton button in this._buttons)
        button.Height = value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal ImageList Images
  {
    get => this._imgList;
    set => this._imgList = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal Control Parent
  {
    get => this._parent;
    set => this._parent = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal List<SchedulerHeaderRadioButton> RadioButtons => this._radioButtons;

  /// <summary>
  /// 
  /// </summary>
  internal int Width
  {
    get => this._bounds.Width;
    set => this._bounds.Width = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="font"></param>
  internal SchedulerHeader(Font font)
  {
    this._font = font;
    this._imgList.ImageSize = new Size(12, 12);
    this._imgList.ColorDepth = ColorDepth.Depth24Bit;
    this.LoadResources();
    List<SchedulerHeaderButton> buttons = this._buttons;
    SchedulerHeaderButton[] collection = new SchedulerHeaderButton[3];
    Rectangle bounds1 = this.Bounds;
    int x1 = bounds1.X;
    bounds1 = this.Bounds;
    int y1 = bounds1.Y;
    int height1 = this.Height;
    collection[0] = new SchedulerHeaderButton(new Rectangle(x1, y1, 91, height1), LocalizationHolder.rm.GetString("Client.Core_1542"), 0);
    Rectangle bounds2 = this.Bounds;
    int x2 = bounds2.X + 91;
    bounds2 = this.Bounds;
    int y2 = bounds2.Y;
    int height2 = this.Height;
    collection[1] = new SchedulerHeaderButton(new Rectangle(x2, y2, 104, height2), LocalizationHolder.rm.GetString("Client.Core_1543"), 1);
    Rectangle bounds3 = this.Bounds;
    int x3 = bounds3.X + 195;
    bounds3 = this.Bounds;
    int y3 = bounds3.Y;
    int height3 = this.Height;
    collection[2] = new SchedulerHeaderButton(new Rectangle(x3, y3, 100, height3), LocalizationHolder.rm.GetString("Client.Core_1544"), 3);
    buttons.AddRange((IEnumerable<SchedulerHeaderButton>) collection);
    this._buttons[0].Active = true;
    Rectangle bounds4 = this.Bounds;
    int x4 = bounds4.X;
    bounds4 = this.Bounds;
    int y4 = bounds4.Y;
    int height4 = this.Height;
    this._summaryButtonsBounds = new Rectangle(x4, y4, 295, height4);
    SchedulerHeaderRadioButton headerRadioButton = new SchedulerHeaderRadioButton(new Point(314, this.Height / 2), this._imgList.ImageSize, LocalizationHolder.rm.GetString("Client.Core_1545"), this._font);
    this._radioButtons.Add(headerRadioButton);
    this._radioButtons.Add(new SchedulerHeaderRadioButton(new Point(headerRadioButton.Bounds.Right + 10, this.Height / 2), this._imgList.ImageSize, LocalizationHolder.rm.GetString("Client.Core_1546"), this._font)
    {
      Checked = true
    });
  }

  /// <summary>
  /// 
  /// </summary>
  [Description("")]
  internal event SchedulerHeader.ClickEventHandler ButtonClick;

  /// <summary>
  /// 
  /// </summary>
  [Description("")]
  internal event SchedulerHeader.ClickEventHandler RadioButtonClick;

  /// <summary>
  /// 
  /// </summary>
  private void LoadResources()
  {
    Assembly assembly = typeof (SchedulerHeader).Assembly;
    this._imgList.Images.Add("Empty", ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Client.Core.Resources.Empty.ico"));
    this._imgList.Images.Add("EmptyHovered", ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Client.Core.Resources.EmptyHovered.ico"));
    this._imgList.Images.Add("Checked", ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Client.Core.Resources.Checked.ico"));
    this._imgList.Images.Add("CheckedHovered", ResourceHelper.GetResourceData<Icon>(assembly, "Intermech.Client.Core.Resources.CheckedHovered.ico"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  private void OnButtonClick(int index)
  {
    if (this.ButtonClick == null)
      return;
    this.ButtonClick((object) this, index);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  private void OnRadioButtonClick(int index)
  {
    if (this.RadioButtonClick == null)
      return;
    this.RadioButtonClick((object) this, index);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public void OnMouseClick(MouseEventArgs e)
  {
    if (this._summaryButtonsBounds.Contains(e.Location))
    {
      int num = -1;
      foreach (SchedulerHeaderButton button in this._buttons)
      {
        if (button.Bounds.Contains(e.Location))
        {
          button.State = InputState.Clicked;
          button.Active = true;
          num = button.Index;
        }
        else
        {
          button.State = InputState.Normal;
          button.Active = false;
        }
      }
      this.OnButtonClick(num == 1 ? (this._radioButtons[0].Checked ? 1 : 2) : num);
    }
    else
    {
      if (!this._buttons[1].Active)
        return;
      if (this._radioButtons[0].Bounds.Contains(e.Location))
      {
        this._radioButtons[0].Checked = true;
        this._radioButtons[1].Checked = false;
        this.OnRadioButtonClick(0);
      }
      else
      {
        if (!this._radioButtons[1].Bounds.Contains(e.Location))
          return;
        this._radioButtons[1].Checked = true;
        this._radioButtons[0].Checked = false;
        this.OnRadioButtonClick(1);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public void OnDraw(PaintEventArgs e)
  {
    SchedulerHeaderRendererEventArgs e1 = new SchedulerHeaderRendererEventArgs(this, e.Graphics);
    this._renderer.OnDrawBackground(e1);
    this._renderer.OnDrawButtons(e1);
    if (this._buttons[1].Active)
      this._renderer.OnDrawRadioButtons(e1);
    this._renderer.OnDrawBorder(e1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  public void OnMouseMove(MouseEventArgs e)
  {
    Rectangle bounds;
    if (!this._summaryButtonsBounds.Contains(e.Location))
    {
      if (this._buttons[1].Active)
      {
        bounds = this._radioButtons[0].Bounds;
        if (!bounds.Contains(e.Location))
        {
          if (this._buttons[1].Active)
          {
            bounds = this._radioButtons[1].Bounds;
            if (bounds.Contains(e.Location))
              goto label_5;
          }
        }
        else
          goto label_5;
      }
      this._parent.Cursor = Cursors.Default;
      goto label_7;
    }
label_5:
    this._parent.Cursor = Cursors.Hand;
label_7:
    foreach (SchedulerHeaderButton button in this._buttons)
    {
      bounds = button.Bounds;
      button.State = bounds.Contains(e.Location) ? InputState.Hovered : InputState.Normal;
    }
    foreach (SchedulerHeaderRadioButton radioButton in this._radioButtons)
      radioButton.State = radioButton.Bounds.Contains(e.Location) ? InputState.Hovered : InputState.Normal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  public void SetActiveButton(int index)
  {
    foreach (SchedulerHeaderButton button in this._buttons)
    {
      if (button.Index == index)
      {
        button.State = InputState.Clicked;
        button.Active = true;
      }
      else
      {
        button.State = InputState.Normal;
        button.Active = false;
      }
    }
  }

  public void SetCheckedRadioButton(int index)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="index"></param>
  internal delegate void ClickEventHandler(object sender, int index);
}
