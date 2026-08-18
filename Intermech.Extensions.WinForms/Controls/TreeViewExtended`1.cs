// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeViewExtended`1
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Async;
using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public abstract class TreeViewExtended<TRootTreeNode> : TreeView, ICancellationTokenOwner where TRootTreeNode : TreeNodeExtendedBase
{
  [CanBeNull]
  private readonly ImageList _imageList;
  private readonly bool _autoDisposeImageList;
  [NotNull]
  private readonly List<Task> _runningTasks = new List<Task>();
  [NotNull]
  private readonly SynchronizationContext _mainContext = SynchronizationContext.Current ?? throw new NullReferenceException("SynchronizationContext.Current");
  private bool _allTasksCancelled;
  [NotNull]
  private readonly SemaphoreSlim _cancelAllRunningTasksSemaphore = new SemaphoreSlim(1, 1);
  [CanBeNull]
  [ItemNotNull]
  private TreeNodeExtendedCollection<TRootTreeNode> _nodes;
  private bool? _inDesignMode;
  private TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum _loadRootStatus;

  [CanBeNull]
  protected CancellationTokenSource CancellationTokenSource { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.Threading.CancellationToken? CancellationToken
  {
    get => this.CancellationTokenSource?.Token;
  }

  protected TreeViewExtended()
  {
    this.AutoLoadRootOnShown = true;
    base.FullRowSelect = true;
    (this._imageList, this._autoDisposeImageList) = this.CreateImageList();
    if (this._imageList == null)
      return;
    this.ImageList = this._imageList;
  }

  protected virtual (ImageList imageList, bool autoImageList) CreateImageList()
  {
    return ((ImageList) null, false);
  }

  protected override async void Dispose(bool disposing)
  {
    try
    {
      await this.CancelAllRunningTasksAsync().ConfigureAwait(false);
    }
    finally
    {
      if (this.InvokeRequired)
        await this._mainContext;
    }
    this.CancellationTokenSource?.Dispose();
    this.CancellationTokenSource = (CancellationTokenSource) null;
    if (this._imageList != null)
    {
      if (base.ImageList == this._imageList)
        this.ImageList = (ImageList) null;
      if (this._autoDisposeImageList)
        this._imageList.Dispose();
    }
    base.Dispose(disposing);
  }

  [NotNull]
  protected virtual async Task CancelAllRunningTasksAsync()
  {
    CancellationTokenSource cancellationTokenSource = this.CancellationTokenSource;
    if ((cancellationTokenSource != null ? (!cancellationTokenSource.IsCancellationRequested ? 1 : 0) : 0) != 0)
      this.CancellationTokenSource.Cancel();
    if (this._runningTasks.Count <= 0)
      return;
    try
    {
      await this._cancelAllRunningTasksSemaphore.WaitAsync().ConfigureAwait(false);
    }
    finally
    {
      if (this.InvokeRequired)
        await this._mainContext;
    }
    try
    {
      this._allTasksCancelled = true;
      if (this._runningTasks.Count <= 0)
        return;
      try
      {
        try
        {
          await Task.WhenAll(this._runningTasks.ToArray<Task>(this._runningTasks.Count)).ConfigureAwait(false);
        }
        finally
        {
          if (this.InvokeRequired)
            await this._mainContext;
        }
      }
      catch (Exception ex)
      {
        Exception e;
        ref Exception local = ref e;
        if (ex.TryExtractNotOperationCancelled(out local) && !this.ShowExceptionDialog(e))
          throw e;
      }
      finally
      {
        this._runningTasks.Clear();
      }
    }
    finally
    {
      this._cancelAllRunningTasksSemaphore.Release();
    }
  }

  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual TreeNodeExtendedCollection<TRootTreeNode> Nodes
  {
    get => this._nodes ?? (this._nodes = new TreeNodeExtendedCollection<TRootTreeNode>(this));
  }

  [NotNull]
  [ItemNotNull]
  protected internal TreeNodeCollection OriginalNodes => base.Nodes;

  protected bool InDesignMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (this._inDesignMode ?? (this._inDesignMode = new bool?(this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || this.GetParentsEnumeration(true).Any<Control>((Func<Control, bool>) (ctrl =>
      {
        ISite site = ctrl.Site;
        return site != null && site.DesignMode;
      }))))).Value;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum LoadRootStatus
  {
    get => this._loadRootStatus;
    private set
    {
      if (this._loadRootStatus == value)
        return;
      TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum loadRootStatus = this._loadRootStatus;
      this._loadRootStatus = value;
      this.OnAfterLoadRootStatusChanged(loadRootStatus);
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event TreeViewExtended<TRootTreeNode>.AfterLoadRootStatusChangedDelegate AfterLoadRootStatusChanged;

  protected virtual void OnAfterLoadRootStatusChanged(
    TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum oldLoadRootStatus)
  {
    TreeViewExtended<TRootTreeNode>.AfterLoadRootStatusChangedDelegate rootStatusChanged = this.AfterLoadRootStatusChanged;
    if (rootStatusChanged == null)
      return;
    rootStatusChanged(oldLoadRootStatus);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool Shown { get; private set; }

  protected virtual bool ReadyToShow => true;

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public event EventHandler AfterShown;

  protected virtual void FireAfterShown()
  {
    EventHandler afterShown = this.AfterShown;
    if (afterShown == null)
      return;
    afterShown((object) this, EventArgs.Empty);
  }

  protected override void WndProc(ref Message m)
  {
    base.WndProc(ref m);
    if (m.Msg != 15 || this.LoadRootStatus != TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.NotStarted || !this.ReadyToShow || this.Shown)
      return;
    Form form = this.FindForm();
    System.Threading.CancellationToken? nullable = form is ICancellationTokenOwner cancellationTokenOwner ? cancellationTokenOwner.CancellationToken : new System.Threading.CancellationToken?();
    CancellationTokenSource cancellationTokenSource;
    if (!nullable.HasValue)
      cancellationTokenSource = new CancellationTokenSource();
    else
      cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(nullable.Value);
    this.CancellationTokenSource = cancellationTokenSource;
    if (form != null)
      this.LinkToForm(form);
    this.Shown = true;
    this.FireAfterShown();
    if (!this.AutoLoadRootOnShown || this.LoadRootStatus != TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.NotStarted || this.InDesignMode)
      return;
    this.SafeRunAsync(this.LoadRootAsync());
  }

  private async Task SafeRunAsync([NotNull] Task task)
  {
    this._runningTasks.Add(task);
    try
    {
      try
      {
        await task.ConfigureAwait(false);
      }
      finally
      {
        if (this.InvokeRequired)
          await this._mainContext;
      }
    }
    catch (Exception ex)
    {
      Exception e;
      ref Exception local = ref e;
      if (ex.TryExtractNotOperationCancelled(out local))
      {
        if (!this.ShowExceptionDialog(e))
          throw e;
      }
    }
    finally
    {
      if (!this._allTasksCancelled)
      {
        try
        {
          await this._cancelAllRunningTasksSemaphore.WaitAsync().ConfigureAwait(false);
        }
        finally
        {
          if (this.InvokeRequired)
            await this._mainContext;
        }
        try
        {
          this._runningTasks.Remove(task);
        }
        finally
        {
          this._cancelAllRunningTasksSemaphore.Release();
        }
      }
    }
  }

  private protected void LinkToForm([NotNull] Form ownerForm)
  {
    ownerForm.Closing += new CancelEventHandler(this.OnOwnerFormClosing);
    ownerForm.Closed += new EventHandler(this.OnOwnerFormClosed);
  }

  public event CancelEventHandler OwnerFormClosing;

  protected virtual void OnOwnerFormClosing([CanBeNull] object sender, [NotNull] CancelEventArgs e)
  {
    CancelEventHandler ownerFormClosing = this.OwnerFormClosing;
    if (ownerFormClosing == null)
      return;
    ownerFormClosing(sender, e);
  }

  public event EventHandler OwnerFormClosed;

  protected virtual void OnOwnerFormClosed([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CancellationTokenSource?.Cancel();
    EventHandler ownerFormClosed = this.OwnerFormClosed;
    if (ownerFormClosed == null)
      return;
    ownerFormClosed(sender, e);
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  [DisplayName("Автоматическая загрузка корня")]
  [Description("Автоматическая создание корневых элементов дерева во время первой отрисовки контрола")]
  public bool AutoLoadRootOnShown { get; set; }

  public async Task LoadRootAsync()
  {
    Intermech.Diagnostics.Check.Assert<TreeViewExtendedInitException>(!this.InDesignMode, "Инициализация дерева в режиме дизайнера невозможна");
    Intermech.Diagnostics.Check.Assert<TreeViewExtendedInitException>(this.LoadRootStatus == TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.NotStarted, "Дерево не может быть повторно инициализировано");
    this.OnBeforeLoadRoot((object) this);
    this.LoadRootStatus = TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.Started;
    try
    {
      IReadOnlyCollection<TRootTreeNode> rootNodes;
      using (TreeViewExtended<TRootTreeNode>.OperationService operationService = new TreeViewExtended<TRootTreeNode>.OperationService(this))
      {
        try
        {
          try
          {
            rootNodes = await this.CreateRootNodesAsync((TreeViewExtended<TRootTreeNode>.IOperationService) operationService, this.CancellationTokenSource.Token).ConfigureAwait(false);
          }
          finally
          {
            if (this.InvokeRequired)
              await this._mainContext;
          }
        }
        catch (Exception ex)
        {
          Exception e;
          ref Exception local = ref e;
          if (ex.TryExtractNotOperationCancelled(out local) && !this.ShowExceptionDialog(e))
            throw e;
          this.LoadRootStatus = TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.Aborted;
          return;
        }
        finally
        {
          System.Threading.CancellationToken? cancellationToken = this.CancellationToken;
          ref System.Threading.CancellationToken? local = ref cancellationToken;
          if ((local.HasValue ? (local.GetValueOrDefault().IsCancellationRequested ? 1 : 0) : 0) != 0 && this.LoadRootStatus != TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.Aborted)
            this.LoadRootStatus = TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.Aborted;
          this.ShowLoadCancelled();
        }
        CancellationTokenSource cancellationTokenSource = this.CancellationTokenSource;
        if (cancellationTokenSource != null)
          cancellationTokenSource.ThrowIfCancellationRequested();
        foreach (TRootTreeNode rootTreeNode in rootNodes.WithCancellationNotNull<TRootTreeNode>(this.CancellationTokenSource?.Token))
          rootTreeNode.OnAfterCreate((object) this);
      }
      if (rootNodes.Count > 0)
      {
        CancellationTokenSource cancellationTokenSource1 = this.CancellationTokenSource;
        if (cancellationTokenSource1 != null)
          cancellationTokenSource1.ThrowIfCancellationRequested();
        this.BeginUpdate();
        try
        {
          this.Nodes.AddRange<TRootTreeNode>(rootNodes.WithCancellation<TRootTreeNode>(this.CancellationTokenSource?.Token));
          CancellationTokenSource cancellationTokenSource2 = this.CancellationTokenSource;
          if (cancellationTokenSource2 != null)
            cancellationTokenSource2.ThrowIfCancellationRequested();
          if (this.Nodes.Count > 0)
            this.SelectedNode = (TreeNodeExtendedBase) this.Nodes[0];
          CancellationTokenSource cancellationTokenSource3 = this.CancellationTokenSource;
          if (cancellationTokenSource3 != null)
            cancellationTokenSource3.ThrowIfCancellationRequested();
        }
        finally
        {
          this.EndUpdate();
        }
      }
      rootNodes = (IReadOnlyCollection<TRootTreeNode>) null;
    }
    catch (Exception ex)
    {
      Exception e;
      ref Exception local = ref e;
      if (ex.TryExtractNotOperationCancelled(out local) && !this.ShowExceptionDialog(e))
        throw e;
    }
    finally
    {
      CancellationTokenSource cancellationTokenSource4 = this.CancellationTokenSource;
      if (cancellationTokenSource4 != null)
        cancellationTokenSource4.ThrowIfCancellationRequested();
      this.LoadRootStatus = TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.Completed;
      CancellationTokenSource cancellationTokenSource5 = this.CancellationTokenSource;
      if (cancellationTokenSource5 != null)
        cancellationTokenSource5.ThrowIfCancellationRequested();
      this.OnAfterLoadRoot((object) this);
    }
  }

  public void SyncAction([NotNull] Action action)
  {
    if (this.InvokeRequired)
      this.Invoke((Delegate) action);
    else
      action();
  }

  private void ShowLoadCancelled()
  {
    if (this.LoadRootStatus != TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum.Aborted || this._allTasksCancelled || this.Disposing || this.IsDisposed)
      return;
    Label label = new Label();
    label.Location = new Point(1, 1);
    label.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);
    label.Text = "Загрузка отменена";
    label.TextAlign = ContentAlignment.MiddleCenter;
    this.Controls.Add((Control) label);
  }

  protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
  {
    base.OnBeforeExpand(e);
    bool cancel = e.Cancel;
    if (e.Node is TreeNodeExtendedBase node1)
      node1.OnBeforeExpand((object) this, ref cancel);
    e.Cancel = cancel;
    TreeNode node2 = e.Node;
    if ((node2 != null ? (!node2.IsSelected ? 1 : 0) : 1) == 0)
      return;
    this.SelectedNode = e.Node;
  }

  protected override void OnAfterExpand(TreeViewEventArgs e)
  {
    base.OnAfterExpand(e);
    if (!(e.Node is TreeNodeExtendedBase node))
      return;
    node.OnAfterExpand((object) this);
  }

  protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
  {
    base.OnBeforeCollapse(e);
    bool cancel = e.Cancel;
    if (e.Node is TreeNodeExtendedBase node)
      node.OnBeforeCollapse((object) this, ref cancel);
    e.Cancel = cancel;
  }

  protected override void OnAfterCollapse(TreeViewEventArgs e)
  {
    base.OnAfterCollapse(e);
    if (!(e.Node is TreeNodeExtendedBase node))
      return;
    node.OnAfterCollapse((object) this);
  }

  protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
  {
    base.OnBeforeCheck(e);
    bool cancel = e.Cancel;
    if (e.Node is TreeNodeExtendedBase node)
      node.OnBeforeCheck((object) this, ref cancel);
    e.Cancel = cancel;
  }

  protected override void OnAfterCheck(TreeViewEventArgs e)
  {
    base.OnAfterCheck(e);
    if (!(e.Node is TreeNodeExtendedBase node))
      return;
    node.OnAfterCheck((object) this);
  }

  [DisplayName("Начало загрузки корня дерева")]
  [Description("Событие начала загрузки корня дерева")]
  public event EventHandler BeforeLoadRoot;

  protected virtual void OnBeforeLoadRoot([CanBeNull] object sender, [CanBeNull] EventArgs eventArgs = null)
  {
    EventHandler beforeLoadRoot = this.BeforeLoadRoot;
    if (beforeLoadRoot == null)
      return;
    beforeLoadRoot(sender, EventArgs.Empty);
  }

  [NotNull]
  [ItemNotNull]
  protected abstract Task<IReadOnlyCollection<TRootTreeNode>> CreateRootNodesAsync(
    [NotNull] TreeViewExtended<TRootTreeNode>.IOperationService operationService,
    System.Threading.CancellationToken cancellationToken);

  [DisplayName("Завершение загрузки корня дерева")]
  [Description("Событие завершения загрузки корня дерева")]
  public event EventHandler AfterLoadRoot;

  protected virtual void OnAfterLoadRoot([CanBeNull] object sender, [CanBeNull] EventArgs eventArgs = null)
  {
    EventHandler afterLoadRoot = this.AfterLoadRoot;
    if (afterLoadRoot == null)
      return;
    afterLoadRoot((object) this, eventArgs ?? EventArgs.Empty);
  }

  [CanBeNull]
  public TreeNodeExtendedBase GetNodeAt(Point pt) => (TreeNodeExtendedBase) base.GetNodeAt(pt);

  [CanBeNull]
  public TreeNodeExtendedBase GetNodeAt(int x, int y)
  {
    return (TreeNodeExtendedBase) base.GetNodeAt(x, y);
  }

  [NotNull]
  protected OwnerDrawPropertyBag GetItemRenderStyles([CanBeNull] TreeNodeExtendedBase node, int state)
  {
    return this.GetItemRenderStyles((TreeNode) node, state);
  }

  protected virtual bool ShowExceptionDialog([CanBeNull] Exception e) => false;

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new ImageList ImageList => base.ImageList;

  [CanBeNull]
  protected ImageList OriginalImageList
  {
    get => base.ImageList;
    set => this.ImageList = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(true)]
  public new bool FullRowSelect
  {
    get => base.FullRowSelect;
    set => base.FullRowSelect = value;
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public TreeNodeExtendedBase TopNode
  {
    get => (TreeNodeExtendedBase) base.TopNode;
    set => this.TopNode = (TreeNode) value;
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public TreeNodeExtendedBase SelectedNode
  {
    get => (TreeNodeExtendedBase) base.SelectedNode;
    set => this.SelectedNode = (TreeNode) value;
  }

  public enum LoadRootStatusEnum
  {
    NotStarted,
    Started,
    Completed,
    Aborted,
  }

  public delegate void AfterLoadRootStatusChangedDelegate(
    TreeViewExtended<TRootTreeNode>.LoadRootStatusEnum oldLoadRootStatus)
    where TRootTreeNode : TreeNodeExtendedBase;

  public interface IOperationService
  {
    void ShowLoadingCircleOnOperation();
  }

  internal class OperationService : TreeViewExtended<TRootTreeNode>.IOperationService, IDisposable
  {
    [NotNull]
    private readonly TreeViewExtended<TRootTreeNode> _treeView;
    [CanBeNull]
    private LoadingCircle _loadingCircle;

    public OperationService([NotNull] TreeViewExtended<TRootTreeNode> treeView)
    {
      this._treeView = treeView;
    }

    public void ShowLoadingCircleOnOperation()
    {
      LoadingCircle loadingCircle = new LoadingCircle();
      loadingCircle.Location = new Point(1, 1);
      Size size1 = this._treeView.Size;
      int width1 = size1.Width - 2;
      size1 = this._treeView.Size;
      int height1 = size1.Height - 2;
      loadingCircle.Size = new Size(width1, height1);
      this._loadingCircle = loadingCircle;
      Size size2 = this._treeView.Size;
      int width2 = size2.Width;
      size2 = this._treeView.Size;
      int height2 = size2.Height;
      int num = Math.Min(width2, height2);
      this._loadingCircle.OuterCircleRadius = num / 14;
      this._loadingCircle.InnerCircleRadius = num / 24;
      this._loadingCircle.SpokeThickness = num / 64 /*0x40*/;
      this._treeView.Controls.Add((Control) this._loadingCircle);
      this._loadingCircle.Active = true;
    }

    public void Dispose() => this._treeView.SyncAction(new Action(this.DisposeSync));

    private void DisposeSync()
    {
      if (this._loadingCircle == null)
        return;
      this._loadingCircle.Active = false;
      this._treeView.Controls.Remove((Control) this._loadingCircle);
      this._loadingCircle.Dispose();
    }
  }
}
