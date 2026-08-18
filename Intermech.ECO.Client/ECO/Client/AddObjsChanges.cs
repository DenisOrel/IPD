// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.AddObjsChanges
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class AddObjsChanges : Form
{
  public static AddObjsChanges.PictPair[] pairList = new AddObjsChanges.PictPair[16 /*0x10*/]
  {
    new AddObjsChanges.PictPair(1052, AddObjsChanges.PictType.Detal),
    new AddObjsChanges.PictPair(1074, AddObjsChanges.PictType.Sbor_ed),
    new AddObjsChanges.PictPair(1105, AddObjsChanges.PictType.Standard),
    new AddObjsChanges.PictPair(1017, AddObjsChanges.PictType.Izdel),
    new AddObjsChanges.PictPair(1258, AddObjsChanges.PictType.Gab_drawing),
    new AddObjsChanges.PictPair(1247, AddObjsChanges.PictType.Sbor_drawing),
    new AddObjsChanges.PictPair(1250, AddObjsChanges.PictType.Mont_drawing),
    new AddObjsChanges.PictPair(1259, AddObjsChanges.PictType.Specif),
    new AddObjsChanges.PictPair(1262, AddObjsChanges.PictType.Detal_drawing),
    new AddObjsChanges.PictPair(1202, AddObjsChanges.PictType.All_drawing),
    new AddObjsChanges.PictPair(1204, AddObjsChanges.PictType.Electro_drawing),
    new AddObjsChanges.PictPair(1187, AddObjsChanges.PictType.Drawing_model),
    new AddObjsChanges.PictPair(1160, AddObjsChanges.PictType.Sbor_model),
    new AddObjsChanges.PictPair(1567, AddObjsChanges.PictType.Standard_model),
    new AddObjsChanges.PictPair(1082, AddObjsChanges.PictType.CAD_Doc),
    new AddObjsChanges.PictPair(1000, AddObjsChanges.PictType.Docums)
  };
  private List<List<PendingLink>> chList;
  private TreeListNode draggingNode;
  private TreeListNode targetNode;
  private IContainer components;
  private Panel panel1;
  private Button btnMoveToNewChange;
  private Button btnClose;
  private TreeList tree;
  private TreeListColumn NameCol;
  private TreeListColumn Kind;
  private RepositoryItemTextEdit repositoryItemTextEdit1;
  private ImageList IL;
  private ImageList ilChecks;
  private ComboBox cbGoal;

  public AddObjsChanges() => this.InitializeComponent();

  public static AddObjsChanges.PictType GetPictType(int objType)
  {
    foreach (AddObjsChanges.PictPair pair in AddObjsChanges.pairList)
    {
      if (pair.allChilds.Contains(objType))
        return pair.pictType;
    }
    return AddObjsChanges.PictType.All_objects;
  }

  private void SetTreeView()
  {
    this.tree.Nodes.Clear();
    string str1 = LocalizationHolder.rm.GetString("ECO.Client_395");
    foreach (List<PendingLink> ch in this.chList)
    {
      if (ch.Count != 0)
      {
        TreeListNode treeListNode = this.tree.AppendNode((object) new object[2]
        {
          (object) str1,
          (object) $"({ch[0].ecoGoal.GetDescription<ECOGoal>()})"
        }, -1, 0, 0, 3);
        treeListNode.Tag = (object) ch[0].ecoGoal;
        foreach (PendingLink pendingLink in ch)
        {
          pendingLink.UpdateDesign();
          pendingLink.UpdateObjType();
          AddObjsChanges.PictType pictType = AddObjsChanges.GetPictType(pendingLink.objType);
          AddObjsChanges.CheckType hideType = (AddObjsChanges.CheckType) pendingLink.hideType;
          string str2 = MetaDataHelper.GetObjectTypeName(pendingLink.objType);
          if (pendingLink.LockMove)
            str2 = $"[{str2}]";
          this.tree.AppendNode((object) new object[2]
          {
            (object) pendingLink.design,
            (object) str2
          }, treeListNode.Id, (int) pictType, (int) pictType, (int) hideType).Tag = (object) pendingLink;
        }
      }
    }
  }

  public void Execute(List<List<PendingLink>> changeList)
  {
    this.cbGoal.SelectedIndex = 0;
    this.chList = changeList;
    this.SetTreeView();
    this.tree.FullExpand();
    if (this.tree.Nodes.Count > 0)
      this.EnableButtons(this.tree.Nodes[0]);
    int num = (int) this.ShowDialog();
  }

  private void tree_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    TreeListHitInfo hitInfo = this.tree.GetHitInfo(new Point(e.X, e.Y));
    if (hitInfo.Node == null)
      return;
    this._ToggleCheck(hitInfo.Node);
  }

  private void tree_MouseClick(object sender, MouseEventArgs e)
  {
    TreeListHitInfo hitInfo = this.tree.GetHitInfo(new Point(e.X, e.Y));
    if (hitInfo.HitInfoType != HitInfoType.StateImage)
      return;
    this._ToggleCheck(hitInfo.Node);
  }

  private void _ToggleCheck(TreeListNode node)
  {
    if (!(node.Tag is PendingLink tag))
      return;
    switch (node.StateImageIndex)
    {
      case 1:
        node.StateImageIndex = 2;
        tag.hideType = HidingType.Hidden;
        break;
      case 2:
        node.StateImageIndex = 1;
        tag.hideType = HidingType.CanBeHidden;
        break;
    }
    this.tree.FocusedNode = node;
  }

  private void btnMoveToNewChange_Click(object sender, EventArgs e)
  {
    if (this.tree.FocusedNode == null)
      return;
    ECOGoal ecoGoal = (ECOGoal) (this.cbGoal.SelectedIndex - 1);
    if (ecoGoal == ECOGoal.NoGoal)
      return;
    TreeListNode destinationNode = this.tree.AppendNode((object) new object[2]
    {
      (object) LocalizationHolder.rm.GetString("ECO.Client_395"),
      (object) ecoGoal.GetDescription<ECOGoal>()
    }, -1, 0, 0, 3);
    destinationNode.Tag = (object) ecoGoal;
    TreeListNode focusedNode = this.tree.FocusedNode;
    TreeListNode parentNode = focusedNode.ParentNode;
    this.tree.MoveNode(focusedNode, destinationNode);
    if (parentNode != null && parentNode.StateImageIndex == 3 && parentNode.Nodes.Count == 0)
      this.tree.DeleteNode(parentNode);
    ((PendingLink) focusedNode.Tag).ecoGoal = ecoGoal;
  }

  private void tree_AfterFocusNode(object sender, NodeEventArgs e) => this.EnableButtons(e.Node);

  private void EnableButtons(TreeListNode node)
  {
    this.btnMoveToNewChange.Enabled = node.StateImageIndex != 3;
  }

  private void tree_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
  {
    this.draggingNode = e.Node;
    if (this.draggingNode.StateImageIndex != 3 && this.draggingNode.Tag is PendingLink tag && !tag.LockMove)
      e.CanDrag = true;
    else
      e.CanDrag = false;
  }

  private void tree_DragOver(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    TreeListHitInfo hitInfo = this.tree.GetHitInfo(this.tree.PointToClient(new Point(e.X, e.Y)));
    if (hitInfo.HitInfoType == HitInfoType.SelectImage || hitInfo.HitInfoType == HitInfoType.StateImage)
    {
      e.Effect = DragDropEffects.None;
    }
    else
    {
      this.targetNode = hitInfo.Node;
      if (this.targetNode == null || this.targetNode == this.draggingNode || this.targetNode.HasAsParent(this.draggingNode))
        return;
      e.Effect = DragDropEffects.Move;
    }
  }

  private void tree_DragDrop(object sender, DragEventArgs e)
  {
    if (this.targetNode == null || this.draggingNode == null)
      return;
    e.Effect = DragDropEffects.None;
    TreeListNode parentNode1 = this.targetNode.ParentNode;
    TreeListNode parentNode2 = this.draggingNode.ParentNode;
    ECOGoal tag;
    if (parentNode1 == null)
    {
      this.tree.MoveNode(this.draggingNode, this.targetNode);
      tag = (ECOGoal) this.targetNode.Tag;
    }
    else
    {
      int index = parentNode1.Nodes.IndexOf(this.targetNode);
      this.tree.MoveNode(this.draggingNode, parentNode1);
      this.tree.SetNodeIndex(this.draggingNode, index);
      tag = (ECOGoal) parentNode1.Tag;
    }
    if (parentNode2 != null && parentNode2.StateImageIndex == 3 && parentNode2.Nodes.Count == 0)
      this.tree.DeleteNode(parentNode2);
    ((PendingLink) this.draggingNode.Tag).ecoGoal = tag;
  }

  private void CollectChangeList(List<List<PendingLink>> changeList)
  {
    changeList.Clear();
    for (int index1 = 0; index1 < this.tree.Nodes.Count; ++index1)
    {
      List<PendingLink> pendingLinkList = new List<PendingLink>();
      TreeListNode node = this.tree.Nodes[index1];
      for (int index2 = 0; index2 < node.Nodes.Count; ++index2)
        pendingLinkList.Add((PendingLink) node.Nodes[index2].Tag);
      changeList.Add(pendingLinkList);
    }
  }

  private void AddObjsChanges_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.CollectChangeList(this.chList);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddObjsChanges));
    this.panel1 = new Panel();
    this.cbGoal = new ComboBox();
    this.btnMoveToNewChange = new Button();
    this.btnClose = new Button();
    this.tree = new TreeList();
    this.NameCol = new TreeListColumn();
    this.Kind = new TreeListColumn();
    this.repositoryItemTextEdit1 = new RepositoryItemTextEdit();
    this.IL = new ImageList(this.components);
    this.ilChecks = new ImageList(this.components);
    this.panel1.SuspendLayout();
    this.tree.BeginInit();
    this.repositoryItemTextEdit1.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.cbGoal);
    this.panel1.Controls.Add((Control) this.btnMoveToNewChange);
    this.panel1.Controls.Add((Control) this.btnClose);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 311);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(570, 30);
    this.panel1.TabIndex = 0;
    this.cbGoal.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbGoal.FormattingEnabled = true;
    this.cbGoal.Items.AddRange(new object[6]
    {
      (object) "(Нет)",
      (object) "Изменение",
      (object) "Аннулирование",
      (object) "Литера",
      (object) "Замена",
      (object) "Создание"
    });
    this.cbGoal.Location = new Point(136, 4);
    this.cbGoal.Name = "cbGoal";
    this.cbGoal.Size = new Size(109, 21);
    this.cbGoal.TabIndex = 3;
    this.btnMoveToNewChange.Location = new Point(12, 3);
    this.btnMoveToNewChange.Name = "btnMoveToNewChange";
    this.btnMoveToNewChange.Size = new Size(118, 23);
    this.btnMoveToNewChange.TabIndex = 2;
    this.btnMoveToNewChange.Text = "В новое изменение";
    this.btnMoveToNewChange.UseVisualStyleBackColor = true;
    this.btnMoveToNewChange.Click += new EventHandler(this.btnMoveToNewChange_Click);
    this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClose.DialogResult = DialogResult.OK;
    this.btnClose.Location = new Point(483, 3);
    this.btnClose.Name = "btnClose";
    this.btnClose.Size = new Size(75, 23);
    this.btnClose.TabIndex = 0;
    this.btnClose.Text = "Закрыть";
    this.btnClose.UseVisualStyleBackColor = true;
    this.tree.AllowDrop = true;
    this.tree.BehaviorOptions = BehaviorOptionsFlags.Editable | BehaviorOptionsFlags.MoveOnEdit | BehaviorOptionsFlags.DragNodes | BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.ResizeNodes | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoNodeHeight | BehaviorOptionsFlags.CanCloneNodesOnDrop | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.tree.Columns.AddRange(new TreeListColumn[2]
    {
      this.NameCol,
      this.Kind
    });
    this.tree.Dock = DockStyle.Fill;
    this.tree.Location = new Point(0, 0);
    this.tree.MenuOptions = MenuOptionsFlags.None;
    this.tree.Name = "tree";
    this.tree.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.repositoryItemTextEdit1
    });
    this.tree.RootValue = (object) "0";
    this.tree.RowHeight = 18;
    this.tree.SelectImageList = this.IL;
    this.tree.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
    this.tree.Size = new Size(570, 311);
    this.tree.StateImageList = this.ilChecks;
    this.tree.TabIndex = 7;
    this.tree.Text = "treeList1";
    this.tree.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowButtons | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowHorzLines | ViewOptionsFlags.ShowRoot | ViewOptionsFlags.ShowVertLines | ViewOptionsFlags.ShowFocusedFrame;
    this.tree.BeforeDragNode += new BeforeDragNodeEventHandler(this.tree_BeforeDragNode);
    this.tree.AfterFocusNode += new NodeEventHandler(this.tree_AfterFocusNode);
    this.tree.DragDrop += new DragEventHandler(this.tree_DragDrop);
    this.tree.DragOver += new DragEventHandler(this.tree_DragOver);
    this.tree.MouseClick += new MouseEventHandler(this.tree_MouseClick);
    this.tree.MouseDoubleClick += new MouseEventHandler(this.tree_MouseDoubleClick);
    this.NameCol.Caption = "Заголовок";
    this.NameCol.FieldName = "Комментарий";
    this.NameCol.Name = "NameCol";
    this.NameCol.Options = ColumnOptions.CanResized | ColumnOptions.ReadOnly;
    this.NameCol.VisibleIndex = 0;
    this.NameCol.Width = 108;
    this.Kind.Caption = "Тип";
    this.Kind.FieldName = "Модификатор";
    this.Kind.Name = "Kind";
    this.Kind.Options = ColumnOptions.CanResized | ColumnOptions.ReadOnly | ColumnOptions.ShowInCustomizationForm;
    this.Kind.VisibleIndex = 1;
    this.repositoryItemTextEdit1.AutoHeight = false;
    this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "блок.png");
    this.IL.Images.SetKeyName(1, "020.png");
    this.IL.Images.SetKeyName(2, "021.png");
    this.IL.Images.SetKeyName(3, "010.png");
    this.IL.Images.SetKeyName(4, "011.png");
    this.IL.Images.SetKeyName(5, "025.png");
    this.IL.Images.SetKeyName(6, "002.png");
    this.IL.Images.SetKeyName(7, "001.png");
    this.IL.Images.SetKeyName(8, "004.png");
    this.IL.Images.SetKeyName(9, "003.png");
    this.IL.Images.SetKeyName(10, "005.png");
    this.IL.Images.SetKeyName(11, "006.png");
    this.IL.Images.SetKeyName(12, "007.png");
    this.IL.Images.SetKeyName(13, "008.png");
    this.IL.Images.SetKeyName(14, "009.png");
    this.IL.Images.SetKeyName(15, "022.png");
    this.IL.Images.SetKeyName(16 /*0x10*/, "023.png");
    this.IL.Images.SetKeyName(17, "024.png");
    this.ilChecks.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilChecks.ImageStream");
    this.ilChecks.TransparentColor = Color.Magenta;
    this.ilChecks.Images.SetKeyName(0, "cb3.png");
    this.ilChecks.Images.SetKeyName(1, "cb2.png");
    this.ilChecks.Images.SetKeyName(2, "cb1.png");
    this.ilChecks.Images.SetKeyName(3, "cb0.png");
    this.AcceptButton = (IButtonControl) this.btnClose;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(570, 341);
    this.Controls.Add((Control) this.tree);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(400, 300);
    this.Name = nameof (AddObjsChanges);
    this.Text = "Дополнительные объекты и изменения";
    this.FormClosing += new FormClosingEventHandler(this.AddObjsChanges_FormClosing);
    this.panel1.ResumeLayout(false);
    this.tree.EndInit();
    this.repositoryItemTextEdit1.EndInit();
    this.ResumeLayout(false);
  }

  public enum CheckType
  {
    ForceChecked,
    Checked,
    Unchecked,
    NoCheckBox,
  }

  public enum PictType
  {
    Folder,
    Izdel,
    Detal,
    Sbor_model,
    Standard_model,
    Docums,
    Gab_drawing,
    CAD_Doc,
    Sbor_drawing,
    Mont_drawing,
    Specif,
    Detal_drawing,
    All_drawing,
    Electro_drawing,
    Drawing_model,
    Sbor_ed,
    Standard,
    All_objects,
  }

  public struct PictPair
  {
    public int objTypeId;
    public AddObjsChanges.PictType pictType;
    public List<int> allChilds;

    public PictPair(int typeId, AddObjsChanges.PictType pt)
    {
      this.objTypeId = typeId;
      this.pictType = pt;
      this.allChilds = MetaDataHelper.GetObjectTypeChildrenIDRecursive(this.objTypeId);
    }
  }
}
