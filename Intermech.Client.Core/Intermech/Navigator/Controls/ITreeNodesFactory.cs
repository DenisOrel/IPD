
// Type: Intermech.Navigator.Controls.ITreeNodesFactory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Controls;

/// <summary>Интерфейc сервиса подбора классов нода дерева (потомков NavigatorTreeNode) для отображения в дереве навигатора нод</summary>
public interface ITreeNodesFactory
{
  /// <summary>Создать кастомную ноду дерева (потомка NavigatorTreeNode) для отображения ноды (INode), созданной по переданному
  /// идентификатору ноды (INodeID). Если вернёт null будет создана обычная NavigatorTreeNode. </summary>
  /// <param name="navTreeView">Дерево навигатора</param>
  /// <param name="parent">Нода дерева навигатора, в составе которой должна быть создана нода дерева</param>
  /// <param name="nodeID">Интерфейс идентификатора создаваемой ноды</param>
  /// <param name="fieldValues">Значения полей</param>
  /// <param name="rawValues">Значения полей в raw виде</param>
  /// <returns>Кастомная нода дерева, которая будет представлять создаваемую ноду. Если null - должна быть создана обычная NavigatorTreeNode</returns>
  NavigatorTreeNode CreateNavTreeNode(
    NavigatorTreeView navTreeView,
    NavigatorTreeNode parent,
    INodeID nodeID,
    object[] fieldValues,
    object[] rawValues);
}
