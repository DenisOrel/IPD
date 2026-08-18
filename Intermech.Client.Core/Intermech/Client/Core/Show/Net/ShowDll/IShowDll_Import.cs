
// Type: Intermech.Client.Core.Show.Net.ShowDll.IShowDll_Import
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Text;


namespace Intermech.Client.Core.Show.Net.ShowDll;

/// <summary> функции работы с Show.Dll</summary>
public interface IShowDll_Import
{
  string DLLName { get; }

  /// <summary>Передать DLL адрес функции сообщения об ошибке</summary>
  /// <param name="pfn">адрес функции</param>
  void RegisterCallbackLogHelper(CallbackLogHelperFunc pfn);

  /// <summary>Получить версию  DLL (Net стиль)</summary>
  /// <param name="versionNet">текущая версия подключаемого Net DLL</param>
  /// <returns>возвращает версию  DLL (Net стиль)</returns>
  int CheckVersionNet(int versionNet);

  /// <summary>Получить версию DLL</summary>
  /// <param name="version">возвращает версию DLL</param>
  void CheckVersion(ref short version);

  /// <summary>начать чтение файла</summary>
  /// <param name="fileName">путь и имя файла</param>
  /// <param name="lenfileDwgdata">длинна файла</param>
  /// <param name="fileDwgdata">содержимое файла</param>
  /// <param name="defaultWeight">толщина линий по умолчанию</param>
  /// <param name="fun">CallBack-функкция для получения внешних файлов</param>
  /// <param name="code">код завершения чтения</param>
  void Open_Dwg_Net(
    string fileName,
    int lenfileDwgdata,
    byte[] fileDwgdata,
    float defaultWeight,
    ref FindFileDelegate fun,
    out short code);

  /// <summary>начать чтение файла</summary>
  /// <param name="fileName">путь и имя файла</param>
  /// <param name="code">код завершения чтения</param>
  void Open_Dwg_Files(string fileName, out short code);

  /// <summary>начать чтение файла</summary>
  /// <param name="fileDwgdata">содержимое файла</param>
  /// <param name="lenfileDwgdata">длинна файла</param>
  /// <param name="code">код завершения чтения</param>
  void Open_Dwg_Handle(byte[] fileDwgdata, int lenfileDwgdata, out short code);

  /// <summary>проверить версию файла</summary>
  /// <param name="fileName">путь и имя файла</param>
  /// <param name="version">версия файла</param>
  /// <param name="versionMdt">версия MDT файла</param>
  void CheckVersionDwg(string fileName, out short version, out short versionMdt);

  /// <summary>закрыть файл и освободить все связи с чертежом</summary>
  void Close_Dwg_Files();

  /// <summary>прочитать единицы (мм или дюймы)</summary>
  /// <param name="measurement">единицы (мм или дюймы)</param>
  void GetMeasurement(out short measurement);

  /// <summary>прочитать текущий номер чертежа</summary>
  /// <param name="numModelState">номер чертежа</param>
  void Get_Model_State(out short numModelState);

  /// <summary>сделать текущим указанный номер чертежа</summary>
  /// <param name="numModelState">номер чертежа</param>
  void Set_Model_State(ref short numModelState);

  /// <summary>прочитать имя чертежа</summary>
  /// <param name="indexLayout">номер чертежа</param>
  /// <returns>имя чертежа</returns>
  string Get_Name_Layout(ref short indexLayout);

  /// <summary>прочитать имя слоя и его состояние</summary>
  /// <param name="indexLayer">номер слоя</param>
  /// <param name="statusLayer">состояние слоя</param>
  /// <returns>имя слоя</returns>
  string Get_Name_Layer(ref short indexLayer, out short statusLayer);

  /// <summary>установить  для слоя состояние</summary>
  /// <param name="indexLayer">номер слоя</param>
  /// <param name="statusLayer">состояние слоя</param>
  void Set_Layer_State(ref short indexLayer, ref short statusLayer);

  /// <summary>прочитать для слоя границы окна рисования</summary>
  /// <param name="indexLayer">номер слоя</param>
  /// <returns>границы окна рисования</returns>
  Rectangle Get_Border_Layer(ref short indexLayer);

  /// <summary>прочитать габариты слоя в чертеже</summary>
  /// <param name="indexLayer">номер слоя</param>
  /// <param name="minX">минимум по X</param>
  /// <param name="minY">минимум по Y</param>
  /// <param name="maxX">максимум по X</param>
  /// <param name="maxY">максимум по Y</param>
  void Get_Gabarit_Layer(
    ref short indexLayer,
    out double minX,
    out double minY,
    out double maxX,
    out double maxY);

  Point TransferDwg_to_Win(PointD pnt);

  PointD TransferWin_to_Dwg(Point pnt);

  void SetDrawDwgWin(double x1, double y1, double x2, double y2);

  /// <summary>установить границы окна рисования</summary>
  /// <param name="windowDraw">границы окна рисования</param>
  void SetWindow_Dwg(Rectangle windowDraw);

  string Get_Name_Dwg_All(ref short indexAll);

  string Get_Name_Image(ref short indexImage);

  string Get_Name_Block(ref short indexBlock);

  void SetZoomAll_Dwg(ref short indexBlock);

  void StartDrawDwg(out int ratioWndow);

  void NextDrawDwg(out IntPtr buffer, out int arSize);

  void StartDrawDwgDouble(out int ratioWndow);

  void NextDrawDwgDouble(out IntPtr buffer, out int arSize);

  string Get_Name_ImageAll(ref short indexImageAll);

  string Get_Name_Block_Current(ref short indexBlock);

  bool Open_ScanTable(byte[] fileCfgdata);

  /// <summary>прочитать данные параметров штампа</summary>
  /// <param name="fileCfgName">имя файла с описанием штампа</param>
  /// <param name="lenArrayParameter">количество параметров</param>
  /// <param name="returnCode">код завершения чтения(=0 )</param>
  void Open_Scan_Files(string fileCfgName, out short lenArrayParameter, out short returnCode);

  void Scaning_Dwg();

  void Set_Scan_State(short wCod, short lm, short dm);

  void GetNameParameter(ref short paramIndex, StringBuilder nameParameter);

  void GetParameter(ref short paramIndex, StringBuilder dataParameter);

  void GetAllText(short codeAtrib, ref string textAll);

  void SaveDWGFile(string dwgFilePath);

  void SetParameter(string name, string value);
}
