
// Type: Intermech.Navigator.DBObjects.ProjectObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Узел, реализующий элемент навигации для объектов типа "Проекты"
/// </summary>
public class ProjectObjectNode : ObjectNode
{
  /// <summary>Контейнер сервисов</summary>
  protected new AdvancedServiceContainer _services = new AdvancedServiceContainer();

  /// <summary>Создать узел</summary>
  /// <param name="objTypeID">Тип</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  public ProjectObjectNode(int objTypeID, long objID)
    : base(objTypeID, objID)
  {
    this._services.AddService(typeof (ProjectObjectID), (object) new ProjectObjectID(objID));
  }

  /// <summary>Контейнер сервисов</summary>
  public override IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  /// <summary>Составить список слотов с составом-папками для узла</summary>
  /// <returns>Список слотов с составом-папками для узла</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = base.CreateFolderSlots();
    IViewState service = this.Services != null ? this.Services.GetService(typeof (IViewState)) as IViewState : (IViewState) null;
    if (service == null || (service.ViewState & ViewStateFlags.NodeInTree) == ViewStateFlags.None)
      return folderSlots;
    if (folderSlots == null)
      folderSlots = new List<PartSlot>();
    folderSlots.Insert(0, new PartSlot(Intermech.Navigator.Consts.CategoryAllProjectObjectsNodeGuid, (INodePart) new DescriptorsPart(this.GetAllProjectObjectsPart())));
    return folderSlots;
  }

  protected override List<PartSlot> CreateNonFolderSlots() => base.CreateNonFolderSlots();

  /// <summary>
  /// Часть узла, представляющая собой узел "Все объекты проекта"
  /// </summary>
  private DescriptorCollection GetAllProjectObjectsPart()
  {
    return new DescriptorCollection()
    {
      {
        Intermech.Navigator.Consts.CategoryAllProjectObjectsNodeGuid,
        (IDescriptor) new AllProjectObjectsDescriptor(this._objID)
      }
    };
  }
}
