// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.AttributeChoosingCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class AttributeChoosingCntrl : UserControl
{
  private int _attrID;
  private bool _isChanged;
  private IContainer components;
  private GroupBox gbAttr;
  private Button btnChooseAttr;
  private TextBox tbAttrName;

  public int AttrID
  {
    get => this._attrID;
    set
    {
      this._attrID = value;
      this.UpdateControl();
    }
  }

  public event EventHandler Modified;

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  public AttributeChoosingCntrl() => this.InitializeComponent();

  private void btnChooseAttr_Click(object sender, EventArgs e)
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false);
    attributesSelectDlg.AllowedAttrsTypesFilter.Add(FieldTypes.ftObjectLink);
    attributesSelectDlg.AllowedAttributesSourceTypes = AllowedAttrsSourceTypesEnum.Objects;
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count <= 0)
      return;
    this.AttrID = attributesSelectDlg.SelectedAttributesID[0];
    this.IsChanged = true;
  }

  private void UpdateControl()
  {
    if (this.AttrID == 0)
      return;
    this.tbAttrName.Text = MetaDataHelper.GetAttributeTypeName(this.AttrID);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.gbAttr = new GroupBox();
    this.btnChooseAttr = new Button();
    this.tbAttrName = new TextBox();
    this.gbAttr.SuspendLayout();
    this.SuspendLayout();
    this.gbAttr.Controls.Add((Control) this.btnChooseAttr);
    this.gbAttr.Controls.Add((Control) this.tbAttrName);
    this.gbAttr.Dock = DockStyle.Fill;
    this.gbAttr.Location = new Point(0, 0);
    this.gbAttr.Name = "gbAttr";
    this.gbAttr.Size = new Size(346, 64 /*0x40*/);
    this.gbAttr.TabIndex = 0;
    this.gbAttr.TabStop = false;
    this.gbAttr.Text = "Выберите атрибут";
    this.btnChooseAttr.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnChooseAttr.Location = new Point(307, 17);
    this.btnChooseAttr.Name = "btnChooseAttr";
    this.btnChooseAttr.Size = new Size(29, 23);
    this.btnChooseAttr.TabIndex = 1;
    this.btnChooseAttr.Text = "...";
    this.btnChooseAttr.UseVisualStyleBackColor = true;
    this.btnChooseAttr.Click += new EventHandler(this.btnChooseAttr_Click);
    this.tbAttrName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbAttrName.Enabled = false;
    this.tbAttrName.Location = new Point(7, 20);
    this.tbAttrName.Name = "tbAttrName";
    this.tbAttrName.Size = new Size(288, 20);
    this.tbAttrName.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gbAttr);
    this.Name = nameof (AttributeChoosingCntrl);
    this.Size = new Size(346, 64 /*0x40*/);
    this.gbAttr.ResumeLayout(false);
    this.gbAttr.PerformLayout();
    this.ResumeLayout(false);
  }
}
