
// Type: Intermech.Kernel.GlobalIndex_.DwgDxfFileConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;


namespace Intermech.Kernel.GlobalIndex_;

/// <summary>Конвертер различных текстовых форматов через ShowIPS.DLL</summary>
public class DwgDxfFileConverter
{
  public static string[] _TextFilesExtensions = new string[3]
  {
    ".DWG",
    ".DXF",
    ".DXB"
  };
  /// <summary>Объект для синхронизации</summary>
  private static object _syncRoot = new object();

  /// <summary>чтение из файла</summary>
  /// <param name="unpackFileNam">путь и имя файла</param>
  /// <returns>текст из файла</returns>
  private string GetTexts(string unpackFileNam)
  {
    lock (DwgDxfFileConverter._syncRoot)
    {
      short code = 0;
      DwgDxfFileConverter.ShowDll_Text.Open_Dwg_Files(unpackFileNam, out code);
      if (code != (short) 0)
        return string.Empty;
      string allText = DwgDxfFileConverter.ShowDll_Text.GetAllText((short) 1);
      DwgDxfFileConverter.ShowDll_Text.GetAllText((short) -1);
      DwgDxfFileConverter.ShowDll_Text.Close_Dwg_Files();
      return allText;
    }
  }

  public string Caption => LocalizationHolder.rm.GetString(nameof (DwgDxfFileConverter));

  public string[] SupportedFileExtensions => DwgDxfFileConverter._TextFilesExtensions;

