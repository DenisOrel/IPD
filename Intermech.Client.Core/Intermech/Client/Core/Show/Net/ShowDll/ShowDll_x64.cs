
// Type: Intermech.Client.Core.Show.Net.ShowDll.ShowDll_x64
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary>вызовы ShowIPSx64.dll win x64</summary>
internal static class ShowDll_x64
{
  internal const string DllName = "ShowIPSx64.dll";

  [DllImport("ShowIPSx64.dll", EntryPoint = "RegisterCallbackLogHelper")]
  internal static extern void RegisterCallbackLogHelper_Import(CallbackLogHelperFunc pfn);

  /// <summary>Получить версию  DLL (Net стиль) </summary>
  /// <param name="versionNet">текущая версия подключаемого Net DLL</param>
  /// <returns>возвращает версию  DLL (Net стиль)</returns>
  [DllImport("ShowIPSx64.dll", EntryPoint = "CheckVersionNet")]
  internal static extern int CheckVersionNet_Import(int versionNet);

  /// <summary>Получить версию DLL</summary>
  /// <param name="version">возвращает версию DLL</param>
  [DllImport("ShowIPSx64.dll", EntryPoint = "CheckVersion")]
  internal static extern void CheckVersion_Import(ref short version);

  /// <summary>начать чтение файла</summary>
  /// <param name="fileName">путь и имя файла</param>
  /// <param name="lenfileDwgdata">длинна файла</param>
  /// <param name="fileDwgdata">содержимое файла</param>
  /// <param name="defaultWeight">толщина линий по умолчанию</param>
  /// <param name="fun">CallBack-функкция для получения внешних файлов</param>
  /// <param name="code">код завершения чтения</param>
  [DllImport("ShowIPSx64.dll", EntryPoint = "Open_Dwg_Net", CharSet = CharSet.Ansi)]
  internal static extern void Open_Dwg_Net_Import(
    string fileName,
    int lenfileDwgdata,
    byte[] fileDwgdata,
    float defaultWeight,
    ref FindFileDelegate fun,
    out short code);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Open_DWG_Files", CharSet = CharSet.Ansi)]
  internal static extern void Open_Dwg_Files_Import(string fileName, out short code);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Open_DWG_Handle")]
  internal static extern void Open_Dwg_Handle_Import(
    byte[] fileDwgdata,
    int lenfileDwgdata,
    out short code);

  [DllImport("ShowIPSx64.dll", EntryPoint = "CheckVersionDWG", CharSet = CharSet.Ansi)]
  internal static extern void CheckVersionDwg_Import(
    string fileName,
    out short version,
    out short versionMdt);

  /// <summary>закрыть файл и освободить все связи с чертежом</summary>
  [DllImport("ShowIPSx64.dll", EntryPoint = "Close_DWG_Files")]
  internal static extern void Close_Dwg_Files_Import();

  [DllImport("ShowIPSx64.dll", EntryPoint = "GetMeasurement")]
  internal static extern void GetMeasurement_Import(out short measurement);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Model_State")]
  internal static extern void Get_Model_State_Import(out short numModelState);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Set_Model_State")]
  internal static extern void Set_Model_State_Import(ref short numModelState);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Name_Layout", CharSet = CharSet.Ansi)]
  internal static extern void Get_Name_Layout_Import(ref short indexLayout, out IntPtr buf);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Name_Layer")]
  internal static extern void Get_Name_Layer_Import(
    ref short indexLayer,
    out IntPtr buf,
    out short statusLayer);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Set_Layer_State")]
  internal static extern void Set_Layer_State_Import(ref short indexLayer, ref short statusLayer);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Border_Layer")]
  internal static extern void Get_Border_Layer_Import(
    ref short indexLayer,
    out RectLocal windowBorder);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Gabarit_Layer")]
  internal static extern void Get_Gabarit_Layer_Import(
    ref short indexLayer,
    out double minX,
    out double minY,
    out double maxX,
    out double maxY);

  [DllImport("ShowIPSx64.dll", EntryPoint = "TransferDwg_to_Win")]
  internal static extern void TransferDwg_to_Win_Import(
    double pntX,
    double pntY,
    out PointLocal pnt);

