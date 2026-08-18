// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoHint
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces;

public class AutoHint
{
  private static ToolTip _tooltip = (ToolTip) null;
  private static Control _activeControl = (Control) null;
  private static Point _mousePos = new Point();
  private static string _hint = "";
  private static Timer _hintTimer = (Timer) null;
  private static bool _shown = false;

  public static void Attach(Control c)
  {
    c.MouseEnter += new EventHandler(AutoHint.OnMouseEnter);
    c.MouseLeave += new EventHandler(AutoHint.OnMouseLeave);
    c.MouseMove += new MouseEventHandler(AutoHint.OnMouseMove);
    c.Disposed += new EventHandler(AutoHint.ControlDisposed);
  }

  private static void ControlDisposed(object sender, EventArgs e)
  {
    AutoHint.Detach(sender as Control);
  }

  private static void OnMouseEnter(object sender, EventArgs e)
  {
    AutoHint._activeControl = sender as Control;
  }

  private static void OnMouseLeave(object sender, EventArgs e)
  {
    AutoHint.CancelHint();
    AutoHint._activeControl = (Control) null;
  }

  private static void OnMouseMove(object sender, MouseEventArgs e)
  {
    AutoHint._mousePos = e.Location;
  }

  public static void Detach(Control c)
  {
    c.MouseEnter -= new EventHandler(AutoHint.OnMouseEnter);
    c.MouseLeave -= new EventHandler(AutoHint.OnMouseLeave);
  }

  public static string Hint
  {
    get => AutoHint._hint;
    set
    {
      if (!(AutoHint._hint != value))
        return;
      AutoHint._hint = value;
      if (AutoHint.Hint != "")
      {
        if (AutoHint._hintTimer == null)
        {
          AutoHint._hintTimer = new Timer();
          AutoHint._hintTimer.Interval = 1000;
          AutoHint._hintTimer.Tick += new EventHandler(AutoHint.HintTimer_Tick);
        }
        AutoHint._hintTimer.Stop();
        AutoHint._hintTimer.Start();
      }
      else
        AutoHint.CancelHint();
    }
  }

  private static void CancelHint()
  {
    if (AutoHint._shown)
      return;
    if (AutoHint._tooltip != null && AutoHint._activeControl != null)
      AutoHint._tooltip.Hide((IWin32Window) AutoHint._activeControl);
    if (AutoHint._hintTimer == null)
      return;
    AutoHint._hintTimer.Stop();
  }

  private static void HintTimer_Tick(object sender, EventArgs e)
  {
    AutoHint._hintTimer.Stop();
    if (AutoHint._tooltip == null)
    {
      AutoHint._tooltip = new ToolTip();
      AutoHint._tooltip.AutoPopDelay = 1000;
    }
    if (AutoHint._activeControl == null)
      return;
    AutoHint._shown = true;
    AutoHint._tooltip.Show(AutoHint.Hint, (IWin32Window) AutoHint._activeControl, AutoHint._mousePos.X, AutoHint._mousePos.Y + AutoHint._activeControl.Cursor.Size.Height / 2);
  }
}
