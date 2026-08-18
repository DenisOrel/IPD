
// Type: Intermech.Controls.ContextMenuItemSurrogate
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Controls;

public class ContextMenuItemSurrogate : 
  Control,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IPopupControlHost,
  IPopupMenuItem,
  IArrowKeysNavigationSupported
{
  public static readonly Color DefaultBackColor = SystemColors.Menu;
  public const string DefaultBackColorName = "Menu";
  protected static readonly Color DefaultActiveBackColor = SystemColors.MenuHighlight;
  protected const string DefaultActiveBackColorName = "MenuHighlight";
  private Color _activeBackColor = ContextMenuItemSurrogate.DefaultActiveBackColor;
  private const int DefaultCheckedBgColorLighterBy = 70;
  private const int DefaultCheckedBorderColorLighterBy = -30;
  private Color _checkedBgColor = ContextMenuItemSurrogate.GetDefaultCheckedBgColor(ContextMenuItemSurrogate.DefaultActiveBackColor);
  private bool _checkedBgColorManuallyChoosen;
  private Color _checkedBorderColor = ContextMenuItemSurrogate.GetDefaultCheckedBorderColor(ContextMenuItemSurrogate.DefaultActiveBackColor);
  private bool _checkedBorderColorManuallyChoosen;
  private Brush _checkedBgColorBrush;
  private Pen _checkedBorderColorPen;
  private Brush _activeForeColorBrush;
  private Brush _activeBackColorBrush;
  private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
  protected StringFormat _stringFormat = new StringFormat();
  private object _lockObject = new object();
  private Brush _textBrush;
  private Image _image;
  private Control _dropDownControl;
  private const int DefaultDropDownDelay = 100;
  private int _dropDownDelay = 100;
  /// <summary>Popup control.</summary>
  private PopupControl m_popupCtrl = new PopupControl();
  private System.Windows.Forms.Timer _showDropDownTimer;
  private bool _active;
  private System.Windows.Forms.Timer _focusDropDownTimer;
  private System.Windows.Forms.Timer _mouseCursorTimer;
  private Lazy<IPopupMenu> _parentPopupMenu;
  private bool _isDroppedDown;
  private Point _oldMousePosition = Point.Empty;
  public Func<Control> GetDropDownControl;
  private int _lockCheckIsActiveCounter;
  private bool _hasDropDownControl;
  private string _radioGroupName = string.Empty;
  private bool _checked;
  private Lazy<Control> _parentUserControlOrForm;
  private ImageList _imageList;
  private int _imageIndex;
  protected static readonly Color DefaultBorderColor = Color.Empty;
  protected const string DefaultBorderColorName = "Empty";
  private Color _borderColor = ContextMenuItemSurrogate.DefaultBorderColor;
  private Pen _borderPen;
  public const AnchorStyles AllBorders = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
  private AnchorStyles _borders = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
  private Control _upControl;
  private Control _downControl;
  private Control _leftControl;
  private Control _rightControl;

  public ContextMenuItemSurrogate()
  {
    this._parentUserControlOrForm = new Lazy<Control>(new Func<Control>(this.FindParentUserControlOrForm));
    this._parentPopupMenu = new Lazy<IPopupMenu>(new Func<IPopupMenu>(this.FindParentPopupMenu));
    base.BackColor = ContextMenuItemSurrogate.DefaultBackColor;
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.Selectable, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this._stringFormat.Trimming = StringTrimming.EllipsisCharacter;
    this._stringFormat.FormatFlags = StringFormatFlags.NoWrap;
    this.CheckStringFormatAligment();
  }

  public void CheckStringFormatAligment()
  {
    this._stringFormat.Alignment = this._textAlign.ToStringAlignment(Axis.Horizontal);
    this._stringFormat.LineAlignment = this._textAlign.ToStringAlignment(Axis.Vertical);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.DisposeObj<Brush>(ref this._textBrush);
      this.DisposeObj<Brush>(ref this._activeBackColorBrush);
      this.DisposeObj<Brush>(ref this._activeForeColorBrush);
      this.DisposeObj<Brush>(ref this._checkedBgColorBrush);
      this.DisposeObj<Pen>(ref this._checkedBorderColorPen);
      this.DisposeObj<Pen>(ref this._borderPen);
      this.DisposeObj<StringFormat>(ref this._stringFormat);
      this.DisposeTimer(ref this._showDropDownTimer);
      this.DisposeTimer(ref this._focusDropDownTimer);
      this.DisposeTimer(ref this._mouseCursorTimer);
      if (this._dropDownControl != null && this._dropDownControl is IArrowKeysNavigationSupported)
        ((IArrowKeysNavigationSupported) this._dropDownControl).OnNavigateToLeft -= new OnNavigateDelegate(this.dropDownControl_OnNavigateToLeft);
      this._dropDownControl = (Control) null;
    }
    base.Dispose(disposing);
  }

  private void DisposeTimer(ref System.Windows.Forms.Timer timer)
  {
    if (timer != null && timer.Enabled)
      timer.Stop();
    this.DisposeObj<System.Windows.Forms.Timer>(ref timer);
  }

  protected override void OnPaintBackground(PaintEventArgs e)
  {
    if (!this._active)
      base.OnPaintBackground(e);
    else
      e.Graphics.FillRectangle(this.ActiveBackColorBrush, this.ClientRectangle);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "MenuHighlight")]
  [RefreshProperties(RefreshProperties.All)]
  public virtual Color ActiveBackColor
  {
    [DebuggerStepThrough] get => this._activeBackColor;
    set
    {
      if (value == Color.Empty)
        value = ContextMenuItemSurrogate.DefaultActiveBackColor;
      if (!(this._activeBackColor != value))
        return;
      this._activeBackColor = value;
      this.DisposeObj<Brush>(ref this._activeBackColorBrush);
      if (!this._checkedBgColorManuallyChoosen)
        this.CheckedBgColor = ContextMenuItemSurrogate.GetDefaultCheckedBgColor(this._activeBackColor);
      if (!this._checkedBorderColorManuallyChoosen)
        this.CheckedBorderColor = ContextMenuItemSurrogate.GetDefaultCheckedBorderColor(this._activeBackColor);
      if (!this.Active)
        return;
      this.Invalidate();
    }
  }

  private static Color GetDefaultCheckedBgColor(Color activeBackColor)
  {
    return activeBackColor.LighterBy(70);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public Color CheckedBgColor
  {
    get => this._checkedBgColor;
    set
    {
      if (value == Color.Empty)
        value = ContextMenuItemSurrogate.GetDefaultCheckedBgColor(this._activeBackColor);
      if (!(this._checkedBgColor != value))
        return;
      this._checkedBgColor = value;
      this._checkedBgColorManuallyChoosen = value != this._activeBackColor.LighterBy(70);
      this.DisposeObj<Brush>(ref this._checkedBgColorBrush);
      if (!this.Checked)
        return;
      this.Invalidate();
    }
  }

  public bool ShouldSerializeCheckedBgColor() => this._checkedBgColorManuallyChoosen;

  private static Color GetDefaultCheckedBorderColor(Color activeBackColor)
  {
    return activeBackColor.LighterBy(-30);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public Color CheckedBorderColor
  {
    get => this._checkedBorderColor;
    set
    {
      if (value == Color.Empty)
        value = ContextMenuItemSurrogate.GetDefaultCheckedBorderColor(this._activeBackColor);
      if (!(this._checkedBorderColor != value))
        return;
      this._checkedBorderColor = value;
      this._checkedBorderColorManuallyChoosen = value != this._activeBackColor.LighterBy(-30);
      this.DisposeObj<Pen>(ref this._checkedBorderColorPen);
      if (!this.Checked)
        return;
      this.Invalidate();
    }
  }

  public bool ShouldSerializeCheckedBorderColor() => this._checkedBorderColorManuallyChoosen;

  protected Brush CheckedBgColorBrush
  {
    get
    {
      return LazyInitializer.EnsureInitialized<Brush>(ref this._checkedBgColorBrush, (Func<Brush>) (() => (Brush) new SolidBrush(this._checkedBgColor)));
    }
  }

  protected Pen CheckedBorderColorPen
  {
    get
    {
      return LazyInitializer.EnsureInitialized<Pen>(ref this._checkedBorderColorPen, (Func<Pen>) (() => new Pen(this._checkedBorderColor)));
    }
  }

  public Brush ActiveForeColorBrush
  {
    get
    {
      return LazyInitializer.EnsureInitialized<Brush>(ref this._activeForeColorBrush, (Func<Brush>) (() => this._activeBackColor.GetInvertedBlackWhiteBrush()));
    }
  }

  private Brush ActiveBackColorBrush
  {
    get
    {
      return LazyInitializer.EnsureInitialized<Brush>(ref this._activeBackColorBrush, (Func<Brush>) (() => (Brush) new SolidBrush(this._activeBackColor)));
    }
  }

  [DefaultValue(typeof (Color), "Menu")]
  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      if (value == Color.Empty)
        value = ContextMenuItemSurrogate.DefaultBackColor;
      if (!(base.BackColor != value))
        return;
      base.BackColor = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (ContentAlignment), "MiddleLeft")]
  public virtual ContentAlignment TextAlign
  {
    [DebuggerStepThrough] get => this._textAlign;
    set
    {
      if (this._textAlign == value)
        return;
      this._textAlign = value;
      this.CheckStringFormatAligment();
      this.Invalidate();
    }
  }

  protected void DisposeObj<T>(ref T obj) where T : class, IDisposable
  {
    CommonHelper.SafeDisposeAndNull<T>(this._lockObject, ref obj);
  }

  protected Brush TextBrush
  {
    get
    {
      return !this.Active ? LazyInitializer.EnsureInitialized<Brush>(ref this._textBrush, (Func<Brush>) (() => (Brush) new SolidBrush(this.ForeColor))) : this.ActiveForeColorBrush;
    }
  }

  protected override void OnFontChanged(EventArgs e)
  {
    this.DisposeObj<Brush>(ref this._textBrush);
    base.OnFontChanged(e);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  public Image Image
  {
    [DebuggerStepThrough] get => this._image;
    set
    {
      if (this._image == value)
        return;
      this._image = value;
      this.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual Control DropDownControl
  {
    [DebuggerStepThrough] get => this._dropDownControl;
    set
    {
      if (this._dropDownControl == value)
        return;
      if (this._dropDownControl != null && this._dropDownControl is IArrowKeysNavigationSupported)
        ((IArrowKeysNavigationSupported) this._dropDownControl).OnNavigateToLeft -= new OnNavigateDelegate(this.dropDownControl_OnNavigateToLeft);
      if (value != null && value is IArrowKeysNavigationSupported)
        ((IArrowKeysNavigationSupported) value).OnNavigateToLeft += new OnNavigateDelegate(this.dropDownControl_OnNavigateToLeft);
      this._dropDownControl = value;
      this.HasDropDownControl = true;
    }
  }

  private void dropDownControl_OnNavigateToLeft(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    if (this._isDroppedDown)
    {
      this.LockCheckIsActiveCounter();
      try
      {
        this.HideDropDown();
      }
      finally
      {
        this.UnlockCheckIsActiveCounter();
      }
    }
    blockDefaultNavigation = true;
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(100)]
  private int DropDownDelay
  {
    [DebuggerStepThrough] get => this._dropDownDelay;
    set
    {
      if (this._dropDownDelay != value)
        return;
      this._dropDownDelay = value;
    }
  }

  private System.Windows.Forms.Timer ShowDropDownTimer
  {
    get
    {
      if (this._showDropDownTimer == null)
      {
        lock (this._lockObject)
        {
          if (this._showDropDownTimer == null)
          {
            this._showDropDownTimer = new System.Windows.Forms.Timer();
            this._showDropDownTimer.Interval = this.DropDownDelay;
            this._showDropDownTimer.Tick += new EventHandler(this._showDropDownTimer_Tick);
          }
        }
      }
      return this._showDropDownTimer;
    }
  }

  private void _showDropDownTimer_Tick(object sender, EventArgs e)
  {
    if (this.Active)
      this.ShowDropDown();
    this.ShowDropDownTimer.Stop();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Active
  {
    [DebuggerStepThrough] get => this._active;
    private set
    {
      if (this._active == value)
        return;
      this._active = value;
      this.Invalidate();
    }
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    lock (this._lockObject)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      if (this._borders != AnchorStyles.None && this._borderColor != Color.Empty)
      {
        Rectangle growBy = clientRectangle.GetGrowBy(-1, -1);
        if (this._borders == (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right))
        {
          e.Graphics.DrawRectangle(this.BorderPen, growBy);
        }
        else
        {
          if (this._borders.HasFlag((Enum) AnchorStyles.Left))
            e.Graphics.DrawLine(this.BorderPen, growBy.TopLeft(), growBy.BottomLeft());
          if (this._borders.HasFlag((Enum) AnchorStyles.Top))
            e.Graphics.DrawLine(this.BorderPen, growBy.TopLeft(), growBy.TopRight());
          if (this._borders.HasFlag((Enum) AnchorStyles.Right))
            e.Graphics.DrawLine(this.BorderPen, growBy.TopRight(), growBy.BottomRight());
          if (this._borders.HasFlag((Enum) AnchorStyles.Bottom))
            e.Graphics.DrawLine(this.BorderPen, growBy.BottomRight(), growBy.BottomLeft());
        }
        clientRectangle = this.ClientRectangle;
      }
      Color color = this.Active ? this._activeBackColor : this.BackColor;
      clientRectangle.Inflate(-2, -2);
      if (this.Focused)
        ControlPaint.DrawFocusRectangle(e.Graphics, clientRectangle, color.InvertAsBlackWhite(), color.AsBlackWhite());
      clientRectangle.Inflate(-2, 0);
      Size size = this.ClientSize;
      int y = size.Height >> 1;
      if (this._imageList != null || this._image != null)
      {
        Image image = this._image;
        if (image == null && this._imageIndex >= 0)
        {
          int imageIndex = this._imageIndex;
          size = this._imageList.ImageSize;
          int width = size.Width;
          if (imageIndex < width)
            image = this._imageList.Images[this._imageIndex];
        }
        if (image != null)
          e.Graphics.DrawImageUnscaled(this._image, clientRectangle.Left + 2, y - (this._image.Height >> 1));
        int num1;
        if (image == null)
        {
          if (this._imageList == null)
          {
            num1 = 16 /*0x10*/;
          }
          else
          {
            size = this._imageList.ImageSize;
            num1 = size.Width;
          }
        }
        else
          num1 = image.Width;
        int num2 = num1;
        clientRectangle.X += num2 + 8;
        clientRectangle.Width -= num2 + 8;
      }
      if (this.HasDropDownControl)
      {
        int x = clientRectangle.Right - 2;
        Point[] points = new Point[3]
        {
          new Point(x - 4, y - 4),
          new Point(x, y),
          new Point(x - 4, y + 4)
        };
        e.Graphics.FillPolygon(color.GetInvertedBlackWhiteBrush(), points);
        clientRectangle.Width -= 8;
      }
      bool drawDefaultText = true;
      string text = this.Text;
      this.PaintContent(e, this.TextBrush, color, ref text, ref clientRectangle, ref drawDefaultText);
      if (!(!string.IsNullOrEmpty(text) & drawDefaultText))
        return;
      e.Graphics.DrawString(text, this.Font, this.TextBrush, (RectangleF) clientRectangle, this._stringFormat);
    }
  }

  public event ContextMenuItemSurrogate.PaintContentDelegate OnPaintContent;

  protected virtual void PaintContent(
    PaintEventArgs e,
    Brush TextBrush,
    Color bgColor,
    ref string text,
    ref Rectangle textRectangle,
    ref bool drawDefaultText)
  {
    if (this.OnPaintContent == null)
      return;
    this.OnPaintContent(e, TextBrush, bgColor, ref text, ref textRectangle, ref drawDefaultText);
  }

  protected override void OnMouseEnter(EventArgs e)
  {
    base.OnMouseEnter(e);
    if (!this.Enabled)
      return;
    this.Active = true;
    if (!this.HasDropDownControl)
      return;
    this.ShowDropDownTimer.Start();
  }

  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    if (!this.Enabled || this._isDroppedDown)
      return;
    this.Active = false;
    if (this._showDropDownTimer == null || !this._showDropDownTimer.Enabled)
      return;
    this._showDropDownTimer.Stop();
  }

  protected override void OnEnter(EventArgs e)
  {
    base.OnEnter(e);
    if (this.Enabled)
      this.Active = true;
    this.Invalidate();
  }

  protected override void OnLeave(EventArgs e)
  {
    base.OnLeave(e);
    if (this.Enabled)
      this.Active = false;
    this.Invalidate();
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    if (this.Enabled)
    {
      this.Focus();
      if (this.HasDropDownControl && !this._isDroppedDown)
        this.ShowDropDown();
    }
    base.OnMouseDown(e);
  }

  private System.Windows.Forms.Timer FocusDropDownTimer
  {
    get
    {
      if (this._focusDropDownTimer == null)
      {
        lock (this._lockObject)
        {
          if (this._focusDropDownTimer == null)
          {
            this._focusDropDownTimer = new System.Windows.Forms.Timer();
            this._focusDropDownTimer.Interval = 10;
            this._focusDropDownTimer.Tick += new EventHandler(this._focusDropDownTimer_Tick);
          }
        }
      }
      return this._focusDropDownTimer;
    }
  }

  public event EventHandler OnBeforeShowDropDownControl;

  protected virtual void FireOnBeforeShowDropDownControl(EventArgs e)
  {
    if (this.OnBeforeShowDropDownControl == null)
      return;
    this.OnBeforeShowDropDownControl((object) this, e);
  }

  public event EventHandler OnAutoFocusDroppedDownControl;

  protected virtual void FireOnAutoFocusDroppedDownControl(EventArgs e)
  {
    if (this.OnAutoFocusDroppedDownControl == null)
      return;
    this.OnAutoFocusDroppedDownControl((object) this, e);
  }

  public event EventHandler OnDropDownControlHidden;

  protected virtual void FireOnDropDownControlHidden(EventArgs e)
  {
    if (this.OnDropDownControlHidden == null)
      return;
    this.OnDropDownControlHidden((object) this, e);
  }

  private void _focusDropDownTimer_Tick(object sender, EventArgs e)
  {
    if (!this.m_popupCtrl.Visible || this.DropDownControl.Focused)
      return;
    this.DropDownControl.Focus();
    this._focusDropDownTimer.Stop();
    this.FireOnAutoFocusDroppedDownControl(EventArgs.Empty);
  }

  private System.Windows.Forms.Timer MouseCursorTimer
  {
    get
    {
      if (this._mouseCursorTimer == null)
      {
        lock (this._lockObject)
        {
          if (this._mouseCursorTimer == null)
          {
            this._mouseCursorTimer = new System.Windows.Forms.Timer();
            this._mouseCursorTimer.Interval = 10;
            this._mouseCursorTimer.Tick += new EventHandler(this._mouseCurdorTimer_Tick);
          }
        }
      }
      return this._mouseCursorTimer;
    }
  }

  private void _mouseCurdorTimer_Tick(object sender, EventArgs e)
  {
    if (!(this._oldMousePosition != Control.MousePosition))
      return;
    this._oldMousePosition = Control.MousePosition;
    if (((Control) this.FindForm() ?? this.Parent) == null)
      return;
    Control underMouseCursor = Intermech.Extensions.Controls.GetControlUnderMouseCursor();
    if (underMouseCursor == null)
      return;
    IPopupMenu parentPopupMenu = this.ParentPopupMenu;
    if (parentPopupMenu != null)
    {
      foreach (Control menuItem in parentPopupMenu.MenuItems)
      {
        if (menuItem != PopupControl.TopVisibleControl && underMouseCursor.GetParentsEnumeration(true).Contains<Control>(menuItem))
        {
          this.HideDropDown();
          return;
        }
      }
    }
    IPopupMenuItem menuItemUnderCousor = this.FindOtherMenuItemUnderCousor(underMouseCursor);
    if (menuItemUnderCousor == null)
      return;
    this.HideDropDown();
    menuItemUnderCousor.ProcessMouseEnter();
  }

  private IPopupMenuItem FindOtherMenuItemUnderCousor(Control controlUnderCursor)
  {
    Control control1 = controlUnderCursor.GetParentsEnumeration(true).TakeWhile<Control>((Func<Control, bool>) (control => !(control is Form)), true).FirstOrDefault<Control>((Func<Control, bool>) (control => control is IPopupMenuItem));
    return control1 == null || control1.GetParentsEnumeration(true).TakeWhile<Control>((Func<Control, bool>) (control => !(control is Form)), true).Any<Control>((Func<Control, bool>) (control => control == this || control == this._dropDownControl)) ? (IPopupMenuItem) null : control1 as IPopupMenuItem;
  }

  private IPopupMenu ParentPopupMenu
  {
    [DebuggerStepThrough] get => this._parentPopupMenu.Value;
  }

  private IPopupMenu FindParentPopupMenu()
  {
    return this.GetParentsEnumeration(true).TakeWhile<Control>((Func<Control, bool>) (control => !(control is Form)), true).FirstOrDefault<Control>((Func<Control, bool>) (control => control is IPopupMenu)) as IPopupMenu;
  }

  public void ShowDropDown()
  {
    if (this.HasDropDownControl)
    {
      if (this._dropDownControl == null)
      {
        this._dropDownControl = this.CreateDropDownControl();
        if (this._dropDownControl != null && this._dropDownControl is IArrowKeysNavigationSupported)
          ((IArrowKeysNavigationSupported) this._dropDownControl).OnNavigateToLeft += new OnNavigateDelegate(this.dropDownControl_OnNavigateToLeft);
      }
      if (this._dropDownControl == null)
        this.HasDropDownControl = false;
    }
    if (!this.HasDropDownControl || this.DropDownControl == null || this._isDroppedDown)
      return;
    this.FireOnBeforeShowDropDownControl(EventArgs.Empty);
    Point screen = this.PointToScreen(new Point(this.Width, 0));
    this.m_popupCtrl.Show((Control) this, this.DropDownControl, screen.X, screen.Y);
    this._isDroppedDown = true;
    this.m_popupCtrl.PopupControlHost = (IPopupControlHost) this;
    this._oldMousePosition = Control.MousePosition;
    this.MouseCursorTimer.Start();
    this.FocusDropDownTimer.Start();
  }

  protected virtual Control CreateDropDownControl()
  {
    return this.GetDropDownControl == null ? (Control) null : this.GetDropDownControl();
  }

  public void HideDropDown()
  {
    if (this.FocusDropDownTimer != null && this.FocusDropDownTimer.Enabled)
      this.FocusDropDownTimer.Stop();
    if (this._mouseCursorTimer != null && this._mouseCursorTimer.Enabled)
      this._mouseCursorTimer.Stop();
    if (this.m_popupCtrl != null && this._isDroppedDown)
    {
      this.m_popupCtrl.Hide();
      this._isDroppedDown = false;
      this.FireOnDropDownControlHidden(EventArgs.Empty);
    }
    this.CheckIsActive();
  }

  private void LockCheckIsActiveCounter() => ++this._lockCheckIsActiveCounter;

  private void UnlockCheckIsActiveCounter()
  {
    if (this._lockCheckIsActiveCounter == 0)
      throw new Exception("_lockCheckIsActiveCounter is 0, can`t unlock");
    --this._lockCheckIsActiveCounter;
  }

  private void CheckIsActive()
  {
    if (!this.Enabled || this._lockCheckIsActiveCounter != 0)
      return;
    if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
    {
      if (this.Active)
        return;
      this.Active = true;
      if (!this.HasDropDownControl)
        return;
      this.ShowDropDownTimer.Start();
    }
    else
    {
      if (!this.Active)
        return;
      this.Active = false;
      if (this._showDropDownTimer == null || !this._showDropDownTimer.Enabled)
        return;
      this._showDropDownTimer.Stop();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  public virtual bool HasDropDownControl
  {
    [DebuggerStepThrough] get => this._hasDropDownControl;
    set
    {
      if (this._hasDropDownControl == value)
        return;
      this._hasDropDownControl = value;
      this.Invalidate();
    }
  }

  public void ProcessMouseEnter() => this.CheckIsActive();

  protected override bool IsInputKey(Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Left:
      case Keys.Left | Keys.Shift:
      case Keys.Left | Keys.Control:
        if (this._leftControl != null || this.OnNavigateToLeft != null)
          return true;
        return this.HasDropDownControl && this._isDroppedDown;
      case Keys.Up:
      case Keys.Up | Keys.Shift:
      case Keys.Up | Keys.Control:
        return this._upControl != null || this.OnNavigateToUp != null;
      case Keys.Right:
      case Keys.Right | Keys.Shift:
      case Keys.Right | Keys.Control:
        return this._rightControl != null || this.OnNavigateToRight != null || this.HasDropDownControl;
      case Keys.Down:
      case Keys.Down | Keys.Shift:
      case Keys.Down | Keys.Control:
        return this._downControl != null || this.OnNavigateToDown != null;
      default:
        return base.IsInputKey(keyData);
    }
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    switch (e.KeyCode)
    {
      case Keys.Left:
        if (this.HasDropDownControl && this._isDroppedDown)
        {
          this.HideDropDown();
          break;
        }
        if (this._leftControl == null && this.OnNavigateToLeft == null)
          break;
        this.NavigateToLeft();
        break;
      case Keys.Up:
        if (this._upControl == null && this.OnNavigateToUp == null)
          break;
        this.NavigateToUp();
        break;
      case Keys.Right:
        if (this.HasDropDownControl && !this._isDroppedDown)
        {
          this.ShowDropDown();
          break;
        }
        if (this._rightControl == null && this.OnNavigateToRight == null)
          break;
        this.NavigateToRight();
        break;
      case Keys.Down:
        if (this._downControl == null && this.OnNavigateToDown == null)
          break;
        this.NavigateToDown();
        break;
    }
  }

  public override string Text
  {
    [DebuggerStepThrough] get => base.Text;
    set
    {
      if (!(base.Text != value))
        return;
      base.Text = value;
      this.Invalidate();
    }
  }

  public event ContextMenuItemSurrogate.MenuItemEventHandler OnChecked;

  protected virtual void FireOnChecked()
  {
    if (this.OnChecked == null)
      return;
    this.OnChecked(this);
  }

  public string RadioGroupName
  {
    get => this._radioGroupName;
    set
    {
      if (!(this._radioGroupName != value))
        return;
      this._radioGroupName = value;
      if (!this.Checked)
        return;
      this.UncheckOtherItemsInRadioGroup();
    }
  }

  private void UncheckOtherItemsInRadioGroup()
  {
    if (string.IsNullOrEmpty(this._radioGroupName) || !this._checked)
      return;
    Control userControlOrForm = this.FindParentUserControlOrForm();
    if (userControlOrForm == null)
      return;
    userControlOrForm.GetChildsRecursive(true).OfType<ContextMenuItemSurrogate>().Where<ContextMenuItemSurrogate>((Func<ContextMenuItemSurrogate, bool>) (menuItem => menuItem != this && menuItem.Checked && menuItem.RadioGroupName.Equals(this._radioGroupName))).InvokeForAll<ContextMenuItemSurrogate>((Action<ContextMenuItemSurrogate>) (menuItem => menuItem.Checked = false));
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool Checked
  {
    [DebuggerStepThrough] get => this._checked;
    set
    {
      if (this._checked == value)
        return;
      this._checked = value;
      if (this._checked)
        this.UncheckOtherItemsInRadioGroup();
      this.FireOnChecked();
      this.Invalidate();
    }
  }

  private Control FindParentUserControlOrForm()
  {
    return this.GetParentsEnumeration().FirstOrDefault<Control>((Func<Control, bool>) (parent => parent is Form || parent is UserControl));
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(null)]
  private ImageList ImageList
  {
    [DebuggerStepThrough] get => this._imageList;
    set
    {
      if (this._imageList == value)
        return;
      this._imageList = value;
      this.Invalidate();
    }
  }

  public int ImageIndex
  {
    [DebuggerStepThrough] get => this._imageIndex;
    set
    {
      if (this._imageIndex == value)
        return;
      this._imageIndex = value;
      if (this._imageList == null || this._image != null)
        return;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (Color), "Empty")]
  public Color BorderColor
  {
    [DebuggerStepThrough] get => this._borderColor;
    set
    {
      if (!(this._borderColor != value))
        return;
      this._borderColor = value;
      this.DisposeObj<Pen>(ref this._borderPen);
      if (this._borders == AnchorStyles.None)
        return;
      this.Invalidate();
    }
  }

  protected Pen BorderPen
  {
    get
    {
      return !(this._borderColor != Color.Empty) ? (Pen) null : LazyInitializer.EnsureInitialized<Pen>(ref this._borderPen, (Func<Pen>) (() => new Pen(this._borderColor)));
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right)]
  public AnchorStyles Borders
  {
    [DebuggerStepThrough] get => this._borders;
    set
    {
      if (value == this._borders)
        return;
      this._borders = value;
      if (!(this._borderColor != Color.Empty))
        return;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control UpControl
  {
    [DebuggerStepThrough] get => this._upControl;
    set
    {
      if (this._upControl == value)
        return;
      Control upControl = this._upControl;
      if (value != null)
        value.Disposed += new EventHandler(this.UpControl_Disposed);
      if (this._upControl != null)
        this._upControl.Disposed -= new EventHandler(this.UpControl_Disposed);
      this._upControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.DownControl == null)
          navigationSupported.DownControl = (Control) this;
      }
      if (upControl == null || upControl.IsDisposed || !(upControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) upControl;
      if (navigationSupported1.DownControl != this)
        return;
      navigationSupported1.DownControl = (Control) null;
    }
  }

  private void UpControl_Disposed(object sender, EventArgs e)
  {
    this._upControl.Disposed -= new EventHandler(this.UpControl_Disposed);
    this._upControl = (Control) null;
  }

  private bool NavigateFromDirrection(Control control, IEnumerable<Control> borderControls)
  {
    bool flag = false;
    if (control is ILastFocusedControlTracker)
    {
      ILastFocusedControlTracker focusedControlTracker = (ILastFocusedControlTracker) control;
      if (focusedControlTracker.TrackLastFocusedChildControl && focusedControlTracker.LastFocusedChildControl != null)
      {
        Control lastControl = focusedControlTracker.LastFocusedChildControl;
        if (lastControl.CanFocus && borderControls.Contains<Control>((Predicate<Control>) (ctrl => ctrl == lastControl)))
        {
          lastControl.Focus();
          flag = true;
        }
      }
    }
    if (!flag)
    {
      Control control1 = borderControls.FirstOrDefault<Control>((Func<Control, bool>) (ctrl => ctrl.CanFocus));
      if (control1 != null)
      {
        control1.Focus();
        flag = true;
      }
    }
    return flag;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToUp;

  public virtual void NavigateToUp()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToUp != null)
      this.OnNavigateToUp((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._upControl == null || blockDefaultNavigation || !this._upControl.CanFocus)
      return;
    bool flag = false;
    if (this._upControl is IFocusFromDirection)
    {
      IFocusFromDirection upControl = (IFocusFromDirection) this._upControl;
      if (!upControl.BottomMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._upControl, upControl.BottomMostControls);
    }
    if (flag)
      return;
    this._upControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control DownControl
  {
    [DebuggerStepThrough] get => this._downControl;
    set
    {
      if (this._downControl == value)
        return;
      Control downControl = this._downControl;
      if (value != null)
        value.Disposed += new EventHandler(this.DownControl_Disposed);
      if (this._downControl != null)
        this._downControl.Disposed -= new EventHandler(this.DownControl_Disposed);
      this._downControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.UpControl == null)
          navigationSupported.UpControl = (Control) this;
      }
      if (downControl == null || downControl.IsDisposed || !(downControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) downControl;
      if (navigationSupported1.UpControl != this)
        return;
      navigationSupported1.UpControl = (Control) null;
    }
  }

  private void DownControl_Disposed(object sender, EventArgs e)
  {
    this._downControl.Disposed -= new EventHandler(this.DownControl_Disposed);
    this._downControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToDown;

  public virtual void NavigateToDown()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToDown != null)
      this.OnNavigateToDown((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._downControl == null || blockDefaultNavigation || !this._downControl.CanFocus)
      return;
    bool flag = false;
    if (this._downControl is IFocusFromDirection)
    {
      IFocusFromDirection downControl = (IFocusFromDirection) this._downControl;
      if (!downControl.TopMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._downControl, downControl.TopMostControls);
    }
    if (flag)
      return;
    this._downControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control LeftControl
  {
    [DebuggerStepThrough] get => this._leftControl;
    set
    {
      if (this._leftControl == value)
        return;
      Control leftControl = this._leftControl;
      if (value != null)
        value.Disposed += new EventHandler(this.LeftControl_Disposed);
      if (this._leftControl != null)
        this._leftControl.Disposed -= new EventHandler(this.LeftControl_Disposed);
      this._leftControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.RightControl == null)
          navigationSupported.RightControl = (Control) this;
      }
      if (leftControl == null || leftControl.IsDisposed || !(leftControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) leftControl;
      if (navigationSupported1.RightControl != this)
        return;
      navigationSupported1.RightControl = (Control) null;
    }
  }

  private void LeftControl_Disposed(object sender, EventArgs e)
  {
    this._leftControl.Disposed -= new EventHandler(this.LeftControl_Disposed);
    this._leftControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToLeft;

  public virtual void NavigateToLeft()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToLeft != null)
      this.OnNavigateToLeft((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._leftControl == null || blockDefaultNavigation || !this._leftControl.CanFocus)
      return;
    bool flag = false;
    if (this._leftControl is IFocusFromDirection)
    {
      IFocusFromDirection leftControl = (IFocusFromDirection) this._leftControl;
      if (!leftControl.RightMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._leftControl, leftControl.RightMostControls);
    }
    if (flag)
      return;
    this._leftControl.Focus();
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Navigation")]
  [DefaultValue(null)]
  public Control RightControl
  {
    [DebuggerStepThrough] get => this._rightControl;
    set
    {
      if (this._rightControl == value)
        return;
      Control rightControl = this._rightControl;
      if (value != null)
        value.Disposed += new EventHandler(this.RightControl_Disposed);
      if (this._rightControl != null)
        this._rightControl.Disposed -= new EventHandler(this.RightControl_Disposed);
      this._rightControl = value;
      if (value != null && value is IArrowKeysNavigationSupported)
      {
        IArrowKeysNavigationSupported navigationSupported = (IArrowKeysNavigationSupported) value;
        if (navigationSupported.LeftControl == null)
          navigationSupported.LeftControl = (Control) this;
      }
      if (rightControl == null || rightControl.IsDisposed || !(rightControl is IArrowKeysNavigationSupported))
        return;
      IArrowKeysNavigationSupported navigationSupported1 = (IArrowKeysNavigationSupported) rightControl;
      if (navigationSupported1.LeftControl != this)
        return;
      navigationSupported1.LeftControl = (Control) null;
    }
  }

  private void RightControl_Disposed(object sender, EventArgs e)
  {
    this._rightControl.Disposed -= new EventHandler(this.RightControl_Disposed);
    this._rightControl = (Control) null;
  }

  [Category("Navigation")]
  public event OnNavigateDelegate OnNavigateToRight;

  public virtual void NavigateToRight()
  {
    bool blockDefaultNavigation = false;
    if (this.OnNavigateToRight != null)
      this.OnNavigateToRight((IArrowKeysNavigationSupported) this, ref blockDefaultNavigation);
    if (this._rightControl == null || blockDefaultNavigation || !this._rightControl.CanFocus)
      return;
    bool flag = false;
    if (this._rightControl is IFocusFromDirection)
    {
      IFocusFromDirection rightControl = (IFocusFromDirection) this._rightControl;
      if (!rightControl.LeftMostControls.IsEmpty<Control>())
        flag = this.NavigateFromDirrection(this._leftControl, rightControl.LeftMostControls);
    }
    if (flag)
      return;
    this._rightControl.Focus();
  }

  public delegate void PaintContentDelegate(
    PaintEventArgs e,
    Brush TextBrush,
    Color bgColor,
    ref string text,
    ref Rectangle textRectangle,
    ref bool drawDefaultText);

  [ComVisible(true)]
  [Serializable]
  public delegate void MenuItemEventHandler(ContextMenuItemSurrogate sender);
}
