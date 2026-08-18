
// Type: Intermech.Navigator.DBObjects.FavoritesRootNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

public class FavoritesRootNode : CompositeNode, IContextAware, IFavoritesNode, INodeNotifications
{
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();
  private ConditionStructure[] _conditions;

  /// <summary>Конструктор.</summary>
  public FavoritesRootNode()
  {
    this._services.AddService(typeof (IFavoritesNode), (object) new FavoritesNode());
    this._services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.LocalTypesMode));
    this.options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>
  /// Создает и возвращает часть элемента, отвечающую за версии объектов (локальные в том числе), входящие в Избранное конкретного пользователя.
  /// </summary>
  /// <returns>Интерфейс части</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new FavoritesObjectsPart(this.Services));
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IUserFavouritesService)) is IUserFavouritesService customService))
        throw new KernelException("Не найден сервис для работы с Избранным.");
      DescriptorCollection descriptors = new DescriptorCollection();
      foreach (int objTypeID in ((IEnumerable<int>) customService.GetObjectTypes(sessionKeeper.Session.SessionGUID)).ToList<int>())
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objTypeID));
      return this.SlotsFromSinglePart((INodePart) new DescriptorsPart(descriptors, true));
    }
  }

  /// <summary>Контейнер сервисов.</summary>
  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  public ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    switch (e.EventName)
    {
      case "FavoritesChanged":
        return ProcessResult.RefreshNode;
      case "FavoritesRemoveType":
        if (e is DBObjectTypesEventArgs args && this.folderSlots.Count > 0)
          this.RemoveTypeDescriptor(args);
        return ProcessResult.RefreshNode;
      default:
        return ProcessResult.None;
    }
  }

  private void RemoveTypeDescriptor(DBObjectTypesEventArgs args)
  {
    DescriptorCollection descriptors = ((DescriptorsPart) this.folderSlots[0].Object).Descriptors;
    descriptors.RemoveAt(descriptors.IndexOf(descriptors.FirstOrDefault<IDescriptor>((Func<IDescriptor, bool>) (o => args.ObjectTypeIDs.Contains(((Intermech.Navigator.DBObjectTypes.Descriptor) o).ObjectTypeID)))));
  }
}
