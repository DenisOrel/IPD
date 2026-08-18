
// Type: Intermech.Navigator.Controls.NavigatorNodeCycle
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Циклическая зависимость ("петля"), обнаруженная для узла в дереве Навигатора
/// </summary>
public enum NavigatorNodeCycle
{
  /// <summary>"Петли" нет</summary>
  None,
  /// <summary>
  /// "Петля" обнаружена, в дереве есть узел, содержащий такой же объект, но с другим родительским объектом.
  /// Узел требуется показать, но без состава
  /// </summary>
  Object,
  /// <summary>
  /// "Петля" обнаружена, в дереве есть узел, содержащий такой же объект и принадлежащий такому же родителю.
  /// Узел требуется скрывать
  /// </summary>
  Link,
}
