
// Type: Intermech.Client.Core.Show.Net.ShowDll.ShowDll
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.ShowNew.ExternFile;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary> функции работы с Show.Dll</summary>
public sealed class ShowDll
{
  private static FileData _currentWorkFile = (FileData) null;
  private static FindFileDelegate _currentWorkFun = (FindFileDelegate) null;
  private static IShowDll_Import _import = (IShowDll_Import) null;
  /// <summary> текущая версия подключаемого Net DLL </summary>
  private static readonly int _maxVersionNetDll = 1;
  /// <summary> Версия подключаемого Net DLL </summary>
  private static readonly int _versionWorkNetDll = 0;
  /// <summary> Версия подключаемого DLL </summary>
  private static readonly int _versionShowDLL;

  /// <summary> Объектов не создавать </summary>
  private ShowDll()
  {
  }

  public static IShowDll_Import Import => Intermech.Client.Core.Show.Net.ShowDll.ShowDll._import;

  /// <summary>Показать сообщение об ошибке из DLL</summary>
  /// <param name="message">сообщение об ошибке из ShowIPSx86.DLL, ShowIPSx64.DLL и ShowARX.DLL</param>
  public static void ShowToTrace([MarshalAs(UnmanagedType.LPWStr)] string message)
  {
    if (message == null || !(ServicesManager.GetService(typeof (IOutputView)) is IOutputView service))
      return;
    service.WriteString("Ошибки", message);
  }

