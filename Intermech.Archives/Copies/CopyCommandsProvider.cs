// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopyCommandsProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Провайдер команд для копий документов</summary>
public class CopyCommandsProvider : ICommandsProvider
{
  /// <summary>
  /// Метод вызывается для получения допустимых и подавляемых команд контекстного меню для
  /// выделенных элементов навигации одной категории и типа.
  /// Например, если в «Навигаторе» выделены элементы навигации нескольких разных категорий и типов,
  /// то данная команда будет вызываться для каждой из подгрупп этих элементов, сгруппированных
  /// по их категориям и типам. Наиболее применяемый метод даного интерфейса.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// Метод вызывается для получения допустимых и подавляемых команд контекстного меню для всей группы выделенных
  /// элементов навигации. Особенности данного метода:
  /// 1. Если команда зарегистрирована на все категории, то метод вызывается один раз и получает в качестве параметра
  /// items все выделенные в «Навигаторе» элементы навигации;
  /// 2. Если команда зарегистрирована на конкретную категорию, то метод будет вызван один раз для всех выделенных
  /// элементов навигации только в том случае, если все они принадлежат одной категории; для всех выделенных
  /// элементов навигации только в том случае, если все они принадлежат указанной категории;
  /// 3. Если команда зарегистрирована на конкретные категорию и тип, то метод будет вызван один раз для всех
  /// выделенных элементов навигации только в том случае, если все они принадлежат указанной категории и типу.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="viewServices">Контейнер сервисов, которыми могут пользоваться команды.</param>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (items == null || items.Count != 1 || ConstsHolder.OriginalObjectVersionID == -1 || (items.GetItemData(0, typeof (IDBObjectTypeID)) as IDBObjectTypeID).Value != ConstsHolder.CopyOfDocumentID)
      return CommandsInfo.Empty;
    CommandsInfo groupCommands = new CommandsInfo();
    groupCommands.Add("OpenDocumentInNewWindow", new CommandInfo(0, new ClickEventHandler(CopyCommandsProvider.OpenDocumentInNewWindow)));
    return groupCommands;
  }

  /// <summary>
  /// Открыть документ, для которого создана копия, в новом окне
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  public static void OpenDocumentInNewWindow(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(itemData.ObjectID, ConstsHolder.OriginalObjectVersionID);
      if (objectAttributeById == null || objectAttributeById.IsNull)
        return;
      long int64 = Convert.ToInt64(objectAttributeById.Value);
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(int64, false);
      if (objectActualCopy == null)
        return;
      ISelectedItems items1 = Intermech.Navigator.ContextMenu.Services.GetItems(objectActualCopy.ObjectID);
      ServiceContainer viewServices1 = new ServiceContainer();
      viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
      ServiceContainer viewServices2 = viewServices1;
      Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items1, (IServiceProvider) viewServices2), (IServiceProvider) viewServices1);
    }
  }
}