  public string GetPlainText(IDBAttribute attribute)
  {
    IBlobReader blobReader = attribute as IBlobReader;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      if (blobInformation.PackedFileSize == 0L)
        return string.Empty;
    }
    finally
    {
      blobReader.CloseBlob();
    }
    string path = "";
    string str = Path.GetTempFileName() + Path.GetExtension(blobInformation.FileName);
    if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
    {
      using (IsolatedStorageFileStream inStream = new IsolatedStorageFileStream(path, FileMode.Open))
      {
        using (FileStream outStream = new FileStream(str, FileMode.Create))
          ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
      }
    }
    else
    {
      using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream(path, FileMode.Open))
      {
        using (FileStream fileStream = new FileStream(str, FileMode.Create))
        {
          byte[] buffer = new byte[32768 /*0x8000*/];
          for (int count = storageFileStream.Read(buffer, 0, buffer.Length); count > 0; count = storageFileStream.Read(buffer, 0, buffer.Length))
          {
            fileStream.Write(buffer, 0, count);
            if (count < buffer.Length)
              break;
          }
        }
      }
    }
    string texts = this.GetTexts(str);
    if (!File.Exists(str))
      return texts;
    File.Delete(str);
    return texts;
  }

  /// <summary>обёртка ShowARX.dll</summary>
  internal static class ShowDll_Text
  {
    private static readonly bool IsWin32 = IntPtr.Size == 4;
    private const string DllName_x86 = "ShowIPSx86.dll";
    private const string DllName_x64 = "ShowIPSx64.dll";

    /// <summary>Показать сообщение об ошибке из DLL</summary>
    /// <param name="message">сообщение об ошибке из ShowIPSx86.DLL, ShowIPSx64.DLL и ShowARX.DLL</param>
    public static void ShowToTrace([MarshalAs(UnmanagedType.LPWStr)] string message)
    {
      if (message == null || !(ServicesManager.GetService(typeof (IOutputView)) is IOutputView service))
        return;
      service.WriteString("Ошибки", message);
    }

    /// <summary>Статический конструктор</summary>
    static ShowDll_Text()
    {
      try
      {
        DwgDxfFileConverter.ShowDll_Text.RegisterCallbackLogHelper(new DwgDxfFileConverter.ShowDll_Text.CallbackLogHelperFunc(DwgDxfFileConverter.ShowDll_Text.ShowToTrace));
      }
      catch (Exception ex)
      {
      }
    }

    [DllImport("ShowIPSx86.dll", EntryPoint = "RegisterCallbackLogHelper")]
    private static extern void RegisterCallbackLogHelper_x86(
      DwgDxfFileConverter.ShowDll_Text.CallbackLogHelperFunc pfn);

    [DllImport("ShowIPSx64.dll", EntryPoint = "RegisterCallbackLogHelper")]
    private static extern void RegisterCallbackLogHelper_x64(
      DwgDxfFileConverter.ShowDll_Text.CallbackLogHelperFunc pfn);

    internal static void RegisterCallbackLogHelper(
      DwgDxfFileConverter.ShowDll_Text.CallbackLogHelperFunc pfn)
    {
      if (DwgDxfFileConverter.ShowDll_Text.IsWin32)
        DwgDxfFileConverter.ShowDll_Text.RegisterCallbackLogHelper_x86(pfn);
      else
        DwgDxfFileConverter.ShowDll_Text.RegisterCallbackLogHelper_x64(pfn);
    }

    /// <summary>имя DLL</summary>
    internal static string DLLName
    {
      get => !DwgDxfFileConverter.ShowDll_Text.IsWin32 ? "ShowIPSx64.dll" : "ShowIPSx86.dll";
    }

    internal static void Open_Dwg_Files(string fileName, out short code)
    {
      if (DwgDxfFileConverter.ShowDll_Text.IsWin32)
        DwgDxfFileConverter.ShowDll_Text.Open_Dwg_Files_x86(fileName, out code);
      else
        DwgDxfFileConverter.ShowDll_Text.Open_Dwg_Files_x64(fileName, out code);
    }

    /// <summary>закрыть файл и освободить все связи с чертежом</summary>
    internal static void Close_Dwg_Files()
    {
      if (DwgDxfFileConverter.ShowDll_Text.IsWin32)
        DwgDxfFileConverter.ShowDll_Text.Close_Dwg_Files_x86();
      else
        DwgDxfFileConverter.ShowDll_Text.Close_Dwg_Files_x64();
    }

    internal static string GetAllText(short codeAtrib)
    {
      IntPtr buf;
      if (DwgDxfFileConverter.ShowDll_Text.IsWin32)
        DwgDxfFileConverter.ShowDll_Text.GetAllText_x86(codeAtrib, out buf);
      else
        DwgDxfFileConverter.ShowDll_Text.GetAllText_x64(codeAtrib, out buf);
      return Marshal.PtrToStringAnsi(buf);
    }

    /// <summary>начать чтение файла</summary>
    /// <param name="fileName">путь и имя файла</param>
    /// <param name="code">код завершения чтения</param>
    [DllImport("ShowIPSx86.dll", EntryPoint = "Open_DWG_Files", CharSet = CharSet.Ansi)]
    private static extern void Open_Dwg_Files_x86(string fileName, out short code);

    /// <summary>закрыть файл и освободить все связи с чертежом</summary>
    [DllImport("ShowIPSx86.dll", EntryPoint = "Close_DWG_Files")]
    private static extern void Close_Dwg_Files_x86();

    /// <summary>чтение из файла</summary>
    /// <param name="codeAtrib">признак -1 очистить ,1 и аттрибуты тоже</param>
    /// <param name="buf">текст из файла</param>
    [DllImport("ShowIPSx86.dll", EntryPoint = "GetAllText", CharSet = CharSet.Ansi)]
    private static extern void GetAllText_x86(short codeAtrib, out IntPtr buf);

    /// <summary>начать чтение файла</summary>
    /// <param name="fileName">путь и имя файла</param>
    /// <param name="code">код завершения чтения</param>
    [DllImport("ShowIPSx64.dll", EntryPoint = "Open_DWG_Files", CharSet = CharSet.Ansi)]
    private static extern void Open_Dwg_Files_x64(string fileName, out short code);

    /// <summary>закрыть файл и освободить все связи с чертежом</summary>
    [DllImport("ShowIPSx64.dll", EntryPoint = "Close_DWG_Files")]
    private static extern void Close_Dwg_Files_x64();

    /// <summary>чтение из файла</summary>
    /// <param name="codeAtrib">признак -1 очистить ,1 и аттрибуты тоже</param>
    /// <param name="buf">текст из файла</param>
    [DllImport("ShowIPSx64.dll", EntryPoint = "GetAllText", CharSet = CharSet.Ansi)]
    private static extern void GetAllText_x64(short codeAtrib, out IntPtr buf);

    /// <summary>получить сообщение об ошибке из DLL</summary>
    /// <param name="message">сообщение об ошибке из ShowIPSx86.DLL, ShowIPSx64.DLL и ShowARX.DLL</param>
    public delegate void CallbackLogHelperFunc([MarshalAs(UnmanagedType.LPWStr)] string message);
  }
}
