
// Type: Intermech.Html.NativeMethods
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Intermech.Html;

internal class NativeMethods
{
  public const int DOCHOSTUIDBLCLICK_DEFAULT = 0;
  public const int DOCHOSTUIDBLCLICK_SHOWCODE = 2;
  public const int DOCHOSTUIDBLCLICK_SHOWPROPERTIES = 1;
  public const int DOCHOSTUIFLAG_ACTIVATE_CLIENTHIT_ONLY = 512 /*0x0200*/;
  public const int DOCHOSTUIFLAG_DIALOG = 1;
  public const int DOCHOSTUIFLAG_DISABLE_COOKIE = 1024 /*0x0400*/;
  public const int DOCHOSTUIFLAG_DISABLE_HELP_MENU = 2;
  public const int DOCHOSTUIFLAG_DISABLE_OFFSCREEN = 64 /*0x40*/;
  public const int DOCHOSTUIFLAG_DISABLE_SCRIPT_INACTIVE = 16 /*0x10*/;
  public const int DOCHOSTUIFLAG_DIV_BLOCKDEFAULT = 256 /*0x0100*/;
  public const int DOCHOSTUIFLAG_ENABLE_INPLACE_NAVIGATION = 65536 /*0x010000*/;
  public const int DOCHOSTUIFLAG_FLAT_SCROLLBAR = 128 /*0x80*/;
  public const int DOCHOSTUIFLAG_NO3DBORDER = 4;
  public const int DOCHOSTUIFLAG_OPENNEWWIN = 32 /*0x20*/;
  public const int DOCHOSTUIFLAG_SCROLL_NO = 8;
  public const int E_INVALIDARG = -2147024809;
  public const int E_NOINTERFACE = -2147467262 /*0x80004002*/;
  public const int E_NOTIMPL = -134234113;
  public const int OLECLOSE_NOSAVE = 1;
  public const int OLEIVERB_DISCARDUNDOSTATE = -6;
  public const int OLEIVERB_HIDE = -3;
  public const int OLEIVERB_INPLACEACTIVATE = -5;
  public const int OLEIVERB_PRIMARY = 0;
  public const int OLEIVERB_PROPERTIES = -7;
  public const int OLEIVERB_SHOW = -1;
  public const int OLEIVERB_UIACTIVATE = -4;
  public const int S_FALSE = 1;
  public const int S_OK = 0;

  private NativeMethods()
  {
  }

  internal enum CommandExecuteOptions
  {
    DoDefault,
    PromptUser,
    DontPromptUser,
    ShowHelp,
  }

  internal enum CommandName
  {
    Open = 1,
    New = 2,
    Save = 3,
    SaveAs = 4,
    SaveCopyAs = 5,
    Print = 6,
    PrintPreview = 7,
    PageSetup = 8,
    Spell = 9,
    Properties = 10, // 0x0000000A
    Cut = 11, // 0x0000000B
    Copy = 12, // 0x0000000C
    Paste = 13, // 0x0000000D
    PasteSpecial = 14, // 0x0000000E
    Undo = 15, // 0x0000000F
    Redo = 16, // 0x00000010
    SelectAll = 17, // 0x00000011
    ClearSelection = 18, // 0x00000012
    Zoom = 19, // 0x00000013
    GetZoomRange = 20, // 0x00000014
    UpdateCommands = 21, // 0x00000015
    Refresh = 22, // 0x00000016
    Stop = 23, // 0x00000017
  }

  internal enum CommandStatus
  {
    Supported = 1,
    Enabled = 2,
    Latched = 4,
  }

  [ComVisible(true)]
  [StructLayout(LayoutKind.Sequential)]
  internal class DocHostUserInterfaceInfo
  {
    [MarshalAs(UnmanagedType.U4)]
    public int Size;
    [MarshalAs(UnmanagedType.I4)]
    public int Flags;
    [MarshalAs(UnmanagedType.I4)]
    public int DoubleClick;
    [MarshalAs(UnmanagedType.I4)]
    public int Reserved1;
    [MarshalAs(UnmanagedType.I4)]
    public int Reserved2;

