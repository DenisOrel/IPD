
// Type: Intermech.Controls.ElapsedTimePanel
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;


namespace Intermech.Controls;

public class ElapsedTimePanel : Control
{
  private System.Timers.Timer _timer;
  private TimeSpan _elapsed;
  private StringFormat _stringFormat;

  public event EventHandler Changed;

  public ElapsedTimePanel()
  {
    this.Size = new Size(150, 15);
    this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    this._timer = new System.Timers.Timer();
    this._timer.Enabled = false;
    this._timer.Interval = 1000.0;
    this._timer.Elapsed += new ElapsedEventHandler(this.Timer_Elapsed);
    this.HandleCreated += new EventHandler(this.ElapsedTimePanel_HandleCreated);
    this._stringFormat = new StringFormat();
    this._stringFormat.Alignment = StringAlignment.Center;
    this._stringFormat.LineAlignment = StringAlignment.Center;
  }

  public void Reset()
  {
    this._elapsed = new TimeSpan();
    this.Invalidate();
  }

  public void Start() => this._timer.Enabled = true;

  public void Stop() => this._timer.Enabled = false;

  protected override void OnEnabledChanged(EventArgs e)
  {
    if (this.InvokeRequired)
      this.Invoke((Delegate) new EventHandler(this.EnableChangedInvoke));
    else
      base.OnEnabledChanged(e);
    this._timer.Enabled = this.Enabled;
  }

  private void EnableChangedInvoke(object sender, EventArgs e) => base.OnEnabledChanged(e);

  private void ElapsedTimePanel_HandleCreated(object sender, EventArgs e)
  {
    this._timer.Enabled = this.Enabled;
  }

  [Browsable(false)]
  public TimeSpan Elapsed => this._elapsed;

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (!disposing || this._stringFormat == null)
      return;
    this._stringFormat.Dispose();
    this._stringFormat = (StringFormat) null;
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    TimeSpan elapsed = this._elapsed;
    string s = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}";
    using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
      e.Graphics.DrawString(s, this.Font, (Brush) solidBrush, (RectangleF) this.ClientRectangle, this._stringFormat);
  }

  private void Timer_Elapsed(object sender, ElapsedEventArgs e)
  {
    this._elapsed += TimeSpan.FromSeconds(1.0);
    if (this.Enabled)
      this.Invalidate();
    this.OnChanged();
  }

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, EventArgs.Empty);
  }
}
