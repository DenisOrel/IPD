// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.GlobalIndex.DwgDxfFileConverter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.IO;
using System.Runtime.InteropServices;


namespace Intermech.Kernel.GlobalIndex;

public class DwgDxfFileConverter : CustomFileConverter
{
  public static string[] _TextFilesExtensions = new string[3]
  {
    ".DWG",
    ".DXF",
    ".DXB"
  };
  private static object _syncRoot = new object();

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

  public override string Caption => LocalizationHolder.rm.GetString(nameof (DwgDxfFileConverter));

  public override string[] SupportedFileExtensions => DwgDxfFileConverter._TextFilesExtensions;

  public override string GetPlainText(IDBAttribute attribute)
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
    IAppServerFilesCache service = ServerServices.GetService(typeof (IAppServerFilesCache)) as IAppServerFilesCache;
    string isolatedFileName = (attribute as DBStorageAttribute)._FileStruct.GetIsolatedFileName(service.FStorage);
    string empty = string.Empty;
    string tempFileName = Path.GetTempFileName();
    string str = tempFileName + Path.GetExtension(blobInformation.FileName);
    try
    {
      if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
      {
        using (FileStream inStream = new FileStream(isolatedFileName, FileMode.Open))
        {
          using (FileStream outStream = new FileStream(str, FileMode.Create))
            ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((Stream) outStream, (Stream) inStream);
        }
      }
      else
      {
        using (FileStream fileStream1 = new FileStream(isolatedFileName, FileMode.Open))
        {
          using (FileStream fileStream2 = new FileStream(str, FileMode.Create))
          {
            byte[] buffer = new byte[32768 /*0x8000*/];
            for (int count = fileStream1.Read(buffer, 0, buffer.Length); count > 0; count = fileStream1.Read(buffer, 0, buffer.Length))
            {
              fileStream2.Write(buffer, 0, count);
              if (count < buffer.Length)
                break;
            }
          }
        }
      }
      return this.GetTexts(str);
    }
    finally
    {
      if (File.Exists(tempFileName))
        File.Delete(tempFileName);
      if (File.Exists(str))
        File.Delete(str);
    }
  }

  internal static class ShowDll_Text
  {
    private static readonly bool IsWin32 = IntPtr.Size == 4;
    private const string DllName_x86 = "ShowIPSx86.dll";
    private const string DllName_x64 = "ShowIPSx64.dll";

    public static void ShowToTrace([MarshalAs(UnmanagedType.LPWStr)] string message)
    {
      if (message == null || !(ServerServices.GetService(typeof (IOutputView)) is IOutputView service))
        return;
      service.WriteString("Ошибки", message);
    }

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

    [DllImport("ShowIPSx86.dll", EntryPoint = "Open_DWG_Files", CharSet = CharSet.Ansi)]
    private static extern void Open_Dwg_Files_x86(string fileName, out short code);

    [DllImport("ShowIPSx86.dll", EntryPoint = "Close_DWG_Files")]
    private static extern void Close_Dwg_Files_x86();

    [DllImport("ShowIPSx86.dll", EntryPoint = "GetAllText", CharSet = CharSet.Ansi)]
    private static extern void GetAllText_x86(short codeAtrib, out IntPtr buf);

    [DllImport("ShowIPSx64.dll", EntryPoint = "Open_DWG_Files", CharSet = CharSet.Ansi)]
    private static extern void Open_Dwg_Files_x64(string fileName, out short code);

    [DllImport("ShowIPSx64.dll", EntryPoint = "Close_DWG_Files")]
    private static extern void Close_Dwg_Files_x64();

    [DllImport("ShowIPSx64.dll", EntryPoint = "GetAllText", CharSet = CharSet.Ansi)]
    private static extern void GetAllText_x64(short codeAtrib, out IntPtr buf);

    public delegate void CallbackLogHelperFunc([MarshalAs(UnmanagedType.LPWStr)] string message);
  }
}
