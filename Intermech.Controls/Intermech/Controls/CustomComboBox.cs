
// Type: Intermech.Controls.CustomComboBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>
/// <c>CustomComboBox</c> is an extension of <c>ComboBox</c> which provides drop-down customization.
/// </summary>
[Designer(typeof (CustomComboBoxDesigner))]
[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class CustomComboBox : 
  ComboBoxAdv,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IPopupControlHost
{
  private Control _assignedControl;
  public const uint WM_COMMAND = 273;
  public const uint WM_USER = 1024 /*0x0400*/;
  public const uint WM_REFLECT = 8192 /*0x2000*/;
  public const uint WM_LBUTTONDOWN = 513;
  public const uint CBN_DROPDOWN = 7;
  public const uint CBN_CLOSEUP = 8;
  private static DateTime m_sShowTime = DateTime.Now;
  /// <summary>Popup control.</summary>
  private PopupControl m_popupCtrl = new PopupControl();
  /// <summary>Actual drop-down control itself.</summary>
  private Lazy<Control> m_dropDownCtrl;
  /// <summary>Indicates if drop-down is currently shown.</summary>
  private bool m_bDroppedDown;
  /// <summary>Indicates current sizing mode.</summary>
  private CustomComboBox.SizeMode m_sizeMode;
  /// <summary>Time drop-down was last hidden.</summary>
  private DateTime m_lastHideTime = DateTime.Now;
  /// <summary>
  /// Automatic focus timer helps make sure drop-down control is focused for user
  /// input upon drop-down.
  /// </summary>
  private Timer m_timerAutoFocus;
  /// <summary>
  /// Original size of control dimensions when first assigned.
  /// </summary>
  private Size m_sizeOriginal = new Size(1, 1);
  /// <summary>
  /// Original size of combo box dropdown when first assigned.
  /// </summary>
  private Size m_sizeCombo;
  /// <summary>Indicates if drop-down is resizable.</summary>
  private bool m_bIsResizable = true;

  public CustomComboBox()
  {
    this.m_sizeCombo = new Size(base.DropDownWidth, base.DropDownHeight);
    this.m_popupCtrl.Closing += new ToolStripDropDownClosingEventHandler(this.m_dropDown_Closing);
    this.DropDownStyle = ComboBoxStyle.DropDownList;
    this.m_dropDownCtrl = new Lazy<Control>(new Func<Control>(this.CreateDropDownControlInternal));
  }

  private Control CreateDropDownControlInternal()
  {
    Control dropDownControl = this.CreateDropDownControl();
    if (dropDownControl != null && dropDownControl is IArrowKeysNavigationSupported)
      ((IArrowKeysNavigationSupported) dropDownControl).OnNavigateToUp += new OnNavigateDelegate(this.CustomComboBox_OnNavigateToUp);
    return dropDownControl;
  }

  private void CustomComboBox_OnNavigateToUp(
    IArrowKeysNavigationSupported sender,
    ref bool blockDefaultNavigation)
  {
    this.HideDropDown();
  }

  protected virtual Control CreateDropDownControl() => this._assignedControl;

  private void m_dropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
  {
    this.m_lastHideTime = DateTime.Now;
  }

  public CustomComboBox(Control dropControl)
    : this()
  {
    this.DropDownControl = dropControl;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.m_timerAutoFocus != null)
      {
        this.m_timerAutoFocus.Dispose();
        this.m_timerAutoFocus = (Timer) null;
      }
      if (this.m_dropDownCtrl != null && this.m_dropDownCtrl.IsValueCreated)
        ((IArrowKeysNavigationSupported) this.m_dropDownCtrl.Value).OnNavigateToUp -= new OnNavigateDelegate(this.CustomComboBox_OnNavigateToUp);
    }
    base.Dispose(disposing);
  }

  public event EventHandler OnAutoFocus;

  protected virtual void FireOnAutoFocus(EventArgs e)
  {
    if (this.OnAutoFocus == null)
      return;
    this.OnAutoFocus((object) this, e);
  }

  private void timerAutoFocus_Tick(object sender, EventArgs e)
  {
    if (this.m_popupCtrl.Visible && !this.DropDownControl.Focused)
    {
      this.DropDownControl.Focus();
      this.m_timerAutoFocus.Enabled = false;
      this.FireOnAutoFocus(EventArgs.Empty);
    }
    if (!this.DroppedDown)
      return;
    this.DroppedDown = false;
  }

  private void m_dropDown_LostFocus(object sender, EventArgs e)
  {
    this.m_lastHideTime = DateTime.Now;
  }

  public new event EventHandler DropDown;

  public new event EventHandler DropDownClosed;

  public event OldNewEventHandler<object> SelectedValueChanged;

  public void RaiseDropDownEvent()
  {
    if (this.DropDown == null)
      return;
    this.DropDown((object) this, EventArgs.Empty);
  }

  public void RaiseDropDownClosedEvent()
  {
    if (this.DropDownClosed == null)
      return;
    this.DropDownClosed((object) this, EventArgs.Empty);
  }

  public void RaiseSelectedValueChangedEvent(object oldValue, object newValue)
  {
    if (this.SelectedValueChanged == null)
      return;
    this.SelectedValueChanged((object) this, new OldNewEventArgs<object>(oldValue, newValue));
  }

  /// <summary>
  /// Displays drop-down area of combo box, if not already shown.
  /// </summary>
  public virtual void ShowDropDown()
  {
    if (this.m_popupCtrl == null || this.IsDroppedDown)
      return;
    if (!this.Focused)
      this.Focus();
    this.RaiseDropDownEvent();
    this.AutoSizeDropDown();
    Point screen = this.PointToScreen(new Point(0, this.Height));
    PopupResizeMode resizeMode = this.m_bIsResizable ? PopupResizeMode.BottomRight : PopupResizeMode.None;
    if (this.DropDownSizeMode == CustomComboBox.SizeMode.UseControlSize)
      this.m_popupCtrl.Show((Control) this, this.DropDownControl, screen.X + this.Width - this.DropDownControl.Width, screen.Y, this.DropDownControl.Width, this.Height, resizeMode);
    else
      this.m_popupCtrl.Show((Control) this, this.DropDownControl, screen.X, screen.Y, this.Width, this.Height, resizeMode);
    this.m_bDroppedDown = true;
    this.m_popupCtrl.PopupControlHost = (IPopupControlHost) this;
    if (this.m_timerAutoFocus == null)
    {
      this.m_timerAutoFocus = new Timer();
      this.m_timerAutoFocus.Interval = 10;
      this.m_timerAutoFocus.Tick += new EventHandler(this.timerAutoFocus_Tick);
    }
    this.m_timerAutoFocus.Enabled = true;
    CustomComboBox.m_sShowTime = DateTime.Now;
  }

  /// <summary>Hides drop-down area of combo box, if shown.</summary>
  public virtual void HideDropDown()
  {
    if (this.m_popupCtrl == null || !this.IsDroppedDown)
      return;
    this.m_popupCtrl.Hide();
    this.m_bDroppedDown = false;
    if (this.m_timerAutoFocus != null && this.m_timerAutoFocus.Enabled)
      this.m_timerAutoFocus.Enabled = false;
    this.RaiseDropDownClosedEvent();
  }

  /// <summary>Automatically resize drop-down from properties.</summary>
  protected void AutoSizeDropDown()
  {
    if (this.m_dropDownCtrl == null || !this.m_dropDownCtrl.IsValueCreated)
      return;
    switch (this.DropDownSizeMode)
    {
      case CustomComboBox.SizeMode.UseComboSize:
        this.DropDownControl.Size = new Size(this.Width, this.DropDownControl.Height);
        break;
      case CustomComboBox.SizeMode.UseDropDownSize:
        this.DropDownControl.Size = this.m_sizeCombo;
        break;
    }
  }

  /// <summary>
  /// Assigns control to custom drop-down area of combo box.
  /// </summary>
  /// <param name="control">Control to be used as drop-down. Please note that this control must not be contained elsewhere.</param>
  protected virtual void AssignControl(Control control)
  {
    if (control == this.DropDownControl)
      return;
    this.m_sizeOriginal = control.Size;
    this._assignedControl = control;
  }

  public static uint HIWORD(int n) => (uint) (n >> 16 /*0x10*/ & (int) ushort.MaxValue);

  public override bool PreProcessMessage(ref Message m)
  {
    return (m.Msg != 8465 || CustomComboBox.HIWORD((int) m.WParam) != 7U) && base.PreProcessMessage(ref m);
  }

  private void AutoDropDown()
  {
    if (this.m_popupCtrl != null && this.m_popupCtrl.Visible)
    {
      this.HideDropDown();
    }
    else
    {
      if ((DateTime.Now - this.m_lastHideTime).Milliseconds <= 50)
        return;
      this.ShowDropDown();
    }
  }

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 513)
    {
      this.AutoDropDown();
    }
    else
    {
      if (m.Msg == 8465)
      {
        switch (CustomComboBox.HIWORD((int) m.WParam))
        {
          case 7:
            this.AutoDropDown();
            return;
          case 8:
            if ((DateTime.Now - CustomComboBox.m_sShowTime).Seconds <= 1)
              return;
            this.HideDropDown();
            return;
        }
      }
      base.WndProc(ref m);
    }
  }

  /// <summary>Actual drop-down control itself.</summary>
  [Browsable(false)]
  public Control DropDownControl
  {
    get
    {
      int num = !this.m_dropDownCtrl.IsValueCreated ? 1 : 0;
      Control control = this.m_dropDownCtrl.Value;
      if (num != 0)
        this.AutoSizeDropDown();
      return this.m_dropDownCtrl.Value;
    }
    set => this.AssignControl(value);
  }

  /// <summary>Indicates if drop-down is currently shown.</summary>
  [Browsable(false)]
  public bool IsDroppedDown => this.m_bDroppedDown;

  /// <summary>Indicates if drop-down is resizable.</summary>
  [Category("Custom Drop-Down")]
  [Description("Indicates if drop-down is resizable.")]
  public bool AllowResizeDropDown
  {
    get => this.m_bIsResizable;
    set => this.m_bIsResizable = value;
  }

  /// <summary>Indicates current sizing mode.</summary>
  [Category("Custom Drop-Down")]
  [Description("Indicates current sizing mode.")]
  [DefaultValue(CustomComboBox.SizeMode.UseComboSize)]
  public CustomComboBox.SizeMode DropDownSizeMode
  {
    get => this.m_sizeMode;
    set
    {
      if (value == this.m_sizeMode)
        return;
      this.m_sizeMode = value;
      this.AutoSizeDropDown();
    }
  }

  [Category("Custom Drop-Down")]
  public Size DropSize
  {
    get => this.m_sizeCombo;
    set
    {
      this.m_sizeCombo = value;
      if (this.DropDownSizeMode != CustomComboBox.SizeMode.UseDropDownSize)
        return;
      this.AutoSizeDropDown();
    }
  }

  [Category("Custom Drop-Down")]
  [Browsable(false)]
  public Size ControlSize
  {
    get => this.m_sizeOriginal;
    set
    {
      this.m_sizeOriginal = value;
      if (this.DropDownSizeMode != CustomComboBox.SizeMode.UseControlSize)
        return;
      this.AutoSizeDropDown();
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new ComboBox.ObjectCollection Items => base.Items;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new int ItemHeight
  {
    get => base.ItemHeight;
    set
    {
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new int MaxDropDownItems
  {
    get => base.MaxDropDownItems;
    set
    {
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new string DisplayMember
  {
    get => base.DisplayMember;
    set
    {
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new string ValueMember
  {
    get => base.ValueMember;
    set
    {
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new int DropDownWidth
  {
    get => base.DropDownWidth;
    set
    {
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new int DropDownHeight
  {
    get => base.DropDownHeight;
    set
    {
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new bool IntegralHeight
  {
    get => base.IntegralHeight;
    set
    {
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [ReadOnly(true)]
  public new bool Sorted
  {
    get => base.Sorted;
    set
    {
    }
  }

  public enum SizeMode
  {
    UseComboSize,
    UseControlSize,
    UseDropDownSize,
  }
}
