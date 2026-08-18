
// Type: Intermech.Docking.DocumentContainer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using Intermech.Docking.Rendering;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Docking;

[ToolboxBitmap(typeof (DocumentContainer))]
[Designer(typeof (DocumentContainerDesigner))]
[DefaultEvent("ActiveDocumentChanged")]
[ToolboxItem(true)]
public class DocumentContainer : DockContainer, IMessageFilter
{
  private DockControl _activeDocument;
  private DockControl _oldActiveDocument;
  private Intermech.Docking.Rendering.BorderStyle _borderStyle;
  private DockControl[] _documentsList;
  private int _documentListIndex;
  private ArrayList _documents;
  private DockManager _hardManager;
  private int _activeDocumentIndex;
  private bool _keyboardNavigation;
  private bool _showImageInDocumentTab;
  private bool _integralClose;

  public event ActiveDocumentEventHandler ActiveDocumentChanged;

  public event DocumentClosingEventHandler DocumentClosing;

  public event DocumentClosedEventHandler DocumentClosed;

  public event DocumentContainer.DocumentListClickEventHandler DocumentListClick;

  public DocumentContainer()
  {
    this._activeDocument = (DockControl) null;
    this._borderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this._activeDocumentIndex = -1;
    this._keyboardNavigation = true;
    this._showImageInDocumentTab = false;
    this._documents = new ArrayList();
    base.Dock = DockStyle.Fill;
  }

  private DockControl ActivateDocument()
  {
    if (this._documentListIndex > this._documentsList.Length)
      this._documentListIndex = this._documentsList.Length;
    DockControl documents = this._documentsList[this._documents.Count - 1 - this._documentListIndex];
    documents._layoutSystem.SelectedControl = documents;
    documents.Activate();
    return documents;
  }

  private void a(DockControl document)
  {
    if (document != null && document._layoutSystem != null && this._activeDocument != document && this._layoutSystems.Contains((object) document._layoutSystem))
    {
      this.PerformResize((LayoutSystemBase) document._layoutSystem, document._layoutSystem.Bounds);
      this.Invalidate(document._layoutSystem.Bounds);
    }
    if (this._activeDocument == null || this._activeDocument._layoutSystem == null || !this._layoutSystems.Contains((object) this._activeDocument._layoutSystem))
      return;
    this.PerformResize((LayoutSystemBase) this._activeDocument._layoutSystem, this._activeDocument._layoutSystem.Bounds);
    this.Invalidate(this._activeDocument._layoutSystem.Bounds);
  }

  public void AddDocument(DockControl control)
  {
    if (this._documents.Contains((object) control))
      throw new ArgumentException("Document already belongs to this Document Container.");
    ControlLayoutSystem layoutSystem = this.GetLayoutSystem(this.LayoutSystem);
    if (layoutSystem == null)
    {
      layoutSystem = (ControlLayoutSystem) new DocumentLayoutSystem();
      this.LayoutSystem.LayoutSystems.Add((LayoutSystemBase) layoutSystem);
    }
    layoutSystem.Controls.Add(control);
  }

  public bool CheckCloseDocuments()
  {
    CancelEventArgs cea = new CancelEventArgs();
    for (int index = 0; index < this._documents.Count; ++index)
    {
      if (this._documents[index] is DockControl document)
      {
        document.CheckClose(cea);
        if (cea.Cancel)
          return false;
      }
    }
    return true;
  }

  private void FillDocumentsList()
  {
    ArrayList arrayList = new ArrayList(20);
    foreach (LayoutSystemBase layoutSystem in this._layoutSystems)
    {
      if (layoutSystem is ControlLayoutSystem)
      {
        foreach (DockControl control in (CollectionBase) ((ControlLayoutSystem) layoutSystem).Controls)
          arrayList.Add((object) control);
      }
    }
    for (int index = this._documents.Count - 1; index >= 0; --index)
    {
      if (!arrayList.Contains(this._documents[index]))
        this._documents.RemoveAt(index);
    }
    foreach (DockControl dockControl in arrayList)
    {
      if (!this._documents.Contains((object) dockControl))
        this._documents.Add((object) dockControl);
    }
  }

  internal void DocumentRemoved(DockControl document)
  {
    if (this._documents.Contains((object) document))
      this._documents.Remove((object) document);
    if (this._activeDocument != document)
      return;
    DockControl activeDocument = this._activeDocument;
    this._activeDocument = this.GetOldActiveDocument(activeDocument);
    this.OnActiveDocumentChanged(new ActiveDocumentEventArgs(activeDocument, this._activeDocument));
    this.a(activeDocument);
  }

