// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.DocumentControl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Controls;
using Intermech.Docking;
using Intermech.Document.Model;
using Intermech.Document.Model.FindReplace;
using Intermech.Document.Model.UI;
using Intermech.Document.Model.Undo;
using Intermech.Document.RtfEditor;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Элемент управления - интерфейс документа</summary>
public class DocumentControl : UserControl
{
  private DocumentViewMode documentViewMode = DocumentViewMode.Normal;
  private bool multiSelect = true;
  private RulerButton rulerButton;
  private PageControl pageControl;
  private List<Page> newPages = new List<Page>();
  /// <summary>Горизонтальная полоса прокрутки</summary>
  public HScrollBar hScrollBar;
  private VScrollBar vScrollBar;
  private FlowLayoutPanel flowLayoutPanel1;
  /// <summary>Панель на которой лежит горизонтальная полоса прокрутки и дополнительные элементы управления</summary>
  public Panel HSPanel;
  /// <summary>Control для переключения видов документа</summary>
  public ViewSwitch ViewsSwitch;
  private ImageList _viewsPageImages;
  private Bevel bevel1;
  private Splitter splitter1;
  private Bevel bevel2;
  private Ruler rulerVertical;
  public Ruler rulerHorizontal;
  private Panel subPanel;
  private ImDocument document;
  private DocumentsComplect documentsComplect;
  /// <summary>Пустое пространство вокруг страницы</summary>
  internal int margin = 10;
  private DocumentTreeNode suspendedSelection;
  private Page suspendedActivePage;
  private bool suspendedLastPage;
  private bool needSetIdentsToRuler = true;
  private int suspendScrollBars;
  internal PageElementUI focusedElement;
  private bool readOnly;
  private bool readOnlyGeometry;
  private bool readOnlyGeometryForDocument;
  private List<DocumentTreeNode> selectedNodes = new List<DocumentTreeNode>();
  private PointF pageCursorPosition = PointF.Empty;
  public bool NeedUpdateToolbar = true;
  private bool isElementSelecting = true;
  private bool isElementCreating;
  private static bool isCoorSystemSelecting;
  private bool isTableRowsSelecting;
  private bool isTableColumnsSelecting;
  private bool isTableCellsSelecting;
  private TableElement selectedTable;
  private IImDocumentManager documentManager;
  private Page activePage;
  private DocumentTreeNode activeElement;
  private IContainer components;
  private float documentScale = 1f;
  private DocZoomMode zoomMode;
  private bool rowSelection;
  public BarManager BarManager;
  /// <summary>Буферный экземпляр ImRtfEditor для редактирования</summary>
  private ImRtfEditor ternEditorBuffer;
  private bool queryCache_HasLockedNodes;
  /// <summary>Заблокировать обработчики из фоновых потоков т.к. окно закрывается</summary>
  public bool LockForClosing;
  public int LockedForHandler;
  private TextBoxElement ActiveEditorForSavedSelection;
  private SelectionBlock SavedActiveEditorSelection;
  private bool OldNeedUpdateToolBar = true;
  private SelectionBlock selBlock;
  private int cursLine;
  private int cursCol;
  private bool inPlaceEditorDeactivated;

  /// <summary>Выделение нескольких элементов происходит строками</summary>
  public bool RowSelection
  {
    get => this.rowSelection;
    set => this.rowSelection = value;
  }

  public event RowSelection_EventHandler RowSelectionEvent;

