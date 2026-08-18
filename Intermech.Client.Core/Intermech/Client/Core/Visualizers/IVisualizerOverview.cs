
// Type: Intermech.Client.Core.Visualizers.IVisualizerOverview
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Map;


namespace Intermech.Client.Core.Visualizers;

/// <summary>
/// Обеспечивает просмотр окна визуализатора и навигацию в дочернем окне
/// </summary>
public interface IVisualizerOverview
{
  /// <summary>Пдосоединяет дочерний вид к окну просмотра</summary>
  /// <param name="childView"></param>
  void Attach(MapView childView);
}
