
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypeNodeOptionsHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Diagnostics;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Интерфейс позволяет хранить дополнительные настройки для узла
/// </summary>
public sealed class ObjectTypeNodeOptionsHolder : IObjectTypeNodeOptionsHolder
{
  /// <summary>
  /// Набор дополнительных свойств элемента пространства навигации для типов объектов
  /// </summary>
  private ObjectTypeNodeOptions _options;

  /// <summary>Создать пустой экземпляр класса</summary>
  public ObjectTypeNodeOptionsHolder()
  {
  }

  /// <summary>Создать экземпляр класса, задать начальные значения</summary>
  /// <param name="options">Опции</param>
  public ObjectTypeNodeOptionsHolder(ObjectTypeNodeOptions options) => this._options = options;

  /// <summary>
  /// Набор дополнительных свойств элемента пространства навигации для типов объектов
  /// </summary>
  public ObjectTypeNodeOptions Options
  {
    [DebuggerStepThrough] get => this._options;
    set => this._options = value;
  }
}
