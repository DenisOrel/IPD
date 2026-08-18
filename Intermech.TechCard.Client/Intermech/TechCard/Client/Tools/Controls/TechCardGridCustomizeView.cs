// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Tools.Controls.TechCardGridCustomizeView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.NavBars;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.Tools.Controls;

/// <summary>Customization control</summary>
public class TechCardGridCustomizeView : UserControl
{
  private int _objTypeID;
  private TechCardGrid _techGrid;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  internal Button btnOk;
  private GroupBox grbColumns;
  private CheckedListBox chlbColumns;
  private HeaderControl headerControl;
  internal Button btnCancel;
  internal Panel pnlBottom;
  internal Panel pnlButtons;

  /// <summary>Initialize control data</summary>
  private void InitData()
  {
    this._objTypeID = -1;
    this._techGrid = (TechCardGrid) null;
  }

  /// <summary>Fill columns</summary>
  private void FillColumns()
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
        DataTable dataTable = objectType.Attributes.Select("");
        if (dataTable != null && dataTable.Rows != null)
        {
          intList.Capacity = intList.Count + dataTable.Rows.Count;
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            intList.Add(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        }
        foreach (int attrTypeID in intList)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
          if (attributeType != null)
          {
            int num = 0;
            if (this._techGrid != null)
            {
              foreach (iGCol col in (IEnumerable) this._techGrid.Cols)
              {
                if (col.Tag != null && attributeType.AttributeGuid.Equals(col.Tag))
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

  /// <summary>Constructor</summary>
  public TechCardGridCustomizeView()
  {
    this.InitializeComponent();
    this.InitData();
  }

  /// <summary>Object type's id</summary>
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

  /// <summary>Customized techcard grid</summary>
  public TechCardGrid TechGrid
  {
    get => this._techGrid;
    set => this._techGrid = value;
  }

  /// <summary>Load columns data</summary>
  public void LoadData() => this.FillColumns();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void chlbColumns_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (e.CurrentValue != CheckState.Indeterminate)
      return;
    e.NewValue = e.CurrentValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.TechGrid == null)
      return;
    this.TechGrid.BeginUpdate();
    try
    {
      for (int index1 = 0; index1 < this.chlbColumns.Items.Count; ++index1)
      {
        CustomizationItem customizationItem = (CustomizationItem) this.chlbColumns.Items[index1];
        Guid attrGuid = customizationItem.AttrGuid;
        int colIndex = -1;
        for (int index2 = 0; index2 < this.TechGrid.Cols.Count; ++index2)
        {
          iGCol col = this.TechGrid.Cols[index2];
          if (col.Tag != null && attrGuid.Equals(col.Tag))
          {
            colIndex = index2;
            break;
          }
        }
        if (this.chlbColumns.CheckedItems.IndexOf((object) customizationItem) != -1)
        {
          if (colIndex == -1)
          {
            iGCol iGcol = this.TechGrid.Cols.Add(customizationItem.AttrName);
            iGcol.Text = (object) customizationItem.AttrName;
            iGcol.Tag = (object) customizationItem.AttrGuid;
          }
        }
        else if (colIndex != -1)
          this.TechGrid.Cols.RemoveAt(colIndex);
      }
    }
    finally
    {
      this.TechGrid.EndUpdate();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechCardGridCustomizeView));
    this.btnOk = new Button();
    this.grbColumns = new GroupBox();
    this.chlbColumns = new CheckedListBox();
    this.headerControl = new HeaderControl();
    this.btnCancel = new Button();
    this.pnlBottom = new Panel();
    this.pnlButtons = new Panel();
    this.grbColumns.SuspendLayout();
    this.pnlBottom.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.SuspendLayout();
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.grbColumns.Controls.Add((Control) this.chlbColumns);
    componentResourceManager.ApplyResources((object) this.grbColumns, "grbColumns");
    this.grbColumns.Name = "grbColumns";
    this.grbColumns.TabStop = false;
    this.chlbColumns.CheckOnClick = true;
    componentResourceManager.ApplyResources((object) this.chlbColumns, "chlbColumns");
    this.chlbColumns.FormattingEnabled = true;
    this.chlbColumns.Items.AddRange(new object[1]
    {
      (object) componentResourceManager.GetString("chlbColumns.Items")
    });
    this.chlbColumns.Name = "chlbColumns";
    this.chlbColumns.ItemCheck += new ItemCheckEventHandler(this.chlbColumns_ItemCheck);
    componentResourceManager.ApplyResources((object) this.headerControl, "headerControl");
    this.headerControl.HeaderFont = new Font("Tahoma", 12f, FontStyle.Bold);
    this.headerControl.Name = "headerControl";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.pnlBottom.Controls.Add((Control) this.pnlButtons);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnOk);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.grbColumns);
    this.Controls.Add((Control) this.headerControl);
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (TechCardGridCustomizeView);
    this.grbColumns.ResumeLayout(false);
    this.pnlBottom.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
