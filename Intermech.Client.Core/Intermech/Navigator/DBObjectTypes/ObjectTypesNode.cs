
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Класс, реализующий элемент "Типы объектов" из пространства навигации.
/// </summary>
public class ObjectTypesNode : CompositeNode, IContextAware
{
  /// <summary>
  /// Создать элемент пространства навигации "Типы объектов"
  /// </summary>
  public ObjectTypesNode()
    : this(AccessRights.Enabled)
  {
  }

  /// <summary>
  /// Создать элемент пространства навигации "Типы объектов"
  /// </summary>
  /// <param name="accessRights">Права доступа к списку объектов</param>
  public ObjectTypesNode(AccessRights accessRights)
  {
    this.AccessRights = accessRights;
    this.options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Права доступа к списку объектов</summary>
  public AccessRights AccessRights { [DebuggerStepThrough] get; set; }

  protected override ITopBinding GetBinding(BindingType bindingType)
  {
    return (ITopBinding) new ObjectTypesBinding(bindingType);
  }

  /// <summary>
  /// Создает и возвращает части элемента, отвечающие за списки объектов
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    DescriptorCollection descriptors = (DescriptorCollection) null;
    if (this.AccessRights == AccessRights.Enabled)
      descriptors = this.GetSpecialDescriptors();
    List<PartSlot> folderSlots = new List<PartSlot>();
    if (this.AccessRights == AccessRights.Enabled)
      folderSlots.Add(new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(descriptors, false)));
    folderSlots.Add(new PartSlot(Intermech.Navigator.Selections.Consts.ContentPartGuid, (INodePart) new ObjectTypesPart(-1)));
    return folderSlots;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.AccessRights != AccessRights.Enabled ? (List<PartSlot>) null : this.SlotsFromSinglePart((INodePart) new ObjectsPart(this.Services));
  }

  /// <summary>
  /// Контекст (контейнер сервисов) для элемента пространства навигации
  /// </summary>
  public IServiceProvider Services { [DebuggerStepThrough] get; set; }
}
