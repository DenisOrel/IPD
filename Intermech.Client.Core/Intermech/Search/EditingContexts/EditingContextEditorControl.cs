
// Type: Intermech.Search.EditingContexts.EditingContextEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using Intermech.Search.ComponentModel;
using Intermech.Search.UI;
using Intermech.Search.UI.VirtualTree;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.EditingContexts;

public sealed class EditingContextEditorControl : UserControl, ISupportInitialize
{
  private long _editingContextVersionID;
  private bool _canEdit;
  private bool _hasChanges;
  private bool _showLinkedEditingContexts;
  private EditingContext _editingContext;
  private EditingContext[] _linkedEditingContexts;
  private EditingContext[] _editingContextAndLinkedEditingContexts;
  private bool _isLinkedEdintingContextLoaded;
  private NavigatorColumnsTreeFeature _navigatorColumnsTreeFeature;
  private NavigatorSelectedItemsTreeFeature _navigatorSelectedItemsTreeFeature;
  private LazyService<ICommandManager> _commandManager = new LazyService<ICommandManager>();
  private LazyService<INamedImageList> _namedImageList = new LazyService<INamedImageList>();
  private LazyService<ICategoryTypeIconService> _categoryTypeIconService = new LazyService<ICategoryTypeIconService>();
  private LazyService<IClipboard> _clipboard = new LazyService<IClipboard>();
  private LazyService<INotificationService> _notificationService = new LazyService<INotificationService>();
  private EditingContextEditorControl.EditingContextRowBinding _editingContextRowBinding = new EditingContextEditorControl.EditingContextRowBinding();
  private EditingContextEditorControl.EditingContextEditorCommandTarget _editingContextEditorCommandTarget;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Search.UI.VirtualTree.VirtualTree _tree;
  private ToolStrip toolStrip1;
  private ToolStripButton _openInNewWindowToolStripButton;
  private ToolStripButton _cardToolStripButton;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton _addToolStripButton;
  private ToolStripButton _addWithCompositionToolStripButton;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripButton _replaceToolStripButton;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripButton _pasteToolStripButton;
  private ToolStripSeparator toolStripSeparator4;
  private ToolStripButton _removeToolStripButton;
  private ToolStripSeparator toolStripSeparator5;
  private ToolStripButton _toggleLinkedEditingContextsToolStripButton;
  private ToolStripSeparator toolStripSeparator7;
  private ToolStripButton _customizeColumnsToolStripButton;
  private Button _cancelButton;
  private Button _acceptButton;
  private ContextMenuStrip _contextMenuStrip;
  private ToolStripMenuItem _openInNewWindowToolStripMenuItem;
  private ToolStripMenuItem _cardToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator8;
  private ToolStripMenuItem _addToolStripMenuItem;
  private ToolStripMenuItem _addWithCompositionToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator9;
  private ToolStripMenuItem _replaceToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator10;
  private ToolStripMenuItem _pasteToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator11;
  private ToolStripMenuItem _removeToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator13;
  private ToolStripMenuItem _toggleLinkedEditingContextsToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator14;
  private ToolStripMenuItem _customizeColumnsToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator6;
  private ToolStripLabel toolStripLabel1;
  private ToolStripTextBox _searchToolStripTextBox;
  private Panel panel1;
  private MessageControl _messageControl;
  private Panel panel2;
  private ToolStripSeparator toolStripSeparator12;
  private ToolStripButton _refreshToolStripButton;
  private ToolStripSeparator toolStripSeparator15;
  private ToolStripMenuItem _refreshToolStripMenuItem;
  private ToolStripButton _addProductVersionsToolStripButton;
  private ToolStripMenuItem _addProductVersionsToolStripMenuItem;
  private Label _objectsCountLabel;
  private Label label1;

  public EditingContextEditorControl()
  {
    this.InitializeComponent();
    this._navigatorColumnsTreeFeature = new NavigatorColumnsTreeFeature(this._tree)
    {
      NavigatorColumnsKey = new NavigatorColumnsKey(2, -1, "ObjectsInEditingContext")
    };
    this._navigatorSelectedItemsTreeFeature = new NavigatorSelectedItemsTreeFeature(this._tree);
    this._tree.RowBindings.Add((RowBinding) this._editingContextRowBinding);
    this._tree.RowBindings.Add((RowBinding) new EditingContextEditorControl.EditingContextItemRowBinding());
    this._editingContextEditorCommandTarget = new EditingContextEditorControl.EditingContextEditorCommandTarget(this);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HasChanges
  {
    get => this._hasChanges;
    private set
    {
      if (this._hasChanges == value)
        return;
      this._hasChanges = value;
      this.UpdateControls();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long EditingContextVersionID
  {
    get => this._editingContextVersionID;
    set
    {
      if (this._editingContextVersionID == value)
        return;
      this._editingContextVersionID = value;
      this.SetEditingContextVersionID();
      this.HasChanges = false;
    }
  }

  public void AcceptChanges()
  {
    if (!this.HasChanges)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IEditingContextServerService customService = sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService;
      SaveEditingContextParams editingContextParams = new SaveEditingContextParams(this._editingContext.Object.VersionID)
      {
        ObjectVersionIds = this._editingContext.Items.Select<EditingContextItem, long>((System.Func<EditingContextItem, long>) (o => o.Object.VersionID)).ToArray<long>()
      };
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      SaveEditingContextParams saveEditingContextParams = editingContextParams;
      customService.SaveEditingContext(sessionGuid, saveEditingContextParams);
    }
    this.HasChanges = false;
  }

  public void CancelChanges()
  {
    if (!this.HasChanges || MessageBox.Show($"Состав контекста редактирования был изменен.{Environment.NewLine}Отменить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.SetEditingContextVersionID();
    this.HasChanges = false;
  }

  void ISupportInitialize.BeginInit()
  {
  }

  void ISupportInitialize.EndInit()
  {
    if (this.DesignMode)
      return;
    this._navigatorSelectedItemsTreeFeature.AddSelectedItemsToGlobalServiceContainerOnFocus = true;
    this._navigatorColumnsTreeFeature.GetAndApplyNavigatorColumnsFromNavigatorColumnsService(true);
    if (this._tree.Columns.Count == 0)
      this._navigatorColumnsTreeFeature.SetNodeColumnCollection(this.GetDefaultNodeColumnCollection());
    this._openInNewWindowToolStripButton.Image = this._openInNewWindowToolStripMenuItem.Image = this.GetImageFromNamedImageList("imgNavigator");
    this._cardToolStripButton.Image = this._cardToolStripMenuItem.Image = this.GetImageFromNamedImageList("imgCard");
    this._addProductVersionsToolStripButton.Image = this._addProductVersionsToolStripMenuItem.Image = this.GetImage16x16FromCategoryTypeIconService(4, MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545"));
    this._replaceToolStripButton.Image = this._replaceToolStripMenuItem.Image = this.GetImageFromNamedImageList("imgReplace");
    this._pasteToolStripButton.Image = this._pasteToolStripMenuItem.Image = this.GetImageFromNamedImageList("imgPaste");
    this._toggleLinkedEditingContextsToolStripButton.Image = this._toggleLinkedEditingContextsToolStripMenuItem.Image = this.GetImage16x16FromCategoryTypeIconService(4, EditingContextConstants.EditingContextObjectTypeID);
    this._customizeColumnsToolStripButton.Image = this._customizeColumnsToolStripMenuItem.Image = this.GetImageFromNamedImageList("imgViewSettings");
    this._notificationService.Value.Subscribe(new NotificationEventHandler(this.NotificationService_NotificationFired));
  }

  private void EditingContextEditorControl_Enter(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._commandManager.Value.AddTarget((ICommandTarget) this._editingContextEditorCommandTarget);
    this._commandManager.Value.ActiveTarget = (ICommandTarget) this._editingContextEditorCommandTarget;
  }

  private void EditingContextEditorControl_Leave(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this._commandManager.Value.RemoveTarget((ICommandTarget) this._editingContextEditorCommandTarget);
  }

  private void OpenInNewWindowToolStripButton_Click(object sender, EventArgs e)
  {
    this.OpenInNewWindow();
  }

  private void CardToolStripButton_Click(object sender, EventArgs e) => this.Card();

  private void AddProductVersionsToolStripButton_Click(object sender, EventArgs e)
  {
    this.AddProductVersions();
  }

  private void AddToolStripButton_Click(object sender, EventArgs e) => this.Add();

  private void AddWithCompositionToolStripButton_Click(object sender, EventArgs e)
  {
    this.AddWithComposition();
  }

  private void ReplaceToolStripButton_Click(object sender, EventArgs e) => this.Replace();

  private void PasteToolStripButton_Click(object sender, EventArgs e) => this.Paste();

  private void RemoveToolStripButton_Click(object sender, EventArgs e) => this.Remove();

  private void RefreshToolStripButton_Click(object sender, EventArgs e) => this.RefreshItems(true);

  private void ToggleLinkedEditingContextsToolStripButton_Click(object sender, EventArgs e)
  {
    this.ToggleLinkedEditingContexts();
  }

  private void СustomizeColumnsToolStripButton_Click(object sender, EventArgs e)
  {
    this.CustomizeColumns();
  }

  private void SearchToolStripTextBox_Leave(object sender, EventArgs e)
  {
    this._editingContextRowBinding.SearchString = this._searchToolStripTextBox.Text;
  }

  private void SearchToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this._editingContextRowBinding.SearchString = this._searchToolStripTextBox.Text;
  }

  private void SearchToolStripTextBox_KeyPress(object sender, KeyPressEventArgs e)
  {
  }

  private void OpenInNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.OpenInNewWindow();
  }

  private void CardToolStripMenuItem_Click(object sender, EventArgs e) => this.Card();

  private void AddProductVersionsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddProductVersions();
  }

  private void AddToolStripMenuItem_Click(object sender, EventArgs e) => this.Add();

  private void AddWithCompostionToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddWithComposition();
  }

  private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e) => this.Replace();

