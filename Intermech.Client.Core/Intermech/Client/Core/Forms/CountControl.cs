
// Type: Intermech.Client.Core.Forms.CountControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Forms;

public class CountControl : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _button;
  private Label _label;

  public CountControl() => this.InitializeComponent();

  [NotNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public string Value
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._label.Text;
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._label.Text = value;
    }
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this._label.BackColor = this.BackColor;
    this._label.ForeColor = this.ForeColor;
    this._label.Font = this.Font;
  }

  private void CountControl_BackColorChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._label.BackColor = this.BackColor;
  }

  private void CountControl_ForeColorChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._label.ForeColor = this.ForeColor;
  }

  private void CountControl_FontChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._label.Font = this.Font;
  }

  public new event EventHandler Click
  {
    add => this._button.Click += value;
    remove => this._button.Click -= value;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._button = new Button();
    this._label = new Label();
    this.SuspendLayout();
    this._button.AutoSize = true;
    this._button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this._button.Dock = DockStyle.Right;
    this._button.Location = new Point(164, 0);
    this._button.Name = "_button";
    this._button.Size = new Size(26, 28);
    this._button.TabIndex = 0;
    this._button.Text = "...";
    this._button.UseVisualStyleBackColor = true;
    this._label.AutoEllipsis = true;
    this._label.Dock = DockStyle.Fill;
    this._label.Location = new Point(0, 0);
    this._label.Name = "_label";
    this._label.Padding = new Padding(5);
    this._label.Size = new Size(164, 28);
    this._label.TabIndex = 1;
    this._label.Text = "sadfasdf";
    this._label.TextAlign = ContentAlignment.MiddleLeft;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._label);
    this.Controls.Add((Control) this._button);
    this.Name = nameof (CountControl);
    this.Size = new Size(190, 28);
    this.BackColorChanged += new EventHandler(this.CountControl_BackColorChanged);
    this.FontChanged += new EventHandler(this.CountControl_FontChanged);
    this.ForeColorChanged += new EventHandler(this.CountControl_ForeColorChanged);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
