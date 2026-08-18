// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.FindStringForm
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

internal sealed class FindStringForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _cancelButton;
  private Button _findButton;
  private Label _label1;
  private GroupBox _groupBox1;
  private RadioButton _upButton;
  private RadioButton _downButton;
  private CheckBox _caseCheckBox;
  private TextBox _textBox;
  [NotNull]
  private static readonly Dictionary<string, AutoCompleteStringCollection> _autoCompleteStringCollections = new Dictionary<string, AutoCompleteStringCollection>();
  [NotNull]
  private string _domain = string.Empty;
  [NotNull]
  private static string _savedFindString = string.Empty;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancelButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private Button FindButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._findButton.CheckInitializedIn<Button>((object) this);
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
  private RadioButton UpButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._upButton.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private RadioButton DownButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._downButton.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private CheckBox CaseCheckBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._caseCheckBox.CheckInitializedIn<CheckBox>((object) this);
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
    this._cancelButton = new Button();
    this._findButton = new Button();
    this._textBox = new TextBox();
    this._label1 = new Label();
    this._groupBox1 = new GroupBox();
    this._upButton = new RadioButton();
    this._downButton = new RadioButton();
    this._caseCheckBox = new CheckBox();
    this._groupBox1.SuspendLayout();
    this.SuspendLayout();
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(273, 41);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(81, 23);
    this._cancelButton.TabIndex = 2;
    this._cancelButton.Text = "&Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._findButton.DialogResult = DialogResult.OK;
    this._findButton.Location = new Point(273, 12);
    this._findButton.Name = "_findButton";
    this._findButton.Size = new Size(81, 23);
    this._findButton.TabIndex = 1;
    this._findButton.Text = "&Найти далее";
    this._findButton.UseVisualStyleBackColor = true;
    this._textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this._textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
    this._textBox.Location = new Point(47, 12);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(220, 20);
    this._textBox.TabIndex = 0;
    this._label1.AutoSize = true;
    this._label1.Location = new Point(12, 15);
    this._label1.Name = "_label1";
    this._label1.Size = new Size(29, 13);
    this._label1.TabIndex = 3;
    this._label1.Text = "Чт&о:";
    this._groupBox1.Controls.Add((Control) this._upButton);
    this._groupBox1.Controls.Add((Control) this._downButton);
    this._groupBox1.Location = new Point(144 /*0x90*/, 41);
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.Size = new Size(123, 46);
    this._groupBox1.TabIndex = 4;
    this._groupBox1.TabStop = false;
    this._groupBox1.Text = "Направление";
    this._upButton.AutoSize = true;
    this._upButton.Location = new Point(7, 19);
    this._upButton.Name = "_upButton";
    this._upButton.Size = new Size(55, 17);
    this._upButton.TabIndex = 1;
    this._upButton.Text = "Вверх";
    this._upButton.UseVisualStyleBackColor = true;
    this._downButton.AutoSize = true;
    this._downButton.Checked = true;
    this._downButton.Location = new Point(68, 19);
    this._downButton.Name = "_downButton";
    this._downButton.Size = new Size(50, 17);
    this._downButton.TabIndex = 0;
    this._downButton.TabStop = true;
    this._downButton.Text = "Вниз";
    this._downButton.UseVisualStyleBackColor = true;
    this._caseCheckBox.AutoSize = true;
    this._caseCheckBox.Location = new Point(15, 70);
    this._caseCheckBox.Name = "_caseCheckBox";
    this._caseCheckBox.Size = new Size(120, 17);
    this._caseCheckBox.TabIndex = 5;
    this._caseCheckBox.Text = "С у&четом регистра";
    this._caseCheckBox.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._findButton;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(367, 103);
    this.Controls.Add((Control) this._caseCheckBox);
    this.Controls.Add((Control) this._groupBox1);
    this.Controls.Add((Control) this._label1);
    this.Controls.Add((Control) this._textBox);
    this.Controls.Add((Control) this._findButton);
    this.Controls.Add((Control) this._cancelButton);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FindStringForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Найти";
    this._groupBox1.ResumeLayout(false);
    this._groupBox1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public event FindStringForm.FindEventHandler Find;

  public FindStringForm()
  {
    this.InitializeComponent();
    this.TextBox.Text = FindStringForm._savedFindString;
    this.Domain = string.Empty;
  }

  public FindStringForm([NotNull] string domain)
    : this()
  {
    this.Domain = domain;
  }

  private void cancelButton_Click([CanBeNull] object sender, [NotNull] EventArgs e) => this.Close();

  private void findButton_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    FindStringForm._savedFindString = this.TextBox.Text;
    AutoCompleteStringCollection stringCollection = this.AutoCompleteStringCollection;
    if (stringCollection != null && !stringCollection.Contains(FindStringForm._savedFindString))
      stringCollection.Add(FindStringForm._savedFindString);
    FindStringForm.FindEventHandler find = this.Find;
    if (find == null)
      return;
    find((object) this, new FindStringForm.FindEventArgs(this.FindString));
  }

  private void FindStringForm_Activated([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.TextBox.Focus();
    this.TextBox.SelectAll();
  }

  [CanBeNull]
  public AutoCompleteStringCollection AutoCompleteStringCollection
  {
    get => FindStringForm._autoCompleteStringCollections[this.Domain];
  }

  [NotNull]
  public string Domain
  {
    get => this._domain;
    set
    {
      this._domain = value;
      if (!FindStringForm._autoCompleteStringCollections.ContainsKey(this.Domain))
        FindStringForm._autoCompleteStringCollections[this.Domain] = new AutoCompleteStringCollection();
      this.TextBox.AutoCompleteCustomSource = this.AutoCompleteStringCollection;
    }
  }

  [NotNull]
  public string FindString => this.TextBox.Text;

  public int CurrentIndex { get; set; }

  public bool DirectionDown => this.DownButton.Checked;

  public bool FindInString([NotNull] string s)
  {
    return this.CaseCheckBox.Checked ? s.Contains(this.FindString) : s.ToLower().Contains(this.FindString.ToLower());
  }

  public class FindEventArgs : EventArgs
  {
    public FindEventArgs([NotNull] string findString) => this.FindString = findString;

    [NotNull]
    public string FindString { get; }
  }

  public delegate void FindEventHandler([CanBeNull] object sender, [NotNull] FindStringForm.FindEventArgs e);
}
