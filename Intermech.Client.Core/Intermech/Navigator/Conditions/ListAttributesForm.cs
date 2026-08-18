
// Type: Intermech.Navigator.Conditions.ListAttributesForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class ListAttributesForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bCancel;
  private Button bOK;
  private ListView listView1;
  private ColumnHeader columnHeader1;

  public ListAttributesForm()
  {
    this.InitializeComponent();
    this.listView1.SmallImageList = ServicesManager.GetService<ICategoryTypeIconService>().ImageList;
  }

  public ConditionAttributeInfo SelectedAttribute
  {
    get
    {
      return this.listView1.SelectedItems.Count > 0 ? (ConditionAttributeInfo) this.listView1.SelectedItems[0].Tag : (ConditionAttributeInfo) null;
    }
  }

  public void InitializeData(List<ConditionAttributeInfo> attributes)
  {
    this.listView1.Items.Clear();
    ICategoryTypeIconService service = ServicesManager.GetService<ICategoryTypeIconService>();
    attributes.Sort((Comparison<ConditionAttributeInfo>) ((x, y) => string.Compare(x.Name, y.Name)));
    foreach (ConditionAttributeInfo attribute in attributes)
    {
      ListViewItem listViewItem = new ListViewItem(attribute.Name);
      this.listView1.Items.Add(listViewItem);
      listViewItem.ImageIndex = service.IndexOf(3, -1, (object) attribute.FieldType);
      listViewItem.Tag = (object) attribute;
    }
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
    this.bCancel = new Button();
    this.bOK = new Button();
    this.listView1 = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader1.Text = "Допустимые атрибуты";
    this.SuspendLayout();
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(222, 358);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 0;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(95, 358);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.listView1.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.listView1.Location = new Point(12, 12);
    this.listView1.MultiSelect = false;
    this.listView1.Name = "listView1";
    this.listView1.Size = new Size(331, 340);
    this.listView1.TabIndex = 2;
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.Details;
    this.columnHeader1.Width = this.listView1.ClientRectangle.Width - 17;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(355, 403);
    this.Controls.Add((Control) this.listView1);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MinimumSize = new Size(300, 240 /*0xF0*/);
    this.Name = nameof (ListAttributesForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор атрибута";
    this.ResumeLayout(false);
  }
}
