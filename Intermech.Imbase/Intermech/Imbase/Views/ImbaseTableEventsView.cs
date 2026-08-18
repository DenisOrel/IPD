// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseTableEventsView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.EventLog;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImbaseTableEventsView : LinkedEventsView
{
  private IContainer components;

  public override string Caption => "Действия над таблицей IMBASE";

  public override ConditionStructure[] Conditions
  {
    get
    {
      ConditionStructure[] conditionStructureArray = (ConditionStructure[]) null;
      if (this._parentNode != null && this._parentNode.GetData(this._nodeID, typeof (IDBObjectID)) is IDBObjectID data)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          long tableReference = TableLoadHelper.GetTableReference(sessionKeeper.Session, data.Value);
          if (tableReference != 0L)
            conditionStructureArray = new ConditionStructure[1]
            {
              new ConditionStructure(-2, RelationalOperators.Equal, (object) Math.Abs(tableReference), LogicalOperators.NONE, 0, false)
            };
        }
      }
      return conditionStructureArray ?? new ConditionStructure[0];
    }
  }

  public override HybridDictionary ConditionTags
  {
    get
    {
      HybridDictionary conditionTags = (HybridDictionary) null;
      if (this._parentNode != null && this._parentNode.GetData(this._nodeID, typeof (IDBObjectID)) is IDBObjectID data)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          long tableReference = TableLoadHelper.GetTableReference(sessionKeeper.Session, data.Value);
          if (tableReference != 0L)
          {
            conditionTags = new HybridDictionary(1);
            conditionTags[(object) Intermech.Navigator.EventLog.Consts.ObjectVersionID] = (object) Math.Abs(tableReference);
          }
        }
      }
      return conditionTags;
    }
  }

  public override string StateStreamPrefix
  {
    get => "ImbaseTableEventsView_{524C6DFB-05A4-4FB8-A9D6-D10DD97EE009}";
  }

  protected override bool UseInheritedNavViews
  {
    [DebuggerStepThrough] get => false;
    set => base.UseInheritedNavViews = false;
  }

  public override int OrderID => 65;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseTableEventsView));
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "ViewsManager");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ImbaseTableEventsView);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
