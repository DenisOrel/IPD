
// Type: AxKGAXLib.AxKGAXEventMulticaster
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using KGAXLib;
using System;
using System.Runtime.InteropServices;


namespace AxKGAXLib;

[ClassInterface(ClassInterfaceType.None)]
public class AxKGAXEventMulticaster : _DKGAXEvents
{
  private AxKGAX parent;

  public AxKGAXEventMulticaster(AxKGAX parent) => this.parent = parent;

  public virtual void OnKgMouseDown(
    short nButton,
    short nShiftState,
    int x,
    int y,
    out bool proceed)
  {
    _DKGAXEvents_OnKgMouseDownEvent e = new _DKGAXEvents_OnKgMouseDownEvent(nButton, nShiftState, x, y);
    this.parent.RaiseOnOnKgMouseDown((object) this.parent, e);
    proceed = e.proceed;
  }

  public virtual void OnKgMouseUp(
    short nButton,
    short nShiftState,
    int x,
    int y,
    out bool proceed)
  {
    _DKGAXEvents_OnKgMouseUpEvent e = new _DKGAXEvents_OnKgMouseUpEvent(nButton, nShiftState, x, y);
    this.parent.RaiseOnOnKgMouseUp((object) this.parent, e);
    proceed = e.proceed;
  }

  public virtual void OnKgMouseDblClick(
    short nButton,
    short nShiftState,
    int x,
    int y,
    out bool proceed)
  {
    _DKGAXEvents_OnKgMouseDblClickEvent e = new _DKGAXEvents_OnKgMouseDblClickEvent(nButton, nShiftState, x, y);
    this.parent.RaiseOnOnKgMouseDblClick((object) this.parent, e);
    proceed = e.proceed;
  }

  public virtual void OnKgStopCurrentProcess()
  {
    this.parent.RaiseOnOnKgStopCurrentProcess((object) this.parent, new EventArgs());
  }

  public virtual void OnKgCreate(int docID)
  {
    this.parent.RaiseOnOnKgCreate((object) this.parent, new _DKGAXEvents_OnKgCreateEvent(docID));
  }

  public virtual void OnKgPaint(PaintObject paintObj)
  {
    this.parent.RaiseOnOnKgPaint((object) this.parent, new _DKGAXEvents_OnKgPaintEvent(paintObj));
  }

  public virtual void OnKgCreateGLList(GLObject glObj, KDocument3DDrawMode drawMode)
  {
    this.parent.RaiseOnOnKgCreateGLList((object) this.parent, new _DKGAXEvents_OnKgCreateGLListEvent(glObj, drawMode));
  }

  public virtual void OnKgAddGabatit(GabaritObject gabObj)
  {
    this.parent.RaiseOnOnKgAddGabatit((object) this.parent, new _DKGAXEvents_OnKgAddGabatitEvent(gabObj));
  }

  public virtual void OnKgErrorLoadDocument(int docID, string fileName, int errorID)
  {
    this.parent.RaiseOnOnKgErrorLoadDocument((object) this.parent, new _DKGAXEvents_OnKgErrorLoadDocumentEvent(docID, fileName, errorID));
  }

  public virtual void OnKgKeyDown(ref int key, short nShiftState)
  {
    _DKGAXEvents_OnKgKeyDownEvent e = new _DKGAXEvents_OnKgKeyDownEvent(key, nShiftState);
    this.parent.RaiseOnOnKgKeyDown((object) this.parent, e);
    key = e.key;
  }

  public virtual void OnKgKeyUp(ref int key, short nShiftState)
  {
    _DKGAXEvents_OnKgKeyUpEvent e = new _DKGAXEvents_OnKgKeyUpEvent(key, nShiftState);
    this.parent.RaiseOnOnKgKeyUp((object) this.parent, e);
    key = e.key;
  }

  public virtual void OnKgKeyPress(ref int key)
  {
    _DKGAXEvents_OnKgKeyPressEvent e = new _DKGAXEvents_OnKgKeyPressEvent(key);
    this.parent.RaiseOnOnKgKeyPress((object) this.parent, e);
    key = e.key;
  }
}
