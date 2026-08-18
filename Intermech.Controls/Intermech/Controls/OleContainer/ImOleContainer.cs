
// Type: Intermech.Controls.OleContainer.ImOleContainer
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using ContainerHelperNET2;
using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Controls.OleContainer;

[DefaultProperty("SourceDocument")]
[DefaultEvent("Loaded")]
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public class ImOleContainer : ScrollableControl, ISupportInitialize
{
  private ActivationGesture activationGesture;
  private ActivationState activationState;
  private ImOleContainer.ActiveDocumentContainer adcontainer;
  private ImOleContainer.ActiveDocumentSite adsite;
  private BorderStyle borderStyle;
  private static object EventActivated;
  private static object EventDeactivated;
  private static object EventDocumentModified;
  private static object EventLoaded;
  private static object EventSaved;
  private static object EventClosed;
  private static object EventStatusTextChanged;
  private const int FlagDisplayAsIcon = 2048 /*0x0800*/;
  private int flags;
  private const int FlagShowMenus = 8;
  private const int FlagShowToolbars = 4;
  private const int FormClosingHooked = 1024 /*0x0400*/;
  private Guid guid;
  private const int GuidSet = 1;
  private const int InInitialize = 512 /*0x0200*/;
  private const int OleInitCalled = 8192 /*0x2000*/;
  private const int OleObjectInit = 16384 /*0x4000*/;
  private const int OwnObject = 4096 /*0x1000*/;
  private UnsafeMethods.IOleObject pOleObject;
  private string progId;
  private const int ProgIdSet = 2;
  private const int ReactivateObject = 32768 /*0x8000*/;
  private DocumentSizeMode sizeMode;
  private const int SourceDataSet = 32 /*0x20*/;
  private string sourceDoc;
  private IDataObject iDataSource;
  private Guid createObjGuid = Guid.Empty;
  private const int SourceDocSet = 16 /*0x10*/;
  private string statusText;
  private static uint cfObjectDescriptor;
  private UnsafeMethods.IStorage storage;
  private ScrollableControl toolTarget;
  private const int ToolTargetDestroyed = 256 /*0x0100*/;
  private static TraceSwitch TraceContainer = new TraceSwitch(nameof (ImOleContainer), "Trace ImOleContainer");

  [SRDescription("ImOleContainerActivated")]
  [SRCategory("CatBehavior")]
  public event EventHandler Activated
  {
    add
    {
      this.Events[ImOleContainer.EventActivated] = Delegate.Combine(this.Events[ImOleContainer.EventActivated], (Delegate) value);
    }
    remove
    {
      this.Events[ImOleContainer.EventActivated] = Delegate.Remove(this.Events[ImOleContainer.EventActivated], (Delegate) value);
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new event EventHandler BackgroundImageChanged
  {
    add => base.BackgroundImageChanged += value;
    remove => base.BackgroundImageChanged -= value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new event EventHandler BackgroundImageLayoutChanged
  {
    add
    {
    }
    remove
    {
    }
  }

  [SRCategory("CatBehavior")]
  [SRDescription("ImOleContainerDeactivated")]
  public event EventHandler Deactivated
  {
    add
    {
      this.Events[ImOleContainer.EventDeactivated] = Delegate.Combine(this.Events[ImOleContainer.EventDeactivated], (Delegate) value);
    }
    remove
    {
      this.Events[ImOleContainer.EventDeactivated] = Delegate.Remove(this.Events[ImOleContainer.EventDeactivated], (Delegate) value);
    }
  }

  [SRCategory("CatBehavior")]
  [SRDescription("ImOleContainerDocumentModified")]
  public event EventHandler DocumentModified
  {
    add
    {
      this.Events[ImOleContainer.EventDocumentModified] = Delegate.Combine(this.Events[ImOleContainer.EventDocumentModified], (Delegate) value);
    }
    remove
    {
      this.Events[ImOleContainer.EventDocumentModified] = Delegate.Remove(this.Events[ImOleContainer.EventDocumentModified], (Delegate) value);
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new event EventHandler FontChanged
  {
    add => base.FontChanged += value;
    remove => base.FontChanged -= value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new event EventHandler ForeColorChanged
  {
    add => base.ForeColorChanged += value;
    remove => base.ForeColorChanged -= value;
  }

  [SRCategory("CatBehavior")]
  [SRDescription("ImOleContainerLoaded")]
  public event EventHandler Loaded
  {
    add
    {
      this.Events[ImOleContainer.EventLoaded] = Delegate.Combine(this.Events[ImOleContainer.EventLoaded], (Delegate) value);
    }
    remove
    {
      this.Events[ImOleContainer.EventLoaded] = Delegate.Remove(this.Events[ImOleContainer.EventLoaded], (Delegate) value);
    }
  }

  [Browsable(false)]
  public new event PaintEventHandler Paint
  {
    add => base.Paint += value;
    remove => base.Paint -= value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new event EventHandler RightToLeftChanged
  {
    add => base.RightToLeftChanged += value;
    remove => base.RightToLeftChanged -= value;
  }

  [SRDescription("ImOleContainerSaved")]
  [SRCategory("CatBehavior")]
  public event EventHandler Saved
  {
    add
    {
      this.Events[ImOleContainer.EventSaved] = Delegate.Combine(this.Events[ImOleContainer.EventSaved], (Delegate) value);
    }
    remove
    {
      this.Events[ImOleContainer.EventSaved] = Delegate.Remove(this.Events[ImOleContainer.EventSaved], (Delegate) value);
    }
  }

  [Description("OleContainer Closed")]
  [SRCategory("CatBehavior")]
  public event EventHandler Closed
  {
    add
    {
      this.Events[ImOleContainer.EventClosed] = Delegate.Combine(this.Events[ImOleContainer.EventClosed], (Delegate) value);
    }
    remove
    {
      this.Events[ImOleContainer.EventClosed] = Delegate.Remove(this.Events[ImOleContainer.EventClosed], (Delegate) value);
    }
  }

  [SRDescription("ImOleContainerStatusTextChanged")]
  [SRCategory("CatBehavior")]
  public event EventHandler StatusTextChanged
  {
    add
    {
      this.Events[ImOleContainer.EventStatusTextChanged] = Delegate.Combine(this.Events[ImOleContainer.EventStatusTextChanged], (Delegate) value);
    }
    remove
    {
      this.Events[ImOleContainer.EventStatusTextChanged] = Delegate.Remove(this.Events[ImOleContainer.EventStatusTextChanged], (Delegate) value);
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Browsable(false)]
  public new event EventHandler TextChanged
  {
    add => base.TextChanged += value;
    remove => base.TextChanged -= value;
  }

  static ImOleContainer()
  {
    ImOleContainer.EventLoaded = new object();
    ImOleContainer.EventActivated = new object();
    ImOleContainer.EventDeactivated = new object();
    ImOleContainer.EventSaved = new object();
    ImOleContainer.EventClosed = new object();
    ImOleContainer.EventStatusTextChanged = new object();
    ImOleContainer.EventDocumentModified = new object();
    ImOleContainer.cfObjectDescriptor = UnsafeMethods.RegisterClipboardFormat("Object Descriptor");
  }

  public ImOleContainer()
  {
    this.borderStyle = BorderStyle.FixedSingle;
    this.activationGesture = ActivationGesture.DoubleClick;
    this.statusText = "";
    this.SetStyle(ControlStyles.ContainerControl | ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.Selectable, false);
    this.SetFlag(12, true);
    this.BackColor = SystemColors.Window;
    this.DragDrop += new DragEventHandler(this.Self_DragDrop);
    this.DragEnter += new DragEventHandler(this.Self_DragEnter);
  }

  public ImOleContainer(Guid guid)
    : this()
  {
    this.Guid = guid;
  }

  public ImOleContainer(string documentClassName)
    : this()
  {
    this.DocumentClassName = documentClassName;
  }

  public void Activate()
  {
    if (this.OleObject == null || !this.IsHandleCreated || this.adsite == null || this.ActivationState != ActivationState.Inactive && this.ActivationState != ActivationState.Active)
      return;
    this.adsite.UIActivate();
  }

  public void CloseDocument() => this.DestroyOleObject();

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  protected virtual ImOleContainer.ActiveDocumentContainer CreateHostClass()
  {
    return new ImOleContainer.ActiveDocumentContainer(this);
  }

  private UnsafeMethods.IOleObject CreateOleObject(string progId)
  {
    if (this.guid == Guid.Empty)
    {
      this.guid = this.GetGuidFromProgId(progId);
      if (this.guid == Guid.Empty)
        throw new ArgumentException(LangStrings.GetString("ImOleContainerBadProgId", (object) progId));
      if (!this.IsValidDocObject(this.guid, false))
        throw new ArgumentException(LangStrings.GetString("ImOleContainerInvalidProgId", (object) progId));
    }
    return this.CreateOleObject(this.guid, (UnsafeMethods.IStorage) null);
  }

  private UnsafeMethods.IOleObject CreateOleObject(Guid g, UnsafeMethods.IStorage storage)
  {
    if (this.OleObject == null)
    {
      Guid guid1 = typeof (UnsafeMethods.IOleObject).GUID;
      int num;
      if (storage == null)
      {
        num = UnsafeMethods.OleCreate(ref g, ref guid1, OLERENDER.OLERENDER_DRAW, (FORMATETC[]) null, (UnsafeMethods.IOleClientSite) null, this.CreateStorage(g), out this.pOleObject);
        HelperMethods.OleCheck(num);
      }
      else
      {
        num = UnsafeMethods.OleLoad(storage, ref guid1, (UnsafeMethods.IOleClientSite) null, out this.pOleObject);
        HelperMethods.OleCheck(num);
      }
      if (this.OleObject == null)
      {
        if (storage != null)
        {
          Guid guid2;
          if (HelperMethods.Succeeded(UnsafeMethods.OleDoAutoConvert(storage, out guid2)))
            g = guid2;
        }
        else
        {
          Guid newClsid;
          if (HelperMethods.Succeeded(UnsafeMethods.OleGetAutoConvert(ref g, out newClsid)))
            g = newClsid;
        }
        if (g != Guid.Empty)
        {
          this.IsValidDocObject(g, true);
          try
          {
            this.pOleObject = (UnsafeMethods.IOleObject) UnsafeMethods.CoCreateInstance(ref g, (object) null, 5, ref guid1);
          }
          catch (ExternalException ex)
          {
            num = ex.ErrorCode;
          }
        }
      }
      if (HelperMethods.Succeeded(num) && this.OleObject != null)
      {
        this.SetFlag(4096 /*0x1000*/, true);
        this.guid = Guid.Empty;
        this.progId = (string) null;
        this.SetFlag(1, false);
        this.SetFlag(2, false);
        HelperMethods.Succeeded(this.OleObject.GetUserClassID(ref this.guid));
      }
      if (this.OleObject == null)
        throw new ExternalException(LangStrings.GetString("ImOleContainerCantCreate"), num);
    }
    return this.OleObject;
  }

  private UnsafeMethods.IOleObject CreateOleObjectFromFile(string fileName)
  {
    if (this.OleObject == null)
    {
      Guid iidIunknown = HelperMethods.ActiveX.IID_IUnknown;
      Guid empty = Guid.Empty;
      if (HelperMethods.Succeeded(UnsafeMethods.OleCreateFromFile(ref empty, fileName, ref iidIunknown, OLERENDER.OLERENDER_DRAW, (FORMATETC[]) null, (UnsafeMethods.IOleClientSite) null, this.CreateStorage(Guid.Empty), out this.pOleObject)) && this.OleObject != null)
      {
        this.guid = Guid.Empty;
        this.progId = (string) null;
        this.sourceDoc = fileName;
        this.SetFlag(35, false);
        this.SetFlag(4096 /*0x1000*/, true);
        HelperMethods.Succeeded(this.OleObject.GetUserClassID(ref this.guid));
      }
    }
    return this.OleObject;
  }

  public void Copy()
  {
    if (this.Document == null || !(new DataObjectHelperClass().CreateDataObjectHelper((object) this.pOleObject) is IDataObject dataObjectHelper))
      return;
    HelperMethods.OleCheck(UnsafeMethods.OleSetClipboard(dataObjectHelper));
  }

  public IntPtr InsertObjProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam) => IntPtr.Zero;

  private void Self_DragDrop(object sender, DragEventArgs e)
  {
    if (!e.Data.GetDataPresent(DataFormats.FileDrop))
      return;
    string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
    try
    {
      this.LoadFrom(data[0]);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  private void Self_DragEnter(object sender, DragEventArgs e)
  {
    if (e.Data.GetDataPresent(DataFormats.FileDrop))
      e.Effect = DragDropEffects.Copy;
    else
      e.Effect = DragDropEffects.None;
  }

  /// <summary>Диалог вставки нового объекта</summary>
  /// <returns>Был ли новый объект вставлен</returns>
  public bool CallInsertDlg()
  {
    ImOleContainer.CreateInfo createinfo = new ImOleContainer.CreateInfo();
    UnsafeMethods.OLEUIINSERTOBJECT lpIO = new UnsafeMethods.OLEUIINSERTOBJECT();
    lpIO.cbStruct = Marshal.SizeOf(typeof (UnsafeMethods.OLEUIINSERTOBJECT));
    int num1 = 2;
    int num2 = 16 /*0x10*/;
    int num3 = 256 /*0x0100*/;
    int num4 = 1024 /*0x0400*/;
    lpIO.dwFlags = num1 | num3 | num4;
    lpIO.hWndOwner = this.Handle;
    lpIO.lpfnHook = new Intermech.Controls.OleContainer.WndProc(this.InsertObjProc);
    StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
    stringBuilder.Append(char.MinValue, (int) byte.MaxValue);
    string str = stringBuilder.ToString();
    lpIO.lpszFile = str;
    lpIO.cchFile = (uint) byte.MaxValue;
    lpIO.lpszCaption = IntPtr.Zero;
    lpIO.lCustData = IntPtr.Zero;
    lpIO.hInstance = IntPtr.Zero;
    lpIO.lpszTemplate = IntPtr.Zero;
    lpIO.hResource = IntPtr.Zero;
    lpIO.clsid_data1 = 0;
    lpIO.clsid_data2 = (short) 0;
    lpIO.clsid_data3 = (short) 0;
    lpIO.clsid_b0 = (byte) 0;
    lpIO.clsid_b1 = (byte) 0;
    lpIO.clsid_b2 = (byte) 0;
    lpIO.clsid_b3 = (byte) 0;
    lpIO.clsid_b4 = (byte) 0;
    lpIO.clsid_b5 = (byte) 0;
    lpIO.clsid_b6 = (byte) 0;
    lpIO.clsid_b7 = (byte) 0;
    lpIO.cClsidExclude = 0U;
    lpIO.lpClsidExclude = IntPtr.Zero;
    lpIO.iid = Guid.Empty;
    lpIO.oleRender = 0;
    lpIO.lpFormatEtc = IntPtr.Zero;
    lpIO.lpIOleClientSite = (object) null;
    lpIO.lpIStorage = (object) null;
    lpIO.ppvObj = IntPtr.Zero;
    lpIO.sc = 0;
    lpIO.hMetaPict = IntPtr.Zero;
    try
    {
      if (UnsafeMethods.OleUIInsertObject(ref lpIO) == 1U)
      {
        if ((lpIO.dwFlags & num1) != 0)
        {
          createinfo.createType = ImOleContainer.CreateType.ctNewObject;
          createinfo.classID = new Guid(lpIO.clsid_data1, lpIO.clsid_data2, lpIO.clsid_data3, lpIO.clsid_b0, lpIO.clsid_b1, lpIO.clsid_b2, lpIO.clsid_b3, lpIO.clsid_b4, lpIO.clsid_b5, lpIO.clsid_b6, lpIO.clsid_b7);
        }
        else
        {
          createinfo.createType = ImOleContainer.CreateType.ctFromFile;
          createinfo.fileName = lpIO.lpszFile;
        }
        createinfo.showAsIcon = (lpIO.dwFlags & num2) != 0;
        createinfo.hMetaPict = lpIO.hMetaPict;
        if (createinfo.createType != ImOleContainer.CreateType.ctFromFile)
          return this.CreateNew(createinfo);
        this.LoadFrom(createinfo.fileName);
        return true;
      }
    }
    finally
    {
      if (createinfo.hMetaPict != IntPtr.Zero)
      {
        UnsafeMethods.DeleteMetaFile(createinfo.hMetaPict);
        UnsafeMethods.GlobalUnlock(createinfo.hMetaPict);
        UnsafeMethods.GlobalFree(createinfo.hMetaPict);
      }
    }
    return false;
  }

  internal bool CreateNew(ImOleContainer.CreateInfo createinfo)
  {
    this.SetFlag(16 /*0x10*/, true);
    this.SetFlag(3, false);
    this.sourceDoc = (string) null;
    this.iDataSource = (IDataObject) null;
    this.createObjGuid = Guid.Empty;
    try
    {
      switch (createinfo.createType)
      {
        case ImOleContainer.CreateType.ctNewObject:
          this.createObjGuid = createinfo.classID;
          break;
        case ImOleContainer.CreateType.ctFromFile:
          this.sourceDoc = createinfo.fileName;
          break;
        default:
          return false;
      }
      if (!this.GetFlag(512 /*0x0200*/))
      {
        this.SetFlag(32 /*0x20*/, false);
        if (!this.IsHandleCreated)
          return false;
        int num = this.ActivationState == ActivationState.Active ? 1 : 0;
        this.DestroyOleObject();
        this.DisplayAsIcon = createinfo.showAsIcon;
        this.InitializeOleObject();
        if (num == 0)
          return true;
        this.Activate();
        return true;
      }
    }
    catch
    {
      this.SetFlag(16 /*0x10*/, false);
      throw;
    }
    finally
    {
      this.sourceDoc = (string) null;
      this.iDataSource = (IDataObject) null;
      this.createObjGuid = Guid.Empty;
    }
    return false;
  }

  /// <summary>Проверка возможности вставки объекта из Clipboard</summary>
  /// <returns></returns>
  public static bool CanPaste() => Clipboard.GetDataObject().GetDataPresent("Embed Source");

  /// <summary>Вставка содержимого Clipboad-а в контейнер</summary>
  public void Paste()
  {
    if (!ImOleContainer.CanPaste())
      return;
    IDataObject data = (IDataObject) null;
    UnsafeMethods.OleGetClipboard(ref data);
    ImOleContainer.CreateInfo createInfo = new ImOleContainer.CreateInfo();
    createInfo.createType = ImOleContainer.CreateType.ctFromData;
    createInfo.showAsIcon = false;
    createInfo.idataObject = data;
    FORMATETC format = new FORMATETC();
    format.cfFormat = (short) -16370;
    format.ptd = IntPtr.Zero;
    format.dwAspect = DVASPECT.DVASPECT_CONTENT;
    format.lindex = -1;
    format.tymed = TYMED.TYMED_HGLOBAL;
    STGMEDIUM stgmedium = new STGMEDIUM();
    data.GetData(ref format, ref stgmedium);
    HandleRef handle = new HandleRef((object) this, stgmedium.unionmember);
    IntPtr ptr = UnsafeMethods.GlobalLock(handle);
    if (ptr != IntPtr.Zero)
    {
      OBJECTDESCRIPTOR structure = new OBJECTDESCRIPTOR();
      Marshal.PtrToStructure<OBJECTDESCRIPTOR>(ptr, structure);
      if (structure.dwDrawAspect == DVASPECT.DVASPECT_ICON)
        createInfo.showAsIcon = true;
      UnsafeMethods.GlobalUnlock(handle);
      UnsafeMethods.ReleaseStgMedium(ref stgmedium);
    }
    if (createInfo.showAsIcon)
    {
      format.cfFormat = (short) 3;
      format.ptd = IntPtr.Zero;
      format.dwAspect = DVASPECT.DVASPECT_ICON;
      format.lindex = -1;
      format.tymed = TYMED.TYMED_MFPICT;
      data.GetData(ref format, ref stgmedium);
      createInfo.hRef = new HandleRef((object) this, stgmedium.unionmember);
    }
    this.LoadFrom(data);
  }

  private UnsafeMethods.IOleObject CreateOleObjectFromData(IDataObject iDataObject)
  {
    if (this.OleObject == null)
    {
      Guid riid = new Guid("{00000112-0000-0000-C000-000000000046}");
      UnsafeMethods.IOleClientSite siteClass = (UnsafeMethods.IOleClientSite) this.CreateSiteClass();
      uint renderopt = 1;
      if (HelperMethods.Succeeded(UnsafeMethods.OleCreateFromData(iDataObject, ref riid, renderopt, (FORMATETC[]) null, siteClass, this.CreateStorage(Guid.Empty), out this.pOleObject)) && this.OleObject != null)
      {
        this.guid = Guid.Empty;
        this.progId = (string) null;
        this.sourceDoc = (string) null;
        this.SetFlag(35, false);
        this.SetFlag(4096 /*0x1000*/, true);
        HelperMethods.Succeeded(this.OleObject.GetUserClassID(ref this.guid));
      }
    }
    return this.OleObject;
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  protected virtual ImOleContainer.ActiveDocumentSite CreateSiteClass()
  {
    return new ImOleContainer.ActiveDocumentSite(this);
  }

  private UnsafeMethods.IStorage CreateStorage(Guid initGuid)
  {
    if (this.storage == null)
    {
      this.storage = UnsafeMethods.StgCreateDocfileOnILockBytes(UnsafeMethods.CreateILockBytesOnHGlobal(new HandleRef((object) this, IntPtr.Zero), true), 4114, 0);
      if (initGuid != Guid.Empty)
        UnsafeMethods.WriteClassStg(this.storage, ref initGuid);
    }
    return this.storage;
  }

  public void Deactivate()
  {
    if (this.OleObject == null || !this.IsHandleCreated || this.adsite == null || this.ActivationState != ActivationState.Active)
      return;
    this.adsite.Deactivate();
  }

  private void DestroyOleObject()
  {
    Cursor current = Cursor.Current;
    try
    {
      Cursor.Current = Cursors.WaitCursor;
      if (this.OleObject != null)
      {
        try
        {
          this.Deactivate();
        }
        finally
        {
          this.statusText = "";
          this.SetFlag(16384 /*0x4000*/, false);
          this.SetFlag(32768 /*0x8000*/, false);
          this.activationState = ActivationState.None;
          if (this.adsite != null)
          {
            this.adsite.Dispose();
            this.adsite = (ImOleContainer.ActiveDocumentSite) null;
          }
          if (this.adcontainer != null)
          {
            this.adcontainer.Dispose();
            this.adcontainer = (ImOleContainer.ActiveDocumentContainer) null;
          }
        }
        if (this.GetFlag(4096 /*0x1000*/))
        {
          try
          {
            Marshal.ReleaseComObject((object) this.OleObject);
          }
          catch
          {
          }
          this.SetFlag(4096 /*0x1000*/, false);
        }
        this.pOleObject = (UnsafeMethods.IOleObject) null;
        if (this.storage != null)
        {
          Marshal.ReleaseComObject((object) this.storage);
          this.storage = (UnsafeMethods.IStorage) null;
        }
      }
      GC.Collect();
    }
    finally
    {
      Cursor.Current = current;
    }
  }

  protected override void Dispose(bool disposing)
  {
    this.DragDrop -= new DragEventHandler(this.Self_DragDrop);
    this.DragEnter -= new DragEventHandler(this.Self_DragEnter);
    if (disposing)
    {
      this.DestroyOleObject();
      if (this.GetFlag(8192 /*0x2000*/))
      {
        UnsafeMethods.OleUninitialize();
        this.SetFlag(8192 /*0x2000*/, false);
      }
    }
    this.ToolTarget = (ScrollableControl) null;
    base.Dispose(disposing);
  }

  private bool GetFlag(int flag) => (this.flags & flag) == flag;

  private Guid GetGuidFromProgId(string progId)
  {
    RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey(progId);
    if (registryKey1 != null)
    {
      try
      {
        RegistryKey registryKey2 = registryKey1.OpenSubKey("CLSID");
        if (registryKey2 != null)
        {
          try
          {
            return new Guid((string) registryKey2.GetValue((string) null));
          }
          finally
          {
            registryKey2.Close();
          }
        }
      }
      finally
      {
        registryKey1.Close();
      }
    }
    return Guid.Empty;
  }

  [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, Name = "System.Windows.Forms", PublicKey = "0x00000000000000000400000000000000")]
  internal Size GetPreferredSizeCore(Size proposedSize)
  {
    if (this.OleObject != null && this.IsHandleCreated && this.adsite != null && this.ActivationState != ActivationState.Active)
    {
      Size preferredSize = this.adsite.PreferredSize;
      if (preferredSize != Size.Empty)
        return preferredSize;
    }
    return Size.Empty;
  }

  private string GetProgIdFromGuid(Guid g)
  {
    RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("CLSID");
    try
    {
      RegistryKey registryKey2 = registryKey1.OpenSubKey($"{{{g.ToString()}}}");
      if (registryKey2 != null)
      {
        try
        {
          RegistryKey registryKey3 = registryKey2.OpenSubKey("ProgID");
          if (registryKey3 != null)
          {
            try
            {
              return (string) registryKey3.GetValue((string) null);
            }
            finally
            {
              registryKey3.Close();
            }
          }
        }
        finally
        {
          registryKey2.Close();
        }
      }
    }
    finally
    {
      registryKey1.Close();
    }
    return (string) null;
  }

  private void InitializeOleObject()
  {
    Cursor current = Cursor.Current;
    try
    {
      Cursor.Current = Cursors.WaitCursor;
      if (this.GetFlag(16384 /*0x4000*/) || this.GetFlag(512 /*0x0200*/) || !this.GetFlag(1) && !this.GetFlag(2) && this.sourceDoc == null && this.storage == null && this.OleObject == null && this.iDataSource == null && this.createObjGuid == Guid.Empty)
        return;
      if (!this.GetFlag(8192 /*0x2000*/))
      {
        UnsafeMethods.OleInitialize();
        this.SetFlag(8192 /*0x2000*/, true);
      }
      if (this.OleObject == null)
      {
        if (!this.GetFlag(32 /*0x20*/) && this.iDataSource != null)
          this.pOleObject = this.CreateOleObjectFromData(this.iDataSource);
        if (!this.GetFlag(32 /*0x20*/) && this.sourceDoc != null && this.sourceDoc.Length > 0)
          this.pOleObject = this.CreateOleObjectFromFile(this.sourceDoc);
        else if (!this.GetFlag(32 /*0x20*/) && this.createObjGuid != Guid.Empty)
          this.pOleObject = this.CreateOleObject(this.createObjGuid, (UnsafeMethods.IStorage) null);
        else if (this.GetFlag(2) && this.progId != null)
          this.pOleObject = this.CreateOleObject(this.progId);
        else if (this.storage != null && this.GetFlag(32 /*0x20*/) || this.GetFlag(1) && this.guid != Guid.Empty)
          this.pOleObject = this.CreateOleObject(this.guid, this.storage);
      }
      if (this.OleObject == null)
        return;
      if (this.adsite == null)
        this.adsite = this.CreateSiteClass();
      if (this.adcontainer == null)
        this.adcontainer = this.CreateHostClass();
      this.OleObject.SetClientSite((UnsafeMethods.IOleClientSite) this.adsite);
      this.SetFlag(16384 /*0x4000*/, true);
      this.activationState = ActivationState.Inactive;
      this.OnLoaded(EventArgs.Empty);
    }
    finally
    {
      Cursor.Current = current;
    }
  }

  private bool IsValidDocObject(Guid g, bool throwIfFalse)
  {
    RegistryKey registryKey1 = Registry.ClassesRoot.OpenSubKey("CLSID");
    try
    {
      RegistryKey registryKey2 = registryKey1.OpenSubKey($"{{{g.ToString()}}}");
      try
      {
        if (registryKey2 != null)
        {
          RegistryKey registryKey3 = registryKey2.OpenSubKey("Insertable");
          RegistryKey registryKey4 = registryKey2.OpenSubKey("NotInsertable");
          RegistryKey registryKey5 = registryKey2.OpenSubKey("Control");
          try
          {
            if (registryKey3 != null)
            {
              if (registryKey4 == null)
              {
                if (registryKey5 == null)
                  return true;
              }
            }
          }
          finally
          {
            registryKey3?.Close();
            registryKey4?.Close();
            registryKey5?.Close();
          }
        }
      }
      finally
      {
        registryKey2?.Close();
      }
    }
    finally
    {
      registryKey1.Close();
    }
    if (throwIfFalse)
    {
      new object[1][0] = (object) g.ToString();
      throw new ArgumentException("Документ данного типа создать нельзя");
    }
    return false;
  }

  public void LoadFrom(IDataObject dataSource)
  {
    this.SetFlag(16 /*0x10*/, true);
    this.SetFlag(3, false);
    this.sourceDoc = (string) null;
    try
    {
      this.iDataSource = dataSource;
      try
      {
        if (this.GetFlag(512 /*0x0200*/))
          return;
        this.SetFlag(32 /*0x20*/, false);
        if (!this.IsHandleCreated)
          return;
        int num = this.ActivationState == ActivationState.Active ? 1 : 0;
        this.DestroyOleObject();
        this.InitializeOleObject();
        if (num == 0)
          return;
        this.Activate();
      }
      catch
      {
        this.SetFlag(16 /*0x10*/, false);
        this.sourceDoc = (string) null;
        this.iDataSource = (IDataObject) null;
        throw;
      }
    }
    finally
    {
      this.sourceDoc = (string) null;
      this.iDataSource = (IDataObject) null;
    }
  }

  public void LoadFrom(string fileName)
  {
    if (!File.Exists(fileName))
      throw new FileNotFoundException(LangStrings.GetString("FileDialogFileNotFound", (object) fileName));
    this.SetFlag(16 /*0x10*/, true);
    this.SetFlag(3, false);
    this.sourceDoc = fileName;
    try
    {
      if (this.GetFlag(512 /*0x0200*/))
        return;
      this.SetFlag(32 /*0x20*/, false);
      if (!this.IsHandleCreated)
        return;
      int num = this.ActivationState == ActivationState.Active ? 1 : 0;
      this.DestroyOleObject();
      this.InitializeOleObject();
      if (num == 0)
        return;
      this.Activate();
    }
    catch
    {
      this.SetFlag(16 /*0x10*/, false);
      this.sourceDoc = (string) null;
      throw;
    }
  }

  protected virtual void OnActivated(EventArgs e)
  {
    if ((object) this.Events[ImOleContainer.EventActivated] == null)
      return;
    ((EventHandler) this.Events[ImOleContainer.EventActivated])((object) this, e);
  }

  protected virtual void OnDeactivated(EventArgs e)
  {
    if ((object) this.Events[ImOleContainer.EventDeactivated] == null)
      return;
    ((EventHandler) this.Events[ImOleContainer.EventDeactivated])((object) this, e);
  }

  protected virtual void OnDocumentModified(EventArgs e)
  {
    if ((object) this.Events[ImOleContainer.EventDocumentModified] == null)
      return;
    ((EventHandler) this.Events[ImOleContainer.EventDocumentModified])((object) this, e);
  }

  private void OnFormClosing(object sender, CancelEventArgs e) => this.Deactivate();

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    if (!this.GetFlag(16384 /*0x4000*/))
      this.InitializeOleObject();
    else if (this.GetFlag(32768 /*0x8000*/))
      Application.Idle += new EventHandler(this.OnIdle);
    if (this.GetFlag(1024 /*0x0400*/))
      return;
    Form form = this.FindForm();
    if (form != null)
      form.Closing += new CancelEventHandler(this.OnFormClosing);
    this.SetFlag(1024 /*0x0400*/, true);
  }

  protected override void OnHandleDestroyed(EventArgs e)
  {
    bool flag = this.ActivationState == ActivationState.Active;
    this.SetFlag(32768 /*0x8000*/, flag);
    if (flag)
      this.Deactivate();
    if (this.GetFlag(1024 /*0x0400*/))
    {
      Form form = this.FindForm();
      if (form != null)
        form.Closing -= new CancelEventHandler(this.OnFormClosing);
      this.SetFlag(1024 /*0x0400*/, false);
    }
    base.OnHandleDestroyed(e);
  }

  private void OnIdle(object sender, EventArgs e)
  {
    Application.Idle -= new EventHandler(this.OnIdle);
    if (!this.GetFlag(32768 /*0x8000*/))
      return;
    this.SetFlag(32768 /*0x8000*/, false);
    this.Activate();
  }

  protected override void OnLeave(EventArgs e)
  {
    this.Deactivate();
    base.OnLeave(e);
  }

  protected virtual void OnLoaded(EventArgs e)
  {
    if ((object) this.Events[ImOleContainer.EventLoaded] == null)
      return;
    ((EventHandler) this.Events[ImOleContainer.EventLoaded])((object) this, e);
  }

  public void PaintOn(Graphics graphics, Rectangle clipRect)
  {
    this.adsite.PaintOn(graphics, clipRect);
  }

  public void PaintOn2(Graphics graphics, Rectangle clipRect)
  {
    this.adsite.PaintOn2(graphics, clipRect);
  }

  public void Draw(Graphics graphics, RectangleF rect)
  {
    PointF pointF = new PointF(graphics.DpiX, graphics.DpiY);
    IntPtr hdc = graphics.GetHdc();
    try
    {
      if (!(this.pOleObject is UnsafeMethods.IViewObject pOleObject))
        return;
      Rectangle r = new Rectangle((int) ((double) rect.X * ((double) pointF.X / 25.4)), (int) ((double) rect.Y * ((double) pointF.Y / 25.4)), (int) ((double) rect.Width * ((double) pointF.X / 25.4)), (int) ((double) rect.Height * ((double) pointF.Y / 25.4)));
      COMRECT comrect1 = new COMRECT(r);
      COMRECT comrect2 = new COMRECT(r);
      int dwDrawAspect = this.DisplayAsIcon ? 4 : 1;
      IntPtr zero1 = IntPtr.Zero;
      IntPtr zero2 = IntPtr.Zero;
      IntPtr hdcDraw = hdc;
      COMRECT lprcBounds = comrect1;
      COMRECT lprcWBounds = comrect2;
      IntPtr zero3 = IntPtr.Zero;
      pOleObject.Draw(dwDrawAspect, -1, zero1, (tagDVTARGETDEVICE) null, zero2, hdcDraw, lprcBounds, lprcWBounds, zero3, 0);
    }
    finally
    {
      graphics.ReleaseHdc(hdc);
    }
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    switch (this.BorderStyle)
    {
      case BorderStyle.FixedSingle:
        ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, this.ForeColor, ButtonBorderStyle.Solid);
        break;
      case BorderStyle.Fixed3D:
        ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle);
        break;
    }
    base.OnPaint(e);
  }

  protected virtual void OnSaved(EventArgs e)
  {
    if ((object) this.Events[ImOleContainer.EventSaved] == null)
      return;
    ((EventHandler) this.Events[ImOleContainer.EventSaved])((object) this, e);
  }

  protected virtual void OnClosed(EventArgs e)
  {
    if ((object) this.Events[ImOleContainer.EventClosed] == null)
      return;
    ((EventHandler) this.Events[ImOleContainer.EventClosed])((object) this, e);
  }

  protected override void OnSizeChanged(EventArgs e)
  {
    if (this.adsite != null)
      this.adsite.UpdateClientRect(true, Rectangle.Empty);
    base.OnSizeChanged(e);
  }

  protected virtual void OnStatusTextChanged(EventArgs e)
  {
    if ((object) this.Events[ImOleContainer.EventStatusTextChanged] == null)
      return;
    ((EventHandler) this.Events[ImOleContainer.EventStatusTextChanged])((object) this, e);
  }

  private void ResetDocumentClassName()
  {
    if (!this.ShouldSerializeDocumentClassName())
      return;
    this.DocumentClassName = (string) null;
  }

  private void ResetGuid()
  {
    if (!this.ShouldSerializeGuid())
      return;
    this.Guid = Guid.Empty;
  }

  private void ResetSourceDocument()
  {
    if (!this.ShouldSerializeSourceDocument())
      return;
    this.SourceDocument = (string) null;
  }

  private void ResetToolTarget() => this.ToolTarget = (ScrollableControl) null;

  private Stream SaveStateToStream(Stream stream)
  {
    if (this.OleObject == null)
      return (Stream) null;
    Cursor current = Cursor.Current;
    try
    {
      Cursor.Current = Cursors.WaitCursor;
      return this.StreamFromIStorage(this.storage, stream);
    }
    finally
    {
      Cursor.Current = current;
    }
  }

  /// <summary>Сохранить OLE документ в поток</summary>
  /// <param name="stream"></param>
  public void SaveTo(Stream stream)
  {
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    if (this.OleObject == null)
      throw new InvalidOperationException(LangStrings.GetString("ImOleContainerNeedObjectToSave"));
    this.SaveStateToStream(stream);
    if (this.adsite != null)
      this.adsite.DataChanged = false;
    this.OnSaved(EventArgs.Empty);
  }

  public void SaveTo(string fileName)
  {
    if (this.storage == null)
      return;
    if (this.OleObject == null)
      throw new InvalidOperationException(LangStrings.GetString("ImOleContainerNeedObjectToSave"));
    UnsafeMethods.IStorage pStorage = (UnsafeMethods.IStorage) null;
    int docfile = UnsafeMethods.StgCreateDocfile(fileName, 69650, 0, out pStorage);
    if (HelperMethods.Succeeded(docfile) && pStorage != null)
    {
      HelperMethods.OleCheck(UnsafeMethods.OleSave((UnsafeMethods.IPersistStorage) this.OleObject, pStorage, false));
      Marshal.ReleaseComObject((object) pStorage);
      pStorage = (UnsafeMethods.IStorage) null;
      this.adsite.DataChanged = false;
      this.OnSaved(EventArgs.Empty);
    }
    else
    {
      switch (docfile)
      {
        case -2147287036 /*0x80030004*/:
        case -2147287008 /*0x80030020*/:
        case -2147287007:
          throw new IOException(LangStrings.GetString("ImOleContainerFileInUse", (object) fileName), docfile);
        case -2147287035 /*0x80030005*/:
        case -2147287021:
          throw new UnauthorizedAccessException(LangStrings.GetString("ImOleContainerAccessDenied", (object) fileName));
        case -2147287015:
        case -2147287011:
        case -2147287010:
          throw new IOException(LangStrings.GetString("ImOleContainerUnexpectedFileError"), docfile);
      }
    }
  }

  internal void SetActivationStateInternal(ActivationState newState)
  {
    if (newState == this.activationState)
      return;
    this.activationState = newState;
    if (newState != ActivationState.Inactive)
    {
      if (newState != ActivationState.Active)
        return;
      this.OnActivated(EventArgs.Empty);
    }
    else
      this.OnDeactivated(EventArgs.Empty);
  }

  private void SetFlag(int flag, bool value)
  {
    if (value)
      this.flags |= flag;
    else
      this.flags &= ~flag;
  }

  internal void SetStatusTextInternal(string text)
  {
    this.statusText = text;
    this.OnStatusTextChanged(EventArgs.Empty);
  }

  internal bool ShouldSerializeBackColor() => this.BackColor != SystemColors.Window;

  private bool ShouldSerializeDocumentClassName()
  {
    return this.DocumentClassName != null && this.DocumentClassName.Length > 0;
  }

  private bool ShouldSerializeGuid() => this.GetFlag(1) && this.Guid != Guid.Empty;

  private bool ShouldSerializeSourceData()
  {
    if (this.sourceDoc != null)
      return true;
    return this.storage != null && this.IsDocumentDataDirty;
  }

  private bool ShouldSerializeSourceDocument() => this.GetFlag(16 /*0x10*/);

  private bool ShouldSerializeToolTarget() => this.ToolTarget != this;

  private Stream StreamFromIStorage(UnsafeMethods.IStorage storage, Stream stream)
  {
    if (storage == null)
      return (Stream) null;
    UnsafeMethods.ILockBytes ilockBytesOnHglobal = UnsafeMethods.CreateILockBytesOnHGlobal(new HandleRef((object) this, IntPtr.Zero), true);
    UnsafeMethods.IStorage storage1 = (UnsafeMethods.IStorage) null;
    Cursor current = Cursor.Current;
    try
    {
      Cursor.Current = Cursors.WaitCursor;
      storage1 = UnsafeMethods.StgCreateDocfileOnILockBytes(ilockBytesOnHglobal, 4114, 0);
      int hResult = UnsafeMethods.OleSave((UnsafeMethods.IPersistStorage) this.OleObject, storage1, false);
      try
      {
        HelperMethods.OleCheck(hResult);
      }
      catch (Win32Exception ex)
      {
        return stream;
      }
      IntPtr hglobalFromIlockBytes = UnsafeMethods.GetHGlobalFromILockBytes(ilockBytesOnHglobal);
      int count = UnsafeMethods.GlobalSize(new HandleRef((object) this, hglobalFromIlockBytes));
      IntPtr source = UnsafeMethods.GlobalLock(new HandleRef((object) this, hglobalFromIlockBytes));
      if (source == IntPtr.Zero)
        return stream;
      try
      {
        byte[] numArray = new byte[count];
        Marshal.Copy(source, numArray, 0, numArray.Length);
        if (stream == null)
        {
          stream = (Stream) new MemoryStream(numArray);
          return stream;
        }
        stream.Write(numArray, 0, count);
        return stream;
      }
      finally
      {
        UnsafeMethods.GlobalUnlock(new HandleRef((object) this, hglobalFromIlockBytes));
      }
    }
    finally
    {
      Marshal.ReleaseComObject((object) storage1);
      Marshal.ReleaseComObject((object) ilockBytesOnHglobal);
      Cursor.Current = current;
    }
  }

  private UnsafeMethods.IStorage StreamToIStorage(Stream s)
  {
    if (s == null)
      return (UnsafeMethods.IStorage) null;
    byte[] numArray = new byte[s.Length];
    s.Read(numArray, 0, numArray.Length);
    IntPtr handle = UnsafeMethods.GlobalAlloc(8194, numArray.Length);
    IntPtr num = UnsafeMethods.GlobalLock(new HandleRef((object) this, handle));
    try
    {
      Marshal.Copy(numArray, 0, num, numArray.Length);
      byte[] destination = new byte[numArray.Length];
      Marshal.Copy(num, destination, 0, destination.Length);
    }
    finally
    {
      UnsafeMethods.GlobalUnlock(new HandleRef((object) this, handle));
    }
    UnsafeMethods.ILockBytes ilockBytesOnHglobal = UnsafeMethods.CreateILockBytesOnHGlobal(new HandleRef((object) this, handle), true);
    if (ilockBytesOnHglobal != null)
      return UnsafeMethods.StgOpenStorageOnILockBytes(ilockBytesOnHglobal, (UnsafeMethods.IStorage) null, 18, 0, 0);
    UnsafeMethods.GlobalFree(new HandleRef((object) this, handle));
    return (UnsafeMethods.IStorage) null;
  }

  void ISupportInitialize.BeginInit() => this.SetFlag(512 /*0x0200*/, true);

  void ISupportInitialize.EndInit()
  {
    this.SetFlag(512 /*0x0200*/, false);
    if (!this.IsHandleCreated || this.OleObject != null)
      return;
    this.InitializeOleObject();
  }

  private void ToolTarget_HandleCreated(object sender, EventArgs e)
  {
    if (!this.GetFlag(256 /*0x0100*/))
      return;
    this.SetFlag(256 /*0x0100*/, false);
    Application.Idle += new EventHandler(this.OnIdle);
  }

  private void ToolTarget_HandleDestroyed(object sender, EventArgs e)
  {
    if (this.ActivationState != ActivationState.Active)
      return;
    this.SetFlag(33024, true);
    this.Deactivate();
  }

  private void ToolTarget_Resize(object sender, EventArgs e)
  {
    if (this.adcontainer == null)
      return;
    this.adcontainer.UpdateBorders();
  }

  private static bool VerifyCast(object comObject, System.Type t)
  {
    IntPtr iunknownForObject = Marshal.GetIUnknownForObject(comObject);
    if (iunknownForObject != IntPtr.Zero)
    {
      try
      {
        Guid guid = t.GUID;
        IntPtr ppv;
        if (!HelperMethods.Succeeded(Marshal.QueryInterface(iunknownForObject, ref guid, out ppv)))
          return false;
        Marshal.Release(ppv);
      }
      finally
      {
        Marshal.Release(iunknownForObject);
      }
    }
    return true;
  }

  /// <summary>Перевод OLE объекта в состояние выполнения</summary>
  /// <returns></returns>
  public int OleRun() => this.OleObject == null ? 0 : UnsafeMethods.OleRun(this.OleObject);

  [DefaultValue(2)]
  [SRCategory("CatBehavior")]
  [SRDescription("ImOleContainerActivationGesture")]
  public ActivationGesture ActivationGesture
  {
    get => this.activationGesture;
    set
    {
      this.activationGesture = Enum.IsDefined(typeof (ActivationGesture), (object) value) ? value : throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (ActivationGesture));
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ActivationState ActivationState
  {
    get
    {
      return this.Visible && this.IsHandleCreated && this.OleObject != null ? this.activationState : ActivationState.None;
    }
  }

  [Obsolete("This will be removed before the end of M2, please call ImOleContainer.Document instead")]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public object ActiveDocument => this.Document;

  [SRDescription("ImOleContainerActiveDocumentType")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string ActiveDocumentType
  {
    get
    {
      if (this.OleObject != null)
      {
        string userType = (string) null;
        if (HelperMethods.Succeeded(this.OleObject.GetUserType(3, out userType)))
          return userType;
      }
      return "";
    }
  }

  private ImOleContainer.ActiveDocumentContainer AdContainer => this.adcontainer;

  private ImOleContainer.ActiveDocumentSite AdSite => this.adsite;

  public override System.Drawing.Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override Image BackgroundImage
  {
    get => base.BackgroundImage;
    set => base.BackgroundImage = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override ImageLayout BackgroundImageLayout
  {
    get => ImageLayout.None;
    set
    {
    }
  }

  public BorderStyle BorderStyle
  {
    get => this.borderStyle;
    set
    {
      if (!Enum.IsDefined(typeof (BorderStyle), (object) value))
        throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (BorderStyle));
      if (value == this.borderStyle)
        return;
      this.borderStyle = value;
      this.Invalidate(true);
    }
  }

  [SRCategory("CatAppearance")]
  [DefaultValue(false)]
  public bool DisplayAsIcon
  {
    get => this.GetFlag(2048 /*0x0800*/);
    set
    {
      if (value == this.GetFlag(2048 /*0x0800*/))
        return;
      this.SetFlag(2048 /*0x0800*/, value);
      this.Invalidate(true);
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public object Document => (object) this.OleObject;

  [RefreshProperties(RefreshProperties.Repaint)]
  [SRDescription("ImOleContainerProgId")]
  [SRCategory("CatAppearance")]
  public string DocumentClassName
  {
    get
    {
      if (this.progId == null && this.guid != Guid.Empty)
        this.progId = this.GetProgIdFromGuid(this.guid);
      return this.progId;
    }
    set
    {
      if (!(value != this.progId) && !this.IsDocumentDataDirty)
        return;
      bool flag = this.ActivationState == ActivationState.Active;
      string str = (string) null;
      if (this.ShouldSerializeDocumentClassName())
        str = this.progId;
      if (this.OleObject != null)
        this.DestroyOleObject();
      if (value != null && value.Length > 0)
      {
        this.guid = this.GetGuidFromProgId(value);
        if (!this.IsValidDocObject(this.guid, false))
          throw new ArgumentException(LangStrings.GetString("ImOleContainerInvalidProgId", (object) value));
        this.sourceDoc = (string) null;
        this.progId = value;
        this.SetFlag(1, false);
        this.SetFlag(2, true);
        this.SetFlag(16 /*0x10*/, false);
        if (!this.IsHandleCreated)
          return;
        try
        {
          this.InitializeOleObject();
        }
        catch
        {
          this.progId = str;
          this.SetFlag(2, str != null);
          throw;
        }
        if (!flag)
          return;
        this.Activate();
      }
      else
      {
        this.progId = (string) null;
        this.SetFlag(2, false);
      }
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override System.Drawing.Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  [RefreshProperties(RefreshProperties.Repaint)]
  [SRDescription("ImOleContainerGuid")]
  [Browsable(false)]
  [SRCategory("CatAppearance")]
  public Guid Guid
  {
    get
    {
      if (this.guid == Guid.Empty && this.progId != null)
        this.guid = this.GetGuidFromProgId(this.progId);
      return this.guid;
    }
    set
    {
      if (!(value != this.guid) && !this.IsDocumentDataDirty)
        return;
      if (value != Guid.Empty)
        this.IsValidDocObject(value, true);
      Guid guid = Guid.Empty;
      if (this.ShouldSerializeGuid())
        guid = this.guid;
      bool flag = this.ActivationState == ActivationState.Active;
      if (this.OleObject != null)
        this.DestroyOleObject();
      if (value != Guid.Empty)
      {
        this.progId = this.GetProgIdFromGuid(value);
        this.guid = value;
        this.sourceDoc = (string) null;
        this.SetFlag(16 /*0x10*/, false);
        this.SetFlag(1, true);
        this.SetFlag(2, false);
        if (!this.IsHandleCreated)
          return;
        try
        {
          this.InitializeOleObject();
        }
        catch
        {
          this.guid = guid;
          this.SetFlag(1, this.guid != Guid.Empty);
          throw;
        }
        if (!flag)
          return;
        this.Activate();
      }
      else
      {
        this.guid = value;
        this.SetFlag(1, false);
      }
    }
  }

  [Browsable(false)]
  public bool IsDocumentDataDirty => this.adsite != null && this.adsite.DataChanged;

  [Browsable(false)]
  public UnsafeMethods.IOleObject OleObject => this.pOleObject;

  [Obsolete("Please use DocumentClassName.  ProgId will be removed before the end of M2")]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string ProgId
  {
    get => this.DocumentClassName;
    set => this.DocumentClassName = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override RightToLeft RightToLeft
  {
    get => base.RightToLeft;
    set => base.RightToLeft = value;
  }

  [SRCategory("CatAppearance")]
  [SRDescription("ImOleContainerShowMenus")]
  [DefaultValue(true)]
  public bool ShowMenus
  {
    get => this.GetFlag(8);
    set
    {
      if (this.ShowMenus == value)
        return;
      this.SetFlag(8, value);
      if (this.ActivationState != ActivationState.Active)
        return;
      this.Deactivate();
      this.Activate();
    }
  }

  [SRDescription("ImOleContainerShowToolbars")]
  [SRCategory("CatAppearance")]
  [DefaultValue(true)]
  public bool ShowToolbars
  {
    get => this.GetFlag(4);
    set
    {
      if (this.ShowToolbars == value)
        return;
      this.SetFlag(4, value);
      if (this.ActivationState != ActivationState.Active)
        return;
      this.Deactivate();
      this.Activate();
    }
  }

  [SRCategory("CatAppearance")]
  [SRDescription("ImOleContainerSizeMode")]
  [DefaultValue(0)]
  public DocumentSizeMode SizeMode
  {
    get => this.sizeMode;
    set
    {
      if (!Enum.IsDefined(typeof (DocumentSizeMode), (object) value))
        throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (DocumentSizeMode));
      if (value == this.sizeMode)
        return;
      this.sizeMode = value;
      if (this.OleObject == null)
        return;
      this.AdSite.OnSizeModeChanged((object) this, EventArgs.Empty);
    }
  }

  [Browsable(false)]
  public Stream SourceData
  {
    get => this.SaveStateToStream((Stream) null);
    set
    {
      bool flag1 = this.OleObject != null;
      bool flag2 = this.ActivationState == ActivationState.Active;
      if (flag1)
        this.DestroyOleObject();
      this.SetFlag(32 /*0x20*/, value != null);
      this.SetFlag(3, false);
      if (value == null)
        return;
      if (!this.GetFlag(512 /*0x0200*/))
      {
        this.SetFlag(16 /*0x10*/, false);
        this.sourceDoc = (string) null;
      }
      this.storage = this.StreamToIStorage(value);
      if (!flag1 && !this.IsHandleCreated)
        return;
      this.InitializeOleObject();
      if (!flag2)
        return;
      this.Activate();
    }
  }

  [Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.Repaint)]
  [SRDescription("ImOleContainerSourceDocument")]
  [SRCategory("CatAppearance")]
  public string SourceDocument
  {
    get => this.sourceDoc;
    set
    {
      if (!(value != this.sourceDoc) && !this.IsDocumentDataDirty)
        return;
      this.DestroyOleObject();
      if (value != null && value.Length > 0)
      {
        this.LoadFrom(value);
      }
      else
      {
        this.sourceDoc = (string) null;
        this.SetFlag(16 /*0x10*/, false);
      }
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public string StatusText => this.statusText;

  private UnsafeMethods.IStorage Storage => this.storage;

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Bindable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  [SRCategory("CatAppearance")]
  [TypeConverter(typeof (ImOleContainer.ToolTargetConverter))]
  [SRDescription("ImOleContainerToolTarget")]
  public ScrollableControl ToolTarget
  {
    get => this.toolTarget != null ? this.toolTarget : (ScrollableControl) this;
    set
    {
      if (value == this.toolTarget)
        return;
      if (value == this)
        value = (ScrollableControl) null;
      ScrollableControl scrollableControl = value;
      if (value == null)
        scrollableControl = (ScrollableControl) this;
      int num = this.ActivationState == ActivationState.Active ? 1 : 0;
      if (num != 0)
        this.Deactivate();
      if (!this.DesignMode)
      {
        this.ToolTarget.HandleDestroyed -= new EventHandler(this.ToolTarget_HandleDestroyed);
        this.ToolTarget.HandleCreated -= new EventHandler(this.ToolTarget_HandleCreated);
        this.ToolTarget.Resize -= new EventHandler(this.ToolTarget_Resize);
      }
      this.SetFlag(256 /*0x0100*/, false);
      this.toolTarget = value;
      if (!this.DesignMode)
      {
        scrollableControl.HandleDestroyed += new EventHandler(this.ToolTarget_HandleDestroyed);
        scrollableControl.HandleCreated += new EventHandler(this.ToolTarget_HandleCreated);
        scrollableControl.Resize += new EventHandler(this.ToolTarget_Resize);
      }
      if (this.adsite != null && this.Visible)
        this.adsite.UpdateClientRect(true, Rectangle.Empty);
      if (scrollableControl.Visible)
        scrollableControl.PerformLayout();
      if (num == 0)
        return;
      this.Activate();
    }
  }

  public SizeF GetExtentMm()
  {
    tagSIZEL tagSizel = new tagSIZEL();
    if (this.pOleObject is UnsafeMethods.IViewObject2 pOleObject)
      pOleObject.GetExtent(1, -1, (tagDVTARGETDEVICE) null, tagSizel);
    else if (this.pOleObject != null)
      this.pOleObject.GetExtent(1, tagSizel);
    return new SizeF((float) tagSizel.cx / 100f, (float) tagSizel.cy / 100f);
  }

  protected static IContainerControl GetContainerControlInternal(Control acontrol)
  {
    Control containerControlInternal = acontrol;
    while (true)
    {
      switch (containerControlInternal)
      {
        case null:
        case IContainerControl _:
          goto label_3;
        default:
          containerControlInternal = containerControlInternal.Parent;
          continue;
      }
    }
label_3:
    return (IContainerControl) containerControlInternal;
  }

  internal enum CreateType
  {
    ctNewObject,
    ctFromFile,
    ctLinkToFile,
    ctFromData,
    ctLinkFromData,
  }

  internal class CreateInfo
  {
    public ImOleContainer.CreateType createType;
    public bool showAsIcon;
    public HandleRef hRef;
    public Guid classID;
    public string fileName;
    public IDataObject idataObject;
    public IntPtr hMetaPict;
  }

  protected class ActiveDocumentContainer : 
    StandardOleMarshalObject,
    IDisposable,
    UnsafeMethods.IOleInPlaceFrame,
    UnsafeMethods.IOleInPlaceUIWindow,
    UnsafeMethods.IOleWindow,
    UnsafeMethods.IOleContainer
  {
    private UnsafeMethods.IOleInPlaceActiveObject activeInPlaceObject;
    private ScrollableControl.DockPaddingEdges dockPadding;
    private MainMenu formMenu;
    private int lockCount;
    private bool menuSet;
    private int modelessCount;
    private ImOleContainer owner;

    public ActiveDocumentContainer(ImOleContainer owner)
    {
      this.owner = owner;
      owner.Deactivated += new EventHandler(this.OnOwnerDeactivated);
    }

    public virtual void Dispose()
    {
      if (this.owner != null)
      {
        this.owner.Deactivated -= new EventHandler(this.OnOwnerDeactivated);
        this.owner = (ImOleContainer) null;
      }
      if (this.activeInPlaceObject == null)
        return;
      this.activeInPlaceObject = (UnsafeMethods.IOleInPlaceActiveObject) null;
    }

    private void OnOwnerDeactivated(object sender, EventArgs e)
    {
      ((UnsafeMethods.IOleInPlaceFrame) this).SetMenu(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
      if (this.dockPadding == null)
        return;
      this.owner.ToolTarget.SuspendLayout();
      this.owner.ToolTarget.DockPadding.Left = this.dockPadding.Left;
      this.owner.ToolTarget.DockPadding.Top = this.dockPadding.Top;
      this.owner.ToolTarget.DockPadding.Bottom = this.dockPadding.Bottom;
      this.owner.ToolTarget.DockPadding.Right = this.dockPadding.Right;
      this.owner.ToolTarget.ResumeLayout(true);
      this.dockPadding = (ScrollableControl.DockPaddingEdges) null;
    }

    int UnsafeMethods.IOleContainer.EnumObjects(int grfFlags, out UnsafeMethods.IEnumUnknown ppEnum)
    {
      ppEnum = (UnsafeMethods.IEnumUnknown) null;
      return -2147467263 /*0x80004001*/;
    }

    int UnsafeMethods.IOleContainer.LockContainer(bool fLock)
    {
      if (fLock)
        ++this.lockCount;
      else
        --this.lockCount;
      return 0;
    }

    int UnsafeMethods.IOleContainer.ParseDisplayName(
      object pbc,
      string pszDisplayName,
      int[] pchEaten,
      object[] ppmkOut)
    {
      return -2147467263 /*0x80004001*/;
    }

    int UnsafeMethods.IOleInPlaceFrame.ContextSensitiveHelp(int fEnterMode)
    {
      if (this.activeInPlaceObject == null)
        return -2147467259 /*0x80004005*/;
      this.activeInPlaceObject.ContextSensitiveHelp(fEnterMode);
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.EnableModeless(bool fEnable)
    {
      if (fEnable)
        ++this.modelessCount;
      else
        --this.modelessCount;
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.GetBorder(COMRECT borderRect)
    {
      if (!this.owner.ShowToolbars)
        return -2147221087;
      Rectangle clientRectangle = this.owner.ToolTarget.ClientRectangle;
      borderRect.left = clientRectangle.Left;
      borderRect.top = clientRectangle.Top;
      borderRect.bottom = clientRectangle.Bottom;
      borderRect.right = clientRectangle.Right;
      return 0;
    }

    IntPtr UnsafeMethods.IOleInPlaceFrame.GetWindow() => this.owner.ToolTarget.Handle;

    int UnsafeMethods.IOleInPlaceFrame.InsertMenus(
      IntPtr hmenuShared,
      tagOleMenuGroupWidths lpMenuWidths)
    {
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.RemoveMenus(IntPtr hmenuShared)
    {
      ((UnsafeMethods.IOleInPlaceFrame) this).SetMenu(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.RequestBorderSpace(COMRECT r)
    {
      return this.owner.ShowToolbars && r.left + r.right <= this.owner.ToolTarget.Width && r.top + r.bottom <= this.owner.ToolTarget.Height ? 0 : -2147221087;
    }

    int UnsafeMethods.IOleInPlaceFrame.SetActiveObject(
      UnsafeMethods.IOleInPlaceActiveObject pActiveObject,
      string pszObjName)
    {
      if (this.activeInPlaceObject != null)
        this.activeInPlaceObject = (UnsafeMethods.IOleInPlaceActiveObject) null;
      this.activeInPlaceObject = pActiveObject;
      if (this.activeInPlaceObject == null)
        ((UnsafeMethods.IOleInPlaceFrame) this).SetMenu(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.SetBorderSpace(COMRECT r)
    {
      if (!this.owner.ShowToolbars)
      {
        this.dockPadding = (ScrollableControl.DockPaddingEdges) null;
        return -2147221087;
      }
      this.owner.ToolTarget.SuspendLayout();
      if (this.dockPadding == null)
        this.dockPadding = (ScrollableControl.DockPaddingEdges) ((ICloneable) this.owner.ToolTarget.DockPadding).Clone();
      this.owner.ToolTarget.DockPadding.Left = r.left;
      this.owner.ToolTarget.DockPadding.Top = r.top;
      this.owner.ToolTarget.DockPadding.Bottom = r.bottom;
      this.owner.ToolTarget.DockPadding.Right = r.right;
      this.owner.ToolTarget.ResumeLayout(true);
      if (this.owner.adsite != null)
        this.owner.adsite.UpdateClientRect(false, Rectangle.Empty);
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.SetMenu(
      IntPtr hmenuShared,
      IntPtr holemenu,
      IntPtr hwndActiveObject)
    {
      if ((this.owner.ShowMenus || hmenuShared == IntPtr.Zero) && (hmenuShared != IntPtr.Zero || this.menuSet))
      {
        Form wrapper = this.owner.FindForm();
        if (wrapper != null && wrapper.MdiParent != null)
          wrapper = wrapper.MdiParent;
        if (wrapper == null || this.activeInPlaceObject == null && holemenu != IntPtr.Zero)
          return -2147467259 /*0x80004005*/;
        if (holemenu != IntPtr.Zero)
        {
          this.formMenu = wrapper.Menu;
          wrapper.Menu = (MainMenu) null;
        }
        if (wrapper.IsMdiContainer)
        {
          if (hmenuShared != IntPtr.Zero)
            SafeNativeMethods.DrawMenuBar(new HandleRef((object) wrapper, wrapper.Handle));
        }
        else
          UnsafeMethods.SetMenu(new HandleRef((object) wrapper, wrapper.Handle), new HandleRef((object) this, hmenuShared));
        UnsafeMethods.OleSetMenuDescriptor(holemenu, wrapper.Handle, hwndActiveObject, (UnsafeMethods.IOleInPlaceFrame) this, this.activeInPlaceObject);
        if (holemenu == IntPtr.Zero)
        {
          if (this.formMenu != null)
          {
            wrapper.Menu = this.formMenu;
            this.formMenu = (MainMenu) null;
          }
          this.menuSet = false;
        }
        else
          this.menuSet = true;
        wrapper.PerformLayout();
      }
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.SetStatusText(string pszStatusText)
    {
      this.Owner.SetStatusTextInternal(pszStatusText);
      return 0;
    }

    int UnsafeMethods.IOleInPlaceFrame.TranslateAccelerator(ref MSG lpmsg, short wID)
    {
      if (wID != (short) 27 || !(lpmsg.wParam == (IntPtr) (int) wID))
        return 1;
      this.Owner.Deactivate();
      return 0;
    }

    int UnsafeMethods.IOleInPlaceUIWindow.ContextSensitiveHelp(int fEnterMode)
    {
      return ((UnsafeMethods.IOleInPlaceFrame) this).ContextSensitiveHelp(fEnterMode);
    }

    int UnsafeMethods.IOleInPlaceUIWindow.GetBorder(COMRECT lprectBorder)
    {
      return ((UnsafeMethods.IOleInPlaceFrame) this).GetBorder(lprectBorder);
    }

    IntPtr UnsafeMethods.IOleInPlaceUIWindow.GetWindow()
    {
      return ((UnsafeMethods.IOleInPlaceFrame) this).GetWindow();
    }

    int UnsafeMethods.IOleInPlaceUIWindow.RequestBorderSpace(COMRECT pborderwidths)
    {
      return ((UnsafeMethods.IOleInPlaceFrame) this).RequestBorderSpace(pborderwidths);
    }

    void UnsafeMethods.IOleInPlaceUIWindow.SetActiveObject(
      UnsafeMethods.IOleInPlaceActiveObject pActiveObject,
      string pszObjName)
    {
      ((UnsafeMethods.IOleInPlaceFrame) this).SetActiveObject(pActiveObject, pszObjName);
    }

    int UnsafeMethods.IOleInPlaceUIWindow.SetBorderSpace(COMRECT pborderwidths)
    {
      return ((UnsafeMethods.IOleInPlaceFrame) this).SetBorderSpace(pborderwidths);
    }

    void UnsafeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
    {
    }

    int UnsafeMethods.IOleWindow.GetWindow(out IntPtr pHwnd)
    {
      pHwnd = this.owner.ToolTarget.Handle;
      return 0;
    }

    internal void UpdateBorders()
    {
      if (this.activeInPlaceObject == null || !ImOleContainer.VerifyCast((object) this.activeInPlaceObject, typeof (UnsafeMethods.IOleInPlaceActiveObject)))
        return;
      this.activeInPlaceObject.ResizeBorder(this.owner.ShowToolbars ? new COMRECT(this.owner.ToolTarget.ClientRectangle) : new COMRECT(), (UnsafeMethods.IOleInPlaceUIWindow) this, true);
    }

    internal UnsafeMethods.IOleInPlaceActiveObject ActiveObject => this.activeInPlaceObject;

    protected ImOleContainer Owner => this.owner;
  }

  protected class ActiveDocumentSite : 
    StandardOleMarshalObject,
    UnsafeMethods.IOleWindow,
    UnsafeMethods.IOleClientSite,
    IAdviseSink,
    UnsafeMethods.IOleInPlaceSite,
    UnsafeMethods.IOleServiceProvider,
    IDisposable
  {
    private bool drawingToMetafile;
    private UnsafeMethods.IOleInPlaceObject activeObject;
    private int adviseCookie;
    private int dAdviseCookie;
    private bool dataChanged;
    private bool deactivating;
    private IntPtr hAccelTable;
    private static readonly int hiMetricPerInch = 2540;
    private int innerState;
    private bool lockedRunning;
    private static Point logPixels = Point.Empty;
    private ImOleContainer.OleObjectPlaceholder oleObjectPlaceholder;
    private Rectangle oleParentBounds;
    private ImOleContainer owner;
    private const int StateInactive = 0;
    private const int StateInPlaceActive = 1;
    private const int StateUiActive = 2;
    private bool suppressEvents;

    public ActiveDocumentSite(ImOleContainer owner)
    {
      this.hAccelTable = IntPtr.Zero;
      this.oleParentBounds = Rectangle.Empty;
      this.owner = owner;
      this.oleObjectPlaceholder = new ImOleContainer.OleObjectPlaceholder();
      this.oleObjectPlaceholder.Click += new EventHandler(this.OleObjectPlaceholder_Click);
      this.oleObjectPlaceholder.DoubleClick += new EventHandler(this.OleObjectPlaceholder_DoubleClick);
      this.oleObjectPlaceholder.GotFocus += new EventHandler(this.OleObjectPlaceholder_GotFocus);
      this.oleObjectPlaceholder.LostFocus += new EventHandler(this.OleObjectPlaceholder_LostFocus);
      this.oleObjectPlaceholder.KeyDown += new KeyEventHandler(this.OleObjectPlaceholder_KeyDown);
      this.oleObjectPlaceholder.SizeChanged += new EventHandler(this.OleObjectPlaceholder_SizeChanged);
      this.oleObjectPlaceholder.Paint += new PaintEventHandler(this.OleObjectPlaceholder_Paint);
      this.oleObjectPlaceholder.Dock = DockStyle.Fill;
      owner.Controls.Add((Control) this.oleObjectPlaceholder);
      owner.Loaded += new EventHandler(this.OnOleObjectLoaded);
      this.innerState = 0;
    }

    private Rectangle ComputeDrawRect()
    {
      Rectangle empty = Rectangle.Empty;
      int dwDrawAspect = this.Owner.DisplayAsIcon ? 4 : 1;
      tagSIZEL tagSizel = new tagSIZEL();
      UnsafeMethods.IOleObject oleObject1 = this.Owner.OleObject;
      if (this.Owner.OleObject is UnsafeMethods.IViewObject2 oleObject2)
      {
        try
        {
          oleObject2.GetExtent(dwDrawAspect, -1, (tagDVTARGETDEVICE) null, tagSizel);
        }
        catch (COMException ex)
        {
          tagSizel.cx = 0;
          tagSizel.cy = 0;
        }
      }
      else if (!HelperMethods.Succeeded(oleObject1.GetExtent(dwDrawAspect, tagSizel)))
      {
        tagSizel.cx = 0;
        tagSizel.cy = 0;
      }
      if (tagSizel.cx == 0 || tagSizel.cy == 0)
        return this.oleObjectPlaceholder.ClientRectangle;
      Size pixel = this.HiMetricToPixel(tagSizel.cx, tagSizel.cy);
      Size size = this.oleObjectPlaceholder.ClientRectangle.Size;
      Rectangle drawRect1 = new Rectangle(0, 0, size.Width, size.Height);
      switch (this.Owner.SizeMode)
      {
        case DocumentSizeMode.Clip:
          empty.X = 0;
          empty.Y = 0;
          empty.Width = pixel.Width;
          empty.Height = pixel.Height;
          return empty;
        case DocumentSizeMode.Stretch:
          return drawRect1;
        case DocumentSizeMode.Zoom:
          Rectangle drawRect2 = drawRect1;
          if (pixel.Width * size.Height >= size.Width * pixel.Height)
          {
            drawRect2.Height = (int) ((long) (pixel.Height * size.Width) / (long) pixel.Width) - drawRect2.Top;
            return drawRect2;
          }
          drawRect2.Width = (int) ((long) (pixel.Width * size.Height) / (long) pixel.Height) - drawRect2.Left;
          return drawRect2;
        default:
          return empty;
      }
    }

    internal void Deactivate()
    {
      if (this.deactivating)
        return;
      if (this.activeObject == null)
        return;
      try
      {
        this.deactivating = true;
        this.suppressEvents = true;
        this.activeObject.InPlaceDeactivate();
        this.DoHostEvents();
        this.oleObjectPlaceholder.Invalidate();
      }
      finally
      {
        this.deactivating = false;
        this.suppressEvents = false;
        this.UpdateActivationState(this.innerState);
      }
    }

    public void Dispose()
    {
      if (this.owner.OleObject != null)
      {
        try
        {
          this.owner.OleObject.Close(1);
        }
        catch (Exception ex)
        {
        }
        if (this.lockedRunning)
        {
          UnsafeMethods.OleLockRunning(this.owner.OleObject, false, true);
          this.lockedRunning = false;
        }
      }
      if (this.adviseCookie != 0)
      {
        if (this.owner.OleObject != null)
        {
          try
          {
            this.owner.OleObject.Unadvise(this.adviseCookie);
          }
          catch (Exception ex)
          {
          }
          this.adviseCookie = 0;
        }
      }
      if (this.dAdviseCookie != 0)
      {
        if (this.owner.OleObject is IDataObject)
        {
          try
          {
            ((IDataObject) this.owner.OleObject).DUnadvise(this.dAdviseCookie);
          }
          catch
          {
          }
          this.dAdviseCookie = 0;
        }
      }
      if (this.activeObject != null)
        this.activeObject = (UnsafeMethods.IOleInPlaceObject) null;
      if (this.owner != null && this.oleObjectPlaceholder != null)
      {
        this.oleObjectPlaceholder.Click -= new EventHandler(this.OleObjectPlaceholder_Click);
        this.oleObjectPlaceholder.DoubleClick -= new EventHandler(this.OleObjectPlaceholder_DoubleClick);
        this.oleObjectPlaceholder.GotFocus -= new EventHandler(this.OleObjectPlaceholder_GotFocus);
        this.oleObjectPlaceholder.LostFocus -= new EventHandler(this.OleObjectPlaceholder_LostFocus);
        this.oleObjectPlaceholder.KeyDown -= new KeyEventHandler(this.OleObjectPlaceholder_KeyDown);
        this.oleObjectPlaceholder.SizeChanged -= new EventHandler(this.OleObjectPlaceholder_SizeChanged);
        this.oleObjectPlaceholder.Paint -= new PaintEventHandler(this.OleObjectPlaceholder_Paint);
        this.oleObjectPlaceholder.Dispose();
        this.oleObjectPlaceholder = (ImOleContainer.OleObjectPlaceholder) null;
      }
      if (this.owner != null)
        this.owner.Loaded -= new EventHandler(this.OnOleObjectLoaded);
      if (this.hAccelTable != IntPtr.Zero)
      {
        UnsafeMethods.DestroyAcceleratorTable(new HandleRef((object) this, this.hAccelTable));
        this.hAccelTable = IntPtr.Zero;
      }
      this.owner = (ImOleContainer) null;
    }

    private void DoHostEvents()
    {
    }

    /// <summary>Получить размеры сожержимого OLE документа</summary>
    /// <returns>Размеры содержимого OLE документа в 0.01 мм единицах</returns>
    public Size GetExtent()
    {
      tagSIZEL tagSizel = new tagSIZEL();
      UnsafeMethods.IOleObject oleObject1 = this.Owner.OleObject;
      if (this.Owner.OleObject is UnsafeMethods.IViewObject2 oleObject2)
        oleObject2.GetExtent(1, -1, (tagDVTARGETDEVICE) null, tagSizel);
      else if (!HelperMethods.Succeeded(oleObject1.GetExtent(1, tagSizel)))
      {
        tagSizel.cx = 0;
        tagSizel.cy = 0;
      }
      if (tagSizel.cx != 0 && tagSizel.cy != 0)
        return new Size(tagSizel.cx, tagSizel.cy);
      Rectangle clientRectangle = this.oleObjectPlaceholder.ClientRectangle;
      int width = clientRectangle.Width;
      clientRectangle = this.oleObjectPlaceholder.ClientRectangle;
      int height = clientRectangle.Height;
      return this.PixelToHiMetric(width, height);
    }

    public void PaintOn(Graphics graphics, Rectangle clipRect)
    {
      try
      {
        this.drawingToMetafile = true;
        this.oleObjectPlaceholder.PaintOn(graphics, clipRect);
      }
      finally
      {
        this.drawingToMetafile = false;
      }
    }

    public void PaintOn2(Graphics graphics, Rectangle clipRect)
    {
      try
      {
        this.drawingToMetafile = true;
        this.oleObjectPlaceholder.PaintOn(graphics, clipRect);
      }
      finally
      {
        this.drawingToMetafile = false;
      }
    }

    private void EnsureAcceleratorTable()
    {
      if (!(this.hAccelTable == IntPtr.Zero))
        return;
      IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (HelperMethods.ACCEL)));
      try
      {
        Marshal.StructureToPtr<HelperMethods.ACCEL>(new HelperMethods.ACCEL()
        {
          cmd = (short) 27,
          fVirt = (byte) 1,
          key = (short) 27
        }, num, false);
        this.hAccelTable = UnsafeMethods.CreateAcceleratorTable(new HandleRef((object) null, num), 1);
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.FreeHGlobal(num);
      }
    }

    private void GetObjectRects(ref Rectangle posRect, ref Rectangle clipRect, bool uiActive)
    {
      bool flag = posRect == Rectangle.Empty;
      if (flag)
        posRect = this.ComputeDrawRect();
      posRect.Inflate(-2, -2);
      clipRect = this.oleObjectPlaceholder.ClientRectangle;
      if (!uiActive)
        return;
      if (flag)
      {
        Rectangle client = this.OleObjectParent.RectangleToClient(this.oleObjectPlaceholder.RectangleToScreen(this.oleObjectPlaceholder.ClientRectangle));
        posRect.X += client.X;
        posRect.Y += client.Y;
      }
      tagSIZEL pSizel = new tagSIZEL();
      this.Owner.OleObject.GetExtent(1, pSizel);
      Size pixel = this.HiMetricToPixel(pSizel.cx, pSizel.cy);
      clipRect = this.OleObjectParent.ClientRectangle;
      if (this.Owner.SizeMode == DocumentSizeMode.Clip)
      {
        if (pixel.Width - 4 <= clipRect.Width)
          clipRect.Width = (int) short.MaxValue;
        if (pixel.Height - 4 > clipRect.Height)
          return;
        clipRect.Height = (int) short.MaxValue;
      }
      else
      {
        clipRect.Width = (int) short.MaxValue;
        clipRect.Height = (int) short.MaxValue;
      }
    }

    private Size HiMetricToPixel(int x, int y)
    {
      Point logPixels = this.LogPixels;
      int width = (logPixels.X * x + ImOleContainer.ActiveDocumentSite.hiMetricPerInch / 2) / ImOleContainer.ActiveDocumentSite.hiMetricPerInch;
      logPixels = this.LogPixels;
      int height = (logPixels.Y * y + ImOleContainer.ActiveDocumentSite.hiMetricPerInch / 2) / ImOleContainer.ActiveDocumentSite.hiMetricPerInch;
      return new Size(width, height);
    }

    private Size PixelToHiMetric(int x, int y)
    {
      return new Size(ImOleContainer.ActiveDocumentSite.hiMetricPerInch * x / this.LogPixels.X, ImOleContainer.ActiveDocumentSite.hiMetricPerInch * y / this.LogPixels.Y);
    }

    private void OleObjectPlaceholder_Click(object sender, EventArgs e)
    {
      this.oleObjectPlaceholder.Focus();
      if (this.owner.ActivationGesture != ActivationGesture.Click)
        return;
      this.UIActivate();
    }

    private void OleObjectPlaceholder_DoubleClick(object sender, EventArgs e)
    {
      if (this.owner.ActivationGesture != ActivationGesture.DoubleClick)
        return;
      this.UIActivate();
    }

    private void OleObjectPlaceholder_GotFocus(object sender, EventArgs e)
    {
      this.oleObjectPlaceholder.Invalidate();
      if (this.owner.ActivationGesture == ActivationGesture.Focus)
        this.UIActivate();
      this.SetFocusActiveObject();
    }

    private void OleObjectPlaceholder_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return || this.owner.ActivationGesture == ActivationGesture.Never)
        return;
      this.UIActivate();
    }

    private void OleObjectPlaceholder_LostFocus(object sender, EventArgs e)
    {
      this.oleObjectPlaceholder.Invalidate();
    }

    private void OleObjectPlaceholder_Paint(object sender, PaintEventArgs e)
    {
      if (this.activeObject == null)
      {
        IntPtr hdc = e.Graphics.GetHdc();
        try
        {
          if (this.owner.OleObject is UnsafeMethods.IViewObject oleObject)
          {
            COMRECT comrect1 = new COMRECT(this.ComputeDrawRect());
            COMRECT comrect2 = this.drawingToMetafile ? new COMRECT(e.ClipRectangle) : (COMRECT) null;
            int dwDrawAspect = this.Owner.DisplayAsIcon ? 4 : 1;
            IntPtr zero1 = IntPtr.Zero;
            IntPtr zero2 = IntPtr.Zero;
            IntPtr hdcDraw = hdc;
            COMRECT lprcBounds = comrect1;
            COMRECT lprcWBounds = comrect2;
            IntPtr zero3 = IntPtr.Zero;
            oleObject.Draw(dwDrawAspect, -1, zero1, (tagDVTARGETDEVICE) null, zero2, hdcDraw, lprcBounds, lprcWBounds, zero3, 0);
          }
        }
        finally
        {
          e.Graphics.ReleaseHdc(hdc);
        }
      }
      if (this.drawingToMetafile)
        return;
      switch (this.owner.BorderStyle)
      {
        case BorderStyle.FixedSingle:
          ControlPaint.DrawBorder(e.Graphics, this.oleObjectPlaceholder.ClientRectangle, this.owner.ForeColor, ButtonBorderStyle.Solid);
          break;
        case BorderStyle.Fixed3D:
          ControlPaint.DrawBorder3D(e.Graphics, this.oleObjectPlaceholder.ClientRectangle);
          break;
      }
      if (!this.oleObjectPlaceholder.Focused)
        return;
      Rectangle clientRectangle = this.oleObjectPlaceholder.ClientRectangle;
      clientRectangle.Inflate(-2, -2);
      ControlPaint.DrawFocusRectangle(e.Graphics, clientRectangle);
    }

    private void OleObjectPlaceholder_SizeChanged(object sender, EventArgs e)
    {
      if (!(this.oleObjectPlaceholder.ClientRectangle != this.oleParentBounds))
        return;
      this.UpdateExtent();
      this.oleParentBounds = this.oleObjectPlaceholder.ClientRectangle;
    }

    private void OnOleObjectLoaded(object sender, EventArgs e)
    {
      if (this.adviseCookie == 0)
        this.owner.OleObject.Advise((IAdviseSink) this, out this.adviseCookie);
      if (this.dAdviseCookie != 0 || !(this.owner.OleObject is IDataObject))
        return;
      ((IDataObject) this.owner.OleObject).DAdvise(ref new FORMATETC()
      {
        dwAspect = DVASPECT.NONE,
        lindex = -1,
        tymed = TYMED.TYMED_NONE
      }, ADVF.ADVF_NODATA, (IAdviseSink) this, out this.dAdviseCookie);
    }

    internal void OnSizeModeChanged(object sender, EventArgs e)
    {
      this.UpdateExtent();
      this.oleObjectPlaceholder.Invalidate();
    }

    private void SetFocusActiveObject()
    {
      if (this.activeObject == null)
        return;
      IntPtr hwnd = IntPtr.Zero;
      if (!HelperMethods.Succeeded(this.activeObject.GetWindow(out hwnd)))
        return;
      SafeNativeMethods.SetWindowPos(new HandleRef((object) this, hwnd), HelperMethods.HWND_TOP, 0, 0, 0, 0, 19);
      UnsafeMethods.SetFocus(new HandleRef((object) this, hwnd));
    }

    void IAdviseSink.OnClose()
    {
      this.DataChanged = false;
      this.Owner.OnClosed(EventArgs.Empty);
    }

    void IAdviseSink.OnDataChange(ref FORMATETC pFormatetc, ref STGMEDIUM pStgmed)
    {
      this.DataChanged = true;
    }

    void IAdviseSink.OnRename(IMoniker moniker)
    {
    }

    void IAdviseSink.OnSave() => this.DataChanged = false;

    void IAdviseSink.OnViewChange(int dwAspect, int lindex)
    {
    }

    int UnsafeMethods.IOleClientSite.GetContainer(out UnsafeMethods.IOleContainer container)
    {
      container = (UnsafeMethods.IOleContainer) this.Owner.AdContainer;
      return 0;
    }

    int UnsafeMethods.IOleClientSite.GetMoniker(
      int dwAssign,
      int dwWhichMoniker,
      out object moniker)
    {
      moniker = (object) null;
      return -2147467263 /*0x80004001*/;
    }

    int UnsafeMethods.IOleClientSite.OnShowWindow(int fShow) => 0;

    int UnsafeMethods.IOleClientSite.RequestNewObjectLayout() => -2147467263 /*0x80004001*/;

    int UnsafeMethods.IOleClientSite.SaveObject()
    {
      if (this.Owner.OleObject is UnsafeMethods.IPersistStorage oleObject && oleObject.IsDirty() == 0)
      {
        HelperMethods.OleCheck(UnsafeMethods.OleSave(oleObject, this.Owner.Storage, true));
        this.DataChanged = false;
        this.Owner.OnSaved(EventArgs.Empty);
      }
      return 0;
    }

    int UnsafeMethods.IOleClientSite.ShowObject() => 0;

    int UnsafeMethods.IOleInPlaceSite.CanInPlaceActivate() => 1;

    int UnsafeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode)
    {
      return ((UnsafeMethods.IOleInPlaceFrame) this.Owner.AdContainer).ContextSensitiveHelp(fEnterMode);
    }

    int UnsafeMethods.IOleInPlaceSite.DeactivateAndUndo()
    {
      if (this.activeObject != null)
        this.activeObject.InPlaceDeactivate();
      return 0;
    }

    int UnsafeMethods.IOleInPlaceSite.DiscardUndoState() => -2147467263 /*0x80004001*/;

    IntPtr UnsafeMethods.IOleInPlaceSite.GetWindow() => this.OleObjectParent.Handle;

    int UnsafeMethods.IOleInPlaceSite.GetWindowContext(
      out UnsafeMethods.IOleInPlaceFrame ppFrame,
      out UnsafeMethods.IOleInPlaceUIWindow ppDoc,
      COMRECT lprcPosRect,
      COMRECT lprcClipRect,
      tagOIFI lpFrameInfo)
    {
      if (lprcPosRect == null || lprcClipRect == null || lpFrameInfo == null)
      {
        ppFrame = (UnsafeMethods.IOleInPlaceFrame) null;
        ppDoc = (UnsafeMethods.IOleInPlaceUIWindow) null;
        return -2147024809;
      }
      ppFrame = (UnsafeMethods.IOleInPlaceFrame) this.Owner.AdContainer;
      ppDoc = (UnsafeMethods.IOleInPlaceUIWindow) null;
      Rectangle empty1 = Rectangle.Empty;
      Rectangle empty2 = Rectangle.Empty;
      this.GetObjectRects(ref empty1, ref empty2, true);
      new COMRECT(empty1).CopyTo(lprcPosRect);
      new COMRECT(empty2).CopyTo(lprcClipRect);
      Form form = this.Owner.FindForm();
      lpFrameInfo.cb = Marshal.SizeOf(typeof (tagOIFI));
      lpFrameInfo.fMDIApp = form != null && form.MdiParent != null;
      lpFrameInfo.hwndFrame = this.owner.ToolTarget.Handle;
      this.EnsureAcceleratorTable();
      lpFrameInfo.hAccel = this.hAccelTable;
      lpFrameInfo.cAccelEntries = 1;
      return 0;
    }

    int UnsafeMethods.IOleInPlaceSite.OnInPlaceActivate()
    {
      this.activeObject = (UnsafeMethods.IOleInPlaceObject) this.owner.OleObject;
      if (!this.lockedRunning)
      {
        UnsafeMethods.OleLockRunning(this.owner.OleObject, true, false);
        this.lockedRunning = true;
      }
      this.UpdateActivationState(1);
      this.oleObjectPlaceholder.Invalidate();
      return 0;
    }

    int UnsafeMethods.IOleInPlaceSite.OnInPlaceDeactivate()
    {
      IntPtr handle = this.OleObjectParent.Handle;
      COMRECT lprcPosRect = new COMRECT(this.OleObjectParent.ClientRectangle);
      this.Owner.OleObject.DoVerb(-6, IntPtr.Zero, (UnsafeMethods.IOleClientSite) this, 0, handle, lprcPosRect);
      if (this.activeObject != null)
        this.activeObject = (UnsafeMethods.IOleInPlaceObject) null;
      this.UpdateExtent();
      this.UpdateActivationState(0);
      return 0;
    }

    int UnsafeMethods.IOleInPlaceSite.OnPosRectChange(COMRECT lprcPosRect)
    {
      this.UpdateClientRect(false, new Rectangle(lprcPosRect.left, lprcPosRect.top, lprcPosRect.right - lprcPosRect.left, lprcPosRect.bottom - lprcPosRect.top));
      return 0;
    }

    int UnsafeMethods.IOleInPlaceSite.OnUIActivate()
    {
      this.UpdateActivationState(2);
      this.SetFocusActiveObject();
      this.UpdateClientRect(true, Rectangle.Empty);
      this.oleObjectPlaceholder.Invalidate();
      return 0;
    }

    int UnsafeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable)
    {
      this.Deactivate();
      return 0;
    }

    int UnsafeMethods.IOleInPlaceSite.Scroll(tagSIZE scrollExtant) => -2147467263 /*0x80004001*/;

    int UnsafeMethods.IOleServiceProvider.QueryService(
      ref Guid guidService,
      ref Guid riid,
      out IntPtr ppvObject)
    {
      ppvObject = IntPtr.Zero;
      return -2147467262 /*0x80004002*/;
    }

    void UnsafeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
    {
    }

    int UnsafeMethods.IOleWindow.GetWindow(out IntPtr pHwnd)
    {
      pHwnd = this.OleObjectParent.Handle;
      return 0;
    }

    internal void UIActivate()
    {
      this.oleObjectPlaceholder.Focus();
      this.DoHostEvents();
      try
      {
        this.suppressEvents = true;
        COMRECT lprcPosRect = new COMRECT(this.OleObjectParent.ClientRectangle);
        if (HelperMethods.Succeeded(this.owner.OleObject.DoVerb(0, IntPtr.Zero, (UnsafeMethods.IOleClientSite) this, 0, this.OleObjectParent.Handle, lprcPosRect)))
          return;
        this.owner.OleObject.DoVerb(-4, IntPtr.Zero, (UnsafeMethods.IOleClientSite) this, 0, this.OleObjectParent.Handle, lprcPosRect);
      }
      finally
      {
        this.suppressEvents = false;
        this.UpdateActivationState(this.innerState);
      }
    }

    private void UpdateActivationState(int newState)
    {
      this.innerState = newState;
      if (this.suppressEvents)
        return;
      ActivationState newState1;
      switch (this.innerState)
      {
        case 0:
          newState1 = ActivationState.Inactive;
          break;
        case 1:
        case 2:
          newState1 = ActivationState.Active;
          break;
        default:
          newState1 = ActivationState.Inactive;
          break;
      }
      this.Owner.SetActivationStateInternal(newState1);
    }

    internal void UpdateClientRect(bool updateBorders, Rectangle preferredRect)
    {
      if (this.activeObject == null)
        return;
      Rectangle clientRectangle = this.OleObjectParent.ClientRectangle;
      Rectangle posRect = preferredRect;
      this.GetObjectRects(ref posRect, ref clientRectangle, this.innerState == 2);
      COMRECT lprcPosRect = new COMRECT(posRect);
      COMRECT lprcClipRect = new COMRECT(clientRectangle);
      if (ImOleContainer.VerifyCast((object) this.activeObject, typeof (UnsafeMethods.IOleInPlaceObject)))
        this.activeObject.SetObjectRects(lprcPosRect, lprcClipRect);
      if (!updateBorders)
        return;
      this.owner.AdContainer.UpdateBorders();
    }

    internal void UpdateExtent()
    {
      if (this.innerState < 1)
        return;
      this.UpdateClientRect(false, Rectangle.Empty);
    }

    internal bool DataChanged
    {
      get => this.dataChanged;
      set
      {
        this.dataChanged = value;
        if (!value || this.Owner == null)
          return;
        this.Owner.OnDocumentModified(EventArgs.Empty);
      }
    }

    private Point LogPixels
    {
      get
      {
        if (ImOleContainer.ActiveDocumentSite.logPixels.IsEmpty)
        {
          ImOleContainer.ActiveDocumentSite.logPixels = new Point();
          IntPtr dc = UnsafeMethods.GetDC(new HandleRef((object) this, IntPtr.Zero));
          ImOleContainer.ActiveDocumentSite.logPixels.X = UnsafeMethods.GetDeviceCaps(new HandleRef((object) this, dc), 88);
          ImOleContainer.ActiveDocumentSite.logPixels.Y = UnsafeMethods.GetDeviceCaps(new HandleRef((object) this, dc), 90);
          UnsafeMethods.ReleaseDC(new HandleRef((object) this, IntPtr.Zero), new HandleRef((object) this, dc));
        }
        return ImOleContainer.ActiveDocumentSite.logPixels;
      }
    }

    private Control OleObjectParent
    {
      get
      {
        Control oleObjectParent = (Control) this.owner;
        while (oleObjectParent.Parent != null)
          oleObjectParent = oleObjectParent.Parent;
        return oleObjectParent;
      }
    }

    protected ImOleContainer Owner => this.owner;

    internal Size PreferredSize
    {
      get
      {
        tagSIZEL pSizel = new tagSIZEL();
        if (!HelperMethods.Succeeded(this.Owner.OleObject.GetExtent(this.Owner.DisplayAsIcon ? 4 : 1, pSizel)))
        {
          pSizel.cx = 0;
          pSizel.cy = 0;
        }
        if (pSizel.cx == 0 || pSizel.cy == 0)
          return Size.Empty;
        Size pixel = this.HiMetricToPixel(pSizel.cx, pSizel.cy);
        pixel.Width += 2;
        pixel.Height += 2;
        return pixel;
      }
    }
  }

  private class OleObjectPlaceholder : Control
  {
    public const int BorderWidth = 2;

    internal OleObjectPlaceholder()
    {
      this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
    }

    protected override void Dispose(bool d)
    {
      if (this.Parent != null)
        this.Parent.TabStopChanged -= new EventHandler(this.OnParentTabStopChanged);
      base.Dispose(d);
    }

    internal void PaintOn(Graphics graphics, Rectangle clipRect)
    {
      this.OnPaint(new PaintEventArgs(graphics, clipRect));
    }

    internal bool FocusInternal()
    {
      if (this.Focused && this.Parent != null)
      {
        IContainerControl containerControlInternal = ImOleContainer.GetContainerControlInternal(this.Parent);
        if (containerControlInternal != null)
          containerControlInternal.ActiveControl = (Control) null;
      }
      if (this.CanFocus)
        UnsafeMethods.SetFocus(new HandleRef((object) this, this.Handle));
      if (this.Focused && this.Parent != null)
      {
        IContainerControl containerControlInternal = ImOleContainer.GetContainerControlInternal(this.Parent);
        if (containerControlInternal != null)
          containerControlInternal.ActiveControl = (Control) this;
      }
      return this.Focused;
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      if (this.Parent == null)
        return;
      this.TabStop = this.Parent.TabStop;
      this.Parent.TabStopChanged += new EventHandler(this.OnParentTabStopChanged);
    }

    private void OnParentTabStopChanged(object sender, EventArgs e)
    {
      if (this.Parent == null || sender != this.Parent)
        return;
      this.TabStop = this.Parent.TabStop;
    }
  }

  internal class ToolTargetConverter(System.Type t) : ComponentConverter(t)
  {
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => false;

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
      ArrayList values = new ArrayList(standardValues.Count);
      foreach (object obj in standardValues)
      {
        if (obj != null && (!(obj is ImOleContainer) || obj == context.Instance))
          values.Add(obj);
      }
      return new TypeConverter.StandardValuesCollection((ICollection) values);
    }
  }
}
