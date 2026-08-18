
// Type: Intermech.Navigator.Controls.ITreeViewJob
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Позволяет реализовать алгоритм применения результатов
/// выполнения фонового задания к дереву навигатора.
/// </summary>
internal interface ITreeViewJob
{
  /// <summary>
  /// Обновляет дерево навигатора в соответствии с результатами,
  /// полученными при выполнении фонового задания.
  /// </summary>
  void UpdateTreeView();
}
