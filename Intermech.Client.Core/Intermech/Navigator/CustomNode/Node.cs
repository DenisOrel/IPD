
// Type: Intermech.Navigator.CustomNode.Node
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;


namespace Intermech.Navigator.CustomNode;

/// <summary>
/// Класс, реализующий настраиваемый элемент из пространства навигации.
/// Содержимое этого элемента динамически формируется в процессе выполнения
/// программы. Применяется в окне выбора типов объектов, объектов базы данных
/// и т.д.
/// </summary>
public class Node : CompositeNode
{
  /// <summary>Коллекция дескрипторов дочерних объектов</summary>
  protected DescriptorCollection _descriptors;

  public Node(DescriptorCollection descriptors) => this._descriptors = descriptors;

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new DescriptorsPart(this._descriptors));
  }

  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    NodeColumnCollection supportedColumns = base.GetSupportedColumns(content, ColumnSetName);
    if ((content & ContentType.Folders) != ContentType.None)
    {
      foreach (NodeColumn supportedColumnsObject in (List<NodeColumn>) Utils.DefaultSupportedColumnsObjects())
        supportedColumns.Add(supportedColumnsObject);
    }
    return supportedColumns;
  }
}
