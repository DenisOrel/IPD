
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer.PreviewHandlerViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Localization;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer;

/// <summary>Просмотрщик через PreviewHanlde интерфейс</summary>
internal class PreviewHandlerViewer : UserControl, IViewer
{
  private const string GuidIshellitem = "43826d1e-e718-42ee-bc55-a1e261c37bfe";
  private object _previewHandler;
  private Guid _previewHandlerGuid;
  private Stream _previewHandlerStream;
  private Control _owner;

  /// <summary>Default constructor</summary>
  public PreviewHandlerViewer()
  {
    this._previewHandlerGuid = Guid.Empty;
    this.BorderStyle = BorderStyle.FixedSingle;
    this.Size = new Size(320, 240 /*0xF0*/);
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this.SetStyle(ControlStyles.UserPaint, true);
  }

  public void Init(Control owner)
  {
    this._owner = owner;
    this._owner.SuspendLayout();
    owner.Controls.Add((Control) this);
    this._owner.Resize += new EventHandler(this._owner_Resize);
    this._owner.ResumeLayout(false);
    this.OnResize();
  }

  public void Open(FileItem fileItemInfo, System.IServiceProvider serviceProvider)
  {
    Guid previewHandlerGuid = this.GetPreviewHandlerGUID(fileItemInfo.FileFullName);
    this._previewHandlerGuid = !(previewHandlerGuid == Guid.Empty) ? previewHandlerGuid : throw new Exception(LocalizationHolder.rm.GetString("NoPreviewAvailable"));
    this._previewHandler = Activator.CreateInstance(System.Type.GetTypeFromCLSID(this._previewHandlerGuid));
    switch (this._previewHandler)
    {
      case IInitializeWithFile initializeWithFile:
        initializeWithFile.Initialize(fileItemInfo.FileFullName, 0U);
        break;
      case IInitializeWithStream initializeWithStream:
        this._previewHandlerStream = (Stream) File.Open(fileItemInfo.FileFullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        ManagedIStream pstream = new ManagedIStream(this._previewHandlerStream);
        initializeWithStream.Initialize((System.Runtime.InteropServices.ComTypes.IStream) pstream, 0U);
        break;
      case IInitializeWithItem initializeWithItem:
        IShellItem ppv;
        Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer.PreviewHandlerViewer.SHCreateItemFromParsingName(fileItemInfo.FileFullName, IntPtr.Zero, new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe"), out ppv);
        IShellItem psi = ppv;
        initializeWithItem.Initialize(psi, 0U);
        break;
    }
    if (this._previewHandler is IPreviewHandler previewHandler)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      try
      {
        previewHandler.SetWindow(this.Handle, ref clientRectangle);
        previewHandler.SetRect(ref clientRectangle);
        previewHandler.DoPreview();
      }
      catch (COMException ex)
      {
        if (ex.ErrorCode == -2147467259 /*0x80004005*/)
        {
          previewHandler.SetRect(ref clientRectangle);
          previewHandler.DoPreview();
        }
        if (ex.ErrorCode == -2042494973)
          throw;
      }
    }
    this.Visible = true;
  }

  public void Close()
  {
    this.Visible = false;
    this.UnloadPreviewHandler();
    this.ReleaseCom();
  }

  public void Clear()
  {
    this.Visible = false;
    this._owner.Resize -= new EventHandler(this._owner_Resize);
    if (this._owner == null || !this._owner.Controls.Contains((Control) this))
      return;
    this._owner.Controls.Remove((Control) this);
  }

  /// <summary>Освобождение ресурсов COM сервера</summary>
  private void ReleaseCom()
  {
    if (this._previewHandler == null)
      return;
    Marshal.ReleaseComObject(this._previewHandler);
    this._previewHandler = (object) null;
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.GetTotalMemory(true);
  }

  /// <summary>Получить guid preview handler-а для файла</summary>
  /// <param name="filename"></param>
  /// <returns></returns>
  private Guid GetPreviewHandlerGUID(string filename)
  {
    return RegistryHelper.GetPreviewHandlerGUID(Path.GetExtension(filename));
  }

  /// <summary>Обработка изменения размера</summary>
  private void OnResize()
  {
    this.Width = this.Parent.Width;
    this.Height = this.Parent.Height;
    if (this._previewHandler is IPreviewHandler previewHandler)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      try
      {
        previewHandler.SetRect(ref clientRectangle);
      }
      catch
      {
      }
    }
    else
      this.Refresh();
  }

  /// <summary>Выгрузим preview handler и закроем файловый поток</summary>
  private void UnloadPreviewHandler()
  {
    this._previewHandlerGuid = new Guid();
    try
    {
      if (this._previewHandler is IPreviewHandler previewHandler)
        previewHandler.Unload();
    }
    catch
    {
    }
    if (this._previewHandlerStream == null)
      return;
    this._previewHandlerStream.Close();
    this._previewHandlerStream = (Stream) null;
  }

  protected override void Dispose(bool disposing)
  {
    this.UnloadPreviewHandler();
    this.ReleaseCom();
    base.Dispose(disposing);
  }

  /// <summary>Изменение размера</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _owner_Resize(object sender, EventArgs e) => this.OnResize();

  [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
  private static extern void SHCreateItemFromParsingName(
    [MarshalAs(UnmanagedType.LPWStr), In] string pszPath,
    [In] IntPtr pbc,
    [MarshalAs(UnmanagedType.LPStruct), In] Guid riid,
    [MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);
}
