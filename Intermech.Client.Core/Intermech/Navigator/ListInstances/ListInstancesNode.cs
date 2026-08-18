
// Type: Intermech.Navigator.ListInstances.ListInstancesNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.ListInstances;

/// <summary>Виртуальный нод "Список исполнений"</summary>
public class ListInstancesNode : CompositeNode, IContextAware, INodeNotifications
{
  /// <summary>Значение атрибута "Идентификатор группового изделия"</summary>
  private readonly IListInstancesInfo _info;
  /// <summary>Ссылка на ListInstancesPart</summary>
  private ListInstancesPart _part;
  /// <summary>Контейнер сервисов</summary>
  private readonly AdvancedServiceContainer _services = new AdvancedServiceContainer();

  public ListInstancesNode(IListInstancesInfo info)
    : this()
  {
    this._info = info;
  }

  public ListInstancesNode()
  {
    this._services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications));
  }

  /// <summary>Контейнер сервисов</summary>
  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  /// <summary>
  /// Создает и возвращает часть, которая отвечает за дочерние элементы-папки.
  /// </summary>
  /// <returns>Ссылка на интерфейс части</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    if (this._part == null)
      this._part = new ListInstancesPart(this._info, this.Services);
    return this.SlotsFromSinglePart((INodePart) this._part);
  }

  protected override List<PartSlot> CreateNonFolderSlots() => this.CreateFolderSlots();

  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="AdditionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  public ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    if (e is DBObjectsEventArgs objectsEventArgs && this._part.InstancesIDs.Count > 0 && (e.EventName == "ObjectsChanged" || e.EventName == "ObjectsChangesCancelled" || e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsCheckedOut" || e.EventName == "ObjectsCreated" || e.EventName == "ObjectsFiltrationChanged" || e.EventName == "ObjectsRemoved" || e.EventName == "ProjectChanged"))
    {
      for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
      {
        if (this._part.InstancesIDs.Contains(objectsEventArgs.ObjectIDs[index]))
          return ProcessResult.RefreshNode;
      }
    }
    return ProcessResult.None;
  }
}