  [DllImport("ShowIPSx64.dll", EntryPoint = "TransferWin_to_Dwg")]
  internal static extern void TransferWin_to_Dwg_Import(
    PointLocal pnt,
    out double pntX,
    out double pntY);

  [DllImport("ShowIPSx64.dll", EntryPoint = "SetDrawDwgWin")]
  internal static extern void SetDrawDwgWin_Import(double x1, double y1, double x2, double y2);

  [DllImport("ShowIPSx64.dll", EntryPoint = "SetWindow_Dwg")]
  internal static extern void SetWindow_Dwg_Import(ref RectLocal windowDraw);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Name_DWG_All", CharSet = CharSet.Ansi)]
  internal static extern void Get_Name_Dwg_All_Import(ref short indexAll, out IntPtr buf);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Name_Image", CharSet = CharSet.Ansi)]
  internal static extern void Get_Name_Image_Import(ref short indexImage, out IntPtr buf);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Name_Block", CharSet = CharSet.Ansi)]
  internal static extern void Get_Name_Block_Import(ref short indexBlock, out IntPtr buf);

  [DllImport("ShowIPSx64.dll", EntryPoint = "SetZoomAll_Dwg")]
  internal static extern void SetZoomAll_Dwg_Import(ref short indexBlock);

  [DllImport("ShowIPSx64.dll", EntryPoint = "StartDrawDwg")]
  internal static extern void StartDrawDwg_Import(out int ratioWndow);

  [DllImport("ShowIPSx64.dll", EntryPoint = "NextDrawDwg")]
  internal static extern void NextDrawDwg_Import(out IntPtr buffer, out int arSize);

  [DllImport("ShowIPSx64.dll", EntryPoint = "StartDrawDwgDouble")]
  internal static extern void StartDrawDwgDouble_Import(out int ratioWndow);

  [DllImport("ShowIPSx64.dll", EntryPoint = "NextDrawDwgDouble")]
  internal static extern void NextDrawDwgDouble_Import(out IntPtr buffer, out int arSize);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Name_ImageAll", CharSet = CharSet.Ansi)]
  internal static extern void Get_Name_ImageAll_Import(ref short indexImageAll, out IntPtr buf);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Get_Name_Block_Current", CharSet = CharSet.Ansi)]
  internal static extern void Get_Name_Block_Current_Import(ref short indexBlock, out IntPtr buf);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Open_ScanTable", CharSet = CharSet.Ansi)]
  internal static extern bool Open_ScanTable_Import(byte[] fileCfgdata);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Open_Scan_Files", CharSet = CharSet.Ansi)]
  internal static extern void Open_Scan_Files_Import(
    string fileCfgName,
    out short lenArrayParameter,
    out short returnCode);

  [DllImport("ShowIPSx64.dll", EntryPoint = "Scaning_DWG")]
  internal static extern void Scaning_Dwg_Import();

  [DllImport("ShowIPSx64.dll", EntryPoint = "Set_Scan_State")]
  internal static extern void Set_Scan_State_Import(short wCod, short lm, short dm);

  [DllImport("ShowIPSx64.dll", EntryPoint = "GetNameParam", CharSet = CharSet.Ansi)]
  internal static extern void GetNameParameter_Import(
    ref short paramIndex,
    StringBuilder nameParameter);

  [DllImport("ShowIPSx64.dll", EntryPoint = "GetParam", CharSet = CharSet.Ansi)]
  internal static extern void GetParameter_Import(ref short paramIndex, StringBuilder dataParameter);

  [DllImport("ShowIPSx64.dll", EntryPoint = "GetAllText", CharSet = CharSet.Ansi)]
  internal static extern void GetAllText_Import(short codeAtrib, out IntPtr buf);

  [DllImport("ShowIPSx64.dll", EntryPoint = "SaveDWGFile", CharSet = CharSet.Ansi)]
  internal static extern void SaveDWGFile_Import(string dwgFilePath);

  [DllImport("ShowIPSx64.dll", EntryPoint = "SetParameter", CharSet = CharSet.Ansi)]
  internal static extern void SetParameter_Import(string name, string value);
}
