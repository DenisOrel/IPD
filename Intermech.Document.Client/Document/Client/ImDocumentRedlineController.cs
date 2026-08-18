// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImDocumentRedlineController
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.PropertyEditors;
using Intermech.Client.Core.Redline;
using Intermech.Client.Core.Redline.Controls;
using Intermech.Client.Core.Visualizers;
using Intermech.Document.Client.UI;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Map;
using Intermech.Redline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client;

public class ImDocumentRedlineController
{
  private readonly IRedService _redService = ServiceUtils.GetService<IRedService>((object) ServicesManager.ServiceContainer, true);
  private Redliner _redliner;
  private ICommandManager _commandManager;
  private ImDocumentRedlineNotesDlg redlineNotes;
  private DocumentControl _documentControl;
  private RedlineDocControlWrapper _mapObject;
  private IPager _pager;
  private RedProperty _redMapProperty;
  private bool _isRedlineEnabled;
  private bool isPointerModeOn;
  /// <summary>
  /// Должность/графа подписи для замечаний. Запонимается после первого выбора и очищается после деактивации вкладки
  /// </summary>
  private string _rankSignature;
  private TransparentRedlineView _view;

  public RedProperty RedMapProperty
  {
    get
    {
      if (this._redMapProperty == null)
      {
        this._redMapProperty = new RedProperty();
        this._redMapProperty.Copy((IRedProperty) this._redService);
      }
      return this._redMapProperty;
    }
  }

  public bool IsRedlineEnabled
  {
    get => this._isRedlineEnabled;
    set
    {
      this._isRedlineEnabled = value;
      this.UpdateButtons();
    }
  }

  public DocumentControl DocControl
  {
    get => this._documentControl;
    set
    {
      if (!((this._documentControl?.Document?.Id ?? string.Empty) != (value?.Document?.Id ?? string.Empty)))
        return;
      this.AssignDocumentControl(value);
    }
  }

  public TransparentRedlineView View
  {
    get => this._view;
    set
    {
      this._view = value;
      if (this._view == null || this._documentControl == null)
        return;
      this.View?.SetZoommingScale(this._documentControl.DocumentScale);
    }
  }

  public ImDocumentRedlineNotesDlg NotesDlg
  {
    get => this.redlineNotes;
    set
    {
      if (this.redlineNotes != null)
      {
        this.redlineNotes.NodeAdded -= new TreeViewEventHandler(this.RedlineNotes_NodeAdded);
        this.redlineNotes.OnNodeSelecting -= new TreeViewCancelEventHandler(this.RedlineNotes_OnNodeSelecting);
        this.redlineNotes.NodeSelected -= new TreeViewEventHandler(this.RedlineNotes_NodeSelected);
        this.redlineNotes.CommentTextChanged -= new EventHandler(this.RedlineNotes_CommentTextChanged);
        this.redlineNotes.NodeRenamed -= new TreeViewEventHandler(this.RedlineNotes_NodeRenamed);
      }
      this.redlineNotes = value;
      if (this.redlineNotes == null)
        return;
      this.redlineNotes.NodeAdded += new TreeViewEventHandler(this.RedlineNotes_NodeAdded);
      this.redlineNotes.OnNodeSelecting += new TreeViewCancelEventHandler(this.RedlineNotes_OnNodeSelecting);
      this.redlineNotes.NodeSelected += new TreeViewEventHandler(this.RedlineNotes_NodeSelected);
      this.redlineNotes.CommentTextChanged += new EventHandler(this.RedlineNotes_CommentTextChanged);
      this.redlineNotes.NodeRenamed += new TreeViewEventHandler(this.RedlineNotes_NodeRenamed);
    }
  }

  public Intermech.Bars.ToolBar RedToolbar { get; internal set; }

  public ImDocumentRedlineController(ICommandManager commandManager)
  {
    this._commandManager = commandManager;
  }

  private void RedlineNotes_NodeAdded(object sender, TreeViewEventArgs e)
  {
    this.SetNodeRemarkForeColor(e.Node);
  }

  private void RedlineNotes_OnNodeSelecting(object sender, TreeViewCancelEventArgs e)
  {
    if (!this.IsRedlineEnabled)
      return;
    this.SetNodeRemarkForeColor(this.NotesDlg.SelectedNode, false);
  }

