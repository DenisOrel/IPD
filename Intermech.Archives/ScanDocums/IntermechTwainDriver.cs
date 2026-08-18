// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.IntermechTwainDriver
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.ScanDocums;

/// <summary>Драйвер сканера (TWAIN)</summary>
public class IntermechTwainDriver
{
  private const short CountryUSA = 1;
  private const short LanguageUSA = 13;
  /// <summary>
  /// Перечень используемых кодеков (кодеки которые знает GDI+)
  /// </summary>
  private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
  /// <summary>шапка изображения</summary>
  private BITMAPINFOHEADER bmi;
  private IntPtr hwnd;
  private TwIdentity _appid;
  private TwIdentity srcds;
  private TwEvent evtmsg;
  private IntermechTwainDriver.WINMSG winmsg;

  internal TwIdentity appid
  {
    get => this._appid;
    set => this._appid = value;
  }

  /// <summary>
  /// Конструктор.
  /// Драйвер сканера (TWAIN)
  /// </summary>
  public IntermechTwainDriver()
  {
    this.appid = new TwIdentity();
    this.appid.Id = IntPtr.Zero;
    this.appid.Version.MajorNum = (short) 1;
    this.appid.Version.MinorNum = (short) 1;
    this.appid.Version.Language = (short) 13;
    this.appid.Version.Country = (short) 1;
    this.appid.Version.Info = "intermechTWAIN v1.";
    this.appid.ProtocolMajor = (short) 1;
    this.appid.ProtocolMinor = (short) 9;
    this.appid.SupportedGroups = 3;
    this.appid.Manufacturer = "Intermech";
    this.appid.ProductFamily = "PLM";
    this.appid.ProductName = "Intermech IPS";
    this.srcds = new TwIdentity();
    this.srcds.Id = IntPtr.Zero;
    this.evtmsg.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntermechTwainDriver.WINMSG>(this.winmsg));
  }

  /// <summary>Деструктор</summary>
  ~IntermechTwainDriver() => Marshal.FreeHGlobal(this.evtmsg.EventPtr);

  /// <summary>
  /// Получение данных файла изображения (в указаном формате)
  /// </summary>
  /// <param name="img">IntPtr изображения</param>
  /// <param name="fileImgFormat">формат файла</param>
  /// <returns>массив байт, в случае неудачи - null</returns>
  public byte[] GetImageData(IntPtr img, string fileImgFormat)
  {
    IntPtr num = IntermechTwainDriver.GlobalLock(img);
    IntPtr pixelInfo = this.GetPixelInfo(num);
    IntermechTwainDriver.GdipCreateBitmapFromGdiDib(num, pixelInfo, ref img);
    Guid codecClsid = this.GetCodecClsid(fileImgFormat);
    if (codecClsid.Equals(Guid.Empty))
      throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_95"), (object) fileImgFormat));
    string empty = string.Empty;
    string tempFileName;
    try
    {
      tempFileName = Path.GetTempFileName();
    }
    catch (IOException ex)
    {
      throw ex;
    }
    IntermechTwainDriver.GdipSaveImageToFile(img, tempFileName, ref codecClsid, IntPtr.Zero);
    byte[] imageData = File.ReadAllBytes(tempFileName);
    try
    {
      File.Delete(tempFileName);
    }
    catch
    {
    }
    return imageData;
  }

  /// <summary>Освобождение изображения</summary>
  /// <param name="img">IntPtr изображения</param>
  public void FreeImage(IntPtr img) => IntermechTwainDriver.GlobalFree(img);

  /// <summary>Инициализация драйвера</summary>
  /// <param name="hwndp"></param>
  public void Init(IntPtr hwndp)
  {
    this.Finish();
    if (IntermechTwainDriver.DSMparent(this.appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwndp) != TwRC.Success)
      return;
    if (IntermechTwainDriver.DSMident(this.appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, this.srcds) == TwRC.Success)
    {
      this.hwnd = hwndp;
    }
    else
    {
      int num = (int) IntermechTwainDriver.DSMparent(this.appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp);
    }
  }

  /// <summary>Выбрать сканирующее устройство/драйвер</summary>
  public void Select()
  {
    this.CloseSrc();
    if (this.appid.Id == IntPtr.Zero)
    {
      this.Init(this.hwnd);
      if (this.appid.Id == IntPtr.Zero)
        throw new Exception(ServiceHolder.rm.GetString("Archives_96"));
    }
    int num = (int) IntermechTwainDriver.DSMident(this.appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, this.srcds);
  }

  /// <summary>Сканировать</summary>
  public void Acquire()
  {
    this.CloseSrc();
    if (this.appid.Id == IntPtr.Zero)
    {
      this.Init(this.hwnd);
      if (this.appid.Id == IntPtr.Zero)
        throw new Exception(ServiceHolder.rm.GetString("Archives_96"));
    }
    if (IntermechTwainDriver.DSMident(this.appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, this.srcds) != TwRC.Success)
      return;
    if (IntermechTwainDriver.DScap(this.appid, this.srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, new TwCapability(TwCap.XferCount, (short) 1)) != TwRC.Success)
    {
      this.CloseSrc();
    }
    else
    {
      if (IntermechTwainDriver.DSuserif(this.appid, this.srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, new TwUserInterface()
      {
        ShowUI = (short) 1,
        ModalUI = (short) 1,
        ParentHand = this.hwnd
      }) == TwRC.Success)
        return;
      this.CloseSrc();
    }
  }

  /// <summary>Получение изображений у сканера</summary>
  /// <returns></returns>
  public ArrayList TransferPictures()
  {
    ArrayList arrayList = new ArrayList();
    if (this.srcds.Id == IntPtr.Zero)
      return arrayList;
    IntPtr zero1 = IntPtr.Zero;
    TwPendingXfers pxfr = new TwPendingXfers();
    do
    {
      pxfr.Count = (short) 0;
      IntPtr zero2 = IntPtr.Zero;
      if (IntermechTwainDriver.DSiinf(this.appid, this.srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, new TwImageInfo()) != TwRC.Success)
      {
        this.CloseSrc();
        return arrayList;
      }
      if (IntermechTwainDriver.DSixfer(this.appid, this.srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref zero2) != TwRC.XferDone)
      {
        this.CloseSrc();
        return arrayList;
      }
      if (IntermechTwainDriver.DSpxfer(this.appid, this.srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr) != TwRC.Success)
      {
        this.CloseSrc();
        return arrayList;
      }
      arrayList.Add((object) zero2);
    }
    while (pxfr.Count != (short) 0);
    int num = (int) IntermechTwainDriver.DSpxfer(this.appid, this.srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
    return arrayList;
  }

  /// <summary>
  /// Обработка сообщений (интерплетируем сообщения на свой лад)
  /// </summary>
  /// <param name="m"></param>
  /// <returns></returns>
  public TwainCommand PassMessage(ref Message m)
  {
    if (this.srcds.Id == IntPtr.Zero)
      return TwainCommand.Not;
    int messagePos = IntermechTwainDriver.GetMessagePos();
    this.winmsg.hwnd = m.HWnd;
    this.winmsg.message = m.Msg;
    this.winmsg.wParam = m.WParam;
    this.winmsg.lParam = m.LParam;
    this.winmsg.time = IntermechTwainDriver.GetMessageTime();
    this.winmsg.x = (int) (short) messagePos;
    this.winmsg.y = (int) (short) (messagePos >> 16 /*0x10*/);
    Marshal.StructureToPtr<IntermechTwainDriver.WINMSG>(this.winmsg, this.evtmsg.EventPtr, false);
    this.evtmsg.Message = (short) 0;
    switch (IntermechTwainDriver.DSevent(this.appid, this.srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref this.evtmsg))
    {
      case TwRC.Failure:
        return TwainCommand.CloseOk;
      case TwRC.NotDSEvent:
        return TwainCommand.Not;
      default:
        if (this.evtmsg.Message == (short) 257)
          return TwainCommand.TransferReady;
        if (this.evtmsg.Message == (short) 258)
          return TwainCommand.CloseRequest;
        if (this.evtmsg.Message == (short) 259)
          return TwainCommand.CloseOk;
        return this.evtmsg.Message == (short) 260 ? TwainCommand.DeviceEvent : TwainCommand.Null;
    }
  }

  /// <summary>Закрытие устройства</summary>
  public void CloseSrc()
  {
    if (!(this.srcds.Id != IntPtr.Zero))
      return;
    int num1 = (int) IntermechTwainDriver.DSuserif(this.appid, this.srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, new TwUserInterface());
    int num2 = (int) IntermechTwainDriver.DSMident(this.appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, this.srcds);
  }

  /// <summary>Завершение сканирования</summary>
  public void Finish()
  {
    this.CloseSrc();
    if (this.appid.Id != IntPtr.Zero)
    {
      int num = (int) IntermechTwainDriver.DSMparent(this.appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref this.hwnd);
    }
    this.appid.Id = IntPtr.Zero;
  }

  /// <summary>Получение информации о изображении</summary>
  /// <param name="bmpptr"></param>
  /// <returns></returns>
  protected IntPtr GetPixelInfo(IntPtr bmpptr)
  {
    this.bmi = new BITMAPINFOHEADER();
    Marshal.PtrToStructure<BITMAPINFOHEADER>(bmpptr, this.bmi);
    if (this.bmi.biSizeImage == 0)
      this.bmi.biSizeImage = ((this.bmi.biWidth * (int) this.bmi.biBitCount + 31 /*0x1F*/ & -32) >> 3) * this.bmi.biHeight;
    int num = this.bmi.biClrUsed;
    if (num == 0 && this.bmi.biBitCount <= (short) 8)
      num = 1 << (int) this.bmi.biBitCount;
    return (IntPtr) (num * 4 + this.bmi.biSize + (int) bmpptr);
  }

  /// <summary>Поиск кодека</summary>
  /// <param name="fileImgFormat">расширение файла (пример: .bmp )</param>
  /// <returns>Guid кодека, в случае неудачи - Guid.Empty</returns>
  private Guid GetCodecClsid(string fileImgFormat)
  {
    string str = "*" + fileImgFormat.ToUpper();
    foreach (ImageCodecInfo codec in IntermechTwainDriver.codecs)
    {
      if (codec.FilenameExtension.IndexOf(str) >= 0)
        return codec.Clsid;
    }
    return Guid.Empty;
  }

  [DllImport("gdiplus.dll")]
  internal static extern int GdipDisposeImage(IntPtr image);

  [DllImport("gdiplus.dll")]
  internal static extern int GdipCreateBitmapFromGdiDib(
    IntPtr bminfo,
    IntPtr pixdat,
    ref IntPtr image);

  [DllImport("gdiplus.dll", CharSet = CharSet.Unicode)]
  internal static extern int GdipSaveImageToFile(
    IntPtr image,
    string filename,
    [In] ref Guid clsid,
    IntPtr encparams);

  internal static TwRC DSMparent(
    [In, Out] TwIdentity origin,
    IntPtr zeroptr,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    ref IntPtr refptr)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSMparent(origin, zeroptr, dg, dat, msg, ref refptr) : IntermechTwainDriver.DSM32.DSMparent(origin, zeroptr, dg, dat, msg, ref refptr);
  }

  internal static TwRC DSMident(
    [In, Out] TwIdentity origin,
    IntPtr zeroptr,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    [In, Out] TwIdentity idds)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSMident(origin, zeroptr, dg, dat, msg, idds) : IntermechTwainDriver.DSM32.DSMident(origin, zeroptr, dg, dat, msg, idds);
  }

  internal static TwRC DSMstatus(
    [In, Out] TwIdentity origin,
    IntPtr zeroptr,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    [In, Out] TwStatus dsmstat)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSMstatus(origin, zeroptr, dg, dat, msg, dsmstat) : IntermechTwainDriver.DSM32.DSMstatus(origin, zeroptr, dg, dat, msg, dsmstat);
  }

  internal static TwRC DSuserif(
    [In, Out] TwIdentity origin,
    [In, Out] TwIdentity dest,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    TwUserInterface guif)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSuserif(origin, dest, dg, dat, msg, guif) : IntermechTwainDriver.DSM32.DSuserif(origin, dest, dg, dat, msg, guif);
  }

  internal static TwRC DSevent(
    [In, Out] TwIdentity origin,
    [In, Out] TwIdentity dest,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    ref TwEvent evt)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSevent(origin, dest, dg, dat, msg, ref evt) : IntermechTwainDriver.DSM32.DSevent(origin, dest, dg, dat, msg, ref evt);
  }

  internal static TwRC DSstatus(
    [In, Out] TwIdentity origin,
    [In] TwIdentity dest,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    [In, Out] TwStatus dsmstat)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSstatus(origin, dest, dg, dat, msg, dsmstat) : IntermechTwainDriver.DSM32.DSstatus(origin, dest, dg, dat, msg, dsmstat);
  }

  internal static TwRC DScap(
    [In, Out] TwIdentity origin,
    [In] TwIdentity dest,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    [In, Out] TwCapability capa)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DScap(origin, dest, dg, dat, msg, capa) : IntermechTwainDriver.DSM32.DScap(origin, dest, dg, dat, msg, capa);
  }

  internal static TwRC DSiinf(
    [In, Out] TwIdentity origin,
    [In] TwIdentity dest,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    [In, Out] TwImageInfo imginf)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSiinf(origin, dest, dg, dat, msg, imginf) : IntermechTwainDriver.DSM32.DSiinf(origin, dest, dg, dat, msg, imginf);
  }

  internal static TwRC DSixfer(
    [In, Out] TwIdentity origin,
    [In] TwIdentity dest,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    ref IntPtr hbitmap)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSixfer(origin, dest, dg, dat, msg, ref hbitmap) : IntermechTwainDriver.DSM32.DSixfer(origin, dest, dg, dat, msg, ref hbitmap);
  }

  internal static TwRC DSpxfer(
    [In, Out] TwIdentity origin,
    [In] TwIdentity dest,
    TwDG dg,
    TwDAT dat,
    TwMSG msg,
    [In, Out] TwPendingXfers pxfr)
  {
    return IntermechTwainDriver.Is64Bit ? IntermechTwainDriver.DSM64.DSpxfer(origin, dest, dg, dat, msg, pxfr) : IntermechTwainDriver.DSM32.DSpxfer(origin, dest, dg, dat, msg, pxfr);
  }

  internal static bool Is64Bit => Environment.Is64BitProcess;

  [DllImport("kernel32.dll")]
  internal static extern IntPtr GlobalAlloc(int flags, int size);

  [DllImport("kernel32.dll")]
  internal static extern IntPtr GlobalLock(IntPtr handle);

  [DllImport("kernel32.dll")]
  internal static extern bool GlobalUnlock(IntPtr handle);

  [DllImport("kernel32.dll")]
  internal static extern IntPtr GlobalFree(IntPtr handle);

  [DllImport("user32.dll")]
  private static extern int GetMessagePos();

  [DllImport("user32.dll")]
  private static extern int GetMessageTime();

  [DllImport("gdi32.dll")]
  private static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  private static extern IntPtr CreateDC(
    string szdriver,
    string szdevice,
    string szoutput,
    IntPtr devmode);

  [DllImport("gdi32.dll")]
  private static extern bool DeleteDC(IntPtr hdc);

  private class DSM32
  {
    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSMparent(
      [In, Out] TwIdentity origin,
      IntPtr zeroptr,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      ref IntPtr refptr);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSMident(
      [In, Out] TwIdentity origin,
      IntPtr zeroptr,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwIdentity idds);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSMstatus(
      [In, Out] TwIdentity origin,
      IntPtr zeroptr,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwStatus dsmstat);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSuserif(
      [In, Out] TwIdentity origin,
      [In, Out] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      TwUserInterface guif);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSevent(
      [In, Out] TwIdentity origin,
      [In, Out] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      ref TwEvent evt);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSstatus(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwStatus dsmstat);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DScap(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwCapability capa);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSiinf(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwImageInfo imginf);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSixfer(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      ref IntPtr hbitmap);

    [DllImport("twain_32.dll", EntryPoint = "#1")]
    internal static extern TwRC DSpxfer(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwPendingXfers pxfr);
  }

  private class DSM64
  {
    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSMparent(
      [In, Out] TwIdentity origin,
      IntPtr zeroptr,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      ref IntPtr refptr);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSMident(
      [In, Out] TwIdentity origin,
      IntPtr zeroptr,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwIdentity idds);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSMstatus(
      [In, Out] TwIdentity origin,
      IntPtr zeroptr,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwStatus dsmstat);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSuserif(
      [In, Out] TwIdentity origin,
      [In, Out] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      TwUserInterface guif);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSevent(
      [In, Out] TwIdentity origin,
      [In, Out] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      ref TwEvent evt);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSstatus(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwStatus dsmstat);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DScap(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwCapability capa);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSiinf(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwImageInfo imginf);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSixfer(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      ref IntPtr hbitmap);

    [DllImport("TWAINDSM.dll", EntryPoint = "#1")]
    internal static extern TwRC DSpxfer(
      [In, Out] TwIdentity origin,
      [In] TwIdentity dest,
      TwDG dg,
      TwDAT dat,
      TwMSG msg,
      [In, Out] TwPendingXfers pxfr);
  }

  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct WINMSG
  {
    public IntPtr hwnd;
    public int message;
    public IntPtr wParam;
    public IntPtr lParam;
    public int time;
    public int x;
    public int y;
  }
}
