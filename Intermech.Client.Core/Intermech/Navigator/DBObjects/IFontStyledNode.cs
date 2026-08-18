
// Type: Intermech.Navigator.DBObjects.IFontStyledNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Queries;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

public interface IFontStyledNode
{
  /// <summary>
  /// Функция вызываетя перед создание нода (в RelatedPartBase.cs)
  /// для получения вида шрифта
  /// </summary>
  /// <param name="fieldValues"> Значения полей </param>
  /// <param name="adapter"> Адаптер ?</param>
  /// <param name="stateAttr"> Значение атрибута статус или нулл если его нет</param>
  /// <returns>Вид шрифта</returns>
  FontStyle ComputeFontStyleStatus(object[] fieldValues, RecordAdapter adapter, byte[] stateAttr);
}
