
// Type: Intermech.ComparisonPlugins.PDFComparison.UI.CustomUpDown




using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.ComparisonPlugins.PDFComparison.UI
{
    [DefaultEvent("ValueChanged")]
    public class CustomUpDown : UserControl
    {
      private double _value;
      private IContainer components;
      private Button buttonDown;
      private TextBox textBoxValue;
      private Button buttonUp;

      public event EventHandler ValueChanged;

      [DefaultValue(1)]
      public float Increment { get; set; }

      [DefaultValue(0)]
      public int DigitsCount { get; set; }

      [DefaultValue(0)]
      public double Value
      {
        get => this._value;
        set
        {
          this._value = Math.Round(value, this.DigitsCount);
          this.textBoxValue.Text = this._value.ToString($"F{this.DigitsCount}");
          EventHandler valueChanged = this.ValueChanged;
          if (valueChanged == null)
            return;
          valueChanged((object) null, EventArgs.Empty);
        }
      }

      public CustomUpDown()
      {
        this.InitializeComponent();
        this.Increment = 1f;
        this.DigitsCount = 0;
        this.Value = 0.0;
      }

      private void increment() => this.Value += (double) this.Increment;

      private void decrement() => this.Value -= (double) this.Increment;

      private void buttonUp_Click(object sender, EventArgs e) => this.increment();

      private void buttonDown_Click(object sender, EventArgs e) => this.decrement();

      private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
      {
        if (e.KeyData != Keys.Return)
          return;
        double result;
        double.TryParse(this.textBoxValue.Text, out result);
        this.Value = result;
      }

      private void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
      {
        FormHelper.CheckEnterFormat(e);
      }

      private void textBoxValue_Leave(object sender, EventArgs e)
      {
        this.textBoxValue.Text = this.Value.ToString($"F{this.DigitsCount}");
      }

      public override bool Focused
      {
        get
        {
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            if (control.Focused)
              return true;
          }
          return false;
        }
      }

      protected override void OnMouseWheel(MouseEventArgs e)
      {
        if (!this.Focused)
          return;
        base.OnMouseWheel(e);
        if (e.Delta > 0)
          this.increment();
        if (e.Delta >= 0)
          return;
        this.decrement();
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.buttonUp = new Button();
        this.buttonDown = new Button();
        this.textBoxValue = new TextBox();
        this.SuspendLayout();
        this.buttonUp.FlatStyle = FlatStyle.System;
        this.buttonUp.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
        this.buttonUp.Location = new Point(-1, -1);
        this.buttonUp.Name = "buttonUp";
        this.buttonUp.Size = new Size(15, 22);
        this.buttonUp.TabIndex = 0;
        this.buttonUp.Text = "+";
        this.buttonUp.UseVisualStyleBackColor = true;
        this.buttonUp.Click += new EventHandler(this.buttonUp_Click);
        this.buttonDown.FlatStyle = FlatStyle.System;
        this.buttonDown.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
        this.buttonDown.Location = new Point(61, -1);
        this.buttonDown.Name = "buttonDown";
        this.buttonDown.Size = new Size(15, 22);
        this.buttonDown.TabIndex = 0;
        this.buttonDown.Text = "-";
        this.buttonDown.UseVisualStyleBackColor = true;
        this.buttonDown.Click += new EventHandler(this.buttonDown_Click);
        this.textBoxValue.Location = new Point(14, 0);
        this.textBoxValue.Name = "textBoxValue";
        this.textBoxValue.Size = new Size(47, 20);
        this.textBoxValue.TabIndex = 1;
        this.textBoxValue.TextAlign = HorizontalAlignment.Center;
        this.textBoxValue.KeyDown += new KeyEventHandler(this.textBoxValue_KeyDown);
        this.textBoxValue.KeyPress += new KeyPressEventHandler(this.textBoxValue_KeyPress);
        this.textBoxValue.Leave += new EventHandler(this.textBoxValue_Leave);
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add((Control) this.textBoxValue);
        this.Controls.Add((Control) this.buttonDown);
        this.Controls.Add((Control) this.buttonUp);
        this.Name = nameof (CustomUpDown);
        this.Size = new Size(75, 20);
        this.ResumeLayout(false);
        this.PerformLayout();
      }
    }
}
