
// Type: Intermech.Client.Core.Visualizers.IVisualizerService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Client.Core.Visualizers;

/// <summary>Сервис для управления визуализаторами файлов</summary>
public interface IVisualizerService
{
  /// <summary>Регистрирует визуализатор по расширению файла</summary>
  /// <param name="fileExt">Расширение файла</param>
  /// <param name="visualizer">Визуализатор</param>
  void AddVisualizer(string fileExt, IVisualizer visualizer);

  /// <summary>Возвращает зарегистрированный визуализатор</summary>
  /// <param name="fileExt">Расширение файла</param>
  /// <returns>Визуализатор или null, если для такого расширения визуализатор не зарегистрирован</returns>
  IVisualizer GetVisualizer(string fileExt);

  /// <summary>Поддерживаемые расширения файлов</summary>
  /// <returns></returns>
  List<string> SupportedExtensions();
}
