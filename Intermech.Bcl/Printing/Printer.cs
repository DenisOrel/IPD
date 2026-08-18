using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.WindowsDll;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;


namespace Intermech.Printing
{
    /// <summary>Информация о принтере</summary>
    public class Printer : IComparable<Printer>, IComparable<string>, IDisposable
    {
      [NotNull]
      private readonly Lazy<PrinterLocationInfo> _locationInfo;
      private readonly IntPtr _pidl;
      [CanBeNull]
      private List<PaperSize> _cachePaperSizes;
      [CanBeNull]
      private List<int> _cachePaperRawKinds;
      [CanBeNull]
      private PageSettings _defaultPageSettings;
      [NotNull]
      private readonly Lazy<Icon> _icon;
      [NotNull]
      private readonly Lazy<Icon> _smallIcon;
      /// <summary>Отображаемое имя принтера</summary>
      [CanBeNull]
      public readonly string DisplayName;
      /// <summary>Фактическое имя принтера (отличается от отображаемого тем, что в)</summary>
      [NotNull]
      public readonly string Name;

      public Printer([NotNull] string name, [CanBeNull] string displayName, IntPtr pidl)
      {
        this.DisplayName = displayName;
        this.Name = name;
        this._pidl = pidl;
        this._locationInfo = new Lazy<PrinterLocationInfo>((Func<PrinterLocationInfo>) (() => new PrinterLocationInfo(this.Name)), false);
        this._icon = new Lazy<Icon>((Func<Icon>) (() => this.GetAnyIcon()), false);
        this._smallIcon = new Lazy<Icon>((Func<Icon>) (() => this.GetAnyIcon(Shell32.SHGFI.SmallIcon)), false);
      }

      public void Dispose()
      {
        if (this._icon.IsValueCreated)
          this._icon.Value.Dispose();
        if (this._smallIcon.IsValueCreated)
          this._smallIcon.Value.Dispose();
        if (!(this._pidl != IntPtr.Zero))
          return;
        Shell32.ILFree(this._pidl);
      }

      private void InitPaperSizes()
      {
        PrinterSettings printerSettings = new PrinterSettings();
        printerSettings.PrinterName = this.Name ?? string.Empty;
        this._cachePaperSizes = printerSettings.PaperSizes.Cast<PaperSize>().ToList(printerSettings.PaperSizes.Count);
        this._cachePaperRawKinds = this._cachePaperSizes.Select((Func<PaperSize, int>) (paperSize => paperSize.RawKind)).ToList(this._cachePaperSizes.Count);
        this._defaultPageSettings = printerSettings.DefaultPageSettings;
      }

      [NotNull]
      private Icon GetAnyIcon(Shell32.SHGFI iconFlag = Shell32.SHGFI.Empty)
      {
        Shell32.SHFILEINFO shfileinfo = new Shell32.SHFILEINFO();
        Shell32.SHGetFileInfo(this._pidl, 0, shfileinfo, Marshal.SizeOf(shfileinfo), Shell32.SHGFI.Icon | Shell32.SHGFI.PIDL | Shell32.SHGFI.AddOverlays | iconFlag);
        Icon anyIcon = (Icon) Icon.FromHandle(shfileinfo.hIcon).Clone();
        Shell32.DestroyIcon(shfileinfo.hIcon);
        return anyIcon;
      }

      /// <summary>Драйвер</summary>
      [CanBeNull]
      public string Driver => this._locationInfo.Value.Driver;

      /// <summary>Порт. Для сетевого например IP адрес, для локального - имя порта, к которому подключен</summary>
      [CanBeNull]
      public string Port => this._locationInfo.Value.Port;

      /// <summary>Порт. Для сетевого например IP адрес, для локального - имя порта, к которому подключен</summary>
      [CanBeNull]
      public string ServerName => this._locationInfo.Value.ServerName;

      /// <summary>Является ли принтером по-умолчанию</summary>
      public bool IsDefault => this.Equals((object) Printers.DefaultPrinterName);

      /// <summary>Список размеров страниц, поддерживаемых принтером</summary>
      [NotNull]
      public List<PaperSize> PaperSizes
      {
        get
        {
          if (this._cachePaperSizes == null)
            this.InitPaperSizes();
          return this._cachePaperSizes ?? throw new InvalidOperationException();
        }
      }

