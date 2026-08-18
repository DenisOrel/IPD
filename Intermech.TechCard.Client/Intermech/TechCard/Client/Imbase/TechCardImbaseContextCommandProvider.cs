// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Imbase.TechCardImbaseContextCommandProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Imbase;
using Intermech.Imbase.Commands;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Imbase;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard.Imbase;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common;
using Intermech.TechCard.Client.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Imbase;

/// <summary>
/// 
/// </summary>
internal class TechCardImbaseContextCommandProvider : ICommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <param name="additionalInfo"></param>
  private void AddFromImbaseCommand(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) sessionKeeper.Session, false);
      List<int> objTypeIds = (List<int>) null;
      service?.GetCreationTypes(sessionKeeper.Session.SessionGUID, out objTypeIds);
      HashSet<int> visibleObjTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechcardClientUtils.ObjectTypes.GetVisibleObjTypes()).ToHashSet<int>();
      HashSet<int> imbaseVisibleObjTypeHashSet = (objTypeIds != null ? objTypeIds.Where<int>((Func<int, bool>) (item => visibleObjTypes.Contains(item))).ToHashSet<int>() : (HashSet<int>) null) ?? new HashSet<int>();
      MetaDataHelper.GetObjectTypeApplicabilities(itemData.ObjectType).Where<IMSApplicability>((Func<IMSApplicability, bool>) (item => imbaseVisibleObjTypeHashSet.Contains(item.ChildObjectTypeID))).Select<IMSApplicability, int>((Func<IMSApplicability, int>) (item => item.ChildObjectTypeID)).ToArray<int>();
      ImbaseSelectorParams selectorParams = new ImbaseSelectorParams(LocalizationHolder.rm.GetString("TechCard.Client_254"), string.Empty, (object) new ImbaseRootNodeDescriptor(), false, true, (int[]) null, -1)
      {
        SelectedItemsAnalyzer = (object) new SelectedImbaseAnalyzer((IEnumerable<int>) new List<int>((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS)),
        SelectionOptions = SelectionOptions.SelectObjects | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect
      };
      long selObjID = ServiceUtils.GetService<IImbaseSelector>((object) ApplicationServices.Container, true).SelectFromCatalog(selectorParams);
      if (selObjID == -1L)
        return;
      ImbaseContextCommandProvider.DoInsertIntoObject(items.GetParentPath(0), itemData, selObjID);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    ViewStateFlags viewStateFlags = !(viewServices.GetService(typeof (IViewState)) is IViewState service) ? ViewStateFlags.None : service.ViewState;
    if ((viewStateFlags & ViewStateFlags.NodeInTree) == ViewStateFlags.None && (viewStateFlags & ViewStateFlags.NodeInViews) == ViewStateFlags.None || items == null || items.Count != 1)
      return CommandsInfo.Empty;
    CommandsInfo mergedCommands = new CommandsInfo();
    if (!(items.GetItemData(0, typeof (IDBObjectTypeID)) is IDBObjectTypeID itemData))
      return CommandsInfo.Empty;
    int objTypeID = itemData.Value;
    bool flag = false;
    if ((viewStateFlags & ViewStateFlags.ReadOnly) == ViewStateFlags.None)
      flag = MetaDataHelper.GetObjectTypeApplicabilities(objTypeID).Any<IMSApplicability>();
    if (flag)
      mergedCommands.Add("AddFromImbase", new CommandInfo(1, new ClickEventHandler(this.AddFromImbaseCommand)));
    else
      mergedCommands.Suppress("AddFromImbase", 1);
    return mergedCommands;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    new TechCardImbaseContextCommandProvider().RegisterForAllBaseTypes(factory);
  }
}
