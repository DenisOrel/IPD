// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechCardClientTreeListCustomizationView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using Intermech.Interfaces;
using Intermech.NavBars;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>
/// Контрол настройки видимости столбцов в DevExpess.TreeList (obsoleted)
/// </summary>
public class TechCardClientTreeListCustomizationView : UserControl
{
  /// <summary>Ид. типа объекта</summary>
  private int _objTypeID;
  /// <summary>
  /// 
  /// </summary>
  private TreeList _customTreeList;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal Panel pnlBottom;
  internal Panel pnlButtons;
  internal Button btnCancel;
  internal Button btnOk;
  private HeaderControl headerControl;
  private GroupBox grbColumns;
  private CheckedListBox chlbColumns;
  private ContextMenuStrip cmsColumns;
  private ToolStripMenuItem tsmiSelectItems;
  private ToolStripMenuItem tsmiClearItems;
  private ToolStripMenuItem tsmiInvertItems;

  /// <summary>Инициализация контрола</summary>
  private void InitData()
  {
    this._objTypeID = -1;
    this.CustomTreeList = (TreeList) null;
  }

  /// <summary>Заполнение данных</summary>
  private void FilltlColums()
  {
    if (this._objTypeID <= 0)
      return;
    this.chlbColumns.BeginUpdate();
    try
    {
      this.chlbColumns.Items.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this._objTypeID);
        if (objectType == null)
          return;
        List<int> intList = new List<int>();
        intList.Add(-50);
        foreach (DataRow row in (InternalDataCollectionBase) objectType.Attributes.Select("").Rows)
          intList.Add(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        foreach (int attrTypeID in intList)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
          if (attributeType != null)
          {
            int num = 0;
            if (this.CustomTreeList != null)
            {
              foreach (TreeListColumn column in (CollectionBase) this.CustomTreeList.Columns)
              {
                if (column != null && column.Tag != null && attributeType.AttributeGuid.Equals(column.Tag))
                {
                  num = 1;
                  break;
                }
              }
            }
            this.chlbColumns.Items.Add((object) new CustomizationItem(attributeType.AttributeGuid, attributeType.Name), num == 1);
          }
        }
      }
    }
    finally
    {
      this.chlbColumns.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ColumnsSelectItems()
  {
    for (int index = 0; index < this.chlbColumns.Items.Count; ++index)
      this.chlbColumns.SetItemChecked(index, true);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ColumnsClearItems()
  {
    for (int index = 0; index < this.chlbColumns.Items.Count; ++index)
      this.chlbColumns.SetItemChecked(index, false);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ColumnsInvertItems()
  {
    for (int index = 0; index < this.chlbColumns.Items.Count; ++index)
      this.chlbColumns.SetItemChecked(index, !this.chlbColumns.GetItemChecked(index));
  }

  /// <summary>Конструктор</summary>
  public TechCardClientTreeListCustomizationView()
  {
    this.InitData();
    this.InitializeComponent();
  }

  /// <summary>Идентификатор типа объекта</summary>
  public int ObjTypeID
  {
    get => this._objTypeID;
    set
    {
      if (this._objTypeID == value)
        return;
      this._objTypeID = value;
    }
  }

  /// <summary>Настраиваемый TreeList</summary>
  public TreeList CustomTreeList
  {
    get => this._customTreeList;
    set => this._customTreeList = value;
  }

  /// <summary>Загрузка параметров</summary>
  public void LoadData() => this.FilltlColums();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiSelectItems_Click(object sender, EventArgs e) => this.ColumnsSelectItems();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiClearItems_Click(object sender, EventArgs e) => this.ColumnsClearItems();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiInvertItems_Click(object sender, EventArgs e) => this.ColumnsInvertItems();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chlbColumns_SelectedValueChanged(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chlbColumns_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (e == null || e.CurrentValue != CheckState.Indeterminate)
      return;
    e.NewValue = e.CurrentValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chlbColumns_Click(object sender, EventArgs e)
  {
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.CustomTreeList == null)
      return;
    for (int index1 = 0; index1 < this.chlbColumns.Items.Count; ++index1)
    {
      CustomizationItem customizationItem = (CustomizationItem) this.chlbColumns.Items[index1];
      if (customizationItem != null)
      {
        Guid attrGuid = customizationItem.AttrGuid;
        int index2 = -1;
        for (int index3 = 0; index3 < this.CustomTreeList.Columns.Count; ++index3)
        {
          TreeListColumn column = this.CustomTreeList.Columns[index3];
          if (column != null && column.Tag != null && attrGuid.Equals(column.Tag))
          {
            index2 = index3;
            break;
          }
        }
        if (this.chlbColumns.CheckedItems.IndexOf((object) customizationItem) != -1)
        {
          if (index2 == -1)
          {
            TreeListColumn treeListColumn = this.CustomTreeList.Columns.Add(customizationItem.AttrName);
            treeListColumn.Caption = customizationItem.AttrName;
            treeListColumn.Options &= ~ColumnOptions.CanSorted;
            treeListColumn.VisibleIndex = treeListColumn.AbsoluteIndex;
            treeListColumn.Tag = (object) customizationItem.AttrGuid;
          }
        }
        else if (index2 != -1)
          this.CustomTreeList.Columns.Remove(this.CustomTreeList.Columns[index2]);
      }
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechCardClientTreeListCustomizationView));
    this.pnlBottom = new Panel();
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.headerControl = new HeaderControl();
    this.grbColumns = new GroupBox();
    this.chlbColumns = new CheckedListBox();
    this.cmsColumns = new ContextMenuStrip(this.components);
    this.tsmiSelectItems = new ToolStripMenuItem();
    this.tsmiClearItems = new ToolStripMenuItem();
    this.tsmiInvertItems = new ToolStripMenuItem();
    this.pnlBottom.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.grbColumns.SuspendLayout();
    this.cmsColumns.SuspendLayout();
    this.SuspendLayout();
    this.pnlBottom.Controls.Add((Control) this.pnlButtons);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnOk);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.headerControl, "headerControl");
    this.headerControl.HeaderFont = new Font("Tahoma", 12f, FontStyle.Bold);
    this.headerControl.Name = "headerControl";
    this.grbColumns.Controls.Add((Control) this.chlbColumns);
    componentResourceManager.ApplyResources((object) this.grbColumns, "grbColumns");
    this.grbColumns.Name = "grbColumns";
    this.grbColumns.TabStop = false;
    this.chlbColumns.CheckOnClick = true;
    this.chlbColumns.ContextMenuStrip = this.cmsColumns;
    componentResourceManager.ApplyResources((object) this.chlbColumns, "chlbColumns");
    this.chlbColumns.FormattingEnabled = true;
    this.chlbColumns.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("chlbColumns.Items")
    });
    this.chlbColumns.Name = "chlbColumns";
    this.chlbColumns.ItemCheck += new ItemCheckEventHandler(this.chlbColumns_ItemCheck);
    this.chlbColumns.SelectedValueChanged += new EventHandler(this.chlbColumns_SelectedValueChanged);
    this.chlbColumns.Click += new EventHandler(this.chlbColumns_Click);
    this.cmsColumns.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.tsmiSelectItems,
      (ToolStripItem) this.tsmiClearItems,
      (ToolStripItem) this.tsmiInvertItems
    });
    this.cmsColumns.Name = "cmsColumns";
    componentResourceManager.ApplyResources((object) this.cmsColumns, "cmsColumns");
    this.tsmiSelectItems.Name = "tsmiSelectItems";
    componentResourceManager.ApplyResources((object) this.tsmiSelectItems, "tsmiSelectItems");
    this.tsmiSelectItems.Click += new EventHandler(this.tsmiSelectItems_Click);
    this.tsmiClearItems.Name = "tsmiClearItems";
    componentResourceManager.ApplyResources((object) this.tsmiClearItems, "tsmiClearItems");
    this.tsmiClearItems.Click += new EventHandler(this.tsmiClearItems_Click);
    this.tsmiInvertItems.Name = "tsmiInvertItems";
    componentResourceManager.ApplyResources((object) this.tsmiInvertItems, "tsmiInvertItems");
    this.tsmiInvertItems.Click += new EventHandler(this.tsmiInvertItems_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.grbColumns);
    this.Controls.Add((Control) this.headerControl);
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (TechCardClientTreeListCustomizationView);
    this.Tag = (object) "";
    this.pnlBottom.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.grbColumns.ResumeLayout(false);
    this.cmsColumns.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
