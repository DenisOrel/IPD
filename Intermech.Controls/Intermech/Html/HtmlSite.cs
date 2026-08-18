
// Type: Intermech.Html.HtmlSite
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Html;

[ClassInterface(ClassInterfaceType.None)]
internal class HtmlSite : 
  NativeMethods.IDocHostUIHandler,
  NativeMethods.IOleClientSite,
  NativeMethods.IOleInPlaceSite,
  NativeMethods.IOleInPlaceFrame,
  NativeMethods.IOleWindow
{
  protected const int E_NOTIMPL = -2147467263 /*0x80004001*/;
  private NativeMethods.IWebBrowser2 browser;
  private HtmlBrowser host;
  private NativeMethods.IOleInPlaceActiveObject oleInPlaceActiveObject;
  private NativeMethods.IOleInPlaceObject oleInPlaceObject;
  private NativeMethods.IOleObject oleObject;

  public HtmlSite(HtmlBrowser host) => this.host = host;

  public int CanInPlaceActivate() => 0;

  public int ContextSensitiveHelp(int enterMode) => -2147467263 /*0x80004001*/;

  public void CreateBrowser()
  {
    this.browser = (NativeMethods.IWebBrowser2) new NativeMethods.WebBrowser();
    this.oleObject = (NativeMethods.IOleObject) this.browser;
    this.oleObject.SetClientSite((NativeMethods.IOleClientSite) this);
    this.DoVerb(-5);
    this.DoVerb(-1);
    this.oleInPlaceObject = (NativeMethods.IOleInPlaceObject) this.oleObject;
    this.oleInPlaceActiveObject = (NativeMethods.IOleInPlaceActiveObject) this.oleObject;
  }

  public int DeactivateAndUndo() => -2147467263 /*0x80004001*/;

  public void DestroyBrowser()
  {
    if (this.oleObject != null)
    {
      this.oleObject.Close(1);
      this.oleObject.SetClientSite((NativeMethods.IOleClientSite) null);
      Marshal.ReleaseComObject((object) this.oleObject);
      this.oleObject = (NativeMethods.IOleObject) null;
    }
    if (this.oleInPlaceActiveObject != null)
    {
      Marshal.ReleaseComObject((object) this.oleInPlaceActiveObject);
      this.oleInPlaceActiveObject = (NativeMethods.IOleInPlaceActiveObject) null;
    }
    if (this.oleInPlaceObject != null)
    {
      Marshal.ReleaseComObject((object) this.oleInPlaceObject);
      this.oleInPlaceObject = (NativeMethods.IOleInPlaceObject) null;
    }
    if (this.browser == null)
      return;
    Marshal.ReleaseComObject((object) this.browser);
    this.browser = (NativeMethods.IWebBrowser2) null;
  }

  public int DiscardUndoState() => -2147467263 /*0x80004001*/;

  public void DoVerb(int verb)
  {
    NativeMethods.Rectangle lprcPosRect = new NativeMethods.Rectangle();
    System.Drawing.Rectangle bounds1 = this.host.Bounds;
    lprcPosRect.Left = bounds1.Left;
    System.Drawing.Rectangle bounds2 = this.host.Bounds;
    lprcPosRect.Top = bounds2.Top;
    System.Drawing.Rectangle bounds3 = this.host.Bounds;
    lprcPosRect.Bottom = bounds3.Bottom;
    System.Drawing.Rectangle bounds4 = this.host.Bounds;
    lprcPosRect.Right = bounds4.Right;
    this.oleObject.DoVerb(verb, IntPtr.Zero, (NativeMethods.IOleClientSite) this, 0, this.host.Handle, lprcPosRect);
  }

  public int EnableModeless(bool fEnable) => 0;

  public int EnableModeless(int enable) => -2147467263 /*0x80004001*/;

  public int FilterDataObject(object dataObject, out object filteredDataObject)
  {
    filteredDataObject = (object) null;
    return -2147467263 /*0x80004001*/;
  }

  public int GetBorder(NativeMethods.Rectangle border) => -2147467263 /*0x80004001*/;

  public int GetContainer(out object container)
  {
    container = (object) null;
    return -2147467262 /*0x80004002*/;
  }

  public int GetDropTarget(object dropSource, out object dropTarget)
  {
    dropTarget = (object) null;
    return -2147467263 /*0x80004001*/;
  }

  public int GetExternal(out object dispatch)
  {
    dispatch = (object) null;
    return -2147467263 /*0x80004001*/;
  }

  public int GetHostInfo(NativeMethods.DocHostUserInterfaceInfo info)
  {
    info.Size = Marshal.SizeOf(typeof (NativeMethods.DocHostUserInterfaceInfo));
    info.DoubleClick = 0;
    info.Flags = 20;
    info.Reserved1 = 0;
    info.Reserved2 = 0;
    return 0;
  }

  public int GetMoniker(int assign, int whichMoniker, out object moniker)
  {
    moniker = (object) null;
    return -2147467263 /*0x80004001*/;
  }

  public int GetOptionKeyPath(string[] path, int flags)
  {
    path[0] = (string) null;
    return 0;
  }

  public int GetWindow(out IntPtr windowHandle)
  {
    windowHandle = this.host.Handle;
    return 0;
  }

  public int GetWindowContext(
    [MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleInPlaceFrame frame,
    [MarshalAs(UnmanagedType.Interface)] out object doc,
    [Out] NativeMethods.Rectangle rect,
    [Out] NativeMethods.Rectangle clipRect,
    [In, Out] NativeMethods.OleInPlaceFrameInfo frameInfo)
  {
    frame = (NativeMethods.IOleInPlaceFrame) this;
    doc = (object) null;
    System.Drawing.Rectangle clientRectangle1 = this.host.ClientRectangle;
    rect.Left = clientRectangle1.Left;
    System.Drawing.Rectangle clientRectangle2 = this.host.ClientRectangle;
    rect.Right = clientRectangle2.Right;
    System.Drawing.Rectangle clientRectangle3 = this.host.ClientRectangle;
    rect.Top = clientRectangle3.Top;
    System.Drawing.Rectangle clientRectangle4 = this.host.ClientRectangle;
    rect.Bottom = clientRectangle4.Bottom;
    clipRect.Left = 0;
    clipRect.Top = 0;
    clipRect.Right = 32000;
    clipRect.Bottom = 32000;
    frameInfo.Size = Marshal.SizeOf(typeof (NativeMethods.OleInPlaceFrameInfo));
    frameInfo.IsMdiApplication = 0;
    frameInfo.FrameWindowHandle = this.host.Handle;
    frameInfo.AcceleratorHandle = IntPtr.Zero;
    frameInfo.AcceleratorCount = 0;
    return 0;
  }

  public int HideUI() => 0;

  public int InsertMenus(IntPtr menuShared, object menuWidths) => -2147467263 /*0x80004001*/;

  public int OnDocWindowActivate(bool activate) => -2147467263 /*0x80004001*/;

  public int OnFrameWindowActivate(bool activate) => -2147467263 /*0x80004001*/;

  public int OnInPlaceActivate()
  {
    System.Drawing.Rectangle clientRectangle = this.host.ClientRectangle;
    this.SetBounds(clientRectangle.Left, clientRectangle.Top, clientRectangle.Right, clientRectangle.Bottom);
    return 0;
  }

  public int OnInPlaceDeactivate() => -2147467263 /*0x80004001*/;

  public int OnPosRectChange(NativeMethods.Rectangle rect)
  {
    this.SetBounds(rect.Left, rect.Top, rect.Right, rect.Bottom);
    return 0;
  }

  public int OnShowWindow(int show) => 0;

  public int OnUIActivate() => 0;

  public int OnUIDeactivate(int undoable) => -2147467263 /*0x80004001*/;

  public int RemoveMenus(IntPtr menu) => -2147467263 /*0x80004001*/;

  public int RequestBorderSpace(NativeMethods.Rectangle borderWidths) => -2147467263 /*0x80004001*/;

  public int RequestNewObjectLayout() => -2147467263 /*0x80004001*/;

  public int ResizeBorder(NativeMethods.Rectangle rect, object doc, bool frameWindow)
  {
    return -2147467263 /*0x80004001*/;
  }

  public int SaveObject() => -2147467263 /*0x80004001*/;

  public int Scroll(object scrollExtant) => -2147467263 /*0x80004001*/;

  public int SetActiveObject(NativeMethods.IOleInPlaceActiveObject activeObject, string name)
  {
    return -2147467263 /*0x80004001*/;
  }

  public int SetBorderSpace(NativeMethods.Rectangle borderWidths) => -2147467263 /*0x80004001*/;

  public void SetBounds(int left, int top, int right, int bottom)
  {
    if (this.oleInPlaceObject == null)
      return;
    this.oleInPlaceObject.UIDeactivate();
    this.oleInPlaceObject.SetObjectRects(new NativeMethods.Rectangle()
    {
      Left = left,
      Top = top,
      Bottom = bottom,
      Right = right
    }, new NativeMethods.Rectangle()
    {
      Left = left,
      Top = top,
      Bottom = bottom,
      Right = right
    });
  }

  public int SetMenu(IntPtr menu, IntPtr holeMenu, IntPtr activeObjectWindowHandle)
  {
    return -2147467263 /*0x80004001*/;
  }

  public int SetStatusText(string text) => 0;

  public int ShowContextMenu(
    int id,
    NativeMethods.Point point,
    object commandTarget,
    object dipatch)
  {
    if (this.host != null && this.host.ContextMenu != null)
      this.host.ContextMenu.Show((Control) this.host, this.host.PointToClient(new System.Drawing.Point(point.X, point.Y)));
    return 0;
  }

  public int ShowObject() => 0;

  public int ShowUI(
    int dwID,
    NativeMethods.IOleInPlaceActiveObject activeObject,
    object commandTarget,
    NativeMethods.IOleInPlaceFrame frame,
    object doc)
  {
    return 0;
  }

  public bool TranslateAccelarator(NativeMethods.Message message)
  {
    return this.oleInPlaceActiveObject != null && this.oleInPlaceActiveObject.TranslateAccelerator(message) != 1;
  }

  public int TranslateAccelerator(ref NativeMethods.Message message, short id) => 0;

  public int TranslateAccelerator(NativeMethods.Message message, ref Guid group, int commandName)
  {
    return 1;
  }

  public int TranslateUrl(int translate, string urlIn, out string urlOut)
  {
    urlOut = (string) null;
    return -2147467263 /*0x80004001*/;
  }

  public int UpdateUI() => 0;

  public NativeMethods.IWebBrowser2 Browser => this.browser;
}
