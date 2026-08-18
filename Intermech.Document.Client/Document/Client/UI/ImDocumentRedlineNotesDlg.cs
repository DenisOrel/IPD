// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.UI.ImDocumentRedlineNotesDlg
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Redline;
using Intermech.Docking;
using Intermech.Document.UI;
using Intermech.Extensions;
using Intermech.Localization;
using Intermech.Map;
using Intermech.Redline;
using Intermech.Redline.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.UI;

public class ImDocumentRedlineNotesDlg : DockControl, ISkipTargetActivate
{
  public static Guid DockGuid = new Guid("{E630612E-4760-4290-BF61-0AC8E891C29D}");
  private ICommandManager _commandManager;
  private RedlineSplitContainer splitContainerRedObject;
  private TreeView treeView;
  private RichTextBox tBoxComment;
  private UCRedlineLayerInfo redLayerInfo;
  private Intermech.Bars.ToolBar toolBarTreeView;
  private MenuBar menuBarTreeView;
  private MenuBar popupMenuBar;
  private RedlineToolBarPresenter toolbarPresenter;
  private EStatusRemark FilterFlags = EStatusRemark.eAll;

  public event TreeViewEventHandler NodeAdded;

  public event TreeViewEventHandler NodeRenamed;

  public event TreeViewCancelEventHandler OnNodeSelecting;

  public event TreeViewEventHandler NodeSelected;

  public event EventHandler CommentTextChanged;

  public DocumentControl DocumentControl { get; set; }

  public TreeNode SelectedNode => this.treeView?.SelectedNode;

  public string Comment
  {
    get => this.tBoxComment.Text;
    set => this.tBoxComment.Text = value;
  }

  public Intermech.Bars.ToolBar NotesTreeToolbar => this.toolBarTreeView;

  public ImDocumentRedlineNotesDlg(RedlineToolBarPresenter presenter)
  {
    this.HideOnClose = true;
    this.Guid = ImDocumentRedlineNotesDlg.DockGuid;
    this.InitializeComponent();
    this.splitContainerRedObject.Paint += new PaintEventHandler(this.splitContainerRedObject_Paint);
    this.splitContainerRedObject.Resize += new EventHandler(this.splitContainerRedObject_Resize);
    this.treeView.MouseUp += new MouseEventHandler(this.treeView_MouseUp);
    this.treeView.BeforeSelect += new TreeViewCancelEventHandler(this.treeView_BeforeSelect);
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.treeView.DrawNode += new DrawTreeNodeEventHandler(this.treeView_DrawNode);
    this.treeView.AfterLabelEdit += new NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
    this.tBoxComment.TextChanged += new EventHandler(this.tBoxComment_Changed);
    this.toolbarPresenter = presenter;
    this.toolBarTreeView = this.toolbarPresenter.TreeViewToolbar;
    this.popupMenuBar = this.toolbarPresenter.TreeViewContextMenu;
    this.splitContainerRedObject.Panel1.Controls.Add((Control) this.toolBarTreeView);
    this.splitContainerRedObject.Panel1.Controls.Add((Control) this.popupMenuBar);
    this.popupMenuBar.Location = new Point(0, 0);
  }

  private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
  {
    e.Node.EndEdit(false);
    this.treeView.LabelEdit = false;
    e.Node.Text = e.Label;
    TreeViewEventHandler nodeRenamed = this.NodeRenamed;
    if (nodeRenamed == null)
      return;
    nodeRenamed(sender, new TreeViewEventArgs(e.Node));
  }

  private void tBoxComment_Changed(object sender, EventArgs e)
  {
    EventHandler commentTextChanged = this.CommentTextChanged;
    if (commentTextChanged == null)
      return;
    commentTextChanged(sender, e);
  }

  internal void UpdateTreeView(RedlineLayer redLayer, Redliner redliner)
  {
    if (redLayer == null && redliner.CurrentRedLayer != null)
    {
      redLayer = redliner.CurrentRedLayer.Identifier as RedlineLayer;
      redliner.CurrentRedLayer = (MapLayer) null;
    }
    TreeNode treeNode = this.treeView.Nodes.SearchTree((object) redLayer, (Func<object, object, bool>) ((o, o1) => o is RedlineLayer redlineLayer1 && o1 is RedlineLayer redlineLayer2 && (long) redlineLayer2.RedObjectID == (long) redlineLayer1.RedObjectID));
    if (treeNode != null)
    {
      this.treeView.SelectedNode = treeNode;
      this.treeView.Focus();
    }
    else
    {
      this.ClearBoxView();
      List<object> list = (this.treeView.SelectedNode != null ? this.treeView.SelectedNode.Nodes : this.treeView.Nodes).Collect().Select<TreeNode, object>((Func<TreeNode, object>) (element => element.Tag)).ToList<object>();
      redliner.ChangeVisibleLayers(list);
    }
    redliner.OnChanged();
  }

