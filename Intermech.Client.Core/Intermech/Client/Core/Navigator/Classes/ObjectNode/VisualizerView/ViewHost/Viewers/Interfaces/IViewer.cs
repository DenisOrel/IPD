
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces.IViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;

public interface IViewer
{
  /// <summary>Подготовить контрол к просмотру</summary>
  /// <returns>Возвращает контол, в котом будет отображение (возможно в дочернем)</returns>
  void Init(Control owner);

  /// <summary>Открыть просматриваемый файл</summary>
  /// <param name="fileItemInfo"></param>
  /// <param name="serviceProvider"></param>
  /// <returns>false - если не удалось открыть</returns>
  void Open(FileItem fileItemInfo, System.IServiceProvider serviceProvider);

  /// <summary>Закрыть просматриваемый файл</summary>
  void Close();

  /// <summary>Очистить контрол</summary>
  void Clear();
}
