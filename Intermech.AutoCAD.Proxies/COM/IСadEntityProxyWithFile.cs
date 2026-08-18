// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.IСadEntityProxyWithFile
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Необязательный интерфейс прокси-объекта для COM-объекта элемента документа CAD-системы,
/// чье содержимое может храниться во внешнем файле.
/// </summary>
public interface IСadEntityProxyWithFile
{
  /// <summary>
  /// Возвращает путь к файлу элемента документа CAD-системы.
  /// Значение может включать абсолютный путь, относительный путь или только имя файла без пути.
  /// </summary>
  /// <returns>Путь к файлу или null, если содержимое элемента документа хранится в файле самого документа</returns>
  string TryGetFilePath();
}