  internal DockControl GetOldActiveDocument(DockControl exceptDc)
  {
    return this._hardManager != null ? this._hardManager.FindMostRecentlyUsedDocument(exceptDc) : this._documents.OfType<DockControl>().Where<DockControl>((Func<DockControl, bool>) (d => d.DockLocation == DockLocation.Document && d != exceptDc)).OrderByDescending<DockControl, DateTime>((Func<DockControl, DateTime>) (i => i.LastFocused)).FirstOrDefault<DockControl>();
  }

  internal void ActivateDocument(DockControl newDocument)
  {
    if (newDocument == this._activeDocument)
      return;
    if (this._activeDocumentIndex == -1)
    {
      if (this._documents.Contains((object) newDocument))
        this._documents.Remove((object) newDocument);
      this._documents.Add((object) newDocument);
    }
    this._oldActiveDocument = this._activeDocument;
    this._activeDocument = newDocument;
    this.OnActiveDocumentChanged(new ActiveDocumentEventArgs(this._oldActiveDocument, this._activeDocument));
    this.a(this._oldActiveDocument);
    if (this._documentsList != null)
      return;
    newDocument.LastFocused = DateTime.Now;
  }

  internal void AddDocumentDockControl(DockControl document)
  {
    if (this._documents.Contains((object) document))
      return;
    this._documents.Add((object) document);
  }

  internal override void LayoutSystemsChanged()
  {
    base.LayoutSystemsChanged();
    this.FillDocumentsList();
    if (this._activeDocument == null || this._activeDocument._layoutSystem != null && this._layoutSystems.Contains((object) this._activeDocument._layoutSystem))
      return;
    DockControl activeDocument = this._activeDocument;
    this._activeDocument = this.GetOldActiveDocument((DockControl) null);
    this.OnActiveDocumentChanged(new ActiveDocumentEventArgs(activeDocument, this._activeDocument));
    this.a(activeDocument);
  }

  protected virtual void OnActiveDocumentChanged(ActiveDocumentEventArgs e)
  {
    if (this.ActiveDocumentChanged == null)
      return;
    this.ActiveDocumentChanged((object) this, e);
  }

  protected internal virtual void OnDocumentClosing(DocumentClosingEventArgs e)
  {
    if (this.DocumentClosing == null)
      return;
    this.DocumentClosing((object) this, e);
  }

  protected internal virtual void OnDocumentClosed(DockControl document)
  {
    if (this.DocumentClosed == null)
      return;
    this.DocumentClosed((object) this, new DocumentClosedEventArgs(document));
  }