  private void PasteToolStripMenuItem_Click(object sender, EventArgs e) => this.Paste();

  private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) => this.Remove();

  private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.RefreshItems(true);
  }

  private void ToggleLinkedEditingContextsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.ToggleLinkedEditingContexts();
  }

  private void CustomizeColumnsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.CustomizeColumns();
  }

  private void ApplyButton_Click(object sender, EventArgs e) => this.AcceptChanges();

  private void СancelButton_Click(object sender, EventArgs e) => this.CancelChanges();

  private void Tree_SelectionChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
    if (this.DesignMode)
      return;
    this._commandManager.Value.QueryStatus();
  }

  private void Tree_SortColumnChanged(object sender, EventArgs e)
  {
    Column sortColumn = this._tree.SortColumn;
    if (sortColumn == null)
      return;
    int result = 0;
    if (!string.IsNullOrEmpty(sortColumn.DataField))
      int.TryParse(sortColumn.DataField, out result);
    ListSortDirection sortDirection = sortColumn.SortDirection;
    if (AttributeTypeHelper.IsUnknownAttributeTypeID(result))
      return;
    this._editingContextRowBinding.ListSortDirection = sortDirection;
    this._editingContextRowBinding.SortAttributeTypeID = result;
  }

  private void Tree_GetContextMenuStrip(object sender, GetContextMenuStripEventArgs e)
  {
    e.ContextMenuStrip = this._contextMenuStrip;
  }

  private void Tree_GetAllowedRowDropLocations(object sender, GetAllowedRowDropLocationsEventArgs e)
  {
    if (!this._canEdit || this.GetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIdsFromDataObject(e.Data) == null || (!(e.Row.Item is EditingContext) || e.Row.Item != this._editingContext) && (!(e.Row.Item is EditingContextItem) || ((EditingContextItem) e.Row.Item).EditingContext != this._editingContext))
      return;
    e.AllowedDropLocations = RowDropLocation.BelowRow | RowDropLocation.OnRow;
  }

  private void Tree_RowDrop(object sender, RowDropEventArgs e)
  {
    if (!this._canEdit)
      return;
    IDBTypedObjectID[] idsFromDataObject = this.GetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIdsFromDataObject(e.Data);
    if (idsFromDataObject == null || (!(e.Row.Item is EditingContext) || e.Row.Item != this._editingContext) && (!(e.Row.Item is EditingContextItem) || ((EditingContextItem) e.Row.Item).EditingContext != this._editingContext))
      return;
    this.AddObjectsToEditingContext(((IEnumerable<IDBTypedObjectID>) idsFromDataObject).Select<IDBTypedObjectID, long>((System.Func<IDBTypedObjectID, long>) (o => o.ObjectID)).ToArray<long>(), AddObjectsToEditingContextType.Objects);
  }

  private void Tree_GetChildPolicy(object sender, GetChildPolicyEventArgs e)
  {
    if (!(e.Row is Row) || ((Row) e.Row).Item != this._editingContext)
      return;
    e.ChildPolicy = RowChildPolicy.AutoExpand;
  }

  private void EditingContextItems_ListChanged(object sender, ListChangedEventArgs e)
  {
    this.HasChanges = true;
  }

  private void NotificationService_NotificationFired(object sender, NotificationEventArgs e)
  {
    if (!this.IsDisposed)
    {
      if (ObjectHelper.IsUnknownObjectVersionID(this._editingContextVersionID))
        return;
      if ((e.EventName == "ProjectChanged" || e.EventName == "ObjectsChanged" && e is DBObjectsEventArgs && ((DBObjectsEventArgs) e).ObjectIDs.Contains(this._editingContextVersionID)) && !this.HasChanges)
      {
        this.RefreshItems();
      }
      else
      {
        if (!(e.EventName == "ObjectsCheckedIn") && !(e.EventName == "ObjectsCheckedOut") && !(e.EventName == "ObjectsChangesCancelled") || !(e is DBObjectsEventArgs) || !((DBObjectsEventArgs) e).ObjectIDs.Contains(this._editingContextVersionID) || !this.HasChanges)
          return;
        this.HasChanges = false;
      }
    }
    else
      this._notificationService.Value.Unsubscribe(new NotificationEventHandler(this.NotificationService_NotificationFired));
  }

  private void UpdateControls()
  {
    this._openInNewWindowToolStripButton.Enabled = this._openInNewWindowToolStripMenuItem.Enabled = this.CanOpenInNewWindow();
    this._cardToolStripButton.Enabled = this._cardToolStripMenuItem.Enabled = this.CanCard();
    this._addToolStripButton.Enabled = this._addToolStripMenuItem.Enabled = this.CanAdd();
    this._addWithCompositionToolStripButton.Enabled = this._addWithCompositionToolStripMenuItem.Enabled = this.CanAddWithComposition();
    this._addProductVersionsToolStripButton.Enabled = this._addProductVersionsToolStripMenuItem.Enabled = this.CanAddProductVersions();
    this._replaceToolStripButton.Enabled = this._replaceToolStripMenuItem.Enabled = this.CanReplace();
    this._pasteToolStripButton.Enabled = this._pasteToolStripMenuItem.Enabled = this.CanPaste();
    this._removeToolStripButton.Enabled = this._removeToolStripMenuItem.Enabled = this.CanRemove();
    this._toggleLinkedEditingContextsToolStripButton.Checked = this._toggleLinkedEditingContextsToolStripMenuItem.Checked = this._showLinkedEditingContexts;
    this._acceptButton.Enabled = this.CanApply();
    this._cancelButton.Enabled = this.CanCancel();
    this._objectsCountLabel.Text = this._editingContext != null ? this._editingContext.Items.Count.ToString() : "0";
  }

  private bool CanAddProductVersions() => this._canEdit;

  private void SetEditingContextVersionID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int[] array = this._navigatorColumnsTreeFeature.GetNodeColumnCollection().Select<NodeColumn, int>((System.Func<NodeColumn, int>) (o => o.Attribute.AttributeID)).ToArray<int>();
      IEditingContextServerService customService = sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService;
      EditingContext editingContext = customService.FindEditingContext(sessionKeeper.Session.SessionGUID, new FindEditingContextParams(this._editingContextVersionID)
      {
        AttributeTypeIds = array
      }) ?? new EditingContext(this._editingContextVersionID);
      this._editingContextVersionID = editingContext.Object.VersionID;
      if (this._editingContext != null)
        this._editingContext.Items.ListChanged -= new ListChangedEventHandler(this.EditingContextItems_ListChanged);
      this._editingContext = editingContext;
      if (this._editingContext != null)
        this._editingContext.Items.ListChanged += new ListChangedEventHandler(this.EditingContextItems_ListChanged);
      this._isLinkedEdintingContextLoaded = false;
      this._linkedEditingContexts = (EditingContext[]) null;
      this._editingContextAndLinkedEditingContexts = (EditingContext[]) null;
      this.SetTreeDataSource();
      this._canEdit = customService.CheckEditingContextEditRights(sessionKeeper.Session.SessionGUID, this._editingContextVersionID);
      this._messageControl.Visible = !this._canEdit;
      this.UpdateControls();
    }
  }

  private void SetTreeDataSource()
  {
    List<EditingContext> editingContextList = new List<EditingContext>();
    editingContextList.Add(this._editingContext);
    if (this._showLinkedEditingContexts)
      this.LoadLinkedEditingContexts();
    if (this._showLinkedEditingContexts && this._linkedEditingContexts != null)
      editingContextList.AddRange((IEnumerable<EditingContext>) this._linkedEditingContexts);
    this._editingContextAndLinkedEditingContexts = editingContextList.ToArray();
    this._editingContextRowBinding.Reset();
    this._tree.DataSource = (object) this._editingContextAndLinkedEditingContexts;
  }

  private void OpenInNewWindow()
  {
    object selectedItem = this._tree.SelectedItem;
    switch (selectedItem)
    {
      case EditingContext _:
        this.OpenInNewWindow(((EditingContext) selectedItem).Object.VersionID);
        break;
      case EditingContextItem _:
        this.OpenInNewWindow(((EditingContextItem) selectedItem).Object.VersionID);
        break;
    }
  }

  private void Card()
  {
    object selectedItem = this._tree.SelectedItem;
    switch (selectedItem)
    {
      case EditingContext _:
        this.ShowCard(((EditingContext) selectedItem).Object.VersionID);
        break;
      case EditingContextItem _:
        this.ShowCard(((EditingContextItem) selectedItem).Object.VersionID);
        break;
    }
  }

  private void AddProductVersions()
  {
    long[] array = this._editingContext.Items.Where<EditingContextItem>((System.Func<EditingContextItem, bool>) (o => this.IsDocumentObjectType(o.Object.TypeID))).Select<EditingContextItem, long>((System.Func<EditingContextItem, long>) (o => o.Object.VersionID)).ToArray<long>();
    long[] source = (long[]) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      source = (sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService).FindProductsForDocuments(sessionKeeper.Session.SessionGUID, array);
    this.AddObjectsToEditingContext(((IEnumerable<long>) source).ToArray<long>(), AddObjectsToEditingContextType.Objects);
  }

  private bool IsDocumentObjectType(int objectTypeID)
  {
    return ((IEnumerable<int>) EditingContextConstants.DocumentObjectTypesIds).Contains<int>(objectTypeID);
  }

  private void Add() => this.AddObjectsToEditingContext(AddObjectsToEditingContextType.Objects);

  private void AddWithComposition()
  {
    switch (EditingContextClientHelper.ShowSelectAddObjectsToEditingContextTypeDialog())
    {
      case DialogResult.Yes:
        this.AddObjectsToEditingContext(AddObjectsToEditingContextType.ObjectsWithRecursiveComposition);
        break;
      case DialogResult.No:
        this.AddObjectsToEditingContext(AddObjectsToEditingContextType.ObjectsWithComposition);
        break;
    }
  }

  private void AddObjectsToEditingContext(
    AddObjectsToEditingContextType addObjectsToEditingContextType)
  {
    this.AddObjectsToEditingContext(this.SelectObjectVersionIdsForAddToEditingContext(), addObjectsToEditingContextType);
  }

  private AddObjectsToEditingContextResult AddObjectsToEditingContext(
    long[] objectVersionIds,
    AddObjectsToEditingContextType addObjectsToEditingContextType)
  {
    if (objectVersionIds == null || objectVersionIds.Length == 0)
      return (AddObjectsToEditingContextResult) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IEditingContextServerService customService = sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService;
      int[] array = this._navigatorColumnsTreeFeature.GetNodeColumnCollection().Select<NodeColumn, int>((System.Func<NodeColumn, int>) (o => o.Attribute.AttributeID)).ToArray<int>();
      AddObjectsToEditingContextParams editingContextParams = new AddObjectsToEditingContextParams(this._editingContext.Object.VersionID)
      {
        ObjectVersionIds = objectVersionIds,
        Type = addObjectsToEditingContextType,
        AttributeTypeIds = array
      };
      this.LoadLinkedEditingContexts();
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      AddObjectsToEditingContextParams addObjectsToEditingContextParams = editingContextParams;
      AddObjectsToEditingContextResult editingContext = EditingContextsHelper.AddObjectsToEditingContext(((IEnumerable<_Object>) customService.FindObjectsForAddToEditingContext(sessionGuid, addObjectsToEditingContextParams)).ToArray<_Object>(), this._editingContext, ((IEnumerable<EditingContext>) this._linkedEditingContexts).ToArray<EditingContext>());
      EditingContextClientHelper.ShowAddObjectsToEditingContextResultDialog(editingContext);
      return editingContext;
    }
  }

  private void Replace()
  {
    EditingContextItem selectedItem = (EditingContextItem) this._tree.SelectedItem;
    long versionID = ObjectVersionSelection.SelectVersion(selectedItem.Object.ID, true, (List<long>) null, selectedItem.Object.VersionID);
    if (ObjectHelper.IsUnknownObjectVersionID(versionID))
      return;
    this._editingContext.Items.Remove(selectedItem);
    AddObjectsToEditingContextResult editingContext = this.AddObjectsToEditingContext(new long[1]
    {
      versionID
    }, AddObjectsToEditingContextType.Objects);
    if (editingContext == null || editingContext.SkippedObjectsCount <= 0)
      return;
    this._editingContext.Items.Add(selectedItem);
  }

  private void Paste()
  {
    this.AddObjectsToEditingContext(this.GetObjectVersionIdsFromClipboard(), AddObjectsToEditingContextType.Objects);
  }

  private void Remove()
  {
    IEnumerable<EditingContextItem> source = this._tree.SelectedItems.Cast<EditingContextItem>();
    EditingContextItem[] array1 = source.Where<EditingContextItem>((System.Func<EditingContextItem, bool>) (o => !ObjectTypeHelper.IsVersionedObjectTypeID(o.Object.TypeID))).ToArray<EditingContextItem>();
    EditingContextItem[] array2 = source.Where<EditingContextItem>((System.Func<EditingContextItem, bool>) (o => ObjectTypeHelper.IsVersionedObjectTypeID(o.Object.TypeID))).ToArray<EditingContextItem>();
    if (MessageBox.Show("Удалить версию(-и) из контекста?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    foreach (EditingContextItem editingContextItem in array2)
      this._editingContext.Items.Remove(editingContextItem);
    if (array1.Length == 0)
      return;
    int num = (int) MessageBox.Show($"Объекты {string.Join(", ", ((IEnumerable<EditingContextItem>) array1).Select<EditingContextItem, string>((System.Func<EditingContextItem, string>) (o => $"#{o.Object.VersionID} {o.Object.Caption}")))} не могут быть удалены из состава контекста редактирования, т.к. являются неверсионными.{Environment.NewLine}Для их удаления воспользуйтесь операцией чисти базы данных.", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  private void ToggleLinkedEditingContexts()
  {
    this._showLinkedEditingContexts = !this._showLinkedEditingContexts;
    this.SetTreeDataSource();
    this.UpdateControls();
  }

  private void LoadLinkedEditingContexts()
  {
    if (this._isLinkedEdintingContextLoaded)
      return;
    int[] array = this._navigatorColumnsTreeFeature.GetNodeColumnCollection().Select<NodeColumn, int>((System.Func<NodeColumn, int>) (o => o.Attribute.AttributeID)).ToArray<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._linkedEditingContexts = (sessionKeeper.Session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService).FindLinkedEdintingContexts(sessionKeeper.Session.SessionGUID, new FindEditingContextParams(this._editingContextVersionID)
      {
        AttributeTypeIds = array
      });
    this._isLinkedEdintingContextLoaded = true;
  }

  private void CustomizeColumns()
  {
    this._navigatorColumnsTreeFeature.ShowColumnCustomizeDialog(Intermech.Navigator.Utils.GetObjectsColumnsOnly());
    this.RefreshItems();
  }

  private void RefreshItems(bool showMessage = false)
  {
    if (this.HasChanges & showMessage && MessageBox.Show((IWin32Window) this, "Состав контекста редактирования был изменен. Выполнение обновления приведет к отмене изменений. Обновить?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    this.SetEditingContextVersionID();
    this.HasChanges = false;
  }

  private long[] SelectObjectVersionIdsForAddToEditingContext()
  {
    ServiceContainer nodesContext = new ServiceContainer();
    ObjectTypeNodeFilter serviceInstance = new ObjectTypeNodeFilter();
    serviceInstance.EnabledObjectTypes.AddRange((IEnumerable<int>) this.GetObjectTypeIdsAllowableForAddToEditingContext(-2));
    nodesContext.AddService(typeof (IObjectTypeNodeFilter), (object) serviceInstance);
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ContextObjectsSelectedItemsAnalyzer(), true);
    return Intermech.Navigator.SelectionWindow.SelectObjects("Intermech Professional Solution", "Выберите объекты для добавления в состав контекста редактирования", (IDescriptor) new ObjectTypesDescriptor(this.GetObjectTypeIdsAllowableForAddToEditingContext(-1), LocalizationHolder.rm.GetString("Client.Core_283")), (System.IServiceProvider) nodesContext, SelectionOptions.SelectObjects | SelectionOptions.ForceFilterObjectsByRule) ?? new long[0];
  }

  private int[] GetObjectTypeIdsAllowableForAddToEditingContext(int mode)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectTypeCollection(mode, true).Select("F_OBJECT_TYPE").Rows.Cast<DataRow>().Select<DataRow, int>((System.Func<DataRow, int>) (o => DataSetProcessor.GetInt32Value(o, "F_OBJECT_TYPE", -1))).Where<int>((System.Func<int, bool>) (o => MetaDataHelper.CanAddObjTypeToEditingContext(o, false))).Distinct<int>().ToArray<int>();
  }

  private long[] GetObjectVersionIdsFromClipboard()
  {
    List<long> longList = new List<long>();
    object dataObject = this._clipboard.Value.GetDataObject();
    if (!(dataObject is DBObjectTypedIDCollection))
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1448"), "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      foreach (IDBTypedObjectID typedObject in ((IDBObjectTypedIDCollection) dataObject).GetTypedObjects())
        longList.Add(typedObject.ObjectID);
    }
    return longList.ToArray();
  }

  private void OpenInNewWindow(long objectVersionID)
  {
    Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectVersionID), (System.IServiceProvider) ServicesManager.ServiceContainer);
  }

  private void ShowCard(long objectVersionID)
  {
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, objectVersionID, true);
  }

  private bool CanOpenInNewWindow() => this.IsInTreeSelectedEditingContextOrEditingContextItem();

  private bool CanCard() => this.IsInTreeSelectedEditingContextOrEditingContextItem();

  private bool CanAdd() => this._canEdit;

  private bool CanAddWithComposition() => this._canEdit;

  private bool CanPaste() => this._canEdit;

  private bool CanReplace() => this.IsInTreeSelectedEditableEditingContextItem() && this._canEdit;

  private bool CanRemove() => this.IsInTreeSelectedEditableEditingContextItems() && this._canEdit;

  private bool CanApply() => this.HasChanges && this._canEdit;

  private bool CanCancel() => this.HasChanges && this._canEdit;

  private bool IsInTreeSelectedEditingContextOrEditingContextItem()
  {
    if (this._tree.SelectedRows.Count != 1)
      return false;
    return this._tree.SelectedItem is EditingContext || this._tree.SelectedItem is EditingContextItem;
  }

  private bool IsInTreeSelectedEditableEditingContextOrEditableEditingContextItem()
  {
    if (this._tree.SelectedRows.Count != 1)
      return false;
    if (this._tree.SelectedItem is EditingContext && ((EditingContext) this._tree.SelectedItem).Object.VersionID == this._editingContextVersionID)
      return true;
    return this._tree.SelectedItem is EditingContextItem && this.IsEditableEditingContextItem((EditingContextItem) this._tree.SelectedItem);
  }

  private bool IsInTreeSelectedEditableEditingContextItem()
  {
    return this._tree.SelectedRows.Count == 1 && this._tree.SelectedItem is EditingContextItem && this.IsEditableEditingContextItem((EditingContextItem) this._tree.SelectedItem);
  }

  private bool IsInTreeSelectedEditableEditingContextItems()
  {
    return this._tree.SelectedRows.Count > 0 && this._tree.SelectedRows.Where<Row>((System.Func<Row, bool>) (o => !(o.Item is EditingContextItem) || !this.IsEditableEditingContextItem((EditingContextItem) o.Item))).Count<Row>() == 0;
  }

  private bool IsEditableEditingContextItem(EditingContextItem item)
  {
    return item.EditingContext == this._editingContext && !item.ReadOnly;
  }

  private Image GetImageFromNamedImageList(string imageKey)
  {
    int index = this._namedImageList.Value.ImageIndex(imageKey);
    return index <= 0 ? (Image) null : this._namedImageList.Value.ImageList.Images[index];
  }

  private Image GetImage16x16FromCategoryTypeIconService(int categoryID, int typeID)
  {
    int index = this._categoryTypeIconService.Value.IndexOf(categoryID, typeID);
    Image image = index > 0 ? this._categoryTypeIconService.Value.ImageList.Images[index] : (Image) null;
    if (image == null)
      return (Image) null;
    Bitmap categoryTypeIconService = new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    using (Graphics graphics = Graphics.FromImage((Image) categoryTypeIconService))
      graphics.DrawImage(image, 0, 0);
    return (Image) categoryTypeIconService;
  }

  private IDBTypedObjectID[] GetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIdsFromDataObject(
    IDataObject dataObject)
  {
    IDBTypedObjectID[] typedObjectIds = (IDBTypedObjectID[]) null;
    if (dataObject.GetDataPresent(typeof (IOSource)) && dataObject.GetData(typeof (IOSource)) is IOSource data && data.SelectedItems != null)
      SelectedItemsHelper.TryGetTypedObjectIdsWithObjectVersionIdsAndObjectTypeIds(data.SelectedItems, out typedObjectIds);
    return typedObjectIds ?? new IDBTypedObjectID[0];
  }

  private NodeColumnCollection GetDefaultNodeColumnCollection()
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection columnCollection = new NodeColumnCollection();
    NodeColumn column1 = service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID, NodeColumnSortOrder.Ascending, 0);
    columnCollection.Add(column1, 150);
    NodeColumn column2 = service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, NodeColumnSortOrder.Ascending, 0);
    columnCollection.Add(column2, 250);
    NodeColumn column3 = service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE, NodeColumnSortOrder.Ascending, 0);
    columnCollection.Add(column3, 250);
    NodeColumn column4 = service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_LC_STEP, NodeColumnSortOrder.Ascending, 0);
    columnCollection.Add(column4, 250);
    NodeColumn column5 = service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_CHKOUT_BY, NodeColumnSortOrder.Ascending, 0);
    columnCollection.Add(column5, 150);
    return columnCollection;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._notificationService != null)
        this._notificationService.Value.Unsubscribe(new NotificationEventHandler(this.NotificationService_NotificationFired));
      this._commandManager.Value.RemoveTarget((ICommandTarget) this._editingContextEditorCommandTarget);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditingContextEditorControl));
    this.toolStrip1 = new ToolStrip();
    this._openInNewWindowToolStripButton = new ToolStripButton();
    this._cardToolStripButton = new ToolStripButton();
    this._addProductVersionsToolStripButton = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._addToolStripButton = new ToolStripButton();
    this._addWithCompositionToolStripButton = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this._replaceToolStripButton = new ToolStripButton();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this._pasteToolStripButton = new ToolStripButton();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this._removeToolStripButton = new ToolStripButton();
    this.toolStripSeparator5 = new ToolStripSeparator();
    this._toggleLinkedEditingContextsToolStripButton = new ToolStripButton();
    this.toolStripSeparator7 = new ToolStripSeparator();
    this._customizeColumnsToolStripButton = new ToolStripButton();
    this.toolStripSeparator12 = new ToolStripSeparator();
    this._refreshToolStripButton = new ToolStripButton();
    this.toolStripSeparator6 = new ToolStripSeparator();
    this.toolStripLabel1 = new ToolStripLabel();
    this._searchToolStripTextBox = new ToolStripTextBox();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this._contextMenuStrip = new ContextMenuStrip(this.components);
    this._openInNewWindowToolStripMenuItem = new ToolStripMenuItem();
    this._cardToolStripMenuItem = new ToolStripMenuItem();
    this._addProductVersionsToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator8 = new ToolStripSeparator();
    this._addToolStripMenuItem = new ToolStripMenuItem();
    this._addWithCompositionToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator9 = new ToolStripSeparator();
    this._replaceToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator10 = new ToolStripSeparator();
    this._pasteToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator11 = new ToolStripSeparator();
    this._removeToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator13 = new ToolStripSeparator();
    this._toggleLinkedEditingContextsToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator14 = new ToolStripSeparator();
    this._customizeColumnsToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator15 = new ToolStripSeparator();
    this._refreshToolStripMenuItem = new ToolStripMenuItem();
    this.panel1 = new Panel();
    this._objectsCountLabel = new Label();
    this.label1 = new Label();
    this.panel2 = new Panel();
    this._tree = new Intermech.Search.UI.VirtualTree.VirtualTree();
    this._messageControl = new MessageControl();
    this.toolStrip1.SuspendLayout();
    this._contextMenuStrip.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this._tree.BeginInit();
    this.SuspendLayout();
    this.toolStrip1.Items.AddRange(new ToolStripItem[21]
    {
      (ToolStripItem) this._openInNewWindowToolStripButton,
      (ToolStripItem) this._cardToolStripButton,
      (ToolStripItem) this._addProductVersionsToolStripButton,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._addToolStripButton,
      (ToolStripItem) this._addWithCompositionToolStripButton,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this._replaceToolStripButton,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this._pasteToolStripButton,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this._removeToolStripButton,
      (ToolStripItem) this.toolStripSeparator5,
      (ToolStripItem) this._toggleLinkedEditingContextsToolStripButton,
      (ToolStripItem) this.toolStripSeparator7,
      (ToolStripItem) this._customizeColumnsToolStripButton,
      (ToolStripItem) this.toolStripSeparator12,
      (ToolStripItem) this._refreshToolStripButton,
      (ToolStripItem) this.toolStripSeparator6,
      (ToolStripItem) this.toolStripLabel1,
      (ToolStripItem) this._searchToolStripTextBox
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(873, 25);
    this.toolStrip1.TabIndex = 1;
    this.toolStrip1.Text = "toolStrip1";
    this._openInNewWindowToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._openInNewWindowToolStripButton.Image = (Image) componentResourceManager.GetObject("_openInNewWindowToolStripButton.Image");
    this._openInNewWindowToolStripButton.ImageTransparentColor = Color.Magenta;
    this._openInNewWindowToolStripButton.Name = "_openInNewWindowToolStripButton";
    this._openInNewWindowToolStripButton.Size = new Size(23, 22);
    this._openInNewWindowToolStripButton.Text = "Открыть в новом окне";
    this._openInNewWindowToolStripButton.Click += new EventHandler(this.OpenInNewWindowToolStripButton_Click);
    this._cardToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._cardToolStripButton.Image = (Image) componentResourceManager.GetObject("_cardToolStripButton.Image");
    this._cardToolStripButton.ImageTransparentColor = Color.Magenta;
    this._cardToolStripButton.Name = "_cardToolStripButton";
    this._cardToolStripButton.Size = new Size(23, 22);
    this._cardToolStripButton.Text = "Карточка";
    this._cardToolStripButton.Click += new EventHandler(this.CardToolStripButton_Click);
    this._addProductVersionsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addProductVersionsToolStripButton.Image = (Image) componentResourceManager.GetObject("_addProductVersionsToolStripButton.Image");
    this._addProductVersionsToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addProductVersionsToolStripButton.Name = "_addProductVersionsToolStripButton";
    this._addProductVersionsToolStripButton.Size = new Size(23, 22);
    this._addProductVersionsToolStripButton.Text = "Включить версии изделий в контекст";
    this._addProductVersionsToolStripButton.Click += new EventHandler(this.AddProductVersionsToolStripButton_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this._addToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addToolStripButton.Image = (Image) Intermech.Client.Core.Properties.Resources.AddStandart;
    this._addToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addToolStripButton.Name = "_addToolStripButton";
    this._addToolStripButton.Size = new Size(23, 22);
    this._addToolStripButton.Text = "Добавить";
    this._addToolStripButton.Click += new EventHandler(this.AddToolStripButton_Click);
    this._addWithCompositionToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addWithCompositionToolStripButton.Image = (Image) Intermech.Client.Core.Properties.Resources.AddFile;
    this._addWithCompositionToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addWithCompositionToolStripButton.Name = "_addWithCompositionToolStripButton";
    this._addWithCompositionToolStripButton.Size = new Size(23, 22);
    this._addWithCompositionToolStripButton.Text = "Добавить с составом";
    this._addWithCompositionToolStripButton.Click += new EventHandler(this.AddWithCompositionToolStripButton_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(6, 25);
    this._replaceToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._replaceToolStripButton.Image = (Image) componentResourceManager.GetObject("_replaceToolStripButton.Image");
    this._replaceToolStripButton.ImageTransparentColor = Color.Magenta;
    this._replaceToolStripButton.Name = "_replaceToolStripButton";
    this._replaceToolStripButton.Size = new Size(23, 22);
    this._replaceToolStripButton.Text = "Заменить";
    this._replaceToolStripButton.Click += new EventHandler(this.ReplaceToolStripButton_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    this.toolStripSeparator3.Size = new Size(6, 25);
    this._pasteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._pasteToolStripButton.Image = (Image) componentResourceManager.GetObject("_pasteToolStripButton.Image");
    this._pasteToolStripButton.ImageTransparentColor = Color.Magenta;
    this._pasteToolStripButton.Name = "_pasteToolStripButton";
    this._pasteToolStripButton.Size = new Size(23, 22);
    this._pasteToolStripButton.Text = "Вставить";
    this._pasteToolStripButton.Click += new EventHandler(this.PasteToolStripButton_Click);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    this.toolStripSeparator4.Size = new Size(6, 25);
    this._removeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeToolStripButton.Image = (Image) Intermech.Client.Core.Properties.Resources.DeleteStandart;
    this._removeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeToolStripButton.Name = "_removeToolStripButton";
    this._removeToolStripButton.Size = new Size(23, 22);
    this._removeToolStripButton.Text = "Удалить";
    this._removeToolStripButton.Click += new EventHandler(this.RemoveToolStripButton_Click);
    this.toolStripSeparator5.Name = "toolStripSeparator5";
    this.toolStripSeparator5.Size = new Size(6, 25);
    this._toggleLinkedEditingContextsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._toggleLinkedEditingContextsToolStripButton.Image = (Image) componentResourceManager.GetObject("_toggleLinkedEditingContextsToolStripButton.Image");
    this._toggleLinkedEditingContextsToolStripButton.ImageTransparentColor = Color.Magenta;
    this._toggleLinkedEditingContextsToolStripButton.Name = "_toggleLinkedEditingContextsToolStripButton";
    this._toggleLinkedEditingContextsToolStripButton.Size = new Size(23, 22);
    this._toggleLinkedEditingContextsToolStripButton.Text = "Отображать связанные контексты";
    this._toggleLinkedEditingContextsToolStripButton.Click += new EventHandler(this.ToggleLinkedEditingContextsToolStripButton_Click);
    this.toolStripSeparator7.Name = "toolStripSeparator7";
    this.toolStripSeparator7.Size = new Size(6, 25);
    this._customizeColumnsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._customizeColumnsToolStripButton.Image = (Image) componentResourceManager.GetObject("_customizeColumnsToolStripButton.Image");
    this._customizeColumnsToolStripButton.ImageTransparentColor = Color.Magenta;
    this._customizeColumnsToolStripButton.Name = "_customizeColumnsToolStripButton";
    this._customizeColumnsToolStripButton.Size = new Size(23, 22);
    this._customizeColumnsToolStripButton.Text = "Настроить колонки";
    this._customizeColumnsToolStripButton.Click += new EventHandler(this.СustomizeColumnsToolStripButton_Click);
    this.toolStripSeparator12.Name = "toolStripSeparator12";
    this.toolStripSeparator12.Size = new Size(6, 25);
    this._refreshToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._refreshToolStripButton.Image = (Image) componentResourceManager.GetObject("_refreshToolStripButton.Image");
    this._refreshToolStripButton.ImageTransparentColor = Color.Magenta;
    this._refreshToolStripButton.Name = "_refreshToolStripButton";
    this._refreshToolStripButton.Size = new Size(23, 22);
    this._refreshToolStripButton.Text = "Обновить";
    this._refreshToolStripButton.ToolTipText = "Обновить";
    this._refreshToolStripButton.Click += new EventHandler(this.RefreshToolStripButton_Click);
    this.toolStripSeparator6.Name = "toolStripSeparator6";
    this.toolStripSeparator6.Size = new Size(6, 25);
    this.toolStripLabel1.Name = "toolStripLabel1";
    this.toolStripLabel1.Size = new Size(48 /*0x30*/, 22);
    this.toolStripLabel1.Text = "Искать:";
    this._searchToolStripTextBox.Name = "_searchToolStripTextBox";
    this._searchToolStripTextBox.Size = new Size(250, 25);
    this._searchToolStripTextBox.Leave += new EventHandler(this.SearchToolStripTextBox_Leave);
    this._searchToolStripTextBox.KeyPress += new KeyPressEventHandler(this.SearchToolStripTextBox_KeyPress);
    this._searchToolStripTextBox.KeyUp += new KeyEventHandler(this.SearchToolStripTextBox_KeyUp);
    this._cancelButton.Anchor = AnchorStyles.Right;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Enabled = false;
    this._cancelButton.Location = new Point(793, 6);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 2;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._cancelButton.Click += new EventHandler(this.СancelButton_Click);
    this._acceptButton.Anchor = AnchorStyles.Right;
    this._acceptButton.DialogResult = DialogResult.OK;
    this._acceptButton.Enabled = false;
    this._acceptButton.Location = new Point(712, 6);
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Size = new Size(75, 23);
    this._acceptButton.TabIndex = 3;
    this._acceptButton.Text = "ОК";
    this._acceptButton.UseVisualStyleBackColor = true;
    this._acceptButton.Click += new EventHandler(this.ApplyButton_Click);
    this._contextMenuStrip.Items.AddRange(new ToolStripItem[18]
    {
      (ToolStripItem) this._openInNewWindowToolStripMenuItem,
      (ToolStripItem) this._cardToolStripMenuItem,
      (ToolStripItem) this._addProductVersionsToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator8,
      (ToolStripItem) this._addToolStripMenuItem,
      (ToolStripItem) this._addWithCompositionToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator9,
      (ToolStripItem) this._replaceToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator10,
      (ToolStripItem) this._pasteToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator11,
      (ToolStripItem) this._removeToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator13,
      (ToolStripItem) this._toggleLinkedEditingContextsToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator14,
      (ToolStripItem) this._customizeColumnsToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator15,
      (ToolStripItem) this._refreshToolStripMenuItem
    });
    this._contextMenuStrip.Name = "_contextMenuStrip";
    this._contextMenuStrip.Size = new Size(280, 310);
    this._openInNewWindowToolStripMenuItem.Name = "_openInNewWindowToolStripMenuItem";
    this._openInNewWindowToolStripMenuItem.Size = new Size(279, 22);
    this._openInNewWindowToolStripMenuItem.Text = "Открыть в новом окне";
    this._openInNewWindowToolStripMenuItem.Click += new EventHandler(this.OpenInNewWindowToolStripMenuItem_Click);
    this._cardToolStripMenuItem.Name = "_cardToolStripMenuItem";
    this._cardToolStripMenuItem.Size = new Size(279, 22);
    this._cardToolStripMenuItem.Text = "Карточка";
    this._cardToolStripMenuItem.Click += new EventHandler(this.CardToolStripMenuItem_Click);
    this._addProductVersionsToolStripMenuItem.Name = "_addProductVersionsToolStripMenuItem";
    this._addProductVersionsToolStripMenuItem.Size = new Size(279, 22);
    this._addProductVersionsToolStripMenuItem.Text = "Включить версии изделий в контекст";
    this._addProductVersionsToolStripMenuItem.Click += new EventHandler(this.AddProductVersionsToolStripMenuItem_Click);
    this.toolStripSeparator8.Name = "toolStripSeparator8";
    this.toolStripSeparator8.Size = new Size(276, 6);
    this._addToolStripMenuItem.Image = (Image) Intermech.Client.Core.Properties.Resources.AddStandart;
    this._addToolStripMenuItem.Name = "_addToolStripMenuItem";
    this._addToolStripMenuItem.Size = new Size(279, 22);
    this._addToolStripMenuItem.Text = "Добавить";
    this._addToolStripMenuItem.Click += new EventHandler(this.AddToolStripMenuItem_Click);
    this._addWithCompositionToolStripMenuItem.Image = (Image) Intermech.Client.Core.Properties.Resources.AddFile;
    this._addWithCompositionToolStripMenuItem.ImageTransparentColor = Color.Magenta;
    this._addWithCompositionToolStripMenuItem.Name = "_addWithCompositionToolStripMenuItem";
    this._addWithCompositionToolStripMenuItem.Size = new Size(279, 22);
    this._addWithCompositionToolStripMenuItem.Text = "Добавить с составом";
    this._addWithCompositionToolStripMenuItem.Click += new EventHandler(this.AddWithCompostionToolStripMenuItem_Click);
    this.toolStripSeparator9.Name = "toolStripSeparator9";
    this.toolStripSeparator9.Size = new Size(276, 6);
    this._replaceToolStripMenuItem.Name = "_replaceToolStripMenuItem";
    this._replaceToolStripMenuItem.Size = new Size(279, 22);
    this._replaceToolStripMenuItem.Text = "Заменить";
    this._replaceToolStripMenuItem.Click += new EventHandler(this.ReplaceToolStripMenuItem_Click);
    this.toolStripSeparator10.Name = "toolStripSeparator10";
    this.toolStripSeparator10.Size = new Size(276, 6);
    this._pasteToolStripMenuItem.Name = "_pasteToolStripMenuItem";
    this._pasteToolStripMenuItem.Size = new Size(279, 22);
    this._pasteToolStripMenuItem.Text = "Вставить";
    this._pasteToolStripMenuItem.Click += new EventHandler(this.PasteToolStripMenuItem_Click);
    this.toolStripSeparator11.Name = "toolStripSeparator11";
    this.toolStripSeparator11.Size = new Size(276, 6);
    this._removeToolStripMenuItem.Image = (Image) Intermech.Client.Core.Properties.Resources.DeleteStandart;
    this._removeToolStripMenuItem.Name = "_removeToolStripMenuItem";
    this._removeToolStripMenuItem.Size = new Size(279, 22);
    this._removeToolStripMenuItem.Text = "Удалить";
    this._removeToolStripMenuItem.Click += new EventHandler(this.RemoveToolStripMenuItem_Click);
    this.toolStripSeparator13.Name = "toolStripSeparator13";
    this.toolStripSeparator13.Size = new Size(276, 6);
    this._toggleLinkedEditingContextsToolStripMenuItem.Name = "_toggleLinkedEditingContextsToolStripMenuItem";
    this._toggleLinkedEditingContextsToolStripMenuItem.Size = new Size(279, 22);
    this._toggleLinkedEditingContextsToolStripMenuItem.Text = "Отображать связанные контексты";
    this._toggleLinkedEditingContextsToolStripMenuItem.Click += new EventHandler(this.ToggleLinkedEditingContextsToolStripMenuItem_Click);
    this.toolStripSeparator14.Name = "toolStripSeparator14";
    this.toolStripSeparator14.Size = new Size(276, 6);
    this._customizeColumnsToolStripMenuItem.Name = "_customizeColumnsToolStripMenuItem";
    this._customizeColumnsToolStripMenuItem.Size = new Size(279, 22);
    this._customizeColumnsToolStripMenuItem.Text = "Настроить колонки";
    this._customizeColumnsToolStripMenuItem.Click += new EventHandler(this.CustomizeColumnsToolStripMenuItem_Click);
    this.toolStripSeparator15.Name = "toolStripSeparator15";
    this.toolStripSeparator15.Size = new Size(276, 6);
    this._refreshToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_refreshToolStripMenuItem.Image");
    this._refreshToolStripMenuItem.ImageTransparentColor = Color.Magenta;
    this._refreshToolStripMenuItem.Name = "_refreshToolStripMenuItem";
    this._refreshToolStripMenuItem.Size = new Size(279, 22);
    this._refreshToolStripMenuItem.Text = "Обновить";
    this._refreshToolStripMenuItem.ToolTipText = "Обновить";
    this._refreshToolStripMenuItem.Click += new EventHandler(this.RefreshToolStripMenuItem_Click);
    this.panel1.Controls.Add((Control) this._objectsCountLabel);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this._cancelButton);
    this.panel1.Controls.Add((Control) this._acceptButton);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 413);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(873, 36);
    this.panel1.TabIndex = 4;
    this._objectsCountLabel.AutoSize = true;
    this._objectsCountLabel.Location = new Point(62, 11);
    this._objectsCountLabel.Name = "_objectsCountLabel";
    this._objectsCountLabel.Size = new Size(35, 13);
    this._objectsCountLabel.TabIndex = 5;
    this._objectsCountLabel.Text = "label2";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(3, 11);
    this.label1.Name = "label1";
    this.label1.Size = new Size(63 /*0x3F*/, 13);
    this.label1.TabIndex = 4;
    this.label1.Text = "Объектов: ";
    this.panel2.BackColor = SystemColors.Control;
    this.panel2.Controls.Add((Control) this._tree);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(0, 79);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(873, 334);
    this.panel2.TabIndex = 6;
    this._tree.AllowDrop = true;
    this._tree.Dock = DockStyle.Fill;
    this._tree.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._tree.IconWidth = 32 /*0x20*/;
    this._tree.ImageList = (ImageList) null;
    this._tree.LineStyle = LineStyle.Dot;
    this._tree.Location = new Point(0, 0);
    this._tree.Name = "_tree";
    this._tree.RowEvenStyle.BorderStyle = Border3DStyle.Adjust;
    this._tree.RowEvenStyle.BorderWidth = 1;
    this._tree.RowOddStyle.BorderStyle = Border3DStyle.Adjust;
    this._tree.RowOddStyle.BorderWidth = 1;
    this._tree.ShowRootRow = false;
    this._tree.Size = new Size(873, 334);
    this._tree.TabIndex = 0;
    this._tree.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.Tree_GetAllowedRowDropLocations);
    this._tree.GetChildPolicy += new GetChildPolicyHandler(this.Tree_GetChildPolicy);
    this._tree.GetContextMenuStrip += new GetContextMenuStripHandler(this.Tree_GetContextMenuStrip);
    this._tree.RowDrop += new RowDropHandler(this.Tree_RowDrop);
    this._tree.SelectionChanged += new EventHandler(this.Tree_SelectionChanged);
    this._tree.SortColumnChanged += new EventHandler(this.Tree_SortColumnChanged);
    this._messageControl.BackColor = Color.LightYellow;
    this._messageControl.BorderStyle = BorderStyle.FixedSingle;
    this._messageControl.Dock = DockStyle.Top;
    this._messageControl.Location = new Point(0, 25);
    this._messageControl.Name = "_messageControl";
    this._messageControl.Size = new Size(873, 54);
    this._messageControl.TabIndex = 5;
    this._messageControl.Text = "Внимание! Редактирование контекста редактирования запрещено.\r\nУбедитесь, что у вас  есть права на изменение контекста редактирования и он не взят на изменение другим пользователем.";
    this._messageControl.Type = _MessageType.Warning;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this._messageControl);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.toolStrip1);
    this.Name = nameof (EditingContextEditorControl);
    this.Size = new Size(873, 449);
    this.Enter += new EventHandler(this.EditingContextEditorControl_Enter);
    this.Leave += new EventHandler(this.EditingContextEditorControl_Leave);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this._contextMenuStrip.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this._tree.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class EditingContextRowBinding : ObjectHolderRowBinding
  {
    private Dictionary<long, EditingContextEditorControl.EditingContextItemCollectionAdapter> _editingContextItemCollectionAdapterDictionary = new Dictionary<long, EditingContextEditorControl.EditingContextItemCollectionAdapter>();
    private int _sortAttributeTypeID;
    private ListSortDirection _listSortDirection;
    private string _searchString;

    public EditingContextRowBinding()
      : base(typeof (EditingContext))
    {
      this.AllowDropBelowRow = true;
      this.AllowDropOnRow = true;
    }

    public int SortAttributeTypeID
    {
      get => this._sortAttributeTypeID;
      set
      {
        if (this._sortAttributeTypeID == value)
          return;
        this._sortAttributeTypeID = value;
        this.SetSortAttributeTypeID();
      }
    }

    public ListSortDirection ListSortDirection
    {
      get => this._listSortDirection;
      set
      {
        if (this._listSortDirection == value)
          return;
        this._listSortDirection = value;
        this.SetListSortDirection();
      }
    }

    public string SearchString
    {
      get => this._searchString;
      set
      {
        if (!(this._searchString != value))
          return;
        this._searchString = value;
        this.SetSearchString();
      }
    }

    public void Reset()
    {
      this._searchString = string.Empty;
      this._editingContextItemCollectionAdapterDictionary.Clear();
    }

    public override IList GetChildrenForRow(Row row)
    {
      EditingContext editingContext = row != null ? (EditingContext) row.Item : throw new ArgumentNullException(nameof (row));
      EditingContextEditorControl.EditingContextItemCollectionAdapter childrenForRow = (EditingContextEditorControl.EditingContextItemCollectionAdapter) null;
      if (!this._editingContextItemCollectionAdapterDictionary.TryGetValue(editingContext.Object.VersionID, out childrenForRow))
      {
        childrenForRow = new EditingContextEditorControl.EditingContextItemCollectionAdapter(editingContext.Items);
        this._editingContextItemCollectionAdapterDictionary[editingContext.Object.VersionID] = childrenForRow;
      }
      return (IList) childrenForRow;
    }

    private void SetSortAttributeTypeID()
    {
      this.ResetEditingContextItemCollectionAdapterProperties();
    }

    private void SetListSortDirection()
    {
      this.ResetEditingContextItemCollectionAdapterProperties();
    }

    private void SetSearchString() => this.ResetEditingContextItemCollectionAdapterProperties();

    private void ResetEditingContextItemCollectionAdapterProperties()
    {
      foreach (KeyValuePair<long, EditingContextEditorControl.EditingContextItemCollectionAdapter> collectionAdapter1 in this._editingContextItemCollectionAdapterDictionary)
      {
        EditingContextEditorControl.EditingContextItemCollectionAdapter collectionAdapter2 = collectionAdapter1.Value;
        collectionAdapter2.ListSortDirection = this.ListSortDirection;
        collectionAdapter2.SearchString = this.SearchString;
        collectionAdapter2.SortAttributeTypeID = this.SortAttributeTypeID;
      }
    }
  }

  private sealed class EditingContextItemRowBinding : ObjectHolderRowBinding
  {
    public EditingContextItemRowBinding()
      : base(typeof (EditingContextItem))
    {
      this.AllowDropBelowRow = true;
      this.AllowDropOnRow = true;
    }

    public override void GetCellData(Row row, Column column, CellData cellData)
    {
      if (row == null)
        throw new ArgumentNullException(nameof (row));
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (cellData == null)
        throw new ArgumentNullException(nameof (cellData));
      base.GetCellData(row, column, cellData);
      if (!(row.Item is EditingContextItem editingContextItem) || !editingContextItem.ReadOnly)
        return;
      cellData.EvenStyle = new Style(cellData.EvenStyle, new StyleDelta()
      {
        ForeColor = Color.Gray
      });
      cellData.OddStyle = new Style(cellData.OddStyle, new StyleDelta()
      {
        ForeColor = Color.Gray
      });
    }
  }

  private sealed class EditingContextItemCollectionAdapter : BindingListBase<EditingContextItem>
  {
    private EditingContextItemCollection _editingContextItemCollection;
    private int _sortAttributeTypeID;
    private ListSortDirection _listSortDirection;
    private string _searchString;

    public EditingContextItemCollectionAdapter(
      EditingContextItemCollection editingContextItemCollection)
    {
      this._editingContextItemCollection = editingContextItemCollection != null ? editingContextItemCollection : throw new ArgumentNullException(nameof (editingContextItemCollection));
      this._editingContextItemCollection.ListChanged += new ListChangedEventHandler(this.EditingContextItemCollection_ListChanged);
      this.ResetItems();
    }

    public int SortAttributeTypeID
    {
      get => this._sortAttributeTypeID;
      set
      {
        if (this._sortAttributeTypeID == value)
          return;
        this._sortAttributeTypeID = value;
        this.SetSortAttributeTypeID();
      }
    }

    public ListSortDirection ListSortDirection
    {
      get => this._listSortDirection;
      set
      {
        if (this._listSortDirection == value)
          return;
        this._listSortDirection = value;
        this.SetListSortDirection();
      }
    }

    public string SearchString
    {
      get => this._searchString;
      set
      {
        if (!(this._searchString != value))
          return;
        this._searchString = value;
        this.SetSearchString();
      }
    }

    private void EditingContextItemCollection_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (e.ListChangedType != ListChangedType.ItemAdded && e.ListChangedType != ListChangedType.ItemDeleted && e.ListChangedType != ListChangedType.ItemMoved && e.ListChangedType != ListChangedType.Reset)
        return;
      this.ResetItems();
    }

    private void SetSortAttributeTypeID() => this.ResetItems();

    private void SetListSortDirection() => this.ResetItems();

    private void SetSearchString() => this.ResetItems();

    private void ResetItems()
    {
      int num = this.RaiseListChangedEvents ? 1 : 0;
      this.RaiseListChangedEvents = false;
      try
      {
        IEnumerable<EditingContextItem> editingContextItems = (IEnumerable<EditingContextItem>) this._editingContextItemCollection;
        if (!string.IsNullOrEmpty(this.SearchString))
          editingContextItems = editingContextItems.Where<EditingContextItem>((System.Func<EditingContextItem, bool>) (o => o.Object.Attributes.Where<_Attribute>((System.Func<_Attribute, bool>) (oo => oo.Value != null && oo.Value.ToString().ToLowerInvariant().Contains(this.SearchString.ToLowerInvariant()))).Count<_Attribute>() > 0));
        if (!AttributeTypeHelper.IsUnknownAttributeTypeID(this.SortAttributeTypeID))
          editingContextItems = this.ListSortDirection != ListSortDirection.Ascending ? (IEnumerable<EditingContextItem>) editingContextItems.OrderByDescending<EditingContextItem, object>((System.Func<EditingContextItem, object>) (o => o.Object.Attributes.GetAttributeValue(this.SortAttributeTypeID))) : (IEnumerable<EditingContextItem>) editingContextItems.OrderBy<EditingContextItem, object>((System.Func<EditingContextItem, object>) (o => o.Object.Attributes.GetAttributeValue(this.SortAttributeTypeID)));
        this.Clear();
        this.AddRange(editingContextItems);
      }
      finally
      {
        this.RaiseListChangedEvents = true;
        this.ResetBindings();
      }
    }
  }

  private sealed class EditingContextEditorCommandTarget : ICommandTarget
  {
    private EditingContextEditorControl _editingContextEditorControl;

    public EditingContextEditorCommandTarget(
      EditingContextEditorControl editingContextEditorControl)
    {
      this._editingContextEditorControl = editingContextEditorControl != null ? editingContextEditorControl : throw new ArgumentNullException(nameof (editingContextEditorControl));
    }

    public bool Execute(ICommandState commandState)
    {
      if (commandState == null)
        throw new ArgumentNullException(nameof (commandState));
      if (commandState.CommandName == "OpenInNewWindow")
      {
        this._editingContextEditorControl.OpenInNewWindow();
        return true;
      }
      if (commandState.CommandName == "ParametersCard")
      {
        this._editingContextEditorControl.Card();
        return true;
      }
      if (commandState.CommandName == "Paste")
      {
        this._editingContextEditorControl.Paste();
        return true;
      }
      if (commandState.CommandName == "Delete")
      {
        this._editingContextEditorControl.Remove();
        return true;
      }
      if (commandState.CommandName == "CustomizeColumns")
      {
        this._editingContextEditorControl.CustomizeColumns();
        return true;
      }
      if (!(commandState.CommandName == "Save"))
        return false;
      this._editingContextEditorControl.AcceptChanges();
      return true;
    }

    public bool QueryStatus(ICommandState commandState)
    {
      if (commandState == null)
        throw new ArgumentNullException(nameof (commandState));
      if (commandState.CommandName == "OpenInNewWindow")
      {
        commandState.Enabled = this._editingContextEditorControl.CanOpenInNewWindow();
        return true;
      }
      if (commandState.CommandName == "ParametersCard")
      {
        commandState.Enabled = this._editingContextEditorControl.CanCard();
        return true;
      }
      if (commandState.CommandName == "Paste")
      {
        commandState.Enabled = this._editingContextEditorControl.CanPaste();
        return true;
      }
      if (commandState.CommandName == "Delete")
      {
        commandState.Enabled = this._editingContextEditorControl.CanRemove();
        return true;
      }
      if (commandState.CommandName == "CustomizeColumns")
        return true;
      if (!(commandState.CommandName == "Save"))
        return false;
      commandState.Enabled = this._editingContextEditorControl.CanApply();
      return true;
    }
  }
}
