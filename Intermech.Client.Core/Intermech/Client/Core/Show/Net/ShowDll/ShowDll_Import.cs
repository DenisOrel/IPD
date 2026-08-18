
// Type: Intermech.Client.Core.Show.Net.ShowDll.ShowDll_Import
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary>обёртка ShowIPS.dll</summary>
public sealed class ShowDll_Import : IShowDll_Import
{
  private static readonly bool IsWin32 = IntPtr.Size == 4;

  /// <summary>имя DLL</summary>
  public string DLLName => !ShowDll_Import.IsWin32 ? "ShowIPSx64.dll" : "ShowIPSx86.dll";

  /// <summary>Получить версию  DLL (Net стиль) </summary>
  /// <param name="versionNet">текущая версия подключаемого Net DLL</param>
  /// <returns>возвращает версию  DLL (Net стиль)</returns>
  public int CheckVersionNet(int versionNet)
  {
    return !ShowDll_Import.IsWin32 ? ShowDll_x64.CheckVersionNet_Import(versionNet) : ShowDll_x86.CheckVersionNet_Import(versionNet);
  }

  public void RegisterCallbackLogHelper(CallbackLogHelperFunc pfn)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.RegisterCallbackLogHelper_Import(pfn);
    else
      ShowDll_x64.RegisterCallbackLogHelper_Import(pfn);
  }

  /// <summary>Получить версию DLL </summary>
  /// <param name="version">возвращает версию DLL</param>
  public void CheckVersion(ref short version)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.CheckVersion_Import(ref version);
    else
      ShowDll_x64.CheckVersion_Import(ref version);
  }

  /// <summary>начать чтение файла</summary>
  /// <param name="fileName">путь и имя файла</param>
  /// <param name="lenfileDwgdata">длинна файла</param>
  /// <param name="fileDwgdata">содержимое файла</param>
  /// <param name="defaultWeight">толщина линий по умолчанию</param>
  /// <param name="fun">CallBack-функкция для получения внешних файлов</param>
  /// <param name="code">код завершения чтения</param>
  public void Open_Dwg_Net(
    string fileName,
    int lenfileDwgdata,
    byte[] fileDwgdata,
    float defaultWeight,
    ref FindFileDelegate fun,
    out short code)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Open_Dwg_Net_Import(fileName, lenfileDwgdata, fileDwgdata, defaultWeight, ref fun, out code);
    else
      ShowDll_x64.Open_Dwg_Net_Import(fileName, lenfileDwgdata, fileDwgdata, defaultWeight, ref fun, out code);
  }

  public void Open_Dwg_Files(string fileName, out short code)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Open_Dwg_Files_Import(fileName, out code);
    else
      ShowDll_x64.Open_Dwg_Files_Import(fileName, out code);
  }

  public void Open_Dwg_Handle(byte[] fileDwgdata, int lenfileDwgdata, out short code)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Open_Dwg_Handle_Import(fileDwgdata, lenfileDwgdata, out code);
    else
      ShowDll_x64.Open_Dwg_Handle_Import(fileDwgdata, lenfileDwgdata, out code);
  }

  public void CheckVersionDwg(string fileName, out short version, out short versionMdt)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.CheckVersionDwg_Import(fileName, out version, out versionMdt);
    else
      ShowDll_x64.CheckVersionDwg_Import(fileName, out version, out versionMdt);
  }

  /// <summary>закрыть файл и освободить все связи с чертежом</summary>
  public void Close_Dwg_Files()
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Close_Dwg_Files_Import();
    else
      ShowDll_x64.Close_Dwg_Files_Import();
  }

  public void GetMeasurement(out short measurement)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.GetMeasurement_Import(out measurement);
    else
      ShowDll_x64.GetMeasurement_Import(out measurement);
  }

  public void Get_Model_State(out short numModelState)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Model_State_Import(out numModelState);
    else
      ShowDll_x64.Get_Model_State_Import(out numModelState);
  }

  public void Set_Model_State(ref short numModelState)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Set_Model_State_Import(ref numModelState);
    else
      ShowDll_x64.Set_Model_State_Import(ref numModelState);
  }

  public string Get_Name_Layout(ref short indexLayout)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Name_Layout_Import(ref indexLayout, out buf);
    else
      ShowDll_x64.Get_Name_Layout_Import(ref indexLayout, out buf);
    return Marshal.PtrToStringAnsi(buf);
  }

  public string Get_Name_Layer(ref short indexLayer, out short statusLayer)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Name_Layer_Import(ref indexLayer, out buf, out statusLayer);
    else
      ShowDll_x64.Get_Name_Layer_Import(ref indexLayer, out buf, out statusLayer);
    return Marshal.PtrToStringAnsi(buf);
  }

  public void Set_Layer_State(ref short indexLayer, ref short statusLayer)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Set_Layer_State_Import(ref indexLayer, ref statusLayer);
    else
      ShowDll_x64.Set_Layer_State_Import(ref indexLayer, ref statusLayer);
  }

  /// <summary>прочитать для слоя границы окна рисования</summary>
  /// <param name="indexLayer">номер слоя</param>
  /// <returns>границы окна рисования</returns>
  public Rectangle Get_Border_Layer(ref short indexLayer)
  {
    RectLocal windowBorder = new RectLocal();
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Border_Layer_Import(ref indexLayer, out windowBorder);
    else
      ShowDll_x64.Get_Border_Layer_Import(ref indexLayer, out windowBorder);
    return new Rectangle(Math.Min(windowBorder.right, windowBorder.left), Math.Min(windowBorder.top, windowBorder.bottom), Math.Abs(windowBorder.right - windowBorder.left), Math.Abs(windowBorder.top - windowBorder.bottom));
  }

  public void Get_Gabarit_Layer(
    ref short indexLayer,
    out double minX,
    out double minY,
    out double maxX,
    out double maxY)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Gabarit_Layer_Import(ref indexLayer, out minX, out minY, out maxX, out maxY);
    else
      ShowDll_x64.Get_Gabarit_Layer_Import(ref indexLayer, out minX, out minY, out maxX, out maxY);
  }

  public Point TransferDwg_to_Win(PointD pnt)
  {
    PointLocal pnt1 = new PointLocal();
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.TransferDwg_to_Win_Import(pnt.X, pnt.Y, out pnt1);
    else
      ShowDll_x64.TransferDwg_to_Win_Import(pnt.X, pnt.Y, out pnt1);
    return new Point(pnt1.x, pnt1.y);
  }

  public PointD TransferWin_to_Dwg(Point pnt)
  {
    PointLocal pnt1 = new PointLocal(pnt.X, pnt.Y);
    double pntX;
    double pntY;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.TransferWin_to_Dwg_Import(pnt1, out pntX, out pntY);
    else
      ShowDll_x64.TransferWin_to_Dwg_Import(pnt1, out pntX, out pntY);
    return new PointD(pntX, pntY);
  }

  public void SetDrawDwgWin(double x1, double y1, double x2, double y2)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.SetDrawDwgWin_Import(x1, y1, x2, y2);
    else
      ShowDll_x64.SetDrawDwgWin_Import(x1, y1, x2, y2);
  }

  public void SetWindow_Dwg(Rectangle windowDraw)
  {
    RectLocal windowDraw1 = new RectLocal(windowDraw);
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.SetWindow_Dwg_Import(ref windowDraw1);
    else
      ShowDll_x64.SetWindow_Dwg_Import(ref windowDraw1);
  }

  public string Get_Name_Dwg_All(ref short indexAll)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Name_Dwg_All_Import(ref indexAll, out buf);
    else
      ShowDll_x64.Get_Name_Dwg_All_Import(ref indexAll, out buf);
    return Marshal.PtrToStringAnsi(buf);
  }

  public string Get_Name_Image(ref short indexImage)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Name_Image_Import(ref indexImage, out buf);
    else
      ShowDll_x64.Get_Name_Image_Import(ref indexImage, out buf);
    return Marshal.PtrToStringAnsi(buf);
  }

  public string Get_Name_Block(ref short indexBlock)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Name_Block_Import(ref indexBlock, out buf);
    else
      ShowDll_x64.Get_Name_Block_Import(ref indexBlock, out buf);
    return Marshal.PtrToStringAnsi(buf);
  }

  public void SetZoomAll_Dwg(ref short indexBlock)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.SetZoomAll_Dwg_Import(ref indexBlock);
    else
      ShowDll_x64.SetZoomAll_Dwg_Import(ref indexBlock);
  }

  public void StartDrawDwg(out int ratioWndow)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.StartDrawDwg_Import(out ratioWndow);
    else
      ShowDll_x64.StartDrawDwg_Import(out ratioWndow);
  }

  public void NextDrawDwg(out IntPtr buffer, out int arSize)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.NextDrawDwg_Import(out buffer, out arSize);
    else
      ShowDll_x64.NextDrawDwg_Import(out buffer, out arSize);
  }

  public void StartDrawDwgDouble(out int ratioWndow)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.StartDrawDwgDouble_Import(out ratioWndow);
    else
      ShowDll_x64.StartDrawDwgDouble_Import(out ratioWndow);
  }

  public void NextDrawDwgDouble(out IntPtr buffer, out int arSize)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.NextDrawDwgDouble_Import(out buffer, out arSize);
    else
      ShowDll_x64.NextDrawDwgDouble_Import(out buffer, out arSize);
  }

  public string Get_Name_ImageAll(ref short indexImageAll)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Name_ImageAll_Import(ref indexImageAll, out buf);
    else
      ShowDll_x64.Get_Name_ImageAll_Import(ref indexImageAll, out buf);
    return Marshal.PtrToStringAnsi(buf);
  }

  public string Get_Name_Block_Current(ref short indexBlock)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Get_Name_Block_Current_Import(ref indexBlock, out buf);
    else
      ShowDll_x64.Get_Name_Block_Current_Import(ref indexBlock, out buf);
    return Marshal.PtrToStringAnsi(buf);
  }

  public bool Open_ScanTable(byte[] fileCfgdata)
  {
    return !ShowDll_Import.IsWin32 ? ShowDll_x64.Open_ScanTable_Import(fileCfgdata) : ShowDll_x86.Open_ScanTable_Import(fileCfgdata);
  }

  public void Open_Scan_Files(
    string fileCfgName,
    out short lenArrayParameter,
    out short returnCode)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Open_Scan_Files_Import(fileCfgName, out lenArrayParameter, out returnCode);
    else
      ShowDll_x64.Open_Scan_Files_Import(fileCfgName, out lenArrayParameter, out returnCode);
  }

  public void SaveDWGFile(string dwgFilePath)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.SaveDWGFile_Import(dwgFilePath);
    else
      ShowDll_x64.SaveDWGFile_Import(dwgFilePath);
  }

  public void SetParameter(string name, string value)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.SetParameter_Import(name, value);
    else
      ShowDll_x64.SetParameter_Import(name, value);
  }

  public void Scaning_Dwg()
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Scaning_Dwg_Import();
    else
      ShowDll_x64.Scaning_Dwg_Import();
  }

  public void Set_Scan_State(short wCod, short lm, short dm)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.Set_Scan_State_Import(wCod, lm, dm);
    else
      ShowDll_x64.Set_Scan_State_Import(wCod, lm, dm);
  }

  public void GetNameParameter(ref short paramIndex, StringBuilder nameParameter)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.GetNameParameter_Import(ref paramIndex, nameParameter);
    else
      ShowDll_x64.GetNameParameter_Import(ref paramIndex, nameParameter);
  }

  public void GetParameter(ref short paramIndex, StringBuilder dataParameter)
  {
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.GetParameter_Import(ref paramIndex, dataParameter);
    else
      ShowDll_x64.GetParameter_Import(ref paramIndex, dataParameter);
  }

  public void GetAllText(short codeAtrib, ref string textAll)
  {
    IntPtr buf;
    if (ShowDll_Import.IsWin32)
      ShowDll_x86.GetAllText_Import(codeAtrib, out buf);
    else
      ShowDll_x64.GetAllText_Import(codeAtrib, out buf);
    textAll = Marshal.PtrToStringAnsi(buf);
  }
}