  /// <summary>Статический конструктор</summary>
  static ShowDll()
  {
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._import = (IShowDll_Import) new ShowDll_Import();
    try
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.RegisterCallbackLogHelper(new CallbackLogHelperFunc(Intermech.Client.Core.Show.Net.ShowDll.ShowDll.ShowToTrace));
    }
    catch (Exception ex)
    {
    }
    try
    {
      short version = 6;
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.CheckVersion(ref version);
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll._versionShowDLL = (int) version;
    }
    catch (FileNotFoundException ex)
    {
      throw new FileNotFoundException(Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.DLLName, (Exception) ex);
    }
    try
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll._versionWorkNetDll = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.CheckVersionNet(Intermech.Client.Core.Show.Net.ShowDll.ShowDll._maxVersionNetDll);
    }
    catch
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll._versionWorkNetDll = 0;
    }
  }

  internal static FileData CurrentWorkFile => Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile;

  /// <summary>Получить версию  DLL (Net стиль)</summary>
  /// <returns>возвращает версию  DLL</returns>
  internal static int VersionNetShowDLL => Intermech.Client.Core.Show.Net.ShowDll.ShowDll._versionWorkNetDll;

  /// <summary> Версия подключаемого DLL </summary>
  internal static int VersionShowDLL => Intermech.Client.Core.Show.Net.ShowDll.ShowDll._versionShowDLL;

  /// <summary>начать чтение файла </summary>
  /// <param name="file">файл</param>
  /// <param name="defaultWeight">толщина линий по умолчанию</param>
  /// <param name="fun">CallBack-функкция для получения внешних файлов</param>
  /// <returns>код завершения чтения </returns>
  internal static DwgOpenException.ReturnType Open_Dwg_Net(
    FileData file,
    float defaultWeight,
    ref FindFileDelegate fun)
  {
    if (Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile == file)
      return DwgOpenException.ReturnType.exOk;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile = (FileData) null;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFun = (FindFileDelegate) null;
    short code = 0;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Open_Dwg_Net(file.OriginalPath, file.InFile.Length, file.InFile, defaultWeight, ref fun, out code);
    if (code == (short) 0)
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile = file;
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFun = fun;
    }
    return (DwgOpenException.ReturnType) code;
  }

  public static int Open_DWG_Files(string nameFiles)
  {
    FileData file = new FileData(nameFiles, (byte[]) null);
    if (Intermech.Client.Core.Show.Net.ShowDll.ShowDll.VersionNetShowDLL == 0)
      return (int) Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Open_Dwg_Data(file);
    float setting = (float) ShowSetting.Settings[(object) "DefaultWeight"];
    FindFileDelegate fun = (FindFileDelegate) null;
    DwgOpenException.ReturnType returnType = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Open_Dwg_Net(file, setting, ref fun);
    return returnType == DwgOpenException.ReturnType.exOk ? (int) returnType : throw new DwgOpenException("Error Open", returnType);
  }

  internal static DwgOpenException.ReturnType Open_Dwg_Data(FileData file)
  {
    if (Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile == file)
      return DwgOpenException.ReturnType.exOk;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile = (FileData) null;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFun = (FindFileDelegate) null;
    short code = 0;
    bool flag = false;
    try
    {
      if (file.InFile != null)
      {
        Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Open_Dwg_Handle(file.InFile, file.InFile.Length, out code);
        flag = true;
      }
    }
    catch
    {
      flag = false;
    }
    if (!flag && file.OriginalPath != string.Empty)
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Open_Dwg_Files(file.OriginalPath, out code);
    if (code == (short) 0)
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile = file;
    return (DwgOpenException.ReturnType) code;
  }

  internal static int CheckVersionDwg(string fileName, out int versionMdt)
  {
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile = (FileData) null;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFun = (FindFileDelegate) null;
    short version;
    short versionMdt1;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.CheckVersionDwg(fileName, out version, out versionMdt1);
    versionMdt = (int) versionMdt1;
    return (int) version;
  }

  /// <summary>закрыть файл и освободить все связи с чертежом</summary>
  internal static void Close()
  {
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Close_Dwg_Files();
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFile = (FileData) null;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll._currentWorkFun = (FindFileDelegate) null;
    int versionShowDll = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.VersionShowDLL;
  }

  internal static GraphicsUnit GetMeasurement()
  {
    GraphicsUnit setting = (GraphicsUnit) ShowSetting.Settings[(object) "DefaultUnits"];
    short measurement;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.GetMeasurement(out measurement);
    if (measurement == (short) -1)
      return setting;
    return measurement != (short) 0 ? GraphicsUnit.Millimeter : GraphicsUnit.Inch;
  }

  public static int Layout
  {
    get
    {
      short numModelState;
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Model_State(out numModelState);
      return (int) numModelState;
    }
    set
    {
      short numModelState = (short) value;
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Set_Model_State(ref numModelState);
      int layout = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout;
    }
  }

  public static string[] GetLayoutNames()
  {
    int layout = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout;
    List<string> stringList = new List<string>();
    try
    {
      for (short indexLayout = (short) (Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout = 0); (int) indexLayout == Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout; Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout = (int) ++indexLayout)
      {
        string nameLayout = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_Layout(ref indexLayout);
        stringList.Add(nameLayout);
      }
    }
    finally
    {
      Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Layout = layout;
    }
    return stringList.ToArray();
  }

  internal static string[] GetLayerNames()
  {
    List<string> stringList = new List<string>();
    stringList.Add(string.Empty);
    for (short indexLayer = 1; (int) indexLayer < stringList.Count + 2; ++indexLayer)
    {
      short statusLayer;
      string nameLayer = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_Layer(ref indexLayer, out statusLayer);
      if (nameLayer.Length != 0 && statusLayer >= (short) 0)
      {
        if (nameLayer.Length > 0 && nameLayer[0] != char.MinValue)
          stringList.Add(nameLayer);
      }
      else
        break;
    }
    return stringList.ToArray();
  }

  internal static bool GetLayerVisible(int indexLayer)
  {
    short indexLayer1 = (short) indexLayer;
    short statusLayer;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_Layer(ref indexLayer1, out statusLayer);
    return statusLayer != (short) 0;
  }

  internal static void SetLayerVisible(int indexLayer, bool visible)
  {
    short indexLayer1 = (short) indexLayer;
    short statusLayer = visible ? (short) 3 : (short) 0;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Set_Layer_State(ref indexLayer1, ref statusLayer);
  }

  internal static Rectangle GetBounds(int indexLayer)
  {
    short indexLayer1 = (short) indexLayer;
    return Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Border_Layer(ref indexLayer1);
  }

  internal static RectangleD GetDwgBounds(int indexLayer)
  {
    short indexLayer1 = (short) indexLayer;
    double minX;
    double minY;
    double maxX;
    double maxY;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Gabarit_Layer(ref indexLayer1, out minX, out minY, out maxX, out maxY);
    if (minX == 3.5E+120)
      return RectangleD.Empty;
    double width = maxX - minX;
    double height = maxY - minY;
    return new RectangleD(minX, minY, width, height);
  }

  internal static Point Transfer(PointD val) => Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.TransferDwg_to_Win(val);

  internal static PointD Transfer(Point pnt) => Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.TransferWin_to_Dwg(pnt);

  internal static void SetDwgBounds(RectangleD box)
  {
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.SetDrawDwgWin(box.Left, box.Bottom, box.Right, box.Top);
  }

  internal static void SetBounds(Rectangle windowDraw) => Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.SetWindow_Dwg(windowDraw);

  public static string[] GetReferenceOnly()
  {
    List<string> stringList = new List<string>();
    short indexAll = 0;
    while (true)
    {
      string nameDwgAll = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_Dwg_All(ref indexAll);
      if (nameDwgAll.Length != 0)
      {
        stringList.Add(nameDwgAll);
        ++indexAll;
      }
      else
        break;
    }
    return stringList.ToArray();
  }

  public static string[] GetImageOnly()
  {
    List<string> stringList = new List<string>();
    short indexImage = 0;
    while (true)
    {
      string nameImage = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_Image(ref indexImage);
      if (nameImage.Length != 0)
      {
        stringList.Add(nameImage);
        ++indexImage;
      }
      else
        break;
    }
    return stringList.ToArray();
  }

  internal static string[] GetBlockNames()
  {
    List<string> stringList = new List<string>();
    short indexBlock = 1;
    while (true)
    {
      string nameBlock = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_Block(ref indexBlock);
      if (nameBlock.Length != 0)
      {
        stringList.Add(nameBlock);
        ++indexBlock;
      }
      else
        break;
    }
    return stringList.ToArray();
  }

  internal static bool SetZoomAll_Dwg(int indexBlock)
  {
    short indexBlock1 = (short) indexBlock;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.SetZoomAll_Dwg(ref indexBlock1);
    return (int) indexBlock1 == indexBlock;
  }

  internal static double StartDrawDwg()
  {
    int ratioWndow;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.StartDrawDwg(out ratioWndow);
    return (double) ratioWndow / 10000.0;
  }

  internal static int NextDrawDwg(out IntPtr buffer)
  {
    int arSize = 0;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.NextDrawDwg(out buffer, out arSize);
    return arSize;
  }

  internal static double StartDrawDwgDouble()
  {
    int ratioWndow;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.StartDrawDwgDouble(out ratioWndow);
    return (double) ratioWndow / 10000.0;
  }

  internal static int NextDrawDwgDouble(out IntPtr buffer)
  {
    int arSize = 0;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.NextDrawDwgDouble(out buffer, out arSize);
    return arSize;
  }

  internal static string Get_Name_ImageAll(int indexImageAll)
  {
    short indexImageAll1 = (short) indexImageAll;
    return Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_ImageAll(ref indexImageAll1);
  }

  internal static string Get_Name_Block_Current(int indexBlock)
  {
    short indexBlock1 = (short) indexBlock;
    return Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Get_Name_Block_Current(ref indexBlock1);
  }

  /// <summary>прочитать данные параметров штампа</summary>
  /// <param name="fileCfgName">имя файла с описанием штампа</param>
  /// <returns>[код завершения чтения(=0 ),количество параметров]</returns>
  public static int[] Open_Scan_Files(string fileCfgName)
  {
    short lenArrayParameter;
    short returnCode;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Open_Scan_Files(fileCfgName, out lenArrayParameter, out returnCode);
    return new int[2]
    {
      (int) returnCode,
      (int) lenArrayParameter
    };
  }

  public static void SaveDWGFile(string dwgFilePath) => Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.SaveDWGFile(dwgFilePath);

  public static void SetParameter(string name, string value)
  {
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.SetParameter(name, value);
  }

  public static bool Open_Scan_FilesData(string fileCfgName, byte[] fileCfgdata)
  {
    if (fileCfgdata != null)
      return Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Open_ScanTable(fileCfgdata);
    short returnCode;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Open_Scan_Files(fileCfgName, out short _, out returnCode);
    return returnCode == (short) 0;
  }

  internal static List<KeyValuePair<string, string>> ScanLayout()
  {
    List<KeyValuePair<string, string>> keyValuePairList = new List<KeyValuePair<string, string>>();
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Scaning_Dwg();
    int paramIndex = 0;
    string nameParameter;
    for (; (nameParameter = Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetNameParameter(paramIndex)) != string.Empty; ++paramIndex)
      keyValuePairList.Add(new KeyValuePair<string, string>(nameParameter, Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetParameter(paramIndex)));
    return keyValuePairList;
  }

  public static string GetNameParameter(int paramIndex)
  {
    StringBuilder nameParameter = new StringBuilder(501);
    nameParameter.Length = 500;
    short paramIndex1 = (short) paramIndex;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.GetNameParameter(ref paramIndex1, nameParameter);
    return nameParameter.ToString();
  }

  public static string GetParameter(int paramIndex)
  {
    StringBuilder dataParameter = new StringBuilder(501);
    dataParameter.Length = 500;
    short paramIndex1 = (short) paramIndex;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.GetParameter(ref paramIndex1, dataParameter);
    return dataParameter.ToString();
  }

  internal static string GetAllText(int codeAtrib)
  {
    string empty = string.Empty;
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.GetAllText((short) codeAtrib, ref empty);
    return empty;
  }

  public static void Set_Scan_State(short wCod, short lm, short dm)
  {
    Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Set_Scan_State((short) 1, (short) 0, (short) 0);
  }

  public static List<string> Scaning_Layers()
  {
    return new List<string>((IEnumerable<string>) Intermech.Client.Core.Show.Net.ShowDll.ShowDll.GetLayerNames());
  }

  public static void Scaning_Dwg() => Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Import.Scaning_Dwg();

  public static void Close_Dwg_Files() => Intermech.Client.Core.Show.Net.ShowDll.ShowDll.Close();
}
