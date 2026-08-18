
// Type: Intermech.Client.Core.Visualizers.IVisualizerEx
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;


namespace Intermech.Client.Core.Visualizers;

/// <summary>
/// Расширенный интерфейс визуализатора для получения точных спецификаций
/// </summary>
public interface IVisualizerEx : IVisualizer
{
  MapObject GetViewObject(VisualizerExParams visualizerExParams);
}
