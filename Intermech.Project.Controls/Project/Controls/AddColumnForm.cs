// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.AddColumnForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class AddColumnForm : Form
{
  [NotNull]
  [ItemNotNull]
  public readonly List<ColumnInfo> Columns = new List<ColumnInfo>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _buttonsPanel;
  private Button _cancButton;
  private Button _okButton;
  private GroupBox _groupBox1;
  private TextBox _textBox;
  private Label _label2;
  private Label _label1;
  private ComboBox _attrsCombo;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Panel ButtonsPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonsPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private GroupBox GroupBox1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._groupBox1.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private TextBox TextBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBox.CheckInitializedIn<TextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Label Label2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Label Label1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label1.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private ComboBox AttrsCombo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._attrsCombo.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  public AddColumnForm() => this.InitializeComponent();

  private void AddColumnForm_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AttrsCombo.Items.AddRange((object[]) this.Columns.ToArray());
    if (this.AttrsCombo.Items.Count <= 0)
      return;
    this.AttrsCombo.SelectedIndex = 0;
  }

  private void AttrsCombo_SelectedValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.TextBox.Text = this.AttrsCombo.SelectedItem?.ToString() ?? string.Empty;
  }

  [CanBeNull]
  public ColumnInfo SelectedColumnInfo
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.AttrsCombo.SelectedItem as ColumnInfo;
    }
  }

  [NotNull]
  public string ColumnText
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.TextBox.Text;
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
    this._buttonsPanel = new Panel();
    this._cancButton = new Button();
    this._okButton = new Button();
    this._groupBox1 = new GroupBox();
    this._textBox = new TextBox();
    this._label2 = new Label();
    this._label1 = new Label();
    this._attrsCombo = new ComboBox();
    this._buttonsPanel.SuspendLayout();
    this._groupBox1.SuspendLayout();
    this.SuspendLayout();
    this._buttonsPanel.BackColor = Color.Transparent;
    this._buttonsPanel.Controls.Add((Control) this._cancButton);
    this._buttonsPanel.Controls.Add((Control) this._okButton);
    this._buttonsPanel.Dock = DockStyle.Bottom;
    this._buttonsPanel.Location = new Point(15, 106);
    this._buttonsPanel.Name = "_buttonsPanel";
    this._buttonsPanel.Size = new Size(431, 37);
    this._buttonsPanel.TabIndex = 7;
    this._cancButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.ImeMode = ImeMode.NoControl;
    this._cancButton.Location = new Point(356, 14);
    this._cancButton.Name = "_cancButton";
    this._cancButton.Size = new Size(75, 23);
    this._cancButton.TabIndex = 101;
    this._cancButton.Text = "Отмена";
    this._okButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.ImeMode = ImeMode.NoControl;
    this._okButton.Location = new Point(275, 14);
    this._okButton.Name = "_okButton";
    this._okButton.Size = new Size(75, 23);
    this._okButton.TabIndex = 100;
    this._okButton.Text = "OK";
    this._groupBox1.Controls.Add((Control) this._textBox);
    this._groupBox1.Controls.Add((Control) this._label2);
    this._groupBox1.Controls.Add((Control) this._label1);
    this._groupBox1.Controls.Add((Control) this._attrsCombo);
    this._groupBox1.Dock = DockStyle.Fill;
    this._groupBox1.Location = new Point(15, 15);
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.Size = new Size(431, 91);
    this._groupBox1.TabIndex = 8;
    this._groupBox1.TabStop = false;
    this._textBox.Location = new Point(115, 53);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(288, 20);
    this._textBox.TabIndex = 7;
    this._label2.AutoSize = true;
    this._label2.Location = new Point(15, 56);
    this._label2.Name = "_label2";
    this._label2.Size = new Size(64 /*0x40*/, 13);
    this._label2.TabIndex = 6;
    this._label2.Text = "Заголовок:";
    this._label1.AutoSize = true;
    this._label1.Location = new Point(15, 29);
    this._label1.Name = "_label1";
    this._label1.Size = new Size(50, 13);
    this._label1.TabIndex = 5;
    this._label1.Text = "Атрибут:";
    this._attrsCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._attrsCombo.FormattingEnabled = true;
    this._attrsCombo.Location = new Point(115, 26);
    this._attrsCombo.Name = "_attrsCombo";
    this._attrsCombo.Size = new Size(288, 21);
    this._attrsCombo.Sorted = true;
    this._attrsCombo.TabIndex = 4;
    this._attrsCombo.SelectedValueChanged += new EventHandler(this.AttrsCombo_SelectedValueChanged);
    this.AcceptButton = (IButtonControl) this._okButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancButton;
    this.ClientSize = new Size(461, 158);
    this.Controls.Add((Control) this._groupBox1);
    this.Controls.Add((Control) this._buttonsPanel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AddColumnForm);
    this.Padding = new Padding(15);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Добавление колонки";
    this.Load += new EventHandler(this.AddColumnForm_Load);
    this._buttonsPanel.ResumeLayout(false);
    this._groupBox1.ResumeLayout(false);
    this._groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
