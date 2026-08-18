// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseTableView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Imbase.Controls;
using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImbaseTableView : UserControl, IView, ISelectedItemsHost, IIOSource
{
  internal static int _imageIndex = -1;
  private TableView _tableView;
  private ImbaseTableView.TableSelectedItems _selectedItems;
  private IImbaseSelector _selectorService;
  private INode _node;
  private long _objectId;
  private bool _loaded;
  protected System.IServiceProvider _services;
  protected IIODispatcher _dispatcher;
  protected INode _parentNode;
  protected INodeID _nodeID;
  protected NodeIDPath _path;
  private bool _disposed;

  internal long RecordId
  {
    get => this._tableView.RecordId;
    set
    {
      if (this._tableView == null)
        return;
      this._tableView.RecordId = value;
    }
  }

  internal bool DisabledRecord => this._tableView.DisabledRecord();

  public TableView TblView => this._tableView;

  public ImbaseTableView()
  {
    this.InitializeComponent();
    this._selectorService = ServicesManager.GetService(typeof (IImbaseSelector)) as IImbaseSelector;
    this._tableView.Grid.DoubleClick += new EventHandler(this.Grid_DoubleClick);
    this._tableView.FocusedChanged += new EventHandler(this.TableView_FocusedChanged);
    this._tableView.ItemEnterPress += new EventHandler(this.Grid_DoubleClick);
    this.Subscribe();
  }

  protected override void Dispose(bool disposing)
  {
    if (!this._disposed)
    {
      this.Unsubscribe();
      this._disposed = true;
    }
    base.Dispose(disposing);
  }

  private void _tableView_Enter(object sender, EventArgs e)
  {
    this._tableView.ActivateDisplaySettings = true;
  }

  private void TableEditor_TableChanged(object sender, ImbaseTableChangedEventArgs e)
  {
    if (e.TableId != this._tableView.TableId)
      return;
    this.ReloadTable();
  }

  private void Grid_DoubleClick(object sender, EventArgs e)
  {
    if (e is MouseEventArgs mouseEventArgs && this._tableView.Grid.HitTest(mouseEventArgs.X, mouseEventArgs.Y).Type != DataGridViewHitTestType.Cell)
      return;
    if (UserRowSelector.Instance.Enabled)
    {
      long recordId = this._tableView.RecordId;
      foreach (DataRow row in (InternalDataCollectionBase) this._tableView.Table.Rows)
      {
        if (Convert.ToInt64(row["-2"]) == recordId)
        {
          if (!UserRowSelector.Instance.OnSelectingRow(this._tableView.RowAttProps, row))
            return;
          break;
        }
      }
    }
    if (this.SelectedItems.Count <= 0 || this._services == null)
      return;
    if (this._services.GetService(typeof (ISelectionWindow)) is ISelectionWindow service)
    {
      if (service is ICurrentSelectedItemsHost selectedItemsHost)
        selectedItemsHost.ItemsHost = (ISelectedItemsHost) this;
      if (this.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is ImbaseTableRecordID itemData)
        this._selectorService.RecordId = itemData.Value;
      service.OkButton.PerformClick();
    }
    else
    {
      int count = this.SelectedItems.Count;
    }
  }

  private void TableView_FocusedChanged(object sender, EventArgs e)
  {
    this._selectedItems.Invalidate();
    this.OnSelectedItemsChanged();
  }

  private void Unsubscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe("ObjectsCreated", new NotificationEventHandler(this.OnObjectChanged));
    service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
    service.Unsubscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectChanged));
    service.Unsubscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectChanged));
  }

  private void Subscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Subscribe("ObjectsCreated", new NotificationEventHandler(this.OnObjectChanged));
    service.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectChanged));
    service.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.OnObjectChanged));
    service.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.OnObjectChanged));
  }

  private void OnObjectChanged(object sender, NotificationEventArgs ne)
  {
    DBObjectsEventArgs objectsEventArgs = ne as DBObjectsEventArgs;
    bool flag = true;
    switch (objectsEventArgs.EventName)
    {
      case "ObjectsChanged":
        flag = objectsEventArgs.ObjectIDs.Contains(this._tableView.TableId) || objectsEventArgs.ObjectIDs.Contains(this._tableView.LinkId);
        break;
      case "ObjectsCheckedIn":
        long num1 = Math.Abs(this._tableView.TableId);
        if (!objectsEventArgs.ObjectIDs.Contains(this._tableView.TableId) && !objectsEventArgs.ObjectIDs.Contains(num1))
        {
          long num2 = Math.Abs(this._tableView.LinkId);
          if (flag = objectsEventArgs.ObjectIDs.Contains(this._tableView.LinkId) || objectsEventArgs.ObjectIDs.Contains(num2))
          {
            this._objectId = Math.Abs(this._objectId);
            break;
          }
          flag = false;
          break;
        }
        this._objectId = Math.Abs(this._objectId);
        break;
      case "ObjectsCheckedOut":
        if (objectsEventArgs.ObjectIDs.Contains(this._tableView.TableId) && this._tableView.LinkId == -1L)
          this._objectId *= -1L;
        if (objectsEventArgs.ObjectIDs.Contains(this._tableView.LinkId))
        {
          this._objectId *= -1L;
          break;
        }
        flag = false;
        break;
      case "ObjectsCreated":
        flag = false;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          using (IEnumerator<long> enumerator = objectsEventArgs.ObjectIDs.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              long current = enumerator.Current;
              IDBAttribute attributeById = sessionKeeper.Session.GetObject(current, false)?.GetAttributeByID(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
              if (attributeById != null && attributeById.AsInteger == this._objectId)
              {
                if (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
                {
                  this._tableView.GetCreatedObjects(sessionKeeper.Session, customService);
                  DataGridViewRow currentRow = this._tableView.Grid.CurrentRow;
                  if (currentRow != null)
                  {
                    this._tableView.Grid.InvalidateRow(currentRow.Index);
                    break;
                  }
                  break;
                }
                break;
              }
            }
            break;
          }
        }
    }
    if (!flag)
      return;
    long recordId = this._tableView.RecordId;
    this.ReloadTable();
    this._tableView.RecordId = recordId;
  }

  private void ReloadTable()
  {
    this._tableView.ObjectId = -1L;
    this._tableView.ObjectId = this._objectId;
  }

  private void OnSelectedItemsChanged()
  {
    EventHandler selectedItemsChanged = this.SelectedItemsChanged;
    if (selectedItemsChanged == null)
      return;
    selectedItemsChanged((object) this, EventArgs.Empty);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._loaded = false;
    this._objectId = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this._services = provider;
    this._dispatcher = this._services.GetService(typeof (IIODispatcher)) as IIODispatcher;
    NodeIDPath parentPath = items.GetParentPath(0);
    this._parentNode = (INode) items.GetItemData(0, typeof (INode));
    this._node = (INode) null;
    this._nodeID = items.GetItemID(0);
    this._path = new NodeIDPath(parentPath, this._nodeID);
    this._selectedItems = new ImbaseTableView.TableSelectedItems(this._path, this.Node, this);
    this._tableView.SetServices(this._services);
  }

  public void Activate(IView previousView)
  {
    if (!this._loaded)
    {
      this._loaded = true;
      this._tableView.ObjectId = this._objectId;
      this._selectedItems.ParentId = this._objectId;
      this._selectorService.RecordId = -1L;
    }
    this._selectorService.ContextObjectId = this._objectId;
    this.OnSelectedItemsChanged();
  }

  public void Deactivate(IView nextView)
  {
    this._tableView.Detach();
    this._selectorService.ContextObjectId = -1L;
  }

  private INode Node
  {
    get
    {
      if (this._node == null)
      {
        this._node = this._parentNode.GetChild(this._nodeID);
        if (this._node == null)
          this._node = this._parentNode.GetData(this._nodeID, typeof (INode)) as INode;
        if (this._node is IContextAware node)
          node.Services = this._services;
      }
      return this._node;
    }
  }

  public string Caption => LocalizationHolder.rm.GetString("Imbase.Client_96");

  public int OrderID => int.MinValue;

  public int ImageIndex => ImbaseTableView._imageIndex;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseTableView));
    this._tableView = new TableView();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._tableView, "_tableView");
    this._tableView.FollowSelectMode = ImFollowSelectMode.imfsmFirstRow;
    this._tableView.Name = "_tableView";
    this._tableView.RecordId = -1L;
    this._tableView.Enter += new EventHandler(this._tableView_Enter);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((System.Windows.Forms.Control) this._tableView);
    this.Name = nameof (ImbaseTableView);
    this.ResumeLayout(false);
  }

  public ISelectedItems SelectedItems => (ISelectedItems) this._selectedItems;

  public event EventHandler SelectedItemsChanged;

  public object Control
  {
    get => (object) this;
    set
    {
    }
  }

  public System.IServiceProvider Services
  {
    get => this._services;
    set
    {
    }
  }

  ISelectedItems IIOSource.SelectedItems
  {
    get => (ISelectedItems) this._selectedItems;
    set
    {
    }
  }

  internal class TableSelectedItems : ISelectedItems, ISimpleSelectedItems
  {
    private NodeItems _nodeItems;
    private ImbaseTableView _view;
    private bool _valid;
    private INodeQuery _query;
    private long _parentId;

    private INodeQuery Query
    {
      get
      {
        if (this._query == null)
        {
          this._query = this._view.Node.GetQuery(ContentType.NonFolders);
          this._query.Execute((object) null, 2147483646);
        }
        return this._query;
      }
    }

    internal long ParentId
    {
      set => this._parentId = value;
      get => this._parentId;
    }

    public TableSelectedItems(NodeIDPath handlerPath, INode handler, ImbaseTableView owner)
    {
      this._nodeItems = new NodeItems(handlerPath, handler, new NodeIDCollection(), owner._services);
      this._view = owner;
      this._parentId = -1L;
    }

    public bool IsCollage
    {
      get
      {
        this.Validate();
        return this._nodeItems.IsCollage;
      }
    }

    public int Count
    {
      get
      {
        this.Validate();
        return this._nodeItems.Count;
      }
    }

    public object GetItemData(int index, System.Type dataFormat)
    {
      this.Validate();
      return this._nodeItems.GetItemData(index, dataFormat);
    }

    public INodeID GetItemID(int index)
    {
      this.Validate();
      return this._nodeItems.GetItemID(index);
    }

    public object GetParentData(int index, System.Type dataFormat)
    {
      this.Validate();
      return this._nodeItems.GetParentData(index, dataFormat);
    }

    public NodeIDPath GetParentPath(int index)
    {
      this.Validate();
      return this._nodeItems.GetParentPath(index);
    }

    private void Validate()
    {
      if (this._valid)
        return;
      this._view._selectorService.ContextObjectId = this._parentId;
      NodeIDCollection nodeIds = this._nodeItems.NodeIDs;
      nodeIds.Clear();
      try
      {
        this._valid = true;
        long recordId = this._view.RecordId;
        if (recordId == -1L || this._view.DisabledRecord)
          return;
        nodeIds.Add((INodeID) new ImbaseTableRecordNodeID(new ImbaseTableRecordID(recordId)));
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        this._valid = false;
        nodeIds.Clear();
      }
    }

    public void Invalidate() => this._valid = false;
  }
}
