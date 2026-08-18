
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersEditorForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CompositionByObjectTypesFiltersEditorControl _compositionByObjectTypesFiltersEditorControl;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _closeButton;

  public CompositionByObjectTypesFiltersEditorForm() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ObjectVersionID
  {
    get => this._compositionByObjectTypesFiltersEditorControl.ObjectVersionID;
    set => this._compositionByObjectTypesFiltersEditorControl.ObjectVersionID = value;
  }

  private void CompositionByObjectTypesFiltersEditorForm_FormClosed(
    object sender,
    FormClosedEventArgs e)
  {
    Hashtable hashtable = new Hashtable();
    CompositionByObjectTypesFiltersEditorControl.CompositionByObjectTypesFiltersEditorControlMemento memento = this._compositionByObjectTypesFiltersEditorControl.GetMemento();
    hashtable[(object) "SplitterPosition"] = (object) memento.SplitterPosition;
    hashtable[(object) "TreeNodeColumns"] = (object) memento.TreeNodeColumns;
    FormStorage.SaveLayout((Control) this, (IDictionary) hashtable);
  }

  private void CompositionByObjectTypesFiltersEditorForm_Load(object sender, EventArgs e)
  {
    Hashtable hashtable = new Hashtable();
    FormStorage.LoadLayout((Control) this, (IDictionary) hashtable);
    if (!hashtable.ContainsKey((object) "SplitterPosition"))
      return;
    this._compositionByObjectTypesFiltersEditorControl.SetMemento(new CompositionByObjectTypesFiltersEditorControl.CompositionByObjectTypesFiltersEditorControlMemento()
    {
      SplitterPosition = (double) hashtable[(object) "SplitterPosition"],
      TreeNodeColumns = hashtable[(object) "TreeNodeColumns"] as NodeColumnCollection
    });
  }

  private void СloseButton_Click(object sender, EventArgs e) => this.Close();

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
    this._compositionByObjectTypesFiltersEditorControl = new CompositionByObjectTypesFiltersEditorControl();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._closeButton = new Button();
    this._compositionByObjectTypesFiltersEditorControl.BeginInit();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this._compositionByObjectTypesFiltersEditorControl.Dock = DockStyle.Fill;
    this._compositionByObjectTypesFiltersEditorControl.Location = new Point(3, 3);
    this._compositionByObjectTypesFiltersEditorControl.Name = "_compositionByObjectTypesFiltersEditorControl";
    this._compositionByObjectTypesFiltersEditorControl.Size = new Size(524, 271);
    this._compositionByObjectTypesFiltersEditorControl.TabIndex = 0;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this._compositionByObjectTypesFiltersEditorControl, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
    this.tableLayoutPanel1.Size = new Size(530, 317);
    this.tableLayoutPanel1.TabIndex = 1;
    this.flowLayoutPanel1.Controls.Add((Control) this._closeButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 280);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(524, 34);
    this.flowLayoutPanel1.TabIndex = 1;
    this._closeButton.Location = new Point(446, 3);
    this._closeButton.Name = "_closeButton";
    this._closeButton.Size = new Size(75, 23);
    this._closeButton.TabIndex = 0;
    this._closeButton.Text = "Закрыть";
    this._closeButton.UseVisualStyleBackColor = true;
    this._closeButton.Click += new EventHandler(this.СloseButton_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(530, 317);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (CompositionByObjectTypesFiltersEditorForm);
    this.ShowIcon = false;
    this.Text = "Фильтрация состава по родительским и дочерним типам объектов";
    this.FormClosed += new FormClosedEventHandler(this.CompositionByObjectTypesFiltersEditorForm_FormClosed);
    this.Load += new EventHandler(this.CompositionByObjectTypesFiltersEditorForm_Load);
    this._compositionByObjectTypesFiltersEditorControl.EndInit();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
