
// Type: Intermech.Navigator.DBObjects.ExcludeCommandProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Предназначен для облегчения процесса создания провайдеров команд контекстного меню,
/// которым надо проверять выполнение некоторого условия для каждого элемента навигации
/// прежде чем определить, какие команды для них возможны.
/// </summary>
internal class ExcludeCommandProvider : IStepwiseCommandsProvider
{
  /// <summary>Допустимы ли команды</summary>
  private bool _allow;
  private bool _allowReplaceVersion;

  public void Preprocess(ISelectedItems items, IServiceProvider viewServices)
  {
    this._allow = true;
    if (((viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L) & 2L) != 0L)
      this._allow = false;
    if (!items.IsCollage)
      return;
    this._allow = false;
  }

  public void Process(ISelectedItems items, int index)
  {
    if (!(items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData1) || itemData1.Value == 0L || itemData1.Value == -1L)
    {
      this._allow = false;
    }
    else
    {
      if (!this._allow)
        return;
      int projectTypeId = SelectedItemsHelper.GetProjectTypeID(items);
      if (!ObjectTypeHelper.IsUnknownObjectTypeID(projectTypeId))
      {
        IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        if (MetaDataHelper.HasApplicability(projectTypeId, itemData.ObjectType, itemData1.RelationType))
          return;
        this._allow = false;
      }
      else
        this._allow = false;
    }
  }

  public void Postprocess(CommandsInfo commandsInfo)
  {
    if (!this._allow)
      return;
    commandsInfo.Add("Exclude", new CommandInfo(8, new ClickEventHandler(ObjectCommands.ExcludeCommand)));
  }

  public bool CanContinue => this._allow;
}
