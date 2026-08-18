// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Imbase.TechCardImbaseObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Imbase.Selection;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Search;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Imbase;

/// <summary>
/// 
/// </summary>
internal class TechCardImbaseObjectCreatorService : 
  ITechCardImbaseObjectCreatorService,
  ICommandsProvider
{
  /// <summary>Список креаторов</summary>
  private readonly IDictionary<int, TechCardImbaseObjectCreator> _creators = (IDictionary<int, TechCardImbaseObjectCreator>) new ConcurrentDictionary<int, TechCardImbaseObjectCreator>();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="serviceProvider"></param>
  private void RegisterServices(System.IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (IFactory)) is IFactory service))
      return;
    service.AddCommandsProvider(1, (ICommandsProvider) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <returns></returns>
  internal TechCardImbaseObjectCreator GetCreator(int objectTypeId)
  {
    TechCardImbaseObjectCreator creator;
    if (!this._creators.TryGetValue(objectTypeId, out creator))
    {
      creator = new TechCardImbaseObjectCreator(objectTypeId);
      this.RegisterCreator(creator);
    }
    return creator;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="creator"></param>
  private void RegisterCreator(TechCardImbaseObjectCreator creator)
  {
    this._creators[creator.ObjectTypeId] = creator != null ? creator : throw new ArgumentNullException(nameof (creator));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="creator"></param>
  internal void UnRegisterCreator(TechCardImbaseObjectCreator creator)
  {
    if (creator == null)
      throw new ArgumentNullException(nameof (creator));
    this._creators.Remove(creator.ObjectTypeId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="serviceProvider"></param>
  internal TechCardImbaseObjectCreatorService(System.IServiceProvider serviceProvider)
  {
    this.RegisterServices(serviceProvider);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <param name="items"></param>
  /// <param name="contextServices"></param>
  /// <returns></returns>
  public IList<ImbaseObjectInfoItem> SelectObjects(
    int objectTypeId,
    ISelectedItems items,
    System.IServiceProvider contextServices)
  {
    IList<ImbaseObjectInfoItem> collection = (IList<ImbaseObjectInfoItem>) new List<ImbaseObjectInfoItem>();
    using (TechCardImbaseObjectCreator imbaseObjectCreator = new TechCardImbaseObjectCreator(objectTypeId))
    {
      imbaseObjectCreator.UpdateContext(items, contextServices);
      imbaseObjectCreator.Enabled = true;
      if (imbaseObjectCreator.ShowDialog() == DialogResult.OK)
        collection.AddRange<ImbaseObjectInfoItem>(imbaseObjectCreator.SelectedObjItems);
    }
    return collection;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectTypeId"></param>
  /// <param name="items"></param>
  /// <param name="contextServices"></param>
  public void CreateObjects(
    int objectTypeId,
    ISelectedItems items,
    System.IServiceProvider contextServices)
  {
    TechCardImbaseObjectCreator creator = this.GetCreator(objectTypeId);
    creator.UpdateContext(items, contextServices);
    creator.Show();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    if (this._creators.Count == 0)
      return CommandsInfo.Empty;
    ISelectedItems navigatorSelection = SelectedItemsHelper.GetNavigatorSelection();
    if (navigatorSelection == null || navigatorSelection.Count != items.Count)
      return CommandsInfo.Empty;
    for (int index = 0; index < navigatorSelection.Count; ++index)
    {
      IDBObjectID itemData1 = items.GetItemData<IDBObjectID>(index, false);
      IDBObjectID itemData2 = navigatorSelection.GetItemData<IDBObjectID>(index, false);
      if (itemData1 == null || itemData2 == null || itemData1.Value != itemData2.Value)
        return CommandsInfo.Empty;
    }
    foreach (KeyValuePair<int, TechCardImbaseObjectCreator> creator in (IEnumerable<KeyValuePair<int, TechCardImbaseObjectCreator>>) this._creators)
      creator.Value.UpdateContext(items, viewServices);
    return CommandsInfo.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }
}
