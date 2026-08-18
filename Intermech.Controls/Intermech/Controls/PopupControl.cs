
// Type: Intermech.Controls.PopupControl
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Controls;

public class PopupControl
{
  [NotNull]
  public static readonly List<PopupControl> VisiblePopupControls = new List<PopupControl>();
  [CanBeNull]
  private ToolStripLabel _ownerItem;
  [CanBeNull]
  private ToolStripControlHost _host;
  [CanBeNull]
  private PopupDropDown _dropDown;

  /// <summary>Последний из открытых в данный момент пунктов меню</summary>
  [CanBeNull]
  public static PopupControl TopVisiblePopupControl
  {
    get
    {
      return PopupControl.VisiblePopupControls.GetIfOrDefault<List<PopupControl>, PopupControl>((Func<List<PopupControl>, bool>) (list => list.Count > 0), (Func<List<PopupControl>, PopupControl>) (list => list[list.Count - 1]));
    }
  }

  /// <summary>Последний из открытых в данный момент пунктов меню</summary>
  [CanBeNull]
  public static Control TopVisibleControl
  {
    get
    {
      return PopupControl.VisiblePopupControls.GetIfOrDefault<List<PopupControl>, Control>((Func<List<PopupControl>, bool>) (list => list.Count > 0), (Func<List<PopupControl>, Control>) (list => list[list.Count - 1].Control));
    }
  }

  public bool IsTopVisible => this == PopupControl.TopVisiblePopupControl;

  public PopupControl() => this.InitializeDropDown();

  private void m_dropDown_Closed([NotNull] object sender, [NotNull] ToolStripDropDownClosedEventArgs e)
  {
    if (this.AutoResetWhenClosed)
      this.DisposeHost();
    if (this.PopupControlHost == null)
      return;
    this.PopupControlHost.HideDropDown();
  }

  public event ToolStripDropDownClosingEventHandler Closing
  {
    add
    {
      if (this._dropDown == null)
        return;
      this._dropDown.Closing += value;
    }
    remove
    {
      if (this._dropDown == null)
        return;
      this._dropDown.Closing -= value;
    }
  }

  public void Show([NotNull] Control ownerControl, [NotNull] Control control, int x, int y)
  {
    this.Show(ownerControl, control, x, y, PopupResizeMode.None);
  }

  public void Show(
    [NotNull] Control ownerControl,
    [NotNull] Control control,
    int x,
    int y,
    PopupResizeMode resizeMode)
  {
    this.Show(ownerControl, control, x, y, -1, -1, resizeMode);
  }

  public void Show(
    [NotNull] Control ownerControl,
    [NotNull] Control control,
    int x,
    int y,
    int width,
    int height,
    PopupResizeMode resizeMode)
  {
    this.InitializeHost(control);
    ownerControl.GetParentsEnumeration(true).FirstOrDefault<Control>((Func<Control, bool>) (parent => parent is PopupDropDown)).InvokeIfNotNull<Control>((Action<Control>) (popupDropDown =>
    {
      if (this._dropDown == null)
        return;
      this._dropDown.OwnerItem = popupDropDown is PopupDropDown popupDropDown2 ? popupDropDown2.OwnerItem : (ToolStripItem) null;
    }));
    if (this._dropDown?.OwnerItem == null)
    {
      this._ownerItem = new ToolStripLabel();
      if (this._dropDown != null)
        this._dropDown.OwnerItem = (ToolStripItem) this._ownerItem;
    }
    if (this._dropDown != null)
    {
      this._dropDown.ResizeMode = resizeMode;
      this._dropDown.Show(x, y, width, height);
    }
    PopupControl.VisiblePopupControls.InvokeForAll<PopupControl>((Action<PopupControl>) (popupControl => popupControl.AutoClose = new bool?(false)));
    PopupControl.VisiblePopupControls.Add(this);
  }

  public void Hide()
  {
    PopupControl.VisiblePopupControls.Remove(this);
    PopupDropDown dropDown = this._dropDown;
    if ((dropDown != null ? (dropDown.Visible ? 1 : 0) : 0) != 0)
    {
      this._dropDown.Hide();
      this.DisposeHost();
    }
    if (PopupControl.TopVisiblePopupControl == null)
      return;
    PopupControl.TopVisiblePopupControl.AutoClose = new bool?(true);
    Control control = PopupControl.TopVisiblePopupControl.Control;
    if ((control != null ? (!control.ContainsFocus ? 1 : 0) : 0) == 0)
      return;
    PopupControl.TopVisiblePopupControl.Hide();
  }

  public void Reset() => this.DisposeHost();

  protected void DisposeHost()
  {
    if (this._host != null)
    {
      if (this._dropDown != null)
        this._dropDown.Items.Clear();
      this._host = (ToolStripControlHost) null;
    }
    if (this._ownerItem != null)
    {
      this._ownerItem.Dispose();
      this._ownerItem = (ToolStripLabel) null;
    }
    this.PopupControlHost = (IPopupControlHost) null;
  }

  protected void InitializeHost([NotNull] Control control)
  {
    this.InitializeDropDown();
    if (control != this.Control)
      this.DisposeHost();
    if (this._host == null)
    {
      this._host = new ToolStripControlHost(control);
      this._host.AutoSize = false;
      this._host.Padding = this.Padding;
      this._host.Margin = this.Margin;
    }
    if (this._dropDown == null)
      return;
    this._dropDown.Items.Clear();
    this._dropDown.Padding = this._dropDown.Margin = Padding.Empty;
    this._dropDown.Items.Add((ToolStripItem) this._host);
  }

  protected void InitializeDropDown()
  {
    if (this._dropDown != null)
      return;
    this._dropDown = new PopupDropDown(false);
    this._dropDown.Closed += new ToolStripDropDownClosedEventHandler(this.m_dropDown_Closed);
  }

  public bool? AutoClose
  {
    get => this._dropDown?.AutoClose;
    set
    {
      if (this._dropDown == null || !value.HasValue)
        return;
      this._dropDown.AutoClose = value.Value;
    }
  }

  public bool Visible
  {
    get
    {
      PopupDropDown dropDown = this._dropDown;
      return dropDown != null && dropDown.Visible;
    }
  }

  [CanBeNull]
  public Control Control => this._host?.Control;

  public Padding Padding { get; set; } = Padding.Empty;

  public Padding Margin { get; set; } = new Padding(1, 1, 1, 1);

  public bool AutoResetWhenClosed { get; set; }

  /// <summary>Gets or sets the popup control host, this is used to hide/show popup.</summary>
  [CanBeNull]
  public IPopupControlHost PopupControlHost { get; set; }
}
