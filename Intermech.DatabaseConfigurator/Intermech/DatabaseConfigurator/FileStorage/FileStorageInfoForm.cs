// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileStorageInfoForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Objects;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FileStorageInfoForm : Form
{
  private IContainer components;
  private Button _bClose;
  private GroupBox groupBox1;
  private Label label3;
  private Label label2;
  private Label label1;
  private Label lFilesPackedSize;
  private Label lFilesSize;
  private Label lFilesCount;
  private Label lPercent;
  private Label label5;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileStorageInfoForm));
    this._bClose = new Button();
    this.groupBox1 = new GroupBox();
    this.lPercent = new Label();
    this.label5 = new Label();
    this.lFilesPackedSize = new Label();
    this.lFilesSize = new Label();
    this.lFilesCount = new Label();
    this.label3 = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._bClose, "_bClose");
    this._bClose.DialogResult = DialogResult.Cancel;
    this._bClose.Name = "_bClose";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.lPercent);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.Controls.Add((Control) this.lFilesPackedSize);
    this.groupBox1.Controls.Add((Control) this.lFilesSize);
    this.groupBox1.Controls.Add((Control) this.lFilesCount);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.label2);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lPercent, "lPercent");
    this.lPercent.Name = "lPercent";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.lFilesPackedSize, "lFilesPackedSize");
    this.lFilesPackedSize.Name = "lFilesPackedSize";
    componentResourceManager.ApplyResources((object) this.lFilesSize, "lFilesSize");
    this.lFilesSize.Name = "lFilesSize";
    componentResourceManager.ApplyResources((object) this.lFilesCount, "lFilesCount");
    this.lFilesCount.Name = "lFilesCount";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this._bClose);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FileStorageInfoForm);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }

  public FileStorageInfoForm(long objID)
  {
    this.InitializeComponent();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objID, true);
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(dbObject.ObjectType, true);
      this.Text = dbObject.Caption != string.Empty ? $"{objectType.ObjectInstanceName} \"{dbObject.Caption}\"" : $"{objectType.ObjectInstanceName} {dbObject.ObjectID.ToString()}";
      FileStorageInfo fileStorageInfo = (dbObject as IBlobStorageObject).GetFileStorageInfo();
      this.lFilesCount.Text = fileStorageInfo.FilesCount.ToString();
      this.lFilesPackedSize.Text = Win32Subst.StrFormatByteSize(fileStorageInfo.PackedFilesSize, 1);
      this.lFilesSize.Text = Win32Subst.StrFormatByteSize(fileStorageInfo.RealFilesSize, 1);
      double d = Math.Ceiling((double) fileStorageInfo.PackedFilesSize * 100.0 / (double) fileStorageInfo.RealFilesSize);
      if (!double.IsNaN(d))
        this.lPercent.Text = d.ToString() + " %";
      else
        this.lPercent.Text = "-";
    }
  }
}
