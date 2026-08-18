
// Type: Intermech.Bars.FlatComboBox
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public class FlatComboBox : ComboBox
    {
      private bool _a;
      private string _defaultText;
      private bool c;
      private IComboBoxRenderer _renderer;
      private Timer e;

      public event EventHandler DefaultTextChanged;

      public FlatComboBox()
      {
        this._defaultText = string.Empty;
        this.c = false;
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this._renderer = (IComboBoxRenderer) new Office2002Renderer();
        this.e = new Timer();
        this.e.Interval = 50;
        this.e.Tick += new EventHandler(this.Timer_Tick);
      }

      private void OnWmPaint()
      {
        using (Graphics graphics = Graphics.FromHwnd(this.Handle))
        {
          IComboBoxRenderer comboBoxRenderer = this._renderer;
          if (this.Parent is ToolBar)
            comboBoxRenderer = (IComboBoxRenderer) ((ToolBar) this.Parent).WorkingRenderer;
          DrawItemState state = DrawItemState.Default;
          if (this._a || this.ContainsFocus)
            state |= DrawItemState.HotLight;
          if (!this.Enabled)
            state |= DrawItemState.Disabled;
          if (this.DroppedDown)
            state |= DrawItemState.Selected;
          if (comboBoxRenderer is Office2003Renderer)
            ((Office2003Renderer) comboBoxRenderer)._comboBox = (ComboBox) this;
          comboBoxRenderer.DrawComboBox((ComboBox) this, graphics, this.ClientRectangle, state, this.RightToLeft == RightToLeft.Yes);
        }
      }

      private void Timer_Tick(object A_0, EventArgs A_1)
      {
        if (!this.IsDisposed && this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
          return;
        this.e.Enabled = false;
        this._a = false;
        if (this.IsDisposed)
          return;
        this.Invalidate();
      }

      private void b()
      {
        if (this.DropDownStyle == ComboBoxStyle.DropDown && this.Text.Length == 0 && !this.ContainsFocus && !this.c)
        {
          base.Text = this._defaultText;
          this.ForeColor = SystemColors.ControlDark;
          this.c = true;
        }
        else
        {
          if (!this.c)
            return;
          this.ForeColor = SystemColors.ControlText;
          this.c = false;
          base.Text = string.Empty;
        }
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
          this._renderer.Dispose();
        base.Dispose(disposing);
      }

      protected virtual void OnDefaultTextChanged()
      {
        if (this.DefaultTextChanged == null)
          return;
        this.DefaultTextChanged((object) this, EventArgs.Empty);
      }

      protected override void OnEnabledChanged(EventArgs e)
      {
        base.OnEnabledChanged(e);
        if (this._a && !this.Enabled)
          this._a = false;
        this.Invalidate();
      }

      protected override void OnGotFocus(EventArgs e)
      {
        base.OnGotFocus(e);
        this.b();
        this.Invalidate();
      }

      protected override void OnLostFocus(EventArgs e)
      {
        base.OnLostFocus(e);
        this.b();
        this.Invalidate();
      }

      protected override void OnMouseMove(MouseEventArgs e)
      {
        base.OnMouseMove(e);
        if (this._a)
          return;
        this._a = true;
        this.e.Enabled = true;
        this.Invalidate();
      }

      protected override void WndProc(ref Message m)
      {
        try
        {
          if (m.Msg == 15)
          {
            base.WndProc(ref m);
            this.OnWmPaint();
            m.Result = IntPtr.Zero;
          }
          else
            base.WndProc(ref m);
        }
        catch
        {
        }
      }

      [Category("Appearance")]
      [Description("Provides a textual hint as to the type of data to enter, before any is entered.")]
      [DefaultValue("")]
      [Localizable(true)]
      public string DefaultText
      {
        get => this._defaultText;
        set
        {
          this._defaultText = value != null ? value : throw new ArgumentNullException();
          if (this.c)
          {
            if (value.Length == 0)
            {
              this.c = false;
              this.ForeColor = SystemColors.ControlText;
              this.Text = string.Empty;
            }
            base.Text = value;
          }
          else
            this.b();
          this.OnDefaultTextChanged();
        }
      }

      public override string Text
      {
        get => this.c ? string.Empty : base.Text;
        set
        {
          if (value == null)
            value = string.Empty;
          if (this.c)
          {
            if (value.Length <= 0)
              return;
            this.c = false;
            this.ForeColor = SystemColors.ControlText;
            base.Text = value;
          }
          else if (value.Length == 0)
          {
            if (this.c)
              return;
            this.c = true;
            this.ForeColor = SystemColors.ControlDark;
            base.Text = this.DefaultText;
          }
          else
            base.Text = value;
        }
      }
    }
}
