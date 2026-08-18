// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.KeywordReplacementDictForm
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class KeywordReplacementDictForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBody;
  private Panel panelButtons;
  private Button btnDefault;
  private Button btnDelete;
  private Button btnAdd;
  private Button btnCancel;
  private Button btnOK;
  private UCKeywordReplacementDict ctlKeywordReplacementDictionary;

  public long SettingsObjectID
  {
    get => this.ctlKeywordReplacementDictionary.SettingsObjectID;
    set => this.ctlKeywordReplacementDictionary.SettingsObjectID = value;
  }

  public KeywordReplacementDictForm(long settingsObjectID)
  {
    this.InitializeComponent();
    this.btnAdd.Enabled = this.btnDelete.Enabled = this.btnDefault.Enabled = true;
    this.ctlKeywordReplacementDictionary.IsChangedStateChanged += (EventHandler) ((s, e) => this.UpdateControls());
    this.SettingsObjectID = settingsObjectID;
  }

  private void UpdateControls()
  {
    this.btnOK.Enabled = this.ctlKeywordReplacementDictionary.IsChanged;
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    if (this.ctlKeywordReplacementDictionary.IsReadonly)
    {
      int num = (int) MessageBox.Show(this.ctlKeywordReplacementDictionary.ReadonlyReason, "Редактирование словаря замен", MessageBoxButtons.OK);
    }
    else
    {
      if (!this.ctlKeywordReplacementDictionary.IsSettingsObjectReadyToEdit())
        return;
      this.ctlKeywordReplacementDictionary.AddItem((string) null, (string) null, true);
    }
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (this.ctlKeywordReplacementDictionary.IsReadonly)
    {
      int num = (int) MessageBox.Show(this.ctlKeywordReplacementDictionary.ReadonlyReason, "Редактирование словаря замен", MessageBoxButtons.OK);
    }
    else
    {
      if (!this.ctlKeywordReplacementDictionary.IsSettingsObjectReadyToEdit())
        return;
      this.ctlKeywordReplacementDictionary.DeleteItem();
    }
  }

  private void btnDefault_Click(object sender, EventArgs e)
  {
    if (this.ctlKeywordReplacementDictionary.IsReadonly)
    {
      int num = (int) MessageBox.Show(this.ctlKeywordReplacementDictionary.ReadonlyReason, "Редактирование словаря замен", MessageBoxButtons.OK);
    }
    else
    {
      if (!this.ctlKeywordReplacementDictionary.IsSettingsObjectReadyToEdit())
        return;
      this.ctlKeywordReplacementDictionary.SetToDefault();
    }
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    this.ctlKeywordReplacementDictionary.SaveData();
    if (!this.ctlKeywordReplacementDictionary.AutoCheckedOut)
      return;
    this.ctlKeywordReplacementDictionary.CheckInSettingsObject();
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (!this.ctlKeywordReplacementDictionary.AutoCheckedOut)
      return;
    this.ctlKeywordReplacementDictionary.RollbackChangesInSettingsObject();
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
    this.panelBody = new Panel();
    this.panelButtons = new Panel();
    this.btnDefault = new Button();
    this.btnDelete = new Button();
    this.btnAdd = new Button();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.ctlKeywordReplacementDictionary = new UCKeywordReplacementDict();
    this.panelBody.SuspendLayout();
    this.panelButtons.SuspendLayout();
    this.SuspendLayout();
    this.panelBody.BackColor = SystemColors.Control;
    this.panelBody.BorderStyle = BorderStyle.Fixed3D;
    this.panelBody.Controls.Add((Control) this.ctlKeywordReplacementDictionary);
    this.panelBody.Dock = DockStyle.Fill;
    this.panelBody.ForeColor = SystemColors.ControlText;
    this.panelBody.Location = new Point(0, 0);
    this.panelBody.Margin = new Padding(3, 3, 13, 3);
    this.panelBody.MinimumSize = new Size(400, 370);
    this.panelBody.Name = "panelBody";
    this.panelBody.Padding = new Padding(0, 0, 0, 80 /*0x50*/);
    this.panelBody.Size = new Size(403, 371);
    this.panelBody.TabIndex = 7;
    this.panelButtons.Controls.Add((Control) this.btnDefault);
    this.panelButtons.Controls.Add((Control) this.btnDelete);
    this.panelButtons.Controls.Add((Control) this.btnAdd);
    this.panelButtons.Controls.Add((Control) this.btnCancel);
    this.panelButtons.Controls.Add((Control) this.btnOK);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(0, 291);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(403, 80 /*0x50*/);
    this.panelButtons.TabIndex = 8;
    this.btnDefault.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnDefault.Location = new Point(17, 41);
    this.btnDefault.Name = "btnDefault";
    this.btnDefault.Size = new Size(120, 27);
    this.btnDefault.TabIndex = 11;
    this.btnDefault.Text = "По умолчанию";
    this.btnDefault.UseVisualStyleBackColor = true;
    this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
    this.btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnDelete.Location = new Point(140, 12);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(120, 27);
    this.btnDelete.TabIndex = 10;
    this.btnDelete.Text = "Удалить";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnAdd.Location = new Point(17, 12);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(120, 27);
    this.btnAdd.TabIndex = 9;
    this.btnAdd.Text = "Добавить...";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(263, 41);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(120, 27);
    this.btnCancel.TabIndex = 8;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.FlatStyle = FlatStyle.System;
    this.btnOK.ImeMode = ImeMode.NoControl;
    this.btnOK.Location = new Point(140, 41);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(120, 27);
    this.btnOK.TabIndex = 7;
    this.btnOK.Text = "ОК";
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.ctlKeywordReplacementDictionary.Dock = DockStyle.Fill;
    this.ctlKeywordReplacementDictionary.Location = new Point(0, 0);
    this.ctlKeywordReplacementDictionary.Name = "ctlKeywordReplacementDictionary";
    this.ctlKeywordReplacementDictionary.SettingsObjectID = -1L;
    this.ctlKeywordReplacementDictionary.Size = new Size(399, 287);
    this.ctlKeywordReplacementDictionary.TabIndex = 0;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(403, 371);
    this.Controls.Add((Control) this.panelButtons);
    this.Controls.Add((Control) this.panelBody);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (KeywordReplacementDictForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Словарь замен";
    this.panelBody.ResumeLayout(false);
    this.panelButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
