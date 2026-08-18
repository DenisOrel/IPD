// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.SelectAttributeForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class SelectAttributeForm : Form
{
  private IContainer components;
  private Button btOk;
  private Button btCancel;
  private ListView _listView;

  internal static AttributeTypeProperties SelectAttribute(List<AttributeTypeProperties> attList)
  {
    using (SelectAttributeForm selectAttributeForm = new SelectAttributeForm())
    {
      selectAttributeForm.SetData(attList);
      if (selectAttributeForm.ShowDialog() == DialogResult.OK)
        return selectAttributeForm.GetData();
      return new AttributeTypeProperties()
      {
        AttributeID = 0
      };
    }
  }

  public SelectAttributeForm()
  {
    this.InitializeComponent();
    this._listView.SmallImageList = Statics.IconSrv.ImageList;
  }

  private void SetData(List<AttributeTypeProperties> attList)
  {
    int count = attList.Count;
    for (int index = 0; index < count; ++index)
    {
      int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attList[index].FieldType);
      this._listView.Items.Add(new ListViewItem(attList[index].Name, imageIndex)
      {
        Tag = (object) attList[index]
      });
    }
  }

  private AttributeTypeProperties GetData()
  {
    return this._listView.SelectedItems.Count > 0 ? (AttributeTypeProperties) this._listView.SelectedItems[0].Tag : new AttributeTypeProperties();
  }

  private void _listView_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btOk.Enabled = this._listView.SelectedItems.Count > 0;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.btOk = new Button();
    this.btCancel = new Button();
    this._listView = new ListView();
    this.SuspendLayout();
    this.btOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOk.DialogResult = DialogResult.OK;
    this.btOk.Enabled = false;
    this.btOk.Location = new Point(187, 212);
    this.btOk.Name = "btOk";
    this.btOk.Size = new Size(75, 23);
    this.btOk.TabIndex = 0;
    this.btOk.Text = "OK";
    this.btOk.UseVisualStyleBackColor = true;
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Location = new Point(268, 212);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 1;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this._listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._listView.FullRowSelect = true;
    this._listView.HideSelection = false;
    this._listView.Location = new Point(12, 12);
    this._listView.MultiSelect = false;
    this._listView.Name = "_listView";
    this._listView.Size = new Size(331, 194);
    this._listView.TabIndex = 2;
    this._listView.UseCompatibleStateImageBehavior = false;
    this._listView.View = View.List;
    this._listView.SelectedIndexChanged += new EventHandler(this._listView_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this.btOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(355, 247);
    this.Controls.Add((Control) this._listView);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOk);
    this.DoubleBuffered = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(200, 250);
    this.Name = nameof (SelectAttributeForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Выберите атрибут";
    this.ResumeLayout(false);
  }
}
