
// Type: Intermech.Search.EditingContexts.EditingContextEditorDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.EditingContexts;

public sealed class EditingContextEditorDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private EditingContextEditorControl _editingContextEditorControl;

  public EditingContextEditorDialog() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long EditingContextVersionID
  {
    get => this._editingContextEditorControl.EditingContextVersionID;
    set => this._editingContextEditorControl.EditingContextVersionID = value;
  }

  private void EditingContextDialog_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this._editingContextEditorControl.HasChanges)
      return;
    switch (MessageBox.Show("Состав контекста редактирования был изменен. Сохранить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
    {
      case DialogResult.Cancel:
        e.Cancel = true;
        break;
      case DialogResult.Yes:
        this._editingContextEditorControl.AcceptChanges();
        break;
    }
  }

  private void EditingContextDialog_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void EditingContextDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    this._editingContextEditorControl = new EditingContextEditorControl();
    ((ISupportInitialize) this._editingContextEditorControl).BeginInit();
    this.SuspendLayout();
    this._editingContextEditorControl.Dock = DockStyle.Fill;
    this._editingContextEditorControl.Location = new Point(0, 0);
    this._editingContextEditorControl.Name = "_editingContextEditorControl";
    this._editingContextEditorControl.Size = new Size(779, 355);
    this._editingContextEditorControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(779, 355);
    this.Controls.Add((Control) this._editingContextEditorControl);
    this.Name = nameof (EditingContextEditorDialog);
    this.Text = "Редактор контекста редактирования";
    this.FormClosing += new FormClosingEventHandler(this.EditingContextDialog_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.EditingContextDialog_FormClosed);
    this.Load += new EventHandler(this.EditingContextDialog_Load);
    ((ISupportInitialize) this._editingContextEditorControl).EndInit();
    this.ResumeLayout(false);
  }
}
