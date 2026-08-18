
// Type: Intermech.Navigator.DBObjects.MultipleObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор для списка нескольких объектов, содержащий коллекцию стандартных дескрипторов объектов IPS.
/// Наследовать от CustomNode.Descriptor пришлось дабы его нода она поддерживала
/// сервисы фильтрации, иначе с тулбаров пропадают соотв. команды</summary>
/// <summary>Создать дескриптор составного узла "Навигатора"</summary>
/// <param name="caption">Заголовок узла</param>
/// <param name="descriptors">Коллекция дескрипторов частей узла "Навигатора"</param>
/// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная "несколько объектов"</param>
/// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
public class MultipleObjectsDescriptor(
  [NotNull] string caption,
  [CanBeNull] IEnumerable<IDescriptor> descriptors,
  [CanBeNull] Image mainIcon = null,
  [CanBeNull] Image prefixIcon = null) : 
  CustomMultipleObjectsDescriptor<MultipleObjectsNode>(Intermech.Diagnostics.Check.ArgumentNotNull<string>(caption, nameof (caption)), descriptors, mainIcon, prefixIcon),
  IDescriptor,
  INodeItems,
  IPersistable
{
  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  [NotNull]
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new MultipleObjectsNode(this._descriptors, this.MainIcon, this.PrefixIcon);
  }
}
