
// Type: Intermech.Client.Core.FormDesigner.Actions.ContextCommand.ContextCommandSelectMethodWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Actions.ContextCommand;

/// <summary>
/// 
/// </summary>
public class ContextCommandSelectMethodWindow : Form
{
  /// <summary>сервис для хранение иконок</summary>
  private ICategoryTypeIconService _objtypesIcons;
  /// <summary>сервис для хранения именованных иконок</summary>
  private INamedImageList _images;
  /// <summary>
  /// 
  /// </summary>
  private int _emptyIndex = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOK;
  private Button btnCancel;
  private RepositoryItemPictureEdit repositoryItemPictureEdit4;
  private RepositoryItemPictureEdit repositoryItemPictureEdit3;
  private ImageList imageList1;
  private ToolTip ttBattonBar;
  private TreeList treeListCommands;
  private TreeListColumn columnCaption;
  private TreeListColumn columnCommandName2;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeData()
  {
    this._images = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, true);
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._emptyIndex = this._images.ImageIndex("imgEmpty");
  }

  /// <summary>
  /// 
  /// </summary>
  private void ChangeButtonsEnable()
  {
    TreeListNode focusedNode = this.treeListCommands.FocusedNode;
    if (focusedNode == null)
      this.btnOK.Enabled = false;
    this.btnOK.Enabled = focusedNode.Tag is AdjustableMenuCommand && focusedNode.Nodes.Count == 0;
  }

  /// <summary>Загружаем информацию о коммандах</summary>
  private void LoadCommandData()
  {
    this.treeListCommands.ClearNodes();
    this.treeListCommands.BeginSort();
    foreach (AdjustableMenuCommand childCommand in (List<AdjustableMenuCommand>) AdjustableMenusHelper.BuildFromMenuTemplate(ServiceUtils.GetService<IFactory>((object) ServicesManager.ServiceContainer, true).ContextMenuTemplate))
      this.LoadMenuTemplate(this.treeListCommands, childCommand, (TreeListNode) null);
    this.treeListCommands.EndSort();
    this.treeListCommands.FocusedNode = this.treeListCommands.Nodes.FirstNode;
    this.ChangeButtonsEnable();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="currentTreeList"></param>
  /// <param name="childCommand"></param>
  /// <param name="root"></param>
  private void LoadMenuTemplate(
    TreeList currentTreeList,
    AdjustableMenuCommand childCommand,
    TreeListNode root)
  {
    TreeListNode root1 = currentTreeList.AppendNode((object) new object[2]
    {
      (object) childCommand.Caption,
      (object) childCommand.Command
    }, root);
    root1.Tag = (object) childCommand;
    for (int index = 0; index < childCommand.Items.Count; ++index)
      this.LoadMenuTemplate(currentTreeList, childCommand.Items[index], root1);
  }

  /// <summary>
  /// 
  /// </summary>
  public ContextCommandSelectMethodWindow()
  {
    this.InitializeComponent();
    this.InitializeData();
    this.LoadCommandData();
  }

  /// <summary>
  /// 
  /// </summary>
  public string SelectedCommand
  {
    get
    {
      AdjustableMenuCommand tag = this.treeListCommands.FocusedNode != null ? this.treeListCommands.FocusedNode.Tag as AdjustableMenuCommand : (AdjustableMenuCommand) null;
      return tag == null ? string.Empty : tag.Command;
    }
    set
    {
      if (value == string.Empty)
        return;
      Func<TreeListNodes, TreeListNode> findCommandCode = (Func<TreeListNodes, TreeListNode>) null;
      findCommandCode = (Func<TreeListNodes, TreeListNode>) (nodes =>
      {
        TreeListNode treeListNode = (TreeListNode) null;
        foreach (TreeListNode node in nodes)
        {
          if (node.Tag is AdjustableMenuCommand tag2 && tag2.Command == value)
          {
            treeListNode = node;
            break;
          }
          treeListNode = findCommandCode(node.Nodes);
          if (treeListNode != null)
            break;
        }
        return treeListNode;
      });
      this.treeListCommands.FocusedNode = findCommandCode(this.treeListCommands.Nodes);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOK_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeListCommands_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.ChangeButtonsEnable();
  }

  /// <summary>для всех команд в системе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeListCommands_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
  {
    if (e.Column.FieldName != this.columnCaption.FieldName || !(e.Node.Tag is AdjustableMenuCommand tag))
      return;
    Image image = this.imageList1.Images[8];
    if (tag.ImageListSource == ImageListSource.CategoryImageList)
    {
      if (tag.ImageIndex != -1)
        image = this._objtypesIcons.ImageList.Images[tag.ImageIndex];
    }
    else
      image = tag.ImageIndex != -1 ? this._images.ImageList.Images[tag.ImageIndex] : this._images.ImageList.Images[this._emptyIndex];
    this.DrawIcon(image, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="image"></param>
  /// <param name="e"></param>
  private void DrawIcon(Image image, CustomDrawNodeCellEventArgs e)
  {
    e.Graphics.FillRectangle(e.Style.BackBrush, e.Bounds);
    Rectangle rect;
    ref Rectangle local = ref rect;
    Rectangle bounds1 = e.Bounds;
    int x = bounds1.X + 2;
    bounds1 = e.Bounds;
    int y = bounds1.Y + 2;
    int width = image.Width;
    int height = image.Height;
    local = new Rectangle(x, y, width, height);
    e.Graphics.DrawImageUnscaled(image, rect);
    Rectangle bounds2 = e.Bounds;
    bounds2.X += image.Width + 4;
    bounds2.Width -= image.Width + 4;
    e.Graphics.DrawString(e.CellText, e.Style.Font, e.Style.ForeBrush, (RectangleF) bounds2, e.Style.StrFormat);
    e.Handled = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectButtonsWindow_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectButtonsWindow_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContextCommandSelectMethodWindow));
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.repositoryItemPictureEdit4 = new RepositoryItemPictureEdit();
    this.repositoryItemPictureEdit3 = new RepositoryItemPictureEdit();
    this.imageList1 = new ImageList(this.components);
    this.ttBattonBar = new ToolTip(this.components);
    this.treeListCommands = new TreeList();
    this.columnCaption = new TreeListColumn();
    this.columnCommandName2 = new TreeListColumn();
    this.repositoryItemPictureEdit4.BeginInit();
    this.repositoryItemPictureEdit3.BeginInit();
    this.treeListCommands.BeginInit();
    this.SuspendLayout();
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(101, 448);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(228, 448);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 2;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.repositoryItemPictureEdit4.Name = "repositoryItemPictureEdit4";
    this.repositoryItemPictureEdit4.PictureAlignment = ContentAlignment.MiddleLeft;
    this.repositoryItemPictureEdit4.ReadOnly = true;
    this.repositoryItemPictureEdit4.ShowMenu = false;
    this.repositoryItemPictureEdit4.SizeMode = PictureSizeMode.Clip;
    this.repositoryItemPictureEdit3.Name = "repositoryItemPictureEdit3";
    this.repositoryItemPictureEdit3.PictureAlignment = ContentAlignment.MiddleLeft;
    this.repositoryItemPictureEdit3.ReadOnly = true;
    this.repositoryItemPictureEdit3.ShowMenu = false;
    this.repositoryItemPictureEdit3.SizeMode = PictureSizeMode.Clip;
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "arrow_all_left_blue.ico");
    this.imageList1.Images.SetKeyName(1, "arrow_all_right_blue.ico");
    this.imageList1.Images.SetKeyName(2, "arrow_bottom_blue.ico");
    this.imageList1.Images.SetKeyName(3, "arrow_down_blue.ico");
    this.imageList1.Images.SetKeyName(4, "arrow_left_blue.ico");
    this.imageList1.Images.SetKeyName(5, "arrow_right_blue.ico");
    this.imageList1.Images.SetKeyName(6, "arrow_top_blue.ico");
    this.imageList1.Images.SetKeyName(7, "arrow_up_blue.ico");
    this.imageList1.Images.SetKeyName(8, "EmptyIcon.ico");
    this.imageList1.Images.SetKeyName(9, "Checkbox_checked.ico");
    this.treeListCommands.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.treeListCommands.BehaviorOptions = BehaviorOptionsFlags.Editable | BehaviorOptionsFlags.MoveOnEdit | BehaviorOptionsFlags.PopulateServiceColumns | BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.ResizeNodes | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoNodeHeight | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.treeListCommands.Columns.AddRange(new TreeListColumn[2]
    {
      this.columnCaption,
      this.columnCommandName2
    });
    this.treeListCommands.KeyFieldName = "columnCommandName2";
    this.treeListCommands.Location = new Point(1, 0);
    this.treeListCommands.MinimumSize = new Size(50, 0);
    this.treeListCommands.Name = "treeListCommands";
    this.treeListCommands.Size = new Size(359, 439);
    this.treeListCommands.TabIndex = 5;
    this.treeListCommands.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowButtons | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowHorzLines | ViewOptionsFlags.ShowRoot | ViewOptionsFlags.ShowVertLines | ViewOptionsFlags.ShowFocusedFrame;
    this.treeListCommands.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeListCommands_FocusedNodeChanged);
    this.treeListCommands.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this.treeListCommands_CustomDrawNodeCell);
    this.columnCaption.FieldName = "columnCaption";
    this.columnCaption.Name = "columnCaption";
    this.columnCaption.Options = ColumnOptions.CanSorted | ColumnOptions.ReadOnly;
    this.columnCaption.SortOrder = SortOrder.Ascending;
    this.columnCaption.VisibleIndex = 0;
    this.columnCaption.Width = 300;
    this.columnCommandName2.Caption = "columnCommandName2";
    this.columnCommandName2.FieldName = "columnCommandName2";
    this.columnCommandName2.Name = "columnCommandName2";
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(361, 482);
    this.Controls.Add((Control) this.treeListCommands);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.btnCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(300, 300);
    this.Name = nameof (ContextCommandSelectMethodWindow);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выберите команду";
    this.FormClosed += new FormClosedEventHandler(this.SelectButtonsWindow_FormClosed);
    this.Load += new EventHandler(this.SelectButtonsWindow_Load);
    this.repositoryItemPictureEdit4.EndInit();
    this.repositoryItemPictureEdit3.EndInit();
    this.treeListCommands.EndInit();
    this.ResumeLayout(false);
  }
}