      /// <summary>Список типов страниц, поддерживаемых принтером</summary>
      [NotNull]
      public List<int> PaperRawKinds
      {
        get
        {
          if (this._cachePaperRawKinds == null)
            this.InitPaperSizes();
          return this._cachePaperRawKinds ?? throw new InvalidOperationException();
        }
      }

      /// <summary>Настройки страницы по-умолчанию у данного принтера</summary>
      [CanBeNull]
      public PageSettings DefaultPageSettings
      {
        get
        {
          if (this._defaultPageSettings == null)
            this.InitPaperSizes();
          return this._defaultPageSettings;
        }
      }

      /// <summary>Размер страницы по-умолчанию у данного принтера</summary>
      [CanBeNull]
      public PaperSize DefaultPaperSize => this.DefaultPageSettings?.PaperSize;

      /// <summary>Размер страницы по-умолчанию у данного принтера</summary>
      public int? DefaultPaperRawKind => this.DefaultPageSettings?.PaperSize?.RawKind;

      /// <summary>Тип страницы по-умолчанию у данного принтера</summary>
      public PaperKind? DefaultPaperKind => this.DefaultPageSettings?.PaperSize?.Kind;

      /// <summary>Иконка принтера (32x32)</summary>
      [CanBeNull]
      public Icon Icon => this._icon.Value;

      /// <summary>Маленькая иконка принтера (16x16)</summary>
      [CanBeNull]
      public Icon SmallIcon => this._smallIcon.Value;

      /// <summary>Показать диалог настройки принтера</summary>
      /// <returns>true закрыт по ok, false - в иных случаях.
      /// Если диалог закрыт по ok. то настройки страницы надо перечитать из DefaultPageSettings принтера</returns>
      public bool ShowPropertiesDialog(IntPtr hwnd, [CanBeNull] PaperSize paperSize, bool landscape)
      {
        if (!Printers.OpenPrinterPropertiesDialog(new PrinterSettings()
        {
          PrinterName = this.Name ?? string.Empty,
          DefaultPageSettings = {
            Landscape = landscape,
            PaperSize = paperSize ?? new PaperSize()
          }
        }, hwnd))
          return false;
        this._defaultPageSettings = (PageSettings) null;
        return true;
      }

      /// <summary>Получить размер страницы по типу страницы</summary>
      [CanBeNull]
      public PaperSize GetPaperSizeByPaperKind(PaperKind paperKind)
      {
        return this.PaperSizes.FirstOrDefault((Func<PaperSize, bool>) (paperSize => paperSize != null && paperSize.Kind == paperKind));
      }

      public override int GetHashCode() => this.Name == null ? 0 : this.Name.ToLower().GetHashCode();

      public override bool Equals([CanBeNull] object obj)
      {
        switch (obj)
        {
          case null:
            return false;
          case Printer printer:
            return string.Equals(this.Name, printer.Name, StringComparison.InvariantCultureIgnoreCase) || string.Equals(this.DisplayName, printer.DisplayName, StringComparison.InvariantCultureIgnoreCase);
          case string b:
            return string.Equals(this.Name, b, StringComparison.InvariantCultureIgnoreCase) || string.Equals(this.DisplayName, b, StringComparison.InvariantCultureIgnoreCase);
          default:
            return false;
        }
      }

      public override string ToString() => this.DisplayName ?? string.Empty;

      public int CompareTo([CanBeNull] Printer other)
      {
        return other == null ? 1 : string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
      }

      public int CompareTo([CanBeNull] string other)
      {
        return string.Compare(this.Name, other, StringComparison.OrdinalIgnoreCase);
      }

      public struct PrinterLocationInfo
      {
        [CanBeNull]
        public readonly string Port;
        [CanBeNull]
        public readonly string Driver;
        [CanBeNull]
        public readonly string ServerName;

        public PrinterLocationInfo([NotNull] string printerName)
        {
          SafePrinterHandle safePrinterHandle = new SafePrinterHandle(printerName);
          try
          {
            if (safePrinterHandle.PrinterInfo2 != null)
            {
              this.Port = safePrinterHandle.PrinterInfo2.PortName;
              this.Driver = safePrinterHandle.PrinterInfo2.DriverName;
              this.ServerName = safePrinterHandle.PrinterInfo2.ServerName;
            }
            else
            {
              this.Port = (string) null;
              this.Driver = (string) null;
              this.ServerName = (string) null;
            }
          }
          finally
          {
            safePrinterHandle.Close();
          }
        }
      }
    }
}
