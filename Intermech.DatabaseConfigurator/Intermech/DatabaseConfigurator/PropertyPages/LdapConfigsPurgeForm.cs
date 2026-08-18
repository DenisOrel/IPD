// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.LdapConfigsPurgeForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class LdapConfigsPurgeForm : Form
{
  private bool modified;
  private string defaultCatalogName = string.Empty;
  private IContainer components;
  private Button btnCancel;
  private Button btnDelete;
  private ListView lvCatalogs;
  private ColumnHeader chName;
  private Label label1;
  private Label lDefaultCatalog;
  private Button btnOk;
  private GroupBox groupBox1;

  public LdapConfigsPurgeForm()
  {
    this.InitializeComponent();
    this.lDefaultCatalog.Font = new Font(this.lDefaultCatalog.Font, FontStyle.Bold);
  }

  private void LdapConfigsPurgeForm_Load(object sender, EventArgs e)
  {
    this.LoadConfig();
    this.UpdateControls();
  }

  private void UpdateControls()
  {
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this.btnOk.Enabled = service != null || service.IsAdmin;
    this.btnDelete.Enabled = this.btnOk.Enabled;
  }

  private void LoadConfig()
  {
    this.ClearForm();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      HybridDictionary catalogsAndExclusionUsers;
      customService.SynchronizeDirectoryReadConfig(sessionKeeper.Session.SessionGUID, out this.defaultCatalogName, out catalogsAndExclusionUsers);
      this.FillForm(this.defaultCatalogName, catalogsAndExclusionUsers);
    }
  }

  private void ClearForm()
  {
    this.lDefaultCatalog.Text = string.Empty;
    this.lvCatalogs.Items.Clear();
    this.modified = false;
  }

  private void FillForm(string defaultCatalogName, HybridDictionary catalogsAndExclusionUsers)
  {
    this.lDefaultCatalog.Text = defaultCatalogName;
    this.lvCatalogs.Items.Clear();
    foreach (DictionaryEntry andExclusionUser in catalogsAndExclusionUsers)
      this.lvCatalogs.Items.Add(andExclusionUser.Key.ToString()).Tag = andExclusionUser.Value;
  }

  public new DialogResult ShowDialog() => base.ShowDialog();

  private void btnDelete_Click(object sender, EventArgs e)
  {
    foreach (ListViewItem selectedItem in this.lvCatalogs.SelectedItems)
    {
      this.lvCatalogs.Items.Remove(selectedItem);
      this.modified = true;
    }
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (!this.modified)
      return;
    switch (IMMessageBox.Show("Подтверждение", "Сохранить изменения настроек синхронизации?", MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
    {
      case DialogResult.Cancel:
        this.DialogResult = DialogResult.None;
        break;
      case DialogResult.Yes:
        HybridDictionary catalogsAndExclusionUsers = new HybridDictionary();
        for (int index = 0; index < this.lvCatalogs.Items.Count; ++index)
          catalogsAndExclusionUsers.Add((object) this.lvCatalogs.Items[index].Text, this.lvCatalogs.Items[index].Tag);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService) || customService.SynchronizeDirectoryWriteConfig(sessionKeeper.Session.SessionGUID, this.defaultCatalogName, catalogsAndExclusionUsers, false) == 0)
            break;
          int num = (int) IMMessageBox.Show("Ошибка", "Ошибка сохранения настроек синхронизации", MessageBoxButtons.OK, IMMessageBoxImage.Error);
          this.DialogResult = DialogResult.None;
          break;
        }
      case DialogResult.No:
        break;
      default:
        this.DialogResult = DialogResult.None;
        break;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.btnCancel = new Button();
    this.btnDelete = new Button();
    this.lvCatalogs = new ListView();
    this.label1 = new Label();
    this.lDefaultCatalog = new Label();
    this.chName = new ColumnHeader();
    this.btnOk = new Button();
    this.groupBox1 = new GroupBox();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(552, 223);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnDelete.Location = new Point(6, 151);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(75, 23);
    this.btnDelete.TabIndex = 2;
    this.btnDelete.Text = "Удалить";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.lvCatalogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvCatalogs.Columns.AddRange(new ColumnHeader[1]
    {
      this.chName
    });
    this.lvCatalogs.HideSelection = false;
    this.lvCatalogs.Location = new Point(6, 19);
    this.lvCatalogs.Name = "lvCatalogs";
    this.lvCatalogs.Size = new Size(601, 124);
    this.lvCatalogs.TabIndex = 3;
    this.lvCatalogs.UseCompatibleStateImageBehavior = false;
    this.lvCatalogs.View = View.Details;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(17, 13);
    this.label1.Name = "label1";
    this.label1.Size = new Size(128 /*0x80*/, 13);
    this.label1.TabIndex = 4;
    this.label1.Text = "Каталог по умолчанию: ";
    this.lDefaultCatalog.AutoSize = true;
    this.lDefaultCatalog.Location = new Point(168, 13);
    this.lDefaultCatalog.Name = "lDefaultCatalog";
    this.lDefaultCatalog.Size = new Size(43, 13);
    this.lDefaultCatalog.TabIndex = 5;
    this.lDefaultCatalog.Text = "______";
    this.chName.Text = "Наименование каталога";
    this.chName.Width = 339;
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Location = new Point(471, 223);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(75, 23);
    this.btnOk.TabIndex = 6;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.groupBox1.Controls.Add((Control) this.lvCatalogs);
    this.groupBox1.Controls.Add((Control) this.btnDelete);
    this.groupBox1.Location = new Point(14, 37);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(613, 180);
    this.groupBox1.TabIndex = 7;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = " Список каталогов, которые имеют настройку синхронизации с IPS ";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(639, 254);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.lDefaultCatalog);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(250, 250);
    this.Name = nameof (LdapConfigsPurgeForm);
    this.Text = "Управление настройками синхронизации со службами каталогов";
    this.Load += new EventHandler(this.LdapConfigsPurgeForm_Load);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
