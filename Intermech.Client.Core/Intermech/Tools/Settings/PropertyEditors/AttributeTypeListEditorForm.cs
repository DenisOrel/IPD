
// Type: Intermech.Tools.Settings.PropertyEditors.AttributeTypeListEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class AttributeTypeListEditorForm : Form
{
  private List<GlobalId<int>> attrTypes;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btOK;
  private Button btCancel;
  private Label lbObjectTypes;
  private ListView lvAttributeTypes;
  private Button btAdd;
  private Button btRemove;
  private ColumnHeader chAttributeType;

  public AttributeTypeListEditorForm() => this.InitializeComponent();

  private void AttributeTypeListEditorForm_Shown(object sender, EventArgs e)
  {
    this.lvAttributeTypes.BeginUpdate();
    try
    {
      this.lvAttributeTypes.Items.Clear();
      this.lvAttributeTypes.SmallImageList = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, true).ImageList;
      for (int index = 0; index < this.attrTypes.Count; ++index)
        this.lvAttributeTypes.Items.Add(this.MakeListItem(this.attrTypes[index]));
      if (this.lvAttributeTypes.Items.Count <= 0)
        return;
      this.lvAttributeTypes.Items[0].Selected = true;
    }
    finally
    {
      this.lvAttributeTypes.EndUpdate();
    }
  }

  private ListViewItem MakeListItem(GlobalId<int> attrType)
  {
    return new ListViewItem(attrType.Name)
    {
      Tag = (object) attrType,
      ImageIndex = ServiceUtils.GetService<ICategoryTypeIconService>((object) ServicesManager.ServiceContainer, true).IndexOf(3, attrType.Id)
    };
  }

  private void lvAttributeTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btRemove.Enabled = this.lvAttributeTypes.SelectedIndices.Count > 0;
  }

  private void btOK_Click(object sender, EventArgs e)
  {
    List<GlobalId<int>> collection = new List<GlobalId<int>>(this.lvAttributeTypes.Items.Count);
    for (int index = 0; index < this.lvAttributeTypes.Items.Count; ++index)
      collection.Add((GlobalId<int>) this.lvAttributeTypes.Items[index].Tag);
    this.attrTypes.Clear();
    this.attrTypes.AddRange((IEnumerable<GlobalId<int>>) collection);
  }

  private void btAdd_Click(object sender, EventArgs e)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.ShowCreateAttrBtn = false;
      attributesSelectDlg.RelationGroupEnable = false;
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[4]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftPassword
      });
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count != 1)
        return;
      GlobalId<int> attributeType = this.ConvertToAttributeType(attributesSelectDlg.SelectedAttributesID[0]);
      AppAttributeTypeEventArgs e1 = new AppAttributeTypeEventArgs(attributeType, true);
      if (this.AddObject != null)
        this.AddObject((object) this, e1);
      if (!e1.CanAdd)
        return;
      ListViewItem listItem = this.FindListItem(attributeType);
      if (listItem != null)
      {
        listItem.Selected = true;
        this.lvAttributeTypes.Focus();
      }
      else
      {
        ListViewItem listViewItem = this.MakeListItem(attributeType);
        this.lvAttributeTypes.BeginUpdate();
        try
        {
          this.lvAttributeTypes.Items.Add(listViewItem);
          listViewItem.Selected = true;
          this.lvAttributeTypes.Focus();
        }
        finally
        {
          this.lvAttributeTypes.EndUpdate();
        }
      }
    }
  }

  private GlobalId<int> ConvertToAttributeType(int attrTypeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrTypeId, true);
      return new GlobalId<int>(((IDBGuid) attributeType).GUID, attrTypeId, attributeType.Name);
    }
  }

  private ListViewItem FindListItem(GlobalId<int> attrType)
  {
    for (int index = 0; index < this.lvAttributeTypes.Items.Count; ++index)
    {
      if (this.lvAttributeTypes.Items[index].Tag.Equals((object) attrType))
        return this.lvAttributeTypes.Items[index];
    }
    return (ListViewItem) null;
  }

  private void btRemove_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.lvAttributeTypes.SelectedIndices[0];
    this.lvAttributeTypes.BeginUpdate();
    try
    {
      this.lvAttributeTypes.Items.RemoveAt(selectedIndex);
      if (this.lvAttributeTypes.Items.Count > 0)
      {
        if (selectedIndex > 0)
          --selectedIndex;
        this.lvAttributeTypes.Items[selectedIndex].Selected = true;
      }
      this.lvAttributeTypes.Focus();
    }
    finally
    {
      this.lvAttributeTypes.EndUpdate();
    }
  }

  public List<GlobalId<int>> AttributeTypes
  {
    get => this.attrTypes;
    set => this.attrTypes = value;
  }

  public event EventHandler<AppAttributeTypeEventArgs> AddObject;

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
    this.btOK = new Button();
    this.btCancel = new Button();
    this.lbObjectTypes = new Label();
    this.lvAttributeTypes = new ListView();
    this.chAttributeType = new ColumnHeader();
    this.btAdd = new Button();
    this.btRemove = new Button();
    this.SuspendLayout();
    this.btOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Location = new Point(376, 233);
    this.btOK.Name = "btOK";
    this.btOK.Size = new Size(75, 23);
    this.btOK.TabIndex = 4;
    this.btOK.Text = "OK";
    this.btOK.UseVisualStyleBackColor = true;
    this.btOK.Click += new EventHandler(this.btOK_Click);
    this.btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Location = new Point(457, 233);
    this.btCancel.Name = "btCancel";
    this.btCancel.Size = new Size(75, 23);
    this.btCancel.TabIndex = 5;
    this.btCancel.Text = "Отмена";
    this.btCancel.UseVisualStyleBackColor = true;
    this.lbObjectTypes.AutoSize = true;
    this.lbObjectTypes.Location = new Point(9, 9);
    this.lbObjectTypes.Name = "lbObjectTypes";
    this.lbObjectTypes.Size = new Size(148, 13);
    this.lbObjectTypes.TabIndex = 0;
    this.lbObjectTypes.Text = "Выбранные типы атрибутов";
    this.lvAttributeTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvAttributeTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.chAttributeType
    });
    this.lvAttributeTypes.FullRowSelect = true;
    this.lvAttributeTypes.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvAttributeTypes.HideSelection = false;
    this.lvAttributeTypes.Location = new Point(12, 25);
    this.lvAttributeTypes.MultiSelect = false;
    this.lvAttributeTypes.Name = "lvAttributeTypes";
    this.lvAttributeTypes.Size = new Size(439, 202);
    this.lvAttributeTypes.TabIndex = 1;
    this.lvAttributeTypes.UseCompatibleStateImageBehavior = false;
    this.lvAttributeTypes.View = View.Details;
    this.lvAttributeTypes.SelectedIndexChanged += new EventHandler(this.lvAttributeTypes_SelectedIndexChanged);
    this.chAttributeType.Text = "Тип атрибута";
    this.chAttributeType.Width = 407;
    this.btAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btAdd.Location = new Point(457, 25);
    this.btAdd.Name = "btAdd";
    this.btAdd.Size = new Size(75, 23);
    this.btAdd.TabIndex = 2;
    this.btAdd.Text = "Добавить";
    this.btAdd.UseVisualStyleBackColor = true;
    this.btAdd.Click += new EventHandler(this.btAdd_Click);
    this.btRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btRemove.Enabled = false;
    this.btRemove.Location = new Point(457, 54);
    this.btRemove.Name = "btRemove";
    this.btRemove.Size = new Size(75, 23);
    this.btRemove.TabIndex = 3;
    this.btRemove.Text = "Удалить";
    this.btRemove.UseVisualStyleBackColor = true;
    this.btRemove.Click += new EventHandler(this.btRemove_Click);
    this.AcceptButton = (IButtonControl) this.btCancel;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.ClientSize = new Size(544, 268);
    this.Controls.Add((Control) this.btRemove);
    this.Controls.Add((Control) this.btAdd);
    this.Controls.Add((Control) this.lvAttributeTypes);
    this.Controls.Add((Control) this.lbObjectTypes);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(550, 300);
    this.Name = nameof (AttributeTypeListEditorForm);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Список типов атрибутов";
    this.Shown += new EventHandler(this.AttributeTypeListEditorForm_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
