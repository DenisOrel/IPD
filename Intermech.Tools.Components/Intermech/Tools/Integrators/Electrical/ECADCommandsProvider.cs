// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADCommandsProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Абстрактный базовый провайдер команд для интеграторов</summary>
public abstract class ECADCommandsProvider : ICommandsProvider
{
  protected readonly IIntegrator integrator;
  private readonly int objType;
  protected readonly string elementListCommandText = "Перечень элементов";

  /// <summary>Создать объект</summary>
  /// <param name="integrator">Интегратор</param>
  /// <param name="objTypeGuid">Тип объектов для команд</param>
  public ECADCommandsProvider(IIntegrator integrator, Guid objTypeGuid)
  {
    this.integrator = integrator;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.objType = sessionKeeper.Session.GetObjectType(objTypeGuid, true).ObjectType;
  }

  /// <summary>Обновление контекстного меню навигатора</summary>
  public void UpdateMenuTemplate()
  {
    ((IFactory) ServicesManager.GetService(typeof (IFactory))).ContextMenuTemplate["Create"]?.Nodes.Add(new MenuTemplateNode(this.elementListCommandName, this.elementListCommandText, -1, 10, 10));
  }

  public virtual CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public virtual CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null || items.Count == 0 || items.Count != 1 || items.GetItemID(0).CategoryID != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || itemData.ObjectType != this.objType)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add(this.elementListCommandName, new CommandInfo(0, new ClickEventHandler(this.CreateElementList)));
    return groupCommands;
  }

  private void CreateElementList(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    IDBTypedObjectID itemData = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.elementList.Create(sessionKeeper.Session, itemData);
  }

  /// <summary>Создать Перечень элементов</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="documentID">Идентификатор версии документа, по которому создается ПЭ</param>
  public (int, int, int, List<string>) CreateElementList(
    IUserSession session,
    long documentID,
    bool silent = false)
  {
    (int, int, int) tuple = this.elementList.Create(session, documentID, silent);
    return (tuple.Item1, tuple.Item2, tuple.Item3, this.elementList.Errors);
  }

  public int ObjType => this.objType;

  protected abstract ElementList elementList { get; }

  protected abstract string elementListCommandName { get; }
}
