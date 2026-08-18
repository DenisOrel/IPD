
// Type: AxKGAXLib.AxKGAX
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using KGAXLib;
using Kompas6API5;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace AxKGAXLib;

[AxHost.Clsid("{6b943e71-5ca2-435d-afa3-d7817b13aca2}")]
[DesignTimeVisible(true)]
[DefaultEvent("OnKgMouseDown")]
public class AxKGAX : AxHost
{
  private _DKGAX ocx;
  private AxKGAXEventMulticaster eventMulticaster;
  private AxHost.ConnectionPointCookie cookie;

  public AxKGAX()
    : base("6b943e71-5ca2-435d-afa3-d7817b13aca2")
  {
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(-518)]
  public virtual string Caption
  {
    get
    {
      return this.ocx != null ? this.ocx.Caption : throw new AxHost.InvalidActiveXStateException(nameof (Caption), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (Caption), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.Caption = value;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(-517)]
  public override string Text
  {
    get => this.ocx != null && this.PropsValid() ? this.ocx.Text : base.Text;
    set
    {
      base.Text = value;
      if (this.ocx == null)
        return;
      this.ocx.Text = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(1)]
  public virtual KDocumentType DocumentType
  {
    get
    {
      return this.ocx != null ? this.ocx.DocumentType : throw new AxHost.InvalidActiveXStateException(nameof (DocumentType), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (DocumentType), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.DocumentType = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(2)]
  public virtual string DocumenFileName
  {
    get
    {
      return this.ocx != null ? this.ocx.DocumenFileName : throw new AxHost.InvalidActiveXStateException(nameof (DocumenFileName), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (DocumenFileName), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.DocumenFileName = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(3)]
  public virtual KDocument3DDrawMode Document3DDrawMode
  {
    get
    {
      return this.ocx != null ? this.ocx.Document3DDrawMode : throw new AxHost.InvalidActiveXStateException(nameof (Document3DDrawMode), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (Document3DDrawMode), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.Document3DDrawMode = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DispId(4)]
  public virtual bool Document3DWireframeShadedMode
  {
    get
    {
      return this.ocx != null ? this.ocx.Document3DWireframeShadedMode : throw new AxHost.InvalidActiveXStateException(nameof (Document3DWireframeShadedMode), AxHost.ActiveXInvokeKind.PropertyGet);
    }
    set
    {
      if (this.ocx == null)
        throw new AxHost.InvalidActiveXStateException(nameof (Document3DWireframeShadedMode), AxHost.ActiveXInvokeKind.PropertySet);
      this.ocx.Document3DWireframeShadedMode = value;
    }
  }

  public event _DKGAXEvents_OnKgMouseDownEventHandler OnKgMouseDown;

  public event _DKGAXEvents_OnKgMouseUpEventHandler OnKgMouseUp;

  public event _DKGAXEvents_OnKgMouseDblClickEventHandler OnKgMouseDblClick;

  public event EventHandler OnKgStopCurrentProcess;

  public event _DKGAXEvents_OnKgCreateEventHandler OnKgCreate;

  public event _DKGAXEvents_OnKgPaintEventHandler OnKgPaint;

  public event _DKGAXEvents_OnKgCreateGLListEventHandler OnKgCreateGLList;

  public event _DKGAXEvents_OnKgAddGabatitEventHandler OnKgAddGabatit;

  public event _DKGAXEvents_OnKgErrorLoadDocumentEventHandler OnKgErrorLoadDocument;

  public event _DKGAXEvents_OnKgKeyDownEventHandler OnKgKeyDown;

  public event _DKGAXEvents_OnKgKeyUpEventHandler OnKgKeyUp;

  public event _DKGAXEvents_OnKgKeyPressEventHandler OnKgKeyPress;

  public virtual int GetDocumentID(object index)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.GetDocumentID(index) : throw new AxHost.InvalidActiveXStateException(nameof (GetDocumentID), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual KompasObject GetKompasObject()
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.GetKompasObject() : throw new AxHost.InvalidActiveXStateException(nameof (GetKompasObject), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual KDocumentType GetDocumentType(object index)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.GetDocumentType(index) : throw new AxHost.InvalidActiveXStateException(nameof (GetDocumentType), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual object GetDocumentInterface(object index, int newAPI)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (GetDocumentInterface), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    return this.ocx.GetDocumentInterface(index, newAPI);
  }

  public virtual int GetActiveDocumentID()
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.GetActiveDocumentID() : throw new AxHost.InvalidActiveXStateException(nameof (GetActiveDocumentID), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual int GetDocumentsCount()
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.GetDocumentsCount() : throw new AxHost.InvalidActiveXStateException(nameof (GetDocumentsCount), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual int AddDocument(string fileName)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.AddDocument(fileName) : throw new AxHost.InvalidActiveXStateException(nameof (AddDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual int AddNewDocument(KDocumentType type)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.AddNewDocument(type) : throw new AxHost.InvalidActiveXStateException(nameof (AddNewDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual int InsertDocument(string fileName, object index)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (InsertDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    return this.ocx.InsertDocument(fileName, index);
  }

  public virtual int InsertNewDocument(KDocumentType type, object index)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (InsertNewDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    return this.ocx.InsertNewDocument(type, index);
  }

  public virtual bool RemoveDocument(object index)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.RemoveDocument(index) : throw new AxHost.InvalidActiveXStateException(nameof (RemoveDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual bool ActivateDocument(object index)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.ActivateDocument(index) : throw new AxHost.InvalidActiveXStateException(nameof (ActivateDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual int CloseAll()
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.CloseAll() : throw new AxHost.InvalidActiveXStateException(nameof (CloseAll), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual int TestLoadDocument(string fileName)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.TestLoadDocument(fileName) : throw new AxHost.InvalidActiveXStateException(nameof (TestLoadDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual bool InvalidateActiveDocument(bool erase)
  {
    // ISSUE: reference to a compiler-generated method
    return this.ocx != null ? this.ocx.InvalidateActiveDocument(erase) : throw new AxHost.InvalidActiveXStateException(nameof (InvalidateActiveDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
  }

  public virtual void ZoomEntireDocument()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (ZoomEntireDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.ZoomEntireDocument();
  }

  public virtual void MoveViewDocument()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (MoveViewDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.MoveViewDocument();
  }

  public virtual void PanoramaViewDocument()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (PanoramaViewDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.PanoramaViewDocument();
  }

  public virtual void RotateViewDocument()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (RotateViewDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.RotateViewDocument();
  }

  public virtual void OrientationDocument()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (OrientationDocument), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.OrientationDocument();
  }

  public virtual void ZoomWindow()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (ZoomWindow), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.ZoomWindow();
  }

  public virtual void ZoomWindow(KZoomType type)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (ZoomWindow), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.ZoomWindow(type);
  }

  public virtual void StopCurrentProcess()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (StopCurrentProcess), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.StopCurrentProcess();
  }

  public virtual void StopCurrentProcess(bool cancel)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (StopCurrentProcess), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.StopCurrentProcess(cancel);
  }

  public virtual bool DrawToDC(int dc, int left, int top, int width, int height)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (DrawToDC), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    return this.ocx.DrawToDC(dc, left, top, width, height);
  }

  public virtual void SetCurrentLibManager(int t)
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (SetCurrentLibManager), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.SetCurrentLibManager(t);
  }

  public virtual void SetGabaritModifying()
  {
    if (this.ocx == null)
      throw new AxHost.InvalidActiveXStateException(nameof (SetGabaritModifying), AxHost.ActiveXInvokeKind.MethodInvoke);
    // ISSUE: reference to a compiler-generated method
    this.ocx.SetGabaritModifying();
  }

  protected override void CreateSink()
  {
    try
    {
      this.eventMulticaster = new AxKGAXEventMulticaster(this);
      this.cookie = new AxHost.ConnectionPointCookie((object) this.ocx, (object) this.eventMulticaster, typeof (_DKGAXEvents));
    }
    catch (Exception ex)
    {
    }
  }

  protected override void DetachSink()
  {
    try
    {
      this.cookie.Disconnect();
    }
    catch (Exception ex)
    {
    }
  }

  protected override void AttachInterfaces()
  {
    try
    {
      this.ocx = (_DKGAX) this.GetOcx();
    }
    catch (Exception ex)
    {
    }
  }

  internal void RaiseOnOnKgMouseDown(object sender, _DKGAXEvents_OnKgMouseDownEvent e)
  {
    if (this.OnKgMouseDown == null)
      return;
    this.OnKgMouseDown(sender, e);
  }

  internal void RaiseOnOnKgMouseUp(object sender, _DKGAXEvents_OnKgMouseUpEvent e)
  {
    if (this.OnKgMouseUp == null)
      return;
    this.OnKgMouseUp(sender, e);
  }

  internal void RaiseOnOnKgMouseDblClick(object sender, _DKGAXEvents_OnKgMouseDblClickEvent e)
  {
    if (this.OnKgMouseDblClick == null)
      return;
    this.OnKgMouseDblClick(sender, e);
  }

  internal void RaiseOnOnKgStopCurrentProcess(object sender, EventArgs e)
  {
    if (this.OnKgStopCurrentProcess == null)
      return;
    this.OnKgStopCurrentProcess(sender, e);
  }

  internal void RaiseOnOnKgCreate(object sender, _DKGAXEvents_OnKgCreateEvent e)
  {
    if (this.OnKgCreate == null)
      return;
    this.OnKgCreate(sender, e);
  }

  internal void RaiseOnOnKgPaint(object sender, _DKGAXEvents_OnKgPaintEvent e)
  {
    if (this.OnKgPaint == null)
      return;
    this.OnKgPaint(sender, e);
  }

  internal void RaiseOnOnKgCreateGLList(object sender, _DKGAXEvents_OnKgCreateGLListEvent e)
  {
    if (this.OnKgCreateGLList == null)
      return;
    this.OnKgCreateGLList(sender, e);
  }

  internal void RaiseOnOnKgAddGabatit(object sender, _DKGAXEvents_OnKgAddGabatitEvent e)
  {
    if (this.OnKgAddGabatit == null)
      return;
    this.OnKgAddGabatit(sender, e);
  }

  internal void RaiseOnOnKgErrorLoadDocument(
    object sender,
    _DKGAXEvents_OnKgErrorLoadDocumentEvent e)
  {
    if (this.OnKgErrorLoadDocument == null)
      return;
    this.OnKgErrorLoadDocument(sender, e);
  }

  internal void RaiseOnOnKgKeyDown(object sender, _DKGAXEvents_OnKgKeyDownEvent e)
  {
    if (this.OnKgKeyDown == null)
      return;
    this.OnKgKeyDown(sender, e);
  }

  internal void RaiseOnOnKgKeyUp(object sender, _DKGAXEvents_OnKgKeyUpEvent e)
  {
    if (this.OnKgKeyUp == null)
      return;
    this.OnKgKeyUp(sender, e);
  }

  internal void RaiseOnOnKgKeyPress(object sender, _DKGAXEvents_OnKgKeyPressEvent e)
  {
    if (this.OnKgKeyPress == null)
      return;
    this.OnKgKeyPress(sender, e);
  }
}
