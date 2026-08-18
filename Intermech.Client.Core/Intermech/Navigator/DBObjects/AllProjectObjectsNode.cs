
// Type: Intermech.Navigator.DBObjects.AllProjectObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.Navigator;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Виртуальный узел, реализующий список всех объектов проекта
/// </summary>
public class AllProjectObjectsNode : CompositeNode, IContextAware, INodeNotifications
{
  /// <summary>Название виртуального узла - "Все объекты проекта"</summary>
  public static readonly string AllProjectObjectsNodeName = LocalizationHolder.rm.GetString("Client.Core_1224");
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _services;
  /// <summary>
  /// Условия, по которым выбираются объекты, входящие в проект
  /// </summary>
  private ConditionStructure[] _conditions;
  /// <summary>Идентификатор проекта, с которым связан данный узел</summary>
  private long _projectID;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="projectID">Идентификатор версии проекта</param>
  public AllProjectObjectsNode(long projectID)
  {
    this.options |= NodeOptions.CanContainsObjectsList;
    this._projectID = projectID;
    this._conditions = new ConditionStructure[3]
    {
      new ConditionStructure(-14, RelationalOperators.Equal, (object) this._projectID, LogicalOperators.AND, 0, true),
      new ConditionStructure(-2, RelationalOperators.NotEqual, (object) this._projectID, LogicalOperators.AND, 0, true),
      new ConditionStructure(-2, RelationalOperators.NotEqual, (object) -this._projectID, LogicalOperators.NONE, 0, true)
    };
  }

  /// <summary>Создать слоты не-папок</summary>
  /// <returns>Слоты не-папок</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    List<PartSlot> nonFolderSlots = base.CreateNonFolderSlots() ?? new List<PartSlot>();
    nonFolderSlots.Add(new PartSlot(Intermech.Navigator.Consts.CategoryRecentObjectsNodeGuid, (INodePart) new ObjectsNodePart(this._conditions, this.Services)));
    return nonFolderSlots;
  }

  /// <summary>Контейнер сервисов</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="AdditionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  public virtual ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    return ProcessResult.None;
  }
}
