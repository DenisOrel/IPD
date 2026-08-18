
// Type: SuperTooltips.SuperTooltip
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace SuperTooltips
{
    [ComVisible(false)]
    [ProvideProperty("SuperTooltip", typeof (IComponent))]
    [ToolboxBitmap(typeof (SuperTooltip), "SuperTooltip.bmp")]
    [ToolboxItem(true)]
    [Designer(typeof (SuperTooltipDesigner))]
    public class SuperTooltip : Component, IExtenderProvider
    {
      private bool _positionBelowControl;
      private Hashtable _tooltips;
      private SuperTooltipControl _tooltipControl;
      private Timer _timer;
      private bool _checkOnScreenPosition;
      private IntPtr _activeWindow;
      private int _tooltipDuration;
      private long _showTime;
      private SuperTooltipInfo _defaultTooltipSettings;
      private static SuperTooltipInfo _defaultSuperTooltipInfo;
      private Font font;
      private bool _lockTimer;

      public event SuperTooltipEventHandler BeforeTooltipDisplay;

      public SuperTooltip()
      {
        this._positionBelowControl = true;
        this._tooltips = new Hashtable();
        this._checkOnScreenPosition = true;
        this._activeWindow = IntPtr.Zero;
        this._tooltipDuration = 20;
      }

      public bool CanExtend(object extendee) => extendee is Control;

      protected override void Dispose(bool disposing)
      {
        this.HideTooltip();
        this.StopTimer();
        base.Dispose(disposing);
      }

      [DefaultValue(null)]
      [Editor(typeof (SuperTooltipInfoEditor), typeof (UITypeEditor))]
      public SuperTooltipInfo GetSuperTooltip(IComponent component)
      {
        return this._tooltips.Contains((object) component) && this._tooltips[(object) component] is SuperTooltipInfo tooltip ? tooltip : (SuperTooltipInfo) null;
      }

      public void HideTooltip()
      {
        this.StopTimer();
        if (this._tooltipControl == null)
          return;
        this._tooltipControl.Hide();
        this._tooltipControl.Dispose();
        this._tooltipControl = (SuperTooltipControl) null;
      }

      private void DetachComponent(IComponent component)
      {
        this._tooltips.Remove((object) component);
        if (this.DesignMode)
          return;
        switch (component)
        {
          case Control _:
            Control control = component as Control;
            control.MouseHover -= new EventHandler(this.Control_MouseHover);
            control.MouseLeave -= new EventHandler(this.Control_MouseLeave);
            break;
          case ISuperTooltipInfoProvider _:
            (component as ISuperTooltipInfoProvider).DisplayTooltip -= new EventHandler(this.Control_MouseHover);
            break;
        }
      }

      private void AttachComponent(IComponent component, SuperTooltipInfo tooltipInfo)
      {
        this._tooltips[(object) component] = (object) tooltipInfo;
        switch (component)
        {
          case Control _:
            Control control = component as Control;
            control.MouseHover += new EventHandler(this.Control_MouseHover);
            control.MouseLeave += new EventHandler(this.Control_MouseLeave);
            control.MouseDown += new MouseEventHandler(this.Control_MouseDown);
            break;
          case ISuperTooltipInfoProvider _:
            ISuperTooltipInfoProvider tooltipInfoProvider = component as ISuperTooltipInfoProvider;
            tooltipInfoProvider.DisplayTooltip += new EventHandler(this.Control_MouseHover);
            tooltipInfoProvider.HideTooltip += new EventHandler(this.OnHideTooltip);
            break;
        }
      }

      private void OnHideTooltip(object sender, EventArgs e) => this.HideTooltip();

      private void Control_MouseDown(object sender, MouseEventArgs mea) => this.HideTooltip();

      private void JZ(object sender, EventArgs e)
      {
        if (!(sender is Control))
          return;
        this.StartMouseTracking(sender as Control);
      }

      private void Control_MouseLeave(object sender, EventArgs e) => this.HideTooltip();

      private void Control_MouseHover(object sender, EventArgs e) => this.ActivateTooltip(sender);

      private void ActivateTooltip(object sender)
      {
        Rectangle rect = Rectangle.Empty;
        if (sender is Control)
        {
          Control control = sender as Control;
          rect = new Rectangle(control.PointToScreen(Point.Empty), control.Size);
        }
        else if (sender is ISuperTooltipInfoProvider)
          rect = (sender as ISuperTooltipInfoProvider).ComponentRectangle;
        if (rect.IsEmpty || !(this._tooltips[sender] is SuperTooltipInfo tooltip))
          return;
        this.HideTooltip();
        Point point = new Point(Control.MousePosition.X + SystemInformation.CursorSize.Width / 2, Control.MousePosition.Y + SystemInformation.CursorSize.Height / 2);
        if (this._positionBelowControl)
          point.Y = rect.Bottom + 1;
        this._tooltipControl = new SuperTooltipControl();
        if (this.font != null)
          this._tooltipControl.Font = this.font;
        if (this._checkOnScreenPosition)
        {
          ScreenInfo screenInfo = ControlHelper.GetScreenInfo(point);
          if (screenInfo != null)
          {
            this._tooltipControl.UpdateWithSuperTooltipInfo(tooltip);
            this._tooltipControl.RecalcSize();
            Rectangle rectangle = new Rectangle(point, this._tooltipControl.Size);
            Size size = screenInfo._workingarea.Size;
            if (rectangle.Right > screenInfo._workingarea.Right)
            {
              rectangle.X -= rectangle.Right - screenInfo._workingarea.Right;
              if (rectangle.IntersectsWith(rect))
                rectangle.X = rect.X - rectangle.Width;
            }
            if (rectangle.Bottom > screenInfo._bounds.Bottom)
            {
              rectangle.Y = screenInfo._bounds.Bottom - rectangle.Height;
              if (rectangle.IntersectsWith(rect))
                rectangle.Y = rect.Y - rectangle.Height;
            }
            point = rectangle.Location;
          }
        }
        if (this.BeforeTooltipDisplay != null)
        {
          SuperTooltipEventArgs e = new SuperTooltipEventArgs(sender, tooltip, point);
          this.BeforeTooltipDisplay((object) this, e);
          if (e.Cancel)
          {
            this._tooltipControl.Dispose();
            this._tooltipControl = (SuperTooltipControl) null;
            return;
          }
          point = e.Location;
        }
        this._tooltipControl.ShowTooltip(tooltip, point.X, point.Y, false);
        this._showTime = 0L;
        this.StartTimer();
      }

      private void StartTimer()
      {
        if (this._timer != null || this._lockTimer)
          return;
        this._lockTimer = true;
        try
        {
          this._activeWindow = Win32API.GetActiveWindow();
          this._timer = new Timer();
          this._timer.Interval = 300;
          this._timer.Tick += new EventHandler(this.TimerTick);
          this._timer.Start();
        }
        finally
        {
          this._lockTimer = false;
        }
      }

      private void TimerTick(object sender, EventArgs e)
      {
        this._showTime += (long) this._timer.Interval;
        if (this._activeWindow != IntPtr.Zero && Win32API.GetActiveWindow() != this._activeWindow)
        {
          this._timer.Stop();
          this.HideTooltip();
        }
        else
        {
          if (this._tooltipDuration <= 0 || this._showTime <= (long) (this._tooltipDuration * 1000))
            return;
          this._timer.Stop();
          this.HideTooltip();
        }
      }

      private void StopTimer()
      {
        if (this._lockTimer)
          return;
        this._lockTimer = true;
        try
        {
          if (this._timer == null)
            return;
          this._timer.Enabled = false;
          this._timer.Stop();
          this._timer.Tick -= new EventHandler(this.TimerTick);
          this._timer.Dispose();
          this._timer = (Timer) null;
        }
        finally
        {
          this._lockTimer = false;
        }
      }

      private void StartMouseTracking(Control ctrl)
      {
        if (!ControlHelper.IsControlValid(ctrl))
          return;
        Win32API.TRACKMOUSEEVENT val = new Win32API.TRACKMOUSEEVENT()
        {
          wsFlags = 1073741824 /*0x40000000*/,
          dwHoverTime = (int) ctrl.Handle
        };
        val.cbSize = Marshal.SizeOf((object) val);
        Win32API.TrackMouseEvent(ref val);
        val.wsFlags |= 1U;
        Win32API.TrackMouseEvent(ref val);
      }

      public void SetSuperTooltip(IComponent c, SuperTooltipInfo info)
      {
        if (this._tooltips.Contains((object) c))
        {
          if (info == null)
            this.DetachComponent(c);
          else
            this._tooltips[(object) c] = (object) info;
        }
        else if (info != null)
          this.AttachComponent(c, info);
        int num = this.DesignMode ? 1 : 0;
      }

      [Browsable(true)]
      [Description("Indicates whether tooltip position is checked before tooltip is displayed and adjusted to tooltip always falls into screen bounds.")]
      [DefaultValue(true)]
      [Category("Behavior")]
      public bool CheckOnScreenPosition
      {
        get => this._checkOnScreenPosition;
        set => this._checkOnScreenPosition = value;
      }

      [Category("Appearance")]
      [Description("Indicates default tooltip font.")]
      [DefaultValue(null)]
      [Browsable(true)]
      public Font DefaultFont
      {
        get => this.font;
        set => this.font = value;
      }

      [Browsable(false)]
      public static SuperTooltipInfo DefaultSuperTooltipInfo => SuperTooltip._defaultSuperTooltipInfo;

      [Editor(typeof (SuperTooltipInfoEditor), typeof (UITypeEditor))]
      [DefaultValue(null)]
      [Description("Indicates default setting for new toolips you create in design time.")]
      public SuperTooltipInfo DefaultTooltipSettings
      {
        get => this._defaultTooltipSettings;
        set
        {
          this._defaultTooltipSettings = value;
          SuperTooltip._defaultSuperTooltipInfo = value;
        }
      }

      [Browsable(false)]
      public bool IsTooltipVisible => this._tooltipControl != null && this._tooltipControl.Visible;

      [DefaultValue(true)]
      [Browsable(true)]
      [Description("")]
      [Category("Behavior")]
      public bool PositionBelowControl
      {
        get => this._positionBelowControl;
        set => this._positionBelowControl = value;
      }

      [Browsable(false)]
      public SuperTooltipControl SuperTooltipControl => this._tooltipControl;

      [Browsable(true)]
      [Description("Indicates duration in seconds that tooltip is kept on screen after it is displayed.")]
      [DefaultValue(20)]
      [Category("Behavior")]
      public int TooltipDuration
      {
        get => this._tooltipDuration;
        set => this._tooltipDuration = value;
      }
    }
}
