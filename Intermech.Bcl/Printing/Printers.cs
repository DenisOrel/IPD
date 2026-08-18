using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Printing
{
    /// <summary>Хелпер для работы с зарегистрированными в системе принтерами
    /// Не потокобезопасен, но оно вроде и не требуется</summary>
    public static class Printers
    {
      [NotNull]
      public static readonly object SyncRoot = new object();
      private static DateTime _lastTimeRefresh;
      private static readonly TimeSpan _needRefreshPeriod = new TimeSpan(0, 0, 10);
      [CanBeNull]
      private static Lazy<List<Printer>> _printers;
      [NotNull]
      private static Dictionary<string, string> _dictDisplayNameToFullName = new Dictionary<string, string>();
      [CanBeNull]
      private static Printer _defaultPrinter;
      [CanBeNull]
      private static Lazy<string> _defaultPrinterName;
      private static IntPtr _hwndOwner = IntPtr.Zero;

      static Printers() => Printers.Refresh(IntPtr.Zero);

      /// <summary>Список зарегистрированных в системе принтеров.
      /// Перед началом блока работы с принтером (напр. при вызове диалога выбора принтера) лучше вызывать метод Refresh - он обновит кэш параметров принтера,
      /// т.к. настройки принтеров пользователь мог изменить с момента первоначально инициализации кэша</summary>
      [NotNull]
      [ItemNotNull]
      public static List<Printer> List
      {
        get => Printers._printers?.Value ?? throw new InvalidOperationException();
      }

      /// <summary>Имя принтера по-умолчанию</summary>
      [CanBeNull]
      public static string DefaultPrinterName
      {
        [DebuggerStepThrough] get
        {
          return Printers._defaultPrinterName != null ? Printers._defaultPrinterName.Value : throw new InvalidOperationException();
        }
      }

      /// <summary>Принтер по-умолчанию в системе</summary>
      [CanBeNull]
      public static Printer DefaultPrinter
      {
        get
        {
          if (Printers._defaultPrinter != null)
            return Printers._defaultPrinter;
          Printers.Refresh(IntPtr.Zero);
          return Printers._defaultPrinter;
        }
      }

      /// <summary>Перечисление имён принтеров, зарегистрированных в системе</summary>
      [NotNull]
      [ItemNotNull]
      public static IEnumerable<string> InstalledPrinterNames
      {
        get => Printers.List.Select((Func<Printer, string>) (printer => printer.Name));
      }

      [NotNull]
      private static List<Printer> CreatePrintersList()
      {
            List<Printer> printersList = new List<Printer>(PrinterSettings.InstalledPrinters.Count);
        Shell32.IShellFolder desktopFolder = Shell32.GetDesktopFolder();
        try
        {
          IntPtr ppidl1;
          if (Shell32.SHGetFolderLocation(Printers._hwndOwner, 4, IntPtr.Zero, 0, out ppidl1) == 0)
          {
            try
            {
              StringBuilder pszBuf = new StringBuilder(260);
              Guid iidIshellFolder = Shell32.IID_IShellFolder;
              IntPtr ppv;
              desktopFolder.BindToObject(ppidl1, IntPtr.Zero, ref iidIshellFolder, out ppv);
              object objectForIunknown = Marshal.GetTypedObjectForIUnknown(ppv, Shell32.ShellFolderType);
              try
              {
                Shell32.IShellFolder shellFolder = (Shell32.IShellFolder) objectForIunknown;
                foreach (string str in PrinterSettings.InstalledPrinters.Cast<string>())
                {
                  IntPtr ppidl2;
                  shellFolder.ParseDisplayName(Printers._hwndOwner, IntPtr.Zero, str, IntPtr.Zero, out ppidl2, IntPtr.Zero);
                  Shell32.STRRET pName;
                  shellFolder.GetDisplayNameOf(ppidl2, Shell32.ESHGDN.SHGDN_NORMAL, out pName);
                  if (Shell32.StrRetToBuf(ref pName, ppidl2, pszBuf, pszBuf.Capacity) == 0)
                  {
                    ppidl2 = Shell32.ILCombine(ppidl1, ppidl2);
                    string displayName = pszBuf.ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                      Printer printer = new Printer(str, displayName, ppidl2);
                      if (printer.IsDefault)
                        Printers._defaultPrinter = printer;
                      printersList.Add(printer);
                    }
                  }
                }
              }
              finally
              {
                if (objectForIunknown != null)
                  Marshal.ReleaseComObject(objectForIunknown);
              }
            }
            finally
            {
              Shell32.ILFree(ppidl1);
            }
          }
        }
        finally
        {
          Marshal.ReleaseComObject((object) desktopFolder);
        }
        if (Printers._defaultPrinter == null && printersList.Count > 0)
          Printers._defaultPrinter = printersList[0];
        return printersList;
      }

      /// <summary>Перечитка кэша информации о принтерах, вызывать например перед открытием диалога с выбором принтера</summary>
      /// <param name="hwndOwner"></param>
      public static void Refresh(IntPtr hwndOwner)
      {
        lock (Printers.SyncRoot)
        {
          if (Printers._printers != null && !(Printers._lastTimeRefresh - DateTime.Now > Printers._needRefreshPeriod))
            return;
          Printers._hwndOwner = hwndOwner;
          try
          {
            Printers._defaultPrinterName = new Lazy<string>((Func<string>) (() =>
            {
              using (PrintDocument printDocument = new PrintDocument())
                return printDocument.PrinterSettings.PrinterName;
            }));
            Printers._defaultPrinter = (Printer) null;
            Lazy<List<Printer>> printers = Printers._printers;
            if ((printers != null ? (printers.IsValueCreated ? 1 : 0) : 0) != 0)
            {
              foreach (Printer printer in Printers._printers.Value)
                printer?.Dispose();
            }
            Printers._printers = new Lazy<List<Printer>>(new Func<List<Printer>>(Printers.CreatePrintersList));
          }
          finally
          {
            Printers._hwndOwner = IntPtr.Zero;
          }
          Printers._lastTimeRefresh = DateTime.Now;
        }
      }

      /// <summary>
      /// Поиск имени принтера, который наиболее похож на тот, чьи параметры переданы в параметры. Так что читаю из
      /// настроек имя принтера, проверяю что у него драйвер, чьё имя передано в параметры, если нет, либо принтера с нужным
      /// именем нет - ищу принтер по драйверу и порту (напр. сетевой путь)
      /// при этом если с нужным драйвером принтер найдется (даже несколько), а вот порт не подойдёт - выбираю первый с нужным драйвером</summary>
      /// <param name="name">Имя принтера</param>
      /// <param name="driver">Драйвер принтера</param>
      /// <param name="port">Порт (напр. LPT для локального или IP адрес для сетевого)</param>
      /// <returns>Имя подходящего принтера, если принтера не найдено - вернёт null</returns>
      [CanBeNull]
      public static string FindActualPrinterName([CanBeNull] string name, [CanBeNull] string driver, [CanBeNull] string port)
      {
        if (!string.IsNullOrWhiteSpace(name))
        {
          if (!Printers.InstalledPrinterNames.Contains(name, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase))
            name = (string) null;
          else if (driver != null)
          {
            if (string.Equals(new Printer.PrinterLocationInfo(name).Driver, driver, StringComparison.InvariantCultureIgnoreCase))
              return name;
            name = (string) null;
          }
          else
            name = (string) null;
        }
        if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(driver))
        {
                List<Printer> list = Printers.List.Where((Func<Printer, bool>) (printer => string.Equals(printer.Driver, driver, StringComparison.InvariantCultureIgnoreCase))).ToList();
          if (list.Count > 0)
          {
            Printer printer1 = list.Count == 1 || string.IsNullOrEmpty(port) ? list[0] : list.Where((Func<Printer, bool>) (printer => printer != null)).FirstOrDefault((Func<Printer, bool>) (printerInfo => printerInfo.Port == port)) ?? list[0];
            if (printer1 != null)
              return printer1.Name;
          }
        }
        return (string) null;
      }

      [CanBeNull]
      public static string FindActualPrinterName([CanBeNull] string driver, [CanBeNull] string port)
      {
        return Printers.FindActualPrinterName((string) null, driver, port);
      }

      public static bool OpenPrinterPropertiesDialog([NotNull] PrinterSettings printerSettings, IntPtr hwnd)
      {
        IntPtr hdevmode = printerSettings.GetHdevmode();
        IntPtr pDevModeInput = Kernel32.GlobalLock_ThrowWinErrors(hdevmode);
        IntPtr num1 = Marshal.AllocHGlobal(Winspool.DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero, pDevModeInput, 0));
        int fMode = 14;
        int num2 = Winspool.DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, num1, pDevModeInput, fMode);
        Kernel32.GlobalUnlock_ThrowWinErrors(hdevmode);
        printerSettings.SetHdevmode(num1);
        printerSettings.DefaultPageSettings.SetHdevmode(num1);
        Kernel32.GlobalFree_ThrowWinErrors(hdevmode);
        Marshal.FreeHGlobal(num1);
        return num2 == 1;
      }

      /// <summary>Получение имени принтера (в коллекции PrinterSettings.InstalledPrinters) по отображаемому имени (в Devices and printers)
      /// TODO Переписать</summary>
      [CanBeNull]
      public static string GetPrinterNameByDisplayName([CanBeNull] string displayName)
      {
        return PrinterSettings.InstalledPrinters.Cast<string>().FirstOrDefault((Func<string, bool>) (printerName =>
        {
          if (printerName == null)
            return false;
          if (printerName == displayName)
            return true;
          SafePrinterHandle safePrinterHandle = new SafePrinterHandle(printerName);
          try
          {
            return displayName != null && safePrinterHandle.PrinterInfo2?.DriverName != null && displayName.Contains(safePrinterHandle.PrinterInfo2.DriverName) && !string.IsNullOrEmpty(safePrinterHandle.PrinterInfo2.ServerName) && safePrinterHandle.PrinterInfo2.ServerName.Length > 2 && displayName.Contains(safePrinterHandle.PrinterInfo2.ServerName.Remove(0, 2));
          }
          finally
          {
            safePrinterHandle.Close();
          }
        }));
      }
    }
}
