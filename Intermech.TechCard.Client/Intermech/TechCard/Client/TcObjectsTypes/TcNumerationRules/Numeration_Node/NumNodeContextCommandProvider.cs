// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node.NumNodeContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.Projects.DBScripts;
using Intermech.Scripting.Services;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TcNumerationRules.Numeration_Node;

/// <summary>
/// Провайдер контестного меню для элементов правила нумерации
/// </summary>
public class NumNodeContextCommandProvider : ICommandsProvider
{
  /// <summary>ViewScriptCommand</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  protected virtual void ViewScriptCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.OpenScript(items, true);
  }

  /// <summary>EditScriptCommand</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  protected virtual void EditScriptCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.OpenScript(items, false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="readOnly"></param>
  protected virtual void OpenScript(ISelectedItems items, bool readOnly)
  {
    if (items == null)
      return;
    for (int index = 0; index < items.Count; ++index)
    {
      if (items.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData && ApplicationServices.Container.GetService(typeof (IScriptPadService)) is IScriptPadService service)
      {
        DBScriptProject scriptProject = service.GetScriptProject(itemData.Value, true);
        service.OpenScriptInIDEWindow((ScriptProject) scriptProject, new OpenInScriptPadParameters()
        {
          ReadOnlyMode = readOnly
        });
      }
    }
  }

  /// <summary>GetMergedCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    if (viewServices == null || items == null)
      return CommandsInfo.Empty;
    long viewState = viewServices.GetService(typeof (IViewState)) is IViewState service ? (long) service.ViewState : 0L;
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("ViewDocument", new CommandInfo(0, new ClickEventHandler(this.ViewScriptCommand)));
    if ((viewState & 2L) == 0L)
      mergedCommands.Add("EditDocument", new CommandInfo(0, new ClickEventHandler(this.EditScriptCommand)));
    return mergedCommands;
  }

  /// <summary>GetGroupCommands</summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }
}
