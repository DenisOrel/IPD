
// Type: Intermech.Client.Core.PropertyEditors.AttrProcessor.Editors.DropDownForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core.PropertyEditors.AttrProcessor.Editors;

[ToolboxItem(false)]
public class DropDownForm : Form
{
  private bool _displayed;
  private bool _manageContainedControlDisposal;
  private System.ComponentModel.Container components;

  public DropDownForm()
  {
    this._manageContainedControlDisposal = true;
    this.InitializeComponent();
  }

  protected override void Dispose(bool disposing)
  {
    this._displayed = false;
    if (disposing)
    {
      if (!this.ManageContainedControlDisposal)
        this.Controls.Clear();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  [DllImport("user32.dll")]
  private static extern IntPtr GetWindowDC(IntPtr hWnd);

  private void InitializeComponent()
  {
    this.AutoScaleBaseSize = new Size(5, 13);
    this.ClientSize = new Size(120, 16 /*0x10*/);
    this.MinimumSize = new Size(1, 1);
    this.Name = nameof (DropDownForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.Manual;
    this.Text = nameof (DropDownForm);
    this.TopMost = true;
  }

  protected override void OnActivated(EventArgs e)
  {
    base.OnActivated(e);
    this._displayed = true;
  }

  protected override void OnDeactivate(EventArgs e)
  {
    base.OnDeactivate(e);
    this._displayed = false;
    this.Left = -this.Width;
  }

  public virtual void Show(Control owner, Rectangle bounds)
  {
    this.RightToLeft = owner.RightToLeft;
    int width = this.Width;
    int height = this.Height;
    if (!this.Created)
    {
      this.Width += 2;
      this.Height += 2;
    }
    Rectangle rectangle = owner.Bounds;
    if (!bounds.IsEmpty)
      rectangle = bounds;
    Point screen = owner.Parent.PointToScreen(rectangle.Location);
    if (!bounds.IsEmpty)
      screen = owner.PointToScreen(rectangle.Location);
    screen.X += rectangle.Width - width;
    screen.Y += rectangle.Height;
    Rectangle workingArea = Screen.FromControl(owner).WorkingArea;
    screen.X = Math.Min(workingArea.Right - width, Math.Max(workingArea.X, screen.X));
    if (screen.Y + this.Height > workingArea.Bottom)
      screen.Y = screen.Y - rectangle.Height - height + 1;
    this.Location = screen;
    this.Show();
    this.Activate();
  }

  public virtual void ShowModal(Control owner, Rectangle bounds)
  {
    this.Show(owner, bounds);
    while (this.Displayed)
    {
      Application.DoEvents();
      Thread.Sleep(20);
    }
  }

  protected virtual void WmNonClientPaint(ref Message m) => base.WndProc(ref m);

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 133)
      this.WmNonClientPaint(ref m);
    else
      base.WndProc(ref m);
  }

  [Browsable(false)]
  public Control ContainedControl
  {
    get => this.Controls.Count > 0 ? this.Controls[0] : (Control) null;
    set
    {
      this.Controls.Clear();
      if (value == null)
        return;
      value.Dock = DockStyle.Fill;
      this.Controls.Add(value);
    }
  }

  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      createParams.Style |= 8388608 /*0x800000*/;
      return createParams;
    }
  }

  [Browsable(false)]
  public bool Displayed => this._displayed;

  [Browsable(false)]
  public bool ManageContainedControlDisposal
  {
    get => this._manageContainedControlDisposal;
    set => this._manageContainedControlDisposal = value;
  }
}
