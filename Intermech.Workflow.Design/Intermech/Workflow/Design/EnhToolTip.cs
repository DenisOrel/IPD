// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.EnhToolTip
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class EnhToolTip : ToolTip
{
  private Dictionary<Control, string> _hints = new Dictionary<Control, string>();
  private bool _tipActive;
  private Timer _tooltipTimer;
  private Point _mousePos;
  private Control _control;
  public int MaxLineLength = 100;

  public EnhToolTip()
  {
  }

  public EnhToolTip(IContainer cont)
    : this()
  {
    cont?.Add((IComponent) this);
  }

  [DefaultValue("")]
  [Localizable(true)]
  public new string GetToolTip(Control control)
  {
    string str = "";
    return this._hints.TryGetValue(control, out str) ? str : "";
  }

  public new void SetToolTip(Control control, string caption)
  {
    if (!this._hints.ContainsKey(control))
    {
      control.MouseMove += new MouseEventHandler(this._control_MouseMove);
      control.MouseLeave += new EventHandler(this._control_MouseLeave);
    }
    this._hints[control] = caption;
  }

  private void _control_MouseLeave(object sender, EventArgs e) => this.HideTip();

  private void _control_MouseMove(object sender, MouseEventArgs e)
  {
    if (this._mousePos.Equals((object) e.Location))
      return;
    this._mousePos = e.Location;
    this._control = (Control) sender;
    this.UpdateTip();
  }

  private void HideTip()
  {
    if (this._tipActive)
    {
      this._tipActive = false;
      this.Hide((IWin32Window) this._control);
    }
    if (this._tooltipTimer == null)
      return;
    this._tooltipTimer.Stop();
  }

  private void UpdateTip()
  {
    this.HideTip();
    if (this._tooltipTimer == null)
    {
      this._tooltipTimer = new Timer();
      this._tooltipTimer.Interval = this.InitialDelay;
      this._tooltipTimer.Tick += new EventHandler(this._tooltipTimer_Tick);
    }
    this._tooltipTimer.Start();
  }

  private void _tooltipTimer_Tick(object sender, EventArgs e)
  {
    if (this._tipActive || this._control == null)
      return;
    string text = "";
    if (!this._hints.TryGetValue(this._control, out text))
      return;
    this._tipActive = true;
    this.Show(text, (IWin32Window) this._control, this._mousePos.X, this._mousePos.Y + this._control.Cursor.Size.Height / 2);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this._tooltipTimer != null)
    {
      this._tooltipTimer.Stop();
      this._tooltipTimer.Dispose();
    }
    base.Dispose(disposing);
  }

  private string AddCRLF(string text)
  {
    if (text.Length < this.MaxLineLength)
      return text;
    StringBuilder stringBuilder = new StringBuilder();
    int num = 0;
    for (int index = 0; index < text.Length; ++index)
    {
      if (num >= this.MaxLineLength && char.IsWhiteSpace(text[index]))
      {
        stringBuilder.Append(Environment.NewLine);
        num = 0;
      }
      if (num == 0)
      {
        while (index < text.Length && char.IsWhiteSpace(text[index]))
          ++index;
      }
      if (index < text.Length)
        stringBuilder.Append(text[index]);
      ++num;
    }
    return stringBuilder.ToString();
  }
}
