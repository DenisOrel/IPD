
// Type: Intermech.Client.Core.Visualizers.IVisualizerFiles
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Client.Core.Visualizers;

/// <summary>
/// Объект, содержащий список файлов для отображения в тоолбаре и
/// обеспечивающий реакцию иа их выбор
/// </summary>
public interface IVisualizerFiles
{
  void Initialize(ComboBox comboBox);
}
