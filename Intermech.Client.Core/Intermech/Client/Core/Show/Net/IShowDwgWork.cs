
// Type: Intermech.Client.Core.Show.Net.IShowDwgWork
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Show.Net.ShowNew.Shape;
using Intermech.Interfaces.Show;
using System.Drawing;


namespace Intermech.Client.Core.Show.Net;

/// <summary> прочитать графику из компоновки или блока</summary>
internal interface IShowDwgWork : IShowDwg
{
  /// <summary>прочитать графику из компоновки или блока</summary>
  /// <param name="obj">компоновка или блок</param>
  /// <returns>объект работы со списком графики</returns>
  ShapeList SubReadDataShowBlock(IDllIndex obj);

  /// <summary>прочитать штамп (для видимых слоёв)</summary>
  /// <param name="layout">компоновка со штампом</param>
  /// <param name="fileCfgName">имя файла конфигурации штампа</param>
  /// <param name="cfgData">данные файла конфигурации штампа</param>
  /// <returns>массив прочитанных данных из штампа; null -нет штампа</returns>
  IStampField[] SubReadScanStamp(ILayout layout, string fileCfgName, byte[] cfgData);

  /// <summary>проверить нужно ли сменять цвета у графики</summary>
  bool CheckColorToBlack();

  /// <summary>цвет подложки</summary>
  Color PaperColor { get; }
}
