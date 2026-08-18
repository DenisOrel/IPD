
// Type: Intermech.Client.Core.VersionChoosingForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core;

/// <summary>
/// Форма выбора версии
/// При наличии архивной версии для взятого текущим пользователем на изменение объекта доступна кнопка Сравнить с архивной копией.
/// </summary>
public class VersionChoosingForm : Form
{
  private long _initialObjectId;
  private long _versionForCompareId;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ChildrenView childrenView1;
  private Panel panel1;
  private Button btnCompareWithArchiveVersion;
  private Button btnOk;
  private Button btnCancel;

  public long VersionForCompareId => this._versionForCompareId;

  public VersionChoosingForm()
  {
    this.InitializeComponent();
    this.childrenView1.DisableDoubleClicks = true;
    this.childrenView1.Grid.MouseDoubleClick += new MouseEventHandler(this.GridOnMouseDoubleClick);
  }

  public void Init(long objectId, List<long> versionsIds, System.IServiceProvider services)
  {
    this._initialObjectId = objectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject == null)
        return;
      ListDescriptor rootDescriptor = new ListDescriptor(4, dbObject.ObjectType, string.Empty, (IList) versionsIds);
      this.childrenView1.ViewContentType = ContentType.NonFolders;
      this.childrenView1.Grid.SelectionMode = iGSelectionMode.One;
      this.childrenView1.Initialize((IDescriptor) rootDescriptor, services);
      this.childrenView1.Activate((IView) null);
      if (dbObject.CheckoutBy != 0L && dbObject.CheckoutBy == sessionKeeper.Session.UserID)
        this.btnCompareWithArchiveVersion.Visible = true;
      else
        this.btnCompareWithArchiveVersion.Visible = false;
    }
  }

  /// <summary>Закрываемся</summary>
  private void PerformClosing()
  {
    if (this.childrenView1.SelectedItems.Count != 1 || !(this.childrenView1.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    this._versionForCompareId = itemData.Value;
    this.Close();
  }

  private void GridOnMouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this.childrenView1.SelectedItems.Count == 0)
      return;
    this.PerformClosing();
  }

  /// <summary>Кнопка сравнения с архивной версией</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCompareWithArchiveVersion_Click(object sender, EventArgs e)
  {
    this._versionForCompareId = Math.Abs(this._initialObjectId);
    this.Close();
  }

  private void btnOk_Click(object sender, EventArgs e) => this.PerformClosing();

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
    this.childrenView1 = (ChildrenView) new ObjectsViewBase();
    this.panel1 = new Panel();
    this.btnCompareWithArchiveVersion = new Button();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.childrenView1.AllowCustomGroupValues = true;
    this.childrenView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.childrenView1.AutoScroll = true;
    this.childrenView1.Control = (object) this.childrenView1;
    this.childrenView1.DisableKeyDownEvents = false;
    this.childrenView1.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this.childrenView1.Font = new Font("Tahoma", 8.25f);
    this.childrenView1.Location = new Point(0, 1);
    this.childrenView1.Name = "childrenView1";
    this.childrenView1.Size = new Size(647, 278);
    this.childrenView1.TabIndex = 0;
    this.childrenView1.ViewContentType = ContentType.Folders | ContentType.NonFolders;
    this.panel1.AutoScroll = true;
    this.panel1.AutoSize = true;
    this.panel1.Controls.Add((Control) this.btnCompareWithArchiveVersion);
    this.panel1.Controls.Add((Control) this.btnOk);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 282);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(650, 44);
    this.panel1.TabIndex = 1;
    this.btnCompareWithArchiveVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCompareWithArchiveVersion.Location = new Point(160 /*0xA0*/, 3);
    this.btnCompareWithArchiveVersion.Name = "btnCompareWithArchiveVersion";
    this.btnCompareWithArchiveVersion.Size = new Size(172, 31 /*0x1F*/);
    this.btnCompareWithArchiveVersion.TabIndex = 4;
    this.btnCompareWithArchiveVersion.Text = "Сравнить с архивной копией";
    this.btnCompareWithArchiveVersion.UseVisualStyleBackColor = true;
    this.btnCompareWithArchiveVersion.Click += new EventHandler(this.btnCompareWithArchiveVersion_Click);
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.Location = new Point(337, 3);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(150, 31 /*0x1F*/);
    this.btnOk.TabIndex = 3;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(492, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(149, 31 /*0x1F*/);
    this.btnCancel.TabIndex = 2;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(650, 326);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.childrenView1);
    this.MinimumSize = new Size(666, 300);
    this.Name = nameof (VersionChoosingForm);
    this.Text = "Выбор версии для сравнения";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
