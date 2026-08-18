
// Type: Intermech.Navigator.DBObjects.SimpleCustomObjectDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор ноды объекта IPS, позволяющий в конструкторе указать заголовок объекта и иконки для его отображения</summary>
public class SimpleCustomObjectDescriptor : 
  CustomObjectDescriptor<CustomObjectNode>,
  IDescriptor,
  INodeItems,
  IPersistable,
  ICloneable,
  IDescriptorElementStatuses,
  IContextAware
{
  /// <summary>Создает кастомный дескриптор объекта</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="objGuid">Guid версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  /// <param name="caption">Заголовок узла. Если null, то будет использоваться стандартный заголовок объекта</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная иконка объекта</param>
  public SimpleCustomObjectDescriptor(
    long objID,
    Guid objGuid,
    ObjectFiltrationState state,
    [CanBeNull] string caption = null,
    [CanBeNull] Image prefixIcon = null,
    [CanBeNull] Image mainIcon = null)
    : base(objID, objGuid, state, caption, prefixIcon, mainIcon)
  {
  }

  /// <summary>Создает кастомный дескриптор объекта</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="caption">Заголовок узла. Если null, то будет использоваться стандартный заголовок объекта</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная иконка объекта</param>
  public SimpleCustomObjectDescriptor(
    long objID,
    [CanBeNull] string caption = null,
    [CanBeNull] Image prefixIcon = null,
    [CanBeNull] Image mainIcon = null)
    : base(objID, caption, prefixIcon, mainIcon)
  {
  }

  /// <summary>Создает кастомный дескриптор объекта</summary>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="state">Статус подобранной версии</param>
  /// <param name="caption">Заголовок узла. Если null, то будет использоваться стандартный заголовок объекта</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная иконка объекта</param>
  public SimpleCustomObjectDescriptor(
    long objID,
    ObjectFiltrationState state,
    [CanBeNull] string caption = null,
    [CanBeNull] Image prefixIcon = null,
    [CanBeNull] Image mainIcon = null)
    : base(objID, state, caption, prefixIcon, mainIcon)
  {
  }

  [NotNull]
  public override INode GetNode(INodeID nodeID, params object[] args)
  {
    NodeID nodeId = (NodeID) nodeID;
    return (INode) new CustomObjectNode(nodeId.ObjectTypeID, nodeId.ObjectID, this.PrefixIcon, this.MainIcon);
  }
}
