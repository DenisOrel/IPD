
// Type: Intermech.Client.Core.Forms.CheckedObjectsSummaryForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Client.Core.Forms;

public class CheckedObjectsSummaryForm : 
  IpsBaseDialog,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  private Intermech.VirtualTreeView.VirtualTreeView _treeViewChecked;
  private Column _columnObjType;
  private Column _columnCount;
  private StatusStrip statusStrip1;
  private ToolStripStatusLabel _labelTotalChecked;
  private CellEditor _cellEditorObjectsList;
  private IContainer components;

  public CheckedObjectsSummaryForm() => this.InitializeComponent();

  public CheckedObjectsSummaryForm(
    [CanBeNull] Form parentForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName)
    : base(parentForm, ownerServices, contextName)
  {
    this.InitializeComponent();
  }

  protected override void OnShown([NotNull] EventArgs e)
  {
    this._treeViewChecked.DataSource = (object) new CheckedObjectsSummaryForm.TestItems();
  }

  private void _treeViewChecked_GetCellData([CanBeNull] object sender, [NotNull] GetCellDataEventArgs e)
  {
    if (e.Row.Item == null)
      return;
    if (e.Column == this._columnObjType)
    {
      e.CellData.Value = (object) "Тип объекта";
    }
    else
    {
      if (e.Column != this._columnCount)
        return;
      e.CellData.Value = (object) "Число объектов";
      e.CellData.TypeEditor = new UITypeEditor();
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
    this._treeViewChecked = new Intermech.VirtualTreeView.VirtualTreeView();
    this._columnObjType = new Column();
    this._columnCount = new Column();
    this._cellEditorObjectsList = new CellEditor();
    this.statusStrip1 = new StatusStrip();
    this._labelTotalChecked = new ToolStripStatusLabel();
    this._treeViewChecked.BeginInit();
    this.statusStrip1.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 296);
    this._pnlDialogButtons.Size = new Size(687, 36);
    this._cancelButton.Text = "Закрыть";
    this._okButton.Enabled = false;
    this._okButton.Visible = false;
    this._bevelDialogButtons.Location = new Point(0, 294);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(687, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._treeViewChecked.AllowDrop = true;
    this._treeViewChecked.AutoFitColumns = true;
    this._treeViewChecked.Columns.Add(this._columnObjType);
    this._treeViewChecked.Columns.Add(this._columnCount);
    this._treeViewChecked.DisableHeaderContextMenu = false;
    this._treeViewChecked.Dock = DockStyle.Fill;
    this._treeViewChecked.Editors.Add(this._cellEditorObjectsList);
    this._treeViewChecked.HeaderStyle.BorderStyle = Border3DStyle.Flat;
    this._treeViewChecked.ImageList = (ImageList) null;
    this._treeViewChecked.LineStyle = LineStyle.None;
    this._treeViewChecked.Location = new Point(0, 0);
    this._treeViewChecked.Name = "_treeViewChecked";
    this._treeViewChecked.RowHeight = 23;
    this._treeViewChecked.Size = new Size(687, 294);
    this._treeViewChecked.TabIndex = 4;
    this._treeViewChecked.GetCellData += new GetCellDataHandler(this._treeViewChecked_GetCellData);
    this._columnObjType.AutoSizePolicy = ColumnAutoSizePolicy.AutoIncrease;
    this._columnObjType.Caption = "Тип объекта";
    this._columnObjType.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._columnObjType.Movable = false;
    this._columnObjType.Name = "_columnObjType";
    this._columnObjType.Resizable = false;
    this._columnObjType.Sortable = false;
    this._columnObjType.Width = 246;
    this._columnCount.AutoFitWeight = 0.0f;
    this._columnCount.Caption = "Отмечено";
    this._columnCount.CellEditor = this._cellEditorObjectsList;
    this._columnCount.Movable = false;
    this._columnCount.Name = "_columnCount";
    this._columnCount.Resizable = false;
    this._columnCount.Sortable = false;
    this._cellEditorObjectsList.Control = (Control) null;
    this._cellEditorObjectsList.DisplayMode = CellEditorDisplayMode.Always;
    this.statusStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this._labelTotalChecked
    });
    this.statusStrip1.Location = new Point(0, 272);
    this.statusStrip1.Name = "statusStrip1";
    this.statusStrip1.Size = new Size(687, 22);
    this.statusStrip1.SizingGrip = false;
    this.statusStrip1.TabIndex = 5;
    this.statusStrip1.Text = "statusStrip1";
    this._labelTotalChecked.BorderStyle = Border3DStyle.SunkenInner;
    this._labelTotalChecked.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this._labelTotalChecked.Name = "_labelTotalChecked";
    this._labelTotalChecked.Size = new Size(672, 17);
    this._labelTotalChecked.Spring = true;
    this._labelTotalChecked.Text = "Всего отмечено 0 из 0 загруженных объектов";
    this._labelTotalChecked.TextAlign = ContentAlignment.MiddleLeft;
    this.AcceptButton = (IButtonControl) null;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(687, 332);
    this.Controls.Add((Control) this.statusStrip1);
    this.Controls.Add((Control) this._treeViewChecked);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (CheckedObjectsSummaryForm);
    this.Text = "Сводка по отмеченным объектам";
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._treeViewChecked, 0);
    this.Controls.SetChildIndex((Control) this.statusStrip1, 0);
    this._treeViewChecked.EndInit();
    this.statusStrip1.ResumeLayout(false);
    this.statusStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class TestItems
  {
  }
}
