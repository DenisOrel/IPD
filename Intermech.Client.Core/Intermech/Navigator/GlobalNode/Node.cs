
// Type: Intermech.Navigator.GlobalNode.Node
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.GlobalNodes;
using System.Collections.Generic;


namespace Intermech.Navigator.GlobalNode;

/// <summary>
/// Реализует корневой элемент всего пространства навигации. Все дочерние
/// элементы являются папками.
/// </summary>
public class Node : CompositeNode, INodeNotifications
{
  /// <summary>Вернуть слоты-папки</summary>
  /// <returns>Слоты-папки</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new DescriptorsPart(((IGlobalNodeRegistry) ServicesManager.GetService(typeof (IGlobalNodeRegistry))).CreateDescriptorCollection(), false));
  }

  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="AdditionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  public ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    return ProcessResult.None;
  }
}
