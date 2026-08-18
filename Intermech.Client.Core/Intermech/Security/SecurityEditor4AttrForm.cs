
// Type: Intermech.Security.SecurityEditor4AttrForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Security;

/// <summary>
/// Редактор безопасности на атрибуты (обычно применительно к сочетанию, напр. на шаг ЖЦ + тип объекта )
/// </summary>
public class SecurityEditor4AttrForm : Form
{
  private bool isReadonly;
  private bool modified;
  private List<int> attrIdArray;
  private ISecurityCallback securityCallback;
  private ListViewItem lviLast;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SecurityControl securityControl;
  private SplitContainer splitContainer;
  private ListView listView;
  private Panel panel;
  private Button btnClose;
  private Button btnApply;
  private ColumnHeader attrColumnHeader;

  public SecurityEditor4AttrForm() => this.InitializeComponent();

  public void Execute(List<int> attrIdArray, ISecurityCallback securityCallback, bool lReadonly)
  {
    this.isReadonly = lReadonly;
    this.securityControl.Readonly = this.isReadonly;
    this.attrIdArray = attrIdArray;
    this.securityCallback = securityCallback;
    this.FillListView(attrIdArray);
    this.modified = false;
    this.UpdateControls();
    int num = (int) this.ShowDialog();
  }

  private void FillListView(List<int> attrIdList)
  {
    this.listView.BeginUpdate();
    try
    {
      this.lviLast = (ListViewItem) null;
      this.listView.Items.Clear();
      this.listView.SmallImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
      for (int index = 0; index < attrIdList.Count; ++index)
      {
        IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attrIdList[index]);
        ListViewItem lvi = this.listView.Items.Add(attributeType.Name);
        lvi.Tag = (object) attrIdList[index];
        this.SetIcon(lvi, attributeType.AttributeType);
      }
    }
    finally
    {
      this.listView.EndUpdate();
    }
  }

  private void SetIcon(ListViewItem lvi, FieldTypes fieldType)
  {
    int num = Statics.IconSrv.IndexOf(3, -1, (object) fieldType);
    lvi.ImageIndex = num;
  }

  private void UpdateControls()
  {
    this.securityControl.Visible = this.listView.SelectedIndices.Count != 0;
    this.btnApply.Enabled = this.modified && !this.isReadonly;
  }

  private void btnApply_Click(object sender, EventArgs e) => this.Apply();

  private void btnClose_Click(object sender, EventArgs e)
  {
    if (!this.modified)
      return;
    switch (IMMessageBox.Show("Подтверждение выхода", "Сохранить изменения?", MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
    {
      case DialogResult.Cancel:
        this.DialogResult = DialogResult.None;
        break;
      case DialogResult.Yes:
        this.Apply();
        break;
    }
  }

  private void Apply()
  {
    if (!this.modified)
      return;
    if (this.isReadonly)
      throw new Exception("Сохранение в режиме чтения запрещено");
    if (!this.securityControl.SaveSecurity())
      throw new Exception("Ошибка при сохранении настроек безопасности");
    this.modified = false;
    this.UpdateControls();
  }

  private void SecurityEditor4AttrForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this.attrColumnHeader.Width = -2;
  }

  private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
  {
    if (e.IsSelected)
    {
      this.securityControl.LoadSecurity(new object[1]
      {
        e.Item.Tag
      }, this.securityCallback);
      this.modified = false;
      this.UpdateControls();
    }
    else
    {
      if (!this.modified || IMMessageBox.Show("Запрос", "Сохранить изменения?", MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
        return;
      this.Apply();
    }
  }

  private void securityControl_SecurityChanged(object sender, EventArgs e)
  {
    this.modified = true;
    this.UpdateControls();
  }

  private void SecurityEditor4AttrForm_FormClosed(object sender, FormClosedEventArgs e)
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
    this.securityControl = new SecurityControl();
    this.splitContainer = new SplitContainer();
    this.listView = new ListView();
    this.attrColumnHeader = new ColumnHeader();
    this.panel = new Panel();
    this.btnClose = new Button();
    this.btnApply = new Button();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.panel.SuspendLayout();
    this.SuspendLayout();
    this.securityControl.Dock = DockStyle.Fill;
    this.securityControl.FocusedUserId = (object) null;
    this.securityControl.Location = new Point(0, 0);
    this.securityControl.Name = "securityControl";
    this.securityControl.Readonly = false;
    this.securityControl.Size = new Size(590, 455);
    this.securityControl.TabIndex = 0;
    this.securityControl.SecurityChanged += new SecurityControl.SecurityChangedEventHandler(this.securityControl_SecurityChanged);
    this.splitContainer.Dock = DockStyle.Fill;
    this.splitContainer.Location = new Point(0, 0);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.listView);
    this.splitContainer.Panel1MinSize = 200;
    this.splitContainer.Panel2.Controls.Add((Control) this.securityControl);
    this.splitContainer.Panel2MinSize = 250;
    this.splitContainer.Size = new Size(907, 455);
    this.splitContainer.SplitterDistance = 313;
    this.splitContainer.TabIndex = 1;
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.attrColumnHeader
    });
    this.listView.Dock = DockStyle.Fill;
    this.listView.FullRowSelect = true;
    this.listView.HideSelection = false;
    this.listView.Location = new Point(0, 0);
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Size = new Size(313, 455);
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.TabIndex = 0;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
    this.attrColumnHeader.Text = "Атрибут";
    this.attrColumnHeader.Width = 250;
    this.panel.Controls.Add((Control) this.btnClose);
    this.panel.Controls.Add((Control) this.btnApply);
    this.panel.Dock = DockStyle.Bottom;
    this.panel.Location = new Point(0, 415);
    this.panel.Name = "panel";
    this.panel.Size = new Size(907, 40);
    this.panel.TabIndex = 2;
    this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnClose.DialogResult = DialogResult.OK;
    this.btnClose.Location = new Point(795, 8);
    this.btnClose.Name = "btnClose";
    this.btnClose.Size = new Size(100, 23);
    this.btnClose.TabIndex = 1;
    this.btnClose.Text = "Закрыть";
    this.btnClose.UseVisualStyleBackColor = true;
    this.btnClose.Click += new EventHandler(this.btnClose_Click);
    this.btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnApply.Location = new Point(685, 8);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(100, 23);
    this.btnApply.TabIndex = 0;
    this.btnApply.Text = "Применить";
    this.btnApply.UseVisualStyleBackColor = true;
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(907, 455);
    this.Controls.Add((Control) this.panel);
    this.Controls.Add((Control) this.splitContainer);
    this.Name = nameof (SecurityEditor4AttrForm);
    this.Text = "Настройки безопасности атрибутов";
    this.FormClosed += new FormClosedEventHandler(this.SecurityEditor4AttrForm_FormClosed);
    this.Load += new EventHandler(this.SecurityEditor4AttrForm_Load);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.panel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
