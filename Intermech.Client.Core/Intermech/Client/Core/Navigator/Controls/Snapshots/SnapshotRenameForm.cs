
// Type: Intermech.Client.Core.Navigator.Controls.Snapshots.SnapshotRenameForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Controls.Snapshots;

/// <summary>Форма для переименования итерации</summary>
public class SnapshotRenameForm : Form
{
  /// <summary>ИД итерации</summary>
  private readonly long _snapshotID;
  /// <summary>ИД объекта</summary>
  private readonly long _objectID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lblNewName;
  private TextBox textBox2;
  private Button btnCancel;
  private Button btnOK;

  public SnapshotRenameForm() => this.InitializeComponent();

  public SnapshotRenameForm(long objectID, long snapshotID)
    : this()
  {
    this._snapshotID = snapshotID;
    this._objectID = objectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.textBox2.Text = sessionKeeper.Session.GetSnapshot(this._snapshotID).SnapshotName;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    string text = this.textBox2.Text;
    if (string.IsNullOrWhiteSpace(text))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1622"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetSnapshot(this._snapshotID).SnapshotName = text;
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
        service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("SnapshotsChanged", this._objectID));
      this.Close();
    }
  }

  private void btnCancel_Click(object sender, EventArgs e) => this.Close();

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
    this.lblNewName = new Label();
    this.textBox2 = new TextBox();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.SuspendLayout();
    this.lblNewName.AutoSize = true;
    this.lblNewName.Location = new Point(12, 9);
    this.lblNewName.Name = "lblNewName";
    this.lblNewName.Size = new Size(169, 13);
    this.lblNewName.TabIndex = 1;
    this.lblNewName.Text = "Новое наименование итерации:";
    this.textBox2.Location = new Point(12, 34);
    this.textBox2.Name = "textBox2";
    this.textBox2.Size = new Size(348, 20);
    this.textBox2.TabIndex = 3;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(240 /*0xF0*/, 68);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 5;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.ImeMode = ImeMode.NoControl;
    this.btnOK.Location = new Point(113, 68);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 4;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(372, 107);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.textBox2);
    this.Controls.Add((Control) this.lblNewName);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SnapshotRenameForm);
    this.Text = "Переименование итерации";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
