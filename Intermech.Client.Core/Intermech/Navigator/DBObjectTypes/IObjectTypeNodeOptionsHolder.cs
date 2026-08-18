
// Type: Intermech.Navigator.DBObjectTypes.IObjectTypeNodeOptionsHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Интерфейс позволяет хранить дополнительные настройки для узла
/// </summary>
public interface IObjectTypeNodeOptionsHolder
{
  /// <summary>
  /// Набор дополнительных свойств элемента пространства навигации для типов объектов
  /// </summary>
  ObjectTypeNodeOptions Options { get; set; }
}