    public DocHostUserInterfaceInfo()
    {
      this.Size = 0;
      this.Flags = 0;
      this.DoubleClick = 0;
      this.Reserved1 = 0;
      this.Reserved2 = 0;
    }
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
  [ComImport]
  internal interface IConnectionPoint
  {
    void GetConnectionInterface(out Guid interfaceIdentifier);

    void GetConnectionPointContainer(
      out NativeMethods.IConnectionPointContainer container);

    void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int cookie);

    void Unadvise(int cookie);

    void EnumConnections(out object enumerator);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
  [ComImport]
  internal interface IConnectionPointContainer
  {
    void EnumConnectionPoints(out object enumerator);

    void FindConnectionPoint([In] ref Guid riid, out NativeMethods.IConnectionPoint connectionPoint);
  }

  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A")]
  [ComImport]
  internal interface IDocHostUIHandler
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ShowContextMenu(
      [MarshalAs(UnmanagedType.U4), In] int id,
      [In] NativeMethods.Point point,
      [MarshalAs(UnmanagedType.Interface), In] object pcmdtReserved,
      [MarshalAs(UnmanagedType.Interface), In] object pdispReserved);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetHostInfo([In, Out] NativeMethods.DocHostUserInterfaceInfo info);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ShowUI(
      [MarshalAs(UnmanagedType.I4), In] int dwID,
      [MarshalAs(UnmanagedType.Interface), In] NativeMethods.IOleInPlaceActiveObject activeObject,
      [MarshalAs(UnmanagedType.Interface), In] object commandTarget,
      [MarshalAs(UnmanagedType.Interface), In] NativeMethods.IOleInPlaceFrame frame,
      [MarshalAs(UnmanagedType.Interface), In] object doc);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int HideUI();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UpdateUI();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnableModeless([MarshalAs(UnmanagedType.Bool), In] bool enable);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnDocWindowActivate([MarshalAs(UnmanagedType.Bool), In] bool activate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnFrameWindowActivate([MarshalAs(UnmanagedType.Bool), In] bool activate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ResizeBorder([In] NativeMethods.Rectangle rectangle, [In] object doc, [In] bool frameWindow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int TranslateAccelerator([In] NativeMethods.Message message, [In] ref Guid group, [MarshalAs(UnmanagedType.I4), In] int commandName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetOptionKeyPath([MarshalAs(UnmanagedType.LPArray), Out] string[] pbstrKey, [MarshalAs(UnmanagedType.U4), In] int dw);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDropTarget([MarshalAs(UnmanagedType.Interface), In] object pDropTarget, [MarshalAs(UnmanagedType.Interface)] out object ppDropTarget);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetExternal([MarshalAs(UnmanagedType.Interface)] out object ppDispatch);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int TranslateUrl([MarshalAs(UnmanagedType.U4), In] int dwTranslate, [MarshalAs(UnmanagedType.LPWStr), In] string strURLIn, [MarshalAs(UnmanagedType.LPWStr)] out string pstrURLOut);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FilterDataObject([MarshalAs(UnmanagedType.Interface), In] object dataObject, [MarshalAs(UnmanagedType.Interface)] out object filteredDataObject);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
  internal interface IHtmlDocument2
  {
    [return: MarshalAs(UnmanagedType.Interface)]
    object GetScript();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetAll();

    [return: MarshalAs(UnmanagedType.Interface)]
    NativeMethods.IHtmlElement GetBody();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetActiveElement();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetImages();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetApplets();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetLinks();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetForms();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetAnchors();

    void SetTitle([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetTitle();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetScripts();

    void SetDesignMode([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetDesignMode();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetSelection();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetReadyState();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetFrames();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetEmbeds();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetPlugins();

    void SetAlinkColor([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetAlinkColor();

    void SetBackColor([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetBackColor();

    void SetForeColor([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetForeColor();

    void SetLinkColor([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetLinkColor();

    void SetVlinkColor([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetVlinkColor();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetReferrer();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetLocation();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetLastModified();

    void SetUrl([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetUrl();

    void SetDomain([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetDomain();

    void SetCookie([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetCookie();

    void SetExpando([MarshalAs(UnmanagedType.Bool), In] bool p);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool GetExpando();

    void SetCharset([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetCharset();

    void SetDefaultCharset([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetDefaultCharset();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetMimeType();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetFileSize();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetFileCreatedDate();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetFileModifiedDate();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetFileUpdatedDate();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetSecurity();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetProtocol();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetNameProp();

    void DummyWrite([MarshalAs(UnmanagedType.I4), In] int psarray);

    void DummyWriteln([MarshalAs(UnmanagedType.I4), In] int psarray);

    [return: MarshalAs(UnmanagedType.Interface)]
    object Open([MarshalAs(UnmanagedType.BStr), In] string url, [MarshalAs(UnmanagedType.Struct), In] object name, [MarshalAs(UnmanagedType.Struct), In] object features, [MarshalAs(UnmanagedType.Struct), In] object replace);

    void Close();

    void Clear();

    [return: MarshalAs(UnmanagedType.Bool)]
    bool QueryCommandSupported([MarshalAs(UnmanagedType.BStr), In] string cmdID);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool QueryCommandEnabled([MarshalAs(UnmanagedType.BStr), In] string cmdID);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool QueryCommandState([MarshalAs(UnmanagedType.BStr), In] string cmdID);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool QueryCommandIndeterm([MarshalAs(UnmanagedType.BStr), In] string cmdID);

    [return: MarshalAs(UnmanagedType.BStr)]
    string QueryCommandText([MarshalAs(UnmanagedType.BStr), In] string cmdID);

    [return: MarshalAs(UnmanagedType.Struct)]
    object QueryCommandValue([MarshalAs(UnmanagedType.BStr), In] string cmdID);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool ExecCommand([MarshalAs(UnmanagedType.BStr), In] string cmdID, [MarshalAs(UnmanagedType.Bool), In] bool showUI, [MarshalAs(UnmanagedType.Struct), In] object value);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool ExecCommandShowHelp([MarshalAs(UnmanagedType.BStr), In] string cmdID);

    [return: MarshalAs(UnmanagedType.Interface)]
    object CreateElement([MarshalAs(UnmanagedType.BStr), In] string eTag);

    void SetOnhelp([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnhelp();

    void SetOnclick([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnclick();

    void SetOndblclick([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOndblclick();

    void SetOnkeyup([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnkeyup();

    void SetOnkeydown([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnkeydown();

    void SetOnkeypress([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnkeypress();

    void SetOnmouseup([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmouseup();

    void SetOnmousedown([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmousedown();

    void SetOnmousemove([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmousemove();

    void SetOnmouseout([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmouseout();

    void SetOnmouseover([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmouseover();

    void SetOnreadystatechange([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnreadystatechange();

    void SetOnafterupdate([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnafterupdate();

    void SetOnrowexit([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnrowexit();

    void SetOnrowenter([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnrowenter();

    void SetOndragstart([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOndragstart();

    void SetOnselectstart([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnselectstart();

    [return: MarshalAs(UnmanagedType.Interface)]
    object ElementFromPoint([MarshalAs(UnmanagedType.I4), In] int x, [MarshalAs(UnmanagedType.I4), In] int y);

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetParentWindow();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetStyleSheets();

    void SetOnBeforeUpdate([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnBeforeUpdate();

    void SetOnErrorUpdate([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnErrorUpdate();

    [return: MarshalAs(UnmanagedType.BStr)]
    string ToString();

    [return: MarshalAs(UnmanagedType.Interface)]
    object CreateStyleSheet([MarshalAs(UnmanagedType.BStr), In] string bstrHref, [MarshalAs(UnmanagedType.I4), In] int lIndex);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
  [ComVisible(true)]
  internal interface IHtmlElement
  {
    void SetAttribute([MarshalAs(UnmanagedType.BStr), In] string attributeName, [MarshalAs(UnmanagedType.Struct), In] object attributeValue, [MarshalAs(UnmanagedType.I4), In] int flags);

    void GetAttribute([MarshalAs(UnmanagedType.BStr), In] string attributeName, [MarshalAs(UnmanagedType.I4), In] int flags, [MarshalAs(UnmanagedType.LPArray), Out] object[] values);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool RemoveAttribute([MarshalAs(UnmanagedType.BStr), In] string attributeName, [MarshalAs(UnmanagedType.I4), In] int flags);

    void SetClassName([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetClassName();

    void SetId([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetId();

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetTagName();

    [return: MarshalAs(UnmanagedType.Interface)]
    NativeMethods.IHtmlElement GetParentElement();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetStyle();

    void SetOnHelp([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnHelp();

    void SetOnClick([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnClick();

    void SetOnDoubleClick([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnDoubleClick();

    void SetOnKeyDown([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnKeyDown();

    void SetOnKeyUp([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnKeyUp();

    void SetOnKeyPress([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnKeyPress();

    void SetOnMouseOut([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnMouseOut();

    void SetOnmouseOver([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnMouseOver();

    void SetOnmousemove([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmousemove();

    void SetOnmousedown([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmousedown();

    void SetOnmouseup([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnmouseup();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetDocument();

    void SetTitle([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetTitle();

    void SetLanguage([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetLanguage();

    void SetOnselectstart([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnselectstart();

    void ScrollIntoView([MarshalAs(UnmanagedType.Struct), In] object start);

    [return: MarshalAs(UnmanagedType.Bool)]
    bool Contains([MarshalAs(UnmanagedType.Interface), In] NativeMethods.IHtmlElement pChild);

    [return: MarshalAs(UnmanagedType.I4)]
    int GetSourceIndex();

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetRecordNumber();

    void SetLang([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetLang();

    [return: MarshalAs(UnmanagedType.I4)]
    int GetOffsetLeft();

    [return: MarshalAs(UnmanagedType.I4)]
    int GetOffsetTop();

    [return: MarshalAs(UnmanagedType.I4)]
    int GetOffsetWidth();

    [return: MarshalAs(UnmanagedType.I4)]
    int GetOffsetHeight();

    [return: MarshalAs(UnmanagedType.Interface)]
    NativeMethods.IHtmlElement GetOffsetParent();

    void SetInnerHtml([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetInnerHtml();

    void SetInnerText([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetInnerText();

    void SetOuterHtml([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetOuterHtml();

    void SetOuterText([MarshalAs(UnmanagedType.BStr), In] string pointer);

    [return: MarshalAs(UnmanagedType.BStr)]
    string GetOuterText();

    void InsertAdjacentHtml([MarshalAs(UnmanagedType.BStr), In] string whereText, [MarshalAs(UnmanagedType.BStr), In] string html);

    void InsertAdjacentText([MarshalAs(UnmanagedType.BStr), In] string whereText, [MarshalAs(UnmanagedType.BStr), In] string text);

    [return: MarshalAs(UnmanagedType.Interface)]
    NativeMethods.IHtmlElement GetParentTextEdit();

    [return: MarshalAs(UnmanagedType.Bool)]
    bool GetIsTextEdit();

    void Click();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetFilters();

    void SetOnDragStart([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnDragStart();

    [return: MarshalAs(UnmanagedType.BStr)]
    string ToString();

    void SetOnBeforeUpdate([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnBeforeUpdate();

    void SetOnAfterUpdate([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnAfterUpdate();

    void SetOnErrorUpdate([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnErrorUpdate();

    void SetOnRowExit([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnRowExit();

    void SetOnRowEnter([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnRowEnter();

    void SetOnDataSetChanged([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnDataSetChanged();

    void SetOndataavailable([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnDataAvailable();

    void SetOnDataSetComplete([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnDatasetComplete();

    void SetOnFilterChange([MarshalAs(UnmanagedType.Struct), In] object pointer);

    [return: MarshalAs(UnmanagedType.Struct)]
    object GetOnFilterChange();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetChildren();

    [return: MarshalAs(UnmanagedType.Interface)]
    object GetAll();
  }

  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000118-0000-0000-C000-000000000046")]
  internal interface IOleClientSite
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int SaveObject();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetMoniker([MarshalAs(UnmanagedType.U4), In] int dwAssign, [MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object ppmk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetContainer(out object container);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int ShowObject();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int OnShowWindow([MarshalAs(UnmanagedType.I4), In] int fShow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int RequestNewObjectLayout();
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [Guid("00000117-0000-0000-C000-000000000046")]
  [ComImport]
  internal interface IOleInPlaceActiveObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetWindow(out IntPtr windowHandle);

    int ContextSensitiveHelp([MarshalAs(UnmanagedType.I4), In] int enterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int TranslateAccelerator([MarshalAs(UnmanagedType.LPStruct), In] NativeMethods.Message message);

    void OnFrameWindowActivate([MarshalAs(UnmanagedType.I4), In] int activate);

    void OnDocWindowActivate([MarshalAs(UnmanagedType.I4), In] int activate);

    void ResizeBorder([In] NativeMethods.Rectangle prcBorder, [In] object pUIWindow, [MarshalAs(UnmanagedType.I4), In] int fFrameWindow);

    void EnableModeless([MarshalAs(UnmanagedType.I4), In] int enable);
  }

  [Guid("00000116-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleInPlaceFrame
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetWindow(out IntPtr windowHandle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ContextSensitiveHelp(int enterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetBorder([Out] NativeMethods.Rectangle lprectBorder);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RequestBorderSpace([In] NativeMethods.Rectangle pborderwidths);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetBorderSpace([In] NativeMethods.Rectangle pborderwidths);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetActiveObject(
      [MarshalAs(UnmanagedType.Interface), In] NativeMethods.IOleInPlaceActiveObject pActiveObject,
      [MarshalAs(UnmanagedType.LPWStr), In] string pszObjName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int InsertMenus([In] IntPtr hmenuShared, [In, Out] object lpMenuWidths);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RemoveMenus([In] IntPtr hmenuShared);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetStatusText([MarshalAs(UnmanagedType.BStr), In] string pszStatusText);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnableModeless(bool fEnable);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int TranslateAccelerator([In] ref NativeMethods.Message lpmsg, [MarshalAs(UnmanagedType.U2), In] short wID);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000113-0000-0000-C000-000000000046")]
  [ComImport]
  internal interface IOleInPlaceObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindow(out IntPtr hwnd);

    void ContextSensitiveHelp(int enterMode);

    void InPlaceDeactivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int UIDeactivate();

    void SetObjectRects([In] NativeMethods.Rectangle position, [In] NativeMethods.Rectangle clip);

    void ReactivateAndUndo();
  }

  [Guid("00000119-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IOleInPlaceSite
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetWindow(out IntPtr windowHandle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ContextSensitiveHelp(int fEnterMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CanInPlaceActivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnInPlaceActivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnUIActivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindowContext(
      [MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleInPlaceFrame frame,
      [MarshalAs(UnmanagedType.Interface)] out object doc,
      [Out] NativeMethods.Rectangle lprcPosRect,
      [Out] NativeMethods.Rectangle lprcClipRect,
      [In, Out] NativeMethods.OleInPlaceFrameInfo frameInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Scroll([MarshalAs(UnmanagedType.U4), In] object scrollExtant);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnUIDeactivate(int fUndoable);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnInPlaceDeactivate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DiscardUndoState();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DeactivateAndUndo();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnPosRectChange([In] NativeMethods.Rectangle lprcPosRect);
  }

  [ComVisible(true)]
  [Guid("00000112-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IOleObject
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int SetClientSite([MarshalAs(UnmanagedType.Interface), In] NativeMethods.IOleClientSite pClientSite);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetClientSite(out NativeMethods.IOleClientSite site);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int SetHostNames([MarshalAs(UnmanagedType.LPWStr), In] string szContainerApp, [MarshalAs(UnmanagedType.LPWStr), In] string szContainerObj);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int Close([MarshalAs(UnmanagedType.I4), In] int dwSaveOption);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int SetMoniker([MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface), In] object pmk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetMoniker([MarshalAs(UnmanagedType.U4), In] int dwAssign, [MarshalAs(UnmanagedType.U4), In] int dwWhichMoniker, out object moniker);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int InitFromData([MarshalAs(UnmanagedType.Interface), In] object dDataObject, [MarshalAs(UnmanagedType.I4), In] int fCreation, [MarshalAs(UnmanagedType.U4), In] int dwReserved);

    int GetClipboardData([MarshalAs(UnmanagedType.U4), In] int dwReserved, out object data);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int DoVerb(
      [MarshalAs(UnmanagedType.I4), In] int iVerb,
      [In] IntPtr lpmsg,
      [MarshalAs(UnmanagedType.Interface), In] NativeMethods.IOleClientSite pActiveSite,
      [MarshalAs(UnmanagedType.I4), In] int lindex,
      [In] IntPtr hwndParent,
      [In] NativeMethods.Rectangle lprcPosRect);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int EnumVerbs(out object e);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int OleUpdate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int IsUpToDate();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetUserClassID(out Guid pClsid);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetUserType([MarshalAs(UnmanagedType.U4), In] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int SetExtent([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, [In] object pSizel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetExtent([MarshalAs(UnmanagedType.U4), In] int dwDrawAspect, [Out] object pSizel);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int Advise([MarshalAs(UnmanagedType.Interface), In] object pAdvSink, out int cookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int Unadvise([MarshalAs(UnmanagedType.U4), In] int dwConnection);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int EnumAdvise(out object e);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int GetMiscStatus([MarshalAs(UnmanagedType.U4), In] int dwAspect, out int misc);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.I4)]
    int SetColorScheme([In] object pLogpal);
  }

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000114-0000-0000-C000-000000000046")]
  [ComImport]
  public interface IOleWindow
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindow(out IntPtr hwnd);

    int ContextSensitiveHelp(int fEnterMode);
  }

  [Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
  [TypeLibType(TypeLibTypeFlags.FHidden | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FOleAutomation)]
  [ComImport]
  public interface IWebBrowser2
  {
    [DispId(100)]
    void GoBack();

    [DispId(101)]
    void GoForward();

    [DispId(102)]
    void GoHome();

    [DispId(103)]
    void GoSearch();

    [DispId(104)]
    void Navigate(
      [In] string Url,
      [In] ref object flags,
      [In] ref object targetFrameName,
      [In] ref object postData,
      [In] ref object headers);

    [DispId(-550)]
    void Refresh();

    [DispId(105)]
    void Refresh2([In] ref object level);

    [DispId(106)]
    void Stop();

    [DispId(300)]
    void Quit();

    [DispId(301)]
    void ClientToWindow(out int pcx, out int pcy);

    [DispId(302)]
    void PutProperty([In] string property, [In] object vtValue);

    [DispId(303)]
    object GetProperty([In] string property);

    [DispId(500)]
    void Navigate2(
      [In] ref object URL,
      [In] ref object flags,
      [In] ref object targetFrameName,
      [In] ref object postData,
      [In] ref object headers);

    [DispId(501)]
    NativeMethods.CommandStatus QueryStatus(NativeMethods.CommandName commandName);

    [DispId(502)]
    void Execute(
      NativeMethods.CommandName commandName,
      NativeMethods.CommandExecuteOptions options,
      ref object arguments,
      ref object results);

    [DispId(503)]
    void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);

    [DispId(200)]
    object Application { get; }

    [DispId(201)]
    object Parent { get; }

    [DispId(202)]
    object Container { get; }

    [DispId(203)]
    NativeMethods.IHtmlDocument2 Document { get; }

    [DispId(204)]
    bool TopLevelContainer { get; }

    [DispId(205)]
    string Type { get; }

    [DispId(206)]
    int Left { get; set; }

    [DispId(207)]
    int Top { get; set; }

    [DispId(208 /*0xD0*/)]
    int Width { get; set; }

    [DispId(209)]
    int Height { get; set; }

    [DispId(210)]
    string LocationName { get; }

    [DispId(211)]
    string LocationURL { get; }

    [DispId(212)]
    bool Busy { get; }

    [DispId(0)]
    string Name { get; }

    [DispId(-515)]
    int HWND { get; }

    [DispId(400)]
    string FullName { get; }

    [DispId(401)]
    string Path { get; }

    [DispId(402)]
    bool Visible { get; set; }

    [DispId(403)]
    bool StatusBar { get; set; }

    [DispId(404)]
    string StatusText { get; set; }

    [DispId(405)]
    int ToolBar { get; set; }

    [DispId(406)]
    bool MenuBar { get; set; }

    [DispId(407)]
    bool FullScreen { get; set; }

    [DispId(-525)]
    object ReadyState { get; }

    [DispId(550)]
    bool Offline { get; set; }

    [DispId(551)]
    bool Silent { get; set; }

    [DispId(552)]
    bool RegisterAsBrowser { get; set; }

    [DispId(553)]
    bool RegisterAsDropTarget { get; set; }

    [DispId(554)]
    bool TheaterMode { get; set; }

    [DispId(555)]
    bool AddressBar { get; set; }

    [DispId(556)]
    bool Resizable { get; set; }
  }

  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential)]
  internal class Message
  {
    public IntPtr WindowHandle;
    public int Code;
    public IntPtr WParam;
    public IntPtr LParam;
    public int Time;
    public int X;
    public int Y;

    public Message()
    {
      this.WindowHandle = IntPtr.Zero;
      this.Code = 0;
      this.WParam = IntPtr.Zero;
      this.LParam = IntPtr.Zero;
      this.Time = 0;
      this.X = 0;
      this.Y = 0;
    }
  }

  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential)]
  internal sealed class OleInPlaceFrameInfo
  {
    [MarshalAs(UnmanagedType.U4)]
    public int Size;
    [MarshalAs(UnmanagedType.I4)]
    public int IsMdiApplication;
    public IntPtr FrameWindowHandle;
    public IntPtr AcceleratorHandle;
    [MarshalAs(UnmanagedType.U4)]
    public int AcceleratorCount;

    public OleInPlaceFrameInfo()
    {
      this.Size = 0;
      this.IsMdiApplication = 0;
      this.FrameWindowHandle = IntPtr.Zero;
      this.AcceleratorHandle = IntPtr.Zero;
      this.AcceleratorCount = 0;
    }
  }

  [ComVisible(true)]
  [StructLayout(LayoutKind.Sequential)]
  internal class Point
  {
    public int X;
    public int Y;

    public Point()
    {
      this.X = 0;
      this.Y = 0;
    }
  }

  [ComVisible(true)]
  [StructLayout(LayoutKind.Sequential)]
  internal class Rectangle
  {
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;

    public Rectangle()
    {
      this.Left = 0;
      this.Top = 0;
      this.Right = 0;
      this.Bottom = 0;
    }
  }

  [Guid("8856f961-340a-11d0-a96b-00c04fd705a2")]
  [ComVisible(true)]
  [ComImport]
  internal class WebBrowser
  {
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern WebBrowser();
  }
}