  /// <summary>Вызывает событие ActiveElementChanged</summary>
  /// <param name="e">Аргументы события</param>
  internal bool OnRowSelection(List<DocumentTreeNode> nodes)
  {
    try
    {
      RowSelection_EventArgs e = new RowSelection_EventArgs();
      e.Nodes = nodes;
      RowSelection_EventHandler rowSelectionEvent = this.RowSelectionEvent;
      if (rowSelectionEvent != null)
        rowSelectionEvent((object) this, e);
      return e.RowSelection.HasValue ? e.RowSelection.Value : this.RowSelection;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return this.RowSelection;
  }

  /// <summary>Параметры просмотра документа</summary>
  public DocumentViewMode DocumentViewMode
  {
    get => this.documentViewMode;
    set => this.documentViewMode = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool MultiSelect
  {
    get => this.multiSelect;
    set => this.multiSelect = value;
  }

  public void SetDocument(ImDocument doc, bool updateUI, bool refreshUI)
  {
    try
    {
      if (this.document == doc)
        return;
      this.SetSelection(new List<DocumentTreeNode>(), false, Point.Empty, false, false);
      if (this.document != null)
      {
        this.document.ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.document_ChildNodeAdded);
        this.document.ChildNodeRemoved -= new ChildNodeRemoved_EventHandler(this.document_ChildNodeRemoved);
        this.document.PageUnlocked -= new PageUnlocked_EventHandler(this.document_PageUnlocked);
        this.document.BackgroundThreadsFinished -= new BackgroundThreadsFinished_EventHandler(this.document_BackgroundThreadsFinished);
        this.document.TreeNodeAdded -= new ChildNodeAdded_EventHandler(this.document_TreeNodeAdded);
        this.document.TreeNodeRemoved -= new ChildNodeRemoved_EventHandler(this.document_TreeNodeRemoved);
        this.document.EndStructureChangingEvent -= new StructureChanging_EventHandler(this.document_EndStructureChangingEvent);
        this.document.ModifiedChanged -= new ModifiedChanged_EventHandler(this.Document_ModifiedChanged_Handler);
        this.document.InplaceEditorActivated -= new EventHandler(this.document_InplaceEditorActivated);
        this.document.documentControl = (DocumentControl) null;
      }
      if (doc == null && this.document != null)
        this.document.DestroyUI();
      this.document = doc;
      if (this.document != null)
      {
        if (this.PageControl == null)
        {
          this.PageControl = new PageControl();
          if (this.PageControl.Parent != this.subPanel)
            this.PageControl.Parent = (Control) this.subPanel;
        }
        this.PageControl.Size = this.subPanel.Size;
        this.PageControl.SetDocument(this.Document, updateUI, refreshUI);
        if (this.document.DocumentControl != this)
          this.document.DocumentControl = this;
        this.PageControl.BringToFront();
        this.PageControl.Focus();
        this.document.ChildNodeAdded += new ChildNodeAdded_EventHandler(this.document_ChildNodeAdded);
        this.document.ChildNodeRemoved += new ChildNodeRemoved_EventHandler(this.document_ChildNodeRemoved);
        this.document.PageUnlocked += new PageUnlocked_EventHandler(this.document_PageUnlocked);
        this.document.BackgroundThreadsFinished += new BackgroundThreadsFinished_EventHandler(this.document_BackgroundThreadsFinished);
        this.document.TreeNodeAdded += new ChildNodeAdded_EventHandler(this.document_TreeNodeAdded);
        this.document.TreeNodeRemoved += new ChildNodeRemoved_EventHandler(this.document_TreeNodeRemoved);
        this.document.EndStructureChangingEvent += new StructureChanging_EventHandler(this.document_EndStructureChangingEvent);
        this.document.ModifiedChanged += new ModifiedChanged_EventHandler(this.Document_ModifiedChanged_Handler);
        this.document.InplaceEditorActivated += new EventHandler(this.document_InplaceEditorActivated);
        this.selectedNodes = new List<DocumentTreeNode>();
        this.ActivePage = this.Document.NodesCount <= 0 ? (Page) null : this.Document.Nodes[0] as Page;
        if (!this.document.SuspendedRefreshUIFlag)
          this.Refresh();
        if (this.DocumentEditorForm == null)
          return;
        this.DocumentEditorForm.OnDocumentChanged();
      }
      else
        this.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void Document_ModifiedChanged_Handler(object sender, ModifiedChanged_EventArgs e)
  {
    ModifiedChanged_EventHandler documentModifiedChanged = this.DocumentModifiedChanged;
    if (documentModifiedChanged == null)
      return;
    documentModifiedChanged(sender, e);
  }

  public event ModifiedChanged_EventHandler DocumentModifiedChanged;

  private void document_EndStructureChangingEvent(object sender, StructureChanging_EventArgs e)
  {
    if (this.LockForClosing)
      return;
    try
    {
      this.UnselectRemovedNodes();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void document_TreeNodeRemoved(object sender, ChildNode_EventArgs e)
  {
    if (this.LockForClosing)
      return;
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new MethodInvoke_ChildNodeEvent(this.document_TreeNodeRemoved), sender, (object) e);
    }
    else
    {
      try
      {
        if (this.document == null || this.document.IsChangingStructure || e.ByShift)
          return;
        this.UnselectRemovedNodes();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  private void document_TreeNodeAdded(object sender, ChildNode_EventArgs e)
  {
  }

  /// <summary>Документ содержимое которого отображается</summary>
  public ImDocument Document
  {
    [DebuggerStepThrough] get => this.document;
    set => this.SetDocument(value, true, true);
  }

  /// <summary>Комплект документов</summary>
  public DocumentsComplect DocumentsComplect
  {
    get => this.documentsComplect;
    set
    {
      if (this.documentsComplect != null)
      {
        this.documentsComplect.ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.documentsComplect_ChildNodeAdded);
        this.documentsComplect.ChildNodeRemoved -= new ChildNodeRemoved_EventHandler(this.documentsComplect_ChildNodeRemoved);
      }
      this.documentsComplect = value;
      if (this.documentsComplect == null)
        return;
      this.Document = DocumentsComplect.GetFirstDocument((DocumentTreeNode) this.documentsComplect) as ImDocument;
      this.documentsComplect.ChildNodeAdded += new ChildNodeAdded_EventHandler(this.documentsComplect_ChildNodeAdded);
      this.documentsComplect.ChildNodeRemoved += new ChildNodeRemoved_EventHandler(this.documentsComplect_ChildNodeRemoved);
    }
  }

  private void documentsComplect_ChildNodeRemoved(object sender, ChildNode_EventArgs e)
  {
    if (this.LockForClosing)
      return;
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new MethodInvoke_ChildNodeEvent(this.documentsComplect_ChildNodeRemoved), sender, (object) e);
    }
    else
    {
      try
      {
        if (this.DocumentEditorForm == null)
          return;
        this.DocumentEditorForm.UpdateNavigationCommands();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  private void documentsComplect_ChildNodeAdded(object sender, ChildNode_EventArgs e)
  {
    if (this.LockForClosing)
      return;
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new MethodInvoke_ChildNodeEvent(this.documentsComplect_ChildNodeAdded), sender, (object) e);
    }
    else
    {
      try
      {
        if (this.DocumentEditorForm == null)
          return;
        this.DocumentEditorForm.UpdateNavigationCommands();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  private void document_BackgroundThreadsFinished2(object sender, BackgroundThreadsFinishedArgs e)
  {
    if (this.LockForClosing)
      return;
    try
    {
      if (this.suspendedLastPage)
        this.GotoLastPage();
      if (this.documentManager != null && this.documentManager.CommandManager != null)
        this.documentManager.CommandManager.QueryStatus();
      if (this.documentManager == null)
        return;
      this.documentManager.UpdatePagesInfo();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void document_BackgroundThreadsFinished(object sender, BackgroundThreadsFinishedArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      if (this.LockedForHandler > 0 && !this.InvokeRequired)
        --this.LockedForHandler;
      if (!this.InvokeRequired)
        return;
      ++this.LockedForHandler;
      this.BeginInvoke((Delegate) new BackgroundThreadsFinished_EventHandler(this.document_BackgroundThreadsFinished2), sender, (object) e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void document_PageUnlocked(object sender, PageUnlockedArgs e)
  {
    if (this.document != null && this.document.IsLoading)
      return;
    if (this.LockedForHandler > 0 && !this.InvokeRequired)
      --this.LockedForHandler;
    if (this.LockForClosing || this.IsDisposed)
      return;
    if (this.InvokeRequired)
    {
      ++this.LockedForHandler;
      this.BeginInvoke((Delegate) new PageUnlocked_EventHandler(this.document_PageUnlocked), sender, (object) e);
    }
    else
    {
      try
      {
        if (this.suspendedSelection != null)
        {
          int? distributingPage;
          if (this.suspendedSelection is PageElementNode suspendedSelection1 && !suspendedSelection1.Page.IsLocked)
          {
            distributingPage = this.document?.pageThreadStatus.StartDistributingPage;
            int index = suspendedSelection1.Page.Index;
            if (distributingPage.GetValueOrDefault() >= index & distributingPage.HasValue)
            {
              this.SetSelection(this.suspendedSelection, true, Point.Empty, true, false);
              this.suspendedSelection = (DocumentTreeNode) null;
              goto label_16;
            }
          }
          if (this.suspendedSelection is PageData suspendedSelection2 && !suspendedSelection2.IsLocked)
          {
            distributingPage = this.document?.pageThreadStatus.StartDistributingPage;
            int index = suspendedSelection2.Index;
            if (distributingPage.GetValueOrDefault() >= index & distributingPage.HasValue)
            {
              this.SetSelection(this.suspendedSelection, true, Point.Empty, true, false);
              this.suspendedSelection = (DocumentTreeNode) null;
            }
          }
        }
        else if (this.suspendedActivePage != null && !this.suspendedActivePage.IsWaitForDistributed)
        {
          this.ActivePage = this.suspendedActivePage;
          this.suspendedActivePage = (Page) null;
        }
label_16:
        if (this.activeElement is PageElementNode activeElement)
        {
          if (activeElement.Page != null && activeElement.Page == e.Page && activeElement.Page != this.activePage)
          {
            this.ActivePage = activeElement.Page as Page;
            if (activeElement is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.TextBox != null && textBoxElement.TextBox.EditorControl != null)
              textBoxElement.TextBox.EditorControl.Parent = (Control) this.PageControl;
            this.ScrollSelectionToView(true, false);
          }
          if (activeElement is TableElement tableElement && tableElement.NextTable != null)
            DocumentControl.SetShowSelected((DocumentTreeNode) tableElement.NextTable, false, false);
        }
        if (this.documentManager == null)
          return;
        this.documentManager.UpdatePagesInfo();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Конструктор. Автоматически создает объект ImDocument</summary>
  public DocumentControl()
  {
    try
    {
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.InitializeComponent();
      this.IsElementSelecting = true;
      this.UpdateScrollBars(false);
      this.rulerHorizontal.Document = this;
      this.rulerVertical.Document = this;
      ImDocumentEditorConfig.Instance.Changed += new EventHandler(this.ImDocumentEditorConfig_Changed);
      this.SetRulersPos();
      IntPtr handle = this.Handle;
      int num = this.IsHandleCreated ? 1 : 0;
      this.Document = new ImDocument(this, true);
      this.rulerHorizontal.Refresh();
      this.rulerVertical.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public DocumentControl(ImDocument document)
    : this(document, (IImDocumentManager) null)
  {
  }

  /// <param name="manager">менеджер документов</param>
  public DocumentControl(ImDocument document, IImDocumentManager manager)
  {
    try
    {
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.InitializeComponent();
      this.DocumentManager = manager;
      this.UpdateScrollBars(false);
      this.rulerHorizontal.Document = this;
      this.rulerVertical.Document = this;
      ImDocumentEditorConfig.Instance.Changed += new EventHandler(this.ImDocumentEditorConfig_Changed);
      this.SetRulersPos();
      this.Document = document;
      this.rulerHorizontal.Refresh();
      this.rulerVertical.Refresh();
      IntPtr handle = this.Handle;
      int num = this.IsHandleCreated ? 1 : 0;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  protected override void OnHandleCreated(EventArgs e) => base.OnHandleCreated(e);

  private void SetRulersPos()
  {
    try
    {
      Point point = new Point();
      Size size1 = new Size(this.Width - this.vScrollBar.Size.Width, this.vScrollBar.Size.Height);
      if (ImDocumentEditorConfig.Instance.VerticalRuler)
      {
        this.rulerVertical.Visible = true;
        point.X = 23;
        size1.Width -= 23;
      }
      else
      {
        this.rulerVertical.Visible = false;
        point.X = 0;
      }
      if (ImDocumentEditorConfig.Instance.HorizontalRuler)
      {
        this.rulerHorizontal.Visible = true;
        Size size2 = new Size(this.Width - this.vScrollBar.Size.Width, 23);
        if (ImDocumentEditorConfig.Instance.VerticalRuler)
        {
          this.rulerHorizontal.Location = new Point(23, 0);
          size2.Width -= 23;
        }
        else
          this.rulerHorizontal.Location = new Point(0, 0);
        this.rulerHorizontal.Size = size2;
        point.Y = 23;
        this.rulerVertical.Location = new Point(0, 23);
        size2 = new Size(23, this.vScrollBar.Size.Height - 23);
        this.rulerVertical.Size = size2;
        size1.Height -= 23;
      }
      else
      {
        this.rulerHorizontal.Visible = false;
        point.Y = 0;
        this.rulerVertical.Location = new Point(0, 0);
        this.rulerVertical.Size = new Size(23, this.vScrollBar.Size.Height);
      }
      if (ImDocumentEditorConfig.Instance.HorizontalRuler && ImDocumentEditorConfig.Instance.VerticalRuler)
      {
        this.rulerButton.Location = new Point(0, 0);
        this.rulerButton.Visible = true;
      }
      else
        this.rulerButton.Visible = false;
      this.subPanel.Location = point;
      this.subPanel.Size = size1;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void ImDocumentEditorConfig_Changed(object sender, EventArgs e)
  {
    try
    {
      this.SetRulersPos();
      if (this.ActivePage != null)
      {
        this.rulerHorizontal.Page = this.ActivePage;
        this.rulerVertical.Page = this.ActivePage;
      }
      else
      {
        this.rulerHorizontal.Page = (Page) null;
        this.rulerVertical.Page = (Page) null;
      }
      this.SetRulerBorders();
      this.SetIdentsToRuler();
      this.rulerVertical.RebuildRulerCoords();
      this.rulerHorizontal.RebuildRulerCoords();
      this.rulerHorizontal.Refresh();
      this.rulerVertical.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  internal void AddPageUI(PageControl pageControl)
  {
  }

  /// <summary>Сфокуситрованный элемент (элемент обрабатывающий нажатия клавиш)</summary>
  public PageElementUI FocusedElement
  {
    [DebuggerStepThrough] get
    {
      if (this.PageControl == null)
        return (PageElementUI) null;
      return this.PageControl.focusedElement != null ? this.PageControl.focusedElement : (PageElementUI) this.PageControl.pageControlUI;
    }
  }

  public PageControl PageControl
  {
    get => this.pageControl;
    set => this.pageControl = value;
  }

  /// <summary>Установить активной страницу документа</summary>
  /// <param name="page">Страница документа</param>
  /// <param name="updateUI">Обновить геометрию элементов, если необходимо из-за предыдущих действий. Не вызывает обновления разбивки!</param>
  /// <param name="refreshUI">Отразить смену выбора в интерфейсе пользователя с перемоткой полосы прокрутки</param>
  /// <param name="showFull">Попытаться показать страницу целиком, если она была видна частично</param>
  /// <param name="showLeftTop">Показать левый верхний угол страинцы в левом верхнем углу окна</param>
  public void SetActivePage(
    Page page,
    bool updateUI,
    bool refreshUI,
    bool showFull,
    bool showLeftTop)
  {
    try
    {
      if (this.activePage == page || page != null && page.OwnerDocument == null || !this.EditorValidating())
        return;
      if (page != null && page.IsWaitForDistributed)
      {
        this.suspendedActivePage = page;
        this.suspendedLastPage = false;
      }
      else
      {
        this.suspendedActivePage = (Page) null;
        this.suspendedLastPage = false;
        Page activePage = this.activePage;
        ImDocument doc = page == null ? this.Document : (ImDocument) page.OwnerDocument;
        doc?.SuspendUpdateGeometryRefreshUI();
        try
        {
          this.SetDocument(doc, true, false);
          this.activePage = page;
          if ((this.ActiveElement == null || !(this.ActiveElement is Page)) && this.ActiveElement is PageElementNode activeElement)
          {
            activeElement.SetNeedUpdateUIGeometry(true, true);
            activeElement.DeactivateInPlaceEditor();
          }
          this.OnActivePageChanged(new EventArgs());
          if (this.PageControl == null || !this.PageControl.OnePage)
            return;
          double num = (double) this.SetZoom(this.ZoomMode, this.DocumentScale);
        }
        finally
        {
          doc?.ResumeUpdateRefreshUI(updateUI, false);
          if (this.ActiveElement != null && this.ActiveElement is Page)
            this.SetSelection((DocumentTreeNode) this.activePage, false, Point.Empty, showFull, true);
          else if (this.activePage != null && this.activePage.PageUI != null)
            this.ScrollToViewRectangle(this.activePage.PageUI.Bounds, showFull, showLeftTop);
          if (doc != null & refreshUI)
            doc.RefreshUI();
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установить активной страницу документа</summary>
  /// <param name="page">Страница документа</param>
  /// <param name="updateUI">Обновить геометрию элементов, если необходимо из-за предыдущих действий. Не вызывает обновления разбивки!</param>
  /// <param name="refreshUI">Отразить смену выбора в интерфейсе пользователя с перемоткой полосы прокрутки</param>
  public void SetActivePage(Page page, bool updateUI = true, bool refreshUI = true)
  {
    this.SetActivePage(page, updateUI, refreshUI, false, true);
  }

  /// <summary>Активная страница</summary>
  public Page ActivePage
  {
    [DebuggerStepThrough] get => this.activePage;
    set => this.SetActivePage(value);
  }

  /// <summary>Перейти к последней странице</summary>
  /// <returns>Текущая страница после перехода</returns>
  public Page GotoLastPage()
  {
    try
    {
      if (this.document != null)
      {
        if (this.document.BackThreadIsActive)
        {
          this.suspendedLastPage = true;
          this.suspendedActivePage = (Page) null;
        }
        else
        {
          this.suspendedLastPage = false;
          this.suspendedActivePage = (Page) null;
          if (this.document.Nodes.Count > 0)
          {
            this.SetActivePage((Page) ImDocumentData.GetLastPage((DocumentTreeNode) this.document), true, true, true, true);
            return this.ActivePage;
          }
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return (Page) null;
  }

  /// <summary>Перейти к первой странице</summary>
  /// <returns>Текущая страница после перехода</returns>
  public Page GotoFirstPage()
  {
    try
    {
      if (this.Document != null)
      {
        if (this.Document.NodesCount > 0)
        {
          this.SetActivePage((Page) ImDocumentData.GetFirstPage((DocumentTreeNode) this.Document), true, true, true, true);
          return this.ActivePage;
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return (Page) null;
  }

  /// <summary>Перейти к предыдущей странице</summary>
  /// <returns>Текущая страница после перехода</returns>
  public Page GotoPrevPage()
  {
    try
    {
      if (this.Document != null)
      {
        if (this.ActivePage == null || this.ActivePage.Parent == null)
          return this.GotoFirstPage();
        Page prevPage = (Page) ImDocumentData.GetPrevPage(this.ActivePage.Parent, this.ActivePage.Index, false);
        if (prevPage != null)
          this.SetActivePage(prevPage, true, true, true, true);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return this.ActivePage;
  }

  /// <summary>Перейти к следующей странице</summary>
  /// <returns>Текущая страница после перехода</returns>
  public Page GotoNextPage()
  {
    try
    {
      if (this.Document != null)
      {
        if (this.ActivePage == null || this.ActivePage.Parent == null)
          return this.GotoFirstPage();
        Page nextPage = (Page) ImDocumentData.GetNextPage(this.ActivePage.Parent, this.ActivePage.Index, false);
        if (nextPage != null)
          this.SetActivePage(nextPage, true, true, true, true);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return this.ActivePage;
  }

  public void GoToNextDocument()
  {
    try
    {
      if (this.DocumentsComplect == null || this.Document == null)
        return;
      ImDocumentData nextDocument = DocumentsComplect.GetNextDocument(this.Document.Parent, this.Document.Index, false);
      if (nextDocument == null)
        return;
      this.SuspendScrollBars();
      try
      {
        this.SetDocument(nextDocument as ImDocument, false, false);
      }
      finally
      {
        this.ResumeScrollBars(false);
        this.VScrollBar.Value = 0;
        this.HScrollBar.Value = 0;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public void GoToPrevDocument(bool scrollDown)
  {
    try
    {
      if (this.DocumentsComplect == null || this.Document == null)
        return;
      ImDocumentData prevDocument = DocumentsComplect.GetPrevDocument(this.Document.Parent, this.Document.Index, false);
      if (prevDocument == null)
        return;
      this.SetDocument(prevDocument as ImDocument, false, false);
      this.SuspendScrollBars();
      try
      {
        this.SetDocument(prevDocument as ImDocument, false, false);
      }
      finally
      {
        this.ResumeScrollBars(false);
        if (scrollDown)
          this.PageControl.SetScrollBarValue((ScrollBar) this.VScrollBar, this.VScrollBar.Maximum);
        else
          this.PageControl.SetScrollBarValue((ScrollBar) this.VScrollBar, 0);
        this.HScrollBar.Value = 0;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Создать новую страницу документа или шаблона в соответствии с режимом</summary>
  /// <returns>Созданная страница</returns>
  public Page NewPage()
  {
    page2 = (Page) null;
    try
    {
      if (this.Document == null)
        this.Document = new ImDocument(true);
      Page page1 = (Page) null;
      if (this.Document.Nodes.Count > 0)
        page1 = (Page) this.Document.Nodes[this.Document.Nodes.Count - 1];
      if (page1 != null)
      {
        if (page1.AddNewDataPage(false) is Page page2)
        {
          page2.SetParent((DocumentTreeNode) this.Document, false, false);
          page1.NextPage = (PageData) page2;
        }
        else
        {
          page2 = this.Document.NewPage() as Page;
          page2.SetParent((DocumentTreeNode) this.Document, false, false);
        }
      }
      else
      {
        page2 = this.Document.NewPage() as Page;
        page2.SetParent((DocumentTreeNode) this.Document, false, false);
      }
      this.OnPageAdded((EventArgs) null);
      if (page2 != null)
        this.ActivePage = page2;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return page2;
  }

  /// <summary>Вставить новую страницу</summary>
  /// <param name="insertBefore">true - вставить перед текущей странице, false - после текущей</param>
  /// <returns>Новую страницу</returns>
  public Page InsertNewPage(bool insertBefore) => this.InsertNewPage((string) null, insertBefore);

  /// <summary>Вставить новую страницы по шаблону</summary>
  /// <param name="pageTemplateId">Шаблон новой страницы</param>
  /// <param name="insertBefore">true - вставить перед текущей странице, false - после текущей</param>
  /// <returns>Новую страницу</returns>
  public virtual Page InsertNewPage(string pageTemplateId, bool insertBefore)
  {
    Page child = (Page) null;
    try
    {
      if (this.Document == null)
        this.Document = new ImDocument(true);
      child = pageTemplateId == null || !(pageTemplateId != "") ? this.Document.NewPage((DocumentTreeNode) null) as Page : this.Document.ClonePageFromTemplate(pageTemplateId, false) as Page;
      this.Document.InsertChildNode(this.ActivePage == null || this.ActivePage.Index == -1 ? (insertBefore ? 0 : this.Document.Nodes.Count) : (insertBefore ? this.ActivePage.Index : this.ActivePage.Index + 1), (DocumentTreeNode) child, false, true, true, true, false);
      if (child != null)
        this.ActivePage = child;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return child;
  }

  /// <summary>Вставить дополнительную страницу после текущей</summary>
  /// <param name="currentPage">объект текущей страницы, null - активная страница</param>
  /// <param name="hierarchicalPageNumber">номер новой дополнительной страницы</param>
  /// <returns>Новая доп.страница или null в случае ошибки</returns>
  public Page InsertAdditionalPageAfter(
    PageData currentPage,
    string hierarchicalPageNumber,
    bool makeActive = false)
  {
    Page page = (Page) null;
    currentPage = currentPage ?? (PageData) this.ActivePage;
    if (this.Document == null)
      return (Page) null;
    if (currentPage == null)
      return (Page) null;
    if (currentPage.IsFinalPage)
      return (Page) null;
    try
    {
      page = currentPage.AddNewAdditionalPage(hierarchicalPageNumber, true) as Page;
      if (page != null & makeActive)
        this.ActivePage = page;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return page;
  }

  /// <summary>Получить индекс элемента перед активным листом.
  /// Используется для вставки нового листа</summary>
  /// <returns></returns>
  public int GetIndexBeforeCurrentPage() => this.ActivePage != null ? this.ActivePage.Index : 0;

  /// <summary>Получить индекс элемента перед активным листом.
  /// Используется для вставки нового листа</summary>
  /// <returns></returns>
  public int GetIndexAfterCurrentPage()
  {
    return this.ActivePage != null ? this.ActivePage.Index + 1 : this.Document.Nodes.Count;
  }

  /// <summary>Вставить новую страницы по шаблону после текущей и встроить её в поток данных, разбитый по страницам</summary>
  /// <param name="pageTemplateId">Шаблон новой страницы</param>
  public Page InsertNewPageInDataFlowAfterCurrent(
    string pageTemplateId,
    bool manualInserted = true,
    bool makeActive = true)
  {
    Page page = (Page) null;
    try
    {
      int afterCurrentPage = this.GetIndexAfterCurrentPage();
      page = this.Document.InsertNewPageInDocumentFlow(pageTemplateId, afterCurrentPage, manualInserted, true) as Page;
      if (page != null & makeActive)
        this.ActivePage = page;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return page;
  }

  /// <summary>Вставить новую страницы по шаблону перед текущей и встроить её в поток данных, разбитый по страницам</summary>
  /// <param name="pageTemplateId">Шаблон новой страницы</param>
  public Page InsertNewPageInDataFlowBeforeCurrent(string pageTemplateId, bool manualInserted = true)
  {
    page = (Page) null;
    try
    {
      int beforeCurrentPage = this.GetIndexBeforeCurrentPage();
      if (this.Document.InsertNewPageInDocumentFlow(pageTemplateId, beforeCurrentPage, manualInserted, true) is Page page)
        this.ActivePage = page;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return page;
  }

  public Page RemovePage(Page page, bool updateUI, bool refreshUI)
  {
    page = page ?? this.ActivePage;
    Page page1 = (Page) null;
    Page page2 = (Page) null;
    if (page != null)
    {
      Page prevPage = (Page) ImDocumentData.GetPrevPage(page.Parent, page.Index, false);
      if (!page.IsLastPage)
        page2 = (Page) ImDocumentData.GetNextPage(page.Parent, page.Index, false);
      page.RemovePageFromDataFlow(updateUI);
      page1 = page2 ?? prevPage;
      this.SetActivePage(page1, updateUI, refreshUI);
    }
    return page1;
  }

  /// <summary>Событие Добавлена страница</summary>
  public event PageAdded_EventHandler PageAdded;

  /// <summary>Вызывает событие PageAdded</summary>
  /// <param name="e">Аргументы события</param>
  public void OnPageAdded(EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new DocumentControl.MethodInvoke_EventArgs(this.OnPageAdded), (object) e);
    }
    else
    {
      if (this.LockedForHandler > 0)
        --this.LockedForHandler;
      if (this.LockForClosing)
        return;
      try
      {
        if (this.DocumentEditorForm != null && this.DocumentEditorForm.TopLevelControl != null)
        {
          if (this.DocumentEditorForm.TopLevelControl.InvokeRequired)
          {
            ++this.LockedForHandler;
            this.DocumentEditorForm.TopLevelControl.BeginInvoke((Delegate) new MethodInvoker(this.DocumentEditorForm.UpdateNavigationCommands));
          }
          else
            this.DocumentEditorForm.UpdateNavigationCommands();
        }
        this.documentManager?.UpdatePagesInfo();
        PageAdded_EventHandler pageAdded = this.PageAdded;
        if (pageAdded == null)
          return;
        pageAdded((object) this, e);
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Событие Удалена страница</summary>
  public event PageRemoved_EventHandler PageRemoved;

  /// <summary>Вызывает событие PageRemoved</summary>
  /// <param name="e">Аргументы события</param>
  public void OnPageRemoved(EventArgs e)
  {
    if (this.LockForClosing)
      return;
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new DocumentControl.MethodInvoke_EventArgs(this.OnPageRemoved), (object) e);
    }
    else
    {
      try
      {
        this.DocumentEditorForm?.UpdateNavigationCommands();
        this.documentManager?.UpdatePagesInfo();
        PageRemoved_EventHandler pageRemoved = this.PageRemoved;
        if (pageRemoved == null)
          return;
        pageRemoved((object) this, e);
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Событие Свойство ActivePage изменено</summary>
  public event ActivePageChanged_EventHandler ActivePageChanged;

  /// <summary>Вызывает событие ActivePageChanged</summary>
  /// <param name="e">Аргументы события</param>
  public void OnActivePageChanged(EventArgs e)
  {
    if (this.LockForClosing)
      return;
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new DocumentControl.MethodInvoke_EventArgs(this.OnActivePageChanged), (object) e);
    }
    else
    {
      try
      {
        if (this.DocumentEditorForm != null)
          this.DocumentEditorForm.UpdateNavigationCommands();
        if (this.documentManager != null)
          this.documentManager.UpdatePagesInfo();
        this.rulerVertical.Page = this.ActivePage;
        this.rulerHorizontal.Page = this.ActivePage;
        this.SetRulerBorders();
        this.SetIdentsToRuler();
        this.rulerVertical.RebuildRulerCoords();
        this.rulerHorizontal.RebuildRulerCoords();
        this.rulerHorizontal.Refresh();
        this.rulerVertical.Refresh();
        ActivePageChanged_EventHandler activePageChanged = this.ActivePageChanged;
        if (activePageChanged == null)
          return;
        activePageChanged((object) this, e);
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Обработчик события ChildNodeAdded в документе </summary>
  /// <param name="sender">Объект вызвавший событие</param>
  /// <param name="e">Аргументы события</param>
  public void document_ChildNodeAdded(object sender, ChildNode_EventArgs e)
  {
    try
    {
      int num = 5;
      if (this.LockedForHandler > 0 && !this.InvokeRequired)
        --this.LockedForHandler;
      if (this.LockForClosing)
        return;
      Page page = (Page) null;
      if (e != null)
        page = e.Child as Page;
      if (page == null)
        return;
      this.newPages.Add(page);
      if (this.newPages.Count < num)
        return;
      if (this.InvokeRequired)
      {
        ++this.LockedForHandler;
        this.BeginInvoke((Delegate) new ChildNodeAdded_EventHandler(this.document_ChildNodeAdded), sender, (object) e);
      }
      else
      {
        this.document.SuspendUpdateGeometryRefreshUI();
        try
        {
          for (int index = 0; index < this.newPages.Count; ++index)
            this.newPages[index].CreateUI();
          this.OnPageAdded(new EventArgs());
        }
        finally
        {
          this.document.ResumeUpdateRefreshUI(false, false);
        }
        if (this.pageControl != null)
          this.pageControl.UpdateSettings();
        this.newPages.Clear();
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обработчик события ChildNodeRemoved в документе </summary>
  /// <param name="sender">Объект вызвавший событие</param>
  /// <param name="e">Аргументы события</param>
  public void document_ChildNodeRemoved(object sender, ChildNode_EventArgs e)
  {
    if (this.LockForClosing)
      return;
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new MethodInvoke_ChildNodeEvent(this.document_ChildNodeRemoved), sender, (object) e);
    }
    else
    {
      try
      {
        if (e.Child is Page child && child.PageControl != null && this.pageControl != null)
          this.pageControl.UpdateSettings();
        this.OnPageRemoved(new EventArgs());
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Обработчик события InplaceEditorActivated в документе </summary>
  /// <param name="sender">Объект вызвавший событие</param>
  /// <param name="e">Аргументы события</param>
  public void document_InplaceEditorActivated(object sender, EventArgs e)
  {
    if (this.LockForClosing)
      return;
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new EventHandler(this.document_InplaceEditorActivated), sender, (object) e);
    }
    else
    {
      try
      {
        this.DocumentEditorForm?.UpdateFormatCommands();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Видимая область документа</summary>
  public virtual Rectangle VisibleWorkArea
  {
    [DebuggerStepThrough] get
    {
      Rectangle displayRectangle = this.DisplayRectangle;
      displayRectangle.Width -= this.vScrollBar.Width;
      displayRectangle.Height -= this.hScrollBar.Height;
      return displayRectangle;
    }
  }

  public void InvokeUpdateUIGeometry(PageData page, bool refreshUI)
  {
    if (this.LockedForHandler > 0 && !this.InvokeRequired)
      --this.LockedForHandler;
    if (page == null)
      return;
    try
    {
      if (this.InvokeRequired)
      {
        ++this.LockedForHandler;
        this.BeginInvoke((Delegate) new MethodInvokeUpdateUIGeometry(this.InvokeUpdateUIGeometry), (object) page, (object) refreshUI);
      }
      else
        page.UpdateUIGeometry(refreshUI);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Сделать активным родителя активного элемента</summary>
  public virtual void GotoParentElement()
  {
    try
    {
      if (!this.EditorValidating() || this.ActiveElement == null || this.ActiveElement.Parent == null)
        return;
      this.SetSelection(this.ActiveElement.Parent, false, Point.Empty, true, false);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обработать диалоговые клавиши. Заблокированы действия ContainerControl.</summary>
  /// <param name="keyData"></param>
  /// <returns></returns>
  protected override bool ProcessDialogKey(Keys keyData)
  {
    try
    {
      if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
      {
        switch (keyData & Keys.KeyCode)
        {
          case Keys.Tab:
            if (this.ProcessTabKey((keyData & Keys.Shift) == Keys.None))
              return false;
            break;
          case Keys.Left:
          case Keys.Up:
          case Keys.Right:
          case Keys.Down:
            return false;
        }
      }
      return base.ProcessDialogKey(keyData);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return false;
  }

  /// <summary>Обработка клавиши PageUp</summary>
  public void ProcessPageUp()
  {
    try
    {
      int num = this.VScrollBar.Value;
      this.PageControl.SetScrollBarValue((ScrollBar) this.VScrollBar, this.VScrollBar.Value - this.VScrollBar.LargeChange);
      if (this.VScrollBar.Value != num)
        return;
      this.GoToPrevDocument(true);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обработка клавиши PageDown</summary>
  public void ProcessPageDown()
  {
    try
    {
      int num = this.VScrollBar.Value;
      this.PageControl.SetScrollBarValue((ScrollBar) this.VScrollBar, this.VScrollBar.Value + this.VScrollBar.LargeChange);
      if (this.VScrollBar.Value != num)
        return;
      this.GoToNextDocument();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обработка клавиши Ctrl+PageDown</summary>
  public void ProcessCtrlPageDown()
  {
    try
    {
      int num = this.VScrollBar.Value;
      this.PageControl.SetScrollBarValue((ScrollBar) this.VScrollBar, this.VScrollBar.Maximum);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обработка клавиши Ctrl+PageUp</summary>
  public void ProcessCtrlPageUp()
  {
    try
    {
      int num = this.VScrollBar.Value;
      this.PageControl.SetScrollBarValue((ScrollBar) this.VScrollBar, this.VScrollBar.Minimum);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public void OnPreProcessCmdKey(PreProcessCmdKey_EventArgs e)
  {
    try
    {
      PreProcessCmdKey_EventHandler preProcessCmdKey = this.PreProcessCmdKey;
      if (preProcessCmdKey == null)
        return;
      preProcessCmdKey((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public event PreProcessCmdKey_EventHandler PreProcessCmdKey;

  /// <summary>Обработать нажатие клавиши</summary>
  /// <param name="msg">Сообщение</param>
  /// <param name="keyData">Данные о нажатой клавише</param>
  /// <returns>true, если нажатие обработано и не требует дальнейшей обработки</returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    try
    {
      PageElementUI focusedElement = (PageElementUI) null;
      if (this.activeElement is IPageElementWithInterface activeElement)
        focusedElement = activeElement.PageUI;
      if (focusedElement == null || !focusedElement.InPlaceEditorActive || activeElement?.InPlaceEditorControl == null || !activeElement.InPlaceEditorControl.Focused)
        focusedElement = (PageElementUI) null;
      PreProcessCmdKey_EventArgs e = new PreProcessCmdKey_EventArgs(msg, keyData, focusedElement);
      this.OnPreProcessCmdKey(e);
      msg = e.Msg;
      if (e.Cancel)
        return true;
      switch (keyData)
      {
        case Keys.Return:
          if (this.IsElementCreating)
          {
            if (this.SelectedElementCreator != null)
              this.SelectedElementCreator.CompleteCreation((object) null, (EventArgs) null);
            this.IsElementCreating = false;
            return true;
          }
          break;
        case Keys.Escape:
          if (DocumentControl.IsCoorSystemSelecting)
            DocumentControl.IsCoorSystemSelecting = false;
          if (this.IsElementSelecting)
            this.GotoParentElement();
          else if (this.IsElementCreating)
          {
            if (this.SelectedElementCreator != null)
              this.SelectedElementCreator.CancelCreation((object) null, (EventArgs) null);
            else
              this.IsElementCreating = false;
          }
          return true;
        case Keys.Prior:
          this.ProcessPageUp();
          return true;
        case Keys.Next:
          this.ProcessPageDown();
          return true;
        case Keys.Prior | Keys.Control:
          this.ProcessCtrlPageUp();
          return true;
        case Keys.Next | Keys.Control:
          this.ProcessCtrlPageDown();
          return true;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return false;
  }

  /// <summary>Событие Получить контекстное меню элемента</summary>
  public event GetCustomElementContextMenu_EventHandler GetCustomElementContextMenu;

  /// <summary>Получить контекстное меню</summary>
  /// <param name="node">Узел</param>
  /// <returns>Список элементов контекстного меню</returns>
  [Obsolete("Заменить вызов на GetContexMenu(DocumentTreeNode node, bool overrideSelection)")]
  public List<MenuButtonItem> GetContexMenu(DocumentTreeNode node)
  {
    return this.GetContexMenu(node, false);
  }

  /// <summary>Получить контекстное меню</summary>
  /// <param name="node">Узел</param>
  /// <param name="overrideSelection">Если true, то выделение игнорируется</param>
  /// <returns>Список элементов контекстного меню</returns>
  public List<MenuButtonItem> GetContexMenu(DocumentTreeNode node, bool overrideSelection)
  {
    try
    {
      DocumentTreeNode[] context = (DocumentTreeNode[]) null;
      if (node != null)
      {
        if (!overrideSelection && this.NodeIsSelected(node))
          context = this.SelectedNodes.ToArray();
        else
          context = new DocumentTreeNode[1]{ node };
      }
      return this.GetContexMenu(context);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      return new List<MenuButtonItem>();
    }
  }

  /// <summary>Получить контекстное меню</summary>
  /// <param name="context">Узлы - контекст меню</param>
  /// <returns>Список элементов контекстного меню</returns>
  public List<MenuButtonItem> GetContexMenu(List<DocumentTreeNode> context)
  {
    return this.GetContexMenu(context?.ToArray());
  }

  /// <summary>Получить контекстное меню</summary>
  /// <param name="context">Узлы - контекст меню</param>
  /// <returns>Список элементов контекстного меню</returns>
  public virtual List<MenuButtonItem> GetContexMenu(DocumentTreeNode[] context)
  {
    try
    {
      List<MenuButtonItem> contextMenu = NodeContextMenu.GetContextMenu(this, this.DocumentManager?.CommandManager, context);
      GetCustomElementContextMenu_EventHandler elementContextMenu = this.GetCustomElementContextMenu;
      if (elementContextMenu != null)
        elementContextMenu((object) this, new GetCustomElementContextMenu_EventArgs(context, contextMenu));
      return contextMenu;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      return new List<MenuButtonItem>();
    }
  }

  /// <summary>Можно ли вставить в контекст из буфера Windows</summary>
  /// <param name="context">Контекст</param>
  /// <returns>Можно ли вставить в контекст из буфера Windows</returns>
  public virtual bool CanPasteFromClipboard(DocumentTreeNode[] context)
  {
    try
    {
      return context.Length == 1 && !context[0].ReadOnlyStructure && NodeClipboardHelper.CanPasteFromClipboard(context[0]);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
      return false;
    }
  }

  /// <summary>Событие изменения свойства Scale</summary>
  public event EventHandler ZoomValueChanged;

  /// <summary>Позиция курсора мыши на странице в мм</summary>
  public PointF PageCursorPosition
  {
    [DebuggerStepThrough] get => this.pageCursorPosition;
  }

  internal void AssignPageCursorPosition(Page page, PointF position)
  {
    this.pageCursorPosition = position;
    this.OnPageCursorPositionChanged(new PageCursorPositionChanged_EventArgs(page, this.pageCursorPosition));
  }

  /// <summary>Событие Позиция курсора на странице изменилась </summary>
  public event PageCursorPositionChanged_EventHandler PageCursorPositionChanged;

  private void OnPageCursorPositionChanged(PageCursorPositionChanged_EventArgs e)
  {
    try
    {
      PageCursorPositionChanged_EventHandler cursorPositionChanged = this.PageCursorPositionChanged;
      if (cursorPositionChanged == null)
        return;
      cursorPositionChanged((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Событие - изменение значения одного из скролингов</summary>
  public event ScrollValueChanged_EventHandler ScrollValueChanged;

  /// <summary>Метод вызывающий обработчики события ScrollValueChanged</summary>
  protected virtual void OnScrollValueChanged()
  {
    try
    {
      ScrollValueChanged_EventHandler scrollValueChanged = this.ScrollValueChanged;
      if (scrollValueChanged == null)
        return;
      scrollValueChanged((object) this, new ScrollValueChanged_EventArgs());
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Получить видимый прямоугольник контрола страницы</summary>
  public Rectangle GetPageControlViewRectangle()
  {
    try
    {
      return this.PageControl == null ? new Rectangle(0, 0, 0, 0) : this.PageControl.Bounds;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return new Rectangle(0, 0, 0, 0);
  }

  /// <summary>Подвинуть изображение, чтобы было видно выделение</summary>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  public void ScrollSelectionToView(bool showFull, bool showLeftTop)
  {
    this.Document.SuspendRefreshUI();
    try
    {
      try
      {
        Point point1 = new Point(int.MaxValue, int.MaxValue);
        Point point2 = new Point(int.MinValue, int.MinValue);
        PageData pageData = (PageData) null;
        Rectangle bounds;
        for (int index = 0; index < this.selectedNodes.Count; ++index)
        {
          if (this.selectedNodes[index] is IPageElementWithInterface selectedNode && selectedNode.PageUI != null && selectedNode is PageElementNode pageElementNode && pageElementNode.Page != null)
          {
            if (pageData != null && pageData == pageElementNode.Page)
            {
              bounds = selectedNode.PageUI.Bounds;
              if (point1.X > bounds.X)
                point1.X = bounds.X;
              if (point2.X < bounds.Right)
                point2.X = bounds.Right;
              if (point1.Y > bounds.Y)
                point1.Y = bounds.Y;
              if (point2.Y < bounds.Bottom)
                point2.Y = bounds.Bottom;
            }
            else if (pageData == null || pageData.Index > pageElementNode.Page.Index)
            {
              pageData = pageElementNode.Page;
              bounds = selectedNode.PageUI.Bounds;
              point1 = bounds.Location;
              point2 = new Point(bounds.Right, bounds.Bottom);
            }
          }
        }
        TextBoxElement activeElement = this.activeElement as TextBoxElement;
        Point? caret = new Point?();
        int? rowHeight = new int?();
        if (activeElement != null && activeElement.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) activeElement.TextBox;
          if (textBox != null && textBox.EditorControl != null)
          {
            Point location = textBox.EditorControl.Location;
            Point textCursorCoor = textBox.GetTextCursorCoor();
            caret = new Point?(new Point(textCursorCoor.X + location.X, textCursorCoor.Y + location.Y));
            rowHeight = new int?(textBox.GetCurLineHeight());
          }
        }
        Page page = pageData as Page;
        if (this.ActivePage != page && page != null)
          this.ActivePage = page;
        this.ScrollToViewRectangle(Rectangle.FromLTRB(point1.X, point1.Y, point2.X, point2.Y), caret, rowHeight, showFull, showLeftTop);
      }
      finally
      {
        this.Document.ResumeRefreshUI(true);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установить скроллинг, так чтобы был виден прямоугольник</summary>
  /// <param name="bounds">Видимый прямоугольник</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  public void ScrollToViewRectangle(Rectangle bounds, bool showFull, bool showLeftTop)
  {
    this.ScrollToViewRectangle(bounds, new Point?(), new int?(), showFull, showLeftTop);
  }

  /// <summary>Установить скроллинг, так чтобы был виден прямоугольник</summary>
  /// <param name="bounds">Видимый прямоугольник</param>
  /// <param name="caret">Положение каретки в тексте</param>
  /// <param name="rowHeight">Высота строки текста</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  public virtual void ScrollToViewRectangle(
    Rectangle bounds,
    Point? caret,
    int? rowHeight,
    bool showFull,
    bool showLeftTop)
  {
    try
    {
      showLeftTop = false;
      if (this.SuspendedScrollBars)
        return;
      Rectangle controlViewRectangle = this.GetPageControlViewRectangle();
      int num1 = 0;
      int num2 = 0;
      if (!controlViewRectangle.Contains(bounds))
      {
        Point empty = Point.Empty;
        if (caret.HasValue)
        {
          empty = caret.Value;
          if (!bounds.Contains(empty))
            caret = new Point?();
        }
        if (!caret.HasValue || !rowHeight.HasValue)
        {
          if (showLeftTop || bounds.Right <= controlViewRectangle.X || showFull && bounds.X < controlViewRectangle.X || bounds.X >= controlViewRectangle.Right && bounds.Width > controlViewRectangle.Width)
            num1 = bounds.X - controlViewRectangle.X;
          else if (bounds.X >= controlViewRectangle.Right && (showFull || bounds.Width <= controlViewRectangle.Width) || showFull && bounds.Right > controlViewRectangle.Right)
            num1 = bounds.Right - controlViewRectangle.Right;
          if (showLeftTop || bounds.Bottom <= controlViewRectangle.Y || showFull && bounds.Y < controlViewRectangle.Y || bounds.Y >= controlViewRectangle.Bottom && bounds.Height > controlViewRectangle.Height)
            num2 = bounds.Y - controlViewRectangle.Y;
          else if (bounds.Y >= controlViewRectangle.Bottom && (showFull || bounds.Height <= controlViewRectangle.Height) || showFull && bounds.Bottom > controlViewRectangle.Bottom)
            num2 = bounds.Height > controlViewRectangle.Height ? bounds.Y - controlViewRectangle.Y : bounds.Bottom - controlViewRectangle.Bottom;
        }
        else
        {
          int num3 = rowHeight.Value;
          if (bounds.Right <= controlViewRectangle.X || empty.X <= controlViewRectangle.X || showFull && bounds.X < controlViewRectangle.X || bounds.X >= controlViewRectangle.Right && bounds.Width > controlViewRectangle.Width)
            num1 = empty.X - controlViewRectangle.Width / 2 - controlViewRectangle.X;
          else if (bounds.X >= controlViewRectangle.Right && (showFull || bounds.Width <= controlViewRectangle.Width) || showFull && bounds.Right > controlViewRectangle.Right || empty.X + num3 > controlViewRectangle.Right)
            num1 = empty.X - controlViewRectangle.Width / 2 - controlViewRectangle.X;
          if (bounds.Bottom <= controlViewRectangle.Y || showFull && bounds.Y < controlViewRectangle.Y || bounds.Y >= controlViewRectangle.Bottom && bounds.Height > controlViewRectangle.Height)
            num2 = empty.Y + num3 - bounds.Y < controlViewRectangle.Height ? bounds.Y - controlViewRectangle.Y : empty.Y - num3 / 2 - controlViewRectangle.Y;
          else if (empty.Y < controlViewRectangle.Y)
            num2 = empty.Y - controlViewRectangle.Y;
          else if (bounds.Y >= controlViewRectangle.Bottom && (showFull || bounds.Height <= controlViewRectangle.Height) || showFull && bounds.Bottom > controlViewRectangle.Bottom)
            num2 = bounds.Bottom - controlViewRectangle.Bottom;
          else if (empty.Y + num3 > controlViewRectangle.Bottom)
            num2 = empty.Y + 2 * num3 - controlViewRectangle.Bottom;
        }
      }
      int num4 = controlViewRectangle.X + num1;
      int num5 = controlViewRectangle.Y + num2;
      ++this.suspendScrollBars;
      try
      {
        Rectangle layoutBounds = this.PageControl.LayoutBounds;
        int num6 = -layoutBounds.X + num4;
        if (num6 < this.hScrollBar.Minimum)
          num6 = this.hScrollBar.Minimum;
        if (num6 > this.hScrollBar.Maximum)
          num6 = this.hScrollBar.Maximum;
        this.hScrollBar.Value = num6;
        layoutBounds = this.PageControl.LayoutBounds;
        int num7 = -layoutBounds.Y + num5;
        if (num7 < this.vScrollBar.Minimum)
          num7 = this.vScrollBar.Minimum;
        if (num7 > this.vScrollBar.Maximum)
          num7 = this.vScrollBar.Maximum;
        this.vScrollBar.Value = num7;
      }
      finally
      {
        --this.suspendScrollBars;
      }
      if (num1 == 0 && num2 == 0)
        return;
      this.ScrollBars_ValueChanged((object) null, (EventArgs) null);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void ScrollBars_ValueChanged(object sender, EventArgs e)
  {
    this.ScrollBars_ValueChanged(false);
  }

  private void ScrollBars_ValueChanged(bool ignoreLocked)
  {
    try
    {
      if (this.SuspendedScrollBars && !ignoreLocked || this.PageControl == null)
        return;
      if (this.vScrollBar.Value < this.vScrollBar.Minimum)
        this.vScrollBar.Value = this.vScrollBar.Minimum;
      int maximum1 = this.vScrollBar.Maximum;
      if (this.vScrollBar.Value > maximum1)
        this.vScrollBar.Value = maximum1;
      if (this.hScrollBar.Value < this.hScrollBar.Minimum)
        this.hScrollBar.Value = this.hScrollBar.Minimum;
      int maximum2 = this.hScrollBar.Maximum;
      if (this.hScrollBar.Value > maximum2)
        this.hScrollBar.Value = maximum2;
      if (this.Document != null)
        this.Document.SuspendRefreshUI();
      bool refresh = false;
      try
      {
        Size offset;
        ref Size local = ref offset;
        int num1 = this.hScrollBar.Value;
        Rectangle layoutBounds = this.PageControl.LayoutBounds;
        int x = layoutBounds.X;
        int width = num1 + x;
        int num2 = this.vScrollBar.Value;
        layoutBounds = this.PageControl.LayoutBounds;
        int y = layoutBounds.Y;
        int height = num2 + y;
        local = new Size(width, height);
        refresh = !offset.IsEmpty;
        this.PageControl.MoveLayout(offset);
      }
      finally
      {
        if (this.Document != null)
          this.Document.ResumeRefreshUI(refresh);
      }
      this.OnScrollValueChanged();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Ширина области в которой виден документ</summary>
  public int DocRegionWidth
  {
    [DebuggerStepThrough] get => this.subPanel.Width - 2 * this.margin;
  }

  /// <summary>Высота области в которой виден документ</summary>
  public int DocRegionHeight
  {
    [DebuggerStepThrough] get => this.subPanel.Height - 2 * this.margin;
  }

  /// <summary>Обновить полосы прокрутки</summary>
  public void UpdateScrollBars(bool ignoreLocked)
  {
    try
    {
      if (this.PageControl == null)
        return;
      this.vScrollBar.BringToFront();
      ++this.suspendScrollBars;
      int num1 = this.vScrollBar.Value;
      try
      {
        int height = this.PageControl.LayoutBounds.Height;
        int num2 = height >= this.PageControl.Height ? height + this.margin : this.PageControl.Height;
        if (num2 < 0)
          num2 = 1;
        this.vScrollBar.Minimum = 0;
        this.vScrollBar.Maximum = num2;
        this.vScrollBar.SmallChange = Convert.ToInt32(this.PageControl.Height / 5);
        this.vScrollBar.LargeChange = Convert.ToInt32(this.PageControl.Height);
        this.PageControl.SetScrollBarValue((ScrollBar) this.vScrollBar, this.vScrollBar.Value);
        int width = this.PageControl.LayoutBounds.Width;
        int num3 = width >= this.PageControl.Width ? width + this.margin : this.PageControl.Width;
        if (num3 < 0)
          num3 = 1;
        this.hScrollBar.Minimum = 0;
        this.hScrollBar.Maximum = num3;
        this.hScrollBar.SmallChange = Convert.ToInt32(this.PageControl.Width / 5);
        this.hScrollBar.LargeChange = Convert.ToInt32(this.PageControl.Width);
        this.PageControl.SetScrollBarValue((ScrollBar) this.hScrollBar, this.hScrollBar.Value);
      }
      finally
      {
        --this.suspendScrollBars;
      }
      this.ScrollBars_ValueChanged(ignoreLocked);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void DocumentControl_Resize(object sender, EventArgs e)
  {
    if (this.LockForClosing)
      return;
    this.UpdateScrollBars(false);
  }

  /// <summary>Вызвать событие MouseWheel</summary>
  /// <param name="e">Аргументы события</param>
  protected override void OnMouseWheel(MouseEventArgs e)
  {
    if (this.LockForClosing)
      return;
    try
    {
      switch (Control.ModifierKeys)
      {
        case Keys.Control:
          if (this.Document != null)
          {
            float documentScale = this.documentScale;
            float num1 = 0.1f;
            float num2 = Math.Abs((float) e.Delta / 120f * num1);
            if (e.Delta > 0)
              documentScale += num2;
            else if (e.Delta < 0)
              documentScale -= num2;
            if ((double) documentScale >= 0.1 && (double) documentScale <= 5.0 && (double) documentScale != (double) this.documentScale)
            {
              double num3 = (double) this.SetZoom(DocZoomMode.Custom, documentScale);
              if (this.DocumentManager != null && this.DocumentManager.CommandManager != null)
              {
                this.DocumentManager.CommandManager.QueryStatus();
                break;
              }
              break;
            }
            break;
          }
          break;
        case Keys.Alt:
          int num4 = this.hScrollBar.Value - e.Delta;
          int num5 = this.hScrollBar.Maximum - this.subPanel.Width;
          if (num5 < 0)
            num5 = 0;
          if (num4 < this.hScrollBar.Minimum)
            num4 = this.hScrollBar.Minimum;
          if (num4 > num5)
            num4 = num5;
          this.hScrollBar.Value = num4;
          break;
        default:
          int num6 = this.vScrollBar.Value - e.Delta;
          int num7 = this.vScrollBar.Maximum - this.subPanel.Height;
          if (num7 < 0)
            num7 = 0;
          if (num6 < this.vScrollBar.Minimum)
            num6 = this.vScrollBar.Minimum;
          if (num6 > num7)
            num6 = num7;
          this.vScrollBar.Value = num6;
          break;
      }
      base.OnMouseWheel(e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Получить активный редактор</summary>
  /// <returns>Активный редактор</returns>
  public virtual ImRtfEditor GetActiveEditorControl()
  {
    try
    {
      if (this.ActiveElement is TextBoxElement activeElement)
      {
        if (activeElement.InPlaceEditorActive)
        {
          if (activeElement.TextBox != null)
            return activeElement.TextBox.EditorControl as ImRtfEditor;
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return (ImRtfEditor) null;
  }

  /// <summary>Обновить меню и инструменты форматирования</summary>
  public virtual void UpdateFormatCommands()
  {
    try
    {
      if (this.documentManager != null)
        this.documentManager.UpdateFormatCommands();
      this.SetIdentsToRuler();
      this.rulerHorizontal.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public ImRtfEditor TernEditorBuffer
  {
    get => this.ternEditorBuffer;
    set
    {
      if (this.ternEditorBuffer != null)
      {
        this.ternEditorBuffer.UndoSaved -= new EventHandler(this.ternEditorBuffer_UndoSaved);
        this.ternEditorBuffer.LostFocus -= new EventHandler(this.ternEditorBuffer_LostFocus);
      }
      this.ternEditorBuffer = value;
      if (this.ternEditorBuffer == null)
        return;
      this.ternEditorBuffer.UndoSaved += new EventHandler(this.ternEditorBuffer_UndoSaved);
      this.ternEditorBuffer.LostFocus += new EventHandler(this.ternEditorBuffer_LostFocus);
    }
  }

  private void ternEditorBuffer_LostFocus(object sender, EventArgs e)
  {
    if (this.LockForClosing)
      return;
    try
    {
      if (this.DocumentEditorForm == null || this.DocumentEditorForm.UndoManager == null)
        return;
      List<IUndoAction> undoActionList = new List<IUndoAction>();
      undoActionList.AddRange((IEnumerable<IUndoAction>) this.DocumentEditorForm.UndoManager.Actions);
      this.DocumentEditorForm.UndoManager.Actions.Clear();
      foreach (IUndoAction undoAction in undoActionList)
      {
        if (!(undoAction is UndoEditorAction))
          this.DocumentEditorForm.UndoManager.Actions.Add(undoAction);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void ternEditorBuffer_UndoSaved(object sender, EventArgs e)
  {
    try
    {
      if (this.DocumentEditorForm == null || this.DocumentEditorForm.UndoManager == null)
        return;
      this.DocumentEditorForm.UndoManager.CreateUndo((IUndoAction) new UndoEditorAction(this.DocumentEditorForm.UndoManager), false);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Сбросить буфер контрола редактора RTF</summary>
  public void ResetTernBufer() => this.ternEditorBuffer = (ImRtfEditor) null;

  /// <summary>Выбранные узлы</summary>
  public List<DocumentTreeNode> SelectedNodes
  {
    [DebuggerStepThrough] get => this.selectedNodes;
  }

  /// <summary>Выбранный узел, если выбран 1 узел</summary>
  public DocumentTreeNode SelectedNode
  {
    get
    {
      return this.selectedNodes != null && this.selectedNodes.Count == 1 ? this.selectedNodes[0] : (DocumentTreeNode) null;
    }
  }

  /// <summary>Есть отложенное выделение или переход на страницу</summary>
  public bool HasSuspendedSelection
  {
    [DebuggerStepThrough] get
    {
      return this.suspendedSelection != null || this.suspendedLastPage || this.suspendedActivePage != null;
    }
  }

  /// <summary>Отложенное выделение. Используется если выбранный пользователем элемент занят фоновым потоком</summary>
  public DocumentTreeNode SuspendedSelection
  {
    [DebuggerStepThrough] get => this.suspendedSelection;
  }

  public virtual bool SuspendedScrollBars
  {
    [DebuggerStepThrough] get => this.suspendScrollBars > 0;
  }

  public void SuspendScrollBars() => ++this.suspendScrollBars;

  public void ResumeScrollBars(bool update)
  {
    if (this.suspendScrollBars > 0)
      --this.suspendScrollBars;
    if (!(!this.SuspendedScrollBars & update))
      return;
    this.ScrollBars_ValueChanged((object) null, (EventArgs) null);
  }

  /// <summary>Отложенный переход на другую страницу</summary>
  public Page SuspendedActivePage
  {
    [DebuggerStepThrough] get => this.suspendedActivePage;
  }

  /// <summary>Отложенный переход на последнюю страницу</summary>
  public bool SuspendedLastPage
  {
    [DebuggerStepThrough] get => this.suspendedLastPage;
  }

  /// <summary>Получить выбранные узлы </summary>
  /// <returns>Массив выбранных узлов</returns>
  public DocumentTreeNode[] GetSelectedNodes()
  {
    DocumentTreeNode[] array;
    if (this.SelectedNodes != null && this.SelectedNodes.Count != 0)
    {
      array = new DocumentTreeNode[this.SelectedNodes.Count];
      this.SelectedNodes.CopyTo(array);
    }
    else
      array = new DocumentTreeNode[0];
    return array;
  }

  /// <summary>Выбрать узел</summary>
  /// <param name="node">Узел</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  public virtual void SelectNode(
    DocumentTreeNode node,
    bool inPlaceEditEnabled,
    Point cursorPosition)
  {
    try
    {
      if (!this.EditorValidating())
        return;
      this.suspendedSelection = (DocumentTreeNode) null;
      this.suspendedActivePage = (Page) null;
      this.suspendedLastPage = false;
      switch (node)
      {
        case PageElementNode pageElementNode:
          if (pageElementNode.Page != null && pageElementNode.Page.IsLocked)
          {
            this.suspendedSelection = (DocumentTreeNode) pageElementNode;
            node = (DocumentTreeNode) null;
            break;
          }
          break;
        case PageData pageData:
          if (pageData.IsLocked)
          {
            this.suspendedSelection = (DocumentTreeNode) pageData;
            node = (DocumentTreeNode) null;
            break;
          }
          break;
      }
      DocumentControl.SetShowSelected(node, true, true);
      if (this.selectedNodes.IndexOf(node) != -1)
        return;
      if (this.selectedNodes.Count == 1)
        DocumentControl.RefreshShowSelected(this.selectedNodes[0], true);
      this.selectedNodes.Add(node);
      this.UpdateActiveElement(inPlaceEditEnabled, cursorPosition);
      this.OnSelectionChanged();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Отменить выбор узла</summary>
  /// <param name="node">Узел</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  public virtual void UnselectNode(DocumentTreeNode node, bool inPlaceEditEnabled)
  {
    if (!this.EditorValidating())
      return;
    try
    {
      DocumentControl.SetShowSelected(node, false, true);
      if (this.selectedNodes.IndexOf(node) <= -1)
        return;
      this.selectedNodes.Remove(node);
      if (this.selectedNodes.Count == 1)
        DocumentControl.RefreshShowSelected(this.selectedNodes[0], true);
      this.UpdateActiveElement(inPlaceEditEnabled, Point.Empty);
      this.OnSelectionChanged();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обновить вид выбранных узлов</summary>
  public void RefreshSelected()
  {
    try
    {
      for (int index = 0; index < this.selectedNodes.Count; ++index)
      {
        if (this.selectedNodes[index] is VisualNode selectedNode)
          selectedNode.RefreshUI();
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Добавить узлы к выделению</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  public virtual void AddNodesToSelection(
    IList<DocumentTreeNode> nodes,
    bool inPlaceEditEnabled,
    Point cursorPosition)
  {
    try
    {
      if (!this.EditorValidating())
        return;
      this.Document.SuspendRefreshUI();
      if (nodes != null)
      {
        for (int index = 0; index < nodes.Count; ++index)
        {
          if ((!(nodes[index] is PageElementNode node1) || node1.Page == null || !node1.Page.IsLocked) && (!(nodes[index] is PageData node2) || !node2.IsLocked) && this.selectedNodes.IndexOf(nodes[index]) == -1)
          {
            this.selectedNodes.Add(nodes[index]);
            DocumentControl.SetShowSelected(nodes[index], true, false);
          }
        }
      }
      this.UpdateActiveElement(inPlaceEditEnabled, cursorPosition);
      this.OnSelectionChanged();
      this.Document.ResumeRefreshUI(true);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Переключить выделение узлов</summary>
  /// <param name="nodes">Узлы</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  public virtual void ToggleNodesSelection(
    IList<DocumentTreeNode> nodes,
    bool inPlaceEditEnabled,
    Point cursorPosition)
  {
    try
    {
      if (!this.EditorValidating())
        return;
      this.Document.SuspendRefreshUI();
      if (nodes != null)
      {
        if (!this.MultiSelect)
        {
          List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
          documentTreeNodeList.AddRange((IEnumerable<DocumentTreeNode>) this.selectedNodes);
          foreach (DocumentTreeNode node in documentTreeNodeList)
            this.UnselectNode(node, false);
        }
        List<DocumentTreeNode> documentTreeNodeList1 = new List<DocumentTreeNode>();
        List<DocumentTreeNode> collection = new List<DocumentTreeNode>();
        for (int index = 0; index < nodes.Count; ++index)
        {
          DocumentTreeNode node1 = nodes[index];
          if ((!(nodes[index] is PageElementNode node2) || node2.Page == null || !node2.Page.IsLocked) && (!(nodes[index] is PageData node3) || !node3.IsLocked))
          {
            if (this.selectedNodes.IndexOf(nodes[index]) == -1)
            {
              if (!collection.Contains(node1))
                collection.Add(node1);
            }
            else if (!documentTreeNodeList1.Contains(node1))
              documentTreeNodeList1.Add(node1);
          }
        }
        for (int index1 = 0; index1 < documentTreeNodeList1.Count; ++index1)
        {
          int index2 = this.selectedNodes.IndexOf(documentTreeNodeList1[index1]);
          DocumentControl.SetShowSelected(documentTreeNodeList1[index1], false, false);
          this.selectedNodes.RemoveAt(index2);
        }
        List<DocumentTreeNode> Selection = new List<DocumentTreeNode>();
        Selection.AddRange((IEnumerable<DocumentTreeNode>) this.selectedNodes);
        Selection.AddRange((IEnumerable<DocumentTreeNode>) collection);
        this.OnBeforeSelectionChanged(new BeforeSelectionChanged_EventArgs(Selection));
        this.selectedNodes.Clear();
        foreach (DocumentTreeNode selectedNode in this.selectedNodes)
        {
          if (!Selection.Contains(selectedNode))
          {
            DocumentControl.SetShowSelected(selectedNode, false, false);
            this.selectedNodes.Remove(selectedNode);
          }
        }
        foreach (DocumentTreeNode node in Selection)
        {
          this.selectedNodes.Add(node);
          DocumentControl.SetShowSelected(node, true, false);
        }
      }
      this.UpdateActiveElement(inPlaceEditEnabled, cursorPosition);
      this.OnSelectionChanged();
      this.Document.ResumeRefreshUI(true);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Переключить выделение узла</summary>
  /// <param name="node">Узел</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  public virtual void ToggleNodeSelection(
    DocumentTreeNode node,
    bool inPlaceEditEnabled,
    Point cursorPosition)
  {
    try
    {
      this.ToggleNodesSelection((IList<DocumentTreeNode>) new List<DocumentTreeNode>()
      {
        node
      }, inPlaceEditEnabled, cursorPosition);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установить выбранные узлы</summary>
  /// <param name="selection">Новый выбранный узел</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  public void SetSelection(DocumentTreeNode selection, bool showFull, bool showLeftTop)
  {
    this.SetSelection(selection, false, Point.Empty, showFull, showLeftTop);
  }

  /// <summary>Установить выбранные узлы</summary>
  /// <param name="selection">Новый выбранный узел</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в выбранном элементе</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  public void SetSelection(
    DocumentTreeNode selection,
    bool inPlaceEditEnabled,
    Point cursorPosition,
    bool showFull,
    bool showLeftTop)
  {
    List<DocumentTreeNode> selection1 = new List<DocumentTreeNode>();
    if (selection != null)
      selection1.Add(selection);
    this.SetSelection(selection1, inPlaceEditEnabled, cursorPosition, showFull, showLeftTop);
  }

  /// <summary>Установить выбранные узлы</summary>
  /// <param name="selection">Новые выбранные узлы</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  public virtual void SetSelection(
    List<DocumentTreeNode> selection,
    bool showFull,
    bool showLeftTop)
  {
    this.SetSelection(selection, false, Point.Empty, showFull, showLeftTop);
  }

  /// <summary>Установить выбранные узлы</summary>
  /// <param name="selection">Новые выбранные узлы</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  /// <param name="showFull">Показать весь прямоугольник, даже если его часть уже видна</param>
  /// <param name="showLeftTop">Левый верхний угол</param>
  public virtual void SetSelection(
    List<DocumentTreeNode> selection,
    bool inPlaceEditEnabled,
    Point cursorPosition,
    bool showFull,
    bool showLeftTop)
  {
    try
    {
      if (selection != null && selection.Count > 0)
      {
        for (int index = selection.Count - 1; index >= 0; --index)
        {
          if (selection[index] == null)
            selection.RemoveAt(index);
        }
      }
      if (!this.MultiSelect && selection != null && selection.Count > 0)
      {
        if (selection[0] is TableData && (selection[0] as TableData).IsVirtualNode && selection[0].NodesCount > 0)
        {
          DocumentTreeNode node = selection[0].Nodes[selection[0].Nodes.Count - 1];
          selection.Clear();
          selection.Add(node);
        }
        if (selection.Count > 1)
        {
          DocumentTreeNode documentTreeNode = selection[selection.Count - 1];
          selection.Clear();
          selection.Add(documentTreeNode);
        }
      }
      if (this.Document == null || DocumentTreeNodeCollection.ContentEquals((IList<DocumentTreeNode>) this.selectedNodes, (IList<DocumentTreeNode>) selection) || !this.EditorValidating())
        return;
      ImDocument document = this.Document;
      document.SuspendRefreshUI();
      try
      {
        if (!selection.IsEmpty<DocumentTreeNode>() && this.document.LoadFromStreamThread != null && (this.document.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running && this.document.DistributeThread != null && (this.document.DistributeThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running)
        {
          List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>((IEnumerable<DocumentTreeNode>) selection);
          do
          {
            for (int index = documentTreeNodeList.Count - 1; index >= 0; --index)
            {
              if (documentTreeNodeList[index] is PageElementNode pageElementNode)
              {
                if (pageElementNode.Page != null && pageElementNode.Page.IsWaitForDistributed)
                {
                  Thread.Sleep(0);
                  break;
                }
                documentTreeNodeList.RemoveAt(index);
              }
              else
              {
                if (documentTreeNodeList[index] is PageData pageData && pageData.IsWaitForDistributed)
                {
                  Thread.Sleep(0);
                  break;
                }
                documentTreeNodeList.RemoveAt(index);
              }
            }
          }
          while (documentTreeNodeList.Count != 0 && this.document.LoadFromStreamThread != null && (this.document.LoadFromStreamThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running && this.document.DistributeThread != null && (this.document.DistributeThread.ThreadState & (System.Threading.ThreadState.Stopped | System.Threading.ThreadState.Aborted)) == System.Threading.ThreadState.Running);
        }
        this.suspendedSelection = (DocumentTreeNode) null;
        this.suspendedActivePage = (Page) null;
        this.suspendedLastPage = false;
        List<DocumentTreeNode> selectedNodes = this.selectedNodes;
        this.selectedNodes = selection ?? new List<DocumentTreeNode>();
        this.OnBeforeSelectionChanged(new BeforeSelectionChanged_EventArgs(this.selectedNodes));
        for (int index = 0; index < selectedNodes.Count; ++index)
        {
          if (!this.selectedNodes.Contains(selectedNodes[index]))
            DocumentControl.SetShowSelected(selectedNodes[index], false, false);
        }
        Point point1 = new Point(int.MaxValue, int.MaxValue);
        Point point2 = new Point(int.MinValue, int.MinValue);
        for (int index = 0; index < this.selectedNodes.Count; ++index)
        {
          if (this.selectedNodes[index] is PageElementNode selectedNode)
            selectedNode.UpdateUIGeometry(false);
        }
        this.UpdateActiveElement(inPlaceEditEnabled, cursorPosition);
        PageData pageData1 = (PageData) null;
        PageElementUI pageElementUi = (PageElementUI) null;
        PageData pageData2 = (PageData) null;
        for (int index = 0; index < this.selectedNodes.Count; ++index)
        {
          IPageElementWithInterface selectedNode1 = this.selectedNodes[index] as IPageElementWithInterface;
          PageElementNode selectedNode2 = this.selectedNodes[index] as PageElementNode;
          if (this.selectedNodes[index] is ImDocumentData)
          {
            ImDocumentData selectedNode3 = this.selectedNodes[index] as ImDocumentData;
            if (selectedNode3.NodesCount > 0 && this.ActivePage != null && this.ActivePage.Parent != selectedNode3)
            {
              pageData2 = (PageData) (selectedNode3.Nodes[0] as Page);
              pageElementUi = (PageElementUI) (pageData2 as Page).PageUI;
            }
          }
          else if (this.selectedNodes[index] is Page)
          {
            pageData2 = (PageData) (this.selectedNodes[index] as Page);
            pageElementUi = (PageElementUI) (pageData2 as Page).PageUI;
          }
          else
          {
            if (selectedNode1 != null)
              pageElementUi = selectedNode1.PageUI;
            if (selectedNode2 != null)
              pageData2 = selectedNode2.Page;
          }
          Rectangle bounds;
          if (pageData2 != null && pageElementUi != null)
          {
            if (pageData1 != null && pageData1 == pageData2)
            {
              bounds = pageElementUi.Bounds;
              if (point1.X > bounds.X)
                point1.X = bounds.X;
              if (point2.X < bounds.Right)
                point2.X = bounds.Right;
              if (point1.Y > bounds.Y)
                point1.Y = bounds.Y;
              if (point2.Y < bounds.Bottom)
                point2.Y = bounds.Bottom;
            }
            else if (pageData1 == null || pageData1.Index > pageData2.Index)
            {
              pageData1 = pageData2;
              bounds = pageElementUi.Bounds;
              point1 = bounds.Location;
              point2 = new Point(bounds.Right, bounds.Bottom);
            }
          }
          DocumentControl.SetShowSelected(this.selectedNodes[index], true, false);
        }
        this.SelectDataCellInTemplate((IList<DocumentTreeNode>) this.selectedNodes);
        this.OnSelectionChanged();
        TextBoxElement activeElement = this.activeElement as TextBoxElement;
        Point? caret = new Point?();
        int? rowHeight = new int?();
        if (activeElement != null && activeElement.InPlaceEditorActive)
        {
          InSiteEditorWrapper textBox = (InSiteEditorWrapper) activeElement.TextBox;
          if (textBox != null && textBox.EditorControl != null)
          {
            Point location = textBox.EditorControl.Location;
            Point textCursorCoor = textBox.GetTextCursorCoor();
            caret = new Point?(new Point(textCursorCoor.X + location.X, textCursorCoor.Y + location.Y));
            rowHeight = new int?(textBox.GetCurLineHeight());
          }
        }
        Page page = pageData1 as Page;
        if (this.ActivePage != page && page != null)
          this.ActivePage = page;
        if (this.SelectedNodes.Count <= 0)
          return;
        this.ScrollToViewRectangle(Rectangle.FromLTRB(point1.X, point1.Y, point2.X, point2.Y), caret, rowHeight, showFull, showLeftTop);
      }
      finally
      {
        document?.ResumeRefreshUI(true);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Разбить узлы документа по родительским таблицам и их ячейкам. Остальные узлы игнорируются</summary>
  /// <param name="nodes">Список узлов документа</param>
  /// <returns></returns>
  private Dictionary<TableData, List<RectangleElement>> SelectCellsParents(
    IList<DocumentTreeNode> nodes)
  {
    Dictionary<TableData, List<RectangleElement>> dictionary = new Dictionary<TableData, List<RectangleElement>>();
    foreach (DocumentTreeNode node in (IEnumerable<DocumentTreeNode>) nodes)
    {
      if (node is RectangleElement rectangleElement && rectangleElement.ParentCell != null)
      {
        List<RectangleElement> rectangleElementList;
        if (!dictionary.TryGetValue(rectangleElement.ParentCell, out rectangleElementList))
        {
          rectangleElementList = new List<RectangleElement>();
          dictionary.Add(rectangleElement.ParentCell, rectangleElementList);
        }
        rectangleElementList.Add(rectangleElement);
      }
    }
    return dictionary;
  }

  private List<DocumentTreeNode> SelectSwitchableCellsFromParents(IList<DocumentTreeNode> nodes)
  {
    List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
    if (nodes == null || nodes.Count == 0)
      return documentTreeNodeList;
    foreach (DocumentTreeNode node in (IEnumerable<DocumentTreeNode>) nodes)
    {
      if (node is RectangleElement rectangleElement)
      {
        RectangleElement dataCellInHierarchy = rectangleElement.FindSwitchableDataCellInHierarchy();
        if (dataCellInHierarchy != null)
          documentTreeNodeList.Add((DocumentTreeNode) dataCellInHierarchy);
      }
    }
    return documentTreeNodeList;
  }

  /// <summary>Выбрать необязательные ячейки в таблицах, которые должны отображаться, и спрятать остальные</summary>
  /// <param name="nodes">Выбранные узлы</param>
  private void SelectDataCellInTemplate(IList<DocumentTreeNode> nodes)
  {
    foreach (KeyValuePair<TableData, List<RectangleElement>> selectCellsParent in this.SelectCellsParents((IList<DocumentTreeNode>) this.SelectSwitchableCellsFromParents(nodes)))
      selectCellsParent.Key.UpdateCells_IsVisibleNow(selectCellsParent.Value);
  }

  /// <summary>Установить свойство ShowSelected заданному узлу</summary>
  /// <param name="node">Узел</param>
  /// <param name="show">Значение свойства ShowSelected</param>
  public static void SetShowSelected(DocumentTreeNode node, bool show, bool invalidate)
  {
    try
    {
      if (node is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
      {
        elementWithInterface.PageUI.SetSelected(show, invalidate);
        if (show || !(node is TableElement tableElement) || tableElement.NextTable == null)
          return;
        for (TableElement nextTable = tableElement.NextTable as TableElement; nextTable != null; nextTable = nextTable.NextTable as TableElement)
        {
          if (nextTable.PageUI != null)
            nextTable.PageUI.SetSelected(show, invalidate);
        }
      }
      else
      {
        if (node == null || node.Nodes == null || !node.IsVirtualNode)
          return;
        for (int index = 0; index < node.Nodes.Count; ++index)
          DocumentControl.SetShowSelected(node.Nodes[index], show, invalidate);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установить свойство ShowSelected заданному узлу</summary>
  /// <param name="node">Узел</param>
  /// <param name="show">Обновлять если значение свойства ShowSelected равно show</param>
  public static void RefreshShowSelected(DocumentTreeNode node, bool show)
  {
    try
    {
      if (node is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI != null)
      {
        if (elementWithInterface.PageUI.IsSelected != show)
          return;
        elementWithInterface.PageUI.InvalidateUI();
      }
      else
      {
        if (node == null || node.Nodes == null || !node.IsVirtualNode)
          return;
        for (int index = 0; index < node.Nodes.Count; ++index)
          DocumentControl.RefreshShowSelected(node.Nodes[index], show);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Выбран ли узел</summary>
  /// <param name="node">Узел</param>
  /// <returns>Выбран ли узел</returns>
  public virtual bool NodeIsSelected(DocumentTreeNode node)
  {
    return node != null && this.selectedNodes != null && this.selectedNodes.Contains(node);
  }

  /// <summary>Принадлежит ли узел выбранным элементам</summary>
  /// <param name="node">Узел</param>
  internal bool NodeInSelection(DocumentTreeNode node)
  {
    if (node == null)
      return false;
    try
    {
      if (!(node is RectangleElement rectangleElement))
        this.NodeIsSelected(node);
      TableData topLevelTable = rectangleElement?.TopLevelTable;
      for (int index = 0; index < this.selectedNodes.Count; ++index)
      {
        if (this.selectedNodes[index] is RectangleElement selectedNode && selectedNode.TopLevelTable == topLevelTable)
          return true;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return false;
  }

  /// <summary>Отменить выбор для удаленных узлов</summary>
  public virtual void UnselectRemovedNodes()
  {
    try
    {
      for (int index = this.selectedNodes.Count - 1; index >= 0; --index)
      {
        if (this.selectedNodes[index] is IDocumentElement selectedNode && selectedNode.OwnerDocument == null)
        {
          DocumentControl.SetShowSelected(this.selectedNodes[index], false, false);
          this.selectedNodes.RemoveAt(index);
        }
      }
      if (this.ActivePage == null || this.ActivePage.Parent != null)
        return;
      this.SetActivePage((Page) null, false, false);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Событие Выделение изменено</summary>
  public event SelectionChanged_EventHandler SelectionChanged;

  /// <summary>Вызывает событие SelectionChanged</summary>
  protected virtual void OnSelectionChanged()
  {
    if (this.LockForClosing)
      return;
    this.OnSelectionChanged(new SelectionChanged_EventArgs());
  }

  /// <summary>Событие перед изменением выделения</summary>
  public event BeforeSelectionChanged_EventHandler BeforeSelectionChanged;

  /// <summary>Вызывает событие BeforeSelectionChanged</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnBeforeSelectionChanged(BeforeSelectionChanged_EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      BeforeSelectionChanged_EventHandler selectionChanged = this.BeforeSelectionChanged;
      if (selectionChanged == null)
        return;
      selectionChanged((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Событие Выделение изменено</summary>
  public event CanShiftSelect_EventHandler CanShiftSelect;

  /// <summary>Вызывает событие SelectionChanged</summary>
  /// <param name="e">Аргументы события</param>
  internal virtual void OnCanShiftSelect(CanShiftSelect_EventArgs e)
  {
    try
    {
      CanShiftSelect_EventHandler canShiftSelect = this.CanShiftSelect;
      if (canShiftSelect == null)
        return;
      canShiftSelect((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обновить активный элемент</summary>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPosition">Позиция курсора в координатах страницы</param>
  internal void UpdateActiveElement(bool inPlaceEditEnabled, Point cursorPosition)
  {
    try
    {
      if (this.SelectedNodes != null && this.SelectedNodes.Count == 1)
        this.SetActiveElement(this.SelectedNodes[0], inPlaceEditEnabled, cursorPosition);
      else
        this.SetActiveElement((DocumentTreeNode) null, inPlaceEditEnabled, cursorPosition);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Вызывает событие SelectionChanged</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnSelectionChanged(SelectionChanged_EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      this.SetRulerBorders();
      this.SetIdentsToRuler();
      this.rulerVertical.RebuildRulerCoords();
      this.rulerHorizontal.RebuildRulerCoords();
      this.rulerHorizontal.Refresh();
      this.rulerVertical.Refresh();
      SelectionChanged_EventHandler selectionChanged = this.SelectionChanged;
      if (selectionChanged != null)
        selectionChanged((object) this, e);
      this.DocumentManager?.SelectionChanged();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void QueryParagraphFormat(
    IList<DocumentTreeNode> context,
    ref ParagraphFormat queryParagraphFormat)
  {
    try
    {
      if (this.GetActiveEditorControl() == null)
      {
        bool firstLoad = true;
        this.QueryParagraphFormat(context, ref queryParagraphFormat, ref firstLoad);
      }
      else
      {
        if (this.TernEditorBuffer == null)
          return;
        int num1 = -1;
        int num2 = -1;
        int EndLine = -1;
        int EndCol = -1;
        if (!this.TernEditorBuffer.TerGetSelection(out num1, out num2, out EndLine, out EndCol))
        {
          this.TernEditorBuffer.TerAbsToRowCol(this.TernEditorBuffer.TerGetCaretPos(), out num1, out num2);
          EndLine = num1;
        }
        if (num1 > EndLine)
        {
          int num3 = EndLine;
          EndLine = num1;
          num1 = num3;
        }
        int? nullable1 = new int?(-1);
        int? nullable2 = new int?(-1);
        int? nullable3 = new int?(-1);
        for (int LineNo = num1; LineNo <= EndLine; ++LineNo)
        {
          int LeftIndent;
          int RightIndent;
          int FirstIndent;
          if (this.TernEditorBuffer.TerGetParaInfo(LineNo, out LeftIndent, out RightIndent, out FirstIndent, out int _, out int _, out int _, out int _, out int _, out int _, out int _, out int _, out int _))
          {
            if (LineNo == num1)
            {
              nullable1 = new int?(LeftIndent);
              nullable2 = new int?(RightIndent);
              nullable3 = new int?(FirstIndent);
            }
            else
            {
              int? nullable4;
              if (nullable1.HasValue)
              {
                nullable4 = nullable1;
                int num4 = LeftIndent;
                if (!(nullable4.GetValueOrDefault() == num4 & nullable4.HasValue))
                  nullable1 = new int?();
              }
              if (nullable2.HasValue)
              {
                nullable4 = nullable2;
                int num5 = RightIndent;
                if (!(nullable4.GetValueOrDefault() == num5 & nullable4.HasValue))
                  nullable2 = new int?();
              }
              if (nullable3.HasValue)
              {
                nullable4 = nullable3;
                int num6 = FirstIndent;
                if (!(nullable4.GetValueOrDefault() == num6 & nullable4.HasValue))
                  nullable3 = new int?();
              }
            }
          }
        }
        if (queryParagraphFormat == null)
          queryParagraphFormat = new ParagraphFormat(true);
        if (nullable3.HasValue)
          queryParagraphFormat.IdentFirstLine = new float?(UnitsConverter.TwipsToMm((float) nullable3.Value) / 10f);
        if (nullable1.HasValue)
          queryParagraphFormat.IdentLeft = new float?(UnitsConverter.TwipsToMm((float) nullable1.Value) / 10f);
        if (!nullable2.HasValue)
          return;
        queryParagraphFormat.IdentRight = new float?(UnitsConverter.TwipsToMm((float) nullable2.Value) / 10f);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void QueryParagraphFormat(
    IList<DocumentTreeNode> context,
    ref ParagraphFormat queryParagraphFormat,
    ref bool firstLoad)
  {
    if (context == null || context.Count <= 0)
      return;
    for (int index = 0; index < context.Count; ++index)
      this.QueryParagraphFormat(context[index], ref queryParagraphFormat, ref firstLoad);
  }

  private void QueryParagraphFormat(
    DocumentTreeNode context,
    ref ParagraphFormat queryParagraphFormat,
    ref bool firstLoad)
  {
    try
    {
      if (context == null || context is Page)
        return;
      if (context.NodesCount > 0)
      {
        this.QueryParagraphFormat((IList<DocumentTreeNode>) context.Nodes, ref queryParagraphFormat, ref firstLoad);
      }
      else
      {
        if (!(context is TextData))
          return;
        ParagraphFormat paragraphFormat = ((TextData) context).ParagraphFormat;
        if (firstLoad)
        {
          queryParagraphFormat = paragraphFormat.Clone();
          firstLoad = false;
        }
        else
          queryParagraphFormat.GetFields(paragraphFormat);
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Обработчик события Удален элемент для активного элемента</summary>
  /// <param name="node">Удаленный узел</param>
  private void ActiveElementRemoved(object sender, Removed_EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (e.RemovedByShift || this.ActiveElement != e.Node)
        return;
      this.SetActiveElement((DocumentTreeNode) null, false, Point.Empty);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Активный элемент документа</summary>
  public DocumentTreeNode ActiveElement
  {
    [DebuggerStepThrough] get => this.activeElement;
  }

  /// <summary>Установить значение ActiveElement</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="inPlaceEditEnabled">Включать режим редактирования по месту для активного элемента</param>
  /// <param name="cursorPos">Координаты курсор в редакторе, если он активируется</param>
  public virtual void SetActiveElement(
    DocumentTreeNode value,
    bool inPlaceEditEnabled,
    Point cursorPos)
  {
    try
    {
      if (this.activeElement == value)
        return;
      if (this.activeElement != null)
      {
        if (this.activeElement is IPageElementWithInterface activeElement1 && activeElement1.PageUI != null)
          activeElement1.PageUI.SetIsActiveElement(false);
        this.activeElement.NodeRemoved -= new NodeRemoved_EventHandler(this.ActiveElementRemoved);
        if (this.activeElement is PageElementNode activeElement2)
        {
          if (activeElement2.InPlaceEditorActive)
            activeElement2.DeactivateInPlaceEditor();
          if (this.ActivePage != null && this.PageControl != null)
            this.PageControl.ActiveControl = (Control) null;
        }
      }
      this.activeElement = value;
      if (this.activeElement != null)
      {
        PageElementNode activeElement3 = this.activeElement as PageElementNode;
        if (this.activeElement is IPageElementWithInterface activeElement4)
        {
          if (activeElement4.PageUI == null && activeElement3 != null && activeElement3.Page != null)
            activeElement3.Page.CreateUI();
          if (activeElement4.PageUI != null)
          {
            activeElement4.PageUI.FocusUI();
            activeElement4.PageUI.SetIsActiveElement(true);
          }
        }
        if (!(this.activeElement is Page page) && activeElement3 != null)
          page = activeElement3.Page as Page;
        if (page != null)
        {
          this.ActivePage = page;
          if ((activeElement4 == null || activeElement4.PageUI == null) && this.pageControl != null)
            this.pageControl.SetFocusedElement((PageElementUI) null);
        }
        this.activeElement = value;
        this.activeElement.NodeRemoved += new NodeRemoved_EventHandler(this.ActiveElementRemoved);
        if (inPlaceEditEnabled && activeElement4 != null && activeElement4.CanActivateInPlaceEditor && !activeElement4.InPlaceEditorActive)
        {
          if (activeElement4.PageUI == null)
            activeElement4.CreateUI();
          if (activeElement4.PageUI != null)
            activeElement4.ActivateInPlaceEditor(activeElement4.PageUI, new MouseEventArgs(MouseButtons.None, 0, cursorPos.X, cursorPos.Y, 0));
        }
      }
      else if (this.pageControl != null)
        this.pageControl.SetFocusedElement((PageElementUI) null);
      this.OnActiveElementChanged(new ActiveElementChanged_EventArgs(this.activeElement));
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Вызывает событие ActiveElementChanged</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnActiveElementChanged(ActiveElementChanged_EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      ActiveElementChanged_EventHandler activeElementChanged = this.ActiveElementChanged;
      if (activeElementChanged == null)
        return;
      activeElementChanged((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Событие Активирована гиперссылка</summary>
  public event HyperLinkActivated_EventHandler HyperLinkActivated;

  /// <summary>Вызывает событие ActiveElementChanged</summary>
  /// <param name="e">Аргументы события</param>
  internal void OnHyperLinkActivated(HyperLinkActivated_EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      HyperLinkActivated_EventHandler hyperLinkActivated = this.HyperLinkActivated;
      if (hyperLinkActivated == null)
        return;
      hyperLinkActivated((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public bool EditorValidating()
  {
    CancelEventArgs cancelArgs = new CancelEventArgs();
    try
    {
      this.EditorValidating(cancelArgs);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return !cancelArgs.Cancel;
  }

  public virtual void EditorValidating(CancelEventArgs cancelArgs)
  {
    try
    {
      if (this.ReadOnly || this.FocusedElement == null)
        return;
      this.FocusedElement.OnValidating(cancelArgs);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Событие Изменилось свойство ActiveElement</summary>
  public event ActiveElementChanged_EventHandler ActiveElementChanged;

  public event SelectedElementBoundsChanging_EventHandler SelectedElementBoundsChanging;

  public void OnSelectedElementBoundsChanging(BoundsChangingEventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      SelectedElementBoundsChanging_EventHandler elementBoundsChanging = this.SelectedElementBoundsChanging;
      if (elementBoundsChanging == null)
        return;
      elementBoundsChanging((object) this, e);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Для внутреннего использования. Кэш для Query - есть ли в выделении блокированные элементы</summary>
  public bool QueryCache_HasLockedNodes
  {
    [DebuggerStepThrough] get => this.queryCache_HasLockedNodes;
    set => this.queryCache_HasLockedNodes = value;
  }

  /// <summary>Система координат страницы</summary>
  [CustomDisplayName("Attribute.Document.Model_1")]
  [CustomDescription("Attribute.Document.Model_2")]
  [CustomCategory("Attribute.Document.Model_3")]
  public PageCoorSystem CoorSystem
  {
    [DebuggerStepThrough] get => ImDocumentEditorConfig.Instance.CoorSystem;
  }

  /// <summary>Размер сетки привязки выбора точек</summary>
  [TypeConverter(typeof (FloatConverter))]
  public float GridSize
  {
    [DebuggerStepThrough] get => ImDocumentEditorConfig.Instance.GridSize;
  }

  /// <summary>Привязать координату к сетке</summary>
  /// <param name="f">Координата</param>
  /// <returns>Ближайшая координат на сетке</returns>
  public float SnapToGrid(float f)
  {
    try
    {
      if ((double) this.GridSize != 0.0)
      {
        int num1;
        float num2 = (float) (num1 = (int) Math.Floor((double) f / (double) this.GridSize)) * this.GridSize;
        float num3 = (float) (num1 + 1) * this.GridSize;
        f = (double) f - (double) num2 >= (double) num3 - (double) f ? num3 : num2;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return f;
  }

  /// <summary>Привязать координаты к сетке</summary>
  /// <param name="point">Координаты</param>
  /// <returns>Ближайшие координаты на сетке</returns>
  public PointF SnapToGrid(PointF point)
  {
    return new PointF(this.SnapToGrid(point.X), this.SnapToGrid(point.Y));
  }

  /// <summary>Привязать координаты к сетке</summary>
  /// <param name="rectangle">Координаты</param>
  /// <returns>Ближайшие координаты на сетке</returns>
  public RectangleF SnapToGrid(RectangleF rectangle)
  {
    return new RectangleF(this.SnapToGrid(rectangle.X), this.SnapToGrid(rectangle.Y), this.SnapToGrid(rectangle.Width), this.SnapToGrid(rectangle.Height));
  }

  public IUndoManager UndoManager
  {
    get
    {
      return this.DocumentEditorForm == null ? (IUndoManager) null : this.DocumentEditorForm.UndoManager;
    }
  }

  public IExternalEditor ExternalEditor
  {
    get
    {
      return this.DocumentEditorForm == null ? (IExternalEditor) null : this.DocumentEditorForm.ExternalEditor;
    }
  }

  public FindReplaceManager FindReplaceManager
  {
    get
    {
      return this.DocumentEditorForm == null ? (FindReplaceManager) null : this.DocumentEditorForm.FindReplaceManager;
    }
  }

  /// <summary>Объект управляющий окнами документа</summary>
  public IImDocumentManager DocumentManager
  {
    [DebuggerStepThrough] get => this.documentManager;
    set
    {
      this.documentManager = value;
      if (this.documentManager != null)
        return;
      this.IsElementSelecting = true;
      this.IsElementCreating = false;
    }
  }

  /// <summary>Включен режим выбора элементов. Если имеет значение true,
  /// то IsElementCreating не может иметь значение true</summary>
  public bool IsElementSelecting
  {
    [DebuggerStepThrough] get
    {
      return this.documentManager != null ? this.documentManager.IsElementSelecting : this.isElementSelecting;
    }
    set
    {
      if (this.IsElementSelecting == value)
        return;
      if (this.documentManager != null)
      {
        this.documentManager.IsElementSelecting = value;
      }
      else
      {
        this.isElementSelecting = value;
        this.IsElementCreating = !value;
      }
    }
  }

  public Panel SubPanel => this.subPanel;

  public ImDocumentEditorFormBase DocumentEditorForm
  {
    get
    {
      for (Control parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is ImDocumentEditorFormBase)
          return parent as ImDocumentEditorFormBase;
      }
      return (ImDocumentEditorFormBase) null;
    }
  }

  public Ruler HorzRuler => this.rulerHorizontal;

  public Ruler VertRuler => this.rulerVertical;

  public HScrollBar HScrollBar => this.hScrollBar;

  public VScrollBar VScrollBar => this.vScrollBar;

  /// <summary>Включен режим создания элементов. Если имеет значение true,
  /// то IsElementSelecting не может иметь значение true</summary>
  public bool IsElementCreating
  {
    [DebuggerStepThrough] get
    {
      return this.documentManager != null ? this.documentManager.IsElementCreating : this.isElementCreating;
    }
    set
    {
      if (this.IsElementCreating == value)
        return;
      if (this.documentManager != null)
      {
        this.documentManager.IsElementCreating = value;
      }
      else
      {
        this.isElementCreating = value;
        this.IsElementSelecting = !value;
      }
    }
  }

  /// <summary>Включен режим выбора системы координат</summary>
  public static bool IsCoorSystemSelecting
  {
    [DebuggerStepThrough] get => DocumentControl.isCoorSystemSelecting;
    set
    {
      if (DocumentControl.isCoorSystemSelecting == value)
        return;
      DocumentControl.isCoorSystemSelecting = value;
    }
  }

  /// <summary>Установить режим выбора строк таблицы</summary>
  /// <param name="value">Включить режим</param>
  /// <param name="selectedTable">Таблица внутри которой происходит выбор</param>
  internal void SetTableRowsSelectingMode(bool value, TableElement selectedTable)
  {
    if (this.isTableRowsSelecting == value)
      return;
    this.isTableRowsSelecting = value;
    if (this.isTableRowsSelecting || this.RowSelection)
      this.selectedTable = selectedTable;
    else
      this.selectedTable = (TableElement) null;
  }

  /// <summary>Установить режим выбора столбцов таблицы</summary>
  /// <param name="value">Включить режим</param>
  /// <param name="selectedTable">Таблица внутри которой происходит выбор</param>
  internal void SetTableColumnsSelectingMode(bool value, TableElement selectedTable)
  {
    if (this.isTableColumnsSelecting == value)
      return;
    this.isTableColumnsSelecting = value;
    if (this.isTableColumnsSelecting)
      this.selectedTable = selectedTable;
    else
      this.selectedTable = (TableElement) null;
  }

  /// <summary>Установить режим выбора ячеек таблицы</summary>
  /// <param name="value">Включить режим</param>
  /// <param name="selectedTable">Таблица внутри которой происходит выбор</param>
  internal void SetTableCellsSelectingMode(bool value, TableElement selectedTable)
  {
    if (this.isTableCellsSelecting == value)
      return;
    this.isTableCellsSelecting = value;
    if (this.isTableCellsSelecting)
      this.selectedTable = selectedTable;
    else
      this.selectedTable = (TableElement) null;
  }

  /// <summary>Режим выбора строк таблицы</summary>
  internal bool IsTableRowsSelecting
  {
    [DebuggerStepThrough] get => this.isTableRowsSelecting && this.MultiSelect;
  }

  /// <summary>Режим выбора столбцов таблицы</summary>
  internal bool IsTableColumnsSelecting
  {
    [DebuggerStepThrough] get => this.isTableColumnsSelecting && this.MultiSelect;
  }

  /// <summary>Режим выбора ячеек таблицы</summary>
  internal bool IsTableCellsSelecting => this.isTableCellsSelecting && this.MultiSelect;

  /// <summary>Таблица в которой выбираются ячейки</summary>
  internal TableElement SelectedTable
  {
    [DebuggerStepThrough] get => this.selectedTable;
  }

  /// <summary>Объект управляющий созданием элемента</summary>
  public PageElementCreator SelectedElementCreator
  {
    [DebuggerStepThrough] get
    {
      return this.DocumentManager != null ? this.DocumentManager.SelectedElementCreator : (PageElementCreator) null;
    }
    set
    {
      if (this.DocumentManager == null)
        return;
      this.DocumentManager.SelectedElementCreator = value;
    }
  }

  /// <summary>Пользователь не может редактировать документ</summary>
  public bool ReadOnly
  {
    [DebuggerStepThrough] get => this.readOnly;
    set
    {
      if (this.readOnly == value)
        return;
      this.readOnly = value;
    }
  }

  /// <summary>Геометрию нельзя изменять</summary>
  public bool ReadOnlyGeometry
  {
    [DebuggerStepThrough] get
    {
      if (this.readOnlyGeometry || this.readOnly)
        return true;
      return this.readOnlyGeometryForDocument && this.Document != null && !this.Document.IsTemplate;
    }
    set
    {
      try
      {
        if (this.readOnlyGeometry == value)
          return;
        List<DocumentTreeNode> selectedNodes = this.selectedNodes;
        this.readOnlyGeometry = value;
        this.Update();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Геометрию документа нельзя изменять. На шаблон не влияет.</summary>
  public bool ReadOnlyGeometryForDocument
  {
    [DebuggerStepThrough] get => this.readOnlyGeometryForDocument;
    set
    {
      try
      {
        if (this.readOnlyGeometryForDocument == value)
          return;
        List<DocumentTreeNode> selectedNodes = this.selectedNodes;
        this.readOnlyGeometryForDocument = value;
        this.Update();
      }
      catch (Exception ex)
      {
        string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
        ImDocumentData.ShowException(ex, errorFormCaption);
      }
    }
  }

  /// <summary>Показывать невидимые линии границ</summary>
  public bool ShowInvisibleLines
  {
    [DebuggerStepThrough] get => ImDocumentEditorConfig.Instance.ShowInvisibleLines;
  }

  /// <summary>Установить режим масштабирования страниц документа. Масштаб округляется до 1%</summary>
  /// <param name="zoomMode">Режим масштабирования</param>
  /// <param name="docScale">Масштаб для режима Custom</param>
  /// <returns>Установленный масштаб</returns>
  public virtual float SetZoom(DocZoomMode zoomMode, float docScale, bool fireEvent = true)
  {
    try
    {
      this.zoomMode = zoomMode;
      if (zoomMode != DocZoomMode.Custom)
      {
        docScale = 1f;
        if (this.document != null && this.document.Nodes.Count > 0)
        {
          Page page1 = this.ActivePage;
          int index = 0;
          for (int count = this.document.Nodes.Count; page1 == null && index < count; ++index)
            page1 = this.document.Nodes[index] as Page;
          if (page1 != null)
          {
            int docRegionWidth = this.DocRegionWidth;
            Page page2 = page1;
            SizeF size = page1.Size;
            double width = (double) size.Width;
            int pixel1 = page2.ConvertXMmToPixel((float) width);
            docScale = (float) docRegionWidth / (float) pixel1;
            if (zoomMode == DocZoomMode.FitPage)
            {
              int docRegionHeight = this.DocRegionHeight;
              Page page3 = page1;
              size = page1.Size;
              double height = (double) size.Height;
              int pixel2 = page3.ConvertYMmToPixel((float) height);
              float num = (float) docRegionHeight / (float) pixel2;
              if ((double) docScale > (double) num)
                docScale = num;
            }
            docScale = Convert.ToSingle(Math.Floor((double) docScale * 100.0) / 100.0);
          }
        }
      }
      docScale = Convert.ToSingle(Math.Round((double) docScale * 100.0) / 100.0);
      if ((double) docScale > 9.9999997473787516E-06)
      {
        if ((double) this.documentScale != (double) docScale)
        {
          this.documentScale = docScale;
          int num = this.document == null ? 1 : (!this.document.SuspendedUpdateUIGeometryFlag ? 0 : (this.document.SuspendedRefreshUIFlag ? 1 : 0));
          if (num == 0)
            this.document?.SuspendUpdateGeometryRefreshUI();
          this.document?.SetNeedUpdateUIGeometryRecursive(true, false);
          if (num == 0)
          {
            List<DocumentTreeNode> selectedNodes = this.SelectedNodes;
            this.document?.ResumeUpdateRefreshUI(true, false);
            if (selectedNodes.Count == 1 && selectedNodes[0] is PageElementNode pageElementNode && pageElementNode.Page == this.ActivePage)
            {
              this.selectedNodes = new List<DocumentTreeNode>();
              this.SetSelection(selectedNodes, false, false);
            }
            this.document?.RefreshUI();
          }
          if (fireEvent)
          {
            EventHandler zoomValueChanged = this.ZoomValueChanged;
            if (zoomValueChanged != null)
              zoomValueChanged((object) this, EventArgs.Empty);
          }
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return this.documentScale;
  }

  /// <summary>Режим масштабирования документа</summary>
  [Browsable(false)]
  public DocZoomMode ZoomMode
  {
    [DebuggerStepThrough] get => this.zoomMode;
  }

  /// <summary>Коэффициент масштабирования</summary>
  [CustomDisplayName("Attribute.Document.Model_4")]
  [CustomDescription("Attribute.Document.Model_5")]
  [CustomCategory("Attribute.Document.Model_6")]
  [Browsable(false)]
  public float DocumentScale
  {
    [DebuggerStepThrough] get => this.documentScale;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    try
    {
      this.LockForClosing = true;
      if (disposing && this.components != null)
        this.components.Dispose();
      if (this.documentsComplect != null)
      {
        DocumentsComplect documentsComplect = this.documentsComplect;
        this.DocumentsComplect = (DocumentsComplect) null;
      }
      if (this.document != null)
      {
        ImDocument document = this.document;
        this.Document = (ImDocument) null;
      }
      ImDocumentEditorConfig.Instance.Changed -= new EventHandler(this.ImDocumentEditorConfig_Changed);
      this.suspendedActivePage = (Page) null;
      this.suspendedSelection = (DocumentTreeNode) null;
      this.suspendedActivePage = (Page) null;
      this.focusedElement = (PageElementUI) null;
      if (this.selectedNodes != null)
      {
        this.selectedNodes.Clear();
        this.selectedNodes = (List<DocumentTreeNode>) null;
      }
      this.selectedTable = (TableElement) null;
      this.documentManager = (IImDocumentManager) null;
      this.activePage = (Page) null;
      this.activeElement = (DocumentTreeNode) null;
      this.components = (IContainer) null;
      if (this.ternEditorBuffer != null)
      {
        this.ternEditorBuffer.Parent = (Control) null;
        this.ternEditorBuffer.Dispose();
        this.ternEditorBuffer = (ImRtfEditor) null;
      }
      this.PageControl = (PageControl) null;
      if (this.documentsComplect != null)
      {
        DocumentsComplect documentsComplect = this.documentsComplect;
        this.DocumentsComplect = (DocumentsComplect) null;
        documentsComplect.Dispose();
      }
      base.Dispose(disposing);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Required method for Designer support - do not modify
  /// the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentControl));
    this.hScrollBar = new HScrollBar();
    this.vScrollBar = new VScrollBar();
    this.subPanel = new Panel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.HSPanel = new Panel();
    this.bevel2 = new Bevel();
    this.splitter1 = new Splitter();
    this.bevel1 = new Bevel();
    this.ViewsSwitch = new ViewSwitch();
    this._viewsPageImages = new ImageList(this.components);
    this.rulerVertical = new Ruler();
    this.rulerHorizontal = new Ruler();
    this.rulerButton = new RulerButton();
    this.HSPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.hScrollBar, "hScrollBar");
    this.hScrollBar.Name = "hScrollBar";
    this.hScrollBar.ValueChanged += new EventHandler(this.ScrollBars_ValueChanged);
    componentResourceManager.ApplyResources((object) this.vScrollBar, "vScrollBar");
    this.vScrollBar.Name = "vScrollBar";
    this.vScrollBar.ValueChanged += new EventHandler(this.ScrollBars_ValueChanged);
    componentResourceManager.ApplyResources((object) this.subPanel, "subPanel");
    this.subPanel.BackColor = SystemColors.ControlDarkDark;
    this.subPanel.Name = "subPanel";
    this.subPanel.LocationChanged += new EventHandler(this.subPanel_LocationChanged);
    this.subPanel.Click += new EventHandler(this.subPanel_Click);
    this.subPanel.MouseDown += new MouseEventHandler(this.subPanel_MouseDown);
    this.subPanel.SizeChanged += new EventHandler(this.subPanel_SizeChanged);
    componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    componentResourceManager.ApplyResources((object) this.HSPanel, "HSPanel");
    this.HSPanel.BackColor = SystemColors.Control;
    this.HSPanel.Controls.Add((Control) this.bevel2);
    this.HSPanel.Controls.Add((Control) this.hScrollBar);
    this.HSPanel.Controls.Add((Control) this.splitter1);
    this.HSPanel.Controls.Add((Control) this.bevel1);
    this.HSPanel.Controls.Add((Control) this.ViewsSwitch);
    this.HSPanel.Name = "HSPanel";
    this.bevel2.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.bevel2, "bevel2");
    this.bevel2.Name = "bevel2";
    this.splitter1.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.bevel1.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.bevel1, "bevel1");
    this.bevel1.Name = "bevel1";
    this.ViewsSwitch.ActivePageColor = Color.FromArgb((int) byte.MaxValue, 192 /*0xC0*/, 111);
    this.ViewsSwitch.ActivepageIndex = 0;
    componentResourceManager.ApplyResources((object) this.ViewsSwitch, "ViewsSwitch");
    this.ViewsSwitch.HlightPageColor = Color.FromArgb((int) byte.MaxValue, 238, 194);
    this.ViewsSwitch.ImageIndexes = new int[2]{ 0, 1 };
    this.ViewsSwitch.ImageList = this._viewsPageImages;
    this.ViewsSwitch.InactivePageColor = SystemColors.Control;
    this.ViewsSwitch.Name = "ViewsSwitch";
    this.ViewsSwitch.ViewsCaptions = new string[2]
    {
      "Документ",
      "Шаблон"
    };
    this.ViewsSwitch.ViewsHints = new string[0];
    this._viewsPageImages.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_viewsPageImages.ImageStream");
    this._viewsPageImages.TransparentColor = Color.Transparent;
    this._viewsPageImages.Images.SetKeyName(0, "DocumentView.gif");
    this._viewsPageImages.Images.SetKeyName(1, "TemplateView.gif");
    componentResourceManager.ApplyResources((object) this.rulerVertical, "rulerVertical");
    this.rulerVertical.BordersColor = SystemColors.InactiveCaption;
    this.rulerVertical.BordersReadOnly = false;
    this.rulerVertical.Document = (DocumentControl) null;
    this.rulerVertical.IdentFirstLine = new float?(0.0f);
    this.rulerVertical.IdentLeft = new float?(0.0f);
    this.rulerVertical.IdentRight = new float?(0.0f);
    this.rulerVertical.Index = 0;
    this.rulerVertical.Name = "rulerVertical";
    this.rulerVertical.Orientation = enumOrientation.orVertical;
    this.rulerVertical.ScaleMode = enumScaleMode.smCentimetres;
    this.rulerVertical.ShowRuler = false;
    this.rulerVertical.ShowSliders = false;
    this.rulerVertical.BorderPositionChanged += new Ruler.BorderPositionChanged_EventHandler(this.rulerHorizontal_BorderPositionChanged);
    componentResourceManager.ApplyResources((object) this.rulerHorizontal, "rulerHorizontal");
    this.rulerHorizontal.BordersColor = SystemColors.InactiveCaption;
    this.rulerHorizontal.BordersReadOnly = false;
    this.rulerHorizontal.Document = (DocumentControl) null;
    this.rulerHorizontal.IdentFirstLine = new float?(10f);
    this.rulerHorizontal.IdentLeft = new float?(10f);
    this.rulerHorizontal.IdentRight = new float?(10f);
    this.rulerHorizontal.Index = 0;
    this.rulerHorizontal.Name = "rulerHorizontal";
    this.rulerHorizontal.Orientation = enumOrientation.orHorizontal;
    this.rulerHorizontal.ScaleMode = enumScaleMode.smCentimetres;
    this.rulerHorizontal.ShowRuler = false;
    this.rulerHorizontal.ShowSliders = true;
    this.rulerHorizontal.BorderPositionChanged += new Ruler.BorderPositionChanged_EventHandler(this.rulerHorizontal_BorderPositionChanged);
    this.rulerHorizontal.IdentChanged += new Ruler.IdentChanged_EventHandler(this.rulerHorizontal_IdentChanged);
    componentResourceManager.ApplyResources((object) this.rulerButton, "rulerButton");
    this.rulerButton.Name = "rulerButton";
    this.Controls.Add((Control) this.rulerButton);
    this.Controls.Add((Control) this.rulerVertical);
    this.Controls.Add((Control) this.rulerHorizontal);
    this.Controls.Add((Control) this.HSPanel);
    this.Controls.Add((Control) this.flowLayoutPanel1);
    this.Controls.Add((Control) this.subPanel);
    this.Controls.Add((Control) this.vScrollBar);
    this.Name = nameof (DocumentControl);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Resize += new EventHandler(this.DocumentControl_Resize);
    this.HSPanel.ResumeLayout(false);
    this.HSPanel.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  protected override void OnParentChanged(EventArgs e) => base.OnParentChanged(e);

  protected override void OnVisibleChanged(EventArgs e)
  {
    try
    {
      base.OnVisibleChanged(e);
      if (!this.Visible)
        return;
      Panel hsPanel = this.HSPanel;
      Size size = this.Size;
      int height1 = size.Height;
      size = this.HSPanel.Size;
      int height2 = size.Height;
      Point point = new Point(0, height1 - height2);
      hsPanel.Location = point;
      this.HSPanel.Size = new Size(this.Width - this.vScrollBar.Size.Width, this.HSPanel.Height);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Получить все реальные ячейки виртуальной ячейки</summary>
  /// <param name="cell"></param>
  /// <param name="cur_var"></param>
  /// <param name="hasleft"></param>
  private void GetRealCells(RectangleElement cell, ref List<DocumentTreeNode> cur_var)
  {
    try
    {
      if (!cell.IsSingleCell)
      {
        if (cell.Nodes.Count == 0)
          return;
        this.GetRealCells(cell.Nodes[0] as RectangleElement, ref cur_var);
        int index = 1;
        for (int count = cell.Nodes.Count; index < count; ++index)
          this.GetRealCells(cell.Nodes[index] as RectangleElement, ref cur_var);
      }
      else
        cur_var.Add((DocumentTreeNode) cell);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>
  /// Получение  ячеек в строках в котоорые входят верхние ячейки
  /// </summary>
  /// <param name="cell">выделенная область</param>
  /// <param name="cur_var">список ячеек</param>
  /// <param name="hastop">есть ли ячейка выше данной</param>
  private void GetLeftCells(
    RectangleElement cell,
    ref List<DocumentTreeNode> cur_var,
    bool hasleft)
  {
    try
    {
      bool hasleft1 = hasleft;
      bool flag = false;
      if (!cell.IsSingleCell)
      {
        if (cell.Nodes.Count == 0)
          return;
        if (cell is TableElement)
          flag = (cell as TableElement).IsRow;
        this.GetLeftCells(cell.Nodes[0] as RectangleElement, ref cur_var, hasleft1);
        int index = 1;
        for (int count = cell.Nodes.Count; index < count; ++index)
        {
          RectangleElement node = cell.Nodes[index] as RectangleElement;
          bool hasleft2 = hasleft;
          if (flag)
            hasleft2 = true;
          this.GetLeftCells(node, ref cur_var, hasleft2);
        }
      }
      else
      {
        if (hasleft || cell.ParentCell == null || cell.ParentCell.ParentCell == null || cell.ParentCell.Nodes.Count == 0)
          return;
        for (int index = 0; index < cell.ParentCell.ParentCell.Nodes.Count; ++index)
        {
          if (!cur_var.Contains(cell.ParentCell.ParentCell.Nodes[index]))
            cur_var.Add(cell.ParentCell.ParentCell.Nodes[index]);
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>
  /// Получение  ячеек в строках в котоорые входят верхние ячейки
  /// </summary>
  /// <param name="cell">выделенная область</param>
  /// <param name="cur_var">список ячеек</param>
  /// <param name="hastop">есть ли ячейка выше данной</param>
  private void GetTopCells(RectangleElement cell, ref List<DocumentTreeNode> cur_var, bool hastop)
  {
    try
    {
      bool hastop1 = hastop;
      bool flag = false;
      if (!cell.IsSingleCell)
      {
        if (cell.Nodes.Count == 0)
          return;
        if (cell is TableElement)
          flag = (cell as TableElement).IsRow;
        this.GetTopCells(cell.Nodes[0] as RectangleElement, ref cur_var, hastop1);
        int index = 1;
        for (int count = cell.Nodes.Count; index < count; ++index)
        {
          RectangleElement node = cell.Nodes[index] as RectangleElement;
          bool hastop2 = hastop;
          if (!flag)
            hastop2 = true;
          this.GetTopCells(node, ref cur_var, hastop2);
        }
      }
      else
      {
        if (hastop || cell.ParentCell == null || cell.ParentCell.Nodes.Count == 0)
          return;
        for (int index = 0; index < cell.ParentCell.Nodes.Count; ++index)
        {
          if (!cur_var.Contains(cell.ParentCell.Nodes[index]))
            cur_var.Add(cell.ParentCell.Nodes[index]);
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private TextData SaveParagraphFormat(
    IList<DocumentTreeNode> context,
    ParagraphFormat paragraphFormat,
    bool firstLoad)
  {
    TextData textData1 = (TextData) null;
    try
    {
      if (context == null || context.Count <= 0)
        return (TextData) null;
      ImRtfEditor activeEditorControl = this.GetActiveEditorControl();
      bool flag = true;
      if (activeEditorControl != null)
        flag = activeEditorControl.AllParagraphsSelected();
      if (activeEditorControl == null | flag)
      {
        for (int index = 0; index < context.Count; ++index)
        {
          TextData textData2 = this.SaveParagraphFormat(context[index], paragraphFormat);
          if (textData2 != null)
            textData1 = textData2;
        }
      }
      else
        this.ApplyTextIdent(paragraphFormat, activeEditorControl, false);
      if (firstLoad)
      {
        if (textData1 != null)
        {
          if (textData1.TopLevelTable != null)
            textData1.TopLevelTable.UpdateLayout(true);
          else
            textData1.UpdateLayout(true);
        }
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return textData1;
  }

  private TextData SaveParagraphFormat(DocumentTreeNode context, ParagraphFormat paragraphFormat)
  {
    try
    {
      if (context == null || context is Page)
        return (TextData) null;
      if (context.NodesCount > 0)
        this.SaveParagraphFormat((IList<DocumentTreeNode>) context.Nodes, paragraphFormat, false);
      else if (context is TextData)
      {
        TextData textData = (TextData) context;
        ParagraphFormat paragraphFormat1 = textData.ParagraphFormat;
        ParagraphFormat paragraphFormat2 = paragraphFormat1.Clone();
        bool flag = false;
        float? nullable1 = paragraphFormat.IdentLeft;
        if (nullable1.HasValue)
        {
          nullable1 = paragraphFormat1.IdentLeft;
          float? identLeft = paragraphFormat.IdentLeft;
          if (!((double) nullable1.GetValueOrDefault() == (double) identLeft.GetValueOrDefault() & nullable1.HasValue == identLeft.HasValue))
          {
            paragraphFormat2.IdentLeft = paragraphFormat.IdentLeft;
            flag = true;
          }
        }
        float? nullable2;
        if (paragraphFormat.IdentRight.HasValue)
        {
          nullable2 = paragraphFormat1.IdentRight;
          nullable1 = paragraphFormat.IdentRight;
          if (!((double) nullable2.GetValueOrDefault() == (double) nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          {
            paragraphFormat2.IdentRight = paragraphFormat.IdentRight;
            flag = true;
          }
        }
        nullable1 = paragraphFormat.IdentFirstLine;
        if (nullable1.HasValue)
        {
          nullable1 = paragraphFormat1.IdentFirstLine;
          nullable2 = paragraphFormat.IdentFirstLine;
          if (!((double) nullable1.GetValueOrDefault() == (double) nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            paragraphFormat2.IdentFirstLine = paragraphFormat.IdentFirstLine;
            flag = true;
          }
        }
        if (flag)
        {
          textData.SetParagraphFormat(paragraphFormat2, false, false);
          if (textData is TextBoxElement)
          {
            TextBoxElement textBoxElement = textData as TextBoxElement;
            ImRtfEditor ternPaintBuffer = (textBoxElement.OwnerDocument as ImDocument).TernPaintBuffer;
            if (ternPaintBuffer != null && !textBoxElement.IsEmptyText && textBoxElement.Rtf != null)
            {
              Rectangle editorBounds;
              ref Rectangle local = ref editorBounds;
              RectangleF bounds = textBoxElement.Bounds;
              int left = (int) bounds.Left;
              bounds = textBoxElement.Bounds;
              int top = (int) bounds.Top;
              bounds = textBoxElement.Bounds;
              int width = (int) bounds.Width;
              bounds = textBoxElement.Bounds;
              int height = (int) bounds.Height;
              local = new Rectangle(left, top, width, height);
              textBoxElement.TextBox.SetupEditor(ternPaintBuffer, textBoxElement.Rtf, true, textBoxElement.StartCharIndex, paragraphFormat2, textBoxElement.Orientation, textBoxElement.CharFormat, textBoxElement.BackColor, textBoxElement.Bounds, editorBounds, new MarginsF(textBoxElement.LeftMargin, textBoxElement.RightMargin, textBoxElement.TopMargin, textBoxElement.BottomMargin), 1f, textBoxElement.DefaultRowSize);
              ternPaintBuffer.SelectAll(false);
              this.ApplyTextIdent(paragraphFormat, ternPaintBuffer, false);
              textBoxElement.AssignText(textBoxElement.Text, ternPaintBuffer.RtfText, true, false, false);
            }
          }
        }
        return textData;
      }
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
    return (TextData) null;
  }

  /// <summary>Применить отступы к терну</summary>
  /// <param name="paragraphFormat"></param>
  /// <param name="tern"></param>
  /// <param name="needUpdate"></param>
  private void ApplyTextIdent(ParagraphFormat paragraphFormat, ImRtfEditor tern, bool needUpdate)
  {
    try
    {
      if (tern == null)
        return;
      int left = paragraphFormat.IdentLeft.HasValue ? UnitsConverter.MmToTwips(paragraphFormat.IdentLeft.Value * 10f) : -1;
      float? nullable = paragraphFormat.IdentRight;
      int num1;
      if (!nullable.HasValue)
      {
        num1 = -1;
      }
      else
      {
        nullable = paragraphFormat.IdentRight;
        num1 = UnitsConverter.MmToTwips(nullable.Value * 10f);
      }
      int right = num1;
      nullable = paragraphFormat.IdentFirstLine;
      int num2;
      if (!nullable.HasValue)
      {
        num2 = -1;
      }
      else
      {
        nullable = paragraphFormat.IdentFirstLine;
        num2 = UnitsConverter.MmToTwips(nullable.Value * 10f);
      }
      int first = num2;
      if (left == -1 && right == -1 && first == -1)
        return;
      tern.TerSetParaIndent(left, right, first, needUpdate);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установка отступов выделенного элемента</summary>
  public void SetIdentsToElement(ParagraphFormat pf)
  {
    try
    {
      this.rulerHorizontal.DrawRuler = false;
      this.needSetIdentsToRuler = false;
      this.SaveParagraphFormat((IList<DocumentTreeNode>) new List<DocumentTreeNode>()
      {
        this.ActiveElement
      }, pf, true);
      this.rulerHorizontal.DrawRuler = true;
      this.needSetIdentsToRuler = true;
      this.rulerHorizontal.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Установка отступов на линейке</summary>
  public void SetIdentsToRuler()
  {
    try
    {
      if (!this.needSetIdentsToRuler || !this.rulerHorizontal.Visible)
        return;
      if (this.ActiveElement != null && this.DocumentEditorForm != null)
      {
        bool cacheHasLockedNodes = this.QueryCache_HasLockedNodes;
        if (!this.DocumentEditorForm.MenuHelper.QueryStatus_FormatText(this.ActiveElement) | cacheHasLockedNodes)
        {
          this.HorzRuler.ShowSliders = false;
        }
        else
        {
          ParagraphFormat queryParagraphFormat = new ParagraphFormat(true);
          this.QueryParagraphFormat((IList<DocumentTreeNode>) new List<DocumentTreeNode>()
          {
            this.ActiveElement
          }, ref queryParagraphFormat);
          float? nullable = queryParagraphFormat.IdentLeft;
          float num1;
          if (nullable.HasValue)
          {
            nullable = queryParagraphFormat.IdentLeft;
            num1 = nullable.Value * 10f;
          }
          else
            num1 = 0.0f;
          nullable = queryParagraphFormat.IdentRight;
          float num2;
          if (nullable.HasValue)
          {
            nullable = queryParagraphFormat.IdentRight;
            num2 = nullable.Value * 10f;
          }
          else
            num2 = 0.0f;
          nullable = queryParagraphFormat.IdentFirstLine;
          float num3;
          if (nullable.HasValue)
          {
            nullable = queryParagraphFormat.IdentLeft;
            if (nullable.HasValue)
            {
              nullable = queryParagraphFormat.IdentFirstLine;
              num3 = nullable.Value * 10f + num1;
              goto label_14;
            }
          }
          num3 = 0.0f;
label_14:
          this.HorzRuler.ShowSliders = true;
          Ruler horzRuler = this.HorzRuler;
          double IdentLeft = (double) num1;
          nullable = queryParagraphFormat.IdentLeft;
          int num4 = !nullable.HasValue ? 1 : 0;
          double IdentRight = (double) num2;
          nullable = queryParagraphFormat.IdentRight;
          int num5 = !nullable.HasValue ? 1 : 0;
          double IdentFirstLine = (double) num3;
          nullable = queryParagraphFormat.IdentFirstLine;
          int num6 = !nullable.HasValue ? 1 : 0;
          horzRuler.SetIdents((float) IdentLeft, num4 != 0, (float) IdentRight, num5 != 0, (float) IdentFirstLine, num6 != 0);
        }
      }
      else
        this.rulerHorizontal.ShowSliders = false;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  internal void SaveActiveEditorSelection()
  {
    if (!(this.ActiveElement is TextBoxElement activeElement) || !activeElement.InPlaceEditorActive)
      return;
    this.ActiveEditorForSavedSelection = activeElement;
    this.SavedActiveEditorSelection = this.TernEditorBuffer.GetSelectionBlock();
    this.OldNeedUpdateToolBar = this.NeedUpdateToolbar;
  }

  internal void RestoreActiveEditorSelection()
  {
    if (this.SavedActiveEditorSelection == null || !(this.ActiveElement is TextBoxElement activeElement) || activeElement != this.ActiveEditorForSavedSelection || activeElement.Page == null || activeElement.Page.IsWaitForDistributed || this.TernEditorBuffer == null || activeElement.InPlaceEditorActive || !activeElement.CanActivateInPlaceEditor)
      return;
    activeElement.ActivateInPlaceEditor(activeElement.PageUI, (MouseEventArgs) null);
    this.TernEditorBuffer.RestoreSelection(this.SavedActiveEditorSelection, false);
    this.NeedUpdateToolbar = this.OldNeedUpdateToolBar;
    this.inPlaceEditorDeactivated = false;
    this.ActiveEditorForSavedSelection = (TextBoxElement) null;
    this.SavedActiveEditorSelection = (SelectionBlock) null;
  }

  /// <summary>Деактивировать терн и сохранить его параметры</summary>
  public void DeactivateInPlaceEditor()
  {
    try
    {
      if (!(this.ActiveElement is PageElementNode activeElement) || this.TernEditorBuffer == null || !activeElement.InPlaceEditorActive)
        return;
      this.selBlock = this.TernEditorBuffer.GetSelectionBlock();
      this.TernEditorBuffer.GetTerCursorPos(out this.cursLine, ref this.cursCol);
      activeElement.DeactivateInPlaceEditor();
      this.inPlaceEditorDeactivated = true;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Активировать редактор и восстановить его параметры</summary>
  public void ActivateInPlaceEditor()
  {
    try
    {
      if (!(this.ActiveElement is TextBoxElement activeElement) || this.TernEditorBuffer == null || !activeElement.CanActivateInPlaceEditor || activeElement.InPlaceEditorActive || !this.inPlaceEditorDeactivated)
        return;
      activeElement.ActivateInPlaceEditor(activeElement.PageUI, (MouseEventArgs) null);
      this.TernEditorBuffer.SetTerCursorPos(this.cursLine, this.cursCol, false);
      this.TernEditorBuffer.RestoreSelection(this.selBlock, false);
      this.inPlaceEditorDeactivated = false;
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public void SetBordersToElement(Ruler ruler, float oldValue, int index, enumTypeDrag type)
  {
    try
    {
      RectangleF bounds1;
      if (ruler == this.rulerHorizontal && this.ActiveElement is RectangleElement)
      {
        RectangleElement activeElement = this.ActiveElement as RectangleElement;
        if (activeElement.TopLevelTable == null)
        {
          RectangleF bounds2 = activeElement.Bounds with
          {
            X = this.rulerHorizontal.BorderPositions[0],
            Width = this.rulerHorizontal.BorderPositions[1] - this.rulerHorizontal.BorderPositions[0]
          };
          activeElement.SetCellSizes(bounds2, true, true, true, true);
          activeElement.UpdateLayout(true);
        }
        else
        {
          List<DocumentTreeNode> cur_var = new List<DocumentTreeNode>();
          this.GetRealCells(activeElement, ref cur_var);
          TableData topLevelTable = (cur_var[0] as RectangleElement).TopLevelTable;
          topLevelTable.SuspendUpdateLayout();
          topLevelTable.SuspendUpdateGeometryRefreshUI();
          TableElement tableElement = activeElement as TableElement;
          if (index == ruler.BorderPositions.Length - 1)
            tableElement = activeElement.TableOwner as TableElement;
          if (tableElement != null && !(activeElement is VirtualColumn) && tableElement.IsTopLevelTable && !tableElement.IsVirtualNode)
          {
            if (index != 0)
              tableElement.SetGridColumnWidth(index - 1, this.rulerHorizontal.BorderPositions[index] - this.rulerHorizontal.BorderPositions[index - 1], true, false, false);
            if (index != 0 && index != this.rulerHorizontal.BorderPositions.Length - 1 && type == enumTypeDrag.tdBordersOne)
              tableElement.SetGridColumnWidth(index, this.rulerHorizontal.BorderPositions[index + 1] - this.rulerHorizontal.BorderPositions[index], true, false, false);
            if (index == 0)
            {
              RectangleF bounds3 = topLevelTable.Bounds with
              {
                X = this.rulerHorizontal.BorderPositions[0]
              };
              bounds3.Width = bounds3.Width + oldValue - this.rulerHorizontal.BorderPositions[0];
              if (bounds3.Location != topLevelTable.Location)
                tableElement.RecalcCellLocations(bounds3.Location, 0, tableElement.Nodes.Count, false, false, false);
              activeElement.SetCellSizes(bounds3, false, true, true, true);
            }
          }
          else
          {
            List<DocumentTreeNode> documentTreeNodeList1 = new List<DocumentTreeNode>();
            List<RectangleElement> rectangleElementList = new List<RectangleElement>();
            if (index != 0)
              topLevelTable.FindResizableRightSide(rectangleElementList, oldValue);
            else
              topLevelTable.FindResizableLeftSide(rectangleElementList, oldValue);
            switch (activeElement)
            {
              case TableElement _:
              case VirtualColumn _:
label_19:
                if (index != 0 && index != this.rulerHorizontal.BorderPositions.Length - 1)
                {
                  for (int index1 = 0; index1 < rectangleElementList.Count; ++index1)
                  {
                    if (cur_var.Contains((DocumentTreeNode) rectangleElementList[index1]) || cur_var.Contains((DocumentTreeNode) rectangleElementList[index1].NextNode))
                      documentTreeNodeList1.Add((DocumentTreeNode) rectangleElementList[index1]);
                  }
                }
                if (documentTreeNodeList1.Count == 0)
                  documentTreeNodeList1.AddRange((IEnumerable<DocumentTreeNode>) rectangleElementList);
                bool flag1 = false;
                bool flag2 = false;
                bool flag3 = false;
                List<DocumentTreeNode> documentTreeNodeList2 = new List<DocumentTreeNode>();
                for (int index2 = 0; index2 < documentTreeNodeList1.Count; ++index2)
                {
                  if (index2 == documentTreeNodeList1.Count - 1)
                    flag1 = true;
                  RectangleElement parentCell1 = documentTreeNodeList1[index2] as RectangleElement;
                  RectangleElement rectangleElement = parentCell1.NextNode;
                  if (rectangleElement == null && parentCell1.ParentCell != null && parentCell1.ParentCell.ParentCell != null)
                  {
                    parentCell1 = (RectangleElement) parentCell1.ParentCell.ParentCell;
                    rectangleElement = parentCell1.NextNode;
                  }
                  RectangleF bounds4 = parentCell1.Bounds;
                  bool flag4 = cur_var.Contains((DocumentTreeNode) parentCell1) || cur_var.Contains((DocumentTreeNode) rectangleElement) || parentCell1.WidthOverrided;
                  TableData parentCell2 = parentCell1.ParentCell;
                  TableData paramsOwner = (TableData) null;
                  List<RowColParams> rowColParamsList = (List<RowColParams>) null;
                  bounds1 = parentCell1.Bounds;
                  if ((double) bounds1.Right == (double) oldValue)
                  {
                    int index3 = index - 1;
                    if (parentCell1 is TableElement && !parentCell1.IsVirtualNode)
                      index3 = 0;
                    if (index == 0)
                      index3 = 0;
                    bounds4.X = this.rulerHorizontal.BorderPositions[index3];
                    bounds4.Width = bounds4.Width + this.rulerHorizontal.BorderPositions[index] - oldValue;
                    if (flag4 && !documentTreeNodeList2.Contains((DocumentTreeNode) parentCell1))
                    {
                      parentCell1.WidthOverrided = true;
                      parentCell1.SetCellSizes(bounds4, false, true, true, true);
                      documentTreeNodeList2.Add((DocumentTreeNode) parentCell1);
                    }
                    else if (parentCell2 != null && !flag2)
                    {
                      rowColParamsList = parentCell2.GetGridColumnsParams(out paramsOwner, out bool _, true, true);
                      if (paramsOwner != null)
                      {
                        int gridColumnIndex = parentCell1.GetGridColumnIndex();
                        float width = 0.0f;
                        if (rowColParamsList != null && rowColParamsList.Count > gridColumnIndex)
                          width = rowColParamsList[gridColumnIndex].Size + this.rulerHorizontal.BorderPositions[index] - oldValue;
                        paramsOwner.SetGridColumnWidth(gridColumnIndex, width, true, false, false);
                        flag2 = true;
                      }
                    }
                  }
                  if (index == 0)
                    rectangleElement = parentCell1;
                  if (rectangleElement != null && type == enumTypeDrag.tdBordersOne)
                  {
                    bounds4 = rectangleElement.Bounds with
                    {
                      X = this.rulerHorizontal.BorderPositions[index]
                    };
                    bounds4.Width = bounds4.Width - this.rulerHorizontal.BorderPositions[index] + oldValue;
                    if (flag4 && !documentTreeNodeList2.Contains((DocumentTreeNode) rectangleElement))
                    {
                      rectangleElement.WidthOverrided = true;
                      rectangleElement.SetCellSizes(bounds4, false, true, true, true);
                      documentTreeNodeList2.Add((DocumentTreeNode) rectangleElement);
                    }
                    else if (paramsOwner != null && !flag3)
                    {
                      int gridColumnIndex = rectangleElement.GetGridColumnIndex();
                      float width = 0.0f;
                      if (rowColParamsList != null && rowColParamsList.Count > gridColumnIndex)
                        width = rowColParamsList[gridColumnIndex].Size - this.rulerHorizontal.BorderPositions[index] + oldValue;
                      paramsOwner.SetGridColumnWidth(gridColumnIndex, width, true, false, false);
                      flag3 = true;
                    }
                  }
                  if (index == 0 & flag1)
                  {
                    bounds4 = topLevelTable.Bounds with
                    {
                      X = this.rulerHorizontal.BorderPositions[0]
                    };
                    bounds4.Width = bounds4.Width - this.rulerHorizontal.BorderPositions[0] + oldValue;
                    topLevelTable.SetCellSizes(bounds4, false, true, true, true, false);
                  }
                }
                break;
              default:
                cur_var.Clear();
                goto label_19;
            }
          }
          topLevelTable.ResumeUpdateLayout(false, true);
          topLevelTable.ResumeUpdateRefreshUI(true, true);
        }
      }
      if (ruler == this.rulerVertical && this.ActiveElement is RectangleElement)
      {
        RectangleElement activeElement = this.ActiveElement as RectangleElement;
        if (activeElement.TopLevelTable == null)
        {
          RectangleF bounds5 = activeElement.Bounds with
          {
            Y = ruler.BorderPositions[0],
            Height = ruler.BorderPositions[1] - ruler.BorderPositions[0]
          };
          activeElement.SetCellSizes(bounds5, true, true, true, true);
          activeElement.UpdateLayout(true);
        }
        else
        {
          List<DocumentTreeNode> documentTreeNodeList3 = new List<DocumentTreeNode>();
          TableData tableOwner = activeElement.TableOwner;
          tableOwner.SuspendUpdateLayout();
          tableOwner.SuspendUpdateGeometryRefreshUI();
          List<DocumentTreeNode> documentTreeNodeList4 = new List<DocumentTreeNode>();
          List<RectangleElement> rectangleElementList = new List<RectangleElement>();
          tableOwner.FindResizableBottomSide(rectangleElementList, oldValue);
          documentTreeNodeList4.AddRange((IEnumerable<DocumentTreeNode>) rectangleElementList);
          if (index == 0)
          {
            RectangleF bounds6 = tableOwner.Bounds with
            {
              Y = ruler.BorderPositions[0]
            };
            bounds6.Height = bounds6.Height - ruler.BorderPositions[0] + oldValue;
            if (bounds6.Location != tableOwner.Location)
              tableOwner.RecalcCellLocations(bounds6.Location, 0, tableOwner.Nodes.Count, false, false, false);
            tableOwner.SetCellSizes(bounds6, false, true, true, true, false);
          }
          for (int index4 = 0; index4 < documentTreeNodeList4.Count; ++index4)
          {
            if (index != 0)
            {
              TableData parentCell = documentTreeNodeList4[index4] as TableData;
              if (!parentCell.IsRow)
                parentCell = parentCell.ParentCell;
              RectangleF bounds7 = parentCell.Bounds;
              bounds7.Height = bounds7.Height + ruler.BorderPositions[index] - oldValue;
              parentCell.SetCellSizes(bounds7, false, true, true, true, false);
            }
          }
          if ((double) tableOwner.MaxHeight != 0.0)
          {
            TableData tableData = tableOwner;
            bounds1 = tableOwner.Bounds;
            double height = (double) bounds1.Height;
            tableData.MaxHeight = (float) height;
          }
          tableOwner.ResumeUpdateLayout(false, true);
          tableOwner.ResumeUpdateRefreshUI(true, true);
        }
      }
      this.SetRulerBorders();
      this.rulerHorizontal.UpdateIdents();
      this.rulerHorizontal.Refresh();
      this.rulerVertical.Refresh();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  public void SetRulerBorders()
  {
    try
    {
      float[] array = (float[]) null;
      float[] leftOffset = (float[]) null;
      float[] rightOffset = (float[]) null;
      if (this.rulerHorizontal.Visible)
      {
        if (this.ActiveElement != null && this.ActiveElement is PageElementNode && (this.ActiveElement as PageElementNode).Page == this.ActivePage && this.ActiveElement is RectangleElement)
        {
          RectangleElement activeElement = this.ActiveElement as RectangleElement;
          if (activeElement.TopLevelTable == null)
          {
            array = new float[2]
            {
              activeElement.Bounds.Left,
              activeElement.Bounds.Right
            };
            leftOffset = new float[2]
            {
              activeElement.RightMargin,
              activeElement.RightMargin
            };
            rightOffset = new float[2]
            {
              activeElement.LeftMargin,
              activeElement.LeftMargin
            };
            this.rulerHorizontal.Index = 0;
          }
          else
          {
            switch (activeElement)
            {
              case TableElement _:
              case VirtualColumn _:
                TableElement tableElement = activeElement as TableElement;
                if (!(activeElement is VirtualColumn) && tableElement.IsTopLevelTable && !tableElement.IsVirtualNode)
                {
                  List<RowColParams> rowColParamsList1 = new List<RowColParams>();
                  float[] numArray1 = new float[0];
                  List<DocumentTreeNode> cur_var = new List<DocumentTreeNode>();
                  this.GetRealCells(activeElement, ref cur_var);
                  if (tableElement.GridColumnsParams != null)
                  {
                    List<RowColParams> rowColParamsList2 = new List<RowColParams>((IEnumerable<RowColParams>) tableElement.GridColumnsParams);
                    float[] numArray2 = new float[rowColParamsList2.Count];
                    for (int index = 0; index < rowColParamsList2.Count; ++index)
                      numArray2[index] = rowColParamsList2[index].Size;
                    array = new float[rowColParamsList2.Count + 1];
                    rightOffset = new float[rowColParamsList2.Count + 1];
                    leftOffset = new float[rowColParamsList2.Count + 1];
                    if (cur_var.Count > 0)
                    {
                      RectangleElement topLevelTable = (RectangleElement) (cur_var[0] as RectangleElement).TopLevelTable;
                      array[0] = topLevelTable.Bounds.Left;
                      rightOffset[0] = topLevelTable.LeftMargin;
                      leftOffset[0] = topLevelTable.RightMargin;
                      for (int index = 0; index < numArray2.Length; ++index)
                      {
                        array[index + 1] = array[index] + numArray2[index];
                        rightOffset[index + 1] = topLevelTable.LeftMargin;
                        leftOffset[index + 1] = topLevelTable.RightMargin;
                      }
                    }
                  }
                  else
                  {
                    TableData parentCell1 = activeElement.ParentCell;
                    if (activeElement is VirtualColumn)
                    {
                      TableData parentCell2 = ((activeElement as VirtualColumn).Nodes[0] as RectangleElement).ParentCell;
                    }
                    ArrayList arrayList1 = new ArrayList();
                    if (tableElement.NodesCount > 0)
                    {
                      DocumentTreeNode node = tableElement.Nodes[0];
                      if (node.NodesCount > 0)
                      {
                        for (int index = 0; index < node.NodesCount; ++index)
                          arrayList1.Add((object) node.Nodes[index]);
                      }
                    }
                    ArrayList arrayList2 = new ArrayList();
                    for (int index = 0; index < arrayList1.Count; ++index)
                    {
                      if (!arrayList2.Contains((object) (arrayList1[index] as RectangleElement).Bounds.Left))
                        arrayList2.Add((object) (arrayList1[index] as RectangleElement).Bounds.Left);
                      if (!arrayList2.Contains((object) (arrayList1[index] as RectangleElement).Bounds.Right))
                        arrayList2.Add((object) (arrayList1[index] as RectangleElement).Bounds.Right);
                    }
                    array = new float[arrayList2.Count];
                    rightOffset = new float[arrayList2.Count];
                    leftOffset = new float[arrayList2.Count];
                    for (int index = 0; index < arrayList2.Count; ++index)
                    {
                      array[index] = (float) arrayList2[index];
                      rightOffset[index] = activeElement.TopLevelTable.LeftMargin;
                      leftOffset[index] = activeElement.TopLevelTable.RightMargin;
                    }
                    Array.Sort<float>(array);
                    this.rulerHorizontal.Index = Array.IndexOf<float>(array, activeElement.Bounds.Left);
                  }
                  this.rulerHorizontal.Index = 0;
                  break;
                }
                List<DocumentTreeNode> cur_var1 = new List<DocumentTreeNode>();
                this.GetTopCells(activeElement, ref cur_var1, false);
                for (TableData parentCell = (cur_var1[0] as RectangleElement).ParentCell; parentCell != null; parentCell = parentCell.ParentCell)
                {
                  if (parentCell.IsRow)
                  {
                    for (int index = 0; index < parentCell.NodesCount; ++index)
                    {
                      if (!cur_var1.Contains(parentCell.Nodes[index]))
                        cur_var1.Add(parentCell.Nodes[index]);
                    }
                  }
                }
                ArrayList arrayList3 = new ArrayList();
                for (int index = 0; index < cur_var1.Count; ++index)
                {
                  if (!arrayList3.Contains((object) (cur_var1[index] as RectangleElement).Bounds.Left))
                    arrayList3.Add((object) (cur_var1[index] as RectangleElement).Bounds.Left);
                  if (!arrayList3.Contains((object) (cur_var1[index] as RectangleElement).Bounds.Right))
                    arrayList3.Add((object) (cur_var1[index] as RectangleElement).Bounds.Right);
                }
                array = new float[arrayList3.Count];
                rightOffset = new float[arrayList3.Count];
                leftOffset = new float[arrayList3.Count];
                for (int index = 0; index < arrayList3.Count; ++index)
                {
                  array[index] = (float) arrayList3[index];
                  rightOffset[index] = activeElement.TopLevelTable.LeftMargin;
                  leftOffset[index] = activeElement.TopLevelTable.RightMargin;
                }
                Array.Sort<float>(array);
                RectangleElement rectangleElement = activeElement;
                while (rectangleElement.IsVirtualNode)
                  rectangleElement = rectangleElement.Nodes[0] as RectangleElement;
                this.rulerHorizontal.Index = Array.IndexOf<float>(array, rectangleElement.Bounds.Left);
                break;
              default:
                TableData parentCell3 = activeElement.ParentCell;
                if (activeElement is VirtualColumn)
                  parentCell3 = ((activeElement as VirtualColumn).Nodes[0] as RectangleElement).ParentCell;
                ArrayList arrayList4 = new ArrayList();
                for (; parentCell3 != null; parentCell3 = parentCell3.ParentCell)
                {
                  if (parentCell3.IsRow)
                  {
                    for (int index = 0; index < parentCell3.NodesCount; ++index)
                      arrayList4.Add((object) parentCell3.Nodes[index]);
                  }
                }
                ArrayList arrayList5 = new ArrayList();
                for (int index = 0; index < arrayList4.Count; ++index)
                {
                  if (!arrayList5.Contains((object) (arrayList4[index] as RectangleElement).Bounds.Left))
                    arrayList5.Add((object) (arrayList4[index] as RectangleElement).Bounds.Left);
                  if (!arrayList5.Contains((object) (arrayList4[index] as RectangleElement).Bounds.Right))
                    arrayList5.Add((object) (arrayList4[index] as RectangleElement).Bounds.Right);
                }
                array = new float[arrayList5.Count];
                rightOffset = new float[arrayList5.Count];
                leftOffset = new float[arrayList5.Count];
                for (int index = 0; index < arrayList5.Count; ++index)
                {
                  array[index] = (float) arrayList5[index];
                  rightOffset[index] = activeElement.TopLevelTable.LeftMargin;
                  leftOffset[index] = activeElement.TopLevelTable.RightMargin;
                }
                Array.Sort<float>(array);
                this.rulerHorizontal.Index = Array.IndexOf<float>(array, activeElement.Bounds.Left);
                break;
            }
          }
          this.rulerHorizontal.BordersReadOnly = activeElement.TemplateId != null || activeElement.TableOwner != null && activeElement.TableOwner.TemplateId != null;
        }
        this.rulerHorizontal.SetBordersPositions(array, leftOffset, rightOffset);
      }
      if (!this.rulerVertical.Visible)
        return;
      if (this.ActiveElement != null && this.ActiveElement is RectangleElement)
      {
        RectangleElement activeElement = this.ActiveElement as RectangleElement;
        if (activeElement.TopLevelTable == null)
        {
          array = new float[2]
          {
            activeElement.Bounds.Top,
            activeElement.Bounds.Bottom
          };
          leftOffset = new float[2]
          {
            activeElement.BottomMargin,
            activeElement.BottomMargin
          };
          rightOffset = new float[2]
          {
            activeElement.TopMargin,
            activeElement.TopMargin
          };
        }
        else
        {
          switch (activeElement)
          {
            case TableElement _:
            case VirtualColumn _:
              TableElement tableElement = activeElement as TableElement;
              if (!(activeElement is VirtualColumn) && tableElement.IsTopLevelTable && !tableElement.IsVirtualNode)
              {
                ArrayList arrayList = new ArrayList();
                for (int index = 0; index < tableElement.NodesCount; ++index)
                {
                  if (!arrayList.Contains((object) (tableElement.Nodes[index] as RectangleElement).Bounds.Top))
                    arrayList.Add((object) (tableElement.Nodes[index] as RectangleElement).Bounds.Top);
                  if (!arrayList.Contains((object) (tableElement.Nodes[index] as RectangleElement).Bounds.Bottom))
                    arrayList.Add((object) (tableElement.Nodes[index] as RectangleElement).Bounds.Bottom);
                }
                array = new float[arrayList.Count];
                rightOffset = new float[arrayList.Count];
                leftOffset = new float[arrayList.Count];
                for (int index = 0; index < arrayList.Count; ++index)
                {
                  array[index] = (float) arrayList[index];
                  rightOffset[index] = activeElement.TopLevelTable.TopMargin;
                  leftOffset[index] = activeElement.TopLevelTable.BottomMargin;
                }
                Array.Sort<float>(array);
                break;
              }
              List<DocumentTreeNode> cur_var = new List<DocumentTreeNode>();
              this.GetLeftCells(activeElement, ref cur_var, false);
              ArrayList arrayList6 = new ArrayList();
              if (cur_var != null && cur_var.Count > 0)
              {
                for (TableData parentCell = cur_var[0] is RectangleElement rectangleElement ? rectangleElement.ParentCell : (TableData) null; parentCell != null; parentCell = parentCell.ParentCell)
                {
                  if (parentCell.IsColumn)
                  {
                    for (int index = 0; index < parentCell.NodesCount; ++index)
                    {
                      if (!cur_var.Contains(parentCell.Nodes[index]))
                        cur_var.Add(parentCell.Nodes[index]);
                    }
                  }
                }
                for (int index = 0; index < cur_var.Count; ++index)
                {
                  if (cur_var[index] is RectangleElement rectangleElement)
                  {
                    if (!arrayList6.Contains((object) rectangleElement.Bounds.Top))
                      arrayList6.Add((object) rectangleElement.Bounds.Top);
                    if (!arrayList6.Contains((object) rectangleElement.Bounds.Bottom))
                      arrayList6.Add((object) rectangleElement.Bounds.Bottom);
                  }
                }
              }
              array = new float[arrayList6.Count];
              rightOffset = new float[arrayList6.Count];
              leftOffset = new float[arrayList6.Count];
              for (int index = 0; index < arrayList6.Count; ++index)
              {
                array[index] = (float) arrayList6[index];
                rightOffset[index] = activeElement.TopLevelTable.TopMargin;
                leftOffset[index] = activeElement.TopLevelTable.BottomMargin;
              }
              Array.Sort<float>(array);
              break;
            default:
              TableData parentCell4 = activeElement.ParentCell;
              ArrayList arrayList7 = new ArrayList();
              for (; parentCell4 != null; parentCell4 = parentCell4.ParentCell)
              {
                if (parentCell4.IsColumn)
                {
                  for (int index = 0; index < parentCell4.NodesCount; ++index)
                    arrayList7.Add((object) parentCell4.Nodes[index]);
                }
              }
              ArrayList arrayList8 = new ArrayList();
              for (int index = 0; index < arrayList7.Count; ++index)
              {
                if (!arrayList8.Contains((object) (arrayList7[index] as RectangleElement).Bounds.Top))
                  arrayList8.Add((object) (arrayList7[index] as RectangleElement).Bounds.Top);
                if (!arrayList8.Contains((object) (arrayList7[index] as RectangleElement).Bounds.Bottom))
                  arrayList8.Add((object) (arrayList7[index] as RectangleElement).Bounds.Bottom);
              }
              array = new float[arrayList8.Count];
              rightOffset = new float[arrayList8.Count];
              leftOffset = new float[arrayList8.Count];
              for (int index = 0; index < arrayList8.Count; ++index)
              {
                array[index] = (float) arrayList8[index];
                rightOffset[index] = activeElement.TopLevelTable.TopMargin;
                leftOffset[index] = activeElement.TopLevelTable.BottomMargin;
              }
              Array.Sort<float>(array);
              break;
          }
        }
        this.rulerVertical.BordersReadOnly = activeElement.TemplateId != null || activeElement.TableOwner != null && activeElement.TableOwner.TemplateId != null;
      }
      this.rulerVertical.SetBordersPositions(array, leftOffset, rightOffset);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void subPanel_SizeChanged(object sender, EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      if (this.PageControl != null)
        this.PageControl.Size = this.subPanel.Size;
      if ((double) this.documentScale <= 9.9999997473787516E-06)
        return;
      double num = (double) this.SetZoom(this.zoomMode, this.documentScale);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void subPanel_LocationChanged(object sender, EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      if (this.PageControl != null)
        this.PageControl.Location = new Point(0, 0);
      double num = (double) this.SetZoom(this.zoomMode, this.documentScale);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void label1_MouseEnter(object sender, EventArgs e)
  {
    try
    {
      if (this.LockForClosing || sender == null || !(sender is Label))
        return;
      ((Control) sender).BackColor = Color.FromArgb((int) byte.MaxValue, 238, 194);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void label1_MouseLeave(object sender, EventArgs e)
  {
    try
    {
      if (this.LockForClosing || sender == null || !(sender is Label))
        return;
      ((Control) sender).BackColor = Color.FromArgb((int) byte.MaxValue, 192 /*0xC0*/, 111);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void rulerHorizontal_BorderPositionChanged(
    object sender,
    BorderPositionChanged_EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      this.SetBordersToElement(sender as Ruler, e.OldValue, e.Index, e.Type);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void rulerHorizontal_IdentChanged(object sender, IdentChanged_EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      ParagraphFormat pf = new ParagraphFormat(true);
      float? nullable1 = this.rulerHorizontal.IdentLeft;
      float? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = this.rulerHorizontal.IdentFirstLine;
        if (nullable1.HasValue)
        {
          ParagraphFormat paragraphFormat = pf;
          nullable1 = this.rulerHorizontal.IdentFirstLine;
          nullable2 = this.rulerHorizontal.IdentLeft;
          float? nullable3 = nullable1.HasValue & nullable2.HasValue ? new float?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new float?();
          paragraphFormat.IdentFirstLine = nullable3;
          goto label_5;
        }
      }
      pf.IdentFirstLine = this.rulerHorizontal.IdentFirstLine;
label_5:
      pf.IdentLeft = this.rulerHorizontal.IdentLeft;
      pf.IdentRight = this.rulerHorizontal.IdentRight;
      nullable2 = pf.IdentFirstLine;
      if (nullable2.HasValue)
      {
        ParagraphFormat paragraphFormat = pf;
        nullable2 = pf.IdentFirstLine;
        float num = 10f;
        float? nullable4;
        if (!nullable2.HasValue)
        {
          nullable1 = new float?();
          nullable4 = nullable1;
        }
        else
          nullable4 = new float?(nullable2.GetValueOrDefault() / num);
        paragraphFormat.IdentFirstLine = nullable4;
      }
      nullable2 = pf.IdentLeft;
      if (nullable2.HasValue)
      {
        ParagraphFormat paragraphFormat = pf;
        nullable2 = pf.IdentLeft;
        float num = 10f;
        float? nullable5;
        if (!nullable2.HasValue)
        {
          nullable1 = new float?();
          nullable5 = nullable1;
        }
        else
          nullable5 = new float?(nullable2.GetValueOrDefault() / num);
        paragraphFormat.IdentLeft = nullable5;
      }
      nullable2 = pf.IdentRight;
      if (nullable2.HasValue)
      {
        ParagraphFormat paragraphFormat = pf;
        nullable2 = pf.IdentRight;
        float num = 10f;
        float? nullable6;
        if (!nullable2.HasValue)
        {
          nullable1 = new float?();
          nullable6 = nullable1;
        }
        else
          nullable6 = new float?(nullable2.GetValueOrDefault() / num);
        paragraphFormat.IdentRight = nullable6;
      }
      this.SetIdentsToElement(pf);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void subPanel_Click(object sender, EventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      this.SetSelection((DocumentTreeNode) null, false, false);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  private void subPanel_MouseDown(object sender, MouseEventArgs e)
  {
    try
    {
      if (this.LockForClosing)
        return;
      if (e.Button == MouseButtons.Right)
        this.CustomizeToolbars(e.Location);
      else
        this.SetSelection((DocumentTreeNode) this.Document, false, Point.Empty, false, false);
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  /// <summary>Вызываем меню видимости тулбаров</summary>
  /// <param name="pos"></param>
  private void CustomizeToolbars(Point pos)
  {
    try
    {
      if (this.LockForClosing || this.BarManager == null)
        return;
      MenuBarItem menuBarItem = new MenuBarItem();
      ArrayList arrayList = new ArrayList((ICollection) this.BarManager.GetToolbarsList());
      Intermech.Bars.ToolBar toolBar1 = new Intermech.Bars.ToolBar()
      {
        Renderer = this.BarManager.Renderer,
        Items = {
          (ToolbarItemBase) menuBarItem
        }
      };
      foreach (Intermech.Bars.ToolBar toolBar2 in arrayList)
      {
        if (toolBar2.Closable)
        {
          MenuButtonItem menuButtonItem = new MenuButtonItem();
          menuButtonItem.Text = toolBar2.Text;
          menuButtonItem.Checked = toolBar2.IsOpen;
          menuButtonItem.Tag = (object) toolBar2;
          menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem);
        }
      }
      if (menuBarItem.HasChildren)
      {
        MenuButtonItem menuButtonItem = menuBarItem.Show((Control) this.subPanel, pos);
        if (menuButtonItem != null)
          (menuButtonItem.Tag as Intermech.Bars.ToolBar).Hidden = (menuButtonItem.Tag as Intermech.Bars.ToolBar).IsOpen;
      }
      menuBarItem.Dispose();
    }
    catch (Exception ex)
    {
      string errorFormCaption = LocalizationHolder.rm.GetString("Document.Model_617");
      ImDocumentData.ShowException(ex, errorFormCaption);
    }
  }

  internal delegate void MethodInvoke_PageNameChanged(object sender, NameChanged_EventArgs e);

  internal delegate void MethodInvoke_Sender_EventArgs(object sender, EventArgs e);

  /// <summary>Делегат для запуска SelectTab через Invoke</summary>
  internal delegate void SelectTab_DelegateForInvoke(Tab tab);

  internal delegate void MethodInvoke_EventArgs(EventArgs e);
}
