
// Type: Intermech.Client.Core.Visualizers.IVisualizerFilesSite
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Visualizers;

/// <summary>
/// Интерфейс управляет показом и выбором списка файлов на тоолбаре визуализатора
/// </summary>
public interface IVisualizerFilesSite
{
  /// <summary>Разрешает или запрещает выбор из списка файлов</summary>
  bool Enabled { get; set; }

  /// <summary>Объект, содержащий список файлов</summary>
  IVisualizerFiles Files { get; set; }
}
