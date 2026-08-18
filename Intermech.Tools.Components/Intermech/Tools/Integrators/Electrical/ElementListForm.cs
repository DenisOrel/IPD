// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElementListForm
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal class ElementListForm : Form
{
  private bool _manualChanged;
  private bool _autoChange;
  private int _listElementType = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private TextBox tbDesignation;
  private TextBox tbName;
  private Label label2;
  private GroupBox groupBox1;
  private Button bOK;
  private Button bCancel;
  private ListView listView1;
  private ColumnHeader columnHeader1;
  private Button bSelectAll;

  public ElementListForm(int listElementType)
  {
    this.InitializeComponent();
    this._listElementType = listElementType;
  }

  /// <summary>
  /// список исполнений: ид.версии, заголовок, обозначение, наименование
  /// </summary>
  /// <param name="asms"></param>
  public void LoadData(List<Tuple<long, string, string, string>> asms)
  {
    this.listView1.Items.Clear();
    foreach (Tuple<long, string, string, string> asm in asms)
      this.listView1.Items.Add(new ListViewItem(asm.Item2)
      {
        Tag = (object) asm,
        Checked = true
      });
    this.RefreshButtons();
  }

  private void RefreshButtons()
  {
    this.bOK.Enabled = this.tbDesignation.Text.Length > 0 && this.tbName.Text.Length > 0 && this.listView1.CheckedItems.Count > 0;
  }

  private void ElementListForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void ElementListForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  internal void RefreshDesignation()
  {
    if (this._manualChanged)
      return;
    ListView.CheckedListViewItemCollection checkedItems = this.listView1.CheckedItems;
    this._autoChange = true;
    if (checkedItems.Count == 0)
    {
      this.tbDesignation.Text = "";
      this.tbName.Text = "";
    }
    else
    {
      this.tbName.Text = ((Tuple<long, string, string, string>) checkedItems[0].Tag).Item4;
      List<string> stringList = new List<string>();
      foreach (ListViewItem listViewItem in checkedItems)
        stringList.Add(((Tuple<long, string, string, string>) listViewItem.Tag).Item3);
      stringList.Sort();
      this.tbDesignation.Text = DocumentDesignationHelper.AppendDocCode(stringList[0], this._listElementType);
      this._autoChange = false;
    }
  }

  private void tbDesignation_TextChanged(object sender, EventArgs e)
  {
    this.RefreshButtons();
    if (this._autoChange)
      return;
    this._manualChanged = true;
  }

  public CreatedElementList ElementList
  {
    get
    {
      CreatedElementList elementList = new CreatedElementList(this.tbDesignation.Text, this.tbName.Text);
      elementList.Type = this._listElementType;
      foreach (ListViewItem checkedItem in this.listView1.CheckedItems)
        elementList.Assemblies.Add((Tuple<long, string, string, string>) checkedItem.Tag);
      return elementList;
    }
  }

  private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    this.RefreshDesignation();
    this.RefreshButtons();
  }

  private void bSelectAll_Click(object sender, EventArgs e)
  {
    this.CheckItems(this.listView1.CheckedItems.Count != this.listView1.Items.Count);
  }

  private void CheckItems(bool check)
  {
    foreach (ListViewItem listViewItem in this.listView1.Items)
      listViewItem.Checked = check;
  }

  private void ElementListForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.Control || e.KeyCode != Keys.A || this.listView1.CheckedItems.Count == this.listView1.Items.Count)
      return;
    this.CheckItems(true);
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
    this.label1 = new Label();
    this.tbDesignation = new TextBox();
    this.tbName = new TextBox();
    this.label2 = new Label();
    this.groupBox1 = new GroupBox();
    this.listView1 = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.bSelectAll = new Button();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    this.label1.AutoSize = true;
    this.label1.Location = new Point(30, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(74, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Обозначение";
    this.tbDesignation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbDesignation.Location = new Point(33, 25);
    this.tbDesignation.Name = "tbDesignation";
    this.tbDesignation.Size = new Size(419, 20);
    this.tbDesignation.TabIndex = 0;
    this.tbDesignation.TextChanged += new EventHandler(this.tbDesignation_TextChanged);
    this.tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbName.Location = new Point(33, 69);
    this.tbName.Name = "tbName";
    this.tbName.Size = new Size(419, 20);
    this.tbName.TabIndex = 1;
    this.tbName.TextChanged += new EventHandler(this.tbDesignation_TextChanged);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(30, 53);
    this.label2.Name = "label2";
    this.label2.Size = new Size(83, 13);
    this.label2.TabIndex = 2;
    this.label2.Text = "Наименование";
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.listView1);
    this.groupBox1.Location = new Point(33, 95);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(422, 244);
    this.groupBox1.TabIndex = 4;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Исполнения";
    this.listView1.CheckBoxes = true;
    this.listView1.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    this.listView1.Dock = DockStyle.Fill;
    this.listView1.Location = new Point(3, 16 /*0x10*/);
    this.listView1.Name = "listView1";
    this.listView1.Size = new Size(416, 225);
    this.listView1.TabIndex = 0;
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.Details;
    this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
    this.columnHeader1.Text = "Заголовок";
    this.columnHeader1.Width = 463;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(204, 355);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 3;
    this.bOK.Text = "Создать";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(331, 355);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 4;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bSelectAll.Location = new Point(36, 455);
    this.bSelectAll.Name = "bSelectAll";
    this.bSelectAll.Size = new Size(121, 27);
    this.bSelectAll.TabIndex = 5;
    this.bSelectAll.Text = "Снять/Выделить";
    this.bSelectAll.UseVisualStyleBackColor = true;
    this.bSelectAll.Click += new EventHandler(this.bSelectAll_Click);
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(484, 401);
    this.Controls.Add((Control) this.bSelectAll);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.tbName);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.tbDesignation);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.MinimumSize = new Size(500, 290);
    this.Name = nameof (ElementListForm);
    this.Text = "Новый перечень элементов";
    this.FormClosing += new FormClosingEventHandler(this.ElementListForm_FormClosing);
    this.Load += new EventHandler(this.ElementListForm_Load);
    this.KeyDown += new KeyEventHandler(this.ElementListForm_KeyDown);
    this.groupBox1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