  internal void OnDocumentListClick(DocumentLayoutSystem dls)
  {
    if (this.DocumentListClick == null)
      return;
    this.DocumentListClick((object) this, new DocumentListEventArgs(dls));
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    DockControl.PaintBorder((Control) this, e.Graphics, this._borderStyle);
  }

  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    this.WorkingRenderer.DrawDocumentContainerBackground(pevent.Graphics, this.DisplayRectangle);
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData != (Keys.Tab | Keys.Control) && keyData != (Keys.Tab | Keys.Shift | Keys.Control) || !this._keyboardNavigation)
      return base.ProcessCmdKey(ref msg, keyData);
    DockControl[] documents = this._hardManager.GetDocuments();
    if (documents.Length > 1)
    {
      DateTime[] keys = new DateTime[documents.Length];
      for (int index = 0; index < documents.Length; ++index)
        keys[index] = documents[index].LastFocused;
      Array.Sort<DateTime, DockControl>(keys, documents);
      this._documentsList = documents;
      this._documentListIndex = (keyData & Keys.Shift) != Keys.Shift ? 1 : this._documentsList.Length - 1;
      this.ActivateDocument();
      Application.AddMessageFilter((IMessageFilter) this);
    }
    return true;
  }

  bool IMessageFilter.PreFilterMessage(ref Message m)
  {
    IntPtr wparam;
    if (m.Msg == 256 /*0x0100*/)
    {
      wparam = m.WParam;
      if (wparam.ToInt32() == 9)
      {
        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
          --this._documentListIndex;
        else
          ++this._documentListIndex;
        if (this._documentListIndex > this._documentsList.Length - 1)
          this._documentListIndex = 0;
        if (this._documentListIndex < 0)
          this._documentListIndex = this._documentsList.Length - 1;
        this.ActivateDocument();
        return true;
      }
    }
    if (m.Msg == 256 /*0x0100*/)
    {
      wparam = m.WParam;
      if (wparam.ToInt32() == 16 /*0x10*/)
        goto label_17;
    }
    if (m.Msg == 257)
    {
      wparam = m.WParam;
      if (wparam.ToInt32() == 17)
        goto label_16;
    }
    if (m.Msg != 256 /*0x0100*/)
      return false;
label_16:
    this._documentListIndex = -1;
    this._documentsList = (DockControl[]) null;
    Application.RemoveMessageFilter((IMessageFilter) this);
label_17:
    return true;
  }

  public void RemoveDocument(DockControl control)
  {
    if (!this._documents.Contains((object) control))
      throw new ArgumentException("Document not found.");
    DockHelper.DetachDockControl(control);
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  [DefaultValue(typeof (DockControl), null)]
  public DockControl ActiveDocument
  {
    get => this._activeDocument;
    set
    {
      if (value == null)
        throw new ArgumentNullException();
      if (!this._documents.Contains((object) value))
        throw new InvalidOperationException("Specified DockControl does not belong to this DocumentContainer.");
      value.Activate();
    }
  }

  [DefaultValue(typeof (Intermech.Docking.Rendering.BorderStyle), "Flat")]
  [Category("Appearance")]
  [Description("The type of border to be drawn around the control.")]
  public Intermech.Docking.Rendering.BorderStyle BorderStyle
  {
    get => this._borderStyle;
    set
    {
      this._borderStyle = value;
      this.OnResize(EventArgs.Empty);
    }
  }

  protected override Size DefaultSize => new Size(300, 300);

  public override Rectangle DisplayRectangle
  {
    get
    {
      Rectangle displayRectangle = base.DisplayRectangle;
      switch (this._borderStyle)
      {
        case Intermech.Docking.Rendering.BorderStyle.Flat:
        case Intermech.Docking.Rendering.BorderStyle.RaisedThin:
        case Intermech.Docking.Rendering.BorderStyle.SunkenThin:
          displayRectangle.Inflate(-1, -1);
          return displayRectangle;
        case Intermech.Docking.Rendering.BorderStyle.RaisedThick:
        case Intermech.Docking.Rendering.BorderStyle.SunkenThick:
          displayRectangle.Inflate(-2, -2);
          return displayRectangle;
        default:
          return displayRectangle;
      }
    }
  }

  [Browsable(true)]
  [DefaultValue(typeof (DockStyle), "Fill")]
  public override DockStyle Dock
  {
    get => base.Dock;
    set => base.Dock = value;
  }

  [Browsable(false)]
  public DockControl[] Documents
  {
    get
    {
      DockControl[] documents = new DockControl[this._documents.Count];
      this._documents.CopyTo((Array) documents);
      return documents;
    }
  }

  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Indicates whether the ctrl-tab and ctrl-shift-tab sequences will navigate between documents.")]
  public bool KeyboardNavigation
  {
    get => this._keyboardNavigation;
    set => this._keyboardNavigation = value;
  }

  [Browsable(true)]
  public override RendererBase Renderer
  {
    get => base.Renderer;
    set => base.Renderer = value;
  }

  [Localizable(false)]
  [DefaultValue(false)]
  [Description("Show image in DocumentMode tab.")]
  [Category("Appearance")]
  public bool ShowImageInDocumentTab
  {
    get => this._showImageInDocumentTab;
    set
    {
      if (this._showImageInDocumentTab == value)
        return;
      this._showImageInDocumentTab = value;
      this.LayoutNeeded();
    }
  }

  internal DockControl TopMostDocument
  {
    get
    {
      return this._documents.Count >= 1 ? (DockControl) this._documents[this._documents.Count - 1] : (DockControl) null;
    }
  }

  internal bool IntegralClose
  {
    get => this._integralClose;
    set
    {
      this._integralClose = value;
      this.CalculateAllMetricsAndLayout();
    }
  }

  internal bool DocListEnabled() => this.DocumentListClick != null;

  internal void SetHardManager(DockManager value) => this._hardManager = value;

  public delegate void DocumentListClickEventHandler(object sender, DocumentListEventArgs e);
}
