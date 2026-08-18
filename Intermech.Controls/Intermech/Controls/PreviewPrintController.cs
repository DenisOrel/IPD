
// Type: Intermech.Controls.PreviewPrintController
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Runtime.InteropServices;


namespace Intermech.Controls;

/// <summary>
/// 
/// </summary>
public class PreviewPrintController : PrintController
{
  private IntPtr _handle;
  private IntPtr _dc;
  private Graphics _graphics;
  private IList _list = (IList) new ArrayList();
  private PreviewPrintControl _previewPrintControl;
  private bool _antiAlias;

  public PreviewPrintController(PreviewPrintControl previewPrintControl)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<PreviewPrintControl>(previewPrintControl, nameof (previewPrintControl));
    this._previewPrintControl = previewPrintControl;
  }

  /// <summary>
  /// 
  /// </summary>
  public virtual bool UseAntiAlias
  {
    get => this._antiAlias;
    set => this._antiAlias = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="hdevmode"></param>
  /// <param name="document"></param>
  /// <returns></returns>
  internal IntPtr CreateIC(IntPtr hdevmode, PrintDocument document)
  {
    IntPtr hdevnames = document.PrinterSettings.GetHdevnames();
    string lpszDriverName = PreviewPrintController.ReadOneDEVNAME(PreviewPrintController.GlobalLock(new HandleRef((object) null, hdevnames)), 0);
    PreviewPrintController.GlobalUnlock(new HandleRef((object) null, hdevnames));
    IntPtr handle = PreviewPrintController.GlobalLock(new HandleRef((object) null, hdevmode));
    string printerName = document.PrinterSettings.PrinterName;
    HandleRef lpInitData = new HandleRef((object) null, handle);
    IntPtr ic = PreviewPrintController.CreateIC(lpszDriverName, printerName, (string) null, lpInitData);
    PreviewPrintController.GlobalUnlock(new HandleRef((object) null, hdevmode));
    return ic;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="document"></param>
  /// <param name="e"></param>
  public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
  {
    this.CheckSecurity();
    this._graphics.Dispose();
    this._graphics = (Graphics) null;
    base.OnEndPage(document, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="document"></param>
  /// <param name="e"></param>
  public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
  {
    this.CheckSecurity();
    PreviewPrintController.ReleaseDC(IntPtr.Zero, this._dc);
    this._dc = IntPtr.Zero;
    base.OnEndPrint(document, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="document"></param>
  /// <param name="e"></param>
  /// <returns></returns>
  public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
  {
    this.CheckSecurity();
    base.OnStartPage(document, e);
    e.PageSettings.CopyToHdevmode(this._handle);
    Size size1 = this._previewPrintControl.ShowOnlyPrintableArea ? Size.Round(e.PageSettings.PrintableArea.Size) : new Size(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
    Size physicalSize = size1;
    if (e.PageSettings.Landscape)
    {
      physicalSize.Width = physicalSize.Height;
      physicalSize.Height = size1.Width;
    }
    Size size2 = PrinterUnitConvert.Convert(physicalSize, PrinterUnit.Display, PrinterUnit.HundredthsOfAMillimeter);
    Metafile metafile = new Metafile(this._dc, new Rectangle(0, 0, size2.Width, size2.Height));
    this._list.Add((object) new ExtendedPreviewPageInfo((Image) metafile, physicalSize, e.PageSettings.Margins, this._previewPrintControl.ShowOnlyPrintableArea ? Rectangle.Empty : Rectangle.Round(e.PageSettings.PrintableArea)));
    this._graphics = Graphics.FromImage((Image) metafile);
    if (this._antiAlias)
    {
      this._graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      this._graphics.SmoothingMode = SmoothingMode.AntiAlias;
    }
    return this._graphics;
  }

  /// <summary>
  /// 
  /// </summary>
  public override bool IsPreview => true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="document"></param>
  /// <param name="e"></param>
  public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
  {
    this.CheckSecurity();
    base.OnStartPrint(document, e);
    if (!document.PrinterSettings.IsValid)
      throw new InvalidPrinterException(document.PrinterSettings);
    this._handle = document.PrinterSettings.GetHdevmode(document.DefaultPageSettings);
    this._dc = this.CreateIC(this._handle, document);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CheckSecurity()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public ExtendedPreviewPageInfo[] GetPreviewPageInfo()
  {
    this.CheckSecurity();
    ExtendedPreviewPageInfo[] previewPageInfo = new ExtendedPreviewPageInfo[this._list.Count];
    this._list.CopyTo((Array) previewPageInfo, 0);
    return previewPageInfo;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pDevnames"></param>
  /// <param name="slot"></param>
  /// <returns></returns>
  private static string ReadOneDEVNAME(IntPtr pDevnames, int slot)
  {
    int num = Marshal.SystemDefaultCharSize * (int) Marshal.ReadInt16((IntPtr) ((long) pDevnames + (long) (slot * 2)));
    return Marshal.PtrToStringAuto((IntPtr) ((long) pDevnames + (long) num));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="handle"></param>
  /// <returns></returns>
  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr GlobalLock(HandleRef handle);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="lpszDriverName"></param>
  /// <param name="lpszDeviceName"></param>
  /// <param name="lpszOutput"></param>
  /// <param name="lpInitData"></param>
  /// <returns></returns>
  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr CreateIC(
    string lpszDriverName,
    string lpszDeviceName,
    string lpszOutput,
    HandleRef lpInitData);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="handle"></param>
  /// <returns></returns>
  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  private static extern bool GlobalUnlock(HandleRef handle);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="hWnd"></param>
  /// <param name="hDC"></param>
  /// <returns></returns>
  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
}
