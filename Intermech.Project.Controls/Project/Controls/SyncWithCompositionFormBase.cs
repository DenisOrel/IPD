// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.SyncWithCompositionFormBase
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Infralution.Controls;
using Intermech.Bars;
using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class SyncWithCompositionFormBase : 
  ImportObjectsFormAdv,
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
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SyncWithCompositionFormBase));
    this.TreeViewControl.PanelSelectButtons.SuspendLayout();
    this._treeViewControl.SuspendLayout();
    this._panelTreeCaption.SuspendLayout();
    this._panelRight.SuspendLayout();
    this._groupBoxSettings.SuspendLayout();
    this._editMaxLevels.BeginInit();
    this._panelRightDown.SuspendLayout();
    this._panel1.SuspendLayout();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.TreeViewControl.TreeView.BeginInit();
    this.SuspendLayout();
    this._treeViewControl.AllowChangeObjects = true;
    this.TreeViewControl.BtnClearSorting.AutoToggle = AutoToggleType.Single;
    this.TreeViewControl.BtnClearSorting.CommandName = "btCancelSort";
    this.TreeViewControl.BtnClearSorting.ImageIndex = 9;
    this.TreeViewControl.BtnClearSorting.ToolTipText = "Режим ручной сортировки";
    this._treeViewControl.BtnSelectObjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this._treeViewControl.BtnSelectObjects.Location = new Point(173, 6);
    this.TreeViewControl.BtnSetupSorting.CommandName = "btSetupSorting";
    this.TreeViewControl.BtnSetupSorting.ImageIndex = 10;
    this.TreeViewControl.BtnSetupSorting.ToolTipText = "Выполнить настройку ручной сортировки";
    this.TreeViewControl.ImagesToolbar.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("SyncWithCompositionFormBase.TreeViewControl.ImagesToolbar.ImageStream");
    this.TreeViewControl.ImagesToolbar.TransparentColor = Color.Transparent;
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(0, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(1, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(2, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(3, "");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(4, "ручная_сортировка.png");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(5, "настройка_ручной_сортировки.png");
    this.TreeViewControl.ImagesToolbar.Images.SetKeyName(6, "SettingsIcons");
    this.TreeViewControl.LabelSpace.BeginGroup = true;
    this.TreeViewControl.LabelSpace.CommandName = "labelSpace";
    this.TreeViewControl.LabelSpace.Enabled = false;
    this.TreeViewControl.LabelSpace.Stretch = true;
    this.TreeViewControl.LabelSpace.Text = " ";
    this.TreeViewControl.LabelSpace.ToolTipText = " ";
    this.TreeViewControl.PanelSelectButtons.Location = new Point(0, 474);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnUncheckAll, 0);
    this.TreeViewControl.PanelSelectButtons.Controls.SetChildIndex((Control) this._treeViewControl._btnCheckAll, 0);
    this.TreeViewControl.TreeToolbar.FlipLastItem = true;
    this.TreeViewControl.TreeToolbar.FullMenus = true;
    this.TreeViewControl.TreeToolbar.Guid = new Guid("3fb71a02-4b93-44ea-84a6-db6e9ca5869f");
    this.TreeViewControl.TreeToolbar.Hidden = false;
    this.TreeViewControl.TreeToolbar.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.TreeViewControl.BtnClearSorting,
      (ToolbarItemBase) this.TreeViewControl.BtnSetupSorting,
      (ToolbarItemBase) this.TreeViewControl.LabelSpace
    });
    this.TreeViewControl.TreeToolbar.Location = new Point(0, 0);
    this.TreeViewControl.TreeToolbar.Name = "_tbTreePanel";
    this.TreeViewControl.TreeToolbar.Size = new Size(562, 24);
    this.TreeViewControl.TreeToolbar.TabIndex = 8;
    this.TreeViewControl.TreeToolbar.Text = "";
    this._bevelObjTypes.Style = BevelStyle.Lowered;
    this.bevel1.Style = BevelStyle.Lowered;
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.ClientSize = new Size(915, 637);
    this.Name = nameof (SyncWithCompositionFormBase);
    this.TreeViewControl.TreeView.BackgroundImageMode = ImageDrawMode.Tile;
    this.TreeViewControl.TreeView.BorderStyle = BorderStyle.Fixed3D;
    this.TreeViewControl.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.TreeViewControl.TreeView.RootDbObjectVersionIDs = (IReadOnlyList<long>) componentResourceManager.GetObject("_treeViewControl.TreeView.RootDbObjectVersionIDs");
    this.TreeViewControl.TreeView.RowEvenStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowOddStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowSelectedStyle.WordWrap = false;
    this.TreeViewControl.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this.TreeViewControl.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.TreeViewControl.TreeView.RowStyle.BorderWidth = 1;
    this.TreeViewControl.TreeView.RowStyle.WordWrap = false;
    this.TreeViewControl.TreeView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.TreeViewControl.PanelSelectButtons.ResumeLayout(false);
    this.TreeViewControl.PanelSelectButtons.PerformLayout();
    this._treeViewControl.ResumeLayout(false);
    this._panelTreeCaption.ResumeLayout(false);
    this._panelTreeCaption.PerformLayout();
    this._panelRight.ResumeLayout(false);
    this._groupBoxSettings.ResumeLayout(false);
    this._groupBoxSettings.PerformLayout();
    this._editMaxLevels.EndInit();
    this._panelRightDown.ResumeLayout(false);
    this._panel1.ResumeLayout(false);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.TreeViewControl.TreeView.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public SyncWithCompositionFormBase()
  {
  }

  public SyncWithCompositionFormBase([NotNull] System.IServiceProvider ownerServices, [NotNull] string contextName)
    : base(ownerServices, contextName)
  {
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected override System.Type DefaultSelectObjectsInCompositionControlType
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return typeof (SelectObjectsForSyncWithCompositionControl);
    }
  }

  /// <summary>Контрол с деревом навигатора</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public SelectObjectsForSyncWithCompositionControl TreeViewControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (SelectObjectsForSyncWithCompositionControl) this._treeViewControl;
    }
  }

  /// <summary>UI: Дерево состава объекта</summary>
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [NotNull]
  public SelectObjectsForSyncWithCompositionNavTreeView TreeView
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return ((SelectObjectsForSyncWithCompositionControlBase) this._treeViewControl).TreeView;
    }
  }
}
