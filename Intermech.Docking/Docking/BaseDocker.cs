
// Type: Intermech.Docking.BaseDocker
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Util;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking;

internal abstract class BaseDocker : IDisposable, IMessageFilter
{
  private Form _form;
  private Control _control;
  private DockingHints _dockingHints;
  private Rectangle _bounds;
  private bool _drawTab;
  private bool _tabbed;
  private TranslucentFillForm _transfluentForm;
  private int _offset;

  public BaseDocker(Control control, DockingHints hints, bool tabbed)
  {
    this._form = (Form) null;
    this._control = (Control) null;
    this._dockingHints = DockingHints.TranslucentFill;
    this._bounds = Rectangle.Empty;
    this._drawTab = false;
    this._transfluentForm = (TranslucentFillForm) null;
    this._offset = 21;
    this._tabbed = tabbed;
    if (hints == DockingHints.TranslucentFill && !Win32.IsWin2K())
      hints = DockingHints.RubberBand;
    this._dockingHints = hints;
    this._form = control.FindForm();
    this._control = control;
    if (this._form != null)
      this._form.Deactivate += new EventHandler(this.Form_Deactivate);
    Application.AddMessageFilter((IMessageFilter) this);
    if (hints != DockingHints.TranslucentFill)
      return;
    this._transfluentForm = new TranslucentFillForm(tabbed);
  }

  public BaseDocker(Control control, DockingHints hints, bool tabbed, int offset)
    : this(control, hints, tabbed)
  {
    this._offset = offset;
  }

  public virtual void OnCommit() => this.Dispose();

  public abstract void Update(Point pos);

  public bool PreFilterMessage(ref Message msg)
  {
    if (msg.Msg == 15)
      this.Paint();
    if ((msg.Msg == 256 /*0x0100*/ || msg.Msg == 257) && msg.WParam.ToInt32() == 17)
    {
      this.Update(Cursor.Position);
      return false;
    }
    if (msg.Msg < 256 /*0x0100*/ || msg.Msg > 264)
      return false;
    this.OnCancel();
    return true;
  }

  protected void Redraw(Rectangle bounds, bool drawTab)
  {
    if (this._bounds == bounds)
      return;
    if (this._dockingHints == DockingHints.RubberBand)
      this.HideRubberBand();
    if (this._dockingHints == DockingHints.RubberBand)
    {
      if (this._tabbed)
        DockingDrawer.DrawReversibleHollowRectangle((Control) null, bounds, drawTab, this._offset);
      else
        DockingDrawer.DrawReversibleHatchedRectangle((Control) null, bounds);
      this._bounds = bounds;
      this._drawTab = drawTab;
    }
    else
    {
      if (this._transfluentForm == null)
        return;
      this._transfluentForm.ShowNoActivate(bounds, drawTab);
    }
  }

  private void Form_Deactivate(object sender, EventArgs e) => this.OnCancel();

  public virtual void Dispose()
  {
    this.Hide();
    if (this._dockingHints == DockingHints.TranslucentFill && this._transfluentForm != null)
    {
      this._transfluentForm.Dispose();
      this._transfluentForm = (TranslucentFillForm) null;
    }
    if (this._form != null)
      this._form.Deactivate -= new EventHandler(this.Form_Deactivate);
    Application.RemoveMessageFilter((IMessageFilter) this);
    this._form = (Form) null;
    this._control = (Control) null;
  }

  public virtual void OnCancel() => this.Dispose();

  private void Paint()
  {
    if (this._dockingHints != DockingHints.RubberBand)
      return;
    this.HideRubberBand();
  }

  private void HideRubberBand()
  {
    if (this._bounds != Rectangle.Empty)
    {
      if (this._tabbed)
        DockingDrawer.DrawReversibleHollowRectangle((Control) null, this._bounds, this._drawTab, this._offset);
      else
        DockingDrawer.DrawReversibleHatchedRectangle((Control) null, this._bounds);
    }
    this._bounds = Rectangle.Empty;
  }

  protected void Hide()
  {
    if (this._dockingHints == DockingHints.RubberBand)
    {
      this.HideRubberBand();
    }
    else
    {
      if (this._transfluentForm == null)
        return;
      this._transfluentForm.Hide();
    }
  }

  public static bool IsDockLocationValid(DockLocation dockLocation, DockLocation allowedLocations)
  {
    return ((allowedLocations & DockLocation.Float) != DockLocation.Unknown || dockLocation != DockLocation.Float) && ((allowedLocations & DockLocation.Document) != DockLocation.Unknown || dockLocation != DockLocation.Document) && ((allowedLocations & DockLocation.Center) != DockLocation.Unknown || dockLocation != DockLocation.Center) && ((allowedLocations & DockLocation.Left) != DockLocation.Unknown || dockLocation != DockLocation.Left) && ((allowedLocations & DockLocation.Right) != DockLocation.Unknown || dockLocation != DockLocation.Right) && ((allowedLocations & DockLocation.Top) != DockLocation.Unknown || dockLocation != DockLocation.Top) && ((allowedLocations & DockLocation.Bottom) != DockLocation.Unknown || dockLocation != DockLocation.Bottom);
  }
}
