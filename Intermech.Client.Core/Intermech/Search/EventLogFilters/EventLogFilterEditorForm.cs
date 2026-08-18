
// Type: Intermech.Search.EventLogFilters.EventLogFilterEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.EventLogFilters;

public sealed class EventLogFilterEditorForm : Form
{
  private EventLogFilter _filter;
  private bool _hasChanges;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _cancelButton;
  private Button _acceptButton;
  private EventLogFilterEditorControl _eventLogFilterEditorControl;

  public EventLogFilterEditorForm()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public EventLogFilter Filter
  {
    get => this._filter;
    set
    {
      if (this._filter == value)
        return;
      if (this._filter != null)
        this._filter.PropertyChanged -= new PropertyChangedEventHandler(this.Filter_PropertyChanged);
      this._filter = value;
      this._eventLogFilterEditorControl.Filter = this._filter;
      this.UpdateControls();
      if (this._filter == null)
        return;
      this._filter.PropertyChanged += new PropertyChangedEventHandler(this.Filter_PropertyChanged);
    }
  }

  private void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    this._hasChanges = true;
    this.UpdateControls();
  }

  private void EventLogFilterEditorForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void EventLogFilterEditorForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void UpdateControls()
  {
    this._acceptButton.Enabled = this._filter != null && this._hasChanges;
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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this._eventLogFilterEditorControl = new EventLogFilterEditorControl();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this._eventLogFilterEditorControl.BeginInit();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this._eventLogFilterEditorControl, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(484, 611);
    this.tableLayoutPanel1.TabIndex = 0;
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._acceptButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 579);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(478, 29);
    this.flowLayoutPanel1.TabIndex = 0;
    this._cancelButton.AutoSize = true;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Location = new Point(400, 3);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 0;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._acceptButton.AutoSize = true;
    this._acceptButton.DialogResult = DialogResult.OK;
    this._acceptButton.Location = new Point(319, 3);
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Size = new Size(75, 23);
    this._acceptButton.TabIndex = 1;
    this._acceptButton.Text = "OK";
    this._acceptButton.UseVisualStyleBackColor = true;
    this._eventLogFilterEditorControl.Dock = DockStyle.Fill;
    this._eventLogFilterEditorControl.Location = new Point(3, 3);
    this._eventLogFilterEditorControl.Name = "_eventLogFilterEditorControl";
    this._eventLogFilterEditorControl.Size = new Size(478, 570);
    this._eventLogFilterEditorControl.TabIndex = 1;
    this.AcceptButton = (IButtonControl) this._acceptButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(484, 611);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (EventLogFilterEditorForm);
    this.ShowIcon = false;
    this.Text = "Редактор фильтра журнала событий";
    this.FormClosed += new FormClosedEventHandler(this.EventLogFilterEditorForm_FormClosed);
    this.Load += new EventHandler(this.EventLogFilterEditorForm_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this._eventLogFilterEditorControl.EndInit();
    this.ResumeLayout(false);
  }
}
