
// Type: Intermech.Navigator.ContextMenu.StepwiseProviderCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Navigator.ContextMenu;

/// <summary>Реализует коллекцию пошаговых провайдеров.</summary>
public class StepwiseProviderCollection : CollectionBase
{
  /// <summary>Добавляет провайдер в коллекцию.</summary>
  /// <param name="provider">Провайдер</param>
  public void Add(IStepwiseCommandsProvider provider) => this.List.Add((object) provider);

  /// <summary>Вставляет провайдер в коллекцию в указанную позицию.</summary>
  /// <param name="index">Позиция для вставки</param>
  /// <param name="provider">Провайдер</param>
  public void Insert(int index, IStepwiseCommandsProvider provider)
  {
    this.List.Insert(index, (object) provider);
  }

  /// <summary>Удаляет провайдер из коллекции.</summary>
  /// <param name="provider">Провайдер</param>
  public void Remove(IStepwiseCommandsProvider provider) => this.List.Remove((object) provider);

  /// <summary>Возвращает признак наличия провайдера в коллекции.</summary>
  /// <param name="provider">Провайдер</param>
  /// <returns>Признак наличия провайдера в коллекции</returns>
  public bool Contains(IStepwiseCommandsProvider provider) => this.List.Contains((object) provider);

  /// <summary>Возвращает позицию провайдера в коллекции.</summary>
  /// <param name="provider">Провайдер</param>
  /// <returns>Позиция провайдера</returns>
  public int IndexOf(IStepwiseCommandsProvider provider) => this.List.IndexOf((object) provider);

  /// <summary>
  /// Возвращает провайдер из указанной позиции в коллекции.
  /// </summary>
  public IStepwiseCommandsProvider this[int index] => (IStepwiseCommandsProvider) this.List[index];
}