  private void RedlineNotes_NodeSelected(object sender, TreeViewEventArgs e)
  {
    if (!this.IsRedlineEnabled)
      return;
    TreeView treeView = e.Node.TreeView;
    treeView?.Refresh();
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer != null)
    {
      if (this.IsPageRemark(currentRedLayer))
      {
        List<Page> remarkPages = this.GetRemarkPages(this._redliner, currentRedLayer);
        if (remarkPages != null && remarkPages.Count > 0)
        {
          ((IPager) this._redliner.Relative).Current = (object) remarkPages[0];
          this._redliner.ChangeVisibleLayers((List<object>) null);
          e.Node.ForeColor = Color.Blue;
          this._redliner.ChangeVisibleLayer(currentRedLayer, this.IsRedlineEnabled);
          this.UpdateInfoText(currentRedLayer);
        }
        else
        {
          e.Node.ForeColor = SystemColors.GrayText;
          this.NotesDlg.ClearBoxView();
          this.UpdateInfoText((RedlineLayer) null);
          this._redliner.ChangeVisibleLayers((List<object>) null);
        }
      }
      else
      {
        e.Node.ForeColor = Color.Blue;
        this._redliner.ChangeVisibleLayer(currentRedLayer, this.IsRedlineEnabled);
        this.UpdateInfoText(currentRedLayer);
      }
    }
    else
    {
      this.NotesDlg.ClearBoxView();
      List<RedlineLayer> list1 = (treeView.SelectedNode != null ? treeView.SelectedNode.Nodes : treeView.Nodes).Collect().Select<TreeNode, object>((Func<TreeNode, object>) (element => element.Tag)).OfType<RedlineLayer>().ToList<RedlineLayer>();
      List<RedlineLayer> list2 = list1.Where<RedlineLayer>(new Func<RedlineLayer, bool>(this.IsPageRemark)).ToList<RedlineLayer>();
      List<object> list3 = list1.Where<RedlineLayer>((Func<RedlineLayer, bool>) (x => !this.IsPageRemark(x))).Cast<object>().ToList<object>();
      Page page = list2.SelectMany<RedlineLayer, Page>((Func<RedlineLayer, IEnumerable<Page>>) (x => (IEnumerable<Page>) this.GetRemarkPages(this._redliner, x))).FirstOrDefault<Page>((Func<Page, bool>) (x => x != null));
      if (page != null)
      {
        List<object> list4 = list2.Cast<object>().ToList<object>();
        ((IPager) this._redliner.Relative).Current = (object) page;
        this._redliner.ChangeVisibleLayers(list4);
      }
      else
        this._redliner.ChangeVisibleLayers(list3);
    }
    this._redliner.OnChanged();
  }

  private void RedlineNotes_CommentTextChanged(object sender, EventArgs e)
  {
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || !(currentRedLayer.Comment != this.NotesDlg.Comment))
      return;
    currentRedLayer.UndoManager.StartTransaction();
    currentRedLayer.CommentText.Text = currentRedLayer.Comment = this.NotesDlg.Comment;
    currentRedLayer.UndoManager.FinishTransaction("Comment");
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
  }

  private void RedlineNotes_NodeRenamed(object sender, TreeViewEventArgs e)
  {
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || !(currentRedLayer.NameRemark != e.Node?.Text))
      return;
    currentRedLayer.UndoManager.StartTransaction();
    currentRedLayer.NameRemark = e.Node?.Text ?? "?";
    currentRedLayer.UndoManager.FinishTransaction("NameRemark");
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
  }

  internal void RoleChanged(object sender, EventArgs e)
  {
    if (!(sender is ComboBoxItem comboBoxItem) || !(comboBoxItem.ComboBox.SelectedItem is string selectedItem))
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || !(currentRedLayer.Signature != selectedItem))
      return;
    currentRedLayer.UndoManager.StartTransaction();
    currentRedLayer.SignatureText.Text = currentRedLayer.Signature = selectedItem;
    currentRedLayer.UndoManager.FinishTransaction("Signature");
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
    this.NotesDlg.AdjustComboBoxWidth(selectedItem);
    this.NotesDlg.FillTreeView(this._redliner);
    this.NotesDlg.UpdateTreeView(currentRedLayer, this._redliner);
  }

  internal void RoleDropDownOpened(object sender, EventArgs e)
  {
    float num1;
    using (Graphics graphics = this.NotesDlg.CreateGraphics())
      num1 = graphics.DpiX / 96f;
    ComboBox myCombo = (ComboBox) sender;
    int verticalScrollBarWidth = myCombo.Items.Count > myCombo.MaxDropDownItems ? SystemInformation.VerticalScrollBarWidth : 0;
    int num2 = myCombo.Items.OfType<object>().Select<object, int>((Func<object, int>) (x => TextRenderer.MeasureText(x.ToString(), myCombo.Font).Width)).DefaultIfEmpty<int>(0).Max();
    myCombo.DropDownWidth = (int) ((double) (num2 + verticalScrollBarWidth + 3) * (double) num1);
    if (myCombo.Items.Count < 2)
      myCombo.DropDownHeight = 1;
    else
      myCombo.DropDownHeight = 100;
  }

  internal void RoleDropDownClosed(object sender, EventArgs e)
  {
    if (!(sender is ComboBoxItem comboBoxItem))
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || currentRedLayer.ParentID != 0UL || (currentRedLayer.LockRemark ? 1 : (currentRedLayer.UserID != Redliner.UserNameID ? 1 : 0)) != 0)
      return;
    this._rankSignature = comboBoxItem.ComboBox.SelectedItem as string;
  }

  private void AssignDocumentControl(DocumentControl value)
  {
    if (value != null)
    {
      this._documentControl = value;
      this._mapObject = new RedlineDocControlWrapper(this._documentControl);
      this.AttachRedliner(this._mapObject);
      this.View?.SetZoommingScale(this._documentControl.DocumentScale);
      this._documentControl.PageControl.Painted += new PaintEventHandler(this.OnDocControlPainted);
      this._documentControl.PageControl.Layout += new LayoutEventHandler(this.OnDocControlGotFocus);
      this._documentControl.ZoomValueChanged += new EventHandler(this.OnDocControlZoomChanged);
    }
    else
    {
      if (this._documentControl != null)
      {
        this._documentControl.ZoomValueChanged -= new EventHandler(this.OnDocControlZoomChanged);
        this._documentControl.PageControl.Painted -= new PaintEventHandler(this.OnDocControlPainted);
        this._documentControl.PageControl.Layout -= new LayoutEventHandler(this.OnDocControlGotFocus);
      }
      this.DetachRedliner();
      this._mapObject = (RedlineDocControlWrapper) null;
      this._documentControl = (DocumentControl) null;
    }
  }

  private void OnDocControlZoomChanged(object sender, EventArgs e)
  {
    this.View?.SetZoommingScale(this._documentControl.DocumentScale);
  }

  private void OnDocControlPainted(object sender, PaintEventArgs e)
  {
    this.View?.RaiseOnPaintWithArgs(e);
  }

  private void OnDocControlGotFocus(object sender, EventArgs e)
  {
    if (!this.View.Visible)
      return;
    this.View.BringToFront();
  }

  private void AttachRedliner(RedlineDocControlWrapper mapObject)
  {
    this._redliner = new Redliner((MapView) this.View, (IMapRelative) mapObject, this.RedMapProperty)
    {
      UseUnitsConversion = true
    };
    this.View.ViewChanged += new EventHandler(this._redliner.OnViewChanged);
    this.View.SelectionDeleted += new EventHandler(this._redliner.OnViewChanged);
    this.View.SelectionMoved += new EventHandler(this._redliner.OnViewChanged);
    this.View.ObjectSingleClicked += new MapObjectEventHandler(this.OnObjectSingleClicked);
    this.View.ZoomRequested += new ZoomRequestedHandler(this.OnViewZoomRequested);
    this.View.ObjectEdited += new MapSelectionEventHandler(this.OnObjectEdited);
    this._redliner.RestoreTools();
    this._redliner.EditRedRole = SignsClient.ShowUserGraphs(this.DocControl.Document.DBObjectID);
    if (this._redliner.EditRedRole.Length == 0)
      this._redliner.EditRedRole = SignsClient.ShowUserGraphs(0L);
    this._redliner.Changed += new EventHandler(this.RedlinerChanged);
    IPager pager;
    if ((RedlineDocControlWrapper) (pager = (IPager) mapObject) != null)
      pager.PageChanged += new EventHandler(this.PageChanged_);
    this.LoadRedlineData();
    this.NotesDlg.FillTreeView(this._redliner);
    this.CheckAllRedView();
  }

  private void OnViewZoomRequested(object sender, MapInputEventArgs e)
  {
    double num = (double) this._documentControl.SetZoom(DocZoomMode.Custom, this._documentControl.DocumentScale + (float) e.Delta / 1200f);
  }

  private void OnObjectSingleClicked(object sender, MapObjectEventArgs e)
  {
    if (!(this.View.Tool is RedLinePointerTool))
      return;
    this._redliner.CancelDraw();
    this.UpdateButtons();
    this.UpdateMapViewTransparency();
  }

  private void OnObjectEdited(object sender, MapSelectionEventArgs e)
  {
    if (!(this.View.Tool is RedLineNoteTool tool) && !this.isPointerModeOn)
      return;
    if (this.isPointerModeOn)
    {
      this.isPointerModeOn = false;
      this._redliner.View.Selection.Clear();
    }
    if (tool != null)
      this._redliner.CancelDraw();
    this.UpdateButtons();
    this.UpdateMapViewTransparency();
  }

  private void CheckAllRedView()
  {
    if (!this.IsRedlineEnabled)
      return;
    this.NotesDlg.ClearBoxView();
    List<RedlineLayer> list = this.NotesDlg.GetRedlineLayers().Where<RedlineLayer>(new Func<RedlineLayer, bool>(this.IsPageRemark)).ToList<RedlineLayer>();
    object current = ((IPager) this._redliner.Relative).Current;
    List<Page> pageList1;
    if (!(current is List<Page> pageList2))
      pageList1 = new List<Page>() { current as Page };
    else
      pageList1 = pageList2;
    List<Page> curPages = pageList1;
    Func<RedlineLayer, bool> predicate = (Func<RedlineLayer, bool>) (x =>
    {
      List<Page> remarkPages = this.GetRemarkPages(this._redliner, x);
      return remarkPages != null && remarkPages.Intersect<Page>((IEnumerable<Page>) curPages).Any<Page>();
    });
    this._redliner.ChangeVisibleLayers(list.Where<RedlineLayer>(predicate).Cast<object>().ToList<object>());
    this._redliner.OnChanged();
  }

  private void LoadRedlineData()
  {
    if (this._redliner == null)
      return;
    this._redliner.LoadData(this._documentControl.Document.DBObjectID, -1L, this._documentControl.Document.FileName);
    this._redliner.SetDirty(false);
  }

  private void PageChanged_(object sender, EventArgs e)
  {
  }

  public void SaveRedline()
  {
    if (this._redliner == null)
      return;
    if (this._redliner.Dirty)
    {
      if (MessageBox.Show("Данные в графических замечаниях были изменены. Сохранить изменения?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
      {
        this.WriteRedlineData();
      }
      else
      {
        this.LoadRedlineData();
        this.NotesDlg.FillTreeView(this._redliner);
      }
    }
    this._redliner.OnChanged();
  }

  private void WriteRedlineData()
  {
    this._redliner.WriteData(this._documentControl.Document.DBObjectID, -1L, this._documentControl.Document.FileName, true);
  }

  private void RedlinerChanged(object sender, EventArgs e) => this.UpdateButtons();

  private void UpdateButtons()
  {
    if (this.View == null)
      return;
    IMapTool tool = this.View.Tool;
    foreach (object obj in (CollectionBase) this.RedToolbar.Items)
    {
      if (obj is ButtonItemBase buttonItemBase)
      {
        ICommandState command = this._commandManager.FindCommand(buttonItemBase.CommandName);
        if (command != null && this.QueryStatus(command) && command.Enabled)
          buttonItemBase.Checked = buttonItemBase.CommandName.Equals(tool.GetType().Name, StringComparison.OrdinalIgnoreCase) || this.isPointerModeOn && buttonItemBase.CommandName.Equals("RedLinePointerTool", StringComparison.OrdinalIgnoreCase);
      }
    }
    foreach (object obj in (CollectionBase) this.NotesDlg.NotesTreeToolbar.Items)
    {
      if (obj is ButtonItemBase buttonItemBase)
      {
        ICommandState command = this._commandManager.FindCommand(buttonItemBase.CommandName);
        if (command != null)
          this.QueryStatus(command);
      }
    }
    this.NotesDlg?.UpdateFilterButtonsState();
  }

  private void UpdateInfoText(RedlineLayer redLayer)
  {
    if (redLayer == null)
    {
      this.NotesDlg.ClearBoxView();
    }
    else
    {
      this.NotesDlg.SetLayer(redLayer);
      int num = redLayer.LockRemark ? 1 : (redLayer.UserID != Redliner.UserNameID ? 1 : 0);
      string signature = redLayer.Signature;
      List<string> signatures = this._redliner.GenerateSignatures(redLayer.StatusRemark);
      if (num != 0)
        signatures.Clear();
      if (!signatures.Contains(signature))
        signatures.Insert(0, signature);
      this.NotesDlg.UpdateRoleCombo(signatures.Cast<object>().ToArray<object>(), signature);
    }
  }

  private void DetachRedliner()
  {
    if (this._redliner == null)
      return;
    this.SaveRedline();
    IPager mapObject;
    if ((RedlineDocControlWrapper) (mapObject = (IPager) this._mapObject) != null)
      mapObject.PageChanged -= new EventHandler(this.PageChanged_);
    this._redliner.Changed -= new EventHandler(this.RedlinerChanged);
    this.View.ObjectSingleClicked -= new MapObjectEventHandler(this.OnObjectSingleClicked);
    this.View.ZoomRequested -= new ZoomRequestedHandler(this.OnViewZoomRequested);
    this.View.ObjectEdited -= new MapSelectionEventHandler(this.OnObjectEdited);
    this.NotesDlg?.ClearTreeView();
    this._redliner.DeleteRedLayers();
    this._redliner.Dispose();
    this._redliner = (Redliner) null;
  }

  private void SetNodeRemarkForeColor(TreeNode node, bool SelectedNodeBlue = true)
  {
    if (node == null)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer(node);
    if (this.IsPageRemark(currentRedLayer))
    {
      TreeNode treeNode = node;
      List<Page> remarkPages = this.GetRemarkPages(this._redliner, currentRedLayer);
      Color color = (remarkPages != null ? (remarkPages.Any<Page>() ? 1 : 0) : 0) != 0 ? (node.IsSelected & SelectedNodeBlue ? Color.Blue : SystemColors.ControlText) : SystemColors.GrayText;
      treeNode.ForeColor = color;
    }
    else
      node.ForeColor = node.IsSelected & SelectedNodeBlue ? Color.Blue : SystemColors.ControlText;
  }

  /// <summary>Найти страницу соответствующую замечанию</summary>
  /// <param name="redliner"></param>
  /// <param name="itemRemark"></param>
  /// <returns></returns>
  private List<Page> GetRemarkPages(Redliner redliner, RedlineLayer itemRemark)
  {
    MapLayer mapLayer = this.View.Document.Layers.Find((object) itemRemark);
    if (redliner == null)
      return (List<Page>) null;
    List<object> redPagesForLayer = redliner.GetRedPagesForLayer(mapLayer);
    return redPagesForLayer == null ? (List<Page>) null : redPagesForLayer.OfType<Page>().ToList<Page>();
  }

  /// <summary>Относится ли замечение к странице</summary>
  /// <param name="itemRemark"></param>
  /// <returns>true - к странице, false -  целиком к документу </returns>
  private bool IsPageRemark(RedlineLayer itemRemark)
  {
    MapLayer source = this.View.Document.Layers.Find((object) itemRemark);
    return source != null && source.OfType<IMapRelativePosition>().Any<IMapRelativePosition>();
  }

  private RedlineLayer GetCurrentRedLayer(TreeNode node = null)
  {
    if (node == null)
      node = this.NotesDlg?.SelectedNode;
    return node?.Tag as RedlineLayer;
  }

  internal bool QueryStatus(ICommandState commandState)
  {
    if (this._redliner == null || this.View == null)
      return true;
    bool flag1 = this.View != null && this.View.Visible && this.IsRedlineEnabled;
    bool flag2 = this.IsRedlineEnabled && this._redliner.CurrentRedLayer != null && this._redliner.CurrentRedLayer.AllowEdit;
    bool flag3 = true;
    if (this._redliner?.Relative is IPager relative)
    {
      List<object> redPagesForLayer = this._redliner.GetRedPagesForLayer(this._redliner.CurrentRedLayer);
      if (relative.Current is Page current2)
        flag3 = redPagesForLayer == null || redPagesForLayer.OfType<Page>().Contains<Page>(current2);
      else if (relative.Current is List<Page> current1)
        flag3 = redPagesForLayer == null || !redPagesForLayer.Any<object>() || redPagesForLayer.OfType<Page>().Intersect<Page>((IEnumerable<Page>) current1).Any<Page>();
    }
    bool flag4 = flag1 & flag2 & flag3;
    switch (commandState.CommandName)
    {
      case "DistanceTool":
      case "RedBoxRole":
      case "RedLineCircleFillTool":
      case "RedLineCircleTool":
      case "RedLineEllipseFillTool":
      case "RedLineEllipseTool":
      case "RedLineNoteTool":
      case "RedLinePencilTool":
      case "RedLinePointerTool":
      case "RedLineRectangleFillTool":
      case "RedLineRectangleTool":
      case "RedLineStrokeTool":
        commandState.Enabled = flag4;
        commandState.Visible = flag1;
        return true;
      case "RedColor":
        commandState.Visible = flag1;
        commandState.Enabled = flag1 && this.RedMapProperty != null;
        return true;
      case "RedNew":
        commandState.Enabled = flag1;
        return true;
      case "RedRedo":
        commandState.Enabled = flag1 && this._redliner.CanRedo;
        return true;
      case "RedSave":
        commandState.Enabled = flag1 && this._redliner.Dirty;
        return true;
      case "RedUndo":
        commandState.Enabled = flag1 && this._redliner.CanUndo;
        return true;
      case "RemoveNote":
      case "RenameNote":
        this.CheckCommandState_RenameRemove(commandState);
        return true;
      case "eAgreed~E":
      case "eInconsistent~E":
        this.CheckCommandState_InconsistentOrAgreed(commandState);
        return true;
      case "eAgreed~F":
      case "eCorrected~F":
      case "eInconsistent~F":
      case "eRejected~F":
        commandState.Enabled = flag1;
        commandState.Visible = flag1;
        return true;
      case "eCorrected~E":
      case "eRejected~E":
        this.CheckCommandState_CorrectedOrRejected(commandState);
        return true;
      default:
        return false;
    }
  }

  private void CheckCommandState_RenameRemove(ICommandState commandState)
  {
    commandState.Visible = true;
    commandState.Enabled = false;
    if (!this.IsRedlineEnabled)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || currentRedLayer.LockRemark || currentRedLayer.ParentID != 0UL && !this._redliner.isEditRedRole || currentRedLayer.UserID != Redliner.UserNameID)
      return;
    commandState.Enabled = true;
  }

  private void CheckCommandState_InconsistentOrAgreed(ICommandState commandState)
  {
    commandState.Visible = commandState.Enabled = false;
    if (!this.IsRedlineEnabled || !this._redliner.isEditRedRole)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || currentRedLayer.LockRemark || currentRedLayer.StatusRemark == EStatusRemark.eInconsistent || this._redliner.ListChainRedlineLayer(currentRedLayer.RedObjectID).Any<RedlineLayer>((Func<RedlineLayer, bool>) (u => u.StatusRemark == EStatusRemark.eAgreed)))
      return;
    commandState.Visible = true;
    if (currentRedLayer.UserID == Redliner.UserNameID)
      return;
    EStatusRemark status = commandState.CommandName.Split('~')[0].ToEnum<EStatusRemark>();
    if (!Enum.IsDefined(typeof (EStatusRemark), (object) status) || this._redliner.GenerateSignatures(status).Count == 0)
      return;
    commandState.Enabled = true;
  }

  private void CheckCommandState_CorrectedOrRejected(ICommandState commandState)
  {
    commandState.Visible = commandState.Enabled = false;
    if (!this.IsRedlineEnabled || !this._redliner.isEditRedRole)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || currentRedLayer.LockRemark || currentRedLayer.StatusRemark == EStatusRemark.eCorrected || currentRedLayer.StatusRemark == EStatusRemark.eRejected || this._redliner.ListChainRedlineLayer(currentRedLayer.RedObjectID).Any<RedlineLayer>((Func<RedlineLayer, bool>) (u => u.StatusRemark == EStatusRemark.eAgreed)))
      return;
    commandState.Visible = true;
    if (currentRedLayer.UserID == Redliner.UserNameID)
      return;
    EStatusRemark status = commandState.CommandName.Split('~')[0].ToEnum<EStatusRemark>();
    if (!Enum.IsDefined(typeof (EStatusRemark), (object) status) || this._redliner.GenerateSignatures(status).Count == 0)
      return;
    commandState.Enabled = true;
  }

  private void PerformFilterAction(EStatusRemark status)
  {
    this.NotesDlg.SetFilterFlags(status);
    this.NotesDlg.FillTreeView(this._redliner);
    this.NotesDlg.UpdateTreeView((RedlineLayer) null, this._redliner);
  }

  private void DisplayOverallComentList()
  {
    using (RTFEditorForm rtfEditorForm = new RTFEditorForm())
    {
      rtfEditorForm.Text = LocalizationHolder.rm.GetString("Document.Client_171");
      rtfEditorForm.RTFText = string.Join("\\line ", (IEnumerable<string>) this.NotesDlg.GetCommentList());
      int num = (int) rtfEditorForm.ShowDialog();
    }
  }

  private void ChangeRedProperty()
  {
    using (RedPropertyView redPropertyView = new RedPropertyView())
    {
      redPropertyView.LoadSettings(this.RedMapProperty);
      if (redPropertyView.ShowDialog() != DialogResult.OK)
        return;
      redPropertyView.Apply();
    }
  }

  private void CreateNewNote()
  {
    if (!this.IsRedlineEnabled)
      return;
    RedlineLayer redlineLayer = this._redliner.CreateRedlineLayer(this.DocControl.Document.DBObjectID, EStatusRemark.eInconsistent, true);
    if (string.IsNullOrEmpty(this._rankSignature))
      this._rankSignature = this._redliner.NewSignature(EStatusRemark.eInconsistent) ?? "Не выбрана";
    if (string.IsNullOrEmpty(this._rankSignature))
      return;
    redlineLayer.Signature = this._rankSignature;
    MapDocument document = this.View.Document;
    MapLayer newLayerAfter = document.Layers.CreateNewLayerAfter(document.Layers.Default);
    newLayerAfter.Identifier = (object) redlineLayer;
    newLayerAfter.Add((MapObject) redlineLayer.CreateCommentText());
    newLayerAfter.Add((MapObject) redlineLayer.CreateSignatureText());
    this.NotesDlg.FillTreeView(this._redliner);
    this._redliner.CurrentRedLayer = newLayerAfter;
    this.NotesDlg.UpdateTreeView((RedlineLayer) null, this._redliner);
    redlineLayer.UndoManager.Clear();
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
  }

  internal bool Execute(ICommandState commandState)
  {
    if (!this.View.Enabled || !this.IsRedlineEnabled)
      return false;
    switch (commandState.CommandName)
    {
      case "RedColor":
        this.ChangeRedProperty();
        return true;
      case "RedComments":
        this.DisplayOverallComentList();
        return true;
      case "RedLineCircleFillTool":
        if (!(this._redliner.View.Tool is RedLineCircleFillTool))
          this._redliner.DrawCircleFill();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLineCircleTool":
        if (!(this._redliner.View.Tool is RedLineCircleTool))
          this._redliner.DrawCircle();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLineEllipseFillTool":
        if (!(this._redliner.View.Tool is RedLineEllipseFillTool))
          this._redliner.DrawEllipseFill();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLineEllipseTool":
        if (!(this._redliner.View.Tool is RedLineEllipseTool))
          this._redliner.DrawEllipse();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLineNoteTool":
        if (!(this._redliner.View.Tool is RedLineNoteTool))
          this._redliner.DrawNote();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLinePencilTool":
        if (!(this._redliner.View.Tool is RedLinePencilTool))
          this._redliner.DrawPencil();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLinePointerTool":
        this.isPointerModeOn = !this.isPointerModeOn;
        if (!this.isPointerModeOn)
          this._redliner.View.Selection.Clear();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLineRectangleFillTool":
        if (!(this._redliner.View.Tool is RedLineRectangleFillTool))
          this._redliner.DrawRectangleFill();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLineRectangleTool":
        if (!(this._redliner.View.Tool is RedLineRectangleTool))
          this._redliner.DrawRectangle();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedLineStrokeTool":
        if (!(this._redliner.View.Tool is RedLineStrokeTool))
          this._redliner.DrawLine();
        else
          this._redliner.CancelDraw();
        this.UpdateButtons();
        this.UpdateMapViewTransparency();
        return true;
      case "RedNew":
        this.CreateNewNote();
        return true;
      case "RedRedo":
      case "RedUndo":
        this._redliner.CancelDraw();
        if (commandState.CommandName == "RedUndo")
          this._redliner.Undo();
        else
          this._redliner.Redo();
        if (this._redliner.CurrentRedLayer?.Identifier is RedlineLayer identifier)
        {
          this._redliner.ChangeVisibleLayer(identifier, this.IsRedlineEnabled);
          identifier.Comment = identifier.CommentText.Text;
          identifier.Signature = identifier.SignatureText.Text;
          this.NotesDlg.Comment = identifier.Comment;
          this.NotesDlg.UpdateRoleCombo((object[]) null, identifier.SignatureText.Text);
          this.NotesDlg.FillTreeView(this._redliner);
          this.NotesDlg.UpdateTreeView(identifier, this._redliner);
        }
        this.UpdateButtons();
        return true;
      case "RedSave":
        this._redliner.CancelDraw();
        this.WriteRedlineData();
        this._redliner.SetDirty(false);
        this.UpdateButtons();
        return true;
      case "RemoveNote":
        this.NotesDlg.RemoveTreeNode(this._redliner);
        this.NotesDlg.FillTreeView(this._redliner);
        this._redliner.SetDirty(true);
        this._redliner.OnChanged();
        this._redliner.CancelDraw();
        return true;
      case "RenameNote":
        this.NotesDlg.EditTreeNode();
        return true;
      case "eAgreed~E":
      case "eCorrected~E":
      case "eInconsistent~E":
      case "eRejected~E":
        this.PerformChangeStatusAction(commandState.CommandName.Split('~')[0].ToEnum<EStatusRemark>());
        return true;
      case "eAgreed~F":
      case "eCorrected~F":
      case "eInconsistent~F":
      case "eRejected~F":
        this.PerformFilterAction(commandState.CommandName.Split('~')[0].ToEnum<EStatusRemark>());
        return true;
      default:
        return false;
    }
  }

  private void UpdateMapViewTransparency()
  {
    bool flag = this._redliner.TypeTool != (System.Type) null || this.isPointerModeOn;
    if (flag)
      this.DocControl.DeactivateInPlaceEditor();
    if (!(this._redliner.View is TransparentRedlineView view))
      return;
    view.TrackToolActions = flag;
  }

  private void PerformChangeStatusAction(EStatusRemark status)
  {
    if (!Enum.IsDefined(typeof (EStatusRemark), (object) status))
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    MapLayer layer = this.View.Document.Layers.Find((object) currentRedLayer);
    string str = this._redliner.NewSignature(status);
    if (string.IsNullOrEmpty(str))
      return;
    RedlineLayer redlineLayer = this._redliner.CreateRedlineLayer(this.DocControl.Document.DBObjectID, status);
    redlineLayer.Signature = str;
    redlineLayer.ParentID = currentRedLayer.RedObjectID;
    this._redliner.ListChainRedlineLayer(currentRedLayer.RedObjectID).Last<RedlineLayer>().StatusRemark = status;
    MapDocument document = this.View.Document;
    MapLayer newLayerAfter = document.Layers.CreateNewLayerAfter(document.Layers.Default);
    newLayerAfter.Identifier = (object) redlineLayer;
    newLayerAfter.Add((MapObject) redlineLayer.CreateCommentText());
    newLayerAfter.Add((MapObject) redlineLayer.CreateSignatureText());
    this._redliner.CopyLayerDark(layer, newLayerAfter);
    currentRedLayer.LockRemark = true;
    redlineLayer.UndoManager.Clear();
    this.NotesDlg.FillTreeView(this._redliner);
    this._redliner.CurrentRedLayer = newLayerAfter;
    this.NotesDlg.UpdateTreeView((RedlineLayer) null, this._redliner);
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
  }

  public void ResetRankSignature() => this._rankSignature = string.Empty;
}