  internal void FillTreeView(Redliner redliner)
  {
    this.treeView.BeginUpdate();
    try
    {
      this.treeView.ImageList = this.treeView.ImageList ?? RedlineToolBarPresenter.ImageList;
      this.ClearTreeView();
      this.CreateRedLinerTree(this.treeView.Nodes, redliner);
    }
    finally
    {
      this.treeView.EndUpdate();
    }
  }

  private void CreateRedLinerTree(TreeNodeCollection redlinerNodes, Redliner redliner)
  {
    if (redlinerNodes == null || redliner == null)
      return;
    this.toolbarPresenter.UpdateTreeToolbarFilterButtons(this.FilterFlags);
    this.treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
    List<RedlineLayer> redlineLayerList = redliner.ListRedlineLayer();
    List<RedlineLayer> list = redlineLayerList.Where<RedlineLayer>((Func<RedlineLayer, bool>) (e => e.ParentID > 0UL)).ToList<RedlineLayer>();
    Dictionary<string, List<RedlineLayer>> dictionary1 = redlineLayerList.Except<RedlineLayer>((IEnumerable<RedlineLayer>) list).GroupBy<RedlineLayer, string>((Func<RedlineLayer, string>) (e => e.Signature)).ToDictionary<IGrouping<string, RedlineLayer>, string, List<RedlineLayer>>((Func<IGrouping<string, RedlineLayer>, string>) (gr => gr.Key), (Func<IGrouping<string, RedlineLayer>, List<RedlineLayer>>) (gr => gr.ToList<RedlineLayer>()));
    redlineLayerList.Clear();
    Dictionary<string, List<RedlineLayer>> dictionary2 = dictionary1.OrderBy<KeyValuePair<string, List<RedlineLayer>>, string>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key)).ToDictionary<KeyValuePair<string, List<RedlineLayer>>, string, List<RedlineLayer>>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key), (Func<KeyValuePair<string, List<RedlineLayer>>, List<RedlineLayer>>) (x => x.Value));
    for (int index1 = 0; index1 < dictionary2.Count; ++index1)
    {
      KeyValuePair<string, List<RedlineLayer>> keyValuePair1 = dictionary2.ElementAt<KeyValuePair<string, List<RedlineLayer>>>(index1);
      TreeNode node1 = new TreeNode(keyValuePair1.Key.Split('|')[0]);
      redlinerNodes.Add(node1);
      node1.ImageIndex = node1.SelectedImageIndex = RedlineToolBarPresenter.RoleImageIndex;
      Dictionary<string, List<RedlineLayer>> dictionary3 = keyValuePair1.Value.GroupBy<RedlineLayer, string>((Func<RedlineLayer, string>) (e => e.UserID)).ToDictionary<IGrouping<string, RedlineLayer>, string, List<RedlineLayer>>((Func<IGrouping<string, RedlineLayer>, string>) (gr => gr.Key), (Func<IGrouping<string, RedlineLayer>, List<RedlineLayer>>) (gr => gr.ToList<RedlineLayer>())).OrderBy<KeyValuePair<string, List<RedlineLayer>>, string>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key)).ToDictionary<KeyValuePair<string, List<RedlineLayer>>, string, List<RedlineLayer>>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key), (Func<KeyValuePair<string, List<RedlineLayer>>, List<RedlineLayer>>) (x => x.Value));
      for (int index2 = 0; index2 < dictionary3.Count; ++index2)
      {
        KeyValuePair<string, List<RedlineLayer>> keyValuePair2 = dictionary3.ElementAt<KeyValuePair<string, List<RedlineLayer>>>(index2);
        TreeNode node2 = new TreeNode(keyValuePair2.Key.Split('|')[0]);
        node2.ImageIndex = node2.SelectedImageIndex = RedlineToolBarPresenter.UserImageIndex;
        node1.Nodes.Add(node2);
        Dictionary<string, List<RedlineLayer>> dictionary4 = this.ApplyFilter(keyValuePair2.Value).GroupBy<RedlineLayer, string>((Func<RedlineLayer, string>) (e => e.NameRemark)).ToDictionary<IGrouping<string, RedlineLayer>, string, List<RedlineLayer>>((Func<IGrouping<string, RedlineLayer>, string>) (gr => gr.Key), (Func<IGrouping<string, RedlineLayer>, List<RedlineLayer>>) (gr => gr.ToList<RedlineLayer>())).OrderBy<KeyValuePair<string, List<RedlineLayer>>, string>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key)).ToDictionary<KeyValuePair<string, List<RedlineLayer>>, string, List<RedlineLayer>>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key), (Func<KeyValuePair<string, List<RedlineLayer>>, List<RedlineLayer>>) (x => x.Value));
        for (int index3 = 0; index3 < dictionary4.Count; ++index3)
        {
          KeyValuePair<string, List<RedlineLayer>> keyValuePair3 = dictionary4.ElementAt<KeyValuePair<string, List<RedlineLayer>>>(index3);
          for (int index4 = 0; index4 < keyValuePair3.Value.Count; ++index4)
          {
            RedlineLayer redlineLayer = keyValuePair3.Value[index4];
            ReportAttribute attribute = redlineLayer.StatusRemark.GetAttribute<ReportAttribute>();
            TreeNode treeNode = new TreeNode(keyValuePair3.Key);
            node2.Nodes.Add(treeNode);
            treeNode.ImageIndex = treeNode.SelectedImageIndex = this.toolbarPresenter.GetImageIndexByName(attribute.ImgName);
            treeNode.Tag = (object) redlineLayer;
            TreeViewEventHandler nodeAdded = this.NodeAdded;
            if (nodeAdded != null)
              nodeAdded((object) this.treeView, new TreeViewEventArgs(treeNode));
            this.CreateTree(treeNode, redlineLayer.RedObjectID, list);
          }
        }
        node2.Expand();
      }
      node1.Expand();
    }
  }

  private void CreateTree(TreeNode root, ulong redObjectID, List<RedlineLayer> listAllParents)
  {
    List<RedlineLayer> list = listAllParents.Where<RedlineLayer>((Func<RedlineLayer, bool>) (e => (long) e.ParentID == (long) redObjectID)).ToList<RedlineLayer>();
    list.Sort((Comparison<RedlineLayer>) ((x, y) => DateTime.Compare(x.Time, y.Time)));
    for (int index = 0; index < list.Count; ++index)
    {
      RedlineLayer redlineLayer = list[index];
      TreeNode treeNode = new TreeNode(redlineLayer.NameRemark)
      {
        Tag = (object) redlineLayer
      };
      treeNode.ImageIndex = treeNode.SelectedImageIndex = this.treeView.ImageList.Images.Count + 1;
      root.Nodes.Add(treeNode);
      TreeViewEventHandler nodeAdded = this.NodeAdded;
      if (nodeAdded != null)
        nodeAdded((object) this.treeView, new TreeViewEventArgs(treeNode));
      this.CreateTree(treeNode, redlineLayer.RedObjectID, listAllParents);
    }
  }

  private List<RedlineLayer> ApplyFilter(List<RedlineLayer> list)
  {
    List<RedlineLayer> redlineLayerList = new List<RedlineLayer>();
    list.Sort((Comparison<RedlineLayer>) ((x, y) => DateTime.Compare(x.Time, y.Time)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eAgreed))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eAgreed)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eCorrected))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eCorrected)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eInconsistent))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eInconsistent)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eRejected))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eRejected)));
    return redlineLayerList;
  }

  public void ClearTreeView()
  {
    this.treeView.SelectedNode = (TreeNode) null;
    this.treeView.Nodes.Clear();
    this.ClearBoxView();
  }

  public List<RedlineLayer> GetRedlineLayers()
  {
    return this.treeView.Nodes.Collect().Select<TreeNode, object>((Func<TreeNode, object>) (element => element.Tag)).OfType<RedlineLayer>().ToList<RedlineLayer>();
  }

  public void ClearBoxView()
  {
    this.redLayerInfo.ClearTextBoxes();
    this.tBoxComment.Text = "";
    this.tBoxComment.ReadOnly = true;
  }

  private void treeView_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.treeView.SelectedNode = this.treeView.GetNodeAt(e.X, e.Y);
    if (this.treeView.SelectedNode == null || !(this.popupMenuBar.Items[0] is ContextMenuBarItem contextMenuBarItem))
      return;
    contextMenuBarItem.Show((Control) this.treeView, e.Location);
  }

  private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
  {
    TreeViewCancelEventHandler onNodeSelecting = this.OnNodeSelecting;
    if (onNodeSelecting == null)
      return;
    onNodeSelecting(sender, e);
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeViewEventHandler nodeSelected = this.NodeSelected;
    if (nodeSelected == null)
      return;
    nodeSelected(sender, e);
  }

  private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
  {
    if (this.treeView.ImageList == null || e.Node.ImageIndex < this.treeView.ImageList.Images.Count)
    {
      e.DrawDefault = true;
    }
    else
    {
      e.DrawDefault = false;
      int width1 = this.treeView.ImageList.ImageSize.Width;
      if (this.treeView.ShowLines)
      {
        int num1 = e.Node.Bounds.Left - 3 - width1 / 2;
        int num2 = (e.Node.Bounds.Top + e.Node.Bounds.Bottom) / 2;
        using (Pen pen = new Pen(this.treeView.LineColor, 1f))
        {
          pen.DashStyle = DashStyle.Dot;
          e.Graphics.DrawLine(pen, num1 - width1 / 2, num2, num1 + width1 / 2, num2);
          if (!this.treeView.CheckBoxes)
          {
            if (e.Node.IsExpanded)
              e.Graphics.DrawLine(pen, num1, num2, num1, num2 + width1 / 2);
          }
        }
      }
      Rectangle bounds1 = e.Bounds;
      e.Graphics.FillRectangle(Brushes.White, bounds1);
      using (StringFormat format = new StringFormat()
      {
        Alignment = StringAlignment.Near,
        LineAlignment = StringAlignment.Center
      })
      {
        if (e.Node.IsSelected)
        {
          using (Brush brush1 = (Brush) new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)))
          {
            Graphics graphics = e.Graphics;
            Brush brush2 = brush1;
            Rectangle bounds2 = e.Bounds;
            int x = bounds2.X;
            bounds2 = e.Bounds;
            int y = bounds2.Y;
            Rectangle bounds3 = e.Bounds;
            int width2 = bounds3.Width;
            bounds3 = e.Bounds;
            int height = bounds3.Height;
            graphics.FillRectangle(brush2, x, y, width2, height);
          }
          Graphics graphics1 = e.Graphics;
          Pen black = Pens.Black;
          Rectangle bounds4 = e.Bounds;
          int x1 = bounds4.X;
          bounds4 = e.Bounds;
          int y1 = bounds4.Y;
          Rectangle bounds5 = e.Bounds;
          int width3 = bounds5.Width - 1;
          bounds5 = e.Bounds;
          int height1 = bounds5.Height - 1;
          graphics1.DrawRectangle(black, x1, y1, width3, height1);
          e.Graphics.DrawString(e.Node.Text, this.treeView.Font, Brushes.White, (RectangleF) bounds1, format);
        }
        else
        {
          if (bounds1.Height == 0)
            return;
          e.Graphics.DrawString(e.Node.Text, this.treeView.Font, Brushes.Black, (RectangleF) bounds1, format);
        }
      }
    }
  }

  private void splitContainerRedObject_Paint(object sender, PaintEventArgs e)
  {
    if (!(sender is SplitContainer splitContainer))
      return;
    if (splitContainer.SplitterWidth != this.Font.Height)
      splitContainer.SplitterWidth = this.Font.Height;
    using (StringFormat format = new StringFormat())
    {
      Rectangle splitterRectangle = splitContainer.SplitterRectangle;
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      format.Trimming = StringTrimming.None;
      e.Graphics.DrawString(LocalizationHolder.rm.GetString("Document.Client_171"), this.Font, Brushes.Black, (RectangleF) splitterRectangle, format);
    }
  }

  private void splitContainerRedObject_Resize(object sender, EventArgs e)
  {
    ((Control) sender).Invalidate();
  }

  internal void UpdateSelection()
  {
  }

  /// <summary>Активировать слой замечаний</summary>
  /// <param name="redLayer"></param>
  internal void SetLayer(RedlineLayer redLayer)
  {
    this.redLayerInfo.ClearTextBoxes();
    this.redLayerInfo.UpdateInfoText(redLayer);
    bool flag = redLayer.LockRemark || redLayer.UserID != Redliner.UserNameID;
    this.tBoxComment.Text = redLayer.Comment;
    this.tBoxComment.ReadOnly = flag;
  }

  internal void UpdateRoleCombo(object[] items, string selectedItem)
  {
    ComboBoxItem roleCombo = this.toolbarPresenter.GetRoleCombo();
    items = items ?? roleCombo.Items.OfType<object>().ToArray<object>();
    roleCombo.ComboBox.BeginUpdate();
    roleCombo.Items.Clear();
    roleCombo.Items.AddRange(items);
    roleCombo.ComboBox.EndUpdate();
    roleCombo.ComboBox.SelectedItem = (object) selectedItem;
    this.AdjustComboBoxWidth(selectedItem);
  }

  /// <summary>Подогнать длину комбобокса под текст выбранной роли</summary>
  public void AdjustComboBoxWidth(string sRole)
  {
    ComboBoxItem roleCombo = this.toolbarPresenter.GetRoleCombo();
    if (string.IsNullOrEmpty(sRole))
      return;
    int width = TextRenderer.MeasureText(sRole, roleCombo.ComboBox.Font).Width;
    roleCombo.MinimumControlWidth = width + SystemInformation.VerticalScrollBarWidth;
  }

  internal void UpdateFilterButtonsState()
  {
    this.toolbarPresenter.UpdateTreeToolbarFilterButtons(this.FilterFlags);
  }

  internal void SetFilterFlags(EStatusRemark status)
  {
    if (this.FilterFlags.HasFlag((Enum) status))
      this.FilterFlags &= ~status;
    else
      this.FilterFlags |= status;
    this.UpdateFilterButtonsState();
  }

  /// <summary>Получить список комментариев</summary>
  internal List<string> GetCommentList(TreeNodeCollection nodes = null, List<string> comments = null)
  {
    List<string> stringList = comments;
    if (stringList == null)
      stringList = new List<string>() { "{\\rtf1 " };
    comments = stringList;
    nodes = nodes ?? this.treeView.Nodes;
    foreach (TreeNode node in nodes)
    {
      if (node.Nodes.Count != 0)
        this.GetCommentList(node.Nodes, comments);
      else if (!string.IsNullOrEmpty(node?.Tag is RedlineLayer tag ? tag.Comment : (string) null))
      {
        string s = tag.Comment.TrimEnd(' ', '\n');
        if (!string.IsNullOrEmpty(s))
        {
          comments.AddRange((IEnumerable<string>) node.Parent.GetNodePath());
          comments.Add(node.Text.GetRtfUnicodeEscapedString(true));
          comments.Add(s.GetRtfUnicodeEscapedString());
          comments.Add("");
        }
      }
    }
    return comments;
  }

  /// <summary>Открыть список в комбобоксе</summary>
  internal void OpenRoleCombo() => this.toolbarPresenter.GetRoleCombo().ComboBox.DroppedDown = true;

  /// <summary>Редактировать текст надписи узла</summary>
  internal void EditTreeNode()
  {
    if (this.treeView.SelectedNode == null)
      return;
    this.treeView.LabelEdit = true;
    if (this.treeView.SelectedNode.IsEditing)
      return;
    this.treeView.SelectedNode.BeginEdit();
  }

  /// <summary>Удалить узел замечания из дерева</summary>
  internal void RemoveTreeNode(Redliner redliner)
  {
    RedlineLayer redLayer = this.treeView.SelectedNode?.Tag as RedlineLayer;
    if (redLayer == null)
      return;
    redliner.CurrentRedLayer = (MapLayer) null;
    List<RedlineLayer> source = redliner.ListRedlineLayer();
    RedlineLayer node = source.SingleOrDefault<RedlineLayer>((Func<RedlineLayer, bool>) (x => (long) x.RedObjectID == (long) redLayer.ParentID)) ?? redLayer;
    RedlineLayer redlineLayer = source.SingleOrDefault<RedlineLayer>((Func<RedlineLayer, bool>) (x => (long) x.RedObjectID == (long) node.ParentID)) ?? node;
    List<RedlineLayer> list = source.Where<RedlineLayer>((Func<RedlineLayer, bool>) (x => (long) x.ParentID == (long) node.RedObjectID)).ToList<RedlineLayer>();
    list.Sort((Comparison<RedlineLayer>) ((x, y) => DateTime.Compare(x.Time, y.Time)));
    source.Clear();
    if (list.Count == 1)
    {
      node.LockRemark = false;
      redlineLayer.StatusRemark = node != redlineLayer ? node.StatusRemark : EStatusRemark.eInconsistent;
    }
    if (list.Count == 2)
    {
      if (node == redlineLayer)
      {
        redlineLayer.StatusRemark = (list[0] != redLayer ? list[0] : list[1]).StatusRemark;
      }
      else
      {
        node.LockRemark = false;
        redlineLayer.StatusRemark = node != redlineLayer ? node.StatusRemark : EStatusRemark.eInconsistent;
      }
    }
    this.treeView.SelectedNode.Tag = (object) null;
    redliner.DeleteRedLayer(redLayer);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing) => base.Dispose(disposing);

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.splitContainerRedObject = new RedlineSplitContainer();
    this.treeView = new TreeView();
    this.tBoxComment = new RichTextBox();
    this.redLayerInfo = new UCRedlineLayerInfo();
    this.splitContainerRedObject.BeginInit();
    this.splitContainerRedObject.Panel1.SuspendLayout();
    this.splitContainerRedObject.Panel2.SuspendLayout();
    this.splitContainerRedObject.SuspendLayout();
    this.SuspendLayout();
    this.splitContainerRedObject.Dock = DockStyle.Fill;
    this.splitContainerRedObject.FixedPanel = FixedPanel.Panel2;
    this.splitContainerRedObject.Location = new Point(0, 0);
    this.splitContainerRedObject.Name = "splitContainerRedObject";
    this.splitContainerRedObject.Orientation = Orientation.Horizontal;
    this.splitContainerRedObject.Panel1.Controls.Add((Control) this.treeView);
    this.splitContainerRedObject.Panel2.Controls.Add((Control) this.tBoxComment);
    this.splitContainerRedObject.Panel2.Controls.Add((Control) this.redLayerInfo);
    this.splitContainerRedObject.Size = new Size(219, 532);
    this.splitContainerRedObject.SplitterDistance = 230;
    this.splitContainerRedObject.SplitterWidth = 24;
    this.splitContainerRedObject.TabIndex = 0;
    this.treeView.Dock = DockStyle.Fill;
    this.treeView.Location = new Point(0, 0);
    this.treeView.Name = "treeView";
    this.treeView.Size = new Size(219, 230);
    this.treeView.TabIndex = 0;
    this.tBoxComment.AcceptsTab = true;
    this.tBoxComment.Dock = DockStyle.Fill;
    this.tBoxComment.Location = new Point(0, 0);
    this.tBoxComment.MaxLength = 5000;
    this.tBoxComment.Name = "tBoxComment";
    this.tBoxComment.Size = new Size(219, 128 /*0x80*/);
    this.tBoxComment.TabIndex = 0;
    this.tBoxComment.Padding = new Padding(0, 0, 0, 250);
    this.tBoxComment.Margin = new Padding(0, 0, 0, 270);
    this.tBoxComment.Text = "";
    this.redLayerInfo.Dock = DockStyle.Bottom;
    this.redLayerInfo.Location = new Point(0, 128 /*0x80*/);
    this.redLayerInfo.MinimumSize = new Size(200, 150);
    this.redLayerInfo.Name = "redLayerInfo";
    this.redLayerInfo.Size = new Size(219, 150);
    this.redLayerInfo.Margin = new Padding(0, 130, 0, 0);
    this.redLayerInfo.TabIndex = 1;
    this.Controls.Add((Control) this.splitContainerRedObject);
    this.Name = nameof (ImDocumentRedlineNotesDlg);
    this.MinimumSize = new Size(220, 150);
    this.Size = new Size(220, 532);
    this.splitContainerRedObject.Panel1.ResumeLayout(false);
    this.splitContainerRedObject.Panel2.ResumeLayout(false);
    this.splitContainerRedObject.EndInit();
    this.splitContainerRedObject.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
